using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Quan_ly_Homestay.BLL;
using Quan_ly_Homestay.Model;
using DrawingFont = System.Drawing.Font;
using PdfFont = iTextSharp.text.Font;
using FontStyle = System.Drawing.FontStyle;

namespace Quan_ly_Homestay.GUI
{
    public partial class Room : Form
    {
        private PhongBLL phongBLL;
        private HomestayBLL homestayBLL;
        private InvoiceBLL invoiceBLL;
        private ServiceBLL serviceBLL;
        private CurrentUser currentUser;
        private List<Phong> danhSachPhong;
        private List<Homestay> danhSachHomestay;
        private int homestayHienTaiId = 0; // 0 = táº¥t cáº£ homestays
        private ToolTip toolTip1;

        // Modern Light Theme Colors
        private static readonly Color CLR_DANG_DUNG   = Color.FromArgb(239, 68, 68);    // Äá» 
        private static readonly Color CLR_TRONG       = Color.FromArgb(34, 197, 94);   // Xanh lÃ¡ 
        private static readonly Color CLR_DANG_DON    = Color.FromArgb(251, 191, 36);  // VÃ ng 
        private static readonly Color CLR_BAO_TRI     = Color.FromArgb(156, 163, 175); // XÃ¡m
        private static readonly Color CLR_DA_DAT      = Color.FromArgb(59, 130, 246);  // Xanh dÆ°Æ¡ng

        // Light Theme Backgrounds
        private static readonly Color CLR_FORM_BG     = Color.FromArgb(248, 250, 252);  // XÃ¡m
        private static readonly Color CLR_PANEL_BG    = Color.White;
        private static readonly Color CLR_HEADER_BG   = Color.FromArgb(59, 130, 246);  // Xanh dÆ°Æ¡ng 
        private static readonly Color CLR_TEXT_DARK   = Color.FromArgb(31, 41, 55);   // Xanh Ä‘en
        private static readonly Color CLR_TEXT_LIGHT  = Color.White;
        private static readonly Color CLR_BORDER      = Color.FromArgb(229, 231, 235); // XÃ¡m nháº¡t cho border

        private const int COLS         = 6;   // columns in the room grid
        private const int BTN_GAP      = 8;
        private const int GRID_PADDING = 8;
        private const int BTN_H        = 75;

        public Room()
        {
            InitializeComponent();
            danhSachPhong  = new List<Phong>();
            danhSachHomestay = new List<Homestay>();
            toolTip1 = new ToolTip();
            invoiceBLL = new InvoiceBLL();
            serviceBLL = new ServiceBLL();
        }

        public Room(CurrentUser user)
            : this()
        {
            currentUser = user;
        }

        private bool IsRestrictedStaff()
        {
            return currentUser != null
                && !string.Equals(currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase);
        }

        private void ApplyRolePermissions()
        {
            if (!IsRestrictedStaff())
            {
                return;
            }

            btnThemPhong.Visible = false;
            btnSuaPhong.Visible = false;
            btnXoaPhong.Visible = false;
        }

        // â”€â”€ Event Handlers â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

        private void panelGrid_Resize(object sender, EventArgs e)
        {
            if (danhSachPhong != null && danhSachPhong.Count > 0)
            {
                HienThiGridPhong();
            }
        }

        private void Room_Load(object sender, EventArgs e)
        {
            // Initialize data access layer
            if (phongBLL == null)
                phongBLL = new PhongBLL();
            if (homestayBLL == null)
                homestayBLL = new HomestayBLL();

            // Load danh sach homestay
            LoadDanhSachHomestay();

            ApplyRolePermissions();

            // Hien thi ten homestay hien tai
            CapNhatTieuDeHomestay();

            LoadDanhSachPhong();
            HienThiGridPhong();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDanhSachPhong();
            HienThiGridPhong();
        }

        /// <summary>
        /// Chon homestay khac de xem phong
        /// </summary>
        private void btnDoiHome_Click(object sender, EventArgs e)
        {
            if (danhSachHomestay.Count == 0)
            {
                MessageBox.Show("KhÃ´ng cÃ³ homestay nÃ o!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tao dialog chon homestay
            var dialog = new Form
            {
                Text = "Chá»n Homestay",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = CLR_PANEL_BG
            };

            var lblTitle = new Label
            {
                Text = "ðŸ  Chá»n Homestay Ä‘á»ƒ xem phÃ²ng",
                Font = new DrawingFont("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = CLR_HEADER_BG,
                Location = new Point(20, 15),
                Size = new Size(350, 30)
            };

            // ComboBox hien thi tat ca homestays
            var cmbHomestay = new ComboBox
            {
                Location = new Point(20, 55),
                Size = new Size(340, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = CLR_PANEL_BG,
                ForeColor = CLR_TEXT_DARK,
                Font = new DrawingFont("Segoe UI", 10F)
            };

            // Them option "Tat ca"
            cmbHomestay.Items.Add("ðŸ“ Táº¥t cáº£ Homestays");
            foreach (var hs in danhSachHomestay)
            {
                cmbHomestay.Items.Add($"ðŸ  {hs.DisplayName}");
            }

            // Chon homestay hien tai
            if (homestayHienTaiId == 0)
            {
                cmbHomestay.SelectedIndex = 0;
            }
            else
            {
                int index = danhSachHomestay.FindIndex(h => h.HomestayId == homestayHienTaiId);
                cmbHomestay.SelectedIndex = (index >= 0) ? index + 1 : 0;
            }

            // Hien thi thong tin chi tiet
            var lblInfo = new Label
            {
                Text = "",
                Font = new DrawingFont("Segoe UI", 9F),
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(20, 95),
                Size = new Size(340, 80),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = CLR_FORM_BG
            };

            Action updateInfo = () =>
            {
                if (cmbHomestay.SelectedIndex == 0)
                {
                    lblInfo.Text = "Hiá»ƒn thá»‹ táº¥t cáº£ phÃ²ng tá»« má»i homestay.";
                }
                else if (cmbHomestay.SelectedIndex > 0)
                {
                    var hs = danhSachHomestay[cmbHomestay.SelectedIndex - 1];
                    lblInfo.Text = $"ðŸ“ Äá»‹a chá»‰: {hs.Address}\nðŸ“ž SÄT: {hs.Phone ?? "ChÆ°a cÃ³"}\n\nSá»‘ phÃ²ng: {demSoPhong(hs.HomestayId)} phÃ²ng";
                }
            };
            cmbHomestay.SelectedIndexChanged += (s, ev) => updateInfo();
            updateInfo();

            var btnChon = new Button
            {
                Text = "âœ“ Chá»n",
                Location = new Point(80, 190),
                Size = new Size(100, 40),
                BackColor = CLR_TRONG,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnChon.FlatAppearance.BorderSize = 0;

            var btnHuy = new Button
            {
                Text = "âœ— Há»§y",
                Location = new Point(200, 190),
                Size = new Size(100, 40),
                BackColor = CLR_DANG_DUNG,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnHuy.FlatAppearance.BorderSize = 0;

            btnChon.Click += (s, ev) =>
            {
                if (cmbHomestay.SelectedIndex == 0)
                {
                    homestayHienTaiId = 0; // Tat ca
                }
                else
                {
                    homestayHienTaiId = danhSachHomestay[cmbHomestay.SelectedIndex - 1].HomestayId;
                }
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            };

            btnHuy.Click += (s, ev) =>
            {
                dialog.DialogResult = DialogResult.Cancel;
                dialog.Close();
            };

            dialog.Controls.AddRange(new Control[] { lblTitle, cmbHomestay, lblInfo, btnChon, btnHuy });

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                CapNhatTieuDeHomestay();
                LoadDanhSachPhong();
                HienThiGridPhong();
            }
        }

        private int demSoPhong(int homestayId)
        {
            return danhSachPhong.Count(p => p.HomestayId == homestayId);
        }

        // â”€â”€ Data â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

        private void LoadDanhSachPhong()
        {
            if (phongBLL != null)
            {
                // Tá»± Ä‘á»™ng cáº­p nháº­t tráº¡ng thÃ¡i phÃ²ng Ä‘Ã£ quÃ¡ ngÃ y check-out
                try
                {
                    phongBLL.CapNhatPhongQuaHanCheckout();
                }
                catch
                {
                    // Bá» qua lá»—i náº¿u cÃ³, khÃ´ng áº£nh hÆ°á»Ÿng Ä‘áº¿n viá»‡c load danh sÃ¡ch
                }

                if (homestayHienTaiId == 0)
                {
                    // Lay tat ca phong
                    danhSachPhong = phongBLL.LayDanhSachTatCaPhong();
                }
                else
                {
                    // Lay phong theo homestay
                    danhSachPhong = homestayBLL.LayDanhSachPhongTheoHomestay(homestayHienTaiId);
                }
            }
        }

        private void LoadDanhSachHomestay()
        {
            if (homestayBLL != null)
            {
                danhSachHomestay = homestayBLL.LayDanhSachHomestay();
            }
        }

        private void CapNhatTieuDeHomestay()
        {
            if (homestayHienTaiId == 0)
            {
                lblTitle.Text = "ðŸ¨ SÆ  Äá»’ PHÃ’NG - Táº¤T Cáº¢ HOMESTAYS";
                toolTip1.SetToolTip(lblTitle, null);
            }
            else
            {
                var hs = danhSachHomestay.FirstOrDefault(h => h.HomestayId == homestayHienTaiId);
                if (hs != null)
                {
                    lblTitle.Text = $"ðŸ¨ {hs.HomestayName}";
                    toolTip1.SetToolTip(lblTitle, $"ðŸ“ {hs.Address}\nðŸ“ž {hs.Phone ?? "ChÆ°a cÃ³ SÄT"}");
                }
            }
        }

        // â”€â”€ Grid rendering â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

        private void HienThiGridPhong()
        {
            panelGrid.SuspendLayout();
            panelGrid.Controls.Clear();

            var sorted = danhSachPhong.OrderBy(p => p.SoPhong).ToList();

            // Calculate responsive button width based on available panel width
            int availableWidth = panelGrid.ClientSize.Width - (2 * GRID_PADDING) - (COLS - 1) * BTN_GAP - 20; // 20 for scrollbar
            int btnW = Math.Max(80, availableWidth / COLS);

            for (int i = 0; i < sorted.Count; i++)
            {
                var phong = sorted[i];
                int col = i % COLS;
                int row = i / COLS;

                int x = GRID_PADDING + col * (btnW + BTN_GAP);
                int y = GRID_PADDING + row * (BTN_H + BTN_GAP);

                var btn = TaoNutPhong(phong, x, y, btnW);
                panelGrid.Controls.Add(btn);
            }

            panelGrid.ResumeLayout();
        }

        private Button TaoNutPhong(Phong phong, int x, int y, int btnW)
        {
            Color bgColor  = LayMauTrangThai(phong.MauTrangThai);
            Color txtColor = (bgColor == CLR_DANG_DON)
                             ? CLR_TEXT_DARK
                             : CLR_TEXT_LIGHT;

            var btn = new Button
            {
                Text      = phong.SoPhong,
                Tag       = phong,
                BackColor = bgColor,
                ForeColor = txtColor,
                FlatStyle = FlatStyle.Flat,
                Font      = new DrawingFont("Segoe UI", 10F, FontStyle.Bold),
                Size      = new Size(btnW, BTN_H),
                Location  = new Point(x, y),
                Cursor    = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Name      = "btn_" + phong.SoPhong,
                TabIndex  = 0,
                Margin    = new Padding(3)
            };

            // Modern flat style with subtle shadow effect
            btn.FlatAppearance.BorderSize  = 0;
            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(bgColor, 0.1f);
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(bgColor, 0.1f);

            // Hover effect
            btn.MouseEnter += (s, e) => {
                btn.BackColor = ControlPaint.Light(bgColor, 0.15f);
            };
            btn.MouseLeave += (s, e) => {
                btn.BackColor = bgColor;
            };

            btn.Click += NutPhong_Click;

            // Show status as tooltip with modern styling
            var tip = new ToolTip();
            tip.BackColor = CLR_PANEL_BG;
            tip.ForeColor = CLR_TEXT_DARK;
            tip.ToolTipTitle = $"PhÃ²ng {phong.SoPhong}";
            tip.SetToolTip(btn,
                $"Loáº¡i: {phong.TenLoaiPhong}\n" +
                $"Tráº¡ng thÃ¡i: {phong.TenTrangThai}\n" +
                $"GiÃ¡/NgÃ y: {phong.GiaPhong:N0} VNÄ");

            return btn;
        }

        // â”€â”€ Add / Edit / Delete Room â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            try
            {
                List<RoomType> loaiPhongs = phongBLL.LayDanhSachLoaiPhong();
                List<Homestay> homestays = homestayBLL.LayDanhSachHomestay();

                if (loaiPhongs.Count == 0)
                {
                    MessageBox.Show("ChÆ°a cÃ³ loáº¡i phÃ²ng nÃ o! Vui lÃ²ng thÃªm loáº¡i phÃ²ng trÆ°á»›c.", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (homestays.Count == 0)
                {
                    MessageBox.Show("ChÆ°a cÃ³ homestay nÃ o! Vui lÃ²ng thÃªm homestay trÆ°á»›c.", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dialog = TaoDialogPhong("âž• ThÃªm PhÃ²ng Má»›i", null, loaiPhongs, homestays);

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var phongMoi = (Phong)dialog.Tag;
                    int newId = phongBLL.ThemPhongMoi(phongMoi);
                    MessageBox.Show($"ÄÃ£ thÃªm phÃ²ng {phongMoi.RoomName} thÃ nh cÃ´ng!\nMÃ£ phÃ²ng: {newId}", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachPhong();
                    HienThiGridPhong();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lá»—i thÃªm phÃ²ng: {ex.Message}", "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaPhong_Click(object sender, EventArgs e)
        {
            // Hien dialog chon phong can sua
            if (danhSachPhong.Count == 0)
            {
                MessageBox.Show("KhÃ´ng cÃ³ phÃ²ng nÃ o Ä‘á»ƒ sá»­a!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Phong phongChon = ChonPhongTuDanhSach("Chá»n phÃ²ng cáº§n sá»­a");
            if (phongChon == null) return;

            try
            {
                List<RoomType> loaiPhongs = phongBLL.LayDanhSachLoaiPhong();
                List<Homestay> homestays = homestayBLL.LayDanhSachHomestay();

                var dialog = TaoDialogPhong("âœï¸ Sá»­a PhÃ²ng", phongChon, loaiPhongs, homestays);

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var phongCapNhat = (Phong)dialog.Tag;
                    phongCapNhat.RoomId = phongChon.RoomId;
                    bool result = phongBLL.CapNhatThongTinPhong(phongCapNhat);
                    if (result)
                    {
                        MessageBox.Show($"ÄÃ£ cáº­p nháº­t phÃ²ng {phongCapNhat.RoomName} thÃ nh cÃ´ng!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachPhong();
                        HienThiGridPhong();
                    }
                    else
                    {
                        MessageBox.Show("KhÃ´ng thá»ƒ cáº­p nháº­t phÃ²ng!", "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lá»—i sá»­a phÃ²ng: {ex.Message}", "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaPhong_Click(object sender, EventArgs e)
        {
            if (danhSachPhong.Count == 0)
            {
                MessageBox.Show("KhÃ´ng cÃ³ phÃ²ng nÃ o Ä‘á»ƒ xÃ³a!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Phong phongChon = ChonPhongTuDanhSach("Chá»n phÃ²ng cáº§n xÃ³a");
            if (phongChon == null) return;

            DialogResult confirm = MessageBox.Show(
                $"Báº¡n cÃ³ cháº¯c muá»‘n xÃ³a phÃ²ng {phongChon.SoPhong}?\n(Homestay: {phongChon.HomestayName}, Loáº¡i: {phongChon.TenLoaiPhong})\n\nPhÃ²ng sáº½ bá»‹ áº©n khá»i danh sÃ¡ch (xÃ³a má»m).",
                "XÃ¡c nháº­n xÃ³a",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                bool result = phongBLL.XoaPhong(phongChon.RoomId);
                if (result)
                {
                    MessageBox.Show($"ÄÃ£ xÃ³a phÃ²ng {phongChon.SoPhong} thÃ nh cÃ´ng!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachPhong();
                    HienThiGridPhong();
                }
                else
                {
                    MessageBox.Show("KhÃ´ng thá»ƒ xÃ³a phÃ²ng!", "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lá»—i xÃ³a phÃ²ng: {ex.Message}", "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tao dialog them/sua phong
        /// </summary>
        private Form TaoDialogPhong(string title, Phong phongHienTai, List<RoomType> loaiPhongs, List<Homestay> homestays)
        {
            bool isEdit = phongHienTai != null;

            var dialog = new Form
            {
                Text = title,
                Size = new Size(420, 350),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = CLR_PANEL_BG
            };

            int y = 15;

            // Title
            var lblDlgTitle = new Label
            {
                Text = title,
                Font = new DrawingFont("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = CLR_HEADER_BG,
                Location = new Point(20, y),
                Size = new Size(370, 30)
            };
            y += 40;

            // Room Name
            var lblRoomName = new Label
            {
                Text = "TÃªn phÃ²ng (sá»‘ phÃ²ng):",
                Font = new DrawingFont("Segoe UI", 9.5F),
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(20, y),
                Size = new Size(150, 22)
            };
            var txtRoomName = new TextBox
            {
                Location = new Point(175, y - 2),
                Size = new Size(200, 25),
                Font = new DrawingFont("Segoe UI", 10F),
                Text = isEdit ? phongHienTai.RoomName : ""
            };
            y += 35;

            // Room Type
            var lblRoomType = new Label
            {
                Text = "Loáº¡i phÃ²ng:",
                Font = new DrawingFont("Segoe UI", 9.5F),
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(20, y),
                Size = new Size(150, 22)
            };
            var cmbRoomType = new ComboBox
            {
                Location = new Point(175, y - 2),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new DrawingFont("Segoe UI", 10F),
                BackColor = CLR_PANEL_BG,
                ForeColor = CLR_TEXT_DARK
            };
            foreach (var rt in loaiPhongs)
            {
                cmbRoomType.Items.Add(rt);
            }
            if (isEdit)
            {
                int idx = loaiPhongs.FindIndex(rt => rt.RoomTypeId == phongHienTai.RoomTypeId);
                cmbRoomType.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else
            {
                cmbRoomType.SelectedIndex = 0;
            }
            y += 35;

            // Homestay
            var lblHomestay = new Label
            {
                Text = "Homestay:",
                Font = new DrawingFont("Segoe UI", 9.5F),
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(20, y),
                Size = new Size(150, 22)
            };
            var cmbHomestay = new ComboBox
            {
                Location = new Point(175, y - 2),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new DrawingFont("Segoe UI", 10F),
                BackColor = CLR_PANEL_BG,
                ForeColor = CLR_TEXT_DARK
            };
            foreach (var hs in homestays)
            {
                cmbHomestay.Items.Add(hs.HomestayName);
            }
            if (isEdit)
            {
                int idx = homestays.FindIndex(h => h.HomestayId == phongHienTai.HomestayId);
                cmbHomestay.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else
            {
                cmbHomestay.SelectedIndex = 0;
            }
            y += 45;

            // Info label
            var lblInfo = new Label
            {
                Text = isEdit ? $"Äang sá»­a phÃ²ng: {phongHienTai.RoomName}" : "PhÃ²ng má»›i sáº½ cÃ³ tráº¡ng thÃ¡i: Trá»‘ng",
                Font = new DrawingFont("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(20, y),
                Size = new Size(350, 20)
            };
            y += 30;

            // Buttons
            var btnSave = new Button
            {
                Text = isEdit ? "ðŸ’¾ Cáº­p nháº­t" : "âž• ThÃªm má»›i",
                Location = new Point(80, y),
                Size = new Size(120, 40),
                BackColor = CLR_TRONG,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "âŒ Há»§y",
                Location = new Point(220, y),
                Size = new Size(120, 40),
                BackColor = Color.Gray,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            btnSave.Click += (s, ev) =>
            {
                // Validate
                if (string.IsNullOrWhiteSpace(txtRoomName.Text))
                {
                    MessageBox.Show("Vui lÃ²ng nháº­p tÃªn phÃ²ng!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRoomName.Focus();
                    return;
                }
                if (cmbRoomType.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lÃ²ng chá»n loáº¡i phÃ²ng!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbHomestay.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lÃ²ng chá»n homestay!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var phong = new Phong
                {
                    RoomName = txtRoomName.Text.Trim(),
                    RoomTypeId = loaiPhongs[cmbRoomType.SelectedIndex].RoomTypeId,
                    HomestayId = homestays[cmbHomestay.SelectedIndex].HomestayId
                };
                dialog.Tag = phong;
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            };

            btnCancel.Click += (s, ev) =>
            {
                dialog.DialogResult = DialogResult.Cancel;
                dialog.Close();
            };

            dialog.Controls.AddRange(new Control[] {
                lblDlgTitle, lblRoomName, txtRoomName,
                lblRoomType, cmbRoomType,
                lblHomestay, cmbHomestay,
                lblInfo, btnSave, btnCancel
            });

            return dialog;
        }

        /// <summary>
        /// Hien dialog chon phong tu danh sach (dung cho Sua/Xoa)
        /// </summary>
        private Phong ChonPhongTuDanhSach(string title)
        {
            var dialog = new Form
            {
                Text = title,
                Size = new Size(400, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = CLR_PANEL_BG
            };

            var lblDlgTitle = new Label
            {
                Text = title,
                Font = new DrawingFont("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = CLR_HEADER_BG,
                Location = new Point(20, 15),
                Size = new Size(350, 28)
            };

            var lstPhong = new ListBox
            {
                Location = new Point(20, 50),
                Size = new Size(345, 260),
                Font = new DrawingFont("Segoe UI", 10F),
                BackColor = CLR_PANEL_BG,
                ForeColor = CLR_TEXT_DARK
            };

            var sorted = danhSachPhong.OrderBy(p => p.SoPhong).ToList();
            foreach (var p in sorted)
            {
                lstPhong.Items.Add($"{p.SoPhong} - {p.TenLoaiPhong} ({p.TenTrangThai}) - {p.HomestayName}");
            }

            Phong phongChon = null;

            var btnChon = new Button
            {
                Text = "âœ“ Chá»n",
                Location = new Point(80, 320),
                Size = new Size(100, 38),
                BackColor = CLR_TRONG,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnChon.FlatAppearance.BorderSize = 0;

            var btnHuy = new Button
            {
                Text = "âœ— Há»§y",
                Location = new Point(210, 320),
                Size = new Size(100, 38),
                BackColor = CLR_DANG_DUNG,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnHuy.FlatAppearance.BorderSize = 0;

            btnChon.Click += (s, ev) =>
            {
                if (lstPhong.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lÃ²ng chá»n má»™t phÃ²ng!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                phongChon = sorted[lstPhong.SelectedIndex];
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            };

            btnHuy.Click += (s, ev) =>
            {
                dialog.DialogResult = DialogResult.Cancel;
                dialog.Close();
            };

            dialog.Controls.AddRange(new Control[] { lblDlgTitle, lstPhong, btnChon, btnHuy });

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                return phongChon;
            }
            return null;
        }

        private void NutPhong_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Phong phong)
            {
                if (IsRestrictedStaff())
                {
                    HienThiDialogTheoDoiPhong(phong);
                    return;
                }

                if (phong.TrangThai == TrangThaiPhong.DangThue)
                {
                    // PhÃ²ng Ä‘ang cÃ³ khÃ¡ch â†’ Hiá»ƒn thá»‹ dialog quáº£n lÃ½ dá»‹ch vá»¥
                    HienThiDialogDichVu(phong);
                }
                else
                {
                    // PhÃ²ng khÃ¡c â†’ Hiá»ƒn thá»‹ dialog Ä‘á»•i tráº¡ng thÃ¡i nhÆ° cÅ©
                    HienThiDialogDoiTrangThai(phong);
                }
            }
        }

        /// <summary>Dialog Ä‘á»•i tráº¡ng thÃ¡i cho phÃ²ng khÃ´ng pháº£i Occupied</summary>
        private void HienThiDialogTheoDoiPhong(Phong phong)
        {
            var dialog = new Form
            {
                Text = $"Phong {phong.SoPhong} - Theo doi",
                Size = new Size(430, 430),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = CLR_PANEL_BG,
                AutoScroll = true
            };

            int y = 15;

            Label CreateLabel(string text, bool bold = false, Color? foreColor = null)
            {
                var label = new Label
                {
                    Text = text,
                    Location = new Point(20, y),
                    Size = new Size(370, 22),
                    ForeColor = foreColor ?? CLR_TEXT_DARK,
                    Font = new DrawingFont("Segoe UI", 10F, bold ? FontStyle.Bold : FontStyle.Regular)
                };
                y += 27;
                return label;
            }

            dialog.Controls.Add(CreateLabel($"Ph\u00f2ng: {phong.SoPhong}", true, CLR_HEADER_BG));
            dialog.Controls.Add(CreateLabel($"Homestay: {phong.HomestayName}"));
            dialog.Controls.Add(CreateLabel($"Lo\u1ea1i ph\u00f2ng: {phong.TenLoaiPhong}"));
            dialog.Controls.Add(CreateLabel($"Gi\u00e1/ng\u00e0y: {phong.GiaPhong:N0} VN\u0110"));
            dialog.Controls.Add(CreateLabel($"Tr\u1ea1ng th\u00e1i: {phong.TenTrangThai}", true, LayMauTrangThai(phong.MauTrangThai)));

            if (phong.TrangThai == TrangThaiPhong.DangThue)
            {
                try
                {
                    DataTable bookingInfo = invoiceBLL.GetActiveBookingByRoomId(phong.RoomId);
                    if (bookingInfo.Rows.Count > 0)
                    {
                        DataRow booking = bookingInfo.Rows[0];
                        string customerName = booking["CustomerName"].ToString();
                        string customerPhone = booking["CustomerPhone"] != DBNull.Value
                            ? booking["CustomerPhone"].ToString()
                            : "Ch\u01b0a c\u00f3";
                        decimal deposit = Convert.ToDecimal(booking["Deposit"]);
                        DateTime checkIn = Convert.ToDateTime(booking["CheckInDate"]);
                        DateTime? checkOut = booking["CheckOutDate"] != DBNull.Value
                            ? (DateTime?)Convert.ToDateTime(booking["CheckOutDate"])
                            : null;

                        y += 8;
                        dialog.Controls.Add(CreateLabel("Th\u00f4ng tin \u0111\u1eb7t ph\u00f2ng", true, CLR_HEADER_BG));
                        dialog.Controls.Add(CreateLabel($"Kh\u00e1ch h\u00e0ng: {customerName}"));
                        dialog.Controls.Add(CreateLabel($"S\u0110T: {customerPhone}"));
                        dialog.Controls.Add(CreateLabel($"Check-in: {checkIn:dd/MM/yyyy HH:mm}"));
                        dialog.Controls.Add(CreateLabel(checkOut.HasValue
                            ? $"Check-out: {checkOut.Value:dd/MM/yyyy HH:mm}"
                            : "Check-out: Ch\u01b0a tr\u1ea3"));
                        dialog.Controls.Add(CreateLabel($"Ti\u1ec1n c\u1ecdc: {deposit:N0} VN\u0110"));
                    }
                }
                catch (Exception ex)
                {
                    dialog.Controls.Add(CreateLabel("Kh\u00f4ng t\u1ea3i \u0111\u01b0\u1ee3c th\u00f4ng tin \u0111\u1eb7t ph\u00f2ng: " + ex.Message));
                }
            }

            var btnClose = new Button
            {
                Text = "\u0110\u00f3ng",
                Location = new Point(150, Math.Max(y + 10, 330)),
                Size = new Size(120, 36),
                BackColor = Color.Gray,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, ev) => dialog.Close();

            dialog.Controls.Add(btnClose);
            dialog.ShowDialog(this);
        }

        private void HienThiDialogDoiTrangThai(Phong phong)
        {
            var dialog = new Form
            {
                Text = $"PhÃ²ng {phong.SoPhong} - Äá»•i tráº¡ng thÃ¡i",
                Size = new Size(400, 360),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = CLR_PANEL_BG
            };

            int y = 15;

            var lblTitle = new Label
            {
                Text = $"ðŸ¨ PhÃ²ng: {phong.SoPhong}",
                Font = new DrawingFont("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = CLR_HEADER_BG,
                Location = new Point(20, y),
                Size = new Size(350, 30)
            };
            y += 35;

            var lblHomestay = new Label
            {
                Text = $"ðŸ“ Homestay: {phong.HomestayName}",
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(20, y),
                Size = new Size(350, 20)
            };
            y += 25;

            var lblLoai = new Label
            {
                Text = $"ðŸ  Loáº¡i phÃ²ng: {phong.TenLoaiPhong}",
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(20, y),
                Size = new Size(350, 20)
            };
            y += 25;

            var lblGia = new Label
            {
                Text = $"ðŸ’° GiÃ¡/NgÃ y: {phong.GiaPhong:N0} VNÄ",
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(20, y),
                Size = new Size(350, 20)
            };
            y += 25;

            var lblCurrent = new Label
            {
                Text = $"ðŸŸ¢ Tráº¡ng thÃ¡i hiá»‡n táº¡i: {phong.TenTrangThai}",
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = LayMauTrangThai(phong.MauTrangThai),
                Location = new Point(20, y),
                Size = new Size(350, 25)
            };
            y += 35;

            var separator = new Label
            {
                Text = "â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€",
                ForeColor = CLR_BORDER,
                Location = new Point(20, y),
                Size = new Size(350, 20)
            };
            y += 25;

            var lblSelect = new Label
            {
                Text = "ðŸ”„ Äá»•i sang tráº¡ng thÃ¡i:",
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(20, y),
                Size = new Size(180, 25)
            };

            var cmbStatus = new ComboBox
            {
                Location = new Point(200, y),
                Size = new Size(160, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = CLR_PANEL_BG,
                ForeColor = CLR_TEXT_DARK,
                Font = new DrawingFont("Segoe UI", 10F)
            };
            cmbStatus.Items.AddRange(new object[] { "Trá»‘ng", "Äang thuÃª", "Äang dá»n", "Báº£o trÃ¬" });
            cmbStatus.SelectedIndex = (int)phong.TrangThai;
            y += 40;

            var btnSave = new Button
            {
                Text = "ðŸ’¾ LÆ°u thay Ä‘á»•i",
                Location = new Point(60, y),
                Size = new Size(120, 40),
                BackColor = CLR_TRONG,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "âŒ ÄÃ³ng",
                Location = new Point(200, y),
                Size = new Size(120, 40),
                BackColor = Color.Gray,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            TrangThaiPhong? newStatus = null;

            btnSave.Click += (s, ev) =>
            {
                newStatus = (TrangThaiPhong)cmbStatus.SelectedIndex;
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            };

            btnCancel.Click += (s, ev) =>
            {
                dialog.DialogResult = DialogResult.Cancel;
                dialog.Close();
            };

            dialog.Controls.AddRange(new Control[] {
                lblTitle, lblHomestay, lblLoai, lblGia, lblCurrent,
                separator, lblSelect, cmbStatus, btnSave, btnCancel
            });

            if (dialog.ShowDialog(this) == DialogResult.OK && newStatus.HasValue && newStatus.Value != phong.TrangThai)
            {
                try
                {
                    bool result = phongBLL.CapNhatTrangThaiPhong(phong.RoomId, newStatus.Value);
                    if (result)
                    {
                        MessageBox.Show("ÄÃ£ cáº­p nháº­t tráº¡ng thÃ¡i phÃ²ng thÃ nh cÃ´ng!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachPhong();
                        HienThiGridPhong();
                    }
                    else
                    {
                        MessageBox.Show("KhÃ´ng thá»ƒ cáº­p nháº­t tráº¡ng thÃ¡i!", "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lá»—i: {ex.Message}", "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>Dialog quáº£n lÃ½ dá»‹ch vá»¥ cho phÃ²ng Ä‘ang cÃ³ khÃ¡ch (Occupied)</summary>
        private void HienThiDialogDichVu(Phong phong)
        {
            // Láº¥y thÃ´ng tin booking Ä‘ang hoáº¡t Ä‘á»™ng
            DataTable bookingInfo = invoiceBLL.GetActiveBookingByRoomId(phong.RoomId);
            if (bookingInfo.Rows.Count == 0)
            {
                MessageBox.Show("KhÃ´ng tÃ¬m tháº¥y thÃ´ng tin Ä‘áº·t phÃ²ng cho phÃ²ng nÃ y!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow booking = bookingInfo.Rows[0];
            int bookingId = Convert.ToInt32(booking["BookingId"]);
            string customerName = booking["CustomerName"].ToString();
            string customerPhone = booking["CustomerPhone"] != DBNull.Value ? booking["CustomerPhone"].ToString() : "ChÆ°a cÃ³";
            decimal deposit = Convert.ToDecimal(booking["Deposit"]);
            DateTime checkIn = Convert.ToDateTime(booking["CheckInDate"]);
            DateTime? checkOut = booking["CheckOutDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(booking["CheckOutDate"]) : null;

            // Táº¡o dialog lá»›n
            var dialog = new Form
            {
                Text = $"ðŸ¨ PhÃ²ng {phong.SoPhong} - Quáº£n lÃ½ dá»‹ch vá»¥",
                Size = new Size(750, 620),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = CLR_PANEL_BG
            };

            int y = 10;

            // ===== PHáº¦N THÃ”NG TIN KHÃCH HÃ€NG =====
            var panelInfo = new Panel
            {
                Location = new Point(10, y),
                Size = new Size(720, 120),
                BackColor = Color.FromArgb(240, 253, 244),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblInfoTitle = new Label
            {
                Text = "ðŸ“‹ THÃ”NG TIN KHÃCH HÃ€NG",
                Font = new DrawingFont("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = CLR_HEADER_BG,
                Location = new Point(10, 8),
                Size = new Size(300, 22)
            };

            var lblCustomer = new Label
            {
                Text = $"ðŸ‘¤ KhÃ¡ch hÃ ng: {customerName}",
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(10, 35),
                Size = new Size(350, 20)
            };

            var lblPhone = new Label
            {
                Text = $"ðŸ“ž SÄT: {customerPhone}",
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(370, 35),
                Size = new Size(330, 20)
            };

            var lblCheckInInfo = new Label
            {
                Text = $"ðŸ“… Check-in: {checkIn:dd/MM/yyyy HH:mm}",
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(10, 60),
                Size = new Size(250, 20)
            };

            var lblCheckOutInfo = new Label
            {
                Text = checkOut.HasValue ? $"ðŸ“… Check-out: {checkOut.Value:dd/MM/yyyy HH:mm}" : "ðŸ“… Check-out: ChÆ°a tráº£",
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(270, 60),
                Size = new Size(250, 20)
            };

            var lblDepositInfo = new Label
            {
                Text = $"ðŸ’µ Tiá»n cá»c: {deposit:N0} VNÄ",
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74),
                Location = new Point(10, 85),
                Size = new Size(300, 22)
            };

            var lblRoomPriceInfo = new Label
            {
                Text = $"ðŸ’° GiÃ¡/NgÃ y: {phong.GiaPhong:N0} VNÄ",
                Font = new DrawingFont("Segoe UI", 10F),
                ForeColor = CLR_TEXT_DARK,
                Location = new Point(370, 85),
                Size = new Size(330, 22)
            };

            panelInfo.Controls.AddRange(new Control[] {
                lblInfoTitle, lblCustomer, lblPhone,
                lblCheckInInfo, lblCheckOutInfo, lblDepositInfo, lblRoomPriceInfo
            });
            y += 130;

            // ===== PHáº¦N Dá»ŠCH Vá»¤ ÄÃƒ THÃŠM =====
            var lblServiceTitle = new Label
            {
                Text = "ðŸ›’ Dá»ŠCH Vá»¤ ÄÃƒ THÃŠM",
                Font = new DrawingFont("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = CLR_HEADER_BG,
                Location = new Point(10, y),
                Size = new Size(300, 22)
            };
            y += 25;

            var dgvServices = new DataGridView
            {
                Location = new Point(10, y),
                Size = new Size(720, 150),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new DrawingFont("Segoe UI", 9F)
            };
            dgvServices.ColumnHeadersDefaultCellStyle.BackColor = CLR_HEADER_BG;
            dgvServices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvServices.ColumnHeadersDefaultCellStyle.Font = new DrawingFont("Segoe UI", 9F, FontStyle.Bold);
            dgvServices.EnableHeadersVisualStyles = false;
            dgvServices.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 255);
            y += 160;

            // ===== Tá»”NG Dá»ŠCH Vá»¤ (khai bÃ¡o trÆ°á»›c lambda) =====
            var lblTotalService = new Label
            {
                Text = "ðŸ’° Tá»•ng dá»‹ch vá»¥: 0 VNÄ",
                Font = new DrawingFont("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74),
                Location = new Point(10, y),
                Size = new Size(350, 25)
            };

            // Load dá»‹ch vá»¥
            Action loadServices = () =>
            {
                try
                {
                    DataTable services = LoadServicesForRoom(phong.RoomId);
                    dgvServices.DataSource = services;

                    if (dgvServices.Columns.Contains("DetailID"))
                        dgvServices.Columns["DetailID"].Visible = false;
                    if (dgvServices.Columns.Contains("ServiceName"))
                        dgvServices.Columns["ServiceName"].HeaderText = "TÃªn dá»‹ch vá»¥";
                    if (dgvServices.Columns.Contains("Quantity"))
                        dgvServices.Columns["Quantity"].HeaderText = "SL";
                    if (dgvServices.Columns.Contains("UnitPrice"))
                    {
                        dgvServices.Columns["UnitPrice"].HeaderText = "ÄÆ¡n giÃ¡";
                        dgvServices.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
                    }
                    if (dgvServices.Columns.Contains("TotalPrice"))
                    {
                        dgvServices.Columns["TotalPrice"].HeaderText = "ThÃ nh tiá»n";
                        dgvServices.Columns["TotalPrice"].DefaultCellStyle.Format = "N0";
                    }

                    // TÃ­nh tá»•ng dá»‹ch vá»¥
                    decimal totalService = 0;
                    foreach (DataRow row in services.Rows)
                    {
                        totalService += Convert.ToDecimal(row["TotalPrice"]);
                    }
                    lblTotalService.Text = $"ðŸ’° Tá»•ng dá»‹ch vá»¥: {totalService:N0} VNÄ";
                }
                catch (Exception ex)
                {
                    dgvServices.DataSource = null;
                    lblTotalService.Text = "ðŸ’° Tá»•ng dá»‹ch vá»¥: 0 VNÄ";
                    System.Diagnostics.Debug.WriteLine("LoadServices error: " + ex.Message);
                }
            };

            // ===== PHáº¦N THÃŠM Dá»ŠCH Vá»¤ Má»šI =====
            var panelAddService = new Panel
            {
                Location = new Point(10, y),
                Size = new Size(720, 100),
                BackColor = Color.FromArgb(239, 246, 255),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblAddTitle = new Label
            {
                Text = "âž• THÃŠM Dá»ŠCH Vá»¤",
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = CLR_HEADER_BG,
                Location = new Point(10, 8),
                Size = new Size(200, 20)
            };

            // Load danh sÃ¡ch dá»‹ch vá»¥
            DataTable servicesList = new DataTable();
            try
            {
                servicesList = serviceBLL.LayDanhSachDichVu();
            }
            catch { }

            var cmbService = new ComboBox
            {
                Location = new Point(10, 35),
                Size = new Size(250, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new DrawingFont("Segoe UI", 9.5F),
                BackColor = Color.White
            };
            foreach (DataRow row in servicesList.Rows)
            {
                cmbService.Items.Add($"{row["ServiceName"]} - {Convert.ToDecimal(row["Price"]):N0} VNÄ/{row["Unit"]}");
            }
            if (cmbService.Items.Count > 0) cmbService.SelectedIndex = 0;

            var lblQty = new Label
            {
                Text = "SL:",
                Location = new Point(270, 38),
                Size = new Size(30, 20),
                Font = new DrawingFont("Segoe UI", 9.5F)
            };

            var numQty = new NumericUpDown
            {
                Location = new Point(300, 35),
                Size = new Size(60, 25),
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                Font = new DrawingFont("Segoe UI", 9.5F)
            };

            var btnAddService = new Button
            {
                Text = "âž• ThÃªm",
                Location = new Point(380, 32),
                Size = new Size(90, 32),
                BackColor = CLR_TRONG,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 9.5F, FontStyle.Bold)
            };
            btnAddService.FlatAppearance.BorderSize = 0;

            var btnHuyService = new Button
            {
                Text = "ðŸ—‘ï¸ Há»§y DV",
                Location = new Point(480, 32),
                Size = new Size(100, 32),
                BackColor = CLR_DANG_DUNG,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 9.5F, FontStyle.Bold)
            };
            btnHuyService.FlatAppearance.BorderSize = 0;

            var btnDoiTrangThai = new Button
            {
                Text = "ðŸ”„ Äá»•i tráº¡ng thÃ¡i",
                Location = new Point(595, 32),
                Size = new Size(115, 32),
                BackColor = CLR_DA_DAT,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 9F, FontStyle.Bold)
            };
            btnDoiTrangThai.FlatAppearance.BorderSize = 0;

            panelAddService.Controls.AddRange(new Control[] {
                lblAddTitle, cmbService, lblQty, numQty, btnAddService, btnHuyService, btnDoiTrangThai
            });
            y += 110;

            // lblTotalService Ä‘Ã£ Ä‘Æ°á»£c khai bÃ¡o á»Ÿ trÃªn (trÆ°á»›c lambda loadServices)
            y += 35;

            // TÃ­nh tiá»n phÃ²ng
            var (days, calculatedRoomAmount) = invoiceBLL.CalculateRoomAmount(checkIn, phong.GiaPhong);
            decimal roomAmount = calculatedRoomAmount;

            // ===== NÃšT THANH TOÃN & ÄÃ“NG =====
            var btnCheckout = new Button
            {
                Text = "ðŸ’³ Thanh toÃ¡n",
                Location = new Point(200, y),
                Size = new Size(160, 40),
                BackColor = Color.FromArgb(34, 197, 94),
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnCheckout.FlatAppearance.BorderSize = 0;

            var btnClose = new Button
            {
                Text = "âŒ ÄÃ³ng",
                Location = new Point(380, y),
                Size = new Size(140, 40),
                BackColor = Color.Gray,
                ForeColor = CLR_TEXT_LIGHT,
                FlatStyle = FlatStyle.Flat,
                Font = new DrawingFont("Segoe UI", 10F, FontStyle.Bold)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, ev) => dialog.Close();

            // ===== EVENT HANDLERS =====

            // ThÃªm dá»‹ch vá»¥
            btnAddService.Click += (s, ev) =>
            {
                if (cmbService.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lÃ²ng chá»n dá»‹ch vá»¥!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    DataRow selectedService = servicesList.Rows[cmbService.SelectedIndex];
                    int serviceID = Convert.ToInt32(selectedService["ServiceId"]);
                    int quantity = (int)numQty.Value;
                    decimal unitPrice = Convert.ToDecimal(selectedService["Price"]);

                    // Láº¥y hoáº·c táº¡o TempInvoice
                    int invoiceID = invoiceBLL.GetOrCreateTempInvoice(phong.RoomId);

                    // ThÃªm dá»‹ch vá»¥
                    bool added = invoiceBLL.AddServiceToInvoice(invoiceID, serviceID, quantity, unitPrice);
                    if (added)
                    {
                        MessageBox.Show("ÄÃ£ thÃªm dá»‹ch vá»¥ thÃ nh cÃ´ng!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadServices();
                        numQty.Value = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lá»—i thÃªm dá»‹ch vá»¥: " + ex.Message, "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Há»§y dá»‹ch vá»¥
            btnHuyService.Click += (s, ev) =>
            {
                if (dgvServices.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lÃ²ng chá»n dá»‹ch vá»¥ cáº§n há»§y!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow selectedRow = dgvServices.SelectedRows[0];
                string serviceName = selectedRow.Cells["ServiceName"].Value?.ToString() ?? "dá»‹ch vá»¥";

                DialogResult confirm = MessageBox.Show(
                    $"Báº¡n cÃ³ cháº¯c muá»‘n há»§y dá»‹ch vá»¥ '{serviceName}'?\nDá»‹ch vá»¥ sáº½ Ä‘Æ°á»£c Ä‘Ã¡nh dáº¥u lÃ  Ä‘Ã£ há»§y.",
                    "XÃ¡c nháº­n há»§y",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        int detailID = Convert.ToInt32(selectedRow.Cells["DetailID"].Value);
                        invoiceBLL.HuyDichVu(detailID);
                        MessageBox.Show("ÄÃ£ há»§y dá»‹ch vá»¥!", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadServices();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lá»—i há»§y dá»‹ch vá»¥: " + ex.Message, "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            // Äá»•i tráº¡ng thÃ¡i phÃ²ng
            btnDoiTrangThai.Click += (s, ev) =>
            {
                dialog.Hide();
                HienThiDialogDoiTrangThai(phong);
                dialog.Close();
            };

            // Thanh toÃ¡n & Tráº£ phÃ²ng
            btnCheckout.Click += (s, ev) =>
            {
                try
                {
                    // Láº¥y tá»•ng dá»‹ch vá»¥ tá»« DataTable
                    decimal serviceAmount = 0;
                    DataTable currentServices = LoadServicesForRoom(phong.RoomId);
                    foreach (DataRow row in currentServices.Rows)
                    {
                        serviceAmount += Convert.ToDecimal(row["TotalPrice"]);
                    }

                    decimal totalPay = roomAmount + serviceAmount - deposit;
                    if (totalPay < 0) totalPay = 0;

                    string msg = $"ðŸ’° XÃC NHáº¬N THANH TOÃN\n\n" +
                                 $"ðŸ‘¤ KhÃ¡ch: {customerName}\n" +
                                 $"ðŸšª PhÃ²ng: {phong.SoPhong}\n\n" +
                                 $"ðŸ“… á»ž {days} ngÃ y x {phong.GiaPhong:N0} = {roomAmount:N0} VNÄ\n" +
                                 $"ðŸ›’ Dá»‹ch vá»¥: {serviceAmount:N0} VNÄ\n" +
                                 $"ðŸ’µ ÄÃ£ cá»c: {deposit:N0} VNÄ\n" +
                                 $"â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€\n" +
                                 $"ðŸ’³ CÃ’N PHáº¢I TRáº¢: {totalPay:N0} VNÄ";

                    DialogResult confirm = MessageBox.Show(msg, "XÃ¡c nháº­n thanh toÃ¡n", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;

                    bool ok = invoiceBLL.ProcessCheckout(bookingId, roomAmount, serviceAmount, deposit);
                    if (ok)
                    {
                        MessageBox.Show("Thanh toÃ¡n thÃ nh cÃ´ng! PhÃ²ng Ä‘Ã£ Ä‘Æ°á»£c giáº£i phÃ³ng.", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Há»i xuáº¥t hÃ³a Ä‘Æ¡n PDF
                        DialogResult exportPdf = MessageBox.Show(
                            "Báº¡n cÃ³ muá»‘n xuáº¥t hÃ³a Ä‘Æ¡n PDF?",
                            "Xuáº¥t hÃ³a Ä‘Æ¡n",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (exportPdf == DialogResult.Yes)
                        {
                            // DÃ¹ng láº¡i currentServices Ä‘Ã£ load TRÆ¯á»šC khi ProcessCheckout
                            // (vÃ¬ sau khi ProcessCheckout, TempInvoice bá»‹ Ä‘Ã¡nh dáº¥u 'ÄÃ£ thanh toÃ¡n'
                            //  nÃªn LoadServicesForRoom sáº½ tráº£ vá» rá»—ng)
                            string homestayName = phong.HomestayName ?? "";
                            string filePath = ExportInvoicePDF(
                                customerName, customerPhone, phong.SoPhong, homestayName,
                                checkIn, checkOut, days, phong.GiaPhong, roomAmount,
                                currentServices, deposit, totalPay);

                            if (!string.IsNullOrEmpty(filePath))
                            {
                                DialogResult openFile = MessageBox.Show(
                                    $"HÃ³a Ä‘Æ¡n Ä‘Ã£ lÆ°u táº¡i:\n{filePath}\n\nBáº¡n cÃ³ muá»‘n má»Ÿ file khÃ´ng?",
                                    "Xuáº¥t PDF thÃ nh cÃ´ng",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Information);
                                if (openFile == DialogResult.Yes)
                                {
                                    System.Diagnostics.Process.Start(filePath);
                                }
                            }
                        }

                        dialog.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thanh toÃ¡n tháº¥t báº¡i!", "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lá»—i thanh toÃ¡n: " + ex.Message, "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // ThÃªm controls vÃ o dialog
            dialog.Controls.AddRange(new Control[] {
                panelInfo, lblServiceTitle, dgvServices,
                panelAddService, lblTotalService, btnCheckout, btnClose
            });

            // Load dá»‹ch vá»¥ láº§n Ä‘áº§u
            loadServices();

            dialog.ShowDialog(this);

            // Refresh grid sau khi Ä‘Ã³ng dialog
            LoadDanhSachPhong();
            HienThiGridPhong();
        }

        /// <summary>Load danh sÃ¡ch dá»‹ch vá»¥ Ä‘Ã£ thÃªm cho phÃ²ng (tá»« TempInvoiceDetails)</summary>
        private DataTable LoadServicesForRoom(int roomId)
        {
            try
            {
                return invoiceBLL.GetActiveServicesByRoom(roomId);
            }
            catch { }
            return new DataTable();
        }

        // â”€â”€ Xuáº¥t hÃ³a Ä‘Æ¡n PDF â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

        private string ExportInvoicePDF(string customerName, string customerPhone, string roomName,
            string homestayName, DateTime checkIn, DateTime? checkOut, int days, decimal roomPrice,
            decimal roomAmount, DataTable services, decimal deposit, decimal totalPay)
        {
            var saveDlg = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"HoaDon_{roomName}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                Title = "LÆ°u hÃ³a Ä‘Æ¡n PDF"
            };
            if (saveDlg.ShowDialog() != DialogResult.OK) return string.Empty;

            string filePath = saveDlg.FileName;
            try
            {
                using (var doc = new Document(PageSize.A4, 40, 40, 40, 40))
                {
                    PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                    doc.Open();

                    var fontTitle = FontFactory.GetFont("Arial", 18, PdfFont.BOLD, BaseColor.DARK_GRAY);
                    var fontHeader = FontFactory.GetFont("Arial", 12, PdfFont.BOLD, BaseColor.WHITE);
                    var fontNormal = FontFactory.GetFont("Arial", 11, PdfFont.NORMAL, BaseColor.DARK_GRAY);
                    var fontBold = FontFactory.GetFont("Arial", 11, PdfFont.BOLD, BaseColor.DARK_GRAY);
                    var fontGreen = FontFactory.GetFont("Arial", 12, PdfFont.BOLD, new BaseColor(34, 197, 94));
                    var fontRed = FontFactory.GetFont("Arial", 12, PdfFont.BOLD, new BaseColor(220, 38, 38));

                    // Title
                    doc.Add(new Paragraph("HÃ“A ÄÆ N THANH TOÃN", fontTitle) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph($"NgÃ y: {DateTime.Now:dd/MM/yyyy HH:mm}", fontNormal) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph("\n"));

                    // Info table
                    var infoTable = new PdfPTable(2) { WidthPercentage = 100, HorizontalAlignment = Element.ALIGN_LEFT };
                    infoTable.SetWidths(new float[] { 1.5f, 3f });
                    infoTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    infoTable.DefaultCell.Padding = 5;

                    void AddInfoRow(string label, string value)
                    {
                        infoTable.AddCell(new Phrase(label, fontBold));
                        infoTable.AddCell(new Phrase(value, fontNormal));
                    }

                    AddInfoRow("KhÃ¡ch hÃ ng:", customerName);
                    AddInfoRow("Sá»‘ Ä‘iá»‡n thoáº¡i:", customerPhone);
                    AddInfoRow("Homestay:", homestayName);
                    AddInfoRow("PhÃ²ng:", roomName);
                    AddInfoRow("NgÃ y nháº­n:", checkIn.ToString("dd/MM/yyyy HH:mm"));
                    AddInfoRow("NgÃ y tráº£:", checkOut.HasValue ? checkOut.Value.ToString("dd/MM/yyyy HH:mm") : DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    doc.Add(infoTable);
                    doc.Add(new Paragraph("\n"));

                    // Room fee table
                    var roomTable = new PdfPTable(4) { WidthPercentage = 100 };
                    roomTable.SetWidths(new float[] { 3f, 1.5f, 2f, 2f });
                    roomTable.DefaultCell.Padding = 6;
                    roomTable.DefaultCell.BackgroundColor = new BaseColor(243, 244, 246);

                    roomTable.AddCell(new Phrase("MÃ´ táº£", fontBold));
                    roomTable.AddCell(new Phrase("Sá»‘ ngÃ y", fontBold));
                    roomTable.AddCell(new Phrase("ÄÆ¡n giÃ¡", fontBold));
                    roomTable.AddCell(new Phrase("ThÃ nh tiá»n", fontBold));

                    roomTable.DefaultCell.BackgroundColor = BaseColor.WHITE;
                    roomTable.AddCell(new Phrase("Tiá»n phÃ²ng", fontNormal));
                    roomTable.AddCell(new Phrase(days.ToString(), fontNormal));
                    roomTable.AddCell(new Phrase($"{roomPrice:N0} VNÄ", fontNormal));
                    roomTable.AddCell(new Phrase($"{roomAmount:N0} VNÄ", fontNormal));
                    doc.Add(roomTable);
                    doc.Add(new Paragraph("\n"));

                    // Services table
                    if (services != null && services.Rows.Count > 0)
                    {
                        var svcTable = new PdfPTable(4) { WidthPercentage = 100 };
                        svcTable.SetWidths(new float[] { 3.5f, 1f, 2f, 2f });
                        svcTable.DefaultCell.Padding = 6;
                        svcTable.DefaultCell.BackgroundColor = new BaseColor(243, 244, 246);

                        svcTable.AddCell(new Phrase("Dá»‹ch vá»¥", fontBold));
                        svcTable.AddCell(new Phrase("SL", fontBold));
                        svcTable.AddCell(new Phrase("ÄÆ¡n giÃ¡", fontBold));
                        svcTable.AddCell(new Phrase("ThÃ nh tiá»n", fontBold));

                        svcTable.DefaultCell.BackgroundColor = BaseColor.WHITE;
                        foreach (DataRow row in services.Rows)
                        {
                            svcTable.AddCell(new Phrase(row["ServiceName"].ToString(), fontNormal));
                            svcTable.AddCell(new Phrase(row["Quantity"].ToString(), fontNormal));
                            svcTable.AddCell(new Phrase($"{Convert.ToDecimal(row["UnitPrice"]):N0} VNÄ", fontNormal));
                            svcTable.AddCell(new Phrase($"{Convert.ToDecimal(row["TotalPrice"]):N0} VNÄ", fontNormal));
                        }
                        doc.Add(svcTable);
                        doc.Add(new Paragraph("\n"));
                    }

                    // Totals
                    var totalTable = new PdfPTable(2) { WidthPercentage = 60, HorizontalAlignment = Element.ALIGN_RIGHT };
                    totalTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    totalTable.DefaultCell.Padding = 5;
                    totalTable.SetWidths(new float[] { 2f, 2f });

                    totalTable.AddCell(new Phrase("Tiá»n phÃ²ng:", fontBold));
                    totalTable.AddCell(new Phrase($"{roomAmount:N0} VNÄ", fontNormal));

                    decimal serviceAmount = 0;
                    if (services != null)
                    {
                        foreach (DataRow row in services.Rows)
                            serviceAmount += Convert.ToDecimal(row["TotalPrice"]);
                    }
                    totalTable.AddCell(new Phrase("Dá»‹ch vá»¥:", fontBold));
                    totalTable.AddCell(new Phrase($"{serviceAmount:N0} VNÄ", fontNormal));

                    totalTable.AddCell(new Phrase("ÄÃ£ cá»c:", fontBold));
                    totalTable.AddCell(new Phrase($"-{deposit:N0} VNÄ", fontRed));

                    totalTable.AddCell(new Phrase("Tá»”NG CÃ’N PHáº¢I TRáº¢:", fontGreen));
                    totalTable.AddCell(new Phrase($"{totalPay:N0} VNÄ", fontGreen));

                    doc.Add(totalTable);
                    doc.Add(new Paragraph("\n\n"));

                    // Footer
                    var footer = new Paragraph("Cáº£m Æ¡n quÃ½ khÃ¡ch Ä‘Ã£ sá»­ dá»¥ng dá»‹ch vá»¥!", fontNormal)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(footer);

                    doc.Close();
                }
                return filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i xuáº¥t PDF: " + ex.Message, "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        // â”€â”€ Helpers â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€

        private Color LayMauTrangThai(ConsoleColor consoleColor)
        {
            switch (consoleColor)
            {
                case ConsoleColor.Red:    return CLR_DANG_DUNG;
                case ConsoleColor.Green:  return CLR_TRONG;
                case ConsoleColor.Yellow: return CLR_DANG_DON;
                case ConsoleColor.Gray:   return CLR_BAO_TRI;
                case ConsoleColor.Cyan:   return CLR_DA_DAT;
                default:                  return Color.FromArgb(50, 50, 50);
            }
        }
    }
}
