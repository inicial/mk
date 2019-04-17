using System.Collections.ObjectModel;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Цепочка сообщений
    /// </summary>
    public class RequestMessageThread : Data
    {
        private RequestMessage _message;
        public RequestMessage Message
        {
            get { return _message; }
            set { SetValue(ref _message, value); }
        }

        private ObservableCollection<RequestMessageThread> _threads;
        public ObservableCollection<RequestMessageThread> Threads
        {
            get { return _threads; }
            set { SetValue(ref _threads, value); }
        }

        public RequestMessageThread()
        {
            Threads = new ObservableCollection<RequestMessageThread>();
        }
    }
}