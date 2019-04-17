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
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for ServiceTypeSelectorView.xaml
    /// </summary>
    public partial class ServiceTypeSelectorView : UserControl
    {
        public ServiceTypeSelectorView()
        {
            InitializeComponent();
        }

        /*private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceTabSelectorViewModel vm = (ServiceTabSelectorViewModel)DataContext;
            vm.OnSelectedItem(((ServiceTabWrapper)TabControl.SelectedItem).ViewModel);
        }*/

        private void TabControl_OnSelected(object sender, RoutedEventArgs e)
        {
            ServiceTabSelectorViewModel vm = (ServiceTabSelectorViewModel)DataContext;
            vm.OnSelectedItem(((ServiceTabWrapper)TabControl.SelectedItem).ViewModel);
        }
    }
}
