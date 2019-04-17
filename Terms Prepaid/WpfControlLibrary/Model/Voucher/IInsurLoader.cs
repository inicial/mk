using System.Collections.Generic;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public interface IInsurLoader
    {
        IEnumerable<NameValue> GetUralsibData(string dgCode);
        IEnumerable<TouristForInshur> GetTourists(string dgCode);
        bool GetCreatedStatus(int dlkey);
    }
}