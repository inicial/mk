namespace terms_prepaid
{
    partial class frmIntroMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntroMain));
            this.pic_Intro = new System.Windows.Forms.PictureBox();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.lbl_Login = new System.Windows.Forms.Label();
            this.edt_Password = new System.Windows.Forms.TextBox();
            this.edt_Login = new System.Windows.Forms.TextBox();
            this.btn_Enter = new System.Windows.Forms.Button();
            this.pic_Close = new System.Windows.Forms.PictureBox();
            this.pic_CloseFrame = new System.Windows.Forms.PictureBox();
            this.pLogin = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_Status = new System.Windows.Forms.TextBox();
            this.pic_Loader = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Intro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_CloseFrame)).BeginInit();
            this.pLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Loader)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Intro
            // 
            this.pic_Intro.Image = global::terms_prepaid.Properties.Resources.img_Intro;
            this.pic_Intro.Location = new System.Drawing.Point(0, 50);
            this.pic_Intro.Name = "pic_Intro";
            this.pic_Intro.Size = new System.Drawing.Size(600, 100);
            this.pic_Intro.TabIndex = 0;
            this.pic_Intro.TabStop = false;
            // 
            // lbl_Password
            // 
            this.lbl_Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(138)))), ((int)(((byte)(201)))));
            this.lbl_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_Password.ForeColor = System.Drawing.Color.White;
            this.lbl_Password.Location = new System.Drawing.Point(50, 42);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(72, 21);
            this.lbl_Password.TabIndex = 17;
            this.lbl_Password.Text = "Пароль:";
            // 
            // lbl_Login
            // 
            this.lbl_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(138)))), ((int)(((byte)(201)))));
            this.lbl_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_Login.ForeColor = System.Drawing.Color.White;
            this.lbl_Login.Location = new System.Drawing.Point(50, 9);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(72, 21);
            this.lbl_Login.TabIndex = 15;
            this.lbl_Login.Text = "Логин:";
            // 
            // edt_Password
            // 
            this.edt_Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(114)))), ((int)(((byte)(163)))));
            this.edt_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edt_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edt_Password.ForeColor = System.Drawing.Color.AliceBlue;
            this.edt_Password.Location = new System.Drawing.Point(125, 39);
            this.edt_Password.Name = "edt_Password";
            this.edt_Password.PasswordChar = '*';
            this.edt_Password.Size = new System.Drawing.Size(150, 26);
            this.edt_Password.TabIndex = 14;
            this.edt_Password.TextChanged += new System.EventHandler(this.edt_Password_TextChanged);
            // 
            // edt_Login
            // 
            this.edt_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(114)))), ((int)(((byte)(163)))));
            this.edt_Login.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edt_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edt_Login.ForeColor = System.Drawing.Color.AliceBlue;
            this.edt_Login.Location = new System.Drawing.Point(125, 6);
            this.edt_Login.Name = "edt_Login";
            this.edt_Login.Size = new System.Drawing.Size(150, 26);
            this.edt_Login.TabIndex = 13;
            this.edt_Login.Tag = "";
            this.edt_Login.TextChanged += new System.EventHandler(this.edt_Login_TextChanged);
            // 
            // btn_Enter
            // 
            this.btn_Enter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(180)))), ((int)(((byte)(245)))));
            this.btn_Enter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Enter.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Enter.Location = new System.Drawing.Point(125, 74);
            this.btn_Enter.Name = "btn_Enter";
            this.btn_Enter.Size = new System.Drawing.Size(150, 30);
            this.btn_Enter.TabIndex = 16;
            this.btn_Enter.Text = "Войти";
            this.btn_Enter.UseVisualStyleBackColor = false;
            this.btn_Enter.Click += new System.EventHandler(this.btn_Enter_Click);
            // 
            // pic_Close
            // 
            this.pic_Close.Location = new System.Drawing.Point(575, 9);
            this.pic_Close.Name = "pic_Close";
            this.pic_Close.Size = new System.Drawing.Size(14, 14);
            this.pic_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Close.TabIndex = 86;
            this.pic_Close.TabStop = false;
            this.pic_Close.Visible = false;
            this.pic_Close.Click += new System.EventHandler(this.pic_Close_Click);
            this.pic_Close.MouseEnter += new System.EventHandler(this.pic_Close_MouseEnter);
            this.pic_Close.MouseLeave += new System.EventHandler(this.pic_Close_MouseLeave);
            // 
            // pic_CloseFrame
            // 
            this.pic_CloseFrame.Location = new System.Drawing.Point(573, 7);
            this.pic_CloseFrame.Name = "pic_CloseFrame";
            this.pic_CloseFrame.Size = new System.Drawing.Size(18, 18);
            this.pic_CloseFrame.TabIndex = 87;
            this.pic_CloseFrame.TabStop = false;
            this.pic_CloseFrame.Visible = false;
            // 
            // pLogin
            // 
            this.pLogin.Controls.Add(this.edt_Login);
            this.pLogin.Controls.Add(this.btn_Enter);
            this.pLogin.Controls.Add(this.edt_Password);
            this.pLogin.Controls.Add(this.lbl_Login);
            this.pLogin.Controls.Add(this.lbl_Password);
            this.pLogin.Controls.Add(this.btn_Close);
            this.pLogin.Location = new System.Drawing.Point(100, 153);
            this.pLogin.Margin = new System.Windows.Forms.Padding(0);
            this.pLogin.Name = "pLogin";
            this.pLogin.Size = new System.Drawing.Size(400, 110);
            this.pLogin.TabIndex = 88;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(180)))), ((int)(((byte)(245)))));
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.Location = new System.Drawing.Point(196, 40);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 18;
            this.btn_Close.TabStop = false;
            this.btn_Close.Text = "Закрыть";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_Status
            // 
            this.txt_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(138)))), ((int)(((byte)(201)))));
            this.txt_Status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Status.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_Status.ForeColor = System.Drawing.Color.AliceBlue;
            this.txt_Status.Location = new System.Drawing.Point(50, 263);
            this.txt_Status.Multiline = true;
            this.txt_Status.Name = "txt_Status";
            this.txt_Status.ReadOnly = true;
            this.txt_Status.Size = new System.Drawing.Size(500, 24);
            this.txt_Status.TabIndex = 18;
            this.txt_Status.TabStop = false;
            this.txt_Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pic_Loader
            // 
            this.pic_Loader.Image = ((System.Drawing.Image)(resources.GetObject("pic_Loader.Image")));
            this.pic_Loader.Location = new System.Drawing.Point(283, 169);
            this.pic_Loader.Name = "pic_Loader";
            this.pic_Loader.Size = new System.Drawing.Size(36, 36);
            this.pic_Loader.TabIndex = 89;
            this.pic_Loader.TabStop = false;
            this.pic_Loader.Visible = false;
            // 
            // frmIntroMain
            // 
            this.AcceptButton = this.btn_Enter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(138)))), ((int)(((byte)(201)))));
            this.CancelButton = this.btn_Close;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.pic_Loader);
            this.Controls.Add(this.pLogin);
            this.Controls.Add(this.pic_Close);
            this.Controls.Add(this.pic_CloseFrame);
            this.Controls.Add(this.txt_Status);
            this.Controls.Add(this.pic_Intro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmIntroMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmIntroMain";
            this.Load += new System.EventHandler(this.frmIntroMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Intro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_CloseFrame)).EndInit();
            this.pLogin.ResumeLayout(false);
            this.pLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Loader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Intro;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.Label lbl_Login;
        private System.Windows.Forms.TextBox edt_Password;
        private System.Windows.Forms.TextBox edt_Login;
        private System.Windows.Forms.Button btn_Enter;
        private System.Windows.Forms.PictureBox pic_Close;
        private System.Windows.Forms.PictureBox pic_CloseFrame;
        private System.Windows.Forms.Panel pLogin;
        private System.Windows.Forms.TextBox txt_Status;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.PictureBox pic_Loader;
    }
}