using System;
using System.IO;
using System.Net.Mail;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class InlineImgHelperBase64 : InlineImgHelper
    {
        public override void GetImage(MailMessage msg, string src)
        {
            var stream = new FileStream(src, FileMode.Open);

            var extension = Path.GetExtension(src);
            var mimeType =  MimeTypeMap.List.MimeTypeMap.GetMimeType(extension);

            //AttachImageBase64(msg.Body, stream.ToString(), src, mimeType);
        }

        public string AttachImageBase64(string html, string stream, string oldSrc, System.Net.Mime.ContentType contentType)
        {
            var i = html.IndexOf(oldSrc, StringComparison.Ordinal);
            if (i == -1) throw new Exception(string.Format("AttachImageBase64 : ContentId={0} not found in html={1}", oldSrc, html));

            string oldStr = string.Format("src=\"cid:{0}", oldSrc);
            string newStr = string.Format("src=\"data:{0};base64,{1}", contentType, stream);
            return html.Replace(oldStr, newStr);
        }
    }
}