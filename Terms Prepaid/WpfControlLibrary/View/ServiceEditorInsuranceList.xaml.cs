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
    /// <summary>
    /// Interaction logic for ServiceEditorInsuranceList.xaml
    /// </summary>
    public partial class ServiceEditorInsuranceList : Window
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;


        public ServiceEditorInsuranceList ( int iX, int iY )
        {
            InitializeComponent();

            StartPosX = iX;
            StartPosY = iY;

            this.Loaded += Window_Load;
            this.Deactivated += Window_Deactivate;
        }

        private void Window_Load(object sender, EventArgs e)
        {
            this.Left = StartPosX;
            this.Top = StartPosY;

            //Accord_List();
        }


        private void Window_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
