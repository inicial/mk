using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace lanta.Clients
{
    public partial class LoadPicture : Form
    {
        Stream myStream = null;
        SqlDataAdapter adapter;
        string keyField;
        string ImageField;
        DataTable tab = new DataTable("tab");
        public LoadPicture(string tabName,
            string  keyField,
            string NameField,
            string ImageField,            
            SqlDataAdapter adapter)
        {
            InitializeComponent();
            this.adapter = adapter;
            this.keyField = keyField;
            this.ImageField = ImageField;
            adapter.SelectCommand.CommandText = @"select " + keyField + "," + NameField + "," + ImageField + " from " + tabName + " order by "+NameField;
            adapter.Fill(tab);
            dataGridView_С.DataSource = tab;
        }
        private void AddPhoto(string Field)
        {
            DataRow dr = null;
            if (dataGridView_С.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView_С.SelectedRows[0];
                string  KEY = Convert.ToString(row.Cells[keyField].Value);
                DataRow[] drs = tab.Select(keyField+"="+KEY);
                if (drs.Length > 0)
                    dr = drs[0];
             }
            if (dr == null)
                return;
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            byte[] bufer;
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        bufer = new byte[myStream.Length];
                        using (myStream)
                        {
                            myStream.Read(bufer, 0, bufer.Length);
                            dr[Field] = bufer;
                            pictureBox_LCU_PHOTO.Image = new Bitmap(myStream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Фотография такого формата не может быть загружена. Исходная ошибка: " + ex.Message);
                }
            }
        }
        private void button_Open_Click(object sender, EventArgs e)
        {
            AddPhoto(ImageField);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.UpdateCommand.CommandText = @"UPDATE  [LCI_FlagImage] = @p3 WHERE [LCI_ISO] = @p4";
            adapter.Update(tab);
        }
    }
}
