using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary.Common;
using Application = System.Windows.Application;
using UserControl = System.Windows.Controls.UserControl;

namespace WpfControlLibrary
{
    public partial class BackButton : UserControl, IButton
    {
        public event MyControlEventHandlerSample OnButtonClick;

        public BackButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OnButtonClick != null)
                OnButtonClick(this);
        }

    }
}
