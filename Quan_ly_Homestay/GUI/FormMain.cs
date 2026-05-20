using System;
using System.Drawing;
using System.Windows.Forms;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.GUI
{
    public partial class FormMain : Form
    {
        private readonly CurrentUser currentUser;
        private Form currentChildForm = null;
        private Button currentActiveButton = null;

        private readonly Color COLOR_NORMAL = Color.White;
        private readonly Color COLOR_ACTIVE = Color.Teal;
        private readonly Color COLOR_TEXT_NORMAL = Color.Black;
        private readonly Color COLOR_TEXT_ACTIVE = Color.White;
        private const string ACCESS_DENIED_MESSAGE = "B\u1ea1n ch\u01b0a \u0111\u1ee7 th\u1ea9m quy\u1ec1n truy c\u1eadp";

        public FormMain()
            : this(new CurrentUser())
        {
        }

        public FormMain(CurrentUser user)
        {
            InitializeComponent();
            currentUser = user ?? new CurrentUser();
        }

        private void SetActiveButton(Button activeButton)
        {
            ResetAllButtonColors();

            if (activeButton != null)
            {
                activeButton.BackColor = COLOR_ACTIVE;
                activeButton.ForeColor = COLOR_TEXT_ACTIVE;
                activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                activeButton.FlatAppearance.BorderColor = COLOR_ACTIVE;
                currentActiveButton = activeButton;
            }
        }

        private void ResetAllButtonColors()
        {
            ResetButtonColor(btnHomestay);
            ResetButtonColor(btnRoom);
            ResetButtonColor(btnBooking);
            ResetButtonColor(btnStaff);
            ResetButtonColor(btnStatistics);
            ResetButtonColor(btnHistory);
        }

        private void ResetButtonColor(Button btn)
        {
            if (btn != null && btn.Visible)
            {
                btn.BackColor = COLOR_NORMAL;
                btn.ForeColor = COLOR_TEXT_NORMAL;
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                btn.FlatAppearance.BorderColor = Color.Teal;
            }
        }

        private bool IsAdminUser()
        {
            return string.Equals(currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase);
        }

        private bool CanAccessMenu(Button menuButton)
        {
            if (IsAdminUser())
            {
                return true;
            }

            return menuButton == btnBooking
                || menuButton == btnRoom
                || menuButton == btnHistory;
        }

        private bool EnsureMenuAccess(Button menuButton)
        {
            if (CanAccessMenu(menuButton))
            {
                return true;
            }

            MessageBox.Show(
                ACCESS_DENIED_MESSAGE,
                "Kh\u00f4ng \u0111\u1ee7 quy\u1ec1n",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        private void OpenChildForm(Form childForm, Button senderButton)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
            }

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.AutoScroll = true;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            SetActiveButton(senderButton);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = string.Format(
                "Xin chào: {0} | Quyền: {1}",
                currentUser.FullName,
                currentUser.Role);

            // Apply unified styling
            UIHelper.StyleMenuButton(btnHomestay, "🏠 Homestay");
            UIHelper.StyleMenuButton(btnRoom, "🚪 Phòng");
            UIHelper.StyleMenuButton(btnBooking, "📅 Đặt Phòng");
            UIHelper.StyleMenuButton(btnStaff, "👤 Nhân Sự");
            UIHelper.StyleMenuButton(btnStatistics, "📊 Thống Kê");
            UIHelper.StyleMenuButton(btnHistory, "📋 Lịch Sử");

            // Style logout button
            UIHelper.StyleDangerButton(btnLogout, "Đăng Xuất", 100, 40);
        }

        private void btnHomestay_Click(object sender, EventArgs e)
        {
            if (!EnsureMenuAccess(btnHomestay)) return;
            OpenChildForm(new FormHomestay(), btnHomestay);
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            if (!EnsureMenuAccess(btnRoom)) return;
            OpenChildForm(new Room(currentUser), btnRoom);
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            if (!EnsureMenuAccess(btnBooking)) return;
            OpenChildForm(new BookingUI(currentUser), btnBooking);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            if (!EnsureMenuAccess(btnStaff)) return;
            OpenChildForm(new FormStaffManagement(currentUser), btnStaff);
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            if (!EnsureMenuAccess(btnStatistics)) return;
            OpenChildForm(new FormStatistics(), btnStatistics);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (!EnsureMenuAccess(btnHistory)) return;
            OpenChildForm(new FormBookingHistory(), btnHistory);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "B\u1ea1n c\u00f3 ch\u1eafc mu\u1ed1n \u0111\u0103ng xu\u1ea5t kh\u00f4ng?",
                "X\u00e1c nh\u1eadn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DialogResult = DialogResult.Retry;
                Close();
            }
        }
    }
}
