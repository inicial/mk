using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WpfControlLibrary.Helpers
{
    public class MailAddressHelper
    {
        private const string _pattern = @"(\S+@\S+[.]\S+)";
        private const string _patternMailto = @"mailto:(\S+@\S+[.]\S+)";
        private readonly string[] _trash = { "Fwd:", "Offline Message from" };

        public string[] GetMails(string text)
        {
            var mails = new List<string>();
            var regex = new Regex(_pattern);
            var match = regex.Match(text);

            while (match.Success)
            {
                mails.Add(match.Groups[1].Value);
                match = match.NextMatch();
            }

            return mails.ToArray();
        }

        public string GetMailUrl(string url)
        {
            var regex = new Regex(_patternMailto);
            var match = regex.Match(url);

            return match.Success ? match.Groups[1].Value : null;
        }

        public string RemoveTrash(string url)
        {
            return _trash.Aggregate(url, (r, n) => r.Replace(n, "")).TrimStart();
        }
    }
}
