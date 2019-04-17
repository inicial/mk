using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataService;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Buttons
{
    /// <summary>
    /// Interaction logic for ReservationButton.xaml
    /// </summary>
    public partial class ReservationButton : SimpleWpfButton
    {
        private readonly IRequestJournalService _requestJournalService;
        private readonly IUsersService _usersService;
        private readonly IAccessService _accessService;
        private readonly int _userId;
        private readonly string _dgCode;
        private const int _status = 3;   // Забронировано

        public ReservationButton(string dgCode)
        {
            InitializeComponent();
            _dgCode = dgCode;
            _requestJournalService = Repository.GetInstance<IRequestJournalService>();
            _usersService = Repository.GetInstance<IUsersService>();
            _accessService = Repository.GetInstance<IAccessService>();
            _userId = _usersService.GetUsKey();
            Load();
        }

        private void Load()
        {
            /*var itemEnum = _accessService.isSuperViser
                ? _requestJournalService.GetRequestsIdForReservation()
                : _requestJournalService.GetRequestsIdForReservation(_userId);
             */ 
            var itemEnum = _requestJournalService.GetRequestsIdForReservation();

            var items = new ObservableCollection<int>(itemEnum
                    .Select()
                    .Select(r => r.Field<int>("Id")));

            var state = items.Any();
            MainButton.IsEnabled = state;
            MainButton.Visibility = state ? Visibility.Visible : Visibility.Hidden;

            RequestsCbx.ItemsSource = items;
        }

        private void MakeReservation()
        {
            var requestId = RequestsCbx.SelectedItem as int? ?? -1;
            if (requestId == -1)
                return;

            _requestJournalService.MakeReservation(requestId, _dgCode, _userId, DateTime.Now);
            Load();
        }

        protected override void Button_Click(object sender, RoutedEventArgs e)
        {
            popupOption.IsOpen = true;
        }

        private void ButtonOK_OnClick(object sender, RoutedEventArgs e)
        {
            popupOption.IsOpen = false;
            MakeReservation();
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            popupOption.IsOpen = false;
        }
    }
}
