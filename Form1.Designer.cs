namespace 蓝奏云批量上传
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.userNameLible = new System.Windows.Forms.Label();
            this.passwordLible = new System.Windows.Forms.Label();
            this.update = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.fileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.jindu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userName = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.Button();
            this.loginStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loadPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userNameLible
            // 
            this.userNameLible.AutoSize = true;
            this.userNameLible.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userNameLible.Location = new System.Drawing.Point(42, 18);
            this.userNameLible.Margin = new System.Windows.Forms.Padding(33, 9, 33, 33);
            this.userNameLible.Name = "userNameLible";
            this.userNameLible.Size = new System.Drawing.Size(87, 22);
            this.userNameLible.TabIndex = 0;
            this.userNameLible.Text = "用户名:";
            // 
            // passwordLible
            // 
            this.passwordLible.AutoSize = true;
            this.passwordLible.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordLible.Location = new System.Drawing.Point(64, 57);
            this.passwordLible.Margin = new System.Windows.Forms.Padding(33, 10, 33, 22);
            this.passwordLible.Name = "passwordLible";
            this.passwordLible.Size = new System.Drawing.Size(76, 22);
            this.passwordLible.TabIndex = 1;
            this.passwordLible.Text = "密码: ";
            this.passwordLible.Click += new System.EventHandler(this.password_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(12, 104);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(787, 23);
            this.update.TabIndex = 3;
            this.update.Text = "开始上传";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileName,
            this.filePath,
            this.fileSize,
            this.status,
            this.jindu});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 162);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(787, 445);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // fileName
            // 
            this.fileName.Text = "文件名称";
            this.fileName.Width = 207;
            // 
            // filePath
            // 
            this.filePath.Text = "文件路径";
            this.filePath.Width = 238;
            // 
            // fileSize
            // 
            this.fileSize.Text = "文件大小";
            this.fileSize.Width = 127;
            // 
            // status
            // 
            this.status.Text = "状态";
            this.status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.status.Width = 92;
            // 
            // jindu
            // 
            this.jindu.Text = "服务器返回内容";
            this.jindu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.jindu.Width = 118;
            // 
            // userName
            // 
            this.userName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userName.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userName.Location = new System.Drawing.Point(165, 18);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(310, 23);
            this.userName.TabIndex = 5;
            this.userName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // password
            // 
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.password.Location = new System.Drawing.Point(165, 57);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(310, 23);
            this.password.TabIndex = 6;
            this.password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(517, 39);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(121, 23);
            this.login.TabIndex = 7;
            this.login.Text = "登陆";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // loginStatus
            // 
            this.loginStatus.AutoSize = true;
            this.loginStatus.BackColor = System.Drawing.SystemColors.Control;
            this.loginStatus.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginStatus.ForeColor = System.Drawing.Color.Red;
            this.loginStatus.Location = new System.Drawing.Point(755, 39);
            this.loginStatus.Name = "loginStatus";
            this.loginStatus.Size = new System.Drawing.Size(32, 22);
            this.loginStatus.TabIndex = 8;
            this.loginStatus.Text = "×";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(651, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 22);
            this.label1.TabIndex = 9;
            this.label1.Text = "登陆状态";
            // 
            // loadPath
            // 
            this.loadPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loadPath.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loadPath.Location = new System.Drawing.Point(12, 133);
            this.loadPath.Name = "loadPath";
            this.loadPath.Size = new System.Drawing.Size(660, 23);
            this.loadPath.TabIndex = 10;
            this.loadPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(678, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "选中目录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 619);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loadPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loginStatus);
            this.Controls.Add(this.login);
            this.Controls.Add(this.password);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.update);
            this.Controls.Add(this.passwordLible);
            this.Controls.Add(this.userNameLible);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userNameLible;
        private System.Windows.Forms.Label passwordLible;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.ColumnHeader fileName;
        private System.Windows.Forms.ColumnHeader filePath;
        private System.Windows.Forms.ColumnHeader fileSize;
        public System.Windows.Forms.TextBox userName;
        public System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button login;
        public System.Windows.Forms.Label loginStatus;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox loadPath;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.ColumnHeader status;
        public System.Windows.Forms.ColumnHeader jindu;
    }
}

