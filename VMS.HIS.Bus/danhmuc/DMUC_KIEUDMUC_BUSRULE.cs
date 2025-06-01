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
   public class DMUC_KIEUDMUC_BUSRULE
    {
        /// <summary>
        /// Lấy thông tin bảng danh mục theo từng Loại
        /// </summary>
        /// <param name="p_strLoai">Loại danh mục</param>
        /// <returns></returns>
        public DataSet dsGetList()
        {
            try
            {
                DataTable dtData = new DmucKieudmucController().FetchAll().ToDataTable();
                dtData.TableName = DmucKieudmuc.Schema.TableName;
                DataSet dsData = new DataSet();
                dsData.Tables.Add(dtData);
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
        public void InsertList(DmucKieudmuc obj,  ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord(obj.MaLoai);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                obj.IsNew = true;
                obj.Save();
                obj.Id =Utility.Int32Dbnull( DmucKieudmuc.CreateQuery().GetMax(DmucKieudmuc.IdColumn.ColumnName),0);
                ActResult = ActionResult.Success.ToString();
            }
            catch
            {
                ActResult = ActionResult.Exception.ToString();
            }

        }

     
       
        public ActionResult isExistedRecord(string Maloai)
        {
            try
            {
                DmucKieudmuc v_obj = new Select().From(DmucKieudmuc.Schema.TableName).Where(DmucKieudmuc.Columns.MaLoai).IsEqualTo(Maloai).ExecuteSingle<DmucKieudmuc>();
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
        public ActionResult isExistedRecord4Update(string MaMoi, string MaCu)
        {
            try
            {
                DmucKieudmucCollection v_obj = new DmucKieudmucController().FetchByQuery(DmucKieudmuc.CreateQuery().AddWhere(DmucKieudmuc.Columns.MaLoai, Comparison.NotEquals, MaCu));
                List<DmucKieudmuc> q = (from p in v_obj
                                      where p.MaLoai == MaMoi
                                      select p).ToList<DmucKieudmuc>();
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

        public void UpdateList(DmucKieudmuc obj, string strOldCode,  ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord4Update(obj.MaLoai, strOldCode);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                int record = new Update(DmucKieudmuc.Schema)
                    .Set(DmucKieudmuc.Columns.MaLoai).EqualTo(obj.MaLoai)
                    .Set(DmucKieudmuc.Columns.TenLoai).EqualTo(obj.TenLoai)
                    .Set(DmucKieudmuc.Columns.MotaThem).EqualTo(obj.MotaThem)
                    .Set(DmucKieudmuc.Columns.TrangThai).EqualTo(obj.TrangThai)
                    .Set(DmucKieudmuc.Columns.NgaySua).EqualTo(obj.NgaySua)
                    .Set(DmucKieudmuc.Columns.NguoiSua).EqualTo(obj.NguoiSua)
                    .Where(DmucKieudmuc.Columns.Id).IsEqualTo(obj.Id).Execute();
                if (record > 0)
                {
                    //Update trong bảng danh mục
                    new Update(DmucChung.Schema).Set(DmucChung.Columns.Loai).EqualTo(obj.MaLoai)
                        .Where(DmucChung.Columns.Loai).IsEqualTo(strOldCode).Execute();
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
        public ActionResult DeleteList(int ID, string Maloai)
        {
            try
            {
                if (isRecordinUsed(Maloai)) return ActionResult.DataHasUsedinAnotherTable;
                int record = new Delete().From(DmucKieudmuc.Schema).Where(DmucKieudmuc.Columns.Id).IsEqualTo(ID).Execute();
                return ActionResult.Success;
            }
            catch
            {
                return ActionResult.Exception;
            }
        }
        public bool isRecordinUsed(string Maloai)
        {
            try
            {
                DmucChung v_obj = new Select().From(DmucChung.Schema.TableName).Where(DmucChung.Columns.Ma).IsEqualTo(Maloai).ExecuteSingle<DmucChung>();
                return v_obj != null;
            }
            catch
            {
                return false;
            }
        }
       
    }
}
