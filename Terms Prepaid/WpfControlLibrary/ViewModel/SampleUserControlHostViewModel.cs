using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class SampleUserControlHostViewModel : ViewModelBase
    {
        private ViewModelBase _vm;
        public ViewModelBase Vm
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

        public SampleUserControlHostViewModel(IView view, ViewModelBase vm)
        {
            View = view;
            Vm = vm;
            View.DataContext = Vm;
        }
    }
}
