using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using NLog;
using VNS.Properties;
using System.Data;
namespace VNS.HIS.NGHIEPVU.THUOC
{

    public class KiemTra
    {
        private static NLog.Logger log;
        public KiemTra()
        {
            log = NLog.LogManager.GetLogger("KCB_KEDONTHUOC");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="objPhieuNhapCt"></param>
        /// <param name="id_thuockho">có thể là id thuốc kho(Với tình huống đơn thuốc) hoặc id chuyển(với các phiếu nhập xuất)</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static ActionResult KiemtraTonthuoctheoIdthuockho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt, long id_thuockho, ref string errMsg)
        {
            //TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
            //  TThuockho.CreateQuery()
            //  .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhoxuat)
            //  .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
            //  .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
            //  .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
            //  .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
            //  .AND(TThuockho.MaNhacungcapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.MaNhacungcap)
            //  .AND(TThuockho.SoLoColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.SoLo)
            //  .AND(TThuockho.NgayNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayNhap)
            //  .AND(TThuockho.GiaBhytColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBhyt)
            //  .AND(TThuockho.VatColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.Vat)
            //  );
            //TThuockhoCollection vCollection = new Select().From(TThuockho.Schema).Where(TThuockho.Columns.IdThuockho).IsEqualTo(objPhieuNhapCt.IdChuyen).ExecuteAsCollection<TThuockhoCollection>();
            //if (vCollection.Count <= 0)
            //{
            //    errMsg = string.Format("ID thuốc kho {0} không tồn tại. Đề nghị kiểm tra lại", objPhieuNhapCt.IdChuyen.ToString());
            //    log.Trace(errMsg);
            //    return ActionResult.Exceed;//Lỗi không có dòng dữ liệu trong bảng kho-thuốc
            //}
            //decimal SoLuong = vCollection.Sum(o=>o.SoLuong);
            //SoLuong = SoLuong - Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0);

            DataTable dtThuockho = SPs.ThuocNhapkhoKiemtratruockhihuy(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, id_thuockho).GetDataSet().Tables[0];
            if (dtThuockho == null || dtThuockho.Rows.Count <= 0) return ActionResult.Exceed;


            decimal SluongChia = Utility.DecimaltoDbnull(objPhieuNhapCt.SluongChia, 1);
            if (SluongChia <= 0) SluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
            decimal SoLuong = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong"], 0);//số lượng nhìn thấy khi xem tồn kho
            decimal sluong_choxacnhan = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong_choxacnhan"], 0);// lượng chờ xác nhận không được đụng đến(bao gồm cả số lượng đang xét nên lát cần + lại)
            decimal sluong_sudung = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong_sudung"], 0);// số lượng có thể sử dụng =SoLuong-sluong_choxacnhan


            decimal slconlai = sluong_sudung + Utility.DecimaltoDbnull( objPhieuNhapCt.SoLuong,0);

            if (slconlai < Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0))
            {
                errMsg = string.Format("ID thuốc={0},id thuốc kho={1}: Số lượng còn trong kho {2}, Số lượng bị trừ {3}", objPhieuNhapCt.IdThuoc.ToString(), id_thuockho, slconlai.ToString(), objPhieuNhapCt.SoLuong.ToString());
                //log.Trace(errMsg);
                return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
             
            }
            return ActionResult.Success;
        }
        /// <summary>
        /// Kiểm tra tồn thuốc theo ID thuốc kho trước khi thực hiện các thao tác hủy xác nhận, xác nhận hoặc phát thuốc
        /// </summary>
        /// <param name="id_thuoc"></param>
        /// <param name="id_khoxuat"></param>
        /// <param name="sl_ke"></param>
        /// <param name="SluongChia"></param>
        /// <param name="id_thuockho">có thể là id thuốc kho(Với tình huống đơn thuốc) hoặc id chuyển(với các phiếu nhập xuất)</param>
        /// <param name="IncludeMe">true: số lượng khả dụng=Tồn kho- chờ xác nhận; False: số lượng khả dụng=Tồn kho- chờ xác nhận + số lượng  </param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static ActionResult KiemtraTonthuoctheoIdthuockho(int id_thuoc, short? id_khoxuat, decimal? sl_ke,decimal SluongChia, long? id_thuockho,bool IncludeMe, ref string errMsg)
        {

            id_khoxuat = Utility.Int16Dbnull(id_khoxuat, 0);
            sl_ke = Utility.DecimaltoDbnull(sl_ke, 0);
            id_thuockho = Utility.Int64Dbnull(id_thuockho, 0);
            DataTable dtThuockho = SPs.ThuocNhapkhoKiemtratruockhihuy(id_thuoc, id_khoxuat, id_thuockho).GetDataSet().Tables[0];
            if (dtThuockho == null || dtThuockho.Rows.Count <= 0) return ActionResult.Exceed;


            if (SluongChia <= 0) SluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
            decimal SoLuong = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong"], 0);//số lượng nhìn thấy khi xem tồn kho
            decimal sluong_choxacnhan = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong_choxacnhan"], 0);// lượng chờ xác nhận không được đụng đến(bao gồm cả số lượng đang xét nên lát cần + lại)
            decimal sluong_sudung = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong_sudung"], 0);// số lượng khả dụng cấp 1 =SoLuong-sluong_choxacnhan


            decimal slconlai = sluong_sudung + (IncludeMe ? Utility.DecimaltoDbnull(sl_ke, 0) : 0m);

            if (SoLuong<=0 || slconlai < Utility.DecimaltoDbnull(sl_ke, 0))
            {
                errMsg = string.Format("ID thuốc: {0}\nid thuốc kho: {1}\nSố lượng khả dụng: {2}\nSố lượng bị trừ: {3}", id_thuoc.ToString(), id_thuockho, slconlai.ToString(), sl_ke.ToString());
                //log.Trace(errMsg);
                return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy

            }
            return ActionResult.Success;
        }
        /// <summary>
        /// Xử lý khác logic chút đoạn số lượng chờ xác nhận phải >0. Nếu không do vượt ngày nhả tồn
        /// </summary>
        /// <param name="id_thuoc"></param>
        /// <param name="id_khoxuat"></param>
        /// <param name="sl_ke"></param>
        /// <param name="SluongChia"></param>
        /// <param name="id_thuockho"></param>
        /// <param name="IncludeMe"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static ActionResult KiemtraTonthuoctheoIdthuockho_ngoaitru(int id_thuoc, short? id_khoxuat, decimal? sl_ke, decimal SluongChia, long? id_thuockho, bool IncludeMe, ref string errMsg)
        {

            id_khoxuat = Utility.Int16Dbnull(id_khoxuat, 0);
            sl_ke = Utility.DecimaltoDbnull(sl_ke, 0);
            id_thuockho = Utility.Int64Dbnull(id_thuockho, 0);
            DataTable dtThuockho = SPs.ThuocNhapkhoKiemtratruockhihuy(id_thuoc, id_khoxuat, id_thuockho).GetDataSet().Tables[0];
            if (dtThuockho == null || dtThuockho.Rows.Count <= 0) return ActionResult.Exceed;


            if (SluongChia <= 0) SluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
            decimal SoLuong = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong"], 0);//số lượng nhìn thấy khi xem tồn kho
            decimal sluong_choxacnhan = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong_choxacnhan"], 0);// lượng chờ xác nhận không được đụng đến(bao gồm cả số lượng đang xét nên lát cần + lại)
            decimal sluong_sudung = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong_sudung"], 0);// số lượng khả dụng cấp 1 =SoLuong-sluong_choxacnhan

            if (sluong_choxacnhan <= sl_ke || SoLuong<=0) IncludeMe = false;//Vượt ngày nhả tồn nên chờ xác nhận không tính số lượng kê
            decimal slconlai = sluong_sudung + (IncludeMe ? Utility.DecimaltoDbnull(sl_ke, 0) : 0m);

            if ( slconlai < Utility.DecimaltoDbnull(sl_ke, 0))
            {
                errMsg = string.Format("ID thuốc: {0}\nid thuốc kho: {1}\nSố lượng khả dụng: {2}\nSố lượng bị trừ: {3}", id_thuoc.ToString(), id_thuockho, slconlai.ToString(), sl_ke.ToString());
                //log.Trace(errMsg);
                return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy

            }
            return ActionResult.Success;
        }
        public static ActionResult KiemtraTonthuoctheoIdthuockho_PhieuNhapxuat(int id_thuoc, short? id_khoxuat, decimal? sl_ke, decimal SluongChia, long? id_thuockho, bool IncludeMe, ref string errMsg)
        {

            id_khoxuat = Utility.Int16Dbnull(id_khoxuat, 0);
            sl_ke = Utility.DecimaltoDbnull(sl_ke, 0);
            id_thuockho = Utility.Int64Dbnull(id_thuockho, 0);
            DataTable dtThuockho = SPs.ThuocNhapkhoKiemtratruockhihuy(id_thuoc, id_khoxuat, id_thuockho).GetDataSet().Tables[0];
            if (dtThuockho == null || dtThuockho.Rows.Count <= 0) return ActionResult.Exceed;


            if (SluongChia <= 0) SluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
            decimal SoLuong = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong"], 0);//số lượng nhìn thấy khi xem tồn kho
            decimal sluong_choxacnhan = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong_choxacnhan"], 0);// lượng chờ xác nhận không được đụng đến(bao gồm cả số lượng đang xét nên lát cần + lại)
            decimal sluong_sudung = Utility.DecimaltoDbnull(dtThuockho.Rows[0]["sluong_sudung"], 0);// số lượng khả dụng cấp 1 =SoLuong-sluong_choxacnhan


            decimal slconlai = sluong_sudung + (IncludeMe ? Utility.DecimaltoDbnull(sl_ke, 0) : 0m);
            decimal slsauhuy = SoLuong - Utility.DecimaltoDbnull(sl_ke, 0);
            if (slsauhuy < Utility.DecimaltoDbnull(sluong_choxacnhan, 0))
            {
                errMsg = string.Format("ID thuốc: {0}\nid thuốc kho: {1}\nSố lượng trong kho: {2},\nSố lượng hủy: {3}\nSố lượng chờ xác nhận: {4} > số lượng sau hủy {5}-->Không thể thực hiện", id_thuoc.ToString(), id_thuockho, SoLuong.ToString(), sl_ke.ToString(), sluong_choxacnhan, slsauhuy);
                //log.Trace(errMsg);
                return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy

            }
            return ActionResult.Success;
        }
    }
}
