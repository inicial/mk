using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DataService;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WpfControlLibrary.Util;
using WpfControlLibrary.Common;
using WpfControlLibrary.Messages;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.View;

namespace WpfControlLibrary.ViewModel
{
    public class ServiceTabSelectorViewModel : ViewModelBase
    {
        public RelayCommand TestCommand { get; set; }

        public static class Pages
        {
            public const string Flight = "А/П";
            public const string Cruise = "Круиз";
            public const string Hotel = "Отели";
            public const string Inshur = "Страховки";
            public const string Other = "Другие услуги";
            public const string Visa = "Визы";
        }

        private ServiceTabWrapper _selectedWrap;
        public ServiceTabWrapper SelectedWrap
        {
            get { return _selectedWrap; }
            set
            {
                SetValue(ref _selectedWrap, value);
                OnSelectedItem(_selectedWrap.ViewModel);
            }
        }

        private ObservableCollection<ServiceTabWrapper> _serviceTabWrappers;
        public ObservableCollection<ServiceTabWrapper> ServiceTabWrappers
        {
            get { return _serviceTabWrappers; }
            set { SetValue(ref _serviceTabWrappers, value); }
        }

        public ServiceTabSelectorViewModel(ServiceTabViewModel.SelectedEventHandler selHandler, CruiseViewModel.UpdateVoucherDelegate updateVoucherHandler, 
            MyControlEventHandlerSample showInshuranceEventHandler, ServiceViewModel.Permissions permissionLevel, Voucher v, VoucherViewModel vm)
        {
            ServiceTabWrappers = new ObservableCollection<ServiceTabWrapper>();

            AttachVoucherTab2(Repository.GetInstance<IVoucherTabView>(), vm);

            AttachServiceTab2(Repository.GetInstance<ICruiseControl>(),
                new CruiseViewModel(updateVoucherHandler, permissionLevel), selHandler, v.CruiseServiceList, Pages.Cruise);
            AttachServiceTab2(Repository.GetInstance<IHotelControl>(), new HotelViewModel(), selHandler, v.HotelServiceList, Pages.Hotel);
            AttachServiceTab2(Repository.GetInstance<IFlightControl>(), new FlightViewModel(), selHandler, v.AviaServiceList, Pages.Flight);
            AttachServiceTab2(Repository.GetInstance<IInshurControl>(), new InshurViewModel(showInshuranceEventHandler), selHandler, v.InshurServiceList, Pages.Inshur);
            AttachServiceTab2(Repository.GetInstance<IVisaControl>(), new VisaViewModel(), selHandler, v.VisaServiceList, Pages.Visa);
            AttachServiceTab2(Repository.GetInstance<IOtherServiceControl>(), new OtherServiceViewModel(), selHandler, v.OtherServiceList, Pages.Other);

            TestCommand = new RelayCommand(
                ()
                    =>
                {
                    OnSelectedItem(SelectedWrap.ViewModel);
                });
        }

        public void AttachServiceTab2(IServiceView view, ServiceViewModel viewModel, ServiceTabViewModel.SelectedEventHandler callback, 
            IEnumerable<Service> services, string tabName)
        {
            IServiceTabView tabView = new ServiceTabView();// Repository.GetInstance<IServiceTabView>();
            ServiceTabViewModel serviceTabViewModel = new ServiceTabViewModel(view, viewModel, ServiceSelectedHandler, tabName)
            {
                Services = new ObservableCollection<CaruselData>(services),
                TypeId = 2
            };
            tabView.DataContext = serviceTabViewModel;
            ServiceTabWrappers.Add(new ServiceTabWrapper(tabView, serviceTabViewModel, tabName) { TypeId = 2 });
        }

        public void AttachVoucherTab2(IVoucherTabView view, VoucherViewModel viewModel)
        {
            viewModel.TypeId = 1;
            ServiceTabWrappers.Add(new ServiceTabWrapper(view, viewModel, "Вся путевка") { TypeId = 1 });
        }

        public void OnSelectedItem(TabAbstractViewModel vm)
        {
            if (vm == null)
                return;

            if (vm is VoucherViewModel)
            {
                var voucherViewModel = (VoucherViewModel)vm;
                
                if(voucherViewModel.Voucher != null)
                    Messenger.Default.Send(new SetVoucherMessage(voucherViewModel.Voucher.DgCode));
            }
            else if (vm is ServiceTabViewModel)
            {
                var serviceTabViewModel = (ServiceTabViewModel)vm;

                ServiceSelectedHandler(serviceTabViewModel.SelectedService);
            }
        }

        private void ServiceSelectedHandler(CaruselData data)
        {
            if (!(data is Service))
                return;

            var service = (Service) data;

            Messenger.Default.Send(new SetServiceMessage(service.DgCode, service));
        }
    }
}
