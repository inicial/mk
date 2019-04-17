using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for CollectingLowcostAirlinesView.xaml
    /// </summary>
    public partial class CollectingLowcostAirlinesView : PopupWindow
    {
        public CollectingLowcostAirlinesView()
        {
            InitializeComponent();
            try
            {
                DataContext = Repository.GetInstance<ICollectingLowcostAirlinesViewModel>();
            }
            catch (Exception e)
            {
                //TpLogger.ErrorWithMessage(e);
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() =>
                Grid.UnselectAll()));
        }

    }
}
