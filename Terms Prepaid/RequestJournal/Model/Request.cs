using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using GemBox.Email.Imap;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using MailKit.Search;
using Org.BouncyCastle.Bcpg;
using S22.Imap;
using Sgml;
using Utilities.DataTypes.ExtensionMethods;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;
using WpfControlLibrary.ViewModel;
using Envelope = Limilabs.Client.IMAP.Envelope;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using ImapClient = MailKit.Net.Imap.ImapClient;
using MessageThread = Limilabs.Client.IMAP.MessageThread;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Класс заявки / запроса от клиента
    /// </summary>
    public class Request : Data
    {
        private RequestMessageBinder _messageBinder;
        public RequestMessageBinder MessageBinder
        {
            get { return _messageBinder; }
            set { SetValue(ref _messageBinder, value); }
        }

        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                SetValue(ref _number, value);
                MessageBinder = new RequestMessageBinder(Number);
            }
        }

        private string _bronNumber;
        public string BronNumber
        {
            get { return _bronNumber; }
            set { SetValue(ref _bronNumber, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetValue(ref _date, value); }
        }

        private int _requestTypeId;
        public int RequestTypeId
        {
            get { return _requestTypeId; }
            set { SetValue(ref _requestTypeId, value); }
        }

        private string _requestTypeStr;
        public string RequestTypeStr
        {
            get { return _requestTypeStr; }
            set { SetValue(ref _requestTypeStr, value); }
        }

        private int _statusId;
        public int StatusId
        {
            get { return _statusId; }
            set { SetValue(ref _statusId, value); }
        }

        private string _statusStr;
        public string StatusStr
        {
            get { return _statusStr; }
            set { SetValue(ref _statusStr, value); }
        }

        private string _performer;
        public string Performer
        {
            get { return _performer; }
            set { SetValue(ref _performer, value); }
        }

        private int _usKey;
        public int UsKey
        {
            get { return _usKey; }
            set { SetValue(ref _usKey, value); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetValue(ref _userName, value); }
        }

        private ObservableCollection<RequestStatus> _requestStatuses;
        public ObservableCollection<RequestStatus> RequestStatuses
        {
            get { return _requestStatuses; }
            set { SetValue(ref _requestStatuses, value); }
        }

        private RequestMessage _firstMessage;
        public RequestMessage FirstMessage
        {
            get { return _firstMessage; }
            set { SetValue(ref _firstMessage, value); }
        }

        private RequestMessage _lastMessage;
        public RequestMessage LastMessage
        {
            get { return _lastMessage; }
            set { SetValue(ref _lastMessage, value); }
        }

        private RequestMessage _lastMessageMtM;
        public RequestMessage LastMessageMtM
        {
            get { return _lastMessageMtM; }
            set { SetValue(ref _lastMessageMtM, value); }
        }

        private RequestMessage _lastMessageMtC;
        public RequestMessage LastMessageMtC
        {
            get { return _lastMessageMtC; }
            set { SetValue(ref _lastMessageMtC, value); }
        }

        private ObservableCollection<RequestMessage> _messages;
        public ObservableCollection<RequestMessage> Messages
        {
            get { return _messages; }
            set
            {
                SetValue(ref _messages, value);
                FirstMessage = Messages != null ? Messages.FirstOrDefault() : null;
                LastMessage = Messages != null ? Messages.LastOrDefault() : null;
                LastMessageMtC = Messages != null ? Messages.LastOrDefault(m => m.Mod.Equals("MTC")) : null;
                LastMessageMtM = Messages != null ? Messages.LastOrDefault(m => m.Mod.Equals("MTM")) : null;
            }
        }

        private int _messagesCountMtm;
        public int MessagesCountMtm
        {
            get { return _messagesCountMtm; }
            set { SetValue(ref _messagesCountMtm, value); }
        }

        private int _messagesCountMtc;
        public int MessagesCountMtc
        {
            get { return _messagesCountMtc; }
            set { SetValue(ref _messagesCountMtc, value); }
        }

        private int _messagesCountMtmUnwatched;
        public int MessagesCountMtmUnwatched
        {
            get { return _messagesCountMtmUnwatched; }
            set { SetValue(ref _messagesCountMtmUnwatched, value); }
        }

        private int _messagesCountMtcUnwatched;
        public int MessagesCountMtcUnwatched
        {
            get { return _messagesCountMtcUnwatched; }
            set { SetValue(ref _messagesCountMtcUnwatched, value); }
        }

        private bool _seen;
        public bool Seen
        {
            get { return _seen; }
            set { SetValue(ref _seen, value); }
        }

        public void UpdateMessageCounts()
        {
            MessagesCountMtm = Messages.Count(m => m.Mod.Equals(RequestMessageMod.MTM));
            MessagesCountMtc = Messages.Count(m => m.Mod.Equals(RequestMessageMod.MTC));
            MessagesCountMtmUnwatched = Messages.Count(m => m.Mod.Equals(RequestMessageMod.MTM) && !m.Seen);
            MessagesCountMtcUnwatched = Messages.Count(m => m.Mod.Equals(RequestMessageMod.MTC) && !m.Seen);
            Seen = MessagesCountMtmUnwatched + MessagesCountMtcUnwatched == 0;
        }

        public void WatchMessages(string mod)
        {
            UpdateMessageCounts();
        }
    }
}
