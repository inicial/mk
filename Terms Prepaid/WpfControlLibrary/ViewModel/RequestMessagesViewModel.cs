using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class RequestMessagesViewModel : ViewModelBase, IRequestMessagesViewModel
    {
        private readonly IRequestJournalService _requestJournalService;
        private readonly RequestMessagesHelper _requestMessagesHelper;

        protected Action _closeCallback;

        private Request _request;
        public Request Request
        {
            get { return _request; }
            set
            {
                SetValue(ref _request, value);
                if (_request != null && CorrespondenceIsClosed != _request.IsClosed) CorrespondenceIsClosed = _request.IsClosed;
            }
        }

        private ISimpleWindows _newMessageWindows;

        public string Mod { get; set; }

        private bool _correspondenceIsClosed;
        public bool CorrespondenceIsClosed
        {
            get { return _correspondenceIsClosed; }
            set
            {
                SetValue(ref _correspondenceIsClosed, value);
            }
        }

        public RelayCommand CloseButtonCommand { get; private set; }
        public RelayCommand CloseCorrespondenceCmd { get; private set; }

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

        private RequestMessageViewModel _requestMessageViewModel;
        public RequestMessageViewModel RequestMessageViewModel
        {
            get { return _requestMessageViewModel; }
            set { SetValue(ref _requestMessageViewModel, value); }
        }

        protected RequestNewMessageViewModel _requestNewMessageViewModel { get; set; }

        public RequestMessagesViewModel(RequestNewMessageViewModel requestNewMessageViewModel, Action closeCallback)
        {
            _requestJournalService = Repository.GetInstance<IRequestJournalService>();
            _requestMessagesHelper = new RequestMessagesHelper();
            _requestNewMessageViewModel = requestNewMessageViewModel;
            _closeCallback = closeCallback;
            CloseButtonCommand = new RelayCommand(() =>
            {
                if (_closeCallback != null) _closeCallback.Invoke();
            });

            CloseCorrespondenceCmd = new RelayCommand(() =>
            {
                var usKey = Session.GetInstance().UsKey;
                if (CorrespondenceIsClosed)
                    _requestJournalService.CloseCorrespondence(Request.Number, usKey);
                else
                    _requestJournalService.OpenCorrespondence(Request.Number, usKey);
                
                if(Request!= null)
                    Request.IsClosed = CorrespondenceIsClosed;
            });
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

        public void Update(Request request)
        {
            var selectedMessageId = SelectedItem != null ? SelectedItem.RequestMessage.Id : -1;
            
            Request = request;

            Messages = new ObservableCollection<RequestMessageViewModel>(
                request.Messages.Where(MessageTypeTest)
                    .Select(m => new RequestMessageViewModel(m, Reply, Resubmit, Comment)));

            _requestNewMessageViewModel.Update(Request);

            if (Messages != null && Messages.Count > 0)
            {
                SelectedItem = Messages.FirstOrDefault(m => m.RequestMessage.Id == selectedMessageId) ?? Messages.LastOrDefault();
                RequestMessageViewModel = SelectedItem;
            }
            else
            {
                RequestMessageViewModel = new RequestMessageViewModelMtM(null, Reply, Resubmit, Comment);
            }
        }

        private bool MessageTypeTest(RequestMessage msg)
        {
            return msg.Mod.Equals(Mod, StringComparison.OrdinalIgnoreCase) || 
                (Mod == RequestMessageMod.MTC && msg.Mod.Equals(RequestMessageMod.COM, StringComparison.OrdinalIgnoreCase));
        }

        protected void Reply(RequestMessageViewModel requestMessageViewModel)
        {


            if (_requestNewMessageViewModel == null) return;

            if (_requestNewMessageViewModel.Destinations != null && RequestMessageViewModel.RequestMessage != null)
            {
                var msg = RequestMessageViewModel.RequestMessage;

                _requestNewMessageViewModel.SelectedManager =
                    _requestNewMessageViewModel.Destinations.FirstOrDefault(u =>
                        msg.IsIncomming && msg.SenderAddress != null && u.Email.Equals(msg.SenderAddress) || !msg.IsIncomming && u.Email.Equals(msg.DestinationAddress));
            }

            _requestNewMessageViewModel.CommentMode = false;
            Repository.GetInstance<IWindowsHelper>().ShowWindow(_requestNewMessageViewModel, Repository.GetInstance<IRequestNewMessageView>());
        }

        protected void Resubmit(RequestMessageViewModel requestMessageViewModel)
        {
            if (_requestNewMessageViewModel == null) return;
            _requestNewMessageViewModel.CommentMode = false;

            if (RequestMessageViewModel.RequestMessage != null)
                _requestNewMessageViewModel.ReSend(RequestMessageViewModel.RequestMessage);
        }

        protected void Comment(RequestMessageViewModel requestMessageViewModel)
        {
            if (_requestNewMessageViewModel == null) return;

            if (_requestNewMessageViewModel.Destinations != null && RequestMessageViewModel.RequestMessage != null)
            {
                var msg = RequestMessageViewModel.RequestMessage;

                _requestNewMessageViewModel.SelectedManager =
                    _requestNewMessageViewModel.Destinations.FirstOrDefault(u =>
                        msg.IsIncomming && msg.SenderAddress != null && u.Email.Equals(msg.SenderAddress) || !msg.IsIncomming && u.Email.Equals(msg.DestinationAddress));
            }

            _requestNewMessageViewModel.CommentMode = true;
            Repository.GetInstance<IWindowsHelper>().ShowWindow(_requestNewMessageViewModel, Repository.GetInstance<IRequestNewMessageView>());
        }
    }
}
