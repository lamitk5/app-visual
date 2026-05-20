using System;
using System.Windows.Forms;
using Quan_ly_Homestay.BLL;
using Quan_ly_Homestay.Model;

namespace Quan_ly_Homestay.GUI
{
    public partial class FormLogin : Form
    {
        private readonly AuthBLL authBLL = new AuthBLL();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (username == string.Empty)
            {
                MessageBox.Show(
                    "Vui lòng nhập tên đăng nhập!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (password == string.Empty)
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                CurrentUser user = authBLL.Login(username, password);

                if (user == null)
                {
                    MessageBox.Show(
                        Constants.MSG_LOGIN_FAILED,
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                    return;
                }

                MessageBox.Show(
                    Constants.MSG_LOGIN_SUCCESS,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Hide();

                using (FormMain formMain = new FormMain(user))
                {
                    DialogResult result = formMain.ShowDialog();

                    if (result == DialogResult.Retry)
                    {
                        txtPassword.Clear();
                        chkShowPassword.Checked = false;
                        Show();
                        txtPassword.Focus();
                        return;
                    }
                }

                Close();
            }
            catch (Exception ex)
            {
                if (!Visible)
                {
                    Show();
                }

                MessageBox.Show(
                    Constants.MSG_ERROR_DATABASE + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
