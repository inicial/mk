namespace terms_prepaid.Forms
{
    partial class frmChangesTurist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangesTurist));
            this.dgvChanges = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChanges)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvChanges
            // 
            this.dgvChanges.AllowUserToAddRows = false;
            this.dgvChanges.AllowUserToDeleteRows = false;
            this.dgvChanges.AllowUserToResizeColumns = false;
            this.dgvChanges.AllowUserToResizeRows = false;
            this.dgvChanges.BackgroundColor = System.Drawing.Color.White;
            this.dgvChanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChanges.Location = new System.Drawing.Point(0, 0);
            this.dgvChanges.Name = "dgvChanges";
            this.dgvChanges.ReadOnly = true;
            this.dgvChanges.Size = new System.Drawing.Size(784, 343);
            this.dgvChanges.TabIndex = 0;
            // 
            // frmChangesTurist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 343);
            this.Controls.Add(this.dgvChanges);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangesTurist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Shown += new System.EventHandler(this.frmChangesTurist_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChanges)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvChanges;
    }
}