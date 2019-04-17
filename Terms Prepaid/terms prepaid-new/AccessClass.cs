using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace terms_prepaid
{
    class AccessClass
    {
        public bool isRealize = false, isBronir = false, isSuperViser = false;

        public AccessClass(SqlConnection con)
        {
            int saleManager = 0, produktManeger = 0,director=0;
            using (SqlCommand com = new SqlCommand(@"select IS_ROLEMEMBER('avSalesManagers')", con))
            {
                saleManager = (int)com.ExecuteScalar();
            }
            using (SqlCommand com = new SqlCommand(@"select IS_ROLEMEMBER('avProductManagers')", con))
            {
                produktManeger = (int)com.ExecuteScalar();
            }
            using (SqlCommand com = new SqlCommand(@"select IS_ROLEMEMBER('mk_wp_SuperViser')", con))
            {
                director = (int)com.ExecuteScalar();
            }
            if (saleManager==1)
            {
                isRealize = true;
            }
            else
            {
                isRealize = false;
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
