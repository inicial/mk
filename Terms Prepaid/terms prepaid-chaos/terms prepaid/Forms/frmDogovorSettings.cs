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

namespace terms_prepaid
{
    public partial class frmDogovorSettings : Form
    {
        private int? _bron, _realiz;
        private bool bron = false, realize = false;
        private string _dgcode;
        DataTable _usersBron = new DataTable(), _usersReal = new DataTable();
        public frmDogovorSettings(string dgcode,int? bron,int? realiz)
        {
            InitializeComponent();
            _bron = bron;
            _realiz = realiz;
            _dgcode = dgcode;
            GetDate();
            cbBron.DataSource = _usersBron;
            cbBron.ValueMember = "us_key";
            cbBron.DisplayMember = "US_FullNameLat";
            if (_bron != null)
            {
                cbBron.SelectedValue = _bron;
            }
            else
            {
                cbBron.SelectedIndex = 2;
            }
            cbRealiz.DataSource = _usersReal;
            cbRealiz.ValueMember = "us_key";
            cbRealiz.DisplayMember = "US_FullNameLat";
            if (_realiz != null)
            {
                cbRealiz.SelectedValue = _realiz;
            }
            else
            {
                cbRealiz.SelectedIndex = 2;
            }
            this.bron = false;
            this.realize = false;
        }
        //IS_ROLEMEMBER('avSalesManagers',US_USERID) as sale, IS_ROLEMEMBER('avProductManagers',US_USERID) as bron
        void GetDate()
        {
            using (SqlCommand com = new SqlCommand(@"select us_key,US_FullNameLat from UserList left join mk_user_rule on UR_USKEY=us_key  where isnull([is_realiz],0)=1
order by US_FullNameLat ", WorkWithData.Connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(com);

                _usersReal.Rows.Clear();
                adapter.Fill(_usersReal);
            }
            using (SqlCommand com = new SqlCommand(@"select us_key,US_FullNameLat from UserList left join mk_user_rule on UR_USKEY=us_key  where isnull([is_bronir],0)=1
order by US_FullNameLat ", WorkWithData.Connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                _usersBron.Rows.Clear();
                adapter.Fill(_usersBron);

            }
            
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (bron)
            {
                using (SqlCommand com = new SqlCommand("mk_set_bronir", WorkWithData.Connection))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@dgcode", _dgcode);
                    com.Parameters.AddWithValue("@user", cbBron.SelectedValue);
                    com.ExecuteNonQuery();
                }
               if (
                   MessageBox.Show("Поменять бронировщика на все услуги?", "", MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question) == DialogResult.Yes)
               {
                   using (SqlCommand com = new SqlCommand("update mk_DogovorListAdd set bron=@user where dg_code=@dgcode", WorkWithData.Connection))
                   {
                       
                       com.Parameters.AddWithValue("@dgcode", _dgcode);
                       com.Parameters.AddWithValue("@user", cbBron.SelectedValue);
                       com.ExecuteNonQuery();
                   }
               }
            }
            if (realize)
            {
                using (SqlCommand com = new SqlCommand("update tbl_Dogovor set DG_OWNER=@user where DG_CODE=@dgcode",WorkWithData.Connection))
                {
                   
                    com.Parameters.AddWithValue("@dgcode", _dgcode);
                    com.Parameters.AddWithValue("@user", cbRealiz.SelectedValue);
                    com.ExecuteNonQuery();
                }
            }

            Close();
        }

        private void cbBron_SelectedIndexChanged(object sender, EventArgs e)
        {
            bron = true;
        }

        private void cbRealiz_SelectedIndexChanged(object sender, EventArgs e)
        {
            realize = true;
        }
    }
}
