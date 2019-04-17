using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid.Forms
{
    public partial class frmAccounts : Form
    {
        private string _dgcode = "";
        private const string selectAccounts = "Select Ac_Name,AC_URL from mk_AccountsURL where AC_TYPE in({0},-1) ";
//        private const string insertHistory = @"INSERT INTO [lanta].[dbo].[History]([HI_DGCOD]
//           ,[HI_DATE]
//           ,[HI_WHO]
//           ,[HI_TEXT]
//           ,[HI_MOD])
//            values (@dgCode,GetDate(),@Who,@Text,@mod)";
        private DataTable accountTable;
        private int _type,_agencyKey;
        public frmAccounts(string dg_code,int agencyKey)
        {
            
            InitializeComponent();
            _dgcode = dg_code;
            _type = agencyKey == 0 ? 0:1;
            _agencyKey = agencyKey;
            GetDate();
        }
        void GetDate()
        {
            accountTable= new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(string.Format(selectAccounts,_type),WorkWithData.Connection))
            {
                adapter.Fill(accountTable);
            }
            lbAccounts.DataSource = accountTable;
            lbAccounts.ValueMember = "AC_URL";
            lbAccounts.DisplayMember = "AC_NAME";
            lbNonResidentAccount.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void PrintAccount(string innerUrl)
        {
            string url = innerUrl;

            url = url.Replace("[dgcode]", _dgcode);


            int pr;
            using (SqlCommand com = new SqlCommand(@"select top(1)us_key from DUP_USER where US_PRKEY=" + _agencyKey.ToString(), WorkWithData.Connection))
            {
                pr = (int)com.ExecuteScalar();
            }
            if (_agencyKey == 0)
            {
                int clkey = 0;
                using (SqlCommand com = new SqlCommand("select top 1 isnull(DD_CLКеу,0) from Lanta_DogovorDeputat inner join tbl_dogovor on dg_key=DD_DGКеу where DG_CODE = @dgcod ",WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@dgcod", _dgcode);
                    clkey = (int) com.ExecuteScalar();
                    url = url.Replace("[cl_key]",clkey.ToString());
                }
            }
            url = url.Replace("[agencyKey]", _agencyKey.ToString());
            url = url.Replace("[agencyID]", pr.ToString());
            url = url.Replace("[us_key]", WorkWithData.GetUserID().ToString());
            string tempPath = Path.GetTempPath();
            WebClient client = new WebClient();
            byte[] data = client.DownloadData(url);
            string filename = "Account_" + _dgcode + ".pdf";
            BinaryWriter bw = new BinaryWriter(File.Create(tempPath + filename));
            bw.Write(data);
            bw.Close();
            Process.Start(tempPath + filename);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {/*  http://qa.mcruises.ru/OrderListBonus/PrintOrder.php?agencyId=421&view_details=CAR60214A1&dgCode=CAR60214A1&us_key=64 */
            //string url = (string)lbAccounts.SelectedValue;

            //url = url.Replace("[dgcode]", _dgcode);
           

            //int pr;
            //using (SqlCommand com = new SqlCommand(@"select top(1)us_key from DUP_USER where US_PRKEY="+_agencyKey.ToString(),WorkWithData.Connection))
            //{
            //    pr = (int) com.ExecuteScalar();
            //}
            //url = url.Replace("[agencyKey]", _agencyKey.ToString()); 
            //url = url.Replace("[agencyID]", pr.ToString());
            //url = url.Replace("[us_key]", WorkWithData.GetUserID().ToString());
            //string tempPath = Path.GetTempPath();
            //WebClient client = new WebClient();
            //byte[] data = client.DownloadData(url);
            //string filename = "Account_" + _dgcode + ".pdf";
            //BinaryWriter bw = new BinaryWriter(File.Create(tempPath+filename));
            //bw.Write(data);
            //bw.Close();
            //Process.Start(tempPath + filename);
            PrintAccount((string)lbAccounts.SelectedValue);
            //url += string.Format("?agencyId={0}&view_details={1}&dgCode={1}&us_key={2}",0,_dgcode,WorkWithData.GetUserID());

            //using (SqlCommand com = new SqlCommand(@"",WorkWithData.Connection))
            //{

            //}
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            frmInvoicePuplic frm = new frmInvoicePuplic(_dgcode);
            frm.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool flag;
            using (
                SqlCommand com =
                    new SqlCommand(
                        "select case when DA_InvoiceRateOut is not null then 1 else 0 end  from mk_DogovorAdd where DA_DGCODE =@dgcod",
                        WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dgcod", _dgcode);
                flag = ((int)com.ExecuteScalar())==1?true:false;
            }
            if (!flag)
            {
                MessageBox.Show("Сначало неоходимо выставить счет для нерезидентов.", "", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string url = string.Empty;
                if (_agencyKey != 0)
                {
                    url ="http://mcruises.ru/OrderListBonus/invoicePDF.php?dgCode=[dgcode]&agencyId=[agencyKey]&us_key=[us_key]";
                }
                else
                {
                    url = "http://mcruises.ru/OrderListBonus/invoicePDF.php?dgCode=[dgcode]&cl_key=[cl_key]&us_key=[us_key]";
                }
                PrintAccount(url);
            }
        }
    }
}
