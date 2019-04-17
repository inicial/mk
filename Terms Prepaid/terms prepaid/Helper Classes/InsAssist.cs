using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Word;
using terms_prepaid.Helper_Classes;
using lanta.SQLConfig;
using terms_prepaid.Helpers;
using AlfaIns;


namespace terms_prepaid
{
    public delegate void SetStatusDelegate(string status_text);
    public delegate void ShowMessageDelegate(string message_text);

    //====================================================================================================
    #region PolicyRecord
    //----------------------------------------------------------------------------------------------------
    public class PolicyRecord : object
    {
        //....................................................................................................
        public string INS_Numder { get; set; }
        public DateTime INS_Date { get; set; }
        public int INS_Status { get; set; }
        public string StatusText { get; set; }
        public DateTime INS_DateBegin { get; set; }
        public DateTime INS_DateEnd { get; set; }
        public int INS_Duration { get; set; }

        //....................................................................................................
        public string Number
        {
            get { return INS_Numder; }
            set { INS_Numder = value; }
        }
        public string Status
        {
            get { return StatusText; }
            set { StatusText = value; }
        }
        public string DateBegin
        {
            get
            {
                if (INS_DateBegin == null) return "";
                if (INS_DateBegin.Year < 2000) return "";
                return INS_DateBegin.ToString("dd.MM.yy");
            }
            set { }
        }
        public string DateEnd
        {
            get
            {
                if (INS_DateEnd == null) return "";
                if (INS_DateEnd.Year < 2000) return "";
                return INS_DateEnd.ToString("dd.MM.yy");
            }
            set { }
        }
        public string Duration
        {
            get { if (INS_Duration > 0) return INS_Duration.ToString(); else return ""; }
            set { }
        }
        //....................................................................................................
    }
    //----------------------------------------------------------------------------------------------------
    #endregion // PolicyRecord
    //====================================================================================================
    #region TouristRecord
    //----------------------------------------------------------------------------------------------------
    public class TouristRecord : object
    {
        //....................................................................................................
        public int TU_KEY { get; set; }
        public string TOURIST_NAME { get; set; }
        public string DG_CODE { get; set; }
        public DateTime TU_BIRTHDAY { get; set; }
        public double DG_NDAY { get; set; }
        public DateTime DG_TURDATE { get; set; }
        public string TU_PHONE { get; set; }
        public string TU_PASPORTNUM { get; set; }
        public string TU_PASPORTTYPE { get; set; }
        public string INS_NUMBER { get; set; }
        public int AGE { get; set; }

        //....................................................................................................
        public string PolicyNumber
        {
            get { return INS_NUMBER; }
            set { INS_NUMBER = value; }
        }
        public string PhoneNUmber
        {
            get { return TU_PHONE; }
            set { TU_PHONE = value; }
        }
        public string PassportNumber
        {
            get { return TU_PASPORTNUM; }
            set { TU_PASPORTNUM = value; }
        }
        //....................................................................................................
    }
    //----------------------------------------------------------------------------------------------------
    #endregion // TouristRecord
    //====================================================================================================
    #region ServiceRecord
    //----------------------------------------------------------------------------------------------------
    public class ServiceRecord : object
    {
        public int DL_KEY { get; set; }
        public string DL_DGCOD { get; set; }
        public int DL_DAY { get; set; }
        public int tu_key { get; set; }
        public string TU_NAMELAT { get; set; }
        public string TU_FNAMELAT { get; set; }
        public DateTime TU_BIRTHDAY { get; set; }
        public int DL_CODE { get; set; }
        public int AC_slkey { get; set; }
        public int DL_NDAYS { get; set; }
        public double AC_Coef { get; set; }
        public string A1_NAME { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    #endregion // ServiceRecord
    //====================================================================================================
    #region ContractRecord
    //----------------------------------------------------------------------------------------------------
    public class ContractRecord : object
    {
        public string DG_CODE { get; set; }
        public DateTime DG_TURDATE { get; set; }
        public int DG_NMEN { get; set; }
        public double DG_PRICE { get; set; }
        public double DG_NDAY { get; set; }
        public string DG_MAINMEN { get; set; }
        public string DG_MAINMENPHONE { get; set; }
        public string DG_MAINMENPASPORT { get; set; }
        public string DG_RATE { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    #endregion // ContractRecord
    //====================================================================================================
    #region ParamsRecord
    //----------------------------------------------------------------------------------------------------
    public class ParamsRecord : object
    {
        public string ParamsString { get; set; }

        public ParamsRecord ( string iParamsString = "" )
        {
            ParamsString = iParamsString;
        }

        public void DecodeParams()
        {
            DecodeParams ( ParamsString );
        }

        public void DecodeParams ( string params_string )
        {


        }
    }
    //----------------------------------------------------------------------------------------------------
    #endregion // ParamsRecord
    //====================================================================================================
    #region InsPerson
    //----------------------------------------------------------------------------------------------------
    public class InsPerson
    {
        //----------------------------------------------------------------------------------------------------
        public string Name;
        public DateTime birhtDay;
        public string Passport;
        public int tu_key;

        public decimal sumIns;
        public decimal sumMed;
        public decimal sumMedRb;
        public decimal deductible;
        public decimal sumVal;
        public decimal sumRb;

        //----------------------------------------------------------------------------------------------------
        public InsPerson(string iName, DateTime birday, int tukey)
        {
            Name = iName;
            birhtDay = birday;
            tu_key = tukey;

            sumIns = 0;
            deductible = 0;
            sumRb = 0;
        }

        //----------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------
    #endregion // InsPerson
    //====================================================================================================
    #region InsRisk
    //----------------------------------------------------------------------------------------------------
    public class InsRisk
    {
        //----------------------------------------------------------------------------------------------------
        public string Name;
        public double Summa;
        public string Currency;

        //----------------------------------------------------------------------------------------------------
        public InsRisk(string name, double summa, string currency)
        {
            Name = name;
            Summa = summa;
            Currency = currency;
        }

        //----------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------
    #endregion // InsRisk
    //====================================================================================================
    #region InsAssist
    //----------------------------------------------------------------------------------------------------
    public class InsAssist
    {
        //----------------------------------------------------------------------------------------------------
        #region Properties
        //----------------------------------------------------------------------------------------------------

        public List<InsPerson> persons;
        public List<InsRisk> risks;
        //public List<string> risks;

        public string nomber;
        public string policyUID;
        public DateTime dateIsue;
        public DateTime dateFrom;
        public DateTime dateTo;
        public string holder;
        public string passport;
        public DateTime holderBirthday;
        public string tel;
        public string terretory;
        public string country;
        public int country_id;
        public int days;
        public decimal medicalsum;
        public decimal acidentsum;
        public decimal tripsum;
        public decimal bagsum;
        public decimal fligsum;
        public decimal medicalfran;
        public decimal acidentfran;
        public decimal tripfran;
        public decimal bagfran;
        public decimal fligfran;
        public decimal medicalprem;
        public decimal acidentprem;
        public decimal tripprem;
        public decimal bagprem;
        public decimal fligprem;
        public decimal totalsum;
        public decimal totalsumRb;
        public decimal medicalpremRb;
        public decimal acidentpremRb;
        public decimal trippremRb;
        public decimal bagpremRb;
        public decimal fligpremRb;
        public string program;
        public string dop = "";
        public string curens;
        public string DG_code;
        //private SqlConnection _connection;

        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //----------------------------------------------------------------------------------------------------
        #region InsAssist
        //----------------------------------------------------------------------------------------------------
        public InsAssist()
        {
            //_connection = con;

            List<InsPerson> persons = new List<InsPerson>();
            List<InsRisk> risks = new List<InsRisk>();
            //List<string> risks = new List<string>();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsAssist
        //----------------------------------------------------------------------------------------------------
        #region IsNull
        //----------------------------------------------------------------------------------------------------
        string IsNull(decimal value)
        {
            if ((value == 0) || (value == null)) return "--";

            return value.ToString("N2");
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // IsNull
        //----------------------------------------------------------------------------------------------------
        #region ReplaceInDoc
        //----------------------------------------------------------------------------------------------------
        /*
        void ReplaceInDoc(Document oDoc, string find, string replace)
        {
            var range = oDoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: find, ReplaceWith: replace, Replace: WdReplace.wdReplaceAll);
        }
        */
        //----------------------------------------------------------------------------------------------------
        #endregion // ReplaceInDoc
        //----------------------------------------------------------------------------------------------------
        #region PrintPolicy
        //----------------------------------------------------------------------------------------------------
        public string PrintPolicy(bool dialog_flag)
        {
            /*
            //....................................................................................................
            //обьект пустого значения
            Object wMissing = System.Reflection.Missing.Value;
            //обьекты true  и  false
            Object wTrue = true;
            Object wFalse = false;

            //....................................................................................................
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document oDoc = new Microsoft.Office.Interop.Word.Document();
            //app.Visible = true;
            Object docPath = Environment.CurrentDirectory + "\\..\\Shablon\\uralsib.dot";
            // oDoc.Protect(WdProtectionType.OnlyReading);
            oDoc.Password = "yuiyu";
            oDoc = app.Documents.Add(ref docPath, ref wMissing, ref wTrue, ref wFalse);

            //....................................................................................................
            //Замена переменных

            ReplaceInDoc(oDoc, "<--number-->", this.nomber);
            ReplaceInDoc(oDoc, "<--cur-->", this.curens);
            ReplaceInDoc(oDoc, "<--date-->", this.dateIsue.Date.ToString().Substring(0, 10));
            ReplaceInDoc(oDoc, "<--holder-->", this.holder);
            ReplaceInDoc(oDoc, "<--passport-->", this.passport);
            ReplaceInDoc(oDoc, "<--phone-->", this.tel);
            ReplaceInDoc(oDoc, "<--terretory-->", this.terretory);
            ReplaceInDoc(oDoc, "<--datefrom-->", this.dateFrom.Date.ToString().Substring(0, 10));
            ReplaceInDoc(oDoc, "<--dateto-->", this.dateTo.Date.ToString().Substring(0, 10));
            ReplaceInDoc(oDoc, "<--days-->", this.days.ToString());
            ReplaceInDoc(oDoc, "<--dop-->", this.dop);
            int count = this.persons.Count;
            while (this.persons.Count < 4)
            {
                this.persons.Add(new InsPerson("", DateTime.Now, 0));
            }
            for (int i = 1; i <= 4; i++)
            {
                ReplaceInDoc(oDoc, "<--person" + i.ToString() + "-->", this.persons[i - 1].Name);
                if (this.persons[i - 1].Name == string.Empty)
                {
                    ReplaceInDoc(oDoc, "<--birth" + i.ToString() + "-->", "");
                    ReplaceInDoc(oDoc, "<--sum" + i.ToString() + "-->", "");
                    ReplaceInDoc(oDoc, "<--prem" + i.ToString() + "-->", "");
                    ReplaceInDoc(oDoc, "<--rb" + i.ToString() + "-->", "");
                    ReplaceInDoc(oDoc, "<--cur" + i.ToString() + "-->", "");
                }
                else
                {
                    ReplaceInDoc(oDoc, "<--birth" + i.ToString() + "-->", this.persons[i - 1].birhtDay.Date.ToString().Substring(0, 10));
                    ReplaceInDoc(oDoc, "<--sum" + i.ToString() + "-->", IsNull(this.persons[i - 1].sumIns));
                    ReplaceInDoc(oDoc, "<--prem" + i.ToString() + "-->", IsNull(this.persons[i - 1].sumVal));
                    ReplaceInDoc(oDoc, "<--rb" + i.ToString() + "-->", IsNull(this.persons[i - 1].sumRb));
                    if (IsNull(this.persons[i - 1].sumIns) == "--")
                    {
                        ReplaceInDoc(oDoc, "<--cur" + i.ToString() + "-->", "");
                    }
                    else
                    {
                        ReplaceInDoc(oDoc, "<--cur" + i.ToString() + "-->", this.curens);
                    }

                }

            }
            while (this.persons.Count > count)
            {
                this.persons.Remove(persons.Last());
            }

            ReplaceInDoc(oDoc, "<--prog-->", this.program);
            ReplaceInDoc(oDoc, "<--total-->", this.totalsum.ToString("N2"));
            ReplaceInDoc(oDoc, "<--totalrb-->", this.totalsumRb.ToString("N2"));

            ReplaceInDoc(oDoc, "<--med-->", IsNull(this.medicalsum));
            ReplaceInDoc(oDoc, "<--trip-->", IsNull(this.tripsum));
            ReplaceInDoc(oDoc, "<--lug-->", IsNull(this.bagsum));
            ReplaceInDoc(oDoc, "<--lia-->", IsNull(this.fligsum));

            ReplaceInDoc(oDoc, "<--medpr-->", IsNull(this.medicalprem));
            ReplaceInDoc(oDoc, "<--trippr-->", IsNull(this.tripprem));
            ReplaceInDoc(oDoc, "<--lugpr-->", IsNull(this.bagprem));
            ReplaceInDoc(oDoc, "<--liapr-->", IsNull(this.fligprem));

            ReplaceInDoc(oDoc, "<--medrb-->", IsNull(this.medicalpremRb));
            ReplaceInDoc(oDoc, "<--triprb-->", IsNull(this.trippremRb));
            ReplaceInDoc(oDoc, "<--lugrb-->", IsNull(this.bagpremRb));
            ReplaceInDoc(oDoc, "<--liarb-->", IsNull(this.fligpremRb));
            ReplaceInDoc(oDoc, "-- " + this.curens, "--");

            //....................................................................................................
            //Сохранение в фаил

            SaveFileDialog file_dialog = new SaveFileDialog();
            file_dialog.Filter = "Pdf|*.pdf";
            file_dialog.DefaultExt = ".pdf";
            file_dialog.FileName = this.nomber.Replace('/', ' ');
            string fileName;
            if (dialog_flag)
            {
                if (file_dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = file_dialog.FileName;
                }
                else
                {
                    fileName = "d:\\" + this.nomber.Replace('/', ' ') + ".pdf";
                }
            }
            else
            {
                if (!Directory.Exists("d:\\Insurense\\"))
                {
                    Directory.CreateDirectory("d:\\Insurense\\");
                }
                fileName = "d:\\Insurense\\" + this.nomber.Replace('/', ' ') + ".pdf";
            }

            oDoc.SaveAs(fileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);

            //....................................................................................................
            oDoc.Close(false);
            app.Quit(false);

            return fileName;
            */
            return "";
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // PrintPolicy
        //----------------------------------------------------------------------------------------------------
    }

    //----------------------------------------------------------------------------------------------------
    #endregion // InsAssist
    //====================================================================================================
    #region AlfaInsAssist
    //----------------------------------------------------------------------------------------------------
    public static class AlfaInsAssist
    {
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------
        public static string URS_InsuranceTable = "URS_Insurance";
        public static string ALF_InsuranceTable = "ALF_Insurance";
        public static string InsuranceTable = ALF_InsuranceTable;

        public static SetStatusDelegate SetStatusCallback;
        public static ShowMessageDelegate ShowMessageCallback;

        private static TI_Service AlfaService;
        private static string AlfaUserLogin = "";
        private static string AlfaUserPassword = "";
        private static string AlfaAgentUid = "";
        private static string AlfaPolicyLink = "";

        public static InsAssist Insist;
        public static Decimal CurrencyCourse;

        private static Config_XML LantaConfig;

        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region AlfaIns data access
        //----------------------------------------------------------------------------------------------------
        #region ProgramList
        //----------------------------------------------------------------------------------------------------
        public static List<TI_Program> ProgramList
        {
            get { if (AlfaService != null) return AlfaService.ProgramList; else return null; }
            set { if (AlfaService != null) AlfaService.ProgramList = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ProgramList
        //----------------------------------------------------------------------------------------------------
        #region CurrencyList
        //----------------------------------------------------------------------------------------------------
        public static List<TI_Currency> CurrencyList
        {
            get { if (AlfaService != null) return AlfaService.CurrencyList; else return null; }
            set { if (AlfaService != null) AlfaService.CurrencyList = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CurrencyList
        //----------------------------------------------------------------------------------------------------
        #region TerritoryList
        //----------------------------------------------------------------------------------------------------
        public static List<TI_Territory> TerritoryList
        {
            get { if (AlfaService != null) return AlfaService.TerritoryList; else return null; }
            set { if (AlfaService != null) AlfaService.TerritoryList = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // TerritoryList
        //----------------------------------------------------------------------------------------------------
        #region AlfaPolicyList
        //----------------------------------------------------------------------------------------------------
        public static List<TI_Policy> AlfaPolicyList
        {
            get { if (AlfaService != null) return AlfaService.PolicyList; else return null; }
            set { if (AlfaService != null) AlfaService.PolicyList = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AlfaPolicyList
        //----------------------------------------------------------------------------------------------------
        #region StruhSumList
        //----------------------------------------------------------------------------------------------------
        public static List<TI_Sum> StruhSumList
        {
            get { if (AlfaService != null) return AlfaService.StruhSumList; else return null; }
            set { if (AlfaService != null) AlfaService.StruhSumList = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // StruhSumList
        //----------------------------------------------------------------------------------------------------
        #region Get_Program(int program_id)
        //----------------------------------------------------------------------------------------------------
        private static TI_Program Get_Program(int program_id)
        {
            foreach (TI_Program program in ProgramList)
            {
                if (program.ID == program_id) return program;
            }
            return null;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Get_Program
        //----------------------------------------------------------------------------------------------------
        #region Get_Program_ByName
        //----------------------------------------------------------------------------------------------------
        public static TI_Program Get_Program_ByName(string program_name)
        {
            foreach (TI_Program program in ProgramList)
            {
                if (program.Name == program_name) return program;
            }
            return null;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Get_Program_ByName
        //----------------------------------------------------------------------------------------------------
        #region Get_Program_ByUID
        //----------------------------------------------------------------------------------------------------
        private static TI_Program Get_Program_ByUID(string program_UID)
        {
            foreach (TI_Program program in ProgramList)
            {
                if (program.UID == program_UID) return program;
            }
            return null;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Get_Program_ByUID
        //----------------------------------------------------------------------------------------------------
        #region  Get_ProgramRisk
        //----------------------------------------------------------------------------------------------------
        public static TI_Risk Get_ProgramRisk(string program_UID, string risk_name)
        {
            foreach (TI_Program program in ProgramList)
            {
                if (program.UID == program_UID)
                {
                    foreach (TI_Risk risk in program.RiskList)
                    {
                        if (risk.Name.ToUpper().IndexOf(risk_name.ToUpper()) >= 0) return risk;
                    }
                    return null;
                }
            }
            return null;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Get_Program_ByUIN
        //----------------------------------------------------------------------------------------------------
        #region Get_Territory
        //----------------------------------------------------------------------------------------------------
        public static TI_Territory Get_Territory(string territory_name)
        {
            foreach (TI_Territory terr in TerritoryList)
            {
                if (terr.Name == territory_name) return terr;
            }
            return null;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Get_Program_ByName
        //----------------------------------------------------------------------------------------------------
        #endregion // AlfaIns data access
        //====================================================================================================
        #region Data access - App data structures
        //----------------------------------------------------------------------------------------------------
        #region GetTourists
        //----------------------------------------------------------------------------------------------------
        public static List<TouristRecord> GetTourists(string DGCode)
        {
            List<TouristRecord> TouristList = new List<TouristRecord>();
            if (string.IsNullOrEmpty(DGCode)) return TouristList;

            string query = @"SELECT DISTINCT TU_KEY, DG_CODE, TU_NAMELAT+' '+TU_FNAMELAT as TOURIST_NAME ";
            query = query + "  ,TU_BIRTHDAY,DG_NDAY,DG_TURDATE,TU_PHONE,TU_PASPORTNUM,TU_PASPORTTYPE ";
            query = query + "  ,isnull(ins_numder,'') as INS_NUMBER ";
            query = query + "  ,dbo.GetYears(TU_BIRTHDAY,DG_TURDATE + DG_NDAY - 1) as AGE ";
            query = query + "  FROM tbl_Turist INNER JOIN tbl_Dogovor ON tbl_Turist.TU_DGCOD = tbl_Dogovor.DG_CODE ";
            query = query + "  LEFT JOIN " + InsuranceTable + " ON INS_tukey=TU_KEY and INS_Status=1 ";
            query = query + "  WHERE DG_code = @dgcode";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@dgcode", DGCode);
            DataTable dt_Tour = new DataTable();

            try
            {
                adapter.Fill(dt_Tour);
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }

            if (dt_Tour == null) return TouristList;
            if (dt_Tour.Rows.Count == 0) return TouristList;

            //....................................................................................................
            for (int i = 0; i < dt_Tour.Rows.Count; i++)
            {
                DataRow row = dt_Tour.Rows[i];
                TouristRecord tourist = new TouristRecord();

                try
                {
                    tourist.TU_KEY = row.Field<int>("TU_KEY");
                    tourist.TOURIST_NAME = row.Field<string>("TOURIST_NAME");
                    tourist.DG_CODE = row.Field<string>("DG_CODE");
                    tourist.TU_BIRTHDAY = row.Field<DateTime>("TU_BIRTHDAY");
                    tourist.DG_NDAY = row.Field<double>("DG_NDAY");
                    tourist.DG_TURDATE = row.Field<DateTime>("DG_TURDATE");
                    tourist.TU_PHONE = row.Field<string>("TU_PHONE");
                    tourist.TU_PASPORTNUM = row.Field<string>("TU_PASPORTNUM");
                    tourist.TU_PASPORTTYPE = row.Field<string>("TU_PASPORTTYPE");
                    tourist.INS_NUMBER = row.Field<string>("INS_NUMBER");
                    tourist.AGE = row.Field<int>("AGE");

                    TouristList.Add(tourist);
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }

            return TouristList;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetTourists
        //----------------------------------------------------------------------------------------------------
        #region GetPolicies --
        //----------------------------------------------------------------------------------------------------
        public static List<PolicyRecord> GetPolicies(string DGCode)
        {
            List<PolicyRecord> PolicyList = new List<PolicyRecord>();
            if (string.IsNullOrEmpty(DGCode)) return PolicyList;

            string query = @"SELECT distinct [INS_Numder], [INS_Date], CONVERT(integer, [INS_Status]) AS [INS_Status] ";
            query = query + " ,case when INS_Status=1 then 'Выписана' when INS_Status=0 then 'Аннулирована' end as [StatusText] ";
            query = query + " ,[INS_DateBegin], [INS_DateEnd], [INS_Duration] ";
            query = query + " FROM [dbo].[" + InsuranceTable + "] WHERE INS_DGCode=@dgcode";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@dgcode", DGCode);
            DataTable dt_Pol = new DataTable();

            try
            {
                adapter.Fill(dt_Pol);
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }

            if (dt_Pol == null) return PolicyList;
            if (dt_Pol.Rows.Count == 0) return PolicyList;

            //....................................................................................................
            for (int i = 0; i < dt_Pol.Rows.Count; i++)
            {
                DataRow row = dt_Pol.Rows[i];
                PolicyRecord policy = new PolicyRecord();

                try
                {
                    policy.INS_Numder = row.Field<string>("INS_Numder");
                    policy.INS_Date = row.Field<DateTime>("INS_Date");
                    policy.INS_Status = row.Field<int>("INS_Status");
                    policy.StatusText = row.Field<string>("StatusText");
                    policy.INS_DateBegin = row.Field<DateTime>("INS_DateBegin");
                    policy.INS_DateEnd = row.Field<DateTime>("INS_DateEnd");
                    policy.INS_Duration = row.Field<int>("INS_Duration");

                    PolicyList.Add(policy);
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }

            return PolicyList;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetPolicies
        //----------------------------------------------------------------------------------------------------
        #region GetContracts
        //----------------------------------------------------------------------------------------------------
        public static List<ContractRecord> GetContracts(string DGCode)
        {
            List<ContractRecord> ContractList = new List<ContractRecord>();
            if (string.IsNullOrEmpty(DGCode)) return ContractList;

            string query = @"SELECT [DG_CODE],[DG_TURDATE],[DG_NMEN],[DG_PRICE],[DG_NDAY] ";
            query = query + " ,[DG_MAINMEN],[DG_MAINMENPHONE],[DG_MAINMENPASPORT],[DG_RATE] ";
            query = query + " FROM [dbo].[tbl_Dogovor] ";
            query = query + " WHERE DG_CODE=@dgcode";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            com.Parameters.AddWithValue("@dgcode", DGCode);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt_Contr = new DataTable();

            try
            {
                adapter.Fill(dt_Contr);
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }

            if (dt_Contr == null) return ContractList;
            if (dt_Contr.Rows.Count == 0) return ContractList;

            //....................................................................................................
            for (int i = 0; i < dt_Contr.Rows.Count; i++)
            {
                DataRow row = dt_Contr.Rows[i];
                ContractRecord contract = new ContractRecord();

                try
                {
                    contract.DG_CODE = row.Field<string>("DG_CODE");
                    contract.DG_TURDATE = row.Field<DateTime>("DG_TURDATE");
                    contract.DG_NMEN = row.Field<short>("DG_NMEN");
                    contract.DG_PRICE = (double)row.Field<decimal>("DG_PRICE");
                    contract.DG_NDAY = row.Field<double>("DG_NDAY");
                    contract.DG_MAINMEN = row.Field<string>("DG_MAINMEN");
                    contract.DG_MAINMENPHONE = row.Field<string>("DG_MAINMENPHONE");
                    contract.DG_MAINMENPASPORT = row.Field<string>("DG_MAINMENPASPORT");
                    contract.DG_RATE = row.Field<string>("DG_RATE");

                    ContractList.Add(contract);
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }

            return ContractList;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetContracts
        //----------------------------------------------------------------------------------------------------
        #region GetServicies
        //----------------------------------------------------------------------------------------------------
        public static List<ServiceRecord> GetServicies(string DGCode)
        {
            List<ServiceRecord> ServicetList = new List<ServiceRecord>();
            if (string.IsNullOrEmpty(DGCode)) return ServicetList;

            string query = @"SELECT DL_KEY,DL_DGCOD,DL_NAME,DL_DAY,DL_CODE,DL_SUBCODE1 ,DL_SVKEY,DL_NDAYS, ";
            query = query + " tu_key,TU_TURDATE,TU_NAMELAT,TU_FNAMELAT,TU_BIRTHDAY, ";
            query = query + " TU_SEX,TU_RealSex,DL_COST,A1_NAME,AC_Coef,AC_slkey ";
            query = query + " FROM TuristService INNER JOIN tbl_Turist ON TU_TUKEY = TU_KEY ";
            query = query + "   INNER JOIN tbl_DogovorList ON DL_KEY = TU_DLKEY ";
            query = query + "   INNER JOIN AddDescript1 ON DL_SUBCODE1=A1_KEY ";
            query = query + "   INNER JOIN INS_AgeCoef ON DATEDIFF(day,TU_BIRTHDAY,DL_TURDATE)/365 between AC_AgeFrom and AC_AgeTo ";
            query = query + " WHERE DL_SVKEY = 6 AND DL_DGCOD = @dgcode ";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            com.Parameters.AddWithValue("@dgcode", DGCode);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt_Service = new DataTable();

            try
            {
                adapter.Fill(dt_Service);
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }

            if (dt_Service == null) return ServicetList;
            if (dt_Service.Rows.Count == 0) return ServicetList;

            //....................................................................................................
            for (int i = 0; i < dt_Service.Rows.Count; i++)
            {
                DataRow row = dt_Service.Rows[i];
                ServiceRecord service = new ServiceRecord();

                try
                {
                    service.DL_KEY = row.Field<int>("DL_KEY");
                    service.DL_DGCOD = row.Field<string>("DL_DGCOD");
                    service.DL_DAY = row.Field<Int16>("DL_DAY");
                    service.tu_key = row.Field<int>("tu_key");
                    service.TU_NAMELAT = row.Field<string>("TU_NAMELAT");
                    service.TU_FNAMELAT = row.Field<string>("TU_FNAMELAT");
                    service.TU_BIRTHDAY = row.Field<DateTime>("TU_BIRTHDAY");
                    service.DL_CODE = row.Field<int>("DL_CODE");
                    service.AC_slkey = row.Field<int>("AC_slkey");
                    service.DL_NDAYS = row.Field<Int16>("DL_NDAYS");
                    service.AC_Coef = row.Field<double>("AC_Coef");
                    service.A1_NAME = row.Field<string>("A1_NAME");

                    //DL_NAME,DL_DAY,DL_SUBCODE1,DL_SVKEY,TU_TURDATE,
                    //TU_SEX,TU_RealSex,DL_COST

                    ServicetList.Add(service);
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }

            return ServicetList;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetServicies
        //----------------------------------------------------------------------------------------------------
        #region GetCourse
        //----------------------------------------------------------------------------------------------------
        public static double GetCourse(string CourseRate)
        {
            if (String.IsNullOrEmpty(CourseRate)) return 0;

            string query = @"SELECT RC_COURSE_CB FROM RealCourses ";
            query = query + " WHERE RC_RCOD1='рб' AND RC_DATEBEG=@date AND RC_RCOD2=@code";
            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);

            com.Parameters.AddWithValue("@date", DateTime.Now.Date);
            com.Parameters.AddWithValue("@code", CourseRate);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt_Course = new DataTable();
            double course = 0;

            try
            {
                adapter.Fill(dt_Course);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            if (dt_Course.Rows.Count > 0)
                course = (double)dt_Course.Rows[0].Field<decimal>("RC_COURSE_CB");

            return course;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetCourse
        //----------------------------------------------------------------------------------------------------
        #region GetInsurance --
        //----------------------------------------------------------------------------------------------------
        public static InsAssist GetInsurance(string ins_number)
        {
            if (String.IsNullOrEmpty(ins_number)) return null;


            string query = @"SELECT * FROM [dbo].[" + InsuranceTable + "] WHERE INS_Numder=@number";
            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);

            com.Parameters.AddWithValue("@number", ins_number);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt_insurance = new DataTable();

            try
            {
                adapter.Fill(dt_insurance);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            if (dt_insurance.Rows.Count == 0) return null;

            //....................................................................................................
            InsAssist ins = new InsAssist();

            ins.nomber = dt_insurance.Rows[0].Field<string>("INS_Numder");
            ins.holder = dt_insurance.Rows[0].Field<string>("INS_Holder");
            ins.passport = dt_insurance.Rows[0].Field<string>("INS_PassportHolder");
            ins.tel = dt_insurance.Rows[0].Field<string>("INS_PhoneHolder");
            ins.curens = dt_insurance.Rows[0].Field<string>("INS_Currency");
            ins.holderBirthday = dt_insurance.Rows[0].Field<DateTime>("INS_BirthdayHolder");
            ins.dateIsue = dt_insurance.Rows[0].Field<DateTime>("INS_Date");
            ins.dateFrom = dt_insurance.Rows[0].Field<DateTime>("INS_DateBegin");
            ins.dateTo = dt_insurance.Rows[0].Field<DateTime>("INS_DateEnd");
            ins.days = dt_insurance.Rows[0].Field<int>("INS_Duration");
            ins.terretory = dt_insurance.Rows[0].Field<string>("INS_Country");
            ins.medicalsum = 50000;
            ins.tripsum = 0;
            ins.fligsum = 1000;
            ins.bagsum = 0;


            List<InsPerson> persons = new List<InsPerson>();
            foreach (DataRow row in dt_insurance.Rows)
            {
                InsPerson person = null;
                foreach (InsPerson i_person in persons)
                {
                    if (i_person.tu_key == row.Field<int>("INS_tukey")) person = i_person;
                }
                if (person == null)
                {
                    string name = row.Field<string>("INS_Person");
                    DateTime birthday = row.Field<DateTime>("INS_BirthdayPerson").Date;
                    int tukey = row.Field<int>("INS_tukey");
                    person = new InsPerson(name, birthday, tukey);
                    persons.Add(person);
                }
                string program = row.Field<string>("INS_Program");
                if (program == "B" || program == "C")
                {
                    ins.program = row.Field<string>("INS_Program");
                    ins.medicalsum = row.Field<decimal>("INS_Sum");

                    person.sumMed = row.Field<decimal>("INS_Prem");
                    person.sumMedRb = row.Field<decimal>("INS_PremRb");
                }
                if (program == "CTI")
                {
                    person.sumIns = row.Field<decimal>("INS_Sum");
                    person.sumRb = row.Field<decimal>("INS_PremRb");
                    person.sumVal = row.Field<decimal>("INS_Prem");
                }
            }
            ins.persons = persons;

            decimal med = 0, medrb = 0, total = 0, totalRb = 0;
            foreach (InsPerson i_person in persons)
            {
                med += i_person.sumMed;
                medrb += i_person.sumMedRb;
                total += i_person.sumMed + i_person.sumVal;
                totalRb += i_person.sumMedRb + i_person.sumRb;

            }
            ins.medicalprem = med;
            ins.medicalpremRb = medrb;
            ins.totalsum = total;
            ins.totalsumRb = totalRb;

            //....................................................................................................

            return ins;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetInsurance
        //----------------------------------------------------------------------------------------------------
        #endregion // Data access - App data structures
        //====================================================================================================
        #region Database methods
        //----------------------------------------------------------------------------------------------------
        #region AddInsurance
        //----------------------------------------------------------------------------------------------------
        public static bool AddInsurance(InsAssist Ins)
        {
            //....................................................................................................
            String query = @"INSERT INTO [" + InsuranceTable + "] (";
            query = query + "[INS_Numder],[INS_Holder],[INS_BirthdayHolder],[INS_Person],[INS_BirthdayPerson]";
            query = query + ",[INS_Country],[INS_Date],[INS_DateBegin],[INS_DateEnd],[INS_Duration],[INS_Dop]";
            query = query + ",[INS_Program],[INS_Code],[INS_Sum],[INS_Currency],[INS_Prem],[INS_CurruncyPrem]";
            query = query + ",[INS_PremRb],[INS_Rule],[INS_DateChange],[INS_Status],[INS_DGCode],[INS_PassportHolder]";
            query = query + ",[INS_PhoneHolder],[INS_TUKEY]";
            query = query + ") VALUES (";
            query = query + "@INS_Numder,@INS_Holder,@INS_BirthdayHolder,@INS_Person,@INS_BirthdayPerson";
            query = query + ",@INS_Country,@INS_Date,@INS_DateBegin,@INS_DateEnd,@INS_Duration,@INS_Dop";
            query = query + ",@INS_Program,@INS_Code,@INS_Sum,@INS_Currency,@INS_Prem,@INS_CurruncyPrem";
            query = query + ",@INS_PremRb,@INS_Rule,@INS_DateChange,@INS_Status,@INS_DGCode,@INS_PassportHolder";
            query = query + ",@INS_PhoneHolder,@ins_tukey)";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            com.Parameters.AddWithValue("@INS_Numder", Ins.nomber);
            com.Parameters.AddWithValue("@INS_Holder", Ins.holder);
            com.Parameters.AddWithValue("@INS_BirthdayHolder", Ins.holderBirthday);
            com.Parameters.AddWithValue("@INS_Country", Ins.terretory);
            com.Parameters.AddWithValue("@INS_Date", Ins.dateIsue);
            com.Parameters.AddWithValue("@INS_DateBegin", Ins.dateFrom);
            com.Parameters.AddWithValue("@INS_DateEnd", Ins.dateTo);
            com.Parameters.AddWithValue("@INS_Duration", Ins.days);
            com.Parameters.AddWithValue("@INS_Dop", Ins.dop);
            com.Parameters.AddWithValue("@INS_Code", "ST");
            com.Parameters.AddWithValue("@INS_Currency", Ins.curens);
            com.Parameters.AddWithValue("@INS_CurruncyPrem", Ins.curens);
            com.Parameters.AddWithValue("@INS_Rule", "019");
            com.Parameters.AddWithValue("@INS_DateChange", DateTime.Now);
            com.Parameters.AddWithValue("@INS_Status", true);
            com.Parameters.AddWithValue("@INS_DGCode", Ins.DG_code);
            com.Parameters.AddWithValue("@INS_PassportHolder", Ins.passport);
            com.Parameters.AddWithValue("@INS_PhoneHolder", Ins.tel);
            com.Parameters.AddWithValue("@INS_Person", "");
            com.Parameters.AddWithValue("@INS_BirthdayPerson", DateTime.Now);
            com.Parameters.AddWithValue("@INS_Program", "");
            com.Parameters.AddWithValue("@INS_Sum", 0);
            com.Parameters.AddWithValue("@ins_tukey", 0);
            com.Parameters.AddWithValue("@INS_Prem", 0);
            com.Parameters.AddWithValue("@INS_PremRb", 0);

            //....................................................................................................
            for (int p = 0; p < Ins.persons.Count; p++)
            {
                InsPerson person = Ins.persons[p];

                com.Parameters["@INS_Person"].Value = person.Name;
                com.Parameters["@INS_BirthdayPerson"].Value = person.birhtDay;
                com.Parameters["@INS_Program"].Value = Ins.program;
                com.Parameters["@ins_tukey"].Value = person.tu_key;
                com.Parameters["@INS_Sum"].Value = Ins.medicalsum;
                com.Parameters["@INS_Prem"].Value = person.sumMed;
                com.Parameters["@INS_PremRb"].Value = person.sumMedRb;
                try
                {
                    com.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                    return false;
                }
                /*
                com.Parameters["@INS_Program"].Value = "CL";
                com.Parameters["@INS_Sum"].Value = Ins.fligsum;
                com.Parameters["@INS_Prem"].Value = 0;
                com.Parameters["@INS_PremRb"].Value = 0;
                try
                {
                    com.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }

                if (person.sumIns > 0)
                {
                    com.Parameters["@INS_Program"].Value = "CTI";
                    com.Parameters["@INS_Sum"].Value = person.sumIns;
                    com.Parameters["@INS_Prem"].Value = person.sumVal;
                    com.Parameters["@INS_PremRb"].Value = person.sumRb;
                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        string msg = ex.Message;
                    }
                }
                */
            }

            //....................................................................................................

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion AddInsurance
        //----------------------------------------------------------------------------------------------------
        #region UpdateInsurance
        //----------------------------------------------------------------------------------------------------
        public static bool UpdateInsurance(InsAssist Ins)
        {
            String query = @"UPDATE [" + InsuranceTable + "] ";
            query = query + " SET [INS_Numder] = @INS_Numder, [INS_Country] = @INS_Country ";
            query = query + " WHERE [INS_DGCode] = @INS_DGCode ";
            query = query + " AND [INS_Date] = @INS_Date";
            // AND [INS_DateBegin] = @INS_DateBegin
            // AND [INS_DateChange] = @INS_DateChange";
            query = query + " AND ([INS_Numder] IS NULL OR LEN([INS_Numder]) = 0)";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            com.Parameters.AddWithValue("@INS_Numder", Ins.nomber);
            string country = Ins.country;
            if (country != null && country.Length > 150) country = country.Substring(0, 150);
            com.Parameters.AddWithValue("@INS_Country", country);
            com.Parameters.AddWithValue("@INS_DGCode", Ins.DG_code);
            com.Parameters.AddWithValue("@INS_Date", Ins.dateIsue);
            //com.Parameters.AddWithValue("@INS_DateBegin", Ins.dateFrom);
            //com.Parameters.AddWithValue("@INS_DateChange", DateTime.Now);

            try
            {
                com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                return false;
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion UpdateInsurance
        //----------------------------------------------------------------------------------------------------
        #region AnnulateInsurance
        //----------------------------------------------------------------------------------------------------
        public static bool AnnulateInsurance(string DgCode, string PolicyNumber)
        {
            String query = @"UPDATE [" + InsuranceTable + "] ";
            query = query + " SET [INS_Status] = 0, INS_DateChange=GETDATE() ";
            query = query + " WHERE [INS_DGCode] = @INS_DGCode ";
            query = query + " AND [INS_Numder] = @INS_Numder ";
            query = query + " AND [INS_Status] = 1";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            com.Parameters.AddWithValue("@INS_DGCode", DgCode);
            com.Parameters.AddWithValue("@INS_Numder", PolicyNumber);

            try
            {
                com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                return false;
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion AnnulateInsurance
        //----------------------------------------------------------------------------------------------------
        #region AddHistory
        //----------------------------------------------------------------------------------------------------
        public static bool AddHistory(string DgCode, string PolicyNumber)
        {
            //....................................................................................................
            string query = @"INSERT INTO History (HI_DGCOD,HI_DATE,HI_WHO,HI_TEXT,HI_MOD) ";
            query = query + " VALUES (@dg_code, GetDate(), ";
            query = query + " (SELECT TOP 1 isnull(US_FullName,'Администратор') FROM UserList WHERE US_USERID = SUSER_SNAME()), ";
            query = query + " @text, @mod)";

            using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dg_code", DgCode);
                com.Parameters.AddWithValue("@text", "Аннулирована страховка № " + PolicyNumber);
                com.Parameters.AddWithValue("@mod", "CAN");
                try
                {
                    com.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                    return false;
                }
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        public static bool AddHistory(InsAssist Ins)
        {
            return AddHistory(Ins.DG_code, Ins.nomber);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion AddHistory
        //----------------------------------------------------------------------------------------------------
        #region CancelInsurance
        //----------------------------------------------------------------------------------------------------
        public static bool CancelInsurance(string ins_number)
        {
            //....................................................................................................
            string query = @"UPDATE " + InsuranceTable + " SET INS_DateChange = @date, INS_Status = 0";
            query = query + " WHERE INS_Numder = @number AND INS_Status = 1";

            using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@date", DateTime.Now);
                com.Parameters.AddWithValue("@number", ins_number);
                try
                {
                    com.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                    return false;
                }
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion UpdateInsurance
        //----------------------------------------------------------------------------------------------------
        #region CancelHistory
        //----------------------------------------------------------------------------------------------------
        public static bool CancelHistory(string dg_code, string ins_number)
        {
            //....................................................................................................
            string query = @"INSERT INTO History (HI_DGCOD,HI_DATE,HI_WHO,HI_TEXT,HI_MOD) ";
            query = query + " VALUES (@dg_code, GetDate(), ";
            query = query + " (SELECT TOP 1 isnull(US_FullName,'Администратор') FROM UserList WHERE US_USERID = SUSER_SNAME()), ";
            query = query + " @text, @mod)";

            using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dg_code", dg_code);
                com.Parameters.AddWithValue("@text", "Аннулирована страховка № " + ins_number);
                com.Parameters.AddWithValue("@mod", "CAN");
                try
                {
                    com.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                    return false;
                }
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion CancelHistory
        //----------------------------------------------------------------------------------------------------
        #endregion // Database methods
        //====================================================================================================
        #region AlfaIns service work
        //----------------------------------------------------------------------------------------------------
        #region InitService ()
        //----------------------------------------------------------------------------------------------------
        public static void InitService ()
        {
            LantaConfig = new Config_XML(TypeFileConfig.tfcXML);
            AlfaUserLogin = LantaConfig.Get_Value("AlfaInsService", "user_login");
            AlfaUserPassword = LantaConfig.Get_Value("AlfaInsService", "user_password");
            AlfaAgentUid = LantaConfig.Get_Value("AlfaInsService", "agent_uid");
            AlfaPolicyLink = LantaConfig.Get_Value("AlfaInsService", "policy_link");

            string path = System.Windows.Forms.Application.ExecutablePath;
            string work_dir = path.Substring(0, path.LastIndexOf('\\') + 1);

            AlfaService = new TI_Service(AlfaUserLogin, AlfaUserPassword, AlfaAgentUid, work_dir);
            AlfaService.CashFileName = "AlfaData.txt";
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InitService ()
        //----------------------------------------------------------------------------------------------------
        #region SetStatus ()
        //----------------------------------------------------------------------------------------------------
        public static void SetStatus(string status_text)
        {
            if (SetStatusCallback == null) return;

            SetStatusCallback( status_text );
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SetStatus()
        //----------------------------------------------------------------------------------------------------
        #region ShowMessage ()
        //----------------------------------------------------------------------------------------------------
        public static void ShowMessage(string message_text)
        {
            if (ShowMessageCallback == null) return;

            ShowMessageCallback( message_text );
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ShowMessage()
        //----------------------------------------------------------------------------------------------------
        #region ErrorMessage
        //----------------------------------------------------------------------------------------------------
        public static string ErrorMessage 
        {
            get
            {
                if (AlfaService == null) return "";
                return AlfaService.ErrorMessage;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ErrorMessage
        //----------------------------------------------------------------------------------------------------
        #region Open_Service ()
        //----------------------------------------------------------------------------------------------------
        public static void Open_Service()
        {
            if (AlfaService == null) return;
            if (AlfaService.IsOpened) return;

            SetStatus("Открытие вэб-сервиса AlfaIns ...");

            try
            {
                AlfaService.Open_Service();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                SetStatus("Ошибка подключения к вэб-сервису AlfaIns: " + msg);
            }

            SetStatus("");
            if (!AlfaService.IsOpened)
            {
                SetStatus("Неудачное подключения к вэб-сервису AlfaIns: " + AlfaService.StatusText);
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Open_Service()
        //----------------------------------------------------------------------------------------------------
        #region Close_Service()
        //----------------------------------------------------------------------------------------------------
        public static void Close_Service()
        {
            if (AlfaService == null) return;
            if (!AlfaService.IsOpened) return;

            AlfaService.Close_Service();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Close_Service()
        //----------------------------------------------------------------------------------------------------
        #region Fill_Policy()
        //----------------------------------------------------------------------------------------------------
        public static TI_Policy Fill_Policy(InsAssist Ins, string operation)
        {
            if (Ins == null) return null;

            bool bValidCommand = false;
            if (operation == "Calculate") bValidCommand = true;
            if (operation == "Draft") bValidCommand = true;
            if (operation == "Create") bValidCommand = true;
            if (!bValidCommand)
            {
                SetStatus("Команда <" + operation + "> некорректна.");
                return null;
            }

            //................................................................................
            TI_Policy policy = new TI_Policy(0);


            policy.Operation = operation;

            DateTime dtf = Ins.dateFrom.Date;
            policy.DateFrom = new DateTime(dtf.Year, dtf.Month, dtf.Day);
            DateTime dtt = Ins.dateTo.Date;
            policy.DateTill = new DateTime(dtt.Year, dtt.Month, dtt.Day);

            policy.CurrencyCode = Ins.curens;

            policy.FIO = Ins.holder;
            policy.DateOfBirth = Ins.holderBirthday;
            policy.Passport = Ins.passport;
            policy.AddressPhone = Ins.tel;
            policy.Email = "";

            //................................................................................
            string programUid = "";
            string territoryName = "";
            string countryUid = "";
            //string riskUid = "";

            TI_Program program = null;
            TI_Territory program_territory = null;
            TI_Country program_country = null;

            if (Ins.program == "C") program = Get_Program_ByName("Классик"); // ProgramList[0];
            if (Ins.program == "B") program = Get_Program_ByName("Эконом"); // ProgramList[3]; 
            if (program == null)
            {
                SetStatus("Программа страхования не выбрана");
                return null;
            }
            programUid = program.UID;
            if (string.IsNullOrEmpty(programUid))
            {
                SetStatus("UID программы страхования не определен");
                return null;
            }
            if (program.RiskList == null || program.RiskList.Count == 0)
            {
                SetStatus("Список рисков для программы страхования пуст");
                return null;
            }

            //................................................................................
            if (!string.IsNullOrEmpty(Ins.terretory)) program_territory = Get_Territory(Ins.terretory);
            if (program_territory == null)
            {
                SetStatus("Территория для страхования не задана");
                return null;
            }
            territoryName = program_territory.Name;
            if (string.IsNullOrEmpty(territoryName))
            {
                SetStatus("Территория для страхования не задана");
                return null;
            }

            //................................................................................
            int country_id = Ins.country_id;
            //if (country_id <= 0)
            //{
            //    if (Ins.program == "B") country_id = 27770; // RUSSIA (OVER 90 KM FROM THE PLACE OF PERMANENT RESIDENCE)  >>  5278282f-8ad0-479b-8f3a-d3f224ac5175
            //    if (Ins.program == "C") country_id = 105736; // WORLDWIDE  >>  33da5330-5cc7-4665-92e5-be37da33f2cf
            //}
            if (country_id > 0)
            {
                for (int i = 0; i < program.CountryList.Count; i++)
                {
                    program_country = program.CountryList[i];
                    if (program_country.ID == Ins.country_id)
                    {
                        countryUid = program_country.UID;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(countryUid) || program_country == null)
            {
                SetStatus("Страна для страхования не задана");
                return null;
            }
            Ins.country = program_country.Name;

            //................................................................................
            // risks - Список рисков

            policy.Risks.Clear();

            for (int i = 0; i < Ins.risks.Count; i++)
            {
                InsRisk iRisk = Ins.risks[i];
                string risk_name = iRisk.Name;
                TI_Risk risk = Get_ProgramRisk(program.UID, risk_name);
                if (risk != null)
                {
                    double risk_summa = iRisk.Summa;
                    string risk_currency = iRisk.Currency; // policy.CurrencyCode; 
                    policy.AddRisk(risk.Name, risk.UID, risk_summa, risk_currency, risk.PremCurrency, risk.PremRUR);
                }
            }

            if (policy.Risks.Count == 0)
            {
                SetStatus("Список рисков для полиса пуст");
                return null;
            }

            //................................................................................
            // insureds – список страхователей

            policy.Persons.Clear();

            for (int i = 0; i < Ins.persons.Count; i++)
            {
                InsPerson ins_person = Ins.persons[i];
                if (ins_person != null)
                {
                    TI_Person ti_person = policy.AddInsured(ins_person.Name, ins_person.birhtDay, ins_person.Passport);
                    ti_person.ID = ins_person.tu_key;
                }
            }

            if (policy.Persons.Count == 0)
            {
                SetStatus("Список страхователей пуст");
                return null;
            }

            //................................................................................

            policy.AgentUID = AlfaAgentUid;
            policy.UserLogin = AlfaUserLogin;
            policy.UserPassword = AlfaUserPassword;

            policy.PragramUID = programUid;

            policy.CountryUID = countryUid;

            policy.Comment = "";

            //................................................................................

            return policy;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Fill_Policy()
        //----------------------------------------------------------------------------------------------------
        #region Create_New_Policy()
        //----------------------------------------------------------------------------------------------------
        public static bool Create_New_Policy(InsAssist Ins)
        {
            if (Ins == null) return false;
            if (AlfaService == null) return false;
            if (!AlfaService.IsOpened)
            {
                ShowMessage("Вэб-сервис Альфа-страхоания не подключен.");
                return false;
            }

            //....................................................................................................
            // заполнение структуры полиса
            TI_Policy ti_policy = Fill_Policy(Insist, "Create");

            if (ti_policy == null)
            {
                ShowMessage("Данные для оформления полиса не сформированы.");
                return false;
            }

            //................................................................................
            SetStatus("Запрос на получение страхового полиса...");

            TI_Policy policy = AlfaService.New_Policy(ti_policy);

            if (policy == null) return false;

            //................................................................................
            // handle result 

            //Ins.nomber = "MCR0001025";
            Ins.nomber = policy.Number;
            Ins.policyUID = policy.UID;
            Ins.dateIsue = policy.CreateDate;



            //................................................................................

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Create_New_Policy()
        //----------------------------------------------------------------------------------------------------
        #region Annulate_Policy()
        //----------------------------------------------------------------------------------------------------
        public static bool Annulate_Policy(string policy_number)
        {
            if (string.IsNullOrEmpty(policy_number))
            {
                ShowMessage("Не задан номера полиса для аннуляции.");
                return false;
            }

            if (AlfaService == null) return false;
            if (!AlfaService.IsOpened)
            {
                ShowMessage("Вэб-сервис Альфа-страхоания не подключен.");
                return false;
            }

            //................................................................................
            SetStatus("Аннулирование полиса...");

            bool bAnnulated = AlfaService.Annulate_Policy(policy_number);

            if (!bAnnulated)
            {
                SetStatus("Аннулирование полиса не выполнено");
                return false;
            }

            SetStatus("Аннулирование полиса выполнено.");
            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Annulate_Policy()
        //----------------------------------------------------------------------------------------------------
        #region Upload_Policy()
        //----------------------------------------------------------------------------------------------------
        public static bool Upload_Policy(InsAssist Ins, bool splash_flag = true)
        {
            if (Ins == null) return false;

            frmIntro intro = null;
            if (splash_flag)
            {
                intro = new frmIntro("Выкладка файла полиса в ЛК...");
                intro.Show();
                intro.Refresh();
            }

            bool bRes = Do_Upload_Policy(Ins);

            if (intro != null) intro.Close();

            return bRes;
        }

        //----------------------------------------------------------------------------------------------------
        public static bool Do_Upload_Policy(InsAssist Ins)
        {
            if (Ins == null) return false;

            string DgCode = Ins.DG_code;
            string PolicyNumber = Ins.nomber;
            string PolicyUID = Ins.policyUID;

            if (string.IsNullOrEmpty(DgCode)) return false;
            if (string.IsNullOrEmpty(PolicyNumber)) return false;
            if (string.IsNullOrEmpty(PolicyUID)) return false;

            //................................................................................
            Config_XML conf = new Config_XML();
            string ftp_url = conf.Get_Value("appSettings", "Ftp");
            if (string.IsNullOrEmpty(ftp_url)) ftp_url = "mcruises.ru"; 

            SetStatus("Получение файла полиса...");

            string policy_path = @"d:\Insurense\" + PolicyNumber + ".pdf";

            Download_Policy(PolicyUID, policy_path);

            SetStatus("Файл полиса получен");

            if (!File.Exists(policy_path))
            {
                SetStatus("Файл полиса не найден");
                return false;
            }

            //................................................................................
            SetStatus("Выкладка полиса в ЛК...");

            string serverName = "mcruises.ru"; 
            WorkWithFTP ftp = new WorkWithFTP(serverName);
            string rError = "";
        
            string lala = ftp.lala;
            string papa = ftp.papa;
            ftp.lala = "orderpaper";
            ftp.papa = "5jm60i5bsk";

            if (ftp.GetFilesOnFTPAndCreateNewDir(DgCode, out rError) != WorkWithFTP.FTP_ERROR.ERROR_NO)
            {
                return false;
            }

            string newNameFile = "MK_" + DateTime.Now.ToString("yyMMdd") + (new Random()).Next().ToString();
            string filePath = DgCode;
            if (ftp.Upload(filePath, policy_path, newNameFile, out rError) != WorkWithFTP.FTP_ERROR.ERROR_NO)
            {
                return false;
            }

            ftp.lala = lala;
            ftp.papa = papa;

            File.Delete(policy_path);

            //................................................................................
            string sel_qry = @"SELECT MAX(pa_Number) FROM Lanta_PersonalArea WHERE pa_ddgID = 100600 AND pa_DG_Code = '" + DgCode + "'";
            SqlCommand sel_com = new SqlCommand(sel_qry, WorkWithData.Connection);
            var last_number = sel_com.ExecuteScalar();
            int number = 1;
            if (last_number != null)
            {
                string num = last_number.ToString();
                if (!string.IsNullOrEmpty(num)) number = int.Parse(num) + 1;
            }

            string query = @"insert into Lanta_PersonalArea( ";
            query = query + " [pa_TU_Key],[pa_DG_Code],[pa_ddgID],[pa_Number],[pa_FileName],[pa_UserUpdate],[pa_Description]) ";
            query = query + " Values (@tu_key,@dg_code,@ddgid,@Number,@FileName, ";
            query = query + " (select top 1 US_FullName from UserList where US_USERID = SUSER_SNAME()), ' ')";

            foreach (InsPerson person in Ins.persons)
            {
                int tu_key = person.tu_key;
                using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@tu_key", tu_key);
                    com.Parameters.AddWithValue("@dg_code", DgCode);
                    com.Parameters.AddWithValue("@ddgid", 100600);
                    com.Parameters.AddWithValue("@Number", number);
                    com.Parameters.AddWithValue("@FileName", newNameFile + ".pdf");
                    com.ExecuteNonQuery();
                }
            }

            //................................................................................
            SetStatus("Полис выложен");

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Upload_Policy()
        //----------------------------------------------------------------------------------------------------
        #region Download_Policy()
        //----------------------------------------------------------------------------------------------------
        private static bool Download_Policy(string policyUID, string policyPath)
        {
            string policy_link = @"https://ti.alfastrah.ru/reportproxy/epolicy.aspx?uid=" + policyUID;

            WebClient wc = new WebClient();
            wc.DownloadFile(policy_link, policyPath);

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Download_Policy()
        //----------------------------------------------------------------------------------------------------
        #region Remove_Policy()
        //----------------------------------------------------------------------------------------------------
        public static bool Remove_Policy(string PolicyNumber, bool splash_flag = true)
        {
            if (string.IsNullOrEmpty(PolicyNumber)) return false;

            frmIntro intro = null;
            if (splash_flag)
            {
                intro = new frmIntro("Удаление файла полиса с сервера (из личного кабинета)...");
                intro.Show();
                intro.Refresh();
            }

            bool bRes = Do_Remove_Policy(PolicyNumber);

            if (intro != null) intro.Close();

            return bRes;
        }

        //----------------------------------------------------------------------------------------------------
        public static bool Do_Remove_Policy(string PolicyNumber)
        {
            // for all persons in Isist.persons
            try
            {
                string query = @"select top 1 ins_tukey,ins_dgcode from " + InsuranceTable + " where INS_numder='" + PolicyNumber + "'";
                string DgCode = "";
                int tukey = -1;
                using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count < 1)
                    {
                        return false;
                    }
                    DgCode = table.Rows[0].Field<string>("INS_dgcode");
                    tukey = table.Rows[0].Field<int>("INS_tukey");
                }
                string selFile = @"select top 1 pa_FileName from Lanta_PersonalArea where pa_ddgID=100600 and pa_TU_Key=" + tukey.ToString();
                string filename = "";
                using (SqlCommand com = new SqlCommand(selFile, WorkWithData.Connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count < 1)
                    {
                        return false;
                    }
                    filename = table.Rows[0].Field<string>("pa_FileName");
                }
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                configMap.ExeConfigFilename = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "lanta.sqlconfig.dll.config");
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                string servername = config.AppSettings.Settings["ftp"].Value;

                string serverName = "mcruises.ru";
                WorkWithFTP ftp = new WorkWithFTP(serverName);
                string rError = "";

                string lala = ftp.lala;
                string papa = ftp.papa;
                ftp.lala = "orderpaper";
                ftp.papa = "5jm60i5bsk";

                //FTP_ERROR Delete(string namepath, string fileName, out string strerror)
                ftp.Delete(DgCode, filename, out rError);

                string delPersonalArea = @"delete from Lanta_PersonalArea where  pa_ddgID = 100600 and  pa_DG_Code ='" + DgCode + "' and pa_FileName='" + filename + "'";
                using (SqlCommand com = new SqlCommand(delPersonalArea, WorkWithData.Connection))
                {
                    com.ExecuteNonQuery();
                }

                ftp.lala = lala;
                ftp.papa = papa;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Remove_Policy()
        //----------------------------------------------------------------------------------------------------
        #endregion // AlfaIns service work
        //====================================================================================================
    }
    //----------------------------------------------------------------------------------------------------
    #endregion // AlfaInsAssist
    //====================================================================================================

}
