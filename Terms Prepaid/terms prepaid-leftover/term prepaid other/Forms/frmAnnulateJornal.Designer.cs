namespace terms_prepaid.Forms
{
    partial class frmAnnulateJornal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnnulateJornal));
            this.dgvJornal = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbIsOk = new System.Windows.Forms.CheckBox();
            this.cbIsCalc = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJornal)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvJornal
            // 
            this.dgvJornal.AllowUserToAddRows = false;
            this.dgvJornal.AllowUserToDeleteRows = false;
            this.dgvJornal.AllowUserToResizeColumns = false;
            this.dgvJornal.AllowUserToResizeRows = false;
            this.dgvJornal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJornal.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvJornal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJornal.Location = new System.Drawing.Point(12, 35);
            this.dgvJornal.Name = "dgvJornal";
            this.dgvJornal.ReadOnly = true;
            this.dgvJornal.Size = new System.Drawing.Size(1400, 379);
            this.dgvJornal.TabIndex = 0;
            this.dgvJornal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJornal_CellContentClick);
            this.dgvJornal.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvJornal_RowPrePaint);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1337, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Вернутся";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbIsOk
            // 
            this.cbIsOk.AutoSize = true;
            this.cbIsOk.Checked = true;
            this.cbIsOk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsOk.Location = new System.Drawing.Point(12, 12);
            this.cbIsOk.Name = "cbIsOk";
            this.cbIsOk.Size = new System.Drawing.Size(190, 17);
            this.cbIsOk.TabIndex = 2;
            this.cbIsOk.Text = "Не показывать взятые в работу";
            this.cbIsOk.UseVisualStyleBackColor = true;
            this.cbIsOk.CheckedChanged += new System.EventHandler(this.cbIsOk_CheckedChanged);
            // 
            // cbIsCalc
            // 
            this.cbIsCalc.AutoSize = true;
            this.cbIsCalc.Checked = true;
            this.cbIsCalc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsCalc.Location = new System.Drawing.Point(208, 12);
            this.cbIsCalc.Name = "cbIsCalc";
            this.cbIsCalc.Size = new System.Drawing.Size(263, 17);
            this.cbIsCalc.TabIndex = 3;
            this.cbIsCalc.Text = "Не показывать с выставленной калькуляцией";
            this.cbIsCalc.UseVisualStyleBackColor = true;
            this.cbIsCalc.CheckedChanged += new System.EventHandler(this.cbIsCalc_CheckedChanged);
            // 
            // frmAnnulateJornal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 432);
            this.Controls.Add(this.cbIsCalc);
            this.Controls.Add(this.cbIsOk);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvJornal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAnnulateJornal";
            this.Text = "Журнал аннуляций";
            this.Load += new System.EventHandler(this.frmAnnulateJornal_Load);
            this.Shown += new System.EventHandler(this.frmAnnulateJornal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJornal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvJornal;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox cbIsOk;
        private System.Windows.Forms.CheckBox cbIsCalc;
    }
}