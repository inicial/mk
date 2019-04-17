using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;
using WpfControlLibrary.Model.Voucher;
using terms_prepaid.Helpers;
using terms_prepaid.Forms;
using lanta.SQLConfig;
using AlfaIns;


namespace terms_prepaid
{
    public delegate void UpdateInsDataNotify();

    public partial class frmNewOptionsEditInsurance : Form
    {
        //----------------------------------------------------------------------------------------------------
        #region Properties
        //----------------------------------------------------------------------------------------------------
        public int InitLeft = 100;
        public int InitTop = 100;
        public bool InitPositionFlag;

        private string CurrencyCode = "";

        private VoucherViewModel _voucherViewModel;
        public InsuranceViewModel _insuranceViewModel;
        public ServiceEditorInsurance _insuranceView;
        private AccessClass _access = new AccessClass(WorkWithData.Connection);

        UpdateInsDataNotify UpdateNotify;

        private bool CollapsedFlag = false;
        private int CollapsedHeigth = 0;

        public List<TouristRecord> TouristRecordList;
        public List<ContractRecord> ContractRecordList;
        public List<ServiceRecord> ServiceRecordList;

        private static string InsErrorMessage = "";
        private int LastAddedDLKEY;

        System.Windows.Forms.Form frmList; // форма для выбора услуги
        private int ServiceEditRowNumber;


        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //----------------------------------------------------------------------------------------------------
        #region frmNewOptionsEditInsurance()
        //----------------------------------------------------------------------------------------------------
        public frmNewOptionsEditInsurance(VoucherViewModel iVoucherViewModel, UpdateInsDataNotify iUpdateNotify)
        {
            InitializeComponent();

            _voucherViewModel = iVoucherViewModel;

            _insuranceViewModel = new InsuranceViewModel();
            _insuranceViewModel.PartnerFlag = _voucherViewModel.PartnerFlag;

            UpdateNotify = iUpdateNotify;

            //GetData();

            //int min_height = 40 + 20 * _insuranceViewModel.InsTouristList.Count;
            //if (min_height < 100) min_height = 100;
            //if (min_height > 200) min_height = 200;

            _insuranceView = new ServiceEditorInsurance(this);
            _insuranceView.CollapsedCallback += Set_Collapsed;
            _insuranceView.AddCallback += Add_Services;
            _insuranceView.SaveCallback += Save_Services;
            _insuranceView.ListCallback += List_Services;
            _insuranceView.DeleteCallback += Delete_Services;
            _insuranceView.NewPolicyCallback += New_Policy;
            //_insuranceView.RealisPolicyCallback += Realis_Policy;
            _insuranceView.AnnulatePolicyCallback += Annulate_Policy;
            //_insuranceView.DataContext = _insuranceViewModel;
            //_insuranceView.SetListHeight(min_height);
            ServiceListHost.Child = _insuranceView;

            GetData();

            //CollapsedHeigth = 200 - min_height + 310;

            //....................................................................................................
            AlfaInsAssist.InitService();

            AlfaInsAssist.Open_Service();

            //....................................................................................................

        }


        //----------------------------------------------------------------------------------------------------
        private void frmNewOptionsEditInsurance_FormClosing(object sender, FormClosingEventArgs e)
        {
            AlfaInsAssist.Close_Service();
        }


        //----------------------------------------------------------------------------------------------------
        #endregion // frmNewOptionsEditInsurance
        //----------------------------------------------------------------------------------------------------
        #region InitPosition()
        //----------------------------------------------------------------------------------------------------
        public void InitPosition(int iLeft, int iTop)
        {
            InitLeft = iLeft;
            InitTop = iTop;
            InitPositionFlag = true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InitPosition
        //----------------------------------------------------------------------------------------------------
        #region frmNewOptionsEditInsurance_Load()
        //----------------------------------------------------------------------------------------------------
        private void frmNewOptionsEditInsurance_Load(object sender, EventArgs e)
        {
            if (InitPositionFlag)
            {
                this.Left = InitLeft;
                this.Top = InitTop;
            }

            Set_Collapsed(true);

            foreach (Service svc in _voucher.ServiceList)
            {
                svc.EditableFlag = false;
                if (svc.KeyForSerch == 7) svc.EditableFlag = true;
                svc.EditFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // frmNewOptionsEditInsurance_Load()
        //----------------------------------------------------------------------------------------------------
        #region Set_Collapsed()
        //----------------------------------------------------------------------------------------------------
        private void Set_Collapsed(bool collapsed_flag)
        {
            //if (collapsed_flag == CollapsedFlag) return;

            CollapsedFlag = collapsed_flag;

            Accord_Height();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_Collapsed()
        //----------------------------------------------------------------------------------------------------
        #region Accord_Height()
        //----------------------------------------------------------------------------------------------------
        private void Accord_Height()
        {
            int min_height = 40 + 20 * _insuranceViewModel.InsTouristList.Count;
            if (min_height < 100) min_height = 100;
            //if (min_height > 200) min_height = 200;

            //if (_insuranceView != null) _insuranceView.SetListHeight(min_height);

            int FormHeight = 700;
            CollapsedHeigth = 200 - min_height + 310;

            if (CollapsedFlag)
            {
                //this.Height = this.Height - CollapsedHeigth;
                this.Height = FormHeight - CollapsedHeigth;
            }
            else
            {
                //this.Height = this.Height + CollapsedHeigth;
                this.Height = FormHeight;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Accord_Height()
        //----------------------------------------------------------------------------------------------------
        #region _voucher
        //----------------------------------------------------------------------------------------------------
        private Voucher _voucher
        {
            get
            {
                if (_voucherViewModel == null) return null;
                return _voucherViewModel.Voucher;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // _voucher
        //----------------------------------------------------------------------------------------------------
        #region ShowMessage()
        //----------------------------------------------------------------------------------------------------
        private void ShowMessage(string msg, string title = "")
        {
            frmNewOptionsConfirmSave frm = new frmNewOptionsConfirmSave(msg);
            frm.SetOkMode(title);
            frm.ShowDialog();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ShowMessage()
        //----------------------------------------------------------------------------------------------------
        #region ShowConfirm()
        //----------------------------------------------------------------------------------------------------
        private bool ShowConfirm(string msg, string title = "")
        {
            frmNewOptionsConfirmSave frm = new frmNewOptionsConfirmSave(msg);
            if (!string.IsNullOrEmpty(title)) frm.Text = title;
            frm.SetYesText("Да");
            frm.SetNoText("Нет");
            frm.ShowDialog();

            if (frm.ConfirmFlag) return true;
            return false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ShowConfirm()
        //====================================================================================================
        #region Data access
        //----------------------------------------------------------------------------------------------------
        #region InsViewModel
        //----------------------------------------------------------------------------------------------------
        private InsuranceViewModel InsViewModel
        {
            get
            {
                if (_insuranceViewModel == null) return null;

                return _insuranceViewModel;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsViewModel
        //----------------------------------------------------------------------------------------------------
        #region ServiceList
        //----------------------------------------------------------------------------------------------------
        private ObservableCollection<Service> ServiceList
        {
            get
            {
                if (InsViewModel == null) return null;

                return InsViewModel.ServiceList;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ServiceList
        //----------------------------------------------------------------------------------------------------
        #region InsTouristList
        //----------------------------------------------------------------------------------------------------
        private List<InsTouristItem> InsTouristList
        {
            get
            {
                if (InsViewModel == null) return null;

                return InsViewModel.InsTouristList;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsTouristList
        //----------------------------------------------------------------------------------------------------
        #region InsServiceList
        //----------------------------------------------------------------------------------------------------
        private List<InsServiceItem> InsServiceList
        {
            get
            {
                if (InsViewModel == null) return null;

                return InsViewModel.InsServiceList;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsTouristList
        //----------------------------------------------------------------------------------------------------
        #region DGCode
        //----------------------------------------------------------------------------------------------------
        private string DGCode
        {
            get
            {
                if (InsViewModel == null) return "";

                return InsViewModel.DGCode;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // DGCode
        //----------------------------------------------------------------------------------------------------
        #region PolicyTouristList
        //----------------------------------------------------------------------------------------------------
        private List<InsTouristItem> PolicyTouristList
        {
            get
            {
                if (InsViewModel == null) return null;

                return InsViewModel.PolicyTouristList;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // PolicyTouristList
        //----------------------------------------------------------------------------------------------------
        #region PolicyInstanceCount
        //----------------------------------------------------------------------------------------------------
        private int PolicyInstanceCount
        {
            get
            {
                if (InsViewModel == null) return 0;

                return InsViewModel.PolicyInstanceCount;
            }
            set
            {
                if (InsViewModel != null) InsViewModel.PolicyInstanceCount = value;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // PolicyInstanceCount
        //----------------------------------------------------------------------------------------------------
        #region Insist
        //----------------------------------------------------------------------------------------------------
        private InsAssist Insist
        {
            get { return AlfaInsAssist.Insist; }
            set { AlfaInsAssist.Insist = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Insist
        //----------------------------------------------------------------------------------------------------
        #region CurrencyCourse
        //----------------------------------------------------------------------------------------------------
        private Decimal CurrencyCourse
        {
            get { return AlfaInsAssist.CurrencyCourse; }
            set { AlfaInsAssist.CurrencyCourse = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CurrencyCourse
        //----------------------------------------------------------------------------------------------------
        #endregion // Data access
        //====================================================================================================
        #region Data work
        //----------------------------------------------------------------------------------------------------
        #region GetData()
        //----------------------------------------------------------------------------------------------------
        private void GetData()
        {
            if (_insuranceViewModel == null) return;

            frmIntro intro = new frmIntro(""); 
            intro.Show();
            intro.Refresh();

            CurrencyCode = _voucherViewModel.ServiceCurrency;

            _insuranceViewModel.CurrencyCode = CurrencyCode;
            _insuranceViewModel.SetVoucherData(_voucherViewModel.Voucher);
            _insuranceViewModel.SetPoliciesData(GetPolicies());
            _insuranceViewModel.SetTouristsData(GetTourists());
            _insuranceViewModel.SetServicesData(GetServices());
            //_insuranceViewModel.AccordData();
            _insuranceViewModel.SetServicesList(GetServicesList());

            GetPolicyInstances();

            //....................................................................................................
            TouristRecordList = AlfaInsAssist.GetTourists(DGCode);
            ContractRecordList = AlfaInsAssist.GetContracts(DGCode);
            ServiceRecordList = AlfaInsAssist.GetServicies(DGCode);

            string DG_Rate = "";
            CurrencyCourse = 0;

            if (ContractRecordList.Count > 0)
            {
                DG_Rate = ContractRecordList[0].DG_RATE;
            }

            if (!String.IsNullOrEmpty(DG_Rate))
            {
                if (DG_Rate != "рб")
                {
                    CurrencyCourse = Convert.ToDecimal(AlfaInsAssist.GetCourse(DG_Rate));
                }
                else
                {
                    CurrencyCourse = 1;
                    //ShowMessage("Страховки по России выписываются через систему страховой компании.");
                    //this.Close();
                }
            }

            //....................................................................................................
            
            int min_height = 40 + 20 * _insuranceViewModel.InsTouristList.Count;
            if (min_height < 100) min_height = 100;
            //if (min_height > 200) min_height = 200;

            _insuranceView.DataContext = null;
            _insuranceView.DataContext = _insuranceViewModel;
            _insuranceView.SetListHeight(min_height);
            _insuranceView.AccordControls();

            CollapsedHeigth = 200 - min_height + 310;

            if (intro != null) intro.Close();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetData()
        //----------------------------------------------------------------------------------------------------
        #region GetVoucherDates()
        //----------------------------------------------------------------------------------------------------
        /*
        private void GetVoucherDates()
        {
            string main_query = @"select DG_CODE,DG_NMEN,DG_MAINMEN,DG_MAINMENPHONE,DG_MAINMENEMAIL,DG_TURDATE,DG_NDAY,DG_PARTNERKEY,DG_RATE,";
            main_query = main_query + " IsNull(broniro.US_KEY, 0) as bron_key, broniro.US_FullNameLat as bronir,";
            main_query = main_query + " IsNull(MANAGER.US_KEY, 0) as manag_key, MANAGER.US_FullNameLat as manag";
            main_query = main_query + " from  dbo.tbl_Dogovor with (nolock)";
            main_query = main_query + " Left join mk_DogovorAdd with (nolock) on dg_code COLLATE Cyrillic_General_CS_AS = da_dgcode COLLATE Cyrillic_General_CS_AS";
            main_query = main_query + " Left join UserList as broniro with (nolock) on da_bron  = broniro.us_key";
            main_query = main_query + " left join UserList as manager with (nolock) on dg_owner = manager.us_key";
            main_query = main_query + " where DG_CODE=@dgcode";

            using (var adapter = new SqlDataAdapter(main_query, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", _dgCode);
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    _tourDate = dt.Rows[0].Field<DateTime>("DG_TURDATE");

                    tbDateBegin.Text = dt.Rows[0].Field<DateTime>("DG_TURDATE").Date.ToString("dd MMMM yy");
                    tbDateEnd.Text =
                        dt.Rows[0].Field<DateTime>("DG_TURDATE")
                            .AddDays((int)dt.Rows[0].Field<double>("DG_NDAY") - 1)
                            .Date.ToString("dd MMMM yy");
                }
            }
        }
        */
        //----------------------------------------------------------------------------------------------------
        #endregion // GetVoucherDates()
        //----------------------------------------------------------------------------------------------------
        #region GetPolicies()
        //----------------------------------------------------------------------------------------------------
        private DataTable GetPolicies()
        {
            DataTable dt = new DataTable();

            string dgcode = _voucher.DgCode;

            string query = @"SELECT DISTINCT [INS_Numder],[INS_Status],"; // [INS_tukey],";
            query = query + " case when INS_Status=1 then 'Выписана' when INS_Status=0 then 'Аннулирована' end as [status] ";
            query = query + " FROM [dbo].[" + AlfaInsAssist.InsuranceTable + "] WHERE INS_DGCode=@dgcode";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@dgcode", dgcode);
            try
            {
                adapter.Fill(dt);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            return dt;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetPolicies()
        //----------------------------------------------------------------------------------------------------
        #region GetTourists()
        //----------------------------------------------------------------------------------------------------
        public DataTable GetTourists()
        {
            DataTable dt = new DataTable();
            string dgcode = _voucher.DgCode;

            string query = @"SELECT DISTINCT TU_KEY, DG_CODE, TU_NAMELAT+' '+TU_FNAMELAT as TOURIST_NAME ";
            query = query + "  ,TU_BIRTHDAY,DG_NDAY,DG_TURDATE,TU_PHONE,TU_PASPORTNUM,TU_PASPORTTYPE ";
            query = query + "  ,isnull(ins_numder,'') as INS_NUMBER ";
            query = query + "  ,dbo.GetYears(TU_BIRTHDAY,DG_TURDATE + DG_NDAY - 1) as AGE ";
            query = query + "  FROM tbl_Turist INNER JOIN tbl_Dogovor ON tbl_Turist.TU_DGCOD = tbl_Dogovor.DG_CODE ";
            query = query + "  LEFT JOIN " + AlfaInsAssist.InsuranceTable + " ON INS_tukey=TU_KEY and INS_Status=1 ";
            query = query + "  WHERE DG_code = @dgcode";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@dgcode", dgcode);
            try
            {
                adapter.Fill(dt);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            return dt;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetTourists()
        //----------------------------------------------------------------------------------------------------
        #region GetServices()
        //----------------------------------------------------------------------------------------------------
        public DataTable GetServices()
        {
            DataTable dt = new DataTable();
            string dgcode = _voucher.DgCode;
            DateTime tourDate = DateTime.Now.Date;
            if (_voucher.TourDate.Year > 2010)
                tourDate = _voucher.TourDate.Date;

            //....................................................................................................
            Config_XML conf = new Config_XML();
            string partner_list = conf.Get_Value("appSettings", "InsPartnersKeys");
            if (string.IsNullOrEmpty(partner_list)) partner_list = "55166"; // 51457,51458,52106 - URALSIB

            string query = @"SELECT [CS_SVKEY],[CS_CODE],[CS_SUBCODE1],[CS_SUBCODE2],[CS_PRKEY] ";
            query = query + ",IsNull([SL_NAME], '') AS [SL_NAME],IsNull([A1_NAME],'') AS [A1_NAME] ";
            query = query + ",IsNull([SL_NAME], '') + '/' + IsNull([A1_NAME],'') AS [LL_NAME] ";
            query = query + ",[CS_COSTNETTO],[CS_COST],[CS_RATE],[CS_BYDAY],[CS_ID] ";
            query = query + "FROM [dbo].[tbl_Costs] ";
            query = query + "  LEFT JOIN ServiceList ON [SL_KEY] = CS_CODE ";
            query = query + "  LEFT JOIN AddDescript1 ON A1_KEY = CS_SUBCODE1 ";
            query = query + "  LEFT JOIN tbl_Partners ON PR_KEY = [CS_PRKEY] ";
            query = query + "WHERE CS_SVKEY = 6 ";
            query = query + "  AND CS_DATE <= @date AND CS_DATEEND >= @dateend ";
            query = query + "  AND CS_RATE = @rate";
            query = query + "  AND CS_PRKEY IN (" + partner_list + ")";
            query = query + "  ORDER BY [LL_NAME]";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@date", tourDate);
            com.Parameters.AddWithValue("@dateend", tourDate);
            com.Parameters.AddWithValue("@rate", CurrencyCode);
          
            try
            {
                adapter.Fill(dt);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            return dt;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetServices()
        //----------------------------------------------------------------------------------------------------
        #region GetServicesList()
        //----------------------------------------------------------------------------------------------------
        public DataTable GetServicesList()
        {
            DataTable dt = new DataTable();
            string dgcode = _voucher.DgCode;

            string query = @"SELECT DL_DGCOD,DL_KEY,DL_TRKEY,DL_SVKEY";
            query = query + ",dbo.MK_name_SERVISES_lk_new(DL_SVKEY, DL_CODE, DL_SUBCODE1,DL_SUBCODE2,DL_NAME,dl.DL_KEY) AS DL_NAME ";
            query = query + ",DL_TURDATE,DL_DAY,DL_CODE,DL_SUBCODE1 ";
            query = query + ",DL_NMEN,DL_NDAYS,DL_COST,DL_BRUTTO,DL_DGKEY,TU_TUKEY, TU_DLKEY,TU_NAMERUS, TU_FNAMERUS ";
            query = query + ",(DL_COST / DL_NMEN) AS [NETTO],((DL_BRUTTO + IsNull(DL_DISCOUNT,0)) / DL_NMEN) AS [BRUTTO] ";
            query = query + ",IsNull(DL_PARTNERKEY,0) AS [DL_PARTNERKEY] ";
            query = query + "FROM tbl_DogovorList AS DL ";
            query = query + "INNER JOIN TuristService AS TS ON TS.[TU_DLKEY] = DL.DL_KEY ";
            query = query + "INNER JOIN tbl_Turist AS TU ON TU.tu_key = TS.[TU_TUKEY] ";
            query = query + "WHERE DL.DL_DGCOD = @dgcode AND DL.DL_SVKEY = 6 ";
            query = query + "ORDER BY DL.DL_KEY";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@dgcode", dgcode);
            try
            {
                adapter.Fill(dt);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            return dt;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetServicesList()
        //----------------------------------------------------------------------------------------------------
        #region GetPolicyParams()
        //----------------------------------------------------------------------------------------------------
        public string GetPolicyParams(int dl_code, int dl_subcode, int partner_key)
        {
            DataTable data_table = new DataTable();
            //string dgcode = _voucher.DgCode;
            int svkey = 6;

            string query = @"SELECT IsNull([IL_PARAMS],'') AS [IL_PARAMS] FROM [mk_InsuranceLink] ";
            query = query + "WHERE [IL_SVKEY] = @svkey AND [IL_CODE] = @code AND [IL_SUBCODE1] = @subcode AND [IL_PRKEY] = @prkey";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            //com.Parameters.AddWithValue("@dgcode", dgcode);
            com.Parameters.AddWithValue("@svkey", svkey);
            com.Parameters.AddWithValue("@code", dl_code);
            com.Parameters.AddWithValue("@subcode", dl_subcode);
            com.Parameters.AddWithValue("@prkey", partner_key);
            try
            {
                adapter.Fill(data_table);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            string policy_params = "";
            if (data_table != null && data_table.Rows.Count > 0)
            {
                DataRow row = data_table.Rows[0];
                policy_params = row.Field<string>("IL_PARAMS");
            }

            return policy_params;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetPolicyParams()
        //----------------------------------------------------------------------------------------------------
        #region GetPolicyInstances()
        //----------------------------------------------------------------------------------------------------
        public int GetPolicyInstances()
        {
            PolicyInstanceCount = 0;

            if (InsTouristList == null) return 0;
            if (InsTouristList.Count == 0) return 0;

            //....................................................................................................
            foreach (InsTouristItem tourist in InsTouristList)
            {
                Fill_TouristPolicy_Data(tourist);
            }

            //....................................................................................................
            List<InsTouristItem> PolicyList = new List<InsTouristItem>();
            
            // with policy number
            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.HasRisk("Мед") && !string.IsNullOrEmpty(item.PolicyNumberString))
                {
                    bool bAdded = false;
                    foreach (InsTouristItem pol_item in PolicyList)
                    {
                        bool bDiff = false;
                        //if (pol_item.policy_territory != item.policy_territory) bDiff = true;
                        //if (pol_item.TourDate.Date != item.TourDate.Date) bDiff = true;
                        //if (pol_item.NDays != item.NDays) bDiff = true;
                        if (string.Compare(pol_item.PolicyNumberString, item.PolicyNumberString) != 0) bDiff = true;
                        if (!bDiff)
                        {
                            bAdded = true;
                            item.InstanceNumber = pol_item.InstanceNumber;
                            break;
                        }
                    }
                    if (!bAdded)
                    {
                        PolicyList.Add(item);
                        item.InstanceNumber = PolicyList.Count;

                    }
                    foreach (InsTouristItem subitem in InsTouristList)
                    {
                        if (!subitem.HasRisk("Мед") && subitem.Tukey == item.Tukey)
                        {
                            subitem.InstanceNumber = item.InstanceNumber;
                        }
                    }
                }
            }
            
            // without policy number
            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.HasRisk("Мед") && string.IsNullOrEmpty(item.PolicyNumberString))
                {
                    bool bAdded = false;
                    foreach (InsTouristItem pol_item in PolicyList)
                    {
                        if (string.IsNullOrEmpty(pol_item.PolicyNumberString) && item.RowNumber != pol_item.RowNumber)
                        {
                            bool bDiff = false;
                            if (pol_item.policy_territory != item.policy_territory) bDiff = true;
                            if (pol_item.TourDate.Date != item.TourDate.Date) bDiff = true;
                            if (pol_item.NDays != item.NDays) bDiff = true;
                            if (ServicesDiffs(pol_item, item)) bDiff = true;
                            //bDiff = true;
                            if (!bDiff)
                            {
                                bAdded = true;
                                item.InstanceNumber = pol_item.InstanceNumber;
                                break;
                            }
                        }
                    }
                    if (!bAdded)
                    {
                        PolicyList.Add(item);
                        item.InstanceNumber = PolicyList.Count;

                    }
                    foreach (InsTouristItem subitem in InsTouristList)
                    {
                        if (!subitem.HasRisk("Мед") && subitem.Tukey == item.Tukey)
                        {
                            subitem.InstanceNumber = item.InstanceNumber;
                        }
                    }
                }
            }

//foreach (InsTouristItem item in InsTouristList)
//    item.PricingString = item.InstanceNumber.ToString();
            
            List<int> InstanceList = new List<int>();
            InsTouristItem prev_item = null;
            foreach (InsTouristItem item in InsTouristList)
            {
                int instance = item.InstanceNumber;
                bool bAdded = false;
                foreach (int inst in InstanceList)
                {
                    if (inst == instance) bAdded = true;
                }
                if (!bAdded)
                {
                    InstanceList.Add(instance);
                    item.PolicyIconsFlag = true;
                    if (prev_item != null) prev_item.LastRowFlag = true;
                }
//if (item.HasRisk("Мед")) 
//{
//    item.PolicyIconsFlag = true;
//    if (prev_item != null) prev_item.LastRowFlag = true;
//}
                prev_item = item;
            }

            //....................................................................................................
            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.PolicyNumberFlag)
                {
                    foreach (InsTouristItem subitem in InsTouristList)
                    {
                        if (subitem.InstanceNumber == item.InstanceNumber)
                            subitem.PolicyExistsFlag = true;
                    }
                }
            }

            //....................................................................................................
            return 0;
        }

        private bool ServicesDiffs(InsTouristItem item1, InsTouristItem item2)
        {
            if (item1 == null) return false;
            if (item2 == null) return false;
            if (item1.RowNumber == item2.RowNumber) return false;

            foreach (InsTouristItem subitem1 in InsTouristList)
            {
                if (!subitem1.HasRisk("Мед") && subitem1.Tukey == item1.Tukey)
                {
                    bool bFound = false;
                    foreach (InsTouristItem subitem2 in InsTouristList)
                    {
                        if (!subitem2.HasRisk("Мед") && subitem2.Tukey == item2.Tukey)
                        {
                            bool bDiff = false;
                            if (subitem1.DlCode != subitem2.DlCode) bDiff = true;
                            if (!bDiff) bFound = true;
                        }
                    }
                    if (!bFound) return true;
                }
            }

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetPolicyInstances()
        //----------------------------------------------------------------------------------------------------
        #region GetCountryByTerritory()
        //----------------------------------------------------------------------------------------------------
        public int GetCountryByTerritory(string territory_name )
        {
            if (string.IsNullOrEmpty(territory_name)) return 0;

            DataTable data_table = new DataTable();

            string query = @"SELECT IsNull([TL_COUNTRYID],'') AS [TL_COUNTRYID] FROM [mk_TerritoryLink] WHERE [TL_NAME] = @name";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@name", territory_name);
            try
            {
                adapter.Fill(data_table);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            int country_id = 0;
            if (data_table != null && data_table.Rows.Count > 0)
            {
                DataRow row = data_table.Rows[0];
                country_id = row.Field<int>("TL_COUNTRYID");
            }

            return country_id;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetCountryByTerritory()
        //----------------------------------------------------------------------------------------------------
        #region Add_Services()
        //----------------------------------------------------------------------------------------------------
        private void Add_Services()
        {
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;
            
            if (InsServiceList == null) return;
            if (InsServiceList.Count == 0) return;

            //....................................................................................................
            string tourist_list = "";
            int tourist_count = 0;
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.AddFlag)
                {
                    tourist_count++;
                    if (!string.IsNullOrEmpty(tourist_list)) tourist_list = tourist_list + ", ";
                    tourist_list = tourist_list + tourist.TouristFullName;
                }
            }
            if (tourist_count == 0)
            {
                //MessageBox.Show("Выберите туристов для добавления услуг страхования.");
                ShowMessage("Выберите туристов для добавления услуг страхования.");
                return;
            }

            string service_list = "";
            int service_count = 0;
            foreach (InsServiceItem item in InsServiceList)
            {
                if (item.AddFlag)
                {
                    service_count++;
                    if (!string.IsNullOrEmpty(service_list)) service_list = service_list + ", " + (char)13 + (char)10 + (char)13 + (char)10;
                    service_list = service_list + service_count.ToString() + ") " + item.ServiceName;
                }
            }
            if (service_count == 0)
            {
                //MessageBox.Show("Выберите услуги страхования для добавления.");
                ShowMessage("Выберите услуги страхования для добавления.");
                return;
            }

            //....................................................................................................
            int add_rows = 0;
            string msg = "Для туриста" + (char)13 + (char)10;
            if (tourist_count > 1) msg = "Для туристов" + (char)13 + (char)10;
            msg = msg + tourist_list + (char)13 + (char)10;
            string msg2 = "будет добавлена услуга" + (char)13 + (char)10;
            if (service_count > 1) { msg2 = "будут добавлены услуги" + (char)13 + (char)10; add_rows = add_rows + service_count; }
            msg = msg + msg2 + (char)13 + (char)10 + service_list;
            
            //if (MessageBox.Show(msg, "Подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes) return;
            frmNewOptionsConfirmSave frm = new frmNewOptionsConfirmSave(msg, false, 50 + add_rows * 50);
            frm.SetYesText("Да, добавить");
            frm.ShowDialog();
            if (!frm.ConfirmFlag) return;
            
            //....................................................................................................
            foreach (InsServiceItem item in InsServiceList)
            {
                if (item.AddFlag) Add_ServiceItem(item);
            }

            foreach (InsServiceItem item in InsServiceList)
            {
                if (item.AddFlag) item.AddFlag = false;
            }

            //Set_Collapsed(true);
            _insuranceView.Reset_Controls();

            //....................................................................................................
            // update form data

            GetData();

            Accord_Height();
            //Set_Collapsed(true);

            if (UpdateNotify != null) UpdateNotify();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Add_Services()
        //----------------------------------------------------------------------------------------------------
        #region Add_ServiceItem()
        //----------------------------------------------------------------------------------------------------
        private void Add_ServiceItem(InsServiceItem service_item)
        {
            LastAddedDLKEY = 0;

            if (service_item == null) return;
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            //....................................................................................................
            int tourist_count = 0;
            foreach (InsTouristItem tourist in InsTouristList)
                if (tourist.AddFlag) tourist_count++;
            if (tourist_count == 0) return;

            //....................................................................................................
            //string name = service_item.ServiceName;
            //MessageBox.Show("Adding service: " + name);

            string dgcode = _voucher.DgCode; // DL_DGCOD
            int DL_SVKEY = 6;
            int DL_KEY = 0;
            int DL_CODE = service_item.CODE; // DL_CODE from service_item params - service type
            int DL_SUBCODE = service_item.Subcode1; // DL_SUBCODE from service_item params - age
            string DL_NAME = service_item.ServiceName; 
            int DL_DGKEY = 0;
            int DL_NMEN = tourist_count;
            int DL_NDAYS = 1; // количество дней
            decimal cost = (decimal)service_item.Netto; // cost from service_item params - NETTO
            decimal brutto = (decimal)service_item.Brutto; // cost from service_item params - BRUTTO
            int user_key = _access.userKey;
            int partner_key = service_item.PartnerKey;
            DateTime tur_date = DateTime.Now.Date;
            DateTime date_beg = DateTime.Now.Date;
            DateTime date_end = DateTime.Now.Date;
            //tur_date = _voucher

            string query = "SELECT MAX(DL_KEY) FROM tbl_DogovorList";
            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
            try
            {
                DL_KEY = (int)com.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                return;
            }
            if (DL_KEY == 0) return;

            DL_KEY = DL_KEY + 1;

            query = "SELECT DG_Key, DG_TURDATE, DG_NDAY FROM tbl_Dogovor WHERE DG_CODE = @dgcode";
            com = new SqlCommand(query, WorkWithData.Connection);
            com.Parameters.AddWithValue("@dgcode", dgcode);
            try
            {
                //DL_DGKEY = (int)com.ExecuteScalar();
                DataTable dtable = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dtable);
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    DataRow row = dtable.Rows[0];
                    DL_DGKEY = row.Field<int>("DG_Key");
                    tur_date = row.Field<DateTime>("DG_TURDATE");
                    DL_NDAYS = (int)Math.Round(row.Field<double>("DG_NDAY"));

                    date_beg = tur_date;
                    //date_end = tur_date.AddDays(DL_NDAYS - 1);
                    date_end = tur_date.AddDays(DL_NDAYS);
                }
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                return;
            }
            if (DL_DGKEY == 0) return;


            //....................................................................................................
            query = "INSERT INTO tbl_DogovorList ( ";
            query = query + " DL_DGCOD,DL_KEY,DL_TRKEY,DL_SVKEY,DL_NAME,DL_DAY,DL_CODE,DL_SUBCODE1 ";
            query = query + ",DL_NMEN,DL_NDAYS,DL_COST,DL_BRUTTO,DL_DGKEY,DL_CREATOR,DL_OWNER ";
            query = query + ",DL_PARTNERKEY,DL_TURDATE, DL_DATEBEG, DL_DATEEND ";
            query = query + ") VALUES (";
            query = query + "@dgcode, @dlkey, 0, @svkey, @dlname, @dlday, @dlcode, @dlsubcode, ";
            query = query + "@nmen, @ndays, @cost, @brutto, @dgkey, @creator, @owner, ";
            query = query + "@partnerkey, @turdate, @datebeg, @dateend ) ";
            //query = query + " SELECT CONVERT(integer, scope_identity())";

            com = new SqlCommand(query, WorkWithData.Connection);
            com.Parameters.AddWithValue("@dgcode", dgcode);
            com.Parameters.AddWithValue("@dlkey", DL_KEY);
            com.Parameters.AddWithValue("@svkey", DL_SVKEY);
            com.Parameters.AddWithValue("@dlname", DL_NAME);
            com.Parameters.AddWithValue("@dlday", 1);
            com.Parameters.AddWithValue("@dlcode", DL_CODE);
            com.Parameters.AddWithValue("@dlsubcode", DL_SUBCODE);
            com.Parameters.AddWithValue("@nmen", DL_NMEN);
            com.Parameters.AddWithValue("@ndays", DL_NDAYS);
            com.Parameters.AddWithValue("@dgkey", DL_DGKEY);
            com.Parameters.AddWithValue("@cost", cost);
            com.Parameters.AddWithValue("@brutto", brutto);
            com.Parameters.AddWithValue("@creator", user_key);
            com.Parameters.AddWithValue("@owner", user_key);
            com.Parameters.AddWithValue("@partnerkey", partner_key);
            com.Parameters.AddWithValue("@turdate", tur_date);
            com.Parameters.AddWithValue("@datebeg", date_beg);
            com.Parameters.AddWithValue("@dateend", date_end);

            try
            {
                com.ExecuteNonQuery();
                //DL_KEY = (int)com.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                return;
            }

            //....................................................................................................
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.AddFlag) 
                {
                    int TU_KEY = tourist.Tukey;

                    //string service_name = service_item.ServiceName;
                    //string tourist_name = tourist.FullName + " (" + tourist.TouristName + ")";
                    //MessageBox.Show("Adding service - tourist link: " + (char)13 + (char)10 + service_name + " >> " + tourist_name);

                    query = "INSERT INTO TuristService (TU_TUKEY,TU_DLKEY) VALUES (@tukey, @dlkey)";
                    com = new SqlCommand(query, WorkWithData.Connection);
                    com.Parameters.AddWithValue("@tukey", TU_KEY);
                    com.Parameters.AddWithValue("@dlkey", DL_KEY);

                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        string msg = ex.Message;
                    }
                }
            }

            LastAddedDLKEY = DL_KEY;

            //....................................................................................................
            //SqlCommand com = WorkWithData.Connection.CreateCommand();
            //com.CommandType = CommandType.StoredProcedure;
            //com.CommandText = "mk_dogovor_recalc";

            com = new SqlCommand("mk_dogovor_recalc", WorkWithData.Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("dg_code", dgcode);
            try
            {
                com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Add_ServiceItem()
        //----------------------------------------------------------------------------------------------------
        #region Save_Services()
        //----------------------------------------------------------------------------------------------------
        private void Save_Services()
        {
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            //....................................................................................................
            InsTouristItem edit_tourist = null;
            string tourist_list = "";
            int tourist_count = 0;
            string service_name = "";
            int dl_key = 0;
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.AddFlag)
                {
                    if (tourist.EditFlag && dl_key == 0)
                    {
                        edit_tourist = tourist;
                        dl_key = tourist.DlKey;
                        service_name = tourist.ServiceName;
                    }
                    tourist_count++;
                    if (!string.IsNullOrEmpty(tourist_list)) tourist_list = tourist_list + ", ";
                    string name = tourist.TouristFullName;
                    if (string.IsNullOrEmpty(name))
                    {
                        foreach (InsTouristItem item in InsTouristList)
                            if (item.Tukey == tourist.Tukey && !string.IsNullOrEmpty(item.TouristFullName))
                            {
                                name = item.TouristFullName;
                                break;
                            }
                    }
                    tourist_list = tourist_list + name;
                }
            }
            if (tourist_count == 0)
            {
                MessageBox.Show("Выберите туристов для добавления услуг страхования.");
                return;
            }
            if (edit_tourist == null) return;
            /*
            bool bPricing = false;
            foreach (InsServiceItem item in InsServiceList)
            {
                if (item.CODE == edit_tourist.DlCode && item.Subcode1 == edit_tourist.DlSubcode)
                {
                    edit_tourist.Netto = (decimal)item.Netto;
                    edit_tourist.Brutto = (decimal)item.Brutto;
                    bPricing = true;
                }
            }
            if (!bPricing)
            {
                MessageBox.Show("Не найдена цена для изменения...");
                return;
            }
            */
            bool bUpdate = true;
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.DlKey == dl_key && !tourist.AddFlag)
                {
                    bUpdate = false;
                    break;
                }
            }

            //....................................................................................................
            string msg = "Для туриста" + (char)13 + (char)10;
            if (tourist_count > 1) msg = "Для туристов" + (char)13 + (char)10;
            msg = msg + tourist_list + (char)13 + (char)10;
            msg = msg + "будет изменена услуга" + (char)13 + (char)10;
            msg = msg + service_name;

            //if (MessageBox.Show(msg, "Подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes) return;
            frmNewOptionsConfirmSave frm = new frmNewOptionsConfirmSave(msg, false, 50);
            frm.ShowDialog();
            if (!frm.ConfirmFlag) return;

            //....................................................................................................
            if (bUpdate)
            {
                Update_ServiceItem(edit_tourist);
            }
            else
            {
                // DELETE AND INSERT CHANGED SERVICE
                //msg = "Для раздельного редактирования необходимо удалить услугу у выбранных туристов и добавить новую.";
                //MessageBox.Show(msg, "Сообщение", MessageBoxButtons.OK);
                //return;

                foreach (InsTouristItem item in InsTouristList)
                {
                    if (item.DlKey == dl_key && item.AddFlag)
                    {
                        Delete_Services_Item(item); // DELETE SERVICE FOR THIS TOURIST
                    }
                }

                InsServiceItem add_service = null;
                foreach (InsServiceItem item in InsServiceList)
                {
                    if (item.CODE == edit_tourist.DlCode && item.Subcode1 == edit_tourist.DlSubcode)
                    {
                        add_service = item;
                        break;
                    }
                }
                if (add_service != null)
                {
                    Add_ServiceItem(add_service); // ADD CHANGED SERVICE FOR SELECTED TOURISTS
                    if (LastAddedDLKEY > 0)
                    {
                        edit_tourist.DlKey = LastAddedDLKEY;
                        Update_ServiceItem(edit_tourist);
                    }
                }
            }
            
            // ACCORD VOUCHER DATES



            //Set_Collapsed(true);
            _insuranceView.Reset_Controls();
            
            //....................................................................................................
            // update form data

            GetData();

            if (UpdateNotify != null) UpdateNotify();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Save_Services()
        //----------------------------------------------------------------------------------------------------
        #region Update_ServiceItem()
        //----------------------------------------------------------------------------------------------------
        private void Update_ServiceItem(InsTouristItem ins_item)
        {
            if (ins_item == null) return;

            //....................................................................................................
            string dgcode = _voucher.DgCode; 
            //int DL_SVKEY = 6;
            int DL_KEY = ins_item.DlKey;
            int DL_CODE = ins_item.DlCode;
            int DL_SUBCODE = ins_item.DlSubcode;
            string DL_NAME = ins_item.ServiceName;
            DateTime DL_TURDATE = ins_item.DateFrom.Date;
            int DL_NDAYS = 0;
            DateTime DateTill = DL_TURDATE;
            do 
            {
                DL_NDAYS++;
                DateTill = DL_TURDATE.Date.AddDays(DL_NDAYS - 1);
            } while (DateTill.Date < ins_item.DateTill.Date);
            //decimal cost = (decimal)ins_item.Netto;
            //decimal brutto = (decimal)ins_item.Brutto; 

            //....................................................................................................
            string query = "UPDATE tbl_DogovorList SET ";
            //query = query + " DL_SVKEY = @svkey, ";
            query = query + " DL_CODE = @dlcode, ";
            query = query + " DL_SUBCODE1 = @dlsubcode, ";
            query = query + " DL_NAME = @dlname, ";
            query = query + " DL_TURDATE = @dlturdate, ";
            query = query + " DL_NDAYS = @dlndays ";
            //query = query + " DL_COST = @cost, ";
            //query = query + " DL_BRUTTO = @brutto ";
            query = query + " WHERE DL_KEY = @dlkey";

            SqlCommand com = new SqlCommand(query, WorkWithData.Connection);

            using (com)
            {
                try
                {
                    //com.Parameters.AddWithValue("@svkey", DL_SVKEY);
                    com.Parameters.AddWithValue("@dlcode", DL_CODE);
                    com.Parameters.AddWithValue("@dlsubcode", DL_SUBCODE);
                    com.Parameters.AddWithValue("@dlname", DL_NAME);
                    com.Parameters.AddWithValue("@dlturdate", DL_TURDATE);
                    com.Parameters.AddWithValue("@dlndays", DL_NDAYS);
                    //com.Parameters.AddWithValue("@cost", cost);
                    //com.Parameters.AddWithValue("@brutto", brutto);
                    com.Parameters.AddWithValue("@dlkey", DL_KEY);
                    com.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                    return;
                }
            }

            //....................................................................................................
            com = new SqlCommand("mk_dogovor_recalc", WorkWithData.Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("dg_code", dgcode);
            try
            {
                com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Update_ServiceItem()
        //----------------------------------------------------------------------------------------------------
        #region List_Services()
        //----------------------------------------------------------------------------------------------------
        private void List_Services( int row_number, int selected_index )
        {
            if (frmList != null)
            {
                bool bExit = false;
                if (frmList.WindowState == System.Windows.Forms.FormWindowState.Normal)
                {
                    frmList.Close();
                    bExit = true;
                }
                if (!frmList.IsDisposed) frmList.Dispose();
                frmList = null;
                if (bExit) return;
            }
            if (frmList == null)
            {
                ServiceEditRowNumber = row_number;

                int ix = 250;
                int iy = row_number * 20 + 142;
                int x = this.Left + ix;
                int y = this.Top + iy;

                List<string> ItemsList = new List<string>();
                if (InsServiceList != null && InsServiceList.Count > 0)
                    foreach (InsServiceItem item in InsServiceList)
                        ItemsList.Add(item.ServiceName);

                frmList = new frmNewOptionsEditInsList(x, y, ItemsList, selected_index, List_SelectService);
                frmList.Show();
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // List_Services()
        //----------------------------------------------------------------------------------------------------
        #region List_SelectService()
        //----------------------------------------------------------------------------------------------------
        public void List_SelectService(int selected_index)
        {
            //if (!frmList.IsDisposed) frmList.Dispose();
            frmList = null;
            _insuranceView.ListClosedDT = DateTime.Now;
            _insuranceView.IsListOpened = false;

            if (selected_index < 0) return;

            InsTouristItem ins_item = null;
            int index = ServiceEditRowNumber - 1;
            if (index >= 0 && index < InsTouristList.Count) ins_item = InsTouristList[index];
            if (ins_item == null) return;

            InsServiceItem s_item = null;
            if (selected_index >= 0 && selected_index < InsServiceList.Count) s_item = InsServiceList[selected_index];
            if (s_item == null) return;

            ins_item.DlCode = s_item.CODE;
            ins_item.DlSubcode = s_item.Subcode1;
            ins_item.ServiceName = s_item.ServiceName;
            //ins_item.Netto = (decimal)s_item.Netto;
            //ins_item.Brutto = (decimal)s_item.Brutto;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // List_SelectService()
        //----------------------------------------------------------------------------------------------------
        #region Delete_Services()
        //----------------------------------------------------------------------------------------------------
        private void Delete_Services(int row_number)
        {
            if (row_number == 0) return;
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            InsTouristItem delete_item = null;
            //....................................................................................................
            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.RowNumber == row_number)
                {
                    delete_item = item;
                    break;
                }
            }
            if (delete_item == null) return;

            int dl_key = delete_item.DlKey;
            int tu_key = delete_item.Tukey;
            int nmen = delete_item.NMen;
            if (dl_key == 0) return;
            if (tu_key == 0) return;

            //....................................................................................................
            string msg = "Услуга будет удалена:" + (char)13 + (char)10 + delete_item.ServiceName + (char)13 + (char)10 + "Вы хотите удалить услугу ?";
            if (MessageBox.Show(msg, "Подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes) return;

            //....................................................................................................
            bool bRes = Delete_Services_Item(delete_item);

            if (!bRes) return;

            //....................................................................................................
            // update form data

            GetData();

            Accord_Height();
            //if (CollapsedFlag) Set_Collapsed(false);
            //Set_Collapsed(true);

            if (UpdateNotify != null) UpdateNotify();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Delete_Services()
        //----------------------------------------------------------------------------------------------------
        #region Delete_Services_Item()
        //----------------------------------------------------------------------------------------------------
        private bool Delete_Services_Item(InsTouristItem delete_item)
        {
            if (delete_item == null) return false;

            int dl_key = delete_item.DlKey;
            int tu_key = delete_item.Tukey;
            int nmen = delete_item.NMen;
            if (dl_key == 0) return false;
            if (tu_key == 0) return false;

            //....................................................................................................
            bool bRes = DeleteInsuranceTouristService(dl_key, tu_key, nmen);

            using (SqlCommand com = new SqlCommand("mk_dogovor_recalc", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dg_code", DGCode);
                com.ExecuteNonQuery();
            }

            if (!bRes) return false;

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Delete_Services_Item()
        //----------------------------------------------------------------------------------------------------
        #region DeleteInsuranceTouristService()
        //----------------------------------------------------------------------------------------------------
        public bool DeleteInsuranceTouristService(int dl_key, int tu_key, int tu_count = -1)
        {
            if (dl_key <= 0) return false;
            if (tu_key <= 0) return false;

            string delete_query = "DELETE FROM TuristService WHERE TU_DLKEY = @dlkey AND TU_TUKEY = @tukey";
            using (SqlCommand com = new SqlCommand(delete_query, WorkWithData.Connection))
            {
                try
                {
                    com.Parameters.AddWithValue("@dlkey", dl_key);
                    com.Parameters.AddWithValue("@tukey", tu_key);
                    com.ExecuteNonQuery();
                }
                catch (SqlException) { return false; }
            }

            int nmen = -1;
            if (tu_count > 0) nmen = tu_count;
            if (tu_count <= 0)
            {
                string query = "SELECT DL_NMEN FROM tbl_DogovorList WHERE DL_KEY = @dlkey";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, WorkWithData.Connection))
                {
                    try
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@dlkey", dl_key);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                            nmen = dt.Rows[0].Field<int>("DL_NMEN");
                    }
                    catch (SqlException) { return false; }
                }
            }
            if (nmen > 1)
            {
                string update_query = "UPDATE tbl_DogovorList SET [DL_NMEN] = [DL_NMEN] - 1 WHERE DL_KEY = @dlkey";
                using (SqlCommand com = new SqlCommand(update_query, WorkWithData.Connection))
                {
                    try
                    {
                        com.Parameters.AddWithValue("@dlkey", dl_key);
                        com.ExecuteNonQuery();
                    }
                    catch (SqlException) { return false; }
                }
            }
            else
            {
                //delete_query = "DELETE FROM tbl_DogovorList WHERE DL_KEY = @dlkey";
                delete_query = "UPDATE tbl_DogovorList SET ";
                //delete_query = delete_query + " DL_SVKEY = 0, ";
                delete_query = delete_query + " DL_DGCOD = '', DL_TRKEY = 0, DL_NAME = '', DL_DAY = 0, ";
                delete_query = delete_query + " DL_CODE = 0, DL_SUBCODE1 = 0, DL_NMEN = 0, DL_NDAYS = 0, ";
                delete_query = delete_query + " DL_COST = 0, DL_BRUTTO = 0, DL_DGKEY = 0, DL_CREATOR = 0, DL_OWNER = 0, ";
                delete_query = delete_query + " DL_PARTNERKEY = 0, DL_TURDATE = GetDate(), DL_DATEBEG = GetDate(), DL_DATEEND = GetDate() ";
                delete_query = delete_query + " WHERE DL_KEY = @dlkey";
                using (SqlCommand com = new SqlCommand(delete_query, WorkWithData.Connection))
                {
                    try
                    {
                        com.Parameters.AddWithValue("@dlkey", dl_key);
                        com.ExecuteNonQuery();
                    }
                    catch (SqlException ex) { string msg = ex.Message; return false; }
                }
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // DeleteInsuranceTouristService()
        //----------------------------------------------------------------------------------------------------
        #region New_Policy()
        //----------------------------------------------------------------------------------------------------
        private void New_Policy(int row_number, bool cabinet_flag)
        {
            New_Policy(row_number);

            if (!cabinet_flag) return;

            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            //....................................................................................................
            List<int> InstanceList = new List<int>();
            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.PolicyExistsFlag)
                {
                    bool bFound = false;
                    foreach (int inst in InstanceList)
                    {
                        if (inst == item.InstanceNumber) { bFound = true; break; }
                    }
                    if (!bFound) InstanceList.Add(item.InstanceNumber);
                }
            }
            if (InstanceList.Count == 0) return;

            //....................................................................................................
            foreach (int instance in InstanceList)
            {
                InsTouristItem to_policy_item = null;
                foreach (InsTouristItem item in InsTouristList)
                {
                    if (item.PolicyExistsFlag && item.InstanceNumber == instance)
                    {
                        to_policy_item = item;
                        break;
                    }
                }
                if (to_policy_item != null)
                {
                    bool bRes = Cabinet_Policy(to_policy_item.PolicyNumberString);
                }
            }

        }

        //----------------------------------------------------------------------------------------------------
        private void New_Policy(int row_number)
        {
            //if (row_number == 0) return;
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            //....................................................................................................
            List<int> InstanceList = new List<int>();
            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.CheckToPolicyRow(row_number))
                {
                    bool bFound = false;
                    foreach (int inst in InstanceList)
                    {
                        if (inst == item.InstanceNumber) { bFound = true; break; }
                    }
                    if (!bFound) InstanceList.Add(item.InstanceNumber);
                }
            }
            if (InstanceList.Count == 0) return;

            //....................................................................................................
            foreach (InsTouristItem tourist in InsTouristList)
            {
                Fill_TouristPolicy_Data(tourist); // done above
            }

            //....................................................................................................
            bool bUpdate = false;
            int policy_num = 0;
            foreach (int instance in InstanceList)
            {
                InsTouristItem to_policy_item = null;
                foreach (InsTouristItem item in InsTouristList)
                {
                    if (item.CheckToPolicyRow(row_number) && item.InstanceNumber == instance)
                    {
                        to_policy_item = item;
                        break;
                    }
                }
                if (to_policy_item != null)
                {
                    if (InstanceList.Count > 1) policy_num++;
                    bool bRes = New_Policy_Instance(to_policy_item.InstanceNumber, policy_num);
                    if (bRes) bUpdate = true;
                }
            }

            if (!bUpdate) return;

            //....................................................................................................
            // update form data

            GetData();

            if (UpdateNotify != null) UpdateNotify();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // New_Policy()
        //----------------------------------------------------------------------------------------------------
        #region New_Policy_Instance()
        //----------------------------------------------------------------------------------------------------
        private bool New_Policy_Instance(int instance, int policy_num)
        {
            if (instance <= 0) return false;
            if (InsTouristList == null) return false;
            if (InsTouristList.Count == 0) return false;

            //....................................................................................................
            List<InsTouristItem> NewPolicyTouristList = new List<InsTouristItem>();
            InsTouristItem policy_tourist = null;
            //int dl_code = 0;
            //int dl_subcode = 0;
            int partner_key = 0;
            string tourist_list = "";
            int tourist_count = 0;
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.InstanceNumber == instance)
                {
                    //if (tourist.DlKey == dlkey)
                    if (tourist.HasRisk("Мед"))
                    {
                        //if (partner_key == 0 && tourist.PartnerKey > 0)
                        if (policy_tourist == null && tourist != null)
                        {
                            //    dl_code = tourist.DlCode;
                            //    dl_subcode = tourist.DlSubcode;
                            //    partner_key = tourist.PartnerKey;
                            //tourist.policy_binding_flag = true;
                            policy_tourist = tourist;
                            partner_key = tourist.PartnerKey;
                        }

                        foreach (InsTouristItem pol_tourist in PolicyTouristList)
                        {
                            if (pol_tourist.Tukey == tourist.Tukey)
                            {
                                bool bAdded = false;
                                foreach (InsTouristItem new_tourist in NewPolicyTouristList)
                                {
                                    if (new_tourist.Tukey == pol_tourist.Tukey)
                                    {
                                        bAdded = true;
                                        break;
                                    }
                                }
                                if (!bAdded)
                                {
                                    NewPolicyTouristList.Add(pol_tourist);

                                    tourist_count++;
                                    if (!string.IsNullOrEmpty(tourist_list)) tourist_list = tourist_list + ", ";
                                    tourist_list = tourist_list + tourist.FullName; // TouristFullName;

                                    break;
                                }
                            }
                        }
                    }
                    //else
                    //{
                    //    MessageBox.Show("У туриста " + tourist.TouristFullName + " нет медицинской страховки!", "Предупреждение");
                    //    return;
                    //}
                }
            }
            if (tourist_count == 0)
            {
                //MessageBox.Show("Нет туристов для выписки полиса.");
                ShowMessage("Нет туристов для выписки полиса.");
                return false;
            }
            if (policy_tourist == null)
            {
                //MessageBox.Show("Нет основной услуги медицинского страхования.");
                ShowMessage("Нет основной услуги медицинского страхования.");
                return false;
            }
            if (partner_key == 0)
            //if (policy_tourist.PartnerKey == 0)
            {
                //MessageBox.Show("Нет партнера для выписки полиса.");
                ShowMessage("Нет партнера для выписки полиса.");
                return false;
            }

            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.InstanceNumber == instance)
                {
                    foreach (InsTouristItem pol_tourist in PolicyTouristList)
                    {
                        if (pol_tourist.Tukey == tourist.Tukey)
                        {
                            bool bAdded = false;
                            foreach (InsTouristItem new_tourist in NewPolicyTouristList)
                            {
                                if (new_tourist.Tukey == pol_tourist.Tukey)
                                {
                                    bAdded = true;
                                    break;
                                }
                            }
                            if (!bAdded)
                            {
                                //MessageBox.Show("У туриста " + tourist.TouristFullName + " нет медицинской страховки!", "Предупреждение");
                                ShowMessage("У туриста " + tourist.TouristFullName + " нет медицинской страховки!", "Предупреждение");
                                return false;
                            }
                        }
                    }
                }
            }

            //string PolicyParams = GetPolicyParams(dl_code, dl_subcode, partner_key);

            //if (string.IsNullOrEmpty(PolicyParams))
            //{
            //    MessageBox.Show("Нет набора параметров для выписки полиса.");
            //    return;
            //}

            //....................................................................................................
            string msg = "Выбран турист для выписки полиса";
            if (tourist_count > 1) msg = "Выбраны туристы для выписки полиса";
            if (policy_num > 0) msg = msg + " " + policy_num .ToString() + ":";
            msg = msg + (char)13 + (char)10;
            msg = msg + tourist_list + (char)13 + (char)10;
            msg = msg + "Продолжить выписку?";

            string title = "Проверка списка застрахованных";
            if (policy_num > 0) title = title + " (полис " + policy_num.ToString() + ")";

            //if (MessageBox.Show(msg, title, MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes) return false;
            if (!ShowConfirm(msg, title)) return false;

            //....................................................................................................
            bool bRes = false;

            //if (partner_key == 51457 || partner_key == 51458 || partner_key == 52106) bRes = Create_Policy_Uralsib();

            //if (partner_key == 55166) bRes = Create_Policy_Alfa( DGCode, NewPolicyTouristList, PolicyParams );

            bRes = Create_Policy_Alfa(DGCode, NewPolicyTouristList, policy_tourist); //, PolicyParams);

            if (!bRes) return false;

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // New_Policy_Instance()
        //----------------------------------------------------------------------------------------------------
        #region Cabinet_Policy()
        //----------------------------------------------------------------------------------------------------
        private bool Cabinet_Policy ( string policy_number )
        {
            //MessageBox.Show("Страховка будет выложена в ЛК...", "Сообщение");

            string dgcode = _voucher.DgCode;

            string query = @"update [dbo].[mk_DogovorAdd] set [DA_DocumentAccept] = dateadd(minute,1,GetDate()) where [DA_DGCODE]=@dgcode";

            try
            {
                SqlCommand com = new SqlCommand(query, WorkWithData.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                com.Parameters.AddWithValue("@dgcode", dgcode);
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
        #endregion // Cabinet_Policy()
        //----------------------------------------------------------------------------------------------------
        #region Annulate_Policy()
        //----------------------------------------------------------------------------------------------------
        private void Annulate_Policy(int row_number)
        {
            if (row_number == 0) return;
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            //....................................................................................................
            string policy_number = "";
            int partner_key = 0;
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.RowNumber == row_number)
                //if (tourist.HasRisk("Мед"))
                {
                    policy_number = tourist.PolicyNumberString;
                    partner_key = tourist.PartnerKey;
                    break;
                }
            }

            if (string.IsNullOrEmpty(policy_number)) 
            {
                //MessageBox.Show("Нет номера полиса, аннуляция невозможна.");
                return;
            }

            //....................................................................................................

            if (MessageBox.Show("Вы хотите аннулировать полис  " + policy_number + " ?", "Подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes) return;

            //....................................................................................................
            bool bRes = false;

            //if (partner_key == 51457 || partner_key == 51458 || partner_key == 52106) bRes = Annulate_Policy_Uralsib();

            //if (partner_key == 55166) bRes = Annulate_Policy_Alfa(DGCode, policy_number);

            bRes = Annulate_Policy_Alfa(DGCode, policy_number);

            if (!bRes) return;

            //....................................................................................................
            // update form data

            GetData();

            if (UpdateNotify != null) UpdateNotify();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Annulate_Policy()
        //----------------------------------------------------------------------------------------------------
        #endregion // Data work
        //====================================================================================================
        #region Policy work
        //----------------------------------------------------------------------------------------------------
        #region Create_Policy_Uralsib()
        //----------------------------------------------------------------------------------------------------
        public bool Create_Policy_Uralsib()
        {
            MessageBox.Show("Выписка полисов Уралсиба - в другом месте.", "Сообщение", MessageBoxButtons.OK);
            return false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Create_Policy_Uralsib()
        //----------------------------------------------------------------------------------------------------
        #region Create_Policy_Alfa()
        //----------------------------------------------------------------------------------------------------
        public bool Create_Policy_Alfa(string dgcode, List<InsTouristItem> policy_tourists, InsTouristItem policy_tourist)
        {
            //....................................................................................................
            #region проверка входных данных
            //....................................................................................................
            if (string.IsNullOrEmpty(dgcode))
            {
                //SetStatus("Нет кода договора...");
                InsErrorMessage = "Нет кода договора";
                return false;
            }

            if (policy_tourists == null || policy_tourists.Count == 0)
            {
                //SetStatus("Нет туристов...");
                InsErrorMessage = "Нет туристов";
                return false;
            }

            if (policy_tourist == null)
            {
                InsErrorMessage = "Нет туриста для привязки параметров полиса";
                return false;
            }

            //if (string.IsNullOrEmpty(policy_params))
            //{
                //SetStatus("Нет строки параметров...");
            //    InsErrorMessage = "Нет строки параметров";
            //    return false;
            //}

            //....................................................................................................
            #endregion // проверка входных данных
            //....................................................................................................
            #region проверка на наличие полисов у туристов
            //....................................................................................................
            string tourist_list = "";
            int tourist_count = 0;
            foreach (InsTouristItem tourist in policy_tourists)
            {
                if (!string.IsNullOrEmpty(tourist.PolicyNumberString))
                {
                    tourist_count++;
                    if (tourist_list.Length > 0) tourist_list = tourist_list + ", ";
                    tourist_list = tourist_list + tourist.TouristName;
                }
            }
            if (tourist_count > 0)
            {
                string msg = "У туриста " + tourist_list + " уже есть страховка.";
                if (tourist_count > 1) msg = "У туристов " + tourist_list + " уже есть страховка.";
                MessageBox.Show(msg, "Сообщение");
                return false;
            }

            //....................................................................................................
            #endregion // проверка на наличие полисов у туристов
            //....................................................................................................
            #region проверка на количество туристов - не более 4
            //....................................................................................................
            if (policy_tourists.Count > 4)
            {
                MessageBox.Show("В одной страховке может быть не более 4 чекловек.", "Сообщение");
                return false;
            }

            //....................................................................................................
            #endregion // проверка на количество туристов - не более 4
            //....................................................................................................
            #region проверка наличия услуги
            //....................................................................................................
            if (ServiceList.Count < 1)
            {
                MessageBox.Show("Не найдена услуга 'Страховка' или к ней не привязаны туристы.", "Сообщение");
                return false;
            }

            //....................................................................................................
            #endregion // проверка наличия услуги
            //....................................................................................................
            #region заполнение данных и добавление записей о страховке
            //....................................................................................................

            bool bCreate = Fill_Policy_Data(policy_tourists, policy_tourist); // policy_params);

            //....................................................................................................
            #endregion // заполнение данных и добавление записей о страховке
            //....................................................................................................
            #region оформление страхового полиса в системе страхования
            //....................................................................................................

            if (bCreate)
            {
                //....................................................................................................
                //AlfaInsAssist.AddInsurance(Insist);  // внесение данных в базу

                bool bPolicy = AlfaInsAssist.Create_New_Policy(Insist); // создание полиса в системе страхования

                if (bPolicy) // внесение и обновление данных в базе
                {
                    AlfaInsAssist.AddInsurance(Insist);  // внесение данных в базу

                    AlfaInsAssist.UpdateInsurance(Insist); // обновление данных в базе
                    AlfaInsAssist.AddHistory(Insist);

                    AlfaInsAssist.Upload_Policy( Insist );

                    //MessageBox.Show("Создан страховой полис номер " + Insist.nomber + ".", "Сообщение", MessageBoxButtons.OK);
                }
                else
                {
                    string msg = "Данные полиса не получены.";
                    if (!string.IsNullOrEmpty(AlfaInsAssist.ErrorMessage))
                        msg = msg + (char)13 + (char)10 + "Ошибка: " + AlfaInsAssist.ErrorMessage;
                    MessageBox.Show(msg, "Сообщение", MessageBoxButtons.OK);
                }

                GetData(); // обновление данных
                
                //....................................................................................................
            }
            else
            {
                //if (!string.IsNullOrEmpty(InsErrorMessage))

                return false;
            }

            //....................................................................................................
            #endregion // оформление страхового полиса в системе страхования
            //....................................................................................................
            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Create_Policy_Alfa()
        //----------------------------------------------------------------------------------------------------
        #region Fill_TouristPolicy_Data()
        //----------------------------------------------------------------------------------------------------
        public void Fill_TouristPolicy_Data(InsTouristItem tourist)
        {
            if (tourist == null) return;

            tourist.policy_params_flag = false;
            tourist.policy_params = GetPolicyParams(tourist.DlCode, tourist.DlSubcode, tourist.PartnerKey);
// PROGRAM=C;TERRITORY=T-IV;COUNTRY=105736;RISK=Мед;SUMM=40000;
            if (string.IsNullOrEmpty(tourist.policy_params)) 
            {
                tourist.policy_params_error = "Нет набора параметров для выписки полиса";
                return;
            }

            //....................................................................................................
            string InsProgram = "";
            string InsTerritory = "";
            int InsCountryID = 0;
            List<string> InsRisks = new List<string>();
            double InsSumma = 0.0;

            string[] params_list = tourist.policy_params.Split(';');
            foreach (string param_string in params_list)
            {
                string param_name = "";
                string param_value = "";
                string[] param_array = param_string.Split('=');
                if (param_array.Length > 0) param_name = param_array[0].Trim();
                if (param_array.Length > 1) param_value = param_array[1].Trim();

                //....................................................................................................
                if (param_name.ToUpper() == "PROGRAM")
                {
                    if (param_value.ToUpper() == "C") InsProgram = param_value;  // C - Классик
                    if (param_value.ToUpper() == "B") InsProgram = param_value;  // B - Эконом
                    if (string.IsNullOrEmpty(InsProgram))
                    {
                        tourist.policy_params_error = "В параметрах не задана программа страхования";
                        //return;
                    }
                }
                //....................................................................................................
                if (param_name.ToUpper() == "TERRITORY")
                {
                    param_value = param_value.Replace('Т', 'T'); // русские на английские
                    if (param_value.ToUpper() == "T-I") InsTerritory = param_value;   // Россия... T - english
                    if (param_value.ToUpper() == "T-IV") InsTerritory = param_value;  // Все страны мира, включая Россию (исключая 90км от места постоянного проживания)
                    if (param_value.ToUpper() == "T-V") InsTerritory = param_value;   // Россия (свыше 90 км от места постоянного проживания)
                    if (string.IsNullOrEmpty(InsTerritory))
                    {
                        tourist.policy_params_error = "В параметрах не задана территория";
                        //return;
                    }
                }
                //....................................................................................................
                if (param_name.ToUpper() == "COUNTRY")
                {
                    if (!string.IsNullOrEmpty(param_value))
                    {
                        try
                        {
                            InsCountryID = int.Parse(param_value);
                        }
                        catch (System.Exception) { }
                    }
                    else
                    {
                        //InsErrorMessage = "В параметрах не задана страна";
                        //return false;
                    }
                }
                //....................................................................................................
                if (param_name.ToUpper() == "RISK")
                {
                    string[] risks_list = param_value.Split(',');
                    foreach (string risk_param in risks_list)
                    {
                        string risk = "";
                        if (risk_param.ToUpper() == "Мед".ToUpper()) risk = risk_param;        // Медицинские и иные расходы
                        if (risk_param.ToUpper() == "Несчас".ToUpper()) risk = risk_param;     // Несчастный случай 
                        if (risk_param.ToUpper() == "Отмена".ToUpper()) risk = risk_param;     // Отмена поездки 
                        if (risk_param.ToUpper() == "Граждан".ToUpper()) risk = risk_param;    // Гражданская ответственность 
                        if (risk_param.ToUpper() == "Имущ".ToUpper()) risk = risk_param;       // Имущество 
                        if (risk_param.ToUpper() == "Багаж".ToUpper()) risk = risk_param;      // Страхование багажа 

                        if (!string.IsNullOrEmpty(risk)) InsRisks.Add(risk);
                    }
                    if (InsRisks.Count == 0)
                    {
                        tourist.policy_params_error = "В параметрах не заданы риски для страхования";
                        //return;
                    }
                }
                //....................................................................................................
                if (param_name.ToUpper() == "SUMM")
                {
                    if (!string.IsNullOrEmpty(param_value))
                    {
                        try
                        {
                            InsSumma = double.Parse(param_value);
                        }
                        catch (System.Exception ex) { }
                    }
                    else
                    {
                        //InsErrorMessage = "В параметрах не задана сумма покрытия";
                        //return false;
                    }
                }
                //....................................................................................................
            }

            //....................................................................................................
            // define CountryID by territory

            if (InsCountryID <= 0)
            {
                InsCountryID = GetCountryByTerritory(InsTerritory);
            }

            //....................................................................................................
            tourist.policy_program = InsProgram;
            tourist.policy_territory = InsTerritory;
            tourist.policy_country_id = InsCountryID;
            tourist.policy_risks = InsRisks;
            tourist.policy_summa = InsSumma;

            tourist.policy_params_flag = true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Fill_TouristPolicy_Data()
        //----------------------------------------------------------------------------------------------------
        #region Fill_Policy_Data()
        //----------------------------------------------------------------------------------------------------
        public bool Fill_Policy_Data(List<InsTouristItem> policy_tourists, InsTouristItem policy_tourist) //, string policy_params) 
        {
            //....................................................................................................
            #region Check input data
            //....................................................................................................
            if (ContractRecordList == null) return false;
            if (ContractRecordList.Count == 0)
            {
                //ShowMessage("Не найдено записи о договоре!");
                InsErrorMessage = "Не найдено записи о договоре";
                return false;
            }
            if (ServiceList == null) return false;
            if (policy_tourists == null || policy_tourists.Count == 0)
            {
                //ShowMessage("Список страхователей пуст!");
                InsErrorMessage = "Список страхователей пуст";
                return false;
            }
            if (policy_tourist == null)
            {
                //ShowMessage("Список страхователей пуст!");
                InsErrorMessage = "Нет туриста для привязки параметров полиса";
                return false;
            }
            //if (string.IsNullOrEmpty(policy_params))
            //{
                //ShowMessage("Нет списка параметров для оформления полиса!");
            //    InsErrorMessage = "Нет списка параметров для оформления полиса";
            //    return false;
            //}

            //....................................................................................................
            #endregion // Check input data
            //....................................................................................................
            #region Set basic policy data
            //....................................................................................................

            string currency_code = CurrencyCode;
            if (CurrencyCode == "Eu") { currency_code = "EUR"; }
            else if (CurrencyCode == "$") { currency_code = "USD"; }
            else if (CurrencyCode == "рб") { currency_code = "RUR"; }
            else
            {
                //MessageBox.Show("Не подходящая валюта для рисков полиса!", "Предупреждение");
                //return false;
            }

            string InsProgram = "";
            string InsTerritory = "";
            int InsCountryID = 0;
            List<InsRisk> InsRisks = new List<InsRisk>();
            double InsSumma = 0;

            InsProgram = policy_tourist.policy_program;
            InsTerritory = policy_tourist.policy_territory;
            InsCountryID = policy_tourist.policy_country_id;
            InsSumma = policy_tourist.policy_summa;
            //InsRisks = policy_tourist.policy_risks;

            //for (int i = 0; i < policy_tourist.policy_risks.Count; i++)
            //{
            //    string risk_name = policy_tourist.policy_risks[i];
            //    InsRisks.Add(new InsRisk(risk_name, InsSumma, CurrencyCode));
            //}

            if (InsCountryID <= 0)
            {
                if (InsProgram == "C")
                {
                    if (InsTerritory == "T-I") InsCountryID = 105736; // WORLDWIDE  >>  33da5330-5cc7-4665-92e5-be37da33f2cf
                    if (InsTerritory == "T-IV") InsCountryID = 105736;
                    //if (InsTerritory == "T-V") InsCountryID = 105736;
                }
                if (InsProgram == "B")
                {
                    //if (InsTerritory == "T-I") InsCountryID = 27770; 
                    //if (InsTerritory == "T-IV") InsCountryID = 27770;
                    if (InsTerritory == "T-V") InsCountryID = 27770; // RUSSIA (OVER 90 KM FROM THE PLACE OF PERMANENT RESIDENCE)  >>  5278282f-8ad0-479b-8f3a-d3f224ac5175
                }
            }

            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.InstanceNumber == policy_tourist.InstanceNumber)
                {
                    for (int r = 0; r < item.policy_risks.Count; r++)
                    {
                        string risk_name = item.policy_risks[r];
                        bool bAdd = true;
                        for (int t = 0; t < InsRisks.Count; t++)
                        {
                            InsRisk risk = InsRisks[t];
                            if (string.Compare(risk.Name.ToUpper(), risk_name.ToUpper()) == 0)
                            {
                                bAdd = false;
                                break;
                            }
                        }
                        if (bAdd)
                        {
                            InsRisks.Add(new InsRisk(risk_name, item.policy_summa, currency_code));
                        }
                    }
                }
            }

            //....................................................................................................
            #endregion // Set basic policy data
            //....................................................................................................
            #region decode params ---
            //....................................................................................................
            /*
            string[] params_list = policy_params.Split(';');
            foreach (string param_string in params_list)
            {
                string param_name = "";
                string param_value = "";
                string[] param_array = param_string.Split('=');
                if (param_array.Length > 0) param_name = param_array[0].Trim();
                if (param_array.Length > 1) param_value = param_array[1].Trim();
                //....................................................................................................
                if (param_name.ToUpper() == "PROGRAM")
                {
                    if (param_value.ToUpper() == "C") InsProgram = param_value;  // C - Классик
                    if (param_value.ToUpper() == "B") InsProgram = param_value;  // B - Эконом
                    if (string.IsNullOrEmpty(InsProgram))
                    {
                        InsErrorMessage = "В параметрах не задана программа страхования";
                        return false;
                    }
                }
                //....................................................................................................
                if (param_name.ToUpper() == "TERRITORY")
                {
                    if (param_value.ToUpper() == "T-IV") InsTerritory = param_value;  // Все страны мира, включая Россию (исключая 90км от места постоянного проживания)
                    if (param_value.ToUpper() == "T-V") InsTerritory = param_value;   // Россия (свыше 90 км от места постоянного проживания)
                    if (string.IsNullOrEmpty(InsTerritory))
                    {
                        InsErrorMessage = "В параметрах не задана территория";
                        return false;
                    }
                }
                //....................................................................................................
                if (param_name.ToUpper() == "RISK")
                {
                    string[] risks_list = param_value.Split(',');
                    foreach (string risk_param in risks_list)
                    {
                        string risk = "";
                        if (risk_param.ToUpper() == "Мед".ToUpper()) risk = risk_param;        // Медицинские и иные расходы
                        if (risk_param.ToUpper() == "Несчас".ToUpper()) risk = risk_param;     // Несчастный случай 
                        if (risk_param.ToUpper() == "Отмена".ToUpper()) risk = risk_param;     // Отмена поездки 
                        if (risk_param.ToUpper() == "Граждан".ToUpper()) risk = risk_param;    // Гражданская ответственность 
                        if (risk_param.ToUpper() == "Имущ".ToUpper()) risk = risk_param;       // Имущество 
                        if (risk_param.ToUpper() == "Багаж".ToUpper()) risk = risk_param;      // Страхование багажа 

                        if (!string.IsNullOrEmpty(risk)) InsRisks.Add(risk);
                    }
                    if (InsRisks.Count == 0)
                    {
                        InsErrorMessage = "В параметрах не заданы риски для страхования";
                        return false;
                    }
                }
                // Классик C
                //Insist.risks.Add("Мед");     // Медицинские и иные расходы
                //Insist.risks.Add("Несчас");  // Несчастный случай 
                //Insist.risks.Add("Отмена");  // Отмена поездки
                //Insist.risks.Add("Граждан"); // Гражданская ответственность
                //Insist.risks.Add("Имущ");    // Имущество
                //Insist.risks.Add("Багаж");   // Страхование багажа
                // Эконом B
                //Insist.risks.Add("Мед");     // Медицинские и иные расходы
                //Insist.risks.Add("Несчас");  // Несчастный случай 
                //Insist.risks.Add("Имущ");    // Имущество
                //....................................................................................................
                if (param_name.ToUpper() == "SUMM")
                {
                    if (!string.IsNullOrEmpty(param_value))
                    {
                        try
                        {
                            InsSumma = double.Parse(param_value);
                        }
                        catch ( System.Exception ex )
                        {

                        }
                    }
                    else
                    {
                    //    InsErrorMessage = "В параметрах не задана сумма покрытия";
                    //    return false;
                    }
                }
                //....................................................................................................
            }
            */
            //....................................................................................................
            #endregion // decode params
            //....................................................................................................

            Insist = new InsAssist();

            bool isCancels = false;

            //Decimal med = 0;
            //Decimal cancel = 0;
            //Decimal tarifMed = Convert.ToDecimal(0.79);

            Insist.DG_code = DGCode;

            //....................................................................................................
            List<int> persons_keys = new List<int>();
            foreach (InsTouristItem tourist in policy_tourists)
            {
                persons_keys.Add(tourist.Tukey);
            }

            if (persons_keys == null || persons_keys.Count == 0)
            {
                //ShowMessage("Список страхователей пуст!");
                InsErrorMessage = "Список страхователей пуст";
                return false;
            }

            //....................................................................................................
            // проверка данных для страховки

            List<InsPerson> persons_list = new List<InsPerson>();
            
            for (int i = 0; i < ServiceRecordList.Count; i++)
            {
                ServiceRecord service = ServiceRecordList[i];
                if (persons_keys.IndexOf(service.tu_key) < 0) continue;

                string lname = service.TU_NAMELAT;
                string name = service.TU_FNAMELAT;
                int tu_key = service.tu_key;
                DateTime birdhday = service.TU_BIRTHDAY;

                //....................................................................................................
                InsPerson person = null;
                if (persons_list.Count == 0)
                {
                    person = new InsPerson(lname + " " + name, birdhday, tu_key);
                    persons_list.Add(person);
                }

                foreach (InsPerson inshured in persons_list)
                {
                    if ((inshured.tu_key == tu_key))
                    {
                        person = inshured;
                        break;
                    }
                }

                if (person == null)
                {
                    person = new InsPerson(lname + " " + name, birdhday, tu_key);
                    persons_list.Add(person);
                }

                //....................................................................................................
                /*
                if (service.DL_CODE == 777000695 || service.DL_CODE == service.AC_slkey)
                {
                    person.sumMed = tarifMed * Convert.ToInt32(service.DL_NDAYS) * Convert.ToDecimal(service.AC_Coef);
                    person.sumMedRb = person.sumMed * CurrencyCourse;
                    med = med + person.sumMed;
                }

                if (service.DL_CODE == 76636 || service.DL_CODE == 76636)
                {
                    decimal percent;
                    isCancels = true;
                    person.sumIns = Convert.ToDecimal(service.A1_NAME);
                    if (person.sumIns < Convert.ToDecimal(5000))
                    {
                        percent = Convert.ToDecimal(0.02);
                    }
                    else
                    {
                        percent = Convert.ToDecimal(0.035);
                    }

                    person.sumVal = person.sumIns * percent;
                    person.deductible = 0;
                    cancel = cancel + person.sumVal;
                    person.sumRb = person.sumVal * CurrencyCourse;
                }
                */
                //....................................................................................................
            }

            Insist.persons = persons_list;

            //....................................................................................................
            bool bFound = false;

            for (int i = 0; i < TouristRecordList.Count; i++)
            {
                TouristRecord tourist = TouristRecordList[i];
                if (persons_keys.IndexOf(tourist.TU_KEY) >= 0)
                {
                    if (tourist.AGE < 18) continue;

                    bFound = true;
                    if (tourist.TU_PHONE != null)
                    {
                        Insist.tel = tourist.TU_PHONE;
                    }
                    else
                    {
                        Insist.tel = string.Empty;
                    }

                    Insist.passport = tourist.TU_PASPORTTYPE + ' ' + tourist.TU_PASPORTNUM;
                    Insist.holder = tourist.TOURIST_NAME;
                    Insist.holderBirthday = tourist.TU_BIRTHDAY;
                    break;
                }
            }

            if (!bFound)
            {
                MessageBox.Show("Ни один из страхуемых не подходит для держателя полиса!", "Предупреждение");
                //InsErrorMessage = "Ни один из страхуемых не подходит для держателя полиса";
                return false;
            }

          //   {
         //       MessageBox.Show("Не указан номер паспорта для для держателя полиса!", "Предупреждение");
          //      return false;
         //   }
         //   if (string.IsNullOrEmpty(Insist.tel.Trim()))
          //  {
          //      MessageBox.Show("Не указан номер телефона для для держателя полиса!", "Предупреждение");
          //      return false;
            //}

            //....................................................................................................
            // проверка связки сервиса и туриста

            //Service ins_serv = null;
            //for (int i = 0; i < ServiceList.Count; i++) 
            //{
            //    Service serv = ServiceList[i]; 
            //    if ((serv.DlKey == 777000695 || serv.AC_slkey == 777000695 || serv.DL_CODE == serv.AC_slkey) && (persons_keys.IndexOf(serv.tu_key) >= 0))
            //    {
            //        ins_serv = serv;
            //        break;
            //    }
            //}

            ServiceRecord ins_service = null;
            for (int i = 0; i < ServiceRecordList.Count; i++) 
            {
                ServiceRecord service = ServiceRecordList[i];
                if (persons_keys.IndexOf(service.tu_key) >= 0)
                {
                    bool bMed = false;
                    foreach (InsTouristItem item in InsTouristList)
                    {
                        if (item.DlKey == service.DL_KEY)
                        {
                            if (item.HasRisk("Мед"))
                            {
                                bMed = true;
                                break;
                            }
                        }
                    }

                    //if ((service.DL_CODE == 777000949 || service.DL_CODE == 777000695 || service.AC_slkey == 777000695 || service.DL_CODE == service.AC_slkey))
                    //if ((service.DL_CODE == 777000695 || service.AC_slkey == 777000695 || service.DL_CODE == service.AC_slkey))
                    if (bMed)
                    {
                        ins_service = service;
                        break;
                    }
                }
            }

            if (ins_service == null)
            {
                MessageBox.Show("Ни один из страхуемых не связан с услугой страхования!", "Предупреждение");
                //InsErrorMessage = "Ни один из страхуемых не связан с услугой страхования";
                return false;
            }

            //....................................................................................................
            // формирование данных о страховке 

            Insist.program = InsProgram;

            ContractRecord dogovor = ContractRecordList[0];

            Insist.dateFrom = dogovor.DG_TURDATE.Date.AddDays(Convert.ToInt32(ins_service.DL_DAY) - 1);
            Insist.days = Convert.ToInt32(ins_service.DL_NDAYS);
            Insist.dateTo = Insist.dateFrom.Date.AddDays(Insist.days - 1);
            Insist.dateIsue = DateTime.Now.Date;

            Insist.terretory = InsTerritory;
            Insist.country_id = InsCountryID;

            //Insist.medicalsum = 50000;
            //Insist.tripsum = 0;
            //Insist.fligsum = 1000;
            //Insist.bagsum = 0;
            if (isCancels)
            {
                Insist.dop = Insist.dop + " Начало поездки : " + Insist.dateFrom.ToString().Substring(0, 10);
            }
            Insist.dop = Insist.dop + string.Format(" COVERED ONLY ({0}) DAYS", Insist.days);

            //....................................................................................................
            // риски страхования

            if (Insist.risks == null) Insist.risks = new List<InsRisk>();

            Insist.risks.Clear();

            Insist.risks = InsRisks;

            Insist.nomber = ""; // вносится по результатам оформления страховки
            //Insist.nomber = GenNomber();

            //Insist.medicalprem = med;
            //Insist.medicalpremRb = Insist.medicalprem * CurrencyCourse;
            //Insist.totalsum = med + cancel;
            //Insist.totalsumRb = Insist.totalsum * CurrencyCourse;
            if (dogovor.DG_RATE == "Eu")
            {
                Insist.curens = "EUR";
            }
            else if (dogovor.DG_RATE == "$")
            {
                Insist.curens = "USD";
            }
            else if (dogovor.DG_RATE == "рб")
            {
                Insist.curens = "RUR";
            }
            else
            {
                MessageBox.Show("Не подходящая валюта путевки!", "Предупреждение");
                return false;
                //Insist.curens = dogovor.DG_RATE;
            }

            //foreach (InsPerson person in persons_list)
            //{
            //    if (person.sumMed <= 0)
            //    {
            //        MessageBox.Show("У туриста " + person.Name + " нет медицинской страховки!" + (char)13 + (char)10 + "Либо выбрана неправильно по возрасту!", "Предупреждение");
            //        return false;
            //    }
            //}

            //if (ShowConfirm("Отредактировать даты с/по ?", "Редактирование страховки?"))
            //{
            // вызов формы редактирования дат
            //    frmDateCorrect.DayCorrect(ref Insist.dateFrom, ref Insist.dateTo);
            //}

            //....................................................................................................
            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Fill_Policy_Data()
        //----------------------------------------------------------------------------------------------------
        #region Annulate_Policy_Alfa()
        //----------------------------------------------------------------------------------------------------
        public bool Annulate_Policy_Alfa(string dgcode, string policy_number)
        {
            //....................................................................................................

            bool bAnnulated = AlfaInsAssist.Annulate_Policy(policy_number); // аннуляция полиса в системе страхования

            if (bAnnulated) // обновление данных в базе
            {
                AlfaInsAssist.AnnulateInsurance(dgcode, policy_number);  
                AlfaInsAssist.AddHistory(dgcode, policy_number);

                AlfaInsAssist.Remove_Policy( policy_number ); // Remove from FTP...

                MessageBox.Show("Аннулирован страховой полис номер " + policy_number + ".", "Сообщение", MessageBoxButtons.OK);
            }
            else
            {
                string msg = "Данные об аннуляции не получены.";
                if (!string.IsNullOrEmpty(AlfaInsAssist.ErrorMessage))
                    msg = msg + (char)13 + (char)10 + "Ошибка: " + AlfaInsAssist.ErrorMessage;
                MessageBox.Show(msg, "Сообщение", MessageBoxButtons.OK);
            }

            GetData(); // обновление данных

            //....................................................................................................
            return true;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // Annulate_Policy_Alfa()
        //----------------------------------------------------------------------------------------------------
        #endregion // Policy work
        //====================================================================================================

    }
}
