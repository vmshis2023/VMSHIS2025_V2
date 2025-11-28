using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMS.Helpers
{
    public class DonThuocQuocGiaModel
    {
        public class thongTinDonThuoc
        {
            public string ma_thuoc { get; set; }
            public string biet_duoc { get; set; }
            public string ten_thuoc { get; set; }
            public string don_vi_tinh { get; set; }
            public string so_luong { get; set; }
            public string cach_dung { get; set; }
        }
        public class capnhatsoluongban
        {
            public string ma_don_thuoc { get; set; }
            public List<thongTinDonThuoc> thong_tin_thuoc { get; set; }
            public string ma_dinh_danh_co_so_cung_ung_thuoc { get; set; }
            public string ten_co_so_cung_ung_thuoc { get; set; }
            public string so_dien_thoai_co_so_cung_ung_thuoc { get; set; }
            public string dia_chi_co_so_cung_ung_thuoc { get; set; }
            public string ma_hoa_don { get; set; }
        }


        public class Guidonthuoc
        {
            [JsonIgnore]
            public int id_donthuoc_tt { get; set; }
            [JsonIgnore]
            public string ma_lien_thong_bac_si { get; set; }
            [JsonIgnore]
            public string password { get; set; }
            [JsonIgnore]
            public string ma_lien_thong_co_so_kham_chua_benh { get; set; }
            public string loai_don_thuoc { get; set; }
            public string ma_don_thuoc { get; set; }
            public string ho_ten_benh_nhan { get; set; }
            public string ma_dinh_danh_y_te { get; set; }
            public string ma_dinh_danh_cong_dan { get; set; }
            public string ngay_sinh_benh_nhan { get; set; }
            public decimal can_nang { get; set; }
            public int gioi_tinh { get; set; }
            public string ma_so_the_bao_hiem_y_te { get; set; }
            public string thong_tin_nguoi_giam_ho { get; set; }
            public string dia_chi { get; set; }
            public List<Chan_doan> chan_doan { get; set; }
            public string luu_y { get; set; }
            public int hinh_thuc_dieu_tri { get; set; }
            public List<dot_dung_thuoc> dot_dung_thuoc { get; set; }
            public List<thong_tin_don_thuoc> thong_tin_don_thuoc { get; set; }
            public string loi_dan { get; set; }
            public string so_dien_thoai_nguoi_kham_benh { get; set; }
            public string ngay_tai_kham { get; set; }
            public string ngay_gio_ke_don { get; set; }
            public string signature { get; set; }

        }

        public class thong_tin_don_thuoc
        {
            public string ma_thuoc { get; set; }
            public string biet_duoc { get; set; }
            public string ten_thuoc { get; set; }
            public string don_vi_tinh { get; set; }
            public decimal so_luong { get; set; }
            public string cach_dung { get; set; }
        }

        public class dot_dung_thuoc
        {
            public string dot { get; set; }
            public string tu_ngay { get; set; }
            public string den_ngay { get; set; }
        }

        public class Chan_doan
        {
            public string ma_chan_doan { get; set; }
            public string ten_chan_doan { get; set; }
            public string ket_luan { get; set; }
        }

    }
}
