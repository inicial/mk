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
    public delegate bool CloseTaskCallback(int TaskID);

    public partial class TaskerControl : UserControl
    {
        public CloseTaskCallback CloseTask;

        public TaskerControl()
        {
            InitializeComponent();
        }

        public void chkTaskClose_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(System.Windows.Controls.CheckBox)) return;
            if (CloseTask == null) return;

            System.Windows.Controls.CheckBox checkbox = (System.Windows.Controls.CheckBox)sender;
            if (!(bool)checkbox.IsChecked) return;
            string tag = checkbox.Tag.ToString();
            int id = 0;
            if (!string.IsNullOrEmpty(tag)) id = int.Parse(tag);

            if (!CloseTask(id)) checkbox.IsChecked = false;
        }


    }
}
