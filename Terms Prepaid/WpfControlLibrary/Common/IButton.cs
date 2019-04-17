using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary.Common
{
    public delegate void MyControlEventHandlerSample(object sender);

    public delegate void MenuItemClickEventHandler(object sender, int index);

    public interface IButton
    {
        event MyControlEventHandlerSample OnButtonClick;
    }

    public interface IMenu
    {
        event MenuItemClickEventHandler OnMenuItemClick;
        void SetEnabled(int menuIndex, bool state);
        void SetVisibility(int menuIndex, bool state);
    }
}
