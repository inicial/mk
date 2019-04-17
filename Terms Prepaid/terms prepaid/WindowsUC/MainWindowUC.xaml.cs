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
    public partial class MainWindowUC : UserControl
    {
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------
        public WorkWindow ParentWindow;

        private string InitWindowTitle = "Work application";
        private int InitWindowWidth = 1100;
        private int InitWindowHeight = 650;
        private int MinWindowWidth = 1000;
        private int MinWindowHeight = 600;
        private int MaxWindowWidth = 1600;
        private int MaxWindowHeight = 1000;

        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region MainWindowUC()
        //----------------------------------------------------------------------------------------------------
        public MainWindowUC(WorkWindow iWorkWindow = null)
        {
            InitializeComponent();

            Set_ParentWindow(iWorkWindow);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // MainWindowUC()
        //----------------------------------------------------------------------------------------------------
        #endregion // Methods
        //====================================================================================================
        #region Window control
        //----------------------------------------------------------------------------------------------------
        #region Wnd
        //----------------------------------------------------------------------------------------------------
        public WorkWindow Wnd
        {
            get { return ParentWindow; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Wnd
        //----------------------------------------------------------------------------------------------------
        #region WF
        //----------------------------------------------------------------------------------------------------
        public WindowFrame WF
        {
            get
            {
                if (ParentWindow == null) return null;
                return ParentWindow.WF;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WF
        //----------------------------------------------------------------------------------------------------
        #region Set_ParentWindow()
        //----------------------------------------------------------------------------------------------------
        public void Set_ParentWindow(WorkWindow iParentWindow)
        {
            ParentWindow = iParentWindow;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_ParentWindow()
        //----------------------------------------------------------------------------------------------------
        #region Set_WindowBrush()
        //----------------------------------------------------------------------------------------------------
        public void Set_WindowBrush(Color brush_color)
        {
            WF.Set_WindowBrush(brush_color);

            SolidColorBrush brush = (SolidColorBrush)this.TryFindResource("WindowBorderBrush");
            if (brush != null) this.Resources["WindowBorderBrush"] = new SolidColorBrush(brush_color);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_WindowBrush()
        //----------------------------------------------------------------------------------------------------
        #region Accord_ParentWindow()
        //----------------------------------------------------------------------------------------------------
        public void Accord_ParentWindow()
        {
            if (ParentWindow == null) return;
            if (WF == null) return;

            WF.Set_WindowSize(InitWindowWidth, InitWindowHeight);
            WF.Set_WindowMinimumSize(MinWindowWidth, MinWindowHeight);
            WF.Set_WindowMaximumSize(MaxWindowWidth, MaxWindowHeight);
            WF.Set_WindowTitle(InitWindowTitle);

            //Color window_color = Colors.LightSteelBlue;
            //Color window_color = Color.FromArgb(255, 239, 239, 242);
            //Set_WindowBrush(window_color);

            //....................................................................................................
            //Color wc = window_color;
            //Color tab_color = Color.FromArgb(255, (byte)(wc.R + 10), (byte)(wc.G + 10), (byte)(wc.B + 10));
            //SolidColorBrush brush = (SolidColorBrush)this.TryFindResource("MainTabBrush");
            //if (brush != null) this.Resources["MainTabBrush"] = new SolidColorBrush(tab_color);

            //Color item_color = Color.FromArgb(255, (byte)(wc.R - 10), (byte)(wc.G - 10), (byte)(wc.B - 10));
            //brush = (SolidColorBrush)this.TryFindResource("MainTabUnselectedBrush");
            //if (brush != null) this.Resources["MainTabUnselectedBrush"] = new SolidColorBrush(item_color);

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Accord_ParentWindow()
        //----------------------------------------------------------------------------------------------------
        #region Close_Window()
        //----------------------------------------------------------------------------------------------------
        private void Close_Window()
        {
            if (WF == null) return;

            WF.Close_Window();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Close_Window()
        //----------------------------------------------------------------------------------------------------
        #endregion // Window control
        //====================================================================================================
        #region Controls events
        //----------------------------------------------------------------------------------------------------
        #region Toolbar_Loaded()
        //----------------------------------------------------------------------------------------------------
        private void Toolbar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Toolbar_Loaded()
        //----------------------------------------------------------------------------------------------------
        #endregion // Controls events
        //====================================================================================================
    }
}
