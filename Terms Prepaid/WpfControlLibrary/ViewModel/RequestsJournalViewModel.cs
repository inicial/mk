using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using GalaSoft.MvvmLight.Threading;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.View;
using Xceed.Wpf.DataGrid;
using DataRow = System.Data.DataRow;

namespace WpfControlLibrary.ViewModel
{
    /// <summary>
    /// RequestMessagesViewMode
    /// </summary>
    public class RequestsJournalViewModel : RequestsJournalViewModelBase
    {
        private IRequestMessagesViewModel _requestMessagesViewModelMtC;
        public IRequestMessagesViewModel RequestMessagesViewModelMtC
        {
            get { return _requestMessagesViewModelMtC; }
            set { SetValue(ref _requestMessagesViewModelMtC, value); }
        }

        private IRequestMessagesViewModel _requestMessagesViewModelMtM;
        public IRequestMessagesViewModel RequestMessagesViewModelMtM
        {
            get { return _requestMessagesViewModelMtM; }
            set { SetValue(ref _requestMessagesViewModelMtM, value); }
        }

        private RequestMessagesHelper _requestMessagesHelper;

        public RequestsJournalViewModel()
        {
            _requestMessagesHelper = new RequestMessagesHelper();
            var requestNewMessageViewModelMtC = new RequestNewMessageViewModel(User, null, CorrespondenceType.Client, () => Update(), ShowDataGrid) { Managers = Managers };
            var requestNewMessageViewModelMtM = new RequestNewMessageViewModel(User, null, CorrespondenceType.Manager, () => Update(), ShowDataGrid) { Managers = Managers };
            RequestMessagesViewModelMtC = new RequestMessagesViewModelMtC(requestNewMessageViewModelMtC, ShowDataGrid);
            RequestMessagesViewModelMtM = new RequestMessagesViewModelMtM(requestNewMessageViewModelMtM, ShowDataGrid);
            InitFilters();
            Update();
            InitFilters();
        }

        public override void UpdateCorrespondence()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (_selectedItem == null) return;

                var statusHistory = _service.GetRequestStatusHistory2(_selectedItem.Number);
                _selectedItem.RequestStatuses = new ObservableCollection<RequestStatus>(from DataRow row in statusHistory.Rows
                        select new RequestStatus(row.Field<int>("RequestStatusId"), row.Field<string>("Name"),
                            row.Field<DateTime>("Date"), row.Field<int?>("UserId"), row.Field<string>("UsName")));

                _selectedItem.Messages = new ObservableCollection<RequestMessage>(_requestMessagesHelper.GetMessages(_selectedItem.Number));
                RequestMessagesViewModelMtC.Update(_selectedItem);
                RequestMessagesViewModelMtM.Update(_selectedItem);
            });
        }

        protected override void ShowCorrespWithClient()
        {
            HideAllRows = true;
            ShowCorrespondenceWithClient = true;
            ShowCorrespondenceWithManager = false;
            RequestMessagesViewModelMtC.Active = true;
            RequestMessagesViewModelMtM.Active = false;
            SetCorrespondence(RequestMessageMod.MTC);
        }

        protected override void ShowCorrespWithManager()
        {
            HideAllRows = true;
            ShowCorrespondenceWithManager = true;
            ShowCorrespondenceWithClient = false;
            RequestMessagesViewModelMtC.Active = false;
            RequestMessagesViewModelMtM.Active = true;
            SetCorrespondence(RequestMessageMod.MTM);
        }

        public override void ShowDataGrid()
        {
            HideAllRows = false;
            ShowCorrespondenceWithManager = false;
            ShowCorrespondenceWithClient = false;
            RequestMessagesViewModelMtC.Active = false;
            RequestMessagesViewModelMtM.Active = false;
        }
    }
}
