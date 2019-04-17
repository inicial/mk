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
    public class CruiseViewModel : ServiceViewModel
    {
        public delegate void UpdateVoucherDelegate(bool aviaErrorCallback = false, ServiceType serviceType = ServiceType.Unknow);

        public RelayCommand AddOptionCommand { get; set; }
        public RelayCommand ClearAddOptionCommand { get; set; }
        public RelayCommand BonusOkCommand { get; set; }
        public RelayCommand ClearBonusesAndServicesCommand { get; set; }

        private Permissions _permission;
        public Permissions Permission
        {
            get { return _permission; }
            set { SetValue(ref _permission, value); }
        }

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

        private bool _isBook;
        public bool IsBook
        {
            get { return _isBook; }
            set { SetValue(ref _isBook, value); }
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

        private ObservableCollection<BonusAndService> _bonusesAndServices;
        public ObservableCollection<BonusAndService> BonusesAndServices
        {
            get { return _bonusesAndServices; }
            set { SetValue(ref _bonusesAndServices, value); }
        }

        private UpdateVoucherDelegate _updateVoucherHandler;

        public CruiseViewModel(UpdateVoucherDelegate callback, Permissions permission)
        {
            _updateVoucherHandler = callback;
            Permission = permission;
            AddOptionCommand = new RelayCommand(AddOption, OptionIsChanged);
            ClearAddOptionCommand = new RelayCommand(ClearAddOption, OptionIsChanged);
            BonusOkCommand = new RelayCommand(BonusOk, BonusesAnServicesIsChanged);
            ClearBonusesAndServicesCommand = new RelayCommand(ClearBonusesAndServices, BonusesAnServicesIsChanged);
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

        private bool OptionIsChanged()
        {
            var service = (CruiseService)Service;
            if (service == null) return false;

            return Category != service.Category || CabinNumber != service.CabinNumber || CabinDef != service.CabinDef ||
                OptionNumber != service.OptionNumber || OptionDate != service.OptionDate || IsBook != service.IsBook ||
                DocumentGet  != service.DocumentGet || DocumentQuery != service.DocumentQuery || !string.IsNullOrEmpty(SpecCanc);
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

        private void LoadOptions()
        {
            var service = (CruiseService) Service;
            Category = service.Category;
            CabinNumber = service.CabinNumber;
            CabinDef = service.CabinDef;
            OptionNumber = service.OptionNumber;
            OptionDate = service.OptionDate;
            IsBook = service.IsBook;
            DocumentGet = service.DocumentGet;
            DocumentQuery = service.DocumentQuery;
            SpecCanc = "";
        }

        private void LoadBonusesAndServices()
        {
            var service = (CruiseService)Service;
            BonusesAndServices = service.BonusesAndServices != null ? new ObservableCollection<BonusAndService>(service.BonusesAndServices.Clone()) : null;
        }

        private void AddOption()
        {
            var service = (CruiseService) Service;

            if (!Repository.GetInstance<IMessageBoxService>()
                    .ShowMessage("Вы уверены что хотите добавить опцию?", "Проверка!", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning)) return;

            if (service.Category == null || !service.Category.Equals(Category))
            {
                if (!Repository.GetInstance<IMessageBoxService>().ShowMessage("Категория каюты была изменена. Подтвердить?", "Изменение категории каюты!",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning)) return;
            }

            Repository.GetInstance<IVoucherService>().AddCruiseOption(service.DlKey, "Отказ по спец сервису " + SpecCanc, OptionNumber, CabinNumber, CabinDef,
                Category, OptionDate, IsBook, DocumentQuery, DocumentGet);

            Update();
        }

        private void ClearAddOption()
        {
            if (Repository.GetInstance<IMessageBoxService>().ShowMessage("отменить все изменения", "Отмена изменений", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                LoadOptions();
        }

        
        private void BonusOk()
        {
            var service = (CruiseService) Service;
            var serv = Repository.GetInstance<IVoucherService>();

            serv.CruiseBonusesAndServicesSet(service.DlKey, BonusesAndServices.Where(b => b.IsRight).Select(b => b.Id).ToArray());
            serv.CruiseBonusesAndServicesReset(service.DlKey, BonusesAndServices.Where(b => !b.IsRight).Select(b => b.Id).ToArray());

            foreach (var b in BonusesAndServices)
                if(b.Text != null)serv.CruiseBonusesAndServicesChangeText(service.DlKey, b.Id, b.Text);

            Update();
        }

        private void ClearBonusesAndServices()
        {
            if (Repository.GetInstance<IMessageBoxService>()
                .ShowMessage("отменить все изменения", "Отмена изменений", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning))
                LoadBonusesAndServices();
        }
    }
}
