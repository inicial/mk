using System;

namespace WpfControlLibrary.ViewModel
{
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