using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.Flight;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class ArrivalsButtonViewModel : ViewModelBase
    {
        private ArrivalsViewModel _arrivalsViewModel;
        public ArrivalsViewModel ArrivalsViewModel
        {
            get { return _arrivalsViewModel; }
            set { SetValue(ref _arrivalsViewModel, value); }
        }

        public ArrivalsButtonViewModel()
        {
            ArrivalsViewModel = new ArrivalsViewModel();
            ArrivalsViewModel.ThereAndBackFlights = GetFlightsThereAndBack();
            ArrivalsViewModel.ThereFlights = GetFlightsThere();
        }

        private ObservableCollection<FlightInfo> GetFlightsThereAndBack()
        {
            ObservableCollection<FlightInfo> flightInfoCollection = new ObservableCollection<FlightInfo>();

            flightInfoCollection.Add(new FlightInfo("М - Барселона - М", 21000, 40000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Рим - М", 17000, 35000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Милан - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Генуя - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Венеция - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Амстердам - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Копенгаген - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Нью-йорк - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Майами - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Пунта-Кана - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Гавана - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Сингапур - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Гонконг - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Сидней - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Рио-де-Жанейро - М", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Буэнос Айрес - М", 18700, 33000, 1, 1));

            return flightInfoCollection;
        }

        private ObservableCollection<FlightInfo> GetFlightsThere()
        {
            ObservableCollection<FlightInfo> flightInfoCollection = new ObservableCollection<FlightInfo>();

            flightInfoCollection.Add(new FlightInfo("М - Барселона", 21000, 40000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Рим", 17000, 35000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Милан", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Генуя", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Венеция", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Амстердам", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Копенгаген", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Нью-йорк", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Майами", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Пунта-Кана", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Гавана", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Сингапур", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Гонконг", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Сидней", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Рио-де-Жанейро", 18700, 33000, 1, 1));
            flightInfoCollection.Add(new FlightInfo("М - Буэнос Айрес", 18700, 33000, 1, 1));

            return flightInfoCollection;
        }
    }
}
