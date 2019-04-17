using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RingsFromSite.Properties;

namespace RingsFromSite
{
    public partial class frmJournal : Form
    {
        private DataTable _journal;
        private WorkWithData wwr;
        private SqlConnection _connection;
        private int[] closeStatuses = new int[]{3,4};
        public frmJournal(SqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            wwr = new WorkWithData(_connection);
            
            GetDate();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            SetFilter();

        }

        void SetFilter()
        {
            _journal = wwr.GetRingsJournal(dtpCreateBeg.Value.Date, dtpCreateEnd.Value.Date, (int)cbStatus.SelectedValue);
            _journal.Columns.Add("settings", typeof (Image));
            foreach (DataRow row in _journal.Rows)
            {
                if (Array.Exists(closeStatuses, x => x == row.Field<int>("status")))
                {
                    row["settings"] = Resources.empty;
                }
                else
                {
                    row["settings"] = Resources.gear;
                }
            }
            UpdateDataGrid();
        }
        private void GetDate()
        {

            cbStatus.DataSource = wwr.GetAllStatuses();
            cbStatus.ValueMember = "id";
            cbStatus.DisplayMember = "name_ru";
            dtpCreateBeg.Value = DateTime.Now.Date;
            dtpCreateEnd.Value = DateTime.Now.Date;
            cbStatus.SelectedValue = -1;
        }
        void UpdateDataGrid()
        {
            dgvJournal.DataSource = _journal;
            foreach (DataGridViewColumn column in dgvJournal.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "DATE_CREATE":
                        column.HeaderText = "Дата создание";
                        column.DisplayIndex = 0;
                        column.Width = 90;
                        column.DefaultCellStyle.Format = "dd.MM.yy HH:mm";
                        break;
                    case "PHONE":
                        column.HeaderText = "Телефон";
                        column.DisplayIndex = 1;
                        column.Width = 90;
                        break;
                    case "TIME_FOR_RING":
                        column.HeaderText = "Время для звонка";
                        column.DisplayIndex = 2;
                        column.Width = 90;
                        column.DefaultCellStyle.Format = "dd.MM.yy HH:mm";
                        break;
                    case "NAME_RU":
                        column.HeaderText = "Статус";
                        column.DisplayIndex = 3;
                        column.Width = 120;
                        break;
                    case "URL_FROM_RING":
                        column.HeaderText = "Место клиента на сайте"; 
                        column.DisplayIndex = 4;
                        column.Width = 90;
                        column.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Underline);
                        column.DefaultCellStyle.ForeColor = Color.Blue;
                        break;
                    case "SETTINGS":
                        column.HeaderText = "Сменить статус";
                        column.DisplayIndex = 4;
                        column.Width = 90;
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }

        private void frmJournal_Shown(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void dgvJournal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int idRing = (int)dgvJournal.Rows[e.RowIndex].Cells["id"].Value;
                int status = (int)dgvJournal.Rows[e.RowIndex].Cells["status"].Value;
                switch (dgvJournal.Columns[e.ColumnIndex].Name.ToUpper())
                {
                    case "URL_FROM_RING":
                        Process.Start((string)dgvJournal.Rows[e.RowIndex].Cells["URL_FROM_RING"].Value);
                        break;
                    case "SETTINGS":
                        if (!Array.Exists(closeStatuses,x=>x== status))
                        {
                            new frmChangeStatus(idRing, _connection).ShowDialog();
                            SetFilter();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
