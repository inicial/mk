using System;
using System.Collections.Generic;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Интерфейс загрузчика новых писем
    /// </summary>
    public interface IRequestJournalLoader : IDisposable
    {
        bool Connect(IMailConfig config);
        void RemoveMessages();
        List<RequestMessageThread> GetMessages();
        void SetNewMessageNotificationsCallback(Action newMessageCallback);
        bool ClientIsOk();
    }
}