using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using Request = WpfControlLibrary.Model.RequestJournal.Request;

namespace WpfControlLibrary.Common
{
    public interface IRequestJournalService
    {
        void WathMessage(int messageId);
        void WathMessages(int requestId, string mod);
        void InsertRequestMessageToHistory(string text, string mod, int requestId);
        void SetTracking(int id, DateTime date);
        void SetSent(int id, DateTime date);
        void SuperviserChecked(int id, int usKey);
        void CloseCorrespondence(int id, int usKey);
        void OpenCorrespondence(int id, int usKey);
        int? GetAnnulateStatusId();
        void AnnulateRequest(int requestId, int userId, int subStatusId);
        void AddAttachment(int requestMessageId, string contentType, string name, byte[] data);
        void AddRequestStatus(int requestId, int statusId, int? userId, DateTime dateTime);
        void SetRequestSubStatus(int requestId, int subStatusId, int? userId, DateTime dateTime);
        void MakeReservation(int requestId, string dgCode, int? userId, DateTime dateTime);
        void ChangeSenderAddress(int messageId, string newAddress);
        bool RequestIdIsExists(int id);
        int GetMaxMessageId();
        int GetRequestByMessageId(int messageId);
        int AddMessage(int messageId, int? requestId, string text, DateTime date, string senderAddress, string reseiverAddress, bool seen, DateTime? readDate,
            string theme, bool isIncoming, int inReplyToId, bool reply, string html, string mod, int? usKey);
        ulong GetMaxTimeStamp();
        int GetProblemRequestCount(int? uskey);
        int GetRequestCount(DateTime? dateBegin, DateTime? dateEnd, int? usKey, bool showReserved, bool showAnnulate);
        DataRow GetAnnulateStatus();
        DataTable GetCancelationReasons();
        DataTable GetRequestsIdForReservation();
        DataTable GetRequestsIdForReservation(int usKey);
        DataTable GetAllRequests(bool cancellate, bool reserved, ulong timeStamp);
        DataTable GetRequests(DateTime? dateBegin, DateTime? dateEnd, string senderAddress, int? usKey, int[] problems, int[] statuses, bool senderAddressContains);
        DataTable GetAllSenders(bool cancellate, bool reserved);
        DataTable GetAllStatuses();
        DataTable GetProblemSubStatuses();
        DataTable GetRequestProblems();
        DataTable GetProblemRequests();
        DataTable GetMessages(int reuestId);
        DataTable GetAttachments(int requestMessageId);
        DataTable GetRequestCorrespondence(int requestId);
        DataTable GetRequestStatusHistory(int requestId);
        DataTable GetRequestStatusHistory2(int requestId);
        DataTable GetRequestSubStatusHistory2(int requestId);
        DataTable GetProblemRequestGroups(int? uskey);
        
    }
}
