using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlLibrary.Model.CallRecordJournal;
using WpfControlLibrary.View;

namespace TermsPrepairTests
{
    public static class InfoHelper
    {
        public static void GetInfo(CallRecord[] records)
        {
            foreach (var callRecord in records)
            {
                var sb = new StringBuilder();
                sb.Append("Id: ");
                sb.Append(callRecord.Id.ToString());
                sb.Append(" CallTime: ");
                sb.Append((callRecord.CallTime ?? DateTime.MinValue).ToString(CultureInfo.InvariantCulture));
                sb.Append(" ClientIp: ");
                sb.Append(callRecord.ClientIp.ToString());
                sb.Append(" Date: ");
                sb.Append((callRecord.Date ?? DateTime.MinValue).ToString(CultureInfo.InvariantCulture));
                sb.Append(" Duration: ");
                sb.Append(callRecord.Duration);
                sb.Append(" ManageId: ");
                sb.Append(callRecord.ManageId.ToString());
                sb.Append(" Phone: ");
                sb.Append(callRecord.Phone);
                sb.Append(" RingUrl: ");
                sb.Append(callRecord.RingUrl);
                sb.Append(" RingUrl2: ");
                sb.Append(callRecord.RingUrl2);
                sb.Append(" StatusId: ");
                sb.Append(callRecord.StatusId.ToString());
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
