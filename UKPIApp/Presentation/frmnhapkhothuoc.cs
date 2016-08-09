using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using Excel;
using FPT.Component.ExcelPlus;
using UKPI.BusinessObject;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;
using UKPI.Controls;
using Intermec.DataCollection.RFID;
namespace UKPI.Presentation
{
    public partial class frmnhapkhothuoc : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmnhapkhothuoc));
        private readonly LFThongTinHangHoaChiTietDao _thongTinHangHoaChiTietDao = new LFThongTinHangHoaChiTietDao();
        List<LFThongTinHangHoaChiTiet> listChiTietHangHoa = new List<LFThongTinHangHoaChiTiet>();
        private LFThongTinHangHoaChiTiet ttct;
        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        private readonly ThongTinNhapKhoDao _thongTinNhapKhoDao = new ThongTinNhapKhoDao();
        private readonly ChotTonKhoDao _chotTonKhoDao = new ChotTonKhoDao();

        //lưu trữ thông tin RFID
        List<string> _lstRFID;


        List<ThongTinSanPham> _lstSanPham;

        QuyetDinhNghiPhep quyetDinhNghiPhep;
        readonly System.Data.DataTable _dt = null;
        ComboBox cbm;
        DataGridViewCell currentCell;
        private DateTimePicker cellDateTimePicker;
        private int _checkRowsCount = 0;
        private Dictionary<int, string> danhSachThuoc = new Dictionary<int, string>();

        // Declare constants
        private const string FieldCheck = "colCheck";
        private const String Check = "CHECK";
        private const String ValueTrue = "Y";
        private const String ValueFalse = "N";
        //param value.
        private String parHanChotDuyetCong = "";
        private String parHanChotDitre = "";
        private String parHanChotVeSom = "";
        private String parChuanTinhCong = "";
        private String parHanMucTinhOt = "";


        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();

        readonly DataGridViewColumn _originalColumns;
        private DataTable _dtApproveTimesheet;

        #endregion

        #region Constructors
        public BRIReader brdr = null;
        private bool bReaderOffLine = true;

        public frmnhapkhothuoc()
        {

            InitializeComponent();
            //OpenReaderConnection();
            clsTitleManager.InitTitle(this);

            #region Xu lý du lieu ngày gio
            this.cellDateTimePicker = new DateTimePicker();
            this.cellDateTimePicker.Format = DateTimePickerFormat.Custom;
            this.cellDateTimePicker.Width = 100;
            this.cellDateTimePicker.CustomFormat = "dd-MM-yyyy";

            #endregion


            #region Đang ky du kien
            //this.cellDateTimePicker.ValueChanged += new EventHandler(cellDateTimePickerValueChanged);
            this.cellDateTimePicker.ValueChanged += new EventHandler(cellDateTimePicker_ValueChanged);
            // this.cellDateTimePicker.CloseUp += new EventHandler(oDateTimePicker_CloseUp);              
            #endregion


            this.cellDateTimePicker.Visible = false;
            this.grdSanPham.Controls.Add(cellDateTimePicker);
            cbbPhongKham.Enabled = false;
            SetDefauldValue();
            this.Text = "NHẬP KHO";
            // Save original columns
            // _originalColumns = new DataGridViewColumn[grdStores.Columns.Count;
            // grdStores.Columns.CopyTo(_originalColumns, 0);
            // grdStores.Sorted += grdStores_Sorted;
            if (_chotTonKhoDao.CheckChotTonDangHoatDong(cbbPhongKham.SelectedValue.ToString()) > 0)
            {
                DialogResult result = MessageBox.Show("Kho đang được chốt tồn. Vui lòng thực hiện sau", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLuuIn.Enabled = false;
            }
            else
            {
                btnLuuIn.Enabled = true;
            }
        }

        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
            cellDateTimePicker.Visible = false;
        }
        private void SetDefauldValue()
        {
            BindPhongKham();
            BuildGridViewRow();
            LoadThongTinNhanVien();
        }

        /// <summary>
        /// Lấy hết danh mục các phòng khám
        /// </summary>
        private void BindPhongKham()
        {
            //cbbPhongKham.DataSource = _shareEntityDao.LoadDanhSachPhongKham();
            List<PhongKham> listPhongKham = _shareEntityDao.LoadDanhSachPhongKham();
            cbbPhongKham.DataSource = listPhongKham;
            string currentKho = System.Configuration.ConfigurationManager.AppSettings["RCLINIC00002"];
            int currentIndex = listPhongKham.FindIndex(a => a.RoomID == currentKho);
            cbbPhongKham.SelectedIndex = currentIndex;
        }

        private void LoadThongTinNhanVien()
        {
            // ThongTinBenhNhan ttNhanVien = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
            txtMaNhanVienNhap.Text = clsSystemConfig.UserName;
            txtNhanVienNhap.Text = clsSystemConfig.FullName;
        }

        private void BuildGridViewRow()
        {
            // Lấy danh sách sản phẩm để chọn lựa trên lưới nhập kho--ChinhLH
            _lstSanPham = _shareEntityDao.LoadThongTinSanPham();

            #region Thiết kế layout


            //DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            //checkBoxColumn.Width = 60;
            //checkBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            //grdSanPham.Columns.Add(checkBoxColumn);


            DataGridViewTextBoxColumn tenThuocColumn = new DataGridViewTextBoxColumn();
            tenThuocColumn.HeaderText = "Tên";
            tenThuocColumn.Width = 145;
            tenThuocColumn.ReadOnly = true;
            tenThuocColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdSanPham.Columns.Add(tenThuocColumn);

            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.Width = 140;
            col.HeaderText = "Mã";
            col.DataSource = _lstSanPham;
            col.DisplayMember = "ProductID";
            //col.ValueMember = "MedicineID";
            col.ValueMember = "ProductID";
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdSanPham.Columns.Add(col);

            //DataGridViewTextBoxColumn hanSuDungColumn = new DataGridViewTextBoxColumn();
            //hanSuDungColumn.Width = 100;
            //hanSuDungColumn.HeaderText = "Hạn sử dụng";
            //hanSuDungColumn.Visible = true;
            //hanSuDungColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            //grdSanPham.Columns.Add(hanSuDungColumn);

            //DataGridViewCheckBoxColumn baoHiemColumn = new DataGridViewCheckBoxColumn();
            //baoHiemColumn.Width = 100;
            //baoHiemColumn.HeaderText = "Thuốc BH";
            ////hanSuDungColumn.Visible = false;
            ////baoHiemColumn.ReadOnly = true;
            //baoHiemColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            //grdSanPham.Columns.Add(baoHiemColumn);

            DataGridViewTextBoxColumn soLuongColumn = new DataGridViewTextBoxColumn();
            soLuongColumn.Width = 130;
            soLuongColumn.HeaderText = "Số lượng";
            soLuongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdSanPham.Columns.Add(soLuongColumn);

            DataGridViewTextBoxColumn hamLuongColumn = new DataGridViewTextBoxColumn();
            hamLuongColumn.Width = 130;
            hamLuongColumn.HeaderText = "ĐVT";
            hamLuongColumn.ReadOnly = true;
            hamLuongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdSanPham.Columns.Add(hamLuongColumn);

            DataGridViewTextBoxColumn thanhTienColumn = new DataGridViewTextBoxColumn();
            thanhTienColumn.Width = 130;
            thanhTienColumn.HeaderText = "Thành tiến";
            thanhTienColumn.ReadOnly = true;
            thanhTienColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdSanPham.Columns.Add(thanhTienColumn);

            DataGridViewButtonColumn byRFIDColumn = new DataGridViewButtonColumn();
            byRFIDColumn.Width = 130;
            byRFIDColumn.Text = "by RFID";
            byRFIDColumn.HeaderText = "";
            byRFIDColumn.UseColumnTextForButtonValue = true;
            byRFIDColumn.Name = "byRFIDColumn";
            byRFIDColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdSanPham.Columns.Add(byRFIDColumn);


            //grdSanPham.Columns.Add(thanhTienColumn);
            grdSanPham.CellBeginEdit += this.dataGridView1_CellBeginEdit;
            grdSanPham.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            grdSanPham.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            grdSanPham.CellValueChanged += grdSanPham_CellValueChanged;
            // grdSanPham.CellClick += dataGridView1_CellClick;
            #endregion

            grdSanPham.CellContentClick += dtdvDanhMucSanPham_CellContentClick;

            int rowIndex = this.grdSanPham.Rows.Add(1);
            //this.grdSanPham.Rows[rowIndex].Cells["byRFIDColumn"].ReadOnly = true;
            //(this.grdSanPham.Rows[rowIndex].Cells["byRFIDColumn"] as DataGridViewButtonCell).ReadOnly = true;

            //var row = this.grdSanPham.Rows[rowIndex];

        }
        public void GetValue(string str)
        {// khai báo 1 hàm với 2 tham số đầu vào là str1, và str2 nó sẽ đưa dữ liệu vào 2 lable
            txtTenThuoc.Text = str;
        }



        //private void grdSanPham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //currentCell = this.grdSanPham.CurrentCell;
        //    //bool isValidMaThuoc = this.grdSanPham[2, currentCell.RowIndex].Value != null && this.grdSanPham[2, currentCell.RowIndex].Value.ToString() != "";
        //    //if (e.ColumnIndex == 3 && isValidMaThuoc)
        //    //{
        //    //    System.Drawing.Rectangle tempRect = grdSanPham.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

        //    //    cellDateTimePicker.Location = tempRect.Location;

        //    //    cellDateTimePicker.Width = tempRect.Width;

        //    //    cellDateTimePicker.Visible = true;

        //    //}
        // }


        /// <summary>
        /// Thực hiện show PopUp Form Khi Click By RFID. Made by PhongLF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtdvDanhMucSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                string strTempID = null;

                if (grdSanPham.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    strTempID = grdSanPham.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //DataGridViewRow row = this.grdSanPham.Rows[e.RowIndex];
                    ThongTinSanPham thongTinSanPham = _lstSanPham.Where(ttsp => ttsp.ProductID == strTempID).First();
                    //fruits.Where(fruit => fruit.Length < 6)
                    #region Code Phong

                    //_lstSanPham = _shareEntityDao.LoadThongTinSanPham();

                    //string pos = grdSanPham.Rows[e.RowIndex].Cells[2].Value.ToString();
                    //int n = -1;
                    //foreach (ThongTinSanPham item in _lstSanPham)
                    //{
                    //    if (item.ProductID == pos)
                    //    {
                    //        n = _lstSanPham.IndexOf(item);
                    //        break;
                    //    }
                    //}
                    //MessageBox.Show(n.ToString());
                    //ThongTinSanPham tttcbm = _lstSanPham[n];
                    //var form = new frmNhapKhoChiTiet(tttcbm);                    
                    #endregion

                    var form = new frmNhapKhoChiTiet(thongTinSanPham);
                    //đăng ký deletegate cho form con
                    form.MyGetData = new frmNhapKhoChiTiet.GetString(GetValue);
                    form.Show(this);



                }
                else
                {
                    MessageBox.Show("Vui lòng chọn Mã!");
                }

            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdSanPham.CurrentCell;
            try
            {
                //if (currentCell != null && currentCell.ColumnIndex == 6  )
                if (e.ColumnIndex == grdSanPham.Columns[""].Index && currentCell.ColumnIndex == 6)
                {
                    //string maHang = this.grdSanPham[currentCell.ColumnIndex - 4, currentCell.RowIndex].Value.ToString();

                    //MessageBox.Show("Button on row {0} clicked", e.RowIndex.ToString());

                    List<ThongTinSanPham> lstThuoc = _shareEntityDao.LoadThongTinSanPham();
                    ThongTinSanPham tttcbm = this.cbm.SelectedItem as ThongTinSanPham;
                    //ThongTinThuoc tttcbm = lstThuoc[currentCell.RowIndex];
                    var form = new frmNhapKhoChiTiet(tttcbm);
                    form.Show();



                    // var form = new frmNhapKhoChiTiet(ttct);                    
                    // form.Show(this);
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.ToString());

                MessageBox.Show("Vui lòng chọn Mã!");
            }




            /*
            if (e.ColumnIndex == 6)
            {
                
                // MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                MessageBox.Show(this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString());
                //this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()
            }*/
        }
        void cellDateTimePickerValueChanged(object sender, EventArgs e)
        {
            //grdSanPham.CurrentCell.Value = cellDateTimePicker.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]);//convert the date as per your format
            //cellDateTimePicker.Visible = false;
        }


        private void Export()
        {

        }


        #endregion

        private void btnExport_Click(object sender, EventArgs e)
        {
            // this.Export();
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(clsResources.GetMessage("messages.save.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.Waring"), clsResources.GetMessage("messages.frmnhapkhothuoc.Title"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                grpThongTinKhamBenh.Enabled = false;
                btnXoaThuoc.Enabled = false;
            }

        }
        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            DialogResult warningMessage = MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.WarningMessage"), clsResources.GetMessage("messages.frmnhapkhothuoc.SuccessTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (warningMessage == DialogResult.Yes)
            {

                if (!ValidateThongSoNhapKho())
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.ValidateThongSoNhapKho"), clsResources.GetMessage("messages.frmnhapkhothuoc.Title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ThongTinNhapKho thongTinNhapKho = BuildThongTinNhapKho();
                if (thongTinNhapKho != null)
                {
                    List<ThongTinNhapKhoDetail> listThongTinNhapKhoDetail = BuildThongTinNhapKhoDetail(thongTinNhapKho.MaNhapKho);
                    if (listThongTinNhapKhoDetail != null && listThongTinNhapKhoDetail.Count > 0)
                    {
                        if (_thongTinNhapKhoDao.SaveThongTinNhapKho(thongTinNhapKho, listThongTinNhapKhoDetail))
                        {
                            DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.Success"), clsResources.GetMessage("messages.frmnhapkhothuoc.SuccessTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (result == DialogResult.OK)
                            {
                                grdSanPham.Rows.Clear();
                                grdSanPham.Rows.Add(1);
                                cellDateTimePicker.Visible = false;
                                txtDonViCungCap.Clear();
                                txtMaSoHDD.Clear();
                                txtMaDonViCungCap.Clear();
                            }
                            return;
                        }
                        else
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.Error"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                }
            }
        }
        public void SetQuyetDinhNghiPhep(QuyetDinhNghiPhep qd)
        {
            this.quyetDinhNghiPhep = qd;
        }
        private bool ValidateThongSoNhapKho()
        {
            bool isValid = true;
            if (txtMaSoHDD.Text == "")
            {
                txtMaSoHDD.BackColor = Color.Red;
                isValid = false;
            }
            if (txtDonViCungCap.Text == "")
            {
                txtDonViCungCap.BackColor = Color.Red;
                isValid = false;
            }
            if (txtMaDonViCungCap.Text == "")
            {
                txtMaDonViCungCap.BackColor = Color.Red;
                isValid = false;
            }
            if (!isValid)
            {

            }
            else
            {
                txtMaSoHDD.BackColor = Color.White;
                txtDonViCungCap.BackColor = Color.White;
                txtMaDonViCungCap.BackColor = Color.White;
            }
            return isValid;
        }
        private ThongTinNhapKho BuildThongTinNhapKho()
        {
            ThongTinNhapKho thongTinNhapKho = new ThongTinNhapKho();
            thongTinNhapKho.MaNhapKho = _thongTinNhapKhoDao.GenerateNewMaNhapKho();
            thongTinNhapKho.PhongKhamKho = cbbPhongKham.Text;
            thongTinNhapKho.MaKho = cbbPhongKham.SelectedValue.ToString();
            thongTinNhapKho.NgayNhapKho = dtpNgayNhapKho.Value;
            thongTinNhapKho.TongTienHD = txtTongThanhTien.Text;
            thongTinNhapKho.MaNhanVien = txtMaNhanVienNhap.Text;
            thongTinNhapKho.TenNhanVien = txtNhanVienNhap.Text;
            thongTinNhapKho.MaHDD = txtMaSoHDD.Text;
            thongTinNhapKho.DonViCungCap = txtDonViCungCap.Text;
            thongTinNhapKho.MaSoDVCungCap = txtMaDonViCungCap.Text;
            return thongTinNhapKho;
        }

        private List<ThongTinNhapKhoDetail> BuildThongTinNhapKhoDetail(string maNhapKho)
        {
            List<string> listmaThuoc = new List<string>();
            Dictionary<CustomKey, string> dic = _shareEntityDao.BuildTuDienThuoc();
            List<ThongTinNhapKhoDetail> listThongTinNhapKhoDetail = new List<ThongTinNhapKhoDetail>();
            if (grdSanPham.Rows.Count > 0)
            {

                for (int i = 0; i < grdSanPham.Rows.Count; i++)
                {

                    ThongTinNhapKhoDetail thongTinNhapKhoDetail = new ThongTinNhapKhoDetail();
                    if ((string)grdSanPham.Rows[i].Cells[1].FormattedValue == "")
                        continue;
                    thongTinNhapKhoDetail.TenThuoc = (string)grdSanPham.Rows[i].Cells[1].FormattedValue;
                    thongTinNhapKhoDetail.MaThuoc = (string)grdSanPham.Rows[i].Cells[2].FormattedValue;
                    string hanSuDung = grdSanPham.Rows[i].Cells[3].FormattedValue.ToString();
                    if (hanSuDung == "")
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckHanSuDungThuoc"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    //DateTime dt = DateTime.ParseExact(hanSuDung.Replace("-",""), "ddMMyyyy",
                    //              CultureInfo.InvariantCulture);
                    //dt.ToString("yyyyMMdd");
                    string strHanSuDung = (string)grdSanPham.Rows[i].Cells[3].FormattedValue;
                    thongTinNhapKhoDetail.HanSuDung = DateTime.ParseExact(strHanSuDung, System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
                    thongTinNhapKhoDetail.ThuocBH = (bool)grdSanPham.Rows[i].Cells[4].FormattedValue;
                    thongTinNhapKhoDetail.MaNhapKho = maNhapKho;
                    thongTinNhapKhoDetail.LoThuoc = DateTime.Now.ToString("yyyyMMddHHmmss");
                    thongTinNhapKhoDetail.HamLuong = (string)grdSanPham.Rows[i].Cells[9].FormattedValue;

                    CustomKey ck = new CustomKey(thongTinNhapKhoDetail.MaThuoc, (bool)grdSanPham.Rows[i].Cells[4].FormattedValue);
                    thongTinNhapKhoDetail.MaThuoc = dic[ck];
                    try
                    {
                        string strThanhTien = (string)grdSanPham.Rows[i].Cells[10].FormattedValue;
                        thongTinNhapKhoDetail.ThanhTien = decimal.Parse(strThanhTien);
                    }
                    catch { }
                    try
                    {
                        string strSoLuong = (string)grdSanPham.Rows[i].Cells[5].FormattedValue;
                        thongTinNhapKhoDetail.SoLuong = int.Parse(strSoLuong);
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    try
                    {
                        string strGiaTT = (string)grdSanPham.Rows[i].Cells[7].FormattedValue;
                        thongTinNhapKhoDetail.GiaTT = decimal.Parse(strGiaTT);
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaTruocThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    try
                    {
                        string strGiaST = (string)grdSanPham.Rows[i].Cells[8].FormattedValue;
                        thongTinNhapKhoDetail.GiaST = decimal.Parse(strGiaST);
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaSauThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    try
                    {
                        string strGiaThoiDiemNhap = (string)grdSanPham.Rows[i].Cells[6].FormattedValue;
                        thongTinNhapKhoDetail.GiaThoiDiemNhap = decimal.Parse(strGiaThoiDiemNhap);
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaMua"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    if (!listmaThuoc.Contains(thongTinNhapKhoDetail.MaThuoc))
                    {
                        listmaThuoc.Add(thongTinNhapKhoDetail.MaThuoc);
                    }
                    else
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckTrungLapThuoc"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    listThongTinNhapKhoDetail.Add(thongTinNhapKhoDetail);

                }

            }
            return listThongTinNhapKhoDetail;
        }
        private void btnXoaThuoc_Click(object sender, EventArgs e)
        {
            for (int i = grdSanPham.Rows.Count - 1; i > 0; i--)
            {
                if ((bool)grdSanPham.Rows[i].Cells[0].FormattedValue)
                {
                    grdSanPham.Rows.RemoveAt(i);
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void grdSanPham_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdSanPham.CurrentCell;
            //MessageBox.Show(currentCell.ColumnIndex.ToString());

            if (currentCell != null && currentCell.ColumnIndex == 5)
            {
                int currentSoLuong = 0;
                bool isValidMaThuoc = this.grdSanPham[2, currentCell.RowIndex].Value != null && this.grdSanPham[2, currentCell.RowIndex].Value.ToString() != "";
                bool isValidSoLuongThuoc = this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value != null && this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString() != "";
                if (isValidMaThuoc && isValidSoLuongThuoc)
                {
                    try
                    {
                        currentSoLuong = this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? int.Parse(this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;
                        if (currentSoLuong <= 0)
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdSanPham[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdSanPham[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGia = 0;
                }

                if (currentGia <= 0)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaSauThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                decimal currentTienThuoc = currentSoLuong * currentGia;
                // MessageBox.Show("CellChange" + currentTienThuoc.ToString());
                this.grdSanPham[currentCell.ColumnIndex + 5, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                CalculateTotal();
            }
            //Check gia sau thue
            if (currentCell != null && currentCell.ColumnIndex == 8)
            {
                int currentSoLuong = 0;
                bool isValidMaThuoc = this.grdSanPham[2, currentCell.RowIndex].Value != null && this.grdSanPham[2, currentCell.RowIndex].Value.ToString() != "";
                bool isValidSoLuongThuoc = this.grdSanPham[currentCell.ColumnIndex - 3, currentCell.RowIndex].Value != null && this.grdSanPham[currentCell.ColumnIndex - 3, currentCell.RowIndex].Value.ToString() != "";
                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGia = 0;
                }

                if (currentGia <= 0)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaSauThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (isValidMaThuoc && isValidSoLuongThuoc)
                {
                    try
                    {
                        currentSoLuong = this.grdSanPham[currentCell.ColumnIndex - 3, currentCell.RowIndex].Value != null ? int.Parse(this.grdSanPham[currentCell.ColumnIndex - 3, currentCell.RowIndex].Value.ToString()) : 0;
                        if (currentSoLuong <= 0)
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                decimal currentTienThuoc = currentSoLuong * currentGia;
                // MessageBox.Show("CellChange" + currentTienThuoc.ToString());
                this.grdSanPham[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                CalculateTotal();
            }
            //check gia thoi diem nhap
            if (currentCell != null && currentCell.ColumnIndex == 6)
            {
                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGia = 0;
                }

                if (currentGia <= 0)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaMua"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            //check gia truoc thue
            if (currentCell != null && currentCell.ColumnIndex == 7)
            {
                int currentSoLuong = 0;
                bool isValidMaThuoc = this.grdSanPham[2, currentCell.RowIndex].Value != null && this.grdSanPham[2, currentCell.RowIndex].Value.ToString() != "";
                bool isValidSoLuongThuoc = this.grdSanPham[currentCell.ColumnIndex - 2, currentCell.RowIndex].Value != null && this.grdSanPham[currentCell.ColumnIndex - 2, currentCell.RowIndex].Value.ToString() != "";

                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;
                }
                catch
                {
                    currentGia = 0;
                }

                if (currentGia <= 0)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaTruocThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (isValidMaThuoc && isValidSoLuongThuoc)
                {
                    try
                    {
                        currentSoLuong = this.grdSanPham[currentCell.ColumnIndex - 2, currentCell.RowIndex].Value != null ? int.Parse(this.grdSanPham[currentCell.ColumnIndex - 2, currentCell.RowIndex].Value.ToString()) : 0;
                        if (currentSoLuong <= 0)
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                decimal currentTienThuoc = currentSoLuong * currentGia;
                // MessageBox.Show("CellChange" + currentTienThuoc.ToString());
                this.grdSanPham[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                CalculateTotal();
            }
            return;
        }

        private void CalculateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in grdSanPham.Rows)
            {
                if (row.Cells[10].Value != null)
                {
                    total += decimal.Parse(row.Cells[10].Value.ToString());
                }
            }

            txtTongThanhTien.Text = total.ToString();

            //BtxtTongThanhTien.Click += new EventHandler(txtTongThanhTien_Click);

        }

        //void txtTongThanhTien_Click(object sender, EventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if ((grdSanPham.Focused) && grdSanPham.CurrentCell.ColumnIndex == 3)
                {
                    cellDateTimePicker.Location = grdSanPham.GetCellDisplayRectangle(grdSanPham.CurrentCell.ColumnIndex, grdSanPham.CurrentCell.RowIndex, false).Location;
                    cellDateTimePicker.Visible = true;
                    if (grdSanPham.CurrentCell.Value != null)
                    {
                        string hanSuDung = grdSanPham.CurrentCell.FormattedValue.ToString();
                        cellDateTimePicker.Value = DateTime.ParseExact(hanSuDung, System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        cellDateTimePicker.Value = DateTime.Today;
                    }
                }
                else
                {
                    cellDateTimePicker.Visible = false;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cbm != null)
            {


                // Here we will remove the subscription for selected index changed
                cbm.SelectedIndexChanged -= new EventHandler(cbm_SelectedIndexChanged);
            }
            try
            {
                if ((grdSanPham.Focused) && grdSanPham.CurrentCell.ColumnIndex == 3)
                {
                    grdSanPham.CurrentCell.Value = cellDateTimePicker.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]);//convert the date as per your format//cellDateTimePicker.Value;
                }

                if ((grdSanPham.Focused) && grdSanPham.CurrentCell.ColumnIndex == 7)
                {
                    var giaMuaTT = decimal.Parse(grdSanPham.CurrentCell.Value.ToString());
                    if (giaMuaTT > 0)
                    {
                        this.grdSanPham[8, currentCell.RowIndex].Value = (giaMuaTT * decimal.Parse("1.05")).ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cellDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            grdSanPham.CurrentCell.Value = cellDateTimePicker.Text;
        }
        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Here try to add subscription for selected index changed event
            if (e.Control is ComboBox)
            {
                cbm = (ComboBox)e.Control;
                if (cbm != null)
                {

                    cbm.DropDownStyle = ComboBoxStyle.DropDown;
                    cbm.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cbm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
                    cbm.SelectedIndexChanged += new EventHandler(cbm_SelectedIndexChanged);
                }
                currentCell = this.grdSanPham.CurrentCell;
            }
        }

        void cbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Invoke method if the selection changed event occurs
            BeginInvoke(new MethodInvoker(EndEdit));
        }

        void EndEdit()
        {
            // Change the content of appropriate cell when selected index changes
            if (cbm != null)
            {
                ThongTinThuoc ttt = cbm.SelectedItem as ThongTinThuoc;
                //DataRowView drv = cbm.SelectedItem as DataRowView;
                if (ttt != null)
                {
                    //  string item = this.grdSanPham[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString();
                    if (currentCell.ColumnIndex == 2)
                    {
                        //     MessageBox.Show(ttt.MedicineName);
                        //case 1: chua co thong tin thuoc cho row
                        if (!danhSachThuoc.ContainsKey(currentCell.RowIndex) && !danhSachThuoc.ContainsValue(ttt.MedicineID))
                        {
                            danhSachThuoc.Add(currentCell.RowIndex, ttt.MedicineID);
                        }
                        else if (danhSachThuoc.ContainsKey(currentCell.RowIndex))
                        {
                            danhSachThuoc.Remove(currentCell.RowIndex);
                            danhSachThuoc.Add(currentCell.RowIndex, ttt.MedicineID);
                        }
                        else
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckTrungLapThuoc1"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //Tính từ vị trí currentCell là CombiBox = 0 --> Column Tên nằm bên trái currentCell là -1
                        this.grdSanPham[currentCell.ColumnIndex - 1, currentCell.RowIndex].Value = ttt.MedicineName;
                        //this.grdSanPham[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value = ttt.BaoHiem;
                        this.grdSanPham[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value = ttt.TenDonViTinh;
                        /* this.grdSanPham[currentCell.ColumnIndex + 4, currentCell.RowIndex].Value = ttt.GiaDNMuaVAT;
                         this.grdSanPham[currentCell.ColumnIndex + 5, currentCell.RowIndex].Value = ttt.GiaDNMua;
                         //this.grdSanPham[currentCell.ColumnIndex + 6, currentCell.RowIndex].Value = ttt.GiaDNMuaVAT;
                         //this.grdSanPham[currentCell.ColumnIndex + 7, currentCell.RowIndex].Value = ttt.HamLuong;
                         this.grdSanPham[currentCell.ColumnIndex + 6, currentCell.RowIndex].Value = ttt.HamLuong;*/
                    }
                    if (currentCell.ColumnIndex == 2 && (currentCell.RowIndex == grdSanPham.Rows.Count - 1))
                    {
                        grdSanPham.Rows.Add(1);
                    }

                }

            }

        }

        //RFID Reader Area
        private bool OpenReaderConnection()
        {
            //Establish connection with reader.
            //Choose network or serial connection.

            bool bStatus = false;
            string sMsg = null;
            string sConnection = "TCP://showroomlf.ddns.net";

            //define connection
            //string sConnection = "SERIAL://COM1";
            //string sConnection = "TCP://192.168.1.1";


            //optional: enable IDL debug logging *******************************
            // BRIReader.LoggerOptionsAdv LogOp = new BRIReader.LoggerOptionsAdv();
            //LogOp.LogFilePath = ".\\IDLClassDebugLog.txt";
            //LogOp.ShowNonPrintableChars = true;
            //******************************************************************

            try
            {
                //option 1: open reader connection simple method, no debugging
                brdr = new BRIReader(this, sConnection);

                //option two -> set size of reader buffer, event buffer, and enable IDL logging.
                //Reader Buffer is used for storing tags when you issue a READ, or READ REPORT=NO
                //Event Buffer is used for storing tags when you issue a READ REPORT=EVENT and all other events.
                //BRIReader(this, sConnection, Reader Buffer, Event Buffer, LogOp);
                //this.brdr = new BRIReader(this, sConnection, 1000, 1000, LogOp);

                bStatus = true;
            }
            catch (BasicReaderException ex)
            {
                textBox1.Text = "Không thể kết nối";
                textBox1.ForeColor = Color.FromKnownColor(KnownColor.Red);
                textBox1.ReadOnly = true;
                // MessageBox.Show(ex.ToString());
                bStatus = false;
            }

            if (brdr == null || bStatus == false)
            {
                //failed to create reader connection
                bReaderOffLine = true;
                //PostMessageToListBox1("Unable to connect to reader!");
                // PostMessageToListBox1(sConnection);
                return false;
            }

            //Verify that we are actually talking to the RF module.  Should return OK>
            sMsg = this.brdr.Execute("PING");
            if (sMsg != null)
            {
                if (sMsg.Equals("OK>"))
                {
                    //get reader firmware version
                    sMsg = this.brdr.Execute("VER");
                    //ParseResponseMessage(sMsg);
                    //dataGridView2.DataSource = sMsg;
                    //label1.Visible = true;

                    SetReaderAttributes();
                    bReaderOffLine = false;
                    bStatus = true;
                }
            }

            if (bStatus == false)
            {
                //not connected to reader
                // PostMessageToListBox1("Unable to connect to hand held");
                // PostMessageToListBox1(sConnection);
                bReaderOffLine = true;
            }

            return bStatus;
        }
        private void SetReaderAttributes()
        {
            //Optional Code
            //Some examples of how to set attributes
            string sRsp = null;

            try
            {

                brdr.Execute("ATTRIB IDTRIES=1");
                brdr.Execute("ATTRIB ANTTRIES=1");
                brdr.Execute("ATTRIB WRTRIES=3");
                brdr.Execute("ATTRIB ANTS=1");

                //Other examples of selecting antennas
                //brdr.Execute("ATTRIB ANTS=1,2,3,4");
                //brdr.Execute("ATTRIB ANTS=2,4");
                //brdr.Execute("ATTRIB ANTS=1,3,4");

                //get the list of all attributes from the reader and display them.
                sRsp = this.brdr.Execute("ATTRIB");
                //ParseResponseMessage(sRsp);
            }
            catch (Intermec.DataCollection.RFID.BasicReaderException ex)
            {
                MessageBox.Show("SetAttribute Exception : " + ex.Message);
            }
        }
    }
}