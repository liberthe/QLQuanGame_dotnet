using System;
using System.Data.SqlClient;
using QLQG.DTO;

namespace QLQG.DAL
{
    public class LoginDAL
    {
        private readonly DataProvider dao = DataProvider.Instance;

        public LoginDTO AuthenticateUser(string TenDangNhap, string MatKhau)
        {
            try
            {
                using (SqlConnection conn = dao.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT MaNV, TenDangNhap FROM Taikhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau", conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", MatKhau);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LoginDTO
                            {
                                MaNV = reader.GetInt32(0),
                                TenDangNhap = reader.GetString(1)
                            };
                        }
                    }
                }
                return null; // Trả về null nếu không tìm thấy tài khoản
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL khi xác thực người dùng: {ex.Message}\n{ex.StackTrace}");
                throw new Exception("Lỗi kết nối cơ sở dữ liệu khi đăng nhập.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xác thực người dùng: {ex.Message}\n{ex.StackTrace}");
                throw new Exception("Đã xảy ra lỗi trong quá trình đăng nhập.", ex);
            }
        }
    }
}