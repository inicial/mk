﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CallRecordsContext : DbContext
    {
        public CallRecordsContext()
            : base("name=CallRecordsContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<mk_CallRecord> mk_CallRecords { get; set; }
        public DbSet<mk_CallRecordStatus> mk_CallRecordStatuses { get; set; }
        public DbSet<mk_CallRecordsStatusFilter> mk_CallRecordsStatusFilters { get; set; }
    }
}
