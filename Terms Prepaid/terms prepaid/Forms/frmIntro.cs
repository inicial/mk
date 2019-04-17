using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace terms_prepaid
{
    public partial class frmIntro : Form
    {
        public frmIntro(string message)
        {
            InitializeComponent();

            string msg = "Подождите  пожалуйста,  идет  загрузка.";
            if (!string.IsNullOrEmpty(message)) msg = message;

            lbl_Message.Text = msg;
        }
    }
}
