using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.UserControls;

namespace terms_prepaid.Forms
{
    public partial class frmNewOptionsConfirmTaskerWPF : Form
    {
        public int StartPosX;
        public int StartPosY;

        public bool ConfirmFlag;
        public bool CancelFlag;
        public bool DelayFlag;

        public DateTime DelayDate;

        TaskerConfirmControl Confirm;


        public frmNewOptionsConfirmTaskerWPF(string iTaskName)
        {
            //dtpDelayDate.Format = DateTimePickerFormat.Custom;
            //dtpDelayDate.CustomFormat = "dd.MM.yy";
            //dtpDelayDate.Value = DateTime.Now.AddDays(1).Date;

            InitializeComponent();

            DelayDate = DateTime.Now.Date;

            Confirm = new TaskerConfirmControl(iTaskName.ToUpper());
            Confirm.DelayDate = DelayDate;
//            Tasker.CloseTask = ListCloseTask;
//            TaskerModel = new TaskerViewModel();
            Confirm.ConfirmForm = this;
            //TaskerModel.SourceTaskList = iTasks;
            Confirm.DataContext = this;
            ConfirmControl.Child = Confirm;
        }

        private void frmNewOptionsTasker_Load(object sender, EventArgs e)
        {
            int x = this.Left;
            int y = this.Top;
            if (StartPosX > 0) x = StartPosX;
            if (StartPosY > 0) y = StartPosY;

            this.Left = x;
            this.Top = y;
        }

        private void frmNewOptionsTask_Deactivate(object sender, EventArgs e)
        {

        }

        private void frmNewOptionsConfirmTaskerWPF_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmFlag = Confirm.ConfirmFlag;
            CancelFlag = Confirm.CancelFlag;
            DelayFlag = Confirm.DelayFlag;
            DelayDate = Confirm.DelayDate;
        }
    }
}
