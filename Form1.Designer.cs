namespace RemoteAccessTool
{
    partial class RAT
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabConnections = new System.Windows.Forms.TabPage();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.BtnStart = new System.Windows.Forms.Button();
            this.TxtPort = new System.Windows.Forms.TextBox();
            this.LblPort = new System.Windows.Forms.Label();
            this.TxtIp = new System.Windows.Forms.TextBox();
            this.Lblip = new System.Windows.Forms.Label();
            this.ListConnection = new System.Windows.Forms.ListView();
            this.ColumnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnOs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnArch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnRam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnCPU = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnWindow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnInstallTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnCountry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabAutoTask = new System.Windows.Forms.TabPage();
            this.TabProxy = new System.Windows.Forms.TabPage();
            this.TabSettingBuilder = new System.Windows.Forms.TabPage();
            this.TabLogs = new System.Windows.Forms.TabPage();
            this.TabAbout = new System.Windows.Forms.TabPage();
            this.TabControl1.SuspendLayout();
            this.TabConnections.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabConnections);
            this.TabControl1.Controls.Add(this.TabAutoTask);
            this.TabControl1.Controls.Add(this.TabProxy);
            this.TabControl1.Controls.Add(this.TabSettingBuilder);
            this.TabControl1.Controls.Add(this.TabLogs);
            this.TabControl1.Controls.Add(this.TabAbout);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl1.Location = new System.Drawing.Point(0, 0);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(1417, 548);
            this.TabControl1.TabIndex = 0;
            // 
            // TabConnections
            // 
            this.TabConnections.Controls.Add(this.LabelInfo);
            this.TabConnections.Controls.Add(this.BtnStart);
            this.TabConnections.Controls.Add(this.TxtPort);
            this.TabConnections.Controls.Add(this.LblPort);
            this.TabConnections.Controls.Add(this.TxtIp);
            this.TabConnections.Controls.Add(this.Lblip);
            this.TabConnections.Controls.Add(this.ListConnection);
            this.TabConnections.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabConnections.Location = new System.Drawing.Point(4, 25);
            this.TabConnections.Name = "TabConnections";
            this.TabConnections.Padding = new System.Windows.Forms.Padding(3);
            this.TabConnections.Size = new System.Drawing.Size(1409, 519);
            this.TabConnections.TabIndex = 0;
            this.TabConnections.Text = "Connections";
            this.TabConnections.UseVisualStyleBackColor = true;
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.Location = new System.Drawing.Point(527, 496);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(116, 16);
            this.LabelInfo.TabIndex = 6;
            this.LabelInfo.Text = "Server Stopped";
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(445, 493);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 5;
            this.BtnStart.Text = "START";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // TxtPort
            // 
            this.TxtPort.Location = new System.Drawing.Point(339, 493);
            this.TxtPort.Name = "TxtPort";
            this.TxtPort.Size = new System.Drawing.Size(100, 22);
            this.TxtPort.TabIndex = 4;
            // 
            // LblPort
            // 
            this.LblPort.AutoSize = true;
            this.LblPort.Location = new System.Drawing.Point(299, 496);
            this.LblPort.Name = "LblPort";
            this.LblPort.Size = new System.Drawing.Size(43, 16);
            this.LblPort.TabIndex = 3;
            this.LblPort.Text = "Port :";
            // 
            // TxtIp
            // 
            this.TxtIp.Location = new System.Drawing.Point(91, 493);
            this.TxtIp.Name = "TxtIp";
            this.TxtIp.Size = new System.Drawing.Size(202, 22);
            this.TxtIp.TabIndex = 2;
            // 
            // Lblip
            // 
            this.Lblip.AutoSize = true;
            this.Lblip.Location = new System.Drawing.Point(6, 496);
            this.Lblip.Name = "Lblip";
            this.Lblip.Size = new System.Drawing.Size(87, 16);
            this.Lblip.TabIndex = 1;
            this.Lblip.Text = "IP/Domain :";
            // 
            // ListConnection
            // 
            this.ListConnection.BackColor = System.Drawing.Color.Black;
            this.ListConnection.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnID,
            this.ColumnName,
            this.ColumnOs,
            this.ColumnArch,
            this.ColumnIP,
            this.ColumnPort,
            this.ColumnRam,
            this.ColumnCPU,
            this.ColumnWindow,
            this.ColumnInstallTime,
            this.ColumnCountry});
            this.ListConnection.Dock = System.Windows.Forms.DockStyle.Top;
            this.ListConnection.ForeColor = System.Drawing.Color.LimeGreen;
            this.ListConnection.FullRowSelect = true;
            this.ListConnection.HideSelection = false;
            this.ListConnection.Location = new System.Drawing.Point(3, 3);
            this.ListConnection.MultiSelect = false;
            this.ListConnection.Name = "ListConnection";
            this.ListConnection.Size = new System.Drawing.Size(1403, 487);
            this.ListConnection.TabIndex = 0;
            this.ListConnection.UseCompatibleStateImageBehavior = false;
            this.ListConnection.View = System.Windows.Forms.View.Details;
            // 
            // ColumnID
            // 
            this.ColumnID.Text = "Client ID";
            this.ColumnID.Width = 85;
            // 
            // ColumnName
            // 
            this.ColumnName.Text = "Name";
            this.ColumnName.Width = 165;
            // 
            // ColumnOs
            // 
            this.ColumnOs.Text = "Operating System";
            this.ColumnOs.Width = 155;
            // 
            // ColumnArch
            // 
            this.ColumnArch.Text = "Arch";
            this.ColumnArch.Width = 72;
            // 
            // ColumnIP
            // 
            this.ColumnIP.Text = "IP";
            this.ColumnIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnIP.Width = 192;
            // 
            // ColumnPort
            // 
            this.ColumnPort.Text = "Port";
            this.ColumnPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ColumnRam
            // 
            this.ColumnRam.Text = "RAM";
            this.ColumnRam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnRam.Width = 121;
            // 
            // ColumnCPU
            // 
            this.ColumnCPU.Text = "CPU";
            this.ColumnCPU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnCPU.Width = 158;
            // 
            // ColumnWindow
            // 
            this.ColumnWindow.Text = "Windows";
            this.ColumnWindow.Width = 81;
            // 
            // ColumnInstallTime
            // 
            this.ColumnInstallTime.Text = "Installation Time";
            this.ColumnInstallTime.Width = 127;
            // 
            // ColumnCountry
            // 
            this.ColumnCountry.Text = "Country";
            this.ColumnCountry.Width = 168;
            // 
            // TabAutoTask
            // 
            this.TabAutoTask.Location = new System.Drawing.Point(4, 25);
            this.TabAutoTask.Name = "TabAutoTask";
            this.TabAutoTask.Padding = new System.Windows.Forms.Padding(3);
            this.TabAutoTask.Size = new System.Drawing.Size(1011, 519);
            this.TabAutoTask.TabIndex = 1;
            this.TabAutoTask.Text = "Automatic Taks";
            this.TabAutoTask.UseVisualStyleBackColor = true;
            // 
            // TabProxy
            // 
            this.TabProxy.Location = new System.Drawing.Point(4, 25);
            this.TabProxy.Name = "TabProxy";
            this.TabProxy.Padding = new System.Windows.Forms.Padding(3);
            this.TabProxy.Size = new System.Drawing.Size(1011, 519);
            this.TabProxy.TabIndex = 2;
            this.TabProxy.Text = "Proxy Servers";
            this.TabProxy.UseVisualStyleBackColor = true;
            // 
            // TabSettingBuilder
            // 
            this.TabSettingBuilder.Location = new System.Drawing.Point(4, 25);
            this.TabSettingBuilder.Name = "TabSettingBuilder";
            this.TabSettingBuilder.Padding = new System.Windows.Forms.Padding(3);
            this.TabSettingBuilder.Size = new System.Drawing.Size(1011, 519);
            this.TabSettingBuilder.TabIndex = 3;
            this.TabSettingBuilder.Text = "Settings/Builder";
            this.TabSettingBuilder.UseVisualStyleBackColor = true;
            // 
            // TabLogs
            // 
            this.TabLogs.Location = new System.Drawing.Point(4, 25);
            this.TabLogs.Name = "TabLogs";
            this.TabLogs.Padding = new System.Windows.Forms.Padding(3);
            this.TabLogs.Size = new System.Drawing.Size(1011, 519);
            this.TabLogs.TabIndex = 4;
            this.TabLogs.Text = "Logs";
            this.TabLogs.UseVisualStyleBackColor = true;
            // 
            // TabAbout
            // 
            this.TabAbout.Location = new System.Drawing.Point(4, 25);
            this.TabAbout.Name = "TabAbout";
            this.TabAbout.Padding = new System.Windows.Forms.Padding(3);
            this.TabAbout.Size = new System.Drawing.Size(1011, 519);
            this.TabAbout.TabIndex = 5;
            this.TabAbout.Text = "About";
            this.TabAbout.UseVisualStyleBackColor = true;
            // 
            // RAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 548);
            this.Controls.Add(this.TabControl1);
            this.Name = "RAT";
            this.Text = "RAT";
            this.TabControl1.ResumeLayout(false);
            this.TabConnections.ResumeLayout(false);
            this.TabConnections.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage TabConnections;
        private System.Windows.Forms.TabPage TabAutoTask;
        private System.Windows.Forms.TabPage TabProxy;
        private System.Windows.Forms.TabPage TabSettingBuilder;
        private System.Windows.Forms.ListView ListConnection;
        private System.Windows.Forms.ColumnHeader ColumnID;
        private System.Windows.Forms.ColumnHeader ColumnName;
        private System.Windows.Forms.ColumnHeader ColumnOs;
        private System.Windows.Forms.ColumnHeader ColumnArch;
        private System.Windows.Forms.ColumnHeader ColumnIP;
        private System.Windows.Forms.ColumnHeader ColumnPort;
        private System.Windows.Forms.ColumnHeader ColumnRam;
        private System.Windows.Forms.ColumnHeader ColumnCPU;
        private System.Windows.Forms.ColumnHeader ColumnWindow;
        private System.Windows.Forms.ColumnHeader ColumnInstallTime;
        private System.Windows.Forms.TabPage TabLogs;
        private System.Windows.Forms.TabPage TabAbout;
        private System.Windows.Forms.ColumnHeader ColumnCountry;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.TextBox TxtPort;
        private System.Windows.Forms.Label LblPort;
        private System.Windows.Forms.TextBox TxtIp;
        private System.Windows.Forms.Label Lblip;
        private System.Windows.Forms.Label LabelInfo;
    }
}

