using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using WpfControlLibrary.Common;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using NLog;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.Util;
using System.Diagnostics;

namespace WpfControlLibrary.Model.Flight
{
    public enum BronStatus
    {
        Created = 1, Paid = 2, CanseledNonPayment = 3
    }

    public class BookingAvia : Data
    {
        private const string _sellerErrorValue = "ERROR";
        private readonly TimeSpan _maxTimeOffset = new TimeSpan(0, 1, 0);

        public TimeSpan MaxWaitingTime = new TimeSpan(1, 0, 0, 0);

        private string _number;
        public string Number
        {
            get { return _number; }
            set { SetValue(ref _number, value);} 
        }

        private string _route;
        public string Route
        {
            get { return _route; }
            set { SetValue(ref _route, value); }
        }

        private string _duration;
        public string Duration
        {
            get { return _duration; }
            set { SetValue(ref _duration, value); }
        }

        private string _rules;
        public string Rules
        {
            get { return _rules; }
            set { SetValue(ref _rules, value); }
        }

        private DateTime _dateOf;
        public DateTime DateOf
        {
            get { return _dateOf; }
            set { SetValue(ref _dateOf, value); }
        }

        private DateTime _dateOf2;
        public DateTime DateOf2
        {
            get { return _dateOf2; }
            set { SetValue(ref _dateOf2, value); }
        }

        public int Status
        {
            get
            {
                if (_flightList != null && _flightList.Count > 0)
                {
                    return _flightList.First().Status;
                }
                else
                    return -1;
            }
        }

        public bool IsOk
        {
            get
            {
                if (_flightList != null && _flightList.Count > 0)
                {
                    return _flightList.First().IsOk;
                }
                else
                    return false;
            }
        }

        public int PaymentState
        {
            get
            {
                if (_flightList != null && _flightList.Count > 0)
                {
                    return _flightList.First().PaymentState;
                }
                else
                    return -1;
            }
        }

        private BronStatus _status2;
        public BronStatus Status2
        {
            get { return _status2; }
            set
            {
                StringBuilder sb = new StringBuilder();
                SetValue(ref _status2, value);
                switch (value)
                {
                    case BronStatus.Created:
                        sb.Append(string.Format("Создано {0}", DateOf.ToString(@"HH:mm \/ dd.MM.yy")));
                        //sb.AppendLine(string.Format("Актуально до {0:HH.mm/dd.MM.yy}", DateOf2));
                        StatusString2 = sb.ToString();
                        break;

                    case BronStatus.Paid:
                        sb.Append(string.Format("Оплачено клиентом"));
                        StatusString2 = sb.ToString();
                        break;

                    case BronStatus.CanseledNonPayment:
                        sb.Append(string.Format("Анулировано за неоплату"));
                        StatusString2 = sb.ToString();
                        break;
          
                    default:
                        StatusString2 = "";
                        break;
                }
            }
        }

        private string _statusString2;
        public string StatusString2
        {
            get { return _statusString2; }
            set { SetValue(ref _statusString2, value); }
        }

        public void SetStatus2()
        {
            if (IsOk)
                Status2 = BronStatus.Created;

            if (PaymentState == 1)
                Status2 = BronStatus.Paid;

            if (Status == 24)
                Status2 = BronStatus.CanseledNonPayment;
        }

        private ObservableCollection<Flight> _flightList;
        public ObservableCollection<Flight> FlightList
        {
            get { return _flightList; }
            set
            {
                SetValue(ref _flightList, value);
                SetDuration();
            } 
        }

        private ObservableCollection<Flight> _flightListThere;
        public ObservableCollection<Flight> FlightListThere
        {
            get { return _flightListThere; }
            set { SetValue(ref _flightListThere, value); }
        }

        private ObservableCollection<Flight> _flightListBack;
        public ObservableCollection<Flight> FlightListBack
        {
            get { return _flightListBack; }
            set { SetValue(ref _flightListBack, value); }
        }

        private ObservableCollection<Passenger> _passengerList;
        public ObservableCollection<Passenger> PassengerList
        {
            get { return _passengerList; }
            set { SetValue(ref _passengerList, value); } 
        }

        private bool _changesInFlightThere;
        public bool ChangesInFlightThere
        {
            get { return _changesInFlightThere; }
            set { SetValue(ref _changesInFlightThere, value); }
        }

        private bool _changesInFlightBack;
        public bool ChangesInFlightBack
        {
            get { return _changesInFlightBack; }
            set { SetValue(ref _changesInFlightBack, value); }
        }

        public BookingAvia(string number, DataTable flightTable, DataTable turistsTable)
        {
            Number = number;

            try
            {
                Rules = ParseRules(GetRules(flightTable));
            }
            catch (Exception e)
            {
                Rules = "";
                TpLogger.Error("Rules loading error", "Route loading error", e);
            }

            FlightList = GetAllAvia(flightTable);
            PassengerList = GetAllPassenger(turistsTable);

            SetWaitingTime(FlightList);
            SplitFlightListByDate();

            GetAdditionInfo();
            Route = GetRoute();
            DateOf2 = DateOf;
            SetStatus2();
        }

        public BookingAvia(string number, DataTable flightTable, DataTable turistsTable, string route)
        {
            Route = route;
            Number = number;

            try
            {
                Rules = ParseRules(GetRules(flightTable));
            }
            catch (Exception e)
            {
                Rules = "";
                TpLogger.Error("Rules loading error", "Route loading error", e);
            }

            FlightList = GetAllAvia(flightTable);
            PassengerList = GetAllPassenger(turistsTable);

            SetWaitingTime(FlightList);
            SplitFlightListByDate();

            GetAdditionInfo();
            DateOf2 = DateOf;
            SetStatus2();
        }

        private void GetAdditionInfo()
        {
            if (_flightList != null && _flightList.Count > 0)
            {
                DateOf = _flightList.First().DateOf;
            }
        }

        private void SetWaitingTime(ObservableCollection<Flight> flights)
        {
            if (flights != null)
            {
                Flight prev = null;
                for (int i = 0; i < flights.Count; i++)
                {
                    Flight cur = flights[i];
                    cur.WaitingTime = prev != null
                                          ? cur.DepartureDateTime.Subtract(prev.ArrivalDateTime)
                                          : new TimeSpan();
                    prev = cur;
                }
            }
        }

        public void SplitFlightListByAirports()
        {
            FlightListThere = new ObservableCollection<Flight>();
            FlightListBack = new ObservableCollection<Flight>();

            foreach (var flight in _flightList)
            {
                if (FlightListThere.FirstOrDefault(f => f.DepartureAirport.Equals(flight.ArrivalAirport)) == null)
                    FlightListThere.Add(flight);
                else
                    FlightListBack.Add(flight);
            }
            SetWaitingTime(FlightListThere);
            SetWaitingTime(FlightListBack);
        }

        public void SplitFlightListByDate()
        {
            FlightListThere = new ObservableCollection<Flight>();
            FlightListBack = new ObservableCollection<Flight>();

            bool there = true;

            foreach (var flight in _flightList)
            {
                if (there && flight.WaitingTime > MaxWaitingTime)
                    there = false;

                if (there)
                    FlightListThere.Add(flight);
                else
                    FlightListBack.Add(flight);
                    
            }

            SetWaitingTime(FlightListThere);
            SetWaitingTime(FlightListBack);
        }

        private string GetCity(string airport)
        {
            int b = airport.IndexOf('(');
            int e = airport.IndexOf(')');
            if (b > -1 && e > b)
                return airport.Substring(b + 1, e - b - 1);
            else
                return airport;
        }

        public string GetRoute()
        {
            StringBuilder route = new StringBuilder();

            foreach (var flight in FlightList)
            {
                route.Append(GetCity(flight.DepartureAirport));
                route.Append("-");
            }

            try
            {
                route.Append(GetCity(FlightList.Last().ArrivalAirport));
            }
            catch(Exception e)
            {
                TpLogger.Error("BookingAvia.GetRoute", string.Format("aviaBronID={0}", ""), e);
            }

            return route.ToString();
        }

        private void SetDuration()
        {
            if (FlightList != null && FlightList.Count > 0)
                SetDuration(FlightList.First().DepartureDateTime, FlightList.Last().ArrivalDateTime);
        }

        private void SetDuration(DateTime begin, DateTime end)
        {
            if (begin.Date.Equals(end.Date))
                Duration = begin.ToString("dd.MM.yy");

            Duration = String.Format("{0} - {1}", begin.ToString("dd.MM.yy"), end.ToString("dd.MM.yy"));
        }

        public bool ErrorBronTest(TimeSpan maxTimeOffset, out Flight errorFlight)
        {
            //var flight = FlightList.FirstOrDefault(f => !f.IsOk || f.Seller.Equals(_sellerErrorValue, StringComparison.OrdinalIgnoreCase));

            if (FlightList != null && FlightList.Count > 0)
            {
                var flight = FlightList.Last();
                if (flight != null)
                {
                    //!flight.IsOk ||
                    if (flight.Seller.Equals(_sellerErrorValue, StringComparison.OrdinalIgnoreCase)
                        && DateTime.Now.Subtract(maxTimeOffset) < flight.DateOf)
                    {
                        errorFlight = flight;
                        return true;
                    }
                }
            }
            errorFlight = null;
            return false;
        }

        private void GetAllFields(dynamic obj)
        {
            foreach (dynamic entries in obj)
            {
                foreach (var entry in entries)
                {
                    if (entry.Name != null && entry.Value != null)
                    {
                        string name = entry.Name;
                        dynamic value = entry.Value;
                        string valueStr = value.ToString();
                        Debug.WriteLine("Name: {0}, Value: {1}", name, valueStr);
                    }
                    GetAllFields(entry);
                }
            }
        }

        private void JsonParse(string json)
        {
            dynamic parsedObject = JsonConvert.DeserializeObject(json);
            GetAllFields(parsedObject);
        }

        private string ParseRules(string jsonRules)
        {
            if (jsonRules != null)
            {
                var rules = (Rules) JsonConvert.DeserializeObject(jsonRules, typeof (Rules));

                //return rules.CANCELLATION_POLICE.fares[0].content;

                StringBuilder sb = new StringBuilder(rules.CANCELLATION_POLICE.fares[0].content);

                for (int index = 0; index < rules.CANCELLATION_POLICE.fares.Length; index++)
                {
                    var fare = rules.CANCELLATION_POLICE.fares[index];
                    sb.AppendLine("");
                    sb.AppendLine("------------------------------------------------------------------------------------------------------");
                    sb.AppendLine("");
                    sb.Append(fare.content);
                }
                return sb.ToString();
            }
            return "";
        }

        /// <summary>
        /// Полчить список авиаперелетов
        /// </summary>
        /// <param name="flightTable"></param>
        /// <returns></returns>
        public static ObservableCollection<Flight> GetAllAvia(DataTable flightTable)
        {
            ObservableCollection<Flight> flightList = new ObservableCollection<Flight>();

            foreach (DataRow row in flightTable.Rows)
            {
                string departureStr = string.Format("{0}({1})", row.Field<string>("CountryFrom"), row.Field<string>("CityFrom"));
                string arrivalStr = string.Format("{0}({1})", row.Field<string>("CountryTo"), row.Field<string>("CityTo"));
                string flightNumber = row.Field<string>("reis");
                string company = row.Field<string>("AL_NAME");
                DateTime dateFrom = row.Field<DateTime>("date_from");

                DateTime dateTo = row.Field<DateTime>("date_to");
                string baggage = row.Field<string>("baggage");
                string className = row.Field<string>("className");
                string classCode = row.Field<string>("classCode");
                int idBron = row.Field<int>("id_bron");
                bool isOk = row.Field<bool?>("is_ok") ?? false;
                string errorCode = row.Field<string>("error_code");
                string seller = row.Field<string>("n_bron_seller");
                int paymentState = row.Field<int>("payment_state");
                int status = row.Field<int>("DL_CONTROL");

                DateTime dateOf = row.Field<DateTime>("date_of");
                
                int id = row.Field<int>("reisId");

                flightList.Add(new Flight(dateFrom, dateTo, departureStr, arrivalStr, flightNumber,
                    company, className, classCode, baggage, idBron, isOk, errorCode, dateOf, seller,
                        paymentState, status));
            }
            
            return flightList;
        }

        public static String GetRules(DataTable flightTable)
        {
            if (flightTable.Rows != null && flightTable.Rows.Count > 0)
                return flightTable.Rows[0].Field<string>("textDetail");
            
            return null;
        }

        /// <summary>
        /// Получить список пассажиров
        /// </summary>
        /// <param name="turistsTable"></param>
        /// <returns></returns>
        public static ObservableCollection<Passenger> GetAllPassenger(DataTable turistsTable)
        {
            ObservableCollection<Passenger> passengerList = new ObservableCollection<Passenger>();

            foreach (DataRow row in turistsTable.Rows)
            {
                string secondName = row.Field<string>("NAME");
                string firstName = row.Field<string>("FNAME");
                string middleName = row.Field<string>("SNAME");
                string ticket = row.Field<string>("TICKET");

                passengerList.Add(new Passenger(firstName, secondName, middleName, ticket));
            }

            return passengerList;
        }
    }
}
