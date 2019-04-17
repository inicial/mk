using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.ViewModel
{
    public class TranfserViewModel : ServiceViewModel
    {
        public RelayCommand SaveChangesCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ChangeDataCommand { get; set; }

        private Permissions _permissions;
        private bool _dataIsChanged;

        private ObservableCollection<string> _route;
        public ObservableCollection<string> Route
        {
            get { return _route; }
            set { SetValue(ref _route, value); }
        }

        private ObservableCollection<TransferService.TransferInfo> _transfersBefore;
        public ObservableCollection<TransferService.TransferInfo> TransfersBefore
        {
            get { return _transfersBefore; }
            set { SetValue(ref _transfersBefore, value); }
        }

        private ObservableCollection<TransferService.TransferInfo> _transfersAfter;
        public ObservableCollection<TransferService.TransferInfo> TransfersAfter
        {
            get { return _transfersAfter; }
            set { SetValue(ref _transfersAfter, value); }
        }

        private ObservableCollection<TransferService.TransferInfo> _transfersBoth;
        public ObservableCollection<TransferService.TransferInfo> TransfersBoth
        {
            get { return _transfersBoth; }
            set { SetValue(ref _transfersBoth, value); }
        }

        public TranfserViewModel(Permissions permissions)
        {
            _permissions = permissions;
            SaveChangesCommand = new RelayCommand(SaveChanges, () => _dataIsChanged);
            CancelCommand = new RelayCommand(Cancel, () => _dataIsChanged);
            ChangeDataCommand = new RelayCommand(() => _dataIsChanged = true);
        }

        public override void LoadService()
        {
            var service = (TransferService)Service;
            service.Update();

            TransfersAfter = UpdateCollection(service.TransfersAfter);
            TransfersBefore = UpdateCollection(service.TransfersBefore);
            TransfersBoth = UpdateCollection(service.TransfersBoth);

            UpdateRoute();
            _dataIsChanged = false;
        }

        private ObservableCollection<TransferService.TransferInfo> UpdateCollection(IEnumerable<TransferService.TransferInfo> transfers)
        {
            return transfers != null ? new ObservableCollection<TransferService.TransferInfo>(transfers) : null;
        }

        private void UpdateRoute()
        {
            var route = new List<string>();

            if (TransfersBefore != null)
                route.AddRange(TransfersBefore.Select(t => t.Route));

            if (TransfersBoth != null)
                route.AddRange(TransfersBoth.Select(t => t.Route));

            if (TransfersAfter != null)
                route.AddRange(TransfersAfter.Select(t => t.Route));

            Route = new ObservableCollection<string>(route);
        }

        private void SaveChanges()
        {
            var service = (TransferService)Service;
            service.SaveTransfers();
            LoadService();
        }

        private void Cancel()
        {
            LoadService();
        }
    }
}
