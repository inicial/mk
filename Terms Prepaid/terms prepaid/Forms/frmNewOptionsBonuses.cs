using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataService;
using NLog;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;
using WpfControlLibrary.ViewModel;
using WpfControlLibrary.View;
using terms_prepaid.Helpers;


namespace terms_prepaid.Forms
{

    public partial class frmNewOptionsBonuses : Form
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;

        private List<NameValue> BonusesList;
        private List<NameValue> SourceBonusesList;

        public frmNewOptionsBonuses(int iX, int iY, List<NameValue> iBonusesList, string iTitle)
        {
            StartPosX = iX;
            StartPosY = iY;

            InitializeComponent();

            if (iTitle != null)
                this.Text = iTitle;

            SourceBonusesList = iBonusesList;
            BonusesList = new List<NameValue>();
            if (SourceBonusesList != null)
            {
                foreach (NameValue bonus in SourceBonusesList)
                {
                    BonusesList.Add(new NameValue(bonus.Name, bonus.Value));
                }
            }
            //BonusesList.Add(new NameValue("New", ""));
            //BonusesList.Add(new NameValue("New", ""));
            //BonusesList.Add(new NameValue("New", ""));


            BonusesControl BonusesCont = new BonusesControl();
            BonusesCont.BonusesList.ItemsSource = BonusesList;
            BonusListHost.Child = BonusesCont;
        }

        private void frmNewOptionsBonuses_Load(object sender, EventArgs e)
        {
            this.Left = StartPosX;
            this.Top = StartPosY;

            int ch = BonusListHost.Height;
            int lh = 18; // 16;
            int h = 1 * lh;
            if (BonusesList != null)
                if (BonusesList.Count > 1) 
                    h = BonusesList.Count * lh;
            this.Height = this.Height + (h + 4) - ch; //  + 36
        }

        private void frmNewOptionsBonuses_Deactivate(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
