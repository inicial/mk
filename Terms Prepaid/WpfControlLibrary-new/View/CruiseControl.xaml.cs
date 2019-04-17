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

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for CruiseControl.xaml
    /// </summary>
    public partial class CruiseControl : UserControl, ICruiseControl
    {
        private readonly double[] _rowHeight = {5.0, 3.0, 3.0};
        private RowDefinition[] _rows;

        public CruiseControl()
        {
            InitializeComponent();
            _rows = new [] {Row1, Row2, Row3};
        }

        public void ScrollToTop()
        {
            //throw new NotImplementedException();
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
