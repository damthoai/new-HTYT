﻿using System;
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
using Microsoft.Reporting.WinForms;
namespace UKPI.Presentation
{
    public partial class frmBaoCaoXuatNhapTon : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmbaocaolichsubenhnhan));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ReportBo _reportBo = new ReportBo();
        #endregion

        #region Constructors

        public frmBaoCaoXuatNhapTon()
        {

            InitializeComponent();
            this.Text = "BÁO CÁO NHẬP - XUẤT - TỒN";
           // clsTitleManager.InitTitle(this);

            SetDefauldValue();

            BindPhongKham();
        }
        private void BindPhongKham()
        {

            List<PhongKham> listPhongKham = _shareEntityDao.LoadDanhSachPhongKham();
            cbbPhongKham.DataSource = listPhongKham;
            string currentKho = System.Configuration.ConfigurationManager.AppSettings["RCLINIC00002"];
            int currentIndex = listPhongKham.FindIndex(a => a.RoomID == currentKho);
            cbbPhongKham.SelectedIndex = currentIndex;



        }


        private void SetDefauldValue()
        {
            ckbBaoCaoTheoQuyNam.Checked = true;

            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";


            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";


        }


        private void LoadThongTinXuatKho()
        {

        }

        #endregion


        private void btnExport_Click(object sender, EventArgs e)
        {
            // this.Export();
        }

        public void SetQuyetDinhNghiPhep(QuyetDinhNghiPhep qd)
        {

        }

        private void frmBaoCaoXuatNhapTon_Load(object sender, EventArgs e)
        {

        }

        private void RunReport()
        {
            this.rvBaoCaoTTBHYT.RefreshReport();
            rvBaoCaoTTBHYT.Reset();
            rvBaoCaoTTBHYT.ProcessingMode = ProcessingMode.Local;
            LocalReport localReport = rvBaoCaoTTBHYT.LocalReport;
            var dir = System.IO.Directory.GetCurrentDirectory() + "\\Presentation\\reports\\";

            localReport.ReportPath = dir + "BaoCaoXuatNhapTon.rdlc";

            DataTable _tbToaThuoc = new DataTable();

            var quy =  txtQuy.Text ;
            var nam =  txtNam.Text ;
            var tuNgay =  dtpTuNgay.Value.ToString("yyyy-MM-dd") ;
            var denNgay =  dtpDenNgay.Value.ToString("yyyy-MM-dd");
            var kho = cbbPhongKham.SelectedValue.ToString();

            _tbToaThuoc = _reportBo.baoCaoXuatNhapTon(kho, quy, nam, tuNgay, denNgay);

            // Create a report data source for the sales order data
            ReportDataSource dsToaThuoc = new ReportDataSource();
            dsToaThuoc.Name = "dsBaoCaoXuatNhapTon";
            dsToaThuoc.Value = _tbToaThuoc;

            localReport.DataSources.Add(dsToaThuoc);
            // Refresh the report
            rvBaoCaoTTBHYT.RefreshReport();
            this.rvBaoCaoTTBHYT.RefreshReport();
        }


        private void ckbBaoCaoTheoQuyNam_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbBaoCaoTheoQuyNam.Checked)
            {
                ckbBaoCaoTheoNgay.Checked = false;
                txtNam.Enabled = true;
                txtQuy.Enabled = true;

            }
            else
            {
                txtNam.Enabled = false;
                txtQuy.Enabled = false;
                ckbBaoCaoTheoNgay.Checked = true;
            }
        }

        private void ckbBaoCaoTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbBaoCaoTheoNgay.Checked)
            {
                dtpTuNgay.Enabled = true;
                dtpDenNgay.Enabled = true;
                ckbBaoCaoTheoQuyNam.Checked = false;
            }
            else
            {
                dtpTuNgay.Enabled = false;
                dtpDenNgay.Enabled = false;
                ckbBaoCaoTheoQuyNam.Checked = true;
            }
        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {
            RunReport();
        }
    }
}