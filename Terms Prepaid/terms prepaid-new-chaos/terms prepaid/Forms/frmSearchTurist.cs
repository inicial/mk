using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid.Forms
{
    public partial class frmSearchTurist : Form
    {
        private DataTable _turists;
        public frmSearchTurist(string turist="")
        {
            InitializeComponent();
            tbSearchString.Text = turist;
        }
        void SetFilter()
        {

            _turists = WorkWithData.FindTurists(tbSearchString.Text.Trim());
            UpdateDataGrid();
            
        }
        void UpdateDataGrid()
        {
            dgvTurists.DataSource = _turists;
            foreach (DataGridViewColumn column in dgvTurists.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "NAME" :
                        {
                            column.HeaderText = "ФИО туриста";
                        }
                        break;
                    case "DG_CODE":
                        {
                            column.HeaderText = "Путевка";
                        }
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }
        private void tbSearchString_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void dgvTurists_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                switch (dgvTurists.Columns[e.ColumnIndex].Name.ToUpper())
                {
                    case "NAME":
                        int tukey = (int)dgvTurists.Rows[e.RowIndex].Cells["tu_key"].Value;
                        new frmQuestionnaire(tukey, 2).ShowDialog();
                        break;
                    case "DG_CODE":
                        string dg_code = (string) dgvTurists.Rows[e.RowIndex].Cells["DG_CODE"].Value;
                        new frmNewOptions(dg_code,4).ShowDialog();
                        break;
                }
            

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
