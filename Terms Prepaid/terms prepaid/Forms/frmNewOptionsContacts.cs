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
    public partial class frmNewOptionsContacts : Form
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;

        private List<NameValue> ContactsList;

        public frmNewOptionsContacts(int iX, int iY, List<NameValue> iContactsList, string iTitle)
        {
            StartPosX = iX;
            StartPosY = iY;

            InitializeComponent();

            if (iTitle != null)
                this.Text = iTitle;

            ContactsList = iContactsList;

            ContactsControl ContactsCont = new ContactsControl();
            ContactsCont.ContactsList.ItemsSource = ContactsList;
            ContactListHost.Child = ContactsCont;
        }

        private void frmNewOptionsContacts_Load(object sender, EventArgs e)
        {
            this.Left = StartPosX;
            this.Top = StartPosY;

            int ch = ContactListHost.Height;
            int lh = 18; // 16;
            int h = 1 * lh;
            if (ContactsList != null)
                if (ContactsList.Count > 1)
                    h = ContactsList.Count * lh;
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
