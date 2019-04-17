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
    public partial class frmDogovorRegionSetting : Form
    {
        private string _dgcode;
        private DataTable _regions;
        public frmDogovorRegionSetting(string dgCode)
        {
            InitializeComponent();
            _dgcode = dgCode;
            GetDate();
        }
        void GetDate()
        {
            _regions = WorkWithData.GetRegions();
            cbRegion.DataSource = _regions;
            cbRegion.ValueMember ="key";
            cbRegion.DisplayMember = "name";
            int? curRegion = WorkWithData.GetRegionByDogovor(_dgcode);
            if (curRegion != null)
            {
                cbRegion.SelectedValue = curRegion.Value;
            }
            else
            {
                cbRegion.SelectedIndex = -1;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int regId = (int)cbRegion.SelectedValue;
            WorkWithData.UpdateDogovorRegion(dgcode:_dgcode,regionId:regId);
        }
    }
}
