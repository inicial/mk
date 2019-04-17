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
    public partial class frmNewOptionsConfirmUndo : Form
    {
        public int StartPosX;
        public int StartPosY;

        public bool ConfirmFlag;
        public bool CancelFlag;

        public frmNewOptionsConfirmUndo()
        {
            InitializeComponent();


            string msg = "Отменить  все  изменения ?";
            msg = msg + (char)13 + (char)10;

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
