using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                DateEnd = Date.AddDays(Days);
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

        private int _days;
        public int Days
        {
            get { return _days; }
            set { SetValue(ref _days, value);} 
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
                NettoString = _netto.ToString("F0");
            } 
        }

        private string _nettoString;
        public string NettoString
        {
            get { return _nettoString; }
            set { SetValue(ref _nettoString, value);} 
        }
        
        private decimal _brutto;
        public decimal Brutto
        {
            get { return _brutto; }
            set
            {
                SetValue(ref _brutto, value);
                BruttoString = _brutto.ToString("F0");
            } 
        }

        private string _bruttoString;
        public string BruttoString
        {
            get { return _bruttoString; }
            set { SetValue(ref _bruttoString, value); } 
        }

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

        public DataRow DataRow { get; set; }

        public Service()
        {

        }

        public Service(Service source)
        {
            ServType = source.ServType;
            ServiceNumber = source.ServiceNumber;
            BeginClass = source.BeginClass;
            EndClass = source.EndClass;
            MidClass = source.MidClass;
            BronId = source.BronId;
            Day = source.Day;
            Days = source.Days;
            ServiceName = source.ServiceName;
            ServiceType = source.ServiceType;
            Number = source.Number;
            Netto = source.Netto;
            Brutto = source.Brutto;
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
    }
}
