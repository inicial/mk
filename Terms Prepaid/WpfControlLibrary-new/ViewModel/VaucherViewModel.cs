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
    public class VaucherViewModel : TabAbstractViewModel
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
            set { SetValue(ref _voucher, value); }
        }

        private Service _selectedService;
        public Service SelectedService
        {
            get { return _selectedService; }
            set
            {
                SetValue(ref _selectedService, value);
                Select();
            }
        }

        private ICommand _selectServiceCommand;
        public ICommand SelectServiceCommand
        {
            get
            {
                return _selectServiceCommand ?? (_selectServiceCommand = new RelayCommand(p => Select()));
            }
        }

        public event MyControlEventHandlerSample OnRowSelect;

        public VaucherViewModel(RowSelectedEventHandler rowSelected, string tabName)
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
        }
    }
}
