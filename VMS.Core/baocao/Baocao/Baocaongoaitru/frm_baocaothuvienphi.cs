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
using VNS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using Janus.Windows.GridEX;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaothuvienphi : Form
    {
        public DataTable _reportTable = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        public frm_baocaothuvienphi()
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
            this.Load += new System.EventHandler(this.frm_baocaothuvienphi_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_baocaothuvienphi_KeyDown);
            cmdChotDanhSach.Click += new EventHandler(cmdChotDanhSach_Click);
            cboHoadon.SelectedIndexChanged += new EventHandler(cboHoadon_SelectedIndexChanged);
            chkChitiet.CheckedChanged += new EventHandler(chkChitiet_CheckedChanged);
            ShowGrid();
        }

        void cmdChotDanhSach_Click(object sender, EventArgs e)
        {
            ActionResult actionResult = ActionResult.UNKNOW;
            if (radoChotMoi.Checked)
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn chốt các bản ghi thanh toán còn sót lại hay không?", "Xác nhận chốt thanh toán", true))
                    actionResult = new KCB_THANHTOAN().Chotbaocao(dtpNgayChot.Value, dtpNgayThanhToan.Value);
            }
            else
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn chốt các bản ghi thanh toán còn sót lại hay không?", "Xác nhận chốt thanh toán", true))
                    actionResult = new KCB_THANHTOAN().ChotVetbaocao(dtpNgayChot.Value, dtpNgayThanhToan.Value);
            }
            switch (actionResult)
            {
                case ActionResult.Success:
                    Utility.ShowMsg("Đã chốt thanh toán thành công");
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Có lỗi trong quá trình chốt thanh toán");
                    break;
            }

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
                baocaO_TIEUDE1.Init("baocao_thuvienphi_chitiet");
            }
            else
            {
                grdList.BringToFront();
                baocaO_TIEUDE1.Init("baocao_thuvienphi_tonghop");
            }
        }

        void cboHoadon_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataTable m_dtKhoathucHien = new DataTable();

        private void frm_baocaothuvienphi_Load(object sender, EventArgs e)
        {
            try
            {
               

                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);
                DataBinding.BindDataCombobox(cbonhanvien, THU_VIEN_CHUNG.LaydanhsachThunganvien(),
                                      DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "Chọn nhân viên thu ngân", true);
                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cbokhoa, m_dtKhoathucHien,
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

        MoneyByLetter _moneyByLetter = new MoneyByLetter();
        /// <summary>
        /// hàm thực hiện việc in phiếu xét nghiêm
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkChitiet.Checked)
                {
                    _reportTable =
                      BAOCAO_NGOAITRU.BaocaoThuvienphiChitiet(Utility.sDbnull(cbokhoa.SelectedValue, "-1"), chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                      chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate, Utility.sDbnull(cbonhanvien.SelectedValue, -1),
                      Utility.sDbnull(cboDoituongKCB.SelectedValue, -1),
                                                        Utility.Int32Dbnull(cboNTNT.SelectedValue, -1), Utility.Int32Dbnull(cboHoadon.SelectedValue, -1), chkLoaitimkiem.Checked ? 1 : 0);

                    Utility.SetDataSourceForDataGridEx(grdChitiet, _reportTable, false, true, "1=1", "");
                    //Janus.Windows.GridEX.GridEXColumn gridExColumnTientong = grdChitiet.RootTable.Columns["tong_Tien"];
                    //tong_tien = Utility.Int32Dbnull(grdList.GetTotal(gridExColumnTientong, Janus.Windows.GridEX.AggregateFunction.Sum));

                }
                else
                {
                    _reportTable =
                    BAOCAO_NGOAITRU.BaocaoThuvienphiTonghop(Utility.sDbnull(cbokhoa.SelectedValue, "-1"), chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                    chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate, Utility.sDbnull(cbonhanvien.SelectedValue, "-1"),
                    Utility.sDbnull(cboDoituongKCB.SelectedValue, "-1"),
                                                     Utility.Int32Dbnull(cboNTNT.SelectedValue, -1), Utility.Int32Dbnull(cboHoadon.SelectedValue, -1), chkLoaitimkiem.Checked ? 1 : 0);

                    Utility.SetDataSourceForDataGridEx(grdList, _reportTable, false, true, "1=1", "");
                    //Janus.Windows.GridEX.GridEXColumn gridExColumnTientong = grdList.RootTable.Columns["tong_Tien"];
                    //tong_tien = Utility.Int32Dbnull(grdList.GetTotal(gridExColumnTientong, Janus.Windows.GridEX.AggregateFunction.Sum));
                }
                THU_VIEN_CHUNG.CreateXML(_reportTable, chkChitiet.Checked ? "baocao_thuvienphi_chitiet" : "baocao_thuvienphi_tonghop");
                if (_reportTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
                string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} -TNV :{3} - Loại điều trị :{4} ", dtFromDate.Text, dtToDate.Text,
                                        cboDoituongKCB.SelectedIndex > 0
                                            ? Utility.sDbnull(cboDoituongKCB.Text)
                                            : "Tất cả",
                                        cbonhanvien.SelectedIndex > 0
                                            ? Utility.sDbnull(cbonhanvien.Text)
                                            : "Tất cả", cboNTNT.SelectedIndex > 0 ? Utility.sDbnull(cboNTNT.Text) : "Tất cả");
                Utility.UpdateLogotoDatatable(ref _reportTable);
                string reportCode = chkChitiet.Checked ? "baocao_thuvienphi_chitiet" : "baocao_thuvienphi_tonghop";
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
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi in báo cáo", ex);
            }
        }

       

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
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

            }
        }

       

        private void frm_baocaothuvienphi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

        private void grdList_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

      
    }
}
