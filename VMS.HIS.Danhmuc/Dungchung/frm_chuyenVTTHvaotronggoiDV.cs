using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_chuyenVTTHvaotronggoiDV : Form
    {
        private DataTable m_dtGoidichvu;
        private DataTable m_dtVTTH = new DataTable();
        private DataTable m_dtVTTH_tronggoi = new DataTable();
        private DataTable m_dtVTTHChitiet_View = new DataTable();
        public KcbLuotkham objLuotkham = null;
        public bool m_blnCancel = true;
        public frm_chuyenVTTHvaotronggoiDV()
        {
            InitializeComponent();
            this.Load+=frm_chuyenVTTHvaotronggoiDV_Load;
            InitEvents();
        }
        void InitEvents()
        {
            mnuDeleteCheckedItems.Click += mnuDeleteCheckedItems_Click;
            mnuMoveto.Click += mnuMoveto_Click;
            grdGoidichvu.SelectionChanged += grdGoidichvu_SelectionChanged;
            this.Load += frm_chuyenVTTHvaotronggoiDV_Load;
            mnuRestore.Click += mnuRestore_Click;
            mnuDelItems.Click += mnuDelItems_Click;
            mnumoveIn.Click += mnumoveIn_Click;
            mnuMoveout.Click += mnuMoveout_Click;
            grdVTTH.CellUpdated += grdVTTH_CellUpdated;
            grdVTTH_tronggoi.CellUpdated += grdVTTH_tronggoi_CellUpdated;
            cmdAccept.Click += cmdAccept_Click;
            cmdHuy.Click += cmdHuy_Click;
        }

        void cmdHuy_Click(object sender, EventArgs e)
        {
            
        }

        void cmdAccept_Click(object sender, EventArgs e)
        {
            
        }

        void grdVTTH_tronggoi_CellUpdated(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
                new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.SoLuong).EqualTo(Utility.Int32Dbnull(Utility.getValueOfGridCell(grdVTTH_tronggoi, KcbDonthuocChitiet.Columns.SoLuong)))
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Utility.Int32Dbnull(Utility.getValueOfGridCell(grdVTTH_tronggoi, KcbDonthuocChitiet.Columns.IdChitietdonthuoc))).Execute();
                DataRow row = ((DataRowView)grdVTTH_tronggoi.CurrentRow.DataRow).Row;

                if (!Utility.Byte2Bool(row[KcbDonthuocChitiet.Columns.TuTuc]))
                {
                    decimal BHCT = 0m;
                    if (objLuotkham.TrangthaiNoitru <= 0)
                        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                    else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);

                    //decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                    decimal num3 = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                    row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                    row[KcbDonthuocChitiet.Columns.BnhanChitra] = num3;


                }
                else
                {
                    row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;

                    row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                    row[KcbDonthuocChitiet.Columns.BnhanChitra] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                }

                row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                if (Utility.Byte2Bool(row[KcbDonthuocChitiet.Columns.TrongGoi]))
                {
                    row[KcbDonthuocChitiet.Columns.DonGia] = 0;
                    row[KcbDonthuocChitiet.Columns.BnhanChitra] = 0;
                    row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                    row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                    row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                    row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                    row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                    row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                    row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                }
                m_dtVTTH_tronggoi.AcceptChanges();
                m_blnCancel = false;

            }
            catch (Exception ex)
            {
                Utility.SetMsg(lblMsg, ex.Message, true);

            }
        }

        void grdVTTH_CellUpdated(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
                new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.SoLuong).EqualTo(Utility.Int32Dbnull(Utility.getValueOfGridCell(grdVTTH, KcbDonthuocChitiet.Columns.SoLuong)))
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Utility.Int32Dbnull(Utility.getValueOfGridCell(grdVTTH, KcbDonthuocChitiet.Columns.IdChitietdonthuoc))).Execute();
                DataRow row = ((DataRowView)grdVTTH.CurrentRow.DataRow).Row;

                //    if (!Utility.Byte2Bool(row[KcbDonthuocChitiet.Columns.TuTuc]))
                //    {
                //        decimal BHCT = 0m;
                //        if (objLuotkham.TrangthaiNoitru <= 0)
                //            BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                //        else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                //            BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);

                //        //decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                //        decimal num3 = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                //        row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                //        row[KcbDonthuocChitiet.Columns.BnhanChitra] = num3;


                //    }
                //    else
                //    {
                //        row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;

                //        row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                //        row[KcbDonthuocChitiet.Columns.BnhanChitra] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                //    }

                //row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                //row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                //row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                //row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                //row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                //row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                if (Utility.Byte2Bool(row[KcbDonthuocChitiet.Columns.TrongGoi]))
                {
                    row[KcbDonthuocChitiet.Columns.DonGia] = 0;
                    row[KcbDonthuocChitiet.Columns.BnhanChitra] = 0;
                    row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                    row[KcbDonthuocChitiet.Columns.PhuThu] = 0;
                    row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                    row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                    row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                    row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                    row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                    row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                }
                m_dtVTTH.AcceptChanges();
                m_blnCancel = false;
            }
            catch (Exception ex)
            {
                Utility.SetMsg(lblMsg, ex.Message, true);

            }
        }

        void mnuMoveout_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                List<long> lstIdchitietDonthuoc = (from q in grdVTTH.GetCheckedRows().AsEnumerable()
                                                   select Utility.Int64Dbnull(q.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1)).Distinct().ToList<long>();
                if (lstIdchitietDonthuoc.Count <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn các chi tiết cần chuyển ra ngoài gói(có tính phí)", true);
                    return;
                }
                new noitru_phieudieutri().ChuyenVTTH(lstIdchitietDonthuoc, (byte)0);
                UpdateTrangthai(m_dtVTTH, lstIdchitietDonthuoc, (byte)0,-1);
                m_blnCancel = false;
                Utility.SetMsg(lblMsg,"Đã chuyển các chi tiết được chọn về trạng thái ngoài gói(có thu phí) thành công",false);
            }
            catch (Exception ex)
            {

                Utility.SetMsg(lblMsg, ex.Message, true);
            } 
        }

        void mnumoveIn_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                List<long> lstIdchitietDonthuoc = (from q in grdVTTH.GetCheckedRows().AsEnumerable()
                                                           select Utility.Int64Dbnull(q.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1)).Distinct().ToList<long>();
                if (lstIdchitietDonthuoc.Count <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn các chi tiết cần chuyển vào trong gói(không tính phí)", true);
                    return;
                }
                new noitru_phieudieutri().ChuyenVTTH(lstIdchitietDonthuoc, (byte)1);
                UpdateTrangthai(m_dtVTTH, lstIdchitietDonthuoc, (byte)1,-1);
                m_blnCancel = false;
                Utility.SetMsg(lblMsg, "Đã chuyển các chi tiết được chọn về trạng thái trong gói(không tính phí) thành công", false);
            }
            catch (Exception ex)
            {

                Utility.SetMsg(lblMsg, ex.Message, true);
            }
        }

        void mnuDelItems_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các chi tiết đang chọn ra khỏi phiếu Vật tư thuộc: " + Utility.getValueOfGridCell(grdGoidichvu, "ten_chitietdichvu") + " hay không?", "Xác nhận xóa", true))
                {
                    return;
                }
                List<long> lstIdchitietDonthuoc = (from q in grdVTTH_tronggoi.GetCheckedRows().AsEnumerable()
                                                   select Utility.Int64Dbnull(q.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1)).Distinct().ToList<long>();
                if (lstIdchitietDonthuoc.Count <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn ít nhất một chi tiết cần xóa", true);
                    return;
                }
                string sId = string.Join(",", lstIdchitietDonthuoc);
                new KCB_KEDONTHUOC().XoaChitietDonthuoc(sId);
                XoaThuocKhoiBangdulieu(m_dtVTTH_tronggoi, lstIdchitietDonthuoc);
                m_blnCancel = false;
                Utility.SetMsg(lblMsg, "Đã xóa các chi tiết Vật tư tiêu hao trong gói thành công", false);
            }
            catch (Exception ex)
            {
                Utility.SetMsg(lblMsg, ex.Message, true);
            }
        }
        void mnuRestore_Click(object sender, EventArgs e)
        {

        }
        void XoaThuocKhoiBangdulieu(DataTable dtData, List<long> lstIdChitietDonthuoc)
        {
            try
            {
                var p = (from q in dtData.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    dtData.Rows.Remove(p[i]);
                dtData.AcceptChanges();
            }
            catch
            {
            }
        }
        void UpdateTrangthai(DataTable dtData, List<long> lstIdChitietDonthuoc,byte tronggoi,int id_goi)
        {
            try
            {
                decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
                var p = (from q in dtData.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                foreach (DataRow row in p)
                {
                    row[KcbDonthuocChitiet.Columns.TrongGoi] = tronggoi;
                    row[KcbDonthuocChitiet.Columns.IdGoi] = id_goi;
                    //if (!Utility.Byte2Bool(row[KcbDonthuocChitiet.Columns.TuTuc]))
                    //{
                    //    decimal BHCT = 0m;
                    //    if (objLuotkham.TrangthaiNoitru <= 0)
                    //        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                    //    else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                    //        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);

                    //    //decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                    //    decimal num3 = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                    //    row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                    //    row[KcbDonthuocChitiet.Columns.BnhanChitra] = num3;


                    //}
                    //else
                    //{
                    //    row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;

                    //    row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                    //    row[KcbDonthuocChitiet.Columns.BnhanChitra] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                    //}

                    //row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                    //row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                    //row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                    //row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                    //row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                    //row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                    if (Utility.Byte2Bool(row[KcbDonthuocChitiet.Columns.TrongGoi]))
                    {
                        row[KcbDonthuocChitiet.Columns.DonGia] = 0;
                        row[KcbDonthuocChitiet.Columns.BnhanChitra] = 0;
                        row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                        row[KcbDonthuocChitiet.Columns.PhuThu] = 0;
                        row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                        row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                        row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                        row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                        row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                        row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                    }
                    m_dtVTTH_tronggoi.AcceptChanges();
                }
                dtData.AcceptChanges();
            }
            catch
            {
            }
        }
        
        void mnuDeleteCheckedItems_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các chi tiết đang chọn ra khỏi phiếu Vật tư", "Xác nhận xóa", true))
                {
                    return;
                }
                List<long> lstIdchitietDonthuoc = (from q in grdVTTH.GetCheckedRows().AsEnumerable()
                                                   select Utility.Int64Dbnull(q.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1)).Distinct().ToList<long>();
                if (lstIdchitietDonthuoc.Count <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn ít nhất một chi tiết cần xóa", true);
                    return;
                }
                string sId = string.Join(",", lstIdchitietDonthuoc);
                new KCB_KEDONTHUOC().XoaChitietDonthuoc(sId);
                XoaThuocKhoiBangdulieu(m_dtVTTH, lstIdchitietDonthuoc);
                m_blnCancel = false;
                Utility.SetMsg(lblMsg, "Đã xóa các chi tiết Vật tư tiêu hao thành công", false);
            }
            catch (Exception ex)
            {

                Utility.SetMsg(lblMsg, ex.Message, true);
            }
        }
        void frm_chuyenVTTHvaotronggoiDV_Load(object sender, EventArgs e)
        {
            LoadData();
            cmdAccept.Enabled = Utility.isValidGrid(grdGoidichvu) && grdVTTH.GetDataRows().Length > 0;
        }

        void grdGoidichvu_SelectionChanged(object sender, EventArgs e)
        {
            cmdAccept.Enabled = Utility.isValidGrid(grdGoidichvu);
            LayVTTHtronggoi();
        }

        void mnuMoveto_Click(object sender, EventArgs e)
        {
            if(!Utility.isValidCheckedGrid(grdVTTH))
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất 1 vật tư tiêu hao cần chuyển vào gói");
                return;
            }
            if (!Utility.isValidGrid(grdGoidichvu))
            {
                Utility.ShowMsg("Bạn cần chọn gói dịch vụ để chuyển vật tư tiêu hao");
                return;
            }
            try
            {
                if (grdVTTH_tronggoi.GetDataRows().Length <= 0)//Chưa có phiếu VTTH cho gói này
                {
                    if (grdVTTH.GetCheckedRows().Length == grdVTTH.GetDataRows().Length)
                    {
                        //Chuyển tất cả-->Chỉ việc update Id_goi vào trong_goi
                        List<long> lstIdDonthuoc = (from q in grdVTTH.GetCheckedRows().AsEnumerable()
                                                    select Utility.Int64Dbnull(q.Cells[KcbDonthuoc.Columns.IdDonthuoc].Value, -1)).Distinct().ToList<long>();
                        int id_goi = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChitietchidinh));
                        foreach (long _id in lstIdDonthuoc)
                        {
                            new noitru_phieudieutri().ChuyentoanboVTTHvaogoi(_id, id_goi);
                        }
                    }
                    else//Chuyển 1 phần
                    {
                        List<long> lstIdDonthuoc = (from q in grdVTTH.GetCheckedRows().AsEnumerable()
                                                    select Utility.Int64Dbnull(q.Cells[KcbDonthuoc.Columns.IdDonthuoc].Value, -1)).Distinct().ToList<long>();
                         int id_goi = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChitietchidinh));
                         foreach (long _id in lstIdDonthuoc)
                         {
                             List<long> lstIdchitietDonthuoc = (from q in grdVTTH.GetCheckedRows().AsEnumerable()
                                                                where Utility.Int64Dbnull(q.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, -1) == _id
                                                         select Utility.Int64Dbnull(q.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1)).Distinct().ToList<long>();

                             new noitru_phieudieutri().ChuyenVTTHvaogoi(_id, lstIdchitietDonthuoc, id_goi);
                         }
                    }
                }
                else//Chuyển VTTH vào mặc định 1 trong các phiếu VTTH thuộc gói
                {
                    List<long> lstIdDonthuoc = (from q in grdVTTH.GetCheckedRows().AsEnumerable()
                                                select Utility.Int64Dbnull(q.Cells[KcbDonthuoc.Columns.IdDonthuoc].Value, -1)).Distinct().ToList<long>();
                    int id_goi = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChitietchidinh));
                    grdVTTH_tronggoi.MoveFirst();
                    int IdDonthuoc_Des = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdVTTH_tronggoi, KcbDonthuocChitiet.Columns.IdDonthuoc),-1);
                    foreach (long _id in lstIdDonthuoc)
                    {
                        List<long> lstIdchitietDonthuoc = (from q in grdVTTH.GetCheckedRows().AsEnumerable()
                                                           where Utility.Int64Dbnull(q.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, -1) == _id
                                                           select Utility.Int64Dbnull(q.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1)).Distinct().ToList<long>();

                        new noitru_phieudieutri().ChuyenVTTHvaogoi(_id,IdDonthuoc_Des, lstIdchitietDonthuoc, id_goi);
                    }
                }
                m_blnCancel = false;
            }
            catch (Exception ex)
            {
                
                
            }
        }
        void LayVTTHtronggoi()
        {
            try
            {
                if (Utility.isValidGrid(grdGoidichvu))
                {
                    m_dtVTTH_tronggoi =new KCB_THAMKHAM().NoitruLaythongtinVTTHTrongoi((int)objLuotkham.IdBenhnhan,
                                                              objLuotkham.MaLuotkham,
                                                              -1, Utility.Int32Dbnull(Utility.getValueOfGridCell(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChitietchidinh))
                                                              ).Tables[0];

                    Utility.SetDataSourceForDataGridEx(grdVTTH_tronggoi, m_dtVTTH_tronggoi, false, true, "", KcbDonthuocChitiet.Columns.SttIn);
                }
                else
                {
                    if (m_dtVTTH_tronggoi != null) m_dtVTTH_tronggoi.Clear();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        
        void LoadData()
        {
            try
            {
                decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
                DataSet ds =new KCB_THAMKHAM().NoitruLayDanhsachVtthGoidichvu((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
                m_dtVTTH = ds.Tables[0];
                m_dtGoidichvu = ds.Tables[1];
                Utility.SetDataSourceForDataGridEx_Basic(grdGoidichvu, m_dtGoidichvu, false, true, "",
                                                  "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");



                m_dtVTTHChitiet_View = m_dtVTTH.Clone();
                foreach (DataRow row in m_dtVTTH.Rows)
                {
                    row["CHON"] = 0;
                    DataRow[] drview
                        = m_dtVTTHChitiet_View
                        .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                        + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(row[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                    if (drview.Length <= 0)
                    {
                        //if (!Utility.Byte2Bool(row[KcbDonthuocChitiet.Columns.TuTuc]))
                        //{
                        //    decimal BHCT = 0m;
                        //    if (objLuotkham.TrangthaiNoitru <= 0)
                        //        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                        //    else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                        //        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);

                        //    //decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                        //    decimal num3 = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                        //    row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                        //    row[KcbDonthuocChitiet.Columns.BnhanChitra] = num3;


                        //}
                        //else
                        //{
                        //    row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;

                        //    row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                        //    row[KcbDonthuocChitiet.Columns.BnhanChitra] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                        //}

                        //row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                        //row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                        //row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                        //row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                        //row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                        //row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                        if (Utility.Byte2Bool(row[KcbDonthuocChitiet.Columns.TrongGoi]))
                        {
                            row[KcbDonthuocChitiet.Columns.DonGia] = 0;
                            row[KcbDonthuocChitiet.Columns.BnhanChitra] = 0;
                            row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                            row[KcbDonthuocChitiet.Columns.PhuThu] = 0;
                            row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                            row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                            row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                            row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                            row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                            row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                        }
                        m_dtVTTHChitiet_View.ImportRow(row);
                    }
                    else
                    {

                        drview[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0);
                        drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                        drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        drview[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                        drview[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                        m_dtVTTHChitiet_View.AcceptChanges();
                    }
                }
                //Old-->Utility.SetDataSourceForDataGridEx
                Utility.SetDataSourceForDataGridEx_Basic(grdVTTH, m_dtVTTHChitiet_View, false, true, "",KcbDonthuocChitiet.Columns.TrongGoi+","+ KcbDonthuocChitiet.Columns.SttIn);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
          
        }
    }
}
