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


namespace WpfControlLibrary.View
{
    public delegate void SelectTaskCallback(int TaskNim);


    public partial class TaskListControl : UserControl
    {
        private int MouseTaskNum;

        public SelectTaskCallback SelectTask;

        public TaskListControl()
        {
            InitializeComponent();
        }

        public void Task_MouseDown(object sender, EventArgs e)
        {
            MouseTaskNum = 0;

            if (sender.GetType() != typeof(System.Windows.Controls.Grid)) return;

            System.Windows.Controls.Grid grid = (System.Windows.Controls.Grid)sender;
            string tag = grid.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) MouseTaskNum = int.Parse(tag);
        }

        public void Task_MouseUp(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(System.Windows.Controls.Grid)) return;
            if (MouseTaskNum == 0) return;

            System.Windows.Controls.Grid grid = (System.Windows.Controls.Grid)sender;
            string tag = grid.Tag.ToString();
            int num = 0;
            if (!string.IsNullOrEmpty(tag)) num = int.Parse(tag);
            if (num != MouseTaskNum) return;

            if (SelectTask == null) return;

            SelectTask(num);
        }
    }
}
