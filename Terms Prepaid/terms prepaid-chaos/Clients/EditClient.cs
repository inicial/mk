using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using lanta.Clients.ru.lantatur.lanta;
using System.Net;
using lanta.SQLConfig;


namespace lanta.Clients
{
    public partial class EditClient : Form
    {
        public enum DogovorCountry { Россия, Україна };
        DogovorCountry country;
        public string[] comparisonRule = new string[] {
                "TU_NAMERUS=CL_NAMERUS=Фамилия русская",
                "TU_NAMELAT=CL_NAMELAT=Фамилия латинская",
                "TU_SHORTNAME=CL_SHORTNAME=Инициалы",
                "TU_SEX=CL_SEX=Пол",
                "TU_FNAMERUS=CL_FNAMERUS=Имя русское",
                "TU_FNAMELAT=CL_FNAMELAT=Имя латинское",
                "TU_SNAMERUS=CL_SNAMERUS=Отчество русское",
                "TU_SNAMELAT=CL_SNAMELAT=Отчество латинское",
                "TU_BIRTHDAY=CL_BIRTHDAY=День рождения" ,
                "TU_BIRTHCOUNTRY=CL_BIRTHCOUNTRY=Страна рождения",
                "TU_BIRTHCITY=CL_BIRTHCITY=Город рождения",
                "TU_CITIZEN=CL_CITIZEN=Гражданство",
                "TU_POSTINDEX=CL_POSTINDEX=Почтовый индекс",
                "TU_POSTCITY=CL_POSTCITY=Адрес, город",
                "TU_POSTSTREET=CL_POSTSTREET=Адрес, улица",
                "TU_POSTBILD=CL_POSTBILD=Адрес, дом",
                "TU_POSTFLAT=CL_POSTFLAT=Адрес, квартира",
                "TU_PHONE=CL_PHONE=Телефон",
                "TU_PASPORTTYPE=CL_PASPORTSER=Серия паспорта",
                "TU_PASPORTNUM=CL_PASPORTNUM=Номер паспорта",
                "TU_PASPORTDATE=CL_PASPORTDATE=Дата выдачи паспорта",
                "TU_PASPORTDATEEND=CL_PASPORTDATEEND=Окончание срока действия паспорта",
                "TU_PASPORTBYWHOM=CL_PASPORTBYWHOM=Кем выдан",
                "TU_PASPRUSER=CL_PASPRUSER=Серия внутреннего паспорта",
                "TU_PASPRUNUM=CL_PASPRUNUM=Номер внутреннего паспорта",
                "TU_PASPRUDATE=CL_PASPRUDATE=Дата выдачи внутреннего паспорта",
                "TU_PASPRUBYWHOM=CL_PASPRUBYWHOM=Кем выдан",
                "TU_RealSex=CL_RealSex=Реальный пол" };  
        //string[] dataBases;//
        public DataTable clientsDT = new DataTable("Clients");
        DataTable templateClientDT = new DataTable("templateClientDT");
        DataTable professionDT = new DataTable("Profession");
        DataTable TitleTypeClientDT = new DataTable("TitleTypeClient");
        DataTable TitleTypeImpressDT = new DataTable("TitleTypeImpress");
        DataTable CardInfoDT = new DataTable("CardInfoDT");
        DataTable Lanta_ClientFamily = new DataTable("Lanta_ClientFamily");
        DataTable Lanta_ClientFamilyt = new DataTable("Lanta_ClientFamilyt");
        DataTable Lanta_ClientAccess = new DataTable("Lanta_ClientAccess");
        DataTable CR_PHONES = new DataTable("CR_PHONES");
        DataTable tbl_Turist = new DataTable("tbl_Turist");

        DataTable Lanta_AnketaQuestions = new DataTable("Lanta_AnketaQuestions");
        DataTable Lanta_AnketaAnswers = new DataTable("Lanta_AnketaAnswers");
        int CL_CITIZEN_LCI_ISO = 643;//,810,804
        bool RusUkCitizen = true;
        DataTable stat = new DataTable("stat");
        DataTable sput = new DataTable("sput");
        DataTable Lanta_ClientStatDogovor = new DataTable("Lanta_ClientStatDogovor");
        DataTable Lanta_ClientStatSputniki = new DataTable("Lanta_ClientStatSputniki");
        DataTable Lanta_ClientStatDogovorList = new DataTable("Lanta_ClientStatDogovorList");
        DataTable Lanta_ClientPassangerLink = new DataTable("Lanta_ClientPassangerLink");
        DataTable tbl_DogovorList = new DataTable("tbl_DogovorList");
        SqlDataAdapter adapter;
        SqlCommandBuilder builder;
        DataTable q1, q2, q3;
        //bool q2Changed = false, q5Changed = false;

        public DataTable DUP_USER = new DataTable("DUP_USER");
        public DataTable Lanta_ClientCurator = new DataTable("Lanta_ClientCurator");
        DataTable Curators = new DataTable("Curators");
        DataTable Filials = new DataTable("Filials");
        
        DataTable CitizenCountry = new DataTable("CitizenCountry"); 
        DataTable ISOCountry = new DataTable("ISOCountry");     
        
        DataTable logins = new DataTable("logins");
        DataTable logins2 = new DataTable("logins2");
        EncryptionService es = new EncryptionService();
        Random rnd = new Random();
        Decoder d = Encoding.ASCII.GetDecoder();
        DataRow dr;

        string vm = "Необходимо заполнить и сохранить обязательное поле ";
        string vmax = "Длина поля не должна превышать ";
        public long CL_KEY = -1;
        long MANAGER_ID = 0;
        int TU_KEY = -1;
        Passanger pas = null;
        SqlConnection turistFromConnection;
        bool isEdit = true;
        SqlConnection connection = null;
        public long templateClient = -1;//Идентификатор клиента, у которого нужно скопировать часть данных, например адрес
        bool Presenter = false;
        bool CheckZagran = false;
        public EditClient()
        {
            InitializeComponent();
        }
        public EditClient(long CL_KEY, long MANAGER_ID, SqlConnection connection, bool Presenter, bool CheckZagran)
        {
            InitializeComponent();
            Config_XML config = new Config_XML();
            es.Url = config.Get_Value("EncryptionService", "url");
            this.Presenter = Presenter;
            this.CheckZagran = CheckZagran;
            Init(CL_KEY, MANAGER_ID, connection);
            

            
            if (Presenter)
            {
                checkBox_Family.Visible = false;
                panel5.Visible = false;
                

            }
            UpdateStatistic();
            if (CheckZagran)
                label_CheckZagran.Text = "Включена";
        }

        private void Init(long CL_KEY, long MANAGER_ID, SqlConnection connection)
        {
            this.CL_KEY = CL_KEY;
            this.MANAGER_ID = MANAGER_ID;
            this.connection = connection;
            this.turistFromConnection = connection;
            adapter = new SqlDataAdapter("", connection);
            isEdit = CL_KEY > -1;
            Cursor.Current = Cursors.WaitCursor;
           
            if (connection.DataSource.StartsWith("192.168.10"))
                country = DogovorCountry.Россия;
            else
                country = DogovorCountry.Україна;

            try
            {
                GetVocabs();
                GetData();

                if (isEdit)
                    PopulateFields();

            }

            finally { Cursor.Current = Cursors.Arrow; }
        }

        private void RefreshCR_PHONES()
        {
            CR_PHONES.Clear();
            adapter.SelectCommand.CommandText = @"SELECT     CCLP_CCPC, CCLP_CCP, CCLP_CLPHONE, CCLP_ADDPHONE
                FROM         Lanta_CallClientPhones
                WHERE     CCLP_CLKEY =" + Convert.ToString(CL_KEY);
            adapter.Fill(CR_PHONES);
            string info = "";
            foreach (DataRow dr in CR_PHONES.Rows)
            {
                info = info + Convert.ToString(dr["CCLP_CCPC"]) + Convert.ToString(dr["CCLP_CCP"]) + Convert.ToString(dr["CCLP_CLPHONE"]);
                if (dr["CCLP_ADDPHONE"] != System.DBNull.Value)
                    info = info + "(" + Convert.ToString(dr["CCLP_ADDPHONE"]) + ")";
            }
            textBox_CR_PHONES.Text = info;
        }
        private void RefreshCards()
        {
            CardInfoDT.Clear();
            adapter.SelectCommand.CommandText= @"SELECT     CARDS.CD_Key, Discount_Client.DS_NAME, CARDS.CD_Code, CARDS.CD_Number, CARDS.CD_IsValid, Discount_Client.DS_VALUE, 
                      Discount_Client.DS_TYPE, CARDS.CD_Date, UserList.US_NAME
                        FROM         CARDS INNER JOIN
                      Discount_Client ON CARDS.CD_DSKey = Discount_Client.DS_KEY LEFT JOIN
                      UserList ON CARDS.CD_CREATOR = UserList.US_KEY
                        WHERE     CARDS.CD_CLKey = " + Convert.ToString(CL_KEY);
               //для получения схемы таблицы для новой строки
                adapter.Fill(CardInfoDT);
                
                if (!CardInfoDT.Columns.Contains("DS_TYPE_VAL"))
                     CardInfoDT.Columns.Add("DS_TYPE_VAL");
                foreach (DataRow dr in CardInfoDT.Rows)
                    if (Convert.ToUInt16(dr["DS_TYPE"]) == 1)
                        dr["DS_TYPE_VAL"] = "%";
                dataGridView_CARDS.DataSource = CardInfoDT;
        }
        private void GetData()
        {
            if (isEdit)
                adapter.SelectCommand.CommandText = "select * from clients where CL_KEY=" + Convert.ToString(CL_KEY);
            else//для получения схемы таблицы для новой строки
                adapter.SelectCommand.CommandText ="select * from clients where 1=2";
            adapter.Fill(clientsDT);

            if (isEdit)
            {
                RefreshCR_PHONES();
                RefreshCards();
                GetFamilyInfo();
              
                adapter.SelectCommand.CommandText =
                @"SELECT     TU_KEY
                FROM         tbl_Turist
                WHERE    TU_ID =" + Convert.ToString(CL_KEY) + " ORDER BY TU_TURDATE DESC";
                adapter.Fill(tbl_Turist);
                bool AnketaMiss = true;
                foreach (DataRow dr in tbl_Turist.Rows)
                { //На клиента может быть оформлено много туристов, у каждого туриста может быть своя анкета
                    //При просмотре клиентов отображаем первую попавшуюся из последних
                    TU_KEY = Convert.ToInt32(dr["TU_KEY"]);
                    if (GetTuristAnketa(TU_KEY))
                    {
                        PopulateAnketaFields();
                        AnketaMiss = false;
                        break;
                    }
                }
                if (AnketaMiss)
                    tabControl1.TabPages.RemoveByKey("tabPage9");
            }
            //при создании нового клиента тоже нужен доступ
            adapter.SelectCommand.CommandText =
                            @"SELECT     CA_ID, CA_CLКеу, CA_DUPUSERKEY, CA_MANAGER, CA_CREATEDATE
                FROM         Lanta_ClientAccess
                WHERE    CA_CLКеу =" + Convert.ToString(CL_KEY);
            adapter.Fill(Lanta_ClientAccess);

            if (Lanta_ClientAccess.Rows.Count > 0)
            {
                DUP_USER.Clear();
                string US_KEY = Convert.ToString(Lanta_ClientAccess.Rows[0]["CA_DUPUSERKEY"]);
                adapter.SelectCommand.CommandText = @"SELECT US_KEY, US_ID, US_PASSWORD,US_AGENT,US_REG,US_TURAGENT,US_PRKEY
                    FROM         [dbo].[DUP_USER]
                    WHERE     US_KEY = " + US_KEY;
                adapter.Fill(DUP_USER);
            }
            else
            {
                adapter.SelectCommand.CommandText = @"SELECT US_KEY, US_ID, US_PASSWORD,US_AGENT,US_REG,US_TURAGENT,US_PRKEY FROM [dbo].[dup_user] WHERE US_KEY=-1";
                adapter.FillSchema(DUP_USER, SchemaType.Source);
            }

            Lanta_ClientCurator.Clear();
            adapter.SelectCommand.CommandText =
                            @"SELECT     LDC_ID, LDC_ClientКеу, LDC_CuratorUserКеу, LDC_MANAGER, LDC_UPDATE, LDC_CuratorFilial
             FROM    Lanta_ClientCurator
                WHERE    LDC_ClientКеу =" + Convert.ToString(CL_KEY);
            adapter.Fill(Lanta_ClientCurator);
            




        }
        private void PopulateAnketaFields()
        {
            DataRow[] drs;
            DataRow dr;
            //Вопрос 1
            drs = Lanta_AnketaAnswers.Select("AA_AQKey=1");
            if (drs.Length > 0)
            {
                dr = drs[0];
                comboBox_1.SelectedValue = dr["AA_AAKey"];
            }
            else
            {
                comboBox_1.SelectedIndex = -1;
                
            }
            //След вопрос 2 
            listBox_2.SelectedIndex = -1;
            drs = Lanta_AnketaAnswers.Select("AA_AQKey=6");
            if (drs.Length > 0)
            {
                //q2Changed = true;
                foreach (DataRow r in drs)
                {
                    foreach (DataRow r2 in q2.Rows)
                    {
                        if (Convert.ToInt32(r["AA_AAKey"]) == Convert.ToInt32(r2["AQ_Key"]))
                            r2["AQ_Order"] = r["AA_AnswerDetail"];
                    }
                }
                DataTable q22 = q2.Clone();
                q2.Select("", "AQ_Order").CopyToDataTable(q22, LoadOption.Upsert);
                q2 = q22;
                listBox_2.DataSource = q2;

            }
           
            //След вопрос 3
            checkedListBox_3.SelectedIndex = -1;
            drs = Lanta_AnketaAnswers.Select("AA_AQKey=12");
            if (drs.Length > 0)
            {
                checkedListBox_3.SelectedIndex = 0;
                foreach (DataRow r in drs)
                {
                    for (int i = 0; i < checkedListBox_3.Items.Count; i++)
                    {
                        if (Convert.ToInt32(r["AA_AAKey"]) == Convert.ToInt32(((System.Data.DataRowView)checkedListBox_3.Items[i]).Row["AQ_Key"]))
                            checkedListBox_3.SetItemChecked(i, Convert.ToInt32(r["AA_AnswerDetail"]) > 0);
                    }
                }
            }
            
            //Вопрос 4
            drs = Lanta_AnketaAnswers.Select("AA_AQKey=19");
            if (drs.Length > 0)
            {
                dr = drs[0];
                textBox_4.Text = Convert.ToString(dr["AA_Answer"]);
            }
            else
            {
                textBox_4.Text = "";
                /*if (label_4.Text.Length > 0 && label_4.Text[0] != '^')
                    label_4.Text = "^" + label_4.Text;*/
            }
            //След вопрос 5
            drs = Lanta_AnketaAnswers.Select("AA_AQKey=20");
            if (drs.Length > 0)
            {
               // q5Changed = true;
                dr = drs[0];
                checkBox_5.Checked = Convert.ToInt32(dr["AA_AnswerDetail"]) > 0;
            }
            else
            {
                /*if (label_5.Text.Length > 0 && label_5.Text[0] != '^')
                    label_5.Text = "^" + label_5.Text;*/
            }
        }
        private bool GetTuristAnketa(int TU_KEY)
        {
            adapter.SelectCommand = new SqlCommand(
                "select * from Lanta_AnketaQuestions order by AQ_Order", connection);
            adapter.Fill(Lanta_AnketaQuestions);

            label_q1.Text = Convert.ToString(Lanta_AnketaQuestions.Select("AQ_KEY=1")[0]["AQ_NAME"]) + "*";
            q1 = Lanta_AnketaQuestions.Clone();
            Lanta_AnketaQuestions.Select("AQ_PARENTKEY=1").CopyToDataTable(q1, LoadOption.Upsert);
            comboBox_1.DataSource = q1;

            label_2.Text = Convert.ToString(Lanta_AnketaQuestions.Select("AQ_KEY=6")[0]["AQ_NAME"]) + "*";
            q2 = Lanta_AnketaQuestions.Clone();
            Lanta_AnketaQuestions.Select("AQ_PARENTKEY=6").CopyToDataTable(q2, LoadOption.Upsert);
            listBox_2.DataSource = q2;

            label_3.Text = Convert.ToString(Lanta_AnketaQuestions.Select("AQ_KEY=12")[0]["AQ_NAME"]) + "*";
            q3 = Lanta_AnketaQuestions.Clone();
            Lanta_AnketaQuestions.Select("AQ_PARENTKEY=12").CopyToDataTable(q3, LoadOption.Upsert);
            checkedListBox_3.DataSource = q3;
            checkedListBox_3.DisplayMember = "AQ_NAME";
            checkedListBox_3.ValueMember = "AQ_KEY";

            label_4.Text = Convert.ToString(Lanta_AnketaQuestions.Select("AQ_KEY=19")[0]["AQ_NAME"]) + "*";

            label_5.Text = Convert.ToString(Lanta_AnketaQuestions.Select("AQ_KEY=20")[0]["AQ_NAME"]) + "*";
            
            adapter.SelectCommand = new SqlCommand(
                 "select * from Lanta_AnketaAnswers where AA_TUKey=" + Convert.ToString(TU_KEY), connection);
            adapter.Fill(Lanta_AnketaAnswers);
            return Lanta_AnketaAnswers.Rows.Count > 0;
        }
        private void GetFamilyInfo()
        {

            Lanta_ClientFamily = FillFamilyInfo(CL_KEY);
            dataGridView_Fam.DataSource = Lanta_ClientFamily;
            dataGridView_Fam.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        public DataTable FillFamilyInfo(long key)
        {
            DataTable ret = new DataTable("Fam");
            DataTable clf = new DataTable("clf");

            adapter.SelectCommand = new SqlCommand("select * from Lanta_ClientFamily where CF_CLKey=" + Convert.ToString(key), connection);
            adapter.Fill(clf);
            int famid = -1;
            if (clf.Rows.Count > 0)
                famid = Convert.ToInt32(clf.Rows[0]["CF_Community"]);

            adapter.SelectCommand = new SqlCommand(
                @"SELECT     Lanta_ClientFamily.CF_ID, Lanta_ClientFamily.CF_Community, Clients.CL_NAMERUS +' '+Clients.CL_FNAMERUS +' '+Clients.CL_SNAMERUS AS CL_NAMERUS, Lanta_ClientFamily.CF_CLKey, Lanta_ClientFamily.CF_MANAGER,Lanta_ClientFamily.CF_CREATEDATE,
                  UserList.US_FullName
                    FROM         Lanta_ClientFamily LEFT OUTER JOIN
                    Clients ON Lanta_ClientFamily.CF_CLKey = Clients.CL_KEY
                    LEFT OUTER JOIN
                      UserList ON Lanta_ClientFamily.CF_MANAGER = UserList.US_KEY
                        WHERE     Lanta_ClientFamily.CF_Community=" + Convert.ToString(famid), connection);
            if (clf.Rows.Count > 0)
                adapter.Fill(ret);
            else
                adapter.FillSchema(ret, SchemaType.Source);
            return ret;
        }

        private void GetVocabs()
        {
            RefreshProfession();

            adapter.SelectCommand = new SqlCommand(
                "select * from TitleTypeClient", connection);
            adapter.Fill(TitleTypeClientDT);
            listBox_CL_TYPE.DataSource = TitleTypeClientDT;
            listBox_CL_TYPE.ClearSelected();

            adapter.SelectCommand = new SqlCommand(
                "select * from TitleTypeImpress", connection);
            adapter.Fill(TitleTypeImpressDT);
            listBox_CL_IMPRESSKEY.DataSource = TitleTypeImpressDT;
            listBox_CL_IMPRESSKEY.ClearSelected();

            adapter.SelectCommand.CommandText = @"SELECT     UserList.US_KEY, UserList.US_FullName 
                      + '-' + tbl_Partners.PR_NAME AS US_FullName
            FROM         UserList INNER JOIN
                      Lanta_CallAllowUsers ON UserList.US_KEY = Lanta_CallAllowUsers.CAU_UserEnabledKey LEFT OUTER JOIN
                      tbl_Partners ON UserList.US_PRKEY = tbl_Partners.PR_KEY
            ORDER BY US_FullName";
            adapter.Fill(Curators);
            comboBox_Curator.DisplayMember = "US_FullName";
            comboBox_Curator.ValueMember = "US_KEY";
            comboBox_Curator.DataSource = Curators;

            adapter.SelectCommand.CommandText = @"SELECT     PR_KEY, PR_NAME
                FROM         tbl_Partners
                WHERE     (tbl_Partners.PR_Filial > 0 and PR_CTKEY = 1) or (dbo.tbl_Partners.PR_Filial =1)";
            adapter.Fill(Filials);
            comboBox_Filial.ValueMember = "PR_KEY";
            comboBox_Filial.DisplayMember = "PR_NAME";
            comboBox_Filial.DataSource = Filials;

            adapter.SelectCommand.CommandText = @"SELECT LCI_ISO,LCI_RUSNAME,LCI_FlagImage FROM Lanta_ISOCountry where LCI_ISO = 643";
            adapter.Fill(ISOCountry);
            adapter.SelectCommand.CommandText = @"SELECT LCI_ISO,LCI_RUSNAME,LCI_FlagImage FROM Lanta_ISOCountry where LCI_ISO = 810";
            adapter.Fill(ISOCountry);
            adapter.SelectCommand.CommandText = @"SELECT LCI_ISO,LCI_RUSNAME,LCI_FlagImage FROM Lanta_ISOCountry where LCI_ISO = 804";
            adapter.Fill(ISOCountry);
            adapter.SelectCommand.CommandText = @"SELECT LCI_ISO,LCI_RUSNAME,LCI_FlagImage FROM Lanta_ISOCountry where LCI_ISO not in (643,810,804) order by LCI_RUSNAME";
            adapter.Fill(ISOCountry);
            comboBox_CL_BIRTHCOUNTRY.ValueMember = "LCI_ISO";
            comboBox_CL_BIRTHCOUNTRY.DisplayMember = "LCI_RUSNAME";
            comboBox_CL_BIRTHCOUNTRY.DataSource = ISOCountry;

            CitizenCountry = ISOCountry.Copy();
            this.comboBox_CL_CITIZEN.SelectedIndexChanged -= new System.EventHandler(this.comboBox_CL_CITIZEN_SelectedIndexChanged);
            comboBox_CL_CITIZEN.ValueMember = "LCI_ISO";
            comboBox_CL_CITIZEN.DisplayMember = "LCI_RUSNAME";
            comboBox_CL_CITIZEN.DataSource = CitizenCountry;
            this.comboBox_CL_CITIZEN.SelectedIndexChanged += new System.EventHandler(this.comboBox_CL_CITIZEN_SelectedIndexChanged);
            cbCountry.ValueMember = "LCI_ISO";
            cbCountry.DisplayMember = "LCI_RUSNAME";
            DataTable CCountry = CitizenCountry.Copy();
            CCountry.Rows.RemoveAt(1);
            cbCountry.DataSource =CCountry;
        }

        private void RefreshProfession()
        {
            professionDT.Clear();
            adapter.SelectCommand = new SqlCommand(
                "select * from profession", connection);
            adapter.Fill(professionDT);
            comboBox_CL_PFKEY.DataSource = professionDT;
            if (clientsDT.Rows.Count > 0)
            {
                DataRow dr = clientsDT.Rows[0];
                if (dr["CL_PFKEY"] != System.DBNull.Value)
                    comboBox_CL_PFKEY.SelectedValue = Convert.ToInt64(dr["CL_PFKEY"]);
            }
        }
        private void PopulateFields()
        {
            DataRow dr;
            if (clientsDT.Rows.Count > 0)
            {
                dr = clientsDT.Rows[0];
                foreach (DataColumn dc in clientsDT.Columns)
                {
                    if (dr[dc.ColumnName] != System.DBNull.Value)
                    {
                        switch (dc.ColumnName)
                        {
                            case "CL_PFKEY":
                                comboBox_CL_PFKEY.SelectedValue = Convert.ToInt64(dr["CL_PFKEY"]);
                                break;
                            case "CL_CITIZEN":
                                string inCITIZEN = Convert.ToString(dr["CL_CITIZEN"]);
                                if (comboBox_CL_CITIZEN.Text == inCITIZEN)
                                {
                                    comboBox_CL_CITIZEN_SelectedIndexChanged(this, null);
                                }
                                else
                                    comboBox_CL_CITIZEN.Text = inCITIZEN;
                                textBox_CL_CITIZEN.Text = Convert.ToString(dr["CL_CITIZEN"]);
                                break;
                            case "CL_PASPRUSER":
                                textBox_CL_PASPRUSER.Text = Convert.ToString(dr["CL_PASPRUSER"]);
                                break;
                            case "CL_PASPRUNUM":
                                textBox_CL_PASPRUNUM.Text = Convert.ToString(dr["CL_PASPRUNUM"]);
                                break;
                            case "CL_NAMERUS":
                                textBox_CL_NAMERUS.Text = Convert.ToString(dr["CL_NAMERUS"]);
                                break;
                            case "CL_FNAMERUS":
                                textBox_CL_FNAMERUS.Text = Convert.ToString(dr["CL_FNAMERUS"]);
                                break;
                            case "CL_SNAMERUS":
                                textBox_CL_SNAMERUS.Text = Convert.ToString(dr["CL_SNAMERUS"]);
                                break;
                            case "CL_SEX":
                                checkBox1_CL_SEX.Checked = Convert.ToInt16(dr["CL_SEX"]) == 2;
                                checkBox2_CL_SEX.Checked = Convert.ToInt16(dr["CL_SEX"]) == 3;
                                textBox_CL_SEX.Text = Convert.ToString(dr["CL_SEX"]);
                                break;
                            case "CL_RealSex":
                                radioButton2.Checked = !(radioButton1.Checked = Convert.ToInt16(dr["CL_RealSex"]) == 0);
                                textBox_CL_RealSex.Text = Convert.ToString(dr["CL_RealSex"]);
                                break;
                            case "CL_BIRTHDAY":
                                textBox_CL_BIRTHDAY.Text = Convert.ToDateTime(dr["CL_BIRTHDAY"]).ToString("d");
                                break;
                            case "CL_BIRTHCITY":
                                textBox_CL_BIRTHCITY.Text = Convert.ToString(dr["CL_BIRTHCITY"]);
                                break;
                            case "CL_BIRTHCOUNTRY":
                                comboBox_CL_BIRTHCOUNTRY.Text = Convert.ToString(dr["CL_BIRTHCOUNTRY"]);
                                textBox_CL_BIRTHCOUNTRY.Text = Convert.ToString(dr["CL_BIRTHCOUNTRY"]);
                                break;
                            case "CL_PASPRUBYWHOM":
                                textBox_CL_PASPRUBYWHOM.Text = Convert.ToString(dr["CL_PASPRUBYWHOM"]);
                                break;
                            case "CL_PASPRUDATE":
                                textBox_CL_PASPRUDATE.Text = Convert.ToDateTime(dr["CL_PASPRUDATE"]).ToString("d");
                                break;
                            case "CL_POSTINDEX":
                                textBox_CL_POSTINDEX.Text = Convert.ToString(dr["CL_POSTINDEX"]);
                                break;
                            case "CL_POSTCITY":
                                textBox_CL_POSTCITY.Text = Convert.ToString(dr["CL_POSTCITY"]);
                                break;
                            case "CL_POSTSTREET":
                                textBox_CL_POSTSTREET.Text = Convert.ToString(dr["CL_POSTSTREET"]);
                                break;
                            case "CL_POSTBILD":
                                textBox_CL_POSTBILD.Text = Convert.ToString(dr["CL_POSTBILD"]);
                                break;
                            case "CL_POSTFLAT":
                                textBox_CL_POSTFLAT.Text = Convert.ToString(dr["CL_POSTFLAT"]);
                                break;
                            case "CL_PHONE":
                                textBox_CL_PHONE.Text = Convert.ToString(dr["CL_PHONE"]);
                                break;
                            case "CL_PASPORTSER":
                                textBox_CL_PASPORTSER.Text = Convert.ToString(dr["CL_PASPORTSER"]);
                                break;
                            case "CL_PASPORTNUM":
                                textBox_CL_PASPORTNUM.Text = Convert.ToString(dr["CL_PASPORTNUM"]);
                                break;
                            case "CL_NAMELAT":
                                textBox_CL_NAMELAT.Text = Convert.ToString(dr["CL_NAMELAT"]);
                                break;
                            case "CL_FNAMELAT":
                                textBox_CL_FNAMELAT.Text = Convert.ToString(dr["CL_FNAMELAT"]);
                                break;
                            case "CL_SNAMELAT":
                                textBox_CL_SNAMELAT.Text = Convert.ToString(dr["CL_SNAMELAT"]);
                                break;
                            case "CL_PASPORTDATE":
                                textBox_CL_PASPORTDATE.Text = Convert.ToDateTime(dr["CL_PASPORTDATE"]).ToString("d");
                                break;
                            case "CL_PASPORTDATEEND":
                                textBox_CL_PASPORTDATEEND.Text = Convert.ToDateTime(dr["CL_PASPORTDATEEND"]).ToString("d");
                                break;
                            case "CL_PASPORTBYWHOM":
                                textBox_CL_PASPORTBYWHOM.Text = Convert.ToString(dr["CL_PASPORTBYWHOM"]);
                                break;
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
                            case "cl_mail":
                                textBox_cl_mail.Text = Convert.ToString(dr["cl_mail"]);
                                break;
                            case "cl_fax":
                                textBox_cl_fax.Text = Convert.ToString(dr["cl_fax"]);
                                break;
                        }
                    }
                }
            }

            PopulateStatistics();

            if (DUP_USER.Rows.Count > 0)
            {
                dr = DUP_USER.Rows[0];
                textBox_LOGIN.Text = Convert.ToString(dr["US_ID"]);
                try
                {
                    textBox_PSW.Text = es.DecryptString(Convert.ToString(dr["US_PASSWORD"]));
                }
                catch
                {
                    textBox_PSW.Text = "Invalid psw:"+Convert.ToString(dr["US_PASSWORD"]);
                }

            }

            if (Lanta_ClientCurator.Rows.Count > 0)
            {
                dr = Lanta_ClientCurator.Rows[0];
                if (dr["LDC_CuratorUserКеу"]!=System.DBNull.Value)
                    comboBox_Curator.SelectedValue = Convert.ToInt32(dr["LDC_CuratorUserКеу"]);
                this.comboBox_Curator.SelectedValueChanged += new System.EventHandler(this.comboBox_Curator_SelectedValueChanged);

                if (dr["LDC_CuratorFilial"]!=System.DBNull.Value)
                    comboBox_Filial.SelectedValue = Convert.ToInt32(dr["LDC_CuratorFilial"]);
                this.comboBox_Filial.SelectedValueChanged += new System.EventHandler(this.comboBox_Curator_SelectedValueChanged);
             
            
            }
            else
            {   
                this.comboBox_Curator.SelectedValueChanged += new System.EventHandler(this.comboBox_Curator_SelectedValueChanged);
                comboBox_Curator.SelectedValue = MANAGER_ID;

                DataTable FromLantaPresenter = new DataTable("FromLantaPresenter");
                adapter.SelectCommand.CommandText =
                @"SELECT     tbl_Partners.PR_KEY
                FROM         UserList INNER JOIN
                                      tbl_Partners ON UserList.US_PRKEY = tbl_Partners.PR_KEY
                WHERE     UserList.US_KEY = " + MANAGER_ID.ToString();
                adapter.Fill(FromLantaPresenter);
                if (FromLantaPresenter.Rows.Count > 0)
                {
                    comboBox_Filial.SelectedValue = Convert.ToInt32(FromLantaPresenter.Rows[0]["PR_KEY"]);
                } 

            }


        }
        private void ExcludeOwn()
        {
            DataTable sputc = sput.Clone();
            DataRow[] sputca = sput.Select("CSS_CLКеу <>" + Convert.ToString(CL_KEY), "CSD_DGTURDATE");
            foreach (DataRow dr in sputca)
                sputc.Rows.Add(dr.ItemArray);
            dataGridView_SPUT.DataSource = sputc;

           

        }
        public void CalculateStatisticPrice(DataRow dr)
        {
            dr["CSD_RCCOURSE$"] = Decimal.Round(Convert.ToDecimal(dr["CSD_RCCOURSE$"]),4);
            
            dr["PerMan"] = Decimal.Round(Convert.ToDecimal(dr["CSD_DGPRICE"]) / Convert.ToDecimal(dr["CSD_DGNMEN"]),4);
             
            dr["DG_PRICE$"] = Decimal.Round(Convert.ToDecimal(dr["CSD_DGPRICE"]) * Convert.ToDecimal(dr["CSD_RCCOURSE$"]), 4);
            
            dr["PerMan$"] = Decimal.Round(Convert.ToDecimal(dr["DG_PRICE$"]) / Convert.ToDecimal(dr["CSD_DGNMEN"]), 4); ;

            if (dr["CSD_DGCNKEY"] != System.DBNull.Value)
            {
                DataTable cn = new DataTable("cn");
                adapter.SelectCommand.CommandText = @" select CN_NAME from tbl_Country where CN_KEY=" + Convert.ToString(dr["CSD_DGCNKEY"]);
                adapter.Fill(cn);
                if (cn.Rows.Count > 0)
                    dr["CN_NAME"] = cn.Rows[0]["CN_NAME"];
            }
        
        
        }
        public void PopulateStatistics()
        {
            if (CL_KEY > -1)
            {
                //Статистика и вкладка Путёвки

                adapter.SelectCommand = new SqlCommand(@"SELECT     Lanta_ClientStatDogovor.CSD_CLКеу, Lanta_ClientStatDogovor.CSD_DGCODE, Lanta_ClientStatDogovor.CSD_DGTURDATE, 
                      Lanta_ClientStatDogovor.CSD_DGNMEN, Lanta_ClientStatDogovor.CSD_DGPRICE, 
                      Lanta_ClientStatDogovor.CSD_DGRATE, Lanta_ClientStatDogovor.CSD_DGPRICE / (Case when  Lanta_ClientStatDogovor.CSD_DGNMEN=0 then 1 else Lanta_ClientStatDogovor.CSD_DGNMEN end)  AS PerMan,
                      Lanta_ClientStatDogovor.CSD_DGCRDATE, Lanta_ClientStatDogovor.CSD_RCCOURSE$, 
                      Lanta_ClientStatDogovor.CSD_DGPRICE * Lanta_ClientStatDogovor.CSD_RCCOURSE$ AS DG_PRICE$, 
                      Lanta_ClientStatDogovor.CSD_DGPRICE / (Case When Lanta_ClientStatDogovor.CSD_DGNMEN=0 then 1 else Lanta_ClientStatDogovor.CSD_DGNMEN end) * Lanta_ClientStatDogovor.CSD_RCCOURSE$ AS PerMan$, 
                      Lanta_ClientStatDogovor.CSD_DGKEY, Lanta_ClientStatDogovor.CSD_DGCOMMENT,Lanta_ClientStatDogovor.CSD_DodovorType,
                    Lanta_ClientStatDogovor.CSD_DGCNKEY, tbl_Country.CN_NAME
                    FROM         Lanta_ClientStatDogovor LEFT OUTER JOIN
                      tbl_Country ON Lanta_ClientStatDogovor.CSD_DGCNKEY = tbl_Country.CN_KEY
                    WHERE     (Lanta_ClientStatDogovor.CSD_CLКеу =   " + Convert.ToString(CL_KEY) + @")
                    ORDER BY Lanta_ClientStatDogovor.CSD_DGTURDATE", connection);
                adapter.Fill(stat);
                dataGridView_STAT.DataSource = stat;

                //Вкладка спутники
                adapter.SelectCommand = new SqlCommand(@"SELECT     Lanta_ClientStatDogovor.CSD_DGCODE, Lanta_ClientStatDogovor.CSD_DGTURDATE, Clients.CL_NAMERUS AS TU_NAMERUS, 
                      Clients.CL_FNAMERUS AS TU_FNAMERUS, Clients.CL_SNAMERUS AS TU_SNAMERUS, Clients.CL_BIRTHDAY AS TU_BIRTHDAY, 
                      Lanta_ClientStatSputniki.CSS_CLКеу, Lanta_ClientStatSputniki.CSS_DGKEY
                        FROM         Lanta_ClientStatDogovor INNER JOIN
                      Lanta_ClientStatSputniki ON Lanta_ClientStatDogovor.CSD_DGKEY = Lanta_ClientStatSputniki.CSS_DGKEY INNER JOIN
                      Clients ON Lanta_ClientStatSputniki.CSS_CLКеу = Clients.CL_KEY
                WHERE     (Lanta_ClientStatDogovor.CSD_DodovorType = 0)AND
                (Lanta_ClientStatDogovor.CSD_CLКеу =   " + Convert.ToString(CL_KEY) + @")", connection);// ORDER BY tbl_Dogovor.DG_TURDATE
                adapter.Fill(sput);
                ExcludeOwn();

                //Вкладки сервисов путёвки tbl_DogovorList.DL_SVKEY = 1 AND 3
                adapter.SelectCommand = new SqlCommand(@"SELECT     Lanta_ClientStatDogovor.CSD_DGCODE, Lanta_ClientStatDogovorList.CSL_DLSVKEY, 
                      Lanta_ClientStatDogovorList.CSL_DLNAME, Lanta_ClientStatDogovorList.CSL_DGKEY, 
                      Lanta_ClientStatDogovorList.CSL_DLKEY
                    FROM         Lanta_ClientStatDogovorList INNER JOIN
                      Lanta_ClientStatDogovor ON Lanta_ClientStatDogovorList.CSL_CLKEY = Lanta_ClientStatDogovor.CSD_CLКеу AND 
                      Lanta_ClientStatDogovorList.CSL_DGKEY = Lanta_ClientStatDogovor.CSD_DGKEY
                    WHERE     Lanta_ClientStatDogovor.CSD_CLКеу = " + Convert.ToString(CL_KEY), connection);
                adapter.Fill(tbl_DogovorList);

                DataTable avia = tbl_DogovorList.Clone();
                DataRow[] avias = tbl_DogovorList.Select("CSL_DLSVKEY = 1 OR CSL_DLSVKEY = -1");
                foreach (DataRow dr in avias)
                    avia.Rows.Add(dr.ItemArray);
                dataGridView_AVIA.DataSource = avia;
                dataGridView_AVIA.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView_AVIA.Refresh();

                DataTable HOTEL = tbl_DogovorList.Clone();
                DataRow[] HOTELs = tbl_DogovorList.Select("CSL_DLSVKEY = 3");
                foreach (DataRow dr in HOTELs)
                    HOTEL.Rows.Add(dr.ItemArray);
                dataGridView_HOTEL.DataSource = HOTEL;
                dataGridView_HOTEL.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView_HOTEL.Refresh();

            }
        }

        private int  ReadPassanger(DataRow dr)
        {
            //Passanger_KEY
            try
            {
                bool auto = false;//auto Выключено - отображение диалога преобразования пассажира в клиента
                DialogResult res = DialogResult.OK;
                Passanger ptc = pas;
                
                if (!auto)
                    res = ptc.ShowDialog();
                if (res == DialogResult.OK)
                {
                    ptc.ReadDataToClient(dr);
                    return 1;
                }
                else
                    if (res == DialogResult.Ignore)
                    {
                        return 2;
                    }
                    else
                        return 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }      
        private void ReadTurist(DataRow dr,int TU_KEY)
        {
            //string curBase = connection.Database;
            try
            {
                if (turistFromConnection.State != ConnectionState.Open)
                    turistFromConnection.Open();
                DataTable turistDT = new DataTable("turist");
                adapter.SelectCommand = new SqlCommand("select * from tbl_Turist where TU_KEY=" + Convert.ToString(TU_KEY), turistFromConnection);
                adapter.Fill(turistDT);
                if (turistDT.Rows.Count > 0)
                {
                    DataRow drTurist = turistDT.Rows[0];
                    string[] Rule;
                    foreach (string rule in comparisonRule)
                    {
                        Rule = rule.Split('=');
                        if (!drTurist.Table.Columns.Contains(Rule[0]))//На случай копирования из старых баз, где нет некоторых полей
                            continue;
                        if (Rule[0] == "TU_BIRTHDAY" && drTurist[Rule[0]] != System.DBNull.Value)
                        {
                            DateTime BIRTHDAY = Convert.ToDateTime(drTurist[Rule[0]]).Date;
                            if (BIRTHDAY != new DateTime(1899, 1, 1))
                                dr[Rule[1]] = BIRTHDAY;
                        }
                        else
                        {
                            if (dr[Rule[1]] == System.DBNull.Value || (dr[Rule[1]] != System.DBNull.Value && Convert.ToString(dr[Rule[1]]).Length == 0))//
                                dr[Rule[1]] = drTurist[Rule[0]];
                        }


                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        private void ReadClient(DataRow dr,DataRow drnew,bool keyExist)
        {
            foreach (DataColumn col in drnew.Table.Columns)
            {
                if (col.ColumnName != "CL_KEY" || !keyExist)//Если ключа такого нет - оставляем старый
                {
                    if (dr.Table.Columns.Contains(col.ColumnName))
                        dr[col.ColumnName] = drnew[col.ColumnName];
                    
                }
            }
        }
        public void ReadStatistic(DataRow dr)
        {
            foreach (DataColumn dc in clientsDT.Columns)
            {
                switch (dc.ColumnName)
                {
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
                        break;
                }
            }

        }
        private void ReadFields(DataRow dr)
        {
            foreach (DataColumn dc in clientsDT.Columns)
            {
                switch (dc.ColumnName)
                {
                    case "CL_OPERUPDATE":
                        dr["CL_OPERUPDATE"] = MANAGER_ID;
                        break;
                    case "CL_DATEUPDATE":
                        dr["CL_DATEUPDATE"] = DateTime.Now;
                        break;
                    case "CL_PFKEY":
                        if (comboBox_CL_PFKEY.SelectedValue != null)
                            dr["CL_PFKEY"] = comboBox_CL_PFKEY.SelectedValue;
                        else
                            dr["CL_PFKEY"] = System.DBNull.Value;
                        break;
                    case "CL_CITIZEN":
                        
                        dr["CL_CITIZEN"] = comboBox_CL_CITIZEN.Text;
                        break;
                    case "CL_PASPRUSER":
                        dr["CL_PASPRUSER"] = textBox_CL_PASPRUSER.Text;
                        break;
                    case "CL_PASPRUNUM":
                        dr["CL_PASPRUNUM"] = textBox_CL_PASPRUNUM.Text;
                        break;
                    case "CL_NAMERUS":
                        dr["CL_NAMERUS"] = textBox_CL_NAMERUS.Text;
                        break;
                    case "CL_FNAMERUS":
                        dr["CL_FNAMERUS"] = textBox_CL_FNAMERUS.Text;
                        break;
                    case "CL_SNAMERUS":
                        dr["CL_SNAMERUS"] = textBox_CL_SNAMERUS.Text;
                        break;
                    case "CL_SHORTNAME":
                        dr["CL_SHORTNAME"] = textBox_CL_FNAMERUS.Text[0] + "." + textBox_CL_SNAMERUS.Text[0] + ".";
                        break;
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
                        break;
                    case "CL_BIRTHDAY":
                        DateTime ret;
                        if (DateTime.TryParse(textBox_CL_BIRTHDAY.Text, out ret))
                            dr["CL_BIRTHDAY"] = ret;
                        break;
                    case "CL_BIRTHCITY":
                        dr["CL_BIRTHCITY"] = textBox_CL_BIRTHCITY.Text;
                        break;
                    case "CL_BIRTHCOUNTRY":
                        
                        dr["CL_BIRTHCOUNTRY"] = comboBox_CL_BIRTHCOUNTRY.Text;
                        break;
                    case "CL_PASPRUBYWHOM":
                        dr["CL_PASPRUBYWHOM"] = textBox_CL_PASPRUBYWHOM.Text;
                        break;
                    case "CL_PASPRUDATE":
                        if (textBox_CL_PASPRUDATE.Text.Length == 0)
                            dr["CL_PASPRUDATE"] = System.DBNull.Value;
                        else
                        {
                            DateTime ret0;
                            if (DateTime.TryParse(textBox_CL_PASPRUDATE.Text, out ret0))
                                dr["CL_PASPRUDATE"] = ret0;
                        }
                        break;
                    case "CL_POSTINDEX":
                        dr["CL_POSTINDEX"] = textBox_CL_POSTINDEX.Text;
                        break;
                    case "CL_POSTCITY":
                        dr["CL_POSTCITY"] = textBox_CL_POSTCITY.Text;
                        break;
                    case "CL_POSTSTREET":
                        dr["CL_POSTSTREET"] = textBox_CL_POSTSTREET.Text;
                        break;
                    case "CL_POSTBILD":
                        dr["CL_POSTBILD"] = textBox_CL_POSTBILD.Text;
                        break;
                    case "CL_POSTFLAT":
                        dr["CL_POSTFLAT"] = textBox_CL_POSTFLAT.Text;
                        break;
                    case "CL_ADDRESS":
                        string addr = "";
                        if (textBox_CL_POSTINDEX.Text.Length > 0)
                            addr += textBox_CL_POSTINDEX.Text + ", ";
                        addr += textBox_CL_POSTCITY.Text + ", ";
                        addr += textBox_CL_POSTSTREET.Text + ", ";
                        addr += "д." + textBox_CL_POSTBILD.Text + ", ";
                        addr += "кв." + textBox_CL_POSTFLAT.Text;
                        dr["CL_ADDRESS"] = addr;
                        break;
                    case "CL_PHONE":
                        dr["CL_PHONE"] = textBox_CL_PHONE.Text;
                        break;
                    case "CL_PASPORTSER":
                        dr["CL_PASPORTSER"] = textBox_CL_PASPORTSER.Text;
                        break;
                    case "CL_PASPORTNUM":
                        dr["CL_PASPORTNUM"] = textBox_CL_PASPORTNUM.Text;
                        break;
                    case "CL_NAMELAT":
                        dr["CL_NAMELAT"] = textBox_CL_NAMELAT.Text;
                        break;
                    case "CL_FNAMELAT":
                        dr["CL_FNAMELAT"] = textBox_CL_FNAMELAT.Text;
                        break;
                    case "CL_SNAMELAT":
                        dr["CL_SNAMELAT"] = textBox_CL_SNAMELAT.Text;
                        break;
                    case "CL_PASPORTDATE":
                        DateTime ret2;
                        if (DateTime.TryParse(textBox_CL_PASPORTDATE.Text, out ret2))
                            dr["CL_PASPORTDATE"] = ret2;
                        break;
                    case "CL_PASPORTDATEEND":
                        DateTime ret3;
                        if (DateTime.TryParse(textBox_CL_PASPORTDATEEND.Text, out ret3))
                            dr["CL_PASPORTDATEEND"] = ret3;
                        break;
                    case "CL_PASPORTBYWHOM":
                        dr["CL_PASPORTBYWHOM"] = textBox_CL_PASPORTBYWHOM.Text;
                        break;
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
                        break;
                    case "cl_mail":
                        dr["cl_mail"] = textBox_cl_mail.Text;
                        break;
                    case "cl_fax":
                        dr["cl_fax"] = textBox_cl_fax.Text;
                        break;
                }
            }

        }
        private void ReadClientCurator()
        {
            DataRow dr;
            bool added = false;
            if (Lanta_ClientCurator.Rows.Count > 0)
                dr = Lanta_ClientCurator.Rows[0];
            else
            {
                dr = Lanta_ClientCurator.NewRow();
                added = true;
            }
            dr["LDC_ClientКеу"] = CL_KEY;
            dr["LDC_MANAGER"] = MANAGER_ID;
            dr["LDC_UPDATE"] = DateTime.Now;
            if (comboBox_Curator.SelectedValue != null)
                dr["LDC_CuratorUserКеу"] = Convert.ToInt32(comboBox_Curator.SelectedValue);
            else
                dr["LDC_CuratorUserКеу"] = MANAGER_ID;

            if (comboBox_Filial.SelectedValue != null)
                dr["LDC_CuratorFilial"] = Convert.ToInt32(comboBox_Filial.SelectedValue);
            else
                dr["LDC_CuratorFilial"] = 1;
           
            if (added)
                Lanta_ClientCurator.Rows.Add(dr);

        }
        private void ReadClientAcess()
        {
            DataRow dr;
            if (Lanta_ClientAccess.Rows.Count > 0)
            {
                dr = Lanta_ClientAccess.Rows[0];
                dr["CA_MANAGER"] = MANAGER_ID;
                dr["CA_CREATEDATE"] = DateTime.Now;
            }
            else
            {
                if (textBox_LOGIN.Text.Length > 0)
                {
                    dr = Lanta_ClientAccess.NewRow();
                    dr["CA_CLКеу"] = Convert.ToString(CL_KEY);
                    dr["CA_MANAGER"] = MANAGER_ID;
                    dr["CA_CREATEDATE"] = DateTime.Now;
                    Lanta_ClientAccess.Rows.Add(dr);
                }
            }

            if (DUP_USER.Rows.Count > 0)
            {
                dr = DUP_USER.Rows[0];
                dr["US_ID"] = textBox_LOGIN.Text;
                dr["US_PASSWORD"] = es.EncryptString(textBox_PSW.Text);
                dr["US_AGENT"] = 1;
                dr["US_REG"] = 1;
                dr["US_TURAGENT"] = 1;
                dr["US_PRKEY"] = 0;
                dr["US_USERKEY"] = TU_KEY;

            }
            else
            {
                if (textBox_LOGIN.Text.Length > 0)
                {
                    dr = DUP_USER.NewRow();
                    dr["US_KEY"] = -1;
                    dr["US_ID"] = textBox_LOGIN.Text;
                    dr["US_PASSWORD"] = es.EncryptString(textBox_PSW.Text);
                    DUP_USER.Rows.Add(dr);
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ValidateTab(tabControl1.SelectedTab.Name, false))
            {
                MessageBox.Show("Все поля на вкладке заполнены успешно!");
            }
        }

        public bool ValidateLogin()
        {
            return ValidateLogin(true);
        }

        public bool ValidateLogin(bool visible)
        {
            //Проверка логина
            logins.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("Select  US_KEY from [dbo].[dup_user] WHERE US_ID='" + textBox_LOGIN.Text + "'", connection);
            adapter.Fill(logins);
            if (logins.Rows.Count > 0)
            {
                //Такой ключ уже есть в базе. Смотрим кому он принадлежит
                int US_KEY = Convert.ToInt32(logins.Rows[0]["US_KEY"]);
                logins2.Clear();
                adapter.SelectCommand = new SqlCommand("Select  CA_CLКеу from Lanta_ClientAccess WHERE CA_DUPUSERKEY=" + Convert.ToString(US_KEY), connection);
                adapter.Fill(logins2);
                if (logins2.Rows.Count > 0)
                {
                    if (Convert.ToInt64(logins2.Rows[0]["CA_CLКеу"]) == CL_KEY)
                    {//Пароль принадлежит текущему клиенту
                    }
                    else
                    {
                        if (visible)
                            MessageBox.Show(this, "Такой логин уже существует!", "Проверка уникальности логина", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    if (visible)
                        MessageBox.Show(this, "Такой логин уже существует!", "Проверка уникальности логина", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        public bool ValidateTab(string tabPage, bool External)
        {
            string tabName = "";
            if (tabPage != "-1")
                tabName = " на вкладке " + tabControl1.TabPages[tabPage].Text;
            switch (tabPage)
            {
                case "-1":
                    for (int i = 0; i < tabControl1.TabCount; i++)
                        if (!ValidateTab(tabControl1.TabPages[i].Name, External))
                            return false;
                    break;
                case "tabPage1":
                    if (!
                            (
                             ValidateField(textBox_CL_NAMERUS.Text, label_CL_NAMERUS.Text + tabName, 35)
                            && ValidateRus(textBox_CL_NAMERUS.Text, label_CL_NAMERUS.Text + tabName)
                            && ValidateField(textBox_CL_FNAMERUS.Text, label_CL_FNAMERUS.Text + tabName, 20)
                            && ValidateRus(textBox_CL_FNAMERUS.Text, label_CL_FNAMERUS.Text + tabName)
                            && ValidateField(textBox_CL_POSTINDEX.Text, label_CL_POSTINDEX.Text + tabName, 8)
                            && ValidateField(textBox_CL_POSTCITY.Text, label_CL_POSTCITY.Text + tabName, 60)
                            && ValidateField(textBox_CL_POSTSTREET.Text, label_CL_POSTSTREET.Text + tabName, 25)
                            && ValidateField(textBox_CL_POSTBILD.Text, label_CL_POSTBILD.Text + tabName, 10)
                            && ValidateField(textBox_CL_POSTFLAT.Text, label_CL_POSTFLAT.Text + tabName, 4)
                            )
                            )
                        return false;

                        RusUkCitizen = CL_CITIZEN_LCI_ISO == 643;
                        if (RusUkCitizen)//Тогда проверяется отчество
                        {
                          if (!ValidateField(textBox_CL_SNAMERUS.Text, label_CL_SNAMERUS.Text + tabName, 15))
                            return false;  
                          if (!ValidateRus(textBox_CL_SNAMERUS.Text, label_CL_SNAMERUS.Text + tabName))
                            return false;  
                        }
                        //Проверка максимального размера полей и уведомление при превышении
                        if (!ValidateFieldMaxLen(textBox_CL_CITIZEN.Text, textBox_CL_CITIZEN.Text + tabName, 50))
                            return false;
                        if (!ValidateFieldMaxLen(textBox_CL_PASPRUSER.Text, textBox_CL_PASPRUSER.Text + tabName, 10))
                            return false;                       
                        if (!ValidateFieldMaxLen(textBox_CL_PASPRUNUM.Text, textBox_CL_PASPRUNUM.Text + tabName, 10))
                            return false;
                        if (!ValidateFieldMaxLen(textBox_CL_PASPRUBYWHOM.Text, label_CL_PASPRUBYWHOM.Text + tabName, 255))
                            return false;
                        if (!ValidateFieldMaxLen(textBox_CL_BIRTHCITY.Text, textBox_CL_BIRTHCITY.Text + tabName, 60))
                            return false;
                      
                        if (Presenter)//Если является лицом, заключающим договор, должны быть данные паспорта 
                        {
                            if (comboBox_CL_CITIZEN.Text == country.ToString())//Если гражданин своей страны, то проверяются  данные внутреннего паспорта
                            {
                              if (!
                                   (
                                    ValidateField(textBox_CL_PASPRUSER.Text, label_CL_PASPRUSER.Text + tabName, 10)
                                   && ValidateField(textBox_CL_PASPRUNUM.Text, label_CL_PASPRUNUM.Text + tabName, 10)
                                   && ValidateField(textBox_CL_PASPRUBYWHOM.Text, label_CL_PASPRUBYWHOM.Text + tabName, 255)
                                   && ValidateFieldDate(textBox_CL_PASPRUDATE.Text, label_CL_PASPRUDATE.Text + tabName)
                                   )
                                   )
                                return false;
                            }
                        }

                        if (!Presenter)
                        {
                            if (!
                                (
                                ValidateFieldDate(textBox_CL_BIRTHDAY.Text, label_CL_BIRTHDAY.Text + tabName)
                                && ValidateField(textBox_CL_BIRTHCITY.Text, label_CL_BIRTHCITY.Text + tabName, 60)
                                )
                                )
                                return false;
                            if (External
                                && !ValidateField(textBox_CL_SEX.Text, label_CL_SEX.Text + tabName, 20)
                                && !ValidateField(textBox_CL_RealSex.Text, label_CL_SEX.Text + tabName, 20)
                                )
                                return false;
                        }
                    break;
                case "tabPage2":
                    if (!ValidateFieldMaxLen(textBox_CL_PASPORTSER.Text, textBox_CL_PASPORTSER.Text + tabName, 5))
                        return false;
                    if (!ValidateFieldMaxLen(textBox_CL_PASPORTNUM.Text, textBox_CL_PASPORTNUM.Text + tabName, 13))
                        return false;
                    if (!ValidateFieldMaxLen(textBox_CL_NAMELAT.Text, textBox_CL_NAMELAT.Text + tabName, 35))
                        return false;
                    if (!ValidateFieldMaxLen(textBox_CL_FNAMELAT.Text, textBox_CL_FNAMELAT.Text + tabName, 20))
                        return false;   
                    if (!ValidateFieldMaxLen(textBox_CL_FNAMELAT.Text, textBox_CL_FNAMELAT.Text + tabName, 20))
                        return false;
                    if (!ValidateFieldMaxLen(textBox_CL_PASPORTBYWHOM.Text, textBox_CL_PASPORTBYWHOM.Text + tabName, 255))
                        return false;   
             
                    if (!Presenter && CheckZagran)
                    {
                        if (!
                            (
                               ValidateField(textBox_CL_PASPORTSER.Text, label_CL_PASPORTSER.Text + tabName, 5)
                            && ValidateField(textBox_CL_PASPORTNUM.Text, label_CL_PASPORTNUM.Text + tabName, 13)
                            && ValidateField(textBox_CL_NAMELAT.Text, label_CL_NAMELAT.Text + tabName, 35)
                            && ValidateField(textBox_CL_FNAMELAT.Text, label_CL_FNAMELAT.Text + tabName, 20)
                            //&& ValidateField(textBox_CL_SNAMELAT.Text, label_CL_SNAMELAT.Text+tabName)
                            && ValidateFieldDate(textBox_CL_PASPORTDATE.Text, label_CL_PASPORTDATE.Text + tabName)
                            && ValidateFieldDate(textBox_CL_PASPORTDATEEND.Text, label_CL_PASPORTDATEEND.Text + tabName)
                            && ValidateField(textBox_CL_PASPORTBYWHOM.Text, label_CL_PASPORTBYWHOM.Text + tabName, 15)
                            )
                            )
                            return false;
                    }
                    break;
                case "tabPage3":
                    if (!(checkBox1_CL_SEX.Checked || checkBox2_CL_SEX.Checked))//Если не ребёнок и не инфант проверяются данные
                    {
                        if (!
                            (
                               ValidateField(textBox_CL_PHONE.Text, label_CL_PHONE.Text + tabName, 60)
                               && ValidateField(textBox_cl_mail.Text.Replace(" ", ""), label_cl_mail.Text + tabName, 50)
                            )
                            )
                            return false;
                    }
                    break;
                case "tabPage8":

                    if (Presenter || textBox_LOGIN.Text.Length > 0)
                    {
                        if (!(ValidateField(textBox_LOGIN.Text, label_LOGIN.Text + tabName, 50)
                            && ValidateField(textBox_PSW.Text, label_PSW.Text + tabName, 256)
                            )
                            )
                            return false;

                        //Проверка логина
                        if (!ValidateLogin())
                            return false;
                    }
                    break;
            }
            return true;
        }
        /*
         Таблица [dbo].[Clients](			- Постоянные клиенты
            [CL_KEY] [int] NOT NULL,			- ключ
            [CL_OPERUPDATE] [int] NOT NULL,		- менеджер из таблицы UserList
            [CL_DATEUPDATE] [datetime] NULL,		- дата обновления информации
            [CL_PFKEY] [int] NULL,				- профессия из таблицы dbo.Profession 
            [CL_NAMERUS] [varchar](25) NOT NULL,	- фамилия по-русски
            [CL_NAMELAT] [varchar](25) NOT NULL,	- фамилия по-английски
            [CL_SHORTNAME] [varchar](4) NULL,		- Короткое имя (первая буква с точкой)
            [CL_SEX] [smallint] NULL,			- Пол 0-мужчина, 1-женщина
            [CL_FNAMERUS] [varchar](20) NOT NULL,	- Имя по-русски
            [CL_FNAMELAT] [varchar](20) NOT NULL,	- Имя по-английски
            [CL_SNAMERUS] [varchar](15) NULL,		- Отчество по-русски
            [CL_SNAMELAT] [varchar](15) NULL,		- Отчество по-английски
            [CL_BIRTHDAY] [datetime] NULL,		- Дата рождения
            [CL_BIRTHCOUNTRY] [varchar](25) NULL,	- Страна рождения
            [CL_BIRTHCITY] [varchar](60) NULL,		- Город рождения
            [CL_CITIZEN] [varchar](15) NULL,		- Гражданство страна	
            [CL_ADDRESS] [varchar](110) NULL,		- Адрес	
            [CL_POSTINDEX] [varchar](8) NULL,		- Почтовый индекс
            [CL_POSTCITY] [varchar](60) NULL,		- Адрес, город
            [CL_POSTSTREET] [varchar](25) NULL,		- Адрес, улица
            [CL_POSTBILD] [varchar](10) NULL,		- Адрес, дом
            [CL_POSTFLAT] [varchar](4) NULL,		- Адрес, квартира
            [CL_PHONE] [varchar](60) NULL,		- Телефон в виде строки с кодом
            [CL_PASPORTSER] [varchar](5) NULL,		- Загранпаспорт, серия
            [CL_PASPORTNUM] [varchar](13) NULL,		- Загранпаспорт, номер паспорта
            [CL_PASPORTDATE] [datetime] NULL,		- Загранпаспорт, дата выдачи
            [CL_PASPORTDATEEND] [datetime] NULL,	- Загранпаспорт, окончание срока действия
            [CL_PASPORTBYWHOM] [varchar](15) NULL,	- Загранпаспорт, кем выдан
            [CL_PASPRUSER] [varchar](10) NULL,		- Внутренний паспорт, серия
            [CL_PASPRUNUM] [varchar](10) NULL, 		- Внутренний паспорт, номер
            [CL_PASPRUDATE] [datetime] NULL,		- Внутренний паспорт, дата выдачи
            [CL_PASPRUBYWHOM] [varchar](45) NULL, 	- Внутренний паспорт, кем выдан
            [CL_ISMARK] [int] NULL,				- Оповещение
            [CL_TYPE] [int] NULL,				- Признак клиента из dbo.TitleTypeClient 
            [CL_IMPRESSNOTE] [varchar](254) NULL,	- Общие впечатления, текст
            [CL_NOTE] [varchar](250) NULL,		- Примечания, особые отметки
            [CL_REMARK] [varchar](250) NULL,		- Примечания, примечания
            [CL_IMPRESSKEY] [int] NULL,			- Впечатления клиента из справочника dbo.TitleTypeImpress
            [CL_TITLE1] [varchar](80) NULL,		- Заголовки из вкладки Служебные отметки
            [CL_TITLE2] [varchar](80) NULL,
            [CL_TITLE3] [varchar](80) NULL,
            [CL_TITLE4] [varchar](80) NULL,
            [CL_FUTURE] [varchar](254) NULL,		- Пожелания
            [CL_LASTSTAT] [datetime] NULL,		- Дата формирования последней статистики
            [CL_SUMMA] [float] NULL,			- Сумма по клиенту
            [CL_NMENWITH] [smallint] NULL,		- Число спутников
            [CL_SUMDOGOVOR] [float] NULL,			- Сумма по путёвкам
            [CL_NTRIP] [smallint] NULL,			- Число поездок
            [ROWID] [timestamp] NOT NULL,
            [cl_fax] [char](70) NULL,			- Факс
            [cl_mail] [char](70) NULL,			- e-mail-->>>varchar(50)
            [CL_MINCOST] [float] NULL,			- Минимальная стоимость поездки
            [CL_MAXCOST] [float] NULL,			- Максимальная мтоимость поездки
            [CL_DSKEY] [int] NULL,				- NULL во всех записях
            [CL_RealSex] [smallint] NULL,			- Действительный пол
 
         */
        private bool ValidateFieldDate(string validatingString, string fieldName)
        {
            if (validatingString.Length == 0)
            {
                MessageBox.Show(vm + fieldName, "Проверка полей постоянного клиента", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            DateTime dt;
            if (!DateTime.TryParse(validatingString, out dt))
            {
                MessageBox.Show(vm + fieldName + "\r\nПравильный формат " + DateTime.Now.ToString("d"), "Проверка полей", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;

        }
        private bool ValidateRus(string validatingString, string fieldName)
        {//проверка полей на русские буквы
            byte[] bytes = Encoding.ASCII.GetBytes(validatingString);
            int count_russian = 0, count_nonrus = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 63 || bytes[i] == 32 || bytes[i] == 45)//Русский символ, пробел или дефис
                    count_russian++;
                else
                    count_nonrus++;

            }
            //bool ГражданинЛатвии = CL_CITIZEN_LCI_ISO == 428;
            if (bytes.Length > 0 && (bytes[0] == 32 || bytes[0] == 45))
            {
                MessageBox.Show(vm + fieldName + "\r\nПоле не может начинаться с пробела или дефиса!", "Проверка полей постоянного клиента", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (count_russian != bytes.Length)
            {
                MessageBox.Show(vm + fieldName + "\r\nПоле должно заполняться на национальном языке!", "Проверка полей постоянного клиента", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }
        private bool ValidateEMail(string mailAddress)
        {
            //nslookup -type=mx mail.ru
            IPHostEntry iphe = Dns.GetHostEntry("mail.ru"); 
            
            

            return true;
        }
        private bool ValidateFieldMaxLen(string validatingString, string fieldName, int maxlen)
        {
            if (validatingString.Length > maxlen)
            {
                MessageBox.Show(vmax + maxlen.ToString() + " " + fieldName, "Проверка полей постоянного клиента", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;

        }
        private bool ValidateField(string validatingString, string fieldName, int maxlen)
        {
            if (validatingString.Length == 0)
            {
                MessageBox.Show(vm + fieldName, "Проверка полей постоянного клиента", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (validatingString.Length > maxlen)
            {
                MessageBox.Show(vmax + maxlen.ToString() + " " + fieldName, "Проверка полей постоянного клиента", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;

        }

        private void button2_Click(object sender, EventArgs e)//Сохранение
        {
            if (SaveClient(true))
            {
                //MessageBox.Show("Данные сохранены успешно!");
                //Закрывать только когда проверка прошла
                this.DialogResult = DialogResult.OK;
            }
        }
        public bool  SaveClient(bool needCheck)
        {
            return SaveClient(-1, null, false, needCheck)==1;
        }
        public void UpdateClientByTurist(int TU_KEY, SqlConnection turistFromConnection)
        {
            if (TU_KEY > -1)
            {
                this.turistFromConnection = turistFromConnection;
                SaveClient(TU_KEY, null, false, false);
            }
        }
        public void SaveClientByTurist(int TU_KEY,  SqlConnection turistFromConnection)
        {
            this.turistFromConnection = turistFromConnection;
            SaveClient(TU_KEY, null, false, false);
        }
        public int SaveClientByPassanger(Passanger pas)
        {
            this.pas = pas;
            return SaveClient(-1, null, false, false);
        }
        public void SaveClientByRow(DataRow drClient, bool keyExist)
        {
            SaveClient(-1, drClient, keyExist, false);
        }
        private int SaveClient(int TU_KEY,DataRow drClient,bool keyExist,bool needCheck)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (needCheck)
                {
                    if (textBox_CL_RealSex.Text.Length == 0)
                    {
                        textBox_CL_RealSex.Text = Convert.ToString(Convert.ToInt16(radioButton2.Checked));
                    }

                    if (!ValidateTab("-1", false))
                    {
                        return 0;
                    }
                }
                DataRow dr = null;
                if (isEdit)//Сохранение редактированного клиента
                    dr = clientsDT.Rows[0];
                else//Вставка нового клиента
                {
                    dr = clientsDT.NewRow();
                    clientsDT.Rows.Add(dr);
                    long newKey = -1;
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MAX(CL_KEY) AS Expr1 FROM Clients", connection);
                    newKey = Convert.ToInt64(cmd.ExecuteScalar()) + 1;
                    connection.Close();
                    if (newKey > -1)
                    {
                        dr["CL_KEY"] = newKey;
                        if (needCheck|| dr["CL_OPERUPDATE"]==System.DBNull.Value)
                            dr["CL_OPERUPDATE"] = MANAGER_ID;
                        CL_KEY = newKey;

                    }
                    else
                    {
                        MessageBox.Show("Невозможно вставить строку!");
                        return 0; ;
                    }
                    dr["CL_DATEUPDATE"] = DateTime.Now;
                }
                if (drClient != null)
                {
                    ReadClient(dr, drClient, keyExist);
                }
                else
                    if (pas != null)
                    {
                        int res = ReadPassanger(dr);
                        if (res != 1)
                            return res;
                    }
                    else
                        if (TU_KEY > -1)
                        {

                            ReadTurist(dr, TU_KEY);
                            /* Апдейтить статистику надо после сохранения клиента и привязки его к туристу
         */
                        }
                        else
                            if (needCheck)
                                ReadFields(dr);
                            else
                                ReadStatistic(dr);

                adapter.SelectCommand = new SqlCommand("select * from clients where CL_KEY=" + Convert.ToString(CL_KEY), connection);
                builder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.UpdateCommand.CommandText = "UPDATE [clients] SET [CL_OPERUPDATE] = @p2, [CL_DATEUPDATE] = @p3, [CL_PFKEY] = @p4, [CL_NAMERUS] = @p5, [CL_NAMELAT] = @p6, [CL_SHORTNAME] = @p7, [CL_SEX] = @p8, [CL_FNAMERUS] = @p9, [CL_FNAMELAT] = @p10, [CL_SNAMERUS] = @p11, [CL_SNAMELAT] = @p12, [CL_BIRTHDAY] = @p13, [CL_BIRTHCOUNTRY] = @p14, [CL_BIRTHCITY] = @p15, [CL_CITIZEN] = @p16, [CL_ADDRESS] = @p17, [CL_POSTINDEX] = @p18, [CL_POSTCITY] = @p19, [CL_POSTSTREET] = @p20, [CL_POSTBILD] = @p21, [CL_POSTFLAT] = @p22, [CL_PHONE] = @p23, [CL_PASPORTSER] = @p24, [CL_PASPORTNUM] = @p25, [CL_PASPORTDATE] = @p26, [CL_PASPORTDATEEND] = @p27, [CL_PASPORTBYWHOM] = @p28, [CL_PASPRUSER] = @p29, [CL_PASPRUNUM] = @p30, [CL_PASPRUDATE] = @p31, [CL_PASPRUBYWHOM] = @p32, [CL_ISMARK] = @p33, [CL_TYPE] = @p34, [CL_IMPRESSNOTE] = @p35, [CL_NOTE] = @p36, [CL_REMARK] = @p37, [CL_IMPRESSKEY] = @p38, [CL_TITLE1] = @p39, [CL_TITLE2] = @p40, [CL_TITLE3] = @p41, [CL_TITLE4] = @p42, [CL_FUTURE] = @p43, [CL_LASTSTAT] = @p44, [CL_SUMMA] = @p45, [CL_NMENWITH] = @p46, [CL_SUMDOGOVOR] = @p47, [CL_NTRIP] = @p48, [cl_fax] = @p49, [cl_mail] = @p50, [CL_MINCOST] = @p51, [CL_MAXCOST] = @p52, [CL_DSKEY] = @p53, [CL_RealSex] = @p54 WHERE ([CL_KEY] = @p1)";
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.Update(clientsDT);

                //Сохраненеие данных семьи
                SaveFamilyInfo();

                //Сохранение паролей
                SaveClientAccess();

                //Сохранение паролей
                SaveClientCurator();
               
                //Сохранение статистики
                SaveStatistic();

            }
            catch (Exception)
            {
                //MessageBox.Show(cex.ToString(), "Исключение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor.Current = Cursors.Arrow; }
            return 1;
        }
        public void SaveClientCurator()
        {
            ReadClientCurator();
            adapter.SelectCommand.CommandText = @"SELECT     LDC_ID, LDC_ClientКеу, LDC_CuratorUserКеу, LDC_MANAGER, LDC_UPDATE, LDC_CuratorFilial
             FROM    Lanta_ClientCurator
                WHERE    LDC_ClientКеу =" + Convert.ToString(CL_KEY);
            builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.UpdateCommand.CommandText = "UPDATE [Lanta_ClientCurator] SET [LDC_ClientКеу] = @p1, [LDC_CuratorUserКеу] = @p2, [LDC_MANAGER] = @p3, [LDC_UPDATE] = @p4, [LDC_CuratorFilial] = @p5 WHERE [LDC_ID] = @p6";
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.Update(Lanta_ClientCurator);

            //Отправка сообщения менеджеру в CallRegistration
            if (checkBox_MessageToCurator.Checked)
            {
                //Добавление сообщения менеджеру
                DataTable Lanta_CallRegistration = new DataTable("Lanta_CallRegistration");
                adapter.SelectCommand.CommandText = @"SELECT     CR_KEY, CR_CLKEY, CR_CLNAMERUS, CR_CLMAIL, CR_RECALLTIME, CR_DESCRIPTION, CR_MANAGER, CR_CREATEDATE, CR_ACTIVE
                FROM         Lanta_CallRegistration";
                adapter.FillSchema(Lanta_CallRegistration, SchemaType.Source);
                dr = Lanta_CallRegistration.NewRow();
                dr["CR_CLKEY"] = CL_KEY;
                dr["CR_CLNAMERUS"] = textBox_CL_NAMERUS.Text+" "+textBox_CL_FNAMERUS.Text+" "+textBox_CL_SNAMERUS.Text;
                dr["CR_CLMAIL"] = textBox_cl_mail.Text;
                dr["CR_RECALLTIME"] = DateTime.Now.AddMinutes(5);
                dr["CR_DESCRIPTION"] = @"Вы назначены персональным менеджером для клиента "+Convert.ToString(dr["CR_CLNAMERUS"]);
                dr["CR_MANAGER"] = Convert.ToInt32(Lanta_ClientCurator.Rows[0]["LDC_CuratorUserКеу"]);
                dr["CR_CREATEDATE"] = DateTime.Now;
                dr["CR_ACTIVE"] = true;
                Lanta_CallRegistration.Rows.Add(dr);
                builder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.Update(Lanta_CallRegistration);

                //Проверка записи  сообщения и получение добавленного ключа
                Lanta_CallRegistration = new DataTable("Lanta_CallRegistration");
                adapter.SelectCommand.CommandText = @"SELECT     CR_KEY
                FROM         Lanta_CallRegistration
                WHERE     (CR_ACTIVE = 1) AND (CR_CLKEY = "+CL_KEY.ToString()+@")
                ORDER BY CR_CREATEDATE DESC";
                adapter.Fill(Lanta_CallRegistration);
                if (Lanta_CallRegistration.Rows.Count > 0)
                {
                    DataTable Lanta_CallForwardQueue = new DataTable("Lanta_CallForwardQueue");
                    adapter.SelectCommand.CommandText = @"SELECT CFQ_KEY, CFQ_MessageKey
                      FROM Lanta_CallForwardQueue";
                    adapter.FillSchema(Lanta_CallForwardQueue, SchemaType.Source);
                    dr = Lanta_CallForwardQueue.NewRow();
                    dr["CFQ_KEY"] = Convert.ToInt32(Lanta_ClientCurator.Rows[0]["LDC_CuratorUserКеу"]);
                    dr["CFQ_MessageKey"] = Lanta_CallRegistration.Rows[0]["CR_KEY"];
                    Lanta_CallForwardQueue.Rows.Add(dr);
                   
                    builder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.Update(Lanta_CallForwardQueue);
                }
            }


            
            //Отправка сообщения клиенту на почту об изменениии персонального менеджера
            if (checkBox_MessageToClent.Checked)
            {
                int city = 1;
                string message = GetUserMessageBody(out city);
                if (message.Length > 0)
                {
                    DataTable LTS_SpamServer = new DataTable("LTS_SpamServer");
                    adapter.SelectCommand.CommandText = @"SELECT     LSS_MailFrom, LSS_MailTo, LSS_Subject, LSS_Body, LSS_ServiceSend, LSS_PRKey
                        FROM         LTS_SpamServer";
                    adapter.FillSchema(LTS_SpamServer, SchemaType.Source);
                    dr = LTS_SpamServer.NewRow();

                    dr["LSS_MailTo"] = textBox_cl_mail.Text;
                    dr["LSS_Body"] = message;
                    dr["LSS_ServiceSend"] = "ALEX_FIT";
                    dr["LSS_PRKey"] = -1;

                    switch (city)
                    {
                        case 218:
                            dr["LSS_MailFrom"] = "lsale@express-voyage.com.ua";
                            dr["LSS_Subject"] = "Экспресс Вояж: система оповещения о изменении офиса обслуживания";
                            break;
                        default:
                            dr["LSS_MailFrom"] = "info@lantatur.ru";
                            dr["LSS_Subject"] = "Ланта-тур вояж: система оповещения о изменении офиса обслуживания";
                            break;
                    }
                    
                   
                    LTS_SpamServer.Rows.Add(dr);

                    builder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.Update(LTS_SpamServer);
                }
            }
        }
        public string GetUserMessageBody(out int PR_CTKEY)
        {
            PR_CTKEY = 1;
            int CuratorKey = Convert.ToInt32(Lanta_ClientCurator.Rows[0]["LDC_CuratorUserКеу"]);
            DataTable CuratorInfo = new DataTable("CuratorInfo");
            adapter.SelectCommand.CommandText = @"SELECT     UserList.US_JOB, tbl_Partners.PR_NAME, UserList.US_NAME, UserList.US_FNAME, UserList.US_SNAME, UserList.US_ICQ, 
                      tbl_Partners.PR_PHONES, UserList.US_MAILBOX, tbl_Partners.PR_POSTINDEX, CityDictionary.CT_NAME, tbl_Partners.PR_ADRESS, tbl_Partners.PR_CTKEY
                        FROM         UserList INNER JOIN
                         tbl_Partners ON UserList.US_PRKEY = tbl_Partners.PR_KEY INNER JOIN
                         CityDictionary ON tbl_Partners.PR_CTKEY = CityDictionary.CT_KEY
                        WHERE     UserList.US_KEY = " + CuratorKey.ToString();
            adapter.Fill(CuratorInfo);
            if (CuratorInfo.Rows.Count == 0)
                return "";
            dr = CuratorInfo.Rows[0];
            string cur;
            string titul = "";
            string ret = "";
            PR_CTKEY = Convert.ToInt32(dr["PR_CTKEY"]);
            switch (PR_CTKEY)
            {
                case 218://Киев, Украина
                    cur = Convert.ToString(dr["PR_PHONES"]);
                    if (cur.Length > 0)
                        titul = titul + "\r\n тел. " + cur;
                    cur = Convert.ToString(dr["PR_NAME"]);
                    if (cur.Length > 0)
                        titul = titul + "\r\n Офiс: " + cur;
                    titul = titul + "\r\n Адресa офiса:" + Convert.ToString(dr["PR_POSTINDEX"])
                        + " " + Convert.ToString(dr["CT_NAME"])
                        + " " + Convert.ToString(dr["PR_ADRESS"])
                        ;

                    ret = @"<center><a href='http://www.express-voyage.com.ua/'><img src='http://www.express-voyage.com.ua/img/logo.gif' border=0></a>
            <hr><font color=""#333355"">Уважаемый " + textBox_CL_NAMERUS.Text + " " + textBox_CL_FNAMERUS.Text + " " + textBox_CL_SNAMERUS.Text + @"!</font><hr></center>
                    <p align=left><ul>
                    Сообщаем Вам координаты офиса, где Вы можете получить всю интересующую информацию по турам." + titul.Replace("\r\n", "<br>") + @"
                   <br><br><center><font color=""#333355"">C уважением, Экспресс Вояж.</font> </center></ul>
                    ";
                    break;
                default://Москва
                    /*
                    titul = titul+
                         Convert.ToString(dr["US_JOB"])
                        +" "+ Convert.ToString(dr["US_NAME"])
                        +" "+ Convert.ToString(dr["US_FNAME"])
                        +" "+ Convert.ToString(dr["US_SNAME"])
                        ;
                    cur = Convert.ToString(dr["US_ICQ"]);
                    if (cur.Length > 0)
                        titul = titul+"\r\n ICQ: " + cur;
                    cur = Convert.ToString(dr["US_MAILBOX"]);
                    if (cur.Length > 0)
                        titul = titul+"\r\n E-MAIL: " + cur;*/
                    cur = Convert.ToString(dr["PR_PHONES"]);
                    if (cur.Length > 0)
                        titul = titul + "\r\n тел. " + cur;
                    cur = Convert.ToString(dr["PR_NAME"]);
                    if (cur.Length > 0)
                        titul = titul + "\r\n Офис: " + cur;
                    titul = titul + "\r\n Адрес офиса:" + Convert.ToString(dr["PR_POSTINDEX"])
                        + " " + Convert.ToString(dr["CT_NAME"])
                        + " " + Convert.ToString(dr["PR_ADRESS"])
                        ;

                    ret = @"<center><a href='http://www.lantatur.ru'><img src='http://www.lantatur.ru/img/logo.gif' border=0></a>
            <hr><font color=""#333355"">Уважаемый " + textBox_CL_NAMERUS.Text + " " + textBox_CL_FNAMERUS.Text + " " + textBox_CL_SNAMERUS.Text + @"!</font><hr></center>
                    <p align=left><ul>
                    Сообщаем Вам координаты офиса, где Вы можете получить всю интересующую информацию по турам." + titul.Replace("\r\n", "<br>") + @"
                   <br><br><center><font color=""#333355"">C уважением, Ланта-тур вояж.</font> </center></ul>
                    ";

                    break;

            }
            return ret;

        }
        public void SaveClientAccess()
        {
            ReadClientAcess();
            if (DUP_USER.Rows.Count < 1)//Нечего сохранять
                return;
            SqlDataAdapter adapter = new SqlDataAdapter();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            try
            {
                SqlTransaction transaction = connection.BeginTransaction("Transaction");
                long US_KEY = Convert.ToInt32(DUP_USER.Rows[0]["US_KEY"]);
                if (US_KEY == -1)
                {
                    //Получение нового ИДшника

                    SqlCommand cmd = new SqlCommand("SELECT MAX(US_KEY) FROM [dbo].[dup_user]", connection);
                    cmd.Transaction = transaction;
                    US_KEY = Convert.ToInt64(cmd.ExecuteScalar()) + 1;
                    if (US_KEY > -1)
                    {
                        DUP_USER.Rows[0]["US_KEY"] = US_KEY;
                        Lanta_ClientAccess.Rows[0]["CA_DUPUSERKEY"] = US_KEY;
                    }

                }
                adapter.SelectCommand = new SqlCommand(
                    @"SELECT US_KEY, US_ID, US_PASSWORD, US_AGENT, US_REG, US_TURAGENT,US_PRKEY
                    FROM         DUP_USER
                    WHERE    US_KEY=" + Convert.ToString(US_KEY), connection);
                adapter.SelectCommand.Transaction = transaction;
                builder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.UpdateCommand.CommandText = "UPDATE [dbo].[dup_user] SET  [US_ID] = @p2, [US_PASSWORD] = @p3, [US_AGENT] = 1,[US_REG]=1,[US_TURAGENT]=1,[US_PRKEY]=0 WHERE [US_KEY] = @p1";
                adapter.UpdateCommand.Transaction = transaction;
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.InsertCommand.Transaction = transaction;
                adapter.Update(DUP_USER);

                adapter.SelectCommand = new SqlCommand(
                    @"SELECT     CA_ID, CA_CLКеу, CA_DUPUSERKEY, CA_MANAGER, CA_CREATEDATE
                FROM         Lanta_ClientAccess
                    WHERE    CA_ID=1", connection);
                adapter.SelectCommand.Transaction = transaction;
                builder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.UpdateCommand.CommandText = "UPDATE [Lanta_ClientAccess] SET [CA_CLКеу] = @p1, [CA_DUPUSERKEY] = @p2, [CA_MANAGER] = @p3, [CA_CREATEDATE] = @p4 WHERE [CA_ID] = @p5)";
                adapter.UpdateCommand.Transaction = transaction;
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.InsertCommand.Transaction = transaction;
                adapter.Update(Lanta_ClientAccess);

                transaction.Commit();
            }
            finally
            {
                connection.Close();
            }
        }
        private void button_CL_PFKEY_Click(object sender, EventArgs e)
        {
            Vocabulary vc = new Vocabulary("Profession", connection);
            if (clientsDT.Rows.Count > 0)
            {
                DataRow dr = clientsDT.Rows[0];
                if (comboBox_CL_PFKEY.SelectedValue != null)
                {
                    dr["CL_PFKEY"] = comboBox_CL_PFKEY.SelectedValue;
                }
            }
            if (vc.ShowDialog() == DialogResult.OK)
            {
                RefreshProfession();
            }
        }

        private void checkBox1_CL_SEX_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1_CL_SEX.Checked && checkBox2_CL_SEX.Checked)
                checkBox2_CL_SEX.Checked = false;
            CaseInfantOrChild();

        }

        private void checkBox2_CL_SEX_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1_CL_SEX.Checked && checkBox2_CL_SEX.Checked)
                checkBox1_CL_SEX.Checked = false;
            CaseInfantOrChild();
        }
        private void CaseInfantOrChild()
        {
            label_CL_PASPRUSER.Text = label_CL_PASPRUSER.Text.TrimEnd('*');
            label_CL_PASPRUNUM.Text = label_CL_PASPRUNUM.Text.TrimEnd('*');
            label_CL_PASPRUBYWHOM.Text = label_CL_PASPRUBYWHOM.Text.TrimEnd('*');
            label_CL_PASPRUDATE.Text = label_CL_PASPRUDATE.Text.TrimEnd('*');
            label_CL_PHONE.Text = label_CL_PHONE.Text.TrimEnd('*');
            label_cl_mail.Text = label_cl_mail.Text.TrimEnd('*');

            label_q1.Text = label_q1.Text.TrimEnd('*');
            label_2.Text = label_2.Text.TrimEnd('*');
            label_3.Text = label_3.Text.TrimEnd('*');
            label_4.Text = label_4.Text.TrimEnd('*');
            label_5.Text = label_5.Text.TrimEnd('*');

            if (!checkBox1_CL_SEX.Checked && !checkBox2_CL_SEX.Checked)//Поля обязательные только если взрослый
            {
                label_CL_PASPRUSER.Text = label_CL_PASPRUSER.Text + "*";
                label_CL_PASPRUNUM.Text = label_CL_PASPRUNUM.Text + "*";
                label_CL_PASPRUBYWHOM.Text = label_CL_PASPRUBYWHOM.Text + "*";
                label_CL_PASPRUDATE.Text = label_CL_PASPRUDATE.Text + "*";
                label_CL_PHONE.Text = label_CL_PHONE.Text + "*";
                label_cl_mail.Text = label_cl_mail.Text + "*";

                label_q1.Text = label_q1.Text + "*";
                label_2.Text = label_2.Text + "*";
                label_3.Text = label_3.Text + "*";
                label_4.Text = label_4.Text + "*";
                label_5.Text = label_5.Text + "*";
            }

        }
        private void EditClient_Load(object sender, EventArgs e)
        {
            checkBox_Family.Visible = templateClient > -1;
            if (checkBox_Family.Visible)
            {
                if (tabControl1.TabPages.ContainsKey("tabPage7"))
                    tabControl1.TabPages.RemoveByKey("tabPage7");

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("select * from clients where CL_KEY=" + Convert.ToString(templateClient), connection);
                adapter.Fill(templateClientDT);
                if (templateClientDT.Rows.Count > 0)
                {
                    checkBox_Family.Text = "Член семьи \"" + Convert.ToString(templateClientDT.Rows[0]["CL_NAMERUS"]) + "\"";
                }
            }
        }

        private void checkBox_Family_CheckedChanged(object sender, EventArgs e)
        {
            if (templateClientDT.Rows.Count > 0 && checkBox_Family.Checked)
            {
                DataRow dr = templateClientDT.Rows[0];
                textBox_CL_POSTINDEX.Text = Convert.ToString(dr["CL_POSTINDEX"]);
                textBox_CL_POSTCITY.Text = Convert.ToString(dr["CL_POSTCITY"]);
                textBox_CL_POSTSTREET.Text = Convert.ToString(dr["CL_POSTSTREET"]);
                textBox_CL_POSTBILD.Text = Convert.ToString(dr["CL_POSTBILD"]);
                textBox_CL_POSTFLAT.Text = Convert.ToString(dr["CL_POSTFLAT"]);
                textBox_CL_PHONE.Text = Convert.ToString(dr["CL_PHONE"]);
            }
            else
            {
                textBox_CL_POSTINDEX.Text = "";
                textBox_CL_POSTCITY.Text = "";
                textBox_CL_POSTSTREET.Text = "";
                textBox_CL_POSTBILD.Text = "";
                textBox_CL_POSTFLAT.Text = "";
                textBox_CL_PHONE.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {//Выбор Семьи
            ClientsMainForm cl = new ClientsMainForm("", MANAGER_ID, connection, false);//textBox_CL_NAMERUS.Text
            cl.SetButtonSelectText("Выбрать члена семьи", "Выбор члена семьи в постоянных клиентах");
            if (cl.ShowDialog() == DialogResult.OK)
            {
                templateClient = cl.return_CL_KEY;
                SaveFamilyInfo();
            }

            GetFamilyInfo();
        }

        private void button6_Click(object sender, EventArgs e)
        {//Обновить
            GetFamilyInfo();
        }

        private void SaveFamilyInfo()
        {
            //Сохранение параметров семьи
            DataRow dr;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select * from Lanta_ClientFamily where CF_CLKey=" + Convert.ToString(CL_KEY), connection);
            builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.UpdateCommand.CommandText = "UPDATE [Lanta_ClientFamily] SET [CF_Community] = @p1, [CF_CLKey] = @p2 WHERE [CF_ID] = @p3";
            //adapter.DeleteCommand = builder.GetDeleteCommand();
            //adapter.DeleteCommand.CommandText = "DELETE FROM [Lanta_ClientFamily] WHERE [CF_Community] = @p2"; 
            adapter.InsertCommand = builder.GetInsertCommand();

            //Если у редактируемого клиента нет семьи
            if (Lanta_ClientFamily.Rows.Count == 0)
            {
                //Смотрим есть ли семья у добавляемого клиента 
                Lanta_ClientFamily = FillFamilyInfo(templateClient);
                //Если есть - добавляем текущего клиента в семью добавляемого
                if (Lanta_ClientFamily.Rows.Count > 0)
                {
                    DataRow[] drs = Lanta_ClientFamily.Select("CF_CLKey=" + Convert.ToString(CL_KEY));
                    if (drs.Length == 0)
                    {
                        dr = Lanta_ClientFamily.NewRow();
                        dr["CF_Community"] = Lanta_ClientFamily.Rows[0]["CF_Community"];
                        dr["CF_CLKey"] = CL_KEY;
                        dr["CF_MANAGER"] = MANAGER_ID;
                        dr["CF_CREATEDATE"] = DateTime.Now;
                        Lanta_ClientFamily.Rows.Add(dr);
                    }
                }
            }
            //Если у редактируемого клиента есть семья
            if (Lanta_ClientFamily.Rows.Count > 0)
            {
                //Смотрим есть ли семья у добавляемого клиента
                DataTable addFamily = FillFamilyInfo(templateClient);
                if (addFamily.Rows.Count > 0)
                {//Добавляем клиента и всю его семью если у клиента есть семья
                    foreach (DataRow tr in addFamily.Rows)
                    {
                        DataRow[] drs = Lanta_ClientFamily.Select("CF_CLKey=" + Convert.ToString(tr["CF_CLKey"]));
                        //Добавляется если нет ещё в основную семью
                        if (drs.Length == 0)
                        {
                            dr = Lanta_ClientFamily.NewRow();
                            dr["CF_Community"] = Lanta_ClientFamily.Rows[0]["CF_Community"];
                            dr["CF_CLKey"] = tr["CF_CLKey"];
                            dr["CF_MANAGER"] = MANAGER_ID;
                            dr["CF_CREATEDATE"] = DateTime.Now;
                            Lanta_ClientFamily.Rows.Add(dr);
                        }
                    }
                    if (Convert.ToString(addFamily.Rows[0]["CF_Community"]) != Convert.ToString(Lanta_ClientFamily.Rows[0]["CF_Community"]))
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM [Lanta_ClientFamily] WHERE [CF_Community] = " + Convert.ToString(addFamily.Rows[0]["CF_Community"]), connection);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                else
                {//Если у добавляемого клиента нет семьи - добавляем его одного
                    if (templateClient > -1)//Если он есть
                    {
                        dr = Lanta_ClientFamily.NewRow();
                        dr["CF_Community"] = Lanta_ClientFamily.Rows[0]["CF_Community"];
                        dr["CF_CLKey"] = templateClient;
                        dr["CF_MANAGER"] = MANAGER_ID;
                        dr["CF_CREATEDATE"] = DateTime.Now;
                        Lanta_ClientFamily.Rows.Add(dr);
                    }
                }
            }
            else//Если у обоих нет семьи
            {
                if (templateClient > -1)//Если он есть
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MAX(CF_Community) FROM Lanta_ClientFamily", connection);
                    object res = cmd.ExecuteScalar();
                    int familyID = 1;
                    if (res != System.DBNull.Value)
                        familyID = Convert.ToInt32(res) + 1;

                    connection.Close();

                    dr = Lanta_ClientFamily.NewRow();
                    dr["CF_Community"] = familyID;
                    dr["CF_CLKey"] = CL_KEY;
                    dr["CF_MANAGER"] = MANAGER_ID;
                    dr["CF_CREATEDATE"] = DateTime.Now;
                    Lanta_ClientFamily.Rows.Add(dr);

                    if (templateClient != CL_KEY)
                    {
                        dr = Lanta_ClientFamily.NewRow();
                        dr["CF_Community"] = familyID;
                        dr["CF_CLKey"] = templateClient;
                        dr["CF_MANAGER"] = MANAGER_ID;
                        dr["CF_CREATEDATE"] = DateTime.Now;
                        Lanta_ClientFamily.Rows.Add(dr);
                    }
                }
            }

            adapter.Update(Lanta_ClientFamily);

        }

        private void button5_Click(object sender, EventArgs e)
        { //Удаление

            if (dataGridView_Fam.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView_Fam.SelectedRows[0];
                long rowid = Convert.ToInt64(row.Cells["CF_ID"].Value);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [Lanta_ClientFamily] WHERE [CF_ID] = " + Convert.ToString(rowid), connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            else
                MessageBox.Show("Выберите, пожалуйста, клиента для удаления");


            GetFamilyInfo();
        }

        private void button7_Click(object sender, EventArgs e)
        {//Добавление нового клиента на основе текущего
            EditClient cl = null;
            if (dataGridView_Fam.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView_Fam.SelectedRows[0];
                long CL_KEY = Convert.ToInt64(row.Cells["CF_CLKey"].Value);
                cl = new EditClient(-1, MANAGER_ID, connection,false,false);
                cl.templateClient = CL_KEY;
            }
            else
            {
                cl = new EditClient(-1, MANAGER_ID, connection,false,false);
                cl.templateClient = CL_KEY;
            }
            if (cl != null)
            {
                if (cl.ShowDialog() == DialogResult.OK)
                {
                    GetFamilyInfo();
                    //templateClient = cl.CL_KEY;
                    //SaveFamilyInfo();
                }
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            UpdateStatistic();
            SaveClient(false);
        }
        
        public void UpdateStatisticPassanger(DataTable stat, DataTable tbl_DogovorList)
        {
            int PassangerKey;
            //Получение списка авиабилетов по клиенту 
            SqlDataAdapter adapterServ = new SqlDataAdapter("", connection);
             //DataTable Lanta_ClientStatDogovor = new DataTable("Lanta_ClientStatDogovor");
            Lanta_ClientPassangerLink.Clear();
            adapter.SelectCommand.CommandText = @"SELECT     Lanta_ClientPassangerLink.CPL_ClientКеу, Lanta_ClientPassangerLink.CPL_PassangerКеу
                    FROM         LINKS_DG_TIC_LANTA RIGHT OUTER JOIN
                      Lanta_ClientPassangerLink ON LINKS_DG_TIC_LANTA.TIC_ID = Lanta_ClientPassangerLink.CPL_PassangerКеу
                    WHERE     (LINKS_DG_TIC_LANTA.DG_CODE IS NULL) AND Lanta_ClientPassangerLink.CPL_ClientКеу = " + CL_KEY.ToString();
            adapter.Fill(Lanta_ClientPassangerLink);
            foreach (DataRow drPass in Lanta_ClientPassangerLink.Rows)
            {
                PassangerKey = Convert.ToInt32(drPass["CPL_PassangerКеу"]);
                //Создание записи о билете в статистике.
               
                try
                {
                    DataRow dr = stat.NewRow();
                    dr["CSD_CLКеу"] = CL_KEY;
                    dr["CSD_DGKEY"] = PassangerKey;
                    dr["CSD_DGNMEN"] = 1;
                   
                    dr["CSD_DodovorType"] = 1;


                    dr["CSD_DGCODE"] = "AVIATICKET";
                    dr["CSD_DGTURDATE"] = new DateTime(1899, 1, 1);
                    dr["CSD_DGCRDATE"] = new DateTime(1899, 1, 1);
                    dr["CSD_DGRATE"] = "";
                    dr["CSD_RCCOURSE$"] = 1.0;

                    
                    //TICMAIN.State='S'-Sale,'V'-void, 'R'-возврат
                    DataTable Serv = new DataTable("Serv");
                    adapterServ.SelectCommand.CommandText = @"SELECT     TICMAIN.State,TICMAIN.NUMTICKET, TICMAIN.CURR, TICSR.TOTAL_D,
                    TICSR.RUBTOTAL_D, TICBASIC.DepDate, TICSR.DATEIN, CityDictionary.CT_CNKEY,TICBASIC.TO_AIRPCODE
                    FROM         Airport INNER JOIN
                      CityDictionary ON Airport.AP_CTKEY = CityDictionary.CT_KEY RIGHT OUTER JOIN
                      avia.avia.dbo.TICBASIC AS TICBASIC ON Airport.AP_CODE = TICBASIC.TO_AIRPCODE RIGHT OUTER JOIN
                      avia.avia.dbo.TICMAIN AS TICMAIN LEFT OUTER JOIN
                      avia.avia.dbo.TICSR AS TICSR ON TICMAIN.ID = TICSR.ID_OWN ON TICBASIC.ID_OWN = TICMAIN.ID AND TICBASIC.NUM = 1 AND TICBASIC.DepDate<GetDate()
             WHERE    TICMAIN.ID =" + PassangerKey.ToString();


                    adapterServ.Fill(Serv);
                    DataRow servdr;
                    string NUMTICKET="";
                    if (Serv.Rows.Count > 0)
                    {
                        servdr = Serv.Rows[0];
                        switch (Convert.ToString(servdr["State"]))
                        {
                            case "V":
                                dr["CSD_DGCOMMENT"] = "Aннулирована";
                                break;
                            case "R":
                                dr["CSD_DGCOMMENT"] = "Возврат";
                                break;
                            default:
                                dr["CSD_DGCOMMENT"] = servdr["State"];
                                break;
                        
                        }
                        NUMTICKET = Convert.ToString(servdr["NUMTICKET"]);
                        dr["CSD_DGCODE"] = NUMTICKET;
                        if (servdr["DepDate"] != System.DBNull.Value)
                            dr["CSD_DGTURDATE"] = servdr["DepDate"];
                        DateTime DGCRDATE = Convert.ToDateTime(servdr["DATEIN"]);
                        dr["CSD_DGCRDATE"] = DGCRDATE;
                        dr["CSD_DGRATE"] = servdr["CURR"];
                        if (servdr["CT_CNKEY"] != System.DBNull.Value)//
                            dr["CSD_DGCNKEY"] = servdr["CT_CNKEY"];
                        else
                        {
                            if (servdr["TO_AIRPCODE"] != System.DBNull.Value)
                            {
                                //если аэропорт не найден в списке аэропортов в базе путёвок, то страна ищется в базе авиабилетов по имени
                                DataTable cnserv = new DataTable("cnserv");
                                adapterServ.SelectCommand.CommandText = @"SELECT     AirPortAll_1.CodeAll, AirPortAll_1.Ctry_Code, AirPortAll_1.Ctry_Name, tbl_Country.CN_KEY
                                FROM         avia.avia.dbo.AirPortAll AS AirPortAll_1 INNER JOIN
                                                      tbl_Country ON AirPortAll_1.Ctry_Name = UPPER(tbl_Country.CN_NAMELAT)
                                WHERE     (AirPortAll_1.CodeAll = '" + Convert.ToString(servdr["TO_AIRPCODE"]) + "')";
                                adapterServ.Fill(cnserv);
                                if (cnserv.Rows.Count > 0)
                                {
                                    dr["CSD_DGCNKEY"] = cnserv.Rows[0]["CN_KEY"];
                                }

                            }
                        }

                        DataTable curs = new DataTable("curs");
                        string crdate = DGCRDATE.Year.ToString() + "-" + DGCRDATE.Month.ToString().PadLeft(2, '0') + "-" + DGCRDATE.Day.ToString().PadLeft(2, '0');
                        if (Convert.ToString(dr["CSD_DGRATE"]) == "RUB")
                        {
                            dr["CSD_DGPRICE"] = Convert.ToDouble(servdr["RUBTOTAL_D"]);
                            adapterServ.SelectCommand.CommandText = @"SELECT      RC_COURSE
                    FROM         RealCourses
                    WHERE       (RC_RCOD1 = 'рб') AND (RC_RCOD2 = '$') AND (RC_DATEBEG = CONVERT(DATETIME, '" + crdate + "', 102))";
                        }
                        else
                        {
                            dr["CSD_DGPRICE"] = Convert.ToDouble(servdr["TOTAL_D"]);
                            adapterServ.SelectCommand.CommandText = @"SELECT      RC_COURSE
                    FROM         RealCourses
                    WHERE       (RC_RCOD1 = '$') AND (RC_RCOD2 = 'Eu') AND (RC_DATEBEG = CONVERT(DATETIME, '" + crdate + "', 102))";

                        }
                        if (dr["CSD_DGPRICE"] == System.DBNull.Value)
                            dr["CSD_DGPRICE"] = 0.0;

                        adapterServ.Fill(curs);
                        if (curs.Rows.Count > 0)
                        {
                            if (Convert.ToString(dr["CSD_DGRATE"]) == "RUB")
                            {
                                dr["CSD_RCCOURSE$"] = 1 / Convert.ToDouble(curs.Rows[0]["RC_COURSE"]);
                            }
                            else
                            {
                                dr["CSD_RCCOURSE$"] = Convert.ToDouble(curs.Rows[0]["RC_COURSE"]);
                            }
                        }
                    }
                    CalculateStatisticPrice(dr);
                    stat.Rows.Add(dr);
                    //Информация о перелётах
                    
                    Serv.Clear();
                    adapterServ.SelectCommand.CommandText = @"SELECT     TICBASIC.ID_OWN, TICBASIC.NUM, TICBASIC.FROM_AIRPCODE AS Airport_FROM, TICBASIC.TO_AIRPCODE AS Airport_TO, TICBASIC.CARRIER, 
                      TICBASIC.FLIGHT, TICBASIC.TimeTo, TICBASIC.ArrDate, TICBASIC.CLASS, AFROM.City_NameR AS City_From, ATO.City_NameR AS City_To, 
                      AFROM.City_Name AS City_FromENG, ATO.City_Name AS City_ToENG
                        FROM         avia.avia.dbo.TICBASIC AS TICBASIC LEFT OUTER JOIN
                      avia.avia.dbo.AirPortAll AS AFROM ON TICBASIC.FROM_AIRPCODE = AFROM.CodeAll LEFT OUTER JOIN
                      avia.avia.dbo.AirPortAll AS ATO ON TICBASIC.TO_AIRPCODE = ATO.CodeAll
                        WHERE     TICBASIC.ID_OWN = " + PassangerKey.ToString() +
                      " AND TICBASIC.FROM_AIRPCODE is not null AND  TICBASIC.FROM_AIRPCODE <>'' ORDER BY TICBASIC.NUM";
                    adapterServ.Fill(Serv);
                    foreach (DataRow drFly in Serv.Rows)
                    {
                        //Вставить заполнение описания перелёта
                        dr = tbl_DogovorList.NewRow();
                        // CSL_DLSVKEY,CSL_DLNAME,

                        //dr["CSL_CLKEY"] = CL_KEY;
                        dr["CSD_DGCODE"] = NUMTICKET;
                        dr["CSL_DGKEY"] = PassangerKey;
                        dr["CSL_DLKEY"] = drFly["NUM"];
                        dr["CSL_DLSVKEY"] = -1;
                        string TimeTo = "";
                        if (drFly["TimeTo"] != System.DBNull.Value)
                        {
                            TimeTo = Convert.ToString(drFly["TimeTo"]);
                            if (TimeTo.Length == 4)
                                TimeTo = TimeTo.Insert(2, ":");
                        }
                        string ArrDate = "";
                        if (drFly["ArrDate"] != System.DBNull.Value)
                        {
                            ArrDate = Convert.ToDateTime(drFly["ArrDate"]).ToString("hh:mm");
                        }
                        string City_From = "";
                        if (drFly["City_From"] != System.DBNull.Value)
                            City_From = Convert.ToString(drFly["City_From"]).TrimEnd(' ');
                        else
                            City_From = Convert.ToString(drFly["City_FromENG"]).TrimEnd(' ');
                        
                         string City_To = "";
                        if (drFly["City_To"] != System.DBNull.Value)
                            City_To = Convert.ToString(drFly["City_To"]).TrimEnd(' ');
                        else
                            City_To = Convert.ToString(drFly["City_ToENG"]).TrimEnd(' ');                       
                        
                        dr["CSL_DLNAME"] = "А_П::" + City_From + "/"+ City_To + "/" +
                            drFly["CARRIER"] + drFly["FLIGHT"] + "," +
                            drFly["Airport_FROM"] + "-" + drFly["Airport_TO"] + "," +
                             TimeTo + "-" + ArrDate + "/" +
                            drFly["CLASS"]// + " " + drFly["AS_NAMERUS"]
                            ;

                        tbl_DogovorList.Rows.Add(dr);

                    }
                     

                }
                catch (Exception cex)
                {
                    cex.ToString();
                }
            }
        }
        public void CalculateStatisticAgregates(DataTable stat)
        {
            if (stat.Rows.Count > 0)
            {
                textBox_CL_LASTSTAT.Text = DateTime.Now.ToString();
                string filter = "(CSD_DodovorType=0 AND CSD_DGTURDATE>#12/30/1899#) OR (CSD_DodovorType=1 AND CSD_DGCOMMENT='S')";
                //textBox_CL_NTRIP.Text = stat.Rows.Count.ToString();
                object dg = stat.Compute("Count(CSD_DGCODE)", filter);
                int numValidDogovor = 0;
                if (dg != System.DBNull.Value)
                {
                    numValidDogovor = Convert.ToInt32(dg);
                    textBox_CL_NTRIP.Text = Convert.ToString(numValidDogovor);
                }
                else
                    textBox_CL_NTRIP.Text = "";

                dg = stat.Compute("Sum(CSD_DGNMEN)", filter);
                if (dg != System.DBNull.Value)
                    textBox_CL_NMENWITH.Text = Convert.ToString(Convert.ToInt32(dg) - numValidDogovor);
                else
                    textBox_CL_NMENWITH.Text = "";

                dg = stat.Compute("Min(DG_PRICE$)", filter);
                if (dg != System.DBNull.Value)
                    textBox_CL_MINCOST.Text = (Convert.ToDouble(dg)).ToString("f2");
                else
                    textBox_CL_MINCOST.Text = "";

                dg = stat.Compute("Max(DG_PRICE$)", filter);
                if (dg != System.DBNull.Value)
                    textBox_CL_MAXCOST.Text = (Convert.ToDouble(dg)).ToString("f2");
                else
                    textBox_CL_MAXCOST.Text = "";

                dg = stat.Compute("Sum(DG_PRICE$)", filter);
                if (dg != System.DBNull.Value)
                    textBox_CL_SUMDOGOVOR.Text = (Convert.ToDouble(dg)).ToString("f2");
                else
                    textBox_CL_SUMDOGOVOR.Text = "";

                dg = stat.Compute("Sum(PerMan$)", filter);
                if (dg != System.DBNull.Value)
                    textBox_CL_SUMMA.Text = (Convert.ToDouble(dg)).ToString("f2");
                else
                    textBox_CL_SUMMA.Text = "";
            }
        }
        public void UpdateStatistic()
        {
            /*Получение договоров по клиенту
            SELECT     tbl_Dogovor.DG_CODE, tbl_Dogovor.DG_TURDATE
            FROM         tbl_Dogovor RIGHT OUTER JOIN
                      tbl_Turist LEFT OUTER JOIN
                      Clients ON tbl_Turist.TU_ID = Clients.CL_KEY ON tbl_Dogovor.DG_Key = tbl_Turist.TU_DGKEY
            WHERE     (Clients.CL_KEY = 15543)*/
            //string dbNameNative = connection.Database;
           // string[] ConnectionStringNative = connection.ConnectionString.Split(';');
            SqlConnection connSys = connection;//new SqlConnection(String.Format("{2};{3};User ID={0}; pwd={1}; Timeout=30", "sysadm", "ieheg", ConnectionStringNative[0],ConnectionStringNative[1])); ;
            try
            {
                if (CL_KEY > -1)
                {
                    stat.Clear();
                    DataTable StatClone = stat.Clone();
                    DataRow[] drs;
                    //Статистика и вкладка Путёвки

                    if (connSys.State != ConnectionState.Open)
                        connSys.Open();
                    
                        StatClone.Clear();
                        adapter.SelectCommand = new SqlCommand(@"SELECT     tbl_Turist.TU_ID AS CSD_CLКеу, tbl_Turist.TU_DGCOD AS CSD_DGCODE,
                        tbl_Turist.TU_TURDATE AS CSD_DGTURDATE, tbl_Dogovor.DG_NMEN as CSD_DGNMEN, 
                      tbl_Dogovor.DG_PRICE as CSD_DGPRICE, tbl_Dogovor.DG_RATE as CSD_DGRATE,
                    tbl_Dogovor.DG_PRICE /
                     (case WHEN tbl_Dogovor.DG_NMEN=0 then 1 else ISNULL(tbl_Dogovor.DG_NMEN,1) end) AS PerMan,
                      tbl_Dogovor.DG_CRDATE as CSD_DGCRDATE, 
                      CASE WHEN RealCourses.RC_RCOD1 = '$' THEN ISNULL(RealCourses.RC_COURSE, 1) ELSE 1 / ISNULL(RealCourses.RC_COURSE, 1) 
                      END AS CSD_RCCOURSE$, CASE WHEN RealCourses.RC_RCOD1 = '$' THEN tbl_Dogovor.DG_PRICE * ISNULL(RealCourses.RC_COURSE, 1) 
                      ELSE tbl_Dogovor.DG_PRICE / ISNULL(RealCourses.RC_COURSE, 1) END AS DG_PRICE$, 
                      CASE WHEN RealCourses.RC_RCOD1 = '$' THEN tbl_Dogovor.DG_PRICE * ISNULL(RealCourses.RC_COURSE, 1) 
                      ELSE tbl_Dogovor.DG_PRICE / ISNULL(RealCourses.RC_COURSE, 1) END /
                       (case WHEN tbl_Dogovor.DG_NMEN=0 then 1 else ISNULL(tbl_Dogovor.DG_NMEN,1) end) AS PerMan$, 
                       tbl_Dogovor.DG_Key as CSD_DGKEY, 
                      tbl_Dogovor.DG_CNKEY as CSD_DGCNKEY, tbl_Country.CN_NAME, 0 as CSD_DodovorType
FROM         tbl_Country INNER JOIN
                      tbl_Dogovor ON tbl_Country.CN_KEY = tbl_Dogovor.DG_CNKEY RIGHT OUTER JOIN
                      tbl_Turist ON tbl_Dogovor.DG_Key = tbl_Turist.TU_DGKEY LEFT OUTER JOIN
                      RealCourses ON tbl_Dogovor.DG_RATE <> '$' AND (RealCourses.RC_RCOD1 = '$' AND tbl_Dogovor.DG_RATE = RealCourses.RC_RCOD2 OR
                      RealCourses.RC_RCOD2 = '$' AND tbl_Dogovor.DG_RATE = RealCourses.RC_RCOD1) AND ABS(DATEDIFF(day, RealCourses.RC_DATEBEG, 
                      tbl_Dogovor.DG_CRDATE)) < 1
                WHERE     (tbl_Turist.TU_ID=  " + Convert.ToString(CL_KEY) + @") AND tbl_Dogovor.DG_TURDATE<GetDate()
                    union SELECT     Clients.CL_KEY AS CSD_CLКеу, tbl_Dogovor.DG_CODE AS CSD_DGCODE,tbl_Dogovor.DG_TURDATE AS CSD_DGTURDATE, tbl_Dogovor.DG_NMEN as CSD_DGNMEN, 
           tbl_Dogovor.DG_PRICE as CSD_DGPRICE, tbl_Dogovor.DG_RATE as CSD_DGRATE,tbl_Dogovor.DG_PRICE / (case WHEN tbl_Dogovor.DG_NMEN=0 then 1 else ISNULL(tbl_Dogovor.DG_NMEN,1) end) AS PerMan,
           tbl_Dogovor.DG_CRDATE as CSD_DGCRDATE,CASE WHEN RealCourses.RC_RCOD1 = '$' THEN ISNULL(RealCourses.RC_COURSE, 1) ELSE 1 / ISNULL(RealCourses.RC_COURSE, 1) 
           END AS CSD_RCCOURSE$, CASE WHEN RealCourses.RC_RCOD1 = '$' THEN tbl_Dogovor.DG_PRICE * ISNULL(RealCourses.RC_COURSE, 1) 
           ELSE tbl_Dogovor.DG_PRICE / ISNULL(RealCourses.RC_COURSE, 1) END AS DG_PRICE$, 
           CASE WHEN RealCourses.RC_RCOD1 = '$' THEN tbl_Dogovor.DG_PRICE * ISNULL(RealCourses.RC_COURSE, 1) 
           ELSE tbl_Dogovor.DG_PRICE / ISNULL(RealCourses.RC_COURSE, 1) END /
           (case WHEN tbl_Dogovor.DG_NMEN=0 then 1 else ISNULL(tbl_Dogovor.DG_NMEN,1) end) AS PerMan$, 
           tbl_Dogovor.DG_Key as CSD_DGKEY,tbl_Dogovor.DG_CNKEY as CSD_DGCNKEY, tbl_Country.CN_NAME, 0 as CSD_DodovorType
FROM       tbl_Country 
INNER JOIN tbl_Dogovor ON tbl_Country.CN_KEY = tbl_Dogovor.DG_CNKEY 
INNER JOIN Lanta_DogovorDeputat ON DD_CLКеу = " + Convert.ToString(CL_KEY) + @"
RIGHT OUTER JOIN Clients ON " + Convert.ToString(CL_KEY) + @" = Clients.CL_KEY AND tbl_Dogovor.DG_Key = Lanta_DogovorDeputat.DD_DGКеу
LEFT OUTER JOIN  RealCourses ON tbl_Dogovor.DG_RATE <> '$' AND (RealCourses.RC_RCOD1 = '$' AND tbl_Dogovor.DG_RATE = RealCourses.RC_RCOD2 OR
                 RealCourses.RC_RCOD2 = '$' AND tbl_Dogovor.DG_RATE = RealCourses.RC_RCOD1) AND ABS(DATEDIFF(day, RealCourses.RC_DATEBEG, 
                 tbl_Dogovor.DG_CRDATE)) < 1
WHERE tbl_Dogovor.DG_TURDATE<GetDate()
ORDER BY CSD_DGTURDATE", connSys);

                        //connSys.ChangeDatabase(dbName);
                        //if (dbName == "lanta05" || dbName == "lanta04" || dbName == "lanta03")
                        //    adapter.SelectCommand.CommandText = adapter.SelectCommand.CommandText
                        //        .Replace("ON tbl_Dogovor.DG_Key = tbl_Turist.TU_DGKEY", "ON tbl_Dogovor.DG_CODE  = tbl_Turist.TU_DGCOD")
                        //        .Replace("DG_RATE <>", "DG_RATE collate Cyrillic_General_CS_AS  <>")
                        //        .Replace("DG_RATE =", "DG_RATE collate Cyrillic_General_CS_AS  =")
                        //        //.Replace("tbl_Dogovor.DG_Key", "NULL AS DG_Key")

                        //        ;
                       // adapter.Fill(StatClone);
                        adapter.Fill(stat);
                        //foreach (DataRow dr in StatClone.Rows)
                        //{
                        //    drs = stat.Select("CSD_DGCODE = '" + Convert.ToString(dr["CSD_DGCODE"]) + "'");
                        //    if (drs.Length == 0)
                        //        stat.Rows.Add(dr.ItemArray);
                        //}
                    

                    //Вкладка спутники
                    sput.Clear();
                    DataTable sputClone = sput.Clone();
                   
                        sputClone.Clear();
                        adapter.SelectCommand = new SqlCommand(@"SELECT     tbl_Dogovor.DG_CODE as CSD_DGCODE, tbl_Dogovor.DG_TURDATE as CSD_DGTURDATE,
                    tbl_Turist_1.TU_NAMERUS, tbl_Turist_1.TU_FNAMERUS, tbl_Turist_1.TU_SNAMERUS, 
                      tbl_Turist_1.TU_BIRTHDAY, tbl_Turist_1.TU_ID as CSS_CLКеу, tbl_Dogovor.DG_Key as CSS_DGKEY
                FROM         tbl_Turist LEFT OUTER JOIN
                      tbl_Turist AS tbl_Turist_1 INNER JOIN
                      tbl_Dogovor ON tbl_Turist_1.TU_DGKEY = tbl_Dogovor.DG_Key ON tbl_Turist.TU_DGKEY = tbl_Dogovor.DG_Key
                WHERE     (tbl_Turist.TU_ID =   " + Convert.ToString(CL_KEY) + @")", connSys);

                        //connSys.ChangeDatabase(dbName);
                        //if (dbName == "lanta05" || dbName == "lanta04" || dbName == "lanta03")
                        //    adapter.SelectCommand.CommandText = adapter.SelectCommand.CommandText
                        //        .Replace("ON tbl_Turist.TU_DGKEY = tbl_Dogovor.DG_Key", "ON tbl_Turist.TU_DGCOD  = tbl_Dogovor.DG_CODE")
                        //        .Replace("ON tbl_Turist_1.TU_DGKEY = tbl_Dogovor.DG_Key", "ON tbl_Turist_1.TU_DGCOD  = tbl_Dogovor.DG_CODE");

                        adapter.Fill(sputClone);
                        //adapter.Fill(sput);
                        foreach (DataRow dr in sputClone.Rows)
                        {
                            if (dr["CSS_CLКеу"] != System.DBNull.Value)
                            {
                                drs = sput.Select("CSD_DGCODE = '" + Convert.ToString(dr["CSD_DGCODE"]) + "' AND CSS_CLКеу = " + Convert.ToString(dr["CSS_CLКеу"]));
                                if (drs.Length == 0)
                                    sput.Rows.Add(dr.ItemArray);
                            }
                        }
                    
                    ExcludeOwn();

                    //Вкладки сервисов путёвки tbl_DogovorList.DL_SVKEY = 1 AND
                    tbl_DogovorList.Clear();
                    DataTable tbl_DogovorListClone = tbl_DogovorList.Clone();
                    
                        tbl_DogovorListClone.Clear();
                        adapter.SelectCommand = new SqlCommand(@"SELECT     tbl_DogovorList.DL_DGCOD as CSD_DGCODE, tbl_DogovorList.DL_SVKEY as CSL_DLSVKEY,
                        tbl_DogovorList.DL_NAME as CSL_DLNAME, tbl_DogovorList.DL_DGKEY as CSL_DGKEY, tbl_DogovorList.DL_KEY as CSL_DLKEY
                            FROM         tbl_Turist INNER JOIN
                              tbl_DogovorList INNER JOIN TuristService ON tbl_DogovorList.DL_KEY = TuristService.TU_DLKEY ON tbl_Turist.TU_KEY = TuristService.TU_TUKEY
                            WHERE     tbl_Turist.TU_ID = " + Convert.ToString(CL_KEY) + @" AND (tbl_DogovorList.DL_SVKEY = 1 OR
                              tbl_DogovorList.DL_SVKEY = 3)", connSys);
                        //connSys.ChangeDatabase(dbName);
                        //if (dbName == "lanta05" || dbName == "lanta04" || dbName == "lanta03")
                        //    adapter.SelectCommand.CommandText = adapter.SelectCommand.CommandText
                        //        .Replace("ON tbl_DogovorList.DL_DGCOD = TuristService.TU_DLKEY", "ON tbl_Turist.TU_DGCOD  = tbl_Dogovor.DG_CODE")
                        //        .Replace(" tbl_DogovorList.DL_DGKEY as CSL_DGKEY,", " tbl_Dogovor.DG_Key as CSL_DGKEY,")
                        //        .Replace("WHERE", "INNER JOIN tbl_Dogovor ON tbl_Turist.TU_DGCOD = tbl_Dogovor.DG_CODE WHERE")
                        //        ;

                        //adapter.Fill(tbl_DogovorListClone);
                        adapter.Fill(tbl_DogovorList);
                        //foreach (DataRow dr in tbl_DogovorListClone.Rows)
                        //{
                        //    drs = tbl_DogovorList.Select("CSD_DGCODE = '" + Convert.ToString(dr["CSD_DGCODE"]) + "' AND CSL_DLKEY = " + Convert.ToString(dr["CSL_DLKEY"]));
                        //    if (drs.Length == 0)
                        //        tbl_DogovorList.Rows.Add(dr.ItemArray);
                        //}
                    
                   
                    //Возвращение адаптера в исходное подключение
                    adapter = new SqlDataAdapter("", connection);
                    //Добавление в общий список авиабилеты без договоров
                    UpdateStatisticPassanger(stat, tbl_DogovorList);
                    // 

                    CalculateStatisticAgregates(stat);
                    dataGridView_STAT.DataSource = stat;
                    dataGridView_STAT.Sort(dataGridView_STAT.Columns["DG_TURDATE"], ListSortDirection.Descending);
                   
                    DataTable avia = tbl_DogovorList.Clone();
                    DataRow[] avias = tbl_DogovorList.Select("CSL_DLSVKEY = 1 OR CSL_DLSVKEY = -1");
                    foreach (DataRow dr in avias)
                        avia.Rows.Add(dr.ItemArray);
                    dataGridView_AVIA.DataSource = avia;
                    dataGridView_AVIA.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView_AVIA.Refresh();

                    DataTable HOTEL = tbl_DogovorList.Clone();
                    DataRow[] HOTELs = tbl_DogovorList.Select("CSL_DLSVKEY = 3");
                    foreach (DataRow dr in HOTELs)
                        HOTEL.Rows.Add(dr.ItemArray);
                    dataGridView_HOTEL.DataSource = HOTEL;
                    dataGridView_HOTEL.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView_HOTEL.Refresh();



                }
            }
            finally
            {
                //if (connSys.State != ConnectionState.Closed)
                //    connSys.Close();
            }
        }

        public void SaveStatistic()
        {
            //После обновления статистики необходимо заполнить поля основных таблиц для последующего сохранения
            if (CL_KEY > -1)
            {
                //Таблица договоров по клиенту
                Lanta_ClientStatDogovor.Clear();
                adapter.SelectCommand = new SqlCommand(@"SELECT    * 
                    FROM Lanta_ClientStatDogovor
                    WHERE      Lanta_ClientStatDogovor.CSD_CLКеу = " + Convert.ToString(CL_KEY), connection);
                adapter.Fill(Lanta_ClientStatDogovor);


                DataRow[] drs;
                DataRow dr;
                int DG_KEY;
                string DG_CODE;
                
                foreach (DataRow drf in Lanta_ClientStatDogovor.Rows)//Таблица в  которую будут копироваться данные
                {
                    DG_KEY = Convert.ToInt32(drf["CSD_DGKEY"]);
                    DG_CODE = Convert.ToString(drf["CSD_DGCODE"]);
                    drs = stat.Select("CSD_DGKEY = " + DG_KEY.ToString() + " OR CSD_DGCODE='" + DG_CODE + "'");
                    if (drs.Length == 0)
                    {
                        drf.Delete();//Удаляем лишнюю статистику. Например билеты, которые прицепили к путёвкам.
                        SaveStatServices(DG_KEY, DG_CODE, CL_KEY,true);
                    }
                }
                
                foreach (DataRow drf in stat.Rows)//Таблица из которой будут копироваться данные
                {
                    //Смотрим есть ли уже такая поездка в статистике
                    if (drf["CSD_DGKEY"] != System.DBNull.Value)
                    {
                        DG_KEY = Convert.ToInt32(drf["CSD_DGKEY"]);
                        drs = Lanta_ClientStatDogovor.Select("CSD_DGKEY = " + DG_KEY.ToString());
                        DG_CODE = "";
                    }
                    else if (drf["CSD_DGCODE"] != System.DBNull.Value)
                    {
                        DG_CODE = Convert.ToString(drf["CSD_DGCODE"]);
                        drs = Lanta_ClientStatDogovor.Select("CSD_DGCODE = '" + DG_CODE + "'");
                        //GOA90502A0
                        if (DG_CODE.Length < 10)
                            DG_CODE = DG_CODE.Replace('-', '0').PadLeft(10, '0');
                        string subcode = DG_CODE.Substring(3, 5) + Convert.ToByte(DG_CODE[9]).ToString().PadLeft(3,'0');
                        DG_KEY = -Convert.ToInt32(subcode);
                    }
                    else
                        return;//Неправильный договор

                    if (drs.Length > 0)
                    {
                        dr = drs[0];
                        for (int i = 1; i < drs.Length; i++)
                            drs[i].Delete();//Удаление дублей в статистике по одному и тому же договору

                    }
                    else
                    {
                        dr = Lanta_ClientStatDogovor.NewRow();
                        Lanta_ClientStatDogovor.Rows.Add(dr);
                    }
                    dr["CSD_CLКеу"] = drf["CSD_CLКеу"];
                    if (drf["CSD_DGNMEN"] != System.DBNull.Value)
                        dr["CSD_DGNMEN"] = drf["CSD_DGNMEN"];
                    else
                        dr["CSD_DGNMEN"] = 1;
                    dr["CSD_DGCODE"] = drf["CSD_DGCODE"];
                    dr["CSD_DGTURDATE"] = drf["CSD_DGTURDATE"];
                    if (drf["CSD_DGPRICE"] != System.DBNull.Value)
                        dr["CSD_DGPRICE"] = drf["CSD_DGPRICE"];
                    else
                        dr["CSD_DGPRICE"] = 0;
                    if (drf["CSD_DGRATE"] != System.DBNull.Value)
                        dr["CSD_DGRATE"] = drf["CSD_DGRATE"];
                    else
                        dr["CSD_DGRATE"] = "$";
                    if (drf["CSD_DGCRDATE"] != System.DBNull.Value)
                        dr["CSD_DGCRDATE"] = drf["CSD_DGCRDATE"];
                    else
                        dr["CSD_DGCRDATE"] = new DateTime(1899,1,1);
                    dr["CSD_RCCOURSE$"] = drf["CSD_RCCOURSE$"];
                    //dr["CSD_DGCOMMENT"] = drf["CSD_DGCOMMENT"];
                    dr["CSD_DGCNKEY"] = drf["CSD_DGCNKEY"];
                    DG_KEY = SaveStatSputniki(DG_KEY, DG_CODE);//Если нет кода договора у клиента, то он может оказаться у попутчика!
                    dr["CSD_DGKEY"] = DG_KEY;
                    SaveStatServices(DG_KEY,DG_CODE, CL_KEY,false);
                    dr["CSD_DodovorType"] = drf["CSD_DodovorType"];
                }

                builder = new SqlCommandBuilder(adapter);
                
                adapter.UpdateCommand.CommandText = "UPDATE [Lanta_ClientStatDogovor] SET [CSD_CLКеу] = @p1, [CSD_DGKEY] = @p2, [CSD_DGNMEN] = @p3, [CSD_DGCODE] = @p4, [CSD_DGTURDATE] = @p5, [CSD_DGPRICE] = @p6, [CSD_DGRATE] = @p7, [CSD_DGCRDATE] = @p8, [CSD_RCCOURSE$] = @p9, [CSD_DGCOMMENT] = @p10, [CSD_DGCNKEY] = @p11, [CSD_DodovorType] = @p12 WHERE [CSD_ID] = @p13";
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                adapter.Update(Lanta_ClientStatDogovor);
            }
        }
        public void SaveStatServices(int DG_KEY,string DG_CODE, long CL_KEY,bool DeleteService)
        {
            //Таблица услуг по путёвке. Интересуют авиаперелёты(SVKEY=1) и гостиницы(SVKEY=3)
            SqlDataAdapter adapter = new SqlDataAdapter();
            Lanta_ClientStatDogovorList.Clear();
            adapter.SelectCommand = new SqlCommand(@"SELECT *
                FROM         Lanta_ClientStatDogovorList
                WHERE     CSL_DGKEY = " + Convert.ToString(DG_KEY) +" AND CSL_CLKEY="+Convert.ToString(CL_KEY), connection);
            adapter.Fill(Lanta_ClientStatDogovorList);

            if (DeleteService)
            {
                foreach (DataRow drf in Lanta_ClientStatDogovorList.Rows)
                    drf.Delete();
            }
            else
            {
                DataRow[] drssput;
                if (DG_CODE.Length == 0)
                    drssput = tbl_DogovorList.Select("CSL_DGKEY=" + Convert.ToString(DG_KEY));//Таблица из которой будут копироваться данные
                else
                    drssput = tbl_DogovorList.Select("CSD_DGCODE='" + DG_CODE + "'");//Таблица из которой будут копироваться данные

                DataRow[] drs;
                DataRow dr;

                foreach (DataRow drf in Lanta_ClientStatDogovorList.Rows)
                {//Удаление ненужных сервисов
                    drs = tbl_DogovorList.Select("CSL_DLKEY=" + Convert.ToString(drf["CSL_DLKEY"]) +
                        " AND (CSL_DGKEY=" + Convert.ToString(DG_KEY)+" OR CSD_DGCODE='" + DG_CODE + "')" );
                    if (drs.Length == 0)
                        drf.Delete();
                }
                
                foreach (DataRow drf in drssput)
                {
                    drs = Lanta_ClientStatDogovorList.Select("CSL_DLKEY=" + Convert.ToString(drf["CSL_DLKEY"]));
                    if (drs.Length > 0)
                    {
                        dr = drs[0];
                        for (int i = 1; i < drs.Length; i++)
                            drs[i].Delete();
                    }
                    else
                    {
                        dr = Lanta_ClientStatDogovorList.NewRow();
                        Lanta_ClientStatDogovorList.Rows.Add(dr);
                    }
                    dr["CSL_CLKEY"] = CL_KEY;
                    dr["CSL_DGKEY"] = DG_KEY;
                    dr["CSL_DLKEY"] = drf["CSL_DLKEY"];
                    dr["CSL_DLSVKEY"] = drf["CSL_DLSVKEY"];
                    dr["CSL_DLNAME"] = drf["CSL_DLNAME"];
                }
            }
            builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.UpdateCommand.CommandText = "UPDATE [Lanta_ClientStatDogovorList] SET [CSL_CLKEY] = @p1, [CSL_DGKEY] = @p2, [CSL_DLKEY] = @p3, [CSL_DLSVKEY] = @p4, [CSL_DLNAME] = @p5 WHERE ([CSL_ID] = @p6)";
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.Update(Lanta_ClientStatDogovorList);
        }
        public int SaveStatSputniki(int DG_KEY, string DG_CODE)
        {
            //Таблица спутников в поездках
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataRow[] drssput;
                if (DG_KEY>0)
                    drssput = sput.Select("CSS_DGKEY=" + Convert.ToString(DG_KEY));
                else
                    drssput = sput.Select("CSD_DGCODE='" + DG_CODE + "'");
            DataRow[] drs;
            DataRow dr;
            foreach (DataRow drf in drssput)//Если нет ключа договора - он может оказаться у спутника.
            {
                if (drf["CSS_DGKEY"] != System.DBNull.Value && Convert.ToInt32(drf["CSS_DGKEY"]) != DG_KEY)
                    DG_KEY = Convert.ToInt32(drf["CSS_DGKEY"]);
            }
            Lanta_ClientStatSputniki.Clear();
            adapter.SelectCommand = new SqlCommand(@"SELECT  CSS_ID, CSS_CLКеу, CSS_DGKEY
                FROM         Lanta_ClientStatSputniki
                WHERE     CSS_DGKEY  = " + Convert.ToString(DG_KEY), connection);
            adapter.Fill(Lanta_ClientStatSputniki);
            
            foreach (DataRow drf in drssput)//Таблица из которой будут копироваться данные
            {
                if (drf["CSS_CLКеу"] != System.DBNull.Value)
                {
                    drs = Lanta_ClientStatSputniki.Select("CSS_CLКеу=" + Convert.ToString(drf["CSS_CLКеу"]));
                    if (drs.Length > 0)
                    {
                        dr = drs[0];
                        for (int i = 1; i < drs.Length; i++)
                            drs[i].Delete();
                    }
                    else
                    {
                        dr = Lanta_ClientStatSputniki.NewRow();
                        Lanta_ClientStatSputniki.Rows.Add(dr);
                    }
                    dr["CSS_CLКеу"] = drf["CSS_CLКеу"];
                    dr["CSS_DGKEY"] = DG_KEY;
                }
            }

            builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.UpdateCommand.CommandText = "UPDATE [Lanta_ClientStatSputniki] SET [CSS_CLКеу] = @p1, [CSS_DGKEY] = @p2 WHERE ([CSS_ID] = @p3)";
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.Update(Lanta_ClientStatSputniki);
            return DG_KEY;
        }
        public void button9_Click(object sender, EventArgs e)
        {
            textBox_PSW.Text = GenerateRandom(8);
        }

        public void button10_Click(object sender, EventArgs e)
        {
            textBox_LOGIN.Text = GenerateRandom(6);
        }

        private string GenerateRandom(int len)
        {
            string ret = "";
            byte[] b = new byte[1];
            char[] chars = new char[1];

            while (ret.Length < len)
            {
                rnd.NextBytes(b);
                d.GetChars(b, 0, b.Length, chars, 0);
                if (Char.IsLetterOrDigit(chars[0]))
                    ret = ret + chars[0];
            }
            return ret;
        }

        private void button13_Click(object sender, EventArgs e)
        {//Комментарий к поездкам
            if (dataGridView_STAT.SelectedRows.Count > 0)
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                DataGridViewRow row = dataGridView_STAT.SelectedRows[0];
                int DG_Key = Convert.ToInt32(row.Cells["DG_Keyi"].Value);
                string comment=string.Empty;
                string DG_CODE=string.Empty;
                using (SqlCommand com = new SqlCommand(@"SELECT CSD_DGCOMMENT,CSD_DGCODE FROM Lanta_ClientStatDogovor where CSD_DGKEY=@p1", connection))
                {
                    com.Parameters.AddWithValue("@p1", DG_Key);
                    var rd = com.ExecuteReader();
                    while(rd.Read())
                    {
                         comment = Convert.ToString(rd["CSD_DGCOMMENT"]);
                         DG_CODE = Convert.ToString(rd["CSD_DGCODE"]);
                    }
                    rd.Close();
                }
                if (DG_CODE!=string.Empty)
                {
                   
                   
                    EditDogovorComment edc = new EditDogovorComment(DG_CODE, comment);
                    if (edc.ShowDialog() == DialogResult.OK)
                    {
                        //dr["CSD_DGCOMMENT"] = edc.ret_comment;
                        
                        using(SqlCommand com = new SqlCommand(@"UPDATE Lanta_ClientStatDogovor SET [CSD_DGCOMMENT]=@p1 where CSD_DGCODE=@p2",connection))
                        {
                            com.Parameters.AddWithValue("@p1", edc.ret_comment);
                            com.Parameters.AddWithValue("@p2", DG_CODE);
                            com.ExecuteNonQuery();
                        }
                        dataGridView_STAT.Refresh();
                    }
                }
            }
            else
                MessageBox.Show("Выберите, пожалуйста, поездку для комментария");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //Проверить все вкладки
            if (ValidateTab("-1", false))
            {
                MessageBox.Show("Все поля заполнены успешно!");
            }
        }

        private void comboBox_Curator_SelectedValueChanged(object sender, EventArgs e)
        {
            //checkBox_MessageToCurator.Visible = true;
            checkBox_MessageToClent.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {//Выдать карту
            DiscountCard dc = new DiscountCard(CL_KEY, MANAGER_ID, connection);
            if (dc.ShowDialog() == DialogResult.OK)
                RefreshCards();
        }

        private void button_DelCard_Click(object sender, EventArgs e)
        {//Удалить карту
            if (dataGridView_CARDS.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dataGridView_CARDS.SelectedRows[0];
                    int CD_Key = Convert.ToInt32(row.Cells["CD_Key"].Value);
                    string card = Convert.ToString(row.Cells["CD_Code"].Value) + " " + Convert.ToString(row.Cells["CD_Number"].Value);
                    //CARDS.CD_Code, CARDS.CD_Number,
                    if (MessageBox.Show("Вы уверены что хотите удалить дисконтную карту '" + card + "'?", "Подтверждение удаления!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        SqlCommand cmd = new SqlCommand("delete from CARDS where CD_Key=" + CD_Key.ToString(), connection);
                        cmd.ExecuteNonQuery();
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                        RefreshCards();
                    }
                }
                catch (Exception cex)
                {
                    ExceptionForm ef = new ExceptionForm(cex.ToString());
                    ef.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Выберите, пожалуйста, дисконтную карту для удаления");
            }
        }

        private void button_CheckMail_Click(object sender, EventArgs e)
        {
            label_CheckMail.Text = "";
            linkLabel1.Visible = false;
            EMailCheck em = new EMailCheck();
            if (em.Check(textBox_cl_mail.Text.TrimEnd(' ')))
            {
                label_CheckMail.Text = "ОК";
            }
            else
            {
                label_CheckMail.Text = "EMail не найден. Проверьте по адресу:";
                //linkLabel1.Text = "http://domw.net/service:network-email#l:email:data:"+textBox_cl_mail.Text.TrimEnd(' ');
                //linkLabel1.Text = "http://domw.net/service:network-email";
                linkLabel1.Links[0].LinkData = "http://domw.net/service:network-email#l:email:data:" + textBox_cl_mail.Text.TrimEnd(' ');
                //linkLabel1.Links.Clear();
                //linkLabel1.Links.Add(0,0, linkLabel1.Text);
                linkLabel1.Visible = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Convert.ToString(linkLabel1.Links[0].LinkData));
        }

        private void comboBox_CL_BIRTHCOUNTRY_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = ((DataRowView)(comboBox_CL_BIRTHCOUNTRY.SelectedItem)).Row;
            if (dr["LCI_FlagImage"] != System.DBNull.Value)
            {
                System.IO.Stream myStream = new System.IO.MemoryStream((byte[])dr["LCI_FlagImage"]);
                try
                {
                    pictureBox_LCU_PHOTO.Image = new Bitmap(myStream);
                }
                catch (Exception cex) { cex.ToString(); }
            }
            else
                pictureBox_LCU_PHOTO.Image = null;

        }

        private void comboBox_CL_CITIZEN_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = ((DataRowView)(comboBox_CL_CITIZEN.SelectedItem)).Row;
            if (dr["LCI_ISO"] != System.DBNull.Value)
            {
                CL_CITIZEN_LCI_ISO = Convert.ToInt32(dr["LCI_ISO"]);
                RusUkCitizen = CL_CITIZEN_LCI_ISO == 643 || CL_CITIZEN_LCI_ISO == 804;
                label_CL_SNAMERUS.Text = label_CL_SNAMERUS.Text.TrimEnd('*');
                if (RusUkCitizen)
                {
                    label_CL_SNAMERUS.Text = label_CL_SNAMERUS.Text + "*";
                    textBox_CL_PASPRUSER.ReadOnly = false;
                    textBox_CL_PASPRUNUM.ReadOnly = false;
                    textBox_CL_PASPRUBYWHOM.ReadOnly = false;
                    textBox_CL_PASPRUDATE.ReadOnly = false;
                    comboBox_CL_BIRTHCOUNTRY.SelectedValue = CL_CITIZEN_LCI_ISO;
                }
                else
                {
                    textBox_CL_PASPRUSER.ReadOnly = true; 
                    textBox_CL_PASPRUNUM.ReadOnly = true;
                    textBox_CL_PASPRUBYWHOM.ReadOnly = true;
                    textBox_CL_PASPRUDATE.ReadOnly = true;
                }
            }
            if (dr["LCI_FlagImage"] != System.DBNull.Value)
            {
                System.IO.Stream myStream = new System.IO.MemoryStream((byte[])dr["LCI_FlagImage"]);
                try
                {
                    pictureBox_CL_CITIZEN.Image = new Bitmap(myStream);
                }
                catch (Exception cex) { cex.ToString(); }
            }
            else
                pictureBox_CL_CITIZEN.Image = null;
        }

        private void button_CopyClientData_Click(object sender, EventArgs e)
        {//Скопировать данные адреса одного клиента в другого
            if (dataGridView_Fam.SelectedRows.Count == 2)
            {
                ClientCopyData cud = new ClientCopyData(
                    Convert.ToInt32(dataGridView_Fam.SelectedRows[1].Cells["CF_CLKey"].Value),
                    Convert.ToInt32(dataGridView_Fam.SelectedRows[0].Cells["CF_CLKey"].Value),
                    MANAGER_ID, connection
                    );
                if (cud.ShowDialog() == DialogResult.OK)
                {
                    GetFamilyInfo();
                }
            }
            else
            {
                MessageBox.Show("Нужно выбрать двух клиентов для копирования данных");
            }
        }

        private void button_MorePassport_Click(object sender, EventArgs e)
        {
            ClientPassports cp = new ClientPassports(CL_KEY,adapter);
            if (cp.ShowDialog() == DialogResult.OK)
            {
                DataRow dr = cp.drPaspInfo;
                textBox_CL_PASPORTSER.Text = Convert.ToString(dr["CL_PASPORTSER"]);
                textBox_CL_PASPORTNUM.Text = Convert.ToString(dr["CL_PASPORTNUM"]);
                textBox_CL_NAMELAT.Text = Convert.ToString(dr["CL_NAMELAT"]);
                textBox_CL_FNAMELAT.Text = Convert.ToString(dr["CL_FNAMELAT"]);                
                textBox_CL_SNAMELAT.Text = Convert.ToString(dr["CL_SNAMELAT"]);
                textBox_CL_PASPORTDATE.Text = Convert.ToDateTime(dr["CL_PASPORTDATE"]).ToString("d");
                textBox_CL_PASPORTDATEEND.Text = Convert.ToDateTime(dr["CL_PASPORTDATEEND"]).ToString("d");
                textBox_CL_PASPORTBYWHOM.Text = Convert.ToString(dr["CL_PASPORTBYWHOM"]);
            }
        }
        const string UPDATE_QUERY = @"UPDATE [clients] SET  [CL_IMPRESSNOTE] = @p2, [CL_NOTE] = @p3, [CL_REMARK] = @p4, [CL_IMPRESSKEY] = @p5, [CL_TITLE1] = @p6, [CL_TITLE2] = @p7, [CL_TITLE3] = @p8, [CL_TITLE4] = @p9, [CL_FUTURE] = @p10 WHERE ([CL_KEY] = @p1)";
        private void button16_Click(object sender, EventArgs e)
        {
            object impkey = DBNull.Value;
            if (listBox_CL_IMPRESSKEY.SelectedIndex > 0)
                impkey = listBox_CL_IMPRESSKEY.SelectedIndex;
            if(connection.State==ConnectionState.Closed)connection.Open();
            using(SqlCommand com = new SqlCommand(UPDATE_QUERY,connection))
            {
                com.Parameters.AddWithValue("@p1", CL_KEY);
                com.Parameters.AddWithValue("@p2", textBox_CL_IMPRESSNOTE.Text);
                com.Parameters.AddWithValue("@p3", textBox_CL_NOTE.Text);
                com.Parameters.AddWithValue("@p4", textBox_CL_REMARK.Text);
                com.Parameters.AddWithValue("@p5", impkey);
                com.Parameters.AddWithValue("@p6", textBox_CL_TITLE1.Text);
                com.Parameters.AddWithValue("@p7", textBox_CL_TITLE2.Text);
                com.Parameters.AddWithValue("@p8", textBox_CL_TITLE3.Text);
                com.Parameters.AddWithValue("@p9", textBox_CL_TITLE4.Text);
                com.Parameters.AddWithValue("@p10", textBox_CL_FUTURE.Text);
                com.ExecuteNonQuery();
            }
             
        }

        private void btnFromPassport_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in clientsDT.Rows)
            {
                
                if (dr["CL_CITIZEN"] != DBNull.Value && dr["CL_CITIZEN"].ToString() != String.Empty)
                {
                    cbCountry.SelectedItem =
                        (cbCountry.DataSource as DataTable).Select(string.Format("LCI_RUSNAME like '{0}'",
                                                                                 dr["CL_CITIZEN"].ToString()))[0];
                }
                tbPostIndex.Text = dr["CL_POSTINDEX"].ToString();
                tbSettlement.Text = dr["CL_POSTCITY"].ToString();
                tbStreet.Text = dr["CL_POSTSTREET"].ToString();
                tbHouse.Text = dr["CL_POSTBILD"].ToString();
                tbApartment.Text = dr["CL_POSTFLAT"].ToString();
            }
        }

        private void btnSaveAddress_Click(object sender, EventArgs e)
        {
            if (tbSettlement.Text == string.Empty || tbPostIndex.Text == string.Empty || tbStreet.Text == string.Empty || tbHouse.Text == string.Empty)
                MessageBox.Show("Не все обязательные поля заполнены", "Ошибка", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            else
            {
                SqlDataAdapter adapter = new SqlDataAdapter(@"select * from mk_tbClientsMailAddress where CL_KEY = "+CL_KEY,connection);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.UpdateCommand = new SqlCommand(@"update mk_tbClientsMailAddress set COUNTRY=@p2,REGION=@p3,SETTLEMENT=@p4,CITY_INDEX=@p5,STREET=@p6,HOUSE=@p7,APARTMENTS=@p8 WHERE CL_KEY = @p1",connection);
                DataTable dt = new DataTable("address");
               
                adapter.Fill(dt);
                DataRow dr;
                if(dt.Rows.Count==0)
                {
                    dr = dt.NewRow();
                    
                    dr["CL_KEY"] = CL_KEY;
                    dr["COUNTRY"] = cbCountry.Text;
                    dr["REGION"] = tbRegion.Text;
                    dr["SETTLEMENT"] = tbSettlement.Text;
                    dr["CITY_INDEX"] = tbPostIndex.Text;
                    dr["STREET"] = tbStreet.Text;
                    dr["HOUSE"] = tbHouse.Text;
                    dr["APARTMENTS"] = tbApartment.Text;
                    dt.Rows.Add(dr);
                    adapter.Update(dt);
                }
                else
                {
                    adapter.UpdateCommand.Parameters.AddWithValue("@p1", CL_KEY);
                    adapter.UpdateCommand.Parameters.AddWithValue("@p2", cbCountry.Text);
                    adapter.UpdateCommand.Parameters.AddWithValue("@p3", tbRegion.Text);
                    adapter.UpdateCommand.Parameters.AddWithValue("@p4", tbSettlement.Text);
                    adapter.UpdateCommand.Parameters.AddWithValue("@p5", Convert.ToInt32(tbPostIndex.Text));
                    adapter.UpdateCommand.Parameters.AddWithValue("@p6", tbStreet.Text);
                    adapter.UpdateCommand.Parameters.AddWithValue("@p7", tbHouse.Text);
                    adapter.UpdateCommand.Parameters.AddWithValue("@p8", tbApartment.Text);
                    adapter.UpdateCommand.ExecuteNonQuery();
                }
               
            }
        }

        private void tbPostIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar))
            {
                if(e.KeyChar!=(char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void tabPage14_Enter(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(@"select * from mk_tbClientsMailAddress where CL_KEY = " + CL_KEY, connection);
              DataTable dt = new DataTable("address");
                adapter.Fill(dt);
                DataRow dr;
                if (dt.Rows.Count == 0) return;
             dr = dt.Rows[0];
             cbCountry.Text = dr["COUNTRY"].ToString() ;
             tbRegion.Text = dr["REGION"]==DBNull.Value?string.Empty:dr["REGION"].ToString();
             tbSettlement.Text = dr["SETTLEMENT"].ToString();
             tbPostIndex.Text = dr["CITY_INDEX"].ToString();
             tbStreet.Text=dr["STREET"].ToString();
             tbHouse.Text=dr["HOUSE"].ToString();
             tbApartment.Text = dr["APARTMENTS"] == DBNull.Value ? string.Empty : dr["APARTMENTS"].ToString();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            ///tabControl1.TabPages 
            //foreach (TabPage tabPage in tabControl1.TabPages)
            //{
            //    tabPage.Capture = SystemColors.Control;

            //}
            //e.TabPage.BackColor = SystemColors.ActiveCaption;
        }
     }
}
