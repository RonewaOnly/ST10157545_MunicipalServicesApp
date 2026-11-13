namespace ST10157545_MunicipalServicesApp
{
    partial class LocalEventsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.chkFilterByDate = new System.Windows.Forms.CheckBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.lblResultsCount = new System.Windows.Forms.Label();
            this.lvEvents = new System.Windows.Forms.ListView();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colTitle = new System.Windows.Forms.ColumnHeader();
            this.colCategory = new System.Windows.Forms.ColumnHeader();
            this.colLocation = new System.Windows.Forms.ColumnHeader();
            this.grpRecommended = new System.Windows.Forms.GroupBox();
            this.lstRecommended = new System.Windows.Forms.ListBox();
            this.grpRecentlyViewed = new System.Windows.Forms.GroupBox();
            this.lstRecentlyViewed = new System.Windows.Forms.ListBox();
            this.btnBackToMain = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.grpRecommended.SuspendLayout();
            this.grpRecentlyViewed.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = ST10157545_MunicipalServicesApp.Theme.PrimaryColor;
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(720, 60);
            this.pnlHeader.TabIndex = 0;
            //
            // lblHeader
            //
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(24, 14);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(360, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Local Events and Announcements";
            //
            // lblCategory
            //
            this.lblCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(27, 72);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(60, 15);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Category";
            //
            // cmbCategory
            //
            this.cmbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(30, 92);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(220, 23);
            this.cmbCategory.TabIndex = 2;
            //
            // chkFilterByDate
            //
            this.chkFilterByDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.chkFilterByDate.AutoSize = true;
            this.chkFilterByDate.Location = new System.Drawing.Point(270, 94);
            this.chkFilterByDate.Name = "chkFilterByDate";
            this.chkFilterByDate.Size = new System.Drawing.Size(97, 19);
            this.chkFilterByDate.TabIndex = 3;
            this.chkFilterByDate.Text = "Filter by date";
            this.chkFilterByDate.UseVisualStyleBackColor = true;
            //
            // dtpDate
            //
            this.dtpDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(390, 92);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(150, 23);
            this.dtpDate.TabIndex = 4;
            //
            // lblKeyword
            //
            this.lblKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(27, 128);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(107, 15);
            this.lblKeyword.TabIndex = 5;
            this.lblKeyword.Text = "Keyword (optional)";
            //
            // txtKeyword
            //
            this.txtKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyword.Location = new System.Drawing.Point(30, 148);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(660, 23);
            this.txtKeyword.TabIndex = 6;
            //
            // btnSearch
            //
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.BackColor = ST10157545_MunicipalServicesApp.Theme.AccentColor;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(30, 184);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 32);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            //
            // btnClearFilters
            //
            this.btnClearFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilters.Location = new System.Drawing.Point(160, 184);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(120, 32);
            this.btnClearFilters.TabIndex = 8;
            this.btnClearFilters.Text = "Clear Filters";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            //
            // lblResultsCount
            //
            this.lblResultsCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResultsCount.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 9F, System.Drawing.FontStyle.Italic);
            this.lblResultsCount.ForeColor = System.Drawing.Color.DimGray;
            this.lblResultsCount.Location = new System.Drawing.Point(300, 192);
            this.lblResultsCount.Name = "lblResultsCount";
            this.lblResultsCount.Size = new System.Drawing.Size(390, 18);
            this.lblResultsCount.TabIndex = 9;
            this.lblResultsCount.Text = "Showing all upcoming events, soonest first.";
            //
            // lvEvents
            //
            this.lvEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colTitle,
            this.colCategory,
            this.colLocation});
            this.lvEvents.FullRowSelect = true;
            this.lvEvents.GridLines = true;
            this.lvEvents.HideSelection = false;
            this.lvEvents.Location = new System.Drawing.Point(30, 222);
            this.lvEvents.MultiSelect = false;
            this.lvEvents.Name = "lvEvents";
            this.lvEvents.Size = new System.Drawing.Size(660, 234);
            this.lvEvents.TabIndex = 10;
            this.lvEvents.UseCompatibleStateImageBehavior = false;
            this.lvEvents.View = System.Windows.Forms.View.Details;
            this.lvEvents.DoubleClick += new System.EventHandler(this.lvEvents_DoubleClick);
            //
            // colDate
            //
            this.colDate.Text = "Date";
            this.colDate.Width = 100;
            //
            // colTitle
            //
            this.colTitle.Text = "Title";
            this.colTitle.Width = 240;
            //
            // colCategory
            //
            this.colCategory.Text = "Category";
            this.colCategory.Width = 150;
            //
            // colLocation
            //
            this.colLocation.Text = "Location";
            this.colLocation.Width = 160;
            //
            // grpRecommended
            //
            this.grpRecommended.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRecommended.Controls.Add(this.lstRecommended);
            this.grpRecommended.Location = new System.Drawing.Point(30, 464);
            this.grpRecommended.Name = "grpRecommended";
            this.grpRecommended.Size = new System.Drawing.Size(420, 100);
            this.grpRecommended.TabIndex = 11;
            this.grpRecommended.TabStop = false;
            this.grpRecommended.Text = "Recommended for You";
            //
            // lstRecommended
            //
            this.lstRecommended.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRecommended.FormattingEnabled = true;
            this.lstRecommended.ItemHeight = 15;
            this.lstRecommended.Location = new System.Drawing.Point(3, 19);
            this.lstRecommended.Name = "lstRecommended";
            this.lstRecommended.Size = new System.Drawing.Size(414, 78);
            this.lstRecommended.TabIndex = 0;
            //
            // grpRecentlyViewed
            //
            this.grpRecentlyViewed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRecentlyViewed.Controls.Add(this.lstRecentlyViewed);
            this.grpRecentlyViewed.Location = new System.Drawing.Point(460, 464);
            this.grpRecentlyViewed.Name = "grpRecentlyViewed";
            this.grpRecentlyViewed.Size = new System.Drawing.Size(230, 100);
            this.grpRecentlyViewed.TabIndex = 12;
            this.grpRecentlyViewed.TabStop = false;
            this.grpRecentlyViewed.Text = "Recently Viewed";
            //
            // lstRecentlyViewed
            //
            this.lstRecentlyViewed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRecentlyViewed.FormattingEnabled = true;
            this.lstRecentlyViewed.ItemHeight = 15;
            this.lstRecentlyViewed.Location = new System.Drawing.Point(3, 19);
            this.lstRecentlyViewed.Name = "lstRecentlyViewed";
            this.lstRecentlyViewed.Size = new System.Drawing.Size(224, 78);
            this.lstRecentlyViewed.TabIndex = 0;
            //
            // btnBackToMain
            //
            this.btnBackToMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackToMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToMain.Location = new System.Drawing.Point(460, 572);
            this.btnBackToMain.Name = "btnBackToMain";
            this.btnBackToMain.Size = new System.Drawing.Size(230, 36);
            this.btnBackToMain.TabIndex = 13;
            this.btnBackToMain.Text = "Back to Main Menu";
            this.btnBackToMain.UseVisualStyleBackColor = true;
            this.btnBackToMain.Click += new System.EventHandler(this.btnBackToMain_Click);
            //
            // LocalEventsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = ST10157545_MunicipalServicesApp.Theme.BackgroundColor;
            this.ClientSize = new System.Drawing.Size(720, 620);
            this.Controls.Add(this.btnBackToMain);
            this.Controls.Add(this.grpRecentlyViewed);
            this.Controls.Add(this.grpRecommended);
            this.Controls.Add(this.lvEvents);
            this.Controls.Add(this.lblResultsCount);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.lblKeyword);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.chkFilterByDate);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 9F);
            this.MinimumSize = new System.Drawing.Size(680, 600);
            this.Name = "LocalEventsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Events and Announcements - Municipal Services Application";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpRecommended.ResumeLayout(false);
            this.grpRecentlyViewed.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.CheckBox chkFilterByDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.Label lblResultsCount;
        private System.Windows.Forms.ListView lvEvents;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.ColumnHeader colLocation;
        private System.Windows.Forms.GroupBox grpRecommended;
        private System.Windows.Forms.ListBox lstRecommended;
        private System.Windows.Forms.GroupBox grpRecentlyViewed;
        private System.Windows.Forms.ListBox lstRecentlyViewed;
        private System.Windows.Forms.Button btnBackToMain;
    }
}
