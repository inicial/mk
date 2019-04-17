using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Forms;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public static class RequestMessageMod
    {
        public static string MTM = "MTM";
        public static string MTC = "MTC";
        public static string COM = "COM";
    }

    public interface ICorrespondenceBaseViewModel
    {
        event EventHandler ScrollBottom;
    }

    public class CorrespondenceBaseViewModel : ViewModelBase, ICorrespondenceBaseViewModel
    {
        public event EventHandler ScrollBottom;

        protected int _maxSymbols = int.MaxValue;
        protected readonly string _correspClose = "MCO";

        protected CorrespondenceType _type;

        protected ICorrespondenceService _service;

        private CorrespondenceBase _corresp;
        public CorrespondenceBase Corresp
        {
            get { return _corresp; }
            set { SetValue(ref _corresp, value); }
        }

        private string _newMessage;
        public string NewMessage
        {
            get { return _newMessage; }
            set { SetValue(ref _newMessage, value); }
        }

        private bool _newMessageEnabled;
        public bool NewMessageEnabled
        {
            get { return _newMessageEnabled; }
            set { SetValue(ref _newMessageEnabled, value); }
        }

        private System.Windows.Media.Brush _brush;
        public System.Windows.Media.Brush Brush
        {
            get { return _brush; }
            set { SetValue(ref _brush, value); }
        }

        private int _buttonStyle;
        public int ButtonStyle
        {
            get { return _buttonStyle; }
            set { SetValue(ref _buttonStyle, value); }
        }

        private bool _closeCorresp;
        public bool CloseCorresp
        {
            get { return _closeCorresp; }
            set
            {
                SetValue(ref _closeCorresp, value);
                SetCorresponseClosedStatus(value);
            }
        }

        private bool _closeCorrespVisible;
        public bool CloseCorrespVisible
        {
            get { return _closeCorrespVisible; }
            set { SetValue(ref _closeCorrespVisible, value); }
        }

        private bool _closeButtonVisible;
        public bool CloseButtonVisible
        {
            get { return _closeButtonVisible; }
            set { SetValue(ref _closeButtonVisible, value); }
        }

        public RelayCommand SendCommand { get; protected set; }

        public CorrespondenceBaseViewModel(ICorrespondenceService service = null)
        {
            _service = service ?? Repository.GetInstance<ICorrespondenceService>();
            SendCommand = new RelayCommand(() => Send(NewMessage));
        }

        protected virtual void UpdateCorrespodence()
        {
            
        }

        protected virtual void Send(string msg)
        {

        }

        protected IEnumerable<string> SplitString(string str, int maxSymbols)
        {
            return maxSymbols > 0 ?
                Enumerable.Range(0, (int)Math.Ceiling((double)str.Length / maxSymbols)).Select(i => str.Substring(i * maxSymbols, Math.Min(str.Length - i * maxSymbols, maxSymbols)))
                : null;
        }

        protected void SetCorresponseClosedStatus(bool status)
        {
            NewMessageEnabled = !status;

            if (!status) return;

            var lastMsg = Corresp.GetLastMessage();

            if (lastMsg == null || !lastMsg.Mod.Equals(_correspClose))
                CloseCorrespondence();
        }

        protected void CloseCorrespondence()
        {
            Repository.GetInstance<ICorrespondenceService>().InsertHistory2(((Correspondence)Corresp).DgCode, "", _correspClose, "");
            UpdateCorrespodence();
        }

        protected bool GetCorresponseClosedStatus()
        {
            var lastMsg = Corresp.GetLastMessage();
            return lastMsg != null && lastMsg.Mod.Equals(_correspClose);
        }
    }

    public class CorrespondenceTabViewModel : CorrespondenceBaseViewModel
    {
        private string _dgCode;

        private const int MAX_SYMBOLS = 252;
        
       // Constructor
        public CorrespondenceTabViewModel(string dgCode, CorrespondenceType type, int maxSymbols = MAX_SYMBOLS, ICorrespondenceService service = null)
            : base(service)
        {
            _maxSymbols = maxSymbols;
            SetCorrespondence(dgCode, type);
        }

        public void SetCorrespondence(string dgCode, CorrespondenceType type)
        {
            _dgCode = dgCode;
            _type = type;
            UpdateCorrespodence();
            CloseCorrespVisible = type == CorrespondenceType.Client;
            ButtonStyle = (int)type;
        }

        protected override void UpdateCorrespodence()
        {
            Corresp = new Correspondence(_dgCode, _type);
            CloseCorresp = GetCorresponseClosedStatus();
        }

        protected override void Send(string msg)
        {
            if (msg == null || msg.Equals(string.Empty))
                return;

            var lbuff = new List<string>();
            var sb = new StringBuilder();
            var words = msg.Split(' ');

            foreach (var word in words)
            {
                if (sb.Length + word.Length > _maxSymbols)
                {
                    if (sb.Length > 0)
                    {
                        lbuff.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                    if (word.Length > _maxSymbols)
                    {
                        lbuff.AddRange(SplitString(word, _maxSymbols));
                        sb = new StringBuilder();
                        continue;
                    }
                }

                if (sb.Length > 0) 
                    sb.Append(" ");
                
                sb.Append(word);
            }

            if (sb.Length > 0) 
                lbuff.Add(sb.ToString());

            foreach (string s in lbuff)
                _service.InsertHistory2(((Correspondence)Corresp).DgCode, s, Corresp.Mod, "");

            NewMessage = "";

            UpdateCorrespodence();
        }
        
    }

    public interface ICorrespondenceRequestBaseViewModel : ICorrespondenceBaseViewModel
    {
        FlowDocument Document { get; set; }
        string ContentHtml { get; set; }
        void GetAttachment(string param);
        void RemoveAttachments();
    }

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

    public class CorrespondenceRequestViewModel : CorrespondenceRequestViewModelBase
    {
        public CorrespondenceRequestViewModel(User user, Request request, CorrespondenceType type, Action updateCallback, Action closeCorrespondenceCallback, IRequestJournalService service = null, IRequestJournalSender sender = null) :
            base(user, request, type, updateCallback, closeCorrespondenceCallback, service, sender)
        {
            Destination.IsEditable = false;
        }

        public override void UpdateCorrespondence()
        {
            Corresp = new RequestCorrespondenceHtml(Request, CorrespondenceType.Client);

            if (Request == null || Request.Messages == null)
                return;

            var lastMsg = Request.Messages.LastOrDefault(m => m.Mod == RequestMessageMod.MTC);
            if (lastMsg != null)
                Destination.Subject = lastMsg.Theme;

            var firstMsg = Request.Messages.LastOrDefault(m => m.Mod == RequestMessageMod.MTC);
            if (firstMsg != null)
                Destination.SelectedContact = new KeyValuePair<string, string>(firstMsg.SenderAddress, firstMsg.SenderAddress); 
        }

        protected override void Send(string message)
        {
            if(Request == null || Request.FirstMessage == null) throw new Exception("Ошибка ответа на запрос: сообщение запроса отсутствует");

            var msg = RequestMessageBuilder.GetRequestMessageHtml(User, Request, Request.Messages.LastOrDefault(m => m.IsIncomming && m.Mod == RequestMessageMod.MTC), 
                message, RequestMessageMod.MTC, Attachments.Select(a => a.Attachment));

            Send(msg);
        }
    }

    /// <summary>
    /// Переписка по заявке между сотрудниками в формате html
    /// </summary>
    public class CorrespondenceRequestMtMViewModel : CorrespondenceRequestViewModelBase
    {
        public CorrespondenceRequestMtMViewModel(User user, Request request, CorrespondenceType type, Action updateCallback, Action closeCorrespondenceCallback, IRequestJournalService service = null, IRequestJournalSender sender = null) :
            base(user, request, type, updateCallback, closeCorrespondenceCallback, service, sender)
        {
            Destination.IsEditable = true;
        }

        public override void UpdateCorrespondence()
        {
            Corresp = new RequestCorrespondenceHtml(Request, CorrespondenceType.Manager);
        }

        protected override void Send(string message)
        {
            if (Request == null || SelectedManager == null) return;

            var lastMsg = Request.Messages.LastOrDefault(m => m.Mod == RequestMessageMod.MTM && m.IsIncomming);

            var msg = RequestMessageBuilder.GetRequestMessageHtml(User, Request, message, _sender.Config.Address, SelectedManager.Email, RequestMessageMod.MTM, Attachments.Select(a => a.Attachment), lastMsg);
            
            Send(msg);
        }
    }
}
