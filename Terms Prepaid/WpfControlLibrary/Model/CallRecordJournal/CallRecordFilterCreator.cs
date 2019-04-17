using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.CallRecordJournal
{
    public class CallRecordFilterCreator
    {
        private readonly ICallRecordService _dbService;

        public CallRecordFilterCreator()
        {
            _dbService = Repository.GetInstance<ICallRecordService>();
        }

        public CallRecordFilter GetFilter()
        {
            return new CallRecordFilter
                (
                    new [] { new KeyValuePair<int,string>(-1, "Все") }
                    .Union(
                        _dbService.GetCallRecordStatuses()
                        .Select()
                        .Select(r => new KeyValuePair<int, string>(r.Field<int>("id"), r.Field<string>("name_ru")))
                    )
                );
        }

        public CallRecordFilter GetFilter(DateTime? dateBegin, DateTime? dateEnd, int? statusId)
        {
            var filter = GetFilter();
            
            filter.Set(dateBegin, dateEnd, statusId);
            
            return filter;
        }
    }
}
