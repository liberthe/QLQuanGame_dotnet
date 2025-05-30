using System;
using System.Collections.Generic;
using System.Data;
using QLQG.DTO;

namespace QLQG.DAL
{
    public class MayTinh_DAL
    {
        public List<MayTinhDTO> GetMayTinhByPhong(int maPhong)
        {
            List<MayTinhDTO> list = new List<MayTinhDTO>();
            var parameters = new (string, object)[]
            {
        ("@MaPhong", maPhong)
            };

            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_GetMayTinhByPhong", parameters, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MayTinhDTO(row));
            }
            return list;
        }


        public void UpdateTrangThaiMay(int maMay, string trangThai)
        {
            DataProvider.Instance.ExecuteNonQuery(
                "UPDATE Maytinh SET Trangthai = @TrangThai WHERE MaMay = @MaMay",
                new (string, object)[] { ("@TrangThai", trangThai), ("@MaMay", maMay) }
            );
        }
    }
}