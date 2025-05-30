using System.Data;
using System;

public class Hoadon
{
    public int MaHD { get; set; }
    public DateTime NgayTao { get; set; }
    public string TenNV { get; set; }
    public string LoaiHD { get; set; }
    public decimal ChiPhi { get; set; }
    public decimal GiaMon { get; set; }
    public decimal ThueMay { get; set; }
    public decimal TongTien { get; set; }

    public Hoadon(DataRow row)
    {
        MaHD = Convert.ToInt32(row["MaHD"]);
        NgayTao = Convert.ToDateTime(row["NgayTao"]);
        TenNV = row["TenNV"].ToString();
        LoaiHD = row["LoaiHD"].ToString();
        ChiPhi = row["ChiPhi"] != DBNull.Value ? Convert.ToDecimal(row["ChiPhi"]) : 0;
        GiaMon = row["GiaMon"] != DBNull.Value ? Convert.ToDecimal(row["GiaMon"]) : 0;
        ThueMay = row["ThueMay"] != DBNull.Value ? Convert.ToDecimal(row["ThueMay"]) : 0;
        TongTien = row["TongTien"] != DBNull.Value ? Convert.ToDecimal(row["TongTien"]) : 0;
    }
}