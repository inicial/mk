using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingsFromSite
{
     class  WorkWithData
     {
         private SqlConnection _connection;
         public WorkWithData(SqlConnection connection)
         {
             _connection = connection;
         }

        #region Consts

         private const string updateStatus = @"update Mk_ring_from_site set [status] =@st where id = @id";
        private const string selectstatuses = @"select id,name_ru from mk_status_ring where is_enable =1";
        private const string selectallstatuses = @"select -1 as id ,'Все звонки'as name_ru union
                            select id,name_ru from mk_status_ring";
        private const string selectRings =
            @"select mrf.id,Phone,Time_for_ring,[status],msr.name_ru,date_create,duration_ring,url_from_ring from Mk_ring_from_site as mrf inner join mk_status_ring as msr on mrf.[status]=msr.id";
        #endregion
        public DataTable GetAllStatuses()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectallstatuses, _connection))
            {
                adapter.Fill(dt);
            }
            return dt;
        }
        
         public void ChangeStatus(int idRign, int idStatus)
         {
             using (SqlCommand com = new SqlCommand(updateStatus,_connection))
             {
                 com.Parameters.AddWithValue("@st", idStatus);
                 com.Parameters.AddWithValue("@id", idRign);
                 com.ExecuteNonQuery();
             }
         } 
         public DataTable GetRingsJournal(DateTime dateBegin,DateTime dateEnd,int status)
         {
             DataTable dt = new DataTable();
             string query = selectRings;
             string whereString = string.Empty;
             dateEnd = dateEnd.Date.AddHours(23).AddMinutes(59);
             if (status==-1)
             {
                 whereString = "  where date_create between @datebeg and @dateEnd";
             }
             else
             {
                 whereString = "  where date_create between @datebeg and @dateEnd and [status] =@status";
             }
             query += whereString;
             using (SqlDataAdapter adapter = new SqlDataAdapter(query, _connection))
             {
                 SqlCommand com = adapter.SelectCommand;
                 com.Parameters.AddWithValue("@datebeg", dateBegin);
                 com.Parameters.AddWithValue("@dateEnd", dateEnd);
                 if (status != -1)
                 {
                     com.Parameters.AddWithValue("@status", status);
                 }
                 adapter.Fill(dt);
             }
             return dt;
         }
         public  DataTable GetStatuses()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectstatuses,_connection))
            {
                adapter.Fill(dt);
            }
            return dt;

        }
    }
}
