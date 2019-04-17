using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;


namespace terms_prepaid.Forms
{
    public partial class frmAnnulateJornal : Form
    {
       DataTable _journal = new DataTable();
        public frmAnnulateJornal()
        {
            InitializeComponent();
            GetDate();
        }

        private void GetDate()
        {
            _journal = WorkWithData.GetAnnulateJournal(!cbIsOk.Checked,!cbIsCalc.Checked);
            _journal.Columns.Add("Petition", typeof (Image));
            _journal.Columns.Add("Ok", typeof (Image));
            _journal.Columns.Add("Ok2", typeof(Image));
            _journal.Columns.Add("Calc", typeof (Image));
            foreach (DataRow row in _journal.Rows)
            {
                row["Petition"] = terms_prepaid.Properties.Resources.view;
                if (row["CloseManag"] == DBNull.Value)
                {
                    if (row["MANAGE_OK"] == DBNull.Value)
                    {
                        row["ok"] = terms_prepaid.Properties.Resources.empty;

                    }
                    else
                    {
                        row["ok"] = terms_prepaid.Properties.Resources.cheked;
                    }
                    
                    if (row["MANAGE_CALCULATE"] == DBNull.Value)
                    {
                        row["Calc"] = terms_prepaid.Properties.Resources.empty;

                    }
                    else
                    {
                        row["Calc"] = terms_prepaid.Properties.Resources.cheked;
                    }
                   
                    
                    if (row["BronOk"] == DBNull.Value)
                    {
                        row["ok2"] = terms_prepaid.Properties.Resources.empty;

                    }
                    else
                    {
                        row["ok2"] = terms_prepaid.Properties.Resources.cheked;
                    }

                    //if (
                    //    !(new AccessClass(WorkWithData.Connection).isBronir ||
                    //      new AccessClass(WorkWithData.Connection).isSuperViser))
                    //{
                    //    _journal.Columns.Remove("ok2");
                    //}
                }
                else
                {
                    row["Calc"] = terms_prepaid.Properties.Resources.empty;
                    row["ok2"] = terms_prepaid.Properties.Resources.empty;
                    row["ok"] = terms_prepaid.Properties.Resources.empty;
                }
            }
            UpdateDataGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAnnulateJornal_Shown(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            dgvJornal.DataSource = _journal;
            foreach (DataGridViewColumn column in dgvJornal.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "AN_KEY":
                        column.DisplayIndex = 0;
                        column.Width = 90;
                        column.HeaderText = "Номер заявления";
                        break;
                    case "DG_CODE":
                        column.DisplayIndex = 1;
                        column.Width = 110;
                        column.HeaderText = "Путевка";
                        break;
                    case "DATE_OF_CREATE":
                        column.DisplayIndex = 2;
                        column.Width = 90;
                        column.DefaultCellStyle.Format = "dd.MM.yy HH.mm";
                        column.HeaderText = "Дата заявки на аннуляцию";
                        break;
                    case "REASON_TEXT":
                        column.DisplayIndex = 3;
                        column.Width = 250;
                        column.HeaderText = "Причина";
                        break;
                    case "IS_FULL":
                        column.DisplayIndex = 4;
                        column.Width = 80;
                        column.HeaderText = "Польностью";
                        break;
                    case "NAME":
                        column.DisplayIndex = 1;
                        column.Width = 300;
                        column.HeaderText = "Аннулирующий";
                        break;
                    case "OK":
                        column.DisplayIndex = 6;
                        column.Width = 80;
                        column.HeaderText = "Взять в работу";
                        break;
                    case "OK2":
                        column.DisplayIndex = 6;
                        column.Width = 80;
                        column.HeaderText = "Взять в работу(бронировщик)";
                        break;
                    case "CALC":
                        column.DisplayIndex = 6;
                        column.Width = 80;
                        column.HeaderText = "Выставить калькуляцию";
                        break;
                    case "PT_NAME":
                        column.DisplayIndex = 7;
                        column.Width = 150;
                        column.HeaderText = "Тип заявления";
                        break;
                    case "PETITION":
                        column.DisplayIndex = 8;
                        column.Width = 70;
                        column.HeaderText = "Заявление";
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }

        }

        private void dgvJornal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int ankey = (int) dgvJornal.Rows[e.RowIndex].Cells["AN_KEY"].Value;
                switch (dgvJornal.Columns[e.ColumnIndex].Name.ToUpper())
                {
                    case "DG_CODE":
                        
                            this.Hide();
                            new frmNewOptions(dgvJornal.Rows[e.RowIndex].Cells["DG_CODE"].Value.ToString(), 5).ShowDialog();
                            this.Show();
                        break;
                    case "PETITION":


                        byte[] data = WorkWithData.GetPetition(ankey);
                        string filename = Path.GetTempPath() + Path.PathSeparator + "Petition" + ankey.ToString() +
                                          ".pdf";
                        BinaryWriter br = new BinaryWriter(File.Create(filename));
                        br.Write(data);
                        br.Close();
                        Process.Start(filename);

                        break;
                    //case "OK":
                    //    if ((dgvJornal.Rows[e.RowIndex].Cells["CloseManag"].Value) != DBNull.Value)return;
                    //    if (
                    //        MessageBox.Show("Вы хотите взять в работу заявление на аннуляцию №" + ankey.ToString(), "",
                    //                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //    {
                    //        if ((dgvJornal.Rows[e.RowIndex].Cells["MANAGE_OK"].Value) == DBNull.Value)
                    //            WorkWithData.UpdateAnnulateOk(ankey);
                    //    }
                    //    break;
                    //case "OK2":
                    //    if ((dgvJornal.Rows[e.RowIndex].Cells["CloseManag"].Value) != DBNull.Value) return;
                    //    if (
                    //        MessageBox.Show("Вы хотите взять в работу как бронировщик заявление на аннуляцию №" + ankey.ToString(), "",
                    //                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //    {
                    //        if (((dgvJornal.Rows[e.RowIndex].Cells["bronOK"].Value).Equals(DBNull.Value)) && (new AccessClass(WorkWithData.Connection).isBronir || new AccessClass(WorkWithData.Connection).isSuperViser))
                    //            WorkWithData.UpdateAnnulateBronOk(ankey);
                    //    }
                    //    break;
                    //case "CALC":
                    //    if ((dgvJornal.Rows[e.RowIndex].Cells["CloseManag"].Value) != DBNull.Value) return;
                    //    if (
                    //        MessageBox.Show("Вы выставили калькуляцию по заявлению №" + ankey.ToString(), "",
                    //                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //    {
                    //        try
                    //        {
                    //            if (dgvJornal.Rows[e.RowIndex].Cells["MANAGE_CALCULATE"].Value.Equals(DBNull.Value) &&
                    //                !dgvJornal.Rows[e.RowIndex].Cells["MANAGE_OK"].Equals(DBNull.Value))
                    //                WorkWithData.UpdateAnnulateCalculate(ankey);

                    //        }
                    //        catch (Exception)
                    //        {


                    //        }

                    //    }
                    //    break;
                
          
                    default:
                        break;
                        
                }
            }
        }

        private void dgvJornal_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = dgvJornal.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                dgvJornal.Rows[index].HeaderCell.Value = indexStr; 
        }

        private void cbIsOk_CheckedChanged(object sender, EventArgs e)
        {
            GetDate();
        }

        private void cbIsCalc_CheckedChanged(object sender, EventArgs e)
        {
            GetDate();
        }

        private void frmAnnulateJornal_Load(object sender, EventArgs e)
        {

        }
    }
}
