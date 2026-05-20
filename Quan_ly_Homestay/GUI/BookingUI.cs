using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Quan_ly_Homestay.BLL;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.GUI
{
    public partial class BookingUI : Form
    {
        private readonly BookingBLL bookingBLL = new BookingBLL();
        private readonly HomestayBLL homestayBLL = new HomestayBLL();
        private readonly CurrentUser currentUser;
        private int selectedHomestayId = 0;

        public BookingUI()
            : this(new CurrentUser())
        {
        }

        public BookingUI(CurrentUser user)
        {
            InitializeComponent();
            currentUser = user ?? new CurrentUser();
            dtpCheckIn.Value = DateTime.Now;
            dtpCheckOut.Value = DateTime.Now.AddDays(1);

            // Apply unified styling
            ApplyUIStyles();
        }

        private void ApplyUIStyles()
        {
            // Apply title
            UIHelper.StyleFormTitle(lblHeader, "Đặt Phòng", 18F);

            // Apply buttons
            UIHelper.StylePrimaryButton(btnSelectHomestay, "Chọn Homestay", 120, 35);
            UIHelper.StyleNeutralButton(btnRefresh, "Làm Mới", 90, 35);
            UIHelper.StyleSuccessButton(btnBook, "Đặt Phòng", 100, 35);

            // Apply group boxes
            UIHelper.StyleGroupBox(grpCustomer, "Thông Tin Khách");
            UIHelper.StyleGroupBox(grpBooking, "Chi Tiết Đặt Phòng");
        }

        // ==================== HOMESTAY SELECTION ====================

        private void btnSelectHomestay_Click(object sender, EventArgs e)
        {
            ShowHomestaySelector();
        }

        private void ShowHomestaySelector()
        {
            DataTable homestays = LoadHomestaysForSelect();

            if (homestays.Rows.Count == 0)
            {
                MessageBox.Show("Không có homestay nào!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (Form dialog = new Form())
            {
                dialog.Text = "Chọn Homestay";
                dialog.Size = new Size(500, 350);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.Font = new Font("Segoe UI", 9F);

                DataGridView dgv = new DataGridView();
                dgv.DataSource = homestays;
                dgv.Dock = DockStyle.Top;
                dgv.Height = 220;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.ReadOnly = true;
                dgv.AllowUserToAddRows = false;
                dgv.RowHeadersVisible = false;
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.None;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(155, 89, 182);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                dgv.EnableHeadersVisualStyles = false;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 160, 220);

                if (dgv.Columns.Contains("HomestayId"))
                    dgv.Columns["HomestayId"].Visible = false;

                Button btnSelect = new Button();
                btnSelect.Text = "Chọn";
                btnSelect.DialogResult = DialogResult.OK;
                btnSelect.BackColor = Color.FromArgb(155, 89, 182);
                btnSelect.ForeColor = Color.White;
                btnSelect.FlatStyle = FlatStyle.Flat;
                btnSelect.Size = new Size(100, 35);
                btnSelect.Location = new Point(160, 260);
                btnSelect.Font = new Font("Segoe UI Bold", 9F, FontStyle.Bold);

                Button btnCancel = new Button();
                btnCancel.Text = "Hủy";
                btnCancel.DialogResult = DialogResult.Cancel;
                btnCancel.Size = new Size(100, 35);
                btnCancel.Location = new Point(270, 260);

                dialog.Controls.Add(btnCancel);
                dialog.Controls.Add(btnSelect);
                dialog.Controls.Add(dgv);
                dialog.AcceptButton = btnSelect;
                dialog.CancelButton = btnCancel;

                if (dialog.ShowDialog(this) == DialogResult.OK && dgv.SelectedRows.Count > 0)
                {
                    DataRowView row = dgv.SelectedRows[0].DataBoundItem as DataRowView;
                    if (row != null)
                    {
                        selectedHomestayId = Convert.ToInt32(row["HomestayId"]);
                        lblHomestayName.Text = row["HomestayName"].ToString();
                        lblHomestayName.ForeColor = Color.FromArgb(39, 174, 96);
                        lblHomestayName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                        // Tự động tải danh sách phòng trống sau khi chọn homestay
                        LoadAvailableRooms();
                    }
                }
            }
        }

        private DataTable LoadHomestaysForSelect()
        {
            return homestayBLL.GetActiveForSelection();
        }

        // ==================== REFRESH ROOMS ====================

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAvailableRooms();
        }

        /// <summary>
        /// Tự động tải danh sách phòng trống dựa trên homestay đã chọn và ngày check-in/check-out.
        /// Được gọi khi chọn homestay hoặc thay đổi ngày.
        /// </summary>
        private void LoadAvailableRooms()
        {
            if (selectedHomestayId == 0)
            {
                cmbRooms.DataSource = null;
                return;
            }

            try
            {
                DataTable rooms = bookingBLL.GetAvailableRooms(
                    selectedHomestayId, dtpCheckIn.Value, dtpCheckOut.Value);

                cmbRooms.DataSource = rooms;
                cmbRooms.DisplayMember = "RoomName";
                cmbRooms.ValueMember = "RoomId";

                if (rooms.Rows.Count == 0)
                {
                    MessageBox.Show("Không có phòng trống trong khoảng thời gian này tại homestay đã chọn.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra phòng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện thay đổi ngày check-in/check-out để tự động cập nhật danh sách phòng trống.
        /// </summary>
        private void dtpCheckIn_ValueChanged(object sender, EventArgs e)
        {
            LoadAvailableRooms();
        }

        private void dtpCheckOut_ValueChanged(object sender, EventArgs e)
        {
            LoadAvailableRooms();
        }

        // ==================== BOOKING ====================

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (selectedHomestayId == 0)
            {
                MessageBox.Show("Vui lòng chọn homestay trước!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRooms.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phòng.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy CCCD/CMND từ textbox (có thể để trống)
            string idCard = txtIdCard?.Text?.Trim() ?? "";
            string phone = txtPhone.Text.Trim();

            try
            {
                DataRowView selectedRow = cmbRooms.SelectedItem as DataRowView;
                if (selectedRow == null) return;

                int roomId = Convert.ToInt32(selectedRow["RoomId"]);

                bool success = bookingBLL.ProcessBooking(
                    txtFullName.Text.Trim(),
                    phone,
                    idCard, // ID tạm từ tên+SĐT
                    roomId,
                    currentUser.UserId,
                    dtpCheckIn.Value,
                    dtpCheckOut.Value,
                    numDeposit.Value
                );

                if (success)
                {
                    MessageBox.Show("Đặt phòng thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Reset form after successful booking
                    txtFullName.Text = "";
                    txtPhone.Text = "";
                    txtIdCard.Text = "";
                    cmbRooms.DataSource = null;
                    numDeposit.Value = 0;
                    selectedHomestayId = 0;
                    lblHomestayName.Text = "Chưa chọn homestay";
                    lblHomestayName.ForeColor = Color.Gray;
                    lblHomestayName.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                    dtpCheckIn.Value = DateTime.Now;
                    dtpCheckOut.Value = DateTime.Now.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đặt phòng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
