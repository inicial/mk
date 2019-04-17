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
    public partial class frmParner : Form
    {
        private int _prkey = 0;
        public frmParner(int prKey)
        {
            InitializeComponent();
            _prkey = prKey;
            GetDate();
        }
       void GetDate()
       {
           DataRow row = WorkWithData.GetPartnerData(_prkey);
           tbName.Text = row.Field<string>("PR_NAME");
           tbFullName.Text = row.Field<string>("PR_FULLNAME");
           tbE_Mail.Text = row.Field<string>("PR_EMAIL");
           tbPhone.Text = row.Field<string>("PR_Phones");
           tbAdress.Text = row.Field<string>("PR_ADRESS");
       }

       private void btnClose_Click(object sender, EventArgs e)
       {
           this.Close();
       }
        
    }
}
