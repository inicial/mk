using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Model.Voucher
{
    public enum ServiceType
    {
        Cruise = Voucher.SvType.Cruise,
        Avia = Voucher.SvType.Avia,
        Inshur = Voucher.SvType.Insur,
        Visa = Voucher.SvType.Visa,
        //DopPaket = 9997,
        Hotel = Voucher.SvType.Hotel,
//        TransferOld = Voucher.SvType.Transfer,
        Transfer = Voucher.SvType.TransferNew,
        Unknow = 9999
    }

    public enum TypesId
    {
        Cruise = 1
    }

    /// <summary>
    /// Класс услуга путевки
    /// </summary>
    public class Service : GroupGridItem
    {
        public ServiceType ServType { get; set; }

        private char[] _delimiters = {'/'};

        public string DgCode { get; set; }

        private string _rate;
        public string Rate
        {
            get { return _rate; }
            set { SetValue(ref _rate, value); }
        }

        public Voucher Voucher { get; set; }

        //public bool AccessPartnerFlag = false;

        private int _dlKey;
        public int DlKey
        {
            get { return _dlKey; }
            set { SetValue(ref _dlKey, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                SetValue(ref _number, value);
                NumberString = _number.ToString("F0");
            }
        }

        private string _name2;
        public string Name2
        {
            get { return _name2; }
            set { SetValue(ref _name2, value); }
        }

        private string _slName;
        public string SlName
        {
            get { return _slName; }
            set { SetValue(ref _slName, value); }
        }

        private string _numberString;
        public string NumberString
        {
            get { return _numberString; }
            set { SetValue(ref _numberString, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetValue(ref _isSelected, value); }
        }

        private int _serviceNumber;
        public int ServiceNumber
        {
            get { return _serviceNumber; }
            set { SetValue(ref _serviceNumber, value); }
        }

        private int _bronId;
        public int BronId
        {
            get { return _bronId; }
            set { SetValue(ref _bronId, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                SetValue(ref _date, value);
                DateEnd = Date.AddDays(Days - 1);
                Duration = GetDuration();
            } 
        }

        private string GetDuration()
        {
            if (Date.Date.Equals(DateEnd.Date))
                return Date.ToString("dd.MM.yy");
            
            return String.Format("{0} - {1}", Date.ToString("dd.MM.yy"), DateEnd.ToString("dd.MM.yy"));
        }

        private DateTime _dateEnd;
        public DateTime DateEnd
        {
            get { return _dateEnd; }
            set { SetValue(ref _dateEnd, value); }
        }

        private string _duration;
        public string Duration
        {
            get { return _duration; }
            set { SetValue(ref _duration, value); }
        }

        private int _day;
        public int Day
        {
            get { return _day; }
            set { SetValue(ref _day, value);} 
        }

        private int _daysNights;
        public int DaysNights
        {
            get { return _daysNights; }
            set 
            { 
                SetValue(ref _daysNights, value);
                if (_daysNights > 0)
                    DaysNightsString = _daysNights.ToString();
                else
                    DaysNightsString = "";
            }
        }

        private string _daysNightsString;
        public string DaysNightsString
        {
            get { return _daysNightsString; }
            set { SetValue(ref _daysNightsString, value); }
        }

        private int _days;
        public int Days
        {
            get { return _days; }
            set 
            { 
                SetValue(ref _days, value);
                if (_days > 1)
                    DaysNights = _days - 1;
                else
                    DaysNights = 0;
            } 
        }

        private string _serviceName;
        public string ServiceName
        {
            get { return _serviceName; }
            set
            {
                SetValue(ref _serviceName, CityCountryConverter.Convert(value));
                //ServiceNameNew = GetNewServiceName(_serviceName);
            } 
        }

        private ServiceGroup _group;
        public ServiceGroup Group
        {
            get { return _group; }
            set { SetValue(ref _group, value); }
        }

        public int TypeId { get; set; }

        private string GetNewServiceName(string str)
        {
            string str2 = CityCountryConverter.Convert(str);

            int b = str.IndexOf(":");
            int b2 = str.IndexOf("::");

            if (b < 0)
                return str;

            string serviceType = str.Substring(0, b);

            b += b2 > -1 ? 2 : 1;

            string[] parms = str.Substring(b).Split(_delimiters);

            if (serviceType.Equals("Авиаперелет ") && parms.Length >= 3)
            {
                string cls = parms[2].Replace("E Экономический класс", "Эконом класс");
                //string route = ((AviaService) this).BookingAvia.GetRoute();
                return string.Format("{0} {1} {2}", serviceType, parms[1], cls);
            }

            if (serviceType.Equals("Круиз") && parms.Length >= 2)
            {
                return string.Format("{0} {1} {2}", serviceType, parms[1], parms[2]);
            }
            
            return str2;
        }

        private string _serviceNameNew;
        public string ServiceNameNew
        {
            get { return _serviceNameNew; }
            set { SetValue(ref _serviceNameNew, value); }
        }

        private string _serviceType;
        public string ServiceType
        {
            get { return _serviceType; }
            set { SetValue(ref _serviceType, value); }
        }

        private decimal _netto;
        public decimal Netto
        {
            get { return _netto; }
            set
            {
                SetValue(ref _netto, value);
                //NettoString = _netto.ToString("F0");
                NettoString = _netto.ToString("# ###.");
            } 
        }

        private string _nettoString;
        public string NettoString
        {
            get { return _nettoString; }
            set { SetValue(ref _nettoString, value);} 
        }

        private string _nettoNewString;
        public string NettoNewString
        {
            get { return _nettoNewString; }
            set { SetValue(ref _nettoNewString, value); }
        }

        private decimal _brutto;
        public decimal Brutto
        {
            get { return _brutto; }
            set
            {
                SetValue(ref _brutto, value);
                //BruttoString = _brutto.ToString("F0");
                BruttoString = _brutto.ToString("# ###.");
            } 
        }

        private string _bruttoString;
        public string BruttoString
        {
            get { return _bruttoString; }
            set { SetValue(ref _bruttoString, value); } 
        }

        private string _bruttoNewString;
        public string BruttoNewString
        {
            get { return _bruttoNewString; }
            set { SetValue(ref _bruttoNewString, value); }
        }

        private decimal _percent;
        public decimal Percent
        {
            get { return _percent; }
            set
            {
                SetValue(ref _percent, value);
                if (_percent > 0)
                {
                    decimal r = Math.Round(_percent, 0);
                    decimal rr = Math.Round(_percent, 1);
                    if (r == rr)
                        PercentString = _percent.ToString("F0");
                    else
                        PercentString = _percent.ToString("F1");

                    //if (_percent < 10)
                    //    PercentString = _percent.ToString("F1"); // + "%";
                    //else
                    //    PercentString = _percent.ToString("F0"); // + "%";
                }
                else
                {
                    PercentString = "";
                }
            }
        }

        private string _percentString;
        public string PercentString
        {
            get { return _percentString; }
            set { SetValue(ref _percentString, value); }
        }

        private decimal _summa;
        public decimal Summa
        {
            get { return _summa; }
            set
            {
                SetValue(ref _summa, value);
                //SummaString = _summa.ToString("F0");
                SummaString = _summa.ToString("# ###.");
            }
        }

        private string _summaString;
        public string SummaString
        {
            get { return _summaString; }
            set { SetValue(ref _summaString, value); }
        }

        private bool PartnerPercentFlag = false;

        private string _status;
        public string Status
        {
            get { return _status; }
            set { SetValue(ref _status, value);} 
        }

        private int _statusValue;
        public int StatusValue
        {
            get { return _statusValue; }
            set { SetValue(ref _statusValue, value); }
        }

        private int _svKey;
        public int SvKey
        {
            get { return _svKey; }
            set { SetValue(ref _svKey, value); } 
        }

        private string _dlComment;
        public string DlComment
        {
            get { return _dlComment; }
            set { SetValue(ref _dlComment, value); } 
        }

        private int _partnerKey;
        public int PartnerKey
        {
            get { return _partnerKey; }
            set { SetValue(ref _partnerKey, value); } 
        }

        private int _keyForSerch;
        public int KeyForSerch
        {
            get { return _keyForSerch; }
            set { SetValue(ref _keyForSerch, value); }
        }

        private string _dateBeginString;
        public string DateBeginString
        {
            get { return _dateBeginString; }
            set { SetValue(ref _dateBeginString, value); }
        }

        private string _dateEndString;
        public string DateEndString
        {
            get { return _dateEndString; }
            set { SetValue(ref _dateEndString, value); }
        }

        public DiscountSetting DiscountSetting { get; set; }

        private StatusSetting _statusSetting;
        public StatusSetting StatusSetting
        {
            get { return _statusSetting; }
            set { SetValue(ref _statusSetting, value); }
        }

        private ServiceSetting _serviceSetting;
        public ServiceSetting ServiceSetting
        {
            get { return _serviceSetting; }
            set { SetValue(ref _serviceSetting, value); }
        }

        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set { SetValue(ref _discount, value); }
        }

        public List<Service> RelatedServices { get; set; }
        public Service Parent { get; set; }

        private bool _hideBorderClass;
        public bool HideBorderClass
        {
            get { return _hideBorderClass; }
            set { SetValue(ref _hideBorderClass, value); }
        }

        public bool _filterFlag;
        public bool FilterFlag
        {
            get { return _filterFlag; }
            set { SetValue(ref _filterFlag, value); }
        }

        public bool _hideInGroupFlag;
        public bool HideInGroupFlag
        {
            get { return _hideInGroupFlag; }
            set { SetValue(ref _hideInGroupFlag, value); }
        }

        public bool _editableFlag;
        public bool EditableFlag
        {
            get 
            {
                if (KeyForSerch == 7) return true;
                return _editableFlag; 
            }
            set { SetValue(ref _editableFlag, value); }
        }

        public bool _editFlag;
        public bool EditFlag
        {
            get { return _editFlag; }
            set { SetValue(ref _editFlag, value); }
        }

        public DataRow DataRow { get; set; }

        public bool PartnerFlag { get; set; }


        protected readonly IVoucherService VoucherService;

        public Service()
        {
            
        }

        public Service(Voucher voucher, DataRow row)
        {
            int index = ServiceName != null ? ServiceName.IndexOf(":") : -1;

            //Days = row.Field<Int16?>("DL_NDAYS") ?? 0;
            Days = row.Field<int?>("DL_NDAYS") ?? 0;
            BronId = row.Field<int?>("bronId") ?? -1;
            Brutto = row.Field<decimal?>("DL_BRUTTO") ?? 0;
            Date = row.Field<DateTime>("DL_TURDATE");
            Day = row.Field<Int16?>("DL_DAY") ?? 0;
            DlKey = row.Field<int>("DL_KEY");
            PartnerKey = row.Field<int>("DL_PARTNERKEY");
            DlComment = row.Field<string>("DL_Comment");
            Netto = row.Field<decimal?>("DL_COST") ?? 0;
            Number = row.Field<Int16>("DL_NMEN");
            ServiceName = row.Field<string>("DL_NAME");
            SvKey = row.Field<int>("DL_SVKEY");
            Status = row.Field<string>("CR_NAME");
            StatusValue = row.Field<int>("DL_CONTROL");
            ServiceType = index < 0 ? ServiceName : ServiceName.Remove(index);
            DataRow = row;
            Voucher = voucher;
            DgCode = voucher.DgCode;
            ServiceSetting = new ServiceSetting(DgCode, row);
            TypeId = row.Field<int>("typeId");
            Rate = voucher.VoucherSettingInfo.Rate;
            Discount = (decimal)(row.Field<double?>("DL_DISCOUNT") ?? 0);
            ServType = Voucher.GetServiceType(SvKey, TypeId);
            KeyForSerch = row.Field<int>("KEY_for_serch");

            Percent = 0;
            Summa = 0;
            //Percent = row.Field<decimal?>("PR_PERCENT") ?? 0;
            //Summa = row.Field<decimal?>("PR_SUMMA") ?? 0;

            bool bPercent = false;
            if (SvKey == 1 || SvKey == 2 || SvKey == 3 || SvKey == 4 || SvKey == 8 || SvKey == 9) bPercent = true;
            if (SvKey == 3143 || SvKey == 3149 || SvKey == 3171 || SvKey == 3172 || SvKey == 3177 || SvKey == 3180) bPercent = true;
            if (SvKey == 3184 || SvKey == 3187) bPercent = true;

            bool bPartner = false;
            if (Voucher != null)
                if (Voucher.AccessPartnerFlag) bPartner = true; 

            decimal full_cost = Brutto + Discount;
            if (bPartner && bPercent && full_cost > 0 && Netto > 0)
            {
                Summa = full_cost - Netto;
                Percent = Math.Round(Summa * (decimal)100.0 / full_cost, 2);
            }

            CalcPercent();

            //HideFields();
        }

        public Service(Service source)
        {
            VoucherService = Repository.GetInstance<IVoucherService>();

            if (source == null)
            {
                DateTime zdt = new DateTime(2000, 01, 01);
                
                ServType = 0; // ServiceType.Visa;
                ServiceNumber = 0;
                BeginClass = false;
                EndClass = false;
                MidClass = false;
                HideBorderClass = false;
                HideInGroupFlag = false;
                BronId = 0;
                Day = 0;
                Days = 0;
                ServiceName = "";
                ServiceType = "";
                PartnerKey = 0;
                Number = 0;
                Netto = 0;
                Brutto = 0;
                Percent = 0;
                Summa = 0;
                Status = "";
                StatusValue = 0;
                SvKey = 5;
                ColorIndex = 0;
                DlKey = 0;
                Date = zdt;     // В самом конце - обновляет DateEnd и Duration
                ServiceSetting = new ServiceSetting();
                StatusSetting = new StatusSetting();
                DiscountSetting = new DiscountSetting();
                DataRow = null;
                Voucher = null;
                DgCode = "";
                Group = null;
                TypeId = 0;
                Rate = "";
                Discount = 0;

                FullName = "";

                return;
            }

            ServType = source.ServType;
            ServiceNumber = source.ServiceNumber;
            BeginClass = source.BeginClass;
            EndClass = source.EndClass;
            MidClass = source.MidClass;
            HideBorderClass = source.HideBorderClass;
            HideInGroupFlag = source.HideInGroupFlag;
            BronId = source.BronId;
            Day = source.Day;
            Days = source.Days;
            ServiceName = source.ServiceName;
            ServiceType = source.ServiceType;
            PartnerKey = source.PartnerKey;
            Number = source.Number;
            Netto = source.Netto;
            Brutto = source.Brutto;
            Percent = source.Percent;
            Summa = source.Summa;
            Status = source.Status;
            StatusValue = source.StatusValue;
            SvKey = source.SvKey;
            ColorIndex = source.ColorIndex;
            DlKey = source.DlKey;
            Date = source.Date;     // В самом конце - обновляет DateEnd и Duration
            ServiceSetting = source.ServiceSetting;
            StatusSetting = source.StatusSetting;
            DiscountSetting = source.DiscountSetting;
            DataRow = source.DataRow;
            Voucher = source.Voucher;
            DgCode = source.DgCode;
            Group = source.Group;
            TypeId = source.TypeId;
            Rate = source.Rate;
            Discount = source.Discount;

            CalcPercent();
        }

        public void SetCurrency(Currency currency, decimal price)
        {
            NettoString = PriceConverter.GetStringValue(Netto, currency, price);
            BruttoString = PriceConverter.GetStringValue(Brutto, currency, price);
        }

        public virtual void GetInfo()
        {

        }

        public virtual void GetFullName(int number)
        {
            GetInfo();
            FullName = String.Format("{0}{1}  {2}-{3}", Name2, number, DateBeginString, DateEndString);
        }

        public override bool EqualData(CaruselData obj)
        {
            if (obj == null)
                return false;

            var service = obj as Service;

            if (service == null)
                return false;
            else
                return DlKey.Equals(service.DlKey);
        }

        //----------------------------------------------------------------------------------------------------
        public void CalcPercent()
        {
            decimal sum = 0;
            decimal perc = 0;
            if (Brutto > 0)
            {
                sum = Brutto - Netto;
                if (sum != 0) perc = (decimal)(Math.Round((double)sum * 10000.0 / (double)Brutto) / 100.0);
            }
            Summa = sum;
            Percent = perc;
        }

        //----------------------------------------------------------------------------------------------------
        public void HideFields()
        {
            NettoString = "";
            SummaString = "";
            PercentString = "";
        }

        //----------------------------------------------------------------------------------------------------
        public void SetPartnerFlag(bool bPartner)
        {
            PartnerFlag = bPartner;
            if (!PartnerFlag) HideFields();
        }

        //----------------------------------------------------------------------------------------------------

    }
}
