using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VNS.HIS.DAL;
using VNS.Libs;
using SubSonic;

namespace VNS.HIS.NGHIEPVU.THUOC
{
    public class InsertKeDon
    {
        public ActionResult ThemDonKe (DPhieuXuatBnhan objPhieuXuatBnhan,TXuatthuocTheodon []objArrPhieuDonThuocCts)
        {

            try
            {
                using (var Scope = new TransactionScope())
              {
                  using (var dbScope = new SharedDbConnectionScope())
                  {
                      ThemPhieuXuatTH(objPhieuXuatBnhan);
                      foreach (var objPhieuDonThuocCt in objArrPhieuDonThuocCts)
                      {
                          objPhieuDonThuocCt.IdPhieuXuatBn = Utility.Int32Dbnull(objPhieuXuatBnhan.IdPhieuXuatBn);
                          objPhieuDonThuocCt.IsNew = true;
                          objPhieuDonThuocCt.Save();
                          StoredProcedure sp = SPs.TXuatthuocTheodonInsert(objPhieuDonThuocCt.ITXuatthuocTheodon,
                                                                          objPhieuDonThuocCt.IdPhieuXuatBn,
                                                                          objPhieuDonThuocCt.IdThuoc,
                                                                          objPhieuDonThuocCt.SoLuong,
                                                                          objPhieuDonThuocCt.DonGia,
                                                                          objPhieuDonThuocCt.PhuThu,
                                                                          objPhieuDonThuocCt.Bnct,
                                                                          objPhieuDonThuocCt.Bhct,
                                                                          objPhieuDonThuocCt.PtramBhyt,
                                                                          objPhieuDonThuocCt.ChiDan,
                                                                          objPhieuDonThuocCt.CachDung,
                                                                          objPhieuDonThuocCt.ChiDanThem,
                                                                          objPhieuDonThuocCt.SoLanDung,
                                                                          objPhieuDonThuocCt.SoluongDung,
                                                                          objPhieuDonThuocCt.Ngaytao,
                                                                          objPhieuDonThuocCt.NguoiTao,
                                                                          objPhieuDonThuocCt.PresDetailId,
                                                                          objPhieuDonThuocCt.PresId);
                      }

                   }
                  Scope.Complete();
                  return ActionResult.Success; 
              }
            }
            catch (Exception)
            {
                return ActionResult.Error;
                throw;
            }
        }
        public  void ThemPhieuXuatTH(DPhieuXuatBnhan objectPhieuxuatBN)
        {
            try
            {
                 DPhieuXuatBnhan _phieuxuatBN = new DPhieuXuatBnhan();
                _phieuxuatBN = objectPhieuxuatBN;
                _phieuxuatBN.IsNew = true;
                _phieuxuatBN.Save();
                StoredProcedure sp = SPs.DPhieuXuatBnhanInsert(objectPhieuxuatBN.IdPhieuXuatBn,
                                                               objectPhieuxuatBN.MaPhieuXuatBn,
                                                               objectPhieuxuatBN.NgayXuatBn, objectPhieuxuatBN.NgayKeDon,
                                                               objectPhieuxuatBN.TenBenhnhan, objectPhieuxuatBN.TenKhongDau,
                                                               objectPhieuxuatBN.Gtinh, objectPhieuxuatBN.ChanDoan,
                                                               objectPhieuxuatBN.MaBenh, objectPhieuxuatBN.DiaChi,
                                                               objectPhieuxuatBN.NamSinh, objectPhieuxuatBN.SoThe,
                                                               objectPhieuxuatBN.IdDoiTuong,
                                                               objectPhieuxuatBN.IdNhanVien,
                                                               objectPhieuxuatBN.IdDonThuoc, objectPhieuxuatBN.IdKhoaCd,
                                                               objectPhieuxuatBN.IdBacsiKedon, objectPhieuxuatBN.IdKhoXuat,
                                                               objectPhieuxuatBN.HienThi, objectPhieuxuatBN.NoiTru,
                                                               objectPhieuxuatBN.LoaiDthuoc, objectPhieuxuatBN.TrangThai,
                                                               objectPhieuxuatBN.Ngaytao, objectPhieuxuatBN.NguoiTao,
                                                               objectPhieuxuatBN.LoaiPhieu);
                sp.Execute();
                objectPhieuxuatBN.IdPhieuXuatBn = Utility.Int32Dbnull(sp.OutputValues[0]);
                // return ActionResult.Success;
            }
            catch (Exception)
            {
              
            }
        }
    }
}
