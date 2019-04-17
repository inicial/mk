using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{

    public interface IRequestMessageViewModel
    {
        bool Favorites { get; set; }
        bool Attachments { get; }
        RequestMessage RequestMessage { get; }
    }

    public class RequestMessageViewModel : ViewModelBase
    {
        public delegate void ReplyDelegate(RequestMessageViewModel requestMessageViewModel);

        protected ReplyDelegate _replyHandler { get; set; }

        private bool _favorites;
        public bool Favorites
        {
            get { return _favorites; }
            set { SetValue(ref _favorites, value); }
        }

        private bool _attachmentsIsExist;
        public bool AttachmentsIsExist
        {
            get { return _attachmentsIsExist; }
            set { SetValue(ref _attachmentsIsExist, value); }
        }

        private int _buttonStyle;
        public int ButtonStyle
        {
            get { return _buttonStyle; }
            set { SetValue(ref _buttonStyle, value); }
        }

        private RequestMessage _requestMessage;
        public RequestMessage RequestMessage
        {
            get { return _requestMessage; }
            set { SetValue(ref _requestMessage, value); }
        }

        private ObservableCollection<AttachmentViewModel> _attachments;
        public ObservableCollection<AttachmentViewModel> Attachments
        {
            get { return _attachments; }
            set { SetValue(ref _attachments, value); }
        }

        public RelayCommand ReplyCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public RequestMessageViewModel(RequestMessage requestMessage, ReplyDelegate replyHandler)
        {
            Attachments = new ObservableCollection<AttachmentViewModel>();
            Update(requestMessage);
            ReplyCommand = new RelayCommand(Reply);
            DeleteCommand = new RelayCommand(Delete, () => RequestMessage != null);
            _replyHandler = replyHandler;
        }

        public void Update(RequestMessage requestMessage)
        {
            RequestMessage = requestMessage;

            if (RequestMessage == null) return;

            ButtonStyle = requestMessage.Mod.Equals("MTM") ? 1 : 2;

            AttachmentsIsExist = requestMessage.Attachments != null && requestMessage.Attachments.Count > 0;
            
            if (requestMessage.Attachments != null)
                Attachments = new ObservableCollection<AttachmentViewModel>(requestMessage.Attachments.Select(a => new AttachmentViewModel(a, null, SaveAttachmentAs, OpenAttachment)));
        }

        private void SaveAttachmentAs(AttachmentViewModel attachment)
        {
            FileHelper.SaveToFileWithDialog(attachment.Attachment.Name, attachment.Attachment.Data);
        }

        private void OpenAttachment(AttachmentViewModel attachment)
        {
            FileHelper.OpenTemp(attachment.Attachment.Name, attachment.Attachment.Data);
        }

        private void Reply()
        {
            if (_replyHandler != null)
                _replyHandler.Invoke(this);
        }

        public void Delete()
        {

        }
    }

    public class RequestMessageViewModelMtC : RequestMessageViewModel
    {
        public RequestMessageViewModelMtC(RequestMessage requestMessage, ReplyDelegate replyHandler)
            : base(requestMessage, replyHandler)
        {

        }
    }

    public class RequestMessageViewModelMtM : RequestMessageViewModel
    {
        public RequestMessageViewModelMtM(RequestMessage requestMessage, ReplyDelegate replyHandler)
            : base(requestMessage, replyHandler)
        {

        }
    }
}
