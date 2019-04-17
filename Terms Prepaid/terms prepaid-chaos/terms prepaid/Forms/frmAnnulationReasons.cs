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
    public partial class frmAnnulationReasons : Form
    {
        private static int? idReason = null;
        public frmAnnulationReasons()
        {
            InitializeComponent();
            GetDate();
        }
        void GetDate()
        {
            cbReasons.DataSource = WorkWithData.GetAnnulateReasons();
            cbReasons.ValueMember = "key";
            cbReasons.DisplayMember = "name";
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {

                idReason = int.Parse(cbReasons.SelectedValue.ToString());
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public static int? GetReason()
        {
            new frmAnnulationReasons().ShowDialog();
            return idReason;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            idReason = null;
            Close();
        }

    }
}
