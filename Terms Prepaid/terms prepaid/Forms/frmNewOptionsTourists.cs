using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.Common;
using WpfControlLibrary.View;


namespace terms_prepaid.Forms
{
    public partial class frmNewOptionsTourists : Form
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;

        private List<NameValue> TouristsList;

        public frmNewOptionsTourists(int iX, int iY, List<NameValue> iTouristsList, string iTitle)
        {
            StartPosX = iX;
            StartPosY = iY;

            InitializeComponent();

            if (iTitle != null)
                this.Text = iTitle;

            TouristsList = iTouristsList;

            TouristsControl TouristsCont = new TouristsControl();
            TouristsCont.TouristsList.ItemsSource = TouristsList;
            TouristListHost.Child = TouristsCont;
        }

        private void frmNewOptionsContacts_Load(object sender, EventArgs e)
        {
            this.Left = StartPosX;
            this.Top = StartPosY;

            int ch = TouristListHost.Height;
            int lh = 18; // 16;
            int h = 1 * lh;
            if (TouristsList != null)
                if (TouristsList.Count > 1)
                    h = TouristsList.Count * lh;
            this.Height = this.Height + (h + 4 - ch);
        }

        private void frmNewOptionsContacts_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
