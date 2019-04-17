using System;
using System.Data.Entity;

namespace DataService
{
    public class Repo<T> 
        where T : DbContext
    {
        private readonly DbContext _db;

        private static Repo<T> _instance;
        public static string ConnectionString { get; set; }

        private Repo()
        {
            _db = (ConnectionString != null ? 
                Activator.CreateInstance(typeof(T), ConnectionString) : 
                Activator.CreateInstance(typeof(T), null)) as T;
        }

        public static Repo<T> Instance()
        {
            return _instance ?? (_instance = new Repo<T>());
        }

        public static T GetDb()
        {
            return Instance()._db as T;
        }
    }
}