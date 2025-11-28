using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using Microsoft.VisualBasic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using C1.C1Excel;
using Aspose.Cells;
using VNS.HIS.UI.Classess;

namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaodoanhthuphongkham_Hongphat_V2 : Form
    {
        public DataTable _dtData = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        public frm_baocaodoanhthuphongkham_Hongphat_V2()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
            
        }
        void Initevents()
        {
            cboReportType.SelectedIndex = 0;
            this.KeyDown += new KeyEventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown);
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            chkByDate.CheckedChanged += new EventHandler(chkByDate_CheckedChanged);
            this.Load += new EventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
          
        
        }

        private void cbo_loaidichvu_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtDvu = new Select().From(DmucDichvuclsChitiet.Schema).Where(DmucDichvuclsChitiet.Columns.IdDichvu).IsEqualTo(cbo_loaidichvu.SelectedValue).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cbo_dichvu, dtDvu,
                                     DmucDichvuclsChitiet.Columns.IdChitietdichvu, DmucDichvuclsChitiet.Columns.TenChitietdichvu, "-----Chọn-----", true);
        }

        private void cbo_pttt_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            cbo_nganhang.Enabled = lstPTTT.Contains(Utility.sDbnull( cbo_pttt.SelectedValue,"XYZ"));
            if (!cbo_nganhang.Enabled) cbo_nganhang.SelectedIndex = 0;
        }
      
       
        void ShowGrid(int idx)
        {

            if (idx == 0)
            {
                grd_tonghop.BringToFront();
                baocaO_TIEUDE1.Init("baocao_doanhthuphongkham_Chitiet_hongphat");
            }
            else if (idx == 1)
            {
                grdChitiet.BringToFront();
                baocaO_TIEUDE1.Init("baocao_doanhthuphongkham_tonghop_hongphat");

            }
            else if (idx == 2)
            {
                grd_bc_chitiet_theomathang.BringToFront();
                baocaO_TIEUDE1.Init("baocao_sochitietbanhang_theomathang");

            }
            else
            {
                grd_baocaothutienvienphi_kcb.BringToFront();
                baocaO_TIEUDE1.Init("baocao_thutienvienphi_kcb");

            }
        }
        void SetDataSource4Grid(int idx,DataTable _dtData)
        {

            if (idx == 0)
            {
              
                Utility.SetDataSourceForDataGridEx_Basic(grd_tonghop, _dtData, true, true, "1=1", "");
            }
            else if (idx == 1)
            {
               
                Utility.SetDataSourceForDataGridEx_Basic(grdChitiet, _dtData, true, true, "1=1", "");

            }
            else if (idx == 2)
            {
               
                Utility.SetDataSourceForDataGridEx_Basic(grd_bc_chitiet_theomathang, _dtData, true, true, "1=1", "");

            }
            else
            {
               
                Utility.SetDataSourceForDataGridEx_Basic(grd_baocaothutienvienphi_kcb, _dtData, true, true, "1=1", "");

            }
        }
        DataTable m_dtKhoathucHien=new DataTable();
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                cbo_loaibaocao.SelectedIndex = 0;
                ShowGrid(0);
                DataBinding.BindDataCombobox(cboNguongioithieu, THU_VIEN_CHUNG.LayDulieuDanhmucChung("NGUONGTHIEU", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten, "-----Chọn-----", true);
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "-----Chọn-----", true);
                DataBinding.BindDataCombobox(cbo_thunganvien, THU_VIEN_CHUNG.LaydanhsachThunganvien(),
                                      DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "-----Chọn-----", true);
                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cboKhoa, m_dtKhoathucHien,
                                     DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "-----Chọn-----", true);
                var query = from khoa in m_dtKhoathucHien.AsEnumerable()
                            where Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) == globalVariables.MA_KHOA_THIEN
                            select khoa;
                if (query.Count() > 0)
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
                DataTable dt_loaiDvu = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cbo_loaidichvu, dt_loaiDvu,
                                    DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.TenDichvu, "-----Chọn-----", true);
               
                DataTable dtDvu = new Select().From(DmucDichvuclsChitiet.Schema).ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cbo_dichvu, dtDvu,
                                   DmucDichvuclsChitiet.Columns.IdChitietdichvu, DmucDichvuclsChitiet.Columns.TenChitietdichvu, "-----Chọn-----", true);
                DataBinding.BindDataCombobox(cbo_nganhang, THU_VIEN_CHUNG.LayDulieuDanhmucChung("NGANHANG",true),
                                    DmucChung.Columns.Ma, DmucChung.Columns.Ten, "-----Chọn-----", true);
                DataBinding.BindDataCombobox(cbo_pttt, THU_VIEN_CHUNG.LayDulieuDanhmucChung("PHUONGTHUCTHANHTOAN", true),
                                    DmucChung.Columns.Ma, DmucChung.Columns.Ten, "-----Chọn-----", true);

                cbo_pttt_SelectedIndexChanged(cbo_pttt, eventArgs);
                
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!", ex);
            }

        }
        /// <summary>
        /// trạng thái của tìm kiếm từ ngày tới ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin của form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
            //  if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
        /// <summary>
        /// hàm thực hiên việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        MoneyByLetter _moneyByLetter = new MoneyByLetter();
        /// <summary>
        /// hàm thực hiện việc export excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkTachCDHA.Checked)
                {
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
                else
                {
                    saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                    saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
                    //saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string sPath = saveFileDialog1.FileName;
                        FileStream fs = new FileStream(sPath, FileMode.Create);
                        fs.CanWrite.CompareTo(true);
                        fs.CanRead.CompareTo(true);
                        gridEXExporter2.Export(fs);
                        fs.Dispose();
                    }
                    saveFileDialog1.Dispose();
                    saveFileDialog1.Reset();
                }
                

            }
            catch (Exception exception)
            {

            }
        }
        void BaocaoChung(string reportcode)
        {
            byte loai_baocao = Convert.ToByte(cbo_loaibaocao.SelectedIndex);
            
            _dtData =
                 BAOCAO_NGOAITRU.BaocaoDoanhthuphongkhamHongphat(
                     chkByDate.Checked ? dtFromDate.Value.Date : Convert.ToDateTime("01/01/1900"),
                     chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                     Utility.sDbnull(cboDoituongKCB.SelectedValue, -1),
                     Utility.sDbnull(cbo_thunganvien.SelectedValue, -1),Utility.ByteDbnull(cboLoaiDieutri.SelectedValue),"KKB",Utility.sDbnull(cbo_pttt.SelectedValue)
                     , Utility.sDbnull(cbo_nganhang.SelectedValue),"",loai_baocao,Utility.sDbnull(cboNguongioithieu.SelectedValue)
                     );
            SetDataSource4Grid(cbo_loaibaocao.SelectedIndex, _dtData);
            THU_VIEN_CHUNG.CreateXML(_dtData, reportcode + ".xml");
            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo", MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);


            string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Nhân viên :{3}", dtFromDate.Text, dtToDate.Text,
                                          cboDoituongKCB.SelectedIndex >= 0
                                              ? Utility.sDbnull(cboDoituongKCB.Text)
                                              : "Tất cả",
                                          cbo_thunganvien.SelectedIndex > 0
                                              ? Utility.sDbnull(cbo_thunganvien.Text)
                                              : "Tất cả");
            var crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count <= 0 ? false : true);
                //try
                //{
                crpt.SetDataSource(_dtData);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportcode;
                Utility.SetParameterValue(crpt,"ten_donvi_captren", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "ten_benhvien", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "dia_chi", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "dien_thoai", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "dieu_kien", Condition);
                Utility.SetParameterValue(crpt, "dieukientimkiem", Condition);
                Utility.SetParameterValue(crpt, "tu_ngay_den_ngay", string.Format("Từ ngày: {0} đến ngày: {1}",dtFromDate.Text,dtToDate.Text));
                Utility.SetParameterValue(crpt, "tieu_de", tieude);
                Utility.SetParameterValue(crpt, "tien_bangchu", new MoneyByLetter().sMoneyToLetter(tong_tien.ToString()));
                Utility.SetParameterValue(crpt, "ngay_in", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "thongtin_in", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);

                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {


            }
        }
       
     
        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            string report_code = "baocao_doanhthuphongkham_tonghop_Hongphat";
            if(cbo_loaibaocao.SelectedIndex == 0 )
                report_code = "baocao_doanhthuphongkham_tonghop_Hongphat";
            else if (cbo_loaibaocao.SelectedIndex == 1)
                report_code = "baocao_doanhthuphongkham_chitiet_Hongphat";
            else if(cbo_loaibaocao.SelectedIndex == 2)
                report_code = "baocao_sochitietbanhang_theomathang";
            else if (cbo_loaibaocao.SelectedIndex == 3)
                report_code = "baocao_thutienvienphi_kcb";
            BaocaoChung(report_code);
              
        }
        


        

        private void cbo_loaibaocao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGrid(cbo_loaibaocao.SelectedIndex);
        }

        


       
    }
}
