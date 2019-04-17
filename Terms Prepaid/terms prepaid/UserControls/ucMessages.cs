using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using terms_prepaid.Helpers;
using WpfControlLibrary.ViewModel;

namespace terms_prepaid.UserControls
{
    public partial class ucMessages : UserControl
    {
        private const string selectMessageMaster =
            "select HI_DGCOD,HI_DATE ,HI_WHO,HI_TEXT,HI_MOD from History where [HI_MOD]=@p1 and [HI_DGCOD]=@p2 order by HI_DATE desc";

        private const string updteNew = "insert into dbo.mk_messageStatus(MS_HIID,MS_IsRead,MS_USKEY) " +
                                        "select HI_ID,1,@user from History where HI_MOD in ('MTM','WWW') and [HI_DGCOD]=@p2 and HI_ID not in (select MS_HIID from dbo.mk_messageStatus where MS_USKEY=@user ) " +
                                        "update mk_messageStatus set MS_IsRead=1 where MS_HIID in(select HI_ID from History where [HI_DGCOD]=@p2 ) ";

        private const string selectMessageMaster1 =
            "select HI_DGCOD,HI_DATE ,case when HI_WHO in('www.mcruises.ru' ,'')then 'Клиент' else HI_WHO end  as HI_WHO,HI_TEXT,HI_MOD from History where [HI_MOD] in('MTC','www','MCO')  and [HI_DGCOD]=@p2 order by HI_DATE desc";

        private const string insertMessage =
            "insert into History (HI_DGCOD,HI_DATE,HI_WHO,HI_TEXT,HI_MOD) values (@p1,GetDate(),(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME()),@p3,@p4)";

        private DataTable _mtmTable = new DataTable();
        private DataTable _mtcTable = new DataTable();
        RichTextBox rtb = new RichTextBox();
        private string _dgCode = "";

        public ucMessages(string dgCode)
        {

            InitializeComponent();
            _dgCode = dgCode;
            GetData();
            UpdateNonRead();
            

        }


        public ucMessages(string dgCode, int width, int x, int y)
        {
            InitializeComponent();
            _dgCode = dgCode;
            GetData();
           // UpdateNonRead();
            this.Width = width;
            this.SetBounds(x,y,width,Screen.PrimaryScreen.Bounds.Height-y-35);
            // this.Dispose();
           // this.SetDesktopLocation(x,y);// = new Point(422, 307);
           //this.Anchor =AnchorStyles.Right;
            //this.Refresh();


        }

        private void UpdateNonRead()
        {
            DataTable dt = new DataTable();
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter(
                        "Select HI_ID,HI_MOD from History where HI_MOD in ('MTM','WWW') and HI_ID not in (select MS_HIID from dbo.mk_messageStatus where MS_isRead=1  and ms_uskey=@user) and HI_DGCOD =@p2 ",
                        WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@p2", _dgCode);
                adapter.SelectCommand.Parameters.AddWithValue("@user", WorkWithData.GetUserID());
                adapter.Fill(dt);
            }
            int mtmCount = dt.Select("HI_MOD='MTM'").Length, wwwCount = dt.Rows.Count - mtmCount;
            //if (mtmCount > 0)
            //{
            //    tpComments.Text = "?Сообщения бронировщику\\реализатору";
            //    //tpComments.Font.Style = new FontStyle();
            //    //tpComments.ForeColor = Color.Green;
            //}
            //else
            //{
            //    tpComments.Text = "Сообщения бронировщику\\реализатору";
            //    //tpComments.ForeColor = SystemColors.ControlText;
            //}
            //if (wwwCount > 0)
            //{
            //    tpMessages.Text = "?Переписка с клиентом";
            //    //tpMessages.ForeColor = Color.Green;
            //}
            //else
            //{
            //    tpMessages.Text = "Переписка с клиентом";
            //    //tpMessages.ForeColor = SystemColors.ControlText;
            //}
            using (SqlCommand com = new SqlCommand(updteNew, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@p2", _dgCode);
                com.Parameters.AddWithValue("@user", WorkWithData.GetUserID());
                com.ExecuteNonQuery();
            }

        }

        void VisulMessages(RichTextBox rtb,DataTable _dataTable)
        {
            rtb.Text = "";
            List<string> names = new List<string>();
            foreach (DataRow row in _dataTable.Rows)
            {
                if (row.Field<string>("HI_MOD") == "MCO")
                {
                    
                    continue;
                }
                String buff = row.Field<DateTime>("HI_DATE").ToString("dd.MM.yyyy HH:mm") + " :: " +
                              row.Field<string>("HI_WHO") + " :: " + row.Field<string>("HI_TEXT").Replace("\n", " ").Replace("\r", " ");
                if(!( names.IndexOf(row.Field<string>("HI_WHO"))>=0))
                {
                    names.Add(row.Field<string>("HI_WHO"));
                }
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
                
                DataRow[] rows = _dataTable.Select(string.Format("HI_WHO like '%{0}%'", name.Trim()));
                try
                {
                    DataRow row = rows[0];
                    if (row.Field<string>("HI_mod").ToUpper() == RequestMessageMod.MTM || row.Field<string>("HI_mod").ToUpper() == RequestMessageMod.MTC)
                    {
                        rtb.SelectionColor = Color.Blue;
                    }
                    else
                    {
                        rtb.SelectionColor = Color.Green;
                    }
                }
                catch (Exception)
                {
                    
                }

            }
            //rtb.Text = rtb.Text.Replace("::", "");
        }
        private void GetData()
        {
            
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectMessageMaster, WorkWithData.Connection))
            {
                SqlCommand com = adapter.SelectCommand;
                com.Parameters.AddWithValue("@p1", RequestMessageMod.MTM);
                com.Parameters.AddWithValue("@p2", _dgCode);
                _mtmTable.Clear();
                adapter.Fill(_mtmTable);
                VisulMessages(rtbMTM,_mtmTable);
            }
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectMessageMaster1, WorkWithData.Connection))
            {
                SqlCommand com = adapter.SelectCommand;
               // com.Parameters.AddWithValue("@p1", RequestMessageMod.MTC);
                com.Parameters.AddWithValue("@p2", _dgCode);
                _mtcTable.Clear();
                adapter.Fill(_mtcTable);
                VisulMessages(rtbMTCMesages, _mtcTable);
            }
            if (_mtcTable.Rows.Count > 0)
            {
                DataRow lastClient = _mtcTable.Rows[_mtcTable.Rows.Count - 1];
                if (lastClient.Field<string>("HI_MOD") == "MCO")
                {
                    cbClientOk.Checked = true;
                }
            }
            UpdateNonRead();
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
                WorkWithData.InsertHistory(_dgCode,s,mod,"");
                //using (SqlCommand com = new SqlCommand(insertMessage,WorkWithData.Connection))
                //{
                //    com.Parameters.AddWithValue("@p4",mod);
                //    com.Parameters.AddWithValue("@p3",s);
                //    //com.Parameters.AddWithValue("@p2", mod);
                //    com.Parameters.AddWithValue("@p1", _dgCode);
                //    com.ExecuteNonQuery();
                //}
            }
        }
        private void btnSpendMTM_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbMTMMessage.Text)){return;}
            InsertMessage(RequestMessageMod.MTM,tbMTMMessage.Text);
            tbMTMMessage.Text = "";
            GetData();

        }

        private void btnMTCSpend_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbMTCMessage.Text)) { return; }
            InsertMessage(RequestMessageMod.MTC, tbMTCMessage.Text);
            tbMTCMessage.Text = "";
            GetData();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmstCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbMTCMesages.SelectedText))
                Clipboard.SetText(rtbMTCMesages.SelectedText);

            /*if(!string.IsNullOrEmpty(rtb.SelectedText))
                Clipboard.SetText(rtbMTCMesages.SelectedText);*/
        }

        private void rtbMTCMesages_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rtb = sender as RichTextBox;
            }
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbMTM.SelectedText))
                Clipboard.SetText(rtbMTM.SelectedText);

            /*if (!string.IsNullOrEmpty(rtb.SelectedText))
                Clipboard.SetText(rtbMTM.SelectedText);*/
        }

        private void cbClientOk_CheckedChanged(object sender, EventArgs e)
        {
            if (cbClientOk.Checked)
            {
                tbMTCMessage.Enabled = false;
                btnMTCSpend.Enabled = false;
                 DataRow lastClient = _mtcTable.Rows[_mtcTable.Rows.Count - 1];
                if (lastClient.Field<string>("HI_MOD") != "MCO")
                {
                    WorkWithData.InsertHistory(_dgCode, "", "MCO", "");
                }
            }
            else
            {
                tbMTCMessage.Enabled = true;
                btnMTCSpend.Enabled = true;
            }
        }
    }
}
