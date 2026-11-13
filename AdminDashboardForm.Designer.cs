namespace ST10157545_MunicipalServicesApp
{
    partial class AdminDashboardForm
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
            this.btnCreateAccount = new System.Windows.Forms.Button();
            this.btnBackToMain = new System.Windows.Forms.Button();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabRequests = new System.Windows.Forms.TabPage();
            this.lblSort = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.lvRequests = new System.Windows.Forms.ListView();
            this.colTicket = new System.Windows.Forms.ColumnHeader();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colCategory = new System.Windows.Forms.ColumnHeader();
            this.colWard = new System.Windows.Forms.ColumnHeader();
            this.colPriority = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.colCitizen = new System.Windows.Forms.ColumnHeader();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.lblNewStatus = new System.Windows.Forms.Label();
            this.cmbNewStatus = new System.Windows.Forms.ComboBox();
            this.lblResponse = new System.Windows.Forms.Label();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.btnSaveUpdate = new System.Windows.Forms.Button();
            this.tabUrgent = new System.Windows.Forms.TabPage();
            this.lblUrgent = new System.Windows.Forms.Label();
            this.lstUrgent = new System.Windows.Forms.ListBox();
            this.lblTree = new System.Windows.Forms.Label();
            this.txtCategoryTree = new System.Windows.Forms.TextBox();
            this.tabRoutes = new System.Windows.Forms.TabPage();
            this.lblWard = new System.Windows.Forms.Label();
            this.cmbWard = new System.Windows.Forms.ComboBox();
            this.btnBfs = new System.Windows.Forms.Button();
            this.btnDfs = new System.Windows.Forms.Button();
            this.btnNearby = new System.Windows.Forms.Button();
            this.btnMst = new System.Windows.Forms.Button();
            this.txtGraphOutput = new System.Windows.Forms.TextBox();
            this.pnlHeader.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabRequests.SuspendLayout();
            this.tabUrgent.SuspendLayout();
            this.tabRoutes.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = ST10157545_MunicipalServicesApp.Theme.PrimaryColor;
            this.pnlHeader.Controls.Add(this.btnBackToMain);
            this.pnlHeader.Controls.Add(this.btnCreateAccount);
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(940, 56);
            this.pnlHeader.TabIndex = 0;
            //
            // lblHeader
            //
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 14);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(300, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Service Request Management";
            //
            // btnCreateAccount
            //
            this.btnCreateAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateAccount.BackColor = ST10157545_MunicipalServicesApp.Theme.AccentColor;
            this.btnCreateAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateAccount.ForeColor = System.Drawing.Color.White;
            this.btnCreateAccount.Location = new System.Drawing.Point(600, 12);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(180, 32);
            this.btnCreateAccount.TabIndex = 1;
            this.btnCreateAccount.Text = "Create Staff Account";
            this.btnCreateAccount.UseVisualStyleBackColor = false;
            this.btnCreateAccount.Click += new System.EventHandler(this.btnCreateAccount_Click);
            //
            // btnBackToMain
            //
            this.btnBackToMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackToMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToMain.ForeColor = System.Drawing.Color.White;
            this.btnBackToMain.Location = new System.Drawing.Point(790, 12);
            this.btnBackToMain.Name = "btnBackToMain";
            this.btnBackToMain.Size = new System.Drawing.Size(130, 32);
            this.btnBackToMain.TabIndex = 2;
            this.btnBackToMain.Text = "Back to Menu";
            this.btnBackToMain.UseVisualStyleBackColor = true;
            this.btnBackToMain.Click += new System.EventHandler(this.btnBackToMain_Click);
            //
            // tabs
            //
            this.tabs.Controls.Add(this.tabRequests);
            this.tabs.Controls.Add(this.tabUrgent);
            this.tabs.Controls.Add(this.tabRoutes);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 56);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(940, 584);
            this.tabs.TabIndex = 1;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            //
            // tabRequests
            //
            this.tabRequests.BackColor = ST10157545_MunicipalServicesApp.Theme.BackgroundColor;
            this.tabRequests.Controls.Add(this.btnSaveUpdate);
            this.tabRequests.Controls.Add(this.txtResponse);
            this.tabRequests.Controls.Add(this.lblResponse);
            this.tabRequests.Controls.Add(this.cmbNewStatus);
            this.tabRequests.Controls.Add(this.lblNewStatus);
            this.tabRequests.Controls.Add(this.txtDetails);
            this.tabRequests.Controls.Add(this.lvRequests);
            this.tabRequests.Controls.Add(this.cmbStatusFilter);
            this.tabRequests.Controls.Add(this.lblFilter);
            this.tabRequests.Controls.Add(this.cmbSort);
            this.tabRequests.Controls.Add(this.lblSort);
            this.tabRequests.Location = new System.Drawing.Point(4, 24);
            this.tabRequests.Name = "tabRequests";
            this.tabRequests.Padding = new System.Windows.Forms.Padding(8);
            this.tabRequests.Size = new System.Drawing.Size(932, 556);
            this.tabRequests.TabIndex = 0;
            this.tabRequests.Text = "All Requests";
            //
            // lblSort
            //
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(12, 16);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(48, 15);
            this.lblSort.Text = "Sort by";
            //
            // cmbSort
            //
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.Location = new System.Drawing.Point(66, 12);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(300, 23);
            this.cmbSort.TabIndex = 0;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            //
            // lblFilter
            //
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(386, 16);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(42, 15);
            this.lblFilter.Text = "Status";
            //
            // cmbStatusFilter
            //
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Location = new System.Drawing.Point(434, 12);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(160, 23);
            this.cmbStatusFilter.TabIndex = 1;
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            //
            // lvRequests
            //
            this.lvRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRequests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTicket,
            this.colDate,
            this.colCategory,
            this.colWard,
            this.colPriority,
            this.colStatus,
            this.colCitizen});
            this.lvRequests.FullRowSelect = true;
            this.lvRequests.GridLines = true;
            this.lvRequests.HideSelection = false;
            this.lvRequests.Location = new System.Drawing.Point(12, 44);
            this.lvRequests.MultiSelect = false;
            this.lvRequests.Name = "lvRequests";
            this.lvRequests.Size = new System.Drawing.Size(908, 250);
            this.lvRequests.TabIndex = 2;
            this.lvRequests.UseCompatibleStateImageBehavior = false;
            this.lvRequests.View = System.Windows.Forms.View.Details;
            this.lvRequests.SelectedIndexChanged += new System.EventHandler(this.lvRequests_SelectedIndexChanged);
            //
            // columns
            //
            this.colTicket.Text = "Ticket #";
            this.colTicket.Width = 85;
            this.colDate.Text = "Date Reported";
            this.colDate.Width = 130;
            this.colCategory.Text = "Category";
            this.colCategory.Width = 150;
            this.colWard.Text = "Ward / Location";
            this.colWard.Width = 170;
            this.colPriority.Text = "Priority";
            this.colPriority.Width = 80;
            this.colStatus.Text = "Status";
            this.colStatus.Width = 100;
            this.colCitizen.Text = "Citizen";
            this.colCitizen.Width = 150;
            //
            // txtDetails
            //
            this.txtDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDetails.BackColor = System.Drawing.Color.White;
            this.txtDetails.Location = new System.Drawing.Point(12, 304);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetails.Size = new System.Drawing.Size(908, 90);
            this.txtDetails.TabIndex = 3;
            //
            // lblNewStatus
            //
            this.lblNewStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNewStatus.AutoSize = true;
            this.lblNewStatus.Location = new System.Drawing.Point(12, 410);
            this.lblNewStatus.Name = "lblNewStatus";
            this.lblNewStatus.Size = new System.Drawing.Size(65, 15);
            this.lblNewStatus.Text = "Set status";
            //
            // cmbNewStatus
            //
            this.cmbNewStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbNewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewStatus.Location = new System.Drawing.Point(12, 430);
            this.cmbNewStatus.Name = "cmbNewStatus";
            this.cmbNewStatus.Size = new System.Drawing.Size(160, 23);
            this.cmbNewStatus.TabIndex = 4;
            //
            // lblResponse
            //
            this.lblResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResponse.AutoSize = true;
            this.lblResponse.Location = new System.Drawing.Point(190, 410);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(180, 15);
            this.lblResponse.Text = "Response to the citizen (optional)";
            //
            // txtResponse
            //
            this.txtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponse.Location = new System.Drawing.Point(190, 430);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResponse.Size = new System.Drawing.Size(560, 70);
            this.txtResponse.TabIndex = 5;
            //
            // btnSaveUpdate
            //
            this.btnSaveUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveUpdate.BackColor = ST10157545_MunicipalServicesApp.Theme.AccentColor;
            this.btnSaveUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveUpdate.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveUpdate.ForeColor = System.Drawing.Color.White;
            this.btnSaveUpdate.Location = new System.Drawing.Point(770, 430);
            this.btnSaveUpdate.Name = "btnSaveUpdate";
            this.btnSaveUpdate.Size = new System.Drawing.Size(150, 70);
            this.btnSaveUpdate.TabIndex = 6;
            this.btnSaveUpdate.Text = "Save Update";
            this.btnSaveUpdate.UseVisualStyleBackColor = false;
            this.btnSaveUpdate.Click += new System.EventHandler(this.btnSaveUpdate_Click);
            //
            // tabUrgent
            //
            this.tabUrgent.BackColor = ST10157545_MunicipalServicesApp.Theme.BackgroundColor;
            this.tabUrgent.Controls.Add(this.txtCategoryTree);
            this.tabUrgent.Controls.Add(this.lblTree);
            this.tabUrgent.Controls.Add(this.lstUrgent);
            this.tabUrgent.Controls.Add(this.lblUrgent);
            this.tabUrgent.Location = new System.Drawing.Point(4, 24);
            this.tabUrgent.Name = "tabUrgent";
            this.tabUrgent.Padding = new System.Windows.Forms.Padding(8);
            this.tabUrgent.Size = new System.Drawing.Size(932, 556);
            this.tabUrgent.TabIndex = 1;
            this.tabUrgent.Text = "Urgent Queue and Categories";
            //
            // lblUrgent
            //
            this.lblUrgent.AutoSize = true;
            this.lblUrgent.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 10F, System.Drawing.FontStyle.Bold);
            this.lblUrgent.Location = new System.Drawing.Point(12, 14);
            this.lblUrgent.Name = "lblUrgent";
            this.lblUrgent.Size = new System.Drawing.Size(300, 19);
            this.lblUrgent.Text = "Top 8 most urgent open requests (max-heap)";
            //
            // lstUrgent
            //
            this.lstUrgent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstUrgent.FormattingEnabled = true;
            this.lstUrgent.ItemHeight = 15;
            this.lstUrgent.Location = new System.Drawing.Point(12, 38);
            this.lstUrgent.Name = "lstUrgent";
            this.lstUrgent.Size = new System.Drawing.Size(908, 154);
            this.lstUrgent.TabIndex = 0;
            //
            // lblTree
            //
            this.lblTree.AutoSize = true;
            this.lblTree.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 10F, System.Drawing.FontStyle.Bold);
            this.lblTree.Location = new System.Drawing.Point(12, 206);
            this.lblTree.Name = "lblTree";
            this.lblTree.Size = new System.Drawing.Size(400, 19);
            this.lblTree.Text = "Department / category hierarchy (binary tree, pre-order) with request counts";
            //
            // txtCategoryTree
            //
            this.txtCategoryTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCategoryTree.BackColor = System.Drawing.Color.White;
            this.txtCategoryTree.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtCategoryTree.Location = new System.Drawing.Point(12, 230);
            this.txtCategoryTree.Multiline = true;
            this.txtCategoryTree.Name = "txtCategoryTree";
            this.txtCategoryTree.ReadOnly = true;
            this.txtCategoryTree.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCategoryTree.Size = new System.Drawing.Size(908, 310);
            this.txtCategoryTree.TabIndex = 1;
            //
            // tabRoutes
            //
            this.tabRoutes.BackColor = ST10157545_MunicipalServicesApp.Theme.BackgroundColor;
            this.tabRoutes.Controls.Add(this.txtGraphOutput);
            this.tabRoutes.Controls.Add(this.btnMst);
            this.tabRoutes.Controls.Add(this.btnNearby);
            this.tabRoutes.Controls.Add(this.btnDfs);
            this.tabRoutes.Controls.Add(this.btnBfs);
            this.tabRoutes.Controls.Add(this.cmbWard);
            this.tabRoutes.Controls.Add(this.lblWard);
            this.tabRoutes.Location = new System.Drawing.Point(4, 24);
            this.tabRoutes.Name = "tabRoutes";
            this.tabRoutes.Padding = new System.Windows.Forms.Padding(8);
            this.tabRoutes.Size = new System.Drawing.Size(932, 556);
            this.tabRoutes.TabIndex = 2;
            this.tabRoutes.Text = "Ward Network and Routes";
            //
            // lblWard
            //
            this.lblWard.AutoSize = true;
            this.lblWard.Location = new System.Drawing.Point(12, 16);
            this.lblWard.Name = "lblWard";
            this.lblWard.Size = new System.Drawing.Size(80, 15);
            this.lblWard.Text = "Starting ward";
            //
            // cmbWard
            //
            this.cmbWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWard.Location = new System.Drawing.Point(100, 12);
            this.cmbWard.Name = "cmbWard";
            this.cmbWard.Size = new System.Drawing.Size(240, 23);
            this.cmbWard.TabIndex = 0;
            //
            // btnBfs
            //
            this.btnBfs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBfs.Location = new System.Drawing.Point(12, 48);
            this.btnBfs.Name = "btnBfs";
            this.btnBfs.Size = new System.Drawing.Size(170, 34);
            this.btnBfs.TabIndex = 1;
            this.btnBfs.Text = "Breadth-first traversal";
            this.btnBfs.UseVisualStyleBackColor = true;
            this.btnBfs.Click += new System.EventHandler(this.btnBfs_Click);
            //
            // btnDfs
            //
            this.btnDfs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDfs.Location = new System.Drawing.Point(190, 48);
            this.btnDfs.Name = "btnDfs";
            this.btnDfs.Size = new System.Drawing.Size(170, 34);
            this.btnDfs.TabIndex = 2;
            this.btnDfs.Text = "Depth-first traversal";
            this.btnDfs.UseVisualStyleBackColor = true;
            this.btnDfs.Click += new System.EventHandler(this.btnDfs_Click);
            //
            // btnNearby
            //
            this.btnNearby.BackColor = ST10157545_MunicipalServicesApp.Theme.AccentColor;
            this.btnNearby.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNearby.ForeColor = System.Drawing.Color.White;
            this.btnNearby.Location = new System.Drawing.Point(368, 48);
            this.btnNearby.Name = "btnNearby";
            this.btnNearby.Size = new System.Drawing.Size(250, 34);
            this.btnNearby.TabIndex = 3;
            this.btnNearby.Text = "Nearby open requests (within 2 hops)";
            this.btnNearby.UseVisualStyleBackColor = false;
            this.btnNearby.Click += new System.EventHandler(this.btnNearby_Click);
            //
            // btnMst
            //
            this.btnMst.BackColor = ST10157545_MunicipalServicesApp.Theme.AccentColor;
            this.btnMst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMst.ForeColor = System.Drawing.Color.White;
            this.btnMst.Location = new System.Drawing.Point(626, 48);
            this.btnMst.Name = "btnMst";
            this.btnMst.Size = new System.Drawing.Size(290, 34);
            this.btnMst.TabIndex = 4;
            this.btnMst.Text = "Optimal service route (minimum spanning tree)";
            this.btnMst.UseVisualStyleBackColor = false;
            this.btnMst.Click += new System.EventHandler(this.btnMst_Click);
            //
            // txtGraphOutput
            //
            this.txtGraphOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGraphOutput.BackColor = System.Drawing.Color.White;
            this.txtGraphOutput.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtGraphOutput.Location = new System.Drawing.Point(12, 94);
            this.txtGraphOutput.Multiline = true;
            this.txtGraphOutput.Name = "txtGraphOutput";
            this.txtGraphOutput.ReadOnly = true;
            this.txtGraphOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGraphOutput.Size = new System.Drawing.Size(908, 446);
            this.txtGraphOutput.TabIndex = 5;
            //
            // AdminDashboardForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = ST10157545_MunicipalServicesApp.Theme.BackgroundColor;
            this.ClientSize = new System.Drawing.Size(940, 640);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font(ST10157545_MunicipalServicesApp.Theme.FontFamily, 9F);
            this.MinimumSize = new System.Drawing.Size(900, 640);
            this.Name = "AdminDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Request Management";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabRequests.ResumeLayout(false);
            this.tabRequests.PerformLayout();
            this.tabUrgent.ResumeLayout(false);
            this.tabUrgent.PerformLayout();
            this.tabRoutes.ResumeLayout(false);
            this.tabRoutes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnCreateAccount;
        private System.Windows.Forms.Button btnBackToMain;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabRequests;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.ListView lvRequests;
        private System.Windows.Forms.ColumnHeader colTicket;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.ColumnHeader colWard;
        private System.Windows.Forms.ColumnHeader colPriority;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colCitizen;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.Label lblNewStatus;
        private System.Windows.Forms.ComboBox cmbNewStatus;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Button btnSaveUpdate;
        private System.Windows.Forms.TabPage tabUrgent;
        private System.Windows.Forms.Label lblUrgent;
        private System.Windows.Forms.ListBox lstUrgent;
        private System.Windows.Forms.Label lblTree;
        private System.Windows.Forms.TextBox txtCategoryTree;
        private System.Windows.Forms.TabPage tabRoutes;
        private System.Windows.Forms.Label lblWard;
        private System.Windows.Forms.ComboBox cmbWard;
        private System.Windows.Forms.Button btnBfs;
        private System.Windows.Forms.Button btnDfs;
        private System.Windows.Forms.Button btnNearby;
        private System.Windows.Forms.Button btnMst;
        private System.Windows.Forms.TextBox txtGraphOutput;
    }
}
