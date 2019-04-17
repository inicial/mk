using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestMessageInfo : Data
    {
        private string _senderAddress;
        public string SenderAddress
        {
            get { return _senderAddress; }
            set { SetValue(ref _senderAddress, value); }
        }

        private bool _isIncomming;
        public bool IsIncomming
        {
            get { return _isIncomming; }
            set { SetValue(ref _isIncomming, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetValue(ref _date, value); }
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

        private DateTime? _readDate;
        public DateTime? ReadDate
        {
            get { return _readDate; }
            set { SetValue(ref _readDate, value); }
        }

        private string _mod;
        public string Mod
        {
            get { return _mod; }
            set { SetValue(ref _mod, value); }
        }
    }
}
