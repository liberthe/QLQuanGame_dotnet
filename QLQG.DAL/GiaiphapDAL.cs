using QLQG.DTO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class GiaiphapDAL
    {
        public static decimal GetChiPhi(int maGP)
        {
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Chiphi FROM dbo.Giaiphap WHERE MaGP = @MaGP", conn))
                {
                    cmd.Parameters.AddWithValue("@MaGP", maGP);
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        public static string GetTenGP(int maGP)
        {
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TenGP FROM dbo.Giaiphap WHERE MaGP = @MaGP", conn))
                {
                    cmd.Parameters.AddWithValue("@MaGP", maGP);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? string.Empty;
                }
            }
        }

        public static int GetMaGPFromMaTT(int maTT)
        {
            // Giả định ánh xạ 1:1 giữa MaTT và MaGP dựa trên dữ liệu ChitietBT
            // Có thể cần bảng ánh xạ thực tế
            switch (maTT)
            {
                case 2: return 2; // Màn hình xanh -> Cài lại driver
                case 3: return 3; // Chơi game bị lag -> Nâng cấp RAM
                case 4: return 4; // Không kết nối mạng -> Cấu hình router
                default: return maTT; // Mặc định trả về cùng giá trị nếu không có ánh xạ
            }
        }
    }
}