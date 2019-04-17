using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Util
{
    public class SegmentNumberConverter : NumberConverter
    {
        public SegmentNumberConverter()
        {
            Postfix = " сегмент";

            PostFunc = (s) => s.Equals("1 сегмент") ? "" : s;
        }
    }
}
