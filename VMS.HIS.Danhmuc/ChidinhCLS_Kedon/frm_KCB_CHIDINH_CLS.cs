using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using SubSonic.Sugar;
using VNS.HIS.UI.Classess;
using VNS.Libs;
using VMS.HIS.DAL;
using SortOrder = Janus.Windows.GridEX.SortOrder;
using Strings = Microsoft.VisualBasic.Strings;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.BusRule.Goikham;
using VNS.HIS.UI.DANHMUC;

namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_KCB_CHIDINH_CLS : Form
    {
        private readonly Logger log;
        private int CurrentRowIndex = -1;
        public int Exam_ID = -1;
        decimal BHYT_PTRAM_TRAITUYENNOITRU=0;
        private string Help = "";
        KCB_CHIDINH_CANLAMSANG CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        KCB_THAMKHAM __THAMKHAM = new KCB_THAMKHAM();
        public byte noitru = 0;
        private int ServiceDetail_Id;
        bool m_blnAllowSelectionChanged = false;
        private ActionResult actionResult = ActionResult.Error;
        public bool m_blnCancel=true;
        public bool AutoAddAfterCheck = false;
        public byte tnv_chidinh = 0;
        private bool isSaved;
        private int lastIndex;
        private char lastKey;
        private DataTable m_dtChitietPhieuCLS = new DataTable();
        private DataTable m_dtReport = new DataTable();
        public DataTable m_dtDanhsachDichvuCLS = new DataTable();
        public DataTable m_dtDanhsachDichvuCLS_org = new DataTable();
        public action m_eAction = action.Insert;
        private bool neverFound = true;
        public KcbLuotkham objLuotkham;
        public KcbDanhsachBenhnhan objBenhnhan;
        public DataTable p_AssignInfo;
        private string rowFilter = "1=1";
        private string strQuestion = "";
        private int v_AssignId = -1;
        string nhomchidinh = "";
        byte kieu_chidinh = 0;
        
        #region "khai báo khởi tạo ban đầu"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nhomchidinh">Mô tả thêm của nhóm chỉ định CLS dùng cho việc tách form kê chỉ định CLS và kê gói dịch vụ</param>
        public frm_KCB_CHIDINH_CLS(string nhomchidinh, byte kieu_chidinh)
        {
            log = LogManager.GetLogger("CHIDINH_CLS_LOGS");
            log.Trace("Begin......................................................................................");
            InitializeComponent();
            Utility.SetVisualStyle(this);
            log.Trace("Initialized.");
            InitEvents();
            this.nhomchidinh = nhomchidinh;
            this.kieu_chidinh = kieu_chidinh;
            txtFilterName.Focus();
            dtRegDate.Value = globalVariables.SysDate;
            dtRegDate.ReadOnly = !globalVariables.IsAdmin;
            //txtidchandoan.Visible = globalVariables.IsAdmin;
            //txtidkham.Visible = globalVariables.IsAdmin;
            //chkChiDinhNhanh.Visible = globalVariables.IsAdmin;
            //grpPhieuChiDinh.Enabled = globalVariables.IsAdmin;
            if (globalVariables.gv_UserAcceptDeleted) FormatUserNhapChiDinh();
            CauHinh();
        }

        void InitEvents()
        {
            Load += new EventHandler(frm_KCB_CHIDINH_CLS_Load);
            KeyDown += new KeyEventHandler(frm_KCB_CHIDINH_CLS_KeyDown);
            FormClosing += new FormClosingEventHandler(frm_KCB_CHIDINH_CLS_FormClosing);
            txtChanDoan.LostFocus += txtChanDoan_LostFocus;
            grdDichvuCLS.CellValueChanged += new ColumnActionEventHandler(grdServiceDetail_CellValueChanged);
            grdDichvuCLS.Enter += new EventHandler(grdServiceDetail_Enter);
            grdDichvuCLS.KeyDown += new KeyEventHandler(grdServiceDetail_KeyDown);
            grdDichvuCLS.KeyPress += new KeyPressEventHandler(grdServiceDetail_KeyPress);
            grdDichvuCLS.SelectionChanged += new EventHandler(grdServiceDetail_SelectionChanged);
            grdDichvuCLS.UpdatingCell += new UpdatingCellEventHandler(grdServiceDetail_UpdatingCell);

            grdChitietChidinhCLS.CellUpdated += new ColumnActionEventHandler(grdAssignDetail_CellUpdated);
            grdChitietChidinhCLS.CellValueChanged += new ColumnActionEventHandler(grdAssignDetail_CellValueChanged);
            grdChitietChidinhCLS.ColumnHeaderClick += new ColumnActionEventHandler(grdAssignDetail_ColumnHeaderClick);
            grdChitietChidinhCLS.FormattingRow += new RowLoadEventHandler(grdAssignDetail_FormattingRow);
            grdChitietChidinhCLS.UpdatingCell += new UpdatingCellEventHandler(grdAssignDetail_UpdatingCell);
            grdChitietChidinhCLS.KeyDown += new KeyEventHandler(grdAssignDetail_KeyDown);
            cboDichVu.SelectedIndexChanged += new EventHandler(cboDichVu_SelectedIndexChanged);
            txtFilterName.KeyDown += new KeyEventHandler(txtFilterName_KeyDown);
            txtFilterName.TextChanged += new EventHandler(txtFilterName_TextChanged);
            cmdAddDetail.Click += new EventHandler(cmdAddDetail_Click);
            cmdCauHinh.Click += new EventHandler(cmdCauHinh_Click);

            chkChiDinhNhanh.CheckedChanged += new EventHandler(chkChiDinhNhanh_CheckedChanged);

            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdInPhieuCLS.Click += new EventHandler(cmdInPhieuCLS_Click);
            cmdDelete.Click += new EventHandler(cmdDelete_Click);
            mnuDelete.Click += new EventHandler(mnuDelete_Click);
            cmdTaonhom.Click += cmdTaonhom_Click;
            cmdAccept.Click += cmdAccept_Click;
            txtNhomDichvuCLS._OnEnterMe += txtNhomDichvuCLS__OnEnterMe;
            mnuUpdateprice.Click += mnuUpdateprice_Click;
            mnuSaveAs.Click += mnuSaveAs_Click;
            cmdIntachPhieu.Click += cmdIntachPhieu_Click;
            grdChitietChidinhCLS.RootTable.Columns["don_gia"].EditType = Utility.Coquyen("chidinhcls_quyen_suadongia") ? EditType.TextBox : EditType.NoEdit;
            uiTab1.SelectedTabChanged += uiTab1_SelectedTabChanged;
            cboGoi.KeyDown += CboGoi_KeyDown;
            cboDoituongKcb.SelectedIndexChanged += CboDoituongKcb_SelectedIndexChanged;
            
        }
        bool Allow_ChongiaKCB = false;
        private void CboDoituongKcb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Allow_ChongiaKCB || objLuotkham == null) return;
                string ma_doituongkcb = Utility.sDbnull(cboDoituongKcb.SelectedValue);
                if (objLuotkham.MaDoituongKcb != "BHYT" && ma_doituongkcb == "BHYT")
                {
                    Utility.ShowMsg(string.Format("Không được phép áp giá BHYT cho đối tượng KCB mã {0}", objLuotkham.MaDoituongKcb));
                    return;
                }
                byte batbuoc_tutuc =Utility.ByteDbnull( objLuotkham.MaDoituongKcb == "BHYT" && ma_doituongkcb != "BHYT" ? 1 : 0);
                m_dtDanhsachDichvuCLS = CHIDINH_CANLAMSANG.LaydanhsachCLS_chidinh(ma_doituongkcb, batbuoc_tutuc,
                                                                                   objLuotkham.TrangthaiNoitru,
                                                                                   Utility.ByteDbnull(objLuotkham.GiayBhyt, 0), -1,
                                                                                   Utility.Int32Dbnull(objLuotkham.DungTuyen.Value, 0),
                                                                                   globalVariables.MA_KHOA_THIEN, nhomchidinh, tnv_chidinh, dtRegDate.Value);
                if (!m_dtDanhsachDichvuCLS.Columns.Contains(KcbChidinhclsChitiet.Columns.SoLuong))
                    m_dtDanhsachDichvuCLS.Columns.Add(KcbChidinhclsChitiet.Columns.SoLuong, typeof(int));
                if (!m_dtDanhsachDichvuCLS.Columns.Contains("ten_donvitinh"))
                    m_dtDanhsachDichvuCLS.Columns.Add("ten_donvitinh", typeof(string));

                if (!m_dtDanhsachDichvuCLS.Columns.Contains("chophep_denghi_mg"))
                    m_dtDanhsachDichvuCLS.Columns.Add("chophep_denghi_mg", typeof(bool));
                if (!m_dtDanhsachDichvuCLS.Columns.Contains("tyle_mg"))
                    m_dtDanhsachDichvuCLS.Columns.Add("tyle_mg", typeof(byte));

                m_dtDanhsachDichvuCLS_org = m_dtDanhsachDichvuCLS.Copy();

                m_dtDanhsachDichvuCLS.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdDichvuCLS, m_dtDanhsachDichvuCLS, false, true, "", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void CboGoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cboGoi_SelectedIndexChanged(cboGoi, new EventArgs());
        }

        void uiTab1_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            if(e.Page.Key=="0")
                SendKeys.Send("{F2}");
        }

        void mnuUpdateprice_Click(object sender, EventArgs e)
        {
            if (!Utility.Coquyen("chidinhcls_quyen_suadongia"))
            {
                Utility.thongbaokhongcoquyen("chidinhcls_quyen_suadongia", " sửa giá dịch vụ cận lâm sàng");
                return;
            }
           
            grdChitietChidinhCLS.RootTable.Columns["don_gia"].EditType = Utility.Coquyen("chidinhcls_quyen_suadongia") ? EditType.TextBox : EditType.NoEdit;
            Utility.focusCellofCurrentRow(grdChitietChidinhCLS, "don_gia");
        }

        void cmdIntachPhieu_Click(object sender, EventArgs e)
        {
            ChoninphieuCLS(false);
        }

        void mnuSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdChitietChidinhCLS.GetCheckedRows().Length <= 1)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 2 dịch vụ để lưu thành nhóm chỉ định mới");
                    return;
                }
                frm_themnhanh_nhomcls _themnhanh_nhomcls = new frm_themnhanh_nhomcls(nhomchidinh);
                _themnhanh_nhomcls.grdAssignDetail = this.grdChitietChidinhCLS;
                _themnhanh_nhomcls.ShowDialog();
                if (!_themnhanh_nhomcls.m_blnCancel)
                {
                    //DataTable dtNhomCLS = new DataTable();
                    //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINHCLS_NHOMCHIDINH_CHIASE", "0", true) == "1")
                    //    dtNhomCLS = new Select().From(DmucNhomcanlamsang.Schema).ExecuteDataSet().Tables[0];
                    //else
                    //    dtNhomCLS = new Select().From(DmucNhomcanlamsang.Schema).Where(DmucNhomcanlamsang.Columns.NguoiTao).IsEqualTo(globalVariables.UserName).Or(DmucNhomcanlamsang.Columns.NguoiTao).IsEqualTo("ADMIN").ExecuteDataSet().Tables[0];
                    txtNhomDichvuCLS.Init(SPs.DmucNhomcanlamsangGetdata(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin)).GetDataSet().Tables[0],
                new List<string>()
                    {
                        DmucNhomcanlamsang.Columns.Id,
                        DmucNhomcanlamsang.Columns.MaNhom,
                        DmucNhomcanlamsang.Columns.TenNhom
                    });
                    Utility.ShowMsg("Thêm mới nhóm chỉ định cận lâm sàng thành công. Bạn có thể chỉ định nhanh bằng nhóm mới thêm này khi thực hiện các ca thăm khám khác");
                    txtNhomDichvuCLS.Focus();
                    txtNhomDichvuCLS.SelectAll();
                }
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }

        void txtChanDoan_LostFocus(object sender, EventArgs e)
        {
            cmdluuchandoan.PerformClick();
        }

        void txtNhomDichvuCLS__OnEnterMe()
        {
            txtFilterName.Clear();
            if (PropertyLib._HISCLSProperties.InsertAfterSelectGroup)
                AddDetailbySelectedGroup();
        }
        void AddDetailbySelectedGroup()
        {
            grdDichvuCLS.RemoveFilters();
            if (Utility.Int32Dbnull(txtNhomDichvuCLS.MyID, -1) > 0)
            {
                DataTable dtChitietnhom = CHIDINH_CANLAMSANG.DmucLaychitietCLSTheonhomchidinhCls(Utility.Int32Dbnull(txtNhomDichvuCLS.MyID, -1));
                uncheckItems();
                foreach (GridEXRow row in grdDichvuCLS.GetDataRows())
                {
                    if (dtChitietnhom.Select(DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu + "="
                        + Utility.sDbnull(row.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu].Value, "-1")).Length > 0)
                        row.IsChecked = true;
                }
                cmdAddDetail_Click(cmdAddDetail, new EventArgs());
            }
        }
        void AddDetailbySelectedPackage()
        {
            grdDichvuCLS.RemoveFilters();
            txtFilterName.Clear();
            int id_goi = Utility.Int32Dbnull(cboGoi.SelectedValue, -1);
          
            if (id_goi > 0)
            {
                DataTable dtChitietnhom = new clsGoikham().LayChiTietGoiKhamTheoIdGoi(Utility.Int32Dbnull(cboGoi.SelectedValue, -1));
                uncheckItems();
                foreach (GridEXRow row in grdDichvuCLS.GetDataRows())
                {
                    if (dtChitietnhom.Select(DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu + "=" + Utility.sDbnull(row.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu].Value, "-1")).Length > 0)
                        row.IsChecked = true;
                }
                resetNewItem();
                AddDetail(id_goi);
                uncheckItems();
            }
        }
        void AddDetailbySelectedPackage(DataTable dtChitietchon)
        {
            grdDichvuCLS.RemoveFilters();
            txtFilterName.Clear();
            int id_goi = Utility.Int32Dbnull(cboGoi.SelectedValue, -1);
           
            if (id_goi > 0)
            {
                uncheckItems();
                foreach (GridEXRow row in grdDichvuCLS.GetDataRows())
                {
                   
                    if (dtChitietchon.Select(DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu + "=" + Utility.sDbnull(row.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu].Value, "-1")).Length > 0)
                        row.IsChecked = true;
                }
                resetNewItem();
                AddDetail(id_goi);
                uncheckItems();
            }
        }
        void cmdAccept_Click(object sender, EventArgs e)
        {
            AddDetailbySelectedGroup();
            txtNhomDichvuCLS.SelectAll();
            txtNhomDichvuCLS.Focus();
        }

        void cmdTaonhom_Click(object sender, EventArgs e)
        {
            var quanlynhomchidinhCls = new frm_quanlynhomchidinh_cls(nhomchidinh);
            quanlynhomchidinhCls.ShowDialog();
            txtNhomDichvuCLS.Init(
               SPs.DmucNhomcanlamsangGetdata(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin)).GetDataSet().Tables[0],
                new List<string>()
                    {
                        DmucNhomcanlamsang.Columns.Id,
                        DmucNhomcanlamsang.Columns.MaNhom,
                        DmucNhomcanlamsang.Columns.TenNhom
                    });
        }

        void grdAssignDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.Delete) || e.KeyCode == Keys.Delete)
                mnuDelete_Click(mnuDelete, new EventArgs());
        }
        #endregion
        public int IdBacsikham = -1;
        public KcbDangkyKcb objCongkham { get; set; }
        public NoitruPhieudieutri objPhieudieutriNoitru { get; set; }
        // public KcbLuotkham objLuotkham;

        private void CauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                chkChiDinhNhanh.Checked = PropertyLib._ThamKhamProperties.Chidinhnhanh;
            }
        }
        private void frm_KCB_CHIDINH_CLS_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
            if (grdChitietChidinhCLS.RowCount > 0)
            {
                if (!isSaved)
                {
                    if (m_dtChitietPhieuCLS.Select("NoSave=1").Length > 0)
                    {
                        if (
                            Utility.AcceptQuestion(
                                "Bạn đã thêm mới một số chỉ định chi tiết mà chưa nhấn Ghi.\nBạn nhấn yes để hệ thống tự động lưu thông tin.\nNhấn No để hủy bỏ các chỉ định vừa thêm mới.", "Thông báo",
                                true))
                        {
                            if (!SaveData())
                                e.Cancel = true;
                        }
                    }
                }
            }
            log.Trace("End......................................................................................");
            SaveCauHinh();
        }

        private void SaveCauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                PropertyLib._ThamKhamProperties.Chidinhnhanh = chkChiDinhNhanh.Checked;
                PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
        }
        private void FormatUserNhapChiDinh()
        {
            try
            {
                GridEXColumn gridExColumn =
                    grdChitietChidinhCLS.RootTable.Columns[KcbChidinhclsChitiet.Columns.NguoiTao];
                GridEXColumn gridExColumnTarget =
                    grdChitietChidinhCLS.RootTable.Columns[DmucDichvuclsChitiet.Columns.TenChitietdichvu];
                var gridExFormatCondition = new GridEXFormatCondition(gridExColumn, ConditionOperator.NotEqual,
                                                                      globalVariables.UserName);
                gridExFormatCondition.FormatStyle.BackColor = Color.Red;
                gridExFormatCondition.TargetColumn = gridExColumnTarget;
                grdChitietChidinhCLS.RootTable.FormatConditions.Add(gridExFormatCondition);
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }
        /// <summary>
        /// hàm thực hiện việc đống form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        void ResetNhominCLS()
        {
            try
            {
                if (grdChitietChidinhCLS.GetDataRows().Length <= 0) return;
                var nhomcls = new List<string>();
                foreach (GridEXRow gridExRow in grdChitietChidinhCLS.GetDataRows())
                {
                    if (!nhomcls.Contains(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value)))
                    {
                        nhomcls.Add(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value));
                    }
                }
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung(globalVariables.DC_NHOMIN_CLS, true);
                DataTable dttempt = dtNhomin.Clone();
                foreach (DataRow dr in dtNhomin.Rows)
                    if (nhomcls.Contains(Utility.sDbnull(dr[DmucChung.Columns.Ma], "")))
                        dttempt.ImportRow(dr);
                DataBinding.BindDataCombobox(cboServicePrint, dttempt, DmucChung.Columns.Ma, DmucChung.Columns.Ten,
                                                          "Tất cả", true);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        /// <summary>
        /// hàm hự hiện việc load form hiện tại lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_CHIDINH_CLS_Load(object sender, EventArgs e)
        {
            try
            {
                Allow_ChongiaKCB = false;
                DataTable dtDoituongKCB = new Select().From(DmucDoituongkcb.Schema)//Where(DmucDoituongkcb.Columns.hieu)
                    .OrderAsc(DmucDoituongkcb.Columns.SttHthi).ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboDoituongKcb, dtDoituongKCB, DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb);
                cboDoituongKcb.SelectedValue = objLuotkham.MaDoituongKcb;
                log.Trace("StartLoading...");
                LoadUserConfigs();
                if (objPhieudieutriNoitru == null && objCongkham != null && objCongkham.IdPhieudieutri > 0)
                    objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(objCongkham.IdPhieudieutri);
                txtChanDoan.Init();
                LaydanhsachbacsiChidinh();
                BHYT_PTRAM_TRAITUYENNOITRU =Utility.DecimaltoDbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false),0m);
                DataTable dtDmucDichvuCls = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboDichVu, dtDmucDichvuCls,
                                           DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.TenDichvu,
                                           "Lọc thông tin theo loại dịch vụ", false);
                //DataTable dtNhomDichVuCLS =
                //    new Select().From(DmucNhomcanlamsang.Schema)
                //        .Where(DmucNhomcanlamsang.Columns.NguoiTao)
                //        .IsEqualTo(globalVariables.UserName)
                //        .Or(DmucNhomcanlamsang.Columns.NguoiTao)
                //        .IsEqualTo("ADMIN")
                //        .ExecuteDataSet()
                //        .Tables[0];
                //txtNhomDichvuCLS.Init( globalVariables.gv_dtNhomDichVuCLS,
                //                    new List<string>()
                //                          {
                //                              DmucNhomcanlamsang.Columns.Id,
                //                              DmucNhomcanlamsang.Columns.MaNhom,
                //                              DmucNhomcanlamsang.Columns.TenNhom
                //                          });
                txtNhomDichvuCLS.Init(SPs.DmucNhomcanlamsangGetdata(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin)).GetDataSet().Tables[0],
               new List<string>()
                    {
                        DmucNhomcanlamsang.Columns.Id,
                        DmucNhomcanlamsang.Columns.MaNhom,
                        DmucNhomcanlamsang.Columns.TenNhom
                    });
                
                log.Trace("StartLoading1...");
                InitData();
                log.Trace("StartLoading2...");
                GetData();
                LoadDichvuChidinhCLS();//Rem lại dòng này nếu dùng gói trừ đuổi
                LoadthongtinGoiKham1Lan();
                if (IdBacsikham > 0) txtBacsi.SetId(IdBacsikham);
                log.Trace("StartLoading3...");
                if (m_eAction == action.Update)
                {
                    LoadNhomin();
                }
              //  txtFilterName.Focus();
                log.Trace("StartLoading4...");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                log.Trace("Loaded");
                Allow_ChongiaKCB = true;
                 v_AssignId = Utility.Int32Dbnull(txtAssign_ID.Text, -1);
                SendKeys.Send("{F2}");
            }
        }
        void LoadthongtinGoiKham1Lan()
        {
            try
            {
                allowLoadGoikham = false;
                if (objLuotkham == null)
                {
                    cboGoi.Items.Clear();
                    cboGoi.Enabled = false;
                    return;
                }
                DataTable dtGoi1lan = new Select().From(GoiDanhsach.Schema).Where(GoiDanhsach.Columns.TrangThai).IsEqualTo(1).And(GoiDanhsach.Columns.KieuGoi).IsEqualTo(0).ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox_Goi(cboGoi, dtGoi1lan, "id_goi", "ten_goi", "---Chọn gói khám---", true, true);
                cboGoi.Enabled = dtGoi1lan.Rows.Count > 0;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                allowLoadGoikham = true;
                //if (cboGoi.Enabled) cboGoi.SelectedIndex = 1;
                //else
                //    if (cboGoi.Items.Count <= 0) cboGoi.SelectedIndex = -1;
                //else cboGoi.SelectedIndex = 0;
                //cboGoi_SelectedIndexChanged(cboGoi, new EventArgs());
            }
        }
        void LoadthongtinGoiKhamTruduoi()
        {
            try
            {
                allowLoadGoikham = false;
                if (objLuotkham == null)
                {
                    cboGoi.Items.Clear();
                    cboGoi.Enabled = false;
                    return;
                }
                DataTable _dtGoiKhamTheoBNCaNhan = new clsGoikham().LayGoiKhamTheoBN(objLuotkham.IdBenhnhan, "-1");
                DataTable dtAvailable = _dtGoiKhamTheoBNCaNhan.Clone();
                var q = from p in _dtGoiKhamTheoBNCaNhan.AsEnumerable()
                        where Utility.Int32Dbnull(p["condichvu"], 0) > 0
                        && Utility.ByteDbnull(p["tthai_kichhoat"]) > 0
                        && Utility.ByteDbnull(p["tthai_ttoan"]) > 0
                        && Utility.ByteDbnull(p["tthai_huy"]) <= 0
                        select p;
                if (q.Any())
                    dtAvailable = q.CopyToDataTable();
                DataBinding.BindDataCombobox_Goi(cboGoi, dtAvailable, "ID", "ten_goi", "---Dịch vụ ngoài gói---", true, true);
                cboGoi.Enabled = dtAvailable.Rows.Count > 0;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                allowLoadGoikham = true;
                if (cboGoi.Enabled) cboGoi.SelectedIndex = 1;
                else
                    if (cboGoi.Items.Count <= 0) cboGoi.SelectedIndex = -1;
                    else cboGoi.SelectedIndex = 0;
                cboGoi_SelectedIndexChanged(cboGoi, new EventArgs());
            }
        }
        void LoadUserConfigs()
        {
            try
            {
                chkIntachsaukhiluu.Checked = Utility.getUserConfigValue(chkIntachsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkIntachsaukhiluu.Checked)) == 1;
                chkTudongchontatca.Checked = Utility.getUserConfigValue(chkTudongchontatca.Tag.ToString(), Utility.Bool2byte(chkTudongchontatca.Checked)) == 1;
                chkThoatsaucapnhat.Checked = Utility.getUserConfigValue(chkThoatsaucapnhat.Tag.ToString(), Utility.Bool2byte(chkThoatsaucapnhat.Checked)) == 1;
                chkThoatsaukhithemmoi.Checked = Utility.getUserConfigValue(chkThoatsaukhithemmoi.Tag.ToString(), Utility.Bool2byte(chkThoatsaukhithemmoi.Checked)) == 1;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkIntachsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkIntachsaukhiluu.Checked));
                Utility.SaveUserConfig(chkTudongchontatca.Tag.ToString(), Utility.Bool2byte(chkTudongchontatca.Checked));
                Utility.SaveUserConfig(chkThoatsaucapnhat.Tag.ToString(), Utility.Bool2byte(chkThoatsaucapnhat.Checked));
                Utility.SaveUserConfig(chkThoatsaukhithemmoi.Tag.ToString(), Utility.Bool2byte(chkThoatsaukhithemmoi.Checked));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void LaydanhsachbacsiChidinh()
        {
            try
            {
                // DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, HosStatus);

                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }

                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_CHIDINHCLS_CHONBACSI", "0", false) == "1")
                    txtBacsi.Enabled = true;
                else
                    txtBacsi.Enabled = false;
                //txtBacsi.Enabled = globalVariables.IsAdmin;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCls()) return;
                string lstvalues = "";
                foreach (GridEXRow gridExRow in grdChitietChidinhCLS.GetCheckedRows())
                {
                    long idChidinhchitiet =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                    int idChidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value, -1);
                    if (idChidinhchitiet > 0)
                    {
                        CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(idChidinhchitiet, idChidinh);
                        lstvalues += idChidinhchitiet + ",";
                    }
                }
                DataRow[] rows = null;
                string _deleteitems = "";
                if (lstvalues.Length > 0)
                {
                    lstvalues = lstvalues.Substring(0, lstvalues.Length - 1);
                    rows =
                        m_dtChitietPhieuCLS.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " IN (" + lstvalues +
                                                   ")"); // UserName is Column Name
                    _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                                     select Utility.sDbnull(p["ten_chitietdichvu"])).ToList<string>());

                }
                if (rows != null && rows.Length > 0)
                {
                    foreach (DataRow r in rows)
                        r.Delete();
                    m_dtChitietPhieuCLS.AcceptChanges();
                }
                else
                {
                    foreach (GridEXRow gridExRow in grdChitietChidinhCLS.GetCheckedRows())
                    {
                        gridExRow.Delete();
                    }
                    m_dtChitietPhieuCLS.AcceptChanges();
                }
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa chỉ định của bệnh nhân ID={0}, PID={1}, Tên={2}, DS chỉ định xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                if (grdChitietChidinhCLS.GetDataRows().Length <= 0)
                {
                    m_eAction = action.Insert;
                    txtAssign_ID.Text = "(Tự sinh)";
                    txtAssignCode.Text = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                }
                m_blnCancel = false;
                ModifyCommand();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện kiểm tra thông tin chỉ dịnh cận lâm sàng
        /// </summary>
        /// <returns></returns>
        private bool IsValidDataXoaCls()
        {
            DataTable dtTempt = new DataTable();
            SqlQuery sqlQuery;
            if (Utility.Int64Dbnull(txtAssign_ID.Text) > 0 || m_eAction == action.Update)
            {

                DataTable dtPaymentData = SPs.ChidinhclsKiemtratrangthaiChitiet(Utility.Int64Dbnull(txtAssign_ID.Text)).GetDataSet().Tables[0];
                if (dtPaymentData.Rows.Count > 0)
                {
                    Utility.ShowMsg("Phiếu chỉ định đã có dịch vụ được thanh toán trong lúc bạn đang mở xem nên bạn không được phép sửa/xóa phiếu. Nhấn OK để kết thúc và liên hệ bộ phận thanh toán");
                    return false;
                }
            }

            if (grdChitietChidinhCLS.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một bản ghi thực hiện xóa thông tin dịch vụ cận lâm sàng",
                                "Thông báo", MessageBoxIcon.Warning);
                grdChitietChidinhCLS.Focus();
                return false;
            }
            
            if (!globalVariables.IsAdmin)
            {

                foreach (GridEXRow gridExRow in grdChitietChidinhCLS.GetCheckedRows())
                {
                    int v_intIdChitietchidinh =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                  int i =   m_dtChitietPhieuCLS.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " = " + v_intIdChitietchidinh +
                                               " And " + KcbChidinhclsChitiet.Columns.NguoiTao + " <> '" + globalVariables.UserName+"'").Count();
                    //SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    //    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(v_intIdChitietchidinh)
                    //    .And(KcbChidinhclsChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                    if (i > 0)
                    {
                        Utility.ShowMsg("Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                        return false;
                       
                    }
                    dtTempt = new Select().From(KcbPhieupttt.Schema).Where(KcbPhieupttt.Columns.IdChitietchidinh).IsEqualTo(v_intIdChitietchidinh).ExecuteDataSet().Tables[0];
                    if (dtTempt.Rows.Count > 0)
                    {
                        Utility.ShowMsg(string.Format("Dịch vụ {0} đã có phiếu Phẫu thuật-thủ thuật nên không thể xóa.", Utility.sDbnull(gridExRow.Cells["ten_chitietdichvu"].Value)));
                        return false;
                    }
                }
            }
            foreach (GridEXRow gridExRow in grdChitietChidinhCLS.GetCheckedRows())
            {
                long v_intIdChitietchidinh =
                    Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                //int i = m_dtChitietPhieuCLS.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " = " + v_intIdChitietchidinh +
                //                 " And " + KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan + " = 1").Count();
                 sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(v_intIdChitietchidinh)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                    return false;

                }
                sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(v_intIdChitietchidinh)
                    .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                    return false;
                }
            }
            
            return true;
        }

        private bool IsValidDataXoaCLS_Selected()
        {
            if (Utility.Int64Dbnull(txtAssign_ID.Text) > 0 || m_eAction == action.Update)
            {

                DataTable dtPaymentData =SPs.ChidinhclsKiemtratrangthaiChitiet(Utility.Int64Dbnull(txtAssign_ID.Text)).GetDataSet().Tables[0];
                if (dtPaymentData.Rows.Count > 0)
                {
                    Utility.ShowMsg("Phiếu chỉ định đã có dịch vụ được thanh toán trong lúc bạn đang mở xem nên bạn không được phép sửa/xóa phiếu. Nhấn OK để kết thúc và liên hệ bộ phận thanh toán");
                    return false;
                }
            }
            if (grdChitietChidinhCLS.RowCount <= 0 || grdChitietChidinhCLS.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một bản ghi thực hiện xóa thông tin dịch vụ cận lâm sàng",
                                "Thông báo", MessageBoxIcon.Warning);
                grdChitietChidinhCLS.Focus();
                return false;
            }
            int AssignDetail_ID = Utility.Int32Dbnull(grdChitietChidinhCLS.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
            SqlQuery sqlQuery = null;
            if (!globalVariables.IsAdmin)
            {


                int i = m_dtChitietPhieuCLS.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " = " + AssignDetail_ID +
                                              " And " + KcbChidinhclsChitiet.Columns.NguoiTao + " <> '" + globalVariables.UserName + "'").Count();

                //sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                //    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                //    .And(KcbChidinhclsChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                if (i > 0)
                {
                    Utility.ShowMsg("Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }


            //int j = m_dtChitietPhieuCLS.Select(KcbChidinhclsChitiet.Columns.IdChitietchidinh + " = " + AssignDetail_ID +
            //                 " And " + KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan + " = 1").Count();
            sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }

            sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
               .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
               .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }
        void ChoninphieuCLS(bool inchung)
        {
            frm_InphieuCLS _InphieuCLS = new frm_InphieuCLS(m_dtChitietPhieuCLS,chkTudongchontatca.Checked);
            _InphieuCLS.objLuotkham = this.objLuotkham;
            if (!inchung)
                _InphieuCLS.ShowDialog();
            else
            {
                _InphieuCLS.InChungphieu();
            }
        }
        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin của dịch vụ cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                action oldAct = m_eAction;
                cmdSave.Enabled = false;
                if (!SaveData()) return;
                if (chkIntachsaukhiluu.Checked)
                    ChoninphieuCLS(false);
                    //cmdInPhieuCLS.PerformClick();
                if ((oldAct == action.Insert && chkThoatsaukhithemmoi.Checked) || (oldAct == action.Update && chkThoatsaucapnhat.Checked))
                {
                    this.Close();
                }

                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                cmdSave.Enabled = true;
                GC.Collect();
            }
          
        }

        private bool KiemTraTamUng()
        {
            if (objLuotkham.MaDoituongKcb == "BHYT") return true;
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_CANHBAO_TAMUNG", "1", true) == "0") 
                return true;
            decimal tstamung = noitru_TamungHoanung.LaySoTienTamUng(objLuotkham.MaLuotkham, Utility.Int64Dbnull(objLuotkham.IdBenhnhan), 0);
            decimal t_bntt = 0;
            //Kiểm tra nếu không có tạm ứng ngoại trú thì không cần kiểm tra
            if (tstamung <= 0)
                return true;
            //Nếu có tạm ứng ngoại trú thì kiểm tra
            //B1: Lấy tổng chi phí chỉ định lần này
            foreach (GridEXRow  row  in  grdChitietChidinhCLS.GetDataRows())
            {
                if (Utility.Int64Dbnull(row.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1) < 0)
                {
                    t_bntt = t_bntt + Utility.DecimaltoDbnull(row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0);
                }
            }
            //B2: Lấy tổng chi phí đã chỉ định trước đó
            decimal TongChiPhi = KCB_CHIDINH_CANLAMSANG.LayTongSoTienChuaThanhToan(objLuotkham.MaLuotkham,
                objLuotkham.IdBenhnhan, Utility.Int32Dbnull(objLuotkham.Noitru));
            //B3: Kiểm tra tổng chi phí > tổng tạm ứng thì ko cho lưu
            if (tstamung < t_bntt + TongChiPhi)
            {
                Decimal Gioihancanhbao = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("NGOAITRU_GIOIHAN_NOPTIENTAMUNG", "0", true), 0);
                string result = "";
                if (tstamung - (t_bntt + TongChiPhi) > Gioihancanhbao)//OK
                {
                    result = "";
                }
                string s1 = String.Format(Utility.FormatDecimal(), Gioihancanhbao);
                string s2 = String.Format(Utility.FormatDecimal(), String.Format(Utility.FormatDecimal(), Convert.ToDecimal((tstamung - (t_bntt + TongChiPhi)).ToString())));
                string s3 = String.Format(Utility.FormatDecimal(), Gioihancanhbao - (tstamung - (t_bntt + TongChiPhi)));
                 result = string.Format("Giới hạn cảnh báo khi tổng tiền tạm ứng của người bệnh nhỏ hơn hoặc bằng {0}. Hiện tại, Tổng tạm ứng - Tổng chi phí = {1}. Do vậy, người bệnh cần đóng thêm: {2}", s1, s2, s3);
                 string sTU = String.Format(Utility.FormatDecimal(), tstamung);
                string sTCPDachidinh = String.Format(Utility.FormatDecimal(), TongChiPhi);
                string sTCPDangchichinh = String.Format(Utility.FormatDecimal(), t_bntt );
                string sChenhlech = String.Format(Utility.FormatDecimal(), tstamung - (t_bntt + TongChiPhi));
                 string sTCP = String.Format(Utility.FormatDecimal(), (t_bntt + TongChiPhi));
                 Utility.ShowMsg(string.Format("Tổng tạm ứng: {0} đồng\nTổng chi phí=Tổng chi phí đã chỉ định+ Tổng chi phí chỉ định lần này={1}+{2}={3} đồng\nTổng chênh lệch=Tổng tạm ứng - Tổng chi phí={4} đồng\n Người bệnh cần nộp thêm tiền tạm ứng ít nhất {5} đồng trước khi chỉ định các dịch vụ", sTU, sTCPDachidinh, sTCPDangchichinh, sTCP, sChenhlech, s3));
                //if (Utility.AcceptQuestion(string.Format("Giới hạn kê chỉ định. Số tiền tạm ứng đã vượt quá. Số tiền tạm ứng - Tổng chi phí:  {0} - {1} < 0. Nhấn 'YES' nếu bạn muốn tiếp tục lưu.", tstamung, (t_bntt + TongChiPhi)), "Cảnh báo", true))
                //{
                //    return true;
                //}
                return false;

                //12/05/2023: Bỏ do trùng với bên trên
                //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_CAMCHIDINH_TAMUNG", "1", true) == "1")
                //{
                //    Utility.ShowMsg(string.Format("Giới hạn kê chỉ định. Số tiền tạm ứng đã vượt quá. Số tiền tạm ứng - Tổng chi phí:  {0} - {1} < 0 ", tstamung, (t_bntt + TongChiPhi)));
                //    return false;
                //}
                //return true;
            }
            return true;
        }
        bool SaveData()
        {
            if (!IsValidData()) return false;
            if (!KiemTraTamUng() ) return false;
            isSaved = true;
            SetSaveStatus();
            PerformAction();
            return true;
        }
        /// <summary>
        /// hàm thực hiện việc kiểm tra lại thông tin 
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Bạn cần chọn bác sĩ chỉ định trước khi thực hiện lưu chỉ định", true);
                txtBacsi.Focus();
                return false;
            }
            if (objPhieudieutriNoitru != null)
            {
                if (objPhieudieutriNoitru.NgayDieutri != null && dtRegDate.Value.Date > objPhieudieutriNoitru.NgayDieutri.Value.Date)
                {
                    Utility.ShowMsg("Ngày chỉ định phải <= " + objPhieudieutriNoitru.NgayDieutri.Value.ToString("dd/MM/yyyy"));
                    dtRegDate.Focus();
                    return false;
                }
            }
            if (objCongkham != null )//||(objCongkham==null && noitru==0))
            {
                objCongkham = KcbDangkyKcb.FetchByID(objCongkham.IdKham);
                if (objCongkham == null)//Bác sĩ đang ở trạng thái kê đơn hoặc cls thì tiếp đón xóa mất công khám
                {
                    Utility.ShowMsg("Công khám bác sĩ đang kê chỉ định CLS không tồn tại(Có thể bị xóa trong lúc bác sĩ đang kê và chưa kịp lưu).\nVui lòng liên hệ quầy tiếp đón và IT bệnh viện để xác minh");
                    return false;
                }
            }    
            if (grdChitietChidinhCLS.RowCount <= 0)
            {
                Utility.ShowMsg("Hiện không có bản ghi nào thực hiện việc lưu lại thông tin", "Thông báo",
                                MessageBoxIcon.Warning);
                cmdSave.Focus();
                return false;
            }
            if (Utility.Int64Dbnull(txtAssign_ID.Text) > 0 || m_eAction == action.Update)
            {
                DataTable dtPaymentData = SPs.ChidinhclsKiemtratrangthaiChitiet(Utility.Int64Dbnull(txtAssign_ID.Text)).GetDataSet().Tables[0];
                if (dtPaymentData.Rows.Count > 0)
                {
                    Utility.ShowMsg("Phiếu chỉ định đã có dịch vụ được thanh toán trong lúc bạn đang mở xem nên bạn không được phép sửa/xóa phiếu. Nhấn OK để kết thúc và liên hệ bộ phận thanh toán");
                    return false;
                }
            }
            //Kiểm tra các dịch vụ trong gói xem số lượng có > số lượng còn lại hay không. Có thể do nhiều BS cùng kê
            foreach (DataRow dr in m_dtChitietPhieuCLS.Rows)
            {
                int id_dangky=Utility.Int32Dbnull(dr["id_dangky"],-1);
                int so_luong_ke=Utility.Int32Dbnull(dr["so_luong"],-1);
                if (id_dangky > 0)
                {
                    GoiTinhtrangsudung goittsd = GoiTinhtrangsudung.FetchByID(id_dangky);
                    if (goittsd.SoluongDung < so_luong_ke)
                    {
                        string msg = string.Format("Dịch vụ trong gói : {0} đang có số lượng chỉ định: {1} nhiều hơn số lượng khả dụng: {2}. Đề nghị chỉnh lại số lượng chỉ định phù hợp", dr["ten_chitietdichvu"].ToString(), so_luong_ke, goittsd.SoluongDung);
                        Utility.ShowMsg(msg);
                        return false;
                    }
                }
            }
            return true;
        }

        private void ModifyCommand()
        {
            try
            {
                cmdSave.Enabled = grdChitietChidinhCLS.RowCount > 0;
                cmdDelete.Enabled = Utility.isValidGrid(grdChitietChidinhCLS) || grdChitietChidinhCLS.GetCheckedRows().Count() > 0;
                cmdInPhieuCLS.Enabled =cmdIntachPhieu.Enabled= grdChitietChidinhCLS.RowCount > 0;
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                log.Error("Loi trong qua trinh trang thai cua nut {0}", ex);
            }
        }

        /// <summary>
        /// hàm thục hiện việc trạng thái của hoạt động của 
        /// 
        /// </summary>
        private void PerformAction()
        {
            try
            {
                cmdSave.Enabled = false;
                switch (m_eAction)
                {
                    case action.Insert:
                        InsertDataCls();
                        break;
                    case action.Update:
                        UpdateDataCLS();
                        break;
                }
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
               // m_blnCancel = false; ;
                ModifyCommand();
                cmdSave.Enabled = true;
            }
        }

        List<int> getDichvutronggoi()
        {
            var q = from p in m_dtChitietPhieuCLS.AsEnumerable()
                    where Utility.Int32Dbnull(p["id_dangky"], -1) > 0 && Utility.Int32Dbnull(p["id_goi"], -1) > 0 && Utility.Int32Dbnull(p["trangthai_thanhtoan"], -1) <=0
                    select Utility.Int32Dbnull(p["id_chitietdichvu"], -1);
            if (q.Any())
                return q.ToList<int>();
            return new List<int>();
        }
        List<int> GetIdDangkyGoi()
        {
            var q = from p in m_dtChitietPhieuCLS.AsEnumerable()
                    where Utility.Int32Dbnull(p["id_dangky"], -1) > 0 && Utility.Int32Dbnull(p["id_goi"], -1) > 0 && Utility.Int32Dbnull(p["trangthai_thanhtoan"], -1) <= 0
                    select Utility.Int32Dbnull(p["id_dangky"], -1);
            if (q.Any())
                return q.Distinct().ToList<int>();
            return new List<int>();
        }
        List<int> GetIdGoi()
        {
            var q = from p in m_dtChitietPhieuCLS.AsEnumerable()
                    where Utility.Int32Dbnull(p["id_goi"], -1) > 0 && Utility.Int32Dbnull(p["trangthai_thanhtoan"], -1) <= 0
                    select Utility.Int32Dbnull(p["id_goi"], -1);
            if (q.Any())
                return q.Distinct().ToList<int>();
            return new List<int>();
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới thông tin cận lâm sàng
        /// </summary>
        private void InsertDataCls()
        {
            KcbChidinhcl objKcbChidinhcls = TaoPhieuchidinh();
            string ErrMsg = "";
            actionResult =
               CHIDINH_CANLAMSANG.InsertDataChiDinhCls(
                    objKcbChidinhcls, objLuotkham, TaoChitietchidinh(), GetIdGoi(), GetIdDangkyGoi(), getDichvutronggoi(), ref ErrMsg);
            switch (actionResult)
            {
                case ActionResult.Success:
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới phiếu chỉ định cho bệnh nhân ID={0}, PID={1}, Tên={2} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                    if (objKcbChidinhcls != null)
                    {
                        txtAssign_ID.Text = Utility.sDbnull(objKcbChidinhcls.IdChidinh);
                        txtAssignCode.Text = Utility.sDbnull(objKcbChidinhcls.MaChidinh);
                       
                    }
                    m_eAction = action.Update;
                    m_blnCancel = false;
                    //LayThongTin_Chitiet_CLS();
                    //Bỏ dòng dưới tránh lỗi duplicate do ID ko được reset từ -1 về giá trị mới insert
                   //if(! PropertyLib._HISCLSProperties.ThoatSauKhiLuu)//if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_CHONINSAUKHILUU", "1", false) == "1") //if (chkSaveAndPrint.Checked)
                   // {
                        GetData();
                        ResetNhominCLS();
                        ModifyRegions();
                    //}
                    
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm mới thông tin", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
            //Utility.EnableButton(cmdSave, true);
            ModifyCommand();
        }
        bool _hasLoadPrintType = false;
        void LoadNhomin()
        {
            if (_hasLoadPrintType) return;
            _hasLoadPrintType = true;
            //chkIntach.Visible = true;
            //cboServicePrint.Visible = true;
         //   DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
            DataBinding.BindDataCombox(cboServicePrint, globalVariables.gv_dtNhomInCLS, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Tất cả", true);
        }
        void ModifyRegions()
        {
            return;
            cboServicePrint.Location=new Point(223, 20);
            cboServicePrint.Size = new Size(430, 23);
            chkIntach.Visible = true;
            cboServicePrint.Visible = true;
            cmdSave.Visible = false;
            cmdDelete.Visible = false;
        }
        /// <summary>
        /// hmf thực hiện cập n hập thông tin cận lâm sàng
        /// </summary>
        private void UpdateDataCLS()
        {
            KcbChidinhcl objKcbChidinhcls = TaoPhieuchidinh();
            string ErrMsg = "";
            actionResult =
                CHIDINH_CANLAMSANG.UpdateDataChiDinhCLS(
                    objKcbChidinhcls, objLuotkham, TaoChitietchidinh(), GetIdGoi(), GetIdDangkyGoi(), getDichvutronggoi(), ref ErrMsg);
            switch (actionResult)
            {
                case ActionResult.Success:
                    m_blnCancel = false;
                    GetData();
                    //Utility.ShowMsg("Cập nhật thông tin phiếu chỉ định CLS thành công. Nhấn OK để kết thúc");
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật phiếu chỉ định cho bệnh nhân ID={0}, PID={1}, Tên={2} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa mới thông tin", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
            Utility.EnableButton(cmdSave, true);
        }

        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của phiếu chỉ định cận lâm sàng
        /// </summary>
        /// <returns></returns>
        private KcbChidinhcl TaoPhieuchidinh()
        {
            KcbChidinhcl objKcbChidinhcls = null;

            objKcbChidinhcls = KcbChidinhcl.FetchByID(Utility.Int32Dbnull(txtAssign_ID.Text, -1));
            if (objKcbChidinhcls != null)
            {
                objKcbChidinhcls.IsNew = false;
                objKcbChidinhcls.MarkOld();
            }
            else
            {
                objKcbChidinhcls = new KcbChidinhcl();
                objKcbChidinhcls.IsNew = true;
            }
            objKcbChidinhcls.LoaiPhieu = Utility.Bool2byte(chkPhieuCDThem.Checked);
            objKcbChidinhcls.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
            objKcbChidinhcls.MaCoso = objLuotkham.MaCoso;
            objKcbChidinhcls.MatheBhyt = objLuotkham.MatheBhyt;
            objKcbChidinhcls.MaChidinh = txtAssignCode.Text;
            objKcbChidinhcls.MaLuotkham = objLuotkham.MaLuotkham;
            objKcbChidinhcls.IdBenhnhan = Utility.Int64Dbnull(objLuotkham.IdBenhnhan, -1);
            objKcbChidinhcls.IdBacsiChidinh = Utility.Int16Dbnull(txtBacsi.MyID, globalVariables.gv_intIDNhanvien);
            
            objKcbChidinhcls.NguoiTao = globalVariables.UserName;
            objKcbChidinhcls.NgayTao = globalVariables.SysDate;
            objKcbChidinhcls.NgayChidinh = dtRegDate.Value.Date;
            objKcbChidinhcls.KieuChidinh = kieu_chidinh;
            objKcbChidinhcls.IdKham = objCongkham != null ? objCongkham.IdKham : -1;
            if (objCongkham != null)//Chỉ định qua phòng khám hoặc nội trú
            {
                objKcbChidinhcls.IdKhoaChidinh = objCongkham != null ? objCongkham.IdKhoadieutri : (Int16)globalVariables.idKhoatheoMay;
                if (Utility.Byte2Bool(objCongkham.Noitru))
                {
                    objKcbChidinhcls.IdKhoadieutri = objCongkham.IdKhoadieutri;
                    objKcbChidinhcls.IdPhongChidinh = objCongkham.IdPhongkham;
                    objKcbChidinhcls.IdKhoaChidinh = (Int16)globalVariables.idKhoatheoMay;
                }
                else//Phòng khám
                {
                    objKcbChidinhcls.IdPhongChidinh = objCongkham.IdPhongkham;
                    objKcbChidinhcls.IdKhoaChidinh = objCongkham.IdKhoakcb;
                }

            }
            
            if (objPhieudieutriNoitru != null)
            {
                objKcbChidinhcls.IdDieutri = objPhieudieutriNoitru.IdPhieudieutri;
                objKcbChidinhcls.IdKhoadieutri = objPhieudieutriNoitru.IdKhoanoitru;
                objKcbChidinhcls.IdBuongGiuong = objPhieudieutriNoitru.IdBuongGiuong;
                objKcbChidinhcls.IdPhongChidinh = objPhieudieutriNoitru.IdKhoanoitru;
            }
            objKcbChidinhcls.Noitru =noitru;
            if (m_eAction == action.Update)
            {
               
                objKcbChidinhcls.NgaySua = globalVariables.SysDate;
                objKcbChidinhcls.NguoiSua = globalVariables.UserName;
                objKcbChidinhcls.IpMaysua = globalVariables.gv_strIPAddress;
                objKcbChidinhcls.TenMaysua = globalVariables.gv_strComputerName;
            }
            else
            {
                objKcbChidinhcls.IpMaytao = globalVariables.gv_strIPAddress;
                objKcbChidinhcls.TenMaytao = globalVariables.gv_strComputerName;
            }
            objKcbChidinhcls.MaCoso = globalVariables.Ma_Coso;
            return objKcbChidinhcls;
        }

        /// <summary>
        /// hàm thực hiện việc tạo mảng thông tin của chỉ định chi tiết cận lâm sàng
        /// </summary>
        /// <returns></returns>
        private KcbChidinhclsChitiet[] TaoChitietchidinh()
        {
            int i = 0;
            foreach (GridEXRow gridExRow in grdChitietChidinhCLS.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record) i++;
            }
            int idx = 0;
            var arrAssignDetail = new KcbChidinhclsChitiet[i];
            foreach (GridEXRow gridExRow in grdChitietChidinhCLS.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    arrAssignDetail[idx] = new KcbChidinhclsChitiet();
                    if (Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1) > 0)
                    {
                        arrAssignDetail[idx].IsLoaded = true;
                        arrAssignDetail[idx].IsNew = false;
                        arrAssignDetail[idx].MarkOld();
                        arrAssignDetail[idx].IdChitietchidinh = Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                    }
                    else
                    {
                        arrAssignDetail[idx].IsNew = true;
                    }
                    arrAssignDetail[idx].IdChidinh = Utility.Int32Dbnull(txtAssign_ID.Text, -1);
                    arrAssignDetail[idx].NguoiTao = globalVariables.UserName;
                    arrAssignDetail[idx].IdDichvu = Utility.Int16Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdDichvu].Value, -1);
                    arrAssignDetail[idx].IdChitietdichvu = Utility.Int16Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    arrAssignDetail[idx].SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 1);
                    arrAssignDetail[idx].TyleTt = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 1);
                    arrAssignDetail[idx].DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);

                    arrAssignDetail[idx].GiaDanhmuc = Utility.DecimaltoDbnull(gridExRow.Cells["gia_goc"].Value, 0);
                    arrAssignDetail[idx].PhuThu = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
                    arrAssignDetail[idx].HienthiBaocao = 0;
                    
                    if (m_eAction == action.Insert)
                    {
                        arrAssignDetail[idx].TrangthaiBhyt = 0;
                        arrAssignDetail[idx].TrangthaiHuy = 0;
                        arrAssignDetail[idx].TrangThai = 0;
                        
                    }
                    arrAssignDetail[idx].TrangthaiThanhtoan = Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan].Value, 0);
                    //arrAssignDetail[idx].MotaThem = Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.MotaThem].Value, "");
                    arrAssignDetail[idx].TinhChkhau = Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TinhChkhau].Value, 0);
                    arrAssignDetail[idx].NgayTao = globalVariables.SysDate;
                    arrAssignDetail[idx].TuTuc = Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value, 0);
                    arrAssignDetail[idx].MadoituongGia = Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.MadoituongGia].Value, objLuotkham.MaDoituongKcb);

                    arrAssignDetail[idx].IdThanhtoan = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdThanhtoan].Value, -1);
                    arrAssignDetail[idx].IdDangky = Utility.Int32Dbnull(gridExRow.Cells["Id_Dangky"].Value, -1);
                    arrAssignDetail[idx].IdGoi = Utility.Int32Dbnull(gridExRow.Cells["Id_Goi"].Value, -1);
                    arrAssignDetail[idx].ChophepDenghiMg = Utility.ByteDbnull(gridExRow.Cells["chophep_denghi_mg"].Value, 0) == 1;
                    arrAssignDetail[idx].TyleMg = Utility.ByteDbnull(gridExRow.Cells["tyle_mg"].Value, 0);
                    //if (arrAssignDetail[idx].IdGoi > 0) //PM đang coi dịch vụ trong gói đơn giá sẽ =0-->Bỏ để case của BV mắt hoạt động do gói ở đây chỉ mang tính chất quản lý
                    arrAssignDetail[idx].TrongGoi = 0;
                    arrAssignDetail[idx].GhiChu = Utility.sDbnull(gridExRow.Cells["ghi_chu"].Value, "");
                    arrAssignDetail[idx].IpMaysua = globalVariables.gv_strIPAddress;
                    arrAssignDetail[idx].TenMaysua = globalVariables.gv_strComputerName;

                    arrAssignDetail[idx].IpMaytao = globalVariables.gv_strIPAddress;
                    arrAssignDetail[idx].TenMaytao = globalVariables.gv_strComputerName;
                    idx++;
                }
            }
            return arrAssignDetail;
        }

        /// <summary>
        /// hàm thực hiẹn eviệc thây đổi thông tin trên lưới
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt của form thực hiện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_CHIDINH_CLS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) Utility.ShowMsg(Help);
            if (e.KeyCode == Keys.F4 || (e.Control && e.KeyCode == Keys.P)) cmdInPhieuCLS_Click(cmdInPhieuCLS, new EventArgs());
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.A && e.Control) cmdAddDetail.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtFilterName.Focus();
                txtFilterName.SelectAll();
            }
            if ((e.Control && e.KeyCode == Keys.F3) || e.KeyCode == Keys.F3)
            {
                txtNhomDichvuCLS.Focus();
                txtNhomDichvuCLS.SelectAll();
            }
            if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.Alt && e.KeyCode == Keys.M) grdDichvuCLS.Select();
        }

        /// <summary>
        /// hàm thực hiện việc lọc thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            FilterCLS();
        }

        private void txtFilterName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Down))
            {
                if (grdDichvuCLS.GetDataRows().Length == 1)
                {
                    foreach (GridEXRow _row in grdDichvuCLS.GetDataRows())
                    {
                        if (_row.RowType == RowType.Record)
                        {
                            _row.IsChecked = true;
                            if (_row.IsChecked)
                            {
                                Utility.focusCell(grdDichvuCLS, DmucDichvucl.Columns.TenDichvu);
                                AddOneRow_ServiceDetail();
                                txtFilterName.SelectAll();
                                txtFilterName.Focus();
                            }

                        }
                    }
                }
                else
                    Utility.focusCell(grdDichvuCLS, DmucDichvucl.Columns.TenDichvu);
            }
        }
       
        private void FilterCLS()
        {
            try
            {
                log.Trace("Filtering...");
                m_blnAllowSelectionChanged = false;
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtFilterName.Text))
                {
                    string _value = Utility.DoTrim(txtFilterName.Text.ToUpper());
                    int rowcount = 0;
                    rowcount =
                        m_dtDanhsachDichvuCLS.Select(DmucDichvucl.Columns.MaDichvu + " ='" + _value +
                                                 "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = DmucDichvucl.Columns.MaDichvu + " ='" + _value + "'";
                    }
                    else
                    {
                        rowFilter = DmucDichvuclsChitiet.Columns.TenChitietdichvu + " like '%" + _value +
                                    "%'  OR  " + DmucDichvuclsChitiet.Columns.MaChitietdichvu + " like '" + _value +
                                    "%'";

                    }
                }
                //m_dtDanhsachDichvuCLS.DefaultView.RowFilter = "1=1";
                m_dtDanhsachDichvuCLS.DefaultView.RowFilter = rowFilter;
                grdDichvuCLS.Refetch();
                Application.DoEvents();
            }
            catch (Exception exception)
            {
                log.ErrorException("Filter.exception-->", exception);
                log.Error("loi trong qua trinh loc thong tin ", exception);
                rowFilter = "1=2";
            }
            finally
            {
                log.Trace("Filtered");
                m_blnAllowSelectionChanged = true;
            }
        }

        /// <summary>
        /// hamf thuc hien viec them thong tin cua can lam sang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            resetNewItem();
            AddDetail();
            uncheckItems();
        }
        void resetNewItem()
        {
            if (m_dtChitietPhieuCLS == null || !m_dtChitietPhieuCLS.Columns.Contains("isnew")) return;
            foreach (DataRow dr in m_dtChitietPhieuCLS.Rows)
                dr["isnew"] = 0;
            m_dtChitietPhieuCLS.AcceptChanges();
        }
        void SetSaveStatus()
        {
            if (m_dtChitietPhieuCLS == null || !m_dtChitietPhieuCLS.Columns.Contains("isnew")) return;
            foreach (DataRow dr in m_dtChitietPhieuCLS.Rows)
                dr["NoSave"] = 0;
            m_dtChitietPhieuCLS.AcceptChanges();
        }
        string KiemtraCamchidinhchungphieu(int idDichvuchitiet,string tenChitiet)
        {
            string reval = "";
            string tempt = "";
            List<string> lstKey = new List<string>();
            string _key = "";
            //Kiểm tra dịch vụ đang thêm có phải là dạng Single-Service hay không?
            DataRow[] arrSingle = m_dtDanhsachDichvuCLS.Select(DmucDichvuclsChitiet.Columns.SingleService + "=1 AND " + DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + idDichvuchitiet);
            if (arrSingle.Length > 0 && m_dtChitietPhieuCLS.Select(KcbChidinhclsChitiet.Columns.IdChitietdichvu + "<>" + idDichvuchitiet).Length > 0)
            {
                return string.Format("Single-Service: {0}", tenChitiet);
            }
            //Kiểm tra các dịch vụ đã thêm có cái nào là Single-Service hay không?
            List<int> lstID=m_dtChitietPhieuCLS.AsEnumerable().Select(c=>Utility.Int32Dbnull( c[KcbChidinhclsChitiet.Columns.IdChitietdichvu],0)).Distinct().ToList<int>();
            var q = from p in m_dtDanhsachDichvuCLS.AsEnumerable()
                    where Utility.ByteDbnull(p[DmucDichvuclsChitiet.Columns.SingleService], 0) == 1
                    && lstID.Contains(Utility.Int32Dbnull(p[DmucDichvuclsChitiet.Columns.IdChitietdichvu], 0))
                    select p;
            if (q.Any())
            {
                return string.Format("Single-Service: {0}",Utility.sDbnull( q.FirstOrDefault()[DmucDichvuclsChitiet.Columns.TenChitietdichvu],""));
            }
            //Lấy các cặp cấm chỉ định chung cùng nhau
            DataRow[] arrDr = _mDtqheCamchidinhClsChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvu + "=" + idDichvuchitiet );
            DataRow[] arrDr1 = _mDtqheCamchidinhClsChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung + "=" + idDichvuchitiet);
            foreach (DataRow dr in arrDr)
            {

                DataRow[] arrtemp = m_dtChitietPhieuCLS.Select(KcbChidinhclsChitiet.Columns.IdChitietdichvu + "=" + Utility.sDbnull(dr[QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung]));
                if (arrtemp.Length > 0)
                {

                    foreach (DataRow dr1 in arrtemp)
                    {
                        tempt = string.Empty;
                        _key = idDichvuchitiet.ToString() + "-" + Utility.sDbnull(dr1[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            tempt = string.Format("{0} - {1}", tenChitiet, Utility.sDbnull(dr1[DmucDichvuclsChitiet.Columns.TenChitietdichvu], ""));
                        }
                        if(tempt!=string.Empty)
                            reval += tempt + "\n";
                    }
                   
                }
            }
            foreach (DataRow dr in arrDr1)
            {

                DataRow[] arrtemp = m_dtChitietPhieuCLS.Select(KcbChidinhclsChitiet.Columns.IdChitietdichvu + "=" + Utility.sDbnull(dr[QheCamchidinhChungphieu.Columns.IdDichvu]));
                if (arrtemp.Length > 0)
                {

                    foreach (DataRow dr1 in arrtemp)
                    {
                        tempt = string.Empty;
                        _key = idDichvuchitiet.ToString() + "-" + Utility.sDbnull(dr1[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            tempt = string.Format("{0} - {1}", tenChitiet, Utility.sDbnull(dr1[DmucDichvuclsChitiet.Columns.TenChitietdichvu], ""));
                        }
                        if (tempt != string.Empty)
                            reval += tempt + "\n";
                    }
                }
            }
            return reval;
        }
        private void AddDetail(int id_goi)
        {
            try
            {
                string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
                isSaved = false;
                bool selectnew = false;
                bool canhbao = THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINHCLS_CANHBAOTRUNGDICHVU_TRONGNGAY", "1", false) == "1";
                GridEXRow[] arrCheckList = grdDichvuCLS.GetCheckedRows();
                foreach (GridEXRow gridExRow in arrCheckList)
                {
                    if (!kiemtrathoigianchidinhcls(gridExRow)) continue;
                    if (!kiemtratrungchidinhcls_trongngay_tieptuc(gridExRow, canhbao)) continue;
                    Int32 ServiceDetail_Id = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    EnumerableRowCollection<DataRow> query = from loz in m_dtChitietPhieuCLS.AsEnumerable().Cast<DataRow>()
                                                             where
                                                                 Utility.Int32Dbnull(
                                                                     loz[KcbChidinhclsChitiet.Columns.IdChitietdichvu], -1) ==
                                                                 ServiceDetail_Id
                                                             select loz;
                    if (!query.Any())
                    {
                        DataRow newDr = m_dtChitietPhieuCLS.NewRow();
                        newDr[KcbChidinhclsChitiet.Columns.IdChitietchidinh] = -1;
                        newDr["id_dangky"] = Utility.Int32Dbnull(gridExRow.Cells["id_dangky"].Value, -1);
                        newDr["id_goi"] = Utility.Int32Dbnull(gridExRow.Cells["id_goi"].Value, -1);
                        newDr["chophep_denghi_mg"] = Utility.Int32Dbnull(gridExRow.Cells["chophep_denghi_mg"].Value, 0);
                        newDr["tyle_mg"] = Utility.ByteDbnull(gridExRow.Cells["tyle_mg"].Value, 0);
                        newDr["nhom_in_cls"] = Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value, "");
                        newDr["stt_hthi_dichvu"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                        newDr["stt_hthi_chitiet"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.IdGoi] = id_goi;
                        newDr["ten_goi"] = cboGoi.Text;
                        newDr[KcbChidinhclsChitiet.Columns.IdChidinh] = v_AssignId;
                        newDr[KcbChidinhclsChitiet.Columns.IdDichvu] = Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu] = Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.PtramBhyt] = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                        //Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PtramBhyt].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.TyleTt] = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.GiaDanhmuc] = Utility.DecimaltoDbnull(gridExRow.Cells["gia_goc"].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.LoaiChietkhau] = Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.LoaiChietkhau].Value);
                        newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");
                        newDr[DmucDichvuclsChitiet.Columns.TinhChkhau] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TinhChkhau].Value, "");
                        newDr["IsNew"] = 1;
                        newDr["NoSave"] = 1;
                        newDr["IsLocked"] = 0;
                        newDr[DmucDoituongkcb.Columns.IdDoituongKcb] = objLuotkham.IdDoituongKcb;
                        newDr[KcbChidinhclsChitiet.Columns.HienthiBaocao] = 1;
                        newDr[KcbChidinhclsChitiet.Columns.SoLuong] = 1;// Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 1);
                        newDr[KcbChidinhclsChitiet.Columns.TuTuc] = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value);
                        newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                        newDr[KcbChidinhclsChitiet.Columns.MadoituongGia] = Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.MadoituongGia].Value, "");
                        newDr[KcbChidinhclsChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
                        chkTutuc.Checked = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value, 0) == 1;
                        //Phân tích đơn giá theo các thành phần
                        //Nếu đối tượng BHYT dùng giá tự túc thì phân tích giá để nguyên, chỉ trường tự túc đánh dấu =1
                        THU_VIEN_CHUNG.Bhyt_PhantichGia(objLuotkham, newDr);
                        newDr[KcbChidinhclsChitiet.Columns.NguoiTao] = globalVariables.UserName;
                        newDr[KcbChidinhclsChitiet.Columns.NgayTao] = globalVariables.SysDate;
                        errMsg_temp = KiemtraCamchidinhchungphieu(Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], 0), Utility.sDbnull(newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu], ""));
                        if (errMsg_temp != string.Empty)
                        {
                            errMsg += errMsg_temp;
                        }
                        else
                        {
                            m_dtChitietPhieuCLS.Rows.Add(newDr);
                            if (!selectnew)
                            {
                                Utility.GonewRowJanus(grdChitietChidinhCLS, KcbChidinhclsChitiet.Columns.IdChitietdichvu, Utility.sDbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "0"));
                                selectnew = true;
                            }
                        }
                    }
                }
                if (errMsg != string.Empty)
                {
                    if (errMsg.Contains("Single-Service:"))
                    {
                        Utility.ShowMsg("Dịch vụ sau được đánh dấu không được phép kê chung với bất kỳ dịch vụ nào. Đề nghị bạn kiểm tra lại:\n" + Utility.DoTrim(errMsg.Replace("Single-Service:", "")));
                    }
                    else
                        Utility.ShowMsg("Các cặp dịch vụ sau đã được thiết lập chống chỉ định chung phiếu. Đề nghị bạn kiểm tra lại:\n" + errMsg);
                }
                m_dtChitietPhieuCLS.AcceptChanges();
                //UpdateDataWhenChanged();
                m_dtDanhsachDichvuCLS.AcceptChanges();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void AddDetail()
        {
            try
            {
                string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
                isSaved = false;
                bool selectnew = false;
                bool canhbao = THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINHCLS_CANHBAOTRUNGDICHVU_TRONGNGAY", "1", false) == "1";
                GridEXRow[] arrCheckList = grdDichvuCLS.GetCheckedRows();
                foreach (GridEXRow gridExRow in arrCheckList)
                {
                    if (!kiemtrathoigianchidinhcls(gridExRow)) continue;
                    if (!kiemtratrungchidinhcls_trongngay_tieptuc(gridExRow, canhbao)) continue;
                    Int32 ServiceDetail_Id = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    EnumerableRowCollection<DataRow> query = from loz in m_dtChitietPhieuCLS.AsEnumerable().Cast<DataRow>()
                                                             where
                                                                 Utility.Int32Dbnull(
                                                                     loz[KcbChidinhclsChitiet.Columns.IdChitietdichvu], -1) ==
                                                                 ServiceDetail_Id
                                                             select loz;
                    if (!query.Any())
                    {
                        DataRow newDr = m_dtChitietPhieuCLS.NewRow();
                        newDr[KcbChidinhclsChitiet.Columns.IdChitietchidinh] = -1;
                        newDr["id_dangky"] = Utility.Int32Dbnull(gridExRow.Cells["id_dangky"].Value, -1);
                        newDr["id_goi"] = Utility.Int32Dbnull(gridExRow.Cells["id_goi"].Value, -1);
                        newDr["ten_goi"] = "Ngoài gói";
                        newDr["nhom_in_cls"] = Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value, "");
                        newDr["stt_hthi_dichvu"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                        newDr["stt_hthi_chitiet"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.IdChidinh] = v_AssignId;
                        newDr[KcbChidinhclsChitiet.Columns.IdDichvu] = Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu] = Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.PtramBhyt] = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                        //Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PtramBhyt].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.TyleTt] = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.GiaDanhmuc] = Utility.DecimaltoDbnull(gridExRow.Cells["gia_goc"].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.LoaiChietkhau] = Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.LoaiChietkhau].Value);
                        newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");
                        newDr[DmucDichvuclsChitiet.Columns.TinhChkhau] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TinhChkhau].Value, "");
                        newDr["IsNew"] = 1;
                        newDr["NoSave"] = 1;
                        newDr["IsLocked"] = 0;
                        newDr[DmucDoituongkcb.Columns.IdDoituongKcb] = objLuotkham.IdDoituongKcb;
                        newDr[KcbChidinhclsChitiet.Columns.HienthiBaocao] = 1;
                        newDr[KcbChidinhclsChitiet.Columns.SoLuong] = 1;// Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 1);
                        newDr[KcbChidinhclsChitiet.Columns.TuTuc] = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value);
                        newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                        newDr[KcbChidinhclsChitiet.Columns.MadoituongGia] = Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.MadoituongGia].Value, "");
                        newDr[KcbChidinhclsChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
                        chkTutuc.Checked = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value, 0) == 1;
                        //Phân tích đơn giá theo các thành phần
                        //Nếu đối tượng BHYT dùng giá tự túc thì phân tích giá để nguyên, chỉ trường tự túc đánh dấu =1
                        THU_VIEN_CHUNG.Bhyt_PhantichGia(objLuotkham, newDr);
                        newDr[KcbChidinhclsChitiet.Columns.NguoiTao] = globalVariables.UserName;
                        newDr[KcbChidinhclsChitiet.Columns.NgayTao] = globalVariables.SysDate;
                        errMsg_temp = KiemtraCamchidinhchungphieu(Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], 0), Utility.sDbnull(newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu], ""));
                        if (errMsg_temp != string.Empty)
                        {
                            errMsg += errMsg_temp;
                        }
                        else
                        {
                            m_dtChitietPhieuCLS.Rows.Add(newDr);
                            if (!selectnew)
                            {
                                Utility.GonewRowJanus(grdChitietChidinhCLS, KcbChidinhclsChitiet.Columns.IdChitietdichvu, Utility.sDbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "0"));
                                selectnew = true;
                            }
                        }
                    }
                }
                if (errMsg != string.Empty)
                {
                    if (errMsg.Contains("Single-Service:"))
                    {
                        Utility.ShowMsg("Dịch vụ sau được đánh dấu không được phép kê chung với bất kỳ dịch vụ nào. Đề nghị bạn kiểm tra lại:\n" + Utility.DoTrim(errMsg.Replace("Single-Service:", "")));
                    }
                    else
                        Utility.ShowMsg("Các cặp dịch vụ sau đã được thiết lập chống chỉ định chung phiếu. Đề nghị bạn kiểm tra lại:\n" + errMsg);
                }
                m_dtChitietPhieuCLS.AcceptChanges();
                //UpdateDataWhenChanged();
                m_dtDanhsachDichvuCLS.AcceptChanges();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        void AutoWarning()
        {
            try
            {
                string Canhbaotamung = THU_VIEN_CHUNG.Canhbaotamung(objLuotkham);
               Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], Canhbaotamung, true);

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        private void ModifyButtonCommand()
        {
            cmdDelete.Enabled = grdChitietChidinhCLS.GetCheckedRows().Length > 0;
            cmdSave.Enabled = grdChitietChidinhCLS.RowCount > 0;
        }

        private void cmdSearchKhoa_Click(object sender, EventArgs e)
        {
            //frm_YHHQ_KHOANOITRU frm = new frm_YHHQ_KHOANOITRU();
            //frm.ShowDialog();
            //if (frm.m_blnCancel)
            //{
            //    cboKhoaNoitru.SelectedIndex = Utility.GetSelectedIndex(cboKhoaNoitru, frm.Department_ID.ToString());
            //}
        }

        private void uncheckItems()
        {
            if (grdDichvuCLS.RowCount <= 0) return;
            grdDichvuCLS.UnCheckAllRecords();
            //try
            //{
            //    foreach (GridEXRow _item in grdServiceDetail.GetCheckedRows())
            //    {
            //        _item.IsChecked = false;
            //    }
            //}
            //catch
            //{
            //}
        }

        private void grdServiceDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdDichvuCLS.Focused)
                if (e.KeyCode == Keys.Enter)
                {
                    cmdAddDetail.PerformClick();
                    txtFilterName.SelectAll();
                    txtFilterName.Focus();
                }
                else if (e.KeyCode == Keys.Space && grdDichvuCLS.CurrentColumn != null && Utility.sDbnull(grdDichvuCLS.CurrentColumn.Key, "") != "colCHON")
                {
                    grdDichvuCLS.CurrentRow.IsChecked = !grdDichvuCLS.CurrentRow.IsChecked;
                    if (chkChiDinhNhanh.Checked)
                    {
                        if (grdDichvuCLS.CurrentRow.IsChecked)
                        {
                            AddOneRow_ServiceDetail();
                        }
                    }
                }
            //if (grdServiceDetail.Focused)
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        cmdAddDetail.PerformClick();
            //        txtFilterName.SelectAll();
            //        txtFilterName.Focus();
            //    }
            //    else if (e.KeyCode == Keys.Space && Utility.sDbnull(grdServiceDetail.CurrentColumn.Key, "") != "colCHON")
            //    {
            //        grdServiceDetail.CurrentRow.IsChecked = !grdServiceDetail.CurrentRow.IsChecked;
            //        if (chkChiDinhNhanh.Checked)
            //        {
            //            if (grdServiceDetail.CurrentRow.IsChecked)
            //            {
            //                AddOneRow_ServiceDetail();
            //                //txtFilterName.SelectAll();
            //                //txtFilterName.Focus();
            //            }
            //        }
            //    }
        }

        /// <summary>
        /// hàm thực hiện việc kiểm tra số lượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                //Kiểm tra xem bản ghi đã thanh toán hay chưa?
                long id_chitietchidinh = Utility.Int64Dbnull(grdChitietChidinhCLS.GetValue("id_chitietchidinh"), -1);
                if (id_chitietchidinh > 0)
                {
                    KcbChidinhclsChitiet objchitiet = KcbChidinhclsChitiet.FetchByID(id_chitietchidinh);
                    if (objchitiet != null && Utility.ByteDbnull(objchitiet.TrangthaiThanhtoan, 0) > 0)
                    {
                        Utility.ShowMsg("Bản ghi đã được thanh toán nên bạn không được phép chỉnh sửa số lượng hoặc đơn giá");
                        e.Value = e.InitialValue;
                        return;
                    }
                }

                if (e.Column.Key == KcbChidinhclsChitiet.Columns.SoLuong)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    decimal quanlity = Utility.DecimaltoDbnull(e.InitialValue, 1);
                    decimal quanlitynew = Utility.DecimaltoDbnull(e.Value);
                    if (quanlitynew <= 0)
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải >0", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                    }
                    int id_dangky = Utility.Int32Dbnull(grdChitietChidinhCLS.GetValue("id_dangky"), -1);
                    if (id_dangky > 0)
                    {
                        //GoiTinhtrangsudung goittsd = GoiTinhtrangsudung.FetchByID(id_dangky);
                        //if (goittsd.SoluongDung < quanlitynew)
                        //{
                        //    e.Value = e.InitialValue;
                        //    string msg = string.Format("Dịch vụ trong gói : {0} đang có số lượng chỉ định: {1} nhiều hơn số lượng khả dụng: {2}. Đề nghị chỉnh lại số lượng chỉ định phù hợp",grdAssignDetail.GetValue("ten_chitietdichvu").ToString(), quanlitynew, goittsd.SoluongDung);
                        //    Utility.ShowMsg(msg);
                        //    return ;
                        //}
                        e.Value = e.InitialValue;
                        string msg = string.Format("Dịch vụ trong gói : {0}  không được phép chỉnh số lượng trên lưới. Muốn kê thêm thì lập phiếu chỉ định mới", grdChitietChidinhCLS.GetValue("ten_chitietdichvu").ToString());
                        Utility.ShowMsg(msg);
                        return;
                    }

                    GridEXRow _row = grdChitietChidinhCLS.CurrentRow;
                    string ten_dvu = _row.Cells["ten_chitietdichvu"].Value.ToString();
                    //_row.Cells["TT_BHYT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra].Value, 0)) * quanlitynew;
                    //_row.Cells["TT_BN"].Value =
                    //    (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) +
                    //     Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    //_row.Cells["TT_PHUTHU"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    //_row.Cells["TT_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0)) / 100 * quanlitynew;
                    //_row.Cells["TT_BN_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) * quanlitynew;

                    _row.Cells["TT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) + Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    grdChitietChidinhCLS.UpdateData();
                    int record = new Update(KcbChidinhclsChitiet.Schema)
                       .Set(KcbChidinhclsChitiet.Columns.SoLuong).EqualTo(quanlitynew)
                       .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_chitietchidinh).Execute();
                    if (record > 0)
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa số lượng dịch vụ cận lâm sàng {0} từ {1} thành {2} thành công ", ten_dvu, Utility.FormatCurrencyHIS(quanlity), Utility.FormatCurrencyHIS(quanlitynew)), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                }
                else if (e.Column.Key == KcbChidinhclsChitiet.Columns.DonGia)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải nhập thông tin đơn giá. Vui lòng nhập lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                    }
                    decimal dongia_cu = Utility.DecimaltoDbnull(e.InitialValue, 1);
                    decimal dongia_moi = Utility.DecimaltoDbnull(e.Value);
                    GridEXRow _row = grdChitietChidinhCLS.CurrentRow;
                    string ten_dvu = _row.Cells["ten_chitietdichvu"].Value.ToString();
                    if (dongia_moi == 0)
                    {
                        if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn đổi giá dịch vụ cls {0} thành 0 đồng hay không?", ten_dvu, "Xác nhận giá 0 đồng", true)))
                        {
                            e.Value = e.InitialValue;
                            return;
                        }
                    }
                    if (dongia_moi < 0)
                    {
                        Utility.ShowMsg("Đơn giá phải >=0. Vui lòng nhập lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                    }
                    
                    int so_luong = Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 0);
                    _row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value = dongia_moi;
                    //_row.Cells["TT_BHYT"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra].Value, 0)) * so_luong;
                    //_row.Cells["TT_BN"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) + Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                    //_row.Cells["TT_PHUTHU"].Value = (Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                    //_row.Cells["TT_KHONG_PHUTHU"].Value = (dongia_moi * Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0) / 100) * so_luong;
                    ////_row.Cells["TT_BN_KHONG_PHUTHU"].Value = Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) * quanlitynew;

                    _row.Cells["TT"].Value = (dongia_moi + Utility.DecimaltoDbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * so_luong;
                    grdChitietChidinhCLS.UpdateData();
                    int record = new Update(KcbChidinhclsChitiet.Schema)
                        .Set(KcbChidinhclsChitiet.Columns.DonGia).EqualTo(dongia_moi)
                        .Set(KcbChidinhclsChitiet.Columns.BnhanChitra).EqualTo(dongia_moi)
                        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_chitietchidinh).Execute();
                    if (record > 0)
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa đơn giá dịch vụ cận lâm sàng {0} từ {1} thành {2} thành công ", ten_dvu, Utility.FormatCurrencyHIS(dongia_cu), Utility.FormatCurrencyHIS(dongia_moi)), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            ModifyButtonCommand();
        }

        /// <summary>
        /// HÀM THỰC HIỆN VIỆC CHO PHÉ THƯC HIỆN CIỆC CHỌN GÓI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTheoDoiTuong_CheckedChanged(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// HÀM THƯC HIỆN VIỆC LOAD NHỮNG DANH MỤC CLS TRONG GÓI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTrongGoi_CheckedChanged(object sender, EventArgs e)
        {
            InitData();
        }

        private void rad_TuTuc_CheckedChanged(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// hàm thưc hiện việc nhóm cận lâm sàng thong tin 
        /// khi chọn danh sách cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNhom_CLS_Click(object sender, EventArgs e)
        {
            try
            {
                //var frm = new frm_Nhom_DVuCLS();
                //frm.MaDoiTuong = Utility.sDbnull(objLuotkham.MaDoiTuong);
                //frm.ShowDialog();
                //if (frm.m_blnCancel)
                //{
                //    GridEXRow[] gridExRows = frm.gridExRows;
                //    foreach (GridEXRow gridExRow in gridExRows)
                //    {
                //        int ServiceDetail_Id = Utility.Int32Dbnull(gridExRow.Cells[LNhomDvuCl.Columns.IdDvu].Value);
                //        EnumerableRowCollection<DataRow> query =
                //            from nhom in m_dtChitietPhieuCLS.AsEnumerable().Cast<DataRow>()
                //            where
                //                Utility.Int32Dbnull(nhom[KcbChidinhclsChitiet.Columns.IdChitietdichvu], -1) == ServiceDetail_Id
                //            select nhom;

                //        if (query.Count() <= 0)
                //        {
                //            AddNhomCLS(gridExRow);
                //        }
                //    }
                //}
                //ModifyCommand();
                //ModifyButtonCommand();
            }
            catch (Exception exception)
            {
                // throw;
            }
        }

        /// <summary>
        /// hàm thực hiệnv iệc add nhóm cls vào cls đang dùng
        /// </summary>
        /// <param name="gridExRow"></param>
        private void AddNhomCLS(GridEXRow gridExRow)
        {
            //DataRow newDr = m_dtChitietPhieuCLS.NewRow();
            //newDr[KcbChidinhclsChitiet.Columns.IdChitietchidinh] = -1;

            //newDr[KcbChidinhclsChitiet.Columns.IdChidinh] = v_AssignId;
            //newDr[KcbChidinhclsChitiet.Columns.IdDichvu] =
            //    Utility.Int32Dbnull(gridExRow.Cells[LNhomDvuCl.Columns.IdLoaiDvu].Value, -1);
            //ServiceDetail_Id = Utility.Int32Dbnull(gridExRow.Cells[LNhomDvuCl.Columns.IdDvu].Value, -1);
            //newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu] = ServiceDetail_Id;
            ////newDr[KcbChidinhclsChitiet.Columns.OriginPrice] = Utility.DecimaltoDbnull(gridExRow.Cells["Price"].Value, 0);
            //newDr[KcbChidinhclsChitiet.Columns.DiscountType] = 1;
            //newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells["TEN_DVU"].Value, "");
            //newDr["IsNew"] = 1;
            //newDr["IsLocked"] = 0;
            //newDr[DmucDoituongkcb.Columns.IdDoituongKcb] = objLuotkham.ObjectTypeId;
            //newDr[KcbChidinhclsChitiet.Columns.DisplayOnReport] = 1;
            //newDr[KcbChidinhclsChitiet.Columns.SoLuong] = Utility.Int32Dbnull(gridExRow.Cells[LNhomDvuCl.Columns.SoLuong].Value, 1);
            //newDr[KcbChidinhclsChitiet.Columns.TuTuc] = 0;
            //newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells["TEN_LOAI_DVU"].Value, "");
            //newDr[KcbChidinhclsChitiet.Columns.IdBacsiChidinh] = globalVariables.gv_StaffID;
            //IEnumerable<GridEXRow> query = from dichvu in grdServiceDetail.GetDataRows().AsEnumerable()
            //                               where
            //                                   Utility.Int32Dbnull(
            //                                       dichvu.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value) ==
            //                                   ServiceDetail_Id
            //                               select dichvu;
            //if (query.Count() > 0)
            //{
            //    GridEXRow exRow = query.FirstOrDefault();
            //    newDr[KcbChidinhclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(exRow.Cells[.IdDichvu].Value, 0);
            //    newDr[KcbChidinhclsChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(exRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
            //    newDr[KcbChidinhclsChitiet.Columns.OriginPrice] = Utility.DecimaltoDbnull(exRow.Cells["Price"].Value, 0);

            //    newDr[KcbChidinhclsChitiet.Columns.IdGoiDvu] = Utility.Int32Dbnull(exRow.Cells["ID_GOI_DVU"].Value, -1);
            //    newDr[KcbChidinhclsChitiet.Columns.TrongGoi] = Utility.Int32Dbnull(exRow.Cells["TRONG_GOI"].Value, 0);
            //}
            //else
            //{
            //    SqlQuery sqlQuery = new Select().From(LObjectTypeService.Schema)
            //        .Where(LObjectTypeService.Columns.MaDtuong).IsEqualTo(objLuotkham.MaDoiTuong)
            //        .And(LNhomDvuCl.Columns.IdDvu).IsEqualTo(ServiceDetail_Id);
            //    var objectTypeService = sqlQuery.ExecuteSingle<LObjectTypeService>();
            //    if (objectTypeService != null)
            //    {
            //        newDr[KcbChidinhclsChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(objectTypeService.Surcharge, 0);
            //        newDr[KcbChidinhclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(objectTypeService.Surcharge, 0);
            //        newDr[KcbChidinhclsChitiet.Columns.IdGoiDvu] = -1;
            //        newDr[KcbChidinhclsChitiet.Columns.TrongGoi] = 0;
            //    }
            //}

            ////  newDr["TT"] = Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.PhuThu], 0) + Utility.DecimaltoDbnull(drv["Price"], 0);

            //m_dtChitietPhieuCLS.Rows.Add(newDr);
        }

        private void grdServiceDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (!m_blnAllowSelectionChanged) return;
            if (!Utility.isValidGrid(grdDichvuCLS)) return;
            if (!grdDichvuCLS.Focused && grdDichvuCLS.CurrentColumn == null)
            {
                Utility.focusCell(grdDichvuCLS, DmucDichvucl.Columns.TenDichvu);
            }
        }

        private void clearGrid(int i)
        {
            grdDichvuCLS.MoveFirst();
        }

        private void grdServiceDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsLetter(e.KeyChar))
                {
                    neverFound = false;
                    if (grdDichvuCLS.CurrentRow == null) grdDichvuCLS.MoveFirst();
                    int oldIdex = grdDichvuCLS.CurrentRow.Position;
                    CurrentRowIndex = grdDichvuCLS.CurrentRow.Position;
                    bool hasFound = false;
                    bool lastIdx = false;
                    string _Keyvalue = e.KeyChar.ToString().ToUpper();
                    object value;
                    if (CurrentRowIndex + 1 > grdDichvuCLS.RowCount - 1)
                    {
                        grdDichvuCLS.MoveFirst();
                        grdDichvuCLS.Focus();
                        for (int j = 0; j <= oldIdex; j++)
                        {
                            value = grdDichvuCLS.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                            if (value != null &&
                                value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                _Keyvalue)
                                hasFound = true;
                            if (hasFound) break;
                            if (!hasFound && j <= grdDichvuCLS.RowCount - 1)
                            {
                                grdDichvuCLS.MoveNext();
                                grdDichvuCLS.Focus();
                            }
                        }
                    }
                    else
                    {
                        for (int i = CurrentRowIndex + 1; i <= grdDichvuCLS.RowCount - 1; i++)
                        {
                            grdDichvuCLS.MoveNext();
                            grdDichvuCLS.Focus();
                            value = grdDichvuCLS.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                            if (value != null &&
                                value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                _Keyvalue)
                                hasFound = true;

                            if (hasFound) break;
                        }
                        if (!hasFound && oldIdex > 0)
                        {
                            grdDichvuCLS.MoveFirst();
                            grdDichvuCLS.Focus();

                            for (int j = 0; j <= oldIdex; j++)
                            {
                                value = grdDichvuCLS.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                                if (value != null &&
                                    value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                    _Keyvalue)
                                    hasFound = true;
                                if (hasFound) break;
                                if (!hasFound && j <= grdDichvuCLS.RowCount - 1)
                                {
                                    grdDichvuCLS.MoveNext();
                                    grdDichvuCLS.Focus();
                                }
                            }
                        }
                    }
                    if (!hasFound) grdDichvuCLS.MoveToRowIndex(oldIdex - 1);
                    CurrentRowIndex = grdDichvuCLS.CurrentRow.Position;
                }
            }
            catch (Exception)
            {
                
                
            }
            
        }

        private void grdAssignDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.TotalRow)
            {
                e.Row.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value = "Tổng cộng :";
            }
        }

        private void cmdNhom_CLS_Click_1(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của dịch vụ khi lọc thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _rowFilter = "1=1";
            try
            {
                if (cboDichVu.SelectedIndex > 0)
                {
                    _rowFilter = string.Format("{0}={1}", DmucDichvuclsChitiet.Columns.IdDichvu,
                                               Utility.Int32Dbnull(cboDichVu.SelectedValue, -1));
                }
            }

            catch (Exception)
            {
                _rowFilter = "1=1";
            }
            m_dtDanhsachDichvuCLS.DefaultView.RowFilter = _rowFilter;
            m_dtDanhsachDichvuCLS.AcceptChanges();
            
            if (grdDichvuCLS.RowCount > 0)
            {
                grdDichvuCLS.Focus();
                grdDichvuCLS.MoveFirst();
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdDichvuCLS.RootTable.Columns[DmucDichvucl.Columns.TenDichvu];
                grdDichvuCLS.Col = gridExColumn.Position;
            }
        }

        private void grdServiceDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (chkChiDinhNhanh.Checked || AutoAddAfterCheck)
            {
                if (grdDichvuCLS.CurrentRow.IsChecked)
                {
                    AddOneRow_ServiceDetail();
                }
            }
        }
        bool kiemtratrungchidinhcls_trongngay_tieptuc(GridEXRow gridExRow, bool canhbao)
        {
            try
            {

                int id_chitietdichvu = Utility.Int32Dbnull(gridExRow.Cells["id_chitietdichvu"].Value, -1);
                string ten_dichvu = Utility.sDbnull(gridExRow.Cells["ten_chitietdichvu"].Value, "");

                DataTable dtCheck = SPs.ChidinhclsKiemtrachidinhtrungTrongNgay(id_chitietdichvu, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, dtRegDate.Value.ToString("dd/MM/yyyy")).GetDataSet().Tables[0];
                if (dtCheck.Rows.Count > 0)//Kiểm tra
                {

                    if (canhbao)//Cảnh báo và hỏi
                    {
                        if (Utility.AcceptQuestion(string.Format("Dịch vụ {0} đã được chỉ định trong ngày {1} tại phiếu có Id={2}, mã chỉ định ={3}. Bạn có muốn tiếp tục thêm chỉ định này hay không?", ten_dichvu, dtRegDate.Value.ToString("dd/MM/yyyy"), Utility.sDbnull(dtCheck.Rows[0]["id_chidinh"], ""), Utility.sDbnull(dtCheck.Rows[0]["ma_chidinh"], "")), "Cảnh báo", true))
                        {
                            return true;
                        }
                        else
                            return false;
                    }//chặn ko cho chỉ định dịch vụ này để sang dịch vụ kế tiếp
                    else
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return true;
            }
        }
        bool kiemtrathoigianchidinhcls(GridEXRow gridExRow)
        {
            try
            {

                int id_chitietdichvu = Utility.Int32Dbnull(gridExRow.Cells["id_chitietdichvu"].Value, -1);
                string ten_dichvu = Utility.sDbnull(gridExRow.Cells["ten_chitietdichvu"].Value,"");
                int songay_chophep_chidinhtiep = Utility.Int32Dbnull(gridExRow.Cells["songay_chophep_chidinhtiep"].Value, 0);
                if (songay_chophep_chidinhtiep > 0)
                {
                    DataTable dtthoigian = SPs.KcbChidinhclsKiemtrathoigianchidinh(id_chitietdichvu, objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                    if (dtthoigian.Rows.Count > 0)//Kiểm tra
                    {
                        DateTime ngaychidinhgannhat = Convert.ToDateTime(dtthoigian.Rows[0]["ngaychidinh_gannhat"]);
                        int songay = (DateTime.Now - ngaychidinhgannhat).Days;
                        if (songay < songay_chophep_chidinhtiep)//Chưa đến ngày được chỉ định
                        {
                            if (Utility.AcceptQuestion(string.Format("Dịch vụ {0} chưa đến ngày được chỉ định cho lần khám tiếp theo. Còn {1} ngày mới sử dụng tiếp dịch vụ. \n Bạn có muốn tiếp tục chỉ định dịch vụ không?", ten_dichvu, (songay_chophep_chidinhtiep - songay)), "Cảnh báo", true))
                            {
                                return true;
                            }
                            else
                            return false;
                        }
                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }
        private void AddOneRow_ServiceDetail()
        {
            try
            {
                string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
                GridEXRow gridExRow = grdDichvuCLS.CurrentRow;
                bool canhbao = THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINHCLS_CANHBAOTRUNGDICHVU_TRONGNGAY", "1", false) == "1";
                if (!kiemtrathoigianchidinhcls(gridExRow)) return;
                if (!kiemtratrungchidinhcls_trongngay_tieptuc(gridExRow, canhbao)) return;
                resetNewItem();
                Int32 IdChitietdichvu = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);
                EnumerableRowCollection<DataRow> query = from loz in m_dtChitietPhieuCLS.AsEnumerable().Cast<DataRow>()
                                                         where
                                                             Utility.Int32Dbnull(
                                                                 loz[KcbChidinhclsChitiet.Columns.IdChitietdichvu], -1) ==
                                                             IdChitietdichvu
                                                         select loz;
                if (query.Count() <= 0)
                {
                    DataRow newDr = m_dtChitietPhieuCLS.NewRow();
                    newDr[KcbChidinhclsChitiet.Columns.IdChitietchidinh] = -1;
                    newDr["nhom_in_cls"] = Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value, "");
                    newDr["id_goi"] = Utility.Int32Dbnull(gridExRow.Cells["id_goi"].Value, -1);
                    newDr["id_dangky"] = Utility.Int32Dbnull(gridExRow.Cells["id_dangky"].Value, -1);
                    newDr["stt_hthi_dichvu"] =
                        Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                    newDr["stt_hthi_chitiet"] =
                        Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);
                    newDr[KcbChidinhclsChitiet.Columns.IdChidinh] = v_AssignId;
                    newDr[KcbChidinhclsChitiet.Columns.IdDichvu] =
                        Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                    newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu] =
                        Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    newDr[KcbChidinhclsChitiet.Columns.PtramBhyt] = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    //Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PtramBhyt].Value, 0);
                    newDr[KcbChidinhclsChitiet.Columns.DonGia] =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                    newDr[KcbChidinhclsChitiet.Columns.TyleTt] =
                   Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TyleTt].Value, 0);
                    newDr[KcbChidinhclsChitiet.Columns.GiaDanhmuc] = Utility.DecimaltoDbnull(gridExRow.Cells["gia_goc"].Value, 0);
                    newDr[KcbChidinhclsChitiet.Columns.LoaiChietkhau] =
                        Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.LoaiChietkhau].Value);
                    newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");
                    newDr[DmucDichvuclsChitiet.Columns.TinhChkhau] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TinhChkhau].Value, "");
                    newDr["IsNew"] = 1;
                    newDr["IsLocked"] = 0;
                    newDr["NoSave"] = 1;
                    newDr[DmucDoituongkcb.Columns.IdDoituongKcb] = objLuotkham.IdDoituongKcb;
                    newDr[KcbChidinhclsChitiet.Columns.HienthiBaocao] = 1;
                    newDr[KcbChidinhclsChitiet.Columns.SoLuong] = 1;// Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 1);
                    newDr[KcbChidinhclsChitiet.Columns.TuTuc] = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value);
                    newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                    newDr[KcbChidinhclsChitiet.Columns.MadoituongGia] = Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.MadoituongGia].Value, "");
                    newDr[KcbChidinhclsChitiet.Columns.PhuThu] =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
                    chkTutuc.Checked = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value, 0) == 1;
                    //Phân tích đơn giá theo các thành phần
                    //Nếu đối tượng BHYT dùng giá tự túc thì phân tích giá để nguyên, chỉ trường tự túc đánh dấu =1
                    THU_VIEN_CHUNG.Bhyt_PhantichGia(objLuotkham, newDr);
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_KIEMTRACAMCHIDINHCHUNG", "1", false) == "1")
                    {
                        errMsg_temp =
                            KiemtraCamchidinhchungphieu(
                                Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], 0),
                                Utility.sDbnull(newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu], ""));
                    }
                    else
                    {
                        errMsg_temp = string.Empty;
                    }
                    if (errMsg_temp != string.Empty)
                    {
                        errMsg += errMsg_temp;
                    }
                    else
                    {
                        m_dtChitietPhieuCLS.Rows.Add(newDr);
                        Utility.GonewRowJanus(grdChitietChidinhCLS, KcbChidinhclsChitiet.Columns.IdChitietdichvu, Utility.sDbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "0"));
                        m_dtDanhsachDichvuCLS.AcceptChanges();
                    }
                }
                if (errMsg != string.Empty)
                {
                    if (errMsg.Contains("Single-Service:"))
                    {
                        Utility.ShowMsg("Dịch vụ sau được đánh dấu không được phép kê chung với bất kỳ dịch vụ nào. Đề nghị bạn kiểm tra lại:\n" + Utility.DoTrim(errMsg.Replace("Single-Service:", "")));
                    }
                    else
                        Utility.ShowMsg("Các cặp dịch vụ sau đã được thiết lập chống chỉ định chung phiếu. Đề nghị bạn kiểm tra lại:\n" + errMsg);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ex.Message);
            }
            finally
            {
                ModifyButtonCommand();
            }
        }

        private void chkChiDinhNhanh_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void grdAssignDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void grdAssignDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "ghi_chu")
            {
                string GhiChu = Utility.sDbnull(grdChitietChidinhCLS.GetValue(KcbChidinhclsChitiet.Columns.GhiChu), "");

              int ra=  new Update(KcbChidinhclsChitiet.Schema)
                    .Set(KcbChidinhclsChitiet.Columns.GhiChu).EqualTo(GhiChu)
                    .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(
                        Utility.Int32Dbnull(grdChitietChidinhCLS.GetValue(KcbChidinhclsChitiet.Columns.IdChitietchidinh))).Execute();
                //grdAssignDetail.CurrentRow.BeginEdit();
                //grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NgaySua].Value = globalVariables.SysDate;
                //grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiSua].Value = globalVariables.UserName;
                //grdAssignDetail.CurrentRow.EndEdit();
            }
            ModifyButtonCommand();
        }

        private void grdServiceDetail_Enter(object sender, EventArgs e)
        {
           // grdServiceDetail.Col = 0;
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCLS_Selected()) return;

                long AssignDetail =
                    Utility.Int64Dbnull(grdChitietChidinhCLS.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                int idChidinh = Utility.Int32Dbnull(grdChitietChidinhCLS.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value, -1);
                CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail, idChidinh);
                grdChitietChidinhCLS.CurrentRow.Delete();
                grdChitietChidinhCLS.UpdateData();
                grdChitietChidinhCLS.Refresh();
                m_dtChitietPhieuCLS.AcceptChanges();
                if (grdChitietChidinhCLS.GetDataRows().Length <= 0)
                {
                    m_eAction = action.Insert;
                    txtAssign_ID.Text = "(Tự sinh)";
                    txtAssignCode.Text = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                }
                m_blnCancel = false;
                ModifyCommand();
                ModifyButtonCommand();
            }
            catch
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc cấu hình của cls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._HISCLSProperties);
            frm.ShowDialog();
            CauHinh();
            //InitData();
        }

        private void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuCLS_Click(object sender, EventArgs e)
        {
            ChoninphieuCLS(true);
            //try
            //{
            //    var actionResult = ActionResult.Error;
            //    string mayin = "";
            //    int v_AssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
            //    string v_AssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
            //    List<string> nhomcls = new List<string>();
            //    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            //    {
            //        if (!nhomcls.Contains(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value)))
            //            nhomcls.Add(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value));
            //    }
            //    List<long> lstSelectedPrint = (from p in grdAssignDetail.GetDataRows().AsEnumerable()
            //                                   select Utility.Int64Dbnull(p.Cells["id_chitietchidinh"].Value, 0)).ToList();
            //    string nhomincls = "ALL";
            //    if (cboServicePrint.SelectedIndex > 0)
            //    {
            //        nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");
            //        switch (cboServicePrint.SelectedIndex)
            //        {
            //            case 1:
            //                break;
            //        }
            //    }
            //    if (cboServicePrint.SelectedIndex > 0 || chkIntach.Checked)
            //    {
            //        actionResult = KcbInphieu.InTachToanBoPhieuCls(lstSelectedPrint,(int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId,
            //                                 v_AssignCode, nhomcls, Utility.sDbnull(cboServicePrint.SelectedValue, "ALL"), cboServicePrint.SelectedIndex, chkIntach.Checked,
            //                                 ref mayin);
            //    }
            //    else
            //    {
            //        actionResult = KcbInphieu.InphieuChidinhCls((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId,
            //                                      v_AssignCode, nhomincls, cboServicePrint.SelectedIndex,
            //                                      chkIntach.Checked, ref mayin);
            //    }
                       
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
        }

        private void grdServiceDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == KcbChidinhclsChitiet.Columns.SoLuong)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    decimal quanlity = Utility.DecimaltoDbnull(e.InitialValue, 1);
                    decimal quanlitynew = Utility.DecimaltoDbnull(e.Value);
                    if (quanlitynew <= 0)
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải >=1", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = quanlity;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception exception)
            {
                log.Trace("Loi : "+ exception.Message);
            }
        }



        #region "Event Common"

        private int ID_Goi_Dvu = -1;

        /// <summary>
        /// hàm thực hiện việc lấy thông tin 
        /// </summary>
        private void GetData()
        {
            log.Trace("StartLoading2.1...");
            KcbChidinhcl objKcbChidinhcls = null;
            if (Utility.Int64Dbnull(txtAssign_ID.Text) > 0)
            {

                log.Trace("StartLoading2.2...");
                objKcbChidinhcls  = KcbChidinhcl.FetchByID(Utility.Int32Dbnull(txtAssign_ID.Text, -1));
            }
            if (objKcbChidinhcls != null)
            {
                log.Trace("StartLoading2.3...");
                txtAssignCode.Text = objKcbChidinhcls.MaChidinh;
                dtRegDate.Value = objKcbChidinhcls.NgayChidinh;
                txtBacsi.SetId(Utility.sDbnull(objKcbChidinhcls.IdBacsiChidinh, ""));
            }
            else
            {
                log.Trace("StartLoading2.4...");
                if (objPhieudieutriNoitru != null)
                {
                    if (objPhieudieutriNoitru.NgayDieutri != null)
                        dtRegDate.Value = objPhieudieutriNoitru.NgayDieutri.Value;
                }
                else
                dtRegDate.Value = globalVariables.SysDate;
                txtAssignCode.Text = THU_VIEN_CHUNG.SinhMaChidinhCLS();
             
            }
            if (objLuotkham != null)
            {
                if (objBenhnhan != null)
                {
                    _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                   .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(Utility.Int32Dbnull(txtidkham.Text, -1)).
                                   ExecuteSingle
                                   <KcbChandoanKetluan>();
                    if (_KcbChandoanKetluan != null)
                    {
                        txtidchandoan.Text = Utility.sDbnull(_KcbChandoanKetluan.IdChandoan, "-1");
                        txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);

                    }
                    log.Trace("StartLoading2.5...");
                    if (objBenhnhan.NamSinh != null)
                        this.Text = @"Chỉ định dịch vụ Cận lâm sàng cho Bệnh nhân:" + objBenhnhan.TenBenhnhan
                                    + @", " + (Utility.Int32Dbnull(objBenhnhan.GioiTinh) == 0 ? "Nam" : "Nữ") + @", " +
                                    (globalVariables.SysDate.Year + 1 - objBenhnhan.NamSinh.Value) + @" tuổi";
                    // if (m_eAction == action.Update)
                    LayThongTin_Chitiet_CLS();
                    log.Trace("StartLoading2.6...");
                }
            }
            
        }

        /// <summary>
        /// ham thc hien viecj lay thông tin chi tiết của cls
        /// </summary>
        private void LayThongTin_Chitiet_CLS()
        {
            try
            {
                //if (Utility.Int64Dbnull(txtAssign_ID.Text) > 0)
                //{
                    m_dtChitietPhieuCLS = CHIDINH_CANLAMSANG.LaythongtinCLS_Thuoc(Utility.Int32Dbnull(txtAssign_ID.Text, -1), "DICHVU");
                    Utility.SetDataSourceForDataGridEx(grdChitietChidinhCLS, m_dtChitietPhieuCLS, false, true, "1=1",
                                                       "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
                    grdChitietChidinhCLS.MoveFirst();
                    ResetNhominCLS();
                    ModifyCommand();
               // }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy dữ liệu CLS chi tiết\n" + ex.Message);
            }
        }
        /// <summary>
        /// Đã được xử lý phía Client
        /// </summary>
        private void ProcessData()
        {
            //Utility.AddColumToDataTable(ref m_dtDanhsachDichvuCLS, KcbChidinhclsChitiet.Columns.TuTuc, typeof(int));
            //foreach (DataRow dr in m_dtDanhsachDichvuCLS.Rows)
            //{
            //    if (objLuotkham.MaDoituongKcb == "BHYT" && objLuotkham.DungTuyen == 0)
            //    {
            //        dr[KcbChidinhclsChitiet.Columns.PhuThu] = dr["Phuthu_traituyen"];
            //    }
            //}
            //m_dtDanhsachDichvuCLS.AcceptChanges();
        }
        DataTable _mDtqheCamchidinhClsChungphieu = new DataTable();
        /// <summary>
        /// khởi tạo thông tin của dữ liệu
        /// </summary>
        private void InitData()
        {
            try
            {
                string MA_KHOA_THIEN = globalVariables.MA_KHOA_THIEN;
                if (Utility.Int32Dbnull(objLuotkham.Noitru, 0) <= 0)
                {
                    if (objCongkham != null)
                    {
                        if (objCongkham.KhamNgoaigio == 1)
                            MA_KHOA_THIEN = "KYC";
                    }
                    else
                    {
                        if (THU_VIEN_CHUNG.IsNgoaiGio())
                        {
                            MA_KHOA_THIEN = "KYC";
                        }
                    }
                }
                else
                {
                    MA_KHOA_THIEN = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_GIACLS", false) ?? MA_KHOA_THIEN;
                }
                _mDtqheCamchidinhClsChungphieu = new Select().From(QheCamchidinhChungphieu.Schema).ExecuteDataSet().Tables[0];// globalVariables.gv_dtDmucQheCamCLSChungPhieu;
                
                ////Tạm REM 05/07/2024 để gọi theo cbo gói
              //  m_dtDanhsachDichvuCLS = CHIDINH_CANLAMSANG.LaydanhsachCLS_chidinh(objLuotkham.MaDoituongKcb,
              //                                                                    objLuotkham.TrangthaiNoitru,
              //                                                                    Utility.ByteDbnull(
              //                                                                        objLuotkham.GiayBhyt, 0), -1,
              //                                                                    Utility.Int32Dbnull(
              //                                                                        objLuotkham.DungTuyen.Value, 0),
              //                                                                    MA_KHOA_THIEN, nhomchidinh, tnv_chidinh);
              //  //Xử lý phụ thu đúng tuyến-trái tuyến
              ////  ProcessData();
              //  if (!m_dtDanhsachDichvuCLS.Columns.Contains(KcbChidinhclsChitiet.Columns.SoLuong))
              //      m_dtDanhsachDichvuCLS.Columns.Add(KcbChidinhclsChitiet.Columns.SoLuong, typeof(int));
              //  if (!m_dtDanhsachDichvuCLS.Columns.Contains("ten_donvitinh"))
              //      m_dtDanhsachDichvuCLS.Columns.Add("ten_donvitinh", typeof(string));
              //  m_dtDanhsachDichvuCLS.AcceptChanges();
              //  Utility.SetDataSourceForDataGridEx(grdServiceDetail, m_dtDanhsachDichvuCLS, false, true, "", "");
              //  GridEXColumn gridExColumnGroupIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_dichvu"];
              //  GridEXColumn gridExColumnIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_chitiet"];
              //  Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnGroupIntOrder, SortOrder.Ascending);
              //  Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnIntOrder, SortOrder.Ascending);
              //  m_dtDanhsachDichvuCLS_org = m_dtDanhsachDichvuCLS.Copy();
                txtFilterName.Focus();
                txtFilterName.SelectAll();

            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình load thông tin :" + exception.Message);
            }
        }
        #endregion

        private void mnuTuTuc_Click(object sender, EventArgs e)
        {
            try
            {
                //bool reval = false;
                //long idChidinhChitiet = Utility.Int64Dbnull(grdAssignDetail.CurrentRow.Cells["id_chitietchidinh"].Value, -1);
                byte tutuc = (byte)grdChitietChidinhCLS.CurrentRow.Cells["tu_tuc"].Value;
                //if (idChidinhChitiet > 0)
                //{
                //    KcbChidinhclsChitiet objChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(idChidinhChitiet);
                //    if (objChidinhclsChitiet != null)
                //    {
                //        if (objChidinhclsChitiet.TuTuc != null)
                //        {
                //            if (objChidinhclsChitiet.TuTuc == 1)
                //            {
                //                grdAssignDetail.CurrentRow.BeginEdit();
                //                grdAssignDetail.CurrentRow.Cells["tu_tuc"].Value = 0;
                //                grdAssignDetail.CurrentRow.EndEdit();
                //                //reval = TinhCLS.CapnhatTrangthaiTutuc(objChidinhclsChitiet, objLuotkham, false,
                //                //        0, Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0));
                //            }
                //            else
                //            {
                //                grdAssignDetail.CurrentRow.BeginEdit();
                //                grdAssignDetail.CurrentRow.Cells["tu_tuc"].Value = 1;
                //                grdAssignDetail.CurrentRow.EndEdit();
                //                //reval = TinhCLS.CapnhatTrangthaiTutuc(objChidinhclsChitiet, objLuotkham, false,
                //                //        1, Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0));
                //            }
                         
                //        }
                //    }
                //}
                //else
                //{
                    if (tutuc == 1)
                    {
                        grdChitietChidinhCLS.CurrentRow.BeginEdit();
                        grdChitietChidinhCLS.CurrentRow.Cells["tu_tuc"].Value = 0;
                        grdChitietChidinhCLS.CurrentRow.EndEdit();
                    }
                    else
                    {
                        grdChitietChidinhCLS.CurrentRow.BeginEdit();
                        grdChitietChidinhCLS.CurrentRow.Cells["tu_tuc"].Value = 1;
                        grdChitietChidinhCLS.CurrentRow.EndEdit();
                    }
                //}

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        void ChangeMenu(GridEXRow row)
        {
            mnuTuTuc.Text = Utility.GetValueFromGridColumn(row, KcbThanhtoanChitiet.Columns.TuTuc) == "1" ? "Giá đối tượng" : "Tự túc";
            mnuTuTuc.Tag = Utility.GetValueFromGridColumn(row, KcbThanhtoanChitiet.Columns.TuTuc);
        }

        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            ChangeMenu(grdChitietChidinhCLS.CurrentRow);
        }
        private readonly KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        private void cmdluuchandoan_Click(object sender, EventArgs e)
        {
            return;//Tránh lỗi mất chẩn đoán sơ bộ khi lưu
            if (!Utility.Coquyen("quyen_khamtatcacacphong_ngoaitru") &&
                (Utility.Int16Dbnull(objCongkham.IdPhongkham) !=
                 Utility.Int16Dbnull(globalVariables.IdPhongNhanvien)))
            {
                Utility.ShowMsg(
                    "Bệnh nhân không thuộc phòng khám này. \n Người dùng không được phân quyền khám tất cả các phòng!",
                    "Thông báo");
            }
            else
            {
                cmdluuchandoan.Enabled = false;
                ActionResult act = _KCB_THAMKHAM.LuuHoibenhvaChandoan(TaoDulieuChandoanKetluan(),null, null,null, false,false);
                if (act == ActionResult.Success)
                {
                    _KcbChandoanKetluan.IsNew = false;
                    _KcbChandoanKetluan.MarkOld();
                    //_KcbChandoanKetluan.Save();
                }
                cmdluuchandoan.Enabled = true;
            }
        }
        private KcbChandoanKetluan _KcbChandoanKetluan;
        private KcbChandoanKetluan TaoDulieuChandoanKetluan()
        {
            try
            {
                
                SqlQuery sqlkt = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(Utility.Int32Dbnull(txtidkham.Text,-1));

                _KcbChandoanKetluan = new KcbChandoanKetluan();
                if (_KcbChandoanKetluan == null || sqlkt.GetRecordCount() <= 0)
                {
                    _KcbChandoanKetluan.IsNew = true;
                    _KcbChandoanKetluan.IdChandoan = -1;
                    _KcbChandoanKetluan.NgayTao = globalVariables.SysDate;
                    _KcbChandoanKetluan.NguoiTao = globalVariables.UserName;
                }
                else
                {
                    KcbChandoanKetluan objchuandoan = sqlkt.ExecuteSingle<KcbChandoanKetluan>();
                    _KcbChandoanKetluan.IsNew = false;
                    _KcbChandoanKetluan.MarkOld();
                    _KcbChandoanKetluan.IdChandoan = Utility.Int64Dbnull(objchuandoan.IdChandoan, -1);
                    _KcbChandoanKetluan.NguoiSua = globalVariables.UserName;
                    _KcbChandoanKetluan.NgaySua = globalVariables.SysDate;
                    _KcbChandoanKetluan.IpMaysua = globalVariables.gv_strIPAddress;
                }
                _KcbChandoanKetluan.IdKham = Utility.Int64Dbnull(objCongkham.IdKham,-1);
                _KcbChandoanKetluan.IdPhieudieutri = objCongkham.IdPhieudieutri;
                _KcbChandoanKetluan.IdKhoanoitru = objCongkham.IdKhoadieutri;
                _KcbChandoanKetluan.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
                _KcbChandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(objLuotkham.IdBenhnhan, "-1");
                _KcbChandoanKetluan.Noitru = noitru;
                _KcbChandoanKetluan.MabenhChinh = "";
                _KcbChandoanKetluan.MotaBenhchinh = "";
                _KcbChandoanKetluan.Nhommau = "";
                _KcbChandoanKetluan.Nhietdo = "";
                _KcbChandoanKetluan.Huyetap = "";
                _KcbChandoanKetluan.Mach = "";
                _KcbChandoanKetluan.Nhiptim = "";
                _KcbChandoanKetluan.Nhiptho = "";
                _KcbChandoanKetluan.Chieucao = "";
                _KcbChandoanKetluan.Cannang = "";
                _KcbChandoanKetluan.HuongDieutri = ""; //.myCode.Trim();
                _KcbChandoanKetluan.SongayDieutri = 0;
                _KcbChandoanKetluan.SoNgayhen = 0;
                _KcbChandoanKetluan.Ketluan = "";
                _KcbChandoanKetluan.NhanXet = "";
                _KcbChandoanKetluan.ChedoDinhduong = "";
                _KcbChandoanKetluan.ChisoIbm = 0;
                _KcbChandoanKetluan.ThilucMp = "";
                _KcbChandoanKetluan.ThilucMt = "";
                _KcbChandoanKetluan.NhanapMp ="";
                _KcbChandoanKetluan.NhanapMt = "";
                if (Utility.Int16Dbnull(txtBacsi.MyID, -1) > 0)
                    _KcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID);
                else
                {
                    _KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                if (objCongkham != null)
                {
                    _KcbChandoanKetluan.IdKhoanoitru = Utility.Int32Dbnull(objCongkham.IdKhoakcb, -1);
                    _KcbChandoanKetluan.IdPhongkham = Utility.Int32Dbnull(objCongkham.IdPhongkham, -1);
                    DmucKhoaphong objDepartment =
                        DmucKhoaphong.FetchByID(Utility.Int32Dbnull(objCongkham.IdPhongkham, -1));
                    if (objDepartment != null)
                    {
                        _KcbChandoanKetluan.TenPhongkham = Utility.sDbnull(objDepartment.TenKhoaphong, "");
                    }
                }
                else
                {
                    _KcbChandoanKetluan.IdKhoanoitru = globalVariables.idKhoatheoMay;
                    _KcbChandoanKetluan.IdPhongkham = globalVariables.idKhoatheoMay;
                }
                //_KcbChandoanKetluan.IdKham = Utility.Int32Dbnull(txt_idchidinhphongkham.Text, -1);

                _KcbChandoanKetluan.NgayChandoan = dtRegDate.Value;
                _KcbChandoanKetluan.Ketluan ="";
                _KcbChandoanKetluan.Chandoan = txtChanDoan.Text;
                _KcbChandoanKetluan.ChandoanKemtheo = "";
                return _KcbChandoanKetluan;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void mnuUpdateprice_Click_1(object sender, EventArgs e)
        {

        }
     bool   allowLoadGoikham=false;
        private void cboGoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!allowLoadGoikham || Utility.Int32Dbnull(cboGoi.SelectedValue) <= 0) return;
            if (PropertyLib._HISCLSProperties.InsertAfterSelectPackage)
                AddDetailbySelectedPackage();
            else
            {
                frm_ChitietdichvuTronggoi1Lan _ChitietdichvuTronggoi1Lan = new frm_ChitietdichvuTronggoi1Lan(Utility.Int32Dbnull(cboGoi.SelectedValue), m_dtChitietPhieuCLS);
                _ChitietdichvuTronggoi1Lan._OnAccept += _ChitietdichvuTronggoi1Lan__OnAccept;
                _ChitietdichvuTronggoi1Lan.ShowDialog();
            }

        }

        private void _ChitietdichvuTronggoi1Lan__OnAccept(DataTable dtChitietdichvutronggoi)
        {
            AddDetailbySelectedPackage(dtChitietdichvutronggoi);
        }
        void LoadDichvuChidinhCLS()
        {
            if (m_dtDanhsachDichvuCLS_org != null && m_dtDanhsachDichvuCLS_org.Rows.Count > 0) m_dtDanhsachDichvuCLS = m_dtDanhsachDichvuCLS_org.Copy();
            else
            {
                m_dtDanhsachDichvuCLS = CHIDINH_CANLAMSANG.LaydanhsachCLS_chidinh(objLuotkham.MaDoituongKcb,0,
                                                                            objLuotkham.TrangthaiNoitru,
                                                                            Utility.ByteDbnull(
                                                                                objLuotkham.GiayBhyt, 0), -1,
                                                                            Utility.Int32Dbnull(
                                                                                objLuotkham.DungTuyen.Value, 0),
                                                                            globalVariables.MA_KHOA_THIEN, nhomchidinh, tnv_chidinh, dtRegDate.Value);
                if (!m_dtDanhsachDichvuCLS.Columns.Contains(KcbChidinhclsChitiet.Columns.SoLuong))
                    m_dtDanhsachDichvuCLS.Columns.Add(KcbChidinhclsChitiet.Columns.SoLuong, typeof(int));
                if (!m_dtDanhsachDichvuCLS.Columns.Contains("ten_donvitinh"))
                    m_dtDanhsachDichvuCLS.Columns.Add("ten_donvitinh", typeof(string));

                if (!m_dtDanhsachDichvuCLS.Columns.Contains("chophep_denghi_mg"))
                    m_dtDanhsachDichvuCLS.Columns.Add("chophep_denghi_mg", typeof(bool));
                if (!m_dtDanhsachDichvuCLS.Columns.Contains("tyle_mg"))
                    m_dtDanhsachDichvuCLS.Columns.Add("tyle_mg", typeof(byte));
                m_dtDanhsachDichvuCLS_org = m_dtDanhsachDichvuCLS.Copy();
            }
            m_dtDanhsachDichvuCLS.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdDichvuCLS, m_dtDanhsachDichvuCLS, false, true, "", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
        }
        void LoadDulieuGoiKhamTruduoi()
        {
            try
            {
                if (!allowLoadGoikham) return;
                if (cboGoi.SelectedValue.ToString() == "-1")
                {
                    if (m_dtDanhsachDichvuCLS_org != null && m_dtDanhsachDichvuCLS_org.Rows.Count > 0) m_dtDanhsachDichvuCLS = m_dtDanhsachDichvuCLS_org.Copy();
                    else
                    {
                        m_dtDanhsachDichvuCLS = CHIDINH_CANLAMSANG.LaydanhsachCLS_chidinh(objLuotkham.MaDoituongKcb,0,
                                                                                    objLuotkham.TrangthaiNoitru,
                                                                                    Utility.ByteDbnull(
                                                                                        objLuotkham.GiayBhyt, 0), -1,
                                                                                    Utility.Int32Dbnull(
                                                                                        objLuotkham.DungTuyen.Value, 0),
                                                                                    globalVariables.MA_KHOA_THIEN, nhomchidinh, tnv_chidinh, dtRegDate.Value);
                        if (!m_dtDanhsachDichvuCLS.Columns.Contains(KcbChidinhclsChitiet.Columns.SoLuong))
                            m_dtDanhsachDichvuCLS.Columns.Add(KcbChidinhclsChitiet.Columns.SoLuong, typeof(int));
                        if (!m_dtDanhsachDichvuCLS.Columns.Contains("ten_donvitinh"))
                            m_dtDanhsachDichvuCLS.Columns.Add("ten_donvitinh", typeof(string));

                        if (!m_dtDanhsachDichvuCLS.Columns.Contains("chophep_denghi_mg"))
                            m_dtDanhsachDichvuCLS.Columns.Add("chophep_denghi_mg", typeof(bool));
                        if (!m_dtDanhsachDichvuCLS.Columns.Contains("tyle_mg"))
                            m_dtDanhsachDichvuCLS.Columns.Add("tyle_mg", typeof(byte));
                        m_dtDanhsachDichvuCLS_org = m_dtDanhsachDichvuCLS.Copy();
                    }
                    m_dtDanhsachDichvuCLS.AcceptChanges();
                    Utility.SetDataSourceForDataGridEx(grdDichvuCLS, m_dtDanhsachDichvuCLS, false, true, "", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
                    //GridEXColumn gridExColumnGroupIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_dichvu"];
                    //GridEXColumn gridExColumnIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_chitiet"];
                    //Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnGroupIntOrder, SortOrder.Ascending);
                    //Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnIntOrder, SortOrder.Ascending);
                }
                else
                {
                    string Id = cboGoi.SelectedValue.ToString().Split('_')[0];

                    //Load các dịch vụ trong gói còn số lượng sử dụng < số lượng
                    m_dtDanhsachDichvuCLS = CHIDINH_CANLAMSANG.LaydanhsachCLS_chidinh(objLuotkham.MaDoituongKcb,0,
                                                                                   objLuotkham.TrangthaiNoitru,
                                                                                   Utility.ByteDbnull(
                                                                                       objLuotkham.GiayBhyt, 0), Utility.Int32Dbnull(Id, -1),
                                                                                   Utility.Int32Dbnull(
                                                                                       objLuotkham.DungTuyen.Value, 0),
                                                                                   globalVariables.MA_KHOA_THIEN, nhomchidinh, tnv_chidinh, dtRegDate.Value);
                    if (!m_dtDanhsachDichvuCLS.Columns.Contains(KcbChidinhclsChitiet.Columns.SoLuong))
                        m_dtDanhsachDichvuCLS.Columns.Add(KcbChidinhclsChitiet.Columns.SoLuong, typeof(int));
                    if (!m_dtDanhsachDichvuCLS.Columns.Contains("ten_donvitinh"))
                        m_dtDanhsachDichvuCLS.Columns.Add("ten_donvitinh", typeof(string));

                    if (!m_dtDanhsachDichvuCLS.Columns.Contains("chophep_denghi_mg"))
                        m_dtDanhsachDichvuCLS.Columns.Add("chophep_denghi_mg", typeof(bool));
                    if (!m_dtDanhsachDichvuCLS.Columns.Contains("tyle_mg"))
                        m_dtDanhsachDichvuCLS.Columns.Add("tyle_mg", typeof(byte));
                    m_dtDanhsachDichvuCLS.AcceptChanges();
                    Utility.SetDataSourceForDataGridEx(grdDichvuCLS, m_dtDanhsachDichvuCLS, false, true, "", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
                    //GridEXColumn gridExColumnGroupIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_dichvu"];
                    //GridEXColumn gridExColumnIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_chitiet"];
                    //Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnGroupIntOrder, SortOrder.Ascending);
                    //Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnIntOrder, SortOrder.Ascending);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cboChonGoi_Click(object sender, EventArgs e)
        {
            cboGoi_SelectedIndexChanged(cboGoi, e);
        }
    }
}