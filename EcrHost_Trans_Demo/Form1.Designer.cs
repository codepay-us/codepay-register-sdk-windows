
namespace EcrHost_Trans_Demo
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.Disconnect = new System.Windows.Forms.Button();
            this.btnCancelTransaction = new System.Windows.Forms.Button();
            this.btnQueryTransaction = new System.Windows.Forms.Button();
            this.radUsb = new System.Windows.Forms.RadioButton();
            this.radWifi = new System.Windows.Forms.RadioButton();
            this.btnSale = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tbOrderNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnConnectState = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAppId = new System.Windows.Forms.TextBox();
            this.tbTeminalIp = new System.Windows.Forms.TextBox();
            this.lbIp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(76, 140);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(157, 40);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(268, 140);
            this.Disconnect.Margin = new System.Windows.Forms.Padding(4);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(157, 40);
            this.Disconnect.TabIndex = 3;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // btnCancelTransaction
            // 
            this.btnCancelTransaction.Location = new System.Drawing.Point(268, 248);
            this.btnCancelTransaction.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelTransaction.Name = "btnCancelTransaction";
            this.btnCancelTransaction.Size = new System.Drawing.Size(157, 40);
            this.btnCancelTransaction.TabIndex = 5;
            this.btnCancelTransaction.Text = "Cancel";
            this.btnCancelTransaction.UseVisualStyleBackColor = true;
            this.btnCancelTransaction.Click += new System.EventHandler(this.btnCancelTransaction_Click);
            // 
            // btnQueryTransaction
            // 
            this.btnQueryTransaction.Location = new System.Drawing.Point(76, 248);
            this.btnQueryTransaction.Margin = new System.Windows.Forms.Padding(4);
            this.btnQueryTransaction.Name = "btnQueryTransaction";
            this.btnQueryTransaction.Size = new System.Drawing.Size(157, 40);
            this.btnQueryTransaction.TabIndex = 6;
            this.btnQueryTransaction.Text = "Query";
            this.btnQueryTransaction.UseVisualStyleBackColor = true;
            this.btnQueryTransaction.Click += new System.EventHandler(this.btnQueryTransaction_Click);
            // 
            // radUsb
            // 
            this.radUsb.AutoSize = true;
            this.radUsb.Location = new System.Drawing.Point(76, 42);
            this.radUsb.Margin = new System.Windows.Forms.Padding(4);
            this.radUsb.Name = "radUsb";
            this.radUsb.Size = new System.Drawing.Size(52, 19);
            this.radUsb.TabIndex = 7;
            this.radUsb.Text = "USB";
            this.radUsb.UseVisualStyleBackColor = true;
            this.radUsb.CheckedChanged += new System.EventHandler(this.radUsb_CheckedChanged);
            // 
            // radWifi
            // 
            this.radWifi.AutoSize = true;
            this.radWifi.Checked = true;
            this.radWifi.Location = new System.Drawing.Point(268, 42);
            this.radWifi.Margin = new System.Windows.Forms.Padding(4);
            this.radWifi.Name = "radWifi";
            this.radWifi.Size = new System.Drawing.Size(52, 19);
            this.radWifi.TabIndex = 7;
            this.radWifi.TabStop = true;
            this.radWifi.Text = "LAN";
            this.radWifi.UseVisualStyleBackColor = true;
            this.radWifi.CheckedChanged += new System.EventHandler(this.radWifi_CheckedChanged);
            // 
            // btnSale
            // 
            this.btnSale.Location = new System.Drawing.Point(76, 194);
            this.btnSale.Margin = new System.Windows.Forms.Padding(4);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(157, 40);
            this.btnSale.TabIndex = 8;
            this.btnSale.Text = "Sale";
            this.btnSale.UseVisualStyleBackColor = true;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.Location = new System.Drawing.Point(268, 194);
            this.btnRefund.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(157, 40);
            this.btnRefund.TabIndex = 9;
            this.btnRefund.Text = "Refund";
            this.btnRefund.UseVisualStyleBackColor = true;
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(17, 308);
            this.tbLog.Margin = new System.Windows.Forms.Padding(5);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(1031, 238);
            this.tbLog.TabIndex = 12;
            // 
            // tbOrderNumber
            // 
            this.tbOrderNumber.Location = new System.Drawing.Point(771, 78);
            this.tbOrderNumber.Margin = new System.Windows.Forms.Padding(4);
            this.tbOrderNumber.Name = "tbOrderNumber";
            this.tbOrderNumber.Size = new System.Drawing.Size(277, 25);
            this.tbOrderNumber.TabIndex = 13;
            this.tbOrderNumber.Text = "123";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(585, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Merchant Order No:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(673, 118);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Amount:";
            // 
            // tbAmount
            // 
            this.tbAmount.Location = new System.Drawing.Point(771, 113);
            this.tbAmount.Margin = new System.Windows.Forms.Padding(4);
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(277, 25);
            this.tbAmount.TabIndex = 15;
            this.tbAmount.Text = "1";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(879, 248);
            this.btnClearLog.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(157, 40);
            this.btnClearLog.TabIndex = 16;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnConnectState
            // 
            this.btnConnectState.BackColor = System.Drawing.Color.Red;
            this.btnConnectState.Location = new System.Drawing.Point(588, 248);
            this.btnConnectState.Name = "btnConnectState";
            this.btnConnectState.Size = new System.Drawing.Size(277, 40);
            this.btnConnectState.TabIndex = 17;
            this.btnConnectState.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(673, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "App Id:";
            // 
            // tbAppId
            // 
            this.tbAppId.Location = new System.Drawing.Point(771, 44);
            this.tbAppId.Name = "tbAppId";
            this.tbAppId.Size = new System.Drawing.Size(277, 25);
            this.tbAppId.TabIndex = 19;
            this.tbAppId.Text = "wz6012822ca2f1as78";
            // 
            // tbTeminalIp
            // 
            this.tbTeminalIp.Location = new System.Drawing.Point(120, 93);
            this.tbTeminalIp.Name = "tbTeminalIp";
            this.tbTeminalIp.Size = new System.Drawing.Size(305, 25);
            this.tbTeminalIp.TabIndex = 20;
            // 
            // lbIp
            // 
            this.lbIp.AutoSize = true;
            this.lbIp.Location = new System.Drawing.Point(76, 98);
            this.lbIp.Name = "lbIp";
            this.lbIp.Size = new System.Drawing.Size(38, 15);
            this.lbIp.TabIndex = 21;
            this.lbIp.Text = "IP：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.lbIp);
            this.Controls.Add(this.tbTeminalIp);
            this.Controls.Add(this.tbAppId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnConnectState);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbOrderNumber);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnRefund);
            this.Controls.Add(this.btnSale);
            this.Controls.Add(this.radWifi);
            this.Controls.Add(this.radUsb);
            this.Controls.Add(this.btnQueryTransaction);
            this.Controls.Add(this.btnCancelTransaction);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ECR Payment Demo V1.0.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Button btnCancelTransaction;
        private System.Windows.Forms.Button btnQueryTransaction;
        private System.Windows.Forms.RadioButton radUsb;
        private System.Windows.Forms.RadioButton radWifi;
        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.Button btnRefund;
        public System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.TextBox tbOrderNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnConnectState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAppId;
        private System.Windows.Forms.TextBox tbTeminalIp;
        private System.Windows.Forms.Label lbIp;
    }
}

