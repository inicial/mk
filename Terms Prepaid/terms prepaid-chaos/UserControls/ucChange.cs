using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace terms_prepaid.UserControls
{
    public partial class ucChange : UserControl
    {
        private string _dgcode,_text;
        private int _type = 0;
        public ucChange(string dgCode,string text,int type)
        {
            _dgcode = dgCode;
            _text = text;
            _type = type;
            InitializeComponent();
            lbDgCode.Text = _dgcode;
            lbText.Text = _text;

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Control tObj = this.Parent as Control;
            while ((tObj as Form)==null)
            {
                tObj = tObj.Parent;
            }
            Form tForm = tObj as Form;
            tForm.TopMost = false;
            new frmNewOptions(_dgcode,_type).ShowDialog();
            tForm.TopMost = true;
        }
    }
}
