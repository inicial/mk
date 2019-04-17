using System;
using System.Collections.Generic;
using System.Linq;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.ViewModel
{
    public class CorrespondenceRequestViewModel : CorrespondenceRequestViewModelBase
    {
        public CorrespondenceRequestViewModel(User user, Request request, CorrespondenceType type, Action updateCallback, Action closeCorrespondenceCallback, IRequestJournalService service = null, IRequestJournalSender sender = null) :
            base(user, request, type, updateCallback, closeCorrespondenceCallback, service, sender)
        {
            Destination.IsEditable = false;
        }

        public override void UpdateCorrespondence()
        {
            Corresp = new RequestCorrespondenceHtml(Request, CorrespondenceType.Client);

            if (Request == null || Request.Messages == null)
                return;

            var lastMsg = Request.Messages.LastOrDefault(m => m.Mod == RequestMessageMod.MTC);
            if (lastMsg != null)
                Destination.Subject = lastMsg.Theme;

            var firstMsg = Request.Messages.LastOrDefault(m => m.Mod == RequestMessageMod.MTC);
            if (firstMsg != null)
                Destination.SelectedContact = new KeyValuePair<string, string>(firstMsg.SenderAddress, firstMsg.SenderAddress); 
        }

        protected override void Send(string message)
        {
            if(Request == null || Request.FirstMessage == null) throw new Exception("Ошибка ответа на запрос: сообщение запроса отсутствует");

            var msg = RequestMessageBuilder.GetRequestMessageHtml(User, Request, Request.Messages.LastOrDefault(m => m.IsIncomming && m.Mod == RequestMessageMod.MTC), 
                message, RequestMessageMod.MTC, Attachments.Select(a => a.Attachment));

            Send(msg);
        }
    }
}