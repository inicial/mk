using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public abstract class GroupGridItem : CaruselData
    {
        private bool _beginClass;
        public bool BeginClass
        {
            get { return _beginClass; }
            set { SetValue(ref _beginClass, value); }
        }

        private bool _endClass;
        public bool EndClass
        {
            get { return _endClass; }
            set { SetValue(ref _endClass, value); }
        }

        private bool _midClass;
        public bool MidClass
        {
            get { return _midClass; }
            set { SetValue(ref _midClass, value); }
        }

        private int _colorIndex;
        public int ColorIndex
        {
            get { return _colorIndex; }
            set { SetValue(ref _colorIndex, value); }
        }
    }
}
