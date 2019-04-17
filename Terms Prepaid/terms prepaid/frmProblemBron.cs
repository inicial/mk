using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rep10027.Helpers;

namespace terms_prepaid
{
    public partial class frmProblemBron : Form
    {
        static private DataTable _dt;
        public frmProblemBron()
        {
            InitializeComponent();
            GetDate();
        }
        void GetDate()
        {
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@status", 1);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                tbOption3.Text= com.Parameters["@count"].Value.ToString();

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@status", 6);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                tbNoInshur.Text = com.Parameters["@count"].Value.ToString();

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@status", 7);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                tbNoAcceptUsluga.Text = com.Parameters["@count"].Value.ToString();

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@status", 5);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                tbDogovorNoAccept.Text = com.Parameters["@count"].Value.ToString();

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dogovor", 2);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                tbNoDogovorAccept.Text = com.Parameters["@count"].Value.ToString();

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@document", 1);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                tbNoPrintDoc.Text = com.Parameters["@count"].Value.ToString();

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@document", 4);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                tbNoInsertDoc.Text = com.Parameters["@count"].Value.ToString();

            }
            foreach (var control in tableLayoutPanel1.Controls)
            {
                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    if (int.Parse(tb.Text) > 0)
                    {

                            tb.ForeColor = Color.Red;

                    }
                    else
                    {
                        tb.ForeColor = SystemColors.ControlText;
                    }
                }

            }
            //timePulse.Enabled = true;
        }
        static public DataTable GetProblemBron(DataTable data)
        {
            _dt = data;
            frmProblemBron frm = new frmProblemBron();
            frm.ShowDialog();
            return _dt;
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            _dt.Clear();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType= CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status",1);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnNoPrintDoc_Click(object sender, EventArgs e)
        {
            _dt.Clear();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@document", 1);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnNoInsertDoc_Click(object sender, EventArgs e)
        {
            _dt.Clear();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@document", 4);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnNoDogovorAccept_Click(object sender, EventArgs e)
        {
            _dt.Clear();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@dogovor", 2);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnNoInshur_Click(object sender, EventArgs e)
        {
            _dt.Clear();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 6);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnNoAcceptUsluga_Click(object sender, EventArgs e)
        {
            _dt.Clear();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 7);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void btnDogovorNoAccept_Click(object sender, EventArgs e)
        {
            _dt.Clear();
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"mk_search_dogovor", WorkWithData.Connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@status", 5);
                adapter.SelectCommand.Parameters.AddWithValue("@problem", true);
                adapter.Fill(_dt);
            }
            Close();
        }

        private void timePulse_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
