using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public abstract class CaruselData : Data
    {
        private int _rowIndex;
        public int RowIndex
        {
            get { return _rowIndex; }
            set { SetValue(ref _rowIndex, value); }
        }

        private string _visible;
        public string Visible
        {
            get { return _visible; }
            set { SetValue(ref _visible, value); }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { SetValue(ref _fullName, value); }
        }

        public virtual bool EqualData(CaruselData obj)
        {
            return false;
        }
    }
}
