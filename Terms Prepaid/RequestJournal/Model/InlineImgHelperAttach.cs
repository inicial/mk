using System;
using System.Net.Mail;
using System.Net.Mime;
using MailKit;
using WpfControlLibrary.Helpers;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class InlineImgHelperAttach : InlineImgHelper
    {
        public override void GetImage(MailMessage msg, string src)
        {
            System.Net.Mime.ContentType contentType = FileHelper.GetContenetType(src);
            if (contentType == null)
                return;

            try
            {
                msg.AlternateViews.Add(GetEmbeddedImage(src, contentType));
            }
            catch (Exception e)
            {
                throw new MessageNotFoundException(string.Format("Image not attached src={0}", src));
            }
        }

        private AlternateView GetEmbeddedImage(String filePath, System.Net.Mime.ContentType contentType)
        {
            LinkedResource inline = new LinkedResource(filePath) { ContentId = Guid.NewGuid().ToString() };
            inline.ContentType = contentType;
            string htmlBody = @"<img src='cid:" + inline.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);
            return alternateView;
        }
    }
}