using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.ViewModel
{
    public class InshurViewModel : ServiceViewModel
    {
        private event MyControlEventHandlerSample _showInsuranceSystemHandler;

        private bool _insuranceManually;
        public bool InsuranceManually
        {
            get { return _insuranceManually; }
            set { SetValue(ref _insuranceManually, value); }
        }

        public RelayCommand InsuranceSystemCommand { get; private set; }

        public RelayCommand InshurOkCommand { get; private set; }

        public InshurViewModel(MyControlEventHandlerSample showInsuranceSystemHandler)
        {
            InsuranceSystemCommand = new RelayCommand(ShowInsuranceSystem);
            InshurOkCommand = new RelayCommand(() => InshurOk(InsuranceManually));
            _showInsuranceSystemHandler = showInsuranceSystemHandler;
        }

        private void ShowInsuranceSystem()
        {
            if (_showInsuranceSystemHandler != null)
                _showInsuranceSystemHandler(this);
        }

        private void InshurOk(bool isOk)
        {
            Repository.GetInstance<IVoucherService>().InshurOk(((Service)Service).DgCode, isOk);
        }

        public override void LoadService()
        {
            InsuranceManually = ((InshurService) Service).IsCreated;
        }
    }
}
