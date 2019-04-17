using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    /// <summary>
    /// ChangeSenderAddressBaseViewModel
    /// </summary>
    public class ChangeSenderAddressBaseViewModel : ViewModelBase
    {
        public RelayCommand OkCommand { get; protected set; }

        private string _senderAddress;
        public string SenderAddress
        {
            get { return _senderAddress; }
            set { SetValue(ref _senderAddress, value); }
        }

        public ChangeSenderAddressBaseViewModel()
        {
            OkCommand = new RelayCommand(SaveChanges);
        }

        public virtual void SaveChanges()
        {

        }
    }

    /// <summary>
    /// ChangeSenderAddressViewModel with using IRequestJournalService
    /// </summary>
    public class ChangeSenderAddressViewModel : ChangeSenderAddressBaseViewModel
    {
        private readonly RequestMessage _message;
        private readonly IRequestJournalService _service;
        
        public ChangeSenderAddressViewModel(RequestMessage message, string newSenderAddress = null)
        {
            _message = message;
            _service = Repository.GetInstance<IRequestJournalService>();
            if (message != null) SenderAddress = newSenderAddress ?? message.SenderAddress;
            
        }

        public override void SaveChanges()
        {
            _service.ChangeSenderAddress(_message.Id, SenderAddress);
        }
    }

    /// <summary>
    /// ChangeSenderAddress2ViewModel with using callback
    /// </summary>
    public class ChangeSenderAddress2ViewModel : ChangeSenderAddressBaseViewModel
    {
        private readonly Action<string> _changeAddressHandler;

        public ChangeSenderAddress2ViewModel(Action<string> changeAddressHandler, string oldSenderAddress, string newSenderAddress = null)
        {
            _changeAddressHandler = changeAddressHandler;
            SenderAddress = newSenderAddress ?? oldSenderAddress;
        }

        public override void SaveChanges()
        {
            if (_changeAddressHandler != null)
                _changeAddressHandler.Invoke(SenderAddress);
        }
    }
}
