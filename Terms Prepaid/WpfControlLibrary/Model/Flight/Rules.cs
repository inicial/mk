using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Model.Flight
{
    public class Arrival
    {
        public string cityCode;
    }

    public class Departure
    {
        public string cityCode;
    }

    public class Segment
    {
        public string OD;
        public Arrival arrival;
        public string arrivalCode;
        public Departure departure;
        public string departureCode;
    }

    public class Fare
    {
        public bool cancellationAfterDeparture;
        public bool cancellationBeforeDeparture;
        public bool changeAfterDeparture;
        public bool changeBeforeDeparture;
        public string content;
        public Segment segment;
    }

    public class CANCELLATION_POLICE
    {
        public Fare[] fares;
    }

    public class Rules
    {
        public CANCELLATION_POLICE CANCELLATION_POLICE;
    }
}
