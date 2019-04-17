namespace lanta.Clients
{
    partial class ReportSendList
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ClientsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lantaDataSet = new lanta.Clients.lantaDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ClientsTableAdapter = new lanta.Clients.lantaDataSetTableAdapters.ClientsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ClientsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lantaDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // ClientsBindingSource
            // 
            this.ClientsBindingSource.DataMember = "Clients";
            this.ClientsBindingSource.DataSource = this.lantaDataSet;
            // 
            // lantaDataSet
            // 
            this.lantaDataSet.DataSetName = "lantaDataSet";
            this.lantaDataSet.EnforceConstraints = false;
            this.lantaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "lantaDataSet_Clients";
            reportDataSource1.Value = this.ClientsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "lanta.Clients.Clients.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(926, 372);
            this.reportViewer1.TabIndex = 0;
            // 
            // ClientsTableAdapter
            // 
            this.ClientsTableAdapter.ClearBeforeFill = true;
            // 
            // ReportSendList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 372);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportSendList";
            this.Text = "Список рассылки";
            this.Load += new System.EventHandler(this.ReportSendList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ClientsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lantaDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ClientsBindingSource;
        private lantaDataSet lantaDataSet;
        private Clients.lantaDataSetTableAdapters.ClientsTableAdapter ClientsTableAdapter;
        

    }
}