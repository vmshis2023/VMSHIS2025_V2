using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
using VNS.HIS.UI.THUOC;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Forms.Cauhinh;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_PhieuTrathuoc_KhoLeVeKhoChan : Form
    {
        private bool b_Hasloaded = false;
        private DataTable m_dtDataNhapKho = new DataTable();
        private DataTable m_dtDataPhieuChiTiet = new DataTable();
        
        private int id_PhieuNhap;
       
        public string KIEU_THUOC_VT = "THUOC";
        public frm_PhieuTrathuoc_KhoLeVeKhoChan(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            
            dtFromdate.Value = globalVariables.SysDate.AddMonths(-1);
            dtToDate.Value = globalVariables.SysDate;
            this.KeyDown += new KeyEventHandler(frm_PhieuTrathuoc_KhoLeVeKhoChan_KeyDown);
            grdList.ApplyingFilter += new CancelEventHandler(grdList_ApplyingFilter);
            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
            CauHinh();
            setquyen();
            this.KeyUp += _KeyUp;
        }

        void _KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }
        void setquyen()
        {
            try
            {
                cmdThemPhieuNhap.Visible = Utility.Coquyen("thuoc_phieutrathuoclechan_themmoi");
                cmdUpdatePhieuNhap.Visible = Utility.Coquyen("thuoc_phieutrathuoclechan_sua");
                cmdXoaPhieuNhap.Visible = Utility.Coquyen("thuoc_phieutrathuoclechan_xoa");
                cmdNhapKho.Visible = Utility.Coquyen("thuoc_phieutrathuoclechan_xacnhan");
                cmdHuychuyenkho.Visible = Utility.Coquyen("thuoc_phieutrathuoclechan_huyxacnhan");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhieuTrathuoc_KhoLeVeKhoChan_KeyDown(object sender, KeyEventArgs e)
        {
        
            if (e.KeyCode == Keys.N && e.Control) cmdThemPhieuNhap.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdUpdatePhieuNhap.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoaPhieuNhap.PerformClick();
            if ((e.Control && e.KeyCode==Keys.P) || e.KeyCode == Keys.F4) cmdInPhieuNhapKho.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        }
       
        /// <summary>
        /// hàm thực hiện việc tim kiem thong tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TIMKIEM_THONGTIN();

        }
        /// <summary>
        /// HÀM THỰC HIỆN TÌM KIẾM THÔNG TIN 
        /// </summary>
        private void TIMKIEM_THONGTIN()
        {
            int TRANG_THAI = -1;
            if (radDaNhap.Checked) TRANG_THAI = 1;
            if (radChuaNhapKho.Checked) TRANG_THAI = 0;
            if (radTatCa.Checked) TRANG_THAI = -1;
            string MaKho = "-1";
           

            m_dtDataNhapKho =
                new THUOC_NHAPKHO().Laydanhsachphieunhapkho(chkByDate.Checked ? dtFromdate.Value.ToString("dd/MM/yyyy") :"01/01/1900",
                                             chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900", -1, Utility.Int32Dbnull(txtTenthuoc.MyID, -1),
                                             Utility.Int32Dbnull(cboKhoNhap.SelectedValue, -1), Utility.Int32Dbnull(cboKhoXuat.SelectedValue, -1),
                                             Utility.Int32Dbnull(cboNhanVien.SelectedValue, -1),
                                             -1, "-1", Utility.sDbnull(txtSoPhieu.Text), TRANG_THAI, (int)LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan, MaKho, 2, KIEU_THUOC_VT);

            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtDataNhapKho, true, true, "1=1", "");
            if (!Utility.isValidGrid(grdList)) if(m_dtDataPhieuChiTiet!=null) m_dtDataPhieuChiTiet.Clear();
            Utility.SetGridEXSortKey(grdList, TPhieuNhapxuatthuoc.Columns.IdPhieu,
                                    Janus.Windows.GridEX.SortOrder.Ascending);

            ModifyCommand();
        }
        private void ModifyCommand()
        {
            try
            {
                bool CHUAXACNHAN = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.TrangThai), 0) == 0;
                cmdUpdatePhieuNhap.Enabled = cmdXoaPhieuNhap.Enabled = CHUAXACNHAN && Utility.isValidGrid(grdList);
                cmdNhapKho.Enabled = CHUAXACNHAN && Utility.isValidGrid(grdList);
                cmdHuychuyenkho.Enabled = Utility.isValidGrid(grdList) && !cmdNhapKho.Enabled;
                cmdInPhieuNhapKho.Enabled = Utility.isValidGrid(grdList);
                if (!Utility.isValidGrid(grdList))
                {
                    if (Utility.isValidDatatable(m_dtDataPhieuChiTiet, false)) m_dtDataPhieuChiTiet.Rows.Clear();
                }
            }
            catch (Exception)
            {

            }

          
        }
        /// <summary>
        /// hàm thực hiện việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// trạng thái của khi chọn toàn bọ hoặc bỏ ngày tháng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }
        private bool IsValid4UpdateDelete()
        {
            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
            int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
            SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuoc.Schema)
                .Where(TPhieuNhapxuatthuoc.Columns.TrangThai).IsEqualTo(1)
                .And(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(IdPhieu);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Phiếu chuyển kho đã xác nhận, Bạn không thể sửa hoặc xóa thông tin", false);
                return false;
            }
            TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(IdPhieu);
            if (Utility.sDbnull(objTPhieuNhapxuatthuoc.NguoiTao) != globalVariables.UserName && THU_VIEN_CHUNG.Laygiatrithamsohethong_off("THUOC_PHANQUYEN_USER", "1", false) == "1")
            {
                Utility.ShowMsg(
                    string.Format(
                        "Phiếu số {0} được tạo bởi tài khoản {1}. \n Xin liên hệ với người dùng {1} để sửa hoặc xóa, nhập phiếu",
                        objTPhieuNhapxuatthuoc.IdPhieu, objTPhieuNhapxuatthuoc.NguoiTao));
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện xóa thông tin của hiếu nhập chi tiết
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaPhieuNhap_Click(object sender, EventArgs e)
        {
            int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
            if (!IsValid4UpdateDelete()) return;
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa thông tin phiếu chuyển kho với mã phiếu {0} hay không?", IdPhieu), "Thông báo", true))
            {
                ActionResult actionResult = new THUOC_NHAPKHO().XoaPhieuNhapKho(IdPhieu);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.Delete();
                        grdList.UpdateData();
                        m_dtDataNhapKho.AcceptChanges();
                        Utility.ShowMsg("Bạn xóa thông tin phiếu chuyển kho thành công", "Thông báo", MessageBoxIcon.Information);
                        break;
                    case ActionResult.Error:
                        break;
                }

            }
            ModifyCommand();
        }
        string ten_kieuthuoc_vt = "Thuốc";
        private void frm_PhieuTrathuoc_KhoLeVeKhoChan_Load(object sender, EventArgs e)
        {
            ten_kieuthuoc_vt = KIEU_THUOC_VT == "VT" ? "Vật tư" : "Thuốc";
            InitData();
            TIMKIEM_THONGTIN();
            ModifyCommand();
            cmdCauhinh.Visible = globalVariables.IsAdmin;
        }
        private DataTable m_dtKhoNhap, m_dtKhoXuat = new DataTable();
        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của Form
        /// </summary>
        private void InitData()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoNhap = CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", "THUOC,THUOCVT", "CHANLE,CHAN", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN();
                m_dtKhoXuat = CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", "THUOC,THUOCVT", "CHANLE,LE", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE();
            }
            else
            {
                m_dtKhoNhap = CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", "VT,THUOCVT", "CHANLE,CHAN", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
                m_dtKhoXuat = CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", "VT,THUOCVT", "CHANLE,LE", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU", "NOITRU" });
            }

            //if (KIEU_THUOC_VT == "THUOC")
            //{
            //    m_dtKhoXuat = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN();
            //    m_dtKhoNhap = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE();
            //}
            //else
            //{
            //    m_dtKhoXuat = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
            //    m_dtKhoNhap = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU", "NOITRU" });
            //}

            if(m_dtKhoXuat.Rows.Count>1)
                DataBinding.BindDataCombobox(cboKhoXuat, m_dtKhoXuat,
                                       TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                       "---Kho xuất---",false);
            else
                DataBinding.BindDataCombobox(cboKhoXuat, m_dtKhoXuat,
                                       TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn---", true);
            DataBinding.BindDataCombobox(cboKhoNhap, m_dtKhoNhap,
                                      TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                      "---Kho nhập---",false);
            txtTenthuoc.Init(CommonLoadDuoc.LayThongTinThuoc(KIEU_THUOC_VT),
                             new List<string>() { DmucThuoc.Columns.IdThuoc, DmucThuoc.Columns.MaThuoc, DmucThuoc.Columns.TenThuoc });

        }
        /// <summary>
        /// hà thực hiện việc in phiêu xuat kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuNhapKho_Click(object sender, EventArgs e)
        {
            int IDPhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);

            TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(IDPhieuNhap);
            if (objPhieuNhap != null)
            {
                if (Utility.Byte2Bool(objPhieuNhap.DuTru.Value))
                    VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuDutru(IDPhieuNhap, "PHIẾU DỰ TRÙ", globalVariables.SysDate);
                else
                {
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_INPHIEUXUATKHO _2LIEN", "0", false) == "1")
                        VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuXuatkho_2lien(IDPhieuNhap, "PHIẾU XUẤT", globalVariables.SysDate);
                    else
                        VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuXuatkho(IDPhieuNhap, "PHIẾU NHẬP TRẢ", globalVariables.SysDate);
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_INBIENBAN_GIAONHANHANG", "0", false) == "1")
                    {
                        VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InBienBanGiaoHang(IDPhieuNhap, "BIÊN BẢN GIAO NHẬN HÀNG HÓA", globalVariables.SysDate);
                    }
                }
            }


            //int IDPhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);

            //TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(IDPhieuNhap);
            //if (objPhieuNhap != null)
            //{
            //    VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuTraKholeVeKhochan(IDPhieuNhap, "PHIẾU NHẬP TRẢ", globalVariables.SysDate);
            //}
        }
        /// <summary>
        /// hàm thực hiện việc cho phép chuyển thông tin xác nhận vào kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNhapKho_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                cmdNhapKho.Enabled = false;
                Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
                int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(IdPhieu);
                if (objTPhieuNhapxuatthuoc != null)
                {
                    if (Utility.ByteDbnull(objTPhieuNhapxuatthuoc.TrangThai, 0) == 1)
                    {
                        return;
                    }
                    //if (Utility.sDbnull(objTPhieuNhapxuatthuoc.NguoiTao) != globalVariables.UserName && THU_VIEN_CHUNG.Laygiatrithamsohethong_off("THUOC_PHANQUYEN_USER", "1", false) == "1")
                    //{
                    //    Utility.ShowMsg(
                    //        string.Format(
                    //            "Phiếu số {0} được tạo bởi tài khoản {1}. \n Xin liên hệ với người dùng {1} để sửa hoặc xóa, nhập phiếu",
                    //            objTPhieuNhapxuatthuoc.IdPhieu, objTPhieuNhapxuatthuoc.NguoiTao));
                    //    return;
                    //}
                    string errMsg = "";
                    DateTime _ngayxacnhan = globalVariables.SysDate;
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_HIENTHI_NGAYXACNHAN", "0", false) == "1")
                    {
                        frm_ChonngayXacnhan _ChonngayXacnhan = new frm_ChonngayXacnhan();
                        _ChonngayXacnhan.pdt_InputDate = objTPhieuNhapxuatthuoc.NgayHoadon;
                        _ChonngayXacnhan.ShowDialog();
                        if (_ChonngayXacnhan.b_Cancel)
                            return;
                        else
                            _ngayxacnhan = _ChonngayXacnhan.pdt_InputDate;
                    }
                    ActionResult actionResult =
                         new PhieuTraLai().XacNhanTraLaiKhoLeVeKhoChan(objTPhieuNhapxuatthuoc, _ngayxacnhan, ref errMsg);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Trả thuốc từ kho lẻ về kho chẵn thành công", false);
                            Utility.ShowMsg(uiStatusBar2.Panels["MSG"].Text);
                            grdList.CurrentRow.BeginEdit();
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 1;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NgayXacnhan].Value = _ngayxacnhan;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NguoiXacnhan].Value = globalVariables.UserName;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 1;
                            grdList.CurrentRow.EndEdit();
                            break;
                        case ActionResult.Exceed:
                            Utility.ShowMsg("Không có " + ten_kieuthuoc_vt + " trong kho xuất nên không thể xác nhận Phiếu xuất trả kho lẻ về kho chẵn\n" + errMsg, "Thông báo lỗi", MessageBoxIcon.Warning);
                            break;
                        case ActionResult.NotEnoughDrugInStock:
                            Utility.ShowMsg("" + ten_kieuthuoc_vt + " trong kho xuất không còn đủ số lượng nên không thể xác nhận Phiếu xuất trả kho lẻ về kho chẵn\n" + errMsg, "Thông báo lỗi", MessageBoxIcon.Warning);
                            break;
                        case ActionResult.UNKNOW:
                            Utility.ShowMsg(string.Format("{0} trong {1} không thể trả lại {2}. Do {3} này được nhận từ kho khác\n{4}", ten_kieuthuoc_vt, grdList.GetValue("ten_khoxuat").ToString(), grdList.GetValue("ten_khonhap").ToString(), ten_kieuthuoc_vt, errMsg), "Thông báo lỗi", MessageBoxIcon.Warning);
                            break;
                        case ActionResult.Error:
                            break;
                    }
                }

               
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
                ModifyCommand();
            }
                
        }

        /// <summary>
        /// hàm thực hiện việc cho phép lọc thông tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện việc di chuyển thông tin của trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
            {
                int IDPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                m_dtDataPhieuChiTiet =
                       new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(IDPhieu);
                Utility.SetDataSourceForDataGridEx_Basic(grdPhieuNhapChiTiet, m_dtDataPhieuChiTiet, true, true, "1=1", "");
            }
            else
            {
                grdPhieuNhapChiTiet.DataSource = null;
            }
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc thêm phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_PhieutrathuocKholeVeKhoChan frm = new frm_themmoi_PhieutrathuocKholeVeKhoChan();
                frm.m_enAction = action.Insert;
                frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                frm.grdList = grdList;
                frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                frm.txtIDPhieuNhapKho.Text = "-1";
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    grdList_SelectionChanged(grdList, new EventArgs());
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
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc cho phép thông tin của phiếu nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValid4UpdateDelete()) return;
                int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                frm_themmoi_PhieutrathuocKholeVeKhoChan frm = new frm_themmoi_PhieutrathuocKholeVeKhoChan();
                frm.m_enAction = action.Update;
                frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                frm.grdList = grdList;
                frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                frm.txtIDPhieuNhapKho.Text = Utility.sDbnull(IdPhieu);

                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    grdList_SelectionChanged(grdList, new EventArgs());
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
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc tì kiếm thông tin sucar phiếu xuất kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSoPhieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoPhieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSearch.PerformClick();
            }
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._ChuyenkhoProperties);
                frm.ShowDialog();
                CauHinh();

                //grdPresDetail.SaveLayoutFile();
            }
            catch (Exception exception)
            {

            }
        }

        private void CauHinh()
        {

            cmdThemPhieuNhap.Visible = PropertyLib._ChuyenkhoProperties.QuyenThemPhieu;
            cmdUpdatePhieuNhap.Visible = PropertyLib._ChuyenkhoProperties.QuyenSuaPhieu;
            cmdXoaPhieuNhap.Visible = PropertyLib._ChuyenkhoProperties.QuyenXoaPhieu;
            cmdNhapKho.Visible = PropertyLib._ChuyenkhoProperties.QuyenXacNhanPhieu;
            cmdHuychuyenkho.Visible = PropertyLib._ChuyenkhoProperties.QuyenHuyXacNhanPhieu;
            cmdInPhieuNhapKho.Visible = PropertyLib._ChuyenkhoProperties.QuyenInphieu;
        }

        private void cmdHuychuyenkho_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                cmdHuychuyenkho.Enabled = false;
                Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy xác nhận Phiếu xuất trả kho lẻ về kho chẵn đang chọn hay không?", "Thông báo", true))
                {
                    int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(IdPhieu);
                    if (objTPhieuNhapxuatthuoc != null)
                    {
                        if (Utility.ByteDbnull(objTPhieuNhapxuatthuoc.TrangThai, 0) == 0)
                        {
                            return;
                        }
                        if (Utility.sDbnull(objTPhieuNhapxuatthuoc.NguoiXacnhan) != globalVariables.UserName && THU_VIEN_CHUNG.Laygiatrithamsohethong_off("THUOC_PHANQUYEN_USER", "1", false) == "1")
                        {
                            Utility.ShowMsg(
                                string.Format(
                                    "Phiếu số {0} được tạo bởi tài khoản {1}. \n Xin liên hệ với người dùng {1} để sửa hoặc xóa, nhập phiếu",
                                    objTPhieuNhapxuatthuoc.IdPhieu, objTPhieuNhapxuatthuoc.NguoiTao));
                            return;
                        }
                        string errMsg = "";
                        ActionResult actionResult =
                             new PhieuTraLai().HuyXacNhanPhieuTralaiKho(objTPhieuNhapxuatthuoc, ref errMsg);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Hủy trả thuốc từ kho lẻ về kho chẵn thành công", false);
                                Utility.ShowMsg(uiStatusBar2.Panels["MSG"].Text);
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 0;
                                grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NgayXacnhan].Value = DBNull.Value;
                                grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NguoiXacnhan].Value = DBNull.Value;
                                grdList.CurrentRow.EndEdit();
                                break;
                            case ActionResult.notExists:
                                Utility.ShowMsg("Không tìm thấy id thuốc kho xuất để cộng số lượng trả lại. Vui lòng kiểm tra lại\n" + errMsg, "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Exceed:
                                Utility.ShowMsg("Thuốc trong kho nhận đã được sử dụng hết nên bạn không thể hủy Phiếu xuất trả kho lẻ về kho chẵn", "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.NotEnoughDrugInStock:
                                Utility.ShowMsg("Thuốc trong kho nhận(chẵn) không đủ số lượng tồn để hoàn trả lại kho xuất(lẻ)\n" + errMsg, "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Error:
                                break;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
                ModifyCommand();
            }
          
        }

        private void cmdCauhinh_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties(PropertyLib._ChuyenkhoProperties);
                frm.ShowDialog();
                CauHinh();

                //grdPresDetail.SaveLayoutFile();
            }
            catch (Exception exception)
            {

            }
        }

        private void mnuUpdateIdThuockho_Click(object sender, EventArgs e)
        {
             if (!Utility.isValidGrid(grdList))
            {
                Utility.ShowMsg("Bạn cần chọn phiếu chuyển kho trước khi thực hiện chức năng này");
                return;
            }
            if (!Utility.isValidGrid(grdPhieuNhapChiTiet))
            {
                Utility.ShowMsg("Bạn cần chọn thuốc/vật tư trong phiếu trước khi thực hiện chức năng này");
                return;
            }
            try
            {
                //Check xem phiếu đang chọn ở trạng thái chưa xác nhận mới cho cập nhật
                TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_phieu"), -1));
                if (objPhieuNhap.TrangThai == 1)
                {
                    Utility.ShowMsg("Phiếu đã chọn đã được xác nhận nên bạn không được phép cập nhật lại id thuốc kho của bất kỳ chi tiết nào thuộc phiếu. Muốn cập nhật id thuốc kho thì cần hủy xác nhận phiếu");
                    return;
                }

                frm_capnhat_idthuockho _capnhat_idthuockho = new frm_capnhat_idthuockho();
            _capnhat_idthuockho.id_phieu = Utility.Int64Dbnull(grdPhieuNhapChiTiet.GetValue("id_phieu"), -1);
            _capnhat_idthuockho.id_phieu_ctiet = Utility.Int64Dbnull(grdPhieuNhapChiTiet.GetValue("id_phieuchitiet"), -1);
            _capnhat_idthuockho.so_luong = Utility.Int32Dbnull(grdPhieuNhapChiTiet.GetValue("so_luong"), -1);
            _capnhat_idthuockho.id_thuoc = Utility.Int32Dbnull(grdPhieuNhapChiTiet.GetValue("id_thuoc"), -1);
            _capnhat_idthuockho.id_kho = Utility.Int32Dbnull(grdList.GetValue("id_khoxuat"), -1);
            _capnhat_idthuockho.loai = Utility.ByteDbnull(objPhieuNhap.LoaiPhieu);
            _capnhat_idthuockho.ShowDialog();
            if (_capnhat_idthuockho.id_thuockho > 0)
            {
                grdPhieuNhapChiTiet.CurrentRow.BeginEdit();
                grdPhieuNhapChiTiet.CurrentRow.Cells["id_chuyen"].Value = _capnhat_idthuockho.id_thuockho;
                grdPhieuNhapChiTiet.CurrentRow.EndEdit();
                grdPhieuNhapChiTiet.UpdateData();
            }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            try
            {
              
                int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                frm_themmoi_PhieutrathuocKholeVeKhoChan frm = new frm_themmoi_PhieutrathuocKholeVeKhoChan();
                frm.m_enAction = action.View;
                frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                frm.grdList = grdList;
                frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                frm.txtIDPhieuNhapKho.Text = Utility.sDbnull(IdPhieu);

                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    grdList_SelectionChanged(grdList, new EventArgs());
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
                ModifyCommand();
            }
        }
    }
}
