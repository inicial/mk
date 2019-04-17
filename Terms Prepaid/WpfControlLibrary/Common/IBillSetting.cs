using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.Voucher;


namespace WpfControlLibrary.Common
{
    public interface IBillSetting
    {
        Service Service { get; }
        void Dispose();
        void SetVoucher(int serviceCount);
        void SetService(Service service);
        void SetBillSetting();
    }
}
