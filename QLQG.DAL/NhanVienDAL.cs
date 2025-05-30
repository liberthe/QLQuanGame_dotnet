using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class NhanVienDAL
    {
        public static string GetTenNhanVien(int maNV)
        {
            string ten = "";
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TenNV FROM NhanVien WHERE MaNV = @ma", conn);
                cmd.Parameters.AddWithValue("@ma", maNV);
                var result = cmd.ExecuteScalar();
                if (result != null)
                    ten = result.ToString();
            }
            return ten;
        }
    }
}
