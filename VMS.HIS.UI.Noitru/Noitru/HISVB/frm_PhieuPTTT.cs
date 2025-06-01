using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using Aspose.Words;
using VNS.HIS.UCs;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Classess;
using System.Runtime.InteropServices;
using System.Threading;
using System.Transactions;
using VNS.Libs.AppUI;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_PhieuPTTT : Form
    {
        #region Variables
        private DataTable m_bacsi;
        private DataTable m_DsChiDinh = new DataTable();
        //private DataTable m_DsChiDinh_chitiet = new DataTable();
        private DataTable m_loaipt = new DataTable();
        private DataTable m_phieupttt = new DataTable();
        private DataTable m_phieupttt_chitiet = new DataTable();
        private DataTable m_dtKhoaNoiTru = new DataTable();
        private DataTable m_dtPhong = new DataTable();
        private DataTable m_dtGiuong = new DataTable();
        private DataTable m_dtLyDoTaiBien = new DataTable();
        private DataTable m_dtLyDoTuVong = new DataTable();
        private bool b_Hasloaded = false;
        private string _rowFilter = "1=1";
        private bool AllowSeletionChanged = false;
        private string malakham = "";
        private KcbLuotkham objLuotkham;
        private KcbDanhsachBenhnhan objBenhnhan;
        private long ID_PHIEUPTTT;
        public action m_enAct = action.Insert;
        public DataTable dtbsphauthuat = new DataTable();
        public DataTable dtbsgayme = new DataTable();
        public DataTable dtbsphauthuatphu = new DataTable();
        public DataTable dtdieuduonggayme = new DataTable();
        public DataTable dtDieuduongvongngoai = new DataTable();
        public DataTable dtDieuduongvongtrong = new DataTable();
        public bool b_CallParent = false;
        public int _assignDetailid = -1;
        public int _nPatient_ID = -1;
        public KcbPhieupttt objpttt = null;
        string noitru = "2";//0=ngoại trú;1= nội trú;2=Tất cả
        #endregion

        #region Form events
        public frm_PhieuPTTT(string noitru)
        {
            InitializeComponent();
            this.noitru = noitru;
            radNoiTru.Checked = noitru == "1";
            radNgoaiTru.Checked = noitru == "0";
            Shown += frm_PhieuPTTT_Shown;
            FormClosing += frm_PhieuPTTT_FormClosing;
            Utility.SetVisualStyle(this);
            dtNgayPhauThuat.Value = THU_VIEN_CHUNG.GetSysDateTime();
            dtpNgayRut.Value = dtNgayPhauThuat.Value;
            dtpNgayCatChi.Value = dtNgayPhauThuat.Value;
            autoBSPhauthuat._OnEnterMe += autoBSPhauthuat__OnEnterMe;
            autoBSGayme._OnEnterMe += autoBSGayme__OnEnterMe;
            autoBSphu._OnEnterMe += autoBSphu__OnEnterMe;
            autoDieuduonggayme._OnEnterMe += autoDieuduonggayme__OnEnterMe;
            autoDieuduongvongngoai._OnEnterMe += autoDieuduongvongngoai__OnEnterMe;
            autoDieuduongvongtrong._OnEnterMe += autoDieuduongvongtrong__OnEnterMe;
            autoLoaiPTTT._OnShowDataV1 += __OnShowDataV1;
            txtChanDoanSauPT._OnShowDataV1 += __OnShowDataV1;
            txtChanDoanTruocPT._OnShowDataV1 += __OnShowDataV1;
            txtPhuongPhapPT._OnShowDataV1 += __OnShowDataV1;
            txtPhuongPhapVoCam._OnShowDataV1 += __OnShowDataV1;
            txtLuocDoPhauThuat._OnShowDataV1 += __OnShowDataV1;
            chkPTTT_KetThuc.CheckedChanged += chkPTTT_KetThuc_CheckedChanged;
            autoLydotaibien._OnShowDataV1 += __OnShowDataV1;
            autoLydotuvong._OnShowDataV1 += __OnShowDataV1;
            ucThongtinnguoibenh_doc_v11._OnEnterMe += ucThongtinnguoibenh_doc_v11__OnEnterMe;
            grdChiDinh.SelectionChanged += grdChiDinh_SelectionChanged;
            grdPhieuPTTT.SelectionChanged += grdPhieuPTTT_SelectionChanged;
            dtbsgayme = globalVariables.gv_dtDmucNhanvien.Clone();
            dtbsphauthuat = globalVariables.gv_dtDmucNhanvien.Clone();
            dtbsphauthuatphu = globalVariables.gv_dtDmucNhanvien.Clone();
            dtdieuduonggayme = globalVariables.gv_dtDmucNhanvien.Clone();
            dtDieuduongvongngoai = globalVariables.gv_dtDmucNhanvien.Clone();
            dtDieuduongvongtrong = globalVariables.gv_dtDmucNhanvien.Clone();
            InitData();
            grd_bsgm.ColumnButtonClick += grd_bsgm_ColumnButtonClick;
            grd_bspt.ColumnButtonClick += grd_bspt_ColumnButtonClick;
            grd_bsphauthuatphu.ColumnButtonClick += grd_bsphauthuatphu_ColumnButtonClick;

            grdDieuduonggayme.ColumnButtonClick += grdDieuduonggayme_ColumnButtonClick;
            grdDieuduongvongngoai.ColumnButtonClick += grdDieuduongvongngoai_ColumnButtonClick;
            grdDieuduongvongtrong.ColumnButtonClick += grdDieuduongvongtrong_ColumnButtonClick;
            autoKhoa._OnEnterMe+=autoKhoa__OnEnterMe;
            autoBuong._OnEnterMe += autoBuong__OnEnterMe;
            grdChiDinh.MouseDoubleClick += grdChiDinh_MouseDoubleClick;
            grdPhieuPTTT.MouseDoubleClick += grdPhieuPTTT_MouseDoubleClick;
            txtPhuongPhapPT._OnSaveAsV1 += _OnSaveAs;
            txtPhuongPhapVoCam._OnSaveAsV1 += _OnSaveAs;
            chkTaibien.CheckedChanged += ChkTaibien_CheckedChanged;
            chkBienchung.CheckedChanged += ChkBienchung_CheckedChanged;
        }

        private void ChkBienchung_CheckedChanged(object sender, EventArgs e)
        {
            autoLydobienchung.Enabled = chkBienchung.Checked;
            if (chkTaibien.Checked)
            {
                chkTaibien.Checked = false;
            }
        }

        private void ChkTaibien_CheckedChanged(object sender, EventArgs e)
        {
            autoLydotaibien.Enabled = chkTaibien.Checked;
            if (chkTaibien.Checked)
            {
                chkBienchung.Checked = false;
            }    
        }

        void _OnSaveAs(AutoCompleteTextbox_Danhmucchung obj)
        {
            if (Utility.DoTrim(obj.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, obj.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }  
        }

       
        void InitData()
        {
            try
            {
                DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txtChanDoanTruocPT.LOAI_DANHMUC,txtPhuongPhapPT.LOAI_DANHMUC, txtPhuongPhapVoCam.LOAI_DANHMUC
                , txtLuocDoPhauThuat.LOAI_DANHMUC, txtChanDoanSauPT.LOAI_DANHMUC,autoLydotaibien.LOAI_DANHMUC,autoLydotuvong.LOAI_DANHMUC,autoLoaiPTTT.LOAI_DANHMUC  }, true);
                txtPhuongPhapPT.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPhuongPhapPT.LOAI_DANHMUC));
                txtPhuongPhapVoCam.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPhuongPhapVoCam.LOAI_DANHMUC));
                txtLuocDoPhauThuat.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtLuocDoPhauThuat.LOAI_DANHMUC));
                txtChanDoanSauPT.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtChanDoanSauPT.LOAI_DANHMUC));
                txtChanDoanTruocPT.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtChanDoanTruocPT.LOAI_DANHMUC));
                autoLydotaibien.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLydotaibien.LOAI_DANHMUC));
                autoLydotuvong.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLydotuvong.LOAI_DANHMUC));
                autoLoaiPTTT.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLoaiPTTT.LOAI_DANHMUC));
                //autoDieuduongvongngoai.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoDieuduongvongngoai.LOAI_DANHMUC));
                //autoDieuduongvongtrong.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoDieuduongvongtrong.LOAI_DANHMUC));
                //dtDieuduongvongngoai = autoDieuduongvongngoai.dtData.Clone();
                //dtDieuduongvongtrong = autoDieuduongvongngoai.dtData.Clone();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex); 
            }
           
        }

        void chkPTTT_KetThuc_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgayGioKetThucPTTT.Enabled = chkPTTT_KetThuc.Checked;
        }

        void autoDieuduonggayme__OnEnterMe()
        {
            if (autoDieuduonggayme.MyID != "-1")
            {
                AddBacsi(dtdieuduonggayme, grdDieuduonggayme, autoDieuduonggayme);
                autoDieuduonggayme.Focus();
                autoDieuduonggayme.SelectAll();
            }
        }

        void autoDieuduongvongtrong__OnEnterMe()
        {
            if (autoDieuduongvongtrong.MyID != "-1")
            {
                AddBacsi(dtDieuduongvongtrong, grdDieuduongvongtrong, autoDieuduongvongtrong);
                autoDieuduongvongtrong.Focus();
                autoDieuduongvongtrong.SelectAll();
            }
        }

        void autoDieuduongvongngoai__OnEnterMe()
        {
            if (autoDieuduongvongngoai.MyID != "-1")
            {
                AddBacsi(dtDieuduongvongngoai, grdDieuduongvongngoai, autoDieuduongvongngoai);
                autoDieuduongvongngoai.Focus();
                autoDieuduongvongngoai.SelectAll();
            }
        }

        void grdDieuduongvongtrong_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa điều dưỡng vòng trong: {0} không?", grdDieuduongvongtrong.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grdDieuduongvongtrong.CurrentRow.Delete();
                        dtDieuduongvongtrong.AcceptChanges();
                        grdDieuduongvongtrong.Refetch();
                        grdDieuduongvongtrong.AutoSizeColumns();

                    }
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

        void grdDieuduongvongngoai_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa điều dưỡng vòng ngoài: {0} không?", grdDieuduongvongngoai.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grdDieuduongvongngoai.CurrentRow.Delete();
                        dtDieuduongvongngoai.AcceptChanges();
                        grdDieuduongvongngoai.Refetch();
                        grdDieuduongvongngoai.AutoSizeColumns();

                    }
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

        void grdDieuduonggayme_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa điều dưỡng gây mê: {0} không?", grdDieuduonggayme.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grdDieuduonggayme.CurrentRow.Delete();
                        dtdieuduonggayme.AcceptChanges();
                        grdDieuduonggayme.Refetch();
                        grdDieuduonggayme.AutoSizeColumns();

                    }
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

        void grdPhieuPTTT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChonPhieuPTTT();
        }

        void grdChiDinh_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (!Utility.isValidGrid(grdChiDinh)) return;
            //cmdAddNew.PerformClick();
        }

        void autoBuong__OnEnterMe()
        {
            if (radNoiTru.Checked)
            {
                m_dtGiuong = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(autoKhoa.MyID), Utility.Int32Dbnull(autoBuong.MyID), 0);
                autoGiuong.Init(m_dtGiuong,
                    new List<string>
                {
                    NoitruDmucGiuongbenh.Columns.IdGiuong,
                    NoitruDmucGiuongbenh.Columns.MaGiuong,
                    NoitruDmucGiuongbenh.Columns.TenGiuong
                });
            }
        }

        void autoKhoa__OnEnterMe()
        {
            if (radNoiTru.Checked)
            {
                DataTable m_dtDataRoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(autoKhoa.MyID));
                autoBuong.Init(m_dtDataRoom,
                    new List<string>
                        {
                            NoitruDmucBuong.Columns.IdBuong,
                            NoitruDmucBuong.Columns.MaBuong,
                            NoitruDmucBuong.Columns.TenBuong
                        });
                autoBuong.RaiseEnterEvents();
            }
        }


        void frm_PhieuPTTT_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkHoitruockhixoa.Tag.ToString(), Utility.Bool2byte(chkHoitruockhixoa.Checked));
                Utility.SaveUserConfig(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked));
                Utility.SaveUserConfig(chkCloseAfterSave.Tag.ToString(), Utility.Bool2byte(chkCloseAfterSave.Checked));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void LoadUserConfigs()
        {
            try
            {
                chkHoitruockhixoa.Checked = Utility.getUserConfigValue(chkHoitruockhixoa.Tag.ToString(), Utility.Bool2byte(chkHoitruockhixoa.Checked)) == 1;
                chkPreview.Checked = Utility.getUserConfigValue(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked)) == 1;
                chkCloseAfterSave.Checked = Utility.getUserConfigValue(chkCloseAfterSave.Tag.ToString(), Utility.Bool2byte(chkCloseAfterSave.Checked)) == 1;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        
        void frm_PhieuPTTT_Shown(object sender, EventArgs e)
        {
            LoadUserConfigs();
        }

        void grd_bsphauthuatphu_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa bác sĩ phẫu thuật phụ: {0} không?", grd_bsphauthuatphu.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grd_bsphauthuatphu.CurrentRow.Delete();
                        dtbsphauthuatphu.AcceptChanges();
                        grd_bsphauthuatphu.Refetch();
                        grd_bsphauthuatphu.AutoSizeColumns();
                        
                    }
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

        void grd_bspt_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa bác sĩ phẫu thuật : {0} không?", grd_bspt.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grd_bspt.CurrentRow.Delete();
                        dtbsphauthuat.AcceptChanges();
                        grd_bspt.Refetch();
                        grd_bspt.AutoSizeColumns();

                    }
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

        void grd_bsgm_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa bác sĩ gây mê: {0} không?", grd_bsgm.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grd_bsgm.CurrentRow.Delete();
                        dtbsgayme.AcceptChanges();
                        grd_bsgm.Refetch();
                        grd_bsgm.AutoSizeColumns();

                    }
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
        void ChonPhieuPTTT()
        {
            try
            {
                if (Utility.isValidGrid(grdPhieuPTTT))
                {
                    objpttt = KcbPhieupttt.FetchByID(Utility.Int64Dbnull(grdPhieuPTTT.GetValue(KcbPhieupttt.Columns.IdPhieu)));
                    ID_PHIEUPTTT = objpttt.IdPhieu;
                    if (m_enAct != action.View) m_enAct = action.Update;
                    //lblStatus.Text = "Cập nhật phiếu PTTT. Nhấn nút Hủy để chọn dịch vụ khác";
                    FillData4Update();
                    
                    autoKhoa.Focus();
                }
                else
                {
                    
                    ID_PHIEUPTTT = -1;
                    objpttt = new KcbPhieupttt();
                    //lblStatus.Text = "Thêm mới phiếu PTTT. Nhấn nút Hủy để chọn dịch vụ khác";
                    ClearControl();
                   
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }
        void grdPhieuPTTT_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowSeletionChanged) return;
            ChonPhieuPTTT();
        }
        void ModifyCommands()
        {
            cmdAddNew.Enabled = Utility.isValidGrid(grdChiDinh) && objLuotkham!=null && !isDoing && grdPhieuPTTT.GetDataRows().Count()<=0;
            cmdPrint.Enabled = cmdDelete.Enabled = Utility.isValidGrid(grdPhieuPTTT) && objLuotkham != null;
            cmdSave.Enabled = !cmdAddNew.Enabled || grdPhieuPTTT.RowCount > 0;
            if (grdChiDinh.RowCount <= 0) ClearControl();
            if (m_enAct == action.View)
                cmdAddNew.Enabled = cmdDelete.Enabled = cmdSave.Enabled = false;

        }
        void ResetColor()
        {
            if (!lstKhongbatnhap.Contains(ma_dichvu))
            {
                lblBSgayme.ForeColor = lblDieuduonggayme.ForeColor = lbldieuduongvongngoai.ForeColor = lbldieuduongvongtrong.ForeColor = Color.Black;
                grd_bsgm.RootTable.Columns["ten_nhanvien"].HeaderStyle.ForeColor = grdDieuduonggayme.RootTable.Columns["ten_nhanvien"].HeaderStyle.ForeColor = grdDieuduongvongtrong.RootTable.Columns["ten_nhanvien"].HeaderStyle.ForeColor = grdDieuduongvongngoai.RootTable.Columns["ten_nhanvien"].HeaderStyle.ForeColor = Color.Black;
            }
            else
            {
                lblBSgayme.ForeColor = lblDieuduonggayme.ForeColor = lbldieuduongvongngoai.ForeColor = lbldieuduongvongtrong.ForeColor = Color.Red;
                grd_bsgm.RootTable.Columns["ten_nhanvien"].HeaderStyle.ForeColor = grdDieuduonggayme.RootTable.Columns["ten_nhanvien"].HeaderStyle.ForeColor = grdDieuduongvongtrong.RootTable.Columns["ten_nhanvien"].HeaderStyle.ForeColor = grdDieuduongvongngoai.RootTable.Columns["ten_nhanvien"].HeaderStyle.ForeColor = Color.Red;

            }
        }
        DataTable dtPttt = new DataTable();
        long IdChitietchidinh = -1;
        string ma_dichvu = "";
        void ChonChidinh()
        {
            try
            {
                ma_dichvu = "";

                if (!Utility.isValidGrid(grdChiDinh) || !AllowSeletionChanged) return;
                ma_dichvu = Utility.sDbnull(grdChiDinh.GetValue("ma_dichvu"), "PTTT");
                IdChitietchidinh = Utility.Int64Dbnull(grdChiDinh.GetValue(KcbChidinhclsChitiet.Columns.IdChitietchidinh));
                if (noitru == "2")
                {
                    if (Utility.Int16Dbnull(grdChiDinh.GetValue("noitru")) == 1)
                        radNoiTru.Checked = true;
                    else
                        radNoiTru.Checked = false;
                }
                autoGiuong.Enabled = autoBuong.Enabled = radNoiTru.Checked;
                dtPttt = SPs.KcbPtttTimkiemdanhsachPtttTheodichvucls(IdChitietchidinh).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPhieuPTTT, dtPttt, true, true, "1=1", "ngay_pttt");
                if (grdPhieuPTTT.RowCount <= 0)
                    ClearControl();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }
        void grdChiDinh_SelectionChanged(object sender, EventArgs e)
        {
            ChonChidinh();
        }
        DataTable dtbuonggiuong = new DataTable();
        void ucThongtinnguoibenh_doc_v11__OnEnterMe()
        {
            if (ucThongtinnguoibenh_doc_v11.objLuotkham != null)
            {
                AllowSeletionChanged = false;
                objLuotkham = ucThongtinnguoibenh_doc_v11.objLuotkham;
                 dtbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema).
                Where(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteDataSet().Tables[0];
               
                autoKhoa.SetId(objLuotkham.IdKhoanoitru);
                autoKhoa.RaiseEnterEvents();
                autoBuong.SetId(objLuotkham.IdBuong);
                autoBuong.RaiseEnterEvents();
                autoGiuong.SetId(objLuotkham.IdGiuong);
                GetCls();
            }
        }
        void GetCls()
        {
            try
            {
                byte ntnt = noitru == "0" ? (byte)0 : (noitru == "1" ? (byte)1 : (byte)100);
                DataTable dtCls = SPs.KcbPtttTimkiemchidinhPttt(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, ntnt).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChiDinh, dtCls, true, true, "1=1", "");
                AllowSeletionChanged = true;
                grdChiDinh_SelectionChanged(grdChiDinh, new EventArgs());
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        string getBacsithamgia(DataTable dtData)
        {
            var q = from p in dtData.AsEnumerable()
                    select Utility.sDbnull(p["id_nhanvien"], "");
            return string.Join(",", q.ToArray<string>());
        }
        string getFieldValue(DataTable dtData,string fieldName)
        {
            var q = from p in dtData.AsEnumerable()
                    select Utility.sDbnull(p[fieldName], "");
            return string.Join(",", q.ToArray<string>());
        }
        
        void FillBacsiPttt(string dataString,DataTable dtData,GridEX grdlist)
        {
            dtData.Clear();
            if (!string.IsNullOrEmpty(dataString) && dtData.Columns.Count>0)
            {
                string[] rows = dataString.Split(',');
                foreach (string row in rows)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        DataRow newDr = dtData.NewRow();
                        newDr[DmucNhanvien.Columns.IdNhanvien] =Utility.Int16Dbnull( row,-1);
                        newDr[DmucNhanvien.Columns.TenNhanvien] =  LaytenNvien(Utility.sDbnull( row,-1));
                        dtData.Rows.Add(newDr);
                        dtData.AcceptChanges();
                    }
                }
                grdlist.DataSource = dtData;
            }
        }
        //void FillDungcu(string dataString, DataTable dtData, GridEX grdlist, bool dungcu_trongngoai)
        //{
        //    dtData.Clear();
        //    if (!string.IsNullOrEmpty(dataString) && dtData.Columns.Count>0)
        //    {
        //        string[] rows = dataString.Split(',');
        //        foreach (string row in rows)
        //        {
        //            if (!string.IsNullOrEmpty(row))
        //            {
        //                DataRow newDr = dtData.NewRow();
        //                newDr[DmucChung.Columns.Ma] = Utility.Int16Dbnull(row, -1);
        //                newDr[DmucChung.Columns.Ten] = LaytenDungcu(Utility.sDbnull(row, -1), dungcu_trongngoai);
        //                dtData.Rows.Add(newDr);
        //                dtData.AcceptChanges();
        //            }
        //        }
        //        grdlist.DataSource = dtData;
        //    }
        //}
        //private string LaytenDungcu(string ma_dungcu,bool dungcu_trongngoai)
        //{
        //    string ten_dungcu = "";
        //    DataRow[] arrmaDungcu =
        //        dungcu_trongngoai ? autoDieuduongvongtrong.dtData.Select(string.Format(DmucChung.Columns.Ma + "='{0}'", ma_dungcu)) : autoDieuduongvongngoai.dtData.Select(string.Format(DmucChung.Columns.Ma + "='{0}'", ma_dungcu));
        //    if (arrmaDungcu.GetLength(0) > 0) ten_dungcu = Utility.sDbnull(arrmaDungcu[0][DmucChung.Columns.Ten], "");
        //    return ten_dungcu;
        //}
        private string LaytenNvien(string id_nhanvien)
        {
            try
            {
                string TenNhanvien = "";
                DataRow[] arrMaBenh =
                    globalVariables.gv_dtDmucNhanvien.Select(string.Format(DmucNhanvien.Columns.IdNhanvien + "={0}", id_nhanvien));
                if (arrMaBenh.GetLength(0) > 0) TenNhanvien = Utility.sDbnull(arrMaBenh[0][DmucNhanvien.Columns.TenNhanvien], "");
                return TenNhanvien;
            }
            catch (Exception ex)
            {
                return "";
            }
           
        }
        void FillData4Update()
        {
            try
            {
                if (objpttt != null)
                {
                    autoKhoa.SetId(objpttt.IdKhoadieutri);
                    autoBuong.SetId(objpttt.IdBuong);
                    autoGiuong.SetId(objpttt.IdGiuong);
                    dtNgayPhauThuat.Value = objpttt.NgayPttt;
                    chkPTTT_KetThuc.Checked = objpttt.NgayKetthuc.HasValue;
                    if (chkPTTT_KetThuc.Checked)
                        dtpNgayGioKetThucPTTT.Value = objpttt.NgayKetthuc.Value;
                    else
                        dtpNgayGioKetThucPTTT.Value = DateTime.Now;
                    FillBacsiPttt(objpttt.IdbacsiGayme, dtbsgayme, grd_bsgm);
                    FillBacsiPttt(objpttt.IdbacsiPttt, dtbsphauthuat, grd_bspt);
                    FillBacsiPttt(objpttt.IdbacsiPtttPhu, dtbsphauthuatphu, grd_bsphauthuatphu);
                    FillBacsiPttt(objpttt.IdDieuduonggayme, dtdieuduonggayme, grdDieuduonggayme);

                    FillBacsiPttt(objpttt.MaDungcuvongtrong, dtDieuduongvongtrong, grdDieuduongvongtrong);
                    FillBacsiPttt(objpttt.MaDungcuvongngoai, dtDieuduongvongngoai, grdDieuduongvongngoai);
                    dtpNgayGioKetThucPTTT.Enabled = chkPTTT_KetThuc.Checked;
                    txtIdPhieuPTTT.Text = objpttt.IdPhieu.ToString();
                    txtMaphieu.Text = objpttt.MaPhieu;
                    txtChanDoanTruocPT._Text = objpttt.TruocPttt;
                    txtChanDoanSauPT._Text = objpttt.SauPttt;
                    autoLoaiPTTT.SetCode(objpttt.LoaiPttt);
                    txtPhuongPhapPT._Text=objpttt.PhuongphapPttt;
                    txtPhuongPhapVoCam._Text=objpttt.PhuongphapVocam;
                    txtLuocDoPhauThuat._Text = objpttt.LuocdoPttt;
                    txtDanLuu.Text = objpttt.DanLuu;
                    txtBac.Text = objpttt.Bac;
                    chkNgayRut.Checked = objpttt.NgayRut.HasValue;
                    if (chkNgayRut.Checked)
                        dtpNgayRut.Value = objpttt.NgayRut.Value;
                    else
                        dtpNgayRut.Value = DateTime.Now;
                    chkNgayCatChi.Checked = objpttt.NgayCatchi.HasValue;
                    if (chkNgayCatChi.Checked)
                        dtpNgayCatChi.Value = objpttt.NgayCatchi.Value;
                    else
                        dtpNgayCatChi.Value = DateTime.Now;
                    txtKhac.Text = objpttt.Khac;
                    txtTrinhTuPhauThat.Text = objpttt.TrinhtuPttt;
                    chkTaibien.Checked = Utility.Bool2Bool(objpttt.TaiBien);
                    chkBienchung.Checked = Utility.Bool2Bool(objpttt.BienChung);
                    chkTuvong.Checked = Utility.Bool2Bool(objpttt.TuVong);
                    if (chkTuvong.Checked) dtNgayGioTuVong.Value = objpttt.NgayTuvong.Value;
                    autoLydotuvong.SetCode(objpttt.LydoTuvong);
                    autoLydotaibien.SetCode(objpttt.LydoTaibien);
                    autoLydotaibien.SetCode(objpttt.LydoTaibien);
                    autoLydobienchung.SetCode(objpttt.LydoBienchung);
                    autoLydotuvong.Enabled = chkTuvong.Checked;
                    autoLydobienchung.Enabled = chkBienchung.Checked;
                    if (Utility.Bool2Bool(objpttt.Noitru))
                        radNoiTru.Checked = true;
                    else
                        radNgoaiTru.Checked = true;
                    bool temp = b_Hasloaded;
                    b_Hasloaded = false;
                    if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                    {
                        if (picPTTT.Image != null)
                        {
                            picPTTT.Image.Dispose();
                            picPTTT.Image = null;
                        }
                        cboHinhPTTT.SelectedValue = 0;
                    }
                    else //if (objpttt.MaHinhanh == "1")
                    {
                        cboHinhPTTT.SelectedIndex =Utility.Int32Dbnull(objpttt.MaHinhanh) ;// Utility.GetSelectedIndex(cboHinhPTTT, objpttt.MaHinhanh);
                        if (objpttt.MaHinhanh != "-1")
                        {

                            //myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            picPTTT.Image = Image.FromFile(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                        }
                        else
                            cboHinhPTTT.SelectedIndex = 0;
                    }
                    b_Hasloaded = temp;
                    //else if (objpttt.MaHinhanh == "2")
                    //{
                    //    picPTTT.Image = Image.FromFile(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                    //}
                    //else if (objpttt.MaHinhanh == "3")
                    //{
                    //    picPTTT.Image = Image.FromFile(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                    //}
                }
                else
                {
                    if (picPTTT.Image != null)
                    {
                        picPTTT.Image.Dispose();
                        picPTTT.Image = null;
                    }
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void __OnShowDataV1(AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }
        }

        void autoBSphu__OnEnterMe()
        {
            if (autoBSphu.MyID!="-1")
            {
                AddBacsi(dtbsphauthuatphu, grd_bsphauthuatphu, autoBSphu);
                autoBSphu.Focus();
                autoBSphu.SelectAll();
            }
        }

        void autoBSGayme__OnEnterMe()
        {
            if (autoBSGayme.MyID != "-1")
            {
                AddBacsi(dtbsgayme, grd_bsgm, autoBSGayme);
                autoBSGayme.Focus();
                autoBSGayme.SelectAll();
            }
        }

        void autoBSPhauthuat__OnEnterMe()
        {
            if (autoBSPhauthuat.MyID != "-1")
            {
                AddBacsi(dtbsphauthuat, grd_bspt, autoBSPhauthuat);
                autoBSPhauthuat.Focus();
                autoBSPhauthuat.SelectAll();
            }
        }
        private void AddBacsi(DataTable dtData,GridEX grdList,AutoCompleteTextbox auto)
        {
            try
            {
                EnumerableRowCollection<DataRow> query = from benh in dtData.AsEnumerable()
                                                         where Utility.sDbnull(benh[DmucNhanvien.Columns.MaNhanvien]) == auto.MyCode
                                                         && Utility.sDbnull(benh[DmucNhanvien.Columns.IdNhanvien]) == auto.MyID
                                                         select benh;
                if (!query.Any())
                {
                    DataRow drv = dtData.NewRow();
                    drv[DmucNhanvien.Columns.IdNhanvien] = auto.MyID;
                    drv[DmucNhanvien.Columns.MaNhanvien] = auto.MyCode;
                    drv[DmucNhanvien.Columns.TenNhanvien] = auto.Text;
                    dtData.Rows.Add(drv);
                    dtData.AcceptChanges();
                    grdList.AutoSizeColumns();
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
        //private void AddDungcu(DataTable dtData, GridEX grdList, AutoCompleteTextbox_Danhmucchung auto)
        //{
        //    try
        //    {
        //        EnumerableRowCollection<DataRow> query = from benh in dtData.AsEnumerable()
        //                                                 where Utility.sDbnull(benh[DmucChung.Columns.Ma]) == auto.MyCode
        //                                                 select benh;
        //        if (!query.Any())
        //        {
        //            DataRow drv = dtData.NewRow();
        //            drv[DmucChung.Columns.Ma] = auto.MyCode;
        //            drv[DmucChung.Columns.Ten] = auto.Text;
        //            dtData.Rows.Add(drv);
        //            dtData.AcceptChanges();
        //            grdList.AutoSizeColumns();
        //        }
        //        else
        //        {
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.CatchException(ex);
        //    }
        //    finally
        //    {
        //    }
        //}
        List<string> lstKhongbatnhap = new List<string>();
        private void frm_PhieuPTTT_Load(object sender, EventArgs e)
        {

            lstKhongbatnhap = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_KHONGYC_NHAPBACSI_DIEUDUONG_GAYME", "IUI", true).Split(',').ToList<string>();
            DataTable mDtKhoaNoitru;
            if(noitru=="0")
               mDtKhoaNoitru= THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
            else if (noitru == "0")
                mDtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            else
                mDtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
            autoKhoa.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            autoBSPhauthuat.Init(globalVariables.gv_dtDmucNhanvien,
                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            autoBSGayme.Init(autoBSPhauthuat.AutoCompleteSource, autoBSPhauthuat.defaultItem);
            autoBSphu.Init(autoBSPhauthuat.AutoCompleteSource, autoBSPhauthuat.defaultItem);
            autoDieuduonggayme.Init(autoBSPhauthuat.AutoCompleteSource, autoBSPhauthuat.defaultItem);
            autoDieuduongvongngoai.Init(autoBSPhauthuat.AutoCompleteSource, autoBSPhauthuat.defaultItem);
            autoDieuduongvongtrong.Init(autoBSPhauthuat.AutoCompleteSource, autoBSPhauthuat.defaultItem);
            //AllowTextChanged = false;
            Utility.SetDataSourceForDataGridEx(grd_bspt, dtbsphauthuat, false, true, "", "");
            Utility.SetDataSourceForDataGridEx(grd_bsgm, dtbsgayme, false, true, "", "");
            Utility.SetDataSourceForDataGridEx(grd_bsphauthuatphu, dtbsphauthuatphu, false, true, "", "");

            Utility.SetDataSourceForDataGridEx(grdDieuduonggayme, dtdieuduonggayme, false, true, "", "");
            Utility.SetDataSourceForDataGridEx(grdDieuduongvongngoai, dtDieuduongvongngoai, false, true, "", "");
            Utility.SetDataSourceForDataGridEx(grdDieuduongvongtrong, dtDieuduongvongtrong, false, true, "", "");
            DataTable dtHAPTTT = 
                new Select().From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai).IsEqualTo("PTTT_LUOCDO_HINHANH")
                    .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                    .OrderAsc(DmucChung.Columns.SttHthi)
                    .ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboHinhPTTT, dtHAPTTT, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Chọn hình phẫu thuật", false);
            b_Hasloaded = true;
            //if (m_enAct == action.Update)
            //{
                ucThongtinnguoibenh_doc_v11.Refresh();
            //}
            //else
            //{
            //    ucThongtinnguoibenh_doc_v11.Refresh();
            //}
            ModifyCommands();

        }
        private void frm_PhieuPTTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((ActiveControl != null && (ActiveControl.Name == autoBSPhauthuat.Name || ActiveControl.Name == autoBSGayme.Name || ActiveControl.Name == autoBSphu.Name || ActiveControl.Name == txtTrinhTuPhauThat.Name)))
                    return;
                else
                    SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.Escape)
            {
                cmdExit.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                cmdSave.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.T)
            {
                cmdAddNew.PerformClick();
            }
        }
        #endregion

     
        private void ClearControl()
        {
            objpttt = new KcbPhieupttt();
            autoKhoa.SetId(-1);
            autoBuong.SetId(-1);
            autoGiuong.SetId(-1);
            autoLoaiPTTT.SetDefaultItem();
            autoLydotaibien.SetDefaultItem();
            autoLydobienchung.SetDefaultItem();
            autoLydotuvong.SetDefaultItem();
            txtChanDoanTruocPT.SetDefaultItem();
            txtChanDoanSauPT.SetDefaultItem();
            txtPhuongPhapPT.SetDefaultItem();
            txtPhuongPhapVoCam.SetDefaultItem();
            autoBSGayme.SetId(-1);
            autoBSPhauthuat.SetId(-1);
            autoBSphu.SetId(-1);
            dtbsgayme.Rows.Clear();
            dtbsgayme.AcceptChanges();
            dtbsphauthuat.Rows.Clear();
            dtbsphauthuatphu.Rows.Clear();

            dtdieuduonggayme.Rows.Clear();
            dtDieuduongvongngoai.Rows.Clear();
            dtDieuduongvongtrong.Rows.Clear();
            //grd_bspt.DataSource = null;
            //grd_bsphauthuatphu.DataSource = null;
            //grd_bsgm.DataSource = null;
            txtLuocDoPhauThuat.SetDefaultItem();
            txtBac.Clear();
            txtDanLuu.Clear();
            txtKhac.Clear();
            txtTrinhTuPhauThat._Text = "";
            dtNgayPhauThuat.Value = DateTime.Now;
            dtpNgayRut.Value = DateTime.Now;
            dtpNgayCatChi.Value = DateTime.Now;
            dtpNgayGioKetThucPTTT.Value = DateTime.Now;
            dtNgayGioTuVong.Value = DateTime.Now;
            dtNgayIn.Value = DateTime.Now;
            chkNgayRut.Checked = false;
            chkPTTT_KetThuc.Checked = false;
            chkNgayCatChi.Checked = false;
            chkTaibien.Checked = false;
            chkBienchung.Checked = false;
            chkTuvong.Checked = false;
            dtpNgayRut.Enabled = false;
            dtpNgayCatChi.Enabled = false;
            picPTTT.Image = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool isDoing = false;
        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
          //if(Utility.isValidGrid(grdChiDinh) && Utility.sDbnull(grdChiDinh.GetValue("noi_tru") )=="0" && Utility.sDbnull(grdChiDinh.GetValue("trangthai_thanhtoan")) == "0")
          //  {
          //      Utility.ShowMsg(string.Format("Dịch vụ {0} chưa được thanh toán nên bạn không thể làm phiếu PTTT được. Vui lòng kiểm tra lại", Utility.sDbnull(grdChiDinh.GetValue("ten_chitietdichvu"))));
          //      return;
          //  }    
            cmdAddNew.Enabled = false;
            isDoing = true;
            AllowSeletionChanged = false;
            m_enAct = action.Insert;
            cmdCancel.BringToFront();
            ClearControl();
            autoKhoa.SetId(objLuotkham.IdKhoanoitru);
            autoKhoa.RaiseEnterEvents();
            autoBuong.SetId(objLuotkham.IdBuong);
            autoBuong.RaiseEnterEvents();
            autoGiuong.SetId(objLuotkham.IdGiuong);

            dtNgayPhauThuat.Focus();
            ModifyCommands();
            
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            AllowSeletionChanged = true;
            isDoing = false;
            cmdExit.BringToFront();
            grdPhieuPTTT_SelectionChanged(grdPhieuPTTT, e);
            ModifyCommands();
        }
        bool isValiData()
        {
            if (objpttt != null && objpttt.IdPhieu>0)
            {
                if (globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("kcb_phieupttt_sua") || globalVariables.UserName == objpttt.NguoiTao)
                {
                }
                else
                {
                    Utility.ShowMsg(string.Format("Phiếu PTTT bạn đang chọn do người dùng {0} tạo nên bạn không có quyền sửa. Muốn sửa phiếu của người khác bạn phải là Admin,Super Admin hoặc có quyền (kcb_phieupttt_sua).\nLiên hệ IT Bệnh viện để được hỗ trợ", objpttt.NguoiTao));
                    return false;
                }
            }
            if (!Utility.isValidGrid(grdChiDinh))
            {
                Utility.ShowMsg("Người bệnh cần được chỉ định dịch vụ liên quan đến Phẫu thuật-Thủ thuật để thực hiện tiếp chức năng này. Vui lòng kiểm tra lại");
                return false;
            }
            KcbChidinhclsChitiet objChidinhCLSChitiet = KcbChidinhclsChitiet.FetchByID(IdChitietchidinh);
            if(objChidinhCLSChitiet==null)
            {
                Utility.ShowMsg(string.Format( "Chỉ định {0} đã bị xóa trong lúc bạn đang thực hiện lập phiếu PTTT. Vui lòng kiểm tra lại",Utility.sDbnull( grdChiDinh.GetValue("ten_chitietdichvu"))));
                return false;
            }    
            //if (Utility.isValidGrid(grdChiDinh) && Utility.sDbnull(grdChiDinh.GetValue("noi_tru")) == "0" && Utility.sDbnull(grdChiDinh.GetValue("trangthai_thanhtoan")) == "0")
            //{
            //    Utility.ShowMsg(string.Format("Dịch vụ {0} chưa được thanh toán nên bạn không thể làm phiếu PTTT được. Vui lòng kiểm tra lại", Utility.sDbnull(grdChiDinh.GetValue("ten_chitietdichvu"))));
            //    return false;
            //}
            if (radNoiTru.Checked)
            {
                if (autoKhoa.MyID != "-1" || autoBuong.MyID != "-1" || autoGiuong.MyID != "-1")
                {
                    var q = from p in dtbuonggiuong.AsEnumerable()
                            where Utility.sDbnull(p[NoitruPhanbuonggiuong.Columns.IdKhoanoitru], "-1") == Utility.sDbnull(autoKhoa.MyID, "-1")
                            && Utility.sDbnull(p[NoitruPhanbuonggiuong.Columns.IdBuong], "-1") == Utility.sDbnull(autoBuong.MyID, "-1")
                              && Utility.sDbnull(p[NoitruPhanbuonggiuong.Columns.IdGiuong], "-1") == Utility.sDbnull(autoGiuong.MyID, "-1")
                            select p;
                    if (!q.Any())
                    {
                        Utility.ShowMsg(string.Format("Người bệnh {0} chưa từng được nằm ở khoa {1} - Buồng {2} - Giường {3}. Vui lòng kiểm tra lại thông tin.\nNhấn OK để hệ thống hiển thị thông tin buồng giường của người bệnh", ucThongtinnguoibenh_doc_v11.txtTenBN.Text, autoKhoa.Text, autoBuong.Text, autoGiuong.Text));
                        autoBuong.Focus();
                        return false;
                    }
                }
            }
            if (chkPTTT_KetThuc.Checked)
            {
                if (dtpNgayGioKetThucPTTT.Value < dtNgayPhauThuat.Value)
                {
                    Utility.ShowMsg("Thời gian kết thúc phẫu thuật phải >= thời gian bắt đầu phẫu thuật");
                    dtpNgayGioKetThucPTTT.Focus();
                    return false;
                }
            }
            if (autoLoaiPTTT.myCode == "-1")
            {
                Utility.ShowMsg("Bạn cần chọn loại phẫu thuật thủ thuật");
                autoLoaiPTTT.Focus();
                return false;
            }
            if (Utility.sDbnull( txtPhuongPhapPT.Text).Length<=0)
            {
                Utility.ShowMsg("Bạn cần chọn phương pháp phẫu thuật");
                txtPhuongPhapPT.Focus();
                return false;
            }
            if (Utility.sDbnull(txtPhuongPhapVoCam.Text).Length <= 0 )
            {
                Utility.ShowMsg("Bạn cần chọn phương pháp vô cảm");
                txtPhuongPhapVoCam.Focus();
                return false;
            }

            //if (Utility.DoTrim( txtLuocDoPhauThuat.Text).Length<=0)
            //{
            //    Utility.ShowMsg("Bạn cần chọn lược đồ PTTT");
            //    txtLuocDoPhauThuat.Focus();
            //    return false;
            //}
            if (dtbsphauthuat.Rows.Count <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một bác sĩ phẫu thuật thủ thuật chính");
                autoBSPhauthuat.Focus();
                return false;
            }
           
            if (!lstKhongbatnhap.Contains(ma_dichvu))
            {
                if (dtbsgayme.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một bác sĩ gây mê");
                    autoBSGayme.Focus();
                    return false;
                }
                //if (dtbsphauthuatphu.Rows.Count <= 0)
                //{
                //    Utility.ShowMsg("Bạn cần chọn ít nhất một bác sĩ PTTT phụ");
                //    autoBSphu.Focus();
                //    return false;
                //}
                if (dtdieuduonggayme.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một điều dưỡng gây mê");
                    autoDieuduonggayme.Focus();
                    return false;
                }
                if (dtDieuduongvongngoai.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một điều dưỡng vòng ngoài");
                    autoDieuduongvongngoai.Focus();
                    return false;
                }
                if (dtDieuduongvongtrong.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một điều dưỡng vòng trong");
                    autoDieuduongvongtrong.Focus();
                    return false;
                }
            }

            //if(objLuotkham.TrangthaiNoitru>0 && radNgoaiTru.Checked)
            //    if (!Utility.AcceptQuestion(string.Format("Trạng thái người bệnh đang: {0} trong khi bạn chọn: {1}. Bạn có chắc chắn?", objLuotkham.TrangthaiNoitru>0?"Nội trú":"Ngoại trú","Ngoại trú"), "Cảnh báo", true))
            //    {
            //        radNgoaiTru.Focus();
            //        return false;
            //    }
            return true;
        }
        private string Laysophieu()
        {
            string ma_phieu = "";
            StoredProcedure sp = SPs.SpGetMaphieuPttt(DateTime.Now.Year, ma_phieu);
            sp.Execute();
            return Utility.sDbnull(sp.OutputValues[0], "-1");
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                UIAction.SetTextStatus(lblStatus, "", false);
                if (isValiData() == false) return;
                //if (MessageBox.Show("Bạn chắc chắn muốn lưu phiếu phẫu thuật thủ thuật?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                if (objpttt == null) objpttt = new KcbPhieupttt();
                if (objpttt.IdPhieu <= 0)
                {
                    objpttt = new KcbPhieupttt();
                    objpttt.IsNew = true;
                    objpttt.NgayTao = DateTime.Now;
                    objpttt.NguoiTao = globalVariables.UserName;
                    objpttt.MaPhieu = Laysophieu();

                }
                else
                {
                    objpttt.MarkOld();
                    objpttt.IsNew = false;
                    objpttt.NgaySua = DateTime.Now;
                    objpttt.NguoiSua = globalVariables.UserName;
                }
                objpttt.IdBenhnhan = objLuotkham.IdBenhnhan;
                objpttt.MaLuotkham = objLuotkham.MaLuotkham;
                objpttt.IdKhoadieutri = Utility.Int16Dbnull(autoKhoa.MyID);
                objpttt.IdBuong = Utility.Int16Dbnull(autoBuong.MyID);
                objpttt.IdGiuong = Utility.Int16Dbnull(autoGiuong.MyID);
                objpttt.NgayPttt = dtNgayPhauThuat.Value;
                if (chkPTTT_KetThuc.Checked)
                    objpttt.NgayKetthuc = dtpNgayGioKetThucPTTT.Value;
                else
                    objpttt.NgayKetthuc = null;
                objpttt.IdbacsiGayme = getBacsithamgia(dtbsgayme);
                objpttt.IdbacsiPttt = getBacsithamgia(dtbsphauthuat);
                objpttt.IdbacsiPtttPhu = getBacsithamgia(dtbsphauthuatphu);
                objpttt.IdDieuduonggayme = getBacsithamgia(dtdieuduonggayme);
                objpttt.MaHinhanh = cboHinhPTTT.SelectedIndex.ToString();
                objpttt.MaDungcuvongngoai = getBacsithamgia(dtDieuduongvongngoai);
                objpttt.MaDungcuvongtrong = getBacsithamgia(dtDieuduongvongtrong);
                objpttt.TenDungcuvongngoai = getFieldValue(dtDieuduongvongngoai, "ten_nhanvien");
                objpttt.TenDungcuvongtrong = getFieldValue(dtDieuduongvongtrong, "ten_nhanvien");
                objpttt.LoaiPttt = autoLoaiPTTT.myCode;
                objpttt.TruocPttt = txtChanDoanTruocPT.Text;
                objpttt.SauPttt = txtChanDoanSauPT.Text;
                objpttt.PhuongphapPttt = txtPhuongPhapPT.Text;
                objpttt.PhuongphapVocam = txtPhuongPhapVoCam.Text;
                objpttt.LuocdoPttt = txtLuocDoPhauThuat.Text;
                objpttt.DanLuu = txtDanLuu.Text;
                objpttt.Bac = txtBac.Text;
                if (chkNgayRut.Checked)
                    objpttt.NgayRut = dtpNgayRut.Value;
                else
                    objpttt.NgayRut = null;
                if (chkNgayCatChi.Checked)
                    objpttt.NgayCatchi = dtpNgayCatChi.Value;
                else
                    objpttt.NgayCatchi = null;
                objpttt.Khac = txtKhac.Text;
                objpttt.TrinhtuPttt = txtTrinhTuPhauThat.Text;
                objpttt.TaiBien = chkTaibien.Checked;
                objpttt.BienChung = chkBienchung.Checked;
                objpttt.TuVong = chkTuvong.Checked;
                if (chkTuvong.Checked) objpttt.NgayTuvong = dtNgayGioTuVong.Value;
                else
                    objpttt.NgayTuvong = null;
                objpttt.LydoTuvong = autoLydotuvong.myCode;
                objpttt.LydoTaibien = autoLydotaibien.myCode;
                objpttt.LydoBienchung = autoLydobienchung.myCode;
                objpttt.Noitru = radNoiTru.Checked;
                objpttt.IdChitietchidinh = IdChitietchidinh;
                objpttt.Save();
                txtIdPhieuPTTT.Text = objpttt.IdPhieu.ToString();

                if (m_enAct == action.Insert)
                {
                    DataRow newRow = dtPttt.NewRow();
                    Utility.FromObjectToDatarow(objpttt, ref newRow);
                    newRow["ten_phuongphap_vocam"] = txtPhuongPhapVoCam.Text;
                    newRow["ten_phuongphap_pttt"] = txtPhuongPhapPT.Text;
                    newRow["ten_khoaphong"] = autoKhoa.Text;
                    newRow["ten_buong"] = autoBuong.Text;
                    newRow["ten_giuong"] = autoGiuong.Text;
                    newRow["ten_loaipttt"] = autoLoaiPTTT.Text;
                    dtPttt.Rows.Add(newRow);
                    dtPttt.AcceptChanges();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới phiếu PTTT cho bệnh nhân: {0}-{1} thành công", objpttt.MaLuotkham, ucThongtinnguoibenh_doc_v11.txtTenBN.Text), objpttt.IsNew ? newaction.Insert : newaction.Update, "UI");
                    UIAction.SetTextStatus(lblStatus, "Đã Thêm mới phiếu PTTT thành công.", false);
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật phiếu PTTT cho bệnh nhân: {0}-{1} thành công", objpttt.MaLuotkham, ucThongtinnguoibenh_doc_v11.txtTenBN.Text), objpttt.IsNew ? newaction.Insert : newaction.Update, "UI");
                   UIAction.SetTextStatus(lblStatus, "Đã cập nhật phiếu PTTT thành công.",false);
                    m_enAct = action.Update;
                }
                cmdExit.BringToFront();
                cmdCancel.PerformClick();
                AllowSeletionChanged = true;
                grdPhieuPTTT_SelectionChanged(grdPhieuPTTT, e);
                ModifyCommands();
                if (chkCloseAfterSave.Checked) this.Close();
            }
            catch (Exception ex)
            {

                Utility.CatchException("Lỗi khi lưu phiếu PTTT ",ex);
            }
            finally
            {
               
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("noitru_phieupttt_xoa") || globalVariables.UserName == grdPhieuPTTT.GetValue("nguoi_tao"))
                {
                }
                else
                {
                    Utility.thongbaokhongcoquyen("noitru_phieupttt_xoa", "xóa phiếu phẫu thuật thủ thuật");
                    return;
                }
                if (objpttt == null || objpttt.IdPhieu <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn một phiếu PTTT trên danh sách để xóa. Vui lòng kiểm tra lại");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin phiếu PTTT đang chọn không ?", "Thông báo", true))
                {
                    int banghi = new Delete().From<KcbPhieupttt>()
                         .Where(KcbPhieupttt.Columns.IdPhieu)
                         .IsEqualTo(Utility.Int32Dbnull(objpttt.IdPhieu))
                         .Execute();
                    if (banghi > 0)
                    {
                        Utility.ShowMsg("Bạn xóa thông tin phiếu PTTT thành công", "Thông báo");
                        DataRow dr = ((DataRowView)grdPhieuPTTT.CurrentRow.DataRow).Row;
                        dtPttt.Rows.Remove(dr);
                        dtPttt.AcceptChanges();
                        grdPhieuPTTT_SelectionChanged(grdPhieuPTTT, e);

                    }

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
            
        }
          void CreateMergeFields(DataTable dt)
        {
            try
            {
                string fields="";
                string values = "";
                foreach (DataColumn col in dt.Columns)
                {
                    fields += col.ColumnName + ",";
                    values += col.ColumnName + "_Value,";
                }
                if (fields.Length > 0) fields = fields.Substring(0, fields.Length - 1);
                if (values.Length > 0) values = values.Substring(0, values.Length - 1);
                string fileName=string.Format(@"{0}\{1}\{2}.txt",Application.StartupPath,"MergeFields",dt.TableName);
                using (StreamWriter _Writer = new StreamWriter(fileName))
                {
                    _Writer.WriteLine(fields);
                    _Writer.WriteLine(values);
                    _Writer.Flush();
                    _Writer.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
          }
          private void cmdPrint_Click(object sender, EventArgs e)
          {
              ctxInphieu.Show(cmdPrint, new Point(0, cmdPrint.Height));
          }

        private void chkNgayRut_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgayRut.Enabled = chkNgayRut.Checked;
        }

        private void chkNgayCatChi_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgayCatChi.Enabled = chkNgayCatChi.Checked;
        }

        

        private void chkTuvong_CheckedChanged(object sender, EventArgs e)
        {
            autoLydotuvong.Enabled = dtNgayGioTuVong.Enabled = chkTuvong.Checked;
        }

        private void mnuInphieuthuthuat_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.KcbPtttInphieu(Utility.Int64Dbnull(grdPhieuPTTT.GetValue("id_phieu"))).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU", "GIẤY CHỨNG NHẬN PHẪU THUẬT-THỦ THUẬT", true).Split('@').ToList<string>();
                string ma_loaidvu = Utility.sDbnull(grdChiDinh.GetValue("ma_dichvu"), "PTTT");
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_CHUNGNHAN_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEU_PTTT_NOITRU", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PhieuPTTT", objLuotkham.MaLuotkham, Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));

               
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
                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                         byte[] myimage=null;

                         if (objpttt != null && objpttt.MaHinhanh != null)
                         {
                             if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                             {
                                 myimage = null;
                             }
                             else //if (objpttt.MaHinhanh == "1")
                             {
                                 myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                             }
                             //else if (objpttt.MaHinhanh == "2")
                             //{
                             //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                             //}
                             //else if (objpttt.MaHinhanh == "3")
                             //{
                             //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                             //}

                         }
                         if (myimage!=null)
                             builder.InsertImage(myimage);
                         else
                             builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }

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

        private void mnuInphieuphauthuat_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtData = SPs.KcbPtttInphieu(Utility.Int64Dbnull(grdPhieuPTTT.GetValue("id_phieu"))).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                THU_VIEN_CHUNG.CreateXML(dtData, "PHIEU_CHUNGNHAN_PTTT.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                noitru_inphieu.InPhieuChungNhanPTTT(dtData, DateTime.Now, chkPreview.Checked, "CRPT_PHIEU_CHUNGNHAN_PT");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdScanFinger_Click(object sender, EventArgs e)
        {
            RegisterFinger();
        }
        internal static IntPtr hWnd;
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern void SendMessageW(IntPtr hWnd, uint msg, uint wParam, uint lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowW(string className, string windowName);
        internal static Process process;

        void RegisterFinger()
        {
            try
            {

                string patientID = "-1";
                if (Utility.Int32Dbnull(patientID, -1) > 0)
                {
                    List<string> _list = new List<string>();
                    _list.Add(patientID.ToString());
                    _list.Add(0.ToString());
                    string sPatientInforFile = Application.StartupPath + @"\IVF_FR\PatientInfor.txt";
                    string appName = Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe";
                    if (File.Exists(sPatientInforFile))
                    {
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    else
                    {
                        File.CreateText(sPatientInforFile);
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    Utility.KillProcess(appName);
                    Thread.Sleep(100);
                    process = Process.Start(Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe");
                    if (process != null) process.WaitForExit();
                    WaitForSingleObject(process.Handle, 0xffffffff);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void mnuInphieuPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.KcbPtttInphieu(Utility.Int64Dbnull(grdPhieuPTTT.GetValue("id_phieu"))).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_PTTT_NOITRU.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEU_PTTT_NOITRU", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PhieuPTTT", objLuotkham.MaLuotkham, Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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

                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;

                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }
                            //else if (objpttt.MaHinhanh == "2")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                            //}
                            //else if (objpttt.MaHinhanh == "3")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                            //}

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
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

        private void mnuCamket_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = SPs.KcbPtttInphieu(Utility.Int64Dbnull(grdPhieuPTTT.GetValue("id_phieu"))).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU_CAMKET", "GIẤY CAM ĐOAN CHẤP NHẬN PHẪU THUẬT, THỦ THUẬT VÀ GÂY MÊ HỒI SỨC@GIẤY CAM ĐOAN CHẤP NHẬN PHẪU THUẬT, THỦ THUẬT VÀ GÂY MÊ HỒI SỨC", true).Split('@').ToList<string>();
                string ma_loaidvu = Utility.sDbnull(grdChiDinh.GetValue("ma_dichvu"), "PTTT");
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_CAMKET_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEU_PTTT_NOITRU", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PhieuPTTT", objLuotkham.MaLuotkham, Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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

                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;

                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }
                            //else if (objpttt.MaHinhanh == "2")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                            //}
                            //else if (objpttt.MaHinhanh == "3")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                            //}

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
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

        private void cboHinhPTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!b_Hasloaded) return;
                if (picPTTT.Image != null)
                {
                    picPTTT.Image.Dispose();
                    picPTTT.Image = null;
                }

                if (cboHinhPTTT.SelectedIndex == 0)
                {
                }
                else //if (cboHinhPTTT.SelectedIndex == 1)
                {

                    picPTTT.Image = Image.FromFile(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\{0}.png", cboHinhPTTT.SelectedValue.ToString()));
                }
                //else if (cboHinhPTTT.SelectedIndex == 2)
                //{
                //    picPTTT.Image = Image.FromFile(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                //}
                //else if (cboHinhPTTT.SelectedIndex == 3)
                //{
                //    picPTTT.Image = Image.FromFile(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                //}
                if (objpttt != null && objpttt.IdPhieu>0)
                {
                    objpttt.MaHinhanh = cboHinhPTTT.SelectedIndex.ToString();
                    objpttt.IsNew = false;
                    objpttt.MarkOld();
                    objpttt.Save();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex); 
            }
        }

        private void mnuPhieutuongtrinhPTTT_Click(object sender, EventArgs e)
        {
            try
            {

                long ID_PHIEUPTTT = Utility.Int64Dbnull(grdPhieuPTTT.GetValue("id_phieu"));
                KcbPhieupttt objpttt = KcbPhieupttt.FetchByID(ID_PHIEUPTTT);
                DataTable dtData = SPs.KcbPtttInphieu(ID_PHIEUPTTT).GetDataSet().Tables[0];
                dtData.TableName = "kcb_phieu_pttt";
                Utility.AddColums2DataTable(ref dtData, new List<string>() { "thogian_vaovien", "thoigian_batdau_phauthuat", "thoigian_ketthuc_phauthuat" }, typeof(string));
                List<string> lst_ten_phieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("PTTT_TENPHIEU_TUONGTRINH", "PHIẾU TƯỜNG TRÌNH PHẪU THUẬT@PHIẾU TƯỜNG TRÌNH THỦ THUẬT", true).Split('@').ToList<string>();
                string ma_loaidvu = Utility.sDbnull(grdChiDinh.GetValue("ma_dichvu"), "PTTT");
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = ma_loaidvu == "PTTT" ? lst_ten_phieu[0] : (ma_loaidvu == "PHAUTHUAT" ? lst_ten_phieu[1] : lst_ten_phieu[2]);
                drData["thogian_vaovien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien,"");
                drData["thoigian_batdau_phauthuat"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objpttt.NgayPttt, "Từ");
                drData["thoigian_ketthuc_phauthuat"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objpttt.NgayKetthuc, "Đến");
                List<string> fieldNames = new List<string>();
               

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEU_TUONGTRINH_PTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                 CreateMergeFields(dtData);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEU_TUONGTRINH_PTTT", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PhieuPTTT", objLuotkham.MaLuotkham, Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));

                int w = 100;
                int h = 100;
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
                             w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                             h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    byte[] NoImage = Utility.fromimagepath2byte(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png");
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("PTTTsize").ExecuteSingle<SysSystemParameter>();
                    if (builder.MoveToMergeField("anh1"))
                    {
                        byte[] myimage = null;
                        w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                        h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                        if (objpttt != null && objpttt.MaHinhanh != null)
                        {
                            if (objpttt.MaHinhanh == "0" || objpttt.MaHinhanh == null)
                            {
                                myimage = null;
                            }
                            else //if (objpttt.MaHinhanh == "1")
                            {
                                myimage = Utility.fromimagepath2byte(Application.StartupPath + string.Format(@"\Hinhanh_PTTT\pttt0{0}.png", objpttt.MaHinhanh));
                            }
                            //else if (objpttt.MaHinhanh == "2")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt02.png");
                            //}
                            //else if (objpttt.MaHinhanh == "3")
                            //{
                            //    myimage = Utility.fromimagepath2byte(Application.StartupPath + @"\Hinhanh_PTTT\pttt03.png");
                            //}

                        }
                        if (myimage != null)
                            builder.InsertImage(myimage,w,h);
                        else
                            builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                    }
                    else
                    {
                        if (builder.MoveToMergeField("anh1"))
                            builder.InsertImage(NoImage, 10, 10);
                    }
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

        private void grpThongTin_Enter(object sender, EventArgs e)
        {

        }
     
    }
}
