using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class GoiMonDAO
    {
        private static GoiMonDAO instance;

        public static GoiMonDAO Instance
        {
            get { if (instance == null) instance = new GoiMonDAO(); return instance; }
            private set { instance = value; }
        }

        private GoiMonDAO() { }

        public bool AddOrUpdateGoiMon(int maHD, int maMon)
        {
            try
            {
                using (SqlConnection conn = DataProvider.Instance.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_AddOrUpdateGoiMon", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaHD", maHD);
                        cmd.Parameters.AddWithValue("@MaMon", maMon);
                        cmd.Parameters.AddWithValue("@SoLuong", 1);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong GoiMonDAO.AddOrUpdateGoiMon: {ex.Message}\n{ex.StackTrace}");
                throw new Exception("Không thể thêm hoặc cập nhật món gọi", ex);
            }
        }

        public DataTable GetGoiMonByMaHD(int maHD)
        {
            try
            {
                return DataProvider.Instance.ExecuteQuery("sp_GetGoiMonByMaHD", new[] { ("@MaHD", (object)maHD) }, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong GoiMonDAO.GetGoiMonByMaHD: {ex.Message}");
                throw new Exception("Không thể lấy danh sách món gọi", ex);
            }
        }

        public decimal CalculateTotalByMaHD(int maHD)
        {
            try
            {
                using (SqlConnection conn = DataProvider.Instance.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_CalculateTotalByMaHD", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaHD", maHD);
                        SqlParameter outputParam = new SqlParameter("@TongTien", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();
                        return outputParam.Value != null && outputParam.Value != DBNull.Value ? Convert.ToDecimal(outputParam.Value) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong GoiMonDAO.CalculateTotalByMaHD: {ex.Message}");
                throw new Exception("Không thể tính tổng tiền hóa đơn", ex);
            }
        }
    }
}