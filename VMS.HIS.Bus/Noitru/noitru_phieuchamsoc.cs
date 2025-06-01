using NLog;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VNS.Libs;
using VMS.HIS.DAL;
namespace newBus.Noitru
{
    public class PhieuChamSoc
    {
        private Logger log;

        public PhieuChamSoc()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// hàm thực hiện việc thêm mới phiếu chăm sóc
        /// </summary>
        /// <param name="objThongtinGoiDvuBnhan"></param>
        /// <returns></returns>
        public ActionResult LuuPhieuChamSoc(NoitruPhieuchamsoc objPhieuchamsoc)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(NoitruPhieuchamsoc.Schema)
                            .Where(NoitruPhieuchamsoc.Columns.Id).IsEqualTo(objPhieuchamsoc.Id);
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            objPhieuchamsoc.IsNew = true;
                            objPhieuchamsoc.Save();
                        }
                        else
                        {
                            objPhieuchamsoc.MarkOld();
                            objPhieuchamsoc.IsLoaded = true;
                            objPhieuchamsoc.NguoiSua = globalVariables.UserName;
                            objPhieuchamsoc.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                            objPhieuchamsoc.Save();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien LuuPhieuChamSoc {0}:", ex.ToString());
                return ActionResult.Error;
                //throw;
            }
        }

        /// <summary>
        /// hàm thực hiên xóa thông tin phiêu chăm sóc
        /// </summary>
        /// <param name="objPhieuchamsoc"></param>
        /// <returns></returns>
        public ActionResult XoaPhieuChamSoc(NoitruPhieuchamsoc objPhieuchamsoc)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Delete().From(NoitruPhieutheodoiChucnangsong.Schema)
                            .Where(NoitruPhieutheodoiChucnangsong.Columns.IdPhieuchamsoc).IsEqualTo(objPhieuchamsoc.Id).Execute();
                        NoitruPhieuchamsoc.Delete(objPhieuchamsoc.Id);
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien XoaPhieuChamSoc {0}:", ex.ToString());
                return ActionResult.Error;
                //throw;
            }
        }

        /// <summary>
        /// hàm thực hiện việc lưu thông tin chi tiết phiếu chăm sóc
        /// </summary>
        /// <param name="objPhieutheodoichucnangsong"></param>
        /// <returns></returns>
        public ActionResult LuuChiTietPhieuChamSoc(NoitruPhieutheodoiChucnangsong objPhieutheodoichucnangsong)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Delete().From(NoitruPhieutheodoiChucnangsong.Schema)
                            .Where(NoitruPhieutheodoiChucnangsong.Columns.Id).IsEqualTo(objPhieutheodoichucnangsong.Id).Execute();

                        objPhieutheodoichucnangsong.IsNew = true;
                        objPhieutheodoichucnangsong.Save();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien LuuChiTietPhieuChamSoc {0}:", ex.ToString());
                return ActionResult.Error;
                //throw;
            }
        }

        /// <summary>
        /// hàm thực hiện việc lưu thông tin chi tiết phiếu chăm sóc
        /// </summary>
        /// <param name="objPhieutheodoichucnangsong"></param>
        /// <returns></returns>
        public ActionResult LuuChiTietPhieuTheoDoi(NoitruPhieutheodoiChucnangsong objPhieutheodoichucnangsong)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(NoitruPhieutheodoiChucnangsong.Schema)
                            .Where(NoitruPhieutheodoiChucnangsong.Columns.Id).IsEqualTo(objPhieutheodoichucnangsong.Id);
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            objPhieutheodoichucnangsong.IsNew = true;
                            objPhieutheodoichucnangsong.Save();
                        }
                        else
                        {
                            objPhieutheodoichucnangsong.Save();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien LuuChiTietPhieuTheoDoi {0}:", ex.ToString());
                return ActionResult.Error;
                //throw;
            }
        }

        public ActionResult XoaChiTietPhieuTheoDoi(int IDPhieuChiTiet)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        NoitruPhieutheodoiChucnangsong.Delete(IDPhieuChiTiet);
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien XoaChiTietPhieuTheoDoi {0}:", ex.ToString());
                return ActionResult.Error;
                //throw;
            }
        }
        public ActionResult LuuChiTieNoitruPhieuthuphanungthuoc(NoitruPhieuthuphanungthuoc objPhieuThuPhanUngThuoc)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(NoitruPhieuthuphanungthuoc.Schema)
                            .Where(NoitruPhieuthuphanungthuoc.Columns.IdPhieu).IsEqualTo(objPhieuThuPhanUngThuoc.IdPhieu);
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            objPhieuThuPhanUngThuoc.IsNew = true;
                            objPhieuThuPhanUngThuoc.Save();
                        }
                        else
                        {
                            objPhieuThuPhanUngThuoc.Save();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien luu thong tin phieu thu pphan ung thuoc {0}:", ex.ToString());
                return ActionResult.Error;
                //throw;
            }
        }
        public ActionResult XoaChiTieNoitruPhieuthuphanungthuoc(int IDPhieuThu)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        NoitruPhieuthuphanungthuoc.Delete(IDPhieuThu);
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien XoaChiTieNoitruPhieuthuphanungthuoc {0}:", ex.ToString());
                return ActionResult.Error;
                //throw;
            }
        }
    }
}
