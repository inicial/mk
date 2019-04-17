using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid.UserControls
{
    public partial class ucDayTasks : UserControl
    {
        public DataTable _taskTable;

        public bool FiltrFlag;
        public int AllCount;
        public int FiltrCount;


        public ucDayTasks()
        {
            InitializeComponent();

            _taskTable = new DataTable();

            // GetDate();
        }
        
        private void GetDate()
        {
            //dgvTasks.Font.Size = 8;
            AllCount = 0;
            FiltrCount = 0;

            _taskTable = WorkWithData.GetDaysTasks();

            if (_taskTable != null)
            {
                if (_taskTable.Columns.Count > 0)
                {
                    try
                    {
                        UpdateDataGrid();
                    }
                    catch (Exception)
                    {

                    }
                }
            }

        }
        
        private void UpdateDataGrid()
        {
            AllCount = _taskTable.Rows.Count;
            FiltrCount = 0;

            _taskTable.Columns.Add("WARNING", typeof(Image));
            _taskTable.Columns.Add("NAME_DATE", typeof(string));

            int num = 0;
            DateTime now_dt = DateTime.Now.Date;
            //foreach (DataRow row in _taskTable.Rows)
            for (int i = 0; i < _taskTable.Rows.Count; i++)
            {
                DataRow row = _taskTable.Rows[i];
                bool bFiltr = ((DateTime)row["task_date"]).Date > now_dt;
                if (!bFiltr) FiltrCount++;
                if (bFiltr && FiltrFlag)
                {
                    _taskTable.Rows.Remove(row);
                    i--;
                }
                else
                {
                    num++;
                    row["task_number"] = num;
                    if (row.Field<int>("TASK_PRIOR") == 1)
                    {
                        //row["warning"] = Properties.Resources.ico_warning_1.ToBitmap();
                        row["warning"] = Properties.Resources.ico_warning_2.ToBitmap();
                    }
                    else if (row.Field<int>("TASK_PRIOR") == 2)
                    {
                        //row["warning"] = Properties.Resources.ico_warning_2.ToBitmap();
                        row["warning"] = Properties.Resources.ico_warning_3.ToBitmap();
                    }
                    else if (row.Field<int>("TASK_PRIOR") == 3)
                    {
                        //row["warning"] = Properties.Resources.ico_warning_3.ToBitmap();
                        row["warning"] = Properties.Resources.ico_empty.ToBitmap();
                    }
                    else
                    {
                        row["warning"] = Properties.Resources.ico_empty.ToBitmap();
                    }

                    string name_date = (string)row["NAME"];
                    DateTime task_date = (DateTime)row["task_date"];
                    if (task_date.Year > 2010) name_date = name_date + "  на " + task_date.ToString("dd.MM.yy");
                    row["NAME_DATE"] = name_date;
                }
            }
            
            dgvTasks.DataSource = _taskTable;

            foreach (DataGridViewColumn column in dgvTasks.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "TASK_PRIOR":
                        column.Visible = false;
                        //column.HeaderText = "!";
                        //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        //column.Width = 20;
                        break;
                    case "WARNING":
                        if (dgvTasks.Columns.Count > 1)
                            column.DisplayIndex = 1;
                        column.Width = 40;
                        column.HeaderText = " ";
                        break;
                    case "TASK_NUMBER":
                        column.HeaderText = "";
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.Width = 20;
                        break;
                    case "DG_CODE":
                        column.HeaderText = "Путевка"; 
                        //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        //column.Width = 90;
                        break;
                    case "DATES":
                        column.HeaderText = "Дата/время";
                        //column.DefaultCellStyle.Format = "dd.MM.yy HH:mm";
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.Width = 90;
                        break;
                    //case "TIMES":
                    //    column.HeaderText = "Время";
                    //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //    column.Width = 40;
                    //    break;
                    //case "NAME":
                    //    column.HeaderText = "Задача";
                    //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //    column.Width = 300; // 180;
                    //    break;
                    case "NAME_DATE":
                        if (dgvTasks.Columns.Count > 8)
                            column.DisplayIndex = 8;
                        column.HeaderText = "Задача";
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.Width = 300; // 180;
                        break;
                    //case "STAT":
                    //    column.HeaderText = "Статус";
                    //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //    column.Width = 120;
                    //    break;
                    default:
                        column.Visible = false;
                        break;
                }
            }

        }

        public void RefreshData()
        {
            GetDate();
        }

        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Control parent = this.Parent;
                while ((parent as Form) == null)
                {
                    parent = parent.Parent;
                }
                int code = (int)dgvTasks.Rows[e.RowIndex].Cells["code"].Value;
                string dgcode = dgvTasks.Rows[e.RowIndex].Cells["DG_CODE"].Value.ToString();

                string msg = "Подождите, пожалуйста, идет загрузка."; // "Загрузка  путевки  " + dgcode
                frmIntro intro = new frmIntro(""); // msg
                intro.Show();
                intro.Refresh();

                Form frmParent = parent as Form;
                //frmParent.Hide();
                //new frmNewOptions(dgcode, code).ShowDialog();
                frmNewOptions newOptions = new frmNewOptions(dgcode, code);
                newOptions.Text += " ver." + newOptions.GetType().Assembly.GetName().Version.ToString()
                              + " db:" + WorkWithData.Connection.Database;
                newOptions.SearchForm = frmParent;
                newOptions.Show();
                //frmParent.Show();

                if (intro != null) intro.Close();
            }
        }
    }
}
