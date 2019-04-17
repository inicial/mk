using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.Messages
{
    public abstract class VoucherMessage
    {
        public string DgCode { get; protected set; }

        protected VoucherMessage(string dgCode)
        {
            DgCode = dgCode;
        }
    }

    public class SetVoucherMessage : VoucherMessage
    {
        public SetVoucherMessage(string dgCode) 
            : base(dgCode)
        {
            
        }
    }

    public class SetServiceMessage : VoucherMessage
    {
        public Service Service { get; set; }

        public SetServiceMessage(string dgCode, Service service)
            : base(dgCode)
        {
            Service = service;
        }
    }
}
