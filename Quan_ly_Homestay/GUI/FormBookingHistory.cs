using Quan_ly_Homestay.BLL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Quan_ly_Homestay.GUI
{
    public partial class FormBookingHistory : Form
    {
        private readonly BookingBLL bookingBLL = new BookingBLL();
        private readonly HomestayBLL homestayBLL = new HomestayBLL();
        private bool isLoading = false;

        public FormBookingHistory()
        {
            InitializeComponent();
        }

        private void FormBookingHistory_Load(object sender, EventArgs e)
        {
            LoadHomestays();
        }

        private void LoadHomestays()
        {
            try
            {
                isLoading = true;
                DataTable table = homestayBLL.GetActiveNames();

                DataRow allRow = table.NewRow();
                allRow["HomestayId"] = 0;
                allRow["HomestayName"] = "-- Tất cả Homestay --";
                table.Rows.InsertAt(allRow, 0);

                cmbHomestay.SelectedIndexChanged -= cmbHomestay_SelectedIndexChanged;
                cmbHomestay.DataSource = table;
                cmbHomestay.DisplayMember = "HomestayName";
                cmbHomestay.ValueMember = "HomestayId";
                cmbHomestay.SelectedIndex = 0;
                cmbHomestay.SelectedIndexChanged += cmbHomestay_SelectedIndexChanged;

                isLoading = false;
                LoadBookingHistory();
            }
            catch (Exception ex)
            {
                isLoading = false;
                MessageBox.Show("Lỗi tải homestay: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbHomestay_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBookingHistory();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadBookingHistory();
        }

        private void LoadBookingHistory()
        {
            if (isLoading) return;

            try
            {
                int homestayId = 0;
                if (cmbHomestay.SelectedValue != null)
                {
                    if (cmbHomestay.SelectedValue is DataRowView drv)
                        homestayId = Convert.ToInt32(drv["HomestayId"]);
                    else
                        homestayId = Convert.ToInt32(cmbHomestay.SelectedValue);
                }

                string keyword = txtSearch.Text.Trim();
                string statusFilter = cmbStatus.SelectedIndex > 0
                    ? cmbStatus.SelectedItem.ToString()
                    : "";

                DataTable table = bookingBLL.GetBookingHistory(homestayId, keyword, statusFilter);
                dgvHistory.DataSource = table;
                FormatHistoryGrid();
                UpdateSummary(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatHistoryGrid()
        {
            SetDateColumn("Ngày nhận", "dd/MM/yyyy HH:mm", 120);
            SetDateColumn("Ngày trả", "dd/MM/yyyy", 100);
            SetDateColumn("Ngày thanh toán", "dd/MM/yyyy HH:mm", 130);
            SetDateColumn("Ngày đặt", "dd/MM/yyyy HH:mm", 120);

            SetNumberColumn("Giá/Ngày", 90);
            SetNumberColumn("Phí DV", 90);
            SetNumberColumn("Tiền cọc", 90);
            SetNumberColumn("Tổng tiền", 100);

            if (dgvHistory.Columns.Contains("Mã ĐP"))
                dgvHistory.Columns["Mã ĐP"].Visible = false;
            if (dgvHistory.Columns.Contains("Phòng"))
                dgvHistory.Columns["Phòng"].Width = 70;
            if (dgvHistory.Columns.Contains("SĐT"))
                dgvHistory.Columns["SĐT"].Width = 100;
            if (dgvHistory.Columns.Contains("Khách hàng"))
                dgvHistory.Columns["Khách hàng"].Width = 130;
            if (dgvHistory.Columns.Contains("Trạng thái"))
                dgvHistory.Columns["Trạng thái"].Width = 90;

            foreach (DataGridViewRow dgvRow in dgvHistory.Rows)
            {
                string status = dgvRow.Cells["Trạng thái"].Value?.ToString() ?? "";
                if (status == "CheckedIn")
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(220, 252, 231);
                }
                else if (status == "CheckedOut")
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
                }
                else if (status == "Cancelled")
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    dgvRow.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        private void SetDateColumn(string columnName, string format, int width)
        {
            if (!dgvHistory.Columns.Contains(columnName)) return;
            dgvHistory.Columns[columnName].DefaultCellStyle.Format = format;
            dgvHistory.Columns[columnName].Width = width;
        }

        private void SetNumberColumn(string columnName, int width)
        {
            if (!dgvHistory.Columns.Contains(columnName)) return;
            dgvHistory.Columns[columnName].DefaultCellStyle.Format = "N0";
            dgvHistory.Columns[columnName].Width = width;
        }

        private void UpdateSummary(DataTable table)
        {
            int checkedIn = 0;
            int checkedOut = 0;
            int cancelled = 0;
            decimal totalRevenue = 0;

            foreach (DataRow row in table.Rows)
            {
                string status = row["Trạng thái"].ToString();
                if (status == "CheckedIn") checkedIn++;
                else if (status == "CheckedOut") checkedOut++;
                else if (status == "Cancelled") cancelled++;

                if (status == "CheckedOut" && row["Tổng tiền"] != DBNull.Value)
                    totalRevenue += Convert.ToDecimal(row["Tổng tiền"]);
            }

            lblSummary.Text = string.Format(
                "Tổng: {0} đơn | Đang ở: {1} | Đã trả: {2} | Đã hủy: {3} | Doanh thu: {4:N0} VNĐ",
                table.Rows.Count,
                checkedIn,
                checkedOut,
                cancelled,
                totalRevenue);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbStatus.SelectedIndex = 0;
            LoadBookingHistory();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvHistory.DataSource == null || dgvHistory.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                    sfd.FileName = "LichSuDatPhong_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    DataTable dt = (DataTable)dgvHistory.DataSource;
                    List<string> visibleColumns = GetVisibleColumns();

                    using (var package = new ExcelPackage())
                    {
                        var ws = package.Workbook.Worksheets.Add("Lịch Sử Đặt Phòng");
                        WriteExportHeader(ws, visibleColumns.Count);
                        WriteExportTable(ws, dt, visibleColumns);

                        int lastRow = dt.Rows.Count + 5;
                        ws.Cells[lastRow, 1].Value = lblSummary.Text;
                        ws.Cells[lastRow, 1, lastRow, visibleColumns.Count].Merge = true;
                        ws.Cells[lastRow, 1].Style.Font.Bold = true;
                        ws.Cells[lastRow, 1].Style.Font.Size = 11;
                        ws.Cells.AutoFitColumns();

                        package.SaveAs(new System.IO.FileInfo(sfd.FileName));
                    }

                    MessageBox.Show("Đã xuất file Excel thành công!\n" + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> GetVisibleColumns()
        {
            var visibleColumns = new List<string>();
            foreach (DataGridViewColumn col in dgvHistory.Columns)
            {
                if (col.Visible)
                    visibleColumns.Add(col.Name);
            }
            return visibleColumns;
        }

        private void WriteExportHeader(ExcelWorksheet ws, int columnCount)
        {
            ws.Cells[1, 1].Value = "LỊCH SỬ ĐẶT PHÒNG HOMESTAY";
            ws.Cells[1, 1, 1, columnCount].Merge = true;
            ws.Cells[1, 1].Style.Font.Size = 16;
            ws.Cells[1, 1].Style.Font.Bold = true;
            ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Cells[2, 1].Value = "Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ws.Cells[2, 1, 2, columnCount].Merge = true;
            ws.Cells[2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void WriteExportTable(ExcelWorksheet ws, DataTable dt, List<string> visibleColumns)
        {
            int colIndex = 1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (!visibleColumns.Contains(dt.Columns[i].ColumnName))
                    continue;

                ws.Cells[4, colIndex].Value = dt.Columns[i].ColumnName;
                ws.Cells[4, colIndex].Style.Font.Bold = true;
                ws.Cells[4, colIndex].Style.Font.Color.SetColor(Color.White);
                ws.Cells[4, colIndex].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[4, colIndex].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(99, 102, 241));
                ws.Cells[4, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[4, colIndex].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                colIndex++;
            }

            for (int rowIdx = 0; rowIdx < dt.Rows.Count; rowIdx++)
            {
                colIndex = 1;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string colName = dt.Columns[i].ColumnName;
                    if (!visibleColumns.Contains(colName))
                        continue;

                    var cell = ws.Cells[rowIdx + 5, colIndex];
                    object value = dt.Rows[rowIdx][i];

                    if (colName.Contains("Giá") || colName.Contains("Tiền") || colName.Contains("Tổng"))
                    {
                        if (value != DBNull.Value)
                        {
                            cell.Value = Convert.ToDecimal(value);
                            cell.Style.Numberformat.Format = "#,##0";
                        }
                    }
                    else if (colName.Contains("Ngày"))
                    {
                        if (value != DBNull.Value)
                        {
                            cell.Value = Convert.ToDateTime(value);
                            cell.Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                        }
                    }
                    else
                    {
                        cell.Value = value;
                    }

                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    colIndex++;
                }

                ApplyStatusColor(ws, dt.Rows[rowIdx]["Trạng thái"]?.ToString() ?? "", rowIdx + 5, colIndex);
            }
        }

        private void ApplyStatusColor(ExcelWorksheet ws, string status, int row, int columnCount)
        {
            Color? fillColor = null;
            Color? fontColor = null;

            if (status == "CheckedIn")
                fillColor = Color.FromArgb(220, 252, 231);
            else if (status == "CheckedOut")
                fillColor = Color.FromArgb(241, 245, 249);
            else if (status == "Cancelled")
            {
                fillColor = Color.FromArgb(254, 226, 226);
                fontColor = Color.Gray;
            }

            if (!fillColor.HasValue) return;

            for (int c = 1; c < columnCount; c++)
            {
                ws.Cells[row, c].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, c].Style.Fill.BackgroundColor.SetColor(fillColor.Value);
                if (fontColor.HasValue)
                    ws.Cells[row, c].Style.Font.Color.SetColor(fontColor.Value);
            }
        }
    }
}
