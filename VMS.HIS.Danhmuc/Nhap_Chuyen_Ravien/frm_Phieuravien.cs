using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UCs;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.Libs;
using System.Text;
using VNS.HIS.UI.NGOAITRU;
using Janus.Windows.GridEX;
using VNS.HIS.UI.Forms.Noitru;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_Phieuravien : Form
    {
        private bool AllowTextChanged;
        public bool AutoLoad = false;
        private action m_enAct = action.Insert;
        public bool mv_blnCancel = true;
        public KcbLuotkham objLuotkham = null;
        public KcbDanhsachBenhnhan objBenhnhan = null;
        private NoitruPhieuravien objRavien;
        DataTable dt_ICD_PHU = new DataTable();
        public frm_Phieuravien()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
        }

        private void InitEvents()
        {
            KeyDown += frm_Phieuravien_KeyDown;
            Load += frm_Phieuravien_Load;
            FormClosing += frm_Phieuravien_FormClosing;
            txtKieuchuyenvien._OnSaveAs += txtKieuchuyenvien__OnSaveAs;

            txtKqdieutri._OnSaveAs += txtKqdieutri__OnSaveAs;

            txtTinhtrangravien._OnSaveAs += txtTinhtrangravien__OnSaveAs;

            txtPhuongphapdieutri._OnSaveAs += txtPhuongphapdieutri__OnSaveAs;

            cmdExit.Click += cmdExit_Click;
            cmdChuyen.Click += cmdChuyen_Click;
            cmdHuy.Click += cmdHuy_Click;

            cmdGetBV.Click += cmdGetBV_Click;
            cmdPrint.Click += cmdPrint_Click;

            chkChuyenvien.CheckedChanged += chkChuyenvien_CheckedChanged;
            dtpNgayravien.ValueChanged += dtpNgayravien_ValueChanged;
            txtBenhphu._OnEnterMe += txtBenhphu__OnEnterMe;
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
            txtSongayhentaikham.LostFocus += txtSongayhentaikham_LostFocus;
            txtSoNgayHen.LostFocus+=txtSoNgayHen_LostFocus;
            grdPresDetail.SelectionChanged += grdPresDetail_SelectionChanged;
            txtChandoan._OnShowDataV1+=_OnShowDataV1;
             txtKqdieutri._OnShowDataV1+=_OnShowDataV1;
             txtTinhtrangravien._OnShowDataV1+=_OnShowDataV1;
            // txtBenhgiaiphau._OnShowDataV1+=_OnShowDataV1;
             txtKieuchuyenvien._OnShowDataV1+=_OnShowDataV1;
             txtPhuongphapdieutri._OnShowDataV1+=_OnShowDataV1;
             txtphuongtienvc._OnShowDataV1+=_OnShowDataV1;
             autoLydotuvong._OnShowDataV1+=_OnShowDataV1;
             txtPhuongphapdieutri._OnEnterMe += txtPhuongphapdieutri__OnEnterMe;
             txtBenhchinh._OnEnterMe += txtBenhchinh__OnEnterMe;
             txtTinhtrangravien._OnEnterMe += txtTinhtrangravien__OnEnterMe;

        }

        void txtTinhtrangravien__OnEnterMe()
        {
            try
            {
                if (txtTinhtrangravien.myCode == THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MATINHTRANGRAVIEN_CHUYENVIEN","0", false).ToUpper())
                    chkChuyenvien.Checked = true;
                else
                    chkChuyenvien.Checked = false;
                //cmdChuyenvien.Visible = chkChuyenvien.Checked;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        void txtBenhchinh__OnEnterMe()
        {
            txtTenBenhChinh.Text = txtBenhchinh.MyText.Replace(txtBenhchinh.MyCode, "");
        }

        void txtPhuongphapdieutri__OnEnterMe()
        {
            //if (txtPhuongphapdieutri.myCode == THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_CHUYENVIEN", false).ToUpper())
            //    chkChuyenvien.Checked = true;
            //else
            //    chkChuyenvien.Checked = false;
        }

        void _OnShowDataV1(AutoCompleteTextbox_Danhmucchung obj)
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = obj.Text;
                obj.Init();
                obj._Text = oldCode;
                obj.Focus();
            }
        }

        void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
             RowThuoc = Utility.findthelastChild(grdPresDetail.CurrentRow);
        }
        private void txtSoNgayHen_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoNgayHen.Text))
            {
                txtSongayhentaikham.Text = txtSoNgayHen.Text;
                dtpNgayHen.Value = dtpNgayravien.Value.AddDays(Utility.Int32Dbnull(txtSongayhentaikham.Text, 0));
            }
        }
        void txtSongayhentaikham_LostFocus(object sender, EventArgs e)
        {
            dtpNgayHen.Value = dtpNgayravien.Value.AddDays(Utility.Int32Dbnull(txtSongayhentaikham.Text, 0));
            txtSoNgayHen.Text = txtSongayhentaikham.Text; 
        }
        private void txtSongaydieutri_LostFocus(object sender, EventArgs e)
        {
            
        }
        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null && ucThongtinnguoibenh1.objBenhnhan != null)
            {
                objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                objBenhnhan = ucThongtinnguoibenh1.objBenhnhan;
                cmdChuyen.Enabled = true;
                if (objLuotkham.TrangthaiNoitru == 0)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân chưa vào nội trú nên không thể lập phiếu ra viện. Đề nghị bạn kiểm tra lại");
                    cmdChuyen.Enabled = false;
                    return;
                }
                cmdChuyen.Enabled = objLuotkham != null && globalVariables.isSuperAdmin ? true : objLuotkham.TrangthaiNoitru <= 3 && objLuotkham.TrangthaiNoitru >= 1;

                LoadData();
                ModifyRavien();
                ModifyCommmands();
                dtpNgayravien.Focus();
            }
            else
                ClearControls(new List<string>() { "xyz" });
        }
        void LoadUserConfigs()
        {
            try
            {

                chkInsaukhiluu.Checked = Utility.getUserConfigValue(chkInsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkInsaukhiluu.Checked)) == 1;
                chkThoatsaukhiluu.Checked = Utility.getUserConfigValue(chkThoatsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkThoatsaukhiluu.Checked)) == 1;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                Application.DoEvents();
            }
        }
        void SaveUserConfigs()
        {
            try
            {

                Utility.SaveUserConfig(chkInsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkInsaukhiluu.Checked));
                Utility.SaveUserConfig(chkThoatsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkThoatsaukhiluu.Checked));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void frm_Phieuravien_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void txtBenhphu__OnEnterMe()
        {
            if (txtBenhphu.MyCode != "-1")
            {
                AddBenhPhu();
                txtBenhphu.Focus();
                txtBenhphu.SelectAll();
            }
        }
        private void AddBenhPhu()
        {
            try
            {
                //int record = dt_ICD.Select(string.Format(DmucBenh.Columns.MaBenh+ " ='{0}'", txtMaBenhphu.Text)).GetLength(0);
                EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                             txtBenhphu.MyCode
                                                         select benh;


                if (!query.Any())
                {
                    AddMaBenh(txtBenhphu.MyCode, txtBenhphu.Text);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
            }
        }

        private void AddMaBenh(string maBenh, string tenBenh)
        {
            EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                     where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == maBenh
                                                     select benh;
            if (!query.Any())
            {
                DataRow drv = dt_ICD_PHU.NewRow();
                drv[DmucBenh.Columns.MaBenh] = maBenh;
                EnumerableRowCollection<string> query1 = from benh in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                             maBenh
                                                         select Utility.sDbnull(benh[DmucBenh.Columns.TenBenh]);
                if (query1.Any())
                {
                    drv[DmucBenh.Columns.TenBenh] = Utility.sDbnull(query1.FirstOrDefault());
                }

                dt_ICD_PHU.Rows.Add(drv);
                dt_ICD_PHU.AcceptChanges();
                grd_ICD.AutoSizeColumns();
            }
        }
       
        void FillMabenhphu(string dataString)
        {
            dt_ICD_PHU.Clear();
            if (!string.IsNullOrEmpty(dataString))
            {
                string[] rows = dataString.Split(',');
                foreach (string row in rows)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        DataRow newDr = dt_ICD_PHU.NewRow();
                        newDr[DmucBenh.Columns.MaBenh] = row;
                        newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                        dt_ICD_PHU.Rows.Add(newDr);
                        dt_ICD_PHU.AcceptChanges();
                    }
                }
                grd_ICD.DataSource = dt_ICD_PHU;
            }
        }
        private string GetTenBenh(string maBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh =
                globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", maBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return TenBenh;
        }
       
        private void dtpNgayravien_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowTextChanged || dtpNgayravien.Value ==null) return;
            txtTongSoNgayDtri.Text =
                THU_VIEN_CHUNG.Songay(objLuotkham.NgayNhapvien.Value,
                    new DateTime(dtpNgayravien.Value.Year, dtpNgayravien.Value.Month, dtpNgayravien.Value.Day,
                        Utility.Int32Dbnull(txtGioRaVien.Text, 0), Utility.Int32Dbnull(txtPhutRaVien.Text, 0), 0))
                    .ToString();
            
            txtGioRaVien.Text = dtpNgayravien.Value.Hour.ToString();
            txtPhutRaVien.Text = dtpNgayravien.Value.Minute.ToString();
        }

        private void chkChuyenvien_CheckedChanged(object sender, EventArgs e)
        {
            txtNoichuyenden.Enabled =txtKieuchuyenvien.Enabled=
                cmdGetBV.Enabled =
                    txtNguoivanchuyen.Enabled =
                        txtBsChidinh.Enabled = txtphuongtienvc.Enabled = chkChuyenvien.Checked;
            if (chkChuyenvien.Checked) txtNoichuyenden.Focus();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                
                Utility.WaitNow(this);
                DataTable dtData =
                    SPs.NoitruInphieuravien(Utility.DoTrim(ucThongtinnguoibenh1.txtMaluotkham.Text)).GetDataSet().Tables[0];
                VMS.HIS.Bus.WordPrinter.InPhieu(null,dtData, "PHIEU_RAVIEN.doc");
                //if (dtData.Rows.Count <= 0)
                //{
                //    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                //    return;
                //}
                //THU_VIEN_CHUNG.CreateXML(dtData, "noitru_phieuravien.XML");
                //Utility.UpdateLogotoDatatable(ref dtData);
                //string StaffName = globalVariables.gv_strTenNhanvien;
                //if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                //string tieude = "", reportname = "";
                //ReportDocument crpt = Utility.GetReport("noitru_phieuravien", ref tieude, ref reportname);
                //if (crpt == null) return;
                //var objForm = new frmPrintPreview(baocaO_TIEUDE1.TIEUDE, crpt, true,
                //    dtData.Rows.Count <= 0 ? false : true);
                //crpt.SetDataSource(dtData);

                //objForm.mv_sReportFileName = Path.GetFileName(reportname);
                //objForm.nguoi_thuchien =Utility.sDbnull( dtData.Rows[0]["ten_bacsi_chuyenvien"],"");
                //objForm.mv_sReportCode = "noitru_phieuravien";
                //Utility.SetParameterValue(crpt, "StaffName", StaffName);
                //Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                //Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                //Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                //Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                //Utility.SetParameterValue(crpt, "sTitleReport", baocaO_TIEUDE1.TIEUDE);
                //Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(Convert.ToDateTime(dtData.Rows[0]["ngay_ravien"].ToString())));
                //Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                //Utility.SetParameterValue(crpt, "txtTrinhky",
                //          Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                //objForm.crptViewer.ReportSource = crpt;
                //objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }

        private void cmdGetBV_Click(object sender, EventArgs e)
        {
            var _danhsachbenhvien = new frm_danhsachbenhvien();
            if (_danhsachbenhvien.ShowDialog() == DialogResult.OK)
            {
                txtNoichuyenden.SetId(_danhsachbenhvien.idBenhvien);
            }
        }

        //private void cmdgetPatient_Click(object sender, EventArgs e)
        //{
        //    var _DSACH_BN_TKIEM = new frm_DSACH_BN_TKIEM("ALL");
        //    if (_DSACH_BN_TKIEM.ShowDialog() == DialogResult.OK)
        //    {
        //        txtMaluotkham.Text = _DSACH_BN_TKIEM.MaLuotkham;
        //        txtMaluotkham_KeyDown(txtMaluotkham, new KeyEventArgs(Keys.Enter));
        //    }
        //}
        private void cmdHuy_Click_bak(object sender, EventArgs e)
        {
            if (cmdHuy.Tag.ToString() == "1")//Hủy ra viện
            {
                if (objLuotkham == null)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn bệnh nhân trước khi thực hiện hủy ra viện", true);
                    return;
                }
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (objLuotkham.TrangthaiNoitru == 4)
                {
                    Utility.SetMsg(lblMsg,
                        "Bệnh nhân đã được xác nhận dữ liệu nội trú để ra viện nên bạn không thể hủy ra viện", true);
                    return;
                }
                if (objLuotkham.TrangthaiNoitru == 5)
                {
                    Utility.SetMsg(lblMsg,
                        "Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể hủy ra viện", true);
                    return;
                }
                if (objLuotkham.TrangthaiNoitru == 6)
                {
                    Utility.SetMsg(lblMsg,
                        "Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể hủy ra viện", true);
                    return;
                }
                KcbPhieuchuyenvien objphieucv = new Select().From(KcbPhieuchuyenvien.Schema)
                           .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(ucThongtinnguoibenh1.txtIdBn.Text)
                           .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(ucThongtinnguoibenh1.txtMaluotkham.Text)
                    //.And(KcbPhieuchuyenvien.Columns.NoiTru).IsEqualTo(1)
                           .ExecuteSingle<KcbPhieuchuyenvien>();
                if (objphieucv != null)
                {
                    Utility.ShowMsg("Người bệnh đã được lập phiếu chuyển viện. Do vậy bạn cần hủy phiếu chuyển viện trước khi hủy ra viện.\nVui lòng kiểm tra lại");
                    return;
                }
                if (
                    Utility.AcceptQuestion(
                        string.Format("Bạn có chắc chắn muốn hủy ra viện cho bệnh nhân {0} hay không?", ucThongtinnguoibenh1.txtTenBN.Text),
                        "Xác nhận hủy ra viện", true))
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
                                objLuotkham.TrangthaiNoitru = 2;
                                objLuotkham.NgayRavien = null;
                                //objLuotkham.IdRavien = -1; Giữ lại để chửi gây lỗi tùm lum
                                objLuotkham.SoRavien = "";
                                objLuotkham.IsNew = false;
                                objLuotkham.MarkOld();
                                objLuotkham.Save();
                                new Delete().From(NoitruPhieuravien.Schema)
                                    .Where(NoitruPhieuravien.Columns.IdRavien).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1))
                                    .Execute();
                                new Delete().From(KcbPhieuchuyenvien.Schema)
                                   .Where(KcbPhieuchuyenvien.Columns.IdRavien).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1))
                                   .And(KcbPhieuchuyenvien.Columns.NoiTru).IsEqualTo(1)
                                   .Execute();
                                NoitruPhanbuonggiuong objNoitruPhanbuonggiuong =
                                    NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien.Value);
                                if (objNoitruPhanbuonggiuong != null)
                                {
                                    objNoitruPhanbuonggiuong.MarkOld();
                                    objNoitruPhanbuonggiuong.IsNew = false;
                                    objNoitruPhanbuonggiuong.SoLuong = 0;
                                    objNoitruPhanbuonggiuong.SoluongGio = 0;
                                    objNoitruPhanbuonggiuong.NgayKetthuc = null;
                                    objNoitruPhanbuonggiuong.Save();
                                }
                            }
                            scope.Complete();

                        }
                        mv_blnCancel = false;
                        Utility.ShowMsg(string.Format("Hủy ra viện cho bệnh nhân {0} thành công", ucThongtinnguoibenh1.txtTenBN.Text));
                        cmdHuy.Enabled = false;
                        cmdPrint.Enabled = false;
                        cmdChuyen.Enabled = objLuotkham != null && globalVariables.isSuperAdmin ? true : objLuotkham.TrangthaiNoitru <= 3;
                        LoadData();
                        dt_ICD_PHU.Rows.Clear();
                        dtpNgayravien.Focus();
                    }
                    catch (Exception ex)
                    {
                        Utility.CatchException(ex);
                    }
                }
            }
            else//Đánh dấu ra viện
            {
                objLuotkham.NgayRavien = objRavien.NgayRavien;
                objLuotkham.SoRavien = Utility.sDbnull(objRavien.IdRavien);
                objLuotkham.TrangthaiNoitru = 3;
                objLuotkham.IsNew = false;
                objLuotkham.MarkOld();
                objLuotkham.Save();
            }
            ModifyRavien();
        }
        private void cmdHuy_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdHuy.Tag.ToString() == "HUY")//Hủy ra viện
                {
                    if (!Utility.Coquyen("noitru_ravien_quyenhuy"))
                    {
                        Utility.ShowMsg("Bạn không có quyền hủy phiếu ra viện(noitru_ravien_quyenhuy). Đề nghị liên hệ quản trị hệ thống để được cấp quyền");
                        return;
                    }
                    if (objLuotkham == null)
                    {
                        Utility.SetMsg(lblMsg, "Bạn cần chọn bệnh nhân trước khi thực hiện hủy ra viện", true);
                        return;
                    }
                    objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                    if (objLuotkham.TrangthaiNoitru == 4)
                    {
                        Utility.SetMsg(lblMsg,
                            "Bệnh nhân đã được xác nhận dữ liệu nội trú để ra viện nên bạn không thể hủy ra viện. Liên hệ IT hoặc Tài chính để Chuyển về khoa", true);
                        return;
                    }
                    if (objLuotkham.TrangthaiNoitru == 5)
                    {
                        Utility.SetMsg(lblMsg,
                            "Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể hủy ra viện", true);
                        return;
                    }
                    if (objLuotkham.TrangthaiNoitru == 6)
                    {
                        Utility.SetMsg(lblMsg,
                            "Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể hủy ra viện", true);
                        return;
                    }
                    KcbPhieuchuyenvien objphieucv = new Select().From(KcbPhieuchuyenvien.Schema)
                               .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(ucThongtinnguoibenh1.txtIdBn.Text)
                               .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(ucThongtinnguoibenh1.txtMaluotkham.Text)
                        //.And(KcbPhieuchuyenvien.Columns.NoiTru).IsEqualTo(1)
                               .ExecuteSingle<KcbPhieuchuyenvien>();
                    if (objphieucv != null)
                    {
                        Utility.ShowMsg("Người bệnh đã được lập phiếu chuyển viện. Do vậy bạn cần hủy phiếu chuyển viện trước khi hủy ra viện.\nVui lòng kiểm tra lại");
                        return;
                    }
                    if (
                        Utility.AcceptQuestion(
                            string.Format("Bạn có chắc chắn muốn hủy ra viện cho bệnh nhân {0} hay không?", ucThongtinnguoibenh1.txtTenBN.Text),
                            "Xác nhận hủy ra viện", true))
                    {
                        try
                        {
                            using (var scope = new TransactionScope())
                            {
                                using (var dbscope = new SharedDbConnectionScope())
                                {

                                    objLuotkham.TrangthaiNoitru = 2;
                                    //objLuotkham.NgayRavien = null;
                                    //objLuotkham.SoRavien = "";
                                    objLuotkham.IsNew = false;
                                    objLuotkham.MarkOld();
                                    objLuotkham.Save();
                                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy xác nhận ra viện cho người bệnh có Id={0}, PID={1}, Tên={2} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                                }
                                scope.Complete();

                            }
                            mv_blnCancel = false;
                            Utility.ShowMsg(string.Format("Hủy ra viện cho bệnh nhân {0} thành công", ucThongtinnguoibenh1.txtTenBN.Text));
                            cmdHuy.Enabled = false;
                            cmdPrint.Enabled = false;
                            cmdChuyen.Enabled = objLuotkham != null && globalVariables.isSuperAdmin ? true : objLuotkham.TrangthaiNoitru <= 3;
                            LoadData();
                            dtpNgayravien.Focus();
                        }
                        catch (Exception ex)
                        {
                            Utility.CatchException(ex);
                        }
                        finally
                        {
                        }
                    }
                }
                else//Đánh dấu ra viện
                {
                    objLuotkham.NgayRavien = objRavien.NgayRavien;
                    objLuotkham.SoRavien = Utility.sDbnull(objRavien.IdRavien);
                    objLuotkham.TrangthaiNoitru = 3;
                    objLuotkham.IsNew = false;
                    objLuotkham.MarkOld();
                    objLuotkham.Save();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xác nhận ra viện cho người bệnh có Id={0}, PID={1}, Tên={2} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                }
                ModifyRavien();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        void ModifyRavien()
        {
            //uiTabPagedonthuoc.TabVisible = objLuotkham != null && objRavien != null;
            cmdHuyphieuravien.Enabled = objLuotkham != null && objRavien != null && objLuotkham.TrangthaiNoitru < 3;
            cmdChuyen.Enabled = objLuotkham != null && globalVariables.isSuperAdmin ? true : objLuotkham.TrangthaiNoitru < 3;
            cmdPrint.Enabled = objRavien != null;
            cmdHuy.Enabled = objRavien != null;
            cmdHuy.Tag = objLuotkham.TrangthaiNoitru == 3 ? "HUY" : "RA";
            cmdHuy.Text = objLuotkham.TrangthaiNoitru == 3 ? "Hủy ra viện" : "Ra viện";
            cmdHuy.Image = objLuotkham.TrangthaiNoitru == 3 ? global::VMS.HIS.Danhmuc.Properties.Resources.Cancel : global::VMS.HIS.Danhmuc.Properties.Resources.HOME_24; 
        }
        private string GetDanhsachBenhphu()
        {
            var sMaICDPHU = new StringBuilder("");
            try
            {
                int recordRow = 0;

                if (dt_ICD_PHU.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_ICD_PHU.Rows)
                    {
                        if (recordRow > 0)
                            sMaICDPHU.Append(",");
                        sMaICDPHU.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                        recordRow++;
                    }
                }
                return sMaICDPHU.ToString();
            }
            catch
            {
                return "";
            }
        }
        private void cmdChuyen_Click_bak(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", false);
            if (dtpNgayravien.Value < dtpNgaynhapvien.Value)
            {
                Utility.ShowMsg("Ngày ra viện phải lớn hơn hoặc bằng ngày nhập viện. Vui lòng kiểm tra lại");
                dtpNgayravien.Focus();
                return;
            }
            //if (Utility.DoTrim(txtGioRaVien.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin giờ ra viện", true);
            //    txtGioRaVien.Focus();
            //    return;
            //}
            //if (Utility.Int32Dbnull(txtGioRaVien.Text, 0) >= 24)
            //{
            //    Utility.SetMsg(lblMsg, "Giờ ra viện nằm trong khoảng giá trị từ 0 đến 23", true);
            //    txtGioRaVien.Focus();
            //    return;
            //}
            //if (Utility.DoTrim(txtPhuRaVien.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin phút ra viện", true);
            //    txtPhuRaVien.Focus();
            //    return;
            //}
            //if (Utility.Int32Dbnull(txtPhuRaVien.Text, 0) >= 60)
            //{
            //    Utility.SetMsg(lblMsg, "Phút ra viện nằm trong khoảng giá trị từ 0 đến 59", true);
            //    txtPhuRaVien.Focus();
            //    return;
            //}
            if (Utility.DoTrim(txtSoRaVien.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin số phiếu ra viện", true);
                txtSoRaVien.Focus();
                return;
            }
            DataTable dtData = new Select().From(NoitruPhieuravien.Schema).Where(NoitruPhieuravien.Columns.SophieuRavien).IsEqualTo(Utility.DoTrim(txtSoRaVien.Text)).And(NoitruPhieuravien.Columns.IdRavien).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1)).ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, "Số ra viện đã được sử dụng. Vui lòng nhập số phiếu khác", true);
                txtSoRaVien.Focus();
                return;
            }
            if (txtTruongkhoa.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin trưởng khoa điều trị", true);
                txtTruongkhoa.Focus();
                return;
            }
            //if (chkChuyenvien.Checked)
            //{
            //    if (txtNoichuyenden.MyCode == "-1")
            //    {
            //        Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin nơi chuyển đến", true);
            //        txtNoichuyenden.Focus();
            //        return;
            //    }
            //    //if (txtBsChidinh.MyCode == "-1")
            //    //{
            //    //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin bác sĩ chỉ định chuyển viện", true);
            //    //    txtBsChidinh.Focus();
            //    //    return;
            //    //}
            //}
            if (txtKqdieutri.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin kết quả điều trị theo danh mục", true);
                txtKqdieutri.Focus();
                return;
            }
            if (txtTinhtrangravien.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin tình trạng ra viện theo danh mục", true);
                txtTinhtrangravien.Focus();
                return;
            }
            //if (Utility.DoTrim(txtLoidanBS.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin lời dặn bác sĩ", true);
            //    txtLoidanBS.Focus();
            //    return;
            //}


            try
            {
                if (m_enAct == action.Insert)
                {
                    objRavien = new NoitruPhieuravien();
                    objRavien.IsNew = true;
                }
                else
                {
                    objRavien = NoitruPhieuravien.FetchByID(Utility.Int32Dbnull(txtId.Text));
                    objRavien.IsNew = false;
                    objRavien.MarkOld();
                }
                string sMaICDPHU = GetDanhsachBenhphu();
                objRavien.NgayRavien = new DateTime(dtpNgayravien.Value.Year, dtpNgayravien.Value.Month, dtpNgayravien.Value.Day, Utility.Int32Dbnull(txtGioRaVien.Text, 0), Utility.Int32Dbnull(txtPhutRaVien.Text, 0), 0);
                objRavien.SophieuRavien = Utility.DoTrim(txtSoRaVien.Text);
                objRavien.TongsongayDieutri = Utility.Int32Dbnull(txtTongSoNgayDtri.Text);
                objRavien.MabenhChinh = txtBenhchinh.MyCode;
                objRavien.MotaBenhchinh = txtBenhchinh.Text;
                objRavien.IdBenhnhan = objLuotkham.IdBenhnhan;
                objRavien.MaLuotkham = objLuotkham.MaLuotkham;
                //objRavien.ChanDoan = Utility.DoTrim(txtChandoan.myCode);
                objRavien.ChanDoan = Utility.DoTrim(txtChandoan.Text);
                objRavien.SoBenhAn = Utility.Int32Dbnull(objLuotkham.SoBenhAn, -1);
                objRavien.IdKhoaravien = Utility.Int32Dbnull(cboKhoaRavien.SelectedValue);
                objRavien.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                objRavien.TrangThai = 0;
                objRavien.MabenhGiaiphau = txtBenhgiaiphau.MyCode;
                objRavien.MabenhBienchung = txtBenhbienchung.MyCode;
                objRavien.MabenhPhu = sMaICDPHU;
                objRavien.MabenhNguyennhan = txtBenhnguyennhan.MyCode;
                objRavien.MaKquaDieutri = txtKqdieutri.MyCode;
                objRavien.MatheBhyt = txtMatheBHYT.Text;
                if (Utility.sDbnull(objRavien.MatheBhyt).Length > 0)
                {
                    objRavien.BhytTungay = dtInsFromDate.Value;
                    objRavien.BhytDenngay = dtInsToDate.Value;
                }
                else
                {
                    objRavien.BhytTungay = null;
                    objRavien.BhytDenngay = null;
                }
                if (chkHentaikham.Checked)
                {
                    objRavien.SongayhenTaikham = (byte)Utility.DecimaltoDbnull(txtSongayhentaikham.Text, 0);
                    objRavien.NgayhenTaikham = dtpNgayHen.Value;
                }
                else
                {
                    objRavien.NgayhenTaikham = null;
                    objRavien.SongayhenTaikham = 0;
                }

                objRavien.MaKieuchuyenvien = txtKieuchuyenvien.MyCode;
                objRavien.MaTinhtrangravien = txtTinhtrangravien.MyCode;
                objRavien.IdBacsiChuyenvien = Utility.Int16Dbnull(txtTruongkhoa.MyID, -1);
                objRavien.PhuongphapDieutri = Utility.DoTrim(txtPhuongphapdieutri.Text);
                objRavien.TrangthaiChuyenvien = Utility.Bool2byte(chkChuyenvien.Checked);
                objRavien.IdBenhvienDi = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                objRavien.LoidanBacsi = Utility.DoTrim(txtLoidanBS.Text);
                objRavien.SotuanThai = Utility.Int16Dbnull(txtsotuanthai.Text, 0);
                if (chkTuvong.Checked)
                    objRavien.TuvongNgay = dtpNgaytuvong.Value;
                else
                    objRavien.TuvongNgay = null;
                objRavien.YkienDexuat = Utility.DoTrim(txtYkien.Text);
                objRavien.PhuhopChandoanlamsang = Utility.Bool2byte(chkPhuHopChanDoanCLS.Checked);
                objRavien.NgayCapgiayravien = dtNGAY_CAP_GIAY_RVIEN.Value;
                KcbPhieuchuyenvien _phieuchuyenvien = null;
                if (chkChuyenvien.Checked)
                {
                    string idcname = "";
                    string idcCode = "";
                    Utility.GetChanDoan(objRavien.MabenhChinh, objRavien.MabenhPhu, objRavien.ChanDoan, ref idcname, ref idcCode);
                    string fullChandoan = string.Format("{0}:{1}", idcCode, idcname);

                    _phieuchuyenvien = new Select().From(KcbPhieuchuyenvien.Schema)
                        .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(ucThongtinnguoibenh1.txtIdBn.Text)
                        .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(ucThongtinnguoibenh1.txtMaluotkham.Text)
                        //.And(KcbPhieuchuyenvien.Columns.NoiTru).IsEqualTo(1)
                        .ExecuteSingle<KcbPhieuchuyenvien>();

                    if (_phieuchuyenvien == null)
                    {
                        _phieuchuyenvien = new KcbPhieuchuyenvien();
                        _phieuchuyenvien.IsNew = true;
                        _phieuchuyenvien.SoChuyentuyen = Laysochuyenvien();
                        _phieuchuyenvien.ChanDoan = fullChandoan;
                        _phieuchuyenvien.IdBenhnhan = objLuotkham.IdBenhnhan;
                        _phieuchuyenvien.MaLuotkham = objLuotkham.MaLuotkham;
                        _phieuchuyenvien.IdBenhvienChuyenden = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                        _phieuchuyenvien.DauhieuCls = "";// Utility.DoTrim(txtTinhtrangravien.Text);
                        _phieuchuyenvien.KetquaXnCls = "";
                        _phieuchuyenvien.NoiTru = 1;
                        _phieuchuyenvien.IdBacsiChuyenvien = objRavien.IdBacsiChuyenvien;
                        _phieuchuyenvien.ThuocSudung = "";
                        _phieuchuyenvien.LydoChuyen = 1;
                        _phieuchuyenvien.TrangthaiBenhnhan = Utility.DoTrim(txtKqdieutri.Text);
                        _phieuchuyenvien.HuongDieutri = "";// Utility.DoTrim(txtPhuongphapdieutri.Text);
                        _phieuchuyenvien.PhuongtienChuyen = Utility.DoTrim(txtphuongtienvc.Text);
                        _phieuchuyenvien.TenNguoichuyen = Utility.DoTrim(txtNguoivanchuyen.Text);
                        _phieuchuyenvien.NgayChuyenvien = objRavien.NgayRavien;
                        _phieuchuyenvien.IdKhoanoitru = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtIdkhoanoitru.Text, -1);
                        _phieuchuyenvien.IdBuong = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtidBuong.Text, -1);
                        _phieuchuyenvien.IdGiuong = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtidgiuong.Text, -1);
                    }
                    else
                    {
                        _phieuchuyenvien.IsNew = false;
                        if (_phieuchuyenvien.ChanDoan == "")
                            _phieuchuyenvien.ChanDoan = fullChandoan;
                        _phieuchuyenvien.NgayChuyenvien = objRavien.NgayRavien;
                        _phieuchuyenvien.IdKhoanoitru = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtIdkhoanoitru.Text, -1);
                        _phieuchuyenvien.IdBuong = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtidBuong.Text, -1);
                        _phieuchuyenvien.IdGiuong = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtidgiuong.Text, -1);
                        _phieuchuyenvien.MarkOld();
                    }
                    //_phieuchuyenvien.IdBenhnhan = objLuotkham.IdBenhnhan;
                    //_phieuchuyenvien.MaLuotkham = objLuotkham.MaLuotkham;
                    //_phieuchuyenvien.IdBenhvienChuyenden = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                    //_phieuchuyenvien.DauhieuCls = "";// Utility.DoTrim(txtTinhtrangravien.Text);
                    //_phieuchuyenvien.KetquaXnCls = "";
                    //_phieuchuyenvien.ChanDoan = "";
                    //_phieuchuyenvien.NoiTru = 1;

                    //_phieuchuyenvien.IdBacsiChuyenvien = objRavien.IdBacsiChuyenvien;
                    //_phieuchuyenvien.ThuocSudung = "";
                    //_phieuchuyenvien.LydoChuyen = 1;
                    //_phieuchuyenvien.TrangthaiBenhnhan = Utility.DoTrim(txtKqdieutri.Text);
                    //_phieuchuyenvien.HuongDieutri = "";// Utility.DoTrim(txtPhuongphapdieutri.Text);
                    //_phieuchuyenvien.PhuongtienChuyen = Utility.DoTrim(txtphuongtienvc.Text);
                    //_phieuchuyenvien.TenNguoichuyen = Utility.DoTrim(txtNguoivanchuyen.Text);

                }
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objRavien.Save();
                        if (_phieuchuyenvien != null)
                        {
                            _phieuchuyenvien.IdRavien = (int)objRavien.IdRavien;
                            _phieuchuyenvien.Save();
                            objLuotkham.TthaiChuyendi = 1;

                            objLuotkham.IdBacsiChuyenvien = _phieuchuyenvien.IdBacsiChuyenvien;
                            objLuotkham.NgayRavien = objRavien.NgayRavien;
                            objLuotkham.IdBenhvienDi = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                        }
                        objLuotkham.NgayRavien = objRavien.NgayRavien;
                        objLuotkham.SoRavien = Utility.sDbnull(objRavien.IdRavien);
                        objLuotkham.TrangthaiNoitru = 3;
                        objLuotkham.IsNew = false;
                        objLuotkham.MarkOld();
                        objLuotkham.Save();

                        NoitruPhanbuonggiuong objNoitruPhanbuonggiuong =
                            NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien.Value);
                        if (objNoitruPhanbuonggiuong != null)
                        {
                            objNoitruPhanbuonggiuong.MarkOld();
                            objNoitruPhanbuonggiuong.IsNew = false;
                            objNoitruPhanbuonggiuong.NgayKetthuc = objRavien.NgayRavien;
                            objNoitruPhanbuonggiuong.CachtinhSoluong = 0;
                            objNoitruPhanbuonggiuong.SoluongGio =
                                (int)
                                    Math.Ceiling(
                                        (objNoitruPhanbuonggiuong.NgayKetthuc.Value -
                                         objNoitruPhanbuonggiuong.NgayVaokhoa).TotalHours);
                            objNoitruPhanbuonggiuong.SoLuong =
                                THU_VIEN_CHUNG.Songay(objNoitruPhanbuonggiuong.NgayKetthuc.Value,
                                    objNoitruPhanbuonggiuong.NgayVaokhoa);
                            objNoitruPhanbuonggiuong.Save();
                        }
                    }
                    scope.Complete();
                }
                mv_blnCancel = false;
                Utility.ShowMsg(m_enAct == action.Insert ? "Thêm mới phiếu ra viện thành công" : "Cập nhật phiếu ra viện thành công");
                //if (m_enAct == action.Insert)
                cmdPrint.Enabled = true;
                ModifyRavien();
                //cmdHuy.Enabled = objRavien != null && objLuotkham != null && objLuotkham.TrangthaiNoitru == 3;
                m_enAct = action.Update;
                txtId.Text = objRavien.IdRavien.ToString();
                if (chkInsaukhiluu.Checked) cmdPrint.PerformClick();
                if (chkChuyenvien.Checked)
                {
                    cmdChuyenvien.Visible = true;
                    cmdChuyenvien.Focus();
                    //cmdChuyenvien.PerformClick();
                }
                else
                    if (chkThoatsaukhiluu.Checked) this.Close();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommmands();
            }
        }
        private void cmdChuyen_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", false);
            DateTime ngay_ravien = new DateTime(dtpNgayravien.Value.Year, dtpNgayravien.Value.Month, dtpNgayravien.Value.Day, Utility.Int32Dbnull(txtGioRaVien.Text, 0), Utility.Int32Dbnull(txtPhutRaVien.Text, 0), 0);
            if (ngay_ravien < dtpNgaynhapvien.Value)
            {
                Utility.ShowMsg("Ngày ra viện phải lớn hơn hoặc bằng ngày nhập viện. Vui lòng kiểm tra lại");
                dtpNgayravien.Focus();
                return;
            }

            if (Utility.DoTrim(txtSoRaVien.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin số phiếu ra viện", true);
                txtSoRaVien.Focus();
                return;
            }
            DataTable dtData = new Select().From(NoitruPhieuravien.Schema).Where(NoitruPhieuravien.Columns.SophieuRavien).IsEqualTo(Utility.DoTrim(txtSoRaVien.Text)).And(NoitruPhieuravien.Columns.IdRavien).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1)).ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, "Số ra viện đã được sử dụng. Vui lòng nhập số phiếu khác", true);
                txtSoRaVien.Focus();
                return;
            }
            if (txtTruongkhoa.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin trưởng khoa điều trị", true);
                txtTruongkhoa.Focus();
                return;
            }

            if (txtKqdieutri.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin kết quả điều trị theo danh mục", true);
                txtKqdieutri.Focus();
                return;
            }
            if (txtTinhtrangravien.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin tình trạng ra viện theo danh mục", true);
                txtTinhtrangravien.Focus();
                return;
            }
            //if (Utility.DoTrim(txtLoidanBS.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin lời dặn bác sĩ", true);
            //    txtLoidanBS.Focus();
            //    return;
            //}


            try
            {
                if (m_enAct == action.Insert)
                {
                    objRavien = new NoitruPhieuravien();
                    objRavien.IsNew = true;
                    objRavien.NgayTao = DateTime.Now;
                    objRavien.NguoiTao = globalVariables.UserName;
                }
                else
                {
                    objRavien = NoitruPhieuravien.FetchByID(Utility.Int32Dbnull(txtId.Text));
                    objRavien.NgaySua = DateTime.Now;
                    objRavien.NguoiSua = globalVariables.UserName;
                    objRavien.IsNew = false;
                    objRavien.MarkOld();
                }
                string sMaICDPHU = GetDanhsachBenhphu();
                objRavien.NgayRavien = new DateTime(dtpNgayravien.Value.Year, dtpNgayravien.Value.Month, dtpNgayravien.Value.Day, Utility.Int32Dbnull(txtGioRaVien.Text, 0), Utility.Int32Dbnull(txtPhutRaVien.Text, 0), 0);
                objRavien.SophieuRavien = Utility.DoTrim(txtSoRaVien.Text);
                objRavien.TongsongayDieutri = Utility.Int32Dbnull(txtTongSoNgayDtri.Text);
                objRavien.MabenhChinh = txtBenhchinh.MyCode;
                objRavien.MotaBenhchinh = txtBenhchinh.Text;
                objRavien.IdBenhnhan = objLuotkham.IdBenhnhan;
                objRavien.MaLuotkham = objLuotkham.MaLuotkham;
                //objRavien.ChanDoan = Utility.DoTrim(txtChandoan.myCode);
                objRavien.ChanDoan = Utility.DoTrim(txtChandoan.Text);
                objRavien.SoBenhAn = Utility.Int32Dbnull(objLuotkham.SoBenhAn, -1);
                objRavien.IdKhoaravien = Utility.Int32Dbnull(cboKhoaRavien.SelectedValue);
                objRavien.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                objRavien.TrangThai = 0;
                objRavien.MabenhGiaiphau = txtBenhgiaiphau.MyCode;
                objRavien.MabenhBienchung = txtBenhbienchung.MyCode;
                objRavien.MabenhPhu = sMaICDPHU;
                objRavien.MabenhNguyennhan = txtBenhnguyennhan.MyCode;
                objRavien.MaKquaDieutri = txtKqdieutri.MyCode;
                objRavien.MatheBhyt = txtMatheBHYT.Text;
                if (Utility.sDbnull(objRavien.MatheBhyt).Length > 0)
                {
                    objRavien.BhytTungay = dtInsFromDate.Value;
                    objRavien.BhytDenngay = dtInsToDate.Value;
                }
                else
                {
                    objRavien.BhytTungay = null;
                    objRavien.BhytDenngay = null;
                }
                if (chkHentaikham.Checked)
                {
                    objRavien.SongayhenTaikham = (byte)Utility.DecimaltoDbnull(txtSongayhentaikham.Text, 0);
                    objRavien.NgayhenTaikham = dtpNgayHen.Value;
                }
                else
                {
                    objRavien.NgayhenTaikham = null;
                    objRavien.SongayhenTaikham = 0;
                }

                objRavien.MaKieuchuyenvien = txtKieuchuyenvien.MyCode;
                objRavien.MaTinhtrangravien = txtTinhtrangravien.MyCode;
                objRavien.IdBacsiChuyenvien = Utility.Int16Dbnull(txtTruongkhoa.MyID, -1);
                objRavien.PhuongphapDieutri = Utility.DoTrim(txtPhuongphapdieutri.Text);
                objRavien.TrangthaiChuyenvien = Utility.Bool2byte(chkChuyenvien.Checked);
                objRavien.IdBenhvienDi = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                objRavien.LoidanBacsi = Utility.DoTrim(txtLoidanBS.Text);
                objRavien.SotuanThai = Utility.Int16Dbnull(txtsotuanthai.Text, 0);
                objRavien.GpbLanhtinh = optLanhtinh.Checked;
                objRavien.GpbNghingo = optNghingo.Checked;
                objRavien.GpbActinh = opActinh.Checked;
                objRavien.Taibien = chkTaibien.Checked;
                objRavien.Bienchung = chkBienchung.Checked;
                if (chkTaibien.Checked)
                    objRavien.TaibienMota = Utility.sDbnull(txtTaibien.Text);
                else
                    objRavien.TaibienMota = "";
                if (chkBienchung.Checked)
                    objRavien.BienchungMota = Utility.sDbnull(txtBienchung.Text);
                else
                    objRavien.BienchungMota = "";
                if (chkTuvong.Checked)
                {
                    objRavien.TuvongNgay = dtpNgaytuvong.Value;
                    objRavien.TuvongDobenh = optDobenh.Checked;
                    objRavien.TuvongDotaibien = optDotaibiendieutri.Checked;
                    objRavien.TuvongDokhac = optLydokhac.Checked;
                    objRavien.TuvongTrong24gio = optTrong24gio.Checked;
                    objRavien.TuvongSau24h = optSau24Gio.Checked;
                    objRavien.TuvongTrong48h = optTrong48h.Checked;
                    objRavien.TuvongTrong72h = optTrong72h.Checked;
                    objRavien.TuvongNguyennhanchinh = autoLydotuvong.Text;
                    objRavien.TuvongNguyennhanchinhMa = autoLydotuvong.MyCode;
                    objRavien.TuvongKhamnghiemtuthi = chkKhamnghiemtuthi.Checked;
                    objRavien.TuvongChandoangiaiphaututhi = chkChandoangiaiphaututhi.Checked;
                    if (chkChandoangiaiphaututhi.Checked) objRavien.TuvongChandoangiaiphaututhiMota = Utility.sDbnull(txtChandoanGiaiphauTuthi);
                    else objRavien.TuvongChandoangiaiphaututhiMota = "";
                }
                else
                {
                    objRavien.TuvongNgay = null;
                    objRavien.TuvongDobenh = false;
                    objRavien.TuvongDotaibien = false;
                    objRavien.TuvongDokhac = false;
                    objRavien.TuvongTrong24gio = false;
                    objRavien.TuvongSau24h = false;
                    objRavien.TuvongTrong48h = false;
                    objRavien.TuvongTrong72h = false;
                    objRavien.TuvongNguyennhanchinh = "";
                    objRavien.TuvongNguyennhanchinhMa = "";
                    objRavien.TuvongKhamnghiemtuthi = false;
                    objRavien.TuvongChandoangiaiphaututhi = false;
                    objRavien.TuvongChandoangiaiphaututhiMota = "";
                }
                objRavien.YkienDexuat = Utility.DoTrim(txtYkien.Text);
                objRavien.PhuhopChandoanlamsang = Utility.Bool2byte(chkPhuHopChanDoanCLS.Checked);
                objRavien.NgayCapgiayravien = dtNGAY_CAP_GIAY_RVIEN.Value;
                KcbPhieuchuyenvien _phieuchuyenvien = null;
                if (chkChuyenvien.Checked)
                {
                    string idcname = "";
                    string idcCode = "";
                    Utility.GetChanDoan(objRavien.MabenhChinh, objRavien.MabenhPhu, objRavien.ChanDoan, ref idcname, ref idcCode);
                    string fullChandoan = string.Format("{0}:{1}", idcCode, idcname);

                    _phieuchuyenvien = new Select().From(KcbPhieuchuyenvien.Schema)
                        .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(ucThongtinnguoibenh1.txtIdBn.Text)
                        .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(ucThongtinnguoibenh1.txtMaluotkham.Text)
                        //.And(KcbPhieuchuyenvien.Columns.NoiTru).IsEqualTo(1)
                        .ExecuteSingle<KcbPhieuchuyenvien>();

                    if (_phieuchuyenvien == null)
                    {
                        _phieuchuyenvien = new KcbPhieuchuyenvien();
                        _phieuchuyenvien.IsNew = true;
                        _phieuchuyenvien.SoChuyentuyen = Laysochuyenvien();
                        _phieuchuyenvien.ChanDoan = fullChandoan;
                        _phieuchuyenvien.IdBenhnhan = objLuotkham.IdBenhnhan;
                        _phieuchuyenvien.MaLuotkham = objLuotkham.MaLuotkham;
                        _phieuchuyenvien.IdBenhvienChuyenden = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                        _phieuchuyenvien.DauhieuCls = "";// Utility.DoTrim(txtTinhtrangravien.Text);
                        _phieuchuyenvien.KetquaXnCls = "";
                        _phieuchuyenvien.NoiTru = 1;
                        _phieuchuyenvien.IdBacsiChuyenvien = objRavien.IdBacsiChuyenvien;
                        _phieuchuyenvien.ThuocSudung = "";
                        _phieuchuyenvien.LydoChuyen = 1;
                        _phieuchuyenvien.TrangthaiBenhnhan = Utility.DoTrim(txtKqdieutri.Text);
                        _phieuchuyenvien.HuongDieutri = "";// Utility.DoTrim(txtPhuongphapdieutri.Text);
                        _phieuchuyenvien.PhuongtienChuyen = Utility.DoTrim(txtphuongtienvc.Text);
                        _phieuchuyenvien.TenNguoichuyen = Utility.DoTrim(txtNguoivanchuyen.Text);
                        _phieuchuyenvien.NgayChuyenvien = objRavien.NgayRavien;
                        _phieuchuyenvien.IdKhoanoitru = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtIdkhoanoitru.Text, -1);
                        _phieuchuyenvien.IdBuong = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtidBuong.Text, -1);
                        _phieuchuyenvien.IdGiuong = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtidgiuong.Text, -1);
                    }
                    else
                    {
                        _phieuchuyenvien.IsNew = false;
                        if (_phieuchuyenvien.ChanDoan == "")
                            _phieuchuyenvien.ChanDoan = fullChandoan;
                        _phieuchuyenvien.NgayChuyenvien = objRavien.NgayRavien;
                        _phieuchuyenvien.IdKhoanoitru = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtIdkhoanoitru.Text, -1);
                        _phieuchuyenvien.IdBuong = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtidBuong.Text, -1);
                        _phieuchuyenvien.IdGiuong = Utility.Int32Dbnull(ucThongtinnguoibenh1.txtidgiuong.Text, -1);
                        _phieuchuyenvien.MarkOld();
                    }
                    //_phieuchuyenvien.IdBenhnhan = objLuotkham.IdBenhnhan;
                    //_phieuchuyenvien.MaLuotkham = objLuotkham.MaLuotkham;
                    //_phieuchuyenvien.IdBenhvienChuyenden = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                    //_phieuchuyenvien.DauhieuCls = "";// Utility.DoTrim(txtTinhtrangravien.Text);
                    //_phieuchuyenvien.KetquaXnCls = "";
                    //_phieuchuyenvien.ChanDoan = "";
                    //_phieuchuyenvien.NoiTru = 1;

                    //_phieuchuyenvien.IdBacsiChuyenvien = objRavien.IdBacsiChuyenvien;
                    //_phieuchuyenvien.ThuocSudung = "";
                    //_phieuchuyenvien.LydoChuyen = 1;
                    //_phieuchuyenvien.TrangthaiBenhnhan = Utility.DoTrim(txtKqdieutri.Text);
                    //_phieuchuyenvien.HuongDieutri = "";// Utility.DoTrim(txtPhuongphapdieutri.Text);
                    //_phieuchuyenvien.PhuongtienChuyen = Utility.DoTrim(txtphuongtienvc.Text);
                    //_phieuchuyenvien.TenNguoichuyen = Utility.DoTrim(txtNguoivanchuyen.Text);

                }
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objRavien.Save();
                        if (_phieuchuyenvien != null)
                        {
                            _phieuchuyenvien.IdRavien = (int)objRavien.IdRavien;
                            _phieuchuyenvien.Save();
                            objLuotkham.TthaiChuyendi = 1;

                            objLuotkham.IdBacsiChuyenvien = _phieuchuyenvien.IdBacsiChuyenvien;
                            objLuotkham.NgayRavien = objRavien.NgayRavien;
                            objLuotkham.IdBenhvienDi = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                        }
                        objLuotkham.NgayRavien = objRavien.NgayRavien;
                        objLuotkham.SoRavien = Utility.sDbnull(objRavien.IdRavien);
                        objLuotkham.IsNew = false;
                        objLuotkham.MarkOld();
                        objLuotkham.Save();

                        NoitruPhanbuonggiuong objNoitruPhanbuonggiuong =
                            NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien.Value);
                        if (objNoitruPhanbuonggiuong != null)
                        {
                            objNoitruPhanbuonggiuong.MarkOld();
                            objNoitruPhanbuonggiuong.IsNew = false;
                            objNoitruPhanbuonggiuong.NgayKetthuc = objRavien.NgayRavien;
                            objNoitruPhanbuonggiuong.TrangThai = 1;
                            objNoitruPhanbuonggiuong.CachtinhSoluong = 0;
                            objNoitruPhanbuonggiuong.NgaySua = globalVariables.SysDate;
                            objNoitruPhanbuonggiuong.NguoiSua = globalVariables.UserName;
                            objNoitruPhanbuonggiuong.SoluongGio = (int)Math.Ceiling((objNoitruPhanbuonggiuong.NgayKetthuc.Value - objNoitruPhanbuonggiuong.NgayVaokhoa).TotalHours);
                            objNoitruPhanbuonggiuong.SoLuong = THU_VIEN_CHUNG.Songay(objNoitruPhanbuonggiuong.NgayVaokhoa, objNoitruPhanbuonggiuong.NgayKetthuc.Value);
                            objNoitruPhanbuonggiuong.Save();
                            //Nhả giường
                            new Update(NoitruDmucGiuongbenh.Schema).Set(NoitruDmucGiuongbenh.Columns.DangSudung).EqualTo(0)
                                .Where(NoitruDmucGiuongbenh.Columns.IdGiuong).IsEqualTo(objNoitruPhanbuonggiuong.IdGiuong).Execute();
                        }
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm phiếu ra viện cho người bệnh có Id={0}, PID={1}, Tên={2} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                    }
                    scope.Complete();
                }
                mv_blnCancel = false;
                Utility.ShowMsg(m_enAct == action.Insert ? "Thêm mới phiếu ra viện thành công" : "Cập nhật phiếu ra viện thành công");
                //if (m_enAct == action.Insert)
                cmdPrint.Enabled = true;
                ModifyRavien();
                //if (cmdHuy.Tag.ToString() == "HUY")
                //    cmdHuy.Enabled = objRavien != null && objLuotkham != null && objLuotkham.TrangthaiNoitru == 3;
                //else
                //    cmdHuy.Enabled = objRavien != null && objLuotkham != null && objLuotkham.TrangthaiNoitru < 3;
                m_enAct = action.Update;
                txtId.Text = objRavien.IdRavien.ToString();
                if (chkInsaukhiluu.Checked) cmdPrint.PerformClick();
                if (chkChuyenvien.Checked)
                {
                    cmdChuyenvien.Visible = true;
                    cmdChuyenvien.Focus();
                    //cmdChuyenvien.PerformClick();
                }
                else
                    if (chkThoatsaukhiluu.Checked) this.Close();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommmands();
            }
        }
        private string Laysochuyenvien()
        {
            string sochuyenvien = "";
            StoredProcedure sp = SPs.SpGetSoChuyenVien(DateTime.Now.Year, sochuyenvien);
            sp.Execute();
            return Utility.sDbnull(sp.OutputValues[0], "-1");
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

       

        private void txtPhuongphapdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txtPhuongphapdieutri.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtKieuchuyenvien.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtKieuchuyenvien.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKieuchuyenvien.Text;
                txtKieuchuyenvien.Init();
                txtKieuchuyenvien._Text = oldCode;
                txtKieuchuyenvien.Focus();
            }
        }

      

        private void txtTinhtrangravien__OnSaveAs()
        {
            if (Utility.DoTrim(txtTinhtrangravien.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtTinhtrangravien.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtTinhtrangravien.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTinhtrangravien.Text;
                txtTinhtrangravien.Init();
                txtTinhtrangravien._Text = oldCode;
                txtTinhtrangravien.Focus();
            }
        }

       

        private void txtKqdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txtKqdieutri.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtKqdieutri.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtKqdieutri.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKqdieutri.Text;
                txtKqdieutri.Init();
                txtKqdieutri._Text = oldCode;
                txtKqdieutri.Focus();
            }
        }

       

        private void txtKieuchuyenvien__OnSaveAs()
        {
            if (Utility.DoTrim(txtPhuongphapdieutri.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtKieuchuyenvien.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtKieuchuyenvien.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKieuchuyenvien.Text;
                txtKieuchuyenvien.Init();
                txtKieuchuyenvien._Text = oldCode;
                txtKieuchuyenvien.Focus();
            }
        }

        private void frm_Phieuravien_Load(object sender, EventArgs e)
        {
            try
            {
                LoadUserConfigs();
                DataTable v_dtkhoanoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                DataBinding.BindDataCombobox(cboKhoaRavien, v_dtkhoanoitru, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
                dt_ICD_PHU = globalVariables.gv_dtDmucBenh.Clone();
                dtNGAY_CAP_GIAY_RVIEN.Value = dtpNgaytuvong.Value = dtpNgayHen.Value =dtpNgayin.Value= globalVariables.SysDate;
                LaydanhsachBacsi();
                baocaO_TIEUDE1.Init("noitru_phieuravien");
                AutocompleteBenhvien();
                AutocompleteICD();
                DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txtChandoan.LOAI_DANHMUC, txtKqdieutri.LOAI_DANHMUC
                , txtTinhtrangravien.LOAI_DANHMUC, txtKieuchuyenvien.LOAI_DANHMUC,
                 txtPhuongphapdieutri.LOAI_DANHMUC,txtphuongtienvc.LOAI_DANHMUC, autoLydotuvong.LOAI_DANHMUC,txtNguoivanchuyen.LOAI_DANHMUC }, true);
                txtNguoivanchuyen.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtNguoivanchuyen.LOAI_DANHMUC));
                txtChandoan.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtChandoan.LOAI_DANHMUC));
                txtKqdieutri.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtKqdieutri.LOAI_DANHMUC));
                txtTinhtrangravien.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtTinhtrangravien.LOAI_DANHMUC));
                //txtBenhgiaiphau.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtBenhgiaiphau.LOAI_DANHMUC));
                txtKieuchuyenvien.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtKieuchuyenvien.LOAI_DANHMUC));
                txtPhuongphapdieutri.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPhuongphapdieutri.LOAI_DANHMUC));
                txtphuongtienvc.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtphuongtienvc.LOAI_DANHMUC));
                autoLydotuvong.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLydotuvong.LOAI_DANHMUC));

                cboKhoaRavien.SelectedValue = objLuotkham.IdKhoanoitru;


                chkInsaukhiluu.Checked = Utility.getUserConfigValue(chkInsaukhiluu.Tag.ToString(), (byte)1) == 1;
                if (objLuotkham != null)
                {
                    ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                    ucThongtinnguoibenh1.txtMaluotkham.Focus();
                }
                if (AutoLoad)
                {
                    ucThongtinnguoibenh1.txtMaluotkham_KeyDown(ucThongtinnguoibenh1.txtMaluotkham, new KeyEventArgs(Keys.Enter));
                    dtpNgayravien.Focus();
                    dtpNgayravien.Select();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Loi: "+ ex.Message);
            }
        }

        private void LaydanhsachBacsi()
        {
            try
            {
                DataTable dtBacsi = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, -1);
                txtBsChidinh.Init(dtBacsi, new List<string>() {DmucNhanvien.Columns.IdNhanvien,DmucNhanvien.Columns.MaNhanvien,DmucNhanvien.Columns.TenNhanvien });
                txtTruongkhoa.Init(dtBacsi, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            }
            catch (Exception exception)
            {
                // throw;
            }
        }

        private void AutocompleteICD()
        {
            try
            {
                txtBenhchinh.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
                txtBenhphu.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
                txtBenhgiaiphau.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
                txtBenhnguyennhan.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
                txtBenhbienchung.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void frm_Phieuravien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ActiveControl != null && ((ActiveControl.Name == txtBenhphu.Name && txtBenhphu.MyCode != "-1") ||ActiveControl.Name==txtPhuongphapdieutri.Name))
                    return;
                else
                    ProcessTabKey(true);
            }
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdChuyen.PerformClick();
            if (e.Control && e.KeyCode == Keys.P) cmdPrint.PerformClick();
        }

        //public void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaluotkham.Text) != "")
        //        {
        //            AllowTextChanged = false;
        //            if (!AutoLoad)
        //            {
        //                var dtPatient = new DataTable();

        //                objLuotkham = null;
        //                string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
        //                ClearControls();

        //                dtPatient = new KCB_THAMKHAM().TimkiemBenhnhan(txtMaluotkham.Text, -1, 1, 0);

        //                DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
        //                if (arrPatients.GetLength(0) <= 0)
        //                {
        //                    if (dtPatient.Rows.Count > 1)
        //                    {
        //                        var frm = new frm_DSACH_BN_TKIEM("ALL");
        //                        frm.MaLuotkham = txtMaluotkham.Text;
        //                        frm.dtPatient = dtPatient;
        //                        frm.ShowDialog();
        //                        if (!frm.has_Cancel)
        //                        {
        //                            txtMaluotkham.Text = frm.MaLuotkham;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    txtMaluotkham.Text = _patient_Code;
        //                }
        //            }
        //            DataTable dt_Patient =
        //                new KCB_THAMKHAM().NoitruTimkiemThongtinBenhnhansaukhigoMaBN(txtMaluotkham.Text, -1, "ALL");
        //            if (dt_Patient != null && dt_Patient.Rows.Count > 0)
        //            {
        //                txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
        //                objLuotkham =
        //                    new Select().From(KcbLuotkham.Schema)
        //                        .Where(KcbLuotkham.Columns.IdBenhnhan)
        //                        .IsEqualTo(txtIdBn.Text)
        //                        .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
        //                        .ExecuteSingle<KcbLuotkham>();
        //                dtpNgaynhapvien.Value = objLuotkham.NgayNhapvien.Value;
        //                txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
        //                txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["Tuoi"], "");
        //                txtgioitinh.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.GioiTinh], "");
        //                txtDiachi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
        //                txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MatheBhyt], "");
        //                txtKhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_khoaphong_noitru"], "");
        //                txtBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucBuong.Columns.TenBuong], "");
        //                txtGiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucGiuongbenh.Columns.TenGiuong], "");
                        
        //                txtBenhchinh.SetCode(Utility.sDbnull(dt_Patient.Rows[0]["mabenh_chinh"],""));
        //                txtIdkhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdKhoanoitru], "-1");
        //                txtIdravien.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdRavien], "-1");
        //                txtidBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdBuong], "-1");
        //                txtidgiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdGiuong], "-1");
        //                cmdChuyen.Enabled = true;
        //                if (objLuotkham.TrangthaiNoitru == 0)
        //                {
        //                    Utility.ShowMsg(
        //                        "Bệnh nhân chưa vào nội trú nên không thể lập phiếu ra viện. Đề nghị bạn kiểm tra lại");
        //                    cmdChuyen.Enabled = false;
        //                    return;
        //                }
        //                cmdChuyen.Enabled = objLuotkham != null && objLuotkham.TrangthaiNoitru <= 3 && objLuotkham.TrangthaiNoitru >=1;
                        
        //                LoadData();
        //                dtpNgayravien.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
        //    }
        //    finally
        //    {
        //        AllowTextChanged = true;
        //    }
        //}

        private void LoadData()
        {
            //Tự động tính tổng số ngày điều trị
            txtTongSoNgayDtri.Text =
                THU_VIEN_CHUNG.Songay(objLuotkham.NgayNhapvien.Value,
                    new DateTime(dtpNgayravien.Value.Year, dtpNgayravien.Value.Month, dtpNgayravien.Value.Day,
                        Utility.Int32Dbnull(txtGioRaVien.Text, 0), Utility.Int32Dbnull(txtPhutRaVien.Text, 0), 0))
                    .ToString();

            objRavien =
                new Select().From(NoitruPhieuravien.Schema)
                    .Where(NoitruPhieuravien.Columns.IdBenhnhan)
                    .IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(NoitruPhieuravien.Columns.MaLuotkham)
                    .IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<NoitruPhieuravien>();
            m_enAct = objRavien != null ? action.Update : action.Insert;
            cmdPrint.Enabled = objRavien != null;
            dtpNgaynhapvien.Value = objLuotkham.NgayNhapvien.Value;
            txtTruongkhoa.SetId(ucThongtinnguoibenh1._khoaphong.IdTruongkhoa);
            cmdHuy.Enabled = objRavien != null && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 3  && objLuotkham.TrangthaiNoitru >=1;
            if (objRavien != null)
            {
                txtTruongkhoa.SetId(objRavien.IdBacsiChuyenvien);
                txtId.Text = objRavien.IdRavien.ToString();
                dtpNgayravien.Value = objRavien.NgayRavien;
                txtGioRaVien.Text = objRavien.NgayRavien.ToString("HH");
                txtPhutRaVien.Text = objRavien.NgayRavien.ToString("mm");
                txtSoRaVien.Text = objRavien.SophieuRavien;
                txtTongSoNgayDtri.Text = objRavien.TongsongayDieutri.ToString();
                txtBenhchinh.SetCode(objRavien.MabenhChinh);
                //txtChandoan.SetCode(objRavien.ChanDoan);
                txtChandoan._Text=objRavien.ChanDoan;
                //txtBenhchinh._Text = Utility.sDbnull(objRavien.MotaBenhchinh,objRavien.MabenhChinh);
                FillMabenhphu(objRavien.MabenhPhu);
                txtBenhgiaiphau.SetCode(objRavien.MabenhGiaiphau);
                optLanhtinh.Checked = Utility.Bool2Bool(objRavien.GpbLanhtinh);
                opActinh.Checked = Utility.Bool2Bool(objRavien.GpbActinh);
                optNghingo.Checked = Utility.Bool2Bool(objRavien.GpbNghingo);
                txtBenhbienchung.SetCode(objRavien.MabenhBienchung);
                chkBienchung.Checked = Utility.Bool2Bool(objRavien.Bienchung);
                txtBienchung.Text = objRavien.BienchungMota;
                chkTaibien.Checked = Utility.Bool2Bool(objRavien.Taibien);
                txtTaibien.Text = objRavien.TaibienMota;
                
                txtBenhnguyennhan.SetCode(objRavien.MabenhNguyennhan);
                txtKqdieutri.SetCode(objRavien.MaKquaDieutri);
                txtKieuchuyenvien.SetCode(objRavien.MaKieuchuyenvien);
                txtTinhtrangravien.SetCode(objRavien.MaTinhtrangravien);
                txtPhuongphapdieutri._Text=  objRavien.PhuongphapDieutri;
                txtMatheBHYT.Text = objRavien.MatheBhyt;
                dtInsFromDate.Value = objRavien.BhytTungay.HasValue ? objRavien.BhytTungay.Value : DateTime.Now;
                dtInsToDate.Value = objRavien.BhytDenngay.HasValue ? objRavien.BhytDenngay.Value : DateTime.Now;
                chkChuyenvien.Checked = Utility.Byte2Bool(objRavien.TrangthaiChuyenvien);
                cboKhoaRavien.SelectedValue = Utility.Int32Dbnull(objRavien.IdKhoaravien) > 0 ? Utility.Int32Dbnull(objRavien.IdKhoaravien) : Utility.Int32Dbnull(objRavien.IdKhoanoitru);
                if (chkChuyenvien.Checked)
                {
                    //KcbPhieuchuyenvien _chuyenvien = new Select().From(KcbPhieuchuyenvien.Schema).Where(KcbPhieuchuyenvien.Columns.IdRavien).IsEqualTo(objRavien.IdRavien).ExecuteSingle<KcbPhieuchuyenvien>();
                    //txtNguoivanchuyen._Text = _chuyenvien.TenNguoichuyen;
                    //txtphuongtienvc._Text = _chuyenvien.PhuongtienChuyen;
                }
                txtNoichuyenden.SetId(objRavien.IdBenhvienDi);
                txtLoidanBS.Text = objRavien.LoidanBacsi;
                txtYkien.Text = objRavien.YkienDexuat;
                if (objRavien.NgayhenTaikham.HasValue)
                {
                    txtSongayhentaikham.Text = objRavien.SongayhenTaikham.ToString();
                    dtpNgayHen.Value = objRavien.NgayhenTaikham.Value;
                    chkHentaikham.Checked = true;
                }
                else
                {
                    txtSongayhentaikham.Text = "";
                    dtpNgayHen.Value = DateTime.Now;
                    chkHentaikham.Checked = false;
                }
                if (objRavien.TuvongNgay.HasValue)
                {
                    chkTuvong.Checked = true;
                    dtpNgaytuvong.Value = objRavien.TuvongNgay.Value;
                }
                else
                {
                    chkTuvong.Checked = false;
                    dtpNgaytuvong.Value = DateTime.Now;
                }
                if(chkTuvong.Checked)
                {
                    optDobenh.Checked= Utility.Bool2Bool(objRavien.TuvongDobenh);
                    optDotaibiendieutri.Checked = Utility.Bool2Bool(objRavien.TuvongDotaibien);
                    chkttrvKhac.Checked = Utility.Bool2Bool(objRavien.TuvongDokhac);
                    optTrong24gio.Checked = Utility.Bool2Bool(objRavien.TuvongTrong24gio);
                    optSau24Gio.Checked = Utility.Bool2Bool(objRavien.TuvongSau24h);
                    optTrong48h.Checked = Utility.Bool2Bool(objRavien.TuvongTrong48h);
                    optTrong72h.Checked = Utility.Bool2Bool(objRavien.TuvongTrong72h);
                    chkKhamnghiemtuthi.Checked = Utility.Bool2Bool(objRavien.TuvongKhamnghiemtuthi);
                    autoLydotuvong._Text = objRavien.TuvongNguyennhanchinh;
                    chkChandoangiaiphaututhi.Checked = Utility.Bool2Bool(objRavien.TuvongChandoangiaiphaututhi);
                    txtChandoanGiaiphauTuthi.Text = objRavien.TuvongChandoangiaiphaututhiMota;
                }    
                if (objRavien.NgayCapgiayravien.HasValue)
                {
                    chkDaCapGiayRaVien.Checked = true;
                    dtNGAY_CAP_GIAY_RVIEN.Value = objRavien.NgayCapgiayravien.Value;
                }
                else
                {
                    chkDaCapGiayRaVien.Checked = false;
                    dtNGAY_CAP_GIAY_RVIEN.Value = DateTime.Now;
                }

                chkPhuHopChanDoanCLS.Checked = Utility.Byte2Bool(objRavien.PhuhopChandoanlamsang);
                txtBsChidinh.SetId(objRavien.IdBacsiChuyenvien);
                LayDanhsachdonthuoc();
            }
            else
            {
                dt_ICD_PHU.Rows.Clear();
                grd_ICD.DataSource = dt_ICD_PHU;
                ClearControls(new List<string>() { txtTongSoNgayDtri.Name, txtTruongkhoa.Name });
                txtBsChidinh.SetId(-1);
                 txtMatheBHYT.Clear();
                dtInsFromDate.Value =   DateTime.Now;
                dtInsToDate.Value = DateTime.Now;

                dtpNgayravien.Value = DateTime.Now;
                dtpNgaytuvong.Value = DateTime.Now;
                dtpNgayHen.Value = DateTime.Now;
                txtGioRaVien.Text = dtpNgayravien.Value.ToString("HH");
                txtPhutRaVien.Text = dtpNgayravien.Value.ToString("mm");
                txtId.Text = "";
                txtSoRaVien.Text = THU_VIEN_CHUNG.Laysoravien();
                dtpNgaytuvong.ResetText();
                chkTuvong.Checked = false;
                chkTaibien.Checked = chkBienchung.Checked = false;
                opActinh.Checked = optLanhtinh.Checked = optNghingo.Checked = false;
                chkKhamnghiemtuthi.Checked = chkChandoangiaiphaututhi.Checked = false;
                optDobenh.Checked = optDotaibiendieutri.Checked = optLydokhac.Checked = false;
                optTrong24gio.Checked = optSau24Gio.Checked = optTrong48h.Checked = optTrong72h.Checked = false;
                //txtTinhtrangravien.setDefaultValue();
                //txtLoidanBS.setDefaultValue();
                cmdPrint.Enabled = false;
                cmdHuy.Enabled = false;
            }
            cmdChuyenvien.Visible = chkChuyenvien.Checked;
        }

        private void AutocompleteBenhvien()
        {
            txtNoichuyenden.Init(globalVariables.gv_dtDmucBenhVien,new List<string>(){ DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien});
        }

        private void ClearControls(List<string> lstExcludeControlNames)
        {
            try
            {
                cmdPrint.Enabled = false;
                cmdHuy.Enabled = false;

                //foreach (Control control in pnlTop.Controls)
                //{
                //    if (control is EditBox)
                //    {
                //        ((EditBox) (control)).Clear();
                //    }
                //    else if (control is MaskedEditBox)
                //    {
                //        control.Text = "";
                //    }
                //    else if (control is AutoCompleteTextbox)
                //    {
                //        ((AutoCompleteTextbox) control)._Text = "";
                //    }
                //    else if (control is TextBox)
                //    {
                //        ((TextBox) (control)).Clear();
                //    }
                //}
                foreach (Control control in pnlFill.Controls)
                {
                    if (!lstExcludeControlNames.Contains(control.Name))
                    {
                        if (control is EditBox)
                        {
                            ((EditBox)(control)).Clear();
                        }
                        else if (control is MaskedEditBox)
                        {
                            control.Text = "";
                        }
                        else if (control is AutoCompleteTextbox)
                        {
                            ((AutoCompleteTextbox)control)._Text = "";
                        }
                        else if (control is TextBox)
                        {
                            ((TextBox)(control)).Clear();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void grd_ICD_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

        private void cmdgetPatient_Click_1(object sender, EventArgs e)
        {

        }

        private void dtpNgayravien_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void cmdChuyen_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdChuyen_Click_2(object sender, EventArgs e)
        {

        }

        private void cmdCreateNewPres_Click(object sender, EventArgs e)
        {
            try
            {
                cmdCreateNewPres.Enabled = false;
                if (objLuotkham == null) return;
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (objLuotkham==null)
                {
                    Utility.ShowMsg("Không tồn tại bệnh nhân! Bạn cần nạp lại thông tin dữ liệu", "Thông Báo");
                    return;
                }
                
                ThemMoiDonThuoc();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
               
            }
            finally
            {
                //cmdCreateNewPres.Enabled = true;
            }
            
        }
        private void ThemMoiDonThuoc()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("THUOC");
                frm.em_Action = action.Insert;
                frm.KieuDonthuoc = 3;
                frm.objLuotkham = objLuotkham;
                frm._KcbCDKL = null;
                frm._MabenhChinh = txtBenhchinh.MyCode;
                frm._Chandoan = txtChandoan.Text;
                frm.DtIcd = dt_ICD_PHU;
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = -1;
                frm.objCongkham = null;
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan, "-1");
                frm.txtSoDT.Text = objBenhnhan.DienThoai;
                frm.txtPatientName.Text = objBenhnhan.TenBenhnhan;
                frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = DateTime.Now;
                frm._ngayhenkhamlai = txtSongayhentaikham.Text;
                frm.noitru = 0;
                frm.txtLoiDanBS._Text = txtLoidanBS.Text;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();

                if (!frm.m_blnCancel)
                {
                    if (frm.chkNgayTaiKham.Checked)
                    {
                        dtpNgayHen.Value = frm.dtNgayKhamLai.Value;
                    }
                    else
                    {
                        dtpNgayHen.Value = DateTime.Now;
                        
                    }
                    LayDanhsachdonthuoc();
                    Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                }
                frm.Dispose();
                frm = null;
                GC.Collect();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                ModifyCommmands();
               
            }
        }
        DataTable m_dtPresDetail = new DataTable();
        private void LayDanhsachdonthuoc()
        {
            try
            {
                m_dtPresDetail =
                     new KCB_THAMKHAM().KcbThamkhamLayDanhsachDonThuocTheolankham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, -1l,-1l,3, "THUOC",-1,0).Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtPresDetail, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        void ModifyCommmands()
        {
            cmdCreateNewPres.Enabled = objLuotkham != null && objRavien != null && objLuotkham.TrangthaiNoitru<=5;
            cmdUpdatePres.Enabled = cmdDeletePres.Enabled = cmdPrintPres.Enabled = cmdWords.Enabled = Utility.isValidGrid(grdPresDetail) && objLuotkham != null && objRavien != null && objLuotkham.TrangthaiNoitru <= 5;
        }
        private void dtpNgayHen_ValueChanged(object sender, EventArgs e)
        {
            txtSongayhentaikham.Text = txtSongayhentaikham.Text = (dtpNgayHen.Value.Date - dtpNgayravien.Value.Date).Days.ToString();
        }

        private void chkDaCapGiayRaVien_CheckedChanged(object sender, EventArgs e)
        {
            dtNGAY_CAP_GIAY_RVIEN.Enabled = chkDaCapGiayRaVien.Checked;
        }

        private void chkHentaikham_CheckedChanged(object sender, EventArgs e)
        {
            txtSongayhentaikham.Enabled = dtpNgayHen.Enabled = chkHentaikham.Checked;
        }

        private void chkTuvong_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgaytuvong.Enabled =pnlLydotuvong.Enabled=pnlThoigiantuvong.Enabled=lblNguyennhanchinhtuvong.Enabled=autoLydotuvong.Enabled=chkKhamnghiemtuthi.Enabled=chkChandoangiaiphaututhi.Enabled=txtChandoanGiaiphauTuthi.Enabled= chkTuvong.Checked;
        }
        int Pres_ID = -1;
        GridEXRow RowThuoc = null;
        KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();
        private void cmdUpdatePres_Click(object sender, EventArgs e)
        {

            if (!cmdUpdatePres.Enabled) return;
          
          if (RowThuoc != null)
          {
              Pres_ID = Utility.Int32Dbnull(Utility.getCellValuefromGridEXRow(RowThuoc, KcbDonthuocChitiet.Columns.IdDonthuoc), -1);// grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
              if (Utility.Coquyen("quyen_suadonthuoc") || Utility.sDbnull(Utility.getCellValuefromGridEXRow(RowThuoc, KcbDonthuocChitiet.Columns.NguoiTao)) == globalVariables.UserName)
              {
                  UpdateDonThuoc();
              }
              else
              {
                  Utility.ShowMsg("Đơn thuốc đang chọn sửa được tạo bởi bác sĩ khác hoặc bạn không được gán quyền sửa(quyen_suadonthuoc). Vui lòng kiểm tra lại");
                  return;
              }
          }

        }

        private bool Donthuoc_DangXacnhan(int pres_id)
        {
            var _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id).And(
                    KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }

        private void UpdateDonThuoc()
        {
            try
            {
                if (grdPresDetail.RowCount > 0)//grdPresDetail.CurrentRow != null && grdPresDetail.CurrentRow.RowType == RowType.Record)
                {
                    if (objLuotkham != null)
                    {

                      
                        if (Donthuoc_DangXacnhan(Pres_ID))
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát thuốc tại phòng Dược");
                            return;
                        }
                        var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                            .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                            .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                            .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        if (v_collect.Count > 0)
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp thuốc để hủy xác nhận đơn thuốc tại kho thuốc");
                            return;
                        }
                        KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                        if (objPrescription != null)
                        {
                            var frm = new frm_KCB_KE_DONTHUOC("THUOC");
                            frm.em_Action = action.Update;
                            frm._KcbCDKL = null;
                            frm._MabenhChinh = txtBenhchinh.Text;
                            frm._Chandoan = txtChandoan.Text;
                            frm.DtIcd = dt_ICD_PHU;
                            frm.dt_ICD_PHU = dt_ICD_PHU;
                            frm.noitru = 0;
                            frm.objLuotkham = objLuotkham;
                            frm.id_kham = -1;
                            frm.objCongkham = null;
                            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                            frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                            frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                            frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                            frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                            frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.dtNgayKhamLai.MinDate = dtpNgayHen.Value;
                            frm._ngayhenkhamlai = dtpNgayravien.Value.ToString("yyMMdd") == dtpNgayHen.Value.ToString("yyMMdd") ? "" : dtpNgayHen.Text;

                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                dt_ICD_PHU = frm.dt_ICD_PHU;
                                if (frm.chkNgayTaiKham.Checked)
                                {
                                    dtpNgayHen.Value = frm.dtNgayKhamLai.Value;
                                }
                                else
                                {
                                    dtpNgayHen.Value = DateTime.Now;
                                }
                              
                                LayDanhsachdonthuoc();
                                Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc, Utility.sDbnull(frm.txtPres_ID.Text));
                            }
                            frm.Dispose();
                            frm = null;
                            GC.Collect();
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private void cmdDeletePres_Click(object sender, EventArgs e)
        {
            if (!KiemtraThuocTruockhixoa()) return;
            PerformActionDeletePres();
            ModifyCommmands();
        }
        private void PerformActionDeletePres()
        {
            string s = "";
            var lstIdchitiet = new List<int>();
            if (grdPresDetail.GetCheckedRows().Count() <= 0 && RowThuoc != null)
            {
                try
                {
                    RowThuoc.BeginEdit();
                    RowThuoc.IsChecked = true;
                    RowThuoc.EndEdit();
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 chi tiết thuốc để xóa");
                    return;
                }
            }
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                List<int> _temp = GetIdChitiet(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
            }
            if (lstIdchitiet.Count <= 0) return;
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            DataRow[] rows =
                         m_dtPresDetail.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + " IN (" + String.Join(",", lstIdchitiet.ToArray()) + ")");
            string _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                                    select Utility.sDbnull(p["ten_thuoc"])).ToList<string>());
            // UserName is Column Name
            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa đơn thuốc của bệnh nhân ID={0}, PID={1}, Tên={2}, DS thuốc xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            DeletefromDatatable(lstIdchitiet);
            m_dtPresDetail.AcceptChanges();
        }
        private List<int> GetIdChitiet(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr =
                m_dtPresDetail.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " +
                                      KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                                      + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                                      select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                return p.ToList();
            }
            return new List<int>();
        }
        private void DeletefromDatatable(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                DataRow[] p = (from q in m_dtPresDetail.Select("1=1").AsEnumerable()
                               where
                                   lstIdChitietDonthuoc.Contains(
                                       Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                               select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtPresDetail.Rows.Remove(p[i]);
                m_dtPresDetail.AcceptChanges();
            }
            catch
            {
            }
        }
        private bool KiemtraThuocTruockhixoa()
        {
            bool b_Cancel = false;
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các thuốc đang chọn hay không?", "Xác nhận xóa", true)) return false;
            if (RowThuoc == null)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suadonthuoc") || globalVariables.IsAdmin ||
                    Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        "Trong các thuốc bạn chọn xóa, có một số thuốc được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các thuốc do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int vIdChitietdonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    KcbDonthuocChitiet kcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(vIdChitietdonthuoc);
                    if (kcbDonthuocChitiet != null && (Utility.Byte2Bool(kcbDonthuocChitiet.TrangthaiThanhtoan) ||
                         Utility.Byte2Bool(kcbDonthuocChitiet.TrangThai)))
                    {
                        b_Cancel = true;
                        break;
                    }
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Một số thuốc bạn chọn đã thanh toán hoặc đã phát thuốc cho Bệnh nhân nên bạn không được phép xóa. Mời bạn kiểm tra lại ",
                    "Thông báo",
                    MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private void cmdChuyenvien_Click(object sender, EventArgs e)
        {
            frm_chuyenvien _chuyenvien = new frm_chuyenvien();
            _chuyenvien.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
            _chuyenvien.ucThongtinnguoibenh1.Refresh();
            _chuyenvien.ShowDialog();
        }

        private void cmdHuy_Click_1(object sender, EventArgs e)
        {

        }

        private void lblMatheBHYT_Click(object sender, EventArgs e)
        {

        }

        private void cmdHuyphieuravien_Click(object sender, EventArgs e)
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn bệnh nhân trước khi thực hiện hủy phiếu ra viện", true);
                return;
            }
            //Kiểm tra xem có đơn thuốc ra viện chưa
            DataTable dt = SPs.NoitruKiemtracodonthuocravien(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                Utility.ShowMsg("Người bệnh đã có đơn thuốc ra viện nên bạn không thể hủy phiếu. Nếu muốn hủy phiếu thì bạn cần hủy đơn thuốc ra viện.\nVui lòng kiểm tra lại");
                return;
            }
            if (objLuotkham.TrangthaiNoitru >= 3)
            {
                Utility.ShowMsg("Người bệnh đã được cho ra viện(có thể bởi người khác trong lúc bạn đang xử lý) nên bạn không thể hủy phiếu. Nếu muốn hủy phiếu thì bạn cần hủy ra viện trước khi hủy phiếu.\nVui lòng kiểm tra lại");
                return ;
            }
                if (objLuotkham.TrangthaiNoitru == 4)
                {
                    Utility.SetMsg(lblMsg,
                        "Bệnh nhân đã được xác nhận dữ liệu nội trú để ra viện nên bạn không thể hủy phiếu ra viện", true);
                    return;
                }
                if (objLuotkham.TrangthaiNoitru == 5)
                {
                    Utility.SetMsg(lblMsg,
                        "Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể hủy phiếu ra viện", true);
                    return;
                }
                if (objLuotkham.TrangthaiNoitru == 6)
                {
                    Utility.SetMsg(lblMsg,
                        "Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể hủy phiếu ra viện", true);
                    return;
                }
                KcbPhieuchuyenvien objphieucv = new Select().From(KcbPhieuchuyenvien.Schema)
                           .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(ucThongtinnguoibenh1.txtIdBn.Text)
                           .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(ucThongtinnguoibenh1.txtMaluotkham.Text)
                    //.And(KcbPhieuchuyenvien.Columns.NoiTru).IsEqualTo(1)
                           .ExecuteSingle<KcbPhieuchuyenvien>();
                if (objphieucv != null)
                {
                    Utility.ShowMsg("Người bệnh đã được lập phiếu chuyển viện. Do vậy bạn cần hủy phiếu chuyển viện trước khi hủy phiếu ra viện.\nVui lòng kiểm tra lại");
                    return;
                }
            bool Bedhasused=false;
            DataTable dtGiuong=new DataTable();
            NoitruPhanbuonggiuong objNoitruPhanbuonggiuong = null;
                if (
                    Utility.AcceptQuestion(
                        string.Format("Bạn có chắc chắn muốn hủy phiếu ra viện cho bệnh nhân {0} hay không?", ucThongtinnguoibenh1.txtTenBN.Text),
                        "Xác nhận hủy phiếu ra viện", true))
                {
                    try
                    {
                        using (var scope = new TransactionScope())
                        {
                            using (var dbscope = new SharedDbConnectionScope())
                            {
                                SPs.NoitruXoaphieuravien(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, objRavien.IdRavien, objLuotkham.IdRavien.Value).Execute();
                                //new Delete().From(NoitruPhieuravien.Schema)
                                //    .Where(NoitruPhieuravien.Columns.IdRavien).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1))
                                //    .Execute();
                                //new Delete().From(KcbPhieuchuyenvien.Schema)
                                //   .Where(KcbPhieuchuyenvien.Columns.IdRavien).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1))
                                //   .And(KcbPhieuchuyenvien.Columns.NoiTru).IsEqualTo(1)
                                //   .Execute();
                                 objNoitruPhanbuonggiuong =
                                    NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien.Value);
                                if (objNoitruPhanbuonggiuong != null && Utility.Int16Dbnull(objNoitruPhanbuonggiuong.IdGiuong, -1) > 0)
                                    //{
                                    //    objNoitruPhanbuonggiuong.MarkOld();
                                    //    objNoitruPhanbuonggiuong.IsNew = false;
                                    //    objNoitruPhanbuonggiuong.SoLuong = 0;
                                    //    objNoitruPhanbuonggiuong.SoluongGio = 0;
                                    //    objNoitruPhanbuonggiuong.NgayKetthuc = null;
                                    //    objNoitruPhanbuonggiuong.Save();
                                    //Lấy lại giường
                                    //Kiểm tra xem giường đã bị chiếm dụng hay chưa
                                    dtGiuong = new Select().From(NoitruDmucGiuongbenh.Schema)
                                        .Where(NoitruDmucGiuongbenh.Columns.IdGiuong).IsEqualTo(objNoitruPhanbuonggiuong.IdGiuong)
                                        .And(NoitruDmucGiuongbenh.Columns.DangSudung).IsEqualTo(1)
                                        .ExecuteDataSet().Tables[0];
                                if (dtGiuong.Rows.Count > 0)
                                {
                                    Utility.ShowMsg(string.Format("Giường cũ người bệnh nằm trước khi lập phiếu ra viện đã được sử dụng cho người bệnh khác. Vui lòng vào chức năng phân buồng giường đổi sang giường khác khi quay trở lại viện"));
                                   
                                    new Update(NoitruPhanbuonggiuong.Schema)
                                   .Set(NoitruPhanbuonggiuong.Columns.IdBuong).EqualTo(-1)
                                   .Set(NoitruPhanbuonggiuong.Columns.IdGiuong).EqualTo(-1)
                                   .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(objNoitruPhanbuonggiuong.Id).Execute();
                                    Bedhasused = true;
                                }
                                else
                                {
                                    new Update(NoitruDmucGiuongbenh.Schema).Set(NoitruDmucGiuongbenh.Columns.DangSudung).EqualTo(1)
                                        .Where(NoitruDmucGiuongbenh.Columns.IdGiuong).IsEqualTo(objNoitruPhanbuonggiuong.IdGiuong).Execute();
                                }
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy phiếu ra viện cho người bệnh có Id={0}, PID={1}, Tên={2} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                            }
                            scope.Complete();

                        }

                        if (objNoitruPhanbuonggiuong != null && Bedhasused && objNoitruPhanbuonggiuong.IdBuong > 0 && objNoitruPhanbuonggiuong.IdGiuong>0)
                        {

                            frm_lichsubuonggiuong _history = new frm_lichsubuonggiuong(objNoitruPhanbuonggiuong.IdKhoanoitru, (int)objNoitruPhanbuonggiuong.IdBuong, (int)objNoitruPhanbuonggiuong.IdGiuong, true);
                            _history.ShowDialog();
                        }
                        mv_blnCancel = false;
                        Utility.ShowMsg(string.Format("Hủy phiếu ra viện cho bệnh nhân {0} thành công", ucThongtinnguoibenh1.txtTenBN.Text));
                        cmdHuy.Enabled = false;
                        cmdPrint.Enabled = false;
                        cmdChuyen.Enabled = objLuotkham != null && globalVariables.isSuperAdmin?true: objLuotkham.TrangthaiNoitru <= 3;
                        LoadData();
                        ModifyRavien();
                        dtpNgayravien.Focus();
                    }
                    catch (Exception ex)
                    {
                        Utility.CatchException(ex);
                    }
                }
           
            
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void chkTaibien_CheckedChanged(object sender, EventArgs e)
        {
            txtTaibien.Enabled = chkTaibien.Checked;
            if (txtTaibien.Enabled) txtTaibien.Focus();
        }

        private void chkBienchung_CheckedChanged(object sender, EventArgs e)
        {
            txtBienchung.Enabled = chkBienchung.Checked;
            if (txtBienchung.Enabled) txtBienchung.Focus();
        }

        private void chkChandoangiaiphaututhi_CheckedChanged(object sender, EventArgs e)
        {
            txtChandoanGiaiphauTuthi.Enabled = chkChandoangiaiphaututhi.Checked;
            if (txtChandoanGiaiphauTuthi.Enabled) txtChandoanGiaiphauTuthi.Focus();
        }
    }
}