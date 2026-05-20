using System;
using System.Data;
using System.Windows.Forms;
using Quan_ly_Homestay.BLL;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.GUI
{
    public partial class FormHomestay : Form
    {
        private readonly HomestayBLL homestayBLL = new HomestayBLL();
        private int selectedHomestayId = -1;

        public FormHomestay()
        {
            InitializeComponent();
        }

        private void FormHomestay_Load(object sender, EventArgs e)
        {
            LoadHomestays();
        }

        private void LoadHomestays()
        {
            try
            {
                DataTable table = homestayBLL.GetAll();
                dgvHomestays.DataSource = table;

                if (dgvHomestays.Columns.Contains("HomestayId"))
                {
                    dgvHomestays.Columns["HomestayId"].Visible = false;
                }

                if (dgvHomestays.Columns.Contains("HomestayName"))
                {
                    dgvHomestays.Columns["HomestayName"].HeaderText = "T\u00ean Homestay";
                }

                if (dgvHomestays.Columns.Contains("Address"))
                {
                    dgvHomestays.Columns["Address"].HeaderText = "\u0110\u1ecba ch\u1ec9";
                }

                if (dgvHomestays.Columns.Contains("Phone"))
                {
                    dgvHomestays.Columns["Phone"].HeaderText = "S\u1ed1 \u0111i\u1ec7n tho\u1ea1i";
                }

                ClearInput();
                dgvHomestays.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "L\u1ed7i t\u1ea3i danh s\u00e1ch Homestay: " + ex.Message,
                    "L\u1ed7i",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearInput()
        {
            selectedHomestayId = -1;
            txtHomestayName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtHomestayName.Focus();
        }

        private bool ValidateInput()
        {
            if (txtHomestayName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(
                    "Vui l\u00f2ng nh\u1eadp t\u00ean Homestay!",
                    "Th\u00f4ng b\u00e1o",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtHomestayName.Focus();
                return false;
            }

            if (txtPhone.Text.Trim() == string.Empty)
            {
                MessageBox.Show(
                    "Vui l\u00f2ng nh\u1eadp s\u1ed1 \u0111i\u1ec7n tho\u1ea1i!",
                    "Th\u00f4ng b\u00e1o",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                string name = txtHomestayName.Text.Trim();
                string address = txtAddress.Text.Trim();
                string phone = txtPhone.Text.Trim();

                bool result = homestayBLL.Add(name, address, phone);

                if (result)
                {
                    MessageBox.Show(
                        "Th\u00eam Homestay th\u00e0nh c\u00f4ng!",
                        "Th\u00f4ng b\u00e1o",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LoadHomestays();
                }
                else
                {
                    MessageBox.Show(
                        "Th\u00eam Homestay th\u1ea5t b\u1ea1i!",
                        "Th\u00f4ng b\u00e1o",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "L\u1ed7i th\u00eam Homestay: " + ex.Message,
                    "L\u1ed7i",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedHomestayId == -1)
            {
                MessageBox.Show(
                    "Vui l\u00f2ng ch\u1ecdn Homestay c\u1ea7n s\u1eeda!",
                    "Th\u00f4ng b\u00e1o",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
            {
                return;
            }

            try
            {
                string name = txtHomestayName.Text.Trim();
                string address = txtAddress.Text.Trim();
                string phone = txtPhone.Text.Trim();

                bool result = homestayBLL.Update(selectedHomestayId, name, address, phone);

                if (result)
                {
                    MessageBox.Show(
                        "S\u1eeda Homestay th\u00e0nh c\u00f4ng!",
                        "Th\u00f4ng b\u00e1o",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LoadHomestays();
                }
                else
                {
                    MessageBox.Show(
                        "S\u1eeda Homestay th\u1ea5t b\u1ea1i!",
                        "Th\u00f4ng b\u00e1o",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "L\u1ed7i s\u1eeda Homestay: " + ex.Message,
                    "L\u1ed7i",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedHomestayId == -1)
            {
                MessageBox.Show(
                    "Vui l\u00f2ng ch\u1ecdn Homestay c\u1ea7n x\u00f3a!",
                    "Th\u00f4ng b\u00e1o",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "B\u1ea1n c\u00f3 ch\u1eafc mu\u1ed1n x\u00f3a m\u1ec1m Homestay n\u00e0y kh\u00f4ng?",
                "X\u00e1c nh\u1eadn x\u00f3a",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return;
            }

            try
            {
                bool result = homestayBLL.SoftDelete(selectedHomestayId);

                if (result)
                {
                    MessageBox.Show(
                        "X\u00f3a m\u1ec1m Homestay th\u00e0nh c\u00f4ng!",
                        "Th\u00f4ng b\u00e1o",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LoadHomestays();
                }
                else
                {
                    MessageBox.Show(
                        "X\u00f3a m\u1ec1m th\u1ea5t b\u1ea1i!",
                        "Th\u00f4ng b\u00e1o",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "L\u1ed7i x\u00f3a Homestay: " + ex.Message,
                    "L\u1ed7i",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadHomestays();
        }

        private void dgvHomestays_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvHomestays.Rows[e.RowIndex];

            selectedHomestayId = Convert.ToInt32(row.Cells["HomestayId"].Value);
            txtHomestayName.Text = Convert.ToString(row.Cells["HomestayName"].Value);
            txtAddress.Text = Convert.ToString(row.Cells["Address"].Value);
            txtPhone.Text = Convert.ToString(row.Cells["Phone"].Value);
        }
    }
}
