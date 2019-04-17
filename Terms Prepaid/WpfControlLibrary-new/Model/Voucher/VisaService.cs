using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class VisaService : Service
    {
        public static int Index = 0;

        private string _country;
        public string Country
        {
            get { return _country; }
            set { SetValue(ref _country, value); }
        }

        public VisaService(Service source)
            : base(source)
        {
            Index++;

            DataRow row = Repository.GetInstance<IVoucherService>().GetVisaInfo2(DlKey);

            if (row != null)
            {
                SlName = row.Field<string>("SL_NAME");
                Country = row.Field<string>("CN_NAME");
            }

            GetFullName(Index);
        }

        public sealed override void GetInfo()
        {
            Name2 = "Визовая услуга №";
            
            DateBeginString = DateTime.Now.ToString("dd.MM.yy");
            DateEndString = DateTime.Now.ToString("dd.MM.yy");
        }

        public sealed override void GetFullName(int number)
        {
            GetInfo();
            FullName = String.Format("№{0}  Визовая услуга - {1} - {2}", number, Country, SlName);
            //FullName = String.Format("{0}.{1}: {2} длительность {3} дней", number, SlName, Country, Days);
        }
    }
}
