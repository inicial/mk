using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface IMilesAirlineListViewModel
    {
        string SkyTeamText { get; set; }
        string OneWorldText { get; set; }
        string StarAllianceText { get; set; }
    }
}
