namespace QLquanGame
{
    partial class FormThemMon
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblThemmon = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnXacnhanthem = new System.Windows.Forms.Button();
            this.txtAnh = new System.Windows.Forms.TextBox();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.txtTenMon = new System.Windows.Forms.TextBox();
            this.labAnh = new System.Windows.Forms.Label();
            this.lblLoaiMon = new System.Windows.Forms.Label();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.lblTenMon = new System.Windows.Forms.Label();
            this.picMon = new System.Windows.Forms.PictureBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.cbbLoaiMon = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMon)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel1.Controls.Add(this.lblThemmon);
            this.panel1.Location = new System.Drawing.Point(-7, -7);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 49);
            this.panel1.TabIndex = 14;
            // 
            // lblThemmon
            // 
            this.lblThemmon.AutoSize = true;
            this.lblThemmon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThemmon.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblThemmon.Location = new System.Drawing.Point(189, 13);
            this.lblThemmon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblThemmon.Name = "lblThemmon";
            this.lblThemmon.Size = new System.Drawing.Size(145, 20);
            this.lblThemmon.TabIndex = 0;
            this.lblThemmon.Text = "Form Thêm món";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel2.Controls.Add(this.btnXacnhanthem);
            this.panel2.Location = new System.Drawing.Point(-7, 364);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(549, 61);
            this.panel2.TabIndex = 24;
            // 
            // btnXacnhanthem
            // 
            this.btnXacnhanthem.Location = new System.Drawing.Point(405, 16);
            this.btnXacnhanthem.Margin = new System.Windows.Forms.Padding(2);
            this.btnXacnhanthem.Name = "btnXacnhanthem";
            this.btnXacnhanthem.Size = new System.Drawing.Size(75, 28);
            this.btnXacnhanthem.TabIndex = 0;
            this.btnXacnhanthem.Text = "Thêm";
            this.btnXacnhanthem.UseVisualStyleBackColor = true;
            this.btnXacnhanthem.Click += new System.EventHandler(this.btnXacnhanthem_Click);
            // 
            // txtAnh
            // 
            this.txtAnh.Location = new System.Drawing.Point(217, 236);
            this.txtAnh.Margin = new System.Windows.Forms.Padding(2);
            this.txtAnh.Name = "txtAnh";
            this.txtAnh.Size = new System.Drawing.Size(158, 22);
            this.txtAnh.TabIndex = 23;
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Location = new System.Drawing.Point(133, 162);
            this.txtGiaNhap.Margin = new System.Windows.Forms.Padding(2);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(226, 22);
            this.txtGiaNhap.TabIndex = 22;
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Location = new System.Drawing.Point(133, 121);
            this.txtGiaBan.Margin = new System.Windows.Forms.Padding(2);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(226, 22);
            this.txtGiaBan.TabIndex = 21;
            // 
            // txtTenMon
            // 
            this.txtTenMon.Location = new System.Drawing.Point(133, 79);
            this.txtTenMon.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.Size = new System.Drawing.Size(226, 22);
            this.txtTenMon.TabIndex = 20;
            // 
            // labAnh
            // 
            this.labAnh.AutoSize = true;
            this.labAnh.Location = new System.Drawing.Point(31, 234);
            this.labAnh.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labAnh.Name = "labAnh";
            this.labAnh.Size = new System.Drawing.Size(30, 16);
            this.labAnh.TabIndex = 19;
            this.labAnh.Text = "Ảnh";
            // 
            // lblLoaiMon
            // 
            this.lblLoaiMon.AutoSize = true;
            this.lblLoaiMon.Location = new System.Drawing.Point(22, 202);
            this.lblLoaiMon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLoaiMon.Name = "lblLoaiMon";
            this.lblLoaiMon.Size = new System.Drawing.Size(62, 16);
            this.lblLoaiMon.TabIndex = 18;
            this.lblLoaiMon.Text = "Loại món";
            // 
            // lblGiaNhap
            // 
            this.lblGiaNhap.AutoSize = true;
            this.lblGiaNhap.Location = new System.Drawing.Point(22, 162);
            this.lblGiaNhap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGiaNhap.Name = "lblGiaNhap";
            this.lblGiaNhap.Size = new System.Drawing.Size(61, 16);
            this.lblGiaNhap.TabIndex = 17;
            this.lblGiaNhap.Text = "Giá nhập";
            // 
            // lblGiaBan
            // 
            this.lblGiaBan.AutoSize = true;
            this.lblGiaBan.Location = new System.Drawing.Point(22, 121);
            this.lblGiaBan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGiaBan.Name = "lblGiaBan";
            this.lblGiaBan.Size = new System.Drawing.Size(54, 16);
            this.lblGiaBan.TabIndex = 16;
            this.lblGiaBan.Text = "Giá bán";
            // 
            // lblTenMon
            // 
            this.lblTenMon.AutoSize = true;
            this.lblTenMon.Location = new System.Drawing.Point(22, 80);
            this.lblTenMon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTenMon.Name = "lblTenMon";
            this.lblTenMon.Size = new System.Drawing.Size(60, 16);
            this.lblTenMon.TabIndex = 15;
            this.lblTenMon.Text = "Tên món";
            // 
            // picMon
            // 
            this.picMon.Location = new System.Drawing.Point(217, 259);
            this.picMon.Margin = new System.Windows.Forms.Padding(2);
            this.picMon.Name = "picMon";
            this.picMon.Size = new System.Drawing.Size(147, 97);
            this.picMon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMon.TabIndex = 27;
            this.picMon.TabStop = false;
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.Location = new System.Drawing.Point(133, 234);
            this.btnChonAnh.Margin = new System.Windows.Forms.Padding(2);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(81, 23);
            this.btnChonAnh.TabIndex = 26;
            this.btnChonAnh.Text = "Chọn Ảnh";
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // cbbLoaiMon
            // 
            this.cbbLoaiMon.FormattingEnabled = true;
            this.cbbLoaiMon.Location = new System.Drawing.Point(133, 200);
            this.cbbLoaiMon.Margin = new System.Windows.Forms.Padding(2);
            this.cbbLoaiMon.Name = "cbbLoaiMon";
            this.cbbLoaiMon.Size = new System.Drawing.Size(226, 24);
            this.cbbLoaiMon.TabIndex = 25;
            this.cbbLoaiMon.SelectedIndexChanged += new System.EventHandler(this.cbbLoaiMon_SelectedIndexChanged);
            // 
            // FormThemMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 418);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtAnh);
            this.Controls.Add(this.txtGiaNhap);
            this.Controls.Add(this.txtGiaBan);
            this.Controls.Add(this.txtTenMon);
            this.Controls.Add(this.labAnh);
            this.Controls.Add(this.lblLoaiMon);
            this.Controls.Add(this.lblGiaNhap);
            this.Controls.Add(this.lblGiaBan);
            this.Controls.Add(this.lblTenMon);
            this.Controls.Add(this.picMon);
            this.Controls.Add(this.btnChonAnh);
            this.Controls.Add(this.cbbLoaiMon);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormThemMon";
            this.Text = "ThemMon";
            this.Load += new System.EventHandler(this.FormThemMon_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblThemmon;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnXacnhanthem;
        private System.Windows.Forms.TextBox txtAnh;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.TextBox txtTenMon;
        private System.Windows.Forms.Label labAnh;
        private System.Windows.Forms.Label lblLoaiMon;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.Label lblTenMon;
        private System.Windows.Forms.PictureBox picMon;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.ComboBox cbbLoaiMon;
    }
}