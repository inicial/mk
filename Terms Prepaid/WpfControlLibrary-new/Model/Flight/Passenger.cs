using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Common;

namespace WpfControlLibrary.Model.Flight
{
    public class Passenger : Person
    {
        private string _ticket;
        public string Ticket
        {
            get { return _ticket; }
            set
            {
                SetValue(ref _ticket, value);
                TicketVisibility = _ticket.Equals("-") ? "Collapsed" : "Visible";
            }
        }

        private string _ticketVisibility;
        public string TicketVisibility
        {
            get { return _ticketVisibility; }
            set { SetValue(ref _ticketVisibility, value); }
        }

        public Passenger(string firstName, string secondName, string middleName, string ticket)
        {
            FirstName = firstName;
            SecondName = secondName;
            MiddleName = middleName;
            Ticket = ticket;
        }
    }
}
