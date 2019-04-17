using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace WpfControlLibrary.Model.RequestJournal
{
    public abstract class InlineImgHelper
    {
        private string pattern = "<IMG[^<,>]*src=\"([^\"]*)\"[^<,>]*>";

        public void AttachImages(MailMessage msg)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(msg.Body);
            while (match.Success)
            {
                var src = match.Groups[1].Value;
                Uri uri = new Uri(src);
                GetImage(msg, uri.LocalPath);
                match = match.NextMatch();
            }
        }

        public abstract void GetImage(MailMessage msg, string src);
    }
}