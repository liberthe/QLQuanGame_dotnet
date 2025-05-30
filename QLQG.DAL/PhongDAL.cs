using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QLQG.DTO;

namespace QLQG.DAL
{
    public class PhongDAL
    {
        public List<PhongDTO> GetAllPhong()
        {
            List<PhongDTO> phongList = new List<PhongDTO>();
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery("sp_GetAllPhong", null, CommandType.StoredProcedure);
                foreach (DataRow row in dt.Rows)
                {
                    PhongDTO phong = new PhongDTO
                    {
                        MaPhong = row["MaPhong"] != DBNull.Value ? Convert.ToInt32(row["MaPhong"]) : 0,
                        TenPhong = row["TenPhong"]?.ToString() ?? "",
                        SoMay = row["SoMay"] != DBNull.Value ? Convert.ToInt32(row["SoMay"]) : 0,
                        Giatheogio = row["Giatheogio"] != DBNull.Value ? Convert.ToDecimal(row["Giatheogio"]) : 0
                    };
                    phongList.Add(phong);
                }
                return phongList;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Lỗi SQL trong PhongDAL.GetAllPhong: {sqlEx.Message}\n{sqlEx.StackTrace}");
                throw new Exception("Không thể lấy danh sách phòng", sqlEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong PhongDAL.GetAllPhong: {ex.Message}\n{ex.StackTrace}");
                throw new Exception("Không thể lấy danh sách phòng", ex);
            }
        }
    }
}