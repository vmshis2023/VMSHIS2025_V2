using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using NLog;
namespace VNS.HIS.NGHIEPVU.THUOC
{

    public class ChotThuoc
    {
        private NLog.Logger log;
        public ChotThuoc()
        {
            log = NLog.LogManager.GetCurrentClassLogger();
        }
        public ActionResult CHOT_CAPPHAT(List<int> lstIDDonthuoc,Int16 id_kho, DateTime Ngay_Chot)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TLichsuChotthuoc _newItem = new TLichsuChotthuoc();
                        _newItem.NgayChot = Ngay_Chot;
                        _newItem.NguoiChot = globalVariables.UserName;
                        _newItem.Noitru = 0;
                        _newItem.IsNew = true;
                        _newItem.Save();
                        object obj = TLichsuChotthuoc.CreateQuery().GetMax("ID_CHOT");
                        TPhieuXuatthuocBenhnhanCollection vCollection = new TPhieuXuatthuocBenhnhanCollection();
                        v_intResult = new Update(TPhieuXuatthuocBenhnhan.Schema.TableName)
                                .Set(TPhieuXuatthuocBenhnhan.NgayChotColumn).EqualTo(Ngay_Chot)
                                .Set(TPhieuXuatthuocBenhnhan.IdChotColumn).EqualTo(_newItem.IdChot)//Utility.Int32Dbnull(obj, -1))
                                .Set(TPhieuXuatthuocBenhnhan.NgayHuychotColumn).EqualTo(null)
                                 .Set(TPhieuXuatthuocBenhnhan.NguoiHuychotColumn).EqualTo(string.Empty)
                                 .Set(TPhieuXuatthuocBenhnhan.LydoHuychotColumn).EqualTo(string.Empty)
                                .Where(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNull()
                                .And(TPhieuXuatthuocBenhnhan.IdDonthuocColumn).In(lstIDDonthuoc)
                                .And(TPhieuXuatthuocBenhnhan.IdKhoColumn).IsEqualTo(id_kho)
                                .Execute();

                        new Update(KcbDonthuoc.Schema.TableName).Set(KcbDonthuoc.NgayChotColumn).EqualTo(Ngay_Chot)
                                .Set(KcbDonthuoc.IdChotColumn).EqualTo(_newItem.IdChot )//Utility.Int32Dbnull(obj, -1))
                                .Set(KcbDonthuoc.NgayHuychotColumn).EqualTo(null)
                                 .Set(KcbDonthuoc.NguoiHuychotColumn).EqualTo(string.Empty)
                                 .Set(KcbDonthuoc.LydoHuychotColumn).EqualTo(string.Empty)
                                .Where(KcbDonthuoc.NgayChotColumn).IsNull()
                                .And(KcbDonthuoc.IdDonthuocColumn).In(lstIDDonthuoc)
                                .Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult HUYCHOT_CAPPHAT(List<int> lstIDDonthuoc, Int16 id_kho, DateTime ngayhuy, string lydohuy)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TPhieuXuatthuocBenhnhanCollection vCollection = new Select()
                            .From(TPhieuXuatthuocBenhnhan.Schema)
                            .Where(TPhieuXuatthuocBenhnhan.IdDonthuocColumn).In(lstIDDonthuoc)
                            .And(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNotNull()
                            .And(TPhieuXuatthuocBenhnhan.IdKhoColumn).IsEqualTo(id_kho)
                            .ExecuteAsCollection<TPhieuXuatthuocBenhnhanCollection>();
                        List<int> lstIdchot = (from p in vCollection
                                               select Utility.Int32Dbnull(p.IdChot, -1)).ToList<int>();
                        foreach (TPhieuXuatthuocBenhnhan _item in vCollection)
                        {
                               new Update(TPhieuXuatthuocBenhnhan.Schema.TableName)
                                .Set(TPhieuXuatthuocBenhnhan.NgayChotColumn).EqualTo(null)
                                .Set(TPhieuXuatthuocBenhnhan.NgayHuychotColumn).EqualTo(ngayhuy)
                                .Set(TPhieuXuatthuocBenhnhan.LydoHuychotColumn).EqualTo(lydohuy)
                                .Set(TPhieuXuatthuocBenhnhan.NguoiHuychotColumn).EqualTo(globalVariables.UserName)
                                .Set(TPhieuXuatthuocBenhnhan.IdChotColumn).EqualTo(null)
                                .Where(TPhieuXuatthuocBenhnhan.IdPhieuColumn).IsEqualTo(_item.IdPhieu)
                                .Execute();

                               new Update(KcbDonthuoc.Schema.TableName)
                                   .Set(KcbDonthuoc.NgayChotColumn).EqualTo(null)
                                   .Set(KcbDonthuoc.NgayHuychotColumn).EqualTo(ngayhuy)
                                   .Set(KcbDonthuoc.LydoHuychotColumn).EqualTo(lydohuy)
                                   .Set(KcbDonthuoc.NguoiHuychotColumn).EqualTo(globalVariables.UserName)
                                   .Set(KcbDonthuoc.IdChotColumn).EqualTo(null)
                                   .Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(_item.IdDonthuoc)
                                   .Execute();
                        }
                        //int itemremains = new Select()
                        //    .From(TPhieuXuatthuocBenhnhan.Schema)
                        //    .Where(TPhieuXuatthuocBenhnhan.IdChotColumn).In(lstIdchot)
                        //    .And(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNotNull()
                        //    .And(TPhieuXuatthuocBenhnhan.IdKhoColumn).IsEqualTo(id_kho)
                        //    .ExecuteAsCollection<TPhieuXuatthuocBenhnhanCollection>().Count;
                        //if (itemremains <= 0)
                        //    new Delete().From(TLichsuChotthuoc.Schema).Where(TLichsuChotthuoc.IdChotColumn).In(lstIdchot).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult LINH_THUOC(int Idcapphat, List<int> lstIDDonthuoc, DateTime Ngay_linh)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        v_intResult = new Update(TPhieuCapphatChitiet.Schema.TableName)
                                .Set(TPhieuCapphatChitiet.DaLinhColumn).EqualTo(1)
                                .Set(TPhieuCapphatChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                .Set(TPhieuCapphatChitiet.Columns.NgaySua).EqualTo(DateTime.Now)
                                .Where(TPhieuCapphatChitiet.Columns.IdCapphat).IsEqualTo(Idcapphat)
                                .And(TPhieuCapphatChitiet.IdDonthuocColumn).In(lstIDDonthuoc)
                                .And(TPhieuCapphatChitiet.DaLinhColumn).IsEqualTo(0)
                                .Execute();

                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult HUY_LINHTHUOC(List<int> lstIDDonthuoc, DateTime ngayhuy, string lydohuy)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TPhieuXuatthuocBenhnhanCollection vCollection = new Select()
                            .From(TPhieuXuatthuocBenhnhan.Schema)
                            .Where(TPhieuXuatthuocBenhnhan.IdDonthuocColumn).In(lstIDDonthuoc)
                            .And(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNotNull()
                            .ExecuteAsCollection<TPhieuXuatthuocBenhnhanCollection>();
                        List<int> lstIdchot = (from p in vCollection
                                               select Utility.Int32Dbnull(p.IdChot, -1)).ToList<int>();
                        foreach (TPhieuXuatthuocBenhnhan _item in vCollection)
                        {
                            new Update(TPhieuXuatthuocBenhnhan.Schema.TableName)
                             .Set(TPhieuXuatthuocBenhnhan.NgayChotColumn).EqualTo(null)
                             .Set(TPhieuXuatthuocBenhnhan.NgayHuychotColumn).EqualTo(ngayhuy)
                             .Set(TPhieuXuatthuocBenhnhan.LydoHuychotColumn).EqualTo(lydohuy)
                             .Set(TPhieuXuatthuocBenhnhan.NguoiHuychotColumn).EqualTo(globalVariables.UserName)
                             .Set(TPhieuXuatthuocBenhnhan.IdChotColumn).EqualTo(null)
                             .Where(TPhieuXuatthuocBenhnhan.IdPhieuColumn).IsEqualTo(_item.IdPhieu)
                             .Execute();

                            new Update(KcbDonthuoc.Schema.TableName)
                                .Set(KcbDonthuoc.NgayChotColumn).EqualTo(null)
                                .Set(KcbDonthuoc.NgayHuychotColumn).EqualTo(ngayhuy)
                                .Set(KcbDonthuoc.LydoHuychotColumn).EqualTo(lydohuy)
                                .Set(KcbDonthuoc.NguoiHuychotColumn).EqualTo(globalVariables.UserName)
                                .Set(KcbDonthuoc.IdChotColumn).EqualTo(null)
                                .Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(_item.IdDonthuoc)
                                .Execute();
                        }
                        int itemremains = new Select()
                            .From(TPhieuXuatthuocBenhnhan.Schema)
                            .Where(TPhieuXuatthuocBenhnhan.IdChotColumn).In(lstIdchot)
                            .And(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNotNull()
                            .ExecuteAsCollection<TPhieuXuatthuocBenhnhanCollection>().Count;
                        if (itemremains <= 0)
                            new Delete().From(TLichsuChotthuoc.Schema).Where(TLichsuChotthuoc.IdChotColumn).In(lstIdchot).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult CHOT_CAPPHAT_NOITRU(int IdCapphat, DateTime Ngay_Chot)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TLichsuChotthuoc _newItem = new TLichsuChotthuoc();
                        _newItem.NgayChot = Ngay_Chot;
                        _newItem.NguoiChot = globalVariables.UserName;
                        _newItem.Noitru = 1;
                        _newItem.IsNew = true;
                        _newItem.Save();
                        //object obj = TLichsuChotthuoc.CreateQuery().GetMax("ID_CHOT");
                        v_intResult = new Update(TPhieuCapphatNoitru.Schema.TableName)
                               .Set(TPhieuCapphatNoitru.NgayChotColumn).EqualTo(Ngay_Chot)
                               .Set(TPhieuCapphatNoitru.NguoiChotColumn).EqualTo(globalVariables.UserName)
                                .Set(TPhieuCapphatNoitru.IdChotColumn).EqualTo(_newItem.IdChot)//Utility.Int32Dbnull(obj, -1))
                                .Set(TPhieuCapphatNoitru.NgayHuychotColumn).EqualTo(null)
                                 .Set(TPhieuCapphatNoitru.NguoiHuychotColumn).EqualTo(string.Empty)
                                 .Set(TPhieuCapphatNoitru.LydoHuychotColumn).EqualTo(string.Empty)
                                .Where(TPhieuCapphatNoitru.NgayChotColumn).IsNull()
                                .And(TPhieuCapphatNoitru.Columns.IdCapphat).IsEqualTo(IdCapphat)
                                .Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult HUYCHOT_CAPPHAT_NOITRU(int IdCapphat, DateTime ngayhuy, string lydohuy)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TPhieuCapphatNoitruCollection vCollection = new Select()
                            .From(TPhieuCapphatNoitru.Schema)
                            .Where(TPhieuCapphatNoitru.IdCapphatColumn).In(IdCapphat)
                            .And(TPhieuCapphatNoitru.NgayChotColumn).IsNotNull()
                            .ExecuteAsCollection<TPhieuCapphatNoitruCollection>();
                        List<int> lstIdchot = (from p in vCollection
                                               select Utility.Int32Dbnull(p.IdChot, -1)).ToList<int>();
                        foreach (TPhieuCapphatNoitru _item in vCollection)
                        {
                            new Update(TPhieuCapphatNoitru.Schema.TableName)
                             .Set(TPhieuCapphatNoitru.NgayChotColumn).EqualTo(null)
                             .Set(TPhieuCapphatNoitru.NguoiChotColumn).EqualTo(null)
                             .Set(TPhieuCapphatNoitru.NgayHuychotColumn).EqualTo(ngayhuy)
                             .Set(TPhieuCapphatNoitru.LydoHuychotColumn).EqualTo(lydohuy)
                             .Set(TPhieuCapphatNoitru.NguoiHuychotColumn).EqualTo(globalVariables.UserName)
                             .Set(TPhieuCapphatNoitru.IdChotColumn).EqualTo(null)
                             .Where(TPhieuCapphatNoitru.IdCapphatColumn).IsEqualTo(_item.IdCapphat)
                             .Execute();

                           
                        }
                        //int itemremains = new Select()
                        //    .From(TPhieuCapphatNoitru.Schema)
                        //    .Where(TPhieuCapphatNoitru.IdChotColumn).In(lstIdchot)
                        //    .And(TPhieuCapphatNoitru.NgayChotColumn).IsNotNull()
                        //    .ExecuteAsCollection<TPhieuCapphatNoitruCollection>().Count;
                        //if (itemremains <= 0)
                        //    new Delete().From(TLichsuChotthuoc.Schema).Where(TLichsuChotthuoc.IdChotColumn).In(lstIdchot).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }

      
    }
}
