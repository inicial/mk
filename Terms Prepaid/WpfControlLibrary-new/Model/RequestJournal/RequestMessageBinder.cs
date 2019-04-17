using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WpfControlLibrary.Common;
using WpfControlLibrary.Messages;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Класс привязки запроса к уще существующему
    /// </summary>
    public class RequestMessageBinder : Data, IRequestMessageBinder
    {
        public int Id { get; set; }

        private bool _requestsIdVisibility;
        public bool RequestsIdVisibility
        {
            get { return _requestsIdVisibility; }
            set { SetValue(ref _requestsIdVisibility, value); }
        }

        private int _selectedId;
        public int SelectedId
        {
            get { return _selectedId; }
            set
            {
                SetValue(ref _selectedId, value);
                Messenger.Default.Send(new BindRequestsMessage{ChildId = Id, ParentId = value});
            }
        }

        public RelayCommand ShowRequestsIdCommand { get; set; }
        public RelayCommand BindMessageCommand { get; set; }

        public RequestMessageBinder(int id)
        {
            Id = id;
            ShowRequestsIdCommand = new RelayCommand(() => RequestsIdVisibility = !RequestsIdVisibility);
        }
    }
}