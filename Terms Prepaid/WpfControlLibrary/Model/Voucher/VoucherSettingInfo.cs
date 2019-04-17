using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Model.Voucher
{

    public class VoucherSettingInfo : Data
    {
        private DateTime? _paymentDate;
        private DateTime? _ppaymentDate;
        private decimal _paymentValue;
        private decimal _ppaymentValue;
        private decimal _discount;
        private string _rate;
        private decimal _course;

        private ObservableCollection<NameValue> _info;
        private ObservableCollection<Transaction> _transactions;


        private string _dgCode;
        public string DgCode
        {
            get { return _dgCode; }
            set { SetValue(ref _dgCode, value); }
        }

        public ObservableCollection<NameValue> Info
        {
            get { return _info; }
            set { SetValue(ref _info, value); }
        }

        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set { SetValue(ref _transactions, value); }
        }

        public DateTime? PaymentDate
        {
            get { return _paymentDate; }
            set { SetValue(ref _paymentDate, value); }
        }

        public DateTime? PPaymentDate
        {
            get { return _ppaymentDate; }
            set { SetValue(ref _ppaymentDate, value); }
        }

        public decimal PaymentValue
        {
            get { return _paymentValue; }
            set { SetValue(ref _paymentValue, value); }
        }

        public decimal PPaymentValue
        {
            get { return _ppaymentValue; }
            set { SetValue(ref _ppaymentValue, value); }
        }

        public decimal Discount
        {
            get { return _discount; }
            set { SetValue(ref _discount, value); }
        }

        public string Rate
        {
            get { return _rate; }
            set { SetValue(ref _rate, value); }
        }

        public decimal Course
        {
            get { return _course; }
            set { SetValue(ref _course, value); }
        }

        private int _clKey;
        public int ClKey
        {
            get { return _clKey; }
            set { SetValue(ref _clKey, value); }
        }

        private int _partnerKey;
        public int PartnerKey
        {
            get { return _partnerKey; }
            set { SetValue(ref _partnerKey, value); }
        }

        private int _lkUserId;
        public int LkUserId
        {
            get { return _lkUserId; }
            set { SetValue(ref _lkUserId, value); }
        }

        public VoucherSettingInfo(string dgCode)
        {
            DgCode = dgCode;
           
            var voucherService = Repository.GetInstance<IVoucherService>();

            DataRow row = voucherService.GetVoucherInfo(_dgCode);

            var discount = row.Field<decimal?>("DG_DISCOUNT") ?? 0;
            var discountSum = row.Field<decimal?>("DG_DISCOUNTSUM") ?? 0;
            var cost = row.Field<decimal>("DG_PRICE");

            DateTime? pDate = row.Field<DateTime?>("DG_PAYMENTDATE");
            DateTime? ppDate = row.Field<DateTime?>("DG_PPAYMENTDATE");

            var paymentDate = TextFormat.GetDate(pDate);
            var ppaymentDate = TextFormat.GetDate(ppDate);

            var ppaymentSum = row.Field<int>("DG_PROCENT") == 1 ?
                (row.Field<decimal?>("DG_PRICE") ?? 0) * (row.Field<decimal?>("DG_RazmerP") ?? 0) / 100 :
                (row.Field<decimal?>("DG_RazmerP") ?? 0);

            var rate = row.Field<string>("DG_RATE");

            PartnerKey = row.Field<int?>("DG_PARTNERKEY") ?? 0;
            ClKey = row.Field<int?>("DD_CLКеу") ?? 0;
            LkUserId = row.Field<int?>("LkUserId") ?? 0;

            Init(cost, ppaymentSum, pDate, ppDate, discountSum, rate, (int)PartnerKey, voucherService.GetTransactions(DgCode), voucherService.GetCourse2(rate));
        }

        private void Init(decimal paymentValue, decimal pPaymentValue, DateTime? paymentDate, DateTime? pPaymentDate, 
            decimal discount, string rate, int partnerKey, DataTable transactionTable, decimal course)
        {
            PaymentValue = paymentValue;
            PPaymentValue = pPaymentValue;
            PaymentDate = paymentDate;
            PPaymentDate = pPaymentDate;
            Discount = discount;
            Rate = rate;

            Info = new ObservableCollection<NameValue>();

            Info.Add(new NameValue("общая стоимость", TextFormat.GetCost(PaymentValue, Rate)));

            if (PaymentDate != null && PPaymentDate != null && ((DateTime)PaymentDate).Date.Equals(((DateTime)PPaymentDate).Date))
            {
                DateTime dt = (DateTime)(PPaymentDate < PaymentDate ? PPaymentDate : PaymentDate);
                Info.Add(new NameValue("дата оплаты", TextFormat.GetDate(dt)));

                 if (PaymentValue != PPaymentValue)
                     Info.Add(new NameValue("сумма предоплаты", TextFormat.GetCost(PPaymentValue, Rate)));
            }
            else
            {
                Info.Add(new NameValue("дата оплаты", TextFormat.GetDate(PaymentDate)));

                if (PaymentValue != PPaymentValue)
                    Info.Add(new NameValue("сумма предоплаты", TextFormat.GetCost(PPaymentValue, Rate)));

                Info.Add(new NameValue("дата предоплаты", TextFormat.GetDate(PPaymentDate)));
            }

            if (Discount != 0)
                Info.Add(new NameValue(partnerKey == 0 ? "скидка" : "комиссия", TextFormat.GetCost(Discount, Rate)));

            Transactions = new ObservableCollection<Transaction>(transactionTable.Select().Select(r => new Transaction(r)));
        }

    }
}
