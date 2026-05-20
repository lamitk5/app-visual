namespace Quan_ly_Homestay.GUI
{
    partial class BookingUI
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

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();

            // Homestay selection
            this.pnlHomestay = new System.Windows.Forms.Panel();
            this.btnSelectHomestay = new System.Windows.Forms.Button();
            this.lblHomestayLabel = new System.Windows.Forms.Label();
            this.lblHomestayName = new System.Windows.Forms.Label();

            // Customer info
            this.grpCustomer = new System.Windows.Forms.GroupBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblIdCard = new System.Windows.Forms.Label();
            this.txtIdCard = new System.Windows.Forms.TextBox();

            // Booking info
            this.grpBooking = new System.Windows.Forms.GroupBox();
            this.lblCheckIn = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.lblDeposit = new System.Windows.Forms.Label();
            this.numDeposit = new System.Windows.Forms.NumericUpDown();

            // Room selection
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblRoom = new System.Windows.Forms.Label();
            this.cmbRooms = new System.Windows.Forms.ComboBox();

            // Book button
            this.btnBook = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numDeposit)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.pnlHomestay.SuspendLayout();
            this.grpCustomer.SuspendLayout();
            this.grpBooking.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = UIHelper.PrimaryColor;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font(UIHelper.FontFamily, 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.ForeColor = UIHelper.TextWhite;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(800, 60);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "HỆ THỐNG ĐẶT PHÒNG HOMESTAY";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = UIHelper.BgForm;
            this.pnlContent.Controls.Add(this.pnlHomestay);
            this.pnlContent.Controls.Add(this.grpCustomer);
            this.pnlContent.Controls.Add(this.grpBooking);
            this.pnlContent.Controls.Add(this.lblRoom);
            this.pnlContent.Controls.Add(this.cmbRooms);
            this.pnlContent.Controls.Add(this.btnBook);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlContent.Size = new System.Drawing.Size(800, 310);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlHomestay
            // 
            this.pnlHomestay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHomestay.BackColor = UIHelper.BgPanel;
            this.pnlHomestay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHomestay.Controls.Add(this.lblHomestayLabel);
            this.pnlHomestay.Controls.Add(this.lblHomestayName);
            this.pnlHomestay.Controls.Add(this.btnRefresh);
            this.pnlHomestay.Controls.Add(this.btnSelectHomestay);
            this.pnlHomestay.Location = new System.Drawing.Point(20, 10);
            this.pnlHomestay.Name = "pnlHomestay";
            this.pnlHomestay.Size = new System.Drawing.Size(740, 50);
            this.pnlHomestay.TabIndex = 20;
            // 
            // lblHomestayLabel
            // 
            this.lblHomestayLabel.AutoSize = true;
            this.lblHomestayLabel.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHomestayLabel.Location = new System.Drawing.Point(10, 14);
            this.lblHomestayLabel.Name = "lblHomestayLabel";
            this.lblHomestayLabel.Size = new System.Drawing.Size(84, 19);
            this.lblHomestayLabel.TabIndex = 0;
            this.lblHomestayLabel.Text = "Homestay:";
            // 
            // lblHomestayName
            // 
            this.lblHomestayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHomestayName.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblHomestayName.ForeColor = UIHelper.TextSecondary;
            this.lblHomestayName.Location = new System.Drawing.Point(100, 14);
            this.lblHomestayName.Name = "lblHomestayName";
            this.lblHomestayName.Size = new System.Drawing.Size(450, 19);
            this.lblHomestayName.TabIndex = 1;
            this.lblHomestayName.Text = "Chưa chọn homestay";
            this.lblHomestayName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnRefresh
            //
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = UIHelper.SecondaryColor;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRefresh.ForeColor = UIHelper.TextWhite;
            this.btnRefresh.Location = new System.Drawing.Point(548, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 32);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "🔄";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            //
            // btnSelectHomestay
            //
            this.btnSelectHomestay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectHomestay.BackColor = UIHelper.PrimaryColor;
            this.btnSelectHomestay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectHomestay.FlatAppearance.BorderSize = 0;
            this.btnSelectHomestay.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSelectHomestay.ForeColor = UIHelper.TextWhite;
            this.btnSelectHomestay.Location = new System.Drawing.Point(590, 8);
            this.btnSelectHomestay.Name = "btnSelectHomestay";
            this.btnSelectHomestay.Size = new System.Drawing.Size(140, 32);
            this.btnSelectHomestay.TabIndex = 2;
            this.btnSelectHomestay.Text = "🏠 Chọn Home";
            this.btnSelectHomestay.UseVisualStyleBackColor = false;
            this.btnSelectHomestay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectHomestay.Click += new System.EventHandler(this.btnSelectHomestay_Click);
            // 
            // grpCustomer
            // 
            this.grpCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCustomer.Controls.Add(this.lblFullName);
            this.grpCustomer.Controls.Add(this.txtFullName);
            this.grpCustomer.Controls.Add(this.lblPhone);
            this.grpCustomer.Controls.Add(this.txtPhone);
            this.grpCustomer.Controls.Add(this.lblIdCard);
            this.grpCustomer.Controls.Add(this.txtIdCard);
            this.grpCustomer.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpCustomer.Location = new System.Drawing.Point(20, 70);
            this.grpCustomer.Name = "grpCustomer";
            this.grpCustomer.Size = new System.Drawing.Size(740, 100);
            this.grpCustomer.TabIndex = 21;
            this.grpCustomer.TabStop = false;
            this.grpCustomer.Text = "Thông tin khách hàng";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFullName.Location = new System.Drawing.Point(15, 32);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(61, 15);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Họ và tên:";
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFullName.Location = new System.Drawing.Point(100, 29);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(300, 23);
            this.txtFullName.TabIndex = 0;
            // 
            // lblPhone
            // 
            this.lblPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPhone.Location = new System.Drawing.Point(430, 32);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(64, 15);
            this.lblPhone.TabIndex = 1;
            this.lblPhone.Text = "Điện thoại:";
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPhone.Location = new System.Drawing.Point(510, 29);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(210, 23);
            this.txtPhone.TabIndex = 1;
            // 
            // lblIdCard
            // 
            this.lblIdCard.AutoSize = true;
            this.lblIdCard.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblIdCard.Location = new System.Drawing.Point(15, 65);
            this.lblIdCard.Name = "lblIdCard";
            this.lblIdCard.Size = new System.Drawing.Size(46, 15);
            this.lblIdCard.TabIndex = 2;
            this.lblIdCard.Text = "CCCD:";
            // 
            // txtIdCard
            // 
            this.txtIdCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdCard.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtIdCard.Location = new System.Drawing.Point(100, 62);
            this.txtIdCard.Name = "txtIdCard";
            this.txtIdCard.Size = new System.Drawing.Size(300, 23);
            this.txtIdCard.TabIndex = 2;
            this.txtIdCard.MaxLength = 12;
            // 
            // grpBooking
            // 
            this.grpBooking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBooking.Controls.Add(this.lblCheckIn);
            this.grpBooking.Controls.Add(this.dtpCheckIn);
            this.grpBooking.Controls.Add(this.lblCheckOut);
            this.grpBooking.Controls.Add(this.dtpCheckOut);
            this.grpBooking.Controls.Add(this.lblDeposit);
            this.grpBooking.Controls.Add(this.numDeposit);
            this.grpBooking.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpBooking.Location = new System.Drawing.Point(20, 150);
            this.grpBooking.Name = "grpBooking";
            this.grpBooking.Size = new System.Drawing.Size(740, 100);
            this.grpBooking.TabIndex = 22;
            this.grpBooking.TabStop = false;
            this.grpBooking.Text = "Thời gian & Đặt cọc";
            // 
            // lblCheckIn
            // 
            this.lblCheckIn.AutoSize = true;
            this.lblCheckIn.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCheckIn.Location = new System.Drawing.Point(15, 32);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(57, 15);
            this.lblCheckIn.TabIndex = 0;
            this.lblCheckIn.Text = "Check-in:";
            //
            // dtpCheckIn
            //
            this.dtpCheckIn.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckIn.Location = new System.Drawing.Point(100, 29);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(180, 23);
            this.dtpCheckIn.TabIndex = 0;
            this.dtpCheckIn.ValueChanged += new System.EventHandler(this.dtpCheckIn_ValueChanged);
            // 
            // lblCheckOut
            // 
            this.lblCheckOut.AutoSize = true;
            this.lblCheckOut.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCheckOut.Location = new System.Drawing.Point(310, 32);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(66, 15);
            this.lblCheckOut.TabIndex = 1;
            this.lblCheckOut.Text = "Check-out:";
            //
            // dtpCheckOut
            //
            this.dtpCheckOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCheckOut.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckOut.Location = new System.Drawing.Point(400, 29);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(180, 23);
            this.dtpCheckOut.TabIndex = 1;
            this.dtpCheckOut.ValueChanged += new System.EventHandler(this.dtpCheckOut_ValueChanged);
            // 
            // lblDeposit
            // 
            this.lblDeposit.AutoSize = true;
            this.lblDeposit.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDeposit.Location = new System.Drawing.Point(15, 65);
            this.lblDeposit.Name = "lblDeposit";
            this.lblDeposit.Size = new System.Drawing.Size(55, 15);
            this.lblDeposit.TabIndex = 2;
            this.lblDeposit.Text = "Tiền cọc:";
            // 
            // numDeposit
            // 
            this.numDeposit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.numDeposit.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numDeposit.Location = new System.Drawing.Point(100, 62);
            this.numDeposit.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numDeposit.Name = "numDeposit";
            this.numDeposit.Size = new System.Drawing.Size(250, 23);
            this.numDeposit.TabIndex = 2;
            this.numDeposit.ThousandsSeparator = true;
            //
            // lblRoom
            //
            this.lblRoom.AutoSize = true;
            this.lblRoom.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRoom.Location = new System.Drawing.Point(20, 265);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(147, 19);
            this.lblRoom.TabIndex = 4;
            this.lblRoom.Text = "Danh sách phòng còn:";
            //
            // cmbRooms
            //
            this.cmbRooms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRooms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRooms.Font = new System.Drawing.Font(UIHelper.FontFamily, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbRooms.FormattingEnabled = true;
            this.cmbRooms.Location = new System.Drawing.Point(180, 262);
            this.cmbRooms.Name = "cmbRooms";
            this.cmbRooms.Size = new System.Drawing.Size(580, 25);
            this.cmbRooms.TabIndex = 4;
            //
            // btnBook
            //
            this.btnBook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBook.BackColor = UIHelper.PrimaryColor;
            this.btnBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBook.FlatAppearance.BorderSize = 0;
            this.btnBook.Font = new System.Drawing.Font(UIHelper.FontFamily, 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBook.ForeColor = UIHelper.TextWhite;
            this.btnBook.Location = new System.Drawing.Point(20, 300);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(740, 50);
            this.btnBook.TabIndex = 5;
            this.btnBook.Text = "✅ XÁC NHẬN ĐẶT PHÒNG NGAY";
            this.btnBook.UseVisualStyleBackColor = false;
            this.btnBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);
            // 
            // BookingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = UIHelper.BgForm;
            this.ClientSize = new System.Drawing.Size(800, 370);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font(UIHelper.FontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(650, 370);
            this.Name = "BookingUI";
            this.Text = "Đặt phòng - Quản lý Homestay";
            ((System.ComponentModel.ISupportInitialize)(this.numDeposit)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlHomestay.ResumeLayout(false);
            this.pnlHomestay.PerformLayout();
            this.grpCustomer.ResumeLayout(false);
            this.grpCustomer.PerformLayout();
            this.grpBooking.ResumeLayout(false);
            this.grpBooking.PerformLayout();

            // Layout and styling (moved from code-behind to Designer)
            ApplyDesignTimeStyles();

            this.ResumeLayout(false);
        }

        private void ApplyDesignTimeStyles()
        {
            // Apply title
            UIHelper.StyleFormTitle(this.lblHeader, "Đặt Phòng", 18F);

            // Apply group boxes
            UIHelper.StyleGroupBox(this.grpCustomer, "Thông Tin Khách");
            UIHelper.StyleGroupBox(this.grpBooking, "Chi Tiết Đặt Phòng");

            // Apply buttons
            UIHelper.StylePrimaryButton(this.btnSelectHomestay, "Chọn Homestay", 120, 35);
            UIHelper.StyleNeutralButton(this.btnRefresh, "Làm Mới", 90, 35);
            UIHelper.StyleSuccessButton(this.btnBook, "Đặt Phòng", 100, 35);
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlContent;

        // Homestay selection
        private System.Windows.Forms.Panel pnlHomestay;
        private System.Windows.Forms.Label lblHomestayLabel;
        private System.Windows.Forms.Label lblHomestayName;
        private System.Windows.Forms.Button btnSelectHomestay;

        // Customer info
        private System.Windows.Forms.GroupBox grpCustomer;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblIdCard;
        private System.Windows.Forms.TextBox txtIdCard;

        // Booking info
        private System.Windows.Forms.GroupBox grpBooking;
        private System.Windows.Forms.Label lblCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.Label lblCheckOut;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Label lblDeposit;
        private System.Windows.Forms.NumericUpDown numDeposit;

        // Room selection
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.ComboBox cmbRooms;

        // Book button
        private System.Windows.Forms.Button btnBook;
    }
}
