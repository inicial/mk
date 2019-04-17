using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using WpfControlLibrary.Buttons;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.Common
{
    public static class WpfAdapter
    {
        public static IButton AttachButton(ElementHost host, Type type, MyControlEventHandlerSample callback)
        {
            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            object instance = ctor.Invoke(null);
            IButton control = instance as IButton;
            control.OnButtonClick += callback;
            host.Child = control as System.Windows.UIElement;
            return control;
        }

        public static IButton AttachButton(ElementHost host, IButton button, MyControlEventHandlerSample callback)
        {
            button.OnButtonClick += callback;
            host.Child = button as System.Windows.UIElement;
            return button;
        }

        public static void AttachButtonWithViewModel(ElementHost host, IView view, IButton viewModel, MyControlEventHandlerSample callback)
        {
            view.DataContext = viewModel;
            viewModel.OnButtonClick += callback;
            host.Child = view as UIElement;
        }

        public static IMenu AttachMenu(ElementHost host, Type type, MenuItemClickEventHandler callback)
        {
            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            object instance = ctor.Invoke(null);
            IMenu control = instance as IMenu;
            control.OnMenuItemClick += callback;
            host.Child = control as System.Windows.UIElement;
            return control;
        }

        public static ServiceTabViewModel AttachServiceTab(ElementHost host, IServiceView serviceView, ServiceViewModel serviceModel, 
            ServiceTabViewModel.SelectedEventHandler callback, string tabName = null)
        {
            var serviceTabView = new ServiceTabView();
            ServiceTabViewModel serviceTabViewModel = new ServiceTabViewModel(serviceView, serviceModel, callback, tabName);
            serviceTabView.DataContext = serviceTabViewModel;
            serviceTabView.InitializeComponent();
            host.Child = serviceTabView;
            return serviceTabViewModel;
        }

        public static DogovorSettingViewModel AttachDogovorSetting(ElementHost host)
        {
            var view = new DogovorSettingView();
            var model = new DogovorSettingViewModel();
            view.DataContext = model;
            view.InitializeComponent();
            host.Child = view;
            return model;
        }

        public static void ShowRequestsJournal()
        {
            const int width = 600;
            const int height = 400;
            var view = new RequestsJournalHostView { Width = width, Height = height };
            var model = new RequestsJournalHostViewModel();
            view.DataContext = model;
            ElementHost.EnableModelessKeyboardInterop(view);

            model.Vm.ShowDataGrid();
            view.Show();
        }

        public static void ShowRequestsJournalDetail()
        {
            const int width = 600;
            const int height = 400;
            var view = new RequestDetailView() { Width = width, Height = height };
            ElementHost.EnableModelessKeyboardInterop(view);
            view.Show();
        }

        public static void Test(ElementHost host)
        {
            var view = new RequestMessageButton();
            var msg = new RequestMessage {HtmlWithHead = "Test"};
            view.DataContext = msg;
            host.Child = view;
        }
    }
}
