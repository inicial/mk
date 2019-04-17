namespace terms_prepaid.Forms
{
    partial class frmDownloadedFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownloadedFiles));
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvDownloadedFiles = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloadedFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(916, 440);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Вернуться";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvDownloadedFiles
            // 
            this.dgvDownloadedFiles.AllowUserToAddRows = false;
            this.dgvDownloadedFiles.AllowUserToDeleteRows = false;
            this.dgvDownloadedFiles.AllowUserToOrderColumns = true;
            this.dgvDownloadedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDownloadedFiles.BackgroundColor = System.Drawing.Color.White;
            this.dgvDownloadedFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDownloadedFiles.Location = new System.Drawing.Point(12, 12);
            this.dgvDownloadedFiles.Name = "dgvDownloadedFiles";
            this.dgvDownloadedFiles.Size = new System.Drawing.Size(979, 422);
            this.dgvDownloadedFiles.TabIndex = 1;
            this.dgvDownloadedFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDownloadedFiles_CellContentClick);
            // 
            // frmDownloadedFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 475);
            this.Controls.Add(this.dgvDownloadedFiles);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDownloadedFiles";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloadedFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvDownloadedFiles;
    }
}