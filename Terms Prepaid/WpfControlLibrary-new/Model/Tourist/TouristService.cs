using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WpfControlLibrary.Model.Tourist
{
    public static class TouristService
    {

        public static ObservableCollection<Tourist> GetTouristList(DataTable touristTable)
        {
            ObservableCollection<Tourist> touristList = new ObservableCollection<Tourist>();

            foreach (DataRow row in touristTable.Rows)
            {
                touristList.Add(GetTourist(row));
            }

            return touristList;
        }

        public static Tourist GetTourist(DataRow row)
        {
            string birtdayStr = row.Field<string>("TU_BIRTHDAY");

            DateTime? birtday;

            try
            {
                birtday = DateTime.ParseExact(birtdayStr, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                birtday = null;
            }

            return new Tourist()
                {
                    Key = row.Field<int>("TU_KEY"),
                    Birtday = birtday,
                    FirstName = row.Field<string>("TU_FNAMELAT"),
                    SecondName = row.Field<string>("TU_NAMELAT"),
                    MiddleName = row.Field<string>("TU_SNAMELAT")
                };
        }
    }
}
