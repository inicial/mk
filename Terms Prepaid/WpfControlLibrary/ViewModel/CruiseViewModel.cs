using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.Util;


namespace WpfControlLibrary.ViewModel
{
    public class CruiseInfo : Data
    {
        private string _category;
        public string Category
        {
            get { return _category; }
            set { SetValue(ref _category, value); }
        }

        private string _cabinNumber;
        public string CabinNumber
        {
            get { return _cabinNumber; }
            set { SetValue(ref _cabinNumber, value); }
        }

        private string _cabinDef;
        public string CabinDef
        {
            get { return _cabinDef; }
            set { SetValue(ref _cabinDef, value); }
        }

        private DateTime _optionDate;
        public DateTime OptionDate
        {
            get { return _optionDate; }
            set { SetValue(ref _optionDate, value); }
        }

        private string _optionNumber;
        public string OptionNumber
        {
            get { return _optionNumber; }
            set { SetValue(ref _optionNumber, value); }
        }

        private string _specCanc;
        public string SpecCanc
        {
            get { return _specCanc; }
            set { SetValue(ref _specCanc, value); }
        }

        private bool _optionFlag;
        public bool OptionFlag // option block visibility control
        {
            get { return _optionFlag; }
            set { SetValue(ref _optionFlag, value); }
        }

        private bool _optionNumberFlag;
        public bool OptionNumberFlag // option number flag
        {
            get { return _optionNumberFlag; }
            set { SetValue(ref _optionNumberFlag, value); }
        }

        private bool _optionCheckFlag;
        public bool OptionCheckFlag // option checkbox visibility control
        {
            get { return _optionCheckFlag; }
            set { SetValue(ref _optionCheckFlag, value); }
        }

        private bool _isBook;
        public bool IsBook
        {
            get { return _isBook; }
            set { SetValue(ref _isBook, value); }
        }

        private string _checkOptionText1;
        public string CheckOptionText1
        {
            get { return _checkOptionText1; }
            set { SetValue(ref _checkOptionText1, value); }
        }

        private string _checkOptionText2;
        public string CheckOptionText2
        {
            get { return _checkOptionText2; }
            set { SetValue(ref _checkOptionText2, value); }
        }

        private bool _documentGet;
        public bool DocumentGet
        {
            get { return _documentGet; }
            set { SetValue(ref _documentGet, value); }
        }

        private bool _documentQuery;
        public bool DocumentQuery
        {
            get { return _documentQuery; }
            set { SetValue(ref _documentQuery, value); }
        }

        private DateTime _documentGetDate;
        public DateTime DocumentGetDate
        {
            get { return _documentGetDate; }
            set 
            { 
                SetValue(ref _documentGetDate, value);
                DocumentGetDateString = NowDateString; // "";
                if (_documentGetDate.Year > 2000)
                    //DocumentGetDateString = _documentGetDate.ToString("dd.MM.yy   HH:mm");
                    DocumentGetDateString = _documentGetDate.ToString("HH:mm   dd.MM.yy");
            }
        }

        private DateTime _documentQueryDate;
        public DateTime DocumentQueryDate
        {
            get { return _documentQueryDate; }
            set 
            { 
                SetValue(ref _documentQueryDate, value);
                DocumentQueryDateString = NowDateString; // "";
                if (_documentQueryDate.Year > 2000)
                    //DocumentQueryDateString = _documentQueryDate.ToString("dd.MM.yy   HH:mm");
                    DocumentQueryDateString = _documentQueryDate.ToString("HH:mm   dd.MM.yy");
            }
        }

        private string _documentGetDateString;
        public string DocumentGetDateString
        {
            get { return _documentGetDateString; }
            set { SetValue(ref _documentGetDateString, value); }
        }

        private string _documentQueryDateString;
        public string DocumentQueryDateString
        {
            get { return _documentQueryDateString; }
            set { SetValue(ref _documentQueryDateString, value); }
        }

        private string _nowDateString;
        public string NowDateString
        {
            get { return _nowDateString; }
            set { SetValue(ref _nowDateString, value); }
        }

        private bool _changeEnableFlag;
        public bool ChangeEnableFlag 
        {
            get { return _changeEnableFlag; }
            set { SetValue(ref _changeEnableFlag, value); }
        }

        public CruiseInfo(CruiseService service)
        {
            NowDateString = DateTime.Now.ToString("HH:mm   dd.MM.yy");

            Category = "";
            CabinNumber = "";
            CabinDef = "";
            OptionNumber = "";
            //OptionDate = DateTime.Now.Date;
            IsBook = false;
            DocumentGet = false;
            DocumentQuery = false;
            //DocumentGetDate = DateTime.Now;
            //DocumentQueryDate = DateTime.Now;
            CheckOptionText1 = "Подтвердить бронь в круизной компании";
            CheckOptionText2 = "Бронь подтверждена в круизной компании";
            string dgcode = "";

            if (service != null)
            {
                dgcode = service.DgCode;
                Category = service.Category;
                CabinNumber = service.CabinNumber;
                CabinDef = service.CabinDef;
                OptionNumber = service.OptionNumber;
                OptionDate = service.OptionDate;
                IsBook = service.IsBook;
                DocumentGet = service.DocumentGet;
                DocumentQuery = service.DocumentQuery;
                DocumentGetDate = service.DocumentGetDate;
                DocumentQueryDate = service.DocumentQueryDate;
            }
            SpecCanc = "";
            OptionNumberFlag = !string.IsNullOrEmpty(OptionNumber);
            OptionCheckFlag = !string.IsNullOrEmpty(OptionNumber) && !IsBook;

            int i = dgcode.IndexOf("SPL");
            if (dgcode.IndexOf("SPL") == 0 && OptionNumberFlag)
            {
                CheckOptionText1 = "Подтвердить бронь в круизной компании SPL";
                CheckOptionText2 = "Бронь подтверждена в круизной компании SPL";
            }
        }

        public bool Equals(CruiseService service)
        {
            if (service == null) return true; // false;

            return Category == service.Category && CabinNumber == service.CabinNumber && // CabinDef == service.CabinDef &&
                OptionNumber == service.OptionNumber && OptionDate == service.OptionDate && IsBook == service.IsBook &&
                DocumentGet == service.DocumentGet && DocumentQuery == service.DocumentQuery && string.IsNullOrEmpty(SpecCanc);
        }

        public bool ChekChanged(CruiseService service)
        {
            if (service != null)
            {
                return !Equals(service);
            }
            else
            {
                if (!string.IsNullOrEmpty(Category)) return true;
                if (!string.IsNullOrEmpty(CabinNumber)) return true;
                if (!string.IsNullOrEmpty(CabinDef)) return true;
                if (!string.IsNullOrEmpty(OptionNumber)) return true;
                if (OptionDate != null)
                    if (OptionDate.Year > 2000) return true;
                if (IsBook) return true;
                if (DocumentGet) return true;
                if (DocumentQuery) return true;
                if (DocumentGetDate != null)
                    if (DocumentGetDate.Year > 2000) return true;
                if (DocumentQueryDate != null)
                    if (DocumentQueryDate.Year > 2000) return true;

                return true; // false;
            }
        }
    }

    public class CruiseViewModel : ServiceViewModel
    {
        public delegate void UpdateVoucherDelegate(bool aviaErrorCallback = false, ServiceType serviceType = ServiceType.Unknow);

        public delegate int ConfirmSaveDelegate(string text);

        public RelayCommand AddOptionCommand { get; set; }
        public RelayCommand ClearAddOptionCommand { get; set; }
        public RelayCommand BonusOkCommand { get; set; }
        public RelayCommand ClearBonusesAndServicesCommand { get; set; }
        public RelayCommand ChangeOptionCommand { get; set; }
        public RelayCommand UndoOptionCommand { get; set; }

        public ConfirmSaveDelegate ConfirmSaveFunction;

        private Permissions _permission;
        public Permissions Permission
        {
            get { return _permission; }
            set { SetValue(ref _permission, value); }
        }

        private CruiseInfo _cruiseInfo;
        public CruiseInfo CruiseInfo
        {
            get { return _cruiseInfo; }
            set { SetValue(ref _cruiseInfo, value); }
        }

        private ObservableCollection<BonusAndService> _bonusesAndServices;
        public ObservableCollection<BonusAndService> BonusesAndServices
        {
            get { return _bonusesAndServices; }
            set { SetValue(ref _bonusesAndServices, value); }
        }

        //private ObservableCollection<BonusAndService> _nobonusesAndServices;
        //public ObservableCollection<BonusAndService> NoBonusesAndServices
        //{
        //    get { return _nobonusesAndServices; }
        //    set { SetValue(ref _nobonusesAndServices, value); }
        //}

        private UpdateVoucherDelegate _updateVoucherHandler;

        public string BonusChangesString = "";
        public string OptionChangesString = "";
        public bool ChangesFlag = false; 


        public CruiseViewModel(UpdateVoucherDelegate callback, Permissions permission)
        {
            _updateVoucherHandler = callback;
            Permission = permission;
            //AddOptionCommand = new RelayCommand(AddOption, OptionIsChanged);
            //ClearAddOptionCommand = new RelayCommand(ClearAddOption, OptionIsChanged);
            AddOptionCommand = new RelayCommand(AddOption, NoBonusesAnServicesIsChanged);
            ClearAddOptionCommand = new RelayCommand(ClearAddOption, NoBonusesAnServicesIsChanged);
            //BonusOkCommand = new RelayCommand(BonusOk, BonusesAnServicesIsChanged);
            //ClearBonusesAndServicesCommand = new RelayCommand(ClearBonusesAndServices, BonusesAnServicesIsChanged);
            BonusOkCommand = new RelayCommand(BonusOk, NoBonusesAnServicesIsChanged);
            ClearBonusesAndServicesCommand = new RelayCommand(ClearBonusesAndServices, NoBonusesAnServicesIsChanged);
            ChangeOptionCommand = new RelayCommand(ChangeOption);
            UndoOptionCommand = new RelayCommand(UndoOption);
        }

        private void Update()
        {
            if (_updateVoucherHandler != null)
                _updateVoucherHandler.Invoke(false, ServiceType.Cruise);
        }

        public override void LoadService()
        {
            LoadOptions();
            LoadBonusesAndServices();
        }

        private string AddChangedField(ref string s_list, string s_field)
        {
            if (s_list == null) s_list = "";

            if (s_field.Length > 0)
            {
                if (s_list.Length > 0) s_list = s_list + (char)13 + (char)10;
                s_list = s_list + s_field;
            }

            return s_list;
        }

        public string GetChangesMessage(string message)
        {
            //string msg = "Сделаны изменения:" + (char)13 + (char)10 + (char)13 + (char)10;
            //msg = msg + OptionChangesString + (char)13 + (char)10 + (char)13 + (char)10;
            //msg = msg + message;

            string msg = "Сделаны изменения полей:" + (char)13 + (char)10;
            //msg = msg + "---------------------------------------------------";
            msg = msg + (char)13 + (char)10;
            msg = msg + OptionChangesString + (char)13 + (char)10;
            //msg = msg + "---------------------------------------------------";
            msg = msg + (char)13 + (char)10;
            msg = msg + message;

            return msg;
        }

        public bool BronConfirmFlag()
        {
            var service = (CruiseService)Service;
            if (service == null) return false;
            if (CruiseInfo == null) return false;

            return !CruiseInfo.OptionNumberFlag && CruiseInfo.IsBook && !service.IsBook;
        }

        private bool OptionIsChanged() // более не использеуется
        {
            //OptionChangesString = "";

            var service = (CruiseService)Service;
            if (service == null) return true;  // если не было записи в базе, то считаем данные измененными (как исключение)

            bool bEqual = CruiseInfo.Equals(service);

            //if (!bEqual)
            //{
            //    OptionChangesString = "";
            //    if (CruiseInfo.Category != service.Category) AddChangedField(ref OptionChangesString, "Категоря: " + CruiseInfo.Category);
            //    if (CruiseInfo.CabinNumber != service.CabinNumber) AddChangedField(ref OptionChangesString, "№ каюты: " + CruiseInfo.CabinNumber);
            //    //if (CruiseInfo.CabinDef != service.CabinDef) AddChangedField(ref OptionChangesString, "CabinDef: " + CruiseInfo.CabinDef);
            //    if (CruiseInfo.OptionNumber != service.OptionNumber) AddChangedField(ref OptionChangesString, "№ опции: " + CruiseInfo.OptionNumber);
            //    if (CruiseInfo.OptionDate != service.OptionDate) AddChangedField(ref OptionChangesString, "Дата опции: " + CruiseInfo.OptionDate.ToString("dd.MM.yy   HH:mm"));
            //    string s_field = "Опция не подтверждена";
            //    if (CruiseInfo.IsBook) s_field = "Бронь подтверждена";
            //    if (CruiseInfo.IsBook != service.IsBook) AddChangedField(ref OptionChangesString, s_field);
            //    s_field = "Документы не запрошены";
            //    if (CruiseInfo.DocumentQuery) s_field = "Документы запрошены";
            //    if (CruiseInfo.DocumentQuery != service.DocumentQuery) AddChangedField(ref OptionChangesString, s_field);
            //    s_field = "Документы не получены";
            //    if (CruiseInfo.DocumentGet) s_field = "Документы получены";
            //    if (CruiseInfo.DocumentGet != service.DocumentGet) AddChangedField(ref OptionChangesString, s_field);
            //}

            return !bEqual;
        }

        private bool BonusesAnServicesIsChanged()
        {
            if (_permission == Permissions.Level0)
                return false;

            var service = (CruiseService)Service;
            if (BonusesAndServices == null || service.BonusesAndServices == null || BonusesAndServices.Count != service.BonusesAndServices.Count)
                return true;

            for (var i = 0; i < BonusesAndServices.Count; i++)
            {
                var f = BonusesAndServices[i];
                var s = service.BonusesAndServices[i];

                if (f.Id != s.Id || f.Text != s.Text || f.Name != s.Name || f.IsRight != s.IsRight)
                    return true;
            }

            foreach (var b in BonusesAndServices)
                b.TextChanged = false;

            return false;
        }

        public bool NoBonusesAnServicesIsChanged()
        {
            BonusChangesString = "";
            OptionChangesString = "";
            ChangesFlag = false;

            if (_permission == Permissions.Level0)
                return false;

            bool bBonusesFailed = false;
            var service = (CruiseService)Service;
            if (service == null) return true; // если не было записи в базе, то считаем данные измененными (как исключение)
            if (BonusesAndServices == null || service.BonusesAndServices == null || BonusesAndServices.Count != service.NoBonusesAndServices.Count)
                //return false;
                bBonusesFailed = true;

            bool bBonusesChanged = false;
            if (!bBonusesFailed)
            {
                for (var i = 0; i < BonusesAndServices.Count; i++)
                {
                    var f = BonusesAndServices[i];
                    var s = service.NoBonusesAndServices[i];

                    if (f.Id != s.Id || f.Text != s.Text || f.Name != s.Name || f.IsRight != s.IsRight)
                    {
                        AddChangedField(ref BonusChangesString, f.Name + ": " + f.Text);
                        bBonusesChanged = true;
                    }
                }
            }

            bool bOptionChanged = CruiseInfo.ChekChanged(service);

            if (bOptionChanged || bBonusesChanged)
            {
                OptionChangesString = "";

                //if (CruiseInfo.Category != service.Category) AddChangedField(ref OptionChangesString, "Категоря каюты: " + CruiseInfo.Category);
                bool bCategory = false;
                if (service.Category == null && CruiseInfo.Category != null) bCategory = true;
                if (service.Category != null && CruiseInfo.Category == null) bCategory = true;
                if (service.Category != null && !service.Category.Equals(CruiseInfo.Category)) bCategory = true;
                if (bCategory)
                {
                    AddChangedField(ref OptionChangesString, "(!) Категоря каюты: " + CruiseInfo.Category );
                }
                if (CruiseInfo.CabinNumber != service.CabinNumber) AddChangedField(ref OptionChangesString, "№ каюты: " + CruiseInfo.CabinNumber);
                //if (CruiseInfo.CabinDef != service.CabinDef) AddChangedField(ref OptionChangesString, "CabinDef: " + CruiseInfo.CabinDef);
                if (CruiseInfo.OptionNumber != service.OptionNumber) AddChangedField(ref OptionChangesString, "№ опции: " + CruiseInfo.OptionNumber);
                if (CruiseInfo.OptionDate != service.OptionDate) AddChangedField(ref OptionChangesString, "Дата опции: " + CruiseInfo.OptionDate.ToString("dd.MM.yy   HH:mm"));
                string s_field = "Опция не подтверждена";
                if (CruiseInfo.IsBook) s_field = "Бронь подтверждена";
                if (CruiseInfo.IsBook != service.IsBook) AddChangedField(ref OptionChangesString, s_field);

                AddChangedField(ref OptionChangesString, BonusChangesString);

                s_field = "Документы не запрошены";
                if (CruiseInfo.DocumentQuery) s_field = "Документы запрошены";
                if (CruiseInfo.DocumentQuery != service.DocumentQuery) AddChangedField(ref OptionChangesString, s_field);
                s_field = "Документы не получены";
                if (CruiseInfo.DocumentGet) s_field = "Документы получены";
                if (CruiseInfo.DocumentGet != service.DocumentGet) AddChangedField(ref OptionChangesString, s_field);

                ChangesFlag = true;
                return true;
            }

            //if (!bBonusesFailed)
            if (BonusesAndServices != null)
            {
                foreach (var b in BonusesAndServices)
                    b.TextChanged = false;
            }

            return false;
        }

        private void LoadOptions()
        {
            CruiseInfo = new CruiseInfo((CruiseService)Service);
        }

        private void LoadBonusesAndServices()
        {
            var service = (CruiseService)Service;
            //BonusesAndServices = service.BonusesAndServices != null ? new ObservableCollection<BonusAndService>(service.BonusesAndServices.Clone()) : null;
            BonusesAndServices = service.NoBonusesAndServices != null ? new ObservableCollection<BonusAndService>(service.NoBonusesAndServices.Clone()) : null;
            //NoBonusesAndServices = service.NoBonusesAndServices != null ? new ObservableCollection<BonusAndService>(service.NoBonusesAndServices.Clone()) : null;

            if (BonusesAndServices != null)
            {
                foreach (BonusAndService item in BonusesAndServices)
                {
                    bool list_flag = false;
                    bool edit_flag = true;
                    int id = item.Id;
                    if (item.Id == -106) { list_flag = true; edit_flag = false; } // Тип кровати
                    if (item.Id == -110) { list_flag = true; edit_flag = false; } // Вариант питания
                    if (item.Id == -104) { list_flag = true; edit_flag = false; } // Смена питания
                    //if (item.Id == -105) { list_flag = true; edit_flag = false; } // Тип питания
                    //if (item.Name.IndexOf("Вид каюты") >= 0) { list_flag = true; edit_flag = false; }
//                    if (item.Name.IndexOf("Круизный тариф") >= 0) { list_flag = true; edit_flag = false; }

                    for (int i = 0; i < service.lst_DopServices.Count; i++)
                    {
                        DopServiceItem ds = service.lst_DopServices[i];
                        if (ds.ID == item.Id)
                        {
                            if (!string.IsNullOrEmpty(ds.Query))
                            {
                                list_flag = true; 
                                edit_flag = false;
                                break;
                            }
                        }
                    }

                    item.HasList = list_flag;
                    item.IsEditable = edit_flag;
                }
            }
        }

        private void AddOption()
        {
            if (Do_AddOption(true))
            {
                Do_BonusOk();

                Update();
            }
        }

        private bool Do_AddOption(bool confirm_flag)
        {
            var service = (CruiseService)Service;

            if (confirm_flag)
            {
                bool bSave = false;
                bool bCancel = false;
                if (ConfirmSaveFunction != null)
                {
                    int confirm = ConfirmSaveFunction("");
                    if (confirm == 1) bSave = true;
                    if (confirm == -1) bCancel = true;
                }
                else
                {
                    bSave = true;
                    string msg = GetChangesMessage("Сохранить изменения ?");
                    if (!Repository.GetInstance<IMessageBoxService>().ShowMessage(msg, "Подтверждение!", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                        bSave = false;
                }

                if (!bSave)
                {
                    if (bCancel)
                    {
                        LoadOptions();
                        LoadBonusesAndServices();
                    }

                    return false;
                }
            }

            bool bCategory = false;
            if (service.Category == null && CruiseInfo.Category != null) bCategory = true;
            if (service.Category != null && CruiseInfo.Category == null) bCategory = true;
            if (service.Category != null && !service.Category.Equals(CruiseInfo.Category)) bCategory = true;
            //if (service.Category == null || !service.Category.Equals(CruiseInfo.Category))
            if (bCategory)
            {
                if (!Repository.GetInstance<IMessageBoxService>().ShowMessage("Категория каюты была изменена. Подтвердить?", "Изменение категории каюты!",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning)) return false;
            }

            ((CruiseService)Service).AddCruiseOption(CruiseInfo);

            //Update();

            return true;
        }

        private void ClearAddOption()
        {
            //string msg = GetChangesMessage("Отменить все изменения ?");

            //if (Repository.GetInstance<IMessageBoxService>().ShowMessage(msg, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            //{
                LoadOptions();
                LoadBonusesAndServices();
            //}
        }

        private void BonusOk()
        {
            bool bOption = Do_AddOption(false);
            if (!bOption) return;

            Do_BonusOk();

            if (bOption) Update();
        }
        
        private void Do_BonusOk()
        {
            var service = (CruiseService) Service;
            var serv = Repository.GetInstance<IVoucherService>();

            serv.CruiseBonusesAndServicesAdd(service.DlKey, BonusesAndServices.Where(b => b.IsToInsert).Select(b => b.Id).ToArray());
            serv.CruiseBonusesAndServicesSet(service.DlKey, BonusesAndServices.Where(b => b.IsRight).Select(b => b.Id).ToArray());
            serv.CruiseBonusesAndServicesReset(service.DlKey, BonusesAndServices.Where(b => !b.IsRight).Select(b => b.Id).ToArray());

            foreach (var b in BonusesAndServices)
                if(b.Text != null)serv.CruiseBonusesAndServicesChangeText(service.DlKey, b.Id, b.Text);
        }

        private void ClearBonusesAndServices()
        {
            if (Repository.GetInstance<IMessageBoxService>()
                .ShowMessage("отменить все изменения", "Отмена изменений", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning))
            {
                LoadBonusesAndServices();
                LoadOptions();
            }
        }

        public void ChangeOption()
        {
            if (CruiseInfo == null) return;
            if (CruiseInfo.ChangeEnableFlag) return;

            CruiseInfo.ChangeEnableFlag = true;
        }

        public void UndoOption()
        {
            if (!NoBonusesAnServicesIsChanged()) return;

            ClearAddOption();
        }

    }
}
