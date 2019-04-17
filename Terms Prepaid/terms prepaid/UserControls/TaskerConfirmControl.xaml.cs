using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace terms_prepaid.UserControls
{

    public partial class TaskerConfirmControl : UserControl
    {
        public bool ConfirmFlag;
        public bool CancelFlag;
        public bool DelayFlag;

        public DateTime DelayDate { get; set; }

        public System.Windows.Forms.Form ConfirmForm;


        public TaskerConfirmControl(string iTaskName)
        {
            //TaskName = iTaskName;
            //DelayDate = DateTime.Now.Date;

            InitializeComponent();

            tbkTaskName.Text = iTaskName;
            //dpDelayDate.SetValue("Value", DateTime.Now.Date);
            dpDelayDate.DataContext = this;
            //dpDelayDate.SelectedDateFormat = "dd.MM.yy";
            
            //dt_DatePayDeposit.Format = DateTimePickerFormat.Custom;
            //dt_DatePayDeposit.CustomFormat = "dd.MM.yyyy";

        }

        private void CloseForm()
        {
            if (ConfirmForm == null) return;

            ConfirmForm.Close();
        }

        public void YesButton_Click(object sender, EventArgs e)
        {
            ConfirmFlag = true;
            CancelFlag = false;
            DelayFlag = false;

            CloseForm();
        }

        public void NoButton_Click(object sender, EventArgs e)
        {
            ConfirmFlag = false;
            CancelFlag = true;
            DelayFlag = false;

            CloseForm();
        }

        public void DelayButton_Click(object sender, EventArgs e)
        {
            ConfirmFlag = false;
            CancelFlag = false;
            DelayFlag = true;

            CloseForm();
        }
    }
}
