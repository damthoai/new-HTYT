using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UKPI.BusinessObject;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;

namespace UKPI.Presentation
{
    public partial class frmDanhMucHangHoa : Form
    {
       // private readonly QuanLyThuocDao _quanLyThuocDao = new QuanLyThuocDao();
        private readonly QuanLyDanhMucHangHoaDao _QuanLyDanhMucHangHoaDao = new QuanLyDanhMucHangHoaDao();
        private List<ThongTinThuoc> listThuoc = new List<ThongTinThuoc>();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        DataGridViewCell currentCell;
        int currentRowIndex = -1;
        private ThongTinThuoc selectedThuoc;
        public frmDanhMucHangHoa()
        {
            InitializeComponent();
            SetDefauldValue();
            this.Text = "DANH MỤC HÀNG HÓA";
            this.AutoScroll = true;
        }

        private void SetDefauldValue()
        {
            //    BuildGridViewRow();
            grdToaThuoc.AutoGenerateColumns = false;
            grdToaThuoc.CellDoubleClick += grdToaThuoc_CellDoubleClick;
            btnUpdate.Enabled = false;

            cbbDonViTinh.DataSource = _shareEntityDao.LoadDonViTinh();
            txtNhaSanXuat.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtNhaSanXuat.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection nsxDataCollection = new AutoCompleteStringCollection();
            //addNhaSanXuatItems(nsxDataCollection);
            txtNhaSanXuat.AutoCompleteCustomSource = nsxDataCollection;

            txtQuocGia.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtQuocGia.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection qgDataCollection = new AutoCompleteStringCollection();
            //addQUocGiaItems(qgDataCollection);
            txtQuocGia.AutoCompleteCustomSource = qgDataCollection;



            txtNhomThuoc.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtNhomThuoc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection nhomThuocDataCollection = new AutoCompleteStringCollection();
            //addNhomThuocItems(nhomThuocDataCollection);
            txtNhomThuoc.AutoCompleteCustomSource = nhomThuocDataCollection;

            LoadDanhMucHangHoa(string.Empty, string.Empty);

            dtpNgayHieuLuc.Format = DateTimePickerFormat.Custom;
            dtpNgayHieuLuc.CustomFormat = "dd/MM/yyyy";
            dtpNgayHieuLuc.Value = DateTime.Now;
            dtpNgayHieuLuc.Enabled = true;
            ckbChonNgayHieuLuc.Checked = true;
            cbHoatDong.Checked = true;
           // LoadCachDung();

        }

        /*private void LoadDanhMucThuoc(string maThuocYTe, string tenThuoc)
        {
            // ThongTinBenhNhan ttNhanVien = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
            listThuoc = _quanLyThuocDao.LoadDanhMucThuoc(maThuocYTe, tenThuoc);
            grdToaThuoc.DataSource = listThuoc;
        }*/

        private void LoadDanhMucHangHoa(string ProductID, string ProductName)
        {
            // ThongTinBenhNhan ttNhanVien = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
            listThuoc = _QuanLyDanhMucHangHoaDao.LoadDanhMucHangHoa(ProductID, ProductName);            
            grdToaThuoc.DataSource = listThuoc;
            //grdToaThuoc.Rows.Add(new object[] { ProductID.ToString(), "1", "12000000000000000000", "1", "RFID Reader 1", string.Format("{0:HH:mm:ss tt}", DateTime.Now) });

        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadDanhMucHangHoa(txtsMaThuoc.Text, txtsTenThuoc.Text);
        }

        private bool CheckThongTinHeSoAnToan()
        {
            bool result = true;
            try
            {
                int heSoAnToan = int.Parse(txtHeSoAnToan.Text);
                if (heSoAnToan <= 0)
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private ThongTinThuoc BuildThongTinThuoc(bool isNew)
        {
            ThongTinThuoc thongTinThuoc = new ThongTinThuoc();
            if (isNew)
            {
                thongTinThuoc.SysID = string.Empty;
                thongTinThuoc.CreatedBy = clsSystemConfig.UserName;
            }
            else
            {
                thongTinThuoc.SysID = selectedThuoc.SysID;
                thongTinThuoc.CreatedBy = string.Empty;
            }
            thongTinThuoc.ProductID = txtMaThuoc.Text;
            thongTinThuoc.ProductName = txtTenThuoc.Text;
            thongTinThuoc.Description = txtDescription.Text;
           // thongTinThuoc.TenThanhPhanThuoc = txtTenThanhPhanThuoc.Text;
            thongTinThuoc.DonViTinh = (int)cbbDonViTinh.SelectedValue;
           // thongTinThuoc.BaoHiem = cbBaoHiem.Checked;


            //thongTinThuoc.GiaDNMua = decimal.Parse("0");
            //thongTinThuoc.GiaDNMuaVAT = decimal.Parse("0");
            //thongTinThuoc.GiaThucMua = decimal.Parse("0");
            //thongTinThuoc.GiaDNBan = decimal.Parse("0");
            //thongTinThuoc.GiaDNBanVAT = decimal.Parse("0");
            //thongTinThuoc.GiaThucBan = decimal.Parse("0");

            //thongTinThuoc.HamLuong = txtHamLuong.Text;
            //thongTinThuoc.SoDKHoacGPKD = txtSoDangKy.Text;
            //thongTinThuoc.DangBaoCheDuongUong = txtDangBaoChe.Text;
            //thongTinThuoc.NhaSanXuat = txtNhaSanXuat.Text;
            //thongTinThuoc.QuocGia = txtQuocGia.Text;
           // thongTinThuoc.HoatDong = cbHoatDong.Checked;
            thongTinThuoc.LastUpdatedBy = clsSystemConfig.UserName;
            thongTinThuoc.HeSoAnToan = int.Parse(txtHeSoAnToan.Text);
            thongTinThuoc.ProductGroup = txtNhomThuoc.Text;
            //thongTinThuoc.CachUong = Int16.Parse(cboCachDung.SelectedValue.ToString());

            //thongTinThuoc.SttMaHoaTheoKQDTSoQDStt = txtSttMHTKQDT.Text;
            //thongTinThuoc.PhanNhomTheoTCHTVaTCCN = txtPhanNhom.Text;
            //thongTinThuoc.TenDonViSYT_BV = txtDonVi.Text;
            //thongTinThuoc.NgayHieuLuc = ckbChonNgayHieuLuc.Checked ? dtpNgayHieuLuc.Value.ToString("yyyy-MM-dd") : string.Empty;

            return thongTinThuoc;
        }

        private void LoadThongTinThuocToForm(ThongTinThuoc selectedThuoc)
        {
            txtMaThuoc.Text = selectedThuoc.ProductID;
            txtTenThuoc.Text = selectedThuoc.ProductName;
            txtDescription.Text = selectedThuoc.Description;
            //txtTenThanhPhanThuoc.Text = selectedThuoc.TenThanhPhanThuoc;
            cbbDonViTinh.SelectedValue = selectedThuoc.DonViTinh;
            //cbBaoHiem.Checked = selectedThuoc.BaoHiem;

           // dtpNgayHieuLuc.Value = selectedThuoc.NgayHieuLuc.Trim() == "" ? DateTime.Now : DateTime.Parse(selectedThuoc.NgayHieuLuc);
            //ckbChonNgayHieuLuc.Checked = selectedThuoc.NgayHieuLuc.Trim() != "";
           // txtSttMHTKQDT.Text = selectedThuoc.SttMaHoaTheoKQDTSoQDStt;
           // txtDonVi.Text = selectedThuoc.TenDonViSYT_BV;
           // txtPhanNhom.Text = selectedThuoc.PhanNhomTheoTCHTVaTCCN;
          //  cboCachDung.SelectedValue = selectedThuoc.CachUong;

           // txtGiaDNBanVAT.Text = selectedThuoc.GiaDNBanVAT.ToString();
           // txtGiaThucBan.Text = selectedThuoc.GiaThucBan.ToString();
          //  txtHamLuong.Text = selectedThuoc.HamLuong;
          //  txtSoDangKy.Text = selectedThuoc.SoDKHoacGPKD;
          //  txtDangBaoChe.Text = selectedThuoc.DangBaoCheDuongUong;
          //  txtNhaSanXuat.Text = selectedThuoc.NhaSanXuat;
          //  txtQuocGia.Text = selectedThuoc.QuocGia;
          //  cbHoatDong.Checked = selectedThuoc.HoatDong;
            txtHeSoAnToan.Text = selectedThuoc.HeSoAnToan.ToString();
            txtNhomThuoc.Text = selectedThuoc.ProductGroup;
        }

        private void grdToaThuoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdToaThuoc.CurrentCell;
            if (currentCell != null)
            {
                currentRowIndex = currentCell.RowIndex;
                selectedThuoc = listThuoc[currentRowIndex];
                LoadThongTinThuocToForm(selectedThuoc);
                txtMaThuoc.ReadOnly = true;
                btnUpdate.Enabled = true;
                btnLuu.Enabled = false;
            }
            else
            {
                currentRowIndex = -1;
            }

        }

        private void ResetFormThongTinHangHoa()
        {
            txtMaThuoc.Text = string.Empty;
            txtTenThuoc.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtTenThanhPhanThuoc.Text = string.Empty;
            cbbDonViTinh.SelectedIndex = 0;
            cbBaoHiem.Checked = false;
            txtSttMHTKQDT.Text = string.Empty;
            txtDonVi.Text = string.Empty;
            dtpNgayHieuLuc.Value = DateTime.Now;
            txtPhanNhom.Text = string.Empty;
            txtGiaDNBanVAT.Text = string.Empty;
            txtGiaThucBan.Text = string.Empty;
            txtHamLuong.Text = string.Empty;
            txtSoDangKy.Text = string.Empty;
            txtDangBaoChe.Text = string.Empty;
            txtNhaSanXuat.Text = string.Empty;
            txtQuocGia.Text = string.Empty;
            cbHoatDong.Checked = false;
            txtNhomThuoc.Text = string.Empty;
            txtHeSoAnToan.Text = string.Empty;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
    
            //if (!CheckThongTinGiaThuoc())
            //{
            //    MessageBox.Show("Thông tin giá thuốc không hợp lệ");
            //    return;
            //}
            //if (!CheckThongTinGiaThuocVAT())
            //{
            //    MessageBox.Show("Thông tin giá thuốc VAT không hợp lệ");
            //    return;
            //}
            if (!CheckThongTinHeSoAnToan())
            {
                MessageBox.Show("Hệ số an toàn không hợp lệ");
                return;
            }
            ThongTinThuoc tttCapNhat = BuildThongTinThuoc(true);
            if (_QuanLyDanhMucHangHoaDao.CheckMaHangExist(tttCapNhat.ProductID) == 1)
            {
                MessageBox.Show("Mã hàng đã tồn tại. Vui lòng chọn mã khác");
                return;
            }
            if (string.IsNullOrEmpty(tttCapNhat.ProductID))
            {
                MessageBox.Show("Vui lòng nhập thông tin hàng");
                return;
            }
            
                if (_QuanLyDanhMucHangHoaDao.LuuCapNhatThongTinThuoc_tbl_product(tttCapNhat))
                {
                    LoadDanhMucHangHoa(txtsMaThuoc.Text, txtsTenThuoc.Text);
                    ResetFormThongTinHangHoa();
                    MessageBox.Show("Lưu thành công");
                }
            
          else
            {
                
                MessageBox.Show("Có lỗi trong khi lưu");
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaThuoc.ReadOnly = false;
            btnUpdate.Enabled = false;
            btnLuu.Enabled = true;
            cbHoatDong.Checked = true;
            ResetFormThongTinHangHoa();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
     
            //if (!CheckThongTinGiaThuoc())
            //{
            //    MessageBox.Show("Thông tin giá thuốc không hợp lệ");
            //    return;
            //}
            //if (!CheckThongTinGiaThuocVAT())
            //{
            //    MessageBox.Show("Thông tin giá thuốc VAT không hợp lệ");
            //    return;
            //}
            if (!CheckThongTinHeSoAnToan())
            {
                MessageBox.Show("Hệ số an toàn không hợp lệ");
                return;
            }
            ThongTinThuoc tttCapNhat = BuildThongTinThuoc(false);
            //if (_quanLyThuocDao.CheckThuocExist(tttCapNhat.MaThuocYTe, tttCapNhat.BaoHiem) == 1)
            //{
            //    MessageBox.Show("Mã thuốc đã tồn tại. Vui lòng chọn mã khác");
            //    return;
            //}
            if (string.IsNullOrEmpty(tttCapNhat.ProductID))
            {
                MessageBox.Show("Vui lòng nhập thông tin hàng hóa");
                return;
            }

            if (_QuanLyDanhMucHangHoaDao.LuuCapNhatThongTinThuoc_tbl_product(tttCapNhat))
            {
                LoadDanhMucHangHoa(txtsMaThuoc.Text, txtsTenThuoc.Text);
                ResetFormThongTinHangHoa();
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Không thể cập nhật thông tin hàng hóa");
            }
        }
    
    }
}
