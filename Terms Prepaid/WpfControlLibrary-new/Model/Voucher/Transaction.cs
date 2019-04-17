using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class Transaction : Data
    {
        private double _course;
        private DateTime _dateTransaction;
        private double _summTransaction;
        private String _summTransactionStr;
        
        public string DgCode { get; set; }
        public string RateTransaction { get; set; }
        public string RateDogovor { get; set; }
        public string WhoPayments { get; set; }
        public string PaymentsType { get; set; }
        public double SummDogovorRate { get; set; }
        public double SummNational { get; set; }
        public DateTime DateCource { get; set; }

        public double SummTransaction
        {
            get { return _summTransaction; }
            set
            {
                SetValue(ref _summTransaction, value);
            }
        }

        public double Course
        {
            get { return _course; }
            set { SetValue(ref _course, value); }
        }

        public DateTime DateTransaction
        {
            get { return _dateTransaction; }
            set { SetValue(ref _dateTransaction, value); }
        }

        public String SummTransactionStr
        {
            get { return _summTransactionStr; }
            set { SetValue(ref _summTransactionStr, value); }
        }

        public void UpdateSummTransactionStr()
        {
            SummTransactionStr = string.Format("{0} руб ({1} {2})", SummNational, SummDogovorRate, RateDogovor);
        }
        
        public Transaction(DataRow row)
        {
            DgCode = row.Field<string>("DGCode");
            SummDogovorRate = row.Field<double>("Summ_DogovorRate");
            SummNational = row.Field<double>("Summ_National");
            Course = row.Field<double>("cours");
            DateCource = row.Field<DateTime>("Date_Course");
            DateTransaction = row.Field<DateTime>("Date_Transaction");
            RateTransaction = row.Field<string>("Rate_Transaction");
            RateDogovor = row.Field<string>("Rate_Dogovor");
            WhoPayments = row.Field<string>("WhoPayments");
            PaymentsType = row.Field<string>("PaymentsType");
            SummTransaction = row.Field<double>("Summ_Transaction");

            UpdateSummTransactionStr();
        }
    }
}
