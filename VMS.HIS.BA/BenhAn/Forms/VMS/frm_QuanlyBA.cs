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

namespace VNS.HIS.UI.BA
{
    public partial class frm_QuanlyBA : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
        string lstLoaiBA = "01";
        public frm_QuanlyBA(string lstLoaiBA)
        {
            InitializeComponent();
            this.lstLoaiBA = lstLoaiBA;
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList, "id_ba", globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
           
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtMaluotkham.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_QuanlyBA_Load;
            KeyDown += frm_QuanlyBA_KeyDown;
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

        private void frm_QuanlyBA_Load(object sender, EventArgs e)
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
            m_dtData = SPs.EmrBaLaydanhsachBA(tungay, denngay, ma_luotkham, ma_BA, ten_benhnhan, id_khoadieutri).GetDataSet().Tables[0];
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
        private void frm_QuanlyBA_KeyDown(object sender, KeyEventArgs e)
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
            //if (e.KeyCode == Keys.P && e.Control) cmdPrint.PerformClick();
        }
     
        KcbLuotkham objKcbLuotkham = null;
       

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frm_BenhAn_NoiKhoa BenhAn_NoiKhoa = new frm_BenhAn_NoiKhoa(lstLoaiBA);
            BenhAn_NoiKhoa.m_enAct = action.Insert;
            BenhAn_NoiKhoa.ShowDialog();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            frm_BenhAn_NoiKhoa BenhAn_NoiKhoa = new frm_BenhAn_NoiKhoa(lstLoaiBA);
            EmrBa bant = EmrBa.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_ba")));
            BenhAn_NoiKhoa.objEmrBa=bant;
            BenhAn_NoiKhoa.ucThongtinnguoibenh_v31.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));
            BenhAn_NoiKhoa.m_enAct = action.Update;
            BenhAn_NoiKhoa.ShowDialog();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("kcb_EmrBa_xoa"))
                {
                    Utility.ShowMsg("Bạn không có quyền xóa Bệnh án nội trú");
                    return;
                }
                EmrBa objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull(grdList.GetValue(EmrBa.Columns.IdBa)));
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
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa Bệnh án nội trú với mã {0} của người bệnh {1} hay không?", grdList.GetValue(EmrBa.Columns.MaBa).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa bệnh án", true))
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
                                Utility.Log("frm_BenhAn_NoiKhoa", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công", objEmrBa.IdBa, objEmrBa.LoaiBa, objEmrBa.MaBa, objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
                            }
                            Scope.Complete();
                        }
                        Utility.ShowMsg(string.Format("Xóa Bệnh án nội trú cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
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
                Utility.CatchException(ex);
            }
        }
        void PhieutuvanPTTT__OnCreated(long id,string ma_ba, action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.EmrBaLaydanhsachBA(new DateTime(1900, 1, 1), new DateTime(1900, 1, 1), "", ma_ba, "", -1).GetDataSet().Tables[0];
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
            InBA(1);
        }

        private void mnuInTo2_Click(object sender, EventArgs e)
        {
            InBA(2);
        }

        private void mnuInTo3_Click(object sender, EventArgs e)
        {
            InBA(3);
        }

        private void mnuInTo4_Click(object sender, EventArgs e)
        {
            InBA(4);
        }

        private void mnuPrintAll_Click(object sender, EventArgs e)
        {
            InBA(100);
        }

        private void mnuInTomtatBA_Click(object sender, EventArgs e)
        {
            objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                return;
            }
            EmrTongketBenhan ttba = new Select().From(EmrTongketBenhan.Schema)
                .Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTongketBenhan>();
            if (ttba == null || ttba.Id <= 0)
            {
                Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                return;
            }
            clsInBA.InTomTatBA(ttba);
        }
        private void InBA(int toBA)
        {
            try
            {
                DataTable dtkhoachuyen = new DataTable();
                DataTable dtkhoanhapvien = new DataTable();
                
                 objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in bệnh án");
                    return;
                }
                SqlQuery sqlQuery = new Select().From<EmrBa>()
                    .Where(EmrBa.Columns.MaLuotkham)
                    .IsEqualTo(objLuotkham.MaLuotkham)
                    .And(EmrBa.Columns.IdBenhnhan)
                    .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
                EmrBa objEmrBa = sqlQuery.ExecuteSingle<EmrBa>();

                if (objEmrBa == null || objEmrBa.IdBa <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo Bệnh án nội trú trước khi thực hiện in");
                    return;
                }
                DataTable dtCacKhoa = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "-1", -1);
                dtkhoachuyen = dtCacKhoa.Clone();
                DataRow[] arrKhoachuyen = dtCacKhoa.Select("id_chuyen>0");
                if (arrKhoachuyen.Length > 0) dtkhoachuyen = arrKhoachuyen.CopyToDataTable();
                DataRow[] arrKhoanhapvien = dtCacKhoa.Select("id_chuyen<=0");
                NoitruPhieunhapvien objNhapvien = new Select().From(NoitruPhieunhapvien.Schema)
                    .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();

                if (arrKhoanhapvien.Length > 0)
                {
                    dtkhoanhapvien = arrKhoanhapvien.CopyToDataTable();
                }

                DataTable dtData = SPs.EmrBaLaythongtinIn(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham).GetDataSet().Tables[0];
                DataRow drData = dtData.Rows[0];
                List<string> lstcheckboxfields = new List<string>();
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                foreach (string chkField in lstcheckboxfields)
                {
                    dicMF.Add(chkField, Utility.Byte2Bool(drData[chkField]) ? "0" : "1");
                }
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                NoitruPhieuravien _phieuravien = new Select().From(NoitruPhieuravien.Schema)
               .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
                NoitruPhieunhapvien _phieunv = new Select().From(NoitruPhieunhapvien.Schema)
               .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                dtData.TableName = "BA_NOITRU";
                Document doc;
                drData["ten_dvicaptren"] = globalVariables.ParentBranch_Name.ToUpper();
                drData["ten_benhvien"] = globalVariables.Branch_Name.ToUpper();
                drData["p102"] = globalVariables.Branch_Name;
                drData["p101"] = globalVariables.ParentBranch_Name;
                drData["p132"] = _phieunv != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(_phieunv.NgayNhapvien, "") : ".......... giờ ....... ngày ........./........./.............";//Vào viện
                if (dtkhoanhapvien.Rows.Count > 0)
                {
                    drData["p141"] = Utility.FormatDateTime_giophut_ngay_thang_nam(Convert.ToDateTime(dtkhoanhapvien.Rows[0]["ngay_vaokhoa"]), "");//vào khoa
                    drData["p141_1"] = Utility.sDbnull(dtkhoanhapvien.Rows[0]["so_luong"], "0");
                }
                drData["p128"] = Utility.FormatDateTime(Utility.sDbnull(drData["p128"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                drData["p145_1"] = _phieuravien != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(_phieuravien.NgayRavien, "") : ".......... giờ ....... ngày ........./........./.............";//ra viện
                //drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                //drData["SDT_bv"] = globalVariables.Branch_Phone;
                //drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                //drData["Fax_bv"] = globalVariables.Branch_Fax;
                //drData["website_bv"] = globalVariables.Branch_Website;
                //drData["email_bv"] = globalVariables.Branch_Email;
                //drData["ten_phieu"] = "Phiếu khám thai";
                //drData["sngay_kham_full"] = Utility.FormatDateTime_giophut_ngay_thang_nam(_pkt.NgayKham, "");
                //drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                //drData["sNgaykykinh_cuoi"] = _pkt.NgayDaukykinhcuoi.HasValue ? _pkt.NgayDaukykinhcuoi.Value.ToString("dd/MM/yyyy") : "";
                //drData["sngaydukien_sinh"] = _pkt.NgayDukiensinh.HasValue ? _pkt.NgayDukiensinh.Value.ToString("dd/MM/yyyy") : "";
                //drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                //drData["ngay_in"] = Utility.FormatDateTime(DateTime.Now);
                //drData["sngay_nhapvien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien, "");

                List<string> fieldNames = new List<string>();
                string tenToBA = "";
                if (toBA == 1) tenToBA = "BA01_BANOIKHOA_TO1.doc";
                else if (toBA == 0) tenToBA = "BA01_BANOIKHOA_BIA.doc";
                else if (toBA == 2) tenToBA = "BA01_BANOIKHOA_TO2.doc";
                else if (toBA == 3) tenToBA = "BA01_BANOIKHOA_TO3.doc";
                else if (toBA == 4) tenToBA = "BA01_BANOIKHOA_TO4.doc";
                else tenToBA = "BA01_BANOIKHOA.doc";
                string PathDoc = string.Format(AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\{0}", tenToBA);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "EmrBa", objLuotkham.MaLuotkham, Utility.sDbnull(objEmrBa.IdBa), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                    //Nạp tiền sử sản khoa. Nhảy đến bảng số 3 trong file doc mẫu
                    try
                    {
                        if (toBA == 2 || toBA == 3 || toBA == 4)
                        {
                        }
                        else
                        {
                            int table_idx = 0;
                            string tbl_idx = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA01_BANOIKHOA_TBLIDX.txt";
                            table_idx = Utility.Int32Dbnull(Utility.GetFirstValueFromFile(tbl_idx), 1);
                            Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[table_idx];

                            tab = tab.Rows[14].Cells[1].FirstChild as Aspose.Words.Tables.Table;//(Aspose.Words.Tables.Table)doc.GetChild(NodeType.Table, 0, true);//
                            int idx = 1;
                            foreach (DataRow dr in dtkhoachuyen.Rows)
                            {
                                Aspose.Words.Tables.Row newRow = idx == 1 ? (Aspose.Words.Tables.Row)tab.LastRow : (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);//.Clone(true);
                                newRow.RowFormat.Borders.Shadow = false;
                                newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;


                                newRow.Cells[0].FirstParagraph.Runs.Clear();
                                newRow.Cells[1].FirstParagraph.Runs.Clear();
                                newRow.Cells[2].FirstParagraph.Runs.Clear();

                                Run r = new Run(doc);
                                r.Font.Name = "Times New Roman";
                                r.Font.Size = 10d;
                                r.Font.Bold = false;
                                //r.Font.Color = Color.FromArgb(102, 0, 102);
                                r.Text = Utility.sDbnull(dr["ten_khoanoitru"], "");
                                newRow.Cells[0].FirstParagraph.AppendChild(r);
                                newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                r = new Run(doc);
                                r.Font.Name = "Times New Roman";
                                r.Font.Bold = false;
                                r.Font.Size = 10d;
                                //r.Font.Color = Color.FromArgb(102, 0, 102);
                                r.Text = Utility.sDbnull(dr["ngay_vaokhoa"], "");
                                newRow.Cells[1].FirstParagraph.AppendChild(r);
                                newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                r = new Run(doc);
                                r.Font.Name = "Times New Roman";
                                r.Font.Bold = false;
                                r.Font.Size = 10d;
                                //r.Font.Color = Color.FromArgb(102, 0, 102);
                                r.Text = Utility.sDbnull(dr["so_luong"], "");
                                newRow.Cells[2].FirstParagraph.AppendChild(r);
                                newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                                if (idx > 1)
                                    tab.AppendChild(newRow);
                                idx += 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }


                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
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
                _PhieuTTBA.ucThongtinnguoibenh_v21.txtMaluotkham.Focus();
                _PhieuTTBA.ucThongtinnguoibenh_v21.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                _PhieuTTBA.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuInVoBA_Click(object sender, EventArgs e)
        {
            InBA(0);
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
            _Emr.ShowDialog();
        }
    }
}
