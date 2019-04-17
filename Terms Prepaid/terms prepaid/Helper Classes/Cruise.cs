using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.Voucher;

namespace terms_prepaid
{
    public class users
    {
       public static users Parse(object obj)
        {
            return obj as users;
        }
        public int id;
        public string UserName;
        public users(int key, string name)
        {
            id = key;
            UserName = name;
        }
        public override string ToString()
        {
            return UserName;
        }
    }

    public class cruise_record
    {
        public int cruise_id;
        public string cruise_code;
        public string cruise_name;

        public cruise_record(int cruise, string code, string name)
        {
            cruise_id = cruise;
            cruise_code = code;
            cruise_name = name;
        }
    }

    public class ship_record
    {
        public int ship_id;
        public int cruise_id;
        public string ship_code;
        public string ship_name;

        public ship_record(int ship, int cruise, string code, string name)
        {
            ship_id = ship;
            cruise_id = cruise;
            ship_code = code;
            ship_name = name;
        }
    }

}
