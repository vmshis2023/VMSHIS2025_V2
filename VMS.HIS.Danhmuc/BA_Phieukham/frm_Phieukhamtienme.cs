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
namespace VMS.EMR.PHIEUKHAM
{
    public partial class frm_Phieukhamtienme : Form
    {
        #region Variables
        private DataTable m_bacsi;
        private DataTable m_DsChiDinh = new DataTable();
        //private DataTable m_DsChiDinh_chitiet = new DataTable();
        private DataTable m_loaipt = new DataTable();
        private DataTable m_phieupttt = new DataTable();
        private DataTable m_phieupttt_chitiet = new DataTable();
        private bool b_Hasloaded = false;
        private string _rowFilter = "1=1";
        private bool AllowSeletionChanged = false;
        private string malakham = "";
        public KcbLuotkham objLuotkham;
        public KcbDanhsachBenhnhan objBenhnhan;
        private long ID_PHIEUPTTT;
        public action m_enAct = action.Insert;
        public bool b_CallParent = false;
        public int _assignDetailid = -1;
        public int _nPatient_ID = -1;
        public KcbPhieukhamTienme objPkTm = new KcbPhieukhamTienme();
        
        #endregion

        #region Form events
        public frm_Phieukhamtienme()
        {
            InitializeComponent();
            Shown += frm_Phieukhamtienme_Shown;
            FormClosing += frm_Phieukhamtienme_FormClosing;
            Utility.SetVisualStyle(this);
            autoBSGayme._OnEnterMe += autoBSGayme__OnEnterMe;
            txtPhuongPhapVoCam._OnShowDataV1 += __OnShowDataV1;
            ucThongtinnguoibenh_doc_v11._OnEnterMe+=ucThongtinnguoibenh_doc_v11__OnEnterMe;
            grdChiDinh.CurrentCellChanged += grdChiDinh_CurrentCellChanged;
            chkDiung_thucan.CheckedChanged += chkDiung_thucan_CheckedChanged;
            chkDiung_thuoc.CheckedChanged += chkDiung_thuoc_CheckedChanged;
            dtNgaykham.Value = DateTime.Now;
        }

        void grdChiDinh_CurrentCellChanged(object sender, EventArgs e)
        {
            ChonChidinh();
        }

        void chkDiung_thuoc_CheckedChanged(object sender, EventArgs e)
        {
            txtThuoc.Enabled = chkDiung_thuoc.Checked;
        }

        void chkDiung_thucan_CheckedChanged(object sender, EventArgs e)
        {
            txtThucan.Enabled = chkDiung_thucan.Checked;
        }

       
        void grdChiDinh_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (!Utility.isValidGrid(grdChiDinh)) return;
            //cmdAddNew.PerformClick();
        }

      

        void frm_Phieukhamtienme_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SaveUserConfigs();
        }
        //void SaveUserConfigs()
        //{
        //    try
        //    {
        //        Utility.SaveUserConfig(chkHoitruockhixoa.Tag.ToString(), Utility.Bool2byte(chkHoitruockhixoa.Checked));
        //        Utility.SaveUserConfig(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked));
        //    }
        //    catch (Exception ex)
        //    {

        //        Utility.CatchException(ex);
        //    }
        //}
        //void LoadUserConfigs()
        //{
        //    try
        //    {
        //        chkHoitruockhixoa.Checked = Utility.getUserConfigValue(chkHoitruockhixoa.Tag.ToString(), Utility.Bool2byte(chkHoitruockhixoa.Checked)) == 1;
        //        chkPreview.Checked = Utility.getUserConfigValue(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked)) == 1;
        //    }
        //    catch (Exception ex)
        //    {

        //        Utility.CatchException(ex);
        //    }
        //}
        
        void frm_Phieukhamtienme_Shown(object sender, EventArgs e)
        {
            //LoadUserConfigs();
        }

       
        void ModifyCommands()
        {
            cmdPrint.Enabled = cmdDelete.Enabled = Utility.isValidGrid(grdChiDinh) && objLuotkham != null && objPkTm!=null && objPkTm.Id>0;
            cmdSave.Enabled = !cmdStart.Enabled && grdChiDinh.RowCount > 0;
            if (grdChiDinh.RowCount <= 0) ClearControl();
        }
        DataTable dtPttt = new DataTable();
        long IdChitietchidinh = -1;
        void ChonChidinh()
        {
            try
            {
                if (!Utility.isValidGrid(grdChiDinh) || !AllowSeletionChanged)
                {
                    ClearControl();
                }
                else
                    FillData4Update();
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
        
        DataTable dtbuonggiuong = new DataTable();
        void ucThongtinnguoibenh_doc_v11__OnEnterMe()
        {
            if (ucThongtinnguoibenh_doc_v11.objLuotkham != null)
            {
               
                objLuotkham = ucThongtinnguoibenh_doc_v11.objLuotkham;
                GetCls();
                
            }
        }
        void GetCls()
        {
            try
            {
                AllowSeletionChanged = false;
                DataTable dtCls = SPs.KcbPtttTimkiemchidinhPttt(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, 100).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChiDinh, dtCls, true, true, "1=1", "");
                AllowSeletionChanged = true;
                grdChiDinh_CurrentCellChanged(grdChiDinh, new EventArgs());
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
       
        void FillData4Update()
        {
            try
            {
                IdChitietchidinh = Utility.Int64Dbnull(grdChiDinh.GetValue("id_chitietchidinh"));
                objPkTm = new Select().From(KcbPhieukhamTienme.Schema).Where(KcbPhieukhamTienme.Columns.IdChitietchidinh).IsEqualTo(IdChitietchidinh).ExecuteSingle<KcbPhieukhamTienme>();
                if (objPkTm != null && objPkTm.Id > 0)
                {
                    m_enAct = action.Update;
                    objPkTm.ChanDoan = "";
                    txttiensu_ngoaigayme.Text = objPkTm.TiensuNgoaigayme;
                    chkDiung_chuaghinhan.Checked = objPkTm.DiUng.Split(',')[0] == "1";
                    chkDiung_thuoc.Checked = objPkTm.DiUng.Split(',')[1] == "1";
                    chkDiung_thucan.Checked = objPkTm.DiUng.Split(',')[2] == "1";
                    txtThucan.Text = objPkTm.DiungThucan;
                    txtThuoc.Text = objPkTm.DiungThuoc;
                    txtThucan.Enabled = chkDiung_thucan.Checked;
                    txtThuoc.Enabled = chkDiung_thuoc.Checked;
                    optRuoubia_khong.Checked = objPkTm.ThoiquenRuoubia.Value == 0;
                    optRuoubia_nghien.Checked = objPkTm.ThoiquenRuoubia.Value == 1;
                    optRuoubia_thinhthoang.Checked = objPkTm.ThoiquenRuoubia.Value == 2;
                    txtThuocla.Text = Utility.sDbnull(objPkTm.ThoiquenThuocla, "");
                    optlamdungcgn_co.Checked = Utility.Byte2Bool(objPkTm.ThoiquenChatgaynghien);
                    optlamdungcgn_khong.Checked = !optlamdungcgn_co.Checked;
                    List<string> lstTSNK = objPkTm.TiensuNoikhoa.Split(',').ToList<string>();
                    chktsnoikhoa_tangha.Checked = lstTSNK[0] == "1";
                    chktsnoikhoa_benhtimtmcb.Checked = lstTSNK[1] == "1";
                    chktsnoikhoa_daithaoduong.Checked = lstTSNK[2] == "1";
                    chktsnoikhoa_buougiap.Checked = lstTSNK[3] == "1";
                    chktsnoikhoa_copd.Checked = lstTSNK[4] == "1";
                    chktsnoikhoa_covid19.Checked = lstTSNK[5] == "1";
                    chktsnoikhoa_roiloannhiptim.Checked = lstTSNK[6] == "1";
                    chktsnoikhoa_vantim.Checked = lstTSNK[7] == "1";
                    chktsnoikhoa_laophoi.Checked = lstTSNK[8] == "1";
                    chktsnoikhoa_henphequan.Checked = lstTSNK[9] == "1";
                    chktsnoikhoa_chuaghinhan.Checked = lstTSNK[10] == "1";
                    autoBSGayme.SetId(objPkTm.IdBsigayme);
                    dtNgaykham.Value = objPkTm.NgayKham;
                    txtTinhtrangdieutri.Text = objPkTm.TinhtrangDieutri;
                    txttiensunoikhoa_khac.Text = objPkTm.TiensuNoikhoakhac;
                    objPkTm.TiensuNoikhoatruyenmau = Utility.ByteDbnull(opttsnoikhoa_truyenmauco.Checked ? 1 : (opttsnoikhoa_truyenmaukhong.Checked ? 0 : 2), 0);
                    opttsnoikhoa_tsgdgmhs_co.Checked = Utility.Byte2Bool(objPkTm.TiensuGiadinhGmhs);
                    // opttsnoikhoa_tsgdgmhs_khong.Checked=
                    txtkhamGMHS_tacdungchung.Text = objPkTm.KhamTacdungchung;
                    txtkhamGMHS_mach.Text = objPkTm.KhamMach;
                    txtkhamGMHS_HA.Text = objPkTm.KhamHa;
                    txtkhamGMHS_nhietdo.Text = objPkTm.KhamNhietdo;
                    txtkhamGMHS_nhiptho.Text = objPkTm.KhamNhiptho;
                    txtkhamNhinan.Text = objPkTm.KhamNhinan;

                    optGMHS_momieng_co.Checked = Utility.Byte2Bool(objPkTm.DanhgiaduongthoMomieng);
                    optGMHS_camgiap_co.Checked = Utility.Byte2Bool(objPkTm.DanhgiaduongthoKhoangcachcamgiap);
                    optGMHS_tonthuongrang_co.Checked = Utility.Byte2Bool(objPkTm.DanhgiaduongthoNguycotonthuongrang);
                    optGMHS_gapnguaco_co.Checked = Utility.Byte2Bool(objPkTm.DanhgiaduongthoGapnguaco);
                    cboMalampati.Text = Utility.sDbnull(objPkTm.DanhgiaduongthoMalampati);
                    optGMHS_duongtruyentinhmach_co.Checked = Utility.Byte2Bool(objPkTm.DanhgiaduongthoDuongtruyentinhmachngoaibien);
                    txtGMHS_khac.Text = objPkTm.DanhgiaduongthoKhac;

                    txtHTM_gangsuc.Text = objPkTm.HetimmachGangsuc;
                    txtHTM_nhiptho.Text = objPkTm.HetimmachNhiptim;
                    optHTM_nhiptho_deu.Checked = Utility.Byte2Bool(objPkTm.HetimmachNhiptimdeu);
                    optHTM_amthoi_co.Checked = Utility.Byte2Bool(objPkTm.HetimmachAmthoi);
                    txtHTM_khac.Text = objPkTm.HetimmachKhac;

                    optHehohap_khotho_co.Checked = Utility.Byte2Bool(objPkTm.HehohapKhotho);
                    optHehohap_ran_co.Checked = Utility.Byte2Bool(objPkTm.HehohapRan);
                    txtHehohap_khac.Text = objPkTm.HehohapKhac;

                    txtcquan_thanhkinhxuongkhop.Text = objPkTm.CquanThankinhCoxuongkhop;
                    txtcquan_tieuhoa.Text = objPkTm.CquanTieuhoa;
                    txtcquan_khac.Text = objPkTm.CquanKhac;
                    chk_cquan_ghinhanbatthuong.Checked = Utility.Byte2Bool(objPkTm.CquanChuaghinhanbatthuong);

                    txtHTM_denghithem.Text = objPkTm.DenghiThem;

                    optdanhgianguyco_duongtho_co.Checked = Utility.Byte2Bool(objPkTm.DanhgianguycoDuongtho);
                    optdanhgianguyco_daday_co.Checked = Utility.Byte2Bool(objPkTm.DanhgianguycoDadayday);
                    optdanhgianguyco_mucdo_nhe.Checked = objPkTm.DanhgianguycoMucdosaumo.Value == 0;
                    optdanhgianguyco_mucdo_trungbinh.Checked = objPkTm.DanhgianguycoMucdosaumo.Value == 1;
                    optdanhgianguyco_mucdonang.Checked = objPkTm.DanhgianguycoMucdosaumo.Value == 2;
                    optdanhgianguyco_matmau_co.Checked = Utility.Byte2Bool(objPkTm.DanhgianguycoMatmau);
                    optchuongtrinh.Checked = Utility.Byte2Bool(objPkTm.ChuongtrinhCapcuu);
                    optASA1.Checked = objPkTm.PhandoAsa.Value == 1;
                    optASA2.Checked = objPkTm.PhandoAsa.Value == 2;
                    optASA3.Checked = objPkTm.PhandoAsa.Value == 3;
                    optASA4.Checked = objPkTm.PhandoAsa.Value == 4;
                    optASA5.Checked = objPkTm.PhandoAsa.Value == 5;
                    txtPhuongPhapVoCam._Text = objPkTm.PpvocamDukien;
                    txtGiamdaudukien.Text = objPkTm.GiamdauDukien;
                    txtChuanbichuyenbiet.Text = objPkTm.ChuanbiChuyenbiet;
                }
                else
                    ClearControl();

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

        
        void autoBSGayme__OnEnterMe()
        {
            
        }

        
      
        private void frm_Phieukhamtienme_Load(object sender, EventArgs e)
        {

            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() {  txtPhuongPhapVoCam.LOAI_DANHMUC
                 }, true);
            txtPhuongPhapVoCam.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPhuongPhapVoCam.LOAI_DANHMUC));

            autoBSGayme.Init(globalVariables.gv_dtDmucNhanvien,
                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            if(objLuotkham!=null)
            {
                ucThongtinnguoibenh_doc_v11.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                ucThongtinnguoibenh_doc_v11.Refresh();
            }
            ModifyCommands();

        }
        private void frm_Phieukhamtienme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if ((ActiveControl != null && (ActiveControl.Name == txttiensu_ngoaigayme.Name || ActiveControl.Name == txtTinhtrangdieutri.Name || ActiveControl.Name == txtkhamNhinan.Name )))
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
            }
        }
        #endregion

     
        private void ClearControl()
        {
            m_enAct = action.Insert;
            objPkTm = new KcbPhieukhamTienme();
            txtPhuongPhapVoCam.SetDefaultItem();
            autoBSGayme.SetId(-1);
            txttiensu_ngoaigayme.Clear();
            chkDiung_chuaghinhan.Checked = false;
            chkDiung_thuoc.Checked = false;
            chkDiung_thucan.Checked = false;
            txtThuoc.Clear();
            txtThucan.Clear();
            txtThuoc.Enabled = chkDiung_thuoc.Checked;
            txtThucan.Enabled = chkDiung_thucan.Checked;
            optRuoubia_khong.Checked = true;
            optRuoubia_nghien.Checked = false;
            optRuoubia_thinhthoang.Checked = false;
            txtThuocla.Clear();
            optlamdungcgn_co.Checked  = false;
            optlamdungcgn_khong.Checked = true;
            chktsnoikhoa_tangha.Checked = false;
            chktsnoikhoa_benhtimtmcb.Checked = false;
            chktsnoikhoa_daithaoduong.Checked = false;
            chktsnoikhoa_buougiap.Checked = false;
            chktsnoikhoa_copd.Checked = false;
            chktsnoikhoa_covid19.Checked = false;
            chktsnoikhoa_roiloannhiptim.Checked = false;
            chktsnoikhoa_vantim.Checked = false;
            chktsnoikhoa_laophoi.Checked = false;
            chktsnoikhoa_henphequan.Checked = false;
            chktsnoikhoa_chuaghinhan.Checked = false;


            txtTinhtrangdieutri.Clear();
            txttiensunoikhoa_khac.Clear();
            opttsnoikhoa_truyenmauco.Checked =false;
            opttsnoikhoa_truyenmaukhong.Checked=true;
            opttsnoikhoa_truyenmau_taibien.Checked=false;
            opttsnoikhoa_tsgdgmhs_co.Checked = false;
            opttsnoikhoa_tsgdgmhs_khong.Checked = true;
            
            txtkhamGMHS_tacdungchung.Clear();
            txtkhamGMHS_mach.Clear();
            txtkhamGMHS_HA.Clear();
            txtkhamGMHS_nhietdo.Clear();
            txtkhamGMHS_nhiptho.Clear();
            txtkhamNhinan.Clear();

            optGMHS_momieng_co.Checked = false;
            optGMHS_momieng_khong.Checked = true;
            optGMHS_camgiap_co.Checked = false;
            optGMHS_camgiap_khong.Checked = true;
            optGMHS_tonthuongrang_co.Checked = false;
            optGMHS_tonthuongrang_khong.Checked = true;
            optGMHS_gapnguaco_co.Checked = false;
            optGMHS_gapnguaco_khong.Checked = true;
            cboMalampati.SelectedIndex = 0;
            optGMHS_duongtruyentinhmach_co.Checked = false;
            optGMHS_duongtruyentinhmach_khong.Checked = true;
            txtGMHS_khac.Clear();

            txtHTM_gangsuc.Clear();
            txtHTM_nhiptho.Clear();
            optHTM_nhiptho_deu.Checked = false;
            optHTM_nhiptho_khongdeu.Checked = true;
            optHTM_amthoi_co.Checked = true;
            optHTM_amthoi_khong.Checked = false;
            txtHTM_khac.Clear();

            optHehohap_khotho_co.Checked = false;
            optHehohap_khotho_khong.Checked = true;
            optHehohap_ran_co.Checked = false;
            optHehohap_ran_khong.Checked = true;
            txtHehohap_khac.Clear();

            txtcquan_thanhkinhxuongkhop.Clear();
            txtcquan_tieuhoa.Clear();
            txtcquan_khac.Clear();
            chk_cquan_ghinhanbatthuong.Checked = false;

            txtHTM_denghithem.Clear();

            optdanhgianguyco_duongtho_co.Checked = false;
            optdanhgianguyco_duongtho_khong.Checked = true;
            optdanhgianguyco_daday_co.Checked = false;
            optdanhgianguyco_daday_khong.Checked = true;
            optdanhgianguyco_mucdo_nhe.Checked = false;
            optdanhgianguyco_mucdo_trungbinh.Checked = false;
            optdanhgianguyco_mucdonang.Checked = false;
            optdanhgianguyco_matmau_co.Checked = false;
            optdanhgianguyco_matmau_khong.Checked = true;
            optchuongtrinh.Checked = true;
            optCapcuu.Checked = false;
            optASA1.Checked = true;
            optASA2.Checked = false;
            optASA3.Checked = false;
            optASA4.Checked = false;
            optASA5.Checked = false;
            txtPhuongPhapVoCam.SetCode("-1");
            txtGiamdaudukien.Clear();
            txtChuanbichuyenbiet.Clear();

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool isDoing = false;
        
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            AllowSeletionChanged = true;
            isDoing = false;
            cmdStart.Enabled = true;
            cmdSave.Enabled = false;
            cmdExit.BringToFront();
            grdChiDinh_CurrentCellChanged(grdChiDinh, e);
            ModifyCommands();
        }
        bool isValiData()
        {
            if (autoBSGayme.MyID == "-1")
            {
                Utility.ShowMsg("Bạn cần chọn Bác sĩ khám tiền mê");
                autoBSGayme.Focus();
                return false;
            }
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
                if (isValiData() == false) return;
                if (MessageBox.Show("Bạn chắc chắn muốn lưu phiếu khám tiền mê?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                objPkTm = new Select().From(KcbPhieukhamTienme.Schema).Where(KcbPhieukhamTienme.Columns.IdChitietchidinh).IsEqualTo(IdChitietchidinh).ExecuteSingle<KcbPhieukhamTienme>();
                if (objPkTm == null) objPkTm = new KcbPhieukhamTienme();
                if (objPkTm.Id <= 0)
                {
                    m_enAct = action.Insert;
                    objPkTm = new KcbPhieukhamTienme();
                    objPkTm.IsNew = true;
                    objPkTm.NgayTao = DateTime.Now;
                    objPkTm.NguoiTao = globalVariables.UserName;

                }
                else
                {
                    m_enAct = action.Update;
                    objPkTm.MarkOld();
                    objPkTm.IsNew = false;
                    objPkTm.NgaySua = DateTime.Now;
                    objPkTm.NguoiSua = globalVariables.UserName;
                }
                objPkTm.IdBenhnhan = objLuotkham.IdBenhnhan;
                objPkTm.MaLuotkham = objLuotkham.MaLuotkham;
                objPkTm.IdChitietchidinh = IdChitietchidinh;
                objPkTm.IdChitietdichvu = Utility.Int32Dbnull(grdChiDinh.GetValue("id_chitietdichvu"));
                objPkTm.IdBsigayme = Utility.Int16Dbnull(autoBSGayme.MyID,-1);
                objPkTm.NgayKham = dtNgaykham.Value;
                objPkTm.ChanDoan = "";
                objPkTm.TiensuNgoaigayme = Utility.DoTrim(txttiensu_ngoaigayme.Text);
                objPkTm.DiUng = string.Format("{0},{1},{2}", Utility.Bool2byte(chkDiung_chuaghinhan), Utility.Bool2byte(chkDiung_thuoc.Checked), Utility.Bool2byte(chkDiung_thucan.Checked));
                objPkTm.DiungThuoc = Utility.DoTrim(txtThuoc.Text);
                objPkTm.DiungThucan = Utility.DoTrim(txtThucan.Text);
                objPkTm.ThoiquenRuoubia = Utility.ByteDbnull(optRuoubia_khong.Checked ? 0 : (optRuoubia_nghien.Checked ? 1 : 2), 0);
                objPkTm.ThoiquenThuocla = Utility.Int16Dbnull(txtThuocla.Text,0);
                objPkTm.ThoiquenChatgaynghien = Utility.Bool2byte(optlamdungcgn_co.Checked);
                objPkTm.TiensuNoikhoa = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", 
                    Utility.Bool2byte(chktsnoikhoa_tangha), Utility.Bool2byte(chktsnoikhoa_benhtimtmcb), Utility.Bool2byte(chktsnoikhoa_daithaoduong)
                    ,Utility.Bool2byte(chktsnoikhoa_buougiap), Utility.Bool2byte(chktsnoikhoa_copd), Utility.Bool2byte(chktsnoikhoa_covid19)
                ,Utility.Bool2byte(chktsnoikhoa_roiloannhiptim), Utility.Bool2byte(chktsnoikhoa_vantim), Utility.Bool2byte(chktsnoikhoa_laophoi)
                ,Utility.Bool2byte(chktsnoikhoa_henphequan), Utility.Bool2byte(chktsnoikhoa_chuaghinhan));
                objPkTm.TinhtrangDieutri = Utility.DoTrim(txtTinhtrangdieutri.Text);
                objPkTm.TiensuNoikhoakhac = Utility.DoTrim(txttiensunoikhoa_khac.Text);
                objPkTm.TiensuNoikhoatruyenmau =Utility.ByteDbnull(opttsnoikhoa_truyenmauco.Checked ? 1 : (opttsnoikhoa_truyenmaukhong.Checked ? 0 : 2), 0);
                objPkTm.TiensuGiadinhGmhs = Utility.ByteDbnull(opttsnoikhoa_tsgdgmhs_co.Checked ? 1 : 0,0);
                objPkTm.KhamTacdungchung = Utility.DoTrim(txtkhamGMHS_tacdungchung.Text);
                objPkTm.KhamMach = Utility.DoTrim(txtkhamGMHS_mach.Text);
                objPkTm.KhamHa = Utility.DoTrim(txtkhamGMHS_HA.Text);
                objPkTm.KhamNhietdo = Utility.DoTrim(txtkhamGMHS_nhietdo.Text);
                objPkTm.KhamNhiptho = Utility.DoTrim(txtkhamGMHS_nhiptho.Text);
                objPkTm.KhamNhinan = Utility.DoTrim(txtkhamNhinan.Text);

                objPkTm.DanhgiaduongthoMomieng=Utility.Bool2byte(optGMHS_momieng_co.Checked);
                objPkTm.DanhgiaduongthoKhoangcachcamgiap=Utility.Bool2byte(optGMHS_camgiap_co.Checked);
                objPkTm.DanhgiaduongthoNguycotonthuongrang=Utility.Bool2byte(optGMHS_tonthuongrang_co.Checked);
                objPkTm.DanhgiaduongthoGapnguaco=Utility.Bool2byte(optGMHS_gapnguaco_co.Checked);
                objPkTm.DanhgiaduongthoMalampati = cboMalampati.Text;
                objPkTm.DanhgiaduongthoDuongtruyentinhmachngoaibien=Utility.Bool2byte(optGMHS_duongtruyentinhmach_co.Checked);
                objPkTm.DanhgiaduongthoKhac = Utility.DoTrim(txtGMHS_khac.Text);

                objPkTm.HetimmachGangsuc=Utility.DoTrim(txtHTM_gangsuc.Text);
                 objPkTm.HetimmachNhiptim=Utility.DoTrim(txtHTM_nhiptho.Text);
                objPkTm.HetimmachNhiptimdeu=Utility.Bool2byte(optHTM_nhiptho_deu.Checked);
                 objPkTm.HetimmachAmthoi=Utility.Bool2byte(optHTM_amthoi_co.Checked);
                 objPkTm.HetimmachKhac=Utility.DoTrim(txtHTM_khac.Text);

                objPkTm.HehohapKhotho=Utility.Bool2byte(optHehohap_khotho_co.Checked);
                 objPkTm.HehohapRan=Utility.Bool2byte(optHehohap_ran_co.Checked);
                 objPkTm.HehohapKhac=Utility.DoTrim(txtHehohap_khac.Text);

                 objPkTm.CquanThankinhCoxuongkhop=Utility.DoTrim(txtcquan_thanhkinhxuongkhop.Text);
                objPkTm.CquanTieuhoa=Utility.DoTrim(txtcquan_tieuhoa.Text);
                 objPkTm.CquanKhac=Utility.DoTrim(txtcquan_khac.Text);
                 objPkTm.CquanChuaghinhanbatthuong=Utility.Bool2byte(chk_cquan_ghinhanbatthuong);

                objPkTm.DenghiThem=Utility.DoTrim(txtHTM_denghithem.Text);

                objPkTm.DanhgianguycoDuongtho=Utility.Bool2byte(optdanhgianguyco_duongtho_co.Checked);
                 objPkTm.DanhgianguycoDadayday=Utility.Bool2byte(optdanhgianguyco_daday_co.Checked);
                objPkTm.DanhgianguycoMucdosaumo=Utility.ByteDbnull(optdanhgianguyco_mucdo_nhe.Checked ? 0 : (optdanhgianguyco_mucdo_trungbinh.Checked ? 1 : 2), 0);
                objPkTm.DanhgianguycoMatmau=Utility.Bool2byte(optdanhgianguyco_matmau_co.Checked);
                objPkTm.ChuongtrinhCapcuu=Utility.Bool2byte(optchuongtrinh.Checked);
                objPkTm.PhandoAsa=Utility.ByteDbnull( optASA1.Checked?1:(optASA2.Checked?2:(optASA3.Checked?3:(optASA4.Checked?4:5))),1);
                objPkTm.PpvocamDukien=Utility.DoTrim(txtPhuongPhapVoCam.Text);
                objPkTm.GiamdauDukien=Utility.DoTrim(txtGiamdaudukien.Text);
                objPkTm.ChuanbiChuyenbiet=Utility.DoTrim(lblCbcb.Text);

                objPkTm.Save();

                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới phiếu khám tiền mê cho bệnh nhân: {0}-{1} thành công", objPkTm.MaLuotkham, ucThongtinnguoibenh_doc_v11.txtTenBN.Text), objPkTm.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã thêm mới phiếu khám tiền mê thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật phiếu khám tiền mê cho bệnh nhân: {0}-{1} thành công", objPkTm.MaLuotkham, ucThongtinnguoibenh_doc_v11.txtTenBN.Text), objPkTm.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã cập nhật phiếu khám tiền mê thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                cmdExit.BringToFront();
                cmdCancel.PerformClick();
                AllowSeletionChanged = true;
                grdChiDinh_CurrentCellChanged(grdChiDinh, e);
                ModifyCommands();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
               
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.Coquyen("noitru_phieukhamtienme_xoa") || globalVariables.UserName ==Utility.sDbnull( grdChiDinh.GetValue("nguoi_tao"),""))
                {
                }
                else
                {
                    Utility.thongbaokhongcoquyen("noitru_phieukhamtienme_xoa", "xóa phiếu khám tiền mê");
                    return;
                }
                if (objPkTm == null || objPkTm.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn một phiếu khám tiền mê trên danh sách để xóa. Vui lòng kiểm tra lại");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin phiếu khám tiền mê đang chọn không ?", "Thông báo", true))
                {
                    int banghi = new Delete().From<KcbPhieukhamTienme>()
                         .Where(KcbPhieukhamTienme.Columns.Id)
                         .IsEqualTo(Utility.Int32Dbnull(objPkTm.Id))
                         .Execute();
                    if (banghi > 0)
                    {
                        Utility.ShowMsg("Bạn xóa thông tin phiếu khám tiền mê thành công", "Thông báo");

                        grdChiDinh_CurrentCellChanged(grdChiDinh, e);

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
              try
              {
                  if (objPkTm == null || objPkTm.Id <= 0)
                  {
                      Utility.ShowMsg("Bạn cần tạo phiếu khám tiền mê cho dịch vụ PTTT trước khi thực hiện in");
                      return;
                  }
                  DataTable dtData = SPs.PtttPhieukhamtienmeInphieu(objPkTm.Id).GetDataSet().Tables[0];
                  
                  List<string> lstAddedFields = new List<string>() { "diung_chuaghinhan", "diung_thuoc", "diung_thucan",
                "ruoubia_khong", "ruoubia_nghien", "ruoubia_thinhthoang",
                "thuocla_co","thuocla_khong",  
                "chatgaynghien_khong", "chatgaynghien_co",
                "tang_ha", "tmcb","daithaoduong","buougiap","COPD","COVID19","roiloannhip","vantim","laophoi","henphequan","chuaghinhan",
                "truyenmau_co","truyenmau_khong","truyenmau_taibien",
                "gmhs_co","gmhs_chuaghinhan",
                "momieng_co","momieng_khong",
                "camgiap_co","camgiap_khong",
                "tonthuongrang_co","tonthuongrang_khong",
                "gapnguaco_gioihan","gapnguaco_bìnhthuong",
                "truyentinhmach_de","truyentinhmach_kho",
                "nhiptho_deu","nhiptho_khongdeu",
                "amthoi_co","amthoi_khong",
                "khotho_co","khotho_khong",
                "ran_co","ran_khong","chuaghinhan_batthuong",
                "duongtho_co","duongtho_khong",
                "daday_co","daday_khong",
                "mucdosaumo_nhe","mucdosaumo_trungbinh","mucdosaumo_nang",
                "matmau_co","matmau_khong","chuongtrinh","capcuu"};
                  
                  dtData.TableName = "pttt_phieukham_tienme";
                  DataTable dtMergeField = dtData.Clone();
                  Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));
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
                  drData["ten_phieu"] = "PHIẾU KHÁM TIỀN MÊ";
                  drData["sngay_kham"] = Utility.FormatDateTime(objPkTm.NgayKham);
                  drData["diung_thucan"] = objPkTm.DiUng.Split(',')[2] == "1" ? drData["diung_thucan"] : "";
                  drData["diung_thuoc"] = objPkTm.DiUng.Split(',')[1] == "1" ? drData["diung_thuoc"] : "";
                  Dictionary<string, string> dicMF = new Dictionary<string, string>();
                  dicMF.Add("diung_chuaghinhan", objPkTm.DiUng.Split(',')[0]);
                  dicMF.Add("diung_thuoc", objPkTm.DiUng.Split(',')[1]);
                  dicMF.Add("diung_thucan", objPkTm.DiUng.Split(',')[2]);
                  dicMF.Add("ruoubia_khong", (objPkTm.ThoiquenRuoubia.Value == 0 ? 1 : 0).ToString());
                  dicMF.Add("ruoubia_nghien", (objPkTm.ThoiquenRuoubia.Value == 1 ? 1 : 0).ToString());
                  dicMF.Add("ruoubia_thinhthoang", (objPkTm.ThoiquenRuoubia.Value == 2 ? 1 : 0).ToString());
                  dicMF.Add("thuocla_khong", (Utility.Int32Dbnull(objPkTm.ThoiquenThuocla, "0") <= 0 ? 1 : 0).ToString());
                  dicMF.Add("thuocla_co", (Utility.Int32Dbnull(objPkTm.ThoiquenThuocla, "0") > 0 ? 1 : 0).ToString());
                  dicMF.Add("chatgaynghien_khong", (Utility.Byte2Bool(objPkTm.ThoiquenChatgaynghien) ? 0 : 1).ToString());
                  dicMF.Add("chatgaynghien_co", (Utility.Byte2Bool(objPkTm.ThoiquenChatgaynghien) ? 1 : 0).ToString());
                  List<string> lstTSNK = objPkTm.TiensuNoikhoa.Split(',').ToList<string>();
                  dicMF.Add("tang_ha", lstTSNK[0] == "1" ? "1" : "0");
                  dicMF.Add("tmcb", lstTSNK[1] == "1" ? "1" : "0");
                  dicMF.Add("daithaoduong", lstTSNK[2] == "1" ? "1" : "0");
                  dicMF.Add("buougiap", lstTSNK[3] == "1" ? "1" : "0");
                  dicMF.Add("COPD", lstTSNK[4] == "1" ? "1" : "0");
                  dicMF.Add("COVID19", lstTSNK[5] == "1" ? "1" : "0");
                  dicMF.Add("roiloannhip", lstTSNK[6] == "1" ? "1" : "0");
                  dicMF.Add("vantim", lstTSNK[7] == "1" ? "1" : "0");
                  dicMF.Add("laophoi", lstTSNK[8] == "1" ? "1" : "0");
                  dicMF.Add("henphequan", lstTSNK[9] == "1" ? "1" : "0");
                  dicMF.Add("chuaghinhan", lstTSNK[10] == "1" ? "1" : "0");
                  dicMF.Add("truyenmau_co", Utility.ByteDbnull(objPkTm.TiensuNoikhoatruyenmau) == 1 ? "1" : "0");
                  dicMF.Add("truyenmau_khong", Utility.ByteDbnull(objPkTm.TiensuNoikhoatruyenmau) == 0 ? "1" : "0");
                  dicMF.Add("truyenmau_taibien", Utility.ByteDbnull(objPkTm.TiensuNoikhoatruyenmau) == 2 ? "1" : "0");

                  dicMF.Add("gmhs_co", Utility.Byte2Bool(objPkTm.TiensuGiadinhGmhs) ? "1" : "0");
                  dicMF.Add("gmhs_chuaghinhan", Utility.Byte2Bool(objPkTm.TiensuGiadinhGmhs) ? "0" : "1");
                  dicMF.Add("momieng_co", Utility.Byte2Bool(objPkTm.DanhgiaduongthoMomieng) ? "1" : "0");
                  dicMF.Add("momieng_khong", Utility.Byte2Bool(objPkTm.DanhgiaduongthoMomieng) ? "0" : "1");
                  dicMF.Add("camgiap_co", Utility.Byte2Bool(objPkTm.DanhgiaduongthoKhoangcachcamgiap) ? "1" : "0");
                  dicMF.Add("camgiap_khong", Utility.Byte2Bool(objPkTm.DanhgiaduongthoKhoangcachcamgiap) ? "0" : "1");
                  dicMF.Add("tonthuongrang_co", Utility.Byte2Bool(objPkTm.DanhgiaduongthoNguycotonthuongrang) ? "1" : "0");
                  dicMF.Add("tonthuongrang_khong", Utility.Byte2Bool(objPkTm.DanhgiaduongthoNguycotonthuongrang) ? "0" : "1");
                  dicMF.Add("gapnguaco_gioihan", Utility.Byte2Bool(objPkTm.DanhgiaduongthoGapnguaco) ? "1" : "0");
                  dicMF.Add("gapnguaco_bìnhthuong", Utility.Byte2Bool(objPkTm.DanhgiaduongthoGapnguaco) ? "0" : "1");
                  dicMF.Add("truyentinhmach_de", Utility.Byte2Bool(objPkTm.DanhgiaduongthoDuongtruyentinhmachngoaibien) ? "1" : "0");
                  dicMF.Add("truyentinhmach_kho", Utility.Byte2Bool(objPkTm.DanhgiaduongthoDuongtruyentinhmachngoaibien) ? "0" : "1");
                  dicMF.Add("nhiptho_deu", Utility.Byte2Bool(objPkTm.HetimmachNhiptimdeu) ? "1" : "0");
                  dicMF.Add("nhiptho_khongdeu", Utility.Byte2Bool(objPkTm.HetimmachNhiptimdeu) ? "0" : "1");
                  dicMF.Add("amthoi_co", Utility.Byte2Bool(objPkTm.HetimmachAmthoi) ? "1" : "0");
                  dicMF.Add("amthoi_khong", Utility.Byte2Bool(objPkTm.HetimmachAmthoi) ? "0" : "1");
                  dicMF.Add("khotho_co", Utility.Byte2Bool(objPkTm.HehohapKhotho) ? "1" : "0");
                  dicMF.Add("khotho_khong", Utility.Byte2Bool(objPkTm.HehohapKhotho) ? "0" : "1");
                  dicMF.Add("ran_co", Utility.Byte2Bool(objPkTm.HehohapRan) ? "1" : "0");
                  dicMF.Add("ran_khong", Utility.Byte2Bool(objPkTm.HehohapRan) ? "0" : "1");
                  dicMF.Add("chuaghinhan_batthuong", Utility.Byte2Bool(objPkTm.CquanChuaghinhanbatthuong) ? "1" : "0");
                  dicMF.Add("duongtho_co", Utility.Byte2Bool(objPkTm.DanhgianguycoDuongtho) ? "1" : "0");
                  dicMF.Add("duongtho_khong", Utility.Byte2Bool(objPkTm.DanhgianguycoDuongtho) ? "0" : "1");
                  dicMF.Add("daday_co", Utility.Byte2Bool(objPkTm.DanhgianguycoDadayday) ? "1" : "0");
                  dicMF.Add("daday_khong", Utility.Byte2Bool(objPkTm.DanhgianguycoDadayday) ? "0" : "1");
                  dicMF.Add("mucdosaumo_nhe", objPkTm.DanhgianguycoMucdosaumo.Value == 0 ? "1" : "0");
                  dicMF.Add("mucdosaumo_trungbinh", objPkTm.DanhgianguycoMucdosaumo.Value == 1 ? "1" : "0");
                  dicMF.Add("mucdosaumo_nang", objPkTm.DanhgianguycoMucdosaumo.Value == 2 ? "1" : "0");
                  dicMF.Add("matmau_co", Utility.Byte2Bool(objPkTm.DanhgianguycoMatmau) ? "1" : "0");
                  dicMF.Add("matmau_khong", Utility.Byte2Bool(objPkTm.DanhgianguycoMatmau) ? "0" : "1");
                  dicMF.Add("chuongtrinh", Utility.Byte2Bool(objPkTm.ChuongtrinhCapcuu) ? "1" : "0");
                  dicMF.Add("capcuu", Utility.Byte2Bool(objPkTm.ChuongtrinhCapcuu) ? "0" : "1");
                  List<string> fieldNames = new List<string>();

                  string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEUKHAM_TIENME.doc";
                  string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                  if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                  string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                  if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                  CreateMergeFields(dtMergeField);
                  if (!File.Exists(PathDoc))
                  {
                      string tieude = "";
                      Utility.GetReport("PHIEUKHAM_TIENME", ref tieude, ref PathDoc);
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
                                 Path.GetFileNameWithoutExtension(PathDoc), "PHIEUKHAM_TIENME", objLuotkham.MaLuotkham, Utility.sDbnull(ID_PHIEUPTTT), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                      Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
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

      

        private void optRuoubia_thinhthoang_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (grdChiDinh.RowCount > 0 )
            {
                cmdStart.Enabled = false;
                isDoing = true;
                AllowSeletionChanged = false;
                cmdCancel.BringToFront();
                txttiensu_ngoaigayme.Focus();
                ModifyCommands();
            }
            else
            {
                Utility.ShowMsg("Bạn cần chọn dịch vụ PTTT để thực hiện khám tiền mê");
            }
        }
     
    }
}
