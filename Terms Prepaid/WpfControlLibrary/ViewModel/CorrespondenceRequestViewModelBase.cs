using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.ViewModel
{
    public abstract class CorrespondenceRequestViewModelBase : CorrespondenceBaseViewModel, ICorrespondenceRequestBaseViewModel
    {
        public class DestinationInfo : Data
        {
            private bool _isEditable;
            public bool IsEditable
            {
                get { return _isEditable; }
                set { SetValue(ref _isEditable, value); }
            }

            private ObservableCollection<KeyValuePair<string, string>> _contacts;
            public ObservableCollection<KeyValuePair<string, string>> Contacts
            {
                get { return _contacts; }
                set { SetValue(ref _contacts, value); }
            }

            private KeyValuePair<string, string> _selectedContact;
            public KeyValuePair<string, string> SelectedContact
            {
                get { return _selectedContact; }
                set { SetValue(ref _selectedContact, value); }
            }

            private string _subject;
            public string Subject
            {
                get { return _subject; }
                set { SetValue(ref _subject, value); }
            }
        }

        protected IRequestJournalSender _sender;

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }

        private DestinationInfo _destination;
        public DestinationInfo Destination
        {
            get { return _destination; }
            set { SetValue(ref _destination, value); }
        }

        protected IRequestJournalService _requestService;

        protected Action _updateCallback;

        private FlowDocument _document;
        public FlowDocument Document
        {
            get { return _document; }
            set
            {
                SetValue(ref _document, value);
            }
        }

        private Request _request;
        public Request Request
        {
            get { return _request; }
            set { SetValue(ref _request, value); }
        }

        private HtmlControlViewModel _htmlControlVM;
        public HtmlControlViewModel HtmlControlVM
        {
            get { return _htmlControlVM; }
            set { SetValue(ref _htmlControlVM, value); }
        }

        private string _contentHtml;
        public string ContentHtml
        {
            get { return _contentHtml; }
            set
            {
                SetValue(ref _contentHtml, value);
                Send(_contentHtml);
            }
        }

        private ObservableCollection<AttachmentViewModel> _attachments;
        public ObservableCollection<AttachmentViewModel> Attachments
        {
            get { return _attachments; }
            set { SetValue(ref _attachments, value); }
        }

        private ObservableCollection<User> _managers;
        public ObservableCollection<User> Managers
        {
            get { return _managers; }
            set { SetValue(ref _managers, new ObservableCollection<User>(value.Where(u => !string.IsNullOrEmpty(u.Email)))); }
        }

        protected User _selectedManager;
        public virtual User SelectedManager
        {
            get { return _selectedManager; }
            set { SetValue(ref _selectedManager, value); }
        }

        public void GetAttachment(string param)
        {
            var regex = new Regex(@"attachment:(\d+):(\d+)");
            var math = regex.Match(param);
            if (!math.Success) return;

            int messageId = Convert.ToInt32(math.Groups[1].Value);
            int attachmentId = Convert.ToInt32(math.Groups[2].Value);

            var firstOrDefault = Request.Messages.FirstOrDefault(m => m.Id == messageId);
            if (firstOrDefault == null) return;

            var attachment = firstOrDefault.Attachments.FirstOrDefault(a => a.Id == attachmentId);

            if (attachment == null) return;

            FileHelper.SaveToFileWithDialog(attachment.Name, attachment.Data);
        }

        public void RemoveAttachments()
        {
            Attachments.Clear();
        }

        public RelayCommand CloseButtonCommand { get; set; }
        public RelayCommand AddAttachmentCommand { get; set; }

        protected CorrespondenceRequestViewModelBase(User user, Request request, CorrespondenceType type, Action updateCallback, Action closeCorrespondenceCallback, IRequestJournalService service = null, IRequestJournalSender sender = null)
        {
            Attachments = new ObservableCollection<AttachmentViewModel>();
            User = user;
            _sender = sender ?? Repository.GetInstance<IRequestJournalSender>();
            Destination = new DestinationInfo();
            _requestService = service ?? Repository.GetInstance<IRequestJournalService>();
            Request = request;
            _type = type;
            ButtonStyle = (int)type;
            CloseCorrespVisible = false;
            _updateCallback = updateCallback;
            CloseButtonVisible = true;
            CloseButtonCommand = new RelayCommand(closeCorrespondenceCallback);
            AddAttachmentCommand = new RelayCommand(AddAttachment);
            HtmlControlVM = new HtmlControlViewModel
            {
                HtmlToDisplay = "<html><body><h1>Hello!!!</h1></body></html>"
            };

            Update(request);
        }

        public void Update(Request request)
        {
            if (request == null)
            {
                Request = null;
                NewMessageEnabled = false;
                Corresp = null;
                return;
            }

            Request = request;
            NewMessageEnabled = true;
            UpdateCorrespondence();
        }

        public virtual void UpdateCorrespondence()
        {
            
        }

        public void AddAttachment()
        {
            var filePath = FileHelper.OpenFileGetFilePath();
            if (filePath == null) return;

            var fileName = Path.GetFileName(filePath);
            var data = File.ReadAllBytes(filePath);
            var contentType = FileHelper.GetContenetType(filePath);
            
            var attachment = new RequestAttachment()
            {
                Data = data,
                ContentType = contentType,
                ContentTypeName = contentType.MediaType,
                Name = fileName
            };

            Attachments.Add(new AttachmentViewModel(attachment, RemoveAttachment, null, null));
        }

        private void RemoveAttachment(AttachmentViewModel attachmentViewModel)
        {
            Attachments.Remove(attachmentViewModel);
        }

        public void Send(RequestMessage msg)
        {
            SaveMailToDb(msg);
            SendMail(msg);
            ClearNewMail();
        }

        public void ReSend(RequestMessage msg)
        {
            SendMail(msg);
            ClearNewMail();
        }

        public void Comment(RequestMessage msg)
        {
            SaveMailToDb(msg);
            ClearNewMail();
        }

        private void SaveMailToDb(RequestMessage msg)
        {
            var realId = _requestService.AddMessage(msg.Id, Request.Number, msg.Text, msg.Date, msg.SenderAddress, msg.DestinationAddress, msg.Seen, msg.ReadDate, msg.Theme, msg.IsIncomming,
                msg.InReplyToId, msg.Reply, msg.Html, msg.Mod, msg.User != null ? msg.User.Key : (int?)null);

            msg.Id = realId;

            if (msg.Attachments == null) SaveAttachmentsToDb(msg);
        }

        private void SaveAttachmentsToDb(RequestMessage msg)
        {
            foreach (var a in msg.Attachments)
            {
                a.RequestMessageId = msg.Id;
                _requestService.AddAttachment(a.RequestMessageId, a.ContentTypeName, a.Name, a.Data);
            }
        }

        private void SendMail(RequestMessage msg)
        {
            var dt = _sender.SendMessage(msg, true, false);
            _requestService.SetSent(msg.Id, dt);

            if (_updateCallback != null)
                _updateCallback.Invoke();
        }

        private void ClearNewMail()
        {
            NewMessage = null;
            Attachments = new ObservableCollection<AttachmentViewModel>();
        }

    }
}