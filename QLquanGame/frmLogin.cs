using System;
using System.Windows.Forms;
using QLQG.DAL;
using QLQG.DTO;

namespace QLquanGame
{
    public partial class frmLogin : Form
    {
        private readonly LoginDAL loginDAL = new LoginDAL();

        public frmLogin()
        {
            InitializeComponent();
            this.Text = "Đăng nhập - Quản lý Quán Game";
            lblError.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string TenDangNhap = txtUsername.Text.Trim();
            string MatKhau = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(TenDangNhap) || string.IsNullOrEmpty(MatKhau))
            {
                lblError.Text = "Vui lòng nhập đầy đủ thông tin!";
                lblError.Visible = true;
                return;
            }

            try
            {
                LoginDTO user = loginDAL.AuthenticateUser(TenDangNhap, MatKhau);
                if (user != null)
                {
                    MainForm mainForm = new MainForm(user); // Truyền LoginDTO vào
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    lblError.Text = "Sai tên đăng nhập hoặc mật khẩu!";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Lỗi đăng nhập: {ex.Message}";
                lblError.Visible = true;
                Console.WriteLine($"Lỗi trong btnLogin_Click: {ex.Message}\n{ex.StackTrace}");
            }
        }
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Thoát toàn bộ ứng dụng
        }

    }
}