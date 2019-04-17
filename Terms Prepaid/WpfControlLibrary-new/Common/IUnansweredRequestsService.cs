using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface IUnansweredRequestsService
    {
        int GetUnansweredRequestsCount(string mod);
        int GetUnansweredRequestsCount(string mod, int usKey);
    }
}
