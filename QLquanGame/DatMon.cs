using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QLQG.DAL;
using QLQG.DTO;
using Microsoft.VisualBasic;

namespace QLquanGame
{
    public partial class DatMon: Form
    {
        private int maNV;
    private int maMay;
        private int maPhong;
    private DataTable orderTable = new DataTable();
    private Dictionary<int, int> maHoaDonTheoMay = new Dictionary<int, int>();

    public DatMon()
    {
        InitializeComponent();
        InitializeDataGridView();
        LoadZones();
        LoadMenu();
    }

    public DatMon(int maNV, int maMay)
    {
        InitializeComponent();
        this.maNV = maNV;
        this.maMay = maMay;
        InitializeDataGridView();
        LoadZones();
        LoadMenu();
    }

    public DatMon(int maNV)
    {
        InitializeComponent();
        this.maNV = maNV;
        this.maMay = 0;
        InitializeDataGridView();
        LoadZones();
        LoadMenu();
    }

    private void DatMon_Load(object sender, EventArgs e)
    {
        LoadDanhSachMonTheoLoai(3);
        cboZone.SelectedIndex = -1;
        cboChonmay.SelectedIndex = -1;
        orderTable.Rows.Clear();
        UpdateTotal();
        TaiDanhMucMon();

        cboZone.SelectedIndexChanged += cboZone_SelectedIndexChanged;
        cboChonmay.SelectedIndexChanged += cboChonmay_SelectedIndexChanged;
    }

    private int? GetCurrentMaHD(int maMay, int maPhong)
    {
        try
        {
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetMaHDFromThuemay", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaMay", maMay);
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    SqlParameter outputMaHD = new SqlParameter("@MaHD", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outputMaHD);

                    cmd.ExecuteNonQuery();
                    return outputMaHD.Value != null && outputMaHD.Value != DBNull.Value ? (int?)Convert.ToInt32(outputMaHD.Value) : null;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi trong GetCurrentMaHD: {ex.Message}");
            return null;
        }
    }
        public void AddMonToGrid(int maMon, string tenMon, decimal gia)
        {
            try
            {
                // Thoát ký tự đặc biệt để tránh lỗi
                DataRow[] rows = orderTable.Select($"TenMon = '{tenMon.Replace("'", "''")}'");
                if (rows.Length > 0)
                {
                    rows[0]["SoLuong"] = Convert.ToInt32(rows[0]["SoLuong"]) + 1;
                    rows[0]["ThanhTien"] = Convert.ToDecimal(rows[0]["SoLuong"]) * gia;
                }
                else
                {
                    DataRow newRow = orderTable.NewRow();
                    newRow["STT"] = orderTable.Rows.Count + 1;
                    newRow["TenMon"] = tenMon;
                    newRow["SoLuong"] = 1;
                    newRow["DonGia"] = gia;
                    newRow["ThanhTien"] = gia;
                    orderTable.Rows.Add(newRow);
                }

                // Cập nhật lại STT
                for (int i = 0; i < orderTable.Rows.Count; i++)
                {
                    orderTable.Rows[i]["STT"] = i + 1;
                }

                // Làm mới DataGridView
                dgvGioHang.DataSource = null;
                dgvGioHang.DataSource = orderTable;
                dgvGioHang.Refresh();
                dgvGioHang.Update();

                UpdateTotal();
                Console.WriteLine($"AddMonToGrid: Đã thêm món {tenMon} (MaMon: {maMon}) vào orderTable");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm món vào giỏ hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Lỗi trong AddMonToGrid: {ex.Message}\n{ex.StackTrace}");
            }
        }
        private void UcMon_Click(object sender, EventArgs e)
        {
            // Kiểm tra đã chọn phòng và máy chưa
            if (cboZone.SelectedIndex == -1 || cboChonmay.SelectedIndex == -1 || cboChonmay.SelectedValue == null || cboZone.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn máy và phòng trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine("UcMon_Click: Chưa chọn phòng hoặc máy");
                return;
            }

            ucMon uc = sender as ucMon;
            if (uc == null)
            {
                Console.WriteLine("UcMon_Click: ucMon không hợp lệ");
                return;
            }

            int maMon = uc.MaMon;
            string tenMon = uc.TenMon;
            decimal gia = uc.Gia;

            int maMay, maPhong;
            try
            {
                maMay = Convert.ToInt32(cboChonmay.SelectedValue);
                maPhong = Convert.ToInt32(cboZone.SelectedValue);
                Console.WriteLine($"UcMon_Click: Đã chọn máy MaMay = {maMay}, phòng MaPhong = {maPhong}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Máy hoặc phòng không hợp lệ, vui lòng chọn lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"UcMon_Click: Lỗi chuyển đổi cboChonmay.SelectedValue hoặc cboZone.SelectedValue: {ex.Message}");
                return;
            }

            try
            {
                // Kiểm tra hóa đơn hiện tại từ Thuemay
                if (!maHoaDonTheoMay.ContainsKey(maMay))
                {
                    string query = @"
                SELECT TOP 1 t.MaHD
                FROM Thuemay t
                JOIN Maytinh m ON t.MaMay = m.MaMay
                WHERE t.MaMay = @maMay
                AND m.MaPhong = @maPhong
                AND m.Trangthai = 'Dang thue'
                AND t.GioKT IS NULL
                ORDER BY t.GioBD DESC";
                    object result = DataProvider.Instance.ExecuteScalar(query, new[] { ("@maMay", (object)maMay), ("@maPhong", (object)maPhong) });
                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show("Máy này không đang thuê hoặc thông tin phòng/máy không khớp, không thể gọi món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Console.WriteLine($"UcMon_Click: Không tìm thấy MaHD cho MaMay = {maMay}, MaPhong = {maPhong}");
                        return;
                    }
                    maHoaDonTheoMay[maMay] = Convert.ToInt32(result);
                }

                // Gọi AddMonToGrid để thêm món
                AddMonToGrid(maMon, tenMon, gia);
                Console.WriteLine($"UcMon_Click: Đã gọi AddMonToGrid cho món {tenMon} (MaMon: {maMon})");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Lỗi trong UcMon_Click: {ex.Message}\n{ex.StackTrace}");
            }
        }


        private void LoadDanhSachMonTheoLoai(int maLoai)
    {
        try
        {
            List<Mon> dsMon = MonDAO.Instance.LayTatCaMon();
            var dsTheoLoai = dsMon.Where(m => m.MaLoai == maLoai).ToList();

            flpMonAn.Controls.Clear();
            foreach (var mon in dsTheoLoai)
            {
                ucMon item = new ucMon(mon.MaMon, mon.TenMon, mon.Gia, mon.HinhAnh);
                item.Click += UcMon_Click;
                flpMonAn.Controls.Add(item);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi tải danh sách món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadMenu()
    {
        try
        {
            List<Mon> monList = MonDAO.Instance.LayTatCaMon();
            flpMonAn.Controls.Clear();

            foreach (var mon in monList)
            {
                ucMon item = new ucMon(mon.MaMon, mon.TenMon, mon.Gia, mon.HinhAnh);
                item.Click += UcMon_Click;
                flpMonAn.Controls.Add(item);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi tải menu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadZones()
    {
        try
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT MaPhong, TenPhong FROM Phong", null, CommandType.Text);
            cboZone.DataSource = dt;
            cboZone.DisplayMember = "TenPhong";
            cboZone.ValueMember = "MaPhong";
            cboZone.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi tải danh sách phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

        private void LoadMachines(int maPhong)
        {
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT MaMay, TenMay FROM Maytinh WHERE MaPhong = @maPhong", new[] { ("@maPhong", (object)maPhong) });
                cboChonmay.DataSource = null;
                cboChonmay.Items.Clear();
                cboChonmay.DataSource = dt;
                cboChonmay.DisplayMember = "TenMay";
                cboChonmay.ValueMember = "MaMay";
                cboChonmay.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboChonmay.DataSource = null;
                cboChonmay.Items.Clear();
            }
        }

        private void InitializeDataGridView()
        {
            orderTable = new DataTable();
            orderTable.Columns.Add("STT", typeof(int));
            orderTable.Columns.Add("TenMon", typeof(string));
            orderTable.Columns.Add("SoLuong", typeof(int));
            orderTable.Columns.Add("DonGia", typeof(decimal));
            orderTable.Columns.Add("ThanhTien", typeof(decimal));

            dgvGioHang.DataSource = orderTable;
            dgvGioHang.ReadOnly = true;
            dgvGioHang.AllowUserToAddRows = false;
            dgvGioHang.AllowUserToDeleteRows = false;
            dgvGioHang.AutoGenerateColumns = true;

            if (dgvGioHang.Columns.Count > 0)
            {
                dgvGioHang.Columns["STT"].HeaderText = "STT";
                dgvGioHang.Columns["TenMon"].HeaderText = "Tên Món";
                dgvGioHang.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvGioHang.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvGioHang.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            }
        }

    private void UpdateTotal()
    {
        decimal total = 0;
        foreach (DataRow row in orderTable.Rows)
        {
            total += Convert.ToDecimal(row["ThanhTien"]);
        }
        lblTongtien.Text = $"Tổng tiền: {total:N0}";
        lblthanhtien.Text = total.ToString("N0");
    }

        private void cboZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboZone.SelectedIndex != -1 && cboZone.SelectedValue != null)
            {
                try
                {
                    DataRowView zoneRowView = cboZone.SelectedValue as DataRowView;
                    if (cboZone.SelectedItem is DataRowView drvPhong)
                        maPhong = Convert.ToInt32(drvPhong["MaPhong"]);
                    else
                        maPhong = -1;



                    if (maPhong == -1)
                    {
                        cboChonmay.DataSource = null;
                        cboChonmay.Items.Clear();
                        orderTable.Clear();
                        UpdateTotal();
                        return;
                    }

                    // Gọi LoadMachines với maPhong
                    LoadMachines(maPhong);
                    cboChonmay.SelectedIndex = -1;
                    orderTable.Clear();
                    UpdateTotal();
                    maHoaDonTheoMay.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboChonmay.DataSource = null;
                    cboChonmay.Items.Clear();
                    orderTable.Clear();
                }
            }
        }
        private void cboChonmay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboChonmay.SelectedIndex == -1 || cboChonmay.SelectedValue == null || cboZone.SelectedIndex == -1)
                {
                    orderTable.Rows.Clear();
                    UpdateTotal();
                    return;
                }

                // Lấy DataRowView được chọn và trích xuất MaMay
                DataRowView mayRowView = cboChonmay.SelectedValue as DataRowView;
                if (cboChonmay.SelectedItem is DataRowView drvMay)
                    maMay = Convert.ToInt32(drvMay["MaMay"]);
                else
                    maMay = -1;



                // Lấy DataRowView được chọn và trích xuất MaPhong
                DataRowView zoneRowView = cboZone.SelectedValue as DataRowView;
                if (cboZone.SelectedItem is DataRowView drvPhong)
                    maPhong = Convert.ToInt32(drvPhong["MaPhong"]);
                else
                    maPhong = -1;



                if (maMay == -1 || maPhong == -1)
                {
                    orderTable.Rows.Clear();
                    UpdateTotal();
                    return;
                }

                int? maHD = GetCurrentMaHD(maMay, maPhong);
                if (maHD.HasValue)
                {
                    maHoaDonTheoMay[maMay] = maHD.Value;
                    LoadGoiMonTheoHoaDon(maHD.Value);
                }
                else
                {
                    orderTable.Rows.Clear();
                    UpdateTotal();
                    maHoaDonTheoMay.Remove(maMay);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGoiMonTheoHoaDon(int maHD)
        {
            try
            {
                if (orderTable == null)
                {
                    orderTable = new DataTable();
                    orderTable.Columns.Add("STT", typeof(int));
                    orderTable.Columns.Add("TenMon", typeof(string));
                    orderTable.Columns.Add("SoLuong", typeof(int));
                    orderTable.Columns.Add("DonGia", typeof(decimal));
                    orderTable.Columns.Add("ThanhTien", typeof(decimal));
                }

                orderTable.Rows.Clear();

                if (maHD <= 0)
                {
                    UpdateTotal();
                    dgvGioHang.DataSource = orderTable;
                    return;
                }

                DataTable dt = GoiMonDAO.Instance.GetGoiMonByMaHD(maHD);
                int stt = 1;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = orderTable.NewRow();
                    newRow["STT"] = stt++;
                    newRow["TenMon"] = row["TenMon"]?.ToString() ?? "";
                    newRow["SoLuong"] = Convert.ToInt32(row["SoLuong"]);
                    newRow["DonGia"] = Convert.ToDecimal(row["Gia"]);
                    newRow["ThanhTien"] = Convert.ToDecimal(row["ThanhTien"]);
                    orderTable.Rows.Add(newRow);
                }

                dgvGioHang.DataSource = null;
                dgvGioHang.Columns.Clear();
                dgvGioHang.AutoGenerateColumns = true;
                dgvGioHang.DataSource = orderTable;

                if (dgvGioHang.Columns.Count > 0)
                {
                    dgvGioHang.Columns["STT"].HeaderText = "STT";
                    dgvGioHang.Columns["TenMon"].HeaderText = "Tên Món";
                    dgvGioHang.Columns["SoLuong"].HeaderText = "Số Lượng";
                    dgvGioHang.Columns["DonGia"].HeaderText = "Đơn Giá";
                    dgvGioHang.Columns["ThanhTien"].HeaderText = "Thành Tiền";

                    dgvGioHang.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                    dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                }

                dgvGioHang.Refresh();
                dgvGioHang.Update();
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    private void btnSua_Click_1(object sender, EventArgs e)
    {
        SuaMon suaMonForm = new SuaMon();
        if (suaMonForm.ShowDialog() == DialogResult.OK)
        {
            LoadMenu();
        }
    }

    private void btnThemmon_Click_1(object sender, EventArgs e)
    {
        FormThemMon form = new FormThemMon();
        if (form.ShowDialog() == DialogResult.OK)
        {
            LoadMenu();
        }
    }

        private void btnThemloai_Click(object sender, EventArgs e)
        {
            string tenLoai = Interaction.InputBox("Nhập tên loại món mới:", "Thêm loại món", "");
            if (string.IsNullOrWhiteSpace(tenLoai))
            {
                MessageBox.Show("Tên loại không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int count = (int)DataProvider.Instance.ExecuteScalar(
                    "SELECT COUNT(*) FROM LoaiMon WHERE TenLoai = @tenLoai",
                    new[] { ("@tenLoai", (object)tenLoai) });

                if (count > 0)
                {
                    MessageBox.Show("Tên loại đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // KHÔNG cần tạo MaLoai nữa
                int rowsAffected = DataProvider.Instance.ExecuteNonQuery(
                    "INSERT INTO LoaiMon (TenLoai) VALUES (@tenLoai)",
                    new[] { ("@tenLoai", (object)tenLoai) });

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhMucMon(); // Cập nhật lại giao diện loại món
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm loại món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoamon_Click(object sender, EventArgs e)
    {
        if (dgvGioHang.CurrentRow != null)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa món này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvGioHang.Rows.RemoveAt(dgvGioHang.CurrentRow.Index);
                UpdateTotal();
            }
        }
        else
        {
            MessageBox.Show("Vui lòng chọn một món để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        orderTable.Clear();
        UpdateTotal();
    }

        private void btnXacnhan_Click(object sender, EventArgs e)
        {
            if (cboChonmay.SelectedIndex == -1 || cboZone.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn máy và phòng trước khi xác nhận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMay = Convert.ToInt32(cboChonmay.SelectedValue);
            int maPhong = Convert.ToInt32(cboZone.SelectedValue);

            try
            {
                int? maHD = GetCurrentMaHD(maMay, maPhong);
                if (!maHD.HasValue)
                {
                    MessageBox.Show("Không có hóa đơn thuê máy nào cho máy và phòng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // XÓA CÁC MÓN CŨ TRƯỚC
                DataProvider.Instance.ExecuteNonQuery("DELETE FROM GoiMon WHERE MaHD = @maHD", new[] { ("@maHD", (object)maHD.Value) });

                foreach (DataRow row in orderTable.Rows)
                {
                    string tenMon = row["TenMon"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);

                    int maMon = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT MaMon FROM Mon WHERE TenMon = @tenMon", new[] { ("@tenMon", (object)tenMon) }));
                    if (maMon == 0)
                    {
                        MessageBox.Show($"Không tìm thấy món {tenMon} trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int tonKho = (int)DataProvider.Instance.ExecuteScalar("SELECT TonKho FROM Mon WHERE MaMon = @MaMon", new[] { ("@MaMon", (object)maMon) });
                    if (soLuong > tonKho)
                    {
                        MessageBox.Show($"Không đủ tồn kho cho món {tenMon}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlConnection conn = DataProvider.Instance.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_XacNhanGoiMon", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MaHD", maHD.Value);
                            cmd.Parameters.AddWithValue("@MaMon", maMon);
                            cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                            SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                            cmd.Parameters.Add(resultParam);

                            cmd.ExecuteNonQuery();
                            if (Convert.ToInt32(resultParam.Value) != 1)
                            {
                                MessageBox.Show($"Lỗi khi thêm món {tenMon} vào hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Cập nhật tồn kho
                            DataProvider.Instance.ExecuteNonQuery("UPDATE Mon SET TonKho = TonKho - @SoLuong WHERE MaMon = @MaMon",
                                new[] { ("@SoLuong", soLuong), ("@MaMon", (object)maMon) });
                        }
                    }
                }

                decimal tongTien = GoiMonDAO.Instance.CalculateTotalByMaHD(maHD.Value);

                // Cập nhật tổng tiền vào bảng hóa đơn
                DataProvider.Instance.ExecuteNonQuery(
                    "UPDATE HoaDon SET TongTien = @TongTien WHERE MaHD = @MaHD",
                    new[] { ("@TongTien", tongTien), ("@MaHD", (object)maHD.Value) }
                );

                // Làm mới lại hiển thị
                LoadGoiMonTheoHoaDon(maHD.Value);

                MessageBox.Show($"Xác nhận hóa đơn {maHD.Value} thành công! Tổng tiền: {tongTien:N0} đ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xác nhận hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtTimkiem_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string keyword = txtTimkiem.Text.Trim();
            flpMonAn.Controls.Clear();
            List<Mon> dsMon = MonDAO.Instance.TimKiemMon(keyword);
            foreach (var mon in dsMon)
            {
                ucMon item = new ucMon(mon.MaMon, mon.TenMon, mon.Gia, mon.HinhAnh);
                item.Click += UcMon_Click;
                flpMonAn.Controls.Add(item);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi tìm kiếm món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void TaiDanhMucMon()
    {
        flowDanhMuc.Controls.Clear();
        try
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT MaLoai, TenLoai FROM LoaiMon", null, CommandType.Text);
            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button
                {
                    Text = row["TenLoai"].ToString(),
                    Tag = row["MaLoai"],
                    AutoSize = true,
                    Margin = new Padding(5),
                    BackColor = Color.LightBlue
                };
                btn.Click += BtnLoaiMon_Click;
                flowDanhMuc.Controls.Add(btn);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi tải danh mục món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnLoaiMon_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn == null) return;

        try
        {
            int maLoai = Convert.ToInt32(btn.Tag);
            LoadDanhSachMonTheoLoai(maLoai);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi lọc món theo loại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
}