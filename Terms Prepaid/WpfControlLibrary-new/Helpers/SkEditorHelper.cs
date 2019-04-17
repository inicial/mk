using System;
using System.Reflection;
using System.Runtime.InteropServices.Expando;
using mshtml;

namespace WpfControlLibrary.Helpers
{
    public class SkEditorHelper
    {
        public SkEditorHelper()
        {
        }

        public string GetContent(HTMLDocument doc)
        {
            var win = doc != null ? doc.parentWindow as HTMLWindow2 : null;

            if (win == null) 
                return null;

            win.execScript("saveContent();");

            PropertyInfo property;
            
            try
            {
                property = ((IExpando)win).GetProperty("content", BindingFlags.Default);
            }
            catch(Exception e)
            {
                return null;
            }

            return property != null ? (string)property.GetValue(win, null) : null;

            //var content = document.getElementById(elementId);
            //return content == null ? null : content.outerHTML;
        }

        public void SetContent(HTMLDocument doc, string html)
        {
            var win = doc != null ? doc.parentWindow as HTMLWindow2 : null;

            if (win == null)
                return;

            if (html == null) html = "";

            win.execScript(string.Format("setContent('{0}');", html));
        }

        public void InsertHtml(HTMLDocument doc, string html)
        {
            var win = doc != null ? doc.parentWindow as HTMLWindow2 : null;

            if (win == null)
                return;

            if (html == null) html = "";

            win.execScript(string.Format("insertHtml('{0}');", html));
        }
    }
}