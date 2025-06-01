using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Reflection;
using Janus.Windows.GridEX;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Xemtonghopchiphi : Form
    {
        public KcbLuotkham objLuotkham = null;
        DataTable m_dtChiPhiThanhtoan = null;
        bool Khoanoitrutonghop = true;
        bool mv_blnHasloaded = false;
        string idkhoanoitru = "-1";
        string SplitterPath = "";
        public frm_Xemtonghopchiphi(bool Khoanoitrutonghop,string idkhoanoitru)
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this,false);
            this.idkhoanoitru = idkhoanoitru;
            this.Khoanoitrutonghop = Khoanoitrutonghop;
            pnlCachthuchienthidulieu.Visible = !Khoanoitrutonghop;
            ucThongtinnguoibenh_doc_v51.SetReadonly();
            optAll.Checked = true;
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += frm_Xemtonghopchiphi_Load;
            this.KeyDown += frm_Xemtonghopchiphi_KeyDown;
            Shown+=frm_Xemtonghopchiphi_Shown;
            FormClosing += frm_Xemtonghopchiphi_FormClosing;
            cmdExit.Click += cmdExit_Click;
            cmdRefresh.Click += cmdRefresh_Click;
            cmdPrint.Click += cmdPrint_Click;
            optAll.CheckedChanged += optAll_CheckedChanged;
            optNoitru.CheckedChanged += optAll_CheckedChanged;
            optNgoaitru.CheckedChanged += optAll_CheckedChanged;
            grdThongTinChuaThanhToan.UpdatingCell += grdThongTinChuaThanhToan_UpdatingCell;
            txtptramtanggia_nguoinuocngoai.KeyDown += txtptramtanggia_nguoinuocngoai_KeyDown;
            txtTilemiengiamAll.KeyDown += txtTilemiengiamAll_KeyDown;
        }
        void txtTilemiengiamAll_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                decimal tile = Utility.DecimaltoDbnull(txtTilemiengiamAll.Text, 0);
                if (e.KeyCode == Keys.Enter  )
                {
                    string ask = string.Format("Bạn có chắc chắn muốn miễn giảm {0} % cho toàn bộ các dịch vụ đang được chọn?",Convert.ToInt16( tile).ToString());
                    if(!Utility.AcceptQuestion(ask,"Xác nhận miễn giảm cho các dịch vụ đang chọn",true)) return;
                    if (chkPercent.Checked)
                    {
                        if (tile > 100m)
                        {
                            Utility.ShowMsg("Tỉ lệ miễn giảm không được vượt quá 100%");
                            return;
                        }
                        foreach (GridEXRow _row in grdThongTinChuaThanhToan.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["trangthai_thanhtoan"].Value, "0") == "0" )
                            {
                                _row.BeginEdit();
                                _row.Cells["tien_chietkhau"].Value = Utility.DecimaltoDbnull(_row.Cells["tt"].Value, 0) * tile / 100;
                                _row.Cells["tile_chietkhau"].Value = tile;
                                _row.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(_row.Cells["tt"].Value, 0) - Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                                _row.EndEdit();
                                //Cập nhật luôn vào bảng trong CSDL để in bảng kê chi phí cho người bệnh xem trước khi thanh toán
                                byte id_loaithanhtoan = Utility.ByteDbnull(_row.Cells["id_loaithanhtoan"].Value);
                                string kieu_chietkhau = "%";
                                decimal tile_chietkhau = Utility.DecimaltoDbnull(_row.Cells["tile_chietkhau"].Value, 0);
                                decimal tien_chietkhau = Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                                long id_phieu = Utility.Int64Dbnull(_row.Cells["id_phieu"].Value);
                                long id_phieuchitiet = Utility.Int64Dbnull(_row.Cells["id_phieu_chitiet"].Value);
                                CapnhatChietkhau_DonGia(0,id_loaithanhtoan, kieu_chietkhau, tile_chietkhau, tien_chietkhau, id_phieu, id_phieuchitiet);
                                
                            }
                        }
                    }
                    else//Nhập tiền nếu vượt quá số tiền thì tự = số tiền
                    {
                        foreach (GridEXRow _row in grdThongTinChuaThanhToan.GetCheckedRows())
                        {
                            if (Utility.sDbnull(_row.Cells["trangthai_thanhtoan"].Value, "0") == "0" )
                            {
                                _row.BeginEdit();
                                if (tile > Utility.DecimaltoDbnull(_row.Cells["tt"].Value, 0))
                                {

                                    _row.Cells["tien_chietkhau"].Value = _row.Cells["tt"].Value;
                                    _row.Cells["tile_chietkhau"].Value = 100;
                                }
                                else
                                {
                                    _row.Cells["tien_chietkhau"].Value = tile;
                                    _row.Cells["tile_chietkhau"].Value = (tile / Utility.DecimaltoDbnull(_row.Cells["tt"].Value, 0)) * 100;
                                }
                                _row.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(_row.Cells["tt"].Value, 0) - Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                                _row.EndEdit();
                                //Cập nhật luôn vào bảng trong CSDL để in bảng kê chi phí cho người bệnh xem trước khi thanh toán
                                byte id_loaithanhtoan = Utility.ByteDbnull(_row.Cells["id_loaithanhtoan"].Value);
                                string kieu_chietkhau = "%";
                                decimal tile_chietkhau = Utility.DecimaltoDbnull(_row.Cells["tile_chietkhau"].Value, 0);
                                decimal tien_chietkhau = Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                                long id_phieu = Utility.Int64Dbnull(_row.Cells["id_phieu"].Value);
                                long id_phieuchitiet = Utility.Int64Dbnull(_row.Cells["id_phieu_chitiet"].Value);
                                CapnhatChietkhau_DonGia(0,id_loaithanhtoan, kieu_chietkhau, tile_chietkhau, tien_chietkhau, id_phieu, id_phieuchitiet);
                            }
                        }
                    }
                    grdThongTinChuaThanhToan.UpdateData();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
            }
        }
        void CapnhatChietkhau_DonGia(byte loai_capnhat, byte id_loaithanhtoan, string kieu_chietkhau, decimal tile_chietkhau, decimal tien_chietkhau, long id_phieu, long id_phieuchitiet)
        {
            try
            {
                StoredProcedure sp = SPs.SpUpdateThongtinchietkhauDongia(loai_capnhat, id_loaithanhtoan, kieu_chietkhau, tile_chietkhau, tien_chietkhau, id_phieu, id_phieuchitiet);
                sp.Execute();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void CapnhatDongia(GridEXRow _row, decimal tile,decimal don_giamoi,bool manual)
        {
            bool nhan_chia = tile > 0;//>0 thì làm phép nhân; <0 thì làm phép chia
            decimal thuong_so = Utility.DecimaltoDbnull(1m + (tile / 100));
            if (Utility.sDbnull(_row.Cells["trangthai_thanhtoan"].Value, "0") == "0")
            {
                _row.BeginEdit();
                //Cập nhật luôn vào bảng trong CSDL để in bảng kê chi phí cho người bệnh xem trước khi thanh toán
                byte id_loaithanhtoan = Utility.ByteDbnull(_row.Cells["id_loaithanhtoan"].Value);
               
                long id_phieu = Utility.Int64Dbnull(_row.Cells["id_phieu"].Value);
                long id_phieuchitiet = Utility.Int64Dbnull(_row.Cells["id_phieu_chitiet"].Value);
                if (!manual)
                {
                    //_row.Cells["don_gia"].Value = Utility.DecimaltoDbnull(_row.Cells["don_gia_goc"].Value, 0) * (1 + tile / 100);
                    //if (nhan_chia)//Fix at 250304
                        _row.Cells["don_gia"].Value = Utility.DecimaltoDbnull(_row.Cells["don_gia_goc"].Value, 0) * thuong_so;
                    //else
                    //    _row.Cells["don_gia"].Value = Utility.DecimaltoDbnull(_row.Cells["don_gia"].Value, 0) / thuong_so;
                }
                else
                {
                    _row.Cells["don_gia"].Value = don_giamoi;
                }
                _row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value = _row.Cells["don_gia"].Value;

                decimal don_gia = Utility.DecimaltoDbnull(_row.Cells["don_gia"].Value, 0);
                CapnhatChietkhau_DonGia(1, id_loaithanhtoan, "%", 0, don_gia, id_phieu, id_phieuchitiet);
                //Tính lại các khoản tiền
                //_row.Cells["TT_BHYT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra].Value, 0)) *
                //  (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) *
                //  Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);

                //_row.Cells["TT_BN"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) *
                //                  (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) +
                //                  Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) *
                //                 Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);

                _row.Cells["TT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0)+
                               Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) *
                              Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);

                //_row.Cells["TT_PHUTHU"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) *
                //                     Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);

                //_row.Cells["TT_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) *
                //                           (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) *
                //                           Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                //_row.Cells["TT_BN_KHONG_TUTUC"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) *
                //                          (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) *
                //                          Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                //_row.Cells["TT_BN_KHONG_PHUTHU"].Value =
                //    Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) *
                //    (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) *
                //    Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                _row.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(_row.Cells["tt"].Value, 0) - Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                _row.EndEdit();
            }
        }
        void txtptramtanggia_nguoinuocngoai_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    if (!Utility.Coquyen("thanhtoan_tanggiam_tile_dongia"))
                    {
                        Utility.ShowMsg("Bạn không có quyền tăng giảm đơn giá thanh toán (thanhtoan_tanggiam_tile_dongia). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                        return;
                    }
                    if (!chkDoituongnguoinuocngoai.Checked)
                    {
                        Utility.ShowMsg("Tính năng này yêu cầu bạn phải chọn mục % tăng giá đối với người nước ngoài. Vui lòng check chọn");
                        chkDoituongnguoinuocngoai.Focus();
                        return;
                    }
                    decimal tile = Utility.DecimaltoDbnull(txtptramtanggia_nguoinuocngoai.Text, 0);//<0 = giảm giá;>0= tăng giá
                    if (grdThongTinChuaThanhToan.GetCheckedRows().Count() <= 0)
                    {
                        Utility.ShowMsg("Bạn cần chọn ít nhất 1 dịch vụ trước khi thực hiện cập tăng/giảm đơn giá");
                        return;
                    }
                    string ask = string.Format("Bạn có chắc chắn muốn {0} giá {1} % cho toàn bộ các dịch vụ đang được chọn?", tile > 0 ? "tăng" : "giảm", Convert.ToInt16(tile).ToString());

                    if (!Utility.AcceptQuestion(ask, string.Format("Xác nhận {0} cho các dịch vụ đang chọn", tile > 0 ? "tăng giá" : "giảm giá"), true)) return;
                    string dsach_dvu = String.Join(",", (from p in grdThongTinChuaThanhToan.GetCheckedRows()
                                                         select Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value, "")).ToArray<string>());


                    foreach (GridEXRow _row in grdThongTinChuaThanhToan.GetCheckedRows())
                    {
                        CapnhatDongia(_row, tile, 0, false);
                    }
                    grdThongTinChuaThanhToan.UpdateData();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("{0} {1} % đơn giá cho các dịch vụ {2} thành công ", tile > 0 ? "tăng" : "giảm", tile, dsach_dvu), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg(string.Format("Bạn vừa thực hiện {0} {1} % đơn giá cho các dịch vụ đang chọn thành công.\nNhấn OK để kết thúc", tile > 0 ? "tăng" : "giảm", tile));


                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
            }
        }
        void frm_Xemtonghopchiphi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) LoadData();
        }

        void grdThongTinChuaThanhToan_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                //Kiểm tra xem bản ghi đã thanh toán hay chưa?
                List<int> lstId = new List<int>() { 2,8,9};
                byte id_loaithanhtoan = Utility.ByteDbnull(grdThongTinChuaThanhToan.GetValue("id_loaithanhtoan"),1000);
                /////REM lại để mở code bên dưới cho cập nhật tất cả các thể loại đơn giá
                //if (!lstId.Contains( id_loaithanhtoan ))
                //{
                //    Utility.ShowMsg("Hệ thống chỉ cho phép bạn hiệu chỉnh đơn giá của dịch vụ cận lâm sàng. Vui lòng kiểm tra lại");
                //    e.Value = e.InitialValue;
                //    return;
                //}
                //long id_chitietchidinh = Utility.Int64Dbnull(grdThongTinChuaThanhToan.GetValue("id_phieu_chitiet"), -1);
                //if (id_chitietchidinh > 0)
                //{
                //    KcbChidinhclsChitiet objchitiet = KcbChidinhclsChitiet.FetchByID(id_chitietchidinh);
                //    if (objchitiet != null && Utility.ByteDbnull(objchitiet.TrangthaiThanhtoan, 0) > 0)
                //    {
                //        Utility.ShowMsg("Bản ghi đã được thanh toán nên bạn không được phép chỉnh sửa số lượng hoặc đơn giá");
                //        e.Value = e.InitialValue;
                //        return;
                //    }
                //}

                if (e.Column.Key == KcbChidinhclsChitiet.Columns.SoLuong)
                {
                    //if (!Utility.IsNumeric(e.Value.ToString()))
                    //{
                    //    Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                    //    e.Cancel = true;
                    //}
                    //decimal quanlity = Utility.DecimaltoDbnull(e.InitialValue, 1);
                    //decimal quanlitynew = Utility.DecimaltoDbnull(e.Value);
                    //if (quanlitynew <= 0)
                    //{
                    //    Utility.ShowMsg("Bạn phải số lượng phải >0", "Thông báo", MessageBoxIcon.Warning);
                    //    e.Value = e.InitialValue;
                    //}
                    //GridEXRow _row = grdAssignDetail.CurrentRow;
                    //string ten_dvu = _row.Cells["ten_chitietdichvu"].Value.ToString();
                    //_row.Cells["TT_BHYT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra].Value, 0)) * quanlitynew;
                    //_row.Cells["TT_BN"].Value =
                    //    (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) +
                    //     Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    //_row.Cells["TT_PHUTHU"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    //_row.Cells["TT_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0)) / 100 * quanlitynew;
                    //_row.Cells["TT_BN_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) * quanlitynew;

                    //_row.Cells["TT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) *
                    //               (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) +
                    //               Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    //grdAssignDetail.UpdateData();
                    //Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa số lượng dịch vụ cận lâm sàng {0} từ {1} thành {2} thành công ", ten_dvu, Utility.FormatCurrencyHIS(quanlity), Utility.FormatCurrencyHIS(quanlitynew)), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                }
                else if (e.Column.Key == "tile_chietkhau" || e.Column.Key == "tien_chietkhau")
                {
                    decimal tile_chietkhau = 0;
                    decimal tien_chietkhau = 0;
                    string kieu_chietkhau = "%";
                    if (Utility.isValidGrid(grdThongTinChuaThanhToan) && Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["trangthai_thanhtoan"].Value, 1) == 1)
                    {

                        Utility.ShowMsg("Chi tiết bạn chọn đã được thanh toán nên bạn không thể chiết khấu được nữa. Mời bạn kiểm tra lại");
                        e.Value = e.InitialValue;
                        return;
                    }
                    else
                    {
                        if (e.Column.Key == "tile_chietkhau")
                        {
                            tile_chietkhau = Utility.DecimaltoDbnull(e.Value, 0);
                            //Tính lại tiền chiết khấu theo tỉ lệ %
                            if (tile_chietkhau > 100)
                            {
                                Utility.ShowMsg("Tỉ lệ chiết khấu không được phép vượt quá 100 %. Mời bạn kiểm tra lại");
                                e.Cancel = true;
                                return;
                            }
                            grdThongTinChuaThanhToan.CurrentRow.Cells["tien_chietkhau"].Value = Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tt"].Value, 0) * Utility.DecimaltoDbnull(e.Value, 0) / 100;
                            tien_chietkhau = Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tien_chietkhau"].Value);
                            grdThongTinChuaThanhToan.CurrentRow.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tt"].Value, 0) - Utility.DecimaltoDbnull(tien_chietkhau);
                        }
                        else
                        {
                            kieu_chietkhau = "T";
                            tien_chietkhau = Utility.DecimaltoDbnull(e.Value, 0);
                            if (tien_chietkhau > Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tt"].Value, 0))
                            {
                                Utility.ShowMsg("Tiền chiết khấu không được lớn hơn(>) tiền Bệnh nhân chi trả(" + Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tt"].Value, 0).ToString() + "). Mời bạn kiểm tra lại");
                                e.Cancel = true;
                                return;
                            }
                            grdThongTinChuaThanhToan.CurrentRow.Cells["tile_chietkhau"].Value =Math.Round( (Utility.DecimaltoDbnull(e.Value, 0) / Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tt"].Value, 0)) * 100,2);
                            tile_chietkhau =Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tile_chietkhau"].Value);
                        }
                        grdThongTinChuaThanhToan.CurrentRow.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["tt"].Value, 0) - Utility.DecimaltoDbnull(tien_chietkhau);
                        //Cập nhật luôn vào bảng trong CSDL để in bảng kê chi phí cho người bệnh xem trước khi thanh toán
                        
                        
                        long id_phieu = Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_phieu"].Value);
                        long id_phieuchitiet = Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_phieu_chitiet"].Value);
                        CapnhatChietkhau_DonGia(0,id_loaithanhtoan, kieu_chietkhau, tile_chietkhau, tien_chietkhau, id_phieu, id_phieuchitiet);
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa chiết khấu {0} từ {1} thành {2} thành công ", grdThongTinChuaThanhToan.GetValue("ten_chitietdichvu"), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.InitialValue, 0)), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.Value, 0))), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    }
                }
                else if (e.Column.Key == KcbChidinhclsChitiet.Columns.DonGia)
                {
                    if (!Utility.IsNumeric(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải nhập thông tin đơn giá. Vui lòng nhập lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                        return;
                    }
                    decimal dongia_cu = Utility.DecimaltoDbnull(e.InitialValue, 1);
                    decimal dongia_moi = Utility.DecimaltoDbnull(e.Value);
                    if (dongia_moi == 0)
                    {
                        if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn đổi giá dịch vụ cls {0} thành 0 đồng hay không?", grdThongTinChuaThanhToan.GetValue("ten_chitietdichvu")), "Xác nhận giá 0 đồng", true))
                        {
                            e.Value = e.InitialValue;
                            return;
                        }
                    }
                    if (dongia_moi < 0)
                    {
                        Utility.ShowMsg("Đơn giá phải >=0. Vui lòng nhập lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                        return;
                    }
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn đổi giá dịch vụ cls {0} từ {1} thành {2} hay không?", grdThongTinChuaThanhToan.GetValue("ten_chitietdichvu"), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.InitialValue, 0)), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.Value, 0))), "Xác nhận đổi giá", true))
                    {
                        CapnhatDongia(grdThongTinChuaThanhToan.CurrentRow, 0, dongia_moi,true);
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa đơn giá {0} từ {1} thành {2} thành công ", grdThongTinChuaThanhToan.GetValue("ten_chitietdichvu"), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.InitialValue, 0)), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.Value, 0))), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                       // int record = new Update(KcbChidinhclsChitiet.Schema)
                       //.Set(KcbChidinhclsChitiet.Columns.DonGia).EqualTo(dongia_moi)
                       //.Set(KcbChidinhclsChitiet.Columns.BnhanChitra).EqualTo(dongia_moi)
                       //.Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_chitietchidinh).Execute();
                       // if (record > 0)
                       // {
                       //     GridEXRow _row = grdThongTinChuaThanhToan.CurrentRow;
                       //     int so_luong = Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                       //     _row.BeginEdit();
                       //     _row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value = dongia_moi;
                       //     _row.Cells["TT_BHYT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra].Value, 0)) * so_luong;
                       //     _row.Cells["TT_BN"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) + Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                       //     _row.Cells["TT_PHUTHU"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                       //     _row.Cells["TT_KHONG_PHUTHU"].Value = (dongia_moi * Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) * so_luong;
                       //     _row.Cells["TT_BN_KHONG_PHUTHU"].Value = _row.Cells["TT_KHONG_PHUTHU"].Value;
                       //     //_row.Cells["TT_BN_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) * quanlitynew;

                       //     _row.Cells["TT"].Value = (dongia_moi *
                       //                    (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) +
                       //                    Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                       //     _row.Cells["thuc_thu"].Value = Utility.DecimaltoDbnull(_row.Cells["TT_BN"].Value, 0) - Utility.DecimaltoDbnull(_row.Cells["tien_chietkhau"].Value, 0);
                       //     _row.EndEdit();
                       //     grdThongTinChuaThanhToan.UpdateData();
                       //     Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa đơn giá dịch vụ cận lâm sàng {0} từ {1} thành {2} thành công ", grdThongTinChuaThanhToan.GetValue("ten_chitietdichvu"), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.InitialValue, 0)), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.Value, 0))), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                       //     Utility.ShowMsg(string.Format("Sửa đơn giá dịch vụ cận lâm sàng {0} từ {1} thành {2} thành công ", grdThongTinChuaThanhToan.GetValue("ten_chitietdichvu"), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.InitialValue, 0)), Utility.FormatCurrencyHIS(Utility.DecimaltoDbnull(e.Value, 0))));
                       // }
                       // else
                       // {
                       //     Utility.ShowMsg("Không có bản ghi nào được cập nhật đơn giá bạn gõ (Có thể đã bị xóa ngay sau khi bạn nhấn nút tổng hợp). Vui lòng nhấn nút Refresh để kiểm tra xem dữ liệu còn không hoặc liên hệ nội bộ khoa phòng để xác minh");
                       // }
                    }
                    //GridEXRow _row = grdThongTinChuaThanhToan.CurrentRow;
                    //string ten_dvu = _row.Cells["ten_chitietdichvu"].Value.ToString();
                    //int so_luong = Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                    //_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value = dongia_moi;
                    //_row.Cells["TT_BHYT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra].Value, 0)) * so_luong;
                    //_row.Cells["TT_BN"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) + Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                    //_row.Cells["TT_PHUTHU"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                    //_row.Cells["TT_KHONG_PHUTHU"].Value = (dongia_moi * Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) * so_luong;
                    ////_row.Cells["TT_BN_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) * quanlitynew;

                    //_row.Cells["TT"].Value = (dongia_moi *
                    //               (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) +
                    //               Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                    //grdThongTinChuaThanhToan.UpdateData();
                    //int record = new Update(KcbChidinhclsChitiet.Schema)
                    //    .Set(KcbChidinhclsChitiet.Columns.DonGia).EqualTo(dongia_moi)
                    //    .Set(KcbChidinhclsChitiet.Columns.BnhanChitra).EqualTo(dongia_moi)
                    //    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_chitietchidinh).Execute();
                    //if (record > 0)
                       

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void frm_Xemtonghopchiphi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString()});
        }
       
        void Try2Splitter()
        {
            try
            {


                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count == 1)
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];
                }
            }
            catch (Exception)
            {

            }
        }
        void frm_Xemtonghopchiphi_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }
        void optAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!mv_blnHasloaded) return;
                string RowFilter = "1=1";
                PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Tatca;
                if (optNoitru.Checked)
                {
                    PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Noitru;
                    RowFilter = "noi_tru=1";
                }
                if (optNgoaitru.Checked)
                {
                    PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Ngoaitru;
                    RowFilter = "noi_tru=0";
                }
                m_dtChiPhiThanhtoan.DefaultView.RowFilter = RowFilter;
                m_dtChiPhiThanhtoan.AcceptChanges();
                PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
            }
            catch (Exception ex)
            {


            }
        }
        void cmdPrint_Click(object sender, EventArgs e)
        {
            if (chkTonghop.Checked)
            {
                Intonghop();
            }
            else
                Inchiphi();
        }
        void Intonghop()
        {
            try
            {
                Utility.WaitNow(this);
                DataTable dtData = SPs.NoitruTonghopChiphiRavien(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, Utility.Bool2byte(!Khoanoitrutonghop), Utility.sDbnull(cboKhoadieutri.SelectedValue, "-1"), getidphieudieutri()).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                    return;
                }
                DataTable dtDataPrint = dtData.Clone();
                DataRow[] arrDr=null;
                if (optAll1.Checked)
                {
                    dtDataPrint = dtData.Copy();
                }
                else if (optNoitru1.Checked)
                {
                    arrDr = dtData.Select("Noi_tru=1");
                    if (arrDr.Length > 0) dtDataPrint = arrDr.CopyToDataTable();
                }
                else if (optNgoaitru1.Checked)
                {
                    arrDr = dtData.Select("Noi_tru=0");
                    if (arrDr.Length > 0) dtDataPrint = arrDr.CopyToDataTable();
                }
                Utility.UpdateLogotoDatatable(ref dtDataPrint);


                ReportDocument reportDocument = new ReportDocument();
                string tieude = "", reportname = "", reportCode = "";
                reportCode = "thanhtoan_bangkechiphiKCB_Noitru_Tonghop";

                reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);

                if (reportDocument == null) return;
                var crpt = reportDocument;

                decimal tt_bnhan_chitra = Utility.DecimaltoDbnull(dtDataPrint.Rows[0]["tong_tien"]) - Utility.DecimaltoDbnull(dtDataPrint.Rows[0]["tong_tien_tamung"]) - Utility.DecimaltoDbnull(dtDataPrint.Rows[0]["tong_tien_mg"]);
                var objForm = new frmPrintPreview("", crpt, true, true);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                //try
                //{
                crpt.SetDataSource(dtDataPrint.DefaultView);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithLocation(DateTime.Now, globalVariables.gv_strDiadiem));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                       new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tt_bnhan_chitra)));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();

                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }
        void Inchiphi()
        {
            try
            {
                Utility.WaitNow(this);
                DataTable dtData = SPs.NoitruTonghopChiphiRavien(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, Utility.Bool2byte(!Khoanoitrutonghop), Utility.sDbnull(cboKhoadieutri.SelectedValue, "-1"), getidphieudieutri()).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.Sapxepthutuin(ref dtData, false);
                dtData.DefaultView.Sort = "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

                THU_VIEN_CHUNG.CreateXML(dtData, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV_chuathanhtoan.XML");
                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                    return;
                }
                DataTable dtDataPrint = dtData.Clone();
                DataRow[] arrDr = null;
                if (optAll1.Checked)
                {
                    dtDataPrint = dtData.Copy();
                }
                else if (optNoitru1.Checked)
                {
                    arrDr = dtData.Select("Noi_tru=1");
                    if (arrDr.Length > 0) dtDataPrint = arrDr.CopyToDataTable();
                }
                else if (optNgoaitru1.Checked)
                {
                    arrDr = dtData.Select("Noi_tru=0");
                    if (arrDr.Length > 0) dtDataPrint = arrDr.CopyToDataTable();
                }

                Utility.UpdateLogotoDatatable(ref dtDataPrint);
                dtDataPrint.DefaultView.Sort = "stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
                dtDataPrint.AcceptChanges();
                var p = (from q in dtDataPrint.AsEnumerable()
                         group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                         select new
                         {
                             _key = r.Key,
                             tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                             tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                             tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                         }).ToList();

                decimal tong = Utility.getSUM(dtDataPrint, "TT_BN");
                decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
                decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
                tong = tong - tong_ck;
                ReportDocument reportDocument = new ReportDocument();
                string tieude = "", reportname = "", reportCode = "";
                reportCode = "thanhtoan_bangkechiphiKCB_Noitru";
                //if (PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet)
                //{
                //    reportCode = "thanhtoan_Bienlai_Dichvu_A4_Innhiet";
                reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                //}
                //else
                //{
                //    switch (khogiay)
                //    {
                //        case "A4":
                //            reportCode = tong_ck <= 0 ? "thanhtoan_Bienlai_Dichvu_A4" : "thanhtoan_Bienlai_Dichvu_Comiengiam_A4";
                //            reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                //            break;
                //        case "A5":
                //            reportCode = tong_ck <= 0 ? "thanhtoan_Bienlai_Dichvu_A5" : "thanhtoan_Bienlai_Dichvu_Comiengiam_A5";
                //            reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                //            break;

                //    }
                //}
                if (reportDocument == null) return;
                var crpt = reportDocument;


                var objForm = new frmPrintPreview("", crpt, true, true);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                //try
                //{
                crpt.SetDataSource(dtDataPrint.DefaultView);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
                Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
                Utility.SetParameterValue(crpt, "tongtien_bn", tong);
                //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithLocation(DateTime.Now, globalVariables.gv_strDiadiem));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                       new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();

                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                #region backup
                //THU_VIEN_CHUNG.CreateXML(dtData, "noitru_tonghopchiphiravien.XML");
                //string id_khoadieutri = cboKhoadieutri.SelectedValue.ToString();
                //DataTable dt4Print = dtData.Clone();
                //DataRow[] arrDr= dtData.Select("id_khoadieutri="+id_khoadieutri);
                //if (arrDr.Length > 0) dt4Print = arrDr.CopyToDataTable();
                //if (id_khoadieutri == "-1") dt4Print = dtData.Copy();
                //if (dt4Print.Rows.Count <= 0)
                //{
                //    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                //    return;
                //}
                //foreach (DataRow drv in dt4Print.Rows)
                //{
                //    if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "1"//Chi phí KCB
                //        || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "0"//Phí KCB kèm theo
                //        || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "4"//Buồng giường
                //        || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "8"//Gói dịch vụ
                //        )
                //    {

                //        drv["ten_loaidichvu"] = string.Empty;
                //        drv["STT"] = 1;
                //        drv["id_loaidichvu"] = -1;
                //    }
                //    else if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "2")
                //    {
                //        string ma_loaidichvu = Utility.sDbnull(drv["id_loaidichvu"], -1);
                //        //drv["id_loaidichvu"]-->Được xác định trong câu truy vấn

                //        DmucChung objService = THU_VIEN_CHUNG.LaydoituongDmucChung("LOAIDICHVUCLS", ma_loaidichvu);
                //        if (objService != null)
                //        {
                //            drv["ten_loaidichvu"] = Utility.sDbnull(objService.Ten);
                //            drv["STT"] = Utility.sDbnull(objService.SttHthi);
                //        }

                //    }
                //    else if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "3")
                //    {
                //        int Drug_ID = Utility.Int32Dbnull(drv["id_dichvu"], -1);
                //        DmucThuoc objDrug = DmucThuoc.FetchByID(Drug_ID);
                //        if (objDrug != null)
                //        {
                //            if (objDrug.KieuThuocvattu == "THUOC")
                //            {
                //                drv["id_loaidichvu"] = 1;
                //                drv["STT"] = 1;
                //                drv["ten_loaidichvu"] = "Thuốc và dịch truyền";
                //            }
                //            else
                //            {
                //                drv["id_loaidichvu"] = 2;
                //                drv["STT"] = 2;
                //                drv["ten_loaidichvu"] = "Vật tư y tế ";
                //            }
                //        }
                //    }
                //    if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "5")
                //    {
                //        drv["id_loaidichvu"] = 1;
                //        drv["STT"] = 1;
                //        drv["ten_loaidichvu"] = "Chi phí thêm  ";
                //    }
                //}
                //THU_VIEN_CHUNG.Sapxepthutuin(ref dtData, true);
                //dt4Print.DefaultView.Sort = "stt_in ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
                //dt4Print.AcceptChanges();

                //Utility.UpdateLogotoDatatable(ref dt4Print);
                //string StaffName = globalVariables.gv_strTenNhanvien;
                //if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
                //string reportCode=Khoanoitrutonghop ? "noitru_tonghopchiphiravien_theokhoa_dichvu" : "noitru_tonghopchiphiravien_dichvu";
                //if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                //    reportCode = Khoanoitrutonghop ? "noitru_tonghopchiphiravien_theokhoa" : "noitru_tonghopchiphiravien";
                //string tieude = "", reportname = "";
                //ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                //if (crpt == null) return;
                //frmPrintPreview objForm = new frmPrintPreview(baocaO_TIEUDE1.TIEUDE, crpt, true, dtData.Rows.Count <= 0 ? false : true);
                //crpt.SetDataSource(dt4Print);

                //objForm.mv_sReportFileName = Path.GetFileName(reportname);
                //objForm.mv_sReportCode = reportCode;
                //Utility.SetParameterValue(crpt, "StaffName", StaffName);
                //Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                //Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                //Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                //Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                //Utility.SetParameterValue(crpt, "sTitleReport", baocaO_TIEUDE1.TIEUDE);
                //Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(DateTime.Now));
                //Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                //objForm.crptViewer.ReportSource = crpt;
                //objForm.ShowDialog();
                #endregion

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }
        void cmdRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void frm_Xemtonghopchiphi_Load(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init(Khoanoitrutonghop ? "noitru_tonghopchiphiravien_theokhoa" : "noitru_tonghopchiphiravien");
            LoadData();
            pnlTangGiamDonGia.Enabled = Utility.Coquyen("thanhtoan_tanggiam_tile_dongia");
            mv_blnHasloaded = true;
        }
        void LoadData()
        {
            try
            {
                ucThongtinnguoibenh_doc_v51.Refresh(objLuotkham.MaLuotkham);
                m_dtChiPhiThanhtoan =
                   SPs.NoitruTonghopChiphiRavien(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, Utility.Bool2byte(!Khoanoitrutonghop), idkhoanoitru, getidphieudieutri()).GetDataSet().Tables[0];
               // Utility.SetDataSourceForDataGridEx_Basic(grdThongTinChuaThanhToan, m_dtChiPhiThanhtoan,true, true, "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : ""), "");
                Utility.SetDataSourceForDataGridEx_Basic(grdThongTinChuaThanhToan, m_dtChiPhiThanhtoan, true, true, "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : ""), "");
                var q = (from p in m_dtChiPhiThanhtoan.AsEnumerable()
                         select new { id_khoadieutri = Utility.sDbnull(p["id_khoadieutri"]), ten_khoadieutri = Utility.sDbnull(p["ten_khoadieutri"]) }).Distinct();
                DataTable dtKhoadieutri = LINQResultToDataTable(q);
                DataBinding.BindDataCombobox(cboKhoadieutri,dtKhoadieutri,"id_khoadieutri","ten_khoadieutri","Chọn khoa điều trị",true);
                if (cboKhoadieutri.Items.Count == 1) cboKhoadieutri.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }
        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void cboKhoadieutri_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoadieutri.SelectedValue.ToString() == "-1")
                    m_dtChiPhiThanhtoan.DefaultView.RowFilter = "1=1";
                else
                    m_dtChiPhiThanhtoan.DefaultView.RowFilter = "id_khoadieutri=" + cboKhoadieutri.SelectedValue.ToString();
                m_dtChiPhiThanhtoan.AcceptChanges();
                LoadNgayDieutri();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        string getidphieudieutri()
        {
            string lstIdphieudieutri="-1";
            if (cboNgaydieutri.CheckedValues != null)
            {
                var query = (from chk in cboNgaydieutri.CheckedValues.AsEnumerable()
                             let x = Utility.sDbnull(chk)
                             select x).ToArray();
                if (query != null && query.Count() > 0)
                {
                    lstIdphieudieutri = string.Join(",", query);
                }
                else
                    lstIdphieudieutri = "-1";
            }
            return lstIdphieudieutri;
        }
        void LoadNgayDieutri()
        {
            try
            {
                DataTable m_dtPhieudieutri = SPs.NoitruTimkiemphieudieutriTheoKhoadieutri(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, Utility.sDbnull(cboKhoadieutri.SelectedValue, "-1")).GetDataSet().Tables[0];
                cboNgaydieutri.DropDownDataSource = m_dtPhieudieutri;
                cboNgaydieutri.DropDownDisplayMember = "sNgay_dieutri";
                cboNgaydieutri.DropDownDataMember = NoitruPhieudieutri.Columns.IdPhieudieutri;
                cboNgaydieutri.DropDownValueMember = NoitruPhieudieutri.Columns.IdPhieudieutri;
            }
            catch (Exception)
            {
                
                
            }
        }
        private void cmdPrint_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuSuagiaCLS_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("thanhtoan_tanggiam_tile_dongia"))
                {
                    Utility.thongbaokhongcoquyen("thanhtoan_tanggiam_tile_dongia", " sửa giá dịch vụ");
                    return;
                }

                grdThongTinChuaThanhToan.RootTable.Columns["don_gia"].EditType = Utility.Coquyen("thanhtoan_tanggiam_tile_dongia") ? EditType.TextBox : EditType.NoEdit;
                Utility.focusCellofCurrentRow(grdThongTinChuaThanhToan, "don_gia");
            }
            catch (Exception ex)
            {
                
            }
           
        }
        void ExportExcel()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                saveFileDialog1.FileName = "TonghopChiphiKCB.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        gridEXExporter1.GridEX = grdThongTinChuaThanhToan;
                        gridEXExporter1.Export(s);

                    }
                    Utility.ShowMsg("Xuất Excel thành công. Nhấn OK để mở file");
                    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void mnuExcel_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }

        private void mnuCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //byte id_loaithanhtoan = Utility.ByteDbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_loaithanhtoan"].Value);

                //long id_phieu = Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_phieu"].Value);
                //long id_phieuchitiet = Utility.Int64Dbnull(grdThongTinChuaThanhToan.CurrentRow.Cells["id_phieu_chitiet"].Value);
                //if (id_loaithanhtoan == 2 || id_loaithanhtoan == 8|| id_loaithanhtoan ==9 )
                //{
                //    KcbChidinhclsChitiet objCLSchitiet = KcbChidinhclsChitiet.FetchByID(id_phieuchitiet);
                //    if (objCLSchitiet.TrangThai >= 3) //Đã có kết quả
                //    {
                //        Utility.ShowMsg("Chỉ định bạn đang chọn đã có kết quả nên không cho phép hủy. Đề nghị bạn kiểm tra lại");
                //        return;
                //    }
                //    if (objCLSchitiet.TrangthaiThanhtoan > 0) //Đã thanh toán
                //    {
                //        Utility.ShowMsg("Chỉ định bạn đang chọn đã thanh toán nên không cho phép hủy. Đề nghị bạn kiểm tra lại");
                //        return;
                //    }
                //    new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(1)
                //        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_phieuchitiet).Execute();
                //    LoadData();
                //}
                //else
                //{
                //    Utility.ShowMsg("Chỉ cho phép hủy dịch vụ CLS trong chức năng này");
                //    return;
                //}
            }
            catch (Exception ex)
            {

            }
        }  
    }
}
