using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace terms_prepaid
{
    class AccessClass
    {
        public bool isRealize = false, isBronir = false, isSuperViser = false, isBron = false;

        public AccessClass(SqlConnection con)
        {
            int saleManager = 0, produktManeger = 0,director=0,bron;
            using (SqlCommand com = new SqlCommand(@"select isnull(IS_ROLEMEMBER('avSalesManagers'),0)", con))
            {
                saleManager = (int)com.ExecuteScalar();
            }
            using (SqlCommand com = new SqlCommand(@"select isnull(IS_ROLEMEMBER('avProductManagers'),0)", con))
            {
                produktManeger = (int)com.ExecuteScalar();
            }
            using (SqlCommand com = new SqlCommand(@"select isnull(IS_ROLEMEMBER('mk_wp_SuperViser'),0)", con))
            {
                director = (int)com.ExecuteScalar();
            }
            using (SqlCommand com = new SqlCommand(@"select isnull(IS_ROLEMEMBER('mk_wp_bron'),0)", con))
            {
                bron = (int)com.ExecuteScalar();
            }
            if (saleManager==1)
            {
                isRealize = true;
            }
            else
            {
                isRealize = false;
            }
            if (bron == 1)
            {
                isBron = true;
            }
            else
            {
                isBron = false;
            }
            if (produktManeger == 1)
            {
                isBronir = true;
            }
            else
            {
                isBronir = false;
            }
            if (director==1)
            {
                isSuperViser = true;
            }
            else
            {
                isSuperViser = false;
            }
           
        }
    }
}
