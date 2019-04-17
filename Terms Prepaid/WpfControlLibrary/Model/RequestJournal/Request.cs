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
using DataService;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GemBox.Email.Imap;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using MailKit.Search;
using Org.BouncyCastle.Bcpg;
using S22.Imap;
using Sgml;
using Utilities.DataTypes.ExtensionMethods;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
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
    public class Request : RequestBase
    {
        public const int AnnulateId = -1;
        private Action<Request> _annulateAction;

        private readonly IRequestJournalService _requestJournalService;

        public RelayCommand<RequestStatus> StatusChangedCommand { get; private set; }
        public RelayCommand ChangeSenderAddressCommand { get; private set; }
        public RelayCommand SuperviserCheckedCmd { get; private set; }
        public RelayCommand<RelayCommand> SuperviserCheckedAndShowMessagesCmd { get; private set; }
        
        public RelayCommand CloseCorrespondenceCmd { get; private set; }
        
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

        private RequestStatus _status;
        public RequestStatus Status
        {
            get { return _status; }
            set
            {
                SetValue(ref _status, value);
            }
        }

        private int _statusSelectedIndex;
        public int StatusSelectedIndex
        {
            get { return _statusSelectedIndex; }
            set { SetValue(ref _statusSelectedIndex, value); }
        }

        private bool _isClosed;
        public bool IsClosed
        {
            get { return _isClosed; }
            set { SetValue(ref _isClosed, value); }
        }

        private DateTime? _superviserChecked;
        public DateTime? SuperviserChecked
        {
            get { return _superviserChecked; }
            set { SetValue(ref _superviserChecked, value); }
        }

        private RequestSubStatus _subStatus;
        public RequestSubStatus SubStatus
        {
            get { return _subStatus; }
            set { SetValue(ref _subStatus, value); }
        }

        private ObservableCollection<RequestStatus> _statusDictionary;
        public ObservableCollection<RequestStatus> StatusDictionary
        {
            get { return _statusDictionary; }
            set { SetValue(ref _statusDictionary, value); }
        }

        private ObservableCollection<RequestSubStatus> _substatusDictionary;
        public ObservableCollection<RequestSubStatus> SubstatusDictionary
        {
            get { return _substatusDictionary; }
            set { SetValue(ref _substatusDictionary, value); }
        }

        private ObservableCollection<RequestStatus> _requestStatuses;
        public ObservableCollection<RequestStatus> RequestStatuses
        {
            get { return _requestStatuses; }
            set
            {
                SetValue(ref _requestStatuses, value);
                Status = _requestStatuses != null ? _requestStatuses.LastOrDefault() : null;
            }
        }

        private ObservableCollection<RequestSubStatus> _requestSubStatuses;
        public ObservableCollection<RequestSubStatus> RequestSubStatuses
        {
            get { return _requestSubStatuses; }
            set
            {
                SetValue(ref _requestSubStatuses, value);
                SubStatus = _requestSubStatuses != null ? _requestSubStatuses.LastOrDefault() : null;
            }
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
                LastMessageMtC = Messages != null ? Messages.LastOrDefault(m => m.Mod.Equals(RequestMessageMod.MTC)) : null;
                LastMessageMtM = Messages != null ? Messages.LastOrDefault(m => m.Mod.Equals(RequestMessageMod.MTM)) : null;
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

        private string _dgCode;
        public string DgCode
        {
            get { return _dgCode; }
            set { SetValue(ref _dgCode, value); }
        }

        private string _senderAddress;
        public string SenderAddress
        {
            get { return _senderAddress; }
            set { SetValue(ref _senderAddress, value); }
        }

        private string _theme;
        public string Theme
        {
            get { return _theme; }
            set { SetValue(ref _theme, value); }
        }

        private RequestMessageInfo _mtMInfo;
        public RequestMessageInfo MtMInfo
        {
            get { return _mtMInfo; }
            set { SetValue(ref _mtMInfo, value); }
        }

        private RequestMessageInfo _mtCInfo;
        public RequestMessageInfo MtCInfo
        {
            get { return _mtCInfo; }
            set { SetValue(ref _mtCInfo, value); }
        }

        private byte _source;
        public byte Source
        {
            get { return _source; }
            set { SetValue(ref _source, value); }
        }

        private string _mainN;
        public string MainN
        {
            get { return _mainN; }
            set { SetValue(ref _mainN, value); }
        }

        public Request(Action<Request> annulateAction)
        {
            _requestJournalService = Repository.GetInstance<IRequestJournalService>();
            _annulateAction = annulateAction;

            StatusChangedCommand = new RelayCommand<RequestStatus>(StatusChanged);

            ChangeSenderAddressCommand = new RelayCommand(() => ChangeSenderAddress(null));

            SuperviserCheckedCmd = new RelayCommand(SuperviseCheck);

            SuperviserCheckedAndShowMessagesCmd = new RelayCommand<RelayCommand>(cmd =>
            {
                SuperviseCheck();
                cmd.Execute(null);
            });

            CloseCorrespondenceCmd = new RelayCommand(() =>
            {
                var usKey = Session.GetInstance().UsKey;
                _requestJournalService.CloseCorrespondence(Number, usKey);
                IsClosed = true;
            });
        }

        private void SuperviseCheck()
        {
            var usKey = Session.GetInstance().UsKey;
            _requestJournalService.SuperviserChecked(Number, usKey);
            SuperviserChecked = DateTime.Now;
        }

        public void ChangeSenderAddress(string newSenderAddress)
        {
            //Repository.GetInstance<IWindowsHelper>().ShowWindow(new ChangeSenderAddressViewModel(FirstMessage, newSenderAddress), Repository.GetInstance<IChangeSenderAddressView>(), true, true);

            var oldSenderAddress = FirstMessage != null ? FirstMessage.SenderAddress : null;

            Repository.GetInstance<IWindowsHelper>().ShowWindow(new ChangeSenderAddress2ViewModel(ChangeSenderAddressHandler, oldSenderAddress, newSenderAddress), 
                Repository.GetInstance<IChangeSenderAddressView>(), true, true);
        }

        private void ChangeSenderAddressHandler(string newAddress)
        {
            if (FirstMessage != null)
                _requestJournalService.ChangeSenderAddress(FirstMessage.Id, newAddress);
        }

        public void StatusChanged(RequestStatus status)
        {
            if (status != null && status.Id == AnnulateId && _annulateAction != null)
            {
                Status = StatusDictionary.FirstOrDefault();
                _annulateAction.Invoke(this);
            }
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
