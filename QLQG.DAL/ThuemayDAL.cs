using System;
using System.Collections.Generic;
using System.Data;
using QLQG.DTO;

namespace QLQG.DAL
{
    public class ThueMayDAL
    {
        public void StartThueMay(int maMay, int maHD)
        {
            Dictionary<string, object> outputParams;
            DataProvider.Instance.ExecuteStoredProcedure(
                "sp_StartThueMay",
                out outputParams,
                ("@MaMay", maMay),
                ("@MaHD", maHD),
                ("@MaTM", null) // OUTPUT
            );
        }

        public void EndThueMay(int maTM)
        {
            try
            {
                DataProvider.Instance.ExecuteNonQuery(
                    "sp_EndThueMay",
                    new (string, object)[] { ("@MaTM", maTM) },
                    CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kết thúc phiên thuê máy (MaTM = {maTM}): {ex.Message}", ex);
            }
        }

        public ThueMayDTO GetCurrentThueMay(int maMay)
        {
            try
            {
                if (maMay <= 0)
                {
                    throw new ArgumentException("Mã máy không hợp lệ.", nameof(maMay));
                }

                Console.WriteLine($"GetCurrentThueMay: Gọi sp_GetCurrentThueMay với MaMay = {maMay}");

                DataTable dt = DataProvider.Instance.ExecuteQuery(
                    "sp_GetCurrentThueMay",
                    new (string, object)[] { ("@MaMay", maMay) },
                    CommandType.StoredProcedure
                );

                if (dt == null || dt.Rows.Count == 0)
                {
                    Console.WriteLine("GetCurrentThueMay: Không tìm thấy bản ghi thuê máy.");
                    return null;
                }

                DataRow row = dt.Rows[0];
                return new ThueMayDTO
                {
                    MaTM = Convert.ToInt32(row["MaTM"]),
                    MaMay = Convert.ToInt32(row["MaMay"]),
                    GioBD = Convert.ToDateTime(row["GioBD"]),
                    GioKT = row["GioKT"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["GioKT"]),
                    MaHD = Convert.ToInt32(row["MaHD"])
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong GetCurrentThueMay: {ex.Message}\n{ex.StackTrace}");
                throw new Exception($"Lỗi khi truy vấn thông tin thuê máy hiện tại cho MaMay = {maMay}.", ex);
            }
        }
    }
}