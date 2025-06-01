using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
using Janus.Windows.GridEX;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Microsoft.VisualBasic;
using VNS.HIS.UCs;
using VNS.HIS.UI.DANHMUC;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using VNS.HIS.UI.Classess;
using Aspose.Words;
using System.Diagnostics;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.Danhmuc.Dungchung;
using System.Transactions;

namespace VMS.EMR.PHIEUKHAM
{
    public partial class frm_ThemPhieukhamthai : Form
    {
        public action m_enAct = action.FirstOrFinished;
        public KcbPhieukhamthai _pkt = new KcbPhieukhamthai();
        public bool CallfromParent = false;
        KcbLuotkham objLuotkham = null;
        DataTable dtTiensusankhoa = new DataTable();
        public frm_ThemPhieukhamthai()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtpNgaykhamtruoc.Value =dtpNgaydaukykinhcuoi.Value=dtpNgaydukiensinh.Value= DateTime.Now;
            InitEvents();
            SetReadOnly();

        }
        void SetReadOnly()
        {
            try
            {

                //txtXQ.ReadOnly = txtSA.ReadOnly = txtDientim.ReadOnly = isReadOnly;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void InitEvents()
        {
            ucThongtinnguoibenh_doc_v61._OnEnterMe += ucThongtinnguoibenh_doc_v61__OnEnterMe;
            this.KeyDown += frm_ThemPhieukhamthai_KeyDown;
            this.Load+=frm_ThemPhieukhamthai_Load;
            FormClosing += frm_ThemPhieukhamthai_FormClosing;
            cmdSave.Click+=cmdSave_Click;
            cmdIn.Click+=cmdIn_Click;
            cmdExit.Click+=cmdExit_Click;
            cmdViewKQCLS.Click+=cmdViewKQCLS_Click;
            cmdThemMoiBN.Click+=cmdThemMoiBN_Click;
            grdLichsusankhoa.ColumnButtonClick += grdLichsusankhoa_ColumnButtonClick;
            grdLichsusankhoa.CellValueChanged += grdLichsusankhoa_CellValueChanged;
            grdLichsusankhoa.CellUpdated += grdLichsusankhoa_CellUpdated;
            cmdRefreshChucnangsong.Click+=cmdRefreshChucnangsong_Click;
        }

        void grdLichsusankhoa_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            long IdTiensuSankhoa = Utility.Int64Dbnull(grdLichsusankhoa.GetValue(KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa));
            if (IdTiensuSankhoa > 0)
            {
                new Update(KcbPhieukhamthaiTiensusankhoa.Schema).Set(e.Column.Key).EqualTo(Utility.sDbnull(grdLichsusankhoa.CurrentRow.Cells[e.Column].Value))
                    .Where(KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa).IsEqualTo(IdTiensuSankhoa).Execute();
            }
        }

        void grdLichsusankhoa_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
           
        }

        void grdLichsusankhoa_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "XOA")
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa dòng tiền sử sản khoa đang chọn","Xác nhận xóa",true))
                {
                    string noidung = string.Format("Thời gian, nơi kết thúc thai nghén:={0},Tuôi thai (sảy, non, đủ, già tháng):={1},Diễn biến thai:={2},Cách thức sinh:={3},Thông tin trẻ(cân nặng, bệnh tật,...):={4  },Hậu sản:={5}"
                        , Utility.sDbnull(grdLichsusankhoa.GetValue(KcbPhieukhamthaiTiensusankhoa.Columns.ThoigianNoiKetthuc))
                        , Utility.sDbnull(grdLichsusankhoa.GetValue(KcbPhieukhamthaiTiensusankhoa.Columns.TuoiThai))
                        , Utility.sDbnull(grdLichsusankhoa.GetValue(KcbPhieukhamthaiTiensusankhoa.Columns.DienBien))
                        , Utility.sDbnull(grdLichsusankhoa.GetValue(KcbPhieukhamthaiTiensusankhoa.Columns.CachSinh))
                        , Utility.sDbnull(grdLichsusankhoa.GetValue(KcbPhieukhamthaiTiensusankhoa.Columns.MotaTresosinh))
                        , Utility.sDbnull(grdLichsusankhoa.GetValue(KcbPhieukhamthaiTiensusankhoa.Columns.HauSan)));
                    long IdTiensuSankhoa = Utility.Int64Dbnull(grdLichsusankhoa.GetValue(KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa));
                    string guid = Utility.sDbnull(grdLichsusankhoa.GetValue("guid"));
                    using (var Scope = new TransactionScope())
                    {
                        using (var dbScope = new SharedDbConnectionScope())
                        {
                            
                            if(IdTiensuSankhoa>0)
                            new Delete().From(KcbPhieukhamthaiTiensusankhoa.Schema).Where(KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa).IsEqualTo(Utility.Int64Dbnull(grdLichsusankhoa.GetValue(KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa))).Execute();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa thông tin tiền sử sản khoa của người bệnh id={0}, mã lượt khám ={1}, nội dung ={2} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, noidung), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        }
                        Scope.Complete();
                    }
                    //Xóa khỏi bảng 
                    if(IdTiensuSankhoa>0)
                        dtTiensusankhoa.AsEnumerable().Where(r =>Utility.Int64Dbnull( r[KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa]) == IdTiensuSankhoa).ToList().ForEach(row => row.Delete());
                    else
                        dtTiensusankhoa.AsEnumerable().Where(r => Utility.sDbnull(r["guid"]) == guid).ToList().ForEach(row => row.Delete());
                    dtTiensusankhoa.AcceptChanges();

                }
            }
        }
       
      
        void frm_ThemPhieukhamthai_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void frm_ThemPhieukhamthai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control activeCtrl = Utility.getActiveControl(this);
                if (activeCtrl != null )//&& (activeCtrl.Name == autoBSPhauthuat.Name || activeCtrl.Name == autoBSGayme.Name || activeCtrl.Name == autoBSphu.Name || activeCtrl.Name == autoBacsithamgia.Name)))
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
                    //else if (activeCtrl.Name == autoBacsithamgia.Name)
                    //    if (Utility.DoTrim(autoBacsithamgia.Text).Length > 0)
                    //        return;
                    //    else
                    //        SendKeys.Send("{TAB}");
                    else if (activeCtrl.Name == txtBenhphukhoa.Name)
                    {
                        uiTabInfor.SelectedIndex = 1;
                        opttsgd_khong.Focus();
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                
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
                cmdThemMoiBN.PerformClick();
            }

        }

        void ucThongtinnguoibenh_doc_v61__OnEnterMe()
        {
            if (ucThongtinnguoibenh_doc_v61.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh_doc_v61.objLuotkham;
                if (ucThongtinnguoibenh_doc_v61.objBenhnhan.IdGioitinh == 0)
                {
                    Utility.ShowMsg("Phiếu khám thai chỉ dành cho người bệnh mang giới tính Nữ.\nNgười bệnh đang chọn mang giới tính Nam. Vui lòng kiểm tra lại");
                    ClearControl();
                    return;
                }
                _pkt = new Select().From(KcbPhieukhamthai.Schema)
                    .Where(KcbPhieukhamthai.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbPhieukhamthai.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<KcbPhieukhamthai>();
                if (_pkt != null) m_enAct = action.Update;
                FillData4Update();
            }
        }

       
        void FillData4Update()
        {
            txtNhommau.Text = ucThongtinnguoibenh_doc_v61.objBenhnhan != null ? ucThongtinnguoibenh_doc_v61.objBenhnhan.NhomMau : "";
            if (_pkt != null)
            {
               
                dtpNgaykhamtruoc.Value = Convert.ToDateTime(_pkt.NgayKham);
                txtThongtinlankhamtruoc.Text = _pkt.ThongtinLankhamtruoc;
                txtChandoantruoc.Text = _pkt.ChanDoan;
                txtXutri.Text = _pkt.XuTri;
                txtLydovaovien.Text = _pkt.LydoVaovien;
                nmrLancothai.Value = Utility.ByteDbnull(_pkt.LanKhamthai);
                nmrSolancothai.Value = Utility.ByteDbnull(_pkt.SolanKhamthai);

                if (_pkt.NgayDaukykinhcuoi.HasValue)
                    dtpNgaydaukykinhcuoi.Value = _pkt.NgayDaukykinhcuoi.Value;
                else
                    dtpNgaydaukykinhcuoi.NullValue = DBNull.Value;
                txtTuoithai.Text = Utility.sDbnull(_pkt.TuoiThai);
                if (_pkt.NgayDukiensinh.HasValue)
                    dtpNgaydukiensinh.Value = _pkt.NgayDukiensinh.Value;
                else
                    dtpNgaydukiensinh.NullValue = DBNull.Value;

                txtdienbienlamsang.Text = _pkt.DienbienLamsang;
                opttoanthan_binhthuong.Checked = Utility.Byte2Bool(_pkt.Toanthan);
                opttoanthan_batthuong.Checked = !Utility.Byte2Bool(_pkt.Toanthan);
                txttoanthan_batthuong_ghiro.Text = _pkt.ToanthanMota;
                nmrSomuitiemphonguonvan.Text = Utility.sDbnull(_pkt.SomuiTimphongUonvan, "");
                txttsb_chung.Text = _pkt.TiensuBenh;

                optdiung_khong.Checked = !Utility.Byte2Bool(_pkt.TsbDiung);
                optdiung_co.Checked = Utility.Byte2Bool(_pkt.TsbDiung);
                txtDiung.Text = _pkt.TsbDiungMota;

                opttsb_khong.Checked = !Utility.Byte2Bool(_pkt.TsbTsb);
                opttsb_co.Checked = Utility.Byte2Bool(_pkt.TsbTsb);
                txttsb_rieng.Text = _pkt.TsbTsbMota;

                optbenhha_co.Checked = Utility.Byte2Bool(_pkt.TsbBenhhuyetap);
                optbenhha_khong.Checked = !Utility.Byte2Bool(_pkt.TsbBenhhuyetap);

                optbenhtuyengiap_co.Checked = Utility.Byte2Bool(_pkt.TsbBenhtuyengiap);
                optbenhtuyengiap_khong.Checked = !Utility.Byte2Bool(_pkt.TsbBenhtuyengiap);
                optbenhtim_co.Checked = Utility.Byte2Bool(_pkt.TsbBenhtim);
                optbenhtim_khong.Checked = !Utility.Byte2Bool(_pkt.TsbBenhtim);
                optbenhthan_co.Checked = Utility.Byte2Bool(_pkt.TsbBenhthan);
                optbenhthan_khong.Checked = !Utility.Byte2Bool(_pkt.TsbBenhthan);

                optbenhhohap_co.Checked = Utility.Byte2Bool(_pkt.TsbBenhhohap);
                optbenhhohap_khong.Checked = !Utility.Byte2Bool(_pkt.TsbBenhhohap);

                optbenhdaithaoduong_co.Checked = Utility.Byte2Bool(_pkt.TsbBenhdaithaoduong);
                optbenhdaithaoduong_khong.Checked = !Utility.Byte2Bool(_pkt.TsbBenhdaithaoduong);
                txttsb_khac.Text = _pkt.TsbKhac;

                optthuocdangdung_co.Checked = Utility.Byte2Bool(_pkt.TsbThuocdangdung);
                optthuocdangdung_khong.Checked = !Utility.Byte2Bool(_pkt.TsbThuocdangdung);
                txtLoaithuoc.Text = _pkt.TsbThuocdangdungMota;

                opttiensuphauthuat_co.Checked = Utility.Byte2Bool(_pkt.TsbTiensuphauthuat);
                opttiensuphauthuat_khong.Checked = !Utility.Byte2Bool(_pkt.TsbTiensuphauthuat);
                txttiensuphauthuat_co.Text = _pkt.TsbTiensuphauthuatMota;

                txtPara.Text = _pkt.TsbTiensusankhoaPara;
                //Load tiền sử sản khoa
                optchukykinh_deu.Checked = Utility.Byte2Bool(_pkt.TsbChukykinh);
                optchukykinh_khongdeu.Checked = !Utility.Byte2Bool(_pkt.TsbChukykinh);
                nmrChukykinh.Value = Utility.ByteDbnull(_pkt.TsbChukykinhSongay);
                optphauthuatphukhoa_co.Checked = Utility.Byte2Bool(_pkt.TsbPhauthuatphukhoa);
                optphauthuatphukhoa_khong.Checked = !Utility.Byte2Bool(_pkt.TsbPhauthuatphukhoa);
                txtptpk_mota.Text = Utility.sDbnull(_pkt.TsbPhauthuatphukhoaMota);

                optkhoiubuongtrung_co.Checked = Utility.Byte2Bool(_pkt.TsbKhoiubuongtrung);
                optkhoiubuongtrung_khong.Checked = !Utility.Byte2Bool(_pkt.TsbKhoiubuongtrung);

                optdidangsinhduc_co.Checked = Utility.Byte2Bool(_pkt.TsbDidangsinhduc);
                optdidangsinhduc_khong.Checked = !Utility.Byte2Bool(_pkt.TsbDidangsinhduc);
                optkhoiutucung_co.Checked = Utility.Byte2Bool(_pkt.TsbKhoiutucung);
                optkhoiutucung_khong.Checked = !Utility.Byte2Bool(_pkt.TsbKhoiutucung);

                opttangsinhmon_co.Checked = Utility.Byte2Bool(_pkt.TsbTangsinhmon);
                opttangsinhmon_khong.Checked = !Utility.Byte2Bool(_pkt.TsbTangsinhmon);
                optsatangchau_co.Checked = Utility.Byte2Bool(_pkt.TsbSatangchau);
                optsatangchau_khong.Checked = !Utility.Byte2Bool(_pkt.TsbSatangchau);
                txtBenhphukhoa.Text = _pkt.TsbBenhphukhoaMota;

                opttsgd_co.Checked = Utility.Byte2Bool(_pkt.Tsgd);
                opttsgd_khong.Checked = !Utility.Byte2Bool(_pkt.Tsgd);
                txttsgd_mota.Text = Utility.sDbnull(_pkt.TsgdMota);

                opttsgd_dathai_co.Checked = Utility.Byte2Bool(_pkt.TsgdDathai);
                opttsgd_dathai_khong.Checked = !Utility.Byte2Bool(_pkt.TsgdDathai);
                opttsgd_didang_co.Checked = Utility.Byte2Bool(_pkt.TsgdDidang);
                opttsgd_didang_khong.Checked = !Utility.Byte2Bool(_pkt.TsgdDidang);
                opttsgd_dtd_co.Checked = Utility.Byte2Bool(_pkt.TsgdDaithaoduong);
                opttsgd_dtd_khong.Checked = !Utility.Byte2Bool(_pkt.TsgdDaithaoduong);
                opttsgd_benhditruyen_co.Checked = Utility.Byte2Bool(_pkt.TsgdBenhditruyen);
                opttsgd_benhditruyen_khong.Checked = !Utility.Byte2Bool(_pkt.TsgdBenhditruyen);
                txttsgd_khac.Text = Utility.sDbnull(_pkt.TsgdBenhditruyenMota);
                opttsgd_tha_co.Checked = Utility.Byte2Bool(_pkt.TsgdTanghuyetap);
                opttsgd_tha_khong.Checked = !Utility.Byte2Bool(_pkt.TsgdTanghuyetap);

                opttinhthan_tinhtao.Checked = Utility.ByteDbnull(_pkt.KcbTinhthan) == 0;
                opttinhthan_honme.Checked = Utility.ByteDbnull(_pkt.KcbTinhthan) == 1;
                opttinhthan_khac.Checked = Utility.ByteDbnull(_pkt.KcbTinhthan) == 2;

                optphu_co.Checked = Utility.Byte2Bool(_pkt.KcbPhu);
                optphu_khong.Checked = !Utility.Byte2Bool(_pkt.KcbPhu);

                optnieu_co.Checked = Utility.Byte2Bool(_pkt.KcbProteinnieu);
                optnieu_khong.Checked = !Utility.Byte2Bool(_pkt.KcbProteinnieu);
                txtnieu.Text = _pkt.KcbProteinnieuMota;

                optseomocu_khong.Checked = Utility.ByteDbnull(_pkt.KcbSeomocu) == 0;
                optseomocu_co.Checked = Utility.ByteDbnull(_pkt.KcbSeomocu) == 1;
                optseomocu_dauvetmo.Checked = Utility.ByteDbnull(_pkt.KcbSeomocu) == 2;

                optkhungchau_binhthuong.Checked = !Utility.Byte2Bool(_pkt.KcbKhungchau);
                optkhungchau_batthuong.Checked = Utility.Byte2Bool(_pkt.KcbKhungchau);
                nmrChieucaoCTC.Value = Utility.ByteDbnull(_pkt.KcbChieucaotucung);
                nmrVongbung.Value = Utility.ByteDbnull(_pkt.KcbVongbung);
                txtHinhdangTC.Text = Utility.sDbnull(_pkt.KcbHinhdangtucung);
                txtTutheTC.Text = Utility.sDbnull(_pkt.KcbTuthetucung);
                txtVu.Text = Utility.sDbnull(_pkt.KcbVu);
                optngoithai_binhthuong.Checked = !Utility.Byte2Bool(_pkt.KcbNgoithai);
                optngoithai_batthuong.Checked = Utility.Byte2Bool(_pkt.KcbNgoithai);
                txtngoithai_mota.Text = _pkt.KcbNgoithaiMota;

                optconcotucung_co.Checked = Utility.Byte2Bool(_pkt.KcbConcotucung);
                optconcotucung_khong.Checked = !Utility.Byte2Bool(_pkt.KcbConcotucung);
                txttanso.Text = Utility.sDbnull(_pkt.KcbConcotucungTanso);

                optctc_dong.Checked = Utility.ByteDbnull(_pkt.KcbCotucung) == 0;
                optctc_xoa.Checked = Utility.ByteDbnull(_pkt.KcbCotucung) == 1;
                optCTCmo.Checked = Utility.ByteDbnull(_pkt.KcbCotucung) == 2;
                nmrCTCmo.Value = Utility.ByteDbnull(_pkt.KcbCotucungMo);

                opttimthai_co.Checked = Utility.Byte2Bool(_pkt.KcbTimthai);
                opttimthai_khong.Checked = !Utility.Byte2Bool(_pkt.KcbTimthai);
                txtnhiptimthai.Text = Utility.sDbnull(_pkt.KcbTimthaiNhiptimthai);

                optdauoi_phong.Checked = Utility.ByteDbnull(_pkt.KcbDauoi) == 0;
                optdauoi_det.Checked = Utility.ByteDbnull(_pkt.KcbDauoi) == 1;
                optdauoi_quale.Checked = Utility.ByteDbnull(_pkt.KcbDauoi) == 2;
                optdauoi_ri.Checked = Utility.ByteDbnull(_pkt.KcbDauoi) == 3;
                optdauoi_vo.Checked = Utility.ByteDbnull(_pkt.KcbDauoi) == 4;
                txtdauoi_voluc.Text = Utility.sDbnull(_pkt.KcbDauoiMota);

                optnuocoi_trong.Checked = Utility.ByteDbnull(_pkt.KcbNuocoi) == 0;
                optnuocoi_xanhban.Checked = Utility.ByteDbnull(_pkt.KcbNuocoi) == 1;
                optnuocoi_lanmau.Checked = Utility.ByteDbnull(_pkt.KcbNuocoi) == 2;

                optxnthieumau_co.Checked = Utility.Byte2Bool(_pkt.ClsXnmaungoaiviKq);
                optxnthieumau_khong.Checked = !Utility.Byte2Bool(_pkt.ClsXnmaungoaiviKq);
                chkxnmaungoaivi_thieumau.Checked = Utility.Byte2Bool(_pkt.ClsXnmaungoaivi);

                optxnduongmau_binhthuong.Checked = !Utility.Byte2Bool(_pkt.ClsXnsinhoaKq);
                optxnduongmau_cao.Checked = Utility.Byte2Bool(_pkt.ClsXnsinhoaKq);
                chkxnsh_duongmau.Checked = Utility.Byte2Bool(_pkt.ClsXnsinhoa);

                optdongmau_batthuong.Checked = Utility.Byte2Bool(_pkt.ClsXndongmauKq);
                optdongmau_binhthuong.Checked = !Utility.Byte2Bool(_pkt.ClsXndongmauKq);
                chkxndongmau.Checked = Utility.Byte2Bool(_pkt.ClsXndongmau);

                optxnviemganb_duongtinh.Checked = Utility.Byte2Bool(_pkt.ClsViemganBKq);
                optxnviemganb_amtinh.Checked = !Utility.Byte2Bool(_pkt.ClsViemganBKq);
                chkxnviemganb.Checked = Utility.Byte2Bool(_pkt.ClsViemganB);
                optHIV_duongtinh.Checked = Utility.Byte2Bool(_pkt.ClsXnHIVKq);
                optHIV_amtinh.Checked = !Utility.Byte2Bool(_pkt.ClsXnHIVKq);
                chkxnHIV.Checked = Utility.Byte2Bool(_pkt.ClsXnHIV);

                optgiangmai_duongtinh.Checked = Utility.Byte2Bool(_pkt.ClsGiangmaiKq);
                optgiangmai_amtinh.Checked = !Utility.Byte2Bool(_pkt.ClsGiangmaiKq);
                chkxngiangmai.Checked = Utility.Byte2Bool(_pkt.ClsGiangmai);

                optxnnuoctieu_duongtinh.Checked = Utility.Byte2Bool(_pkt.ClsProteinnuoctieukq);
                optxnnuoctieu_amtinh.Checked = !Utility.Byte2Bool(_pkt.ClsProteinnuoctieukq);
                chkxnproteinnieu.Checked = Utility.Byte2Bool(_pkt.ClsProteinnuoctieu);

                optsa_batthuong.Checked = Utility.Byte2Bool(_pkt.ClsSieuamKq);
                optsa_binhthuong.Checked = !Utility.Byte2Bool(_pkt.ClsSieuamKq);
                chkxnsieuam.Checked = Utility.Byte2Bool(_pkt.ClsSieuam);

                txtxn_khac.Text = Utility.sDbnull(_pkt.ClsKhac);
                txtMach.Text = _pkt.Mach;
                txtha.Text = _pkt.Ha;
                txtNhietDo.Text = _pkt.NhietDo;
                txtChieucao.Text = _pkt.Cao;
                txtCannang.Text = _pkt.CanNang;
                txtBmi.Text = _pkt.Bmi;
                txtNhommau.Text = _pkt.Nhommau;
                txtNhiptho.Text = _pkt.Nhiptho;
                txtchandoan_icd.Text = Utility.sDbnull(_pkt.ChanDoanIcd);
                txtkehoachdieutri.Text = _pkt.ThuocChamsoc;
                txthuongdieutri.Text = _pkt.HuongDieutri;
                opttuvan_co.Checked = Utility.Byte2Bool(_pkt.Tuvan);
                opttuvan_khong.Checked = !Utility.Byte2Bool(_pkt.Tuvan);
                txttuvan_mota.Text = _pkt.TuvaMota;
                opttienluong_sinhthuong.Checked = Utility.ByteDbnull(_pkt.TienLuong) == 0;
                opttienluong_sinhnguyco.Checked = Utility.ByteDbnull(_pkt.TienLuong) == 1;
                opttienluong_chidinhmolaythai.Checked = Utility.ByteDbnull(_pkt.TienLuong) == 2;
                txthentaikham.Text = Utility.sDbnull(_pkt.HenTaikham);
                txtluuy.Text = _pkt.LuuY;

                optnguoikham_bssanphukhoa.Checked = Utility.ByteDbnull(_pkt.LoaiNhanvien) == 0;
                optnguoikham_hosinh.Checked = Utility.ByteDbnull(_pkt.LoaiNhanvien) == 1;
                optnguoikham_ysysannhi.Checked = Utility.ByteDbnull(_pkt.LoaiNhanvien) == 2;
                optnguoikham_khac.Checked = Utility.ByteDbnull(_pkt.LoaiNhanvien) == 3;


                txtThongtinlankhamtruoc.Focus();
                //Fill tiền sử khám
                dtTiensusankhoa = new Select(KcbPhieukhamthaiTiensusankhoa.Schema.Name + ".*", "'' as guid").From(KcbPhieukhamthaiTiensusankhoa.Schema).Where(KcbPhieukhamthaiTiensusankhoa.Columns.IdPhieukhamthai).IsEqualTo(_pkt.IdPhieukhamthai).ExecuteDataSet().Tables[0];
                
            }
        }

        public void ClearControl()
        {
            dtpNgaykhamtruoc.Value = DateTime.Now;
            txtThongtinlankhamtruoc.Clear();
            txtChandoantruoc.Clear();
            txtXutri.Clear();
            txtLydovaovien.Clear();
            nmrLancothai.Value = 1;
            nmrSolancothai.Value = 1;
            dtpNgaydaukykinhcuoi.Text = "";
            txttuvan_mota.Clear();
            dtpNgaydukiensinh.Text = "";
            txtdienbienlamsang.Clear();
            opttoanthan_binhthuong.Checked = true;
            opttoanthan_batthuong.Checked = false;
            txttoanthan_batthuong_ghiro.Clear();
            nmrSomuitiemphonguonvan.Value = 1;
            txttsb_chung.Clear();

            optdiung_khong.Checked = true;
            optdiung_co.Checked = false;
            txtDiung.Clear();

            opttsb_khong.Checked = true;
            opttsb_co.Checked = false;
            txttsb_rieng.Clear();

            optbenhha_co.Checked = false;
            optbenhha_khong.Checked = true;

            optbenhtuyengiap_co.Checked = false;
            optbenhtuyengiap_khong.Checked = true;
            optbenhtim_co.Checked = false;
            optbenhtim_khong.Checked = true;
            optbenhthan_co.Checked = false;
            optbenhthan_khong.Checked = true;

            optbenhhohap_co.Checked = false;
            optbenhhohap_khong.Checked = true;

            optbenhdaithaoduong_co.Checked = false;
            optbenhdaithaoduong_khong.Checked = true;
            txttsb_khac.Clear();

            optthuocdangdung_co.Checked = false;
            optthuocdangdung_khong.Checked = true;
            txtLoaithuoc.Clear();

            opttiensuphauthuat_co.Checked = false;
            opttiensuphauthuat_khong.Checked = true;
            txttiensuphauthuat_co.Clear();

            txtPara.Clear();
            //Load tiền sử sản khoa
            optchukykinh_deu.Checked = true;
            optchukykinh_khongdeu.Checked = false;
            nmrChukykinh.Value = 30;
            optphauthuatphukhoa_co.Checked = false;
            optphauthuatphukhoa_khong.Checked = true;
            txtptpk_mota.Clear();

            optkhoiubuongtrung_co.Checked = false;
            optkhoiubuongtrung_khong.Checked = true;

            optdidangsinhduc_co.Checked = false;
            optdidangsinhduc_khong.Checked = true;
            optkhoiutucung_co.Checked = false;
            optkhoiutucung_khong.Checked = true;

            opttangsinhmon_co.Checked = false;
            opttangsinhmon_khong.Checked = true;
            optsatangchau_co.Checked = false;
            optsatangchau_khong.Checked = true;
            txtBenhphukhoa.Clear();

            opttsgd_co.Checked = false;
            opttsgd_khong.Checked = true;
            txttsgd_mota.Clear();

            opttsgd_dathai_co.Checked = false;
            opttsgd_dathai_khong.Checked = true;
            opttsgd_didang_co.Checked = false;
            opttsgd_didang_khong.Checked = true;
            opttsgd_dtd_co.Checked = false;
            opttsgd_dtd_khong.Checked = true;
            opttsgd_benhditruyen_co.Checked = false;
            opttsgd_benhditruyen_khong.Checked = true;
            txttsgd_khac.Clear();
            opttsgd_tha_co.Checked = false;
            opttsgd_tha_khong.Checked = true;

            opttinhthan_tinhtao.Checked = true;
            opttinhthan_honme.Checked = false;
            opttinhthan_khac.Checked = false;

            optphu_co.Checked = false;
            optphu_khong.Checked = true;

            optnieu_co.Checked = false;
            optnieu_khong.Checked = true;
            txtnieu.Clear();

            optseomocu_khong.Checked = true;
            optseomocu_co.Checked = false;
            optseomocu_dauvetmo.Checked = false;

            optkhungchau_binhthuong.Checked = true;
            optkhungchau_batthuong.Checked = false;
            nmrChieucaoCTC.Value = 10;
            nmrVongbung.Value = 120;
            txtTutheTC.Clear();
            txtHinhdangTC.Clear();
            txtVu.Clear();
            optngoithai_binhthuong.Checked = true;
            optngoithai_batthuong.Checked = false;
            txtngoithai_mota.Clear();

            optconcotucung_co.Checked = false;
            optconcotucung_khong.Checked = true;
            txttanso.Clear();

            optctc_dong.Checked = false;
            optctc_xoa.Checked = false;
            optCTCmo.Checked = true;
            nmrCTCmo.Value = 0;

            opttimthai_co.Checked = true;
            opttimthai_khong.Checked = false;
            txtnhiptimthai.Clear();

            optdauoi_phong.Checked = true;
            optdauoi_det.Checked = false;
            optdauoi_quale.Checked = false;
            optdauoi_ri.Checked = false;
            optdauoi_vo.Checked = false;
            txtdauoi_voluc.Clear();

            optnuocoi_trong.Checked = true;
            optnuocoi_xanhban.Checked = false;
            optnuocoi_lanmau.Checked = false;

            optxnthieumau_co.Checked = false;
            optxnthieumau_khong.Checked = true;
            chkxnmaungoaivi_thieumau.Checked = false;

            optxnduongmau_binhthuong.Checked = true;
            optxnduongmau_cao.Checked = false;
            chkxnsh_duongmau.Checked = false;

            optdongmau_batthuong.Checked = false;
            optdongmau_binhthuong.Checked = true;
            chkxndongmau.Checked = false;

            optxnviemganb_duongtinh.Checked = false;
            optxnviemganb_amtinh.Checked = true;
            chkxnviemganb.Checked = false;
            optHIV_duongtinh.Checked = false;
            optHIV_amtinh.Checked = true;
            chkxnHIV.Checked = false;

            optgiangmai_duongtinh.Checked = false;
            optgiangmai_amtinh.Checked = true;
            chkxngiangmai.Checked = false;

            optxnnuoctieu_duongtinh.Checked = false;
            optxnnuoctieu_amtinh.Checked = true;
            chkxnproteinnieu.Checked = false;

            optsa_batthuong.Checked = false;
            optsa_binhthuong.Checked = true;
            chkxnsieuam.Checked = false;

            txtxn_khac.Clear();
            txtMach.Clear();
            txtha.Clear();
            txtNhietDo.Clear();
            txtChieucao.Clear();
            txtCannang.Clear();
            txtBmi.Clear();
            txtNhommau.Clear();

            txtchandoan_icd.Clear();
            txtkehoachdieutri.Clear();
            txthuongdieutri.Clear();
            opttuvan_co.Checked = false;
            opttuvan_khong.Checked = true;
            txttuvan_mota.Clear();
            opttienluong_sinhthuong.Checked = true;
            opttienluong_sinhnguyco.Checked = false;
            opttienluong_chidinhmolaythai.Checked = false;
            txthentaikham.Clear();
            txtluuy.Clear();

            optnguoikham_bssanphukhoa.Checked = true;
            optnguoikham_hosinh.Checked = false;
            optnguoikham_ysysannhi.Checked = false;
            optnguoikham_khac.Checked = false;


        }
        private void ModifyCommand()
        {
            cmdIn.Enabled = m_enAct == action.Update;
        }


        private void btnPrintTrich_pkt_Click(object sender, EventArgs e)
        {
            
        }
        public void InPhieuKhamThai()
        {
           
            try
            {
                DataTable dtData = SPs.KcbLaythongtinPhieukhamthaiIn(_pkt.IdPhieukhamthai).GetDataSet().Tables[0];
                Utility.UpdateLogotoDatatable(ref dtData);
                string tieude = "", reportname = "";
                ReportDocument reportDocument = Utility.GetReport("PHIEUKHAMTHAI", ref tieude, ref reportname);
                if (reportDocument == null) return;
                ReportDocument crpt = reportDocument;
                THU_VIEN_CHUNG.CreateXML(dtData, "phieukhamthai_report.XML");
                var objForm = new frmPrintPreview(tieude, crpt, true, dtData.Rows.Count > 0);

                dtData.AcceptChanges();
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "PHIEUKHAMTHAI";
                crpt.SetDataSource(dtData);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "HotLine", globalVariables.Branch_Fax);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
              
            }
        }

        private void btnPrinKcbPhieukhamthai_Click(object sender, EventArgs e)
        {
           
        }


        private Boolean isValidData()
        {
            //errorProvider1.Clear();
            //if (txtBacsidexuat.MyID == "-1")
            //{
            //    Utility.ShowMsg("Bạn phải nhập bác sĩ đề xuất hội chẩn");
            //    uiTabInfor.SelectedIndex = 0;
            //    errorProvider1.SetError(txtBacsidexuat, "Nhập thông tin");
            //    txtBacsidexuat.Focus();
            //    txtBacsidexuat.SelectAll();
            //    return false;
            //}
            //if (dtbsthamgia.Rows.Count <= 0)
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập ít nhất một bác sĩ tham gia hội chẩn");
            //    errorProvider1.SetError(autoBacsithamgia, "Nhập thông tin");
            //    autoBacsithamgia.Focus();
            //    autoBacsithamgia.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(txtHopTai.Text) == "")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập địa điểm họp hội chẩn");
            //    errorProvider1.SetError(txtHopTai, "Nhập thông tin");
            //    txtHopTai.Focus();
            //    txtHopTai.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(autoChutoa.Text) == "")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập chủ tọa hội chẩn");
            //    errorProvider1.SetError(autoBacsithamgia, "Nhập thông tin");
            //    autoChutoa.Focus();
            //    autoChutoa.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(autoThuki.Text) == "")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập thư kí hội chẩn");
            //    errorProvider1.SetError(autoThuki, "Nhập thông tin");
            //    autoThuki.Focus();
            //    autoThuki.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(txtYeuCauHoiChan.Text) == "")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập yêu cầu hội chẩn");
            //    errorProvider1.SetError(txtYeuCauHoiChan, "Nhập thông tin");
            //    txtYeuCauHoiChan.Focus();
            //    txtYeuCauHoiChan.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(autoLydohc.MyCode) == "-1")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập lý do hội chẩn");
            //    errorProvider1.SetError(autoLydohc, "Nhập thông tin");
            //    autoLydohc.Focus();
            //    autoLydohc.SelectAll();
            //    return false;
            //}
            //if (Utility.DoTrim(autohinhthuchc.MyCode) == "-1")
            //{
            //    uiTabInfor.SelectedIndex = 0;
            //    Utility.ShowMsg("Bạn phải nhập hình thức hội chẩn");
            //    errorProvider1.SetError(autohinhthuchc, "Nhập thông tin");
            //    autohinhthuchc.Focus();
            //    autohinhthuchc.SelectAll();
            //    return false;
            //}
            return true;
        }


        void LoadUserConfigs()
        {
            try
            {
                chkPreview.Checked = Utility.getUserConfigValue(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked)) == 1;

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked));

            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void frm_ThemPhieukhamthai_Load(object sender, EventArgs e)
        {
            LoadUserConfigs();
            if (_pkt != null && m_enAct == action.Update)
            {
                //FillData4Update();
            }
            else
            {
                dtTiensusankhoa = new Select(KcbPhieukhamthaiTiensusankhoa.Schema.Name + ".*","'' as guid").From(KcbPhieukhamthaiTiensusankhoa.Schema).Where(KcbPhieukhamthaiTiensusankhoa.Columns.IdPhieukhamthai).IsEqualTo(-1).ExecuteDataSet().Tables[0];
                ucThongtinnguoibenh_doc_v61.Refresh();
                ucThongtinnguoibenh_doc_v61.txtMaluotkham.Focus();
                

            }
            Utility.SetDataSourceForDataGridEx(grdLichsusankhoa, dtTiensusankhoa, true, true, "1=1", "id_tiensu_sankhoa");
            ModifyCommand();
        }
       

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string Layso_pkt()
        {
            string ma__pkt = "";
            StoredProcedure sp = SPs.SpGetMaBBHC(DateTime.Now.Year, ma__pkt);
            sp.Execute();
            return Utility.sDbnull(sp.OutputValues[0], "-1");
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidData()) return;
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lưu Phiếu khám thai?", "Thông báo", true)) return;
                if (_pkt == null) _pkt = new KcbPhieukhamthai();
                if (_pkt.IdPhieukhamthai <= 0)
                {
                    _pkt.IsNew = true;

                    _pkt.NguoiTao = globalVariables.UserName;
                    _pkt.NgayTao = globalVariables.SysDate;
                    _pkt.MacMaytao = globalVariables.gv_strMacAddress;
                    _pkt.IpMaytao = globalVariables.gv_strIPAddress;
                }
                else
                {
                    _pkt.MarkOld();
                    _pkt.IsNew = false;

                    _pkt.NguoiSua = globalVariables.UserName;
                    _pkt.NgaySua = globalVariables.SysDate;
                    _pkt.MacMaysua = globalVariables.gv_strMacAddress;
                    _pkt.IpMaysua = globalVariables.gv_strIPAddress;
                }
                _pkt.IdBenhnhan = objLuotkham.IdBenhnhan;
                _pkt.MaLuotkham = objLuotkham.MaLuotkham;
                _pkt.NgayKham = dtpNgaykhamtruoc.Value;
                _pkt.ThongtinLankhamtruoc = Utility.sDbnull(txtThongtinlankhamtruoc.Text);
                _pkt.ChanDoan = Utility.sDbnull(txtChandoantruoc.Text);
                _pkt.XuTri = Utility.sDbnull(txtXutri.Text);
                _pkt.LydoVaovien = Utility.sDbnull(txtLydovaovien.Text);
                _pkt.LanKhamthai = Utility.ByteDbnull(nmrLancothai.Value);
                _pkt.SolanKhamthai = Utility.ByteDbnull(nmrSolancothai.Value);

                _pkt.NgayDaukykinhcuoi = dtpNgaydaukykinhcuoi.Value;
                _pkt.TuoiThai = Utility.sDbnull(txtTuoithai.Text);
                _pkt.NgayDukiensinh = dtpNgaydukiensinh.Value;

                _pkt.DienbienLamsang = Utility.sDbnull(txtdienbienlamsang.Text);
                _pkt.Toanthan = Utility.Bool2byte(opttoanthan_batthuong.Checked);
                _pkt.ToanthanMota = Utility.sDbnull(txttoanthan_batthuong_ghiro.Text);
                _pkt.SomuiTimphongUonvan = Utility.ByteDbnull(nmrSomuitiemphonguonvan.Value);
                _pkt.TiensuBenh = Utility.sDbnull(txttsb_chung.Text);

                _pkt.TsbDiung = Utility.Bool2byte(optdiung_co.Checked);
                _pkt.TsbDiungMota = Utility.sDbnull(txtDiung.Text);

                _pkt.TsbTsb = Utility.Bool2byte(opttsb_co.Checked);
                _pkt.TsbTsbMota = Utility.sDbnull(txttsb_rieng.Text);

                _pkt.TsbBenhhuyetap = Utility.Bool2byte(optbenhha_co.Checked);

                _pkt.TsbBenhtuyengiap = Utility.Bool2byte(optbenhtuyengiap_co.Checked);
                _pkt.TsbBenhtim = Utility.Bool2byte(optbenhtim_co.Checked);
                _pkt.TsbBenhthan = Utility.Bool2byte(optbenhthan_co.Checked);
                _pkt.TsbBenhhohap = Utility.Bool2byte(optbenhhohap_co.Checked);
                _pkt.TsbBenhdaithaoduong = Utility.Bool2byte(optbenhdaithaoduong_co.Checked);



                _pkt.TsbKhac = Utility.sDbnull(txttsb_khac.Text);

                _pkt.TsbThuocdangdung = Utility.Bool2byte(optthuocdangdung_co.Checked);
                _pkt.TsbTiensuphauthuat = Utility.Bool2byte(opttiensuphauthuat_co.Checked);
                _pkt.TsbThuocdangdungMota = Utility.sDbnull(txtLoaithuoc.Text);
                _pkt.TsbTiensuphauthuatMota = Utility.sDbnull(txttiensuphauthuat_co.Text);
                _pkt.TsbTiensusankhoaPara = Utility.sDbnull(txtPara.Text);
                //Load tiền sử sản khoa
                _pkt.TsbChukykinh = Utility.Bool2byte(optchukykinh_khongdeu.Checked);
                _pkt.TsbPhauthuatphukhoa = Utility.Bool2byte(optphauthuatphukhoa_co.Checked);
                _pkt.TsbKhoiubuongtrung = Utility.Bool2byte(optkhoiubuongtrung_co.Checked);
                _pkt.TsbDidangsinhduc = Utility.Bool2byte(optdidangsinhduc_co.Checked);
                _pkt.TsbKhoiutucung = Utility.Bool2byte(optkhoiutucung_co.Checked);
                _pkt.TsbTangsinhmon = Utility.Bool2byte(opttangsinhmon_co.Checked);
                _pkt.TsbSatangchau = Utility.Bool2byte(optsatangchau_co.Checked);



                _pkt.TsbChukykinhSongay = Utility.ByteDbnull(nmrChukykinh.Value);
                _pkt.TsbPhauthuatphukhoaMota = Utility.sDbnull(txtptpk_mota.Text);

                _pkt.TsbBenhphukhoaMota = Utility.sDbnull(txtBenhphukhoa.Text);

                _pkt.Tsgd = Utility.Bool2byte(opttsgd_co.Checked);
                _pkt.TsgdDathai = Utility.Bool2byte(opttsgd_dathai_co.Checked);
                _pkt.TsgdMota = Utility.sDbnull(txttsgd_mota.Text);
                _pkt.TsgdDidang = Utility.Bool2byte(opttsgd_didang_co.Checked);
                _pkt.TsgdDaithaoduong = Utility.Bool2byte(opttsgd_dtd_co.Checked);
                _pkt.TsgdBenhditruyen = Utility.Bool2byte(opttsgd_benhditruyen_co.Checked);
                _pkt.TsgdTanghuyetap = Utility.Bool2byte(opttsgd_tha_co.Checked);


                _pkt.TsgdBenhditruyenMota = Utility.sDbnull(txttsgd_khac.Text);

                _pkt.TsgdDathai = Utility.Bool2byte(opttsgd_dathai_co.Checked);
                _pkt.TsgdDathai = Utility.Bool2byte(opttsgd_dathai_co.Checked);
                _pkt.TsgdDathai = Utility.Bool2byte(opttsgd_dathai_co.Checked);

                _pkt.KcbTinhthan = Utility.ByteDbnull(opttinhthan_tinhtao.Checked ? 0 : (opttinhthan_honme.Checked ? 1 : 2));
                _pkt.KcbPhu = Utility.Bool2byte(optphu_co.Checked);
                _pkt.KcbProteinnieu = Utility.Bool2byte(optnieu_co.Checked);




                _pkt.KcbProteinnieuMota = Utility.sDbnull(txtnieu.Text);
                _pkt.KcbSeomocu = Utility.ByteDbnull(optseomocu_khong.Checked ? 0 : (optseomocu_co.Checked ? 1 : 2));
                _pkt.KcbKhungchau = Utility.Bool2byte(optkhungchau_batthuong.Checked);


                _pkt.KcbChieucaotucung = Utility.ByteDbnull(nmrChieucaoCTC.Value);
                _pkt.KcbVongbung = Utility.ByteDbnull(nmrVongbung.Value);
                _pkt.KcbTuthetucung = Utility.sDbnull(txtTutheTC.Text);
                _pkt.KcbHinhdangtucung = Utility.sDbnull(txtHinhdangTC.Text);
                _pkt.KcbVu = Utility.sDbnull(txtVu.Text);
                _pkt.KcbNgoithai = Utility.Bool2byte(optngoithai_batthuong.Checked);
                _pkt.KcbConcotucung = Utility.Bool2byte(optconcotucung_co.Checked);
                _pkt.KcbCotucung = Utility.ByteDbnull(optctc_dong.Checked ? 0 : (optctc_xoa.Checked ? 1 : 2));
                _pkt.KcbTimthai = Utility.Bool2byte(opttimthai_co.Checked);
                _pkt.KcbDauoi = Utility.ByteDbnull(optdauoi_phong.Checked ? 0 : (optdauoi_det.Checked ? 1 : (optdauoi_quale.Checked ? 2 : (optdauoi_ri.Checked ? 3 : 4))));

                optngoithai_binhthuong.Checked = !Utility.Byte2Bool(_pkt.KcbNgoithai);

                txtngoithai_mota.Text = Utility.sDbnull(_pkt.KcbNgoithaiMota);

                txttanso.Text = Utility.sDbnull(_pkt.KcbConcotucungTanso);

                nmrCTCmo.Value = Utility.ByteDbnull(_pkt.KcbCotucungMo);

                txtnhiptimthai.Text = Utility.sDbnull(_pkt.KcbTimthaiNhiptimthai);

                _pkt.KcbDauoiMota = Utility.sDbnull(txtdauoi_voluc.Text);
                _pkt.KcbNuocoi = Utility.ByteDbnull(optnuocoi_trong.Checked ? 0 : (optnuocoi_xanhban.Checked ? 1 : 2));
                _pkt.ClsXnmaungoaiviKq = Utility.Bool2byte(optxnthieumau_co.Checked);
                _pkt.ClsXnmaungoaivi = Utility.Bool2byte(chkxnmaungoaivi_thieumau.Checked);
                _pkt.ClsXnsinhoaKq = Utility.Bool2byte(optxnduongmau_cao.Checked);
                _pkt.ClsXnsinhoa = Utility.Bool2byte(chkxnsh_duongmau.Checked);
                _pkt.ClsXndongmauKq = Utility.Bool2byte(optdongmau_batthuong.Checked);
                _pkt.ClsXndongmau = Utility.Bool2byte(chkxndongmau.Checked);
                _pkt.ClsViemganBKq = Utility.Bool2byte(optxnviemganb_duongtinh.Checked);
                _pkt.ClsViemganB = Utility.Bool2byte(chkxnviemganb.Checked);
                _pkt.ClsXnHIVKq = Utility.Bool2byte(optHIV_duongtinh.Checked);
                _pkt.ClsXnHIV = Utility.Bool2byte(chkxnHIV.Checked);
                _pkt.ClsGiangmaiKq = Utility.Bool2byte(optgiangmai_duongtinh.Checked);
                _pkt.ClsGiangmai = Utility.Bool2byte(chkxngiangmai.Checked);
                _pkt.ClsProteinnuoctieukq = Utility.Bool2byte(optxnnuoctieu_duongtinh.Checked);
                _pkt.ClsProteinnuoctieu = Utility.Bool2byte(chkxnproteinnieu.Checked);
                _pkt.ClsSieuamKq = Utility.Bool2byte(optsa_batthuong.Checked);
                _pkt.ClsSieuam = Utility.Bool2byte(chkxnsieuam.Checked);





                _pkt.ClsKhac = Utility.sDbnull(txtxn_khac.Text);
                _pkt.Mach = Utility.sDbnull(txtMach.Text);
                _pkt.Nhiptho = Utility.sDbnull(txtNhiptho.Text);
                _pkt.Ha = Utility.sDbnull(txtha.Text);
                _pkt.NhietDo = Utility.sDbnull(txtNhietDo.Text);
                _pkt.Cao = Utility.sDbnull(txtChieucao.Text);
                _pkt.CanNang = Utility.sDbnull(txtCannang.Text);
                _pkt.Bmi = Utility.sDbnull(txtBmi.Text);
                _pkt.Nhommau = Utility.sDbnull(txtNhommau.Text);

                _pkt.ChanDoanIcd = Utility.sDbnull(txtchandoan_icd.Text);
                _pkt.ThuocChamsoc = Utility.sDbnull(txtkehoachdieutri.Text);
                _pkt.HuongDieutri = Utility.sDbnull(txthuongdieutri.Text);
                _pkt.Tuvan = Utility.Bool2byte(opttuvan_co.Checked);
                _pkt.TienLuong = Utility.ByteDbnull(opttienluong_sinhthuong.Checked ? 0 : (opttienluong_sinhnguyco.Checked ? 1 : 2));



                _pkt.TuvaMota = Utility.sDbnull(txttuvan_mota.Text);
                _pkt.HenTaikham = Utility.sDbnull(txthentaikham.Text);
                _pkt.LuuY = Utility.sDbnull(txtluuy.Text);
                _pkt.LoaiNhanvien = Utility.ByteDbnull(optnguoikham_bssanphukhoa.Checked ? 0 : (optnguoikham_hosinh.Checked ? 1 : (optnguoikham_ysysannhi.Checked ? 2 : 3)));
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        _pkt.Save();
                        if (Utility.sDbnull(_pkt.Nhommau).Length > 0)
                            new Update(KcbDanhsachBenhnhan.Schema).Set(KcbDanhsachBenhnhan.Columns.NhomMau).EqualTo(_pkt.Nhommau).Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(_pkt.IdBenhnhan).Execute();
                    }
                    Scope.Complete();
                }
                foreach (GridEXRow _row in grdLichsusankhoa.GetDataRows())
                {
                    if (Utility.Int64Dbnull(_row.Cells[KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa].Value) <= 0)
                    {
                        _row.BeginEdit();
                        KcbPhieukhamthaiTiensusankhoa _newItem = new KcbPhieukhamthaiTiensusankhoa();
                        _newItem.IdPhieukhamthai = _pkt.IdPhieukhamthai;
                        _newItem.ThoigianNoiKetthuc = Utility.sDbnull(_row.Cells[KcbPhieukhamthaiTiensusankhoa.Columns.ThoigianNoiKetthuc].Value);
                        _newItem.TuoiThai = Utility.sDbnull(_row.Cells[KcbPhieukhamthaiTiensusankhoa.Columns.TuoiThai].Value);
                        _newItem.DienBien = Utility.sDbnull(_row.Cells[KcbPhieukhamthaiTiensusankhoa.Columns.DienBien].Value);
                        _newItem.CachSinh = Utility.sDbnull(_row.Cells[KcbPhieukhamthaiTiensusankhoa.Columns.CachSinh].Value);
                        _newItem.MotaTresosinh = Utility.sDbnull(_row.Cells[KcbPhieukhamthaiTiensusankhoa.Columns.MotaTresosinh].Value);
                        _newItem.HauSan = Utility.sDbnull(_row.Cells[KcbPhieukhamthaiTiensusankhoa.Columns.HauSan].Value);
                        _newItem.Save();
                        _row.Cells[KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa].Value = _newItem.IdTiensuSankhoa;
                        _row.EndEdit();
                    }

                }
                grdLichsusankhoa.UpdateData();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới Phiếu khám thai bệnh nhân: {0}-{1} thành công", _pkt.MaLuotkham, ucThongtinnguoibenh_doc_v61.txtTenBN.Text), _pkt.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã thêm mới Phiếu khám thai thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Phiếu khám thai bệnh nhân: {0}-{1} thành công", _pkt.MaLuotkham, ucThongtinnguoibenh_doc_v61.txtTenBN.Text), _pkt.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã Cập nhật Phiếu khám thai thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
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
      
        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            if (m_enAct == action.Insert)
            {
                if (!Utility.AcceptQuestion("Bạn đang ở trạng thái thêm mới Phiếu khám thai và có thể đã nhập một số thông tin. Nếu nhấn thêm mới các thông tin mới nhập có thể bị xóa.\nBạn có chắc chắn muốn làm lại từ đầu không?", "Xác nhận", true))
                {
                    return;
                }
            }
            m_enAct = action.Insert;
            cmdIn.Enabled = m_enAct == action.Update;
            ClearControl();
            ucThongtinnguoibenh_doc_v61.txtMaluotkham.Focus();
            ucThongtinnguoibenh_doc_v61.txtMaluotkham.SelectAll();
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {

            try
            {
                if (_pkt == null || _pkt.IdPhieukhamthai <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo Phiếu khám thai trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.KcbLaythongtinPhieukhamthaiIn(_pkt.IdPhieukhamthai).GetDataSet().Tables[0];

                List<string> lstAddedFields = new List<string>() {"doituong_thuphi","doituong_mienphi","doituong_khac", "toanthan_binhthuong", "toanthan_batthuong", "diung_khong",
                "diung_co", "tsb_khong", "tsb_co",
                "huyetap_khong","huyetap_co",  "tuyengiap_khong","tuyengiap_co","benhtim_khong","benhtim_co","benhthan_khong","benhthan_co","benhhohap_khong","benhhohap_co","benhdaithaoduong_khong"
                ,"benhdaithaoduong_co", "thuocdangdung_khong","thuocdangdung_co","tiensuphauthuat_khong","tiensuphauthuat_co","chukykinh_deu","chukykinh_khongdeu"
                ,"phauthuatphukhoa_khong","phauthuatphukhoa_co","khoiubuongtrung_khong","khoiubuongtrung_co","didangsinhduc_co","didangsinhduc_khong"
                ,"khoiutucung_co","khoiutucung_khong","tangsinhmon_khong","tangsinhmon_co","satangchau_khong","satangchau_co"
                ,"tsgd_khong","tsgd_co","tsgddathai_khong","tsgddathai_co","tsgddaithaoduong_khong","tsgddaithaoduong_co","tsgddidang_khong","tsgddidang_co"
                ,"tsgdtanghuyetap_khong","tsgdtanghuyetap_co","tsgdbenhditruyen_khong","tsgdbenhditruyen_co"
                ,"kcbtinhthan_tinhtao","kcbtinhthan_honme","kcbtinhthan_khac","kcbphu_khong","kcbphu_co","kcbproteinnieu_khong","kcbproteinnieu_co"
                ,"kcbseomocu_khong","kcbseomocu_co","kcbseomocu_dauvetmo","kcbkhungchau_binhthuong","kcbkhungchau_batthuong"
                ,"kcbngoithai_binhthuong","kcbngoithai_batthuong","kcbconcotucung_khong","kcbconcotucung_co","kcbtimthai_khong","kcbtimthai_co"
                ,"kcbcotucung_dong","kcbcotucung_xoa","kcbcotucung_mo","kcbdauoi_phong","kcbdauoi_det","kcbdauoi_quale","kcbdauoi_ri","kcbdauoi_vo"
                ,"kcbnuocoi_trong","kcbnuocoi_xanhban","kcbnuocoi_lanmau","xnmaungoaivi_thieumau","xnmaungoaivi_khong","xnmaungoaivi_co"
                ,"xndongmau_dongmau","xndongmau_binhthuong","xndongmau_batthuong"
                ,"xnsinhhoamau_duongmau","xnsinhhoamau_binhthuong","xnsinhhoamau_cao"
                ,"xnhvi_hiv","xnhvi_amtinh","xnhvi_duongtinh"
                ,"xnviemganb_viemganb","xnviemganb_amtinh","xnviemganb_duongtinh"
                ,"xmgiangmai_giangmai","xmgiangmai_amtinh","xmgiangmai_duongtinh"
                ,"xmprotein_proteinnieu","xmprotein_amtinh","xmprotein_duongtinh"
                ,"sieuam_tinhtrang","sieuam_binhthuong","sieuam_batthuong",
                "tuvan_khong","tuvan_co","tienluong_sinhthuong","tienluong_sinhconguyco","tienluong_chidinhmolaythai"
                ,"bacsisanphukhoa","hosinh","ysysannhi","nguoikham_khac"
                };
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("toanthan_binhthuong",Utility.Byte2Bool( _pkt.Toanthan) ? "0" : "1");
                dicMF.Add("toanthan_batthuong", Utility.Byte2Bool(_pkt.Toanthan) ? "1" : "0");
                dicMF.Add("diung_khong", Utility.Byte2Bool(_pkt.TsbDiung) ? "0" : "1");
                dicMF.Add("diung_co", Utility.Byte2Bool(_pkt.TsbDiung) ? "1" : "0");
                dicMF.Add("tsb_khong", Utility.Byte2Bool(_pkt.TsbTsb) ? "0" : "1");
                dicMF.Add("tsb_co", Utility.Byte2Bool(_pkt.TsbTsb) ? "1" : "0");
                dicMF.Add("huyetap_khong", Utility.Byte2Bool(_pkt.Ha)? "0" : "1");
                dicMF.Add("huyetap_co", Utility.Byte2Bool(_pkt.Ha) ? "1" : "0");
                dicMF.Add("tuyengiap_khong", Utility.Byte2Bool(_pkt.TsbBenhtuyengiap) ? "0" : "1");
                dicMF.Add("tuyengiap_co", Utility.Byte2Bool(_pkt.TsbBenhtuyengiap) ? "1" : "0");

                dicMF.Add("benhtim_khong",Utility.Byte2Bool(_pkt.TsbBenhtim) ? "0" : "1");
                dicMF.Add("benhtim_co", Utility.Byte2Bool(_pkt.TsbBenhtim) ? "1" : "0");
                dicMF.Add("benhthan_khong",Utility.Byte2Bool(_pkt.TsbBenhthan) ? "0" : "1");
                dicMF.Add("benhthan_co", Utility.Byte2Bool(_pkt.TsbBenhthan) ? "1" : "0");
                dicMF.Add("benhhohap_khong",Utility.Byte2Bool(_pkt.TsbBenhhohap) ? "0" : "1");
                dicMF.Add("benhhohap_co", Utility.Byte2Bool(_pkt.TsbBenhhohap)? "1" : "0");
                dicMF.Add("benhdaithaoduong_khong",Utility.Byte2Bool(_pkt.TsbBenhdaithaoduong) ? "0" : "1");
                dicMF.Add("benhdaithaoduong_co",Utility.Byte2Bool(_pkt.TsbBenhdaithaoduong) ? "1" : "0");
                dicMF.Add("thuocdangdung_khong",Utility.Byte2Bool(_pkt.TsbThuocdangdung) ? "0" : "1");
                dicMF.Add("thuocdangdung_co", Utility.Byte2Bool(_pkt.TsbThuocdangdung) ? "1" : "0");

                          

                dicMF.Add("tiensuphauthuat_khong", Utility.Byte2Bool(_pkt.TsbTiensuphauthuat)? "0" : "1");
                dicMF.Add("tiensuphauthuat_co",Utility.Byte2Bool(_pkt.TsbTiensuphauthuat) ? "1" : "0");
                dicMF.Add("chukykinh_deu", Utility.Byte2Bool(_pkt.TsbChukykinh) ? "0" : "1");
                dicMF.Add("chukykinh_khongdeu", Utility.Byte2Bool(_pkt.TsbChukykinh)? "1" : "0");
                dicMF.Add("phauthuatphukhoa_khong",Utility.Byte2Bool(_pkt.TsbPhauthuatphukhoa) ? "0" : "1");
                dicMF.Add("phauthuatphukhoa_co",Utility.Byte2Bool(_pkt.TsbPhauthuatphukhoa) ? "1" : "0");
                dicMF.Add("khoiubuongtrung_khong", Utility.Byte2Bool( _pkt.TsbKhoiubuongtrung.Value) ? "0" : "1");
                dicMF.Add("khoiubuongtrung_co", Utility.Byte2Bool(_pkt.TsbKhoiubuongtrung) ? "1" : "0");
                dicMF.Add("didangsinhduc_co",Utility.Byte2Bool(_pkt.TsbDidangsinhduc) ? "1" : "0");
                dicMF.Add("didangsinhduc_khong", Utility.Byte2Bool(_pkt.TsbDidangsinhduc) ? "0" : "1");

                dicMF.Add("khoiutucung_co", Utility.Byte2Bool(_pkt.TsbKhoiutucung)  ? "1" : "0");
                dicMF.Add("khoiutucung_khong", Utility.Byte2Bool(_pkt.TsbKhoiutucung) ? "0" : "1");
                dicMF.Add("tangsinhmon_khong", Utility.Byte2Bool(_pkt.TsbTangsinhmon)  ? "0" : "1");
                dicMF.Add("tangsinhmon_co", Utility.Byte2Bool(_pkt.TsbTangsinhmon) ? "1" : "0");
                dicMF.Add("satangchau_khong", Utility.Byte2Bool(_pkt.TsbSatangchau)  ? "0" : "1");
                dicMF.Add("satangchau_co", Utility.Byte2Bool(_pkt.TsbSatangchau) ? "1" : "0");
                dicMF.Add("tsgd_khong",Utility.Byte2Bool(_pkt.Tsgd)  ? "0" : "1");
                dicMF.Add("tsgd_co", Utility.Byte2Bool(_pkt.Tsgd) ? "1" : "0");
                dicMF.Add("tsgddathai_khong", Utility.Byte2Bool(_pkt.TsgdDathai)  ? "0" : "1");
                dicMF.Add("tsgddathai_co", Utility.Byte2Bool(_pkt.TsgdDathai) ? "1" : "0");

             
                 dicMF.Add("tsgddaithaoduong_khong", Utility.Byte2Bool(_pkt.TsgdDaithaoduong)  ? "0" : "1");
                 dicMF.Add("tsgddaithaoduong_co", Utility.Byte2Bool(_pkt.TsgdDaithaoduong) ? "1" : "0");
                dicMF.Add("tsgddidang_khong", Utility.Byte2Bool(_pkt.TsgdDidang)  ? "0" : "1");
                dicMF.Add("tsgddidang_co", Utility.Byte2Bool(_pkt.TsgdDidang) ? "1" : "0");
                dicMF.Add("tsgdtanghuyetap_khong", Utility.Byte2Bool(_pkt.TsgdTanghuyetap)  ? "0" : "1");
                dicMF.Add("tsgdtanghuyetap_co", Utility.Byte2Bool(_pkt.TsgdTanghuyetap) ? "1" : "0");
                dicMF.Add("tsgdbenhditruyen_khong", Utility.Byte2Bool(_pkt.TsgdBenhditruyen) ? "0" : "1");
                dicMF.Add("tsgdbenhditruyen_co", Utility.Byte2Bool(_pkt.TsgdBenhditruyen) ? "1" : "0");
                dicMF.Add("kcbtinhthan_tinhtao", Utility.ByteDbnull(_pkt.KcbTinhthan)==0 ? "1" : "0");
                dicMF.Add("kcbtinhthan_honme", Utility.ByteDbnull(_pkt.KcbTinhthan) == 1 ? "1" : "0");

                dicMF.Add("kcbtinhthan_khac", Utility.ByteDbnull(_pkt.KcbTinhthan) == 2 ? "1" : "0");
                dicMF.Add("kcbphu_khong", Utility.Byte2Bool(_pkt.KcbPhu) ? "0" : "1");
                dicMF.Add("kcbphu_co", Utility.Byte2Bool(_pkt.KcbPhu) ? "1" : "0");
                dicMF.Add("kcbproteinnieu_khong", Utility.Byte2Bool(_pkt.KcbProteinnieu) ? "0" : "1");
                dicMF.Add("kcbproteinnieu_co", Utility.Byte2Bool(_pkt.KcbProteinnieu) ? "1" : "0");
                dicMF.Add("kcbseomocu_khong", Utility.ByteDbnull(_pkt.KcbSeomocu) == 0 ? "1" : "0");
                dicMF.Add("kcbseomocu_co", Utility.ByteDbnull(_pkt.KcbSeomocu) == 1 ? "1" : "0");
                dicMF.Add("kcbseomocu_dauvetmo", Utility.ByteDbnull(_pkt.KcbSeomocu) == 2 ? "1" : "0");
                dicMF.Add("kcbkhungchau_batthuong", Utility.Byte2Bool(_pkt.KcbKhungchau) ? "1" : "0");
                dicMF.Add("kcbkhungchau_binhthuong", Utility.Byte2Bool(_pkt.KcbKhungchau) ? "0" : "1");
                dicMF.Add("kcbngoithai_binhthuong", Utility.Byte2Bool(_pkt.KcbNgoithai) ? "0" : "1");
                dicMF.Add("kcbngoithai_batthuong", Utility.Byte2Bool(_pkt.KcbNgoithai) ? "1" : "0");
                dicMF.Add("kcbconcotucung_khong", Utility.Byte2Bool(_pkt.KcbConcotucung) ? "0" : "1");
                dicMF.Add("kcbconcotucung_co", Utility.Byte2Bool(_pkt.KcbConcotucung) ? "1" : "0");
                dicMF.Add("kcbcotucung_dong", Utility.ByteDbnull(_pkt.KcbTimthai) == 0 ? "1" : "0");
                dicMF.Add("kcbcotucung_xoa", Utility.ByteDbnull(_pkt.KcbConcotucung) == 1 ? "1" : "0");
                dicMF.Add("kcbcotucung_mo", Utility.ByteDbnull(_pkt.KcbTimthai) ==2? "1" : "0");
                
                dicMF.Add("kcbtimthai_khong", Utility.Byte2Bool(_pkt.KcbTimthai) ? "0" : "1");
                dicMF.Add("kcbtimthai_co", Utility.Byte2Bool(_pkt.KcbConcotucung) ? "1" : "0");

                
                dicMF.Add("kcbdauoi_phong", Utility.ByteDbnull(_pkt.KcbDauoi) == 0 ? "1" : "0");
                dicMF.Add("kcbdauoi_det", Utility.ByteDbnull(_pkt.KcbDauoi) == 1 ? "1" : "0");
                dicMF.Add("kcbdauoi_quale", Utility.ByteDbnull(_pkt.KcbDauoi) == 2 ? "1" : "0");

                dicMF.Add("kcbdauoi_ri", Utility.ByteDbnull(_pkt.KcbDauoi) == 3 ? "1" : "0");
                dicMF.Add("kcbdauoi_vo", Utility.ByteDbnull(_pkt.KcbDauoi) == 4 ? "1" : "0");
                dicMF.Add("kcbnuocoi_trong", Utility.ByteDbnull(_pkt.KcbNuocoi) == 0 ? "1" : "0");
                dicMF.Add("kcbnuocoi_xanhban", Utility.ByteDbnull(_pkt.KcbNuocoi) == 1 ? "1" : "0");
                dicMF.Add("kcbnuocoi_lanmau", Utility.ByteDbnull(_pkt.KcbNuocoi) == 2 ? "1" : "0");
                dicMF.Add("xnmaungoaivi_thieumau", Utility.Byte2Bool(_pkt.ClsXnmaungoaivi) ? "1" : "0");
                dicMF.Add("xnmaungoaivi_khong", Utility.Byte2Bool(_pkt.ClsXnmaungoaiviKq) ? "0" : "1");
                dicMF.Add("xnmaungoaivi_co", Utility.Byte2Bool(_pkt.ClsXnmaungoaiviKq) ? "1" : "0");
               
                dicMF.Add("xndongmau_dongmau", Utility.Byte2Bool(_pkt.ClsXndongmau) ? "1" : "0");
                dicMF.Add("xndongmau_binhthuong", Utility.Byte2Bool(_pkt.ClsXndongmauKq) ? "0" : "1");
                dicMF.Add("xndongmau_batthuong", Utility.Byte2Bool(_pkt.ClsXndongmauKq) ? "1" : "0");

                dicMF.Add("xnsinhhoamau_duongmau", Utility.Byte2Bool(_pkt.ClsXnsinhoa) ? "1" : "0");
                dicMF.Add("xnsinhhoamau_binhthuong", Utility.Byte2Bool(_pkt.ClsXnsinhoaKq) ? "0" : "1");
                dicMF.Add("xnsinhhoamau_cao", Utility.Byte2Bool(_pkt.ClsXnsinhoaKq) ? "1" : "0");
               
                dicMF.Add("xnhvi_hiv", Utility.Byte2Bool(_pkt.ClsXnHIV) ? "1" : "0");
                dicMF.Add("xnhvi_amtinh", Utility.Byte2Bool(_pkt.ClsXnHIVKq) ? "0" : "1");
                dicMF.Add("xnhvi_duongtinh", Utility.Byte2Bool(_pkt.ClsXnHIVKq) ? "1" : "0");
               
                dicMF.Add("xnviemganb_viemganb", Utility.Byte2Bool(_pkt.ClsViemganB) ? "1" : "0");
                dicMF.Add("xnviemganb_amtinh", Utility.Byte2Bool(_pkt.ClsViemganBKq) ? "0" : "1");
                dicMF.Add("xnviemganb_duongtinh", Utility.Byte2Bool(_pkt.ClsViemganBKq) ? "1" : "0");
                
                dicMF.Add("xmgiangmai_giangmai", Utility.Byte2Bool(_pkt.ClsGiangmai) ? "1" : "0");
                dicMF.Add("xmgiangmai_amtinh", Utility.Byte2Bool(_pkt.ClsGiangmaiKq) ? "0" : "1");
                dicMF.Add("xmgiangmai_duongtinh", Utility.Byte2Bool(_pkt.ClsGiangmaiKq) ? "1" : "0");
                
                dicMF.Add("xmprotein_proteinnieu", Utility.Byte2Bool(_pkt.ClsProteinnuoctieu) ? "1" : "0");
                dicMF.Add("xmprotein_amtinh", Utility.Byte2Bool(_pkt.ClsProteinnuoctieukq) ? "0" : "1");
                dicMF.Add("xmprotein_duongtinh", Utility.Byte2Bool(_pkt.ClsProteinnuoctieukq) ? "1" : "0");
                
                dicMF.Add("sieuam_tinhtrang", Utility.Byte2Bool(_pkt.ClsSieuam) ? "1" : "0");
                dicMF.Add("sieuam_binhthuong", Utility.Byte2Bool(_pkt.ClsSieuamKq) ? "0" : "1");
                dicMF.Add("sieuam_batthuong", Utility.Byte2Bool(_pkt.ClsSieuamKq) ? "1" : "0");
                
                dicMF.Add("tuvan_khong", Utility.Byte2Bool(_pkt.Tuvan) ? "0" : "1");
                dicMF.Add("tuvan_co", Utility.Byte2Bool(_pkt.Tuvan) ? "1" : "0");
                dicMF.Add("tienluong_sinhthuong", Utility.ByteDbnull(_pkt.TienLuong) == 0 ? "1" : "0");
                dicMF.Add("tienluong_sinhconguyco", Utility.ByteDbnull(_pkt.TienLuong) == 1 ? "1" : "0");
                dicMF.Add("tienluong_chidinhmolaythai", Utility.ByteDbnull(_pkt.TienLuong) == 2 ? "1" : "0");
                dicMF.Add("bacsisanphukhoa", Utility.ByteDbnull(_pkt.LoaiNhanvien) == 0 ? "1" : "0");
                dicMF.Add("hosinh", Utility.ByteDbnull(_pkt.LoaiNhanvien) == 1 ? "1" : "0");
                dicMF.Add("ysysannhi", Utility.ByteDbnull(_pkt.LoaiNhanvien) == 2 ? "1" : "0");
                dicMF.Add("nguoikham_khac", Utility.ByteDbnull(_pkt.LoaiNhanvien) == 3 ? "1" : "0");
                dicMF.Add("doituong_thuphi", "1");
                dicMF.Add("doituong_mienphi",  "0");
                dicMF.Add("doituong_khac", "0");


                dtData.TableName = "kcb_phieukhamthai";
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
                drData["ten_phieu"] = "Phiếu khám thai";
                drData["sngay_kham_full"] = Utility.FormatDateTime_giophut_ngay_thang_nam(_pkt.NgayKham, "");
                drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                drData["sNgaykykinh_cuoi"] =_pkt.NgayDaukykinhcuoi.HasValue?_pkt.NgayDaukykinhcuoi.Value.ToString("dd/MM/yyyy"):"";
                drData["sngaydukien_sinh"] = _pkt.NgayDukiensinh.HasValue ? _pkt.NgayDukiensinh.Value.ToString("dd/MM/yyyy") : "";
                drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                drData["ngay_in"] = Utility.FormatDateTime(DateTime.Now);
                drData["sngay_nhapvien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien, "");
                
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEUKHAMTHAI.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEUKHAMTHAI", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Phiếu khám thai tại thư mục sau :" + PathDoc);
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
                               Path.GetFileNameWithoutExtension(PathDoc), "PHIEUKHAMTHAI", objLuotkham.MaLuotkham, Utility.sDbnull(_pkt.IdPhieukhamthai), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                    //Nạp tiền sử sản khoa. Nhảy đến bảng số 3 trong file doc mẫu
                    Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[3];
                    int idx = 1;
                    foreach (DataRow dr in dtTiensusankhoa.Rows)
                    {
                        Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);
                        newRow.RowFormat.Borders.Shadow = false;
                        newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[3].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[4].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[5].CellFormat.Shading.BackgroundPatternColor = Color.White;

                        newRow.Cells[0].FirstParagraph.Runs.Clear();
                        newRow.Cells[1].FirstParagraph.Runs.Clear();
                        newRow.Cells[2].FirstParagraph.Runs.Clear();
                        newRow.Cells[3].FirstParagraph.Runs.Clear();
                        newRow.Cells[4].FirstParagraph.Runs.Clear();
                        newRow.Cells[5].FirstParagraph.Runs.Clear();
                        Run r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Size = 9d;
                        r.Font.Bold = false;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["thoigian_noi_ketthuc"], "");
                        newRow.Cells[0].FirstParagraph.AppendChild(r);
                        newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Bold = false;
                        r.Font.Size = 9d;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["tuoi_thai"], "");
                        newRow.Cells[1].FirstParagraph.AppendChild(r);
                        newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Bold = false;
                        r.Font.Size = 9d;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["dien_bien"],"");
                        newRow.Cells[2].FirstParagraph.AppendChild(r);
                        newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                        
                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Bold = false;
                        r.Font.Size = 9d;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["cach_sinh"], "");
                        newRow.Cells[3].FirstParagraph.AppendChild(r);
                        newRow.Cells[3].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Bold = false;
                        r.Font.Size = 9d;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["mota_tresosinh"], "");
                        newRow.Cells[4].FirstParagraph.AppendChild(r);
                        newRow.Cells[4].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Size = 9d;
                        r.Font.Bold = false;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["hau_san"], "");
                        newRow.Cells[5].FirstParagraph.AppendChild(r);
                        newRow.Cells[5].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        tab.AppendChild(newRow);
                        idx += 1;
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

        private void cmdRefreshCDHA_Click(object sender, EventArgs e)
        {
        }

        private void cmdRefreshXN_Click(object sender, EventArgs e)
        {
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
            txtNhiptho.Text = nhiptho;
            txtha.Text = huyetap;
            txtChieucao.Text = chieucao;
            txtCannang.Text = cannang;
            txtBmi.Text = bmi;
            txtNhommau.Text = nhommau;
        }

        private void grpChucNangSong_Enter(object sender, EventArgs e)
        {

        }

        private void cmdViewKQCLS_Click(object sender, EventArgs e)
        {
            frm_XemKQCLS _XemKQCLS = new frm_XemKQCLS(objLuotkham, 100);
            _XemKQCLS.ShowDialog();
        }

        private void cmdInReport_Click(object sender, EventArgs e)
        {
            InPhieuKhamThai();
        }

        private void cmdThemTiensuSankhoa_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = dtTiensusankhoa.NewRow();
                if (_pkt != null)
                {
                    KcbPhieukhamthaiTiensusankhoa _newItem = new KcbPhieukhamthaiTiensusankhoa();
                    _newItem.IdPhieukhamthai = _pkt.IdPhieukhamthai;
                    _newItem.Save();

                    dr[KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa] = _newItem.IdTiensuSankhoa;
                    dr[KcbPhieukhamthaiTiensusankhoa.Columns.IdPhieukhamthai] = _newItem.IdPhieukhamthai;
                }
                else
                {
                    dr[KcbPhieukhamthaiTiensusankhoa.Columns.IdTiensuSankhoa] = -1;
                    dr[KcbPhieukhamthaiTiensusankhoa.Columns.IdPhieukhamthai] = -1;
                    dr["guid"] = THU_VIEN_CHUNG.GetGUID();
                }
                dtTiensusankhoa.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdIn_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdRefreshChucnangsong_Click_1(object sender, EventArgs e)
        {

        }

    }
}
