using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Aspose.Words;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.UI.Classess;
using VMS.HIS.Bus;
using VMS.HIS.Danhmuc.Dungchung;
using System.Transactions;
using VMS.HIS.EMR;
using VMS.HIS.Bus.Emr;
using System.Globalization;
using VNS.HIS.UI.DANHMUC;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_BenhAn_PhuKhoa : Form
    {
        public delegate void OnCreated(long id,string ma_ba, action m_enAct);
        public event OnCreated _OnCreated;
        string lstLoaiBA = "";
        DataTable dt_ThongtinNguoibenh = new DataTable();
        public frm_BenhAn_PhuKhoa(string lstLoaiBA)
        {
            InitializeComponent();
            this.lstLoaiBA = lstLoaiBA;
            Utility.SetVisualStyle(this);
            chkQLNBCapCuu.CheckedChanged += chkQLNBCapCuu_CheckedChanged;
            chkQLNBKKB.CheckedChanged += chkQLNBKKB_CheckedChanged;
            chkQLNBKhoaDieuTri.CheckedChanged += chkQLNBKhoaDieuTri_CheckedChanged;
            chkQLNBTuDen.CheckedChanged += chkQLNBTuDen_CheckedChanged;
            chkQLNBCoQuanYTe.CheckedChanged += chkQLNBCoQuanYTe_CheckedChanged;
            chkQLNBKhac.CheckedChanged += chkQLNBKhac_CheckedChanged;
            chkQLNBTuyenDuoi.CheckedChanged += chkQLNBTuyenDuoi_CheckedChanged;
            chkQLNBTuyenTren.CheckedChanged += chkQLNBTuyenTren_CheckedChanged;
            chkQLNBRaVienRavien.CheckedChanged += chkQLNBRaVien_CheckedChanged;
            chkQLNBChuyenVienCK.CheckedChanged += chkQLNBCK_CheckedChanged;
            chkQLNBRavienXinVe.CheckedChanged += chkQLNBXinVe_CheckedChanged;
            chkQLNBRavienBoVe.CheckedChanged += chkQLNBBoVe_CheckedChanged;
            chkQLNBRavienDuaVe.CheckedChanged += chkQLNBDuaVe_CheckedChanged;
            chkTTRVKhoi.CheckedChanged += chkTTRVKhoi_CheckedChanged;
            chkTTRVDoGiam.CheckedChanged += chkTTRVDoGiam_CheckedChanged;
            chkTTRVKhongThayDoi.CheckedChanged += chkTTRVKhongThayDoi_CheckedChanged;
            chkTTRVNangHon.CheckedChanged += chkTTRVNangHon_CheckedChanged;
            chkTTRVTuVong.CheckedChanged += chkTTRVTuVong_CheckedChanged;
            chkTTRVLanhTinh.CheckedChanged += chkTTRVLanhTinh_CheckedChanged;
            chkTTRVNghiNgo.CheckedChanged += chkTTRVNghiNgo_CheckedChanged;
            chkTTRVAcTinh.CheckedChanged += chkTTRVAcTinh_CheckedChanged;
            chkttrvDoBenh.CheckedChanged += chkttrvDoBenh_CheckedChanged;
            chkttrvTrong24GioVaoVien.CheckedChanged += chkttrvTrong24GioVaoVien_CheckedChanged;
            chkttrvDoTaiBien.CheckedChanged += chkttrvDoTaiBien_CheckedChanged;
            chkttrvTrong48GioVaoVien.CheckedChanged += chkttrvSau24Gio_CheckedChanged;
            chkttrvTrong72hVaovien.CheckedChanged += chkttrvKhac_CheckedChanged;
            ucThongtinnguoibenh_emr_basic1._OnEnterMe += ucThongtinnguoibenh_emr_basic1__OnEnterMe;
            txtIDBenhAn.KeyDown += txtIDBenhAn_KeyDown;
            txtMaBenhAn.KeyDown += txtMaBenhAn_KeyDown;
            ucThongtinnguoibenh_emr_basic1.trangthai_noitru = 5;
            Utility.setEnterEvent(this);
          
            txtB_CTScanner.TextChanged += soluongto_TextChanged;
            txtB_Khac.TextChanged += soluongto_TextChanged;
            txtB_SieuAm.TextChanged += soluongto_TextChanged;
            txtB_XetNghiem.TextChanged += soluongto_TextChanged;
            txtB_Xquang.TextChanged += soluongto_TextChanged;

            //txt_chandoan_truocphauthuat._OnGridSelectionChanged += txt_chandoan_truocphauthuat_OnGridSelectionChanged;
            //txt_chandoan_truocphauthuat._OnSelectionChanged += txt_chandoan_truocphauthuat_OnSelectionChanged;
            txt_chandoan_truocphauthuat._OnEnterMe += txt_chandoan_truocphauthuat_OnEnterMe;

            //txt_chandoan_sauphauthuat._OnGridSelectionChanged += txt_chandoan_sauphauthuat_OnGridSelectionChanged;
            //txt_chandoan_sauphauthuat._OnSelectionChanged += txt_chandoan_sauphauthuat_OnSelectionChanged;
            txt_chandoan_sauphauthuat._OnEnterMe += txt_chandoan_sauphauthuat_OnEnterMe;
           

            PhanquyenTinhnang();
            txtChieuCao.Leave += txtChieucao_Leave;
            txtCanNang.Leave += txtCannang_Leave;
            InitEvents();
            SetLeaveEvents(txtBenhAnQuaTrinhBenhLy);
        }
        void SetLeaveEvents(params VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung[] autoTextboxes)
        {
            foreach (var autoTxt in autoTextboxes)
            {
                autoTxt.Leave += AutoTxt_Leave;
            }
        }
        public static void RemoveTrailingEmptyLine( TextBox tb)
        {
            var lines = tb.Lines;
            int last = lines.Length - 1;

            if (last >= 0 && string.IsNullOrWhiteSpace(lines[last]))
            {
                tb.Lines = lines.Take(last).ToArray();
            }
        }
        private void AutoTxt_Leave(object sender, EventArgs e)
        {
            RemoveTrailingEmptyLine(sender as VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung);
        }

        void InitEvents()
        {
            txtBenhAnQuaTrinhBenhLy._OnShowDataV1 += _OnShowDataV1;
        }

        private void _OnShowDataV1(VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung obj)
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


        private void txtChieucao_Leave(object sender, EventArgs e)
        {
            Utility.CalculateIBM(Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieuCao.Text), 0), Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCanNang.Text), 0), txtBMI);
        }

        private void txtCannang_Leave(object sender, EventArgs e)
        {
            Utility.CalculateIBM(Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieuCao.Text), 0), Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCanNang.Text), 0), txtBMI);
        }

        private void Txt_chandoan_sauphauthuat__OnEnterMe()
        {
            throw new NotImplementedException();
        }

        private void txt_chandoan_truocphauthuat_OnEnterMe()
        {
            lbl_ma_chandoan_truocphauthuat.Text =Utility.AutoCorrectMa_Am1( txt_chandoan_truocphauthuat.MyCode);

            txt_chandoan_sauphauthuat.Focus();
            txt_chandoan_sauphauthuat.SelectAll();
        }

        //private void txt_chandoan_truocphauthuat_OnSelectionChanged()
        //{

        //}

        //private void txt_chandoan_truocphauthuat_OnGridSelectionChanged(short id_benh, string ma_benh, string ten_benh)
        //{
        //    txt_chandoan_truocphauthuat_ten.Text = ten_benh;

        //}

        private void txt_chandoan_sauphauthuat_OnEnterMe()
        {
            lbl_ma_chandoan_sauphauthuat.Text = Utility.AutoCorrectMa_Am1(txt_chandoan_sauphauthuat.MyCode);
        }

        //private void txt_chandoan_sauphauthuat_OnSelectionChanged()
        //{

        //}

        //private void txt_chandoan_sauphauthuat_OnGridSelectionChanged(short id_benh, string ma_benh, string ten_benh)
        //{
        //    txt_chandoan_sauphauthuat_ten.Text = ten_benh;
        //    // autoICD_2mat._Text = ma_benh;
        //}

        void PhanquyenTinhnang()
        {
            //cmdKCB.Visible = cmdKCB.Enabled = Utility.Coquyen("EMR_THEM_PHIEUKCB");
            //chkEditPKB.Visible = chkEditPKB.Enabled = Utility.Coquyen("EMR_SUA_PHIEUKCB");
            //chkEditTKBA.Visible = chkEditTKBA.Enabled = Utility.Coquyen("EMR_SUA_TKBA");
            //txtBenhAnToanThan.ReadOnly = txtBenhAnTuanHoan.ReadOnly = txtBenhAnHoHap.ReadOnly 
            //    = txtBenhAnTieuHoa.ReadOnly = txtBenhAnThanTietNieuSinhDuc.ReadOnly = txtBenhAnThanKinh.ReadOnly 
            //    = txtBenhAnCoXuongKhop.ReadOnly = txtBenhAnTaiMuiHong.ReadOnly = txtBenhAnMat.ReadOnly 
            //    = txtBenhAnNoiTiet.ReadOnly = Utility.Coquyen("EMR_SUA_PHIEUKCB");
        }    
        void soluongto_TextChanged(object sender, EventArgs e)
        {
            txtB_Tongso.Text =( Utility.Int32Dbnull(txtB_CTScanner.Text, 0) + Utility.Int32Dbnull(txtB_Khac.Text, 0) + Utility.Int32Dbnull(txtB_SieuAm.Text, 0) + Utility.Int32Dbnull(txtB_XetNghiem.Text, 0) + Utility.Int32Dbnull(txtB_Xquang.Text, 0)).ToString();
        }

       
        void txtMaBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string maBA = Utility.AutoFillMaBA(txtMaBenhAn.Text);
                    txtMaBenhAn.Text = maBA;
                    if (objEmrBa != null && maBA != objEmrBa.MaBa)
                    {
                        if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo mã: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtMaBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    }
                    objEmrBa = new Select().From(EmrBa.Schema).Where(EmrBa.Columns.MaBa).IsEqualTo(Utility.DoTrim(txtMaBenhAn.Text)).ExecuteSingle<EmrBa>();
                    if (objEmrBa == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = objEmrBa.MaLuotkham;
                        ucThongtinnguoibenh_emr_basic1.Refresh(true);
                        // FillData4Update();
                    }
                }
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

        void txtIDBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (objEmrBa != null)
                        if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo ID: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtIDBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull(txtIDBenhAn.Text));
                    if (objEmrBa == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = objEmrBa.MaLuotkham;
                        ucThongtinnguoibenh_emr_basic1.Refresh(true);
                        // FillData4Update();
                    }
                }
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

        void ucThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null )
            {
                if (ucThongtinnguoibenh_emr_basic1.objLuotkham.TrangthaiNoitru <= 0)
                {
                    Utility.ShowMsg(string.Format("Người bệnh {0} với mã lần khám {1} đang ở trạng thái ngoại trú nên bạn không thể thực hiện tạo BA được. Vui lòng kiểm tra lại", ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, ucThongtinnguoibenh_emr_basic1.objLuotkham.MaLuotkham));
                    objLuotkham = null;
                    //objBenhnhan = null;
                    objEmrBa = null;
                    ClearControl();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
                    return;
                }
                dt_ThongtinNguoibenh = ucThongtinnguoibenh_emr_basic1.dt_ThongtinNguoibenh;
                objEmrBa = null;
                objPhieukhamPhukhoa = null;
                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
               
                objBenhnhan = Utility.getKcbDanhsachBenhnhan(objLuotkham);
                if(objBenhnhan.IdGioitinh!=1)
                {
                    Utility.ShowMsg("Giới tính của người bệnh phải là Nữ mới được phép tạo bệnh án Phụ khoa. Vui lòng kiểm tra lại");
                    objLuotkham = null;
                    objBenhnhan = null;
                    return;
                }    
                ClearControl();
                if(!KiemTraBenhAn())
                {
                    ModifyCommand();
                    return;
                }    
                FillData4Update();
                dtQLNBVaoVien.Focus();
                ModifyCommand();
            }
        }
        bool KiemTraBenhAn()
        {
            objEmrBa = new Select().From<EmrBa>()
                    .Where(EmrBa.Columns.MaLuotkham)
                    .IsEqualTo(objLuotkham.MaLuotkham)
                    .And(EmrBa.Columns.IdBenhnhan)
                    .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan))
                    .ExecuteSingle<EmrBa>();
            if(objEmrBa==null ||(objEmrBa!=null && this.lstLoaiBA.Contains(objEmrBa.LoaiBa)))
            {
                return true;
            }
            else if (objEmrBa != null && !this.lstLoaiBA.Contains( objEmrBa.LoaiBa ))
            {
                Utility.ShowMsg(string.Format("Người bệnh {0} đã có {1} nên không thể tạo Bệnh án Phụ khoa. Vui lòng kiểm tra lại",ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, Utility.GetTenLoaiBenhAn(objEmrBa.LoaiBa)));
                objLuotkham = null;
                objBenhnhan = null; 
                return false;
            }
            return false;
        }
        #region checkbox
        private void chkttrvTrong24GioVaoVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvTrong24GioVaoVien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;

                chkttrvDoTaiBien.Checked = false;
                chkttrvTrong48GioVaoVien.Checked = false;
                chkttrvTrong72hVaovien.Checked = false;
            }
        }

        private void chkttrvDoTaiBien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoTaiBien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;

                chkttrvTrong48GioVaoVien.Checked = false;
                chkttrvTrong72hVaovien.Checked = false;
            }
        }

        private void chkttrvSau24Gio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvTrong48GioVaoVien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;

                chkttrvTrong72hVaovien.Checked = false;
            }
        }

        private void chkttrvKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvTrong72hVaovien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
                chkttrvTrong48GioVaoVien.Checked = false;

            }
        }

        private void chkQLNBBoVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBRavienBoVe.Checked == true)
            {
                chkQLNBRaVienRavien.Checked = false;
                chkQLNBRavienXinVe.Checked = false;

                chkQLNBRavienDuaVe.Checked = false;

            }
        }

        private void chkQLNBDuaVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBRavienDuaVe.Checked == true)
            {
                chkQLNBRaVienRavien.Checked = false;
                chkQLNBRavienXinVe.Checked = false;
                chkQLNBRavienBoVe.Checked = false;


            }
        }

        private void chkTTRVKhoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVKhoi.Checked == true)
            {

                chkTTRVDoGiam.Checked = false;
                chkTTRVKhongThayDoi.Checked = false;
                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVDoGiam_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVDoGiam.Checked == true)
            {
                chkTTRVKhoi.Checked = false;

                chkTTRVKhongThayDoi.Checked = false;
                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVKhongThayDoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVKhongThayDoi.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;

                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVNangHon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVNangHon.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;
                chkTTRVKhongThayDoi.Checked = false;

                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVTuVong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVTuVong.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;
                chkTTRVKhongThayDoi.Checked = false;
                chkTTRVNangHon.Checked = false;



            }
        }

        private void chkTTRVLanhTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVLanhTinh.Checked == true)
            {

                chkTTRVNghiNgo.Checked = false;
                chkTTRVAcTinh.Checked = false;

            }
        }

        private void chkTTRVNghiNgo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVNghiNgo.Checked == true)
            {
                chkTTRVLanhTinh.Checked = false;

                chkTTRVAcTinh.Checked = false;

            }
        }

        private void chkTTRVAcTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVAcTinh.Checked == true)
            {
                chkTTRVLanhTinh.Checked = false;
                chkTTRVNghiNgo.Checked = false;


            }
        }

        private void chkttrvDoBenh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoBenh.Checked == true)
            {

                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
                chkttrvTrong48GioVaoVien.Checked = false;
                chkttrvTrong72hVaovien.Checked = false;
            }
        }


        private void chkQLNBCapCuu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBCapCuu.Checked == true)
            {
                chkQLNBKKB.Checked = false;
                chkQLNBKhoaDieuTri.Checked = false;


            }
        }

        private void chkQLNBKKB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKKB.Checked == true)
            {

                chkQLNBKhoaDieuTri.Checked = false;
                chkQLNBCapCuu.Checked = false;

            }
        }

        private void chkQLNBKhoaDieuTri_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKhoaDieuTri.Checked == true)
            {
                chkQLNBKKB.Checked = false;

                chkQLNBCapCuu.Checked = false;

            }
        }

        private void chkQLNBCoQuanYTe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBCoQuanYTe.Checked == true)
            {
                chkQLNBTuDen.Checked = false;

                chkQLNBKhac.Checked = false;


            }
        }

        private void chkQLNBTuDen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuDen.Checked == true)
            {


                chkQLNBKhac.Checked = false;
                chkQLNBCoQuanYTe.Checked = false;

            }
        }

        private void chkQLNBKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKhac.Checked == true)
            {
                chkQLNBTuDen.Checked = false;
                chkQLNBCoQuanYTe.Checked = false;

            }
        }

        private void chkQLNBTuyenTren_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuyenTren.Checked == true)
            {

                chkQLNBTuyenDuoi.Checked = false;
                chkQLNBChuyenVienCK.Checked = false;

            }
        }

        private void chkQLNBTuyenDuoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuyenDuoi.Checked == true)
            {
                chkQLNBTuyenTren.Checked = false;

                chkQLNBChuyenVienCK.Checked = false;

            }
        }

        private void chkQLNBCK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBChuyenVienCK.Checked == true)
            {
                chkQLNBTuyenTren.Checked = false;
                chkQLNBTuyenDuoi.Checked = false;


            }
        }

        private void chkQLNBRaVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBRaVienRavien.Checked == true)
            {

                chkQLNBRavienXinVe.Checked = false;
                chkQLNBRavienBoVe.Checked = false;
                chkQLNBRavienDuaVe.Checked = false;

            }
        }

        private void chkQLNBXinVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBRavienXinVe.Checked == true)
            {
                chkQLNBRaVienRavien.Checked = false;

                chkQLNBRavienBoVe.Checked = false;
                chkQLNBRavienDuaVe.Checked = false;

            }
        }
        #endregion
       
        
        NoitruPhieuravien objPhieuRavien;
        private void FillThongtinRavien()
        {

            objPhieuRavien=  new Select().From(NoitruPhieuravien.Schema)
                .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
            string chandoan = "";
            string mabenh = "";
            string chandoanphu = "";
            string mabenhphu = "";
           if(objPhieuRavien!=null)
            {
                string ICD_Name = "";
                string ICD_Code = "";
                string ICD_Phu_Name = "";
                string ICD_Phu_Code = "";
               Utility.GetChanDoanChinhPhu(Utility.sDbnull_BoAm1(objPhieuRavien.MabenhChinh, ""),
                           Utility.sDbnull_BoAm1(objPhieuRavien.MabenhPhu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                chandoan += string.IsNullOrEmpty(objPhieuRavien.ChanDoan)
                    ? ICD_Name
                    : Utility.sDbnull_BoAm1(objPhieuRavien.ChanDoan);
                mabenh += ICD_Code;
                chandoanphu += ICD_Phu_Name;
                mabenhphu += ICD_Phu_Code;
                //Điền 1 số thông tin ra viện
                dtpRavien_ngay.Value = objPhieuRavien.NgayRavien;//.ToString("dd/MM/yyyy");
                foreach (CheckBox cb in pnlKetquadieutriravien.Controls)
                    if (Utility.sDbnull_BoAm1(cb.Tag, "-1") == objPhieuRavien.MaKquaDieutri)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
                foreach (CheckBox cb in pnlTinhtrangravien.Controls)
                    if (Utility.sDbnull_BoAm1(cb.Tag, "-1") == objPhieuRavien.MaTinhtrangravien)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
                //Tình trạng ra viện
               

                chkTTRVLanhTinh.Checked = Utility.Bool2Bool(objPhieuRavien.GpbLanhtinh);
                chkTTRVNghiNgo.Checked = Utility.Bool2Bool(objPhieuRavien.GpbNghingo);
                chkTTRVAcTinh.Checked = Utility.Bool2Bool(objPhieuRavien.GpbActinh);
                if (objPhieuRavien.TuvongNgay.HasValue)
                    dtpNgaytuvong.Value = objPhieuRavien.TuvongNgay.Value;
                else
                    dtpNgaytuvong.ResetText();
                chkttrvDoBenh.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongDobenh);
                chkttrvDoTaiBien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongDotaibien);
                chkttrvTrong72hVaovien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongDokhac);
                chkttrvTrong24GioVaoVien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongTrong24gio);
                chkttrvTrong48GioVaoVien.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongSau24h);

                txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull_BoAm1(objPhieuRavien.TuvongNguyennhanchinh);
                chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongChandoangiaiphaututhi);
                txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull_BoAm1(objPhieuRavien.TuvongChandoangiaiphaututhiMota);
                chkCDTaiBien.Checked = Utility.Bool2Bool(objPhieuRavien.Taibien);
                chkCDBienChung.Checked = Utility.Bool2Bool(objPhieuRavien.Bienchung);
            }
            txtCDRavienTenBenhChinh.Text = chandoan;
            txtCDRavienMaBenhChinh.Text = Utility.sDbnull_BoAm1(mabenh);
            txtCDRavienTenBenhKemTheo.Text = chandoanphu;
            txtCDRavienMaBenhKemTheo.Text = mabenhphu;

        }
      

        //private void GetChanDoanKKB()
        //{
        //    SqlQuery sqlQuery = new Select(KcbChandoanKetluan.Columns.DiagInfo, KcbChandoanKetluan.Columns.DifferInfo,
        //                                    KcbChandoanKetluan.Columns.MainDiseaseId, KcbChandoanKetluan.Columns.AuxiDiseaseId).From(
        //                                        KcbChandoanKetluan.Schema)
        //                  .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
        //                  .And(KcbChandoanKetluan.Columns.KeyCode)
        //                    .IsEqualTo("NGOAITRU")
        //                  .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(txtMaBN.Text).OrderAsc(
        //                      KcbChandoanKetluan.Columns.DiagDate);
        //    var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
        //    string chandoan = "";
        //    string mabenh = "";
        //    string tenbenhphu = "";
        //    string tenbenhchinh = "";
        //    string mabenhphu = "";
        //    foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
        //    {
        //        string ICD_Name = "";
        //        string ICD_Code = "";
        //        string ICD_Phu_Name = "";
        //        string ICD_Phu_Code = "";
        //        GetChanDoanChinhPhu(Utility.sDbnull_BoAm1(objDiagInfo.MainDiseaseId, ""),
        //                   Utility.sDbnull_BoAm1(objDiagInfo.AuxiDiseaseId, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
        //        chandoan += string.IsNullOrEmpty(objDiagInfo.DiagInfo) ? "" : Utility.sDbnull_BoAm1(objDiagInfo.DiagInfo);
        //        tenbenhchinh += ICD_Name;
        //        mabenh += ICD_Code;
        //        tenbenhphu += ICD_Phu_Name;
        //        mabenhphu += ICD_Phu_Code;
        //    }
        //    txtCDKKBCapCuu.Text = tenbenhchinh + tenbenhphu + chandoan;
        //    txtCDMaKKBCapCuu.Text = Utility.sDbnull_BoAm1(mabenh + "" + mabenhphu);

        //}
        private void ClearControl()
        {
            //txtMaBN.Clear();
            //txtMaLanKham.Clear();
            txtMaBenhAn.Clear();
            chkQLNBRavienBoVe.Checked = false;
            chkQLNBChuyenVienCK.Checked = false;
            chkQLNBTuyenDuoi.Checked = false;
            chkQLNBTuyenTren.Checked = false;
            txtQLNBChuyenVienNoiChuyenDen.Clear();
            dtpRavien_ngay.ResetText();
            chkQLNBRaVienRavien.Checked = false;


            chkQLNBRavienXinVe.Checked = false;
            chkQLNBRavienBoVe.Checked = false;
            chkQLNBRavienDuaVe.Checked = false;
            txtQLNBTongSoNgayDieuTri.Clear();
            txtCDNoiChuyenDen.Clear();
            txtCDMaNoiChuyenDen.Clear();
            txtCDKKBCapCuu.Clear();
            txtCDMaKKBCapCuu.Clear();


            txtCDKhiVaoDieuTri.Clear();
            txtCDMaKhiVaoDieuTri.Clear();
            txtCDRavienTenBenhChinh.Clear();
            txtCDRavienMaBenhChinh.Clear();
            txtCDRavienTenBenhKemTheo.Clear();
            txtCDRavienMaBenhKemTheo.Clear();
           
            chkCDTaiBien.Checked = false;
            chkCDBienChung.Checked = false;
            chk_cd_dogayme.Checked = false;
            chk_cd_donhiemkhuan.Checked = false;
            chk_cd_dokhac.Checked = false;
            chk_cd_dokhac.Checked = false;

            chkTTRVKhoi.Checked = false;
            chkTTRVDoGiam.Checked = false;
            chkTTRVKhongThayDoi.Checked = false;
            chkTTRVNangHon.Checked = false;
            chkTTRVTuVong.Checked = false;

            chkTTRVLanhTinh.Checked = false;
            chkTTRVNghiNgo.Checked = false;
            chkTTRVAcTinh.Checked = false;

            dtpNgaytuvong.ResetText();
            chkttrvDoBenh.Checked = false;
            chkttrvTrong24GioVaoVien.Checked = false;
            chkttrvDoTaiBien.Checked = false;
            chkttrvTrong48GioVaoVien.Checked = false;
            chkttrvTrong72hVaovien.Checked = false;
            txtTTRVNguyenNhanChinhTuVong.Clear();
            chkTTRVChandoanGiaiphauTuthi.Checked = false;
            txtTTRVChandoanGiaiphauTuthi.Clear();
            txtBenhAnLyDoNhapVien.SetDefaultItem();
            txtBenhAnVaoNgayThu.Clear();
            txtBenhAnQuaTrinhBenhLy.Clear();
            txtBenhAnTiensuBanthan.Clear();
           //Xóa thông tin khám phụ khoa
            txtBenhAnGiaDinh.Clear();
            txtBenhAnToanThan.Clear();
            txtMach.Clear();
            txtNhietDo.Clear();
            txtha.Clear();
            txtNhipTho.Clear();
            txtCanNang.Clear();
            txtChieuCao.Clear();
            txtBMI.Clear();
            txtBenhAnTuanHoan.Clear();
            txtBenhAnHoHap.Clear();
            txtBenhAnTieuHoa.Clear();
            txtBenhAnThanTietNieuSinhDuc.Clear();
            txtBenhAnThanKinh.Clear();
            txtBenhAnCoXuongKhop.Clear();
            txtBenhAnTaiMuiHong.Clear();
            txtBenhAnRangHamMat.Clear();
            txtBenhAnMat.Clear();
            txtBenhAnNoiTiet.Clear();
            txtBenhAnCacXetNghiem.Clear();
            txtBenhAnTomTatBenhAn.Clear();
            txtBenhAnBenhChinh.Clear();
            txtBenhAnBenhKemTheo.Clear();
            txtBenhAnPhanBiet.Clear();
            txtBenhAnTienLuong.Clear();
            txtBenhAnHuongDieuTri.Clear();
            txtTKBAQuaTrinhBenhLy.Clear();
            txtTKBATTomTatKetQua.Clear();
            txtTKBAPhuongPhapDieuTri.Clear();
            txtTKBATinhTrangRaVien.Clear();
            txtTKBAHuongDieuTri.Clear();
            txtNguoiGiaoHoSo.Clear();
            txtNguoiNhanHoSo.Clear();
            txtBSDieuTri.Clear();
            txtB_CTScanner.Clear();
            txtB_Xquang.Clear();
            txtB_SieuAm.Clear();
            txtB_XetNghiem.Clear();
            txtB_Khac.Clear();


        }
       
        private bool IsValidData(int trangthai)
        {
            if (objLuotkham != null)
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            Utility.SetMsg(lblMsg, "", false);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Cần chọn người bệnh trước khi làm Bệnh án. Vui lòng kiểm tra lại", true);
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
                return false;
            }
            if (Utility.sDbnull_BoAm1(cboLoaiBA.SelectedValue, "-1") == "-1")
            {
                uiTabBA.SelectedTab = tabpageTo1;
                Utility.SetMsg(lblMsg, "Cần chọn loại bệnh án", true);
                cboLoaiBA.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtBSlamBA.MyID, -1) <= 0)
            {
                uiTabBA.SelectedTab = tabpageTo1;
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ làm bệnh án từ danh mục Bác sĩ trong hệ thống", true);
                txtBSlamBA.Focus();
                return false;
            }
            if (trangthai == 2)
            {
                if (Utility.Int32Dbnull(txtBacsiKham.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo2;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ khám từ danh mục Bác sĩ trong hệ thống", true);
                    txtBacsiKham.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtNguoiGiaoHoSo.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Người giao hồ sơ trong danh mục hệ thống", true);
                    txtNguoiGiaoHoSo.Focus();
                    return false;
                }
                //if (Utility.Int32Dbnull(txtNguoiNhanHoSo.MyID, -1) <= 0)
                //{
                //    uiTabBA.SelectedTab = tabpageTo4;
                //    Utility.SetMsg(lblMsg, "Bạn cần chọn Người nhận hồ sơ trong danh mục hệ thống", true);
                //    txtNguoiNhanHoSo.Focus();
                //    return false;
                //}
                if (Utility.Int32Dbnull(txtBSDieuTri.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ điều trị từ danh mục Bác sĩ trong hệ thống", true);
                    txtBSDieuTri.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtTruongkhoa.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Trưởng khoa điều trị từ danh mục Bác sĩ trong hệ thống", true);
                    txtTruongkhoa.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtGDBV.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Giám đốc bệnh viện", true);
                    txtGDBV.Focus();
                    return false;
                }
            }
            return true;
        }
        EmrDocuments emrdoc = new EmrDocuments();
        bool isSuccess = false;
        int num = 0;
        private void cmdKetthucBA_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdKetthucBA.Tag.ToString() == "HUY" || objEmrBa.TrangThai == 2)
                {
                    num = new Update(EmrBa.Schema)
                        .Set(EmrBa.Columns.TrangThai).EqualTo(1)
                        .Where(EmrBa.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                        .And(EmrBa.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                        .Execute();
                    if (num > 0) objEmrBa.TrangThai = 1;
                    ModifyCommand();
                }
                else
                    LuuBA(2);
            }
            catch (Exception ex)
            {

            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trangthai">0= khởi tạo;1= Lưu;2= Kết thúc</param>
        void LuuBA(int trangthai)
        {
            try
            {
                isSuccess = false;
                if (!IsValidData(trangthai)) return;
                objEmrBa = TaoEmrBa();
                if (objEmrBa.IdBa > 0)
                {
                    if (!Utility.isValidSignStatus4UpdateDelete(objLuotkham, objEmrBa.IdBa, Loaiphieu_HIS.BA_PHUKHOA, "Bệnh án Phụ khoa"))
                        return;
                }
                objEmrBa.TrangThai =Utility.ByteDbnull( trangthai);
               //if (objEmrBa.IdBa > 0 && objEmrBa.MaBa != maBA)
               //{
               //    if(Utility.AcceptQuestion("Mã bệnh án cũ :{0} đang khác với mã bệnh án nhập tay: {1}. Bạn có chắc chắn muốn cập nhật lại thành mã bệnh án mới","",))
               //    {
               //    }
               //}
                EmrHosoluutru hsba = null;
                if (objEmrBa.IdBa <= 0)
                {
                    hsba = new EmrHosoluutru();
                    hsba.IdBa = objEmrBa.IdBa;
                    hsba.LoaiBa = objEmrBa.LoaiBa;
                    hsba.MaBa = objEmrBa.MaBa;
                    hsba.IdBenhnhan = objEmrBa.IdBenhnhan;
                    hsba.MaLuotkham = objEmrBa.MaLuotkham;
                    hsba.MaCoso = objEmrBa.MaCoso;
                    hsba.NgayTao = objEmrBa.NgaylamBa.Value;
                    hsba.NguoiTao = objEmrBa.NguoiTao;
                    hsba.Nam = objEmrBa.NgayTao.Value.Year;
                    hsba.TrangThai = 0;
                }
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objEmrBa.Save();
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.IdBsDieutrinoitruChinh).EqualTo(objEmrBa.IdBacsiDieutri)
                           .Set(KcbLuotkham.Columns.IdBa).EqualTo(objEmrBa.IdBa)
                           .Set(KcbLuotkham.Columns.LoaiBenhAn).EqualTo(objEmrBa.LoaiBa)
                                 .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                 .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                 .Execute();
                        if (hsba != null)
                        {
                            hsba.IdBa = objEmrBa.IdBa;
                            hsba.Save();
                        }

                        if (trangthai <= 0)
                        {
                            //Thực hiện hàm refresh EMR
                            int num = 0;
                            StoredProcedure sp = SPs.EmrLaydanhsachDocumentsFromTables(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "", 1, num);
                            sp.Execute();
                        }
                        else
                        {
                            //if (Utility.Coquyen("EMR_SUA_PHIEUKCB") && objEmrBa.IdBa > 0 && objEmrBa.TrangThai >= 1)
                            //{
                            TaoPhieuKCB();
                            objPKB.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám toàn thân tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objPKB.IsNew ? newaction.Insert : newaction.Update, "EMR");
                            //}
                            //if (Utility.Coquyen("EMR_SUA_TKBA") && objEmrBa.IdBa > 0 && objEmrBa.TrangThai >= 1)
                            //{
                            //    TaoPhieuTKBA();
                            //    objTKBA.Save();
                            //    if (objTKBA.IsNew)
                            //    {

                            //        emrdoc.InitDocument(objTKBA.IdBenhnhan, objTKBA.MaLuotkham, Utility.Int64Dbnull(objTKBA.Id), objTKBA.NgayTtba.Value, Loaiphieu_HIS.PHIEU_TKBA, "BA_TKBA", objTKBA.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true);
                            //        emrdoc.Save();
                            //    }
                            //    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin Tổng kết BA tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objTKBA.IsNew ? newaction.Insert : newaction.Update, "EMR");
                            //}
                            //if (Utility.Coquyen("EMR_SUA_PHIEUKHAMPHUKHOA") && objEmrBa.IdBa > 0 && objEmrBa.TrangThai >= 1)
                            //{
                            TaoPhieuKhamPhukhoa();
                            objPhieukhamPhukhoa.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám phụ khoa tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objPhieukhamPhukhoa.IsNew ? newaction.Insert : newaction.Update, "EMR");
                            //}
                        }
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_BIA, "BA04_BAPHUKHOA_BIA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);// Loaiphieu_HIS.BA_PHUKHOA);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO1, "BA04_BAPHUKHOA_TO1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO2, "BA04_BAPHUKHOA_TO2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO3, "BA04_BAPHUKHOA_TO3", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO4, "BA04_BAPHUKHOA_TO4", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BA_PHUKHOA, "BA04_BAPHUKHOA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();

                    }
                    scope.Complete();
                    isSuccess = true;
                }
                txtIDBenhAn.Text = objEmrBa.IdBa.ToString();
                if (isSuccess)
                {
                    if (m_enAct == action.Insert)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới BA cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                        MessageBox.Show(trangthai == 0 ? "Đã khởi tạo Bệnh án thành công" : "Đã thêm mới Bệnh án thành công");
                        cmdXoaBenhAn.Enabled = cmdPrint.Enabled = true;
                        if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Insert);
                        m_enAct = action.Update;
                    }
                    else if (m_enAct == action.Update)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Bệnh án cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                        if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Update);
                        MessageBox.Show("Đã cập nhật Bệnh án thành công");
                        m_enAct = action.Update;
                    }
                }
                EnableBA();
                //Utility.ShowMsg("Lưu thông tin thành công", "Thông báo");
                dtDataBA = SPs.EmrBaLaythongtinIn(-1, "", LoaiBA.BA_PHUKHOA, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                _isSuccess = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                ModifyCommand();
                //if (objEmrBa != null && _isSuccess)
                //{
                //    new Update(KcbLuotkham.Schema)
                //        .Set(KcbLuotkham.Columns.IdBa).EqualTo(objEmrBa.IdBa)
                //        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                //        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                //    // EmrThemBenhAn();
                //}

            }
        }
        void TaoPhieuKhamPhukhoa()
        {
            objPhieukhamPhukhoa = new Select().From(EmrPhieukhamPhukhoa.Schema)
             .Where(EmrPhieukhamPhukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
             .And(EmrPhieukhamPhukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
             .ExecuteSingle<EmrPhieukhamPhukhoa>();
            if (objPhieukhamPhukhoa != null && objPhieukhamPhukhoa.Id > 0)
            {
                objPhieukhamPhukhoa.MarkOld();
                objPhieukhamPhukhoa.NguoiSua = globalVariables.UserName;
                objPhieukhamPhukhoa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPhieukhamPhukhoa = new EmrPhieukhamPhukhoa();
                objPhieukhamPhukhoa.IsNew = true;
                objPhieukhamPhukhoa.MaLuotkham = Utility.sDbnull_BoAm1(objLuotkham.MaLuotkham);
                objPhieukhamPhukhoa.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieukhamPhukhoa.NgayKham = dtpNgayKham.Value.Date;
                objPhieukhamPhukhoa.NguoiTao = globalVariables.UserName;
                objPhieukhamPhukhoa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            //Hỏi bệnh
            objPhieukhamPhukhoa.IdBacsi = Utility.Int16Dbnull(txtBacsiKham.MyID, -1);
            objPhieukhamPhukhoa.PhukhoaDaniemmac = Utility.sDbnull_BoAm1(txtDaniemmac.Text);
            objPhieukhamPhukhoa.PhukhoaHach = Utility.sDbnull_BoAm1(txtHach.Text);
            objPhieukhamPhukhoa.PhukhoaVu = Utility.sDbnull_BoAm1(txtVu.Text);
            //Tiền sử sản phụ khoa
            objPhieukhamPhukhoa.BaTsspkBatdauthaykinhNam = Utility.Int16Dbnull(dtpThaykinhnam.Text, 0);
            objPhieukhamPhukhoa.BaTsspkBatdauthaykinhTuoi = Utility.Int16Dbnull(nmrBatdauthaykinhTuoi.Text, 0);
            objPhieukhamPhukhoa.BaTsspkTinhchatkinhnguyet = Utility.sDbnull_BoAm1(txt_tinhchatkinhnguyet.Text);
            objPhieukhamPhukhoa.BaTsspkChuky = Utility.Int16Dbnull(txt_chuky.Text, 0);
            objPhieukhamPhukhoa.BaTsspkSongaythaykinh = Utility.Int16Dbnull(txt_songaythaykinh.Text, 0);
            objPhieukhamPhukhoa.BaTsspkLuongkinh = Utility.sDbnull_BoAm1(txt_luongkinh.Text);
            objPhieukhamPhukhoa.BaTsspkKinhlancuoingay = dtpKinhlancuoingay.Value;
            objPhieukhamPhukhoa.BaTsspkCodaubung = chkCodaubung.Checked;
            objPhieukhamPhukhoa.BaTsspkThoigianTruoc = chk_thoigiantruoc.Checked;
            objPhieukhamPhukhoa.BaTsspkThoigianTrong = chk_thoigiantrong.Checked;
            objPhieukhamPhukhoa.BaTsspkThoigianSau = chk_thoigiansau.Checked;

            objPhieukhamPhukhoa.BaTsspkLaychongNam = Utility.Int16Dbnull(dtpLaychongNam.Text, 0);
            objPhieukhamPhukhoa.BaTsspkLaychongTuoi = Utility.Int16Dbnull(nmrLaychongTuoi.Text, 0);
            objPhieukhamPhukhoa.BaTsspkHetkinhnam = Utility.Int16Dbnull(dtpHetKinhNam.Text, 0);
            objPhieukhamPhukhoa.BaTsspkHetkinhtuoi = Utility.Int16Dbnull(nmrHetkinhTuoi.Text, 0);
            objPhieukhamPhukhoa.BaTsspkBenhphukhoadadieutri = Utility.sDbnull_BoAm1(txt_benhphukhoadadieutri.Text);
            objPhieukhamPhukhoa.BaTsspkPara = Utility.sDbnull_BoAm1(txt_para.Text);
            //Khám ngoài
            objPhieukhamPhukhoa.BaKckCacdauhieusinhducthuphat = Utility.sDbnull_BoAm1(txtCacdauhieusinhducthuphat.Text);
            objPhieukhamPhukhoa.BaKckMoilon = Utility.sDbnull_BoAm1(txtMoilon.Text);
            objPhieukhamPhukhoa.BaKckMoibe = Utility.sDbnull_BoAm1(txtMoibe.Text);
            objPhieukhamPhukhoa.BaKckAmvat = Utility.sDbnull_BoAm1(txtAmvat.Text);
            objPhieukhamPhukhoa.BaKckAmho = Utility.sDbnull_BoAm1(txtAmho.Text);
            objPhieukhamPhukhoa.BaKckMangtrinh = Utility.sDbnull_BoAm1(txtMangtrinh.Text);
            objPhieukhamPhukhoa.BaKckTangsinhmon = Utility.sDbnull_BoAm1(txtTangsinhmon.Text);
            //Khám trong
            objPhieukhamPhukhoa.BaKckAmdao = Utility.sDbnull_BoAm1(txtAmdao.Text);
            objPhieukhamPhukhoa.BaKckCotucung = Utility.sDbnull_BoAm1(txtCotucung.Text);
            objPhieukhamPhukhoa.BaKckThantucung = Utility.sDbnull_BoAm1(txtThantucung.Text);
            objPhieukhamPhukhoa.BaKckPhanphu = Utility.sDbnull_BoAm1(txtPhanphu.Text);
            objPhieukhamPhukhoa.BaKckCactuicung = Utility.sDbnull_BoAm1(txtCactuicung.Text);
            //Chức năng sống
            objPhieukhamPhukhoa.HuyetAp = txtha.Text;
            objPhieukhamPhukhoa.NhietDo = txtNhietDo.Text;
            objPhieukhamPhukhoa.Mach = Utility.sDbnull_BoAm1(txtMach.Text);
            objPhieukhamPhukhoa.NhịpTho = Utility.sDbnull_BoAm1(txtNhipTho.Text);
            objPhieukhamPhukhoa.ChieuCao = Utility.sDbnull_BoAm1(txtChieuCao.Text);
            objPhieukhamPhukhoa.CanNang = Utility.sDbnull_BoAm1(txtCanNang.Text);
            objPhieukhamPhukhoa.Bmi = Utility.sDbnull_BoAm1(txtBMI.Text);
            objPhieukhamPhukhoa.MotaThem = "";
        }
        void TaoPhieuTKBA()
        {
             objTKBA = new Select().From(EmrTomtatBa.Schema).Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrTomtatBa>();
            if (objTKBA == null) objTKBA = new EmrTomtatBa();
            if (objTKBA.Id > 0)
            {
                objTKBA.IsNew = false;
                objTKBA.MarkOld();
                objTKBA.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                objTKBA.NguoiSua = globalVariables.UserName;
            }
            else
            {
                objTKBA.IsNew = true;
                objTKBA.NguoiTao = globalVariables.UserName;
                objTKBA.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objTKBA.MaLuotkham = objLuotkham.MaLuotkham;
            objTKBA.IdBenhnhan = (int)objLuotkham.IdBenhnhan;
            objTKBA.IdKhoadieutri = Utility.Int32Dbnull(objEmrBa.IdKhoaravien, -1);
            objTKBA.TiensuBenh = "";
            objTKBA.TomtatKqcls = "";
            objTKBA.QuatrinhbenhlyDienbienlamsang = objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang;
            objTKBA.TomtatKqcls = objEmrBa.TongketbaTomtatKqcls;
            objTKBA.TinhtrangRavienMota = objEmrBa.TongketbaTinhtrangNguoiravien;
            objTKBA.PhuongphapDieutri = objEmrBa.TongketbaPhuongphapdieutri;
            objTKBA.HuongDieutri = objEmrBa.TongketbaHuongdieutritieptheo;
            objTKBA.NgayTtba = objEmrBa.TongketbaNgay;
            objTKBA.Noikhoa = 0;
            objTKBA.NoikhoaMota = "";
            objTKBA.Pttt = 0;
            objTKBA.PtttMota = "";
            objTKBA.IdNguoigiaoHoso = Utility.Int16Dbnull(txtNguoiGiaoHoSo.MyID);
            objTKBA.MaNguoigiaohoso = txtNguoiGiaoHoSo.MyCode;
            //objTKBA.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
            //objTKBA.MaNguoinhanhoso = txtNguoiNhanHoSo.MyCode;
            objTKBA.IdGiamdoc = Utility.Int16Dbnull(txtGDBV.MyID);
            objTKBA.MaGiamdoc = txtGDBV.MyCode;
            objTKBA.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
            objTKBA.MaBacsiDieutri = txtBSDieuTri.MyCode;
            objTKBA.IdTruongkhoadieutri = Utility.Int16Dbnull(txtTruongkhoa.MyID);
            objTKBA.MaTruongkhoadieutri = txtTruongkhoa.MyCode;
        }
        void TaoPhieuKCB()
        {
            //Refresh lại thông tin KCB
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(EmrPhieukhambenh.Columns.Noitru).IsEqualTo(1)
                .ExecuteSingle<EmrPhieukhambenh>();
            if (objPKB != null && objPKB.Id > 0)
            {
                objPKB.MarkOld();
                objPKB.NguoiSua = globalVariables.UserName;
                objPKB.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPKB = new EmrPhieukhambenh();
                objPKB.IsNew = true;
                objPKB.Noitru = 1;
                objPKB.MaLuotkham = Utility.sDbnull_BoAm1(objLuotkham.MaLuotkham);
                objPKB.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPKB.NgayKham = dtpNgayKham.Value.Date;
                objPKB.NguoiTao = globalVariables.UserName;
                objPKB.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPKB.IdBacsi = Utility.Int16Dbnull(txtBacsiKham.MyID, -1);
            objPKB.HuyetAp = txtha.Text;
            objPKB.NhietDo = txtNhietDo.Text;
            objPKB.Mach = Utility.sDbnull_BoAm1(txtMach.Text);
            objPKB.NhipTho = Utility.sDbnull_BoAm1(txtNhipTho.Text);
            objPKB.ChieuCao = Utility.sDbnull_BoAm1(txtChieuCao.Text);
            objPKB.CanNang = Utility.sDbnull_BoAm1(txtCanNang.Text);
            objPKB.Bmi = Utility.sDbnull_BoAm1(txtBMI.Text);
            objPKB.MotaThem = "";
            objPKB.ToanThan = Utility.sDbnull_BoAm1(txtBenhAnToanThan.Text);
            objPKB.Tuanhoan = Utility.sDbnull_BoAm1(txtBenhAnTuanHoan.Text);
            objPKB.Hohap = Utility.sDbnull_BoAm1(txtBenhAnHoHap.Text);
            objPKB.Tieuhoa = Utility.sDbnull_BoAm1(txtBenhAnTieuHoa.Text);
            objPKB.Thantietnieusinhduc = Utility.sDbnull_BoAm1(txtBenhAnThanTietNieuSinhDuc.Text);
            objPKB.Thankinh = Utility.sDbnull_BoAm1(txtBenhAnThanKinh.Text);
            objPKB.Coxuongkhop = Utility.sDbnull_BoAm1(txtBenhAnCoXuongKhop.Text);
            objPKB.Taimuihong = Utility.sDbnull_BoAm1(txtBenhAnTaiMuiHong.Text);
            objPKB.Ranghammat = Utility.sDbnull_BoAm1(txtBenhAnRangHamMat.Text);
            objPKB.Mat = Utility.sDbnull_BoAm1(txtBenhAnMat.Text);
            objPKB.Noitietdinhduongbenhlykhac = Utility.sDbnull_BoAm1(txtBenhAnNoiTiet.Text);

        }
        void EnableBA()
        {
            cboLoaiBA.Enabled = txtIDBenhAn.Enabled=cmdKhoitaoBA.Enabled= m_enAct == action.Insert;
            if (objEmrBa != null && objEmrBa.LoaiBa != Utility.sDbnull_BoAm1(cboLoaiBA.SelectedValue))
            {
                ThongbaoSaiBenhAn(objEmrBa);
                cmdPrint.Enabled = cmdKetthucBA.Enabled = cmdXoaBenhAn.Enabled = false;
            }
        }
        void ThongbaoSaiBenhAn(EmrBa objEmrBa)
        {
            string Msg = string.Format("Người bệnh {0} đang có hồ sơ Bệnh án {1} không khớp với loại Bệnh án bạn đang chọn. Vui lòng chọn lại đúng loại Bệnh án cần làm", ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, objEmrBa.LoaiBa);
            Utility.ShowMsg(Msg);
        }
        //private void EmrThemBenhAn()
        //{
        //    if (_isSuccess)
        //    {
        //        var objDmucBenhan =
        //            new Select().From(LDmucBenhan.Schema)
        //                .Where(LDmucBenhan.Columns.MaBenhan)
        //                .IsEqualTo(BenhAn_DanhMuc.BenhAn_NoiKhoa)
        //                .ExecuteSingle<LDmucBenhan>();
        //        if (objDmucBenhan != null&& objLuotkham != null)
        //        {
        //            var objPatientHi = new EmrPatientHi();
        //            objPatientHi.IdBenhanHis = Utility.Int32Dbnull(objEmrBa.Id);
        //            objPatientHi.MaPhieuEmr = objDmucBenhan.MaPhieuEmr;
        //            objPatientHi.MaDmucBa = objDmucBenhan.MaBenhan;
        //            objPatientHi.MaLuotkham = objLuotkham.MaLuotkham;
        //            objPatientHi.IdBenhnhan = objLuotkham.IdBenhnhan;
        //            objPatientHi.EmrNo = objEmrBa.MaEmrBa;
        //            ActionResult actionResult = EmrDocumentServices.ThemBenhAn(objPatientHi, action.Insert);
        //            switch (actionResult)
        //            {
        //                case ActionResult.Success: 
        //                    break;
        //            }
        //        }
        //    }
        //} 
        private EmrBa TaoEmrBa()
        {
            if (objEmrBa == null) objEmrBa = new EmrBa();
            try
            {
                int id = Utility.Int32Dbnull(txtIDBenhAn.Text, -1);
                if (objEmrBa.IdBa > 0)
                {
                    objEmrBa.IsLoaded = true;
                    objEmrBa.MarkOld();
                    objEmrBa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objEmrBa.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    SinhMaBenhAn();
                    objEmrBa.MaBa = Utility.sDbnull_BoAm1(txtMaBenhAn.Text);
                    objEmrBa.NguoiTao = globalVariables.UserName;
                    objEmrBa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                objEmrBa.NgaylamBa = dtpNgayBA.Value;
                objEmrBa.TongketbaNgay = dtpNgayTKBA.Value;
                objEmrBa.LoaiBa = cboLoaiBA.SelectedValue.ToString();
                if (dtkhoanhapvienCoGiuong.Rows.Count > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_LAYKHOANOITRU_COGIUONG", "0", false) == "1")
                {
                    objEmrBa.Khoa = Utility.sDbnull_BoAm1(dtkhoanhapvienCoGiuong.Rows[0]["ten_khoanoitru"], "");
                    objEmrBa.Giuong = Utility.sDbnull_BoAm1(dtkhoanhapvienCoGiuong.Rows[0]["ten_giuong"], "");
                    objEmrBa.Buong = Utility.sDbnull_BoAm1(dtkhoanhapvienCoGiuong.Rows[0]["ten_buong"], "");
                }
                else if (dtkhoanhapvien.Rows.Count > 0)
                {
                    objEmrBa.Khoa = Utility.sDbnull_BoAm1(dtkhoanhapvien.Rows[0]["ten_khoanoitru"], "");
                    objEmrBa.Giuong = Utility.sDbnull_BoAm1(dtkhoanhapvien.Rows[0]["ten_giuong"], "");
                    objEmrBa.Buong = Utility.sDbnull_BoAm1(dtkhoanhapvien.Rows[0]["ten_buong"], "");
                }
                else
                {
                    //REM lại vì đây là khoa nhập viện hoặc khoa nhập viện có nằm giường

                }
                //objEmrBa.BenhNgoaiKhoa = Utility.sDbnull_BoAm1(txtBenhNgoai_Khoa.Text);
                objEmrBa.MaCoso = objLuotkham.MaCoso;
                objEmrBa.IdBenhnhan = objLuotkham.IdBenhnhan;
                objEmrBa.TenBenhnhan = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                objEmrBa.MaLuotkham = objLuotkham.MaLuotkham;
                objEmrBa.MaYte = objLuotkham.MaYte;
                objEmrBa.NgaySinh = DateTime.ParseExact(Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ngay_sinh"], DateTime.Now.ToString("yyyyMMdd")), "yyyyMMdd", CultureInfo.InvariantCulture);
                objEmrBa.MaGioitinh = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["id_gioitinh"], "0") == "0" ? "M" : "F";
                objEmrBa.GioiTinh = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["gioi_tinh"], "");
                objEmrBa.Tuoi = Utility.ByteDbnull(dt_ThongtinNguoibenh.Rows[0]["Tuoi"], "0");
                objEmrBa.LoaiTuoi = (byte)objLuotkham.LoaiTuoi;


                objEmrBa.MaNghenghiep = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["nghe_nghiep"], "");
                objEmrBa.TenNghenghiep = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ten_nghenghiep"], "");
                objEmrBa.MaDantoc = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["dan_toc"], "");
                objEmrBa.TenDantoc = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ten_dantoc"], "");
                objEmrBa.MaTongiao = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ton_giao"], "");
                objEmrBa.TenTongiao = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ten_tongiao"], "");
                objEmrBa.MaQuocgia = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ma_quocgia"], "VN");
                objEmrBa.TenQuocgia = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ten_quocgia"], "");
                objEmrBa.NgoaiKieu = (Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ma_quocgia"], "VN") == "VN" ? 0 : 1) == 1;

                objEmrBa.DiachiLienhe = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["diachi_lienhe"], "");
                objEmrBa.DienthoaiLienhe = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["dienthoai_lienhe"], "");
                objEmrBa.NguoiLienhe = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["nguoi_lienhe"], "");
                objEmrBa.CmtNguoilienhe = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["CMT_nguoilienhe"], "");
                objEmrBa.DiaChi = objLuotkham.DiaChi;
                objEmrBa.MaTinhtp = objLuotkham.MaTinhtp;
                objEmrBa.TenTinhtp = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ten_tinhtp"], "");
                objEmrBa.MaQuanhuyen = objLuotkham.MaQuanhuyen;
                objEmrBa.TenQuanhuyen = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ten_quanhuyen"], "");
                objEmrBa.MaXaphuong = objLuotkham.MaXaphuong;
                objEmrBa.TenXaphuong = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ten_xaphuong"], "");
                objEmrBa.MaCoquan = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["co_quan"], "");
                objEmrBa.TenCoquan = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ten_coquan"], "");
                objEmrBa.MatheBhyt = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["mathe_bhyt"], "");
                objEmrBa.MaDoituong = Utility.ByteDbnull(objLuotkham.IdDoituongKcb);
                objEmrBa.TenDoituong = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["ten_doituong_kcb"], "");

                objEmrBa.MatheBhyt = objLuotkham.MatheBhyt;
                objEmrBa.BhytTungay = objLuotkham.NgaybatdauBhyt;
                objEmrBa.BhytDenngay = objLuotkham.NgayketthucBhyt;
                objEmrBa.HotenBo = "";
                objEmrBa.TrinhdoVanhoaBo = "";
                objEmrBa.NghenghiepBo = "";
                objEmrBa.HotenMe = "";
                objEmrBa.TrinhdoVanhoaMe = "";
                objEmrBa.NghenghiepMe = "";

                objEmrBa.CmtCccd = objLuotkham.Cmt;
                objEmrBa.SoHochieu = objLuotkham.Cmt;
                objEmrBa.DienThoai = Utility.sDbnull_BoAm1(dt_ThongtinNguoibenh.Rows[0]["dien_thoai"], "");
                objEmrBa.Email = objLuotkham.Email;

                //objEmrBa.MaKhoaravien = "";
                //objEmrBa.TenKhoaravien = objBenhnhan.TenKhoanoitru;
                //objEmrBa.IdKhoadieutri = objBenhnhan.IdKhoanoitru;
                if (objNhapvien != null)
                {
                    objEmrBa.VaovienNgay = objNhapvien.NgayNhapvien;
                    objEmrBa.VaovienCapcuu = chkQLNBCapCuu.Checked;
                    objEmrBa.VaovienKkb = chkQLNBKKB.Checked;
                    objEmrBa.VaovienKhoadieutri = chkQLNBKhoaDieuTri.Checked;

                    objEmrBa.NoigioithieuCoquanyte = chkQLNBCoQuanYTe.Checked;
                    objEmrBa.NoigioithieuTuden = chkQLNBTuDen.Checked;
                    objEmrBa.NoigioithieuKhac = chkQLNBKhac.Checked;


                    objEmrBa.VaovienLanthu = Utility.ByteDbnull(txtQLNBLanVaoVien.Text);
                }
                //Check lại
                objEmrBa.VaovienMakhoa = lblMakhoavao.Text;
                objEmrBa.VaovienTenkhoa = lblqlbnKhoa.Text;
                objEmrBa.VaovienNgayvaokhoa = null;
                if (objPhieuchuyenvien != null)
                {
                    objEmrBa.ChuyenvienTuyentren = chkQLNBTuyenTren.Checked;
                    objEmrBa.ChuyenvienTuyenduoi = chkQLNBTuyenDuoi.Checked;
                    objEmrBa.ChuyenvienKhac = chkQLNBChuyenVienCK.Checked;
                    objEmrBa.ChuyenvienNoichuyenden = Utility.sDbnull_BoAm1(txtQLNBChuyenVienNoiChuyenDen.Text);
                }
                //if (objPhieuRavien != null)
                //{
                    objEmrBa.RavienRavien = chkQLNBRaVienRavien.Checked;
                    objEmrBa.RavienXinve = chkQLNBRavienXinVe.Checked;
                    objEmrBa.RavienBove = chkQLNBRavienBoVe.Checked;
                    objEmrBa.RavienDuave = chkQLNBRavienDuaVe.Checked;
                    objEmrBa.ChuyenvienNoichuyenden = Utility.sDbnull_BoAm1(txtQLNBChuyenVienNoiChuyenDen.Text);
                    objEmrBa.ChuyenvienNoichuyenden = Utility.sDbnull_BoAm1(txtQLNBChuyenVienNoiChuyenDen.Text);
                    objEmrBa.RavienMaBenhchinh = Utility.sDbnull_BoAm1(txtCDRavienMaBenhChinh.Text);
                    objEmrBa.RavienMaBenhphu = Utility.sDbnull_BoAm1(txtCDRavienMaBenhKemTheo.Text);
                    objEmrBa.RavienTenBenhchinh = Utility.sDbnull_BoAm1(txtCDRavienTenBenhKemTheo.Text);
                    objEmrBa.RavienTenBenhphu = Utility.sDbnull_BoAm1(txtCDRavienMaBenhKemTheo.Text);
                    //Tình trạng ra viện
                    //Kết quả điều trị
                    objEmrBa.TinhtrangravienKetquadieutriKhoi = chkTTRVKhoi.Checked;
                    objEmrBa.TinhtrangravienKetquadieutriDogiam = chkTTRVDoGiam.Checked;
                    objEmrBa.TinhtrangravienKetquadieutriKhongthaydoi = chkTTRVKhongThayDoi.Checked;
                    objEmrBa.TinhtrangravienKetquadieutriNanghon = chkTTRVNangHon.Checked;
                    objEmrBa.TinhtrangravienKetquadieutriTuvong = chkTTRVTuVong.Checked;
                    //Giải phẫu bệnh
                    objEmrBa.TinhtrangravienGpbLanhtinh = chkTTRVLanhTinh.Checked;
                    objEmrBa.TinhtrangravienGpbNghingo = chkTTRVNghiNgo.Checked;
                    objEmrBa.TinhtrangravienGpbActinh = chkTTRVAcTinh.Checked;
                    //Tình hình tử vong
                    objEmrBa.TinhtrangravienThoigianTuvong = dtpNgaytuvong.Value;
                    objEmrBa.TinhtrangravienLydotuvongDobenh = chkttrvDoBenh.Checked;
                    objEmrBa.TinhtrangravienLydotuvongDotaibiendieutri = chkttrvDoTaiBien.Checked;
                    objEmrBa.TinhtrangravienLydotuvongKhac = chkttrvTrong72hVaovien.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongTrong24h = chkttrvTrong24GioVaoVien.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongSau24h = chkttrvTrong48GioVaoVien.Checked;
                    objEmrBa.TinhtrangravienNguyennhantuvong = Utility.sDbnull_BoAm1(txtTTRVNguyenNhanChinhTuVong.Text);
                    //objEmrBa.TinhtrangravienMaNguyennhantuvong = Utility.sDbnull_BoAm1(txtTTRVNguyenNhanChinhTuVong.Text);
                    objEmrBa.TinhtrangravienKhamnghiemtuthi = chkTTRVKhamNgiemTuThi.Checked;
                    objEmrBa.TinhtrangravienChandoangiauphaututhi = Utility.sDbnull_BoAm1(txtTTRVChandoanGiaiphauTuthi.Text);
                    //objEmrBa.TinhtrangravienChandoangiauphaututhi
                //}


                //Chẩn đoán
                objEmrBa.RavienTongsongayDieutri = Utility.Int16Dbnull(txtQLNBTongSoNgayDieuTri.Text);
                objEmrBa.CdNoichuyenden = txtCDNoiChuyenDen.Text;
                objEmrBa.CdNoichuyendenMa = txtCDMaNoiChuyenDen.Text;
                objEmrBa.CdKkbCapcuu = txtCDKKBCapCuu.Text;
                objEmrBa.CdKkbCapcuuMa = txtCDMaKKBCapCuu.Text;
                objEmrBa.CdKhoadieutri = txtCDKhiVaoDieuTri.Text;
                objEmrBa.CdKhoadieutriMa = txtCDMaKhiVaoDieuTri.Text;
                objEmrBa.CdNoichuyenden = txtCDNoiChuyenDen.Text;

                objEmrBa.CdDophauthuat = chk_cd_dophauthuat.Checked;
                objEmrBa.CdDonhiemkhuan = chk_cd_donhiemkhuan.Checked;
                objEmrBa.CdDogayme = chk_cd_dogayme.Checked;
                objEmrBa.CdTaibienBienchungKhac = chk_cd_dokhac.Checked;
                objEmrBa.CdTongsongaydieutriSauphauthuat = Utility.ByteDbnull(nmr_cd_tongsongaydieutri_sauphauthuat.Value);
                objEmrBa.CdTongsolanphauthuat = Utility.ByteDbnull(nmr_cd_tongsolanphauthuat.Value);
                objEmrBa.ChandoanTruocphauthuat = Utility.sDbnull_BoAm1(txt_chandoan_truocphauthuat.Text);
                objEmrBa.MaChandoanTruocphauthuat = Utility.sDbnull_BoAm1(txt_chandoan_truocphauthuat.MyCode);

                objEmrBa.ChandoanSauphauthuat = Utility.sDbnull_BoAm1(txt_chandoan_sauphauthuat.Text);
                objEmrBa.MaChandoanSauphauthuat = Utility.sDbnull_BoAm1(txt_chandoan_sauphauthuat.MyCode);

                objEmrBa.CdTaibien = chkCDTaiBien.Checked;
                objEmrBa.CdBienchung = chkCDBienChung.Checked;


                objEmrBa.VaovienLydovaovien = txtBenhAnLyDoNhapVien.Text;
                objEmrBa.VaovienVaongaythucuabenh = Utility.ByteDbnull(txtBenhAnVaoNgayThu.Text);
                objEmrBa.HoibenhQuatrinhbenhly = Utility.sDbnull_BoAm1(txtBenhAnQuaTrinhBenhLy.Text);
                objEmrBa.HoibenhTiensubanthan = Utility.sDbnull_BoAm1(txtBenhAnTiensuBanthan.Text);
                //if (objPhieukhamPhukhoa != null)//Thông tin khám phụ khoa
                //{
                //    //Hỏi bệnh
                //    objEmrBa.IdBacsiKham = Utility.Int16Dbnull(txtBacsiKham.MyID, -1);
                //    objEmrBa.KhambenhDaniemmac = Utility.sDbnull_BoAm1(txtDaniemmac.Text);
                //    objEmrBa.KhambenhHach = Utility.sDbnull_BoAm1(txtHach.Text);
                //    objEmrBa.KhambenhVu = Utility.sDbnull_BoAm1(txtVu.Text);
                //    //Tiền sử sản phụ khoa
                //    objEmrBa.HoibenhBatdauthaykinhNam = Utility.Int16Dbnull(dtpThaykinhnam.Text, 0);
                //    objEmrBa.HoibenhBatdauthaykinhTuoi = Utility.ByteDbnull(nmrBatdauthaykinhTuoi.Text, 0);
                //    objEmrBa.HoibenhTinhchatkinhnguyet = Utility.sDbnull_BoAm1(txt_tinhchatkinhnguyet.Text);
                //    objEmrBa.HoibenhChukykinh = Utility.ByteDbnull(txt_chuky.Text, 0);
                //    objEmrBa.Songaythaykinh = Utility.Int16Dbnull(txt_songaythaykinh.Text, 0);
                //    objEmrBa.HoibenhLuongkinh = Utility.ByteDbnull(txt_luongkinh.Text);
                //    objEmrBa.Kinhlancuoingay = dtpKinhlancuoingay.Value;
                //    objEmrBa.Codaubung = chkCodaubung.Checked;
                //    objEmrBa.ThoigianTruoc = chk_thoigiantruoc.Checked;
                //    objEmrBa.ThoigianTrong = chk_thoigiantrong.Checked;
                //    objEmrBa.ThoigianSau = chk_thoigiansau.Checked;

                //    objEmrBa.HoibenhLaychongNam = Utility.Int16Dbnull(dtpLaychongNam.Text, 0);
                //    objEmrBa.HoibenhLaychongTuoi = Utility.ByteDbnull(nmrLaychongTuoi.Text, 0);
                //    objEmrBa.Hetkinhnam = Utility.Int16Dbnull(dtpHetKinhNam.Text, 0);
                //    objEmrBa.Hetkinhtuoi = Utility.ByteDbnull(nmrLaychongTuoi.Text, 0);
                //    objEmrBa.HoibenhNhungbenhphukhoadadieutri = Utility.sDbnull_BoAm1(txt_benhphukhoadadieutri.Text);
                //    objEmrBa.HoibenhPara = Utility.sDbnull_BoAm1(txt_para.Text);
                //    //Khám ngoài
                //    objEmrBa.KhamngoaiCacdauhieusinhducthuphat = Utility.sDbnull_BoAm1(txtCacdauhieusinhducthuphat.Text);
                //    objEmrBa.KhamngoaiMoilon = Utility.sDbnull_BoAm1(txtMoilon.Text);
                //    objEmrBa.KhamngoaiMoibe = Utility.sDbnull_BoAm1(txtMoibe.Text);
                //    objEmrBa.KhamngoaiAmvat = Utility.sDbnull_BoAm1(txtAmvat.Text);
                //    objEmrBa.KhamngoaiAmho = Utility.sDbnull_BoAm1(txtAmho.Text);
                //    objEmrBa.KhamngoaiMangtrinh = Utility.sDbnull_BoAm1(txtMangtrinh.Text);
                //    objEmrBa.KhamngoaiTangsinhmon = Utility.sDbnull_BoAm1(txtTangsinhmon.Text);
                //    //Khám trong
                //    objEmrBa.KhamtrongAmdao = Utility.sDbnull_BoAm1(txtAmdao.Text);
                //    objEmrBa.KhamtrongCotucung = Utility.sDbnull_BoAm1(txtCotucung.Text);
                //    objEmrBa.KhamtrongThantucung = Utility.sDbnull_BoAm1(txtThantucung.Text);
                //    objEmrBa.KhamtrongPhanphu = Utility.sDbnull_BoAm1(txtPhanphu.Text);
                //    objEmrBa.KhamtrongCactuicung = Utility.sDbnull_BoAm1(txtCactuicung.Text);

                //}

                objEmrBa.HoibenhTiensugiadinh = txtBenhAnGiaDinh.Text;

                objEmrBa.KbMach = txtMach.Text;
                objEmrBa.KbNhietdo = txtNhietDo.Text;
                objEmrBa.KbHuyetap = txtha.Text;
                objEmrBa.KbNhiptho = txtNhipTho.Text;
                objEmrBa.KbCannang = txtCanNang.Text;
                objEmrBa.KbChieucao = txtChieuCao.Text;
                tinhBMI();
                //Thông tin khám bệnh
                objEmrBa.KbBmi = Utility.sDbnull_BoAm1(txtBMI.Text);
                objEmrBa.KhambenhToanthan = Utility.sDbnull_BoAm1(txtBenhAnToanThan.Text);
                objEmrBa.KhambenhTuanhoan = Utility.sDbnull_BoAm1(txtBenhAnTuanHoan.Text);
                objEmrBa.KhambenhHohap = Utility.sDbnull_BoAm1(txtBenhAnHoHap.Text);
                objEmrBa.KhambenhTieuhoa = Utility.sDbnull_BoAm1(txtBenhAnTieuHoa.Text);
                objEmrBa.KhambenhThantietnieusinhduc = Utility.sDbnull_BoAm1(txtBenhAnThanTietNieuSinhDuc.Text);
                objEmrBa.KhambenhThankinh = Utility.sDbnull_BoAm1(txtBenhAnThanKinh.Text);
                objEmrBa.KhambenhCoxuongkhop = Utility.sDbnull_BoAm1(txtBenhAnCoXuongKhop.Text);
                objEmrBa.KhambenhTaimuihong = Utility.sDbnull_BoAm1(txtBenhAnTaiMuiHong.Text);
                objEmrBa.KhambenhRanghammat = Utility.sDbnull_BoAm1(txtBenhAnRangHamMat.Text);
                objEmrBa.KhambenhMat = Utility.sDbnull_BoAm1(txtBenhAnMat.Text);
                objEmrBa.KhambenhNoitietDinhduongBenhlykhac = Utility.sDbnull_BoAm1(txtBenhAnNoiTiet.Text);

                //
                objEmrBa.KhambenhXetnghiemClsCanlam = Utility.sDbnull_BoAm1(txtBenhAnCacXetNghiem.Text);
                objEmrBa.KhambenhTomtatbenhan = Utility.sDbnull_BoAm1(txtBenhAnTomTatBenhAn.Text);
                objEmrBa.CdKhivaokhoadieutriBenhchinh = Utility.sDbnull_BoAm1(txtBenhAnBenhChinh.Text);
                objEmrBa.CdKhivaokhoadieutriBenhphu = Utility.sDbnull_BoAm1(txtBenhAnBenhKemTheo.Text);
                objEmrBa.CdKhivaokhoadieutriPhanbiet = Utility.sDbnull_BoAm1(txtBenhAnPhanBiet.Text);

                objEmrBa.KhambenhTienluong = Utility.sDbnull_BoAm1(txtBenhAnTienLuong.Text);
                objEmrBa.KhambenhHuongdieutri = Utility.sDbnull_BoAm1(txtBenhAnHuongDieuTri.Text);

                objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang = Utility.sDbnull_BoAm1(txtTKBAQuaTrinhBenhLy.Text);
                objEmrBa.TongketbaTomtatKqcls = Utility.sDbnull_BoAm1(txtTKBATTomTatKetQua.Text);
                objEmrBa.TongketbaPhuongphapdieutri = Utility.sDbnull_BoAm1(txtTKBAPhuongPhapDieuTri.Text);
                objEmrBa.TongketbaTinhtrangNguoiravien = Utility.sDbnull_BoAm1(txtTKBATinhTrangRaVien.Text);
                objEmrBa.TongketbaHuongdieutritieptheo = Utility.sDbnull_BoAm1(txtTKBAHuongDieuTri.Text);

                objEmrBa.IdNguoigiaoHoso = Utility.Int16Dbnull(txtNguoiGiaoHoSo.MyID);
                objEmrBa.TongketbaMaNguoigiaohoso = txtNguoiGiaoHoSo.Text;
                //objEmrBa.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
                //objEmrBa.TongketbaMaNguoiNhanhoso = txtNguoiNhanHoSo.Text;
                objEmrBa.MabacsiLamBA = txtBSlamBA.MyCode;
                objEmrBa.IdBacsiLamBA = Utility.Int16Dbnull(txtBSlamBA.MyID);
                objEmrBa.TenbacsiLamBA = txtBSlamBA.Text;
                objEmrBa.TenbacsiDieutri = txtBSDieuTri.Text;
                objEmrBa.IdGiamdoc = Utility.Int16Dbnull(txtGDBV.MyID);
                objEmrBa.MaGiamdoc = txtGDBV.MyCode;
                objEmrBa.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
                objEmrBa.MabacsiDieutri = txtBSDieuTri.MyCode;

                objEmrBa.IdBacsiKham = Utility.Int16Dbnull(txtBacsiKham.MyID);
                objEmrBa.MabacsiKham = txtBacsiKham.MyCode;

                objEmrBa.IdTruongkhoadieutri = Utility.Int16Dbnull(txtTruongkhoa.MyID);
                objEmrBa.MaTruongkhoadieutri = txtTruongkhoa.MyCode;


                objEmrBa.TongketbaSotoCt = Utility.Int16Dbnull(txtB_CTScanner.Text);
                objEmrBa.TongketbaSotoXquang = Utility.Int16Dbnull(txtB_Xquang.Text);
                objEmrBa.TongketbaSotoSieuam = Utility.Int16Dbnull(txtB_SieuAm.Text);
                objEmrBa.TongketbaSotoXetnghiem = Utility.Int16Dbnull(txtB_XetNghiem.Text);
                objEmrBa.TongketbaSotoKhac = Utility.Int16Dbnull(txtB_Khac.Text);
                objEmrBa.TongketbaNgay = dtpNgayTKBA.Value;
                return objEmrBa;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return objEmrBa;

            }
        }

        private void frm_BenhAn_PhuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region "Xử lý multiline"
                if (e.KeyCode == Keys.Enter)
                {
                    Control activeCtrl = Utility.getActiveControl(this);
                    if ((activeCtrl != null && activeCtrl.GetType().Equals(txtBenhAnQuaTrinhBenhLy.GetType())))
                        return;
                    else
                    {
                        if (activeCtrl.GetType().Equals(typeof(EditBox)))
                        {
                            EditBox box = activeCtrl as EditBox;
                            if (box.Multiline)
                            {
                                return;
                            }
                            else
                                SendKeys.Send("{TAB}");
                        }
                        else if (activeCtrl.GetType().Equals(typeof(TextBox)))
                        {
                            TextBox box = activeCtrl as TextBox;
                            if (box.Multiline)
                            {
                                return;
                            }
                            else
                                SendKeys.Send("{TAB}");
                        }
                        //else if (activeCtrl.Name == txtNhommau.Name)
                        //{
                        //    //uiTabInfor.SelectedIndex = 1;
                        //    //txtCT.Focus();
                        //}
                        else
                            SendKeys.Send("{TAB}");
                    }
                    

                }

                #endregion
                
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (cmdKhoitaoBA.Enabled)
                    cmdKhoitaoBA.PerformClick();
                else
                    cmdSave.PerformClick();
            }
            //if (e.KeyCode == Keys.F4) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 0;
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 1;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 2;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 3;
            }
            else if(e.KeyCode==Keys.F5)
            {
                PhanquyenTinhnang();
            }    
        }
        void InitDanhmucChung()
        {
            txtBenhAnQuaTrinhBenhLy.Init();
        }
        public action m_enAct = action.Insert;
        private void frm_BenhAn_PhuKhoa_Load(object sender, EventArgs e)
        {
            try
            {
                InitDanhmucChung();
                EnableTextBox();//Cho nhanh 
                ucThongtinnguoibenh_emr_basic1.noitrungoaitru = 1;
                ucThongtinnguoibenh_emr_basic1.AutoLoad = true;
                dtpNgayBA.Value = dtpNgayTKBA.Value = DateTime.Now;
                txtBSDieuTri.Init(globalVariables.gv_dtDmucNhanvien,
                                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                txtBSlamBA.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtBacsiKham.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtNguoiGiaoHoSo.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtNguoiNhanHoSo.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtGDBV.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                VMS.HIS.Danhmuc.Util.SetNguoiDaiDienDonVi(txtGDBV);
                txtTruongkhoa.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);

                txt_chandoan_sauphauthuat.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
                txt_chandoan_truocphauthuat.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
                //txt_chandoan_truocphauthuat.dtData = globalVariables.gv_dtDmucBenh;
                //txt_chandoan_truocphauthuat.ChangeDataSource();

                //txt_chandoan_sauphauthuat.dtData = globalVariables.gv_dtDmucBenh;
                //txt_chandoan_sauphauthuat.ChangeDataSource();

                DataTable dtData =
                    new Select().From(DmucChung.Schema)
                        .Where(DmucChung.Columns.Loai).IsEqualTo("EMR_LOAIBA")
                        .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                        .And(DmucChung.Columns.Ma).In(lstLoaiBA.Split(',').ToList<string>())
                        .OrderAsc(DmucChung.Columns.SttHthi)
                        .ExecuteDataSet().Tables[0];
                if (dtData.Rows.Count > 1)
                {
                    DataRow dr = dtData.NewRow();
                    dr[DmucChung.Columns.Ten] = "---Chọn loại BA---";
                    dr[DmucChung.Columns.Ma] = "-1";

                    dtData.Rows.InsertAt(dr, 0);
                }
                DataBinding.BindDataCombobox(cboLoaiBA, dtData, "MA", "TEN");
                txtBenhAnLyDoNhapVien.Init();
                if (m_enAct != action.Insert) ucThongtinnguoibenh_emr_basic1.Refresh();
                //if (m_enAct == action.Insert)
                //{
                   
                //}
                //else
                //{
                //    objLuotkham = Utility.getKcbLuotkham(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham);
                //    objBenhnhan = Utility.getKcbDanhsachBenhnhan(objLuotkham);
                //    dt_ThongtinNguoibenh = SPs.EmrLaythongtinnguoibenhMaluotkhamIdbenhnhan(objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                //    FillData4Update();

                //}

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
        private void FillThongtinChuyenVien()
        {

            KcbPhieuchuyenvien pcv = new Select().From(KcbPhieuchuyenvien.Schema)
                .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbPhieuchuyenvien>();
            if (pcv != null)
            {
                DmucBenhvien objBV = DmucBenhvien.FetchByID(pcv.IdBenhvienChuyenden);
                if (objBV != null)
                {
                    txtCDNoiChuyenDen.Text = objBV.TenBenhvien;
                   
                }
                chkQLNBChuyenVienCK.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 3;
                chkQLNBTuyenDuoi.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 2;
                chkQLNBTuyenTren.Checked = Utility.ByteDbnull(pcv.TuyenChuyen, 1) == 1;
            }
        }

        string ICD_Khoa_NoITru = "";
        string Name_Khoa_NoITru = "";
        DataTable dtkhoachuyen = new DataTable();
        DataTable dtkhoanhapvien = new DataTable();
        DataTable dtkhoanhapvienCoGiuong = new DataTable();
        DataTable dtCacKhoa = new DataTable();
        NoitruPhieunhapvien objNhapvien;
        KcbPhieuchuyenvien objPhieuchuyenvien;
        DataTable dtDataBA = new DataTable();
        public EmrBa objEmrBa;
        EmrPhieukhamPhukhoa objPhieukhamPhukhoa;
         EmrPhieukhambenh objPKB;
        string maBA = "";
        private bool _isSuccess = false;
        void FillData4Update()
        {
            try
            {
                maBA = "";
              
          

                objPhieuchuyenvien = new Select().From(KcbPhieuchuyenvien.Schema)
                   .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<KcbPhieuchuyenvien>();

                
                //Autofill Data

                dtCacKhoa = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "-1", -1);
                dtkhoachuyen = dtCacKhoa.Clone();
                DataRow[] arrKhoachuyen = dtCacKhoa.Select("id_chuyen>0");
                if (arrKhoachuyen.Length > 0) dtkhoachuyen = arrKhoachuyen.CopyToDataTable();
                grdQLNBKhoa.DataSource = dtkhoachuyen;
                DataRow[] arrKhoanhapvien = dtCacKhoa.Select("id_chuyen<=0");
               

                if (arrKhoanhapvien.Length > 0)
                {
                    dtkhoanhapvien = arrKhoanhapvien.CopyToDataTable();
                    lblMakhoavao.Text = Utility.sDbnull_BoAm1(arrKhoanhapvien[0]["ma_khoanoitru"], "");
                    lblqlbnKhoa.Text = Utility.sDbnull_BoAm1(arrKhoanhapvien[0]["ten_khoanoitru"], "");
                }
                var q = from p in dtCacKhoa.AsEnumerable()
                        where Utility.Int32Dbnull(p["id_giuong"], 0) > 0
                        orderby p["ngay_vaokhoa"] ascending
                        select p;
                if (q.Any())
                    dtkhoanhapvienCoGiuong = q.CopyToDataTable();
                if (objLuotkham.NgayNhapvien.HasValue)
                    dtQLNBVaoVien.Value = objLuotkham.NgayNhapvien.Value;
                else
                    dtQLNBVaoVien.ResetText();
                if (objLuotkham.NgayRavien.HasValue)
                    dtpRavien_ngay.Value = objLuotkham.NgayRavien.Value;//.Value.ToString("dd/MM/yyyy HH:mm:ss");
                else
                    dtpRavien_ngay.ResetText();
                txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull_BoAm1(objLuotkham.SongayDieutri);
              
                FillThongtinRavien();
                FillThongtinChuyenVien();
                FillTongketBenhAn();
                FillThongtinPTTT();
                //Trang 2
                FillThongtinNhapvien();
                FillPhieuKhamPhuKhoa();
                //Trang 3
                FillPhieuKCB();
                Utility.GetChanDoanNoitru(objLuotkham, ref ICD_Khoa_NoITru, ref Name_Khoa_NoITru);
                txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
                txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
                
                if (objEmrBa != null)
                {
                    m_enAct = action.Update;
                    cboLoaiBA.SelectedIndex = Utility.GetSelectedIndex(cboLoaiBA, objEmrBa.LoaiBa);
                    maBA = objEmrBa.MaBa;
                    dtDataBA = SPs.EmrBaLaythongtin(-1, "", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                    DataRow dr = dtDataBA.Rows[0];
                    try
                    {
                        txtIDBenhAn.Text = Utility.sDbnull_BoAm1(objEmrBa.IdBa);
                        txtMaBenhAn.Text = Utility.sDbnull_BoAm1(objEmrBa.MaBa);
                        //txtBenhNgoai_Khoa.Text = Utility.sDbnull_BoAm1(objEmrBa.BenhNgoaiKhoa);
                        if (objEmrBa.VaovienNgay.HasValue)
                            dtQLNBVaoVien.Value = objEmrBa.VaovienNgay.Value;
                        else
                            dtQLNBVaoVien.ResetText();
                        chkQLNBCapCuu.Checked= Utility.Bool2Bool(objEmrBa.VaovienCapcuu);
                        chkQLNBKKB.Checked = Utility.Bool2Bool(objEmrBa.VaovienKkb);
                        chkQLNBKhoaDieuTri.Checked = Utility.Bool2Bool(objEmrBa.VaovienKhoadieutri);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuCoquanyte);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuTuden);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuKhac);
                        txtQLNBLanVaoVien.Text = Utility.sDbnull_BoAm1(objEmrBa.VaovienLanthu);

                        //string ICD_chinh_Name = "";
                        //string ICD_chinh_Code = "";
                        //string ICD_Phu_Name = "";
                        //string ICD_Phu_Code = "";

                        //GetChanDoanChinhPhu(objLuotkham.MabenhChinh,
                        //                    objLuotkham.MabenhPhu,
                        //                    ref ICD_chinh_Name,
                        //                    ref ICD_chinh_Code, ref ICD_Phu_Name,
                        //                    ref ICD_Phu_Code);

                        txtCDNoiChuyenDen.Text = Utility.sDbnull_BoAm1(objEmrBa.CdNoichuyenden);
                        txtCDMaNoiChuyenDen.Text = Utility.sDbnull_BoAm1(objEmrBa.CdNoichuyendenMa);
                        txtCDKKBCapCuu.Text = Utility.sDbnull_BoAm1(objEmrBa.CdKkbCapcuu);
                        txtCDMaKKBCapCuu.Text = Utility.sDbnull_BoAm1(objEmrBa.CdKkbCapcuuMa);
                        txtCDKhiVaoDieuTri.Text = Utility.sDbnull_BoAm1(objEmrBa.CdKhoadieutri);
                        txtCDMaKhiVaoDieuTri.Text = Utility.sDbnull_BoAm1(objEmrBa.CdKhoadieutriMa);
                      



                        lblqlbnKhoa.Text = objEmrBa.VaovienTenkhoa;
                        lblMakhoavao.Text = objEmrBa.VaovienMakhoa;
                        chkQLNBTuyenTren.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyentren);
                        chkQLNBTuyenDuoi.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyenduoi);
                        chkQLNBChuyenVienCK.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienKhac);
                        txtQLNBChuyenVienNoiChuyenDen.Text = Utility.sDbnull_BoAm1(objEmrBa.ChuyenvienNoichuyenden);
                        if (objEmrBa.TrangThai >=1 )
                        {
                            if (objEmrBa.RavienNgay.HasValue)
                                dtpRavien_ngay.Value = objEmrBa.RavienNgay.Value;
                            else
                                dtpRavien_ngay.ResetText();
                            chkQLNBRaVienRavien.Checked = Utility.Bool2Bool(objEmrBa.RavienRavien);
                            chkQLNBRavienXinVe.Checked = Utility.Bool2Bool(objEmrBa.RavienXinve);
                            chkQLNBRavienBoVe.Checked = Utility.Bool2Bool(objEmrBa.RavienBove);
                            chkQLNBRavienDuaVe.Checked = Utility.Bool2Bool(objEmrBa.RavienDuave);
                            txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull_BoAm1(objEmrBa.RavienTongsongayDieutri);
                            txtCDRavienTenBenhChinh.Text = Utility.sDbnull_BoAm1(objEmrBa.RavienTenBenhchinh);
                            txtCDRavienMaBenhChinh.Text = Utility.sDbnull_BoAm1(objEmrBa.RavienMaBenhchinh);
                            txtCDRavienTenBenhKemTheo.Text = Utility.sDbnull_BoAm1(objEmrBa.RavienTenBenhphu);
                            txtCDRavienMaBenhKemTheo.Text = Utility.sDbnull_BoAm1(objEmrBa.RavienMaBenhphu);
                        }
                        chk_cd_dogayme.Checked = Utility.Bool2Bool(objEmrBa.CdDogayme);
                        chk_cd_dophauthuat.Checked = Utility.Bool2Bool(objEmrBa.CdPhauthuat);
                        chk_cd_donhiemkhuan.Checked = Utility.Bool2Bool(objEmrBa.CdDonhiemkhuan);
                        chk_cd_dokhac.Checked = Utility.Bool2Bool(objEmrBa.CdTaibienBienchungKhac);
                        chkCDTaiBien.Checked = Utility.Bool2Bool(objEmrBa.CdTaibien);
                        chkCDBienChung.Checked = Utility.Bool2Bool(objEmrBa.CdBienchung);
                        nmr_cd_tongsolanphauthuat.Value = Utility.Int32Dbnull(objEmrBa.CdTongsolanphauthuat);
                        nmr_cd_tongsongaydieutri_sauphauthuat.Value = Utility.Int32Dbnull(objEmrBa.CdTongsongaydieutriSauphauthuat);
                       
                        lbl_ma_chandoan_truocphauthuat.Text = Utility.sDbnull_BoAm1(objEmrBa.MaChandoanTruocphauthuat);
                        txt_chandoan_truocphauthuat.SetCode( Utility.sDbnull_BoAm1(objEmrBa.MaChandoanTruocphauthuat));
                        lbl_ma_chandoan_sauphauthuat.Text = Utility.sDbnull_BoAm1(objEmrBa.MaChandoanSauphauthuat);
                        txt_chandoan_sauphauthuat.SetCode(Utility.sDbnull_BoAm1(objEmrBa.MaChandoanSauphauthuat));
                        if (objEmrBa.TrangThai >= 1)
                        {
                            //Tình trạng ra viện
                            chkTTRVKhoi.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriKhoi);
                            chkTTRVDoGiam.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriDogiam);
                            chkTTRVKhongThayDoi.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriKhongthaydoi);
                            chkTTRVNangHon.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriNanghon);
                            chkTTRVTuVong.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKetquadieutriTuvong);

                            chkTTRVLanhTinh.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienGpbLanhtinh);
                            chkTTRVNghiNgo.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienGpbNghingo);
                            chkTTRVAcTinh.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienGpbActinh);
                            if (objEmrBa.TinhtrangravienThoigianTuvong.HasValue)
                                dtpNgaytuvong.Value = objEmrBa.TinhtrangravienThoigianTuvong.Value;
                            else
                                dtpNgaytuvong.ResetText();
                            chkttrvDoBenh.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienLydotuvongDobenh);
                            chkttrvDoTaiBien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienLydotuvongDotaibiendieutri);
                            chkttrvDoKhac.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienLydotuvongKhac);
                            chkttrvTrong24GioVaoVien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongTrong24h);
                            chkttrvTrong48GioVaoVien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongTrong48h);
                            chkttrvTrong72hVaovien.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienThoigiantuvongTrong72h);

                            txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull_BoAm1(objEmrBa.TinhtrangravienNguyennhantuvong);
                            chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKhamnghiemtuthi);
                            txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull_BoAm1(objEmrBa.TinhtrangravienChandoangiauphaututhi);
                            //Tờ 2
                            txtBenhAnLyDoNhapVien._Text = Utility.sDbnull_BoAm1(objEmrBa.VaovienLydovaovien);// Utility.sDbnull_BoAm1(dr["BaLdvv"].ToString());
                            txtBenhAnVaoNgayThu.Text = Utility.sDbnull_BoAm1(objEmrBa.VaovienVaongaythucuabenh);
                            txtBenhAnQuaTrinhBenhLy._Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhQuatrinhbenhly);// Utility.sDbnull_BoAm1(dr["BaQtbl"].ToString());
                            txtBenhAnTiensuBanthan.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhTiensubanthan);
                        }
                        ////Thông tin khám phụ khoa
                       
                        //txtDaniemmac.Text = objEmrBa.KhambenhDaniemmac;
                        //txtHach.Text = objEmrBa.KhambenhHach;
                        //txtVu.Text = objEmrBa.KhambenhVu;
                        ////Tiền sử sản phụ khoa
                        //dtpThaykinhnam.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhBatdauthaykinhNam);
                        //nmrBatdauthaykinhTuoi.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhBatdauthaykinhTuoi);
                        //txt_tinhchatkinhnguyet.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhTinhchatkinhnguyet);
                        //txt_chuky.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhChukykinh);
                        //txt_songaythaykinh.Text = Utility.sDbnull_BoAm1(objEmrBa.Songaythaykinh);
                        //txt_luongkinh.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhLuongkinh);
                        //if (objEmrBa.Kinhlancuoingay.HasValue)
                        //    dtpKinhlancuoingay.Value = objEmrBa.Kinhlancuoingay.Value;
                        //else
                        //    dtpKinhlancuoingay.ResetText();
                        //chkCodaubung.Checked = Utility.Bool2Bool(objEmrBa.Codaubung);
                        //chk_thoigiantruoc.Checked = Utility.Bool2Bool(objEmrBa.ThoigianTruoc);
                        //chk_thoigiantrong.Checked = Utility.Bool2Bool(objEmrBa.ThoigianTrong);
                        //chk_thoigiansau.Checked = Utility.Bool2Bool(objEmrBa.ThoigianSau);
                        //dtpLaychongNam.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhLaychongNam);
                        //nmrLaychongTuoi.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhLaychongTuoi);
                        //dtpHetKinhNam.Text = Utility.sDbnull_BoAm1(objEmrBa.Hetkinhnam);
                        //nmrHetkinhTuoi.Text = Utility.sDbnull_BoAm1(objEmrBa.Hetkinhtuoi);
                        //txt_benhphukhoadadieutri.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhNhungbenhphukhoadadieutri);
                        ////txt_para.Text = Utility.sDbnull_BoAm1(objEmrBa.pa);
                        ////khám ngoài
                        //txtCacdauhieusinhducthuphat.Text = objEmrBa.KhamngoaiCacdauhieusinhducthuphat;
                        //txtMoilon.Text = objEmrBa.KhamngoaiMoilon;
                        //txtMoibe.Text = objEmrBa.KhamngoaiMoibe;
                        //txtAmvat.Text = objEmrBa.KhamngoaiAmvat;
                        //txtAmho.Text = objEmrBa.KhamngoaiAmho;
                        //txtMangtrinh.Text = objEmrBa.KhamngoaiMangtrinh;
                        //txtTangsinhmon.Text = objEmrBa.KhamngoaiTangsinhmon;
                        ////Khám trong
                        //txtAmdao.Text = objEmrBa.KhamtrongAmdao;
                        //txtCotucung.Text = objEmrBa.KhamtrongCotucung;
                        //txtThantucung.Text = objEmrBa.KhamtrongThantucung;
                        //txtPhanphu.Text = objEmrBa.KhamtrongPhanphu;
                        //txtCactuicung.Text = objEmrBa.KhamtrongCactuicung;
                        txtBacsiKham.SetId(objEmrBa.IdBacsiKham);
                        //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objEmrBa.NgayKham) ? dtNgayKham.Value : objEmrBa.NgayKham);
                        dtpNgayKham.Value = string.IsNullOrEmpty(objEmrBa.NgayKham.ToString()) ? dtpNgayKham.Value : Convert.ToDateTime(objEmrBa.NgayKham);

                        txtBenhAnGiaDinh.Text = Utility.sDbnull_BoAm1(objEmrBa.HoibenhTiensugiadinh);// Utility.sDbnull_BoAm1(dr["BaGiaDinh"].ToString());
                        
                        txtMach.Text = Utility.sDbnull_BoAm1(objEmrBa.KbMach);
                        txtNhietDo.Text = Utility.sDbnull_BoAm1(objEmrBa.KbNhietdo);
                        txtha.Text = Utility.sDbnull_BoAm1(objEmrBa.KbHuyetap);
                        txtNhipTho.Text = Utility.sDbnull_BoAm1(objEmrBa.KbNhiptho);
                        txtCanNang.Text = Utility.sDbnull_BoAm1(objEmrBa.KbCannang);
                        txtChieuCao.Text = Utility.sDbnull_BoAm1(objEmrBa.KbChieucao);
                        tinhBMI();
                        txtBenhAnToanThan.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhToanthan);// Utility.sDbnull_BoAm1(dr["KbToanThan"].ToString());
                        txtBenhAnTuanHoan.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhTuanhoan);
                        txtBenhAnHoHap.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhHohap);
                        txtBenhAnTieuHoa.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhTieuhoa);
                        txtBenhAnThanTietNieuSinhDuc.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhThantietnieusinhduc);
                        txtBenhAnThanKinh.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhThankinh);
                        txtBenhAnCoXuongKhop.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhCoxuongkhop);
                        txtBenhAnTaiMuiHong.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhTaimuihong);
                        txtBenhAnRangHamMat.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhRanghammat);
                        txtBenhAnMat.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhMat);
                        txtBenhAnNoiTiet.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhNoitietDinhduongBenhlykhac);
                        txtBenhAnCacXetNghiem.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhXetnghiemClsCanlam);
                        txtBenhAnTomTatBenhAn.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhTomtatbenhan);
                        txtBenhAnBenhChinh.Text = Utility.sDbnull_BoAm1(objEmrBa.CdKhivaokhoadieutriBenhchinh);
                        txtBenhAnBenhKemTheo.Text = Utility.sDbnull_BoAm1(objEmrBa.CdKhivaokhoadieutriBenhphu);
                        txtBenhAnPhanBiet.Text = Utility.sDbnull_BoAm1(objEmrBa.CdKhivaokhoadieutriPhanbiet);
                        txtBenhAnTienLuong.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhTienluong);
                        txtBenhAnHuongDieuTri.Text = Utility.sDbnull_BoAm1(objEmrBa.KhambenhHuongdieutri);
                        txtTKBAQuaTrinhBenhLy.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang);
                        txtTKBATTomTatKetQua.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaTomtatKqcls);
                        txtTKBAPhuongPhapDieuTri.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaPhuongphapdieutri);
                        txtTKBATinhTrangRaVien.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaTinhtrangNguoiravien);// Utility.sDbnull_BoAm1(dr["TkbaTtrv"].ToString());
                        txtTKBAHuongDieuTri.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaHuongdieutritieptheo);// Utility.sDbnull_BoAm1(dr["TkbaHdt"].ToString());

                        txtNguoiGiaoHoSo.SetId(objEmrBa.IdNguoigiaoHoso);
                        txtNguoiNhanHoSo.SetId(objEmrBa.IdNguoinhanHoso);
                        txtBSDieuTri.SetId(objEmrBa.IdBacsiDieutri);
                        txtGDBV.SetId(objEmrBa.IdGiamdoc);
                        txtTruongkhoa.SetId(objEmrBa.IdTruongkhoadieutri);

                        txtBacsiKham.SetId(objEmrBa.IdBacsiKham);
                        txtBSlamBA.SetId(objEmrBa.IdBacsiLamBA);

                        txtB_CTScanner.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaSotoCt);
                        txtB_Xquang.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaSotoXquang);
                        txtB_SieuAm.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaSotoSieuam);
                        txtB_XetNghiem.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaSotoXetnghiem);
                        txtB_Khac.Text = Utility.sDbnull_BoAm1(objEmrBa.TongketbaSotoKhac);
                        if (objEmrBa.TongketbaNgay.HasValue)
                            dtpNgayTKBA.Value = objEmrBa.TongketbaNgay.Value;
                        else
                            dtpNgayTKBA.Value = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg(ex.ToString());
                    }
                }
                else//Auto fill
                {
                    //Điền các thông tin mặc định người bệnh
                    //Trang 1
                    m_enAct = action.Insert;
                    chkQLNBCapCuu.Checked = false;
                    chkQLNBKKB.Checked = true;
                    chkQLNBKhoaDieuTri.Checked = false;
                   
                   
                    //Trang 4
                    //GetChanDoanKKB();
                    txtCDKKBCapCuu.Text =Utility.Get_ChanDoan_KKB_CapCuu(objLuotkham);
                    txtCDMaKKBCapCuu.Text = Utility.sDbnull_BoAm1(objLuotkham.MabenhChinh, string.Empty);
                    KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                        .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbThongtinchung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbThongtinchung>();
                    if (tef != null)
                    {
                        txtMach.Text = Utility.sDbnull_BoAm1(tef.Mach);
                        txtNhietDo.Text = Utility.sDbnull_BoAm1(tef.Nhietdo);
                        txtha.Text = Utility.sDbnull_BoAm1(tef.Huyetap);
                        txtNhipTho.Text = Utility.sDbnull_BoAm1(tef.Nhiptho);
                        txtCanNang.Text = Utility.sDbnull_BoAm1(tef.Cannang);
                        txtChieuCao.Text = Utility.sDbnull_BoAm1(tef.Chieucao);
                        tinhBMI();
                    }

                    
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                EnableBA();
            }
        }
        DataTable dtPhieuPttt = new DataTable();
        void FillThongtinPTTT()
        {
            dtPhieuPttt = SPs.EmrLaythongtinPhieuPtttTo4(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
            grdPTTT.DataSource = dtPhieuPttt;
        }
        void FillPhieuKCB()
        {
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                 .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                 .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .ExecuteSingle<EmrPhieukhambenh>();
            if (objPKB != null)
            {
                dtpNgayKham.Value = objPKB.NgayKham;
                txtBacsiKham.SetId(objPKB.IdBacsi);
                txtBenhAnToanThan.Text = Utility.sDbnull_BoAm1(objPKB.ToanThan);// Utility.sDbnull_BoAm1(dr["KbToanThan"].ToString());
                txtBenhAnTuanHoan.Text = Utility.sDbnull_BoAm1(objPKB.Tuanhoan);
                txtBenhAnHoHap.Text = Utility.sDbnull_BoAm1(objPKB.Hohap);
                txtBenhAnTieuHoa.Text = Utility.sDbnull_BoAm1(objPKB.Tieuhoa);
                txtBenhAnThanTietNieuSinhDuc.Text = Utility.sDbnull_BoAm1(objPKB.Thantietnieusinhduc);
                txtBenhAnThanKinh.Text = Utility.sDbnull_BoAm1(objPKB.Thankinh);
                txtBenhAnCoXuongKhop.Text = Utility.sDbnull_BoAm1(objPKB.Coxuongkhop);
                txtBenhAnTaiMuiHong.Text = Utility.sDbnull_BoAm1(objPKB.Taimuihong);
                txtBenhAnRangHamMat.Text = Utility.sDbnull_BoAm1(objPKB.Ranghammat);
                txtBenhAnMat.Text = Utility.sDbnull_BoAm1(objPKB.Mat);
            }
        }
        void FillPhieuKhamPhuKhoa()
        {
            objPhieukhamPhukhoa = new Select().From(EmrPhieukhamPhukhoa.Schema)
             .Where(EmrPhieukhamPhukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
             .And(EmrPhieukhamPhukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
             .ExecuteSingle<EmrPhieukhamPhukhoa>();
            if (objPhieukhamPhukhoa != null)
            {
               
                txtDaniemmac.Text = objPhieukhamPhukhoa.PhukhoaDaniemmac;
                txtHach.Text = objPhieukhamPhukhoa.PhukhoaHach;
                txtVu.Text = objPhieukhamPhukhoa.PhukhoaVu;

                txtNhietDo.Text = objPhieukhamPhukhoa.NhietDo;
                txtha.Text = objPhieukhamPhukhoa.NhomMau;
                txtMach.Text = objPhieukhamPhukhoa.Mach;
                txtNhipTho.Text = objPhieukhamPhukhoa.NhịpTho;
                txtChieuCao.Text = objPhieukhamPhukhoa.ChieuCao;
                txtCanNang.Text = objPhieukhamPhukhoa.CanNang;
                txtBMI.Text = objPhieukhamPhukhoa.Bmi;

                //Tiền sử sản phụ khoa
                dtpThaykinhnam.Text = Utility.Int32Dbnull(objPhieukhamPhukhoa.BaTsspkBatdauthaykinhNam) == 0 ? DateTime.Now.Year.ToString() : Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkBatdauthaykinhNam);
                nmrBatdauthaykinhTuoi.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkBatdauthaykinhTuoi);
                txt_tinhchatkinhnguyet.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkTinhchatkinhnguyet);
                txt_chuky.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkChuky);
                txt_songaythaykinh.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkSongaythaykinh);
                txt_luongkinh.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkLuongkinh);
                if (objPhieukhamPhukhoa.BaTsspkKinhlancuoingay.HasValue)
                    dtpKinhlancuoingay.Value = objPhieukhamPhukhoa.BaTsspkKinhlancuoingay.Value;
                else
                    dtpKinhlancuoingay.ResetText();
                chkCodaubung.Checked = Utility.Bool2Bool(objPhieukhamPhukhoa.BaTsspkCodaubung);
                chk_thoigiantruoc.Checked = Utility.Bool2Bool(objPhieukhamPhukhoa.BaTsspkThoigianTruoc);
                chk_thoigiantrong.Checked = Utility.Bool2Bool(objPhieukhamPhukhoa.BaTsspkThoigianTrong);
                chk_thoigiansau.Checked = Utility.Bool2Bool(objPhieukhamPhukhoa.BaTsspkThoigianSau);
                dtpLaychongNam.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkLaychongNam);
                nmrLaychongTuoi.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkLaychongTuoi);
                dtpHetKinhNam.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkHetkinhnam);
                nmrHetkinhTuoi.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkHetkinhtuoi);
                txt_benhphukhoadadieutri.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkBenhphukhoadadieutri);
                txt_para.Text = Utility.sDbnull_BoAm1(objPhieukhamPhukhoa.BaTsspkPara);
                //khám ngoài
                txtCacdauhieusinhducthuphat.Text = objPhieukhamPhukhoa.BaKckCacdauhieusinhducthuphat;
                txtMoilon.Text = objPhieukhamPhukhoa.BaKckMoilon;
                txtMoibe.Text = objPhieukhamPhukhoa.BaKckMoibe;
                txtAmvat.Text = objPhieukhamPhukhoa.BaKckAmvat;
                txtAmho.Text = objPhieukhamPhukhoa.BaKckAmho;
                txtMangtrinh.Text = objPhieukhamPhukhoa.BaKckMangtrinh;
                txtTangsinhmon.Text = objPhieukhamPhukhoa.BaKckTangsinhmon;
                //Khám trong
                txtAmdao.Text = objPhieukhamPhukhoa.BaKckAmdao;
                txtCotucung.Text = objPhieukhamPhukhoa.BaKckCotucung;
                txtThantucung.Text = objPhieukhamPhukhoa.BaKckThantucung;
                txtPhanphu.Text = objPhieukhamPhukhoa.BaKckPhanphu;
                txtCactuicung.Text = objPhieukhamPhukhoa.BaKckCactuicung;
               txtBacsiKham.SetId(objPhieukhamPhukhoa.IdBacsi);
                //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objPhieukhamPhukhoa.NgayKham) ? dtNgayKham.Value : objPhieukhamPhukhoa.NgayKham);
                dtpNgayKham.Value = string.IsNullOrEmpty(objPhieukhamPhukhoa.NgayKham.ToString()) ? dtpNgayKham.Value : Convert.ToDateTime(objPhieukhamPhukhoa.NgayKham);
            }
        }
        void FillThongtinNhapvien()
        {
            objNhapvien = new Select().From(NoitruPhieunhapvien.Schema)
                   .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
            if (objNhapvien != null)
            {
                txtBenhAnLyDoNhapVien._Text = Utility.sDbnull_BoAm1(objNhapvien.LydoNhapvien);
                txtBenhAnTiensuBanthan.Text = Utility.sDbnull_BoAm1(objNhapvien.TsuBanthan);
                txtBenhAnGiaDinh.Text = Utility.sDbnull_BoAm1(objNhapvien.TsuGiadinh);
                txtBenhAnQuaTrinhBenhLy._Text = Utility.sDbnull_BoAm1(objNhapvien.QuatrinhBenhly);
                txtBenhAnToanThan.Text = Utility.sDbnull_BoAm1(objNhapvien.KhamToanthan);
            }
        }
       // VKcbLuotkham objBenhnhan = null;
        KcbLuotkham objLuotkham = null;
        KcbDanhsachBenhnhan objBenhnhan = null;
        private void SinhMaBenhAn()
        {
            //txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn_NoiTru();
            string MaxMaBenhAN = "";
            StoredProcedure sp = SPs.EmrBaSinhMaBA(cboLoaiBA.SelectedValue.ToString(), MaxMaBenhAN);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput) { MaxMaBenhAN = (String)objOutput; });

            txtMaBenhAn.Text = MaxMaBenhAN;

        }
        /// <summary>
        /// Trạng thái 0= khởi tạo;1= Lưu;2= kết thúc
        /// </summary>
        void ModifyCommand()
        {
            tabpageTo2.Enabled = tabpageTo3.Enabled = tabpageTo4.Enabled = objLuotkham != null;
            btnInto2.Enabled = btnInto3.Enabled = Into1.Enabled = btnInto4.Enabled = button1.Enabled = btnInVoBA.Enabled = objLuotkham != null && objEmrBa != null;
            cmdXoaBenhAn.Enabled = objLuotkham != null && objEmrBa != null;
            cmdKhoitaoBA.Enabled = objEmrBa == null;
            cmdSave.Enabled = objEmrBa != null && objEmrBa.TrangThai<=1;
            //cmdSave.Tag = objEmrBa!=null &&  objEmrBa.TrangThai == 1 ? "HUY" : "LUU";
            //cmdSave.Text = objEmrBa != null && objEmrBa.TrangThai == 1 ? "2. Hủy Lưu" : "2. Lưu BA (Ctrl+S)";
            cmdKetthucBA.Enabled = objEmrBa != null && objEmrBa.TrangThai >= 1;
            cmdKetthucBA.Tag = objEmrBa != null && objEmrBa.TrangThai == 2 ? "HUY" : "HOANTAT";
            cmdKetthucBA.Text = objEmrBa != null && objEmrBa.TrangThai == 2 ? "3. Làm lại BA" : "3. Hoàn tất BA";
           
        }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    objLuotkham.MaLuotkham = THU_VIEN_CHUNG.SinhMaHoSoKhiTimKiem(objLuotkham.MaLuotkham);
            //    if (!IsValidData()) return;
            //    FillBenhAnByPatientCode();
            //}
        }
       
        private DataTable getChitietCLS()
        {
            int status = 0;
            DataTable temdt = SPs.ClsKetQuaXetNghiem(-1,"",objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, 1, status).GetDataSet().Tables[0];

            return temdt;
        }

        private void cmdXoaBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
               
                objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull( txtIDBenhAn.Text));
                if (objEmrBa == null)
                {
                    Utility.ShowMsg("Bạn chưa chọn bệnh án nào để xóa hoặc bệnh án muốn xóa không tồn tại trong hệ thống. Vui lòng gõ lại mã lượt khám để kiểm tra");
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
                    return;
                }
                EmrHosoluutru hosoba = new Select().From(EmrHosoluutru.Schema)
                    .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                    .And(EmrHosoluutru.Columns.MaBa).IsEqualTo(objEmrBa.MaBa)
                    .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                    .And(EmrHosoluutru.Columns.IdBenhnhan).IsEqualTo(objEmrBa.IdBenhnhan)
                     .And(EmrHosoluutru.Columns.MaLuotkham).IsEqualTo(objEmrBa.MaLuotkham)
                    .ExecuteSingle<EmrHosoluutru>();
                if (Utility.Int32Dbnull( hosoba.TrangThai,0) == 1)
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
                if (objEmrBa != null && (Utility.Coquyen("EMR_XOA_BA") || globalVariables.UserName == objEmrBa.NguoiTao))
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin bệnh án đang chọn không ?", "Thông báo", true))
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
                                emrdoc.DeleteDocument_WithoutTransaction(objEmrBa.IdBa, new List<string>() { Utility.LayMaBA(objEmrBa.LoaiBa), "BENHAN_BIA", "BENHAN_TO1", "BENHAN_TO2", "BENHAN_TO3", "BENHAN_TO4" }, "");
                                Utility.Log("frm_BenhAn_PhuKhoa", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công",objEmrBa.IdBa,objEmrBa.LoaiBa,objEmrBa.MaBa,objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
                            }
                            Scope.Complete();
                        }
                       
                            Utility.ShowMsg("Bạn xóa bệnh án thành công", "Thông báo");
                            ucThongtinnguoibenh_emr_basic1.Refresh();
                            ModifyCommand();
                            
                       

                    }
                }
                else
                {
                    Utility.ShowMsg("Bạn không có quyền xóa BA(EMR_XOA_BA) hoặc không phải là người tạo Bệnh án");
                    return;

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
         
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            //var frm = new frm_TimKiem_BN();
            //frm.ShowDialog();
            //if (frm.b_Cancel)
            //{
            //    objLuotkham.MaLuotkham = Utility.sDbnull_BoAm1(frm.SoHSBA);
            //    FillBenhAnByPatientCode();
            //}
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Into1_Click(object sender, EventArgs e)
        {

            DataTable sub_dtData = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "-1",-1);
            string reportCode = "BA_NOITRU_TO1";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            THU_VIEN_CHUNG.CreateXML(sub_dtData,  "BA_noitru_khoachuyen.XML");
            noitru_inphieu.BA_noitru_Into1(dtDataBA, sub_dtData, true, reportCode, "");
        }

        private void btnInto2_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO2";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInto3_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO3";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInto4_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO4";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInVoBA_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_VOBA";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_noitru_tongketBA";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void cmdUpdateBNToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (objLuotkham != null)
            //{
            //    var frm = new frm_Update_BN();
            //    frm.objLuotkham = objLuotkham;
            //    frm.ShowDialog();
            //    SqlQuery sql = new Select().From<KcbLuotkham>().Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham);
            //    objLuotkham = sql.ExecuteSingle<KcbLuotkham>();
            //    FillBNById(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
            //    FillLanKhamById(objLuotkham);
            //}
        }

        private void cmdLamMoi_Click(object sender, EventArgs e)
        {
            ClearControl();
            objEmrBa = null;
           // objBenhnhan = null;
            objLuotkham = null;
            m_enAct = action.Insert;
            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
            ModifyCommand();
        }

        bool _isCounterpart = false; //mục đích dùng để ktra xem quá tình bệnh lý ở tổng kết bệnh án đã chỉnh sửa chưa, nếu chỉnh sửa rồi thì ko cập nhật lại
        private void txtBenhAnQuaTrinhBenhLy_Enter(object sender, EventArgs e)
        {
            //_isCounterpart = txtBenhAnQuaTrinhBenhLy.Text.Trim() == txtTKBAQuaTrinhBenhLy.Text.Trim();
        }

        private void txtBenhAnQuaTrinhBenhLy_TextChanged(object sender, EventArgs e)
        {
            //if (_isCounterpart)
            //{
            //    txtTKBAQuaTrinhBenhLy.Text = txtBenhAnQuaTrinhBenhLy.Text;
            //}
        }

        private void txtBenhAnCacXetNghiem_Enter(object sender, EventArgs e)
        {
            _isCounterpart = txtBenhAnCacXetNghiem.Text.Trim() == txtTKBATTomTatKetQua.Text.Trim();
        }

        private void txtBenhAnCacXetNghiem_TextChanged(object sender, EventArgs e)
        {
            if (_isCounterpart)
            {
                txtTKBATTomTatKetQua.Text = txtBenhAnCacXetNghiem.Text;
            }
        }

        private void txtCDKhiVaoDieuTri_TextChanged(object sender, EventArgs e)
        {
            txtBenhAnBenhChinh.Text = txtCDKhiVaoDieuTri.Text;
        }

        private void txtBenhAnHuongDieuTri_Enter(object sender, EventArgs e)
        {
            //_isCounterpart = txtBenhAnHuongDieuTri.Text.Trim() == txtTKBAHuongDieuTri.Text.Trim();
        }

        private void txtBenhAnHuongDieuTri_TextChanged(object sender, EventArgs e)
        {
            //if (_isCounterpart)
            //{
            //    txtTKBAHuongDieuTri.Text = txtBenhAnHuongDieuTri.Text;
            //}
        }

        private void txtBenhAnTomTatBenhAn_Enter(object sender, EventArgs e)
        {
            _isCounterpart = txtBenhAnTomTatBenhAn.Text.Trim() == txtTKBAQuaTrinhBenhLy.Text.Trim();
        }

        private void txtBenhAnTomTatBenhAn_TextChanged(object sender, EventArgs e)
        {
            if (_isCounterpart)
            {
                txtTKBAQuaTrinhBenhLy.Text = txtBenhAnTomTatBenhAn.Text;
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ctxIn.Show(cmdPrint, new Point(0, cmdPrint.Height));

            //if (pnlPrint.Visible == false)
            //{
            //    pnlPrint.Visible = true;
            //}
            //else
            //{
            //    pnlPrint.Visible = false;
            //}
        }

        private void cmdPrint_MouseHover(object sender, EventArgs e)
        {
            //if (pnlPrint.Visible == false)
            //{
            //    pnlPrint.Visible = true;
            //}
            //else
            //{
            //    pnlPrint.Visible = false;
            //}
        }

        private void cmdPrint_MouseLeave(object sender, EventArgs e)
        {
           
            //if (pnlPrint.Visible == false)
            //{
            //    pnlPrint.Visible = true;
            //}
            //else
            //{
            //    Thread.Sleep(5000);
            //    pnlPrint.Visible = false;
            //}
        }

        private void txtMaBenhAn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCanNang_TextChanged(object sender, EventArgs e)
        {
            tinhBMI();
        }

        private void txtChieuCao_TextChanged(object sender, EventArgs e)
        {
            tinhBMI();
        }
        void tinhBMI()
        {
            if (txtCanNang.Text.Trim() != string.Empty && txtChieuCao.Text.Trim() != string.Empty) //2 ô có giá trị thì mới tính
            {
                if (txtCanNang.Text.Trim().All(char.IsDigit) && txtChieuCao.Text.Trim().All(char.IsDigit)) //2 ô phải là kiểu số
                {
                    if (Utility.DecimaltoDbnull(txtCanNang.Text, 0) > 0 && Utility.DecimaltoDbnull(txtChieuCao.Text, 0) > 0) //2 giá trị > 0
                    {
                        decimal bmi = Utility.DecimaltoDbnull(txtCanNang.Text, 0) / (Utility.DecimaltoDbnull(txtChieuCao.Text, 0) / 100 * Utility.DecimaltoDbnull(txtChieuCao.Text, 0) / 100);
                        txtBMI.Text = Utility.sDbnull_BoAm1(Math.Round(bmi, 2));
                    }
                }
            }
        }

        private void mnuInVoBA_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, dtPhieuPttt, 0, false);
        }

        private void mnuInTomtatBA_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                return;
            }
            EmrTomtatBa objTKBA =new Select().From(EmrTomtatBa.Schema)
                .Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTomtatBa>();
            if (objTKBA == null || objTKBA.Id <= 0)
            {
                Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                return;
            }
            clsInBA.InTomTatBA(objTKBA);
        }

        private void mnuInTo1_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, dtPhieuPttt, 1, false);
        }

        private void mnuInTo2_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, dtPhieuPttt, 2, false);
        }

        private void mnuInTo3_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, dtPhieuPttt, 3, false);
        }

        private void mnuInTo4_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, dtPhieuPttt, 4, false);
        }
       
        private void mnuInBA_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, dtkhoanhapvien, dtkhoachuyen, null, dtPhieuPttt, 100, false);
        }

        private void cmdRefreshChucnangsong_Click(object sender, EventArgs e)
        {

            try
            {
                frm_XemthongtinChucnangsong _XemthongtinChucnangsong = new frm_XemthongtinChucnangsong(objLuotkham, true, 100);
                _XemthongtinChucnangsong._OnSelectMe += _XemthongtinChucnangsong__OnSelectMe;
                _XemthongtinChucnangsong.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void _XemthongtinChucnangsong__OnSelectMe(string mach, string nhietdo, string nhiptho, string huyetap, string chieucao, string cannang, string bmi, string nhommau, string SPO2)
        {
            txtMach.Text = mach;
            txtNhietDo.Text = nhietdo;
            txtNhipTho.Text = nhiptho;
            txtha.Text = huyetap;
            txtChieuCao.Text = chieucao;
            txtCanNang.Text = cannang;
            txtBMI.Text = bmi;
        }
        EmrTomtatBa objTKBA;
        void FillTongketBenhAn()
        {
            try
            {
                objTKBA=  new Select().From(EmrTomtatBa.Schema)
                    .Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<EmrTomtatBa>();
                if (objTKBA != null)
                {
                    dtpNgayTKBA.Value = objTKBA.NgayTtba.Value;
                    txtBSDieuTri.SetId(objTKBA.IdBacsiDieutri);
                    txtTKBAQuaTrinhBenhLy.Text = objTKBA.QuatrinhbenhlyDienbienlamsang;
                    txtTKBATTomTatKetQua.Text = objTKBA.TomtatKqcls;
                    txtTKBAPhuongPhapDieuTri.Text = objTKBA.PhuongphapDieutri;
                    txtTKBATinhTrangRaVien.Text = objTKBA.TinhtrangRavienMota;
                    txtTKBAHuongDieuTri.Text = objTKBA.HuongDieutri;

                    txtNguoiGiaoHoSo.SetId(objTKBA.IdNguoigiaoHoso);
                    txtNguoiNhanHoSo.SetId(objTKBA.IdNguoinhanHoso);

                    txtB_CTScanner.Text = Utility.sDbnull_BoAm1(objTKBA.SotoCt);
                    txtB_Xquang.Text = Utility.sDbnull_BoAm1(objTKBA.SotoXquang);
                    txtB_SieuAm.Text = Utility.sDbnull_BoAm1(objTKBA.SotoSieuam);
                    txtB_XetNghiem.Text = Utility.sDbnull_BoAm1(objTKBA.SotoXetnghiem);
                    txtB_Khac.Text = Utility.sDbnull_BoAm1(objTKBA.SotoKhac);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void cmdSyncTKBA_Click(object sender, EventArgs e)
        {
            FillTongketBenhAn();
        }

        private void cmdKhoitaoBA_Click(object sender, EventArgs e)
        {
            LuuBA(0);
        }

        private void cmdLaythongtinKCB_Click(object sender, EventArgs e)
        {

        }

        private void cmdDiungkhac_Click(object sender, EventArgs e)
        {
            
        }

        private void cmdKCB_Click(object sender, EventArgs e)
        {
            try
            {
                if ( objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một người bệnh trên danh sách người bệnh để bắt đầu công việc khám cơ bản");
                    return;
                }
                frm_KCBCoban _KCBCoban = new frm_KCBCoban(objLuotkham, null);
                _KCBCoban.ShowDialog();
                FillPhieuKCB();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdPhieuKCB2_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một người bệnh trên danh sách người bệnh để bắt đầu công việc khám cơ bản");
                    return;
                }
                frm_KCBCoban _KCBCoban = new frm_KCBCoban(objLuotkham, null);
                _KCBCoban.ShowDialog();
                FillPhieuKCB();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void chkEditPKB_CheckedChanged(object sender, EventArgs e)
        {
            //txtBenhAnToanThan.ReadOnly = txtBenhAnTuanHoan.ReadOnly = txtBenhAnHoHap.ReadOnly
            //   = txtBenhAnTieuHoa.ReadOnly = txtBenhAnThanTietNieuSinhDuc.ReadOnly = txtBenhAnThanKinh.ReadOnly
            //   = txtBenhAnCoXuongKhop.ReadOnly = txtBenhAnTaiMuiHong.ReadOnly = txtBenhAnMat.ReadOnly
            //   = txtBenhAnNoiTiet.ReadOnly = true;// !chkEditPKB.Checked && !chkEditPKB.Visible;
        }
        void EnableTextBox()
        {
            //txtBenhAnToanThan.ReadOnly = txtBenhAnTuanHoan.ReadOnly = txtBenhAnHoHap.ReadOnly
            // = txtBenhAnTieuHoa.ReadOnly = txtBenhAnThanTietNieuSinhDuc.ReadOnly = txtBenhAnThanKinh.ReadOnly
            // = txtBenhAnCoXuongKhop.ReadOnly = txtBenhAnTaiMuiHong.ReadOnly = txtBenhAnMat.ReadOnly
            // = txtBenhAnNoiTiet.ReadOnly = true;
            //txtTKBAQuaTrinhBenhLy.ReadOnly = txtTKBATTomTatKetQua.ReadOnly
            // = txtTKBAPhuongPhapDieuTri.ReadOnly = txtTKBATinhTrangRaVien.ReadOnly
            // = txtTKBAHuongDieuTri.ReadOnly = txtB_Xquang.ReadOnly = txtB_CTScanner.ReadOnly = txtB_SieuAm.ReadOnly
            // = txtB_XetNghiem.ReadOnly = txtB_Khac.ReadOnly = txtNguoiGiaoHoSo.ReadOnly = txtNguoiNhanHoSo.ReadOnly = txtBSDieuTri.ReadOnly
            // = false;
        }
        private void chkEditTKBA_CheckedChanged(object sender, EventArgs e)
        {
            //txtTKBAQuaTrinhBenhLy.ReadOnly = txtTKBATTomTatKetQua.ReadOnly
            //  = txtTKBAPhuongPhapDieuTri.ReadOnly = txtTKBATinhTrangRaVien.ReadOnly
            //  = txtTKBAHuongDieuTri.ReadOnly = txtB_Xquang.ReadOnly = txtB_CTScanner.ReadOnly = txtB_SieuAm.ReadOnly
            //  = txtB_XetNghiem.ReadOnly = txtB_Khac.ReadOnly = txtNguoiGiaoHoSo.ReadOnly = txtNguoiNhanHoSo.ReadOnly = txtBSDieuTri.ReadOnly
            //  = false;// !chkEditTKBA.Checked && !chkEditTKBA.Visible;
        }

        private void mnuSent2EMR_Click(object sender, EventArgs e)
        {
            try
            {
                if(objEmrBa!=null)
                {
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_BIA, "BA04_BAPHUKHOA_BIA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_PHUKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO1, "BA04_BAPHUKHOA_TO1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_PHUKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO2, "BA04_BAPHUKHOA_TO2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_PHUKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO3, "BA04_BAPHUKHOA_TO3", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_PHUKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO4, "BA04_BAPHUKHOA_TO4", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_PHUKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BA_PHUKHOA, "BA04_BAPHUKHOA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_PHUKHOA);
                    emrdoc.Save();
                    Utility.ShowMsg("Đẩy dữ liệu vào EMR thành công");
                }    
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdKhamphukhoa_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Cần nhập thông tin người bệnh trước khi thực hiện thêm thông tin phiếu khám phụ khoa");
                return;
            }
            frm_khamPhukhoa _khamPhukhoa = new frm_khamPhukhoa(objLuotkham, objBenhnhan);
            _khamPhukhoa.ShowDialog();
            FillPhieuKhamPhuKhoa();
        }

        private void cmdKhamPhuKhoa2_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Cần nhập thông tin người bệnh trước khi thực hiện thêm thông tin phiếu khám phụ khoa");
                return;
            }
            frm_khamPhukhoa _khamPhukhoa = new frm_khamPhukhoa(objLuotkham, objBenhnhan);
            _khamPhukhoa.ShowDialog();
            FillPhieuKhamPhuKhoa();
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            LuuBA(1);
        }

        private void lnk21_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCDKKBCapCuu.Text = Utility.Get_ChanDoan_KKB_CapCuu(objLuotkham);
            txtCDMaKKBCapCuu.Text = Utility.sDbnull_BoAm1(objLuotkham.MabenhChinh, string.Empty);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utility.GetChanDoanNoitru(objLuotkham, ref ICD_Khoa_NoITru, ref Name_Khoa_NoITru);
            txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
            txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
        }
    }
}
