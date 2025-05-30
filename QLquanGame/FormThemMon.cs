using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QLQG.DAL;

namespace QLquanGame
{
    public partial class FormThemMon : Form
    {
        public FormThemMon()
        {
            InitializeComponent();
        }

        private void FormThemMon_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT MaLoai, TenLoai FROM LoaiMon", null, CommandType.Text);
                cbbLoaiMon.DataSource = dt;
                cbbLoaiMon.DisplayMember = "TenLoai";
                cbbLoaiMon.ValueMember = "MaLoai";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải loại món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacnhanthem_Click(object sender, EventArgs e)
        {
            string tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Tên món không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaBan.Text, out decimal gia) || gia < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaNhap.Text, out decimal giaNhap) || giaNhap < 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbbLoaiMon.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maLoai = Convert.ToInt32(cbbLoaiMon.SelectedValue);
            string anh = txtAnh.Text.Trim();

            try
            {
                using (SqlConnection conn = DataProvider.Instance.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ThemMon", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TenMon", tenMon);
                        cmd.Parameters.AddWithValue("@Gia", gia);
                        cmd.Parameters.AddWithValue("@GiaNhap", giaNhap);
                        cmd.Parameters.AddWithValue("@TonKho", 0);
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        cmd.Parameters.AddWithValue("@Anh", anh);
                        SqlParameter maMonParam = new SqlParameter("@MaMon", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(maMonParam);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Thêm món mới thành công! Mã món: {maMonParam.Value}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sourceImagePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(sourceImagePath);

                string resourceFolder = Path.Combine(Application.StartupPath, "Resources");
                if (!Directory.Exists(resourceFolder))
                {
                    Directory.CreateDirectory(resourceFolder);
                }

                string destPath = Path.Combine(resourceFolder, fileName);
                try
                {
                    if (!File.Exists(destPath))
                    {
                        File.Copy(sourceImagePath, destPath);
                    }
                    txtAnh.Text = fileName;
                    picMon.Image = Image.FromFile(destPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbbLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}