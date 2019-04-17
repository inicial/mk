using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.Voucher;

namespace terms_prepaid
{
    class users
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

}
