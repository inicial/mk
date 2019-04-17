using System.Windows.Controls;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Flight;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for FlightFullControl.xaml
    /// </summary>
    public partial class FlightControl : UserControl, IFlightControl
    {
        public BookingAvia BookingAvia;

        public FlightControl()
        {
            InitializeComponent();
        }

        public void ScrollToTop()
        {
            Scroll.ScrollToTop();
        }
    }
}
