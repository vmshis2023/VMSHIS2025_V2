using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Microsoft.VisualBasic;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UCs;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.Libs;
using NLog;
using System.Text;
using newBus.Noitru;
using VNS.HIS.UI.Classess;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_PhieuChamSoc : Form
    {
        private DataTable m_dtNhanVien = new DataTable();
        private DataTable m_dtPhieuChamSoc = new DataTable();
        private DataTable m_dtPhieuTheoDoiTruyenDich = new DataTable();
        private DataTable m_dNoitruPhieuthuphanungthuoc = new DataTable();
        private DataTable m_dtDataPhieuDichTruyen = new DataTable();        
        private DataTable m_dtPhieuTheoDoiChucNang = new DataTable();
        private long id_chitietdonthuoc;
        private long id_donthuoc;
        private int id_chitietdonthuoc_Thuoc_Thu;
        private int soluong;
        private int idthuockho;
        private int doctorid;
        private string tenthuoc;
        private string solo;
        private int id_thuoc;
        private int Patientdeptid;
        private int id_khoadieutri;
        DataTable dtData_PhieuDichTruyen = new DataTable();
        private string _rowFilter = "1=1";
        public int Id_phieu_thu_thuoc = -1;
        public int Department_ID_phieu_thu_thuoc = -1;
        public int PatientDept_ID_phieu_thu_thuoc = -1;
        public bool b_CallParent = false;
        private readonly Logger _log;
        private string thamso = " ";
        action m_enAct = action.FirstOrFinished;
        bool AllowSelectionChanged = false;
        public frm_PhieuChamSoc()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Shown += frm_PhieuChamSoc_Shown;
            FormClosing += frm_PhieuChamSoc_FormClosing;
            cmdExit.Click += cmdExit_Click;
            ucThongtinnguoibenh1.noitrungoaitru = 1;
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
            grdChamsoc.DoubleClick += GrdListChamSocDoubleClick;
            grdDonthuocChitiet.SelectionChanged += grdDonthuocChitiet_SelectionChanged;
            KeyDown += frm_PhieuChamSoc_KeyDown;
            txtIDPhieu.Visible = txtIDPhieuTheoDoi.Visible = globalVariables.IsAdmin;
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.NguoiTao, globalVariables.IsAdmin);
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.NgayTao, globalVariables.IsAdmin);
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.NguoiSua, globalVariables.IsAdmin);
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.NgaySua, globalVariables.IsAdmin);
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.Id, globalVariables.IsAdmin);
            dtNgayNhap.Value = dtNgayTheoDoi.Value = THU_VIEN_CHUNG.GetSysDateTime();
            chkHienthiphieudain.CheckedChanged+=chkHienthiphieudain_CheckedChanged;
        }

        void grdDonthuocChitiet_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdDonthuocChitiet) || !AllowSelectionChanged) return;
                id_chitietdonthuoc = Utility.Int64Dbnull(grdDonthuocChitiet.GetValue("id_chitietdonthuoc"), -1);
                id_donthuoc = Utility.Int64Dbnull(grdDonthuocChitiet.GetValue("id_donthuoc"), -1);
                soluong = Utility.Int32Dbnull(grdDonthuocChitiet.GetValue("so_luong"), -1);
                idthuockho = Utility.Int32Dbnull(grdDonthuocChitiet.GetValue("Id_ThuocKho"), -1);
                tenthuoc = Utility.sDbnull(grdDonthuocChitiet.GetValue("ten_thuoc"));
                solo = Utility.sDbnull(grdDonthuocChitiet.GetValue("so_lo"));
                id_thuoc = Utility.Int32Dbnull(grdDonthuocChitiet.GetValue("id_thuoc"), -1);
                doctorid = Utility.Int32Dbnull(grdDonthuocChitiet.GetValue("id_bacsi"), -1);
                Patientdeptid = Utility.Int32Dbnull(grdDonthuocChitiet.GetValue("id_buong_giuong"), -1);
                id_khoadieutri = Utility.Int32Dbnull(grdDonthuocChitiet.GetValue("id_khoanoitru"), -1);

                //m_dtDataPhieuDichTruyen.DefaultView.RowFilter = "id_thuoc=" + id_thuoc.ToString();
                //m_dtDataPhieuDichTruyen.AcceptChanges();
                modifyCommandPhieutruyendich();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void modifyCommandPhieutruyendich()
        {
            bool hasPTD=m_dtDataPhieuDichTruyen.Select("id_chitietdonthuoc=" + id_chitietdonthuoc).Length>0;
            cmdThemoiPTD.Enabled = Utility.isValidGrid(grdDonthuocChitiet) && !hasPTD;
            cmdSuaPTD.Enabled = Utility.isValidGrid(grdDonthuocChitiet) && hasPTD;
            cmdXoaPTD.Enabled = Utility.isValidGrid(grdDonthuocChitiet) && hasPTD;
            cmdInPTD.Enabled = Utility.isValidGrid(grdDonthuocChitiet) && hasPTD;
        }
        void frm_PhieuChamSoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void frm_PhieuChamSoc_Shown(object sender, EventArgs e)
        {
            LoadUserConfigs();
        }

        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                BindData();
            }
        }
        public frm_PhieuChamSoc(string sthamso)
        {
            InitializeComponent();

            Utility.SetVisualStyle(this);
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
            cmdExit.Click += cmdExit_Click;
            grdChamsoc.DoubleClick += GrdListChamSocDoubleClick;
            KeyDown += frm_PhieuChamSoc_KeyDown;
            txtIDPhieu.Visible = txtIDPhieuTheoDoi.Visible = globalVariables.IsAdmin;
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.NguoiTao, globalVariables.IsAdmin);
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.NgayTao, globalVariables.IsAdmin);
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.NguoiSua, globalVariables.IsAdmin);
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.NgaySua, globalVariables.IsAdmin);
            Utility.VisiableGridEx(grdChamsoc, NoitruPhieuchamsoc.Columns.Id, globalVariables.IsAdmin);
            dtNgayNhap.Value = dtNgayTheoDoi.Value = THU_VIEN_CHUNG.GetSysDateTime();
            thamso = sthamso;
        }
        void InitEvents()
        {
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkPreview1.Tag.ToString(), Utility.Bool2byte(chkPreview1.Checked));
                Utility.SaveUserConfig(chkPreview2.Tag.ToString(), Utility.Bool2byte(chkPreview2.Checked));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void LoadUserConfigs()
        {
            try
            {
                chkPreview1.Checked = Utility.getUserConfigValue(chkPreview1.Tag.ToString(), Utility.Bool2byte(chkPreview1.Checked)) == 1;
                chkPreview2.Checked = Utility.getUserConfigValue(chkPreview2.Tag.ToString(), Utility.Bool2byte(chkPreview2.Checked)) == 1;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private string MalanKam { get; set; }

        /// <summary>
        ///     hàm thực hiện việc load thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhieuChamSoc_Load(object sender, EventArgs e)
        {
            InitData();
            ucThongtinnguoibenh1.txtMaluotkham.Focus();
            ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
            MoifyCommand();
        }
        int Khoadieutri = -1;
        private void InitData()
        {
            
            DataTable dtNhanvien = THU_VIEN_CHUNG.Laydanhsachnhanvien("ALL");
            //phiếu chăm sóc
            txtbschidinh.Init(getNhanvien(dtNhanvien,"ALL"), new System.Collections.Generic.List<string> { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien });
            txtbsdovakiemtra.Init(txtbschidinh.AutoCompleteSource, txtbschidinh.defaultItem);
            txtnguoithu.Init(txtbschidinh.AutoCompleteSource, txtbschidinh.defaultItem);
            txtYta.Init(txtbschidinh.AutoCompleteSource, txtbschidinh.defaultItem);
            txtYta.SetId(globalVariables.gv_intIDNhanvien);
            ucThongtinnguoibenh1.noitrungoaitru = 1;
            ucChandoanICD1.AutoCompleteTextBox();
        }
        DataTable getNhanvien(DataTable dtNhanvien,string loai_nv)
        {
            if (loai_nv == "ALL") return dtNhanvien;
            DataTable _data = dtNhanvien.Clone();

            DataRow[] arrDr = dtNhanvien.Select(string.Format("{0}='{1}'", "ma_loainhanvien", loai_nv));
            if (arrDr.Length > 0) _data = arrDr.CopyToDataTable();
            return _data;

        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

       
     

        private void ClearControl()
        {
            foreach (Control control in grpNgayChamSoc.Controls)
            {
                if (control is EditBox)
                {
                    var txtFormantTongTien = new EditBox();

                    txtFormantTongTien = ((EditBox) (control));

                    if (txtFormantTongTien.Name != ucThongtinnguoibenh1.txtMaluotkham.Name)
                    {
                        txtFormantTongTien.Clear();
                        txtFormantTongTien.ReadOnly = false;
                    }
                }
                if (control is UIComboBox)
                {
                    var txtFormantTongTien = new UIComboBox();
                    txtFormantTongTien = ((UIComboBox) (control));
                    txtFormantTongTien.Enabled = false;
                    if (txtFormantTongTien.Items.Count > 0)
                        txtFormantTongTien.SelectedIndex = 0;
                }
            }
            txtXuTriChamSoc.Clear();
            txtDienBien.Clear();
            txtDanhGia.Clear();
            txtIDPhieu.Clear();
            dtNgayNhap.Value = THU_VIEN_CHUNG.GetSysDateTime();
            dtNgayNhap.Focus();
            MoifyCommand();
        }

        private void ClearBenhNhan()
        {
            foreach (Control control in grpThongTinBN.Controls)
            {
                if (control is EditBox)
                {
                    var txtFormantTongTien = new EditBox();

                    txtFormantTongTien = ((EditBox) (control));

                    
                }
            }
           ucThongtinnguoibenh1.txtMaluotkham.Focus();
           ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
            m_dtPhieuChamSoc.AcceptChanges();
            grdChamsoc.DataSource = null;
            MoifyCommand();
        }

        private NoitruPhanbuonggiuong objPhanbuonggiuong;
        private void BindData()
        {
            ClearBenhNhan();
            ClearControl();
            AllowSelectionChanged = false;
            if (objLuotkham != null)
            {
                LoadPhieuChamSoc();
                LoadPhieuTheoDoiChucnang();
                ucChandoanICD1.ChangePatients(ucThongtinnguoibenh1.objLuotkham, ucThongtinnguoibenh1.txtKhoanoitru.Text);
                LoadThuocTheoDoiTruyenDich();
                LoadPhieuThuPhanUngThuoc();
                LayDuLieuPhanUngThuoc(Id_phieu_thu_thuoc);
            }
            else
            {
                grdChamsoc.DataSource = null;
                grdTheodoi.DataSource = null;
                grdDonthuocChitiet.DataSource = null;
                grdListDichTruyen.DataSource = null;
            }
            AllowSelectionChanged = true;
            grdDonthuocChitiet_SelectionChanged(grdDonthuocChitiet, new EventArgs());
            MoifyCommand();
            modifyCommandPhieutruyendich();
        }

       
        /// <summary>
        ///     hàm thực hiên việc trạng thái của các nút thông tin
        /// </summary>
        private void MoifyCommand()
        {
            try
            {
                cmdXoa.Enabled = grdChamsoc.RowCount > 0 && objLuotkham!=null;
                tabTheoDoiVaChamSoc.Enabled = objLuotkham != null;
                cmdInPhieu.Enabled = cmdXoa.Enabled = grdChamsoc.RowCount > 0 && objLuotkham != null;
                cmdInPhieuTheoDoi.Enabled = cmdXoaTheoDoi.Enabled = grdTheodoi.RowCount > 0 && objLuotkham != null;
                cmdLuuLai.Enabled = cmdLuuTheoDoi.Enabled = cmdThemTheoiDoi.Enabled = cmdThemMoi.Enabled = objLuotkham != null;
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình lấy thông tin phiếu chăm sóc :{0}", exception));
                }
            }
        }

        private void LoadPhieuChamSoc()
        {
            try
            {
                _rowFilter = "1=1";
                if (!chkHienthiphieudain.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", "tthai_in", 0);
                }
                m_dtPhieuChamSoc = SPs.NoitruLayThongTinPhieuChamSoc(objLuotkham.MaLuotkham,(int) objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChamsoc, m_dtPhieuChamSoc, false, true, _rowFilter, "");
                GridEXColumn gridExColumn = grdChamsoc.RootTable.Columns[NoitruPhieuchamsoc.Columns.NgayThuchien];
                Utility.SetGridEXSortKey(grdChamsoc, gridExColumn, Janus.Windows.GridEX.SortOrder.Ascending);
                grdChamsoc.CheckAllRecords();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình lấy thông tin phiếu chăm sóc :{0}", exception));
                }
            }
        }

        private void LoadPhieuTheoDoiChucnang()
        {
            try
            {
                m_dtPhieuTheoDoiChucNang =
                    new Select().From(NoitruPhieutheodoiChucnangsong.Schema)
                        .Where(NoitruPhieutheodoiChucnangsong.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(objLuotkham.MaLuotkham))
                        .And(NoitruPhieutheodoiChucnangsong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        //.AndExpression(NoitruPhieutheodoiChucnangsong.Columns.HeathId)
                        //.IsLessThan(0)
                        //.Or(NoitruPhieutheodoiChucnangsong.Columns.HeathId)
                        //.
                        //IsNull().CloseExpression()
                        .ExecuteDataSet().Tables[0];
                _rowFilter = "1=1";
                if (!chkHienthiphieudain.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", "tthai_in", 0);
                }
                Utility.SetDataSourceForDataGridEx(grdTheodoi, m_dtPhieuTheoDoiChucNang, false, true, _rowFilter, "");
                GridEXColumn gridExColumn =
                    grdChamsoc.RootTable.Columns[NoitruPhieutheodoiChucnangsong.Columns.NgayTao];         
                Utility.SetGridEXSortKey(grdTheodoi, gridExColumn, Janus.Windows.GridEX.SortOrder.Ascending);
                grdTheodoi.CheckAllRecords();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình lấy thông tin phiếu theo dõi chức năng :{0}",
                        exception));
                }
            }
        }

        

        /// <summary>
        ///     hàm thực hiện việc phím tắt thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhieuChamSoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                if (uiTab1.SelectedIndex == 0)
                    cmdLuuLai.PerformClick();
                else if (uiTab1.SelectedIndex == 1)
                    cmdLuuTheoDoi.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                if (uiTab1.SelectedIndex == 0)
                    cmdXoa.PerformClick();
                else if (uiTab1.SelectedIndex == 1)
                    cmdXoaTheoDoi.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (uiTab1.SelectedIndex == 0)
                    cmdThemMoi.PerformClick();
                else if (uiTab1.SelectedIndex == 1)
                    cmdThemTheoiDoi.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                if (uiTab1.SelectedIndex == 0)
                    cmdInPhieu.PerformClick();
                else if (uiTab1.SelectedIndex == 1)
                    cmdInPhieuTheoDoi.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            else if ((e.Control || e.Alt) && e.KeyCode == Keys.D1)
            {
                uiTab1.SelectedIndex = 0;
            }
            else if ((e.Control || e.Alt) && e.KeyCode == Keys.D2)
            {
                uiTab1.SelectedIndex = 1;
            }
            else if ((e.Control || e.Alt) && e.KeyCode == Keys.D3)
            {
                uiTab1.SelectedIndex = 2;
            }
            else if ((e.Control || e.Alt) && e.KeyCode == Keys.D4)
            {
                uiTab1.SelectedIndex = 3;
            }
           
        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        KcbLuotkham objLuotkham = null;
        /// <summary>
        ///     hàm thực hiện việc xóa thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham==null) return;

                int idphieu = Utility.Int32Dbnull(grdChamsoc.GetValue(NoitruPhieuchamsoc.Columns.Id));
                NoitruPhieuchamsoc objChamsoc = NoitruPhieuchamsoc.FetchByID(idphieu);
                if (objChamsoc != null)
                {
                    if (Utility.AcceptQuestion("Bạn có đồng ý xóa không", "Thông báo", true))
                    {
                        Utility.EnableButton(cmdXoa, false);
                        ActionResult actionResult =
                            new PhieuChamSoc().XoaPhieuChamSoc(
                                objChamsoc);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa phiếu chăm sóc bệnh nhân: {0}, ID phiếu : {1}, ",
                               objLuotkham.MaLuotkham, idphieu), newaction.Delete, "UI");
                                grdChamsoc.CurrentRow.Delete();
                                grdChamsoc.UpdateData();
                                m_dtPhieuChamSoc.AcceptChanges();
                                // LoadPhieuChamSoc();
                                //Utility.GotoNewRowJanus(grdList, NoitruPhieuchamsoc.Columns.HeathId, Utility.sDbnull(objChamsoc.HeathId));
                                // INPHIEU_HOANKYQUI();
                                Utility.EnableButton(cmdXoa, true);
                                MoifyCommand();
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình lưu và in hóa đơn", "thông báo",
                                    MessageBoxIcon.Error);
                                Utility.EnableButton(cmdXoa, true);
                                break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg("Lỗi trong quá trình lưu và in hóa đơn :" + exception, "Thông báo");
            }
            finally
            {
                Utility.EnableButton(cmdXoa, true);
            }
        }
        /// <summary>
        ///     hà thực hiện viecj create phiếu chăm sóc
        /// </summary>
        /// <returns></returns>
        private NoitruPhieuchamsoc CreatePhieuChamSoc()
        {
            var objChamsoc = new NoitruPhieuchamsoc();
            if (Utility.Int32Dbnull(txtIDPhieu.Text) > 0)
            {
                objChamsoc.MarkOld();
                objChamsoc.IsLoaded = true;
                objChamsoc.Id = Utility.Int32Dbnull(txtIDPhieu.Text);
                objChamsoc.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                objChamsoc.NguoiSua = globalVariables.UserName;
            }
            else
            {
                objChamsoc.TthaiIn = false;
            }
            objChamsoc.MaLuotkham = objLuotkham.MaLuotkham;
            objChamsoc.IdBenhnhan = objLuotkham.IdBenhnhan;
            objChamsoc.IdNvien = Utility.Int16Dbnull(txtYta.MyID);
            if (Utility.Int32Dbnull(objChamsoc.IdNvien) <= 0)
            {
                objChamsoc.IdNvien = globalVariables.gv_intIDNhanvien;
            }
            objChamsoc.NguoiTao = globalVariables.UserName;
            objChamsoc.NgayTao = dtNgayNhap.Value;
            objChamsoc.NgayThuchien = dtNgayNhap.Value;
            objChamsoc.GioThuchien = string.Format("{0}:{1}", dtNgayNhap.Value.Hour, dtNgayNhap.Value.Minute);
            objChamsoc.IdKhoa = objLuotkham.IdKhoanoitru;
            objChamsoc.DienBien = Utility.sDbnull(txtDienBien.Text);
            objChamsoc.XuTri = Utility.sDbnull(txtXuTriChamSoc.Text);
            objChamsoc.DanhGia = Utility.sDbnull(txtDanhGia.Text);
            return objChamsoc;
        }

        private void PerformAction()
        {
            try
            {
                Utility.EnableButton(cmdLuuLai, false);
                NoitruPhieuchamsoc objChamsoc = CreatePhieuChamSoc();
                ActionResult actionResult =
                    new PhieuChamSoc().LuuPhieuChamSoc(
                        objChamsoc);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        LoadPhieuChamSoc();
                        Utility.GotoNewRowJanus(grdChamsoc, NoitruPhieuchamsoc.Columns.Id,
                            Utility.sDbnull(objChamsoc.Id));
                        // INPHIEU_HOANKYQUI();
                        Utility.EnableButton(cmdLuuLai, true);
                        cmdThemMoi.PerformClick();

                        MoifyCommand();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình lưu phiếu chăm sóc", "thông báo", MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg("Lỗi trong quá trình lưu phiếu chăm sóc :" + exception, "Thông báo");
            }
            finally
            {
                Utility.EnableButton(cmdLuuLai, true);
            }
        }

        

        /// <summary>
        ///     hàm thực hiện việc lưu thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLuuLai_Click(object sender, EventArgs e)
        {
            if (objLuotkham==null) return;
            if (!IsValidData()) return;
            PerformAction();

            MoifyCommand();
        }

        private bool IsValidData()
        {
            if (txtYta.MyID == "-1")
            {
                Utility.ShowMsg("Bạn cần nhập thông tin y tá/điều dưỡng chăm sóc");
                txtYta.Focus();
                return false;
            }
           
            if (string.IsNullOrEmpty(txtDienBien.Text))
            {
                Utility.ShowMsg("Bạn phải nhập diễn biến\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                txtDienBien.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtXuTriChamSoc.Text))
            {
                Utility.ShowMsg("Bạn phải nhập xử lý\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                txtXuTriChamSoc.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        ///     hàm thực hiện việc lưu lại thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdListChamSocDoubleClick(object sender, EventArgs e)
        {
            if (grdChamsoc.CurrentRow != null)
            {
                txtIDPhieu.Text = Utility.sDbnull(grdChamsoc.GetValue(NoitruPhieuchamsoc.Columns.Id));
                txtXuTriChamSoc.Text = Utility.sDbnull(grdChamsoc.GetValue(NoitruPhieuchamsoc.Columns.XuTri));
                txtDienBien.Text = Utility.sDbnull(grdChamsoc.GetValue(NoitruPhieuchamsoc.Columns.DienBien));
                txtDanhGia.Text = Utility.sDbnull(grdChamsoc.GetValue(NoitruPhieuchamsoc.Columns.DanhGia));
                dtNgayNhap.Value = Convert.ToDateTime(grdChamsoc.GetValue(NoitruPhieuchamsoc.Columns.NgayThuchien));
            }
        }

        /// <summary>
        ///     hàm thực hiện việc làm sạch thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLamSach_Click(object sender, EventArgs e)
        {
            ucThongtinnguoibenh1.txtMaluotkham.Clear();
            ucThongtinnguoibenh1.txtMaluotkham.Focus();
            ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
            ClearBenhNhan();
        }

       
        private void txtMaLanKham_TextChanged(object sender, EventArgs e)
        {
            MoifyCommand();
        }

        /// <summary>
        ///     hàm thực hiện việc in phiếu chăm sóc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdChamsoc.GetCheckedRows().Length <= 0)
                {
                    Utility.ShowMsg("Chưa tồn tại phiếu chăm sóc để in. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                var Heath_ID = new StringBuilder("");
                foreach (GridEXRow gridExRow in grdChamsoc.GetCheckedRows())
                {
                    Heath_ID.Append(",");
                    Heath_ID.Append(gridExRow.Cells["id"].Value.ToString());
                    gridExRow.BeginEdit();
                    gridExRow.Cells[NoitruPhieuchamsoc.Columns.TthaiIn].Value = 1;
                    gridExRow.EndEdit();
                }
                grdChamsoc.UpdateData();
                //p_PhieuDieutri.AcceptChanges();
                Utility.EnableButton(cmdInPhieu, false);
                DataTable dtData_print =
                    SPs.NoitruPhieuchamsocIn(ucThongtinnguoibenh1.txtMaluotkham.Text, (int)objLuotkham.IdBenhnhan,
                        objLuotkham.IdKhoanoitru, Heath_ID.ToString()).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(dtData_print, Application.StartupPath + @"\Xml4Reports\noitru_phieuchamsoc.XML");
                if (dtData_print.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo", MessageBoxIcon.Error);
                    return;
                }
                noitru_inphieu.InPhieutheodoi(dtData_print, chkPreview1.Checked, "noitru_phieuchamsoc", "");
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
            finally
            {
                Utility.EnableButton(cmdInPhieu, true);
            }
        }

      

        private void txtDienBien_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                txtDienBien.Focus();
            }
        }
        /// <summary>
        /// hàm thực hiệnv iệc xem chẩn đoán cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region "Code phần theo dõi chức năng sống"
        bool AllowSelectionChange = false;
        private void cmdThemTheoiDoi_Click(object sender, EventArgs e)
        {
            m_enAct = action.Insert;
            AllowSelectionChange = false;
            foreach (Control control in grpThemThongTinTheoDoi.Controls)
            {
                if (control is EditBox)
                {
                    var txtFormantTongTien = new EditBox();

                    txtFormantTongTien = ((EditBox) (control));
                    txtFormantTongTien.Clear();
                }
                if (control is MaskedEditBox)
                {
                    var txtFormantTongTien = new MaskedEditBox();

                    txtFormantTongTien = ((MaskedEditBox) (control));
                    txtFormantTongTien.Clear();
                }
            }
            dtNgayTheoDoi.Value = THU_VIEN_CHUNG.GetSysDateTime();
            dtNgayTheoDoi.Focus();
            txtIDPhieuTheoDoi.Clear();
        }

        /// <summary>
        ///     hàm thực hiện việc theo dõi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPhieuTheoDoiHienTai_DoubleClick(object sender, EventArgs e)
        {
            if (grdTheodoi.CurrentRow != null)
            {
                txtIDPhieuTheoDoi.Text =
                    Utility.sDbnull(grdTheodoi.GetValue(NoitruPhieutheodoiChucnangsong.Columns.Id));
                NoitruPhieutheodoiChucnangsong objTheodoi =
                    NoitruPhieutheodoiChucnangsong.FetchByID(Utility.Int32Dbnull(txtIDPhieuTheoDoi.Text));
                if (objTheodoi != null)
                {
                    txtHUYET_AP_DUOI.Text = Utility.sDbnull(objTheodoi.HuyetapDuoi);
                    txtHUYET_AP_TREN.Text = Utility.sDbnull(objTheodoi.HuyetapTren);
                    txtNHIP_THO.Text = Utility.sDbnull(objTheodoi.Nhiptho);
                    txtCHIEU_CAO.Text = Utility.sDbnull(objTheodoi.ChieuCao);
                    txtCAN_NANG.Text = Utility.sDbnull(objTheodoi.CanNang);
                    txtMACH.Text = Utility.sDbnull(objTheodoi.Mach);
                    dtNgayTheoDoi.Text = Utility.sDbnull(objTheodoi.NgayTao);
                    txtBMI.Text = Utility.sDbnull(objTheodoi.Bmi);
                    txtSpo2.Text = Utility.sDbnull(objTheodoi.Spo2);
                    txtNHIET_DO.Text = Utility.sDbnull(objTheodoi.NhietDo);
                    txttheodoithem.Text = Utility.sDbnull(objTheodoi.TheodoiThem);
                    txtnuoctieu.Text = Utility.sDbnull(objTheodoi.TheodoiNuoctieu);
                }
            }
        }

        /// <summary>
        ///     hàm thực hiên việc xóa thông tin theo dõi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaTheoDoi_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin theo dõi không", "thông báo", true))
            {
                XoaPhieuTheoDoi();
            }
        }

        private void XoaPhieuTheoDoi()
        {
            try
            {
                Utility.EnableButton(cmdXoaTheoDoi, false);
                int idPhieuTheoDoi =
                    Utility.Int32Dbnull(grdTheodoi.GetValue(NoitruPhieutheodoiChucnangsong.Columns.Id));
                ActionResult actionResult =
                    new PhieuChamSoc().XoaChiTietPhieuTheoDoi(
                        idPhieuTheoDoi);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa phiếu theo dõi bệnh nhân: {0}, ID phiếu : {1}, ",
                               objLuotkham.MaLuotkham, idPhieuTheoDoi), newaction.Delete, "UI");
                        grdTheodoi.CurrentRow.Delete();
                        grdTheodoi.UpdateData();
                        m_dtPhieuTheoDoiChucNang.AcceptChanges();
                        Utility.EnableButton(cmdXoaTheoDoi, true);
                        MoifyCommand();
                        cmdThemMoi.PerformClick();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình lưu phiếu theo dõi", "thông báo", MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg("Lỗi trong quá trình lưu phiếu theo dõi :" + exception, "Thông báo");
            }
            finally
            {
                Utility.EnableButton(cmdXoaTheoDoi, true);
            }
        }

        /// <summary>
        ///     hàm thực hiện viêc xủa lý thông tin lưu phiếu theo dõi chức năng sống
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLuuTheoDoi_Click(object sender, EventArgs e)
        {
            if (objLuotkham==null) return;
            PerformActionTheoDoi();
        }

        private NoitruPhieutheodoiChucnangsong CreateHeathCareDetail()
        {
            var objTheodoi = new NoitruPhieutheodoiChucnangsong();
            if (Utility.Int32Dbnull(txtIDPhieuTheoDoi.Text) > 0)
            {
                objTheodoi.MarkOld();
                objTheodoi.IsLoaded = true;
                objTheodoi.Id = Utility.Int32Dbnull(txtIDPhieuTheoDoi.Text);
            }
            else
            {
                objTheodoi.TthaiIn = false;
            }
            objTheodoi.MaLuotkham = objLuotkham.MaLuotkham;
            objTheodoi.IdBenhnhan = objLuotkham.IdBenhnhan;
            objTheodoi.IdPhieuchamsoc = -1;

            objTheodoi.Nhiptho = Utility.sDbnull(txtNHIP_THO.Text);
            objTheodoi.HuyetapDuoi = Utility.sDbnull(txtHUYET_AP_DUOI.Text);
            objTheodoi.HuyetapTren = Utility.sDbnull(txtHUYET_AP_TREN.Text);
            objTheodoi.CanNang = Utility.sDbnull(txtCAN_NANG.Text);
            objTheodoi.ChieuCao = Utility.sDbnull(txtCHIEU_CAO.Text);
            objTheodoi.Mach = Utility.sDbnull(txtMACH.Text);
            objTheodoi.NhietDo = Utility.sDbnull(txtNHIET_DO.Text);
            objTheodoi.Spo2 = Utility.sDbnull(txtSpo2.Text);
            objTheodoi.Bmi = Utility.sDbnull(txtBMI.Text);
            objTheodoi.TheodoiNuoctieu = Utility.sDbnull(txtnuoctieu.Text.Trim());
            objTheodoi.TheodoiThem = Utility.sDbnull(txttheodoithem.Text.Trim());
            objTheodoi.NguoiTao = globalVariables.UserName;
            objTheodoi.NgayTao = dtNgayTheoDoi.Value; 
            return objTheodoi;
        }

        /// <summary>
        ///     hàm thực hiện việc lưu lại thông tin của phiếu theo dõi chức năng sống
        /// </summary>
        private void PerformActionTheoDoi()
        {
            try
            {
                Utility.EnableButton(cmdLuuTheoDoi, false);
                NoitruPhieutheodoiChucnangsong objTheodoi = CreateHeathCareDetail();
                if (objTheodoi != null)
                {
                    ActionResult actionResult =
                        new PhieuChamSoc().LuuChiTietPhieuTheoDoi(
                            objTheodoi);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            //  LoadPhieuChamSoc();
                            LoadPhieuTheoDoiChucnang();
                            Utility.GotoNewRowJanus(grdTheodoi, NoitruPhieutheodoiChucnangsong.Columns.Id,
                                Utility.sDbnull(objTheodoi.Id));
                            // INPHIEU_HOANKYQUI();
                            Utility.ShowMsg("Bạn cập nhập thành công", "Thông báo");
                            Utility.EnableButton(cmdLuuTheoDoi, true);
                            MoifyCommand();
                            cmdThemTheoiDoi.PerformClick();
                            //  b_Cancel = true;
                            //this.Close();
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình lưu chi tiết thể trạng ", "thông báo",
                                MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình lưu tthe trạng chi tiết :" + exception, "Thông báo");
            }
            finally
            {
                Utility.EnableButton(cmdLuuTheoDoi, true);
            }
        }

        #endregion

        /// <summary>
        /// hàm thực hiện việc focus thông ti 
        /// theo dõi và chắm soc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabTheoDoiVaChamSoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabTheoDoiVaChamSoc.SelectedTab == tabPagePhieuChamSoc)
            {
                dtNgayNhap.Focus();
            }
            if (tabTheoDoiVaChamSoc.SelectedTab == tabPagePhieuTheoDoi)
            {
                dtNgayTheoDoi.Focus();
            }
            if (tabTheoDoiVaChamSoc.SelectedTab == tabTheoDoiTruyenDich)
            {
               
            }
            if (tabTheoDoiVaChamSoc.SelectedTab == tabPagePhieuChamSoc)
            {
                dtNgaythuputhuoc.Focus();
            }
        }

        

        private void txtXuTriChamSoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtXuTriChamSoc.Focus();
            }
        }

        private void cmdInPhieuTheoDoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdTheodoi.GetCheckedRows().Length <= 0)
                {
                    Utility.ShowMsg("Chưa tồn tại phiếu theo dõi để in. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                var HeathDetail_ID = new StringBuilder("-1");
                foreach (GridEXRow gridExRow in grdTheodoi.GetCheckedRows())
                {
                    HeathDetail_ID.Append(",");
                    HeathDetail_ID.Append(gridExRow.Cells["ID"].Value.ToString());
                    gridExRow.BeginEdit();
                    gridExRow.Cells[NoitruPhieutheodoiChucnangsong.Columns.TthaiIn].Value = 1;
                    gridExRow.EndEdit();
                }
                grdTheodoi.UpdateData();
              //  p_PhieuDieutri.AcceptChanges();
                Utility.EnableButton(cmdInPhieuTheoDoi, false);
                DataTable dt_dataprint =
                    SPs.NoitruPhieutheodoiIn(ucThongtinnguoibenh1.txtMaluotkham.Text,(int) objLuotkham.IdBenhnhan, HeathDetail_ID.ToString()
                        ).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(dt_dataprint, Application.StartupPath + @"\Xml4Reports\noitru_phieutheodoi.XML");
                if (dt_dataprint.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy thông tin phiếu theo dõi để in. Vui lòng chọn một phiếu trên danh sách phiếu theo dõi trước khi thực hiện nút in", "Thông báo", MessageBoxIcon.Error);
                    return;
                }
                noitru_inphieu.InPhieutheodoi(dt_dataprint,chkPreview2.Checked,"noitru_phieutheodoi","");
                Utility.EnableButton(cmdInPhieu, true);
            }
            catch (Exception exception)
            {
               Utility.ShowMsg(exception.Message);
            }
            finally
            {
                Utility.EnableButton(cmdInPhieuTheoDoi, true);
            }
        }

        private void txtCHIEU_CAO_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCAN_NANG.Focus();
                if (!string.IsNullOrEmpty(txtCAN_NANG.Text) && !string.IsNullOrEmpty(txtCHIEU_CAO.Text))
                {
                    decimal cannang = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal( txtCAN_NANG.Text),0);
                    decimal chieucao = Utility.DecimaltoDbnull(txtCHIEU_CAO.Text);
                    decimal bmi = Utility.DecimaltoDbnull(cannang / ((chieucao / 100) * (chieucao / 100)));
                    txtBMI.Text = bmi.ToString("0.00").Replace(".00", String.Empty);
                }
            }
        }

        private void txtCAN_NANG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (!string.IsNullOrEmpty(txtCAN_NANG.Text) && !string.IsNullOrEmpty(txtCHIEU_CAO.Text))
                {
                    decimal cannang = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCAN_NANG.Text),0);
                    decimal chieucao = Utility.DecimaltoDbnull(txtCHIEU_CAO.Text);
                    decimal bmi = Utility.DecimaltoDbnull(cannang / ((chieucao / 100) * (chieucao / 100)));
                    txtBMI.Text = bmi.ToString("0.00").Replace(".00", String.Empty);
                }
            }
        }

        private void txtCHIEU_CAO_Leave(object sender, EventArgs e)
        {
            
                if (!string.IsNullOrEmpty(txtCAN_NANG.Text) && !string.IsNullOrEmpty(txtCHIEU_CAO.Text))
                {
                    decimal cannang = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCAN_NANG.Text),0);
                    decimal chieucao = Utility.DecimaltoDbnull(txtCHIEU_CAO.Text);
                    decimal bmi = Utility.DecimaltoDbnull(cannang / ((chieucao / 100) * (chieucao / 100)));
                    txtBMI.Text = bmi.ToString("0.00").Replace(".00", String.Empty);
                }
            
        }

        private void txtCAN_NANG_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtCAN_NANG.Text))
            {
                decimal cannang = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCAN_NANG.Text), 0);
                decimal chieucao = Utility.DecimaltoDbnull(txtCHIEU_CAO.Text);
                decimal bmi = Utility.DecimaltoDbnull(cannang / ((chieucao / 100) * (chieucao / 100)));
                txtBMI.Text = bmi.ToString("0.00").Replace(".00", String.Empty);
            }
        }

        private void chkHienthiphieudain_CheckedChanged(object sender, EventArgs e)
        {
            //m_dtPhieuChamSoc
            //m_dtPhieuTheoDoiChucNang
            try
            {
                LoadPhieuChamSoc();
                LoadPhieuTheoDoiChucnang();
                ucChandoanICD1.ChangePatients(ucThongtinnguoibenh1.objLuotkham, ucThongtinnguoibenh1.txtKhoanoitru.Text);
                LoadThuocTheoDoiTruyenDich();
                LoadPhieuThuPhanUngThuoc();


                _rowFilter = "1=1";
                if (!chkHienthiphieudain.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", "tthai_in", 0);
                }
                m_dtPhieuTheoDoiChucNang.DefaultView.RowFilter = _rowFilter;
                m_dtPhieuChamSoc.DefaultView.RowFilter = _rowFilter;
                m_dtPhieuTheoDoiTruyenDich.DefaultView.RowFilter = _rowFilter;
                m_dNoitruPhieuthuphanungthuoc.DefaultView.RowFilter = _rowFilter;
                m_dtPhieuChamSoc.AcceptChanges();
                m_dtPhieuTheoDoiChucNang.AcceptChanges();
                m_dtPhieuTheoDoiTruyenDich.AcceptChanges();
                m_dNoitruPhieuthuphanungthuoc.AcceptChanges();
                grdChamsoc.CheckAllRecords();
                grdTheodoi.CheckAllRecords();
                grdPhieuThuPhanUngThuoc.CheckAllRecords();
                grdListDichTruyen.CheckAllRecords();
                MoifyCommand();

                }
            catch (Exception)
            {
                //throw;
            }
        }

        private void LoadThuocTheoDoiTruyenDich()
        {
            try
            {
                _rowFilter = "1=1";
                if (!chkHienthiphieudain.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", "trangthai_in", 0);
                }
                m_dtPhieuTheoDoiTruyenDich = SPs.NoitruLayThongTinThuocTruyenDich(objLuotkham.MaLuotkham,(int) objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDonthuocChitiet, m_dtPhieuTheoDoiTruyenDich, false, true, "1=1", "");
                //GridEXColumn gridExColumn = grdDonthuocChitiet.RootTable.Columns["NgayLapPhieu"];
                //Utility.SetGridEXSortKey(grdDonthuocChitiet, gridExColumn, Janus.Windows.GridEX.SortOrder.Ascending);
              
                LoadPhieuTheoDoiTruyenDich(-1);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void LoadPhieuTheoDoiTruyenDich(int id_thuoc)
        {
            try
            {
                m_dtDataPhieuDichTruyen = SPs.NoitruPhieutruyendichLaydanhsach(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdKhoanoitru, id_thuoc).GetDataSet().Tables[0];
                _rowFilter = "1=1";
                if (!chkHienthiphieudain.Checked)
                {
                    //  _rowFilter = string.Format("{0}={1}", "id_chitietdonthuoc", id_chitietdonthuoc);
                    _rowFilter = string.Format("{0}={1}", "trangthai_in", 0);
                }
                m_dtDataPhieuDichTruyen.DefaultView.RowFilter = _rowFilter;
                m_dtDataPhieuDichTruyen.AcceptChanges();
                //  grdListDichTruyen.DataSource = m_dtDataPhieuDichTruyen;
                Utility.SetDataSourceForDataGridEx(grdListDichTruyen, m_dtDataPhieuDichTruyen, false, true, _rowFilter, "");
                grdListDichTruyen.CheckAllRecords();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình lấy thông tin phiếu chăm sóc :{0}", exception));
                }
            }
        }
        private void cmdThemoiPTD_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdDonthuocChitiet))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một thuốc để thêm mới phiếu truyền dịch");
                    return;
                }
                frm_themphieutruyendich frm = new frm_themphieutruyendich();
                frm.em_Action = action.Insert;
                frm.p_DataPhieuDich = m_dtDataPhieuDichTruyen;
                frm.id_chitietdonthuoc = id_chitietdonthuoc;
                frm.id_donthuoc = id_donthuoc;
                frm.id_thuoc = id_thuoc;
                frm.grdList = grdListDichTruyen;
                frm.SoLuong = soluong;
                frm.Id_ThuocKho = idthuockho;
                frm.TenThuoc = tenthuoc;
                frm.solo = solo;
                frm.txtID.Text = "-1";
                frm.Doctor_ID = doctorid;
                frm.id_BG = Patientdeptid;
                frm.id_khoadieutri = id_khoadieutri;
                frm.objLuotkham = objLuotkham;
                frm.ShowDialog();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi :{0}", exception));
                }
                _log.Trace(exception);
            }
        }

        private void cmdSuaPTD_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdListDichTruyen.CurrentRow != null)
                {
                    frm_themphieutruyendich frm = new frm_themphieutruyendich();
                    NoitruPhieudichtruyen objPhieuDichTruyen = NoitruPhieudichtruyen.FetchByID(Utility.Int32Dbnull(grdListDichTruyen.GetValue("id_phieu"), -1));
                    if (objPhieuDichTruyen != null)
                    {
                        KcbDonthuocChitiet objDonthuocchitiet = KcbDonthuocChitiet.FetchByID(Utility.Int32Dbnull(objPhieuDichTruyen.IdChitietdonthuoc));
                        if (objDonthuocchitiet != null)
                        {

                            frm.em_Action = action.Update;
                            frm.txtID.Text = objPhieuDichTruyen.IdPhieu.ToString();
                            frm.p_DataPhieuDich = m_dtDataPhieuDichTruyen;
                            frm.id_chitietdonthuoc = id_chitietdonthuoc;
                            frm.id_donthuoc = id_donthuoc;
                            frm.id_thuoc = id_thuoc;
                            frm.grdList = grdListDichTruyen;
                            frm.SoLuong = soluong;
                            frm.Id_ThuocKho = idthuockho;
                            frm.TenThuoc = tenthuoc;
                            frm.solo = solo;
                            frm.Doctor_ID = doctorid;
                            frm.id_BG = Patientdeptid;
                            frm.objPhieuDichTruyen = objPhieuDichTruyen;
                            frm.id_khoadieutri = id_khoadieutri;
                            frm.objLuotkham = objLuotkham;
                            frm.ShowDialog();
                        }

                    }
                    else
                    {
                        Utility.ShowMsg("Phiếu truyền dịch bạn vừa chọn sửa có thể đã bị người khác xóa mất. Vui lòng kiểm tra lại");
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdXoaPTD_Click(object sender, EventArgs e)
        {
            try
            {
                int idPhieu_xoa = Utility.Int32Dbnull(grdListDichTruyen.GetValue("id_phieu"), -1);
                string TenThuoc_Xoa = Utility.sDbnull(grdListDichTruyen.GetValue("ten_thuoc"));



                if (Utility.AcceptQuestion("Bạn có muốn thực hiện xóa phiếu truyền dịch đang chọn không", "Thông báo", true))
                {
                    NoitruPhieudichtruyen objPhieuDichTruyen = NoitruPhieudichtruyen.FetchByID(idPhieu_xoa);
                    if (objPhieuDichTruyen != null)
                    {

                        if (new Delete().From(NoitruPhieudichtruyen.Schema)
                                 .Where(NoitruPhieudichtruyen.Columns.IdPhieu).IsEqualTo(idPhieu_xoa).Execute() > 0)
                        {
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa phiếu truyền dịch bệnh nhân: {0}, Tên thuốc:  {1}, ID phiếu truyền dịch : {2}, IdChitietdonthuoc : {3}",
                              objLuotkham.MaLuotkham, TenThuoc_Xoa, idPhieu_xoa, objPhieuDichTruyen.IdChitietdonthuoc), newaction.Delete, "UI");
                            grdListDichTruyen.CurrentRow.Delete();
                            grdListDichTruyen.UpdateData();
                            grdListDichTruyen.Refresh();
                        }
                    }

                }
                m_dtDataPhieuDichTruyen.AcceptChanges();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                _log.Trace(exception);
            }
        }

        private void cmdInPTD_Click(object sender, EventArgs e)
        {
            try
            {

                if (grdListDichTruyen.CurrentRow != null)
                {
                    string ID_Phieu =  string.Join(",", (from p in grdListDichTruyen.GetCheckedRows() select p.Cells["id_phieu"].Value.ToString()).Distinct().ToArray<string>());
                    string id_thuoc =  string.Join(",", (from p in grdListDichTruyen.GetCheckedRows() select p.Cells["id_thuoc"].Value.ToString()).Distinct().ToArray<string>());

                    foreach (GridEXRow gridExRow in grdListDichTruyen.GetCheckedRows())
                    {
                        gridExRow.BeginEdit();
                        gridExRow.Cells[NoitruPhieudichtruyen.Columns.TrangthaiIn].Value = 1;
                        gridExRow.EndEdit();
                    }
                    grdListDichTruyen.UpdateData();
                    DataTable dt_dataprint = SPs.NoitruPhieutruyendichLaydulieuinphieu(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, id_thuoc, ID_Phieu).GetDataSet().Tables[0];

                    if (m_dtDataPhieuDichTruyen.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("Không tìm thấy dữ liệu để in", "Thông báo");
                        return;
                    }
                    THU_VIEN_CHUNG.CreateXML(dt_dataprint, Application.StartupPath + @"\Xml4Reports\noitru_phieutruyendich.XML");
                    if (dt_dataprint.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("Không tìm thấy thông tin phiếu truyền dịch để in. Vui lòng chọn một phiếu trên danh sách phiếu truyền dịch trước khi nhấn nút in", "Thông báo", MessageBoxIcon.Error);
                        return;
                    }
                    noitru_inphieu.InPhieutheodoi(dt_dataprint, chkPreview2.Checked, "noitru_phieutruyendich", "");
                }

            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                _log.Trace(exception);
            }
        }
        private void cmdThemMoiPhieuTheoDoiTruyenDich_Click(object sender, EventArgs e)
        {
            
        }

        private void cmdSuaPhieuTheoDoiTruyenDich_Click(object sender, EventArgs e)
        {
           
        }


        private void cmdInPhieuTheoDoiTruyenDich_Click(object sender, EventArgs e)
        {
           
        }

        private void XoaPhieuTheoDoiTruyenDich_Click(object sender, EventArgs e)
        {
            
        }

        private void uiGroupBox6_Click(object sender, EventArgs e)
        {

        }
        private void LoadPhieuThuPhanUngThuoc()
        {
            try
            {
                _rowFilter = "1=1";
                if (!chkHienthiphieudain.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", "tthai_in", 0);
                }
                m_dNoitruPhieuthuphanungthuoc = SPs.NoitruLayThongTinPhieuThuPhanUngThuoc(objLuotkham.MaLuotkham,(int) objLuotkham.IdBenhnhan, -1, -1).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPhieuThuPhanUngThuoc, m_dNoitruPhieuthuphanungthuoc, false, true, _rowFilter, "");
                GridEXColumn gridExColumn = grdPhieuThuPhanUngThuoc.RootTable.Columns["NgayLapPhieu"];
                Utility.SetGridEXSortKey(grdPhieuThuPhanUngThuoc, gridExColumn, Janus.Windows.GridEX.SortOrder.Ascending);
                grdPhieuThuPhanUngThuoc.CheckAllRecords();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình lấy thông tin phiếu chăm sóc :{0}", exception));
                }
            }
        }

        private void grdPhieuThuPhanUngThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (grdPhieuThuPhanUngThuoc.CurrentRow != null)
            {
                Id_phieu_thu_thuoc =
                  Utility.Int32Dbnull(grdPhieuThuPhanUngThuoc.GetValue(NoitruPhieuthuphanungthuoc.Columns.IdPhieu));
                id_chitietdonthuoc_Thuoc_Thu =
                  Utility.Int32Dbnull(grdPhieuThuPhanUngThuoc.GetValue(NoitruPhieuthuphanungthuoc.Columns.IdChitietdonthuoc));
                LayDuLieuPhanUngThuoc(Id_phieu_thu_thuoc);
            }
            
        }
        private void LayDuLieuPhanUngThuoc(int Id_phieu_thu_thuoc)
        {
            if (id_chitietdonthuoc_Thuoc_Thu > 0)
            {
                DataTable m_dNoitruPhieuthuphanungthuoc_Id_phieu_thu_thuoc = SPs.NoitruLayThongTinPhieuThuPhanUngThuoc(objLuotkham.MaLuotkham, -1, (int)objLuotkham.IdBenhnhan, Id_phieu_thu_thuoc).GetDataSet().Tables[0];
                if (m_dNoitruPhieuthuphanungthuoc_Id_phieu_thu_thuoc.Rows.Count > 0)
                {
                    DataRow dr = m_dNoitruPhieuthuphanungthuoc_Id_phieu_thu_thuoc.Rows[0];
                    txtPresDetail_ID.Text = Utility.sDbnull(dr["id_chitietdonthuoc"].ToString());
                    txtidphieuthu.Text = Utility.sDbnull(dr["id_phieu"].ToString());
                    txtbschidinh.SetId(dr["id_bsi_chidinh"].ToString());
                    txtbsdovakiemtra.SetId(dr["id_bsi_kiemtra"].ToString());
                    txtnguoithu.SetId(dr["nguoi_thu"].ToString());
                    dtNgaythuputhuoc.Text = Utility.sDbnull(dr["ngay_thien"].ToString());
                    dtNgaydockq.Text = Utility.sDbnull(dr["ngay_kiemtra"].ToString());
                    txtTenThuoc.Text = Utility.sDbnull(dr["ten_thuoc"].ToString());
                    txtphuongphapthu.SetCode( Utility.sDbnull(dr["ma_phuongphapthu"].ToString()));
                }
            }
        }
        
        private void cmdThemMoiPhieuThuPhanUngThuoc_Click(object sender, EventArgs e)
        {
            try
            {

            dtNgaythuputhuoc.Value = THU_VIEN_CHUNG.GetSysDateTime();
            dtNgaydockq.Value = THU_VIEN_CHUNG.GetSysDateTime();
            dtNgayTheoDoi.Focus();
            txtIDPhieuTheoDoi.Clear();
            txtTenThuoc.Clear();
            txtPresDetail_ID.Clear();
            txtphuongphapthu.Clear();
            txtbschidinh.Clear();
            txtbsdovakiemtra.Clear();
            txtnguoithu.Clear();
            Id_phieu_thu_thuoc = -1;

            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                _log.Trace(exception);
            }
        }

        private void cmdLuuPhieuThuPhanUngThuoc_Click(object sender, EventArgs e)
        {
            try
            {

           if (objLuotkham==null) return;
           if (string.IsNullOrEmpty(txtPresDetail_ID.Text))
                {
                    Utility.ShowMsg("Chưa chọn thuốc");
                    return;
                }
            PerformActionPhieuThuPhanUngThuoc();

            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                _log.Trace(exception);
            }
        }
        private void PerformActionPhieuThuPhanUngThuoc()
        {
            try
            {
                Utility.EnableButton(cmdLuuPhieuThuPhanUngThuoc, false);
                NoitruPhieuthuphanungthuoc objPhieuThuPhanUngThuoc = CreatePhieuThuPhanUngThuoc();
                if (objPhieuThuPhanUngThuoc != null)
                {
                    ActionResult actionResult =
                        new PhieuChamSoc().LuuChiTieNoitruPhieuthuphanungthuoc(objPhieuThuPhanUngThuoc);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            LoadPhieuThuPhanUngThuoc();
                            Utility.GotoNewRowJanus(grdPhieuThuPhanUngThuoc, NoitruPhieuthuphanungthuoc.Columns.IdPhieu,Utility.sDbnull(objPhieuThuPhanUngThuoc.IdPhieu));
                            Utility.ShowMsg("Bạn cập nhập thành công", "Thông báo");
                            Utility.EnableButton(cmdLuuPhieuThuPhanUngThuoc, true);
                            MoifyCommand();
                            cmdThemMoiPhieuThuPhanUngThuoc.PerformClick();
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình lưu chi tiết phiếu thử phản ứng thuốc ", "thông báo",
                                MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình lưu chi tiết phiếu thử phản ứng thuốc :" + exception, "Thông báo");
            }
            finally
            {
                Utility.EnableButton(cmdLuuTheoDoi, true);
            }
        }
        private NoitruPhieuthuphanungthuoc CreatePhieuThuPhanUngThuoc()
        {
            var objPhieuThuPhanUngThuoc = new NoitruPhieuthuphanungthuoc();
            if (Utility.Int32Dbnull(Id_phieu_thu_thuoc) > 0)
            {
                objPhieuThuPhanUngThuoc.MarkOld();
                objPhieuThuPhanUngThuoc.IsLoaded = true;
                objPhieuThuPhanUngThuoc.IdPhieu = Utility.Int32Dbnull(Id_phieu_thu_thuoc);
                objPhieuThuPhanUngThuoc.NguoiSua = globalVariables.UserName;
                objPhieuThuPhanUngThuoc.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPhieuThuPhanUngThuoc.TrangthaiIn = false;
                objPhieuThuPhanUngThuoc.NguoiTao = globalVariables.UserName;
                objPhieuThuPhanUngThuoc.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPhieuThuPhanUngThuoc.MaLuotkham = objLuotkham.MaLuotkham;
            objPhieuThuPhanUngThuoc.IdBenhnhan = objLuotkham.IdBenhnhan;


            objPhieuThuPhanUngThuoc.IdBuongGiuong = PatientDept_ID_phieu_thu_thuoc;
            objPhieuThuPhanUngThuoc.IdKhoanoitru = Department_ID_phieu_thu_thuoc ;
            objPhieuThuPhanUngThuoc.IdBsiChidinh = Utility.Int32Dbnull(txtbschidinh.MyID,-1);
            objPhieuThuPhanUngThuoc.IdBsiKiemtra = Utility.Int32Dbnull(txtbsdovakiemtra.MyID, -1);
            objPhieuThuPhanUngThuoc.IdNvienThu = Utility.Int32Dbnull(txtnguoithu.MyID, -1);
            objPhieuThuPhanUngThuoc.NgayThien = dtNgaythuputhuoc.Value;
            objPhieuThuPhanUngThuoc.NgayKiemtra = dtNgaydockq.Value;
            objPhieuThuPhanUngThuoc.IdChitietdonthuoc = Utility.Int64Dbnull(txtPresDetail_ID.Text);
            objPhieuThuPhanUngThuoc.MaPhuongphapthu = Utility.sDbnull(txtphuongphapthu.Text);
            
            return objPhieuThuPhanUngThuoc;
        }

        private void cmdChonthuoc_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtPatient_ID.Text))
            //{
            //    frm_PHIEU_DIEUTRI_NOITRU frm = new frm_PHIEU_DIEUTRI_NOITRU("DIEUTRI");
            //    frm.b_CallParent = true;
            //    frm.txtPID.Text = ucThongtinnguoibenh1.txtMaluotkham.Text;
            //    frm.txtPID.Enabled = false;
            //    frm.ShowDialog();
            //    if (frm.b_Cancel)
            //    {
            //        cmdThemMoiPhieuThuPhanUngThuoc.PerformClick();
            //        txtid_chitietdonthuoc.Text = Utility.sDbnull(frm.nt_id_chitietdonthuoc);
            //        txtidphieuthu.Text = Utility.sDbnull(frm.nt_Id_phieu_thu_thuoc);                   
            //        txtTenThuoc.Text = Utility.sDbnull(frm.nt_TenThuoc);
            //        Id_phieu_thu_thuoc = Utility.Int32Dbnull(frm.nt_Id_phieu_thu_thuoc);
            //        Department_ID_phieu_thu_thuoc = Utility.Int32Dbnull(frm.nt_Department_ID_phieu_thu_thuoc);
            //        PatientDept_ID_phieu_thu_thuoc = Utility.Int32Dbnull(frm.nt_PatientDept_ID_phieu_thu_thuoc);
            //        txtbschidinh.SetCode(frm.nt_bschidinh);
            //    }

            //}
        }

        private void cmdXoaPhieuThuPhanUngThuoc_Click(object sender, EventArgs e)
        {
            try
            {     
            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin theo dõi không", "thông báo", true))
            {
                XoaPhieuThuPhanUngThuoc();
            }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                _log.Trace(exception);
            }
        }
        private void XoaPhieuThuPhanUngThuoc()
        {
            try
            {
                Utility.EnableButton(cmdXoaPhieuThuPhanUngThuoc, false);
                int idPhieuThu = Utility.Int32Dbnull(grdPhieuThuPhanUngThuoc.GetValue(NoitruPhieuthuphanungthuoc.Columns.IdPhieu));
                string TenThuoc_Xoa = Utility.sDbnull(grdPhieuThuPhanUngThuoc.GetValue("Drug_Name"));
                int id_chitietdonthuoc_Xoa = Utility.Int32Dbnull(grdPhieuThuPhanUngThuoc.GetValue(NoitruPhieuthuphanungthuoc.Columns.IdChitietdonthuoc));
                ActionResult actionResult = new PhieuChamSoc().XoaChiTieNoitruPhieuthuphanungthuoc(idPhieuThu);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa phiếu thử phản ứng thuốc, Tên thuốc:  {0}, số phiếu id_chitietdonthuoc:  {1}, ID Phiếu thử : {2}",
                              TenThuoc_Xoa, id_chitietdonthuoc_Xoa, idPhieuThu), newaction.Delete, "UI");
                        grdPhieuThuPhanUngThuoc.CurrentRow.Delete();
                        grdPhieuThuPhanUngThuoc.UpdateData();
                        m_dNoitruPhieuthuphanungthuoc.AcceptChanges();
                        Utility.EnableButton(cmdXoaPhieuThuPhanUngThuoc, true);
                        MoifyCommand();
                        cmdThemMoiPhieuThuPhanUngThuoc.PerformClick();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình xóa phiếu thử phản ứng thuốc", "thông báo", MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg("Lỗi trong quá trình xóa phiếu thử phản ứng thuốc :" + exception, "Thông báo");
                _log.Trace(exception);
            }
            finally
            {
                Utility.EnableButton(cmdXoaPhieuThuPhanUngThuoc, true);
            }
        }

        private void cmdInPhieuThuPhanUngThuoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdPhieuThuPhanUngThuoc.GetCheckedRows().Length <= 0)
                {
                    Utility.ShowMsg("bạn chưa chọn phiếu thử phản ứng thuốc để in ", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                var id_phieu_thu = new StringBuilder("-1");
                foreach (GridEXRow gridExRow in grdPhieuThuPhanUngThuoc.GetCheckedRows())
                {
                    id_phieu_thu.Append(",");
                    id_phieu_thu.Append(gridExRow.Cells["ID_PHIEU_THU"].Value.ToString());
                    gridExRow.BeginEdit();
                    gridExRow.Cells[NoitruPhieuthuphanungthuoc.Columns.TrangthaiIn].Value = 1;
                    gridExRow.EndEdit();
                }
                grdPhieuThuPhanUngThuoc.UpdateData();
                Utility.EnableButton(cmdInPhieuThuPhanUngThuoc, false);
                DataTable mInPhieuThuPhanUngThuoc =
                    SPs.NoitruInphieuThuPhanUngThuoc(ucThongtinnguoibenh1.txtMaluotkham.Text,(int) objLuotkham.IdBenhnhan, id_phieu_thu.ToString()
                        ).GetDataSet().Tables[0];
                if (mInPhieuThuPhanUngThuoc.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo", MessageBoxIcon.Error);
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(mInPhieuThuPhanUngThuoc, "nPhieuThuPhanUngThuoc.XML");
               // VietBaIT.HISLink.Business.Reports.Implementation.InPhieuChamSoc.INPHIEU_THU_PHAN_UNG_THUOC(mInPhieuThuPhanUngThuoc);
                Utility.EnableButton(cmdInPhieuThuPhanUngThuoc, true);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                _log.Trace(exception);
            }
            finally
            {
                Utility.EnableButton(cmdInPhieuTheoDoi, true);
            }
        }

        private void cmdLineChart_Click(object sender, EventArgs e)
        {
            try
            {
               
                DataTable dtData = new DataTable();
                dtData.Columns.AddRange(new DataColumn[] { new DataColumn("mach", typeof(decimal)), new DataColumn("nhiet_do", typeof(decimal)), new DataColumn("ngay", typeof(DateTime)) });
                foreach (GridEXRow gridExRow in grdTheodoi.GetCheckedRows())
                {
                    DataRow _newRow = dtData.NewRow();
                    _newRow["mach"] = Utility.DecimaltoDbnull(gridExRow.Cells["mach"].Value, 0m);
                    _newRow["nhiet_do"] = Utility.DecimaltoDbnull(gridExRow.Cells["nhiet_do"].Value, 0m);
                    _newRow["ngay"] = Convert.ToDateTime(gridExRow.Cells["ngay_tao"].Value);
                    dtData.Rows.Add(_newRow);
                }
                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Chưa có dữ liệu để in", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(dtData, "PhieuTheoDoiChucNang.XML");
                noitru_inphieu.InPhieutheodoi(dtData, chkPreview2.Checked, "noitru_bieudotheodoichucnang", "");
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                _log.Trace(exception);
            }
            finally
            {
                Utility.EnableButton(cmdInPhieuTheoDoi, true);
            }
        }
      
       
    }
}