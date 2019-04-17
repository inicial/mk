using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface IAviaCardsService
    {
        DataTable GetSkyTeam();
        DataTable GetOneWorld();
        DataTable GetStarAliance();
    }
}
