using System;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Send messages interface
    /// </summary>
    public interface IRequestJournalSender
    {
        IMailConfig Config { get; }
        DateTime SendMessage(RequestMessage message, bool isHtml, bool deliveryNotification);
    }
}