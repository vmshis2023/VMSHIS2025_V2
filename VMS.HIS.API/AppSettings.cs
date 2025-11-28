using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMS.HIS.API
{
    
    public class AppSettingMisaInvoices {
        public string LinkAPI { get; set; }
        public string appid { get; set; }
        public string taxcode { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string API_Token { get; set; }
        public string API_Templete { get; set; }
        public string API_Phathanh { get; set; }

        public string API_TaiHoaDon { get; set; }
        public string API_LayTrangThai { get; set; }
        public string API_HuyHoaDon { get; set; }
        public string API_XemtruocHD { get; set; }
  }
    public class Bacsy
    {
        public string ma_lien_thong_bac_si { get; set; }
    }

    public class GuiDonThuocResponse
    {
        public string success { get; set; }
        public string checksum { get; set; }
        public object error { get; set; }
        public class Error
        {
            public List<string> loai_don_thuoc { get; set; }
            public List<string> ma_don_thuoc { get; set; }

        }
    }
    public class TokenBacSy
    {
        public string ma_lien_thong_bac_si { get; set; }
        public string ma_lien_thong_co_so_kham_chua_benh { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string token_type { get; set; }
        public string create_date { get; set; }
        public string expire_date { get; set; }
    }
    public class DangNhapBacSy
    {
        public string ma_lien_thong_bac_si { get; set; }
        public string ma_lien_thong_co_so_kham_chua_benh { get; set; }
        public string password { get; set; }
    }


    public class Dangnhap
    {
        public string ma_lien_thong_co_so_kham_chua_benh { get; set; }
        public string password { get; set; }
    }
    public class TokenCSKCB
    {
        public string token { get; set; }
        public string type_token { get; set; }
        public string create_date { get; set; }
        public string expire_date { get; set; }

    }
    public class AppSettingDonThuocQG
    {
        public string AppName { get; set; }
        public string AppKey { get; set; }
        public string LinkAPI { get; set; }
        public string Gettoken { get; set; }
        public string Ma_lien_thong_co_so_kham { get; set; }
        public string Password { get; set; }
        public string APIgetToken { get; set; }
        public string APIthembacsi { get; set; }
        public string APIxoabacsi { get; set; }
        public string APIdangnhapbacsi { get; set; }
        public string APIguidonthuoc { get; set; }
        public string APIlaydonthuoc { get; set; }
        public string APIcapnhatdonthuoc { get; set; }
        public string ConnectionString { get; set; }
    }
    public class ResponseData
    {
        public bool IsSuccess { get; set; }
        public string Messge { get; set; }
        public object data { get; set; }
    }
}
