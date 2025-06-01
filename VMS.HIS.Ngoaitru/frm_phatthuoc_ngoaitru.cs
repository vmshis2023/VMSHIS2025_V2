using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using VNS.HIS.BusRule.Classes;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
using System.IO;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.NGOAITRU;
using System.Drawing;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.Classes;
using Janus.Windows.GridEX.EditControls;
using VNS.UCs;
using System.Transactions;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_phatthuoc_ngoaitru : Form
    {
        private int Distance = 488;
        private NLog.Logger log;
        private string _fileName = string.Format("{0}/{1}", Application.StartupPath, string.Format("SplitterDistancefrm_PhieuXuatBN.txt"));
        private DataTable mv_dtDonthuoc = new DataTable();
        private DataTable m_dtChitietdonthuoc = new DataTable();
        string kieuthuoc_vt = "THUOC";
        public long id_phieutralaiTTQ = -1;
        public bool AllowSelectionChanged = false;
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        byte trangthai_capcuu = 0;
        string SplitterPath = "";
        private DataTable m_dtChiPhiDaThanhToan = new DataTable();
        string Loai_Nguoidung = "DUOC";//DUOC hoặc TNV
        /// <summary>
        /// Tham số hệ thống =Kiểu thuốc VT - Loại bệnh nhân (Cấp cứu- thường)
        /// </summary>
        /// <param name="kieuthuocVt"></param>
        public frm_phatthuoc_ngoaitru(string kieuthuocVt)
        {
            InitializeComponent();
            try
            {
                SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
                Utility.SetVisualStyle(this);
                this.kieuthuoc_vt = kieuthuocVt.Split('-')[0];
                trangthai_capcuu =Utility.ByteDbnull( kieuthuocVt.Split('-')[1],0);
                Loai_Nguoidung = kieuthuocVt.Split('-')[2];
                InitEvents();
                setquyen();
                dtFromdate.Value = globalVariables.SysDate;
                dtToDate.Value = globalVariables.SysDate;

                log = NLog.LogManager.GetLogger(this.Name);
                dtNgayPhatThuoc.Value = globalVariables.SysDate;
                dtPaymentDate.Value = globalVariables.SysDate;
                CauHinh();
                cmdConfig.Visible = globalVariables.IsAdmin;
                dtPaymentDate.ReadOnly = !Utility.Coquyen("thanhtoan_suangaythanhtoan");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        void setquyen()
        {
            try
            {
                grdLichsutrathuoc.RootTable.Columns["NHAN"].Visible = Utility.Coquyen("thuoc_quaythuoc_thanhtoan") || Loai_Nguoidung == "TNV" || Loai_Nguoidung == "ALL";
                uiTabPagePhieuchi.TabVisible = cmdTraLaiTien.Visible = cmdThanhtoan.Visible = cmdThanhtoan2.Visible = Utility.Coquyen("thuoc_quaythuoc_thanhtoan") || Loai_Nguoidung == "TNV" || Loai_Nguoidung == "ALL";
                cmdHuythanhtoan.Visible = cmdHuythanhtoan2.Visible = cmdPhanboHTTT.Visible = Utility.Coquyen("thuoc_quaythuoc_huythanhtoan") || Loai_Nguoidung == "TNV" || Loai_Nguoidung == "ALL";
                //uiTabPhieuChi.Height = Utility.Coquyen("thuoc_quaythuoc_thanhtoan") || Loai_Nguoidung == "TNV" ? 200 : 0;
              cmdInphieuhoantrathuoc.Visible=  cmdXacnhantralai.Visible = cmdThemdonthuoc.Visible = cmdCapnhatdonthuoc.Visible = cmdXoadonthuoc.Visible = cmdPhatThuoc.Visible = cmdHuycapphat.Visible = cmdDuyetcapphat.Visible = cmdHuyduyetCapphat.Visible = Loai_Nguoidung == "DUOC" || Loai_Nguoidung == "ALL";
                pnlThongtintien.Height = cmdThanhtoan.Visible ? 182 : 40;
                //pnlThanhtoan.Height = cmdThanhtoan.Visible ? 133 : 0;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void InitEvents()
        {
            
            this.Load += new System.EventHandler(this.frm_phatthuoc_ngoaitru_Load);
            Shown += frm_phatthuoc_ngoaitru_Shown;
            FormClosing += frm_phatthuoc_ngoaitru_FormClosing;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_phatthuoc_ngoaitru_KeyDown);
            this.txtPres_ID.Click += new System.EventHandler(this.txtPres_ID_Click);
            this.txtPres_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPres_ID_KeyDown);
            this.txtPID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPID_KeyDown_1);
            
            //this.radTatCa.CheckedChanged += new System.EventHandler(this.radTatCa_CheckedChanged);
            //this.radChuaXacNhan.CheckedChanged += new System.EventHandler(this.radChuaXacNhan_CheckedChanged);
            //this.radDaXacNhan.CheckedChanged += new System.EventHandler(this.radDaXacNhan_CheckedChanged);
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            cmdKiemTraSoLuong.Click+=new EventHandler(cmdKiemTraSoLuong_Click);
            grdPres.ApplyingFilter+=new CancelEventHandler(grdPres_ApplyingFilter);
            grdChitietDonthuoc.UpdatingCell += grdPresDetail_UpdatingCell;
            grdChitietDonthuoc.CellUpdated+=grdPresDetail_CellUpdated;
            grdChitietDonthuoc.RowCheckStateChanged+=grdPresDetail_RowCheckStateChanged;
            grdChitietDonthuoc.ColumnHeaderClick+=grdPresDetail_ColumnHeaderClick;
            grdChitietDonthuoc.EditingCell += grdPresDetail_EditingCell;
            grdChitietDonthuoc.CellValueChanged += grdPresDetail_CellValueChanged;

            grdPayment.ColumnButtonClick += grdPayment_ColumnButtonClick;
           grdPhieuChi.ColumnButtonClick+=grdPhieuChi_ColumnButtonClick;
           grdPhieuChi.UpdatingCell += grdPhieuChi_UpdatingCell;
           chkThanhtoan.CheckedChanged += _CheckedChanged;
           chkHuythanhtoan.CheckedChanged += _CheckedChanged;
           chkCapphat.CheckedChanged += _CheckedChanged;
           chkHuyCapphat.CheckedChanged += _CheckedChanged;
           chkHienthidvuhuyTT.CheckedChanged += _CheckedChanged;
           cmdInPhieuChi.Click += cmdInPhieuChi_Click;
           cmdTraLaiTien.Click += cmdTraLaiTien_Click;
           grdThongTinDaThanhToan.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(grdThongTinDaThanhToan_CellValueChanged);
           grdThongTinDaThanhToan.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(grdThongTinDaThanhToan_CellUpdated);
           grdThongTinDaThanhToan.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(grdThongTinDaThanhToan_ColumnHeaderClick);
           grdPayment.CellValueChanged += grdPayment_CellValueChanged;
          
           grdThongTinDaThanhToan.UpdatingCell += grdThongTinDaThanhToan_UpdatingCell;
           grdLichsutrathuoc.ColumnButtonClick += grdLichsutrathuoc_ColumnButtonClick;
           txtPttt._OnEnterMe += txtPttt__OnEnterMe;
           cmdInBienlai.Click += cmdInBienlai_Click;
           cmdInhoadon.Click += cmdInhoadon_Click;
            grdChitietDonthuoc.CellValueChanged += GrdChitietDonthuoc_CellValueChanged;
        }

        private void GrdChitietDonthuoc_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if(e.Column.Key=="GPP")
                {
                    bool GPP = Utility.Obj2Bool(grdChitietDonthuoc.GetValue("GPP"));
                    int num = new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.Gpp).EqualTo(GPP).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Utility.Int64Dbnull( grdChitietDonthuoc.GetValue("id_phieu_chitiet"),-1)).Execute();
                }    
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void grdLichsutrathuoc_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XEMPTLQT")
                {
                    long id_tralaithuoc = Utility.Int64Dbnull(grdLichsutrathuoc.GetValue("id_tralaithuoc"));
                    frm_XemphieuTralai_Quaythuoc _XemphieuTralai_Quaythuoc = new frm_XemphieuTralai_Quaythuoc(id_tralaithuoc);
                    _XemphieuTralai_Quaythuoc.ShowDialog();
                }
                else if (e.Column.Key == "HUYPTLQT")
                {
                    HuyphieuTralaiThuoctaiQuay();
                }
                else if (e.Column.Key == "NHAN")
                {
                     long id_tralaithuoc = Utility.Int64Dbnull(grdLichsutrathuoc.GetValue("id_tralaithuoc"));
                     AutoFill(id_tralaithuoc);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            
        }
        void AutoFill(long id_tralaithuoc)
        {
            bool hasData = false;
            try
            {
                ThuocLichsuTralaithuoctaiquayPhieu _phieuTL = ThuocLichsuTralaithuoctaiquayPhieu.FetchByID(id_tralaithuoc);
                if (_phieuTL == null)
                {
                    Utility.ShowMsg("Phiếu trả lại thuốc tại quầy không tồn tại. Vui lòng nạp lại dữ liệu");
                    return;
                }
                if (_phieuTL.TrangThai == 1 || _phieuTL.IdPhieuchi > 0)
                {
                    Utility.ShowMsg("Phiếu trả lại thuốc tại quầy đã được sử dụng để lập phiếu chi nên không thể nhận tiếp");
                    return;
                }
                DataTable dtData = new Select().From(ThuocLichsuTralaithuoctaiquayChitiet.Schema)
                    .Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdTralaithuoc).IsEqualTo(id_tralaithuoc)
                    .And(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdChitietThanhtoan).IsGreaterThan(0)
                    .ExecuteDataSet().Tables[0];
                if (dtData.Rows.Count > 0)
                {
                    Utility.ShowMsg("Phiếu trả lại thuốc tại quầy đã có các chi tiết đã được chi tiền nên không thể nhận tiếp");
                    return;
                }

                dtData = SPs.ThuocLichsutralaithuoctaiquayXemchitiet(id_tralaithuoc).GetDataSet().Tables[0];
                grdThongTinDaThanhToan.UnCheckAllRecords();
                foreach (GridEXRow _row in grdThongTinDaThanhToan.GetDataRows())
                {
                    _row.BeginEdit();
                    _row.Cells["sl_tralai"].Value = 0;
                    long id_chitiet_thanhtoan = Utility.Int64Dbnull(_row.Cells[KcbThanhtoanChitiet.Columns.IdChitiet].Value);
                    DataRow[] arrDr = dtData.Select(string.Format("id_chitiet_thanhtoan_goc={0}", id_chitiet_thanhtoan));
                    if (arrDr.Length > 0)
                    {
                        _row.Cells["sl_tralai"].Value = Utility.DecimaltoDbnull(arrDr[0]["so_luong"], 0);
                        hasData = true;
                        _row.IsChecked = true;
                    }
                    _row.EndEdit();
                }
                id_phieutralaiTTQ = id_tralaithuoc;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                cmdTraLaiTien.Enabled =hasData && Utility.isValidGrid(grdPres) && grdThongTinDaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null;
                modifyButtonsTraThuocTaiquay();
            }
        }
        void modifyButtonsTraThuocTaiquay()
        {
            cmdInphieuhoantrathuoc.Enabled = Utility.isValidGrid(grdLichsutrathuoc);
        }
        void HuyphieuTralaiThuoctaiQuay()
        {
            try
            {
                if (!Utility.isValidGrid(grdLichsutrathuoc))
                {
                    return;
                }

                long id_tralaithuoc = Utility.Int64Dbnull(grdLichsutrathuoc.GetValue("id_tralaithuoc"));
                ThuocLichsuTralaithuoctaiquayPhieu _phieuTL = ThuocLichsuTralaithuoctaiquayPhieu.FetchByID(id_tralaithuoc);
                if (_phieuTL == null)
                {
                    Utility.ShowMsg("Phiếu trả lại thuốc tại quầy không tồn tại. Vui lòng nạp lại dữ liệu");
                    return;
                }
                if (globalVariables.isSuperAdmin || globalVariables.IsAdmin || _phieuTL.NguoiTao == globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(string.Format("Phiếu trả lại thuốc tại quầy được tạo bảo người dùng {0} nên bạn không thể hủy. Muốn hủy bạn phải là Admin hoặc liên hệ người tạo phiếu", _phieuTL.NguoiTao));
                    return;
                }
                if (_phieuTL.TrangThai == 1 || _phieuTL.IdPhieuchi > 0)
                {
                    Utility.ShowMsg("Phiếu trả lại thuốc tại quầy đã được sử dụng để lập phiếu chi nên không thể hủy");
                    return;
                }
                DataTable dtData = new Select().From(ThuocLichsuTralaithuoctaiquayChitiet.Schema)
                    .Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdTralaithuoc).IsEqualTo(id_tralaithuoc)
                    .And(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdChitietThanhtoan).IsGreaterThan(0)
                    .ExecuteDataSet().Tables[0];
                if (dtData.Rows.Count > 0)
                {
                    Utility.ShowMsg("Phiếu trả lại thuốc tại quầy đã có các chi tiết đã được chi tiền nên không thể hủy");
                    return;
                }
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu trả lại thuốc tại quầy với id ={0} hay không?", _phieuTL.IdTralaithuoc), "Xác nhận hủy", true))
                {
                    return;
                }
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Delete().From(ThuocLichsuTralaithuoctaiquayChitiet.Schema).Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdTralaithuoc).IsEqualTo(id_tralaithuoc).Execute();
                        new Delete().From(ThuocLichsuTralaithuoctaiquayPhieu.Schema).Where(ThuocLichsuTralaithuoctaiquayPhieu.Columns.IdTralaithuoc).IsEqualTo(id_tralaithuoc).Execute();
                        Utility.Log(Name, globalVariables.UserName, string.Format("Xóa phiếu trả lại thuốc tại quầy: Id= {0} của người bệnh {1} mã lần khám {2} + id bệnh nhân: {3} ",id_tralaithuoc, objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                    }
                    scope.Complete();
                }
                LaydanhsachLichsu_phieutralaithuoc();
                Utility.ShowMsg("Đã xóa phiếu trả lại thuốc tại quầy thành công. Nhấn OK để kết thúc");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                modifyButtonsTraThuocTaiquay();
            }
        }
       
        void cmdInhoadon_Click(object sender, EventArgs e)
        {
            //Tạm rem đoạn xem phiếu
            //CallPhieuThu();
            if (!Utility.isValidGrid(grdPayment))
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một phiếu thanh toán để in hóa đơn trong lưới bên dưới", "thông báo");
                return;
            }
            byte kieuthanhtoan = Utility.ByteDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.KieuThanhtoan].Value, 0);
            if (kieuthanhtoan == 0)
                InHoadon();
            else
                new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1));
            

        }

        void cmdInBienlai_Click(object sender, EventArgs e)
        {
            long _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
            long id_donthuoc = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1);
            byte kieuthanhtoan = Utility.ByteDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.KieuThanhtoan].Value, 0);
            if (kieuthanhtoan == 0)
            {
               
                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, _Payment_ID, id_donthuoc, objLuotkham, 0);
            }
            else//Phiếu chi
            {
                KcbThanhtoan objKcbThanhtoan = KcbThanhtoan.FetchByID(_Payment_ID);
                new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiPhieuChiTralaiThuoc(false, _Payment_ID, id_donthuoc, objLuotkham, objKcbThanhtoan.NoiTru);
            }
           
        }

        void grdPayment_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (objLuotkham != null && objLuotkham.TrangthaiNoitru >= 1)
            {

                Utility.ShowMsg("Người bệnh đã ở trạng thái nội trú nên bạn không được quyền sửa các thông tin liên quan đến phiếu thanh toán ngoại trú. Vui lòng kiểm tra lại");
                return;
            }
            if (!Kiemtradieukienhuytt_theongay()) return;
            if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
            {
                UpdatePaymentDate();
            }
            else if (e.Column.Key == "ma_pttt")
            {
                if (!Utility.Coquyen("THANHTOAN_SUA_PTTT"))
                {
                    Utility.ShowMsg("Bạn không có quyền sửa đổi phương thức thanh toán (THANHTOAN_SUA_PTTT). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                    return;
                }
                //Cập nhật lại PTTT
                if (!Utility.isValidGrid(grdPayment)) return;

                CapnhatPTTT_Grid();
            }
            else if (e.Column.Key == "ma_nganhang")
            {
                if (!Utility.Coquyen("THANHTOAN_SUA_PTTT"))
                {
                    Utility.ShowMsg("Bạn không có quyền sửa đổi phương thức thanh toán (THANHTOAN_SUA_PTTT). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                    return;
                }

                string ma_pttt = grdPayment.GetValue("ma_pttt").ToString();
                string ma_nganhangcu = grdPayment.CurrentRow.Cells["ma_nganhang"].Value.ToString();
                string ma_nganhang = Utility.sDbnull(grdPayment.GetValue("ma_nganhang").ToString(), "-1");

                List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                if (lstPTTT.Contains(ma_pttt) && (ma_nganhang == "-1" || ma_nganhang.Length == 0))//Đợi chọn ngân hàng
                {
                    return;
                }
                CapnhatPTTT_Grid();
            }
        }
        private void UpdatePaymentDate()
        {
            if (
                Utility.AcceptQuestion(
                    "Bạn có muốn thay đổi thông tin chỉnh ngày thanh toán. Việc chỉnh ngày có thể sẽ ảnh hưởng tới báo cáo thanh toán theo ngày",
                    "Thông báo", true))
            {
               long v_Payment_ID = Utility.Int64Dbnull(grdPayment.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                if (objPayment != null)
                {
                    DateTime newDate = Convert.ToDateTime(grdPayment.GetValue(KcbThanhtoan.Columns.NgayThanhtoan));
                    if (objPayment.MaxNgayTao.HasValue && newDate.Date <= objPayment.MaxNgayTao.Value.Date)
                    {
                        Utility.ShowMsg(string.Format("Bạn cần chọn ngày thanh toán cần >= {0}", objPayment.MaxNgayTao.Value.ToString("dd/MM/yyyy")), "Thông báo");
                        return;
                    }
                    objPayment.NgayThanhtoan = newDate;
                    ActionResult actionResult = _THANHTOAN.UpdateNgayThanhtoan(objPayment);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Bạn chỉnh sửa ngày thanh toán thành công", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }
        bool Kiemtradieukienhuytt_theongay()
        {
            KcbThanhtoan _vobjTT = KcbThanhtoan.FetchByID(Utility.Int64Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan)));
            if (_vobjTT == null)
            {
                Utility.ShowMsg("Không lấy được thông tin thanh toán từ lưới danh sách phiếu thu. Vui lòng kiểm tra lại");
                return false;
            }
            int kcbThanhtoanSongayHuythanhtoan =
                           Utility.Int32Dbnull(
                               THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
            var chenhlech =
                (int)Math.Ceiling((globalVariables.SysDate.Date - _vobjTT.NgayThanhtoan.Date).TotalDays);
            if (chenhlech > kcbThanhtoanSongayHuythanhtoan)
            {
                Utility.ShowMsg(string.Format("Ngày thanh toán: {0}. Hệ thống không cho phép bạn thay đổi thông tin thanh toán khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", _vobjTT.NgayThanhtoan.Date.ToString("dd/MM/yyyy"), kcbThanhtoanSongayHuythanhtoan.ToString()));
                return false;
            }
            if (Utility.Byte2Bool(_vobjTT.TrangthaiChot))
            {
                Utility.ShowMsg("Thanh toán đang chọn đã được chốt nên bạn không thể thay đổi thông tin thanh toán. Vui lòng kiểm tra lại!");
                return false;
            }
            return true;
        }
        void CapnhatPTTT_Grid()
        {
            try
            {
                List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                Int32 IdThanhtoan = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(IdThanhtoan);
                string ma_pttt = grdPayment.GetValue("ma_pttt").ToString();
                string ma_pttt_cu = objThanhtoan.MaPttt;
                string ten_pttt_cu = grdPayment.CurrentRow.Cells["ten_pttt"].Value.ToString();
                string ma_nganhang = Utility.sDbnull(grdPayment.GetValue("ma_nganhang").ToString(), "-1");
                if (!lstPTTT.Contains(ma_pttt))
                    if (ma_pttt == ma_pttt_cu) return;

                DataRow[] arrDr = m_dtPayment.Select("id_thanhtoan=" + IdThanhtoan.ToString());
                if (arrDr.Length <= 0) return;
                //List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                //if (cboNganhang.Enabled && cboNganhang.SelectedValue.ToString() == "-1")
                //{
                //    Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt.Text));
                //    cboNganhang.Focus();
                //    return;
                //}


                if (!lstPTTT.Contains(ma_pttt))
                    ma_nganhang = "";
                if (lstPTTT.Contains(ma_pttt) && (ma_nganhang == "-1" || ma_nganhang.Length == 0))//Đợi chọn ngân hàng
                {
                    Utility.ShowMsg("Bạn cần chọn ngân hàng");
                    Utility.focusCell(grdPayment, "ma_nganhang");
                    return;
                }
                if (!isValidPttt_Nganhang_Grid())
                    return;
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thay đổi phương thức thanh toán từ {0} sang {1}", arrDr[0]["ten_pttt"], ma_pttt), "Xác nhận cập nhật PTTT", true))
                {
                    Utility.ShowMsg("Bạn vừa chọn hủy cập nhật phương thức thanh toán. Nhấn OK để kết thúc");
                    return;
                }
                List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                DataTable dtPhanbo = _THANHTOAN.KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(IdThanhtoan, -1l, -1l).Tables[0];
                //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                if (lstPhanbo.Contains(ma_pttt))
                {
                    if (dtPhanbo.Select("so_tien>0").Length <= 1)
                        Phanbo();
                }
                else
                {
                    if (dtPhanbo.Select("so_tien>0").Length > 1)//Không phải chọn phân bổ mà số lượng dòng phân bổ >1 thì thông báo người dùng lựa chọn
                    {
                        if (Utility.AcceptQuestion(string.Format("Lần phân bổ gần nhất bạn đang phân bổ tiền cho nhiều phương thức thanh toán. Trong khi hình thức thanh toán bạn đang chọn ({0}) không phải là phân bổ. Bạn có muốn hệ thống tự động chuyển tất cả số tiền sang hình thức vừa chọn {1} hay không?\n.Nhấn Yes để đồng ý. Nhấn No để xem lại thông tin phân bổ", ma_pttt, ma_pttt), "Cảnh báo và gợi ý", true))
                        {
                            Capnhatphanbo11(dtPhanbo, objThanhtoan, ma_pttt, ma_nganhang);
                        }
                        else
                            Phanbo();
                    }
                    else if (dtPhanbo.Select("so_tien>0").Length == 1)//Thực hiện cập nhật luôn
                    {
                        Capnhatphanbo11(dtPhanbo, objThanhtoan, ma_pttt, ma_nganhang);
                    }
                    else//Lỗi chưa chọn pttt
                    {
                        Capnhatphanbo(objThanhtoan, ma_pttt, ma_nganhang);

                    }


                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void Capnhatphanbo(KcbThanhtoan objThanhtoan, string ma_pttt, string ma_nganhang)
        {


            using (var scope = new TransactionScope())
            {
                using (var sh = new SharedDbConnectionScope())
                {

                    new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema).Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                    KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(objThanhtoan.IdThanhtoan);
                    if (objPayment != null)
                    {
                        objPayment.MaPttt = ma_pttt;
                        objPayment.MaNganhang = ma_nganhang;
                        objPayment.MarkOld();
                        objPayment.IsNew = false;
                        objPayment.Save();
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan, -1l, -1l, ma_pttt, ma_nganhang,
                           objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
                           objThanhtoan.NoiTru, Utility.DecimaltoDbnull(objThanhtoan.TongTien, 0) - Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0), Utility.DecimaltoDbnull(objThanhtoan.TongTien, 0) - Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0),
                            globalVariables.UserName, objThanhtoan.NgayTao, "", objThanhtoan.NgayTao, -1l, 0, (byte)1).Execute();
                    }
                }
                scope.Complete();
            }
            foreach (DataRow dr in m_dtPayment.Rows)
                if (dr["id_thanhtoan"].ToString() == objThanhtoan.IdThanhtoan.ToString())
                {
                    dr["ma_pttt"] = ma_pttt;
                    dr["ten_pttt"] = txtPttt.myCode;
                    dr["ma_nganhang"] = ma_nganhang;
                    dr["ten_nganhang"] = ma_nganhang;
                }
            m_dtPayment.AcceptChanges();
            Utility.ShowMsg("Cập nhật thông tin hình thức thanh toán thành công");
        }
        bool isValidPttt_Nganhang_Grid()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            string ma_pttt = grdPayment.GetValue("ma_pttt").ToString();
            string ma_nganhang = Utility.sDbnull(grdPayment.GetValue("ma_nganhang").ToString(), "-1");
            if (lstPTTT.Contains(ma_pttt) && (ma_nganhang.Length <= 0 || ma_nganhang == "-1"))
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", ma_pttt));
                
                return false;
            }
            return true;
        }

        void Capnhatphanbo11(DataTable dtPhanbo, KcbThanhtoan objThanhtoan, string ma_pttt, string ma_nganhang)
        {
            decimal so_tien = Utility.DecimaltoDbnull(dtPhanbo.Compute("sum(so_tien)", "1=1"), 0);

            using (var scope = new TransactionScope())
            {
                using (var sh = new SharedDbConnectionScope())
                {

                    new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema).Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                    KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(objThanhtoan.IdThanhtoan);
                    if (objPayment != null)
                    {
                        objPayment.MaPttt = ma_pttt;
                        objPayment.MaNganhang = ma_nganhang;
                        objPayment.MarkOld();
                        objPayment.IsNew = false;
                        objPayment.Save();
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan, -1l, -1l, objPayment.MaPttt, objPayment.MaNganhang,
          objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
          objThanhtoan.NoiTru, so_tien, so_tien,
          globalVariables.UserName, DateTime.Now, "", DateTime.Now, -1, 0, (byte)1).Execute();
                    }
                }
                scope.Complete();
            }
            foreach (DataRow dr in m_dtPayment.Rows)
                if (dr["id_thanhtoan"].ToString() == objThanhtoan.IdThanhtoan.ToString())
                {
                    dr["ma_pttt"] = ma_pttt;
                    dr["ten_pttt"] = txtPttt.myCode;
                    dr["ma_nganhang"] = ma_nganhang;
                    dr["ten_nganhang"] = ma_nganhang;
                }
            m_dtPayment.AcceptChanges();
            Utility.ShowMsg("Cập nhật thông tin hình thức thanh toán thành công");
        }
        void grdThongTinDaThanhToan_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "sl_tralai")
                {
                    int dacapphat = Utility.Int32Dbnull(grdThongTinDaThanhToan.CurrentRow.Cells["da_capphat"].Value);
                    int sl = Utility.Int32Dbnull(grdThongTinDaThanhToan.CurrentRow.Cells["so_luong"].Value);
                    int sltralai = Utility.Int32Dbnull(e.Value, 0);
                    int tongtra = Utility.Int32Dbnull(grdThongTinDaThanhToan.CurrentRow.Cells["tong_sl_tralai"].Value);
                    int conlai = sl - tongtra;
                    if (dacapphat == 0)
                    {
                        Utility.ShowMsg("Bạn cần cấp phát thuốc cho người bệnh trước khi thực hiện chức năng trả lại thuốc. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                    }
                    if (sltralai <= 0)
                    {
                        Utility.ShowMsg("Cần nhập số lượng trả lại >0 với các thuốc được chọn trả lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                    }
                    //if (sltralai > conlai)
                    //{
                    //    Utility.ShowMsg(string.Format("Số lượng trả lại phải <= {0}", conlai), "Thông báo", MessageBoxIcon.Warning);
                    //    e.Value = e.InitialValue;
                    //}
                }
            }
            catch (Exception ex)
            {
                
               
            }
        }

       

        void txtPttt__OnEnterMe()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            autoNganhang.Enabled = lstPTTT.Contains(txtPttt.MyCode);
            if (!autoNganhang.Enabled) autoNganhang.SetCode("-1");
        }
        void Try2Splitter()
        {
            try
            {


                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count >=1)
                {
                    splitContainer4.SplitterDistance = lstSplitterSize[0];
                }
            }
            catch (Exception)
            {

            }
        }
        void SaveUserConfigs()
        {
            Utility.SaveUserConfig(chkThanhtoan.Tag.ToString(), Utility.Bool2byte(chkThanhtoan.Checked));
            Utility.SaveUserConfig(chkHuythanhtoan.Tag.ToString(), Utility.Bool2byte(chkHuythanhtoan.Checked));
            Utility.SaveUserConfig(chkHienthidvuhuyTT.Tag.ToString(), Utility.Bool2byte(chkHienthidvuhuyTT.Checked));
            Utility.SaveUserConfig(chkRestoreDefaultPTTT.Tag.ToString(), Utility.Bool2byte(chkRestoreDefaultPTTT.Checked));
            Utility.SaveUserConfig(chkCapphat.Tag.ToString(), Utility.Bool2byte(chkCapphat.Checked));
            Utility.SaveUserConfig(chkHuyCapphat.Tag.ToString(), Utility.Bool2byte(chkHuyCapphat.Checked));

        }
        void frm_phatthuoc_ngoaitru_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer4.SplitterDistance.ToString(), splitContainer4.SplitterDistance.ToString() });
        }

        void frm_phatthuoc_ngoaitru_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }

       

        void grdPresDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            
        }

        void grdPresDetail_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (grdChitietDonthuoc.CurrentColumn != null) grdChitietDonthuoc.CurrentColumn.InputMask = "";
        }
        private void GetChiPhiDaThanhToan()
        {
            try
            {
                m_dtChiPhiDaThanhToan =
                    _THANHTOAN.LayThongtinDaThanhtoan(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, 0, lstIdLoaiTtoan);
                Utility.SetDataSourceForDataGridEx(grdThongTinDaThanhToan, m_dtChiPhiDaThanhToan, false, true, "1=1", "");
                //if (m_dtChiPhiDaThanhToan.Rows.Count > 0)
                //{
                //    dtPaymentDate.ReadOnly = true;
                //    //txtPttt.Enabled = false;
                //    dtPaymentDate.Value = Convert.ToDateTime(m_dtChiPhiDaThanhToan.Rows[0]["ngay_thanhtoan"].ToString());
                //}
                //else
                //{
                //    dtPaymentDate.ReadOnly = false;
                //    //txtPttt.Enabled = true;
                //    dtPaymentDate.Value = globalVariables.SysDate;
                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        /// <summary>
        ///     HÀM THỰC HIỆN VIỆC THAY ĐỔI THÔNG TIN PHIẾU CẬN LÂM SÀNG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdThongTinDaThanhToan_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "sl_tralai")
                {
                    decimal sl_tralai = Utility.DecimaltoDbnull(grdThongTinDaThanhToan.CurrentRow.Cells["sl_tralai"].Value);
                    if (sl_tralai > 0)
                        grdThongTinDaThanhToan.CurrentRow.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }

        private void grdThongTinDaThanhToan_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifyCommand();
        }

        private void grdThongTinDaThanhToan_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifyCommand();
        }
        private bool IsValidCancelData()
        {
            //Kiểm tra thời hạn trả lại
            if (grdThongTinDaThanhToan.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn các dịch vụ cần trả lại tiền ", "Thông báo", MessageBoxIcon.Warning);
                grdThongTinDaThanhToan.Focus();
                return false;
            }
            bool nhapsoluong = true;
            foreach (GridEXRow gridExRow in grdThongTinDaThanhToan.GetCheckedRows())
            {
                int IdPhieuChitiet = Utility.Int32Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdPhieuChitiet].Value);
                string ten_dichvu = gridExRow.Cells["ten_chitietdichvu"].Value.ToString();
                int sl = Utility.Int32Dbnull(gridExRow.Cells["so_luong"].Value);
                int sltralai = Utility.Int32Dbnull(gridExRow.Cells["sl_tralai"].Value);
                int tongtra = Utility.Int32Dbnull(gridExRow.Cells["tong_sl_tralai"].Value);
                int conlai=sl-tongtra;
                if (sltralai <= 0)
                {
                    Utility.ShowMsg("Cần nhập số lượng trả lại >0 với các thuốc được chọn trả lại", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
                //if (sltralai > conlai)
                //{
                //    Utility.ShowMsg(string.Format( "Số lượng trả lại phải <= {0}", conlai), "Thông báo", MessageBoxIcon.Warning);
                //    Utility.focusCell(grdThongTinDaThanhToan, "sl_tralai");
                //    return false;
                //}
                switch (Utility.Int32Dbnull(gridExRow.Cells["Id_Loaithanhtoan"].Value))
                {
                    case 1:
                        KcbDangkyKcb objKcbDangkyKcb = KcbDangkyKcb.FetchByID(IdPhieuChitiet);
                        if (objKcbDangkyKcb != null && Utility.Byte2Bool(objKcbDangkyKcb.TrangThai))
                        {
                            Utility.ShowMsg(
                                "Dịch vụ khám đã thực hiện,Bạn không thể hủy, Mời bạn xem lại\n Mời bạn chọn những bản ghi chưa làm dịch vụ hoàn trả",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        if (objKcbDangkyKcb != null && Utility.Byte2Bool(objKcbDangkyKcb.TrangthaiHuy))
                        {
                            Utility.ShowMsg(
                                "Dịch vụ khám đã trả lại tiền,Bạn không thể trả lại tiếp, Mời bạn xem lại\n Mời bạn chọn những bản ghi chưa làm dịch vụ hoàn trả",
                                "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                    case 2:
                        KcbChidinhclsChitiet objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(IdPhieuChitiet);
                        if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai >= 3)
                        {
                            Utility.ShowMsg("Dịch vụ cận lâm sàng đã trả kết quả,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa thực hiện", "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        if (objKcbChidinhclsChitiet != null && Utility.Byte2Bool(objKcbChidinhclsChitiet.TrangthaiHuy))
                        {
                            Utility.ShowMsg("Dịch vụ cận lâm sàng đã hủy,Bạn không thể hủy, Mời bạn xem lại \n  Mời bạn chọn những bản ghi chưa hủy thông tin", "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                    case 3:
                    case 5:
                        KcbDonthuocChitiet objKcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(IdPhieuChitiet);
                        ////Bỏ điều kiện này vì có thể trả nhiều lần và trả ở bất kì thời điểm nào
                        //if (objKcbDonthuocChitiet != null && Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                        //{
                        //    Utility.ShowMsg(string.Format("Thuốc {0} đã được cấp phát nên bạn không thể trả lại tiền. Vui lòng thực hiện nhận lại thuốc từ người bệnh trước khi thực hiện trả lại tiền", ten_dichvu), "Thông báo", MessageBoxIcon.Warning);
                        //    grdThongTinDaThanhToan.Focus();
                        //    return false;
                        //}
                        if (objKcbDonthuocChitiet != null && Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiHuy))
                        {
                            Utility.ShowMsg(string.Format("Thuốc {0} đã bị hủy nên bạn không thể trả lại tiền. Vui lòng liên hệ quản trị hệ thống để có phương án xử lý", ten_dichvu), "Thông báo", MessageBoxIcon.Warning);
                            grdThongTinDaThanhToan.Focus();
                            return false;
                        }
                        break;
                }
            }


            return true;
        }
        void cmdTraLaiTien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidCancelData()) return;
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn trả lại tiền các dịch vụ đang được chọn cho Bệnh nhân hay không?",
                    "Thông báo", true))
                {
                    if (objLuotkham == null)
                    {
                        objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdPres.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1), Utility.sDbnull(grdPres.GetValue(KcbLuotkham.Columns.MaLuotkham), "-1"));
                    }
                    if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                    {
                        Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép trả lại tiền ngoại trú nữa");
                        return;
                    }
                    KcbThanhtoan objPayment = TaophieuThanhtoanHuy();
                    string[] query = (from p in grdThongTinDaThanhToan.GetCheckedRows()
                                      select string.Format("{0} {1} {2}", Utility.sDbnull(p.Cells["sl_tralai"].Value, ""), Utility.sDbnull(p.Cells["donvi_tinh"].Value, ""), Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value, ""))).ToArray();
                    string noidung = string.Join(";", query);
                    frm_Chondanhmucdungchung _Chondanhmucdungchung = new frm_Chondanhmucdungchung("LYDOTRATIEN", "TRẢ TIỀN LẠI CHO BỆNH NHÂN", "Chọn lý do trả trước khi thực hiện", "Lý do trả tiền",false);
                    _Chondanhmucdungchung.Lydomacdinh = string.Format("Người bệnh trả lại: {0}", noidung);
                    _Chondanhmucdungchung.ShowDialog();
                    if (_Chondanhmucdungchung.m_blnCancel) return;
                    ///Dùng chung 1 người làm
                    ///ActionResult actionResult = _THANHTOAN.Trathuoctaiquay_Rieng(objPayment, objLuotkham, id_phieutralaiTTQ, TaodulieuthanhtoanchitietHuy(), _Chondanhmucdungchung.ma, noidung, _Chondanhmucdungchung.ten);
                    //Dùng riêng từng bộ phận
                    ActionResult actionResult = _THANHTOAN.Trathuoctaiquay_Rieng(objPayment, objLuotkham,id_phieutralaiTTQ, _Chondanhmucdungchung.ma, noidung, _Chondanhmucdungchung.ten);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            //tabThongTinThanhToan.SelectedTab = tabPagePhieuChi;
                            LaydanhsachLichsuthanhtoan_phieuchi();
                            LaydanhsachLichsu_phieutralaithuoc();
                            GetChiPhiDaThanhToan();
                            id_phieutralaiTTQ = -1;
                            //Tạm bỏ do đã refesh lại chi phí đã thanh toán
                            //foreach (GridEXRow gridExRow in grdThongTinDaThanhToan.GetCheckedRows())
                            //{
                            //    gridExRow.BeginEdit();
                            //    gridExRow.Cells[KcbThanhtoanChitiet.Columns.TrangthaiHuy].Value = 1;
                            //    gridExRow.EndEdit();
                            //    grdThongTinDaThanhToan.UpdateData();
                            //    m_dtChiPhiDaThanhToan.AcceptChanges();
                            //}
                            Utility.GotoNewRowJanus(grdPhieuChi, KcbThanhtoan.Columns.IdThanhtoan,
                                    Utility.sDbnull(objPayment.IdThanhtoan));
                            if (PropertyLib._MayInProperties.TudonginPhieuchiSaukhitratien)
                            {
                                new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(objPayment.IdThanhtoan);
                            }
                            if (PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhitratien)
                            {
                                CallPhieuChi(grdPhieuChi);
                            }

                            ModifyCommand();
                            break;
                        case ActionResult.AssignIsConfirmed:
                            Utility.ShowMsg("Đã có dịch vụ được thực hiện rồi. Bạn không thể trả lại tiền !",
                                "Thông báo");
                            break;
                        case ActionResult.PresIsConfirmed:
                            Utility.ShowMsg("Đã có thuốc được xác nhận cấp phát. Bạn không thể trả lại tiền !",
                                "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Bản ghi thanh toán chi tiết trả lại không tồn tại. Liên hệ IT để được hỗ trợ", "Thông báo");
                            break;
                        case ActionResult.Cancel:
                            Utility.ShowMsg("Bản ghi đơn thuốc chi tiết trả lại không tồn tại. Liên hệ IT để được hỗ trợ", "Thông báo");
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }
        private KcbThanhtoan TaophieuThanhtoanHuy()
        {

            KcbThanhtoan objPayment = new KcbThanhtoan();
            objPayment.MaLuotkham = objLuotkham.MaLuotkham;
            objPayment.IdBenhnhan = objLuotkham.IdBenhnhan;
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            objPayment.KieuThanhtoan = 1; //0=Thanh toán thường;1= trả lại tiền;2= thanh toán bỏ viện
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.NoiTru = 0;
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.NguoiIn = string.Empty;
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.TtoanThuoc = true;//Phiếu chi trả lại thuốc tại quầy
            objPayment.TrangThai = 0;
            objPayment.MaPttt = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_TRALAITIEN_PTTT_MACDINH", "TM", false);
            objPayment.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(globalVariables.SysDate);
            objPayment.NgayThanhtoan = globalVariables.SysDate;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.IpMaytao = globalVariables.gv_strIPAddress;
            objPayment.TenMaytao = globalVariables.gv_strComputerName;
            return objPayment;
        }

        //private List<Int64> TaodulieuthanhtoanchitietHuy()
        //{
        //    List<Int64> lstIdChitiet = (from q in grdThongTinDaThanhToan.GetCheckedRows()
        //                                select Utility.Int64Dbnull(q.Cells[KcbThanhtoanChitiet.Columns.IdChitiet].Value)).ToList<Int64>();
        //    return lstIdChitiet;
        //}
        private List<Tralaithuoctaiquay> TaodulieuthanhtoanchitietHuy()
        {
            List<Tralaithuoctaiquay> lstIdChitiet = (from q in grdThongTinDaThanhToan.GetCheckedRows()
                                                     select new Tralaithuoctaiquay()
                                                     {
                                                         id_chitiet_thanhtoan = Utility.Int64Dbnull(q.Cells[KcbThanhtoanChitiet.Columns.IdChitiet].Value),
                                                         id_donthuoc = Utility.Int64Dbnull(q.Cells["id_phieu"].Value),
                                                         id_chitiet_donthuoc = Utility.Int64Dbnull(q.Cells["id_phieu_chitiet"].Value),
                                                         sl_tralai = Utility.Int32Dbnull(q.Cells["sl_tralai"].Value),
                                                         don_gia = Utility.DecimaltoDbnull(q.Cells["don_gia"].Value),
                                                         id_thuoc = Utility.Int32Dbnull(q.Cells["id_dichvu"].Value),
                                                         id_loaithanhtoan = Utility.ByteDbnull(q.Cells["id_loaithanhtoan"].Value)
                                                     }
                                            ).ToList<Tralaithuoctaiquay>();

            return lstIdChitiet;
        }
        void cmdInPhieuChi_Click(object sender, EventArgs e)
        {
            if (Utility.isValidGrid(grdPhieuChi))
            {
                CallPhieuChi(grdPhieuChi);
            } 
        }
       
        void _CheckedChanged(object sender, EventArgs e)
        {
            //Utility.SaveUserConfig(((Janus.Windows.EditControls.UICheckBox)sender).Tag.ToString(), Utility.Bool2byte(((Janus.Windows.EditControls.UICheckBox)sender).Checked));
        }

        void grdPhieuChi_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
            {
                if (Utility.Coquyen("thanhtoan_suangaythanhtoan"))
                {
                    //UpdatePaymentDate();
                    UpdatePhieuChiPaymentDate();
                }
                else
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Bạn không có quyền cập nhật thông tin ngày tạo phiếu chi");
                }
            }
        }

        private void grdPayment_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (grdPayment.GetValue("kieu_thanhtoan").ToString() == "1")
            {
                if (e.Column.Key == "cmdPHIEU_THU")
                {
                    CallPhieuChi(grdPayment);
                }
                if (e.Column.Key == "cmdHUY_PHIEUTHU")
                {
                    HuyPhieuchi(grdPayment);
                }
            }
            else
            {

                if (e.Column.Key == "cmdPHIEU_THU")
                {
                    CallPhieuThu();
                }
                if (e.Column.Key == "cmdHUY_PHIEUTHU")
                {
                    HuyThanhtoan();
                }
            }

            //if (e.Column.Key == "cmdPHIEU_THU")
            //{
            //    CallPhieuThu();
            //}
            //if (e.Column.Key == "cmdHUY_PHIEUTHU")
            //{
            //    HuyThanhtoan();
            //}
        }
        private void CallPhieuThu()
        {
            if (grdPayment.CurrentRow != null)
            {
                long v_Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);


                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);

                if (objPayment != null)
                {
                    if (objLuotkham != null)
                    {
                        frm_HuyThanhtoan frm = new frm_HuyThanhtoan(lstIdLoaiTtoan);
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                        frm.Chuathanhtoan = _chuathanhtoan;
                        frm.TotalPayment = grdPayment.GetDataRows().Length;
                        frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                        frm.ShowCancel = false;
                        frm.ShowDialog();
                    }
                }
            }
        }
        private void grdPhieuChi_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "cmdPHIEU_THU")
                {
                    CallPhieuChi(grdPhieuChi);
                }
                if (e.Column.Key == "cmdHUY_PHIEUTHU")
                {
                    HuyPhieuchi(grdPhieuChi);
                }
            }
            catch (Exception exception)
            {
                //throw;
            }
        }

        /// <summary>
        ///     hàm thực hiện việc hủy thông tin phiếu chi
        /// </summary>
        private void HuyPhieuchi(GridEX grd)
        {
            ma_lydohuy = "";
            if (!Utility.isValidGrid(grd)) return;
            if (!Utility.Coquyen("thanhtoan_huyphieuchi"))
            {
                Utility.ShowMsg("Bạn không có quyền hủy phiếu chi (thanhtoan_huyphieuchi). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                return;
            }
            if (grd.CurrentRow != null)
            {
                if (objLuotkham == null)
                {
                    objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdPres.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1), Utility.sDbnull(grdPres.GetValue(KcbLuotkham.Columns.MaLuotkham), "-1"));
                }
                if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy phiếu chi ngoại trú nữa");
                    return;
                }
                long v_Payment_ID = Utility.Int32Dbnull(grd.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);

                if (objPayment != null)
                {
                    //Kiểm tra ngày hủy
                    int KCB_THANHTOAN_SONGAY_HUYPHIEUCHI = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SONGAY_HUYPHIEUCHI", "0", true), 0);
                    int Chenhlech = (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                    if (Chenhlech > KCB_THANHTOAN_SONGAY_HUYPHIEUCHI)
                    {
                        Utility.ShowMsg(string.Format("Ngày lập phiếu chi {0} - Ngày hủy phiếu chi {1}. Hệ thống không cho phép bạn hủy phiếu chi đã quá {2} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objPayment.NgayThanhtoan.ToString("dd/MM/yyyy"), globalVariables.SysDate.ToString("dd/MM/yyyy"), KCB_THANHTOAN_SONGAY_HUYPHIEUCHI.ToString()));
                        return;
                    }
                    if (PropertyLib._ThanhtoanProperties.Hienthihuyphieuchi)
                    {
                        frm_Tralaitien frm = new frm_Tralaitien();
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                        frm.Chuathanhtoan = _chuathanhtoan;
                        frm.TotalPayment = grdPayment.GetDataRows().Length;
                        frm.ShowCancel = true;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            LaydanhsachLichsuthanhtoan_phieuchi();
                        }
                    }
                    else
                    {
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BATNHAPLYDO_HUYPHIEUCHI", "1", false) == "1")
                        {
                            frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYPHIEUCHI", "Hủy phiếu chi ", "Nhập lý do hủy phiếu chi trước khi thực hiện...", "Lý do Hủy phiếu chi",false);
                            _Nhaplydohuythanhtoan.ShowDialog();
                            if (_Nhaplydohuythanhtoan.m_blnCancel) return;
                            ma_lydohuy = _Nhaplydohuythanhtoan.ma;
                            lydo_huy = _Nhaplydohuythanhtoan.ten;
                        }
                        ActionResult actionResult = _THANHTOAN.HuyPhieuchiTrathuoctaiquay_Rieng(objPayment, objLuotkham, lydo_huy);// _THANHTOAN.HuyPhieuchiTrathuoctaiquay(objPayment, objLuotkham, lydo_huy);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.Log(Name, globalVariables.UserName,
                                      string.Format(
                                          "Bán thuốc tại quầy: Hủy phiếu chi của bệnh nhân {0} có mã lần khám {1} và id bệnh nhân: {2} ", objBenhnhan.TenBenhnhan,
                                          objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan),
                                      newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                                grd.CurrentRow.Delete();
                                ModifyCommand();
                                Utility.ShowMsg("Bạn hủy thông tin phiếu chi thuốc tại quầy thành công", "Thông báo");
                                LaydanhsachLichsuthanhtoan_phieuchi();
                                LaydanhsachLichsu_phieutralaithuoc();
                                id_phieutralaiTTQ = -1;
                                GetChiPhiDaThanhToan();
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Không tìm thấy dòng chi tiết thanh toán của thuốc bị hủy. Vui lòng liên hệ bộ phận IT để được hỗ trợ", "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Cancel:
                                Utility.ShowMsg("Không tìm thấy dòng chi tiết đơn thuốc của thuốc bị hủy. Vui lòng liên hệ bộ phận IT để được hỗ trợ", "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                        }
                    }
                }
                ModifyCommand();
            }
        }
       
        private void UpdatePhieuChiPaymentDate()
        {
            if (
                Utility.AcceptQuestion(
                    "Bạn có muốn thay đổi thông tin chỉnh ngày thanh toán,Nếu bạn chỉnh ngày thanh toán, sẽ ảnh hưởng tới báo cáo",
                    "Thông báo", true))
            {
              long  v_Payment_ID = Utility.Int32Dbnull(grdPhieuChi.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                if (objPayment != null)
                {
                    objPayment.NgayThanhtoan = Convert.ToDateTime(grdPhieuChi.GetValue(KcbThanhtoan.Columns.NgayThanhtoan));
                    ActionResult actionResult = _THANHTOAN.UpdateNgayThanhtoan(objPayment);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Bạn chỉnh sửa ngày thanh toán thành công", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }
        /// <summary>
        ///     hàm thực hiện viecj lấy thông tin phiếu chi
        /// </summary>
        private void CallPhieuChi(GridEX grd)
        {
            try
            {
                frm_Tralaitien frm = new frm_Tralaitien();
                frm.objLuotkham = objLuotkham;
                frm.v_Payment_Id = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grd, KcbThanhtoan.Columns.IdThanhtoan), -1);
                frm.objKcbThanhtoan = KcbThanhtoan.FetchByID(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grd, KcbThanhtoan.Columns.IdThanhtoan), -1));
                frm.Chuathanhtoan = _chuathanhtoan;
                frm.TotalPayment = grd.GetDataRows().Length;
                frm.ShowCancel = false;
                frm.ShowDialog();
            }
            catch (Exception exception)
            {
                //throw;
            }
        }
        private void grdPresDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
            SetSumTotalProperties();
            //TinhToanSoTienPhaithu();
            e.Column.InputMask = "{0:#,#.##}";
        }
      
        void grdPresDetail_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            if (e.Row != null)
            {
                e.Row.BeginEdit();
                if (Utility.sDbnull(e.Row.Cells["trangthai_thanhtoan"].Value, "0") == "0")
                    e.Row.Cells["CHON"].Value = Utility.ByteDbnull(e.CheckState == RowCheckState.Checked ? 1 : 0);
                else
                    e.Row.IsChecked = false;
                e.Row.EndEdit();

            }
            //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
            SetSumTotalProperties();
            //TinhToanSoTienPhaithu();
            ModifyCommand();
        }
        private void grdPresDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

               // grdPresDetail.RowCheckStateChanged -= grdPresDetail_RowCheckStateChanged;
                //if (grdPresDetail.CurrentRow != null)
                //    foreach (GridEXRow row in grdDSKCB.GetDataRows())
                //    {
                //        row.IsChecked = grdPresDetail.CurrentRow.IsChecked;
                //    }
                //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
                SetSumTotalProperties();
                //TinhToanSoTienPhaithu();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                grdChitietDonthuoc.RowCheckStateChanged += grdPresDetail_RowCheckStateChanged;
            }
        }
        void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "tile_chietkhau" || e.Column.Key == "tien_chietkhau")
                {
                    if (Utility.isValidGrid(grdChitietDonthuoc) && Utility.Int64Dbnull(grdChitietDonthuoc.CurrentRow.Cells["trangthai_thanhtoan"].Value, 1) == 1)
                    {
                        Utility.ShowMsg("Chi tiết bạn chọn đã được thanh toán nên bạn không thể chiết khấu được nữa. Mời bạn kiểm tra lại");
                        //e.Cancel = true;
                        e.Value = e.InitialValue;
                        return;
                    }
                    else
                    {
                        if (e.Column.Key == "tile_chietkhau")
                        {
                            //Tính lại tiền chiết khấu theo tỉ lệ %
                            if (Utility.DecimaltoDbnull(e.Value, 0) > 100)
                            {
                                Utility.ShowMsg("Tỉ lệ chiết khấu không được phép vượt quá 100 %. Mời bạn kiểm tra lại");
                                e.Value = e.InitialValue;
                                //e.Cancel = true;
                                return;
                            }
                            grdChitietDonthuoc.CurrentRow.Cells["tien_chietkhau"].Value = Utility.DecimaltoDbnull(grdChitietDonthuoc.CurrentRow.Cells["TT_BN"].Value, 0) * Utility.DecimaltoDbnull(e.Value, 0) / 100;

                        }
                        else
                        {

                            if (Utility.DecimaltoDbnull(e.Value, 0) > Utility.DecimaltoDbnull(grdChitietDonthuoc.CurrentRow.Cells["TT_BN"].Value, 0))
                            {
                                Utility.ShowMsg("Tiền chiết khấu không được lớn hơn(>) tiền Bệnh nhân chi trả(" + Utility.DecimaltoDbnull(grdChitietDonthuoc.CurrentRow.Cells["TT_BN"].Value, 0).ToString() + "). Mời bạn kiểm tra lại");
                                e.Cancel = true;
                                return;
                            }
                            grdChitietDonthuoc.CurrentRow.Cells["tile_chietkhau"].Value = (Utility.DecimaltoDbnull(e.Value, 0) / Utility.DecimaltoDbnull(grdChitietDonthuoc.CurrentRow.Cells["TT_BN"].Value, 0)) * 100;
                        }
                    }
                    grdChitietDonthuoc.CurrentRow.Cells["TT"].Value = Utility.DecimaltoDbnull(grdChitietDonthuoc.CurrentRow.Cells["TT_BN"].Value, 0) - Utility.DecimaltoDbnull(grdChitietDonthuoc.CurrentRow.Cells["tien_chietkhau"].Value, 0);
                }

                else if (e.Column.Key == "sluong_sua")
                {
                    //Check các trạng thái
                    long _IdChitietdonthuoc = Utility.Int64Dbnull(grdChitietDonthuoc.GetValue(KcbDonthuocChitiet.Columns.IdChitietdonthuoc));
                    string ten_thuoc = grdChitietDonthuoc.GetValue("ten_chitietdichvu").ToString();
                    string ten_dvt = grdChitietDonthuoc.GetValue("ten_donvitinh").ToString();
                    KcbDonthuocChitiet objChitiet = KcbDonthuocChitiet.FetchByID(_IdChitietdonthuoc);
                    decimal sl_ke = Utility.DecimaltoDbnull(grdChitietDonthuoc.GetValue(KcbDonthuocChitiet.Columns.SoLuong));
                    //if (so_luong<=)
                    //{
                    //    Utility.ShowMsg(string.Format("Bản ghi đã được cấp phát nên bạn không thể sửa được số lượng mua"));
                    //    //e.Cancel = true;
                    //    e.Value = e.InitialValue;
                    //    return;
                    //}
                    if (objChitiet.TrangThai == 1)
                    {
                        Utility.ShowMsg(string.Format("Bản ghi đã được cấp phát nên bạn không thể sửa được số lượng mua"));
                        //e.Cancel = true;
                        e.Value = e.InitialValue;
                        return;
                    }
                    else if (objChitiet.TrangthaiThanhtoan == 1)
                    {
                        Utility.ShowMsg(string.Format("Bản ghi đã được thanh toán nên bạn không thể sửa được số lượng mua"));
                       // e.Cancel = true;
                        e.Value = e.InitialValue;
                        return;
                    }
                    else if (objChitiet.TrangthaiHuy == 1)
                    {
                        Utility.ShowMsg(string.Format("Bản ghi đã bị hủy nên bạn không thể sửa được số lượng mua"));
                        //e.Cancel = true;
                        e.Value = e.InitialValue;
                        return;
                    }
                    else if (objChitiet.TrangthaiTonghop == 1)
                    {
                        Utility.ShowMsg(string.Format("Bản ghi đã được tổng hợp nên bạn không thể sửa được số lượng mua"));
                        //e.Cancel = true;
                        e.Value = e.InitialValue;
                        return;
                    }
                    //else if (Utility.DecimaltoDbnull(e.Value, 0) > Utility.DecimaltoDbnull(grdPresDetail.GetValue("so_luong")))
                    //{
                        
                    //    Utility.ShowMsg(string.Format("Bạn chỉ được phép điều chỉnh giảm cho các đơn thuốc được kê bởi bác sĩ. Nếu khách hàng muốn mua thêm thì bạn chủ động tự kê đơn thuốc mới"));
                    //    e.Cancel = true;
                    //    e.Value = e.InitialValue;
                    //    return;
                    //}
                    decimal sluongdieuchinh = Utility.DecimaltoDbnull(e.Value);
                    //Lấy số lượng tồn của thuốc trong kho=Số lượng tồn trong kho- tổng số chờ trong tạm kê chưa vượt quá ngày nhả tồn. Bao gồm cả chính chi tiết này. Truyền đúng id_thuockho để chỉ lấy theo id_thuockho này. 
                    //Nếu muốn nhập id_thuockho khác thì vui lòng thêm đơn thuốc mới
                    decimal sluong_ton = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(objChitiet.IdKho, -1), Utility.Int32Dbnull(objChitiet.IdThuoc, -1),Utility.Int64Dbnull( objChitiet.IdThuockho),
                      Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1), (byte)0);
                    //Tồn thực tế=tồn kho+ số lượng đang giữ bởi chính chi tiết này trong bảng tạm kê
                    sluong_ton = sluong_ton + Utility.DecimaltoDbnull(e.InitialValue);
                    if (sluongdieuchinh == 0) //Nhập 0 đồng nghĩa với việc muốn lấy lại số lượng thực tế bác sĩ đã kê lúc đầu
                    {
                        if (sluong_ton < sl_ke)
                        {
                            Utility.ShowMsg(string.Format("Thuốc {0} đang có số lượng khả dụng: {1} {2}.\nSố lượng bạn mua điều chỉnh về 0 đồng nghĩa với việc bạn muốn quay lại số lượng bác sĩ đã kê: {3} lớn hơn số lượng khả dụng.\nĐề nghị điều chỉnh lại số lượng hợp lý.", ten_thuoc, sluong_ton, ten_dvt, sl_ke));
                            e.Value = e.InitialValue;
                            return;
                        }
                    }
                    if (sluong_ton >= sluongdieuchinh && sluong_ton > sl_ke)
                    {
                        //Bắt đầu cập nhật số lượng cho dòng chi tiết và trong bảng tạm kê
                        objChitiet.SluongSua = sluongdieuchinh;
                        objChitiet.NguoiSua = globalVariables.UserName;
                        objChitiet.NgaySua = DateTime.Now;
                        objChitiet.IpMaysua = globalVariables.gv_strIPAddress;
                        objChitiet.IsNew = false;
                        objChitiet.MarkOld();
                        int record =
                        new Update(KcbDonthuocChitiet.Schema)
                               .Set(KcbDonthuocChitiet.Columns.SluongSua).EqualTo(objChitiet.SluongSua)
                               .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objChitiet.IdChitietdonthuoc)
                               .AndExpression(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0).Or(KcbDonthuocChitiet.Columns.TrangThai).IsNull().CloseExpression()
                               .Execute();
                        if (sluongdieuchinh == 0) sluongdieuchinh = sl_ke;
                        if (record > 0)
                        {
                            
                            string GUID = THU_VIEN_CHUNG.GetGUID();
                            //new Delete().From<TTamke>()
                            //    .Where(TTamke.Columns.IdPhieuCtiet).IsEqualTo(objChitiet.IdChitietdonthuoc)
                            //    .And(TTamke.Columns.IdPhieu).IsEqualTo(objChitiet.IdDonthuoc)
                            //    .And(TTamke.Columns.Loai).IsEqualTo(1).Execute();
                            THU_VIEN_CHUNG.UpdateKeTam(Utility.Int64Dbnull(objChitiet.IdChitietdonthuoc), Utility.Int64Dbnull(objChitiet.IdDonthuoc), GUID, "",
                                Utility.Int64Dbnull(objChitiet.IdThuockho), Utility.Int32Dbnull(objChitiet.IdThuoc),
                                Utility.Int16Dbnull(objChitiet.IdKho), sluongdieuchinh, (byte)LoaiTamKe.KEDONTHUOC, objChitiet.MaLuotkham, objChitiet.IdBenhnhan.Value, 0,
                                DateTime.Now, string.Format("Sửa số lượng thuốc từ giá trị {0} thành {1}", e.InitialValue, e.Value));

                            decimal thanhtien = sluongdieuchinh *
                                                (Utility.DecimaltoDbnull(grdChitietDonthuoc.GetValue("don_gia")) +
                                                 Utility.DecimaltoDbnull(grdChitietDonthuoc.GetValue("phu_thu")));
                            updateQtyinDataTable(objChitiet.IdChitietdonthuoc, sluongdieuchinh, thanhtien);
                            //grdPresDetail.CurrentRow.BeginEdit();
                            //grdPresDetail.CurrentRow.Cells["TT"].Value = thanhtien;
                            //grdPresDetail.CurrentRow.BeginEdit();
                        }
                        Utility.Log(Name, globalVariables.UserName, string.Format("Bán thuốc tại quầy: Sửa số lượng thuốc của bệnh nhân {0} có mã lần khám {1} và mã bệnh nhân là: {2} từ {3} sang {4}", objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, e.InitialValue.ToString(), objChitiet.SluongSua.ToString()), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Thuốc {0} đang có số lượng khả dụng: {1} {2}.\nSố lượng bạn mua ={3} nhiều hơn lượng khả dụng.\nĐề nghị điều chỉnh lại số lượng hợp lý.\nChú ý:Hệ thống đang kiểm tra theo Id-thuốc kho={4}\nBạn có thể thực hiện thêm đơn thuốc tại quầy nếu người bệnh muốn mua thêm", ten_thuoc, sluong_ton, ten_dvt, e.Value, objChitiet.IdThuockho));
                        e.Value = e.InitialValue;
                    }

                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ModifyCommand();
            }
        }

        void ChangeQuantity(KcbDonthuocChitiet objChitiet, decimal sl_cu, decimal sl_moi, DataRow currentDR)
        {

            //if (sl_moi < sl_cu)//Chỉ việc xóa trong bảng tạm kê số lượng cũ và bổ sung bảng tạm kê số lượng mới
            //{
            //    string GUID = THU_VIEN_CHUNG.GetGUID();
            //    new Delete().From<TTamke>().Where(TTamke.Columns.IdPhieuCtiet).IsEqualTo(objChitiet.IdChitietdonthuoc)
            //        .And(TTamke.Columns.Loai).IsEqualTo(100).Execute();
            //    THU_VIEN_CHUNG.UpdateKeTam(Utility.Int64Dbnull(objChitiet.IdChitietdonthuoc), Utility.Int64Dbnull(objChitiet.IdDonthuoc),
            //        GUID,
            //        Utility.Int64Dbnull(objChitiet.IdThuockho), Utility.Int32Dbnull(objChitiet.IdThuoc),
            //        Utility.Int16Dbnull(objChitiet.IdKho), Utility.DecimaltoDbnull(objChitiet.SluongSua), (byte)LoaiTamKe.KEDONTHUOC, objChitiet.MaLuotkham, objChitiet.IdBenhnhan.Value, 0,
            //        DateTime.Now, string.Format("Sửa số lượng thuốc từ giá trị {0} thành {1}", objChitiet.SoLuong, objChitiet.SluongSua));

            //    decimal thanhtien = Utility.DecimaltoDbnull(sl_moi) *
            //                        (Utility.DecimaltoDbnull(grdPresDetail.GetValue("don_gia")) +
            //                         Utility.DecimaltoDbnull(grdPresDetail.GetValue("phu_thu")));
            //    updateQtyinDataTable(objChitiet.IdChitietdonthuoc, Utility.DecimaltoDbnull(objChitiet.SluongSua), thanhtien);
            //    //grdPresDetail.CurrentRow.BeginEdit();
            //    //grdPresDetail.CurrentRow.Cells["TT"].Value = thanhtien;
            //    //grdPresDetail.CurrentRow.BeginEdit();
            //}
            //else
            //{
                //decimal chenhlech = sl_moi - sl_cu;
                ////Lấy tồn kho để kiểm tra xem số tồn còn >= chenhlech hay không?
                //decimal slton = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(objChitiet.IdKho), objChitiet.IdThuoc, -1l, Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1), (byte)0);
                //decimal slthat = slton - sl_cu;
                //if (slthat > chenhlech)
                //{
                //    DataTable listdata =
                //    new XuatThuoc().GetObjThuocKhoCollection(
                //         Utility.Int32Dbnull(objChitiet.IdKho),
                //        objChitiet.IdThuoc,
                //        -1,
                //       chenhlech,
                //        objLuotkham.IdLoaidoituongKcb.Value,
                //        Utility.ByteDbnull(objLuotkham.DungTuyen.Value, 0), 0);
                //    decimal soluongke = chenhlech;
                //    foreach (DataRow thuockho in listdata.Rows)
                //    {
                //        decimal soluong = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.SoLuong], 0);
                //        int IdThuockho = Utility.Int32Dbnull(thuockho[TThuockho.Columns.IdThuockho], 0);
                //        if (soluong > 0)
                //        {
                //            DataRow[] rowArray =
                //                m_dtChitietdonthuoc.Select(TThuockho.Columns.IdThuockho + "=" +
                //                                           Utility.sDbnull(
                //                                               thuockho[TThuockho.Columns.IdThuockho]) );
                //            if (rowArray.Length > 0)//Dòng thuốc kho đã tồn tại và giờ kê thêm số lượng
                //            {
                //                rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] =
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) +
                //                    soluong;
                //                rowArray[0]["TT_KHONG_PHUTHU"] =
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                //                rowArray[0]["TT"] =
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                //                    (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) +
                //                     Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                //                rowArray[0]["TT_BHYT"] =
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                //                rowArray[0]["TT_BN"] =
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                //                    (Utility.DecimaltoDbnull(
                //                        rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                //                     Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                //                rowArray[0]["TT_PHUTHU"] =
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                //                rowArray[0]["TT_BN_KHONG_PHUTHU"] =
                //                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                //                    Utility.DecimaltoDbnull(
                //                        rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                //            }
                //            else//1 dòng thuốc kho mới xuất hiện-->Cần thêm ngay vào CSDL của bảng chi tiết
                //            {

                //                DataRow row = m_dtChitietdonthuoc.NewRow();
                //                Utility.CopyData(currentDR, ref row);
                //                row[KcbDonthuocChitiet.Columns.IdThuockho] = IdThuockho;
                //                row[KcbDonthuocChitiet.Columns.SoLuong] = soluong;
                //                row[KcbDonthuocChitiet.Columns.SluongSua] = soluong;


                //                //Kiểm tra tương tác thuốc ở đây
                //                //Code more here
                //                //Cập nhật thông tin tiền
                //                row["TT_KHONG_PHUTHU"] =
                //                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                //                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                //                row["TT"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                //                            (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) +
                //                             Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                //                row["TT_BHYT"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                //                                 Utility.DecimaltoDbnull(
                //                                     row[KcbDonthuocChitiet.Columns.BhytChitra]);
                //                row["TT_BN"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                //                               (Utility.DecimaltoDbnull(
                //                                   row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                //                                Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu],
                //                                    0));
                //                row["TT_PHUTHU"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                //                                   Utility.DecimaltoDbnull(
                //                                       row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                //                row["TT_BN_KHONG_PHUTHU"] =
                //                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                //                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);


                //                m_dtChitietdonthuoc.Rows.Add(row);
                //                lstChitietdonthuoc.Add(getNewItem(row));

                //            }
                //            if (IdThuockho > 0)//100%
                //            {
                //                //Dùng bảng tạm kê để lưu trữ
            //                THU_VIEN_CHUNG.UpdateKeTam(-1, -1, GUID, IdThuockho, objThuoc.IdThuoc, Utility.Int16Dbnull(cboStock.SelectedValue), Utility.DecimaltoDbnull(soluong), (byte)LoaiTamKe.KEDONTHUOC,
                //                       txtPatientCode.Text, Utility.Int32Dbnull(txtPatientID.Text), 0, THU_VIEN_CHUNG.GetSysDateTime(), "Thêm mới thuốc");
                //            }


                //            Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdThuockho,
                //                IdThuockho.ToString());
                //            UpdateDataWhenChanged();
                //            if (soluong > Utility.DecimaltoDbnull(soluongke))
                //            {
                //                return;
                //            }
                //            else
                //            {
                //                soluongke = soluongke - soluong;
                //            }
                //        }

                //}
                //}
            //}
        }
        
        void updateQtyinDataTable(long IdChitietdonthuoc, decimal newQty, decimal TT)
        {
            try
            {
                DataRow[] arrDr = m_dtChitietdonthuoc.Select(string.Format("{0}={1}", KcbDonthuocChitiet.Columns.IdChitietdonthuoc, IdChitietdonthuoc));
                if(arrDr.Length>0)
                {
                    arrDr[0][KcbDonthuocChitiet.Columns.SluongSua] = newQty;
                    arrDr[0]["TT"] = TT;
                }
            }
            catch (Exception ex)
            {

              
            }
        }
        private void CauHinh()
        {
            //cmdHuycapphat.Visible = PropertyLib._HisDuocProperties.HuyXacNhan;
            dtNgayPhatThuoc.Enabled = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_CHOPHEPCHINHNGAYDUYETTHUOC", "1", true)=="1";
            string HIENTHIPHANTICHGIA_TRENLUOI = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_HIENTHIPHANTICHGIA_TRENLUOI", "0", false);
            grdThongTinDaThanhToan.RootTable.Columns["TT_KHONG_PHUTHU"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
            grdThongTinDaThanhToan.RootTable.Columns["TT_BN_KHONG_PHUTHU"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
            grdThongTinDaThanhToan.RootTable.Columns["TT_BHYT"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
            grdThongTinDaThanhToan.RootTable.Columns["TT_PHUTHU"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
            grdThongTinDaThanhToan.RootTable.Columns["TT_BN"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
            grdThongTinDaThanhToan.RootTable.Columns["bnhan_chitra"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
            grdThongTinDaThanhToan.RootTable.Columns["phu_thu"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
            grdThongTinDaThanhToan.RootTable.Columns["bhyt_chitra"].Visible = HIENTHIPHANTICHGIA_TRENLUOI == "1";
            switch (PropertyLib._ThanhtoanProperties.CachChietkhau)
            {
                case 0:
                    grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                    grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].EditType = EditType.NoEdit;
                    break;
                case 1:
                    grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = false;
                    grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                    grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].EditType = EditType.TextBox;
                    break;
                case 2:
                    grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                    grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].EditType = EditType.TextBox;
                    grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                    grdThongTinDaThanhToan.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].EditType = EditType.TextBox;
                    break;
            }
            //grdPresDetail.RootTable.Groups.Clear();
            //if (PropertyLib._ThamKhamProperties.Hienthinhomthuoc)
            //{
            //    GridEXColumn gridExColumn = grdPresDetail.RootTable.Columns["ma_loaithuoc"];
            //    var gridExGroup = new GridEXGroup(gridExColumn);
            //    gridExGroup.GroupPrefix = "Loại thuốc: ";
            //    grdPresDetail.RootTable.Groups.Add(gridExGroup);
            //}
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtEventTongTien_KeyPress(Object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txtTongTien = ((EditBox)(sender));
            Utility.FormatCurrencyHIS(txtTongTien);
        }
        private void setProperties()
        {
            try
            {
               
                foreach (Control control in pnlThongtintien.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();
                        txtFormantTongTien = ((EditBox)(control));
                        txtFormantTongTien.Clear();
                        txtFormantTongTien.ReadOnly = true;
                        //if (txtFormantTongTien.Font.Size < 9)
                        //    txtFormantTongTien.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold,
                        //        GraphicsUnit.Point, 0);
                        txtFormantTongTien.TextAlignment = TextAlignment.Far;
                        txtFormantTongTien.KeyPress += txtEventTongTien_KeyPress;
                        txtFormantTongTien.TextChanged += txtEventTongTien_TextChanged;
                    }
                }
               
            }
            catch (Exception exception)
            {
            }
        }
        void LoadUserConfigs()
        {
            chkThanhtoan.Checked = Utility.getUserConfigValue(chkThanhtoan.Tag.ToString(), Utility.Bool2byte(chkThanhtoan.Checked)) == 1;
            chkHuythanhtoan.Checked = Utility.getUserConfigValue(chkHuythanhtoan.Tag.ToString(), Utility.Bool2byte(chkHuythanhtoan.Checked)) == 1;
            chkHienthidvuhuyTT.Checked = Utility.getUserConfigValue(chkHienthidvuhuyTT.Tag.ToString(), Utility.Bool2byte(chkHienthidvuhuyTT.Checked)) == 1;
            chkRestoreDefaultPTTT.Checked = Utility.getUserConfigValue(chkRestoreDefaultPTTT.Tag.ToString(), Utility.Bool2byte(chkRestoreDefaultPTTT.Checked)) == 1;
            chkCapphat.Checked = Utility.getUserConfigValue(chkCapphat.Tag.ToString(), Utility.Bool2byte(chkCapphat.Checked)) == 1;
            chkHuyCapphat.Checked = Utility.getUserConfigValue(chkHuyCapphat.Tag.ToString(), Utility.Bool2byte(chkHuyCapphat.Checked)) == 1;

           
        }
        private DataTable m_dtKhothuoc=new DataTable();
        private void frm_phatthuoc_ngoaitru_Load(object sender, EventArgs e)
        {
            LoadUserConfigs();
           
            if (kieuthuoc_vt == "ALL")
                m_dtKhothuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NGOAITRU", "ALL", "CHANLE,LE", 100, 100, 1,trangthai_capcuu);// CommonLoadDuoc.LAYTHONGTIN_KHOVTTH_LE_NGOAITRU();
            else if (kieuthuoc_vt == "THUOC")
                m_dtKhothuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NGOAITRU", "THUOC,THUOCVT", "CHANLE,LE", 100, 100, 1, trangthai_capcuu);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NGOAITRU();
            else
                m_dtKhothuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NGOAITRU", "VT,THUOCVT", "CHANLE,LE", 100, 100, 1, trangthai_capcuu);// CommonLoadDuoc.LAYTHONGTIN_KHOLE_NGOAITRU();
            DataBinding.BindData(cboKho, m_dtKhothuoc,
                                     TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
            dtNgayPhatThuoc.Value = globalVariables.SysDate;
            DataTable dtTthai = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("TRANGTHAI_DONTHUOC").And(DmucChung.Columns.TrangThai).IsEqualTo(1).OrderAsc(DmucChung.Columns.SttHthi).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboTrangthai, dtTthai, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            txtPttt.Init();
            autoNganhang.Init();
            InitPTTTColumns();
            setProperties();
            TimKiemThongTinDonThuoc();
            txtPID.Focus();
            txtPID.SelectAll();

        }
        void InitPTTTColumns()
        {
            try
            {
                DataTable dtPTTT = txtPttt.dtData;
                GridEXColumn _colmaPttt = grdPayment.RootTable.Columns["ma_pttt"];
                _colmaPttt.HasValueList = true;
                _colmaPttt.LimitToList = true;

                GridEXValueListItemCollection _colmaPttt_Collection = grdPayment.RootTable.Columns["ma_pttt"].ValueList;
                foreach (DataRow item in dtPTTT.Rows)
                {
                    _colmaPttt_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }

                DataTable dtNganhang = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("NGANHANG").And(DmucChung.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
                GridEXColumn _colIDNganHang = grdPayment.RootTable.Columns["ma_nganhang"];
                _colIDNganHang.HasValueList = true;
                _colIDNganHang.LimitToList = true;

                GridEXValueListItemCollection _colIDNganHang_Collection = grdPayment.RootTable.Columns["ma_nganhang"].ValueList;
                foreach (DataRow item in dtNganhang.Rows)
                {
                    _colIDNganHang_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }


        }
        /// <summary>
        /// hàm thực hiện việc trạng thái thông tin của đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TimKiemThongTinDonThuoc();
        }
        private void TimKiemThongTinDonThuoc()
        {
            try
            {
                AllowSelectionChanged = false;
                int Status  =Utility.Int32Dbnull(cboTrangthai.SelectedValue);
               
                int NoiTru = 0;
                mv_dtDonthuoc = SPs.ThuocTimkiemdonthuocCapphatngoaitru(Utility.Int32Dbnull(txtPres_ID.Text, -1), txtPID.Text,
                                                           Utility.sDbnull(txtTenBN.Text), "ALL",
                                                           chkByDate.Checked ? dtFromdate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                           chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900", Status,
                                                            Utility.Int32Dbnull(cboKho.SelectedValue), Utility.ByteDbnull(cboKieudonthuoc.SelectedValue, 100), kieuthuoc_vt, trangthai_capcuu, 100).GetDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx_Basic(grdPres, mv_dtDonthuoc, true, true, "1=1", "ten_benhnhan");
                grdPres.AutoSizeColumns();
                RowFilterView();
                if (grdPres.GetDataRows().Length <= 0)
                {
                    m_dtChitietdonthuoc.Rows.Clear();
                    m_dtPayment = null;
                    m_dtPhieuChi = null;
                    m_dtChiPhiDaThanhToan = null;
                    ResetItems();
                    ClearControl();
                }
                else
                    grdPres.MoveFirst();
              //  ModifyCommand();
            }
            catch (Exception exception)
            {
                if(globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
               
            }
            finally
            {
                AllowSelectionChanged = true;
                grdPres_SelectionChanged(grdPres, new EventArgs());
                ModifyCommand();
            }
           
        }
        private void ClearControl()
        {
            try
            {
                foreach (Control control in pnlThongtintien.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                }
                
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// hàm thực hiện việc lọc filter của dược
        /// </summary>
        private void RowFilterView()
        {
            if (PropertyLib._HisDuocProperties.LocDonThuocKhiDuyet)
            {
                //string rowFilter = "1=1";

                //if (radChuaXacNhan.Checked) rowFilter = string.Format("{0}={1}", KcbDonthuoc.Columns.TrangThai, 0);
                //if (radDaXacNhan.Checked) rowFilter = string.Format("{0}={1}", KcbDonthuoc.Columns.TrangThai, 1);
                //    mv_dtDonthuoc.DefaultView.RowFilter = rowFilter;
                //    mv_dtDonthuoc.AcceptChanges();
            }
            
        }
        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPres_ID_Click(object sender, EventArgs e)
        {

        }

        private void txtPres_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if(Utility.Int32Dbnull(txtPres_ID.Text)>0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmdSearch.PerformClick();
                }
            }
           
        }
        void RestoredefaultPTTT()
        {
            txtPttt.SetDefault();
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            autoNganhang.Enabled = lstPTTT.Contains(txtPttt.MyCode);
            if (!autoNganhang.Enabled) autoNganhang.SetCode("-1");
        }
        /// <summary>
        /// hàm thực hiện việc dichuyeen thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged) return;
                if (Utility.isValidGrid(grdPres))
                {
                    dtNgayPhatThuoc.Value = DateTime.Now;
                    RestoredefaultPTTT();
                    Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                    KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(Pres_ID);
                    if(objDonthuoc!=null)
                    {
                        txtLoiDanBS.Text = objDonthuoc.LoidanBacsi;
                        txtChandoantheodon.Text = objDonthuoc.ChanDoan;
                    }    
                    objBenhnhan = KcbDanhsachBenhnhan.FetchByID(Utility.Int64Dbnull(grdPres.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1));
                    objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdPres.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1), Utility.sDbnull(grdPres.GetValue(KcbLuotkham.Columns.MaLuotkham), "-1"));
                    string ngaythanhtoan = Utility.sDbnull(grdPres.GetValue("ngay_thanhtoan"),"");
                    GetDataPresDetail();
                    GetChiPhiDaThanhToan();
                    LaydanhsachLichsuthanhtoan_phieuchi();
                    Laydanhsachphieuxuatthuoc();
                    LaydanhsachLichsu_phieutralaithuoc();
                    //dtNgayPhatThuoc.Value = ngaythanhtoan == "" ? globalVariables.SysDate : Convert.ToDateTime(ngaythanhtoan);
                }
                else
                {
                    txtLoiDanBS.Clear();
                    txtChandoantheodon.Clear();
                    grdChitietDonthuoc.DataSource = null;
                }
              
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ex.Message);
                
            }
            ModifyCommand();
        }
        void Laydanhsachphieuxuatthuoc()
        {
            try
            {
                DataTable dtData = SPs.ThuocLaydanhsachphieuxuatthuocbenhnhan(Pres_ID).GetDataSet().Tables[0];
                flowpnlPhieuxuatthuoc.Controls.Clear();
                foreach (DataRow dr in dtData.Rows)
                {
                    ucPhieuxuatthuocBN item = new ucPhieuxuatthuocBN(Utility.Int64Dbnull( dr[TPhieuXuatthuocBenhnhan.Columns.IdPhieu].ToString(),-1), dr[TPhieuXuatthuocBenhnhan.Columns.MaPhieu].ToString(), dr["ten_phieu"].ToString());
                    item._OnClick += item__OnClick;
                   
                    flowpnlPhieuxuatthuoc.Controls.Add(item);
                    toolTip1.SetToolTip(item._ScheduleObj, string.Format("Phiếu xuất thuốc cho bệnh nhân: {0}", dr["ten_phieu"].ToString()));
                    //item.Margin = m_objPadding;
                    item.Size = new Size(PropertyLib._PhieuxuatBNProperty.UCWidth, PropertyLib._PhieuxuatBNProperty.UCHeight);
                    item.ResetColor();
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
              
            }
        }
        void ResetItems()
        {
            ucPhieuxuatthuocBN _item = null;
            foreach (Control ctr in flowpnlPhieuxuatthuoc.Controls)
            {
                _item = ctr as ucPhieuxuatthuocBN;
                _item.Size = new Size(PropertyLib._PhieuxuatBNProperty.UCWidth, PropertyLib._PhieuxuatBNProperty.UCHeight);
                _item.ResetColor();
            }
            Application.DoEvents();
        }
        bool Forced2Select = false;
        bool MyClick = false;
        void item__OnClick(ucPhieuxuatthuocBN obj)
        {
            MyClick = true;
            ClickMe(obj);
            ModifyCommand();
            MyClick = false; 
        }
       
        void ClickMe(ucPhieuxuatthuocBN obj)
        {
            if (!System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
                ResetPreviousSelectedObject(flowpnlPhieuxuatthuoc, obj);
            if (Forced2Select)
                obj.SelectMe();
            else
                if (!obj.isPressed) obj.SelectMe();
                else
                    obj.UnSelectMe();
        }
        public void ResetPreviousSelectedObject(FlowLayoutPanel pnlItems, ucPhieuxuatthuocBN _Selected)
        {
            ucPhieuxuatthuocBN _item = null;
            try
            {
                foreach (Control ctr in pnlItems.Controls)
                {
                    _item = ctr as ucPhieuxuatthuocBN;
                    if (_item.isPressed && _item._ID != _Selected._ID)
                        _item.Reset();
                }
            }
            catch
            {
            }
            finally
            {
            }
            Application.DoEvents();
        }
        ucPhieuxuatthuocBN getSelectedObject()
        {
            if (flowpnlPhieuxuatthuoc.Controls.Count == 1) return flowpnlPhieuxuatthuoc.Controls[0] as ucPhieuxuatthuocBN;
            foreach (Control item in flowpnlPhieuxuatthuoc.Controls)
            {
                ucPhieuxuatthuocBN pxt = item as ucPhieuxuatthuocBN;
                if (pxt.isPressed)
                    return pxt;
            }
            return null;
        }
        private void ModifyCommand()
        {
            try
            {
                if (!AllowSelectionChanged) return;
                bool isValidParent = Utility.isValidGrid(grdPres);
                bool isValidPhieuthu = Utility.isValidGrid(grdPayment);
                bool donthuoctaiquay = Utility.sDbnull(Utility.getValueOfGridCell(grdPres, KcbDonthuoc.Columns.Donthuoctaiquay), "0") == "1";
                bool _chuathanhtoan = m_dtChitietdonthuoc != null && m_dtChitietdonthuoc.AsEnumerable().Any(c => c.Field<byte>(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan) == 0 && c.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) == 0);
                bool _dathanhtoan = m_dtChitietdonthuoc != null && m_dtChitietdonthuoc.AsEnumerable().Any(c => c.Field<byte>(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan) == 1); //Utility.sDbnull(Utility.getValueOfGridCell(grdPres, KcbDonthuoc.Columns.TrangthaiThanhtoan), "0") == "1";
                bool _dacapphathet = m_dtChitietdonthuoc != null && !m_dtChitietdonthuoc.AsEnumerable().Any(c => c.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) == 0);// Utility.sDbnull(Utility.getValueOfGridCell(grdPres, KcbDonthuoc.Columns.TrangThai), "0") == "1";
                bool tthai_capphat = m_dtChitietdonthuoc != null && m_dtChitietdonthuoc.AsEnumerable().Any(c => c.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) == 1 && c.Field<byte>(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan) == 1); ;// Utility.ByteDbnull(Utility.getValueOfGridCell(grdPres, KcbDonthuoc.Columns.TrangThai), "0");
                bool thanhtoanchuacapphat = m_dtChitietdonthuoc != null && m_dtChitietdonthuoc.AsEnumerable().Any(c => c.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) == 0 && c.Field<byte>(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan) == 1);
                bool isValidDetail = Utility.isValidGrid(grdChitietDonthuoc);
                cmdThemmoi.Enabled = true;
                cmdIndonthuoc.Enabled = isValidParent;
                cmdCapnhatdonthuoc.Enabled = isValidParent && donthuoctaiquay && !_dathanhtoan && !tthai_capphat;
                cmdXoadonthuoc.Enabled = isValidParent && donthuoctaiquay && !_dathanhtoan && !tthai_capphat;
                cmdThanhtoan.Enabled = cmdThanhtoan2.Enabled = isValidParent && isValidDetail && grdChitietDonthuoc.GetCheckedRows().Length > 0 && _chuathanhtoan;
                cmdHuythanhtoan.Enabled = cmdHuythanhtoan2.Enabled = isValidParent && isValidDetail && isValidPhieuthu && _dathanhtoan && !tthai_capphat;
                cmdPhanboHTTT.Enabled = isValidParent && isValidDetail && isValidPhieuthu && _dathanhtoan;
                cmdPhatThuoc.Enabled = cmdDuyetcapphat.Enabled = isValidParent && isValidDetail && thanhtoanchuacapphat && !_dacapphathet && (Loai_Nguoidung == "DUOC" || Loai_Nguoidung == "ALL"); // _dathanhtoan && tthai_capphat < 2;
                cmdHuycapphat.Enabled = cmdHuyduyetCapphat.Enabled = isValidParent && isValidDetail && tthai_capphat && (flowpnlPhieuxuatthuoc.Controls.Count == 1 || (flowpnlPhieuxuatthuoc.Controls.Count > 1 && getSelectedObject()!=null));
                cmdKiemTraSoLuong.Enabled = isValidParent && isValidDetail && _dathanhtoan && thanhtoanchuacapphat;
                cmdThemdonthuoc.Enabled = isValidParent;
                cmdTraLaiTien.Enabled = Utility.isValidGrid(grdPres) && grdThongTinDaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null;
                cmdInPhieuChi.Enabled = Utility.isValidGrid(grdPres) && grdPhieuChi.GetDataRows().Length > 0 && objLuotkham != null;
                cmdInBienlai.Enabled = cmdInhoadon.Enabled = Utility.isValidGrid(grdPres) && grdPayment.GetDataRows().Length > 0 && objLuotkham != null;
                modifyButtonsTraThuocTaiquay();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
          
            //if (!Utility.isValidGrid(grdPres) || !Utility.isValidGrid(grdPresDetail))
            //    {
            //        cmdPhatThuoc.Enabled = false;
            //        cmdHuycapphat.Enabled = false;
            //        cmdKiemTraSoLuong.Enabled = false;
            //    }
            //    else
            //    {
            //        int _daphat = m_dtChitietdonthuoc.Select(KcbDonthuocChitiet.Columns.TrangThai + "=1").Length;// Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.TrangThai));
            //        cmdHuycapphat.Enabled = !cmdPhatThuoc.Enabled;
            //        cmdHuycapphat.Enabled = Utility.Coquyen("quyen_huycapphatthuoc_ngoaitru");
            //        cmdKiemTraSoLuong.Enabled = _daphat <= 0;
            //        //Thread.Sleep(10);
            //        cmdPhatThuoc.Enabled = _daphat <= 0;
            //    }
        }
        private long Pres_ID=-1;
        private void GetDataPresDetail()
        {

            string ma_luotkham = Utility.sDbnull(Utility.getValueOfGridCell(grdPres,KcbLuotkham.Columns.MaLuotkham));
            long  v_IdDonthuoc= Utility.Int64Dbnull(Utility.getValueOfGridCell(grdPres, KcbDonthuoc.Columns.IdDonthuoc));
            long v_IdBenhnhan = Utility.Int64Dbnull(Utility.getValueOfGridCell(grdPres, KcbLuotkham.Columns.IdBenhnhan));
            int id_kho = Utility.Int32Dbnull(cboKho.SelectedValue,-1);
            m_dtChitietdonthuoc = SPs.KcbThanhtoanLaythongdonthuoctaiquayDethanhtoan(ma_luotkham, v_IdBenhnhan, v_IdDonthuoc, id_kho).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx_Basic(grdChitietDonthuoc, m_dtChitietdonthuoc, false, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
            Utility.AddColumToDataTable(ref m_dtChitietdonthuoc, "colCHON", typeof(byte));
            UpdateTuCheckKhiChuaThanhToan();
            SetSumTotalProperties();
            m_dtChitietdonthuoc.AcceptChanges();
           
        }
      
      
        /// <summary>
        /// hàm thực hiện việc cho phím tắt thực hiện tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_phatthuoc_ngoaitru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            else if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            else if (e.KeyCode == Keys.F2)
            {
                txtPID.Clear();
                txtPID.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                grdPres_SelectionChanged(grdPres, new EventArgs());
            }
            else if (e.Control && e.KeyCode == Keys.S) cmdPhatThuoc_Click(cmdPhatThuoc, new EventArgs());
            else if (e.Control && e.KeyCode == Keys.N) cmdThemdonthuoc.PerformClick();
            else if (e.Control && e.KeyCode == Keys.U) cmdCapnhatdonthuoc.PerformClick();
            else if (e.Control && e.KeyCode == Keys.T) cmdThemdonthuoc.PerformClick();
            else if (e.Control && e.KeyCode == Keys.D) cmdXoadonthuoc.PerformClick();
            else if (e.Control && e.KeyCode == Keys.P) cmdIndonthuoc.PerformClick();
            else if (e.Control && e.KeyCode == Keys.H) cmdPhanboHTTT.PerformClick();
        }
        void DuyetCapphatthuoc()
        {
            try
            {
                Utility.SetMsg(uiStatusBar2.Panels[1], "", false);
                //Check xem có bị làm phiếu chi trả lại tiền không
                DataTable dtphieuchi = new Select("1").From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID)
                    .And(KcbDonthuocChitiet.Columns.TrangthaiHuy).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.Columns.IdThanhtoan).IsGreaterThan(0)
                    .ExecuteDataSet().Tables[0];
                if (dtphieuchi.Rows.Count > 0)
                {
                    Utility.ShowMsg("Đơn thuốc bạn chọn cấp phát đã có thuốc trả lại tiền(TNV đã lập phiếu chi). Vui lòng kiểm tra lại");
                    return;
                }
                long presId = Utility.Int64Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1);
                Int16 stockId = Utility.Int16Dbnull(m_dtChitietdonthuoc.Rows[0][KcbDonthuocChitiet.Columns.IdKho]);
                //Lấy từ CSDL cho chắc ăn thay vì lấy trên lưới danh sách
                DataTable dtChitietcapphat = SPs.ThuocLaydanhsachchitietthanhtoanchuacapphat(Pres_ID).GetDataSet().Tables[0];
                if (dtChitietcapphat.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tồn tại thuốc thanh toán trên lưới danh sách mà chưa được duyệt cấp phát(Có thể trong lúc bạn chưa duyệt thuốc thì đã bị hủy thanh toán bởi người khác). Vui lòng kiểm tra lại");
                    return;
                }
                //if (!KiemtradonthuocOK(dtChitietcapphat)) return;
                Dictionary<long, string> lstID_Err = new Dictionary<long, string>();
                ActionResult ActionResult = new CapphatThuocKhoa().KiemtratonthuocNgoaitru(dtChitietcapphat, stockId, true, ref lstID_Err);
                switch (ActionResult)
                {
                    case ActionResult.Success:
                        //Utility.ShowMsg("Thuốc trong kho còn đủ để cấp phát");
                        break;
                    case ActionResult.NotEnoughDrugInStock:
                        (from p in m_dtChitietdonthuoc.AsEnumerable() where lstID_Err.ContainsKey(Utility.Int64Dbnull(p["id_thuockho"])) select p).ToList().ForEach(x => { x["isErr"] = 1; x["msg"] = lstID_Err[Utility.Int64Dbnull(x["id_thuockho"])]; });
                        m_dtChitietdonthuoc.AcceptChanges();
                        break;
                    case ActionResult.UNKNOW:
                        Utility.ShowMsg("Tồn tại thuốc trong đơn cấp phát đã bị xóa khỏi bảng danh mục thuốc. Mời bạn kiểm tra lại!");
                        return;

                }
                Utility.EnableButton(cmdPhatThuoc, false);
                cmdPhatThuoc.Cursor = Cursors.WaitCursor;
                
                string ErrMsg="";
                if (presId > 0 && stockId > 0)
                {
                    try
                    {
                        KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(presId);
                        if (objDonthuoc.NgayKedon.ToString("dd/MM/yyyy") != dtNgayPhatThuoc.Value.ToString("dd/MM/yyyy"))
                        {
                            frm_ChonngayXacnhan _ChonngayXacnhan = new frm_ChonngayXacnhan();
                            _ChonngayXacnhan.pdt_InputDate = objDonthuoc.NgayKedon;
                            _ChonngayXacnhan.ShowDialog();
                            if (_ChonngayXacnhan.b_Cancel)
                                return;
                            else
                                dtNgayPhatThuoc.Value = _ChonngayXacnhan.pdt_InputDate;
                        }
                        else//Kiểm tra nếu ngày phát thuốc<ngày kê đơn  (do bật form phát thuốc này trước cả khi các đơn thuốc được kê)
                        {
                            if (objDonthuoc.NgayKedon > dtNgayPhatThuoc.Value)
                                dtNgayPhatThuoc.Value = DateTime.Now;
                        }
                        if (objDonthuoc.NgayKedon > dtNgayPhatThuoc.Value)
                        {
                            Utility.ShowMsg(string.Format("Thời điểm duyệt thuốc phải >= thời điểm kê đơn {0}", objDonthuoc.NgayKedon.ToString("dd/MM/yyyy HH:mm")));
                        }
                        //Lấy danh sách thuốc đã thanh toán nhưng chưa cấp phát
                    List<long> lstIdchitietcancaphat    =dtChitietcapphat.AsEnumerable().Select(c=>c.Field<long>(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)).Distinct().ToList<long>();
                        //List<long> lstIdchitietcancaphat = (from p in grdPresDetail.GetCheckedRows().AsEnumerable()
                        //                                    where Utility.ByteDbnull(p.Cells[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan].Value, -1)==1
                        //                                    && Utility.ByteDbnull(p.Cells[KcbDonthuocChitiet.Columns.TrangThai].Value, -1) == 0
                        //                                    select Utility.Int64Dbnull(p.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1)).Distinct().ToList<long>();
                        ActionResult actionResult = new XuatThuoc().LinhThuocBenhNhan(presId, stockId, lstIdchitietcancaphat, dtNgayPhatThuoc.Value,ref ErrMsg);

                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                UpdateHasConfirm(lstIdchitietcancaphat);
                                Laydanhsachphieuxuatthuoc();
                                Utility.SetMsg(uiStatusBar2.Panels[1], "Bạn thực hiện việc phát thuốc thành công", false);
                                break;
                            case ActionResult.NotEnoughDrugInStock:
                                Utility.ShowMsg(string.Format("Thuốc không đủ cấp phát. Mời bạn kiểm tra lại\n{0}", ErrMsg));
                                break;
                            case ActionResult.Error:
                                Utility.SetMsg(uiStatusBar2.Panels[1], "Lỗi trong quá trình phát thuốc cho bệnh nhân", true);
                                break;
                            case ActionResult.NodataFound:
                                Utility.SetMsg(uiStatusBar2.Panels[1], "Không có chi tiết cấp phát. Vui lòng làm mới lại dữ liệu", true);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi trong quá trình phát thuốc" + ex.Message);
                    }
                }
                cmdPhatThuoc.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                cmdPhatThuoc.Enabled = true && Loai_Nguoidung == "DUOC" || Loai_Nguoidung == "ALL";
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin theo đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPhatThuoc_Click(object sender, EventArgs e)
        {
            DuyetCapphatthuoc();
        }

        private void UpdateHasConfirm(List<long> lstIdchitietcancaphat)
        {
                foreach (GridEXRow gridExRow in grdChitietDonthuoc.GetDataRows())
                {
                    if (lstIdchitietcancaphat.Contains(Utility.Int64Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value)))
                    {
                        gridExRow.BeginEdit();
                        gridExRow.Cells[KcbDonthuocChitiet.Columns.TrangThai].Value = 1;
                        gridExRow.EndEdit();
                    }
                }
                grdChitietDonthuoc.UpdateData();
                m_dtChitietdonthuoc.AcceptChanges();
                var query = from donthuoc in grdChitietDonthuoc.GetDataRows().AsEnumerable()
                            where
                                Utility.Int64Dbnull(donthuoc.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value) == Pres_ID
                            select donthuoc;
                if (query.Any())
                {
                    Pres_ID = Utility.Int64Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                    SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID)
                        .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                    int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                    if (PropertyLib._HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        mv_dtDonthuoc.AcceptChanges();
                    }
                    else
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        mv_dtDonthuoc.AcceptChanges();
                    }
                }
        }
        /// <summary>
        /// hàm thực hiện việc update thông tin hủy
        /// </summary>
        private void UpdateHuyHasConfirm()
        {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdChitietDonthuoc.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells[KcbDonthuocChitiet.Columns.TrangThai].Value = 0;
                    gridExRow.EndEdit();
                }
                grdChitietDonthuoc.UpdateData();
                m_dtChitietdonthuoc.AcceptChanges();
                var query = from donthuoc in grdChitietDonthuoc.GetDataRows().AsEnumerable()
                            where
                                Utility.Int64Dbnull(donthuoc.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value) == Pres_ID
                            select donthuoc;
                if (query.Any())
                {
                    Pres_ID = Utility.Int64Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                    SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID)
                        .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                    int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                    if (PropertyLib._HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        mv_dtDonthuoc.AcceptChanges();
                    }
                    else
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        mv_dtDonthuoc.AcceptChanges();
                    }
                }

        }
        private bool InValiDonthuoc()
        {

           Pres_ID = Utility.Int64Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1);
            SqlQuery sqlQuery = new Select().From(KcbDonthuoc.Schema)
                .Where(KcbDonthuoc.Columns.TrangThai).IsEqualTo(1)
                .And(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(Pres_ID);
            if(sqlQuery.GetRecordCount()>0)
            {
                Utility.ShowMsg("Đơn thuốc đã phát thuốc, Mời bạn xem lại thông tin ","Thông báo",MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool InValiHuyDonthuoc()
        {
            if (grdChitietDonthuoc.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Không tìm thấy chi tiết đơn thuốc. Bạn cần chọn ít nhất 1 đơn thuốc có chi tiết để thao tác", "Thông báo", MessageBoxIcon.Error);

                return false;
            }
            int tthai_chot = Utility.Int32Dbnull(grdPres.GetValue("tthai_chot"), -1);
            if (tthai_chot == 1)
            {
                Utility.ShowMsg("Đơn thuốc đã được chốt nên không thể hủy. Đề nghị bạn kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            if (m_dtChitietdonthuoc.Select(string.Format("{0}={1}", KcbDonthuocChitiet.Columns.TrangThai ,1)).Length<=0)
            {
                Utility.ShowMsg("Đơn thuốc chưa có chi tiết được cấp phát thuốc nên không thể hủy. Đề nghị bạn kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// /hàm thực hiện việc khởi tạo thông tin của phiếu xuất cho bệnh nhân
        /// </summary>
        /// <param name="objPrescription"></param>
        /// <returns></returns>
       
        private TPhieuXuatthuocBenhnhanChitiet []CreatePhieuXuaChiTiet()
        {
            int length = 0;
            int idx = 0;
            var arrPhieuXuatCT = new TPhieuXuatthuocBenhnhanChitiet[length];
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdChitietDonthuoc.GetDataRows())
            {
                if(gridExRow.RowType==RowType.Record)
                {
                    arrPhieuXuatCT[idx]=new TPhieuXuatthuocBenhnhanChitiet();
                    arrPhieuXuatCT[idx].ChiDan =Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.MotaThem].Value);
                    arrPhieuXuatCT[idx].SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoLuong].Value);
                    arrPhieuXuatCT[idx].IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,-1);
                    arrPhieuXuatCT[idx].DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value);
                 
                    
                    idx++;
                }
            }
            return arrPhieuXuatCT;
        }
        /// <summary>
        /// hàm thưc hiện việc kiểm tra thông tin của kho có đủ thuốc không 
        /// Nếu không đủ không cho phát thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdKiemTraSoLuong_Click(object sender, EventArgs e)
        {
            //if(!KiemtradonthuocOK())return;
            //else
            //{
            //    Utility.ShowMsg("Bạn có thể xác nhận phiếu lĩnh thuốc của bệnh nhân\n Mời bạn phát thuốc","Thông báo",MessageBoxIcon.Information);
            //}
        }

        private bool KiemtradonthuocOK(DataTable dtChitietcapphat)
        {
            try
            {
               // if (!radChuaXacNhan.Checked && !grdPres.GetDataRows().Any()) return false;
                string idLoaidoituongKcb = Utility.GetValueFromGridColumn(grdPres, "id_loaidoituong_kcb");
                string inphieuDct = Utility.sDbnull(grdPres.GetValue("id_phieu_dct"), "0");
                string dathanhtoan = Utility.GetValueFromGridColumn(grdPres, "dathanhtoan");
                long  presId = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                string ma_luotkham = Utility.sDbnull(grdPres.GetValue(KcbDonthuoc.Columns.MaLuotkham));
                string  tenBenhnhan = Utility.sDbnull(grdPres.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan));

                //DataSet dskiemtra = SPs.KcbNgoaitruKiemtraCapphatthuoc(presId, ma_luotkham).GetDataSet();
                //SqlQuery sqlkt = new Select().From(TPhieuXuatthuocBenhnhan.Schema).Where(TPhieuXuatthuocBenhnhan.Columns.IdDonthuoc).IsEqualTo(presId);
                //SqlQuery sqlktdonthuoc = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(presId);

                //Tạm bỏ đi sẽ dùng hàm khác kiểm tra sau 230530
                //if (dskiemtra.Tables[0].Rows.Count <= 0)
                //{
                //    Utility.ShowMsg(string.Format("Đơn thuốc của bệnh nhân {0} không tồn tại nữa! Bạn cần tìm kiếm lại thông tin đơn thuốc", tenBenhnhan));
                //    log.Trace(string.Format("Đơn thuốc của bệnh nhân {0} không tồn tại nữa! Bạn cần tìm kiếm lại thông tin đơn thuốc", tenBenhnhan));
                //    return false;
                //}
                //if (dskiemtra.Tables[1].Rows.Count > 0)
                //{
                //    Utility.ShowMsg(string.Format("Đơn thuốc của bệnh nhân {0} đã được cấp phát!", tenBenhnhan));
                //    log.Trace(string.Format("Đơn thuốc của bệnh nhân {0} đã được cấp phát!", tenBenhnhan));
                //    return false;
                //}
                //    if (idLoaidoituongKcb == "1")
                //    {
                //        if (dskiemtra.Tables[2].Rows.Count > 0)
                //        {
                //            Utility.ShowMsg(
                //          string.Format(
                //              "Đối tượng bệnh nhân Dịch vụ đang chọn chưa thanh toán đơn thuốc nên bạn không thể thực hiện cấp phát." +
                //              "\nĐề nghị bệnh nhân đi nộp tiền thanh toán trước khi quay lại lĩnh thuốc"));
                //            return false;
                //        }
                //    }
                //    else
                //    {
                //        if (dskiemtra.Tables[2].Rows.Count <= 0)
                //        {
                //            Utility.ShowMsg(string.Format("Đối tượng bệnh nhân BHYT đang chọn chưa in phôi BHYT nên bạn không thể thực hiện cấp phát thuốc." +
                //              "\nĐề nghị bệnh nhân đến quầy thanh toán in phôi BHYT trước khi quay lại lĩnh thuốc"));
                //            return false;
                //        }

                //    }
                    
                    foreach (DataRow dr in dtChitietcapphat.Rows)//m_dtChitietdonthuoc.Rows)
                {
                    
                    long idDonthuoc = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdDonthuoc]);
                    int idThuoc = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc]);
                    string drugName = Utility.sDbnull(dr["ten_chitietdichvu"]);
                    int idKho = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdKho]);
                    int idThuockho = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdThuockho]);
                    decimal soLuong = Utility.DecimaltoDbnull(dr[KcbDonthuocChitiet.Columns.SoLuong]);
                    decimal soLuongTon = CommonLoadDuoc.SoLuongTonTrongKho_phatthuoc(idDonthuoc, idKho, idThuoc, idThuockho, 0, (byte)0);//Ko cần kiểm tra chờ xác nhận
                    if (soLuongTon < soLuong)
                    {
                        string errMsg = string.Format("ID thuốc: {0}\nid thuốc kho: {1}\nSố lượng khả dụng: {2}\nSố lượng bị trừ: {3}", idThuoc.ToString(), idThuockho, soLuongTon.ToString(), soLuong.ToString());
                        Utility.ShowMsg(errMsg);//string.Format("Bạn không thể xác nhận đơn thuốc, Vì thuốc :id thuốc={0}, tên thuốc={1}, id thuốc kho={2} số lượng tồn hiện tại trong kho ={3} không \n Mời bạn xem lại số lượng", idThuoc, drugName, idThuockho, soLuongTon));
                        Utility.GonewRowJanus(grdChitietDonthuoc, KcbDonthuocChitiet.Columns.IdThuoc, idThuoc.ToString());
                        return false;
                    }
                }
                if (chkCapphat.Checked)
                {
                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn cấp phát đơn thuốc đang chọn của người bệnh {0}. Thuốc sẽ chính thức trừ khỏi kho sau khi cấp phát", tenBenhnhan), "Thông báo", true))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// hàm thực hiện việc di chuyển thôn gtin trên đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện viecj cấu hình 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._HisDuocProperties);
                frm.ShowDialog();
                CauHinh();
            }
            catch(Exception exception)
            {
                Utility.ShowMsg("Lỗi"+ exception.Message);
            }
          
        }

        private void LoadLayout()
        {

            string layoutDir = GetLayoutDirectory() + @"\GridEXLayout.gxl";

            if (File.Exists(layoutDir))
            {

                FileStream layoutStream;

                layoutStream = new FileStream(layoutDir, FileMode.Open);

                grdChitietDonthuoc.LoadLayoutFile(layoutStream);

                layoutStream.Close();

            }

        }
        private string GetLayoutDirectory()
        {
            DirectoryInfo dInfo;
            dInfo = new DirectoryInfo(Application.ExecutablePath).Parent;

            dInfo = new DirectoryInfo(dInfo.FullName + @"\LayoutData");
            if (!dInfo.Exists) dInfo.Create();
            return dInfo.FullName;
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPres_SelectionChanged(grdPres, e);
        }
        void HuyCapphatthuoc()
        {
            try
            {
                ucPhieuxuatthuocBN selectedObject = getSelectedObject();
                if (selectedObject == null)
                {
                    Utility.ShowMsg("Bạn cần chọn một phiếu xuất thuốc bên dưới để thực hiện hủy cấp phát", "thông báo", MessageBoxIcon.Information);
                    return;
                }
                if (chkHuyCapphat.Checked)
                {
                    if (!Utility.AcceptQuestion("Bạn có muốn thực hiện hủy phát thuốc cho bệnh nhân \n Dữ liệu hủy sẽ được trả lại kho phát thuốc", "Thông báo", true))
                    {
                        return;
                    }
                }
                if (!InValiHuyDonthuoc()) return;
                frm_NhaplydoHuy _NhaplydoHuy = new frm_NhaplydoHuy("LYDOHUYXACNHAN", "HỦY XÁC NHẬN ĐƠN THUỐC", "Chọn lý do hủy xác nhận trước khi thực hiện...", "Lý do hủy", "Ngày hủy");
                _NhaplydoHuy.ShowDialog();
                if (!_NhaplydoHuy.m_blnCancel)
                {
                    Int16 stockID = Utility.Int16Dbnull(m_dtChitietdonthuoc.Rows[0][KcbDonthuocChitiet.Columns.IdKho]);
                    dtNgayPhatThuoc.Value = globalVariables.SysDate;
                    try
                    {
                        ActionResult actionResult =
                           new XuatThuoc().HuyXacNhanDonThuocBN(Pres_ID,selectedObject._ID, stockID, _NhaplydoHuy.ngay_thuchien, _NhaplydoHuy.ten);
                        switch (actionResult)
                        {
                            case ActionResult.DataUsed:
                                Utility.ShowMsg("Một trong các thuốc bạn chọn đã được sử dụng nên bạn không thể thực hiện hủy xác nhận", "thông báo", MessageBoxIcon.Information);
                                break;
                            case ActionResult.Success:
                                UpdateHuyHasConfirm();
                                Laydanhsachphieuxuatthuoc();
                                Utility.Log(this.Name, globalVariables.UserName,
                                                   string.Format(
                                                       "Hủy phát thuốc của bệnh nhân có mã lần khám {0} và mã bệnh nhân là: {1}. Đơn thuốc {2} bởi {3}",
                                                       Utility.sDbnull(grdPres.CurrentRow.Cells["ma_luotkham"].Value),
                                                       Utility.sDbnull(grdPres.CurrentRow.Cells["id_benhnhan"].Value),
                                                       Utility.sDbnull(grdPres.CurrentRow.Cells["id_donthuoc"].Value),
                                                       globalVariables.UserName), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                Utility.ShowMsg("Bạn thực hiện việc hủy phát thuốc thành công", "thông báo", MessageBoxIcon.Information);
                                break;
                            case ActionResult.Cancel:
                                Utility.ShowMsg("Tồn tại chi tiết thuốc đã được trả lại tiền nên bạn không thể hủy xác nhận. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình hủy phát thuốc cho bệnh nhân", "Thông báo", MessageBoxIcon.Error);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi trong quá trình hủy đơn thuốc" + ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        /// <summary>
        /// hàm thực hiện việc hủy thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHuyDonThuoc_Click(object sender, EventArgs e)
        {
            HuyCapphatthuoc();
        }
        private KcbDonthuocChitiet []CreatePresDetail()
        {
            int idx = 0;
            int length = 0;
            var query = from chitiet in grdChitietDonthuoc.GetDataRows()
                        let y = chitiet.RowType == RowType.Record
                        select y;
            length = query.Count();
            var arrDetail = new KcbDonthuocChitiet[length];
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdChitietDonthuoc.GetDataRows())
            {
                arrDetail[idx]=new KcbDonthuocChitiet();
              
                arrDetail[idx].IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value);
                arrDetail[idx].IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value);
                arrDetail[idx].IdChitietdonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value);
                arrDetail[idx].IdKho = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdKho].Value);
                arrDetail[idx].DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value);
                arrDetail[idx].PhuThu = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.PhuThu].Value);
                arrDetail[idx].SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoLuong].Value);
                arrDetail[idx].MotaThem = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.MotaThem].Value);
                arrDetail[idx].ChidanThem = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.ChidanThem].Value);
                arrDetail[idx].CachDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.CachDung].Value);
                arrDetail[idx].DonviTinh = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonviTinh].Value);
                arrDetail[idx].SoluongDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoluongDung].Value);
                arrDetail[idx].SolanDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SolanDung].Value);
                arrDetail[idx].IdThanhtoan = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThanhtoan].Value);
                arrDetail[idx].TuTuc = Utility.ByteDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.TuTuc].Value);
                idx++;
            }
            return arrDetail;
        }
        private bool InValiXoaThongTin()
        {
            if (grdChitietDonthuoc.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi để thực hiện việc xóa thông tin đơn thuốc", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdChitietDonthuoc.GetCheckedRows())
            {
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bản ghi đã thanh toán, bạn không thể xóa thông tin ", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdChitietDonthuoc.GetCheckedRows())
            {
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bạn phải chọn những bản ghi chưa xác nhận", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
       
       

        private void radTatCa_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }

        private void radChuaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }

        private void radDaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }
        private void txtPID_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (txtPID.Text.Trim() != "" && Utility.Int32Dbnull(txtPID.Text) > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        string patient_ID = Utility.GetYY(globalVariables.SysDate) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPID.Text, 0), "000000");
                        txtPID.Text = patient_ID;
                        int Status = -1;
                        int NoiTru = 0;
                        mv_dtDonthuoc =
                            SPs.ThuocTimkiemdonthuocCapphatngoaitru(-1, txtPID.Text,"","ALL",
                                                                   "01/01/1900","01/01/1900", Status,
                                                                    Utility.Int32Dbnull(cboKho.SelectedValue),100, kieuthuoc_vt,trangthai_capcuu,100).
                                GetDataSet().Tables[0];

                        Utility.SetDataSourceForDataGridEx(grdPres, mv_dtDonthuoc, true, true, "1=1", "ten_benhnhan asc");
                        RowFilterView();
                     //   ModifyCommand();
                    }
                    catch (Exception exception)
                    {
                        if (globalVariables.IsAdmin)
                        {
                            Utility.ShowMsg(exception.ToString());
                        }

                    }
                    finally
                    {
                        ModifyCommand();
                    }
                }
            }
        }
        private void GetChanDoan(string icdChinh, string idcPhu, ref string icdName, ref string icdCode)
        {
            try
            {
                List<string> lstIcd = icdChinh.Split(',').ToList();
                DmucBenhCollection list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstIcd));
                foreach (DmucBenh item in list)
                {
                    icdName += item.TenBenh + "; ";
                    icdCode += item.MaBenh + "; ";
                }
                lstIcd = idcPhu.Split(',').ToList();
                list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstIcd));
                foreach (DmucBenh _item in list)
                {
                    icdName += _item.TenBenh + "; ";
                    icdCode += _item.MaBenh + "; ";
                }
                if (icdName.Trim() != "") icdName = icdName.Substring(0, icdName.Length - 1);
                if (icdCode.Trim() != "") icdCode = icdCode.Substring(0, icdCode.Length - 1);
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin) Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private readonly KCB_KEDONTHUOC _kcbKedonthuoc = new KCB_KEDONTHUOC();
        private void PrintPres(int presID, string forcedTitle)
        {
            DataTable v_dtDataOrg = _kcbKedonthuoc.LaythongtinDonthuoc_In(presID);

            DataRow[] arrDR = v_dtDataOrg.Select("tuvan_them=0");
            if (arrDR.Length <= 0)
            {
                PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
                return;
            }
            DataTable v_dtData = arrDR.CopyToDataTable();


            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdChitietDonthuoc.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            bool chandoangiunguyen = THU_VIEN_CHUNG.Laygiatrithamsohethong("DONTHUOC_INCHANDOANTHEOBACSI_KE", "1", true) == "1";
            if (!chandoangiunguyen)
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                            Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                if (!chandoangiunguyen)
                    drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
             List<string> lstmatinhchat = (from p in v_dtData.AsEnumerable()
                                          select Utility.sDbnull(p["ma_tinhchat"], "")).Distinct().ToList<string>();
            foreach (string ma_tinhchat in lstmatinhchat)
            {
                DataRow[] arrTemp = v_dtData.Select(string.Format("(ma_tinhchat='{0}' or ma_tinhchat is null) and printed=0", ma_tinhchat));
                DataTable v_PrintData = v_dtData.Clone();
                if (arrTemp.Length > 0) v_PrintData = arrTemp.CopyToDataTable();//Chắc chắn có dữ liệu nên hàm copy ko bị lỗi
                if (v_PrintData.Rows.Count <= 0) continue;
                //Lấy danh sách các reportcode của từng tính chất thuốc
                string report_code = Utility.sDbnull(v_PrintData.Rows[0]["report_code"], "DONTHUOC_THUONG");
                //Lấy lại dữ liệu của tất cả các thuốc có cùng report nhưng khác tính chất để in đảm bảo ko bị tách đơn
                v_PrintData = v_dtData.Select(string.Format("report_code='{0}' and printed=0", report_code)).CopyToDataTable();
                //Đánh dấu trạng thái đã in để tránh in lại ở vòng for tính chất
                (from p in v_dtData.AsEnumerable() where Utility.sDbnull(p["report_code"], "") == report_code select p).ToList().ForEach(x => x["printed"] = 1);

                List<string> lstReportCode = v_PrintData.Rows[0]["report_code"].ToString().Split('@')[0].Split(';').ToList<string>();
                if (lstReportCode.Count <= 0) lstReportCode.Add("thamkham_InDonthuocA4");
                foreach (string _rcode in lstReportCode)
                {
                    string KhoGiay = "A100";// "A5";//Truyền giá trị này để giữ nguyên report
                    if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
                    var reportDocument = new ReportDocument();
                    string tieude = "", reportname = "", reportCode = "";
                    reportCode =string.Format("{0}_quay", _rcode);
                    reportDocument = Utility.GetReport(reportCode, KhoGiay, ref tieude, ref reportname);
                    if (reportDocument == null)
                    {
                        //Lấy mặc định do chưa được khai báo trong danh mục tính chất thuốc
                        switch (KhoGiay)
                        {
                            case "A5":
                                reportCode = "thamkham_InDonthuocA5";
                                reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                                break;
                            case "A4":
                                reportCode = "thamkham_InDonthuocA4";
                                reportDocument = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref reportname);
                                break;
                            default:
                                reportCode = "thamkham_InDonthuocA5";
                                reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                                break;
                        }
                    }
                    if (reportDocument == null) return;
                    if (Utility.DoTrim(forcedTitle).Length > 0)
                        tieude = forcedTitle;
                    Utility.WaitNow(this);
                    ReportDocument crpt = reportDocument;
                    frmPrintPreview objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
                    objForm.nguoi_thuchien = Utility.sDbnull(v_dtData.Rows[0]["ten_bacsikedon"], "");
                    try
                    {
                        objForm.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm.mv_sReportCode = reportCode;
                        crpt.SetDataSource(v_PrintData);
                        Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                        Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                        Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                        Utility.SetParameterValue(crpt, "ReportTitle", "ĐƠN THUỐC");
                        Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                        Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                        objForm.crptViewer.ReportSource = crpt;
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                                   PropertyLib._MayInProperties.PreviewInDonthuoc))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                            objForm.ShowDialog();
                            //cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                        }
                        else
                        {
                            objForm.addTrinhKy_OnFormLoad();
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            crpt.PrintToPrinter(1, false, 0, 0);
                        }

                        Utility.DefaultNow(this);
                    }
                    catch (Exception ex)
                    {
                        Utility.DefaultNow(this);
                    }
                    finally
                    {

                    }
                }//Kết thúc vòng for qua các liên trong tính chất
            }//Kết thúc vòng for tính chất
            //In đơn tư vấn thêm(nếu có)
            PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
        }

        private void PrintPres_old(int presID, string forcedTitle)
        {
            DataTable v_dtDataOrg = _kcbKedonthuoc.LaythongtinDonthuocTaiQuay_In(presID);

            DataRow[] arrDR = v_dtDataOrg.Select("tuvan_them=0");
            if (arrDR.Length <= 0)
            {
                PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
                return;
            }
            DataTable v_dtData = arrDR.CopyToDataTable();
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            if (v_dtData != null && v_dtData.Rows.Count > 0)
                GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                            Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "", reportCode = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonthuocA4";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            frmPrintPreview objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
            objForm.nguoi_thuchien = Utility.sDbnull(v_dtData.Rows[0]["ten_bacsikedon"], "");
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", "ĐƠN THUỐC");
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                           PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                    //cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
            finally
            {
                PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
            }
        }
        private void PrintTuvanthem(int presID, string forcedTitle, DataTable p_dtData)
        {

            DataRow[] arrDR = p_dtData.Select("tuvan_them=1");
            if (arrDR.Length <= 0) return;
            DataTable v_dtData = arrDR.CopyToDataTable();
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            if (v_dtData != null && v_dtData.Rows.Count > 0)
                GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                            Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "", reportCode = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            frmPrintPreview objForm = new frmPrintPreview("IN ĐƠN TƯ VẤN", crpt, true, true);
            objForm.nguoi_thuchien = Utility.sDbnull(v_dtData.Rows[0]["ten_bacsikedon"], "");
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                           PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                   // cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        string m_strMaLuotkham = "";
        private void cmdPrintPres_Click(object sender, EventArgs e)
        {
            m_strMaLuotkham=Utility.sDbnull(grdPres.GetValue(KcbDonthuoc.Columns.MaLuotkham));
            int presId = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
            PrintPres(presId, "");
            //try
            //{
            //    long presId = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
            //    string maLuotkham = Utility.sDbnull(grdPres.GetValue(KcbDonthuoc.Columns.MaLuotkham));
            //    DataSet dskiemtra = SPs.KcbNgoaitruKiemtraCapphatthuoc(presId, maLuotkham).GetDataSet();
            //    if (dskiemtra.Tables[1].Rows.Count > 0)
            //    {

            //        DataTable v_dtData = _kcbKedonthuoc.LaythongtinDonthuoc_In(Utility.Int32Dbnull(presId));
            //        Utility.AddColumToDataTable(ref v_dtData, "trangthai_indon", typeof(string));
            //        Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            //        Utility.CreateBarcodeData(ref v_dtData, presId.ToString(CultureInfo.InvariantCulture));
            //        string icdName = "";
            //        string icdCode = "";
            //        if (v_dtData != null && v_dtData.Rows.Count > 0)
            //            GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
            //                        Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref icdName, ref icdCode);

            //        if (v_dtData != null)
            //        {
            //            foreach (DataRow drv in v_dtData.Rows)
            //            {
            //                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == "" ? icdName : Utility.sDbnull(drv["chan_doan"]) + ";" + icdName;
            //                drv["ma_icd"] = icdCode;
            //                drv["trangthai_indon"] = "Đơn thuốc đã cấp phát";
            //            }
            //            v_dtData.AcceptChanges();
            //            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            //            Utility.UpdateLogotoDatatable(ref v_dtData);
            //            string khoGiay = "A5";
            //            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) khoGiay = "A4";
            //            ReportDocument reportDocument;
            //            string tieude = "", reportname = "", reportCode = "";
            //            switch (khoGiay)
            //            {
            //                case "A5":
            //                    reportCode = "thamkham_InDonthuocA5";
            //                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
            //                    break;
            //                case "A4":
            //                    reportCode = "thamkham_InDonthuocA4";
            //                    reportDocument = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref reportname);
            //                    break;
            //                default:
            //                    reportCode = "thamkham_InDonthuocA5";
            //                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
            //                    break;
            //            }
            //            if (reportDocument == null) return;
            //            Utility.WaitNow(this);
            //            ReportDocument crpt = reportDocument;
            //            var objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
            //            try
            //            {
            //                objForm.mv_sReportFileName = Path.GetFileName(reportname);
            //                objForm.mv_sReportCode = reportCode;
            //                crpt.SetDataSource(v_dtData);
            //                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
            //                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
            //                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
            //                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
            //                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
            //                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
            //                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
            //                objForm.crptViewer.ReportSource = crpt;
            //                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
            //                    PropertyLib._MayInProperties.PreviewInDonthuoc))
            //                {
            //                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
            //                    objForm.ShowDialog();
            //                }
            //                else
            //                {
            //                    objForm.addTrinhKy_OnFormLoad();
            //                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
            //                    crpt.PrintToPrinter(1, false, 0, 0);
            //                }
            //                Utility.DefaultNow(this);
            //            }
            //            catch (Exception ex)
            //            {
            //                Utility.DefaultNow(this);
            //                Utility.ShowMsg("Lỗi: "+ ex.Message);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Utility.ShowMsg("Đơn thuốc chưa cấp phát! \n Nên bạn không thể in được đơn thuốc");
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Utility.ShowMsg("Lỗi:"+ exception.Message);
            //}
            

        }

        private void cmdcauhinh_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties(PropertyLib._MayInProperties);
                frm.ShowDialog();
                CauHinh();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi" + exception.Message);
            }
        }
        
        private void cmdThemmoi_Click(object sender, EventArgs e)
        {
            InsertPatient();
        }
        KcbDanhsachBenhnhan objBenhnhan;
        KcbLuotkham objLuotkham;
        /// <summary>
        /// Insert dữ liệu khi thêm mới hoàn toàn
        /// </summary>hàm chen du lieu moi tin day, benhnhan kham benh moi tinh
        private KcbDanhsachBenhnhan TaoBenhnhan()
        {

                KcbDanhsachBenhnhan BNVanglai_Moi = new KcbDanhsachBenhnhan();
            BNVanglai_Moi.IsNew = true;
            BNVanglai_Moi.TenBenhnhan = "Bệnh nhân vãng lai.";
            BNVanglai_Moi.DiaChi = "";
            BNVanglai_Moi.KieuBenhnhan = 1;//0= đăng kí tiếp đón;1= bệnh nhân vãng lai tại quầy thuốc
            BNVanglai_Moi.NgayTao = globalVariables.SysDate;
            BNVanglai_Moi.NguoiTao = globalVariables.UserName;
            BNVanglai_Moi.NguonGoc = "QUAYTHUOC";
            BNVanglai_Moi.CoQuan = string.Empty;
            BNVanglai_Moi.GioiTinh = "KXĐ";
            BNVanglai_Moi.IdGioitinh = 2;
            BNVanglai_Moi.NamSinh = 2000;
            BNVanglai_Moi.DanToc = "01";
            BNVanglai_Moi.NguoiTiepdon = globalVariables.UserName;
            BNVanglai_Moi.NgayTiepdon = DateTime.Now;
            BNVanglai_Moi.NguoiTao = globalVariables.UserName;
            BNVanglai_Moi.IpMaytao = globalVariables.gv_strIPAddress;
            BNVanglai_Moi.TenMaytao = globalVariables.gv_strComputerName;
            return BNVanglai_Moi;
        }

        /// <summary>
        /// hàm thực hiện việc khwoir tạo thoog tin PatietnExam
        /// </summary>
        /// <returns></returns>
        private KcbLuotkham TaoLuotkham()
        {
            try
            {

                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("ID_DOITUONG_KCB_QUAYTHUOC", "1", true)));
                objLuotkham = new KcbLuotkham();
                //Bỏ đi do đã sinh theo cơ chế bảng danh mục mã lượt khám. Nếu ko sẽ mất mã lượt khám hiện thời.
                // txtMaLankham.Text = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM();
                objLuotkham.IsNew = true;


                objLuotkham.KieuKham = "QT";
                objLuotkham.NhomBenhnhan = objLuotkham.KieuKham;
                objLuotkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                objLuotkham.Noitru = 0;
                objLuotkham.IdDoituongKcb = objectType.IdDoituongKcb;
                objLuotkham.IdLoaidoituongKcb = objectType.IdLoaidoituongKcb;
                objLuotkham.Locked = 0;
                objLuotkham.HienthiBaocao = 1;
                objLuotkham.TrangthaiCapcuu = 0;
                objLuotkham.IdKhoatiepnhan = globalVariables.idKhoatheoMay;
                objLuotkham.NguoiTao = globalVariables.UserName;
                objLuotkham.NgayTao = globalVariables.SysDate;
                objLuotkham.Cmt = "";
                objLuotkham.DiaChi = "";
                objLuotkham.ThongtinNguongt = "-1";
                objLuotkham.CachTao = 0;
                objLuotkham.SoBenhAn = "";
                objLuotkham.Email = "";
                objLuotkham.NoiGioithieu = "";

                objLuotkham.Tuoi = 20;
                objLuotkham.LoaiTuoi = 0;


                objLuotkham.GiayBhyt = 0;
                objLuotkham.MadtuongSinhsong = "";
                objLuotkham.MaKcbbd = "";
                objLuotkham.NoiDongtrusoKcbbd = "";
                objLuotkham.MaNoicapBhyt = "";
                objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
                objLuotkham.MatheBhyt = "";
                objLuotkham.MaDoituongBhyt = "";
                objLuotkham.MaQuyenloi = -1;
                objLuotkham.DungTuyen = 0;
                objLuotkham.NgayketthucBhyt = null;
                objLuotkham.NgaybatdauBhyt = null;
                objLuotkham.NgayDu5nam = null;
                objLuotkham.NoicapBhyt = "";
                objLuotkham.DiachiBhyt = "";

                objLuotkham.PtramBhytGoc = 0;
                objLuotkham.PtramBhyt = 0;
                //chkTraiTuyen.Visible ?Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0):(objLuotkham.DungTuyen == 0 ? 0 : Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0));
                objLuotkham.SolanKham = 1;
                objLuotkham.TrieuChung = "";
                objLuotkham.TrangthaiNgoaitru = 0;
                objLuotkham.TrangthaiNoitru = 0;
                objLuotkham.NoiGioithieu = "";
                objLuotkham.ChiphiGioithieu = 0;

                objLuotkham.LastActionName = action.Add.ToString();

                if (objectType != null)
                {
                    objLuotkham.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                }

                objLuotkham.NgayTiepdon = DateTime.Now;
                objLuotkham.NguoiTiepdon = globalVariables.UserName;

                objLuotkham.IpMaytao = globalVariables.gv_strIPAddress;
                objLuotkham.TenMaytao = globalVariables.gv_strComputerName;

                return objLuotkham;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi tạo thông tin lượt khám", ex);
                return null;
            }

        }
        KCB_DANGKY _kcbDangky = new KCB_DANGKY();
        private void InsertPatient()
        {
            KcbDanhsachBenhnhan objBN = TaoBenhnhan();
            KcbLuotkham objLK = TaoLuotkham();
            string ma_luotkham = "";
            Int64 id_benhnhan = 0;
            ActionResult actionResult = _kcbDangky.ThemmoiBenhnhanTaiQuay(objBN, objLK, ref id_benhnhan, ref ma_luotkham);
            if (actionResult == ActionResult.Success)
            {
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(id_benhnhan);
                objLuotkham = Utility.getKcbLuotkham(id_benhnhan, ma_luotkham);
                if (objBenhnhan != null && objLuotkham != null)
                {
                    ThemMoiDonThuoc();
                }
            }


        }
        private bool Kiemtratrangthai_donthuoc()
        {
            var _item =
                new Select().From(KcbDonthuocChitiet.Schema)
                .Where(KcbDonthuocChitiet.IdDonthuocColumn).IsEqualTo(Pres_ID)
                .AndExpression(KcbDonthuoc.Columns.TrangThai).IsEqualTo(1).Or(KcbDonthuoc.Columns.TrangthaiThanhtoan).IsEqualTo(1).CloseExpression().ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }
        private void UpdateDonThuoc()
        {
            try
            {

                if (objLuotkham != null)
                {
                   
                    var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                        .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                        .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                        .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                    if (v_collect.Count > 0)
                    {
                        Utility.ShowMsg(
                            "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp thuốc để hủy xác nhận đơn thuốc tại kho thuốc");
                        return;
                    }
                    KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                    if (objPrescription != null)
                    {
                        var frm = new frm_KCB_KE_DONTHUOC_TAIQUAY("THUOC");
                        frm.em_Action = action.Update;
                        frm._MabenhChinh = "";
                        frm._Chandoan = "";
                        frm.DtIcd = null;
                        frm.dt_ICD_PHU = null;
                        frm.noitru = 0;
                        frm.objLuotkham = objLuotkham;
                        frm.objBenhnhan = objBenhnhan;
                        frm.id_kham = -1;
                        frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                        frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                        frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                        frm.txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                        frm.txtNgheNghiep.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                        frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                        frm.dtNgayKhamLai.MinDate = DateTime.Now;
                        frm._ngayhenkhamlai = DateTime.Now.ToString("dd/MM/yyyy");

                        frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            grdPres_SelectionChanged(grdPres, new EventArgs());
                        }
                        frm.Dispose();
                        frm = null;
                        GC.Collect();
                    }
                }

            }
            catch
            {
            }
            finally
            {
                ModifyCommand();
            }
        }
      
        private void ThemMoiDonThuoc()
        {
            try
            {
               
                // KeDonThuocTheoDoiTuong();
                var frm = new frm_KCB_KE_DONTHUOC_TAIQUAY("THUOC");
                frm.em_Action = action.Insert;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm._MabenhChinh ="";
                frm._Chandoan = "";
                frm.DtIcd = null ;
                frm.donthuoctaiquay = 1;
                frm.dt_ICD_PHU = null;
                frm.id_kham = -1;
                frm.dtpBOD.Value = new DateTime((int)objBenhnhan.NamSinh, 1, 1);
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                frm.txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                frm.txtNgheNghiep.Text = Utility.sDbnull(objBenhnhan.NgheNghiep, "");
                frm.cboPatientSex.SelectedIndex = 0;// Utility.GetSelectedIndex(cboPatientSex, objBenhnhan.GioiTinh);
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = DateTime.Now;
                frm._ngayhenkhamlai = DateTime.Now.ToString("dd/MM/yyyy");
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();

                if (!frm.m_blnCancel)
                {
                    long intIdDonthuoc = frm.mv_intIdDonthuoc;
                    if (intIdDonthuoc > 0)
                    {
                        DataTable dtTemp =
                         SPs.ThuocTimkiemdonthuocCapphatngoaitru(intIdDonthuoc, txtPID.Text,
                                                                Utility.sDbnull(txtTenBN.Text), "ALL",
                                                                chkByDate.Checked ? dtFromdate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                                chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",-1,
                                                                 Utility.Int32Dbnull(cboKho.SelectedValue), Utility.ByteDbnull(cboKieudonthuoc.SelectedValue, 100), kieuthuoc_vt,trangthai_capcuu,100).GetDataSet().Tables[0];
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            mv_dtDonthuoc.ImportRow(dr);
                        }
                        Utility.GotoNewRowJanus(grdPres, KcbDonthuoc.Columns.IdDonthuoc, intIdDonthuoc.ToString());
                        grdPres_SelectionChanged(grdPres, new EventArgs());
                    }
                }
                frm.Dispose();
                frm = null;
                GC.Collect();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                ModifyCommand();
            }
        }

      
       
        private void cmdThanhtoan_Click(object sender, EventArgs e)
        {
            doPayment();
        }
        #region Phục vụ việc thanh toán
        decimal _chuathanhtoan = 0m;
        bool blnJustPayment = false;
        private DataTable m_dtPayment, m_dtPhieuChi = new DataTable();
        public decimal TongtienCk = 0m;
        public decimal TongtienCkHoadon = 0m;
        public decimal TongtienCkChitiet = 0m;
        public string MaLdoCk = "";
        bool ttoan_dthuoc = false;
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        void doPayment()
        {
            try
            {
                Utility.EnableButton(cmdThanhtoan, false);
                if (blnJustPayment) return;
                blnJustPayment = true;
                if (!IsValidata()) return;
                if (!PayCheckDate(dtNgayPhatThuoc.Value)) return;
                PerformAction();
                blnJustPayment = false;
                chkLayHoadon.Checked = false;
            }
            catch
            {
                blnJustPayment = false;
            }
            finally
            {
                Utility.EnableButton(cmdThanhtoan, true);
                ModifyCommand();
                blnJustPayment = false;
            }
        }
        private bool INPHIEU_CLICK = false;
        private void PerformAction()
        {
            try
            {
                if (objLuotkham != null)
                {
                    if (INPHIEU_CLICK)
                    {
                        goto INPHIEU;
                    }
                    if (chkThanhtoan.Checked)
                        if (!Utility.AcceptQuestion("Bạn có muốn thanh toán cho đơn thuốc đang chọn không", "Thông báo thanh toán", true))
                        {
                            return;
                        }

                    //Nếu thanh toán khi in phôi thì không cần hỏi
                INPHIEU:
                    bool IN_HOADON = false;
                    string ErrMsg = "";
                    long IdHdonLog = -1;
                    IN_HOADON = Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0) > 0;
                    if (IN_HOADON)
                    {
                        //if (chkLayHoadon.Checked)//nếu lấy hóa đơn đỏ mới kiểm tra
                        //{
                        //    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1")//Chỉ khi dùng hóa đơn đỏ mới kiểm tra tiếp.
                        //    {
                        //        if (grdHoaDonCapPhat.RowCount <= 0)
                        //        {
                        //            uiTabHoadon_chiphi.SelectedTab = tabpageHoaDon;
                        //            Utility.ShowMsg("Bạn cần khai báo quyển hóa đơn đỏ trước khi sử dụng tính năng thanh toán với hóa đơn đỏ");
                        //            return;
                        //        }
                        //        if (!Utility.isValidGrid(grdHoaDonCapPhat))
                        //        {
                        //            uiTabHoadon_chiphi.SelectedTab = tabpageHoaDon;
                        //            Utility.ShowMsg("Mời bạn chọn quyển hóa đơn thanh toán");
                        //            return;
                        //        }
                        //        if (!checkSerie(ref IdHdonLog))
                        //        {
                        //            return;
                        //        }
                        //    }
                        //}
                    }

                    List<KcbThanhtoanChitiet> lstItems = Taodulieuthanhtoanchitiet(ref ErrMsg);
                    if (Utility.DoTrim(ErrMsg).Length > 0)
                    {
                        Utility.ShowMsg(ErrMsg);
                        return;
                    }
                    if (lstItems == null)
                    {
                        Utility.ShowMsg("Lỗi khi tạo dữ liệu thanh toán chi tiết. Liên hệ đơn vị cung cấp phần mềm để được hỗ trợ\n" + ErrMsg);
                        return;
                    }
                    TongtienCkChitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                    bool bo_ckchitiet = true;
                    string ma_uudai = "";
                    if (chkChietkhauthem.Checked || TongtienCkChitiet > 0)
                    {
                        frm_ChietkhauTrenHoadon chietkhauTrenHoadon = new frm_ChietkhauTrenHoadon();
                        chietkhauTrenHoadon.TongCKChitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                        chietkhauTrenHoadon.TongtienBN = Utility.DecimaltoDbnull(txtSoTienCanNop.Text) + Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                        chietkhauTrenHoadon.ShowDialog();
                        if (!chietkhauTrenHoadon.m_blnCancel)
                        {
                            ma_uudai = chietkhauTrenHoadon.autoUudai.myCode;
                            bo_ckchitiet = chietkhauTrenHoadon.chkAll.Checked;
                            if (chietkhauTrenHoadon.chkBoChitiet.Checked)
                            {
                                TongtienCkChitiet = 0;
                                HuyCkChitiettruockhithanhtoan();
                            }
                            TongtienCk = chietkhauTrenHoadon.TongtienCK;
                            TongtienCkHoadon = chietkhauTrenHoadon.TongCKHoadon;
                            MaLdoCk = chietkhauTrenHoadon.ma_ldoCk;
                        }
                        else
                        {
                            if (TongtienCkChitiet > 0)
                            {
                                Utility.ShowMsg("Bạn vừa thực hiện hủy thao tác nhập thông tin chiết khấu. Đồng thời hủy thanh toán(Do bạn không nhập lý do chiết khấu). Mời bạn bấm lại nút thanh toán để thực hiện lại");
                                return;
                            }
                            else
                            {
                                if (!Utility.AcceptQuestion("Bạn vừa thực hiện hủy thao tác nhập thông tin chiết khấu. Bạn có muốn tiếp tục thanh toán không có chiết khấu hay không?", "Xác nhận chiết khấu", true))
                                {
                                    return;
                                }
                            }
                        }
                    }
                    decimal ttbnChitrathucsu = 0;
                    ErrMsg = "";
                    long v_Payment_ID=-1;
                    KcbThanhtoan v_objPayment = TaophieuThanhtoan();
                    List<KcbChietkhau> lstChietkhau = new List<KcbChietkhau>();
                    ActionResult actionResult = _THANHTOAN.ThanhtoanThuoctaiquay_V2(v_objPayment, objLuotkham,
                        lstItems, lstChietkhau, ref v_Payment_ID, IdHdonLog,
                        chkLayHoadon.Checked &&
                        THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1", bo_ckchitiet, ma_uudai,
                        ref ttbnChitrathucsu,Utility.Int32Dbnull(cboKho.SelectedValue), ref ErrMsg);
                    IN_HOADON = ttbnChitrathucsu > 0;
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            //Utility.Log(this.Name, globalVariables.UserName, string.Format("Thanh toán đơn thuốc tại quầy thành công. Nhấn OK để kết thúc ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, v_objPayment.TongTien.ToString()), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                            GetChiPhiDaThanhToan();
                            LaydanhsachLichsuthanhtoan_phieuchi();
                            Capnhattin_ttoan(Pres_ID, 1, v_objPayment.NgayThanhtoan);
                            GetDataPresDetail();
                            Utility.GotoNewRowJanus(grdPayment, KcbThanhtoan.Columns.IdThanhtoan, v_Payment_ID.ToString());
                            if (v_Payment_ID <= 0)
                            {
                                grdPayment.MoveFirst();
                            }
                            //Kiểm tra nếu hình thức thanh toán phân bổ thì hiển thị chức năng phân bổ
                            List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                            //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                            if (lstPhanbo.Contains(txtPttt.myCode))
                            {
                                Phanbo();
                            }
                            //Tạm rem phần hóa đơn đỏ lại
                            if (IN_HOADON && PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan)
                            {
                                long id_donthuoc = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1);
                                int kcbThanhtoanKieuinhoadon = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUINHOADONTUDONG_SAUKHITHANHTOAN", "1", false));
                                if (kcbThanhtoanKieuinhoadon == 1 || kcbThanhtoanKieuinhoadon == 3)
                                    InHoadon();
                                if (kcbThanhtoanKieuinhoadon == 2 || kcbThanhtoanKieuinhoadon == 3)
                                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, v_Payment_ID, id_donthuoc, objLuotkham, 0);
                            }
                           
                            if (PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhithanhtoan)
                            {
                                ShowPaymentDetail(v_Payment_ID);
                            }
                            
                                SetSumTotalProperties();
                                if (chkRestoreDefaultPTTT.Checked)
                                    RestoredefaultPTTT();
                          
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                        case ActionResult.Cancel:
                            Utility.ShowMsg(ErrMsg);
                            break;
                    }
                    IN_HOADON = false;
                    INPHIEU_CLICK = false;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                TongtienCk = 0m;
                TongtienCkChitiet = 0m;
                TongtienCkHoadon = 0m;
                MaLdoCk = "";
                ModifyCommand();
                GC.Collect();
            }
        }
        void InHoadon()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                decimal TONG_TIEN = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells["BN_CT"].Value, -1);
                ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(_Payment_ID);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in hóa đơn", "Thông báo lỗi", MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
                log.Trace(ex.Message);
            }
        }
        void InPhieuchi()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdPhieuChi.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                decimal TONG_TIEN = Utility.Int32Dbnull(grdPhieuChi.CurrentRow.Cells["BN_CT"].Value, -1);
                ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(_Payment_ID);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in phiếu chi", "Thông báo lỗi", MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void ShowPaymentDetail(long v_Payment_ID)
        {
            if (objLuotkham != null)
            {
                frm_HuyThanhtoan frm = new frm_HuyThanhtoan(lstIdLoaiTtoan);
                frm.objLuotkham = objLuotkham;
                frm.v_Payment_Id = v_Payment_ID;
                frm.Chuathanhtoan = _chuathanhtoan;
                frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                frm.TotalPayment = grdPayment.GetDataRows().Length;
                frm.ShowCancel = false;
                frm.ShowDialog();
            }
        }
        private KcbThanhtoan TaophieuThanhtoan()
        {
            KcbThanhtoan objPayment = new KcbThanhtoan();
            objPayment.IdThanhtoan = -1;
            objPayment.MaLuotkham = objLuotkham.MaLuotkham;
            objPayment.IdBenhnhan = objLuotkham.IdBenhnhan;
            objPayment.NgayThanhtoan = dtPaymentDate.Value;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.KieuThanhtoan = 0;//0=Thanh toán thường;1= trả lại tiền;2= thanh toán bỏ viện
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.NoiTru = 0;
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.TtoanThuoc = true;//0= thanh toán các loại dịch vụ;1= thanh toán đơn thuốc tại quầy
            objPayment.NguoiIn = string.Empty;
            objPayment.MaPttt = txtPttt.myCode;
            objPayment.MaNganhang = autoNganhang.Enabled ? autoNganhang.myCode : "-1";
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.TongTien = Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0);

            //2 mục này được tính lại ở Business
            objPayment.BnhanChitra = -1;
            objPayment.BhytChitra = -1;
            objPayment.TileChietkhau = 0;
            objPayment.KieuChietkhau = "T";
            objPayment.TongtienChietkhau = TongtienCk;
            objPayment.TongtienChietkhauChitiet = TongtienCkChitiet;
            objPayment.TongtienChietkhauHoadon = TongtienCkHoadon;
            

            objPayment.MaLydoChietkhau = MaLdoCk;
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            objPayment.IpMaytao = globalVariables.gv_strIPAddress;
            objPayment.TenMaytao = globalVariables.gv_strComputerName;
            return objPayment;
        }
        void HuyCkChitiettruockhithanhtoan()
        {
            try
            {

                foreach (GridEXRow _row in grdChitietDonthuoc.GetDataRows())
                {
                    if (Utility.Int64Dbnull(_row.Cells["trangthai_thanhtoan"].Value, 1) == 0)//Chỉ reset các mục chưa thanh toán
                    {
                        _row.BeginEdit();
                        _row.Cells["tile_chietkhau"].Value = 0;
                        _row.Cells["tien_chietkhau"].Value = 0;
                        _row.Cells["ck_nguongt"].Value = 0;
                        _row.EndEdit();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
                SetSumTotalProperties();
                //TinhToanSoTienPhaithu();
            }
        }
        private List<KcbThanhtoanChitiet> Taodulieuthanhtoanchitiet(ref string errMsg)
        {
            try
            {
                DataTable dtDataCheck = new DataTable();
                byte ErrType = 0;//0= xóa dịch vụ sau khi tnv chọn người bệnh-->có trong bảng tt chi tiết, ko có trong các bảng dịch vụ khám,thuốc/vtth,cls;1= đã bị người khác thanh toán;
                List<KcbThanhtoanChitiet> lstItems = new List<KcbThanhtoanChitiet>();
                foreach (GridEXRow row in grdChitietDonthuoc.GetCheckedRows())
                {
                    KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                    newItem.IdThanhtoan = -1;
                    newItem.IdChitiet = -1;
                    newItem.TinhChiphi = 1;
                    if (objLuotkham.PtramBhyt != null) newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                    if (objLuotkham.PtramBhytGoc != null) newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                    newItem.SoLuong = Utility.DecimaltoDbnull(row.Cells["sluong_sua"].Value, 0);
                    if (newItem.SoLuong <= 0) newItem.SoLuong = Utility.DecimaltoDbnull(row.Cells["so_luong"].Value, 0);
                    //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                    newItem.BnhanChitra = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.BnhanChitra].Value, 0);
                    newItem.BhytChitra = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.BhytChitra].Value, 0);
                    newItem.DonGia = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.DonGia].Value, 0);
                    newItem.GiaGoc = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.GiaGoc].Value, 0);
                    newItem.TyleTt = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TyleTt].Value, 0);
                    newItem.PhuThu = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.PhuThu].Value, 0);
                    newItem.TinhChkhau = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TinhChkhau].Value, 0);
                    newItem.CkNguongt = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.CkNguongt].Value, 0);
                    newItem.TuTuc = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TuTuc].Value, 0);
                    newItem.IdPhieu = Utility.Int32Dbnull(row.Cells["id_phieu"].Value);
                    newItem.IdKham = Utility.Int32Dbnull(row.Cells["Id_Kham"].Value);
                    newItem.IdPhieuChitiet = Utility.Int32Dbnull(row.Cells["Id_Phieu_Chitiet"].Value, -1);
                    newItem.IdDichvu = Utility.Int16Dbnull(row.Cells["Id_dichvu"].Value, -1);
                    newItem.IdChitietdichvu = Utility.Int16Dbnull(row.Cells["Id_Chitietdichvu"].Value, -1);
                    newItem.TenChitietdichvu = Utility.sDbnull(row.Cells["Ten_Chitietdichvu"].Value, "Không xác định").Trim();
                    newItem.TenBhyt = Utility.sDbnull(row.Cells["ten_bhyt"].Value, "Không xác định").Trim();
                    newItem.DonviTinh = Utility.chuanhoachuoi(Utility.sDbnull(row.Cells["Ten_donvitinh"].Value, "Lượt"));
                    newItem.SttIn = Utility.Int16Dbnull(row.Cells["stt_in"].Value, 0);
                    newItem.IdKhoakcb = Utility.Int16Dbnull(row.Cells["id_khoakcb"].Value, -1);
                    newItem.IdPhongkham = Utility.Int16Dbnull(row.Cells["id_phongkham"].Value, -1);
                    newItem.IdBacsiChidinh = Utility.Int16Dbnull(row.Cells["id_bacsi"].Value, -1);
                    newItem.IdLoaithanhtoan = Utility.ByteDbnull(row.Cells["Id_Loaithanhtoan"].Value, -1);
                    newItem.IdLichsuDoituongKcb = Utility.Int64Dbnull(row.Cells[KcbThanhtoanChitiet.Columns.IdLichsuDoituongKcb].Value, -1);
                    newItem.MatheBhyt = Utility.sDbnull(row.Cells[KcbThanhtoanChitiet.Columns.MatheBhyt].Value, -1);
                    newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(Utility.Int32Dbnull(row.Cells["Id_Loaithanhtoan"].Value, -1));
                    newItem.TienChietkhau = Math.Round(Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TienChietkhau].Value, 0m), 3);
                    newItem.TileChietkhau = Math.Round(Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TileChietkhau].Value, 0m), 3);
                    newItem.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                    newItem.UserTao = Utility.sDbnull(row.Cells["user_tao"].Value, "UKN").Trim();
                    newItem.KieuChietkhau = "%";
                    newItem.IdThanhtoanhuy = -1;
                    newItem.TrangthaiHuy = 0;
                    newItem.TrangthaiBhyt = 0;
                    newItem.TrangthaiChuyen = 0;
                    newItem.NoiTru = 0;
                    newItem.NguonGoc = (byte)0;
                    newItem.NgayTao = globalVariables.SysDate;
                    newItem.NguoiTao = globalVariables.UserName;
                    lstItems.Add(newItem);
                    dtDataCheck = SPs.ThanhtoanKiemtratontaitruockhithanhtoan(newItem.IdPhieu, newItem.IdPhieuChitiet, newItem.IdLoaithanhtoan).GetDataSet().Tables[0];
                    if (dtDataCheck.Rows.Count <= 0)
                    {
                        ErrType = 0;
                        errMsg += newItem.TenChitietdichvu + "\n";
                        break;
                    }
                    else//Kiểm tra trạng thái thanh toán tránh việc thanh toán 2 lần(2 user cùng chọn và sau đó từng người bấm nút thanh toán)
                        if (dtDataCheck.Rows[0]["trangthai_thanhtoan"].ToString() == "1")
                        {
                            ErrType = 1;
                            errMsg += newItem.TenChitietdichvu + "\n";
                            break;
                        }
                }
                if (errMsg.Length > 0)
                    if (ErrType == 0)
                        errMsg = "Một số dịch vụ đang chọn thanh toán đã bị xóa/hủy bởi người khác. Vui lòng chọn lại người bệnh để lấy lại dữ liệu mới nhất. Kiểm tra các dịch vụ không tồn tại dưới đây:\n" + errMsg;
                    else if (ErrType == 1)
                        errMsg = "Một số dịch vụ đang chọn thanh toán đã được thanh toán bởi TNV khác(trong lúc bạn chọn và chưa bấm thanh toán). Vui lòng chọn lại người bệnh để lấy lại dữ liệu mới nhất. Kiểm tra các dịch vụ đã được thanh toán dưới đây:\n" + errMsg;
                return lstItems;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }
        private bool PayCheckDate(DateTime InputDate)
        {
            if (globalVariables.SysDate.Date != InputDate.Date && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOPHEPCHONGAYTHANHTOAN", "1", false) == "1")
            {
                frm_ChonngayThanhtoan frm = new frm_ChonngayThanhtoan();
                frm.pdt_InputDate = dtNgayPhatThuoc.Value;
                frm.ShowDialog();
                if (!frm.mv_blnCancel)
                {
                    dtPaymentDate.Value = frm.pdt_InputDate;
                    return true;
                }
                else
                    return false;
            }
            return true;
        }
        private bool IsValidata()
        {
            bool bCheckPayment = false;
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHOPHEP_0_DONG", true) == "0")
            {
                if (Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0) == 0)
                {
                    Utility.ShowMsg("Hệ thống chỉ cho phép thanh toán khi số tiền thu phải > 0. Mời bạn kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (grdChitietDonthuoc.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một dịch vụ chưa thanh toán để thực hiện thanh toán", "Thông báo", MessageBoxIcon.Warning);
                grdChitietDonthuoc.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdChitietDonthuoc.GetCheckedRows())
            {
                if (gridExRow.Cells["trangthai_thanhtoan"].Value.ToString() == "1")
                {
                    bCheckPayment = true;
                    break;
                }
            }
            if (bCheckPayment)
            {
                Utility.ShowMsg("Bạn chỉ được phép chọn các bản ghi chưa thực hiện thanh toán mới thanh toán được", "Thông báo", MessageBoxIcon.Warning);
                grdChitietDonthuoc.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdChitietDonthuoc.GetCheckedRows())
            {
                if (gridExRow.Cells["trangthai_huy"].Value.ToString() == "1")
                {
                    bCheckPayment = true;
                    break;
                }
            }
            if (bCheckPayment)
            {
                Utility.ShowMsg("Bạn phải bỏ chọn bản ghi bị hủy trước khi thanh toán.Vui lòng kiểm tra lại", "Thông báo",
                    MessageBoxIcon.Warning);
                grdChitietDonthuoc.Focus();
                return false;
            }
            if (txtPttt.myCode == "-1")
            {
                Utility.ShowMsg("Bạn phải chọn phương thức thanh toán trước khi thực hiện thanh toán");
                txtPttt.Focus();
                return false;
            }
            if (!isValidPttt_Nganhang())
                return false;
          
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin bệnh nhân cần thanh toán. Đề nghị liên hệ IT để được giải quyết");
                return false;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && Utility.DoTrim(objLuotkham.MatheBhyt) == "")
            {
                Utility.ShowMsg("Bệnh nhân BHYT cần nhập mã thẻ BHYT trước khi thanh toán");
                return false;
            }
           
            //if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
            //{
            //    Utility.ShowMsg("Bệnh nhân này đã phát sinh dịch vụ nội trú(Nộp tiền tạm ứng, Lập phiếu điều trị...) nên hệ thống không cho phép thanh toán ngoại trú nữa");
            //    return false;
            //}
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BHYT_NHIEULAN", "0", false) == "0")
            {
                KcbThanhtoan objthanhtoan = new Select().From(KcbThanhtoan.Schema)
                    .Where(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<KcbThanhtoan>();
                if (objthanhtoan != null)
                {
                    Utility.ShowMsg(string.Format("Bệnh nhân {0} thuộc đối tượng BHYT đã được thanh toán ít nhất một lần.\nHệ thống đang cấu hình không cho phép đối tượng BHYT thanh toán nhiều lần\nDo vậy bạn cần hủy thanh toán của các lần thanh toán trước để thực hiện một lần thanh toán duy nhất cho đối tượng này", objBenhnhan.TenBenhnhan));
                    return false;
                }
            }
            //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NGOAITRU_TUDONGHOANUNG_KHITHANHTOANNGOAITRU", "0", false) == "0")
            //{
            //    NoitruTamung objTamung = new Select().From(NoitruTamung.Schema).Where(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //        .And(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //        .And(NoitruTamung.Columns.TrangThai).IsEqualTo(0)
            //        .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(0)
            //        .And(NoitruTamung.Columns.Noitru).IsEqualTo(0)
            //        .ExecuteSingle<NoitruTamung>();
            //    if (objTamung != null)
            //    {
            //        Utility.ShowMsg("Bạn cần thực hiện thao tác hoàn ứng tiền cho bệnh nhân trước khi thực hiện thanh toán ngoại trú");
            //        return false;
            //    }
            //}

            return true;
        }
        bool isValidPttt_Nganhang()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            if (autoNganhang.Enabled && autoNganhang.myCode == "-1")
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", txtPttt.Text));
                autoNganhang.Focus();
                return false;
            }
            return true;
        }
        string lstIdLoaiTtoan = "3,5";
        private void LaydanhsachLichsuthanhtoan_phieuchi()
        {
            try
            {
                m_dtPayment = null;
                m_dtPhieuChi = null;
                DataTable dtData =
                       _THANHTOAN.LaythongtinCacLanthanhtoan(objLuotkham.MaLuotkham,
                           objLuotkham.IdBenhnhan, 0, 0, 1,
                           globalVariables.MA_KHOA_THIEN, lstIdLoaiTtoan,-1);
                DataRow[] arrDR = dtData.Select("Kieu_ThanhToan = 0");
                if (arrDR.Length > 0) m_dtPayment = arrDR.CopyToDataTable();
                else
                    m_dtPayment = dtData.Clone();
                arrDR = dtData.Select("Kieu_ThanhToan = 1");
                if (arrDR.Length > 0) m_dtPhieuChi = arrDR.CopyToDataTable();
                else
                    m_dtPhieuChi = dtData.Clone();


                grdPayment.Visible = m_dtPayment.Rows.Count > 0;
                Utility.SetDataSourceForDataGridEx(grdPayment, dtData, false, true, "1=1", "");
                Utility.SetDataSourceForDataGridEx(grdPhieuChi, m_dtPhieuChi, false, true, "1=1", "");
                if (m_dtPayment.Rows.Count <= 0)
                {
                    txtsotiendathu.Text = "0";
                    txtDachietkhau.Text = "0";
                }
                else
                {
                    txtDachietkhau.Text = m_dtPayment.Compute("SUM(tongtien_chietkhau)", "1=1").ToString();
                    txtsotiendathu.Text = (Utility.DecimaltoDbnull(m_dtPayment.Compute("SUM(TT_BN)", "1=1"), 0) - Utility.DecimaltoDbnull(txtDachietkhau.Text, 0)).ToString();
                }
            }
            catch (Exception exception)
            {
                Utility.CatchException("Lỗi khi lấy thông tin lịch sử thanh toán của bệnh nhân", exception);
                // 
            }
        }
        private void LaydanhsachLichsu_phieutralaithuoc()
        {
            try
            {
                DataTable dtPhieutralai = new Select().From(ThuocLichsuTralaithuoctaiquayPhieu.Schema)
                    .Where(ThuocLichsuTralaithuoctaiquayPhieu.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(ThuocLichsuTralaithuoctaiquayPhieu.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx(grdLichsutrathuoc, dtPhieutralai, false, true, "1=1", "trang_thai,ngay_tao,id_tralaithuoc");
               
            }
            catch (Exception exception)
            {
                Utility.CatchException("Lỗi khi lấy thông tin lịch sử thanh toán của bệnh nhân", exception);
                // 
            }
        }
        private GridEXColumn getGridExColumn(GridEX gridEx, string colName)
        {
            return gridEx.RootTable.Columns[colName];
        }
        private void SetSumTotalProperties()
        {
            try
            {
                if (objLuotkham == null)
                {
                    string v_maluotkham = Utility.sDbnull(Utility.getValueOfGridCell(grdPres, KcbLuotkham.Columns.MaLuotkham));
                    long v_IdBenhnhan = Utility.Int64Dbnull(Utility.getValueOfGridCell(grdPres, KcbLuotkham.Columns.IdBenhnhan));
                    objLuotkham = Utility.getKcbLuotkham(v_IdBenhnhan, v_maluotkham);
                }
                string errMsg = "";
                decimal newBhyt = Utility.DecimaltoDbnull(txtPtramBHChiTra.Text, 0);
                //_THANHTOAN.TinhlaitienBhyTtruocThanhtoan(m_dtChiPhiThanhtoan, TaophieuThanhtoan(), objLuotkham, Taodulieuthanhtoanchitiet(ref errMsg), ref newBhyt);
                txtPtramBHChiTra.Text = newBhyt.ToString();

                GridEXColumn gridExColumntrangthaithanhtoan = getGridExColumn(grdChitietDonthuoc, "trangthai_thanhtoan");
                GridEXColumn gridExColumn = getGridExColumn(grdChitietDonthuoc, "TT_KHONG_PHUTHU");
                GridEXColumn gridExColumnTutuc = getGridExColumn(grdChitietDonthuoc, "TT_BN_KHONG_TUTUC");
                GridEXColumn gridExColumnTt = getGridExColumn(grdChitietDonthuoc, "TT");
                GridEXColumn gridExColumnTtChietkhau = getGridExColumn(grdChitietDonthuoc, KcbThanhtoanChitiet.Columns.TienChietkhau);
                GridEXColumn gridExColumnBhyt = getGridExColumn(grdChitietDonthuoc, "TT_BHYT");
                GridEXColumn gridExColumnTtbn = getGridExColumn(grdChitietDonthuoc, "TT_BN");
                GridEXColumn gridExColumntutuc = getGridExColumn(grdChitietDonthuoc, "tu_tuc");
                GridEXColumn gridExColumntrangthaiHuy = getGridExColumn(grdChitietDonthuoc, "trangthai_huy");
                GridEXColumn gridExColumnPhuThu = getGridExColumn(grdChitietDonthuoc,
                    "TT_PHUTHU");
                var gridExFilterConditionKhongTutuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 0);
                var gridExFilterConditionTutuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 1);
                var gridExFilterChuathanhtoan =
                    new GridEXFilterCondition(gridExColumntrangthaithanhtoan, ConditionOperator.Equal, 0);
                var gridExFilterDathanhtoan =
                  new GridEXFilterCondition(gridExColumntrangthaithanhtoan, ConditionOperator.Equal, 1);
                var gridExFilterConditionTuTuc =
                   new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 1);

                var gridExFilterConditionKhongTuTuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 0);
                var gridExFilterConditiontrangthaiHuy =
                    new GridEXFilterCondition(gridExColumntrangthaiHuy, ConditionOperator.Equal, 0);
                var gridExFilterConditiontrangthaiHuyVaKhongtutuc =
                   new GridEXFilterCondition(gridExColumntrangthaiHuy, ConditionOperator.Equal, 0);
                gridExFilterConditiontrangthaiHuyVaKhongtutuc.AddCondition(gridExFilterConditionKhongTuTuc);
                GridEXColumn gridExColumnBnct = getGridExColumn(grdChitietDonthuoc,
                    "bnhan_chitra");


                decimal bnKhongtutuc = 0m;
                decimal tt = 0m;
                decimal TT_Chietkhau = 0m;
                decimal TT_KHONG_PHUTHU = 0m;
                decimal TT_BHYT = 0m;
                decimal TT_BN = 0m;
                decimal TT_BN_DaTT = 0m;
                _chuathanhtoan = 0m;

                //Tạm bỏ
                //decimal PtramBHYT = 0;
                //_THANHTOAN.LayThongPtramBHYT(TongChiphiBHYT, objLuotkham, ref PtramBHYT);
                decimal PhuThu = 0m;
                decimal TuTuc = 0m;
                foreach (DataRowView drv in m_dtChitietdonthuoc.DefaultView)
                {
                    if (Utility.Int32Dbnull(drv["tinh_chiphi"], 0) == 1 && Utility.Int32Dbnull(drv["trangthai_huy"], 0) == 0)
                    {
                        tt += Utility.DecimaltoDbnull(drv["TT"], 0);
                        if (Utility.sDbnull(drv["colCHON"], "1") == "1")
                        {
                            if (Utility.Int32Dbnull(drv["tu_tuc"], 0) == 0)
                                bnKhongtutuc += Utility.DecimaltoDbnull(drv["TT_BN_KHONG_TUTUC"], 0);
                            TT_Chietkhau += Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.TienChietkhau], 0);
                            TT_KHONG_PHUTHU += Utility.DecimaltoDbnull(drv["TT_KHONG_PHUTHU"], 0);
                            TT_BHYT += Utility.DecimaltoDbnull(drv["TT_BHYT"], 0);
                            TT_BN += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                            TT_BN_DaTT += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                            if (Utility.Int32Dbnull(drv["trangthai_thanhtoan"], 0) == 0) _chuathanhtoan += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                            PhuThu += Utility.DecimaltoDbnull(drv["TT_PHUTHU"], 0);
                            if (Utility.Int32Dbnull(drv["tu_tuc"], 0) == 1) TuTuc += Utility.DecimaltoDbnull(drv["TT_TUTUC"], 0);
                        }
                    }
                }


                txtTongChiPhi.Text = Utility.sDbnull(tt);
                TT_KHONG_PHUTHU -= TuTuc;
                txtTongtienDCT.Text = objLuotkham.MaDoituongKcb == "DV" ? "0" : Utility.sDbnull(TT_BHYT + bnKhongtutuc);
                // objLuotkham.MaDoituongKcb == "DV" ? "0" : Utility.sDbnull(TT_KHONG_PHUTHU);
                txtPhuThu.Text = Utility.sDbnull(PhuThu);
                if (Utility.DecimaltoDbnull(TuTuc) > 0)
                {
                    txtTuTuc.BackColor = Color.Red;
                }
                else
                {
                    txtTuTuc.BackColor = Color.Honeydew;
                }
                txtTuTuc.Text = Utility.sDbnull(TuTuc);
                //decimal BHCT = TongChiphiBHYT*PtramBHYT/100;
                txtBHCT.Text = Utility.sDbnull(TT_BHYT, "0");
                decimal bnct = bnKhongtutuc;
                txtBNCT.Text = Utility.sDbnull(bnct);
                decimal bnPhaiTra = bnct + Utility.DecimaltoDbnull(txtTuTuc.Text, 0) + Utility.DecimaltoDbnull(txtPhuThu.Text);
                txtBNPhaiTra.Text = Utility.sDbnull(TT_BN);
                TinhToanSoTienPhaithu();
                ThongtinTamung();
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        void ThongtinTamung()
        {
            SysSystemParameter _objLabel = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("THANHTOAN_THUATHIEU").ExecuteSingle<SysSystemParameter>();
            decimal tongTamung = 0;
            txtTongTU.Clear();
            DataTable _dtTamung = new KCB_THAMKHAM().NoitruTimkiemlichsuNoptientamung(objLuotkham.MaLuotkham,
                  Utility.Int32Dbnull(objLuotkham.IdBenhnhan, 0),
                  0, -1, (byte)(objLuotkham.TrangthaiNoitru > 0 ? 1 : 0));
            if (true)
            {
                if (_dtTamung != null)
                {
                    tongTamung = Utility.DecimaltoDbnull(_dtTamung.Compute("SUM(so_tien)", "trang_thai=0"), 0);
                    txtTongTU.Text = tongTamung.ToString();
                    if (Math.Abs(tongTamung) != 0)
                    {
                        decimal chenhlech = _chuathanhtoan - tongTamung;
                        if (chenhlech > 0)
                        {
                            lblThuathieu.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
                            txtThuathieu.Text = chenhlech.ToString();
                        }
                        else
                        {
                            lblThuathieu.Text = _objLabel == null ? @"Trả lại BN" : _objLabel.SValue.Split(';')[1];
                            txtThuathieu.Text = Math.Abs(chenhlech).ToString();
                        }
                    }
                }
            }
            else
            {
                lblTiennop.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
            }
            if (tongTamung == 0)
            {
                lblThuathieu.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
                txtThuathieu.Text = txtSoTienCanNop.Text;
            }
        }
        private void TinhToanSoTienPhaithu()
        {
            try
            {
                List<GridEXRow> query = (from thanhtoan in grdChitietDonthuoc.GetCheckedRows()
                                         where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                                               && Utility.Int32Dbnull(thanhtoan.Cells["trangthai_thanhtoan"].Value) == 0
                                         //&& Utility.Int32Dbnull(thanhtoan.Cells["trang_thai"].Value) == 0
                                         select thanhtoan).ToList();

                List<GridEXRow> query1 = (from thanhtoan in grdChitietDonthuoc.GetCheckedRows()
                                          where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                                          select thanhtoan).ToList();

                decimal thanhtien = query.Sum(c => Utility.DecimaltoDbnull(c.Cells["TT_BN"].Value));
                decimal Chietkhauchitiet = query1.Sum(c => Utility.DecimaltoDbnull(c.Cells["tien_chietkhau"].Value));
                txtSoTienCanNop.Text = Utility.sDbnull(thanhtien - Chietkhauchitiet);
                _chuathanhtoan = thanhtien - Chietkhauchitiet;
                txtTienChietkhau.Text = Utility.sDbnull(Chietkhauchitiet);
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void UpdateTuCheckKhiChuaThanhToan()
        {
            try
            {
                foreach (GridEXRow gridExRow in grdChitietDonthuoc.GetDataRows())
                {
                    if (gridExRow.RowType == RowType.Record)
                    {
                        gridExRow.IsChecked = Utility.Int32Dbnull(gridExRow.Cells["trangthai_thanhtoan"].Value, 0) == 0//Chưa thanh toán
                            && Utility.Int32Dbnull(gridExRow.Cells["trang_thai"].Value, 0) == 0//Chưa cấp phát
                                              && Utility.Int32Dbnull(gridExRow.Cells["trangthai_huy"].Value, 0) == 0;//Không hủy
                    }
                }
                grdChitietDonthuoc.UpdateData();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        #endregion

        private void cmdDuyetcapphat_Click(object sender, EventArgs e)
        {
            DuyetCapphatthuoc();
        }

        private void cmdCheck_Click(object sender, EventArgs e)
        {

            KiemTraTonthuoctrongdon();

           
        }
        void KiemTraTonthuoctrongdon()
        {
            try
            {
                if (cboKho.SelectedValue.ToString() == "-1")
                {
                    Utility.ShowMsg("Bạn cần chọn kho kê đơn");
                    cboKho.Focus();
                    return;
                }
                Int16 stockId = Utility.Int16Dbnull(m_dtChitietdonthuoc.Rows[0][KcbDonthuocChitiet.Columns.IdKho]);
                //Lấy từ CSDL cho chắc ăn thay vì lấy trên lưới danh sách
                DataTable dtChitietcapphat = SPs.ThuocLaydanhsachchitietthanhtoanchuacapphat(Pres_ID).GetDataSet().Tables[0];
                //if (!KiemtradonthuocOK(dtChitietcapphat)) return;
                Dictionary<long, string> lstID_Err = new Dictionary<long, string>();
                ActionResult ActionResult = new CapphatThuocKhoa().KiemtratonthuocNgoaitru(dtChitietcapphat, stockId, true, ref lstID_Err);
                switch (ActionResult)
                {
                    case ActionResult.Success:
                        Utility.ShowMsg("Thuốc trong kho còn đủ để cấp phát");
                        break;
                    case ActionResult.NotEnoughDrugInStock:
                        (from p in m_dtChitietdonthuoc.AsEnumerable() where lstID_Err.ContainsKey(Utility.Int64Dbnull(p["id_chitietdichvu"])) select p).ToList().ForEach(x => { x["isErr"] = 1; x["msg"] = lstID_Err[Utility.Int64Dbnull(x["id_thuoc"])]; });
                        m_dtChitietdonthuoc.AcceptChanges();
                        break;
                    case ActionResult.UNKNOW:
                        Utility.ShowMsg("Tồn tại thuốc trong đơn cấp phát đã bị xóa khỏi bảng danh mục thuốc. Mời bạn kiểm tra lại!");
                        return;

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void cmdPhanboHTTT_Click(object sender, EventArgs e)
        {
            if (!Utility.Coquyen("THANHTOAN_QUYEN_PHANBOPTTT"))
            {
                Utility.ShowMsg("Bạn không có quyền phân bổ PTTT(THANHTOAN_QUYEN_PHANBOPTTT). Yêu cầu quản trị hệ thống để được cấp quyền");
                return;
            }
            Phanbo();
        }
        void Phanbo()
        {
            if (!Utility.isValidGrid(grdPayment)) return;
            long v_Payment_ID = Utility.Int64Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
            string ma_pttt = Utility.sDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.MaPttt].Value, "TM");
            string ma_nganhang = Utility.sDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.MaNganhang].Value, "TM");
            frm_PhanbotientheoPTTT _PhanbotientheoPTTT = new frm_PhanbotientheoPTTT(v_Payment_ID, -1l, -1l, ma_pttt, ma_nganhang);
            _PhanbotientheoPTTT.objLuotkham = this.objLuotkham;
            _PhanbotientheoPTTT._OnChangePTTT += _PhanbotientheoPTTT__OnChangePTTT;
            _PhanbotientheoPTTT.ShowDialog();
        }
        void Phanbo(string ma_pttt)
        {
            if (!Utility.isValidGrid(grdPayment)) return;
            long v_Payment_ID = Utility.Int64Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
            string ma_nganhang = Utility.sDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.MaNganhang].Value, "TM");
            frm_PhanbotientheoPTTT _PhanbotientheoPTTT = new frm_PhanbotientheoPTTT(v_Payment_ID, -1l, -1l, ma_pttt, ma_nganhang);
            _PhanbotientheoPTTT.objLuotkham = this.objLuotkham;
            _PhanbotientheoPTTT._OnChangePTTT += _PhanbotientheoPTTT__OnChangePTTT;
            _PhanbotientheoPTTT.ShowDialog();
        }
        void _PhanbotientheoPTTT__OnChangePTTT(long id_thanhtoan, string ma_pttt, string ten_pttt, string ma_nganhang, string ten_nganhang)
        {
            try
            {
                DataRow dr = ((DataRowView)grdPayment.CurrentRow.DataRow).Row;

                dr["ma_pttt"] = ma_pttt;
                dr["ten_pttt"] = ten_pttt;
                dr["ma_nganhang"] = ma_nganhang;
                dr["ten_nganhang"] = ten_nganhang;

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        
        void Capnhattin_ttoan(long id_donthuoc, byte tthai_ttoan, DateTime? ngay_thanhtoan)
        {
            try
            {
                DataRow[] arrdr = mv_dtDonthuoc.Select(string.Format("{0}={1}", KcbDonthuoc.Columns.IdDonthuoc, id_donthuoc));
                if (arrdr.Length > 0)
                {
                    arrdr[0][KcbDonthuoc.Columns.TrangthaiThanhtoan] = tthai_ttoan;
                    //arrdr[0][KcbDonthuoc.Columns.NgayThanhtoan] = tdothai_ttoan == 0 ? DBNull.Value : ngay_thanhtoan;
                }
                m_dtChitietdonthuoc.Select("1=1").ToList<DataRow>().ForEach(c => c[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan] = tthai_ttoan);
                UpdateTuCheckKhiChuaThanhToan();
                SetSumTotalProperties();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                
            }
        }
        string ma_lydohuy = "";
        string lydo_huy = "";
        private void HuyThanhtoan()
        {
            try
            {
                ma_lydohuy = "";
                if (!Utility.isValidGrid(grdPayment)) return;
                if (!Utility.Coquyen("thanhtoan_huythanhtoan"))
                {
                    Utility.ShowMsg("Bạn không có quyền hủy thanh toán (thanhtoan_huythanhtoan). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                    return;
                }
                if (grdPayment.CurrentRow != null)
                {
                   
                    if (objLuotkham.TrangthaiNoitru >=
                        Utility.Int32Dbnull(
                            THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy thanh toán ngoại trú nữa");
                        return;
                    }
                    //if (objPhieuDct.TrangthaiXml > Utility.Int32Dbnull(1))
                    //{
                    //    Utility.ShowMsg("Bệnh nhân đã được gửi dữ liệu BHYT!");
                    //    return;
                    //}

                   long v_Payment_ID =
                        Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                    KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);

                    if (objPayment != null)
                    {
                        //Kiểm tra ngày hủy
                        int kcbThanhtoanSongayHuythanhtoan =
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
                        var chenhlech =
                            (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                        if (chenhlech > kcbThanhtoanSongayHuythanhtoan)
                        {
                            Utility.ShowMsg(
                               string.Format("Ngày thanh toán: {0}. Hệ thống không cho phép bạn hủy thanh toán khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objPayment.NgayThanhtoan.Date.ToString("dd/MM/yyyy"), kcbThanhtoanSongayHuythanhtoan.ToString()));
                            return;
                        }
                        if (Utility.Byte2Bool(objPayment.TrangthaiChot))
                        {
                            Utility.ShowMsg(
                                "Thanh toán đang chọn đã được chốt nên bạn không thể hủy thanh toán. Mời bạn xem lại!");
                            return;
                        }
                        
                        if (chkHienthidvuhuyTT.Checked)
                        {
                            var frm = new frm_HuyThanhtoan(lstIdLoaiTtoan);//Thuốc và vật tư tiêu hao
                            frm.objLuotkham = objLuotkham;
                            frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                            frm.Chuathanhtoan = _chuathanhtoan;
                            frm.TotalPayment = grdPayment.GetDataRows().Length;
                            frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                            frm.ShowCancel = true;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                GetDataPresDetail();//Không cần nữa do đã gọi lại hàm lấy thông tin chi tiết của đơn thuốc
                                LaydanhsachLichsuthanhtoan_phieuchi();
                                //Capnhattin_ttoan(Pres_ID, 0, null);
                               //Cập nhât lại thông tin thanh toán trên lưới danh sách đơn thuốc
                            }
                        }
                        else
                        {
                            if (chkHuythanhtoan.Checked)
                                if (
                                    !Utility.AcceptQuestion(
                                        string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}",
                                            objPayment.IdThanhtoan), "Thông báo", true))
                                {
                                    return;
                                }
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1",
                                    false) == "1")
                            {
                                var nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN",
                                    "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện",
                                    "Lý do hủy thanh toán",false);
                                nhaplydohuythanhtoan.ShowDialog();
                                if (nhaplydohuythanhtoan.m_blnCancel) return;
                                ma_lydohuy = nhaplydohuythanhtoan.ma;
                                lydo_huy = nhaplydohuythanhtoan.ten;
                            }
                            int idHdonLog =
                                Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value, -1);
                            bool huythanhtoanHuybienlai =
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
                            ActionResult actionResult = _THANHTOAN.HuyThanhtoan(objPayment, objLuotkham, lydo_huy,
                                idHdonLog, huythanhtoanHuybienlai);
                            switch (actionResult)
                            {
                                case ActionResult.Success:
                                    GetDataPresDetail();
                                    LaydanhsachLichsuthanhtoan_phieuchi();
                                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy thanh toán tiền cho bệnh nhân ID={0}, PID={1}, Tên={2}, sô tiền={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, objPayment.TongTien.ToString()), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                    //Capnhattin_ttoan(Pres_ID, 0, null);//Không cần nữa do đã gọi lại hàm lấy thông tin chi tiết của đơn thuốc
                                    Utility.Log(Name, globalVariables.UserName,
                                        string.Format(
                                            "Hủy thanh toán ngoại trú của bệnh nhân có mã lần khám {0} và mã bệnh nhân là: {1} bởi {2}",
                                            objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, globalVariables.UserName),
                                        newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                    break;
                                case ActionResult.ExistedRecord:
                                    break;
                                case ActionResult.Error:
                                    Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo",
                                        MessageBoxIcon.Error);
                                    break;
                                case ActionResult.UNKNOW:
                                    Utility.ShowMsg("Lỗi không xác định", "Thông báo", MessageBoxIcon.Error);
                                    break;
                                case ActionResult.Cancel:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyCommand();
                GC.Collect();
            }

        }
        private void cmdHuythanhtoan_Click(object sender, EventArgs e)
        {
            HuyThanhtoan();
        }

        private void cmdIndonthuoc_Click(object sender, EventArgs e)
        {
            m_strMaLuotkham = Utility.sDbnull(grdPres.GetValue(KcbDonthuoc.Columns.MaLuotkham));
            int presId = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
            PrintPres(presId, "");
        }

        private void cmdXoadonthuoc_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPres)) return;
            //Chỉ được phép xóa đơn thuốc quầy thuốc, không cho phép xóa đơn thuốc của bác sĩ kê
            try
            {
                if (Utility.ByteDbnull(grdPres.GetValue(KcbDonthuoc.Columns.Donthuoctaiquay)) == 0)
                {
                    Utility.ShowMsg("Đơn thuốc đang chọn là đơn của bác sĩ kê nên bạn không được phép xóa. Chỉ có thể sửa số lượng giảm hoặc bỏ các thuốc người bệnh không muốn mua. Vui lòng kiểm tra lại");
                    return;
                }
                if (Kiemtratrangthai_donthuoc())//Kiểm tra từ CSDL cho chắc ăn
                {
                    Utility.ShowMsg(
                        "Đơn thuốc này đã có chi tiết được thanh toán hoặc cấp phát nên không thể chỉnh sửa. Vui lòng liên hệ các bộ phận liên quan để kiểm tra lại");
                    return;
                }
                //if (m_dtChitietdonthuoc.AsEnumerable().Any(c => c.Field<byte>(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan) > 0))
                //{
                //    Utility.ShowMsg("Đơn thuốc đang chọn đã được thanh toán nên bạn không thể xóa. Vui lòng kiểm tra lại");
                //    return;
                //}
                //if (m_dtChitietdonthuoc.AsEnumerable().Any(c =>c.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) > 0))
                //{
                //    Utility.ShowMsg("Đơn thuốc đang chọn đã được duyệt cấp phát thuốc nên bạn không thể xóa. Vui lòng kiểm tra lại");
                //    return;
                //}

                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa đơn thuốc của người bệnh {0}?", grdPres.GetValue("ten_benhnhan").ToString()), "xác nhận xóa", true))
                {
                    SPs.DonthuocXoaDonthuoc(Pres_ID).Execute();
                    DataRow[] arrdr = mv_dtDonthuoc.Select(string.Format("{0}={1}", KcbDonthuoc.Columns.IdDonthuoc, Pres_ID));
                    if (arrdr.Length > 0)
                        mv_dtDonthuoc.Rows.Remove(arrdr[0]);
                    m_dtChitietdonthuoc.AcceptChanges();
                    grdPres_SelectionChanged(grdPres, e);
                    Utility.ShowMsg(string.Format("Đã xóa đơn thuốc của người bệnh {0} thành công. Nhấn OK để kết thúc", grdPres.GetValue("ten_benhnhan").ToString()));
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }
        private void cmdCapnhatdonthuoc_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPres)) return;
            if (Utility.ByteDbnull(grdPres.GetValue(KcbDonthuoc.Columns.Donthuoctaiquay)) == 0)
            {
                Utility.ShowMsg("Đơn thuốc đang chọn là đơn của bác sĩ kê nên bạn không được phép xóa. Chỉ có thể sửa số lượng giảm hoặc bỏ các thuốc người bệnh không muốn mua. Vui lòng kiểm tra lại");
                return;
            }
            if (Kiemtratrangthai_donthuoc())
            {
                Utility.ShowMsg(
                    "Đơn thuốc này đã có chi tiết được thanh toán hoặc cấp phát nên không thể chỉnh sửa. Vui lòng liên hệ các bộ phận liên quan để kiểm tra lại");
                return;
            }
           
            if (Utility.Coquyen("quyen_suadonthuoc") || Utility.sDbnull(grdPres.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") == globalVariables.UserName)
            {
                UpdateDonThuoc();
            }
            else
            {
                Utility.ShowMsg(
                    "Đơn thuốc đang chọn sửa được tạo bởi bác sĩ khác và bạn không được gán quyền sửa(quyen_suadonthuoc). Mời bạn thực hiện công việc khác");
                return;
            }
        }
        private void cmdThemdonthuoc_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPres)) return;
            if (objBenhnhan != null && objLuotkham != null)
            {
                ThemMoiDonThuoc();
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdThanhtoan2_Click(object sender, EventArgs e)
        {
            doPayment();
        }

        private void cmdHuythanhtoan2_Click(object sender, EventArgs e)
        {
            HuyThanhtoan();
        }

        private void cmdHuyduyetCapphat_Click(object sender, EventArgs e)
        {
            HuyCapphatthuoc();
        }

        private void chkThanhtoan_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mnuRollback_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdChitietDonthuoc)) return;
            }
            catch (Exception ex)
            {
                
            }
            

        }

        private void cmdCauhinhPhieuxuatthuoc_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._PhieuxuatBNProperty);
            frm.ShowDialog();
            ResetItems();
        }

        private void cmdTraLaiTien_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdCboDownHTTT_Click(object sender, EventArgs e)
        {
            txtPttt.ShowMe();
        }

        private void cmdCboDownNganhang_Click(object sender, EventArgs e)
        {
            autoNganhang.ShowMe();
        }

        private void mnuUpdateIDThuockho_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdChitietDonthuoc))
                {
                    Utility.ShowMsg("Bạn cần chọn thuốc/vật tư trong đơn trước khi thực hiện chức năng này");
                    return;
                }

                frm_capnhat_idthuockho _capnhat_idthuockho = new frm_capnhat_idthuockho();
                _capnhat_idthuockho.id_phieu = Utility.Int64Dbnull(grdChitietDonthuoc.GetValue("id_donthuoc"), -1);
                _capnhat_idthuockho.id_phieu_ctiet = Utility.Int64Dbnull(grdChitietDonthuoc.GetValue("id_chitietdonthuoc"), -1);
                _capnhat_idthuockho.so_luong = Utility.Int32Dbnull(grdChitietDonthuoc.GetValue("so_luong"), -1);
                _capnhat_idthuockho.id_thuoc = Utility.Int32Dbnull(grdChitietDonthuoc.GetValue("id_chitietdichvu"), -1);
                _capnhat_idthuockho.id_kho = Utility.Int32Dbnull(cboKho.SelectedValue, -1);
                _capnhat_idthuockho.loai = 1;
                _capnhat_idthuockho.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuTaobiendongthuoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("THUOC_TAOBIENDONG_THUOCTRALAI_TAIQUAY"))
                {
                    Utility.ShowMsg("Bạn không có quyền tạo biến động thuốc trả lại tại quầy theo phiếu chi đang chọn (THUOC_TAOBIENDONG_THUOCTRALAI_TAIQUAY). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                    return;
                }
                if (Utility.isValidGrid(grdPhieuChi))
                {
                    KcbThanhtoan objPhieuchi = KcbThanhtoan.FetchByID(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdPhieuChi, KcbThanhtoan.Columns.IdThanhtoan), -1));
                    if (objPhieuchi != null)
                        _THANHTOAN.TaobanghiBiendongTrathuoctaiquay(objPhieuchi);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdXacnhantralai_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidCancelData()) return;
                string[] query = (from p in grdThongTinDaThanhToan.GetCheckedRows()
                              select string.Format("Thuốc: {0}, số lượng trả lại: {1} {2}", Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value, ""), Utility.sDbnull(p.Cells["sl_tralai"].Value, ""), Utility.sDbnull(p.Cells["donvi_tinh"].Value, ""))).ToArray();
                string noidung = string.Join("\n", query);
                if (Utility.AcceptQuestion(string.Format( "Bạn có chắc chắn muốn tạo phiếu nhận thuốc trả lại từ người bệnh với các thuốc trả lại dưới đây:\n",noidung),
                    "Thông báo", true))
                {
                    //if (objLuotkham == null)
                    //{
                    //    objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdPres.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1), Utility.sDbnull(grdPres.GetValue(KcbLuotkham.Columns.MaLuotkham), "-1"));
                    //}
                    //if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                    //{
                    //    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép trả lại tiền ngoại trú nữa");
                    //    return;
                    //}
                    //KcbThanhtoan objPayment = TaophieuThanhtoanHuy();
                    ThuocLichsuTralaithuoctaiquayPhieu newPhieu = new ThuocLichsuTralaithuoctaiquayPhieu();
                    newPhieu.IdBenhnhan = objLuotkham.IdBenhnhan;
                    newPhieu.MaLuotkham = objLuotkham.MaLuotkham;
                    newPhieu.TrangThai = 0;
                    newPhieu.NguoiTao = globalVariables.UserName;
                    newPhieu.NgayTao = DateTime.Now;
                    newPhieu.Save();
                    //query = (from p in grdThongTinDaThanhToan.GetCheckedRows()
                    //                  select string.Format("{0} {1} {2}", Utility.sDbnull(p.Cells["sl_tralai"].Value, ""), Utility.sDbnull(p.Cells["donvi_tinh"].Value, ""), Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value, ""))).ToArray();
                    // noidung = string.Join(";", query);
                    //frm_Chondanhmucdungchung _Chondanhmucdungchung = new frm_Chondanhmucdungchung("LYDOTRATIEN", "TRẢ TIỀN LẠI CHO BỆNH NHÂN", "Chọn lý do trả trước khi thực hiện", "Lý do trả tiền", false);
                    //_Chondanhmucdungchung.Lydomacdinh = string.Format("Người bệnh trả lại: {0}", noidung);
                    //_Chondanhmucdungchung.ShowDialog();
                    //if (_Chondanhmucdungchung.m_blnCancel) return;
                    ActionResult actionResult = _THANHTOAN.TaoPhieuTrathuoctaiquay_BophanDuoc(newPhieu,objLuotkham, TaodulieuthanhtoanchitietHuy());
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            //tabThongTinThanhToan.SelectedTab = tabPagePhieuChi;
                            LaydanhsachLichsu_phieutralaithuoc();
                           
                            Utility.GotoNewRowJanus(grdLichsutrathuoc, ThuocLichsuTralaithuoctaiquayPhieu.Columns.IdTralaithuoc,
                                    Utility.sDbnull(newPhieu.IdTralaithuoc));
                            //Thực hiện in phiếu nhận thuốc để kí và mang đến quầy thanh toán nhận lại tiền
                            new INPHIEU_THANHTOAN_NGOAITRU().In_Phieuchi_TraLaithuoc(newPhieu.IdTralaithuoc);
                            ModifyCommand();
                            break;
                        case ActionResult.AssignIsConfirmed:
                            Utility.ShowMsg("Đã có dịch vụ được thực hiện rồi. Bạn không thể trả lại tiền !",
                                "Thông báo");
                            break;
                        case ActionResult.PresIsConfirmed:
                            Utility.ShowMsg("Đã có thuốc được xác nhận cấp phát. Bạn không thể trả lại tiền !",
                                "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Bản ghi thanh toán chi tiết trả lại không tồn tại. Liên hệ IT để được hỗ trợ", "Thông báo");
                            break;
                        case ActionResult.Cancel:
                            Utility.ShowMsg("Bản ghi đơn thuốc chi tiết trả lại không tồn tại. Liên hệ IT để được hỗ trợ", "Thông báo");
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                modifyButtonsTraThuocTaiquay();
            }
        }

        private void cmdInphieuhoantrathuoc_Click(object sender, EventArgs e)
        {
            try
            {
                long id_tralaithuoc = Utility.Int64Dbnull(grdLichsutrathuoc.GetValue("id_tralaithuoc"));
                new INPHIEU_THANHTOAN_NGOAITRU().In_Phieuchi_TraLaithuoc(id_tralaithuoc);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuCheck_Click(object sender, EventArgs e)
        {
            KiemTraTonthuoctrongdon();
        }
    }
}
