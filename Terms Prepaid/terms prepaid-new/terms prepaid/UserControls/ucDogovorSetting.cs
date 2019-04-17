using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Forms;
using terms_prepaid.Helper_Classes;
using terms_prepaid.Helpers;

namespace terms_prepaid.UserControls
{


    public partial class ucDogovorSetting : UserControl
    {
        private string _dgcode = string.Empty;
        private DataRow _rowDogovor = null;
        public ucDogovorSetting(string dgcode)
        {
            InitializeComponent();
            _dgcode = dgcode;
            GetDate();
            SetDogovorSetting();
        }
        private void GetDate()
        {            
            cbStatuses.DataSource = WorkWithData.GetStatuses();
            cbStatuses.DisplayMember = "name";
            cbStatuses.ValueMember = "key";
          
        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            MyTime ppaymenttime = MyTime.ParseTime(mtbPrePaydTime.Text, "Предоплата до");
            MyTime paymenttime = MyTime.ParseTime(mtbPaydTime.Text, "Оплата до");
            if(ppaymenttime==null||paymenttime==null){return;}
            WorkWithData.UpdateDogovorSetting(_dgcode, cbIsprocentPrePayd.Checked, cbIsprocentPrePayd.Checked ? decimal.Parse(tbPrePaid.Text) : decimal.Parse(tbPrePaydSum.Text),
                                              dtpPrePaymentDate.Checked
                                                  ? (object)dtpPrePaymentDate.Value.Date.AddHours(ppaymenttime.hour)
                                                                  .AddMinutes(ppaymenttime.minute)
                                                  : DBNull.Value,
                                              dtpPaymentDate.Checked
                                                  ? (object)dtpPaymentDate.Value.Date.AddHours(paymenttime.hour)
                                                                     .AddMinutes(paymenttime.minute)
                                                  : DBNull.Value);
            SetDogovorSetting();
            
        }

        private void SetDogovorSetting()
        {
            _rowDogovor = WorkWithData.GetDogovorSettings(_dgcode);
            if (_rowDogovor.Field<int>("DG_SOR_CODE") == 2)
            {
                dtpPaymentDate.Enabled = false;
                dtpPrePaymentDate.Enabled = false;
                dtpVisaDate.Enabled = false;
                cbStatuses.SelectedValue = 2;
                cbStatuses.Enabled = false;
                tbDiscount.Enabled = false;
                tbPrePaid.Enabled = false;
                cbIsProcentCommision.Enabled = false;
                btnSave.Enabled = false;
                mtbPaydTime.Enabled = false;
                mtbPrePaydTime.Enabled = false;
                btnSaveDiscoount.Enabled = false;
                btnSaveStatus.Enabled = false;
                tbPrePaydSum.Enabled = false;


            }

            cbStatuses.SelectedValue = _rowDogovor.Field<int>("DA_STATUS");
            lbRate.Text = _rowDogovor.Field<string>("DG_RATE");
            lbDiscountD.Text = _rowDogovor["DISCOUNTWHY"] == DBNull.Value
                                   ? ""
                                   : _rowDogovor.Field<DateTime>("DISCOUNTWHY").ToString("dd.MM.yy HH:mm");
            lbPPaymentDateD.Text = _rowDogovor["PPAYMENTDATEWHY"] == DBNull.Value
                                       ? ""
                                       : _rowDogovor.Field<DateTime>("PPAYMENTDATEWHY").ToString("dd.MM.yy HH:mm");
            lbPaymentDateD.Text = _rowDogovor["PAYMENTDATEWHY"] == DBNull.Value
                                      ? ""
                                      : _rowDogovor.Field<DateTime>("PAYMENTDATEWHY").ToString("dd.MM.yy HH:mm");
            lbPrePaydD.Text = _rowDogovor["Razmer_PWHY"] == DBNull.Value
                                  ? ""
                                  : _rowDogovor.Field<DateTime>("Razmer_PWHY").ToString("dd.MM.yy HH:mm");

            lbPriceD.Text = _rowDogovor["PriceWHY"] == DBNull.Value
                                ? ""
                                : _rowDogovor.Field<DateTime>("PriceWHY").ToString("dd.MM.yy HH:mm");
            lbPrePayd.Text =
                (_rowDogovor.Field<int>("DG_PROCENT") == 1
                     ? _rowDogovor.Field<decimal>("DG_PRICE")*_rowDogovor.Field<decimal>("DG_RazmerP")/100
                     : _rowDogovor.Field<decimal>("DG_RazmerP")).ToString("F2") + " " +
                _rowDogovor.Field<string>("DG_RATE");
            lbPrice.Text = _rowDogovor.Field<decimal>("DG_Price").ToString("F2") + " " +
                           _rowDogovor.Field<string>("DG_RATE");
            lbPriceM.Text = _rowDogovor.Field<string>("PRICEWHO");
            lbDiscountM.Text = _rowDogovor.Field<string>("DISCOUNTWHO");
            lbPPaymentDateM.Text = _rowDogovor.Field<string>("PPAYMENTDATEWHO");
            lbPaymentDateM.Text = _rowDogovor.Field<string>("PAYMENTDATEWHO");
            lbPrePaydM.Text = _rowDogovor.Field<string>("Razmer_PWHO");
            lbDiscount.Text = (_rowDogovor.Field<decimal?>("DG_DISCOUNTSUM") ?? 0).ToString("F2") + " " +
                              _rowDogovor.Field<string>("DG_RATE");






            if (_rowDogovor["DG_PAYMENTDATE"].Equals(DBNull.Value))
            {
                dtpPaymentDate.Checked = false;
                lbPaymentDate.Text = "";

            }
            else
            {
                dtpPaymentDate.Checked = true;
                lbPaymentDate.Text = _rowDogovor.Field<DateTime>("DG_PAYMENTDATE").ToString("dd.MM.yy HH:mm");
                dtpPaymentDate.Value = _rowDogovor.Field<DateTime>("DG_PAYMENTDATE").Date;
                mtbPaydTime.Text = _rowDogovor.Field<DateTime>("DG_PAYMENTDATE").ToString("HH:mm");
            }
            if (_rowDogovor["DG_PPAYMENTDATE"].Equals(DBNull.Value))
            {
                dtpPrePaymentDate.Checked = false;
                lbPrePaydeDate.Text = "";
            }
            else
            {
                dtpPrePaymentDate.Checked = true;
                lbPrePaydeDate.Text = _rowDogovor.Field<DateTime>("DG_PPAYMENTDATE").ToString("dd.MM.yy HH:mm");
                dtpPrePaymentDate.Value = _rowDogovor.Field<DateTime>("DG_PPAYMENTDATE").Date;
                mtbPrePaydTime.Text = _rowDogovor.Field<DateTime>("DG_PPAYMENTDATE").ToString("HH:mm");
            }


            if (_rowDogovor["DG_VISADATE"].Equals(DBNull.Value))
            {
                dtpVisaDate.Checked = false;
            }
            else
            {
                dtpVisaDate.Checked = true;
                dtpVisaDate.Value = _rowDogovor.Field<DateTime>("DG_VISADATE");
            }






            if (_rowDogovor.Field<int>("DG_PROCENT") == 1)
            {
                cbIsprocentPrePayd.Checked = true;
                tbPrePaid.Text = _rowDogovor.Field<decimal>("DG_RazmerP").ToString("f2");
                tbPrePaydSum.Text = (_rowDogovor.Field<decimal>("DG_RazmerP")*_rowDogovor.Field<decimal>("DG_PRICE")/
                                     100).ToString(
                                         "F2");
            }
            else
            {
                cbIsprocentPrePayd.Checked = false;
                tbPrePaydSum.Text = _rowDogovor.Field<decimal>("DG_RazmerP").ToString("f2");
                tbPrePaid.Text = (_rowDogovor.Field<decimal>("DG_RazmerP")/_rowDogovor.Field<decimal>("DG_PRICE")*
                                  100).ToString(
                                      "F2");
            }



            if (_rowDogovor.Field<Int16>("DG_TYPECOUNT") == 1)
            {
                decimal disc =
                    _rowDogovor.Field<decimal?>("DG_DISCOUNT") ?? 0;
                tbDiscount.Text = disc.ToString("f2");
            }
            else
            {
                tbDiscount.Text = 0.ToString("f2");
            }

            DateTime date = WorkWithData.GetDateForStatus(_dgcode, _rowDogovor.Field<string>("NS_QUERYFORDATE"));
            lbStatus.Text = _rowDogovor.Field<string>("NS_Name") + " " + date.ToString("dd.MM.yy HH:mm");
            lbSatusD.Text = (_rowDogovor["StatusWHY"] == DBNull.Value
                                 ? ""
                                 : _rowDogovor.Field<DateTime>("StatusWHY").ToString("dd.MM.yy HH:mm"));
            lbStatusM.Text = _rowDogovor.Field<string>("StatusWHO");
        }

        void SetAnnulation()
        {
            if (_rowDogovor.Field<decimal>("DG_PAYED") > 0)
            {
                MessageBox.Show("Аннуляция невозможна так как путевка оплачена!","", MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
            else
            {
                int? idReason = frmAnnulationReasons.GetReason();
                if (idReason != null)
                {
                   if (
                       MessageBox.Show("Вы уверены что хотите аннулировать путевку?", "", MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question) == DialogResult.Yes)
                   {
                       WorkWithData.SetAnnulateDogovor(_dgcode, idReason.Value);
                   }
                }
            }
        }

        private void cbStatuses_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbStatuses.SelectedValue.Equals(2) && _rowDogovor.Field<int>("DG_SOR_CODE") != 2)
                {
                    SetAnnulation();
                    SetDogovorSetting();
                }
                else
                {
                    
                    lbStatusesDateNew.Text = WorkWithData.GetDateForStatus(_dgcode, (int) cbStatuses.SelectedValue).ToString("dd.MM.yy  HH:mm");
                }

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);

            }

        }

        private void tbPrePaid_KeyPress(object sender, KeyPressEventArgs e)
        {
                       
            char[] acceptChars = {'0','1', '2', '3', '4','5','6', '7', '8', '9',',',(char)8};
            
            
            if (!Array.Exists(acceptChars, x => x == e.KeyChar))
            {
                e.Handled = true;
            }
            if(e.KeyChar==','&&(((TextBox)sender).Text.IndexOf(',')>=0||((TextBox)sender).Text.Length<1))
            {
                e.Handled = true;
            }
        }

        private void tbPrePaid_TextChanged(object sender, EventArgs e)
        {
            if (cbIsprocentPrePayd.Checked)
            {
                try
                {
                    decimal sum = _rowDogovor.Field<decimal>("DG_PRICE");
                    tbPrePaydSum.Text = (decimal.Parse(tbPrePaid.Text)*sum/100).ToString("F2");

                }
                catch (Exception)
                {

                    
                }

            }
            //cbIsprocentPrePayd.Checked = true;

           
        }

        private void btnSaveStatus_Click(object sender, EventArgs e)
        {
            WorkWithData.UpdateStatusDogovor(_dgcode,(int)cbStatuses.SelectedValue);
            SetDogovorSetting();
        }

        private void btnSaveDiscoount_Click(object sender, EventArgs e)
        {
            WorkWithData.UpdateDiscountDogovor(_dgcode, decimal.Parse(tbDiscount.Text));
            SetDogovorSetting();
        }

        private void cbIsprocentPrePayd_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsprocentPrePayd.Checked)
            {
                tbPrePaid.ReadOnly = false;
                tbPrePaydSum.ReadOnly = true;
            }
            else
            {
                tbPrePaid.ReadOnly = true;
                tbPrePaydSum.ReadOnly = false;

            }
        }

        private void tbPrePaid_Click(object sender, EventArgs e)
        {
            cbIsprocentPrePayd.Checked = true;
        }

        private void tbPrePaydSum_Click(object sender, EventArgs e)
        {
            cbIsprocentPrePayd.Checked = false;
        }

        private void tbPrePaydSum_TextChanged(object sender, EventArgs e)
        {
            if (!cbIsprocentPrePayd.Checked)
            {
                try
                {
                    tbPrePaid.Text =
                        (decimal.Parse(tbPrePaydSum.Text)/_rowDogovor.Field<decimal>("DG_PRICE")*100).ToString("F2");

                }
                catch (Exception)
                {


                }

            }
        }
    }
}
