using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Media;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;
using WpfControlLibrary.View;

namespace WpfControlLibrary.ViewModel
{
    public interface IRequestNewMessageViewModelBase
    {
        bool ValidateSenderAddress();
    }

    public class RequestNewMessageViewModel : CorrespondenceRequestViewModel, IRequestNewMessageViewModelBase
    {
        private static class DestinationType
        {
            public const int AddingNewUser = -2;
            public const int UnknownUser = -1;
        }

        private string _html;
        public string Html
        {
            get { return _html; }
            set
            {
                SetValue(ref _html, value);
            }
        }

        private string _from;
        public string From
        {
            get { return _from; }
            set { SetValue(ref _from, value); }
        }

        private string _to;
        public string To
        {
            get { return _to; }
            set { SetValue(ref _to, value); }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { SetValue(ref _subject, value); }
        }

        private ObservableCollection<User> _destinations;
        public ObservableCollection<User> Destinations
        {
            get { return _destinations; }
            set { SetValue(ref _destinations, value); }
        }

        private ObservableCollection<User> _clients;
        public ObservableCollection<User> Clients
        {
            get { return _clients; }
            set { SetValue(ref _clients, value); }
        }

        public override User SelectedManager
        {
            get { return _selectedManager; }
            set
            {
                if (_adding)
                    return;

                if (value != null && value.Key == DestinationType.AddingNewUser)
                    ChangeSenderAddress(_selectedManager != null ? _selectedManager.Email : null);
                else
                    SetValue(ref _selectedManager, value);
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetValue(ref _selectedIndex, value); }
        }

        private bool _adding;

        public new RelayCommand SendCommand { get; set; }
        public RelayCommand SelectedCmd { get; private set; }

        private readonly IRequestJournalService _requestJournalService;

        private bool _commentMode;
        public bool CommentMode
        {
            get { return _commentMode; }
            set { SetValue(ref _commentMode, value); }
        }

        public RequestNewMessageViewModel(User user, Request request, CorrespondenceType type, Action updateCallback, Action closeCorrespondenceCallback, IRequestJournalService service = null, IRequestJournalSender sender = null) :
            base(user, request, type, updateCallback, closeCorrespondenceCallback, service, sender)
        {
            SelectedCmd = new RelayCommand(Selected, () => _adding);
            _requestJournalService = Repository.GetInstance<IRequestJournalService>();
        }

        public bool ValidateSenderAddress()
        {
            if (_type == CorrespondenceType.Manager)
                return true;

            var oldSenderAddress = _selectedManager != null ? _selectedManager.Email : null;

            if (oldSenderAddress != null && oldSenderAddress.IndexOf("mcruises.ru", StringComparison.OrdinalIgnoreCase) == -1)
                return true;

            /*var path = string.Format("{0}/WpfControlLibrary;component/Resources/ButtonResources.xaml", Environment.CurrentDirectory.Replace(":\\", "://").Replace("\\","/"));
            var app = Application.Current ?? new Application();
            app.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(path) });*/

            var dialog = new DialogView("Проверка корректности адреса", 
                string.Format("Проверьте корректность адреса, по которому вы отравляете письмо: {0}", oldSenderAddress),
                DialogView.Buttons.YesNo)
            {
                YesButtonText = "Отправить",
                NoButtonText = "Изменить адрес",
                ImageSource = "img/attention.png",
                ImageWidth = 64,
                ImageHeight = 64,
                Width = 300,
                Height = 200
            };

            dialog.ShowDialog();

            if (dialog.Result)
                return true;
            
            /*if (Repository.GetInstance<IMessageBoxService>()
                .ShowMessage(string.Format("Проверьте корректность адреса, по которому вы отравляете письмо: {0}", oldSenderAddress),
                    "Проверка корректности адреса", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning)) return true;*/
            
            ChangeSenderAddress(null);
            return false;
        }

        public void ChangeSenderAddress(string newSenderAddress)
        {
            var oldSenderAddress = _selectedManager != null ? _selectedManager.Email : null;

            Repository.GetInstance<IWindowsHelper>().ShowWindow(new ChangeSenderAddress2ViewModel(ChangeSenderAddressHandler, oldSenderAddress, newSenderAddress),
                Repository.GetInstance<IChangeSenderAddressView>(), true, true);
        }

        private void ChangeSenderAddressHandler(string newAddress)
        {
            //SelectedManager = Destinations.FirstOrDefault();
            _adding = true;
            Destinations.Insert(Destinations.Count - 1, new User(DestinationType.UnknownUser, newAddress, newAddress, null, null, null, null, null));

            if (Request.FirstMessage != null)
                _requestJournalService.ChangeSenderAddress(Request.FirstMessage.Id, newAddress);
        }

        private void Selected()
        {
            //SelectedIndex = 0;
            _adding = false;
            //SelectedManager = Destinations.FirstOrDefault();
        }

        public override void UpdateCorrespondence()
        {
            FillDestinations();
            From = new MyWorkMail().Address;
            Subject = RequestMessageBuilder.GetTheme(Request.Number);
        }

        private void FillDestinations()
        {
            var selectedAddress = SelectedManager != null ? SelectedManager.Email : null;
            
            switch (_type)
            {
                case CorrespondenceType.Client:
                    Clients = new ObservableCollection<User>(Request.Messages.Select(m => m.IsIncomming ? m.SenderAddress : m.DestinationAddress).Distinct().Select(GetClient));
                    Destinations = Clients;
                    break;
                case CorrespondenceType.Manager:
                    Destinations = new ObservableCollection<User>(Managers)
                    {
                        new User(-1, "visa", "visa@mcruises.ru", null, null, null, null, null)
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException("CorrespondenceType", _type, null);
            }

            Destinations.Add(new User(DestinationType.AddingNewUser, string.Empty, string.Empty, null, null, null, null, null));

            if (selectedAddress != null && Destinations != null)
                SelectedManager = Destinations.FirstOrDefault(m => (m.Email ?? "").Equals(selectedAddress));
        }

        private User GetClient(string address)
        {
            return new User(DestinationType.UnknownUser, address, address, null, null, null, null, null);
        }

        protected override void Send(string message)
        {
            if (SelectedManager == null)return;

            if (Request == null || Request.FirstMessage == null) throw new Exception("Ошибка ответа на запрос: сообщение запроса отсутствует");

            var from = From;
            var to = CommentMode ? User.Email : SelectedManager.Email;
            var subject = Subject;
            var mod = CommentMode ? RequestMessageMod.COM : GetMod(_type);
            var replyTo = Request.Messages.LastOrDefault(m => m.Mod.Equals(mod, StringComparison.OrdinalIgnoreCase));
            var msg = RequestMessageBuilder.GetRequestMessageHtml(User, from, to, subject, Request, replyTo, message, mod, Attachments.Select(a => a.Attachment));

            if(CommentMode)
                Comment(msg);
            else
                Send(msg);
        }

        private bool GetSendCommandEnabled()
        {
            return SelectedManager != null;
        }

        private string GetMod(CorrespondenceType correspType)
        {
            return correspType == CorrespondenceType.Manager ? RequestMessageMod.MTM : correspType == CorrespondenceType.Client ? RequestMessageMod.MTC : "";
        }
    }
}
