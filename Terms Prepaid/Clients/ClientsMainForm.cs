using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using lanta.DirectClientDogovor;
using System.Threading;

namespace lanta.Clients
{
    public partial class ClientsMainForm : Form
    {
        DataTable clients = new DataTable();
        string filterName = "";
        public int return_CL_KEY = -1;
        public long MANAGER_ID = 1537;//Николаев
        SqlConnection connection = null;
        SqlDataAdapter adapter;
        SqlCommandBuilder builder;
        bool Presenter = false;
        public int TU_KEY = -1;
        public DataRow tempClient = null;
        Passanger pas = null;
        SqlConnection turistFromConnection;
        private System.Threading.Timer timer1;
        public bool needCheck = true;
     
        public ClientsMainForm()
        {
            InitializeComponent();

        }
        public ClientsMainForm(string filterName, long MANAGER_ID, SqlConnection cnn, bool Presenter, int TU_KEY, SqlConnection turistFromConnection, bool needCheck)
        {
            InitializeComponent();
            this.TU_KEY = TU_KEY;
            this.turistFromConnection = turistFromConnection;
            if (TU_KEY > -1)
                продолжитьToolStripMenuItem.Visible = true;
            this.needCheck = needCheck;
            Init(filterName, MANAGER_ID, cnn, Presenter);

        }
        public void ReUse(string filterName, long MANAGER_ID, SqlConnection cnn, bool Presenter, int TU_KEY, SqlConnection turistFromConnection, bool needCheck)
        {
            this.TU_KEY = TU_KEY;
            this.turistFromConnection = turistFromConnection;
            if (TU_KEY > -1)
                продолжитьToolStripMenuItem.Visible = true;
            this.needCheck = needCheck;
            Init(filterName, MANAGER_ID, cnn, Presenter);
            RefreshData();

        }
        public ClientsMainForm(long MANAGER_ID, SqlConnection cnn, DataRow tempClient)
        {
            InitializeComponent();
            needCheck = false;
            this.tempClient = tempClient;
            string filterName = "";
            string clInfo = "";
            if (tempClient["CL_NAMERUS"] != System.DBNull.Value)
            {
                filterName = Convert.ToString(tempClient["CL_NAMERUS"]);
                clInfo = filterName + " ";
            }
            if (tempClient["CL_FNAMERUS"] != System.DBNull.Value)
                clInfo = clInfo + Convert.ToString(tempClient["CL_FNAMERUS"]) + " ";
            if (tempClient["CL_SNAMERUS"] != System.DBNull.Value)
                clInfo = clInfo + Convert.ToString(tempClient["CL_SNAMERUS"]) + " ";
            if (tempClient["CL_BIRTHDAY"] != System.DBNull.Value)
                clInfo = clInfo + (Convert.ToDateTime(tempClient["CL_BIRTHDAY"])).ToString("d") + " ";
            SetButtonSelectText("Выбор клиента", "Перемещение временного клиента в постоянного " + clInfo);
            Init(filterName, MANAGER_ID, cnn, Presenter);

        }         
        public ClientsMainForm(string filterName, long MANAGER_ID, SqlConnection cnn,  Passanger pas)
        {
            InitializeComponent();
            needCheck = false;
            this.pas = pas;
            if (TU_KEY > -1)
                продолжитьToolStripMenuItem.Visible = true;
            Init(filterName, MANAGER_ID, cnn, Presenter);

        }        
        public ClientsMainForm(string filterName, long MANAGER_ID, SqlConnection cnn,bool Presenter)
        {
            InitializeComponent();
            Init(filterName, MANAGER_ID, cnn, Presenter);
        }
        
        private void Init(string filterName, long MANAGER_ID, SqlConnection cnn, bool Presenter)
        {
            this.Presenter = Presenter;
            this.filterName = filterName;
            this.MANAGER_ID = MANAGER_ID;
            this.connection = cnn;

            button_select.Visible = true;
            adapter = new SqlDataAdapter("", cnn);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RefreshData();
            //В рабочей версии должно быть закомментировано
            
            //Сгенерировать логины и пароли всем клиентам, у кого нет
            //GenerateClientsAccessForAll();
        }
        private string SearchSpaces()
        {     
            string ret = "";
            string cur, rep;// add;
            //byte[] bytes;
            string[] drs;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable client = new DataTable();
            DataRow drv; 
            string addr = "";
            bool update;
            foreach (DataRow dr in clients.Rows)
            {
                int count_spaces = 1;
                /*
                cur = Convert.ToString(dr["CL_NAMERUS"]);
                bytes = Encoding.ASCII.GetBytes(cur);
                rep = cur;
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == 32)//пробел
                    {
                        count_spaces++;
                        rep = rep.Remove(i, 1);
                        rep = rep.Insert(i, "*");
                    }
                }*/
                if (count_spaces > 0)
                {

                    client.Clear();
                    adapter.SelectCommand = new SqlCommand("select CL_KEY,CL_ADDRESS,  CL_POSTINDEX,CL_POSTCITY,CL_POSTSTREET,CL_POSTBILD,CL_POSTFLAT from clients where CL_KEY=" + Convert.ToString(dr["CL_KEY"]), connection);
                    //adapter.SelectCommand = new SqlCommand("select CL_KEY,CL_NAMERUS,CL_FNAMERUS,CL_SNAMERUS,CL_BIRTHDAY from clients where CL_KEY=" + Convert.ToString(dr["CL_KEY"]), connection);
                    
                    adapter.Fill(client);
                    if (client.Rows.Count > 0)
                    {
                        update = false;
                        drv = client.Rows[0];
                        addr = "";
                        if (Convert.ToString(drv["CL_POSTINDEX"]).TrimEnd(' ').Length > 0)
                            addr += Convert.ToString(drv["CL_POSTINDEX"])
                                .Replace(",", "")
                                .Replace("-", "")
                                .Replace("X", "")
                                + ", ";

                        if (Convert.ToString(drv["CL_POSTCITY"])
                                 .Replace(",", "")
                                .Replace("X", "")
                                .Replace("x", "")
                                .Replace("х", "")
                                .Replace("Х", "")
                            .TrimEnd(' ')
                            .Length > 0)
                        {
                            rep = Convert.ToString(drv["CL_POSTCITY"]);
                            if (rep.IndexOf("обл") < 0
                                &&  rep.IndexOf("с.") < 0
                                )
                                addr += "г.";
                            addr += Convert.ToString(drv["CL_POSTCITY"]).Replace("MOSCOW", "Москва")
                                .Replace("Moscow", "Москва")
                                .Replace("M0SCOW", "Москва")
                                .Replace("Samara", "Самара")
                                .Replace("Norilsk", "Норильск")
                                .Replace("SEVERNIY", "Северный")
                                .Replace("Volgograd", "Волгоград")
                                .Replace("Tula", "Тула")
                                .Replace("Yssyryysk", "Уссурийск")
                                .Replace("RUS ", "")

                                .TrimEnd(' ')
                                + ", ";
                        }


                        if (Convert.ToString(drv["CL_POSTSTREET"]).TrimEnd(' ').Length > 0)
                        {
                            if (Convert.ToString(drv["CL_POSTSTREET"]).IndexOf("мкр") < 0)
                                addr += "ул.";
                            addr += Convert.ToString(drv["CL_POSTSTREET"])
                                .Replace(",", "")
                                .Replace("st.", "")
                                .Replace("str.", "")
                                .Replace("ul.", "")
                                .Replace("ул ", "")
                                .Replace("ул.", "")
                                .Replace("X", "")
                                .TrimEnd(' ')
                                .TrimStart(' ')
                                + ", ";
                        }
                        if (Convert.ToString(drv["CL_POSTBILD"]).TrimEnd(' ').Length > 0)
                            addr += "д." +Convert.ToString(drv["CL_POSTBILD"])
                                .TrimEnd('-')
                                .Replace("\\", "/")
                                + ", ";
                        if (Convert.ToString(drv["CL_POSTFLAT"]).TrimEnd(' ').Length > 0)
                            addr += "кв." + Convert.ToString(drv["CL_POSTFLAT"])
                                .Replace("кв", "")
                                .Replace("X", "")
                                .TrimEnd(' ');
                        addr = addr.TrimEnd(' ').TrimEnd(',').TrimEnd(' ');
                      
                        if (drv["CL_ADDRESS"] == System.DBNull.Value)
                        {
                            if (addr.Length > 0)
                            {
                                drv["CL_ADDRESS"] = addr;
                                update = true;
                            }
                        }
                        else
                        {
                            cur = Convert.ToString(drv["CL_ADDRESS"]);
                            if (cur != addr)
                            {
                                if (addr.Length > 0)
                                {
                                    drv["CL_ADDRESS"] = addr;
                                    update = true;
                                }
                                else
                                {
                                    cur = cur.Replace("ул.", "").Replace("д.", "").Replace("кв.", "").Replace("г.", "");
                                    if (cur.Replace(",","").Replace(" ","").Length == 0)
                                    {
                                        drv["CL_ADDRESS"] = System.DBNull.Value;
                                        update = true;
                                    }
                                    else
                                    {

                                        drs = cur.Split(',');//Восстановление адреса
                                        if (drs.Length == 1)
                                        {
                                            drv["CL_POSTCITY"] = drs[0]
                                                .Replace("-", "")
                                                .Replace("Х", "")                                                
                                                .Replace("х", "");

                                        }
                                        if (drs.Length == 2)
                                        {
                                            drv["CL_POSTSTREET"] = drs[0].Replace("-", "").TrimStart(' ');
                                            drv["CL_POSTBILD"] = drs[1].TrimStart(' ');

                                        }
                                        if (drs.Length == 3)
                                        {
                                            drv["CL_POSTCITY"] = drs[0].Replace("-", "");
                                            drv["CL_POSTSTREET"] = drs[1].Replace("-", "").TrimStart(' ');
                                            drv["CL_POSTBILD"] = drs[2].TrimStart(' ');

                                        }                                        
                                        if (drs.Length == 4)
                                        {
                                            drv["CL_POSTCITY"] = drs[0].Replace("-", "");
                                            drv["CL_POSTSTREET"] = drs[1].TrimStart(' ');
                                            drv["CL_POSTBILD"] = drs[2].TrimStart(' ');
                                            drv["CL_POSTFLAT"] = drs[3].TrimStart(' ');
                                        }

                                        if (drs.Length == 5)
                                        {
                                            drv["CL_POSTINDEX"] = drs[0];
                                            drv["CL_POSTCITY"] = drs[1].Replace("-", "");
                                            drv["CL_POSTSTREET"] = drs[2].TrimStart(' ');
                                            drv["CL_POSTBILD"] = drs[3].TrimStart(' ');
                                            drv["CL_POSTFLAT"] = drs[4].TrimStart(' ');
                                        }


                                        //update = true;
                                    }
                                }
                            }

                        }


                        //drv["CL_NAMERUS"] = Convert.ToString(dr["CL_NAMERUS"]).TrimEnd(' ').TrimStart(' ').TrimStart('.');//
                        /* drv["CL_FNAMERUS"] = Convert.ToString(dr["CL_FNAMERUS"]).TrimEnd(' ').TrimStart(' ').TrimStart('.');//
                            if (dr["CL_SNAMERUS"] != System.DBNull.Value)
                        drv["CL_SNAMERUS"] = Convert.ToString(dr["CL_SNAMERUS"]).TrimEnd(' ').TrimStart(' ').TrimStart('.');//
                            if (dr["cl_mail"] != System.DBNull.Value)
                        if (dr["CL_BIRTHDAY"]!=System.DBNull.Value)
                         drv["CL_BIRTHDAY"] = Convert.ToDateTime(dr["CL_BIRTHDAY"]).Date;//                         
                         
                        if (dr["cl_mail"] != System.DBNull.Value)
                        {
                            drv["cl_mail"] = Convert.ToString(dr["cl_mail"]).TrimEnd(' ').TrimStart(' ').TrimStart('.');//
                            cur = Convert.ToString(drv["cl_mail"]);
                            if (cur.Length == 0)
                                drv["cl_mail"] = System.DBNull.Value;
                            else
                                if (!cur.Contains('@'))
                                    drv["cl_mail"] = System.DBNull.Value;
                        }
                        else
                            drv["cl_mail"] = System.DBNull.Value;

                        */
                        if (update)
                        {
                            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                            adapter.UpdateCommand = builder.GetUpdateCommand();
                            //adapter.UpdateCommand.CommandText = "UPDATE [clients] SET [CL_NAMERUS] = @p2, [CL_FNAMERUS] = @p3, [CL_SNAMERUS] = @p4, [CL_BIRTHDAY] = @p5 WHERE ([CL_KEY] = @p1)";
                            adapter.UpdateCommand.CommandText = "UPDATE [clients] SET  [CL_ADDRESS] = @p2, [CL_POSTINDEX] = @p3, [CL_POSTCITY] = @p4, [CL_POSTSTREET] = @p5, [CL_POSTBILD] = @p6, [CL_POSTFLAT] = @p7 WHERE ([CL_KEY] = @p1)";
                            //adapter.UpdateCommand.Parameters["@p1"].Value = Convert.ToInt32(dr["CL_KEY"]);
                            //adapter.InsertCommand = builder.GetInsertCommand();
                            adapter.Update(client);
                        }
                    }

                    //add = Convert.ToString(dr["CL_KEY"]) + " '" + cur + "'<-'" + rep + "'\r\n";
                    //ret = ret + add;
                }
            }
            RefreshData();
            return ret;
        }   
        private string SearchFusionNames()
        {
            string ret = "";
            string cur;// rep;// add;
            byte[] bytes;
            foreach (DataRow dr in clients.Rows)
            {
                
                cur = Convert.ToString(dr["CL_NAMERUS"]);
                bytes = Encoding.ASCII.GetBytes(cur);
                int count_russian=0,count_nonrus=0;
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == 63 || bytes[i] == 32 || bytes[i] == 45)//Русский символ, пробел или дефис
                        count_russian++;
                    else
                        count_nonrus++;

                }
                if (count_russian != bytes.Length)
                {
                    //if (count_russian > count_nonrus)
                   // {
                        dr["CL_NAMERUS"] = TranslitRUS(Convert.ToString(dr["CL_NAMERUS"]));
                        dr["CL_FNAMERUS"] = TranslitRUS(Convert.ToString(dr["CL_FNAMERUS"]));
                        dr["CL_SNAMERUS"] = TranslitRUS(Convert.ToString(dr["CL_SNAMERUS"]));
                        /*
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            if (bytes[i] != 63 && bytes[i] != 32 && bytes[i] != 45)
                            {
                                rep = rep.Remove(i, 1);
                                rep = rep.Insert(i, "*");
                            }
                        }
                        add = Convert.ToString(dr["CL_KEY"]) + " '" +  cur + "'<-'" + rep + "'\r\n";
                        ret = ret + add;*/
                   // }
                }

            }
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.UpdateCommand.CommandText = "UPDATE [clients] SET [CL_NAMERUS] = @p2, [CL_FNAMERUS] = @p3, [CL_SNAMERUS] = @p4 WHERE [CL_KEY] = @p8";
            adapter.Update(clients);
            return ret;
        }
        private string TranslitRUS(string INText)
        {
            string ret = INText.ToUpper();
            if (ret.EndsWith("ER"))
                ret = ret.TrimEnd('R').TrimEnd('E') + "р";
            if (ret.EndsWith("IA"))
                ret = ret.TrimEnd('A').TrimEnd('I') + "ия";
            if (ret.EndsWith("EY"))
                ret = ret.TrimEnd('Y').TrimEnd('E') + "ей";
            if (ret.EndsWith("AY"))
                ret = ret.TrimEnd('Y').TrimEnd('A') + "ай";
            if (ret.EndsWith("IY"))
                ret = ret.TrimEnd('Y').TrimEnd('I') + "ий";
            if (ret.EndsWith("YY"))
                ret = ret.TrimEnd('Y') + "ый";
            if (ret.EndsWith("Y"))
                ret = ret.TrimEnd('Y') + "ий";

            ret =
            ret.Replace("(", "")
               .Replace(")", "")
               .Replace("  ", " ")
               .Replace("VIACHESLAV", "Вячеслав")
               .Replace("NADEZDA", "Надежда")
               .Replace("TATIANA", "Татьяна")
               .Replace("OLESYA", "Олеся")
               .Replace("ILYA", "Илья")
               .Replace("ELVIRA", "Эльвира")
               .Replace("OLGA", "Ольга")
               .Replace("IGOR", "Игорь")
               .Replace("CONSTANTIN", "Константин")
               .Replace("SHCH", "Щ")
               .Replace("YO", "Ё")
               .Replace("YE", "ье")
               .Replace("ZH", "Ж")
               .Replace("CH", "Ч")
               .Replace("SH", "Ш")
               .Replace("YU", "Ю")
               .Replace("YA", "Я")
               .Replace("AY", "АЙ")
               .Replace("IY", "ИЙ")
               .Replace("IU", "Ю")
               .Replace("TS", "Ц")
               .Replace("EY", "ЕЙ")
               .Replace("JA", "Джа")
               .Replace("JU", "Джу")
               .Replace("KH", "Х")
               .Replace("A", "А")
               .Replace("B", "Б")
               .Replace("V", "В")
               .Replace("G", "Г")
               .Replace("D", "Д")
               .Replace("E", "Е")
               .Replace("Z", "З")
               .Replace("I", "И")
               .Replace("K", "К")
               .Replace("L", "Л")
               .Replace("M", "М")
               .Replace("N", "Н")
               .Replace("O", "О")
               .Replace("P", "П")
               .Replace("R", "Р")
               .Replace("S", "С")
               .Replace("T", "Т")
               .Replace("U", "У")
               .Replace("W", "В")
               .Replace("F", "Ф")
               .Replace("H", "Х")
               .Replace("C", "Ц")
               .Replace("X", "КС")
               .Replace("Y", "Ы")
               .Replace("'", "Ь")
               .Replace("E", "Э")
               .ToLower();

            if (ret.Length > 0)
            {
                Char start = Char.ToUpper(ret[0]);
                ret = ret.Remove(0, 1);
                ret = ret.Insert(0, start.ToString());
            }
            return ret;
        }

        private void GenerateClientsAccessForAll()
        {
            EditClient ec;
            bool ch;
            foreach (DataRow dr in clients.Rows)
            {
                ec = new EditClient(Convert.ToInt64(dr["CL_KEY"]), MANAGER_ID, connection, Presenter,false);
                ch = false;
                if (ec.textBox_LOGIN.Text.Length == 0)
                {
                    ec.button10_Click(this, null); //Логин   
                    ch = true;
                }
                if (ec.textBox_PSW.Text.Length == 0)
                {
                    ec.button9_Click(this, null);  //Пароль
                    ch = true;
                }
                if (ch)
                {
                    while (!ec.ValidateLogin(false))//Если такой логин уже существует - надо перегенерировать
                    {
                        ec.button10_Click(this, null); //Логин
                    }
                    ec.SaveClientAccess();
                }
            
            }
        }
        private void RefreshCompare(int key1,int key2)
        {
            Cursor.Current = Cursors.WaitCursor;
            //connection.ChangeDatabase("lanta08");
            clients.Rows.Clear();
            try
            {
                adapter.SelectCommand = new SqlCommand(
                    @"select CL_KEY, CL_NAMERUS, CL_FNAMERUS, CL_SNAMERUS, CL_BIRTHDAY, CL_MINCOST, CL_MAXCOST from clients 
                WHERE CL_KEY=" + key1.ToString() + " OR CL_KEY=" + key2.ToString() +
                "order by CL_NAMERUS", connection);

                adapter.Fill(clients);
                dataGridView1.DataSource = clients;
            }
            finally { Cursor.Current = Cursors.Arrow; }
        }
        
        private void RefreshData()
        {
            Cursor.Current = Cursors.WaitCursor;
            //connection.ChangeDatabase("lanta08");
            clients.Rows.Clear();
            try
            {
                adapter.SelectCommand.CommandText = @"SELECT  Clients.CL_KEY, Clients.CL_NAMERUS,
                Clients.CL_FNAMERUS, Clients.CL_SNAMERUS, Clients.CL_BIRTHDAY,
                CONVERT(varchar, Clients.CL_BIRTHDAY, 104) AS CL_BIRTHDAY_STR,
                Clients.CL_PHONE,
                Clients.cl_mail, CARDS.CD_Code, CARDS.CD_Number,
                CARDS.CD_IsValid,Clients.CL_MINCOST, Clients.CL_MAXCOST
                        FROM         Clients LEFT OUTER JOIN
                      CARDS ON Clients.CL_KEY = CARDS.CD_CLKey
                        ORDER BY Clients.CL_NAMERUS";

                adapter.Fill(clients);
                dataGridView1.DataSource = clients;
                if (filterName.Length > 0)
                {
                    textBox_CL_NAMERUS.Text = filterName;
                    textBox1_TextChanged(this, null);
                }
            }
            finally { Cursor.Current = Cursors.Arrow; }
        }

        private void ClientsMainForm_Load(object sender, EventArgs e)
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    int? isAccess = null;

                    using (
                        SqlCommand com = new SqlCommand("select IS_ROLEMEMBER('mk_wp_SuperViser')",
                                                        connection))
                    {
                        isAccess = (int?) com.ExecuteScalar();
                    }
                    if (isAccess==1)
                    {
                        toolStripMenuItem2.Visible = true;
                        toolStripMenuItem4.Visible = true;
                    }
                }
            }
            RefreshData();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)//Добавление постоянных клиентов
        {
            AddClient();
        }
        public void AddClient()
        {
            if (tempClient !=null)
            {
                EditClient ec = new EditClient(-1, MANAGER_ID, connection, Presenter, false);
                ec.SaveClientByRow(tempClient,true);
                return_CL_KEY = Convert.ToInt32(ec.CL_KEY);
                this.DialogResult = DialogResult.OK;
            }
            else
                if (pas != null)
                {
                    EditClient ec = new EditClient(-1, MANAGER_ID, connection, Presenter, false);
                    int ret = ec.SaveClientByPassanger(pas);
                    if (ret == 1)
                    {
                        return_CL_KEY = Convert.ToInt32(ec.CL_KEY);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        if (ret == 0)
                            this.DialogResult = DialogResult.Cancel;
                        else
                            this.DialogResult = DialogResult.Ignore;
                    }
                }
                else
                    if (TU_KEY > -1)
                    {
                        EditClient ec = new EditClient(-1, MANAGER_ID, connection, Presenter, false);
                        ec.SaveClientByTurist(TU_KEY, turistFromConnection);
                        return_CL_KEY = Convert.ToInt32(ec.CL_KEY);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        ViewInfo(false);
                    }
        }
        
        private void toolStripMenuItem5_Click(object sender, EventArgs e)//Просмотр/редактирование
        {
            ViewInfo(true);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewInfo(true);
        }
        public void SetButtonSelectText(string text,string frameTitle)
        {
            button_select.Text = text;
            if (text == string.Empty)
            {
                panel1.Visible = false;
                button_select.Visible = false;
            }
            else
            {
                button_select.Visible = true;
                panel1.Visible = true;
            }
            this.Text = frameTitle;
        }
        public void ViewInfo(bool isEdit)
        {
            EditClient ec = null;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                long CL_KEY = Convert.ToInt64(row.Cells["CL_KEY"].Value);

                if (isEdit)
                    ec = new EditClient(CL_KEY, MANAGER_ID, connection,Presenter,false);
                else
                {
                    ec = new EditClient(-1, MANAGER_ID, connection,Presenter,false);
                    ec.templateClient = CL_KEY;
                }
            }
            else
            {
                if (isEdit)
                    MessageBox.Show("Выберите, пожалуйста, клиента для редактирования");
                else
                {
                    ec = new EditClient(-1, MANAGER_ID, connection,false,false);
                    ec.templateClient = -1;
                }
            }
            if (ec != null)
            {
                if (ec.ShowDialog() == DialogResult.OK)
                {
                    RefreshData();
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UseFilter();
            /*
            if (textBox1.Text.Length > 0)
            {
                DataRow[] drs = clients.Select("CL_NAMERUS like '" + textBox1.Text + "%' OR CL_NAMERUS like '" + this.TranslitRUS(textBox1.Text) + "%'");
                DataTable dt = clients.Clone();
                foreach (DataRow dr in drs)
                    dt.Rows.Add(dr.ItemArray);
                dataGridView1.DataSource = dt;

            }
            else
            {
                dataGridView1.DataSource = clients;
            }
            filterName = textBox1.Text;*/
        }
       
        private void UseFilter()
        {
            try
            {
                if (textBox_CL_NAMERUS.Text.Length > 0
                    || textBox_CL_FNAMERUS.Text.Length > 0
                    || textBox_CL_SNAMERUS.Text.Length > 0
                    || textBox_CL_BIRTHDAY.Text.Length > 0
                    || textBox_CL_PHONE.Text.Length > 0
                    || textBox_CD_NUMBER.Text.Length > 0
                    || textBox_CL_EMAIL.Text.Length > 0
                    )
                {
                    string filter = "";
                    if (textBox_CL_NAMERUS.Text.Length > 0)
                        filter = filter + "AND " + "CL_NAMERUS like '" + textBox_CL_NAMERUS.Text + "%' ";
                    if (textBox_CL_FNAMERUS.Text.Length > 0)
                        filter = filter + "AND " + "CL_FNAMERUS like '" + textBox_CL_FNAMERUS.Text + "%' ";
                    if (textBox_CL_SNAMERUS.Text.Length > 0)
                        filter = filter + "AND " + "CL_SNAMERUS like '" + textBox_CL_SNAMERUS.Text + "%' ";

                    if (textBox_CL_BIRTHDAY.Text.Length > 0)
                        filter = filter + "AND " + "CL_BIRTHDAY_STR like '%" + textBox_CL_BIRTHDAY.Text + "%' ";//OR
                    if (textBox_CL_PHONE.Text.Length > 0)
                        filter = filter + "AND " + "CL_PHONE like '%" + textBox_CL_PHONE.Text + "%' ";//OR
                    if (textBox_CD_NUMBER.Text.Length > 0)
                        filter = filter + "AND " + "CD_NUMBER like '%" + textBox_CD_NUMBER.Text + "%' ";//OR
                    if (textBox_CL_EMAIL.Text.Length > 0)
                        filter = filter + "AND " + "CL_mail like '%" + textBox_CL_EMAIL.Text + "%' ";//OR
                    if (filter.StartsWith("AND"))
                        filter = filter.Substring(4);
                    if (filter.StartsWith("OR"))
                        filter = filter.Substring(3);

                    DataRow[] drs = clients.Select(filter);
                    DataTable dt = clients.Clone();
                    foreach (DataRow dr in drs)
                        dt.Rows.Add(dr.ItemArray);
                    dataGridView1.DataSource = dt;

                }
                else
                {
                    dataGridView1.DataSource = clients;
                }
                filterName = textBox_CL_NAMERUS.Text;
             }
            catch (Exception cex)
            {
                ExceptionForm ef = new ExceptionForm(cex.ToString());
                ef.ShowDialog();
            }
        }



        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                long CL_KEY = Convert.ToInt64(row.Cells["CL_KEY"].Value);
                string CL_NAMERUS = Convert.ToString(row.Cells["CL_NAMERUS"].Value);
                if (MessageBox.Show("Вы уверены что хотите удалить клиента '" + CL_NAMERUS + "'?", "Подтверждение удаления!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (connection.State!= ConnectionState.Open)
                        connection.Open();
                    if (MessageBox.Show("Хотите назначить преемника - получателя оповещений для '" + CL_NAMERUS + "'?", "Подтверждение назначения преемника!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Выбор преемника
                        ClientsMainForm cl = new ClientsMainForm("", MANAGER_ID, connection, false);
                        cl.SetButtonSelectText("Выбрать преемника", "Выбор преемника - получателя оповещений");
                        if (cl.ShowDialog() == DialogResult.OK)
                        {
                            int preemnik = cl.return_CL_KEY;
                           
                            //Удаляемый
                            DataTable clientsDT = new DataTable("clientsDT");
                            adapter.SelectCommand = new SqlCommand("select CL_KEY, CL_ISMARK from clients where CL_KEY=" + Convert.ToString(CL_KEY), connection);
                            adapter.Fill(clientsDT);

                            //Преемник
                            DataTable preemnikDT = new DataTable("preemnikDT");
                            adapter.SelectCommand = new SqlCommand("select CL_KEY, CL_ISMARK from clients where CL_KEY=" + Convert.ToString(preemnik), connection);
                            adapter.Fill(preemnikDT);

                            if (preemnikDT.Rows.Count > 0)
                            {
                                preemnikDT.Rows[0]["CL_ISMARK"] = Convert.ToInt16(preemnikDT.Rows[0]["CL_ISMARK"]) | Convert.ToInt16(clientsDT.Rows[0]["CL_ISMARK"]);
                                
                                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                                adapter.UpdateCommand = builder.GetUpdateCommand();
                                adapter.UpdateCommand.CommandText = "UPDATE [clients] SET [CL_ISMARK] = @p2 WHERE [CL_KEY] = @p1";
                                //adapter.UpdateCommand.Transaction = transaction;                              
                                adapter.Update(preemnikDT);
                            }
                        }
                    }

                    DeleteClient(CL_KEY,-1);
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                    RefreshData();
                }
            }
            else
            {
                MessageBox.Show("Выберите, пожалуйста, клиента из списка для удаления");
            }
        }

        public  void DeleteClient(long CL_KEY, long newKey)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            //Каскадное удаление
            SqlTransaction transaction = connection.BeginTransaction("Transaction");
            SqlCommand cmd;

            //Удаление логина клиента
            DataTable login = new DataTable("login");
            adapter.SelectCommand = new SqlCommand("select CA_DUPUSERKEY FROM Lanta_ClientAccess where CA_CLКеу=" + Convert.ToString(CL_KEY), connection);
            adapter.SelectCommand.Transaction = transaction;
            adapter.Fill(login);
            if (login.Rows.Count > 0)
            {
                cmd = new SqlCommand("delete FROM  [dbo].[dup_user] where US_KEY=" + Convert.ToString(login.Rows[0]["CA_DUPUSERKEY"]), connection);
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
            }

            cmd = new SqlCommand("delete FROM Lanta_ClientAccess where CA_CLКеу=" + Convert.ToString(CL_KEY), connection);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

            //Удаление из семьи
            cmd = new SqlCommand("delete FROM Lanta_ClientFamily where CF_CLKey=" + Convert.ToString(CL_KEY), connection);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

            //Удаление представителей на основе клиента
            cmd = new SqlCommand("delete FROM Lanta_DogovorDeputat where DD_CLКеу=" + Convert.ToString(CL_KEY), connection);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

            //Удаление статистики клиента
            
            cmd = new SqlCommand("delete FROM Lanta_ClientStatDogovor where CSD_CLКеу=" + Convert.ToString(CL_KEY), connection);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("delete FROM Lanta_ClientStatDogovorList where CSL_CLKEY=" + Convert.ToString(CL_KEY), connection);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("delete FROM Lanta_ClientStatSputniki where CSS_CLКеу=" + Convert.ToString(CL_KEY), connection);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();
           

            //Удаление самого клиента
            cmd = new SqlCommand("delete FROM Clients where CL_KEY=" + Convert.ToString(CL_KEY), connection);
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

            //Запись об удалении в архивную таблицу(на всякий случай)
            DataTable Lanta_ClientDeleteHistory = new DataTable("Lanta_ClientDeleteHistory");
            adapter.SelectCommand = new SqlCommand("select * FROM Lanta_ClientDeleteHistory where CDH_CLKeyOld=" + Convert.ToString(CL_KEY), connection);
            adapter.SelectCommand.Transaction = transaction;
            adapter.Fill(Lanta_ClientDeleteHistory);
            DataRow histdr = Lanta_ClientDeleteHistory.NewRow();
            histdr["CDH_CLKeyOld"] = CL_KEY;
            if (newKey>-1)
                histdr["CDH_CLKeyNew"] = newKey;
            histdr["CDH_MANAGER"] = MANAGER_ID;
            histdr["CDH_CREATEDATE"] = DateTime.Now;
            Lanta_ClientDeleteHistory.Rows.Add(histdr);
           
            builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.UpdateCommand.CommandText = "UPDATE [tbl_Turist] SET  [CL_OPERUPDATE] = @p2, [CL_DATEUPDATE] = @p3, [CL_PFKEY] = @p4, [CL_NAMERUS] = @p5, [CL_NAMELAT] = @p6, [CL_SHORTNAME] = @p7, [CL_SEX] = @p8, [CL_FNAMERUS] = @p9, [CL_FNAMELAT] = @p10, [CL_SNAMERUS] = @p11, [CL_SNAMELAT] = @p12, [CL_BIRTHDAY] = @p13, [CL_BIRTHCOUNTRY] = @p14, [CL_BIRTHCITY] = @p15, [CL_CITIZEN] = @p16, [CL_ADDRESS] = @p17, [CL_POSTINDEX] = @p18, [CL_POSTCITY] = @p19, [CL_POSTSTREET] = @p20, [CL_POSTBILD] = @p21, [CL_POSTFLAT] = @p22, [CL_PHONE] = @p23, [CL_PASPORTSER] = @p24, [CL_PASPORTNUM] = @p25, [CL_PASPORTDATE] = @p26, [CL_PASPORTDATEEND] = @p27, [CL_PASPORTBYWHOM] = @p28, [CL_PASPRUSER] = @p29, [CL_PASPRUNUM] = @p30, [CL_PASPRUDATE] = @p31, [CL_PASPRUBYWHOM] = @p32, [CL_ISMARK] = @p33, [CL_TYPE] = @p34, [CL_IMPRESSNOTE] = @p35, [CL_NOTE] = @p36, [CL_REMARK] = @p37, [CL_IMPRESSKEY] = @p38, [CL_TITLE1] = @p39, [CL_TITLE2] = @p40, [CL_TITLE3] = @p41, [CL_TITLE4] = @p42, [CL_FUTURE] = @p43, [CL_LASTSTAT] = @p44, [CL_SUMMA] = @p45, [CL_NMENWITH] = @p46, [CL_SUMDOGOVOR] = @p47, [CL_NTRIP] = @p48, [cl_fax] = @p49, [cl_mail] = @p50, [CL_MINCOST] = @p51, [CL_MAXCOST] = @p52, [CL_DSKEY] = @p53, [CL_RealSex] = @p54 WHERE [CL_KEY] = @p1";
            adapter.Update(Lanta_ClientDeleteHistory);


            
            
            transaction.Commit();
            if (connection.State != ConnectionState.Closed)
                connection.Close();

        }

        private void button_select_Click(object sender, EventArgs e)
        {
            //Выбор пользователя в туристы, для создания семьи, в представители
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                return_CL_KEY = Convert.ToInt32(row.Cells["CL_KEY"].Value);
               
                //Перед выбором проверяется правильность и полнота заполнения данных
                EditClient ec = new EditClient(return_CL_KEY, MANAGER_ID, connection, Presenter, false);
                ec.UpdateClientByTurist(TU_KEY, turistFromConnection);//Если в клиенте не достаёт данных, они обновляются в клиенте по туристу!
                if (needCheck)
                {
                    if (!ec.ValidateTab("-1", true))
                    {
                        if (!(ec.ShowDialog() == DialogResult.OK))
                            return;
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Не выбран!");
            }


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                label2.Text = Convert.ToString(row.Cells["CL_NAMERUS"].Value) + "("+Convert.ToString(row.Cells["CL_KEY"].Value)+")";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 2)
            {
                ClientUnionDialog cud = new ClientUnionDialog(
                    Convert.ToInt32(dataGridView1.SelectedRows[1].Cells["CL_KEY"].Value),
                    Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CL_KEY"].Value),
                    MANAGER_ID,connection
                    );
                if (cud.ShowDialog() == DialogResult.OK)
                {
                    RefreshData();
                }
            }
            else if (dataGridView1.SelectedRows.Count > 2)
            {
                if (MessageBox.Show(@"Вы уверены что хотите объединить "+dataGridView1.SelectedRows.Count.ToString()+" клиентов в одного первого?","Подтверждение объединения", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    ClientUnionDialog cud = new ClientUnionDialog(0, 0, MANAGER_ID, connection);
                    for (int i = 1; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        cud.ReUse(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CL_KEY"].Value),
                            Convert.ToInt32(dataGridView1.SelectedRows[i].Cells["CL_KEY"].Value));
                        cud.UnionClients(i == dataGridView1.SelectedRows.Count - 1);
                    }
                    RefreshData();
                }
            }
            else
            {
                MessageBox.Show(@"Необходимо выбрать 2х клиентов для объединения.
Для выбора используйте клавишу CTRL");
            }
        }

        private void удалениеПробеловВКонцеФамилийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Поиск клиентов, у которых  фамилии заканчиваются на пробелы
            string list = SearchSpaces();
            ShowList sl = new ShowList(list);
            sl.Show();
        }

        private void поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Поиск клиентов, у которых в фамилии присутсвуют как русские, так и английские буквы
            
            string list = SearchFusionNames();
            ShowList sl= new ShowList(list);
            sl.Show();
        }

        private void поискИПривязкаНепривязанныхТуристовToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //SearchTourists();
        }


        private void сравнитьСПрошлымГодомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //В отдельный поток, чтоб можно было просмaтривать клиентов
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            this.timer1 = new System.Threading.Timer(new TimerCallback(timer1_Tick), autoEvent, 0, Timeout.Infinite);
            
        }  
        
        private string SearchBaseDifference()
        {
           string ret = "Клиенты, которые есть в старой базе и нет в текущей. Или данные отличаются\r\n";

           try
           {
               if (connection.State != ConnectionState.Open)
                   connection.Open();

               DataTable clients = new DataTable();
               clients.Clear();
               adapter.SelectCommand = new SqlCommand(@"select * from clients order by CL_NAMERUS", connection);
               adapter.Fill(clients);


               string curDataBase = connection.Database;
               DataBaseName dbn = new DataBaseName();
               dbn.ShowDialog();
               connection.ChangeDatabase(dbn.textBox_db.Text);//lanta08
               DataTable clientOld = new DataTable();//WHERE (CL_NAMERUS > 'д')
               adapter.SelectCommand = new SqlCommand(@"select * from clients  order by CL_NAMERUS", connection);
               adapter.Fill(clientOld);
               connection.ChangeDatabase(curDataBase);



               string CL_KEY, CL_NAMERUS, CL_FNAMERUS, CL_SNAMERUS, filter;
               DateTime CL_BIRTHDAY;
               DataRow[] drs;
               AddMissedClient mc;

               EditClient ec = new EditClient(-1, MANAGER_ID, connection, Presenter,false);
               DataRow[] drsc;
               DialogResult res;
               foreach (DataRow drOld in clientOld.Rows)
               {
                   CL_KEY = Convert.ToString(drOld["CL_KEY"]);
                   CL_NAMERUS = Convert.ToString(drOld["CL_NAMERUS"]);
                   CL_FNAMERUS = Convert.ToString(drOld["CL_FNAMERUS"]);
                   CL_SNAMERUS = Convert.ToString(drOld["CL_SNAMERUS"]);
                   CL_BIRTHDAY = DateTime.Now;
                   if (drOld["CL_BIRTHDAY"] != System.DBNull.Value)
                       CL_BIRTHDAY = Convert.ToDateTime(drOld["CL_BIRTHDAY"]);

                   //filter = "CL_KEY=" + CL_KEY+ " AND ";
                   filter = "CL_NAMERUS='" + CL_NAMERUS.Replace("'", "''") + "'";
                   if (CL_FNAMERUS.Length > 0)
                       filter = filter + " AND CL_FNAMERUS='" + CL_FNAMERUS.Replace("'", "''") + "'";
                   if (CL_SNAMERUS.Length > 0)
                       filter = filter + " AND CL_SNAMERUS='" + CL_SNAMERUS.Replace("'", "''") + "'";
                   if (drOld["CL_BIRTHDAY"] != System.DBNull.Value)
                       filter = filter + " AND CL_BIRTHDAY=#" + CL_BIRTHDAY.Month.ToString() + "/" + CL_BIRTHDAY.Day.ToString() + "/" + CL_BIRTHDAY.Year.ToString()
                           + " " + CL_BIRTHDAY.Hour.ToString() + ":" + CL_BIRTHDAY.Minute.ToString() + ":" + CL_BIRTHDAY.Second.ToString() + "#";
                   if (filter == "CL_NAMERUS=''")
                       continue;
                   drs = clients.Select(filter);
                   if (drs.Length == 0)//не найдена
                   {

                       mc = new AddMissedClient(drOld);
                       res = mc.ShowDialog();
                       ret = ret + "=" + filter + "\r\n";
                       if (res == DialogResult.OK)
                       {
                           drsc = clients.Select("CL_KEY=" + Convert.ToString(drOld["CL_KEY"]));
                           ec.SaveClientByRow(drOld, drsc.Length > 0);
                       }
                       else
                       if (res==DialogResult.Cancel)
                          return ret;



                   }


               }
           }
           catch (Exception cex)
           {
               ExceptionForm ef = new ExceptionForm(cex.ToString());
               ef.ShowDialog();
           }
            return ret;
        }

        private void timer1_Tick(Object stateInfo)
        {
            //timer1.Enabled = false;
            string list = SearchBaseDifference();
            ShowList sl = new ShowList(list);
            sl.ShowDialog();
            timer1.Dispose();

        }

        private void спискиРассылокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterClient cl = new FilterClient(connection);
            cl.Show();
        }

        private void подсчётСтатистикиВладельцевКартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgressStatisticCalculation psc = new ProgressStatisticCalculation(connection,MANAGER_ID);
            psc.Show();
        }

        private void продолжитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
        }

        private void похожиеКлиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable EqCl = new DataTable("EqCl");
            Random rnd = new Random(DateTime.Now.Millisecond);

            int fromkey = rnd.Next(777000448, 777060994);//rnd.Next(14874, 21581);
            adapter.SelectCommand.CommandText = @"SELECT     TOP (1) Lanta_ClientStatDogovor.CSD_CLКеу, Lanta_ClientStatDogovor.CSD_DGCODE, Clients.CL_KEY AS key1, Clients_1.CL_KEY AS key2, 
                      Clients.CL_NAMERUS, Clients.CL_FNAMERUS, Clients.CL_SNAMERUS, Clients.CL_BIRTHDAY
                        FROM         Lanta_ClientStatDogovor INNER JOIN
                                              Clients ON Lanta_ClientStatDogovor.CSD_CLКеу = Clients.CL_KEY INNER JOIN
                                              Lanta_ClientStatDogovor AS Lanta_ClientStatDogovor_1 ON 
                                              Lanta_ClientStatDogovor.CSD_DGCODE = Lanta_ClientStatDogovor_1.CSD_DGCODE INNER JOIN
                                              Clients AS Clients_1 ON Lanta_ClientStatDogovor_1.CSD_CLКеу = Clients_1.CL_KEY AND Clients.CL_NAMERUS = Clients_1.CL_NAMERUS AND 
                                              Clients.CL_FNAMERUS = Clients_1.CL_FNAMERUS AND Clients.CL_KEY <> Clients_1.CL_KEY
                        WHERE     (Lanta_ClientStatDogovor.CSD_CLКеу > " +fromkey.ToString()+@")
                        ORDER BY Lanta_ClientStatDogovor.CSD_CLКеу";
            adapter.Fill(EqCl);
            if (EqCl.Rows.Count > 0)
            {
                DataRow dr = EqCl.Rows[0];
                filterName = Convert.ToString(dr["CL_NAMERUS"]).TrimEnd(' ');
                textBox_CL_NAMERUS.Text = filterName;
                textBox1_TextChanged(this, null);
                //this.Text = Convert.ToInt32(dr["key1"]).ToString() + " =? " + Convert.ToInt32(dr["key2"]).ToString();
                

                for (int i=0;i< dataGridView1.Rows.Count;i++)
                {
                    dataGridView1.Rows[i].Selected =
                        Convert.ToInt32(dataGridView1.Rows[i].Cells["CL_KEY"].Value) == Convert.ToInt32(dr["key1"])
                        || Convert.ToInt32(dataGridView1.Rows[i].Cells["CL_KEY"].Value) == Convert.ToInt32(dr["key2"]);
                }
                //RefreshCompare(Convert.ToInt32(dr["key1"]),Convert.ToInt32(dr["key2"]));
               
            }

        }

        private void разделениеНомеровКартНаСериюИНомерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string list = SearchFusionCards();
            ShowList sl = new ShowList(list);
            sl.Show();
        }

        private string SearchFusionCards()
        {
            DataTable Cards = new DataTable("Cards");
            adapter.SelectCommand.CommandText = @"SELECT      CD_Key, CD_Code, CD_Number
                FROM         CARDS";
            adapter.Fill(Cards);
            
            
            string ret = "";
            string cur;// rep;// add;
            byte[] bytes;
            long number;
            string digit = "", nondigit = "";
            foreach (DataRow dr in Cards.Rows)
            {
                cur = Convert.ToString(dr["CD_Number"]);
                if (cur.Length > 0)
                {
                    if (long.TryParse(cur, out number))//Если номер карты состоит из цифр - всё нормально
                        continue;

                    bytes = Encoding.ASCII.GetBytes(cur);
                    digit = ""; nondigit = Convert.ToString(dr["CD_Code"]);
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        if (bytes[i] >= 48 && bytes[i] <= 57)//Цифры в ASCII 48-57
                            digit += Encoding.ASCII.GetString(bytes, i, 1);
                        else
                            nondigit += Encoding.ASCII.GetString(bytes, i, 1);
                    }
                    dr["CD_Code"] = nondigit.TrimEnd(' ');
                    dr["CD_Number"] = digit;
                    //if (nondigit != "_" && nondigit != "b" && nondigit != "g"
                    //    && nondigit != "s" && nondigit.TrimEnd(' ') != "LTV"&& nondigit.TrimEnd(' ') != "LTC"
                        
                    //    )
                        //rep = "";
                }
                //else
                //{
                //        rep = "";
                //}

            }
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.UpdateCommand.CommandText = "UPDATE [CARDS] SET [CD_Code] = @p2, [CD_Number] = @p3 WHERE [CD_Key] = @p4";
            adapter.Update(Cards);
            return ret;
        }

        private void флагиСтранToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadPicture lp = new LoadPicture("Lanta_ISOCountry", "LCI_ISO", "LCI_RUSNAME", "LCI_FlagImage", adapter);
            lp.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
