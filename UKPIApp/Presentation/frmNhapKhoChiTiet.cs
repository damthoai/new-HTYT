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
using UKPI.Presentation.ApproveTSLookup;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;
using UKPI.Controls;

namespace UKPI.Presentation
{
    public partial class frmNhapKhoChiTiet : Form
    {
        private readonly LFThongTinHangHoaChiTietDao _thongTinHangHoaChiTietDao = new LFThongTinHangHoaChiTietDao();
        List<LFThongTinHangHoaChiTiet> listChiTietHangHoa = new List<LFThongTinHangHoaChiTiet>();
        string tagCount = "0";
        string maThuoc;

        public frmNhapKhoChiTiet(ThongTinSanPham tttcbm)
        {
            InitializeComponent();            
            txtTenHang.Text = tttcbm.MedicineName;
            txtMaHeThong.Text = tttcbm.MedicineID;
            txtMaThuocYTe.Text = tttcbm.MaThuocYTe;
            txtDVT.Text = tttcbm.TenDonViTinh;
            txtThanhPhan.Text = tttcbm.TenThanhPhanThuoc;
            txtHamLuong.Text = tttcbm.HamLuong;
            txtHeSoAnToan.Text = tttcbm.HeSoAnToan.ToString();
            txtNhaSanXuat.Text = tttcbm.NhaSanXuat;
            txtQuocGia.Text = tttcbm.QuocGia;
            SetDefaultValue();
            maThuoc = tttcbm.MaThuocYTeHienThi;
            if (File.Exists("TempFileXML\\" + maThuoc + ".xml"))
            {
                LoadXML();
            }
            this.Text = "NHẬP KHO CHI TIẾT: " + tttcbm.MaThuocYTeHienThi;           
        }

        private void SetDefaultValue()
        {
            txtCount.Text = tagCount;            
            //BuildGridViewRow();
        }

        private void BuildGridViewRow()
        {
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Width = 60;
            checkBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdChiTietNhap.Columns.Add(checkBoxColumn);

            DataGridViewTextBoxColumn STT = new DataGridViewTextBoxColumn();
            STT.HeaderText = "STT";
            STT.Width = 60;
            STT.ReadOnly = true;
            STT.SortMode = DataGridViewColumnSortMode.NotSortable;
            STT.HeaderCell.Value = STT.Index + 1;
            grdChiTietNhap.Columns.Add(STT);

            DataGridViewTextBoxColumn maTag = new DataGridViewTextBoxColumn();
            maTag.HeaderText = "Mã Tag RFID";
            maTag.Width = 300;
            maTag.ReadOnly = true;
            maTag.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdChiTietNhap.Columns.Add(maTag);

            DataGridViewTextBoxColumn soLuong = new DataGridViewTextBoxColumn();
            soLuong.HeaderText = "Số Lượng";
            soLuong.Width = 60;
            soLuong.ReadOnly = true;
            soLuong.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdChiTietNhap.Columns.Add(soLuong);

            DataGridViewTextBoxColumn tenReader = new DataGridViewTextBoxColumn();
            tenReader.HeaderText = "Tên Reader";
            tenReader.Width = 150;
            tenReader.ReadOnly = true;
            tenReader.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdChiTietNhap.Columns.Add(tenReader);

            DataGridViewTextBoxColumn sysTime = new DataGridViewTextBoxColumn();
            sysTime.HeaderText = "Thời Gian";
            sysTime.Width = 150;
            sysTime.ReadOnly = true;
            sysTime.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdChiTietNhap.Columns.Add(sysTime);
        }

      

        private void button1_Click()
        {
            grdChiTietNhap.Rows.Add();
            grdChiTietNhap.Rows[0].Cells["maTag"].Value = "12";
            grdChiTietNhap.Rows.Add();
            //grdChiTietNhap.Rows[1].Cells[2].Value = "13";
            //grdChiTietNhap.Rows[2].Cells[2].Value = "14";

            /*grdChiTietNhap.Rows.Add(new object[] { true,"1", "12000000000000000000","1","RFID Reader 1" });
            grdChiTietNhap.Rows.Add(new object[] { true,"2", "13000000000000000000", "1", "RFID Reader 1" });
            grdChiTietNhap.Rows.Add(new object[] { true,"3", "14000000000000000000", "1", "RFID Reader 1" });*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            grdChiTietNhap.Rows.Clear();
            grdChiTietNhap.Refresh();
            //Add Dòng nhưng không có dữ liệu
            /* int rowIndex = this.grdChiTietNhap.Rows.Add(1);
             var row = this.grdChiTietNhap.Rows[rowIndex];*/
            //Add dòng nhưng có dữ liệu
            grdChiTietNhap.Rows.Add(new object[] { true,"1","12000000000000000000", "1", "RFID Reader 1", string.Format("{0:HH:mm:ss tt}", DateTime.Now) });
            grdChiTietNhap.Rows.Add(new object[] { true,"2","13000000000000000000", "1", "RFID Reader 1", string.Format("{0:HH:mm:ss tt}", DateTime.Now) });
            grdChiTietNhap.Rows.Add(new object[] { true,"3","14000000000000000000", "1", "RFID Reader 1", string.Format("{0:HH:mm:ss tt}", DateTime.Now) });

            //Chức năng tự động tăng STT cho Header Row
            foreach (DataGridViewRow STT in grdChiTietNhap.Rows)
            {
                if (STT.IsNewRow) continue;
                STT.Cells[1].Value = String.Format("{0}", STT.Index + 1);
                tagCount = String.Format("{0}", STT.Index + 1);
                grdChiTietNhap.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                txtCount.Text = String.Format("{0}", STT.Index + 1);

            }
        }
        

        
        // Check video này: https://www.youtube.com/watch?v=O_WDIiDhJFg để hiểu Save Data Grid thanh xml
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dttag = new DataTable();
            dttag.TableName = "dtTag";
            dttag.Columns.Add("SoLuongTagDaDoc");
            ds.Tables.Add(dttag);

            DataRow tagCount = ds.Tables["dtTag"].NewRow();
            tagCount["SoLuongTagDaDoc"] = txtCount.Text;
            ds.Tables["dtTag"].Rows.Add(tagCount);

            DataTable dt = new DataTable();
            dt.TableName = "listMa";
            dt.Columns.Add("STT");
            dt.Columns.Add("checkBox");
            dt.Columns.Add("maTag");
            dt.Columns.Add("soLuong");
            dt.Columns.Add("tenReader");
            dt.Columns.Add("thoiGian");            
            ds.Tables.Add(dt);        

           foreach (DataGridViewRow r in grdChiTietNhap.Rows)
            {
                DataRow row = ds.Tables["listMa"].NewRow();                
                row["checkBox"] = r.Cells[0].Value;
                row["STT"] = r.Cells[1].Value.ToString();
                row["maTag"] = r.Cells[2].Value.ToString();
                row["soLuong"] = r.Cells[3].Value.ToString();
                row["tenReader"] = r.Cells[4].Value.ToString();
                row["thoiGian"] = r.Cells[5].Value.ToString();
                ds.Tables["listMa"].Rows.Add(row);
            }
            ds.WriteXml("TempFileXML\\"+maThuoc+".xml");
            this.Close();
        }


        private void LoadXML()
        {
            DataSet ds = new DataSet();
            ds.ReadXml("TempFileXML\\" + maThuoc + ".xml");
            txtCount.Text = ds.Tables["dtTag"].Rows[0][0].ToString();            
            foreach(DataRow item in ds.Tables["listMa"].Rows)
            {
                int n = grdChiTietNhap.Rows.Add();                
                grdChiTietNhap.Rows[n].Cells[0].Value = item["checkBox"];
                grdChiTietNhap.Rows[n].Cells[1].Value = item["STT"].ToString();
                grdChiTietNhap.Rows[n].Cells[2].Value = item["maTag"].ToString();
                grdChiTietNhap.Rows[n].Cells[3].Value = item["soLuong"].ToString();
                grdChiTietNhap.Rows[n].Cells[4].Value = item["tenReader"].ToString();
                grdChiTietNhap.Rows[n].Cells[5].Value = item["thoiGian"].ToString();
            }
        }

        private void btnXoaTag_Click(object sender, EventArgs e)
        {
            for (int i = grdChiTietNhap.Rows.Count - 1; i >= 0; i--)
            {
                if ((bool)grdChiTietNhap.Rows[i].Cells[0].FormattedValue)
                {
                    grdChiTietNhap.Rows.RemoveAt(i);
                }
            }
            txtCount.Text = grdChiTietNhap.Rows.Count.ToString();
        }
    }
}

