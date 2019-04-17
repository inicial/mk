using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Windows.Forms;
using DataService;
using NLog;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Model.Voucher
{
    public class BonusAndService : Data, ICloneable
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        private bool _isRight;
        public bool IsRight
        {
            get { return _isRight; }
            set { SetValue(ref _isRight, value); }
        }

        private bool _textChanged;
        public bool TextChanged
        {
            get { return _textChanged; }
            set { SetValue(ref _textChanged, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                SetValue(ref _text, value);
                TextChanged = true;
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetValue(ref _id, value); }
        }

        public BonusAndService()
        {
            
        }

        public BonusAndService(int id, string text, bool isRight, string name)
        {
            Id = id;
            Text = text;
            IsRight = isRight;
            Name = name;
            TextChanged = false;
        }

        public object Clone()
        {
            return new BonusAndService(Id, Text, IsRight, Name);
        }
    }

    public interface ICruiseLoader
    {
        IEnumerable<string> GetSpecialsForCruise(int dlKey);
        IEnumerable<string> GetBonusesForCruise(int dlKey);
        IEnumerable<NameValue> GetCruiseLinesInfo(string brandCode);
        IEnumerable<NameValue> GetDopServicesForCruise(int dlKey);
        IEnumerable<NameValue> GetServicesForCruise(int dlKey);
        IEnumerable<BonusAndService> GetBonusesAndServices(int dlKey);
    }

    public class CruiseLoader : ICruiseLoader
    {
        public IEnumerable<NameValue> GetCruiseLinesInfo(string brandCode)
        {
            var dt = Repository.GetInstance<IVoucherService>().GetCruiseLinesByBrandCode(brandCode);

            return dt != null ?
                dt.Select().Select(r => new NameValue(r.Field<string>("Parametr_name"), r.Field<string>("Parametr_value").Trim())) :
                null;
        }

        public IEnumerable<NameValue> GetDopServicesForCruise(int dlKey)
        {
            var dt = Repository.GetInstance<IVoucherService>().GetDopServicesForCruise(dlKey);

            return dt != null ?
                dt.Select().Select(r => new NameValue(r.Field<string>("CDP_NAME"), r.Field<string>("Text"))) : 
                null;
        }

        public IEnumerable<NameValue> GetServicesForCruise(int dlKey)
        {
            var dt = Repository.GetInstance<IVoucherService>().GetServicesForCruise(dlKey);

            return dt != null ?
                dt.Select().Select(r => new NameValue(r.Field<string>("CDP_NAME"), r.Field<string>("Text"))) :
                null;
        }

        public IEnumerable<string> GetSpecialsForCruise(int dlKey)
        {
            var i = 0;
            var dt = Repository.GetInstance<IVoucherService>().GetSpecialsForCruise(dlKey);

            return dt != null ? 
                dt.Select().Select(r => string.Format("{0}. {1}", ++i, r.Field<string>("Text").Replace("\\", ""))) : 
                null;
        }

        public IEnumerable<string> GetBonusesForCruise(int dlKey)
        {
            var i = 0;
            var dt = Repository.GetInstance<IVoucherService>().GetBonusesForCruise(dlKey);

            return dt != null ? 
                dt.Select().Select(r => string.Format("{0}. {1}", ++i, r.Field<string>("Text").Replace("\\", ""))) :
                null;
        }

        public IEnumerable<BonusAndService> GetBonusesAndServices(int dlKey)
        {
            var dt = Repository.GetInstance<IVoucherService>().GetBonusesAndServices(dlKey);
            return dt != null ? dt.Select().Select(GetBonusAndService) : null;
        }

        private BonusAndService GetBonusAndService(DataRow r)
        {
            return r.Field<string>("CDP_NAME") != null ? 
                new BonusAndService(r.Field<int>("actions_id"), r.Field<string>("Text"), r.Field<int>("isRight") == 1, r.Field<string>("CDP_NAME")) : 
                new BonusAndService(r.Field<int>("actions_id"), null, r.Field<int>("isRight") == 1, r.Field<string>("Text"));
        }
    }

    public class CruiseService : Service
    {
        public static int Index;

        private string _category;
        public string Category
        {
            get { return _category; }
            set { SetValue(ref _category, value); }
        }

        private string _cabinNumber;
        public string CabinNumber
        {
            get { return _cabinNumber; }
            set { SetValue(ref _cabinNumber, value); }
        }

        private string _shipcode;
        public string Shipcode
        {
            get { return _shipcode; }
            set { SetValue(ref _shipcode, value); }
        }

        private string _brandcode;
        public string Brandcode
        {
            get { return _brandcode; }
            set { SetValue(ref _brandcode, value); }
        }

        private string _brandName;
        public string BrandName
        {
            get { return _brandName; }
            set { SetValue(ref _brandName, value); }
        }

        private int _clId;
        public int ClId
        {
            get { return _clId; }
            set { SetValue(ref _clId, value); }
        }

        private string _shipName;
        public string ShipName
        {
            get { return _shipName; }
            set { SetValue(ref _shipName, value); }
        }

        private string GetPort(string portFullName)
        {
            int e = portFullName.IndexOf(',');

            return e > 0 ? 
                portFullName.Substring(0, e) : 
                portFullName;
        }

        private List<string> _portList;
        public List<string> PortList {
            get { return _portList; }
            set
            {
                _portList = value;
                try
                {
                    if (_portList != null && _portList.Count > 0)
                    {
                        Route = new StringBuilder(GetPort(_portList.First()))
                            .Append(" - ")
                            .Append(GetPort(_portList.Last()))
                            .ToString();
                    }
                }
                catch (Exception e)
                {
                    TpLogger.Error("CruiseService", "get route error", e);
                }
            }
        }

        private string _route;
        private int _shipId;
        private string _cabinClass;
        private decimal _portTaxes;
        private decimal _localTax;
        private decimal _totalCost;

        private ObservableCollection<NameValue> _cruiseLineInfo;
        private ObservableCollection<NameValue> _info;

        public string Route
        {
            get { return _route; }
            set { SetValue(ref _route, value); }
        }

        public int ShipId
        {
            get { return _shipId; }
            set { SetValue(ref _shipId, value); }
        }

        public string CabinClass
        {
            get { return _cabinClass; }
            set { SetValue(ref _cabinClass, value); }
        }

        public decimal PortTaxes
        {
            get { return _portTaxes; }
            set { SetValue(ref _portTaxes, value); }
        }

        public decimal LocalTaxes
        {
            get { return _localTax; }
            set { SetValue(ref _localTax, value); }
        }

        private decimal _tips;
        public decimal Tips
        {
            get { return _tips; }
            set { SetValue(ref _tips, value); }
        }

        private decimal _onboardCredit;
        public decimal OnboardCredit
        {
            get { return _onboardCredit; }
            set { SetValue(ref _onboardCredit, value); }
        }

        public decimal TotalCost
        {
            get { return _totalCost; }
            set { SetValue(ref _totalCost, value); }
        }

        private string _optionNumber;
        private bool _isBook;
        private bool _documentGet;
        private bool _documentQuery;
        private DateTime _optionDate;

        public string OptionNumber
        {
            get { return _optionNumber; }
            set { SetValue(ref _optionNumber, value); }
        }

        public bool IsBook
        {
            get { return _isBook; }
            set { SetValue(ref _isBook, value); }
        }

        public bool DocumentGet
        {
            get { return _documentGet; }
            set { SetValue(ref _documentGet, value); }
        }

        public bool DocumentQuery
        {
            get { return _documentQuery; }
            set { SetValue(ref _documentQuery, value); }
        }

        public DateTime OptionDate
        {
            get { return _optionDate; }
            set { SetValue(ref _optionDate, value); }
        }

        public ObservableCollection<NameValue> Info
        {
            get { return _info; }
            set { SetValue(ref _info, value); }
        }

        public ObservableCollection<NameValue> CruiseLineInfo
        {
            get { return _cruiseLineInfo; }
            set { SetValue(ref _cruiseLineInfo, value); }
        }

        private ObservableCollection<NameValue> _dopServices;
        public ObservableCollection<NameValue> DopServices
        {
            get { return _dopServices; }
            set { SetValue(ref _dopServices, value); }
        }

        private ObservableCollection<string> _specials;
        public ObservableCollection<string> Specials
        {
            get { return _specials; }
            set { SetValue(ref _specials, value); }
        }

        private ObservableCollection<string> _bonuses;
        public ObservableCollection<string> Bonuses
        {
            get { return _bonuses; }
            set { SetValue(ref _bonuses, value); }
        }

        private ObservableCollection<NameValue> _services;
        public ObservableCollection<NameValue> Services
        {
            get { return _services; }
            set { SetValue(ref _services, value); }
        }

        private ObservableCollection<BonusAndService> _bonusesAndServices;
        public ObservableCollection<BonusAndService> BonusesAndServices
        {
            get { return _bonusesAndServices; }
            set { SetValue(ref _bonusesAndServices, value); }
        }

        private ObservableCollection<string> _cabinCountList;
        public ObservableCollection<string> CabinCountList
        {
            get { return _cabinCountList; }
            set { SetValue(ref _cabinCountList, value); }
        }

        private string _cabinDef;
        public string CabinDef
        {
            get { return _cabinDef; }
            set { SetValue(ref _cabinDef, value); }
        }

        private string _documentsStatus;
        public string DocumentsStatus
        {
            get { return _documentsStatus; }
            set { SetValue(ref _documentsStatus, value); }
        }

        private string _optionDateStr;
        public string OptionDateStr
        {
            get { return _optionDateStr; }
            set { SetValue(ref _optionDateStr, value); }
        }

        private int _nights;
        public int Nights
        {
            get { return _nights; }
            set { SetValue(ref _nights, value); }
        }

        private decimal _cruiseRate;
        public decimal CruiseRate
        {
            get { return _cruiseRate; }
            set { SetValue(ref _cruiseRate, value); }
        }

        private ICruiseLoader _loader;

        public CruiseService()
            : base()
        {
            Index++;
        }
        
        public CruiseService(Service source, ICruiseLoader loader = null)
            : base(source)
        {
            Index++;

            _loader = loader ?? Repository.GetInstance<ICruiseLoader>();

            IVoucherService serv = Repository.GetInstance<IVoucherService>();

            DataRow row = serv.GetShipInfo2(DlKey);

            CabinCountList = new ObservableCollection<string>(new[] { "Много", "Еще есть", "Последняя каюта" });

            if (row != null)
            {
                CabinNumber = row.Field<string>("cabinNumber");
                Category = row.Field<string>("category");
                ClId = row.Field<int?>("clId") ?? 0;
                Shipcode = row.Field<string>("shipcode");
                Brandcode = row.Field<string>("brandcode");
                OptionNumber = row.Field<string>("OP_number");
                BrandName = serv.GetCruiseBrandName2(Brandcode);

                if (row.Field<DateTime?>("OP_date_end") != null)
                {
                    IsBook = row.Field<bool>("OP_IsBook");
                    DocumentGet = row.Field<bool>("OP_DOCUMENT_GET");
                    DocumentQuery = row.Field<bool>("OP_DOCUMENT_QUERY");
                    OptionDate = row.Field<DateTime>("OP_date_end");
                    CabinDef = row.Field<string>("OP_LEVEL_CABIN");
                }

                var portList = serv.GetItinerary2(DgCode).Select().Select(r => r.Field<string>("locname_ru")).ToList();

                PortList = portList;

                if(ClId > 0)
                {
                    var shipRow = serv.GetShipName(ClId, Shipcode);
                    if (shipRow != null)
                    {
                        ShipName = shipRow.Field<string>("shipName");
                        ShipId = shipRow.Field<int>("id");
                    }
                }
                else
                    ShipName = "";

                OptionDateStr = !IsBook ? OptionDate.ToString("yy-MM-dd") : "Опция подтверждена";
                DocumentsStatus = DocumentGet ? "Документы получены" : DocumentQuery ? "Документы запрошены" : null;
            }

            try
            {
                CruiseLineInfo = new ObservableCollection<NameValue>(_loader.GetCruiseLinesInfo(Brandcode).Where(n => !((string)n.Name).Equals("URL", StringComparison.OrdinalIgnoreCase)));
                DopServices = new ObservableCollection<NameValue>(_loader.GetDopServicesForCruise(DlKey));
                Specials = new ObservableCollection<string>(_loader.GetSpecialsForCruise(DlKey));
                Bonuses = new ObservableCollection<string>(_loader.GetBonusesForCruise(DlKey));
                Services = new ObservableCollection<NameValue>(_loader.GetServicesForCruise(DlKey));
                BonusesAndServices = new ObservableCollection<BonusAndService>(_loader.GetBonusesAndServices(DlKey));
            }
            catch (Exception e)
            {
                TpLogger.Error("Ошибка загрузки информации о круизе", e.Message, e);
            }

            if (ShipId != 0 && Category != null && !Category.Equals(string.Empty)) 
                CabinClass = serv.GetCabinClasses(ShipId, Category);

            Nights = (DateEnd - Date).Days;

            if (Group != null && Group.RelatedServices != null && Group.RelatedServices.Count > 0)
            {
                var portTaxService = Group.RelatedServices.FirstOrDefault(s => s.SvKey == (int)Voucher.SvType.PortSbor);
                if (portTaxService != null)
                    PortTaxes = portTaxService.Brutto;

                var localTaxService = Group.RelatedServices.FirstOrDefault(s => s.SvKey == (int)Voucher.SvType.Taxes);
                if (localTaxService != null)
                    LocalTaxes = localTaxService.Brutto;

                var tipsService = Group.RelatedServices.FirstOrDefault(s => s.SvKey == (int)Voucher.SvType.Tips);
                if (tipsService != null)
                    Tips = tipsService.Brutto;

                var onboardCredit = Group.RelatedServices.FirstOrDefault(s => s.SvKey == (int)Voucher.SvType.OnboardCredit);
                if (onboardCredit != null)
                    OnboardCredit = onboardCredit.Brutto;
                
                TotalCost = Group.Setting.Payment.PaymentValue.Cost;
            }
            else
            {
                TotalCost = Brutto;
            }

            TotalCost += Discount;
            CruiseRate = Brutto + Discount;

            GetFullName(Index);
        }

        public sealed override void GetFullName(int number)
        {
            FullName = String.Format("№{0} {1}", number, ServiceName);
        }
    }
}
