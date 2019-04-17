using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.Helpers
{
    public static class RequestJournalHelper
    {
        public struct NewRequestMessagesInfo
        {
            public int NewRequestCount;
            public int NewClientMessages;
            public int NewClientMessagesSelf;
            public int NewManagerMessages;
            public int NewManagerMessagesSelf;

            public NewRequestMessagesInfo(int newRequestCount, int newClientMessages, int newClientMessagesSelf, int newManagerMessages, int newManagerMessagesSelf)
                : this()
            {
                NewRequestCount = newRequestCount;
                NewClientMessages = newClientMessages;
                NewClientMessagesSelf = newClientMessagesSelf;
                NewManagerMessages = newManagerMessages;
                NewManagerMessagesSelf = newManagerMessagesSelf;
            }

            public int GetTotal()
            {
                return NewRequestCount + NewClientMessages + NewManagerMessages;
            }
        }


        public static NewRequestMessagesInfo GetNewMessagesCount()
        {
            var usKey = Repository.GetInstance<IUsersService>().GetUsKey();
            var uskeysUnseenMessages = Repository.GetInstance<IUnseenRequestMessageService>().GetUnseenMessages();
            var unseenMessages = uskeysUnseenMessages as int?[] ?? uskeysUnseenMessages.ToArray();

            var messages = Repository.GetInstance<IUnseenRequestMessageService>().GetUnseenMessages2()
                .Select()
                .Select( s =>
                        new
                        {
                            RequestStatusId = s.Field<int>("RequestStatusId"),
                            US_KEY = s.Field<int?>("US_KEY"),
                            Mod = s.Field<string>("Mod")
                        })
                .ToArray();

            return
                new NewRequestMessagesInfo
                    (
                        messages.Count(m => m.US_KEY == null),
                        messages.Count(m => m.Mod.Equals(RequestMessageMod.MTM)),
                        messages.Count(m => m.Mod.Equals(RequestMessageMod.MTM) && m.US_KEY == usKey),
                        messages.Count(m => m.Mod.Equals(RequestMessageMod.MTC)),
                        messages.Count(m => m.Mod.Equals(RequestMessageMod.MTC) && m.US_KEY == usKey) 
                    );
        }

        public static NewRequestMessagesInfo GetNewRequestsStatus()
        {
            var usKey = Repository.GetInstance<IUsersService>().GetUsKey();
            var uskeysUnseenMessages = Repository.GetInstance<IUnseenRequestMessageService>().GetUnseenMessages();
            var unseenMessages = uskeysUnseenMessages as int?[] ?? uskeysUnseenMessages.ToArray();

            var messages = Repository.GetInstance<IUnseenRequestMessageService>().GetUnseenMessages2()
                .Select()
                .Select(s =>
                        new
                        {
                            RequestStatusId = s.Field<int>("RequestStatusId"),
                            US_KEY = s.Field<int?>("US_KEY"),
                            Mod = s.Field<string>("Mod")
                        })
                .ToArray();

            return
                new NewRequestMessagesInfo
                    (
                        messages.Count(m => m.US_KEY == null),
                        messages.Count(m => m.Mod.Equals(RequestMessageMod.MTM)),
                        messages.Count(m => m.Mod.Equals(RequestMessageMod.MTM) && m.US_KEY == usKey),
                        messages.Count(m => m.Mod.Equals(RequestMessageMod.MTC)),
                        messages.Count(m => m.Mod.Equals(RequestMessageMod.MTC) && m.US_KEY == usKey)
                    );
        }
    }
}
