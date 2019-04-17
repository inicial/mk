using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.ViewModel
{
    public class ScrollRulesButtonViewModel : Data
    {
        private string _rulers;
        public string Rulers
        {
            get { return _rulers; }
            set { SetValue(ref _rulers, value); }
        }

        public ScrollRulesButtonViewModel(string rulers)
        {
            Rulers = rulers;
        }
    }
}
