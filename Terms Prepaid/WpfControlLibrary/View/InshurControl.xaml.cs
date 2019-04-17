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


namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for InshurControl.xaml
    /// </summary>
    public partial class InshurControl : UserControl, IInshurControl
    {
        public event MyControlEventHandlerSample OnButtonClick;

        public int Selected = 0;

        public InshurControl()
        {
            InitializeComponent();
        }

        public void ScrollToTop()
        {
            //throw new NotImplementedException();
        }

        private void ButtonAnnulate_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            comboBox.SelectedIndex = 0;
        }
        /*
        private void popupAnnulate_Closed(object sender, EventArgs e)
        {
            btnAnnulate.IsChecked = false;
        }

        private void btnAnnulate_Checked(object sender, RoutedEventArgs e)
        {
            popupAnnulate.IsOpen = true;
            popupAnnulate.Closed -= popupAnnulate_Closed;
            popupAnnulate.Closed += popupAnnulate_Closed;
        }

        private void btnAnnulate_Unchecked(object sender, RoutedEventArgs e)
        {
            popupAnnulate.IsOpen = false;
        }
        */
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
        //    btnAnnulate.IsChecked = false;
        //    Selected = 1;
        //    if (OnButtonClick != null)
        //        OnButtonClick(this);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
        //    btnAnnulate.IsChecked = false;
        //    Selected = 2;
        //    if (OnButtonClick != null)
        //        OnButtonClick(this);
        }
    }
}
