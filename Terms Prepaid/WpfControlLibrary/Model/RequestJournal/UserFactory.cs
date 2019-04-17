using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    public interface IUserFactory
    {
        User GetUser(DataRow row);
    }

    public class UserFactory : IUserFactory
    {
        public User GetUser(DataRow row)
        {
            return new User(
                    DataRowExtensions.Field<int>(row, "us_key"), 
                    DataRowExtensions.Field<string>(row, "US_FullNameLat"),
                    DataRowExtensions.Field<string>(row, "US_MAILBOX"), 
                    DataRowExtensions.Field<int?>(row, "Phone"),
                    DataRowExtensions.Field<string>(row, "US_NAME"),
                    DataRowExtensions.Field<string>(row, "US_FNAME"),
                    DataRowExtensions.Field<string>(row, "US_SNAME"),
                    DataRowExtensions.Field<string>(row, "US_JOB")
           );
        }
    }

    public class UserFactoryTest : IUserFactory
    {
        public User GetUser(DataRow row)
        {
            return new User(
                    DataRowExtensions.Field<int>(row, "us_key"),
                    DataRowExtensions.Field<string>(row, "US_FullNameLat"),
                    DataRowExtensions.Field<string>(row, "US_MAILBOX"),
                    DataRowExtensions.Field<int?>(row, "Phone"),
                    DataRowExtensions.Field<string>(row, "US_NAME"),
                    DataRowExtensions.Field<string>(row, "US_FNAME"),
                    "",
                    "Менеджер"
           );
        }
    }
}
