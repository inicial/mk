using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class SelectCancelationReasonViewModel : ViewModelBase
    {
        private readonly int _requestId;
        private readonly int _userId;

        private readonly IRequestJournalService _requestJournalService;

        public RelayCommand CanselataionReasonSelectCommand { get; private set; }

        private RequestSubStatus _selectedItem;
        public RequestSubStatus SelectedItem
        {
            get { return _selectedItem; }
            set { SetValue(ref _selectedItem, value); }
        }

        private ObservableCollection<RequestSubStatus> _cancelationReasons;
        public ObservableCollection<RequestSubStatus> CancelationReasons
        {
            get { return _cancelationReasons; }
            set { SetValue(ref _cancelationReasons, value); }
        }

        public SelectCancelationReasonViewModel(int requestId, int userId)
        {
            _requestId = requestId;
            _userId = userId;
            CanselataionReasonSelectCommand = new RelayCommand(OnCanselataionReasonSelect, () => SelectedItem != null);
            _requestJournalService = Repository.GetInstance<IRequestJournalService>();
            Load();
        }

        private void Load()
        {
            CancelationReasons = new ObservableCollection<RequestSubStatus>(_requestJournalService.GetCancelationReasons()
                .Select()
                .Select(r => new RequestSubStatus(r.Field<int>("Id"), r.Field<int>("RequestStatusId"),
                            r.Field<string>("Name"), DateTime.MinValue)));
        }

        private void OnCanselataionReasonSelect()
        {
            var subStatusId = SelectedItem.Id;
            _requestJournalService.AnnulateRequest(_requestId, _userId, subStatusId);
        }
    }
}
