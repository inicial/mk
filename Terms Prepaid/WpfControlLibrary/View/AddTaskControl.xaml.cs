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
    public delegate void on_list_selection_changed(int item_index);

    public class TaskRecord
    {
        public int TaskNum { get; set; }
        public string TaskName { get; set; }
        public bool TaskSelected { get; set; }
        public DateTime TaskDate { get; set; }
        public bool TaskReflect { get; set; }

        public bool TaskEmpty 
        { 
            get { return string.IsNullOrEmpty(TaskName); } 
        }

        private int _taskGroup;
        private string _taskColor;
        private string _taskHiColor;

        public int TaskGroup
        {
            get { return _taskGroup; }
            set
            {
                _taskGroup = value;

                string color_code = "#F0F8FF";
                string hicolor_code = "#F0F8FF";
                if (_taskGroup == 1) { color_code = "#D6F8FF"; hicolor_code = "#E6FCFF"; }
                if (_taskGroup == 2) { color_code = "#E0FFD5"; hicolor_code = "#F0FFE5"; }
                if (_taskGroup == 3) { color_code = "#FFFFE0"; hicolor_code = "#FFFFF0"; }
                if (_taskGroup == 4) { color_code = "#FFC4C7"; hicolor_code = "#FFCACD"; }
                if (_taskGroup == 5) { color_code = "#D6F8FF"; hicolor_code = "#E6FCFF"; }
                if (_taskGroup == 6) { color_code = "#E0FFD5"; hicolor_code = "#F0FFE5"; }
                if (_taskGroup == 7) { color_code = "#FFFFE0"; hicolor_code = "#FFFFF0"; }
                if (_taskGroup == 8) { color_code = "#FFFFE0"; hicolor_code = "#FFFFF0"; }
                if (_taskGroup == 9) { color_code = "#D6F8FF"; hicolor_code = "#E6FCFF"; }
                if (_taskGroup == 10) { color_code = "#FFC4C7"; hicolor_code = "#FFCACD"; }

                TaskColor = color_code;
                TaskHiColor = hicolor_code;
            }
        }

        public string TaskColor
        {
            get { return _taskColor; }
            set { _taskColor = value; }
        }

        public string TaskHiColor
        {
            get { return _taskHiColor; }
            set { _taskHiColor = value; }
        }

        //public override string ToString()
        //{
        //    return TaskName;
        //}

    }

    public partial class AddTaskControl : UserControl
    {
        public List<TaskRecord> TaskListObject;

        public RoutedEventHandler GotFocusCallback;
        public RoutedEventHandler LostFocusCallback;
        //public SelectionChangedEventHandler SelectionCallback;
        public on_list_selection_changed SelectionCallback;

        public int MouseTaskNum;
        public int SelectedTaskNum;


        public AddTaskControl(List<TaskRecord> TaskList, on_list_selection_changed iSelectionCallback)
        {
            InitializeComponent();

            TaskListView.GotFocus += OnGotFocus;
            TaskListView.LostFocus += OnLostFocus;
            TaskListView.SelectionChanged += OnSelectionChanged;
            //TaskListView.MouseDown += OnMouseDown;
            //TaskListView.MouseUp += OnMouseUp;

            TaskListObject = TaskList;
            ReflectList();

            SelectionCallback = iSelectionCallback;
        }

        public void ReflectList()
        {
            TaskListView.ItemsSource = null;
            TaskListView.ItemsSource = TaskListObject;
        }

        public void FocusList()
        {
            TaskListView.Focus();
        }

        public int GetSelectedIndex()
        {
            return TaskListView.SelectedIndex;
        }

        public string GetSelectedItem()
        {
            string text = "";
            if (TaskListView.SelectedIndex >= 0)
            {
                TaskRecord task = (TaskRecord)TaskListView.SelectedItem;
                text = task.TaskName;
            }
            return text;
        }
        
        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (GotFocusCallback != null) GotFocusCallback(sender, e);
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (LostFocusCallback != null) LostFocusCallback(sender, e);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = "";
            if (TaskListView.SelectedIndex >= 0)
            {
                TaskRecord task = (TaskRecord)TaskListView.SelectedItem;
                text = task.TaskName;
            }

            if (SelectionCallback != null) SelectionCallback(TaskListView.SelectedIndex);
        }

        public void OnMouseDown(object sender, RoutedEventArgs e)
        {
            MouseTaskNum = 0;
            if (sender.GetType() != typeof(TextBlock)) return;

            TextBlock block = (TextBlock)sender;
            string tag = block.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) MouseTaskNum = int.Parse(tag);
        }

        public void OnMouseUp(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(TextBlock)) return;

            TextBlock block = (TextBlock)sender;
            int num = 0;
            string tag = block.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) num = int.Parse(tag);

            if (num == MouseTaskNum) SelectTask(num);
        }

        public void Task_MouseDown(object sender, EventArgs e)
        {
            MouseTaskNum = 0;

            if (sender.GetType() != typeof(Grid)) return;

            Grid grid = (Grid)sender;
            string tag = grid.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) MouseTaskNum = int.Parse(tag);
        }

        public void Task_MouseUp(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(Grid)) return;
            if (MouseTaskNum == 0) return;

            Grid grid = (Grid)sender;
            int num = 0;
            string tag = grid.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) num = int.Parse(tag);

            if (num == MouseTaskNum) SelectTask(num);
        }

        public void chkTaskReflect_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ClearSelection()
        {
            for (int i = 0; i < TaskListObject.Count; i++)
            {
                TaskRecord task = TaskListObject[i];
                if (task.TaskNum == SelectedTaskNum)
                {
                    if (task.TaskSelected) task.TaskSelected = false;
                    if (task.TaskReflect) task.TaskReflect = false;
                }
            }
            SelectedTaskNum = 0;

            ReflectList();
        }

        public void SelectTask(int select_num)
        {
            SelectedTaskNum = 0;
            int selected_index = -1;

            for (int i = 0; i < TaskListObject.Count; i++)
            {
                TaskRecord task = TaskListObject[i];
                if (task.TaskNum == select_num)
                {
                    if (!string.IsNullOrEmpty(task.TaskName))
                    {
                        task.TaskSelected = true;
                        SelectedTaskNum = select_num;
                        selected_index = i;
                        break;
                    }
                    else
                        return;
                }
            }

            for (int i = 0; i < TaskListObject.Count; i++)
            {
                TaskRecord task = TaskListObject[i];
                if (task.TaskNum != select_num)
                {
                    if (task.TaskSelected) task.TaskSelected = false;
                    if (task.TaskReflect) task.TaskReflect = false;
                }
            }

            if (SelectionCallback != null) SelectionCallback(selected_index);

            ReflectList();
        }

    }
}
