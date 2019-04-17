namespace lanta.Clients
{
    partial class LoadPicture
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_Open = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.dataGridView_С = new System.Windows.Forms.DataGridView();
            this.pictureBox_LCU_PHOTO = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_С)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LCU_PHOTO)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.button_Open, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_Save, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_С, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_LCU_PHOTO, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 242F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(568, 345);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button_Open
            // 
            this.button_Open.Location = new System.Drawing.Point(3, 245);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(116, 23);
            this.button_Open.TabIndex = 0;
            this.button_Open.Text = "Открыть с диска";
            this.button_Open.UseVisualStyleBackColor = true;
            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(3, 308);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(124, 23);
            this.button_Save.TabIndex = 4;
            this.button_Save.Text = "Сохранить в базу";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // dataGridView_С
            // 
            this.dataGridView_С.AllowUserToAddRows = false;
            this.dataGridView_С.AllowUserToDeleteRows = false;
            this.dataGridView_С.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_С.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_С.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_С.Name = "dataGridView_С";
            this.dataGridView_С.ReadOnly = true;
            this.dataGridView_С.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_С.Size = new System.Drawing.Size(562, 236);
            this.dataGridView_С.TabIndex = 5;
            // 
            // pictureBox_LCU_PHOTO
            // 
            this.pictureBox_LCU_PHOTO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_LCU_PHOTO.Location = new System.Drawing.Point(3, 275);
            this.pictureBox_LCU_PHOTO.Name = "pictureBox_LCU_PHOTO";
            this.pictureBox_LCU_PHOTO.Size = new System.Drawing.Size(49, 27);
            this.pictureBox_LCU_PHOTO.TabIndex = 3;
            this.pictureBox_LCU_PHOTO.TabStop = false;
            // 
            // LoadPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 345);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LoadPicture";
            this.Text = "Загрузить картинку страны";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_С)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LCU_PHOTO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.PictureBox pictureBox_LCU_PHOTO;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.DataGridView dataGridView_С;
    }
}