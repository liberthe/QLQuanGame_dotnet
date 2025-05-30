using System;
using System.Data;

namespace QLQG.DTO
{
    public class MayTinhDTO
    {
        public int MaMay { get; set; }
        public string TenMay { get; set; }
        public int MaPhong { get; set; }
        public string Trangthai { get; set; }
        public string TenOcung { get; set; }
        public string TenDL { get; set; }
        public string TenChip { get; set; }
        public string TenRAM { get; set; }
        public string TenTD { get; set; }
        public string TenOdia { get; set; }
        public string TenMH { get; set; }
        public string TenChuot { get; set; }
        public string TenBP { get; set; }
        public string TenTN { get; set; }

        public MayTinhDTO(DataRow row)
        {
            MaMay = row["MaMay"] != DBNull.Value ? Convert.ToInt32(row["MaMay"]) : 0;
            TenMay = row["TenMay"]?.ToString() ?? "";
            MaPhong = row["MaPhong"] != DBNull.Value ? Convert.ToInt32(row["MaPhong"]) : 0;
            Trangthai = row["Trangthai"]?.ToString() ?? "";
            TenOcung = row["TenOcung"]?.ToString() ?? "";
            TenDL = row["TenDL"]?.ToString() ?? "";
            TenChip = row["TenChip"]?.ToString() ?? "";
            TenRAM = row["TenRAM"]?.ToString() ?? "";
            TenTD = row["TenTD"]?.ToString() ?? "";
            TenOdia = row["TenOdia"]?.ToString() ?? "";
            TenMH = row["TenMH"]?.ToString() ?? "";
            TenChuot = row["TenChuot"]?.ToString() ?? "";
            TenBP = row["TenBP"]?.ToString() ?? "";
            TenTN = row["TenTN"]?.ToString() ?? "";
        }
    }
}
