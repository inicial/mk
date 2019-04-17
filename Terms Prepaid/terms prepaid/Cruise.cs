using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    class dogovorlistline
    {
        public int dlkey;
        public string dl_name;
        public static dogovorlistline Parse(object obj)
        {
            return obj as dogovorlistline;
        }
        public dogovorlistline(int key,string name)
        {
            dlkey = key;
            dl_name = name;
        }
        public override string ToString()
        {
            return dl_name;
        }
    }
}
