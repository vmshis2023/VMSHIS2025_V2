using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.Libs;
using VNS.Properties;

namespace VMS.HIS.Bus.BHYT
{
    public class BHYT_InPhieu
    {
        private Logger log;
        DateTime NGAYINPHIEU;
        private MoneyByLetter sMoneyByLetter = new MoneyByLetter();
        public BHYT_InPhieu(DateTime NGAYINPHIEU)
        {
            this.NGAYINPHIEU = NGAYINPHIEU;
        }
        public BHYT_InPhieu(DateTime NGAYINPHIEU, Logger log)
        {
            this.NGAYINPHIEU = NGAYINPHIEU;
            this.log = log;
        }
        public BHYT_InPhieu(Logger log)
        {
            this.log = log;
        }
        public BHYT_InPhieu()
        {
        }
        public bool InPhoiBHYT(KcbLuotkham objLuotkham, DateTime ngayIn, string report_code)
        {
            try
            {
                DataTable m_dtChiPhiInPhoiBHYT =
                        SPs.BhytInPhoi(Utility.sDbnull(objLuotkham.MaLuotkham, ""), Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1), 1, "-1", "-1",Utility.ByteDbnull( objLuotkham.Noitru)).GetDataSet().Tables[0];
                decimal tt = 0;
                decimal tt_bhyt = 0;
                decimal tt_bhyt_cct = 0;
                decimal tt_bn_cct = 0;
                decimal TT_BN = 0;
                decimal tt_dathanhtoan_ngoaitru = 0;
                decimal tt_dathanhtoan_noitru = 0;
                decimal tt_conlai_chuathanhtoan = 0;

                TinhtoanTienCungChiTra(objLuotkham, m_dtChiPhiInPhoiBHYT, ref tt, ref tt_bhyt, ref tt_bhyt_cct, ref tt_bn_cct, ref TT_BN, ref tt_dathanhtoan_ngoaitru, ref tt_dathanhtoan_noitru, ref tt_conlai_chuathanhtoan);
                KcbPhieuDct objPhieuDct = CreatePhieuDongChiTra(objLuotkham);
                objPhieuDct.TongTien = tt_bhyt;
                objPhieuDct.BhytChitra = tt_bhyt_cct;
                objPhieuDct.BnhanChitra = tt_bn_cct;
                objPhieuDct.NgayTao = ngayIn;
                ActionResult actionResult = new KCB_THANHTOAN().UpdatePhieuDCT(objPhieuDct, objLuotkham);
                if (actionResult == ActionResult.Success) //Tránh trường hợp in ra phôi mà ko đẩy vào CSDL
                {

                    THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtChiPhiInPhoiBHYT, false);
                    m_dtChiPhiInPhoiBHYT.DefaultView.Sort = "STT_KHOA,ngaybatdau_bhyt,stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
                    THU_VIEN_CHUNG.CreateXML(m_dtChiPhiInPhoiBHYT, "BHYT_INPHOI.XML");
                    if (m_dtChiPhiInPhoiBHYT.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("Không tìm thấy dữ liệu để in phôi BHYT ", "Thông báo");
                        return false;
                    }

                    m_dtChiPhiInPhoiBHYT.AcceptChanges();
                    INPHOI_BHYT(m_dtChiPhiInPhoiBHYT, ngayIn, objLuotkham, report_code);

                }
                return actionResult == ActionResult.Success;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi thực hiện in phôi BHYT\n" + ex.Message);
                return false;
            }
        }
        void INPHOI_BHYT(DataTable m_dtChiPhiInPhoiBHYT, DateTime ngayIn, KcbLuotkham objLuotkham, string report_code)
        {
            Utility.UpdateLogotoDatatable(ref m_dtChiPhiInPhoiBHYT);
            if (objLuotkham.Noitru == 1)
            {
                THU_VIEN_CHUNG.NoiTru_Sapxepthutuin(ref m_dtChiPhiInPhoiBHYT, true);

            }
            else
            {
                THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtChiPhiInPhoiBHYT, true);
            }
            Utility.UpdateLogotoDatatable(ref m_dtChiPhiInPhoiBHYT);
            m_dtChiPhiInPhoiBHYT.DefaultView.Sort = "STT_KHOA,ngaybatdau_bhyt,stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
            m_dtChiPhiInPhoiBHYT.AcceptChanges();

            string tieude = "", reportname = "";
            var crpt = Utility.GetReport(report_code, ref tieude, ref reportname);
            if (crpt == null) return;
            frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, true);
            objForm.NGAY = NGAYINPHIEU;
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "BHYT_InPhoi";
                crpt.SetDataSource(m_dtChiPhiInPhoiBHYT.DefaultView);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(ngayIn));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(ngayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sMoneyCharacter_Thanhtien", sMoneyByLetter.sMoneyToLetter(SumOfTotal_BH(m_dtChiPhiInPhoiBHYT, "TT_KHONG_PHUTHU").ToString()));
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
                objForm.crptViewer.ReportSource = crpt;


                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInPhoiBHYT))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    if (objForm.ShowDialog() == DialogResult.OK)
                    {
                        ////Tự động khóa BN để kết thúc
                        //new Update(KcbLuotkham.Schema)
                        //    .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(DateTime.Now)
                        //    .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(globalVariables.UserName)
                        //    .Set(KcbLuotkham.Columns.Locked).EqualTo(1)
                        //    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        //    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                    }
                }
                else
                {
                    ////Tự động khóa BN để kết thúc
                    //new Update(KcbLuotkham.Schema)
                    //    .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(DateTime.Now)
                    //    .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(globalVariables.UserName)
                    //    .Set(KcbLuotkham.Columns.Locked).EqualTo(1)
                    //    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    //    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);

                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                log.Trace(ex.Message);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }
        private decimal SumOfTotal_BH(DataTable dataTable, string sFildName)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + sFildName + ")", "1=1"), 0);
        }
        private void TinhtoanTienCungChiTra(KcbLuotkham objLuotkham, DataTable m_dtChiPhiInPhoiBHYT, ref decimal tt, ref decimal tt_bhyt, ref decimal tt_bhyt_cct, ref decimal tt_bn_cct, ref decimal TT_BN, ref decimal tt_dathanhtoan_ngoaitru, ref decimal tt_dathanhtoan, ref decimal tt_conlai_chuathanhtoan)
        {
            try
            {
                string errMsg = "";
                decimal Ptram_bhyt_cu = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                decimal tt_chon = 0m;
                decimal tt_khong_phuthu = 0m;
                decimal tt_bn_ttt = 0m;
                decimal tt_phuthu = 0m;//Tổng tiền phụ thu
                decimal tt_tutuc = 0m;//Tổng tiền tự túc
                decimal tt_nguonkhac = 0m;
                decimal tt_hotro = 0m;
                decimal tt_khac = 0m;//Tổng khác= tổng nguồn khác+ tổng hỗ trợ
                decimal tt_khuyenmai = 0m;
                decimal _chuathanhtoan = 0m;
                decimal ptram_BHYT_moi = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                decimal tt_thanhtoan_ngoaitru = 0m;//
                foreach (DataRow drv in m_dtChiPhiInPhoiBHYT.Rows)//Không tính theo default view để luôn hiển thị đúng phần liên quan BHYT khi thực hiện lọc dữ liệu theo trạng thái thanh toán và loại chi phí
                {
                    if (Utility.Int32Dbnull(drv["tinh_chiphi"], 0) == 1 && Utility.Int32Dbnull(drv["trangthai_huy"], 0) == 0)//&& Utility.Int32Dbnull(drv["tthai_tamthu"], 0) == 0)
                    {

                        tt += Utility.DecimaltoDbnull(drv["TT"], 0);

                        tt_khong_phuthu += Utility.DecimaltoDbnull(drv["TT_KHONG_PHUTHU"], 0);
                        tt_bhyt += Utility.DecimaltoDbnull(drv["TT_BHYT"], 0);
                        tt_bhyt_cct += Utility.DecimaltoDbnull(drv["tt_bhyt_cct"], 0);
                        tt_bn_cct += Utility.DecimaltoDbnull(drv["tt_bn_cct"], 0);
                        tt_bn_ttt += Utility.DecimaltoDbnull(drv["tt_bn_ttt"], 0);
                        TT_BN += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        if (Utility.Int32Dbnull(drv["trangthai_thanhtoan"], 0) == 0) _chuathanhtoan += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        tt_phuthu += Utility.DecimaltoDbnull(drv["TT_PHUTHU"], 0);
                        if (Utility.Int32Dbnull(drv["tu_tuc"], 0) == 1) tt_tutuc += Utility.DecimaltoDbnull(drv["TT_TUTUC"], 0);
                        if (Utility.sDbnull(drv["trangthai_thanhtoan"], "0") == "0")
                        {
                            tt_conlai_chuathanhtoan += Utility.DecimaltoDbnull(drv["TT_BN_KHONG_PHUTHU"], 0) + Utility.DecimaltoDbnull(drv["TT_PHUTHU"], 0);
                        }
                        if (Utility.sDbnull(drv["trangthai_thanhtoan"], "0") == "1")
                        {
                            tt_dathanhtoan += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                            if (Utility.sDbnull(drv["nguon_thanhtoan"], "0") == "0")
                                tt_dathanhtoan_ngoaitru += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        }
                        if (Utility.sDbnull(drv["trangthai_thanhtoan"], "0") == "1" && Utility.sDbnull(drv["noi_tru"], "0") == "0")
                        {
                            tt_thanhtoan_ngoaitru += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        }
                    }
                }
                if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                {
                    ptram_BHYT_moi = THU_VIEN_CHUNG.BHYT_TinhlaiPhantramTheoTongchiPhi(objLuotkham, tt_bhyt);
                    //Tính lại các cột đơn giá cùng chi trả
                    if (Ptram_bhyt_cu != ptram_BHYT_moi)
                    {
                        tt_conlai_chuathanhtoan = 0;//Tính lại tổng tiền còn lại
                        tt_bhyt_cct = tt_bhyt * ptram_BHYT_moi / 100;
                        tt_bn_cct = tt_bhyt - tt_bhyt_cct;

                        //Update một số cột tổng tiền để bước TinhToanSoTienPhaithu(Tính toán số tiền phải thu) bên dưới thể hiện đúng
                        foreach (DataRow dr in m_dtChiPhiInPhoiBHYT.Rows)
                        {
                            if (Utility.Int32Dbnull(dr["tinh_chiphi"], 0) == 1 && Utility.Int32Dbnull(dr["trangthai_huy"], 0) == 0 && Utility.DecimaltoDbnull(dr["bhyt_gia_tyle"], 0) > 0)//&& Utility.Int32Dbnull(drv["tthai_tamthu"], 0) == 0)
                            {
                                dr["muc_huong_bhyt"] = ptram_BHYT_moi;
                                if (ptram_BHYT_moi == 100)
                                {
                                    tt_bn_ttt = 0;
                                    dr["TT_BN_CCT"] = 0;
                                    dr["tt_bhyt_cct"] = dr["TT_BHYT"];
                                    dr["tt_bn_ttt"] = 0;
                                    dr["TT_BN_KHONG_PHUTHU"] = 0;//do tiền cùng chi trả đã thay đổi nên cột giá bnhan_chitra trong CSDL vẫn giữ nguyên nhưng thực tế sẽ thay đổi khác.
                                    dr["THUC_THU"] = 0;
                                }
                                if (Utility.sDbnull(dr["trangthai_thanhtoan"], "0") == "0")
                                {
                                    tt_conlai_chuathanhtoan += Utility.DecimaltoDbnull(dr["TT_BN_KHONG_PHUTHU"], 0) + Utility.DecimaltoDbnull(dr["TT_PHUTHU"], 0);
                                }

                            }
                        }
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_CACHTINH_TONGTIEN_BN_PHAITRA", "1", true) == "1")
                            TT_BN = tt - tt_bhyt - tt_nguonkhac - tt_hotro;//Hoặc =tt_bn_cct+tt_phuthu+tt_tutuc
                        else
                            TT_BN = tt_bn_cct + tt_bn_ttt + tt_phuthu + tt_tutuc;
                    }
                }

                tt_khac = tt_hotro + tt_nguonkhac;
                string s_tt = new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tt));
                string s_tt_dathanhtoan = new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tt_dathanhtoan));
                string s_tt_conlai_chuathanhtoan = new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tt_conlai_chuathanhtoan));
                string s_tt_dathanhtoan_ngoaitru = new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tt_dathanhtoan_ngoaitru));
                //Cập nhật các số tổng tiền chung
                foreach (DataRow dr in m_dtChiPhiInPhoiBHYT.Rows)
                {
                    dr["sotien_tt_ngoaitru"] = tt_dathanhtoan_ngoaitru;
                    dr["sotien_dathanhtoan"] = tt_dathanhtoan;
                    dr["sotien_conlai"] = tt_conlai_chuathanhtoan;
                    //3 số dưới có thể tính bằng sum trên report
                    dr["sotien_bhyt_cct"] = tt_bhyt_cct;
                    dr["sotien_nguoibenh_cct"] = tt_bn_cct;
                    dr["sotien_tutuc"] = tt_tutuc;

                    dr["tt_chu"] = s_tt;
                    dr["sotien_dathanhtoan_chu"] = s_tt_dathanhtoan;
                    dr["sotien_conlai_chu"] = s_tt_conlai_chuathanhtoan;
                    dr["sotien_tt_ngoaitru_chu"] = s_tt_dathanhtoan_ngoaitru;
                    //Đưa tiền tự túc + phụ thu vào cột người bệnh tự trả
                    if (Utility.Int32Dbnull(dr["tu_tuc"], 0) == 1)
                        dr["tt_bn_ttt"] = Utility.DecimaltoDbnull(dr["TT_TUTUC"], 0) + Utility.DecimaltoDbnull(dr["TT_PHUTHU"], 0);
                    else
                        dr["tt_bn_ttt"] = Utility.DecimaltoDbnull(dr["tt_bn_ttt"], 0) + Utility.DecimaltoDbnull(dr["TT_PHUTHU"], 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private KcbPhieuDct CreatePhieuDongChiTra(KcbLuotkham objLuotkham)
        {
            KcbPhieuDct objPhieuDct = new KcbPhieuDct();
            objPhieuDct.MaPhieuDct = Utility.sDbnull(THU_VIEN_CHUNG.TaomaDongChiTra(DateTime.Now));
            objPhieuDct.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
            objPhieuDct.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
            objPhieuDct.KieuThanhtoan = Utility.ByteDbnull(objLuotkham.TrangthaiNoitru > 0 ? 1 : 0);
            objPhieuDct.NguoiTao = globalVariables.UserName;
            objPhieuDct.TrangthaiXml = 0;
            objPhieuDct.NgayTao = DateTime.Now;
            objPhieuDct.IpMaytao = globalVariables.gv_strIPAddress;
            objPhieuDct.MatheBhyt = Utility.sDbnull(objLuotkham.MatheBhyt, "");
            objPhieuDct.MaKhoaThuchien = Utility.sDbnull(objLuotkham.MaKhoaThuchien, "");
            objPhieuDct.TenMaytao = globalVariables.gv_strComputerName;
            objPhieuDct.TrangthaiXml = 0;
            return objPhieuDct;

        }
    }
}
