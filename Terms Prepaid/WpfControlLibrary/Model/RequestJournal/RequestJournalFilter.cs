using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestJournalFilter : FilterBase
    {
        public string SenderAddress { get; set; }
        public bool SenderAddressContains { get; set; }
        public int? UsKey { get; set; }
        public int[] Problems { get; set; }
        public int[] Statuses { get; set; }
        public int[] SubStatuses { get; set; }

        public RequestJournalFilter(DateTime? dateBegin, DateTime? dateEnd, string senderAddress, int? usKey, int[] problems, int[] statuses, int[] subStatuses, bool senderAddressContains)
            : base(dateBegin, dateEnd)
        {
            Problems = problems;
            SenderAddress = senderAddress;
            UsKey = usKey;
            Statuses = statuses;
            SubStatuses = subStatuses;
            SenderAddressContains = senderAddressContains;
        }
    }
}
