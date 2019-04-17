using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Model.Voucher
{
    public class OtherService : ServiceWithTouristData
    {
        public static int Index = 0;

        public OtherService(Service source) : base(source)
        {
            Index++;
            GetFullName(Index);
        }

        public sealed override void GetFullName(int number)
        {
            FullName = String.Format("№{0} {1}", number, ServiceName);
        }
    }
}
