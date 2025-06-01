using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using System.Transactions;
using VNS.Properties;
using System.Data;
using VNS.HIS.BusRule.Classes;

namespace VNS.Libs
{
    public class ChuyenDoituongKCB
    {
        public static ActionResult CapnhatLichsuDoituongKCB(List<KcbLichsuDoituongKcb> lstLichsu,List<long> lstDelete)
        {
            try
            {
                ActionResult _ActionResult = ActionResult.Success;
                
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        KcbLichsuDoituongKcb _item=lstLichsu[0];

                        List<long> lstID = new List<long>();
                        KcbDangkyKcbCollection lstDangkyKCB = new Select().From(KcbDangkyKcb.Schema)
                            .Where(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(_item.IdBenhnhan)
                            .And(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(_item.MaLuotkham)
                            .ExecuteAsCollection<KcbDangkyKcbCollection>();

                        NoitruPhanbuonggiuongCollection lstbuonggiuong=new Select().From(NoitruPhanbuonggiuong.Schema)
                            .Where(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(_item.IdBenhnhan)
                            .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(_item.MaLuotkham)
                            .ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();

                        KcbDonthuocCollection lstDonthuoc = new Select().From(KcbDonthuoc.Schema)
                            .Where(KcbDonthuoc.Columns.IdBenhnhan).IsEqualTo(_item.IdBenhnhan)
                            .And(KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(_item.MaLuotkham)
                            .ExecuteAsCollection<KcbDonthuocCollection>();
                        lstID = lstDonthuoc.Select(c => c.IdDonthuoc).ToList<long>();
                        KcbDonthuocChitietCollection lstDonthuocChitiet = new KcbDonthuocChitietCollection();
                        if (lstID.Count > 0)
                            lstDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                                 .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).In(lstID)
                                 .ExecuteAsCollection<KcbDonthuocChitietCollection>();

                        KcbChidinhclCollection lstChidinh = new Select().From(KcbChidinhcl.Schema)
                            .Where(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(_item.IdBenhnhan)
                            .And(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(_item.MaLuotkham)
                            .ExecuteAsCollection<KcbChidinhclCollection>();
                        lstID = lstChidinh.Select(c => c.IdChidinh).ToList<long>();

                        KcbChidinhclsChitietCollection lstChidinhChitiet = new KcbChidinhclsChitietCollection();
                        if (lstID.Count > 0)
                            lstChidinhChitiet = new Select().From(KcbChidinhclsChitiet.Schema)
                           .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(lstID)
                           .ExecuteAsCollection<KcbChidinhclsChitietCollection>();
                        if (lstDelete.Count > 0)
                            new Delete().From(KcbLichsuDoituongKcb.Schema).Where(KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb).In(lstDelete).Execute();
                        foreach (KcbLichsuDoituongKcb objLichsu in lstLichsu)
                        {

                            //Gán thông tin nội trú
                            if (objLichsu.TrangthaiNoitru > 0)
                            {
                                var q = from p in lstbuonggiuong
                                        where Utility.Int32Dbnull(Utility.GetYYYYMMDD(p.NgayVaokhoa), 0) >= Utility.Int32Dbnull(Utility.GetYYYYMMDD(objLichsu.NgayHieuluc), 0)
                                        select p;
                                if (q.Any())
                                {
                                    objLichsu.IdKhoanoitru = q.FirstOrDefault().IdKhoanoitru;
                                    objLichsu.IdBuong = q.FirstOrDefault().IdBuong;
                                    objLichsu.IdGiuong = q.FirstOrDefault().IdGiuong;
                                    objLichsu.IdRavien = q.FirstOrDefault().IdKhoanoitru;

                                }
                            }
                            objLichsu.Save();
                           _ActionResult= CapnhatChiphiKCB(objLichsu, lstDangkyKCB.ToList<KcbDangkyKcb>());
                            if (_ActionResult == ActionResult.Cancel)
                                return _ActionResult;
                            List<KcbChidinhcl> _chidinhCLS = (from p in lstChidinh
                                                              where Utility.Int32Dbnull(Utility.GetYYYYMMDD(p.NgayChidinh), 0) >= Utility.Int32Dbnull(Utility.GetYYYYMMDD(objLichsu.NgayHieuluc), 0)
                                                              && Utility.Int32Dbnull(Utility.GetYYYYMMDD(p.NgayChidinh), 0) <= Utility.Int32Dbnull(Utility.GetYYYYMMDD(objLichsu.NgayHethieuluc,new DateTime(2099,1,1)), 0)
                                                              select p).ToList<KcbChidinhcl>();
                            lstID = _chidinhCLS.Select(c => c.IdChidinh).ToList<long>();

                            List<KcbChidinhclsChitiet> _chidinhCLsChitiet = (from p in lstChidinhChitiet
                                                                             where lstID.Contains(p.IdChidinh)
                                                                             select p).ToList<KcbChidinhclsChitiet>();
                           
                          _ActionResult=  CapnhatChiphiCLS(objLichsu, _chidinhCLS, _chidinhCLsChitiet);
                            if (_ActionResult == ActionResult.Cancel)
                                return _ActionResult;
                            List<KcbDonthuoc> _donthuoc = (from p in lstDonthuoc
                                                           where Utility.Int32Dbnull(Utility.GetYYYYMMDD(p.NgayKedon), 0) >= Utility.Int32Dbnull(Utility.GetYYYYMMDD(objLichsu.NgayHieuluc), 0)
                                                             && Utility.Int32Dbnull(Utility.GetYYYYMMDD(p.NgayKedon), 0) <= Utility.Int32Dbnull(Utility.GetYYYYMMDD(objLichsu.NgayHethieuluc, new DateTime(2099, 1, 1)), 0)
                                                           select p).ToList<KcbDonthuoc>();
                            lstID = _donthuoc.Select(c => c.IdDonthuoc).ToList<long>();

                            List<KcbDonthuocChitiet> _donthuocChitiet = (from p in lstDonthuocChitiet
                                                                         where lstID.Contains(p.IdDonthuoc)
                                                                         select p).ToList<KcbDonthuocChitiet>();

                           _ActionResult= CapnhatChiphiThuoc(objLichsu, _donthuoc, _donthuocChitiet);
                            if (_ActionResult == ActionResult.Cancel)
                                return _ActionResult;
                            List<NoitruPhanbuonggiuong> _Bg = (from p in lstbuonggiuong
                                                                     where p.NgayVaokhoa >= objLichsu.NgayHieuluc && p.NgayVaokhoa <= objLichsu.NgayHethieuluc
                                                                     select p).ToList<NoitruPhanbuonggiuong>();
                          _ActionResult=  CapnhatBuonggiuong(objLichsu, _Bg);
                            if (_ActionResult == ActionResult.Cancel)
                                return _ActionResult;
                           
                        }
                        DateTime maxDate = lstLichsu.Max(c => c.NgayHieuluc);
                        KcbLichsuDoituongKcb objMax = lstLichsu.Where(c => c.NgayHieuluc == maxDate).FirstOrDefault();
                        if (objMax != null)
                        {
                            new Update(KcbLuotkham.Schema)
                                                      .Set(KcbLuotkham.Columns.MatheBhyt).EqualTo(objMax.MatheBhyt)
                                                      .Set(KcbLuotkham.Columns.MaNoicapBhyt).EqualTo(objMax.MaNoicapBhyt)
                                                      .Set(KcbLuotkham.Columns.MaQuyenloi).EqualTo(objMax.MaQuyenloi)
                                                      .Set(KcbLuotkham.Columns.NgaybatdauBhyt).EqualTo(objMax.NgaybatdauBhyt)
                                                      .Set(KcbLuotkham.Columns.NgayketthucBhyt).EqualTo(objMax.NgayketthucBhyt)
                                                      .Set(KcbLuotkham.Columns.NoicapBhyt).EqualTo(objMax.NoicapBhyt)
                                                      .Set(KcbLuotkham.Columns.IdDoituongKcb).EqualTo(objMax.IdDoituongKcb)
                                                      .Set(KcbLuotkham.Columns.MaKcbbd).EqualTo(objMax.MaKcbbd)
                                                      .Set(KcbLuotkham.Columns.NoiDongtrusoKcbbd).EqualTo(objMax.NoiDongtrusoKcbbd)
                                                      .Set(KcbLuotkham.Columns.MaDoituongBhyt).EqualTo(objMax.MaDoituongBhyt)
                                                      .Set(KcbLuotkham.Columns.DungTuyen).EqualTo(objMax.DungTuyen)
                                                      .Set(KcbLuotkham.Columns.MaDoituongKcb).EqualTo(objMax.MaDoituongKcb)
                                                      .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                                      .Set(KcbLuotkham.Columns.NgaySua).EqualTo(DateTime.Now)
                                                      .Set(KcbLuotkham.Columns.MadtuongSinhsong).EqualTo(objMax.MadtuongSinhsong)
                                                      .Set(KcbLuotkham.Columns.GiayBhyt).EqualTo(objMax.GiayBhyt)
                                                      .Set(KcbLuotkham.Columns.PtramBhyt).EqualTo(objMax.PtramBhyt)
                                                      .Set(KcbLuotkham.Columns.PtramBhytGoc).EqualTo(objMax.PtramBhytGoc)
                                                      .Set(KcbLuotkham.Columns.DiachiBhyt).EqualTo(objMax.DiachiBhyt)
                                                      .Set(KcbLuotkham.Columns.IdLichsuDoituongKcb).EqualTo(objMax.IdLichsuDoituongKcb)

                                                      .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objMax.MaLuotkham)
                                                      .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objMax.IdBenhnhan)
                                                      .Execute();

                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển đối tượng:\n" + ex.Message);
                return ActionResult.Exception;
            }
        }
        /// <summary>
        /// Nối thẻ
        /// </summary>
        /// <param name="objLuotkham"></param>
        /// <param name="objLuotkhamCu"></param>
        /// <param name="objCurrent"></param>
        /// <param name="PtramBHYTcu"></param>
        /// <returns></returns>
        public static ActionResult Noithe(KcbLuotkham objLuotkham, KcbLuotkham objLuotkhamCu, KcbLichsuDoituongKcb objCurrent)
        {
            try
            {
                long _IdLichsuDoituongKcb = -1;
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        if (objCurrent != null && !objCurrent.IsNew)//Cập nhật
                        {
                            objCurrent.Save();
                            _IdLichsuDoituongKcb = objCurrent.IdLichsuDoituongKcb;
                        }
                        else//Thêm thẻ mới
                        {
                            KcbLichsuDoituongKcb objLichsuKcb = KcbLichsuDoituongKcb.FetchByID(objLuotkham.IdLichsuDoituongKcb);// new KcbLichsuDoituongKcb();
                            if (objLichsuKcb != null)
                            {
                                objLichsuKcb.IsNew = false;
                                objLichsuKcb.MarkOld();
                                objLichsuKcb.IdBenhnhan = objLuotkham.IdBenhnhan;
                                objLichsuKcb.MaLuotkham = objLuotkham.MaLuotkham;
                                objLichsuKcb.NgayHieuluc = objLuotkham.NgayTiepdon;
                                objLichsuKcb.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                objLichsuKcb.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                                objLichsuKcb.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                                objLichsuKcb.MatheBhyt = objLuotkham.MatheBhyt;
                                objLichsuKcb.PtramBhyt = objLuotkham.PtramBhyt;
                                objLichsuKcb.PtramBhytGoc = objLuotkham.PtramBhytGoc;
                                objLichsuKcb.NgaybatdauBhyt = objLuotkham.NgaybatdauBhyt;
                                objLichsuKcb.NgayketthucBhyt = objLuotkham.NgayketthucBhyt;
                                objLichsuKcb.NoicapBhyt = objLuotkham.NoicapBhyt;
                                objLichsuKcb.MaNoicapBhyt = objLuotkham.MaNoicapBhyt;
                                objLichsuKcb.MaDoituongBhyt = objLuotkham.MaDoituongBhyt;
                                objLichsuKcb.MaQuyenloi = objLuotkham.MaQuyenloi;
                                objLichsuKcb.NoiDongtrusoKcbbd = objLuotkham.NoiDongtrusoKcbbd;
                                objLichsuKcb.MaKcbbd = objLuotkham.MaKcbbd;
                                objLichsuKcb.TrangthaiNoitru = objLuotkhamCu.TrangthaiNoitru;
                                objLichsuKcb.DungTuyen = objLuotkham.DungTuyen;
                                objLichsuKcb.Cmt = objLuotkham.Cmt;
                                objLichsuKcb.IdRavien = objLuotkhamCu.IdRavien;
                                objLichsuKcb.IdBuong = objLuotkhamCu.IdBuong;
                                objLichsuKcb.IdGiuong = objLuotkhamCu.IdGiuong;
                                objLichsuKcb.IdKhoanoitru = objLuotkhamCu.IdKhoanoitru;
                                objLichsuKcb.NguoiTao = globalVariables.UserName;
                                objLichsuKcb.NgayTao = DateTime.Now;
                                objLichsuKcb.IsNew = true;
                                objLichsuKcb.Save();
                                _IdLichsuDoituongKcb = objLichsuKcb.IdLichsuDoituongKcb;
                            }
                        }
                        //Cập nhật bảng lượt khám
                        new Update(KcbLuotkham.Schema)
                              .Set(KcbLuotkham.Columns.LuongCoban).EqualTo(objLuotkham.LuongCoban)
                              .Set(KcbLuotkham.Columns.TthaiChuyendi).EqualTo(objLuotkham.TthaiChuyendi)
                              .Set(KcbLuotkham.Columns.MatheBhyt).EqualTo(objLuotkham.MatheBhyt)
                              .Set(KcbLuotkham.Columns.MaNoicapBhyt).EqualTo(objLuotkham.MaNoicapBhyt)
                              .Set(KcbLuotkham.Columns.MaQuyenloi).EqualTo(objLuotkham.MaQuyenloi)
                              .Set(KcbLuotkham.Columns.NgaybatdauBhyt).EqualTo(objLuotkham.NgaybatdauBhyt)
                              .Set(KcbLuotkham.Columns.NgayketthucBhyt).EqualTo(objLuotkham.NgayketthucBhyt)
                              .Set(KcbLuotkham.Columns.NoicapBhyt).EqualTo(objLuotkham.NoicapBhyt)
                              .Set(KcbLuotkham.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                              .Set(KcbLuotkham.Columns.MaKcbbd).EqualTo(objLuotkham.MaKcbbd)
                              .Set(KcbLuotkham.Columns.NoiDongtrusoKcbbd).EqualTo(objLuotkham.NoiDongtrusoKcbbd)
                              .Set(KcbLuotkham.Columns.MaDoituongBhyt).EqualTo(objLuotkham.MaDoituongBhyt)
                              .Set(KcbLuotkham.Columns.DungTuyen).EqualTo(objLuotkham.DungTuyen)
                              .Set(KcbLuotkham.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                              .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                              .Set(KcbLuotkham.Columns.NgaySua).EqualTo(DateTime.Now)
                              .Set(KcbLuotkham.Columns.MadtuongSinhsong).EqualTo(objLuotkham.MadtuongSinhsong)
                              .Set(KcbLuotkham.Columns.GiayBhyt).EqualTo(objLuotkham.GiayBhyt)
                              .Set(KcbLuotkham.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                              .Set(KcbLuotkham.Columns.PtramBhytGoc).EqualTo(objLuotkham.PtramBhytGoc)
                              .Set(KcbLuotkham.Columns.DiachiBhyt).EqualTo(objLuotkham.DiachiBhyt)
                              .Set(KcbLuotkham.Columns.IdLoaidoituongKcb).EqualTo(objLuotkham.IdLoaidoituongKcb)
                              .Set(KcbLuotkham.Columns.IdLichsuDoituongKcb).EqualTo(_IdLichsuDoituongKcb)
                              .Set(KcbLuotkham.Columns.MaLydovaovien).EqualTo(objLuotkham.MaLydovaovien)
                              .Set(KcbLuotkham.Columns.TuyentruocDtTungay).EqualTo(objLuotkham.TuyentruocDtTungay)
                              .Set(KcbLuotkham.Columns.TuyentruocDtDenngay).EqualTo(objLuotkham.TuyentruocDtDenngay)
                              .Set(KcbLuotkham.Columns.NgayApdungBhyt).EqualTo(objLuotkham.NgayApdungBhyt)
                              .Set(KcbLuotkham.Columns.NgayDu5nam).EqualTo(objLuotkham.NgayDu5nam)
                               .Set(KcbLuotkham.Columns.IdBenhvienDen).EqualTo(objLuotkham.IdBenhvienDen)
                                .Set(KcbLuotkham.Columns.TthaiChuyenden).EqualTo(objLuotkham.TthaiChuyenden)
                                 .Set(KcbLuotkham.Columns.ChandoanChuyenden).EqualTo(objLuotkham.ChandoanChuyenden)
                              .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                              .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                              .Execute();
                        //Cập nhật dữ liệu đã tồn tại trong bảng công khám, cls, giường, thuốc theo giá trị ngày áp dụng thẻ. Để khi in phôi tự động tính lại chi phí bhyt,bn chi trả,bnttt theo mức hưởng từng thẻ
                        int num = SPs.BhytCapnhatIdThe(_IdLichsuDoituongKcb, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, objLuotkham.NgayApdungBhyt).Execute();
                    }
                    Scope.Complete();
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Cancel;
            }
            
            //if (Utility.Byte2Bool(objDangkyKCB.TrangthaiThanhtoan) || Utility.Int32Dbnull(objDangkyKCB.IdGoi, -1) > 0 || Utility.Byte2Bool(objDangkyKCB.TrongGoi) || objDangkyKCB.NgayTiepdon.Value >= objLuotkham.NgayApdungBhyt.Value)//Không xử lý các tình huống này
            //{
            //}
        }
        public static ActionResult Chuyendoituong(KcbLuotkham objLuotkham, KcbLuotkham objLuotkhamCu,KcbLichsuDoituongKcb objCurrent,  decimal PtramBHYTcu)
        {
            try
            {
            ActionResult _ActionResult = ActionResult.Success;
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        if (objCurrent != null && !objCurrent.IsNew)//Cập nhật
                        {
                            objCurrent.Save();
                            //Cập nhật bảng
                        }
                        else
                        {
                        }
                        long _IdLichsuDoituongKcb = Utility.Int64Dbnull(objLuotkhamCu.IdLichsuDoituongKcb, -1);
                        if (objLuotkham.MaDoituongKcb != objLuotkhamCu.MaDoituongKcb || THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                        {
                            KcbLichsuDoituongKcb objLichsuKcb = KcbLichsuDoituongKcb.FetchByID(objLuotkham.IdLichsuDoituongKcb);// new KcbLichsuDoituongKcb();
                            if (objLichsuKcb != null)
                            {
                                objLichsuKcb.IsNew = false;
                                objLichsuKcb.MarkOld();
                                objLichsuKcb.IdBenhnhan = objLuotkham.IdBenhnhan;
                                objLichsuKcb.MaLuotkham = objLuotkham.MaLuotkham;
                                objLichsuKcb.NgayHieuluc = objLuotkham.NgayTiepdon;
                                objLichsuKcb.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                objLichsuKcb.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                                objLichsuKcb.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                                objLichsuKcb.MatheBhyt = objLuotkham.MatheBhyt;
                                objLichsuKcb.PtramBhyt = objLuotkham.PtramBhyt;
                                objLichsuKcb.PtramBhytGoc = objLuotkham.PtramBhytGoc;
                                objLichsuKcb.NgaybatdauBhyt = objLuotkham.NgaybatdauBhyt;
                                objLichsuKcb.NgayketthucBhyt = objLuotkham.NgayketthucBhyt;
                                objLichsuKcb.NoicapBhyt = objLuotkham.NoicapBhyt;
                                objLichsuKcb.MaNoicapBhyt = objLuotkham.MaNoicapBhyt;
                                objLichsuKcb.MaDoituongBhyt = objLuotkham.MaDoituongBhyt;
                                objLichsuKcb.MaQuyenloi = objLuotkham.MaQuyenloi;
                                objLichsuKcb.NoiDongtrusoKcbbd = objLuotkham.NoiDongtrusoKcbbd;
                                objLichsuKcb.MaKcbbd = objLuotkham.MaKcbbd;
                                objLichsuKcb.TrangthaiNoitru = objLuotkhamCu.TrangthaiNoitru;
                                objLichsuKcb.DungTuyen = objLuotkham.DungTuyen;
                                objLichsuKcb.Cmt = objLuotkham.Cmt;
                                objLichsuKcb.IdRavien = objLuotkhamCu.IdRavien;
                                objLichsuKcb.IdBuong = objLuotkhamCu.IdBuong;
                                objLichsuKcb.IdGiuong = objLuotkhamCu.IdGiuong;
                                objLichsuKcb.IdKhoanoitru = objLuotkhamCu.IdKhoanoitru;
                                objLichsuKcb.NguoiTao = globalVariables.UserName;
                                objLichsuKcb.NgayTao = DateTime.Now;
                                objLichsuKcb.IsNew = true;
                                objLichsuKcb.Save();
                                _IdLichsuDoituongKcb = objLichsuKcb.IdLichsuDoituongKcb;
                            }
                        }
                        objLuotkham.IdLichsuDoituongKcb = _IdLichsuDoituongKcb;
                        _ActionResult = CapnhatGiatheodoituong(objLuotkham, objLuotkhamCu);
                        if (_ActionResult == ActionResult.Cancel)
                        {
                            return _ActionResult;
                        }

                        new Update(KcbLuotkham.Schema)
                           .Set(KcbLuotkham.Columns.LuongCoban).EqualTo(objLuotkham.LuongCoban)
                           .Set(KcbLuotkham.Columns.TthaiChuyendi).EqualTo(objLuotkham.TthaiChuyendi)
                           .Set(KcbLuotkham.Columns.MatheBhyt).EqualTo(objLuotkham.MatheBhyt)
                           .Set(KcbLuotkham.Columns.MaNoicapBhyt).EqualTo(objLuotkham.MaNoicapBhyt)
                           .Set(KcbLuotkham.Columns.MaQuyenloi).EqualTo(objLuotkham.MaQuyenloi)
                           .Set(KcbLuotkham.Columns.NgaybatdauBhyt).EqualTo(objLuotkham.NgaybatdauBhyt)
                           .Set(KcbLuotkham.Columns.NgayketthucBhyt).EqualTo(objLuotkham.NgayketthucBhyt)
                           .Set(KcbLuotkham.Columns.NoicapBhyt).EqualTo(objLuotkham.NoicapBhyt)
                           .Set(KcbLuotkham.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                           .Set(KcbLuotkham.Columns.MaKcbbd).EqualTo(objLuotkham.MaKcbbd)
                           .Set(KcbLuotkham.Columns.NoiDongtrusoKcbbd).EqualTo(objLuotkham.NoiDongtrusoKcbbd)
                           .Set(KcbLuotkham.Columns.MaDoituongBhyt).EqualTo(objLuotkham.MaDoituongBhyt)
                           .Set(KcbLuotkham.Columns.DungTuyen).EqualTo(objLuotkham.DungTuyen)
                           .Set(KcbLuotkham.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                           .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                           .Set(KcbLuotkham.Columns.NgaySua).EqualTo(DateTime.Now)
                           .Set(KcbLuotkham.Columns.MadtuongSinhsong).EqualTo(objLuotkham.MadtuongSinhsong)
                           .Set(KcbLuotkham.Columns.GiayBhyt).EqualTo(objLuotkham.GiayBhyt)
                           .Set(KcbLuotkham.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                           .Set(KcbLuotkham.Columns.PtramBhytGoc).EqualTo(objLuotkham.PtramBhytGoc)
                           .Set(KcbLuotkham.Columns.DiachiBhyt).EqualTo(objLuotkham.DiachiBhyt)
                           .Set(KcbLuotkham.Columns.IdLoaidoituongKcb).EqualTo(objLuotkham.IdLoaidoituongKcb)
                           .Set(KcbLuotkham.Columns.IdLichsuDoituongKcb).EqualTo(_IdLichsuDoituongKcb)
                           .Set(KcbLuotkham.Columns.MaLydovaovien).EqualTo(objLuotkham.MaLydovaovien)
                           .Set(KcbLuotkham.Columns.TuyentruocDtTungay).EqualTo(objLuotkham.TuyentruocDtTungay)
                           .Set(KcbLuotkham.Columns.TuyentruocDtDenngay).EqualTo(objLuotkham.TuyentruocDtDenngay)
                           .Set(KcbLuotkham.Columns.NgayApdungBhyt).EqualTo(objLuotkham.NgayApdungBhyt)
                           .Set(KcbLuotkham.Columns.NgayDu5nam).EqualTo(objLuotkham.NgayDu5nam)
                            .Set(KcbLuotkham.Columns.IdBenhvienDen).EqualTo(objLuotkham.IdBenhvienDen)
                             .Set(KcbLuotkham.Columns.TthaiChuyenden).EqualTo(objLuotkham.TthaiChuyenden)
                              .Set(KcbLuotkham.Columns.ChandoanChuyenden).EqualTo(objLuotkham.ChandoanChuyenden)
                           .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                           .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                           .Execute();


                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển đối tượng:\n"+ex.Message);
                return ActionResult.Exception;
            }
        }
        public static ActionResult CapnhatGiatheodoituong(KcbLuotkham objLuotkham, KcbLuotkham objLuotkhamCu)
        {
            using (var Scope = new TransactionScope())
            {
                ActionResult _ActionResult = ActionResult.Success;
                _ActionResult = CapnhatChiphiKCB(objLuotkham, objLuotkhamCu);
                if (_ActionResult == ActionResult.Cancel)
                    return _ActionResult;
                _ActionResult = CapnhatBuonggiuong(objLuotkham, objLuotkhamCu);
                if (_ActionResult == ActionResult.Cancel)
                    return _ActionResult;
                _ActionResult = CapnhatChiphiCLS(objLuotkham, objLuotkhamCu);
                if (_ActionResult == ActionResult.Cancel)
                    return _ActionResult;
                _ActionResult = CapnhatChiphiThuoc(objLuotkham, objLuotkhamCu);
                if (_ActionResult == ActionResult.Cancel)
                    return _ActionResult;
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        private static ActionResult CapnhatChiphiThuoc(KcbLichsuDoituongKcb objLichsu, List<KcbDonthuoc> lstDonthuoc, List<KcbDonthuocChitiet> lstChitiet)
        {
            using (var Scope = new TransactionScope())
            {

                decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
                THUOC_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_GIATHEO_KHOAKCB", "0", true) == "1";

                bool ApdunggiathuocDoituong = THU_VIEN_CHUNG.Laygiatrithamsohethong("APDUNG_GIATHUOC_DOITUONG", "0", true) == "1";

                DmucDoituongkcb _DmucDoituongkcb = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(objLichsu.MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                if (_DmucDoituongkcb == null) return ActionResult.Success;
                //Kiểm tra nếu đối tượng ngoại trú đã có đơn thuốc thanh toán-->Ko cho phép chuyển nữa
                if (Utility.ByteDbnull(objLichsu.TrangthaiNoitru,0)<=0 && lstChitiet.Where(c => c.TrangthaiThanhtoan > 0).Any())
                {
                    Scope.Complete();
                    return ActionResult.Cancel;
                }
                bool saveParent = false;
                foreach (KcbDonthuoc objKcbDonthuoc in lstDonthuoc)
                {

                    objKcbDonthuoc.IdLichsuDoituongKcb = objLichsu.IdLichsuDoituongKcb;
                    objKcbDonthuoc.MatheBhyt = objLichsu.MatheBhyt;
                    objKcbDonthuoc.MaDoituongKcb = objLichsu.MaDoituongKcb;

                    string MA_KHOA_THIEN = globalVariables.MA_KHOA_THIEN;
                    if (Utility.Int32Dbnull(objKcbDonthuoc.Noitru, 0) <= 0)
                    {
                        MA_KHOA_THIEN = objKcbDonthuoc.MaKhoaThuchien;
                    }
                    else
                    {
                        MA_KHOA_THIEN = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_GIACLS", false) ?? MA_KHOA_THIEN;
                    }

                    foreach (KcbDonthuocChitiet objChitietDonthuoc in lstChitiet.Where(c => c.IdDonthuoc.Equals(objKcbDonthuoc.IdDonthuoc)))
                    {
                        if (Utility.Int32Dbnull(objKcbDonthuoc.IdGoi, -1) > 0)
                        {
                            objChitietDonthuoc.MaDoituongKcb = objLichsu.MaDoituongKcb;
                            if (Utility.Int16Dbnull(objChitietDonthuoc.TrangthaiThanhtoan, 0) == 0)
                            {
                                saveParent = true;
                                objChitietDonthuoc.MadoituongGia = objLichsu.MaDoituongKcb;
                                objChitietDonthuoc.PtramBhyt = objLichsu.PtramBhyt;
                                objChitietDonthuoc.PtramBhytGoc = objLichsu.PtramBhytGoc;
                                DmucThuoc _DmucThuoc = DmucThuoc.FetchByID(objChitietDonthuoc.IdThuoc);
                                if (ApdunggiathuocDoituong || Utility.Byte2Bool(_DmucDoituongkcb.GiathuocQuanhe.Value))//Giá theo bảng quan hệ-->
                                {
                                    QheDoituongThuoc _item = THU_VIEN_CHUNG.LayQheDoituongThuoc(objLichsu.MaDoituongKcb,
                                                                              objChitietDonthuoc.IdThuoc,
                                                                              MA_KHOA_THIEN, THUOC_GIATHEO_KHOAKCB);
                                    if (_item != null)//Tìm thấy quan hệ giá
                                    {
                                        objChitietDonthuoc.DonGia = Utility.DecimaltoDbnull(_item.DonGia);
                                        objChitietDonthuoc.PhuThu = (Utility.isTrue(objLichsu.DungTuyen.Value) ? Utility.DecimaltoDbnull(_item.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_item.PhuthuTraituyen));
                                        objChitietDonthuoc.TuTuc = 0;
                                    }
                                    else//Tìm giá dịch vụ, chỉ xảy ra khi đối tượng BHYT ko tìm thấy giá, 
                                    //còn đối tượng DV chắc chắn do thiếu quan hệ giá nên ko thay đổi gì dòng giá thuốc này
                                    {
                                        _item = THU_VIEN_CHUNG.LayQheDoituongThuoc("DV",
                                                                               objChitietDonthuoc.IdThuoc,
                                                                               MA_KHOA_THIEN, THUOC_GIATHEO_KHOAKCB);
                                        if (_item != null)
                                        {
                                            objChitietDonthuoc.MadoituongGia = "DV";
                                            objChitietDonthuoc.DonGia = Utility.DecimaltoDbnull(_item.DonGia);
                                            objChitietDonthuoc.PhuThu = (Utility.isTrue(objLichsu.DungTuyen.Value) ? Utility.DecimaltoDbnull(_item.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_item.PhuthuTraituyen));
                                            objChitietDonthuoc.TuTuc = 1;
                                            objChitietDonthuoc.PtramBhyt = 0;
                                        }
                                    }
                                }
                                else //Giá theo kho-->
                                {
                                    TThuockho objTK = TThuockho.FetchByID(objChitietDonthuoc.IdThuockho);
                                    if (objTK != null)
                                    {
                                        objChitietDonthuoc.DonGia = THU_VIEN_CHUNG.IsBaoHiem(objLichsu.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(objTK.GiaBhyt.Value, objChitietDonthuoc.DonGia) : objTK.GiaBan;
                                        objChitietDonthuoc.PhuThu = (Utility.Byte2Bool(objLichsu.DungTuyen) ? Utility.DecimaltoDbnull(objTK.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objTK.PhuthuTraituyen));
                                        if (!THU_VIEN_CHUNG.IsBaoHiem(objLichsu.IdLoaidoituongKcb))//Đối tượng dịch vụ-->Ko tính phụ thu
                                            objChitietDonthuoc.PhuThu = 0;
                                    }
                                }
                                //Tính lại các mục tự túc, BHYT chi trả, BN chi trả
                                if (_DmucThuoc != null && Utility.Int32Dbnull(_DmucThuoc.TuTuc, 0) == 1)
                                    objChitietDonthuoc.TuTuc = 1;//Ke ca co trong bang quan he
                                if (objLichsu.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                    objChitietDonthuoc.TuTuc = 0;
                                if (!Utility.Byte2Bool(objChitietDonthuoc.TuTuc))//Nếu ko phải dịch vụ tự túc
                                {
                                    decimal BHCT = 0m;
                                    if (objLichsu.DungTuyen == 1)//Chỉ xảy ra với đối tượng BHYT
                                    {
                                        BHCT = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhyt, 0) / 100);
                                    }
                                    else//Đối tượng DV hoặc BHYT trái tuyến
                                    {
                                        if (objLichsu.TrangthaiNoitru <= 0 || !Utility.Byte2Bool(objKcbDonthuoc.Noitru))//Ngoại trú
                                            BHCT = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhyt, 0) / 100);
                                        else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                            BHCT = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                    }
                                    decimal _BnhanChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) - BHCT;
                                    objChitietDonthuoc.BhytChitra = BHCT;
                                    objChitietDonthuoc.BnhanChitra = _BnhanChitra;
                                }
                                else//Là tự túc
                                {
                                    objChitietDonthuoc.PtramBhyt = 0;
                                    objChitietDonthuoc.BhytChitra = 0;
                                    objChitietDonthuoc.BnhanChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0);
                                }
                            }
                            else
                            {
                                //nếu đã thanh toán thì chỉ xử lý khi đối tượng đang ở trạng thái nội trú.
                                if (Utility.ByteDbnull(objLichsu.TrangthaiNoitru,0)>0)
                                {
                                    saveParent = true;
                                    if (THU_VIEN_CHUNG.IsBaoHiem(objLichsu.IdLoaidoituongKcb))
                                    {
                                        //Đối tượng mới là BHYT thì kiểm tra đổi các dịch vụ có giá DV về tự túc. 
                                        //Các dịch vụ có giá khác DV thì để nguyên(để nếu đối tượng cũ là BHYT thì ko bị thay đổi giá trị tự túc)
                                        if (objChitietDonthuoc.MadoituongGia == "DV")
                                            objChitietDonthuoc.TuTuc = 1;
                                    }
                                    else//Nếu đối tượng từ BHYT chuyển sang dịch vụ thì chuyển thành ko tự túc hết
                                    {
                                        if (objLichsu.MaDoituongKcb != objLichsu.MaDoituongKcb)//Mới=DV, Cũ=BHYT. Còn lại ko làm gì cả
                                            objChitietDonthuoc.TuTuc = 0;
                                    }
                                }
                                else
                                    continue;
                            }
                            if (objLichsu.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                            {
                                objChitietDonthuoc.TuTuc = 0;
                                objChitietDonthuoc.PhuThu = 0;
                            }
                            objChitietDonthuoc.Save();
                        }
                    }
                    if (saveParent)
                    {
                        objKcbDonthuoc.Save();
                        saveParent = false;
                    }

                }
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        private static ActionResult CapnhatChiphiThuoc(KcbLuotkham objLuotkham, KcbLuotkham objLuotkhamCu)
        {
            using (var Scope = new TransactionScope())
            {
                decimal BHYT_PTRAM_TRAITUYENNOITRU =Utility.DecimaltoDbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false),0m);
                THUOC_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_GIATHEO_KHOAKCB", "0", true) == "1";

                KcbDonthuocCollection lstDonthuoc=
                    new Select()
                    .From(KcbDonthuoc.Schema)
                    .Where(KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbDonthuoc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .ExecuteAsCollection<KcbDonthuocCollection>();
                List<long> lstID = lstDonthuoc.Select(c => c.IdDonthuoc).Distinct().ToList<long>();
                if (lstID.Count <= 0)
                {
                    Scope.Complete();
                    return ActionResult.Success;
                }

                KcbDonthuocChitietCollection lstChitiet =
                    new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).In(lstID)
                    .ExecuteAsCollection<KcbDonthuocChitietCollection>();

                bool ApdunggiathuocDoituong = THU_VIEN_CHUNG.Laygiatrithamsohethong("APDUNG_GIATHUOC_DOITUONG", "0", true) == "1";

                DmucDoituongkcb _DmucDoituongkcb = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                if (_DmucDoituongkcb == null) return ActionResult.Success;
                //Kiểm tra nếu đối tượng ngoại trú đã có đơn thuốc thanh toán-->Ko cho phép chuyển nữa
                if (Utility.ByteDbnull(objLuotkham.TrangthaiNoitru,0)<=0 && lstChitiet.Where(c => c.TrangthaiThanhtoan > 0).Any())
                {
                    Scope.Complete();
                    return ActionResult.Cancel;
                }
                bool saveParent = false;
                foreach (KcbDonthuoc objKcbDonthuoc in lstDonthuoc)
                {

                    objKcbDonthuoc.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
                    objKcbDonthuoc.MatheBhyt = objLuotkham.MatheBhyt;
                    objKcbDonthuoc.MaDoituongKcb = objLuotkham.MaDoituongKcb;

                    foreach (KcbDonthuocChitiet objChitietDonthuoc in lstChitiet.Where(c => c.IdDonthuoc.Equals(objKcbDonthuoc.IdDonthuoc)))
                    {
                        if (Utility.Int32Dbnull(objChitietDonthuoc.IdGoi, -1) > 0)
                        {
                            objChitietDonthuoc.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                            if (Utility.Int16Dbnull(objChitietDonthuoc.TrangthaiThanhtoan, 0) == 0)
                            {
                                saveParent = true;
                                objChitietDonthuoc.MadoituongGia = objLuotkham.MaDoituongKcb;
                                objChitietDonthuoc.PtramBhyt = objLuotkham.PtramBhyt;
                                objChitietDonthuoc.PtramBhytGoc = objLuotkham.PtramBhytGoc;
                                DmucThuoc _DmucThuoc = DmucThuoc.FetchByID(objChitietDonthuoc.IdThuoc);
                                if (ApdunggiathuocDoituong || Utility.Byte2Bool(_DmucDoituongkcb.GiathuocQuanhe.Value))//Giá theo bảng quan hệ-->
                                {
                                    QheDoituongThuoc _item = THU_VIEN_CHUNG.LayQheDoituongThuoc(objLuotkham.MaDoituongKcb,
                                                                              objChitietDonthuoc.IdThuoc,
                                                                              objLuotkham.MaKhoaThuchien, THUOC_GIATHEO_KHOAKCB);
                                    if (_item != null)//Tìm thấy quan hệ giá
                                    {
                                        objChitietDonthuoc.DonGia = Utility.DecimaltoDbnull(_item.DonGia);
                                        objChitietDonthuoc.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen.Value) ? Utility.DecimaltoDbnull(_item.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_item.PhuthuTraituyen));
                                        objChitietDonthuoc.TuTuc = 0;
                                    }
                                    else//Tìm giá dịch vụ, chỉ xảy ra khi đối tượng BHYT ko tìm thấy giá, 
                                    //còn đối tượng DV chắc chắn do thiếu quan hệ giá nên ko thay đổi gì dòng giá thuốc này
                                    {
                                        _item = THU_VIEN_CHUNG.LayQheDoituongThuoc("DV",
                                                                               objChitietDonthuoc.IdThuoc,
                                                                               objLuotkham.MaKhoaThuchien, THUOC_GIATHEO_KHOAKCB);
                                        if (_item != null)
                                        {
                                            objChitietDonthuoc.MadoituongGia = "DV";
                                            objChitietDonthuoc.DonGia = Utility.DecimaltoDbnull(_item.DonGia);
                                            objChitietDonthuoc.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen.Value) ? Utility.DecimaltoDbnull(_item.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_item.PhuthuTraituyen));
                                            objChitietDonthuoc.TuTuc = 1;
                                            objChitietDonthuoc.PtramBhyt = 0;
                                        }
                                    }
                                }
                                else //Giá theo kho-->
                                {
                                    TThuockho objTK = TThuockho.FetchByID(objChitietDonthuoc.IdThuockho);
                                    if (objTK != null)
                                    {
                                        objChitietDonthuoc.DonGia = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(objTK.GiaBhyt.Value, objChitietDonthuoc.DonGia) : objTK.GiaBan;
                                        objChitietDonthuoc.PhuThu = (Utility.Byte2Bool(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(objTK.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objTK.PhuthuTraituyen));
                                        if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))//Đối tượng dịch vụ-->Ko tính phụ thu
                                            objChitietDonthuoc.PhuThu = 0;
                                    }
                                }
                                //Tính lại các mục tự túc, BHYT chi trả, BN chi trả
                                if (_DmucThuoc != null && Utility.Int32Dbnull(_DmucThuoc.TuTuc, 0) == 1)
                                    objChitietDonthuoc.TuTuc = 1;//Ke ca co trong bang quan he
                                if (objLuotkham.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                {
                                    objChitietDonthuoc.TuTuc = 0;
                                    objChitietDonthuoc.PhuThu = 0;
                                }
                                if (!Utility.Byte2Bool(objChitietDonthuoc.TuTuc))//Nếu ko phải dịch vụ tự túc
                                {
                                    decimal BHCT = 0m;
                                    if (objLuotkham.DungTuyen == 1)//Chỉ xảy ra với đối tượng BHYT
                                    {
                                        BHCT = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                    }
                                    else//Đối tượng DV hoặc BHYT trái tuyến
                                    {
                                        if (objLuotkham.TrangthaiNoitru <= 0 || !Utility.Byte2Bool(objKcbDonthuoc.Noitru))//Ngoại trú
                                            BHCT = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                        else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                            BHCT = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                                    }
                                    decimal _BnhanChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) - BHCT;
                                    objChitietDonthuoc.BhytChitra = BHCT;
                                    objChitietDonthuoc.BnhanChitra = _BnhanChitra;
                                }
                                else//Là tự túc
                                {
                                    objChitietDonthuoc.PtramBhyt = 0;
                                    objChitietDonthuoc.BhytChitra = 0;
                                    objChitietDonthuoc.PhuThu = 0;
                                    objChitietDonthuoc.BnhanChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0);
                                }
                            }
                            else
                            {
                                //nếu đã thanh toán thì chỉ xử lý khi đối tượng đang ở trạng thái nội trú.
                                if (Utility.ByteDbnull(objLuotkham.TrangthaiNoitru,0) > 0)
                                {
                                    saveParent = true;
                                    if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                                    {
                                        //Đối tượng mới là BHYT thì kiểm tra đổi các dịch vụ có giá DV về tự túc. 
                                        //Các dịch vụ có giá khác DV thì để nguyên(để nếu đối tượng cũ là BHYT thì ko bị thay đổi giá trị tự túc)
                                        if (objChitietDonthuoc.MadoituongGia == "DV")
                                            objChitietDonthuoc.TuTuc = 1;
                                    }
                                    else//Nếu đối tượng từ BHYT chuyển sang dịch vụ thì chuyển thành ko tự túc hết
                                    {
                                        if (objLuotkham.MaDoituongKcb != objLuotkhamCu.MaDoituongKcb)//Mới=DV, Cũ=BHYT. Còn lại ko làm gì cả
                                            objChitietDonthuoc.TuTuc = 0;
                                    }
                                }
                                else
                                    continue;
                            }
                            objChitietDonthuoc.Save();
                        }
                    }

                    if (saveParent)
                    {
                        objKcbDonthuoc.Save();
                        saveParent = false;
                    }

                }
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        static bool CLS_GIATHEO_KHOAKCB = false;
        static bool THUOC_GIATHEO_KHOAKCB = false;
        private static ActionResult CapnhatChiphiCLS_old(KcbLuotkham objLuotkham, KcbLuotkham objLuotkhamCu)
        {
            using (var Scope = new TransactionScope())
            {
                CLS_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_GIATHEO_KHOAKCB","0", true) == "1";
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(
                        new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema)
                        .Where(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        );
                var objChidinhChitietCollection =
                    sqlQuery.ExecuteAsCollection<KcbChidinhclsChitietCollection>();
                foreach (KcbChidinhclsChitiet objChidinhChitiet in objChidinhChitietCollection)
                {
                    if (objChidinhChitiet.TrangthaiThanhtoan == 0)
                    {
                        QheDoituongDichvucl _Items = THU_VIEN_CHUNG.LayQheDoituongCLS(objLuotkham.MaDoituongKcb, objChidinhChitiet.IdChitietdichvu, objLuotkham.MaKhoaThuchien, CLS_GIATHEO_KHOAKCB);
                        if (_Items != null)
                        {

                            objChidinhChitiet.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                            objChidinhChitiet.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                            objChidinhChitiet.TuTuc = 0;
                            objChidinhChitiet.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                            objChidinhChitiet.PtramBhyt = objLuotkham.PtramBhyt;
                        }
                        else
                        {
                            _Items = THU_VIEN_CHUNG.LayQheDoituongCLS("DV", objChidinhChitiet.IdChitietdichvu, objLuotkham.MaKhoaThuchien, CLS_GIATHEO_KHOAKCB);
                            if (_Items != null)
                            {
                                objChidinhChitiet.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                                objChidinhChitiet.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                                objChidinhChitiet.TuTuc = 1;
                                objChidinhChitiet.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                objChidinhChitiet.PtramBhyt = 0;
                            }
                        }
                        DmucDichvuclsChitiet _DmucDichvuclsChitiet = DmucDichvuclsChitiet.FetchByID(objChidinhChitiet.IdChitietdichvu);
                        if (_DmucDichvuclsChitiet != null && Utility.Int32Dbnull(_DmucDichvuclsChitiet.TuTuc, 0) == 1)
                            objChidinhChitiet.TuTuc = 1;//Ke ca co trong bang quan he
                        if (objLuotkham.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                        {
                            objChidinhChitiet.TuTuc = 0;
                            objChidinhChitiet.PhuThu = 0;
                        }

                        objChidinhChitiet.NguoiSua = globalVariables.UserName;
                        objChidinhChitiet.NgaySua = DateTime.Now;
                        decimal PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                        TinhCLS.GB_TinhPhtramBHYT(objChidinhChitiet, objLuotkham,false, PtramBHYT);
                    }
                    else
                    {
                        return ActionResult.Cancel;
                    }
                }
                objChidinhChitietCollection.SaveAll();
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        
        /// <summary>
        /// Lấy toàn bộ dữ liệu CLS giống phần chỉ định CLS sau đó tính giá dựa trên dữ liệu lấy được đó. Thay vì select lại từ các bảng quan hệ
        /// </summary>
        /// <param name="objLuotkham"></param>
        /// <returns></returns>
        private static ActionResult CapnhatChiphiCLS( KcbLichsuDoituongKcb _item,List< KcbChidinhcl> lstChidinh,List< KcbChidinhclsChitiet> lstChitiet)
        {
            using (var Scope = new TransactionScope())
            {
                if (lstChitiet.Count > 0)
                {
                   
                    //Kiểm tra nếu đối tượng ngoại trú đã có dịch vụ thanh toán-->Ko cho phép chuyển nữa
                    if (Utility.ByteDbnull(_item.TrangthaiNoitru,0)<=0 && lstChitiet.Where(c => c.TrangthaiThanhtoan > 0).Any())
                    {
                        {
                            Scope.Complete();
                            return ActionResult.Cancel;
                        }
                    }
                    bool saveParent = false;
                    foreach (KcbChidinhcl objChidinh in lstChidinh)
                    {
                        string MA_KHOA_THIEN = globalVariables.MA_KHOA_THIEN;
                        if (Utility.Int32Dbnull(objChidinh.Noitru, 0) <= 0)
                        {
                            MA_KHOA_THIEN = objChidinh.MaKhoaChidinh;
                        }
                        else
                        {
                            MA_KHOA_THIEN = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_GIACLS", false) ?? MA_KHOA_THIEN;
                        }
                        DataTable m_dtServiceDetail = new KCB_CHIDINH_CANLAMSANG().LaydanhsachCLS_chidinh(_item.MaDoituongKcb,0, Utility.ByteDbnull(_item.TrangthaiNoitru, 0), Utility.ByteDbnull(_item.GiayBhyt, 0), -1, Utility.Int32Dbnull(_item.DungTuyen.Value, 0), MA_KHOA_THIEN, "-GOI,-TIEN", 0, objChidinh.NgayChidinh);

                        objChidinh.IdLichsuDoituongKcb = _item.IdLichsuDoituongKcb;
                        objChidinh.MatheBhyt = _item.MatheBhyt;
                        objChidinh.MaDoituongKcb = _item.MaDoituongKcb;
                        objChidinh.IdLoaidoituongKcb = _item.IdLoaidoituongKcb;
                        objChidinh.IdDoituongKcb = _item.IdDoituongKcb;
                        foreach (KcbChidinhclsChitiet objChidinhChitiet in lstChitiet.Where(c => c.IdChidinh.Equals(objChidinh.IdChidinh)))
                        {
                            if (Utility.Int32Dbnull(objChidinhChitiet.IdGoi, -1) <=0)
                            {
                                objChidinhChitiet.IdDoituongKcb = _item.IdDoituongKcb;
                                if (objChidinhChitiet.TrangthaiThanhtoan == 0)//Chưa thanh toán
                                {
                                    saveParent = true;
                                    objChidinhChitiet.MadoituongGia = _item.MaDoituongKcb;
                                    objChidinhChitiet.PtramBhyt = _item.PtramBhyt;
                                    objChidinhChitiet.PtramBhytGoc = _item.PtramBhytGoc;
                                    DataRow[] arrDr = m_dtServiceDetail.Select(KcbChidinhclsChitiet.Columns.IdChitietdichvu + "=" + Utility.Int32Dbnull(objChidinhChitiet.IdChitietdichvu, -1));
                                    if (arrDr.Length > 0)
                                    {

                                        objChidinhChitiet.PtramBhyt = _item.PtramBhyt;
                                        objChidinhChitiet.DonGia = Utility.DecimaltoDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.DonGia], 0);
                                        objChidinhChitiet.GiaDanhmuc = Utility.DecimaltoDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.DonGia], 0);
                                        objChidinhChitiet.TuTuc = Utility.ByteDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.TuTuc], 0);
                                        objChidinhChitiet.PhuThu = Utility.DecimaltoDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.PhuThu], 0);
                                        objChidinhChitiet.NguoiSua = globalVariables.UserName;
                                        objChidinhChitiet.NgaySua = DateTime.Now;
                                        TinhCLS.GB_TinhPhtramBHYT(objChidinhChitiet, _item, Utility.Byte2Bool(objChidinh.Noitru), Utility.DecimaltoDbnull(_item.PtramBhyt));

                                    }
                                    else//Rất khó nhảy vào nhánh này trừ phi lỗi dữ liệu đặc biệt nào đó
                                    {
                                        CLS_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_GIATHEO_KHOAKCB", "0", true) == "1";
                                        QheDoituongDichvucl _Items = THU_VIEN_CHUNG.LayQheDoituongCLS(_item.MaDoituongKcb, objChidinhChitiet.IdChitietdichvu, MA_KHOA_THIEN, CLS_GIATHEO_KHOAKCB);
                                        if (_Items != null)
                                        {
                                            objChidinhChitiet.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                                            objChidinhChitiet.PhuThu = (Utility.isTrue(_item.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                                            objChidinhChitiet.TuTuc = 0;
                                            objChidinhChitiet.IdDoituongKcb = _item.IdDoituongKcb;
                                            objChidinhChitiet.PtramBhyt = _item.PtramBhyt;
                                        }
                                        else
                                        {
                                            _Items = THU_VIEN_CHUNG.LayQheDoituongCLS("DV", objChidinhChitiet.IdChitietdichvu, MA_KHOA_THIEN, CLS_GIATHEO_KHOAKCB);
                                            if (_Items != null)
                                            {
                                                objChidinhChitiet.MadoituongGia = "DV";
                                                objChidinhChitiet.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                                                objChidinhChitiet.PhuThu = (Utility.isTrue(_item.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                                                objChidinhChitiet.TuTuc = 1;
                                                objChidinhChitiet.IdDoituongKcb = _item.IdDoituongKcb;
                                                objChidinhChitiet.PtramBhyt = 0;
                                            }
                                        }
                                        DmucDichvuclsChitiet _DmucDichvuclsChitiet = DmucDichvuclsChitiet.FetchByID(objChidinhChitiet.IdChitietdichvu);
                                        if (_DmucDichvuclsChitiet != null && Utility.Int32Dbnull(_DmucDichvuclsChitiet.TuTuc, 0) == 1)
                                            objChidinhChitiet.TuTuc = 1;//Ke ca co trong bang quan he
                                        if (_item.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                        {
                                            objChidinhChitiet.TuTuc = 0;
                                            objChidinhChitiet.PhuThu = 0;
                                        }
                                        objChidinhChitiet.NguoiSua = globalVariables.UserName;
                                        objChidinhChitiet.NgaySua = DateTime.Now;
                                        decimal PtramBHYT = Utility.DecimaltoDbnull(_item.PtramBhyt);
                                        TinhCLS.GB_TinhPhtramBHYT(objChidinhChitiet, _item, Utility.Byte2Bool(objChidinh.Noitru), PtramBHYT);
                                    }
                                }
                                else
                                {
                                    //nếu đã thanh toán thì chỉ xử lý khi đối tượng đang ở trạng thái nội trú.
                                    if (Utility.ByteDbnull(_item.TrangthaiNoitru,0)>0)
                                    {
                                        saveParent = true;
                                        if (THU_VIEN_CHUNG.IsBaoHiem(_item.IdLoaidoituongKcb))
                                        {
                                            //Đối tượng mới là BHYT thì kiểm tra đổi các dịch vụ có giá DV về tự túc. 
                                            //Các dịch vụ có giá khác DV thì để nguyên(để nếu đối tượng cũ là BHYT thì ko bị thay đổi giá trị tự túc)
                                            if (objChidinhChitiet.MadoituongGia == "DV")
                                                objChidinhChitiet.TuTuc = 1;
                                        }
                                        else//Nếu đối tượng từ BHYT chuyển sang dịch vụ thì chuyển thành ko tự túc hết
                                        {
                                            if (_item.MaDoituongKcb != _item.MaDoituongKcb)//Mới=DV, Cũ=BHYT. Còn lại ko làm gì cả
                                                objChidinhChitiet.TuTuc = 0;
                                        }
                                    }
                                    else
                                        continue;
                                }
                                if (_item.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                {
                                    objChidinhChitiet.TuTuc = 0;
                                    objChidinhChitiet.PhuThu = 0;
                                }
                                objChidinhChitiet.Save();
                                saveParent = true;
                            }
                        }
                        if (saveParent)
                        {
                            objChidinh.Save();
                            saveParent = false;
                        }
                    }
                }
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        /// <summary>
        /// Lấy toàn bộ dữ liệu CLS giống phần chỉ định CLS sau đó tính giá dựa trên dữ liệu lấy được đó. Thay vì select lại từ các bảng quan hệ
        /// </summary>
        /// <param name="objLuotkham"></param>
        /// <returns></returns>
        private static ActionResult CapnhatChiphiCLS(KcbLuotkham objLuotkham, KcbLuotkham objLuotkhamCu)
        {
            using (var Scope = new TransactionScope())
            {
               
                
                KcbChidinhclCollection lstChidinh= 
                    new Select()
                    .From(KcbChidinhcl.Schema)
                    .Where(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .ExecuteAsCollection<KcbChidinhclCollection>();
                List<long> lstID = lstChidinh.Select(c => c.IdChidinh).Distinct().ToList<long>();
                if (lstID.Count <= 0)
                {
                    Scope.Complete();
                    return ActionResult.Success;
                }
                KcbChidinhclsChitietCollection lstChitiet = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(lstID)
                .ExecuteAsCollection<KcbChidinhclsChitietCollection>();
                if (lstChitiet.Count > 0)
                {
                    //Kiểm tra nếu đối tượng ngoại trú đã có dịch vụ thanh toán-->Ko cho phép chuyển nữa
                    if (Utility.ByteDbnull(objLuotkham.TrangthaiNoitru,0)<=0 && lstChitiet.Where(c => c.TrangthaiThanhtoan > 0).Any())
                    {
                        Scope.Complete();
                        return ActionResult.Cancel;
                    }
                    bool saveParent = false;
                    foreach (KcbChidinhcl objChidinh in lstChidinh)
                    {
                        DataTable m_dtServiceDetail = new KCB_CHIDINH_CANLAMSANG().LaydanhsachCLS_chidinh(objLuotkham.MaDoituongKcb,0, objLuotkham.TrangthaiNoitru, Utility.ByteDbnull(objLuotkham.GiayBhyt, 0), -1, Utility.Int32Dbnull(objLuotkham.DungTuyen.Value, 0), objLuotkham.MaKhoaThuchien, "-GOI,-TIEN", 0, objChidinh.NgayChidinh);//Ko lấy dữ liệu liên quan đến gói dịch vụ+tiền phí phụ thêm
                            objChidinh.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
                            objChidinh.MatheBhyt = objLuotkham.MatheBhyt;
                            objChidinh.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                            objChidinh.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                            objChidinh.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                            foreach (KcbChidinhclsChitiet objChidinhChitiet in lstChitiet.Where(c => c.IdChidinh.Equals(objChidinh.IdChidinh)))
                            {
                                if (Utility.Int32Dbnull(objChidinhChitiet.IdGoi, -1) <=0)
                                {
                                    objChidinhChitiet.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                    if (objChidinhChitiet.TrangthaiThanhtoan == 0)//Chưa thanh toán
                                    {
                                        saveParent = true;
                                        objChidinhChitiet.MadoituongGia = objLuotkham.MaDoituongKcb;
                                        objChidinhChitiet.PtramBhyt = objLuotkham.PtramBhyt;
                                        objChidinhChitiet.PtramBhytGoc = objLuotkham.PtramBhytGoc;
                                        DataRow[] arrDr = m_dtServiceDetail.Select(KcbChidinhclsChitiet.Columns.IdChitietdichvu + "=" + Utility.Int32Dbnull(objChidinhChitiet.IdChitietdichvu, -1));
                                        if (arrDr.Length > 0)
                                        {

                                            objChidinhChitiet.PtramBhyt = objLuotkham.PtramBhyt;
                                            objChidinhChitiet.DonGia = Utility.DecimaltoDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.DonGia], 0);
                                            objChidinhChitiet.GiaDanhmuc = Utility.DecimaltoDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.DonGia], 0);
                                            objChidinhChitiet.TuTuc = Utility.ByteDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.TuTuc], 0);
                                            objChidinhChitiet.PhuThu = Utility.DecimaltoDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.PhuThu], 0);
                                            objChidinhChitiet.NguoiSua = globalVariables.UserName;
                                            objChidinhChitiet.NgaySua = DateTime.Now;
                                            TinhCLS.GB_TinhPhtramBHYT(objChidinhChitiet, objLuotkham, Utility.Byte2Bool(objChidinh.Noitru), Utility.DecimaltoDbnull(objLuotkham.PtramBhyt));

                                        }
                                        else//Rất khó nhảy vào nhánh này trừ phi lỗi dữ liệu đặc biệt nào đó
                                        {
                                            CLS_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_GIATHEO_KHOAKCB", "0", true) == "1";
                                            QheDoituongDichvucl _Items = THU_VIEN_CHUNG.LayQheDoituongCLS(objLuotkham.MaDoituongKcb, objChidinhChitiet.IdChitietdichvu, objLuotkham.MaKhoaThuchien, CLS_GIATHEO_KHOAKCB);
                                            if (_Items != null)
                                            {
                                                objChidinhChitiet.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                                                objChidinhChitiet.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                                                objChidinhChitiet.TuTuc = 0;
                                                objChidinhChitiet.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                                objChidinhChitiet.PtramBhyt = objLuotkham.PtramBhyt;
                                            }
                                            else
                                            {
                                                _Items = THU_VIEN_CHUNG.LayQheDoituongCLS("DV", objChidinhChitiet.IdChitietdichvu, objLuotkham.MaKhoaThuchien, CLS_GIATHEO_KHOAKCB);
                                                if (_Items != null)
                                                {
                                                    objChidinhChitiet.MadoituongGia = "DV";
                                                    objChidinhChitiet.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                                                    objChidinhChitiet.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                                                    objChidinhChitiet.TuTuc = 1;
                                                    objChidinhChitiet.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                                    objChidinhChitiet.PtramBhyt = 0;
                                                }
                                            }
                                            DmucDichvuclsChitiet _DmucDichvuclsChitiet = DmucDichvuclsChitiet.FetchByID(objChidinhChitiet.IdChitietdichvu);
                                            if (_DmucDichvuclsChitiet != null && Utility.Int32Dbnull(_DmucDichvuclsChitiet.TuTuc, 0) == 1)
                                                objChidinhChitiet.TuTuc = 1;//Ke ca co trong bang quan he
                                            if (objLuotkham.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                            {
                                                objChidinhChitiet.TuTuc = 0;
                                                objChidinhChitiet.PhuThu = 0;
                                            }
                                            objChidinhChitiet.NguoiSua = globalVariables.UserName;
                                            objChidinhChitiet.NgaySua = DateTime.Now;
                                            decimal PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                                            TinhCLS.GB_TinhPhtramBHYT(objChidinhChitiet, objLuotkham, Utility.Byte2Bool(objChidinh.Noitru), PtramBHYT);
                                        }

                                    }
                                    else
                                    {
                                        //nếu đã thanh toán thì chỉ xử lý khi đối tượng đang ở trạng thái nội trú.
                                        if (Utility.ByteDbnull(objLuotkham.TrangthaiNoitru,0) > 0)
                                        {
                                            saveParent = true;
                                            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                                            {
                                                //Đối tượng mới là BHYT thì kiểm tra đổi các dịch vụ có giá DV về tự túc. 
                                                //Các dịch vụ có giá khác DV thì để nguyên(để nếu đối tượng cũ là BHYT thì ko bị thay đổi giá trị tự túc)
                                                if (objChidinhChitiet.MadoituongGia == "DV")
                                                    objChidinhChitiet.TuTuc = 1;
                                            }
                                            else//Nếu đối tượng từ BHYT chuyển sang dịch vụ thì chuyển thành ko tự túc hết
                                            {
                                                if (objLuotkham.MaDoituongKcb != objLuotkhamCu.MaDoituongKcb)//Mới=DV, Cũ=BHYT. Còn lại ko làm gì cả
                                                    objChidinhChitiet.TuTuc = 0;
                                            }
                                        }
                                        else
                                            continue;
                                    }
                                    objChidinhChitiet.Save();
                                    saveParent = true;
                                }
                            }
                            if (saveParent)
                            {
                                objChidinh.Save();
                                saveParent = false;
                            }
                        
                    }
                }
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        private static ActionResult CapnhatBuonggiuong(KcbLichsuDoituongKcb objLichsu,List<NoitruPhanbuonggiuong> lstBuonggiuong )
        {
            using (var Scope = new TransactionScope())
            {
                if (lstBuonggiuong.Count > 0)
                {
                    //Kiểm tra nếu đối tượng ngoại trú đã có dịch vụ thanh toán-->Ko cho phép chuyển nữa
                    if (Utility.ByteDbnull(objLichsu.TrangthaiNoitru,0) <= 0 && lstBuonggiuong.Where(c => c.TrangthaiThanhtoan > 0).Any())
                    {
                        Scope.Complete();
                        return ActionResult.Cancel;
                    }
                    foreach (NoitruPhanbuonggiuong objBG in lstBuonggiuong)
                    {
                        if (Utility.Int32Dbnull(objBG.IdGoi, -1) <=0)
                        {
                            objBG.IdLichsuDoituongKcb = objLichsu.IdLichsuDoituongKcb;
                            noitru_nhapvien.LayThongTinGia(objBG, objLichsu);
                            objBG.Save();
                        }
                    }
                }
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        private static ActionResult CapnhatBuonggiuong(KcbLuotkham objLuotkham, KcbLuotkham objLuotkhamCu)
        {
            using (var Scope = new TransactionScope())
            {

                NoitruPhanbuonggiuongCollection lstBuonggiuong =
                    new Select()
                    .From(NoitruPhanbuonggiuong.Schema)
                    .Where(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
                if (lstBuonggiuong.Count > 0)
                {
                    //Kiểm tra nếu đối tượng ngoại trú đã có dịch vụ thanh toán-->Ko cho phép chuyển nữa
                    if (Utility.ByteDbnull(objLuotkham.TrangthaiNoitru,0) <= 0 && lstBuonggiuong.Where(c => c.TrangthaiThanhtoan > 0).Any())
                    {
                        Scope.Complete();
                        return ActionResult.Cancel;
                    }
                    foreach (NoitruPhanbuonggiuong objBG in lstBuonggiuong)
                    {
                        if (Utility.Int32Dbnull(objBG.IdGoi, -1) <=0)
                        {
                            objBG.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
                            noitru_nhapvien.LayThongTinGia(objBG, objLuotkham);
                            objBG.Save();
                        }
                    }
                }
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        private static ActionResult CapnhatChiphiKCB(KcbLichsuDoituongKcb objLichsu, List<KcbDangkyKcb> lstChiphiKCB)
        {
            using (var Scope = new TransactionScope())
            {
                if (lstChiphiKCB.Count > 0)
                {
                    //Kiểm tra nếu đối tượng ngoại trú đã có dịch vụ thanh toán-->Ko cho phép chuyển nữa
                    if (Utility.ByteDbnull(objLichsu.TrangthaiNoitru,0) <= 0 && lstChiphiKCB.Where(c => c.TrangthaiThanhtoan > 0).Any())
                    {
                        Scope.Complete();
                        return ActionResult.Cancel;
                    }
                    //CHUYỂN GIÁ KHÁM BỆNH VÀO PHÒNG
                    foreach (KcbDangkyKcb objDangkyKCB in lstChiphiKCB)
                    {
                        if (Utility.Int32Dbnull(objDangkyKCB.IdGoi, -1) <=0)
                        {
                            objDangkyKCB.MaDoituongkcb = objLichsu.MaDoituongKcb;
                            objDangkyKCB.IdDoituongkcb = objLichsu.IdDoituongKcb;
                            objDangkyKCB.IdLoaidoituongkcb = objLichsu.IdLoaidoituongKcb;
                            objDangkyKCB.IdLichsuDoituongKcb = objLichsu.IdLichsuDoituongKcb;
                            objDangkyKCB.MatheBhyt = objLichsu.MatheBhyt;
                            if (objDangkyKCB.TrangthaiThanhtoan == 0)//Các mục chưa thanh toán thì cho phép chuyển
                            {

                                DmucDichvukcb _DichvukcbCu =
                                    DmucDichvukcb.FetchByID(objDangkyKCB.IdDichvuKcb);
                                var _DichvukcbMoi =
                                    new Select().From(DmucDichvukcb.Schema)
                                        .Where(DmucDichvukcb.Columns.IdKhoaphong).IsEqualTo(_DichvukcbCu.IdKhoaphong)
                                        .And(DmucDichvukcb.Columns.IdPhongkham).IsEqualTo(_DichvukcbCu.IdPhongkham)
                                             .And(DmucDichvukcb.Columns.IdKieukham).IsEqualTo(_DichvukcbCu.IdKieukham)
                                        .AndExpression(DmucDichvukcb.Columns.MaDoituongKcb).IsEqualTo(objLichsu.MaDoituongKcb)
                                        .Or(DmucDichvukcb.Columns.MaDoituongKcb).IsEqualTo("ALL").CloseExpression()
                                        .ExecuteSingle<DmucDichvukcb>();
                                if (_DichvukcbMoi != null)
                                {
                                    objDangkyKCB.IdDichvuKcb = Utility.Int16Dbnull(_DichvukcbMoi.IdDichvukcb, -1);
                                    if (Utility.Int32Dbnull(_DichvukcbMoi.TuTuc, 0) == 1)
                                        objDangkyKCB.TuTuc = 1;//Ke ca co trong bang quan he
                                    if (objLichsu.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                        objDangkyKCB.TuTuc = 0;
                                    objDangkyKCB.TenDichvuKcb = _DichvukcbMoi.TenDichvukcb;
                                    objDangkyKCB.DonGia = Utility.DecimaltoDbnull(_DichvukcbMoi.DonGia);
                                    objDangkyKCB.PhuThu = !Utility.Byte2Bool(objLichsu.DungTuyen)
                                                       ? Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuDungtuyen)
                                                       : Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuTraituyen);
                                    if (!THU_VIEN_CHUNG.IsBaoHiem(objLichsu.IdLoaidoituongKcb))
                                        objDangkyKCB.PhuThu = 0;
                                    objDangkyKCB.PtramBhyt = objLichsu.PtramBhyt;//% BHYT ngoại trú
                                    objDangkyKCB.PtramBhytGoc = objLichsu.PtramBhytGoc;
                                    if (Utility.Byte2Bool(objDangkyKCB.KhamNgoaigio))
                                    {
                                        objDangkyKCB.KhamNgoaigio = 1;
                                        objDangkyKCB.DonGia = Utility.DecimaltoDbnull(_DichvukcbMoi.DongiaNgoaigio, 0);
                                        objDangkyKCB.PhuThu = !Utility.Byte2Bool(objLichsu.DungTuyen) ? Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuNgoaigio, 0) : Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuDungtuyen);
                                    }

                                    if (Utility.Int32Dbnull(objDangkyKCB.TuTuc, 0) == 0)
                                    {
                                        objDangkyKCB.BhytChitra = Utility.DecimaltoDbnull(objDangkyKCB.DonGia) *
                                                               Utility.DecimaltoDbnull(objLichsu.PtramBhyt) / 100;
                                        objDangkyKCB.BnhanChitra = Utility.DecimaltoDbnull(objDangkyKCB.DonGia, 0) -
                                                                  Utility.DecimaltoDbnull(objDangkyKCB.BhytChitra, 0);
                                    }
                                    else
                                    {
                                        objDangkyKCB.BhytChitra = 0;
                                        objDangkyKCB.BnhanChitra = Utility.DecimaltoDbnull(objDangkyKCB.DonGia, 0);
                                    }
                                }
                                
                            }
                            else
                            {
                                //nếu đã thanh toán thì chỉ xử lý khi đối tượng đang ở trạng thái nội trú.
                                if (Utility.ByteDbnull(objLichsu.TrangthaiNoitru,0)>0)
                                {
                                    if (THU_VIEN_CHUNG.IsBaoHiem(objLichsu.IdLoaidoituongKcb))
                                    {
                                        //Đối tượng mới là BHYT thì kiểm tra đổi các dịch vụ có giá DV về tự túc. 
                                        //Các dịch vụ có giá khác DV thì để nguyên(để nếu đối tượng cũ là BHYT thì ko bị thay đổi giá trị tự túc)
                                        if (objDangkyKCB.MadoituongGia == "DV")
                                            objDangkyKCB.TuTuc = 1;
                                    }
                                    else//Nếu đối tượng từ BHYT chuyển sang dịch vụ thì chuyển thành ko tự túc hết
                                    {
                                        if (objLichsu.MaDoituongKcb != objLichsu.MaDoituongKcb)//Mới=DV, Cũ=BHYT. Còn lại ko làm gì cả
                                            objDangkyKCB.TuTuc = 0;
                                    }
                                }
                                else
                                    continue;
                            }
                            if (objLichsu.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc, phụ thu=0
                            {
                                objDangkyKCB.TuTuc = 0;
                                objDangkyKCB.PhuThu = 0;
                            }
                            objDangkyKCB.Save();
                        }
                    }
                    
                }
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        private static ActionResult CapnhatChiphiKCB(KcbLuotkham objLuotkham, KcbLuotkham objLuotkhamCu)
        {
            using (var Scope = new TransactionScope())
            {
                KcbDangkyKcbCollection lstChiphiKCB =
                    new Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(
                        objLuotkham.MaLuotkham).And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbDangkyKcb.Columns.LaPhidichvukemtheo).IsEqualTo(0)
                        .ExecuteAsCollection<KcbDangkyKcbCollection>();
                if (lstChiphiKCB.Count > 0)
                {
                    //Kiểm tra nếu đối tượng ngoại trú đã có dịch vụ thanh toán-->Ko cho phép chuyển nữa
                    if (Utility.ByteDbnull(objLuotkham.TrangthaiNoitru,-1)<=0 && lstChiphiKCB.Where(c => c.TrangthaiThanhtoan > 0).Any())
                    {
                        Scope.Complete();
                        return ActionResult.Cancel;
                    }
                    //CHUYỂN GIÁ KHÁM BỆNH VÀO PHÒNG
                    foreach (KcbDangkyKcb objDangkyKCB in lstChiphiKCB)
                    {
                        if (Utility.Int32Dbnull(objDangkyKCB.IdGoi, -1) <=0)//Chỉ xử lý các bản ghi ngoài gói
                        {
                            objDangkyKCB.MaDoituongkcb = objLuotkham.MaDoituongKcb;
                            objDangkyKCB.IdDoituongkcb = objLuotkham.IdDoituongKcb;
                            objDangkyKCB.IdLoaidoituongkcb = objLuotkham.IdLoaidoituongKcb;
                            objDangkyKCB.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
                            objDangkyKCB.MatheBhyt = objLuotkham.MatheBhyt;
                            if (objDangkyKCB.TrangthaiThanhtoan == 0)//Các mục chưa thanh toán thì cho phép chuyển
                            {

                                DmucDichvukcb _DichvukcbCu =
                                    DmucDichvukcb.FetchByID(objDangkyKCB.IdDichvuKcb);
                                var _DichvukcbMoi =
                                    new Select().From(DmucDichvukcb.Schema)
                                        .Where(DmucDichvukcb.Columns.IdKhoaphong).IsEqualTo(_DichvukcbCu.IdKhoaphong)
                                        .And(DmucDichvukcb.Columns.IdPhongkham).IsEqualTo(_DichvukcbCu.IdPhongkham)
                                             .And(DmucDichvukcb.Columns.IdKieukham).IsEqualTo(_DichvukcbCu.IdKieukham)
                                        .AndExpression(DmucDichvukcb.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb)
                                        .Or(DmucDichvukcb.Columns.MaDoituongKcb).IsEqualTo("ALL").CloseExpression()
                                        .ExecuteSingle<DmucDichvukcb>();
                                if (_DichvukcbMoi != null)
                                {
                                    objDangkyKCB.IdDichvuKcb = Utility.Int16Dbnull(_DichvukcbMoi.IdDichvukcb, -1);
                                    if (Utility.Int32Dbnull(_DichvukcbMoi.TuTuc, 0) == 1)
                                        objDangkyKCB.TuTuc = 1;//Ke ca co trong bang quan he
                                    if (objLuotkham.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                    {
                                        objDangkyKCB.TuTuc = 0;
                                        objDangkyKCB.PhuThu = 0;
                                    }
                                    objDangkyKCB.TenDichvuKcb = _DichvukcbMoi.TenDichvukcb;
                                    objDangkyKCB.DonGia = Utility.DecimaltoDbnull(_DichvukcbMoi.DonGia);
                                    objDangkyKCB.PhuThu = !Utility.Byte2Bool(objLuotkham.DungTuyen)
                                                       ? Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuDungtuyen)
                                                       : Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuTraituyen);
                                    if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                                        objDangkyKCB.PhuThu = 0;
                                    objDangkyKCB.PtramBhyt = objLuotkham.PtramBhyt;//% BHYT ngoại trú
                                    objDangkyKCB.PtramBhytGoc = objLuotkham.PtramBhytGoc;
                                    if (Utility.Byte2Bool(objDangkyKCB.KhamNgoaigio))
                                    {
                                        objDangkyKCB.KhamNgoaigio = 1;
                                        objDangkyKCB.DonGia = Utility.DecimaltoDbnull(_DichvukcbMoi.DongiaNgoaigio, 0);
                                        objDangkyKCB.PhuThu = !Utility.Byte2Bool(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuNgoaigio, 0) : Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuDungtuyen);
                                    }

                                    if (Utility.Int32Dbnull(objDangkyKCB.TuTuc, 0) == 0)
                                    {
                                        objDangkyKCB.BhytChitra = Utility.DecimaltoDbnull(objDangkyKCB.DonGia) *
                                                               Utility.DecimaltoDbnull(objLuotkham.PtramBhyt) / 100;
                                        objDangkyKCB.BnhanChitra = Utility.DecimaltoDbnull(objDangkyKCB.DonGia, 0) -
                                                                  Utility.DecimaltoDbnull(objDangkyKCB.BhytChitra, 0);
                                    }
                                    else
                                    {
                                        objDangkyKCB.BhytChitra = 0;
                                        objDangkyKCB.BnhanChitra = Utility.DecimaltoDbnull(objDangkyKCB.DonGia, 0);
                                    }
                                }
                            }
                            else
                            {
                                //nếu đã thanh toán thì chỉ xử lý khi đối tượng đang ở trạng thái nội trú.
                                if (Utility.ByteDbnull(objLuotkham.TrangthaiNoitru,0) > 0)
                                {
                                    if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                                    {
                                        //Đối tượng mới là BHYT thì kiểm tra đổi các dịch vụ có giá DV về tự túc. 
                                        //Các dịch vụ có giá khác DV thì để nguyên(để nếu đối tượng cũ là BHYT thì ko bị thay đổi giá trị tự túc)
                                        if (objDangkyKCB.MadoituongGia == "DV")
                                            objDangkyKCB.TuTuc = 1;
                                    }
                                    else//Nếu đối tượng từ BHYT chuyển sang dịch vụ thì chuyển thành ko tự túc hết
                                    {
                                        if (objLuotkham.MaDoituongKcb != objLuotkhamCu.MaDoituongKcb)//Mới=DV, Cũ=BHYT. Còn lại ko làm gì cả
                                            objDangkyKCB.TuTuc = 0;
                                    }
                                }
                                else
                                    continue;
                            }
                            objDangkyKCB.Save();
                        }
                    }
                    
                }
                Scope.Complete();
                return ActionResult.Success;
            }
        }
       

      
       
    }
}
