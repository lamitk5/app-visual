namespace Quan_ly_Homestay.GUI
{
    partial class Room
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.Panel panelOuter;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.Label lblLegendRed;
        private System.Windows.Forms.Label lblLegendGreen;
        private System.Windows.Forms.Label lblLegendYellow;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDoiHome;
        private System.Windows.Forms.Button btnThemPhong;
        private System.Windows.Forms.Button btnSuaPhong;
        private System.Windows.Forms.Button btnXoaPhong;
        private System.Windows.Forms.Panel panelGrid;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelOuter = new System.Windows.Forms.Panel();
            this.panelBody = new System.Windows.Forms.Panel();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDoiHome = new System.Windows.Forms.Button();
            this.btnThemPhong = new System.Windows.Forms.Button();
            this.btnSuaPhong = new System.Windows.Forms.Button();
            this.btnXoaPhong = new System.Windows.Forms.Button();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.lblLegendRed = new System.Windows.Forms.Label();
            this.lblLegendGreen = new System.Windows.Forms.Label();
            this.lblLegendYellow = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelOuter.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelLegend.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelOuter
            // 
            this.panelOuter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOuter.BackColor = UIHelper.BgPanel;
            this.panelOuter.Controls.Add(this.panelBody);
            this.panelOuter.Controls.Add(this.panelHeader);
            this.panelOuter.Location = new System.Drawing.Point(17, 17);
            this.panelOuter.Name = "panelOuter";
            this.panelOuter.Size = new System.Drawing.Size(866, 566);
            this.panelOuter.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.BackColor = UIHelper.BgPanel;
            this.panelBody.Controls.Add(this.panelGrid);
            this.panelBody.Controls.Add(this.panelLeft);
            this.panelBody.Controls.Add(this.panelLegend);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 38);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(7);
            this.panelBody.Size = new System.Drawing.Size(866, 528);
            this.panelBody.TabIndex = 1;
            // 
            // panelGrid
            // 
            this.panelGrid.AutoScroll = true;
            this.panelGrid.BackColor = UIHelper.BgForm;
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(101, 42);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(3);
            this.panelGrid.Size = new System.Drawing.Size(758, 479);
            this.panelGrid.TabIndex = 2;
            this.panelGrid.Resize += new System.EventHandler(this.panelGrid_Resize);
            //
            // panelLeft
            //
            this.panelLeft.BackColor = UIHelper.BgPanel;
            this.panelLeft.Controls.Add(this.btnRefresh);
            this.panelLeft.Controls.Add(this.btnDoiHome);
            this.panelLeft.Controls.Add(this.btnThemPhong);
            this.panelLeft.Controls.Add(this.btnSuaPhong);
            this.panelLeft.Controls.Add(this.btnXoaPhong);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(7, 42);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(7, 9, 7, 0);
            this.panelLeft.Size = new System.Drawing.Size(94, 479);
            this.panelLeft.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = UIHelper.SecondaryColor;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = UIHelper.TextWhite;
            this.btnRefresh.Location = new System.Drawing.Point(7, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(77, 31);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDoiHome
            // 
            this.btnDoiHome.BackColor = UIHelper.WarningColor;
            this.btnDoiHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoiHome.FlatAppearance.BorderSize = 0;
            this.btnDoiHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoiHome.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnDoiHome.ForeColor = UIHelper.TextPrimary;
            this.btnDoiHome.Location = new System.Drawing.Point(7, 47);
            this.btnDoiHome.Name = "btnDoiHome";
            this.btnDoiHome.Size = new System.Drawing.Size(77, 31);
            this.btnDoiHome.TabIndex = 1;
            this.btnDoiHome.Text = "Đổi home";
            this.btnDoiHome.UseVisualStyleBackColor = false;
            this.btnDoiHome.Click += new System.EventHandler(this.btnDoiHome_Click);
            //
            // btnThemPhong
            //
            this.btnThemPhong.BackColor = UIHelper.SuccessColor;
            this.btnThemPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemPhong.FlatAppearance.BorderSize = 0;
            this.btnThemPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemPhong.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnThemPhong.ForeColor = UIHelper.TextWhite;
            this.btnThemPhong.Location = new System.Drawing.Point(7, 85);
            this.btnThemPhong.Name = "btnThemPhong";
            this.btnThemPhong.Size = new System.Drawing.Size(77, 31);
            this.btnThemPhong.TabIndex = 2;
            this.btnThemPhong.Text = "➕ Thêm";
            this.btnThemPhong.UseVisualStyleBackColor = false;
            this.btnThemPhong.Click += new System.EventHandler(this.btnThemPhong_Click);
            //
            // btnSuaPhong
            //
            this.btnSuaPhong.BackColor = UIHelper.SecondaryColor;
            this.btnSuaPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuaPhong.FlatAppearance.BorderSize = 0;
            this.btnSuaPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuaPhong.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnSuaPhong.ForeColor = UIHelper.TextWhite;
            this.btnSuaPhong.Location = new System.Drawing.Point(7, 123);
            this.btnSuaPhong.Name = "btnSuaPhong";
            this.btnSuaPhong.Size = new System.Drawing.Size(77, 31);
            this.btnSuaPhong.TabIndex = 3;
            this.btnSuaPhong.Text = "✏️ Sửa";
            this.btnSuaPhong.UseVisualStyleBackColor = false;
            this.btnSuaPhong.Click += new System.EventHandler(this.btnSuaPhong_Click);
            //
            // btnXoaPhong
            //
            this.btnXoaPhong.BackColor = UIHelper.DangerColor;
            this.btnXoaPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaPhong.FlatAppearance.BorderSize = 0;
            this.btnXoaPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaPhong.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaPhong.ForeColor = UIHelper.TextWhite;
            this.btnXoaPhong.Location = new System.Drawing.Point(7, 161);
            this.btnXoaPhong.Name = "btnXoaPhong";
            this.btnXoaPhong.Size = new System.Drawing.Size(77, 31);
            this.btnXoaPhong.TabIndex = 4;
            this.btnXoaPhong.Text = "🗑️ Xóa";
            this.btnXoaPhong.UseVisualStyleBackColor = false;
            this.btnXoaPhong.Click += new System.EventHandler(this.btnXoaPhong_Click);
            //
            // panelLegend
            //
            this.panelLegend.BackColor = UIHelper.BgPanel;
            this.panelLegend.Controls.Add(this.label1);
            this.panelLegend.Controls.Add(this.lblLegendRed);
            this.panelLegend.Controls.Add(this.lblLegendGreen);
            this.panelLegend.Controls.Add(this.lblLegendYellow);
            this.panelLegend.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLegend.Location = new System.Drawing.Point(7, 7);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.panelLegend.Size = new System.Drawing.Size(852, 35);
            this.panelLegend.TabIndex = 0;
            // 
            // lblLegendRed
            // 
            this.lblLegendRed.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLegendRed.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.lblLegendRed.ForeColor = UIHelper.DangerColor;
            this.lblLegendRed.Location = new System.Drawing.Point(539, 0);
            this.lblLegendRed.Name = "lblLegendRed";
            this.lblLegendRed.Size = new System.Drawing.Size(105, 35);
            this.lblLegendRed.TabIndex = 0;
            this.lblLegendRed.Text = "● Đang dùng";
            this.lblLegendRed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLegendGreen
            // 
            this.lblLegendGreen.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLegendGreen.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.lblLegendGreen.ForeColor = UIHelper.SuccessColor;
            this.lblLegendGreen.Location = new System.Drawing.Point(644, 0);
            this.lblLegendGreen.Name = "lblLegendGreen";
            this.lblLegendGreen.Size = new System.Drawing.Size(91, 35);
            this.lblLegendGreen.TabIndex = 1;
            this.lblLegendGreen.Text = "● Trống";
            this.lblLegendGreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLegendYellow
            // 
            this.lblLegendYellow.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLegendYellow.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.lblLegendYellow.ForeColor = UIHelper.WarningColor;
            this.lblLegendYellow.Location = new System.Drawing.Point(735, 0);
            this.lblLegendYellow.Name = "lblLegendYellow";
            this.lblLegendYellow.Size = new System.Drawing.Size(110, 35);
            this.lblLegendYellow.TabIndex = 2;
            this.lblLegendYellow.Text = "● Đang dọn";
            this.lblLegendYellow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = UIHelper.SecondaryColor;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(866, 38);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font(UIHelper.FontFamily, 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = UIHelper.TextWhite;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(866, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📍 Sơ đồ phòng tại: ....";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = UIHelper.NeutralColor;
            this.label1.Location = new System.Drawing.Point(420, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 35);
            this.label1.TabIndex = 3;
            this.label1.Text = "● Bảo trì";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = UIHelper.BgForm;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelOuter);
            this.MinimumSize = new System.Drawing.Size(700, 450);
            this.Name = "Room";
            this.Text = "Sơ Đồ Phòng - Quản lý Homestay";
            this.Load += new System.EventHandler(this.Room_Load);
            this.panelOuter.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLegend.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);

            // Layout and styling (moved from code-behind to Designer)
            ApplyDesignTimeStyles();

            this.ResumeLayout(false);

        }

        private void ApplyDesignTimeStyles()
        {
            // Apply title
            UIHelper.StyleFormTitle(this.lblTitle, "Quản Lý Phòng", 18F);

            // Apply buttons
            UIHelper.StylePrimaryButton(this.btnThemPhong, "Thêm", 90, 35);
            UIHelper.StyleSecondaryButton(this.btnSuaPhong, "Sửa", 90, 35);
            UIHelper.StyleDangerButton(this.btnXoaPhong, "Xóa", 90, 35);
            UIHelper.StyleNeutralButton(this.btnRefresh, "Làm Mới", 90, 35);
            UIHelper.StyleWarningButton(this.btnDoiHome, "Đổi Home", 90, 35);
        }

        #endregion

        private System.Windows.Forms.Label label1;
    }
}
