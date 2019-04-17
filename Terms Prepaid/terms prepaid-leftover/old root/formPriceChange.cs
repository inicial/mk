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
    public partial class formPriceChange : Form
    {
        private double _brutto, _netto;
        private int _dlkey;
        private string _dg_code;
        public formPriceChange(double cost, double costNetto, int dlkey,string dg_code)
        {
            InitializeComponent();
            _dlkey = dlkey;
            _brutto = cost;
            _netto = costNetto;
            tbBrutto.Text = _brutto.ToString("F2");
            tbNetto.Text = _netto.ToString("F2");
            _dg_code = dg_code;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            double brutto, netto;
            if (!double.TryParse(tbBrutto.Text, out brutto))
            {
                MessageBox.Show("Цена брутто заведена некорректно!");
                return;
            }
            if (!double.TryParse(tbNetto.Text, out netto))
            {
                MessageBox.Show("Цена нетто заведена некорректно!");
                return;
            }
            ForMaster.changePrice(brutto, netto, _dlkey, _dg_code);
            Close();
        }
    }
}
