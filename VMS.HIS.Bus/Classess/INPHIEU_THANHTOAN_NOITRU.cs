using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.Classess
{

    public class INPHIEU_THANHTOAN_NOITRU
    {
        DateTime _ngayinphieu;
        public INPHIEU_THANHTOAN_NOITRU(DateTime ngayinphieu)
       {
           this._ngayinphieu = ngayinphieu;
       }
        public INPHIEU_THANHTOAN_NOITRU()
       {
       }
       
        private KcbPhieuDct CreatePhieuDongChiTra(KcbLuotkham objLuotkham, DataTable m_dtPayment)
        {
            KcbPhieuDct objPhieuDct = new KcbPhieuDct();
            objPhieuDct.MaPhieuDct = Utility.sDbnull(THU_VIEN_CHUNG.TaomaDongChiTra(DateTime.Now));
            objPhieuDct.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
            objPhieuDct.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
            objPhieuDct.KieuThanhtoan = 0;
            objPhieuDct.NguoiTao = globalVariables.UserName;
            objPhieuDct.TrangthaiXml = 0;
            objPhieuDct.NgayTao = DateTime.Now;
            objPhieuDct.IpMaytao = globalVariables.gv_strIPAddress;
            objPhieuDct.MatheBhyt = Utility.sDbnull(objLuotkham.MatheBhyt, "");
            objPhieuDct.MaKhoaThuchien = Utility.sDbnull(objLuotkham.MaKhoaThuchien, "");
            objPhieuDct.TenMaytao = globalVariables.gv_strComputerName;
            objPhieuDct.TongTien = (decimal)m_dtPayment.Compute("SUM(TT_DCT)", "1=1");// Utility.DecimaltoDbnull(txtSoTienGoc.Text);
            objPhieuDct.BnhanChitra = (decimal)m_dtPayment.Compute("SUM(BN_CT)", "1=1"); //Utility.DecimaltoDbnull(txtTienBNCT.Text);
            objPhieuDct.BhytChitra = (decimal)m_dtPayment.Compute("SUM(BHYT_CT)", "1=1"); //Utility.DecimaltoDbnull(txtTienBHCT.Text);
            objPhieuDct.TrangthaiXml = 0;
            return objPhieuDct;

        }
        private void GetChanDoanPhu(string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> lstICD = IDC_Phu.Split(',').ToList();
                //DmucBenhCollection _list =
                //    new DmucBenhController().FetchByQuery(
                //        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                // Cách này cho hạn chế phi vào DB 
                foreach (var row in lstICD)
                {
                    var item = (from p in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                where row != null && p[DmucBenh.Columns.MaBenh].Equals(row)
                                select p).FirstOrDefault();
                    if (item != null)
                    {
                        ICD_Name += item["ten_benh"].ToString() + ";";
                        ICD_Code += item["ma_benh"].ToString() + ";";
                    }
                }
                //foreach (DmucBenh _item in _list)
                //{
                //    ICD_Name += _item.TenBenh + ";";
                //    ICD_Code += _item.MaBenh + ";";
                //}
                //_list =
                //    new DmucBenhController().FetchByQuery(
                //        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                //foreach (DmucBenh _item in _list)
                //{
                //    ICD_Name += _item.TenBenh + ";";
                //    ICD_Code += _item.MaBenh + ";";
                //}
                if (ICD_Name.Trim() != "") ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                if (ICD_Code.Trim() != "") ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
            }
            catch
            {
            }
        }
        public bool InPhoiBHYT(KcbLuotkham objLuotkham, DataTable m_dtPayment, DateTime ngayIn)
        {
            try
            {
                KcbPhieuDct objPhieuDct = CreatePhieuDongChiTra(objLuotkham, m_dtPayment);
                objPhieuDct.NgayTao = ngayIn;
                ActionResult actionResult = new KCB_THANHTOAN().UpdatePhieuDCT(objPhieuDct, objLuotkham);
                if (actionResult == ActionResult.Success) //Tránh trường hợp in ra phôi mà ko đẩy vào CSDL
                {
                    DataTable mDtReportPhieuThu =
                         new KCB_THANHTOAN().NoiTruLaythongtinInphoiBHYT(-1, Utility.sDbnull(objLuotkham.MaLuotkham, ""),
                             Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1), 0);
                    THU_VIEN_CHUNG.CreateXML(mDtReportPhieuThu);

                    //load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán)
                    if (mDtReportPhieuThu.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("Không tìm thấy dữ liệu để in phôi BHYT ", "Thông báo");
                        return false;
                    }
                    string ICD_Name = "";
                    string ICD_Code = "";
                    if (mDtReportPhieuThu.Rows.Count > 0)
                        GetChanDoanPhu(Utility.sDbnull(mDtReportPhieuThu.Rows[0]["ma_benhphu"], ""), ref ICD_Name, ref ICD_Code);
                    //foreach (DataRow dr in m_dtReportPhieuThu.Rows)
                    //{
                    //    dr["chuan_doanphu"] = Utility.sDbnull(dr["chuan_doanphu"]).Trim() == ""
                    //                          ? ICD_Name
                    //                          : Utility.sDbnull(dr["chuan_doanphu"]) + ";" + ICD_Name;
                    //    //dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                    //   // dr["ma_icd"] = ICD_Code;
                    //}
                    //
                    foreach (DataRow drv in mDtReportPhieuThu.Rows)
                    {

                        drv["chuan_doanphu"] = Utility.sDbnull(drv["chuan_doanphu"]).Trim() == ""
                                          ? ICD_Name
                                          : Utility.sDbnull(drv["chuan_doanphu"]) + ";" + ICD_Name;
                        if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "1"//Chi phí KCB
                            || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "0"//Phí KCB kèm theo
                            || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "4"//Buồng giường
                            || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "8"//Gói dịch vụ
                            )
                        {

                            drv["ten_loaidichvu"] = string.Empty;
                            drv["STT"] = 1;
                            drv["id_loaidichvu"] = -1;
                        }
                        else if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "2")
                        {
                            string maLoaidichvu = Utility.sDbnull(drv["id_loaidichvu"], -1);
                            //drv["id_loaidichvu"]-->Được xác định trong câu truy vấn
                            var objService = (from p in globalVariables.gv_dtDmucChung.AsEnumerable()
                                              where
                                                  p[DmucChung.Columns.Loai].Equals("LOAIDICHVUCLS") &&
                                                  p[DmucChung.Columns.Ma].Equals(maLoaidichvu)
                                              select p).FirstOrDefault();
                            // DmucChung objService = THU_VIEN_CHUNG.LaydoituongDmucChung("LOAIDICHVUCLS", maLoaidichvu);
                            if (objService != null)
                            {
                                drv["ten_loaidichvu"] = Utility.sDbnull(objService["ten"].ToString());
                                drv["STT"] = Utility.sDbnull(objService["stt_hthi"]);
                            }

                        }
                        else if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "3" 
                            || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "5")
                        {
                            int drugId = Utility.Int32Dbnull(drv["id_dichvu"], -1);
                            var objDrug = (from p in globalVariables.gv_dtDanhMucThuoc.AsEnumerable()
                                           where p[DmucThuoc.Columns.IdThuoc].Equals(drugId)
                                           select p).FirstOrDefault();
                            var objLoaithuoc = (from p in globalVariables.gv_dtLoaiThuoc.AsEnumerable()
                                                where objDrug != null && p[DmucLoaithuoc.Columns.IdLoaithuoc].Equals(objDrug["id_loaithuoc"].ToString())
                                                select p).FirstOrDefault();
                            //DmucThuoc objDrug = DmucThuoc.FetchByID(drugId);
                            //  DmucLoaithuoc objLoaithuoc = DmucLoaithuoc.FetchByID(objDrug.IdLoaithuoc);
                            if (objLoaithuoc != null)
                                if (objDrug != null) objDrug["kieu_thuocvattu"] = objLoaithuoc["kieu_thuocvattu"];
                            //LDrugType objLDrugType = LDrugType.FetchByID(objDrug.DrugTypeId);
                            if (objDrug != null && objDrug["kieu_thuocvattu"].ToString() == "THUOC")
                            {
                                drv["id_loaidichvu"] = "THUOC";
                                drv["STT"] = 1;
                                drv["ten_loaidichvu"] = "3.1 Trong danh mục BHYT";
                            }
                            else
                            {
                                drv["id_loaidichvu"] = "VTTH";
                                drv["STT"] = 2;
                                drv["ten_loaidichvu"] = "Vật tư tiêu hao";
                            }
                        }
                        if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "9")
                        {
                            drv["id_loaidichvu"] = 1;
                            drv["STT"] = 1;
                            drv["ten_loaidichvu"] = "Chi phí thêm  ";
                        }
                    }
                    mDtReportPhieuThu.AcceptChanges();
                    new INPHIEU_THANHTOAN_NOITRU(objPhieuDct.NgayTao).INPHOI_BHYT(
                        mDtReportPhieuThu, ngayIn, objLuotkham);

                }
                return actionResult == ActionResult.Success;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi thực hiện in phôi BHYT\n" + ex.Message);
                return false;
            }
        }
        private decimal SumOfTotal_BH(DataTable dataTable, string sFildName)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + sFildName + ")", "1=1"), 0);
        }
        private MoneyByLetter sMoneyByLetter = new MoneyByLetter();
        public void INPHOI_BHYT(DataTable mDtReportPhieuThu, DateTime ngayIn, KcbLuotkham objPatientExam)
        {
            Utility.UpdateLogotoDatatable(ref mDtReportPhieuThu);
            THU_VIEN_CHUNG.NoiTru_Sapxepthutuin(ref mDtReportPhieuThu, true);
            mDtReportPhieuThu.DefaultView.Sort = "stt_in,stt_hthi_dichvu,stt_hthi_chitiet,stt_in_thuoc,ten_chitietdichvu";
            mDtReportPhieuThu.AcceptChanges();

            string tieude = "", reportname = "";
            var crpt = Utility.GetReport("BHYT_InPhoi_02", ref tieude, ref reportname);
            // VMS.HISLink.Report.Report.BHYT_InPhoi crpt = new VMS.HISLink.Report.Report.BHYT_InPhoi();
            // tieude = "BẢNG KÊ CHI PHÍ KHÁM BỆNH, CHỮA BỆNH NGOẠI TRÚ";
            //reportname = "BHYT_InPhoi.RPT".ToUpper();
            if (crpt == null) return;
            var objForm = new frmPrintPreview(tieude, crpt, true, true);
            objForm.NGAY = _ngayinphieu;
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "BHYT_InPhoi_02";
                crpt.SetDataSource(mDtReportPhieuThu.DefaultView);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(ngayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sMoneyCharacter_Thanhtien",
                                       sMoneyByLetter.sMoneyToLetter(
                                           SumOfTotal_BH(mDtReportPhieuThu, "TT_KHONG_PHUTHU").ToString(CultureInfo.InvariantCulture)));
                Utility.SetParameterValue(crpt, "sMoneyCharacter_Thanhtien_BH",
                                       sMoneyByLetter.sMoneyToLetter(
                                           SumOfTotal_BH(mDtReportPhieuThu, "TT_BHYT").ToString(CultureInfo.InvariantCulture)));
                Utility.SetParameterValue(crpt, "sMoneyCharacter_Thanhtien_BN",
                                       sMoneyByLetter.sMoneyToLetter(
                                           SumOfTotal_BH(mDtReportPhieuThu, "TT_BN").ToString(CultureInfo.InvariantCulture)));
                Utility.SetParameterValue(crpt, "sMoneyCharacter_Thanhtien_Khac",
                                       sMoneyByLetter.sMoneyToLetter(
                                           SumOfTotal_BH(mDtReportPhieuThu, "TT_PHUTHU").ToString(CultureInfo.InvariantCulture)));
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, _ngayinphieu));
                objForm.crptViewer.ReportSource = crpt;


                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInPhoiBHYT))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    if (objForm.ShowDialog() == DialogResult.OK)
                    {
                        //Tự động khóa BN để kết thúc
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(DateTime.Now)
                            .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(globalVariables.UserName)
                            .Set(KcbLuotkham.Columns.Locked).EqualTo(1)
                            .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan).Execute();
                    }
                }
                else
                {
                    //Tự động khóa BN để kết thúc
                    new Update(KcbLuotkham.Schema)
                        .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(DateTime.Now)
                        .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(globalVariables.UserName)
                        .Set(KcbLuotkham.Columns.Locked).EqualTo(1)
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan).Execute();
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);

                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }
    }
}
