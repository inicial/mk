using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Navigation;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.ViewModel
{
    public class HotelViewModel : ServiceViewModel
    {
        private bool _hotelConfirmed;
        public bool HotelConfirmed
        {
            get { return _hotelConfirmed; }
            set { SetValue(ref _hotelConfirmed, value); }
        }

        public RelayCommand HotelOkCommand { get; private set; }

        public HotelViewModel()
        {
            HotelOkCommand = new RelayCommand(() => HotelOk(HotelConfirmed));
        }

        private void HotelOk(bool isOk)
        {
            Repository.GetInstance<IVoucherService>().HotelOk2(((Service)Service).DlKey, isOk);
        }
    }
}
