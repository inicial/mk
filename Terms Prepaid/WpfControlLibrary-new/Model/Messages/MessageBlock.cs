using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Messages
{
    public class MessageBlock : MessageBase
    {
        private string _header;
        private ObservableCollection<Message> _messages;

        public string Header
        {
            get { return _header; }
            set { SetValue(ref _header, value); }
        }

        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set { SetValue(ref _messages, value); }
        }
    }
}
