using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace terms_prepaid.Windows
{
    public class WindowFrame
    {
        //====================================================================================================
        #region user32.dll
        //----------------------------------------------------------------------------------------------------

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        //----------------------------------------------------------------------------------------------------
        #endregion // user32.dll
        //====================================================================================================
        #region Variables
        //----------------------------------------------------------------------------------------------------

        private System.Windows.Window WindowObject;
        private IntPtr WindowHandle;

        //....................................................................................................
        private System.Windows.Controls.Border WindowIcon;
        private System.Windows.Controls.Border WindowTitle;
        private System.Windows.Controls.TextBlock WindowTitleText;
        private System.Windows.Controls.Border WindowMinimizeButton;
        private System.Windows.Controls.Border WindowMaximizeButton;
        private System.Windows.Controls.Border WindowCloseButton;
        private System.Windows.Controls.Border WindowStatusBar;
        private System.Windows.Controls.TextBlock WindowStatusText;
        private System.Windows.Controls.Border WindowGrip;

        private System.Windows.Controls.Border BorderTopLeft;
        private System.Windows.Controls.Border BorderTop;
        private System.Windows.Controls.Border BorderTopRight;
        private System.Windows.Controls.Border BorderLeft;
        private System.Windows.Controls.Border BorderRight;
        private System.Windows.Controls.Border BorderBottomLeft;
        private System.Windows.Controls.Border BorderBottom;
        private System.Windows.Controls.Border BorderBottomRight;

        private System.Windows.Controls.Image WindowMinimizeButtonImage;
        private System.Windows.Controls.Image WindowMaximizeButtonImage;
        private System.Windows.Controls.Image WindowCloseButtonImage;

        //....................................................................................................
        private bool Resize_Flag;

        private double FormHoldLeft;
        private double FormHoldTop;
        private double FormHoldWidth;
        private double FormHoldHeight;

        private double MouseHoldX;
        private double MouseHoldY;

        private bool Title_HoldFlag;
        private bool Grip_HoldFlag;

        private bool BorderTopLeft_HoldFlag;
        private bool BorderTop_HoldFlag;
        private bool BorderTopRight_HoldFlag;
        private bool BorderLeft_HoldFlag;
        private bool BorderRight_HoldFlag;
        private bool BorderBottomLeft_HoldFlag;
        private bool BorderBottom_HoldFlag;
        private bool BorderBottomRight_HoldFlag;

        private bool MinimizeButtonOver;
        private bool MaximizeButtonOver;
        private bool CloseButtonOver;
        private bool MinimizeButtonPressed;
        private bool MaximizeButtonPressed;
        private bool CloseButtonPressed;

        //....................................................................................................
        private System.Windows.Window FW
        {
            get { return WindowObject; }
            set { WindowObject = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Variables
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region WindowFrame()
        //----------------------------------------------------------------------------------------------------
        public WindowFrame(System.Windows.Window iWindowObject)
        {
            WindowObject = iWindowObject;
            WindowHandle = new WindowInteropHelper(WindowObject).Handle;

            Resize_Flag = true;

            InitControls();
        }

        //----------------------------------------------------------------------------------------------------
        public WindowFrame(System.Windows.Window iWindowObject, Border iWindowIcom = null, Border iWindowTitle = null, Border iWindowMinButton = null, Border iWindowMaxButton = null, Border iWindowCloseButton = null, TextBlock iStatusBar = null, Border iWindowGrip = null)
        {
            WindowObject = iWindowObject;
            WindowHandle = new WindowInteropHelper(WindowObject).Handle;

            WindowIcon = iWindowIcom;
            WindowTitle = iWindowTitle;
            WindowMinimizeButton = iWindowMinButton;
            WindowMaximizeButton = iWindowMaxButton;
            WindowCloseButton = iWindowCloseButton;
            WindowStatusText = iStatusBar;
            WindowGrip = iWindowGrip;

            Resize_Flag = true;

            InitControls();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WindowFrame()
        //----------------------------------------------------------------------------------------------------
        #region InitControls()
        //----------------------------------------------------------------------------------------------------
        private void InitControls()
        {
            if (FW == null) return;

            //....................................................................................................
            // assign controls variables
            if (WindowIcon == null)
                WindowIcon = (Border)FW.FindName("WindowIconBorder");
            if (WindowTitle == null)
                WindowTitle = (Border)FW.FindName("WindowTitleBorder");
            if (WindowTitleText == null)
                WindowTitleText = (TextBlock)FW.FindName("WindowTitleTextBlock");

            if (WindowMinimizeButton == null)
                WindowMinimizeButton = (Border)FW.FindName("WindowMinimizeButtonBorder");
            if (WindowMaximizeButton == null)
                WindowMaximizeButton = (Border)FW.FindName("WindowMaximizeButtonBorder");
            if (WindowCloseButton == null)
                WindowCloseButton = (Border)FW.FindName("WindowCloseButtonBorder");
            if (WindowStatusBar == null)
                WindowStatusBar = (Border)FW.FindName("StatusBarBorder");
            if (WindowStatusText == null)
                WindowStatusText = (TextBlock)FW.FindName("StatusBarControl");
            if (WindowGrip == null)
                WindowGrip = (Border)FW.FindName("WindowGripBorder");

            if (BorderTopLeft == null)
                BorderTopLeft = (Border)FW.FindName("BorderTopLeft");
            if (BorderTop == null)
                BorderTop = (Border)FW.FindName("BorderTop");
            if (BorderTopRight == null)
                BorderTopRight = (Border)FW.FindName("BorderTopRight");
            if (BorderLeft == null)
                BorderLeft = (Border)FW.FindName("BorderLeft");
            if (BorderRight == null)
                BorderRight = (Border)FW.FindName("BorderRight");
            if (BorderBottomLeft == null)
                BorderBottomLeft = (Border)FW.FindName("BorderBottomLeft");
            if (BorderBottom == null)
                BorderBottom = (Border)FW.FindName("BorderBottom");
            if (BorderBottomRight == null)
                BorderBottomRight = (Border)FW.FindName("BorderBottomRight");

            WindowMinimizeButtonImage = (Image)FW.FindName("WindowMinimizeButtonImage");
            WindowMaximizeButtonImage = (Image)FW.FindName("WindowMaximizeButtonImage");
            WindowCloseButtonImage = (Image)FW.FindName("WindowCloseButtonImage");

            //....................................................................................................
            // form events
            FW.Deactivated += Window_Deactivated;

            //....................................................................................................
            // images events
            if (WindowIcon != null)
            {
                WindowIcon.MouseDown += WindowIcon_OnMouseDown;
            }
            if (WindowTitle != null)
            {
                WindowTitle.MouseDown += WindowTitle_OnMouseDown;
                WindowTitle.MouseUp += WindowTitle_OnMouseUp;
                WindowTitle.MouseMove += WindowTitle_OnMouseMove;
            }
            if (WindowMinimizeButton != null)
            {
                WindowMinimizeButton.MouseDown += WindowMinimizeButton_OnMouseDown;
                WindowMinimizeButton.MouseUp += WindowMinimizeButton_OnMouseUp;
                WindowMinimizeButton.MouseEnter += WindowMinimizeButton_OnMouseEnter;
                WindowMinimizeButton.MouseLeave += WindowMinimizeButton_OnMouseLeave;
                WindowMinimizeButton.MouseMove += WindowMinimizeButton_OnMouseMove;
            }
            if (WindowMaximizeButton != null)
            {
                WindowMaximizeButton.MouseDown += WindowMaximizeButton_OnMouseDown;
                WindowMaximizeButton.MouseUp += WindowMaximizeButton_OnMouseUp;
                WindowMaximizeButton.MouseEnter += WindowMaximizeButton_OnMouseEnter;
                WindowMaximizeButton.MouseLeave += WindowMaximizeButton_OnMouseLeave;
                WindowMaximizeButton.MouseMove += WindowMaximizeButton_OnMouseMove;
            }
            if (WindowCloseButton != null)
            {
                WindowCloseButton.MouseDown += WindowCloseButton_OnMouseDown;
                WindowCloseButton.MouseUp += WindowCloseButton_OnMouseUp;
                WindowCloseButton.MouseEnter += WindowCloseButton_OnMouseEnter;
                WindowCloseButton.MouseLeave += WindowCloseButton_OnMouseLeave;
                WindowCloseButton.MouseMove += WindowCloseButton_OnMouseMove;
            }
            if (WindowGrip != null)
            {
                WindowGrip.MouseDown += WindowGrip_OnMouseDown;
                WindowGrip.MouseUp += WindowGrip_OnMouseUp;
                WindowGrip.MouseMove += WindowGrip_OnMouseMove;
            }

            //....................................................................................................
            // border events
            if (BorderTopLeft != null)
            {
                BorderTopLeft.MouseDown += BorderTopLeft_OnMouseDown;
                BorderTopLeft.MouseUp += BorderTopLeft_OnMouseUp;
                BorderTopLeft.MouseMove += BorderTopLeft_OnMouseMove;
            }
            if (BorderTop != null)
            {
                BorderTop.MouseDown += BorderTop_OnMouseDown;
                BorderTop.MouseUp += BorderTop_OnMouseUp;
                BorderTop.MouseMove += BorderTop_OnMouseMove;
            }
            if (BorderTopRight != null)
            {
                BorderTopRight.MouseDown += BorderTopRight_OnMouseDown;
                BorderTopRight.MouseUp += BorderTopRight_OnMouseUp;
                BorderTopRight.MouseMove += BorderTopRight_OnMouseMove;
            }
            if (BorderLeft != null)
            {
                BorderLeft.MouseDown += BorderLeft_OnMouseDown;
                BorderLeft.MouseUp += BorderLeft_OnMouseUp;
                BorderLeft.MouseMove += BorderLeft_OnMouseMove;
            }
            if (BorderRight != null)
            {
                BorderRight.MouseDown += BorderRight_OnMouseDown;
                BorderRight.MouseUp += BorderRight_OnMouseUp;
                BorderRight.MouseMove += BorderRight_OnMouseMove;
            }
            if (BorderBottomLeft != null)
            {
                BorderBottomLeft.MouseDown += BorderBottomLeft_OnMouseDown;
                BorderBottomLeft.MouseUp += BorderBottomLeft_OnMouseUp;
                BorderBottomLeft.MouseMove += BorderBottomLeft_OnMouseMove;
            }
            if (BorderBottom != null)
            {
                BorderBottom.MouseDown += BorderBottom_OnMouseDown;
                BorderBottom.MouseUp += BorderBottom_OnMouseUp;
                BorderBottom.MouseMove += BorderBottom_OnMouseMove;
            }
            if (BorderBottomRight != null)
            {
                BorderBottomRight.MouseDown += BorderBottomRight_OnMouseDown;
                BorderBottomRight.MouseUp += BorderBottomRight_OnMouseUp;
                BorderBottomRight.MouseMove += BorderBottomRight_OnMouseMove;
            }
            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InitControls()
        //----------------------------------------------------------------------------------------------------
        #endregion // Methods
        //====================================================================================================
        #region Window control
        //----------------------------------------------------------------------------------------------------
        #region Set_StatusBar()
        //----------------------------------------------------------------------------------------------------
        public void Set_StatusBar(bool visible_flag)
        {
            if (WindowStatusText == null) return;

            if (visible_flag)
                WindowStatusText.Visibility = System.Windows.Visibility.Visible;
            else
                WindowStatusText.Visibility = System.Windows.Visibility.Collapsed;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_StatusBar()
        //----------------------------------------------------------------------------------------------------
        #region Set_MaximizeButton()
        //----------------------------------------------------------------------------------------------------
        public void Set_MaximizeButton(bool visible_flag)
        {
            if (WindowMaximizeButton == null) return;

            if (visible_flag)
                WindowMaximizeButton.Visibility = System.Windows.Visibility.Visible;
            else
                WindowMaximizeButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_MaximizeButton()
        //----------------------------------------------------------------------------------------------------
        #region Set_WindowBrush()
        //----------------------------------------------------------------------------------------------------
        public void Set_WindowBrush(Color brush_color)
        {
            if (FW == null) return;

            SolidColorBrush brush = (SolidColorBrush)FW.TryFindResource("WindowBorderBrush");
            if (brush != null) FW.Resources["WindowBorderBrush"] = new SolidColorBrush(brush_color);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_WindowBrush()
        //----------------------------------------------------------------------------------------------------
        #region Set_WindowWidth()
        //----------------------------------------------------------------------------------------------------
        public void Set_WindowWidth(int window_width)
        {
            if (FW == null) return;

            FW.Width = window_width;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_WindowWidth()
        //----------------------------------------------------------------------------------------------------
        #region Set_WindowHeight()
        //----------------------------------------------------------------------------------------------------
        public void Set_WindowHeight(int window_height)
        {
            if (FW == null) return;

            FW.Height = window_height;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_WindowHeight()
        //----------------------------------------------------------------------------------------------------
        #region Set_WindowSize()
        //----------------------------------------------------------------------------------------------------
        public void Set_WindowSize(int window_width, int window_height)
        {
            if (FW == null) return;

            FW.Width = window_width;
            FW.Height = window_height;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_WindowSize()
        //----------------------------------------------------------------------------------------------------
        #region Set_WindowMinimumSize()
        //----------------------------------------------------------------------------------------------------
        public void Set_WindowMinimumSize(int minimum_width, int minimum_height)
        {
            if (FW == null) return;

            FW.MinWidth = minimum_width;
            FW.MinHeight = minimum_height;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_WindowMinimumSize()
        //----------------------------------------------------------------------------------------------------
        #region Set_WindowMaximumSize()
        //----------------------------------------------------------------------------------------------------
        public void Set_WindowMaximumSize(int maximum_width, int maximum_height)
        {
            if (FW == null) return;

            FW.MaxWidth = maximum_width;
            FW.MaxHeight = maximum_height;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_WindowMaximumSize()
        //----------------------------------------------------------------------------------------------------
        #region Set_WindowTitle()
        //----------------------------------------------------------------------------------------------------
        public void Set_WindowTitle(string window_title)
        {
            if (FW != null) FW.Title = window_title;

            if (WindowTitleText != null) WindowTitleText.Text = window_title;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_WindowTitle()
        //----------------------------------------------------------------------------------------------------
        #region Accord_MaximizeButton()
        //----------------------------------------------------------------------------------------------------
        public void Accord_MaximizeButton()
        {
            if (WindowMaximizeButtonImage == null) return;

            if (FW.WindowState == WindowState.Normal)
                WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/png_w_maximize.png"));
            if (FW.WindowState == WindowState.Maximized)
                WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/png_w_normal.png"));
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Accord_MaximizeButton()
        //----------------------------------------------------------------------------------------------------
        #region Do_Maximize()
        //----------------------------------------------------------------------------------------------------
        private void Do_Maximize()
        {
            WindowState ws = FW.WindowState;
            if (ws == WindowState.Normal)
            {
                FW.WindowState = WindowState.Maximized;
                WindowMaximizeButton.ToolTip = "Свернуть в окно";
            }
            if (ws == WindowState.Maximized)
            {
                FW.WindowState = WindowState.Normal;
                WindowMaximizeButton.ToolTip = "Развернуть";
            }

            Accord_MaximizeButton();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Do_Maximize()
        //----------------------------------------------------------------------------------------------------
        #region Close_Window()
        //----------------------------------------------------------------------------------------------------
        public void Close_Window()
        {
            if (FW == null) return;

            FW.Close();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Close_Window()
        //----------------------------------------------------------------------------------------------------
        #endregion // Window control
        //====================================================================================================
        #region Events
        //----------------------------------------------------------------------------------------------------
        #region Window_Deactivated()
        //----------------------------------------------------------------------------------------------------
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Title_HoldFlag = false;
            Grip_HoldFlag = false;
            BorderTop_HoldFlag = false;
            BorderLeft_HoldFlag = false;
            BorderRight_HoldFlag = false;
            BorderBottom_HoldFlag = false;
            BorderBottomRight_HoldFlag = false;
            CloseButtonPressed = false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Window_Deactivated()
        //----------------------------------------------------------------------------------------------------
        #endregion // Events
        //====================================================================================================
        #region Service functions
        //----------------------------------------------------------------------------------------------------
        #region SetStatus()
        //----------------------------------------------------------------------------------------------------
        private void SetStatus(string status_text)
        {
            if (WindowStatusText == null) return;

            WindowStatusText.Text = status_text;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SetStatus()
        //----------------------------------------------------------------------------------------------------
        #region SetResizeFlag()
        //----------------------------------------------------------------------------------------------------
        public void SetResizeFlag(bool resize_flag)
        {
            if (resize_flag == Resize_Flag) return;

            Resize_Flag = resize_flag;


            if (Resize_Flag)
            {

            }
            else
            {
                ClearBorderCursor();

            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SetResizeFlag()
        //----------------------------------------------------------------------------------------------------
        #region ClearBorderCursor()
        //----------------------------------------------------------------------------------------------------
        public void ClearBorderCursor()
        {
            if (BorderTopLeft != null)
            {
                BorderTopLeft.Cursor = Cursors.Arrow;
            }
            if (BorderTop != null)
            {
                BorderTop.Cursor = Cursors.Arrow;
            }
            if (BorderTopRight != null)
            {
                BorderTopRight.Cursor = Cursors.Arrow;
            }
            if (BorderLeft != null)
            {
                BorderLeft.Cursor = Cursors.Arrow;
            }
            if (BorderRight != null)
            {
                BorderRight.Cursor = Cursors.Arrow;
            }
            if (BorderBottomLeft != null)
            {
                BorderBottomLeft.Cursor = Cursors.Arrow;
            }
            if (BorderBottom != null)
            {
                BorderBottom.Cursor = Cursors.Arrow;
            }
            if (BorderBottomRight != null)
            {
                BorderBottomRight.Cursor = Cursors.Arrow;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ClearBorderCursor()
        //----------------------------------------------------------------------------------------------------
        #region SetSizeButtons()
        //----------------------------------------------------------------------------------------------------
        public void SetSizeButtons(bool maximize_flag, bool minimize_flag)
        {
            if (!maximize_flag)
                WindowMaximizeButton.Visibility = System.Windows.Visibility.Collapsed;
            else
                WindowMaximizeButton.Visibility = System.Windows.Visibility.Visible;

            if (!minimize_flag)
                WindowMinimizeButton.Visibility = System.Windows.Visibility.Collapsed;
            else
                WindowMinimizeButton.Visibility = System.Windows.Visibility.Visible;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SetSizeButtons()
        //----------------------------------------------------------------------------------------------------
        #region SetIconFlag()
        //----------------------------------------------------------------------------------------------------
        public void SetIconFlag(bool icon_flag)
        {
            if (!icon_flag)
                WindowIcon.Visibility = System.Windows.Visibility.Collapsed;
            else
                WindowIcon.Visibility = System.Windows.Visibility.Visible;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SetIconFlag()
        //----------------------------------------------------------------------------------------------------
        #region SetStatusFlag()
        //----------------------------------------------------------------------------------------------------
        public void SetStatusFlag(bool status_flag)
        {
            if (!status_flag)
                WindowStatusBar.Visibility = System.Windows.Visibility.Collapsed;
            else
                WindowStatusBar.Visibility = System.Windows.Visibility.Visible;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SetStatusFlag()
        //----------------------------------------------------------------------------------------------------
        #region SetHoldParams()
        //----------------------------------------------------------------------------------------------------
        private void SetHoldParams()
        {
            if (FW == null) return;

            Point position = Mouse.GetPosition(FW);
            MouseHoldX = position.X;
            MouseHoldY = position.Y;
            FormHoldLeft = FW.Left;
            FormHoldTop = FW.Top;
            FormHoldWidth = FW.Width;
            FormHoldHeight = FW.Height;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SetHoldParams()
        //----------------------------------------------------------------------------------------------------
        #region AccordSize()
        //----------------------------------------------------------------------------------------------------
        private void AccordSize(bool HoldFlag, bool LeftFlag, bool TopFlag, bool RightFlag, bool BottomFlag)
        {
            if (FW == null) return;
            if (!HoldFlag) return;
            if (!LeftFlag && !TopFlag && !RightFlag && !BottomFlag) return;

            Point position = Mouse.GetPosition(FW);
            double d_w = position.X - MouseHoldX - (FW.Width - FormHoldWidth);
            double d_h = position.Y - MouseHoldY - (FW.Height - FormHoldHeight);
            if (LeftFlag) d_w = -(position.X - MouseHoldX);
            if (TopFlag) d_h = -(position.Y - MouseHoldY);

            if (FW.Width + d_w < FW.MinWidth) d_w = FW.MinWidth - FW.Width;
            if (FW.Height + d_h < FW.MinHeight) d_h = FW.MinHeight - FW.Height;
            if (FW.Width + d_w > FW.MaxWidth) d_w = FW.MaxWidth - FW.Width;
            if (FW.Height + d_h > FW.MaxHeight) d_h = FW.MaxHeight - FW.Height;

            if (d_w != 0 && !LeftFlag && !RightFlag) d_w = 0;
            if (d_h != 0 && !TopFlag && !BottomFlag) d_h = 0;

            if (d_w == 0 && d_h == 0) return;

            double d_x = 0;
            double d_y = 0;
            if (LeftFlag) d_x = -d_w;
            if (TopFlag) d_y = -d_h;

            int x = (int)Math.Round(FW.Left + d_x);
            int y = (int)Math.Round(FW.Top + d_y);
            int w = (int)Math.Round(FW.Width + d_w);
            int h = (int)Math.Round(FW.Height + d_h);

            if (WindowHandle == System.IntPtr.Zero)
            {
                WindowInteropHelper wih = new WindowInteropHelper(WindowObject);
                WindowHandle = wih.Handle;
            }

            MoveWindow(WindowHandle, x, y, w, h, true);

            /*
            if (d_w < 0) FW.Width = FW.Width + d_w;
            if (d_h < 0) FW.Height = FW.Height + d_h;
            if (d_x != 0) FW.Left = FW.Left + d_x;
            if (d_y != 0) FW.Top = FW.Top + d_y;
            if (d_w > 0) FW.Width = FW.Width + d_w;
            if (d_h > 0) FW.Height = FW.Height + d_h;
            */
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AccordSize()
        //----------------------------------------------------------------------------------------------------
        #endregion // Service functions
        //====================================================================================================
        #region Events handlers
        //====================================================================================================
        #region WindowIcon
        //----------------------------------------------------------------------------------------------------
        private void WindowIcon_OnMouseDown(object sender, RoutedEventArgs e)
        {
            // window dropdown menu arrives

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WindowIcon
        //====================================================================================================
        #region WindowTitle
        //----------------------------------------------------------------------------------------------------
        private void WindowTitle_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (FW == null) return;
            if (WindowTitle == null) return;

            if (e.ClickCount == 2)
            {
                if (WindowMaximizeButton.Visibility == System.Windows.Visibility.Visible)
                {
                    Do_Maximize();
                    return;
                }
            }

            WindowTitle.CaptureMouse();
            Title_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowTitle_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowTitle == null) return;

            if (Title_HoldFlag)
            {
                WindowTitle.ReleaseMouseCapture();
                Title_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowTitle_OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowTitle == null) return;

            if (Title_HoldFlag)
            {
                Point position = Mouse.GetPosition(FW);
                double d_x = position.X - MouseHoldX;
                double d_y = position.Y - MouseHoldY;
                if (d_x != 0) FW.Left = FW.Left + d_x;
                if (d_y != 0) FW.Top = FW.Top + d_y;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WindowTitle
        //====================================================================================================
        #region WindowMinimizeButton
        //----------------------------------------------------------------------------------------------------
        private void WindowMinimizeButton_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowMinimizeButton == null) return;

            WindowMinimizeButton.CaptureMouse();
            MinimizeButtonPressed = true;

            if (WindowMinimizeButtonImage != null)
            {
                WindowMinimizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_minimize_pressed.bmp"));
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowMinimizeButton_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowMinimizeButton == null) return;

            bool bToMinimize = false;
            if (MinimizeButtonPressed)
            {
                WindowMinimizeButton.ReleaseMouseCapture();
                MinimizeButtonPressed = false;
                if (MinimizeButtonOver) bToMinimize = true;
            }

            if (WindowMinimizeButtonImage != null)
            {
                WindowMinimizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/png_w_minimize.png"));
            }

            if (bToMinimize)
            {
                if (FW.WindowState != WindowState.Minimized) FW.WindowState = WindowState.Minimized;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowMinimizeButton_OnMouseEnter(object sender, RoutedEventArgs e)
        {
            MinimizeButtonOver = true;

            if (WindowMinimizeButtonImage == null) return;

            if (MinimizeButtonPressed)
                WindowMinimizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_minimize_pressed.bmp"));
            else
                WindowMinimizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_minimize_over.bmp"));
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowMinimizeButton_OnMouseLeave(object sender, RoutedEventArgs e)
        {
            MinimizeButtonOver = false;

            if (WindowMinimizeButtonImage == null) return;

            if (MinimizeButtonPressed)
                WindowMinimizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_minimize_over.bmp"));
            else
                WindowMinimizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/png_w_minimize.png"));
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowMinimizeButton_OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowMinimizeButton == null) return;
            if (WindowMinimizeButtonImage == null) return;

            if (!MinimizeButtonPressed) return;

            Point pos = Mouse.GetPosition(WindowMinimizeButtonImage);
            int w = (int)Math.Round(WindowMinimizeButtonImage.Width);
            int h = (int)Math.Round(WindowMinimizeButtonImage.Height);
            bool bPosOver = (pos.X >= 0 && pos.X <= w && pos.Y >= 0 && pos.Y <= h);

            bool bOver = MinimizeButtonOver;
            if (bOver && !bPosOver) MinimizeButtonOver = false;
            if (!bOver && bPosOver) MinimizeButtonOver = true;

            if (!bOver && MinimizeButtonOver) // mouse enter
                WindowMinimizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_minimize_pressed.bmp"));

            if (bOver && !MinimizeButtonOver) // mouse leave
                WindowMinimizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_minimize_over.bmp"));
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WindowMinimizeButton
        //====================================================================================================
        #region WindowMaximizeButton
        //----------------------------------------------------------------------------------------------------
        private void WindowMaximizeButton_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowMaximizeButton == null) return;

            WindowMaximizeButton.CaptureMouse();
            MaximizeButtonPressed = true;

            if (WindowMaximizeButtonImage != null)
            {
                if (FW.WindowState == WindowState.Normal)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_maximize_pressed.bmp"));
                if (FW.WindowState == WindowState.Maximized)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_normal_pressed.bmp"));
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowMaximizeButton_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowMaximizeButton == null) return;

            bool bToMaximize = false;
            if (MaximizeButtonPressed)
            {
                WindowMaximizeButton.ReleaseMouseCapture();
                MaximizeButtonPressed = false;
                if (MaximizeButtonOver) bToMaximize = true;
            }

            if (bToMaximize)
                Do_Maximize();
            else
                Accord_MaximizeButton();
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowMaximizeButton_OnMouseEnter(object sender, RoutedEventArgs e)
        {
            MaximizeButtonOver = true;

            if (WindowMaximizeButtonImage == null) return;

            if (MaximizeButtonPressed)
            {
                if (FW.WindowState == WindowState.Normal)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_maximize_pressed.bmp"));
                if (FW.WindowState == WindowState.Maximized)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_normal_pressed.bmp"));
            }
            else
            {
                if (FW.WindowState == WindowState.Normal)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_maximize_over.bmp"));
                if (FW.WindowState == WindowState.Maximized)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_normal_over.bmp"));
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowMaximizeButton_OnMouseLeave(object sender, RoutedEventArgs e)
        {
            MaximizeButtonOver = false;

            if (WindowMaximizeButtonImage == null) return;

            if (MaximizeButtonPressed)
            {
                if (FW.WindowState == WindowState.Normal)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_maximize_over.bmp"));
                if (FW.WindowState == WindowState.Maximized)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_normal_over.bmp"));
            }
            else
            {
                if (FW.WindowState == WindowState.Normal)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/png_w_maximize.png"));
                if (FW.WindowState == WindowState.Maximized)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/png_w_normal.png"));
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowMaximizeButton_OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowMaximizeButton == null) return;
            if (WindowMaximizeButtonImage == null) return;

            if (!MaximizeButtonPressed) return;

            Point pos = Mouse.GetPosition(WindowMaximizeButtonImage);
            int w = (int)Math.Round(WindowMaximizeButtonImage.Width);
            int h = (int)Math.Round(WindowMaximizeButtonImage.Height);
            bool bPosOver = (pos.X >= 0 && pos.X <= w && pos.Y >= 0 && pos.Y <= h);

            bool bOver = MaximizeButtonOver;
            if (bOver && !bPosOver) MaximizeButtonOver = false;
            if (!bOver && bPosOver) MaximizeButtonOver = true;

            if (!bOver && MaximizeButtonOver) // mouse enter
            {
                if (FW.WindowState == WindowState.Normal)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_maximize_pressed.bmp"));
                if (FW.WindowState == WindowState.Maximized)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_normal_pressed.bmp"));
            }
            if (bOver && !MaximizeButtonOver) // mouse leave
            {
                if (FW.WindowState == WindowState.Normal)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_maximize_over.bmp"));
                if (FW.WindowState == WindowState.Maximized)
                    WindowMaximizeButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_normal_over.bmp"));
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WindowMaximizeButton
        //====================================================================================================
        #region WindowCloseButton
        //----------------------------------------------------------------------------------------------------
        private void WindowCloseButton_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowCloseButton == null) return;

            WindowCloseButton.CaptureMouse();
            CloseButtonPressed = true;

            if (WindowCloseButtonImage != null)
            {
                WindowCloseButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_close_pressed.bmp"));
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowCloseButton_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowCloseButton == null) return;

            bool bToClose = false;
            if (CloseButtonPressed)
            {
                WindowCloseButton.ReleaseMouseCapture();
                CloseButtonPressed = false;
                if (CloseButtonOver) bToClose = true;
            }

            if (WindowCloseButtonImage != null)
            {
                WindowCloseButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/png_w_close.png"));
            }

            if (bToClose)
            {
                Close_Window();
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowCloseButton_OnMouseEnter(object sender, RoutedEventArgs e)
        {
            CloseButtonOver = true;

            if (WindowCloseButtonImage == null) return;

            if (CloseButtonPressed)
                WindowCloseButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_close_pressed.bmp"));
            else
                WindowCloseButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_close_over.bmp"));
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowCloseButton_OnMouseLeave(object sender, RoutedEventArgs e)
        {
            CloseButtonOver = false;

            if (WindowCloseButtonImage == null) return;

            if (CloseButtonPressed)
            {
                WindowCloseButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_close_over.bmp"));
            }
            else
            {
                WindowCloseButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/png_w_close.png"));
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowCloseButton_OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowCloseButton == null) return;
            if (WindowCloseButtonImage == null) return;

            if (!CloseButtonPressed) return;

            Point pos = Mouse.GetPosition(WindowCloseButtonImage);
            int w = (int)Math.Round(WindowCloseButtonImage.Width);
            int h = (int)Math.Round(WindowCloseButtonImage.Height);
            bool bPosOver = (pos.X >= 0 && pos.X <= w && pos.Y >= 0 && pos.Y <= h);

            bool bOver = CloseButtonOver;
            if (bOver && !bPosOver) CloseButtonOver = false;
            if (!bOver && bPosOver) CloseButtonOver = true;

            if (!bOver && CloseButtonOver) // mouse enter
            {
                WindowCloseButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_close_pressed.bmp"));
            }
            if (bOver && !CloseButtonOver) // mouse leave
            {
                WindowCloseButtonImage.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/img_w_close_over.bmp"));
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WindowCloseButton
        //====================================================================================================
        #region WindowGrip
        //----------------------------------------------------------------------------------------------------
        private void WindowGrip_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowGrip == null) return;
            if (!Resize_Flag) return;

            WindowGrip.CaptureMouse();
            Grip_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowGrip_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (WindowGrip == null) return;

            if (Grip_HoldFlag)
            {
                WindowGrip.ReleaseMouseCapture();
                Grip_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void WindowGrip_OnMouseMove(object sender, RoutedEventArgs e)
        {
            AccordSize(Grip_HoldFlag, false, false, true, true);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WindowGrip
        //====================================================================================================
        #region BorderTopLeft
        //----------------------------------------------------------------------------------------------------
        private void BorderTopLeft_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderTopLeft == null) return;
            if (!Resize_Flag) return;

            BorderTopLeft.CaptureMouse();
            BorderTopLeft_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderTopLeft_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderTopLeft == null) return;

            if (BorderTopLeft_HoldFlag)
            {
                BorderTopLeft.ReleaseMouseCapture();
                BorderTopLeft_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderTopLeft_OnMouseMove(object sender, RoutedEventArgs e)
        {
            AccordSize(BorderTopLeft_HoldFlag, true, true, false, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // BorderTopLeft
        //====================================================================================================
        #region BorderTop
        //----------------------------------------------------------------------------------------------------
        private void BorderTop_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderTop == null) return;
            if (!Resize_Flag) return;

            BorderTop.CaptureMouse();
            BorderTop_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderTop_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderTop == null) return;

            if (BorderTop_HoldFlag)
            {
                BorderTop.ReleaseMouseCapture();
                BorderTop_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderTop_OnMouseMove(object sender, RoutedEventArgs e)
        {
            AccordSize(BorderTop_HoldFlag, false, true, false, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // BorderTop
        //====================================================================================================
        #region BorderTopRight
        //----------------------------------------------------------------------------------------------------
        private void BorderTopRight_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderTopRight == null) return;
            if (!Resize_Flag) return;

            BorderTopRight.CaptureMouse();
            BorderTopRight_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderTopRight_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderTopRight == null) return;

            if (BorderTopRight_HoldFlag)
            {
                BorderTopRight.ReleaseMouseCapture();
                BorderTopRight_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderTopRight_OnMouseMove(object sender, RoutedEventArgs e)
        {
            AccordSize(BorderTopRight_HoldFlag, false, true, true, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // BorderTop
        //====================================================================================================
        #region BorderLeft
        //----------------------------------------------------------------------------------------------------
        private void BorderLeft_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderLeft == null) return;
            if (!Resize_Flag) return;

            BorderLeft.CaptureMouse();
            BorderLeft_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderLeft_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderLeft == null) return;

            if (BorderLeft_HoldFlag)
            {
                BorderLeft.ReleaseMouseCapture();
                BorderLeft_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderLeft_OnMouseMove(object sender, RoutedEventArgs e)
        {
            AccordSize(BorderLeft_HoldFlag, true, false, false, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // BorderLeft
        //====================================================================================================
        #region BorderRight
        //----------------------------------------------------------------------------------------------------
        private void BorderRight_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderRight == null) return;
            if (!Resize_Flag) return;

            BorderRight.CaptureMouse();
            BorderRight_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderRight_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderRight == null) return;

            if (BorderRight_HoldFlag)
            {
                BorderRight.ReleaseMouseCapture();
                BorderRight_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderRight_OnMouseMove(object sender, RoutedEventArgs e)
        {
            AccordSize(BorderRight_HoldFlag, false, false, true, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // BorderRight
        //====================================================================================================
        #region BorderBottomLeft
        //----------------------------------------------------------------------------------------------------
        private void BorderBottomLeft_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderBottomLeft == null) return;
            if (!Resize_Flag) return;

            BorderBottomLeft.CaptureMouse();
            BorderBottomLeft_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderBottomLeft_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderBottomLeft == null) return;

            if (BorderBottomLeft_HoldFlag)
            {
                BorderBottomLeft.ReleaseMouseCapture();
                BorderBottomLeft_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderBottomLeft_OnMouseMove(object sender, RoutedEventArgs e)
        {
            AccordSize(BorderBottomLeft_HoldFlag, true, false, false, true);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // BorderBottomLeft
        //====================================================================================================
        #region BorderBottom
        //----------------------------------------------------------------------------------------------------
        private void BorderBottom_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderBottom == null) return;
            if (!Resize_Flag) return;

            BorderBottom.CaptureMouse();
            BorderBottom_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderBottom_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderBottom == null) return;

            if (BorderBottom_HoldFlag)
            {
                BorderBottom.ReleaseMouseCapture();
                BorderBottom_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderBottom_OnMouseMove(object sender, RoutedEventArgs e)
        {
            AccordSize(BorderBottom_HoldFlag, false, false, false, true);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // BorderBottom
        //====================================================================================================
        #region BorderBottomRight
        //----------------------------------------------------------------------------------------------------
        private void BorderBottomRight_OnMouseDown(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderBottomRight == null) return;
            if (!Resize_Flag) return;

            BorderBottomRight.CaptureMouse();
            BorderBottomRight_HoldFlag = true;

            SetHoldParams();
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderBottomRight_OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (FW == null) return;
            if (BorderBottomRight == null) return;

            if (BorderBottomRight_HoldFlag)
            {
                BorderBottomRight.ReleaseMouseCapture();
                BorderBottomRight_HoldFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void BorderBottomRight_OnMouseMove(object sender, RoutedEventArgs e)
        {
            AccordSize(BorderBottomRight_HoldFlag, false, false, true, true);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // BorderBottomRight
        //====================================================================================================
        #endregion // Events handlers
        //====================================================================================================
    }
}

