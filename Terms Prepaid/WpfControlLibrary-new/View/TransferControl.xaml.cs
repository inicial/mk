﻿using System;
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
    /// Interaction logic for TransferControl.xaml
    /// </summary>
    public partial class TransferControl : UserControl, ITransferControl
    {
        public TransferControl()
        {
            InitializeComponent();
        }

        public void ScrollToTop()
        {
            //throw new NotImplementedException();
        }
    }
}
