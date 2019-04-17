using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public class DopServiceItem
    {
        public int ID;
        public string Name;
        public string Query;
        public int IsDefault;
        public List<NameValue> TextList;

        public DopServiceItem(int iID, string iName, string iQuery, int iDefault)
        {
            ID = iID;
            Name = iName;
            Query = iQuery;
            IsDefault = iDefault;
            TextList = new List<NameValue>();
        }
    }
}
