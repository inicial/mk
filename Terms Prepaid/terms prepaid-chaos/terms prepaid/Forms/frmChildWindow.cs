using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Awesomium.Windows.Forms;


namespace terms_prepaid.Forms
{
    public partial class frmChildWindow : Form
    {
        public frmChildWindow()
        {
            InitializeComponent();
        }

        private void frmChildWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void Awesomium_Windows_Forms_WebControl_TargetURLChanged(object sender, Awesomium.Core.UrlEventArgs e)
        {
            if (string.IsNullOrEmpty(((WebControl) sender).HTML))
            {
                Process.Start(e.Url.ToString());
                this.Close();
            }
        }
    }
}
