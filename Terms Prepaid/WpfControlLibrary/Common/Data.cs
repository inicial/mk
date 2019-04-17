using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ActiveSharp.PropertyMapping;

namespace WpfControlLibrary.Common
{
    public abstract class Data : INotifyPropertyChanged
    {
        readonly PropertyChangeHelper _propertyChangeHelper = new PropertyChangeHelper();

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChangeHelper.Add(value); }
            remove { _propertyChangeHelper.Remove(value); }
        }

        protected void SetValue<T>(ref T field, T value)
        {
            _propertyChangeHelper.SetValue(this, ref field, value);
        }
    }
}
