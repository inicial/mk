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

            public TransferInfo(DataRow row)
            {
                GuideStatuses = new [] { "Не определен", "Заказан" };
                _voucherService = Repository.GetInstance<IVoucherService>();
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
                Direction = GetTransferDirection(Id);
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
        }

        public static int Index = 0;
        private IVoucherService _voucherService;

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

        private TransferInfo[] _transferInfos;

        public Service _cruise;
        private List<Service> _transfers;

        private string _optionNumber;
        public string OptionNumber
        {
            get { return _optionNumber; }
            set { SetValue(ref _optionNumber, value); }
        }

        public TransferService(Service source, Service cruise) : base(source)
        {
            _cruise = cruise;
            _voucherService = Repository.GetInstance<IVoucherService>();
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
            _transferInfos = _transfers
                .Select(s => _voucherService.GetTransferInfo(s.DlKey))
                .Where(row => row != null)
                .Select(row => new TransferInfo(row)).ToArray();

            if (_transferInfos == null || _transferInfos.Length == 0)
                return;

            DateOfCreate = _transferInfos.Min(i => i.DateOfCreate);

            var infoWithTimeLimit = _transferInfos.FirstOrDefault(i => i.TimeLimit != null);
            TimeLimitEnabled = infoWithTimeLimit != null;
            TimeLimit = TimeLimitEnabled ? (DateTime)infoWithTimeLimit.TimeLimit : DateTime.Now.AddHours(3);

            var transferWithOptionNumber = _transferInfos.FirstOrDefault(i => i.OptionNumber != null);

            OptionNumber = transferWithOptionNumber != null ? transferWithOptionNumber.OptionNumber
                : _cruise != null ? _voucherService.GetOptionNumber(_cruise.DlKey)
                : null;

            TransfersBefore = GetTransfers(_transferInfos, TransferDirection.Before);
            TransfersAfter = GetTransfers(_transferInfos, TransferDirection.After);
            TransfersBoth = GetTransfers(_transferInfos, TransferDirection.Both);
        }

        public void SaveTransfers()
        {
            var timeLimit = TimeLimitEnabled ? TimeLimit : (DateTime?)null;

            foreach (var transferInfo in _transferInfos)
                transferInfo.Save(OptionNumber, timeLimit);
        }

        private IEnumerable<TransferInfo> GetTransfers(IEnumerable<TransferInfo> transferInfos, TransferDirection direction)
        {
            return transferInfos.Where(i => i.Direction == direction);
        }

        public sealed override void GetFullName(int number)
        {
            FullName = "№1 Трансфер, партнер Costa Cruises";
            //FullName = String.Format("№{0} {1}", number, ServiceName);
        }
    }
}
