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
using terms_prepaid.WindowsUC;


namespace terms_prepaid.Windows
{
    public partial class WorkWindow : Window
    {
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------

        public WindowFrame WF;

        private WindowTypeEnum WindowType;
        private WindowClassEnum WindowClass;

        //private WorkWindowUC ChildControl;
        private UserControl ChildControl;

        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region WorkWindow()
        //----------------------------------------------------------------------------------------------------
        public WorkWindow()
        {
            WorkWindow_Init(WindowClassEnum.MainWindow);
        }

        //----------------------------------------------------------------------------------------------------
        public WorkWindow(WindowClassEnum iWindowClass = WindowClassEnum.MainWindow)
        {
            WorkWindow_Init(iWindowClass);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WorkWindow()
        //====================================================================================================
        #region WorkWindow_Init()
        //----------------------------------------------------------------------------------------------------
        public void WorkWindow_Init(WindowClassEnum iWindowClass)
        {
            WindowClass = iWindowClass;

            //....................................................................................................
            InitializeComponent();

            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //....................................................................................................
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/ico_w_application.ico"));

            //WF = new WindowFrame(this, WindowIconBorder, WindowTitleBorder, WindowMinimizeButtonBorder, WindowMaximizeButtonBorder, WindowCloseButtonBorder, StatusBarTextBlock, WindowGripBorder);
            WF = new WindowFrame(this);

            //WF.Set_WindowBrush(Colors.WhiteSmoke);

            //....................................................................................................
            WorkWindow_InitUC();

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WorkWindow_Init()
        //====================================================================================================
        #region WorkWindow_InitUC()
        //----------------------------------------------------------------------------------------------------
        public void WorkWindow_InitUC()
        {
            WindowType = WindowTypeEnum.SizableWindow;

            //....................................................................................................
            if (WindowClass == WindowClassEnum.WorkWindow)
            {
                Set_WindowType(WindowTypeEnum.SizableWindow);
                WorkWindowUC work_window_uc = new WorkWindowUC(this);
                this.Set_ChildControl(work_window_uc);
                return;
            }

            //....................................................................................................
            if (WindowClass == WindowClassEnum.MainWindow)
            {
                Set_WindowType(WindowTypeEnum.SizableWindow);
                MainWindowUC main_window_uc = new MainWindowUC(this);

                this.Set_ChildControl(main_window_uc);

                main_window_uc.Accord_ParentWindow();
                return;
            }

            //....................................................................................................
            if (WindowClass == WindowClassEnum.MessageWindow)
            {
                Set_WindowType(WindowTypeEnum.DialogWindow);
                MessageWindowUC message_window_uc = new MessageWindowUC(this);

                this.Set_ChildControl(message_window_uc);

                message_window_uc.Accord_ParentWindow();

                return;
            }

            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WorkWindow_InitUC()
        //----------------------------------------------------------------------------------------------------
        #endregion // Methods
        //====================================================================================================
        #region Window control
        //----------------------------------------------------------------------------------------------------
        #region Set_WindowType()
        //----------------------------------------------------------------------------------------------------
        public void Set_WindowType(WindowTypeEnum window_type)
        {
            if (window_type == WindowType) return;

            WindowType = window_type;

            if (window_type == WindowTypeEnum.SizableWindow)
            {

            }
            if (window_type == WindowTypeEnum.DialogWindow)
            {
                WF.SetResizeFlag(false);
                WF.SetSizeButtons(false, false);
                WF.SetIconFlag(false);
                WF.SetStatusFlag(false);
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_WindowType()
        //====================================================================================================
        #region Set_ChildControl()
        //----------------------------------------------------------------------------------------------------
        public void Set_ChildControl(UserControl iChildControl)
        {
            ChildControl = iChildControl;
            //ChildControl.Set_ParentWindow(this);
            WindowContentControl.Content = ChildControl;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_ChildControl()
        //----------------------------------------------------------------------------------------------------
        #region Get_MessageWindowUC()
        //----------------------------------------------------------------------------------------------------
        public MessageWindowUC Get_MessageWindowUC()
        {
            if (ChildControl == null) return null;
            if (ChildControl.GetType() == typeof(MessageWindowUC))
                return (MessageWindowUC)ChildControl;

            return null;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Get_MessageWindowUC()
        //----------------------------------------------------------------------------------------------------
        #endregion // Window control
        //====================================================================================================
        #region New windows
        //----------------------------------------------------------------------------------------------------
        #region New_WorkWindow()
        //----------------------------------------------------------------------------------------------------
        public static WorkWindow New_WorkWindow()
        {
            WorkWindow wnd = new WorkWindow(WindowClassEnum.WorkWindow);
            //wnd.Set_ChildControl(new WorkWindowUC());

            return wnd;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // New_WorkWindow()
        //----------------------------------------------------------------------------------------------------
        #region New_MessageWindow()
        //----------------------------------------------------------------------------------------------------
        public static WorkWindow New_MessageWindow(string message_text, string title_text, bool yes_flag, bool no_flag, bool cancel_flag, string yes_title = "", string no_title = "", string cancel_title = "")
        {
            WorkWindow wnd = new WorkWindow(WindowClassEnum.MessageWindow);
            if (wnd.ChildControl.GetType() == typeof(MessageWindowUC))
            {
                MessageWindowUC uc = (MessageWindowUC)wnd.ChildControl;
                if (uc != null) uc.Init(message_text, title_text, yes_flag, no_flag, cancel_flag, yes_title, no_title, cancel_title);
            }
            return wnd;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // New_MessageWindow()
        //----------------------------------------------------------------------------------------------------
        #endregion // New windows
        //====================================================================================================
        #region Service
        //----------------------------------------------------------------------------------------------------
        #region ShowMessage
        //----------------------------------------------------------------------------------------------------
        public static void ShowMessage(string message_text)
        {
            WorkWindow wnd = WorkWindow.New_MessageWindow(message_text, "Сообщение", true, false, false, "OK");
            wnd.ShowDialog();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ShowMessage
        //----------------------------------------------------------------------------------------------------
        #region ShowRequest
        //----------------------------------------------------------------------------------------------------
        public bool ShowRequest(string message_text, string title_text = "Запрос")
        {
            WorkWindow wnd = WorkWindow.New_MessageWindow(message_text, title_text, true, true, false);
            wnd.ShowDialog();

            MessageWindowUC uc = wnd.Get_MessageWindowUC();
            if (uc == null) return false;
            if (uc.YesFlag) return true;

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ShowRequest
        //----------------------------------------------------------------------------------------------------
        #region ShowConfirm
        //----------------------------------------------------------------------------------------------------
        public bool ShowConfirm(string message_text, string title_text = "Подтверждение")
        {
            WorkWindow wnd = WorkWindow.New_MessageWindow(message_text, title_text, true, true, true);
            wnd.ShowDialog();

            MessageWindowUC uc = wnd.Get_MessageWindowUC();
            if (uc == null) return false;
            if (uc.YesFlag) return true;

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ShowConfirm
        //----------------------------------------------------------------------------------------------------
        #endregion // Service
        //====================================================================================================
    }
}
