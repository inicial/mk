namespace terms_prepaid.Forms
{
    partial class frmNewOptionsConfirmTasker
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
            this.lbl_Message = new System.Windows.Forms.Label();
            this.btn_Yes = new System.Windows.Forms.Button();
            this.btn_No = new System.Windows.Forms.Button();
            this.btn_Delay = new System.Windows.Forms.Button();
            this.dtpDelayDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // lbl_Message
            // 
            this.lbl_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_Message.Location = new System.Drawing.Point(49, 23);
            this.lbl_Message.Name = "lbl_Message";
            this.lbl_Message.Size = new System.Drawing.Size(461, 80);
            this.lbl_Message.TabIndex = 1;
            this.lbl_Message.Text = "Задача  будет  закрыта";
            // 
            // btn_Yes
            // 
            this.btn_Yes.BackColor = System.Drawing.Color.PaleGreen;
            this.btn_Yes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Yes.Location = new System.Drawing.Point(35, 116);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(220, 30);
            this.btn_Yes.TabIndex = 2;
            this.btn_Yes.TabStop = false;
            this.btn_Yes.Text = "Да,  я ее выполнил(а)";
            this.btn_Yes.UseVisualStyleBackColor = false;
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // btn_No
            // 
            this.btn_No.BackColor = System.Drawing.Color.Coral;
            this.btn_No.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_No.Location = new System.Drawing.Point(280, 116);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(220, 30);
            this.btn_No.TabIndex = 3;
            this.btn_No.TabStop = false;
            this.btn_No.Text = "Ой, я ее доделаю";
            this.btn_No.UseVisualStyleBackColor = false;
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            // 
            // btn_Delay
            // 
            this.btn_Delay.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Delay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Delay.Location = new System.Drawing.Point(83, 166);
            this.btn_Delay.Name = "btn_Delay";
            this.btn_Delay.Size = new System.Drawing.Size(244, 30);
            this.btn_Delay.TabIndex = 4;
            this.btn_Delay.TabStop = false;
            this.btn_Delay.Text = "Нет, хочу отложить ее на период";
            this.btn_Delay.UseVisualStyleBackColor = false;
            this.btn_Delay.Click += new System.EventHandler(this.btn_Delay_Click);
            // 
            // dtpDelayDate
            // 
            this.dtpDelayDate.CustomFormat = "dd.MM.yy";
            this.dtpDelayDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpDelayDate.Location = new System.Drawing.Point(349, 169);
            this.dtpDelayDate.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtpDelayDate.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtpDelayDate.Name = "dtpDelayDate";
            this.dtpDelayDate.Size = new System.Drawing.Size(88, 24);
            this.dtpDelayDate.TabIndex = 5;
            this.dtpDelayDate.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            // 
            // frmNewOptionsConfirmTasker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.CancelButton = this.btn_No;
            this.ClientSize = new System.Drawing.Size(524, 218);
            this.Controls.Add(this.dtpDelayDate);
            this.Controls.Add(this.btn_Delay);
            this.Controls.Add(this.btn_No);
            this.Controls.Add(this.btn_Yes);
            this.Controls.Add(this.lbl_Message);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewOptionsConfirmTasker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подтверждение";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmNewOptionsConfirmSave_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Message;
        private System.Windows.Forms.Button btn_Yes;
        private System.Windows.Forms.Button btn_No;
        private System.Windows.Forms.Button btn_Delay;
        private System.Windows.Forms.DateTimePicker dtpDelayDate;
    }
}