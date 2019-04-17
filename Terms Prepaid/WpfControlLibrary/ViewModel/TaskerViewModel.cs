using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
//using terms_prepaid.Helper_Classes;


namespace WpfControlLibrary.ViewModel
{
    public class OptionTask
    {
        public int ID { get; set; }
        public int Num { get; set; }
        public int Prior { get; set; }
        public string DgCode { get; set; }
        public DateTime CreateDate;
        public string DateStr { get; set; }
        public DateTime TaskDate;
        public string TaskDateStr { get; set; }
        public string Name { get; set; }
        public string Stat { get; set; }
        public bool TaskFlag { get; set; }
        public int OpenCode { get; set; }
        public bool ClosedFlag { get; set; }

        public OptionTask(int iID, int iNum, int iPrior, string iDgCode, DateTime iCreateDate, DateTime iTaskDate, string iName, string iStat, int iTaskFlag, int iOpenCode)
        {
            ID = iID;
            Num = iNum;
            Prior = iPrior;
            DgCode = iDgCode;
            CreateDate = iCreateDate;
            DateStr = "";
            if (CreateDate.Year > 2010) DateStr = CreateDate.ToString("dd.MM.yy  HH:mm");
            TaskDate = iTaskDate;
            TaskDateStr = "";
            if (TaskDate.Year > 2010) TaskDateStr = "на " + TaskDate.ToString("dd.MM.yy");
            Name = iName;
            Stat = iStat;
            TaskFlag = iTaskFlag > 0;
            OpenCode = iOpenCode;
        }

        public string PriorIcon
        {
            get 
            { 
                string icon_name = "ico_empty.ico";
                if (Prior == 1) icon_name = "ico_warning_2.ico";
                if (Prior == 2) icon_name = "ico_warning_3.ico";
                //if (Prior >= 3) icon_name = "ico_warning_3.ico";
                return @"/WpfControlLibrary;component/img/" + icon_name; 
            }
        }

    }


    public class TaskerViewModel
    {
        private Form _taskerForm;
        public Form TaskerForm
        {
            get { return _taskerForm; }
            set { _taskerForm = value; }
        }

        public List<OptionTask> _sourceTaskList;
        public List<OptionTask> SourceTaskList
        {
            get { return _sourceTaskList; }
            set { _sourceTaskList = value; InitTaskList(); }
        }

        private ObservableCollection<OptionTask> _taskList;
        public ObservableCollection<OptionTask> TaskList
        {
            get { return _taskList; }
            set { _taskList = value; }
        }

        private string _todayTitle;
        public string TodayTitle
        {
            get { return _todayTitle; }
            set { _todayTitle = value; }
        }

        public RelayCommand CloseCommand { get; set; }

        
        public TaskerViewModel()
        {
            TaskList = new ObservableCollection<OptionTask>();
            TodayTitle = "Задачи по заказу на " + DateTime.Now.Date.ToString("dd.MM.yy");

            CloseCommand = new RelayCommand(CloseForm, null);
        }

        public void CloseForm()
        {
            if (TaskerForm == null) return;

            TaskerForm.Close();
        }

        public void InitTaskList()
        {
            TaskList.Clear();
            if (SourceTaskList == null) return;

            foreach (OptionTask task in SourceTaskList)
            {
                TaskList.Add(task);
            }
        }

    }
}
