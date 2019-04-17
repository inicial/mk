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
    public partial class frmNewOptionsConfirmSave : Form
    {
        public int StartPosX;
        public int StartPosY;

        public bool ConfirmFlag;
        public bool CancelFlag;

        public frmNewOptionsConfirmSave(string message = "", bool delete_flag = false, int height_shift = 0)
        {
            InitializeComponent();


            string msg = "Вы  произвели  изменения.";
            msg = msg + (char)13 + (char)10;
            msg = msg + (char)13 + (char)10;
            msg = msg + "Вы  проверили  корректность  внесенных  данных ?";
            msg = msg + (char)13 + (char)10;

            if (!string.IsNullOrEmpty(message)) msg = message;
            if (delete_flag)
            {
                btn_Yes.Text = "Да, удалить";
                btn_No.Text = "Нет, оставить";
            }

            lbl_Message.Text = msg;

            if (height_shift > 0)
            {
                Height = Height + height_shift;
                btn_Yes.Top = btn_Yes.Top + height_shift;
                btn_No.Top = btn_No.Top + height_shift;
                lbl_Message.Height = lbl_Message.Height + height_shift;
            }
        }

        public void SetYesText(string text)
        {
            btn_Yes.Text = text;
        }

        public void SetNoText(string text)
        {
            btn_No.Text = text;
        }

        public void SetOkMode(string title = "")
        {
            if (!string.IsNullOrEmpty(title)) this.Text = title;
            btn_No.Visible = false;
            btn_Yes.Text = "OK";
            btn_Yes.Left = btn_No.Left;
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
