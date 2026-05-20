namespace Quan_ly_Homestay.GUI
{
    partial class FormStaffManagement
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvStaff;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.dgvStaff = new System.Windows.Forms.DataGridView();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).BeginInit();
            this.panelSearch.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.Controls.Add(this.dgvStaff);
            this.panelMain.Controls.Add(this.panelSearch);
            this.panelMain.Location = new System.Drawing.Point(12, 12);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(480, 520);
            this.panelMain.TabIndex = 0;
            // 
            // dgvStaff
            // 
            this.dgvStaff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStaff.AllowUserToAddRows = false;
            this.dgvStaff.AllowUserToDeleteRows = false;
            this.dgvStaff.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStaff.BackgroundColor = UIHelper.BgPanel;
            this.dgvStaff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStaff.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStaff.ColumnHeadersDefaultCellStyle.BackColor = UIHelper.PrimaryColor;
            this.dgvStaff.ColumnHeadersDefaultCellStyle.ForeColor = UIHelper.TextWhite;
            this.dgvStaff.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold);
            this.dgvStaff.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvStaff.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.dgvStaff.ColumnHeadersHeight = 40;
            this.dgvStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvStaff.DefaultCellStyle.BackColor = UIHelper.BgPanel;
            this.dgvStaff.DefaultCellStyle.ForeColor = UIHelper.TextPrimary;
            this.dgvStaff.DefaultCellStyle.Font = new System.Drawing.Font(UIHelper.FontFamily, 9.5F, System.Drawing.FontStyle.Regular);
            this.dgvStaff.DefaultCellStyle.SelectionBackColor = UIHelper.PrimaryLight;
            this.dgvStaff.DefaultCellStyle.SelectionForeColor = UIHelper.TextPrimary;
            this.dgvStaff.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.dgvStaff.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dgvStaff.EnableHeadersVisualStyles = false;
            this.dgvStaff.Location = new System.Drawing.Point(12, 60);
            this.dgvStaff.MultiSelect = false;
            this.dgvStaff.Name = "dgvStaff";
            this.dgvStaff.ReadOnly = true;
            this.dgvStaff.RowHeadersVisible = false;
            this.dgvStaff.RowTemplate.Height = 35;
            this.dgvStaff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStaff.Size = new System.Drawing.Size(456, 445);
            this.dgvStaff.TabIndex = 1;
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.lblCount);
            this.panelSearch.Controls.Add(this.btnRefresh);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.lblSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(480, 50);
            this.panelSearch.TabIndex = 0;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = UIHelper.TextSecondary;
            this.lblCount.Location = new System.Drawing.Point(360, 18);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(45, 15);
            this.lblCount.TabIndex = 3;
            this.lblCount.Text = "Tổng: 0";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = UIHelper.SecondaryColor;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = UIHelper.TextWhite;
            this.btnRefresh.Location = new System.Drawing.Point(285, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(60, 28);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "🔄";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F);
            this.txtSearch.Location = new System.Drawing.Point(65, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = UIHelper.TextPrimary;
            this.lblSearch.Location = new System.Drawing.Point(12, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(47, 19);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "🔍 Tìm";
            // 
            // panelForm
            // 
            this.panelForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForm.BackColor = UIHelper.BgForm;
            this.panelForm.Controls.Add(this.btnClose);
            this.panelForm.Controls.Add(this.btnClear);
            this.panelForm.Controls.Add(this.btnDelete);
            this.panelForm.Controls.Add(this.btnUpdate);
            this.panelForm.Controls.Add(this.btnAdd);
            this.panelForm.Controls.Add(this.cmbRole);
            this.panelForm.Controls.Add(this.lblRole);
            this.panelForm.Controls.Add(this.txtFullName);
            this.panelForm.Controls.Add(this.lblFullName);
            this.panelForm.Controls.Add(this.txtPassword);
            this.panelForm.Controls.Add(this.lblPassword);
            this.panelForm.Controls.Add(this.txtUsername);
            this.panelForm.Controls.Add(this.lblUsername);
            this.panelForm.Controls.Add(this.lblTitle);
            this.panelForm.Location = new System.Drawing.Point(505, 12);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(280, 520);
            this.panelForm.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = UIHelper.NeutralLight;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = UIHelper.TextWhite;
            this.btnClose.Location = new System.Drawing.Point(155, 470);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "✗ Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = UIHelper.WarningColor;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = UIHelper.TextPrimary;
            this.btnClear.Location = new System.Drawing.Point(20, 400);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(240, 35);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "🔄 Làm mới";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = UIHelper.DangerColor;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = UIHelper.TextWhite;
            this.btnDelete.Location = new System.Drawing.Point(20, 355);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(240, 35);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "🗑️ Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = UIHelper.SecondaryColor;
            this.btnUpdate.Enabled = false;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = UIHelper.TextWhite;
            this.btnUpdate.Location = new System.Drawing.Point(20, 310);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(240, 35);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "✏️ Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = UIHelper.SuccessColor;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = UIHelper.TextWhite;
            this.btnAdd.Location = new System.Drawing.Point(20, 265);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(240, 35);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "➕ Thêm mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F);
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "Admin",
            "Staff"});
            this.cmbRole.Location = new System.Drawing.Point(20, 215);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(240, 25);
            this.cmbRole.TabIndex = 8;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = UIHelper.TextPrimary;
            this.lblRole.Location = new System.Drawing.Point(20, 195);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(62, 19);
            this.lblRole.TabIndex = 7;
            this.lblRole.Text = "Vai trò";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F);
            this.txtFullName.Location = new System.Drawing.Point(20, 165);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(240, 25);
            this.txtFullName.TabIndex = 6;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.ForeColor = UIHelper.TextPrimary;
            this.lblFullName.Location = new System.Drawing.Point(20, 145);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(72, 19);
            this.lblFullName.TabIndex = 5;
            this.lblFullName.Text = "Họ tên";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F);
            this.txtPassword.Location = new System.Drawing.Point(20, 115);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(240, 25);
            this.txtPassword.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = UIHelper.TextPrimary;
            this.lblPassword.Location = new System.Drawing.Point(20, 95);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(72, 19);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F);
            this.txtUsername.Location = new System.Drawing.Point(20, 65);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(240, 25);
            this.txtUsername.TabIndex = 2;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = UIHelper.TextPrimary;
            this.lblUsername.Location = new System.Drawing.Point(20, 45);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(103, 19);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Tên đăng nhập";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = UIHelper.PrimaryColor;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý nhân sự";
            // 
            // FormStaffManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = UIHelper.BgForm;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(700, 450);
            this.Name = "FormStaffManagement";
            this.Text = "Quản lý nhân sự - Quản lý Homestay";
            this.Load += new System.EventHandler(this.FormStaffManagement_Load);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).EndInit();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();

            // Layout and styling (moved from code-behind to Designer)
            ApplyDesignTimeStyles();

            this.ResumeLayout(false);

        }

        private void ApplyDesignTimeStyles()
        {
            // Apply title
            UIHelper.StyleFormTitle(this.lblTitle, "Quản Lý Nhân Sự", 18F);

            // Apply buttons
            UIHelper.StyleSuccessButton(this.btnAdd, "Thêm", 90, 35);
            UIHelper.StyleSecondaryButton(this.btnUpdate, "Sửa", 90, 35);
            UIHelper.StyleDangerButton(this.btnDelete, "Xóa", 90, 35);
            UIHelper.StyleNeutralButton(this.btnClear, "Xóa Form", 90, 35);
            UIHelper.StyleNeutralButton(this.btnRefresh, "Làm Mới", 90, 35);
            UIHelper.StyleNeutralButton(this.btnClose, "Đóng", 90, 35);

            // Apply DataGridView
            UIHelper.StyleDataGridView(this.dgvStaff);
        }
    }
}
