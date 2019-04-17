using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public class FilterBase
    {
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }

        public FilterBase(DateTime? dateBegin, DateTime? dateEnd)
        {
            DateBegin = dateBegin;
            DateEnd = dateEnd;
        }
    }
}
