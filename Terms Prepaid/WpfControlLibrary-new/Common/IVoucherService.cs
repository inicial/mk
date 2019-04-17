using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.Common
{
    public interface IVoucherService
    {
        void HotelOk2(int dlKey, bool isOk);
        void InshurOk(string dgCode, bool isOk);

        void AddCruiseOption(int dlKey, string description, string optionNumber, string cabinNumber, string cabinDef,
            string category, DateTime dt, bool isBook, bool documentQuery, bool documentGet);

        void CruiseBonusesAndServicesSet(int dlKey, int[] id);
        void CruiseBonusesAndServicesReset(int dlKey, int[] id);
        void CruiseBonusesAndServicesChangeText(int dlKey, int id, string text);
        bool GetInshurCreatedStatus(int dlKey);
        void SetTransferInto(int dlKey, bool? giude, string guidePhone, string opNumber, DateTime? timeLimit);
        string GetAviaBron2(int idBron);
        string GetCruiseBrandName2(string brandCode);
        string GetRate2(string dgCode);
        string GetCabinClasses(int shipId, string cabinCategory);
        string GetOptionNumber(int dlKey);
        decimal GetCourse2(string rate);
        DataRow GetShipName(int clId, string shipCode);
        DataRow GetShipInfo2(int dlKey);
        DataRow GetVisaInfo2(int dlKey);
        DataRow GetTransferInfo(int dlKey);
        DataRow GetTransferTypeInfo(int transferId);
        DataRow GetVoucherInfo(string dgCode);
        DataRow GetDogovorSettings(string dgcode);
        DataTable GetAviaTable2(int aviaBronID);
        DataTable GetAviaTurist2(int idBron);
        DataTable GetItinerary2(string dgcode);
        DataTable GetProblemServicesForDogovor(string dgcode = null);
        DataTable GetDogovorList(string dgcode);
        DataTable GetServicesSettings(string dgCode);
        DataTable GetTransactions(string dgCode);
        DataTable GetUralsibInshurs(string dgCode);
        DataTable GetInshurs(string dgCode);
        DataTable GetCruiseLinesByBrandCode(string brandCode);
        DataTable GetSpecialsForCruise(int dlKey);
        DataTable GetBonusesForCruise(int dlKey);
        DataTable GetServicesForCruise(int dlKey);
        DataTable GetDopServicesForCruise(int dlKey);
        DataTable GetBonusesAndServices(int dlKey);
        DataTable GetTouristsByDlKey(int dlKey);
        ServiceSetting GetServiceSetting(string dgCode, int dlKey);
    }
}
