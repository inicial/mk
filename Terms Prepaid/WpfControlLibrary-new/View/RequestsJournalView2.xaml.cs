using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary
{
	/// <summary>
	/// Interaction logic for RequestsJournalView.xaml
	/// </summary>
    public partial class RequestsJournalView2 : UserControl, IRequestJournalView
	{
	    private bool isManualEditCommit;
	    private MailAddressHelper _mailAddressHelper;

		public RequestsJournalView2()
		{
			this.InitializeComponent();
            _mailAddressHelper = new MailAddressHelper();
		}

	    private void FrameworkElement_OnUnloaded(object sender, RoutedEventArgs e)
	    {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
	    }

	    private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
	    {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
	    }

	    private void Browser_OnNavigating(object sender, NavigatingCancelEventArgs e)
	    {
	        if (e.Uri == null) return;

	        var mail = _mailAddressHelper.GetMailUrl(e.Uri.AbsoluteUri);

	        if (mail != null)
	        {
	            var vm = DataContext as IRequestsJournalViewModel;
	            if (vm != null) vm.ChangeSenderAddress(mail);
	        }
	        else
	            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);

	        e.Cancel = true;
	    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
	}
}