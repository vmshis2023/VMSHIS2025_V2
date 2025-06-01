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
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Baocao;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_PhieuTrathuocthua : Form
    {
        private bool b_Hasloaded = false;
        private DataTable m_dtPhieutrathuocthua=new DataTable();
        private DataSet m_dtDetail = new DataSet();
        int id_PhieuNhap = -1;
        public string KIEU_THUOC_VT = "THUOC";
        public frm_PhieuTrathuocthua(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;

            dtFromdate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;
            this.KeyDown += new KeyEventHandler(frm_PhieuCapphatThuocTonghop_KeyDown);
            grdList.ApplyingFilter += new CancelEventHandler(grdList_ApplyingFilter);
            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
            txtKhoanoitru._OnEnterMe += txtKhoanoitru__OnEnterMe;
            grdList.DoubleClick += grdList_DoubleClick;
            mnuViewDetail.Click += mnuViewDetail_Click;
            optAll.CheckedChanged += _CheckedChanged;
            optChuaxacnhan.CheckedChanged += _CheckedChanged;
            optDaxacnhan.CheckedChanged += _CheckedChanged;
            cmdConfig.Click+=cmdConfig_Click;
            cmdInphieu.Click += cmdInphieu_Click;
            CauHinh();
            setquyen();
        }

        void txtKhoanoitru__OnEnterMe()
        {
            try
            {
                short IDKhoa = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
                object myid = txtKhothuoc.MyID;
                //DataRow[] arrdr = m_dtKhonhan.Select("ID_KHOAPHONG=" + IDKhoa + " or id_khoaphong_captren=" + IDKhoa);
                //DataTable _newTable = m_dtKhonhan.Clone();
                //if (arrdr.Length > 0) _newTable = arrdr.CopyToDataTable();
                DataTable _newTable = CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(IDKhoa, -1, "ALL", KIEU_THUOC_VT, 100, 100);// CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(IDKhoa, IDKhoa, "ALL", KIEU_THUOC_VT, 100, 100);
                txtKhothuoc.Init(_newTable, new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });
                txtKhothuoc.SetId(myid);
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
                cmdThemPhieuNhap.Visible = Utility.Coquyen("thuoc_phieutrathuocthua_themmoi");
                cmdUpdatePhieuNhap.Visible = Utility.Coquyen("thuoc_phieutrathuocthua_sua");
                cmdXoaPhieuNhap.Visible = Utility.Coquyen("thuoc_phieutrathuocthua_xoa");
                cmdNhapKho.Visible = Utility.Coquyen("thuoc_phieutrathuocthua_xacnhan");
                cmdHuychuyenkho.Visible = Utility.Coquyen("thuoc_phieutrathuocthua_huyxacnhan");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void cmdInphieu_Click(object sender, EventArgs e)
        {
            int _Id = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocthua.Columns.Id), -1);

            TPhieutrathuocthua objPhieutrathuocthua = TPhieutrathuocthua.FetchByID(_Id);
            if (objPhieutrathuocthua != null)
            {
                thuoc_baocao.InPhieutrathuocthua(objPhieutrathuocthua, "PHIẾU TRẢ THUỐC THỪA", globalVariables.SysDate);
            }
        }

        void grdList_DoubleClick(object sender, EventArgs e)
        {
            grdList_SelectionChanged(sender, e);
        }

        void _CheckedChanged(object sender, EventArgs e)
        {
            
        }

        void mnuViewDetail_Click(object sender, EventArgs e)
        {
            if (mnuViewDetail.Checked) grdChitiet.BringToFront();
            else
                grdChitiet.SendToBack();
            Application.DoEvents();
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhieuCapphatThuocTonghop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && e.Control) cmdThemPhieuNhap.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdUpdatePhieuNhap.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoaPhieuNhap.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInphieu.PerformClick();
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
            
            byte TRANG_THAI = 2;
            if (optDaxacnhan.Checked) TRANG_THAI = 1;
            if (optChuaxacnhan.Checked) TRANG_THAI = 0;
            string MaKho = "-1";

            m_dtPhieutrathuocthua = DuocNoitru.ThuocNoitruTimkiemPhieutrathuocthua(Utility.Int32Dbnull(txtId.Text, -1), chkByDate.Checked ? dtFromdate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900", Utility.Bool2byte(chkTheongaytra.Checked), Utility.Int32Dbnull(txtKhoanoitru.MyID, -1), Utility.Int32Dbnull(txtKhothuoc.MyID, -1)
                , Utility.Int32Dbnull(txtNguoitra.MyID, -1), Utility.Int32Dbnull(txtNguoinhan.MyID, -1), TRANG_THAI, KIEU_THUOC_VT);

            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtPhieutrathuocthua, true, true, "1=1", "");
            if (!Utility.isValidGrid(grdList)) if (m_dtDetail != null) m_dtDetail.Clear();
            ModifyCommand();
        }
        private void ModifyCommand()
        {
            cmdThemPhieuNhap.Enabled = true;
            if (grdList.RowCount <= 0 || grdList.CurrentRow == null || grdList.CurrentRow.RowType != RowType.Record)
            {
                cmdUpdatePhieuNhap.Enabled = false;
                cmdXoaPhieuNhap.Enabled = false;
                cmdNhapKho.Enabled = false;
                cmdHuychuyenkho.Enabled = false;
                cmdInphieu.Enabled = false;
            }
            else
            {
                cmdInphieu.Enabled = true;
                int Trang_thai = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocthua.Columns.TrangThai), 0);
                cmdXoaPhieuNhap.Enabled = Trang_thai == 0;
                cmdUpdatePhieuNhap.Enabled = cmdXoaPhieuNhap.Enabled;
                cmdNhapKho.Enabled = Trang_thai == 0;
                cmdHuychuyenkho.Enabled = !cmdNhapKho.Enabled;
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
        private bool InValiUpdateXoa()
        {
            int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocthua.Columns.Id), -1);
            SqlQuery sqlQuery = new Select().From(TPhieutrathuocthua.Schema)
                .Where(TPhieutrathuocthua.Columns.TrangThai).IsEqualTo(1)
                .And(TPhieutrathuocthua.Columns.Id).IsEqualTo(IdPhieu);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu trả thuốc thừa đã được xác nhận nên bạn không thể sửa hoặc xóa thông tin", "Thông báo", MessageBoxIcon.Warning);
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
            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
            int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocthua.Columns.Id), -1);
            if (!InValiUpdateXoa()) return;
            if (Utility.AcceptQuestion(string.Format("Bạn có muốn xóa phiếu trả thuốc thừa với mã phiếu {0}\n hay không?", IdPhieu), "Thông báo", true))
            {
                ActionResult actionResult = new Trathuocthua().XoaPhieuTrathuocthua(IdPhieu);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.Delete();
                        grdList.UpdateData();
                        m_dtPhieutrathuocthua.AcceptChanges();
                        Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Bạn xóa thông tin phiếu trả thuốc thừa thành công", false);
                        break;
                    case ActionResult.Error:
                        break;
                }

            }
            ModifyCommand();
        }

        private void frm_PhieuCapphatThuocTonghop_Load(object sender, EventArgs e)
        {
            cmdConfig.Visible = globalVariables.IsAdmin;
            InitData();
            TIMKIEM_THONGTIN();
            ModifyCommand();
        }
        private DataTable m_dtKhoNhan =new DataTable();
        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của Form
        /// </summary>
        private void InitData()
        {
            DataTable m_dtKhoaNoitru = THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1);// THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            txtKhoanoitru.SetId(globalVariables.idKhoatheoMay);
            txtKhoanoitru.Init(m_dtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoNhan = CommonLoadDuoc.LAYDANHMUCKHO(-1,"TATCA,NOITRU", "THUOC,THUOCVT", "CHANLE,LE", 100, 100, 1); // CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NOITRU();
            }
            else
            {
                m_dtKhoNhan = CommonLoadDuoc.LAYDANHMUCKHO(-1,"TATCA,NOITRU", "VT,THUOCVT", "CHANLE,LE", 100, 100, 1); // CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_NOITRU();
            }
            txtKhothuoc.Init(m_dtKhoNhan, new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });
            DataTable dtnhanvien = THU_VIEN_CHUNG.Laydanhsachnhanvien("ALL");
            txtNguoinhan.Init(dtnhanvien, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoitra.Init(dtnhanvien, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            b_Hasloaded = true;
        }
        private void AutocompleteKhoanoitru()
        {
          
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
                Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
                if (Utility.AcceptQuestion("Bạn đã chắc chắn muốn xác nhận trả thuốc thừa từ phiếu đang chọn hay không?\nSau khi xác nhận, dữ liệu thuốc sẽ được cộng vào kho nhận", "Thông báo", true))
                {
                    string errMsg = "";
                    int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocthua.Columns.Id), -1);
                    TPhieutrathuocthua objTPhieutrathuocthua = TPhieutrathuocthua.FetchByID(IdPhieu);
                    if (objTPhieutrathuocthua != null)
                    {
                        DateTime _ngayxacnhan = globalVariables.SysDate;
                        string _nguoitra = "";
                        frm_ChonngayXacnhan _ChonngayXacnhan = new frm_ChonngayXacnhan();
                        _ChonngayXacnhan.pdt_InputDate = objTPhieutrathuocthua.NgayLapphieu;
                        _ChonngayXacnhan.ShowDialog();
                        if (_ChonngayXacnhan.b_Cancel)
                            return;
                        else
                        {
                            _ngayxacnhan = _ChonngayXacnhan.pdt_InputDate;
                            _nguoitra = _ChonngayXacnhan.txtNhanvien.Text;
                        }
                        objTPhieutrathuocthua.NgayTra = _ngayxacnhan;
                        objTPhieutrathuocthua.NguoiTra = Utility.Int16Dbnull(_ChonngayXacnhan.txtNhanvien.MyID, -1);
                        ActionResult actionResult =
                            new Trathuocthua().Xacnhanphieutrathuocthua(objTPhieutrathuocthua);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Trả thuốc thừa từ khoa nội trú về kho thành công", false);
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Cells[TPhieutrathuocthua.Columns.TrangThai].Value = 1;
                                grdList.CurrentRow.Cells[TPhieutrathuocthua.Columns.NgayTra].Value = _ngayxacnhan;
                                grdList.CurrentRow.Cells[TPhieutrathuocthua.Columns.NguoiTra].Value = objTPhieutrathuocthua.NguoiTra;
                                grdList.CurrentRow.Cells[TPhieutrathuocthua.Columns.NguoiNhan].Value = globalVariables.gv_intIDNhanvien;
                                grdList.CurrentRow.Cells["sngay_tra"].Value = _ngayxacnhan.ToString("dd/MM/yyyy");
                                grdList.CurrentRow.Cells["ten_nguoitra"].Value = _nguoitra;
                                grdList.CurrentRow.Cells["ten_nguoinhan"].Value = Utility.getTenNhanvien();
                                grdList.CurrentRow.EndEdit();
                                grdList.UpdateData();
                                break;
                            case ActionResult.Exceed:
                                Utility.ShowMsg("Một số chi tiết thuốc không tồn tại nên hệ thống không thể xác nhận. Đề nghị bạn kiểm tra lại");
                                break;
                            case ActionResult.DataChanged:
                                Utility.ShowMsg("Dữ liệu cấp phát đã bị thay đổi nên bạn không thể xác nhận. Đề nghị nhấn nút tìm kiếm để lấy lại dữ liệu mới nhất");
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
            ModifyCommand();
        }
       /// <summary>
       /// hàm thực hiện việc di chuyển thông tin của trên lưới
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (Utility.isValidGrid(grdList))
            {
                int IDPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocthua.Columns.Id), -1);
                m_dtDetail = DuocNoitru.ThuocNoitruLayChitietPhieutrathuocthua(IDPhieu,(byte)0);
                Utility.SetDataSourceForDataGridEx(grdDetail, m_dtDetail.Tables[0], false, true, "1=1", "");
                Utility.SetDataSourceForDataGridEx(grdChitiet, m_dtDetail.Tables[1], false, true, "1=1", "");
            }
            else
            {
                grdChitiet.DataSource = null;
                grdDetail.DataSource = null;
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
                frm_themmoi_phieutrathuocthua frm = new frm_themmoi_phieutrathuocthua(KIEU_THUOC_VT);
                frm.m_enAction = action.Insert;
                frm._OnInsertCompleted+=frm__OnInsertCompleted;
                frm.m_dtData = m_dtPhieutrathuocthua;
                frm.idPhieutra = -1;
                frm.ShowDialog();
                
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
        /// hàm thực hiện việc cho phép thông tin của phiếu trả thuốc từ khoa nội trú về kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (!InValiUpdateXoa()) return;
                int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocthua.Columns.Id), -1);
                frm_themmoi_phieutrathuocthua frm = new frm_themmoi_phieutrathuocthua(KIEU_THUOC_VT);
                frm.m_enAction = action.Update;
                frm.m_dtData = m_dtPhieutrathuocthua;
                frm._OnInsertCompleted += frm__OnInsertCompleted;
                frm.idPhieutra = IdPhieu;
                frm.ShowDialog();
               
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

        void frm__OnInsertCompleted(long idcapphat)
        {
            Utility.GotoNewRowJanus(grdList, TPhieutrathuocthua.Columns.Id, idcapphat.ToString());
        }
        /// <summary>
        /// hàm thực hiện việc tì kiếm thông tin sucar phiếu trả thuốc kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSoPhieu_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSoPhieu_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                cmdSearch.PerformClick();
            }
        }
        private void cmdHuychuyenkho_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có muốn hủy xác nhận phiếu trả thuốc từ khoa nội trú về kho hay không?", "Thông báo", true))
                {
                    string errMsg = "";
                    int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocthua.Columns.Id), -1);
                    TPhieutrathuocthua objTPhieutrathuocthua = TPhieutrathuocthua.FetchByID(IdPhieu);
                    if (objTPhieutrathuocthua != null)
                    {
                        ActionResult actionResult =
                            new Trathuocthua().HuyXacnhanphieuphieutrathuocthua(objTPhieutrathuocthua, ref errMsg);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.ShowMsg("Bạn thực hiện hủy xác nhận phiếu trả thuốc thừa từ khoa nội trú về kho thành công", "Thông báo");
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Cells[TPhieutrathuocthua.Columns.TrangThai].Value = 0;
                                grdList.CurrentRow.Cells[TPhieutrathuocthua.Columns.NgayTra].Value = DBNull.Value;
                                grdList.CurrentRow.Cells[TPhieutrathuocthua.Columns.NguoiTra].Value = DBNull.Value;
                                grdList.CurrentRow.Cells[TPhieutrathuocthua.Columns.NguoiNhan].Value = DBNull.Value;
                                grdList.CurrentRow.Cells["sngay_tra"].Value = "";
                                grdList.CurrentRow.Cells["ten_nguoitra"].Value = "";
                                grdList.CurrentRow.Cells["ten_nguoinhan"].Value = "";
                                grdList.CurrentRow.EndEdit();
                                break;
                            case ActionResult.Exceed:
                                Utility.ShowMsg("Thuốc trong kho nhận đã được sử dụng hết nên bạn không thể hủy phiếu trả thuốc thừa");
                                break;
                            case ActionResult.NotEnoughDrugInStock:
                                Utility.ShowMsg("Thuốc trong kho nhận đã sử dụng gần hết nên không còn đủ số lượng để hoàn trả lại khoa");
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
                ModifyCommand();
            }
        }


        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._TrathuocthuaProperties);
                frm.ShowDialog();
                CauHinh();
            }
            catch (Exception exception)
            {

            }
        }
        private void CauHinh()
        {
            cmdThemPhieuNhap.Visible = PropertyLib._TrathuocthuaProperties.QuyenThemPhieu;
            cmdUpdatePhieuNhap.Visible = PropertyLib._TrathuocthuaProperties.QuyenSuaPhieu;
            cmdXoaPhieuNhap.Visible = PropertyLib._TrathuocthuaProperties.QuyenXoaPhieu;
            cmdInphieu.Visible = PropertyLib._TrathuocthuaProperties.QuyenInphieu;
            cmdNhapKho.Visible = PropertyLib._TrathuocthuaProperties.QuyenXacNhanPhieu;
            cmdHuychuyenkho.Visible = PropertyLib._TrathuocthuaProperties.QuyenHuyXacNhanPhieu;
            chkTheongaytra.Checked = PropertyLib._TrathuocthuaProperties.Timtheongaytra;

        }
    }
}
