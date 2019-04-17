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
using System.Windows.Threading;

namespace WpfControlLibrary.View
{
    public delegate void MouseRightButtonCallback();

    public partial class VoucherView : UserControl
    {

        public bool AccessPartnerFlag = false;

        MouseRightButtonCallback MouseTaskCallback;


        public VoucherView(bool iAccessFlag, MouseRightButtonCallback iMouseTaskCallback)
        {
            AccessPartnerFlag = iAccessFlag;
            MouseTaskCallback = iMouseTaskCallback;

            InitializeComponent();

            if (!AccessPartnerFlag)
            {
                DataGridColumn n_column = Grid.Columns[5];
                DataGridColumn p_column = Grid.Columns[7];
                DataGridColumn s_column = Grid.Columns[8];
                if (n_column != null) n_column.Visibility = System.Windows.Visibility.Collapsed;
                if (p_column != null) p_column.Visibility = System.Windows.Visibility.Collapsed;
                if (s_column != null) s_column.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() =>
                Grid.UnselectAll()));
        }

        private void Grid_MouseRightButtonUp(object sender, MouseEventArgs e)
        {
            if (MouseTaskCallback == null) return;

            MouseTaskCallback();
        }

    }
}
