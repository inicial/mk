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
    public partial class frmNewOptionsConfirmTasker : Form
    {
        public int StartPosX;
        public int StartPosY;

        public bool ConfirmFlag;
        public bool CancelFlag;
        public bool DelayFlag;
        

        public frmNewOptionsConfirmTasker(string TaskName)
        {
            InitializeComponent();

            string msg = "Задача:  " + TaskName.ToUpper() + (char)13 + (char)10;
            msg = msg + (char)13 + (char)10;
            msg = msg + "Вы хотите закрыть задачу как выполненную ?";
            msg = msg + (char)13 + (char)10;

            lbl_Message.Text = msg;

            dtpDelayDate.Format = DateTimePickerFormat.Custom;
            dtpDelayDate.CustomFormat = "dd.MM.yy";
            dtpDelayDate.Value = DateTime.Now.AddDays(1).Date;
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

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            ConfirmFlag = true;
            CancelFlag = false;
            DelayFlag = false;
            this.Close();
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            ConfirmFlag = false;
            CancelFlag = true;
            DelayFlag = false;
            this.Close();
        }

        private void btn_Delay_Click(object sender, EventArgs e)
        {
            ConfirmFlag = false;
            CancelFlag = false;
            DelayFlag = true;
            this.Close();
        }
    }
}
