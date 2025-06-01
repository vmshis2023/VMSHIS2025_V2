using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.BA.Forms
{
    public partial class FrmHoSoCayThuocTranhThai : Form
    {
        public FrmHoSoCayThuocTranhThai()
        {
            InitializeComponent();
            dtpngaythuchien.Value = dtpngaykham.Value = globalVariables.SysDate;
        }
        public action EmAction = action.Insert;
        public string Loaibenhan = "";
        private bool _suaBenhAn;

        public long Idbenhnhan = 1896;

        private void LayThongTinBenhNhan()
        {

            txtidbenhnhan.Visible = globalVariables.IsAdmin;
            DataTable dt = SPs.KcbLaythongtinBenhnhan(Idbenhnhan).GetDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtsohoso.Text = dt.Rows[0]["ma_luotkham"].ToString();
                txtidbenhnhan.Text = dt.Rows[0]["id_benhnhan"].ToString();
                txthovaten.Text = dt.Rows[0]["ten_benhnhan"].ToString();
                txtdiachi.Text = dt.Rows[0]["dia_chi"].ToString();
                txtnamsinh.Text = dt.Rows[0]["nam_sinh"].ToString();
                txtdienthoai.Text = dt.Rows[0]["dienthoai_lienhe"].ToString();
                txtgioitinh.Text = dt.Rows[0]["gioi_tinh"].ToString();
                txtsobenhan.Text = dt.Rows[0]["so_benh_an"].ToString();
                dtpngaykham.Value = Convert.ToDateTime(dt.Rows[0]["ngay_tiepdon"].ToString());
                txtchong_diachi.Text = dt.Rows[0]["dia_chi"].ToString();
            }
        }
        private void FrmHoSoCayThuocTranhThai_Load(object sender, EventArgs e)
        {
            LayThongTinBenhNhan();
            switch (Loaibenhan)
            {
                case "CTT":
                    tabHosothuoctranhthai.TabVisible = true;
                    tabDatdungcu.TabVisible = false;
                    tabthaoquetranhthai.TabVisible = false;
                    tabthaovong.TabVisible = false;
                    break;
                case "THV":
                    tabHosothuoctranhthai.TabVisible = false;
                    tabDatdungcu.TabVisible = false;
                    tabthaoquetranhthai.TabVisible = false;
                    tabthaovong.TabVisible = true;
                    break;
                case "DDC":
                    tabHosothuoctranhthai.TabVisible = false;
                    tabDatdungcu.TabVisible = true;
                    tabthaoquetranhthai.TabVisible = false;
                    tabthaovong.TabVisible = false;
                    break;
                case "TQC":
                     tabHosothuoctranhthai.TabVisible = true;
                    tabDatdungcu.TabVisible = false;
                    tabthaoquetranhthai.TabVisible = true;
                    tabthaovong.TabVisible = false;
                    break;
                default: 
                    tabHosothuoctranhthai.TabVisible = true;
                    tabDatdungcu.TabVisible = false;
                    tabthaoquetranhthai.TabVisible = false;
                    tabthaovong.TabVisible = false;
                    break;

            }
            if (EmAction == action.Insert)
            {
                SinhMaBenhAn();
                cmdPrint.Enabled = false;
                txtsobenhan.Enabled = true;
            }
            if (EmAction == action.Update)
            {
                txtsobenhan.Enabled = false;
                LayDuLieuBenhAn();
            }
        }

        private void LayDuLieuBenhAn()
        {
            switch (Loaibenhan)
            {
                case "CTT":
                    LoadThongTinBenhAn_CTT();
                    break;
                case "THV":
                    LoadThongTinBenhAn_THV();
                    break;
                case "DDC":
                    LoadThongTinBenhAn_DDC();
                    break;
                case "TQC":
                    LoadThongTinBenhAn_TQC();
                    break;
                default:
                    LoadThongTinBenhAn_CTT();
                    break;
            }
        }
        private void LoadThongTinBenhAn_TQC()
        {
            KcbBaThaoquecay objBenhAnNgoaiTru =
                  new Select("*").From(KcbBaThaoquecay.Schema).Where(KcbBaThaoquecay.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtidbenhnhan.Text.Trim())).ExecuteSingle<KcbBaThaoquecay>();
            if (objBenhAnNgoaiTru != null)
            {
                txtidbenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.Id, -1);
                txtidbenhnhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.IdBenhnhan, -1);
                txtsobenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.MaLuotkham, -1);
                txtsobenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBenhAn, -1);
                txtvanhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.VanHoa, -1);
                txtnghenghiep.Text = Utility.sDbnull(objBenhAnNgoaiTru.NgheNghiep, -1);
                txtdienthoai.Text = Utility.sDbnull(objBenhAnNgoaiTru.DienThoai, -1);
                txtchong_hoten.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongHovaten, -1);
                txtchong_diachi.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongDiachi, -1);
                txtchong_vanhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongVanhoa, -1);
                txtchong_nghenghiep.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongNghenghiep, -1);

                txttq_solanmangthai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSolanmangthai, "");
                txttq_solansaythai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSolansaythai, "");
                txttq_naohut.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSolannaohut, "");
                txttq_solande.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSolande, "");
                txttq_socongai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSocongai, "");
                txttq_socontrai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSocontrai, "");
                txttq_lancothaigannhat.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkLancothaigannhat, "");
                txttq_ngaydaukykinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkNgaykkc, "");
                txttq_tinhtrangkinhnguyet.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkTinhtrangkinh, "");
                txttq_benhphukhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkBenhphukhoakhac, "");
                txttq_bpttdadung.Text = Utility.sDbnull(objBenhAnNgoaiTru.KhBpttdadung, "");
                txttq_bpttdangdung.Text = Utility.sDbnull(objBenhAnNgoaiTru.KhBpttdangdung, "");
                txttq_cannang.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttCannang, "");
                txttq_mach.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttMach, "");
                txttq_huyetap.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttHuyetap, "");
                txttq_tim.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttTim, "");
                txttq_phoi.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttPhoi, "");
                txttq_tieuhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttTieuhoa, "");
                txttq_phukhoadamac.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtBenhphukhoa, "");
                txttq_thuocdtri.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtThuocdadtribenh, "");
                txttq_diungthuoc.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtDiungthuoc, "");
                txttq_thuocdadtri.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtThuocdadtridiung, "");
                txttq_benhnoikhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtBenhnoikhoa, "");
                txttq_sieuam.Text = Utility.sDbnull(objBenhAnNgoaiTru.XnSieuam, "");
                txttq_huyethoc.Text = Utility.sDbnull(objBenhAnNgoaiTru.XnHuyethoc, "");
                txttq_sinhhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.XnSinhhoa, "");
                txttq_nuoctieu.Text = Utility.sDbnull(objBenhAnNgoaiTru.XnNuoctieu, "");
                txttq_quecayhethan.Text = Utility.sDbnull(objBenhAnNgoaiTru.LdQuehethan, "");
                txttq_tacdungphu.Text = Utility.sDbnull(objBenhAnNgoaiTru.LdTacdungphu, "");
                txttq_lydokhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.LdKhac, "");
                dtptq_ngaycay.Value = Convert.ToDateTime(objBenhAnNgoaiTru.QtcNgaycay);
                txttq_nguoicay.Text = Utility.sDbnull(objBenhAnNgoaiTru.QtcNguoicay, "");
                txttq_vitricay.Text = Utility.sDbnull(objBenhAnNgoaiTru.QtcVitricay, "");
                txttq_qtrinhthao.Text = Utility.sDbnull(objBenhAnNgoaiTru.Quatrinhthao, "");
                txttq_xutri.Text = Utility.sDbnull(objBenhAnNgoaiTru.Xutri, "");
                txttq_tinhtrangsauthao.Text = Utility.sDbnull(objBenhAnNgoaiTru.TinhtrangSauthao, "");

            }
        }
        private void LoadThongTinBenhAn_DDC()
        {
            KcbBaDatdungcu objBenhAnNgoaiTru =
                  new Select("*").From(KcbBaDatdungcu.Schema).Where(KcbBaDatdungcu.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtidbenhnhan.Text.Trim())).ExecuteSingle<KcbBaDatdungcu>();
            if (objBenhAnNgoaiTru != null)
            {
                txtidbenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.Id, -1);
                txtidbenhnhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.IdBenhnhan, -1);
                txtsobenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.MaLuotkham, -1);
                txtsobenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBenhAn, -1);
                txtvanhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.VanHoa, -1);
                txtnghenghiep.Text = Utility.sDbnull(objBenhAnNgoaiTru.NgheNghiep, -1);
                txtdienthoai.Text = Utility.sDbnull(objBenhAnNgoaiTru.DienThoai, -1);
                txtchong_hoten.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongHovaten, -1);
                txtchong_diachi.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongDiachi, -1);
                txtchong_vanhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongVanhoa, -1);
                txtchong_nghenghiep.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongNghenghiep, -1);

                txtctc_chukykinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkChukykinh, "");
                txtctc_chukykhongdeu.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkChukykhongdeu, "");
                txtctc_luonghuyet.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkLuonghuyet, "");
                txtctc_benhphukhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkBenhphukhoa, "");
                txtctc_dtrodau.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkDtriodau, "");
                txtctc_bienphapkehoach.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkBpttdadung, "");
                
                
                txtctc_solancothai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkTongsocothai, "");
                txtctc_de.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkDe, "");
                txtctc_say.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSay, "");
                txtctc_nao.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkNao, "");
                txtctc_hut.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkHut, "");
                txtctc_socontrai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSocontrai, "");
                txtctc_soluongcongai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSocongai, "");
                txtctc_conbetuoi.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkTuoiconbenhat, "");
                txtctc_kykinhlancuoi.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkKykinhcuoi, "");


                txtctc_cannang.Text = Utility.sDbnull(objBenhAnNgoaiTru.TtCannang, "");
                txtctc_mach.Text = Utility.sDbnull(objBenhAnNgoaiTru.TtMach, "");
                txtctc_huyetap.Text = Utility.sDbnull(objBenhAnNgoaiTru.TtHuyetap, "");


                txtctc_noikhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.NkNoikhoa, "");
                txtctc_amho.Text = Utility.sDbnull(objBenhAnNgoaiTru.KsAmho, "");
                txtctc_amdao.Text = Utility.sDbnull(objBenhAnNgoaiTru.KsAmdao, "");
                txtctc_cotucung.Text = Utility.sDbnull(objBenhAnNgoaiTru.KsCotucung, "");
                txtctc_tucung.Text = Utility.sDbnull(objBenhAnNgoaiTru.KsTucung, "");
                txtctc_phanphu.Text = Utility.sDbnull(objBenhAnNgoaiTru.KsPhanphu, "");


                txtctc_dienbienthuthuat.Text = Utility.sDbnull(objBenhAnNgoaiTru.TtDienbien, "");
                txtctc_loaivongdat.Text = Utility.sDbnull(objBenhAnNgoaiTru.TtLoaivong, "");
                txtctc_nuocsanxuat.Text = Utility.sDbnull(objBenhAnNgoaiTru.TtNuocsx, "");
                txtctc_thuocsaudatvong.Text = Utility.sDbnull(objBenhAnNgoaiTru.TtThuocsaudatvong, "");
                txtctc_nhanxettoantrang.Text = Utility.sDbnull(objBenhAnNgoaiTru.TtNhanxettoantrang, "");
            }
        }
        private void LoadThongTinBenhAn_THV()
        {
            KcbBaThaovong objBenhAnNgoaiTru =
                  new Select("*").From(KcbBaThaovong.Schema).Where(KcbBaThaovong.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtidbenhnhan.Text.Trim())).ExecuteSingle<KcbBaThaovong>();
            if (objBenhAnNgoaiTru != null)
            {
                txtidbenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.Id, -1);
                txtidbenhnhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.IdBenhnhan, -1);
                txtsobenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.MaLuotkham, -1);
                txtsobenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBenhAn, -1);
                txtvanhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.VanHoa, -1);
                txtnghenghiep.Text = Utility.sDbnull(objBenhAnNgoaiTru.NgheNghiep, -1);
                txtdienthoai.Text = Utility.sDbnull(objBenhAnNgoaiTru.DienThoai, -1);
                txtchong_hoten.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongHovaten, -1);
                txtchong_diachi.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongDiachi, -1);
                txtchong_vanhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongVanhoa, -1);
                txtchong_nghenghiep.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongNghenghiep, -1);
                //
                dtpct_ngayvao.Value = Convert.ToDateTime(objBenhAnNgoaiTru.NgayVao);
                dtpct_ngayra.Value = Convert.ToDateTime(objBenhAnNgoaiTru.NgayRa);

                txtct_chandoanra._Text = Utility.sDbnull(objBenhAnNgoaiTru.ChandoanRa, -1);
                txtct_chandoanvao._Text = Utility.sDbnull(objBenhAnNgoaiTru.ChandoanVao, -1);
                txtct_tiensuphukhoa._Text = Utility.sDbnull(objBenhAnNgoaiTru.TsPhukhoa, -1);
                txtct_solande._Text = Utility.sDbnull(objBenhAnNgoaiTru.TsSolande, -1);
                txtct_soconsong._Text = Utility.sDbnull(objBenhAnNgoaiTru.TsSoconsong, -1);
                txtct_tuoiconbenhat._Text = Utility.sDbnull(objBenhAnNgoaiTru.TsTuoiconbenhat, -1);
                txtct_tiensulandetruoc._Text = Utility.sDbnull(objBenhAnNgoaiTru.TsLandetruoc, -1);
                txtct_solanxaynaohut._Text = Utility.sDbnull(objBenhAnNgoaiTru.TsSolansaythai, -1);
                txtct_delancuoicung._Text = Utility.sDbnull(objBenhAnNgoaiTru.TsNgaynaohuthoacde, -1);

                txtct_tiensunoikhoa._Text = Utility.sDbnull(objBenhAnNgoaiTru.TsNoikhoa, -1);
                txtct_toanthan._Text = Utility.sDbnull(objBenhAnNgoaiTru.TkToanthan, -1);
                txtct_cannang._Text = Utility.sDbnull(objBenhAnNgoaiTru.TkCangnang, -1);
                txtct_mach._Text = Utility.sDbnull(objBenhAnNgoaiTru.TkMach, -1);
                txtct_huyetap._Text = Utility.sDbnull(objBenhAnNgoaiTru.TkHuyetap, -1);
                txtct_tim._Text = Utility.sDbnull(objBenhAnNgoaiTru.TkTim, -1);
                txtct_phoi._Text = Utility.sDbnull(objBenhAnNgoaiTru.TkPhoi, -1);
                txtct_khamsanphukhoa._Text = Utility.sDbnull(objBenhAnNgoaiTru.TkKhamphusan, -1);

                txtct_thuoctruocthuthuat._Text = Utility.sDbnull(objBenhAnNgoaiTru.TtThuoctruocpt, -1);
                txtct_dienbientrongthuthuat._Text = Utility.sDbnull(objBenhAnNgoaiTru.TtDienbien, -1);
                txtct_thuoccapsauthuthuat._Text = Utility.sDbnull(objBenhAnNgoaiTru.TtThuoccapsautt, -1);
                txtct_nhanxettoantrang._Text = Utility.sDbnull(objBenhAnNgoaiTru.TtNhanxettoantrang, -1);
            }
        }
        private void LoadThongTinBenhAn_CTT()
        {
            var objBenhAnNgoaiTru =
                  new Select("*").From(KcbBaCaytranhthai.Schema).Where(KcbBaCaytranhthai.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtidbenhnhan.Text.Trim())).ExecuteSingle<KcbBaCaytranhthai>();
            if (objBenhAnNgoaiTru != null)
            {
                txtidbenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.Id, -1);
                txtidbenhnhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.IdBenhnhan, -1);
                txtsobenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.MaLuotkham, -1);
                txtsobenhan.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBenhAn, -1);
                txtvanhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.VanHoa, -1);
                txtnghenghiep.Text = Utility.sDbnull(objBenhAnNgoaiTru.NgheNghiep, -1);
                txtdienthoai.Text = Utility.sDbnull(objBenhAnNgoaiTru.DienThoai, -1);
                txtchong_hoten.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongHovaten, -1);
                txtchong_diachi.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongDiachi, -1);
                txtchong_vanhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongVanhoa, -1);
                txtchong_nghenghiep.Text = Utility.sDbnull(objBenhAnNgoaiTru.ChongNghenghiep, -1);
                // 
                txtctt_solanmangthai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSolanmangthai, -1);
                txtctt_solansaythai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSolansaythai, -1);
                txtctt_solannaohut.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSolannaohut, -1);
                txtctt_solande.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSolande, -1);
                txtctt_socongai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSocongai, -1);
                txtctt_socontrai.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkSocontrai, -1);
                txtctt_lancothaigannhat.Text = Utility.sDbnull(objBenhAnNgoaiTru.SkLancothaigannhat, -1);
                txtctt_ngaydaukkc.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkNgaykkc, -1);
                txtctt_tinhtrangkinhnguyet.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkTinhtrangkinh, -1);
                txtctt_cacbenhphukhac.Text = Utility.sDbnull(objBenhAnNgoaiTru.PkBenhphukhoakhac, -1);
                txtctt_bpttdadung.Text = Utility.sDbnull(objBenhAnNgoaiTru.BpDadung, -1);
                txtctt_bpttdangdung.Text = Utility.sDbnull(objBenhAnNgoaiTru.BpDangdung, -1);
                txtctt_cannang.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttCannang, -1);
                txtctt_mach.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttMach, -1);
                txtctt_huyetap.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttHuyetap, -1);
                txtctt_tim.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttTim, -1);
                txtctt_phoi.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttPhoi, -1);
                txtctt_tieuhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.KttTieuhoa, -1);

                txtctt_cacbenhpkdamac.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtBenhphukhoa, -1);
                txtctt_thuocdtri.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtThuocdadtribenh, -1);
                txtctt_diungthuoc.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtDiungthuoc, -1); 
                txtctt_thuocdadtri2.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtThuocdadtridiung, -1);
                txtctt_macnoikhoa.Text = Utility.sDbnull(objBenhAnNgoaiTru.BtBenhnoikhoa, -1);

                txtctt_hcg.Text = Utility.sDbnull(objBenhAnNgoaiTru.XnHcg, -1);
                txtctt_sieuam.Text = Utility.sDbnull(objBenhAnNgoaiTru.XnSieuam, -1);
                //
                chkctt_amhobinhthuong.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcAmhobt).ToUpper() == "X";
                chkctt_amhoviem.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcAmhoviem).ToUpper() == "X";
                chkctt_amhovettrang.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcAmhovettrang).ToUpper() == "X";
                chkctt_amhosui.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcAmhosui).ToUpper() == "X";

                chkctt_amdaobinhthuong.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcAmdaobt).ToUpper() == "X";
                chkctt_amdaoviem.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcAmdaoviem).ToUpper() == "X";
                chkctt_amdaosui.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcAmdaosui).ToUpper() == "X";

                chkctt_cotucungbinhthuong.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcCotucungbt).ToUpper() == "X";
                chkctt_cotucunglotuyen.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcCotucunglotuyen).ToUpper() == "X";
                chkctt_cotucungviem.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcCotucungviem).ToUpper() == "X";
                chkctt_cotucungloet.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcCotucungloet).ToUpper() == "X";
                chkctt_cotucungtrot.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcCotucungtrot).ToUpper() == "X";

                chkctt_tucungbinhthuong.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcTucungbt).ToUpper() == "X";
                chkctt_tucungto.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcTucungto).ToUpper() == "X";

                chkctt_phanphuphaibinhthuong.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcPpphaibt).ToUpper() == "X";
                chkctt_phanphuphaine.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcPpphaine).ToUpper() == "X";
                chkctt_phanphuphaidau.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcPpphaidau).ToUpper() == "X";
                chkctt_phanphuphaito.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcPpphaito).ToUpper() == "X";

                chkctt_phanphutraibinhthuong.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcPptraibt).ToUpper() == "X";
                chkctt_phanphutraine.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcPptraine).ToUpper() == "X";
                chkctt_phanphutraidau.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcPptraidau).ToUpper() == "X";
                chkctt_phanphutraito.Checked = Utility.sDbnull(objBenhAnNgoaiTru.TcPptraito).ToUpper() == "X";

                txtctt_saukinh._Text = Utility.sDbnull(objBenhAnNgoaiTru.SlSaukinh, -1);
                txtctt_sausaythaitunhien._Text = Utility.sDbnull(objBenhAnNgoaiTru.SlSauxaythaitunhien, -1);
                txtctt_sauphathai._Text = Utility.sDbnull(objBenhAnNgoaiTru.SlSauphathai, -1);
                txtctt_saude._Text = Utility.sDbnull(objBenhAnNgoaiTru.SlSaude, -1);
                txtctt_ketluan._Text = Utility.sDbnull(objBenhAnNgoaiTru.Ketluan, -1);

                chkctt_dangchoconbuco.Checked = Utility.sDbnull(objBenhAnNgoaiTru.SlDangchoconbu).ToUpper() == "X";
                chkctt_dangchoconbukhong.Checked = Utility.sDbnull(objBenhAnNgoaiTru.SlDangchoconbu).ToUpper() == "";

                txtctt_quatrinhcay._Text = Utility.sDbnull(objBenhAnNgoaiTru.QuatrinhCay, -1);
                txtctt_xutri._Text = Utility.sDbnull(objBenhAnNgoaiTru.Xutri, -1);
                txtctt_tinhtrangcay._Text = Utility.sDbnull(objBenhAnNgoaiTru.Tinhtrangsaucay, -1);

            }
        }

        private void SinhMaBenhAn()
        {
            QueryCommand cmd1 = KcbLuotkham.CreateQuery().BuildCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandSql = "Select *  from Kcb_Luotkham where ma_luotkham ='" + txtsohoso.Text.Trim() + "'";
                DataTable temdt1 = DataService.GetDataSet(cmd1).Tables[0];
            if (Utility.sDbnull(temdt1.Rows[0]["so_benh_an"].ToString().Trim(), "-1") == "-1" ||
                Utility.sDbnull(temdt1.Rows[0]["so_benh_an"].ToString().Trim(), "-1") == "")
            {
                txtsobenhan.Text = THU_VIEN_CHUNG.SinhMaBenhAnSanKhoa(Loaibenhan);
            }
            else
            {
                txtsobenhan.Text = Utility.sDbnull(temdt1.Rows[0]["so_benh_an"].ToString());
            }
        }
        private void FrmHoSoCayThuocTranhThai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
        }
      
         private KcbBaThaovong CreateBaThaovong()
        {
            var objThaovong = new KcbBaThaovong();
            if (EmAction == action.Insert)
            {
                objThaovong.IsNew = true;
                objThaovong.NguoiTao = globalVariables.UserName;
                objThaovong.NgayTao = globalVariables.SysDate;
                objThaovong.IpMactao = globalVariables.gv_strMacAddress;
            }
            else
            {
                objThaovong.IsLoaded = true;
                objThaovong.MarkOld();
                objThaovong.Id = Utility.Int32Dbnull(txtidbenhan.Text, -1);
                objThaovong.NgaySua = globalVariables.SysDate;
                objThaovong.NguoiSua = globalVariables.UserName;
                objThaovong.IpMacsua = globalVariables.gv_strMacAddress;
            }
             objThaovong.IdBenhnhan = Utility.Int64Dbnull(txtidbenhnhan.Text);
             objThaovong.MaLuotkham = Utility.sDbnull(txtsohoso.Text);
             objThaovong.TenBenhnhan = Utility.sDbnull(txthovaten.Text);
             objThaovong.SoBenhAn = Utility.sDbnull(txtsobenhan.Text);
             objThaovong.LoaiBenhAn = Loaibenhan;
             objThaovong.IdBacsykham = globalVariables.gv_intIDNhanvien;
             objThaovong.NgayThuchien = Convert.ToDateTime(dtpngaythuchien.Value);
             objThaovong.NgayKham = Convert.ToDateTime(dtpngaykham.Value);
             objThaovong.VanHoa = Utility.sDbnull(txtvanhoa.Text);
             objThaovong.NgheNghiep = Utility.sDbnull(txtnghenghiep.Text);
             objThaovong.DienThoai = Utility.sDbnull(txtdienthoai.Text);
             objThaovong.DiaChi = Utility.sDbnull(txtchong_diachi.Text);
             objThaovong.ChongHovaten = Utility.sDbnull(txtchong_hoten.Text);
             objThaovong.ChongDiachi = Utility.sDbnull(txtchong_diachi.Text);
             objThaovong.ChongVanhoa = Utility.sDbnull(txtchong_vanhoa.Text);
             objThaovong.ChongNghenghiep = Utility.sDbnull(txtchong_nghenghiep.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text); 
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
             objThaovong.ChandoanVao = Utility.sDbnull(txtct_chandoanvao.Text);
            return objThaovong;
        }
        private KcbBaThaoquecay CreateBaThaoquecay()
        {
            var objThaoquecay = new KcbBaThaoquecay();
            if (EmAction == action.Insert)
            {
                objThaoquecay.IsNew = true;
                objThaoquecay.NguoiTao = globalVariables.UserName;
                objThaoquecay.NgayTao = globalVariables.SysDate;
                objThaoquecay.IpMactao = globalVariables.gv_strMacAddress;
            }
            else
            {
                objThaoquecay.IsLoaded = true;
                objThaoquecay.MarkOld();
                objThaoquecay.Id = Utility.Int32Dbnull(txtidbenhan.Text, -1);
                objThaoquecay.NgaySua = globalVariables.SysDate;
                objThaoquecay.NguoiSua = globalVariables.UserName;
                objThaoquecay.IpMacsua = globalVariables.gv_strMacAddress;
            }
            objThaoquecay.IdBenhnhan = Utility.Int64Dbnull(txtidbenhnhan.Text);
            objThaoquecay.MaLuotkham = Utility.sDbnull(txtsohoso.Text);
            objThaoquecay.TenBenhnhan = Utility.sDbnull(txthovaten.Text);
            objThaoquecay.SoBenhAn = Utility.sDbnull(txtsobenhan.Text);
            objThaoquecay.LoaiBenhAn = Loaibenhan;
            objThaoquecay.IdBacsykham = globalVariables.gv_intIDNhanvien;
            objThaoquecay.NgayThuchien = Convert.ToDateTime(dtpngaythuchien.Value);
            objThaoquecay.NgayKham = Convert.ToDateTime(dtpngaykham.Value);
            objThaoquecay.VanHoa = Utility.sDbnull(txtvanhoa.Text);
            objThaoquecay.NgheNghiep = Utility.sDbnull(txtnghenghiep.Text);
            objThaoquecay.DienThoai = Utility.sDbnull(txtdienthoai.Text);
            objThaoquecay.DiaChi = Utility.sDbnull(txtchong_diachi.Text);
            objThaoquecay.ChongHovaten = Utility.sDbnull(txtchong_hoten.Text);
            objThaoquecay.ChongDiachi = Utility.sDbnull(txtchong_diachi.Text);
            objThaoquecay.ChongVanhoa = Utility.sDbnull(txtchong_vanhoa.Text);
            objThaoquecay.ChongNghenghiep = Utility.sDbnull(txtchong_nghenghiep.Text);

            return objThaoquecay;
        }
        private KcbBaDatdungcu CreateKcbBaDatdungcu()
        {
            var objDatdungcu = new KcbBaDatdungcu();
            if (EmAction == action.Insert)
            {
                objDatdungcu.IsNew = true;
                objDatdungcu.NguoiTao = globalVariables.UserName;
                objDatdungcu.NgayTao = globalVariables.SysDate;
                objDatdungcu.IpMactao = globalVariables.gv_strMacAddress;
            }
            else
            {
                objDatdungcu.IsLoaded = true;
                objDatdungcu.MarkOld();
                objDatdungcu.Id = Utility.Int32Dbnull(txtidbenhan.Text, -1);
                objDatdungcu.NgaySua = globalVariables.SysDate;
                objDatdungcu.NguoiSua = globalVariables.UserName;
                objDatdungcu.IpMacsua = globalVariables.gv_strMacAddress;
            }
            objDatdungcu.IdBenhnhan = Utility.Int64Dbnull(txtidbenhnhan.Text);
            objDatdungcu.MaLuotkham = Utility.sDbnull(txtsohoso.Text);
            objDatdungcu.TenBenhnhan = Utility.sDbnull(txthovaten.Text);
            objDatdungcu.SoBenhAn = Utility.sDbnull(txtsobenhan.Text);
            objDatdungcu.LoaiBenhAn = Loaibenhan;
            objDatdungcu.IdBacsykham = globalVariables.gv_intIDNhanvien;
            objDatdungcu.NgayThuchien = Convert.ToDateTime(dtpngaythuchien.Value);
            objDatdungcu.NgayKham = Convert.ToDateTime(dtpngaykham.Value);
            objDatdungcu.VanHoa = Utility.sDbnull(txtvanhoa.Text);
            objDatdungcu.NgheNghiep = Utility.sDbnull(txtnghenghiep.Text);
            objDatdungcu.DienThoai = Utility.sDbnull(txtdienthoai.Text);
            objDatdungcu.DiaChi = Utility.sDbnull(txtchong_diachi.Text);
            objDatdungcu.ChongHovaten = Utility.sDbnull(txtchong_hoten.Text);
            objDatdungcu.ChongDiachi = Utility.sDbnull(txtchong_diachi.Text);
            objDatdungcu.ChongVanhoa = Utility.sDbnull(txtchong_vanhoa.Text);
            objDatdungcu.ChongNghenghiep = Utility.sDbnull(txtchong_nghenghiep.Text);
            return objDatdungcu;
        }
        private KcbBaCaytranhthai CreateKcbBaCayTranhThai ()
        {
            var objBaCaytranhthai = new KcbBaCaytranhthai();
            if (EmAction == action.Insert)
            {
                objBaCaytranhthai.IsNew = true;
                objBaCaytranhthai.NguoiTao = globalVariables.UserName;
                objBaCaytranhthai.NgayTao = globalVariables.SysDate;
                objBaCaytranhthai.IpMactao = globalVariables.gv_strMacAddress;
            }
            else
            {
                objBaCaytranhthai.IsLoaded = true;
                objBaCaytranhthai.MarkOld();
                objBaCaytranhthai.Id = Utility.Int32Dbnull(txtidbenhan.Text, -1);
                objBaCaytranhthai.NgaySua = globalVariables.SysDate;
                objBaCaytranhthai.NguoiSua = globalVariables.UserName;
                objBaCaytranhthai.IpMacsua = globalVariables.gv_strMacAddress;
            }
           objBaCaytranhthai.IdBenhnhan = Utility.Int64Dbnull(txtidbenhnhan.Text);
           objBaCaytranhthai.MaLuotkham = Utility.sDbnull(txtsohoso.Text);
           objBaCaytranhthai.TenBenhnhan = Utility.sDbnull(txthovaten.Text);
           objBaCaytranhthai.SoBenhAn = Utility.sDbnull(txtsobenhan.Text);
           objBaCaytranhthai.LoaiBenhAn = Loaibenhan;
           objBaCaytranhthai.IdBacsykham = globalVariables.gv_intIDNhanvien;
           objBaCaytranhthai.NgayThuchien =Convert.ToDateTime(dtpngaythuchien.Value);
           objBaCaytranhthai.NgayKham = Convert.ToDateTime(dtpngaykham.Value);
           objBaCaytranhthai.VanHoa = Utility.sDbnull(txtvanhoa.Text); 
           objBaCaytranhthai.NgheNghiep = Utility.sDbnull(txtnghenghiep.Text);
           objBaCaytranhthai.DienThoai = Utility.sDbnull(txtdienthoai.Text);
           objBaCaytranhthai.DiaChi = Utility.sDbnull(txtchong_diachi.Text);
           objBaCaytranhthai.ChongHovaten = Utility.sDbnull(txtchong_hoten.Text);
           objBaCaytranhthai.ChongDiachi = Utility.sDbnull(txtchong_diachi.Text);
           objBaCaytranhthai.ChongVanhoa = Utility.sDbnull(txtchong_vanhoa.Text);
           objBaCaytranhthai.ChongNghenghiep = Utility.sDbnull(txtchong_nghenghiep.Text); 
           objBaCaytranhthai.SkLancothaigannhat = Utility.sDbnull(txtctt_solanmangthai.Text); 
           objBaCaytranhthai.SkSocongai = Utility.sDbnull(txtctt_socongai.Text); 
           objBaCaytranhthai.SkSocontrai = Utility.sDbnull(txtctt_socontrai.Text);
           objBaCaytranhthai.SkSolande = Utility.sDbnull(txtctt_solande.Text);
           objBaCaytranhthai.SkSolanmangthai = Utility.sDbnull(txtctt_solanmangthai.Text); 
           objBaCaytranhthai.SkSolannaohut = Utility.sDbnull(txtctt_solannaohut.Text); 
           objBaCaytranhthai.SkSolansaythai = Utility.sDbnull(txtctt_solansaythai.Text); 
           objBaCaytranhthai.PkBenhphukhoakhac = Utility.sDbnull(txtctt_cacbenhphukhac.Text);
           objBaCaytranhthai.PkNgaykkc = Utility.sDbnull(txtctt_ngaydaukkc.Text);
           objBaCaytranhthai.PkTinhtrangkinh = Utility.sDbnull(txtctt_tinhtrangkinhnguyet.Text);
           objBaCaytranhthai.BpDadung = Utility.sDbnull(txtctt_bpttdadung.Text);
           objBaCaytranhthai.BpDangdung = Utility.sDbnull(txtctt_bpttdangdung.Text);
           objBaCaytranhthai.BtBenhnoikhoa = Utility.sDbnull(txtctt_macnoikhoa.Text);
           objBaCaytranhthai.BtBenhphukhoa = Utility.sDbnull(txtctt_cacbenhpkdamac.Text);
           objBaCaytranhthai.BtDiungthuoc = Utility.sDbnull(txtctt_diungthuoc.Text);
           objBaCaytranhthai.BtThuocdadtribenh = Utility.sDbnull(txtctt_thuocdtri.Text); 
           objBaCaytranhthai.BtThuocdadtridiung = Utility.sDbnull(txtctt_thuocdadtri2.Text);
           objBaCaytranhthai.KttCannang = Utility.sDbnull(txtctt_cannang.Text);
           objBaCaytranhthai.KttHuyetap = Utility.sDbnull(txtctt_huyetap.Text); 
           objBaCaytranhthai.KttPhoi = Utility.sDbnull(txtctt_phoi.Text);
           objBaCaytranhthai.KttTieuhoa = Utility.sDbnull(txtctt_tieuhoa.Text);
           objBaCaytranhthai.KttTim = Utility.sDbnull(txtctt_tim.Text);
           objBaCaytranhthai.XnHcg = Utility.sDbnull(txtctt_hcg.Text);
           objBaCaytranhthai.XnSieuam = Utility.sDbnull(txtctt_sieuam.Text);
           objBaCaytranhthai.TcAmdaobt = Utility.sDbnull(chkctt_amdaobinhthuong.Checked ? "x" : "");
           objBaCaytranhthai.TcAmdaosui = Utility.sDbnull(chkctt_amdaosui.Checked ? "x" : "");
           objBaCaytranhthai.TcAmdaoviem = Utility.sDbnull(chkctt_amdaoviem.Checked ? "x" : "");
           objBaCaytranhthai.TcAmhobt = Utility.sDbnull(chkctt_amhobinhthuong.Checked ? "x" : "");
           objBaCaytranhthai.TcAmhosui = Utility.sDbnull(chkctt_amhosui.Checked ? "x" : "");
           objBaCaytranhthai.TcAmhovettrang = Utility.sDbnull(chkctt_amhovettrang.Checked ? "x" : "");
           objBaCaytranhthai.TcAmhoviem = Utility.sDbnull(chkctt_amhoviem.Checked ? "x" : "");
           objBaCaytranhthai.TcCotucungbt = Utility.sDbnull(chkctt_cotucungbinhthuong.Checked ? "x" : "");
           objBaCaytranhthai.TcCotucungloet = Utility.sDbnull(chkctt_cotucungloet.Checked ? "x" : "");
           objBaCaytranhthai.TcCotucunglotuyen = Utility.sDbnull(chkctt_cotucunglotuyen.Checked ? "x" : "");
           objBaCaytranhthai.TcCotucungtrot = Utility.sDbnull(chkctt_cotucungtrot.Checked ? "x" : "");
           objBaCaytranhthai.TcPpphaibt = Utility.sDbnull(chkctt_phanphuphaibinhthuong.Checked ? "x" : "");
           objBaCaytranhthai.TcPpphaidau = Utility.sDbnull(chkctt_phanphuphaidau.Checked ? "x" : "");
           objBaCaytranhthai.TcPpphaine = Utility.sDbnull(chkctt_phanphuphaine.Checked ? "x" : "");
           objBaCaytranhthai.TcPpphaito = Utility.sDbnull(chkctt_phanphuphaito.Checked ? "x" : "");
           objBaCaytranhthai.TcPptraibt = Utility.sDbnull(chkctt_phanphutraibinhthuong.Checked ? "x" : "");
           objBaCaytranhthai.TcPptraidau = Utility.sDbnull(chkctt_phanphutraidau.Checked ? "x" : "");
           objBaCaytranhthai.TcPptraine = Utility.sDbnull(chkctt_phanphutraine.Checked ? "x" : "");
           objBaCaytranhthai.TcPptraito = Utility.sDbnull(chkctt_phanphutraito.Checked ? "x" : "");
           objBaCaytranhthai.TcTucungbt = Utility.sDbnull(chkctt_tucungbinhthuong.Checked ? "x" : "");
           objBaCaytranhthai.TcTucungto = Utility.sDbnull(chkctt_tucungto.Checked ? "x" : "");
           objBaCaytranhthai.SlDangchoconbu = Utility.sDbnull(chkctt_dangchoconbuco.Checked ? "x" : "");
           objBaCaytranhthai.SlSaude = Utility.sDbnull(txtctt_saude.Text);
           objBaCaytranhthai.SlSaukinh = Utility.sDbnull(txtctt_saukinh.Text);
           objBaCaytranhthai.SlSauphathai = Utility.sDbnull(txtctt_sauphathai.Text);
           objBaCaytranhthai.SlSauxaythaitunhien = Utility.sDbnull(txtctt_sausaythaitunhien.Text);
           objBaCaytranhthai.SkLancothaigannhat = Utility.sDbnull(txtctt_lancothaigannhat.Text);
           objBaCaytranhthai.Ketluan = Utility.sDbnull(txtctt_ketluan.Text);
           objBaCaytranhthai.Xutri = Utility.sDbnull(txtctt_xutri.Text);
           objBaCaytranhthai.QuatrinhCay = Utility.sDbnull(txtctt_quatrinhcay.Text);
           objBaCaytranhthai.Tinhtrangsaucay = Utility.sDbnull(txtctt_tinhtrangcay.Text);
           objBaCaytranhthai.IdDienbiencay = Utility.Int32Dbnull(Idbenhnhan);
            return objBaCaytranhthai;
        }

        private void InsertBenhAn()
        {
            try
            {
                SqlQuery soKham = null;
                switch (Loaibenhan)
                {
                    case "CTT":
                        soKham =
                              new Select().From(KcbBaCaytranhthai.Schema)
                                  .Where(KcbBaCaytranhthai.Columns.SoBenhAn)
                                  .IsEqualTo(txtsobenhan.Text);
                        if (soKham.GetRecordCount() > 0)
                        {
                            var objbenhancaytranhthai = soKham.ExecuteSingle<KcbBaCaytranhthai>();
                            Utility.ShowMsg(string.Format("Số bệnh án này đã được sử dụng cho bệnh nhân {0}",
                                objbenhancaytranhthai.MaLuotkham));
                            return;
                        }
                        else
                        {

                            KcbBaCaytranhthai objbacCaytranhthai = CreateKcbBaCayTranhThai();
                            objbacCaytranhthai.Save();
                            txtidbenhan.Text = objbacCaytranhthai.Id.ToString(CultureInfo.InvariantCulture);
                            new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn)
                        .EqualTo(string.Format("{0}-{1}", Loaibenhan, txtsobenhan.Text))
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtsohoso.Text)).And(
                            KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                            txtsobenhan.Enabled = false;
                            MessageBox.Show(@"Lưu Thành Công Thông Tin Bệnh Án");
                        }
                        break;
                    case "TQC":
                        soKham =
                               new Select().From(KcbBaThaoquecay.Schema)
                                   .Where(KcbBaThaoquecay.Columns.SoBenhAn)
                                   .IsEqualTo(txtsobenhan.Text);
                        if (soKham.GetRecordCount() > 0)
                        {
                            var objbenhancaytranhthai = soKham.ExecuteSingle<KcbBaThaoquecay>();
                            Utility.ShowMsg(string.Format("Số bệnh án này đã được sử dụng cho bệnh nhân {0}",
                                objbenhancaytranhthai.MaLuotkham));
                            return;
                        }
                        else
                        {
                            KcbBaThaoquecay objBaThaoquecay = CreateBaThaoquecay();
                            objBaThaoquecay.Save();
                            txtidbenhan.Text = objBaThaoquecay.Id.ToString(CultureInfo.InvariantCulture);
                            new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn)
                        .EqualTo(string.Format("{0}-{1}", Loaibenhan, txtsobenhan.Text))
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtsohoso.Text)).And(
                            KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                            txtsobenhan.Enabled = false;
                            MessageBox.Show(@"Lưu Thành Công Thông Tin Bệnh Án");
                        }
                        break;
                    case "DDC":
                        soKham =
                               new Select().From(KcbBaDatdungcu.Schema)
                                   .Where(KcbBaDatdungcu.Columns.SoBenhAn)
                                   .IsEqualTo(txtsobenhan.Text);
                        if (soKham.GetRecordCount() > 0)
                        {
                            var objbenhancaytranhthai = soKham.ExecuteSingle<KcbBaDatdungcu>();
                            Utility.ShowMsg(string.Format("Số bệnh án này đã được sử dụng cho bệnh nhân {0}",
                                objbenhancaytranhthai.MaLuotkham));
                            return;
                        }
                        else
                        {
                            KcbBaDatdungcu objBaDatdungcu = CreateKcbBaDatdungcu();
                            objBaDatdungcu.Save();
                            txtidbenhan.Text = objBaDatdungcu.Id.ToString(CultureInfo.InvariantCulture);
                            new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn)
                        .EqualTo(string.Format("{0}-{1}", Loaibenhan, txtsobenhan.Text))
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtsohoso.Text)).And(
                            KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                            txtsobenhan.Enabled = false;
                            MessageBox.Show(@"Lưu Thành Công Thông Tin Bệnh Án");
                        }
                        break;
                    case "THV":
                        soKham =
                               new Select().From(KcbBaThaovong.Schema)
                                   .Where(KcbBaThaovong.Columns.SoBenhAn)
                                   .IsEqualTo(txtsobenhan.Text);
                        if (soKham.GetRecordCount() > 0)
                        {
                            var objbenhancaytranhthai = soKham.ExecuteSingle<KcbBaThaovong>();
                            Utility.ShowMsg(string.Format("Số bệnh án này đã được sử dụng cho bệnh nhân {0}",
                                objbenhancaytranhthai.MaLuotkham));
                            return;
                        }
                        else
                        {
                            KcbBaThaovong objBaThaovong = CreateBaThaovong();
                            objBaThaovong.Save();
                            txtidbenhan.Text = objBaThaovong.Id.ToString(CultureInfo.InvariantCulture);
                            new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn)
                        .EqualTo(string.Format("{0}-{1}", Loaibenhan, txtsobenhan.Text))
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtsohoso.Text)).And(
                            KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                            txtsobenhan.Enabled = false;
                            MessageBox.Show(@"Lưu Thành Công Thông Tin Bệnh Án");
                        }
                        break;

                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi: " + exception.Message);
            }
           
         
        }

        private void DeleteBenhAn()
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa bệnh án này không? ", "Thông báo", true))
                {
                    switch (Loaibenhan)
                    {
                        case "CTT":
                            if (Utility.Int64Dbnull(txtidbenhan.Text.Trim()) > 0)
                            {
                                new Delete().From(KcbBaCaytranhthai.Schema)
                                    .Where(KcbBaCaytranhthai.Columns.Id)
                                    .IsEqualTo(Utility.Int64Dbnull(txtidbenhan.Text.Trim()))
                                    .Execute();
                            }
                            break;
                        case "TQC":
                            if (Utility.Int64Dbnull(txtidbenhan.Text.Trim()) > 0)
                            {
                                new Delete().From(KcbBaThaoquecay.Schema)
                                    .Where(KcbBaThaoquecay.Columns.Id)
                                    .IsEqualTo(Utility.Int64Dbnull(txtidbenhan.Text.Trim()))
                                    .Execute();
                            }
                            break;
                        case "DDC":
                            if (Utility.Int64Dbnull(txtidbenhan.Text.Trim()) > 0)
                            {
                                new Delete().From(KcbBaDatdungcu.Schema)
                                    .Where(KcbBaDatdungcu.Columns.Id)
                                    .IsEqualTo(Utility.Int64Dbnull(txtidbenhan.Text.Trim()))
                                    .Execute();
                            }
                            break;
                        case "THV":
                            if (Utility.Int64Dbnull(txtidbenhan.Text.Trim()) > 0)
                            {
                                new Delete().From(KcbBaThaovong.Schema)
                                    .Where(KcbBaThaovong.Columns.Id)
                                    .IsEqualTo(Utility.Int64Dbnull(txtidbenhan.Text.Trim()))
                                    .Execute();
                            }
                            break;
                    }
                    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn)
                                .EqualTo(Utility.sDbnull(""))
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtsohoso.Text)).And(
                                   KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                }
          
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi: " + exception.Message);
            }
            
        }
        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrEmpty(txtidbenhnhan.Text))
            {
                Utility.ShowMsg("Chưa tồn tại bệnh nhân", "Thông báo", MessageBoxIcon.Warning);
                txtidbenhnhan.Focus();

                return false;
            }
            KcbDanhsachBenhnhan objDanhsachBenhnhan = SPs.KcbLaythongtinBenhnhan(Utility.Int64Dbnull(txtidbenhnhan.Text)) .ExecuteTypedList<KcbDanhsachBenhnhan>().FirstOrDefault();
            if (objDanhsachBenhnhan == null)
            {
                Utility.ShowMsg("Không tồn tại mã bệnh nhân này", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieu()) return;
                switch (EmAction)
                {
                    case action.Update:
                        UpdateBenhAn();
                        break;
                    case action.Insert:
                        InsertBenhAn();
                        break;
                    case action.Select:
                        // Chưa dùng đến
                        break;
                    case action.Delete:
                        DeleteBenhAn();
                        break;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi: " + exception.Message);
            }
           
        }

        private void UpdateBenhAn()
        {
            try
            {
                SqlQuery soKham = null;
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn sửa thông tin bệnh án này không? ", "Thông báo", true))
                {
                    switch (Loaibenhan)
                    {
                        case "CTT":
                            soKham =
                                new Select().From(KcbBaCaytranhthai.Schema)
                                    .Where(KcbBaCaytranhthai.Columns.SoBenhAn).IsEqualTo(txtsobenhan.Text)
                                    .And(KcbBaCaytranhthai.Columns.IdBenhnhan).IsNotEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text));
                            if (soKham.GetRecordCount() > 0)
                            {
                                var objbenhancaytranhthai = soKham.ExecuteSingle<KcbBaCaytranhthai>();
                                Utility.ShowMsg(string.Format("Số bệnh án này đã được sử dụng cho bệnh nhân {0}", objbenhancaytranhthai.MaLuotkham));
                                return;
                            }
                            else
                            {
                                KcbBaCaytranhthai objbacCaytranhthai = CreateKcbBaCayTranhThai();
                                objbacCaytranhthai.Save();
                                txtidbenhan.Text = objbacCaytranhthai.Id.ToString(CultureInfo.InvariantCulture);
                                new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn)
                              .EqualTo(string.Format("{0}-{1}", Loaibenhan, txtsobenhan.Text))
                              .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtsohoso.Text)).And(
                                  KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                            }

                            break;
                        case "TQC":
                            soKham =
                              new Select().From(KcbBaThaoquecay.Schema)
                                  .Where(KcbBaThaoquecay.Columns.SoBenhAn)
                                  .IsEqualTo(txtsobenhan.Text).And(KcbBaThaoquecay.Columns.IdBenhnhan).IsNotEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text));
                            if (soKham.GetRecordCount() > 0)
                            {
                                var objbenhancaytranhthai = soKham.ExecuteSingle<KcbBaThaoquecay>();
                                Utility.ShowMsg(string.Format("Số bệnh án này đã được sử dụng cho bệnh nhân {0}",
                                    objbenhancaytranhthai.MaLuotkham));
                                return;
                            }
                            else
                            {
                                KcbBaThaoquecay objBaThaoquecay = CreateBaThaoquecay();
                                objBaThaoquecay.Save();
                                txtidbenhan.Text = objBaThaoquecay.Id.ToString(CultureInfo.InvariantCulture);
                                new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn)
                              .EqualTo(string.Format("{0}-{1}", Loaibenhan, txtsobenhan.Text))
                              .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtsohoso.Text)).And(
                                  KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                            }
                            break;
                        case "DDC":
                            soKham =
                            new Select().From(KcbBaDatdungcu.Schema)
                                .Where(KcbBaDatdungcu.Columns.SoBenhAn)
                                .IsEqualTo(txtsobenhan.Text).And(KcbBaDatdungcu.Columns.IdBenhnhan).IsNotEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text));
                            if (soKham.GetRecordCount() > 0)
                            {
                                var objbenhancaytranhthai = soKham.ExecuteSingle<KcbBaDatdungcu>();
                                Utility.ShowMsg(string.Format("Số bệnh án này đã được sử dụng cho bệnh nhân {0}",
                                    objbenhancaytranhthai.MaLuotkham));
                                return;
                            }
                            else
                            {
                                KcbBaDatdungcu objBaDatdungcu = CreateKcbBaDatdungcu();
                                objBaDatdungcu.Save();
                                txtidbenhan.Text = objBaDatdungcu.Id.ToString(CultureInfo.InvariantCulture);
                                new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn)
                              .EqualTo(string.Format("{0}-{1}", Loaibenhan, txtsobenhan.Text))
                              .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtsohoso.Text)).And(
                                  KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                            }
                            break;
                        case "THV":
                            soKham =
                            new Select().From(KcbBaThaovong.Schema)
                                .Where(KcbBaThaovong.Columns.SoBenhAn)
                                .IsEqualTo(txtsobenhan.Text).And(KcbBaThaovong.Columns.IdBenhnhan).IsNotEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text));
                            if (soKham.GetRecordCount() > 0)
                            {
                                var objbenhancaytranhthai = soKham.ExecuteSingle<KcbBaThaovong>();
                                Utility.ShowMsg(string.Format("Số bệnh án này đã được sử dụng cho bệnh nhân {0}",
                                    objbenhancaytranhthai.MaLuotkham));
                                return;
                            }
                            else
                            {
                                KcbBaThaovong objBaThaovong = CreateBaThaovong();
                                objBaThaovong.Save();
                                txtidbenhan.Text = objBaThaovong.Id.ToString(CultureInfo.InvariantCulture);
                                new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn)
                              .EqualTo(string.Format("{0}-{1}", Loaibenhan, txtsobenhan.Text))
                              .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtsohoso.Text)).And(
                                  KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(txtidbenhnhan.Text)).Execute();
                            }
                            break;

                    }

                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi: " + exception.Message);
            }
         
           
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            long idbenhan = Utility.Int64Dbnull(txtidbenhan.Text,-1);
            long id_benhnhan = Utility.Int64Dbnull(txtidbenhnhan.Text, -1);
            string ma_luotkham = Utility.sDbnull(txtsohoso.Text, "");
            DataTable dtBenhan = new DataTable();
            string reportcode;
            switch (Loaibenhan)
            {
                case "CTT":
                    reportcode = "thamkham_benh_an_caytranhthai";
                    dtBenhan =
                        SPs.KcbThamkhamLaythongtinBenhanCaytranhthai(idbenhan, id_benhnhan, ma_luotkham)
                            .GetDataSet()
                            .Tables[0];
                    break;
                case "THV":
                    reportcode = "thamkham_benh_an_thaovong";
                    dtBenhan =
                      SPs.KcbThamkhamLaythongtinBenhanThaovong(idbenhan, id_benhnhan, ma_luotkham)
                          .GetDataSet()
                          .Tables[0];
                    break;
                case "TQC":
                    reportcode = "thamkham_benh_an_thaoquecay";
                    dtBenhan =
                      SPs.KcbThamkhamLaythongtinBenhanThaoquecay(idbenhan, id_benhnhan, ma_luotkham)
                          .GetDataSet()
                          .Tables[0];
                    break;
                case "DDC":
                    reportcode = "thamkham_benh_an_datdungcu";
                    dtBenhan =
                      SPs.KcbThamkhamLaythongtinBenhanDatdungcu(idbenhan, id_benhnhan, ma_luotkham)
                          .GetDataSet()
                          .Tables[0];
                    break;
                default:
                    reportcode = "thamkham_benh_an_caytranhthai";
                    dtBenhan =
                      SPs.KcbThamkhamLaythongtinBenhanCaytranhthai(idbenhan, id_benhnhan, ma_luotkham)
                          .GetDataSet()
                          .Tables[0];
                    break;
            }
            THU_VIEN_CHUNG.CreateXML(dtBenhan, string.Format("{0}.XML", reportcode));
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;
            try
            {
                var objForm = new frmPrintPreview("BỆNH ÁN: " + tieude, crpt, true, true);
                crpt.SetDataSource(dtBenhan);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportcode;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, Convert.ToDateTime(dtBenhan.Rows[0]["ngay_thuchien"].ToString())));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                UpdateInThongTinBenhAn();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }

        void UpdateInThongTinBenhAn()
        {
            switch (Loaibenhan)
            {
                case "CTT":
                    new Update(KcbBaCaytranhthai.Schema)
                        .Set(KcbBaCaytranhthai.Columns.SolanIn).EqualTo(1)
                        .Set(KcbBaCaytranhthai.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                        .Set(KcbBaCaytranhthai.Columns.NgayIn).EqualTo(globalVariables.SysDate)
                        .Where(KcbBaCaytranhthai.Columns.Id)
                        .IsEqualTo(Utility.Int64Dbnull(txtidbenhan.Text, -1))
                        .Execute();
                    break;
                case "THV":
                    new Update(KcbBaThaovong.Schema)
                         .Set(KcbBaThaovong.Columns.SolanIn).EqualTo(1)
                         .Set(KcbBaThaovong.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                         .Set(KcbBaThaovong.Columns.NgayIn).EqualTo(globalVariables.SysDate)
                         .Where(KcbBaThaovong.Columns.Id)
                         .IsEqualTo(Utility.Int64Dbnull(txtidbenhan.Text, -1))
                         .Execute();
                    break;
                case "TQC":
                    new Update(KcbBaThaoquecay.Schema)
                         .Set(KcbBaThaoquecay.Columns.SolanIn).EqualTo(1)
                         .Set(KcbBaThaoquecay.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                         .Set(KcbBaThaoquecay.Columns.NgayIn).EqualTo(globalVariables.SysDate)
                         .Where(KcbBaThaoquecay.Columns.Id)
                         .IsEqualTo(Utility.Int64Dbnull(txtidbenhan.Text, -1))
                         .Execute();
                    break;
                case "DDC":
                    new Update(KcbBaDatdungcu.Schema)
                         .Set(KcbBaDatdungcu.Columns.SolanIn).EqualTo(1)
                         .Set(KcbBaDatdungcu.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                         .Set(KcbBaDatdungcu.Columns.NgayIn).EqualTo(globalVariables.SysDate)
                         .Where(KcbBaDatdungcu.Columns.Id)
                         .IsEqualTo(Utility.Int64Dbnull(txtidbenhan.Text, -1))
                         .Execute();
                    break;
                default:
                    new Update(KcbBaCaytranhthai.Schema)
                           .Set(KcbBaCaytranhthai.Columns.SolanIn).EqualTo(1)
                           .Set(KcbBaCaytranhthai.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                           .Set(KcbBaCaytranhthai.Columns.NgayIn).EqualTo(globalVariables.SysDate)
                           .Where(KcbBaCaytranhthai.Columns.Id)
                           .IsEqualTo(Utility.Int64Dbnull(txtidbenhan.Text, -1))
                           .Execute();
                    break;
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {

        }

        private void cmdxoabenhan_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exception)
            {
                
            }
            DeleteBenhAn();
        }

    }
}
