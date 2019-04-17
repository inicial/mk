namespace terms_prepaid
{
    partial class frmNewOptionsTasker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TaskListHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // TaskListHost
            // 
            this.TaskListHost.BackColor = System.Drawing.Color.White;
            this.TaskListHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TaskListHost.Location = new System.Drawing.Point(0, 0);
            this.TaskListHost.Name = "TaskListHost";
            this.TaskListHost.Size = new System.Drawing.Size(700, 200);
            this.TaskListHost.TabIndex = 0;
            this.TaskListHost.Text = "TaskListControl";
            this.TaskListHost.Child = null;
            // 
            // frmNewOptionsTasker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 200);
            this.Controls.Add(this.TaskListHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewOptionsTasker";
            this.Text = "Задачи  по  заказу";
            this.Deactivate += new System.EventHandler(this.frmNewOptionsTask_Deactivate);
            this.Load += new System.EventHandler(this.frmNewOptionsTasker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost TaskListHost;
    }
}