namespace terms_prepaid.Forms
{
    partial class frmNewOptionsConfirmTask
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
            this.lblTask = new System.Windows.Forms.Label();
            this.btn_Yes = new System.Windows.Forms.Button();
            this.btn_No = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDeadline = new System.Windows.Forms.Label();
            this.lblReflect = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTask
            // 
            this.lblTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTask.Location = new System.Drawing.Point(49, 43);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(461, 53);
            this.lblTask.TabIndex = 1;
            this.lblTask.Text = "Название  зачачи  к  заказу";
            // 
            // btn_Yes
            // 
            this.btn_Yes.BackColor = System.Drawing.Color.PaleGreen;
            this.btn_Yes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Yes.Location = new System.Drawing.Point(55, 184);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(190, 30);
            this.btn_Yes.TabIndex = 2;
            this.btn_Yes.Text = "Да,  назначить";
            this.btn_Yes.UseVisualStyleBackColor = false;
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // btn_No
            // 
            this.btn_No.BackColor = System.Drawing.Color.Coral;
            this.btn_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_No.Location = new System.Drawing.Point(280, 184);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(190, 30);
            this.btn_No.TabIndex = 3;
            this.btn_No.Text = "Нет,  отменить";
            this.btn_No.UseVisualStyleBackColor = false;
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(51, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(459, 24);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Назначение  зачачи  к  заказу:";
            // 
            // lblDeadline
            // 
            this.lblDeadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDeadline.ForeColor = System.Drawing.Color.DarkRed;
            this.lblDeadline.Location = new System.Drawing.Point(49, 96);
            this.lblDeadline.Name = "lblDeadline";
            this.lblDeadline.Size = new System.Drawing.Size(461, 27);
            this.lblDeadline.TabIndex = 5;
            this.lblDeadline.Text = "Deadline:";
            // 
            // lblReflect
            // 
            this.lblReflect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReflect.Location = new System.Drawing.Point(50, 123);
            this.lblReflect.Name = "lblReflect";
            this.lblReflect.Size = new System.Drawing.Size(459, 24);
            this.lblReflect.TabIndex = 6;
            this.lblReflect.Text = "Отразить  задачу  в  переписке";
            // 
            // frmNewOptionsConfirmTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(524, 239);
            this.Controls.Add(this.lblReflect);
            this.Controls.Add(this.lblDeadline);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btn_No);
            this.Controls.Add(this.btn_Yes);
            this.Controls.Add(this.lblTask);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewOptionsConfirmTask";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подтверждение";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmNewOptionsConfirmSave_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.Button btn_Yes;
        private System.Windows.Forms.Button btn_No;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDeadline;
        private System.Windows.Forms.Label lblReflect;
    }
}