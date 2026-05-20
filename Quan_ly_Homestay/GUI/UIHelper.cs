using System.Drawing;
using System.Windows.Forms;

namespace Quan_ly_Homestay.GUI
{
    /// <summary>
    /// Lớp tiện ích chuẩn hóa giao diện cho toàn bộ ứng dụng.
    /// Cung cấp màu sắc thống nhất, font chữ, và các phương thức style controls.
    /// </summary>
    public static class UIHelper
    {
        // ============================================================
        //  COLOR PALETTE - Bảng màu thống nhất
        // ============================================================

        // Primary colors
        public static readonly Color PrimaryColor = Color.FromArgb(0, 128, 128);        // Teal - màu chính
        public static readonly Color PrimaryDark = Color.FromArgb(0, 100, 100);          // Teal đậm
        public static readonly Color PrimaryLight = Color.FromArgb(224, 242, 242);        // Teal nhạt

        // Secondary / Info
        public static readonly Color SecondaryColor = Color.FromArgb(59, 130, 246);      // Blue
        public static readonly Color SecondaryDark = Color.FromArgb(37, 99, 235);        // Blue đậm

        // Success / Add
        public static readonly Color SuccessColor = Color.FromArgb(34, 197, 94);         // Green
        public static readonly Color SuccessDark = Color.FromArgb(22, 163, 74);          // Green đậm

        // Warning
        public static readonly Color WarningColor = Color.FromArgb(251, 191, 36);        // Amber
        public static readonly Color WarningDark = Color.FromArgb(245, 158, 11);         // Amber đậm

        // Danger / Delete
        public static readonly Color DangerColor = Color.FromArgb(239, 68, 68);          // Red
        public static readonly Color DangerDark = Color.FromArgb(220, 38, 38);           // Red đậm

        // Neutral / Close / Refresh
        public static readonly Color NeutralColor = Color.FromArgb(107, 114, 128);       // Gray
        public static readonly Color NeutralLight = Color.FromArgb(156, 163, 175);       // Light gray

        // Background colors
        public static readonly Color BgForm = Color.FromArgb(248, 250, 252);             // Form background
        public static readonly Color BgPanel = Color.White;                               // Panel background
        public static readonly Color BgCard = Color.White;                                // Card background

        // Text colors
        public static readonly Color TextPrimary = Color.FromArgb(31, 41, 55);           // Text chính
        public static readonly Color TextSecondary = Color.FromArgb(107, 114, 128);      // Text phụ
        public static readonly Color TextWhite = Color.White;

        // ============================================================
        //  FONT CONSTANTS
        // ============================================================
        public static readonly string FontFamily = "Segoe UI";

        // ============================================================
        //  STYLE METHODS - Các phương thức style controls
        // ============================================================

        /// <summary>
        /// Style nút menu bên trái (FormMain sidebar)
        /// </summary>
        public static void StyleMenuButton(Button btn, string text)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = TextPrimary;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = PrimaryColor;
            btn.FlatAppearance.BorderSize = 1;
            btn.Font = new Font(FontFamily, 10F, FontStyle.Regular);
            btn.Size = new Size(210, 45);
            btn.Text = text;
            btn.UseVisualStyleBackColor = false;
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Style nút chính (Thêm, Lưu, Xác nhận)
        /// </summary>
        public static void StylePrimaryButton(Button btn, string text, int width = 120, int height = 40)
        {
            ApplyButtonStyle(btn, PrimaryColor, text, 10F, FontStyle.Bold, width, height);
        }

        /// <summary>
        /// Style nút phụ (Sửa, Cập nhật)
        /// </summary>
        public static void StyleSecondaryButton(Button btn, string text, int width = 120, int height = 40)
        {
            ApplyButtonStyle(btn, SecondaryColor, text, 10F, FontStyle.Bold, width, height);
        }

        /// <summary>
        /// Style nút thành công (Thêm mới - dạng nhỏ)
        /// </summary>
        public static void StyleSuccessButton(Button btn, string text, int width = 120, int height = 40)
        {
            ApplyButtonStyle(btn, SuccessColor, text, 10F, FontStyle.Bold, width, height);
        }

        /// <summary>
        /// Style nút cảnh báo (Đổi, Reset)
        /// </summary>
        public static void StyleWarningButton(Button btn, string text, int width = 120, int height = 40)
        {
            ApplyButtonStyle(btn, WarningColor, text, 10F, FontStyle.Bold, width, height);
            btn.ForeColor = TextPrimary; // Dark text on amber
        }

        /// <summary>
        /// Style nút nguy hiểm (Xóa)
        /// </summary>
        public static void StyleDangerButton(Button btn, string text, int width = 120, int height = 40)
        {
            ApplyButtonStyle(btn, DangerColor, text, 10F, FontStyle.Bold, width, height);
        }

        /// <summary>
        /// Style nút trung tính (Đóng, Làm mới)
        /// </summary>
        public static void StyleNeutralButton(Button btn, string text, int width = 120, int height = 40)
        {
            ApplyButtonStyle(btn, NeutralColor, text, 10F, FontStyle.Bold, width, height);
        }

        /// <summary>
        /// Style DataGridView thống nhất
        /// </summary>
        public static void StyleDataGridView(DataGridView dgv)
        {
            // General
            dgv.BackgroundColor = BgPanel;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(229, 231, 235);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextWhite;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(FontFamily, 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.ColumnHeadersHeight = 40;

            // Row style
            dgv.DefaultCellStyle.BackColor = BgPanel;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.Font = new Font(FontFamily, 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = PrimaryLight;
            dgv.DefaultCellStyle.SelectionForeColor = TextPrimary;
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.RowTemplate.Height = 35;

            // Alternating row
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = PrimaryLight;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = TextPrimary;
        }

        /// <summary>
        /// Style tiêu đề form (label lớn ở đầu form)
        /// </summary>
        public static void StyleFormTitle(Label lbl, string text, float fontSize = 20F)
        {
            lbl.Text = text;
            lbl.Font = new Font(FontFamily, fontSize, FontStyle.Bold);
            lbl.ForeColor = PrimaryColor;
            lbl.AutoSize = true;
            lbl.Anchor = AnchorStyles.Top;
        }

        /// <summary>
        /// Style panel header (thanh tiêu đề có nền màu)
        /// </summary>
        public static void StylePanelHeader(Panel panel, Color? bgColor = null)
        {
            panel.BackColor = bgColor ?? PrimaryColor;
            panel.Dock = DockStyle.Top;
        }

        /// <summary>
        /// Style label tiêu đề trong panel header
        /// </summary>
        public static void StyleHeaderLabel(Label lbl, string text, float fontSize = 14F)
        {
            lbl.Text = text;
            lbl.Font = new Font(FontFamily, fontSize, FontStyle.Bold);
            lbl.ForeColor = TextWhite;
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
        }

        /// <summary>
        /// Style GroupBox thống nhất
        /// </summary>
        public static void StyleGroupBox(GroupBox grp, string text)
        {
            grp.Text = text;
            grp.Font = new Font(FontFamily, 10F, FontStyle.Bold);
            grp.ForeColor = TextPrimary;
        }

        // ============================================================
        //  PRIVATE HELPERS
        // ============================================================

        private static void ApplyButtonStyle(Button btn, Color bgColor, string text,
            float fontSize, FontStyle fontStyle, int width, int height)
        {
            btn.BackColor = bgColor;
            btn.ForeColor = TextWhite;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font(FontFamily, fontSize, fontStyle);
            btn.Size = new Size(width, height);
            btn.Text = text;
            btn.UseVisualStyleBackColor = false;
            btn.Cursor = Cursors.Hand;
        }
    }
}
