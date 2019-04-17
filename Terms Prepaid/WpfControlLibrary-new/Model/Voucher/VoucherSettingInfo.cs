using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
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

        private ObservableCollection<NameValue> _info;
        private ObservableCollection<Transaction> _transactions;

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

        public VoucherSettingInfo(decimal paymentValue, decimal pPaymentValue, DateTime? paymentDate, DateTime? pPaymentDate, 
            decimal discount, string rate, int partnerKey, DataTable transactionTable)
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
