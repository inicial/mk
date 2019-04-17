using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface ICorrespondenceService
    {
        void InsertHistory2(string dgcode, string text, string mod, string remark);
        void CheckUnreadMessages(string dgCode);
        DataTable GetCorrespondence(string dgcode, string mod);
        DataTable GetUnreadMessages(string dgCode);
    }
}
