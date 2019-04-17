using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Threading;

namespace WpfControlLibrary.Model.RequestJournal
{
    public abstract class ImapXRequestMessageLoaderBase : ImapXMailLoaderBase, IRequestJournalLoader
    {
        private Action _newMessageCallback { get; set; }

        /// <summary>
        /// Connect and authorize method
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool Connect(IMailConfig config)
        {
            GetClient(config);
            return _client != null && _client.IsConnected && _client.IsAuthenticated;
        }

        public new void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// Connect and authorize status
        /// </summary>
        /// <returns></returns>
        public new bool ClientIsOk()
        {
            return base.ClientIsOk();
        }

        public virtual List<RequestMessageThread> GetMessages()
        {
            throw new NotImplementedException();
        }

        public void RemoveMessages()
        {
            if (!ClientIsOk())
                throw new Exception("Почтовый клиент недоступен");

            foreach (var msg in _client.Folders.Inbox.Messages.ToArray())
                msg.MoveTo(_client.Folders.Trash);
        }

        /// <summary>
        /// Callback оповещений о новых входящих
        /// </summary>
        /// <param name="newMessageCallback"></param>
        public void SetNewMessageNotificationsCallback(Action newMessageCallback)
        {
            _newMessageCallback = newMessageCallback;

            if (ClientIsOk())
            {
                _client.Folders.Inbox.StartIdling();
                _client.Folders.Inbox.OnNewMessagesArrived +=
                    (sender, args) =>
                    {
                        if (_newMessageCallback != null) DispatcherHelper.CheckBeginInvokeOnUI(() => _newMessageCallback.Invoke());
                    };
            }
            else
            {
                throw new Exception("Почтовый клиент недоступен");
            }
        }
    }
}