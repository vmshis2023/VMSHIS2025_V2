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
   public class DMUC_CHUNG_BUSRULE
    {
        /// <summary>
        /// Lấy thông tin bảng danh mục theo từng Loại
        /// </summary>
        /// <param name="p_strLoai">Loại danh mục</param>
        /// <returns></returns>
        public DataSet dsGetList(string p_strLoai)
        {
            try
            {
               DataSet   dsData = SPs.DmucLaydulieudanhmuchung(p_strLoai,globalVariables.UserName).GetDataSet();
                //new DmucChungController().FetchByQuery(DmucChung.CreateQuery().AddWhere(DmucChung.Columns.Loai, Comparison.Equals, p_strLoai)).ToDataTable();
               dsData.Tables[0].TableName = DmucChung.Schema.TableName;
               return dsData;
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
        public void InsertList(DmucChung obj, int intSTTCu, ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord(obj.Ma, obj.Loai);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                //B1: Tim ban ghi co STT=STT moi
                DmucChungCollection v_lstDmuc = new DmucChungController().FetchByQuery(DmucChung.CreateQuery().AddWhere(DmucChung.Columns.SttHthi, Comparison.Equals, obj.SttHthi).AND(DmucChung.Columns.Loai, Comparison.Equals, obj.Loai));
                if (v_lstDmuc.Count > 0)
                {
                    new Update(DmucChung.Schema).Set(DmucChung.Columns.SttHthi).EqualTo(intSTTCu)
                        .Where(DmucChung.Columns.Ma).IsEqualTo(v_lstDmuc[0].Ma)
                        .And(DmucChung.Columns.Loai).IsEqualTo(v_lstDmuc[0].Loai).Execute();
                }
                obj.IsNew = true;
                obj.Save();
                if (Utility.Byte2Bool(obj.TrangthaiMacdinh))
                    new Update(DmucChung.Schema).Set(DmucChung.Columns.TrangthaiMacdinh).EqualTo(0)
                        .Where(DmucChung.Columns.Ma).IsNotEqualTo(obj.Ma)
                        .And(DmucChung.Columns.Loai).IsEqualTo(obj.Loai).Execute();
                ActResult = ActionResult.Success.ToString();
            }
            catch
            {
                ActResult = ActionResult.Exception.ToString();
            }

        }
        public ActionResult isExistedRecord(string Ma, string Loai)
        {
            try
            {
                DmucChung v_obj = new Select().From(DmucChung.Schema.TableName).Where(DmucChung.Columns.Ma).IsEqualTo(Ma).And(DmucChung.Columns.Loai).IsEqualTo(Loai).ExecuteSingle<DmucChung>();
                if (v_obj != null) return ActionResult.ExistedRecord;
                return ActionResult.Success;
            }
            catch
            {
                return ActionResult.Exception;
            }
        }
        public DmucKieudmuc GetKieuDanhMuc(string MaLoai)
        {
            try
            {
                DmucKieudmuc v_obj = new Select().From(DmucKieudmuc.Schema.TableName).Where(DmucKieudmuc.Columns.MaLoai).IsEqualTo(MaLoai).ExecuteSingle<DmucKieudmuc>();
                return v_obj;
            }
            catch
            {
                return null;
            }
        }
        public ActionResult isExistedRecord4Update(string Loai,string MaMoi, string MaCu)
        {
            try
            {
                DmucChungCollection v_obj = new DmucChungController().FetchByQuery(DmucChung.CreateQuery()
                    .AddWhere(DmucChung.Columns.Ma, Comparison.NotEquals, MaCu)
                    .AND(DmucChung.Columns.Loai, Comparison.Equals, Loai));
                List<DmucChung> q = (from p in v_obj
                                      where p.Ma == MaMoi
                                      select p).ToList<DmucChung>();
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

        public void UpdateList(DmucChung obj, string strOldCode, int intSTTCu, ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord4Update(obj.Loai,obj.Ma, strOldCode);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                //B1: Tim ban ghi co STT=STT moi
                DmucChungCollection v_lstDmuc = new DmucChungController().FetchByQuery(DmucChung.CreateQuery().AddWhere(DmucChung.Columns.SttHthi, Comparison.Equals, obj.SttHthi).AND(DmucChung.Columns.Loai, Comparison.Equals, obj.Loai));
                if (v_lstDmuc.Count > 0)
                {
                    new Update(DmucChung.Schema).Set(DmucChung.Columns.SttHthi).EqualTo(intSTTCu)
                        .Where(DmucChung.Columns.Ma).IsEqualTo(v_lstDmuc[0].Ma)
                        .And(DmucChung.Columns.Loai).IsEqualTo(v_lstDmuc[0].Loai).Execute();
                }
                if (Utility.Byte2Bool(obj.TrangthaiMacdinh))
                    new Update(DmucChung.Schema).Set(DmucChung.Columns.TrangthaiMacdinh).EqualTo(0)
                        .Where(DmucChung.Columns.Ma).IsNotEqualTo(obj.Ma)
                        .And(DmucChung.Columns.Loai).IsEqualTo(obj.Loai).Execute();

                int record = new Update(DmucChung.Schema)
                    .Set(DmucChung.Columns.Ma).EqualTo(obj.Ma)
                    .Set(DmucChung.Columns.Ten).EqualTo(obj.Ten)
                    .Set(DmucChung.Columns.Phanloai).EqualTo(obj.Phanloai)
                    .Set(DmucChung.Columns.VietTat).EqualTo(obj.VietTat)
                    .Set(DmucChung.Columns.SttHthi).EqualTo(obj.SttHthi)
                    .Set(DmucChung.Columns.MotaThem).EqualTo(obj.MotaThem)
                    .Set(DmucChung.Columns.TrangThai).EqualTo(obj.TrangThai)
                    .Set(DmucChung.Columns.TrangthaiMacdinh).EqualTo(obj.TrangthaiMacdinh)
                    .Set(DmucChung.Columns.NgaySua).EqualTo(obj.NgaySua)
                    .Set(DmucChung.Columns.NguoiSua).EqualTo(obj.NguoiSua)
                    .Where(DmucChung.Columns.Ma).IsEqualTo(strOldCode).
                    And(DmucChung.Columns.Loai).IsEqualTo(obj.Loai).Execute();
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
        public void DeleteList(string p_strMa, string LoaiDanhmuc, ref string ActResult)
        {
            try
            {
                int record = new Delete().From(DmucChung.Schema).Where(DmucChung.Columns.Ma).IsEqualTo(p_strMa).And(DmucChung.Columns.Loai).IsEqualTo(LoaiDanhmuc).Execute();
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
                DmucChungCollection CollectionData = new DmucChungController().FetchByQuery(DmucChung.CreateQuery().AddWhere(DmucChung.Columns.Loai, Comparison.Equals, m_strListType));
                int shtMaxSTT = 0;
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
