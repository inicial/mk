using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid
{
    public partial class MainForm : Form
    {
        private string _dgCode;
        private DataTable paymands = new DataTable();
        private int selDL_Key;
        public MainForm(string dgCode)
        {
            InitializeComponent();
            _dgCode = dgCode;
            GetDate();
        }
        

        void GetDate()
        {
            String select = string.Format(@"SELECT 
                                     [DL_Name]
                                    ,[DL_key]
                                    ,[tbl_dogovor_list_key]                                   
                                     ,isnull([PaymantDate],'1900-01-01') as PaymantDate
                                    ,[PaymantValue]
                                    ,[Komission]
                                    ,[Tipe_of_Komission]
                                    ,isnull([PPaymentdaDate],'1900-01-01') as PPaymentdaDate
                                    FROM [dbo].[mk_DogovorListAdd]
                                    inner join tbl_DogovorList on tbl_dogovor_list_key=DL_KEY
	                                where DL_DGCOD = '{0}'", _dgCode);
            SqlCommand com = new SqlCommand(select,WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            
            paymands.Clear();
            adapter.Fill(paymands);
            dgvPaymants.DataSource = paymands;
            UpdateDataGrid();

        }
      void UpdateDataGrid()
      {
          foreach (DataGridViewColumn column  in dgvPaymants.Columns)
          {
              switch (column.Name.ToLower())
              {
                  case "paymantdate":
                          column.HeaderText = "Дата предоплаты";
                          column.DisplayIndex = 1;
                          break;
                  case "ppaymentdadate":
                          column.HeaderText = "Дата полной оплаты";
                          column.DisplayIndex = 2;
                          break;
                  case "paymantvalue":
                          column.HeaderText = "Сумма предоплаты";
                          column.DisplayIndex = 3;
                          break;
                  case "dl_name":
                          column.HeaderText = "Услуга";
                          column.DisplayIndex = 0;
                          break;
                  default:
                          column.Visible = false;
                          break;

              }
          }
      }

      private void dgvPaymants_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
      {
          tbValue.Text = dgvPaymants.SelectedRows[0].Cells["paymantvalue"].Value.ToString();
          dtPaymand.Value = Convert.ToDateTime(dgvPaymants.SelectedRows[0].Cells["paymantdate"].Value).Date;
          dtPPaymanDate.Value = Convert.ToDateTime(dgvPaymants.SelectedRows[0].Cells["ppaymentdadate"].Value).Date;
          tbKomossion.Text = dgvPaymants.SelectedRows[0].Cells["Komission"].Value.ToString();
          dgvPaymants.Enabled = false;
         try
         {
          cbComissionType.SelectedIndex = Convert.ToInt32(dgvPaymants.SelectedRows[0].Cells["Tipe_of_Komission"].Value);
         }
          catch (Exception)
          {
              cbComissionType.SelectedIndex = -1;

          }
          gbInfo.Enabled = true;
          
          selDL_Key = Convert.ToInt32(dgvPaymants.SelectedRows[0].Cells["DL_key"].Value);
        //  string str = WorkWithData.GenerateBlock1ForCruise(selDL_Key);
         // MessageBox.Show(str);
          getOptions();
      }
        void UpdateOptionsGrid()
        {
            
            foreach (DataGridViewColumn column in dgvOptions.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "op_number" :
                        {
                            column.HeaderText = "Номер опции";
                            column.DisplayIndex = 0;
                        }
                        
                        break;
                    case "op_descript":
                        {
                            column.HeaderText = "Описание";
                            column.DisplayIndex = 1;
                        }
                        break;
                    default:
                        column.Visible = false;
                        break;
                }   
            }
        }
       
        void getOptions()
        {
            DataTable optionTable = new DataTable();
            SqlCommand com = new SqlCommand(@"select * from mk_options where OP_DLKEY = @p1",WorkWithData.Connection);
            com.Parameters.AddWithValue("@p1", selDL_Key);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            adapter.Fill(optionTable);
            dgvOptions.DataSource = optionTable;
            UpdateOptionsGrid();
        }

      private void btnCancel_Click(object sender, EventArgs e)
      {
          dgvOptions.DataSource = null;
          dgvPaymants.Enabled = true;
          gbInfo.Enabled = false;
      }
      
      private void btnOk_Click(object sender, EventArgs e)
      {
          String upd = @"declare @user varchar(100)
                        select @user = isnull((select  US_FullName from dbo.UserList
                        where US_USERID=(select suser_sname())),'администратор')
                        update mk_DogovorListAdd set 
                          PaymantDate=@p0 
                        , PaymantValue=@p1
                        , Komission = @p2 
                        , PPaymentdaDate =@p4
                        , HandWho=@user
                        , Tipe_of_Komission = @typecomm
                        where tbl_dogovor_list_key =@p3";
          SqlCommand com = new SqlCommand(upd,WorkWithData.Connection);
          if (dtPaymand.Value.Date==new DateTime(1900,01,01).Date)
          {
            com.Parameters.AddWithValue("@p0", DBNull.Value);
          }
          else
          {
            com.Parameters.AddWithValue("@p0", dtPaymand.Value.Date);  
          }
          
          if (dtPPaymanDate.Value.Date==new DateTime(1900,01,01).Date)
          {
            com.Parameters.AddWithValue("@p4", DBNull.Value);
          }
          else
          {
            com.Parameters.AddWithValue("@p4", dtPPaymanDate.Value.Date);  
          }
          if (tbValue.Text == string.Empty)
          {
              com.Parameters.AddWithValue("@p1", DBNull.Value);
          }
          else
          {
             com.Parameters.AddWithValue("@p1", Convert.ToDecimal(tbValue.Text)); 
          }
          if (tbKomossion.Text == string.Empty)
          {
            com.Parameters.AddWithValue("@p2", DBNull.Value);
          }
          else
          {
            com.Parameters.AddWithValue("@p2", Convert.ToInt32(tbKomossion.Text));  
          }
          if (cbComissionType.SelectedIndex < 0)
          {
              com.Parameters.AddWithValue("@typecomm", DBNull.Value);
          }
          else
          {
              com.Parameters.AddWithValue("@typecomm", cbComissionType.SelectedIndex);
          }
          com.Parameters.AddWithValue("@p3", Convert.ToInt32(dgvPaymants.SelectedRows[0].Cells["tbl_dogovor_list_key"].Value));
          com.ExecuteNonQuery();
          dgvPaymants.Enabled = true;
          gbInfo.Enabled = false;
        
          GetDate();
          dgvOptions.DataSource = null;
      }

      private void btnDelP_Click(object sender, EventArgs e)
      {
          dtPaymand.Value=new DateTime(1900,1,1);
      }

      private void btnDelpp_Click(object sender, EventArgs e)
      {
          dtPPaymanDate.Value = new DateTime(1900, 1, 1);
      }

      private void btnAddOption_Click(object sender, EventArgs e)
      {
          frmEditOptions newOption = new frmEditOptions("Новая опция",selDL_Key);
          newOption.ShowDialog();
          getOptions();

      }

      private void btnEditOption_Click(object sender, EventArgs e)
      {
          if (dgvOptions.SelectedRows.Count < 1)
          {
              MessageBox.Show("Выберете сначало опцию!");
              return;
          }
          frmEditOptions option = new frmEditOptions("", selDL_Key,Convert.ToInt32(dgvOptions.SelectedRows[0].Cells["OP_id"].Value));
          option.ShowDialog();
          getOptions();
      }

      private void btnOptionFiles_Click(object sender, EventArgs e)
      {
          if (dgvOptions.SelectedRows.Count < 1)
          {
              MessageBox.Show("Выберете сначало опцию!");
              return;
          }
          frmFileOptions files = new frmFileOptions(Convert.ToInt32(dgvOptions.SelectedRows[0].Cells["OP_id"].Value));
          files.ShowDialog();

      }

      private void MainForm_Load(object sender, EventArgs e)
      {
          //this.Location = new System.Drawing.Point(width - this.Size.Width, 0); 
      }

      private void tbKomossion_TextChanged(object sender, EventArgs e)
      {

      }

    }
}
