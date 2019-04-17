using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.Forms.MessageBox;
using terms_prepaid.Helper_Classes;
using terms_prepaid.Forms;
using terms_prepaid.Helpers;
using System.Data.SqlClient;
//using lanta.SQLConfig;


namespace terms_prepaid
{
    public delegate void TaskerClosedCallback(bool UpdateFlag);
    public delegate void TaskerCloseTaskCallback(int TaskID);

    public partial class frmNewOptionsTasker : Form
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;

        private TaskerControl Tasker;
        private TaskerViewModel TaskerModel;

        public string DgCode = "";
        private bool ConfirmFlag;

        private List<OptionTask> TaskList;
        private List<OptionTask> SourceTaskList;
        private TaskerClosedCallback TaskerClosed;
        private TaskerCloseTaskCallback CloseTask;


        public frmNewOptionsTasker(int iX, int iY, string iDgCode, List<OptionTask> iTasks, TaskerClosedCallback iTaskerClosed, TaskerCloseTaskCallback iCloseTask)
        {
            StartPosX = iX;
            StartPosY = iY;

            DgCode = iDgCode;

            TaskerClosed = iTaskerClosed;
            CloseTask = iCloseTask;
            SourceTaskList = iTasks;
            //TaskList = iTasks;
            
            InitializeComponent();

            Tasker = new TaskerControl();
            Tasker.CloseTask = ListCloseTask;
            TaskerModel = new TaskerViewModel();
            TaskerModel.TaskerForm = this;
            //TaskerModel.SourceTaskList = iTasks;
            Tasker.DataContext = TaskerModel;
            TaskListHost.Child = Tasker;

            InitTaskList();
        }

        private void frmNewOptionsTasker_Load(object sender, EventArgs e)
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int space = 10;
            if (StartPosX + this.Width + space > width) StartPosX = width - this.Width - space;
            if (StartPosX < 0) StartPosX = 0;
            this.Left = StartPosX;
            this.Top = StartPosY;

//            panelForm.Width = this.Width;
//            panelForm.Height = this.Height;
        }

        private void frmNewOptionsTask_Deactivate(object sender, EventArgs e)
        {
            if (ConfirmFlag) return;

            Exit();
        }

        private void InitTaskList()
        {
            if (TaskList == null) TaskList = new List<OptionTask>();
            TaskList.Clear();

            if (SourceTaskList == null) return;

            int num = 0;
            for (int i = 0; i < SourceTaskList.Count; i++)
            {
                OptionTask task = SourceTaskList[i];
                if (!task.ClosedFlag)
                {
                    num++;
                    int id = task.ID;
                    int prior = task.Prior;
                    string dgcode = task.DgCode;
                    DateTime dates = task.CreateDate;
                    DateTime task_date = task.TaskDate;
                    string name = task.Name;
                    string stat = task.Stat;
                    int task_flag = 0;
                    if (task.TaskFlag) task_flag = 1;
                    int open_code = task.OpenCode;
                    TaskList.Add(new OptionTask(task.ID, num, prior, dgcode, dates, task_date, task.Name, task.Stat, task_flag, open_code));
                }
            }

            TaskerModel.SourceTaskList = TaskList;
            TaskerModel.InitTaskList();
        }

        private void Exit()
        {
            this.Close();

            // update parent form
            if (TaskerClosed != null) TaskerClosed(false);
        }

        public bool ListCloseTask(int TaskID)
        {
            if (TaskList == null) return false;

            ConfirmFlag = true;
            bool bClosed = false;

            OptionTask task = null;
            for (int i = 0; i < TaskList.Count; i++)
            {
                OptionTask t = TaskList[i];
                if (t.ID == TaskID)
                {
                    task = t;
                    break;
                }
            }

            string TaskName = "";
            if (task != null) TaskName = task.Name;

            //frmNewOptionsConfirmTasker frm = new frmNewOptionsConfirmTasker(TaskName);
            frmNewOptionsConfirmTaskerWPF frm = new frmNewOptionsConfirmTaskerWPF(TaskName);
            //frm.StartPosX = this.Left + tcOptions.Left + tcOptions.Width - 30;
            //frm.StartPosY = this.Top + tcOptions.Top + tcOptions.Height - frm.Height - 100;
            frm.StartPosX = Cursor.Position.X - (int)Math.Round((decimal)frm.Width / 2);
            frm.StartPosY = Cursor.Position.Y + 20;
            frm.ShowDialog();

            if (frm.ConfirmFlag)
            {
                Do_CloseTask(TaskID);
                bClosed = true;
            }
            if (frm.DelayFlag)
            {
                Do_DelayTask(TaskID, frm.DelayDate);
                bClosed = false;
            }

            ConfirmFlag = false;
            return bClosed;
        }

        private void Do_CloseTask(int TaskID)
        {
            string query = @"UPDATE mk_DogovorTask SET DT_CLOSED = 1, DT_CLOSED_DATE = GETDATE() WHERE [DT_KEY] = @task_id";
            using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@task_id", TaskID);
                com.ExecuteNonQuery();
            }

            for (int i = 0; i < SourceTaskList.Count; i++)
            {
                OptionTask task = SourceTaskList[i];
                if (task.ID == TaskID)
                {
                    task.ClosedFlag = true;
                    break;
                }
            }

            if (CloseTask != null) CloseTask(TaskID);

            InitTaskList();

            if (TaskList.Count > 0)
            {
                TaskListHost.Refresh();
            }
            else
            {
                Exit();
            }
        }

        private void Do_DelayTask(int TaskID, DateTime DelayDate)
        {
            string query = @"UPDATE mk_DogovorTask SET [DT_TASK_DATE] = @task_date WHERE [DT_KEY] = @task_id";
            using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@task_id", TaskID);
                com.Parameters.AddWithValue("@task_date", DelayDate);
                try
                {
                    com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }

            InitTaskList();
        }
    }
}
