using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace lanta.Clients
{
    public partial class DiscountCard : Form
    {
        DataTable Discount_Client = new DataTable("Discount_Client");
        DataTable Cards = new DataTable("Cards");
        SqlDataAdapter adapter;
        long CL_KEY; long MANAGER_ID;
        SqlConnection connection;
        public DiscountCard(long CL_KEY, long MANAGER_ID, SqlConnection connection)
        {
            InitializeComponent();
            this.CL_KEY = CL_KEY;
            this.MANAGER_ID = MANAGER_ID;
            this.connection = connection;
            adapter = new SqlDataAdapter("", connection);
        }
        private void DiscountCard_Load(object sender, EventArgs e)
        {
            adapter.SelectCommand.CommandText = @"SELECT DS_KEY, DS_NAME, DS_VALUE FROM  Discount_Client";
            adapter.Fill(Discount_Client);

            comboBox_DSTYPE.ValueMember = "DS_KEY";
            comboBox_DSTYPE.DisplayMember = "DS_NAME";
            comboBox_DSTYPE.DataSource = Discount_Client;

            comboBox_DSVALUE.ValueMember = "DS_KEY";
            comboBox_DSVALUE.DisplayMember = "DS_VALUE";
            comboBox_DSVALUE.DataSource = Discount_Client;

        }
        private void button2_Click(object sender, EventArgs e)
        {//Отмена
            this.DialogResult = DialogResult.Cancel;
        }
        private bool CheckFields()
        {
            bool ret = true;

            if (textBox_Code.Text.Length > 5)
            {
                MessageBox.Show("Серия карты не больше 5 символов!", "Проверка полей!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ret = false;
            }
            if (textBox_Number.Text.Length > 10)
            {
                MessageBox.Show("Номер карты не больше 10 символов!", "Проверка полей!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ret = false;
            }
            foreach (Char dig in textBox_Number.Text)
            {
                if (!Char.IsDigit(dig))
                {
                    MessageBox.Show("Номер карты должен содержать числовое значение!", "Проверка полей!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ret = false;
                }
            }

            return ret;
        }
        private void button1_Click(object sender, EventArgs e)
        {//Сохранить
            if (!CheckFields())
                return;
            try
            {

                Cards.Clear();
                adapter.SelectCommand.CommandText = @"SELECT     CD_Key, CD_Date, CD_Code, CD_Number, CD_IsValid, CD_CLKey, CD_DSKey, CD_CREATOR
                FROM         CARDS WHERE CD_Key=-1";
                adapter.Fill(Cards);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = builder.GetInsertCommand();

                DataRow dr = Cards.NewRow();
                dr["CD_Date"] = DateTime.Now;
                dr["CD_Code"] = textBox_Code.Text;
                dr["CD_Number"] = textBox_Number.Text;
                dr["CD_IsValid"] = Convert.ToInt16(checkBox_IsValid.Checked);
                dr["CD_CLKey"] = CL_KEY;
                dr["CD_DSKey"] = Convert.ToInt32(comboBox_DSTYPE.SelectedValue);
                dr["CD_CREATOR"] = MANAGER_ID;

                if (connection.State != ConnectionState.Open)
                    connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT MAX(CD_Key) + 1 AS next FROM CARDS", connection);
                dr["CD_Key"] = Convert.ToInt32(cmd.ExecuteScalar());
                Cards.Rows.Add(dr);
                adapter.Update(Cards);

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception cex)
            {
                ExceptionForm ef = new ExceptionForm(cex.ToString());
                ef.ShowDialog();
            }
        }


    }
}
