using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for ProblemRequestsView.xaml
    /// </summary>
    public partial class ProblemRequestsView : SimpleWindow, IProblemRequestsView
    {
        private static IProblemRequestsView _instance;

        public ProblemRequestsView()
        {
            InitializeComponent();
        }

        public static IProblemRequestsView GetInstance()
        {
            return _instance ?? (_instance = new ProblemRequestsView());
        }

        private void SimpleWindow_SourceInitialized(object sender, EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
