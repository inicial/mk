using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using WpfControlLibrary.Common;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;
using terms_prepaid.Forms;
using terms_prepaid.Helpers;
using lanta.SQLConfig;


namespace terms_prepaid
{
    public partial class frmInshurControlForm : Form
    {
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------
        public InshurControlViewModel _insControlViewModel;
        public InsControlServiceItem DescItem;

        public string PartnerKeys = "";
        public bool SkipConfirmFlag = false;

        public int SortField = 0;
        public int SortDir = 0;
        public string DataSortOrder = "";

        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region frmInshurControlForm()
        //----------------------------------------------------------------------------------------------------
        public frmInshurControlForm()
        {
            InitializeComponent();

            _insControlViewModel = new InshurControlViewModel();
            //Get_Data();

            InshurControlView uc = new InshurControlView(this);
            uc.DataContext = _insControlViewModel;
            uc.AddCallback += AddInsItem;
            uc.CopyCallback += CopyInsItem;
            uc.SaveCallback += SaveInsItem;
            uc.CancelCallback += CancelInsItem;
            uc.DeleteCallback += DeleteInsItem;
            uc.EditDescCallback += EditDescItem;
            uc.ViewDescCallback += ViewDescItem;
            uc.SortCallback += SortItems;
            
            InshurControlHost.Child = uc;

            SortItems(1, 1, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // frmInshurControlForm()
        //----------------------------------------------------------------------------------------------------
        #region frmInshurControlForm_Load()
        //----------------------------------------------------------------------------------------------------
        private void frmInshurControlForm_Load(object sender, EventArgs e)
        {
            Init_Data();

            Get_Data();

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // frmInshurControlForm_Load()
        //----------------------------------------------------------------------------------------------------
        #endregion // Methods
        //====================================================================================================
        #region Data access
        //----------------------------------------------------------------------------------------------------
        #region InsViewModel
        //----------------------------------------------------------------------------------------------------
        public InshurControlViewModel InsViewModel
        {
            get { return _insControlViewModel; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsViewModel
        //----------------------------------------------------------------------------------------------------
        #region InsServiceList
        //----------------------------------------------------------------------------------------------------
        public InsControlServiceList InsServiceList
        {
            get { if (InsViewModel != null) return InsViewModel.InsControlServiceList; else return null; }
            set { if (InsViewModel != null) InsViewModel.InsControlServiceList = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsServiceList
        //----------------------------------------------------------------------------------------------------
        #region SortNameDir
        //----------------------------------------------------------------------------------------------------
        public int SortNameDir
        {
            get { if (InsViewModel != null) return InsViewModel.SortNameDir; else return 0; }
            set 
            {
                if (InsViewModel != null)
                {
                    InsViewModel.SortNameDir = value;
                    if (value == 0) InsViewModel.SortNameTitle = "";
                    if (value > 0) InsViewModel.SortNameTitle = "Описание  страховки  ^";
                    if (value < 0) InsViewModel.SortNameTitle = "Описание  страховки  v";
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortNameDir
        //----------------------------------------------------------------------------------------------------
        #region SortDateFromDir
        //----------------------------------------------------------------------------------------------------
        public int SortDateFromDir
        {
            get { if (InsViewModel != null) return InsViewModel.SortDateFromDir; else return 0; }
            set
            {
                if (InsViewModel != null)
                {
                    InsViewModel.SortDateFromDir = value;
                    if (value == 0) InsViewModel.SortDateFromTitle = "";
                    if (value > 0) InsViewModel.SortDateFromTitle = "с  даты  ^";
                    if (value < 0) InsViewModel.SortDateFromTitle = "с  даты  v";
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortDateFromDir
        //----------------------------------------------------------------------------------------------------
        #region SortDateTillDir
        //----------------------------------------------------------------------------------------------------
        public int SortDateTillDir
        {
            get { if (InsViewModel != null) return InsViewModel.SortDateTillDir; else return 0; }
            set
            {
                if (InsViewModel != null)
                {
                    InsViewModel.SortDateTillDir = value;
                    if (value == 0) InsViewModel.SortDateTillTitle = "";
                    if (value > 0) InsViewModel.SortDateTillTitle = "с  даты  ^";
                    if (value < 0) InsViewModel.SortDateTillTitle = "с  даты  v";
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortDateTillDir
        //----------------------------------------------------------------------------------------------------
        #region SortCurrencyDir
        //----------------------------------------------------------------------------------------------------
        public int SortCurrencyDir
        {
            get { if (InsViewModel != null) return InsViewModel.SortCurrencyDir; else return 0; }
            set
            {
                if (InsViewModel != null)
                {
                    InsViewModel.SortCurrencyDir = value;
                    if (value == 0) InsViewModel.SortCurrencyTitle = "";
                    if (value > 0) InsViewModel.SortCurrencyTitle = "валюта";
                    if (value < 0) InsViewModel.SortCurrencyTitle = "валюта";
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortCurrencyDir
        //----------------------------------------------------------------------------------------------------
        #endregion // Data access
        //====================================================================================================
        #region Data work
        //----------------------------------------------------------------------------------------------------
        #region Init_Data()
        //----------------------------------------------------------------------------------------------------
        private void Init_Data()
        {
            //....................................................................................................
            Config_XML conf = new Config_XML();
            string rate_keys = conf.Get_Value("appSettings", "RateKeys");
            if (string.IsNullOrEmpty(rate_keys)) rate_keys = "38,14,37";

            string currency_list = "$;Eu;рб";
            List<string> RateList = WorkWithData.GetRateList(rate_keys);
            if (RateList != null && RateList.Count > 0)
            {
                currency_list = "";
                for (int i = 0; i < RateList.Count; i++)
                {
                    string rate = RateList[i];
                    if (currency_list.Length > 0) currency_list = currency_list + ";";
                    currency_list = currency_list + rate;
                }
            }
            _insControlViewModel.currency_list = currency_list;

            //....................................................................................................
            PartnerKeys = conf.Get_Value("appSettings", "InsPartnersKeys");
            if (string.IsNullOrEmpty(PartnerKeys)) PartnerKeys = "55166"; // 51457,51458,52106 - URALSIB
            
            DataTable data_table = new DataTable();
            string query = "SELECT [PR_KEY],[PR_NAME] FROM [dbo].[tbl_Partners] WHERE PR_KEY IN (" + PartnerKeys + ")";
            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data_table);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            //"51457,УРАЛСИБ USD;51458,УРАЛСИБ EUR;52106,УРАЛСИБ RUB;0,АЛЬФАСТР USD;0,АЛЬФАСТР EUR;0,АЛЬФАСТР RUB";
            //string partner_list = "51457,УРАЛСИБ USD;51458,УРАЛСИБ EUR;52106,УРАЛСИБ RUB";
            string partner_list = "55166,АЛЬФАСТРАХ";
            if (data_table != null && data_table.Rows.Count > 0)
            {
                partner_list = "";
                for (int i = 0; i < data_table.Rows.Count; i++)
                {
                    DataRow row = data_table.Rows[i];
                    int pr_key = row.Field<int>("PR_KEY");
                    string pr_name = row.Field<string>("PR_NAME");

                    if (partner_list.Length > 0) partner_list = partner_list + ";";
                    partner_list = partner_list + pr_key.ToString() + "," + pr_name;
                }
            }
            _insControlViewModel.partenr_list = partner_list;

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Init_Data()
        //----------------------------------------------------------------------------------------------------
        #region Get_Data()
        //----------------------------------------------------------------------------------------------------
        private void Get_Data()
        {
            if (InsViewModel == null) return;

            DataTable dt = new DataTable();
            DateTime enddate = DateTime.Now.AddMonths(-1);
            enddate = enddate.AddDays(1 - enddate.Day);

            string sort_order = "";
            if (!string.IsNullOrEmpty(DataSortOrder)) sort_order = "ORDER BY " + DataSortOrder;

            //string query = "SELECT [CS_SVKEY],[CS_CODE],[CS_SUBCODE1],[CS_SUBCODE2],[CS_DATE],[CS_DATEEND]";
            //query = query + "    ,[CS_COSTNETTO],[CS_COST],[CS_DISCOUNT],[CS_RATE],[CS_UPDDATE],[CS_BYDAY]";
            //query = query + "    ,[CS_ID],[CS_TypeDivision],[CS_UPDUSER],[LL_NAME],[CS_PRKEY],[PR_NAME]";
            //query = query + "  FROM [dbo].[tbl_Costs] LEFT JOIN ListLink";
            //query = query + "    ON LL_CODE = CS_CODE AND LL_SVKEY = CS_SVKEY AND LL_SUBCODE1 = CS_SUBCODE1";
            //query = query + "  LEFT JOIN tbl_Partners ON PR_KEY = [CS_PRKEY] ";
            //query = query + "  WHERE CS_SVKEY = 6 AND NOT LL_NAME IS NULL AND CS_DATEEND >= @date";
            ////query = query + "    ORDER BY [CS_SUBCODE1]";
            //query = query + "    ORDER BY [CS_ID]";

            string query = "SELECT [CS_SVKEY],[CS_CODE],[CS_SUBCODE1],[CS_SUBCODE2],[CS_DATE],[CS_DATEEND]";
            query = query + "    ,[CS_COSTNETTO],[CS_COST],[CS_DISCOUNT],[CS_RATE],[CS_UPDDATE],[CS_BYDAY]";
            query = query + "    ,[CS_ID],[CS_TypeDivision],[CS_UPDUSER],[CS_PRKEY],[PR_NAME]";
            query = query + "    ,IsNull([SL_NAME], '') + '/' + IsNull([A1_NAME],'') AS [LL_NAME]";
            query = query + "    ,IsNull([IL_KEY],0) AS [IL_KEY],[IL_PARAMS],[IL_DESCRIPTION]";
            query = query + "  FROM [dbo].[tbl_Costs] ";
            query = query + "    LEFT JOIN ServiceList ON [SL_KEY] = CS_CODE ";
            query = query + "    LEFT JOIN AddDescript1 ON A1_KEY = CS_SUBCODE1 ";
            query = query + "    LEFT JOIN tbl_Partners ON PR_KEY = [CS_PRKEY] ";
            query = query + "    LEFT JOIN mk_InsuranceLink ";
            //query = query + "      ON [IL_CSID]=[CS_ID] ";
            query = query + "      ON [IL_SVKEY]=[CS_SVKEY] AND [IL_CODE]=CS_CODE AND [IL_SUBCODE1]=[CS_SUBCODE1] AND [IL_PRKEY]=[CS_PRKEY] ";
            query = query + "  WHERE CS_SVKEY = 6 AND CS_DATEEND >= @date";
            query = query + "    AND CS_PRKEY IN (" + PartnerKeys + ")";
            //query = query + "    ORDER BY [CS_SUBCODE1]";
            //query = query + "    ORDER BY [CS_ID]";
            query = query + " " + sort_order;

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            com.Parameters.AddWithValue("@date", enddate);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            InsViewModel.Set_TableData(dt);
            
            //Reflect_Data();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Get_Data()
        //----------------------------------------------------------------------------------------------------
        #region Set_TableData()
        //----------------------------------------------------------------------------------------------------
        private void Set_TableData(DataTable dtable)
        {
            if (InsServiceList == null) return;

            InsServiceList.Clear();
            if (dtable == null) return;
            if (dtable.Rows.Count == 0) return;

//InsServiceList.Add(new InsControlServiceItem(InsServiceList.Count + 1, "Страховка..."));

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                try
                {
                    DataRow drow = dtable.Rows[i];
                    InsControlServiceItem item = new InsControlServiceItem(InsServiceList.Count + 1, "Страховка...");
                    item.ServiceName = "Описание цен для страховки (" + item.RowNumber.ToString() + ")";
                    item.DateFrom = drow.Field<DateTime>("CS_DATE");
                    item.DateTill = drow.Field<DateTime>("CS_DATEEND");

                //    InsServiceList.Add(item);

if (InsServiceList.Count > 3) break;
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_TableData()
        //----------------------------------------------------------------------------------------------------
        #region AddInsItem()
        //----------------------------------------------------------------------------------------------------
        public int AddInsItem()
        {
            //....................................................................................................
            int item_id = 0;
            string query = @"INSERT INTO [dbo].[tbl_Costs] ";
            query = query + "([CS_SVKEY],[CS_CODE],[CS_SUBCODE1],[CS_SUBCODE2],[CS_PRKEY],[CS_PKKEY] ";
            query = query + ",[CS_DATE],[CS_DATEEND],[CS_COSTNETTO],[CS_COST] ";
            query = query + ",[CS_CREATOR],[CS_RATE],[CS_BYDAY]) ";
            query = query + " VALUES (6,777000695,0,0,55166,0,'2018-10-01','2019-09-30',0,0,0,'',0) ";
            query = query + " SELECT  CONVERT(integer, scope_identity())";

            using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
            {
                try
                {
                    item_id = (int)com.ExecuteScalar();
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                    // TpLogger.Debug("Save_Bill", ex);
                }
            }
            return item_id;

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AddInsItem()
        //----------------------------------------------------------------------------------------------------
        #region CopyInsItem()
        //----------------------------------------------------------------------------------------------------
        public int CopyInsItem(InsControlServiceItem item) //, ref int params_id)
        {
            if (item == null) return 0;

            //string msg = "Данные будут скопированы в новую запись.";
            //if (System.Windows.MessageBox.Show(msg, "Подтверждение", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel) return 0;

            //....................................................................................................
            int item_id = AddInsItem();
            //params_id = 0;
            if (item_id > 0)
            {
                int back_id = item.RowID;
                //int back_params_id = item.ParamsKey;
                item.RowID = item_id;
                //item.ParamsKey = 0;

                SkipConfirmFlag = true;
                bool bUndo = false;
                bool bSaved = SaveInsItem(item, ref bUndo);
                //params_id = item.ParamsKey;
                SkipConfirmFlag = false;
                
                item.RowID = back_id;
                //item.ParamsKey = back_params_id;
            }
            
            return item_id;

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CopyInsItem()
        //----------------------------------------------------------------------------------------------------
        #region SaveInsItem()
        //----------------------------------------------------------------------------------------------------
        public bool SaveInsItem(InsControlServiceItem item, ref bool undo_flag)
        {
            if (item == null) return false;

            //....................................................................................................
            string name = item.ServiceName;
            string name1 = name;
            string name2 = "";
            int pos = name.IndexOf((char)47); // '/'
            if (pos >= 0)
            {
                name1 = name.Substring(0, pos);
                name2 = name.Substring(pos + 1);
            }
            bool bCutCode = (!string.IsNullOrEmpty(name1) && name1.Length > 60);
            bool bCutSubcode = (!string.IsNullOrEmpty(name2) && name2.Length > 40);
            if (!SkipConfirmFlag && (bCutCode || bCutSubcode))
            {
                string msg = "Длина названия больше допустимого." + (char)13 + (char)10;
                msg = msg + "Название услуги (60)  /  параметр (40)" + (char)13 + (char)10;
                msg = msg + "Обрезать строку названия ?";
                frmNewOptionsConfirmSave frm = new frmNewOptionsConfirmSave(msg);
                frm.ShowDialog();
                undo_flag = false;
                if (!frm.ConfirmFlag) return false;

                if (bCutCode) name1 = name1.Substring(0, 60);
                if (bCutSubcode) name2 = name2.Substring(0, 40);
            }

            //....................................................................................................
            if (!SkipConfirmFlag)
            {
                //string msg = "Изменения будут сохранены в базе данных цен.";
                //if (System.Windows.MessageBox.Show(msg, "Подтверждение", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel) return false;

                string msg = "Вы  действительлно  хотите  сохранить  эти  данные ?";
                frmNewOptionsConfirmSave frm = new frmNewOptionsConfirmSave(msg);
                frm.ShowDialog();
                undo_flag = false;
                if (frm.CancelFlag) undo_flag = true;
                if (!frm.ConfirmFlag) return false;
            }

            //....................................................................................................

            //int cs_id = item.RowID;
            //int partner = item.PartnerKey;

            int code = Accord_ServiceCode(name1);
            int subcode = Accord_ServiceSubcode(name2);

            //item.ParamsString = "Params...";
            //item.DescriptionString = "Insurance description...";

            //....................................................................................................
            item.Read_Netto();
            item.Read_Brutto();

            decimal netto = item.Read_Netto();
            decimal brutto = item.Read_Brutto();

            string query = "UPDATE [tbl_Costs] SET ";
            query = query + " CS_CODE=@code, CS_SUBCODE1=@subcode1";
            query = query + ", CS_DATE=@date, CS_DATEEND=@dateend, CS_RATE=@rate";
            query = query + ", CS_COSTNETTO=@netto, CS_COST=@brutto, CS_BYDAY=@byday";
            query = query + ", CS_PRKEY=@partner ";
            query = query + "WHERE CS_ID=@id";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            try
            {
                com.Parameters.AddWithValue("@code", code);
                com.Parameters.AddWithValue("@subcode1", subcode);
                com.Parameters.AddWithValue("@date", item.DateFrom);
                com.Parameters.AddWithValue("@dateend", item.DateTill);
                com.Parameters.AddWithValue("@rate", item.СostRateString);
                com.Parameters.AddWithValue("@netto", netto);
                com.Parameters.AddWithValue("@brutto", brutto);
                com.Parameters.AddWithValue("@byday", item.PriceByDay);
                com.Parameters.AddWithValue("@partner", item.PartnerKey);
                com.Parameters.AddWithValue("@id", item.RowID);

                com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string ex_msg = ex.Message;
            }

            //....................................................................................................
            int il_key = item.ParamsKey;
            if (!(il_key > 0))
            {
                //....................................................................................................
                string select_query = @"SELECT * FROM [dbo].[mk_InsuranceLink]";
                select_query = select_query + " WHERE IL_SVKEY=@svkey AND IL_CODE=@code AND IL_SUBCODE1=@subcode1 AND IL_PRKEY=@partner";
                //select_query = select_query + " ORDER BY IL_CSID";

                SqlCommand select_com = new SqlCommand(select_query, WorkWithData.Connection);
                select_com.Parameters.AddWithValue("@svkey", 6);
                select_com.Parameters.AddWithValue("@code", code);
                select_com.Parameters.AddWithValue("@subcode1", subcode);
                select_com.Parameters.AddWithValue("@partner", item.PartnerKey);

                DataTable data_table = new DataTable();
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(select_com);
                    adapter.Fill(data_table);
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }

                if (data_table != null && data_table.Rows.Count > 0)
                {
                    DataRow drow = data_table.Rows[0];

                    int key = drow.Field<int>("IL_KEY");
                    //cs_id = drow.Field<int>("CS_ID");
                    //code = drow.Field<int>("CS_CODE");
                    //subcode = drow.Field<int>("CS_SUBCODE1");
                    //partner = drow.Field<int>("CS_PRKEY");
                    string params_string = drow.Field<string>("IL_PARAMS");
                    string description_string = drow.Field<string>("IL_DESCRIPTION");
                    //int il_csid = drow.Field<int>("IL_CSID");

                    if (key > 0) // && !(il_csid > 0))
                    {
                        il_key = key;
                        item.ParamsKey = key;
                        item.ParamsString = params_string;
                        item.DescriptionString = description_string;

                        //string update_query = "UPDATE mk_InsuranceLink SET IL_CSID = @csid WHERE IL_KEY = @ilkey";
                        //SqlCommand update_com = new SqlCommand(update_query, WorkWithData.Connection);
                        //update_com.Parameters.AddWithValue("@ilkey", il_key);
                        //update_com.Parameters.AddWithValue("@csid", item.RowID);
                        //try
                        //{
                        //    update_com.ExecuteNonQuery();
                        //}
                        //catch (System.Exception ex)
                        //{
                        //    string msg = ex.Message;
                        //}
                    }
                }
            }

            if (!(il_key > 0))
            {
                //....................................................................................................
                //string select_query = @"SELECT MAX([IL_KEY]) FROM [dbo].[mk_InsuranceLink]";

                //SqlCommand select_com = new SqlCommand(select_query, WorkWithData.Connection);
                //try
                //{
                //    il_key = (int)select_com.ExecuteScalar();
                //}
                //catch (System.Exception ex)
                //{
                //    string msg = ex.Message;
                //}

                //....................................................................................................
                //il_key++;

                string insert_query = @"INSERT INTO [dbo].[mk_InsuranceLink] ";
                insert_query = insert_query + "([IL_SVKEY],[IL_CODE],[IL_SUBCODE1],[IL_PRKEY],[IL_PARAMS],[IL_DESCRIPTION]) "; //,[IL_CSID]) ";
                insert_query = insert_query + " VALUES (@svkey, @code, @subcode1, @partner, @params, @desc)"; //, @csid)";
                insert_query = insert_query + " SELECT  CONVERT(integer, scope_identity())";

                SqlCommand insert_com = new SqlCommand(insert_query, WorkWithData.Connection);
                try
                {
                    //insert_com.Parameters.AddWithValue("@ilkey", il_key);
                    insert_com.Parameters.AddWithValue("@svkey", 6);
                    insert_com.Parameters.AddWithValue("@code", code);
                    insert_com.Parameters.AddWithValue("@subcode1", subcode);
                    insert_com.Parameters.AddWithValue("@partner", item.PartnerKey);
                    insert_com.Parameters.AddWithValue("@params", item.ParamsString);
                    insert_com.Parameters.AddWithValue("@desc", item.DescriptionString);
                    //insert_com.Parameters.AddWithValue("@csid", item.RowID);

                    //insert_com.ExecuteNonQuery();
                    il_key = (int)insert_com.ExecuteScalar();
                    item.ParamsKey = il_key;
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }

                //....................................................................................................
            }

            query = "UPDATE [mk_InsuranceLink] SET ";
            query = query + " IL_SVKEY=@svkey, IL_CODE=@code, IL_SUBCODE1=@subcode1 ";
            query = query + ", IL_PRKEY=@partner, IL_PARAMS=@params, IL_DESCRIPTION=@desc ";
            query = query + "WHERE IL_KEY=@ilkey";

            com = new SqlCommand(query, WorkWithData.Connection);
            try
            {
                com.Parameters.AddWithValue("@svkey", 6);
                com.Parameters.AddWithValue("@code", code);
                com.Parameters.AddWithValue("@subcode1", subcode);
                com.Parameters.AddWithValue("@partner", item.PartnerKey);
                com.Parameters.AddWithValue("@params", item.ParamsString);
                com.Parameters.AddWithValue("@desc", item.DescriptionString);
                com.Parameters.AddWithValue("@ilkey", il_key);

                com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string ex_msg = ex.Message;
            }

            //....................................................................................................
            Accord_LinkFields(item);

            if (!SkipConfirmFlag)
            {
                frmIntro intro = new frmIntro("");
                intro.Show();
                intro.Refresh();

                Get_Data();

                if (intro != null) intro.Close();
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SaveInsItem()
        //----------------------------------------------------------------------------------------------------
        #region CancelInsItem()
        //----------------------------------------------------------------------------------------------------
        public bool CancelInsItem(InsControlServiceItem item)
        {
            //if (item == null) return false;

            string msg = "Вы хотите отменить изменения ?";
            if (System.Windows.MessageBox.Show(msg, "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes) return true;

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CancelInsItem()
        //----------------------------------------------------------------------------------------------------
        #region DeleteInsItem()
        //----------------------------------------------------------------------------------------------------
        public bool DeleteInsItem(InsControlServiceItem item, bool confirm_flag = true)
        {
            if (item == null) return false;

            if (confirm_flag)
            {
                //string msg = "Данные будут удалены из базе данных цен.";
                //if (System.Windows.MessageBox.Show(msg, "Подтверждение", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel) return false;

                string msg = "Вы  действительлно  хотите  удалить  эти  данные  из  базы  цен ?";
                frmNewOptionsConfirmSave frm = new frmNewOptionsConfirmSave(msg, true);
                frm.ShowDialog();
                if (!frm.ConfirmFlag) return false;
            }

            //....................................................................................................
            string query = @"DELETE FROM [dbo].[tbl_Costs] WHERE CS_ID = @id";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            try
            {
                com.Parameters.AddWithValue("@id", item.RowID);

                com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string ex_msg = ex.Message;
                return false;
            }

            //....................................................................................................
            // KEEP PARAMS FOR DELETED PRICES - DO NOT DELETE

            //string update_query = "UPDATE mk_InsuranceLink SET IL_CSID = 0 WHERE IL_KEY = @ilkey";
            //SqlCommand update_com = new SqlCommand(update_query, WorkWithData.Connection);
            //update_com.Parameters.AddWithValue("@ilkey", item.ParamsKey);
            //try
            //{
            //    update_com.ExecuteNonQuery();
            //}
            //catch (System.Exception ex)
            //{
            //    string ex_msg = ex.Message;
            //}


            //query = @"DELETE FROM [dbo].[mk_InsuranceLink] WHERE [IL_KEY] = @ilkey";

            //com = new SqlCommand(query, WorkWithData.Connection);
            //try
            //{
            //    com.Parameters.AddWithValue("@ilkey", item.ParamsKey);

            //    com.ExecuteNonQuery();
            //}
            //catch (System.Exception ex)
            //{
            //    string ex_msg = ex.Message;
            //    return false;
            //}

            //....................................................................................................

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // DeleteInsItem()
        //----------------------------------------------------------------------------------------------------
        #region EditDescItem()
        //----------------------------------------------------------------------------------------------------
        public bool EditDescItem(InsControlServiceItem item)
        {
            if (item == null) return false;

            //....................................................................................................
            DescItem = item;

            Form frm = new frmInshurControlDesc(item.DescriptionString, EditDescCallback);
            frm.Show();

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        public void EditDescCallback(string desc_text)
        {
            if (DescItem == null) return;
            
            DescItem.DescriptionString = desc_text;

            if (!DescItem.EditFlag)
            {
                SkipConfirmFlag = true;
                bool undo_flag = false;
                SaveInsItem(DescItem, ref undo_flag);
                SkipConfirmFlag = false;
            }

            DescItem = null;
        }
        
        //----------------------------------------------------------------------------------------------------
        #endregion // EditDescItem()
        //----------------------------------------------------------------------------------------------------
        #region ViewDescItem()
        //----------------------------------------------------------------------------------------------------
        public bool ViewDescItem(InsControlServiceItem item)
        {
            if (item == null) return false;

            //....................................................................................................
            DescItem = item;

            Form frm = new frmInshurControlDesc(item.DescriptionString);
            frm.Show();

            return true;

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ViewDescItem()
        //----------------------------------------------------------------------------------------------------
        #region SortItems()
        //----------------------------------------------------------------------------------------------------
        public bool SortItems(int sort_field, int sort_dir, bool request_data = true)
        {
            SortField = sort_field;
            SortDir = sort_dir;

            DataSortOrder = "";
            SortNameDir = 0;
            SortDateFromDir = 0;
            SortDateTillDir = 0;
            SortCurrencyDir = 0;

            if (SortField == 1)
            {
                if (SortDir > 0) { DataSortOrder = "LL_NAME ASC"; SortNameDir = 1; }
                if (SortDir < 0) { DataSortOrder = "LL_NAME DESC"; SortNameDir = -1; }
            }
            if (SortField == 2)
            {
                if (SortDir > 0) { DataSortOrder = "CS_DATE ASC"; SortDateFromDir = 1; }
                if (SortDir < 0) { DataSortOrder = "CS_DATE DESC"; SortDateFromDir = -1; }
            }
            if (SortField == 3)
            {
                if (SortDir > 0) { DataSortOrder = "CS_DATEEND ASC"; SortDateTillDir = 1; }
                if (SortDir < 0) { DataSortOrder = "CS_DATEEND DESC"; SortDateTillDir = -1; }
            }
            if (SortField == 4)
            {
                if (SortDir > 0) { DataSortOrder = "CS_RATE ASC"; SortCurrencyDir = 1; }
                if (SortDir < 0) { DataSortOrder = "CS_RATE DESC"; SortCurrencyDir = -1; }
            }

            //....................................................................................................
            frmIntro intro = new frmIntro("");
            intro.Show();
            intro.Refresh();

            Get_Data();

            //....................................................................................................
            /*
            InsControlServiceList list = InsServiceList;
            InsControlServiceList slist = new InsControlServiceList();

            while (list.Count > 0)
            {
                InsControlServiceItem next_item = null;
                for (int i = 0; i < list.Count; i++)
                {
                    InsControlServiceItem item = InsServiceList[i];
                    bool bNext = false;
                    if (next_item != null && item != null)
                    {
                        if (field == 1)
                        {
                            int comp = string.Compare(item.ServiceName, next_item.ServiceName);
                            bNext = (comp > 0 && dir < 0) || (comp < 0 && dir > 0);
                        }
                    }

                    if (bNext || next_item == null)
                    {
                        next_item = item;
                    }
                }
                if (next_item != null)
                {
                    slist.Add(next_item);
                    list.Remove(next_item);
                }
            }

            InsServiceList = slist;
            */
            //....................................................................................................
            if (intro != null) intro.Close();

            return true;

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortItems()
        //----------------------------------------------------------------------------------------------------
        #region Accord_ServiceCode()
        //----------------------------------------------------------------------------------------------------
        public int Accord_ServiceCode(string name)
        {
            if (string.IsNullOrEmpty(name)) return 0;

            DataTable data_table = new DataTable();
            int sl_key = 0;

            //....................................................................................................
            string query = "SELECT [SL_SVKEY],[SL_KEY],[SL_NAME],[SL_NAMELAT] ";
            query = query + " FROM [dbo].[ServiceList] ";
            query = query + " WHERE [SL_SVKEY] = 6 AND [SL_NAME] LIKE ('%" + name + "%')";
            query = query + " ORDER BY [SL_KEY]";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data_table);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            if (data_table != null && data_table.Rows.Count > 0)
            {
                for (int i = 0; i < data_table.Rows.Count; i++)
                {
                    DataRow row = data_table.Rows[i];
                    string sl_name = row.Field<string>("SL_NAME");
                    sl_key = row.Field<int>("SL_KEY");
                    break;
                }
            }
            if (sl_key > 0) return sl_key;

            //....................................................................................................
            string select_query = @"SELECT MAX([SL_KEY]) FROM [dbo].[ServiceList]";

            SqlCommand select_com = new SqlCommand(select_query, WorkWithData.Connection);
            try
            {
                sl_key = (int)select_com.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }
            //....................................................................................................
            sl_key++;

            string insert_query = @"INSERT INTO [dbo].[ServiceList] ";
            insert_query = insert_query + "([SL_SVKEY],[SL_KEY],[SL_NAME],[SL_CNKEY],[SL_CTKEY]) ";
            insert_query = insert_query + " VALUES (6, " + sl_key.ToString() + ", '" + name + "',460,0)";

            SqlCommand insert_com = new SqlCommand(insert_query, WorkWithData.Connection);
            try
            {
                insert_com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            //....................................................................................................

            return sl_key;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Accord_ServiceCode()
        //----------------------------------------------------------------------------------------------------
        #region Accord_ServiceSubcode() 
        //----------------------------------------------------------------------------------------------------
        public int Accord_ServiceSubcode(string name)
        {
            if (string.IsNullOrEmpty(name)) return 0;

            DataTable data_table = new DataTable();
            int a1_key = 0;

            //....................................................................................................
            string query = "SELECT [A1_KEY],[A1_SVKEY],[A1_CODE],[A1_NAME],[A1_NAMELAT] ";
            query = query + " FROM [dbo].[AddDescript1] ";
            query = query + " WHERE [A1_SVKEY] = 6 AND [A1_NAME] = '" + name + "' "; // LIKE ('%" + name + "%')";
            query = query + " ORDER BY [A1_KEY]";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(data_table);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            if (data_table != null && data_table.Rows.Count > 0)
            {
                for (int i = 0; i < data_table.Rows.Count; i++)
                {
                    DataRow row = data_table.Rows[i];
                    string a1_name = row.Field<string>("A1_NAME");
                    a1_key = row.Field<int>("A1_KEY");
                    break;
                }
            }
            if (a1_key > 0) return a1_key;

            //....................................................................................................
            string select_query = @"SELECT MAX([A1_KEY]) FROM [dbo].[AddDescript1]";

            SqlCommand select_com = new SqlCommand(select_query, WorkWithData.Connection);
            try
            {
                a1_key = (int)select_com.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }
            //....................................................................................................
            a1_key++;

            string insert_query = @"INSERT INTO [dbo].[AddDescript1] ([A1_SVKEY],[A1_KEY],[A1_NAME],[A1_CODE]) ";
            insert_query = insert_query + " VALUES (6, " + a1_key.ToString() + ", '" + name + "', '" + a1_key.ToString() + "')";

            SqlCommand insert_com = new SqlCommand(insert_query, WorkWithData.Connection);
            try
            {
                insert_com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            //....................................................................................................

            return a1_key;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Accord_ServiceSubcode()
        //----------------------------------------------------------------------------------------------------
        #region Accord_LinkFields()
        //----------------------------------------------------------------------------------------------------
        public void Accord_LinkFields(InsControlServiceItem accord_item)
        {
            if (accord_item == null) return;
            if (InsServiceList == null) return;
            if (InsServiceList.Count == 0) return;

            //....................................................................................................
            foreach (InsControlServiceItem item in InsServiceList)
            {
                //if (item.ServiceCode == accord_item.ServiceCode && item.ServiceSubcode1 == accord_item.ServiceSubcode1 && item.PartnerKey == accord_item.PartnerKey)
                if (item.ParamsKey == accord_item.ParamsKey)
                {
                    item.ParamsString = accord_item.ParamsString;
                    item.DescriptionString = accord_item.DescriptionString;
                }
            }
            
            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Accord_LinkFields()
        //----------------------------------------------------------------------------------------------------
        #endregion // Data work
        //====================================================================================================
    }
}
