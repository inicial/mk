using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class ServiceGroup : CaruselData
    {
        private Service _mainService;
        private Service _mainService2;
        private ObservableCollection<Service> _relatedServices;
        private decimal _payedSum;
        private decimal _prePayedSum;
        private decimal _discountSum;
        private ServiceSetting _setting;
        private ObservableCollection<ProblemServiceInfo> _problems;
        private ObservableCollection<ProblemServiceInfo> _uniqProblems;

        public Service MainService
        {
            get { return _mainService; }
            set { SetValue(ref _mainService, value); }
        }

        public ObservableCollection<Service> RelatedServices
        {
            get { return _relatedServices; }
            set { SetValue(ref _relatedServices, value); }
        }

        public decimal PayedSum
        {
            get { return _payedSum; }
            set { SetValue(ref _payedSum, value); }
        }

        public decimal PrePayedSum
        {
            get { return _prePayedSum; }
            set { SetValue(ref _prePayedSum, value); }
        }

        public decimal DiscountSum
        {
            get { return _discountSum; }
            set { SetValue(ref _discountSum, value); }
        }

        public ServiceSetting Setting
        {
            get { return _setting; }
            set { SetValue(ref _setting, value); }
        }

        public ObservableCollection<ProblemServiceInfo> Problems
        {
            get { return _problems; }
            set
            {
                SetValue(ref _problems, value); 
                UniqProblems = new ObservableCollection<ProblemServiceInfo>(GetUniqProblems());
            }
        }

        public ObservableCollection<ProblemServiceInfo> UniqProblems
        {
            get { return _uniqProblems; }
            set { SetValue(ref _uniqProblems, value); }
        }

        private IEnumerable<ProblemServiceInfo> GetUniqProblems()
        {
            return Problems.GroupBy(p => p.ProblemCode).Select(g => g.First());
        }

        public List<Service> Services
        {
            get
            {
                var services = new List<Service> {MainService};
                services.AddRange(RelatedServices);
                return services;
            }
        }

        public ServiceGroup(Service mainService, IEnumerable<Service> relatedServices, Service cruise)
        {
            /*if(cruise == null)
                throw new Exception("Услуга \"Круиз\" не найдена");*/
            MainService = mainService;
            RelatedServices = new ObservableCollection<Service>(relatedServices);
            SetSetting(cruise);
        }

        private void SetSetting(Service cruise)
        {
            Setting = (ServiceSetting)MainService.ServiceSetting.Clone();

            var settings = Services.Select(s => new 
            { 
                s.ServiceSetting.Payment.PaymentValue.Cost,
                Payed = s.ServiceSetting.Payment.PaymentValue.NumericValue,
                PrePayed = !s.ServiceSetting.NullValue ? 
                    s.ServiceSetting.Payment.PrePaymentValue.NumericValue :
                    cruise != null ? 
                    0 /*s.ServiceSetting.Payment.PaymentValue.Cost * cruise.ServiceSetting.Payment.PrePaymentValue.ProcentValue / 100*/ :
                    s.ServiceSetting.Payment.PrePaymentValue.NumericValue,
                Discount = s.ServiceSetting.Discount.TrueNumericValue
            });

            var costSum = settings.Sum(s => s.Cost);
            var payedSum = settings.Sum(s => s.Payed);
            var prePayedSum = Setting.Payment.PrePaymentValue.ValueType == 1 ? costSum * Setting.Payment.PrePaymentValue.ProcentValue / 100 : settings.Sum(s => s.PrePayed);
            var discountSum = settings.Sum(s => s.Discount);

            Setting.Payment.PaymentValue.SetValue(costSum, payedSum);
            Setting.Payment.PrePaymentValue.SetValue(costSum, prePayedSum);
            //Setting.Discount.DiscountValue.SetValue(costSum, discountSum);
            Setting.Discount.TrueNumericValue = discountSum;

            Setting.GetServiceSettingInfo();
        }

        public override bool EqualData(CaruselData obj)
        {
            if (obj == null)
                return false;

            var serviceGroup = obj as ServiceGroup;

            if (serviceGroup == null)
                return false;
            else
                return MainService.DlKey.Equals(serviceGroup.MainService.DlKey);
        }
    }
}
