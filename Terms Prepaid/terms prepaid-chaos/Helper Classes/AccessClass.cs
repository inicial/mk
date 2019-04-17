using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace terms_prepaid
{
    public class AccessClass : IAccessService
    {
        public bool isRealize { get; set; }
        public bool isBronir { get; set; }
        public bool isSuperViser { get; set; }
        public bool isBron { get; set; }
        public bool isAviaBron { get; set; }

        public AccessClass(SqlConnection con)
        {
            int saleManager;
            int produktManeger;
            int director;
            int bron;
            int aviaBron;

            using (SqlCommand com = new SqlCommand(@"select isnull(IS_ROLEMEMBER('avSalesManagers'),0)", con))
                saleManager = (int)com.ExecuteScalar();

            using (SqlCommand com = new SqlCommand(@"select isnull(IS_ROLEMEMBER('avProductManagers'),0)", con))
                produktManeger = (int)com.ExecuteScalar();

            using (SqlCommand com = new SqlCommand(@"select isnull(IS_ROLEMEMBER('mk_wp_SuperViser'),0)", con))
                director = (int)com.ExecuteScalar();

            using (SqlCommand com = new SqlCommand(@"select isnull(IS_ROLEMEMBER('mk_wp_bron'),0)", con))
                bron = (int)com.ExecuteScalar();

            using (SqlCommand com = new SqlCommand(@"select isnull(IS_ROLEMEMBER('avAviaBron'),0)", con))
                aviaBron = (int)com.ExecuteScalar();

            isRealize = saleManager == 1;
            isBron = bron == 1;
            isAviaBron = aviaBron == 1;
            isBronir = produktManeger == 1;
            isSuperViser = director == 1;
           
        }
    }
}
