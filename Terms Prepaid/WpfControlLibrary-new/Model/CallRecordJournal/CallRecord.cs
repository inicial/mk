using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataService;

namespace WpfControlLibrary.Model.CallRecordJournal
{ 
    public interface ICallRecord : ICallRecordBase
    {
        DateTime? Date { get; set; }
        DateTime? CallTime { get; set; }
        string ClientIp { get; set; }
        string RingUrl2 { get; set; }
        int? StatusId { get; set; }
        string StatusName { get; set; }
        string RingUrl { get; set; }
        int? Duration { get; set; }
        int? ManageId { get; set; }
    }

    public class CallRecord : ICallRecord
    {
        //[ID]
        public int Id { get; set; }
        
        //[Phone]
        public string Phone { get; set; }

        //[date_create]
        public DateTime? Date { get; set; }
        
        //[Time_for_ring]
        public DateTime? CallTime { get; set; }

        //[Ip_client]
        public string ClientIp { get; set; }

        //[url_ring]
        public string RingUrl2 { get; set; }

        //[status]
        public int? StatusId { get; set; }

        //name_en
        public string StatusName { get; set; }

        //[url_from_ring]
        public string RingUrl { get; set; }

        //[duration_ring]
        public int? Duration { get; set; }

        //[manager]
        public int? ManageId { get; set; }
    }
}
