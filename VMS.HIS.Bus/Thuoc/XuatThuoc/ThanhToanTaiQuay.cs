using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VNS.Libs;
using SubSonic;
using VNS.HIS.DAL;
using System.Transactions;
using NLog;
using VNS.HIS.BusRule.Classes;
namespace VNS.HIS.NGHIEPVU.THUOC
{
  public  class ThanhToanTaiQuay
    {

        private Logger log;
        public ThanhToanTaiQuay()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin vào 
        /// các bảng chức năng
        /// </summary>
        /// <param name="objPayment"></param>
        /// <param name="objPaymentDetail"></param>
        private void UpdateTrangThaiBangChucNang(KcbThanhtoan objPayment, KcbThanhtoanChitiet objPaymentDetail)
        {
            switch (objPaymentDetail.IdLoaithanhtoan)
            {
                case 1:
                    new Update(KcbChidinhPhongkham.Schema)
                        .Set(KcbChidinhPhongkham.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                        .Set(KcbChidinhPhongkham.Columns.TrangthaiThanhtoan).EqualTo(1)
                        .Set(KcbChidinhPhongkham.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                        .Where(KcbChidinhPhongkham.Columns.IdKham).IsEqualTo(objPaymentDetail.IdPhieu).Execute();
                    break;
                case 2:
                    new Update(KcbChidinhclsChitiet.Schema)
                        .Set(KcbChidinhclsChitiet.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                        .Set(KcbChidinhclsChitiet.Columns.TinhtrangThanhtoan).EqualTo(1)
                        .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                        .Where(KcbChidinhclsChitiet.Columns.IdChidinhChitiet).IsEqualTo(objPaymentDetail.IdPhieuChitiet).Execute();
                    break;
                case 3:
                    new Update(KcbDonthuocChitiet.Schema)
                        .Set(KcbDonthuocChitiet.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                        .Set(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
                        .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietDonthuoc).IsEqualTo(objPaymentDetail.IdPhieuChitiet).Execute();
                    break;
                case 5:
                    new Update(KcbDonthuocChitiet.Schema)
                        .Set(KcbDonthuocChitiet.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                        .Set(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
                        .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietDonthuoc).IsEqualTo(objPaymentDetail.IdPhieuChitiet).Execute();
                    break;
                case 4:
                    //new Update(TPatientDept.Schema)
                    //    .Set(TPatientDept.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                    //    .Set(TPatientDept.Columns.TrangthaiThanhtoan).EqualTo(1)
                    //    .Set(TPatientDept.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                    //    .Where(TPatientDept.Columns.PatientDeptId).IsEqualTo(objPaymentDetail.Id).Execute();
                    break;
                case 0:
                    new Update(KcbChidinhclsChitiet.Schema)
                        .Set(KcbChidinhclsChitiet.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                        .Set(KcbChidinhclsChitiet.Columns.TinhtrangThanhtoan).EqualTo(1)
                        .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                        .Where(KcbChidinhclsChitiet.Columns.IdChidinhChitiet).IsEqualTo(objPaymentDetail.IdPhieuChitiet)
                        .Execute();
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện thanh toán xử lý thông tin của phần 
        /// </summary>
        /// <param name="v_decTotalPrice"></param>
        /// <param name="v_decDiscountRate"></param>
        /// <param name="vDiscountType"></param>
        /// <param name="v_SalaryBasic"></param>
        /// <param name="vClinicCode"></param>
        /// <param name="IsSpecialDiscount"></param>
        /// <returns></returns>
        public decimal LayThongPtramBHYT(decimal v_decTotalPrice, TPatientExam objPatientExam)
        {
            decimal decDiscountTotalMoney = 0;
            if (!string.IsNullOrEmpty(objPatientExam.InsClinicCode))
            {
                ///thực hiện xem có đúng tuyến không
                if (objPatientExam.CorrectLine == 1)
                {
                    if (objPatientExam.InsObjectCodeNumber.ToString() == "1" || objPatientExam.InsObjectCodeNumber.ToString() == "2")
                    {
                        decDiscountTotalMoney = 0;
                    }
                    else
                    {
                        if (v_decTotalPrice > objPatientExam.SalaryBasic * 15 / 100)
                        {
                            decDiscountTotalMoney = v_decTotalPrice * (Utility.DecimaltoDbnull(objPatientExam.DiscountRate) / 100);
                        }
                        else
                        {
                            decDiscountTotalMoney = 0;
                        }
                    }
                }
                else
                {
                    decDiscountTotalMoney = v_decTotalPrice * (Utility.DecimaltoDbnull(objPatientExam.DiscountRate) / 100);

                }


            }
            else
            {
                decDiscountTotalMoney = v_decTotalPrice * (Utility.DecimaltoDbnull(objPatientExam.DiscountRate) / 100); ;
            }

            return decDiscountTotalMoney;
        }
        public bool IsBaohiem(TPatientExam objPatientExam)
        {
            LObjectType objObjectType = LObjectType.FetchByID(objPatientExam.ObjectTypeId);
            if (objObjectType.ObjectTypeType == 0) return true;
            else
            {
                return false;
            }
        }
        /// <summary>
        /// hàm thục hiện tính toán thông tin 
        /// </summary>
        /// <param name="v_decTotalPrice"></param>
        /// <param name="objPatientExam"></param>
        /// <param name="PtramBHYT"></param>
        /// <returns></returns>
        public decimal LayThongPtramBHYT(decimal v_decTotalPrice, TPatientExam objPatientExam, ref  decimal PtramBHYT)
        {
            decimal decDiscountTotalMoney = 0;
            SqlQuery q;
            if (!string.IsNullOrEmpty(objPatientExam.InsClinicCode) && IsBaohiem(objPatientExam))
            {
                ///thực hiện xem có đúng tuyến không

                if (objPatientExam.CorrectLine == 1)
                {
                    if (objPatientExam.InsObjectCodeNumber.ToString() == "1" || objPatientExam.InsObjectCodeNumber.ToString() == "2")
                    {
                        decDiscountTotalMoney = 0;
                        PtramBHYT = 100;
                        log.Info("Benh nhan tuong ung voi muc =" + objPatientExam.InsObjectCodeNumber);
                    }
                    else
                    {
                        switch (globalVariables.gv_BenhVienTuyen)
                        {
                            case "TUYEN1":
                                if (v_decTotalPrice >= objPatientExam.SalaryBasic * 15 / 100)
                                {
                                    log.Info(string.Format("Neu ma benh nhan lon hon muc luong co ban={0} thi bat dau tinh gia", BusinessHelper.GetLuongCoBan()));
                                    q = new Select().From(LInsuranceObject.Schema)
                                       .Where(LInsuranceObject.Columns.ObjectTypeId).IsEqualTo(objPatientExam.ObjectTypeId)
                                       .And(LInsuranceObject.Columns.InsObjectCode).IsEqualTo(objPatientExam.InsObjectCode);
                                    LInsuranceObject objInsuranceObject = q.ExecuteSingle<LInsuranceObject>();
                                    if (objInsuranceObject != null)
                                    {
                                        PtramBHYT = Utility.DecimaltoDbnull(objInsuranceObject.Percent, 0);
                                        decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.DecimaltoDbnull(objInsuranceObject.Percent, 0)) / 100;
                                        log.Info("bat dau chi khau theo doi tuong muc tien =" + decDiscountTotalMoney + " cua benh nhan co ma Patient_Code=" + objPatientExam.PatientCode);
                                    }

                                }
                                else
                                {

                                    PtramBHYT = 100;
                                    decDiscountTotalMoney = 0;
                                    log.Info("Benh nhan dc mien phi hoan toan, voi muc chiet khau =0 tuong ung voi Patient_Code=" + objPatientExam.PatientCode);
                                }
                                break;
                            case "TW":
                                log.Info("Lấy thông tin của tuyến trung ương");
                                log.Info(string.Format("Neu ma benh nhan lon hon muc luong co ban={0} thi bat dau tinh gia", BusinessHelper.GetLuongCoBan()));
                                q = new Select().From(LInsuranceObject.Schema)
                                   .Where(LInsuranceObject.Columns.ObjectTypeId).IsEqualTo(objPatientExam.ObjectTypeId)
                                   .And(LInsuranceObject.Columns.InsObjectCode).IsEqualTo(objPatientExam.InsObjectCode);
                                LInsuranceObject objInsuranceObjectTW = q.ExecuteSingle<LInsuranceObject>();
                                if (objInsuranceObjectTW != null)
                                {
                                    PtramBHYT = Utility.DecimaltoDbnull(objInsuranceObjectTW.Percent, 0);
                                    decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.DecimaltoDbnull(objInsuranceObjectTW.Percent, 0)) / 100;
                                    log.Info("bat dau chi khau theo doi tuong muc tien =" + decDiscountTotalMoney + " cua benh nhan co ma Patient_Code=" + objPatientExam.PatientCode);
                                }
                                break;
                            default:
                                if (v_decTotalPrice >= objPatientExam.SalaryBasic * 15 / 100)
                                {
                                    log.Info(string.Format("Neu ma benh nhan lon hon muc luong co ban={0} thi bat dau tinh gia", BusinessHelper.GetLuongCoBan()));
                                    q = new Select().From(LInsuranceObject.Schema)
                                       .Where(LInsuranceObject.Columns.ObjectTypeId).IsEqualTo(objPatientExam.ObjectTypeId)
                                       .And(LInsuranceObject.Columns.InsObjectCode).IsEqualTo(objPatientExam.InsObjectCode);
                                    LInsuranceObject objInsuranceObject = q.ExecuteSingle<LInsuranceObject>();
                                    if (objInsuranceObject != null)
                                    {
                                        PtramBHYT = Utility.DecimaltoDbnull(objInsuranceObject.Percent, 0);
                                        decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.DecimaltoDbnull(objInsuranceObject.Percent, 0)) / 100;
                                        log.Info("bat dau chi khau theo doi tuong muc tien =" + decDiscountTotalMoney + " cua benh nhan co ma Patient_Code=" + objPatientExam.PatientCode);
                                    }

                                }
                                else
                                {

                                    PtramBHYT = 100;
                                    decDiscountTotalMoney = 0;
                                    log.Info("Benh nhan dc mien phi hoan toan, voi muc chiet khau =0 tuong ung voi Patient_Code=" + objPatientExam.PatientCode);
                                }
                                break;

                        }


                    }


                }
                else
                {
                    ///Nếu là đối tượng trái tuyến thực hiện lấy % của trái tuyến
                    LObjectType objObjectType = LObjectType.FetchByID(objPatientExam.ObjectTypeId);
                    if (objObjectType != null)
                    {
                        decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.DecimaltoDbnull(objObjectType.DiscountDiscorrectLine)) / 100;
                        PtramBHYT = Utility.DecimaltoDbnull(objObjectType.DiscountDiscorrectLine, 0);

                    }

                }


            }
            else
            {

                LObjectType objObjectType = LObjectType.FetchByID(objPatientExam.ObjectTypeId);
                if (objObjectType != null)
                    decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.Int32Dbnull(objObjectType.DiscountCorrectLine, 0)) / 100; ;
                PtramBHYT = Utility.DecimaltoDbnull(objObjectType.DiscountCorrectLine, 0);

            }
            return decDiscountTotalMoney;
        }
        public ActionResult ThanhToanKeDonThuocTaiQuay(TPayment objPayment, TPatientExam objPatientExam, TPaymentDetail[] objArrPaymentDetail)
        {
            decimal PtramBHYT = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_TotalOrginPrice = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        ///lấy tổng số Payment của mang truyền vào của pay ment hiện tại
                        //v_TotalOrginPrice = SumOfPaymentDetail_NGOAITRU(objArrPaymentDetail);


                        TPaymentCollection paymentCollection =
                            new TPaymentController().FetchByQuery(
                                TPayment.CreateQuery().AddWhere(TPayment.Columns.PatientCode, Comparison.Equals,
                                                                objPatientExam.PatientCode).AND(
                                                                    TPayment.Columns.PatientId, Comparison.Equals,
                                                                    objPatientExam.PatientId).AND(
                                                                        TPayment.Columns.Status, Comparison.Equals, 0).
                                    AND(TPayment.Columns.KieuThanhToan, Comparison.Equals, 0).AND(
                                        TPayment.Columns.Status, Comparison.Equals, 0));


                        foreach (TPayment Payment in paymentCollection)
                        {
                            TPaymentDetailCollection paymentDetailCollection = new Select().From(TPaymentDetail.Schema)
                                .Where(TPaymentDetail.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                                .And(TPaymentDetail.Columns.IsCancel).IsEqualTo(0).ExecuteAsCollection
                                <TPaymentDetailCollection>();

                            foreach (TPaymentDetail paymentDetail in paymentDetailCollection)
                            {
                                if (paymentDetail.IsPayment == 0)
                                    v_TotalPaymentDetail += Utility.Int32Dbnull(paymentDetail.Quantity) *
                                                            Utility.DecimaltoDbnull(paymentDetail.OriginPrice);

                            }

                        }
                        ///lấy thông tin chiết khấu xem đã thực hiện chưa
                        LayThongPtramBHYT(v_TotalOrginPrice + v_TotalPaymentDetail, objPatientExam, ref PtramBHYT);
                        log.Info(string.Format("Thong tin chi khau {0}, voi ma Patient_Code{1}",
                                               PtramBHYT, objPatientExam.PatientCode));

                        ///hàm thực hiện việc xử lý lại thông tin 
                       // XuLyChiKhauDacBietBHYT(objPatientExam, PtramBHYT);
                        objPayment.DaIn = 0;
                        objPayment.KieuThanhToan = 0;
                        objPayment.NguoiIn = string.Empty;
                        objPayment.TrongGoi = 0;
                        objPayment.IpMacTao = BusinessHelper.GetMACAddress();
                        objPayment.IpMayTao = BusinessHelper.GetIP4Address();
                        objPayment.PaymentCode = BusinessHelper.GeneratePaymentCode(Convert.ToDateTime(objPayment.NgayThanhtoan), 0);
                        objPayment.IsNew = true;
                        objPayment.Save();
                        //StoredProcedure sp = SPs.KcbThanhtoanThemmoi(objPayment.IdThanhtoan, objPayment.PatientCode, objPayment.PatientId,
                        //                  objPayment.NgayThanhtoan, objPayment.StaffId, objPayment.Status,
                        //                  objPayment.CreatedBy, objPayment.CreatedDate, objPayment.ModifyDate,
                        //                  objPayment.ModifyBy, objPayment.PaymentCode, objPayment.KieuThanhToan,
                        //                  objPayment.DaIn, objPayment.NgayIn, objPayment.NgayTHop, objPayment.NguoiIn,
                        //                  objPayment.NguoiTHop, Utility.Int32Dbnull(objPayment.TrongGoi), objPayment.IpMayTao, objPayment.IpMacTao, globalVariables.MA_KHOA_THIEN);
                        //sp.Execute();
                        //objPayment.IdThanhtoan = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                        //objPayment.IdThanhtoan = Utility.Int32Dbnull(_QueryPayment.GetMax(TPayment.Columns.IdThanhtoan), -1);
                        log.Info("Lay ma thanh toan cua phan thanh toan Payment_ID={0}", objPayment.IdThanhtoan);
                        ///hàm thực hiện việc mảng thao tác mảng của chi tiết thanh toán

                        foreach (TPaymentDetail objPaymentDetail in objArrPaymentDetail)
                        {



                            log.Info("Thuc hien thanh cong cap nhap dich vu can lam sang ");
                            ///thanh toán phần thuốc);
                            if (THU_VIEN_CHUNG.LayMaDviLamViec() == "DETMAY")
                            {
                                if (objPaymentDetail.IdLoaithanhtoan == 3)
                                {
                                    new Update(TPrescription.Schema)
                                        .Set(TPrescription.Columns.TrangthaiThanhtoan).EqualTo(1)
                                        .Set(TPrescription.Columns.Status).EqualTo(2)///nếu =2 đối với đơn thuốc ngoại trú
                                        .Where(TPrescription.Columns.PresId).IsEqualTo(objPaymentDetail.Id).Execute();

                                }
                            }

                            log.Info("Thuc hien thanh cong cap nhap thuoc");
                            ///quần áo cho thuê);

                            log.Info("Cap nhap thong tin thanh cong cho phan giuong benh");
                            switch (BusinessHelper.GetThanhToan_TraiTuyen())
                            {
                                case "PHUTHU":
                                    if (objPaymentDetail.IdLoaithanhtoan == 1)
                                    {
                                        objPaymentDetail.SurchargePrice = 0;
                                    }
                                    break;


                            }
                            objPaymentDetail.NguoiTao = globalVariables.UserName;
                            objPaymentDetail.NoiTru = 0;
                            objPaymentDetail.TrongGoi = 0;
                            objPaymentDetail.IpMacTao = BusinessHelper.GetMACAddress();
                            objPaymentDetail.IpMayTao = BusinessHelper.GetIP4Address();
                            objPaymentDetail.IdThanhtoan = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                            objPaymentDetail.MaKieuTtoan = BusinessHelper.MaKieuThanhToan(Utility.Int32Dbnull(objPaymentDetail.IdLoaithanhtoan, -1));
                            objPaymentDetail.TienBnTra = Utility.DecimaltoDbnull(objPaymentDetail.DiscountPrice);
                            StoredProcedure spPaymentDetail = SPs.KcbThanhtoanThemchitiet(
                                objPaymentDetail.PaymentDetailId, objPaymentDetail.IdThanhtoan,
                                objPaymentDetail.Quantity, objPaymentDetail.OriginPrice,
                                objPaymentDetail.DiscountRate, Utility.DecimaltoDbnull(objPaymentDetail.DiscountPrice), 
                                Utility.DecimaltoDbnull(objPaymentDetail.TienBnTra),
                                objPaymentDetail.SurchargePrice, objPaymentDetail.Id,
                                objPaymentDetail.IdDetail, objPaymentDetail.ServiceId,
                                objPaymentDetail.ServiceDetailId, objPaymentDetail.IdLoaithanhtoan,
                                objPaymentDetail.IsCancel, objPaymentDetail.IsPayment,
                                objPaymentDetail.CancelBy, objPaymentDetail.CancelDate,
                                objPaymentDetail.DepartmentId, objPaymentDetail.DoctorAssignId,
                                objPaymentDetail.ThuTuIn, objPaymentDetail.DonViTinh,
                                objPaymentDetail.MaDv, objPaymentDetail.PTramBh,
                                objPaymentDetail.ServiceDetailName, Utility.sDbnull(objPaymentDetail.ServiceDetailName),
                                objPaymentDetail.MaKieuTtoan, 0, objPaymentDetail.TrongGoi, objPaymentDetail.IdGoiDvu,
                                objPaymentDetail.NoiTru, objPaymentDetail.NguoiTao, objPaymentDetail.IpMacTao, objPaymentDetail.IpMayTao);

                            spPaymentDetail.Execute();
                            objPaymentDetail.PaymentDetailId = Utility.Int32Dbnull(spPaymentDetail.OutputValues[0], -1);
                            UpdateTrangThaiBangChucNang(objPayment, objPaymentDetail);


                            log.Info("Thuc hien dua vao chen bang ghi cua phan ky dong");
                        }

                        if (objPatientExam.MaDoiTuong == "BHYT")
                        {
                            if (globalVariables.gv_BenhVienTuyen == "TW")
                            {
                                SqlQuery sqlQuery = new Select().From(TPaymentDetail.Schema)
                                    .Where(TPaymentDetail.Columns.IdThanhtoan).In(
                                        new Select(TPayment.Columns.IdThanhtoan).From(TPayment.Schema).Where(
                                            TPayment.Columns.PatientCode).IsEqualTo(
                                                objPatientExam.PatientCode).And(TPayment.Columns.PatientId).IsEqualTo(
                                                    objPatientExam.PatientId).And(TPayment.Columns.KieuThanhToan).
                                            IsEqualTo(0).
                                            And(TPayment.Columns.Status).IsEqualTo(0))
                                    .And(TPaymentDetail.Columns.IsCancel).IsEqualTo(0)
                                    .And(TPaymentDetail.Columns.IsPayment).IsEqualTo(0);

                                TPaymentDetailCollection objPaymentDetailCollection =
                                    sqlQuery.ExecuteAsCollection<TPaymentDetailCollection>();
                                decimal TongTien =
                                    Utility.DecimaltoDbnull(objPaymentDetailCollection.Sum(c => c.Quantity * c.OriginPrice));
                                LayThongPtramBHYT(TongTien, objPatientExam, ref PtramBHYT);
                                foreach (TPaymentDetail objPaymentDetail in objPaymentDetailCollection)
                                {
                                    decimal BHCT = Utility.DecimaltoDbnull(objPaymentDetail.OriginPrice * PtramBHYT / 100);
                                    decimal BNCT = Utility.DecimaltoDbnull(objPaymentDetail.OriginPrice - BHCT);
                                    new Update(TPaymentDetail.Schema)
                                        .Set(TPaymentDetail.Columns.PTramBh).EqualTo(PtramBHYT)
                                        .Set(TPaymentDetail.Columns.DiscountRate).EqualTo(BHCT)
                                        .Set(TPaymentDetail.Columns.DiscountPrice).EqualTo(BNCT)
                                        .Where(TPaymentDetail.Columns.PaymentDetailId).IsEqualTo(objPaymentDetail.PaymentDetailId).Execute();

                                }
                            }

                        }
                        SPs.KydongThemthongtinThanhtoanThem(objPatientExam.PatientCode, Utility.Int32Dbnull(objPatientExam.PatientId, -1)).
                           Execute();





                    }
                    scope.Complete();
                   // Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                    log.Info("Thuc hien thanh cong viec thanh toan");
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex.ToString());
                return ActionResult.Error;
            }

        }
    }
}
