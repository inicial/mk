using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface IAccessService
    {
        bool isRealize { get; set; }
        bool isBronir { get; set; }
        bool isSuperViser { get; set; }
        bool isBron { get; set; }
        bool isAviaBron { get; set; }
    }

    public interface IUsersService
    {
        int GetUserID2();
        string GetUserName2();
        DataRow GetUserSignature(int usKey);
        DataTable GetManagerList();
    }
}
