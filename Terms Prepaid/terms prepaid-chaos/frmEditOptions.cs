using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Rep10027.Helpers;

namespace terms_prepaid
{
    public partial class frmEditOptions : Form
    {
        private int flag;
        private int option = 0,dlkey;
        public frmEditOptions(string caption,int dl_key)
        {
            InitializeComponent();
            this.Text = caption;
            flag = 1;
            dlkey = dl_key;
            tbNumber.Text = string.Empty;
            tbCabinNumber.Text = string.Empty;
            rtbDescript.Text = string.Empty;
            dtpDateEnd.Value = DateTime.Now.Date;

        }

        public frmEditOptions(string caption, int dl_key, int option_id)
            
        {
            InitializeComponent();
            this.Text = caption;
            flag = 0;
            dlkey = dl_key;
            option = option_id;
            //загрузка опции для редактирования
            SqlCommand com = new SqlCommand(@"Select OP_ID,OP_DLKEY,OP_Descript,OP_number,OP_N_cabin,OP_date_end,OP_category, OP_IsBook from mk_options where OP_ID= " + option_id.ToString(), WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            tbNumber.Text = dt.Rows[0]["OP_number"].ToString();
            tbCabinNumber.Text = dt.Rows[0]["OP_N_cabin"].ToString();
            rtbDescript.Text = dt.Rows[0]["OP_Descript"].ToString();
            dtpDateEnd.Value = Convert.ToDateTime(dt.Rows[0]["OP_date_end"].ToString());
            tbCategory.Text = dt.Rows[0]["OP_category"].ToString();
            cbBook.Checked = bool.Parse(dt.Rows[0]["OP_IsBook"].ToString());


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sqlQuery = string.Empty;
            if (flag == 1)
            {
                sqlQuery = @"INSERT INTO [dbo].[mk_options]
                                         ([OP_DLKEY]
                                         ,[OP_Descript]
                                         ,[OP_number]
                                         ,[OP_N_cabin]
                                         ,[OP_date_end]
                                         ,[OP_WHO]
                                         ,[OP_LastUpdate]
                                         ,[OP_category]
                                         ,OP_IsBook)
                             VALUES
                                         (@OP_DLKEY 
                                         ,@OP_Descript
                                         ,@OP_number
                                         ,@OP_N_cabin
                                         ,@OP_date_end
                                         ,(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())
                                         ,getdate()
                                         ,@OP_category
                                         ,@OP_IsBook)";
            }
            else
            {
                if (flag==0)
                {
                    sqlQuery = @"update [dbo].[mk_options] set
                                         [OP_DLKEY] =@OP_DLKEY
                                         ,[OP_Descript] =@OP_Descript
                                         ,[OP_number]=@OP_number
                                         ,[OP_N_cabin]=@OP_N_cabin
                                         ,[OP_date_end]=@OP_date_end
                                         ,[OP_WHO]=(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())
                                         ,[OP_LastUpdate]=getdate() 
                                          ,[OP_category]=@OP_category 
                                         ,OP_IsBook = @OP_IsBook where OP_ID =  " + option.ToString();

                }
            }
            SqlCommand com = new SqlCommand(sqlQuery,WorkWithData.Connection);
            com.Parameters.AddWithValue("@OP_DLKEY", dlkey);
            com.Parameters.AddWithValue("@OP_Descript", rtbDescript.Text);
            com.Parameters.AddWithValue("@OP_number", tbNumber.Text);
            com.Parameters.AddWithValue("@OP_N_cabin", tbCabinNumber.Text);
            com.Parameters.AddWithValue("@OP_date_end", dtpDateEnd.Value);
            com.Parameters.AddWithValue("@OP_category", tbCategory.Text);
            com.Parameters.AddWithValue("@OP_IsBook", cbBook.Checked);
            com.ExecuteNonQuery();
            Close();
        }

        private void cbBook_CheckedChanged(object sender, EventArgs e)
        {
            dtpDateEnd.Enabled = !cbBook.Checked;
        }
    }
}
