namespace QLquanGame
{
    partial class MainForm
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
            this.lblDangxuat = new System.Windows.Forms.Label();
            this.lblDatmay = new System.Windows.Forms.Label();
            this.lblBaotri = new System.Windows.Forms.Label();
            this.lblDatmon = new System.Windows.Forms.Label();
            this.lblHoadon = new System.Windows.Forms.Label();
            this.lblTrangChu = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel1.Controls.Add(this.lblDangxuat);
            this.panel1.Controls.Add(this.lblDatmay);
            this.panel1.Controls.Add(this.lblBaotri);
            this.panel1.Controls.Add(this.lblDatmon);
            this.panel1.Controls.Add(this.lblHoadon);
            this.panel1.Controls.Add(this.lblTrangChu);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1529, 53);
            this.panel1.TabIndex = 0;
            // 
            // lblDangxuat
            // 
            this.lblDangxuat.AutoSize = true;
            this.lblDangxuat.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangxuat.ForeColor = System.Drawing.Color.Firebrick;
            this.lblDangxuat.Location = new System.Drawing.Point(1072, 9);
            this.lblDangxuat.Name = "lblDangxuat";
            this.lblDangxuat.Size = new System.Drawing.Size(133, 23);
            this.lblDangxuat.TabIndex = 4;
            this.lblDangxuat.Text = "↪ Đăng xuất";
            this.lblDangxuat.Click += new System.EventHandler(this.lblDangxuat_Click);
            // 
            // lblDatmay
            // 
            this.lblDatmay.AutoSize = true;
            this.lblDatmay.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatmay.ForeColor = System.Drawing.Color.White;
            this.lblDatmay.Location = new System.Drawing.Point(259, 9);
            this.lblDatmay.Name = "lblDatmay";
            this.lblDatmay.Size = new System.Drawing.Size(119, 23);
            this.lblDatmay.TabIndex = 0;
            this.lblDatmay.Text = "🖥 Đặt máy";
            this.lblDatmay.Click += new System.EventHandler(this.lblDatmay_Click);
            // 
            // lblBaotri
            // 
            this.lblBaotri.AutoSize = true;
            this.lblBaotri.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaotri.ForeColor = System.Drawing.Color.Transparent;
            this.lblBaotri.Location = new System.Drawing.Point(877, 9);
            this.lblBaotri.Name = "lblBaotri";
            this.lblBaotri.Size = new System.Drawing.Size(119, 23);
            this.lblBaotri.TabIndex = 3;
            this.lblBaotri.Text = "⚙ Bảo trì";
            this.lblBaotri.Click += new System.EventHandler(this.lblBaotri_Click);
            // 
            // lblDatmon
            // 
            this.lblDatmon.AutoSize = true;
            this.lblDatmon.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatmon.ForeColor = System.Drawing.Color.Transparent;
            this.lblDatmon.Location = new System.Drawing.Point(483, 9);
            this.lblDatmon.Name = "lblDatmon";
            this.lblDatmon.Size = new System.Drawing.Size(119, 23);
            this.lblDatmon.TabIndex = 1;
            this.lblDatmon.Text = "🍜 Đặt món";
            this.lblDatmon.Click += new System.EventHandler(this.lblDatmon_Click);
            // 
            // lblHoadon
            // 
            this.lblHoadon.AutoSize = true;
            this.lblHoadon.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoadon.ForeColor = System.Drawing.Color.Transparent;
            this.lblHoadon.Location = new System.Drawing.Point(686, 9);
            this.lblHoadon.Name = "lblHoadon";
            this.lblHoadon.Size = new System.Drawing.Size(119, 23);
            this.lblHoadon.TabIndex = 2;
            this.lblHoadon.Text = "📜 Hóa đơn";
            this.lblHoadon.Click += new System.EventHandler(this.lblHoadon_Click);
            // 
            // lblTrangChu
            // 
            this.lblTrangChu.AutoSize = true;
            this.lblTrangChu.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangChu.ForeColor = System.Drawing.Color.White;
            this.lblTrangChu.Location = new System.Drawing.Point(58, 9);
            this.lblTrangChu.Name = "lblTrangChu";
            this.lblTrangChu.Size = new System.Drawing.Size(109, 23);
            this.lblTrangChu.TabIndex = 5;
            this.lblTrangChu.Text = "Trang chủ";
            this.lblTrangChu.Click += new System.EventHandler(this.lblTrangChu_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel2.BackgroundImage = global::QLquanGame.Properties.Resources._1671884892_idei_club_p_interer_kompyuternogo_kluba_dizain_pintere_79_1024x683;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lblWelcome);
            this.panel2.Location = new System.Drawing.Point(-1, 52);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1529, 711);
            this.panel2.TabIndex = 1;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(122, 54);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(0, 27);
            this.lblWelcome.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(1277, 763);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDangxuat;
        private System.Windows.Forms.Label lblBaotri;
        private System.Windows.Forms.Label lblHoadon;
        private System.Windows.Forms.Label lblDatmon;
        private System.Windows.Forms.Label lblDatmay;
        private System.Windows.Forms.Label lblTrangChu;
        private System.Windows.Forms.Label lblWelcome;
    }
}