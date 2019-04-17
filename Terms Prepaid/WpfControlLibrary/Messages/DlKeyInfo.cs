using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Messages
{
    public class DlKeyInfo
    {
        public int DlKey { get; set; }

        public DlKeyInfo(int dlKey)
        {
            DlKey = dlKey;
        }
    }
}
