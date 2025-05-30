namespace QLquanGame
{
    partial class DatMon
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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.flowDanhMuc = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDoan = new System.Windows.Forms.Button();
            this.btnSnack = new System.Windows.Forms.Button();
            this.btnDouong = new System.Windows.Forms.Button();
            this.btnThemloai = new System.Windows.Forms.Button();
            this.txtTimkiem = new System.Windows.Forms.TextBox();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.flpMonAn = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlfooter = new System.Windows.Forms.Panel();
            this.btnXoamon = new System.Windows.Forms.Button();
            this.lblthanhtien = new System.Windows.Forms.Label();
            this.lblTongtien = new System.Windows.Forms.Label();
            this.btnXacnhan = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.ppnlHeader = new System.Windows.Forms.Panel();
            this.cboChonmay = new System.Windows.Forms.ComboBox();
            this.cboZone = new System.Windows.Forms.ComboBox();
            this.lblChonmay = new System.Windows.Forms.Label();
            this.lblChonzone = new System.Windows.Forms.Label();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThemmon = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            this.flowDanhMuc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.pnlfooter.SuspendLayout();
            this.ppnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.flowDanhMuc);
            this.panelContainer.Controls.Add(this.btnThemloai);
            this.panelContainer.Controls.Add(this.txtTimkiem);
            this.panelContainer.Controls.Add(this.dgvGioHang);
            this.panelContainer.Controls.Add(this.flpMonAn);
            this.panelContainer.Controls.Add(this.pnlfooter);
            this.panelContainer.Controls.Add(this.ppnlHeader);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(2);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1238, 629);
            this.panelContainer.TabIndex = 0;
            // 
            // flowDanhMuc
            // 
            this.flowDanhMuc.AutoScroll = true;
            this.flowDanhMuc.Controls.Add(this.btnDoan);
            this.flowDanhMuc.Controls.Add(this.btnSnack);
            this.flowDanhMuc.Controls.Add(this.btnDouong);
            this.flowDanhMuc.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowDanhMuc.Location = new System.Drawing.Point(34, 182);
            this.flowDanhMuc.Margin = new System.Windows.Forms.Padding(2);
            this.flowDanhMuc.Name = "flowDanhMuc";
            this.flowDanhMuc.Size = new System.Drawing.Size(82, 249);
            this.flowDanhMuc.TabIndex = 25;
            // 
            // btnDoan
            // 
            this.btnDoan.Location = new System.Drawing.Point(2, 2);
            this.btnDoan.Margin = new System.Windows.Forms.Padding(2);
            this.btnDoan.Name = "btnDoan";
            this.btnDoan.Size = new System.Drawing.Size(67, 36);
            this.btnDoan.TabIndex = 5;
            this.btnDoan.Text = "Đồ ăn";
            this.btnDoan.UseVisualStyleBackColor = true;
            // 
            // btnSnack
            // 
            this.btnSnack.Location = new System.Drawing.Point(2, 42);
            this.btnSnack.Margin = new System.Windows.Forms.Padding(2);
            this.btnSnack.Name = "btnSnack";
            this.btnSnack.Size = new System.Drawing.Size(67, 34);
            this.btnSnack.TabIndex = 3;
            this.btnSnack.Text = "Snack";
            this.btnSnack.UseVisualStyleBackColor = true;
            // 
            // btnDouong
            // 
            this.btnDouong.Location = new System.Drawing.Point(2, 80);
            this.btnDouong.Margin = new System.Windows.Forms.Padding(2);
            this.btnDouong.Name = "btnDouong";
            this.btnDouong.Size = new System.Drawing.Size(67, 38);
            this.btnDouong.TabIndex = 4;
            this.btnDouong.Text = "Đồ uống";
            this.btnDouong.UseVisualStyleBackColor = true;
            // 
            // btnThemloai
            // 
            this.btnThemloai.Location = new System.Drawing.Point(46, 435);
            this.btnThemloai.Margin = new System.Windows.Forms.Padding(2);
            this.btnThemloai.Name = "btnThemloai";
            this.btnThemloai.Size = new System.Drawing.Size(57, 29);
            this.btnThemloai.TabIndex = 24;
            this.btnThemloai.Text = "Thêm ";
            this.btnThemloai.UseVisualStyleBackColor = true;
            this.btnThemloai.Click += new System.EventHandler(this.btnThemloai_Click);
            // 
            // txtTimkiem
            // 
            this.txtTimkiem.Location = new System.Drawing.Point(156, 91);
            this.txtTimkiem.Margin = new System.Windows.Forms.Padding(2);
            this.txtTimkiem.Name = "txtTimkiem";
            this.txtTimkiem.Size = new System.Drawing.Size(292, 22);
            this.txtTimkiem.TabIndex = 23;
            this.txtTimkiem.Text = "Tìm kiếm";
            this.txtTimkiem.TextChanged += new System.EventHandler(this.txtTimkiem_TextChanged);
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Location = new System.Drawing.Point(569, 91);
            this.dgvGioHang.Margin = new System.Windows.Forms.Padding(2);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.RowHeadersWidth = 82;
            this.dgvGioHang.RowTemplate.Height = 33;
            this.dgvGioHang.Size = new System.Drawing.Size(649, 453);
            this.dgvGioHang.TabIndex = 21;
            // 
            // flpMonAn
            // 
            this.flpMonAn.AutoScroll = true;
            this.flpMonAn.Location = new System.Drawing.Point(152, 120);
            this.flpMonAn.Margin = new System.Windows.Forms.Padding(2);
            this.flpMonAn.Name = "flpMonAn";
            this.flpMonAn.Size = new System.Drawing.Size(296, 424);
            this.flpMonAn.TabIndex = 22;
            // 
            // pnlfooter
            // 
            this.pnlfooter.BackColor = System.Drawing.SystemColors.Desktop;
            this.pnlfooter.Controls.Add(this.btnXoamon);
            this.pnlfooter.Controls.Add(this.lblthanhtien);
            this.pnlfooter.Controls.Add(this.lblTongtien);
            this.pnlfooter.Controls.Add(this.btnXacnhan);
            this.pnlfooter.Controls.Add(this.btnReset);
            this.pnlfooter.Location = new System.Drawing.Point(5, 561);
            this.pnlfooter.Margin = new System.Windows.Forms.Padding(2);
            this.pnlfooter.Name = "pnlfooter";
            this.pnlfooter.Size = new System.Drawing.Size(1550, 76);
            this.pnlfooter.TabIndex = 20;
            // 
            // btnXoamon
            // 
            this.btnXoamon.Location = new System.Drawing.Point(686, 13);
            this.btnXoamon.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoamon.Name = "btnXoamon";
            this.btnXoamon.Size = new System.Drawing.Size(85, 38);
            this.btnXoamon.TabIndex = 4;
            this.btnXoamon.Text = "Xóa";
            this.btnXoamon.UseVisualStyleBackColor = true;
            this.btnXoamon.Click += new System.EventHandler(this.btnXoamon_Click);
            // 
            // lblthanhtien
            // 
            this.lblthanhtien.AutoSize = true;
            this.lblthanhtien.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblthanhtien.Location = new System.Drawing.Point(1271, 24);
            this.lblthanhtien.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblthanhtien.Name = "lblthanhtien";
            this.lblthanhtien.Size = new System.Drawing.Size(14, 16);
            this.lblthanhtien.TabIndex = 3;
            this.lblthanhtien.Text = "0";
            // 
            // lblTongtien
            // 
            this.lblTongtien.AutoSize = true;
            this.lblTongtien.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTongtien.Location = new System.Drawing.Point(1058, 24);
            this.lblTongtien.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongtien.Name = "lblTongtien";
            this.lblTongtien.Size = new System.Drawing.Size(63, 16);
            this.lblTongtien.TabIndex = 2;
            this.lblTongtien.Text = "Tổng tiền";
            // 
            // btnXacnhan
            // 
            this.btnXacnhan.Location = new System.Drawing.Point(946, 13);
            this.btnXacnhan.Margin = new System.Windows.Forms.Padding(2);
            this.btnXacnhan.Name = "btnXacnhan";
            this.btnXacnhan.Size = new System.Drawing.Size(85, 38);
            this.btnXacnhan.TabIndex = 1;
            this.btnXacnhan.Text = "Xác nhận";
            this.btnXacnhan.UseVisualStyleBackColor = true;
            this.btnXacnhan.Click += new System.EventHandler(this.btnXacnhan_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(821, 13);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(85, 38);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ppnlHeader
            // 
            this.ppnlHeader.BackColor = System.Drawing.SystemColors.Desktop;
            this.ppnlHeader.Controls.Add(this.cboChonmay);
            this.ppnlHeader.Controls.Add(this.cboZone);
            this.ppnlHeader.Controls.Add(this.lblChonmay);
            this.ppnlHeader.Controls.Add(this.lblChonzone);
            this.ppnlHeader.Controls.Add(this.btnSua);
            this.ppnlHeader.Controls.Add(this.btnThemmon);
            this.ppnlHeader.Controls.Add(this.lblTitle);
            this.ppnlHeader.Location = new System.Drawing.Point(5, -4);
            this.ppnlHeader.Margin = new System.Windows.Forms.Padding(2);
            this.ppnlHeader.Name = "ppnlHeader";
            this.ppnlHeader.Size = new System.Drawing.Size(1551, 77);
            this.ppnlHeader.TabIndex = 19;
            // 
            // cboChonmay
            // 
            this.cboChonmay.FormattingEnabled = true;
            this.cboChonmay.Location = new System.Drawing.Point(1040, 29);
            this.cboChonmay.Margin = new System.Windows.Forms.Padding(2);
            this.cboChonmay.Name = "cboChonmay";
            this.cboChonmay.Size = new System.Drawing.Size(173, 24);
            this.cboChonmay.TabIndex = 6;
            this.cboChonmay.SelectedIndexChanged += new System.EventHandler(this.cboChonmay_SelectedIndexChanged);
            // 
            // cboZone
            // 
            this.cboZone.FormattingEnabled = true;
            this.cboZone.Location = new System.Drawing.Point(661, 30);
            this.cboZone.Margin = new System.Windows.Forms.Padding(2);
            this.cboZone.Name = "cboZone";
            this.cboZone.Size = new System.Drawing.Size(166, 24);
            this.cboZone.TabIndex = 5;
            this.cboZone.SelectedIndexChanged += new System.EventHandler(this.cboZone_SelectedIndexChanged);
            // 
            // lblChonmay
            // 
            this.lblChonmay.AutoSize = true;
            this.lblChonmay.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblChonmay.Location = new System.Drawing.Point(943, 33);
            this.lblChonmay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChonmay.Name = "lblChonmay";
            this.lblChonmay.Size = new System.Drawing.Size(67, 16);
            this.lblChonmay.TabIndex = 4;
            this.lblChonmay.Text = "Chọn máy";
            // 
            // lblChonzone
            // 
            this.lblChonzone.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblChonzone.Location = new System.Drawing.Point(560, 32);
            this.lblChonzone.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChonzone.Name = "lblChonzone";
            this.lblChonzone.Size = new System.Drawing.Size(88, 22);
            this.lblChonzone.TabIndex = 3;
            this.lblChonzone.Text = "Chọn Zone";
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(341, 20);
            this.btnSua.Margin = new System.Windows.Forms.Padding(2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(89, 34);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click_1);
            // 
            // btnThemmon
            // 
            this.btnThemmon.Location = new System.Drawing.Point(227, 21);
            this.btnThemmon.Margin = new System.Windows.Forms.Padding(2);
            this.btnThemmon.Name = "btnThemmon";
            this.btnThemmon.Size = new System.Drawing.Size(90, 34);
            this.btnThemmon.TabIndex = 1;
            this.btnThemmon.Text = "Thêm món";
            this.btnThemmon.UseVisualStyleBackColor = true;
            this.btnThemmon.Click += new System.EventHandler(this.btnThemmon_Click_1);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.SystemColors.Info;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTitle.Location = new System.Drawing.Point(24, 24);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(167, 29);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "MENU ĐỒ ĂN";
            // 
            // DatMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 629);
            this.Controls.Add(this.panelContainer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DatMon";
            this.Text = "DatMon";
            this.Load += new System.EventHandler(this.DatMon_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.flowDanhMuc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.pnlfooter.ResumeLayout(false);
            this.pnlfooter.PerformLayout();
            this.ppnlHeader.ResumeLayout(false);
            this.ppnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.FlowLayoutPanel flowDanhMuc;
        private System.Windows.Forms.Button btnDoan;
        private System.Windows.Forms.Button btnSnack;
        private System.Windows.Forms.Button btnDouong;
        private System.Windows.Forms.Button btnThemloai;
        private System.Windows.Forms.TextBox txtTimkiem;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.FlowLayoutPanel flpMonAn;
        private System.Windows.Forms.Panel pnlfooter;
        private System.Windows.Forms.Button btnXoamon;
        private System.Windows.Forms.Label lblthanhtien;
        private System.Windows.Forms.Label lblTongtien;
        private System.Windows.Forms.Button btnXacnhan;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel ppnlHeader;
        private System.Windows.Forms.ComboBox cboChonmay;
        private System.Windows.Forms.ComboBox cboZone;
        private System.Windows.Forms.Label lblChonmay;
        private System.Windows.Forms.Label lblChonzone;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThemmon;
        private System.Windows.Forms.Label lblTitle;
    }
}