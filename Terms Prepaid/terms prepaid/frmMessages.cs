using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rep10027.Helpers;

namespace terms_prepaid
{
    public partial class frmMessages : Form
    {
        private const string selectMessageMaster =
            "select HI_DGCOD,HI_DATE ,HI_WHO,HI_TEXT,HI_MOD from History where [HI_MOD]=@p1 and [HI_DGCOD]=@p2 order by HI_DATE";

        private const string selectMessageMaster1 =
            "select HI_DGCOD,HI_DATE ,case when HI_WHO in('www.mcruises.ru' ,'')then 'Клиент' else HI_WHO end  as HI_WHO,HI_TEXT,HI_MOD from History where [HI_MOD] in('MTC','www')  and [HI_DGCOD]=@p2 order by HI_DATE";

        private const string insertMessage =
            "insert into History (HI_DGCOD,HI_DATE,HI_WHO,HI_TEXT,HI_MOD) values (@p1,GetDate(),(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME()),@p3,@p4)";

        private DataTable _mtmTable = new DataTable();
        private DataTable _mtcTable = new DataTable();
        private string _dgCode = "";


        public frmMessages(string dgCode)
        {
            InitializeComponent();
            _dgCode = dgCode;
            GetData();
        }
        void VisulMessages(RichTextBox rtb,DataTable _dataTable)
        {
            rtb.Text = "";
            List<string> names = new List<string>();
            foreach (DataRow row in _dataTable.Rows)
            {
                String buff = row.Field<DateTime>("HI_DATE").ToString("dd.MM.yyyy HH:mm:ss") + " :: " +
                              row.Field<string>("HI_WHO") + " :: " + row.Field<string>("HI_TEXT").Replace("\n", " ").Replace("\r", " ");

                rtb.Text += buff + "\n";

               // string line = rtb.Lines.Last();
                
                

            }
            foreach (string line in rtb.Lines)
            {
                int p0 = rtb.Text.IndexOf(line), p1 = line.Length;
                rtb.Select(p0, p1);
                String[] masStr = line.Split(' ');
                string name = "";
                if (masStr.Length < 4)
                {
                    continue;
                }
                int i = 0;
                if(line.IndexOf("::")<0)continue;
                if (line.IndexOf("::") == line.LastIndexOf("::"))continue;
                while (masStr[i]!="::")
                {
                    i++;
                }
                i++;
                while (masStr[i] != "::")
                {
                    if (name == "")
                    {
                        name += masStr[i];
                    }
                    else
                    {
                        name += " " + masStr[i];
                    }
                    i++;
                }
                
                DataRow[] rows = _dataTable.Select(string.Format("HI_WHO='{0}'", name));
                DataRow row = rows[0];
                if (row.Field<string>("HI_mod").ToUpper() == "MTM" || row.Field<string>("HI_mod").ToUpper() == "MTC")
                {
                    rtb.SelectionColor = Color.Blue;
                }
                else
                {
                    rtb.SelectionColor = Color.Green;
                }
            }

        }
        private void GetData()
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectMessageMaster, WorkWithData.Connection))
            {
                SqlCommand com = adapter.SelectCommand;
                com.Parameters.AddWithValue("@p1", "MTM");
                com.Parameters.AddWithValue("@p2", _dgCode);
                _mtmTable.Clear();
                adapter.Fill(_mtmTable);
                VisulMessages(rtbMTM,_mtmTable);
            }
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectMessageMaster1, WorkWithData.Connection))
            {
                SqlCommand com = adapter.SelectCommand;
               // com.Parameters.AddWithValue("@p1", "MTC");
                com.Parameters.AddWithValue("@p2", _dgCode);
                _mtcTable.Clear();
                adapter.Fill(_mtcTable);
                VisulMessages(rtbMTCMesages, _mtcTable);
            }
        }
        void InsertMessage(string mod,string buff)
        {
            List<string> lbuff = new List<string>();
            string[] arrayWords = buff.Split(' ');
            int count = 0;
            lbuff.Add("");
            foreach (string arrayWord in arrayWords)
            {
                if (lbuff[count].Length+ arrayWord.Length >= 253)
                {
                    count++;
                    lbuff.Add("");
                }
                lbuff[count] +=" "+ arrayWord;
            }
            foreach (string s in lbuff)
            {
                using (SqlCommand com = new SqlCommand(insertMessage,WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@p4",mod);
                    com.Parameters.AddWithValue("@p3",s);
                    //com.Parameters.AddWithValue("@p2", mod);
                    com.Parameters.AddWithValue("@p1", _dgCode);
                    com.ExecuteNonQuery();
                }
            }
        }
        private void btnSpendMTM_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbMTMMessage.Text)){return;}
            InsertMessage("MTM",tbMTMMessage.Text);
            tbMTMMessage.Text = "";
            GetData();

        }

        private void btnMTCSpend_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbMTCMessage.Text)) { return; }
            InsertMessage("MTC", tbMTCMessage.Text);
            tbMTCMessage.Text = "";
            GetData();
        }
    }
}
