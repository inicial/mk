using NLog;
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
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.Buttons
{
    /// <summary>
    /// Interaction logic for AdditionalServiceButton.xaml
    /// </summary>
    public partial class FlightInfoButton : UserControl, IMenu
    {
        public enum Tags
        {
            Miles = 1,
            Lowcost = 2
        }

        private Dictionary<int, MenuItem> _menuItems;

        public event MenuItemClickEventHandler OnMenuItemClick;

        /*private MilesAirlineListViewModel _milesAirlineListViewModel = new MilesAirlineListViewModel();
        public MilesAirlineListViewModel MilesAirlineListViewModel 
        {
            get { return _milesAirlineListViewModel; }
            set { _milesAirlineListViewModel = value; }
        }*/

        public FlightInfoButton()
        {
            InitializeComponent();
            _menuItems = new Dictionary<int, MenuItem>();

            AddMenuItem(Miles, Tags.Miles);
            AddMenuItem(Lowcost, Tags.Lowcost);

            //Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            WinApiHelper.ExtendGlass(PopupInfo, 0, 50, 0, 50);
            /*HwndSource source = (HwndSource)HwndSource.FromVisual(PopupInfo);
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
            }*/
        }

        private void AddMenuItem(MenuItem menuItem, Tags tag)
        {
            menuItem.Tag = tag;
            _menuItems.Add((int)tag, menuItem);
        }

        public void SetEnabled(int menuIndex, bool state)
        {
            try
            {
                _menuItems[menuIndex].IsEnabled = state;
            }
            catch (Exception e)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Trace(e);
            }
        }

        public void SetVisibility(int menuIndex, bool state)
        {
            try
            {
                _menuItems[menuIndex].Visibility = state ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception e)
            {

            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem) sender;

            if (item != null)
            {
                switch ((Tags)item.Tag)
                {
                    case Tags.Miles:
                        PopupInfo.IsOpen = true;
                        break;

                    case Tags.Lowcost:
                        PopupInfo2.IsOpen = true;
                        break;
                }

                if (OnMenuItemClick != null)
                    OnMenuItemClick(this, Convert.ToInt32(item.Tag));
            }
        }

        private void ClosePopupHandler()
        {
            PopupInfo.IsOpen = false;
            PopupInfo2.IsOpen = false;
        }
    }
}
