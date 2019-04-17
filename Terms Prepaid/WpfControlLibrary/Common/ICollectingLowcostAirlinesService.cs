using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface ICollectingLowcostAirlinesService
    {
        DataTable GetCollectingLowcostAirlinesTable();
        DataTable GetCollectingLowcostAirlinesNotes();
    }
}
