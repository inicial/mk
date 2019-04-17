using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid.Forms
{
    public partial class frmStatusFileSetting : Form
    {
        private string _filename;
        public frmStatusFileSetting(string filename)
        {
            InitializeComponent();
            _filename = filename;
        }

        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked)
            {
                tbDescription.Enabled = true;
            }
            else
            {
                tbDescription.Text = string.Empty;
                tbDescription.Enabled = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbYes.Checked)
            {
                WorkWithData.UpdateFileStatusOk(_filename);
            }
            else if(rbNo.Checked)
            {
                if (string.IsNullOrEmpty(tbDescription.Text.Trim()))
                {
                    MessageBox.Show("При отказе необходим комментарий");
                    return;
                }
                WorkWithData.UpdateFileStatusNo(_filename,tbDescription.Text.Trim());
            }
            else
            {
                MessageBox.Show("Необходимо выбрать статус");
                return;
            }
            this.Close();
        }
    }
}
