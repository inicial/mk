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
    
    public partial class mk_CallRecord
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public System.DateTime Date { get; set; }
        public string Name { get; set; }
        public string PlaceOnline { get; set; }
        public int StatusId { get; set; }
    
        public virtual mk_CallRecordStatus mk_CallRecordsStatuses { get; set; }
    }
}
