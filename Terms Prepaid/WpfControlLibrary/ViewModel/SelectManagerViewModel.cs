using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Common;

namespace WpfControlLibrary.ViewModel
{
    public delegate void SelectManagerHandler(User manager);

    public class SelectManagerViewModel : Data
    {
        private SelectManagerHandler SelectManagerHandler { get; set; }

        public RelayCommand ManagerSelectOkCommand { get; set; }

        private ObservableCollection<User> _managers;
        public ObservableCollection<User> Managers
        {
            get { return _managers; }
            set { SetValue(ref _managers, value); }
        }

        private User _selectedManager;
        public User SelectedManager
        {
            get { return _selectedManager; }
            set { SetValue(ref _selectedManager, value); }
        }

        public SelectManagerViewModel(SelectManagerHandler selectManagerHandler, User[] managers)
        {
            Managers = new ObservableCollection<User>(managers);

            SelectManagerHandler = selectManagerHandler;

            ManagerSelectOkCommand = new RelayCommand(
                () => { if (SelectManagerHandler != null) SelectManagerHandler.Invoke(SelectedManager); });
        }
    }
}
