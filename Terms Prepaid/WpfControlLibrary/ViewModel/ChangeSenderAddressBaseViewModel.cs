using GalaSoft.MvvmLight.Command;
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
}