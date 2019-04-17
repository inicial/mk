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
    public partial class frmChangesTurist : Form
    {
        private int _id;
        private DataTable _changes;
        public frmChangesTurist(int idChange)
        {
            _id = idChange;
            InitializeComponent();
            GetDate();
        }
        void GetDate()
        {
            _changes = WorkWithData.GetTuristChanges(_id);
           // UpdateDataGrid();
        }
        void UpdateDataGrid()
        {
            dgvChanges.DataSource = _changes;
            foreach (DataGridViewColumn  column in dgvChanges.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "HI_TEXT":
                        column.DisplayIndex = 0;
                        column.HeaderText = "Турист";
                        column.Width = 150;
                        break;
                    case "HD_TEXT":
                        column.DisplayIndex = 1;
                        column.HeaderText = "";
                        column.Width = 150;
                        break;
                    case "REMARK1":
                        column.DisplayIndex = 2;
                        column.HeaderText = "Старое значение";
                        column.Width = 150;
                        break;
                    case "REMARK2":
                        column.DisplayIndex = 3;
                        column.HeaderText = "Новое значение";
                        column.Width = 150;
                        break;
                    default:
                        column.Visible = false;
                        break;

                }
            }
        }

        private void frmChangesTurist_Shown(object sender, EventArgs e)
        {
            UpdateDataGrid();
            this.Height = dgvChanges.ClientSize.Height;
        }
    }
}
