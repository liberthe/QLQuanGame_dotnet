using System;
using System.Data;
using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class GoimonDAL
    {
        public static decimal CalculateTotalByMaHD(int maHD)
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
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL trong GoimonDAL.CalculateTotalByMaHD: {ex.Message}");
                throw new Exception("Không thể tính tổng tiền hóa đơn", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong GoimonDAL.CalculateTotalByMaHD: {ex.Message}");
                throw new Exception("Lỗi không xác định khi tính tổng tiền", ex);
            }
        }
    }
}
