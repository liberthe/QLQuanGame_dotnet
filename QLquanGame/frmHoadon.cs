using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using QLQG.DAL;
using QLQG.DTO;

namespace QLquanGame
{
    public partial class frmHoadon : Form
    {
        decimal inc = 0;
        decimal outc = 0;
        decimal profit = 0;
        CultureInfo ct = new CultureInfo("vi-VN");
        private DateTimePicker lastSelectedDateTimePicker;
        private PrintDocument printDocument;
        private List<Hoadon> currentBillList;

        public frmHoadon()
        {
            InitializeComponent();
            ct = new CultureInfo("vi-VN");
            LoadDate();
            ShowBill(tpBd.MinDate, tpKt.MaxDate);

            txtNhanvien.TextChanged += txtNhanvien_TextChanged;
            rbThu.CheckedChanged += rbThuChi_CheckedChanged;
            rbChi.CheckedChanged += rbThuChi_CheckedChanged;

            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void rbKhoang_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKhoang.Checked)
            {
                gbKhoang.Enabled = true;
                ShowBill(tpBd.Value, tpKt.Value);
                ResetValue();
            }
            else
            {
                gbKhoang.Enabled = false;
            }
        }

        private void tpKt_ValueChanged(object sender, EventArgs e)
        {
            ShowBill(tpBd.Value, tpKt.Value);
            ResetValue();
        }

        private void tpBd_ValueChanged(object sender, EventArgs e)
        {
            ShowBill(tpBd.Value, tpKt.Value);
            ResetValue();
        }

        private void rbHai_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHai.Checked)
            {
                gbHai.Enabled = true;
                ResetValue();
            }
            else
            {
                gbHai.Enabled = false;
            }
        }

        private void tpTs_ValueChanged(object sender, EventArgs e)
        {
            lastSelectedDateTimePicker = tpTs;
            ApplyDateFilter();
            ResetValue();
        }

        private void tpTe_ValueChanged(object sender, EventArgs e)
        {
            lastSelectedDateTimePicker = tpTe;
            ApplyDateFilter();
            ResetValue();
        }

        private void rbThuChi_CheckedChanged(object sender, EventArgs e)
        {
            ShowBill(tpBd.Value, tpKt.Value);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetValue();
            ShowBill(tpBd.MinDate, tpKt.MaxDate);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (lvBill.SelectedItems.Count > 0)
            {
                if (currentBillList == null || currentBillList.Count == 0)
                {
                    MessageBox.Show("Danh sách hóa đơn trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (lvBill.Items.Count != currentBillList.Count)
                {
                    MessageBox.Show("Dữ liệu không đồng bộ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowBill(tpBd.Value, tpKt.Value);
                    return;
                }

                int selectedIndex = lvBill.SelectedIndices[0];
                if (selectedIndex < 0 || selectedIndex >= currentBillList.Count)
                {
                    MessageBox.Show("Chỉ số hóa đơn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument;
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (lvBill.SelectedItems.Count == 0 || currentBillList == null || currentBillList.Count == 0)
                return;

            int selectedIndex = lvBill.SelectedIndices[0];
            if (selectedIndex < 0 || selectedIndex >= currentBillList.Count)
                return;

            Hoadon bill = currentBillList[selectedIndex];
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font regularFont = new Font("Arial", 12);
            Font boldFont = new Font("Arial", 12, FontStyle.Bold);
            float yPos = 50;
            float pageWidth = e.MarginBounds.Width;
            CultureInfo ct = new CultureInfo("vi-VN"); // Định dạng VNĐ

            // Tiêu đề
            string title = "Hóa Đơn Metanoia Gaming";
            float xPos = (pageWidth - e.Graphics.MeasureString(title, titleFont).Width) / 2;
            e.Graphics.DrawString(title, titleFont, Brushes.Black, xPos, yPos);
            yPos += titleFont.GetHeight() + 20;

            // Thông tin hóa đơn
            e.Graphics.DrawString($"Mã hóa đơn: {bill.MaHD}", regularFont, Brushes.Black, 50, yPos);
            yPos += regularFont.GetHeight();
            e.Graphics.DrawString($"Ngày tạo: {bill.NgayTao:dd/MM/yyyy HH:mm}", regularFont, Brushes.Black, 50, yPos);
            yPos += regularFont.GetHeight();
            e.Graphics.DrawString($"Nhân viên: {bill.TenNV}", regularFont, Brushes.Black, 50, yPos);
            yPos += regularFont.GetHeight();
            e.Graphics.DrawString($"Loại hóa đơn: {bill.LoaiHD}", regularFont, Brushes.Black, 50, yPos);
            yPos += regularFont.GetHeight() + 10;

            // Chi tiết món gọi
            e.Graphics.DrawString("Danh sách món:", boldFont, Brushes.Black, 50, yPos);
            yPos += boldFont.GetHeight() + 5;
            try
            {
                DataTable dt = GoiMonDAO.Instance.GetGoiMonByMaHD(bill.MaHD);
                if (dt.Rows.Count == 0)
                {
                    e.Graphics.DrawString("Không có món gọi.", regularFont, Brushes.Black, 70, yPos);
                    yPos += regularFont.GetHeight();
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string tenMon = row["TenMon"]?.ToString() ?? "";
                        int soLuong = row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : 0;
                        decimal thanhTien = row["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(row["ThanhTien"]) : 0;
                        e.Graphics.DrawString($"{tenMon} x {soLuong}: {thanhTien.ToString("C2", ct)}", regularFont, Brushes.Black, 70, yPos);
                        yPos += regularFont.GetHeight();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi in món gọi (MaHD={bill.MaHD}): {ex.Message}\n{ex.StackTrace}");
                e.Graphics.DrawString("Không thể tải danh sách món.", regularFont, Brushes.Black, 70, yPos);
                yPos += regularFont.GetHeight();
            }

            // Chi tiết thuê máy
            yPos += 10;
            e.Graphics.DrawString("Danh sách thuê máy:", boldFont, Brushes.Black, 50, yPos);
            yPos += boldFont.GetHeight() + 5;
            try
            {
                using (SqlConnection conn = DataProvider.Instance.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    mt.TenMay, 
                    tm.GioBD, 
                    tm.GioKT,
                    p.Giatheogio,
                    DATEDIFF(MINUTE, tm.GioBD, ISNULL(tm.GioKT, GETDATE())) / 60.0 * p.Giatheogio AS ThanhTien
                FROM Thuemay tm
                JOIN Maytinh mt ON tm.MaMay = mt.MaMay
                JOIN Phong p ON mt.MaPhong = p.MaPhong
                WHERE tm.MaHD = @MaHD", conn);
                    cmd.Parameters.AddWithValue("@MaHD", bill.MaHD);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            e.Graphics.DrawString("Không có thuê máy.", regularFont, Brushes.Black, 70, yPos);
                            yPos += regularFont.GetHeight();
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                string tenMay = dr["TenMay"]?.ToString() ?? "";
                                string gioBD = Convert.ToDateTime(dr["GioBD"]).ToString("dd/MM/yyyy HH:mm");
                                string gioKT = dr["GioKT"] != DBNull.Value
                                    ? Convert.ToDateTime(dr["GioKT"]).ToString("dd/MM/yyyy HH:mm")
                                    : "Đang thuê";
                                decimal thanhTien = dr["ThanhTien"] != DBNull.Value
                                    ? Convert.ToDecimal(dr["ThanhTien"])
                                    : 0;
                                e.Graphics.DrawString($"{tenMay} ({gioBD} - {gioKT}): {thanhTien.ToString("C2", ct)}", regularFont, Brushes.Black, 70, yPos);
                                yPos += regularFont.GetHeight();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi in thuê máy (MaHD={bill.MaHD}): {ex.Message}\n{ex.StackTrace}");
                e.Graphics.DrawString("Không thể tải danh sách thuê máy.", regularFont, Brushes.Black, 70, yPos);
                yPos += regularFont.GetHeight();
            }

            // Tổng hợp
            yPos += 10;
            e.Graphics.DrawString($"Giá thuê máy: {bill.ThueMay.ToString("C2", ct)}", regularFont, Brushes.Black, 50, yPos);
            yPos += regularFont.GetHeight();
            e.Graphics.DrawString($"Giá món ăn: {bill.GiaMon.ToString("C2", ct)}", regularFont, Brushes.Black, 50, yPos);
            yPos += regularFont.GetHeight();
            e.Graphics.DrawString($"Chi phí bảo trì: {bill.ChiPhi.ToString("C2", ct)}", regularFont, Brushes.Black, 50, yPos);
            yPos += regularFont.GetHeight();
            e.Graphics.DrawString($"Tổng tiền: {bill.TongTien.ToString("C2", ct)}", boldFont, Brushes.Black, 50, yPos);
            yPos += boldFont.GetHeight() + 20;

            // Lời cảm ơn
            xPos = (pageWidth - e.Graphics.MeasureString("Cảm ơn quý khách!", new Font("Arial", 12, FontStyle.Italic)).Width) / 2;
            e.Graphics.DrawString("Cảm ơn quý khách!", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, xPos, yPos);
        }
        private void txtNhanvien_TextChanged(object sender, EventArgs e)
        {
            ShowBill(tpBd.Value, tpKt.Value);
        }

        void LoadDate()
        {
            DateTime today = DateTime.Now;
            tpBd.Value = new DateTime(today.Year, today.Month, 1);
            tpKt.Value = today;
            tpTs.Value = new DateTime(today.Year, today.Month, 1);
            tpTe.Value = today.AddMonths(-1);
        }

        void ShowBill(DateTime startDate, DateTime endDate)
        {
            try
            {
                lvBill.Items.Clear();
                string searchName = txtNhanvien.Text.Trim();
                string billingType = rbThu.Checked ? "Thu" : rbChi.Checked ? "Chi" : null;

                if (!rbKhoang.Checked && rbHai.Checked && lastSelectedDateTimePicker != null)
                {
                    startDate = new DateTime(lastSelectedDateTimePicker.Value.Year, lastSelectedDateTimePicker.Value.Month, 1);
                    endDate = startDate.AddMonths(1).AddSeconds(-1);
                }

                currentBillList = BillingDAL.Instance.LoadBillList(startDate, endDate, searchName, billingType);
                decimal income = 0;
                decimal outcome = 0;

                lvBill.Columns.Clear();
                lvBill.View = View.Details;
                lvBill.FullRowSelect = true;
                lvBill.GridLines = true;

                lvBill.Columns.Add("Mã hóa đơn", 100);
                lvBill.Columns.Add("Ngày thanh toán", 120);
                lvBill.Columns.Add("Nhân viên", 100);
                lvBill.Columns.Add("Loại hóa đơn", 100);
                lvBill.Columns.Add("Giá món ăn", 100);
                lvBill.Columns.Add("Chi phí bảo trì", 120);
                lvBill.Columns.Add("Giá thuê máy", 100);
                lvBill.Columns.Add("Tổng tiền", 120);

                foreach (var bill in currentBillList)
                {
                    ListViewItem item = new ListViewItem(bill.MaHD.ToString());
                    item.SubItems.Add(bill.NgayTao.ToString("dd/MM/yyyy HH:mm"));
                    item.SubItems.Add(bill.TenNV);
                    item.SubItems.Add(bill.LoaiHD);
                    item.SubItems.Add(bill.GiaMon.ToString("C2", ct));
                    item.SubItems.Add(bill.ChiPhi.ToString("C2", ct));
                    item.SubItems.Add(bill.ThueMay.ToString("C2", ct));
                    item.SubItems.Add(bill.TongTien.ToString("C2", ct));

                    if (bill.LoaiHD == "Thu")
                        income += bill.TongTien;
                    else
                        outcome += bill.TongTien;

                    lvBill.Items.Add(item);
                }

                inc = income;
                outc = outcome;
                profit = income - outcome;
                txtIn.Text = inc.ToString("C2", ct);
                txtOut.Text = outc.ToString("C2", ct);
                txtProfit.Text = profit.ToString("C2", ct);

                if (rbHai.Checked && lastSelectedDateTimePicker != null)
                {
                    if (lastSelectedDateTimePicker == tpTs)
                        txtProfitMonth.Text = profit.ToString("C2", ct);
                    else if (lastSelectedDateTimePicker == tpTe)
                        txtProfitOtherMonth.Text = profit.ToString("C2", ct);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong ShowBill: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show("Không thể tải danh sách hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                currentBillList = new List<Hoadon>();
                lvBill.Items.Clear();
            }
        }
        void ApplyDateFilter()
        {
            if (lastSelectedDateTimePicker != null)
            {
                DateTime startDate = new DateTime(lastSelectedDateTimePicker.Value.Year, lastSelectedDateTimePicker.Value.Month, 1);
                DateTime endDate = startDate.AddMonths(1).AddSeconds(-1);
                ShowBill(startDate, endDate);
            }
        }

        void ResetValue()
        {
            txtNhanvien.Text = "";
            rbThu.Checked = false;
            rbChi.Checked = false;
        }
    }
}