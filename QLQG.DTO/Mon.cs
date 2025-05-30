using System;
using System.Data;

namespace QLQG.DTO
{
    public class Mon
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; }
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }
        public int MaLoai { get; set; }

        public Mon() { }

        public Mon(int id, string tenMon, decimal gia, string hinhAnh, int maLoai)
        {
            MaMon = id;
            TenMon = tenMon;
            Gia = gia;
            HinhAnh = hinhAnh;
            MaLoai = maLoai;
        }

        public Mon(DataRow row)
        {
            MaMon = row["MaMon"] != DBNull.Value ? Convert.ToInt32(row["MaMon"]) : 0;
            TenMon = row["TenMon"]?.ToString() ?? "";
            Gia = row["Gia"] != DBNull.Value ? Convert.ToDecimal(row["Gia"]) : 0;
            HinhAnh = row["Anh"]?.ToString() ?? "";
            MaLoai = row["MaLoai"] != DBNull.Value ? Convert.ToInt32(row["MaLoai"]) : 0;
        }
    }
}