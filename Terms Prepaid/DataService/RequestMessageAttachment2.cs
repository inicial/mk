//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataService
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequestMessageAttachment2
    {
        public RequestMessageAttachment2()
        {
            this.RequestMessage = new HashSet<RequestMessage2>();
        }
    
        public int Id { get; set; }
        public string RequestMessageId { get; set; }
    
        public virtual ICollection<RequestMessage2> RequestMessage { get; set; }
    }
}
