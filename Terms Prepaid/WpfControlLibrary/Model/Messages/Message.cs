using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.Model.Messages
{
    public class Message : MessageBase
    {
        public enum Rolemember
        {
            Manager = 1,
            Superviser = 2,
            Bronir = 3,
            Client = 4,
            Other = 0
        }

        private string _autor;
        private string _text;
        private string _html;
        private string _mod;
        private Rolemember _role;

        public string Autor
        {
            get { return _autor; }
            set { SetValue(ref _autor, value); }
        }

        public string Text
        {
            get { return _text; }
            set { SetValue(ref _text, value); }
        }

        public string Html
        {
            get { return _html; }
            set { SetValue(ref _html, value); }
        }

        public string Mod
        {
            get { return _mod; }
            set { SetValue(ref _mod, value); }
        }

        public Rolemember Role
        {
            get { return _role; }
            set { SetValue(ref _role, value); }
        }

        private ObservableCollection<RequestAttachment> _attachments;

        public ObservableCollection<RequestAttachment> Attachments
        {
            get { return _attachments; }
            set { SetValue(ref _attachments, value); }
        }
    }
}
