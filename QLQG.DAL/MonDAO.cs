using System;
using System.Collections.Generic;
using System.Data;
using QLQG.DTO;

namespace QLQG.DAL
{
    public class MonDAO
    {
        private static MonDAO instance;
        public static MonDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new MonDAO();
                return instance;
            }
        }

        public List<Mon> LayTatCaMon()
        {
            List<Mon> list = new List<Mon>();
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery("sp_LayTatCaMon", null, CommandType.StoredProcedure);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Mon(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong MonDAO.LayTatCaMon: {ex.Message}");
                throw new Exception("Không thể lấy danh sách món", ex);
            }
            return list;
        }

        public List<Mon> TimKiemMon(string keyword)
        {
            List<Mon> list = new List<Mon>();
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery("sp_TimKiemMon", new[] { ("@Keyword", (object)keyword) }, CommandType.StoredProcedure);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Mon(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong MonDAO.TimKiemMon: {ex.Message}");
                throw new Exception("Không thể tìm kiếm món", ex);
            }
            return list;
        }
    }
}