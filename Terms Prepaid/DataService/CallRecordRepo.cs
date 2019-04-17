using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DataService
{
    public interface ICallRecordBase
    {
        int Id { get; set; }
        string Phone { get; set; }
    }

    public partial class mk_CallRecord : ICallRecordBase
    {
        public mk_CallRecordStatus Status { get; set; }
    }

    public partial class CallRecordsContext : DbContext
    {
        public CallRecordsContext(string connectionString)
            : base(connectionString)
        {

        }
    }

    public class CallRecordRepo
    {
        private readonly CallRecordsContext _db;

        private static CallRecordRepo _instance;
        public static string ConnectionString { get; set; }

        private CallRecordRepo()
        {
            _db = ConnectionString != null ? new CallRecordsContext(ConnectionString) : new CallRecordsContext();
        }

        public static CallRecordRepo Instance()
        {
            return _instance ?? (_instance = new CallRecordRepo());
        }

        public static CallRecordsContext GetDb()
        {
            return Instance()._db;
        }
    }
}
