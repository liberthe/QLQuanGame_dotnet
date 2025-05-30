using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using QLQG.DAL;
using QLQG.DTO;

namespace QLquanGame
{
    public partial class frmBaotri : Form
    {
        private int selectedMaMay = -1; // Lưu mã máy được chọn
        private int currentMaNV; // Lưu mã nhân viên đăng nhập

        public frmBaotri(int maNV)
        {
            InitializeComponent();
            currentMaNV = maNV;
            InitializeForm();
        }

        private void InitializeForm()
        {
            lvMaintain.CheckBoxes = true;
            lvMaintain.Columns.Add("Trạng thái bảo trì", 100); // Thêm cột trạng thái bảo trì
            lvMaintain.ItemChecked += lvMaintain_ItemChecked;
            LoadInitialData();
            LoadCurrentUserName();
            LoadNhaBaoTriComboBox();
        }

            private void LoadCurrentUserName()
        {
            try
            {
                string tenNV = NhanVienDAL.GetTenNhanVien(currentMaNV);
                txtNhanvien.Text = string.IsNullOrEmpty(tenNV) ? "Không tìm thấy nhân viên" : tenNV;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải tên nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInitialData()
        {
            LoadComputerStatus();
            LoadComputers();
            LoadBaotriComboBox();
        }

        private void LoadComputerStatus()
        {
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery(
                    "SELECT Trangthai, COUNT(*) as SoLuong FROM dbo.Maytinh GROUP BY Trangthai",
                    new (string, object)[] { }, // Truyền mảng tham số rỗng
                    CommandType.Text
                );
                int available = 0, usingCount = 0, errorCount = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string status = row["Trangthai"].ToString();
                    int count = Convert.ToInt32(row["SoLuong"]);
                    if (status == "Trong") available = count;
                    else if (status == "Dang thue") usingCount = count;
                    else if (status == "Bao loi") errorCount = count;
                }
                txtAvailable.Text = available.ToString();
                txtUsing.Text = usingCount.ToString();
                txtError.Text = errorCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải trạng thái máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComputers()
        {
            flpCom.Controls.Clear();
            try
            {
                List<MaytinhDTOo> computers = MaytinhDAL.GetAllComputers();
                foreach (var computer in computers)
                {
                    Button btn = new Button
                    {
                        Text = computer.TenMay,
                        Tag = computer.MaMay,
                        Size = new System.Drawing.Size(100, 50),
                        Font = new System.Drawing.Font("Calibri", 10)
                    };

                    if (computer.Trangthai == "Trong")
                        btn.BackColor = System.Drawing.Color.White;
                    else if (computer.Trangthai == "Dang thue")
                        btn.BackColor = System.Drawing.Color.MediumBlue;
                    else if (computer.Trangthai == "Bao loi")
                        btn.BackColor = System.Drawing.Color.Crimson;
                    else
                        btn.BackColor = System.Drawing.Color.Gray;

                    btn.Click += (s, e) =>
                    {
                        selectedMaMay = (int)btn.Tag;
                        UpdateMachineInfo(computer.TenMay, computer.Trangthai);
                        LoadMaintainListForSelectedMachine();
                        UpdateTotalCost();
                        cbBaotri.SelectedIndex = -1;
                        txtDetail.Clear();
                    };

                    flpCom.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateMachineInfo(string tenMay, string trangthai)
        {
            gbMay.Text = $"Máy: {selectedMaMay} - {tenMay}";
            txtTT.Text = trangthai;
        }

        private void LoadBaotriComboBox()
        {
            try
            {
                List<TinhtrangDTO> tinhtrangs = TinhtrangDAO.GetAllTinhtrang();
                foreach (var tinhtrang in tinhtrangs)
                {
                    cbBaotri.Items.Add(new ComboBoxItem(tinhtrang.TenTT, tinhtrang.MaTT));
                }
                cbBaotri.DisplayMember = "Text";
                cbBaotri.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách tình trạng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMaintainListForSelectedMachine()
        {
            lvMaintain.Items.Clear();
            if (selectedMaMay == -1) return;

            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery(
                    "SELECT b.MaBT, b.GhiChu, b.Trangthai AS TrangthaiBaotri, t.TenTT, g.ChiPhi " +
                    "FROM dbo.Baotri b " +
                    "JOIN dbo.ChitietBT c ON b.MaBT = c.MaBT " +
                    "JOIN dbo.Tinhtrang t ON c.MaTT = t.MaTT " +
                    "JOIN dbo.Giaiphap g ON c.MaGP = g.MaGP " +
                    "WHERE c.MaMay = @MaMay",
                    new (string, object)[] { ("@MaMay", selectedMaMay) });

                foreach (DataRow row in dt.Rows)
                {
                    int maBT = Convert.ToInt32(row["MaBT"]);
                    string tenTT = row["TenTT"].ToString();
                    string ghiChu = row["GhiChu"].ToString();
                    decimal chiPhi = row["ChiPhi"] != DBNull.Value ? Convert.ToDecimal(row["ChiPhi"]) : 0;
                    string trangThai = row["TrangthaiBaotri"].ToString();

                    ListViewItem item = new ListViewItem(tenTT); // Cột "Bộ phận"
                    item.SubItems.Add(ghiChu); // Cột "Mô tả"
                    item.SubItems.Add(chiPhi.ToString("N0")); // Cột "Chi phí"
                    item.SubItems.Add(trangThai); // Cột "Trạng thái bảo trì"
                    item.Tag = maBT;

                    if (trangThai == "Đã hoàn tất")
                    {
                        item.Checked = false;
                        item.BackColor = System.Drawing.Color.LightGray;
                    }
                    else
                    {
                        item.Checked = true;
                    }

                    lvMaintain.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách bảo trì: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UpdateTotalCost();
        }
        private void LoadNhaBaoTriComboBox()
        {
            try
            {
                // Giả định có lớp NhaBaoTriDAL để lấy danh sách nhà bảo trì
                List<NhaBaoTriDTO> nhaBaoTris = NhaBaoTriDAL.GetAllNhaBaoTri();
                cbNhaBT.Items.Clear();
                foreach (var nhaBaoTri in nhaBaoTris)
                {
                    cbNhaBT.Items.Add(new ComboBoxItem(nhaBaoTri.TenNBT, nhaBaoTri.MaNBT));
                }
                cbNhaBT.DisplayMember = "Text";
                cbNhaBT.ValueMember = "Value";
                cbNhaBT.SelectedIndex = -1; // Không chọn mặc định
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhà bảo trì: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvMaintain_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.SubItems[3].Text == "Đã hoàn tất")
            {
                e.Item.Checked = false;
            }
            UpdateTotalCost();
        }

        private void UpdateTotalCost()
        {
            decimal totalCost = 0;
            foreach (ListViewItem item in lvMaintain.CheckedItems)
            {
                if (item.SubItems.Count > 2)
                {
                    if (decimal.TryParse(item.SubItems[2].Text.Replace(".", ""), out decimal chiPhi))
                    {
                        totalCost += chiPhi;
                    }
                }
            }
            txtTotal.Text = totalCost.ToString("N0");
        }

        private void btnBaoloi_Click(object sender, EventArgs e)
        {
            if (selectedMaMay == -1)
            {
                MessageBox.Show("Vui lòng chọn một máy để báo lỗi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbBaotri.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tình trạng máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbNhaBT.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhà bảo trì!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangthai = MaytinhDAL.GetTrangthai(selectedMaMay);
            if (trangthai == "Dang thue")
            {
                MessageBox.Show("Máy đang sử dụng, không thể báo lỗi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = DataProvider.Instance.ExecuteQuery(
                "SELECT b.MaBT FROM dbo.Baotri b " +
                "JOIN dbo.ChitietBT c ON b.MaBT = c.MaBT " +
                "WHERE c.MaMay = @MaMay AND b.Trangthai = N'Chưa thanh toán'",
                new (string, object)[] { ("@MaMay", selectedMaMay) });
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Máy đang có bảo trì chưa hoàn tất, không thể báo lỗi mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTT = ((ComboBoxItem)cbBaotri.SelectedItem).Value;
            int maGP = GiaiphapDAL.GetMaGPFromMaTT(maTT);
            int maNN = maTT;
            string ghiChu = txtDetail.Text;
            decimal chiPhi = GiaiphapDAL.GetChiPhi(maGP);
            string giaiPhap = GiaiphapDAL.GetTenGP(maGP);
            int maNBT = ((ComboBoxItem)cbNhaBT.SelectedItem).Value; // Lấy MaNBT từ ComboBox

            if (!string.IsNullOrEmpty(ghiChu))
                ghiChu += Environment.NewLine + giaiPhap;
            else
                ghiChu = giaiPhap;

            try
            {
                int maBT = BaotriDAL.CreateBaotri(currentMaNV, selectedMaMay, maTT, maNN, maGP, ghiChu, chiPhi, maNBT);
                MessageBox.Show("Báo lỗi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadComputerStatus();
                LoadComputers();
                LoadMaintainListForSelectedMachine();
                txtDetail.Clear();
                cbBaotri.SelectedIndex = -1;
                cbNhaBT.SelectedIndex = -1; // Reset ComboBox nhà bảo trì
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi báo lỗi: {ex.Message}\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            List<int> maBTList = new List<int>();
            foreach (ListViewItem item in lvMaintain.CheckedItems)
            {
                if (item.SubItems[3].Text != "Đã hoàn tất")
                {
                    maBTList.Add((int)item.Tag);
                }
            }

            if (maBTList.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một lỗi để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BaotriDAL.ThanhToanBaotri(currentMaNV, maBTList, out int maHD);
                decimal totalCost = 0;
                foreach (int maBT in maBTList)
                {
                    DataTable dt = DataProvider.Instance.ExecuteQuery(
                        "SELECT g.ChiPhi FROM ChitietBT c JOIN Giaiphap g ON c.MaGP = g.MaGP WHERE c.MaBT = @MaBT",
                        new (string, object)[] { ("@MaBT", maBT) });
                    if (dt.Rows.Count > 0)
                    {
                        totalCost += Convert.ToDecimal(dt.Rows[0]["ChiPhi"]);
                    }
                }

                MessageBox.Show($"Thanh toán hoàn tất, tổng chi phí: {totalCost.ToString("N0")} VNĐ. Mã hóa đơn: {maHD}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadComputerStatus();
                LoadComputers();
                LoadMaintainListForSelectedMachine();
                selectedMaMay = -1;
                gbMay.Text = "Thông tin máy";
                txtTT.Text = "";
                txtDetail.Clear();
                cbBaotri.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public ComboBoxItem(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void cbNhaBT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}