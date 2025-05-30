using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QLQG.DTO;

namespace QLQG.DAL
{
    public class BillingDAL
    {
        private static BillingDAL instance;

        public static BillingDAL Instance
        {
            get
            {
                if (instance == null)
                    instance = new BillingDAL();
                return instance;
            }
            private set => instance = value;
        }

        private BillingDAL() { }

        public List<Hoadon> LoadBillList(DateTime ngayBD, DateTime ngayKT, string tenNV = null, string loaiHD = null)
        {
            try
            { 
            Console.WriteLine($"Gọi sp_GetBillListFiltered: NgayBD={ngayBD}, NgayKT={ngayKT}, TenNV={tenNV ?? "NULL"}, LoaiHD={loaiHD ?? "NULL"}");

            var parameters = new (string, object)[]
            {
                ("@NgayBD", ngayBD),
                ("@NgayKT", ngayKT),
                ("@TenNV", (object)tenNV ?? DBNull.Value),
                ("@LoaiHD", (object)loaiHD ?? DBNull.Value)
            };

            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_GetBillListFiltered", parameters, CommandType.StoredProcedure);

            List<Hoadon> danhSach = new List<Hoadon>();

                if (dt == null || dt.Rows.Count == 0)
                {
                    Console.WriteLine("Không tìm thấy hóa đơn.");
                    return danhSach;
                }

                foreach (DataRow row in dt.Rows)
                {
                    danhSach.Add(new Hoadon(row));
                }
                return danhSach;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Lỗi SQL trong BillingDAL.LoadBillList: {sqlEx.Message}\n{sqlEx.StackTrace}");
                throw new Exception("Không thể tải danh sách hóa đơn", sqlEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong BillingDAL.LoadBillList: {ex.Message}\n{ex.StackTrace}");
                throw new Exception("Không thể tải danh sách hóa đơn", ex);
            }
        }

        public int CreateHoaDon(int maNV, string loaiHD = "Thu")
        {
            try
            {
                using (SqlConnection conn = DataProvider.Instance.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_CreateHoaDon", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Int) { Value = maNV });
                        cmd.Parameters.Add(new SqlParameter("@LoaiHD", SqlDbType.NVarChar, 50) { Value = loaiHD });
                        SqlParameter maHDParam = new SqlParameter("@MaHD", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(maHDParam);

                        cmd.ExecuteNonQuery();
                        return Convert.ToInt32(maHDParam.Value);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Lỗi SQL trong BillingDAL.CreateHoaDon: {sqlEx.Message}\n{sqlEx.StackTrace}");
                throw new Exception("Không thể tạo hóa đơn", sqlEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong BillingDAL.CreateHoaDon: {ex.Message}\n{ex.StackTrace}");
                throw new Exception("Không thể tạo hóa đơn", ex);
            }
        }

        public void UpdateTongTien(int maHD, decimal amount)
        {
            try
            {
                var parameters = new (string, object)[]
                    {
                        ("@MaHD", maHD),
                        ("@Amount", amount)
                    };

                DataProvider.Instance.ExecuteNonQuery("sp_UpdateTongTien", parameters, CommandType.StoredProcedure);

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Lỗi SQL trong BillingDAL.UpdateTongTien: {sqlEx.Message}\n{sqlEx.StackTrace}");
                throw new Exception("Không thể cập nhật tổng tiền", sqlEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong BillingDAL.UpdateTongTien: {ex.Message}\n{ex.StackTrace}");
                throw new Exception("Không thể cập nhật tổng tiền", ex);
            }
        }
    }
}