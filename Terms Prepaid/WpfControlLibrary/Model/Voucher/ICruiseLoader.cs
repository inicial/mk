using System.Collections.Generic;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public interface ICruiseLoader
    {
        IEnumerable<NameValue> GetDopServicesList();
        IEnumerable<DopServiceItem> GetDopServiceItems();
        IEnumerable<string> GetSpecialsForCruise(int dlKey);
        IEnumerable<string> GetBonusesForCruise(int dlKey);
        IEnumerable<NameValue> GetCruiseLinesInfo(string brandCode);
        IEnumerable<NameValue> GetDopServicesForCruise(int dlKey);
        IEnumerable<NameValue> GetServicesForCruise(int dlKey);
        IEnumerable<BonusAndService> GetBonusesAndServices(int dlKey);
        IEnumerable<BonusAndService> GetNoBonusesAndServices(int dlKey);
    }
}