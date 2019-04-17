using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfControlLibrary.Buttons;

namespace WpfControlLibrary.Common
{
    public class SimpleMenu : UserControl, IMenu
    {
        protected Dictionary<int, MenuItem> _menuItems;

        public event MenuItemClickEventHandler OnMenuItemClick;

        protected void AddMenuItem(MenuItem menuItem, Object tag)
        {
            menuItem.Tag = tag;
            _menuItems.Add((int)tag, menuItem);
        }

        public void SetEnabled(int menuIndex, bool state)
        {
            try
            {
                _menuItems[menuIndex].IsEnabled = state;
            }
            catch (Exception e)
            {

            }
        }

        public void SetVisibility(int menuIndex, bool state)
        {
            try
            {
                _menuItems[menuIndex].Visibility = state ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception e)
            {

            }
        }

        protected void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;

            if (OnMenuItemClick != null)
                OnMenuItemClick(this, Convert.ToInt32(item.Tag));
        }
    }
}
