using QLQG.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class TinhtrangDAO
    {
        public static List<TinhtrangDTO> GetAllTinhtrang()
        {
            List<TinhtrangDTO> tinhtrangs = new List<TinhtrangDTO>();
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT MaTT, TenTT FROM dbo.Tinhtrang", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tinhtrangs.Add(new TinhtrangDTO
                            {
                                MaTT = Convert.ToInt32(reader["MaTT"]),
                                TenTT = reader["TenTT"].ToString()
                            });
                        }
                    }
                }
            }
            return tinhtrangs;
        }
    }
}