using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;
using WpfControlLibrary.Model.Flight;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.ViewModel
{
    public class FlightViewModel : ServiceViewModel
    {
        public RelayCommand ChangeBooking { get; private set; }

        private bool _oldBooking;
        public bool OldBooking
        {
            get { return _oldBooking; }
            set { SetValue(ref _oldBooking, value); }
        }

        private BookingAvia _bookingAvia;
        public BookingAvia BookingAvia
        {
            get { return _bookingAvia; }
            set
            {
                SetValue(ref _bookingAvia, value);
                ScrollRulersButtonViewModel.Rulers = BookingAvia != null ? BookingAvia.Rules : "";
            }
        }

        private ScrollRulesButtonViewModel _scrollRulersButtonViewModel;
        public ScrollRulesButtonViewModel ScrollRulersButtonViewModel
        {
            get { return _scrollRulersButtonViewModel; }
            set { SetValue(ref _scrollRulersButtonViewModel, value); }
        }
        
        public FlightViewModel()
        {
            ScrollRulersButtonViewModel = new ScrollRulesButtonViewModel("");
            ChangeBooking = new RelayCommand(() => SetBookingAvia(!OldBooking), () => ((AviaService)Service).ThereAreChanges);
        }

        /*
        public FlightViewModel(IVoucherService serv)
            : this()
        {
            _serv = serv;
        }
        */

        public FlightViewModel(AviaService aviaService)
            : this()
        {
            Service = aviaService;
            LoadService();
        }

        /*public void GetBookingAvia(int bronId)
        {
            string bookingId = _serv.GetAviaBron2(bronId);
            DataTable aviaTbl = _serv.GetAviaTable2(bronId);
            DataTable turistsTbl = _serv.GetAviaTurist2(bronId);
            BookingAvia = new BookingAvia(bookingId, aviaTbl, turistsTbl);
        }*/

        public sealed override void LoadService()
        {
            BookingAvia = Service != null ? ((AviaService)Service).BookingAvia : null;
        }

        public void SetBookingAvia(bool old)
        {
            OldBooking = old;
            BookingAvia = !old ? ((AviaService) Service).BookingAviaNew : ((AviaService) Service).BookingAviaOld;
        }

    }
}
