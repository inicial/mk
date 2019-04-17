using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;
using WpfControlLibrary.Model.Flight;
using WpfControlLibrary.Model.Tourist;

namespace WpfControlLibrary.ViewModel
{
    public class ArrivalsViewModel : ViewModelBase
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetValue(ref _date, value); }
        }

        private ObservableCollection<FlightInfo> _thereAndBackFlights;
        public ObservableCollection<FlightInfo> ThereAndBackFlights
        {
            get { return _thereAndBackFlights; }
            set
            {
                SetValue(ref _thereAndBackFlights, value);
                BindCommands(_thereAndBackFlights, OnButtonClick);
            }
        }

        private ObservableCollection<FlightInfo> _thereFlights;
        public ObservableCollection<FlightInfo> ThereFlights
        {
            get { return _thereFlights; }
            set
            {
                SetValue(ref _thereFlights, value);
                BindCommands(_thereFlights, OnButtonClick);
            }
        }

        public ArrivalsViewModel()
        {
            Date = DateTime.Now;
            ThereAndBackFlights = new ObservableCollection<FlightInfo>();
            ThereFlights = new ObservableCollection<FlightInfo>();
        }

        private void BindCommands(ObservableCollection<FlightInfo> flightInfoCollection,
            MyControlEventHandlerSample eventHandler)
        {
            if (flightInfoCollection != null)
                foreach (FlightInfo t in flightInfoCollection)
                {
                    t.OnButtonClick -= eventHandler;
                    t.OnButtonClick += eventHandler;
                }
        }

        public void OnButtonClick(object obj)
        {
            if (obj is FlightInfo)
            {
                var flightInfo = (FlightInfo) obj;
            }
        }
    }
}
