using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Model.Voucher
{
    public class VaucherServicePayment
    {
        public int DlKey { get; set; }

        public DateTime? PaymentDate { get; set; }
        public DateTime? PPaymentDate { get; set; }

        public decimal Cost { get; set; }

        public decimal Payment { get; set; }
        public int PpType { get; set; }

        public decimal Comission { get; set; }
        public int ComissionType { get; set; }

        public string HandWho { get; set; }

        public decimal ComissionValue
        {
            get { return ComissionType == 1 ? Cost * Comission / 100 : Comission; }
        }

        public decimal ComissionProcent
        {
            get { return ComissionType == 1 ? Comission : 100 * Comission / Cost; }
        }

        public decimal PaymentValue
        {
            get { return PpType == 1 ? Cost * Payment / 100 : Payment; }
        }

        public decimal PaymentProcent
        {
            get { return PpType == 1 ? Payment : 100 * Payment / Cost; }
        }
    }
}
