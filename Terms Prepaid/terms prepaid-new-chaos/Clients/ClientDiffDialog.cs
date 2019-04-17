using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using lanta.Clients;

namespace lanta.DirectClientDogovor
{
    public partial class ClientDiffDialog : Form
    {
        DataRow TempClient,  ConstClient;
        DataTable Clients = new DataTable("Clients");
        bool ExistInClients;
        SqlDataAdapter adapter;
        SqlConnection connection;
        int tempCL_KEY;
        long CL_KEY = -1;
        int CL_DupUserKey;
        long MANAGER_ID;

        public ClientDiffDialog()
        {
            InitializeComponent();
        }
        public ClientDiffDialog(DataRow TempClient, int CL_KEY, int CL_DupUserKey, SqlDataAdapter adapter, long MANAGER_ID, bool ExistInClients)
        {
            InitializeComponent();
            this.TempClient = TempClient;
            this.ExistInClients = ExistInClients;
            this.adapter = adapter;
            connection = adapter.SelectCommand.Connection;
            tempCL_KEY = Convert.ToInt32(TempClient["CL_KEY"]);
            this.CL_KEY = CL_KEY;
            this.CL_DupUserKey = CL_DupUserKey;
            this.MANAGER_ID = MANAGER_ID;
            adapter.SelectCommand.CommandText = @"SELECT * FROM  Clients WHERE  CL_KEY = " + CL_KEY.ToString();
            adapter.Fill(Clients);
            if (Clients.Rows.Count > 0)
                this.ConstClient = Clients.Rows[0];
           
            PopulateFields();
            MergeProcedure();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public void UnionClients()
        {
            try
            {
                ReadFields(ConstClient);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                SqlTransaction transaction = connection.BeginTransaction("Transaction");

                //Обновление мигрированных данных
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.SelectCommand = new SqlCommand("select * from clients where CL_KEY=" + Convert.ToString(CL_KEY), connection);
                adapter.SelectCommand.Transaction = transaction;
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.UpdateCommand.CommandText = "UPDATE [clients] SET  [CL_OPERUPDATE] = @p2, [CL_DATEUPDATE] = @p3, [CL_PFKEY] = @p4, [CL_NAMERUS] = @p5, [CL_NAMELAT] = @p6, [CL_SHORTNAME] = @p7, [CL_SEX] = @p8, [CL_FNAMERUS] = @p9, [CL_FNAMELAT] = @p10, [CL_SNAMERUS] = @p11, [CL_SNAMELAT] = @p12, [CL_BIRTHDAY] = @p13, [CL_BIRTHCOUNTRY] = @p14, [CL_BIRTHCITY] = @p15, [CL_CITIZEN] = @p16, [CL_ADDRESS] = @p17, [CL_POSTINDEX] = @p18, [CL_POSTCITY] = @p19, [CL_POSTSTREET] = @p20, [CL_POSTBILD] = @p21, [CL_POSTFLAT] = @p22, [CL_PHONE] = @p23, [CL_PASPORTSER] = @p24, [CL_PASPORTNUM] = @p25, [CL_PASPORTDATE] = @p26, [CL_PASPORTDATEEND] = @p27, [CL_PASPORTBYWHOM] = @p28, [CL_PASPRUSER] = @p29, [CL_PASPRUNUM] = @p30, [CL_PASPRUDATE] = @p31, [CL_PASPRUBYWHOM] = @p32, [CL_ISMARK] = @p33, [CL_TYPE] = @p34, [CL_IMPRESSNOTE] = @p35, [CL_NOTE] = @p36, [CL_REMARK] = @p37, [CL_IMPRESSKEY] = @p38, [CL_TITLE1] = @p39, [CL_TITLE2] = @p40, [CL_TITLE3] = @p41, [CL_TITLE4] = @p42, [CL_FUTURE] = @p43, [CL_LASTSTAT] = @p44, [CL_SUMMA] = @p45, [CL_NMENWITH] = @p46, [CL_SUMDOGOVOR] = @p47, [CL_NTRIP] = @p48, [cl_fax] = @p49, [cl_mail] = @p50, [CL_MINCOST] = @p51, [CL_MAXCOST] = @p52, [CL_DSKEY] = @p53, [CL_RealSex] = @p54 WHERE [CL_KEY] = @p1";
                adapter.UpdateCommand.Transaction = transaction;
                adapter.Update(Clients);


                SqlCommand cmd;
                if (!ExistInClients)
                {
                    //Проверяем, есть или доступ у привязанного клиента
                    DataTable Lanta_ClientAccess = new DataTable("Lanta_ClientAccess");
                    adapter.SelectCommand.CommandText = @"SELECT CA_ID, CA_CLКеу, CA_DUPUSERKEY, CA_MANAGER, CA_CREATEDATE
                         FROM  Lanta_ClientAccess WHERE  CA_CLКеу = " + CL_KEY.ToString();
                    adapter.SelectCommand.Transaction = transaction;
                    adapter.Fill(Lanta_ClientAccess);

                    //для каждого старого доступа перевешиваем путёвки на новый аккаунт и удаляем старый 
                    string oldDupUser = "-1";
                    foreach (DataRow dr in Lanta_ClientAccess.Rows)
                    {
                        oldDupUser = Convert.ToString(dr["CA_DUPUSERKEY"]);
                        //перевешивание на новый аккаунт
                        cmd = new SqlCommand("", connection);
                        cmd.CommandText = @"UPDATE tbl_Dogovor SET DG_DUPUSERKEY=" + CL_DupUserKey.ToString() + " WHERE DG_DUPUSERKEY=" + oldDupUser;
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();

                        //удаление
                        cmd = new SqlCommand("delete FROM  DUP_USER where US_KEY = " + oldDupUser, connection);
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();


                    }
                    //Удаление информации о старых аккаунтах
                    cmd = new SqlCommand("delete FROM  Lanta_ClientAccess where CA_CLКеу = " + CL_KEY.ToString(), connection);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();


                    //Для вновь добавленного клиента вставляем доступ  к личному кабинету как у нового логина
                    cmd = new SqlCommand("", connection);
                    cmd.CommandText = @"INSERT INTO Lanta_ClientAccess(CA_CLКеу, CA_DUPUSERKEY, CA_MANAGER)
                        VALUES     (" + CL_KEY.ToString() + "," + CL_DupUserKey.ToString() + "," + MANAGER_ID.ToString() + ")";
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();

                    //Активируем пароль
                    cmd = new SqlCommand("", connection);
                    cmd.CommandText = @"UPDATE DUP_USER SET US_AGENT=1,US_REG=1,US_TURAGENT=1 WHERE US_KEY=" + CL_DupUserKey.ToString();
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();

                }

                //Если есть в постоянных - во временных можно удалить
                cmd = new SqlCommand("delete FROM Lanta_TempClients where CL_KEY = " + tempCL_KEY.ToString(), connection);
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UnionClients();
            this.DialogResult = DialogResult.OK;
        }
        
        private void ReadFields(DataRow dr)
        {

            foreach (DataColumn dc in Clients.Columns)
            {
                switch (dc.ColumnName)
                {
                    /*case "CL_PFKEY":
                        if (comboBox_CL_PFKEY.SelectedValue != null)
                            dr["CL_PFKEY"] = comboBox_CL_PFKEY.SelectedValue;
                        else
                            dr["CL_PFKEY"] = System.DBNull.Value;
                        break;*/
                    case "CL_CITIZEN":
                        dr["CL_CITIZEN"] = textBox3_CL_CITIZEN.Text;
                        break;
                    case "CL_PASPRUSER":
                        dr["CL_PASPRUSER"] = textBox3_CL_PASPRUSER.Text;
                        break;
                    case "CL_PASPRUNUM":
                        dr["CL_PASPRUNUM"] = textBox3_CL_PASPRUNUM.Text;
                        break;
                    case "CL_NAMERUS":
                        dr["CL_NAMERUS"] = textBox3_CL_NAMERUS.Text;
                        break;
                    case "CL_FNAMERUS":
                        dr["CL_FNAMERUS"] = textBox3_CL_FNAMERUS.Text;
                        break;
                    case "CL_SNAMERUS":
                        dr["CL_SNAMERUS"] = textBox3_CL_SNAMERUS.Text;
                        break;
                    case "CL_SHORTNAME":
                        dr["CL_SHORTNAME"] = "";
                        if (textBox3_CL_FNAMERUS.Text.Length > 0)
                            dr["CL_SHORTNAME"] =  textBox3_CL_FNAMERUS.Text[0] + ".";
                        if (textBox3_CL_SNAMERUS.Text.Length > 0)
                            dr["CL_SHORTNAME"] ="" + dr["CL_SHORTNAME"] + textBox3_CL_SNAMERUS.Text[0] + ".";
                        break;
                    /*
                    case "CL_RealSex":
                        dr["CL_RealSex"] = Convert.ToInt16(radioButton2.Checked);
                        break;
                    case "CL_SEX":
                        if (checkBox1_CL_SEX.Checked)
                            dr["CL_SEX"] = 2;
                        else
                            if (checkBox2_CL_SEX.Checked)
                                dr["CL_SEX"] = 3;
                            else
                                dr["CL_SEX"] = Convert.ToInt16(radioButton2.Checked);
                        break;*/
                    case "CL_BIRTHDAY":
                        DateTime ret;
                        if (DateTime.TryParse(textBox3_CL_BIRTHDAY.Text, out ret))
                            dr["CL_BIRTHDAY"] = ret;
                        break;
                    case "CL_BIRTHCITY":
                        dr["CL_BIRTHCITY"] = textBox3_CL_BIRTHCITY.Text;
                        break;
                    case "CL_BIRTHCOUNTRY":
                        dr["CL_BIRTHCOUNTRY"] = textBox3_CL_BIRTHCOUNTRY.Text;
                        break;
                    case "CL_PASPRUBYWHOM":
                        dr["CL_PASPRUBYWHOM"] = textBox3_CL_PASPRUBYWHOM.Text;
                        break;
                    case "CL_PASPRUDATE":
                        DateTime ret_CL_PASPRUDATE;
                        if (DateTime.TryParse(textBox3_CL_PASPRUDATE.Text, out ret_CL_PASPRUDATE))
                            dr["CL_PASPRUDATE"] = ret_CL_PASPRUDATE;
                        break;
                    case "CL_POSTINDEX":
                        dr["CL_POSTINDEX"] = textBox3_CL_POSTINDEX.Text;
                        break;
                    case "CL_POSTCITY":
                        dr["CL_POSTCITY"] = textBox3_CL_POSTCITY.Text;
                        break;
                    case "CL_POSTSTREET":
                        dr["CL_POSTSTREET"] = textBox3_CL_POSTSTREET.Text;
                        break;
                    case "CL_POSTBILD":
                        dr["CL_POSTBILD"] = textBox3_CL_POSTBILD.Text;
                        break;
                    case "CL_POSTFLAT":
                        dr["CL_POSTFLAT"] = textBox3_CL_POSTFLAT.Text;
                        break;
                    case "CL_ADDRESS":
                        string addr = "";
                        if (textBox3_CL_POSTINDEX.Text.Length > 0)
                            addr += textBox3_CL_POSTINDEX.Text + ", ";
                        addr += textBox3_CL_POSTCITY.Text + ", ";
                        addr += textBox3_CL_POSTSTREET.Text + ", ";
                        addr += "д." + textBox3_CL_POSTBILD.Text + ", ";
                        addr += "кв." + textBox3_CL_POSTFLAT.Text;
                        dr["CL_ADDRESS"] = addr;
                        break;
                    case "CL_PHONE":
                        dr["CL_PHONE"] = textBox3_CL_PHONE.Text;
                        break;
                    case "CL_PASPORTSER":
                        dr["CL_PASPORTSER"] = textBox3_CL_PASPORTSER.Text;
                        break;
                    case "CL_PASPORTNUM":
                        dr["CL_PASPORTNUM"] = textBox3_CL_PASPORTNUM.Text;
                        break;
                    case "CL_NAMELAT":
                        dr["CL_NAMELAT"] = textBox3_CL_NAMELAT.Text;
                        break;
                    case "CL_FNAMELAT":
                        dr["CL_FNAMELAT"] = textBox3_CL_FNAMELAT.Text;
                        break;
                    case "CL_SNAMELAT":
                        dr["CL_SNAMELAT"] = textBox3_CL_SNAMELAT.Text;
                        break;
                    case "CL_PASPORTDATE":
                        DateTime ret2;
                        if (DateTime.TryParse(textBox3_CL_PASPORTDATE.Text, out ret2))
                            dr["CL_PASPORTDATE"] = ret2;
                        break;
                    case "CL_PASPORTDATEEND":
                        DateTime ret3;
                        if (DateTime.TryParse(textBox3_CL_PASPORTDATEEND.Text, out ret3))
                            dr["CL_PASPORTDATEEND"] = ret3;
                        break;
                    case "CL_PASPORTBYWHOM":
                        dr["CL_PASPORTBYWHOM"] = textBox3_CL_PASPORTBYWHOM.Text;
                        break;
                        /*
                    case "CL_ISMARK":
                        int markValue = 0;
                        if (checkBox1_CL_ISMARK.Checked)
                            markValue += 1;
                        if (checkBox2_CL_ISMARK.Checked)
                            markValue += 2;
                        if (checkBox3_CL_ISMARK.Checked)
                            markValue += 4;
                        if (checkBox4_CL_ISMARK.Checked)
                            markValue += 8;
                        if (checkBox5_CL_ISMARK.Checked)
                            markValue += 16;
                        dr["CL_ISMARK"] = markValue;
                        break;
                    case "CL_TYPE":
                        int TypeValue = 0;
                        for (int i = 0; i < listBox_CL_TYPE.SelectedIndices.Count; i++)
                            TypeValue += Convert.ToInt32(Math.Pow(2, listBox_CL_TYPE.SelectedIndices[i]));
                        dr["CL_TYPE"] = TypeValue;
                        break;
                    case "CL_IMPRESSNOTE":
                        dr["CL_IMPRESSNOTE"] = textBox_CL_IMPRESSNOTE.Text;
                        break;
                    case "CL_NOTE":
                        dr["CL_NOTE"] = textBox_CL_NOTE.Text;
                        break;
                    case "CL_REMARK":
                        dr["CL_REMARK"] = textBox_CL_REMARK.Text;
                        break;
                    case "CL_IMPRESSKEY":
                        int IMPRESSKEY_Value = 0;
                        for (int i = 0; i < listBox_CL_IMPRESSKEY.SelectedIndices.Count; i++)
                            IMPRESSKEY_Value += Convert.ToInt32(Math.Pow(2, listBox_CL_IMPRESSKEY.SelectedIndices[i]));
                        dr["CL_IMPRESSKEY"] = IMPRESSKEY_Value;
                        break;
                    case "CL_FUTURE":
                        dr["CL_FUTURE"] = textBox_CL_FUTURE.Text;
                        break;
                    case "CL_TITLE1":
                        dr["CL_TITLE1"] = textBox_CL_TITLE1.Text;
                        break;
                    case "CL_TITLE2":
                        dr["CL_TITLE2"] = textBox_CL_TITLE2.Text;
                        break;
                    case "CL_TITLE3":
                        dr["CL_TITLE3"] = textBox_CL_TITLE3.Text;
                        break;
                    case "CL_TITLE4":
                        dr["CL_TITLE4"] = textBox_CL_TITLE4.Text;
                        break;
                    case "CL_LASTSTAT":
                        DateTime ret_CL_LASTSTAT;
                        if (DateTime.TryParse(textBox_CL_LASTSTAT.Text, out ret_CL_LASTSTAT))
                            dr["CL_LASTSTAT"] = ret_CL_LASTSTAT;
                        break;
                    case "CL_SUMMA":
                        Double ret_CL_SUMMA;
                        if (Double.TryParse(textBox_CL_SUMMA.Text, out ret_CL_SUMMA))
                            dr["CL_SUMMA"] = ret_CL_SUMMA;
                        break;
                    case "CL_NTRIP":
                        Int32 ret_CL_NTRIP;
                        if (Int32.TryParse(textBox_CL_NTRIP.Text, out ret_CL_NTRIP))
                            dr["CL_NTRIP"] = ret_CL_NTRIP;
                        break;
                    case "CL_MINCOST":
                        Double ret_CL_MINCOST;
                        if (Double.TryParse(textBox_CL_MINCOST.Text, out ret_CL_MINCOST))
                            dr["CL_MINCOST"] = ret_CL_MINCOST;
                        break;
                    case "CL_SUMDOGOVOR":
                        Double ret_CL_SUMDOGOVOR;
                        if (Double.TryParse(textBox_CL_SUMDOGOVOR.Text, out ret_CL_SUMDOGOVOR))
                            dr["CL_SUMDOGOVOR"] = ret_CL_SUMDOGOVOR;
                        break;
                    case "CL_NMENWITH":
                        Int32 ret_CL_NMENWITH;
                        if (Int32.TryParse(textBox_CL_NMENWITH.Text, out ret_CL_NMENWITH))
                            dr["CL_NMENWITH"] = ret_CL_NMENWITH;
                        break;
                    case "CL_MAXCOST":
                        Double ret_CL_MAXCOST;
                        if (Double.TryParse(textBox_CL_MAXCOST.Text, out ret_CL_MAXCOST))
                            dr["CL_MAXCOST"] = ret_CL_MAXCOST;
                        break;*/
                    case "cl_mail":
                        dr["cl_mail"] = textBox3_cl_mail.Text;
                        break;
                    case "cl_fax":
                        dr["cl_fax"] = textBox3_cl_fax.Text;
                        break;
                }
            }

        }
        private void PopulateFields()
        {
            DataRow dr1, dr2;

            dr1 = TempClient;
            dr2 = ConstClient;
            foreach (DataColumn dc in Clients.Columns)
            {
                #region Заполнение клиента 1
                if (dr1[dc.ColumnName] != System.DBNull.Value)
                {
                    switch (dc.ColumnName)
                    {
                        case "CL_KEY":
                            textBox1_CL_KEY.Text = Convert.ToString(dr1["CL_KEY"]);
                            break;
                        case "CL_DATEUPDATE":
                            textBox1_CL_DATEUPDATE.Text = Convert.ToString(dr1["CL_DATEUPDATE"]);
                            break;
                        /*
                        case "CL_PFKEY":
                            comboBox_CL_PFKEY.SelectedValue = Convert.ToInt64(dr["CL_PFKEY"]);
                            break;*/
                        case "CL_CITIZEN":
                            textBox1_CL_CITIZEN.Text = Convert.ToString(dr1["CL_CITIZEN"]);
                            break;
                        case "CL_PASPRUSER":
                            textBox1_CL_PASPRUSER.Text = Convert.ToString(dr1["CL_PASPRUSER"]);
                            break;
                        case "CL_PASPRUNUM":
                            textBox1_CL_PASPRUNUM.Text = Convert.ToString(dr1["CL_PASPRUNUM"]);
                            break;
                        case "CL_NAMERUS":
                            textBox1_CL_NAMERUS.Text = Convert.ToString(dr1["CL_NAMERUS"]);
                            break;
                        case "CL_FNAMERUS":
                            textBox1_CL_FNAMERUS.Text = Convert.ToString(dr1["CL_FNAMERUS"]);
                            break;
                        case "CL_SNAMERUS":
                            textBox1_CL_SNAMERUS.Text = Convert.ToString(dr1["CL_SNAMERUS"]);
                            break;
                        /*
                        case "CL_SEX":
                            checkBox1_CL_SEX.Checked = Convert.ToInt16(dr["CL_SEX"]) == 2;
                            checkBox2_CL_SEX.Checked = Convert.ToInt16(dr["CL_SEX"]) == 3;
                            textBox_CL_SEX.Text = Convert.ToString(dr["CL_SEX"]);
                            break;
                        case "CL_RealSex":
                            radioButton2.Checked = !(radioButton1.Checked = Convert.ToInt16(dr["CL_RealSex"]) == 0);
                            textBox_CL_RealSex.Text = Convert.ToString(dr["CL_RealSex"]);
                            break;*/
                        case "CL_BIRTHDAY":
                            textBox1_CL_BIRTHDAY.Text = Convert.ToDateTime(dr1["CL_BIRTHDAY"]).ToString("d");
                            break;
                        case "CL_BIRTHCITY":
                            textBox1_CL_BIRTHCITY.Text = Convert.ToString(dr1["CL_BIRTHCITY"]);
                            break;
                        case "CL_BIRTHCOUNTRY":
                            textBox1_CL_BIRTHCOUNTRY.Text = Convert.ToString(dr1["CL_BIRTHCOUNTRY"]);
                            break;
                        case "CL_PASPRUBYWHOM":
                            textBox1_CL_PASPRUBYWHOM.Text = Convert.ToString(dr1["CL_PASPRUBYWHOM"]);
                            break;
                        case "CL_PASPRUDATE":
                            textBox1_CL_PASPRUDATE.Text = Convert.ToDateTime(dr1["CL_PASPRUDATE"]).ToString("d");
                            break;
                        case "CL_POSTINDEX":
                            textBox1_CL_POSTINDEX.Text = Convert.ToString(dr1["CL_POSTINDEX"]);
                            break;
                        case "CL_POSTCITY":
                            textBox1_CL_POSTCITY.Text = Convert.ToString(dr1["CL_POSTCITY"]);
                            break;
                        case "CL_POSTSTREET":
                            textBox1_CL_POSTSTREET.Text = Convert.ToString(dr1["CL_POSTSTREET"]);
                            break;
                        case "CL_POSTBILD":
                            textBox1_CL_POSTBILD.Text = Convert.ToString(dr1["CL_POSTBILD"]);
                            break;
                        case "CL_POSTFLAT":
                            textBox1_CL_POSTFLAT.Text = Convert.ToString(dr1["CL_POSTFLAT"]);
                            break;
                        case "CL_PHONE":
                            textBox1_CL_PHONE.Text = Convert.ToString(dr1["CL_PHONE"]);
                            break;
                        case "CL_PASPORTSER":
                            textBox1_CL_PASPORTSER.Text = Convert.ToString(dr1["CL_PASPORTSER"]);
                            break;
                        case "CL_PASPORTNUM":
                            textBox1_CL_PASPORTNUM.Text = Convert.ToString(dr1["CL_PASPORTNUM"]);
                            break;
                        case "CL_NAMELAT":
                            textBox1_CL_NAMELAT.Text = Convert.ToString(dr1["CL_NAMELAT"]);
                            break;
                        case "CL_FNAMELAT":
                            textBox1_CL_FNAMELAT.Text = Convert.ToString(dr1["CL_FNAMELAT"]);
                            break;
                        case "CL_SNAMELAT":
                            textBox1_CL_SNAMELAT.Text = Convert.ToString(dr1["CL_SNAMELAT"]);
                            break;
                        case "CL_PASPORTDATE":
                            textBox1_CL_PASPORTDATE.Text = Convert.ToDateTime(dr1["CL_PASPORTDATE"]).ToString("d");
                            break;
                        case "CL_PASPORTDATEEND":
                            textBox1_CL_PASPORTDATEEND.Text = Convert.ToDateTime(dr1["CL_PASPORTDATEEND"]).ToString("d");
                            break;
                        case "CL_PASPORTBYWHOM":
                            textBox1_CL_PASPORTBYWHOM.Text = Convert.ToString(dr1["CL_PASPORTBYWHOM"]);
                            break;
                        /*
                        case "CL_ISMARK":
                            string bit = Convert.ToString(Convert.ToUInt32(dr["CL_ISMARK"]), 2).PadLeft(5, '0');
                            checkBox1_CL_ISMARK.Checked = bit[4] == '1';
                            checkBox2_CL_ISMARK.Checked = bit[3] == '1';
                            checkBox3_CL_ISMARK.Checked = bit[2] == '1';
                            checkBox4_CL_ISMARK.Checked = bit[1] == '1';
                            checkBox5_CL_ISMARK.Checked = bit[0] == '1';
                            break;
                        case "CL_TYPE":
                            string bit_CL_TYPE = Convert.ToString(Convert.ToUInt32(dr["CL_TYPE"]), 2).PadLeft(12, '0');
                            for (int i = 0; i < bit_CL_TYPE.Length; i++)
                                listBox_CL_TYPE.SetSelected(bit_CL_TYPE.Length - i - 1, bit_CL_TYPE[i] == '1');
                            break;
                        case "CL_IMPRESSNOTE":
                            textBox_CL_IMPRESSNOTE.Text = Convert.ToString(dr["CL_IMPRESSNOTE"]);
                            break;
                        case "CL_NOTE":
                            textBox_CL_NOTE.Text = Convert.ToString(dr["CL_NOTE"]);
                            break;
                        case "CL_REMARK":
                            textBox_CL_REMARK.Text = Convert.ToString(dr["CL_REMARK"]);
                            break;
                        case "CL_IMPRESSKEY":
                            string bit_CL_IMPRESSKEY = Convert.ToString(Convert.ToUInt32(dr["CL_IMPRESSKEY"]), 2).PadLeft(12, '0');
                            for (int i = 0; i < bit_CL_IMPRESSKEY.Length; i++)
                                listBox_CL_IMPRESSKEY.SetSelected(bit_CL_IMPRESSKEY.Length - i - 1, bit_CL_IMPRESSKEY[i] == '1');
                            break;
                        case "CL_FUTURE":
                            textBox_CL_FUTURE.Text = Convert.ToString(dr["CL_FUTURE"]);
                            break;
                        case "CL_TITLE1":
                            textBox_CL_TITLE1.Text = Convert.ToString(dr["CL_TITLE1"]);
                            break;
                        case "CL_TITLE2":
                            textBox_CL_TITLE2.Text = Convert.ToString(dr["CL_TITLE2"]);
                            break;
                        case "CL_TITLE3":
                            textBox_CL_TITLE3.Text = Convert.ToString(dr["CL_TITLE3"]);
                            break;
                        case "CL_TITLE4":
                            textBox_CL_TITLE4.Text = Convert.ToString(dr["CL_TITLE4"]);
                            break;
                        case "CL_LASTSTAT":
                            textBox_CL_LASTSTAT.Text = Convert.ToDateTime(dr["CL_LASTSTAT"]).ToString("g");
                            break;
                        case "CL_SUMMA":
                            textBox_CL_SUMMA.Text = Convert.ToDouble(dr["CL_SUMMA"]).ToString("F2");
                            break;
                        case "CL_NTRIP":
                            textBox_CL_NTRIP.Text = Convert.ToString(dr["CL_NTRIP"]);
                            break;
                        case "CL_MINCOST":
                            textBox_CL_MINCOST.Text = Convert.ToDouble(dr["CL_MINCOST"]).ToString("F2");
                            break;
                        case "CL_SUMDOGOVOR":
                            textBox_CL_SUMDOGOVOR.Text = Convert.ToDouble(dr["CL_SUMDOGOVOR"]).ToString("F2");
                            break;
                        case "CL_NMENWITH":
                            textBox_CL_NMENWITH.Text = Convert.ToString(dr["CL_NMENWITH"]);
                            break;
                        case "CL_MAXCOST":
                            textBox_CL_MAXCOST.Text = Convert.ToDouble(dr["CL_MAXCOST"]).ToString("F2");
                            break;*/
                        case "cl_mail":
                            textBox1_cl_mail.Text = Convert.ToString(dr1["cl_mail"]);
                            break;
                        case "cl_fax":
                            textBox1_cl_fax.Text = Convert.ToString(dr1["cl_fax"]);
                            break;
                    }
                }
                #endregion
                #region Заполнение клиента 2
                if (dr2[dc.ColumnName] != System.DBNull.Value)
                {
                    switch (dc.ColumnName)
                    {
                        case "CL_KEY":
                            textBox2_CL_KEY.Text = Convert.ToString(dr2["CL_KEY"]);
                            break;
                        case "CL_DATEUPDATE":
                            textBox2_CL_DATEUPDATE.Text = Convert.ToString(dr2["CL_DATEUPDATE"]);
                            break;
                        /*
                        case "CL_PFKEY":
                            comboBox_CL_PFKEY.SelectedValue = Convert.ToInt64(dr["CL_PFKEY"]);
                            break;*/
                        case "CL_CITIZEN":
                            textBox2_CL_CITIZEN.Text = Convert.ToString(dr2["CL_CITIZEN"]);
                            break;
                        case "CL_PASPRUSER":
                            textBox2_CL_PASPRUSER.Text = Convert.ToString(dr2["CL_PASPRUSER"]);
                            break;
                        case "CL_PASPRUNUM":
                            textBox2_CL_PASPRUNUM.Text = Convert.ToString(dr2["CL_PASPRUNUM"]);
                            break;
                        case "CL_NAMERUS":
                            textBox2_CL_NAMERUS.Text = Convert.ToString(dr2["CL_NAMERUS"]);
                            break;
                        case "CL_FNAMERUS":
                            textBox2_CL_FNAMERUS.Text = Convert.ToString(dr2["CL_FNAMERUS"]);
                            break;
                        case "CL_SNAMERUS":
                            textBox2_CL_SNAMERUS.Text = Convert.ToString(dr2["CL_SNAMERUS"]);
                            break;
                        /*
                        case "CL_SEX":
                            checkBox1_CL_SEX.Checked = Convert.ToInt16(dr["CL_SEX"]) == 2;
                            checkBox2_CL_SEX.Checked = Convert.ToInt16(dr["CL_SEX"]) == 3;
                            textBox_CL_SEX.Text = Convert.ToString(dr["CL_SEX"]);
                            break;
                        case "CL_RealSex":
                            radioButton2.Checked = !(radioButton1.Checked = Convert.ToInt16(dr["CL_RealSex"]) == 0);
                            textBox_CL_RealSex.Text = Convert.ToString(dr["CL_RealSex"]);
                            break;*/
                        case "CL_BIRTHDAY":
                            textBox2_CL_BIRTHDAY.Text = Convert.ToDateTime(dr2["CL_BIRTHDAY"]).ToString("d");
                            break;
                        case "CL_BIRTHCITY":
                            textBox2_CL_BIRTHCITY.Text = Convert.ToString(dr2["CL_BIRTHCITY"]);
                            break;
                        case "CL_BIRTHCOUNTRY":
                            textBox2_CL_BIRTHCOUNTRY.Text = Convert.ToString(dr2["CL_BIRTHCOUNTRY"]);
                            break;
                        case "CL_PASPRUBYWHOM":
                            textBox2_CL_PASPRUBYWHOM.Text = Convert.ToString(dr2["CL_PASPRUBYWHOM"]);
                            break;
                        case "CL_PASPRUDATE":
                            textBox2_CL_PASPRUDATE.Text = Convert.ToDateTime(dr2["CL_PASPRUDATE"]).ToString("d");
                            break;
                        case "CL_POSTINDEX":
                            textBox2_CL_POSTINDEX.Text = Convert.ToString(dr2["CL_POSTINDEX"]);
                            break;
                        case "CL_POSTCITY":
                            textBox2_CL_POSTCITY.Text = Convert.ToString(dr2["CL_POSTCITY"]);
                            break;
                        case "CL_POSTSTREET":
                            textBox2_CL_POSTSTREET.Text = Convert.ToString(dr2["CL_POSTSTREET"]);
                            break;
                        case "CL_POSTBILD":
                            textBox2_CL_POSTBILD.Text = Convert.ToString(dr2["CL_POSTBILD"]);
                            break;
                        case "CL_POSTFLAT":
                            textBox2_CL_POSTFLAT.Text = Convert.ToString(dr2["CL_POSTFLAT"]);
                            break;
                        case "CL_PHONE":
                            textBox2_CL_PHONE.Text = Convert.ToString(dr2["CL_PHONE"]);
                            break;
                        case "CL_PASPORTSER":
                            textBox2_CL_PASPORTSER.Text = Convert.ToString(dr2["CL_PASPORTSER"]);
                            break;
                        case "CL_PASPORTNUM":
                            textBox2_CL_PASPORTNUM.Text = Convert.ToString(dr2["CL_PASPORTNUM"]);
                            break;
                        case "CL_NAMELAT":
                            textBox2_CL_NAMELAT.Text = Convert.ToString(dr2["CL_NAMELAT"]);
                            break;
                        case "CL_FNAMELAT":
                            textBox2_CL_FNAMELAT.Text = Convert.ToString(dr2["CL_FNAMELAT"]);
                            break;
                        case "CL_SNAMELAT":
                            textBox2_CL_SNAMELAT.Text = Convert.ToString(dr2["CL_SNAMELAT"]);
                            break;
                        case "CL_PASPORTDATE":
                            textBox2_CL_PASPORTDATE.Text = Convert.ToDateTime(dr2["CL_PASPORTDATE"]).ToString("d");
                            break;
                        case "CL_PASPORTDATEEND":
                            textBox2_CL_PASPORTDATEEND.Text = Convert.ToDateTime(dr2["CL_PASPORTDATEEND"]).ToString("d");
                            break;
                        case "CL_PASPORTBYWHOM":
                            textBox2_CL_PASPORTBYWHOM.Text = Convert.ToString(dr2["CL_PASPORTBYWHOM"]);
                            break;
                        /*
                        case "CL_ISMARK":
                            string bit = Convert.ToString(Convert.ToUInt32(dr["CL_ISMARK"]), 2).PadLeft(5, '0');
                            checkBox1_CL_ISMARK.Checked = bit[4] == '1';
                            checkBox2_CL_ISMARK.Checked = bit[3] == '1';
                            checkBox3_CL_ISMARK.Checked = bit[2] == '1';
                            checkBox4_CL_ISMARK.Checked = bit[1] == '1';
                            checkBox5_CL_ISMARK.Checked = bit[0] == '1';
                            break;
                        case "CL_TYPE":
                            string bit_CL_TYPE = Convert.ToString(Convert.ToUInt32(dr["CL_TYPE"]), 2).PadLeft(12, '0');
                            for (int i = 0; i < bit_CL_TYPE.Length; i++)
                                listBox_CL_TYPE.SetSelected(bit_CL_TYPE.Length - i - 1, bit_CL_TYPE[i] == '1');
                            break;
                        case "CL_IMPRESSNOTE":
                            textBox_CL_IMPRESSNOTE.Text = Convert.ToString(dr["CL_IMPRESSNOTE"]);
                            break;
                        case "CL_NOTE":
                            textBox_CL_NOTE.Text = Convert.ToString(dr["CL_NOTE"]);
                            break;
                        case "CL_REMARK":
                            textBox_CL_REMARK.Text = Convert.ToString(dr["CL_REMARK"]);
                            break;
                        case "CL_IMPRESSKEY":
                            string bit_CL_IMPRESSKEY = Convert.ToString(Convert.ToUInt32(dr["CL_IMPRESSKEY"]), 2).PadLeft(12, '0');
                            for (int i = 0; i < bit_CL_IMPRESSKEY.Length; i++)
                                listBox_CL_IMPRESSKEY.SetSelected(bit_CL_IMPRESSKEY.Length - i - 1, bit_CL_IMPRESSKEY[i] == '1');
                            break;
                        case "CL_FUTURE":
                            textBox_CL_FUTURE.Text = Convert.ToString(dr["CL_FUTURE"]);
                            break;
                        case "CL_TITLE1":
                            textBox_CL_TITLE1.Text = Convert.ToString(dr["CL_TITLE1"]);
                            break;
                        case "CL_TITLE2":
                            textBox_CL_TITLE2.Text = Convert.ToString(dr["CL_TITLE2"]);
                            break;
                        case "CL_TITLE3":
                            textBox_CL_TITLE3.Text = Convert.ToString(dr["CL_TITLE3"]);
                            break;
                        case "CL_TITLE4":
                            textBox_CL_TITLE4.Text = Convert.ToString(dr["CL_TITLE4"]);
                            break;
                        case "CL_LASTSTAT":
                            textBox_CL_LASTSTAT.Text = Convert.ToDateTime(dr["CL_LASTSTAT"]).ToString("g");
                            break;
                        case "CL_SUMMA":
                            textBox_CL_SUMMA.Text = Convert.ToDouble(dr["CL_SUMMA"]).ToString("F2");
                            break;
                        case "CL_NTRIP":
                            textBox_CL_NTRIP.Text = Convert.ToString(dr["CL_NTRIP"]);
                            break;
                        case "CL_MINCOST":
                            textBox_CL_MINCOST.Text = Convert.ToDouble(dr["CL_MINCOST"]).ToString("F2");
                            break;
                        case "CL_SUMDOGOVOR":
                            textBox_CL_SUMDOGOVOR.Text = Convert.ToDouble(dr["CL_SUMDOGOVOR"]).ToString("F2");
                            break;
                        case "CL_NMENWITH":
                            textBox_CL_NMENWITH.Text = Convert.ToString(dr["CL_NMENWITH"]);
                            break;
                        case "CL_MAXCOST":
                            textBox_CL_MAXCOST.Text = Convert.ToDouble(dr["CL_MAXCOST"]).ToString("F2");
                            break;*/
                        case "cl_mail":
                            textBox2_cl_mail.Text = Convert.ToString(dr2["cl_mail"]);
                            break;
                        case "cl_fax":
                            textBox2_cl_fax.Text = Convert.ToString(dr2["cl_fax"]);
                            break;
                    }
                }
                #endregion
            }

        }
        private void MergeProcedure()
        {
            textBox3_CL_DATEUPDATE.Text = DateTime.Now.ToString();
            textBox3_CL_KEY.Text = textBox2_CL_KEY.Text;
            if (radioButton1.Checked)
            {
                textBox3_CL_CITIZEN.Text=(textBox1_CL_CITIZEN.Text.Length>0)?textBox1_CL_CITIZEN.Text:textBox2_CL_CITIZEN.Text;
                textBox3_CL_PASPRUSER.Text=(textBox1_CL_PASPRUSER.Text.Length>0)?textBox1_CL_PASPRUSER.Text:textBox2_CL_PASPRUSER.Text;
                textBox3_CL_PASPRUNUM.Text=(textBox1_CL_PASPRUNUM.Text.Length>0)?textBox1_CL_PASPRUNUM.Text:textBox2_CL_PASPRUNUM.Text;
                textBox3_CL_NAMERUS.Text=(textBox1_CL_NAMERUS.Text.Length>0)?textBox1_CL_NAMERUS.Text:textBox2_CL_NAMERUS.Text;
                textBox3_CL_FNAMERUS.Text=(textBox1_CL_FNAMERUS.Text.Length>0)?textBox1_CL_FNAMERUS.Text:textBox2_CL_FNAMERUS.Text;
                textBox3_CL_SNAMERUS.Text=(textBox1_CL_SNAMERUS.Text.Length>0)?textBox1_CL_SNAMERUS.Text:textBox2_CL_SNAMERUS.Text;
                textBox3_CL_BIRTHDAY.Text=(textBox1_CL_BIRTHDAY.Text.Length>0)?textBox1_CL_BIRTHDAY.Text:textBox2_CL_BIRTHDAY.Text;
                textBox3_CL_BIRTHCITY.Text=(textBox1_CL_BIRTHCITY.Text.Length>0)?textBox1_CL_BIRTHCITY.Text:textBox2_CL_BIRTHCITY.Text;
                textBox3_CL_BIRTHCOUNTRY.Text=(textBox1_CL_BIRTHCOUNTRY.Text.Length>0)?textBox1_CL_BIRTHCOUNTRY.Text:textBox2_CL_BIRTHCOUNTRY.Text;
                textBox3_CL_PASPRUBYWHOM.Text=(textBox1_CL_PASPRUBYWHOM.Text.Length>0)?textBox1_CL_PASPRUBYWHOM.Text:textBox2_CL_PASPRUBYWHOM.Text;
                textBox3_CL_PASPRUDATE.Text=(textBox1_CL_PASPRUDATE.Text.Length>0)?textBox1_CL_PASPRUDATE.Text:textBox2_CL_PASPRUDATE.Text;
                textBox3_CL_POSTINDEX.Text=(textBox1_CL_POSTINDEX.Text.Length>0)?textBox1_CL_POSTINDEX.Text:textBox2_CL_POSTINDEX.Text;
                textBox3_CL_POSTCITY.Text=(textBox1_CL_POSTCITY.Text.Length>0)?textBox1_CL_POSTCITY.Text:textBox2_CL_POSTCITY.Text;
                textBox3_CL_POSTSTREET.Text=(textBox1_CL_POSTSTREET.Text.Length>0)?textBox1_CL_POSTSTREET.Text:textBox2_CL_POSTSTREET.Text;
                textBox3_CL_POSTBILD.Text=(textBox1_CL_POSTBILD.Text.Length>0)?textBox1_CL_POSTBILD.Text:textBox2_CL_POSTBILD.Text;
                textBox3_CL_POSTFLAT.Text=(textBox1_CL_POSTFLAT.Text.Length>0)?textBox1_CL_POSTFLAT.Text:textBox2_CL_POSTFLAT.Text;
                textBox3_CL_PHONE.Text=(textBox1_CL_PHONE.Text.Length>0)?textBox1_CL_PHONE.Text:textBox2_CL_PHONE.Text;
                textBox3_CL_PASPORTSER.Text=(textBox1_CL_PASPORTSER.Text.Length>0)?textBox1_CL_PASPORTSER.Text:textBox2_CL_PASPORTSER.Text;
                textBox3_CL_PASPORTNUM.Text=(textBox1_CL_PASPORTNUM.Text.Length>0)?textBox1_CL_PASPORTNUM.Text:textBox2_CL_PASPORTNUM.Text;
                textBox3_CL_NAMELAT.Text=(textBox1_CL_NAMELAT.Text.Length>0)?textBox1_CL_NAMELAT.Text:textBox2_CL_NAMELAT.Text;
                textBox3_CL_FNAMELAT.Text=(textBox1_CL_FNAMELAT.Text.Length>0)?textBox1_CL_FNAMELAT.Text:textBox2_CL_FNAMELAT.Text;
                textBox3_CL_SNAMELAT.Text=(textBox1_CL_SNAMELAT.Text.Length>0)?textBox1_CL_SNAMELAT.Text:textBox2_CL_SNAMELAT.Text;
                textBox3_CL_PASPORTDATE.Text=(textBox1_CL_PASPORTDATE.Text.Length>0)?textBox1_CL_PASPORTDATE.Text:textBox2_CL_PASPORTDATE.Text;
                textBox3_CL_PASPORTDATEEND.Text=(textBox1_CL_PASPORTDATEEND.Text.Length>0)?textBox1_CL_PASPORTDATEEND.Text:textBox2_CL_PASPORTDATEEND.Text;
                textBox3_CL_PASPORTBYWHOM.Text=(textBox1_CL_PASPORTBYWHOM.Text.Length>0)?textBox1_CL_PASPORTBYWHOM.Text:textBox2_CL_PASPORTBYWHOM.Text;
                textBox3_cl_mail.Text=(textBox1_cl_mail.Text.Length>0)?textBox1_cl_mail.Text:textBox2_cl_mail.Text;
                textBox3_cl_fax.Text=(textBox1_cl_fax.Text.Length>0)?textBox1_cl_fax.Text:textBox2_cl_fax.Text;
            }
            else
            {
                textBox3_CL_CITIZEN.Text=(textBox2_CL_CITIZEN.Text.Length>0)?textBox2_CL_CITIZEN.Text:textBox1_CL_CITIZEN.Text;
                textBox3_CL_PASPRUSER.Text=(textBox2_CL_PASPRUSER.Text.Length>0)?textBox2_CL_PASPRUSER.Text:textBox1_CL_PASPRUSER.Text;
                textBox3_CL_PASPRUNUM.Text=(textBox2_CL_PASPRUNUM.Text.Length>0)?textBox2_CL_PASPRUNUM.Text:textBox1_CL_PASPRUNUM.Text;
                textBox3_CL_NAMERUS.Text=(textBox2_CL_NAMERUS.Text.Length>0)?textBox2_CL_NAMERUS.Text:textBox1_CL_NAMERUS.Text;
                textBox3_CL_FNAMERUS.Text=(textBox2_CL_FNAMERUS.Text.Length>0)?textBox2_CL_FNAMERUS.Text:textBox1_CL_FNAMERUS.Text;
                textBox3_CL_SNAMERUS.Text=(textBox2_CL_SNAMERUS.Text.Length>0)?textBox2_CL_SNAMERUS.Text:textBox1_CL_SNAMERUS.Text;
                textBox3_CL_BIRTHDAY.Text=(textBox2_CL_BIRTHDAY.Text.Length>0)?textBox2_CL_BIRTHDAY.Text:textBox1_CL_BIRTHDAY.Text;
                textBox3_CL_BIRTHCITY.Text=(textBox2_CL_BIRTHCITY.Text.Length>0)?textBox2_CL_BIRTHCITY.Text:textBox1_CL_BIRTHCITY.Text;
                textBox3_CL_BIRTHCOUNTRY.Text=(textBox2_CL_BIRTHCOUNTRY.Text.Length>0)?textBox2_CL_BIRTHCOUNTRY.Text:textBox1_CL_BIRTHCOUNTRY.Text;
                textBox3_CL_PASPRUBYWHOM.Text=(textBox2_CL_PASPRUBYWHOM.Text.Length>0)?textBox2_CL_PASPRUBYWHOM.Text:textBox1_CL_PASPRUBYWHOM.Text;
                textBox3_CL_PASPRUDATE.Text=(textBox2_CL_PASPRUDATE.Text.Length>0)?textBox2_CL_PASPRUDATE.Text:textBox1_CL_PASPRUDATE.Text;
                textBox3_CL_POSTINDEX.Text=(textBox2_CL_POSTINDEX.Text.Length>0)?textBox2_CL_POSTINDEX.Text:textBox1_CL_POSTINDEX.Text;
                textBox3_CL_POSTCITY.Text=(textBox2_CL_POSTCITY.Text.Length>0)?textBox2_CL_POSTCITY.Text:textBox1_CL_POSTCITY.Text;
                textBox3_CL_POSTSTREET.Text=(textBox2_CL_POSTSTREET.Text.Length>0)?textBox2_CL_POSTSTREET.Text:textBox1_CL_POSTSTREET.Text;
                textBox3_CL_POSTBILD.Text=(textBox2_CL_POSTBILD.Text.Length>0)?textBox2_CL_POSTBILD.Text:textBox1_CL_POSTBILD.Text;
                textBox3_CL_POSTFLAT.Text=(textBox2_CL_POSTFLAT.Text.Length>0)?textBox2_CL_POSTFLAT.Text:textBox1_CL_POSTFLAT.Text;
                textBox3_CL_PHONE.Text=(textBox2_CL_PHONE.Text.Length>0)?textBox2_CL_PHONE.Text:textBox1_CL_PHONE.Text;
                textBox3_CL_PASPORTSER.Text=(textBox2_CL_PASPORTSER.Text.Length>0)?textBox2_CL_PASPORTSER.Text:textBox1_CL_PASPORTSER.Text;
                textBox3_CL_PASPORTNUM.Text=(textBox2_CL_PASPORTNUM.Text.Length>0)?textBox2_CL_PASPORTNUM.Text:textBox1_CL_PASPORTNUM.Text;
                textBox3_CL_NAMELAT.Text=(textBox2_CL_NAMELAT.Text.Length>0)?textBox2_CL_NAMELAT.Text:textBox1_CL_NAMELAT.Text;
                textBox3_CL_FNAMELAT.Text=(textBox2_CL_FNAMELAT.Text.Length>0)?textBox2_CL_FNAMELAT.Text:textBox1_CL_FNAMELAT.Text;
                textBox3_CL_SNAMELAT.Text=(textBox2_CL_SNAMELAT.Text.Length>0)?textBox2_CL_SNAMELAT.Text:textBox1_CL_SNAMELAT.Text;
                textBox3_CL_PASPORTDATE.Text=(textBox2_CL_PASPORTDATE.Text.Length>0)?textBox2_CL_PASPORTDATE.Text:textBox1_CL_PASPORTDATE.Text;
                textBox3_CL_PASPORTDATEEND.Text=(textBox2_CL_PASPORTDATEEND.Text.Length>0)?textBox2_CL_PASPORTDATEEND.Text:textBox1_CL_PASPORTDATEEND.Text;
                textBox3_CL_PASPORTBYWHOM.Text=(textBox2_CL_PASPORTBYWHOM.Text.Length>0)?textBox2_CL_PASPORTBYWHOM.Text:textBox1_CL_PASPORTBYWHOM.Text;
                textBox3_cl_mail.Text=(textBox2_cl_mail.Text.Length>0)?textBox2_cl_mail.Text:textBox1_cl_mail.Text;
                textBox3_cl_fax.Text=(textBox2_cl_fax.Text.Length>0)?textBox2_cl_fax.Text:textBox1_cl_fax.Text;
            }
                                    
            /*
                            case "CL_ISMARK":
                                string bit = Convert.ToString(Convert.ToUInt32(dr["CL_ISMARK"]), 2).PadLeft(5, '0');
                                checkBox1_CL_ISMARK.Checked = bit[4] == '1';
                                checkBox2_CL_ISMARK.Checked = bit[3] == '1';
                                checkBox3_CL_ISMARK.Checked = bit[2] == '1';
                                checkBox4_CL_ISMARK.Checked = bit[1] == '1';
                                checkBox5_CL_ISMARK.Checked = bit[0] == '1';
                                break;
                            case "CL_TYPE":
                                string bit_CL_TYPE = Convert.ToString(Convert.ToUInt32(dr["CL_TYPE"]), 2).PadLeft(12, '0');
                                for (int i = 0; i < bit_CL_TYPE.Length; i++)
                                    listBox_CL_TYPE.SetSelected(bit_CL_TYPE.Length - i - 1, bit_CL_TYPE[i] == '1');
                                break;
                            case "CL_IMPRESSNOTE":
                                textBox_CL_IMPRESSNOTE.Text = Convert.ToString(dr["CL_IMPRESSNOTE"]);
                                break;
                            case "CL_NOTE":
                                textBox_CL_NOTE.Text = Convert.ToString(dr["CL_NOTE"]);
                                break;
                            case "CL_REMARK":
                                textBox_CL_REMARK.Text = Convert.ToString(dr["CL_REMARK"]);
                                break;
                            case "CL_IMPRESSKEY":
                                string bit_CL_IMPRESSKEY = Convert.ToString(Convert.ToUInt32(dr["CL_IMPRESSKEY"]), 2).PadLeft(12, '0');
                                for (int i = 0; i < bit_CL_IMPRESSKEY.Length; i++)
                                    listBox_CL_IMPRESSKEY.SetSelected(bit_CL_IMPRESSKEY.Length - i - 1, bit_CL_IMPRESSKEY[i] == '1');
                                break;
                            case "CL_FUTURE":
                                textBox_CL_FUTURE.Text = Convert.ToString(dr["CL_FUTURE"]);
                                break;
                            case "CL_TITLE1":
                                textBox_CL_TITLE1.Text = Convert.ToString(dr["CL_TITLE1"]);
                                break;
                            case "CL_TITLE2":
                                textBox_CL_TITLE2.Text = Convert.ToString(dr["CL_TITLE2"]);
                                break;
                            case "CL_TITLE3":
                                textBox_CL_TITLE3.Text = Convert.ToString(dr["CL_TITLE3"]);
                                break;
                            case "CL_TITLE4":
                                textBox_CL_TITLE4.Text = Convert.ToString(dr["CL_TITLE4"]);
                                break;
                            case "CL_LASTSTAT":
                                textBox_CL_LASTSTAT.Text = Convert.ToDateTime(dr["CL_LASTSTAT"]).ToString("g");
                                break;
                            case "CL_SUMMA":
                                textBox_CL_SUMMA.Text = Convert.ToDouble(dr["CL_SUMMA"]).ToString("F2");
                                break;
                            case "CL_NTRIP":
                                textBox_CL_NTRIP.Text = Convert.ToString(dr["CL_NTRIP"]);
                                break;
                            case "CL_MINCOST":
                                textBox_CL_MINCOST.Text = Convert.ToDouble(dr["CL_MINCOST"]).ToString("F2");
                                break;
                            case "CL_SUMDOGOVOR":
                                textBox_CL_SUMDOGOVOR.Text = Convert.ToDouble(dr["CL_SUMDOGOVOR"]).ToString("F2");
                                break;
                            case "CL_NMENWITH":
                                textBox_CL_NMENWITH.Text = Convert.ToString(dr["CL_NMENWITH"]);
                                break;
                            case "CL_MAXCOST":
                                textBox_CL_MAXCOST.Text = Convert.ToDouble(dr["CL_MAXCOST"]).ToString("F2");
                                break;
                            case "CL_PFKEY":
                                comboBox_CL_PFKEY.SelectedValue = Convert.ToInt64(dr["CL_PFKEY"]);
                                break;*/
                            /*
                            case "CL_SEX":
                                checkBox1_CL_SEX.Checked = Convert.ToInt16(dr["CL_SEX"]) == 2;
                                checkBox2_CL_SEX.Checked = Convert.ToInt16(dr["CL_SEX"]) == 3;
                                textBox_CL_SEX.Text = Convert.ToString(dr["CL_SEX"]);
                                break;
                            case "CL_RealSex":
                                radioButton2.Checked = !(radioButton1.Checked = Convert.ToInt16(dr["CL_RealSex"]) == 0);
                                textBox_CL_RealSex.Text = Convert.ToString(dr["CL_RealSex"]);
                                break;*/


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            MergeProcedure();
        }

        private void ClientUnionDialog_Load(object sender, EventArgs e)
        {
        
        }
    }
}
