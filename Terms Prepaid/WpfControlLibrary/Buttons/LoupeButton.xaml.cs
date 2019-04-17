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
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Buttons
{
    /// <summary>
    /// Interaction logic for LoupeButton.xaml
    /// </summary>
    public partial class LoupeButton : UserControl, IButton
    {
        public event MyControlEventHandlerSample OnButtonClick;
        public bool Expand;

        public LoupeButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OnButtonClick != null)
                OnButtonClick(this);

            Expand = !Expand;

            ControlTemplate ct;

            if (Expand)
                ct = (ControlTemplate) Resources["LoupeCollapseControlTemplate"];
            else 
                ct = (ControlTemplate) Resources["LoupeExpandControlTemplate"];
            
            C.Template = ct;
        }
    }
}
