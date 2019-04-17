using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Helpers;

namespace WpfControlLibrary.Common
{ 
    public interface IRequestJournalButtonViewModel : IButton
    {
        void Update();
        void SetCallback(MyControlEventHandlerSample buttonClickHandler);
        void SetCallback2(Action<int> buttonClickHandler);
    }
}
