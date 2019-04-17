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
        
        DataTable _taskTable = new DataTable();
        private void GetDate()
        {
            _taskTable = WorkWithData.GetDaysTasks();
            UpdateDataGrid();


        }
        
        private void UpdateDataGrid()
        {
            dgvTasks.DataSource = _taskTable;
            foreach (DataGridViewColumn column in dgvTasks.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "NAME":
                        column.HeaderText = "Задача";
                        break;
                    case "DG_CODE":
                        column.HeaderText = "Путевка";
                        break;
                    case "DATES":
                         column.HeaderText = "Дата";
                        column.DefaultCellStyle.Format = "dd.MM.yy HH:mm";
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }
        public ucDayTasks()
        {
            InitializeComponent();
           // GetDate();
           
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
                Form frmParent = parent as Form;
                frmParent.Hide();
                int code = (int)dgvTasks.Rows[e.RowIndex].Cells["code"].Value;
                string dgcode = dgvTasks.Rows[e.RowIndex].Cells["DG_CODE"].Value.ToString();
                new frmNewOptions(dgcode, code).ShowDialog();
                frmParent.Show();
            }
        }
    }
}
