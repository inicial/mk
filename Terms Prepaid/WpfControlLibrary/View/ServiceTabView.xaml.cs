﻿using System;
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
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for ServiceTabView.xaml
    /// </summary>
    public partial class ServiceTabView : UserControl, IServiceTabView
    {
        public ServiceTabView()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void BtnOption_OnClick(object sender, RoutedEventArgs e)
        {
            LbFlightsBack.SelectedItem = (CaruselData)((Button)sender).Tag;
        }
    }
}
