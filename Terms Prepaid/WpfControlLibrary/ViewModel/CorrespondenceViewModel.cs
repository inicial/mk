using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class CorrespondenceViewModel : ViewModelBase
    {
        private CorrespondenceTabViewModel _managerMessagesViewModel;
        private CorrespondenceTabViewModel _clientMessagesViewModel;

        private SolidColorBrush _brushManager;
        public SolidColorBrush BrushManager
        {
            get { return _brushManager; }
            set { SetValue(ref _brushManager, value); }
        }

        private SolidColorBrush _brushClient;
        public SolidColorBrush BrushClient
        {
            get { return _brushClient; }
            set { SetValue(ref _brushClient, value); }
        }

        private SolidColorBrush _tabControlBrush;
        public SolidColorBrush TabControlBrush
        {
            get { return _tabControlBrush; }
            set { SetValue(ref _tabControlBrush, value); }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                SetValue(ref _selectedIndex, value);
                TabControlBrush = _selectedIndex == 0 ? _brushManager : _brushClient;
            }
        }

        public void SetCorrespondence(string dgCode)
        {
            _managerMessagesViewModel.SetCorrespondence(dgCode, CorrespondenceType.Manager);
            _clientMessagesViewModel.SetCorrespondence(dgCode, CorrespondenceType.Client);
        }

        public CorrespondenceTabViewModel ManagerMessagesViewModel
        {
            get { return _managerMessagesViewModel; }
            set { SetValue(ref _managerMessagesViewModel, value); }
        }

        public CorrespondenceTabViewModel ClientMessagesViewModel
        {
            get { return _clientMessagesViewModel; }
            set { SetValue(ref _clientMessagesViewModel, value); }
        }

        public CorrespondenceViewModel(string dgCode)
        {
            BrushManager = (SolidColorBrush) new BrushConverter().ConvertFrom("#d2b38d");
            BrushClient = (SolidColorBrush)new BrushConverter().ConvertFrom("#80cdf5");
            SelectedIndex = 0;

            _managerMessagesViewModel = new CorrespondenceTabViewModel(dgCode, CorrespondenceType.Manager) { Brush = BrushManager };
            _clientMessagesViewModel = new CorrespondenceTabViewModel(dgCode, CorrespondenceType.Client) { Brush = BrushClient };
        }
    }
}
