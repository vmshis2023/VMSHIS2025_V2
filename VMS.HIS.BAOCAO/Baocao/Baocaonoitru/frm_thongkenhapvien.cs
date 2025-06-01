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
    public partial class frm_thongkenhapvien : Form
    {
        public DataTable _reportTable = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        public frm_thongkenhapvien()
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
            this.Load += new System.EventHandler(this.frm_thongkenhapvien_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_thongkenhapvien_KeyDown);
        
        } 
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void ShowGrid()
        {
            if (radChitiet.Checked)
            {
                grdChitiet.BringToFront();
                
                baocaO_TIEUDE1.Init("baocao_nhapvien_chitiet");
            }
            else
            {
                grdTonghop.BringToFront();
                baocaO_TIEUDE1.Init("baocao_nhapvien_tonghop");
            }
        }
        DataTable m_dtKhoathucHien = new DataTable();

        private void frm_thongkenhapvien_Load(object sender, EventArgs e)
        {
            try
            {
               

                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);
                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                DataBinding.BindDataCombobox(cboKhoaNoiTru, m_dtKhoathucHien,
                                     DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa KCB", true);
                var query = from khoa in m_dtKhoathucHien.AsEnumerable()
                            where Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) == globalVariables.MA_KHOA_THIEN
                            select khoa;
                if (query.Count() > 0)
                {
                    cbokhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
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
                if(radChitiet.Checked)
                {

                    _reportTable =
                    BAOCAO_NGOAITRU.BaoCaoThongkeNhapvienChitiet(chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                    chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate, Utility.Int32Dbnull(cboDoituongKCB.SelectedValue,-1),
                    Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue, -1));
                    THU_VIEN_CHUNG
                        .CreateXML(_reportTable, "baocao_nhapvien_chitiet.XML");
                    Utility.SetDataSourceForDataGridEx(grdChitiet, _reportTable, false, true, "1=1", "");
                }
                else
                {
                    _reportTable =
                 BAOCAO_NGOAITRU.BaoCaoThongkeNhapvienTonghop(chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                    chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate, Utility.Int32Dbnull(cboDoituongKCB.SelectedValue, -1),
                    Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue, -1));
                    THU_VIEN_CHUNG
                       .CreateXML(_reportTable, "baocao_nhapvien_tonghop.XML");
                    Utility.SetDataSourceForDataGridEx(grdTonghop, _reportTable, false, true, "1=1", "");
                }
                    
                if (_reportTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
                string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa: {3} ", dtFromDate.Text, dtToDate.Text,
                                        cboDoituongKCB.SelectedIndex > 0
                                            ? Utility.sDbnull(cboDoituongKCB.Text)
                                            : "Tất cả", cboKhoaNoiTru.SelectedIndex > 0 ? Utility.sDbnull(cboKhoaNoiTru.Text) : "Tất cả");
                Utility.UpdateLogotoDatatable(ref _reportTable);
                string reportCode = radChitiet.Checked ? "baocao_nhapvien_chitiet" : "baocao_nhapvien_tonghop";
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
                if (grdTonghop.RowCount <= 0||grdChitiet.RowCount <=0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    return;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    FileStream fs = new FileStream(sPath, FileMode.Create);
                    fs.CanWrite.CompareTo(true);
                    fs.CanRead.CompareTo(true);
                    gridEXExporter1.Export(fs);
                    fs.Dispose();
                }
                saveFileDialog1.Dispose();
                saveFileDialog1.Reset();

            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi" + exception.Message);
            }
        }

       

        private void frm_thongkenhapvien_KeyDown(object sender, KeyEventArgs e)
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
