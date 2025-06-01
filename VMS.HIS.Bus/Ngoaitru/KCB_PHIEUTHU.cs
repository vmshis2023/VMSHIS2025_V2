using System.Data;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.BusRule.Classes
{
    public class KCB_PHIEUTHU
    {
        public void XuLyThongTinPhieu_DichVu(ref DataTable m_dtReportPhieuThu)
        {
            Utility.AddColumToDataTable(ref m_dtReportPhieuThu, "TONG_BN", typeof (decimal));
            Utility.AddColumToDataTable(ref m_dtReportPhieuThu, "PHU_THU", typeof (decimal));
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                //drv["ThanhTien"] = Utility.Int32Dbnull(drv[KcbThanhtoanChitiet.Columns.SoLuong], 0) *s
                //                   Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.BnhanChitra], 0);
                drv["TotalSurcharge_Price"] = Utility.Int32Dbnull(drv[KcbThanhtoanChitiet.Columns.SoLuong], 0)*
                                              Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.PhuThu], 0);
            }
            m_dtReportPhieuThu.AcceptChanges();
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["TONG_BN"] = Utility.Int32Dbnull(drv[KcbThanhtoanChitiet.Columns.SoLuong], 0)*
                                 Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.BnhanChitra], 0);
                drv["PHU_THU"] = Utility.Int32Dbnull(drv[KcbThanhtoanChitiet.Columns.SoLuong], 0)*
                                 Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.PhuThu], 0);
            }
            m_dtReportPhieuThu.AcceptChanges();
        }

        /// <summary>
        ///     hàm thực hiện việc lấy thông tin của việc in phiếu dịch vụ cho đơn vị hải quân
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <returns></returns>
        public DataTable GetDataInphieuDichvu(KcbThanhtoan objThanhtoan)
        {
            return
                SPs.KcbThanhtoanLaythongtinInphieuDichvu(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
                    objThanhtoan.MaLuotkham,
                    Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        }

        //public DataTable QUAYTHUOC_GetDataInphieuDichvu(KcbThanhtoan objThanhtoan, string stockId)
        //{
        //    return
        //        SPs.NoitietQthuocInphieudvTatca(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
        //                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan), stockId).GetDataSet().Tables[0];

        //}
        //public DataTable INPHIEU_DICHVU_NOITRU(KcbThanhtoan objThanhtoan,string KieuThanhToan)
        //{
        //    return
        //        SPs.YhhqInphieuDichvuNoitru(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
        //                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan), KieuThanhToan).GetDataSet().Tables[0];

        //}
        /// <summary>
        ///     hàm thực hiện việc lấy thông tin của việc in phiếu dịch vụ cho đơn vị hải quân
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <returns></returns>
        //public DataTable GetDataNoiTruInphieuDichvu(KcbThanhtoan objThanhtoan)
        //{
        //    return
        //        SPs.YhhqNoitruInphieuBhDichvu(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
        //                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        //}
        public DataTable KYDONGGetDataInphieuDichvuAo(KcbThanhtoan objThanhtoan)
        {
            return
                SPs.KcbThanhtoanLaythongtinInphieuDichvu(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
                    objThanhtoan.MaLuotkham,
                    Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        }

        /// <summary>
        ///     thực hiện việc lấy thông tin của việc in phiếu cho bảo hiểm
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <returns></returns>
        public DataTable GetDataInphieuBH(KcbThanhtoan objThanhtoan, bool IsBH)
        {
            DataTable dataTable =
                SPs.BhytLaythongtinInphieubhyt(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
                    objThanhtoan.MaLuotkham,
                    Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
            if (IsBH)
            {
                foreach (DataRow drv in dataTable.Rows)
                {
                    if (drv[KcbThanhtoanChitiet.Columns.PhuThu].ToString() == "1") drv.Delete();
                }
                dataTable.AcceptChanges();
            }
            return dataTable;
        }

        public DataTable GetDataInphieuBH(KcbThanhtoan objThanhtoan, bool IsBH, int KieuThanhToan)
        {
            DataTable dataTable =
                SPs.BhytLaythongtinInphieubhyt(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
                    objThanhtoan.MaLuotkham,
                    Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
            if (IsBH)
            {
                foreach (DataRow drv in dataTable.Rows)
                {
                    if (drv[KcbThanhtoanChitiet.Columns.PhuThu].ToString() == "1") drv.Delete();
                }
                dataTable.AcceptChanges();
            }
            return dataTable;
        }

        /// <summary>
        ///     HÀM THỰC HIỆN VIỆC IN PHIẾU CHO BỆNH NHÂN
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <returns></returns>
        public DataTable INPHIEUBH_CHOBENHNHAN(KcbThanhtoan objThanhtoan)
        {
            DataTable dataTable =
                SPs.BhytLaythongtinInphieubhytChobenhnhan(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
                    objThanhtoan.MaLuotkham,
                    Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];

            return dataTable;
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu cho bệnh nhân
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <param name="KieuThanhToan"></param>
        /// <returns></returns>
        //public DataTable INPHIEUBH_CHOBENHNHAN_NOITRU(KcbThanhtoan objThanhtoan,string KieuThanhToan)
        //{
        //    DataTable dataTable =
        //        SPs.YhhqInphieuBaohiemChobnNoitru(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
        //                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan), KieuThanhToan).GetDataSet().Tables[0];

        //    return dataTable;
        //}
        //public DataTable INPHIEUBH_NOITRU(KcbThanhtoan objThanhtoan,int IsPayment,string KieuThanhToan)
        //{
        //    DataTable dataTable =
        //        SPs.YhhqInphieuBiemHiemNoiTru(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
        //                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan), IsPayment, KieuThanhToan).GetDataSet().Tables[0];

        //    return dataTable;
        //}
        /// <summary>
        /// hàm thực hiện in phiếu bảo hiểm đúng tyyeens
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <returns></returns>
        //public DataTable KYDONG_GetDataInphieuBH(KcbThanhtoan objThanhtoan,bool IsPayment)
        //{
        //    DataTable dataTable=
        //        SPs.BhytLaythongtinInphieuTraituyen(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
        //                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        //    if(!IsPayment)
        //    {
        //          foreach (DataRow drv in dataTable.Rows)
        //           {
        //          if(drv[KcbThanhtoanChitiet.Columns.PhuThu].ToString()=="1")drv.Delete();
        //           }
        //        dataTable.AcceptChanges();
        //    }
        //    return dataTable;

        //}
        /// <summary>
        /// hàm thực hiện in phiếu bảo hiểm trái tuyến
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <returns></returns>
        //public DataTable KYDONG_GetDataInphieuBH_TraiTuyen(KcbThanhtoan objThanhtoan)
        //{
        //    return
        //        SPs.KydongInPhieubaohiemTraituyen(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
        //                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];

        //}
        /// <summary>
        /// thực hiện việc lấy thông tin của việc in phiếu cho bảo hiểm
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <returns></returns>
        //public DataTable KYDONGGetDataInphieuBHSoLieuAo(KcbThanhtoan objThanhtoan)
        //{
        //    return
        //        SPs.BhytLaythongtinInphieuTraituyenAo(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
        //                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];

        //}
    }
}