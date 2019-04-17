using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public abstract class TabAbstractViewModel : ViewModelBase
    {
        private string _tabName;
        public string TabName
        {
            get { return _tabName; }
            set { SetValue(ref _tabName, value); }
        }

        private int _typeId;
        public int TypeId
        {
            get { return _typeId; }
            set { SetValue(ref _typeId, value); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { SetValue(ref _isEmpty, value); }
        }
    }
}
