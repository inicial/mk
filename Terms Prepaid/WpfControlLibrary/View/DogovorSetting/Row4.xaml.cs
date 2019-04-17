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

namespace WpfControlLibrary.View.DogovorSetting
{
    /// <summary>
    /// Interaction logic for Row4.xaml
    /// </summary>
    public partial class Row4 : UserControl
    {
        public string Value1
        {
            get { return (string) GetValue(Value1Property); }
            set { SetValue(Value1Property, value); }
        }

        public string Value2
        {
            get { return (string)GetValue(Value2Property); }
            set { SetValue(Value2Property, value); }
        }

        public string Value3
        {
            get { return (string)GetValue(Value3Property); }
            set { SetValue(Value3Property, value); }
        }

        public string Value4
        {
            get { return (string)GetValue(Value4Property); }
            set { SetValue(Value4Property, value); }
        }

        public static readonly DependencyProperty Value1Property = DependencyProperty.Register("Value1", typeof(string), typeof(FrameworkElement));
        public static readonly DependencyProperty Value2Property = DependencyProperty.Register("Value2", typeof(string), typeof(FrameworkElement));
        public static readonly DependencyProperty Value3Property = DependencyProperty.Register("Value3", typeof(string), typeof(FrameworkElement));
        public static readonly DependencyProperty Value4Property = DependencyProperty.Register("Value4", typeof(string), typeof(FrameworkElement));

        public Row4()
        {
            InitializeComponent();
        }
    }
}
