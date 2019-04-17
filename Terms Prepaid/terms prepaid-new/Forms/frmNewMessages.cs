using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using terms_prepaid.Helpers;

namespace terms_prepaid
{
    public partial class frmNewMessages : Form
    {
        private DataTable _dataTable = WorkWithData.GetMessageDogovors();
        Button GetButton(string dgCode)
        {
            Button btn = new Button();
            btn.Dock = DockStyle.Fill;
            btn.Tag = dgCode;
            btn.Image = Properties.Resources.mailIconMin;
            btn.Click += btn_Click;
            btn.ImageAlign=ContentAlignment.MiddleCenter;
           // btn.Image
            return btn;
        }
        Label GetLabel(string text)
        {
            Label lab = new Label();
            lab.Dock= DockStyle.Fill;
            lab.Text = text;
            return lab;

        }
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string dgCod = btn.Tag as string;
           // frmMessages mess = new frmMessages(dgCod);
          //  mess.ShowDialog();
            GetData();
        }
 
        void GetData()
        {
            int i = 0;
            _dataTable = WorkWithData.GetMessageDogovors();
            tableLayoutPanel1.Height =0;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            tableLayoutPanel1.Controls.Clear();
            foreach (DataRow row in _dataTable.Rows)
            {
                string dgCode = row.Field<string>("Hi_dgcod");
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.Height += 27;
                tableLayoutPanel1.RowStyles.Insert(0,new RowStyle(SizeType.Absolute, 27));
                tableLayoutPanel1.Controls.Add(GetLabel(dgCode), 0, i);
                tableLayoutPanel1.Controls.Add(GetButton(dgCode),1,i);
                i++;
            }
            tableLayoutPanel1.Height += 2;
            this.Refresh();
        }
        public frmNewMessages()
        {
            InitializeComponent();
            GetData();
        }
    }
}
