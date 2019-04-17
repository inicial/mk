using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;
using terms_prepaid.UserControls;

namespace terms_prepaid
{
    public partial class frmProblemBron : Form
    {
        private int _maxy = 0;
        static public DataTable _dt;
        AccessClass _access = new AccessClass(WorkWithData.Connection);
        public frmProblemBron(int type =0)
        {
            InitializeComponent();
            GetDate();
            if (type == 0)
            {
                if (_access.isSuperViser)
                {
                    tcAllMy.SelectTab(tcpAll);
                }
                else
                {
                    tcAllMy.SelectTab(tcpMy);
                }
            }else if (type == 1)
            {
                tcAllMy.SelectTab(tcpMy);
            }
        }
        public  void  SetTable(DataTable table)
        {
            _dt = table;
        }
        void GetDate()
        {
            DataTable problemCodes = WorkWithData.GetProblemCodes();
            DataSet problemSet = WorkWithData.GetAllProblems();
            int widh=600, heigth =40;
            bool flag = false;
            tcpAll.Controls.Add(new ucProblemButon(problemSet.Tables["ALL"],"Все проблемные брони")
                {
                    Location = new Point(0,_maxy),
                    Size = new Size(widh, heigth)
                });
            tcpMy.Controls.Add(new ucProblemButon(problemSet.Tables["ALLMY"], "Все проблемные брони")
            {
                Location = new Point(0, _maxy),
                Size = new Size(widh, heigth)
            });
            foreach (DataRow row in problemCodes.Rows)
            {
                int x = flag ? 0 : widh+1;
                string tableName = row.Field<string>("mpc_TableName"), buttonName = row.Field<string>("mpc_name");
                tcpAll.Controls.Add(new ucProblemButon(problemSet.Tables[tableName], buttonName)
                {
                    Location = new Point(x, _maxy),
                    Size = new Size(widh, heigth)
                });
                tcpMy.Controls.Add(new ucProblemButon(problemSet.Tables[tableName+"MY"], buttonName)
                {
                    Location = new Point(x, _maxy),
                    Size = new Size(widh, heigth)
                });
                flag = !flag;
                if (flag)
                {
                    _maxy += heigth;
                }
            }
            int staticAdd = 65;
            int maxHeigth = (Screen.PrimaryScreen.Bounds.Height/2/heigth)*heigth; 
            int newHeight = (flag ? _maxy : _maxy + heigth)> maxHeigth ? maxHeigth +staticAdd: (flag ? _maxy : _maxy + heigth)+staticAdd;
            int newWidht = (flag ? _maxy : _maxy + heigth) > maxHeigth ? 2*widh+10 : 2*widh+30;
            this.Size = new Size(newWidht,newHeight);
            if(!_access.isSuperViser)tcAllMy.TabPages.Remove(tcpAll);
            //foreach (var control in tableLayoutPanel1.Controls)
            //{
            //    TextBox tb = control as TextBox;
            //    if (tb != null)
            //    {
            //        if (string.IsNullOrEmpty(tb.Text)) tb.Text = 0.ToString();
            //        if (int.Parse(tb.Text) > 0)
            //        {

            //                tb.ForeColor = Color.Red;

            //        }
            //        else
            //        {
            //            tb.ForeColor = SystemColors.ControlText;
            //        }
            //    }

            //}
            //timePulse.Enabled = true;
        }
        static public DataTable GetProblemBron(DataTable data,int type =0)
        {
            _dt = data;
            frmProblemBron frm = new frmProblemBron(type);
            frm.ShowDialog();
            return _dt;
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            _dt=new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType= CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status",1);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnNoPrintDoc_Click(object sender, EventArgs e)
        {
            _dt=new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 12);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnNoInsertDoc_Click(object sender, EventArgs e)
        {
            _dt=new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@document", 4);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnNoDogovorAccept_Click(object sender, EventArgs e)
        {
            _dt=new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dogovor", 2);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();

        }

        private void btnNoInshur_Click(object sender, EventArgs e)
        {
            _dt=new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 6);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnNoAcceptUsluga_Click(object sender, EventArgs e)
        {
            _dt=new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 7);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnDogovorNoAccept_Click(object sender, EventArgs e)
        {
            _dt=new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 5);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void timePulse_Tick(object sender, EventArgs e)
        {
            
        }

        private void btnAllProblem_Click(object sender, EventArgs e)
        {
            //_dt = new DataTable();
            //int count = 0;
            //using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@status", 1);
            //    com.Parameters.AddWithValue("@problem", true);
            //    com.Parameters.AddWithValue("@count", 0);
            //    com.Parameters["@count"].Direction = ParameterDirection.Output;
            //    SqlDataAdapter adapter = new SqlDataAdapter(com);
            //    adapter.Fill(_dt);
            //    tbOption3.Text = com.Parameters["@count"].Value.ToString();
            //    count += Convert.ToInt32(com.Parameters["@count"].Value);

            //}
            //using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@status", 6);
            //    com.Parameters.AddWithValue("@count", 0);
            //    com.Parameters.AddWithValue("@problem", true);
            //    com.Parameters["@count"].Direction = ParameterDirection.Output;
            //    SqlDataAdapter adapter = new SqlDataAdapter(com);
            //    adapter.Fill(_dt);
            //    tbNoInshur.Text = com.Parameters["@count"].Value.ToString();
            //    count += Convert.ToInt32(com.Parameters["@count"].Value);

            //}
            //using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@status", 7);
            //    com.Parameters.AddWithValue("@problem", true);
            //    com.Parameters.AddWithValue("@count", 0);
            //    com.Parameters["@count"].Direction = ParameterDirection.Output;
            //    SqlDataAdapter adapter = new SqlDataAdapter(com);
            //    adapter.Fill(_dt);
            //    tbNoAcceptUsluga.Text = com.Parameters["@count"].Value.ToString();
            //    count += Convert.ToInt32(com.Parameters["@count"].Value);

            //}
            //using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@status", 12);
            //    com.Parameters.AddWithValue("@problem", true);
            //    com.Parameters.AddWithValue("@count", 0);
            //    com.Parameters["@count"].Direction = ParameterDirection.Output;
            //    com.ExecuteNonQuery();
            //    tbDogovorNoAccept.Text = com.Parameters["@count"].Value.ToString();
            //    //count += Convert.ToInt32(com.Parameters["@count"].Value);

            //}
            //using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@dogovor", 2);
            //    com.Parameters.AddWithValue("@count", 0);
            //    com.Parameters.AddWithValue("@problem", true);
            //    com.Parameters["@count"].Direction = ParameterDirection.Output;
            //    SqlDataAdapter adapter = new SqlDataAdapter(com);
            //    adapter.Fill(_dt);
            //    tbNoDogovorAccept.Text = com.Parameters["@count"].Value.ToString();
            //    count += Convert.ToInt32(com.Parameters["@count"].Value);

            //}
            ////using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            ////{
            ////    com.CommandType = CommandType.StoredProcedure;
            ////    com.Parameters.AddWithValue("@document", 1);
            ////    com.Parameters.AddWithValue("@count", 0);
            ////    com.Parameters.AddWithValue("@problem", true);
            ////    com.Parameters["@count"].Direction = ParameterDirection.Output;
            ////    SqlDataAdapter adapter = new SqlDataAdapter(com);
            ////    adapter.Fill(_dt);
            ////    tbNoPrintDoc.Text = com.Parameters["@count"].Value.ToString();
            ////    count += Convert.ToInt32(com.Parameters["@count"].Value);

            ////}
            //using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@document", 4);
            //    com.Parameters.AddWithValue("@count", 0);
            //    com.Parameters.AddWithValue("@problem", true);
            //    com.Parameters["@count"].Direction = ParameterDirection.Output;
            //    SqlDataAdapter adapter = new SqlDataAdapter(com);
            //    adapter.Fill(_dt);
            //    tbNoInsertDoc.Text = com.Parameters["@count"].Value.ToString();
            //    count += Convert.ToInt32(com.Parameters["@count"].Value);

            //}
            //int rowCount = _dt.Rows.Count-1;
            //for (int i = 0; i < rowCount; i++)
            //{
            //    DataRow row = _dt.Rows[i];
            //    if (_dt.Select(string.Format("DG_CODE='{0}'", row.Field<string>("DG_CODE"))).Length > 1)
            //    {
            //        _dt.Rows.Remove(row);
            //        rowCount--;
            //    }
            //}
            Close();
        }

        private void frmProblemBron_Load(object sender, EventArgs e)
        {

        }

        
    }
}
