using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VMS.HIS.DAL;
using SubSonic;
using VNS.Libs;
using System.Transactions;

namespace VNS.HIS.NGHIEPVU
{
   public class dmucgiagiuong_busrule
    {
        /// <summary>
        /// Lấy thông tin bảng danh mục theo từng Loại
        /// </summary>
        /// <param name="p_strLoai">Loại danh mục</param>
        /// <returns></returns>
        public DataSet dsGetList(string nhombaocao)
        {
            try
            {
                return SPs.DmucLaydanhmucGiabuonggiuong(nhombaocao).GetDataSet();
            }
            catch
            {
                return null;
            }
        }
        public DataSet dsGetLoaigiuong(Int16 id_giuong)
        {
            try
            {
                return SPs.NoitruLaydanhmucGiabuonggiuong(id_giuong).GetDataSet();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Insert một bản ghi vào bảng Danh mục dùng chung
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void InsertList(NoitruGiabuonggiuong obj, int intSTTCu, ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord(obj.MaGia);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                //B1: Tim ban ghi co STT=STT moi
                NoitruGiabuonggiuongCollection v_lstDmuc = new NoitruGiabuonggiuongController().FetchByQuery(NoitruGiabuonggiuong.CreateQuery().AddWhere(NoitruGiabuonggiuong.Columns.SttHthi, Comparison.Equals, obj.SttHthi));
                if (v_lstDmuc.Count > 0)
                {
                    new Update(NoitruGiabuonggiuong.Schema).Set(NoitruGiabuonggiuong.Columns.SttHthi).EqualTo(intSTTCu)
                        .Where(NoitruGiabuonggiuong.Columns.IdGia).IsEqualTo(v_lstDmuc[0].IdGia).Execute();
                }
                obj.IsNew = true;
                obj.Save();
                ActResult = ActionResult.Success.ToString();
            }
            catch
            {
                ActResult = ActionResult.Exception.ToString();
            }

        }
        public ActionResult isExistedRecord(string Ma)
        {
            try
            {
                NoitruGiabuonggiuong v_obj = new Select().From(NoitruGiabuonggiuong.Schema.TableName).Where(NoitruGiabuonggiuong.Columns.MaGia).IsEqualTo(Ma).ExecuteSingle<NoitruGiabuonggiuong>();
                if (v_obj != null) return ActionResult.ExistedRecord;
                return ActionResult.Success;
            }
            catch
            {
                return ActionResult.Exception;
            }
        }
        
        public ActionResult isExistedRecord4Update(string MaMoi, string MaCu)
        {
            try
            {
                NoitruGiabuonggiuongCollection v_obj = new NoitruGiabuonggiuongController().FetchByQuery(NoitruGiabuonggiuong.CreateQuery()
                    .AddWhere(NoitruGiabuonggiuong.Columns.MaGia, Comparison.NotEquals, MaCu)
                    );
                List<NoitruGiabuonggiuong> q = (from p in v_obj
                                      where p.MaGia == MaMoi
                                      select p).ToList<NoitruGiabuonggiuong>();
                if (q.Count() > 0) return ActionResult.ExistedRecord;
                return ActionResult.Success;
            }
            catch
            {
                return ActionResult.Exception;
            }
        }
        /// <summary>
        /// Update một bản ghi vào bảng Danh mục dùng chung
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        public void UpdateList(NoitruGiabuonggiuong obj, string strOldCode, int intSTTCu, ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord4Update(obj.MaGia, strOldCode);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                //B1: Tim ban ghi co STT=STT moi
                NoitruGiabuonggiuongCollection v_lstDmuc = new NoitruGiabuonggiuongController().FetchByQuery(NoitruGiabuonggiuong.CreateQuery().AddWhere(NoitruGiabuonggiuong.Columns.SttHthi, Comparison.Equals, obj.SttHthi));
                if (v_lstDmuc.Count > 0)
                {
                    new Update(NoitruGiabuonggiuong.Schema).Set(NoitruGiabuonggiuong.Columns.SttHthi).EqualTo(intSTTCu)
                        .Where(NoitruGiabuonggiuong.Columns.MaGia).IsEqualTo(v_lstDmuc[0].MaGia)
                       .Execute();
                }
                int record = new Update(NoitruGiabuonggiuong.Schema)
                    .Set(NoitruGiabuonggiuong.Columns.MaGia).EqualTo(obj.MaGia)
                    .Set(NoitruGiabuonggiuong.Columns.TenGia).EqualTo(obj.TenGia)
                    .Set(NoitruGiabuonggiuong.Columns.GiaDichvu).EqualTo(obj.GiaDichvu)
                    .Set(NoitruGiabuonggiuong.Columns.GiaBhyt).EqualTo(obj.GiaBhyt)
                    .Set(NoitruGiabuonggiuong.Columns.GiaKhac).EqualTo(obj.GiaKhac)
                    .Set(NoitruGiabuonggiuong.Columns.PhuthuDungtuyen).EqualTo(obj.PhuthuDungtuyen)
                    .Set(NoitruGiabuonggiuong.Columns.PhuthuTraituyen).EqualTo(obj.PhuthuTraituyen)
                    .Set(NoitruGiabuonggiuong.Columns.SttHthi).EqualTo(obj.SttHthi)
                    .Set(NoitruGiabuonggiuong.Columns.NhomBaocao).EqualTo(obj.NhomBaocao)
                    .Set(NoitruGiabuonggiuong.Columns.TrangThai).EqualTo(obj.TrangThai)
                    .Set(NoitruGiabuonggiuong.Columns.NgaySua).EqualTo(obj.NgaySua)
                    .Set(NoitruGiabuonggiuong.Columns.NguoiSua).EqualTo(obj.NguoiSua)
                    .Where(NoitruGiabuonggiuong.Columns.IdGia).IsEqualTo(obj.IdGia)
                    .Execute();
                if (record > 0)
                {
                    ActResult = ActionResult.Success.ToString();
                }
                else
                    ActResult = ActionResult.Error.ToString();
            }
            catch
            {
                ActResult = ActionResult.Exception.ToString();
            }
        }

        /// <summary>
        /// Delete một bản ghi vào bảng Danh mục dùng chung
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void DeleteList(Int16 id, ref string ActResult)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Delete().From(NoitruQheGiuongGium.Schema).Where(NoitruQheGiuongGium.Columns.IdGia).IsEqualTo(id).Execute();
                        int record = new Delete().From(NoitruGiabuonggiuong.Schema).Where(NoitruGiabuonggiuong.Columns.IdGia).IsEqualTo(id).Execute();
                        if (record > 0)
                            ActResult = ActionResult.Success.ToString();
                        else
                            ActResult = ActionResult.Error.ToString();
                        scope.Complete();
                    }//end of SharedDbConnectionScope
                }//end of TransactionScope
            }
            catch
            {
                ActResult = ActionResult.Exception.ToString();
            }
        }
        public short GetMaxSTT(string m_strListType)
        {
            try
            {
                NoitruGiabuonggiuongCollection CollectionData = new NoitruGiabuonggiuongController().FetchByQuery(NoitruGiabuonggiuong.CreateQuery());
                Int16 shtMaxSTT = 0;
                //Phải kiểm tra nếu Có dữ liệu mới lấy STT hiện tại=MaxSTT+1
                if (CollectionData.Count > 0) shtMaxSTT = CollectionData.Max(c => c.SttHthi);
                return Convert.ToInt16(shtMaxSTT + 1);
            }
            catch
            {
                return Convert.ToInt16(1);
            }
        }
       
    }
}
