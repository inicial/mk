using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Clipboard = System.Windows.Forms.Clipboard;

namespace WpfControlLibrary.Buttons
{
    /// <summary>
    /// Interaction logic for ShowRulesButton.xaml
    /// </summary>
    public partial class ShowRulesButton : PopupWindow
    {
        public ShowRulesButton()
        {
            InitializeComponent();
            CloseButtonEventHandler = CloseButton;
        }

        private void popupOption_Closed(object sender, EventArgs e)
        {
            btnOption.IsChecked = false;
        }

        private void btnOption_Checked(object sender, RoutedEventArgs e)
        {
            popupOption.IsOpen = true;
            popupOption.Closed -= popupOption_Closed;
            popupOption.Closed += popupOption_Closed;
            TbRulers.ScrollToHome();
        }

        private void btnOption_Unchecked(object sender, RoutedEventArgs e)
        {
            popupOption.IsOpen = false;
        }

        /*private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            popupOption.IsOpen = false;
        }*/

        private void CloseButton()
        {
            popupOption.IsOpen = false;
        }

        private void CopyButton_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TbRulers.Text);
        }

    }
}
