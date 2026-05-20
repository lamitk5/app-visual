namespace Quan_ly_Homestay.GUI
{
    partial class FormStatistics
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnToday = new System.Windows.Forms.Button();
            this.btn7Days = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lblTotalBookings = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.chartDaily = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartHomestay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHomestay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.pnlFilter);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.lblPeriod);
            this.pnlTop.Controls.Add(this.lblTotalBookings);
            this.pnlTop.Controls.Add(this.lblTotalRevenue);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(984, 110);
            this.pnlTop.TabIndex = 0;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFilter.Controls.Add(this.btnToday);
            this.pnlFilter.Controls.Add(this.btn7Days);
            this.pnlFilter.Location = new System.Drawing.Point(0, 50);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(984, 60);
            this.pnlFilter.TabIndex = 5;
            // 
            // btnToday
            // 
            this.btnToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnToday.FlatAppearance.BorderSize = 0;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnToday.Location = new System.Drawing.Point(160, 14);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(140, 35);
            this.btnToday.TabIndex = 1;
            this.btnToday.Text = "Hôm nay";
            this.btnToday.UseVisualStyleBackColor = false;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btn7Days
            // 
            this.btn7Days.BackColor = System.Drawing.Color.Teal;
            this.btn7Days.FlatAppearance.BorderSize = 0;
            this.btn7Days.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn7Days.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn7Days.ForeColor = System.Drawing.Color.White;
            this.btn7Days.Location = new System.Drawing.Point(15, 14);
            this.btn7Days.Name = "btn7Days";
            this.btn7Days.Size = new System.Drawing.Size(140, 35);
            this.btn7Days.TabIndex = 0;
            this.btn7Days.Text = "7 ngày gần nhất";
            this.btn7Days.UseVisualStyleBackColor = false;
            this.btn7Days.Click += new System.EventHandler(this.btn7Days_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.Teal;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(872, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPeriod.Location = new System.Drawing.Point(280, 18);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(10, 15);
            this.lblPeriod.TabIndex = 4;
            this.lblPeriod.Text = "";
            // 
            // lblTotalBookings
            // 
            this.lblTotalBookings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalBookings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblTotalBookings.Location = new System.Drawing.Point(550, 30);
            this.lblTotalBookings.Name = "lblTotalBookings";
            this.lblTotalBookings.Size = new System.Drawing.Size(310, 18);
            this.lblTotalBookings.TabIndex = 3;
            this.lblTotalBookings.Text = "Tổng số đơn: 0";
            this.lblTotalBookings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblTotalRevenue.Location = new System.Drawing.Point(550, 8);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(310, 22);
            this.lblTotalRevenue.TabIndex = 1;
            this.lblTotalRevenue.Text = "Tổng doanh thu: 0 VNĐ";
            this.lblTotalRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Teal;
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(260, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thống Kê Doanh Thu";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 110);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.chartDaily);
            this.splitContainer.Panel1.Controls.Add(this.chartHomestay);
            this.splitContainer.Panel1MinSize = 250;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgvDetails);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(984, 451);
            this.splitContainer.SplitterDistance = 300;
            this.splitContainer.TabIndex = 1;
            // 
            // chartDaily
            // 
            this.chartDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            this.chartDaily.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartDaily.Legends.Add(legend1);
            this.chartDaily.Location = new System.Drawing.Point(0, 0);
            this.chartDaily.Name = "chartDaily";
            this.chartDaily.Size = new System.Drawing.Size(680, 300);
            this.chartDaily.TabIndex = 0;
            this.chartDaily.Text = "chartDaily";
            // 
            // chartHomestay
            // 
            this.chartHomestay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chartHomestay.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartHomestay.Legends.Add(legend2);
            this.chartHomestay.Location = new System.Drawing.Point(680, 0);
            this.chartHomestay.Name = "chartHomestay";
            this.chartHomestay.Size = new System.Drawing.Size(304, 300);
            this.chartHomestay.TabIndex = 1;
            this.chartHomestay.Text = "chartHomestay";
            // 
            // dgvDetails
            // 
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            this.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDetails.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Teal;
            this.dgvDetails.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDetails.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.EnableHeadersVisualStyles = false;
            this.dgvDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dgvDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.RowHeadersVisible = false;
            this.dgvDetails.RowTemplate.Height = 30;
            this.dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetails.Size = new System.Drawing.Size(984, 147);
            this.dgvDetails.TabIndex = 0;
            // 
            // FormStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "FormStatistics";
            this.Text = "Thống Kê Doanh Thu";
            this.Load += new System.EventHandler(this.FormStatistics_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHomestay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();

            // Layout and styling (moved from code-behind to Designer)
            ApplyDesignTimeStyles();

            this.ResumeLayout(false);
        }

        private void ApplyDesignTimeStyles()
        {
            // Apply title
            UIHelper.StyleFormTitle(this.lblTitle, "Thống Kê", 18F);

            // Apply buttons
            UIHelper.StylePrimaryButton(this.btn7Days, "7 Ngày", 100, 35);
            UIHelper.StyleNeutralButton(this.btnToday, "Hôm Nay", 100, 35);
            UIHelper.StyleNeutralButton(this.btnRefresh, "Làm Mới", 100, 35);

            // Apply DataGridView
            UIHelper.StyleDataGridView(this.dgvDetails);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblTotalBookings;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btn7Days;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDaily;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHomestay;
        private System.Windows.Forms.DataGridView dgvDetails;
    }
}
