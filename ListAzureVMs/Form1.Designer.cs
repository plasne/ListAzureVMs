namespace ListAzureVMs
{
    partial class Form1
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
            this.asmLogin = new System.Windows.Forms.Button();
            this.asmQuery = new System.Windows.Forms.Button();
            this.listResults = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.armQuery = new System.Windows.Forms.Button();
            this.armLogin = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // asmLogin
            // 
            this.asmLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.asmLogin.Location = new System.Drawing.Point(996, 12);
            this.asmLogin.Name = "asmLogin";
            this.asmLogin.Size = new System.Drawing.Size(151, 73);
            this.asmLogin.TabIndex = 0;
            this.asmLogin.Text = "login";
            this.asmLogin.UseVisualStyleBackColor = true;
            this.asmLogin.Click += new System.EventHandler(this.asmLogin_Click);
            // 
            // asmQuery
            // 
            this.asmQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.asmQuery.Location = new System.Drawing.Point(996, 100);
            this.asmQuery.Name = "asmQuery";
            this.asmQuery.Size = new System.Drawing.Size(151, 70);
            this.asmQuery.TabIndex = 1;
            this.asmQuery.Text = "query";
            this.asmQuery.UseVisualStyleBackColor = true;
            this.asmQuery.Click += new System.EventHandler(this.asmQuery_Click);
            // 
            // listResults
            // 
            this.listResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listResults.FormattingEnabled = true;
            this.listResults.ItemHeight = 25;
            this.listResults.Location = new System.Drawing.Point(169, 12);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(821, 704);
            this.listResults.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 719);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1159, 37);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(223, 32);
            this.toolStripStatusLabel1.Text = "Click login to start...";
            // 
            // armQuery
            // 
            this.armQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.armQuery.Location = new System.Drawing.Point(12, 100);
            this.armQuery.Name = "armQuery";
            this.armQuery.Size = new System.Drawing.Size(151, 70);
            this.armQuery.TabIndex = 5;
            this.armQuery.Text = "query";
            this.armQuery.UseVisualStyleBackColor = true;
            this.armQuery.Click += new System.EventHandler(this.armQuery_Click);
            // 
            // armLogin
            // 
            this.armLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.armLogin.Location = new System.Drawing.Point(12, 12);
            this.armLogin.Name = "armLogin";
            this.armLogin.Size = new System.Drawing.Size(151, 73);
            this.armLogin.TabIndex = 4;
            this.armLogin.Text = "login";
            this.armLogin.UseVisualStyleBackColor = true;
            this.armLogin.Click += new System.EventHandler(this.armLogin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 756);
            this.Controls.Add(this.armQuery);
            this.Controls.Add(this.armLogin);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listResults);
            this.Controls.Add(this.asmQuery);
            this.Controls.Add(this.asmLogin);
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button asmLogin;
        private System.Windows.Forms.Button asmQuery;
        private System.Windows.Forms.ListBox listResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button armQuery;
        private System.Windows.Forms.Button armLogin;
    }
}

