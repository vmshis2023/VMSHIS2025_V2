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
using VNS.HIS.DAL;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaodanhsachbenhnhantheodotuoi : Form
    {
        public DataTable _dtData = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        private string RowFiler = "1=1";
        public frm_baocaodanhsachbenhnhantheodotuoi()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }
        void Initevents()
        {
            this.KeyDown += new KeyEventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown);
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            chkByDate.CheckedChanged += new EventHandler(chkByDate_CheckedChanged);
            this.Load += new EventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
          
        }
      
        DataTable m_dtKhoathucHien=new DataTable();
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);
                
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

            }
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init();
                _dtData =
                   BAOCAO_NGOAITRU.BaoCaoThongKeBNTheoDotuoiCT(Utility.Int32Dbnull(cboDoituongKCB.SelectedValue, -1),
                    chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                    chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,Utility.Int32Dbnull(cboGT.SelectedValue,-1), 
                    Utility.sDbnull(cboKhoa.SelectedValue, -1));

                Utility.SetDataSourceForDataGridEx(grdChitiet, _dtData, false, true, "1=1", "");

                if (chkDoTuoi.Checked)
                {
                    if (string.IsNullOrEmpty(txtTuTuoi.Text) && string.IsNullOrEmpty(txtDenTuoi.Text))
                    {
                        Utility.ShowMsg("Bạn phải chọn khoảng tuổi", "Thông báo");
                        txtDenTuoi.Focus();
                        return;
                    }
                    else
                    {
                        RowFiler = "Tuoi >=" + Utility.Int32Dbnull(txtTuTuoi.Text, -1) + " and Tuoi <=" + Utility.Int32Dbnull(txtDenTuoi.Text, -1) + "";
                    }

                }
                else
                {
                    switch (cboEqual.SelectedIndex)
                    {
                        case 1:
                            RowFiler = "Tuoi=" + Utility.Int32Dbnull(txtDenTuoi.Text, -1);
                            break;
                        case 3:
                            RowFiler = "Tuoi<=" + Utility.Int32Dbnull(txtDenTuoi.Text, -1);
                            break;
                        case 2:
                            RowFiler = "Tuoi>=" + Utility.Int32Dbnull(txtDenTuoi.Text, -1);
                            break;
                        default:
                            RowFiler = "1=1";
                            break;


                    }
                }

            _dtData.DefaultView.RowFilter = RowFiler;
            if (_dtData.DefaultView.Count <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu  theo các điều kiện bạn chọn!");
                Utility.DefaultNow(this);
                cboDoituongKCB.Focus();
                return;
            }
            else
            {
                _dtData = _dtData.DefaultView.ToTable();
            }
           
            Utility.UpdateLogotoDatatable(ref _dtData);
           
           
            string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa KCB :{3} - Giới tính: {4}", dtFromDate.Text, dtToDate.Text,
                                          cboDoituongKCB.SelectedIndex >= 0
                                              ? Utility.sDbnull(cboDoituongKCB.Text)
                                              : "Tất cả",
                                          cboKhoa.SelectedIndex > 0
                                              ? Utility.sDbnull(cboKhoa.Text)
                                              : "Tất cả",cboGT.SelectedIndex>0?Utility.sDbnull(cboGT.Text):"Tất cả");
            string reportCode = "baocao_thongkebn_theodotuoi_ct";
            var crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count <= 0 ? false : true);
                //try
                //{
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(_dtData);
                Utility.SetParameterValue(crpt,"StaffName", StaffName);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt,"FromDateToDate", Condition);
                Utility.SetParameterValue(crpt,"sTitleReport", tieude);
                Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }

        private void chkDoTuoi_CheckedChanged(object sender, EventArgs e)
        {
            cboEqual.Visible = !chkDoTuoi.Checked;
            txtTuTuoi.Visible = chkDoTuoi.Checked;
            if (chkDoTuoi.Checked) txtTuTuoi.Focus();
            if (!chkDoTuoi.Checked) cboEqual.Focus();
        }
    }
}
