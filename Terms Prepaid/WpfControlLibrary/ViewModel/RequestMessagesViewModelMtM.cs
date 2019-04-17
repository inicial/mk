using System;

namespace WpfControlLibrary.ViewModel
{
    public class RequestMessagesViewModelMtM : RequestMessagesViewModel
    {
        public RequestMessagesViewModelMtM(RequestNewMessageViewModel requestNewMessageViewModel, Action closeCallback)
            : base(requestNewMessageViewModel, closeCallback)
        {
            Mod = RequestMessageMod.MTM;
        }
    }
}