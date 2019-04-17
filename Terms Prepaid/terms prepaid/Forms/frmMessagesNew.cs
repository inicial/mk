using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;

namespace terms_prepaid.Forms
{
    public partial class frmMessagesNew : Form
    {
        private int _x, _y, _w,_h;

        public frmMessagesNew(string dgcode, int x = 0, int y = 0, int h = 400, int w = 700)
        {
            InitializeComponent();
            
            _h = h;
            _w = w;
            _x = x;
            _y = y;

            var correspView = new CorrespondenceControl();
            correspView.CloseButtonClick += CloseButtonClick;
            var correspViewModel = new CorrespondenceViewModel(dgcode);
            correspView.DataContext = correspViewModel;
            correspView.InitializeComponent();
            MessagesHost.Child = correspView;
        }

        private void frmMessagesNew_Shown(object sender, EventArgs e)
        {
            this.SetBounds(_x, _y, _w, _h);
        }

        public void CloseButtonClick()
        {
            this.Close();
        }
    }
}
