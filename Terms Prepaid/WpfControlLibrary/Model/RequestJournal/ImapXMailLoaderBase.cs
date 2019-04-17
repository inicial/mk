using System;
using System.Collections.Generic;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Базовый класс загрузчика новых писем
    /// </summary>
    public abstract class ImapXMailLoaderBase : IDisposable
    {
        protected readonly string STATUS = "\r\n)\r\nIMAPX";
        protected ImapX.ImapClient _client { get; set; }

        public bool GetClient(IMailConfig config)
        {
            List<RequestMessageThread> mThreads = null;

            var client = new ImapX.ImapClient(config.Host);
            
            if (!client.Connect()) throw new Exception("ImapX connect error");
                
            if (client.Login(config.Address, config.Password)) _client = client;

            else throw new Exception("ImapX login error");
            
            return ClientIsOk();
        }

        public bool ClientIsOk()
        {
            return _client != null && _client.IsConnected && _client.IsAuthenticated;
        }
        
        public void Dispose()
        {
            if (!ClientIsOk()) return;

            _client.Disconnect();
            _client.Dispose();
            _client = null;
        }
    }
}