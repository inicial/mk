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
        public delegate void MessageDelegate(RequestMessageViewModel requestMessageViewModel);

        protected MessageDelegate _replyHandler { get; set; }
        protected MessageDelegate _resubmitHandler { get; set; }
        protected MessageDelegate _commentHandler { get; set; }

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

        private bool _isComment;
        public bool IsComment
        {
            get { return _isComment; }
            set { SetValue(ref _isComment, value); }
        }

        public RelayCommand ReplyCommand { get; set; }
        public RelayCommand ResubmitCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand CommentCommand { get; set; }

        public RequestMessageViewModel(RequestMessage requestMessage, MessageDelegate replyHandler, MessageDelegate resubmitHandler, MessageDelegate commentHandler)
        {
            Attachments = new ObservableCollection<AttachmentViewModel>();
            Update(requestMessage);
            ReplyCommand = new RelayCommand(Reply);
            DeleteCommand = new RelayCommand(Delete, () => RequestMessage != null);
            ResubmitCommand = new RelayCommand(Resubmit); 
            CommentCommand = new RelayCommand(Comment);
            _replyHandler = replyHandler;
            _resubmitHandler = resubmitHandler;
            _commentHandler = commentHandler;
        }

        public void Update(RequestMessage requestMessage)
        {
            RequestMessage = requestMessage;

            if (RequestMessage == null) return;

            ButtonStyle = requestMessage.Mod.Equals(RequestMessageMod.MTM) ? 1 : 2;

            AttachmentsIsExist = requestMessage.Attachments != null && requestMessage.Attachments.Count > 0;

            IsComment = requestMessage.Mod == RequestMessageMod.COM;
            
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

        private void Resubmit()
        {
            if (_resubmitHandler != null)
                _resubmitHandler.Invoke(this);
        }

        private void Comment()
        {
            if(_commentHandler != null)
                _commentHandler.Invoke(this);
        }

        public void Delete()
        {

        }
    }

    public class RequestMessageViewModelMtC : RequestMessageViewModel
    {
        public RequestMessageViewModelMtC(RequestMessage requestMessage, MessageDelegate replyHandler, MessageDelegate resubmitHandler, MessageDelegate commentHandler)
            : base(requestMessage, replyHandler, resubmitHandler, commentHandler)
        {

        }
    }

    public class RequestMessageViewModelMtM : RequestMessageViewModel
    {
        public RequestMessageViewModelMtM(RequestMessage requestMessage, MessageDelegate replyHandler, MessageDelegate resubmitHandler, MessageDelegate commentHandler)
            : base(requestMessage, replyHandler, resubmitHandler, commentHandler)
        {

        }
    }
}
