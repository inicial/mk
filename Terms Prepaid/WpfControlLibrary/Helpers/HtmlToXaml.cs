using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace WpfControlLibrary.Helpers
{
    public class HtmlToXaml
    {
        public static RichTextBox Convert(string newXAML)
        {
            RichTextBox box = new RichTextBox();

            box.Document.Blocks.Clear();

            if (!string.IsNullOrEmpty(newXAML))
            {

                using (MemoryStream xamlMemoryStream = new MemoryStream(Encoding.ASCII.GetBytes(newXAML)))
                {

                    ParserContext parser = new ParserContext();
                    parser.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                    parser.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
                    FlowDocument doc = new FlowDocument();

                    Section section = XamlReader.Load(xamlMemoryStream, parser) as Section;
                    box.Document.Blocks.Add(section);
                }

            }

            return box;
        }

    }
}
