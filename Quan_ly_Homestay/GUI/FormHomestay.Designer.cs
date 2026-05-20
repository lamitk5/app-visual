namespace Quan_ly_Homestay.GUI
{
    partial class FormHomestay
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtHomestayName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvHomestays = new System.Windows.Forms.DataGridView();
            this.groupBoxInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHomestays)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(UIHelper.FontFamily, 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = UIHelper.PrimaryColor;
            this.lblTitle.Location = new System.Drawing.Point(301, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(283, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ HOMESTAY";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfo.Controls.Add(this.txtPhone);
            this.groupBoxInfo.Controls.Add(this.lblPhone);
            this.groupBoxInfo.Controls.Add(this.txtAddress);
            this.groupBoxInfo.Controls.Add(this.lblAddress);
            this.groupBoxInfo.Controls.Add(this.txtHomestayName);
            this.groupBoxInfo.Controls.Add(this.lblName);
            this.groupBoxInfo.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxInfo.ForeColor = UIHelper.TextPrimary;
            this.groupBoxInfo.Location = new System.Drawing.Point(30, 80);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(880, 160);
            this.groupBoxInfo.TabIndex = 1;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Thông tin Homestay";
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.Location = new System.Drawing.Point(600, 30);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(250, 25);
            this.txtPhone.TabIndex = 2;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.ForeColor = UIHelper.TextPrimary;
            this.lblPhone.Location = new System.Drawing.Point(480, 35);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(79, 15);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Số điện thoại:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(150, 75);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(280, 25);
            this.txtAddress.TabIndex = 1;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.ForeColor = UIHelper.TextPrimary;
            this.lblAddress.Location = new System.Drawing.Point(30, 80);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(46, 15);
            this.lblAddress.TabIndex = 2;
            this.lblAddress.Text = "Địa chỉ:";
            // 
            // txtHomestayName
            // 
            this.txtHomestayName.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHomestayName.Location = new System.Drawing.Point(150, 30);
            this.txtHomestayName.Name = "txtHomestayName";
            this.txtHomestayName.Size = new System.Drawing.Size(280, 25);
            this.txtHomestayName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = UIHelper.TextPrimary;
            this.lblName.Location = new System.Drawing.Point(30, 35);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(86, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên Homestay:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAdd.BackColor = UIHelper.SuccessColor;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = UIHelper.TextWhite;
            this.btnAdd.Location = new System.Drawing.Point(180, 260);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 40);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "➕ Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpdate.BackColor = UIHelper.SecondaryColor;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = UIHelper.TextWhite;
            this.btnUpdate.Location = new System.Drawing.Point(320, 260);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 40);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "✏️ Sửa";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelete.BackColor = UIHelper.DangerColor;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = UIHelper.TextWhite;
            this.btnDelete.Location = new System.Drawing.Point(460, 260);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 40);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "🗑️ Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRefresh.BackColor = UIHelper.NeutralColor;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = UIHelper.TextWhite;
            this.btnRefresh.Location = new System.Drawing.Point(600, 260);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvHomestays
            // 
            this.dgvHomestays.AllowUserToAddRows = false;
            this.dgvHomestays.AllowUserToDeleteRows = false;
            this.dgvHomestays.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHomestays.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHomestays.BackgroundColor = UIHelper.BgPanel;
            this.dgvHomestays.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHomestays.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHomestays.ColumnHeadersDefaultCellStyle.BackColor = UIHelper.PrimaryColor;
            this.dgvHomestays.ColumnHeadersDefaultCellStyle.ForeColor = UIHelper.TextWhite;
            this.dgvHomestays.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold);
            this.dgvHomestays.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvHomestays.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.dgvHomestays.ColumnHeadersHeight = 40;
            this.dgvHomestays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvHomestays.DefaultCellStyle.BackColor = UIHelper.BgPanel;
            this.dgvHomestays.DefaultCellStyle.ForeColor = UIHelper.TextPrimary;
            this.dgvHomestays.DefaultCellStyle.Font = new System.Drawing.Font(UIHelper.FontFamily, 9.5F, System.Drawing.FontStyle.Regular);
            this.dgvHomestays.DefaultCellStyle.SelectionBackColor = UIHelper.PrimaryLight;
            this.dgvHomestays.DefaultCellStyle.SelectionForeColor = UIHelper.TextPrimary;
            this.dgvHomestays.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.dgvHomestays.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dgvHomestays.EnableHeadersVisualStyles = false;
            this.dgvHomestays.Location = new System.Drawing.Point(30, 320);
            this.dgvHomestays.MultiSelect = false;
            this.dgvHomestays.Name = "dgvHomestays";
            this.dgvHomestays.ReadOnly = true;
            this.dgvHomestays.RowHeadersVisible = false;
            this.dgvHomestays.RowTemplate.Height = 35;
            this.dgvHomestays.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHomestays.Size = new System.Drawing.Size(880, 230);
            this.dgvHomestays.TabIndex = 6;
            this.dgvHomestays.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHomestays_CellClick);
            // 
            // FormHomestay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = UIHelper.BgForm;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.dgvHomestays);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(750, 450);
            this.Name = "FormHomestay";
            this.Text = "Quản lý Homestay";
            this.Load += new System.EventHandler(this.FormHomestay_Load);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHomestays)).EndInit();

            // Layout and styling (moved from code-behind to Designer)
            ApplyDesignTimeStyles();

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ApplyDesignTimeStyles()
        {
            // Apply title
            UIHelper.StyleFormTitle(this.lblTitle, "Quản Lý Homestay", 18F);

            // Apply group box
            UIHelper.StyleGroupBox(this.groupBoxInfo, "Thông Tin");

            // Apply buttons
            UIHelper.StyleSuccessButton(this.btnAdd, "Thêm", 90, 35);
            UIHelper.StyleSecondaryButton(this.btnUpdate, "Sửa", 90, 35);
            UIHelper.StyleDangerButton(this.btnDelete, "Xóa", 90, 35);
            UIHelper.StyleNeutralButton(this.btnRefresh, "Làm Mới", 90, 35);

            // Apply DataGridView
            UIHelper.StyleDataGridView(this.dgvHomestays);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtHomestayName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvHomestays;
    }
}
