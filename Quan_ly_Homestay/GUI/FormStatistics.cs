using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Quan_ly_Homestay.BLL;

namespace Quan_ly_Homestay.GUI
{
    public partial class FormStatistics : Form
    {
        private readonly StatisticsBLL statisticsBLL = new StatisticsBLL();
        private bool is7DayMode = true; // true = 7 days, false = today

        public FormStatistics()
        {
            InitializeComponent();
        }

        private void FormStatistics_Load(object sender, EventArgs e)
        {
            is7DayMode = true;
            UpdateButtonStyles();
            LoadStatistics();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        private void btn7Days_Click(object sender, EventArgs e)
        {
            is7DayMode = true;
            UpdateButtonStyles();
            LoadStatistics();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            is7DayMode = false;
            UpdateButtonStyles();
            LoadStatistics();
        }

        private void UpdateButtonStyles()
        {
            if (is7DayMode)
            {
                btn7Days.BackColor = Color.Teal;
                btn7Days.ForeColor = Color.White;
                btnToday.BackColor = Color.FromArgb(241, 245, 249);
                btnToday.ForeColor = Color.FromArgb(51, 65, 85);
            }
            else
            {
                btnToday.BackColor = Color.Teal;
                btnToday.ForeColor = Color.White;
                btn7Days.BackColor = Color.FromArgb(241, 245, 249);
                btn7Days.ForeColor = Color.FromArgb(51, 65, 85);
            }
        }

        private void LoadStatistics()
        {
            try
            {
                DateTime fromDate, toDate;

                if (is7DayMode)
                {
                    fromDate = DateTime.Today.AddDays(-6); // 7 days including today
                    toDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                }
                else
                {
                    fromDate = DateTime.Today;
                    toDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                }

                // Load daily revenue data
                DataTable dailyData = statisticsBLL.GetDailyRevenue(fromDate, toDate);
                // Load revenue by homestay
                DataTable homestayData = statisticsBLL.GetRevenueByHomestay(fromDate, toDate);

                // Update charts
                UpdateDailyChart(dailyData);
                UpdateHomestayChart(homestayData);

                // Update detail grid
                UpdateDetailGrid(dailyData);

                // Update total
                decimal totalRevenue = 0;
                int totalBookings = 0;
                foreach (DataRow row in dailyData.Rows)
                {
                    if (row["Ngay"] == DBNull.Value) continue;
                    totalRevenue += row["Revenue"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Revenue"]);
                    totalBookings += row["BookingCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["BookingCount"]);
                }
                lblTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue:N0} VNĐ";
                lblTotalBookings.Text = $"Tổng số đơn: {totalBookings}";

                // Update period label
                string periodText = is7DayMode
                    ? $"7 ngày gần nhất ({fromDate:dd/MM/yyyy} - {toDate:dd/MM/yyyy})"
                    : $"Hôm nay ({DateTime.Today:dd/MM/yyyy})";
                lblPeriod.Text = periodText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDailyChart(DataTable data)
        {
            chartDaily.Series.Clear();
            chartDaily.Titles.Clear();

            if (data.Rows.Count == 0)
            {
                chartDaily.Titles.Add("Không có dữ liệu trong khoảng thời gian này");
                chartDaily.Titles[0].Font = new Font("Segoe UI", 12F);
                chartDaily.Titles[0].ForeColor = Color.Gray;
                return;
            }

            // Column chart for daily revenue
            Series seriesBar = new Series("Doanh thu");
            seriesBar.ChartType = SeriesChartType.Column;
            seriesBar.Font = new Font("Segoe UI", 8F);
            seriesBar.IsValueShownAsLabel = true;
            seriesBar.LabelFormat = "#,##0";
            seriesBar.LabelForeColor = Color.FromArgb(55, 65, 81);
            seriesBar["PointWidth"] = "0.6";

            // Line chart for booking count
            Series seriesLine = new Series("Số đơn");
            seriesLine.ChartType = SeriesChartType.Line;
            seriesLine.Font = new Font("Segoe UI", 8F);
            seriesLine.IsValueShownAsLabel = true;
            seriesLine.LabelForeColor = Color.FromArgb(220, 38, 38);
            seriesLine.BorderWidth = 3;
            seriesLine.MarkerStyle = MarkerStyle.Circle;
            seriesLine.MarkerSize = 8;
            seriesLine.YAxisType = AxisType.Secondary;

            Color[] colors = new Color[]
            {
                Color.FromArgb(99, 102, 241),   // Indigo
                Color.FromArgb(34, 197, 94),     // Green
                Color.FromArgb(249, 115, 22),    // Orange
                Color.FromArgb(236, 72, 153),    // Pink
                Color.FromArgb(59, 130, 246),    // Blue
                Color.FromArgb(168, 85, 247),    // Purple
                Color.FromArgb(20, 184, 166),    // Teal
            };

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow row = data.Rows[i];
                if (row["Ngay"] == DBNull.Value) continue;
                DateTime date = Convert.ToDateTime(row["Ngay"]);
                decimal revenue = row["Revenue"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Revenue"]);
                int bookingCount = row["BookingCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["BookingCount"]);

                DataPoint barPoint = new DataPoint();
                barPoint.SetValueXY(date.ToString("dd/MM"), (double)revenue);
                barPoint.Color = colors[i % colors.Length];
                barPoint.ToolTip = $"{date:dd/MM/yyyy}\nDoanh thu: {revenue:N0} VNĐ\nSố đơn: {bookingCount}";
                seriesBar.Points.Add(barPoint);

                DataPoint linePoint = new DataPoint();
                linePoint.SetValueXY(date.ToString("dd/MM"), bookingCount);
                seriesLine.Points.Add(linePoint);
            }

            chartDaily.Series.Add(seriesBar);
            chartDaily.Series.Add(seriesLine);

            // Title
            Title title = new Title("Doanh Thu Theo Ngày");
            title.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(55, 65, 81);
            chartDaily.Titles.Add(title);

            // Configure axes
            chartDaily.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartDaily.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartDaily.ChartAreas[0].AxisX.Interval = 1;
            chartDaily.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0";
            chartDaily.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartDaily.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDaily.ChartAreas[0].AxisY.TitleFont = new Font("Segoe UI", 9F);

            // Secondary Y axis for booking count
            chartDaily.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartDaily.ChartAreas[0].AxisY2.Title = "Số đơn";
            chartDaily.ChartAreas[0].AxisY2.TitleFont = new Font("Segoe UI", 9F);
            chartDaily.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;

            // Legend
            chartDaily.Legends[0].Docking = Docking.Top;
            chartDaily.Legends[0].Font = new Font("Segoe UI", 9F);
        }

        private void UpdateHomestayChart(DataTable data)
        {
            chartHomestay.Series.Clear();
            chartHomestay.Titles.Clear();

            if (data.Rows.Count == 0)
            {
                chartHomestay.Titles.Add("Không có dữ liệu");
                chartHomestay.Titles[0].Font = new Font("Segoe UI", 10F);
                chartHomestay.Titles[0].ForeColor = Color.Gray;
                return;
            }

            // Pie chart for revenue by homestay
            Series seriesPie = new Series("Tỷ lệ");
            seriesPie.ChartType = SeriesChartType.Pie;
            seriesPie.Font = new Font("Segoe UI", 8F);
            seriesPie.IsValueShownAsLabel = true;
            seriesPie.LabelFormat = "#,##0";
            seriesPie["PieLabelStyle"] = "Outside";
            seriesPie["PieLineColor"] = "Gray";

            Color[] colors = new Color[]
            {
                Color.FromArgb(99, 102, 241),
                Color.FromArgb(34, 197, 94),
                Color.FromArgb(249, 115, 22),
                Color.FromArgb(236, 72, 153),
                Color.FromArgb(59, 130, 246),
                Color.FromArgb(168, 85, 247),
                Color.FromArgb(20, 184, 166),
                Color.FromArgb(245, 158, 11),
                Color.FromArgb(239, 68, 68),
            };

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow row = data.Rows[i];
                string name = row["HomestayName"].ToString();
                decimal revenue = row["Revenue"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Revenue"]);

                DataPoint point = new DataPoint();
                point.SetValueXY(name, (double)revenue);
                point.Color = colors[i % colors.Length];
                point.ToolTip = $"{name}\nDoanh thu: {revenue:N0} VNĐ\nSố đơn: {row["BookingCount"]}";
                seriesPie.Points.Add(point);
            }

            chartHomestay.Series.Add(seriesPie);

            Title title = new Title("Doanh Thu Theo Homestay");
            title.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(55, 65, 81);
            chartHomestay.Titles.Add(title);

            chartHomestay.Legends[0].Docking = Docking.Bottom;
            chartHomestay.Legends[0].Font = new Font("Segoe UI", 8F);
        }

        private void UpdateDetailGrid(DataTable data)
        {
            dgvDetails.DataSource = data;

            if (dgvDetails.Columns.Contains("Ngay"))
            {
                dgvDetails.Columns["Ngay"].HeaderText = "Ngày";
                dgvDetails.Columns["Ngay"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvDetails.Columns["Ngay"].Width = 100;
            }
            if (dgvDetails.Columns.Contains("Revenue"))
            {
                dgvDetails.Columns["Revenue"].HeaderText = "Doanh Thu (VNĐ)";
                dgvDetails.Columns["Revenue"].DefaultCellStyle.Format = "N0";
                dgvDetails.Columns["Revenue"].Width = 150;
                dgvDetails.Columns["Revenue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDetails.Columns.Contains("BookingCount"))
            {
                dgvDetails.Columns["BookingCount"].HeaderText = "Số Đơn";
                dgvDetails.Columns["BookingCount"].Width = 80;
                dgvDetails.Columns["BookingCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvDetails.Columns.Contains("AvgPerBooking"))
            {
                dgvDetails.Columns["AvgPerBooking"].HeaderText = "TB/Đơn (VNĐ)";
                dgvDetails.Columns["AvgPerBooking"].DefaultCellStyle.Format = "N0";
                dgvDetails.Columns["AvgPerBooking"].Width = 130;
                dgvDetails.Columns["AvgPerBooking"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

    }
}
