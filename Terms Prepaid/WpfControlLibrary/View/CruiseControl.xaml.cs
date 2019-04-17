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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary.Common;
using WpfControlLibrary.ViewModel;
using System.Globalization;


namespace WpfControlLibrary.View
{
    public delegate bool ConfirmCallback(string message);

    public class TextBoxStyleSelector : IValueConverter
    {
        public Style DefaultStyle { get; set; }
        public Style ChangedStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return (bool)value ? ChangedStyle : DefaultStyle;

            return DefaultStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Interaction logic for CruiseControl.xaml
    /// </summary>
    public partial class CruiseControl : UserControl, ICruiseControl
    {
        private readonly double[] _rowHeight = {5.0, 3.0, 3.0};
        private RowDefinition[] _rows;

        public event MyControlEventHandlerSample OnButtonClick_Passengers;
        public event MyControlEventHandlerSample OnButtonClick_Prices;
        public event MyControlEventHandlerSample OnButtonClick_Bill;
        public event MyControlEventHandlerSample OnButtonClick_Bonuses;
        public event MyControlEventHandlerSample OnButtonClick_Services;
        public event MyControlEventHandlerSample OnButtonClick_Food;
        public event MyControlEventHandlerSample OnButtonClick_Passport1;
        public event MyControlEventHandlerSample OnButtonClick_Passport2;
        //public event MyControlEventHandlerSample OnButtonClick_Service;
        public event System.Windows.RoutedEventHandler OnButtonClick_Service;
        //public event System.Windows.RoutedEventHandler OnGotFocus_Service;

        public ConfirmCallback UndoConfirmFunction;

        //private System.Windows.Controls.TextBlock _Tourist_TextBlock;
        //private System.Windows.Controls.TextBox _Tourist_TextBox;
        //private System.Windows.Controls.ListView _TouristListView;
        //private System.Windows.Controls.Border _Tourist_Border;
        //private System.Windows.Controls.TextBlock _Space3;
        //private System.Windows.Controls.TextBlock _Space5;

        private bool TouristVisibleFlag = false;
        private bool ServicetVisibleFlag = false;

        //public CruiseControl(MyControlEventHandlerSample BonusesHandler)
        public CruiseControl()
        {
            //OnButtonClick_Bonuses = BonusesHandler;
            InitializeComponent();
            _rows = new [] {Row1, Row2, Row3};

        }

        public void ScrollToTop()
        {
            //throw new NotImplementedException();
        }

        public void ScrollToBottom()
        {
            //throw new NotImplementedException();
            //this.AutoScrollPosition = new Point(0, 200);

            CriuseViewer.ScrollToBottom();
        }

        private void CollapseRow(int i)
        {
            if (i < 0 || i >= _rows.Length || i >= _rowHeight.Length || _rows[i] == null)
                return;

            _rows[i].Height = new GridLength(_rowHeight[i], GridUnitType.Star);
        }

        private void ExpandRow(int i)
        {
            for (int j = 0; j < _rows.Length; j++)
                _rows[j].Height = i == j ? GridLength.Auto : new GridLength(_rowHeight[j], GridUnitType.Star);
        }

        public void OnCheckOptionStatus_Checked(object sender, EventArgs e)
        {
            ScrollToBottom();
        }

        public void btnPassengers_Click(object sender, EventArgs e)
        {
            if (OnButtonClick_Passengers != null) OnButtonClick_Passengers(sender);

            TouristVisibleFlag = !TouristVisibleFlag;
            /*
            System.Windows.Visibility vis = System.Windows.Visibility.Collapsed;
            if (TouristVisibleFlag) vis = System.Windows.Visibility.Visible;

            if (_Tourist_TextBlock == null) _Tourist_TextBlock = (TextBlock)this.FindName("Tourist_TextBlock");
            if (_Tourist_TextBox == null) _Tourist_TextBox = (TextBox)this.FindName("Tourist_TextBox");
            if (_TouristListView == null) _TouristListView = (ListView)this.FindName("TouristListView");
            if (_Tourist_Border == null) _Tourist_Border = (Border)this.FindName("Tourist_Border");
            if (_Space3 == null) _Space3 = (TextBlock)this.FindName("Space3");
            if (_Space5 == null) _Space5 = (TextBlock)this.FindName("Space5");

            if (_Tourist_TextBlock != null) _Tourist_TextBlock.Visibility = vis;
            if (_Tourist_TextBox != null) _Tourist_TextBox.Visibility = vis;
            if (_TouristListView != null) _TouristListView.Visibility = vis;
            if (_Tourist_Border != null) _Tourist_Border.Visibility = vis;
            if (_Space3 != null) _Space3.Visibility = vis;
            if (_Space5 != null) _Space5.Visibility = vis;
            */
        }

        public void btnPrices_Click(object sender, EventArgs e)
        {
            if (OnButtonClick_Prices != null) OnButtonClick_Prices(sender);
        }

        public void btnBill_Click(object sender, EventArgs e)
        {
            if (OnButtonClick_Bill != null) OnButtonClick_Bill(sender);
        }

        public void btnBonuses_Click(object sender, EventArgs e)
        {
            if (OnButtonClick_Bonuses != null) OnButtonClick_Bonuses(sender);
        }

        public void btnServices_Click(object sender, EventArgs e)
        {
            if (OnButtonClick_Services != null) OnButtonClick_Services(sender);

            ServicetVisibleFlag = !ServicetVisibleFlag;

            if (ServicetVisibleFlag)
            {
                CriuseViewer.ScrollToBottom();
                //ScrollToBottom();
            }
        }

        public void btnFood_Click(object sender, EventArgs e)
        {
            if (OnButtonClick_Food != null) OnButtonClick_Food(sender);
        }

        public void btnPassport1_Click(object sender, EventArgs e)
        {
            if (OnButtonClick_Passport1 != null) OnButtonClick_Passport1(sender);
        }

        public void btnPassport2_Click(object sender, EventArgs e)
        {
            if (OnButtonClick_Passport2 != null) OnButtonClick_Passport2(sender);
        }

        public void btnChange_Click(object sender, EventArgs e)
        {
            if (this.DataContext.GetType() != typeof(CruiseViewModel)) return;

            CruiseViewModel vm = (CruiseViewModel)this.DataContext;
            vm.ChangeOption();

            //btnChange.Background = new SolidColorBrush(Color.FromArgb(255, 0, 207, 207));
            //btnChange.Background = new SolidColorBrush(Color.FromArgb(255, 106, 255, 214));
            btnChange.Background = new SolidColorBrush(Color.FromArgb(255, 0, 191, 250));
        }

        public void btnUndo_Click(object sender, EventArgs e)
        {
            if (this.DataContext.GetType() != typeof(CruiseViewModel)) return;

            CruiseViewModel vm = (CruiseViewModel)this.DataContext;
            if (!vm.NoBonusesAnServicesIsChanged()) return;
            if (UndoConfirmFunction != null)
            {
                //string msg = vm.GetChangesMessage("Отменить все изменения ?");
                string msg = "Отменить все изменения ?";
                if (!UndoConfirmFunction(msg)) return;
            }

            bool ChangeEnable = true;
            if (vm.CruiseInfo != null) ChangeEnable = vm.CruiseInfo.ChangeEnableFlag;

            vm.UndoOption();

            if (ChangeEnable) vm.CruiseInfo.ChangeEnableFlag = true;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            //CruiseService srv = (CruiseService)this.DataContext;

            //Style def_style = this.FindResource("tbDefault") as Style;
            //Style chd_style = this.FindResource("tbChanged") as Style;

            //tbx.Style = chd_style;
        }

        private void UIElement_OnMouseEnter1(object sender, MouseEventArgs e)
        {
            //ExpandRow(0);
        }

        private void UIElement_OnMouseLeave1(object sender, MouseEventArgs e)
        {
            //CollapseRow(0);
        }

        private void UIElement_OnMouseEnter2(object sender, MouseEventArgs e)
        {
            //ExpandRow(1);
        }

        private void UIElement_OnMouseLeave2(object sender, MouseEventArgs e)
        {
            //CollapseRow(1);
        }

        private void UIElement_OnMouseEnter3(object sender, MouseEventArgs e)
        {
            //ExpandRow(2);
        }

        private void UIElement_OnMouseLeave3(object sender, MouseEventArgs e)
        {
            //CollapseRow(2);
        }
    }
}
