using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface ICollectingLowcostAirlinesViewModel
    {
        DataTable Table { get; set; }
        DataTable Notes { get; set; }
    }
}
