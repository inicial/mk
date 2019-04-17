using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Model.Flight
{
    public class Flight : Data
    {
        private int _idBron;

        private DateTime _departureDateTime;
        public DateTime DepartureDateTime
        {
            get { return _departureDateTime; }
            set
            {
                SetValue(ref _departureDateTime, value);
                HideArrivalDate = DepartureDateTime.Date.Equals(ArrivalDateTime.Date);
            }
        }

        private DateTime _arrivalDateTime;
        public DateTime ArrivalDateTime
        {
            get { return _arrivalDateTime; }
            set
            {
                SetValue(ref _arrivalDateTime, value);
                HideArrivalDate = DepartureDateTime.Date.Equals(ArrivalDateTime.Date);
            }
        }

        private string _dateTimeString;
        public string DateTimeString
        {
            get { return _dateTimeString; }
            set { SetValue(ref _dateTimeString, value);}
        }

        private string _dateString;
        public string DateString
        {
            get { return _dateString; }
            set { SetValue(ref _dateString, value);}
        }

        private string _timeString;
        public string TimeString
        {
            get { return _timeString; }
            set { SetValue(ref _timeString, value);}
        }

        private string _departureAirport;
        public string DepartureAirport
        {
            get { return _departureAirport; }
            set
            {
                SetValue(ref _departureAirport, CityCountryConverter.Convert(value));
                DepartureAirportInvert = GetInvertAirport(_departureAirport);
            }
        }

        private string _departureAirportInvert;
        public string DepartureAirportInvert
        {
            get { return _departureAirportInvert; }
            set { SetValue(ref _departureAirportInvert, value); }
        }

        private string _arrivalAirport;
        public string ArrivalAirport
        {
            get { return _arrivalAirport; }
            set
            {
                SetValue(ref _arrivalAirport, CityCountryConverter.Convert(value));
                ArrivalAirportInvert = GetInvertAirport(_arrivalAirport);
            }
        }

        private string _arrivalAirportInvert;
        public string ArrivalAirportInvert
        {
            get { return _arrivalAirportInvert; }
            set { SetValue(ref _arrivalAirportInvert, value); }
        }

        private string _flightNumber;
        public string FlightNumber
        {
            get { return _flightNumber; }
            set { SetValue(ref _flightNumber, value);}
        }

        private string _company;
        public string Company
        {
            get { return _company; }
            set { SetValue(ref _company, value);}
        }

        private string _baggage;
        public string Baggage
        {
            get { return _baggage; }
            set { SetValue(ref _baggage, value); }
        }

        private string _className;
        public string ClassName
        {
            get { return _className; }
            set
            {
                SetValue(ref _className, value.Replace("Экономический класс", "Эконом"));
            }
        }

        private string _classCode;
        public string ClassCode
        {
            get { return _classCode; }
            set { SetValue(ref _classCode, value); }
        }

        private string _classFullName;
        public string ClassFullName
        {
            get
            {
                _classFullName = GetFullName();
                return _classFullName; 
            }
            set { SetValue(ref _classFullName, value); }
        }

        private bool _hideArrivalDate;
        public bool HideArrivalDate
        {
            get { return _hideArrivalDate; }
            set { SetValue(ref _hideArrivalDate, value); }
        }

        private TimeSpan _waitingTime;
        public TimeSpan WaitingTime
        {
            get { return _waitingTime; }
            set
            {
                SetValue(ref _waitingTime, value);
                ShowWaitingTime = _waitingTime > new TimeSpan();
            }
        }

        private bool _showWaitingTime;
        public bool ShowWaitingTime
        {
            get { return _showWaitingTime; }
            set { SetValue(ref _showWaitingTime, value); }
        }

        private bool _isOk;
        public bool IsOk
        {
            get { return _isOk; }
            set { _isOk = value; }
        }

        private int _paymentState;
        public int PaymentState
        {
            get { return _paymentState; }
            set { _paymentState = value; }
        }

        public int Status { get; set; }

        public string ErrorCode { get; set; }

        public DateTime DateOf { get; set; }

        public string Seller { get; set; }

        private string GetFullName()
        {
            return String.Format("{0} - {1}", ClassCode, ClassName);
        }

        private string GetFirstName(string airport)
        {
            int b = airport.IndexOf('(');
            return (b > -1) ? airport.Substring(0, b) : null;
        }

        private string GetSecondName(string airport)
        {
            int b = airport.IndexOf('(');
            int e = airport.IndexOf(')');

            return (b > -1 && e > b) ? airport.Substring(b + 1, e - b - 1) : null;
        }

        private string GetInvertAirport(string airport)
        {
            string first = GetFirstName(airport);
            string second = GetSecondName(airport);

            return (first != null & second != null) ? String.Format("{1} ({0})", first, second) : airport;
        }

        public Flight(DateTime departureDateTime, DateTime arrivalDateTime, string departureAirport,
            string arrivalAirport, string flightNumber, string company, string className, string classCode,
            string baggage, int idBron, bool isOk, string errorCode, DateTime dateOf, string seller,
            int paymentState, int status)
        {
            DepartureDateTime = departureDateTime;
            ArrivalDateTime = arrivalDateTime;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            FlightNumber = flightNumber;
            Company = company;
            ClassName = className;
            ClassCode = classCode;
            ClassFullName = GetFullName();
            Baggage = baggage;
            _idBron = idBron;
            IsOk = isOk;
            ErrorCode = errorCode;
            DateOf = dateOf;
            Seller = seller;
            PaymentState = paymentState;
            Status = status;
        }
        
    }
}
