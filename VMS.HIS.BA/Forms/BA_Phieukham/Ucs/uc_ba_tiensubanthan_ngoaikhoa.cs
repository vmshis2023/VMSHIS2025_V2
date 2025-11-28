using SubSonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.EMR.Forms.BA_Phieukham.Ucs
{
    public partial class uc_ba_tiensubanthan_ngoaikhoa : UserControl
    {
        DataTable dt_tiensu_pttt = new DataTable();
        EmrPhieukhamNgoaikhoa objPK;
        KcbLuotkham objLuotkham;
        int num = 0;
        public uc_ba_tiensubanthan_ngoaikhoa()
        {
            InitializeComponent();
            grdPTTT.ColumnButtonClick += GrdPTTT_ColumnButtonClick;
            grdPTTT.CellValueChanged += GrdPTTT_CellValueChanged;
        }
        List<string> lstKeys = new List<string>() { "so_luong","vi_tri","thoi_gian", "noi_thuchien" };
        private void GrdPTTT_CellValueChanged(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                long id = Utility.Int64Dbnull(grdPTTT.GetValue("id_tiensu_phauthuat"));
                string key = e.Column.Key;
                string value = Utility.sDbnull(grdPTTT.GetValue(key));
                if(lstKeys.Contains(key))
                new Update(EmrTiensuPhauthuatNgoaikhoa.Schema)
                    .Set(key).EqualTo(value)
                    .Where(EmrTiensuPhauthuatNgoaikhoa.Columns.IdTiensuPhauthuat).IsEqualTo(id).Execute();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void GrdPTTT_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                if(dt_tiensu_pttt!=null)
                {
                    long id = Utility.Int64Dbnull(grdPTTT.GetValue("id_tiensu_phauthuat"));
                    string uuid = Utility.sDbnull(grdPTTT.GetValue("uuid"));
                    string noidung_xoa = string.Format("Vị trí: {0}\nThời gian: {1}\nNơi thực hiện:{2}",Utility.sDbnull(grdPTTT.GetValue("vi_tri")), Utility.sDbnull(grdPTTT.GetValue("thoi_gian")), Utility.sDbnull(grdPTTT.GetValue("noi_thuchien")));
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa tiền sử PTTT {0} đang chọn ?", noidung_xoa), "Xác nhận xóa", true))
                    {
                        DataRow[] rows = dt_tiensu_pttt.Select(string.Format("uuid = '{0}'", uuid));
                        foreach (DataRow row in rows)
                            row.Delete();
                        dt_tiensu_pttt.AcceptChanges();
                        if (id > 0)
                        {
                            num = new Delete().From(EmrTiensuPhauthuatNgoaikhoa.Schema)
                                .Where(EmrTiensuPhauthuatNgoaikhoa.Columns.IdTiensuPhauthuat).IsEqualTo(id)
                                .Execute();
                        }
                    }    
                }    
            }
            catch (Exception ê)
            {

              
            }
        }
        public void InitData(KcbLuotkham objLuotkham)
        {
            this.objPK = objPK;
        }
            public void ShowData(EmrPhieukhamNgoaikhoa objPK, KcbLuotkham objLuotkham)
        {
            try
            {
                this.objPK = objPK;
                this.objLuotkham = objLuotkham;

                 dt_tiensu_pttt = new Select().From(EmrTiensuPhauthuatNgoaikhoa.Schema)
                    .Where(EmrTiensuPhauthuatNgoaikhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(EmrTiensuPhauthuatNgoaikhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteDataSet().Tables[0];
                Utility.AddColums2DataTable(ref dt_tiensu_pttt, new List<string>() { "uuid" }, typeof(string));
                foreach(DataRow dr in dt_tiensu_pttt.Rows)
                {
                    dr["uuid"] = Guid.NewGuid().ToString();
                }
                Utility.SetDataSourceForDataGridEx(grdPTTT, dt_tiensu_pttt, true, true, "1=1", "id_tiensu_phauthuat");
                opt_diung_khong.Checked = Utility.Bool2Bool(objPK.DiungKhong);
                opt_diung_co.Checked = Utility.Bool2Bool(objPK.DiungCo);
                txt_diung_mota.Text = opt_diung_co.Checked?Utility.sDbnull(objPK.DiungMota):"";

                opt_diung_hoachat_mypham_co.Checked = Utility.Bool2Bool(objPK.DiungHoachatMyphamCo);
                opt_diung_hoachat_mypham_khong.Checked = Utility.Bool2Bool(objPK.DiungHoachatMyphamKhong);
                txt_diung_hoachat_mypham_mota.Text = opt_diung_hoachat_mypham_co.Checked? Utility.sDbnull(objPK.DiungHoachatMyphamMota):"";

                opt_diung_thuoc_khong.Checked = Utility.Bool2Bool(objPK.DiungThuocKhong);
                opt_diung_thuoc_co.Checked = Utility.Bool2Bool(objPK.DiungThuocCo);
                txt_diung_thuoc_mota.Text = opt_diung_thuoc_co.Checked ? Utility.sDbnull(objPK.DiungThuocMota):"";

                opt_diung_thucpham_co.Checked = Utility.Bool2Bool(objPK.DiungThucphamCo);
                opt_diung_thucpham_khong.Checked = Utility.Bool2Bool(objPK.DiungThucphamKhong);
                txt_diung_thucpham_mota.Text = opt_diung_thucpham_co.Checked? Utility.sDbnull(objPK.DiungThucphamMota):"";

                txt_diung_khac_mota.Text = Utility.sDbnull(objPK.DiungKhacMota);

                opt_thuoc_dangdung_khong.Checked = Utility.Bool2Bool(objPK.ThuocDangdungKhong);
                opt_thuoc_dangdung_co.Checked = Utility.Bool2Bool(objPK.ThuocDangdungCo);
                txt_thuoc_dangdung_mota.Text = opt_thuoc_dangdung_co.Checked? Utility.sDbnull(objPK.ThuocDangdungMota):"";

                opt_benh_noitiet_co.Checked = Utility.Bool2Bool(objPK.BenhNoitietCo);
                opt_benh_noitiet_khong.Checked = Utility.Bool2Bool(objPK.BenhNoitietKhong);
                txt_benh_noitiet_mota.Text = opt_benh_noitiet_co.Checked? Utility.sDbnull(objPK.BenhNoitietMota):"";

                opt_tsb_co.Checked = Utility.Bool2Bool(objPK.TsbCo);
                opt_tsb_khong.Checked = Utility.Bool2Bool(objPK.TsbKhong);
                txt_tsb_mota.Text = opt_tsb_co.Checked?Utility.sDbnull(objPK.TsbMota):"";

                opt_benh_thankinh_khong.Checked = Utility.Bool2Bool(objPK.BenhThankinhKhong);
                opt_benh_thankinh_co.Checked = Utility.Bool2Bool(objPK.BenhThankinhCo);
                txt_benh_thankinh_mota.Text = opt_benh_thankinh_co.Checked? Utility.sDbnull(objPK.BenhThankinhMota):"";

                opt_benh_timmach_co.Checked = Utility.Bool2Bool(objPK.BenhTimmachCo);
                opt_benh_timmach_khong.Checked = Utility.Bool2Bool(objPK.BenhTimmachKhong);
                txt_benh_timmach_mota.Text = opt_benh_timmach_co.Checked? Utility.sDbnull(objPK.BenhTimmachMota):"";

                opt_benh_hohap_co.Checked = Utility.Bool2Bool(objPK.BenhHohapCo);
                opt_benh_hohap_khong.Checked = Utility.Bool2Bool(objPK.BenhHohapKhong);
                txt_benh_hohap_mota.Text = opt_benh_hohap_co.Checked? Utility.sDbnull(objPK.BenhHohapMota):"";

                opt_benh_tieuhoa_khong.Checked = Utility.Bool2Bool(objPK.BenhTieuhoaKhong);
                opt_benh_tieuhoa_co.Checked = Utility.Bool2Bool(objPK.BenhTieuhoaCo);
                txt_benh_tieuhoa_mota.Text = opt_benh_tieuhoa_co.Checked?Utility.sDbnull(objPK.BenhTieuhoaMota):"";

             

                opt_benh_thantietnieu_co.Checked = Utility.Bool2Bool(objPK.BenhThantietnieuCo);
                opt_benh_thantietnieu_khong.Checked = Utility.Bool2Bool(objPK.BenhThantietnieuKhong);
                txt_benh_thantietnieu_mota.Text = opt_benh_thantietnieu_co.Checked ? Utility.sDbnull(objPK.BenhThantietnieuMota):"";

                opt_benh_coxuongkhop_co.Checked = Utility.Bool2Bool(objPK.BenhCoxuongkhopCo);
                opt_benh_coxuongkhop_khong.Checked = Utility.Bool2Bool(objPK.BenhCoxuongkhopKhong);
                txt_benh_coxuongkhop_mota.Text = opt_benh_coxuongkhop_co.Checked? Utility.sDbnull(objPK.BenhCoxuongkhopMota):"";

                opt_benh_ungthu_co.Checked = Utility.Bool2Bool(objPK.BenhUngthuCo);
                opt_benh_ungthu_khong.Checked = Utility.Bool2Bool(objPK.BenhUngthuKhong);
                txt_benh_ungthu_mota.Text = opt_benh_ungthu_co.Checked ? Utility.sDbnull(objPK.BenhUngthuMota):"";

                txt_benh_khac_mota.Text = Utility.sDbnull(objPK.BenhKhacMota);

                opt_tiensu_phauthuat_co.Checked = Utility.Bool2Bool(objPK.TiensuPhauthuatCo);
                opt_tiensu_phauthuat_khong.Checked = Utility.Bool2Bool(objPK.TiensuPhauthuatKhong);
                txt_tiensu_phauthuat_mota.Text = opt_tiensu_phauthuat_co.Checked ? Utility.sDbnull(objPK.TiensuPhauthuatMota):"";

                opt_yeuto_nguyco_khong.Checked = Utility.Bool2Bool(objPK.YeutoNguycoKhong);
                opt_yeuto_nguyco_co.Checked = Utility.Bool2Bool(objPK.YeutoNguycoCo);
                txt_yeuto_nguyco_mota.Text = opt_yeuto_nguyco_co.Checked? Utility.sDbnull(objPK.YeutoNguycoMota):"";

                opt_thuocla_khong.Checked = Utility.Bool2Bool(objPK.ThuoclaKhong);
                opr_thuocla_co.Checked = Utility.Bool2Bool(objPK.ThuoclaCo);
                nmr_thuocla_nam.Value =  Utility.DecimaltoDbnull(objPK.ThuoclaNam);
                nmr_thuocla_goi.Value =Utility.DecimaltoDbnull(objPK.ThuoclaGoi);

                txt_thuoclao_khong.Checked = Utility.Bool2Bool(objPK.ThuoclaoKhong);
                opt_thuoclao_co.Checked = Utility.Bool2Bool(objPK.ThuoclaoCo);
                txt_thuoclao_mota.Text = opt_thuoclao_co.Checked? Utility.sDbnull(objPK.ThuoclaoMota):"";
                nmr_thuoclao_nam.Value = opt_thuoclao_co.Checked? Utility.DecimaltoDbnull(objPK.ThuoclaoNam):0m;

                opt_ruou_bia_co.Checked = Utility.Bool2Bool(objPK.RuouBiaCo);
                opt_ruou_bia_khong.Checked = Utility.Bool2Bool(objPK.RuouBiaKhong);
                opt_ruou_bia_it.Checked = Utility.Bool2Bool(objPK.RuouBiaIt);
               
                opt_ruou_bia_thinhthoang.Checked = Utility.Bool2Bool(objPK.RuouBiaThinhthoang);
                opt_ruou_bia_thuong_xuyen.Checked = Utility.Bool2Bool(objPK.RuouBiaThuongXuyen);
                txt_ruou_bia_mota.Text = opt_ruou_bia_co.Checked ? Utility.sDbnull(objPK.RuouBiaMota):"";

                opt_chat_gaynghien_co.Checked = Utility.Bool2Bool(objPK.ChatGaynghienCo);
                opt_chat_gaynghien_khong.Checked = Utility.Bool2Bool(objPK.ChatGaynghienKhong);
                chk_chat_gaynghien_loai.Checked = Utility.Bool2Bool(objPK.ChatGaynghienLoai);
                nmr_chat_gaynghien_nam.Value = Utility.DecimaltoDbnull(objPK.ChatGaynghienNam);
                txt_chat_gaynghien_loai_mota.Text = chk_chat_gaynghien_loai.Checked ? Utility.sDbnull(objPK.ChatGaynghienLoaiMota):"";


                opt_tiepxuc_hoahchat_tiaxa_co.Checked = Utility.Bool2Bool(objPK.TiepxucHoahchatTiaxaCo);
                opt_tiepxuc_hoahchat_tiaxa_khong.Checked = Utility.Bool2Bool(objPK.TiepxucHoahchatTiaxaKhong);
                chk_tiepxuc_hoahchat_tiaxa_loai.Checked = Utility.Bool2Bool(objPK.TiepxucHoahchatTiaxaLoai);
                txt_tiepxuc_hoahchat_tiaxa_loai_mota.Text = opt_tiepxuc_hoahchat_tiaxa_co.Checked ? Utility.sDbnull(objPK.TiepxucHoahchatTiaxaLoaiMota):"";
                txt_tiepxuc_hoahchat_tiaxa_mota.Text = Utility.sDbnull(objPK.TiepxucHoahchatTiaxaMota);
                nmr_tiepxuc_hoahchat_tiaxa_nam.Value = Utility.DecimaltoDbnull(objPK.TiepxucHoahchatTiaxaNam);
                txt_nguyco_khac.Text = Utility.sDbnull(objPK.NguycoKhacMota);

                opt_tiensu_giadinh_co.Checked = Utility.Bool2Bool(objPK.TiensuGiadinhCo);
                opt_tiensu_giadinh_khong.Checked = Utility.Bool2Bool(objPK.TiensuGiadinhKhong);
               
                chk_tsgd_benh_timmach.Checked = Utility.Bool2Bool(objPK.TsgdBenhTimmach);
                txt_tsgd_benh_timmach_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhTimmachNguoimac);

                chk_tsgd_benh_tanghuyetap.Checked = Utility.Bool2Bool(objPK.TsgdBenhTanghuyetap);
                txt_tsgd_benh_tanghuyetap_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhTanghuyetapNguoimac);
                
                chk_tsgd_benh_tamthan.Checked = Utility.Bool2Bool(objPK.TsgdBenhTamthan);
                txt_tsgd_benh_tamthan_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhTamthanNguoimac);
               
                chk_tsgd_benh_gout.Checked = Utility.Bool2Bool(objPK.TsgdBenhGout);
                txt_tsgd_benh_gout_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhGoutNguoimac);

                chk_tsgd_benh_hethong.Checked = Utility.Bool2Bool(objPK.TsgdBenhHethong);
                txt_tsgd_benh_hethong_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhHethongNguoimac);

                chk_tsgd_benh_ungthu.Checked = Utility.Bool2Bool(objPK.TsgdBenhUngthu);
                txt_tsgd_benh_ungthu_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhUngthuNguoimac);

                chk_tsgd_benh_daithaoduong.Checked = Utility.Bool2Bool(objPK.TsgdBenhDaithaoduong);
                txt_tsgd_benh_daithaoduong_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhDaithaoduongNguoimac);

                chk_tsgd_benh_lao.Checked = Utility.Bool2Bool(objPK.TsgdBenhLao);
                txt_tsgd_benh_lao_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhLaoNguoimac);

                chk_tsgd_benh_hensuyen.Checked = Utility.Bool2Bool(objPK.TsgdBenhHensuyen);
                txt_tsgd_benh_hensuyen_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhHensuyenNguoimac);

                chk_tsgd_benh_daithaoduong.Checked = Utility.Bool2Bool(objPK.TsgdBenhDaithaoduong);
                txt_tsgd_benh_daithaoduong_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhDaithaoduongNguoimac);

                chk_tsgd_benh_dongkinh.Checked = Utility.Bool2Bool(objPK.TsgdBenhDongkinh);
                txt_tsgd_benh_dongkinh_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhDongkinhNguoimac);

                chk_tsgd_benh_lienquan_chuyenhoa.Checked = Utility.Bool2Bool(objPK.TsgdBenhLienquanChuyenhoa);
                txt_tsgd_benh_lienquan_chuyenhoa_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhLienquanChuyenhoaNguoimac);

                chk_tsgd_benh_khac.Checked = Utility.Bool2Bool(objPK.TsgdBenhKhac);
                txt_tsgd_benh_khac_nguoimac.Text = Utility.sDbnull(objPK.TsgdBenhKhacNguoimac);

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public void SetData(EmrPhieukhamNgoaikhoa objPK)
        {
            try
            {
                objPK.DiungKhong = opt_diung_khong.Checked;
                objPK.DiungCo = opt_diung_co.Checked;
                objPK.DiungMota = opt_diung_co.Checked?Utility.sDbnull(txt_diung_mota.Text):"";
                
                objPK.DiungHoachatMyphamCo = opt_diung_hoachat_mypham_co.Checked;
                objPK.DiungHoachatMyphamKhong = opt_diung_hoachat_mypham_khong.Checked;
                objPK.DiungHoachatMyphamMota = opt_diung_hoachat_mypham_co.Checked ? Utility.sDbnull(txt_diung_hoachat_mypham_mota.Text):"";

                objPK.DiungThuocKhong = opt_diung_thuoc_khong.Checked;
                objPK.DiungThuocCo = opt_diung_thuoc_co.Checked;
                objPK.DiungThuocMota = opt_diung_thuoc_co.Checked ? Utility.sDbnull(txt_diung_thuoc_mota.Text):"";

                objPK.DiungThucphamCo = opt_diung_thucpham_co.Checked;
                objPK.DiungThucphamKhong = opt_diung_thucpham_khong.Checked;
                objPK.DiungThucphamMota = opt_diung_thucpham_co.Checked? Utility.sDbnull(txt_diung_thucpham_mota.Text):"";
              
                objPK.DiungKhacMota = Utility.sDbnull(txt_diung_khac_mota.Text);

                objPK.ThuocDangdungKhong = opt_thuoc_dangdung_khong.Checked;
                objPK.ThuocDangdungCo = opt_thuoc_dangdung_co.Checked;
                objPK.ThuocDangdungMota = opt_thuoc_dangdung_co.Checked? Utility.sDbnull(txt_thuoc_dangdung_mota.Text):"";

                objPK.BenhNoitietCo = opt_benh_noitiet_co.Checked;
                objPK.BenhNoitietKhong = opt_benh_noitiet_khong.Checked;
                objPK.BenhNoitietMota = opt_benh_noitiet_co.Checked?Utility.sDbnull(txt_benh_noitiet_mota.Text):"";

                objPK.TsbCo = opt_tsb_co.Checked;
                objPK.TsbKhong = opt_tsb_khong.Checked;
                objPK.TsbMota = opt_tsb_co.Checked? Utility.sDbnull(txt_tsb_mota.Text):"";

                objPK.BenhThankinhKhong = opt_benh_thankinh_khong.Checked;
                objPK.BenhThankinhCo = opt_benh_thankinh_co.Checked;
                objPK.BenhThankinhMota = opt_benh_thankinh_co.Checked? Utility.sDbnull(txt_benh_thankinh_mota.Text):"";

                objPK.BenhTimmachCo = opt_benh_timmach_co.Checked;
                objPK.BenhTimmachKhong = opt_benh_timmach_khong.Checked;
                objPK.BenhTimmachMota = opt_benh_timmach_co.Checked?Utility.sDbnull(txt_benh_timmach_mota.Text):"";

                objPK.BenhHohapCo = opt_benh_hohap_co.Checked;
                objPK.BenhHohapKhong = opt_benh_hohap_khong.Checked;
                objPK.BenhHohapMota = opt_benh_hohap_co.Checked?Utility.sDbnull(txt_benh_hohap_mota.Text):"";

                objPK.BenhTieuhoaKhong = opt_benh_tieuhoa_khong.Checked;
                objPK.BenhTieuhoaCo = opt_benh_tieuhoa_co.Checked;
                objPK.BenhTieuhoaMota = opt_benh_tieuhoa_co.Checked? Utility.sDbnull(txt_benh_tieuhoa_mota.Text):"";

                objPK.BenhThantietnieuCo = opt_benh_thantietnieu_co.Checked;
                objPK.BenhThantietnieuKhong = opt_benh_thantietnieu_khong.Checked;
                objPK.BenhThantietnieuMota = opt_benh_thantietnieu_co.Checked? Utility.sDbnull(txt_benh_thantietnieu_mota.Text):"";

                objPK.BenhCoxuongkhopCo = opt_benh_coxuongkhop_co.Checked;
                objPK.BenhCoxuongkhopKhong = opt_benh_coxuongkhop_khong.Checked;
                objPK.BenhCoxuongkhopMota = opt_benh_coxuongkhop_co.Checked?Utility.sDbnull(txt_benh_coxuongkhop_mota.Text):"";

                objPK.BenhUngthuCo = opt_benh_ungthu_co.Checked;
                objPK.BenhUngthuKhong = opt_benh_ungthu_khong.Checked;
                objPK.BenhUngthuMota = opt_benh_ungthu_co.Checked? Utility.sDbnull(txt_benh_ungthu_mota.Text):"";

                objPK.BenhKhacMota = Utility.sDbnull(txt_benh_khac_mota.Text);

                objPK.TiensuPhauthuatCo = opt_tiensu_phauthuat_co.Checked;
                objPK.TiensuPhauthuatKhong = opt_tiensu_phauthuat_khong.Checked;
                objPK.TiensuPhauthuatMota = opt_tiensu_phauthuat_co.Checked? Utility.sDbnull(txt_tiensu_phauthuat_mota.Text):"";

                objPK.YeutoNguycoKhong = opt_yeuto_nguyco_khong.Checked;
                objPK.YeutoNguycoCo = opt_yeuto_nguyco_co.Checked;
                objPK.YeutoNguycoMota = opt_yeuto_nguyco_co.Checked? Utility.sDbnull(txt_yeuto_nguyco_mota.Text):"";

                objPK.ThuoclaKhong = opt_thuocla_khong.Checked;
                objPK.ThuoclaCo = opr_thuocla_co.Checked;
                objPK.ThuoclaNam = opt_ruou_bia_co.Checked ? Utility.Int16Dbnull(nmr_thuocla_nam.Value) : (Int16)0;
                objPK.ThuoclaGoi = opt_ruou_bia_co.Checked ? Utility.Int16Dbnull(nmr_thuocla_goi.Value) : (Int16)0;
                objPK.ThuoclaMota = opt_ruou_bia_co.Checked ? Utility.sDbnull(txt_thuocla_mota.Text) : "";

                objPK.ThuoclaoKhong = txt_thuoclao_khong.Checked;
                objPK.ThuoclaoCo = opt_thuoclao_co.Checked;
                objPK.ThuoclaoMota = opt_thuoclao_co.Checked? Utility.sDbnull(txt_thuoclao_mota.Text):"";
                objPK.ThuoclaoNam = opt_thuoclao_co.Checked? Utility.Int16Dbnull(nmr_thuoclao_nam.Value):(Int16)0;

                objPK.RuouBiaCo = opt_ruou_bia_co.Checked;
                objPK.RuouBiaKhong = opt_ruou_bia_khong.Checked;
                objPK.RuouBiaIt = opt_ruou_bia_co.Checked? opt_ruou_bia_it.Checked:false;
                objPK.RuouBiaThinhthoang = opt_ruou_bia_co.Checked? opt_ruou_bia_thinhthoang.Checked:false;
                objPK.RuouBiaThuongXuyen = opt_ruou_bia_co.Checked?opt_ruou_bia_thuong_xuyen.Checked:false;
                objPK.RuouBiaMota = opt_ruou_bia_co.Checked? Utility.sDbnull(txt_ruou_bia_mota.Text):"";

                objPK.ChatGaynghienCo = opt_chat_gaynghien_co.Checked;
                objPK.ChatGaynghienKhong = opt_chat_gaynghien_khong.Checked;
                objPK.ChatGaynghienLoai = opt_chat_gaynghien_co.Checked? chk_chat_gaynghien_loai.Checked:false;
                objPK.ChatGaynghienLoaiMota = objPK.ChatGaynghienLoai.Value ? Utility.sDbnull(txt_chat_gaynghien_loai_mota.Text) : "";
                objPK.ChatGaynghienNam = opt_chat_gaynghien_co.Checked?Utility.Int32Dbnull(nmr_chat_gaynghien_nam.Value):0;

                objPK.TiepxucHoahchatTiaxaCo = opt_tiepxuc_hoahchat_tiaxa_co.Checked;
                objPK.TiepxucHoahchatTiaxaKhong = opt_tiepxuc_hoahchat_tiaxa_khong.Checked;
                objPK.TiepxucHoahchatTiaxaLoai = opt_tiepxuc_hoahchat_tiaxa_co.Checked? chk_tiepxuc_hoahchat_tiaxa_loai.Checked:false;
                objPK.TiepxucHoahchatTiaxaLoaiMota = opt_tiepxuc_hoahchat_tiaxa_co.Checked? Utility.sDbnull(txt_tiepxuc_hoahchat_tiaxa_loai_mota.Text):"";
                objPK.TiepxucHoahchatTiaxaMota = opt_tiepxuc_hoahchat_tiaxa_co.Checked? Utility.sDbnull(txt_tiepxuc_hoahchat_tiaxa_mota.Text):"";
                objPK.TiepxucHoahchatTiaxaNam = opt_tiepxuc_hoahchat_tiaxa_co.Checked?Utility.Int32Dbnull( nmr_tiepxuc_hoahchat_tiaxa_nam.Value):0;
                objPK.NguycoKhacMota = Utility.sDbnull(txt_nguyco_khac.Text);

              
                objPK.TiensuGiadinhCo = opt_tiensu_giadinh_co.Checked;
                objPK.TiensuGiadinhKhong = opt_tiensu_giadinh_khong.Checked;

                objPK.TsgdBenhTimmach = chk_tsgd_benh_timmach.Checked;
                objPK.TsgdBenhTimmachNguoimac = chk_tsgd_benh_timmach.Checked? Utility.sDbnull(txt_tsgd_benh_timmach_nguoimac.Text):"";

                objPK.TsgdBenhTanghuyetap = chk_tsgd_benh_tanghuyetap.Checked;
                objPK.TsgdBenhTanghuyetapNguoimac = chk_tsgd_benh_tanghuyetap.Checked ? Utility.sDbnull(txt_tsgd_benh_tanghuyetap_nguoimac.Text) : "";

                objPK.TsgdBenhTamthan = chk_tsgd_benh_tamthan.Checked;
                objPK.TsgdBenhTamthanNguoimac = chk_tsgd_benh_tamthan.Checked ? Utility.sDbnull(txt_tsgd_benh_tamthan_nguoimac.Text) : "";

                objPK.TsgdBenhGout = chk_tsgd_benh_gout.Checked;
                objPK.TsgdBenhGoutNguoimac = chk_tsgd_benh_gout.Checked ? Utility.sDbnull(txt_tsgd_benh_gout_nguoimac.Text) : "";

                objPK.TsgdBenhHethong = chk_tsgd_benh_hethong.Checked;
                objPK.TsgdBenhHethongNguoimac = chk_tsgd_benh_hethong.Checked ? Utility.sDbnull(txt_tsgd_benh_hethong_nguoimac.Text) : "";

                objPK.TsgdBenhUngthu = chk_tsgd_benh_ungthu.Checked;
                objPK.TsgdBenhUngthuNguoimac = chk_tsgd_benh_ungthu.Checked ? Utility.sDbnull(txt_tsgd_benh_ungthu_nguoimac.Text) : "";

                objPK.TsgdBenhDaithaoduong = chk_tsgd_benh_daithaoduong.Checked;
                objPK.TsgdBenhDaithaoduongNguoimac = chk_tsgd_benh_daithaoduong.Checked ? Utility.sDbnull(txt_tsgd_benh_daithaoduong_nguoimac.Text) : "";

                objPK.TsgdBenhLao = chk_tsgd_benh_lao.Checked;
                objPK.TsgdBenhLaoNguoimac = chk_tsgd_benh_lao.Checked ? Utility.sDbnull(txt_tsgd_benh_lao_nguoimac.Text) : "";

                objPK.TsgdBenhHensuyen = chk_tsgd_benh_hensuyen.Checked;
                objPK.TsgdBenhHensuyenNguoimac = chk_tsgd_benh_hensuyen.Checked ? Utility.sDbnull(txt_tsgd_benh_hensuyen_nguoimac.Text) : "";

                objPK.TsgdBenhDongkinh = chk_tsgd_benh_dongkinh.Checked;
                objPK.TsgdBenhDongkinhNguoimac = chk_tsgd_benh_dongkinh.Checked ? Utility.sDbnull(txt_tsgd_benh_dongkinh_nguoimac.Text) : "";

                objPK.TsgdBenhLienquanChuyenhoa = chk_tsgd_benh_lienquan_chuyenhoa.Checked;
                objPK.TsgdBenhLienquanChuyenhoaNguoimac = chk_tsgd_benh_lienquan_chuyenhoa.Checked ? Utility.sDbnull(txt_tsgd_benh_lienquan_chuyenhoa_nguoimac.Text) : "";

                objPK.TsgdBenhKhac = chk_tsgd_benh_khac.Checked;
                objPK.TsgdBenhKhacNguoimac = chk_tsgd_benh_khac.Checked ? Utility.sDbnull(txt_tsgd_benh_khac_nguoimac.Text):"";



            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_diung_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_diung_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_diung_mota.Focus();

        }

        private void opt_diung_thuoc_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_diung_thuoc_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_diung_thuoc_mota.Focus();
        }

        private void opt_diung_hoachat_mypham_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_diung_hoachat_mypham_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_diung_hoachat_mypham_mota.Focus();
        }

        private void opt_diung_thucpham_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_diung_thucpham_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_diung_thucpham_mota.Focus();
        }

        private void opt_thuoc_dangdung_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_thuoc_dangdung_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_thuoc_dangdung_mota.Focus();
        }

        private void opt_tsb_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_tsb_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_tsb_mota.Focus();
        }

        private void opt_benh_noitiet_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_benh_noitiet_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_benh_noitiet_mota.Focus();
        }

        private void opt_benh_thankinh_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_benh_thankinh_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_benh_thankinh_mota.Focus();
        }

        private void opt_benh_thantietnieu_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_benh_thantietnieu_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_benh_thantietnieu_mota.Focus();
        }

        private void opt_benh_coxuongkhop_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_benh_coxuongkhop_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_benh_coxuongkhop_mota.Focus();
        }

        private void opt_benh_ungthu_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_benh_ungthu_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_benh_ungthu_mota.Focus();
        }

        private void opt_benh_timmach_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_benh_timmach_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_benh_timmach_mota.Focus();
        }

        private void opt_benh_hohap_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_benh_hohap_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_benh_hohap_mota.Focus();
        }

        private void opt_benh_tieuhoa_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_benh_tieuhoa_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_benh_tieuhoa_mota.Focus();
        }

        private void opt_tiensu_phauthuat_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_tiensu_phauthuat_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_tiensu_phauthuat_mota.Focus();
        }

        private void opt_yeuto_nguyco_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_yeuto_nguyco_mota.Enabled =  _obj.Checked;
            if (_obj.Checked) txt_yeuto_nguyco_mota.Focus();
        }

        private void opr_thuocla_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_thuocla_mota.Enabled = nmr_thuocla_nam.Enabled= nmr_thuocla_goi.Enabled= _obj.Checked;
            if (_obj.Checked) txt_thuocla_mota.Focus();
        }

        private void opt_thuoclao_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_thuoclao_mota.Enabled = nmr_thuoclao_nam.Enabled= _obj.Checked;
            if (_obj.Checked) txt_thuoclao_mota.Focus();
        }

        private void opt_ruou_bia_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_ruou_bia_mota.Enabled = opt_ruou_bia_thuong_xuyen.Enabled= opt_ruou_bia_thinhthoang.Enabled= opt_ruou_bia_it.Enabled= _obj.Checked;
            if (_obj.Checked) txt_ruou_bia_mota.Focus();

        }

        private void opt_chat_gaynghien_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_chat_gaynghien_mota.Enabled = chk_chat_gaynghien_loai.Enabled= nmr_chat_gaynghien_nam.Enabled= _obj.Checked;
            if (_obj.Checked) txt_chat_gaynghien_mota.Focus();
        }

        private void opt_tiepxuc_hoahchat_tiaxa_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_tiepxuc_hoahchat_tiaxa_mota.Enabled = chk_tiepxuc_hoahchat_tiaxa_loai.Enabled= nmr_tiepxuc_hoahchat_tiaxa_nam.Enabled= _obj.Checked;
            if (_obj.Checked) txt_tiepxuc_hoahchat_tiaxa_mota.Focus();
        }


        private void chk_chat_gaynghien_loai_CheckedChanged(object sender, EventArgs e)
        {
            txt_chat_gaynghien_loai_mota.Enabled = chk_chat_gaynghien_loai.Checked;
        }

        private void chk_tiepxuc_hoahchat_tiaxa_loai_CheckedChanged(object sender, EventArgs e)
        {
            txt_tiepxuc_hoahchat_tiaxa_loai_mota.Enabled = chk_tiepxuc_hoahchat_tiaxa_loai.Enabled;
        }

        private void grdPTTT_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

        private void chk_tsgd_benh_timmach_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_timmach_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_tanghuyetap_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_tanghuyetap_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_tamthan_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_tamthan_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_gout_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_gout_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_hethong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_hethong_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_ungthu_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_ungthu_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_lao_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_lao_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_khac_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_khac_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_hensuyen_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_hensuyen_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_daithaoduong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_daithaoduong_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_dongkinh_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_dongkinh_nguoimac, sender as CheckBox);
        }

        private void chk_tsgd_benh_lienquan_chuyenhoa_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tsgd_benh_lienquan_chuyenhoa_nguoimac, sender as CheckBox);
        }

        private void cmd_them_tiensu_pttt_Click(object sender, EventArgs e)
        {
            try
            {
                cmd_them_tiensu_pttt.Enabled = false;
                DataRow newItem = dt_tiensu_pttt.NewRow();
                //Thêm luôn vào CSDL
                EmrTiensuPhauthuatNgoaikhoa obj = new EmrTiensuPhauthuatNgoaikhoa();
                obj.IdBenhnhan = objLuotkham.IdBenhnhan;
                obj.MaLuotkham = objLuotkham.MaLuotkham;
                obj.SoLuong = 1;
                obj.ViTri = "";
                obj.NoiThuchien = "";
                obj.ThoiGian = "";
                obj.NgayTao = globalVariables.SysDate;
                obj.NguoiTao = globalVariables.UserName;
                obj.Save();
                Utility.FromObjectToDatarow(obj, ref newItem);
                newItem["UUID"] = Guid.NewGuid().ToString();
                dt_tiensu_pttt.Rows.Add(newItem);
                cmd_them_tiensu_pttt.Enabled = true;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }
    }
}
