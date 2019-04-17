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
    public partial class frmTouristsByItinerary : Form
    {
        public frmTouristsByItinerary()
        {
            InitializeComponent();
           // GetDate();
        }
        DataTable _turists = new DataTable();
        private bool _flag = false;
        void GetDate()
        {
            dtpBegin.Value = DateTime.Now.Date;
            dtpEnd.Value = DateTime.Now.Date;
            cbNeedCruise.Checked = false;
            tbTurist.Text = string.Empty;
            SetFilter();
        }
        void SetFilter()
        {
            _turists = WorkWithData.GetTuristsByItinerary(dtpBegin.Value, dtpEnd.Value, tbTurist.Text,tbDgCode.Text.ToUpper(),
                                                          cbNeedCruise.Checked);
            UpdateDataGridView();
        }
        
        void UpdateDataGridView()
        {
            dgvTurists.DataSource = _turists;
            foreach (DataGridViewColumn column in dgvTurists.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "turists":
                        column.HeaderText = "Туристы";
                        column.Width = 300;
                        column.DisplayIndex = 0;
                        break;
                    case "dg_code":
                        column.HeaderText = "Путевка";
                        column.Width = 100;
                        column.DisplayIndex = 1;
                        break;
                    case "dl_day":
                        column.HeaderText = "Дата";
                        column.Width = 80;
                        column.DisplayIndex = 2;
                        break;
                    case "duration":
                        column.HeaderText = "Н/Дн";
                        column.Width = 50;
                        column.DisplayIndex = 3;
                        break;
                    case "dl_name":
                        column.HeaderText = "Услуга";
                        column.Width = 300;
                        column.DisplayIndex = 4;
                        break;
                    case "dl_brutto":
                        column.HeaderText = "Цена";
                        column.Width = 50;
                        column.DisplayIndex = 5;
                        break;
                    case "pr_name":
                        column.HeaderText = "Партнер";
                        column.Width = 120;
                        column.DisplayIndex = 6;
                        break;
                    case "pr_phones":
                        column.HeaderText = "Телефон";
                        column.Width = 100;
                        column.DisplayIndex = 8;
                        break;
                    case "pr_email":
                        column.HeaderText = "E-mail";
                        column.Width = 100;
                        column.DisplayIndex = 7;
                        break;
                    case "client":
                        column.HeaderText = "Заказчик";
                        column.Width = 150;
                        column.DisplayIndex = 10;
                        break;
                    case "clientphone":
                        column.HeaderText = "Телефон клиента";
                        column.Width = 150;
                        column.DisplayIndex = 11;
                        break;
                    case "clientemail":
                        column.HeaderText = "E-mail клиента";
                        column.Width = 150;
                        column.DisplayIndex = 12;
                        break;
                    case "realizator":
                        column.HeaderText = "Реализатор";
                        column.Width = 150;
                        column.DisplayIndex = 13;
                        break;
                    case "broniro":
                        column.HeaderText = "Бронировщик";
                        column.Width = 150;
                        column.DisplayIndex = 14;
                        break;

                    default:
                        column.Visible = false;
                        break;
                }
            }
            _flag = false;
            //foreach (DataGridViewRow row in dgvTurists.Rows)
            //{

            //    if (row.Index == 0) continue;
            //    if ((dgvTurists).Rows[row.Index - 1].Cells["dg_code"].Value.ToString().Trim() !=
            //        row.Cells["dg_code"].Value.ToString().Trim())
            //    {
            //        _flag = !_flag;
            //    }
            //    if (_flag)
            //    {
            //       row.DefaultCellStyle.BackColor = Color.White;
            //    }
            //    else
            //    {
            //        row.DefaultCellStyle.BackColor = Color.LightGray;
            //    }
            //}

        }

        private void tbTurist_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void dtpBegin_ValueChanged(object sender, EventArgs e)
        {
            ChekDates();
            SetFilter();
        }

        void ChekDates()
        {
            if (dtpBegin.Value > dtpEnd.Value)
            {
                DateTime temp = dtpBegin.Value;
                dtpBegin.Value = dtpEnd.Value;
                dtpEnd.Value = temp;
            } 
        }
        private void cbNeedCruise_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void dgvTurists_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex == (sender as DataGridView).FirstDisplayedScrollingRowIndex) _flag = true;
            if (e.RowIndex > 0)
            {
                if ((sender as DataGridView).Rows[e.RowIndex - 1].Cells["dg_code"].Value.ToString().Trim() !=
                    (sender as DataGridView).Rows[e.RowIndex].Cells["dg_code"].Value.ToString().Trim())
                {
                    _flag = !_flag;
                }
                if (_flag)
                {
                    (sender as DataGridView).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    (sender as DataGridView).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void dgvTurists_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            
        }

        private void dgvTurists_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;


            if (IsRepeatedCellValue(e.RowIndex, e.ColumnIndex,(sender as DataGridView)))
            {
                e.Value = string.Empty;
                e.FormattingApplied = true;
            }
        }
        private bool IsRepeatedCellValue(int rowIndex, int colIndex,DataGridView dgv)
        {
            DataGridViewCell currCell = dgv.Rows[rowIndex].Cells[colIndex];
            DataGridViewCell prevCell = dgv.Rows[rowIndex - 1].Cells[colIndex];

            switch (dgv.Columns[colIndex].Name)
            {
                case "turists":
                    if (currCell.Value.Equals(prevCell.Value))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "dg_code":
                    if (currCell.Value.Equals(prevCell.Value) 
                        && dgv.Rows[rowIndex].Cells["turists"].Value.Equals(dgv.Rows[rowIndex-1].Cells["turists"].Value))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "PR_NAME":
                      
                    if (currCell.Value.Equals(prevCell.Value) 
                        && dgv.Rows[rowIndex].Cells["turists"].Value.Equals(dgv.Rows[rowIndex-1].Cells["turists"].Value)
                        && dgv.Rows[rowIndex].Cells["DG_CODE"].Value.Equals(dgv.Rows[rowIndex - 1].Cells["DG_CODE"].Value))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "client":
                case "realizator":
                case "broniro":

                    if (currCell.Value.Equals(prevCell.Value)
                        && dgv.Rows[rowIndex].Cells["turists"].Value.Equals(dgv.Rows[rowIndex - 1].Cells["turists"].Value)
                        && dgv.Rows[rowIndex].Cells["DG_CODE"].Value.Equals(dgv.Rows[rowIndex - 1].Cells["DG_CODE"].Value))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "clientphone":
                case "clientEMAIL":

                    if (currCell.Value.Equals(prevCell.Value)
                        && dgv.Rows[rowIndex].Cells["turists"].Value.Equals(dgv.Rows[rowIndex - 1].Cells["turists"].Value)
                        && dgv.Rows[rowIndex].Cells["client"].Value.Equals(dgv.Rows[rowIndex - 1].Cells["client"].Value)
                        && dgv.Rows[rowIndex].Cells["DG_CODE"].Value.Equals(dgv.Rows[rowIndex - 1].Cells["DG_CODE"].Value))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    


                    break;
                case "PR_PHONES":
                case "PR_email":

                    if (currCell.Value.Equals(prevCell.Value)
                        && dgv.Rows[rowIndex].Cells["turists"].Value.Equals(dgv.Rows[rowIndex - 1].Cells["turists"].Value)
                        && dgv.Rows[rowIndex].Cells["pr_name"].Value.Equals(dgv.Rows[rowIndex - 1].Cells["pr_name"].Value)
                        && dgv.Rows[rowIndex].Cells["DG_CODE"].Value.Equals(dgv.Rows[rowIndex - 1].Cells["DG_CODE"].Value))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
                    break;
            }
            
        }

        private void dgvTurists_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            // Пропуск заголовков колонок и строк, и первой строки
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;

            if (IsRepeatedCellValue(e.RowIndex, e.ColumnIndex,(sender as DataGridView)))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            }
        }

        private void frmTouristsByItinerary_Shown(object sender, EventArgs e)
        {
            GetDate();
        }

        private void dgvTurists_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTurists.Columns[e.ColumnIndex].Name == "dg_code" &&e.RowIndex>=0)
            {
                this.Hide();
                new frmNewOptions(dgvTurists.Rows[e.RowIndex].Cells["dg_code"].Value.ToString(),4).ShowDialog();
                this.Show();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void btnParner_Click(object sender, EventArgs e)
        {
            new frmSearchPartner().ShowDialog(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTurists_Click(object sender, EventArgs e)
        {
            new frmSearchTurist().ShowDialog();
        }
    }
}
