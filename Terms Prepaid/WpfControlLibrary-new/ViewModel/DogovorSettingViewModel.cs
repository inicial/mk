using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class DogovorSettingViewModel : ViewModelBase, IDogovorSetting
    {
        public delegate void UpdateVoucher(bool aviaErrorCallback = false, ServiceType serviceType = ServiceType.Unknow);

        public UpdateVoucher _updateVoucher;
        private VaucherViewModel _vaucherViewModel;

        private PaymentSetting _payment;
        public PaymentSetting Payment
        {
            get { return _payment; }
            set { SetValue(ref _payment, value); }
        }

        private DiscountSetting _discount;
        public DiscountSetting Discount
        {
            get { return _discount; }
            set { SetValue(ref _discount, value); }
        }

        private StatusSetting _status;
        public StatusSetting Status
        {
            get { return _status; }
            set { SetValue(ref _status, value); }
        }

        private string _rate;
        public string Rate
        {
            get { return _rate; }
            set { SetValue(ref _rate, value); }
        }

        private string _serviceTitle;
        public string ServiceTitle
        {
            get { return _serviceTitle; }
            set { SetValue(ref _serviceTitle, value); }
        }

        public DogovorSettingViewModel()
        {
            
        }

        public DogovorSettingViewModel(VaucherViewModel vaucherViewModel, UpdateVoucher updateVoucher)
        {
            _vaucherViewModel = vaucherViewModel;
            _updateVoucher = updateVoucher;
        }

        private void Load(ServiceSetting serviceSetting)
        {
            Payment = serviceSetting.Payment;
            Discount = serviceSetting.Discount;
            Status = serviceSetting.Status;
            Rate = serviceSetting.Rate;

            //Payment.PaymentValue.
        }

        public Service Service { get; private set; }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SetVoucher(int serviceCount)
        {
            throw new NotImplementedException();
        }

        public void SetService(Service service)
        {
            Load(service.ServiceSetting);

            ServiceTitle = service.FullName ??
                                     (service.ServType == ServiceType.Avia
                                         ? "Авиаперелет: " + ((AviaService)service).BookingAvia.Route
                                         : service.ServiceName);
        }

        public void SetDogovorSetting()
        {
            throw new NotImplementedException();
        }
    }
}
