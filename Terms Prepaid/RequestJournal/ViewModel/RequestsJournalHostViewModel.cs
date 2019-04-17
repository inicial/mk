using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class RequestsJournalHostViewModel : ViewModelBase
    {
        private IRequestsJournalViewModel _vm;
        public IRequestsJournalViewModel Vm
        {
            get { return _vm; }
            set { SetValue(ref _vm, value); }
        }

        private IView _view;
        public IView View
        {
            get { return _view; }
            set { SetValue(ref _view, value); }
        }

        public RequestsJournalHostViewModel()
        {
            Vm = Repository.GetInstance<IRequestsJournalViewModel>();
            View = Repository.GetInstance<IRequestJournalView>();
            View.DataContext = Vm;
        }
    }
}
