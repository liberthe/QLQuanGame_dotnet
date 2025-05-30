using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLquanGame
{
    public partial class ucMon : UserControl
    {
        private int maMon;
        private string tenMon;
        private decimal gia;
        private string hinhAnh;
        public int MaMon
        {
            get { return maMon; }
            set { maMon = value; }
        }

        public string TenMon
        {
            get => tenMon;
            set
            {
                tenMon = value;
                lblTenMon.Text = tenMon;
            }
        }

        public decimal Gia
        {
            get => gia;
            set
            {
                gia = value;
                lblGia.Text = gia.ToString("N0") + "đ";
            }
        }

        public string HinhAnh
        {
            get => hinhAnh;
            set
            {
                hinhAnh = value;
                LoadImage(hinhAnh);
            }
        }

        // Constructor
        public ucMon(int maMon, string tenMon, decimal gia, string tenAnh)
        {
            InitializeComponent();

            MaMon = maMon;
            TenMon = tenMon;
            Gia = gia;
            HinhAnh = tenAnh;

            // Gán sự kiện click cho toàn bộ control
            GanSuKienClick(this, UcMon_Click);
        }

        // Load hình ảnh từ thư mục Resources
        private void LoadImage(string tenAnh)
        {
            try
            {
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", tenAnh);

                if (!File.Exists(imagePath))
                {
                    picAnhMon.Image = null;
                    ToolTip tip = new ToolTip();
                    tip.SetToolTip(picAnhMon, "Hình ảnh không tồn tại: " + tenAnh);
                    return;
                }

                using (var tempImage = Image.FromFile(imagePath))
                {
                    picAnhMon.Image = new Bitmap(tempImage);
                }
            }
            catch (Exception ex)
            {
                picAnhMon.Image = null;
                Console.WriteLine("Lỗi load hình ảnh: " + ex.Message);
            }
        }

        private void GanSuKienClick(Control ctrl, EventHandler handler)
        {
            ctrl.Click += handler;
            foreach (Control child in ctrl.Controls)
            {
                GanSuKienClick(child, handler);
            }
        }

        // ✅ Hàm xử lý khi click
        private void UcMon_Click(object sender, EventArgs e)
        {
            //DatMon form = new DatMon(this.TenMon, this.Gia);
            //  form.ShowDialog();
            // Tìm form cha (DatMon)
            Form parentForm = this.FindForm();
            if (parentForm is DatMon datMonForm)
            {
                // Gọi phương thức AddMonToGrid của form cha
                datMonForm.AddMonToGrid(this.MaMon, this.TenMon, this.Gia);
            }
        }



        private void picAnhMon_Click(object sender, EventArgs e)
        {
            // DatMon form = new DatMon();
            //form.ShowDialog();
        }

    }
}
