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
using WpfControlLibrary.ViewModel;


namespace WpfControlLibrary.Model.Voucher
{
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
        private bool _touristInfoFlag;
        private bool _serviceInfoFlag;

        private ObservableCollection<NameValue> _cruiseLineInfo;
        private ObservableCollection<NameValue> _cruiseTouristInfo;
        private ObservableCollection<NameValue> _info;

        public bool TouristInfoFlag
        {
            get { return _touristInfoFlag; }
            set { SetValue(ref _touristInfoFlag, value); }
        }

        public bool ServiceInfoFlag
        {
            get { return _serviceInfoFlag; }
            set { SetValue(ref _serviceInfoFlag, value); }
        }

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
        private DateTime _documentGetDate;
        private DateTime _documentQueryDate;

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

        public DateTime DocumentGetDate
        {
            get { return _documentGetDate; }
            set { SetValue(ref _documentGetDate, value); }
        }

        public DateTime DocumentQueryDate
        {
            get { return _documentQueryDate; }
            set { SetValue(ref _documentQueryDate, value); }
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

        public ObservableCollection<NameValue> CruiseTouristInfo
        {
            get { return _cruiseTouristInfo; }
            set { SetValue(ref _cruiseTouristInfo, value); }
        }

        //        private string _cruiseTarif_Name;
//        public string CruiseTarif_Name
//        {
//            get { return _cruiseTarif_Name; }
//            set { SetValue(ref _cruiseTarif_Name, value); }
//        }

//        private string _cruiseTarif_Value;
//        public string CruiseTarif_Value
//        {
//            get { return _cruiseTarif_Value; }
//            set { SetValue(ref _cruiseTarif_Value, value); }
//        }

        //private ObservableCollection<NameValue> lst_DopServices;
        public ObservableCollection<DopServiceItem> lst_DopServices;

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

        private ObservableCollection<BonusAndService> _nobonusesAndServices;
        public ObservableCollection<BonusAndService> NoBonusesAndServices
        {
            get { return _nobonusesAndServices; }
            set { SetValue(ref _nobonusesAndServices, value); }
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
            set 
            { 
                SetValue(ref _nights, value);
                if (_nights > 0)
                    NightsString = _nights.ToString() + " н / " + (_nights + 1).ToString() + " дн";
                else
                    NightsString = "";
            }
        }

        private string _nightsString;
        public string NightsString
        {
            get { return _nightsString; }
            set { SetValue(ref _nightsString, value); }
        }

        private string _passengers;
        public string Passengers
        {
            get { return _passengers; }
            set { SetValue(ref _passengers, value); }
        }

        private decimal _cruiseRate;
        public decimal CruiseRate
        {
            get { return _cruiseRate; }
            set { SetValue(ref _cruiseRate, value); }
        }

        //private string _cruiseTarifName;
        //public string CruiseTarifName
        //{
        //    get { return _cruiseTarifName; }
        //    set { SetValue(ref _cruiseTarifName, value); }
        //}

        private ICruiseLoader _loader;

        public CruiseService(Service source, ICruiseLoader loader = null)
            : base(source)
        {
            Index++;
            
            _loader = loader ?? Repository.GetInstance<ICruiseLoader>();

            DataRow row = VoucherService.GetShipInfo2(DlKey);

            CabinCountList = new ObservableCollection<string>(new[] { "Много", "Еще есть", "Последняя каюта" });

            OptionDate = DateTime.Now.Date;
            OptionDateStr = DateTime.Now.ToString("dd.MM.yyyy   HH:mm"); // OptionDate.ToString("dd.MM.yyyy   HH:mm");

            if (row != null)
            {
                CabinNumber = row.Field<string>("cabinNumber");
                Category = row.Field<string>("category");
                ClId = row.Field<int?>("clId") ?? 0;
                Shipcode = row.Field<string>("shipcode");
                Brandcode = row.Field<string>("brandcode");
                OptionNumber = row.Field<string>("OP_number");
                BrandName = VoucherService.GetCruiseBrandName2(Brandcode);

                if (row.Field<DateTime?>("OP_date_end") != null)
                {
                    IsBook = row.Field<bool>("OP_IsBook");
                    DocumentGet = row.Field<bool>("OP_DOCUMENT_GET");
                    DocumentQuery = row.Field<bool>("OP_DOCUMENT_QUERY");
                    OptionDate = row.Field<DateTime>("OP_date_end");
                    CabinDef = row.Field<string>("OP_LEVEL_CABIN");
                }

                if (string.IsNullOrEmpty(OptionNumber) || OptionDate.Year < 2000) 
                    OptionDate = DateTime.Now.Date;

                var portList = VoucherService.GetItinerary2(DgCode).Select().Select(r => r.Field<string>("locname_ru")).ToList();

                PortList = portList;

                if(ClId > 0)
                {
                    var shipRow = VoucherService.GetShipName(ClId, Shipcode);
                    if (shipRow != null)
                    {
                        ShipName = shipRow.Field<string>("shipName");
                        ShipId = shipRow.Field<int>("id");
                    }
                }
                else
                    ShipName = "";

                DateTime BronirDT = DateTime.Now;
                //if (IsBook)
                //{
                //    BronirDT = VoucherService.GetBronirDate(DgCode);
                //}
                DocumentQueryDate = new DateTime(2000, 1, 1); 
                //if (DocumentQuery)
                //{
                //    DocumentQueryDate = VoucherService.GetDocumentQueryDate(DgCode);
                //}
                DocumentGetDate = new DateTime(2000, 1, 1);
                //if (DocumentGet)
                //{
                //    DocumentGetDate = VoucherService.GetDocumentGetDate(DgCode);
                //}

                DateTime _BronirDT = new DateTime(2000, 1, 1);
                DateTime _DocQueryDT = new DateTime(2000, 1, 1);
                DateTime _DocGetDT = new DateTime(2000, 1, 1);
                try
                {
                    VoucherService.GetBronirDocumentDates(DgCode, ref _BronirDT, ref _DocQueryDT, ref _DocGetDT);
                }
                catch (System.Exception ex)
                {
                    TpLogger.Debug("VoucherService.GetBronirDocumentDates", ex);
                }
                if (IsBook && _BronirDT.Year > 2000) BronirDT = _BronirDT;
                if (DocumentQuery && _DocQueryDT.Year > 2000) DocumentQueryDate = _DocQueryDT;
                if (DocumentGet && _DocGetDT.Year > 2000) DocumentGetDate = _DocGetDT;

                if (OptionDate.Year < 2000) OptionDate = DateTime.Now.AddDays(3);

                //OptionDateStr = !IsBook ? OptionDate.ToString("yy-MM-dd") : "Опция подтверждена";
                //OptionDateStr = IsBook ? OptionDate.ToString("dd.MM.yyyy   HH:mm") : "Опция не подтверждена";
                //OptionDateStr = IsBook ? OptionDate.ToString("dd.MM.yyyy   HH:mm") : DateTime.Now.ToString("dd.MM.yyyy   HH:mm");
                OptionDateStr = IsBook ? BronirDT.ToString("dd.MM.yy   HH:mm") : DateTime.Now.ToString("dd.MM.yy   HH:mm");
                DocumentsStatus = DocumentGet ? "Документы получены" : DocumentQuery ? "Документы запрошены" : null;
            }

            try
            {
                //lst_DopServices = new ObservableCollection<NameValue>(_loader.GetDopServicesList());
                lst_DopServices = new ObservableCollection<DopServiceItem>(_loader.GetDopServiceItems());
                

                CruiseLineInfo = new ObservableCollection<NameValue>(_loader.GetCruiseLinesInfo(Brandcode).Where(n => !((string)n.Name).Equals("URL", StringComparison.OrdinalIgnoreCase)));
                CruiseTouristInfo = new ObservableCollection<NameValue>();
                DopServices = new ObservableCollection<NameValue>(_loader.GetDopServicesForCruise(DlKey));
                Specials = new ObservableCollection<string>(_loader.GetSpecialsForCruise(DlKey));
                Bonuses = new ObservableCollection<string>(_loader.GetBonusesForCruise(DlKey));
                Services = new ObservableCollection<NameValue>(_loader.GetServicesForCruise(DlKey));
                BonusesAndServices = new ObservableCollection<BonusAndService>(_loader.GetBonusesAndServices(DlKey));
                NoBonusesAndServices = new ObservableCollection<BonusAndService>(_loader.GetNoBonusesAndServices(DlKey));

                BonusesAndServices_CompleteList();
            }
            catch (Exception e)
            {
                //TpLogger.Error("Ошибка загрузки информации о круизе", e.Message, e);
                TpLogger.Debug("Ошибка загрузки информации о круизе", e);
            }

            if (ShipId != 0 && Category != null && !Category.Equals(string.Empty))
                CabinClass = VoucherService.GetCabinClasses(ShipId, Category);

//            CruiseTarif_Name = "Круизный тариф...";
//            CruiseTarif_Value = ""; // "[название тарифа]";
//            DataRow drTarif = VoucherService.GetCruiseTarif(DlKey);
//            if (drTarif != null)
//            {
//                CruiseTarif_Name = drTarif.Field<string>("CDP_NAME");
//                CruiseTarif_Value = drTarif.Field<string>("Text");
//            }
                 
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

        // добавляет отстутствующие доп.услуги для отображения во вкладке круиза
        public void BonusesAndServices_CompleteList()
        {
            //if (BonusesAndServices == null) return;
            if (NoBonusesAndServices == null) return;

            for (int i = 0; i < lst_DopServices.Count; i++)
            {
                DopServiceItem item = lst_DopServices[i];
                if (item.IsDefault > 0)
                {
                    BonusesAndServices_CompleteAction(item.ID); 
                }
            }

            //BonusesAndServices_CompleteAction(-104); // смена питания
            //BonusesAndServices_CompleteAction(-106); // тип кровати 
            //BonusesAndServices_CompleteAction(-110); // вариант питания
            //BonusesAndServices_CompleteAction(-101); // круизный тариф
            //BonusesAndServices_CompleteAction(-102); // карта лояльности
        }

        public void BonusesAndServices_CompleteAction(int id)
        {
            //if (BonusesAndServices == null) return;
            if (NoBonusesAndServices == null) return;

            foreach (BonusAndService item in NoBonusesAndServices)
            {
                if (item.Id == id) return;
            }

            if (lst_DopServices == null) return;
            if (lst_DopServices.Count == 0) return;
            
            string name = "";
            for (int i = 0; i < lst_DopServices.Count; i++)
            {
                //NameValue row = lst_DopServices[i];
                //if ((int)row.Name == id)
                //{
                //    name = (string)row.Value;
                //    break;
                //}
                DopServiceItem item = lst_DopServices[i];
                if (item.ID == id)
                {
                    name = item.Name;
                    break;
                }
            }

            BonusAndService action = new BonusAndService(id, "", false, name, true, true, false);

            NoBonusesAndServices.Add(action);
        }

        public sealed override void GetFullName(int number)
        {
            FullName = String.Format("№{0} {1}", number, ServiceName);
        }

        public void AddCruiseOption(CruiseInfo cruiseInfo)
        {
            VoucherService.AddCruiseOption(DlKey, "Отказ по спец сервису " + cruiseInfo.SpecCanc,
                cruiseInfo.OptionNumber, cruiseInfo.CabinNumber, cruiseInfo.CabinDef, cruiseInfo.Category,
                cruiseInfo.OptionDate, cruiseInfo.IsBook, cruiseInfo.DocumentQuery, cruiseInfo.DocumentGet);
        }
    }
}
