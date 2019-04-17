using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using WpfControlLibrary.Util;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model;
using WpfControlLibrary.Model.Voucher;


namespace WpfControlLibrary.ViewModel
{
    //====================================================================================================
    public class InsTouristItem : GroupGridItem 
    {
        //----------------------------------------------------------------------------------------------------
        private int _rowNumber;
        public int RowNumber
        {
            get { return _rowNumber; }
            set { SetValue(ref _rowNumber, value); }
        }

        private int _tukey;
        public int Tukey
        {
            get { return _tukey; }
            set { SetValue(ref _tukey, value); }
        }

        private string _touristName;
        public string TouristName
        {
            get { return _touristName; }
            set { SetValue(ref _touristName, value); }
        }

        private string _touristFullName;
        public string TouristFullName
        {
            get { return _touristFullName; }
            set { SetValue(ref _touristFullName, value); }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { SetValue(ref _fullName, value); }
        }

        private string _serviceName;
        public string ServiceName
        {
            get { return _serviceName; }
            set 
            {
                SetValue(ref _serviceName, value);
                ServiceText = _serviceName;
            }
        }
        private string _serviceNameCopy;

        private string _serviceText;
        public string ServiceText
        {
            get { if (NumberFlag && !string.IsNullOrEmpty(PolicyNumberString)) return PolicyNumberString; else return ServiceName; }
            set { SetValue(ref _serviceText, value); }
        }

        private int _dlKey;
        public int DlKey
        {
            get { return _dlKey; }
            set { SetValue(ref _dlKey, value); }
        }

        private int _svKey;
        public int SvKey
        {
            get { return _svKey; }
            set { SetValue(ref _svKey, value); }
        }

        private int _dlCode;
        public int DlCode
        {
            get { return _dlCode; }
            set { SetValue(ref _dlCode, value); }
        }
        private int _dlCodeCopy;

        private int _dlSubcode;
        public int DlSubcode
        {
            get { return _dlSubcode; }
            set { SetValue(ref _dlSubcode, value); }
        }
        private int _dlSubcodeCopy;

        private int _partnerKey;
        public int PartnerKey
        {
            get { return _partnerKey; }
            set { SetValue(ref _partnerKey, value); }
        }

        //....................................................................................................
        private int _instanceNumber;
        public int InstanceNumber
        {
            get { return _instanceNumber; }
            set { SetValue(ref _instanceNumber, value); }
        }

        private string _numberString;
        public string NumberString
        {
            get { return _numberString; }
            set { SetValue(ref _numberString, value); }
        }

        private DateTime _tourDate;
        public DateTime TourDate
        {
            get { return _tourDate; }
            set { SetValue(ref _tourDate, value); }
        }

        private int _nDays;
        public int NDays
        {
            get { return _nDays; }
            set { SetValue(ref _nDays, value); }
        }

        private int _nMen;
        public int NMen
        {
            get { return _nMen; }
            set { SetValue(ref _nMen, value); }
        }

        private DateTime _dateFrom;
        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set 
            { 
                SetValue(ref _dateFrom, value);
                DateFromString = _dateFrom.ToString("dd.MM.yy");
            }
        }
        private DateTime _dateFromCopy;

        private DateTime _dateTill;
        public DateTime DateTill
        {
            get { return _dateTill; }
            set 
            { 
                SetValue(ref _dateTill, value);
                DateTillString = _dateTill.ToString("dd.MM.yy");
            }

        }
        private DateTime _dateTillCopy;

        private string _dateFromString;
        public string DateFromString
        {
            get { return _dateFromString; }
            set { SetValue(ref _dateFromString, value); }
        }

        private string _dateTillString;
        public string DateTillString
        {
            get { return _dateTillString; }
            set { SetValue(ref _dateTillString, value); }
        }

        private decimal _netto;
        public decimal Netto
        {
            get { return _netto; }
            set
            {
                SetValue(ref _netto, value);
                NettoString = _netto.ToString("# ###.");
                decimal r = Math.Round(_netto, 0);
                decimal rr = Math.Round(_netto, 1);
                if (r == rr)
                    NettoString = _netto.ToString("# ###.");
                else
                    NettoString = _netto.ToString("# ###.##");
            }
        }
        private decimal _nettoCopy;

        private string _nettoString;
        public string NettoString
        {
            get { return _nettoString; }
            set { SetValue(ref _nettoString, value); }
        }

        private decimal _brutto;
        public decimal Brutto
        {
            get { return _brutto; }
            set
            {
                SetValue(ref _brutto, value);
                BruttoString = _brutto.ToString("# ###.");
                decimal r = Math.Round(_brutto, 0);
                decimal rr = Math.Round(_brutto, 1);
                if (r == rr)
                    BruttoString = _brutto.ToString("# ###.");
                else
                    BruttoString = _brutto.ToString("# ###.##");
            }
        }
        private decimal _bruttoCopy;

        private string _bruttoString;
        public string BruttoString
        {
            get { return _bruttoString; }
            set { SetValue(ref _bruttoString, value); }
        }

        private decimal _percent;
        public decimal Percent
        {
            get { return _percent; }
            set
            {
                SetValue(ref _percent, value);
                if (_percent > 0)
                {
                    decimal r = Math.Round(_percent, 0);
                    decimal rr = Math.Round(_percent, 1);
                    if (r == rr)
                        PercentString = _percent.ToString("F0");
                    else
                        PercentString = _percent.ToString("F1");
                }
                else
                {
                    PercentString = "";
                }
            }
        }

        private string _percentString;
        public string PercentString
        {
            get { return _percentString; }
            set { SetValue(ref _percentString, value); }
        }

        private decimal _summa;
        public decimal Summa
        {
            get { return _summa; }
            set
            {
                SetValue(ref _summa, value);
                //SummaString = _summa.ToString("F0");
                SummaString = _summa.ToString("# ###.");
            }
        }

        private string _summaString;
        public string SummaString
        {
            get { return _summaString; }
            set { SetValue(ref _summaString, value); }
        }

        private string _pricingString;
        public string PricingString
        {
            get { return _pricingString; }
            set { SetValue(ref _pricingString, value); }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { SetValue(ref _status, value); }
        }

        private string _policyNumberString;
        public string PolicyNumberString
        {
            get { return _policyNumberString; }
            set { SetValue(ref _policyNumberString, value); }
        }

        public bool PolicyNumberFlag
        {
            get { return !string.IsNullOrEmpty(PolicyNumberString); }
        }

        private bool _policyExistsFlag;
        public bool PolicyExistsFlag
        {
            get { return _policyExistsFlag; }
            set { SetValue(ref _policyExistsFlag, value); }
        }

        private bool _policyRowFlag;
        public bool PolicyRowFlag
        {
            get { return _policyRowFlag; }
            set { SetValue(ref _policyRowFlag, value); }
        }

        private bool _canEditFlag;
        public bool CanEditFlag
        {
            get { return _canEditFlag; }
            set { SetValue(ref _canEditFlag, value); }
        }

        private bool _editIconsFlag;
        public bool EditIconsFlag
        {
            get { return CanEditFlag && !PolicyExistsFlag && DlKey > 0; }
            set { SetValue(ref _editIconsFlag, value); }
        }

        public int ServiceCount;

            //....................................................................................................
        public string policy_params = "";
        public bool policy_params_flag;
        public string policy_params_error = "";

        public string policy_program = "";
        public string policy_territory = "";
        public int policy_country_id;
        public List<string> policy_risks;
        public double policy_summa = 0.0;

        //public bool policy_binding_flag;

        //....................................................................................................
        public bool _filterFlag;
        public bool FilterFlag
        {
            get { return _filterFlag; }
            set { SetValue(ref _filterFlag, value); }
        }

        public bool _editableFlag;
        public bool EditableFlag
        {
            get { return _editableFlag; }
            set { SetValue(ref _editableFlag, value); }
        }

        public bool _policyIconsFlag;
        public bool PolicyIconsFlag
        {
            get { return _policyIconsFlag; }
            set { SetValue(ref _policyIconsFlag, value); }
        }
        public bool PolicyIconsFlagCopy;

        public bool _addFlag;
        public bool AddFlag
        {
            get { return _addFlag; }
            set { SetValue(ref _addFlag, value); }
        }

        public bool _addingFlag;
        public bool AddingFlag
        {
            get { return _addingFlag; }
            set { SetValue(ref _addingFlag, value); }
        }

        public bool _checkControlFlag;
        public bool CheckControlFlag
        {
            get { return _checkControlFlag; }
            set { SetValue(ref _checkControlFlag, value); }
        }

        public bool _addControlFlag;
        public bool AddControlFlag
        {
            get { return _addControlFlag; }
            set { SetValue(ref _addControlFlag, value); }
        }

        public string _addVisibility;
        public string AddVisibility
        {
            get { if (AddControlFlag && AddingFlag) return "Visible"; else return "Collapsed"; }
            set { SetValue(ref _addVisibility, value); }
        }

        public bool _editFlag;
        public bool EditFlag
        {
            get { return _editFlag; }
            set { SetValue(ref _editFlag, value); }
        }

        public bool _nameEditFlag;
        public bool NameEditFlag
        {
            get { return _nameEditFlag; }
            set { SetValue(ref _nameEditFlag, value); }
        }

        public bool _selectedFlag;
        public bool SelectedFlag
        {
            get { return _selectedFlag; }
            set { SetValue(ref _selectedFlag, value); }
        }

        public bool _numberFlag;
        public bool NumberFlag
        {
            get { return _numberFlag; }
            set 
            { 
                SetValue(ref _numberFlag, value);
                if (_numberFlag) ServiceText = PolicyNumberString; else ServiceText = ServiceName; 
            }
        }

        //public bool _showEditFlag;
        //public bool ShowEditFlag
        //{
        //    get { return _showEditFlag; }
        //    set { SetValue(ref _showEditFlag, value); }
        //}

        //public bool _showAnnulateFlag;
        //public bool ShowAnnulateFlag
        //{
        //    get { return _showAnnulateFlag; }
        //    set { SetValue(ref _showAnnulateFlag, value); }
        //}

        public bool _firstRowFlag;
        public bool FirstRowFlag
        {
            get { return _firstRowFlag; }
            set { SetValue(ref _firstRowFlag, value); }
        }

        public bool _lastRowFlag;
        public bool LastRowFlag
        {
            get { return _lastRowFlag; }
            set { SetValue(ref _lastRowFlag, value); }
        }

        public bool EmptyRowFlag
        {
            get { return string.IsNullOrEmpty(TouristName) && string.IsNullOrEmpty(ServiceName); }
        }


        //----------------------------------------------------------------------------------------------------
        public InsTouristItem(int iDlKey, int iSvKey, string iTourist, string iService, bool iBegin = false, bool iMid = false, bool iEnd = false)
        {
            DlKey = iDlKey;
            iSvKey = SvKey;
            TouristName = iTourist;
            ServiceName = iService;

            BeginClass = iBegin;
            MidClass = iMid;
            EndClass = iEnd;

            EditableFlag = true;
            CanEditFlag = true;
            EditIconsFlag = true;

            policy_risks = new List<string>();
        }

        //----------------------------------------------------------------------------------------------------
        public void AccordEmpty()
        {
            if (DlKey > 0) return;

            PolicyNumberString = "";
            EditIconsFlag = false;
            PolicyIconsFlag = false;

            LastRowFlag = true;
            CheckControlFlag = true;
        }

        //----------------------------------------------------------------------------------------------------
        public void CalcPercent()
        {
            decimal sum = 0;
            decimal perc = 0;
            if (Brutto > 0)
            {
                sum = Brutto - Netto;
                if (sum != 0) perc = (decimal)(Math.Round((double)sum * 10000.0 / (double)Brutto) / 100.0);
            }
            Summa = sum;
            Percent = perc;
        }

        //----------------------------------------------------------------------------------------------------
        public bool HasRisk(string risk_name)
        {
            if (!policy_params_flag) return false;
            if (policy_risks == null) return false;

            foreach (string risk in policy_risks)
            {
                if (string.Equals(risk.ToUpper(), risk_name.ToUpper())) return true;
            }

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        public void CopyEdit()
        {
            _dateFromCopy = DateFrom;
            _dateTillCopy = DateTill;
            _dlCodeCopy = DlCode;
            _dlSubcodeCopy = DlSubcode;
            _serviceNameCopy = ServiceName;
            _nettoCopy = Netto;
            _bruttoCopy = Brutto;
        }

        //----------------------------------------------------------------------------------------------------
        public void CancelEdit()
        {
            DateFrom = _dateFromCopy;
            DateTill = _dateTillCopy;
            DlCode = _dlCodeCopy;
            DlSubcode = _dlSubcodeCopy;
            ServiceName = _serviceNameCopy;
            Netto = _nettoCopy;
            Brutto = _bruttoCopy;
        }

        //----------------------------------------------------------------------------------------------------
        public bool IsEditChanged()
        {
            if (DateFrom.Date != _dateFromCopy.Date) return true;
            if (DateTill.Date != _dateTillCopy.Date) return true;
            if (DlCode != _dlCodeCopy) return true;
            if (DlSubcode != _dlSubcodeCopy) return true;
            //if (ServiceName != _serviceNameCopy) return true;
            //if (Netto != _nettoCopy) return true;
            //if (Brutto != _bruttoCopy) return true;

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        public bool CheckToPolicyRow(int row_number)
        {
            if (PolicyExistsFlag) return false;
            if (RowNumber == row_number) return true;
            if (row_number == 0 && PolicyRowFlag) return true;

            return false;
        }

        //----------------------------------------------------------------------------------------------------
    }

    //====================================================================================================
    public class InsServiceItem : Data
    {
        //----------------------------------------------------------------------------------------------------
        private string _serviceName;
        public string ServiceName
        {
            get { return _serviceName; }
            set { SetValue(ref _serviceName, value); }
        }

        private string _serviceText;
        public string ServiceText
        {
            get { return _serviceText; }
            set { SetValue(ref _serviceText, value); }
        }

        private int _groupNumber;
        public int GroupNumber
        {
            get { return _groupNumber; }
            set { SetValue(ref _groupNumber, value); }
        }

        private bool _addFlag;
        public bool AddFlag
        {
            get { return _addFlag; }
            set { SetValue(ref _addFlag, value); }
        }

        private bool _filterFlag;
        public bool FilterFlag
        {
            get { return _filterFlag; }
            set { SetValue(ref _filterFlag, value); }
        }

        public int ID;
        public int SVKEY;
        public int CODE;
        public int Subcode1;
        public int Subcode2;
        public int PartnerKey;

        public double Netto;
        public double Brutto;
        public string Rate = "";
        public int ByDay = 0;

        //----------------------------------------------------------------------------------------------------
        public InsServiceItem(string iService, string iText)
        {
            ServiceName = iService;
            //ServiceText = iText;

        }

        //----------------------------------------------------------------------------------------------------
    }

    //====================================================================================================
    public class InsPolicyItem : Data
    {
        //----------------------------------------------------------------------------------------------------
        private string _policyNumber;
        public string PolicyNumber
        {
            get { return _policyNumber; }
            set { SetValue(ref _policyNumber, value); }
        }

        private string _policyStatus;
        public string PolicyStatus
        {
            get { return _policyStatus; }
            set { SetValue(ref _policyStatus, value); }
        }

        private int _tukey;
        public int Tukey
        {
            get { return _tukey; }
            set { SetValue(ref _tukey, value); }
        }

        //----------------------------------------------------------------------------------------------------
        public InsPolicyItem(string iNumber, string iStatus, int iTukey = 0)
        {
            PolicyNumber = iNumber;
            PolicyStatus = iStatus;
            Tukey = iTukey;
        }

        //----------------------------------------------------------------------------------------------------
    }

    //====================================================================================================
    public class InsGroupItem : Data
    {
        //----------------------------------------------------------------------------------------------------
        private int _groupNumber;
        public int GroupNumber
        {
            get { return _groupNumber; }
            set { SetValue(ref _groupNumber, value); }
        }

        private string _groupName;
        public string GroupName
        {
            get { return _groupName; }
            set { SetValue(ref _groupName, value); }
        }

        private int _groupCount;
        public int GroupCount
        {
            get { return _groupCount; }
            set { SetValue(ref _groupCount, value); }
        }

        //----------------------------------------------------------------------------------------------------
        public InsGroupItem(int iNumber, string iName)
        {
            GroupNumber = iNumber;
            GroupName = iName;
            GroupCount = 0;
        }

        //----------------------------------------------------------------------------------------------------
    }

    //====================================================================================================
    public class InsuranceViewModel : ViewModelBase
    {
        //----------------------------------------------------------------------------------------------------
        public string _DGCode;
        public string DGCode
        {
            get { return _DGCode; }
            set { SetValue(ref _DGCode, value); }
        }

        public bool _CodeReadOnly;
        public bool CodeReadOnly
        {
            get { return _CodeReadOnly; }
            set { SetValue(ref _CodeReadOnly, value); }
        }

        public string _currencyCode;
        public string CurrencyCode
        {
            get { return _currencyCode; }
            set { SetValue(ref _currencyCode, value); }
        }

        public string _currencyText;
        public string CurrencyText
        {
            get { if (!string.IsNullOrEmpty(CurrencyCode)) return "Валюта путевки:  " + CurrencyCode; else return ""; }
            set { SetValue(ref _currencyText, value); }
        }

        public List<InsTouristItem> Tourists { get; set; }
        public List<InsTouristItem> InsTouristList { get; set; }
        public List<InsServiceItem> InsServiceList { get; set; }
        public List<InsServiceItem> InsGroupServiceList { get; set; }
        public List<InsGroupItem> InsGroupList { get; set; }
        public List<InsPolicyItem> PolicyList { get; set; }
        public List<InsTouristItem> PolicyTouristList { get; set; }


        //public ObservableCollection<InshurService> ServiceList { get; set; }
        public ObservableCollection<Service> ServiceList { get; set; }
        public ObservableCollection<TouristForInshur> TouristList { get; set; }

        //private List<InsuranceItem> _insuranceList;
        //public List<InsuranceItem> InsuranceList
        //{
        //    get { return _insuranceList; }
        //    set { SetValue(ref _insuranceList, value); }
        //}

        private string _addButtonText;
        public string AddButtonText
        {
            get { if (!string.IsNullOrEmpty(_addButtonText)) return _addButtonText; else return "Выбрать  туриста  и  добавить  услугу"; }
            set { SetValue(ref _addButtonText, value); }
        }

        private string _cabinetButtonText;
        public string CabinetButtonText
        {
            get { if (!string.IsNullOrEmpty(_cabinetButtonText)) return _cabinetButtonText; else return "Выписать  и  выложить  в  ЛК"; }
            set { SetValue(ref _cabinetButtonText, value); }
        }

        private int _policyInstanceCount;
        public int PolicyInstanceCount
        {
            get { return _policyInstanceCount; }
            set { SetValue(ref _policyInstanceCount, value); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { SetValue(ref _isEmpty, value); }
        }

        private bool _withPolicyFlag;
        public bool WithPolicyFlag
        {
            get { return _withPolicyFlag; }
            set { SetValue(ref _withPolicyFlag, value); }
        }

        private bool _withoutPolicyFlag;
        public bool WithoutPolicyFlag
        {
            get { return _withoutPolicyFlag; }
            set { SetValue(ref _withoutPolicyFlag, value); }
        }

        public bool PartnerFlag { get; set; }

        //----------------------------------------------------------------------------------------------------
        public InsuranceViewModel()
        {
            CodeReadOnly = true;

            Tourists = new List<InsTouristItem>();
            InsTouristList = new List<InsTouristItem>();
            InsServiceList = new List<InsServiceItem>();
            InsGroupServiceList = new List<InsServiceItem>();
            InsGroupList = new List<InsGroupItem>();
            PolicyList = new List<InsPolicyItem>();
            PolicyTouristList = new List<InsTouristItem>();

            //ServiceList = new ObservableCollection<InshurService>();
            ServiceList = new ObservableCollection<Service>();
            TouristList = new ObservableCollection<TouristForInshur>();

            //....................................................................................................
            /*
            InsTouristList.Add(new InsTouristItem(1, 1, "Петров А.С.", "Мед. страховка"));
            InsTouristList.Add(new InsTouristItem(2, 2, "", "Страховка багажа"));
            InsTouristList.Add(new InsTouristItem(3, 3, "", "Страховка от невыезда", false, false, true));
            InsTouristList.Add(new InsTouristItem(4, 4, "Сидорова Е.Н.", "Мед. страховка"));
            InsTouristList.Add(new InsTouristItem(5, 5, "", "Страховка багажа"));
            InsTouristList.Add(new InsTouristItem(6, 6, "", "Страховка от невыезда", false, false, true));
            InsTouristList.Add(new InsTouristItem(7, 7, "Петров Д.А.", "Мед. страховка"));
            InsTouristList.Add(new InsTouristItem(8, 8, "", "Страховка от невыезда", false, false, true));
            */
            //InsServiceList.Add(new InsServiceItem("Мед. страховка", "Медицинская страховка - 50 000"));
            //InsServiceList.Add(new InsServiceItem("Страховка багажа", "Страховка имущества - 1 000"));
            //InsServiceList.Add(new InsServiceItem("Страховка от невыезда", "Страховка от невыезда в круиз - 5 000"));
            /*
            InsServiceList.Add(new InsServiceItem("мед. страховка", "мед. страховка до 64 лет, Весь мир, Круизы"));
            InsServiceList.Add(new InsServiceItem("мед. страховка", "мед. страховка до 64 лет, Европа"));
            InsServiceList.Add(new InsServiceItem("мед. страховка", "мед. страховка до 64 лет, Россия"));
            InsServiceList.Add(new InsServiceItem("мед. страховка", "мед. страховка до 65-70 лет, Весь мир, Круизы"));
            InsServiceList.Add(new InsServiceItem("мед. страховка", "мед. страховка до 65-70 лет, Европа"));
            InsServiceList.Add(new InsServiceItem("мед. страховка", "мед. страховка до 65-70 лет, Россия"));
            InsServiceList.Add(new InsServiceItem("от невыезда", "Отмена поездки, покрытие 500"));
            InsServiceList.Add(new InsServiceItem("от невыезда", "Отмена поездки, покрытие 600"));
            InsServiceList.Add(new InsServiceItem("от невыезда", "Отмена поездки, покрытие 700"));
            */
        }

        //----------------------------------------------------------------------------------------------------
        public void SetServices(ObservableCollection<Service> Services)
        {
            foreach (Service svc in Services)
            {
                if (svc.SvKey == 6) // SvType.Insur
                {
                    InshurService ins_svc = new InshurService(svc);
                    ServiceList.Add(ins_svc);
                    if (ins_svc.Tourists != null && ins_svc.Tourists.Count > 0)
                    {
                        foreach (TouristForInshur tourist in ins_svc.Tourists)
                            AddTourist(tourist);
                    }
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void AddTourist(TouristForInshur tourist)
        {
            foreach (TouristForInshur tst in TouristList)
            {
                bool bEqual = true;
                if (tst.FirstName != tourist.FirstName) bEqual = false;
                if (tst.SecondName != tourist.SecondName) bEqual = false;
                if (bEqual)
                {
                    tst.ServiceCount++;
                    return;
                }
            }

            tourist.ServiceCount = 1;
            TouristList.Add(tourist);
        }

        //----------------------------------------------------------------------------------------------------
        public void SetVoucherData(Voucher voucher)
        {
            if (voucher == null) return;

            DGCode = voucher.DgCode;
            //CurrencyCode = "EUR";

            //SetServices(voucher.ServiceList);
            SetServices(voucher.InshurServiceList);

            //AccordData();
        }

        //----------------------------------------------------------------------------------------------------
        public void SetPoliciesData(DataTable policies_table)
        {
            if (policies_table == null) return;
            if (policies_table.Rows.Count == 0) return;

            PolicyList.Clear();
            for (int i = 0; i < policies_table.Rows.Count; i++)
            {
                DataRow row = policies_table.Rows[i];
                try
                {
                    string number = row.Field<string>("INS_Numder");
                    string status = row.Field<string>("status");
                    //int tukey = row.Field<int>("INS_tukey");
                    InsPolicyItem policy = new InsPolicyItem(number, status);
                    PolicyList.Add(policy);
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        public void SetTouristsData(DataTable tourists_table)
        {
            if (tourists_table == null) return;
            if (tourists_table.Rows.Count == 0) return;

            PolicyTouristList.Clear();
            for (int i = 0; i < tourists_table.Rows.Count; i++)
            {
                DataRow row = tourists_table.Rows[i];

                try
                {
                    string name = row.Field<string>("TOURIST_NAME");

                    InsTouristItem tourist = new InsTouristItem(0, 0, name, "");
                    PolicyTouristList.Add(tourist);

                    tourist.Tukey = row.Field<int>("TU_KEY");
                    tourist.PolicyNumberString = row.Field<string>("INS_NUMBER");

                    //tourist.TOURIST_NAME = row.Field<string>("TOURIST_NAME");
                    //tourist.DG_CODE = row.Field<string>("DG_CODE");
                    //tourist.TU_BIRTHDAY = row.Field<DateTime>("TU_BIRTHDAY");
                    //tourist.DG_NDAY = row.Field<double>("DG_NDAY");
                    //tourist.DG_TURDATE = row.Field<DateTime>("DG_TURDATE");
                    //tourist.TU_PHONE = row.Field<string>("TU_PHONE");
                    //tourist.TU_PASPORTNUM = row.Field<string>("TU_PASPORTNUM");
                    //tourist.TU_PASPORTTYPE = row.Field<string>("TU_PASPORTTYPE");
                    //tourist.AGE = row.Field<int>("AGE");
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        public void SetServicesData(DataTable services_table)
        {
            if (services_table == null) return;
            if (services_table.Rows.Count == 0) return;

            InsGroupList.Clear();
            InsGroupList.Add(new InsGroupItem(1, "Мед. страховка Все страны"));
            InsGroupList.Add(new InsGroupItem(2, "Мед. страховка Европа"));
            InsGroupList.Add(new InsGroupItem(3, "Мед. страховка Россия"));
            InsGroupList.Add(new InsGroupItem(4, "Отмена поездки"));
            InsGroupList.Add(new InsGroupItem(5, "Страхование багажа"));
            InsGroupList.Add(new InsGroupItem(6, "Прочие"));

            InsServiceList.Clear();
            InsGroupServiceList.Clear();
            for (int i = 0; i < services_table.Rows.Count; i++)
            {
                DataRow row = services_table.Rows[i];

                try
                {
                    //[CS_SVKEY],[CS_CODE],[CS_SUBCODE1],[CS_SUBCODE2] 
                    //[SL_NAME],[A1_NAME] [LL_NAME]
                    //[CS_COSTNETTO],[CS_COST],[CS_RATE],[CS_BYDAY],[CS_ID] 

                    int id = row.Field<int>("CS_ID");
                    int svkey = row.Field<int>("CS_SVKEY");
                    int code = row.Field<int>("CS_CODE");
                    int subcode1 = row.Field<int>("CS_SUBCODE1");
                    int subcode2 = row.Field<int>("CS_SUBCODE2");
                    int partner = row.Field<int>("CS_PRKEY");

                    string service_name = row.Field<string>("SL_NAME");
                    string full_name = row.Field<string>("LL_NAME");

                    double netto = row.Field<double>("CS_COSTNETTO");
                    double brutto = row.Field<double>("CS_COST");
                    string rate = row.Field<string>("CS_RATE");
                    int byday = row.Field<short>("CS_BYDAY");

                    InsServiceItem service = new InsServiceItem(full_name, full_name);
                    service.ID = id;
                    service.SVKEY = svkey;
                    service.CODE = code;
                    service.Subcode1 = subcode1;
                    service.Subcode2 = subcode2;
                    service.PartnerKey = partner;
                    

                    service.Netto = netto;
                    service.Brutto = brutto;
                    service.Rate = rate;
                    service.ByDay = byday;

                    InsServiceList.Add(service);

                    foreach (InsGroupItem item in InsGroupList)
                    {
                        if (service.ServiceName.IndexOf(item.GroupName) >= 0)
                        {
                            service.GroupNumber = item.GroupNumber;
                            break;
                        }
                    }
                    if (service.GroupNumber <= 0)
                    {
                        InsGroupItem item = InsGroupList[InsGroupList.Count - 1];
                        service.GroupNumber = item.GroupNumber;
                        item.GroupCount++;
                    }
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }

            InsGroupItem group = InsGroupList[InsGroupList.Count - 1];
            if (group.GroupCount == 0) InsGroupList.RemoveAt(InsGroupList.Count - 1);
        }

        //----------------------------------------------------------------------------------------------------
        public void SetServicesList(DataTable services_table)
        {
            SetServicesListData(services_table);

            if (PolicyTouristList == null) return;

            for (int i = 0; i < PolicyTouristList.Count; i++)
            {
                InsTouristItem tourist = PolicyTouristList[i];
                bool bFound = false;
                for (int s = 0; s < InsTouristList.Count; s++)
                {
                    InsTouristItem item = InsTouristList[s];
                    if (item.Tukey == tourist.Tukey)
                    {
                        bFound = true;
                        break;
                    }
                }

//bFound = false; // for testing
                if (!bFound)
                {
                    InsTouristItem item = new InsTouristItem(0, 0, tourist.TouristName, "", false, false, true);
                    item.Tukey = tourist.Tukey;
                    item.RowNumber = InsTouristList.Count + 1;
                    item.AccordEmpty();
                    InsTouristList.Add(item);

                    WithoutPolicyFlag = true;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        public void SetServicesListData(DataTable services_table)
        {
            if (services_table == null) return;
            if (services_table.Rows.Count == 0) return;

            InsTouristList.Clear();

            //....................................................................................................
            if (Tourists == null) Tourists = new List<InsTouristItem>();
            Tourists.Clear();

            WithPolicyFlag = false;
            WithoutPolicyFlag = false;

            for (int i = 0; i < services_table.Rows.Count; i++)
            {
                DataRow row = services_table.Rows[i];

                try
                {
                    bool bFirst = (i == 0);
                    bool bLast = (i >= services_table.Rows.Count - 1);

                    //....................................................................................................
                    int DL_KEY = row.Field<int>("DL_KEY");
                    int DL_TRKEY = row.Field<int>("DL_TRKEY");
                    int DL_SVKEY = row.Field<int>("DL_SVKEY");
                    string TU_NAMERUS = row.Field<string>("TU_NAMERUS");
                    string TU_FNAMERUS = row.Field<string>("TU_FNAMERUS");
                    int TU_TUKEY = row.Field<int>("TU_TUKEY");

                    bool bFound = false;
                    foreach (InsTouristItem tourist in Tourists)
                        if (tourist.Tukey == TU_TUKEY) { tourist.ServiceCount++; bFound = true; break; }

                    //....................................................................................................
                    if (!bFound)
                    {
                        bLast = true;
                        InsTouristItem item = new InsTouristItem(DL_KEY, DL_SVKEY, TU_FNAMERUS, TU_NAMERUS, false, false, false);

                        item.RowNumber = Tourists.Count + 1;
                        item.Tukey = TU_TUKEY;
                        item.ServiceCount = 1;

                        Tourists.Add(item);
                    }
                    //....................................................................................................
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }

            //....................................................................................................
            bool bFirstRow = true;
            int tourist_count = 0;
            foreach (InsTouristItem tourist in Tourists)
            {
                tourist_count++;
                string first_name = tourist.TouristName;
                string second_name = tourist.ServiceName;
                string tourist_name = second_name + " " + first_name;
                int service_count = 0;

                for (int i = 0; i < services_table.Rows.Count; i++)
                {
                    DataRow row = services_table.Rows[i];

                    try
                    {
                        // DL_DGCOD,DL_KEY,DL_TRKEY,DL_SVKEY,DL_NAME,DL_DAY,DL_CODE,DL_SUBCODE1";
                        // DL_NMEN,DL_NDAYS,DL_COST,DL_BRUTTO,DL_DGKEY,TU_TUKEY,TU_NAMERUS, TU_FNAMERUS ";

                        //....................................................................................................
                        string DL_DGCOD = row.Field<string>("DL_DGCOD");
                        int DL_KEY = row.Field<int>("DL_KEY");
                        int DL_TRKEY = row.Field<int>("DL_TRKEY");
                        int DL_SVKEY = row.Field<int>("DL_SVKEY");
                        string DL_NAME = row.Field<string>("DL_NAME");
                        string TU_NAMERUS = row.Field<string>("TU_NAMERUS");
                        string TU_FNAMERUS = row.Field<string>("TU_FNAMERUS");
                        int DL_CODE = row.Field<int>("DL_CODE");
                        int DL_SUBCODE1 = row.Field<int>("DL_SUBCODE1");
                        int PARTNER_KEY = row.Field<int>("DL_PARTNERKEY");
                        //int DL_DAY = row.Field<int>("DL_DAY");
                        //int DL_NMEN = row.Field<int>("DL_NMEN");
                        DateTime DL_TURDATE = row.Field<DateTime>("DL_TURDATE");
                        int DL_NDAYS = row.Field<Int16>("DL_NDAYS");
                        int DL_NMEN = row.Field<Int16>("DL_NMEN");
                        //decimal DL_COST = row.Field<decimal>("DL_COST");
                        //decimal DL_BRUTTO = row.Field<decimal>("DL_BRUTTO");
                        decimal DL_COST = (decimal)row.Field<decimal>("NETTO");
                        decimal DL_BRUTTO = (decimal)row.Field<double>("BRUTTO");
                        int DL_DGKEY = row.Field<int>("DL_DGKEY");
                        int TU_TUKEY = row.Field<int>("TU_TUKEY");

                        //....................................................................................................
                        //bool bFirst = (i == 0);
                        //bool bLast = (i >= services_table.Rows.Count - 1);

                        if (tourist.Tukey == TU_TUKEY)
                        {
                            service_count++;
                            string service_name = DL_NAME;
                            bool bFirst = (service_count == 1);
                            bool bLast = (service_count >= tourist.ServiceCount);
                            string name = tourist_name;
                            //if (bLast) name = "";
                            if (!bFirst) name = "";
                            InsTouristItem item = new InsTouristItem(DL_KEY, DL_SVKEY, name, service_name, false, false, bLast);

                            item.RowNumber = InsTouristList.Count + 1;
                            item.Tukey = TU_TUKEY;
                            item.TouristFullName = name;
                            item.FullName = tourist_name;
                            item.NumberString = ""; // svc.NumberString;
                            item.TourDate = DL_TURDATE;
                            item.NDays = DL_NDAYS;
                            item.NMen = DL_NMEN;

                            item.DateFrom = DL_TURDATE;
                            item.DateTill = DL_TURDATE.Date.AddDays(DL_NDAYS - 1);
                            //item.DateFromString = DL_TURDATE.ToString("dd.MM.yy");
                            //item.DateTillString = DL_TURDATE.Date.AddDays(DL_NDAYS - 1).ToString("dd.MM.yy");
                            item.Netto = DL_COST;
                            item.Brutto = DL_BRUTTO;
                            //item.Summa = svc.Summa;
                            //item.Percent = svc.Percent;
                            item.CalcPercent();
                            item.DlCode = DL_CODE;
                            item.DlSubcode = DL_SUBCODE1;
                            item.PartnerKey = PARTNER_KEY;
                            item.PolicyNumberString = "";
                            //item.Status = svc.Status;
                            item.CheckControlFlag = bFirst;
                            //item.AddControlFlag = bFirst;

                            bool bLastRow = (tourist_count >= TouristList.Count) && (service_count >= tourist.ServiceCount);
                            if (bFirstRow) item.FirstRowFlag = true;
                            if (bLastRow) item.LastRowFlag = true;

                            if (service_count == 1)
                            {
                                bool bPolicy = false;
                                foreach (InsTouristItem tst in PolicyTouristList)
                                {
                                    bool bTourist = false;
                                    string t_name = tst.TouristName;
                                    //if (t_name.IndexOf(first_name) >= 0) bTourist = true;
                                    //if (t_name.IndexOf(second_name) >= 0) bTourist = true;
                                    if (t_name.IndexOf(first_name) >= 0 && t_name.IndexOf(second_name) >= 0) bTourist = true;
                                    if (bTourist && !string.IsNullOrEmpty(tst.PolicyNumberString))
                                    {
                                        item.PolicyNumberString = tst.PolicyNumberString;
                                        item.Status = ""; // "Статус страховки...";
                                        foreach (InsPolicyItem policy in PolicyList)
                                        {
                                            if (policy.PolicyNumber == item.PolicyNumberString)
                                            {
                                                item.PolicyExistsFlag = true;
                                                bPolicy = true;
                                                if (!string.IsNullOrEmpty(policy.PolicyStatus))
                                                    item.Status = policy.PolicyStatus;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                                item.PolicyRowFlag = true;

                                if (bPolicy)
                                    WithPolicyFlag = true;
                                else
                                    WithoutPolicyFlag = true;
                            }

                            if (service_count == 2 && InsTouristList.Count > 0)
                            {
                                item.TouristName = first_name;
                                InsTouristItem item1 = InsTouristList[InsTouristList.Count - 1];
                                item1.TouristName = second_name;
                            }

                            InsTouristList.Add(item);
                            bFirstRow = false;
                        }
                        //....................................................................................................
                    }
                    catch (System.Exception ex)
                    {
                        string msg = ex.Message;
                    }
                }
            }

            //....................................................................................................
            for (int i = 0; i < InsTouristList.Count; i++)
            {
                InsTouristItem item = InsTouristList[i];
                if (!PartnerFlag)
                {
                    item.NettoString = "";
                    item.SummaString = "";
                    item.PercentString = "";
                }
            }

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        public void AccordData()
        {
            InsTouristList.Clear();
            //InsServiceList.Clear();

            bool bFirstRow = true;
            int tourist_count = 0;
            foreach (TouristForInshur tourist in TouristList)
            {
                tourist_count++;
                string first_name = tourist.FirstName;
                string second_name = tourist.SecondName;
                string tourist_name = tourist.SecondName + " " + tourist.FirstName;
                int service_count = 0;
                foreach (InshurService svc in ServiceList)
                {
                    bool bFound = false;
                    if (svc.Tourists != null)
                    {
                        foreach (TouristForInshur tst in svc.Tourists)
                        {
                            bool bEqual = true;
                            if (tst.FirstName != tourist.FirstName) bEqual = false;
                            if (tst.SecondName != tourist.SecondName) bEqual = false;
                            if (bEqual)
                            {
                                bFound = true;
                                break;
                            }
                        }
                    }
                    if (bFound)
                    {
                        string service_name = svc.ServiceName;
                        service_count++;
                        bool bFirst = (service_count == 1);
                        bool bLast = (service_count >= tourist.ServiceCount);
                        string name = tourist_name;
                        //if (bLast) name = "";
                        if (!bFirst) name = "";
                        InsTouristItem item = new InsTouristItem(svc.DlKey, svc.SvKey, name, service_name, false, false, bLast);

                        item.RowNumber = InsTouristList.Count + 1;
                        item.Tukey = tourist_count;
                        item.NumberString = svc.NumberString;
                        item.Netto = svc.Netto;
                        item.Brutto = svc.Brutto;
                        item.Percent = svc.Percent;
                        item.Summa = svc.Summa;
                        item.Status = svc.Status;
                        item.PolicyNumberString = ""; // Номер страховки...";
                        item.AddControlFlag = bFirst;

                        if (bFirstRow)
                        {
                            //item.ShowEditFlag = true;
                            //item.ShowAnnulateFlag = true;
                            item.FirstRowFlag = true;
                        }
                        bool bLastRow = (tourist_count >= TouristList.Count) && (service_count >= tourist.ServiceCount);
                        if (bLastRow)
                        {
                            item.LastRowFlag = true;
                        }

                        if (service_count == 1)
                        {
                            foreach (InsTouristItem tst in PolicyTouristList)
                            {
                                bool bTourist = false;
                                string t_name = tst.TouristName;
                                if (t_name.IndexOf(first_name) >= 0) bTourist = true;
                                if (t_name.IndexOf(second_name) >= 0) bTourist = true;
                                if (bTourist)
                                {
                                    item.PolicyNumberString = tst.PolicyNumberString;
                                    item.Status = ""; // "Статус страховки...";
                                    foreach (InsPolicyItem policy in PolicyList)
                                    {
                                        if (policy.PolicyNumber == item.PolicyNumberString)
                                        {
                                            if (!string.IsNullOrEmpty(policy.PolicyStatus))
                                                item.Status = policy.PolicyStatus;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }

                        if (service_count == 2 && InsTouristList.Count > 0)
                        {
                            item.TouristName = first_name;
                            InsTouristItem item1 = InsTouristList[InsTouristList.Count - 1];
                            item1.TouristName = second_name;
                        }

                        InsTouristList.Add(item);
                        bFirstRow = false;
                    }
                }
                
            }

          //  InsTouristItem empty_item = new InsTouristItem(0, 0, "", "", false, false, false);
          //  InsTouristList.Add(empty_item);
          //  InsTouristList.Add(empty_item);
        }

        //----------------------------------------------------------------------------------------------------
    }

    //====================================================================================================
}

