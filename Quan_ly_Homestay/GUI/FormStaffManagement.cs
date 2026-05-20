using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Quan_ly_Homestay.BLL;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.GUI
{
    /// <summary>
    /// Form Quản lý nhân sự - Chỉ Admin mới truy cập được
    /// </summary>
    public partial class FormStaffManagement : Form
    {
        private readonly UserBLL userBLL;
        private readonly CurrentUser currentUser;
        private DataTable dataTable;
        private int selectedUserId = 0;

        // Colors
        private static readonly Color CLR_PRIMARY    = Color.FromArgb(59, 130, 246);
        private static readonly Color CLR_SUCCESS    = Color.FromArgb(34, 197, 94);
        private static readonly Color CLR_DANGER     = Color.FromArgb(239, 68, 68);
        private static readonly Color CLR_WARNING    = Color.FromArgb(251, 191, 36);
        private static readonly Color CLR_BG         = Color.FromArgb(248, 250, 252);
        private static readonly Color CLR_PANEL      = Color.White;
        private static readonly Color CLR_TEXT       = Color.FromArgb(31, 41, 55);

        public FormStaffManagement(CurrentUser user)
        {
            InitializeComponent();
            
            // Kiểm tra quyền Admin ngay trong constructor
            if (!UserBLL.KiemTraAdmin(user))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!\nChỉ Admin mới được phép.", 
                    "Truy cập bị từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Load += (s, e) => { this.Close(); };
                return;
            }

            currentUser = user;
            userBLL = new UserBLL();
        }

        private void FormStaffManagement_Load(object sender, EventArgs e)
        {
            SetupForm();
            SetupDataGridView();
            LoadData();
            ClearForm();
        }

        private void SetupForm()
        {
            this.BackColor = CLR_BG;
            this.Text = $"👤 Quản lý nhân sự - {currentUser.FullName} (Admin)";

            // Panel chính
            panelMain.BackColor = CLR_PANEL;
            panelForm.BackColor = CLR_BG;
        }

        private void SetupDataGridView()
        {
            dgvStaff.AutoGenerateColumns = false;
            dgvStaff.AllowUserToAddRows = false;
            dgvStaff.AllowUserToDeleteRows = false;
            dgvStaff.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStaff.MultiSelect = false;
            dgvStaff.ReadOnly = true;
            dgvStaff.BackgroundColor = CLR_PANEL;
            dgvStaff.BorderStyle = BorderStyle.None;
            dgvStaff.ColumnHeadersDefaultCellStyle.BackColor = CLR_PRIMARY;
            dgvStaff.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStaff.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvStaff.EnableHeadersVisualStyles = false;
            dgvStaff.RowHeadersVisible = false;
            dgvStaff.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);

            // Columns
            dgvStaff.Columns.Clear();
            
            dgvStaff.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColUserId",
                DataPropertyName = "UserId",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvStaff.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColUsername",
                DataPropertyName = "Username",
                HeaderText = "Tên đăng nhập",
                Width = 120
            });

            dgvStaff.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColFullName",
                DataPropertyName = "FullName",
                HeaderText = "Họ và tên",
                Width = 150
            });

            dgvStaff.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColRole",
                DataPropertyName = "Role",
                HeaderText = "Vai trò",
                Width = 80
            });

            dgvStaff.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == dgvStaff.Columns["ColRole"].Index && e.Value != null)
                {
                    string role = e.Value.ToString();
                    DataGridViewCell cell = dgvStaff.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (role == "Admin")
                        cell.Style.ForeColor = CLR_DANGER;
                    else
                        cell.Style.ForeColor = CLR_SUCCESS;
                    cell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            };

            dgvStaff.CellClick += DgvStaff_CellClick;
        }

        private void LoadData()
        {
            try
            {
                dataTable = userBLL.LayDanhSachNhanVien();
                dgvStaff.DataSource = dataTable;
                lblCount.Text = $"Tổng: {dataTable.Rows.Count} nhân viên";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvStaff.Rows[e.RowIndex];
            selectedUserId = Convert.ToInt32(row.Cells["ColUserId"].Value);
            txtUsername.Text = row.Cells["ColUsername"].Value.ToString();
            txtFullName.Text = row.Cells["ColFullName"].Value?.ToString() ?? "";
            cmbRole.SelectedItem = row.Cells["ColRole"].Value.ToString();
            txtPassword.Text = ""; // Không hiển thị mật khẩu
            
            txtUsername.ReadOnly = true; // Không cho sửa username
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = selectedUserId != currentUser.UserId; // Không cho xóa chính mình
        }

        private void ClearForm()
        {
            selectedUserId = 0;
            txtUsername.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            cmbRole.SelectedIndex = 1; // Mặc định Staff
            txtUsername.ReadOnly = false;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            
            if (dgvStaff.Rows.Count > 0)
                dgvStaff.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.Focus();
                    return;
                }

                string role = cmbRole.SelectedItem?.ToString() ?? "Staff";
                
                bool result = userBLL.ThemNhanVien(
                    txtUsername.Text,
                    txtPassword.Text,
                    txtFullName.Text,
                    role,
                    currentUser
                );

                if (result)
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedUserId == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.Focus();
                    return;
                }

                string role = cmbRole.SelectedItem?.ToString() ?? "Staff";
                
                DialogResult confirm = MessageBox.Show(
                    $"Cập nhật thông tin cho \"{txtFullName.Text}\"?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (confirm != DialogResult.Yes) return;

                bool result = userBLL.CapNhatNhanVien(
                    selectedUserId,
                    txtFullName.Text,
                    role,
                    txtPassword.Text,
                    currentUser
                );

                if (result)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedUserId == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string fullName = txtFullName.Text;
                
                DialogResult confirm = MessageBox.Show(
                    $"Bạn có chắc muốn xóa nhân viên \"{fullName}\"?\nHành động này không thể hoàn tác!",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (confirm != DialogResult.Yes) return;

                bool result = userBLL.XoaNhanVien(selectedUserId, currentUser);

                if (result)
                {
                    MessageBox.Show("Xóa nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Nếu form đang được nhúng trong panel (TopLevel = false), chỉ cần ẩn
            if (!this.TopLevel)
            {
                this.Hide();
            }
            else
            {
                this.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (dataTable == null) return;
            
            string search = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                dgvStaff.DataSource = dataTable;
            }
            else
            {
                DataView dv = dataTable.DefaultView;
                dv.RowFilter = $"Username LIKE '%{search}%' OR FullName LIKE '%{search}%'";
                dgvStaff.DataSource = dv.ToTable();
            }
            lblCount.Text = $"Hiển thị: {dgvStaff.Rows.Count} / {dataTable.Rows.Count} nhân viên";
        }
    }
}
