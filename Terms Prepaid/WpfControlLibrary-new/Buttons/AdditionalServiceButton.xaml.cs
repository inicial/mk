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
    /// Interaction logic for AdditionalServiceButton.xaml
    /// </summary>
    public partial class AdditionalServiceButton : SimpleMenu
    {
        public enum Tags
        {
            AviaTable = 1,
            Statement = 2,
            Tourist = 3,
            Journal = 4,
            RequestsJournal = 5,
            RequestsJournalDetail = 6
        }

        public AdditionalServiceButton()
        {
            InitializeComponent();
            _menuItems = new Dictionary<int, MenuItem>();
            
            AddMenuItem(Flights, Tags.AviaTable);
            AddMenuItem(Statements, Tags.Statement);
            AddMenuItem(Tourists, Tags.Tourist);
            AddMenuItem(Journal, Tags.Journal);
            AddMenuItem(RequestsJournal, Tags.RequestsJournal);
            AddMenuItem(RequestsJournalDetail, Tags.RequestsJournalDetail);
        }
        
    }
}
