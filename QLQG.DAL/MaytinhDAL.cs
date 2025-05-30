using QLQG.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class MaytinhDAL
    {
        public static List<MaytinhDTOo> GetAllComputers()
        {
            List<MaytinhDTOo> computers = new List<MaytinhDTOo>();
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT MaMay, TenMay, MaPhong, Trangthai FROM dbo.Maytinh", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            computers.Add(new MaytinhDTOo
                            {
                                MaMay = Convert.ToInt32(reader["MaMay"]),
                                TenMay = reader["TenMay"].ToString(),
                                MaPhong = Convert.ToInt32(reader["MaPhong"]),
                                Trangthai = reader["Trangthai"].ToString()
                            });
                        }
                    }
                }
            }
            return computers;
        }

public static string GetTrangthai(int maMay)
        {
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Trangthai FROM dbo.Maytinh WHERE MaMay = @MaMay", conn))
                {
                    cmd.Parameters.AddWithValue("@MaMay", maMay);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        public static void UpdateTrangThaiMay(int maMay, string trangThai)
        {
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE MayTinh SET Trangthai = @Trangthai WHERE MaMay = @MaMay", conn))
                {
                    cmd.Parameters.AddWithValue("@MaMay", maMay);
                    cmd.Parameters.AddWithValue("@Trangthai", trangThai);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}