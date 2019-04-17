using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Rep10027.Helpers;

namespace terms_prepaid
{
    public partial class frmFileOptions : Form
    {
        private int optionid;
        DataTable _filesOption = new DataTable();
        public frmFileOptions(int option_id)
        {
            InitializeComponent();
            optionid = option_id;
            GetListFiles();
        }
        void GetListFiles()
        {
            SqlCommand com = new SqlCommand(@"select OPF_Id,OPF_file_name,OPF_Descruipt from mk_option_files where OPF_option_key=" + optionid.ToString(), WorkWithData.Connection);
            SqlDataAdapter ada = new SqlDataAdapter(com);
            _filesOption.Rows.Clear();
            ada.Fill(_filesOption);
            UpdateDataGrid();

        }
        void UpdateDataGrid()
        {
            dgvFiles.DataSource = _filesOption;
            foreach (DataGridViewColumn column in dgvFiles.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "opf_file_name":
                        {
                            column.HeaderText = "Имя файла";
                            column.DisplayIndex = 0;
                        }

                        break;
                    case "opf_descruipt":
                        {
                            column.HeaderText = "Описание";
                            column.DisplayIndex = 1;
                        }
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }

        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
           if (dgvFiles.SelectedRows.Count < 1)
           {
               MessageBox.Show("Сначало выберете файл!");
           }
            int file_id = Convert.ToInt32(dgvFiles.SelectedRows[0].Cells["OPF_Id"].Value);
            SqlCommand com = new SqlCommand(@"select OPF_file_date,OPF_file_name from mk_option_files where  OPF_Id= " + file_id.ToString(), WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            byte[] arrBy = dt.Rows[0].Field<byte[]>("OPF_file_date");
            string filename = dt.Rows[0].Field<string>("OPF_file_name");
            Stream st = new MemoryStream(arrBy);
            FileInfo fi = new FileInfo(filename);
            string ext = fi.Extension;

            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = ext;
            sd.FileName = filename;
            if(sd.ShowDialog()==DialogResult.OK)
            {
                File.WriteAllBytes(sd.FileName,arrBy);
            }       
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand(@"insert into mk_option_files(OPF_Descruipt,OPF_file_date,OPF_file_name,OPF_option_key,OPF_Who,OPF_LastUpdate) values (@Descript,@file,@filename,@optionid,(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())
                                         ,getdate()) ", WorkWithData.Connection);
            com.Parameters.AddWithValue("@optionid", optionid);
            OpenFileDialog opf = new OpenFileDialog();
            String filename;
            if (opf.ShowDialog() == DialogResult.OK)
            {
                filename = opf.FileName;
            }
            else
            {
                MessageBox.Show("Произошла ошибка при выборе файла");
                return;
            }
            byte[] file = File.ReadAllBytes(filename);
            FileInfo infoFile = new FileInfo(filename);
            string descript = Interaction.InputBox("Введите описание файла"," ");
            com.Parameters.AddWithValue("@file", file);
            com.Parameters.AddWithValue("@filename", infoFile.Name);
            com.Parameters.AddWithValue("@Descript", descript);
            com.ExecuteNonQuery();
            GetListFiles();
        }
    }
}
