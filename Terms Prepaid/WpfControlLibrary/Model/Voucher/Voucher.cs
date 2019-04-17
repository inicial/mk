using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using DataService;
using Gecko.WebIDL;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Flight;
using WpfControlLibrary.Util;


namespace WpfControlLibrary.Model.Voucher
{
    public class Voucher : Data
    {
        private static readonly string[] _serviceHistoryStringArray = {"DL_PAYMENTDATE", "DL_PPAYMENTDATE", "DL_PPAYMENTVALUE",
                        "DL_DISCOUNT", "DL_Control"};

        public delegate void ErrorBronCallback(Flight.Flight flight);

        private ErrorBronCallback _errorBronCallback;

        public enum SvType
        {
            Ap = 1,                 // А_П
            Transfer = 2,           // Трансфер
            Hotel = 3,              // Отель
            Visa = 5,               // Виза
            Insur = 6,              // Страховка
            PortSbor = 1059,        // Портовые сборы
            Tips = 1238,            // Чаевые
            Taxes = 1520,           // Местные налоги
            Cruise = 3143,          // Круиз
            DurationCorrect = 3161, // Cкрытая услуга FIT:: Круизы/Корректировка длительности ,9 дней/
            Fuel = 3169,            // Топливный сбор
            Discount = 3170,        // Скидка
            RailwayTicket = 3171,   // Ж/д билет
            OnboardCredit = 3175,   // Бортовой кредит
            Surcharge = 3176,       // Доплата
            TransferNew = 3177,     // Трансфер новый
            Substitution = 3179,    // Услуга для подмены
            ShipVisit = 1380,       // Ship Visit
            AddServiceCruise = 3181,// Дополнительная услуга в круизе
            DiningСruise = 3182,    // Питание в Круизе
            СostCorrectVisa = 3183, // Корректировка стоимости визовых услуг по дате сдачи  документов
            Avia = 3184             // Авиаперелет
        }

        public enum IdTypes
        {
            Cruise = 1,
            AddServiceForCruise = 2,
            Hotel = 3,
            HotelFit = 4,
            TransferGroup = 5,
            TransferInd = 6,
            Excursion = 7,
            Avia = 10,
            Dop = 11,
            Inshurance = 12,
            Visa = 13,
            Other = 14,
            AllInclusive = 15,
            Other2 = 16
        }

        private const int SvKeyVisa = 5;
        private const int SvKeyAvia = 3184;
        private const int SvKeyCruise = 3143;
        private const int SvKeyPortSbor = 1059;
        private const int svKeyTips = 1238;
        private const int SvKeyInsur = 6;
        
        private IVoucherService _serv;
        private Service _cruise;
        private DataTable _servicesSettings;
        public bool AccessPartnerFlag = false;

        private readonly List<int> CruiseAllSvKey = new List<int>(
            new int[] { SvKeyPortSbor, SvKeyCruise, (int)SvType.Taxes, (int)SvType.DiningСruise, (int)SvType.AddServiceCruise });
        
        private readonly List<int> NoStatusFilterList = new List<int>(
            new int[] { SvKeyPortSbor, SvKeyVisa, SvKeyInsur});

        private int _seviceCount;
        public int SeviceCount
        {
            get { return _seviceCount; }
            set { SetValue(ref _seviceCount, value); }
        }

        private string _dgCode;
        public string DgCode
        {
            get { return _dgCode; }
            set { SetValue(ref _dgCode, value); }
        }

        private DateTime _tourDate;
        public DateTime TourDate
        {
            get { return _tourDate; }
            set { SetValue(ref _tourDate, value); }
        }

        private ObservableCollection<ServiceGroup> _groupList;
        public ObservableCollection<ServiceGroup> GroupList
        {
            get { return _groupList; }
            set { SetValue(ref _groupList, value); }
        }

        private ObservableCollection<Service> _serviceList;
        public ObservableCollection<Service> ServiceList
        {
            get { return _serviceList; }
            set { SetValue(ref _serviceList, value);}
        }

        private ObservableCollection<Service> _aviaServiceList;
        public ObservableCollection<Service> AviaServiceList
        {
            get { return _aviaServiceList; }
            set { SetValue(ref _aviaServiceList, value); }
        }


        private ObservableCollection<Service> _transferServiceList;
        public ObservableCollection<Service> TransferServiceList
        {
            get { return _transferServiceList; }
            set { SetValue(ref _transferServiceList, value); }
        }

        private ObservableCollection<Service> _cruiseServiceList;
        public ObservableCollection<Service> CruiseServiceList
        {
            get { return _cruiseServiceList; }
            set { SetValue(ref _cruiseServiceList, value); }
        }

        private ObservableCollection<Service> _visaServiceList;
        public ObservableCollection<Service> VisaServiceList
        {
            get { return _visaServiceList; }
            set { SetValue(ref _visaServiceList, value); }
        }

        private ObservableCollection<Service> _visaCanceledServiceList;
        public ObservableCollection<Service> VisaCanceledServiceList
        {
            get { return _visaCanceledServiceList; }
            set { SetValue(ref _visaCanceledServiceList, value); }
        }

        private ObservableCollection<Service> _hotelServiceList;
        public ObservableCollection<Service> HotelServiceList
        {
            get { return _hotelServiceList; }
            set { SetValue(ref _hotelServiceList, value); }
        }

        private ObservableCollection<Service> _inshurServiceList;
        public ObservableCollection<Service> InshurServiceList
        {
            get { return _inshurServiceList; }
            set { SetValue(ref _inshurServiceList, value); }
        }

        private ObservableCollection<Service> _otherServiceList;
        public ObservableCollection<Service> OtherServiceList
        {
            get { return _otherServiceList; }
            set { SetValue(ref _otherServiceList, value); }
        }

        private ObservableCollection<Service> _generalServiceList;
        public ObservableCollection<Service> GeneralServiceList
        {
            get { return _generalServiceList; }
            set { SetValue(ref _generalServiceList, value); }
        }

        private ObservableCollection<BookingAvia> _bookingAviaList;
        private VoucherSettingInfo _voucherSettingInfo;

        public ObservableCollection<BookingAvia> BookingAviaList
        {
            get { return _bookingAviaList; }
            set { SetValue(ref _bookingAviaList, value); }
        }

        public VoucherSettingInfo VoucherSettingInfo
        {
            get { return _voucherSettingInfo; }
            set { SetValue(ref _voucherSettingInfo, value); }
        }

        public List<ProblemServiceInfo> ProblemServiceInfos { get; set; }

        public bool PartnerFlag { get; set; }


        public Voucher(string dgCode, bool iAccessFlag, ErrorBronCallback errorBronCallback = null)
        {
            DgCode = dgCode;
            AccessPartnerFlag = iAccessFlag;

            _serv = Repository.GetInstance<IVoucherService>();

            VoucherSettingInfo = new VoucherSettingInfo(_dgCode);

            GroupList = GetServiceGroups(_serv.GetDogovorList(_dgCode));

            Grouping();
            
            SetColorIndices();

            SetGroupValue();

            NoStatusFilter();

            GroupingServiceByType();

            LoadCanceledVisas();

            SetNumbers();

            ErrorBronTest(errorBronCallback);

            LoadAllProblemServices();
        }

        private void LoadAllProblemServices()
        {
            var dt = _serv.GetProblemServicesForDogovor(DgCode);
            ProblemServiceInfos = new List<ProblemServiceInfo>();

            if (dt == null)
                return;

            foreach (DataRow row in dt.Rows)
            {
                var p = new ProblemServiceInfo(row.Field<string>("dgcod"), row.Field<int>("dlKey"), row.Field<int>("svKey"),
                    row.Field<int>("problemCode"), row.Field<string>("mpc_name"), row.Field<string>("DL_NAME"));

                ProblemServiceInfos.Add(p);
            }

            foreach (var g in GroupList)
            {
                g.Problems = new ObservableCollection<ProblemServiceInfo>(
                        g.Services.SelectMany(s => ProblemServiceInfos.Where(p => p != null && s != null && p.DlKey == s.DlKey))
                    );
                g.FullName = string.Format("{0} ({1})", 
                    g.MainService != null ? g.MainService.FullName ?? g.MainService.ServiceName : "", 
                    g.UniqProblems != null ? g.UniqProblems.Count : 0);
            }
        }

        public IEnumerable<ProblemServiceInfo> GetProblemServicesByType(ServiceType serviceType)
        {
            int i = 0;
            return GroupList.Where(g => g.MainService != null && g.MainService.SvKey == (int)serviceType && g.UniqProblems != null && g.UniqProblems.Count > 0)
                .OrderBy(g => g.MainService.DlKey)
                .SelectMany(g => g.UniqProblems.Select(p =>
                {
                    var p2 = (ProblemServiceInfo) p.Clone();
                    p2.ServiceName = GetService(g.MainService.DlKey).FullName;
                    p2.Rownum = ++i;
                    return p2;
                }));
        }

        public ObservableCollection<ServiceGroup> GetProblemServiceGroup(ServiceType serviceType)
        {
            return new ObservableCollection<ServiceGroup>(GroupList.Where(g => ProblemTest(g, serviceType)).OrderBy(g => g.MainService.DlKey));
        }

        private bool ProblemTest(ServiceGroup g, ServiceType serviceType)
        {
            int svKey = -1;
            try
            {
                svKey = g.MainService.SvKey;
            } catch (NullReferenceException) {
                try
                {
                    svKey = g.RelatedServices.FirstOrDefault().SvKey;
                } catch (NullReferenceException)
                { }
            }
            return svKey != -1 && svKey == (int)serviceType && g.UniqProblems != null && g.UniqProblems.Count > 0;
        }

        private void ErrorBronTest(ErrorBronCallback errorBronCallback)
        {
            if (_aviaServiceList != null && _aviaServiceList.Count > 0)
            {
                var bookingAvia = ((AviaService)_aviaServiceList.Last()).BookingAvia;
                Flight.Flight flight;

                if (bookingAvia.ErrorBronTest(bookingAvia.MaxWaitingTime, out flight))
                {
                    if (errorBronCallback != null)
                    {
                        errorBronCallback.Invoke(flight);
                    }
                }
            }
        }

        private ObservableCollection<ServiceGroup> GetServiceGroups(DataTable servicesTable)
        {
            VisaService.Index = 0;
            AviaService.Index = 0;
            CruiseService.Index = 0;
            HotelService.Index = 0;
            InshurService.Index = 0;
           
            AviaServiceList = new ObservableCollection<Service>();
            CruiseServiceList = new ObservableCollection<Service>();
            VisaServiceList = new ObservableCollection<Service>();
            VisaCanceledServiceList = new ObservableCollection<Service>();
            HotelServiceList = new ObservableCollection<Service>();
            OtherServiceList = new ObservableCollection<Service>();
            InshurServiceList = new ObservableCollection<Service>();
            TransferServiceList = new ObservableCollection<Service>();

            var rows = servicesTable.Select();

            var sGroups = new ObservableCollection<ServiceGroup>(rows
                .Where(s => s.Field<int>("typeId").In(1, 2)).GroupBy(s => s.Field<int?>("bronId") ?? 0)
                .Union(rows.Where(s => s.Field<int?>("bronId") != null).GroupBy(s => s.Field<int?>("bronId") ?? 0))
                .Union(rows.Where(s => !s.Field<int>("typeId").In(1, 2) && !s.Field<int>("typeId").In(5, 6) && s.Field<int?>("bronId") == null).GroupBy(s => s.Field<int>("DL_KEY")))
                .Union(rows.Where(s => s.Field<int?>("typeId") == 5).GroupBy(s => s.Field<int?>("bronId") ?? 0))
                .Union(rows.Where(s => s.Field<int?>("typeId") == 6).GroupBy(s => s.Field<int?>("bronId") ?? 0))
                .Select(g => GetGroup(g.OrderBy(r => r.Field<int>("rn")))));

            ServiceList = new ObservableCollection<Service>(sGroups.SelectMany(g => g.Services).OrderBy(s => s.DlKey));

            return new ObservableCollection<ServiceGroup>(sGroups);
        }

        private ServiceGroup GetGroup(IOrderedEnumerable<DataRow> services)
        {
            var mainService = NewCommonService(services.FirstOrDefault());

            var sGroup = new ServiceGroup(mainService, services
                .Skip(1)
                .Select(NewCommonService), _cruise);
            mainService.Group = sGroup;

            return sGroup;
        }

        public static ServiceType GetServiceType(int svKey, int typeId)
        {
            switch (svKey)
            {
                case (int)SvType.Avia:
                    return ServiceType.Avia;

                case (int)SvType.Cruise:
                    return ServiceType.Cruise;

                case (int)SvType.Insur:
                    return  ServiceType.Inshur;

                case (int)SvType.Visa:
                    return ServiceType.Visa;

                case (int)SvType.Hotel:
                    return typeId == (int)IdTypes.Cruise ? ServiceType.Cruise : ServiceType.Hotel;

                default:
                    return ServiceType.Unknow;
            }
        }

        private Service NewCommonService(DataRow row)
        {
            if (row == null) return null;

            var service = new Service(this, row);

            if (service.SvKey == (int)SvType.Cruise || service.TypeId == (int)TypesId.Cruise)
                _cruise = service;

            return service;
        }

        private void SetRelatedServices(Service general, IList<Service> other)
        {
            // Установка связанных услуг
            if (general != null && other != null && other.Count() > 1)
            {
                general.RelatedServices = new List<Service>();
                var cruiseServiceOther = other.Where(s => s != general);
                general.RelatedServices.AddRange(cruiseServiceOther);

                foreach (var relatedService in general.RelatedServices)
                    relatedService.Parent = general;
            }
        }

        private void SetRelatedServices(IList<Service> other)
        {
            foreach (var general in other)
            {
                // Установка связанных услуг
                if (general != null && other != null && other.Count() > 1)
                {
                    general.RelatedServices = new List<Service>();
                    var cruiseServiceOther = other.Where(s => s != general);
                    general.RelatedServices.AddRange(cruiseServiceOther);
                }
            }
        }

        private void Grouping()
        {
            /*var cruises = ServiceList
                .Where(s => CruiseAllSvKey.Exists(svKey => svKey.Equals(s.SvKey)))
                .OrderByDescending(s => s.SvKey);

            // Установка связанных услуг для круиза 
            var cruise = cruises.FirstOrDefault(s => s.SvKey == SvKeyCruise);*/

            var cruise = ServiceList.FirstOrDefault(s => s.TypeId == 1);
            var cruises = ServiceList.Where(s => s.TypeId == 1 || s.TypeId == 2).ToArray();
            var cruisesAdd = ServiceList.Where(s => s.TypeId == 2).ToArray();

            SetRelatedServices(cruise, cruisesAdd.ToList());
            SetClassBounds(cruise, cruises.LastOrDefault(), cruises);

            var svKeyGroups = ServiceList
                .Except(cruises)
                .Where(s => s.SvKey != SvKeyAvia)
                .GroupBy(s => s.SvKey)
                .OrderByDescending(g => g.Key);

            foreach (var group in svKeyGroups)
                SetRelatedServices(group.ToList());
            
            SetGroupBounds(svKeyGroups);

            // Авиаперелеты
            var aviaGroups = ServiceList
                .Where(s => s.SvKey == SvKeyAvia)
                .GroupBy(s => s.BronId)
                .OrderBy(g => g.Key);

            foreach (var group in aviaGroups)
                SetRelatedServices(group.FirstOrDefault(), group.ToList());
            
            SetGroupBounds(aviaGroups);

            ServiceList = new ObservableCollection<Service>(cruises.Union(aviaGroups.SelectMany(g => g))
                .Union(svKeyGroups.SelectMany(g => g)));

            var c = ServiceList.FirstOrDefault(s => s.SvKey == SvKeyCruise);
            if (c != null && ServiceList.IndexOf(c) != 0)
            {
                var item = ServiceList[0];
                var index = ServiceList.IndexOf(c);
                ServiceList[index] = item;
                ServiceList[0] = c;
            }
        }

        private void SetClassBounds(Service first, Service last, Service[] services = null)
        {
            if (first != null && last != null)
            {
                //first.HideInGroupFlag = true;
                if (first.Equals(last))
                    first.MidClass = true;
                else
                {
                    first.BeginClass = true;
                    last.EndClass = true;
                }
            }
            if (services == null) return;

            bool bDiff = false;
            foreach (Service srv in services)
            {
                if (srv.DlKey != first.DlKey)
                {
                    if (srv.Date != first.Date || srv.Days != first.Days) { bDiff = true; break; }
                }
            }
            if (!bDiff)
            {
                foreach (Service srv in services)
                {
                    if (srv.DlKey != first.DlKey)
                        srv.HideInGroupFlag = true;
                    if (srv.DlKey != last.DlKey)
                        srv.HideBorderClass = true;
                }
            }
        }

        private void SetGroupBounds(IOrderedEnumerable<IGrouping<int, Service>> groups)
        {
            foreach (var g in groups)
            {
                SetClassBounds(g.First(), g.Last());
                
                Service first = g.First();
                Service last = g.Last();
                bool bDiff = false;
                foreach (Service srv in g)
                {
                    if (srv.DlKey != first.DlKey)
                    {
                        if (srv.Date.Date != first.Date.Date || srv.Days != first.Days) { bDiff = true; break; }
                    }
                }
                if (!bDiff)
                {
                    foreach (Service srv in g)
                    {
                        if (srv.DlKey != first.DlKey)
                            srv.HideInGroupFlag = true;
                        if (srv.DlKey != last.DlKey)
                            srv.HideBorderClass = true;
                    }
                }
            }
        }

        private void SetColorIndices()
        {
            int colorIndex = 1;

            foreach (var s in ServiceList)
            {
                if (s.BeginClass || s.MidClass)
                    colorIndex = colorIndex == 1 ? 0 : 1;

                s.ColorIndex = colorIndex;
            }
        }

        private void SetGroupValue()
        {
            foreach (var s in ServiceList)
            {
                if (s.BeginClass || s.MidClass)
                {
                    s.Status = s.ServiceSetting.Status.StatusName;
                }
                else if (s.EndClass)
                {
                    s.Status = s.Parent != null && s.Parent.ServiceSetting.Status != null ? s.Parent.ServiceSetting.Status.DateChange : "";
                }
                else
                {
                    s.Status = "";
                }

                if (!s.BeginClass)
                {
                    if (s.RelatedServices != null && !s.RelatedServices.Exists(serv => serv.Number != s.Number) ||
                        (s.Parent != null && s.Parent.Number == s.Number))
                            s.NumberString = "";
                }
            }
        }
        
        private void NoStatusFilter()
        {
            foreach (var s in ServiceList)
            {
                if (NoStatusFilterList.Exists(svKey => svKey.Equals(s.SvKey)))
                    s.Status = "";
            }
        }

        public void SetCurrency(Currency currency, decimal price)
        {
            foreach (var service in _serviceList)
                service.SetCurrency(currency, price);
        }

        public void SetFilterKey(int FilterKey)
        {
            foreach (var service in _serviceList)
            {
                service.FilterFlag = false;
                if (FilterKey > 0 && service.KeyForSerch != FilterKey) service.FilterFlag = true;
            }
        }

        public void SetPartnerFlag(bool bPartner)
        {
            PartnerFlag = bPartner;
            foreach (var service in _serviceList)
            {
                service.SetPartnerFlag(PartnerFlag);
            }
        }


        private void GroupingServiceByType()
        {
            VisaService.Index = 0;
            AviaService.Index = 0;
            CruiseService.Index = 0;
            TransferService.Index = 0;

            for (int i = 0; i < ServiceList.Count; i++)
            {
                var s = ServiceList[i];

                switch ((SvType) s.SvKey)
                {
                    case SvType.Avia:
                        if (s.BeginClass || s.MidClass)
                            AviaServiceList.Add(new AviaService(s));
                        break;

                    case SvType.Cruise:
                        CruiseServiceList.Add(new CruiseService(s));

                        // translate parameters names
                        CruiseService cs = (CruiseService)CruiseServiceList[CruiseServiceList.Count-1];
                        if (s != null)
                        {
                            ObservableCollection<NameValue> c = cs.CruiseLineInfo;
                            if (c != null)
                            {
                                for (int p = 0; p < c.Count; p++)
                                {
                                    string p_name = c[p].Name.ToString();
                                    if (p_name.IndexOf("Login") >= 0) c[p].Name = p_name.Replace("Login", "Логин");
                                    if (p_name.IndexOf("Password") >= 0) c[p].Name = p_name.Replace("Password", "Пароль");
                                    if (p_name.Trim() == "Login") c[p].Name = "Логин";
                                    if (p_name.Trim() == "Login_new") c[p].Name = "Логин";
                                    if (p_name.Trim() == "логин") c[p].Name = "Логин";
                                    if (p_name.Trim() == "Password") c[p].Name = "Пароль";
                                    if (p_name.Trim() == "Password_new") c[p].Name = "Пароль";
                                    if (p_name.Trim() == "пароль") c[p].Name = "Пароль";
                                }
                            }
                        }

                        break;

                    case SvType.Transfer:
                    case SvType.TransferNew:
                        if (s.BeginClass || s.MidClass)
                        {
                            var transfer = new TransferService(s, _cruise, "");
                            if (transfer.IsExists)
                            {
                                //TransferServiceList.Add(new TransferService(s, _cruise));
                                TransferService.Index--;
                                foreach (int dlkey in transfer.DlKeys)
                                {
                                    bool bAdd = false;
                                    string option_number = "";
                                    foreach (TransferService.TransferInfo info in transfer.TransferInfos) 
                                    {
                                        if (info.DlKey == dlkey)
                                        {
                                            bAdd = true;
                                            option_number = info.OptionNumber;
                                        }
                                    }
                                    if (bAdd)
                                    {
                                        TransferService ts = new TransferService(s, _cruise, option_number);
                                        ts.DlKey = dlkey;
                                        TransferServiceList.Add(ts);
                                    }
                                }
                            }
                        }
                        break;

                    case SvType.PortSbor:
                        break;

                    case SvType.Tips:
                        break;

                    case SvType.Taxes:
                        break;

                    case SvType.Insur:
                        InshurServiceList.Add(new InshurService(s));
                        break;

                    case SvType.Hotel:
                        if(s.TypeId == (int)IdTypes.Cruise)
                            CruiseServiceList.Add(new CruiseService(s));
                        else if (s.TypeId == (int)IdTypes.Hotel)
                            HotelServiceList.Add(new HotelService(s));
                        break;

                    case SvType.Visa:
                        VisaServiceList.Add(new VisaService(s));
                        break;

                    default:
                        OtherServiceList.Add(new OtherService(s));
                        break;
                }
            }

            SeviceCount = AviaServiceList.Count + CruiseServiceList.Count + HotelServiceList.Count + OtherServiceList.Count;

            var generals = new List<Service>();

            generals.AddRange(CruiseServiceList.ToList());
            generals.AddRange(AviaServiceList.ToList());
            generals.AddRange(HotelServiceList.ToList());
            generals.AddRange(VisaServiceList.ToList());
            generals.AddRange(OtherServiceList.ToList());
            generals.AddRange(InshurServiceList.ToList());
            generals.AddRange(TransferServiceList.ToList());

            foreach (var g in GroupList)
            {
                g.MainService = GetService(g.MainService.DlKey);
                if (g.MainService != null) g.MainService.Group = g;
            }

            GeneralServiceList = new ObservableCollection<Service>(generals);
        }

        private void LoadCanceledVisas()
        {
            if (VisaServiceList.Count > 0) return;

            if (VisaCanceledServiceList == null) VisaCanceledServiceList = new ObservableCollection<Service>();

            _serv = Repository.GetInstance<IVoucherService>();

            DataTable visas_dt = _serv.GetCanceledVisas(DgCode);
            if (visas_dt == null) return;
            if (visas_dt.Rows.Count == 0) return;

            for (int i = 0; i < visas_dt.Rows.Count; i++)
            {
                DataRow row = visas_dt.Rows[i];
                string name = row.Field<String>("HI_TEXT");
                VisaService visa = new VisaService(null);
                visa.Name = name;
                visa.Name2 = name;
                visa.FullName = name;
                visa.Status = "Отказ от виз";
                visa.ServiceType = name;
                VisaCanceledServiceList.Add(visa);
            }
        }

        private void SetNumbers()
        {
            int serviceNumber = 1;

            SetServiceNumber(CruiseServiceList, ref serviceNumber);
            SetServiceNumber(AviaServiceList, ref serviceNumber);
            SetServiceNumber(OtherServiceList, ref serviceNumber);
            SetServiceNumber(InshurServiceList, ref serviceNumber);
            SetServiceNumber(VisaServiceList, ref serviceNumber);
            SetServiceNumber(HotelServiceList, ref serviceNumber);
            SetServiceNumber(TransferServiceList, ref serviceNumber);
        }

        private void SetServiceNumber(IEnumerable<Service> services, ref int number)
        {
            foreach (var service in services)
                service.ServiceNumber = number++;
        }

        public Service GetService(ObservableCollection<Service> collection, int dlKey)
        {
            return collection.FirstOrDefault(s => s.DlKey == dlKey);
        }

        public Service GetService(int dlKey)
        {
            return GetService(_aviaServiceList, dlKey) ??
                          GetService(_cruiseServiceList, dlKey) ??
                          GetService(_visaServiceList, dlKey) ??
                          GetService(_hotelServiceList, dlKey) ??
                          GetService(_otherServiceList, dlKey) ??
                           GetService(_inshurServiceList, dlKey) ??
                           GetService(_transferServiceList, dlKey);


            //return _serviceList.FirstOrDefault(s => s.DlKey == dlKey);
        }
    }
}
