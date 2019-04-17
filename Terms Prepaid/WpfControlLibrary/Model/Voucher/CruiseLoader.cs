using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataService;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class CruiseLoader : ICruiseLoader
    {
        protected readonly IVoucherService VoucherService;

        public CruiseLoader()
        {
            VoucherService = Repository.GetInstance<IVoucherService>();
        }

        public IEnumerable<NameValue> GetDopServicesList()
        {
            var dt = VoucherService.GetDopServicesList();

            return dt != null ?
                dt.Select().Select(r => new NameValue(r.Field<int>("CDS_ID"), r.Field<string>("CDP_NAME"))) :
                null;
        }

        public IEnumerable<DopServiceItem> GetDopServiceItems()
        {
            var dt = VoucherService.GetDopServicesList();

            // DopServiceItem(int iID, string iName, string iQuery, int iDefault)
            return dt != null ?
                dt.Select().Select(r => new DopServiceItem(r.Field<int>("CDS_ID"), r.Field<string>("CDP_NAME"), r.Field<string>("CDP_QWERY"), r.Field<int>("CDP_IS_default"))) :
                null;
        }

        public IEnumerable<NameValue> GetCruiseLinesInfo(string brandCode)
        {
            var dt = VoucherService.GetCruiseLinesByBrandCode(brandCode);

            return dt != null ?
                dt.Select().Select(r => new NameValue(DataRowExtensions.Field<string>(r, "Parametr_name"), DataRowExtensions.Field<string>(r, "Parametr_value").Trim())) :
                null;
        }

        public IEnumerable<NameValue> GetDopServicesForCruise(int dlKey)
        {
            var dt = VoucherService.GetDopServicesForCruise(dlKey);

            return dt != null ?
                dt.Select().Select(r => new NameValue(r.Field<string>("CDP_NAME"), r.Field<string>("Text"))) : 
                null;
        }

        public IEnumerable<NameValue> GetServicesForCruise(int dlKey)
        {
            var dt = VoucherService.GetServicesForCruise(dlKey);

            return dt != null ?
                dt.Select().Select(r => new NameValue(r.Field<string>("CDP_NAME"), r.Field<string>("Text"))) :
                null;
        }

        public IEnumerable<string> GetSpecialsForCruise(int dlKey)
        {
            var i = 0;
            var dt = VoucherService.GetSpecialsForCruise(dlKey);

            return dt != null ? 
                dt.Select().Select(r => string.Format("{0}. {1}", ++i, r.Field<string>("Text").Replace("\\", ""))) : 
                null;
        }

        public IEnumerable<string> GetBonusesForCruise(int dlKey)
        {
            var i = 0;
            var dt = VoucherService.GetBonusesForCruise(dlKey);

            return dt != null ? 
                dt.Select().Select(r => string.Format("{0}. {1}", ++i, r.Field<string>("Text").Replace("\\", ""))) :
                null;
        }

        public IEnumerable<BonusAndService> GetBonusesAndServices(int dlKey)
        {
            var dt = VoucherService.GetBonusesAndServices(dlKey);
            return dt != null ? dt.Select().Select(GetBonusAndService) : null;
        }

        private BonusAndService GetBonusAndService(DataRow r)
        {
            return r.Field<string>("CDP_NAME") != null ? 
                new BonusAndService(r.Field<int>("actions_id"), r.Field<string>("Text"), r.Field<int>("isRight") == 1, r.Field<string>("CDP_NAME"), false, true, false) : 
                new BonusAndService(r.Field<int>("actions_id"), null, r.Field<int>("isRight") == 1, r.Field<string>("Text"), false, true, false);
        }

        public IEnumerable<BonusAndService> GetNoBonusesAndServices(int dlKey)
        {
            var dt = VoucherService.GetNoBonusesAndServices(dlKey);
            return dt != null ? dt.Select().Select(GetBonusAndService) : null;
        }

    }
}