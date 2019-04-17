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
using System.Windows.Shapes;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for DialogView.xaml
    /// </summary>
    public partial class DialogView : SimpleWindow, IDialog
    {
        public enum Buttons { Yes, No, YesNo }

        private Buttons _buttonsState;
        public bool Result { get; private set; }

        public Buttons ButtonsState 
        { 
            get { return _buttonsState;  }
            set
            {
                YesButton.Visibility = value == Buttons.Yes || value == Buttons.YesNo ? Visibility.Visible : Visibility.Collapsed;
                NoButton.Visibility = value == Buttons.No || value == Buttons.YesNo ? Visibility.Visible : Visibility.Collapsed;
                _buttonsState = value;
            }
        }

        private string _yesButtonText;
        public string YesButtonText
        {
            get { return _yesButtonText; }
            set
            {
                YesButton.Content = value;
                _yesButtonText = value;
            }
        }

        private string _noButtonText;
        public string NoButtonText
        {
            get { return _noButtonText; }
            set
            {
                NoButton.Content = value;
                _noButtonText = value;
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                MessageTbl.Text = value;
                _message = value;
            }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                var img = new BitmapImage(new Uri(string.Format(@"/WpfControlLibrary;component/{0}", value), UriKind.Relative));
                Image.Source = img;
                _imageSource = value;
            }
        }

        private double _imageWidth;
        public double ImageWidth
        {
            get { return _imageWidth; }
            set
            {
                Image.Width = value;
                _imageWidth = value;
            }
        }

        private double _imageHeight;
        public double ImageHeight
        {
            get { return _imageHeight; }
            set
            {
                Image.Height = value;
                _imageHeight = value;
            }
        }

        private Style _yesButtonStyle;
        public Style YesButtonStyle
        {
            get { return _yesButtonStyle; }
            set
            {
                YesButton.Style = value;
                _yesButtonStyle = value;
            }
        }

        private Style _noButtonStyle;
        public Style NoButtonStyle
        {
            get { return _noButtonStyle; }
            set
            {
                NoButton.Style = value;
                _noButtonStyle = value;
            }
        }

        public Brush YesButtonColor { get; set; }
        public Brush NoButtonColor { get; set; }

        public static readonly DependencyProperty YesButtonTextProperty = DependencyProperty.Register("YesButtonText", typeof(string), typeof(DialogView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty NoButtonTextProperty = DependencyProperty.Register("NoButtonText", typeof(string), typeof(DialogView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(DialogView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty YesButtonColorProperty = DependencyProperty.Register("YesButtonColor", typeof(Brush), typeof(DialogView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty NoButtonStyleProperty = DependencyProperty.Register("NoButtonStyle", typeof(Brush), typeof(DialogView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty YesButtonsStyleProperty = DependencyProperty.Register("YesButtonsStyle", typeof(Buttons), typeof(DialogView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty NoButtonColorProperty = DependencyProperty.Register("NoButtonColor", typeof(Brush), typeof(DialogView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty ButtonsStateProperty = DependencyProperty.Register("ButtonsState", typeof(Buttons), typeof(DialogView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(DialogView),
            new UIPropertyMetadata(null));

        public DialogView()
        {
            InitializeComponent();
        }

        public DialogView(string title, string message, Buttons buttons)
        {
            InitializeComponent();
            Title = title;
            Message = message;
            ButtonsState = buttons;
        }
        
        private void NoButton_OnClick(object sender, RoutedEventArgs e)
        {
            Result = false;
            DialogResult = false;
        }

        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            Result = true;
            DialogResult = true;
        }
    }
}
