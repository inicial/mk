using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using MailKit;
using WpfControlLibrary.Helpers;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class InlineImgHelper
    {
        private string _filePattern = "<IMG[^>]*src=\"file:///([^\"]*)";
        private string _base64pattern = "<IMG[^>]*src=\"data:([^\"]*);base64,([^\"]*)";

        private delegate LinkedResource GetLinkedResource(ref string body, Match match);

        public void AttachImages(MailMessage msg)
        {
            var body = msg.Body;

            var linkedResourceList = 
                GetAllLinkedResources(ref body, _base64pattern, GetLinkedResourceFromBase64);
            
            linkedResourceList.AddRange(
                GetAllLinkedResources(ref body, _filePattern, GetLinkedResourceFromFile));
            
            var alternateView = msg.AlternateViews.FirstOrDefault();
            if (alternateView == null)
            {
                alternateView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                msg.AlternateViews.Add(alternateView);
            }

            foreach (var r in linkedResourceList)
                alternateView.LinkedResources.Add(r);
        }
        
        private static List<LinkedResource> GetAllLinkedResources(ref string body, string pattern, GetLinkedResource func)
        {
            var linkedResourceList = new List<LinkedResource>();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(body);
            while (match.Success)
            {
                try
                {
                    linkedResourceList.Add(func(ref body, match));
                }
                catch (Exception e)
                {

                }
                match = match.NextMatch();
            }
            return linkedResourceList;
        }


        #region base64

        private LinkedResource GetLinkedResourceFromBase64(ref string body, Match match)
        {
            var mediaType = match.Groups[1].Value;
            var data = match.Groups[2].Value;
            var contentId = Guid.NewGuid().ToString();
            var oldSrc = string.Format("data:{0};base64,{1}", mediaType, data);
            var newSrc = string.Format("cid:{0}", contentId);
            body = body.Replace(oldSrc, newSrc);

            var bitmapData = Convert.FromBase64String(FixBase64ForImage(data));
            var streamBitmap = new System.IO.MemoryStream(bitmapData);
            return new LinkedResource(streamBitmap, mediaType) { ContentId = contentId };
        }

        public string FixBase64ForImage(string image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(image, image.Length);
            sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }

        #endregion


        #region file

        private LinkedResource GetLinkedResourceFromFile(ref string body, Match match)
        {
            var filePath = match.Groups[1].Value;
            var contentType = FileHelper.GetContenetType(filePath);
            
            if (contentType == null)
                return null;

            var oldSrc = string.Format("file:///{0}", filePath);
            var contentId = Guid.NewGuid().ToString();
            var newSrc = string.Format("cid:{0}", contentId);
            body = body.Replace(oldSrc, newSrc);

            return new LinkedResource(filePath)
            {
                ContentId = contentId,
                ContentType = contentType
            };
        }

        #endregion
    }
}