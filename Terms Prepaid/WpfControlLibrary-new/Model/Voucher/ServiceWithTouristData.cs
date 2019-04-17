using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class ServiceWithTouristData : Service
    {
        private ObservableCollection<Turist> _turistList;
        
        public ObservableCollection<Turist> TuristList
        {
            get { return _turistList; }
            set { SetValue(ref _turistList, value); }
        }

        public ServiceWithTouristData(Service source)
            : base(source)
        {
            var db = Repository.GetInstance<TouristDataDataContext>();

            TuristList = new ObservableCollection<Turist>(
                db.TuristServices.Where(s => s.TU_DLKEY == DlKey)
                                 .Select(s => s.Turist));
        }
    }
}
