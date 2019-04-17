using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public abstract class ServiceViewModel : ViewModelBase
    {
        public enum Permissions
        {
            Level0 = 0,
            Level1 = 1
        }

        protected CaruselData _service;

        public virtual CaruselData Service
        {
            get { return _service; }
            set
            {
                if (value != null)
                {
                    SetValue(ref _service, value);
                    LoadService();
                }
            }
        }

        public virtual void LoadService()
        {
            
        }
    }
}
