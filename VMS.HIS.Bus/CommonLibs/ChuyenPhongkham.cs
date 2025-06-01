using System;
using SubSonic;
using VMS.HIS.DAL;
using System.Transactions;

namespace VNS.Libs
{
    public class ChuyenPhongkham
    {
        public static ActionResult ChuyenPhong(KcbDangkyKcb objCongKhamCu, string lydoChuyen, DmucDichvukcb objDichvuKcb)
        {
            try
            {
            ActionResult actionResult = ActionResult.Success;
                using (var scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        int sttkham = THU_VIEN_CHUNG.LaySothutuKCB(objDichvuKcb.IdPhongkham);
                        new Update(KcbDangkyKcb.Schema)
                        .Set(KcbDangkyKcb.Columns.IdPhongkham).EqualTo(objDichvuKcb.IdPhongkham)
                        .Set(KcbDangkyKcb.Columns.IdBacsikham).EqualTo(-1)
                        .Set(KcbDangkyKcb.Columns.SttKham).EqualTo(sttkham)
                        .Set(KcbDangkyKcb.Columns.IdDichvuKcb).EqualTo(objDichvuKcb.IdDichvukcb)
                        .Set(KcbDangkyKcb.Columns.IdKieukham).EqualTo(objDichvuKcb.IdKieukham)
                        .Set(KcbDangkyKcb.Columns.TenDichvuKcb).EqualTo(objDichvuKcb.TenDichvukcb.ToUpper())
                        .Set(KcbDangkyKcb.Columns.NgayDangky).EqualTo(DateTime.Now)
                        .Set(KcbDangkyKcb.Columns.NguoiChuyen).EqualTo(globalVariables.UserName)
                        .Set(KcbDangkyKcb.Columns.NgayChuyen).EqualTo(DateTime.Now)
                        .Set(KcbDangkyKcb.Columns.LydoChuyen).EqualTo(lydoChuyen)
                        .Set(KcbDangkyKcb.Columns.TrangthaiChuyen).EqualTo(1)
                        .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongKhamCu.IdKham)
                        .Execute();
                        //Cập nhật trong bảng thanh toán nếu có
                        new Update(KcbThanhtoanChitiet.Schema)
                         .Set(KcbThanhtoanChitiet.Columns.IdDichvu).EqualTo(objDichvuKcb.IdDichvukcb)
                          .Set(KcbThanhtoanChitiet.Columns.IdChitietdichvu).EqualTo(objDichvuKcb.IdKieukham)
                           .Set(KcbThanhtoanChitiet.Columns.TenChitietdichvu).EqualTo(objDichvuKcb.TenDichvukcb)
                        .Where(KcbThanhtoanChitiet.Columns.IdPhieu).IsEqualTo(objCongKhamCu.IdKham)
                        .And(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan).IsEqualTo(1)
                        .And(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objCongKhamCu.IdThanhtoan)
                        .Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển phòng khám:\n"+ex.Message);
                return ActionResult.Exception;
            }
        }
       

      
       
    }
}
