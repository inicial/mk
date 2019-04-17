using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface ICallRecordService
    {
        DataTable GetCallRecords(DateTime? dateBegin, DateTime? dateEnd, int? status);
        DataTable GetCallRecordStatuses();
    }
}
