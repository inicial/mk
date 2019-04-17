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
    /// Interaction logic for AddServiceButton.xaml
    /// </summary>
    public partial class AddServiceButton : SimpleMenu
    {
        public enum Tags
        {
            Cruise = 1,
            Flight = 2
        }

        public AddServiceButton()
        {
            InitializeComponent();
            _menuItems = new Dictionary<int, MenuItem>();

            AddMenuItem(Cruise, Tags.Cruise);
            AddMenuItem(Flight, Tags.Flight);
        }
    }
}
