using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WpfControlLibrary.Common
{
    public interface IView
    {
        object DataContext { get; set; }
    }

    public interface IScrolledView : IView
    {
        void ScrollToTop();
    }

    public interface IVoucherTabView : IView { }
    public interface IServiceTabView : IView { }
    public interface IServiceView : IScrolledView { }

    public interface IFlightControl : IServiceView { }
    public interface IVisaControl : IServiceView { }
    public interface IHotelControl : IServiceView { }
    public interface IInshurControl : IServiceView { }
    public interface ITransferControl : IServiceView { }
    public interface IOtherServiceControl : IServiceView { }
    public interface ICruiseControl : IServiceView { }

    public interface IRequestJournalView : IView { }
}
