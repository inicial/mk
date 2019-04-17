using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.CallRecordJournal
{
    public class CallRecordCreator
    {
        private readonly ICallRecordService _dbService;

        public CallRecordCreator()
        {
            _dbService = Repository.GetInstance<ICallRecordService>();
        }

        public ICallRecord[] GetRecords(CallRecordFilter filter)
        {
            return _dbService.GetCallRecords(filter.DateBegin, filter.DateEnd, filter.Status.Value != null && filter.Status.Key != -1 ? filter.Status.Key : (int?)null)
                .Select()
                .Select(GetRecord)
                .ToArray();
        }

        private ICallRecord GetRecord(DataRow row)
        {
            return new CallRecord()
            {
                Id = row.Field<int>("ID"),
                Phone = row.Field<string>("Phone"),
                Date = row.Field<DateTime?>("date_create"),
                CallTime = row.Field<DateTime?>("Time_for_ring"),
                ClientIp = row.Field<string>("Ip_client"),
                RingUrl2 = row.Field<string>("url_ring"),
                StatusId = row.Field<int?>("status"),
                StatusName = row.Field<string>("name_ru"),
                RingUrl = row.Field<string>("url_from_ring"),
                Duration = row.Field<int?>("duration_ring"),
                ManageId = row.Field<int?>("manager")
            };
        }
    }
}
