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
//using reports.Baocao;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaodoanhthutiemchung : Form
    {
        public DataTable _dtData = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        private string thamso = "";
        public frm_baocaodoanhthutiemchung(string sThamso)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            dtNgayInPhieu.Value = globalVariables.SysDate;
            thamso = sThamso;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }
        void Initevents()
        {
            this.KeyDown += new KeyEventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown);
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            chkByDate.CheckedChanged += new EventHandler(chkByDate_CheckedChanged);
            this.Load += new EventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
            chkChitiet.CheckedChanged+=new EventHandler(chkChitiet_CheckedChanged);
            ShowGrid();
        }
        void chkChitiet_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }
        void ShowGrid()
        {
            if (chkChitiet.Checked)
            {
                grdChitiet.BringToFront();
                baocaO_TIEUDE1.Init("baocao_thutien_tiemchung_chitiet");
            }
            else
            {
                grdList.BringToFront();
                baocaO_TIEUDE1.Init("baocao_thutien_tiemchung_tonghop");
            }
        }
        DataTable m_dtKhoathucHien=new DataTable();
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);
                DataBinding.BindDataCombobox(cboNhanvien, THU_VIEN_CHUNG.LaydanhsachThunganvien(),
                                      DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "Chọn nhân viên thu ngân", true);
                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                
                DataBinding.BindDataCombobox(cboKhoa, m_dtKhoathucHien,
                                     DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa KCB", true);
                var query = from khoa in m_dtKhoathucHien.AsEnumerable()
                            where Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) == globalVariables.MA_KHOA_THIEN
                            select khoa;
                if (query.Count() > 0)
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
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
               
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls",tieude);
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
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            string kieutimkiem = "-1";
            if (chktienkham.Checked == true && chktientiem.Checked == false && chktienvacxin.Checked == false && chktienxetnghiem.Checked == false)
            {
                kieutimkiem = "1";
            }
            if (chktienkham.Checked == true && chktientiem.Checked == true && chktienvacxin.Checked == false && chktienxetnghiem.Checked == false)
            {
                kieutimkiem = "1,11";
            }
            if (chktienkham.Checked == false && chktientiem.Checked == true && chktienvacxin.Checked == false && chktienxetnghiem.Checked == false)
            {
                kieutimkiem = "11";
            }
            if (chktienkham.Checked == true && chktientiem.Checked == false && chktienvacxin.Checked == true && chktienxetnghiem.Checked == false)
            {
                kieutimkiem = "1,3";
            }
            if (chktienkham.Checked == false && chktientiem.Checked == true && chktienvacxin.Checked == true && chktienxetnghiem.Checked == false)
            {
                kieutimkiem = "11,3";
            }
            if (chktienkham.Checked == false && chktientiem.Checked == true && chktienvacxin.Checked == true && chktienxetnghiem.Checked == false)
            {
                kieutimkiem = "11,3";
            }
            if (chktienkham.Checked == true && chktientiem.Checked == true && chktienvacxin.Checked == true && chktienxetnghiem.Checked == false)
            {
                kieutimkiem = "11,3,1";
            }
            if (chktienkham.Checked == false && chktientiem.Checked == true && chktienvacxin.Checked == true && chktienxetnghiem.Checked == true)
            {
                kieutimkiem = "11,3,2";
            }
            if (chktienkham.Checked == false && chktientiem.Checked == false && chktienvacxin.Checked == true && chktienxetnghiem.Checked == true)
            {
                kieutimkiem = "3,2";
            }
            if (chktienkham.Checked == false && chktientiem.Checked == false && chktienvacxin.Checked == false && chktienxetnghiem.Checked == true)
            {
                kieutimkiem = "2";
            }
            if (chktienkham.Checked == false && chktientiem.Checked == true && chktienvacxin.Checked == false && chktienxetnghiem.Checked == true)
            {
                kieutimkiem = "11,2";
            }
            if (chktienkham.Checked == false && chktientiem.Checked == true && chktienvacxin.Checked == false && chktienxetnghiem.Checked == true)
            {
                kieutimkiem = "11,2";
            }
            if (chktienkham.Checked == true && chktientiem.Checked == false && chktienvacxin.Checked == false && chktienxetnghiem.Checked == true)
            {
                kieutimkiem = "1,2";
            }
            if (chkChitiet.Checked)
            {
                _dtData =
                    BAOCAO_NGOAITRU.BaocaoThutientiemchungchitiet(
                        chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                        chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                        Utility.sDbnull(cboDoituongKCB.SelectedValue, -1),
                        Utility.sDbnull(cboNhanvien.SelectedValue, -1),
                        Utility.sDbnull(cboLoaidichvu.SelectedValue, ""), 
                        Utility.sDbnull(thamso, ""),
                        Utility.sDbnull(cboKhoa.SelectedValue, -1),kieutimkiem);
                THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_thutien_tiemchung_chitiet.xml");
                Utility.SetDataSourceForDataGridEx(grdChitiet, _dtData, false, true, "1=1", "");
                Janus.Windows.GridEX.GridEXColumn gridExColumnTientong = grdChitiet.RootTable.Columns["Thanh_Tien"];
                tong_tien = Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumnTientong, Janus.Windows.GridEX.AggregateFunction.Sum));
            }
            else
            {
                _dtData =
                    BAOCAO_NGOAITRU.BaocaoThutientiemchungtonghop(
                        chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                        chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                        Utility.sDbnull(cboDoituongKCB.SelectedValue, -1),
                        Utility.sDbnull(cboNhanvien.SelectedValue, -1),
                        Utility.Int16Dbnull(cboLoaidichvu.SelectedValue, -1), Utility.sDbnull(thamso,""), Utility.sDbnull(cboKhoa.SelectedValue, -1));
                THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_thutien_tiemchung_tonghop.xml");
                Utility.SetDataSourceForDataGridEx(grdList, _dtData, false, true, "1=1", "");
                Janus.Windows.GridEX.GridEXColumn gridExColumnTientong = grdList.RootTable.Columns["Thanh_Tien"];
                tong_tien = Utility.DecimaltoDbnull(grdList.GetTotal(gridExColumnTientong, Janus.Windows.GridEX.AggregateFunction.Sum));
            }
          
            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo", MessageBoxIcon.Information);
                return;
            }
         
            if (chktienkham.Checked == false && chktientiem.Checked == false && chktienvacxin.Checked == true)
            {
                kieutimkiem = "3";
            }

            Utility.UpdateLogotoDatatable(ref _dtData);
            string condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Nhân viên :{3} - Phương thức thanh toán: {4}" , dtFromDate.Text, dtToDate.Text,
                                          cboDoituongKCB.SelectedIndex >= 0
                                              ? Utility.sDbnull(cboDoituongKCB.Text)
                                              : "Tất cả",
                                          cboNhanvien.SelectedIndex > 0
                                              ? Utility.sDbnull(cboNhanvien.Text)
                                              : "Tất cả", cboLoaidichvu.SelectedIndex>0 ?Utility.sDbnull(cboLoaidichvu.Text):"Tất cả");
            var crpt = Utility.GetReport(chkChitiet.Checked ? "baocao_thutien_tiemchung_chitiet" : "baocao_thutien_tiemchung_tonghop", ref tieude, ref reportname);
            if (crpt == null) return;

            string staffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) staffName = globalVariables.UserName;
            try
            {
                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count <= 0 ? false : true);
                //try
                //{
                crpt.SetDataSource(_dtData);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = chkChitiet.Checked ? "baocao_thutien_tiemchung_chitiet" : "baocao_thutien_tiemchung_tonghop";
                crpt.SetParameterValue("StaffName", staffName);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                crpt.SetParameterValue("Address", globalVariables.Branch_Address);
                crpt.SetParameterValue("Phone", globalVariables.Branch_Phone);
                crpt.SetParameterValue("FromDateToDate", condition);
                crpt.SetParameterValue("sTitleReport", tieude);
                crpt.SetParameterValue("sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                       Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                          globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);

            }
        }
    }
}
