namespace UKPI.Presentation
{
    partial class frmNhapKhoChiTiet
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTenHang = new System.Windows.Forms.Label();
            this.txtTenHang = new System.Windows.Forms.TextBox();
            this.lblMaHeThong = new System.Windows.Forms.Label();
            this.txtMaHeThong = new System.Windows.Forms.TextBox();
            this.lblMaHang = new System.Windows.Forms.Label();
            this.txtMaThuocYTe = new System.Windows.Forms.TextBox();
            this.lblDVT = new System.Windows.Forms.Label();
            this.txtDVT = new System.Windows.Forms.TextBox();
            this.lblThanhPhan = new System.Windows.Forms.Label();
            this.txtThanhPhan = new System.Windows.Forms.TextBox();
            this.lblHamLuong = new System.Windows.Forms.Label();
            this.txtHamLuong = new System.Windows.Forms.TextBox();
            this.lblHeSoAnToan = new System.Windows.Forms.Label();
            this.txtHeSoAnToan = new System.Windows.Forms.TextBox();
            this.txtNhaSanXuat = new System.Windows.Forms.TextBox();
            this.lblNhaSanXuat = new System.Windows.Forms.Label();
            this.lblQuocGia = new System.Windows.Forms.Label();
            this.txtQuocGia = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblTongTagDaDoc = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnXoaTag = new System.Windows.Forms.Button();
            this.grdChiTietNhap = new UKPI.Controls.DataGridView_RowNum();
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenReader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdChiTietNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTenHang
            // 
            this.lblTenHang.AutoSize = true;
            this.lblTenHang.Location = new System.Drawing.Point(30, 61);
            this.lblTenHang.Name = "lblTenHang";
            this.lblTenHang.Size = new System.Drawing.Size(55, 13);
            this.lblTenHang.TabIndex = 44;
            this.lblTenHang.Text = "Tên Hàng";
            // 
            // txtTenHang
            // 
            this.txtTenHang.BackColor = System.Drawing.Color.White;
            this.txtTenHang.Location = new System.Drawing.Point(91, 61);
            this.txtTenHang.Name = "txtTenHang";
            this.txtTenHang.ReadOnly = true;
            this.txtTenHang.Size = new System.Drawing.Size(208, 20);
            this.txtTenHang.TabIndex = 45;
            // 
            // lblMaHeThong
            // 
            this.lblMaHeThong.AutoSize = true;
            this.lblMaHeThong.Location = new System.Drawing.Point(12, 9);
            this.lblMaHeThong.Name = "lblMaHeThong";
            this.lblMaHeThong.Size = new System.Drawing.Size(73, 13);
            this.lblMaHeThong.TabIndex = 46;
            this.lblMaHeThong.Text = "Mã Hệ Thống";
            // 
            // txtMaHeThong
            // 
            this.txtMaHeThong.BackColor = System.Drawing.Color.White;
            this.txtMaHeThong.Location = new System.Drawing.Point(91, 9);
            this.txtMaHeThong.Name = "txtMaHeThong";
            this.txtMaHeThong.ReadOnly = true;
            this.txtMaHeThong.Size = new System.Drawing.Size(208, 20);
            this.txtMaHeThong.TabIndex = 47;
            // 
            // lblMaHang
            // 
            this.lblMaHang.AutoSize = true;
            this.lblMaHang.Location = new System.Drawing.Point(34, 35);
            this.lblMaHang.Name = "lblMaHang";
            this.lblMaHang.Size = new System.Drawing.Size(51, 13);
            this.lblMaHang.TabIndex = 48;
            this.lblMaHang.Text = "Mã Hàng";
            // 
            // txtMaThuocYTe
            // 
            this.txtMaThuocYTe.BackColor = System.Drawing.Color.White;
            this.txtMaThuocYTe.Location = new System.Drawing.Point(91, 35);
            this.txtMaThuocYTe.Name = "txtMaThuocYTe";
            this.txtMaThuocYTe.ReadOnly = true;
            this.txtMaThuocYTe.Size = new System.Drawing.Size(208, 20);
            this.txtMaThuocYTe.TabIndex = 49;
            // 
            // lblDVT
            // 
            this.lblDVT.AutoSize = true;
            this.lblDVT.Location = new System.Drawing.Point(332, 9);
            this.lblDVT.Name = "lblDVT";
            this.lblDVT.Size = new System.Drawing.Size(65, 13);
            this.lblDVT.TabIndex = 50;
            this.lblDVT.Text = "Đơn Vị Tính";
            // 
            // txtDVT
            // 
            this.txtDVT.BackColor = System.Drawing.Color.White;
            this.txtDVT.Location = new System.Drawing.Point(403, 6);
            this.txtDVT.Name = "txtDVT";
            this.txtDVT.ReadOnly = true;
            this.txtDVT.Size = new System.Drawing.Size(208, 20);
            this.txtDVT.TabIndex = 51;
            // 
            // lblThanhPhan
            // 
            this.lblThanhPhan.AutoSize = true;
            this.lblThanhPhan.Location = new System.Drawing.Point(332, 38);
            this.lblThanhPhan.Name = "lblThanhPhan";
            this.lblThanhPhan.Size = new System.Drawing.Size(66, 13);
            this.lblThanhPhan.TabIndex = 52;
            this.lblThanhPhan.Text = "Thành Phần";
            // 
            // txtThanhPhan
            // 
            this.txtThanhPhan.BackColor = System.Drawing.Color.White;
            this.txtThanhPhan.Location = new System.Drawing.Point(403, 35);
            this.txtThanhPhan.Name = "txtThanhPhan";
            this.txtThanhPhan.ReadOnly = true;
            this.txtThanhPhan.Size = new System.Drawing.Size(208, 20);
            this.txtThanhPhan.TabIndex = 53;
            // 
            // lblHamLuong
            // 
            this.lblHamLuong.AutoSize = true;
            this.lblHamLuong.Location = new System.Drawing.Point(332, 68);
            this.lblHamLuong.Name = "lblHamLuong";
            this.lblHamLuong.Size = new System.Drawing.Size(62, 13);
            this.lblHamLuong.TabIndex = 54;
            this.lblHamLuong.Text = "Hàm Lượng";
            // 
            // txtHamLuong
            // 
            this.txtHamLuong.BackColor = System.Drawing.Color.White;
            this.txtHamLuong.Location = new System.Drawing.Point(403, 65);
            this.txtHamLuong.Name = "txtHamLuong";
            this.txtHamLuong.ReadOnly = true;
            this.txtHamLuong.Size = new System.Drawing.Size(208, 20);
            this.txtHamLuong.TabIndex = 55;
            // 
            // lblHeSoAnToan
            // 
            this.lblHeSoAnToan.AutoSize = true;
            this.lblHeSoAnToan.Location = new System.Drawing.Point(636, 9);
            this.lblHeSoAnToan.Name = "lblHeSoAnToan";
            this.lblHeSoAnToan.Size = new System.Drawing.Size(81, 13);
            this.lblHeSoAnToan.TabIndex = 56;
            this.lblHeSoAnToan.Text = "Hệ Số An Toàn";
            // 
            // txtHeSoAnToan
            // 
            this.txtHeSoAnToan.BackColor = System.Drawing.Color.White;
            this.txtHeSoAnToan.Location = new System.Drawing.Point(723, 6);
            this.txtHeSoAnToan.Name = "txtHeSoAnToan";
            this.txtHeSoAnToan.ReadOnly = true;
            this.txtHeSoAnToan.Size = new System.Drawing.Size(208, 20);
            this.txtHeSoAnToan.TabIndex = 57;
            // 
            // txtNhaSanXuat
            // 
            this.txtNhaSanXuat.BackColor = System.Drawing.Color.White;
            this.txtNhaSanXuat.Location = new System.Drawing.Point(723, 35);
            this.txtNhaSanXuat.Name = "txtNhaSanXuat";
            this.txtNhaSanXuat.ReadOnly = true;
            this.txtNhaSanXuat.Size = new System.Drawing.Size(208, 20);
            this.txtNhaSanXuat.TabIndex = 59;
            // 
            // lblNhaSanXuat
            // 
            this.lblNhaSanXuat.AutoSize = true;
            this.lblNhaSanXuat.Location = new System.Drawing.Point(643, 38);
            this.lblNhaSanXuat.Name = "lblNhaSanXuat";
            this.lblNhaSanXuat.Size = new System.Drawing.Size(74, 13);
            this.lblNhaSanXuat.TabIndex = 58;
            this.lblNhaSanXuat.Text = "Nhà Sản Xuất";
            // 
            // lblQuocGia
            // 
            this.lblQuocGia.AutoSize = true;
            this.lblQuocGia.Location = new System.Drawing.Point(665, 68);
            this.lblQuocGia.Name = "lblQuocGia";
            this.lblQuocGia.Size = new System.Drawing.Size(52, 13);
            this.lblQuocGia.TabIndex = 60;
            this.lblQuocGia.Text = "Quốc Gia";
            // 
            // txtQuocGia
            // 
            this.txtQuocGia.BackColor = System.Drawing.Color.White;
            this.txtQuocGia.Location = new System.Drawing.Point(723, 65);
            this.txtQuocGia.Name = "txtQuocGia";
            this.txtQuocGia.Size = new System.Drawing.Size(208, 20);
            this.txtQuocGia.TabIndex = 61;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(750, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 63;
            this.button1.Text = "Emulator";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCount
            // 
            this.txtCount.BackColor = System.Drawing.Color.White;
            this.txtCount.Location = new System.Drawing.Point(109, 112);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(73, 20);
            this.txtCount.TabIndex = 64;
            // 
            // lblTongTagDaDoc
            // 
            this.lblTongTagDaDoc.AutoSize = true;
            this.lblTongTagDaDoc.Location = new System.Drawing.Point(12, 115);
            this.lblTongTagDaDoc.Name = "lblTongTagDaDoc";
            this.lblTongTagDaDoc.Size = new System.Drawing.Size(94, 13);
            this.lblTongTagDaDoc.TabIndex = 65;
            this.lblTongTagDaDoc.Text = "Tổng Tag Đã Đọc";
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Image = global::UKPI.Properties.Resources.save_as3;
            this.btnXacNhan.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnXacNhan.Location = new System.Drawing.Point(375, 105);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(95, 23);
            this.btnXacNhan.TabIndex = 66;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnXoaTag
            // 
            this.btnXoaTag.Image = global::UKPI.Properties.Resources.cancel_4;
            this.btnXoaTag.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnXoaTag.Location = new System.Drawing.Point(263, 105);
            this.btnXoaTag.Name = "btnXoaTag";
            this.btnXoaTag.Size = new System.Drawing.Size(106, 23);
            this.btnXoaTag.TabIndex = 67;
            this.btnXoaTag.Text = "Xóa dòng";
            this.btnXoaTag.UseVisualStyleBackColor = true;
            this.btnXoaTag.Click += new System.EventHandler(this.btnXoaTag_Click);
            // 
            // grdChiTietNhap
            // 
            this.grdChiTietNhap.AllowUserToAddRows = false;
            this.grdChiTietNhap.AllowUserToDeleteRows = false;
            this.grdChiTietNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdChiTietNhap.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdChiTietNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdChiTietNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdChiTietNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBox,
            this.STT,
            this.maTag,
            this.soLuong,
            this.tenReader,
            this.thoiGian});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdChiTietNhap.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdChiTietNhap.Location = new System.Drawing.Point(12, 138);
            this.grdChiTietNhap.Name = "grdChiTietNhap";
            this.grdChiTietNhap.RowHeadersWidth = 39;
            this.grdChiTietNhap.Size = new System.Drawing.Size(915, 175);
            this.grdChiTietNhap.TabIndex = 62;
            // 
            // CheckBox
            // 
            this.CheckBox.HeaderText = "";
            this.CheckBox.Name = "CheckBox";
            // 
            // STT
            // 
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            // 
            // maTag
            // 
            this.maTag.HeaderText = "Mã Tag RFID";
            this.maTag.Name = "maTag";
            this.maTag.Width = 300;
            // 
            // soLuong
            // 
            this.soLuong.HeaderText = "Số Lượng";
            this.soLuong.Name = "soLuong";
            // 
            // tenReader
            // 
            this.tenReader.HeaderText = "Tên Reader";
            this.tenReader.Name = "tenReader";
            // 
            // thoiGian
            // 
            this.thoiGian.HeaderText = "Thời Gian";
            this.thoiGian.Name = "thoiGian";
            // 
            // frmNhapKhoChiTiet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 333);
            this.Controls.Add(this.btnXoaTag);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.lblTongTagDaDoc);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grdChiTietNhap);
            this.Controls.Add(this.txtQuocGia);
            this.Controls.Add(this.lblQuocGia);
            this.Controls.Add(this.txtNhaSanXuat);
            this.Controls.Add(this.lblNhaSanXuat);
            this.Controls.Add(this.txtHeSoAnToan);
            this.Controls.Add(this.lblHeSoAnToan);
            this.Controls.Add(this.txtHamLuong);
            this.Controls.Add(this.lblHamLuong);
            this.Controls.Add(this.txtThanhPhan);
            this.Controls.Add(this.lblThanhPhan);
            this.Controls.Add(this.txtDVT);
            this.Controls.Add(this.lblDVT);
            this.Controls.Add(this.txtMaThuocYTe);
            this.Controls.Add(this.lblMaHang);
            this.Controls.Add(this.txtMaHeThong);
            this.Controls.Add(this.lblMaHeThong);
            this.Controls.Add(this.txtTenHang);
            this.Controls.Add(this.lblTenHang);
            this.Name = "frmNhapKhoChiTiet";
            this.Text = "frmNhapKhoChiTiet";
            ((System.ComponentModel.ISupportInitialize)(this.grdChiTietNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTenHang;
        private System.Windows.Forms.TextBox txtTenHang;
        private System.Windows.Forms.Label lblMaHeThong;
        private System.Windows.Forms.TextBox txtMaHeThong;
        private System.Windows.Forms.Label lblMaHang;
        private System.Windows.Forms.TextBox txtMaThuocYTe;
        private System.Windows.Forms.Label lblDVT;
        private System.Windows.Forms.TextBox txtDVT;
        private System.Windows.Forms.Label lblThanhPhan;
        private System.Windows.Forms.TextBox txtThanhPhan;
        private System.Windows.Forms.Label lblHamLuong;
        private System.Windows.Forms.TextBox txtHamLuong;
        private System.Windows.Forms.Label lblHeSoAnToan;
        private System.Windows.Forms.TextBox txtHeSoAnToan;
        private System.Windows.Forms.TextBox txtNhaSanXuat;
        private System.Windows.Forms.Label lblNhaSanXuat;
        private System.Windows.Forms.Label lblQuocGia;
        private System.Windows.Forms.TextBox txtQuocGia;
        private Controls.DataGridView_RowNum grdChiTietNhap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lblTongTagDaDoc;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn maTag;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenReader;
        private System.Windows.Forms.DataGridViewTextBoxColumn thoiGian;
        private System.Windows.Forms.Button btnXoaTag;
    }
}