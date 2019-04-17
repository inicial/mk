using System;

namespace WpfControlLibrary.ViewModel
{
    public class RequestMessagesViewModelMtC : RequestMessagesViewModel
    {
        public RequestMessagesViewModelMtC(RequestNewMessageViewModel requestNewMessageViewModel, Action closeCallback)
            : base(requestNewMessageViewModel, closeCallback)
        {
            Mod = RequestMessageMod.MTC;
        }
    }
}