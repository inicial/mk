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
    public partial class Passanger : Form
    {
        SqlDataAdapter adapter;
        int PassangerKey;
        SqlCommandBuilder builder;
        SqlConnection connection;
        DataTable Clients = new DataTable("Clients");
        DataTable passanger = new DataTable("passanger");
        long MANAGER_ID; 
        //ADT - взрослый пассажир; CNN - ребенок; INF - инфант; YTH - молодежь; SRC - пенсионер.
        string[] titules = new string[] { "MRS", "MR", "MSTR", "CHD", "(ADT)", "(CHD)", "(YTH)", "(SRC)", "(INF)","RS", "MT" };
        public Passanger(int Passanger_KEY, SqlConnection connection,long MANAGER_ID)
        {
            InitializeComponent();
            this.connection = connection;
            this.MANAGER_ID = MANAGER_ID;
            adapter = new SqlDataAdapter("", connection);
            if (Passanger_KEY>0)
                ReUse(Passanger_KEY);

        }
        private void ClearFields()
        {
            textBox_PASSNAME.Text = "";
            textBox_NUMTICKET.Text = "";
            textBox_PASSPASSPORT.Text = "";
            textBox_RESTRICTION.Text = "";
            textBox_ADDRESTRICTION.Text = "";
            textBox_BPURESTRICTION.Text = "";
            textBox_PassBirthDay.Text = "";
            textBox_DatePassport.Text = "";
            
            
            textBox_NAMERUS.Text = "";
            textBox_FNAMERUS.Text = "";
            textBox_SNAMERUS.Text = "";
            textBox_NAMELAT.Text = "";
            textBox_FNAMELAT.Text = "";
            textBox_SNAMELAT.Text = "";
            textBox_SEX.Text = "";
            textBox_RealSex.Text = "";
            textBox_BIRTHDAY.Text = "";
            textBox_PASPRUSER.Text = "";
            textBox_PASPRUNUM.Text = "";
            textBox_PASPORTSER.Text = "";
            textBox_PASPORTNUM.Text = "";
            textBox_PASPORTDATEEND.Text = "";
        }
        private void ParseDate(string infantInfo)
        {
            infantInfo = infantInfo
                                        .Replace("JAN", ".01.")
                                        .Replace("ЯНВ", ".01.")
                                        .Replace("FEB", ".02.")
                                        .Replace("ФЕВ", ".02.")
                                        .Replace("MAR", ".03.")
                                        .Replace("МАР", ".03.")
                                        .Replace("APR", ".04.")
                                        .Replace("АПР", ".04.")
                                        .Replace("MAY", ".05.")
                                        .Replace("МАЙ", ".05.")
                                        .Replace("JUN", ".06.")
                                        .Replace("ИЮН", ".06.")
                                        .Replace("JUL", ".07.")
                                        .Replace("ИЮЛ", ".07.")
                                        .Replace("AUG", ".08.")
                                        .Replace("АВГ", ".08.")
                                        .Replace("SEP", ".09.")
                                        .Replace("СЕН", ".09.")
                                        .Replace("OCT", ".10.")
                                        .Replace("ОКТ", ".10.")
                                        .Replace("NOV", ".11.")
                                        .Replace("НОЯ", ".11.")
                                        .Replace("DEC", ".12.")
                                        .Replace("ДЕК", ".12.")
                                        ;
            DateTime infb = Convert.ToDateTime(infantInfo);
            textBox_BIRTHDAY.Text = infb.ToString("d");
        }

        public void ReUse(int Passanger_KEY)
        {
            ClearFields();
            this.PassangerKey = Passanger_KEY;
            passanger.Clear();
            adapter.SelectCommand.CommandText = @"SELECT     PASSNAME, PASSPASSPORT, RESTRICTION,ADDRESTRICTION,BPURESTRICTION,PassBirthDay,DatePassport
                    FROM         avia.avia.dbo.TICKET
                    WHERE     ID=" + Convert.ToString(Passanger_KEY);
            adapter.Fill(passanger);
            textBox_NUMTICKET.Text = Passanger_KEY.ToString();
            if (passanger.Rows.Count > 0)
            {
                DataRow Passanger = passanger.Rows[0];
                string[] spl;
                string Familia = "", Name = "", Otch = "", Titul = "";
                string passport = "";

                textBox_PASSNAME.Text = Convert.ToString(Passanger["PASSNAME"]);
                passport = Convert.ToString(Passanger["PASSPASSPORT"]);
                //RESTRICTION,ADDRESTRICTION
                textBox_RESTRICTION.Text = Convert.ToString(Passanger["RESTRICTION"]);
                textBox_ADDRESTRICTION.Text = Convert.ToString(Passanger["ADDRESTRICTION"]);
                textBox_BPURESTRICTION.Text = Convert.ToString(Passanger["BPURESTRICTION"]);
                textBox_DatePassport.Text = Convert.ToString(Passanger["DatePassport"]);
                textBox_PassBirthDay.Text = Convert.ToString(Passanger["PassBirthDay"]);

                textBox_PASSPASSPORT.Text = passport;
                spl = textBox_PASSNAME.Text.Split('/');
                Familia = spl[0];
                textBox_NAMERUS.Text = TranslitRUS(Familia);
                textBox_NAMELAT.Text = TranslitENG(Familia);
                if (spl.Length > 1)
                {
                    spl = spl[1].Split(' ');

                    Name = spl[0];
                    int aster = -1;
                    if ((aster = Name.IndexOf('*')) > 0)
                    {
                        Titul = Name.Substring(aster + 1);
                        Name = Name.Substring(0, aster);
                    }

                    //Нет титула, возможно он без пробела
                    foreach (string ttl in titules)
                    {
                        if (Name.EndsWith(ttl))
                        {
                            Name = Name.Replace(ttl, "");
                            Titul = Titul + ttl.Replace("(", "").Replace(")", "");
                        }
                    }

                    if (spl.Length == 2)
                    {
                        Titul = Titul + spl[1];
                    }
                    else
                        if (spl.Length == 3)
                        {

                            if (Titul.Length > 0)
                            {
                                Titul = Titul + spl[1] + spl[2];
                            }
                            else
                            {
                                Otch = spl[1];
                                Titul = spl[2];
                            }

                            if (Otch.StartsWith("MS")
                                || Otch.StartsWith("MRS")
                                || Otch.StartsWith("MISS")
                                || Otch.StartsWith("MR")
                                || Otch.StartsWith("Г-ЖА")

                                )
                            {
                                Titul = Otch + Titul;
                                Otch = "";

                            }
                        }
                        else if (spl.Length == 4)
                        {
                            Otch = spl[1];
                            if (Otch == "DOB")
                            {
                                Otch = "";
                                Titul = Titul + "DOB";
                            }
                            Titul =Titul + spl[2] + spl[3];
                        }


                    textBox_FNAMERUS.Text = TranslitRUS(Name);
                    textBox_SNAMERUS.Text = TranslitRUS(Otch);

                    textBox_FNAMELAT.Text = TranslitENG(Name);
                    textBox_SNAMELAT.Text = TranslitENG(Otch);
                    int sex = 0, RealSex = 0;

                    if (
                        Titul.Contains("MRS")
                        || Titul.Contains("Г-ЖА")
                        || Titul.Contains("MISS")
                        || (Titul.Contains("MS") && !Titul.Contains("MSTR"))
                        || Titul.Contains("RS")
                        )
                    {
                        sex = 1;
                        RealSex = 1;
                    }
                    DateTime birh;
                    if (DateTime.TryParse(textBox_PassBirthDay.Text, out birh))
                        textBox_BIRTHDAY.Text = birh.ToString();

                    Titul = Titul.Replace("(", "").Replace(")", "");
                    if (Titul.Contains("INF") || Titul.Contains("DOB") || Titul.Contains("CHD"))
                    {
                        int ind = 0;
                        if (Titul.Contains("CHD"))
                        {
                            sex = 2;
                            ind = Titul.IndexOf("CHD");
                        }
                        else
                            if (Titul.Contains("INF"))
                            {
                                sex = 3;
                                ind = Titul.IndexOf("INF");
                            }

                        if (textBox_RESTRICTION.Text.Contains("DOB"))
                        {
                            Titul = Titul + textBox_RESTRICTION.Text.Substring(textBox_RESTRICTION.Text.IndexOf("DOB"));
                        }
                        if (textBox_ADDRESTRICTION.Text.Contains("DOB"))
                        {
                            Titul = Titul + textBox_ADDRESTRICTION.Text.Substring(textBox_ADDRESTRICTION.Text.IndexOf("DOB"));
                        }
                        if (textBox_BPURESTRICTION.Text.Contains("DOB"))
                        {
                            Titul = Titul + textBox_BPURESTRICTION.Text.Substring(textBox_BPURESTRICTION.Text.IndexOf("DOB"));
                        }
                        if (Titul.Contains("DOB"))
                        {
                            ind = Titul.IndexOf("DOB");
                        }


                        string infantInfo = Titul.Substring(ind);
                        if (infantInfo.Length > 3)
                        {
                            if (infantInfo.Length == 9 && infantInfo.EndsWith("0"))
                                infantInfo = infantInfo + "0";
                            if (infantInfo.Length == 12 && (infantInfo.Substring(8, 2) == "19" || infantInfo.Substring(8, 2) == "20"))
                            {//Год указан 4мя цифрами
                                infantInfo = infantInfo.Substring(0, 8) + infantInfo.Substring(10, 2);
                            }
                            if (infantInfo.Length >= 10)
                                 ParseDate(infantInfo.Substring(3, 7));
                        }
                    }
                    if (sex != 1 && RealSex == 0 && textBox_NAMERUS.Text.EndsWith("а"))
                    {
                        RealSex = 1;
                        if (sex == 0)
                            sex = 1;
                    }
                    
                    textBox_SEX.Text = sex.ToString();
                    textBox_RealSex.Text = RealSex.ToString();

                }


                if (passport.Length == 0)
                {
                    passport = FindPassportData("PS");//PSP
                    /*
                    if (passport.Length == 0)
                    {
                        passport = FindPassportData("/P");
                        if (passport.Length > 0)
                            passport = passport.Substring(2);
                    }*/
                }
                if (passport.Length > 0)
                {

                    if (passport.StartsWith("PSPT"))
                    {
                        passport = passport.Replace("PSPT", "");
                    }
                    if (passport.StartsWith("PSPS"))
                    {
                        passport = passport.Replace("PSPS", "");
                    }
                    if (passport.StartsWith("PSP"))
                    {
                        passport = passport.Replace("PSP", "");
                    }
                    if (passport.StartsWith("PS"))
                    {
                        passport = passport.Replace("PS", "");
                    }
                    if (passport.StartsWith("NP"))
                    {
                        //национальный паспорт
                        passport = passport.Substring(2);
                    }
                    if (passport.StartsWith("ПСП"))
                    {
                        passport = passport.Replace("ПСП", "");
                    }

                    if (passport.StartsWith("ПС"))
                    {
                        passport = passport.Replace("ПС", "");
                    }


                    int ps = 0;
                    passport = passport.Replace("1EUR", "EUR");
                    if (textBox_BIRTHDAY.Text.Length ==0 && (ps = passport.IndexOf("RU"))>0)
                    {
                        if (passport.Length-ps>8)
                        ParseDate(passport.Substring(ps+2,7));
                    }
                    ps = 0;
                    foreach (Char pc in passport)
                        if (Char.IsDigit(pc) || pc == 'N' || pc == ' ')
                        {
                            ps++;
                            continue;
                        }
                        else
                            break;

                    if (ps > 7)
                        passport = passport.Substring(0, ps);

                    passport = passport.Replace("N", "").Replace(" ", "");

                    if (passport.Length == 10)//Внутренний
                    {
                        textBox_PASPRUSER.Text = passport.Substring(0, 4);
                        textBox_PASPRUNUM.Text = passport.Substring(4);
                    }
                    else//Заграничный
                    {//Серии бланков паспортов имеют цифровое обозначение, состоящее из двух разрядов:
                        //50 - 98 - паспорта граждан РФ; 10 - дипломатические паспорта; 20 - служебные паспорта.
                        if (passport.Length > 2)
                        {
                            textBox_PASPORTSER.Text = passport.Substring(0, 2);
                            textBox_PASPORTNUM.Text = passport.Substring(2);
                        }
                    }
                    DateTime passportDate;
                    if (DateTime.TryParse(textBox_DatePassport.Text, out passportDate))
                        textBox_PASPORTDATEEND.Text = passportDate.ToString("d");
                    else
                        textBox_PASPORTDATEEND.Text = "";

                }
            }
        }
        private string FindPassportData(string mask)
        {
            //textBox_ADDRESTRICTION.Text
            // textBox_RESTRICTION.Text
            // textBox_BPURESTRICTION.Text
            string passport = "";
            int ps = textBox_ADDRESTRICTION.Text.IndexOf(mask);
            if (ps > -1)
                passport = textBox_ADDRESTRICTION.Text.Substring(ps);
            else
            {
                ps = textBox_RESTRICTION.Text.IndexOf(mask);
                if (ps > -1)
                    passport = textBox_RESTRICTION.Text.Substring(ps);
                else
                {
                    ps = textBox_BPURESTRICTION.Text.IndexOf(mask);
                    if (ps > -1)
                        passport = textBox_BPURESTRICTION.Text.Substring(ps);
                }
            }
            return passport;
        }
        public void ReadDataToClient(DataRow dr)
        {
            //Строка клиента для считывания
            dr["CL_NAMERUS"] = textBox_NAMERUS.Text;
            dr["CL_FNAMERUS"] = textBox_FNAMERUS.Text;
            dr["CL_SNAMERUS"] = textBox_SNAMERUS.Text;
            dr["CL_NAMELAT"] = textBox_NAMELAT.Text;
            dr["CL_FNAMELAT"] = textBox_FNAMELAT.Text;
            dr["CL_SNAMELAT"] = textBox_SNAMELAT.Text;
            string initial = "";
            string ini = Convert.ToString(dr["CL_FNAMERUS"]);
            if (ini.Length > 0)
                initial = initial + ini.Substring(0, 1) + ".";
            ini = Convert.ToString(dr["CL_SNAMERUS"]);
            if (ini.Length > 0)
                initial = initial + ini.Substring(0, 1) + ".";
            dr["CL_SHORTNAME"] = initial;// инициалы
            int sex;
            if (Int32.TryParse(textBox_SEX.Text,out sex))
                dr["CL_SEX"] = sex;
            if (Int32.TryParse(textBox_RealSex.Text, out sex))
                dr["CL_RealSex"] = sex;           

            dr["CL_PASPRUSER"] = textBox_PASPRUSER.Text;
            dr["CL_PASPRUNUM"] = textBox_PASPRUNUM.Text;
            dr["CL_PASPORTSER"] = textBox_PASPORTSER.Text;
            dr["CL_PASPORTNUM"] = textBox_PASPORTNUM.Text;

            DateTime CL_BIRTHDAY;
            if (DateTime.TryParse(textBox_BIRTHDAY.Text, out CL_BIRTHDAY))
                dr["CL_BIRTHDAY"] = CL_BIRTHDAY;

            DateTime PASPORTDATEEND;
            if (DateTime.TryParse(textBox_PASPORTDATEEND.Text, out PASPORTDATEEND))
                dr["CL_PASPORTDATEEND"] = PASPORTDATEEND;

        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private string TranslitENG(string INText)
        {
            string ret = INText.ToUpper().TrimStart(' ')
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("?", "")
                 .Replace("  ", " ")
                 .Replace("А", "A")
                 .Replace("Б", "B")
                 .Replace("В", "V")
                 .Replace("Г", "G")
                 .Replace("Д", "D")
                 .Replace("Е", "E")
                 .Replace("Ё", "E")
                 .Replace("Ж", "ZH")
                 .Replace("З", "Z")
                 .Replace("И", "I")
                 .Replace("Й", "I")
                 .Replace("К", "K")
                 .Replace("Л", "L")
                 .Replace("М", "M")
                 .Replace("Н", "N")
                 .Replace("О", "O")
                 .Replace("П", "P")
                 .Replace("Р", "R")
                 .Replace("С", "S")
                 .Replace("Т", "T")
                 .Replace("У", "U")
                 .Replace("Ф", "F")
                 .Replace("Х", "H")
                 .Replace("Ц", "C")
                 .Replace("Ч", "CH")
                 .Replace("Ш", "SH")
                 .Replace("Щ", "SHCH")
                 .Replace("Ъ", "'")
                 .Replace("Ы", "Y")
                 .Replace("Ь", "'")
                 .Replace("Э", "E")
                 .Replace("Ю", "YU")
                 .Replace("Я", "YA")
                 .ToLower();
            if (ret.Length > 0)
            {
                Char start = Char.ToUpper(ret[0]);
                ret = ret.Remove(0, 1);
                ret = ret.Insert(0, start.ToString());
            }
            return ret;
        }
        private string TranslitRUS(string INText)
        {
            string ret = INText.ToUpper().TrimStart(' ');
            if (ret.EndsWith("ER"))
                ret = ret.TrimEnd('R').TrimEnd('E') + "р";
            if (ret.EndsWith("IA"))
                ret = ret.TrimEnd('A').TrimEnd('I') + "ия";
            if (ret.EndsWith("EY"))
                ret = ret.TrimEnd('Y').TrimEnd('E') + "ей";
            if (ret.EndsWith("AY"))
                ret = ret.TrimEnd('Y').TrimEnd('A') + "ай";
            if (ret.EndsWith("OY"))
                ret = ret.TrimEnd('Y').TrimEnd('O') + "ой";
            if (ret.EndsWith("IY"))
                ret = ret.TrimEnd('Y').TrimEnd('I') + "ий";
            if (ret.EndsWith("YY"))
                ret = ret.TrimEnd('Y') + "ый";
            if (ret.EndsWith("Y"))
                ret = ret.TrimEnd('Y') + "ий";

            ret =
            ret.Replace("(", "")
               .Replace(")", "")
               .Replace("?", "")
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
               .Replace("VICTOR", "Виктор")
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
               .Replace("J", "Дж")
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

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button_Continue_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }
        public void ConnectToClient(int CLkey)
        {
            //Создание привязки
            DataTable Lanta_ClientPassangerLink = new DataTable("Lanta_ClientPassangerLink");
            adapter.SelectCommand.CommandText = @"SELECT     CPL_ID, CPL_ClientКеу, CPL_PassangerКеу, CPL_MANAGER, CPL_UPDATE
            FROM         Lanta_ClientPassangerLink
            WHERE     CPL_ClientКеу = " + CLkey.ToString() + " AND CPL_PassangerКеу =  " + PassangerKey.ToString();
            adapter.Fill(Lanta_ClientPassangerLink);
            DataRow dr;

            if (Lanta_ClientPassangerLink.Rows.Count == 0)
            {
                dr = Lanta_ClientPassangerLink.NewRow();
                dr["CPL_ClientКеу"] = CLkey;
                dr["CPL_PassangerКеу"] = PassangerKey;
            }
            else
            {//Уже добавлен
                dr = Lanta_ClientPassangerLink.Rows[0];
            }
            dr["CPL_MANAGER"] = MANAGER_ID;
            dr["CPL_UPDATE"] = DateTime.Now;
            if (Lanta_ClientPassangerLink.Rows.Count == 0)
                Lanta_ClientPassangerLink.Rows.Add(dr);
            builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.UpdateCommand.CommandText = "UPDATE [Lanta_ClientPassangerLink] SET [CPL_ClientКеу] = @p1, [CPL_PassangerКеу] = @p2, [CPL_MANAGER] = @p3, [CPL_UPDATE] = @p4 WHERE ([CPL_ID] = @p5)";
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.Update(Lanta_ClientPassangerLink);


            EditClient ec = new EditClient(CLkey, MANAGER_ID, connection, false, false);
            ec.UpdateStatistic();
            ec.SaveClient(false);

        }
        public int TakeClientToConnect()
        {
            string selCommand = @"SELECT CL_KEY,CL_NAMERUS,CL_FNAMERUS FROM Clients";
            string filter;
            if (textBox_PASPORTSER.Text.Length > 0)
                filter = " WHERE CL_PASPORTSER = '" + textBox_PASPORTSER.Text + "' AND CL_PASPORTNUM='" + textBox_PASPORTNUM.Text + "'";
            else
                if (textBox_PASPORTSER.Text.Length > 0)
                    filter = " WHERE CL_PASPRUSER = '" + textBox_PASPRUSER.Text + "' AND CL_PASPRUNUM='" + textBox_PASPRUNUM.Text + "'";
                else filter = " WHERE 1=2";

            Clients.Clear();
            adapter.SelectCommand.CommandText = selCommand + filter;
            adapter.Fill(Clients);


            bool auto = false;
            if (Clients.Rows.Count == 1)//Клиент для привязки найден
            {
                if (Convert.ToString(Clients.Rows[0]["CL_NAMERUS"]) != textBox_NAMERUS.Text
                    || Convert.ToString(Clients.Rows[0]["CL_FNAMERUS"]) != textBox_FNAMERUS.Text)
                {
                    auto = false;
                }
                else
                {
                    auto = true;
                    ConnectToClient(Convert.ToInt32(Clients.Rows[0]["CL_KEY"]));
                }

            }

            if (auto)
                return 1;
            else
            {
                auto = true;//Установка с показом диалога привязки или без
                ClientsMainForm cl = new ClientsMainForm(textBox_NAMERUS.Text, MANAGER_ID, connection, this);
                DialogResult dres = DialogResult.OK;
                cl.SetButtonSelectText("Привязать к пассажиру", "Привязка постоянного клиента к пассажиру " + textBox_PASSNAME.Text + " " + textBox_PASSPASSPORT.Text);
                if (auto)
                {
                    cl.AddClient();
                    dres = cl.DialogResult;
                }
                else
                    dres = cl.ShowDialog();
                
                if (dres == DialogResult.OK)
                {
                    ConnectToClient(cl.return_CL_KEY);
                    return 1;
                }
                else
                    if (dres == DialogResult.Ignore)
                        return 2;
                    else//Отказ от создания или выбора
                        return 0;
            }

        }


    }
}
