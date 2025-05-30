using System;
using System.Windows.Forms;
using QLQG.DTO;

namespace QLquanGame
{
    public partial class MainForm : Form
    {
        private readonly LoginDTO currentUser;

        public MainForm(LoginDTO user)
        {
            InitializeComponent();
            currentUser = user;
            lblWelcome.Text = $"Xin chào {user.TenDangNhap}";
        }

        private void lblDangxuat_Click(object sender, EventArgs e)
        {
            // Hiển thị thông báo xác nhận đăng xuất
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Đóng MainForm và mở lại giao diện đăng nhập
                this.Close(); // Đóng MainForm
                frmLogin loginForm = new frmLogin(); // Tạo instance mới của form đăng nhập
                loginForm.Show(); // Hiển thị form đăng nhập
            }
        }

        private void lblDatmay_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmdatmay(currentUser.MaNV));
        }

        private void lblHoadon_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmHoadon());
        }

        private void lblDatmon_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new DatMon());
        }
        private void OpenFormInPanel(Form childForm)
        {
            panel2.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront(); // Đảm bảo form con hiện lên trên
            childForm.Show();
        }

        private void lblBaotri_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmBaotri(currentUser.MaNV));
        }

        private void lblTrangChu_Click(object sender, EventArgs e)
        {
                      // Xóa nội dung trong panel2 để trở về trang chủ ban đầu
            panel2.Controls.Clear();
        }
    
    }
}