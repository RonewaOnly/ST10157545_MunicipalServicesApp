namespace ST10157545_MunicipalServicesApp
{
    partial class MainForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.btnServiceStatus = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();

            //pnlHeader
            this.pnlHeader.BackColor = ST10157545_MunicipalServicesApp.Theme.PrimaryColor;
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0,0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(560,90);
            this.pnlHeader.TabIndex = 0;

            //lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(24, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(420, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Text = "Municipal Services App";

            //lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSubtitle.Location = new System.Drawing.Point(27, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(320, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Please select a service below to get started.";

            //btnReportIssues
            this.btnReportIssues.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 12F, System.Drawing.FontStyle.Bold);
            this.btnReportIssues.Location = new System.Drawing.Point(26, 116);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(508, 60);
            this.btnReportIssues.TabIndex = 2;
            this.btnReportIssues.Text = "Report Issues";
            this.btnReportIssues.UseVisualStyleBackColor = true;
            this.btnReportIssues.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.btnReportIssues.BackColor = ST10157545_MunicipalServicesApp.Theme.AccentColor;
            this.btnReportIssues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportIssues.Click += new System.EventHandler(this.BtnReportIssues_Click);

            //btnLocalEvents
            this.btnLocalEvents.Enabled = false;
            this.btnLocalEvents.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 12F);
            this.btnLocalEvents.Location = new System.Drawing.Point(26, 196);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(508, 60);
            this.btnLocalEvents.TabIndex = 3;
            this.btnLocalEvents.Text = "Local Events and Announcements (coming soon)";
            this.btnLocalEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocalEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocalEvents.UseVisualStyleBackColor = true;

            //btnServiceStatus
            this.btnServiceStatus.Enabled = false;
            this.btnServiceStatus.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 12F);
            this.btnServiceStatus.Location = new System.Drawing.Point(46, 280);
            this.btnServiceStatus.Name = "btnServiceStatus";
            this.btnServiceStatus.Size = new System.Drawing.Size(420, 60);
            this.btnServiceStatus.TabIndex = 4;
            this.btnServiceStatus.Text = "Service Request Status (coming soon)";
            this.btnServiceStatus.UseVisualStyleBackColor = true;
            this.btnServiceStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));


            //lblFooter
            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 8F, System.Drawing.FontStyle.Italic);
            this.lblFooter.ForeColor = System.Drawing.Color.Gray;
            this.lblFooter.Location = new System.Drawing.Point(26, 356);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(508, 34);
            this.lblFooter.TabIndex = 5;
            this.lblFooter.Text = "Only \"Report Issues\" is implemented in this release.";
            this.lblFooter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));


            //MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510,410);
            this.Controls.Add(this.lblFooter);
            this.Controls.Add(this.btnServiceStatus);
            this.Controls.Add(this.btnLocalEvents);
            this.Controls.Add(this.btnReportIssues);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(460, 420);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Services Application";
            this.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 9F);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnReportIssues;
        private System.Windows.Forms.Button btnLocalEvents;
        private System.Windows.Forms.Button btnServiceStatus;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.Panel pnlHeader;


    }
}

