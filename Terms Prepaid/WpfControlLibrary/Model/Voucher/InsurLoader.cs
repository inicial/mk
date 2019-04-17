using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataService;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class InsurLoader : IInsurLoader
    {
        IVoucherService _serv;

        public InsurLoader()
        {
            _serv = Repository.GetInstance<IVoucherService>();
        }

        public IEnumerable<NameValue> GetUralsibData(string dgCode)
        {
            return _serv.GetUralsibInshurs(dgCode)
                .Select()
                .Select(r => new NameValue(DataRowExtensions.Field<string>(r, "INS_Numder"), DataRowExtensions.Field<bool>(r, "INS_Status") ? "Выписана" : "Аннулирована"));
        }

        public IEnumerable<TouristForInshur> GetTourists(string dgCode)
        {
            return _serv.GetInshurs(dgCode).Select().Select(r => new TouristForInshur()
            {
                FirstName = r.Field<string>("TU_FNAMELAT"),
                SecondName = r.Field<string>("TU_NAMELAT"),
                InshurNumber = r.Field<string>("INS_Numder")
            });
        }

        public bool GetCreatedStatus(int dlkey)
        {
            return _serv.GetInshurCreatedStatus(dlkey);
        }
    }
}