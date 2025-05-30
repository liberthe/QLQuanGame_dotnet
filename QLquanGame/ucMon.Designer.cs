namespace QLquanGame
{
    partial class ucMon
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnMon = new System.Windows.Forms.Panel();
            this.lblGia = new System.Windows.Forms.Label();
            this.lblTenMon = new System.Windows.Forms.Label();
            this.picAnhMon = new System.Windows.Forms.PictureBox();
            this.pnMon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhMon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnMon
            // 
            this.pnMon.Controls.Add(this.lblGia);
            this.pnMon.Controls.Add(this.lblTenMon);
            this.pnMon.Controls.Add(this.picAnhMon);
            this.pnMon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMon.Location = new System.Drawing.Point(0, 0);
            this.pnMon.Margin = new System.Windows.Forms.Padding(2);
            this.pnMon.Name = "pnMon";
            this.pnMon.Size = new System.Drawing.Size(227, 212);
            this.pnMon.TabIndex = 2;
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGia.Location = new System.Drawing.Point(80, 182);
            this.lblGia.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(52, 17);
            this.lblGia.TabIndex = 2;
            this.lblGia.Text = "label1";
            // 
            // lblTenMon
            // 
            this.lblTenMon.AutoSize = true;
            this.lblTenMon.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenMon.Location = new System.Drawing.Point(20, 19);
            this.lblTenMon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTenMon.Name = "lblTenMon";
            this.lblTenMon.Size = new System.Drawing.Size(52, 17);
            this.lblTenMon.TabIndex = 1;
            this.lblTenMon.Text = "label1";
            // 
            // picAnhMon
            // 
            this.picAnhMon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picAnhMon.Location = new System.Drawing.Point(12, 19);
            this.picAnhMon.Margin = new System.Windows.Forms.Padding(2);
            this.picAnhMon.Name = "picAnhMon";
            this.picAnhMon.Size = new System.Drawing.Size(195, 161);
            this.picAnhMon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnhMon.TabIndex = 0;
            this.picAnhMon.TabStop = false;
            // 
            // ucMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnMon);
            this.Name = "ucMon";
            this.Size = new System.Drawing.Size(227, 212);
            this.pnMon.ResumeLayout(false);
            this.pnMon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhMon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMon;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.Label lblTenMon;
        private System.Windows.Forms.PictureBox picAnhMon;
    }
}
