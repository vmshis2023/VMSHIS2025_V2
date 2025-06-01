using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VMS.HIS.DAL;
using SubSonic;
using VNS.Libs;

namespace VMS.HIS.NGHIEPVU
{
   public class dmuc_doitac_busrule
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
                DataTable dtData = new DmucDoitacController().FetchAll().ToDataTable();
                dtData.TableName = DmucDoitac.Schema.TableName;
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
        public void InsertList(DmucDoitac obj,  ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord(obj.MaDoitac);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                obj.IsNew = true;
                obj.Save();
                obj.Id =Utility.Int32Dbnull( DmucDoitac.CreateQuery().GetMax(DmucDoitac.IdColumn.ColumnName),0);
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
                DmucDoitac v_obj = new Select().From(DmucDoitac.Schema.TableName).Where(DmucDoitac.Columns.MaDoitac).IsEqualTo(Maloai).ExecuteSingle<DmucDoitac>();
                if (v_obj != null) return ActionResult.ExistedRecord;
                return ActionResult.Success;
            }
            catch
            {
                return ActionResult.Exception;
            }
        }
        public DmucDoitac GetKieuDanhMuc(string MaLoai)
        {
            try
            {
                DmucDoitac v_obj = new Select().From(DmucDoitac.Schema.TableName).Where(DmucDoitac.Columns.MaDoitac).IsEqualTo(MaLoai).ExecuteSingle<DmucDoitac>();
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
                DmucDoitacCollection v_obj = new DmucDoitacController().FetchByQuery(DmucDoitac.CreateQuery().AddWhere(DmucDoitac.Columns.MaDoitac, Comparison.NotEquals, MaCu));
                List<DmucDoitac> q = (from p in v_obj
                                      where p.MaDoitac == MaMoi
                                      select p).ToList<DmucDoitac>();
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

        public void UpdateList(DmucDoitac obj, string strOldCode,  ref string ActResult)
        {
            try
            {
                ActionResult _act = isExistedRecord4Update(obj.MaDoitac, strOldCode);
                if (_act == ActionResult.ExistedRecord || _act == ActionResult.Exception)
                {
                    ActResult = _act.ToString();
                    return;
                }
                int record = new Update(DmucDoitac.Schema)
                    .Set(DmucDoitac.Columns.MaDoitac).EqualTo(obj.MaDoitac)
                    .Set(DmucDoitac.Columns.TenDoitac).EqualTo(obj.TenDoitac)
                    .Set(DmucDoitac.Columns.MotaThem).EqualTo(obj.MotaThem)
                    .Set(DmucDoitac.Columns.MaNguongioithieu).EqualTo(obj.MaNguongioithieu)
                    .Set(DmucDoitac.Columns.TrangThai).EqualTo(obj.TrangThai)
                    .Set(DmucDoitac.Columns.Stt).EqualTo(obj.Stt)
                    .Set(DmucDoitac.Columns.TrangthaiMacdinh).EqualTo(obj.TrangthaiMacdinh)
                    .Set(DmucDoitac.Columns.NgaySua).EqualTo(obj.NgaySua)
                    .Set(DmucDoitac.Columns.NguoiSua).EqualTo(obj.NguoiSua)
                    .Where(DmucDoitac.Columns.Id).IsEqualTo(obj.Id).Execute();
                if (record > 0)
                {
                    //Update trong bảng danh mục
                    new Update(DmucChung.Schema).Set(DmucChung.Columns.Loai).EqualTo(obj.MaDoitac)
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
                int record = new Delete().From(DmucDoitac.Schema).Where(DmucDoitac.Columns.Id).IsEqualTo(ID).Execute();
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
                DataTable dtData = new Select().From(KcbLuotkham.Schema.TableName).Where(KcbLuotkham.Columns.MaDoitac).IsEqualTo(Maloai).ExecuteDataSet().Tables[0];
                return dtData != null && dtData.Rows.Count>0;
            }
            catch
            {
                return false;
            }
        }
       
    }
}
