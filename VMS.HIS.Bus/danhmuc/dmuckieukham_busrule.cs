using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VMS.HIS.DAL;
using SubSonic;
using VNS.Libs;

namespace VNS.HIS.NGHIEPVU
{
   public class dmuckieukham_busrule
    {
        /// <summary>
        /// Lấy thông tin bảng danh mục theo từng Loại
        /// </summary>
        /// <param name="p_strLoai">Loại danh mục</param>
        /// <returns></returns>
        public DataSet dsGetList(string nhombaocao,string madoituongkcb)
        {
            try
            {
                return SPs.DmucLaydanhmuckieukham(nhombaocao, madoituongkcb).GetDataSet();
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
        public void InsertList(DmucKieukham obj, int intSTTCu, ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord(obj.MaKieukham);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                //B1: Tim ban ghi co STT=STT moi
                DmucKieukhamCollection v_lstDmuc = new DmucKieukhamController().FetchByQuery(DmucKieukham.CreateQuery().AddWhere(DmucKieukham.Columns.SttHthi, Comparison.Equals, obj.SttHthi));
                if (v_lstDmuc.Count > 0)
                {
                    new Update(DmucKieukham.Schema).Set(DmucKieukham.Columns.SttHthi).EqualTo(intSTTCu)
                        .Where(DmucKieukham.Columns.IdKieukham).IsEqualTo(v_lstDmuc[0].IdKieukham).Execute();
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
                DmucKieukham v_obj = new Select().From(DmucKieukham.Schema.TableName).Where(DmucKieukham.Columns.MaKieukham).IsEqualTo(Ma).ExecuteSingle<DmucKieukham>();
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
                DmucKieukhamCollection v_obj = new DmucKieukhamController().FetchByQuery(DmucKieukham.CreateQuery()
                    .AddWhere(DmucKieukham.Columns.MaKieukham, Comparison.NotEquals, MaCu)
                    );
                List<DmucKieukham> q = (from p in v_obj
                                      where p.MaKieukham == MaMoi
                                      select p).ToList<DmucKieukham>();
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

        public void UpdateList(DmucKieukham obj, string strOldCode, int intSTTCu, ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord4Update(obj.MaKieukham, strOldCode);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                //B1: Tim ban ghi co STT=STT moi
                DmucKieukhamCollection v_lstDmuc = new DmucKieukhamController().FetchByQuery(DmucKieukham.CreateQuery().AddWhere(DmucKieukham.Columns.SttHthi, Comparison.Equals, obj.SttHthi));
                if (v_lstDmuc.Count > 0)
                {
                    new Update(DmucKieukham.Schema).Set(DmucKieukham.Columns.SttHthi).EqualTo(intSTTCu)
                        .Where(DmucKieukham.Columns.MaKieukham).IsEqualTo(v_lstDmuc[0].MaKieukham)
                       .Execute();
                }
                int record = new Update(DmucKieukham.Schema)
                    .Set(DmucKieukham.Columns.MaKieukham).EqualTo(obj.MaKieukham)
                    .Set(DmucKieukham.Columns.TenKieukham).EqualTo(obj.TenKieukham)
                    .Set(DmucKieukham.Columns.DonGia).EqualTo(obj.DonGia)
                    .Set(DmucKieukham.Columns.SttHthi).EqualTo(obj.SttHthi)
                    .Set(DmucKieukham.Columns.MaDoituongkcb).EqualTo(obj.MaDoituongkcb)
                    .Set(DmucKieukham.Columns.NhomBaocao).EqualTo(obj.NhomBaocao)
                    .Set(DmucKieukham.Columns.TrangThai).EqualTo(obj.TrangThai)
                    .Set(DmucKieukham.Columns.NgaySua).EqualTo(obj.NgaySua)
                    .Set(DmucKieukham.Columns.NguoiSua).EqualTo(obj.NguoiSua)
                    .Where(DmucKieukham.Columns.IdKieukham).IsEqualTo(obj.IdKieukham)
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
                int record = new Delete().From(DmucKieukham.Schema).Where(DmucKieukham.Columns.IdKieukham).IsEqualTo(id).Execute();
                if (record > 0)
                    ActResult = ActionResult.Success.ToString();
                else
                    ActResult = ActionResult.Error.ToString();
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
                DmucKieukhamCollection CollectionData = new DmucKieukhamController().FetchByQuery(DmucKieukham.CreateQuery());
                Int16 shtMaxSTT = 0;
                //Phải kiểm tra nếu Có dữ liệu mới lấy STT hiện tại=MaxSTT+1
                if (CollectionData.Count > 0) shtMaxSTT = CollectionData.Max(c => c.SttHthi).Value;
                return Convert.ToInt16(shtMaxSTT + 1);
            }
            catch
            {
                return Convert.ToInt16(1);
            }
        }
       
    }
}
