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
    public partial class SuaMon : Form
    {
        private int selectedMaMon = -1;
        private string currentImagePath = "";

        public SuaMon()
        {
            InitializeComponent();
            LoadLoaiMon();
            LoadDanhSachMon();
        }

        private void LoadLoaiMon()
        {
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT MaLoai, TenLoai FROM LoaiMon", null, CommandType.Text);
                cboLoaiMon.DataSource = dt;
                cboLoaiMon.DisplayMember = "TenLoai";
                cboLoaiMon.ValueMember = "MaLoai";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải loại món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachMon()
        {
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery(
                    "SELECT MaMon, TenMon, Gia AS GiaBan, GiaNhap, TonKho, MaLoai, Anh FROM Mon",
                    null,
                    CommandType.Text
                );
                dgvDanhSachMon.DataSource = dt;

                dgvDanhSachMon.Columns["MaMon"].HeaderText = "Mã món";
                dgvDanhSachMon.Columns["TenMon"].HeaderText = "Tên món";
                dgvDanhSachMon.Columns["GiaBan"].HeaderText = "Giá bán";
                dgvDanhSachMon.Columns["GiaNhap"].HeaderText = "Giá nhập";
                dgvDanhSachMon.Columns["TonKho"].HeaderText = "Tồn kho";
                dgvDanhSachMon.Columns["MaLoai"].HeaderText = "Mã loại";
                dgvDanhSachMon.Columns["Anh"].HeaderText = "Ảnh";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaMon == -1)
            {
                MessageBox.Show("Vui lòng chọn một món để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Tên món không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaBan.Text, out decimal giaBan) || giaBan < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaNhap.Text, out decimal giaNhap) || giaNhap < 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtTonKho.Text, out int tonKho) || tonKho < 0)
            {
                MessageBox.Show("Tồn kho không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maLoai = Convert.ToInt32(cboLoaiMon.SelectedValue);
            string anh = string.IsNullOrEmpty(currentImagePath) ? "" : currentImagePath;

            try
            {
                using (SqlConnection conn = DataProvider.Instance.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_CapNhatMon", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaMon", selectedMaMon);
                        cmd.Parameters.AddWithValue("@TenMon", tenMon);
                        cmd.Parameters.AddWithValue("@Gia", giaBan);
                        cmd.Parameters.AddWithValue("@GiaNhap", giaNhap);
                        cmd.Parameters.AddWithValue("@TonKho", tonKho);
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        cmd.Parameters.AddWithValue("@Anh", anh);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachMon();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearForm()
        {
            selectedMaMon = -1;
            txtTenMon.Text = "";
            txtGiaBan.Text = "";
            txtGiaNhap.Text = "";
            txtTonKho.Text = "";
            cboLoaiMon.SelectedIndex = -1;
            picAnh.Image = null;
            currentImagePath = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaMon == -1)
            {
                MessageBox.Show("Vui lòng chọn một món để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa món này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = DataProvider.Instance.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_XoaMon", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaMon", selectedMaMon);
                        SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(resultParam);

                        cmd.ExecuteNonQuery();
                        int result = Convert.ToInt32(resultParam.Value);

                        if (result == -1)
                        {
                            MessageBox.Show("Không thể xóa món vì món đang được sử dụng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (result > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachMon();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sourceImagePath = openFileDialog.FileName;
                string imageName = Path.GetFileName(sourceImagePath);

                string resourcesFolder = Path.Combine(Application.StartupPath, "Resources");
                if (!Directory.Exists(resourcesFolder))
                {
                    Directory.CreateDirectory(resourcesFolder);
                }

                string destinationImagePath = Path.Combine(resourcesFolder, imageName);
                try
                {
                    File.Copy(sourceImagePath, destinationImagePath, true);
                    currentImagePath = imageName;

                    if (picAnh.Image != null)
                    {
                        picAnh.Image.Dispose();
                        picAnh.Image = null;
                    }
                    picAnh.Image = Image.FromFile(destinationImagePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Lỗi khi sao chép file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDanhSachMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvDanhSachMon.Rows[e.RowIndex];
                    selectedMaMon = Convert.ToInt32(row.Cells["MaMon"].Value);
                    txtTenMon.Text = row.Cells["TenMon"].Value?.ToString() ?? "";
                    txtGiaBan.Text = row.Cells["GiaBan"].Value?.ToString() ?? "";
                    txtGiaNhap.Text = row.Cells["GiaNhap"].Value?.ToString() ?? "";
                    txtTonKho.Text = row.Cells["TonKho"].Value?.ToString() ?? "";
                    cboLoaiMon.SelectedValue = row.Cells["MaLoai"].Value;

                    if (!string.IsNullOrEmpty(row.Cells["Anh"].Value?.ToString()))
                    {
                        currentImagePath = row.Cells["Anh"].Value.ToString();
                        string resourcesFolder = Path.Combine(Application.StartupPath, "Resources");
                        string imagePath = Path.Combine(resourcesFolder, currentImagePath);
                        if (File.Exists(imagePath))
                        {
                            if (picAnh.Image != null)
                            {
                                picAnh.Image.Dispose();
                                picAnh.Image = null;
                            }
                            picAnh.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            picAnh.Image = null;
                            MessageBox.Show($"Không tìm thấy file ảnh: {imagePath}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        picAnh.Image = null;
                        currentImagePath = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}