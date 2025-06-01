 using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;

using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VMS.HIS.DAL;
using VNS.HIS.UI.THUOC;
using VNS.HIS.UI.DANHMUC;
using VNS.Properties;
using VNS.HIS.UI.Forms.DUOC;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_qhe_doituong_thuoc_coban : Form
    {
        private DataTable m_dtQheDoituongThuoc=new DataTable();
        private DataTable m_dataThuoc=new DataTable();
        action m_enAction = action.Insert;
        string arg = "QHEGIATHUOC-THUOC";
        string Kieuthuoc_vt = "THUOC";
        public bool m_blnCancel = true;
        public bool AutoNew = false;
        public frm_qhe_doituong_thuoc_coban(string arg)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.arg = arg;
            if (arg.Split('-').Length == 2)
            {
                this.arg = arg.Split('-')[0];
                Kieuthuoc_vt = arg.Split('-')[1];
            }
            InitEvents();
            printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyPreview = true;
            ModifyUI();
        }
        void ModifyUI()
        {
            switch (arg)
            {
                case "DMUC":
                    cmdCauhinh.Visible = false;
                    cmdInGiathuoc.Visible = false;
                    tabObjectType.Visible = false;
                    tabObjectType.Width = 0;
                    cmdCauhinhgia.Visible = false;
                    this.Text = Kieuthuoc_vt == "THUOC" ? "Danh mục thuốc" : "Danh mục Vật tư";
                    break;
                case "GIA":
                    cmdThemMoi.Visible = false;
                    cmdCauhinhgia.Visible = true;
                    cmdCapNhap.Visible = false;
                    cmdXoathuoc.Visible = false;
                    this.Text = Kieuthuoc_vt == "THUOC" ? "Quản lý giá thuốc" : "Quản lý giá Vật tư";
                    break;
                case "DMUC_GIA":
                    this.Text = "Quản lý thuốc + giá thuốc";
                    break;
            }
        }
        void InitEvents()
        {
            cboloaithuoc.SelectedIndexChanged += new EventHandler(cboloaithuoc_SelectedIndexChanged);
            cboloaithuoc.KeyDown += cboloaithuoc_KeyDown;
            cmdAdd.Click+=new EventHandler(cmdAdd_Click);
            cmdDelete.Click+=new EventHandler(cmdDelete_Click);
            cmdSaveObjectAll.Click+=new EventHandler(cmdSaveObjectAll_Click);
            cmdDetailDeleteAll.Click+=new EventHandler(cmdDetailDeleteAll_Click);
            this.cmdThemMoi.Click += new System.EventHandler(this.cmdThemMoi_Click);
            this.cmdInGiathuoc.Click += new System.EventHandler(this.cmdInGiathuoc_Click);
            cmdIndanhsachthuoc.Click += new EventHandler(cmdIndanhsachthuoc_Click);
            this.cmdExportExcel.Click += new System.EventHandler(this.cmdExportExcel_Click);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            cmdCapNhap.Click += new EventHandler(cmdCapNhap_Click);
            this.cboKhoaTH.SelectedIndexChanged += new System.EventHandler(this.cboKhoaTH_SelectedIndexChanged);
            this.chkExpand.CheckedChanged += new System.EventHandler(this.chkExpand_CheckedChanged);
            this.Load += new System.EventHandler(this.frm_qhe_doituong_thuoc_coban_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_qhe_doituong_thuoc_coban_KeyDown);
            grdList.ApplyingFilter+=new System.ComponentModel.CancelEventHandler(grdList_ApplyingFilter);
            grdList.SelectionChanged+=new EventHandler(grdList_SelectionChanged);
            grdList.UpdatingCell += grdList_UpdatingCell;
            optTatca.CheckedChanged += new EventHandler(_CheckedChanged);
            optHethieuluc.CheckedChanged += new EventHandler(_CheckedChanged);
            optHieuluc.CheckedChanged += new EventHandler(_CheckedChanged);

            mnuHethieuluc.Click += new EventHandler(mnuHethieuluc_Click);
            mnuUpdate.Click += new EventHandler(mnuUpdate_Click);
            mnuDelete.Click += new EventHandler(mnuDelete_Click);
            cmdXoathuoc.Click += new EventHandler(cmdXoathuoc_Click);
            cmdCauhinh.Click += new EventHandler(cmdCauhinh_Click);
            cmdCauhinhgia.Click += new EventHandler(cmdCauhinhgia_Click);

            grdQhe.CellUpdated += new ColumnActionEventHandler(grdQhe_CellUpdated);
            grdQhe.EditingCell += new EditingCellEventHandler(grdQhe_EditingCell);
            grdQhe.UpdatingCell += grdQhe_UpdatingCell;
            optQhe_tatca.CheckedChanged += new EventHandler(_CheckedChanged);
            optCoQhe.CheckedChanged += new EventHandler(_CheckedChanged);
            optKhongQhe.CheckedChanged += new EventHandler(_CheckedChanged);
            optTatcachia.CheckedChanged += _CheckedChanged;
            optCochia.CheckedChanged += _CheckedChanged;
            optKhongchia.CheckedChanged += _CheckedChanged;
            this.Shown += frm_qhe_doituong_thuoc_coban_Shown;

        }

        void grdQhe_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                decimal gia_cu = Utility.DecimaltoDbnull(e.InitialValue,0);
                if (e.Column.Key == "don_gia")
                {
                    int num = new Update(QheDoituongThuoc.Schema)
                    .Set(QheDoituongThuoc.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(grdQhe.GetValue("don_gia")))
                        .Where(QheDoituongThuoc.Columns.IdQuanhe).IsEqualTo(Utility.Int64Dbnull(grdQhe.GetValue("id_quanhe"))).Execute();
                    if (num > 0) Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa Đơn giá thuốc (quan hệ) ID thuốc={0}, tên thuốc={1},gia_cu={2}, giá mới={3} thành công ", grdList.CurrentRow.Cells["id_thuoc"].Value.ToString(), grdList.CurrentRow.Cells["ten_thuoc"].Value.ToString(), gia_cu, grdQhe.GetValue("don_gia").ToString()), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                }
                else if (e.Column.Key == "phuthu_dungtuyen")
                {
                    int num = new Update(QheDoituongThuoc.Schema)
                    .Set(QheDoituongThuoc.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(grdQhe.GetValue("phuthu_dungtuyen")))
                        .Where(QheDoituongThuoc.Columns.IdQuanhe).IsEqualTo(Utility.Int64Dbnull(grdQhe.GetValue("id_quanhe"))).Execute();
                    if (num > 0) Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa Đơn giá thuốc (quan hệ) ID thuốc={0}, tên thuốc={1},gia_cu={2}, giá mới={3} thành công ", grdList.CurrentRow.Cells["id_thuoc"].Value.ToString(), grdList.CurrentRow.Cells["ten_thuoc"].Value.ToString(), gia_cu, grdQhe.GetValue("phuthu_dungtuyen").ToString()), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                }
                else if (e.Column.Key == "phuthu_traituyen")
                {
                    int num = new Update(QheDoituongThuoc.Schema)
                    .Set(QheDoituongThuoc.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(grdQhe.GetValue("phuthu_traituyen")))
                        .Where(QheDoituongThuoc.Columns.IdQuanhe).IsEqualTo(Utility.Int64Dbnull(grdQhe.GetValue("id_quanhe"))).Execute();
                    if (num > 0) Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa Đơn giá thuốc (quan hệ) ID thuốc={0}, tên thuốc={1},gia_cu={2}, giá mới={3} thành công ", grdList.CurrentRow.Cells["id_thuoc"].Value.ToString(), grdList.CurrentRow.Cells["ten_thuoc"].Value.ToString(), gia_cu, grdQhe.GetValue("phuthu_traituyen").ToString()), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "don_gia")
            {
                DmucThuoc objthuoc = DmucThuoc.FetchByID(Utility.Int64Dbnull( grdList.CurrentRow.Cells["id_thuoc"].Value,0));
                if (objthuoc != null)
                {
                    objthuoc.MarkOld();
                    objthuoc.IsNew = false;
                    objthuoc.DonGia = Utility.DecimaltoDbnull(e.Value, 0);
                    objthuoc.Save();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa Đơn giá thuốc (danh mục) ID thuốc={0}, tên thuốc={1}, Giá cũ={2}, giá mới={3} thành công ", grdList.CurrentRow.Cells["id_thuoc"].Value.ToString(), grdList.CurrentRow.Cells["ten_thuoc"].Value.ToString(), e.InitialValue, e.Value), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }
            }
            if (e.Column.Key == "ton_dau")
            {
                DmucThuoc objthuoc = DmucThuoc.FetchByID(Utility.Int64Dbnull(grdList.CurrentRow.Cells["id_thuoc"].Value, 0));
                if (objthuoc != null)
                {
                    objthuoc.MarkOld();
                    objthuoc.IsNew = false;
                    objthuoc.TonDau = Utility.Int32Dbnull(e.Value, 0);
                    objthuoc.Save();
                }
            }
            else if (e.Column.Key == "gia_dv")
            {
                DmucThuoc objthuoc = DmucThuoc.FetchByID(Utility.Int64Dbnull(grdList.CurrentRow.Cells["id_thuoc"].Value, 0));
                if (objthuoc != null)
                {
                    objthuoc.MarkOld();
                    objthuoc.IsNew = false;
                    objthuoc.GiaDv = Utility.DecimaltoDbnull(e.Value, 0);
                    objthuoc.Save();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa Giá dịch vụ thuốc (danh mục) ID thuốc={0}, tên thuốc={1}, Giá cũ={2}, giá mới={3} thành công ", grdList.CurrentRow.Cells["id_thuoc"].Value.ToString(), grdList.CurrentRow.Cells["ten_thuoc"].Value.ToString(), e.InitialValue, e.Value), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }
            }

        }

       

        void frm_qhe_doituong_thuoc_coban_Shown(object sender, EventArgs e)
        {
            if (AutoNew)
                cmdThemMoi_Click(cmdThemMoi, e);
        }

        void cboloaithuoc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && Utility.sDbnull(cboloaithuoc.SelectedValue, "-1") != "-1")
                {
                    cboloaithuoc.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                
            }
        }

       


        void grdQhe_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (grdQhe.CurrentColumn != null) grdQhe.CurrentColumn.InputMask = "";
            }
            catch
            {
            }
        }

        void grdQhe_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (grdQhe.CurrentColumn != null) e.Column.InputMask = "{0:#,#.##}";
            }
            catch
            {
            }
        }

        void cmdCauhinhgia_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._QheGiaThuocProperties);
            _Properties.ShowDialog();
        }

        void cmdCauhinh_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._ThuocProperties);
            _Properties.ShowDialog();
        }

        

        void cmdXoathuoc_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", true);
                if (!Utility.isValidGrid(grdList)) return;
                int idthuoc = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.IdThuoc].Value, -1);
                string tenthuoc = Utility.sDbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.TenThuoc].Value, "Không xác định");
                //Kiểm tra dữ liệu
                SqlQuery _sql = new Select().From(TThuockho.Schema)
                    .Where(TThuockho.Columns.IdThuoc).IsEqualTo(idthuoc);
                if (_sql.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Thuốc đã có trong kho nên không thể xóa danh mục", true);
                    return;
                }
                _sql = new Select().From(QheDoituongThuoc.Schema)
                    .Where(QheDoituongThuoc.Columns.IdThuoc).IsEqualTo(idthuoc);
                if (_sql.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Thuốc đã có quan hệ đối tượng nên bạn không thể xóa", true);
                    return;
                }
                _sql = new Select().From(KcbDonthuocChitiet.Schema)
                   .Where(KcbDonthuocChitiet.Columns.IdThuoc).IsEqualTo(idthuoc);
                if (_sql.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Thuốc đã được bác sĩ sử dụng kê đơn nên không thể xóa", true);
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa thuốc {0} ra khỏi hệ thống?", tenthuoc), "Xác nhận xóa thuốc", true))
                {
                    int v_intAffectedRecords = new Delete().From(DmucThuoc.Schema).Where(DmucThuoc.Columns.IdThuoc).IsEqualTo(idthuoc).Execute();
                    if (v_intAffectedRecords > 0)
                    {
                        m_blnCancel = false;
                        grdList.CurrentRow.Delete();
                        grdList.UpdateData();
                        grdList.Refetch();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommand_Quanhe();
                ModifyCommand();
            }
        }

        void mnuDelete_Click(object sender, EventArgs e)
        {
            cmdXoathuoc_Click(cmdXoathuoc, e);
        }

        void mnuUpdate_Click(object sender, EventArgs e)
        {
            cmdCapNhap_Click(cmdCapNhap, e);
        }

        void mnuHethieuluc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                int _drugId = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.IdThuoc].Value, -1);
                 int _Trangthai = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.TrangThai].Value, -1);
                 if (_Trangthai == 0) _Trangthai = 1;
                 else _Trangthai = 0;
                new Update(DmucThuoc.Schema).Set(DmucThuoc.Columns.TrangThai).EqualTo(_Trangthai)
                    .Where(DmucThuoc.Columns.IdThuoc).IsEqualTo(_drugId).Execute();
                grdList.CurrentRow.BeginEdit();
                grdList.CurrentRow.Cells[DmucThuoc.Columns.TrangThai].Value = _Trangthai;
                grdList.CurrentRow.EndEdit();
            }
            catch
            {
            } 
        }

       

        void _CheckedChanged(object sender, EventArgs e)
        {
            cboloaithuoc_SelectedIndexChanged(cboloaithuoc, e);
        }

        void cmdCapNhap_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            m_enAction = action.Update;
            ShowInsertNewDrug();
            ModifyCommand();
            ModifyCommand_Quanhe();
        }
        void cboloaithuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
               
               m_dataThuoc = SPs.DmucLaydanhsachThuocTheoquyennhanvien(globalVariables.gv_intIDNhanvien, Utility.Int32Dbnull(cboloaithuoc.SelectedValue, -1), Kieuthuoc_vt, 
                   (byte)(optTatca.Checked ? 2 : (optHieuluc.Checked ? 1 : 0)),
                   (byte)(optQhe_tatca.Checked ? 2 : (optCoQhe.Checked ? 1 : 0))).GetDataSet().Tables[0];
               Utility.SetDataSourceForDataGridEx(grdList, m_dataThuoc, true, true, "1=1", VDmucThuoc.Columns.SttHthiLoaithuoc +","+ DmucThuoc.Columns.TenThuoc);
               grdList.MoveFirst();
               grdList_SelectionChanged(grdList, e);
               if (!Utility.isValidGrid(grdList))
                   grdQhe.DataSource = null;
            }
            catch
            {
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private  DataTable m_dtObjectRelationService=new DataTable();
        private DataTable m_dtReportObjectType=new DataTable();
        private DataSet ds = new DataSet();
        bool m_blnLoaded = false;
        private DataTable m_dtObjectType=new DataTable();
        private void ModifyCommand_Quanhe()
        {
            cmdDetailDeleteAll.Enabled = grdQhe.RowCount > 0;
            cmdDelete.Enabled = grdQhe.RowCount > 0;
            cmdSaveObjectAll.Enabled = grdQhe.RowCount > 0;
          
        }
       

        private string _rowFilter = "1=1";
        private int id_thuoc = -1;
        private string rowFilters = "1=1";
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                if (!Utility.isValidGrid(grdList)) return;
                if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
                {
                    int _Trangthai = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.TrangThai].Value, -1);
                    if (_Trangthai == 0) mnuHethieuluc.Text="Kích hoạt sử dụng thuốc";
                    else mnuHethieuluc.Text = "Khóa thuốc";

                    id_loaithuoc = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.IdLoaithuoc].Value,
                                                           -1);
                    id_thuoc = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.IdThuoc].Value,-1);
                    _rowFilter = DmucThuoc.Columns.IdThuoc + "=" + id_thuoc;
                    m_dtQheDoituongThuoc = new Select().From(VQheDoituongThuoc.Schema)
                        .Where(DmucThuoc.Columns.IdThuoc).IsEqualTo(id_thuoc).OrderAsc(VQheDoituongThuoc.Columns.SttHthi).ExecuteDataSet().Tables[0];
                    if (!m_dtQheDoituongThuoc.Columns.Contains("IsNew")) m_dtQheDoituongThuoc.Columns.Add("IsNew", typeof(int));
                    if (!m_dtQheDoituongThuoc.Columns.Contains("CHON")) m_dtQheDoituongThuoc.Columns.Add("CHON", typeof(int));
                    //if (cboKhoaTH.Visible &&  cboKhoaTH.SelectedValue.ToString() != "-1")
                    //    rowFilters = QheDoituongThuoc.Columns.MaKhoaThuchien + " ='" + Utility.sDbnull(cboKhoaTH.SelectedValue, "") + "' OR " + QheDoituongThuoc.Columns.MaKhoaThuchien + " is null OR " + QheDoituongThuoc.Columns.MaKhoaThuchien + "='' OR " + QheDoituongThuoc.Columns.MaKhoaThuchien + "='ALL'";
                    //else
                    //    rowFilters = QheDoituongThuoc.Columns.MaKhoaThuchien + "='ALL'";
                    Utility.SetDataSourceForDataGridEx(grdQhe, m_dtQheDoituongThuoc, true, true, rowFilters, "");
                }
                ModifyCommand();
                ModifyCommand_Quanhe();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện xóa nhiều bản ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDetailDeleteAll_Click(object sender, EventArgs e)
        {
            Janus.Windows.GridEX.GridEXRow[] ArrCheckList = grdQhe.GetCheckedRows();
            if (ArrCheckList.Length <= 0)
            {

                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa", "Thông báo");
                grdQhe.Focus();
                return;
            }
            string strLength = string.Format("Bạn có muốn xoá {0} bản ghi không", ArrCheckList.Length);
            if (Utility.AcceptQuestion(strLength, "Thông báo", true))
            {
                foreach (GridEXRow drv in ArrCheckList)
                {
                    new Delete().From(QheDoituongThuoc.Schema)
                        .Where(QheDoituongThuoc.Columns.IdQuanhe)
                        .IsEqualTo(Utility.Int32Dbnull(drv.Cells[QheDoituongThuoc.Columns.IdQuanhe].Value, -1))
                        .Execute();
                    drv.Delete();
                    grdQhe.UpdateData();
                    grdQhe.Refresh();
                }
                m_dtQheDoituongThuoc.AcceptChanges();
                ModifyCommand_Quanhe();
            }
            ModifyCommand();
        }
        /// <summary>
        /// xóa thông tin của bản ghi hiện thời
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (grdQhe.CurrentRow != null)
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xoá thông tin bản ghi đang chọn không", "Thông báo", true))
                    {
                        new Delete().From(QheDoituongThuoc.Schema)
                            .Where(QheDoituongThuoc.Columns.IdQuanhe).IsEqualTo(
                                grdQhe.CurrentRow.Cells[QheDoituongThuoc.Columns.IdQuanhe].Value).
                            Execute();
                        grdQhe.CurrentRow.Delete();
                        grdQhe.UpdateData();
                        grdQhe.Refresh();
                        m_dtQheDoituongThuoc.AcceptChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList) ) return;
                frm_ChonDoituongKCB frm = new frm_ChonDoituongKCB();
                string KTH = Utility.sDbnull(cboKhoaTH.SelectedValue, "-1") == "-1" ? "ALL" : Utility.sDbnull(cboKhoaTH.SelectedValue, "-1");
                frm._enObjectType = enObjectType.Thuoc;
                frm.m_dtObjectDataSource = m_dtQheDoituongThuoc;
                frm.Original_Price = Utility.DecimaltoDbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.GiaDv].Value, 0);
                frm.MaKhoaTHIEN = KTH;
                frm.DetailService = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.IdThuoc].Value, 0);
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                    cmdSaveObjectAll_Click(cmdSaveObjectAll, e);
                ModifyCommand_Quanhe();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
           
        private int ServiceObject_Type_ID = -1;
        private int ObjectType_ID = -1;
        private Query m_Query = QheDoituongThuoc.CreateQuery();
        /// <summary>
        /// HÀM THUWCJHIEENJ KHỞI TẠO CHI TIẾT ĐỐI TƯỢNG CHI TIÊT DỊCH VỤ
        /// </summary>
        /// <returns></returns>
        private QheDoituongThuoc CreateDmucDoituongkcbDetailService()
        {
            var objectTypeService = new QheDoituongThuoc();
           
            return objectTypeService;
        }
        private int id_loaithuoc = -1;
        private ActionResult actionResult = ActionResult.Error;
       
        private void ModifyCommand()
        {
            try
            {
                bool isvalidGrid = Utility.isValidGrid(grdList);
                cmdXoathuoc.Enabled =isvalidGrid && grdList.RowCount > 0;
                cmdCapNhap.Enabled = isvalidGrid && grdList.RowCount > 0;

                cmdIndanhsachthuoc.Enabled = grdList.RowCount > 0;
                cmdInGiathuoc.Enabled = grdList.RowCount > 0;
                //cmdExportExcel.Enabled = grdList.RowCount > 0;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        
        /// <summary>
        /// HAM THUC HIEN DICH VU, QUAN HE DOI TUONG -DICH VU
        /// </summary>
        private int v_ObjectType_id_loaithuoc = -1;
        private int v_ObjectType_ID = -1;
        private int v_id_thuoc = -1;
        private DataTable dt_KhoaThucHien;
        void InitData()
        {
            try
            {
                DataTable m_dtLoaithuoc = new DataTable();
                if (Kieuthuoc_vt == "ALL")
                    m_dtLoaithuoc = new Select().From(DmucLoaithuoc.Schema)
                    .ExecuteDataSet().Tables[0];
                else
                    m_dtLoaithuoc = new Select().From(DmucLoaithuoc.Schema)
                   .Where(DmucLoaithuoc.Columns.KieuThuocvattu).IsEqualTo(Kieuthuoc_vt)
                   .ExecuteDataSet().Tables[0];
                DataTable m_dtLoaithuoc_new = m_dtLoaithuoc.Clone();
                if (globalVariables.gv_dtQuyenNhanvien_Dmuc.Select(QheNhanvienDanhmuc.Columns.Loai + "= 1").Length <= 0)
                    m_dtLoaithuoc_new = m_dtLoaithuoc.Copy();
                else
                {
                    foreach (DataRow dr in m_dtLoaithuoc.Rows)
                    {
                        if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucLoaithuoc.Columns.IdLoaithuoc]), "1"))
                        {
                            m_dtLoaithuoc_new.ImportRow(dr);
                        }
                    }
                }
                DataBinding.BindDataCombobox(cboloaithuoc, m_dtLoaithuoc_new, DmucLoaithuoc.Columns.IdLoaithuoc, DmucLoaithuoc.Columns.TenLoaithuoc, "Chọn", false);
                dt_KhoaThucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox_Basic(cboKhoaTH, dt_KhoaThucHien, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
                Laydanhmucthuoc();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void Laydanhmucthuoc()
        {
            if(Kieuthuoc_vt=="ALL")
                m_dataThuoc = SPs.DmucLaydanhsachThuocTheoquyennhanvien(globalVariables.gv_intIDNhanvien, Utility.Int32Dbnull(cboloaithuoc.SelectedValue, -1), Kieuthuoc_vt,
                       (byte)(optTatca.Checked ? 2 : (optHieuluc.Checked ? 1 : 0)),
                       (byte)(optQhe_tatca.Checked ? 2 : (optCoQhe.Checked ? 1 : 0))).GetDataSet().Tables[0];
                
            //    new Select().From(VDmucThuoc.Schema)
            //     .Where(VDmucThuoc.Columns.IdLoaithuoc).IsEqualTo(Utility.Int32Dbnull(cboloaithuoc.SelectedValue, -1))
            //     .OrderAsc(VDmucThuoc.Columns.SttHthiLoaithuoc, VDmucThuoc.Columns.TenThuoc)
            //     .ExecuteDataSet().Tables[0];
            //else
            //     m_dataThuoc = new Select().From(VDmucThuoc.Schema)
            //     .Where(VDmucThuoc.Columns.IdLoaithuoc).IsEqualTo(Utility.Int32Dbnull(cboloaithuoc.SelectedValue, -1))
            //     .And(VDmucThuoc.Columns.KieuThuocvattu).IsEqualTo(Kieuthuoc_vt)
            //     .OrderAsc(VDmucThuoc.Columns.SttHthiLoaithuoc, VDmucThuoc.Columns.TenThuoc)
            //     .ExecuteDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dataThuoc, true, true, "1=1", VDmucThuoc.Columns.SttHthiLoaithuoc + "," + DmucThuoc.Columns.TenThuoc);
            grdList.MoveFirst();
        }
        bool THUOC_GIATHEO_KHOAKCB = false;
        private void frm_qhe_doituong_thuoc_coban_Load(object sender, EventArgs e)
        {
            THUOC_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_GIATHEO_KHOAKCB", "0", true) == "1";
            cboKhoaTH.Enabled = THUOC_GIATHEO_KHOAKCB;
            if (!THUOC_GIATHEO_KHOAKCB) cboKhoaTH.Text = "Tất cả";
            InitData();
            ModifyCommand_Quanhe();
            ModifyCommand();
            m_blnLoaded = true;
            cboloaithuoc_SelectedIndexChanged(cboloaithuoc, e);
            if (grdList.GetDataRows().Length > 0) grdList.MoveFirst();
          
        }
        private DataTable m_dtServiceList=new DataTable();
        private DataTable m_dtServiceTypeList = new DataTable();
       
 

        private void cmdSearchOnGrid_Click(object sender, EventArgs e)
        {
            grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

       
        private void SaveAll()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                decimal GiaDV = LayGiaDV();
                int idThuoc = -1;
                decimal GiaPhuThu = 0;
                decimal GiaBHYT = LayGiaBHYT();
                string KTH = "ALL";
               
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQhe.GetRows())
                {
                    idThuoc = Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdThuoc].Value, -1);
                     KTH = Utility.sDbnull(cboKhoaTH.SelectedValue, "-1") == "-1" ? "ALL" : Utility.sDbnull(cboKhoaTH.SelectedValue, "-1");
                    SqlQuery q =
                        new Select().From(QheDoituongThuoc.Schema).Where(QheDoituongThuoc.Columns.IdThuoc).
                            IsEqualTo(Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdThuoc].Value, -1)).And(
                                QheDoituongThuoc.Columns.MaDoituongKcb).IsEqualTo(Utility.sDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.MaDoituongKcb].Value, "-1"));
                                //.And(QheDoituongThuoc.Columns.MaKhoaThuchien).IsEqualTo(KTH);
                                //.Or(QheDoituongThuoc.Columns.MaDoituongKcb).IsEqualTo("BHYT");
                    GiaPhuThu = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.PhuthuTraituyen].Value, 0);
                    Decimal don_gia = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0);
                    int v_IdLoaidoituongKcb = Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdLoaidoituongKcb].Value, 0);

                    //Nếu có lưu đối tượng BHYT và tồn tại giá DV thì tự động tính phụ thu trái tuyến cho đối tượng BHYT đó
                    if (gridExRow.Cells[QheDoituongThuoc.Columns.IdLoaidoituongKcb].Value.ToString() == "0" && GiaDV > 0)
                    {
                        GiaBHYT = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0);
                        if (PropertyLib._QheGiaThuocProperties.TudongDieuChinhGiaPTTT)
                            GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                    }
                    //Nếu đối tượng BHYT có tồn tại thì update lại thông tin trong đó có giá phụ thu trái tuyến
                    if (q.GetRecordCount() > 0)
                    {

                        new Update(QheDoituongThuoc.Schema)
                            .Set(QheDoituongThuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(QheDoituongThuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(QheDoituongThuoc.Columns.IdLoaithuoc).EqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdLoaithuoc].Value, -1))
                            .Set(QheDoituongThuoc.Columns.DonGia).EqualTo(
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0))
                            .Set(QheDoituongThuoc.Columns.PhuthuDungtuyen).EqualTo(
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.PhuthuDungtuyen].Value, 0))
                            .Set(QheDoituongThuoc.Columns.PhuthuTraituyen).EqualTo(GiaPhuThu)
                             .Set(QheDoituongThuoc.Columns.MaKhoaThuchien).EqualTo(KTH)
                            .Where(QheDoituongThuoc.Columns.IdQuanhe).IsEqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdQuanhe].Value, "-1"))
                                .Execute();
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa Đơn giá thuốc (quan hệ) ID thuốc={0}, tên thuốc={1}, Giá mới={2} thành công ", grdList.CurrentRow.Cells["id_thuoc"].Value.ToString(), grdList.CurrentRow.Cells["ten_thuoc"].Value.ToString(), don_gia), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                    }
                    else
                    {
                        DmucDoituongkcbCollection objectTypeCollection =
                            new DmucDoituongkcbController().FetchByQuery(
                                DmucDoituongkcb.CreateQuery().AddWhere(DmucDoituongkcb.Columns.MaDoituongKcb,
                                                                   Comparison.Equals,
                                                                   Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value, "-1")));

                        foreach (DmucDoituongkcb lObjectType in objectTypeCollection)
                        {
                            QheDoituongThuoc _newItems = new QheDoituongThuoc();
                            _newItems.IdDoituongKcb = lObjectType.IdDoituongKcb;
                            _newItems.IdLoaithuoc = Utility.Int16Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdLoaithuoc].Value, -1);
                            _newItems.IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdThuoc].Value, -1);
                            _newItems.TyleGiamgia = 0;
                            _newItems.KieuGiamgia = "%";
                            _newItems.DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0);
                            _newItems.PhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.PhuthuDungtuyen].Value, 0);
                            _newItems.PhuthuTraituyen = GiaPhuThu;
                            _newItems.IdLoaidoituongKcb = Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdLoaidoituongKcb].Value, -1);
                            _newItems.MaDoituongKcb = lObjectType.MaDoituongKcb;

                            _newItems.NguoiTao = globalVariables.UserName;
                            _newItems.NgayTao = globalVariables.SysDate;
                            _newItems.MaKhoaThuchien = KTH;
                            _newItems.IsNew = true;
                            _newItems.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm Đơn giá thuốc (quan hệ) ID thuốc={0}, tên thuốc={1},Mã đối tượng KCB={2}, Đơn giá={3} thành công ", grdList.CurrentRow.Cells["id_thuoc"].Value.ToString(), grdList.CurrentRow.Cells["ten_thuoc"].Value.ToString(), _newItems.MaDoituongKcb, _newItems.DonGia), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                            gridExRow.BeginEdit();
                            gridExRow.Cells[QheDoituongThuoc.Columns.IdQuanhe].Value = _newItems.IdQuanhe;
                            gridExRow.EndEdit();
                        }
                    }
                    gridExRow.BeginEdit();
                    gridExRow.Cells[QheDoituongThuoc.Columns.PhuthuTraituyen].Value = GiaPhuThu;
                    gridExRow.EndEdit();
                    grdQhe.UpdateData();
                    ////Nếu có chỉnh giá dịch vụ-->Tự động chỉnh giá danh mục thuốc-->Bỏ 20240513
                    //if (PropertyLib._QheGiaThuocProperties.TudongDieuChinhGiaDichVu)
                    //{
                    //    SqlQuery sqlQuery = new Select().From(DmucDoituongkcb.Schema)
                    //        .Where(DmucDoituongkcb.Columns.IdLoaidoituongKcb).IsEqualTo(1)
                    //        .And(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(Utility.sDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.MaDoituongKcb].Value, "-1"));
                    //    DmucDoituongkcb objectType = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                    //    if (objectType != null)
                    //    {
                    //        new Update(DmucThuoc.Schema)
                    //            .Set(DmucThuoc.Columns.DonGia)
                    //            .EqualTo(Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0))
                    //            .Where(DmucThuoc.Columns.IdThuoc)
                    //            .IsEqualTo(Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdThuoc].Value, -1)).Execute();
                    //    }
                    //}
                }
                ////-->Bỏ 20240513
                //new Update(DmucThuoc.Schema).Set(DmucThuoc.Columns.DonGia).EqualTo(GiaDV)
                //   .Set(DmucThuoc.Columns.GiaBhyt).EqualTo(GiaBHYT)
                //   .Where(DmucThuoc.Columns.IdThuoc).IsEqualTo(Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.IdThuoc].Value, -1))
                //   .Execute();
                ////Cập nhật giá BHYT cho các khoa khác-->Bỏ 20240513
                //if (PropertyLib._QheGiaThuocProperties.TudongDieuChinhGiaBHYT)
                //{
                //    if (GiaBHYT >= 0)
                //    {
                //        QheDoituongThuocCollection lstItems =
                //            new Select().From(QheDoituongThuoc.Schema).
                //            Where(QheDoituongThuoc.Columns.IdThuoc).
                //                  IsEqualTo(idThuoc)
                //                  .And(QheDoituongThuoc.MaKhoaThuchienColumn).IsNotEqualTo(KTH).ExecuteAsCollection<QheDoituongThuocCollection>();
                //        foreach (QheDoituongThuoc item in lstItems)
                //        {
                //            int v_IdLoaidoituongKcb = item.IdLoaidoituongKcb;
                //            if (v_IdLoaidoituongKcb == 1)
                //                GiaDV = item.DonGia;
                //        }
                //        GiaPhuThu = 0;
                //        foreach (QheDoituongThuoc item in lstItems)
                //        {
                //            int v_IdLoaidoituongKcb = item.IdLoaidoituongKcb;
                //            if (v_IdLoaidoituongKcb.ToString() == "0" && GiaDV > 0)//Nếu là đối tượng BHYT
                //            {
                //                GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                //                Update _update = new Update(QheDoituongThuoc.Schema).Set(QheDoituongThuoc.DonGiaColumn).EqualTo(GiaBHYT);
                //                if (PropertyLib._QheGiaThuocProperties.TudongDieuChinhGiaPTTT)
                //                    _update.Set(QheDoituongThuoc.PhuthuTraituyenColumn).EqualTo(GiaPhuThu);
                //                _update.Where(QheDoituongThuoc.IdLoaidoituongKcbColumn).IsEqualTo(0).And(QheDoituongThuoc.IdThuocColumn).IsEqualTo(idThuoc)
                //                .And(QheDoituongThuoc.MaKhoaThuchienColumn).IsNotEqualTo(KTH)
                //                .Execute();
                //            }
                //        }
                //    }
                //}
                Utility.SetMsg(lblMsg, "Bạn thực hiện cập nhập giá thành công",false);
            }
            catch (Exception exception)
            {
                Utility.SetMsg(lblMsg, "Lỗi trong quá trình cập nhập thông tin", false);
            }
           
        }
       
        private decimal GetLastPrice(decimal Price,int v_DiscountType,decimal v_DiscountRate)
        {
            decimal v_LastPrice = 0;
            if(v_DiscountType==1)
            {
                v_LastPrice = Price - v_DiscountRate;
            }
            if(v_DiscountType==0)
            {
                v_LastPrice = Price*(100 - v_DiscountRate)/100;
            }
            return v_LastPrice;
        }

       

        private void frm_qhe_doituong_thuoc_coban_KeyDown(object sender, KeyEventArgs e)
        {

            EventArgs evt = new EventArgs();
            if (e.KeyCode == Keys.F5) cboloaithuoc_SelectedIndexChanged(cboloaithuoc, evt);
            if (e.KeyCode == Keys.Escape) cmdClose_Click(cmdClose, e);
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoi_Click(cmdThemMoi, e);
            if (e.KeyCode == Keys.E && e.Control) cmdCapNhap_Click(cmdCapNhap, evt);
            if (e.KeyCode == Keys.S && e.Control) cmdSaveObjectAll_Click(cmdSaveObjectAll, evt);
            if (e.KeyCode == Keys.A && e.Control) cmdAdd_Click(cmdAdd, evt);
            if (e.KeyCode == Keys.Delete && e.Control) cmdDetailDeleteAll_Click(cmdDetailDeleteAll, evt);
            if (e.KeyCode == Keys.Delete) cmdDelete_Click(cmdDelete, evt);
            if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.G && globalVariables.isSuperAdmin) TaogiathuocQhe();
        }

        private void grdList_ApplyingFilter(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModifyCommand_Quanhe();
            ModifyCommand();
        }

       
        private void txtDetailLastPrice_LostFocus(object  sender,EventArgs e)
        {
           
        }
     

       
        private void cmdExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog1.FileName = "danhmucthuocvtt.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                {
                    gridEXExporter.GridEX = grdList;
                    gridEXExporter.Export(s);

                }
                Utility.ShowMsg("Xuất XML thành công !");
            }

            //frm_IE_Excel _IE_Excel = new frm_IE_Excel();
            //_IE_Excel.ShowDialog();
            //if (!_IE_Excel.m_blnCancel)
            //{
            //    cboloaithuoc_SelectedIndexChanged(cboloaithuoc, e);
            //}

            //string sPath = "drug.xls";
            //FileStream fs = new FileStream(sPath, FileMode.Create);
            //gridEXExporter.Export(fs);
        }
        private int v_ObjectType_Id = -1;
      
       
        private QheDoituongThuoc CreateObjectTypeService(GridEXRow gridExRow)
        {
            QheDoituongThuoc objectTypeService=new QheDoituongThuoc();
            objectTypeService.DonGia = Utility.DecimaltoDbnull(
                gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0);
            objectTypeService.PhuthuDungtuyen = Utility.DecimaltoDbnull(
                gridExRow.Cells[QheDoituongThuoc.Columns.PhuthuDungtuyen].Value, 0);
            objectTypeService.IdThuoc = Utility.Int16Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdThuoc].Value);
            objectTypeService.IdDoituongKcb =
                Utility.Int16Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdDoituongKcb].Value, -1);
            objectTypeService.TyleGiamgia =
              Utility.Int16Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.TyleGiamgia].Value, -1);
            objectTypeService.KieuGiamgia =
             Utility.sDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.KieuGiamgia].Value);
            return objectTypeService;

        }

        private decimal GetLastPrice(GridEXRow gridExRow)
        {
            if (gridExRow.Cells[QheDoituongThuoc.Columns.KieuGiamgia].Value.ToString() == "0")
            {
                return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value,0) *
                       (100 - Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.TyleGiamgia].Value))/100;

            }
            else
            {
                return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0) -Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.TyleGiamgia].Value,0);
            }
        }

        private void cmDeteleServiceDetail_Click_1(object sender, EventArgs e)
        {

        }

        
       

        private void txtDetailLastPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
           Utility.OnlyDigit(e);
        }

        

        private void cmdSaveObjectAll_Click(object sender, EventArgs e)
        {
            SaveAll();
           
        }
        decimal LayGiaDV()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQhe.GetRows())
            {
                if (gridExRow.Cells[ DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value.ToString() == "1")
                    return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0);
            }
            return -1;
        }
        decimal LayGiaBHYT()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQhe.GetRows())
            {
                if (gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value.ToString() == "0")
                    return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0);
            }
            return -1;
        }
       
        private void SaveQheDoituongDichvuCSL()
        {
            try
            {


                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetRows())
                {

                    SqlQuery q =
                    new Select().From(QheDoituongThuoc.Schema).Where(QheDoituongThuoc.Columns.IdThuoc).
                        IsEqualTo(Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdThuoc].Value, -1)).And(
                            QheDoituongThuoc.Columns.IdLoaidoituongKcb).IsEqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1));
                    if (q.GetRecordCount() > 0)
                    {

                        new Update(QheDoituongThuoc.Schema)
                            .Set(QheDoituongThuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(QheDoituongThuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(QheDoituongThuoc.Columns.IdLoaithuoc).EqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdLoaithuoc].Value, -1))
                            .Set(QheDoituongThuoc.Columns.DonGia).EqualTo(
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0))
                            .Set(QheDoituongThuoc.Columns.PhuthuDungtuyen).EqualTo(
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.PhuthuDungtuyen].Value, 0))
                            .Set(QheDoituongThuoc.Columns.PhuthuTraituyen).EqualTo(
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.PhuthuTraituyen].Value, 0))
                            .Set(QheDoituongThuoc.Columns.MaKhoaThuchien).EqualTo(Utility.sDbnull(cboKhoaTH.SelectedValue, ""))
                            .Where(QheDoituongThuoc.Columns.IdThuoc).IsEqualTo(id_thuoc)
                            .And(QheDoituongThuoc.Columns.IdLoaidoituongKcb).IsEqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1)).Execute();

                    }
                    else
                    {

                        new QheDoituongThuocController().Insert(-1,
                                                                  Utility.Int16Dbnull(
                                                                      gridExRow.Cells[QheDoituongThuoc.Columns.IdLoaithuoc].Value, -1),
                                                                  Utility.Int32Dbnull(
                                                                      gridExRow.Cells[QheDoituongThuoc.Columns.IdThuoc].Value, -1),
                                                                  0,"%",100,"",
                                                                  Utility.DecimaltoDbnull(
                                                                      gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0),
                                                                  Utility.DecimaltoDbnull(
                                                                      gridExRow.Cells[QheDoituongThuoc.Columns.PhuthuDungtuyen].Value, 0),
                                                                  Utility.Int32Dbnull(
                                                                      gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1),
                                                                  Utility.DecimaltoDbnull(
                                                                      gridExRow.Cells[QheDoituongThuoc.Columns.PhuthuTraituyen].Value, 0), "", globalVariables.UserName, globalVariables.SysDate, null, null, Utility.sDbnull(cboKhoaTH.SelectedValue, ""), Kieuthuoc_vt, Utility.DecimaltoDbnull(
                                                                      gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0));

                    }

                    SqlQuery sqlQuery = new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.Columns.IdLoaidoituongKcb).IsEqualTo(1);
                    DmucDoituongkcb objectType = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                    if (objectType != null && objectType.MaDoituongKcb == "DV")
                    {
                        new Update(DmucThuoc.Schema)
                            .Set(DmucThuoc.Columns.DonGia)
                            .EqualTo(Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongThuoc.Columns.DonGia].Value, 0))
                            .Where(DmucThuoc.Columns.IdThuoc)
                            .IsEqualTo(Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdThuoc].Value, -1)).Execute();

                    }
                }
                Utility.ShowMsg("Bạn thực hiện cập nhập giá thành công", "Thông báo");

            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin", "Thông báo lỗi", MessageBoxIcon.Error);
            }
        }
      
      

       

        private void chkExpand_CheckedChanged(object sender, EventArgs e)
        {
            grdList.GroupMode = chkExpand.Checked ? GroupMode.Default : GroupMode.Expanded;
            chkExpand.Text = chkExpand.Checked ? "Mở rộng" : "Thu lại";
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
        private DataTable m_dtGiathuoc=new DataTable();
        private void cmdInGiathuoc_Click(object sender, EventArgs e)
        {
            try
            {
                m_dtGiathuoc = SPs.ThuocLaydulieuQhedoituongThuocIngia(Kieuthuoc_vt, cboKhoaTH.SelectedValue.ToString()).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(m_dtGiathuoc, "thuoc_giathuoc_doituong");
                PrintReport( m_dtGiathuoc);
            }
            catch
            {
            }
        }

        void cmdIndanhsachthuoc_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDmucThuoc = SPs.ThuocLaydanhmucthuocIn(Kieuthuoc_vt, (Int16)cboloaithuoc.SelectedValue).GetDataSet().Tables[0];
                string tieude="", reportname = "";
               var crpt = Utility.GetReport("thuoc_danhmucthuoc",ref tieude,ref reportname);
               if (crpt == null) return;
               var objFromPre =
                   new frmPrintPreview(PropertyLib._ThuocProperties.TieudeInDanhmucThuoc, crpt, false, true);
               Utility.WaitNow(this);
               if (!PropertyLib._ThuocProperties.NhomthuocIndanhmuc)
                   dtDmucThuoc.DefaultView.Sort = "ten_thuoc asc";
               else
                   dtDmucThuoc.DefaultView.Sort = "stt_hthi_loaithuoc asc,ten_thuoc asc";
               crpt.SetDataSource(dtDmucThuoc.DefaultView);
               Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt,"Nhomthuoc", PropertyLib._ThuocProperties.NhomthuocIndanhmuc?1:0);
               Utility.SetParameterValue(crpt,"sTitleReport", PropertyLib._ThuocProperties.TieudeInDanhmucThuoc);
               objFromPre.crptViewer.ReportSource = crpt;
               objFromPre.ShowDialog();
               Utility.DefaultNow(this);
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện in báo cáo
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void PrintReport(DataTable dtData)
        {
             string tieude="", reportname = "";
             var crpt = Utility.GetReport("thuoc_giathuoc_doituong", ref tieude, ref reportname);
            if (crpt == null) return;
            var objFromPre = new frmPrintPreview(tieude, crpt, true, dtData.Rows.Count > 0);
            //var objFromPre =
            //    new frmPrintPreview(PropertyLib._ThuocProperties.TieudeBaocaoGiathuoc, crpt, false, true);
            Utility.WaitNow(this);
            if (!PropertyLib._ThuocProperties.NhomthuocIngia)
                dtData.DefaultView.Sort = "ten_thuoc asc";
            else
                dtData.DefaultView.Sort = "stt_hthi_loaithuoc asc,ten_thuoc asc";
            crpt.SetDataSource(dtData.DefaultView);
            Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
            Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
            Utility.SetParameterValue(crpt,"Nhomthuoc", PropertyLib._ThuocProperties.NhomthuocIngia ? 1 : 0);
            Utility.SetParameterValue(crpt,"sTitleReport", PropertyLib._ThuocProperties.TieudeBaocaoGiathuoc);
            Utility.SetParameterValue(crpt, "txtTrinhky",
                                      Utility.getTrinhky(objFromPre.mv_sReportFileName, globalVariables.SysDate));
            objFromPre.crptViewer.ReportSource = crpt;
            objFromPre.ShowDialog();
            Utility.DefaultNow(this);
        }
        void TaogiathuocQhe()
        {
            try
            {
                if (!Utility.AcceptQuestion("Bạn có chắc chắn tạo dữ liệu test giá thuốc quan hệ cho toàn bộ thuốc trong kho", "cảnh báo", true)) return;
                DmucThuocCollection lstThuoc = new DmucThuocController().FetchAll();
                DmucDoituongkcbCollection lstdoituong = new  DmucDoituongkcbController().FetchAll();
                Random rnd = new Random();
                foreach(DmucThuoc _thuoc in lstThuoc)
                {
                    foreach (DmucDoituongkcb _doituong in lstdoituong)
                    {
                        QheDoituongThuoc _newitem = new QheDoituongThuoc();
                        _newitem.IdDoituongKcb = _doituong.IdDoituongKcb;
                        _newitem.IdLoaithuoc = _thuoc.IdLoaithuoc;
                        _newitem.IdThuoc = _thuoc.IdThuoc;
                        _newitem.TyleGiamgia = 0;
                        _newitem.KieuGiamgia = "%";
                        _newitem.DonGia = rnd.Next(1000, 5000);
                        _newitem.PhuthuDungtuyen = 0;
                        _newitem.IdLoaidoituongKcb = _doituong.IdLoaidoituongKcb;
                        _newitem.PhuthuTraituyen = 0;
                        _newitem.MaDoituongKcb = _doituong.MaDoituongKcb;
                        _newitem.MaKhoaThuchien = "ALL";
                        _newitem.NguoiTao = globalVariables.UserName;
                        _newitem.NgayTao = globalVariables.SysDate;
                        _newitem.NguoiSua = globalVariables.UserName;
                        _newitem.NgaySua = globalVariables.SysDate;
                        _newitem.IsNew = true;
                        _newitem.Save();
                    }

                }
            }
            catch
            {
            }
        }
        private DataTable GetDataCheck()
        {
            DataTable dataTable = m_dataThuoc.Copy();
            foreach(Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetCheckedRows())
            {
                DataRow[] arrDr =
                    dataTable.Select(DmucThuoc.Columns.IdThuoc+ "=" +
                                                 Utility.Int32Dbnull(gridExRow.Cells[QheDoituongThuoc.Columns.IdThuoc].Value, -1));
                if(arrDr.GetLength(0)<=0)
                {
                  arrDr[0].Delete();
                }
            }
           
            dataTable.AcceptChanges();
            return dataTable;
        }

        public void cmdThemMoi_Click(object sender, EventArgs e)
        {

            frm_themmoi_thuoc frmNewDrug = new frm_themmoi_thuoc("DRUGONLY");
            frmNewDrug.m_dtDrugDataSource = m_dataThuoc;
            frmNewDrug.m_enAction = action.Insert;
            frmNewDrug.m_blnCallFromMenu = false;
            frmNewDrug.grdList = grdList;
            frmNewDrug.CallFrom = CallAction.FromMenu;
            frmNewDrug.objThuoc = GetObjectForUpdateOrDelete();
            if (frmNewDrug.objThuoc == null && frmNewDrug.m_enAction == action.Update) return;
            frmNewDrug.ShowDialog();
            m_blnCancel = frmNewDrug.m_blnCancel;
            ModifyCommand();
            ModifyCommand_Quanhe();
        }
        private DmucThuoc GetObjectForUpdateOrDelete()
        {
            DmucThuoc objDrug = new DmucThuoc();
            try
            {
               int id_thuoc= Utility.Int32Dbnull(grdList.GetValue(DmucThuoc.Columns.IdThuoc), -1);
                if (!Utility.isValidGrid(grdList)) return null;
                else
                    objDrug = DmucThuoc.FetchByID(id_thuoc);
            }
            catch
            {
                return null;
            }
            return objDrug;
        }
        
        private void ShowInsertNewDrug()
        {
            m_enAction = action.Update;
            var frmNewDrug = new frm_themmoi_thuoc("DRUGONLY");
            frmNewDrug.txtID.Text = Utility.sDbnull(grdList.GetValue(DmucThuoc.Columns.IdThuoc));
            frmNewDrug.m_dtDrugDataSource = m_dataThuoc;
            frmNewDrug.m_enAction = action.Update;
            frmNewDrug.m_blnCallFromMenu = false;
            frmNewDrug.grdList = grdList;
            frmNewDrug.CallFrom = CallAction.FromMenu;
            frmNewDrug.objThuoc = GetObjectForUpdateOrDelete();
            if (frmNewDrug.objThuoc == null && m_enAction == action.Update) return;
            frmNewDrug.ShowDialog();
            m_blnCancel = frmNewDrug.m_blnCancel;
        }
        private void cboKhoaTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            grdList_SelectionChanged(grdList, e);

        }

        private void cmdSaveObjectAll_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdExportExcel_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuTaogiaquanhe_Click(object sender, EventArgs e)
        {

            try
            {
                Utility.Log(this.Name, globalVariables.UserName, "Tạo giá quan hệ đối tượng Thuốc/VTTH dựa theo đối tượng tham chiếu", newaction.Reset, this.GetType().Assembly.ManifestModule.Name);
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                Utility.WaitNow(this);
                //if (grdList.GetCheckedRows().Length <= 0)
                //{
                //    Utility.ShowMsg("Bạn cần chọn ít nhất 1 thuốc/VTTH để thực hiện chức năng này");
                //    return;
                //}
                frm_ChonDoituongKCB_All frm = new frm_ChonDoituongKCB_All();
                frm._enObjectType = enObjectType.Thuoc;
                frm.m_dtQheDoituong_Thuoc = m_dtQheDoituongThuoc.Clone();
                frm.Original_Price =
                    Utility.DecimaltoDbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.DonGia].Value, 0);
                frm.MaKhoaTHIEN = Utility.sDbnull(cboKhoaTH.SelectedValue, "");
                frm.DetailService =
                    Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucThuoc.Columns.IdThuoc].Value, 0);
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                    AutoCreate(frm.m_dtQheDoituong_Thuoc, frm.id_doituongkcb_thamchieu);

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
        void AutoCreate(DataTable dtDoituongKcb, Int16 id_doituongKcb_thamchieu)
        {
            try
            {
                foreach (DataRow dr in dtDoituongKcb.Rows)
                {
                    Int16 id_loaidoituongkcb = Utility.Int16Dbnull(dr[QheDoituongDichvucl.Columns.IdLoaidoituongKcb], -1);
                    Int16 id_doituongkcb = Utility.Int16Dbnull(dr[QheDoituongDichvucl.Columns.IdDoituongKcb], -1);
                    SPs.QheThuocvtthDoituongkcbAutoCreate(id_loaidoituongkcb, id_doituongkcb, id_doituongKcb_thamchieu, globalVariables.UserName, DateTime.Now, Kieuthuoc_vt).Execute();
                }
                Utility.ShowMsg("Đã tạo dữ liệu thành công. Nhấn OK để kết thúc");
                grdList_SelectionChanged(grdList, new EventArgs());
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuCreateGiaDV_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.Log(this.Name, globalVariables.UserName, "Reset lại giá quan hệ đối tượng thuốc cho đối tượng DV", newaction.Reset, this.GetType().Assembly.ManifestModule.Name);
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                DmucDoituongkcb objDoituong = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo("DV").ExecuteSingle<DmucDoituongkcb>();
                if (objDoituong != null)
                {
                    SPs.QheThuocvtthDoituongkcbAutoCreateDv(objDoituong.IdLoaidoituongKcb, objDoituong.IdDoituongKcb, objDoituong.IdDoituongKcb, globalVariables.UserName, DateTime.Now, Kieuthuoc_vt).Execute();
                }
                Utility.ShowMsg("Đã tạo dữ liệu thành công. Nhấn OK để kết thúc");
                grdList_SelectionChanged(grdList,e);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        
    }
}
