using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;


namespace WpfControlLibrary.ViewModel
{
    //====================================================================================================
    #region InsCommand
    //====================================================================================================
    public class InsCommand : ICommand
    {
        //----------------------------------------------------------------------------------------------------
        public InsCommand(Action execute)
        {
        }
        public InsCommand(Action execute, Func<bool> canExecute)
        {
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public virtual void Execute(object parameter)
        {

        }
        public void RaiseCanExecuteChanged()
        {

        }
        //----------------------------------------------------------------------------------------------------
    }
    //====================================================================================================
    #endregion // InsCommand
    //====================================================================================================

    //====================================================================================================
    #region InsControlServiceItem
    //====================================================================================================
    public class InsControlServiceItem : Data
    {
        //----------------------------------------------------------------------------------------------------
        private int _rowID;
        public int RowID
        {
            get { return _rowID; }
            set { SetValue(ref _rowID, value); }
        }

        private int _rowNumber;
        public int RowNumber
        {
            get { return _rowNumber; }
            set { SetValue(ref _rowNumber, value); }
        }

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

        private int _serviceCode;
        public int ServiceCode
        {
            get { return _serviceCode; }
            set { SetValue(ref _serviceCode, value); }
        }

        private int _serviceSubcode1;
        public int ServiceSubcode1
        {
            get { return _serviceSubcode1; }
            set { SetValue(ref _serviceSubcode1, value); }
        }

        private DateTime _dateFrom;
        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set
            {
                SetValue(ref _dateFrom, value);
                DateFromString = _dateFrom.ToString("dd.MM.yyyy");
                AccordActPeriod();
            }
        }

        private string _dateFromString;
        public string DateFromString
        {
            get { return _dateFromString; }
            set { SetValue(ref _dateFromString, value); }
        }

        private DateTime _dateTill;
        public DateTime DateTill
        {
            get { return _dateTill; }
            set
            {
                SetValue(ref _dateTill, value);
                DateTillString = _dateTill.ToString("dd.MM.yyyy");
                AccordActPeriod();
            }
        }

        private string _dateTillString;
        public string DateTillString
        {
            get { return _dateTillString; }
            set { SetValue(ref _dateTillString, value); }
        }

        private string _actPeriodString;
        public string ActPeriodString
        {
            get { return _actPeriodString; }
            set { SetValue(ref _actPeriodString, value); }
        }

        private int _priceByDay;
        public int PriceByDay
        {
            get { return _priceByDay; }
            set 
            { 
                SetValue(ref _priceByDay, value);
                СostPeriodString = "";
                if (_priceByDay == 0) СostPeriodString = "период";
                if (_priceByDay == 2) СostPeriodString = "день";
            }
        }

        private string _costPeriodString = "";
        public string СostPeriodString
        {
            get { return _costPeriodString; }
            set { SetValue(ref _costPeriodString, value); }
        }

        private string _costRateString = "";
        public string СostRateString
        {
            get { return _costRateString; }
            set { SetValue(ref _costRateString, value); }
        }
        
        private List<String> _currencyList;
        public List<String> CurrencyList
        {
            get { return _currencyList; }
            set { SetValue(ref _currencyList, value); }
        }

        private int _currencyIndex;
        public int CurrencyIndex
        {
            get { return _currencyIndex; }
            set { SetValue(ref _currencyIndex, value); }
        }

        private string _currencySelectedValue;
        public string CurrencySelectedValue
        {
            get { return _currencySelectedValue; }
            set { SetValue(ref _currencySelectedValue, value); }
        }

        private bool _netto_flag;
        private decimal _netto;
        public decimal Netto
        {
            get { return _netto; }
            set
            {
                _netto_flag = true;
                SetValue(ref _netto, value);
                if (!_nettoString_flag)
                {

                    NettoString = _netto.ToString("# ###.");
                    if (_netto > 0)
                    {
                        decimal r = Math.Round(_netto, 0);
                        decimal rr = Math.Round(_netto, 1);
                        decimal rrr = Math.Round(_netto, 2);
                        if (r == rr && r == rrr)
                            NettoString = _netto.ToString("# ##0.").Trim();
                        else if (rr == rrr)
                            NettoString = _netto.ToString("# ##0.#").Trim();
                        else
                            NettoString = _netto.ToString("# ##0.##").Trim();
                    }
                    else
                    {
                        NettoString = "";
                    }
                }
                _netto_flag = false;
                CalcPercent();
            }
        }

        private bool _nettoString_flag;
        private string _nettoString = "";
        public string NettoString
        {
            get { return _nettoString; }
            set 
            {
                _nettoString_flag = true;
                SetValue(ref _nettoString, value);
                if (!_netto_flag) Read_Netto();
                _nettoString_flag = false;
            }
        }

        private bool _brutto_flag;
        private decimal _brutto;
        public decimal Brutto
        {
            get { return _brutto; }
            set
            {
                _brutto_flag = true;
                SetValue(ref _brutto, value);
                if (!_bruttoString_flag)
                {
                    BruttoString = _brutto.ToString("# ###.");
                    if (_brutto > 0)
                    {
                        decimal r = Math.Round(_brutto, 0);
                        decimal rr = Math.Round(_brutto, 1);
                        decimal rrr = Math.Round(_brutto, 2);
                        if (r == rr && r == rrr)
                            BruttoString = _brutto.ToString("# ##0.").Trim();
                        else if (rr == rrr)
                            BruttoString = _brutto.ToString("# ##0.#").Trim();
                        else
                            BruttoString = _brutto.ToString("# ##0.##").Trim();
                    }
                    else
                    {
                        BruttoString = "";
                    }
                }
                _brutto_flag = false;
                CalcPercent();
            }
        }

        private bool _bruttoString_flag;
        private string _bruttoString = "";
        public string BruttoString
        {
            get { return _bruttoString; }
            set 
            {
                _bruttoString_flag = true;
                SetValue(ref _bruttoString, value); 
                if (!_brutto_flag) Read_Brutto();
                _bruttoString_flag = false;
            }

        }

        private int _partnerKey;
        public int PartnerKey
        {
            get { return _partnerKey; }
            set { SetValue(ref _partnerKey, value); }
        }

        private string _partnerString;
        public string PartnerString
        {
            get { return _partnerString; }
            set { SetValue(ref _partnerString, value); }
        }

        private List<string> _partnerList;
        public List<string> PartnerList
        {
            get { return _partnerList; }
            set { SetValue(ref _partnerList, value); }
        }

        private List<int> _partnerKeyList;
        public List<int> PartnerKeyList
        {
            get { return _partnerKeyList; }
            set { SetValue(ref _partnerKeyList, value); }
        }

        private int _partnerIndex;
        public int PartnerIndex
        {
            get { return _partnerIndex; }
            set { SetValue(ref _partnerIndex, value); }
        }

        private string _partnerSelectedValue;
        public string PartnerSelectedValue
        {
            get { return _partnerSelectedValue; }
            set { SetValue(ref _partnerSelectedValue, value); }
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
                SummaString = _summa.ToString("# ###.");
                if (_summa > 0)
                {
                    decimal r = Math.Round(_summa, 0);
                    decimal rr = Math.Round(_summa, 1);
                    decimal rrr = Math.Round(_summa, 2);
                    if (r == rr && r == rrr)
                        SummaString = _summa.ToString("# ##0.").Trim();
                    else if (rr == rrr)
                        SummaString = _summa.ToString("# ##0.#").Trim();
                    else
                        SummaString = _summa.ToString("# ##0.##").Trim();
                }
                else
                {
                    SummaString = "";
                }

            }
        }

        private string _summaString;
        public string SummaString
        {
            get { return _summaString; }
            set { SetValue(ref _summaString, value); }
        }

        private string _koefString;
        public string KoefString
        {
            get { return _koefString; }
            set { SetValue(ref _koefString, value); }
        }

        private List<String> _periodList;
        public List<String> PeriodList
        {
            get { return _periodList; }
            set { SetValue(ref _periodList, value); }
        }

        private int _periodIndex;
        public int PeriodIndex
        {
            get { return _periodIndex; }
            set { SetValue(ref _periodIndex, value); }
        }

        private string _periodSelectedValue;
        public string PeriodSelectedValue
        {
            get { return _periodSelectedValue; }
            set { SetValue(ref _periodSelectedValue, value); }
        }

        private int _paramsKey;
        public int ParamsKey
        {
            get { return _paramsKey; }
            set { SetValue(ref _paramsKey, value); }
        }

        private string _paramsString;
        public string ParamsString
        {
            get { if (!string.IsNullOrEmpty(_paramsString)) return _paramsString; else return ""; }
            set { SetValue(ref _paramsString, value); }
        }

        private string _descriptionString;
        public string DescriptionString
        {
            get { if (!string.IsNullOrEmpty(_descriptionString)) return _descriptionString; else return ""; }
            set { SetValue(ref _descriptionString, value); }
        }

        private string _updateString;
        public string UpdateString
        {
            get { return _updateString; }
            set { SetValue(ref _updateString, value); }
        }

        public bool _editFlag;
        public bool EditFlag
        {
            get { return _editFlag; }
            set
            {
                SetValue(ref _editFlag, value);
                if (_editFlag)
                    UpdateString = "Сохранить";
                else
                    UpdateString = "Изменить";
            }
        }

        public bool _newFlag;
        public bool NewFlag
        {
            get { return _newFlag; }
            set { SetValue(ref _newFlag, value); }
        }

        public bool _copyFlag;
        public bool CopyFlag
        {
            get { return _copyFlag; }
            set { SetValue(ref _copyFlag, value); }
        }

        public bool _lockFlag;
        public bool LockFlag
        {
            get { return _lockFlag; }
            set { SetValue(ref _lockFlag, value); }
        }


        //----------------------------------------------------------------------------------------------------
        public InsControlServiceItem(int iNumber, string iService)
        {
            RowNumber = iNumber;
            ServiceName = iService;

            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1 - dt.Day);
            dt = dt.AddMonths(1);
            DateFrom = new DateTime(dt.Year, dt.Month, dt.Day);
            dt = dt.AddDays(-1);
            dt = dt.AddYears(1);
            DateTill = new DateTime(dt.Year, dt.Month, dt.Day);

            CurrencyList = new List<string>();
            CurrencyList.Add("$");
            CurrencyList.Add("Eu");
            CurrencyList.Add("рб");

            PartnerList = new List<string>();
            //PartnerList.Add("УРАЛСИБ USD");
            //PartnerList.Add("УРАЛСИБ EUR");
            //PartnerList.Add("УРАЛСИБ RUB");
            //PartnerList.Add("АЛЬФАСТР USD");
            //PartnerList.Add("АЛЬФАСТР EUR");
            //PartnerList.Add("АЛЬФАСТР RUB");
            PartnerList.Add("АЛЬФАСТРАХ");

            PartnerKeyList = new List<int>();
            //PartnerKeyList.Add(51457);
            //PartnerKeyList.Add(51458);
            //PartnerKeyList.Add(52106);
            //PartnerKeyList.Add(0);
            //PartnerKeyList.Add(0);
            //PartnerKeyList.Add(0);
            PartnerKeyList.Add(55166);

            PeriodList = new List<string>();
            PeriodList.Add("период");
            PeriodList.Add("день");

            EditFlag = false;
        }

        //----------------------------------------------------------------------------------------------------
        public void Copy_(InsControlServiceItem copy_item)
        {
            if (copy_item == null) return;

            // RowID
            // RowNumber
            ServiceName = copy_item.ServiceName;
            ServiceText = copy_item.ServiceText;

            ServiceCode = copy_item.ServiceCode;
            ServiceSubcode1 = copy_item.ServiceSubcode1;
            DateFrom = copy_item.DateFrom;
            DateTill = copy_item.DateTill;
            PriceByDay = copy_item.PriceByDay;
            СostRateString = copy_item.СostRateString;

            Copy_CurrencyList(copy_item.CurrencyList);
            CurrencyIndex = copy_item.CurrencyIndex;
            CurrencySelectedValue = copy_item.CurrencySelectedValue;
            
            Netto = copy_item.Netto;
            Brutto = copy_item.Brutto;

            Copy_PartnerList(copy_item.PartnerList);
            Copy_PartnerKeyList(copy_item.PartnerKeyList);
            PartnerKey = copy_item.PartnerKey;
            PartnerString = copy_item.PartnerString;
            PartnerIndex = copy_item.PartnerIndex;
            PartnerSelectedValue = copy_item.PartnerSelectedValue;

            Percent = copy_item.Percent;
            Summa = copy_item.Summa;
            KoefString = copy_item.KoefString;

            Copy_PeriodList(copy_item.PeriodList);
            PeriodIndex = copy_item.PeriodIndex;
            PeriodSelectedValue = copy_item.PeriodSelectedValue;

            ParamsKey = copy_item.ParamsKey;
            ParamsString = copy_item.ParamsString;
            DescriptionString = copy_item.DescriptionString;

            EditFlag = copy_item.EditFlag;
            CopyFlag = copy_item.CopyFlag;
            LockFlag = copy_item.LockFlag;
        }

        //----------------------------------------------------------------------------------------------------
        public void CopyBack(InsControlServiceItem copy_item)
        {
            if (copy_item == null) return;

            ServiceName = copy_item.ServiceName;
            ServiceText = copy_item.ServiceText;

            ServiceCode = copy_item.ServiceCode;
            ServiceSubcode1 = copy_item.ServiceSubcode1;
            DateFrom = copy_item.DateFrom;
            DateTill = copy_item.DateTill;
            PriceByDay = copy_item.PriceByDay;
            СostRateString = copy_item.СostRateString;

            CurrencyIndex = copy_item.CurrencyIndex;
            CurrencySelectedValue = copy_item.CurrencySelectedValue;

            Netto = copy_item.Netto;
            Brutto = copy_item.Brutto;

            PartnerKey = copy_item.PartnerKey;
            PartnerString = copy_item.PartnerString;
            PartnerIndex = copy_item.PartnerIndex;
            PartnerSelectedValue = copy_item.PartnerSelectedValue;

            Percent = copy_item.Percent;
            Summa = copy_item.Summa;
            KoefString = copy_item.KoefString;

            PeriodIndex = copy_item.PeriodIndex;
            PeriodSelectedValue = copy_item.PeriodSelectedValue;

            ParamsString = copy_item.ParamsString;
            DescriptionString = copy_item.DescriptionString;

            //EditFlag = copy_item.EditFlag;
            //CopyFlag = copy_item.CopyFlag;
            //LockFlag = copy_item.LockFlag;
        }

        //----------------------------------------------------------------------------------------------------
        public bool CheckDiff(InsControlServiceItem check_item)
        {
            if (check_item == null) return true;

            if (string.Compare(ServiceName, check_item.ServiceName) != 0) return true;
            if (string.Compare(ServiceText, check_item.ServiceText) != 0) return true;

            if (ServiceCode != check_item.ServiceCode) return true;
            if (ServiceSubcode1 != check_item.ServiceSubcode1) return true;
            if (DateFrom != check_item.DateFrom) return true;
            if (DateTill != check_item.DateTill) return true;
            if (PriceByDay != check_item.PriceByDay) return true;
            if (string.Compare(СostRateString, check_item.СostRateString) != 0) return true;

            if (CurrencyIndex != check_item.CurrencyIndex) return true;
            if (string.Compare(CurrencySelectedValue, check_item.CurrencySelectedValue) != 0) return true;

            Read_Netto();
            Read_Brutto();

            if (Netto != check_item.Netto) return true;
            if (Brutto != check_item.Brutto) return true;

            if (PartnerKey != check_item.PartnerKey) return true;
            if (string.Compare(PartnerString, check_item.PartnerString) != 0) return true;
            if (PartnerIndex != check_item.PartnerIndex) return true;
            if (string.Compare(PartnerSelectedValue, check_item.PartnerSelectedValue) != 0) return true;

            if (Percent != check_item.Percent) return true;
            if (Summa != check_item.Summa) return true;
            if (string.Compare(KoefString, check_item.KoefString) != 0) return true;

            if (PeriodIndex != check_item.PeriodIndex) return true;
            if (string.Compare(PeriodSelectedValue, check_item.PeriodSelectedValue) != 0) return true;

            if (string.Compare(ParamsString, check_item.ParamsString) != 0) return true;
            if (string.Compare(DescriptionString, check_item.DescriptionString) != 0) return true;

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        private void AccordActPeriod()
        {
            string sPeriod = "";
            if (DateFrom.Year > 2010 && DateTill.Year > 2010)
            {
                sPeriod = DateFrom.ToString("dd.MM.yy") + " - " + DateTill.ToString("dd.MM.yy");
            }
            ActPeriodString = sPeriod;
        }

        //----------------------------------------------------------------------------------------------------
        private void CalcPercent()
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
        public decimal Read_Netto()
        {
            decimal netto = 0;
            if (!string.IsNullOrEmpty(NettoString.Trim()))
                try { netto = decimal.Parse(NettoString.Trim()); }
                catch { netto = Netto; }
            Netto = netto;
            return Netto;
        }

        //----------------------------------------------------------------------------------------------------
        public decimal Read_Brutto()
        {
            decimal brutto = 0;
            if (!string.IsNullOrEmpty(BruttoString.Trim()))
                try { brutto = decimal.Parse(BruttoString.Trim()); }
                catch { brutto = Brutto; }
            Brutto = brutto;
            return Brutto;
        }

        //----------------------------------------------------------------------------------------------------
        public void Set_CurrencyList(string currency_list)
        {
            if (CurrencyList == null) CurrencyList = new List<string>();
            CurrencyList.Clear();
            CurrencyIndex = -1;

            if (string.IsNullOrEmpty(currency_list)) return;
            string[] codes = currency_list.Split(';');
            int index = -1;
            foreach (string code in codes)
            {
                CurrencyList.Add(code);
                index++;
                if (code == _costRateString) CurrencyIndex = index;
            }
        }

        //----------------------------------------------------------------------------------------------------
        public void Set_PeriodList(string period_list)
        {
            if (PeriodList == null) PeriodList = new List<string>();
            PeriodList.Clear();
            PeriodIndex = -1;

            if (string.IsNullOrEmpty(period_list)) return;
            string[] codes = period_list.Split(';');
            int index = -1;
            foreach (string code in codes)
            {
                PeriodList.Add(code);
                index++;
                if (code == "период" && _priceByDay == 0) PeriodIndex = index;
                if (code == "день" && _priceByDay == 2) PeriodIndex = index;
            }
        }

        //----------------------------------------------------------------------------------------------------
        public void Set_PartnerList(string partner_list)
        {
            if (PartnerList == null) PartnerList = new List<string>();
            PartnerList.Clear();
            if (PartnerKeyList == null) PartnerKeyList = new List<int>();
            PartnerKeyList.Clear();
            PartnerIndex = -1;

            if (string.IsNullOrEmpty(partner_list)) return;
            string[] codes = partner_list.Split(';');
            int index = -1;
            foreach (string codecode in codes)
            {
                int pos = codecode.IndexOf(',');
                if (pos > 0)
                {
                    string num = codecode.Substring(0, pos);
                    int id = 0;
                    if (!string.IsNullOrEmpty(num)) id = int.Parse(num);
                    string code = codecode.Substring(pos + 1);
                    PartnerList.Add(code);
                    PartnerKeyList.Add(id);
                    index++;
                    if (code == _partnerString) PartnerIndex = index;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        public void Read_SelectedCurrency()
        {
            if (string.IsNullOrEmpty(CurrencySelectedValue)) return;
            СostRateString = CurrencySelectedValue;

            //if (CurrencyList == null) return;
            //if (CurrencyList.Count == 0) return;

            //if (CurrencyIndex >= CurrencyList.Count) return;
            //СostRateString = CurrencyList[CurrencyIndex];
        }

        //----------------------------------------------------------------------------------------------------
        public void Read_SelectedPeriod()
        {
            if (string.IsNullOrEmpty(PeriodSelectedValue)) return;
            string code = PeriodSelectedValue;

            if (code == "период") PriceByDay = 0;
            if (code == "день") PriceByDay = 2;
        }

        //----------------------------------------------------------------------------------------------------
        public void Read_SelectedPartner()
        {
            if (string.IsNullOrEmpty(PartnerSelectedValue)) return;
            PartnerString = PartnerSelectedValue;

            if (PartnerKeyList == null) return;
            if (PartnerKeyList.Count == 0) return;
            if (PartnerIndex < 0) return;
            if (PartnerIndex >= PartnerKeyList.Count) return;
            PartnerKey = PartnerKeyList[PartnerIndex];
        }

        //----------------------------------------------------------------------------------------------------
        public void Copy_CurrencyList(List<string> currency_list)
        {
            if (CurrencyList == null) CurrencyList = new List<string>();
            CurrencyList.Clear();
            //CurrencyIndex = -1;

            if (currency_list == null) return;
            if (currency_list.Count == 0) return;

            foreach (string code in currency_list)
            {
                CurrencyList.Add(code);
                //index++;
                //if (code == _costRateString) CurrencyIndex = index;
            }
        }

        //----------------------------------------------------------------------------------------------------
        public void Copy_PartnerList(List<string> partner_list)
        {
            if (PartnerList == null) PartnerList = new List<string>();
            PartnerList.Clear();
            //PartnerIndex = -1;

            if (partner_list == null) return;
            if (partner_list.Count == 0) return;

            foreach (string code in partner_list)
            {
                PartnerList.Add(code);
                //index++;
                //if (code == _partnerString) PartnerIndex = index;
            }
        }

        //----------------------------------------------------------------------------------------------------
        public void Copy_PartnerKeyList(List<int> partner_key_list)
        {
            if (PartnerKeyList == null) PartnerKeyList = new List<int>();
            PartnerKeyList.Clear();

            if (partner_key_list == null) return;
            if (partner_key_list.Count == 0) return;

            foreach (int key in partner_key_list)
            {
                PartnerKeyList.Add(key);
            }
        }

        //----------------------------------------------------------------------------------------------------
        public void Copy_PeriodList(List<string> period_list)
        {
            if (PeriodList == null) PeriodList = new List<string>();
            PeriodList.Clear();

            if (period_list == null) return;
            if (period_list.Count == 0) return;

            foreach (string period in period_list)
            {
                PeriodList.Add(period);
            }
        }

        //----------------------------------------------------------------------------------------------------
    }
    //====================================================================================================
    #endregion // InsControlServiceItem
    //====================================================================================================
    #region InsControlServiceList
    //====================================================================================================
    public class InsControlServiceList : ObservableCollection<InsControlServiceItem>
    {
        //----------------------------------------------------------------------------------------------------
        public InsControlServiceList()
        {

        }

        //----------------------------------------------------------------------------------------------------
    }
    //====================================================================================================
    #endregion // InsControlServiceList
    //====================================================================================================

    //====================================================================================================
    #region InshurControlViewModel
    //====================================================================================================
    public class InshurControlViewModel : Data
    {
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------
        public InsControlServiceList _insControlServiceList;
        public InsControlServiceList InsControlServiceList 
        {
            get { return _insControlServiceList; }
            set { SetValue(ref _insControlServiceList, value); } 
        }

        //public RoutedCommand AddCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public bool SelectedFlag;

        private string _sortNameTitle = ""; 
        public string SortNameTitle
        {
            get { if (string.IsNullOrEmpty(_sortNameTitle)) return "Описание  страховки"; else return _sortNameTitle; }
            set { SetValue(ref _sortNameTitle, value); }
        }

        private int _sortNameDir;
        public int SortNameDir
        {
            get { return _sortNameDir; }
            set { SetValue(ref _sortNameDir, value); }
        }

        private string _sortDateFromTitle = "";
        public string SortDateFromTitle
        {
            get { if (string.IsNullOrEmpty(_sortDateFromTitle)) return "с  даты"; else return _sortDateFromTitle; }
            set { SetValue(ref _sortDateFromTitle, value); }
        }

        private int _sortDateFromDir;
        public int SortDateFromDir
        {
            get { return _sortDateFromDir; }
            set { SetValue(ref _sortDateFromDir, value); }
        }

        private string _sortDateTillTitle = "";
        public string SortDateTillTitle
        {
            get { if (string.IsNullOrEmpty(_sortDateTillTitle)) return "по  дату"; else return _sortDateTillTitle; }
            set { SetValue(ref _sortDateTillTitle, value); }
        }

        private int _sortDateTillDir;
        public int SortDateTillDir
        {
            get { return _sortDateTillDir; }
            set { SetValue(ref _sortDateTillDir, value); }
        }

        private string _sortCurrencyTitle = "";
        public string SortCurrencyTitle
        {
            get { if (string.IsNullOrEmpty(_sortCurrencyTitle)) return "валюта"; else return _sortCurrencyTitle; }
            set { SetValue(ref _sortCurrencyTitle, value); }
        }

        private int _sortCurrencyDir;
        public int SortCurrencyDir
        {
            get { return _sortCurrencyDir; }
            set { SetValue(ref _sortCurrencyDir, value); }
        }

        private InsControlServiceItem _edittingItem;
        public InsControlServiceItem EdittingItem
        {
            get { return _edittingItem; }
            set { SetValue(ref _edittingItem, value); }
        }

        private InsControlServiceItem _edittingItemBack;
        public InsControlServiceItem EdittingItemBack
        {
            get { return _edittingItemBack; }
            set { SetValue(ref _edittingItemBack, value); }
        }

        private int _edittingNumber;
        public int EdittingNumber
        {
            get { return _edittingNumber; }
            set { SetValue(ref _edittingNumber, value); }
        }

        public bool _edittingFlag;
        public bool EdittingFlag 
        {
            get { return _edittingFlag; }
            set { SetValue(ref _edittingFlag, value); }
        }

        //----------------------------------------------------------------------------------------------------
        #region EditService
        //----------------------------------------------------------------------------------------------------
        private InsControlServiceItem _editService;
        public InsControlServiceItem EditService
        {
            get
            {
                if (_editService == null) _editService = new InsControlServiceItem(0, "");
                return _editService;
            }
            set 
            { 
                SetValue(ref _editService, value);
                SelectedFlag = (_editService != null);
            }
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // EditService
        //----------------------------------------------------------------------------------------------------

        public string currency_list = "$;Eu;рб";
        public string period_list = "период;день";
        public string partenr_list = "51457,УРАЛСИБ USD;51458,УРАЛСИБ EUR;52106,УРАЛСИБ RUB;0,АЛЬФАСТР USD;0,АЛЬФАСТР EUR;0,АЛЬФАСТР RUB";


        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region InshurControlViewModel()
        //----------------------------------------------------------------------------------------------------
        public InshurControlViewModel()
        {
            InsControlServiceList = new InsControlServiceList();

            //AddCommand = new RoutedCommand();
            AddCommand = new RelayCommand(On_AddCommand, Can_AddCommand);
            EditCommand = new RelayCommand(On_EditCommand, Can_EditCommand);
            SaveCommand = new RelayCommand(On_SaveCommand, Can_SaveCommand);
            CancelCommand = new RelayCommand(On_CancelCommand, Can_CancelCommand);
            DeleteCommand = new RelayCommand(On_DeleteCommand, Can_DeleteCommand);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // InshurControlViewModel()
        //----------------------------------------------------------------------------------------------------
        #endregion // Methods
        //====================================================================================================
        #region Data work
        //----------------------------------------------------------------------------------------------------
        #region Set_TableData()
        //----------------------------------------------------------------------------------------------------
        public void Set_TableData(DataTable dtable)
        {
            if (InsControlServiceList == null) InsControlServiceList = new InsControlServiceList();
            InsControlServiceList.Clear();

            //InsControlServiceList = Read_TableData(dtable);
            Read_TableData(dtable);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_TableData()
        //----------------------------------------------------------------------------------------------------
        #region Read_TableData()
        //----------------------------------------------------------------------------------------------------
        public void Read_TableData(DataTable dtable)
        {
            //InsControlServiceList InsList = new InsControlServiceList();
            InsControlServiceList InsList = InsControlServiceList;

            if (dtable == null) return;
            if (dtable.Rows.Count == 0) return;

            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                try
                {
                    DataRow drow = dtable.Rows[i];
                    InsControlServiceItem item = new InsControlServiceItem(InsList.Count + 1, "Страховка...");
                    item.RowID = drow.Field<int>("CS_ID");
                    //item.ServiceName = "Описание цен для страховки (" + item.RowNumber.ToString() + ")";
                    item.ServiceName = drow.Field<string>("LL_NAME");
                    item.ServiceCode = drow.Field<int>("CS_CODE");
                    item.ServiceSubcode1 = drow.Field<int>("CS_SUBCODE1");
                    item.DateFrom = drow.Field<DateTime>("CS_DATE");
                    item.DateTill = drow.Field<DateTime>("CS_DATEEND");
                    item.СostRateString = drow.Field<string>("CS_RATE");
                    item.Netto = (decimal)drow.Field<double>("CS_COSTNETTO");
                    item.Brutto = (decimal)drow.Field<double>("CS_COST");
                    item.PriceByDay = drow.Field<short>("CS_BYDAY");
                    item.PartnerKey = drow.Field<int>("CS_PRKEY");
                    item.PartnerString = drow.Field<string>("PR_NAME");
                    item.ParamsKey = drow.Field<int>("IL_KEY");
                    item.ParamsString = drow.Field<string>("IL_PARAMS");
                    item.DescriptionString = drow.Field<string>("IL_DESCRIPTION");
                    //[CS_DISCOUNT],[CS_UPDDATE],[CS_TypeDivision],[CS_UPDUSER]

                    item.Set_CurrencyList(currency_list);
                    item.Set_PeriodList(period_list);
                    item.Set_PartnerList(partenr_list);

//if (i < 20)
                    InsList.Add(item);
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Read_TableData()
        //----------------------------------------------------------------------------------------------------
        #endregion // Data work
        //====================================================================================================
        #region Commands
        //----------------------------------------------------------------------------------------------------
        #region On_AddCommand()
        //----------------------------------------------------------------------------------------------------
        public void On_AddCommand()
        {
            if (InsControlServiceList == null) return;
            /*
            InsControlServiceList InsList = InsControlServiceList;
            int max_id = 0;
            foreach (InsControlServiceItem ins_item in InsList)
            {
                if (ins_item.RowID > max_id) max_id = ins_item.RowID;
            }

            InsControlServiceItem item = new InsControlServiceItem(InsList.Count + 1, "Новая запись...");
            item.RowID = max_id + 1;
            item.Set_CurrencyList(currency_list);
            item.Set_PeriodList(period_list);
            item.Set_PartnerList(partenr_list);

            InsList.Add(item);
            */
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // On_AddCommand()
        //----------------------------------------------------------------------------------------------------
        #region Can_AddCommand()
        //----------------------------------------------------------------------------------------------------
        public bool Can_AddCommand()
        {
            return !EdittingFlag;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // Can_AddCommand()
        //----------------------------------------------------------------------------------------------------
        #region On_EditCommand()
        //----------------------------------------------------------------------------------------------------
        public void On_EditCommand()
        {
            BeginEditting();
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // On_EditCommand()
        //----------------------------------------------------------------------------------------------------
        #region Can_EditCommand()
        //----------------------------------------------------------------------------------------------------
        public bool Can_EditCommand()
        {
            return SelectedFlag && !EdittingFlag;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // Can_EditCommand()
        //----------------------------------------------------------------------------------------------------
        #region On_SaveCommand()
        //----------------------------------------------------------------------------------------------------
        public void On_SaveCommand()
        {


            EndEditting();
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // On_SaveCommand()
        //----------------------------------------------------------------------------------------------------
        #region Can_SaveCommand()
        //----------------------------------------------------------------------------------------------------
        public bool Can_SaveCommand()
        {
            return SelectedFlag && EdittingFlag;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // Can_SaveCommand()
        //----------------------------------------------------------------------------------------------------
        #region On_CancelCommand()
        //----------------------------------------------------------------------------------------------------
        public void On_CancelCommand()
        {


            EndEditting();
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // On_CancelCommand()
        //----------------------------------------------------------------------------------------------------
        #region Can_CancelCommand()
        //----------------------------------------------------------------------------------------------------
        public bool Can_CancelCommand()
        {
            return SelectedFlag && EdittingFlag;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // Can_CancelCommand()
        //----------------------------------------------------------------------------------------------------
        #region On_DeleteCommand()
        //----------------------------------------------------------------------------------------------------
        public void On_DeleteCommand()
        {


            EndEditting();
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // On_DeleteCommand()
        //----------------------------------------------------------------------------------------------------
        #region Can_DeleteCommand()
        //----------------------------------------------------------------------------------------------------
        public bool Can_DeleteCommand()
        {
            return SelectedFlag;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // Can_DeleteCommand()
        //----------------------------------------------------------------------------------------------------
        #region BeginEditting()
        //----------------------------------------------------------------------------------------------------
        public void BeginEditting()
        {
            if (!SelectedFlag) return;
            if (EditService == null) return;

            EditService.EditFlag = true;
            if (InsControlServiceList != null)
                foreach (InsControlServiceItem item in InsControlServiceList)
                    item.LockFlag = true;

            EdittingFlag = true;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // BeginEditting()
        //----------------------------------------------------------------------------------------------------
        #region EndEditting()
        //----------------------------------------------------------------------------------------------------
        public void EndEditting()
        {
            if (EditService != null)
                EditService.EditFlag = false;

            if (InsControlServiceList != null)
                foreach (InsControlServiceItem item in InsControlServiceList)
                { item.LockFlag = false; item.CopyFlag = false; }

            EdittingFlag = false;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // EndEditting()
        //----------------------------------------------------------------------------------------------------
        #endregion // Commands
        //====================================================================================================
    }
    //====================================================================================================
    #endregion // InshurControlViewModel
    //====================================================================================================
}
