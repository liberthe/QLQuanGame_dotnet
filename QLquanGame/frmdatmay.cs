using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLQG.DAL;
using QLQG.DTO;

namespace QLquanGame
{
    public partial class frmdatmay : Form
    {
        private PhongDAL phongDAL = new PhongDAL();
        private MayTinh_DAL mayTinhDAL = new MayTinh_DAL();
        private ThueMayDAL thueMayDAL = new ThueMayDAL();
        private BillingDAL billingDAL = BillingDAL.Instance;
        private List<PhongDTO> phongList;
        private int selectedRoomId;
        private List<MayTinhDTO> currentMachines;
        private MayTinhDTO selectedMachine;
        private readonly int currentMaNV;

        public frmdatmay(int maNV)
        {
            InitializeComponent();
            currentMaNV = maNV;
            // Kiểm tra và sửa dữ liệu không nhất quán
            try
            {
                DataProvider.Instance.ExecuteNonQuery("sp_FixInconsistentMachineStatus", null, CommandType.StoredProcedure);
                Console.WriteLine("Đã kiểm tra và sửa dữ liệu không nhất quán.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi chạy sp_FixInconsistentMachineStatus: {ex.Message}\n{ex.StackTrace}");
            }
            LoadRooms();
            LoadNhanVien();
        }

        private void LoadRooms()
        {
            try
            {
                phongList = phongDAL.GetAllPhong();
                if (phongList == null || phongList.Count < 2)
                {
                    MessageBox.Show("Không tìm thấy đủ phòng để hiển thị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnZone1.Tag = phongList[0].MaPhong;
                btnZone1.Text = phongList[0].TenPhong;
                btnZone1.Click += RoomButton_Click;

                btnZone2.Tag = phongList[1].MaPhong;
                btnZone2.Text = phongList[1].TenPhong;
                btnZone2.Click += RoomButton_Click;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhanVien()
        {
            try
            {
                // Giả sử có NhanVienDAL
                txtNhanvien.Text = $"NV{currentMaNV}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RoomButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                if (clickedButton?.Tag == null)
                {
                    MessageBox.Show("Nút phòng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                selectedRoomId = Convert.ToInt32(clickedButton.Tag);

                currentMachines = mayTinhDAL.GetMayTinhByPhong(selectedRoomId) ?? new List<MayTinhDTO>();
                DisplayMachines();

                PhongDTO selectedPhong = phongList.Find(p => p.MaPhong == selectedRoomId);
                txtPrice.Text = selectedPhong != null ? $"{selectedPhong.Giatheogio:N0} VNĐ/giờ" : "0 VNĐ/giờ";

                int offlineCount = currentMachines.Count(m => m.Trangthai.Trim().Equals("Trong", StringComparison.OrdinalIgnoreCase));
                int onlineCount = currentMachines.Count(m => m.Trangthai.Trim().Equals("Dang thue", StringComparison.OrdinalIgnoreCase));
                txtAvailable.Text = offlineCount.ToString();
                txtUsing.Text = onlineCount.ToString();


                ClearMachineDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayMachines()
        {
            if (flpCom == null)
            {
                MessageBox.Show("Lỗi: flpCom không được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            flpCom.Controls.Clear();
            foreach (var mayTinh in currentMachines ?? Enumerable.Empty<MayTinhDTO>())
            {
                var machineButton = new Button
                {
                    Text = mayTinh.TenMay,
                    Tag = mayTinh,
                    Width = 100,
                    Height = 50,
                    BackColor = mayTinh.Trangthai == "Trong" ? Color.Cyan : mayTinh.Trangthai == "Dang thue" ? Color.Crimson : Color.Gray,
                    ForeColor = Color.White,
                    Font = new Font("Calibri", 10F),
                    FlatStyle = FlatStyle.Flat
                };
                machineButton.Click += MachineButton_Click;
                flpCom.Controls.Add(machineButton);
            }
        }

        private void MachineButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                if (clickedButton?.Tag == null)
                {
                    MessageBox.Show("Nút máy không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                selectedMachine = (MayTinhDTO)clickedButton.Tag;
                DisplayMachineDetails(selectedMachine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayMachineDetails(MayTinhDTO mayTinh)
        {
            try
            {
                if (mayTinh == null)
                {
                    ClearMachineDetails();
                    return;
                }

                txtTT.Text = mayTinh.Trangthai == "Trong" ? "Offline" : "Online";
                txtOcung.Text = mayTinh.TenOcung;
                txtDL.Text = mayTinh.TenDL;
                txtRam.Text = mayTinh.TenRAM;
                txtOdia.Text = mayTinh.TenOdia;
                txtManhinh.Text = mayTinh.TenMH;
                txtChuot.Text = mayTinh.TenChuot;
                txtBP.Text = mayTinh.TenBP;

                if (mayTinh.Trangthai == "Dang thue")
                {
                    ThueMayDTO thueMay = thueMayDAL.GetCurrentThueMay(mayTinh.MaMay);
                    if (thueMay != null)
                    {
                        tpStime.Value = thueMay.GioBD;
                        TimeSpan duration = DateTime.Now - thueMay.GioBD;
                        txtTime.Text = $"{duration.Hours}h {duration.Minutes}m";

                        PhongDTO phong = phongList.Find(p => p.MaPhong == mayTinh.MaPhong);
                        txtTamtinh.Text = phong != null ? $"{((decimal)duration.TotalHours * phong.Giatheogio):N0} VNĐ" : "0 VNĐ";
                    }
                    else
                    {
                        tpStime.Value = DateTime.Now;
                        txtTime.Text = "0h 0m";
                        txtTamtinh.Text = "0 VNĐ";
                    }
                }
                else
                {
                    tpStime.Value = DateTime.Now;
                    txtTime.Text = "0h 0m";
                    txtTamtinh.Text = "0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị chi tiết máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearMachineDetails()
        {
            selectedMachine = null;
            txtTT.Text = "";
            txtTime.Text = "";
            txtTamtinh.Text = "";
            tpStime.Value = DateTime.Now;
            txtOcung.Text = "";
            txtDL.Text = "";
            txtRam.Text = "";
            txtOdia.Text = "";
            txtManhinh.Text = "";
            txtChuot.Text = "";
            txtBP.Text = "";
        }

        private void btnBatmay_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedMachine == null)
                {
                    MessageBox.Show("Vui lòng chọn một máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (selectedMachine.Trangthai == "Dang thue")
                {
                    MessageBox.Show("Máy đang được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (selectedMachine.Trangthai == "Bao loi")
                {
                    MessageBox.Show("Máy đang được bảo trì, không thể mở!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maHD = billingDAL.CreateHoaDon(currentMaNV);
                thueMayDAL.StartThueMay(selectedMachine.MaMay, maHD);
                mayTinhDAL.UpdateTrangThaiMay(selectedMachine.MaMay, "Dang thue");

                RoomButton_Click(GetCurrentRoomButton(), null);
                MessageBox.Show("Bật máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi bật máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTatmay_Click(object sender, EventArgs e)
        {
            ThueMayDTO thueMay = thueMayDAL.GetCurrentThueMay(selectedMachine.MaMay);
            try
            {
                if (selectedMachine == null) { MessageBox.Show("Vui lòng chọn một máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                if (selectedMachine.Trangthai != "Dang thue")
                {
                    MessageBox.Show("Máy không đang được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

              
                if (thueMay == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin thuê máy. Vui lòng kiểm tra dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                thueMayDAL.EndThueMay(thueMay.MaTM); // sp_EndThueMay đã cập nhật trạng thái máy

                PhongDTO phong = phongList.Find(p => p.MaPhong == selectedMachine.MaPhong);
                if (phong != null)
                {
                    TimeSpan duration = DateTime.Now - thueMay.GioBD;
                    decimal tongTien = (decimal)duration.TotalHours * phong.Giatheogio;
                    billingDAL.UpdateTongTien(thueMay.MaHD, tongTien);
                }

                RoomButton_Click(GetCurrentRoomButton(), null);
                MessageBox.Show("Tắt máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tắt máy (MaTM = {thueMay?.MaTM}): {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnBaoloi_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedMachine == null)
                {
                    MessageBox.Show("Vui lòng chọn một máy để báo lỗi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (selectedMachine.Trangthai == "Dang thue")
                {
                    MessageBox.Show("Máy đang được sử dụng, không thể báo lỗi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var frmBaotri = new frmBaotri(currentMaNV);
                frmBaotri.FormClosed += (s, args) => RoomButton_Click(GetCurrentRoomButton(), null);
                frmBaotri.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở báo lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Button GetCurrentRoomButton()
        {
            if (btnZone1 == null || btnZone2 == null)
            {
                throw new InvalidOperationException("Nút phòng không được khởi tạo!");
            }
            return selectedRoomId == (int)btnZone1.Tag ? btnZone1 : btnZone2;
        }
    }
}