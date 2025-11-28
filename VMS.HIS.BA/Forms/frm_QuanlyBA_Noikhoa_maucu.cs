using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.HIS.UI.Baocao;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.Forms.NGOAITRU;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Aspose.Words;
using System.Diagnostics;
using System.Drawing;
using VMS.HIS.Bus;
using VNS.HIS.UI.NOITRU;
using VMS.HIS.Bus.Emr;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_QuanlyBA_Noikhoa_maucu : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
        string lstLoaiBA = "01";
        public EmrBa objEmrBa;
        DataTable dtkhoachuyen = new DataTable();
        DataTable dtkhoanhapvien = new DataTable();
        DataTable dtCacKhoa = new DataTable();
        DataTable dt_tssk = new DataTable();
        public frm_QuanlyBA_Noikhoa_maucu(string lstLoaiBA)
        {
            InitializeComponent();
            this.lstLoaiBA = lstLoaiBA;
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList, "id_ba", globalVariables.IsAdmin);
            grdList.SelectionChanged += GrdList_SelectionChanged;
            InitEvents();
        }

        private void GrdList_SelectionChanged(object sender, EventArgs e)
        {
            dtkhoachuyen = null;
            dtkhoanhapvien = null;
            dtCacKhoa = null;
            dt_tssk = null;
            objEmrBa = null;
            dtPhieuPttt = null;
        }

        void InitEvents()
        {
           
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtMaluotkham.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_QuanlyBA_Noikhoa_maucu_Load;
            KeyDown += frm_QuanlyBA_Noikhoa_maucu_KeyDown;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            
        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdUpdate.PerformClick();
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

        private void frm_QuanlyBA_Noikhoa_maucu_Load(object sender, EventArgs e)
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
            DataTable dtData =
                  new Select().From(DmucChung.Schema)
                      .Where(DmucChung.Columns.Loai).IsEqualTo("EMR_LOAIBA")
                      .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                      .And(DmucChung.Columns.Ma).IsEqualTo(lstLoaiBA)
                      .OrderAsc(DmucChung.Columns.SttHthi)
                      .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 1)
            {
                DataRow drLoaiBA = dtData.NewRow();
                drLoaiBA[DmucChung.Columns.Ten] = "---Chọn loại BA---";
                drLoaiBA[DmucChung.Columns.Ma] = "-1";

                dtData.Rows.InsertAt(drLoaiBA, 0);
            }
            DataBinding.BindDataCombobox(cboLoaiBA, dtData, "MA", "TEN");
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
            cmdUpdate.Enabled = cmdDelete.Enabled=isValid;
           
        }

        private void TimKiemThongTin()
        {
            DateTime tungay = chkByDate.Checked ? dtFromDate.Value : new DateTime(1900, 1, 1);
            DateTime denngay = chkByDate.Checked ? dtToDate.Value : new DateTime(1900, 1, 1);
            string ma_luotkham=(Utility.DoTrim(txtMaluotkham.Text));
            string ten_benhnhan=(Utility.DoTrim(txtTennguoibenh.Text));
            string ma_BA=Utility.DoTrim(txtmaBA.Text);
            int id_khoadieutri=Utility.Int32Dbnull(autoKhoa.MyID);
            if (ma_luotkham.Length > 0)
            {
                tungay = denngay = new DateTime(1900, 1, 1);
                ten_benhnhan = "";
                ma_BA = "";
                id_khoadieutri = -1;
            }
            m_dtData = SPs.EmrLaydanhsachBA(tungay, denngay, ma_luotkham, ma_BA, Utility.sDbnull(cboLoaiBA.SelectedValue), ten_benhnhan, id_khoadieutri).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngay_tao,ten_benhnhan");
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
        private void frm_QuanlyBA_Noikhoa_maucu_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtMaluotkham.Focus();
                txtMaluotkham.SelectAll();
            }
            if(e.KeyCode==Keys.U&&e.Control)cmdUpdate.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            //if (e.KeyCode == Keys.P && e.Control) cmdPrint.PerformClick();
        }
     
        KcbLuotkham objKcbLuotkham = null;
       

        private void cmdInsert_Click(object sender, EventArgs e)
        {
          
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            string MauBA = Utility.sDbnull(grdList.GetValue("loai_ba"));
            //if (MauBA == LoaiBA.BA_NOIKHOA)//Bệnh án nội khoa
            //{
                frm_BenhAn_NoiKhoa_BA BenhAn_NoiKhoa = new frm_BenhAn_NoiKhoa_BA(MauBA);
                EmrBa bant = EmrBa.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_ba")));
                BenhAn_NoiKhoa.objEmrBa = bant;
                BenhAn_NoiKhoa.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));
                BenhAn_NoiKhoa.m_enAct = action.Update;
                BenhAn_NoiKhoa._OnCreated += _OnCreated;
                BenhAn_NoiKhoa.ShowDialog();
            //}
           
        }
        EmrDocuments emrdoc = new EmrDocuments();
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("EMR_XOA_BENH_AN"))
                {
                    Utility.ShowMsg("Bạn không có quyền xóa Bệnh án ");
                    return;
                }
                 objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull(grdList.GetValue(EmrBa.Columns.IdBa)));
                if (objEmrBa == null)
                {
                    Utility.ShowMsg("Bệnh án không tồn tại để xóa. Vui lòng bấm lại nút tìm kiếm");
                    return;
                }
                EmrHosoluutru hosoba = new Select().From(EmrHosoluutru.Schema)
                    .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                    .And(EmrHosoluutru.Columns.MaBa).IsEqualTo(objEmrBa.MaBa)
                    .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                    .And(EmrHosoluutru.Columns.IdBenhnhan).IsEqualTo(objEmrBa.IdBenhnhan)
                     .And(EmrHosoluutru.Columns.MaLuotkham).IsEqualTo(objEmrBa.MaLuotkham)
                    .ExecuteSingle<EmrHosoluutru>();
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 1)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đóng nên không thể xóa. Muốn xóa cần quay về trạng thái mở");
                    return;
                }
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 2)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đóng và đã gửi KHTH phê duyệt nên không thể xóa");
                    return;
                }
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 3)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đã được duyệt bởi KHTH và đưa vào lưu trữ nên không thể xóa");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa Bệnh án với mã {0} của người bệnh {1} hay không?", grdList.GetValue(EmrBa.Columns.MaBa).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa bệnh án", true))
                {
                    try
                    {
                        using (var Scope = new TransactionScope())
                        {
                            using (var dbScope = new SharedDbConnectionScope())
                            {
                                new Delete().From(EmrBa.Schema)
                                      .Where(EmrBa.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                      .And(EmrBa.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                      .And(EmrBa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                      .Execute();
                                new Delete().From(EmrHosoluutru.Schema)
                                      .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                      .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                      .And(EmrBa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                      .Execute();
                                emrdoc.DeleteDocument_WithoutTransaction(objEmrBa.IdBa, new List<string>() { Utility.LayMaBA(Utility.sDbnull(grdList.GetValue("loai_ba"))), "BENHAN_BIA", "BENHAN_TO1", "BENHAN_TO2", "BENHAN_TO3", "BENHAN_TO4"}, "");
                                Utility.Log("frm_BenhAn_NoiKhoa", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công", objEmrBa.IdBa, objEmrBa.LoaiBa, objEmrBa.MaBa, objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
                            }
                            Scope.Complete();
                        }
                        Utility.ShowMsg(string.Format("Xóa Bệnh cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrBa.Columns.IdBa, grdList.GetValue(EmrBa.Columns.IdBa)));
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
                objEmrBa = null;
                Utility.CatchException(ex);
            }
        }
        void _OnCreated(long id,string ma_ba, action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.EmrLaydanhsachBA(new DateTime(1900, 1, 1), new DateTime(1900, 1, 1), "", ma_ba, Utility.sDbnull(cboLoaiBA.SelectedValue), "", -1).GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrBa.Columns.IdBa, grdList.GetValue(EmrBa.Columns.IdBa)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
                if (m_enAct == action.Insert && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtData.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (m_enAct == action.Update && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtData.Select("id_ba=" + id);
                    if (arrDr.Length > 0)
                    {
                        //arrDr[0]["chan_doan"] = dt_temp.Rows[0]["chan_doan"];
                        //arrDr[0]["phuongphap_vocam"] = dt_temp.Rows[0]["phuongphap_vocam"];
                        //arrDr[0]["phuongphap_giamdau"] = dt_temp.Rows[0]["phuongphap_giamdau"];
                        //arrDr[0]["ruiro_ghinhan"] = dt_temp.Rows[0]["ruiro_ghinhan"];
                        //arrDr[0]["ghichu_them"] = dt_temp.Rows[0]["ghichu_them"];
                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, "id_ba", id.ToString());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }
        bool DeleteMe()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Delete().From(EmrBa.Schema).Where(EmrBa.Columns.IdBa).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(EmrBa.Columns.IdBa), -1)).Execute();
                    }
                    scope.Complete();


                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Utility.WaitNow(this);
            //    string ma_luotkham = grdList.GetValue(EmrBa.Columns.MaLuotkham).ToString();
            //    long id_phieu = Utility.Int64Dbnull(grdList.GetValue(EmrBa.Columns.IdPhieu));
            //    DataTable dtData =
            //                     SPs.KcbThamkhamPhieuchuyenvien(id_phieu, ma_luotkham).GetDataSet().Tables[0];

            //    if (dtData.Rows.Count <= 0)
            //    {
            //        Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
            //        return;
            //    }
            //    THU_VIEN_CHUNG.CreateXML(dtData, "thamkham_phieuchuyenvien.XML");
            //    Utility.UpdateLogotoDatatable(ref dtData);
            //    string StaffName = globalVariables.gv_strTenNhanvien;
            //    if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

            //    string tieude = "", reportname = "";
            //    ReportDocument crpt = Utility.GetReport("thamkham_phieuchuyenvien", ref tieude, ref reportname);
            //    if (crpt == null) return;
            //    try
            //    {

            //        frmPrintPreview objForm = new frmPrintPreview("PHIẾU CHUYỂN TUYẾN", crpt, true, dtData.Rows.Count <= 0 ? false : true);
            //        crpt.SetDataSource(dtData);

            //        objForm.mv_sReportFileName = Path.GetFileName(reportname);
            //        objForm.mv_sReportCode = "thamkham_phieuchuyenvien";
            //        Utility.SetParameterValue(crpt, "StaffName", StaffName);
            //        Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
            //        Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
            //        Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
            //        Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
            //        Utility.SetParameterValue(crpt, "sTitleReport", tieude);
            //        Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithThanhPho(dtpNgayin.Value));
            //        Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
            //        Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
            //        objForm.crptViewer.ReportSource = crpt;
            //        objForm.ShowDialog();

            //    }
            //    catch (Exception ex)
            //    {
            //        Utility.CatchException(ex);
            //    }
            //    finally
            //    {
            //        Utility.DefaultNow(this);
            //        GC.Collect();
            //        Utility.FreeMemory(crpt);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtpNgayin.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtTennguoibenh.Clear();
            txtmaBA.Clear();
            txtmaBA.Focus();
            autoKhoa.SetId(-1);

        }
        KcbLuotkham objLuotkham = null;
        private void mnuInTo1_Click(object sender, EventArgs e)
        {
            InitData4Print();
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 1, false);
        }

        private void mnuInTo2_Click(object sender, EventArgs e)
        {
            InitData4Print();
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 2, false);
        }

        private void mnuInTo3_Click(object sender, EventArgs e)
        {
            InitData4Print();
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 3, false);
        }

        private void mnuInTo4_Click(object sender, EventArgs e)
        {
            InitData4Print();
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 4, false);
        }

        private void mnuPrintAll_Click(object sender, EventArgs e)
        {
            InitData4Print();
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 100, false);
        }

        private void mnuInTomtatBA_Click(object sender, EventArgs e)
        {
            objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                return;
            }
            EmrTomtatBa ttba = new Select().From(EmrTomtatBa.Schema)
                .Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTomtatBa>();
            if (ttba == null || ttba.Id <= 0)
            {
                Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                return;
            }
            clsInBA.InTomTatBA(ttba);
        }
      

        private void cmdTomtatBA_Click(object sender, EventArgs e)
        {
            try
            {
                objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in bệnh án");
                    return;
                }
                frm_TomtatBA _PhieuTTBA = new frm_TomtatBA();
                _PhieuTTBA.m_enAct = action.Insert;
                _PhieuTTBA.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                _PhieuTTBA.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                _PhieuTTBA.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
      void InitData4Print()
        {
            objLuotkham = Utility.getKcbLuotkhamFromGrid(grdList);
            FillThongtinChuyenKhoa();
            FillThongtinTienSuSanKhoa();
            FillThongtinPTTT();
            objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull(grdList.GetValue(EmrBa.Columns.IdBa)));
            globalVariables.dtSignInfor = SPs.EmrLaythongtinChukyTrenphieu(objEmrBa.IdBa.ToString(), "", 1).GetDataSet().Tables[0];
        }
        void FillThongtinChuyenKhoa()
        {

            dtCacKhoa = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "-1", -1);
            dtkhoachuyen = dtCacKhoa.Clone();
            DataRow[] arrKhoachuyen = dtCacKhoa.Select("id_chuyen>0");
            if (arrKhoachuyen.Length > 0) dtkhoachuyen = arrKhoachuyen.CopyToDataTable();
            DataRow[] arrKhoanhapvien = dtCacKhoa.Select("id_chuyen<=0");


            if (arrKhoanhapvien.Length > 0)
            {
                dtkhoanhapvien = arrKhoanhapvien.CopyToDataTable();
            }
        }
        void FillThongtinTienSuSanKhoa()
        {
            dt_tssk = new Select().From(EmrTiensuSankhoa.Schema)
                    .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteDataSet().Tables[0];
           
        }
        DataTable dtPhieuPttt = new DataTable();
        void FillThongtinPTTT()
        {
            dtPhieuPttt = SPs.EmrLaythongtinPhieuPtttTo4(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];

        }
        private void mnuInVoBA_Click(object sender, EventArgs e)
        {
            InitData4Print();
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, null, null, null, null, 0, false);
        }

        private void cmdDongBA_Click(object sender, EventArgs e)
        {
            try
            {
                objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác đóng bệnh án");
                    return;
                }
                if (objLuotkham.TrangthaiNoitru < 3)
                {
                    Utility.ShowMsg("Chưa xác nhận ra viện cho người bệnh nên không cho phép đóng Bệnh Án");
                    return;
                }
                SqlQuery sqlQuery = new Select().From<EmrHosoluutru>()
                       .Where(EmrHosoluutru.Columns.MaLuotkham)
                       .IsEqualTo(objLuotkham.MaLuotkham)
                       .And(EmrHosoluutru.Columns.IdBenhnhan)
                       .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
                EmrHosoluutru objhosoBA = sqlQuery.ExecuteSingle<EmrHosoluutru>();

                if (objhosoBA == null || objhosoBA.TrangThai > 1)
                {
                    Utility.ShowMsg(string.Format("Bệnh án đã được đóng bời người dùng {0} lúc {1} nên không thể thao tác.", objhosoBA.NguoiDong, objhosoBA.NgayDong.Value.ToString("dd/MM/yyyy HH:mm")));
                    return;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }

        private void cmdEmr_Click(object sender, EventArgs e)
        {
            frm_Emr _Emr = new frm_Emr();
            _Emr.isAutoLoad = true;
            _Emr.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));
            _Emr.ShowDialog();
        }

        private void cmdInsert_Click_1(object sender, EventArgs e)
        {
            frm_BenhAn_NoiKhoa_BA BenhAn_NoiKhoa = new frm_BenhAn_NoiKhoa_BA("BA-01");
            BenhAn_NoiKhoa.m_enAct = action.Insert;
            BenhAn_NoiKhoa._OnCreated += _OnCreated;
            BenhAn_NoiKhoa.ShowDialog();
        }

        private void mnuInTo5_Click(object sender, EventArgs e)
        {
            InitData4Print();
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 5, false);
        }

        private void mnuInTo6_Click(object sender, EventArgs e)
        {
            InitData4Print();
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 6, false);
        }

        private void mnu_InCacToConLai_Click(object sender, EventArgs e)
        {
            InitData4Print();
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, dt_tssk, dtPhieuPttt, 10, false);
        }
    }
}
