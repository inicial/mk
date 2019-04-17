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
    public partial class FilterClient : Form
    {
        public DataTable clients = new DataTable("clients");
        DataTable office = new DataTable("office");
        DataTable tbl_Country = new DataTable("tbl_Country");
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlConnection connection;
       /* string selstr = @"SELECT DISTINCT 
                      Clients.CL_KEY, Clients.CL_NAMERUS, Clients.CL_FNAMERUS, Clients.CL_SNAMERUS, Clients.CL_BIRTHDAY, Clients.CL_ADDRESS, Clients.cl_mail, 
                      UserList.US_FullName, tbl_Partners.PR_KEY, tbl_Partners.PR_NAME,  Lanta_AnketaAnswers.AA_Answer,  
                      Lanta_AnketaAnswers.AA_AnswerDetail,   DATEPART(dayofyear, CONVERT(DATETIME, '1980-' + STR(MONTH(Clients.CL_BIRTHDAY), 2) + '-' + STR(DAY(Clients.CL_BIRTHDAY), 2), 102)) 
                      AS CL_BIRTHDAY_DayOfYear, YEAR(Clients.CL_BIRTHDAY) AS CL_BIRTHDAY_YEAR
                    FROM         tbl_Turist LEFT OUTER JOIN
                      Lanta_AnketaAnswers ON tbl_Turist.TU_KEY = Lanta_AnketaAnswers.AA_TUKey AND Lanta_AnketaAnswers.AA_AQKey = 20 RIGHT OUTER JOIN
                      Clients ON tbl_Turist.TU_ID = Clients.CL_KEY LEFT OUTER JOIN
                      UserList ON Clients.CL_OPERUPDATE = UserList.US_KEY LEFT OUTER JOIN
                      tbl_Partners ON UserList.US_PRKEY = tbl_Partners.PR_KEY 
                    ORDER BY Clients.CL_NAMERUS";*/
        string selstr = @"SELECT DISTINCT 
                      Clients.CL_KEY, Clients.CL_NAMERUS, Clients.CL_FNAMERUS, Clients.CL_SNAMERUS, Clients.CL_BIRTHDAY, Clients.CL_ADDRESS, Clients.cl_mail,
                      Clients.CL_DATEUPDATE,
                      UserList.US_FullName, tbl_Partners.PR_KEY, tbl_Partners.PR_NAME, Lanta_AnketaAnswers.AA_Answer, Lanta_AnketaAnswers.AA_AnswerDetail, 
                      DATEPART(dayofyear, CONVERT(DATETIME, '1980-' + STR(MONTH(Clients.CL_BIRTHDAY), 2) + '-' + STR(DAY(Clients.CL_BIRTHDAY), 2), 102)) 
                      AS CL_BIRTHDAY_DayOfYear, YEAR(Clients.CL_BIRTHDAY) AS CL_BIRTHDAY_YEAR, Lanta_ClientStatDogovor.CSD_DGCNKEY,
tbl_Country.CN_NAME, Lanta_ClientStatDogovor.CSD_DGCODE, 
                      Lanta_ClientStatDogovor.CSD_DGTURDATE, Lanta_ClientStatDogovor.CSD_DGCRDATE
            FROM         Lanta_ClientStatDogovor INNER JOIN
                      tbl_Country ON Lanta_ClientStatDogovor.CSD_DGCNKEY = tbl_Country.CN_KEY RIGHT OUTER JOIN
                      Clients ON Lanta_ClientStatDogovor.CSD_CLКеу = Clients.CL_KEY LEFT OUTER JOIN
                      tbl_Turist INNER JOIN
                      Lanta_AnketaAnswers ON tbl_Turist.TU_KEY = Lanta_AnketaAnswers.AA_TUKey AND Lanta_AnketaAnswers.AA_AQKey = 20 ON 
                      Clients.CL_KEY = tbl_Turist.TU_ID LEFT OUTER JOIN
                      UserList ON Clients.CL_OPERUPDATE = UserList.US_KEY LEFT OUTER JOIN
                      tbl_Partners ON UserList.US_PRKEY = tbl_Partners.PR_KEY
            ORDER BY Clients.CL_NAMERUS";
        public int CLKEY = -1;
        /*
        public SelectClient()
        {
            InitializeComponent();
        }*/
        public FilterClient(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            dateTimePickerBTO.Value = DateTime.Now;
            dateTimePickerBFROM.Value = dateTimePickerBTO.Value.AddMonths(-1);
            dateTimePicker_YearTo.Value = dateTimePickerBTO.Value.AddYears(-18);
            RefreshData();
        }
        private void RefreshData()
        {
            Cursor.Current = Cursors.WaitCursor;
            clients.Rows.Clear();
            try
            {
                adapter.SelectCommand = new SqlCommand(selstr, connection);

                adapter.Fill(clients);

               
                adapter = new SqlDataAdapter(@"SELECT DISTINCT tbl_Partners.PR_KEY, tbl_Partners.PR_NAME
                FROM         tbl_Partners INNER JOIN
                      UserList ON tbl_Partners.PR_KEY = UserList.US_PRKEY
                        ORDER BY tbl_Partners.PR_KEY", connection);
                adapter.Fill(office);
                comboBox_OFFICE.ValueMember = "PR_KEY";
                comboBox_OFFICE.DisplayMember = "PR_NAME";
                comboBox_OFFICE.DataSource = office;

                adapter = new SqlDataAdapter(@"SELECT     CN_KEY, CN_NAME
                FROM         tbl_Country
                ORDER BY CN_NAME", connection);
                adapter.Fill(tbl_Country);
                comboBox_CN_NAME.ValueMember = "CN_KEY";
                comboBox_CN_NAME.DisplayMember = "CN_NAME";
                comboBox_CN_NAME.DataSource = tbl_Country;

            }
            finally { Cursor.Current = Cursors.Arrow; }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectClient_Load(object sender, EventArgs e)
        {
            /*
            if (clients.Rows.Count < 1)
            {

                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    adapter = new SqlDataAdapter(@"SELECT  Clients.CL_KEY, Clients.CL_NAMERUS, Clients.CL_FNAMERUS, Clients.CL_SNAMERUS, Clients.CL_BIRTHDAY, Clients.CL_PHONE, Clients.cl_mail, CARDS.CD_Code, CARDS.CD_Number, 
                      CARDS.CD_IsValid
                        FROM         Clients LEFT OUTER JOIN
                      CARDS ON Clients.CL_KEY = CARDS.CD_CLKey
                        ORDER BY Clients.CL_NAMERUS", connection);
                    adapter.Fill(clients);
                    clients.Columns.Add("CL_BIRTHDAY_STR");
                    foreach (DataRow dr in clients.Rows)
                    {
                        if (dr["CL_BIRTHDAY"] != System.DBNull.Value)
                            dr["CL_BIRTHDAY_STR"] = Convert.ToDateTime(dr["CL_BIRTHDAY"]).ToString("d");
                        else
                            dr["CL_BIRTHDAY_STR"] = "";
                    }

                }
                catch (Exception cex)
                {
                    ExceptionForm ef = new ExceptionForm(cex.ToString());
                    ef.ShowDialog();
                }
                finally { Cursor.Current = Cursors.Arrow; }
            }
            dataGridView_CLIENT.DataSource = clients;
            */
            dataGridView_CLIENT.DataSource = clients;
            dataGridView_CLIENT.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dataGridView_CLIENT.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {//Выбор
            if (dataGridView_CLIENT.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView_CLIENT.SelectedRows[0];
                CLKEY = Convert.ToInt32(row.Cells["CL_KEY"].Value);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void textBox_CL_NAMERUS_TextChanged(object sender, EventArgs e)
        {
            UseFilter();
        }


        private void UseFilter()
        {
            try
            {
                if (clients.Rows.Count < 1)
                    return;//Нечего фильтровать

                string filter = "";
                if (textBox_CL_NAMERUS.Text.Length > 0)
                    filter = filter + "AND " + "CL_NAMERUS like '" + textBox_CL_NAMERUS.Text + "%' ";
                if (textBox_CL_FNAMERUS.Text.Length > 0)
                    filter = filter + "AND " + "CL_FNAMERUS like '" + textBox_CL_FNAMERUS.Text + "%' ";
                if (textBox_CL_SNAMERUS.Text.Length > 0)
                    filter = filter + "AND " + "CL_SNAMERUS like '" + textBox_CL_SNAMERUS.Text + "%' ";


                if (checkBox_YearTo.Checked)
                {
                    filter = filter + "AND " + "CL_BIRTHDAY_YEAR <= " + Convert.ToString(dateTimePicker_YearTo.Value.Year) + " ";
                }
               

                if (checkBox_BFROM.Checked)
                {
                    DateTime visokos = new DateTime(1980, dateTimePickerBFROM.Value.Month, dateTimePickerBFROM.Value.Day);
                    filter = filter + "AND CL_BIRTHDAY_DayOfYear>=" + visokos.DayOfYear.ToString();
                }
                if (checkBox_BTO.Checked) //filter = filter + "AND " + "CL_BIRTHDAY < #" + dateTimePickerBTO.Value.Month.ToString() + "/" + dateTimePickerBTO.Value.Day.ToString() + "/" + dateTimePickerBTO.Value.Year.ToString() + "#";
                {
                    DateTime visokos = new DateTime(1980, dateTimePickerBTO.Value.Month, dateTimePickerBTO.Value.Day);
                    filter = filter + "AND CL_BIRTHDAY_DayOfYear<=" + visokos.DayOfYear.ToString();
                }

                
                if (checkBox_CL_ADDRESS.Checked)
                {
                    if (textBox_CL_ADDRESS.Text.Length > 0)
                        filter = filter + "AND " + "CL_ADDRESS like '%" + textBox_CL_ADDRESS.Text + "%' ";//OR
                    else
                        filter = filter + "AND " + "CL_ADDRESS NOT IS NULL AND CL_ADDRESS<>''";// +textBox_CL_ADDRESS.Text + "%' ";//OR

                }
                
                if (textBox_cl_mail.Text.Length > 0)
                    filter = filter + "AND " + "cl_mail like '%" + textBox_cl_mail.Text + "%' ";//OR
                if (checkBox_AGREE.CheckState==CheckState.Checked)
                    filter = filter + "AND " + "AA_AnswerDetail = 1";
                if (checkBox_AGREE.CheckState==CheckState.Unchecked)
                    filter = filter + "AND " + "AA_AnswerDetail = 0";
                if (checkBox_OFFICE.Checked)
                    filter = filter + "AND " + "PR_KEY = " + Convert.ToString(comboBox_OFFICE.SelectedValue);


                if (comboBox_CN_NAME.SelectedValue != null)
                {
                    int CN = Convert.ToInt32(comboBox_CN_NAME.SelectedValue);
                    if (CN!=0)
                        filter = filter + "AND " + "CSD_DGCNKEY = " + CN;
                }
                if (checkBox_CRUISE.Checked)
                    filter = filter + "AND " + "CSD_DGCNKEY = 1111111 ";
                if (checkBox_NOCRUISE.Checked)
                    filter = filter + "AND " + "CSD_DGCNKEY <> 1111111 OR CSD_DGCNKEY is NULL ";
                
                
                if (checkBox_CSD_DGTURDATE_FROM.Checked)
                    filter = filter + "AND " + "CSD_DGTURDATE >= #" + dateTimePicker_CSD_DGTURDATE_FROM.Value.Month.ToString() + "/" + dateTimePicker_CSD_DGTURDATE_FROM.Value.Day.ToString() + "/" + dateTimePicker_CSD_DGTURDATE_FROM.Value.Year.ToString() + "#";
                if (checkBox_CSD_DGTURDATE_TO.Checked)
                    filter = filter + "AND " + "CSD_DGTURDATE <= #" + dateTimePicker_CSD_DGTURDATE_TO.Value.Month.ToString() + "/" + dateTimePicker_CSD_DGTURDATE_TO.Value.Day.ToString() + "/" + dateTimePicker_CSD_DGTURDATE_TO.Value.Year.ToString() + "#";

                if (checkBox_CSD_DGCRDATE_FROM.Checked)
                    filter = filter + "AND " + "CSD_DGCRDATE >= #" + dateTimePicker_CSD_DGCRDATE_FROM.Value.Month.ToString() + "/" + dateTimePicker_CSD_DGCRDATE_FROM.Value.Day.ToString() + "/" + dateTimePicker_CSD_DGCRDATE_FROM.Value.Year.ToString() + "#";
                if (checkBox_CSD_DGCRDATE_TO.Checked)
                    filter = filter + "AND " + "CSD_DGCRDATE <= #" + dateTimePicker_CSD_DGCRDATE_TO.Value.Month.ToString() + "/" + dateTimePicker_CSD_DGCRDATE_TO.Value.Day.ToString() + "/" + dateTimePicker_CSD_DGCRDATE_TO.Value.Year.ToString() + "#";

                if (checkBox_CL_DATEUPDATE_FROM.Checked)
                    filter = filter + "AND " + "CL_DATEUPDATE >= #" + dateTimePicker_CL_DATEUPDATE_FROM.Value.Month.ToString() + "/" + dateTimePicker_CL_DATEUPDATE_FROM.Value.Day.ToString() + "/" + dateTimePicker_CL_DATEUPDATE_FROM.Value.Year.ToString() + "#";
               
                
                if (filter.StartsWith("AND"))
                    filter = filter.Substring(4);
                if (filter.StartsWith("OR"))
                    filter = filter.Substring(3);

                if (filter.Length > 0)
                {
                    DataRow[] drs = clients.Select(filter);
                    DataTable dt = clients.Clone();
                    foreach (DataRow dr in drs)
                        dt.Rows.Add(dr.ItemArray);
                    dataGridView_CLIENT.DataSource = dt;
                    label_COUNT.Text = "Кол-во: " + dt.Rows.Count.ToString();

                }
                else
                {
                    dataGridView_CLIENT.DataSource = clients;
                    label_COUNT.Text = "Кол-во: " + clients.Rows.Count.ToString();
                }
            }
            catch (Exception cex)
            {
                ExceptionForm ef = new ExceptionForm(cex.ToString());
                ef.ShowDialog();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox_CL_NAMERUS.Text = "";
            textBox_CL_FNAMERUS.Text = "";
            textBox_CL_SNAMERUS.Text = "";
            checkBox_YearTo.Checked = false;
            checkBox_BFROM.Checked = false;
            checkBox_CL_ADDRESS.Checked = false;
            checkBox_BTO.Checked = false;
            textBox_CL_ADDRESS.Text = "";
            textBox_cl_mail.Text = "";
            checkBox_AGREE.CheckState = CheckState.Indeterminate;
            checkBox_OFFICE.Checked = false;
            UseFilter();
        }
       
        private string GetFilterSQL()
        {
            string filter = "";
            if (textBox_CL_NAMERUS.Text.Length > 0)
                filter = filter + "AND " + "CL_NAMERUS like '" + textBox_CL_NAMERUS.Text + "%' ";
            if (textBox_CL_FNAMERUS.Text.Length > 0)
                filter = filter + "AND " + "CL_FNAMERUS like '" + textBox_CL_FNAMERUS.Text + "%' ";
            if (textBox_CL_SNAMERUS.Text.Length > 0)
                filter = filter + "AND " + "CL_SNAMERUS like '" + textBox_CL_SNAMERUS.Text + "%' ";
            
                 if (checkBox_YearTo.Checked)
                {
                    filter = filter + "AND " + "YEAR(Clients.CL_BIRTHDAY) <= " + Convert.ToString(dateTimePicker_YearTo.Value.Year) + " ";
                }

            if (checkBox_BFROM.Checked)
            {//filter = filter + "AND " + "CL_BIRTHDAY >= #" + dateTimePickerBFROM.Value.Month.ToString() + "/" + dateTimePickerBFROM.Value.Day.ToString() + "/" + dateTimePickerBFROM.Value.Year.ToString() + "#";
                DateTime visokos = new DateTime(1980, dateTimePickerBFROM.Value.Month, dateTimePickerBFROM.Value.Day);
                filter = filter + "AND DATEPART(dayofyear, CONVERT(DATETIME, '1980-' + STR(MONTH(Clients.CL_BIRTHDAY), 2) + '-' + STR(DAY(Clients.CL_BIRTHDAY), 2), 102)) >=" + visokos.DayOfYear.ToString();
            }
            if (checkBox_BTO.Checked) //filter = filter + "AND " + "CL_BIRTHDAY < #" + dateTimePickerBTO.Value.Month.ToString() + "/" + dateTimePickerBTO.Value.Day.ToString() + "/" + dateTimePickerBTO.Value.Year.ToString() + "#";
            {
                DateTime visokos = new DateTime(1980, dateTimePickerBTO.Value.Month, dateTimePickerBTO.Value.Day);
                filter = filter + "AND DATEPART(dayofyear, CONVERT(DATETIME, '1980-' + STR(MONTH(Clients.CL_BIRTHDAY), 2) + '-' + STR(DAY(Clients.CL_BIRTHDAY), 2), 102)) <=" + visokos.DayOfYear.ToString();
            }


            if (checkBox_CL_ADDRESS.Checked)
            {
                if (textBox_CL_ADDRESS.Text.Length > 0)
                    filter = filter + "AND " + "CL_ADDRESS like '%" + textBox_CL_ADDRESS.Text + "%' ";//OR
                else
                    filter = filter + "AND " + " (NOT (Clients.CL_ADDRESS IS NULL)) AND (CL_ADDRESS<>'')";// +textBox_CL_ADDRESS.Text + "%' ";//OR

            }

            if (textBox_cl_mail.Text.Length > 0)
                filter = filter + "AND " + "cl_mail like '%" + textBox_cl_mail.Text + "%' ";//OR
            if (checkBox_AGREE.CheckState == CheckState.Checked)
                filter = filter + "AND " + "AA_AnswerDetail = 1";
            if (checkBox_AGREE.CheckState == CheckState.Unchecked)
                filter = filter + "AND " + "AA_AnswerDetail = 0";
            if (checkBox_OFFICE.Checked)
                filter = filter + "AND " + "PR_KEY = " + Convert.ToString(comboBox_OFFICE.SelectedValue);
           
            if (comboBox_CN_NAME.SelectedValue != null)
            {
                int CN = Convert.ToInt32(comboBox_CN_NAME.SelectedValue);
                if (CN != 0)
                    filter = filter + "AND " + "CSD_DGCNKEY = " + CN;
            }

            if (checkBox_CRUISE.Checked)
                filter = filter + "AND " + "CSD_DGCNKEY = 1111111 ";
            if (checkBox_NOCRUISE.Checked)
                filter = filter + "AND " + "CSD_DGCNKEY <> 1111111 OR CSD_DGCNKEY is NULL ";

            if (checkBox_CSD_DGTURDATE_FROM.Checked)
            {
                //filter = filter + "AND " + "CSD_DGTURDATE >= #" + dateTimePicker_CSD_DGTURDATE_FROM.Value.Month.ToString() + "/" + dateTimePicker_CSD_DGTURDATE_FROM.Value.Day.ToString() + "/" + dateTimePicker_CSD_DGTURDATE_FROM.Value.Year.ToString() + "#";
                filter = filter + @"AND  CSD_DGTURDATE >= CONVERT(DATETIME, '"
                + dateTimePicker_CSD_DGTURDATE_FROM.Value.Year.ToString() + "-"
                + dateTimePicker_CSD_DGTURDATE_FROM.Value.Month.ToString() + "-"
                + dateTimePicker_CSD_DGTURDATE_FROM.Value.Day.ToString() + @"', 102)";
            }
            if (checkBox_CSD_DGTURDATE_TO.Checked)
            {
                //filter = filter + "AND " + "CSD_DGTURDATE <= #" + dateTimePicker_CSD_DGTURDATE_TO.Value.Month.ToString() + "/" + dateTimePicker_CSD_DGTURDATE_TO.Value.Day.ToString() + "/" + dateTimePicker_CSD_DGTURDATE_TO.Value.Year.ToString() + "#";
                filter = filter + @"AND  CSD_DGTURDATE <= CONVERT(DATETIME, '"
                + dateTimePicker_CSD_DGTURDATE_TO.Value.Year.ToString() + "-"
                + dateTimePicker_CSD_DGTURDATE_TO.Value.Month.ToString() + "-"
                + dateTimePicker_CSD_DGTURDATE_TO.Value.Day.ToString() + @"', 102)";
            }
            if (checkBox_CSD_DGCRDATE_FROM.Checked)
            {
                //filter = filter + "AND " + "CSD_DGCRDATE >= #" + dateTimePicker_CSD_DGCRDATE_FROM.Value.Month.ToString() + "/" + dateTimePicker_CSD_DGCRDATE_FROM.Value.Day.ToString() + "/" + dateTimePicker_CSD_DGCRDATE_FROM.Value.Year.ToString() + "#";
                filter = filter + @"AND  CSD_DGCRDATE >= CONVERT(DATETIME, '"
                    + dateTimePicker_CSD_DGCRDATE_FROM.Value.Year.ToString() + "-"
                    + dateTimePicker_CSD_DGCRDATE_FROM.Value.Month.ToString() + "-"
                    + dateTimePicker_CSD_DGCRDATE_FROM.Value.Day.ToString() + @"', 102)";
            }
            if (checkBox_CSD_DGCRDATE_TO.Checked)
            {
                //filter = filter + "AND " + "CSD_DGCRDATE <= #" + dateTimePicker_CSD_DGCRDATE_TO.Value.Month.ToString() + "/" + dateTimePicker_CSD_DGCRDATE_TO.Value.Day.ToString() + "/" + dateTimePicker_CSD_DGCRDATE_TO.Value.Year.ToString() + "#";
                filter = filter + @"AND  CSD_DGCRDATE <= CONVERT(DATETIME, '"
                    + dateTimePicker_CSD_DGCRDATE_TO.Value.Year.ToString() + "-"
                    + dateTimePicker_CSD_DGCRDATE_TO.Value.Month.ToString() + "-"
                    + dateTimePicker_CSD_DGCRDATE_TO.Value.Day.ToString() + @"', 102)";
            }
            if (checkBox_CL_DATEUPDATE_FROM.Checked)
            {
                filter = filter + @"AND  CL_DATEUPDATE >= CONVERT(DATETIME, '"
                    + dateTimePicker_CL_DATEUPDATE_FROM.Value.Year.ToString() + "-"
                    + dateTimePicker_CL_DATEUPDATE_FROM.Value.Month.ToString() + "-"
                    + dateTimePicker_CL_DATEUPDATE_FROM.Value.Day.ToString() + @"', 102)";
            }
            if (filter.StartsWith("AND"))
                filter = filter.Substring(4);
            if (filter.StartsWith("OR"))
                filter = filter.Substring(3);
            if (filter.Length > 0)
            {
                filter = " WHERE " + filter;
            }

            return filter;
        }
        private void отчётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(selstr, connection);
            cmd.CommandText = cmd.CommandText.Replace("ORDER BY", GetFilterSQL() + " ORDER BY"); 
            ReportSendList rsl = new ReportSendList(cmd);
            rsl.Show();
        }

    }
}
