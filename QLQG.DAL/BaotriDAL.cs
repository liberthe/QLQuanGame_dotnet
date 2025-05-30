using QLQG.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class BaotriDAL
    {
        public static List<BaotriDTO> GetMaintainListForMachine(int maMay)
        {
            List<BaotriDTO> maintainList = new List<BaotriDTO>();
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetMaintainListForMachine", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaMay", maMay);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maintainList.Add(new BaotriDTO
                            {
                                MaBT = Convert.ToInt32(reader["MaBT"]),
                                GhiChu = reader["GhiChu"].ToString(),
                                TrangThai = reader["TrangthaiBaotri"].ToString(),
                                MaHD = Convert.ToInt32(reader["MaHD"])
                            });
                        }
                    }
                }
            }
            return maintainList;
        }

        public static int CreateBaotri(int maNV, int maMay, int maTT, int maNN, int maGP, string ghiChu, decimal chiPhi, int maNBT)
        {
            Dictionary<string, object> outputParams;
            DataProvider.Instance.ExecuteStoredProcedure(
                "sp_CreateBaotri",
                out outputParams,
                ("@MaNV", maNV),
                ("@MaMay", maMay),
                ("@MaTT", maTT),
                ("@MaNN", maNN),
                ("@MaGP", maGP),
                ("@GhiChu", ghiChu ?? (object)DBNull.Value),
                ("@ChiPhi", chiPhi),
                ("@MaNBT", maNBT), // Thêm tham số mới
                ("@MaBT", null),
                ("@MaHD", null)
            );
            return Convert.ToInt32(outputParams["@MaBT"]);
        }
        public static void ThanhToanBaotri(int maNV, List<int> maBTList, out int maHD)
        {
            Dictionary<string, object> outputParams;
            DataProvider.Instance.ExecuteStoredProcedure(
                "sp_ThanhToanBaotri",
                out outputParams,
                ("@MaNV", maNV),
                ("@MaBTList", string.Join(",", maBTList)),
                ("@MaHD", null)
            );
            maHD = Convert.ToInt32(outputParams["@MaHD"]);
        }

        public static int LaySoLoiChuaHoanTat(int maMay)
        {
            object result = DataProvider.Instance.ExecuteScalar(
                "SELECT dbo.fn_LaySoLoiChuaHoanTat(@MaMay)",
                ("@MaMay", maMay)
            );
            return Convert.ToInt32(result);
        }
    }
}