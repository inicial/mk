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
    public partial class frmMessages : Form
    {
        private int _x, _y, _w,_h;
       
        public frmMessages(string dgcode,int x=0,int y=0,int h=400,int w=700)
        {
            InitializeComponent();
            this.Controls.Add(new ucMessages(dgcode)
            {
                Dock = DockStyle.Fill
            });
            _h = h;
            _w = w;
            _x = x;
            _y = y;
            

        }

        private void frmMessages_Load(object sender, EventArgs e)
        {

        }

        private void frmMessages_Shown(object sender, EventArgs e)
        {
            this.SetBounds(_x, _y, _w, _h);
        }
    }
}
