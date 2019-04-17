using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;

namespace DataService
{
    public class Msg
    {
        private static Messenger _instance;

        private Msg()
        {

        }

        public static Messenger GetInstance()
        {
            return _instance ?? (_instance = new Messenger());
        }

        public void Register<TMessage>(object recipient, Action<TMessage> action)
        {
            Messenger.Default.Register(recipient, action);
        }

        public void Send<TMessage>(TMessage message)
        {
            Messenger.Default.Send(message);
        }
    }
}
