using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService;
using terms_prepaid.Helpers;
using WpfControlLibrary.Common;

namespace TermsPrepairTests
{
    /*public class DbAdapter
    {
        private static readonly Lazy<DbAdapter> _instance = 
            new Lazy<DbAdapter>(() => new DbAdapter()); 

        private DbAdapter()
        {
            WorkWithData.InitConnection("s_schekotkov", "wbrnekazzak");
        }

        public static DbAdapter Instance
        {
            get { return _instance.Value; }
        }
    }*/

    public class DbAdapter
    {
        private static readonly Lazy<DbAdapter> _instance =
            new Lazy<DbAdapter>(() => new DbAdapter());

        private DbAdapter()
        {
            WorkWithData.InitConnection("s_schekotkov", "wbrnekazzak");
            Repository.Register<ICallRecordService>(WorkWithData.GetInstance);
        }

        public static DbAdapter Init()
        {
            return _instance.Value;
        }
    }
}
