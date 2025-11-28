using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.HIS.BHYT;
using VNS.Libs;

namespace VNS.HIS.NGHIEPVU.THUOC
{
    public class DuocTT27
    {
        public delegate void OnStatus(string status, bool isErr);
        public event OnStatus _OnStatus;
        private readonly Logger _log;
        public DuocTT27()
        {
            _log = LogManager.GetLogger("DuocTT27");
            globalVariables.API_DONTHUOCQUOCGIA = THU_VIEN_CHUNG.Laygiatrithamsohethong("DONTHUOCQUOCGIA_API","",true);//
            _log.Trace("Load URL TT27 = " + globalVariables.API_DONTHUOCQUOCGIA);

        }

        public string ThemBacSy(string ma_lienthong_bacsy, ref bool ketqua, ref string msg)
        {
            ketqua = false;
            msg = "";
            try
            {
               if(globalVariables.API_DONTHUOCQUOCGIA=="") globalVariables.API_DONTHUOCQUOCGIA = THU_VIEN_CHUNG.Laygiatrithamsohethong("DONTHUOCQUOCGIA_API", "", true);//
                string url = globalVariables.API_DONTHUOCQUOCGIA;
                string apiLink = url + "/api/DTQG/ThemBacSy";
                const string contentType = "application/json";
                const string meThod = "POST";
                Thongtinbacsy objThongtinbacsy = new Thongtinbacsy();
                objThongtinbacsy.ma_lien_thong_bac_si = ma_lienthong_bacsy;
                string thongtin = JsonConvert.SerializeObject(objThongtinbacsy);
                string result = CreateRequest.WebRequest(apiLink, thongtin, "", meThod, contentType);
                _log.Trace(result);
                if (!string.IsNullOrEmpty(result))
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseDataDQG>(result);
                    if (objResponse != null)
                    {
                        if (objResponse.IsSuccess)
                        {
                            ketqua = true;
                            msg = objResponse.Messge;
                            if (_OnStatus != null)
                                _OnStatus(string.Format("Thêm bác sĩ {0} thành công", ma_lienthong_bacsy), false);
                            return result;
                        }
                        else
                        {
                            ketqua = false;
                            msg = objResponse.Messge;
                            if (_OnStatus != null)
                                _OnStatus(string.Format("Thêm bác sĩ thất bại với lỗi trả về: {0}", msg), false);
                            return result;
                        }

                    }
                    else
                    {
                        ketqua = false;
                        msg = result;
                        if (_OnStatus != null)
                            _OnStatus(string.Format("Lỗi khi thêm Bác sĩ: {0}", msg), false);
                        return result;
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                ketqua = false;
                _log.Trace(ex.Message);
                if (_OnStatus != null)
                    _OnStatus(string.Format("Lỗi ngoại lệ khi thêm Bác sĩ: {0}", ex.Message), false);
                return ex.Message;
            }
        }
        public string XoaBacSi(string ma_lienthong_bacsy, ref bool ketqua, ref string msg)
        {
            ketqua = false;
            msg = "";
            try
            {
                if (globalVariables.API_DONTHUOCQUOCGIA == "") globalVariables.API_DONTHUOCQUOCGIA = THU_VIEN_CHUNG.Laygiatrithamsohethong("DONTHUOCQUOCGIA_API", "", true);//
                string url = globalVariables.API_DONTHUOCQUOCGIA;
                string apiLink = url + "/api/DTQG/XoaBacSy";
                const string contentType = "application/json";
                const string meThod = "POST";
                Thongtinbacsy objThongtinbacsy = new Thongtinbacsy();
                objThongtinbacsy.ma_lien_thong_bac_si = ma_lienthong_bacsy;
                string thongtin = JsonConvert.SerializeObject(objThongtinbacsy);
                string result = CreateRequest.WebRequest(apiLink, thongtin, "", meThod, contentType);
                _log.Trace(result);
                if (!string.IsNullOrEmpty(result))
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseDataDQG>(result);
                    if (objResponse != null)
                    {
                        if (objResponse.IsSuccess)
                        {
                            ketqua = true;
                            msg = objResponse.Messge;
                            if (_OnStatus != null)
                                _OnStatus(string.Format("Xóa bác sĩ {0} thành công", ma_lienthong_bacsy), false);
                            return result;
                        }
                        else
                        {
                            ketqua = false;
                            msg = objResponse.Messge;
                            if (_OnStatus != null)
                                _OnStatus(string.Format("Xóa bác sĩ thất bại với lỗi trả về: {0}", msg), false);
                            return result;
                        }

                    }
                    else
                    {
                        ketqua = false;
                        msg = result;
                        if (_OnStatus != null)
                            _OnStatus(string.Format("Lỗi khi Xóa Bác sĩ: {0}", msg), false);
                        return result;
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                ketqua = false;
                _log.Trace(ex.Message);
                if (_OnStatus != null)
                    _OnStatus(string.Format("Lỗi ngoại lệ khi Xóa Bác sĩ: {0}", ex.Message), false);
                return ex.Message;
            }
        }
        public string DangNhapBacSi(string ma_lienthong_bacsy,string matkhau_lien_thong_bac_si, ref bool ketqua, ref string msg)
        {
            ketqua = false;
            msg = "";
            try
            {
                if (globalVariables.API_DONTHUOCQUOCGIA == "") globalVariables.API_DONTHUOCQUOCGIA = THU_VIEN_CHUNG.Laygiatrithamsohethong("DONTHUOCQUOCGIA_API", "", true);//
                string url = globalVariables.API_DONTHUOCQUOCGIA;
                string apiLink = url + "/api/DTQG/DangNhapBacSy";
                const string contentType = "application/json";
                const string meThod = "POST";
                DangNhapBacSy objDangnhapBacsi = new DangNhapBacSy();
                objDangnhapBacsi.ma_lien_thong_bac_si = ma_lienthong_bacsy;
                objDangnhapBacsi.password = matkhau_lien_thong_bac_si;
                objDangnhapBacsi.ma_lien_thong_co_so_kham_chua_benh = "";//Sẽ tự fill trên API do cấu hình ở AppSettings
                string thongtin = JsonConvert.SerializeObject(objDangnhapBacsi);
                string result = CreateRequest.WebRequest(apiLink, thongtin, "", meThod, contentType);
                _log.Trace(result);
                if (!string.IsNullOrEmpty(result))
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseDataDQG>(result);
                    if (objResponse != null)
                    {
                        if (objResponse.IsSuccess)
                        {
                            ketqua = true;
                            msg = objResponse.Messge;
                            if (_OnStatus != null)
                                _OnStatus(string.Format("Đăng nhập bác sĩ {0} thành công", ma_lienthong_bacsy), false);
                            return result;
                        }
                        else
                        {
                            ketqua = false;
                            msg = objResponse.Messge;
                            if (_OnStatus != null)
                                _OnStatus(string.Format("Đăng nhập bác sĩ thất bại với lỗi trả về: {0}", msg), false);
                            return result;
                        }

                    }
                    else
                    {
                        ketqua = false;
                        msg = result;
                        if (_OnStatus != null)
                            _OnStatus(string.Format("Lỗi khi Đăng nhập Bác sĩ: {0}", msg), false);
                        return result;
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                ketqua = false;
                _log.Trace(ex.Message);
                if (_OnStatus != null)
                    _OnStatus(string.Format("Lỗi ngoại lệ khi Đăng nhập Bác sĩ: {0}", ex.Message), false);
                return ex.Message;
            }
        }
        public string Guidonthuoc(string thongtin, ref bool ketqua, ref string msg, ref string _result)
        {
            ketqua = false;
            msg = "";
            try
            {
                if (globalVariables.API_DONTHUOCQUOCGIA == "") globalVariables.API_DONTHUOCQUOCGIA = THU_VIEN_CHUNG.Laygiatrithamsohethong("DONTHUOCQUOCGIA_API", "", true);//
                string url = globalVariables.API_DONTHUOCQUOCGIA;
                string apiLink = url + "/api/DTQG/GuiDonThuoc";
                const string contentType = "application/json";
                const string meThod = "POST";
                string result = CreateRequest.WebRequest(apiLink, thongtin, "", meThod, contentType);
                _log.Trace(result);
                _result = result;
                if (!string.IsNullOrEmpty(result))
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseDataDQG>(result);
                    if (objResponse != null)
                    {
                        if (objResponse.IsSuccess)
                        {
                            ketqua = true;
                            msg = objResponse.Messge;
                            if (_OnStatus != null)
                                _OnStatus("Gửi đơn thuốc thành công", false);
                            return result;
                        }
                        else
                        {
                            ketqua = false;
                            msg = objResponse.Messge;
                            if (_OnStatus != null)
                                _OnStatus(string.Format( "Gửi đơn thuốc thất bại: {0}", msg), false);
                            return result;
                        }

                    }
                    else
                    {
                        ketqua = false;
                        msg = result;
                        if (_OnStatus != null)
                            _OnStatus(string.Format("Lỗi khi gửi Đơn thuốc: {0}", msg), false);
                        return result;
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                ketqua = false;
                _log.Trace(ex.Message);
                if (_OnStatus != null)
                    _OnStatus(string.Format("Lỗi ngoại lệ khi gửi đơn thuốc: {0}", ex.Message), false);
                return ex.Message;
            }
        }
    }

    public class ResponseDataDQG
    {
        public bool IsSuccess { get; set; }
        public string Messge { get; set; }
        public object data { get; set; }
    }
    public class DangNhapBacSy
    {
        public string ma_lien_thong_bac_si { get; set; }
        public string ma_lien_thong_co_so_kham_chua_benh { get; set; }
        public string password { get; set; }
    }
    public class Thongtinbacsy
    {
        public string ma_lien_thong_bac_si { get; set; }
    }


    public class Response
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
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
}
