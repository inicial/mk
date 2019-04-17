using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.ViewModel
{
    public interface IRequestMessagesViewModel
    {
        bool Active { get; set; }
        Request Request { get; }
        ObservableCollection<RequestMessageViewModel> Messages { get; set; }
        RequestMessageViewModel SelectedItem { get; set; }
        void Update(Request request);

        RelayCommand CloseButtonCommand { get; }
        RelayCommand CloseCorrespondenceCmd { get; }
    }
}