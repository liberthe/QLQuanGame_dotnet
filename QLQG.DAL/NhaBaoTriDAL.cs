using QLQG.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class NhaBaoTriDAL
    {
        public static List<NhaBaoTriDTO> GetAllNhaBaoTri()
        {
            List<NhaBaoTriDTO> nhaBaoTris = new List<NhaBaoTriDTO>();
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT MaNBT, TenNBT FROM dbo.NhaBT", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nhaBaoTris.Add(new NhaBaoTriDTO
                            {
                                MaNBT = Convert.ToInt32(reader["MaNBT"]),
                                TenNBT = reader["TenNBT"].ToString()
                            });
                        }
                    }
                }
            }
            return nhaBaoTris;
        }
    }
}