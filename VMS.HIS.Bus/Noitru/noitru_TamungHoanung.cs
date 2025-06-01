using System;
using System.Data;
using System.Transactions;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using System.Text;

using SubSonic;
using NLog;
using VNS.Properties;
using System.Collections.Generic;

namespace VNS.HIS.BusRule.Classes
{
    public class noitru_TamungHoanung
    {
        private NLog.Logger log;
        public noitru_TamungHoanung()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        public static decimal LaySoTienTamUng(string ma_luotkham, long id_benhnhan, short kieu_tamung)
        {
            decimal tongso = 0;
            StoredProcedure sp = SPs.NoitruTamungGetsotien(ma_luotkham, id_benhnhan, Utility.ByteDbnull(kieu_tamung));
            DataTable dt = sp.GetDataSet().Tables[0];
            tongso = Utility.DecimaltoDbnull(dt.Compute("SUM(so_tien)", ""),0);
            return tongso;
        }
        public static  bool NoptienTamung(NoitruTamung objTamung)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        objTamung.Save();
                        //KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(objTamung.IdBenhnhan, objTamung.MaLuotkham);
                        //if (objKcbLuotkham != null)
                        //{
                        //    objKcbLuotkham.IsNew = false;
                        //    objKcbLuotkham.MarkOld();
                        //    if (Utility.ByteDbnull(objKcbLuotkham.TrangthaiNoitru, 0) == 1)
                        //    {
                        //        objKcbLuotkham.TrangthaiNoitru = 2;
                        //        objKcbLuotkham.Save();
                        //    }
                        //}
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(-1l, -1l, objTamung.Id, objTamung.MaPttt, objTamung.MaNganhang,
                                    objTamung.IdBenhnhan, objTamung.MaLuotkham,
                                    objTamung.Noitru, objTamung.SoTien, objTamung.SoTien,
                                    objTamung.NguoiTao, objTamung.NgayTao, "", objTamung.NgayTao,-1,0,1).Execute();
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static DataTable BaocaoTamungHoanung(int IdTNV, string tungay, string denngay, short IdKhoanoitru, short IdDoituongKcb, byte kieutamung)
        {
            return SPs.BaocaoTamungHoanung(IdTNV, tungay, denngay, IdKhoanoitru, IdDoituongKcb, kieutamung).GetDataSet().Tables[0];

        }
        public static bool XoaPhieuthuchikhac(KcbPhieuthu objThuChi, bool VanConTamung, string lydohuy)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        KcbLoghuy objHuy = new KcbLoghuy();
                        objHuy.IdBenhnhan =(long) objThuChi.IdBenhnhan;
                        objHuy.MaLuotkham = objThuChi.MaLuotkham;
                        objHuy.LoaiphieuHuy = 4;//0= hủy thanh toán;1= hủy phiếu chi;2= hủy tạm ứng;3=Hủy phiếu thu khác;4=Hủy phiếu chi khác
                        objHuy.LydoHuy = lydohuy;
                        objHuy.SotienHuy = Utility.DecimaltoDbnull(objThuChi.SoTien, 0);
                        objHuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objHuy.NgayHuy = DateTime.Now;
                        objHuy.NguoiTao = globalVariables.UserName;
                        objHuy.NgayTao = DateTime.Now;
                        objHuy.IsNew = true;
                        objHuy.Save();
                        new Delete().From(KcbPhieuthu.Schema).Where(KcbPhieuthu.Columns.IdPhieuthu).IsEqualTo(objThuChi.IdPhieuthu).Execute();
                        new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema).Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdPhieuthu).IsEqualTo(objThuChi.IdPhieuthu).Execute();
                        Utility.Log("Thu - chi khác", globalVariables.UserName, string.Format("Xóa phiếu thu chi: Id= {0}, số tiền: ={1} của người bệnh {2} mã lần khám {3} ", objThuChi.IdPhieuthu,objThuChi.SoTien, objHuy.TenBenhnhan, objHuy.MaLuotkham), newaction.Delete, "ucThuchikhac");
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool XoaTienMG(KcbChietkhau objCK,  string lydohuy)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        KcbLoghuy objHuy = new KcbLoghuy();
                        objHuy.IdBenhnhan = objCK.IdBenhnhan;
                        objHuy.MaLuotkham = objCK.MaLuotkham;
                        objHuy.LoaiphieuHuy = 2;//0= hủy thanh toán;1= hủy phiếu chi;2= hủy tạm ứng;3=Hủy phiếu thu khác;4=Hủy phiếu chi khác
                        objHuy.LydoHuy = lydohuy;
                        objHuy.SotienHuy = Utility.DecimaltoDbnull(objCK.SoTien, 0);
                        objHuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objHuy.NgayHuy = DateTime.Now;
                        objHuy.NguoiTao = globalVariables.UserName;
                        objHuy.NgayTao = DateTime.Now;
                        objHuy.IsNew = true;
                        objHuy.Save();
                        new Delete().From(KcbChietkhau.Schema).Where(KcbChietkhau.Columns.IdChietkhau).IsEqualTo(objCK.IdChietkhau).Execute();
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Phiên bản xóa là xóa khỏi CSDL. Thay bằng phiên bản dưới đánh dấu trạng thái hủy để làm báo cáo doanh thu
        /// </summary>
        /// <param name="objTamung"></param>
        /// <param name="VanConTamung"></param>
        /// <param name="lydohuy"></param>
        /// <returns></returns>
        public static bool XoaTienTamung_V1(NoitruTamung objTamung,bool VanConTamung,string lydohuy)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        KcbLoghuy objHuy = new KcbLoghuy();
                        objHuy.IdBenhnhan = objTamung.IdBenhnhan;
                        objHuy.MaLuotkham = objTamung.MaLuotkham;
                        objHuy.LoaiphieuHuy = 2;//0= hủy thanh toán;1= hủy phiếu chi;2= hủy tạm ứng;3=Hủy phiếu thu khác;4=Hủy phiếu chi khác
                        objHuy.LydoHuy = lydohuy;
                        objHuy.SotienHuy =Utility.DecimaltoDbnull( objTamung.SoTien,0);
                        objHuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objHuy.NgayHuy=DateTime.Now;
                        objHuy.NguoiTao=globalVariables.UserName;
                        objHuy.NgayTao=DateTime.Now;
                        objHuy.IsNew=true;
                        objHuy.Save();
                        KcbTamungDichvuCollection lstDvu = new Select().From(KcbTamungDichvu.Schema).Where(KcbTamungDichvu.Columns.IdTamung).IsEqualTo(objTamung.Id).ExecuteAsCollection<KcbTamungDichvuCollection>();
                        new Delete().From(NoitruTamung.Schema).Where(NoitruTamung.Columns.Id).IsEqualTo(objTamung.Id).And(NoitruTamung.Columns.TrangThai).IsEqualTo(0).Execute();
                        new Delete().From(KcbTamungDichvu.Schema).Where(KcbTamungDichvu.Columns.IdTamung).IsEqualTo(objTamung.Id).Execute();
                        new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema).Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdTamung).IsEqualTo(objTamung.Id).Execute();
                        if (lstDvu.Count > 0)
                        {
                            foreach (KcbTamungDichvu _chitiet in lstDvu)
                            {
                                StoredProcedure spupdate = SPs.SpUpdateTrangthaitamthu(Convert.ToByte(_chitiet.LoaiDvu), objTamung.Id, objTamung.NgayTamung, objTamung.Noitru, "%", 0, 0, _chitiet.IdPhieu, _chitiet.IdPhieuchitiet, objTamung.NgayTao, objTamung.NguoiTao,1);
                                int reval = spupdate.Execute();
                            }
                        }
                        ////Tạm khóa 240912
                        //NoitruPhieudieutri objNoitruPhieudieutri = Utility.getNoitruPhieudieutri(objTamung.IdBenhnhan, objTamung.MaLuotkham);
                        //KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(objTamung.IdBenhnhan, objTamung.MaLuotkham);
                        //if (Utility.Byte2Bool(objKcbLuotkham.TrangthaiNoitru) && objNoitruPhieudieutri == null && !VanConTamung)//Chỉ update nếu là nội trú và chưa có phiếu điều trị
                        //{
                        //    objKcbLuotkham.IsNew = false;
                        //    objKcbLuotkham.TrangthaiNoitru = 1;
                        //    objKcbLuotkham.MarkOld();
                        //    objKcbLuotkham.Save();
                        //}
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool RestoreTienTamung(NoitruTamung objTamung)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                       
                        objTamung.MarkOld();
                        objTamung.IsNew = false;
                        objTamung.MaHtttHuy = "";
                        objTamung.MaNganhangHuy = "";
                        objTamung.LydoHuy = "";
                        objTamung.NgayHuy = null;
                        objTamung.TthaiHuy = 0;
                        objTamung.NguoiHuy = "";
                        objTamung.Save();
                        new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                            .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdTamung).IsEqualTo(objTamung.Id)
                            .And(KcbThanhtoanPhanbotheoPTTT.Columns.TthaiHuy).IsEqualTo(1)
                            .And(KcbThanhtoanPhanbotheoPTTT.Columns.LoaiPhanbo).IsEqualTo(1)
                            .Execute();

                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool XoaTienTamung(NoitruTamung objTamung, bool VanConTamung, string lydohuy, DateTime ngay_huy, string ma_pttt, string ma_nganhang)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        KcbLoghuy objHuy = new KcbLoghuy();
                        objHuy.IdBenhnhan = objTamung.IdBenhnhan;
                        objHuy.MaLuotkham = objTamung.MaLuotkham;
                        objHuy.LoaiphieuHuy = 2;//0= hủy thanh toán;1= hủy phiếu chi;2= hủy tạm ứng;3=Hủy phiếu thu khác;4=Hủy phiếu chi khác
                        objHuy.LydoHuy = lydohuy;
                        objHuy.SotienHuy = Utility.DecimaltoDbnull(objTamung.SoTien, 0);
                        objHuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objHuy.NgayHuy = DateTime.Now;
                        objHuy.NguoiTao = globalVariables.UserName;
                        objHuy.NgayTao = DateTime.Now;
                        objHuy.IsNew = true;
                        objHuy.Save();
                        objTamung.MarkOld();
                        objTamung.IsNew = false;
                        objTamung.MaHtttHuy = ma_pttt;
                        objTamung.MaNganhangHuy = ma_nganhang;
                        objTamung.LydoHuy = lydohuy;
                        objTamung.NgayHuy = ngay_huy;
                        objTamung.TthaiHuy = 1;
                        objTamung.NguoiHuy = globalVariables.UserName;
                        objTamung.Save();
                        KcbThanhtoanPhanbotheoPTTT newItem = new KcbThanhtoanPhanbotheoPTTT();
                        newItem.IdThanhtoan = -1;
                        newItem.IdPhieuthu = -1;
                        newItem.IdTamung = objTamung.Id;
                        newItem.MaPttt = objTamung.MaPttt;
                        newItem.IdBenhnhan = objTamung.IdBenhnhan;
                        newItem.MaLuotkham = objTamung.MaLuotkham;
                        newItem.TongTien = Utility.DecimaltoDbnull(objTamung.SoTien, 0);
                        newItem.NoiTru = (byte)objTamung.Noitru;
                        newItem.SoTien = Utility.DecimaltoDbnull(objTamung.SoTien, 0);
                        newItem.NguoiTao = globalVariables.UserName;
                        newItem.NgayTao = DateTime.Now;
                        newItem.MaNganhang = objTamung.MaNganhang;
                        newItem.LoaiPhanbo = 1;
                      
                        newItem.IsNew = true;
                        newItem.Save();
                        /////REM dòng dưới này để thêm loại phân bổ, đỡ phải chỉnh thủ tục
                        //SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(-1l, -1l, objTamung.Id, objTamung.MaHttt, objTamung.MaNganhang,
                        //           objTamung.IdBenhnhan, objTamung.MaLuotkham,
                        //           objTamung.Noitru, objTamung.SoTien, objTamung.SoTien,
                        //           objTamung.NguoiTao, objTamung.NgayTao, "", objTamung.NgayTao, -1, 1, 1).Execute();

                       
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DathanhtoanhetNgoaitru(long id_benhnhan, string ma_luotkham)
        {
            try
            {
                DataTable dtData = SPs.KcbKiemtrathanhtoanhetDoituongDVTruockhinhapvien(id_benhnhan, ma_luotkham).GetDataSet().Tables[0];
                if (dtData == null) return true;
                return Utility.DecimaltoDbnull(dtData.Compute("SUM(bnhan_chitra)", "1=1"), 0) == 0;
            }
            catch (Exception ex)
            {
                return false;
                
            }
        }
        public static DataTable NoitruInphieutamung(long idTamung,long id_benhnhan,string ma_luotkham)
        {
            try
            {
               return SPs.NoitruInphieutamung(idTamung, id_benhnhan, ma_luotkham).GetDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return null;

            }
        }
    }
}
