using QLQG.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class NguyennhanDAO
    {
        public static List<NguyennhanDTO> GetAllNguyennhan()
        {
            List<NguyennhanDTO> nguyennhans = new List<NguyennhanDTO>();
            using (SqlConnection conn = DataProvider.Instance.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT MaNN, TenNN FROM dbo.Nguyennhan", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nguyennhans.Add(new NguyennhanDTO
                            {
                                MaNN = Convert.ToInt32(reader["MaNN"]),
                                TenNN = reader["TenNN"].ToString()
                            });
                        }
                    }
                }
            }
            return nguyennhans;
        }
    }
}