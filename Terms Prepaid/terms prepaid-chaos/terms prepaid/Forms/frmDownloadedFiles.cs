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
using DocomentServices;
using terms_prepaid.Helpers;
using terms_prepaid.Helper_Classes;
using lanta.SQLConfig;

namespace terms_prepaid.Forms
{
    public partial class frmDownloadedFiles : Form
    {
        private string _dgCode;
        private DataTable _DownloadedFilesTable;
        private string _tempDirectory = Path.GetTempPath();
        public frmDownloadedFiles(string dgcode)
        {
            InitializeComponent();
            _dgCode = dgcode;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            GetDate();
        }
        void GetDate()
        {
            _DownloadedFilesTable = WorkWithData.GetDownloadedFiles(_dgCode);
            _DownloadedFilesTable.Columns.Add("view", typeof (Image));
            _DownloadedFilesTable.Columns.Add("ChangeStatus", typeof(Image));
            foreach (DataRow row in _DownloadedFilesTable.Rows)
            {
                row["view"] = Properties.Resources.view;
                row["ChangeStatus"] = Properties.Resources.gear;

            }
            UpdateDataGrid();
        }
        void UpdateDataGrid()
        {
            dgvDownloadedFiles.DataSource = _DownloadedFilesTable;
            foreach (DataGridViewColumn column in dgvDownloadedFiles.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "NAME":
                        column.DisplayIndex = 0;
                        column.HeaderText = "Наименование документа";
                        column.Width = 400;
                        break;
                    case "TURISTS":
                        column.DisplayIndex = 1;
                        column.HeaderText = "Туристы";
                        column.Width = 400;
                        break;
                    case "STATUSDOCUMENT":
                        column.DisplayIndex = 2;
                        column.HeaderText = "Статус";
                        column.Width = 300;
                        break;
                    case "VIEW":
                        column.DisplayIndex = 3;
                        column.HeaderText = "Посмотреть";
                        column.Width = 70;
                        break;
                    case "CHANGESTATUS":
                        column.DisplayIndex = 4;
                        column.HeaderText = "Сменить статус";
                        column.Width = 120;
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDownloadedFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex>=0&& e.RowIndex>=0)
            {
                switch (dgvDownloadedFiles.Columns[e.ColumnIndex].Name.ToUpper())
                {
                    case "VIEW":
                        string[] strs =
                            dgvDownloadedFiles.Rows[e.RowIndex].Cells["pad_FileName"].Value.ToString().Split('/');
                         lanta.SQLConfig.Config_XML xmlConfig = new Config_XML();
                         WorkWithFTP ftp = new WorkWithFTP(xmlConfig.Get_Value("appSettings","ftp"));
                        string filename = "";
                        string error = "";
                        WorkWithFTP.FTP_ERROR errorCod = ftp.Download("//files//"+_dgCode, strs.Last(), _tempDirectory,out error,out filename);
                        if (errorCod != WorkWithFTP.FTP_ERROR.ERROR_NO)
                        {
                            MessageBox.Show(error);
                        }
                        else
                        {
                             Process.Start(filename);
                        }
                       
                        break;
                    case "CHANGESTATUS":
                        new frmStatusFileSetting(dgvDownloadedFiles.Rows[e.RowIndex].Cells["pad_FileName"].Value.ToString()).ShowDialog();
                        GetDate();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
