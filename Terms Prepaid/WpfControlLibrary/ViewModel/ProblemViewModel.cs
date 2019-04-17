using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.ViewModel
{
    public class ProblemViewModel : ServiceViewModel
    {
        private string _caption;

        public string Caption
        {
            get { return _caption; }
            set { SetValue(ref _caption, value); }
        }

        public override CaruselData Service
        {
            get { return _service; }
            set
            {
                SetValue(ref _service, value);
                LoadService();
            }
        }

        public sealed override void LoadService()
        {
            Caption = Service != null ? Service.FullName : "";
        }
    }
}
