using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.View
{
    public class ServiceTabWrapper : ViewModelBase
    {
        private IView _view;
        public IView View
        {
            get { return _view; }
            set { SetValue(ref _view, value); }
        }

        private TabAbstractViewModel _viewModel;
        public TabAbstractViewModel ViewModel
        {
            get { return _viewModel; }
            set { SetValue(ref _viewModel, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        private int _typeId;
        public int TypeId
        {
            get { return _typeId; }
            set { SetValue(ref _typeId, value); }
        }
        
        public ServiceTabWrapper(IView view, TabAbstractViewModel viewModel, string name)
        {
            View = view;
            ViewModel = viewModel;
            Name = name;
            View.DataContext = viewModel;
        }
    }
}
