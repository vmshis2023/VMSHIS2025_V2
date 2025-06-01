using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Baocao;
using Janus.Windows.GridEX;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_AddCapPhatVTTHPhongChucnang : Form
    {
        #region "khai bao biến dùng trong form"
        private DataTable dtPres;
        private DataTable dtPresDetail;
        private DataSet dsData;
        private string RowFillerPresDetail = "1=2";
        private DataTable dtDmucthuoc;
        private DataTable dtPhieucapphatchitiet;
        public action m_Action = action.Insert;
        public int _IDCAPPHAT = -1;
        public int DepartmentId = -1;
        private NLog.Logger log;
        private Int16 StockID = 0;
        public int StaffId = -1;
        public DataTable dtList = new DataTable();
        public string KIEUTHUOC_VT = "THUOC";
        int loaiphieu = 0;//0: Don thuoc thuong;1: Don thuoc bo sung
        bool m_blnHasLoaded = false;
        bool m_blnAllowCellChanged = true;
        public delegate void OnInsertCompleted(long idcapphat);
        public event OnInsertCompleted _OnInsertCompleted; 
        DataTable dtKho = new DataTable();
        #endregion

        #region "khoi tao thong tin du lieu cua form"
        /// <summary>
        /// khởi tạo thông tin cấp phát Thuốc/VTTH nội trú
        /// </summary>
        public frm_AddCapPhatVTTHPhongChucnang(string KIEUTHUOC_VT, int loaiphieu)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
            this.KIEUTHUOC_VT = KIEUTHUOC_VT;
            this.loaiphieu = loaiphieu;
            
            log = LogManager.GetCurrentClassLogger();
            
           
            StaffId = globalVariables.gv_intIDNhanvien;
            dtpInputDate.Value = globalVariables.SysDate;
        }
        void InitEvents()
        {
            this.KeyDown += new KeyEventHandler(frm_AddCapPhatVTTHPhongChucnang_KeyDown);
            grdPres.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(grdPres_CellValueChanged);
            grdPres.SelectionChanged += new EventHandler(grdPres_SelectionChanged);
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.Load += new System.EventHandler(this.frm_AddCapPhatVTTHPhongChucnang_Load);
            grdPres.ColumnHeaderClick += grdPres_ColumnHeaderClick;
            grdPres.RowCheckStateChanged += grdPres_RowCheckStateChanged;
            chkDaochon.CheckedChanged += chkDaochon_CheckedChanged;
            chkCheckAll.CheckedChanged += chkCheckAll_CheckedChanged;
            cboStatus.SelectedIndexChanged+=cboStatus_SelectedIndexChanged;
            

        }

        void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                m_blnAllowCellChanged = false;
                foreach (GridEXRow _row in grdPres.GetDataRows())
                {
                    ((DataRowView)_row.DataRow).Row["CHON"] = chkCheckAll.Checked ? 1 : 0;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                m_blnAllowCellChanged = true;
                TongHopThuoc();
            }
        }

        void chkDaochon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                m_blnAllowCellChanged = false;
                foreach (GridEXRow _row in grdPres.GetDataRows())
                {
                    ((DataRowView)_row.DataRow).Row["CHON"] = ((DataRowView)_row.DataRow).Row["CHON"].ToString() == "0" ? 1 : 0;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                m_blnAllowCellChanged = true;
                TongHopThuoc();
            }
        }

        void grdPres_RowCheckStateChanged(object sender, Janus.Windows.GridEX.RowCheckStateChangeEventArgs e)
        {
            //try
            //{
            //    if (e.Row != null)
            //    {
            //        ((DataRowView)e.Row.DataRow).Row["CHON"] = e.CheckState == RowCheckState.Checked ? 1 : 0;
            //        //TongHopThuoc();
            //    }
            //}
            //catch (Exception ex)
            //{
                
                
            //}
           
        }

        void grdPres_ColumnHeaderClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            //m_blnAllowCellChanged = false;
            //foreach (GridEXRow _row in grdPres.GetDataRows())
            //{
            //    ((DataRowView)_row.DataRow).Row["CHON"] = _row.CheckState == RowCheckState.Checked ? 1 : 0;
            //}
            //TongHopThuoc();
            //m_blnAllowCellChanged = true;
            
        }
        void frm_AddCapPhatVTTHPhongChucnang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            else if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            else if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
        }
        #endregion
        #region "khởi tạo thông tin khi load form hiện tại"
        /// <summary>
        /// hàm hực hiện việc load lại danh mục khởi tạo thông tni 
        /// </summary>
        private void InitData()
        {
            try
            {
                LoadDepartments();

                cboStaff.Enabled = globalVariables.IsAdmin;
                dtTuNgay.Value = DateTime.Now.AddDays(-2);
                dtDenNgay.Value = DateTime.Now.AddDays(2);
                cboStatus.SelectedIndex = 0;

                if (KIEUTHUOC_VT == "THUOC")
                {
                    dtKho = CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(-1, -1, "TATCA,NOITRU", "THUOC,THUOCVT", 0, 100); // CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_TUTRUC_NOITRU(KIEUTHUOC_VT);
                }
                else
                {
                    dtKho = CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(-1, -1, "TATCA,NOITRU", "VT,THUOCVT", 0, 100); // CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA",  "NOITRU" });
                }

                DataBinding.BindDataCombobox(cboKhoxuat, dtKho,
                                       TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                       "---Chọn kho xuất---", false);

                DataTable dtDoctorAssign = THU_VIEN_CHUNG.Laydanhsachnhanvien("ALL");
                DataBinding.BindDataCombox(cboStaff, dtDoctorAssign, DmucNhanvien.Columns.IdNhanvien,
                                            DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---", true);
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    if (cboStaff.Items.Count > 0)
                        cboStaff.SelectedIndex = 0;
                }
                else
                {
                    if (cboStaff.Items.Count > 0)
                        cboStaff.SelectedIndex = Utility.GetSelectedIndex(cboStaff,
                                                                                 globalVariables.gv_intIDNhanvien.ToString());
                }
               
            }
            catch
            {
            }
            finally
            {
            }

        }
        /// <summary>
        /// hàm thực hiện việc load dữ liệu của khoa
        /// </summary>
        private void LoadDepartments()
        {
            try
            {
                DataBinding.BindDataCombobox(cboDepartment,THU_VIEN_CHUNG.LaydanhsachKhoatheoUser(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin),100),
                                                  //THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0),
                                                  DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                  "---Chọn khoa nội trú---",false);
                cboDepartment.SelectedIndex = Utility.GetSelectedIndex(cboDepartment, globalVariablesPrivate.objKhoaphong.IdKhoaphong.ToString());
            }
            catch(Exception exception)
            {
                log.Error("loi trong qua trinh khoi tao danh muc ,exception=" + exception.ToString());
            }
        }

        #endregion
        /// <summary>
        /// hàm thực hiện việc load form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_AddCapPhatVTTHPhongChucnang_Load(object sender, EventArgs e)
        {
            try
            {
                InitData();
                lblKhoxuat.Text = KIEUTHUOC_VT == "THUOC" ? "Kho Thuốc/VTTH" : "Kho vật tư:";// optPhieulinhthuoc.Checked = "THUOC,ALL".Contains(KIEUTHUOC_VT);
                if (m_Action == action.Update)
                {
                    TPhieuCapphatNoitru objTPhieuCapphatNoitru = new Select().From(TPhieuCapphatNoitru.Schema)
                        .Where(TPhieuCapphatNoitru.IdCapphatColumn).IsEqualTo(_IDCAPPHAT).ExecuteSingle<TPhieuCapphatNoitru>();
                    if (objTPhieuCapphatNoitru != null)
                    {
                        cboDepartment.SelectedIndex = Utility.GetSelectedIndex(cboDepartment, objTPhieuCapphatNoitru.IdKhoaLinh.ToString());
                        m_blnHasLoaded = true;
                        cboDepartment_SelectedIndexChanged(cboDepartment, e);
                        _IDCAPPHAT = objTPhieuCapphatNoitru.IdCapphat;
                        dtTuNgay.Text = objTPhieuCapphatNoitru.TuNgay.ToString("dd/MM/yyyy");
                        dtDenNgay.Text = objTPhieuCapphatNoitru.DenNgay.ToString("dd/MM/yyyy");
                        dtpInputDate.Text = objTPhieuCapphatNoitru.NgayNhap.ToString("dd/MM/yyyy");
                        cboKhoxuat.SelectedIndex = Utility.GetSelectedIndex(cboKhoxuat, objTPhieuCapphatNoitru.IdKhoXuat.ToString());
                        cboStaff.SelectedIndex = Utility.GetSelectedIndex(cboStaff, objTPhieuCapphatNoitru.IdNhanvien.ToString());
                       
                        if (objTPhieuCapphatNoitru.LoaiPhieu == 0)
                            optLinhThuong.Checked = true;
                        else
                            optLinhBoSung.Checked = true;
                    }
                    else
                    {
                        return;
                    }

                    LaythongtinTongHopDonthuoc(DepartmentId, 1);
                    SetEnabledButton(false);
                    grbFilter.Enabled = true;
                    cboStatus.SelectedIndex = 1;
                    cmdPrint.Enabled = true;
                    cmdPrintDetail.Enabled = true;
                }
                else
                {
                    grbFilter.Height = 0;
                    dtDenNgay.Value = globalVariables.SysDate;
                    dtTuNgay.Value = globalVariables.SysDate.AddDays(-1 * PropertyLib._DuocNoitruProperties.Songayluitimdonthuoc);
                }
                

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                if (m_Action == action.Insert)
                {
                    m_blnHasLoaded = true;
                    cboDepartment_SelectedIndexChanged(cboDepartment, e);
                }
            }
        }
        bool UpdateNgaykedon = false;
        /// <summary>
        /// hàm thực hiẹn việc chấp nhận thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(cboDepartment.SelectedValue, -1) == -1)
            {
                Utility.ShowMsg("Bạn phải chọn khoa nội trú", "Thông báo", MessageBoxIcon.Warning);
                cboDepartment.Focus();
                log.Info("khoa khong duoc de trong thong tin");
                return ;
            }

            if (Utility.Int32Dbnull(cboStaff.SelectedValue, -1) == -1)
            {
                Utility.ShowMsg("Bạn phải chọn người lập phiếu", "Thông báo", MessageBoxIcon.Warning);
                log.Info("Thong tin cua nhan vien khong duoc bo trong");
                cboStaff.Focus();
                return ;
            }
            if (Utility.Int32Dbnull(cboKhoxuat.SelectedValue, -1) == -1)
            {
                Utility.ShowMsg("Bạn phải chọn kho xuất Thuốc/VTTH", "Thông báo", MessageBoxIcon.Warning);
                log.Info("Thong tin cua nhan vien khong duoc bo trong");
                cboKhoxuat.Focus();
                return;
            }
            if (dtpInputDate.Value.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
            {
                if (!Utility.AcceptQuestion("Ngày tạo phiếu khác ngày hiện tại. Bạn có chắc chắn không?\nNhấn Yes để tiếp tục. Nhấn No để chọn lại ngày tạo phiếu", "", true))
                {
                    dtpInputDate.Focus();
                    return;
                }
            }
            SetStatus();
        }
        void SetStatus()
        {
            switch(cmdOK.ToolTipText)
            {
                case "0":
                    SetEnabledButton(false);
                    UpdateNgaykedon = true;
                    LaythongtinTongHopDonthuoc(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1), Utility.Int32Dbnull(cboStatus.SelectedValue, -1));
                    break;
                case "1":
                    SetEnabledButton(true);
                    dtPres.Clear();
                    dtPresDetail.Clear();
                    dtDmucthuoc.Clear();
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện trạng thái của nút thông tin 
        /// </summary>
        /// <param name="visible"></param>
        void SetEnabledButton(bool visible)
        {
            dtpInputDate.Enabled = visible;
            dtDenNgay.Enabled = visible;
            dtTuNgay.Enabled = visible;
            cboStaff.Enabled = visible;
            cboDepartment.Enabled = visible;
            cboKhoxuat.Enabled = visible;
            pnlLoaiphieu.Enabled = visible;
            cmdOK.ToolTipText = visible ? "0" : "1";
            cmdOK.Text = visible ? "Chấp nhận" : "Chọn lại";
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khởi tạo đơn Thuốc/VTTH tổng hợp
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="Status"></param>
        private void LaythongtinTongHopDonthuoc(int DepartmentID,int Status)
        {
            try
            {
                byte noitru =Utility.ByteDbnull( optNoitru.Checked ? 0 : 1);
                int id_kho =Utility.Int32Dbnull( cboKhoxuat.SelectedValue,-1);
                int id_khoa = Utility.Int32Dbnull(cboDepartment.SelectedValue, -1);
                if (m_Action == action.Insert) Status = 0;//Nếu thêm mới ko cho phép load các đơn đã cấp phát cho dù chọn tất cả
                else Status = 1;//nếu là Update hiển thị tất cả các đơn Thuốc/VTTH chưa đề nghị cấp phát+các đơn Thuốc/VTTH được cấp phát cho phiếu đang làm
                dsData = SPs.ThuocNoitruLaydanhsachdonthuocTonghoplinhVTTHPhongChucnang(id_khoa, id_kho, noitru, 4, KIEUTHUOC_VT, (Int16)Status, _IDCAPPHAT, chkCheck.Checked ? dtTuNgay.Value.ToString("dd/MM/yyyy") : "01/01/1900", chkCheck.Checked ? dtDenNgay.Value.ToString("dd/MM/yyyy") : "01/01/1900").GetDataSet();
                
                dtDmucthuoc = dsData.Tables[2];
                //Đơn Thuốc/VTTH và chi tiết cấp phát
                dtPhieucapphatchitiet = dsData.Tables[3];
                //Chi tiết đơn Thuốc/VTTH
                dtPresDetail = dsData.Tables[1];
                grdPresDetail.DataSource = dtPresDetail.DefaultView;
                //Danh sách các đơn Thuốc/VTTH đã hoặc chưa cấp phát
                dtPres = dsData.Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPres,dtPres,true,false,"1=1","");
                grdPres.MoveFirst();
                //Tự động check chọn các đơn Thuốc/VTTH trong phiếu cấp phát
                Tudongchondonthuocdatonghop();
                //Tổng hợp lại thông tin Thuốc/VTTH từ các đơn đã chọn-->lẽ ra mục này phải tự động load từ bảng Detail chứ ko cần tổng hợp lại-->SỬA SAU
                TongHopThuoc();
                grdPres_SelectionChanged(grdPres, new EventArgs());
            }
            catch(Exception ex)
            {
                log.ErrorException("Loi trong qua trinh khoi tao thong tin ",ex);
            }
        }
        /// <summary>
        /// hàm thực hiện việc lấy tự động chọn đơn Thuốc/VTTH
        /// </summary>
        void Tudongchondonthuocdatonghop()
        {
            try
            {
                m_blnAllowCellChanged = false;
                if (dtPres != null && dtPhieucapphatchitiet != null)
                {
                    foreach (DataRow dr in dtPres.Rows)
                    {
                        if (Utility.sDbnull( dr["tthai_tonghop"],"0") =="1")
                            dr["CHON"] = 1;
                        else
                            dr["CHON"] = 0;
                        //REM ĐOẠN DƯỚI 230615
                        //DataRow[] arrDr = dtPhieucapphatchitiet.Select(TPhieuCapphatChitiet.Columns.IdDonthuoc + "=" + dr[TPhieuCapphatChitiet.Columns.IdDonthuoc]);
                        //if (arrDr.Length > 0)
                        //    dr["CHON"] = 1;
                        //else
                        //    dr["CHON"] = 0;
                    }
                    dtPres.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
                m_blnAllowCellChanged = true;
            }

        }
        /// <summary>
        /// di chuyển trên lưới thông tin của đơn Thuốc/VTTH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                RowFillerPresDetail = "1=2";
                int IdDonthuoc = -1;
                if (grdPres.RowCount > 0 && grdPres.CurrentRow != null)
                {
                    IdDonthuoc = Utility.Int32Dbnull(grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.IdDonthuoc].Value, -1);
                    RowFillerPresDetail = KcbDonthuoc.Columns.IdDonthuoc + " = " + IdDonthuoc;
                }
                dtPresDetail.DefaultView.RowFilter = RowFillerPresDetail;
                dtPresDetail.AcceptChanges();
                grdPresDetail.MoveFirst();
                log.Info("Loc thong tin chi tiet voi id_donthuoc=" + IdDonthuoc);
            }
            catch (Exception ex)
            {
                
               log.Error("Loi trong qua trinh di chuyen tren luoi loc thong tin thuoc="+ex.ToString());
            }
           
        }
        /// <summary>
        /// hàm thực hiện việc tổng hợp thông tin Thuốc/VTTH
        /// </summary>
        void TongHopThuoc()
        {
            dtDmucthuoc.Clear();
            grdPres.UpdateData();
            dtPhieucapphatchitiet.Clear();
            //foreach(GridEXRow grd in grdPres.GetCheckedRows())
            //{
            //    DataRow row = ((DataRowView)grd.DataRow).Row;
            foreach(DataRow row in dtPres.Select("CHON=1"))
            {
                foreach (DataRow dr in dtPresDetail.Select("id_donthuoc = " + row["id_donthuoc"]))
                {
                    DataRow drNew = dtDmucthuoc.NewRow();
                    drNew[DmucThuoc.Columns.IdThuoc] = dr[DmucThuoc.Columns.IdThuoc];
                    drNew[DmucThuoc.Columns.TenThuoc] = dr[DmucThuoc.Columns.TenThuoc];
                    drNew["tenthuoc_bietduoc"] = dr["tenthuoc_bietduoc"];
                    drNew[DmucThuoc.Columns.SoDangky] = dr[DmucThuoc.Columns.SoDangky];
                    drNew[DmucThuoc.Columns.HangSanxuat] = dr[DmucThuoc.Columns.HangSanxuat];
                    drNew[DmucThuoc.Columns.QD31] = dr[DmucThuoc.Columns.QD31];
                    drNew[DmucThuoc.Columns.NuocSanxuat] = dr[DmucThuoc.Columns.NuocSanxuat];
                    drNew[DmucThuoc.Columns.HamLuong] = dr[DmucThuoc.Columns.HamLuong];
                    drNew["ten_donvitinh"] = dr["ten_donvitinh"];
                    drNew[DmucLoaithuoc.Columns.TenLoaithuoc] = dr[DmucLoaithuoc.Columns.TenLoaithuoc];
                    drNew[KcbDonthuocChitiet.Columns.SoLuong] = dr[KcbDonthuocChitiet.Columns.SoLuong];
                    dtDmucthuoc.Rows.Add(drNew);

                    DataRow drDetail = dtPhieucapphatchitiet.NewRow();
                    drDetail[TPhieuCapphatChitiet.Columns.IdCapphat] = 0;
                    drDetail[TPhieuCapphatChitiet.Columns.DaLinh] = 0;
                    drDetail[TPhieuCapphatChitiet.Columns.IdBenhnhan] = row[TPhieuCapphatChitiet.Columns.IdBenhnhan];
                    drDetail[TPhieuCapphatChitiet.Columns.MaLuotkham] = row[TPhieuCapphatChitiet.Columns.MaLuotkham];
                    drDetail[TPhieuCapphatChitiet.Columns.NgayKedon] = row[TPhieuCapphatChitiet.Columns.NgayKedon];
                    drDetail[TPhieuCapphatChitiet.Columns.IdKho] = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdKho], -1);
                    drDetail[TPhieuCapphatChitiet.Columns.IdThuockho] = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdThuockho], -1);
                    drDetail[TPhieuCapphatChitiet.Columns.IdDonthuoc] = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdDonthuoc], -1);
                    drDetail[TPhieuCapphatChitiet.Columns.IdChitietdonthuoc] = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1);
                    drDetail[TPhieuCapphatChitiet.Columns.IdThuoc] = dr[DmucThuoc.Columns.IdThuoc];
                    drDetail[TPhieuCapphatChitiet.Columns.SoLuong] = dr["Quantity"];
                    dtPhieucapphatchitiet.Rows.Add(drDetail);
                }
            }
            dtPhieucapphatchitiet.AcceptChanges();
            dtDmucthuoc.AcceptChanges();
            RefeshDataAfrerUpdate();

        }
        /// <summary>
        /// làm tươi lại thông tin sau khi update thông tin 
        /// </summary>
        void RefeshDataAfrerUpdate()
        {
            DataTable dtData = dtDmucthuoc.Clone();
            foreach (DataRow dr in dtDmucthuoc.Rows)
            {
                DataRow[] arrDr = dtData.Select(DmucThuoc.Columns.IdThuoc + "=" + Utility.sDbnull(dr[DmucThuoc.Columns.IdThuoc], "-1"));
                if (arrDr.Length <= 0)
                {
                    dtData.ImportRow(dr);
                }
                else
                {
                    arrDr[0][TThuockho.Columns.SoLuong] = Utility.DecimaltoDbnull(
                            arrDr[0][TThuockho.Columns.SoLuong], 0) + Utility.DecimaltoDbnull(dr[TThuockho.Columns.SoLuong], 0);
                }
            }
            Utility.SetDataSourceForDataGridEx_Basic(grdTongHop, dtData, true, true, "", "ten_thuoc");
            log.Info("Gan thong tin du lieu cua phan thong tin chi tiet vao luoi tong hop");
            grdTongHop.MoveFirst();
            
        }
        /// <summary>
        /// thực hiện việc lưu lại thông tin của đơnt huốc tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }
        /// <summary>
        /// kiểm tra thông tin 
        /// </summary>
        /// <returns></returns>
        bool IsValidData ()
        {
            try
            {
                //if (dtpInputDate.Value < dtDenNgay.Value)
                //{
                //    Utility.ShowMsg("Ngày lập phiếu cấp phát phải >= ngày điều trị đến", "Thông báo", MessageBoxIcon.Warning);
                //    dtpInputDate.Focus();
                //    return false;
                //}
                if (m_Action == action.Update)
                {
                    TPhieuCapphatNoitru objTPhieuCapphatNoitru = new Select().From(TPhieuCapphatNoitru.Schema)
                        .Where(TPhieuCapphatNoitru.IdCapphatColumn).IsEqualTo(_IDCAPPHAT).ExecuteSingle<TPhieuCapphatNoitru>();
                    if (objTPhieuCapphatNoitru == null)
                    {
                        Utility.ShowMsg("Phiếu cấp phát tổng hợp có thể đã bị xóa bởi người khác trong lúc bạn đang mở xem nên bạn không được phép thao tác. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                        return false;
                    }
                    else
                    {
                        if (objTPhieuCapphatNoitru.TrangThai>=1)
                        {
                            Utility.ShowMsg("Phiếu cấp phát tổng hợp đã được cấp phát trong lúc bạn đang mở xem nên bạn không được phép sửa/xóa phiếu. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                            return false;
                        }
                    }
                }

                if (Utility.Int32Dbnull(cboDepartment.SelectedValue, -1) == -1)
                {
                    Utility.ShowMsg("Bạn phải chọn khoa nội trú để tổng hợp","Thông báo",MessageBoxIcon.Warning);
                    cboDepartment.Focus();
                    log.Info("khoa khong duoc de trong thong tin");
                    return false;
                }

                if (Utility.Int32Dbnull(cboStaff.SelectedValue, -1) == -1)
                {
                    Utility.ShowMsg("Bạn phải chọn người tạo phiếu","Thông báo",MessageBoxIcon.Warning);
                    log.Info("Thong tin cua nhan vien khong duoc bo trong");
                    cboStaff.Focus();
                    return false;
                }

                if(dtPres.Select("CHON=1").Length<=0)
                {
                    Utility.ShowMsg("Bạn phải chọn đơn Thuốc/VTTH cần cấp phát","Thông báo",MessageBoxIcon.Warning);
                    Utility.focusCell(grdPres, "CHON");
                    return false;
                }
                if (grdTongHop.GetDataRows().Length<=0)
                {
                    Utility.ShowMsg("Không có dữ liệu tổng hợp Thuốc/VTTH-->Bạn nên chọn chức năng xóa phiếu nếu muốn xóa bỏ phiếu lĩnh Thuốc/VTTH này", "Thông báo", MessageBoxIcon.Warning);
                    grdTongHop.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Có lỗi trong khi kiểm tra dữ liệu /n " + ex);
                log.Error("Loi duoc ban ra vói message="+ex.ToString());
                return false;
            }

        }
        void PerformAction()
        {
            if (!IsValidData())
                return;
            switch (m_Action)
            {
                case action.Insert:
                    InsertData();
                    break;
                case action.Update:
                    UpdataData();
                    break;

            }
            this.Close();
        }
        /// <summary>
        /// update thông tin của phân tạo phiếu thông tin của phần de nghi cap phat thuoc noi tru
        /// </summary>
        void UpdataData()
        {
            try
            {
               List< TThuocCapphatChitiet> lstTThuocCapphatChitiet = Taodulieuchitietcapphat();
               TPhieuCapphatNoitru objTPhieuCapphatNoitru = TaoPhieuCapphatNoitru();
                if (lstTThuocCapphatChitiet == null || lstTThuocCapphatChitiet.Count() <= 0)
                {
                    //Utility.ShowMsg("Chưa có Thuốc/VTTH nào được chọn cấp phát");
                    return;
                }
                ActionResult actionResult = new CapphatThuocKhoa().CappnhatPhieucapphatNoitru(objTPhieuCapphatNoitru, TaoPhieuCapphatChitiet(), lstTThuocCapphatChitiet);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        //Utility.ShowMsg("Cập nhật thông tin thành công");
                        cmdPrint.Enabled = true;
                        cmdPrintDetail.Enabled = true;
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình lưu thông tin");
                        break;

                }
            }
            catch (Exception ex)
            {
                log.ErrorException("ban ra Exception=",ex);
                Utility.ShowMsg("Có lỗi trong quá trình lưu thông tin /n" + ex.ToString());

            }
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới 
        /// </summary>
        /// <param name="DeliveryId"></param>
        void ProcessDataInsert(int DeliveryId)
        {
            DataRow drNew = dtList.NewRow();
            drNew[TPhieuCapphatNoitru.Columns.NgayNhap] =globalVariables.SysDate;
            drNew[TPhieuCapphatNoitru.Columns.IdCapphat] = DeliveryId;
            drNew[TPhieuCapphatNoitru.Columns.IdKhoXuat] = StockID;
            drNew["ten_khoaphong"] = cboDepartment.Text;
            drNew["ten_kho"] = cboKhoxuat.Text;
            drNew[TPhieuCapphatNoitru.Columns.IdKhoaLinh] = Utility.Int32Dbnull(cboDepartment.SelectedValue, -1);
            drNew[TPhieuCapphatNoitru.Columns.IdNhanvien] = Utility.Int16Dbnull(cboStaff.SelectedValue);
            drNew["ten_nhanvien"] = cboStaff.Text;
            drNew["ten_nhanvien_phatthuoc"] = "";
            drNew["DA_LINH"] = 0;
            drNew["CHUA_LINH"] = dtDmucthuoc.Rows.Count;
            drNew["TOTAL"] = dtDmucthuoc.Rows.Count;
            drNew[TPhieuCapphatNoitru.Columns.TrangThai] = 0;
            drNew["ten_trangthai"] = "Chưa duyệt";
            drNew["mota_them"] = "Chưa được kho dược phát Thuốc/VTTH";
            dtList.Rows.InsertAt(drNew, 0);
            dtList.AcceptChanges();
            
        }

        /// <summary>
        /// in đơn thuôc đề nghị câp phát Thuốc/VTTH nội trú
        /// </summary>
        void PrintPhieuDenghiCapPhat()
        {
            try
            {
                int idcapphat = _IDCAPPHAT;
                DataTable dtDmucthuoc = SPs.ThuocNoitruLaydulieuinphieulinhthuocnoitru(idcapphat).GetDataSet().Tables[0];
                TPhieuCapphatNoitru objPhieuCapphatNoitru = TPhieuCapphatNoitru.FetchByID(idcapphat);
                if (objPhieuCapphatNoitru != null)
                {
                    thuoc_baocao.Inphieutonghoplinhthuocnoitru(objPhieuCapphatNoitru, dtDmucthuoc);
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy dữ liệu");
                return;
            }
        }

        /// <summary>
        /// thêm thông tni của đề nghị cấp phat Thuốc/VTTH nọi trú 
        /// </summary>
        void InsertData()
        {
            try
            {
              List< TThuocCapphatChitiet> lstTThuocCapphatChitiet= Taodulieuchitietcapphat();
               TPhieuCapphatNoitru objTPhieuCapphatNoitru=TaoPhieuCapphatNoitru();
               if (lstTThuocCapphatChitiet == null || lstTThuocCapphatChitiet.Count() <= 0)
               {
                   //Utility.ShowMsg("Chưa có Thuốc/VTTH nào được chọn cấp phát");
                   return;
               }
               ActionResult actionResult = new CapphatThuocKhoa().ThemPhieuCapPhatNoiTru(objTPhieuCapphatNoitru, TaoPhieuCapphatChitiet(), lstTThuocCapphatChitiet);

                switch (actionResult)
                {
                    case ActionResult.Success:
                        //Chuyển về trạng thái Update
                        m_Action = action.Update;
                        _IDCAPPHAT = objTPhieuCapphatNoitru.IdCapphat;
                        cmdPrint.Enabled = true;
                        cmdPrintDetail.Enabled = true;
                        ProcessDataInsert(_IDCAPPHAT);
                        if (_OnInsertCompleted != null) _OnInsertCompleted(_IDCAPPHAT);
                        //Utility.ShowMsg("Đã thực hiện cấp phát Thuốc/VTTH thành công\nBạn có thể in phiếu cấp phát Thuốc/VTTH tổng hợp hoặc phiếu cấp phát Thuốc/VTTH chi tiết để mang đến quầy Thuốc/VTTH xin cấp phát.");
                        cmdPrint.Focus();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình lưu thông tin");
                        break;

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lưu thông tin /n" + ex.ToString());
               
            }
        }



        TPhieuCapphatNoitru TaoPhieuCapphatNoitru()
        {
            try
            {
                TPhieuCapphatNoitru objPhieucapphatnoitru = new TPhieuCapphatNoitru();
                if (m_Action == action.Update)
                {
                    TPhieuCapphatNoitruCollection lst = new Select().From(TPhieuCapphatNoitru.Schema)
                        .Where(TPhieuCapphatNoitru.IdCapphatColumn).IsEqualTo(_IDCAPPHAT).ExecuteAsCollection<TPhieuCapphatNoitruCollection>();
                    if (lst.Count > 0) objPhieucapphatnoitru = lst[0];
                }
                if (m_Action == action.Update)
                {
                    objPhieucapphatnoitru.IdCapphat = _IDCAPPHAT;
                    if (UpdateNgaykedon)
                    {
                        objPhieucapphatnoitru.TuNgay = dtTuNgay.Value.Date;
                        objPhieucapphatnoitru.DenNgay = dtDenNgay.Value.Date;
                    }
                    objPhieucapphatnoitru.NgaySua = globalVariables.SysDate;
                    objPhieucapphatnoitru.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    objPhieucapphatnoitru.TuNgay = dtTuNgay.Value.Date;
                    objPhieucapphatnoitru.DenNgay = dtDenNgay.Value.Date;
                    objPhieucapphatnoitru.NgayTao = globalVariables.SysDate;
                    objPhieucapphatnoitru.NguoiTao = globalVariables.UserName;
                    objPhieucapphatnoitru.IdKhoaLinh = Utility.Int32Dbnull(cboDepartment.SelectedValue, -1);
                    objPhieucapphatnoitru.IdNhanvien = Utility.Int32Dbnull(cboStaff.SelectedValue, -1);
                    objPhieucapphatnoitru.TrangThai = 0;
                }
                objPhieucapphatnoitru.LoaiPhieu = 4;//0= lĩnh thường;1= lĩnh bổ sung
                objPhieucapphatnoitru.IdKhoXuat = StockID;
                objPhieucapphatnoitru.KieuThuocVt = KIEUTHUOC_VT;
                objPhieucapphatnoitru.NgayNhap = dtpInputDate.Value;
                objPhieucapphatnoitru.MotaThem = optLinhBoSung.Checked ? "Phiếu tổng hợp lĩnh Thuốc/VTTH(VT) bổ sung" : "Phiếu tổng hợp lĩnh Thuốc/VTTH(VT) thường";

                return objPhieucapphatnoitru;
            }
            catch (Exception)
            {
                return null;
            }
        }
       List< TThuocCapphatChitiet> Taodulieuchitietcapphat()
        {
            try
            {
                DataTable v_dtData = ((DataView)grdTongHop.DataSource).Table;
                StockID = Utility.Int16Dbnull(cboKhoxuat.SelectedValue,-1);
                List<TThuocCapphatChitiet> lstTThuocCapphatChitiet = new List<TThuocCapphatChitiet>();
                //DataTable v_dtStock = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_NOITRU();

                //if (v_dtStock.Rows.Count > 1)
                //{
                //    frmChooseStock v_frmChooseStock = new frmChooseStock();
                //    v_frmChooseStock.m_dtStock = v_dtStock;
                //    v_frmChooseStock.ShowDialog();
                //    if (!v_frmChooseStock.m_blnCancel)
                //        StockID = v_frmChooseStock.m_intStockID;
                //}
                //else if (v_dtStock.Rows.Count == 1)
                //    StockID = Convert.ToInt16(v_dtStock.Rows[0][DKho.Columns.IdKho]);
                if (StockID == -1)
                {
                    Utility.ShowMsg("Bạn cần chọn kho dược nội trú để cấp phát");
                    return null;
                }
                int idx = 0;
                foreach (DataRow row in v_dtData.Rows)
                {
                   TThuocCapphatChitiet _newItem = new TThuocCapphatChitiet();
                   _newItem.IdCapphat = -1;
                   _newItem.IdThuoc = Utility.Int32Dbnull(row[DmucThuoc.Columns.IdThuoc], 0);
                   _newItem.SoLuong = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0);
                   lstTThuocCapphatChitiet.Add(_newItem);
                }
                if (lstTThuocCapphatChitiet == null || lstTThuocCapphatChitiet.Count() <= 0)
                {
                    Utility.ShowMsg("Chưa có Thuốc/VTTH nào được chọn cấp phát");
                    return null;
                }
                return lstTThuocCapphatChitiet;
            }
            catch (Exception)
            {

                return null;
            }
        }
       List< TPhieuCapphatChitiet> TaoPhieuCapphatChitiet()
        {
            try
            {
                List<TPhieuCapphatChitiet> lstCapphatchitiet = new List<TPhieuCapphatChitiet>();
                int idx = 0;
                foreach (DataRow row in dtPhieucapphatchitiet.Rows)
                {
                 TPhieuCapphatChitiet   _newItem = new TPhieuCapphatChitiet();
                 _newItem.IdCapphat = -1;
                 _newItem.DaLinh = 0;
                 _newItem.IdBenhnhan = Utility.Int32Dbnull(row[TPhieuCapphatChitiet.Columns.IdBenhnhan], -1);
                 _newItem.MaLuotkham = Utility.sDbnull(row[TPhieuCapphatChitiet.Columns.MaLuotkham],"");
                 _newItem.NgayKedon = Convert.ToDateTime(row[TPhieuCapphatChitiet.Columns.NgayKedon]);
                 _newItem.ThucLinh = 0;
                 _newItem.SoLuongtralai = 0;
                 _newItem.IdKho = Utility.Int32Dbnull(row[TPhieuCapphatChitiet.Columns.IdKho], -1);
                 _newItem.IdThuockho = Utility.Int32Dbnull(row[TPhieuCapphatChitiet.Columns.IdThuockho], -1);
                 _newItem.IdThuoc = Utility.Int32Dbnull(row[TPhieuCapphatChitiet.Columns.IdThuoc], -1);
                 _newItem.SoLuong = Utility.DecimaltoDbnull(row[TPhieuCapphatChitiet.Columns.SoLuong], -1);
                 _newItem.IdChitietdonthuoc = Utility.Int32Dbnull(row[TPhieuCapphatChitiet.Columns.IdChitietdonthuoc], -1);
                 _newItem.IdDonthuoc = Utility.Int32Dbnull(row[TPhieuCapphatChitiet.Columns.IdDonthuoc], -1);
                 lstCapphatchitiet.Add(_newItem);
                }
                return lstCapphatchitiet;
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        

        private void grdPres_CellValueChanged(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            if (m_blnAllowCellChanged)
                TongHopThuoc();
        }

        

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            PerformAction();
            PrintPhieuDenghiCapPhat();
        }
        
        /// <summary>
        /// thựchiện trạng thái câp phát Thuốc/VTTH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnHasLoaded) return;
                string status = cboStatus.SelectedIndex.ToString();
                if (status == "-1" || status == "2")
                    dtPres.DefaultView.RowFilter = "1=1";
                else
                    dtPres.DefaultView.RowFilter = "tthai_tonghop=" + status;
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện thoát formhiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }

        private void cboStatus_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnHasLoaded || cboDepartment.SelectedValue==null) return;
                
                short IDKhoa =Utility.Int16Dbnull( cboDepartment.SelectedValue,-1);
                //DataRow[] arrdr = dtKho.Select("ID_KHOAPHONG=" + IDKhoa + " or id_khoaphong_captren=" + IDKhoa);
                //DataTable _newTable = dtKho.Clone();
                //if (arrdr.Length > 0) _newTable = arrdr.CopyToDataTable();

                DataTable _newTable = CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(IDKhoa, -1, "TATCA,NOITRU,NGOAITRU", KIEUTHUOC_VT, 100, 100); 
                DataBinding.BindDataCombobox(cboKhoxuat, _newTable,
                                         TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                         "---Chọn tủ Thuốc/VTTH---", true);
                if (_newTable.Rows.Count == 2)
                {
                    cboKhoxuat.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
