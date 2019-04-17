using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.Common
{
    public interface IContractService
    {
        void DogovorRecalc(string dgCode);
        void SetAnnulateDogovor(string dgcode, int reason);
        void UpdateStatusDogovor(string dgcode, int status);
        void UpdateDiscountDogovor(string dgcode, decimal discount);
        void UpdateDogovorSetting(string dgcode, bool prepaydtype, decimal prepayd, object ppaymentdate, object paymentdate);
        void UpdateDogovorLineDiscountSetting(string dgCode, int dlKey, bool discountType, decimal discount);
        decimal CalculateDogovorLineDiscountSetting(string dgCode, int dlKey, decimal discount);
        void UpdateDogovorLinePaymentSetting(string dgCode, int dlKey, bool prepaydType, decimal ppaymentValue, object ppaymentdate,
            object paymentdate, string handWhy);
        void UpdateDogovorLineStatusSetting(string dgCode, int dlKey, int status, string handWhy);
        DataRow GetDogovorSettings(string dgcode);
        DateTime GetDateForStatus(string dgcode, string query);
        DateTime GetDateForStatus(string dgcode, int status);
        DataTable GetStatuses();
        ServiceSetting GetServiceSetting(string dgCode, int dlKey);
        DataTable GetStatusesForService(int svKey);
    }
}
