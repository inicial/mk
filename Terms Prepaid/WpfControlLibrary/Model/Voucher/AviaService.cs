using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Flight;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Model.Voucher
{
    public class AviaService : Service
    {
        public static int Index = 0;

        private int? _aviaStatusValue;
        public int? AviaStatusValue
        {
            get { return _aviaStatusValue; }
            set { SetValue(ref _aviaStatusValue, value); }
        }

        private string _aviaStatusName;
        public string AviaStatusName
        {
            get { return _aviaStatusName; }
            set { SetValue(ref _aviaStatusName, value); }
        }

        private DateTime? _aviaDateOfChange;
        public DateTime? AviaDateOfChange
        {
            get { return _aviaDateOfChange; }
            set { SetValue(ref _aviaDateOfChange, value); }
        }

        private string _addAviaStatusName;
        public string AddAviaStatusName
        {
            get { return _addAviaStatusName; }
            set { SetValue(ref _addAviaStatusName, value); }
        }

        private DateTime? _addAviaDateOfChange;
        public DateTime? AddAviaDateOfChange
        {
            get { return _addAviaDateOfChange; }
            set { SetValue(ref _addAviaDateOfChange, value); }
        }

        private string _addAviaStatusName2;
        public string AddAviaStatusName2
        {
            get { return _addAviaStatusName2; }
            set { SetValue(ref _addAviaStatusName2, value); }
        }

        private DateTime? _addAviaDateOfChange2;
        public DateTime? AddAviaDateOfChange2
        {
            get { return _addAviaDateOfChange2; }
            set { SetValue(ref _addAviaDateOfChange2, value); }
        }

        private string _route;
        public string Route
        {
            get { return _route; }
            set { SetValue(ref _route, value); }
        }

        private bool _thereAreChanges;
        public bool ThereAreChanges
        {
            get { return _thereAreChanges; }
            set { SetValue(ref _thereAreChanges, value); }
        }

        public BookingAvia BookingAviaOld { get; set; }

        public BookingAvia BookingAviaNew { get; set; }

        private BookingAvia _bookingAvia;
        public BookingAvia BookingAvia
        {
            get { return _bookingAvia; }
            set { SetValue(ref _bookingAvia, value); }
        }

        public DateTime? GetLastAviaDateOfStatusChange()
        {
            return AddAviaDateOfChange2 ?? (AddAviaDateOfChange ?? AviaDateOfChange);
        }

        public string GetLastAviaStatus()
        {
            return AddAviaStatusName2 ?? (AddAviaStatusName ?? AviaStatusName);
        }

        public AviaService(Service source)
            : base(source)
        {
            Index++;

            string bookingId = VoucherService.GetAviaBron2(BronId);
            DataTable aviaTbl = VoucherService.GetAviaTable2(BronId);
            DataTable turists = VoucherService.GetAviaTurist2(BronId);

            BookingAvia bookingAvia = new BookingAvia(bookingId, aviaTbl, turists, "");
            bookingAvia.Route = bookingAvia.GetRoute();
            BookingAvia = bookingAvia;
            GetFullName(Index);

            AviaStatusValue = DataRow.Field<int?>("StatusValue");
            AviaStatusName = DataRow.Field<string>("StatusName");
            AviaDateOfChange = DataRow.Field<DateTime?>("DateOfChange");
            AddAviaStatusName = DataRow.Field<string>("AddStatusName");
            AddAviaDateOfChange = DataRow.Field<DateTime?>("AddDateOfChange");
            AddAviaStatusName2 = DataRow.Field<string>("AddStatusName2");
            AddAviaDateOfChange2 = DataRow.Field<DateTime?>("AddDateOfChange2");
        }

        public sealed override void GetInfo()
        {
            Name2 = "А/П ";

            if (BookingAvia != null && BookingAvia.FlightList != null && BookingAvia.FlightList.Count > 0)
            {
                DateBeginString = BookingAvia.FlightList.First().DepartureDateTime.ToString("dd.MM.yy");
                DateEndString = BookingAvia.FlightList.Last().ArrivalDateTime.ToString("dd.MM.yy");
            }
        }

        public sealed override void GetFullName(int number)
        {
            GetInfo();
            FullName = String.Format("№{0}  {1}    {2} - {3}", number, Name2, DateBeginString, DateEndString);
            RowIndex = number - 1;
        }
    }
}
