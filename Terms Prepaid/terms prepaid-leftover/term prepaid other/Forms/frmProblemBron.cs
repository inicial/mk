using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataService;
using terms_prepaid.Helpers;
using terms_prepaid.UserControls;
using WpfControlLibrary.Common;

namespace terms_prepaid
{
    public partial class frmProblemBron : Form
    {
        private int _maxy = 0;
        static public DataTable _dt;
        AccessClass _access = new AccessClass(WorkWithData.Connection);

        private TabPage _managerTab;
        private ComboBox _managers;

        private IUsersService _userService;
        private bool _isSuperviser;

        public frmProblemBron(int type = 0, bool isSuperviser = false)
        {
            InitializeComponent();

            _isSuperviser = isSuperviser;
            _userService = Repository.GetInstance<IUsersService>();

            if (isSuperviser) AddManagerTab();

            GetDate();

            switch (type)
            {
                case 0:
                    tcAllMy.SelectTab(_access.isSuperViser ? tcpAll : tcpMy);
                    break;
                case 1:
                    tcAllMy.SelectTab(tcpMy);
                    break;
            }

            UpdateSize();
        }

        public void SetTable(DataTable table)
        {
            _dt = table;
        }

        private void tcAllMy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcAllMy.SelectedTab == _managerTab)
                UpdateManagerTab();
            
            UpdateSize();
        }

        private void UpdateSize()
        {
            if (tcAllMy.SelectedTab != null)
                SetSizeToTabPageContent(tcAllMy.SelectedTab);
        }

        private void GetDate()
        {
            DataTable problemCodes = WorkWithData.GetProblemCodes();
            DataSet problemSet = WorkWithData.GetAllProblems(_managers != null ? (int?)_managers.SelectedValue : null);
            
            FillTab(tcpAll, problemCodes, problemSet, "");
            FillTab(tcpMy, problemCodes, problemSet, "MY");
            
            if (!_access.isSuperViser)
                tcAllMy.TabPages.Remove(tcpAll);

            UpdateSize();
        }

        private void UpdateManagerTab()
        {
            _maxy = 0;

            DataTable problemCodes = WorkWithData.GetProblemCodes();
            DataSet problemSet = WorkWithData.GetAllProblems((int?)_managers.SelectedValue);

            for (int i = _managerTab.Controls.Count - 1; i > 0; i--)
                _managerTab.Controls.RemoveAt(i);

            FillTab(_managerTab, problemCodes, problemSet, "MANAGER");

            UpdateSize();
        }

        private void SetSizeToTabPageContent(TabPage tabPage)
        {
            const int minWidth = 200;
            const int minHeight = 100;
            const int offsetX = 30;
            const int offsetY = 70;

            var controls = tabPage.Controls.Cast<Control>().ToArray();
            var maxX = controls.Length > 0 ? controls.Max(c => c.Right) : 0;
            var maxY = controls.Length > 0 ? controls.Max(c => c.Bottom) : 0;

            Size = new Size(Math.Max(maxX + offsetX, minWidth), Math.Max(maxY + offsetY, minHeight));
        }

        private void FillTab(TabPage tabPage, DataTable problemCodes, DataSet problemSet, string suffix)
        {
            var heightOffset = _managerTab != null ? _managers.Height : 0;

            _maxy = 0;
            int widh = 600, heigth = 40;
            bool flag = false;

            var t = problemSet.Tables["ALL" + suffix];
            if (t.Rows.Count > 0)
                tabPage.Controls.Add(new ucProblemButon(t, "Все проблемные брони")
                {
                    Location = new Point(0, _maxy + heightOffset),
                    Size = new Size(widh, heigth)
                });

            foreach (DataRow row in problemCodes.Rows)
            {
                int x = flag ? 0 : widh + 1;
                string tableName = row.Field<string>("mpc_TableName"), buttonName = row.Field<string>("mpc_name");

                t = problemSet.Tables[tableName + suffix];
                if (t.Rows.Count > 0)
                    tabPage.Controls.Add(new ucProblemButon(t, buttonName)
                    {
                        Location = new Point(x, _maxy + heightOffset),
                        Size = new Size(widh, heigth)
                    });
                else
                    continue;

                flag = !flag;
                if (flag)
                    _maxy += heigth;
            }
        }

        static public DataTable GetProblemBron(DataTable data, int type = 0, bool isSuperviser = false)
        {
            _dt = data;
            frmProblemBron frm = new frmProblemBron(type, isSuperviser);
            frm.ShowDialog();
            return _dt;
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            _dt=new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(WorkWithData.SearchDogovorProc, WorkWithData.Connection))
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(WorkWithData.SearchDogovorProc, WorkWithData.Connection))
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(WorkWithData.SearchDogovorProc, WorkWithData.Connection))
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(WorkWithData.SearchDogovorProc, WorkWithData.Connection))
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(WorkWithData.SearchDogovorProc, WorkWithData.Connection))
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(WorkWithData.SearchDogovorProc, WorkWithData.Connection))
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(WorkWithData.SearchDogovorProc, WorkWithData.Connection))
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

        private void AddManagerTab()
        {
            _managerTab = new TabPage("По сотруднику");
            tcAllMy.TabPages.Add(_managerTab);

            _managers = new ComboBox
            {
                DataSource = _userService.GetManagersWithProblemVouchers(),
                ValueMember = "mp_bronKey",
                DisplayMember = "mp_bronir"
            };

            _managers.SelectedValueChanged += (sender, args) => UpdateManagerTab();
            _managerTab.Controls.Add(_managers);
        }
    }
}
