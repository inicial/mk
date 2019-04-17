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
using terms_prepaid.Windows;


namespace terms_prepaid.WindowsUC
{
    public partial class WorkWindowUC : UserControl
    {
        //====================================================================================================
        public WorkWindow ParentWindow;


        //====================================================================================================
        public WorkWindowUC(WorkWindow iWorkWindow = null)
        {
            InitializeComponent();

            ParentWindow = iWorkWindow;
        }

        //====================================================================================================
        public WorkWindow Wnd
        {
            get { return ParentWindow; }
        }

        //====================================================================================================
        public void Set_ParentWindow(WorkWindow iParentWindow)
        {
            ParentWindow = iParentWindow;
        }

        //====================================================================================================
        private void Toolbar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
        }

        //====================================================================================================
    }
}
