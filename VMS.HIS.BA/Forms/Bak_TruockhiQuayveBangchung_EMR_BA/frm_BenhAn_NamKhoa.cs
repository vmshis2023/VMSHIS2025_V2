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

namespace VMS.HIS.UI.EMR
{
    public partial class frm_BenhAn_NamKhoa : Form
    {
        public delegate void OnCreated(long id,string ma_ba, action m_enAct);
        public event OnCreated _OnCreated;
        string lstLoaiBA = "";
        DataTable dt_ThongtinNguoibenh = new DataTable();
        public frm_BenhAn_NamKhoa(string lstLoaiBA)
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
            lbl_ma_chandoan_truocphauthuat.Text = txt_chandoan_truocphauthuat.MyCode;

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
            lbl_ma_chandoan_sauphauthuat.Text = txt_chandoan_sauphauthuat.MyCode;
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
            cmdKCB.Visible = cmdKCB.Enabled = Utility.Coquyen("EMR_THEM_PHIEUKCB");
            chkEditPKB.Visible = chkEditPKB.Enabled = Utility.Coquyen("EMR_SUA_PHIEUKCB");
            chkEditTKBA.Visible = chkEditTKBA.Enabled = Utility.Coquyen("EMR_SUA_TKBA");
            txt_toanthan.ReadOnly = txt_tuanhoan.ReadOnly = txt_hohap.ReadOnly 
                = txt_tieuhoa.ReadOnly = txt_thantietnieu_sinhduc.ReadOnly = txt_thankinh.ReadOnly 
                = txt_coxuongkhop.ReadOnly = txt_taimuihong.ReadOnly 
                = txt_khac.ReadOnly = Utility.Coquyen("EMR_SUA_PHIEUKCB");
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
                    objEmrBa = new Select().From(EmrBaNamkhoa.Schema).Where(EmrBaNamkhoa.Columns.MaBa).IsEqualTo(Utility.DoTrim(txtMaBenhAn.Text)).ExecuteSingle<EmrBaNamkhoa>();
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
                    objEmrBa = EmrBaNamkhoa.FetchByID(Utility.Int64Dbnull(txtIDBenhAn.Text));
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
                objEmrBa = null;
                objPhieukhamNamkhoa = null;
                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
                dt_ThongtinNguoibenh = ucThongtinnguoibenh_emr_basic1.dt_ThongtinNguoibenh;
                objBenhnhan = Utility.getKcbDanhsachBenhnhan(objLuotkham);
                if (objBenhnhan.IdGioitinh != 0)
                {
                    Utility.ShowMsg("Giới tính của người bệnh phải là Nam mới được phép tạo bệnh án Nam khoa. Vui lòng kiểm tra lại");
                    objLuotkham = null;
                    objBenhnhan = null;
                    return;
                }
                ClearControl();
                FillData4Update();
                dtQLNBVaoVien.Focus();
                ModifyCommand();
            }
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
        private void GetChanDoanChinhPhu(string ICD_chinh, string IDC_Phu, ref string ICD_chinh_Name,
            ref string ICD_chinh_Code, ref string ICD_Phu_Name, ref string ICD_Phu_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list = new Select().From(DmucBenh.Schema).Where(DmucBenh.Columns.MaBenh).In(lstICD).ExecuteAsCollection<DmucBenhCollection>();
                    //new DmucBenh().FetchByQuery(               DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _objEmrBa in _list)
                {
                    ICD_chinh_Name += _objEmrBa.TenBenh + ";";
                    ICD_chinh_Code += _objEmrBa.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _objEmrBa in _list)
                {
                    ICD_Phu_Name += _objEmrBa.TenBenh + ";";
                    ICD_Phu_Code += _objEmrBa.MaBenh + ";";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }

        private void GetChanDoanNoitru()
        {
            var dtPatient = new DataTable();
            dtPatient =
                new Select("*")
                    .From(KcbChandoanKetluan.Schema)
                    .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                     .And(KcbChandoanKetluan.Columns.KieuChandoan).IsEqualTo(2)//Chẩn đoán trong quá trình điều trị nội trú.
                    .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(1)
                    .ExecuteDataSet()
                    .Tables[0];
            foreach (DataRow row in dtPatient.Rows)
            {
                ICD_Khoa_NoITru += row["mabenh_chinh"] + ";";
                Name_Khoa_NoITru += row["chandoan"] + ";";
            }
        }
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
                GetChanDoanChinhPhu(Utility.sDbnull(objPhieuRavien.MabenhChinh, ""),
                           Utility.sDbnull(objPhieuRavien.MabenhPhu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                chandoan += string.IsNullOrEmpty(objPhieuRavien.ChanDoan)
                    ? ICD_Name
                    : Utility.sDbnull(objPhieuRavien.ChanDoan);
                mabenh += ICD_Code;
                chandoanphu += ICD_Phu_Name;
                mabenhphu += ICD_Phu_Code;
                //Điền 1 số thông tin ra viện
                dtpRavien_ngay.Value = objPhieuRavien.NgayRavien;//.ToString("dd/MM/yyyy");
                foreach (CheckBox cb in pnlKetquadieutriravien.Controls)
                    if (Utility.sDbnull(cb.Tag, "-1") == objPhieuRavien.MaKquaDieutri)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
                foreach (CheckBox cb in pnlTinhtrangravien.Controls)
                    if (Utility.sDbnull(cb.Tag, "-1") == objPhieuRavien.MaTinhtrangravien)
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

                txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objPhieuRavien.TuvongNguyennhanchinh);
                chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objPhieuRavien.TuvongChandoangiaiphaututhi);
                txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull(objPhieuRavien.TuvongChandoangiaiphaututhiMota);
                chkCDTaiBien.Checked = Utility.Bool2Bool(objPhieuRavien.Taibien);
                chkCDBienChung.Checked = Utility.Bool2Bool(objPhieuRavien.Bienchung);
            }
            txtCDRavienTenBenhChinh.Text = chandoan;
            txtCDRavienMaBenhChinh.Text = Utility.sDbnull(mabenh);
            txtCDRavienTenBenhKemTheo.Text = chandoanphu;
            txtCDRavienMaBenhKemTheo.Text = mabenhphu;

        }
        string Get_ChanDoan_KKB_CapCuu()
        {
            string _result = string.Empty;
            try
            {
                SqlQuery sqlQuery = new Select(KcbChandoanKetluan.Columns.Chandoan, KcbChandoanKetluan.Columns.ChandoanKemtheo, KcbChandoanKetluan.Columns.MabenhChinh, KcbChandoanKetluan.Columns.MabenhPhu)
                                            .From(KcbChandoanKetluan.Schema)
                                              .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                                      //.And(KcbChandoanKetluan.Columns.KeyCode).IsEqualTo("NGOAITRU")
                                                      .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).OrderAsc(KcbChandoanKetluan.Columns.NgayChandoan);
                var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                string chandoan = "";
                string mabenh = "";
                string tenbenhphu = "";
                string tenbenhchinh = "";
                string mabenhphu = "";
                foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
                {
                    string ICD_Name = "";
                    string ICD_Code = "";
                    string ICD_Phu_Name = "";
                    string ICD_Phu_Code = "";
                    GetChanDoanChinhPhu(Utility.sDbnull(objDiagInfo.MabenhChinh, ""), Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                    chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan) ? "" : Utility.sDbnull(objDiagInfo.Chandoan);
                    tenbenhchinh += ICD_Name;
                    mabenh += ICD_Code;
                    tenbenhphu += ICD_Phu_Name;
                    mabenhphu += ICD_Phu_Code;
                }
                _result =THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_SUDUNG_ICD_LAM_CHANDOANSOBO","0",true)=="1"  ? tenbenhchinh + tenbenhphu + chandoan : chandoan; //nếu dùng icd làm cdsb thì trên cdsb đã có tên bệnh rồi, ko cần cộng vào nữa
            }
            catch (Exception)
            {
                _result = string.Empty;
            }
            return _result;
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
        //        GetChanDoanChinhPhu(Utility.sDbnull(objDiagInfo.MainDiseaseId, ""),
        //                   Utility.sDbnull(objDiagInfo.AuxiDiseaseId, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
        //        chandoan += string.IsNullOrEmpty(objDiagInfo.DiagInfo) ? "" : Utility.sDbnull(objDiagInfo.DiagInfo);
        //        tenbenhchinh += ICD_Name;
        //        mabenh += ICD_Code;
        //        tenbenhphu += ICD_Phu_Name;
        //        mabenhphu += ICD_Phu_Code;
        //    }
        //    txtCDKKBCapCuu.Text = tenbenhchinh + tenbenhphu + chandoan;
        //    txtCDMaKKBCapCuu.Text = Utility.sDbnull(mabenh + "" + mabenhphu);

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
           //Xóa thông tin khám Sản khoa
            txtBenhAnGiaDinh.Clear();
            txt_toanthan.Clear();
            txtMach.Clear();
            txtNhietDo.Clear();
            txtha.Clear();
            txtNhipTho.Clear();
            txtCanNang.Clear();
            txtChieuCao.Clear();
            txtBMI.Clear();
            txt_tuanhoan.Clear();
            txt_hohap.Clear();
            txt_tieuhoa.Clear();
            txt_thantietnieu_sinhduc.Clear();
            txt_thankinh.Clear();
            txt_coxuongkhop.Clear();
            txt_taimuihong.Clear();
            txt_ranghammat.Clear();
            txt_khac.Clear();
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
       /// <summary>
       /// 
       /// </summary>
       /// <param name="trangthai">0= khởi tạo;1=Đang thực hiện;2= Đã hoàn tất</param>
       /// <returns></returns>
        private bool IsValidData(int trangthai)
        {
            if (objLuotkham != null)
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            Utility.SetMsg(lblMsg, "", false);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Cần chọn người bệnh trước khi làm Bệnh án. Vui lòng kiểm tra lại", true);
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                return false;
            }
            if (Utility.sDbnull(cboLoaiBA.SelectedValue, "-1") == "-1")
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
            if (trangthai==2)
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
                if (Utility.Int32Dbnull(txtNguoiNhanHoSo.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Người nhận hồ sơ trong danh mục hệ thống", true);
                    txtNguoiNhanHoSo.Focus();
                    return false;
                }
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
        private void cmdKetthuc_Click(object sender, EventArgs e)
        {
            LuuBenhAn(2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trangthai"></param>
        void LuuBenhAn(int trangthai)
        {
            try
            {
                isSuccess = false;
                if (!IsValidData(trangthai)) return;
                TaoPhieuKCB();
                TaoPhieuKhamNamkhoa();
                objEmrBa = TaoEmrBa();
                
                TaoPhieuTKBA();
                if (objEmrBa.IdBa > 0)
                {
                    if (!Utility.isValidSignStatus4UpdateDelete(objLuotkham, objEmrBa.IdBa, Loaiphieu_HIS.BA_SANKHOA, "Bệnh án Sản khoa"))
                        return;
                }
                objEmrBa.TrangThai = Utility.ByteDbnull(trangthai);
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
                        if (Utility.Coquyen("EMR_SUA_PHIEUKCB") && objEmrBa.IdBa > 0)
                        {
                            objPKB.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám toàn thân tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objPKB.IsNew ? newaction.Insert : newaction.Update, "EMR");
                        }
                        if (Utility.Coquyen("EMR_SUA_TKBA") && objEmrBa.IdBa > 0)
                        {
                            objTKBA.Save();
                            if (objTKBA.IsNew)
                            {

                                emrdoc.InitDocument(objTKBA.IdBenhnhan, objTKBA.MaLuotkham, Utility.Int64Dbnull(objTKBA.Id), objTKBA.NgayTtba.Value, Loaiphieu_HIS.PHIEU_TKBA, "BA_TKBA", objTKBA.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true);
                                emrdoc.Save();
                            }
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin Tổng kết BA tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objTKBA.IsNew ? newaction.Insert : newaction.Update, "EMR");
                        }
                        if (Utility.Coquyen("EMR_SUA_PHIEUKHAMPHUKHOA") && objEmrBa.IdBa > 0)
                        {
                            objPhieukhamNamkhoa.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám Sản khoa tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objTKBA.IsNew ? newaction.Insert : newaction.Update, "EMR");
                        }
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_BIA, "BANK_BANAMKHOA_BIA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO1, "BANK_BANAMKHOA_TO1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO2, "BANK_BANAMKHOA_TO2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO3, "BANK_BANAMKHOA_TO3", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO4, "BANK_BANAMKHOA_TO4", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BA_NAMKHOA, "BANK_BANAMKHOA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
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
                        MessageBox.Show("Đã thêm mới Bệnh án thành công. Nhấn Ok để kết thúc");
                        cmdXoaBenhAn.Enabled = cmdPrint.Enabled = true;
                        if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Insert);
                        m_enAct = action.Update;
                    }
                    else if (m_enAct == action.Update)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Bệnh án cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                        if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Update);
                        MessageBox.Show("Đã cập nhật Bệnh án thành công. Nhấn Ok để kết thúc");
                        m_enAct = action.Update;
                    }
                }
                EnableBA();
                //Utility.ShowMsg("Lưu thông tin thành công", "Thông báo");
                dtDataBA = SPs.EmrBaNamkhoaLaythongtin(-1, "", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                _isSuccess = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                //if (objEmrBa != null && _isSuccess)
                //{
                //    new Update(KcbLuotkham.Schema)
                //         .Set(KcbLuotkham.Columns.IdBa).EqualTo(objEmrBa.IdBa)
                //        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                //        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                //    // EmrThemBenhAn();
                //}

            }
        }    
        void TaoPhieuKhamNamkhoa()
        {
            objPhieukhamNamkhoa = new Select().From(EmrPhieukhamNamkhoa.Schema)
                .Where(EmrPhieukhamNamkhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhamNamkhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrPhieukhamNamkhoa>();
            if (objPhieukhamNamkhoa != null && objPhieukhamNamkhoa.Id > 0)
            {
                objPhieukhamNamkhoa.MarkOld();
                objPhieukhamNamkhoa.NguoiSua = globalVariables.UserName;
                objPhieukhamNamkhoa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPhieukhamNamkhoa = new EmrPhieukhamNamkhoa();
                objPhieukhamNamkhoa.IsNew = true;
                objPhieukhamNamkhoa.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPhieukhamNamkhoa.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieukhamNamkhoa.NgayKham = dtpNgayKham.Value.Date;
                objPhieukhamNamkhoa.NguoiTao = globalVariables.UserName;
                objPhieukhamNamkhoa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPhieukhamNamkhoa.IdBacsi = Utility.Int16Dbnull(txtBacsiKham.MyID, -1);

            //Nội khoa
            objPhieukhamNamkhoa.BenhlyToanthan = Utility.sDbnull(txt_benhly_toanthan.Text);


            objPhieukhamNamkhoa.QuaibiCo = opt_quaibi_co.Checked;
            objPhieukhamNamkhoa.QuaibiKhong = opt_quaibi_khong.Checked;
            objPhieukhamNamkhoa.BienchungtinhhoanCo = opt_bienchungtinhhoan_co.Checked;
            objPhieukhamNamkhoa.BienchungtinhhoanKhong = opt_bienchungtinhhoan_khong.Checked;
            objPhieukhamNamkhoa.Bienchungtinhhoan1ben = opt_bienchungtinhhoan_1ben.Checked;
            objPhieukhamNamkhoa.Bienchungtinhhoan2ben = opt_bienchungtinhhoan_2ben.Checked;

            objPhieukhamNamkhoa.BenhxahoiCo = opt_benhxahoi_co.Checked;
            objPhieukhamNamkhoa.BenhxahoiKhong = opt_benhxahoi_khong.Checked;
            objPhieukhamNamkhoa.BenhxahoiMota = opt_benhxahoi_co.Checked? Utility.sDbnull(txt_benhxahoi_mota.Text):"";

            objPhieukhamNamkhoa.UngthuCo = opt_ungthu_co.Checked;
            objPhieukhamNamkhoa.UngthuKhong = opt_ungthu_khong.Checked;

            objPhieukhamNamkhoa.TiencanlaoCo = opt_tiencanlao_co.Checked;
            objPhieukhamNamkhoa.TiencanlaoKhong = opt_tiencanlao_khong.Checked;

            objPhieukhamNamkhoa.SudungTestosteronCo = opt_sudungtestosteron_co.Checked;
            objPhieukhamNamkhoa.SudungTestosteronKhong = opt_sudungtestosteron_khong.Checked;
            objPhieukhamNamkhoa.SudungTestosteronMota = opt_sudungtestosteron_co.Checked? Utility.sDbnull(txt_testosteron_mota.Text):"";

            objPhieukhamNamkhoa.NoikhoaKhac = Utility.sDbnull(txt_noikhoa_khac.Text);
            objPhieukhamNamkhoa.Thuocdangdieutri = Utility.sDbnull(txt_thuocdangdieutri.Text);
            //Ngoại khoa
            objPhieukhamNamkhoa.ViphaucothattmtCo = opt_viphauthuatthatTMT_co.Checked;
            objPhieukhamNamkhoa.ViphaucothattmtKhong = opt_viphauthuatthatTMT_khong.Checked;

            objPhieukhamNamkhoa.HatinhoananCo = opt_hatinhoanan_co.Checked;
            objPhieukhamNamkhoa.HatinhoananKhong = opt_hatinhoanan_khong.Checked;
            objPhieukhamNamkhoa.HatinhoananMota = opt_hatinhoanan_co.Checked?Utility.sDbnull(txt_hatinhhoan_mota.Text):"";

            objPhieukhamNamkhoa.ThatongdantinhCo = opt_thatongdantinh_co.Checked;
            objPhieukhamNamkhoa.ThatongdantinhKhong = opt_thatongdantinh_khong.Checked;
            objPhieukhamNamkhoa.ThatongdantinhThoigian = opt_thatongdantinh_co.Checked? Utility.sDbnull(txt_thatongdantinh_mota.Text):"";
            objPhieukhamNamkhoa.NgoaikhoaKhac = Utility.sDbnull(txt_ngoaikhoa_khac.Text);
            //Quan hệ tình dục
            objPhieukhamNamkhoa.QuanhetinhducTansuat = Utility.sDbnull(txtTansuatquanhetinhduc.Text);
            objPhieukhamNamkhoa.RoiloancuongCo = opt_roiloancuongduong_co.Checked;
            objPhieukhamNamkhoa.RoiloancuongKhong = opt_roiloancuongduong_khong.Checked;
            objPhieukhamNamkhoa.RoiloancuongMota = opt_roiloancuongduong_co.Checked? Utility.sDbnull(txt_roiloancuongduong_mota.Text):"";

            objPhieukhamNamkhoa.XuattinhsomTruockhixamnhap = chk_xuattinh_som.Checked;
            objPhieukhamNamkhoa.XuattinhsomSaukhixamnhap = chk_xuattinh_sau.Checked;
            objPhieukhamNamkhoa.XuattinhsomKhong = chk_xuattinh_khong.Checked;

            objPhieukhamNamkhoa.CuckhoaiCo = opt_cuckhoai_co.Checked;
            objPhieukhamNamkhoa.CuckhoaiKhong = opt_cuckhoai_khong.Checked;

            objPhieukhamNamkhoa.CosudungchatboitronCo = opt_sudungchatboitron_co.Checked;
            objPhieukhamNamkhoa.CosudungchatboitronKhong = opt_sudungchatboitron_khong.Checked;
            objPhieukhamNamkhoa.CosudungchatboitronMota = opt_sudungchatboitron_co.Checked? Utility.sDbnull(txt_chatboitron_mota.Text):"";
            //Khám chuyên khoa
            objPhieukhamNamkhoa.ThetichtinhhoanPhai = Utility.sDbnull(txt_tinhoan_thetich_phai.Text);
            objPhieukhamNamkhoa.MatdotinhhoanPhai = Utility.sDbnull(txt_matdotinhoan_phai.Text);
            objPhieukhamNamkhoa.MatdotinhhoanPhaiChac = opt_matdotinhhoanphai_chac.Checked;
            objPhieukhamNamkhoa.MatdotinhhoanPhaiMem = opt_matdotinhhoanphai_mem.Checked;
            objPhieukhamNamkhoa.BemattinhoanPhai = Utility.sDbnull(txt_bemattinhhoan_phai.Text);

            objPhieukhamNamkhoa.ThetichtinhhoanTrai = Utility.sDbnull(txt_tinhoan_thetich_trai.Text);
            objPhieukhamNamkhoa.MatdotinhhoanTrai = Utility.sDbnull(txt_matdotinhoan_trai.Text);
            objPhieukhamNamkhoa.MatdotinhhoanTraiChac = opt_matdotinhhoantrai_chac.Checked;
            objPhieukhamNamkhoa.MatdotinhhoanTraiMem = opt_matdotinhhoantrai_mem.Checked;
            objPhieukhamNamkhoa.BemattinhoanTrai = Utility.sDbnull(txt_bemattinhhoan_trai.Text);
            //Mào tinh
            objPhieukhamNamkhoa.MatdomaotinhPhai = Utility.sDbnull(txt_matdomaotinh_phai.Text);
            objPhieukhamNamkhoa.MatdomaotinhPhaiChac = opt_matdomaotinhphai_chac.Checked;
            objPhieukhamNamkhoa.MatdomaotinhPhaiMem = opt_matdomaotinhphai_mem.Checked;

            objPhieukhamNamkhoa.MatdomaotinhTrai = Utility.sDbnull(txt_matdomaotinh_trai.Text);
            objPhieukhamNamkhoa.MatdomaotinhTraiChac = opt_matdomaotinhtrai_chac.Checked;
            objPhieukhamNamkhoa.MatdomaotinhTraiMem = opt_matdomaotinhtrai_mem.Checked;
            //Mào tinh Nang
            objPhieukhamNamkhoa.MaotinhNangphaiCo = opt_maotinh_nangphai_co.Checked;
            objPhieukhamNamkhoa.MaotinhNangphaiKhong = opt_maotinh_nangphai_khong.Checked;
            objPhieukhamNamkhoa.MaotinhNangphaiKhongxacdinh = opt_maotinh_nangphai_khongxacdinh.Checked;

            objPhieukhamNamkhoa.MaotinhNangtraiCo = opt_maotinh_nangtrai_co.Checked;
            objPhieukhamNamkhoa.MaotinhNangtraiKhong = opt_maotinh_nangtrai_khong.Checked;
            objPhieukhamNamkhoa.MaotinhNangtraiKhongxacdinh = opt_maotinh_nangtrai_khongxacdinh.Checked;
            //Ống dẫn tinh đoạn trong bầu
            objPhieukhamNamkhoa.OngdantinhPhaiCo = opt_ongdantinhdoantrongbauphai_co.Checked;
            objPhieukhamNamkhoa.OngdantinhPhaiKhong = opt_ongdantinhdoantrongbauphai_khong.Checked;
            objPhieukhamNamkhoa.OngdantinhPhaiKhongro = opt_ongdantinhdoantrongbauphai_khongro.Checked;

            objPhieukhamNamkhoa.OngdantinhTraiCo = opt_ongdantinhdoantrongbautrai_co.Checked;
            objPhieukhamNamkhoa.OngdantinhTraiKhong = opt_ongdantinhdoantrongbautrai_khong.Checked;
            objPhieukhamNamkhoa.OngdantinhTraiKhongro = opt_ongdantinhdoantrongbautrai_khongro.Checked;
            //Tĩnh mạch thừng tinh
            objPhieukhamNamkhoa.TinhmachthungtingPhaiBinhthuong = chk_tinhmachthungtinhphai_binhthuong.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGianphai1 = opt_tinhmachthungtinh_gianphai_1.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGianphai2 = opt_tinhmachthungtinh_gianphai_2.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGianphai3 = opt_tinhmachthungtinh_gianphai_3.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingTraiBinhthuong = chk_tinhmachthungtinhtrai_binhthuong.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGiantrai1 = opt_tinhmachthungtinh_giantrai_1.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGiantrai2 = opt_tinhmachthungtinh_giantrai_2.Checked;
            objPhieukhamNamkhoa.TinhmachthungtingGiantrai3 = opt_tinhmachthungtinh_giantrai_3.Checked;
            //Đặc điểm sinh dục thứ phát

            objPhieukhamNamkhoa.PhanbocoBinhthuong = opt_phanboco_binhthuong.Checked;
            objPhieukhamNamkhoa.PhanbocoBatthuong = opt_phanboco_batthuong.Checked;

            objPhieukhamNamkhoa.PhanboMo = Utility.sDbnull(txt_phanbomo.Text);
            objPhieukhamNamkhoa.PhanboLongmu = Utility.sDbnull(txt_longmu.Text);
            objPhieukhamNamkhoa.PhanboChi = Utility.sDbnull(txt_chi.Text);
            //Chức năng sống
            objPhieukhamNamkhoa.HuyetAp = txtha.Text;
            objPhieukhamNamkhoa.NhietDo = txtNhietDo.Text;
            objPhieukhamNamkhoa.Mach = Utility.sDbnull(txtMach.Text);
            objPhieukhamNamkhoa.NhịpTho = Utility.sDbnull(txtNhipTho.Text);
            objPhieukhamNamkhoa.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
            objPhieukhamNamkhoa.CanNang = Utility.sDbnull(txtCanNang.Text);
            objPhieukhamNamkhoa.Bmi = Utility.sDbnull(txtBMI.Text);
        }
        void TaoPhieuTKBA()
        {
             objTKBA = new Select().From(EmrTongketBenhan.Schema).Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrTongketBenhan>();
            if (objTKBA == null) objTKBA = new EmrTongketBenhan();
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
            objTKBA.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
            objTKBA.MaNguoinhanhoso = txtNguoiNhanHoSo.MyCode;
            objTKBA.IdGiamdoc = Utility.Int16Dbnull(txtGDBV.MyID);
            objTKBA.MaGiamdoc = txtGDBV.MyCode;
            objTKBA.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
            objTKBA.MaBacsiDieutri = txtBSDieuTri.MyCode;
            objTKBA.IdTruongkhoadieutri = Utility.Int16Dbnull(txtTruongkhoa.MyID);
            objTKBA.MaTruongkhoadieutri = txtTruongkhoa.MyCode;
        }
        void TaoPhieuKCB()
        {
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrPhieukhambenh>();
            //Phiếu khám bệnh toàn thân
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
                objPKB.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPKB.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPKB.NgayKham = dtpNgayKham.Value.Date;
                objPKB.NguoiTao = globalVariables.UserName;
                objPKB.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPKB.Hach = Utility.sDbnull(txt_hach.Text);
            objPKB.ToanThan = Utility.sDbnull(txt_toanthan.Text);
            objPKB.Tuanhoan = Utility.sDbnull(txt_tuanhoan.Text);
            objPKB.Hohap = Utility.sDbnull(txt_hohap.Text);
            objPKB.Tieuhoa = Utility.sDbnull(txt_tieuhoa.Text);
            objPKB.Thankinh = Utility.sDbnull(txt_thankinh.Text);
            objPKB.Coxuongkhop = Utility.sDbnull(txt_coxuongkhop.Text);
            objPKB.Thantietnieusinhduc = Utility.sDbnull(txt_thantietnieu_sinhduc.Text);
            objPKB.Taimuihong = Utility.sDbnull(txt_taimuihong.Text);
            objPKB.Ranghammat = Utility.sDbnull(txt_ranghammat.Text);
            objPKB.Noitietdinhduongbenhlykhac = Utility.sDbnull(txt_khac.Text);

        }
        void EnableBA()
        {
            cboLoaiBA.Enabled = txtIDBenhAn.Enabled=cmdKhoitaoBA.Enabled= m_enAct == action.Insert;
            if (objEmrBa != null && objEmrBa.LoaiBa != Utility.sDbnull(cboLoaiBA.SelectedValue))
            {
                ThongbaoSaiBenhAn(objEmrBa);
                cmdPrint.Enabled = cmdKetthucBA.Enabled = cmdXoaBenhAn.Enabled = false;
            }
        }
        void ThongbaoSaiBenhAn(EmrBaNamkhoa objEmrBa)
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
        private EmrBaNamkhoa TaoEmrBa()
        {
            if (objEmrBa == null) objEmrBa = new EmrBaNamkhoa();
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
                    objEmrBa.MaBa = Utility.sDbnull(txtMaBenhAn.Text);
                    objEmrBa.NguoiTao = globalVariables.UserName;
                    objEmrBa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                objEmrBa.NgaylamBa = dtpNgayBA.Value;
                objEmrBa.TongketbaNgay = dtpNgayTKBA.Value;
                objEmrBa.LoaiBa = cboLoaiBA.SelectedValue.ToString();
                if (dtkhoanhapvienCoGiuong.Rows.Count > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_LAYKHOANOITRU_COGIUONG", "0", false) == "1")
                {
                    objEmrBa.Khoa = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_khoanoitru"], "");
                    objEmrBa.Giuong = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_giuong"], "");
                    objEmrBa.Buong = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_buong"], "");
                }
                else if (dtkhoanhapvien.Rows.Count > 0)
                {
                    objEmrBa.Khoa = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_khoanoitru"], "");
                    objEmrBa.Giuong = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_giuong"], "");
                    objEmrBa.Buong = Utility.sDbnull(dtkhoanhapvien.Rows[0]["ten_buong"], "");
                }
                else
                {
                    //REM lại vì đây là khoa nhập viện hoặc khoa nhập viện có nằm giường

                }
                //objEmrBa.BenhNgoaiKhoa = Utility.sDbnull(txtBenhNgoai_Khoa.Text);
                objEmrBa.MaCoso = objLuotkham.MaCoso;
                objEmrBa.IdBenhnhan = objLuotkham.IdBenhnhan;
                objEmrBa.TenBenhnhan = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                objEmrBa.MaLuotkham = objLuotkham.MaLuotkham;
                objEmrBa.MaYte = objLuotkham.MaYte;
                objEmrBa.NgaySinh = DateTime.ParseExact(Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ngay_sinh"], DateTime.Now.ToString("yyyyMMdd")), "yyyyMMdd", CultureInfo.InvariantCulture);
                objEmrBa.MaGioitinh = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["id_gioitinh"], "0") == "0" ? "M" : "F";
                objEmrBa.GioiTinh = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["gioi_tinh"], "");
                objEmrBa.Tuoi = Utility.ByteDbnull(dt_ThongtinNguoibenh.Rows[0]["Tuoi"], "0");
                objEmrBa.LoaiTuoi = (byte)objLuotkham.LoaiTuoi;


                objEmrBa.MaNghenghiep = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["nghe_nghiep"], "");
                objEmrBa.TenNghenghiep = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_nghenghiep"], "");
                objEmrBa.MaDantoc = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["dan_toc"], "");
                objEmrBa.TenDantoc = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_dantoc"], "");
                objEmrBa.MaTongiao = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ton_giao"], "");
                objEmrBa.TenTongiao = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_tongiao"], "");
                objEmrBa.MaQuocgia = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ma_quocgia"], "VN");
                objEmrBa.TenQuocgia = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_quocgia"], "");
                objEmrBa.NgoaiKieu = (Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ma_quocgia"], "VN") == "VN" ? 0 : 1) == 1;

                objEmrBa.DiachiLienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["diachi_lienhe"], "");
                objEmrBa.DienthoaiLienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["dienthoai_lienhe"], "");
                objEmrBa.NguoiLienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["nguoi_lienhe"], "");
                objEmrBa.CmtNguoilienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["CMT_nguoilienhe"], "");
                objEmrBa.DiaChi = objLuotkham.DiaChi;
                objEmrBa.MaTinhtp = objLuotkham.MaTinhtp;
                objEmrBa.TenTinhtp = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_tinhtp"], "");
                objEmrBa.MaQuanhuyen = objLuotkham.MaQuanhuyen;
                objEmrBa.TenQuanhuyen = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_quanhuyen"], "");
                objEmrBa.MaXaphuong = objLuotkham.MaXaphuong;
                objEmrBa.TenXaphuong = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_xaphuong"], "");
                objEmrBa.MaCoquan = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["co_quan"], "");
                objEmrBa.TenCoquan = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_coquan"], "");
                objEmrBa.MatheBhyt = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["mathe_bhyt"], "");
                objEmrBa.MaDoituong = Utility.ByteDbnull(objLuotkham.IdDoituongKcb);
                objEmrBa.TenDoituong = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_doituong_kcb"], "");

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
                objEmrBa.DienThoai = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["dien_thoai"], "");
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
                    objEmrBa.ChuyenvienNoichuyenden = Utility.sDbnull(txtQLNBChuyenVienNoiChuyenDen.Text);
                }
                if (objPhieuRavien != null)
                {
                    objEmrBa.RavienRavien = chkQLNBRaVienRavien.Checked;
                    objEmrBa.RavienXinve = chkQLNBRavienXinVe.Checked;
                    objEmrBa.RavienBove = chkQLNBRavienBoVe.Checked;
                    objEmrBa.RavienDuave = chkQLNBRavienDuaVe.Checked;
                    objEmrBa.ChuyenvienNoichuyenden = Utility.sDbnull(txtQLNBChuyenVienNoiChuyenDen.Text);
                    objEmrBa.RavienMaBenhchinh = txtCDRavienMaBenhChinh.Text;
                    objEmrBa.RavienMaBenhphu = txtCDRavienMaBenhKemTheo.Text;
                    objEmrBa.RavienTenBenhchinh = txtCDRavienTenBenhKemTheo.Text;
                    objEmrBa.RavienTenBenhphu = txtCDRavienMaBenhKemTheo.Text;
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
                    objEmrBa.TinhtrangravienThoigianTuvong = objPhieuRavien.TuvongNgay;
                    objEmrBa.TinhtrangravienLydotuvongDobenh = chkttrvDoBenh.Checked;
                    objEmrBa.TinhtrangravienLydotuvongDotaibiendieutri = chkttrvDoTaiBien.Checked;
                    objEmrBa.TinhtrangravienLydotuvongKhac = chkttrvTrong72hVaovien.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongTrong24h = chkttrvTrong24GioVaoVien.Checked;
                    objEmrBa.TinhtrangravienThoigiantuvongSau24h = chkttrvTrong48GioVaoVien.Checked;
                    objEmrBa.TinhtrangravienNguyennhantuvong = Utility.sDbnull(txtTTRVNguyenNhanChinhTuVong.Text);
                    //objEmrBa.TinhtrangravienMaNguyennhantuvong = Utility.sDbnull(txtTTRVNguyenNhanChinhTuVong.Text);
                    objEmrBa.TinhtrangravienKhamnghiemtuthi = chkTTRVKhamNgiemTuThi.Checked;
                    objEmrBa.TinhtrangravienChandoangiauphaututhi = Utility.sDbnull(txtTTRVChandoanGiaiphauTuthi.Text);
                    //objEmrBa.TinhtrangravienChandoangiauphaututhi
                }


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
                objEmrBa.ChandoanTruocphauthuat = Utility.sDbnull(txt_chandoan_truocphauthuat.Text);
                objEmrBa.MaChandoanTruocphauthuat = Utility.sDbnull(txt_chandoan_truocphauthuat.MyCode);

                objEmrBa.ChandoanSauphauthuat = Utility.sDbnull(txt_chandoan_sauphauthuat.Text);
                objEmrBa.MaChandoanSauphauthuat = Utility.sDbnull(txt_chandoan_sauphauthuat.MyCode);

                objEmrBa.CdTaibien = chkCDTaiBien.Checked;
                objEmrBa.CdBienchung = chkCDBienChung.Checked;


                objEmrBa.VaovienLydovaovien = txtBenhAnLyDoNhapVien.Text;
                objEmrBa.VaovienVaongaythucuabenh = Utility.ByteDbnull(txtBenhAnVaoNgayThu.Text);
                objEmrBa.HoibenhQuatrinhbenhly = Utility.sDbnull(txtBenhAnQuaTrinhBenhLy.Text);
                objEmrBa.HoibenhTiensubanthan = Utility.sDbnull(txtBenhAnTiensuBanthan.Text);
                if (objPhieukhamNamkhoa != null)//Thông tin khám Sản khoa
                {
                    //Nội khoa
                    objEmrBa.BenhlyToanthan = objPhieukhamNamkhoa.BenhlyToanthan;
                    objEmrBa.QuaibiCo = objPhieukhamNamkhoa.QuaibiCo;
                    objEmrBa.QuaibiKhong = objPhieukhamNamkhoa.QuaibiKhong;
                    objEmrBa.BienchungtinhhoanCo = objPhieukhamNamkhoa.BienchungtinhhoanCo;
                    objEmrBa.BienchungtinhhoanKhong = objPhieukhamNamkhoa.BienchungtinhhoanKhong;
                    objEmrBa.Bienchungtinhhoan1ben = objPhieukhamNamkhoa.Bienchungtinhhoan1ben;
                    objEmrBa.Bienchungtinhhoan2ben = objPhieukhamNamkhoa.Bienchungtinhhoan2ben;

                    objEmrBa.BenhxahoiCo = objPhieukhamNamkhoa.BenhxahoiCo;
                    objEmrBa.BenhxahoiKhong = objPhieukhamNamkhoa.BenhxahoiKhong;
                    objEmrBa.BenhxahoiMota = objPhieukhamNamkhoa.BenhxahoiMota;

                    objEmrBa.UngthuCo = objPhieukhamNamkhoa.UngthuCo;
                    objEmrBa.UngthuKhong = objPhieukhamNamkhoa.UngthuKhong;

                    objEmrBa.TiencanlaoCo = objPhieukhamNamkhoa.TiencanlaoCo;
                    objEmrBa.TiencanlaoKhong = objPhieukhamNamkhoa.TiencanlaoKhong;

                    objEmrBa.SudungTestosteronCo = objPhieukhamNamkhoa.SudungTestosteronCo;
                    objEmrBa.SudungTestosteronKhong = objPhieukhamNamkhoa.SudungTestosteronKhong;
                    objEmrBa.SudungTestosteronMota = objPhieukhamNamkhoa.SudungTestosteronMota;

                    objEmrBa.NoikhoaKhac = objPhieukhamNamkhoa.NoikhoaKhac;
                    objEmrBa.Thuocdangdieutri = objPhieukhamNamkhoa.Thuocdangdieutri;
                    //Ngoại khoa
                    objEmrBa.ViphaucothattmtCo = objPhieukhamNamkhoa.ViphaucothattmtCo;
                    objEmrBa.ViphaucothattmtKhong = objPhieukhamNamkhoa.ViphaucothattmtKhong;

                    objEmrBa.HatinhoananCo = objPhieukhamNamkhoa.HatinhoananCo;
                    objEmrBa.HatinhoananKhong = objPhieukhamNamkhoa.HatinhoananKhong;
                    objEmrBa.HatinhoananMota = objPhieukhamNamkhoa.HatinhoananMota;

                    objEmrBa.ThatongdantinhCo = objPhieukhamNamkhoa.ThatongdantinhCo;
                    objEmrBa.ThatongdantinhKhong = objPhieukhamNamkhoa.ThatongdantinhKhong;
                    objEmrBa.ThatongdantinhThoigian = objPhieukhamNamkhoa.ThatongdantinhThoigian;
                    objEmrBa.NgoaikhoaKhac = objPhieukhamNamkhoa.NgoaikhoaKhac;
                    //Quan hệ tình dục
                    objEmrBa.QuanhetinhducTansuat = objPhieukhamNamkhoa.QuanhetinhducTansuat;
                    objEmrBa.RoiloancuongCo = objPhieukhamNamkhoa.RoiloancuongCo;
                    objEmrBa.RoiloancuongKhong = objPhieukhamNamkhoa.RoiloancuongKhong;
                    objEmrBa.RoiloancuongMota = objPhieukhamNamkhoa.RoiloancuongMota;

                    objEmrBa.XuattinhsomTruockhixamnhap = objPhieukhamNamkhoa.XuattinhsomTruockhixamnhap;
                    objEmrBa.XuattinhsomSaukhixamnhap = objPhieukhamNamkhoa.XuattinhsomSaukhixamnhap;
                    objEmrBa.XuattinhsomKhong = objPhieukhamNamkhoa.XuattinhsomKhong;

                    objEmrBa.CuckhoaiCo = objPhieukhamNamkhoa.CuckhoaiCo;
                    objEmrBa.CuckhoaiKhong = objPhieukhamNamkhoa.CuckhoaiKhong;

                    objEmrBa.CosudungchatboitronCo = objPhieukhamNamkhoa.CosudungchatboitronCo;
                    objEmrBa.CosudungchatboitronKhong = objPhieukhamNamkhoa.CosudungchatboitronKhong;
                    objEmrBa.CosudungchatboitronMota = objPhieukhamNamkhoa.CosudungchatboitronMota;
                    //Khám chuyên khoa
                    objEmrBa.ThetichtinhhoanPhai = objPhieukhamNamkhoa.ThetichtinhhoanPhai;
                    objEmrBa.MatdotinhhoanPhai = objPhieukhamNamkhoa.MatdotinhhoanPhai;
                    objEmrBa.MatdotinhhoanPhaiChac = objPhieukhamNamkhoa.MatdotinhhoanPhaiChac;
                    objEmrBa.MatdotinhhoanPhaiMem = objPhieukhamNamkhoa.MatdotinhhoanPhaiMem;
                    objEmrBa.BemattinhoanPhai = objPhieukhamNamkhoa.BemattinhoanPhai;

                    objEmrBa.ThetichtinhhoanTrai = objPhieukhamNamkhoa.ThetichtinhhoanTrai;
                    objEmrBa.MatdotinhhoanTrai = objPhieukhamNamkhoa.MatdotinhhoanTrai;
                    objEmrBa.MatdotinhhoanTraiChac = objPhieukhamNamkhoa.MatdotinhhoanTraiChac;
                    objEmrBa.MatdotinhhoanTraiMem = objPhieukhamNamkhoa.MatdotinhhoanTraiMem;
                    objEmrBa.BemattinhoanTrai = objPhieukhamNamkhoa.BemattinhoanTrai;
                    //Mào tinh
                    objEmrBa.MatdomaotinhPhai = objPhieukhamNamkhoa.MatdomaotinhPhai;
                    objEmrBa.MatdomaotinhPhaiChac = objPhieukhamNamkhoa.MatdomaotinhPhaiChac;
                    objEmrBa.MatdomaotinhPhaiMem = objPhieukhamNamkhoa.MatdomaotinhPhaiMem;

                    objEmrBa.MatdomaotinhTrai = objPhieukhamNamkhoa.MatdomaotinhTrai;
                    objEmrBa.MatdomaotinhTraiChac = objPhieukhamNamkhoa.MatdomaotinhTraiChac;
                    objEmrBa.MatdomaotinhTraiMem = objPhieukhamNamkhoa.MatdomaotinhTraiMem;
                    //Mào tinh Nang
                    objEmrBa.MaotinhNangphaiCo = objPhieukhamNamkhoa.MaotinhNangphaiCo;
                    objEmrBa.MaotinhNangphaiKhong = objPhieukhamNamkhoa.MaotinhNangphaiKhong;
                    objEmrBa.MaotinhNangphaiKhongxacdinh = objPhieukhamNamkhoa.MaotinhNangphaiKhongxacdinh;

                    objEmrBa.MaotinhNangtraiCo = objPhieukhamNamkhoa.MaotinhNangtraiCo;
                    objEmrBa.MaotinhNangtraiKhong = objPhieukhamNamkhoa.MaotinhNangtraiKhong;
                    objEmrBa.MaotinhNangtraiKhongxacdinh = objPhieukhamNamkhoa.MaotinhNangtraiKhongxacdinh;
                    //Ống dẫn tinh đoạn trong bầu
                    objEmrBa.OngdantinhPhaiCo = objPhieukhamNamkhoa.OngdantinhPhaiCo;
                    objEmrBa.OngdantinhPhaiKhong = objPhieukhamNamkhoa.OngdantinhPhaiKhong;
                    objEmrBa.OngdantinhPhaiKhongro = objPhieukhamNamkhoa.OngdantinhPhaiKhongro;

                    objEmrBa.OngdantinhTraiCo = objPhieukhamNamkhoa.OngdantinhTraiCo;
                    objEmrBa.OngdantinhTraiKhong = objPhieukhamNamkhoa.OngdantinhTraiKhong;
                    objEmrBa.OngdantinhTraiKhongro = objPhieukhamNamkhoa.OngdantinhTraiKhongro;
                    //Tĩnh mạch thừng tinh
                    objEmrBa.TinhmachthungtingPhaiBinhthuong = objPhieukhamNamkhoa.TinhmachthungtingPhaiBinhthuong;
                    objEmrBa.TinhmachthungtingGianphai1 = objPhieukhamNamkhoa.TinhmachthungtingGianphai1;
                    objEmrBa.TinhmachthungtingGianphai2 = objPhieukhamNamkhoa.TinhmachthungtingGianphai2;
                    objEmrBa.TinhmachthungtingGianphai3 = objPhieukhamNamkhoa.TinhmachthungtingGianphai3;
                    objEmrBa.TinhmachthungtingTraiBinhthuong = objPhieukhamNamkhoa.TinhmachthungtingTraiBinhthuong;
                    objEmrBa.TinhmachthungtingGiantrai1 = objPhieukhamNamkhoa.TinhmachthungtingGiantrai1;
                    objEmrBa.TinhmachthungtingGiantrai2 = objPhieukhamNamkhoa.TinhmachthungtingGiantrai2;
                    objEmrBa.TinhmachthungtingGiantrai3 = objPhieukhamNamkhoa.TinhmachthungtingGiantrai3;
                    //Đặc điểm sinh dục thứ phát

                    objEmrBa.PhanbocoBinhthuong = objPhieukhamNamkhoa.PhanbocoBinhthuong;
                    objEmrBa.PhanbocoBatthuong = objPhieukhamNamkhoa.PhanbocoBatthuong;

                    objEmrBa.PhanboMo = objPhieukhamNamkhoa.PhanboMo;
                    objEmrBa.PhanboLongmu = objPhieukhamNamkhoa.PhanboLongmu;
                    objEmrBa.PhanboChi = objPhieukhamNamkhoa.PhanboChi;
                   

                }
                else
                {
                    //Nội khoa
                    objEmrBa.BenhlyToanthan = Utility.sDbnull(txt_benhly_toanthan.Text);
                    objEmrBa.QuaibiCo = opt_quaibi_co.Checked;
                    objEmrBa.QuaibiKhong = opt_quaibi_khong.Checked;
                    objEmrBa.BienchungtinhhoanCo = opt_bienchungtinhhoan_co.Checked;
                    objEmrBa.BienchungtinhhoanKhong = opt_bienchungtinhhoan_khong.Checked;
                    objEmrBa.Bienchungtinhhoan1ben = opt_bienchungtinhhoan_1ben.Checked;
                    objEmrBa.Bienchungtinhhoan2ben = opt_bienchungtinhhoan_2ben.Checked;

                    objEmrBa.BenhxahoiCo = opt_benhxahoi_co.Checked;
                    objEmrBa.BenhxahoiKhong = opt_benhxahoi_khong.Checked;
                    objEmrBa.BenhxahoiMota = opt_benhxahoi_co.Checked?Utility.sDbnull(txt_benhxahoi_mota.Text):"";

                    objEmrBa.UngthuCo = opt_ungthu_co.Checked;
                    objEmrBa.UngthuKhong = opt_ungthu_khong.Checked;

                    objEmrBa.TiencanlaoCo = opt_tiencanlao_co.Checked;
                    objEmrBa.TiencanlaoKhong = opt_tiencanlao_khong.Checked;

                    objEmrBa.SudungTestosteronCo = opt_sudungtestosteron_co.Checked;
                    objEmrBa.SudungTestosteronKhong = opt_sudungtestosteron_khong.Checked;
                    objEmrBa.SudungTestosteronMota = opt_sudungtestosteron_co.Checked? Utility.sDbnull(txt_testosteron_mota.Text):"";

                    objEmrBa.NoikhoaKhac = Utility.sDbnull(txt_noikhoa_khac.Text);
                    objEmrBa.Thuocdangdieutri = Utility.sDbnull(txt_thuocdangdieutri.Text);
                    //Ngoại khoa
                    objEmrBa.ViphaucothattmtCo = opt_viphauthuatthatTMT_co.Checked;
                    objEmrBa.ViphaucothattmtKhong = opt_viphauthuatthatTMT_khong.Checked;

                    objEmrBa.HatinhoananCo = opt_hatinhoanan_co.Checked;
                    objEmrBa.HatinhoananKhong = opt_hatinhoanan_khong.Checked;
                    objEmrBa.HatinhoananMota = opt_hatinhoanan_co.Checked?Utility.sDbnull(txt_hatinhhoan_mota.Text):"";

                    objEmrBa.ThatongdantinhCo = opt_thatongdantinh_co.Checked;
                    objEmrBa.ThatongdantinhKhong = opt_thatongdantinh_khong.Checked;
                    objEmrBa.ThatongdantinhThoigian = opt_thatongdantinh_co.Checked? Utility.sDbnull(txt_thatongdantinh_mota.Text):"";
                    objEmrBa.NgoaikhoaKhac = Utility.sDbnull(txt_ngoaikhoa_khac.Text);
                    //Quan hệ tình dục
                    objEmrBa.QuanhetinhducTansuat = Utility.sDbnull(txtTansuatquanhetinhduc.Text);
                    objEmrBa.RoiloancuongCo = opt_roiloancuongduong_co.Checked;
                    objEmrBa.RoiloancuongKhong = opt_roiloancuongduong_khong.Checked;
                    objEmrBa.RoiloancuongMota = opt_roiloancuongduong_co.Checked? Utility.sDbnull(txt_roiloancuongduong_mota.Text):"";

                    objEmrBa.XuattinhsomTruockhixamnhap = chk_xuattinh_som.Checked;
                    objEmrBa.XuattinhsomSaukhixamnhap = chk_xuattinh_sau.Checked;
                    objEmrBa.XuattinhsomKhong = chk_xuattinh_khong.Checked;

                    objEmrBa.CuckhoaiCo = opt_cuckhoai_co.Checked;
                    objEmrBa.CuckhoaiKhong = opt_cuckhoai_khong.Checked;

                    objEmrBa.CosudungchatboitronCo = opt_sudungchatboitron_co.Checked;
                    objEmrBa.CosudungchatboitronKhong = opt_sudungchatboitron_khong.Checked;
                    objEmrBa.CosudungchatboitronMota = opt_sudungchatboitron_co.Checked?Utility.sDbnull(txt_chatboitron_mota.Text):"";
                    //Khám chuyên khoa
                    objEmrBa.ThetichtinhhoanPhai = Utility.sDbnull(txt_tinhoan_thetich_phai.Text);
                    objEmrBa.MatdotinhhoanPhai = Utility.sDbnull(txt_matdotinhoan_phai.Text);
                    objEmrBa.MatdotinhhoanPhaiChac = opt_matdotinhhoanphai_chac.Checked;
                    objEmrBa.MatdotinhhoanPhaiMem = opt_matdotinhhoanphai_mem.Checked;
                    objEmrBa.BemattinhoanPhai = Utility.sDbnull(txt_bemattinhhoan_phai.Text);

                    objEmrBa.ThetichtinhhoanTrai = Utility.sDbnull(txt_tinhoan_thetich_trai.Text);
                    objEmrBa.MatdotinhhoanTrai = Utility.sDbnull(txt_matdotinhoan_trai.Text);
                    objEmrBa.MatdotinhhoanTraiChac = opt_matdotinhhoantrai_chac.Checked;
                    objEmrBa.MatdotinhhoanTraiMem = opt_matdomaotinhtrai_mem.Checked;
                    objEmrBa.BemattinhoanTrai = Utility.sDbnull(txt_bemattinhhoan_trai.Text);
                    //Mào tinh
                    objEmrBa.MatdomaotinhPhai = Utility.sDbnull(txt_matdomaotinh_phai.Text);
                    objEmrBa.MatdomaotinhPhaiChac = opt_matdomaotinhphai_chac.Checked;
                    objEmrBa.MatdomaotinhPhaiMem = opt_matdomaotinhphai_mem.Checked;

                    objEmrBa.MatdomaotinhTrai = Utility.sDbnull(txt_matdomaotinh_trai.Text);
                    objEmrBa.MatdomaotinhTraiChac = opt_matdomaotinhtrai_chac.Checked;
                    objEmrBa.MatdomaotinhTraiMem = opt_matdomaotinhtrai_mem.Checked;
                    //Mào tinh Nang
                    objEmrBa.MaotinhNangphaiCo = opt_maotinh_nangphai_co.Checked;
                    objEmrBa.MaotinhNangphaiKhong = opt_maotinh_nangphai_khong.Checked;
                    objEmrBa.MaotinhNangphaiKhongxacdinh = opt_maotinh_nangphai_khongxacdinh.Checked;

                    objEmrBa.MaotinhNangtraiCo = opt_maotinh_nangtrai_co.Checked;
                    objEmrBa.MaotinhNangtraiKhong = opt_maotinh_nangtrai_khong.Checked;
                    objEmrBa.MaotinhNangtraiKhongxacdinh = opt_maotinh_nangtrai_khongxacdinh.Checked;
                    //Ống dẫn tinh đoạn trong bầu
                    objEmrBa.OngdantinhPhaiCo = opt_ongdantinhdoantrongbauphai_co.Checked;
                    objEmrBa.OngdantinhPhaiKhong = opt_ongdantinhdoantrongbauphai_khong.Checked;
                    objEmrBa.OngdantinhPhaiKhongro = opt_ongdantinhdoantrongbauphai_khongro.Checked;

                    objEmrBa.OngdantinhTraiCo = opt_ongdantinhdoantrongbautrai_co.Checked;
                    objEmrBa.OngdantinhTraiKhong = opt_ongdantinhdoantrongbautrai_khong.Checked;
                    objEmrBa.OngdantinhTraiKhongro = opt_ongdantinhdoantrongbautrai_khongro.Checked;
                    //Tĩnh mạch thừng tinh
                    objEmrBa.TinhmachthungtingPhaiBinhthuong = chk_tinhmachthungtinhphai_binhthuong.Checked;
                    objEmrBa.TinhmachthungtingGianphai1 = opt_tinhmachthungtinh_gianphai_1.Checked;
                    objEmrBa.TinhmachthungtingGianphai2 = opt_tinhmachthungtinh_gianphai_2.Checked;
                    objEmrBa.TinhmachthungtingGianphai3 = opt_tinhmachthungtinh_gianphai_3.Checked;
                    objEmrBa.TinhmachthungtingTraiBinhthuong = chk_tinhmachthungtinhtrai_binhthuong.Checked;
                    objEmrBa.TinhmachthungtingGiantrai1 = opt_tinhmachthungtinh_giantrai_1.Checked;
                    objEmrBa.TinhmachthungtingGiantrai2 = opt_tinhmachthungtinh_giantrai_2.Checked;
                    objEmrBa.TinhmachthungtingGiantrai3 = opt_tinhmachthungtinh_giantrai_3.Checked;
                    //Đặc điểm sinh dục thứ phát

                    objEmrBa.PhanbocoBinhthuong = opt_phanboco_binhthuong.Checked;
                    objEmrBa.PhanbocoBatthuong = opt_phanboco_batthuong.Checked;

                    objEmrBa.PhanboMo = Utility.sDbnull(txt_phanbomo.Text);
                    objEmrBa.PhanboLongmu = Utility.sDbnull(txt_longmu.Text);
                    objEmrBa.PhanboChi = Utility.sDbnull(txt_chi.Text);
                    
                }    

                objEmrBa.HoibenhTiensugiadinh = txtBenhAnGiaDinh.Text;

                objEmrBa.KbMach = txtMach.Text;
                objEmrBa.KbNhietdo = txtNhietDo.Text;
                objEmrBa.KbHuyetap = txtha.Text;
                objEmrBa.KbNhiptho = txtNhipTho.Text;
                objEmrBa.KbCannang = txtCanNang.Text;
                objEmrBa.KbChieucao = txtChieuCao.Text;
                tinhBMI();
                //Thông tin khám bệnh
                objEmrBa.KbBmi = Utility.sDbnull(txtBMI.Text, 0);
                objEmrBa.KhambenhToanthan = Utility.sDbnull(txt_toanthan.Text);
                objEmrBa.KhambenhTuanhoan = Utility.sDbnull(txt_tuanhoan.Text);
                objEmrBa.KhambenhHohap = Utility.sDbnull(txt_hohap.Text);
                objEmrBa.KhambenhTieuhoa = Utility.sDbnull(txt_tieuhoa.Text);
                objEmrBa.KhambenhThantietnieusinhduc = Utility.sDbnull(txt_thantietnieu_sinhduc.Text);
                objEmrBa.KhambenhThankinh = Utility.sDbnull(txt_thankinh.Text);
                objEmrBa.KhambenhCoxuongkhop = Utility.sDbnull(txt_coxuongkhop.Text);
                objEmrBa.KhambenhTaimuihong = Utility.sDbnull(txt_taimuihong.Text);
                objEmrBa.KhambenhRanghammat = Utility.sDbnull(txt_ranghammat.Text);
                objEmrBa.KhambenhNoitietDinhduongBenhlykhac = Utility.sDbnull(txt_khac.Text);

                //
                objEmrBa.KhambenhXetnghiemClsCanlam = Utility.sDbnull(txtBenhAnCacXetNghiem.Text);
                objEmrBa.KhambenhTomtatbenhan = Utility.sDbnull(txtBenhAnTomTatBenhAn.Text);
                objEmrBa.CdKhivaokhoadieutriBenhchinh = Utility.sDbnull(txtBenhAnBenhChinh.Text);
                objEmrBa.CdKhivaokhoadieutriBenhphu = Utility.sDbnull(txtBenhAnBenhKemTheo.Text);
                objEmrBa.CdKhivaokhoadieutriPhanbiet = Utility.sDbnull(txtBenhAnPhanBiet.Text);

                objEmrBa.KhambenhTienluong = Utility.sDbnull(txtBenhAnTienLuong.Text);
                objEmrBa.KhambenhHuongdieutri = Utility.sDbnull(txtBenhAnHuongDieuTri.Text);

                objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang = Utility.sDbnull(txtTKBAQuaTrinhBenhLy.Text);
                objEmrBa.TongketbaTomtatKqcls = Utility.sDbnull(txtTKBATTomTatKetQua.Text);
                objEmrBa.TongketbaPhuongphapdieutri = Utility.sDbnull(txtTKBAPhuongPhapDieuTri.Text);
                objEmrBa.TongketbaTinhtrangNguoiravien = Utility.sDbnull(txtTKBATinhTrangRaVien.Text);
                objEmrBa.TongketbaHuongdieutritieptheo = Utility.sDbnull(txtTKBAHuongDieuTri.Text);

                objEmrBa.IdNguoigiaoHoso = Utility.Int16Dbnull(txtNguoiGiaoHoSo.MyID);
                objEmrBa.TongketbaMaNguoigiaohoso = txtNguoiGiaoHoSo.Text;
                objEmrBa.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
                objEmrBa.TongketbaMaNguoiNhanhoso = txtNguoiNhanHoSo.Text;
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

        private void frm_BenhAn_NamKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region "Xử lý multiline"
                if (tabpageTo1.ActiveControl != null)
                {
                    Control ctr = tabpageTo1.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (tabpageTo2.ActiveControl != null)
                {
                    Control ctr = tabpageTo2.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (tabpageTo3.ActiveControl != null)
                {
                    Control ctr = tabpageTo3.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (tabpageTo4.ActiveControl != null)
                {
                    Control ctr = tabpageTo4.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                #endregion
                
            }
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
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
        public action m_enAct = action.Insert;
        private void frm_BenhAn_NamKhoa_Load(object sender, EventArgs e)
        {
            try
            {
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
                ucThongtinnguoibenh_emr_basic1.Refresh();
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
        public EmrBaNamkhoa objEmrBa;
        EmrPhieukhamNamkhoa objPhieukhamNamkhoa;
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

                SqlQuery sqlQuery = new Select().From<EmrBaNamkhoa>()
                    .Where(EmrBaNamkhoa.Columns.MaLuotkham)
                    .IsEqualTo(objLuotkham.MaLuotkham)
                    .And(EmrBaNamkhoa.Columns.IdBenhnhan)
                    .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
                if (objEmrBa == null || (objEmrBa.IdBenhnhan != objLuotkham.IdBenhnhan && objEmrBa.MaLuotkham != objLuotkham.MaLuotkham))
                    objEmrBa = sqlQuery.ExecuteSingle<EmrBaNamkhoa>();
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
                    lblMakhoavao.Text = Utility.sDbnull(arrKhoanhapvien[0]["ma_khoanoitru"], "");
                    lblqlbnKhoa.Text = Utility.sDbnull(arrKhoanhapvien[0]["ten_khoanoitru"], "");
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
                txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objLuotkham.SongayDieutri);
                GetChanDoanNoitru();
                FillThongtinRavien();
                FillThongtinChuyenVien();
                FillTongketBenhAn();
                FillThongtinPTTT();
                //Trang 2
                FillThongtinNhapvien();
                FillPhieuKhamNamKhoa();
                //Trang 3
                FillPhieuKCB();

                txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
                txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
                
                if (objEmrBa != null)
                {
                    m_enAct = action.Update;
                    cboLoaiBA.SelectedIndex = Utility.GetSelectedIndex(cboLoaiBA, objEmrBa.LoaiBa);
                    maBA = objEmrBa.MaBa;
                    dtDataBA = SPs.EmrBaNamkhoaLaythongtin(-1, "", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                    DataRow dr = dtDataBA.Rows[0];
                    try
                    {
                        txtIDBenhAn.Text = Utility.sDbnull(objEmrBa.IdBa);
                        txtMaBenhAn.Text = Utility.sDbnull(objEmrBa.MaBa);
                        //txtBenhNgoai_Khoa.Text = Utility.sDbnull(objEmrBa.BenhNgoaiKhoa);
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
                        txtQLNBLanVaoVien.Text = Utility.sDbnull(objEmrBa.VaovienLanthu);
                       
                        string ICD_chinh_Name = "";
                        string ICD_chinh_Code = "";
                        string ICD_Phu_Name = "";
                        string ICD_Phu_Code = "";

                        GetChanDoanChinhPhu(objLuotkham.MabenhChinh,
                                            objLuotkham.MabenhPhu,
                                            ref ICD_chinh_Name,
                                            ref ICD_chinh_Code, ref ICD_Phu_Name,
                                            ref ICD_Phu_Code);

                        txtCDKKBCapCuu.Text = ICD_chinh_Name + ICD_Phu_Name;
                        txtCDMaKKBCapCuu.Text = ICD_chinh_Code + ICD_Phu_Code;

                       
                       
                        lblqlbnKhoa.Text = objEmrBa.VaovienTenkhoa;
                        lblMakhoavao.Text = objEmrBa.VaovienMakhoa;
                        chkQLNBTuyenTren.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyentren);
                        chkQLNBTuyenDuoi.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienTuyenduoi);
                        chkQLNBChuyenVienCK.Checked = Utility.Bool2Bool(objEmrBa.ChuyenvienKhac);
                        txtQLNBChuyenVienNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.ChuyenvienNoichuyenden);
                        if (objEmrBa.TrangThai == 2)
                        {
                            if (objEmrBa.RavienNgay.HasValue)
                                dtpRavien_ngay.Value = objEmrBa.RavienNgay.Value;
                            else
                                dtpRavien_ngay.ResetText();
                            chkQLNBRaVienRavien.Checked = Utility.Bool2Bool(objEmrBa.RavienRavien);
                            chkQLNBRavienXinVe.Checked = Utility.Bool2Bool(objEmrBa.RavienXinve);
                            chkQLNBRavienBoVe.Checked = Utility.Bool2Bool(objEmrBa.RavienBove);
                            chkQLNBRavienDuaVe.Checked = Utility.Bool2Bool(objEmrBa.RavienDuave);
                            txtQLNBTongSoNgayDieuTri.Text = Utility.sDbnull(objEmrBa.RavienTongsongayDieutri);
                            txtCDNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.CdNoichuyenden);
                            txtCDMaNoiChuyenDen.Text = Utility.sDbnull(objEmrBa.CdNoichuyendenMa);
                            txtCDKKBCapCuu.Text = Utility.sDbnull(objEmrBa.CdKkbCapcuu);
                            txtCDMaKKBCapCuu.Text = Utility.sDbnull(objEmrBa.CdKkbCapcuuMa);
                            txtCDKhiVaoDieuTri.Text = Utility.sDbnull(objEmrBa.CdKhoadieutri);
                            txtCDMaKhiVaoDieuTri.Text = Utility.sDbnull(objEmrBa.CdKhoadieutriMa);
                            txtCDRavienTenBenhChinh.Text = Utility.sDbnull(objEmrBa.RavienTenBenhchinh);
                            txtCDRavienMaBenhChinh.Text = Utility.sDbnull(objEmrBa.RavienMaBenhchinh);
                            txtCDRavienTenBenhKemTheo.Text = Utility.sDbnull(objEmrBa.RavienTenBenhphu);
                            txtCDRavienMaBenhKemTheo.Text = Utility.sDbnull(objEmrBa.RavienMaBenhphu);
                        }
                        chk_cd_dogayme.Checked = Utility.Bool2Bool(objEmrBa.CdDogayme);
                        chk_cd_dophauthuat.Checked = Utility.Bool2Bool(objEmrBa.CdPhauthuat);
                        chk_cd_donhiemkhuan.Checked = Utility.Bool2Bool(objEmrBa.CdDonhiemkhuan);
                        chk_cd_dokhac.Checked = Utility.Bool2Bool(objEmrBa.CdTaibienBienchungKhac);
                        chkCDTaiBien.Checked = Utility.Bool2Bool(objEmrBa.CdTaibien);
                        chkCDBienChung.Checked = Utility.Bool2Bool(objEmrBa.CdBienchung);
                        nmr_cd_tongsolanphauthuat.Value = Utility.Int32Dbnull(objEmrBa.CdTongsolanphauthuat);
                        nmr_cd_tongsongaydieutri_sauphauthuat.Value = Utility.Int32Dbnull(objEmrBa.CdTongsongaydieutriSauphauthuat);
                       
                        lbl_ma_chandoan_truocphauthuat.Text = Utility.sDbnull(objEmrBa.MaChandoanTruocphauthuat);
                        txt_chandoan_truocphauthuat.SetCode( Utility.sDbnull(objEmrBa.MaChandoanTruocphauthuat));
                        lbl_ma_chandoan_sauphauthuat.Text = Utility.sDbnull(objEmrBa.MaChandoanSauphauthuat);
                        txt_chandoan_sauphauthuat.SetCode(Utility.sDbnull(objEmrBa.MaChandoanSauphauthuat));
                        //Tình trạng ra viện
                        if (objEmrBa.TrangThai == 2)
                        {
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

                            txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(objEmrBa.TinhtrangravienNguyennhantuvong);
                            chkTTRVChandoanGiaiphauTuthi.Checked = Utility.Bool2Bool(objEmrBa.TinhtrangravienKhamnghiemtuthi);
                            txtTTRVChandoanGiaiphauTuthi.Text = Utility.sDbnull(objEmrBa.TinhtrangravienChandoangiauphaututhi);
                        }
                        //Tờ 2
                        txtBenhAnLyDoNhapVien._Text = Utility.sDbnull(objEmrBa.VaovienLydovaovien);// Utility.sDbnull(dr["BaLdvv"].ToString());
                        txtBenhAnVaoNgayThu.Text = Utility.sDbnull(objEmrBa.VaovienVaongaythucuabenh);
                        txtBenhAnQuaTrinhBenhLy.Text = Utility.sDbnull(objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang);// Utility.sDbnull(dr["BaQtbl"].ToString());
                        txtBenhAnTiensuBanthan.Text = Utility.sDbnull(objEmrBa.HoibenhTiensubanthan);

                        //Thông tin khám Nam khoa
                        txt_benhly_toanthan.Text = Utility.sDbnull(objEmrBa.BenhlyToanthan);

                        opt_quaibi_co.Checked = Utility.Bool2Bool(objEmrBa.QuaibiCo);
                        opt_quaibi_khong.Checked = Utility.Bool2Bool(objEmrBa.QuaibiKhong);
                        opt_bienchungtinhhoan_co.Checked = Utility.Bool2Bool(objEmrBa.BienchungtinhhoanCo);
                        opt_bienchungtinhhoan_khong.Checked = Utility.Bool2Bool(objEmrBa.BienchungtinhhoanKhong);
                        opt_bienchungtinhhoan_1ben.Checked = Utility.Bool2Bool(objEmrBa.Bienchungtinhhoan1ben);
                        opt_bienchungtinhhoan_2ben.Checked = Utility.Bool2Bool(objEmrBa.Bienchungtinhhoan2ben);
                        txt_xutri.Text = Utility.sDbnull(objEmrBa.BienchungtinhoanMota);

                        opt_benhxahoi_co.Checked = Utility.Bool2Bool(objEmrBa.BenhxahoiCo);
                        opt_benhxahoi_khong.Checked = Utility.Bool2Bool(objEmrBa.BenhxahoiKhong);
                        txt_benhxahoi_mota.Text = Utility.sDbnull(objEmrBa.BenhxahoiMota);

                        opt_ungthu_co.Checked = Utility.Bool2Bool(objEmrBa.UngthuCo);
                        opt_ungthu_khong.Checked = Utility.Bool2Bool(objEmrBa.UngthuKhong);
                        txt_ungthu_mota.Text = Utility.sDbnull(objEmrBa.UngthuMota);

                        opt_tiencanlao_co.Checked = Utility.Bool2Bool(objEmrBa.TiencanlaoCo);
                        opt_tiencanlao_khong.Checked = Utility.Bool2Bool(objEmrBa.TiencanlaoKhong);

                        opt_sudungtestosteron_co.Checked = Utility.Bool2Bool(objEmrBa.SudungTestosteronCo);
                        opt_sudungtestosteron_khong.Checked = Utility.Bool2Bool(objEmrBa.SudungTestosteronKhong);
                        txt_testosteron_mota.Text = Utility.sDbnull(objEmrBa.SudungTestosteronMota);

                        txt_noikhoa_khac.Text = Utility.sDbnull(objEmrBa.NoikhoaKhac);
                        txt_thuocdangdieutri.Text = Utility.sDbnull(objEmrBa.Thuocdangdieutri);
                        //Ngoại khoa
                        opt_viphauthuatthatTMT_co.Checked = Utility.Bool2Bool(objEmrBa.ViphaucothattmtCo);
                        opt_viphauthuatthatTMT_khong.Checked = Utility.Bool2Bool(objEmrBa.ViphaucothattmtKhong);

                        opt_hatinhoanan_co.Checked = Utility.Bool2Bool(objEmrBa.HatinhoananCo);
                        opt_hatinhoanan_khong.Checked = Utility.Bool2Bool(objEmrBa.HatinhoananKhong);
                        txt_hatinhhoan_mota.Text = Utility.sDbnull(objEmrBa.HatinhoananMota);

                        opt_thatongdantinh_co.Checked = Utility.Bool2Bool(objEmrBa.ThatongdantinhCo);
                        opt_thatongdantinh_khong.Checked = Utility.Bool2Bool(objEmrBa.ThatongdantinhKhong);
                        txt_thatongdantinh_mota.Text = Utility.sDbnull(objEmrBa.ThatongdantinhThoigian);
                        txt_ngoaikhoa_khac.Text = Utility.sDbnull(objEmrBa.NgoaikhoaKhac);
                        //Quan hệ tình dục
                        txtTansuatquanhetinhduc.Text = Utility.sDbnull(objEmrBa.QuanhetinhducTansuat);
                        opt_roiloancuongduong_co.Checked = Utility.Bool2Bool(objEmrBa.RoiloancuongCo);
                        opt_roiloancuongduong_khong.Checked = Utility.Bool2Bool(objEmrBa.RoiloancuongKhong);
                        txt_roiloancuongduong_mota.Text = Utility.sDbnull(objEmrBa.RoiloancuongMota);

                        chk_xuattinh_som.Checked = Utility.Bool2Bool(objEmrBa.XuattinhsomTruockhixamnhap);
                        chk_xuattinh_sau.Checked = Utility.Bool2Bool(objEmrBa.XuattinhsomSaukhixamnhap);
                        chk_xuattinh_khong.Checked = Utility.Bool2Bool(objEmrBa.XuattinhsomKhong);

                        opt_cuckhoai_co.Checked = Utility.Bool2Bool(objEmrBa.CuckhoaiCo);
                        opt_cuckhoai_khong.Checked = Utility.Bool2Bool(objEmrBa.CuckhoaiKhong);
                        opt_sudungchatboitron_co.Checked = Utility.Bool2Bool(objEmrBa.CosudungchatboitronCo);
                        opt_sudungchatboitron_khong.Checked = Utility.Bool2Bool(objEmrBa.CosudungchatboitronKhong);

                        txt_chatboitron_mota.Text = Utility.sDbnull(objEmrBa.CosudungchatboitronMota);
                        //Khám chuyên khoa
                        txt_tinhoan_thetich_phai.Text = Utility.sDbnull(objEmrBa.ThetichtinhhoanPhai);
                        txt_tinhoan_thetich_trai.Text = Utility.sDbnull(objEmrBa.ThetichtinhhoanTrai);
                        txt_matdotinhoan_phai.Text = Utility.sDbnull(objEmrBa.MatdotinhhoanPhai);
                        txt_matdotinhoan_trai.Text = Utility.sDbnull(objEmrBa.MatdotinhhoanTrai);
                        opt_matdotinhhoanphai_chac.Checked = Utility.Bool2Bool(objEmrBa.MatdotinhhoanPhaiChac);
                        opt_matdomaotinhphai_mem.Checked = Utility.Bool2Bool(objEmrBa.MatdotinhhoanPhaiMem);
                        opt_matdotinhhoantrai_chac.Checked = Utility.Bool2Bool(objEmrBa.MatdotinhhoanTraiChac);
                        opt_matdomaotinhtrai_mem.Checked = Utility.Bool2Bool(objEmrBa.MatdotinhhoanTraiMem);

                        txt_bemattinhhoan_phai.Text = Utility.sDbnull(objEmrBa.BemattinhoanPhai);
                        txt_bemattinhhoan_trai.Text = Utility.sDbnull(objEmrBa.BemattinhoanTrai);
                        //Mào tinh
                        txt_matdomaotinh_phai.Text = Utility.sDbnull(objEmrBa.MatdomaotinhPhai);
                        txt_matdomaotinh_trai.Text = Utility.sDbnull(objEmrBa.MatdomaotinhTrai);
                        opt_matdomaotinhphai_chac.Checked = Utility.Bool2Bool(objEmrBa.MatdomaotinhPhaiChac);
                        opt_matdomaotinhtrai_chac.Checked = Utility.Bool2Bool(objEmrBa.MatdomaotinhTraiChac);
                        opt_matdomaotinhphai_mem.Checked = Utility.Bool2Bool(objEmrBa.MatdomaotinhPhaiMem);
                        opt_matdomaotinhtrai_mem.Checked = Utility.Bool2Bool(objEmrBa.MatdomaotinhTraiMem);
                        //Nang
                        opt_maotinh_nangphai_co.Checked = Utility.Bool2Bool(objEmrBa.MaotinhNangphaiCo);
                        opt_maotinh_nangphai_khong.Checked = Utility.Bool2Bool(objEmrBa.MaotinhNangphaiKhong);
                        opt_maotinh_nangphai_khongxacdinh.Checked = Utility.Bool2Bool(objEmrBa.MaotinhNangphaiKhongxacdinh);

                        opt_maotinh_nangtrai_co.Checked = Utility.Bool2Bool(objEmrBa.MaotinhNangtraiCo);
                        opt_maotinh_nangtrai_khong.Checked = Utility.Bool2Bool(objEmrBa.MaotinhNangtraiKhong);
                        opt_maotinh_nangtrai_khongxacdinh.Checked = Utility.Bool2Bool(objEmrBa.MaotinhNangtraiKhongxacdinh);

                        //Ống dẫn tinh đoạn trong bầu
                        opt_ongdantinhdoantrongbauphai_co.Checked = Utility.Bool2Bool(objEmrBa.OngdantinhPhaiCo);
                        opt_ongdantinhdoantrongbauphai_khong.Checked = Utility.Bool2Bool(objEmrBa.OngdantinhPhaiKhong);
                        opt_ongdantinhdoantrongbauphai_khongro.Checked = Utility.Bool2Bool(objEmrBa.OngdantinhPhaiKhongro);

                        opt_ongdantinhdoantrongbautrai_co.Checked = Utility.Bool2Bool(objEmrBa.OngdantinhTraiCo);
                        opt_ongdantinhdoantrongbautrai_khong.Checked = Utility.Bool2Bool(objEmrBa.OngdantinhTraiKhong);
                        opt_ongdantinhdoantrongbautrai_khongro.Checked = Utility.Bool2Bool(objEmrBa.OngdantinhTraiKhongro);

                        //Tĩnh mạch thừng tinh
                        opt_tinhmachthungtinh_gianphai_1.Checked = Utility.Bool2Bool(objEmrBa.TinhmachthungtingGianphai1);
                        opt_tinhmachthungtinh_gianphai_2.Checked = Utility.Bool2Bool(objEmrBa.TinhmachthungtingGianphai2);
                        opt_tinhmachthungtinh_gianphai_3.Checked = Utility.Bool2Bool(objEmrBa.TinhmachthungtingGianphai3);
                        chk_tinhmachthungtinhphai_binhthuong.Checked = Utility.Bool2Bool(objEmrBa.TinhmachthungtingTraiBinhthuong);


                        opt_tinhmachthungtinh_giantrai_1.Checked = Utility.Bool2Bool(objEmrBa.TinhmachthungtingGiantrai1);
                        opt_tinhmachthungtinh_giantrai_2.Checked = Utility.Bool2Bool(objEmrBa.TinhmachthungtingGiantrai2);
                        opt_tinhmachthungtinh_giantrai_3.Checked = Utility.Bool2Bool(objEmrBa.TinhmachthungtingGiantrai3);
                        chk_tinhmachthungtinhtrai_binhthuong.Checked = Utility.Bool2Bool(objEmrBa.TinhmachthungtingTraiBinhthuong);
                        //Đặc điểm sinh dục thứ phát
                        opt_phanboco_binhthuong.Checked = Utility.Bool2Bool(objEmrBa.PhanbocoBinhthuong);
                        opt_phanboco_batthuong.Checked = Utility.Bool2Bool(objEmrBa.PhanbocoBatthuong);
                        txt_phanbomo.Text = Utility.sDbnull(objEmrBa.PhanboMo);
                        txt_longmu.Text = Utility.sDbnull(objEmrBa.PhanboLongmu);
                        txt_chi.Text = Utility.sDbnull(objEmrBa.PhanboChi);
                        txtBacsiKham.SetId(objEmrBa.IdBacsiKham);
                        //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objEmrBa.NgayKham) ? dtNgayKham.Value : objEmrBa.NgayKham);
                        dtpNgayKham.Value = string.IsNullOrEmpty(objEmrBa.NgayKham.ToString()) ? dtpNgayKham.Value : Convert.ToDateTime(objEmrBa.NgayKham);

                        txtBenhAnGiaDinh.Text = Utility.sDbnull(objEmrBa.HoibenhTiensugiadinh);// Utility.sDbnull(dr["BaGiaDinh"].ToString());
                        
                        txtMach.Text = Utility.sDbnull(objEmrBa.KbMach);
                        txtNhietDo.Text = Utility.sDbnull(objEmrBa.KbNhietdo);
                        txtha.Text = Utility.sDbnull(objEmrBa.KbHuyetap);
                        txtNhipTho.Text = Utility.sDbnull(objEmrBa.KbNhiptho);
                        txtCanNang.Text = Utility.sDbnull(objEmrBa.KbCannang);
                        txtChieuCao.Text = Utility.sDbnull(objEmrBa.KbChieucao);
                        tinhBMI();
                        txt_toanthan.Text = Utility.sDbnull(objEmrBa.KhambenhToanthan);// Utility.sDbnull(dr["KbToanThan"].ToString());
                        txt_tuanhoan.Text = Utility.sDbnull(objEmrBa.KhambenhTuanhoan);
                        txt_hohap.Text = Utility.sDbnull(objEmrBa.KhambenhHohap);
                        txt_tieuhoa.Text = Utility.sDbnull(objEmrBa.KhambenhTieuhoa);
                        txt_thantietnieu_sinhduc.Text = Utility.sDbnull(objEmrBa.KhambenhThantietnieusinhduc);
                        txt_thankinh.Text = Utility.sDbnull(objEmrBa.KhambenhThankinh);
                        txt_coxuongkhop.Text = Utility.sDbnull(objEmrBa.KhambenhCoxuongkhop);
                        txt_taimuihong.Text = Utility.sDbnull(objEmrBa.KhambenhTaimuihong);
                        txt_ranghammat.Text = Utility.sDbnull(objEmrBa.KhambenhRanghammat);
                        txt_khac.Text = Utility.sDbnull(objEmrBa.KhambenhNoitietDinhduongBenhlykhac);
                       
                        txtBenhAnCacXetNghiem.Text = Utility.sDbnull(objEmrBa.KhambenhXetnghiemClsCanlam);
                        txtBenhAnTomTatBenhAn.Text = Utility.sDbnull(objEmrBa.KhambenhTomtatbenhan);
                        txtBenhAnBenhChinh.Text = Utility.sDbnull(objEmrBa.CdKhivaokhoadieutriBenhchinh);
                        txtBenhAnBenhKemTheo.Text = Utility.sDbnull(objEmrBa.CdKhivaokhoadieutriBenhphu);
                        txtBenhAnPhanBiet.Text = Utility.sDbnull(objEmrBa.CdKhivaokhoadieutriPhanbiet);
                        txtBenhAnTienLuong.Text = Utility.sDbnull(objEmrBa.KhambenhTienluong);
                        txtBenhAnHuongDieuTri.Text = Utility.sDbnull(objEmrBa.KhambenhHuongdieutri);
                        txtTKBAQuaTrinhBenhLy.Text = Utility.sDbnull(objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang);
                        txtTKBATTomTatKetQua.Text = Utility.sDbnull(objEmrBa.TongketbaTomtatKqcls);
                        txtTKBAPhuongPhapDieuTri.Text = Utility.sDbnull(objEmrBa.TongketbaPhuongphapdieutri);
                        txtTKBATinhTrangRaVien.Text = Utility.sDbnull(objEmrBa.TongketbaTinhtrangNguoiravien);// Utility.sDbnull(dr["TkbaTtrv"].ToString());
                        txtTKBAHuongDieuTri.Text = Utility.sDbnull(objEmrBa.TongketbaHuongdieutritieptheo);// Utility.sDbnull(dr["TkbaHdt"].ToString());

                        txtNguoiGiaoHoSo.SetId(objEmrBa.IdNguoigiaoHoso);
                        txtNguoiNhanHoSo.SetId(objEmrBa.IdNguoinhanHoso);
                        txtBSDieuTri.SetId(objEmrBa.IdBacsiDieutri);
                        txtGDBV.SetId(objEmrBa.IdGiamdoc);
                        txtTruongkhoa.SetId(objEmrBa.IdTruongkhoadieutri);

                        txtBacsiKham.SetId(objEmrBa.IdBacsiKham);
                        txtBSlamBA.SetId(objEmrBa.IdBacsiLamBA);

                        txtB_CTScanner.Text = Utility.sDbnull(objEmrBa.TongketbaSotoCt);
                        txtB_Xquang.Text = Utility.sDbnull(objEmrBa.TongketbaSotoXquang);
                        txtB_SieuAm.Text = Utility.sDbnull(objEmrBa.TongketbaSotoSieuam);
                        txtB_XetNghiem.Text = Utility.sDbnull(objEmrBa.TongketbaSotoXetnghiem);
                        txtB_Khac.Text = Utility.sDbnull(objEmrBa.TongketbaSotoKhac);
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
                    txtCDKKBCapCuu.Text = Get_ChanDoan_KKB_CapCuu();
                    txtCDMaKKBCapCuu.Text = Utility.sDbnull(objLuotkham.MabenhChinh, string.Empty);
                    KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                        .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbThongtinchung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbThongtinchung>();
                    if (tef != null)
                    {
                        txtMach.Text = Utility.sDbnull(tef.Mach);
                        txtNhietDo.Text = Utility.sDbnull(tef.Nhietdo);
                        txtha.Text = Utility.sDbnull(tef.Huyetap);
                        txtNhipTho.Text = Utility.sDbnull(tef.Nhiptho);
                        txtCanNang.Text = Utility.sDbnull(tef.Cannang);
                        txtChieuCao.Text = Utility.sDbnull(tef.Chieucao);
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
                txt_toanthan.Text = objPKB.ToanThan;
                txt_hach.Text = objPKB.Hach;
                txt_tuanhoan.Text = objPKB.Tuanhoan;
                txt_tieuhoa.Text = objPKB.Tieuhoa;
                txt_hohap.Text = objPKB.Hohap;
                txt_thankinh.Text = objPKB.Thankinh;

                txt_coxuongkhop.Text = objPKB.Coxuongkhop;
                txt_thantietnieu_sinhduc.Text = objPKB.Thantietnieusinhduc;
                txt_taimuihong.Text = objPKB.Taimuihong;
                txt_ranghammat.Text = objPKB.Ranghammat;
                txt_khac.Text = objPKB.Noitietdinhduongbenhlykhac;
            }
        }
        void FillPhieuKhamNamKhoa()
        {
            if (objPhieukhamNamkhoa != null)
            {
                txtNhietDo.Text = objPhieukhamNamkhoa.NhietDo;
                txtha.Text = objPhieukhamNamkhoa.NhomMau;
                txtMach.Text = objPhieukhamNamkhoa.Mach;
                txtNhipTho.Text = objPhieukhamNamkhoa.NhịpTho;
                txtChieuCao.Text = objPhieukhamNamkhoa.ChieuCao;
                txtCanNang.Text = objPhieukhamNamkhoa.CanNang;
                txtBMI.Text = objPhieukhamNamkhoa.Bmi;
                //Tiền sử nội khoa
                txt_benhly_toanthan.Text = Utility.sDbnull(objPhieukhamNamkhoa.BenhlyToanthan);

                opt_quaibi_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.QuaibiCo);
                opt_quaibi_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.QuaibiKhong);
                opt_bienchungtinhhoan_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.BienchungtinhhoanCo);
                opt_bienchungtinhhoan_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.BienchungtinhhoanKhong);
                opt_bienchungtinhhoan_1ben.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.Bienchungtinhhoan1ben);
                opt_bienchungtinhhoan_2ben.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.Bienchungtinhhoan2ben);
                txt_xutri.Text = Utility.sDbnull(objPhieukhamNamkhoa.BienchungtinhoanMota);

                opt_benhxahoi_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.BenhxahoiCo);
                opt_benhxahoi_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.BenhxahoiKhong);
                txt_benhxahoi_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.BenhxahoiMota);

                opt_ungthu_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.UngthuCo);
                opt_ungthu_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.UngthuKhong);
                txt_ungthu_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.UngthuMota);

                opt_tiencanlao_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TiencanlaoCo);
                opt_tiencanlao_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TiencanlaoKhong);

                opt_sudungtestosteron_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.SudungTestosteronCo);
                opt_sudungtestosteron_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.SudungTestosteronKhong);
                txt_testosteron_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.SudungTestosteronMota);

                txt_noikhoa_khac.Text = Utility.sDbnull(objPhieukhamNamkhoa.NoikhoaKhac);
                txt_thuocdangdieutri.Text = Utility.sDbnull(objPhieukhamNamkhoa.Thuocdangdieutri);
                //Ngoại khoa
                opt_viphauthuatthatTMT_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.ViphaucothattmtCo);
                opt_viphauthuatthatTMT_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.ViphaucothattmtKhong);

                opt_hatinhoanan_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.HatinhoananCo);
                opt_hatinhoanan_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.HatinhoananKhong);
                txt_hatinhhoan_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.HatinhoananMota);

                opt_thatongdantinh_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.ThatongdantinhCo);
                opt_thatongdantinh_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.ThatongdantinhKhong);
                txt_thatongdantinh_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.ThatongdantinhThoigian);
                txt_ngoaikhoa_khac.Text = Utility.sDbnull(objPhieukhamNamkhoa.NgoaikhoaKhac);
                //Quan hệ tình dục
                txtTansuatquanhetinhduc.Text = Utility.sDbnull(objPhieukhamNamkhoa.QuanhetinhducTansuat);
                opt_roiloancuongduong_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.RoiloancuongCo);
                opt_roiloancuongduong_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.RoiloancuongKhong);
                txt_roiloancuongduong_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.RoiloancuongMota);

                chk_xuattinh_som.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.XuattinhsomTruockhixamnhap);
                chk_xuattinh_sau.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.XuattinhsomSaukhixamnhap);
                chk_xuattinh_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.XuattinhsomKhong);

                opt_cuckhoai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.CuckhoaiCo);
                opt_cuckhoai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.CuckhoaiKhong);
                opt_sudungchatboitron_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.CosudungchatboitronCo);
                opt_sudungchatboitron_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.CosudungchatboitronKhong);

                txt_chatboitron_mota.Text = Utility.sDbnull(objPhieukhamNamkhoa.CosudungchatboitronMota);
                //Khám chuyên khoa
                txt_tinhoan_thetich_phai.Text = Utility.sDbnull(objPhieukhamNamkhoa.ThetichtinhhoanPhai);
                txt_tinhoan_thetich_trai.Text = Utility.sDbnull(objPhieukhamNamkhoa.ThetichtinhhoanTrai);
                txt_matdotinhoan_phai.Text = Utility.sDbnull(objPhieukhamNamkhoa.MatdotinhhoanPhai);
                txt_matdotinhoan_trai.Text = Utility.sDbnull(objPhieukhamNamkhoa.MatdotinhhoanTrai);
                opt_matdotinhhoanphai_chac.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanPhaiChac);
                opt_matdomaotinhphai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanPhaiMem);
                opt_matdotinhhoantrai_chac.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanTraiChac);
                opt_matdomaotinhtrai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdotinhhoanTraiMem);

                txt_bemattinhhoan_phai.Text = Utility.sDbnull(objPhieukhamNamkhoa.BemattinhoanPhai);
                txt_bemattinhhoan_trai.Text = Utility.sDbnull(objPhieukhamNamkhoa.BemattinhoanTrai);
                //Mào tinh
                txt_matdomaotinh_phai.Text = Utility.sDbnull(objPhieukhamNamkhoa.MatdomaotinhPhai);
                txt_matdomaotinh_trai.Text = Utility.sDbnull(objPhieukhamNamkhoa.MatdomaotinhTrai);
                opt_matdomaotinhphai_chac.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdomaotinhPhaiChac);
                opt_matdomaotinhtrai_chac.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdomaotinhTraiChac);
                opt_matdomaotinhphai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdomaotinhPhaiMem);
                opt_matdomaotinhtrai_mem.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MatdomaotinhTraiMem);
                //Nang
                opt_maotinh_nangphai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangphaiCo);
                opt_maotinh_nangphai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangphaiKhong);
                opt_maotinh_nangphai_khongxacdinh.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangphaiKhongxacdinh);

                opt_maotinh_nangtrai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangtraiCo);
                opt_maotinh_nangtrai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangtraiKhong);
                opt_maotinh_nangtrai_khongxacdinh.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.MaotinhNangtraiKhongxacdinh);

                //Ống dẫn tinh đoạn trong bầu
                opt_ongdantinhdoantrongbauphai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhPhaiCo);
                opt_ongdantinhdoantrongbauphai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhPhaiKhong);
                opt_ongdantinhdoantrongbauphai_khongro.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhPhaiKhongro);

                opt_ongdantinhdoantrongbautrai_co.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhTraiCo);
                opt_ongdantinhdoantrongbautrai_khong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhTraiKhong);
                opt_ongdantinhdoantrongbautrai_khongro.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.OngdantinhTraiKhongro);

                //Tĩnh mạch thừng tinh
                opt_tinhmachthungtinh_gianphai_1.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGianphai1);
                opt_tinhmachthungtinh_gianphai_2.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGianphai2);
                opt_tinhmachthungtinh_gianphai_3.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGianphai3);
                chk_tinhmachthungtinhphai_binhthuong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingTraiBinhthuong);


                opt_tinhmachthungtinh_giantrai_1.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGiantrai1);
                opt_tinhmachthungtinh_giantrai_2.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGiantrai2);
                opt_tinhmachthungtinh_giantrai_3.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingGiantrai3);
                chk_tinhmachthungtinhtrai_binhthuong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.TinhmachthungtingTraiBinhthuong);
                //Đặc điểm sinh dục thứ phát
                opt_phanboco_binhthuong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.PhanbocoBinhthuong);
                opt_phanboco_batthuong.Checked = Utility.Bool2Bool(objPhieukhamNamkhoa.PhanbocoBatthuong);
                txt_phanbomo.Text = Utility.sDbnull(objPhieukhamNamkhoa.PhanboMo);
                txt_longmu.Text = Utility.sDbnull(objPhieukhamNamkhoa.PhanboLongmu);
                txt_chi.Text = Utility.sDbnull(objPhieukhamNamkhoa.PhanboChi);

               txtBacsiKham.SetId( Utility.sDbnull(objPhieukhamNamkhoa.IdBacsi, "-1"));
                //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objPhieukhamNamkhoa.NgayKham) ? dtNgayKham.Value : objPhieukhamNamkhoa.NgayKham);
                dtpNgayKham.Value = string.IsNullOrEmpty(objPhieukhamNamkhoa.NgayKham.ToString()) ? dtpNgayKham.Value : Convert.ToDateTime(objPhieukhamNamkhoa.NgayKham);
            }
            else
            {
                KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                    .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbThongtinchung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbThongtinchung>();
                if (tef != null)
                {
                    txtMach.Text = Utility.sDbnull(tef.Mach);
                    txtNhietDo.Text = Utility.sDbnull(tef.Nhietdo);
                    txtha.Text = Utility.sDbnull(tef.Huyetap);
                    txtNhipTho.Text = Utility.sDbnull(tef.Nhiptho);
                    txtCanNang.Text = Utility.sDbnull(tef.Cannang);
                    txtChieuCao.Text = Utility.sDbnull(tef.Chieucao);
                    tinhBMI();
                }
            }
        }
        void FillThongtinNhapvien()
        {
            objNhapvien = new Select().From(NoitruPhieunhapvien.Schema)
                   .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
            if (objNhapvien != null)
            {
                txtBenhAnLyDoNhapVien._Text = Utility.sDbnull(objNhapvien.LydoNhapvien);
                txtBenhAnTiensuBanthan.Text = Utility.sDbnull(objNhapvien.TsuBanthan);
                txtBenhAnGiaDinh.Text = Utility.sDbnull(objNhapvien.TsuGiadinh);
                txtBenhAnQuaTrinhBenhLy.Text = Utility.sDbnull(objNhapvien.QuatrinhBenhly);
               
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
        void ModifyCommand()
        {
            tabpageTo2.Enabled = tabpageTo3.Enabled = tabpageTo4.Enabled = objLuotkham != null;
            btnInto2.Enabled = btnInto3.Enabled = Into1.Enabled = btnInto4.Enabled = button1.Enabled = btnInVoBA.Enabled = objLuotkham != null && objEmrBa!=null;
            cmdXoaBenhAn.Enabled = objLuotkham != null && objEmrBa != null;
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
               
                objEmrBa = EmrBaNamkhoa.FetchByID(Utility.Int64Dbnull( txtIDBenhAn.Text));
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
                               new Delete().From(EmrBaNamkhoa.Schema)
                                     .Where(EmrBaNamkhoa.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                     .And(EmrBaNamkhoa.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                     .And(EmrBaNamkhoa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                     .Execute();
                              new Delete().From(EmrHosoluutru.Schema)
                                    .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                    .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                    .And(EmrBaNamkhoa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                    .Execute();
                                emrdoc.DeleteDocument_WithoutTransaction(objEmrBa.IdBa, new List<string>() { "BENHAN", "BENHAN_BIA", "BENHAN_TO1", "BENHAN_TO2", "BENHAN_TO3", "BENHAN_TO4" }, "");
                                Utility.Log("frm_BenhAn_NamKhoa", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công",objEmrBa.IdBa,objEmrBa.LoaiBa,objEmrBa.MaBa,objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
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
            //    objLuotkham.MaLuotkham = Utility.sDbnull(frm.SoHSBA);
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
                        txtBMI.Text = Utility.sDbnull(Math.Round(bmi, 2));
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
            EmrTongketBenhan objTKBA =new Select().From(EmrTongketBenhan.Schema)
                .Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTongketBenhan>();
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
        EmrTongketBenhan objTKBA;
        void FillTongketBenhAn()
        {
            try
            {
                objTKBA=  new Select().From(EmrTongketBenhan.Schema)
                    .Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<EmrTongketBenhan>();
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

                    txtB_CTScanner.Text = Utility.sDbnull(objTKBA.SotoCt);
                    txtB_Xquang.Text = Utility.sDbnull(objTKBA.SotoXquang);
                    txtB_SieuAm.Text = Utility.sDbnull(objTKBA.SotoSieuam);
                    txtB_XetNghiem.Text = Utility.sDbnull(objTKBA.SotoXetnghiem);
                    txtB_Khac.Text = Utility.sDbnull(objTKBA.SotoKhac);
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
            LuuBenhAn(0);
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
            txt_toanthan.ReadOnly = txt_tuanhoan.ReadOnly = txt_hohap.ReadOnly
                = txt_tieuhoa.ReadOnly = txt_thantietnieu_sinhduc.ReadOnly = txt_thankinh.ReadOnly
                = txt_coxuongkhop.ReadOnly = txt_taimuihong.ReadOnly
                = txt_khac.ReadOnly = !chkEditPKB.Checked && !chkEditPKB.Visible;
        }

        private void chkEditTKBA_CheckedChanged(object sender, EventArgs e)
        {
            txtTKBAQuaTrinhBenhLy.ReadOnly = txtTKBATTomTatKetQua.ReadOnly
              = txtTKBAPhuongPhapDieuTri.ReadOnly = txtTKBATinhTrangRaVien.ReadOnly
              = txtTKBAHuongDieuTri.ReadOnly = txtB_Xquang.ReadOnly = txtB_CTScanner.ReadOnly = txtB_SieuAm.ReadOnly
              = txtB_XetNghiem.ReadOnly = txtB_Khac.ReadOnly = txtNguoiGiaoHoSo.ReadOnly = txtNguoiNhanHoSo.ReadOnly = txtBSDieuTri.ReadOnly
              = !chkEditTKBA.Checked && !chkEditTKBA.Visible;
        }

        private void mnuSent2EMR_Click(object sender, EventArgs e)
        {
            try
            {
                if(objEmrBa!=null)
                {
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_BIA, "BA_BANAMKHOA_BIA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO1, "BA_BANAMKHOA_TO1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO2, "BA_BANAMKHOA_TO2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO3, "BA_BANAMKHOA_TO3", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO4, "BA_BANAMKHOA_TO4", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BA_NAMKHOA, "BA_BANAMKHOA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", Loaiphieu_HIS.BA_NAMKHOA);
                    emrdoc.Save();
                    Utility.ShowMsg("Đẩy dữ liệu vào EMR thành công. Nhấn OK để kết thúc");
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
                Utility.ShowMsg("Cần nhập thông tin người bệnh trước khi thực hiện thêm thông tin phiếu khám Sản khoa");
                return;
            }
            frm_khamPhukhoa _khamPhukhoa = new frm_khamPhukhoa(objLuotkham, objBenhnhan);
            _khamPhukhoa.ShowDialog();
            FillPhieuKhamNamKhoa();
        }

        private void cmdKhamPhuKhoa2_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Cần nhập thông tin người bệnh trước khi thực hiện thêm thông tin phiếu khám Sản khoa");
                return;
            }
            frm_khamPhukhoa _khamPhukhoa = new frm_khamPhukhoa(objLuotkham, objBenhnhan);
            _khamPhukhoa.ShowDialog();
            FillPhieuKhamNamKhoa();
        }

        private void opt_bienchungtinhhoan_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_xutri.Enabled = opt_bienchungtinhhoan_co.Checked;
            txt_xutri.Focus();
        }

        private void opt_ungthu_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_ungthu_mota.Enabled = opt_ungthu_co.Checked;
            txt_ungthu_mota.Focus();
        }

        private void opt_sudungtestosteron_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_testosteron_mota.Enabled = opt_sudungtestosteron_co.Checked;
            txt_testosteron_mota.Focus();
        }

        private void opt_viphauthuatthatTMT_co_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void opt_hatinhoanan_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_hatinhhoan_mota.Enabled = opt_hatinhoanan_co.Checked;
            txt_hatinhhoan_mota.Focus();
        }

        private void opt_thatongdantinh_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_thatongdantinh_mota.Enabled = opt_thatongdantinh_co.Checked;
            txt_thatongdantinh_mota.Focus();
        }

        private void opt_roiloancuongduong_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_roiloancuongduong_mota.Enabled = opt_roiloancuongduong_co.Checked;
            txt_roiloancuongduong_mota.Focus();
        }

        private void opt_sudungchatboitron_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_chatboitron_mota.Enabled = opt_sudungchatboitron_co.Checked;
            txt_chatboitron_mota.Focus();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            LuuBenhAn(1);
        }
    }
}
