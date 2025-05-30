namespace QLquanGame
{
    partial class SuaMon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.dgvDanhSachMon = new System.Windows.Forms.DataGridView();
            this.lblSuaMon = new System.Windows.Forms.Label();
            this.cboLoaiMon = new System.Windows.Forms.ComboBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.txtAnh = new System.Windows.Forms.TextBox();
            this.txtTonKho = new System.Windows.Forms.TextBox();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.txtTenMon = new System.Windows.Forms.TextBox();
            this.lblAnh = new System.Windows.Forms.Label();
            this.lblLoaimon = new System.Windows.Forms.Label();
            this.lblTonkho = new System.Windows.Forms.Label();
            this.lblGianhap = new System.Windows.Forms.Label();
            this.lblGiaban = new System.Windows.Forms.Label();
            this.lblTenmon = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picAnh = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachMon)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(656, 13);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 35);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(895, 13);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(66, 35);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(775, 13);
            this.btnSua.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(68, 35);
            this.btnSua.TabIndex = 0;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // dgvDanhSachMon
            // 
            this.dgvDanhSachMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachMon.Location = new System.Drawing.Point(7, 230);
            this.dgvDanhSachMon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvDanhSachMon.Name = "dgvDanhSachMon";
            this.dgvDanhSachMon.RowHeadersWidth = 82;
            this.dgvDanhSachMon.RowTemplate.Height = 33;
            this.dgvDanhSachMon.Size = new System.Drawing.Size(1033, 269);
            this.dgvDanhSachMon.TabIndex = 32;
            this.dgvDanhSachMon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachMon_CellClick);
            // 
            // lblSuaMon
            // 
            this.lblSuaMon.AutoSize = true;
            this.lblSuaMon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuaMon.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblSuaMon.Location = new System.Drawing.Point(410, 18);
            this.lblSuaMon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSuaMon.Name = "lblSuaMon";
            this.lblSuaMon.Size = new System.Drawing.Size(201, 20);
            this.lblSuaMon.TabIndex = 0;
            this.lblSuaMon.Text = "SỬA THÔNG TIN MÓN";
            // 
            // cboLoaiMon
            // 
            this.cboLoaiMon.FormattingEnabled = true;
            this.cboLoaiMon.Location = new System.Drawing.Point(135, 195);
            this.cboLoaiMon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboLoaiMon.Name = "cboLoaiMon";
            this.cboLoaiMon.Size = new System.Drawing.Size(145, 24);
            this.cboLoaiMon.TabIndex = 31;
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.Location = new System.Drawing.Point(668, 176);
            this.btnChonAnh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(123, 24);
            this.btnChonAnh.TabIndex = 30;
            this.btnChonAnh.Text = "Chọn ảnh mới";
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // txtAnh
            // 
            this.txtAnh.Location = new System.Drawing.Point(501, 183);
            this.txtAnh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAnh.Name = "txtAnh";
            this.txtAnh.Size = new System.Drawing.Size(141, 22);
            this.txtAnh.TabIndex = 29;
            // 
            // txtTonKho
            // 
            this.txtTonKho.Location = new System.Drawing.Point(135, 164);
            this.txtTonKho.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTonKho.Name = "txtTonKho";
            this.txtTonKho.Size = new System.Drawing.Size(145, 22);
            this.txtTonKho.TabIndex = 28;
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Location = new System.Drawing.Point(135, 134);
            this.txtGiaNhap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(145, 22);
            this.txtGiaNhap.TabIndex = 27;
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Location = new System.Drawing.Point(135, 107);
            this.txtGiaBan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(145, 22);
            this.txtGiaBan.TabIndex = 26;
            // 
            // txtTenMon
            // 
            this.txtTenMon.Location = new System.Drawing.Point(135, 71);
            this.txtTenMon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.Size = new System.Drawing.Size(243, 22);
            this.txtTenMon.TabIndex = 25;
            // 
            // lblAnh
            // 
            this.lblAnh.AutoSize = true;
            this.lblAnh.Location = new System.Drawing.Point(445, 75);
            this.lblAnh.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAnh.Name = "lblAnh";
            this.lblAnh.Size = new System.Drawing.Size(30, 16);
            this.lblAnh.TabIndex = 24;
            this.lblAnh.Text = "Ảnh";
            // 
            // lblLoaimon
            // 
            this.lblLoaimon.AutoSize = true;
            this.lblLoaimon.Location = new System.Drawing.Point(41, 200);
            this.lblLoaimon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLoaimon.Name = "lblLoaimon";
            this.lblLoaimon.Size = new System.Drawing.Size(62, 16);
            this.lblLoaimon.TabIndex = 23;
            this.lblLoaimon.Text = "Loại món";
            // 
            // lblTonkho
            // 
            this.lblTonkho.AutoSize = true;
            this.lblTonkho.Location = new System.Drawing.Point(41, 168);
            this.lblTonkho.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTonkho.Name = "lblTonkho";
            this.lblTonkho.Size = new System.Drawing.Size(56, 16);
            this.lblTonkho.TabIndex = 22;
            this.lblTonkho.Text = "Tồn kho";
            // 
            // lblGianhap
            // 
            this.lblGianhap.AutoSize = true;
            this.lblGianhap.Location = new System.Drawing.Point(41, 136);
            this.lblGianhap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGianhap.Name = "lblGianhap";
            this.lblGianhap.Size = new System.Drawing.Size(61, 16);
            this.lblGianhap.TabIndex = 21;
            this.lblGianhap.Text = "Giá nhập";
            // 
            // lblGiaban
            // 
            this.lblGiaban.AutoSize = true;
            this.lblGiaban.Location = new System.Drawing.Point(41, 107);
            this.lblGiaban.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGiaban.Name = "lblGiaban";
            this.lblGiaban.Size = new System.Drawing.Size(54, 16);
            this.lblGiaban.TabIndex = 20;
            this.lblGiaban.Text = "Giá bán";
            // 
            // lblTenmon
            // 
            this.lblTenmon.AutoSize = true;
            this.lblTenmon.Location = new System.Drawing.Point(41, 73);
            this.lblTenmon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTenmon.Name = "lblTenmon";
            this.lblTenmon.Size = new System.Drawing.Size(60, 16);
            this.lblTenmon.TabIndex = 19;
            this.lblTenmon.Text = "Tên món";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel2.Controls.Add(this.btnHuy);
            this.panel2.Controls.Add(this.btnXoa);
            this.panel2.Controls.Add(this.btnSua);
            this.panel2.Location = new System.Drawing.Point(-4, 503);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1045, 61);
            this.panel2.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel1.Controls.Add(this.lblSuaMon);
            this.panel1.Location = new System.Drawing.Point(-4, -3);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 52);
            this.panel1.TabIndex = 17;
            // 
            // picAnh
            // 
            this.picAnh.Location = new System.Drawing.Point(495, 75);
            this.picAnh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picAnh.Name = "picAnh";
            this.picAnh.Size = new System.Drawing.Size(147, 104);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAnh.TabIndex = 33;
            this.picAnh.TabStop = false;
            // 
            // SuaMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 561);
            this.Controls.Add(this.picAnh);
            this.Controls.Add(this.dgvDanhSachMon);
            this.Controls.Add(this.cboLoaiMon);
            this.Controls.Add(this.btnChonAnh);
            this.Controls.Add(this.txtAnh);
            this.Controls.Add(this.txtTonKho);
            this.Controls.Add(this.txtGiaNhap);
            this.Controls.Add(this.txtGiaBan);
            this.Controls.Add(this.txtTenMon);
            this.Controls.Add(this.lblAnh);
            this.Controls.Add(this.lblLoaimon);
            this.Controls.Add(this.lblTonkho);
            this.Controls.Add(this.lblGianhap);
            this.Controls.Add(this.lblGiaban);
            this.Controls.Add(this.lblTenmon);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SuaMon";
            this.Text = "SuaMon";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachMon)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picAnh;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.DataGridView dgvDanhSachMon;
        private System.Windows.Forms.Label lblSuaMon;
        private System.Windows.Forms.ComboBox cboLoaiMon;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.TextBox txtAnh;
        private System.Windows.Forms.TextBox txtTonKho;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.TextBox txtTenMon;
        private System.Windows.Forms.Label lblAnh;
        private System.Windows.Forms.Label lblLoaimon;
        private System.Windows.Forms.Label lblTonkho;
        private System.Windows.Forms.Label lblGianhap;
        private System.Windows.Forms.Label lblGiaban;
        private System.Windows.Forms.Label lblTenmon;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}