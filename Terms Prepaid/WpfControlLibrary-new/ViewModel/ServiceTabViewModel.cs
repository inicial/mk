using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.Util;
using WpfControlLibrary.View;

namespace WpfControlLibrary.ViewModel
{
    public class ServiceTabViewModel : TabAbstractViewModel
    {
        public delegate void SelectedEventHandler(CaruselData selected);

        private SelectedEventHandler _selectedEventHandler;

        public bool CaruselEnabled { get; set; }
        
        private ServiceViewModel _serviceViewModel;
        public ServiceViewModel ServiceViewModel
        {
            get { return _serviceViewModel; }
            set
            {
                SetValue(ref _serviceViewModel, value);
                ServiceView.DataContext = _serviceViewModel;
            }
        }

        private string _tabName;
        public string TabName
        {
            get { return _tabName; }
            set { SetValue(ref _tabName, value); }
        }

        private IServiceView _serviceView;
        public IServiceView ServiceView
        {
            get { return _serviceView; }
            set { SetValue(ref _serviceView, value); }
        }

        public void InvokeSelectedEvent()
        {
            _serviceViewModel.Service = _selectedService;
            _serviceView.ScrollToTop();

            if (_selectedEventHandler != null && _selectedService != null)
                _selectedEventHandler.Invoke(_selectedService);
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetValue(ref _selectedIndex, value); }
        }

        private CaruselData _selectedService;
        public CaruselData SelectedService
        {
            get { return _selectedService; }
            set
            {
                if (value == _selectedService) 
                    return;

                if (value == null)
                    _serviceViewModel.Service = null;

                if (_serviceViewModel != null && value != null)
                {
                    SetValue(ref _selectedService, value);

                    Move(Services.IndexOf(_selectedService));

                    if (_serviceViewModel.Service != null && _serviceViewModel.Service.EqualData(_selectedService))
                        return;

                    InvokeSelectedEvent();
                }
            }
        }

        private ObservableCollection<CaruselData> _services;
        public ObservableCollection<CaruselData> Services
        {
            get { return _services; }
            set
            {
                SetValue(ref _services, value);

                if (_services != null && _services.Count > 0)
                {
                    if (SelectedService != null)
                    {
                        CaruselData lastService = _services.FirstOrDefault(s => s.EqualData(_selectedService));
                        if (lastService != null)
                            SelectedIndex = _services.IndexOf(lastService);
                        else
                            SelectedService = _services.Last();
                    }
                    else
                        SelectedIndex = _services.Count - 1;

                    IsEmpty = false;
                }
                else
                    IsEmpty = true;
            }
        }

        private void Move(int selectedIndex)
        {
            if (selectedIndex < 0)
                return;

            int move = _services.Count - selectedIndex - 1;

            for (int i = 0; i < _services.Count; i++)
            {
                var serv = _services[i];
                if (CaruselEnabled)
                {
                    serv.RowIndex = (i + move)%_services.Count;
                    serv.Visible = serv.RowIndex == _services.Count - 1 ? "Collapsed" : "Visible";
                }
                else
                {
                    serv.RowIndex = i;
                    serv.Visible = serv.RowIndex == selectedIndex ? "Collapsed" : "Visible";
                }
            }
        }

        public ServiceTabViewModel(IServiceView serviceView, ServiceViewModel serviceViewModel, SelectedEventHandler selectedEventHandler, 
            string tabName = null, bool caruselEnabled = false)
        {
            Services = new ObservableCollection<CaruselData>();
            _selectedEventHandler = selectedEventHandler;
            ServiceView = serviceView;
            ServiceViewModel = serviceViewModel;
            CaruselEnabled = caruselEnabled;
            TabName = tabName;
        }
    }
}
