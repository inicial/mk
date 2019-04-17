using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Common
{
    public class PopupWindow : UserControl
    {
        public delegate void CloseEventHandler();
        public CloseEventHandler CloseButtonEventHandler { get; set; }

        public RelayCommand CloseButtonCommand { get; private set; }

        public PopupWindow()
        {
            CloseButtonCommand = new RelayCommand(() =>
            {
                if (CloseButtonEventHandler != null)
                    CloseButtonEventHandler.Invoke();
            });
            //Background = new SolidColorBrush(Colors.Transparent);

            //Loaded += PopupWindow_Loaded;
        }

        void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource source = (HwndSource)HwndSource.FromVisual(this);
            IntPtr mainWindowPtr = source.Handle;
            HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
            mainWindowSrc.CompositionTarget.BackgroundColor = Colors.Transparent;

            this.Background = Brushes.Transparent;

            // Set the proper margins for the extended glass part
            MARGINS margins = new MARGINS();
            margins.cxLeftWidth = -1;
            margins.cxRightWidth = -1;
            margins.cyTopHeight = -1;
            margins.cyBottomHeight = -1;

            int result = WinApiHelper.DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);

            if (result < 0)
            {
                MessageBox.Show("An error occured while extending the glass unit.");
            }
        }

        protected void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CloseButtonEventHandler != null)
                CloseButtonEventHandler.Invoke();
        }
    }
}
