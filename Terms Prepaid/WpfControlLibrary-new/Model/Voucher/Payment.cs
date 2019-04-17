using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Model.Voucher
{
    public class RowSetting : Data, ICloneable
    {
        private string _name;
        private string _dateChange;
        private string _manager;

        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        public string DateChange
        {
            get { return _dateChange; }
            set { SetValue(ref _dateChange, value); }
        }

        public string Manager
        {
            get { return _manager; }
            set { SetValue(ref _manager, value); }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class RowSettingValue : RowSetting
    {
        private decimal _value;
        private int _valueType;
        private decimal _cost;
        private decimal _numericValue;
        private decimal _procentValue;

        public decimal Value
        {
            get { return _value; }
            set
            {
                SetValue(ref _value, value);
                UpdateValues();
            }
        }

        public int ValueType
        {
            get { return _valueType; }
            set
            {
                SetValue(ref _valueType, value);
                UpdateValues();
            }
        }

        public decimal Cost
        {
            get { return _cost; }
            set
            {
                SetValue(ref _cost, value);
                UpdateValues();
            }
        }

        /*private decimal GetInvertProcent(decimal procent)
        {
            return (100 - procent) / 100;
        }

        private void GetProcentFromInvert(decimal invertProcent)
        {
            return 
        }*/

        private void UpdateValues()
        {
            //NumericValue = ValueType == 1 ? Cost / (100 - Value) : Value;
            //ProcentValue = ValueType == 1 ? Value : (Cost != 0 ? 100 * Value / Cost : 0);

            NumericValue = ValueType == 1 ? Cost * Value / 100 : Value;
            ProcentValue = ValueType == 1 ? Value : (Cost != 0 ? 100 * Value / Cost : 0);
        }

        public void SetValue(decimal cost, decimal numericValue)
        {
            Cost = cost;
            Value = ValueType == 1 ? (cost != 0 ? 100 * numericValue / cost : 0) : numericValue;
        }

        public decimal NumericValue
        {
            get { return _numericValue; }
            set { SetValue(ref _numericValue, value); }
        }

        public decimal ProcentValue
        {
            get { return _procentValue; }
            set { SetValue(ref _procentValue, value); }
        }
    }

    public class RowSettingDate : RowSetting
    {
        private DateTime? _dateValue;

        public DateTime? DateValue
        {
            get { return _dateValue; }
            set { SetValue(ref _dateValue, value);}
        }
    }

    public class RowSettingString : RowSetting
    {
        private string _value;

        public string Value
        {
            get { return _value; }
            set { SetValue(ref _value, value); }
        }
    }

    public class CommonSetting : Data, ICloneable
    {
        private string _rate;

        public string Rate
        {
            get { return _rate; }
            set { SetValue(ref _rate, value); }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class PaymentSetting : CommonSetting
    {
        private RowSettingDate _prePaymentDate;
        private RowSettingDate _paymentDate;
        private RowSettingValue _prePaymentValue;
        private RowSettingValue _paymentValue;

        public RowSettingDate PrePaymentDate
        {
            get { return _prePaymentDate; }
            set { SetValue(ref _prePaymentDate, value);}
        }

        public RowSettingDate PaymentDate
        {
            get { return _paymentDate; }
            set { SetValue(ref _paymentDate, value); }
        }

        public RowSettingValue PrePaymentValue
        {
            get { return _prePaymentValue; }
            set { SetValue(ref _prePaymentValue, value); }
        }

        public RowSettingValue PaymentValue
        {
            get { return _paymentValue; }
            set { SetValue(ref _paymentValue, value); }
        }
    }

    public class DiscountSetting : CommonSetting
    {
        private decimal _trueNumericValue;
        public decimal TrueNumericValue
        {
            get { return _trueNumericValue; }
            set { SetValue(ref _trueNumericValue, value); }
        }

        private RowSettingValue _discountValue;
        public RowSettingValue DiscountValue
        {
            get { return _discountValue; }
            set { SetValue(ref _discountValue, value); }
        }
    }

    public class StatusSetting : RowSetting
    {
        private int _statusValue;
        private string _statusName;

        public int StatusValue
        {
            get { return _statusValue; }
            set { SetValue(ref _statusValue, value);}
        }

        public string StatusName
        {
            get { return _statusName; }
            set { SetValue(ref _statusName, value); }
        }
    }

    public class ServiceSetting : Data, ICloneable
    {
        private PaymentSetting _payment;
        private DiscountSetting _discount;
        private StatusSetting _status;

        private ServiceSettingInfo _paymentData;
        private ServiceSettingInfo _ppaymentData;
        private ServiceSettingInfo _paymentValue;
        private ServiceSettingInfo _ppaymentValue;
        private ServiceSettingInfo _discountValue;
        private string _rate;

        private bool _nullValue;

        public int DlKey { get; set; }
        public string DgCode { get; set; }

        public string Rate
        {
            get { return _rate; }
            set { SetValue(ref _rate, value); }
        }

        public ServiceType Service { get; set; }

        public bool NullValue
        {
            get { return _nullValue; }
            set { SetValue(ref _nullValue, value); }
        }

        public PaymentSetting Payment
        {
            get { return _payment; }
            set { SetValue(ref _payment, value);}
        }

        public DiscountSetting Discount
        {
            get { return _discount; }
            set { SetValue(ref _discount, value); }
        }

        public StatusSetting Status
        {
            get { return _status; }
            set { SetValue(ref _status, value); }
        }

        public ServiceSettingInfo PaymentData
        {
            get { return _paymentData; }
            set { SetValue(ref _paymentData, value); }
        }

        public ServiceSettingInfo PPaymentData
        {
            get { return _ppaymentData; }
            set { SetValue(ref _ppaymentData, value); }
        }

        public ServiceSettingInfo PaymentValue
        {
            get { return _paymentValue; }
            set { SetValue(ref _paymentValue, value); }
        }

        public ServiceSettingInfo PPaymentValue
        {
            get { return _ppaymentValue; }
            set { SetValue(ref _ppaymentValue, value); }
        }

        public ServiceSettingInfo DiscountValue
        {
            get { return _discountValue; }
            set { SetValue(ref _discountValue, value); }
        }

        public ServiceSetting()
        {
            
        }

        public ServiceSetting(string dgCode, DataRow row)
        {
            //decimal cost = row.Field<decimal?>("DL_COST") ?? 0;
            decimal cost = row.Field<decimal?>("DL_BRUTTO") ?? 0;
            string handWho = row.Field<string>("HandWho");
            Rate = row.Field<string>("DG_Rate");

            NullValue = row.Field<decimal?>("PaymantValue") == null;

            Payment = new PaymentSetting
            {
                PaymentDate = new RowSettingDate()
                {
                    Name = "Оплата до",
                    DateValue = row.Field<DateTime?>("PaymantDate"),
                    DateChange = TextFormat.GetDate(row.Field<DateTime?>("paymentDateDate")),
                    Manager = row.Field<string>("paymentDateWho")
                },
                PrePaymentDate = new RowSettingDate()
                {
                    Name = "Предоплата до",
                    DateValue = row.Field<DateTime?>("PPaymentdaDate"),
                    DateChange = TextFormat.GetDate(row.Field<DateTime?>("ppaymentDateDate")),
                    Manager = row.Field<string>("ppaymentDateWho")
                },
                PaymentValue = new RowSettingValue()
                {
                    Name = "Сумма оплаты",
                    Value = cost,
                    ValueType = 0,
                    Cost = cost,
                    DateChange = "",
                    Manager = handWho
                },
                PrePaymentValue = new RowSettingValue()
                {
                    Name = "Сумма предоплаты",
                    Value = row.Field<decimal?>("PaymantValue") ?? 0,
                    ValueType = row.Field<int?>("PPaimentTipe") ?? 0,
                    Cost = cost,
                    DateChange = TextFormat.GetDate(row.Field<DateTime?>("ppaymentValueDate")),
                    Manager = row.Field<string>("ppaymentValueWho")
                }
            };

            Discount = new DiscountSetting
            {
                DiscountValue = new RowSettingValue()
                {
                    Name = "Скидка/комиссия",
                    Value = (decimal)(row.Field<float?>("Komission") ?? 0),
                    ValueType = row.Field<int?>("Tipe_of_Komission") ?? 0,
                    Cost = cost,
                    DateChange = TextFormat.GetDate(row.Field<DateTime?>("discountDate")),
                    Manager = row.Field<string>("discountWho")
                },
                TrueNumericValue = Convert.ToDecimal(row.Field<double?>("DL_DISCOUNT") ?? 0)
            };

            Status = new StatusSetting
            {
                Name = "Статус",
                DateChange = TextFormat.GetDate(row.Field<DateTime?>("statusDate")),
                Manager = row.Field<string>("statusWho"),
                StatusValue = row.Field<int>("DL_CONTROL"),
                StatusName = row.Field<string>("CR_NAME")
            };

            DlKey = row.Field<int>("DL_KEY");
            DgCode = dgCode;

            GetServiceSettingInfo();
        }

        public void GetServiceSettingInfo()
        {
            PaymentData = new ServiceSettingInfo("Оплата до", Payment.PaymentDate, Payment.PrePaymentDate.DateValue);
            PPaymentData = new ServiceSettingInfo("Предоплата до", Payment.PrePaymentDate);
            PaymentValue = new ServiceSettingInfo("Сумма оплаты", Payment.PaymentValue, Rate);
            PPaymentValue = new ServiceSettingInfo("Сумма предоплаты", Payment.PrePaymentValue, Rate);
            DiscountValue = new ServiceSettingInfo("Скидка/комиссия", Discount.DiscountValue, Rate);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
