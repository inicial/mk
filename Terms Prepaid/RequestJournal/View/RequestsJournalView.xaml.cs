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

namespace WpfControlLibrary
{
	/// <summary>
	/// Interaction logic for RequestsJournalView.xaml
	/// </summary>
    public partial class RequestsJournalView : UserControl, IRequestJournalView
	{
	    private bool isManualEditCommit;

		public RequestsJournalView()
		{
			this.InitializeComponent();
		}

	    private void FrameworkElement_OnUnloaded(object sender, RoutedEventArgs e)
	    {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
	    }
	}
}