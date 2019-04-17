using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Messages
{
    public class BindRequestsMessage
    {
        public int ParentId { get; set; }
        public int ChildId { get; set; }
    }
}
