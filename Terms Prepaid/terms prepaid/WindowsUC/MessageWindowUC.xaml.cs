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
    public partial class MessageWindowUC : UserControl
    {
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------
        public WorkWindow ParentWindow;

        private string InitWindowTitle = "Message window";
        private int InitWindowWidth = 600;
        private int InitWindowHeight = 200;

        //....................................................................................................
        public System.Windows.Forms.DialogResult Result;
        public bool YesFlag;
        public bool NoFlag;
        public bool CancelFlag;

        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region MessageWindowUC()
        //----------------------------------------------------------------------------------------------------
        public MessageWindowUC(WorkWindow iWorkWindow = null)
        {
            InitializeComponent();

            Set_ParentWindow(iWorkWindow);

            //....................................................................................................
            //this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/ico_application.ico"));


            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // MessageWindowUC()
        //----------------------------------------------------------------------------------------------------
        #region Init()
        //----------------------------------------------------------------------------------------------------
        public void Init(string message_text, string title_text, bool yes_flag, bool no_flag, bool cancel_flag, string yes_title = "", string no_title = "", string cancel_title = "")
        {
            if (WF != null) WF.Set_WindowTitle(title_text);

            if (!string.IsNullOrEmpty(yes_title)) tblYes.Text = yes_title;
            if (!string.IsNullOrEmpty(no_title)) tblNo.Text = no_title;
            if (!string.IsNullOrEmpty(cancel_title)) tblCancel.Text = cancel_title;

            if (!yes_flag) btnYes.Visibility = System.Windows.Visibility.Collapsed;
            if (!no_flag) btnNo.Visibility = System.Windows.Visibility.Collapsed;
            if (!cancel_flag) btnCancel.Visibility = System.Windows.Visibility.Collapsed;

            if (yes_flag && !no_flag)
            {
                btnYes.IsDefault = true;
                if (!cancel_flag) btnYes.IsCancel = true;
            }
            if (no_flag && !cancel_flag) btnNo.IsCancel = true;
            if (cancel_flag) btnCancel.IsCancel = true;

            tblMessage.Text = message_text;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // Init()
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
        private WindowFrame WF
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
        #region Accord_ParentWindow()
        //----------------------------------------------------------------------------------------------------
        public void Accord_ParentWindow()
        {
            if (ParentWindow == null) return;
            if (WF == null) return;

            WF.Set_WindowSize(InitWindowWidth, InitWindowHeight);
            WF.Set_WindowTitle(InitWindowTitle);

            //WF.Set_WindowBrush(Colors.LightSkyBlue);
            WF.Set_WindowBrush(Color.FromArgb(255, 200, 235, 255));
            WindowGrid.Background = new SolidColorBrush(Color.FromArgb(255, 240, 248, 255));
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
        #region Button_Click()
        //----------------------------------------------------------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Button_Click()
        //----------------------------------------------------------------------------------------------------
        #region btnYes_Click()
        //----------------------------------------------------------------------------------------------------
        public void btnYes_Click(object sender, EventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.Yes;
            YesFlag = true;

            WF.Close_Window();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // btnYes_Click()
        //----------------------------------------------------------------------------------------------------
        #region btnNo_Click()
        //----------------------------------------------------------------------------------------------------
        public void btnNo_Click(object sender, EventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.No;
            NoFlag = true;

            WF.Close_Window();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // btnNo_Click()
        //----------------------------------------------------------------------------------------------------
        #region btnCancel_Click()
        //----------------------------------------------------------------------------------------------------
        public void btnCancel_Click(object sender, EventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.Cancel;
            CancelFlag = true;

            WF.Close_Window();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // btnCancel_Click()
        //----------------------------------------------------------------------------------------------------
        #endregion // Controls events
        //====================================================================================================    
    }
}
