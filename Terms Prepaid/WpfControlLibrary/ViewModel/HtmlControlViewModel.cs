using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public interface IHtmlControlViewModel
    {
        string HtmlToDisplay { get; set; }
        Action HtmlChangedHandler { get; set; }
    }

    public class HtmlControlViewModel : ViewModelBase, IHtmlControlViewModel
    {
        private string _htmlToDisplay;
        public string HtmlToDisplay
        {
            get { return _htmlToDisplay; }
            set
            {
                SetValue(ref _htmlToDisplay, value);
                if(HtmlChangedHandler != null)
                    HtmlChangedHandler.Invoke();
            }
        }
        
        private Action _htmlChangedHandler;
        public Action HtmlChangedHandler
        {
            get { return _htmlChangedHandler; }
            set
            {
                if (_htmlChangedHandler == value)
                    return;

                _htmlChangedHandler = value;
                HtmlChangedHandler.Invoke();
            }
        }
    }
}
