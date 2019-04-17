using System;
using System.Collections.ObjectModel;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Сообщения переписки запроса
    /// </summary>
    public class RequestMessage : Data
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetValue(ref _id, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetValue(ref _text, value); }
        }

        private int _requestId;
        public int RequestId
        {
            get { return _requestId; }
            set { SetValue(ref _requestId, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetValue(ref _date, value); }
        }

        private string _senderAddress;
        public string SenderAddress
        {
            get { return _senderAddress; }
            set { SetValue(ref _senderAddress, value); }
        }

        private string _destinationAddress;
        public string DestinationAddress
        {
            get { return _destinationAddress; }
            set { SetValue(ref _destinationAddress, value); }
        }

        private bool _seen;
        public bool Seen
        {
            get { return _seen; }
            set
            {
                SetValue(ref _seen, value);
                SeenStr = Seen ? "Просмотрена" : "Не просмотрена";
            }
        }

        private string _seenStr;
        public string SeenStr
        {
            get { return _seenStr; }
            set { SetValue(ref _seenStr, value); }
        }

        private DateTime? _readDate;
        public DateTime? ReadDate
        {
            get { return _readDate; }
            set { SetValue(ref _readDate, value); }
        }

        private string _theme;
        public string Theme
        {
            get { return _theme; }
            set { SetValue(ref _theme, value); }
        }

        private bool _isIncomming;
        public bool IsIncomming
        {
            get { return _isIncomming; }
            set { SetValue(ref _isIncomming, value); }
        }

        private int _inReplyToId;
        public int InReplyToId
        {
            get { return _inReplyToId; }
            set { SetValue(ref _inReplyToId, value); }
        }

        private bool _reply;
        public bool Reply
        {
            get { return _reply; }
            set { SetValue(ref _reply, value); }
        }

        private string _html;
        public string Html
        {
            get { return _html; }
            set
            {
                SetValue(ref _html, value);
                HtmlWithHead = new HtmlWrapper().IncludeHeaderWithUtf8Charset(_html);
            }
        }

        private string _htmlWithHead;
        public string HtmlWithHead
        {
            get { return _htmlWithHead; }
            set { SetValue(ref _htmlWithHead, value); }
        }

        private string _mod;
        public string Mod
        {
            get { return _mod; }
            set { SetValue(ref _mod, value); }
        }

        private DateTime? _tracking;
        public DateTime? Tracking
        {
            get { return _tracking; }
            set { SetValue(ref _tracking, value); }
        }

        private DateTime? _sent;
        public DateTime? Sent
        {
            get { return _sent; }
            set { SetValue(ref _sent, value); }
        }

        /// <summary>
        /// Признак перенаправлено ли сообщение
        /// </summary>
        private bool _forwarded;
        public bool Forwarded
        {
            get { return _forwarded; }
            set { SetValue(ref _forwarded, value); }
        }

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }

        private ObservableCollection<RequestAttachment> _attachments;
        public ObservableCollection<RequestAttachment> Attachments
        {
            get { return _attachments; }
            set { SetValue(ref _attachments, value); }
        }
    }
}