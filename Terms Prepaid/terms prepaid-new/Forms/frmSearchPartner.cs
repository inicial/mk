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
    public partial class frmSearchPartner : Form
    {
        DataTable _partners = new DataTable();
        public frmSearchPartner(string parnerName="")
        {
            
            InitializeComponent();
            tbSearchString.Text = parnerName;
            SetFilter();
        }

        private void tbSearchString_TextChanged(object sender, EventArgs e)
        {
           
            SetFilter();
        }

        private void SetFilter()
        {
            _partners = WorkWithData.FindPartners(tbSearchString.Text.Trim());
            UpdateDataGrid();

        }

        private void UpdateDataGrid()
        {
            dgvPartners.DataSource = _partners;
            foreach (DataGridViewColumn column in dgvPartners.Columns)
            {
                switch (column.Name.ToUpper())
                { 
                    case "NAME":
                        column.HeaderText = "Наименование партнера";
                        break;
                    default:
                        column.Visible = false;
                        break;



                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void dgvPartners_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int prkey = (int)dgvPartners.Rows[e.RowIndex].Cells["PR_KEY"].Value;
                new frmParner(prkey).ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
