using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using lanta.SQLConfig;
using terms_prepaid;

namespace Rep10027.Helpers
{
    public class WorkWithData
    {

        public  static DataTable GetFiltersData()
        {
            DataTable dt = new DataTable();
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter(@"select  [key_filter] ,[name],[group_filter] from [mk_filters_setting] where visible=1 order by group_filter,key_filter  ",
                                       Connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }
        public static string GetUserName()
        {
            string user = "";
            using (SqlCommand com = new SqlCommand(@"(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())", Connection))
            {
               user= (string)com.ExecuteScalar();
            }
            return user;
        }
        public static int GetUserID()
        {
            int id = 0;
            using (SqlCommand com = new SqlCommand(@"select top 1 isnull(US_KEY,0 ) from UserList where US_USERID = SUSER_SNAME()", Connection))
            {
                id = (int) com.ExecuteScalar();
            }
            return id;
        }

        public const string selectTurists = @"
            --declare @dgcode varchar(10)
            --set @dgcode='COS50102A2' 
            SELECT 
                 [TU_KEY]
                ,[TU_NAMERUS]
                ,[TU_NAMELAT]
                ,[TU_FNAMERUS]
                ,[TU_FNAMELAT]
                ,[TU_SNAMERUS]
                ,isnull(convert(varchar(10),[TU_BIRTHDAY],104),'') as TU_BIRTHDAY
                ,isNull(TU_SNAMELAT,'') as TU_SNAMELAT
                ,isnull([TU_PASPORTTYPE],'') as TU_PASPORTTYPE
                ,isNull([TU_PASPORTNUM],'') as TU_PASPORTNUM
                ,isnull(convert(varchar(10),[TU_PASPORTDATE],104),'') as TU_PASPORTDATE
                ,isnull(convert(varchar(10),[TU_PASPORTDATEEND],104),'') as TU_PASPORTDATEEND
                ,[TU_PASPORTBYWHOM]
                ,isnull([TU_PASPRUSER],'')as TU_PASPRUSER
                ,isnull([TU_PASPRUNUM],'')as TU_PASPRUNUM
                ,isnull(convert(varchar(10),[TU_PASPRUDATE],104),'') as TU_PASPRUDATE
                ,[TU_PASPRUBYWHOM]
             FROM [dbo].[tbl_Turist]
             where tu_dgcod=@dgcode";
        private const string selAction = @"SELECT [DL_key]
      ,[actions_id]
      ,[Text]
      ,[isBonus]
  FROM [dbo].[mk_actions_options]
  where actions_id >0 and  DL_key =@dlkey and  isBonus= @bon";
        static public string GenerateBlock1ForCruise(int dlKey)
        {
            DataTable _turists = new DataTable(), _servises = new DataTable(), _servisescrin = new DataTable(), _cruiseinfo = new DataTable();
            String str = string.Empty;
            int nmen = 0;


           
          
           //Загрузка сервисов
           string servises = string.Empty;
           SqlCommand serviseCommand = new SqlCommand(@"SELECT [DL_key]
      ,[actions_id]
      ,[Text]
      ,CDP_NAME
      ,CDP_ORDER
      ,CDP_BLOCK
  FROM [dbo].[mk_actions_options] 
  inner join dbo.MK_Cruise_DOPServise on actions_id=CDS_ID
  where actions_id <0 and
  DL_key = @dlkey and
  CDP_BLOCK = 2 
  order by CDP_ORDER  ", Connection);
           serviseCommand.Parameters.AddWithValue("@dlkey", dlKey);
           SqlDataAdapter serviceAdapter = new SqlDataAdapter(serviseCommand);

           serviceAdapter.Fill(_servises);
           servises = "\n\nСервис";
           foreach (DataRow row in _servises.Rows)
           {
               servises += "\n" + row.Field<string>("CDP_NAME") + ": " + row.Field<string>("Text");
           }
           //Загрузка туристов

           string turists = string.Empty;
            
           SqlCommand selTurist = new SqlCommand(@"SELECT 
	        TU_NAMELAT
	        ,TU_FNAMELAT
	        ,TU_BIRTHDAY
	        ,TU_BIRTHCITY
            ,TU_BIRTHCOUNTRY
            ,TU_PASPORTTYPE
            ,TU_PASPORTNUM
            ,TU_PASPORTDATE
            ,TU_PASPORTDATEEND
            ,TU_PASPORTBYWHOM
            ,TU_CITIZEN
            FROM [dbo].[TuristService] 
            inner join dbo.tbl_Turist on TU_KEY = tu_tukey
            where tu_dlkey=@dlkey", Connection);
           selTurist.Parameters.AddWithValue("@dlkey", dlKey);
           SqlDataAdapter turistadapter = new SqlDataAdapter(selTurist);
           turistadapter.Fill(_turists);
 
           int i = 0;
           foreach (DataRow row in _turists.Rows)
            {
                ++i;
                string turist = string.Empty;
                turist += "\n\n" + "Турист " + i.ToString();
                turist += Convert.ToChar(13) + "фамилия: " + row.Field<string>("TU_NAMELAT");
                turist += Convert.ToChar(13) + "имя: " + row.Field<string>("TU_FNAMELAT");
                DateTime birrthday = row.Field<DateTime>("TU_BIRTHDAY");
                
                turist += Convert.ToChar(13) + "дата: " + birrthday.Date.ToString("dd.MM") + " год рождения " + birrthday.Date.Year.ToString();
                if ((row.Field<string>("TU_CITIZEN") != string.Empty) && (row.Field<string>("TU_CITIZEN") != null))
                {
                    turist += Convert.ToChar(13) + "национальность: " + row.Field<string>("TU_CITIZEN");
                }
                if ((row.Field<string>("TU_BIRTHCITY") != string.Empty) && (row.Field<string>("TU_BIRTHCITY") != null))
                {
                    turist += Convert.ToChar(13) + "место рождения: " + row.Field<string>("TU_BIRTHCITY") + "(" + row.Field<string>("TU_BIRTHCOUNTRY") + ")";
                }
                
                turist += Convert.ToChar(13) + "загранпаспорт: " + row.Field<string>("TU_PASPORTTYPE") + "№" + row.Field<string>("TU_PASPORTNUM");
                if (row.Field<DateTime?>("TU_PASPORTDATE")!=null)
                {
                    turist += Convert.ToChar(13) + "дата выдачи з\\паспорта: " + row.Field<DateTime>("TU_PASPORTDATE").Date.ToString("dd.MM.yyyy");
                }
                else
                {
                    turist += Convert.ToChar(13) + "дата выдачи з\\паспорта: ";
                }
                if (row.Field<DateTime?>("TU_PASPORTDATEEND") != null)
                {
                    turist += Convert.ToChar(13) + "дата окончания действия паспорта: " +
                              row.Field<DateTime>("TU_PASPORTDATEEND").Date.ToString("dd.MM.yyyy");
                }
                else
                {
                    turist += Convert.ToChar(13) + "дата окончания действия паспорта: ";
                }
                turists += turist;
   

            }
           
           //загрузка инфо по круизу
           string cruiseInfo = string.Empty;
           SqlCommand selcruiseinfo = new SqlCommand(@"select * from mk_dogovorlistadd where tbl_dogovor_list_key=@dlKey", Connection);
           selcruiseinfo.Parameters.AddWithValue("@dlKey", dlKey);
           SqlDataAdapter cruiseinfoadapter = new SqlDataAdapter(selcruiseinfo);
           DateTime turdate;
            string dg_code;
           using (SqlCommand com = new SqlCommand(@"SELECT 
        [DL_DGCOD]
      ,[DL_TURDATE]
      ,[DL_KEY]
      ,[DL_DAY]
      ,[DL_NMEN]
  FROM [dbo].[tbl_DogovorList]
  where  DL_KEY = @dlkey", Connection))
           {
               com.Parameters.AddWithValue("@dlkey", dlKey);
               DataTable dt = new DataTable();
               SqlDataAdapter ad = new SqlDataAdapter(com);
               ad.Fill(dt);
               turdate =
                   dt.Rows[0].Field<DateTime>("DL_TURDATE").AddDays(dt.Rows[0].Field<Int16>("DL_DAY") - 1);
               nmen = dt.Rows[0].Field<Int16>("DL_NMEN");
               dg_code = dt.Rows[0].Field<string>("DL_DGCOD");


           }
            cruiseinfoadapter.Fill(_cruiseinfo);
            DataTable _servisesmas = new DataTable();
            _servisesmas.Clear();
            
            
            using (SqlCommand com = new SqlCommand("MK_lk_servises_putevka", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dg_code", dg_code);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(_servisesmas);
                
            }
            
           
           if (_cruiseinfo.Rows.Count < 1) return "";
           string ship=string.Empty, crline =string.Empty,brandCode = string.Empty;
            if (_cruiseinfo.Rows[0].Field<string>("brandcode") != null)
            {
                using (
                    SqlCommand com = new SqlCommand(@"select name_en,mnemo from  CruiseLines where mnemo = @crline ",
                                                    ConnectionTS))
                {
                    com.Parameters.AddWithValue("@crline", _cruiseinfo.Rows[0].Field<string>("brandcode"));
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    crline = dt.Rows[0].Field<string>("name_en");
                    brandCode = dt.Rows[0].Field<string>("mnemo");
                }
            }
            int id_ship = 0;
            DataTable _SystemDate = new DataTable();
            //if(brandCode!=string.Empty)
            SqlCommand systemCommand = new SqlCommand(@"select * from dbo.CruiseLines_Sys where brandCode = @crl order by parent ", ConnectionTS);
            systemCommand.Parameters.AddWithValue("@crl", brandCode);
            SqlDataAdapter sysadapter = new SqlDataAdapter(systemCommand);
            sysadapter.Fill(_SystemDate);
            foreach (DataRow row in _SystemDate.Rows)
            {
                cruiseInfo += "\n" + row.Field<string>("Parametr_name") + " : " + row.Field<string>("Parametr_value");
            }
            if (_cruiseinfo.Rows[0].Field<int?>("cl_id") != null &&
                _cruiseinfo.Rows[0].Field<string>("shipcode") != null)
            {
                using (
                    SqlCommand com =
                        new SqlCommand(
                            @"select id,name_en from  Ships where code = @shipcode and cruise_line_id = @crline ",
                            ConnectionTS))
                {
                    com.Parameters.AddWithValue("@crline", _cruiseinfo.Rows[0].Field<int>("cl_id"));
                    com.Parameters.AddWithValue("@shipcode", _cruiseinfo.Rows[0].Field<string>("shipcode"));
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    ship = dt.Rows[0].Field<string>("name_en");
                    id_ship = dt.Rows[0].Field<int>("id");
                }
            }
            string cabinNomber = string.Empty, cabinCategory = string.Empty,optionNumer =string.Empty,cabinDef=string.Empty;
            DateTime? optionDate = null;
            bool isBook = false;

           using (SqlCommand com = new SqlCommand(@"SELECT top 1
		[OP_ID]
      ,[OP_DLKEY]
      ,[OP_Descript]
      ,[OP_number]
      ,[OP_N_cabin]
      ,[OP_date_end]
      ,[OP_WHO]
      ,[OP_LastUpdate]
      ,[OP_category]
      ,[OP_IsBook]
      ,[OP_LEVEL_CABIN]
  FROM [dbo].[mk_options] where  OP_DLKEY = @dlkey
  order by OP_ID desc", Connection))
           {
               com.Parameters.AddWithValue("@dlkey", dlKey);
               DataTable dt = new DataTable();
               SqlDataAdapter ad = new SqlDataAdapter(com);
               ad.Fill(dt);
               
               if (dt.Rows.Count > 0)
               {
                   cabinNomber = dt.Rows[0].Field<string>("OP_N_cabin");
                   cabinCategory = dt.Rows[0].Field<string>("OP_category");
                   optionNumer = dt.Rows[0].Field<string>("OP_number");
                   optionDate = dt.Rows[0].Field<DateTime>("OP_date_end");
                   isBook = dt.Rows[0].Field<bool>("OP_IsBook");
                   cabinDef = dt.Rows[0].Field<string>("OP_LEVEL_CABIN");

               }
           }
            string cabinClass = string.Empty;
            if (cabinCategory != string.Empty && cabinCategory != null)
            {
                using (SqlCommand com = new SqlCommand(@"SELECT  name
  FROM [dbo].[CabinCategories]
  inner join dbo.CabinClasses as cc on class_id = cc.id where ship_id = @id and code =@code", ConnectionTS))
                {
                    com.Parameters.AddWithValue("@id", id_ship);
                    com.Parameters.AddWithValue("@code", cabinCategory);
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cabinClass = dt.Rows[0].Field<string>("name");
                    }
                }
            }
            cruiseInfo += "\n\nКруизная компания: " + crline;
           cruiseInfo += "\nЛайнер: " + ship + " код лайнера " + _cruiseinfo.Rows[0].Field<string>("shipcode");
           cruiseInfo += "\nДата круиза: " + turdate.ToString("dd.MM.yyyy");
           cruiseInfo += "\nкатегория каюты: " + cabinCategory + " с размещением " + nmen.ToString() + " чел.";
            cruiseInfo += "\nкласс каюты:" + cabinClass;

 
            if (optionNumer != null && optionNumer != string.Empty)
           {
               cruiseInfo += "\n№ каюты: " + cabinNomber;
               
               cruiseInfo += "\nНомер опции:" + optionNumer;
               cruiseInfo += "\nУровень дефицита кают :" + cabinDef;
               if (!isBook)
               {
                   cruiseInfo += "\nОпция до:" + optionDate.ToString();
               }
               else
               {
                   cruiseInfo += "\nОпция до:" + "Опция подтверждена";
               }
           }
            float totalSum = 0;
            foreach (DataRow row in _servisesmas.Rows)
            {
                if (row.Field<int>("tipe") == 1)
                {
                    if (row.Field<int>("order") == 1)
                    {
                        cruiseInfo += "\n" +  "круизный тариф : " +
                                      row.Field<float>("DL_BRUTTO");
                        totalSum += row.Field<float>("DL_BRUTTO");
                    }
                    else
                    {
                        cruiseInfo += "\n" + row.Field<string>("DL_NAME") + " : " +
                                      row.Field<float>("DL_BRUTTO");
                        totalSum += row.Field<float>("DL_BRUTTO");
                    }
                }
            }
            cruiseInfo += "\nОбщая сумма: " + totalSum.ToString();
            SqlCommand servisecrinCommand = new SqlCommand(@"SELECT [DL_key]
      ,[actions_id]
      ,[Text]
      ,CDP_NAME
      ,CDP_ORDER
      ,CDP_BLOCK
  FROM [dbo].[mk_actions_options] 
  inner join dbo.MK_Cruise_DOPServise on actions_id=CDS_ID
  where actions_id <0 and
  DL_key = @dlkey and
  CDP_BLOCK = 1
  order by CDP_ORDER  ", Connection);
           servisecrinCommand.Parameters.AddWithValue("@dlkey", dlKey);
           SqlDataAdapter serviccrineAdapter = new SqlDataAdapter(servisecrinCommand);
           serviccrineAdapter.Fill(_servisescrin);
           foreach (DataRow row in _servisescrin.Rows)
           {
               cruiseInfo += "\n"+row.Field<string>("CDP_NAME") + " " + row.Field<string>("Text");
           }
           
           //Акции
            string actions = string.Empty;
            using (SqlCommand com = new SqlCommand(selAction, Connection))
           {
               com.Parameters.AddWithValue("@dlkey", dlKey);
               com.Parameters.AddWithValue("@bon", false);
               DataTable dt = new DataTable();
               SqlDataAdapter ad = new SqlDataAdapter(com);
               ad.Fill(dt);
               if (dt.Rows.Count>0)
               {
                   actions += "\n\nАкции";
                   int ii = 1;
                   foreach (DataRow row in dt.Rows)
                   {
                       actions += "\n" + ii.ToString() + "." + row.Field<string>("Text").Replace("\\", "");
                       ii++;
                   }
               }


           }
           //бонусы
            string bonus = string.Empty;
            using (SqlCommand com = new SqlCommand(selAction, Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                com.Parameters.AddWithValue("@bon", true);
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    bonus += "\n\nБонусы";
                    int ii = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        bonus += "\n" + ii.ToString() + "." + row.Field<string>("Text").Replace("\\","");
                        ii++;
                    }
                }


            }
           //сборка 
        //   str += "\n1.Создание брони в круиз.компаниях";
           str += cruiseInfo;
           str += actions;
           str += bonus;
           str += turists;
           str += servises;
           return str;
        }
        
        
        private static SqlConnection _connection,_connectionTS;
        


        public static bool InitConnection(string user,string pass)
        {
            bool state, stateTS;
#if DEBUG
            _connection = LantaSQLConnection.Open_LantaSQLConnection("test", user,pass,out state);
            _connectionTS = LantaSQLConnection.Open_LantaSQLConnection("total_services_test", user, pass, out stateTS);
#else
             _connection = LantaSQLConnection.Open_LantaSQLConnection("mk", user,pass,out state);
            _connectionTS= LantaSQLConnection.Open_LantaSQLConnection("total_services", user,pass,out stateTS);

#endif
            

            return state && stateTS;
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
        static public SqlConnection ConnectionTS
        {
            get
            {
                
                if (_connectionTS == null)
                    throw new Exception("ConnectionTS must be initialized");
                if (_connectionTS.State != ConnectionState.Open)
                {
                    _connectionTS.Open();
                }
                return _connectionTS;
            }
        }
       
      
    }
}