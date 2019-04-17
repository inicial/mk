using System;
using System.Data;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestMessagInfoFactory
    {
        public RequestMessageInfo GetMtMInfo(DataRow row)
        {
            return Get(row.Field<string>("MtMSenderAddress"),
                row.Field<bool?>("MtMIsIncomming"),
                row.Field<DateTime?>("MtMDate"),
                row.Field<DateTime?>("MtMTracking"),
                row.Field<DateTime?>("MtMSent"),
                row.Field<DateTime?>("MtMReadDate"),
                RequestMessageMod.MTM
                );
        }

        public RequestMessageInfo GetMtCInfo(DataRow row)
        {
            return Get(row.Field<string>("MtCSenderAddress"),
                row.Field<bool?>("MtCIsIncomming"),
                row.Field<DateTime?>("MtCDate"),
                row.Field<DateTime?>("MtCTracking"),
                row.Field<DateTime?>("MtCSent"),
                row.Field<DateTime?>("MtCReadDate"),
                RequestMessageMod.MTC);
        }

        public RequestMessageInfo Get(string senderAddress, bool? isIncoming, DateTime? date, DateTime? tracking, DateTime? sent, DateTime? readDate, string mod)
        {
            if(isIncoming == null || date == null)
                return null;

            return new RequestMessageInfo()
            {
                SenderAddress = senderAddress,
                IsIncomming = (bool) isIncoming,
                Date = (DateTime) date,
                Tracking = tracking,
                Sent = sent,
                ReadDate = readDate,
                Mod = mod
            };
        }
    }
}