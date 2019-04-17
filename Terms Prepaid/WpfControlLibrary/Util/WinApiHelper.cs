using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Util
{
    [StructLayout(LayoutKind.Sequential)]
    public class MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    public static class WinApiHelper
    {
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        public static MARGINS GetDpiAdjustedMargins(IntPtr windowHandle, int left, int right, int top, int bottom)
        {
            // Получить Dpi системы
            System.Drawing.Graphics desktop = System.Drawing.Graphics.FromHwnd(windowHandle);
            float DesktopDpiX = desktop.DpiX;
            float DesktopDpiY = desktop.DpiY;

            // Установка полей
            MARGINS margins = new MARGINS();

            margins.cxLeftWidth = Convert.ToInt32(left * (DesktopDpiX / 96));
            margins.cxRightWidth = Convert.ToInt32(right * (DesktopDpiX / 96));
            margins.cyTopHeight = Convert.ToInt32(top * (DesktopDpiX / 96));
            margins.cyBottomHeight = Convert.ToInt32(right * (DesktopDpiX / 96));

            return margins;
        }

        /*public static void ExtendGlass(Window win, int left, int right, int top, int bottom)
        {
            // Получение Win32-дескриптора для окна WPF
            WindowInteropHelper windowInterop = new WindowInteropHelper(win);
            IntPtr handle = windowInterop.Handle;
            ExtendGlass(handle, left, right, top, bottom);
        }*/

        public static void ExtendGlass(FrameworkElement control, int left, int right, int top, int bottom)
        {
            HwndSource source = (HwndSource)HwndSource.FromVisual(control);
            IntPtr handle = source.Handle;
            ExtendGlass(handle, left, right, top, bottom);
        }

        private static void ExtendGlass(IntPtr windowHandle, int left, int right, int top, int bottom)
        {
            HwndSource mainWindowSrc = HwndSource.FromHwnd(windowHandle);
            mainWindowSrc.CompositionTarget.BackgroundColor = Colors.Transparent;

            MARGINS margins = new MARGINS(){cxLeftWidth = left, cxRightWidth = right, cyTopHeight = top, cyBottomHeight = bottom};
            //MARGINS margins = GetDpiAdjustedMargins(windowHandle, left, right, top, bottom);

            int returnVal = DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);

            if (returnVal < 0)
            {
                throw new NotSupportedException("Operation failed.");
            }
        }
    }


}
