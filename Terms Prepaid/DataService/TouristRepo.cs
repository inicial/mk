using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService
{
    public class TouristRepo
    {
        private readonly TouristDataDataContext _db;

        private static TouristRepo _instance;
        public static string ConnectionString { get; set; }

        private TouristRepo()
        {
            _db = ConnectionString != null ? new TouristDataDataContext(ConnectionString) : new TouristDataDataContext();
        }

        public static TouristRepo Instance()
        {
            return _instance ?? (_instance = new TouristRepo());
        }

        public static TouristDataDataContext GetDb()
        {
            return Instance()._db;
        }
    }
}
