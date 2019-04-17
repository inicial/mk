using System;
using System.Collections.Generic;
using System.Linq;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
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
}