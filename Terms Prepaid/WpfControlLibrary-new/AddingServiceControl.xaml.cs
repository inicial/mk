using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary.Common;

namespace WpfControlLibrary
{

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AddingServiceControl : UserControl, IButton
    {
        //public delegate void MyControlEventHandler(object sender, MyControlEventArgs args);
        //public event MyControlEventHandler OnButtonClick;

        public event MyControlEventHandlerSample OnButtonClick;
        public bool IsOpen { get { return popupOption.IsOpen; } }

        public AddingServiceControl()
        {
            InitializeComponent();
            popupOption.IsOpen = true;
        }

        private void btnOption_Checked(object sender, RoutedEventArgs e)
        {
            popupOption2.IsOpen = true;
            popupOption2.Closed += (senderClosed, eClosed) => { btnOption.IsChecked = false; };
        }

        private void btnOption_Unchecked(object sender, RoutedEventArgs e)
        {
            popupOption2.IsOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnOption.IsChecked = false;
            if (OnButtonClick != null)
                OnButtonClick(this);
        }

        public void Update()
        {
            Hide();
            Show();
        }

        public void Hide()
        {
            popupOption.IsOpen = false;
        }

        public void Show()
        {
            popupOption.IsOpen = true;
        }
    }
}
