using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using Janus.Windows.GridEX;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaotinhhinhnoitru : Form
    {
        public DataTable _reportTable = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        public frm_baocaotinhhinhnoitru()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }
        void Initevents()
        {
            this.cmdExportToExcel.Click += new System.EventHandler(this.cmdExportToExcel_Click);
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.Load += new System.EventHandler(this.frm_baocaotinhhinhnoitru_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_baocaotinhhinhnoitru_KeyDown);
            grdChitiet.CellValueChanged += GrdChitiet_CellValueChanged;
            grdChitiet.CellUpdated += GrdChitiet_CellUpdated;
        }

        private void GrdChitiet_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                int num = new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.MotaThem).EqualTo(Utility.sDbnull(grdChitiet.GetValue(KcbLuotkham.Columns.MotaThem)))
                      .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(grdChitiet.GetValue(KcbLuotkham.Columns.IdBenhnhan)))
                      .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(grdChitiet.GetValue(KcbLuotkham.Columns.MaLuotkham)))
                      .Execute();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void GrdChitiet_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void ShowGrid()
        {
            //if (optNhapvien.Checked)
            //{
            //    grdChitiet.BringToFront();
                
            //    baocaO_TIEUDE1.Init("baocao_nhapvien_chitiet");
            //}
            
        }
        DataTable m_dtKhoathucHien = new DataTable();

        private void frm_baocaotinhhinhnoitru_Load(object sender, EventArgs e)
        {
            try
            {
               

                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);
                m_dtKhoathucHien = THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1);// THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                DataBinding.BindDataCombobox(cboKhoaNoiTru, m_dtKhoathucHien,
                                     DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa KCB", true);
                var query = from khoa in m_dtKhoathucHien.AsEnumerable()
                            where Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) == globalVariables.MA_KHOA_THIEN
                            select khoa;
                if (query.Count() > 0)
                {
                    cboKhoaNoiTru.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
                m_blnhasLoaded = true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!",ex);
            }

        }

        /// <summary>
        /// hàm thực hiện việc in báo cáo thống kê chuyển viện
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                byte loai_baocao=Utility.ByteDbnull( optNhapvien.Checked?1:(optNamvien.Checked?2:3),100);
                

                    _reportTable =
                    BAOCAO_NGOAITRU.BaocaoTinhinhNhapvien(chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                    chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate, Utility.Int32Dbnull(cboDoituongKCB.SelectedValue,-1),
                    Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue, -1), loai_baocao,Utility.Bool2byte(chkDangdieutri.Checked));
                    THU_VIEN_CHUNG
                        .CreateXML(_reportTable, "BaocaoTinhinhNhapvien.XML");
                    Utility.SetDataSourceForDataGridEx(grdChitiet, _reportTable, false, true, "1=1", "");
                if (_reportTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
                string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa: {3} - Tình trạng: {4}", dtFromDate.Text, dtToDate.Text,
                                        cboDoituongKCB.SelectedIndex > 0
                                            ? Utility.sDbnull(cboDoituongKCB.Text)
                                            : "Tất cả", cboKhoaNoiTru.SelectedIndex > 0 ? Utility.sDbnull(cboKhoaNoiTru.Text) : "Tất cả", loai_baocao==1?"Nhập viện":(loai_baocao==2?"Nằm viện":"Ra viện"));
                Utility.UpdateLogotoDatatable(ref _reportTable);
                string reportCode = "BaocaoTinhinhNhapvien";
                var crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                if (crpt == null) return;


                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, _reportTable.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(_reportTable);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "loai_baocao", loai_baocao);
                Utility.SetParameterValue(crpt, "FromDateToDate", Condition);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());

                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch(Exception ex)
            {
                Utility.CatchException("Lỗi khi in báo cáo",ex);
            }
        }

       

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                if (grdChitiet.RowCount <=0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    return;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        gridEXExporter1.GridEX = grdChitiet;
                        gridEXExporter1.Export(s);

                    }
                    Utility.ShowMsg("Xuất Excel thành công. Nhấn OK để mở file");
                    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                }
                
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi" + exception.Message);
            }
        }

       

        private void frm_baocaotinhhinhnoitru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

        private void radChuyenDi_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void radChuyenDen_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }

    }
}
