using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WildberriesHomework.Util;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.ViewModel
{
    public class VoucherViewModel : TabAbstractViewModel
    {
        public delegate void RowSelectedEventHandler(Service selected);

        private event RowSelectedEventHandler _rowSelected;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetValue(ref _title, value); }
        }

        private Voucher _voucher;
        public Voucher Voucher
        {
            get { return _voucher; }
            set 
            { 
                SetValue(ref _voucher, value);
                AccordPartnerFlag();
            }
        }

        private Service _selectedService;
        public Service SelectedService
        {
            get { return _selectedService; }
            set
            {
                SetValue(ref _selectedService, value);
                Select();
                //if (_selectedService != null)
                //    SelectedServiceName = _selectedService.Name;
            }
        }

        private string _selectedServiceName;
        public string SelectedServiceName
        {
            get { return _selectedServiceName; }
            set { SetValue(ref _selectedServiceName, value); }
        }

        private string _serviceCurrency;
        public string ServiceCurrency
        {
            //get { return "EUR"; }
            get { return _serviceCurrency; }
            set { SetValue(ref _serviceCurrency, value); }
        }

        public bool PartnerFlag { get; set; }

        private ICommand _selectServiceCommand;
        public ICommand SelectServiceCommand
        {
            get
            {
                return _selectServiceCommand ?? (_selectServiceCommand = new RelayCommand(p => Select()));
            }
        }

        public event MyControlEventHandlerSample OnRowSelect;

        public VoucherViewModel(RowSelectedEventHandler rowSelected, string tabName)
        {
            _rowSelected = rowSelected;
            TabName = tabName;
        }

        private void Select()
        {
            //var dlKey = SelectedService.DlKey;

            if (_rowSelected != null)
                _rowSelected(SelectedService);
        }

        public void SetCurrency(Currency currency, decimal price)
        {
            Voucher.SetCurrency(currency, price);

            ServiceCurrency = currency.ToString();
        }

        public void SetFilterKey(int FilterKey)
        {
            Voucher.SetFilterKey(FilterKey);
        }

        public void AccordPartnerFlag()
        {
            if (Voucher == null) return;

            Voucher.SetPartnerFlag(PartnerFlag);
        }

        public void SetPartnerFlag(bool bPartner)
        {
            PartnerFlag = bPartner;
            AccordPartnerFlag();
        }

    }
}
