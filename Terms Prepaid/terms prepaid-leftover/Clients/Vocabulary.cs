using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace lanta.Clients
{
    public partial class Vocabulary: Form
    {
        DataTable vocab = new DataTable();
        string table = "Clients";
        string vocabName="";
        ArrayList vocabFieldNames = new ArrayList();
        string vocabKeyName,vocabFilterName;
        string selectedFields="";
        SqlConnection connection;
        public Vocabulary()
        {
            InitializeComponent();
        }
        public Vocabulary(string table, SqlConnection connection)
        {
            InitializeComponent();
            this.table = table;
            this.connection = connection;
            switch (table)
            {
                case "Profession":
                    vocabName = "Список профессий";
                    vocabKeyName = "PF_KEY";
                    vocabFilterName = "PF_NAME";
                    vocabFieldNames.Add(new string[]{"PF_KEY", "Код"});
                    vocabFieldNames.Add(new string[]{"PF_NAME", "Название"});
                    vocabFieldNames.Add(new string[]{"PF_NameLat", "Английское название"});
                    vocabFieldNames.Add(new string[]{"PF_CODE", "Сокращённое название"});
                    break;
                default:
                    break;

            }
            this.Text += " - " + vocabName;

            DataGridViewTextBoxColumn dc = null;
            foreach (string[] de in vocabFieldNames)
            {
                dc = new DataGridViewTextBoxColumn();
                dc.HeaderText = Convert.ToString(de[1]);//.Value
                dc.Name = Convert.ToString(de[0]);//.Key
                dc.DataPropertyName = dc.Name;
                dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (dc.Name == vocabKeyName)
                {
                    //dc.Visible = false;
                }
                dataGridView1.Columns.Add(dc);
                selectedFields += dc.Name + ",";
            }
           dataGridView1.AutoResizeColumns();
           selectedFields =  selectedFields.TrimEnd(',');
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                vocab.Rows.Clear();
                //using (SqlConnection connection = new SqlConnection(global::lanta.Clients.Properties.Settings.Default.ConnectString))
                //{
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(
                        "select " + selectedFields + " from " + table, connection);
                    adapter.Fill(vocab);
                //}
                dataGridView1.DataSource = vocab;
            }
            finally { Cursor.Current = Cursors.Arrow; }


        }

        private void ClientsMainForm_Load(object sender, EventArgs e)
        {
             RefreshData();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)//Сохранение
        {
            //using (SqlConnection connection = new SqlConnection(global::lanta.Clients.Properties.Settings.Default.ConnectString))
           // {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("select " + selectedFields + " from " + table, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.Update(vocab);
            //}
            MessageBox.Show("Данные сохранены успешно!");
            this.DialogResult = DialogResult.OK;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                DataRow[] drs = vocab.Select(vocabFilterName+" like '" + textBox1.Text + "%'");
                DataTable dt = vocab.Clone();
                foreach (DataRow dr in drs)
                    dt.Rows.Add(dr.ItemArray);
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = vocab;
            }
        }
    }
}
