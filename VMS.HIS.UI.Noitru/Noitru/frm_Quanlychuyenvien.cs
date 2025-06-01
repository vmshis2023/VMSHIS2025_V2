using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.Forms.Noitru;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.Forms.NGOAITRU;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Quanlychuyenvien : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
        public frm_Quanlychuyenvien()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
           
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtMaluotkham.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_Quanlychuyenvien_Load;
            KeyDown += frm_Quanlychuyenvien_KeyDown;
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Quanlychuyenvien_Load(object sender, EventArgs e)
        {
            
            InitData();
            TimKiemThongTin();
            ModifyCommand();
            
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
            DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).Where(DmucBenhvien.Columns.Trangthai).IsEqualTo(1).OrderAsc(DmucBenhvien.Columns.SttHthi).ExecuteDataSet().Tables[0];
            txtNoichuyenden.Init(m_dtBenhvien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });
            LaydanhsachBacsi();
            dtpNgayin.Value = globalVariables.SysDate;

        }
        private void LaydanhsachBacsi()
        {
            try
            {
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                              new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }
            }
            catch (Exception exception)
            {
                // throw;
            }

        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdList);
            cmdUpdate.Enabled = cmdDelete.Enabled =cmdPrint.Enabled= isValid;
           
        }

        private void TimKiemThongTin()
        {
            string tungay=chkByDate.Checked ? dtFromDate.Value.ToString("dd/MM/yyyy") : "01/01/1900";
            string denngay=chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900";
            string ma_luotkham=(Utility.DoTrim(txtMaluotkham.Text));
            string ten_benhnhan=(Utility.DoTrim(txtTennguoibenh.Text));
            int noichuyenden=Utility.Int32Dbnull(txtNoichuyenden.MyID);
            int bschuyen=Utility.Int32Dbnull(txtBacsi.MyID);
            byte lydochuyen=Utility.ByteDbnull(cboTrangthai.SelectedValue);
            if (ma_luotkham.Length > 0)
            {
                tungay = denngay = "01/01/1900";
                ten_benhnhan = "";
                noichuyenden = -1;
                bschuyen = -1;
                lydochuyen = 10;
            }
            m_dtData = SPs.KcbLaydanhsachphieuchuyenvien(tungay, denngay, -1l, ma_luotkham,ten_benhnhan,noichuyenden,bschuyen,lydochuyen).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngay_chuyenvien,so_chuyentuyen");
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện trạng thái của tmf kiếm từ ngày đến ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
      
        /// <summary>
        /// hàm thưc hiện việc tìm kiếm htoong tin nhanh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadMaLanKham();
                chkByDate.Checked = false;
                cmdTimKiem.PerformClick();
            }
        }
        private void LoadMaLanKham()
        {
            MaLuotkham = Utility.sDbnull(txtMaluotkham.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtMaluotkham.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                txtMaluotkham.Text = MaLuotkham;
                txtMaluotkham.Select(txtMaluotkham.Text.Length, txtMaluotkham.Text.Length);
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_Quanlychuyenvien_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtMaluotkham.Focus();
                txtMaluotkham.SelectAll();
            }
            if(e.KeyCode==Keys.N&&e.Control)cmdInsert.PerformClick();
            if(e.KeyCode==Keys.U&&e.Control)cmdUpdate.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.KeyCode == Keys.P && e.Control) cmdPrint.PerformClick();
        }
     
        KcbLuotkham objKcbLuotkham = null;
       

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frm_chuyenvien _chuyenvien = new frm_chuyenvien();
            _chuyenvien.m_enAct = action.Insert;
           _chuyenvien.ShowDialog();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            frm_chuyenvien _chuyenvien = new frm_chuyenvien();
            _chuyenvien.ucThongtinnguoibenh1.txtMaluotkham.Text = grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString();
            _chuyenvien.ucThongtinnguoibenh1.Refresh();
            _chuyenvien.ShowDialog();

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                KcbPhieuchuyenvien objphieucv = KcbPhieuchuyenvien.FetchByID(Utility.Int32Dbnull(grdList.GetValue(KcbPhieuchuyenvien.Columns.IdPhieu), -1));
                if (objphieucv == null)
                {
                    Utility.ShowMsg(string.Format("Phiếu chuyển viện của người bệnh {0} có thể đã bị người khác xóa ở chức năng khác. Vui lòng kiểm tra lại bằng cách nhấn nút tìm kiếm", grdList.GetValue("ten_benhnhan").ToString()));
                    return;
                }
                KcbLuotkham objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString());
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu chuyển viện trên lưới trước khi thực hiện hủy chuyển viện");
                    return;
                }
                if (objLuotkham.TrangthaiNoitru == 4)
                {
                    Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để ra viện nên bạn không thể hủy chuyển viện");
                    return;
                }
                if (objLuotkham.TrangthaiNoitru == 5)
                {
                    Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể hủy chuyển viện");
                    return;
                }
                if (objLuotkham.TrangthaiNoitru == 6)
                {
                    Utility.ShowMsg("Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể hủy chuyển viện");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy chuyển viện cho phiếu {0} của người bệnh {1} hay không?", grdList.GetValue(KcbPhieuchuyenvien.Columns.SoChuyentuyen).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận hủy chuyển viện", true))
                {
                    try
                    {
                        using (var scope = new TransactionScope())
                        {
                            using (var dbscope = new SharedDbConnectionScope())
                            {
                                objLuotkham.TthaiChuyendi = 0;
                                objLuotkham.IdBenhvienDi = -1;
                                objLuotkham.IdBacsiChuyenvien = -1;
                                //objLuotkham.NgayRavien = null;
                                objLuotkham.IsNew = false;
                                objLuotkham.MarkOld();
                                objLuotkham.Save();
                                new Delete().From(KcbPhieuchuyenvien.Schema).Where(KcbPhieuchuyenvien.Columns.IdPhieu).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(KcbPhieuchuyenvien.Columns.IdPhieu), -1)).Execute();

                            }
                            scope.Complete();
                        }
                        Utility.ShowMsg(string.Format("Hủy chuyển viện cho bệnh nhân {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", KcbPhieuchuyenvien.Columns.IdPhieu, grdList.GetValue(KcbPhieuchuyenvien.Columns.IdPhieu)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                    catch (Exception ex)
                    {
                        Utility.CatchException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                string ma_luotkham = grdList.GetValue(KcbPhieuchuyenvien.Columns.MaLuotkham).ToString();
                long id_phieu = Utility.Int64Dbnull(grdList.GetValue(KcbPhieuchuyenvien.Columns.IdPhieu));
                DataTable dtData =
                                 SPs.KcbThamkhamPhieuchuyenvien(id_phieu, ma_luotkham).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(dtData, "thamkham_phieuchuyenvien.XML");
                Utility.UpdateLogotoDatatable(ref dtData);
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport("thamkham_phieuchuyenvien", ref tieude, ref reportname);
                if (crpt == null) return;
                try
                {

                    frmPrintPreview objForm = new frmPrintPreview("PHIẾU CHUYỂN TUYẾN", crpt, true, dtData.Rows.Count <= 0 ? false : true);
                    crpt.SetDataSource(dtData);

                    objForm.mv_sReportFileName = Path.GetFileName(reportname);
                    objForm.mv_sReportCode = "thamkham_phieuchuyenvien";
                    Utility.SetParameterValue(crpt, "StaffName", StaffName);
                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                    Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                    Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithThanhPho(dtpNgayin.Value));
                    Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                    Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                    objForm.crptViewer.ReportSource = crpt;
                    objForm.ShowDialog();

                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }
                finally
                {
                    Utility.DefaultNow(this);
                    GC.Collect();
                    Utility.FreeMemory(crpt);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtpNgayin.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtTennguoibenh.Clear();
            txtBacsi.SetId(-1);
            txtNoichuyenden.SetId(-1);
            cboTrangthai.SelectedValue = 10;
            txtMaluotkham.Focus();

        }
     
    }
}
