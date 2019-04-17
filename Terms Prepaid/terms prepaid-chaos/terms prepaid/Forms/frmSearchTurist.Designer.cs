namespace terms_prepaid.Forms
{
    partial class frmSearchTurist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchTurist));
            this.tbSearchString = new System.Windows.Forms.TextBox();
            this.dgvTurists = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurists)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSearchString
            // 
            this.tbSearchString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchString.Location = new System.Drawing.Point(12, 12);
            this.tbSearchString.Name = "tbSearchString";
            this.tbSearchString.Size = new System.Drawing.Size(763, 20);
            this.tbSearchString.TabIndex = 0;
            this.tbSearchString.TextChanged += new System.EventHandler(this.tbSearchString_TextChanged);
            // 
            // dgvTurists
            // 
            this.dgvTurists.AllowUserToAddRows = false;
            this.dgvTurists.AllowUserToDeleteRows = false;
            this.dgvTurists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTurists.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTurists.BackgroundColor = System.Drawing.Color.White;
            this.dgvTurists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurists.Location = new System.Drawing.Point(12, 38);
            this.dgvTurists.Name = "dgvTurists";
            this.dgvTurists.ReadOnly = true;
            this.dgvTurists.RowHeadersVisible = false;
            this.dgvTurists.Size = new System.Drawing.Size(763, 470);
            this.dgvTurists.TabIndex = 1;
            this.dgvTurists.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTurists_CellDoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(700, 514);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Вернуться";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmSearchTurist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 549);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvTurists);
            this.Controls.Add(this.tbSearchString);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSearchTurist";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurists)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSearchString;
        private System.Windows.Forms.DataGridView dgvTurists;
        private System.Windows.Forms.Button btnClose;
    }
}