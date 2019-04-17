namespace lanta.Clients
{
    partial class DiscountCard
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
            this.label_Code = new System.Windows.Forms.Label();
            this.textBox_Code = new System.Windows.Forms.TextBox();
            this.label_Number = new System.Windows.Forms.Label();
            this.textBox_Number = new System.Windows.Forms.TextBox();
            this.checkBox_IsValid = new System.Windows.Forms.CheckBox();
            this.label_DSTYPE = new System.Windows.Forms.Label();
            this.comboBox_DSTYPE = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label_DSVALUE = new System.Windows.Forms.Label();
            this.comboBox_DSVALUE = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label_Code, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Code, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_Number, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Number, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_IsValid, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_DSTYPE, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_DSTYPE, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label_DSVALUE, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_DSVALUE, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(271, 144);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label_Code
            // 
            this.label_Code.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Code.AutoSize = true;
            this.label_Code.Location = new System.Drawing.Point(35, 5);
            this.label_Code.Name = "label_Code";
            this.label_Code.Size = new System.Drawing.Size(72, 13);
            this.label_Code.TabIndex = 0;
            this.label_Code.Text = "Серия карты";
            // 
            // textBox_Code
            // 
            this.textBox_Code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Code.Location = new System.Drawing.Point(113, 3);
            this.textBox_Code.Name = "textBox_Code";
            this.textBox_Code.Size = new System.Drawing.Size(155, 20);
            this.textBox_Code.TabIndex = 1;
            // 
            // label_Number
            // 
            this.label_Number.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Number.AutoSize = true;
            this.label_Number.Location = new System.Drawing.Point(32, 28);
            this.label_Number.Name = "label_Number";
            this.label_Number.Size = new System.Drawing.Size(75, 13);
            this.label_Number.TabIndex = 0;
            this.label_Number.Text = "Номер карты";
            // 
            // textBox_Number
            // 
            this.textBox_Number.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Number.Location = new System.Drawing.Point(113, 26);
            this.textBox_Number.Name = "textBox_Number";
            this.textBox_Number.Size = new System.Drawing.Size(155, 20);
            this.textBox_Number.TabIndex = 1;
            // 
            // checkBox_IsValid
            // 
            this.checkBox_IsValid.AutoSize = true;
            this.checkBox_IsValid.Checked = true;
            this.checkBox_IsValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_IsValid.Location = new System.Drawing.Point(113, 49);
            this.checkBox_IsValid.Name = "checkBox_IsValid";
            this.checkBox_IsValid.Size = new System.Drawing.Size(74, 17);
            this.checkBox_IsValid.TabIndex = 2;
            this.checkBox_IsValid.Text = "Активная";
            this.checkBox_IsValid.UseVisualStyleBackColor = true;
            // 
            // label_DSTYPE
            // 
            this.label_DSTYPE.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_DSTYPE.AutoSize = true;
            this.label_DSTYPE.Location = new System.Drawing.Point(42, 74);
            this.label_DSTYPE.Name = "label_DSTYPE";
            this.label_DSTYPE.Size = new System.Drawing.Size(65, 13);
            this.label_DSTYPE.TabIndex = 0;
            this.label_DSTYPE.Text = "Тип скидки";
            // 
            // comboBox_DSTYPE
            // 
            this.comboBox_DSTYPE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_DSTYPE.FormattingEnabled = true;
            this.comboBox_DSTYPE.Location = new System.Drawing.Point(113, 72);
            this.comboBox_DSTYPE.Name = "comboBox_DSTYPE";
            this.comboBox_DSTYPE.Size = new System.Drawing.Size(155, 21);
            this.comboBox_DSTYPE.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(193, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 118);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label_DSVALUE
            // 
            this.label_DSVALUE.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_DSVALUE.AutoSize = true;
            this.label_DSVALUE.Location = new System.Drawing.Point(8, 97);
            this.label_DSVALUE.Name = "label_DSVALUE";
            this.label_DSVALUE.Size = new System.Drawing.Size(99, 13);
            this.label_DSVALUE.TabIndex = 0;
            this.label_DSVALUE.Text = "Размер скидки, %";
            // 
            // comboBox_DSVALUE
            // 
            this.comboBox_DSVALUE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_DSVALUE.FormattingEnabled = true;
            this.comboBox_DSVALUE.Location = new System.Drawing.Point(113, 95);
            this.comboBox_DSVALUE.Name = "comboBox_DSVALUE";
            this.comboBox_DSVALUE.Size = new System.Drawing.Size(155, 21);
            this.comboBox_DSVALUE.TabIndex = 3;
            // 
            // DiscountCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 144);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiscountCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выдача дисконтной карты";
            this.Load += new System.EventHandler(this.DiscountCard_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_Code;
        private System.Windows.Forms.TextBox textBox_Code;
        private System.Windows.Forms.Label label_Number;
        private System.Windows.Forms.TextBox textBox_Number;
        private System.Windows.Forms.CheckBox checkBox_IsValid;
        private System.Windows.Forms.Label label_DSTYPE;
        private System.Windows.Forms.ComboBox comboBox_DSTYPE;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label_DSVALUE;
        private System.Windows.Forms.ComboBox comboBox_DSVALUE;
    }
}