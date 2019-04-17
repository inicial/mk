using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.View;
using System.Windows;
using System.Windows.Controls;


namespace terms_prepaid
{
    public partial class frmNewOptionsTask : Form
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;

        AddTaskControl TaskControl;

        //private List<TaskRecord> AllTaskList;
        private List<TaskRecord> BronirTaskList;
        private List<TaskRecord> RealizTaskList;
        private List<TaskRecord> TaskList;
        public List<TaskRecord> TaskRecordList;

        private int TaskList_SelectedIndex;
        //private bool TaskList_Focused;

        private SelectOptionTask TaskCallback;

        private bool IsBronir; 
        private bool IsRealiz;
        private bool BronirFlag; 
        private bool RealizFlag;

        private int SelectedTaskIndex = -1;
        private string SelectedTaskName = "";
        private DateTime SelectedTaskDate;
        private bool SelectedTaskReflect = false;

        private bool DateFocusFlag;


        public frmNewOptionsTask(int iX, int iY, List<TaskRecord> iBronirTaskList, List<TaskRecord> iRealizTaskList, SelectOptionTask iTaskCallback, bool iBronir, bool iRealiz)
        {
            StartPosX = iX;
            StartPosY = iY;

            //AllTaskList = iAllTaskList;
            BronirTaskList = iBronirTaskList;
            RealizTaskList = iRealizTaskList;
            TaskCallback = iTaskCallback;
            IsBronir = iBronir;
            IsRealiz = iRealiz;
            //BronirFlag = iBronir;
            //RealizFlag = iRealiz;

            InitializeComponent();

            if (IsBronir && !IsRealiz)
            {
                tab_Bronir.Text = "Задачи бронировщика";
                ctl_Tabs.TabPages.Remove(tab_Realiz);
                BronirFlag = true;
                RealizFlag = false;
            }
            if (!IsBronir && IsRealiz)
            {
                tab_Realiz.Text = "Задачи реализатора";
                ctl_Tabs.TabPages.Remove(tab_Bronir);
                BronirFlag = false;
                RealizFlag = true;
            }

            InitTaskList();

            ctl_Tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
            ctl_Tabs.DrawItem += ctl_Tabs_DrawItem;
        }

        private void frmNewOptionsTask_Load(object sender, EventArgs e)
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int space = 10;
            if (StartPosX + this.Width + space > width) StartPosX = width - this.Width - space;
            if (StartPosX < 0) StartPosX = 0;
            this.Left = StartPosX;
            this.Top = StartPosY;

            panelForm.Width = this.Width;
            panelForm.Height = this.Height;

            dtp_Date.Format = DateTimePickerFormat.Custom;
            dtp_Date.CustomFormat = "dd.MM.yy";

            if (!BronirFlag && !RealizFlag || BronirFlag && RealizFlag)
            {
                BronirFlag = true;
                RealizFlag = false;
            }
            //if (!BronirFlag && !RealizFlag || BronirFlag && RealizFlag)
            //{
                ctl_Tabs.SelectedIndex = 0;
                Accord_List();
            //}
            //if (BronirFlag && !RealizFlag) ctl_Tabs.SelectedIndex = 0;
            if (!BronirFlag && RealizFlag) ctl_Tabs.SelectedIndex = 1;

            dtp_Date.Value = DateTime.Now.Date;
            SelectedTaskDate = new DateTime(2000, 1, 1);
            //chk_Day_2.Checked = true;
        }

        private void frmNewOptionsTask_Deactivate(object sender, EventArgs e)
        {
            if (DateFocusFlag) return;

            //Exit();
            this.Close();
        }

        private void InitTaskList()
        {
            if (TaskRecordList == null) TaskRecordList = new List<TaskRecord>();

            TaskControl = new AddTaskControl(TaskRecordList, TaskList_OnSelectionChanged);
            TaskControl.GotFocusCallback = TaskList_OnGotFocus;
            TaskControl.LostFocusCallback = TaskList_OnLostFocus;
            //_TaskListControl.OnButtonClick += OptionControlOnButtonClick;
            TaskListHost.Child = TaskControl;
        }

        private void frmNewOptionsTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            if (e.KeyCode == Keys.Enter)
            {
                Select_Task();
            }
            if (e.KeyCode == Keys.Space)
            {
//                if (TaskList_Focused) Select_Task();
                //if (lst_Task.Focused) Select_Task();

            }
        }

        private void ctl_Tabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage tp = ctl_Tabs.TabPages[e.Index];
            bool bSelected = ctl_Tabs.SelectedTab.Equals(tp);
            Color color = ctl_Tabs.SelectedTab.Equals(tp) ? Color.FromArgb(255, 179, 209, 234) : Color.LightGray;

            using (Brush br = new SolidBrush(color))
            {
                tp.BackColor = color;
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(ctl_Tabs.TabPages[e.Index].Text, e.Font);
                int y = (int)Math.Round(e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2) + 1;
                if (bSelected) y = y - 2;
                e.Graphics.DrawString(ctl_Tabs.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, y);

                Rectangle rect = e.Bounds;
                //rect.Offset(0, 1);
                rect.Height++;
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
            }
        }

        private void Add_List_Row(string row, int group)
        {
            string str = "";
            if (!string.IsNullOrEmpty(row)) str = row;

///            lst_Task.Items.Add(str);

            TaskRecord task1 = new TaskRecord();
            task1.TaskNum = TaskList.Count + 1;
            task1.TaskSelected = false;
            task1.TaskName = str;
            task1.TaskGroup = group;
            TaskList.Add(task1);
            
            TaskRecord task2 = new TaskRecord();
            task2.TaskNum = TaskRecordList.Count + 1;
            task2.TaskSelected = false;
            task2.TaskName = str;
            task2.TaskGroup = group;
            TaskRecordList.Add(task2);
        }

        private void Accord_List()
        {
///            lst_Task.Items.Clear();
            if (TaskList == null) TaskList = new List<TaskRecord>();
            TaskList.Clear();
            if (TaskRecordList == null) TaskRecordList = new List<TaskRecord>();
            TaskRecordList.Clear();

            if (BronirTaskList == null) return;
            if (BronirTaskList.Count == 0) return;

            //if (BronirFlag && RealizFlag)
            //    foreach (TaskRecord task in AllTaskList) { Add_List_Row(task.TaskName, task.TaskGroup); }

            if (BronirFlag && !RealizFlag)
                foreach (TaskRecord task in BronirTaskList) { Add_List_Row(task.TaskName, task.TaskGroup); }

            if (!BronirFlag && RealizFlag)
                foreach (TaskRecord task in RealizTaskList) { Add_List_Row(task.TaskName, task.TaskGroup); }

            //Add_List_Row("Empty...", 1);
            //Add_List_Row("Empty...", 1); 

            AccordButton();

            TaskControl.ReflectList();
            TaskListHost.Refresh();
        }

        private void Select_Task()
        {
//            if (lst_Task.SelectedIndex >= 0) SelectedTask = lst_Task.SelectedItem.ToString();
            if (SelectedTaskIndex >= 0 && TaskRecordList != null)
                if (SelectedTaskIndex < TaskRecordList.Count)
                    {
                        TaskRecord task = TaskRecordList[SelectedTaskIndex];
                        //SelectedTaskName = task.TaskName;
                        SelectedTaskReflect = task.TaskReflect;
                    }

            string edit = txt_Edit.Text;
            if (!string.IsNullOrEmpty(edit))
            {
                SelectedTaskName = edit;
                if (chk_ReflectEdit.Checked) SelectedTaskReflect = true;
            }
            DateTime dt = dtp_Date.Value;

            this.Close();

            if (TaskCallback != null)
                TaskCallback(BronirFlag, RealizFlag, SelectedTaskName, dt, SelectedTaskReflect);
        }

        private void Exit()
        {
            DateTime dt = dtp_Date.Value;

            this.Close();

            if (TaskCallback != null) TaskCallback(BronirFlag, RealizFlag, "", dt, SelectedTaskReflect);
        }

        private void ctl_Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ctl_Tabs.TabCount == 0) return;

            BronirFlag = false;
            RealizFlag = false;

            if (ctl_Tabs.TabCount == 1)
            {
                if (IsBronir)
                {
                    BronirFlag = true;
                    RealizFlag = false;
                }
                if (IsRealiz)
                {
                    BronirFlag = false;
                    RealizFlag = true;
                }
            }
            if (ctl_Tabs.TabCount == 2)
            {
                //if (ctl_Tabs.SelectedIndex == 0)
                //{
                //    BronirFlag = true;
                //    RealizFlag = true;
                //}
                if (ctl_Tabs.SelectedIndex == 0)
                {
                    BronirFlag = true;
                    RealizFlag = false;
                }
                if (ctl_Tabs.SelectedIndex == 1)
                {
                    BronirFlag = false;
                    RealizFlag = true;
                }
            }

            Accord_List();
            
            //lst_Task.Focus();
            TaskControl.FocusList();
        }

        private void TaskList_OnGotFocus(object sender, RoutedEventArgs e)
        {
            //TaskList_Focused = true;

            //TaskList_SelectedIndex = TaskControl.GetSelectedIndex();
            //SelectedTaskName = TaskControl.GetSelectedItem();

            //txt_Edit.Text = "";

            //AccordButton();
        }

        private void TaskList_OnLostFocus(object sender, RoutedEventArgs e)
        {
            //TaskList_Focused = false;
        }

        private void TaskList_OnSelectionChanged(int item_index)
        {
            TaskList_SelectedIndex = item_index;

            SelectedTaskIndex = -1;
            SelectedTaskName = "";
            SelectedTaskReflect = false;
            if (item_index >= 0)
            {
                if (TaskRecordList != null)
                {
                    if (item_index < TaskRecordList.Count)
                    {
                        TaskRecord task = TaskRecordList[item_index];
                        SelectedTaskIndex = item_index;
                        SelectedTaskName = task.TaskName;
                        SelectedTaskReflect = task.TaskReflect;
                    }
                }
            }

            if (!string.IsNullOrEmpty(txt_Edit.Text))
            {
                txt_Edit.Text = "";
                //if (SelectedTaskDate.Date >= DateTime.Now.Date) ClearDate();
                //if (dtp_Date.Enabled) dtp_Date.Focus();
                if (chk_Day_0.Enabled) chk_Day_0.Focus();
            }

            AccordButton();
        }

        private void lst_Task_Enter(object sender, EventArgs e)
        {
            txt_Edit.Text = "";
        }

        private void ClearSelection()
        {
            SelectedTaskIndex = -1;
            SelectedTaskName = "";
            SelectedTaskReflect = false;

            if (TaskList_SelectedIndex >= 0)
                TaskList_SelectedIndex = -1;
            if (TaskControl.SelectedTaskNum > 0)
                TaskControl.ClearSelection();
        }

        private void ClearDate()
        {
            dtp_Date.Value = DateTime.Now.Date;
            chk_Day_0.Checked = false;
            chk_Day_1.Checked = false;
            chk_Day_2.Checked = false;
            chk_Day_3.Checked = false;
            SelectedTaskDate = new DateTime(2000, 1, 1);
        }

        private void txt_Edit_Enter(object sender, EventArgs e)
        {
            ClearSelection();
            if (string.IsNullOrEmpty(txt_Edit.Text))
                ClearDate();

            AccordButton();
        }

        private void txt_Edit_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Edit.Text))
              ClearSelection();

            if (string.IsNullOrEmpty(txt_Edit.Text))
                ClearDate();

            AccordButton();
        }

        private void AccordButton()
        {
            bool bTask = false;
            bool bEdit = false;
            bool bDate = false;
            bool bSelect = false;
            //if (lst_Task.SelectedIndex >= 0) bTask = true;
            //if (TaskList_SelectedIndex >= 0) bTask = true;
            if (!string.IsNullOrEmpty(SelectedTaskName)) { bTask = true; bDate = true; }
            if (!string.IsNullOrEmpty(txt_Edit.Text)) { bEdit = true; bDate = true; }
            if (SelectedTaskDate.Date >= DateTime.Now.Date) bSelect = true;

//if (btn_Select.Enabled && !bTask)
//  btn_Select.Enabled = bTask;

            dtp_Date.Enabled = bDate;
            chk_Day_0.Enabled = bDate;
            chk_Day_1.Enabled = bDate;
            chk_Day_2.Enabled = bDate;
            chk_Day_3.Enabled = bDate;

            if (!bEdit && chk_ReflectEdit.Checked) chk_ReflectEdit.Checked = false;
            chk_ReflectEdit.Enabled = bEdit;
            btn_Select.Enabled = bSelect && bDate;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            bool bTask = false;
            //if (lst_Task.SelectedIndex >= 0) bTask = true;
            if (TaskList_SelectedIndex >= 0) bTask = true;
            if (!string.IsNullOrEmpty(txt_Edit.Text)) bTask = true;
            if (!bTask) return;

            Select_Task();
        }

        private void dtp_Date_ValueChanged(object sender, EventArgs e)
        {
            SelectedTaskDate = dtp_Date.Value;

            AccordButton();
        }

        private void SetSate(int days)
        {
            dtp_Date.Value = DateTime.Now.AddDays(days);
        }

        private void chk_Day_0_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Day_0.Checked) SetSate(0);
        }

        private void chk_Day_1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Day_1.Checked) SetSate(1);
        }

        private void chk_Day_2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Day_2.Checked) SetSate(2);
        }

        private void chk_Day_3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Day_3.Checked) SetSate(3);
        }

        private void chk_Day_0_Click(object sender, EventArgs e)
        {
            if (chk_Day_0.Checked)
            {
                if (chk_Day_1.Checked) chk_Day_1.Checked = false;
                if (chk_Day_2.Checked) chk_Day_2.Checked = false;
                if (chk_Day_3.Checked) chk_Day_3.Checked = false;
            }
            else
            {
                chk_Day_0.Checked = true;
            }
        }

        private void chk_Day_1_Click(object sender, EventArgs e)
        {
            if (chk_Day_1.Checked)
            {
                if (chk_Day_0.Checked) chk_Day_0.Checked = false;
                if (chk_Day_2.Checked) chk_Day_2.Checked = false;
                if (chk_Day_3.Checked) chk_Day_3.Checked = false;
            }
            else
            {
                chk_Day_1.Checked = true;
            }
        }

        private void chk_Day_2_Click(object sender, EventArgs e)
        {
            if (chk_Day_2.Checked)
            {
                if (chk_Day_0.Checked) chk_Day_0.Checked = false;
                if (chk_Day_1.Checked) chk_Day_1.Checked = false;
                if (chk_Day_3.Checked) chk_Day_3.Checked = false;
            }
            else
            {
                chk_Day_2.Checked = true;
            }
        }

        private void chk_Day_3_Click(object sender, EventArgs e)
        {
            if (chk_Day_3.Checked)
            {
                if (chk_Day_0.Checked) chk_Day_0.Checked = false;
                if (chk_Day_1.Checked) chk_Day_1.Checked = false;
                if (chk_Day_2.Checked) chk_Day_2.Checked = false;
            }
            else
            {
                chk_Day_3.Checked = true;
            }
        }

        private void dtp_Date_Enter(object sender, EventArgs e)
        {
            if (chk_Day_0.Checked) chk_Day_0.Checked = false;
            if (chk_Day_1.Checked) chk_Day_1.Checked = false;
            if (chk_Day_2.Checked) chk_Day_2.Checked = false;
            if (chk_Day_3.Checked) chk_Day_3.Checked = false;
        }

        private void dtp_Date_Leave(object sender, EventArgs e)
        {

        }

        private void dtp_Date_DropDown(object sender, EventArgs e)
        {
            DateFocusFlag = true;
        }

        private void dtp_Date_CloseUp(object sender, EventArgs e)
        {
            DateFocusFlag = false;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
