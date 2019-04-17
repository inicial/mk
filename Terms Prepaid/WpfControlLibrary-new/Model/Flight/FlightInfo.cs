using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WildberriesHomework.Util;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Flight
{
    public enum PriceType
    {
        From,
        Default
    }

    public class FlightInfo : Data
    {
        private event MyControlEventHandlerSample _onButtonClick;
        public event MyControlEventHandlerSample OnButtonClick
        {
            add { _onButtonClick += value; } 
            remove { _onButtonClick -= value; } 
        }

        private string _route;
        public string Route
        {
            get { return _route; }
            set { SetValue(ref _route, value); }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { SetValue(ref _price, value); }
        }

        private decimal _price2;
        public decimal Price2
        {
            get { return _price2; }
            set { SetValue(ref _price2, value); }
        }

        private int _currency;
        public int Currency
        {
            get { return _currency; }
            set { SetValue(ref _currency, value); }
        }

        private int _priceType;
        public int PriceType
        {
            get { return _priceType; }
            set { SetValue(ref _priceType, value); }
        }

        private ICommand _command;
        public ICommand Command
        {
            get
            {
                if (_command == null)
                {
                    _command = new RelayCommand(
                        p =>
                        {
                            if (_onButtonClick != null)
                                _onButtonClick(this);
                        });
                }
                return _command;
            }
        }

        public FlightInfo()
        {
            
        }

        public FlightInfo(string route, decimal price, decimal price2, int currency, int priceType)
        {
            Route = route;
            Price = price;
            Price2 = price2;
            Currency = currency;
            PriceType = priceType;
        }
    }
}
