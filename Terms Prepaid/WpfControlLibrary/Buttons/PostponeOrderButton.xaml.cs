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
using System.Threading.Tasks;
using System.ComponentModel;
using WpfControlLibrary.Common;
using ActiveSharp.PropertyMapping;


namespace WpfControlLibrary
{
    public class PostponeViewModel : INotifyPropertyChanged
    {
        //readonly PropertyChangeHelper _propertyChangeHelper = new PropertyChangeHelper();

        public event PropertyChangedEventHandler PropertyChanged;
        //public event PropertyChangedEventHandler PropertyChanged
        //{
        //    add { _propertyChangeHelper.Add(value); }
        //    remove { _propertyChangeHelper.Remove(value); }
        //}

        //protected void SetValue<T>(ref T field, T value)
        //{
        //    _propertyChangeHelper.SetValue(this, ref field, value);
        //}

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private string TitleStr = "Отложить заказ";

        public int _titleCount;
        public int TitleCount
        {
            get { return _titleCount; }
            set
            {
                //SetValue(ref _titleCount, value);
                _titleCount = value;
                if (_titleCount > 0)
                    TitleText = TitleStr + " (" + _titleCount.ToString() + ")";
                else
                    TitleText = TitleStr;
            }
        }

        private string _titleText;
        public string TitleText
        {
            get { return _titleText; }
            set 
            { 
                //SetValue(ref _titleText, value);
                _titleText = value;
                OnPropertyChanged("TitleText");
            }
        }

        public PostponeViewModel()
        {
            TitleText = TitleStr;
            TitleCount = 0;
        }
    }

    public partial class PostponeOrderButton : UserControl, IButton
    {
        public event MyControlEventHandlerSample OnButtonClick;

        public int Selected = 0;

        public PostponeViewModel _viewModel;


        public PostponeOrderButton()
        {
            InitializeComponent();

            _viewModel = new PostponeViewModel();
            DataContext = _viewModel;
        }

        public void SetTitleCount(int count)
        {
            if (_viewModel == null) return;

            _viewModel.TitleCount = count;
        }
        private void popupOption_Closed(object sender, EventArgs e)
        {
            btnOption.IsChecked = false;
        }

        private void btnOption_Checked(object sender, RoutedEventArgs e)
        {
            Selected = 0;
            if (OnButtonClick != null)
            {
                OnButtonClick(this);
                return;
            }

            popupOption.IsOpen = true;
            popupOption.Closed -= popupOption_Closed;
            popupOption.Closed += popupOption_Closed;
        }

        private void btnOption_Unchecked(object sender, RoutedEventArgs e)
        {
            popupOption.IsOpen = false;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            btnOption.IsChecked = false;
            Selected = 1;
            if (OnButtonClick != null)
                OnButtonClick(this);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            btnOption.IsChecked = false;
            Selected = 2;
            if (OnButtonClick != null)
                OnButtonClick(this);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            btnOption.IsChecked = false;
            Selected = 3;
            if (OnButtonClick != null)
                OnButtonClick(this);
        }
    }

}
