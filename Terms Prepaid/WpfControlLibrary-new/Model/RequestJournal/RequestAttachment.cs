using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestAttachment : Data
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetValue(ref _id, value); }
        }

        private int _requestAttachmentTypeId;
        public int RequestAttachmentTypeId
        {
            get { return _requestAttachmentTypeId; }
            set { SetValue(ref _requestAttachmentTypeId, value); }
        }

        private int _requestMessageId;
        public int RequestMessageId
        {
            get { return _requestMessageId; }
            set { SetValue(ref _requestMessageId, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        private byte[] _data;
        public byte[] Data
        {
            get { return _data; }
            set { SetValue(ref _data, value); }
        }

        private string _extension;
        public string Extension
        {
            get { return _extension; }
            set { SetValue(ref _extension, value); }
        }

        private string _contentId;
        public string ContentId
        {
            get { return _contentId; }
            set { SetValue(ref _contentId, value); }
        }

        private string _contentTypeName;
        public string ContentTypeName
        {
            get { return _contentTypeName; }
            set { SetValue(ref _contentTypeName, value); }
        }

        private System.Net.Mime.ContentType _contentType;
        public System.Net.Mime.ContentType ContentType
        {
            get { return _contentType; }
            set { SetValue(ref _contentType, value); }
        }
    }
}