using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid.Forms
{
    public partial class frmInvoicePuplic : Form
    {
        private string _dgcode;
        DataTable rates = new DataTable();
        public frmInvoicePuplic(string dg_code)
        {
            InitializeComponent();
            _dgcode = dg_code;
            GetDate();
        }

        private void GetDate()
        {
            rates.Columns.Add("Rate_Name", typeof (string));
            rates.Columns.Add("Rate_Iso_Code",typeof(string));
            rates.Rows.Add("Доллар", "USD");
            rates.Rows.Add("Евро", "EUR");
            rates.Rows.Add("", DBNull.Value);
            cbRate.DataSource = rates;
            cbRate.DisplayMember = "Rate_Name";
            cbRate.ValueMember = "Rate_Iso_Code";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string message = string.Format("Вы уверены что хотите выставить счет для нерезидентов в  {0} для путевки {1}? ", cbRate.SelectedValue,_dgcode);
          
            if (MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string text = "Выставлен Invoice  в валюте "+ cbRate.SelectedValue.ToString();
                using (SqlCommand com = new SqlCommand("update mk_DogovorAdd set DA_InvoiceRateOut = @Rate where DA_DGCODE = @dgcode ",WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@Rate", cbRate.SelectedValue);
                    com.Parameters.AddWithValue("dgcode", _dgcode);
                    com.ExecuteNonQuery();
                }

                WorkWithData.InsertHistory(_dgcode,text,"IPC","");
                Close();
            }

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
