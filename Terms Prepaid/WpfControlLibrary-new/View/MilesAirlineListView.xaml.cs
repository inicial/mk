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
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for MilesAirlineListView.xaml
    /// </summary>
    public partial class MilesAirlineListView : PopupWindow
    {

        public MilesAirlineListView()
        {
            InitializeComponent();
            try
            {
                DataContext = Repository.GetInstance<IMilesAirlineListViewModel>();
            }
            catch (Exception e)
            {
                //TpLogger.ErrorWithMessage(e);
            }
        }
    }
}
