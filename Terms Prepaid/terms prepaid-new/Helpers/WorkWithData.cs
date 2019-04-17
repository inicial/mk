using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using AccountsLib.HelperClasses;
using lanta.SQLConfig;
namespace Rep10027.Helpers
{
    public class WorkWithData
    {
        private static SqlConnection _connection;
        private static List<string> _waybills;
        private static AccountsLibConfiguration _accountsConfiguration = new AccountsLibConfiguration();
        public static bool InitConnection(string user,string pass)
        {
            bool state;
#if DEBUG
            _connection = LantaSQLConnection.Open_LantaSQLConnection("test", user,pass,out state);
#else
             _connection = LantaSQLConnection.Open_LantaSQLConnection("mk", user,pass,out state);
#endif
            return state;
        }
        
        static public SqlConnection Connection
        {
            get
            {
                
                if (_connection == null)
                    throw new Exception("Connection must be initialized");
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }

        public static List<string> Waybills
        {
            get
            {
                if (_waybills != null) return _waybills;
                using (var adapter = new SqlDataAdapter(@"select DG_CODE from Dogovor", Connection))
                {
                    var dt = new DataTable("Waybills");
                    adapter.Fill(dt);
                    _waybills = new List<string>(from DataRow r in dt.Rows select r[0].ToString());
                }
                return _waybills;
            }
        }

        public static  string[] Currencies
        {
            get
            {
                return _accountsConfiguration.Currencies;
            }
        }
    }
}