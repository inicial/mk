using System;
using System.Collections.Generic;
using ImapX.Enums;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class ImapXRequestMessageLoader : ImapXRequestMessageLoaderBase
    {
        /// <summary>
        /// Get all new messages
        /// </summary>
        /// <returns></returns>
        public override List<RequestMessageThread> GetMessages()
        {
            if (!ClientIsOk())
                throw new Exception("Почтовый клиент недоступен");

            return GetThreads(_client);
        }

        private List<RequestMessageThread> GetThreads(ImapX.ImapClient client)
        {
            client.Behavior.MessageFetchMode = MessageFetchMode.Full;
            client.Folders.Inbox.Messages.Download();

            return null;//client.Folders.Inbox.Messages.Select(msg => AttachThread(client, msg)).ToList();
        }
    }
}