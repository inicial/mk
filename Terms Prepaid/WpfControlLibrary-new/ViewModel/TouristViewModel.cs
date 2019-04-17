using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.Tourist;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class TouristViewModel : ViewModelBase
    {
        private ObservableCollection<Tourist> _touristList;
        public ObservableCollection<Tourist> TouristList
        {
            get { return _touristList; }
            set { SetValue(ref _touristList, value); }
        }

        public TouristViewModel()
        {

        }
    }
}
