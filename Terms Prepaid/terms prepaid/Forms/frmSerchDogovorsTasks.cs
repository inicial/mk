using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;


namespace terms_prepaid.Forms
{
    public delegate void SelectTaskCallback(string dg_code, int open_code);


    public partial class frmSerchDogovorsTasks : Form
    {
        public int StartPosX = 0;  // начальные координаты для открытия формы
        public int StartPosY = 0;

        private List<OptionTask> TaskList;
        private TaskListControl Tasker;
        private TaskerViewModel TaskerModel;

        public SelectTaskCallback SelectTaskFunc;

                
        public frmSerchDogovorsTasks(List<OptionTask> iTasks)
        {
            InitializeComponent();

            TaskList = iTasks;

            Tasker = new TaskListControl();
            Tasker.SelectTask = ListSelectTask;
            TaskerModel = new TaskerViewModel();
            TaskerModel.TaskerForm = this;
            TaskerModel.SourceTaskList = iTasks;
            TaskerModel.InitTaskList();
            Tasker.DataContext = TaskerModel;
            TaskListHost.Child = Tasker;
        }

        private void frmSerchDogovorsTasks_Load(object sender, EventArgs e)
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            int space = 0;
            if (StartPosX + this.Width + space > width) StartPosX = width - this.Width - space;
            if (StartPosX < 0) StartPosX = 0;
            if (StartPosY + this.Height + space > height) StartPosY = height - this.Height - space;
            if (StartPosY < 0) StartPosY = 0;
            this.Left = StartPosX;
            this.Top = StartPosY;

            int count = 5;
            if (TaskList != null) count = TaskList.Count;
            if (count < 5) count = 5;
            if (count > 15) count = 15;
            this.Height = count * 21 + 46;
        }

        private void frmSerchDogovorsTasks_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ListSelectTask(int TaskNum)
        {
            if (TaskList == null) return;

            string DgCode = "";
            int open_code = 0;
            for (int i = 0; i < TaskList.Count; i++)
            {
                OptionTask task = TaskList[i];
                if (task.Num == TaskNum)
                {
                    DgCode = task.DgCode;
                    open_code = task.OpenCode;
                    break;
                }
            }
            if (string.IsNullOrEmpty(DgCode)) return;

            this.Close();

            if (SelectTaskFunc == null) return;

            SelectTaskFunc(DgCode, open_code);
        }
    }
}
