using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public interface IRequestNewMessageViewModelBase
    {

    }

    public class RequestNewMessageViewModel : CorrespondenceRequestViewModel, IRequestNewMessageViewModelBase
    {
        private string _html;
        public string Html
        {
            get { return _html; }
            set
            {
                SetValue(ref _html, value);
            }
        }

        private string _from;
        public string From
        {
            get { return _from; }
            set { SetValue(ref _from, value); }
        }

        private string _to;
        public string To
        {
            get { return _to; }
            set { SetValue(ref _to, value); }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { SetValue(ref _subject, value); }
        }

        private ObservableCollection<User> _destinations;
        public ObservableCollection<User> Destinations
        {
            get { return _destinations; }
            set { SetValue(ref _destinations, value); }
        }

        private ObservableCollection<User> _clients;
        public ObservableCollection<User> Clients
        {
            get { return _clients; }
            set { SetValue(ref _clients, value); }
        }

        public new RelayCommand SendCommand { get; set; }

        public RequestNewMessageViewModel(User user, Request request, CorrespondenceType type, Action updateCallback, Action closeCorrespondenceCallback, IRequestJournalService service = null, IRequestJournalSender sender = null) :
            base(user, request, type, updateCallback, closeCorrespondenceCallback, service, sender)
        {

        }

        public override void UpdateCorrespondence()
        {
            FillDestinations();
            From = new MyWorkMail().Address;
            Subject = RequestMessageBuilder.GetTheme(Request.Number);
        }

        private void FillDestinations()
        {
            switch (_type)
            {
                case CorrespondenceType.Client:
                    Clients = new ObservableCollection<User>(Request.Messages.Select(m => m.IsIncomming ? m.SenderAddress : m.DestinationAddress).Distinct().Select(GetClient));
                    Destinations = Clients;
                    break;
                case CorrespondenceType.Manager:
                    Destinations = Managers;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("CorrespondenceType", _type, null);
            }
        }

        private User GetClient(string address)
        {
            return new User(-1, address, address, null);
        }

        protected override void Send(string message)
        {
            if (SelectedManager == null)return;

            if (Request == null || Request.FirstMessage == null) throw new Exception("Ошибка ответа на запрос: сообщение запроса отсутствует");

            var from = From;
            var to = SelectedManager.Email;
            var subject = Subject;
            var mod = GetMod(_type);
            var replyTo = Request.Messages.LastOrDefault(m => m.Mod.Equals(mod, StringComparison.OrdinalIgnoreCase));
            var msg = RequestMessageBuilder.GetRequestMessageHtml(User, from, to, subject, Request, replyTo, message, UserSignature, mod, Attachments.Select(a => a.Attachment));

            Send(msg);
        }

        private bool GetSendCommandEnabled()
        {
            return SelectedManager != null;
        }

        private string GetMod(CorrespondenceType correspType)
        {
            return correspType == CorrespondenceType.Manager ? "MTM" : correspType == CorrespondenceType.Client ? "MTC" : "";
        }
    }
}
