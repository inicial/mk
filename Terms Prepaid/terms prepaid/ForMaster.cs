using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Rep10027.Helpers;
using terms_prepaid.WebGetInfo;

namespace terms_prepaid
{
    static class ForMaster
    {
        static public bool changeCabin(int dl_key,string dg_code)
        {
            WebGetInfo.Service1Soap webInfo = new Service1SoapClient();
            //webInfo.WebCreateCost();
            //webInfo.WebCreateCost();
            webInfo.WebRecalculateDogovor(dg_code);
            return true;
        }
        
        static public bool changePrice(double newPriceBrutto, double newPriceNetto, int dl_key,string dg_code)
        {
            //изменение цены по услуге
            string updstr = @"DECLARE @svkey int  
                              DECLARE @code  int  
                              DECLARE @subcode1 int  
                              DECLARE @subcode2  int  
                              DECLARE @trkey  int  
                              DECLARE @paketkey  int
                              DECLARE @date datetime  
                              declare @partner
                              select @paketkey=DL_PAKETKEY,
                              @trkey= DL_TRKEY,
                              @svkey= DL_SVKEY,
                              @code= DL_CODE,
                              @subcode1= DL_SUBCODE1,                                 
                              @date =DL_TURDATE + DL_DAY -1,
                              @subcode2=DL_SUBCODE2,
                              @partner=dl_partnerkey

                              from tbl_DogovorList where DL_KEY = @dlkey

                                UPDATE dbo.tbl_Costs 
                                SET CS_COST = @cost 
                                ,CS_COSTNETTO =@netto 
                                WHERE CS_SVKEY = @svkey 
                                AND CS_CODE = @code 
                                AND CS_SUBCODE1 = @subcode1 
                                AND CS_SUBCODE2 =@subcode2 
                                AND CS_PKKEY =@paketkey 
                                and cs_prkey=@partner
                                AND @date BETWEEN isnull(cs_DateSellBeg,@date) AND isnull(cs_DateSellEnd,@date+1) ";
            using (SqlCommand com = new SqlCommand(updstr,WorkWithData.Connection))
            {

                com.Parameters.AddWithValue("@cost", newPriceBrutto);
                com.Parameters.AddWithValue("@netto", newPriceNetto);
                com.Parameters.AddWithValue("@dlkey", dl_key);
                com.ExecuteNonQuery();
            }
            //пересчет путевки
            WebGetInfo.Service1Soap webInfo = new Service1SoapClient();
            webInfo.WebRecalculateDogovor(dg_code);
            return true;
        }
    }
}
