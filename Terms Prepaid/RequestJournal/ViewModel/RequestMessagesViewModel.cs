using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public interface IRequestMessagesViewModel
    {
        bool Active { get; set; }
        Request Request { get; }
        ObservableCollection<RequestMessageViewModel> Messages { get; set; }
        RequestMessageViewModel SelectedItem { get; set; }
        void Update(Request request);
    }

    public class RequestMessagesViewModel : ViewModelBase, IRequestMessagesViewModel
    {
        public Request Request { get; private set; }

        private ISimpleWindows _newMessageWindows;

        public string Mod { get; set; }

        private ObservableCollection<RequestMessageViewModel> _messages;
        public ObservableCollection<RequestMessageViewModel> Messages
        {
            get { return _messages; }
            set
            {
                SetValue(ref _messages, value);
                MessagesInvert = new ObservableCollection<RequestMessageViewModel>(Messages.OrderByDescending(m => m.RequestMessage.Date));
            }
        }

        private ObservableCollection<RequestMessageViewModel> _messagesInvert;
        public ObservableCollection<RequestMessageViewModel> MessagesInvert
        {
            get { return _messagesInvert; }
            set { SetValue(ref _messagesInvert, value); }
        }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set
            {
                SetValue(ref _active, value);
                WatchMessage();
            }
        }

        private RequestMessageViewModel _selectedItem;
        public RequestMessageViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetValue(ref _selectedItem, value);
                if (_selectedItem != null)
                {
                    WatchMessage();
                    RequestMessageViewModel = value;
                }
            }
        }

        private void WatchMessage()
        {
            if (_selectedItem != null && _selectedItem.RequestMessage != null && !_selectedItem.RequestMessage.Seen && Active)
            {
                Repository.GetInstance<IRequestJournalService>().WathMessage(_selectedItem.RequestMessage.Id);
                _selectedItem.RequestMessage.Seen = true;
                Request.UpdateMessageCounts();
            }
        }

        private RequestMessageViewModel _requestMessageViewModel;
        public RequestMessageViewModel RequestMessageViewModel
        {
            get { return _requestMessageViewModel; }
            set { SetValue(ref _requestMessageViewModel, value); }
        }

        protected RequestNewMessageViewModel _requestNewMessageViewModel { get; set; }

        public RequestMessagesViewModel(RequestNewMessageViewModel requestNewMessageViewModel)
        {
            _requestNewMessageViewModel = requestNewMessageViewModel;
        }

        public void Update(Request request)
        {
            var selectedMessageId = SelectedItem != null ? SelectedItem.RequestMessage.Id : -1;
            
            Request = request;

            if (Request == null) return;

            if (Request.Messages == null || Request.Messages.Count == 0)
                throw new Exception(string.Format("Request №{0} не содержит сообщений", Request.Number));

            Messages = new ObservableCollection<RequestMessageViewModel>(
                request.Messages.Where(m => m.Mod.Equals(Mod, StringComparison.OrdinalIgnoreCase)).Select(m => new RequestMessageViewModel(m, Reply)));

            _requestNewMessageViewModel.Update(Request);

            if (Messages != null && Messages.Count > 0)
            {
                SelectedItem = Messages.FirstOrDefault(m => m.RequestMessage.Id == selectedMessageId) ?? Messages.LastOrDefault();
                RequestMessageViewModel = SelectedItem;
            }
            else
            {
                RequestMessageViewModel = new RequestMessageViewModelMtM(null, Reply);
            }

        }

        protected void Reply(RequestMessageViewModel requestMessageViewModel)
        {
            /*if (_newMessageWindows == null)
                _newMessageWindows = Repository.GetInstance<IWindowsHelper>().GetWindow(_requestNewMessageViewModel, Repository.GetInstance<IRequestNewMessageView>());
            _newMessageWindows.Show();*/

            if (_requestNewMessageViewModel != null && RequestMessageViewModel.RequestMessage != null)
                _requestNewMessageViewModel.SelectedManager =
                    _requestNewMessageViewModel.Destinations.FirstOrDefault(u =>
                        RequestMessageViewModel.RequestMessage.IsIncomming &&
                        u.Email.Equals(RequestMessageViewModel.RequestMessage.SenderAddress) ||
                        !RequestMessageViewModel.RequestMessage.IsIncomming &&
                        u.Email.Equals(RequestMessageViewModel.RequestMessage.DestinationAddress));

            Repository.GetInstance<IWindowsHelper>().ShowWindow(_requestNewMessageViewModel, Repository.GetInstance<IRequestNewMessageView>());
        }
    }

    public class RequestMessagesViewModelMtC : RequestMessagesViewModel
    {
        public RequestMessagesViewModelMtC(RequestNewMessageViewModel requestNewMessageViewModel)
            : base(requestNewMessageViewModel)
        {
            Mod = RequestMessageMod.MTC;
        }
    }

    public class RequestMessagesViewModelMtM : RequestMessagesViewModel
    {
        public RequestMessagesViewModelMtM(RequestNewMessageViewModel requestNewMessageViewModel)
            : base(requestNewMessageViewModel)
        {
            Mod = RequestMessageMod.MTM;
        }
    }
}
