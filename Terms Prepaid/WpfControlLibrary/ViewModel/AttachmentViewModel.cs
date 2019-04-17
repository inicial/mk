using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class AttachmentViewModel : ViewModelBase
    {
        public delegate void AttachmentActionDelegate(AttachmentViewModel attachment);

        private AttachmentActionDelegate _removeAttachmentHandler;
        private AttachmentActionDelegate _saveAttachmentHandler;
        private AttachmentActionDelegate _openAttachmentHandler;

        private RequestAttachment _attachment;
        public RequestAttachment Attachment
        {
            get { return _attachment; }
            set
            {
                SetValue(ref _attachment, value);
                Size = Attachment != null && Attachment.Data != null ? GetSize(Attachment.Data) : "0";
            }
        }

        private string _size;
        public string Size
        {
            get { return _size; }
            set { SetValue(ref _size, value); }
        }

        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand OpenCommand { get; set; }

        public AttachmentViewModel(RequestAttachment attachment, AttachmentActionDelegate removeAttachmentHandler, AttachmentActionDelegate saveAttachmentHandler,
            AttachmentActionDelegate openAttachmentHandler)
        {
            Attachment = attachment;
            _removeAttachmentHandler = removeAttachmentHandler;
            _saveAttachmentHandler = saveAttachmentHandler;
            _openAttachmentHandler = openAttachmentHandler;

            RemoveCommand = new RelayCommand(
                () => { if (_removeAttachmentHandler != null) _removeAttachmentHandler.Invoke(this); });

            SaveCommand = new RelayCommand(
                () => { if (_saveAttachmentHandler != null) _saveAttachmentHandler.Invoke(this); });

            OpenCommand = new RelayCommand(
                () => { if (_saveAttachmentHandler != null) _openAttachmentHandler.Invoke(this); });
        }

        private string GetSize(byte[] data)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = data.Length;
            int order = 0;
            while (len >= 1024 && ++order < sizes.Length)
            {
                len = len / 1024;
            }

            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }

    }
}
