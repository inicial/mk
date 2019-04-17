using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Data;
using FastColoredTextBoxNS;
using HtmlAgilityPack;
using Utilities.Reflection.Emit.Commands;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.Helpers
{
    public interface IHtmlWrapper
    {
        string Wrap(string body);
    }

    public class HtmlWrapper : IHtmlWrapper
    {
        public string Wrap(string body)
        {
            return string.Format("<!DOCTYPE html><HTML lang=\"ru\"><Body>{0}</Body></HTML>", body);
        }

        public string IncludeHeaderWithUtf8Charset(string htmlStr)
        {
            return IncludeHeaderWithUtf8CharsetUsingHAP(htmlStr);
        }

        private string IncludeHeaderWithUtf8CharsetUsingHAP(string htmlStr)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlStr);

            HtmlNode html = doc.DocumentNode.SelectSingleNode("/html");
            if (html == null)
            {
                htmlStr = string.Format("<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><body>{0}</body></html>", htmlStr);
                doc.LoadHtml(htmlStr);
                html = doc.DocumentNode.SelectSingleNode("/html");
            }

            HtmlNode body = doc.DocumentNode.SelectSingleNode("/html/body");

            HtmlNode head = doc.DocumentNode.SelectSingleNode("/html/head");
            if (head == null)
            {
                head = doc.CreateElement("head");
                html.InsertBefore(head, body);
            }
               
            HtmlNode meta = doc.CreateElement("meta");
            head.AppendChild(meta);
            meta.SetAttributeValue("charset", "utf-8");

            return doc.DocumentNode.OuterHtml;
        }
    }

    /// <summary>
    /// Хелпер, возвращающий переписку в виде html
    /// </summary>
    public class RequestCorrespondenceTableCreator
    {
        private readonly string _headers = "<tr id=\"header\"><td style=\"width:5%;\">Дата</td><td style=\"width:5%;\">Время</td>" +
                                           "<td style=\"width:80%;\">Сообщение</td><td style=\"width:5%;\">От</td><td style=\"width:5%;\">Автор</td></tr>";

        private readonly string _head = "<head><meta charset=\"utf-8\"/> <title></title> <style> table {border: 1px solid black;} " + "#header { background-color: gainsboro;} " +
                                        "td { margin-top: 5px; margin-bottom: 5px; margin-right: 5px; margin-left: 5px; vertical-align: top; border: 1px solid black;} " +
                                        "tr { color: black; } #superviser { color: SaddleBrown; } #manager { color: Blue; } #client { color: green; } #attachmentImage { border: 0; width: 24px; height: 24px; } </style> </head>";

        private static readonly string _attachmentImage =
            "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAHBh" +
            "aW50Lm5ldCA0LjAuMTJDBGvsAAADFElEQVRIS81Wz0sqURh9f5QIQYK0kHqapRC0yNqoPERciKlZqygqSYgW4VpoEaWUoCio/QGGZSsRMSj6pREEaWWk7+B3G8cZp3R4wTsr771zz/nu+c6d8Vfrh/HfC5RKpe" +
            "3t7aWlpdXV1ePj4/f3d7bwCfkCzWYzGo1OT0/Pzs7Oz8/bbDatVru4uFiv19kTbcgUAPvR0ZHRaNzY2Li4uAApcH5+Pjo6urW1hVX2nDwB7D88PJyYmAgEAg8PD2y2jVgsplar+ZNyBFC7Xq9fWVlBA9jUJ+7v" +
            "74eHh/P5PBvLEIjH48TucrlmZmbe3t7YQhvlclmj0ZydnbHxoALEvry87Ha78QNN5gvAup2dHcxfX1+zqf4FyPfx8XGwIzPEzg8MHjg4OECTd3d3+ap9CWBzJBIBO5zhahewh8NhsKPtuBCvr69soR8Bqh2k5P" +
            "sX7Jubm1arVaVS8Zv/jQDHDmecTqdOp8NQwL63twd2v99vt9sNBkM6ne73BNgMZzh2WISACtj39/fJGdxksGcyGT478JUAdRXOELvYGaqd2CcnJ1OplIAdkBTAnSR2+A5nxLVzXQU7TplMJsXsQG8BsGMP8uDx" +
            "ePBD7Duxw3eqHewvLy9suRtCAWwmZ9bW1vD6xbVEGwTslHdkhrra0xkOXQLYTHlfX1/HWwVEY2Njd3d3bLnbGWIXd1WALgFKJA4OdgxzuZxCobi6uqJVjp1qhzOCRPZERwB02IPaK5UKzeDGY8br9RYKhdvbW7" +
            "wDyPeeeZcCE0B1f9q4ubmhGcLl5eXc3NzvNmAdPiZSeZcCE3h6ehoZGUHSPz4+aIYDOlwsFk9OTk5PTx0Oh1TepcAEYAs+FNlsloZigJEiK5V3KTCB5+dnpVKJzTQUoFargf3rvEuhqwcWi0X8vwPsCwsL3+Zd" +
            "CkwAQFSGhoaCwSBf4/HxESkaqKsCdARwiEQigatrNptDoRDqxffPZDJNTU31mcie6AgA0MA5fD4f7EIckXe87BAewZd9IHQJEBqNRrVaxQ3AnUDzocoWZKGHwL/FDwu0Wn8BxFiPHxBue8cAAAAASUVORK5CYII=";
        
        public string GetHtml(MessageBlock[] blocks)
        {
            var sb = new StringBuilder();
            sb.Append("<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append(_head);
            sb.Append("<body>");
            sb.Append("<table style=\"width:100%;\">");
            sb.Append(_headers);
            
            foreach (var block in blocks)
                AppendMessageBlock(sb, block);

            sb.Append("</table>");

            sb.Append("</body></html>");

            return sb.ToString();
        }

        private void AppendMessageBlock(StringBuilder sb, MessageBlock block)
        {
            for (int i = 0; i < block.Messages.Count; i++)
            {
                var msg = block.Messages[i];

                string[] values = null;
                
                if (i > 0)
                    AppendRow(sb,
                        new[] { GetTime(msg.Date), AppendAttachments(msg), CorrespondenceDoc.GetAutorRole(msg), msg.Autor }, GetRowId(msg.Role),
                        block.Messages.Count);
                else
                    AppendRow(sb,
                        new[] { GetDate(msg.Date), GetTime(msg.Date), AppendAttachments(msg), CorrespondenceDoc.GetAutorRole(msg), msg.Autor }, GetRowId(msg.Role),
                        block.Messages.Count);
            }
        }

        private string AppendAttachments(Message msg)
        {
            if (msg.Attachments == null) return msg.Html;

            var sb = new StringBuilder();
            sb.Append("<br>");

            foreach (var a in msg.Attachments)
            {
                sb.Append(GetAttachment(a));
                sb.Append("  ");
            }

            int i = msg.Html.LastIndexOf("</body>", StringComparison.OrdinalIgnoreCase);

            if (i == -1) i = msg.Html.Length - 1;
            return msg.Html.Insert(i, sb.ToString());
        }

        private string GetAttachment(RequestAttachment a)
        {
            //return string.Format("<a href=\"attachment:{0}:{1}\">{3}{2}</a>", a.RequestMessageId, a.Id, a.Name, "<img alt=\"\" id=\"attachmentImage\"/>");
            return string.Format("<a href=\"attachment:{0}:{1}\"><img id=\"attachmentImage\" alt=\"\" src=\"{3}\"/>{2}</a>", a.RequestMessageId, a.Id, a.Name, _attachmentImage);
        }

        private string GetRowId(Message.Rolemember role)
        {
            switch (role)
            {
                case Message.Rolemember.Superviser:
                    return "superviser";

                case Message.Rolemember.Manager:
                    return "manager";

                case Message.Rolemember.Client:
                    return "client";

                default:
                    return null;
            }
        }

        private void AppendRow(StringBuilder sb, string[] cells, string rowId, int rowspan = 1)
        {
            sb.Append(rowId != null ? string.Format("<tr id=\"{0}\">", rowId) : "<tr>");

            for (int i = 0; i < cells.Length; i++)
            {
                if (i == 0 && rowspan > 1)
                    sb.Append(string.Format("<td rowspan=\"{0}\">", rowspan));
                else
                    sb.Append("<td>");

                sb.Append(cells[i]);
                sb.Append("</td>");
            }

            sb.Append("</tr>");
        }

        private string GetDate(DateTime dt)
        {
            return string.Format("{0:dd.MM.yy}", dt);
        }

        private string GetTime(DateTime dt)
        {
            return string.Format("{0:HH:mm}", dt);
        }
    }

    public class RequestCorrespondenceHtmlHelper
    {
        public IEnumerable<string> GetMessages(MessageBlock[] blocks)
        {
            if (blocks == null)
                return null;

            return blocks.Select(m =>
            {
                var firstOrDefault = m.Messages.FirstOrDefault();
                return firstOrDefault != null ? firstOrDefault.Html : null;
            });
        }

        public string GetAllCorrespondenceAsHtml(MessageBlock[] blocks)
        {
            return null;
        }

        public string HtmlParseTest(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            
            return null;
        }
    }

    public static class TextToHtml
    {
        public static string Convert(string text)
        {
            text = HttpUtility.HtmlEncode(text);
            text = text.Replace("\r\n", "\r");
            text = text.Replace("\n", "\r");
            text = text.Replace("\r", "<br>\r\n");
            text = text.Replace("  ", " &nbsp;");
            return text;
        }
    }

    public static class HtmlToText
    {
        public static string Convert(string source)
        {
            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                return source;
            }
        }
    }

    public class HtmlWrapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><body>{0}</body></html>", value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HtmlToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return HtmlToText.Convert(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
