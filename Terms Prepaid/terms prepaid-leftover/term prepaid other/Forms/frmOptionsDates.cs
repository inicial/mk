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

namespace terms_prepaid.Forms
{
    public partial class frmOptionsDates : Form
    {
        
        private DataTable _optionsTable =new DataTable();
        private string _dgcode;
        private int _x = 0, _y = 0;

        public frmOptionsDates(string DG_code,int x,int y)
        {
            InitializeComponent();
            _dgcode = DG_code;
            _x = x;
            _y = y;
           // this.SetBounds(x,y,this.Width,this.Height); 
            GetDate();
            UpdateDataGrid();
        }
        void GetDate()
        {
            _optionsTable = WorkWithData.GetOptionDates(_dgcode);

        }
        void UpdateDataGrid()
        {
            dgvOptions.DataSource = _optionsTable;
            foreach (DataGridViewColumn column in dgvOptions.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "NAME" :
                        column.DisplayIndex = 0;
                        column.HeaderText = "Услги(" + _optionsTable.Rows.Count.ToString() + ")";
                        break;
                    case "DATE":
                        column.DisplayIndex = 1;
                        column.HeaderText = "Дата";
                        column.DefaultCellStyle.Format = "dd.MM.yy";
                        break;
                    case "TIME":
                        column.DisplayIndex = 2;
                        column.HeaderText = "Время";
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            } 
        }

        private void dgvOptions_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = ((DataGridView)sender).Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                ((DataGridView)sender).Rows[index].HeaderCell.Value = indexStr; 

        }

        private void frmOptionsDates_Shown(object sender, EventArgs e)
        {
            if (_optionsTable.Rows.Count < 1)
            {
                this.Close();
            }
            else
            {
                int i = 0;
                foreach (DataGridViewColumn column in dgvOptions.Columns)
                {
                    i += column.Width + 1;
                }
                int w = i+57;
                int h = dgvOptions.Rows.Count*25 + 60;
                if ((Screen.PrimaryScreen.Bounds.Width - w) < _x) _x = Screen.PrimaryScreen.Bounds.Width - w;
                if ((Screen.PrimaryScreen.Bounds.Height - h) < _y) _y = Screen.PrimaryScreen.Bounds.Height - h;
                this.SetBounds(_x,_y,w,h);
            }
        }
    }
}
