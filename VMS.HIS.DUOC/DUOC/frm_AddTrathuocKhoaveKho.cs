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
using VNS.HIS.NGHIEPVU.THUOC;



namespace VNS.HIS.UI.THUOC
{
    public partial class frm_AddTrathuocKhoaveKho : Form
    {
        private DataTable m_dtKhoNhap, m_dtKhoTra, m_KhoaTra = new DataTable();
        bool b_Hasloaded = false;
        public DataTable p_mDataPhieuNhapKho = new DataTable();
        private DataTable m_dtDataPhieuChiTiet = new DataTable();
        public action m_enAction = action.Insert;
        public bool b_Cancel = false;
        public Janus.Windows.GridEX.GridEX grdList;

        string KIEU_THUOC_VT = "THUOC";
        public frm_AddTrathuocKhoaveKho(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            dtNgayNhap.Value = globalVariables.SysDate;
            cmdInPhieuNhap.Click+=new EventHandler(cmdInPhieuNhap_Click);
            cboKhoXuat.SelectedIndexChanged+=new EventHandler(cboKhoXuat_SelectedIndexChanged);
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
            grdKhoXuat.KeyDown += new KeyEventHandler(grdKhoXuat_KeyDown);
            
        }
        void grdKhoXuat_KeyDown(object sender, KeyEventArgs e)
        {
            Janus.Windows.GridEX.GridEXColumn gridExColumn = grdKhoXuat.RootTable.Columns["SO_LUONG_CHUYEN"];
            if (e.Control && e.KeyCode == Keys.Enter)// && Utility.Int32Dbnull(grdKhoXuat.GetValue(gridExColumn.Key), 0) > 0 && grdKhoXuat.CurrentColumn.Position == gridExColumn.Position)
            {
                txtFilterName.Focus();
                grdKhoXuat.Focus();
                cmdNext_Click(cmdNext, new EventArgs());
                txtFilterName.Clear();
                txtFilterName.Focus();
            }
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaThongTin_Click(object sender, EventArgs e)
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuXuatChiTiet.GetCheckedRows())
            {
                int ID = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet].Value);
                TPhieuNhapxuatthuocChitiet.Delete(ID);
                gridExRow.Delete();
                grdPhieuXuatChiTiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc trạng thái thông tin 
        /// của nút
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdSave.Enabled = grdPhieuXuatChiTiet.RowCount > 0;
                cmdInPhieuNhap.Enabled = grdPhieuXuatChiTiet.RowCount > 0;
                cboKhoXuat.Enabled = grdPhieuXuatChiTiet.RowCount <= 0;

            }
            catch (Exception exception)
            {

            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuNhap_Click(object sender, EventArgs e)
        {
            int IDPhieuNhap = Utility.Int32Dbnull(txtIDPhieuNhapKho.Text, -1);
            TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(IDPhieuNhap);
            if (objPhieuNhap != null)
            {
                VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuXuatkho(IDPhieuNhap, "PHIẾU XUẤT", globalVariables.SysDate);
            }
        }
        private void InitalData()
        {
            m_KhoaTra = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0);
            DataBinding.BindDataCombobox(cboNhanVien, CommonLoadDuoc.LAYTHONGTIN_NHANVIEN(),
                                      DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "---Nhân viên---",true);
            cboNhanVien.Enabled = false;
            LoadKho();
        }

        private void LoadKho()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoNhap = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NOITRU();
                m_dtKhoTra = CommonLoadDuoc.LAYTHONGTIN_TUTHUOC();
            }
            else
            {
                m_dtKhoNhap = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
                m_dtKhoTra = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE_TUTRUC();//new List<string> { "TATCA", "NOITRU" });
            }

            //m_dtKhoNhap = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE();
            //m_dtKhoTra = m_dtKhoNhap.Copy();
           
            cboNhanVien.SelectedValue = globalVariables.gv_intIDNhanvien;
            DataBinding.BindDataCombobox(cboKhoNhap, m_dtKhoNhap,
                                       TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Kho nhập---",false);
            DataBinding.BindDataCombobox(cboKhoaTra, m_KhoaTra,
                                      DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                      "---Khoa trả---",false);
        }

        private void getData()
        {
            if (m_enAction == action.Update)
            {
                TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                if (objPhieuNhap != null)
                {
                    dtNgayNhap.Value = objPhieuNhap.NgayHoadon;
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    cboKhoaTra.SelectedIndex = Utility.GetSelectedIndex(cboKhoaTra, objPhieuNhap.IdKhoalinh.ToString());
                    cboKhoXuat.SelectedIndex = Utility.GetSelectedIndex(cboKhoXuat, objPhieuNhap.IdKhoxuat.ToString());
                    cboKhoNhap.SelectedIndex = Utility.GetSelectedIndex(cboKhoNhap, objPhieuNhap.IdKhonhap.ToString());
                    cboNhanVien.SelectedIndex = Utility.GetSelectedIndex(cboNhanVien, objPhieuNhap.IdNhanvien.ToString());

                

                    m_dtDataPhieuChiTiet =
                        new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    Utility.SetDataSourceForDataGridEx(grdPhieuXuatChiTiet, m_dtDataPhieuChiTiet, false, true, "1=1", "");
                }
            }
            if (m_enAction == action.Insert)
            {
                m_dtDataPhieuChiTiet =
                       new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(-100);
                Utility.SetDataSourceForDataGridEx(grdPhieuXuatChiTiet, m_dtDataPhieuChiTiet, false, true, "SO_LUONG>0", "");
            }
            UpdateWhenChanged();
        }
        private void SetStatusControl()
        {
            Utility.ResetMessageError(errorProvider1);
            Utility.ResetMessageError(errorProvider2);

            if (cboKhoXuat.SelectedIndex <= 0)
            {
                Utility.SetMsgError(errorProvider2, cboKhoXuat, "Bạn chọn kho xuất");

            }
            if (cboKhoNhap.SelectedIndex <= 0)
            {
                Utility.SetMsgError(errorProvider2, cboKhoNhap, "Phải chọn kho nhập");

            }

        }
        private void frm_AddTrathuocKhoaveKho_Load(object sender, EventArgs e)
        {
            InitalData();
            getData();
            ModifyCommand();
            b_Hasloaded = true;
        }
        //hàm thực hiện việc ẩn hiện thông tin 
        private void cboKhoXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _rowFilter = "1=1";
            try
            {

                if (cboKhoXuat.SelectedIndex > 0)
                {
                    _rowFilter = string.Format("{0}<>{1}", TDmucKho.Columns.IdKho,
                                               Utility.Int32Dbnull(cboKhoXuat.SelectedValue));
                }
            }
            catch (Exception)
            {
                _rowFilter = "1=1";

            }
            m_dtKhoNhap.DefaultView.RowFilter = _rowFilter;
            m_dtKhoNhap.AcceptChanges();
            SetStatusControl();
            getThuocTrongKho();

        }
        private DataTable m_dtDataThuocKho=new DataTable();
        private void getThuocTrongKho()
        {
            try
            {
                m_dtDataThuocKho =
                    SPs.ThuocLaythuoctrongkhoxuat(Utility.Int32Dbnull(cboKhoXuat.SelectedValue),
                                                    chkIsHetHan.Checked ? 1 : 0, KIEU_THUOC_VT).GetDataSet().Tables[0];
                Utility.AddColumToDataTable(ref  m_dtDataThuocKho,"ShortName",typeof(string));
                foreach (DataRow drv in m_dtDataThuocKho.Rows)
                {
                    drv["ShortName"] = Utility.UnSignedCharacter(Utility.sDbnull(drv["Ten_Thuoc"]));
                }
                m_dtDataThuocKho.AcceptChanges();
                BindKhoThuoc(m_dtDataThuocKho);
                
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        private void BindKhoThuoc(DataTable dataTable)
        {
            Utility.SetDataSourceForDataGridEx(grdKhoXuat, dataTable, false, true, "So_luong>0", "");
        }

        /// <summary>
        /// HÀM HỰC HIỆN VIỆC LƯU LẠI THÔNG TIN 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!InValiNhapKho()) return;
            PerformAction();
        }
        private void PerformAction()
        {
            switch (m_enAction)
            {
                case action.Insert:
                    ThemPhieuXuatKho();
                    break;
                case action.Update:
                    UpdatePhieuXuatKho();
                    break;
            }
        }
        #region "khai báo các đối tượng để thực hiện việc "
        private TPhieuNhapxuatthuoc CreatePhieuNhapKho()
        {
            TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = new TPhieuNhapxuatthuoc();
            if (m_enAction == action.Update)
            {
                objTPhieuNhapxuatthuoc.MarkOld();
                objTPhieuNhapxuatthuoc.IsLoaded = true;
                objTPhieuNhapxuatthuoc.IdPhieu = Utility.Int32Dbnull(txtIDPhieuNhapKho.Text, -1);
            }
            objTPhieuNhapxuatthuoc.Vat = 0;
            objTPhieuNhapxuatthuoc.SoHoadon = string.Empty;
            objTPhieuNhapxuatthuoc.IdKhonhap = Utility.Int16Dbnull(cboKhoNhap.SelectedValue, -1);
            objTPhieuNhapxuatthuoc.IdKhoxuat = Utility.Int16Dbnull(cboKhoXuat.SelectedValue, -1);
            objTPhieuNhapxuatthuoc.MaNhacungcap = "";
            objTPhieuNhapxuatthuoc.MotaThem = "Trả thuốc từ khoa về kho lẻ";
            objTPhieuNhapxuatthuoc.TrangThai = 0;
            objTPhieuNhapxuatthuoc.IdKhoalinh = Utility.Int16Dbnull(cboKhoaTra.SelectedValue, 0);

            objTPhieuNhapxuatthuoc.IdNhanvien = Utility.Int16Dbnull(cboNhanVien.SelectedValue, -1);
            if (Utility.Int32Dbnull(objTPhieuNhapxuatthuoc.IdNhanvien, -1) <= 0)
                objTPhieuNhapxuatthuoc.IdNhanvien = globalVariables.gv_intIDNhanvien;
            objTPhieuNhapxuatthuoc.NgayHoadon = dtNgayNhap.Value;
            objTPhieuNhapxuatthuoc.NgayTao = globalVariables.SysDate;
            objTPhieuNhapxuatthuoc.NguoiTao = globalVariables.UserName;
            objTPhieuNhapxuatthuoc.NguoiGiao = globalVariables.UserName;
            objTPhieuNhapxuatthuoc.LoaiPhieu =(byte) LoaiPhieu.PhieuNhapTraKhoLe;
            objTPhieuNhapxuatthuoc.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapTraKhoLe);
            return objTPhieuNhapxuatthuoc;
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin chi tiết
        /// </summary>
        /// <returns></returns>
        private TPhieuNhapxuatthuocChitiet[] CreateArrPhieuChiTiet()
        {
            List<TPhieuNhapxuatthuocChitiet> lstItems = new List<TPhieuNhapxuatthuocChitiet>();

            foreach (DataRow dr in m_dtDataPhieuChiTiet.Rows)
            {

                TPhieuNhapxuatthuocChitiet newItem = new TPhieuNhapxuatthuocChitiet();
                newItem.IdThuoc =
                    Utility.Int32Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc]);
                newItem.NgayHethan =
                    Convert.ToDateTime(dr[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan]).Date;
                newItem.GiaBan = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaBan]);
                newItem.GiaNhap = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap]);
                newItem.SoLo = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoLo], "");
                newItem.SoLuong = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoLuong], 0);
                newItem.ThanhTien =
                    Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap]) *
                    Utility.Int32Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoLuong]);

                newItem.Vat = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.Vat], 0);
                newItem.MotaThem = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.MotaThem]);
                newItem.IdPhieu = Utility.Int32Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu], -1);

                newItem.IdThuockho = Utility.Int64Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho], -1);
                newItem.MaNhacungcap = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap]);

                lstItems.Add(newItem);

            }
            return lstItems.ToArray();
        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc thêm phiếu trả nhập kho thuốc
        /// </summary>
        private void ThemPhieuXuatKho()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = CreatePhieuNhapKho();

            ActionResult actionResult = new XuatThuoc().ThemPhieuXuatKho(objPhieuNhap, CreateArrPhieuChiTiet());
            switch (actionResult)
            {
                case ActionResult.Success:
                    txtIDPhieuNhapKho.Text = Utility.sDbnull(objPhieuNhap.IdPhieu);
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow newDr = p_mDataPhieuNhapKho.NewRow();
                    Utility.FromObjectToDatarow(objTPhieuNhapxuatthuoc, ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoNhap.SelectedValue, -1));
                    if (objKho != null)
                        newDr["TEN_KHO_NHAP"] = Utility.sDbnull(objKho.TenKho);
                    objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoXuat.SelectedValue, -1));
                    if (objKho != null)
                        newDr["TEN_KHO_XUAT"] = Utility.sDbnull(objKho.TenKho);
                    newDr["ID_KHOA_LINH"] = Utility.Int16Dbnull(cboKhoaTra.SelectedValue,0);
                    newDr["TEN_KHOA"] = cboKhoaTra.Text;
                    p_mDataPhieuNhapKho.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdList, TPhieuNhapxuatthuoc.Columns.IdPhieu, Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    Utility.ShowMsg("Bạn thêm mới phiếu trả thành công", "Thông báo");
                    m_enAction = action.Insert;
                  
                    b_Cancel = true;
                    this.Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm phiếu trả", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }
        private void UpdatePhieuXuatKho()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = CreatePhieuNhapKho();

            ActionResult actionResult = new XuatThuoc().UpdatePhieuXuatKho(objPhieuNhap, CreateArrPhieuChiTiet());
            switch (actionResult)
            {
                case ActionResult.Success:
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow[] arrDr =
                        p_mDataPhieuNhapKho.Select(string.Format("{0}={1}", TPhieuNhapxuatthuoc.Columns.IdPhieu,
                                                                 Utility.Int32Dbnull(txtIDPhieuNhapKho.Text)));
                    if (arrDr.GetLength(0) > 0)
                    {
                        arrDr[0].Delete();
                    }
                    DataRow newDr = p_mDataPhieuNhapKho.NewRow();
                    Utility.FromObjectToDatarow(objTPhieuNhapxuatthuoc, ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoNhap.SelectedValue, -1));
                    if (objKho != null)
                        newDr["TEN_KHO_NHAP"] = Utility.sDbnull(objKho.TenKho);
                    objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoXuat.SelectedValue, -1));
                    if (objKho != null)
                        newDr["TEN_KHO_XUAT"] = Utility.sDbnull(objKho.TenKho);
                    newDr["ID_KHOA_LINH"] = Utility.Int16Dbnull(cboKhoaTra.SelectedValue, 0);
                    newDr["TEN_KHOA"] = cboKhoaTra.Text;
                    p_mDataPhieuNhapKho.Rows.Add(newDr);

                    Utility.GonewRowJanus(grdList, TPhieuNhapxuatthuoc.Columns.IdPhieu, Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    Utility.ShowMsg("Bạn sửa  phiếu trả thành công", "Thông báo");
                    m_enAction = action.Insert;
                   
                    b_Cancel = true;
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện việc Invalinhap khoa thuốc
        /// </summary>
        /// <returns></returns>
        private bool InValiNhapKho()
        {
            if (cboKhoaTra.SelectedIndex <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn khoa trả", "Thông báo", MessageBoxIcon.Warning);
                cboKhoaTra.Focus();
                return false;
            }
            if (cboKhoXuat.SelectedIndex <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn kho xuất", "Thông báo", MessageBoxIcon.Warning);
                cboKhoXuat.Focus();
                return false;
            }

            if (cboKhoNhap.SelectedIndex <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn kho để nhập thuốc", "Thông báo", MessageBoxIcon.Warning);
                cboKhoNhap.Focus();
                return false;
            }


            return true;
        }

        private void cboKhoXuat_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void chkIsHetHan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                getThuocTrongKho();
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        private void cmdGetData_Click(object sender, EventArgs e)
        {
            getThuocTrongKho();
        }
        /// <summary>
        /// hàm thực hiện việc thay đổi thôn gtin của trên lưới 
        /// loc thông tin của thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterText(txtFilterName.Text.Trim());
            }
            catch (Exception)
            {
                
                //throw;
            }
           
        }
        private void FilterText(string prefixText)
        {
            try
            {
                m_dtDataThuocKho.DefaultView.RowFilter = "1=1";
                if (!string.IsNullOrEmpty(prefixText))
                {
                    m_dtDataThuocKho.DefaultView.RowFilter = "TEN_THUOC like '%" + prefixText + "%' OR ma_thuoc like '%" + prefixText + "%' OR shortname like '%" + prefixText + "%'";
                }
            }
            catch (Exception)
            {

                /// throw;
            }
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt của lọc thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.PageDown)
            {
                grdKhoXuat.Focus();
                grdKhoXuat.MoveFirst();
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdKhoXuat.RootTable.Columns["SO_LUONG_CHUYEN"];
                grdKhoXuat.Col = gridExColumn.Position;
            }
        }

        private void grdKhoXuat_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "SO_LUONG_CHUYEN")
            {
                int soluongchuyen = Utility.Int32Dbnull(e.Value);
                int soluongchuyencu = Utility.Int32Dbnull(e.InitialValue);
                int soluongthat = Utility.Int32Dbnull(grdKhoXuat.GetValue("So_luong"));
                if (soluongchuyen < 0)
                {
                    Utility.ShowMsg("Số lượng thuốc cần chuyển phải >=0", "Thông báo", MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    if (soluongchuyen > soluongthat)
                    {
                        Utility.ShowMsg("Số lượng thuốc cần chuyển phải <= số lượng thuốc có trong kho", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = soluongchuyencu;
                        e.Cancel = true;
                    }
                    else
                    {
                        grdKhoXuat.CurrentRow.IsChecked = soluongchuyen > 0;
                    }
                }
            }
        }
        /// <summary>
        /// hàm thực hiện việc đấy thông tin của 
        /// dược từ kho này sang kho kia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNext_Click(object sender, EventArgs e)
        {
            AddDetailNext();
        }

        private void AddDetailNext()
        {
            try
            {
                string manhacungcap = "";
                string NgayHethan = "";
                string solo = "";
                int id_thuoc = -1;
                decimal dongia = 0m;
                decimal Giaban = 0m;
                Int32 soluongchuyen = 0;
                decimal vat = 0m;
                int isHetHan = 0;
                long IdThuockho = 0;
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetDataRows())
                {
                    soluongchuyen = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_CHUYEN"].Value, 0);
                    if (soluongchuyen > 0)
                    {
                        NgayHethan = Utility.sDbnull(gridExRow.Cells["NGAY_HET_HAN"].Value);
                        solo = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLo].Value);
                        id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuoc].Value, -1);
                        IdThuockho = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuockho].Value, -1);
                        dongia = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaNhap].Value, 0);
                        Giaban = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBan].Value, 0);

                        vat = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.Vat].Value, 0);
                        isHetHan = Utility.Int32Dbnull(gridExRow.Cells["IsHetHan"].Value, 0);
                        manhacungcap = Utility.sDbnull(gridExRow.Cells[TThuockho.Columns.MaNhacungcap].Value, 0);
                        DataRow[] arrDr = m_dtDataPhieuChiTiet.Select(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho + "=" + IdThuockho.ToString());
                        if (arrDr.Length <= 0)
                        {
                            DataRow drv = m_dtDataPhieuChiTiet.NewRow();
                            drv[TPhieuNhapxuatthuocChitiet.Columns.MotaThem] = String.Empty;

                            drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc] = id_thuoc;
                            drv["ten_donvitinh"] = Utility.sDbnull(gridExRow.Cells["ten_donvitinh"].Value);
                            drv["IsHetHan"] = isHetHan;
                            DmucThuoc objLDrug = DmucThuoc.FetchByID(id_thuoc);
                            if (objLDrug != null)
                            {
                                drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objLDrug.TenThuoc);
                                drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(objLDrug.HamLuong);
                                drv[DmucThuoc.Columns.MaHoatchat] = Utility.sDbnull(objLDrug.MaHoatchat);
                                drv[DmucThuoc.Columns.NuocSanxuat] = Utility.sDbnull(objLDrug.NuocSanxuat);
                                drv[DmucThuoc.Columns.HangSanxuat] = Utility.sDbnull(objLDrug.HangSanxuat);
                            }
                            drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = vat;
                            drv[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap] = dongia;
                            drv[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap] = manhacungcap;
                            drv[TPhieuNhapxuatthuocChitiet.Columns.SoLo] = solo;
                            drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho] = IdThuockho;
                            drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBan] = Giaban;

                            drv[TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = soluongchuyen;
                            drv[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = dongia * soluongchuyen;
                            drv[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau] = 0;
                            drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = NgayHethan;
                            drv[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu] = -1;
                            m_dtDataPhieuChiTiet.Rows.Add(drv);
                        }
                        else
                        {
                            arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + soluongchuyen;
                            m_dtDataPhieuChiTiet.AcceptChanges();
                        }
                    }

                }
                UpdateWhenChanged();
                ResetValueInGridEx();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển thuốc:\n" + ex.Message);
            }
        }
        private void RemoveDetails()
        {
            try
            {
                string manhacungcap = "";
                string NgayHethan = "";
                string solo = "";
                int id_thuoc = -1;
                decimal dongia = 0m;
                decimal Giaban = 0m;
                Int32 soluong = 0;
                decimal vat = 0m;
                int isHetHan = 0;
                long IdThuockho = 0;
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuXuatChiTiet.GetCheckedRows())
                {


                    NgayHethan = Utility.sDbnull(gridExRow.Cells["NGAY_HET_HAN"].Value);
                    solo = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLo].Value);
                    id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuoc].Value, -1);
                    IdThuockho = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuockho].Value, -1);
                    dongia = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaNhap].Value, 0);
                    Giaban = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBan].Value, 0);
                    soluong = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG"].Value, 0);
                    vat = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.Vat].Value, 0);
                    isHetHan = Utility.Int32Dbnull(gridExRow.Cells["IsHetHan"].Value, 0);
                    manhacungcap = Utility.sDbnull(gridExRow.Cells[TThuockho.Columns.MaNhacungcap].Value, 0);



                    DataRow[] arrDr = m_dtDataThuocKho.Select(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho + "=" + IdThuockho.ToString());
                    if (arrDr.Length <= 0)
                    {
                        DataRow drv = m_dtDataThuocKho.NewRow();


                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc] = id_thuoc;
                        drv["ten_donvitinh"] = Utility.sDbnull(gridExRow.Cells["ten_donvitinh"].Value);
                        drv["IsHetHan"] = isHetHan;
                        DmucThuoc objLDrug = DmucThuoc.FetchByID(id_thuoc);
                        if (objLDrug != null)
                        {
                            drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objLDrug.TenThuoc);
                            drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(objLDrug.HamLuong);
                            drv[DmucThuoc.Columns.MaHoatchat] = Utility.sDbnull(objLDrug.MaHoatchat);
                            drv[DmucThuoc.Columns.NuocSanxuat] = Utility.sDbnull(objLDrug.NuocSanxuat);
                            drv[DmucThuoc.Columns.HangSanxuat] = Utility.sDbnull(objLDrug.HangSanxuat);
                        }
                        drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = vat;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap] = dongia;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap] = manhacungcap;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLo] = solo;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho] = IdThuockho;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBan] = Giaban;

                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = soluong;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = dongia * soluong;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau] = 0;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = NgayHethan;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu] = -1;
                        m_dtDataThuocKho.Rows.Add(drv);

                    }
                    else
                    {
                        arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + soluong;
                        arrDr[0]["SO_LUONG_THAT"] = arrDr[0]["SO_LUONG"];
                        m_dtDataThuocKho.AcceptChanges();
                    }
                    gridExRow.Delete();
                    grdPhieuXuatChiTiet.UpdateData();
                    grdPhieuXuatChiTiet.Refresh();
                    m_dtDataPhieuChiTiet.AcceptChanges();
                    m_dtDataThuocKho.AcceptChanges();
                }
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy chuyển thuốc:\n" + ex.Message);
            }
        }
        private void ResetValueInGridEx()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetCheckedRows())
            {
                gridExRow.BeginEdit();
                gridExRow.Cells["SO_LUONG_CHUYEN"].Value = 0;
                gridExRow.IsChecked = false;
                gridExRow.BeginEdit();
            }
            grdKhoXuat.UpdateData();
            m_dtDataThuocKho.AcceptChanges();
        }
        private  void UpdateWhenChanged()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    var query = from thuoc in grdPhieuXuatChiTiet.GetDataRows()
                                let sl = Utility.Int32Dbnull(thuoc.Cells["SO_LUONG"].Value)
                                let IdThuockho = Utility.Int32Dbnull(thuoc.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho].Value)
                                where IdThuockho == Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho].Value)
                                select sl;
                    if (query.Any())
                    {
                        int soluong = Utility.Int32Dbnull(query.FirstOrDefault());
                        int soluongthat = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_THAT"].Value);
                        gridExRow.BeginEdit();
                        gridExRow.Cells["SO_LUONG"].Value = soluongthat - soluong;
                        gridExRow.EndEdit();
                        grdKhoXuat.UpdateData();
                        m_dtDataThuocKho.AcceptChanges();
                    }
                }
            }
        }

        private void cmdPrevius_Click(object sender, EventArgs e)
        {
            RemoveDetails();
        }

        private void cmdXoaThongTin_Click_1(object sender, EventArgs e)
        {

        }
        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }
        private void cboKhoaTra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!b_Hasloaded) return;
                string IDKhoa = cboKhoaTra.SelectedValue.ToString();
                DataRow[] arrdr = m_dtKhoTra.Select("ID_KHOA=" + IDKhoa);
                DataTable _newTable = m_dtKhoTra.Clone();
                if (arrdr.Length > 0) _newTable = arrdr.CopyToDataTable();
                DataBinding.BindDataCombobox(cboKhoXuat, _newTable,
                                           TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "--Tủ thuốc--",false);

            }
            catch
            {
            }
        }
    }
}
