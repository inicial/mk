using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using Utilities.DataTypes.ExtensionMethods;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class TransferService : ServiceWithTouristData
    {
        public enum TransferDirection
        {
            Unknown, After, Before, Both
        }

        public class TransferInfo : Data
        {
            public int DlKey { get; private set; }
            public string PointFrom { get; private set; }
            public string PointTo { get; private set; }

            public DateTime TimeFrom  { get; private set; }
            public DateTime TimeTo  { get; private set; }
            public DateTime DateOfCreate { get; private set; }

            public TimeSpan Duration  { get; private set; }

            public int? Id { get; set; }

            public int Guide { get; set; }
            public string GuidePhone { get; set; }

            public string GuideItem { get; set; }

            public TransferDirection Direction { get; set; }
            public string OptionNumber { get; set; }

            public string Route { get; set; }

            private IVoucherService _voucherService;
            
            public string[] GuideStatuses { get; set; }

            public DateTime? TimeLimit { get; set; }

            public bool TimeLimitEnabled { get; set; }

            public TransferInfo(DataRow row)
            {
                GuideStatuses = new [] { "Не определен", "Заказан" };
                _voucherService = Repository.GetInstance<IVoucherService>();

                if (row != null)
                {
                    DlKey = row.Field<int>("dl_key");
                    Id = row.Field<int?>("id_transfer");
                    PointFrom = row.Field<string>("point_from");
                    PointTo = row.Field<string>("point_to");
                    Route = GetRoute();
                    TimeFrom = row.Field<DateTime>("time_from");
                    TimeTo = row.Field<DateTime>("time_to");
                    DateOfCreate = row.Field<DateTime>("Date_of_create");
                    OptionNumber = row.Field<string>("OP_number");
                    Duration = TimeTo.Subtract(TimeFrom);
                    var guideBool = row.Field<bool?>("guide");
                    Guide = guideBool != null && (bool)guideBool ? 1 : 0;
                    GuideItem = GuideStatuses[Guide];
                    GuidePhone = row.Field<string>("guide_phone");
                    TimeLimit = row.Field<DateTime?>("PPaymentdaDate");
                }
                else
                {

                }
                Direction = GetTransferDirection(Id);
            }

            public void GetTransferInfo(Service srv)
            {
                if (srv.GetType() != typeof(TransferService)) return;

                TransferService ts = (TransferService)srv;

                DlKey = ts.DlKey;
                //Id = row.Field<int?>("id_transfer");
                //PointFrom = row.Field<string>("point_from");
                //PointTo = row.Field<string>("point_to");
                //Route = GetRoute();
                //TimeFrom = row.Field<DateTime>("time_from");
                //TimeTo = row.Field<DateTime>("time_to");
                //DateOfCreate = row.Field<DateTime>("Date_of_create");
                OptionNumber = ts.OptionNumber;
                //Duration = TimeTo.Subtract(TimeFrom); // ts.Duration
                //var guideBool = row.Field<bool?>("guide");
                //Guide = guideBool != null && (bool)guideBool ? 1 : 0;
                //GuideItem = GuideStatuses[Guide];
                //GuidePhone = row.Field<string>("guide_phone");
                //TimeLimit = row.Field<DateTime?>("PPaymentdaDate");

            }

            public TransferDirection GetTransferDirection(int? transferId)
            {
                if (transferId == null)
                    return TransferDirection.Unknown;

                var row = _voucherService.GetTransferTypeInfo((int)transferId);

                if(row == null)
                    return TransferDirection.Unknown;

                var directionString = row.Field<string>("direction");

                if (directionString == null)
                    return TransferDirection.Unknown;

                return directionString.Equals("after") ? TransferDirection.After
                    : (directionString.Equals("before") ? TransferDirection.Before
                        : (directionString.Equals("both") ? TransferDirection.Both 
                            : TransferDirection.Unknown));
            }

            private string GetRoute()
            {
                return string.Format("{0} - {1}", PointFrom, PointTo);
            }

            public void Save(string optionNumber, DateTime? timeLimit)
            {
                var guidBool = Guide == 1 ? (bool?)true : null;
                _voucherService.SetTransferInto(DlKey, guidBool, GuidePhone, optionNumber, timeLimit);
            }

            public void SaveOwn()
            {
                var guidBool = Guide == 1 ? (bool?)true : null;
                var TTLimit = TimeLimitEnabled ? TimeLimit : (DateTime?)null;
                _voucherService.SetTransferInto(DlKey, guidBool, GuidePhone, OptionNumber, TTLimit);
            }

        }

        public static int Index = 0;

        public DateTime DateOfCreate { get; private set; }

        private DateTime _timeLimit;
        public DateTime TimeLimit
        {
            get { return _timeLimit; }
            set
            {
                if (_timeLimit.Date != value.Date)
                {
                    var newDateTime = new DateTime(value.Year, value.Month, value.Day, _timeLimit.Hour, _timeLimit.Minute, _timeLimit.Second);
                    SetValue(ref _timeLimit, newDateTime);
                }
                else    
                    SetValue(ref _timeLimit, value);
            }
        }

        private bool _timeLimitEnabled;
        public bool TimeLimitEnabled
        {
            get { return _timeLimitEnabled; }
            set { SetValue(ref _timeLimitEnabled, value); }
        }

        public IEnumerable<TransferInfo> TransfersBefore { get; set; }
        public IEnumerable<TransferInfo> TransfersAfter { get; set; }
        public IEnumerable<TransferInfo> TransfersBoth { get; set; }

        //private TransferInfo[] _transferInfos;
        //public TransferInfo[] TransferInfos
        //{
        //    get { return _transferInfos; }
        //    set { _transferInfos = value; }
        //}
        private IEnumerable<TransferInfo> _transferInfos;
        public IEnumerable<TransferInfo> TransferInfos
        {
            get { return _transferInfos; }
            set { _transferInfos = value; }
        }
        private TransferInfo[] _transferLoaded;

        public Service _cruise;
        private List<Service> _transfers;
        public List<Service> Transfers
        {
            get { return _transfers; }
            set { _transfers = value; }
        }

        public List<string> OptionNumbers;
        public List<int> DlKeys;

        private string _optionNumber;
        public string OptionNumber
        {
            get { return _optionNumber; }
            set { SetValue(ref _optionNumber, value); }
        }

        public string MainOptionNumber;
        public string FirstServiceName;

        public bool IsExists { get; set; }

        public TransferService(Service source, Service cruise, string option_number) : base(source)
        {
            _cruise = cruise;
            MainOptionNumber = option_number;
            FirstServiceName = "";

            LoadTransfers(source.RelatedServices);
            Index++;
            GetFullName(Index);
        }

        public void LoadTransfers(List<Service> relateServices)
        {
            _transfers = new List<Service> { this };

            if (relateServices != null && relateServices.Count > 0)
                _transfers.AddRange(relateServices);

            Update();
        }

        public void Update()
        {
            List<int> transferKeysLoaded = new List<int>();
            List<int> transferKeys = _transfers.Select(s => s.DlKey).ToList();
            //_transferLoaded = new TransferInfo[transferKeys.Count];
            
            // add without doubled transfers...
            for (int i1 = 0; i1 < transferKeys.Count; i1++)
            {
                int key = transferKeys[i1];
                bool bAdd = true;
                for (int i2 = 0; i2 < transferKeysLoaded.Count; i2++)
                {
                    if (transferKeysLoaded[i2] == key) bAdd = false;
                }
                if (bAdd)
                {
                    transferKeysLoaded.Add(key);

                    //DataRow row = VoucherService.GetTransferInfo(key);
                    //TransferInfo info = new TransferInfo(row);
                    //if (row == null)
                    //{
                    //    for (int i3 = 0; i3 < _transfers.Count; i3++)
                    //        if (_transfers[i3].DlKey == key) {  info.GetTransferInfo(_transfers[i3]); break;}
                    //}
                    //_transferLoaded[i1] = info;
                }
            }

            //DataRow dr = VoucherService.GetTransferInfo(4325778);

            _transferLoaded = transferKeysLoaded
                .Select(key => VoucherService.GetTransferInfo(key))
                .Where(row => row != null)
                .Select(row => new TransferInfo(row)).ToArray();

            if (MainOptionNumber != "")
                _transferInfos = _transferLoaded.Where(i => i.OptionNumber == MainOptionNumber);
            else
                _transferInfos = _transferLoaded;

            if (_transferInfos == null) return;
            if (_transferInfos.Count<TransferInfo>() == 0) return;

            DateOfCreate = _transferInfos.Min(i => i.DateOfCreate);

            var infoWithTimeLimit = _transferInfos.FirstOrDefault(i => i.TimeLimit != null);
            TimeLimitEnabled = infoWithTimeLimit != null;
            TimeLimit = TimeLimitEnabled ? (DateTime)infoWithTimeLimit.TimeLimit : DateTime.Now.AddHours(3);

            if (OptionNumbers == null) OptionNumbers = new List<string>();
            OptionNumbers.Clear();
            if (DlKeys == null) DlKeys = new List<int>();
            DlKeys.Clear();

            foreach (TransferInfo info in _transferInfos)
            {
                info.TimeLimitEnabled = (info.TimeLimit != null);
                if (info.TimeLimit == null) info.TimeLimit = DateTime.Now.AddHours(3);

                string option_number = info.OptionNumber;
                int dl_key = info.DlKey;

                if (info.OptionNumber == MainOptionNumber)
                {
                    FirstServiceName = "Трансфер: " + info.Route;
                }

                bool bAdd = true;
                foreach (string num in OptionNumbers) { if (num == info.OptionNumber) bAdd = false; }
                if (bAdd)
                {
                    OptionNumbers.Add(info.OptionNumber);
                    DlKeys.Add(info.DlKey);
                }
            }

            var transferWithOptionNumber = _transferInfos.FirstOrDefault(i => i.OptionNumber != null);

            OptionNumber = transferWithOptionNumber != null ? transferWithOptionNumber.OptionNumber
                : _cruise != null ? VoucherService.GetOptionNumber(_cruise.DlKey)
                : null;

            TransfersBefore = GetTransfers(_transferInfos, TransferDirection.Before);
            TransfersAfter = GetTransfers(_transferInfos, TransferDirection.After);
            TransfersBoth = GetTransfers(_transferInfos, TransferDirection.Both);

            IsExists = true;
        }

        public void SaveTransfers()
        {
            var timeLimit = TimeLimitEnabled ? TimeLimit : (DateTime?)null;

            //foreach (var transferInfo in _transferInfos)
            //    transferInfo.Save(OptionNumber, timeLimit);
            foreach (var transferInfo in _transferInfos)
                transferInfo.SaveOwn();
        }

        private IEnumerable<TransferInfo> GetTransfers(IEnumerable<TransferInfo> transferInfos, TransferDirection direction)
        {
            //IEnumerable<TransferInfo> infos = null;
            //if (MainOptionNumber != "")
            //    infos = (transferInfos.Where(i => i.Direction == direction)).Where(i => i.OptionNumber == MainOptionNumber);
            //else                
            //    infos = transferInfos.Where(i => i.Direction == direction);
            //return infos;
            
            return transferInfos.Where(i => i.Direction == direction);
        }

        public sealed override void GetFullName(int number)
        {
            string option_number = "";
            if (MainOptionNumber != "") option_number = "[" + MainOptionNumber + "]";
            string service_name = ServiceName;
            if (FirstServiceName != "") service_name = FirstServiceName;

            FullName = String.Format("№{0} {1}", number, service_name);
            //FullName = String.Format("№{0} {1} {2}", number, option_number, service_name);
            //FullName = "№1 Трансфер, партнер Costa Cruises";
        }
    }
}
