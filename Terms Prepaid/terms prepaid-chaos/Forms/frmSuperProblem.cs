using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid.Forms
{
    public partial class frmSuperProblem : Form
    {



        public frmSuperProblem()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.No;
            GetDate();
           
        }

       
        void CorrectText()
        {
            foreach (string s in WorkWithData.rezervedText)
            {
                string temp = rtbProblems.Text;
                int deleted = 0;
                int i = temp.IndexOf(s);
                while (i>=0)
                {
                    rtbProblems.Select(deleted+i,s.Length);
                    rtbProblems.SelectionColor = Color.DeepSkyBlue;
                    deleted += i + (s).Length;
                    temp = temp.Remove(0, i + s.Length);
                    i = temp.IndexOf(s);
                }
                //string temp1 = rtbProblems.Text;
                //int deleted1 = 0;
                //int i1 = temp.IndexOf("##");
                //while (i >= 0)
                //{
                //    rtbProblems.Select(deleted1 + i1, "##".Length);
                //    rtbProblems.SelectionFont = new Font(rtbProblems.Font.FontFamily,2);
                //    deleted1 += i1 + "##".Length;
                //    temp1 = temp1.Remove(0, i1 + "##".Length);
                //    i = temp.IndexOf(s);
                //}
                //rtbProblems.Text = rtbProblems.Text.Replace("##", "");
            }
            

        }
        void GetDate()
        {
            string problemString = WorkWithData.GetSuperProblem();
            if (problemString == string.Empty)
            {
                this.DialogResult = DialogResult.Yes;
                Close();
            }
            else
            {
                //problemString = "\n" +problemString;
                rtbProblems.Text = problemString;
                CorrectText();
                rtbProblems.Select(0, 0);
                HideCaret(rtbProblems.Handle);
            }
           

        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
           
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool HideCaret(IntPtr hwnd);
        private void frmSuperProblem_Shown(object sender, EventArgs e)
        {
            HideCaret(rtbProblems.Handle);
        }
    }
}
