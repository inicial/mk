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

namespace WpfControlLibrary.View
{
    public delegate void CloseButtonCallback();

    public partial class CorrespondenceControl : UserControl
    {
        public CloseButtonCallback CloseButtonClick;

        public CorrespondenceControl()
        {
            InitializeComponent();
        }

        public void CloseButton_Click(object sender, EventArgs e)
        {
            if (CloseButtonClick != null) CloseButtonClick();
        }
    }
}
