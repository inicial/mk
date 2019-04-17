using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using lanta.Clients;
using System.Threading;

namespace lanta.DirectClientDogovor
{
    public partial class ProgressStatisticCalculation : Form
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlConnection connection;
        long MANAGER_ID; 
        DataTable cards = new DataTable("cards");
        DataTable Discount_Client = new DataTable("Discount_Client");
        bool Break;
        Thread t; int i; string cur_name; string cur_KEY;
        public ProgressStatisticCalculation(SqlConnection connection, long MANAGER_ID)
        {
            InitializeComponent();
            this.connection = connection;
            this.MANAGER_ID = MANAGER_ID;
            

        }

        private void ProgressStatisticCalculation_Load(object sender, EventArgs e)
        {
 
            adapter.SelectCommand = new SqlCommand(
                    @"SELECT     DS_KEY, DS_NAME
            FROM         Discount_Client", connection);//3 - золотые, 1 - серебряные, 0 - обычные
            adapter.Fill(Discount_Client);
            DataRow dr = Discount_Client.NewRow();
            dr["DS_KEY"] = 200;
            dr["DS_NAME"] = "Все с дисконтными картами";
            Discount_Client.Rows.Add(dr);
          
            dr = Discount_Client.NewRow();
            dr["DS_KEY"] = 300;
            dr["DS_NAME"] = "Все с картами и без ";
            Discount_Client.Rows.Add(dr);

            dr = Discount_Client.NewRow();
            dr["DS_KEY"] = 400;
            dr["DS_NAME"] = "Клиенты путёвок без статистики";
            Discount_Client.Rows.Add(dr);
           
            dr = Discount_Client.NewRow();
            dr["DS_KEY"] = 500;
            dr["DS_NAME"] = "Клиенты без статистики";
            Discount_Client.Rows.Add(dr);



            comboBox_Discount.DataSource = Discount_Client;
            comboBox_Discount.DisplayMember = "DS_NAME";
            comboBox_Discount.ValueMember = "DS_KEY";
 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query=@"SELECT CL_KEY FROM Clients";
                        
            cards.Clear();
            if (Convert.ToInt32(comboBox_Discount.SelectedValue) < 300)
            {
                query = @"SELECT     CARDS.CD_CLKey
                    FROM         CARDS INNER JOIN
                      Discount_Client ON CARDS.CD_DSKey = Discount_Client.DS_KEY";
                if (Convert.ToInt32(comboBox_Discount.SelectedValue) < 200)//3 - золотые, 1 - серебряные, 0 - обычные
                {
                    query = query + " WHERE     (Discount_Client.DS_KEY = " + comboBox_Discount.SelectedValue.ToString() + ")";
                }
                cur_KEY = "CD_CLKey";
            }
            else
            {
                if (Convert.ToInt32(comboBox_Discount.SelectedValue) == 300)
                {
                    query = @"SELECT CL_KEY FROM Clients order by CL_KEY DESC";
                        
                }
                if (Convert.ToInt32(comboBox_Discount.SelectedValue) == 400)
                {
                    query = @"SELECT Clients.CL_KEY
                        FROM         tbl_Turist INNER JOIN
                      tbl_Dogovor ON tbl_Turist.TU_DGKEY = tbl_Dogovor.DG_Key INNER JOIN
                      Clients ON tbl_Turist.TU_ID = Clients.CL_KEY LEFT OUTER JOIN
                      Lanta_ClientStatDogovor ON tbl_Dogovor.DG_CODE = Lanta_ClientStatDogovor.CSD_DGCODE COLLATE Cyrillic_General_CS_AS
                        WHERE     (tbl_Dogovor.DG_PARTNERKEY = 0) AND (Lanta_ClientStatDogovor.CSD_CLКеу IS NULL) AND (tbl_Dogovor.DG_TURDATE < GETDATE())";
                }
                if (Convert.ToInt32(comboBox_Discount.SelectedValue) == 500)
                {
                    query = @"SELECT    Clients.CL_KEY
                        FROM         Clients LEFT OUTER JOIN
                          Lanta_ClientStatDogovor ON Clients.CL_KEY = Lanta_ClientStatDogovor.CSD_CLКеу
                        WHERE     (Lanta_ClientStatDogovor.CSD_CLКеу IS NULL) order by CL_KEY DESC";
                }

                cur_KEY = "CL_KEY";
            }
            
            adapter.SelectCommand = new SqlCommand(query, connection);
            adapter.Fill(cards);
            progressBar1.Maximum = cards.Rows.Count;
            t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
            timer1.Start();
          
        }
        public  void ThreadProc()
        {  
            int CL_KEY;
            EditClient ec;
            i = 0;
            Break = false;
            foreach (DataRow dr in cards.Rows)
            {
                i++;
                CL_KEY = Convert.ToInt32(dr[cur_KEY]);
                ec = new EditClient(CL_KEY, MANAGER_ID, connection,false,false);
                cur_name = "(" + i.ToString() + " из " + cards.Rows.Count.ToString() + ") " + ec.textBox_CL_NAMERUS.Text + " " + ec.textBox_CL_FNAMERUS.Text + " " + ec.textBox_CL_SNAMERUS.Text + " : " + CL_KEY.ToString();
                ec.UpdateStatistic();
                ec.SaveClient(false);
                ec.Dispose();
                if (Break)
                    break;
            } 
            cur_name = "Подсчёт завершён";
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Break = true;
            t.Join();
            label_name.Text = "Подсчёт остановлен";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = i;
            label_name.Text = cur_name;
        }
    }
}
