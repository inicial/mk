using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataService;

namespace WpfControlLibrary.Model.Voucher
{
    public class HotelService : ServiceWithTouristData
    {
        public static int Index = 0;

        public HotelService(Service source)
            : base(source)
        {
            Index++;

            GetFullName(Index);
        }

        /*public sealed override void GetInfo()
        {
            Name2 = "Отель №";

            DateBeginString = DateTime.Now.ToString("dd.MM.yy");
            DateEndString = DateTime.Now.ToString("dd.MM.yy");
        }*/

        private string GetStringWithSuffix(string main, int number)
        {
            var sb = new StringBuilder();
            sb.Append(number.ToString());
            sb.Append(" ");
            sb.Append(main);
            switch (number)
            {
                case 1:
                    sb.Append("ь");
                    break;

                case 2:
                case 3:
                case 4:
                    sb.Append("и");
                    break;

                default:
                    sb.Append("ей");
                    break;
            }
            return sb.ToString();
        }

        public sealed override void GetFullName(int number)
        {
            var db = Repository.GetInstance<TouristDataDataContext>();
            dynamic obj = db.mk_hotels_by_dlKey(DlKey).First();
            FullName = String.Format("№{0}  Отель - {1} / {2} - {3}, {4}", number, obj.country, obj.name, obj.stars, GetStringWithSuffix("ноч", obj.night));
        }
    }
}
