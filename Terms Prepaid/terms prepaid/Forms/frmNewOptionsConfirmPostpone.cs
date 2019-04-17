using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace terms_prepaid.Forms
{
    public partial class frmNewOptionsConfirmPostpone : Form
    {
        public int StartPosX;
        public int StartPosY;

        public bool ConfirmFlag;
        public bool CancelFlag;

        public frmNewOptionsConfirmPostpone(string text)
        {
            InitializeComponent();

            string msg = "Заказ  будет  отложен.";
            msg = msg + (char)13 + (char)10;
            msg = msg + (char)13 + (char)10;
            msg = msg + "Отложить  заказ ?";
            msg = msg + (char)13 + (char)10;

            if (!string.IsNullOrEmpty(text)) msg = text;

            lbl_Message.Text = msg;
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            ConfirmFlag = true;
            CancelFlag = false;
            this.Close();
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            ConfirmFlag = false;
            CancelFlag = true;
            this.Close();
        }

        private void frmNewOptionsConfirmSave_Load(object sender, EventArgs e)
        {
            int x = this.Left;
            int y = this.Top;
            if (StartPosX > 0) x = StartPosX;
            if (StartPosY > 0) y = StartPosY;

            this.Left = x;
            this.Top = y;
        }
    }
}
