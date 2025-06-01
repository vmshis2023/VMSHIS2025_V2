using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VMS.HIS.DAL;
using NLog;
using SubSonic;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using VNS.Libs;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UCs;
using VNS.Properties;
using System.Transactions;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_themthau : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;
        private readonly Logger log;
        public bool m_blnCancel = false;
        public action m_enAct = action.Insert;
        private DataTable dtChiTietThau = new DataTable();
        private DataTable dtDmThuoc = new DataTable();
        private string kieu_thuocvt = "TATCA";
        public TThau _objThau = null;
        #region form events
        public frm_themthau(string skieu_thuocvt)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            log = LogManager.GetCurrentClassLogger();
            dtpNgayThau.Value =dtpngaythau_den.Value= DateTime.Now;
            dtpNgayQDinh.Value =dtpngayqd_den.Value= DateTime.Now;
            kieu_thuocvt = skieu_thuocvt;
            InitEvents();
            
        }
        private int id_thuockho = -1;
        private bool _allowDrugChanged;
        private void txtdrug__OnGridSelectionChanged(string ID, int id_thuockho, string _name, string Dongia,
         string phuthu, int tutuc)
        {
           
        }
        private void txtdrug__OnChangedView(bool gridview)
        {
            //PropertyLib._AppProperties.GridView = gridview;
            //PropertyLib.SaveProperty(PropertyLib._AppProperties);
        }
        void _OnSaveAsV1(AutoCompleteTextbox_Danhmucchung obj)
        {
        }
        void InitEvents()
        {
            txtLoaiThau._OnSaveAsV1 += _OnSaveAsV1;
            txtLoaiThau._OnShowDataV1 += _OnShowData;
            txtGoiThau._OnSaveAsV1 += _OnSaveAsV1;
            txtGoiThau._OnShowDataV1 += _OnShowData;
            txtNhomThau._OnSaveAsV1 += _OnSaveAsV1;
            txtNhomThau._OnShowDataV1 += _OnShowData;
            txtNhacungcap._OnSaveAsV1 += _OnShowData;
            txtDrugName._OnEnterMe += txtDrugName__OnEnterMe;
            txtDrugName._OnGridSelectionChanged += txtdrug__OnGridSelectionChanged;
            txtDrugName._OnChangedView += txtdrug__OnChangedView;
            txtDrug_ID.TextChanged+=txtDrug_ID_TextChanged;
            grdChiTietThau.RowDoubleClick += grdChiTietThau_RowDoubleClick;
            txtSoLuong.TextChanged+=txtSoLuong_TextChanged;
            txtDonGia.TextChanged+=txtDonGia_TextChanged;
        }
        bool isUpdate = false;
        void grdChiTietThau_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdChiTietThau)) return;
               //Load dữ liệu lên để sửa
                if (Utility.ByteDbnull(grdChiTietThau.GetValue("trang_thai"))==1)
                {
                    Utility.ShowMsg("Chi tiết thầu đã được xác nhận nên bạn không thể chỉnh sửa");
                    return;
                }
                txtDrugName.SetId(grdChiTietThau.GetValue("id_thuoc"));
                txtNhomThau.SetCode(Utility.sDbnull(grdChiTietThau.GetValue("nhom_thau")));
                txtSoDK.Text = Utility.sDbnull(grdChiTietThau.GetValue("so_dangky"));
                txtTCCL.Text = Utility.sDbnull(grdChiTietThau.GetValue("tccl"));
                txtDangSoChe.Text = Utility.sDbnull(grdChiTietThau.GetValue("dang_soche"));
                txtSoLuong.Text = Utility.sDbnull(grdChiTietThau.GetValue("so_luong"));
                txtDonGia.Text = Utility.sDbnull(grdChiTietThau.GetValue("don_gia"));
                isUpdate = true;
            }
            catch (Exception ex)
            {
                
            }
           
        }
        public frm_themthau()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            log = LogManager.GetCurrentClassLogger();
            dtpNgayThau.Value = DateTime.Now;
            dtpNgayQDinh.Value = DateTime.Now;
            InitEvents();
        }

        void txtDrugName__OnEnterMe()
        {
            int _idthuoc = Utility.Int32Dbnull(txtDrugName.MyID, -1);
            txtDrug_ID.Text = _idthuoc.ToString();
        }
        DmucThuoc objThuoc = null;
        DmucChung objDVT = null;
        private void txtDrug_ID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Utility.Int32Dbnull(txtDrug_ID.Text, -1) > 0)
                {
                    objThuoc = DmucThuoc.FetchByID(Utility.Int32Dbnull(txtDrug_ID.Text));
                    if (objThuoc != null)
                    {
                        txtMathuoc.Text = objThuoc.MaThuoc;
                        //txtnhasx.Text = objThuoc.manh;
                        objDVT = THU_VIEN_CHUNG.LaydoituongDmucChung("DONVITINH", Utility.sDbnull(objThuoc.MaDonvitinh));
                        if (objDVT != null)
                        {
                            lblDVT.Text = objDVT.Ten;
                        }
                        txtDangSoChe.Text = objThuoc.DangBaoche;
                    }
                    else
                    {
                        txtMathuoc.Clear();
                        txtnhasx.Clear();
                    }

                }
                else
                {
                    txtMathuoc.Clear();
                    txtnhasx.Clear();
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void _OnSaveAs(AutoCompleteTextbox_Danhmucchung obj)
        {
            if (Utility.DoTrim(obj.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, obj.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }
        }
        private void _OnShowData(AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            } 
        }
        
        private void frm_themthau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            else if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            else if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
        }
        private void frm_themthau_Load(object sender, EventArgs e)
        {
            chkDieuTiet.Checked = pnlDieuTiet.Enabled = false;
           
            txtNhacungcap.Init();
            txtNoidieutiet.Init(globalVariables.gv_dtDmucBenhVien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });
            DataTable dtHinhthucthau = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("HINHTHUCTHAU").ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cbohinhthucthau, dtHinhthucthau, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "-----Chọn hình thức thầu-----",true);
          
            grpThongtinthau.Enabled = m_enAct != action.Add;
            AutocompleteBenhvien();
            ModifyCommands();
            LoadAuCompleteThuoc();
            txtLoaiThau.Init();
            txtLoaiThau.SetCode("-1");
            txtGoiThau.Init();
            txtGoiThau.SetCode("-1");
            txtNhomThau.Init();
            txtNhomThau.SetCode("-1");
            GetData();
            if (m_enAct == action.View)
            {
                cmdAddDetail.Enabled = cmdSave.Enabled = cmdClear.Enabled = false;
            }
        }
        private void AutocompleteBenhvien()
        {
            //DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
            DataTable m_dtBenhvien = globalVariables.gv_dtDmucBenhVien;
            try
            {
                if (m_dtBenhvien == null) return;
                if (!m_dtBenhvien.Columns.Contains("ShortCut"))
                    m_dtBenhvien.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                txtNoidieutiet.Init(m_dtBenhvien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void LoadAuCompleteThuoc()
        {
            txtDrugName.dtData = CommonLoadDuoc.LayThongTinThuoc(kieu_thuocvt);
            txtDrugName.ChangeDataSource();
        }
        #endregion

        private void ModifyCommands()
        {
            try
            {
                cmdSave.Enabled = grdChiTietThau.RowCount > 0 || _objThau != null;
                cmdIn.Enabled = grdChiTietThau.RowCount > 0;
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
                log.Trace(exception.Message);
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cmdInPhieuNhap_Click(object sender, EventArgs e)
        {
           
        }
        private void GetData()
        {
            if (m_enAct !=action.Insert)
            {
                if(_objThau==null) _objThau = TThau.FetchByID(Utility.Int32Dbnull(txtIDThau.Text));
                if (_objThau != null)
                {
                    txtIDThau.Text = Utility.sDbnull(_objThau.IdThau, "-1");
                    txtLoaiThau.SetCode(Utility.sDbnull(_objThau.LoaiThau));
                    txtGoiThau.SetCode(Utility.sDbnull(_objThau.GoiThau));
                    txtSoThau.Text = _objThau.SoThau;
                    dtpNgayThau.Value = Convert.ToDateTime(_objThau.NgaythauTu);
                    dtpngaythau_den.Value = Convert.ToDateTime(_objThau.NgaythauDen);
                    txtSoQDinh.Text = Utility.sDbnull(_objThau.SoQuyetdinh, "");
                    dtpNgayQDinh.Value = Convert.ToDateTime(_objThau.NgayqdTu);
                    dtpngayqd_den.Value = Convert.ToDateTime(_objThau.NgayqdDen);
                    txtThongTinKhac.Text = Utility.sDbnull(_objThau.GhiChu);
                    txtNhacungcap.SetCode(_objThau.MaNhacungcap);
                    chkDieuTiet.Checked = Utility.Byte2Bool(_objThau.LaApthau);
                    if (chkDieuTiet.Checked)
                    {
                        txtNoidieutiet.SetId(Utility.Int32Dbnull(_objThau.IdBenhvienDieutiet, -1));
                        txtSoHd_dieutiet.Text = Utility.sDbnull(_objThau.SoHdongDieutiet, string.Empty);
                        dtpNgayHD_DieuTiet.Value = Convert.ToDateTime(_objThau.NgayhdDieutiet);
                        dtpNgayKT_DieuTiet.Value = Convert.ToDateTime(_objThau.NgayktDieutiet);
                    }
                    cbohinhthucthau.SelectedValue = Utility.Int32Dbnull(_objThau.HthucThau, -1);
                    dtChiTietThau = SPs.ThuocThauLaythongtinchitiet(Utility.Int32Dbnull(txtIDThau.Text)).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grdChiTietThau, dtChiTietThau, false, true, "1=1", "");
                }
            }
            else if (m_enAct == action.Insert)
            {
                dtChiTietThau = SPs.ThuocThauLaythongtinchitiet(-100).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChiTietThau, dtChiTietThau, false, true, "", "");
            }
        }

        #region Save
        private void CreateThongTinThau()
        {
            if (m_enAct == action.Update || m_enAct == action.Add)
            {
                if (_objThau == null)
                    _objThau = TThau.FetchByID(Utility.Int32Dbnull(txtIDThau.Text));
                _objThau.MarkOld();
                _objThau.IsLoaded = true;
                _objThau.NguoiSua = globalVariables.UserName;
                _objThau.NgaySua = DateTime.Now;
            }
            else
            {
                _objThau = new TThau();
                _objThau.TrangThai = 0;
                _objThau.TthaiDieutiet = false;
                _objThau.TthaiXacnhan = 0;
                _objThau.NgayTao = DateTime.Now;
                _objThau.NguoiTao = globalVariables.UserName;
                _objThau.NguoiSua = null;
                _objThau.NgaySua = null;
                _objThau.IsNew = true;
            }
            _objThau.LoaiThau = Utility.sDbnull(txtLoaiThau.MyCode, string.Empty);
            _objThau.GoiThau = Utility.sDbnull(txtGoiThau.MyCode, string.Empty);
            _objThau.SoThau = Utility.sDbnull(txtSoThau.Text, string.Empty);
            _objThau.NgaythauTu = Convert.ToDateTime(dtpNgayThau.Value);
            _objThau.NgaythauDen = Convert.ToDateTime(dtpngaythau_den.Value);
            _objThau.SoQuyetdinh = Utility.sDbnull(txtSoQDinh.Text, string.Empty);
            _objThau.NgayqdTu = Convert.ToDateTime(dtpNgayQDinh.Value);
            _objThau.NgayqdDen = Convert.ToDateTime(dtpngayqd_den.Value);
            _objThau.MaNhacungcap = Utility.sDbnull(txtNhacungcap.MyCode, string.Empty);
            _objThau.GhiChu = Utility.sDbnull(txtThongTinKhac.Text, string.Empty);
            _objThau.KieuThuocvt = this.kieu_thuocvt;
            _objThau.LaApthau =Utility.Bool2byte( chkDieuTiet.Checked);
            if (chkDieuTiet.Checked)
            {
                _objThau.IdBenhvienDieutiet = Utility.Int32Dbnull(txtNoidieutiet.MyID, -1);
                _objThau.SoHdongDieutiet = Utility.sDbnull(txtSoHd_dieutiet.Text, string.Empty);
                _objThau.NgayhdDieutiet = Convert.ToDateTime(dtpNgayHD_DieuTiet.Value);
                _objThau.NgayktDieutiet = Convert.ToDateTime(dtpNgayKT_DieuTiet.Value);
            }
            _objThau.HthucThau = Utility.Int32Dbnull(cbohinhthucthau.SelectedValue, -1);
            _objThau.BophanDung = -1;

        }
        private TThauChitietCollection CreateChiTietThau()
        {
            TThauChitietCollection colChiTiet = new TThauChitietCollection();
            TThauChitiet _obj = null;
            foreach (GridEXRow gridExRow in grdChiTietThau.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    _obj = new TThauChitiet();
                    long id_thau_ct = Utility.Int64Dbnull(gridExRow.Cells["id_thau_ct"].Value,-1);
                    if (id_thau_ct<=0)
                    {
                        _obj.NgayTao = globalVariables.SysDate;
                        _obj.NguoiTao = globalVariables.UserName;
                        _obj.IdThau = -1;
                        _obj.IdThauCt = -1;
                        _obj.IsNew = true;
                    }
                    else
                    {
                        _obj.MarkOld();
                        _obj.IsNew = false;
                        _obj.NgaySua = globalVariables.SysDate;
                        _obj.NguoiSua = globalVariables.UserName;
                        _obj.IdThau = Utility.Int32Dbnull(gridExRow.Cells[TThauChitiet.Columns.IdThau].Value);
                        _obj.IdThauCt = Utility.Int32Dbnull(gridExRow.Cells[TThauChitiet.Columns.IdThauCt].Value);
                    }
                    _obj.IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[TThauChitiet.Columns.IdThuoc].Value);
                    _obj.MaDvt = Utility.sDbnull(gridExRow.Cells[TThauChitiet.Columns.MaDvt].Value);
                    _obj.DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[TThauChitiet.Columns.DonGia].Value);
                    _obj.SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells[TThauChitiet.Columns.SoLuong].Value, 0);
                    _obj.NhomTckt = Utility.sDbnull(gridExRow.Cells[TThauChitiet.Columns.NhomTckt].Value, string.Empty);
                    _obj.Tccl = Utility.sDbnull(gridExRow.Cells[TThauChitiet.Columns.Tccl].Value, string.Empty);
                    _obj.DangSoche = Utility.sDbnull(gridExRow.Cells[TThauChitiet.Columns.DangSoche].Value, string.Empty);
                    _obj.NguonGoc = Utility.sDbnull(gridExRow.Cells[TThauChitiet.Columns.NguonGoc].Value, string.Empty);
                    _obj.NhomThau = Utility.sDbnull(gridExRow.Cells[TThauChitiet.Columns.NhomThau].Value, string.Empty);
                    _obj.SoDangky = Utility.sDbnull(gridExRow.Cells[TThauChitiet.Columns.SoDangky].Value, string.Empty);
                    colChiTiet.Add(_obj);
                }
            }
            return colChiTiet;
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            PerformAction();
        }
        private void PerformAction()
        {
            try
            {
                cmdSave.Enabled = false;
                switch (m_enAct)
                {
                    case action.Insert:
                        ThemThau();
                        break;
                    case action.Update:
                        CapnhatThau();
                        break;
                    case action.Add:
                        BoSungChiTietThau();
                        break;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
                log.Trace(exception.Message);
            }
            finally
            {
                ModifyCommands();
                cmdSave.Enabled = true;
            }
        }
        private void ThemThau()
        {
            try
            {
                CreateThongTinThau();
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        TThauChitietCollection lstChitietThau = CreateChiTietThau();
                        _objThau.IsNew = true;
                        _objThau.Save();

                        new Delete().From(TThauChitiet.Schema).Where(TThauChitiet.Columns.IdThau).IsEqualTo(_objThau.IdThau).Execute();
                        foreach (TThauChitiet item in lstChitietThau)
                        {
                            item.IdThau = _objThau.IdThau;
                            item.IsNew = true;
                            item.Save();
                        }
                    }
                    scope.Complete();
                }
                //SPs.SpPostTTThau2SoQDinh(Utility.Int32Dbnull(objThau.IdThau, -1)).Execute();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới Thông tin thầu thành công: {0} thành công", _objThau.IdThau), _objThau.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới thầu thành công. Nhấn Ok để kết thúc");
                    cmdIn.Enabled = true;
                    if (_OnCreated != null) _OnCreated(_objThau.IdThau, action.Insert);
                    m_enAct = action.Update;
                    if (chkCloseAfterSave.Checked) this.Close();
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Thông tin thầu có ID: {0} thành công", _objThau.IdThau), _objThau.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null) _OnCreated(_objThau.IdThau, action.Update);
                    MessageBox.Show("Đã cập nhật thầu thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                    if (chkCloseAfterSave.Checked) this.Close();
                }
                txtIDThau.Text = Utility.sDbnull(_objThau.IdThau);
                m_blnCancel = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }

        }
        private void CapnhatThau()
        {
            try
            {
               CreateThongTinThau();
               TThauChitietCollection lstChitietThau = CreateChiTietThau();
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       _objThau.MarkOld();
                       _objThau.Save();
                       foreach (TThauChitiet item in lstChitietThau)
                       {
                           if (item.IdThauCt <= 0)
                           {
                               item.IdThau = _objThau.IdThau;
                               item.IsNew = true;
                               item.Save();
                           }
                           else//Update
                           {
                               item.MarkOld();
                               item.Save();
                           }
                       }
                   }
                   scope.Complete();
               }
                //SPs.SpPostTTThau2SoQDinh(Utility.Int32Dbnull(objThau.IdThau, -1)).Execute();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới Thông tin thầu thành công: {0} thành công", _objThau.IdThau), _objThau.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới thầu thành công. Nhấn Ok để kết thúc");
                    cmdIn.Enabled = true;
                    if (_OnCreated != null) _OnCreated(_objThau.IdThau, action.Insert);
                    m_enAct = action.Update;
                    if (chkCloseAfterSave.Checked) this.Close();
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Thông tin thầu có ID: {0} thành công", _objThau.IdThau), _objThau.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null)
                        if (lstChitietThau.Count <= 0)
                            _OnCreated(_objThau.IdThau, action.Delete);
                        else _OnCreated(_objThau.IdThau, action.Update);
                    if (chkCloseAfterSave.Checked) this.Close();
                    MessageBox.Show("Đã cập nhật thầu thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                m_blnCancel = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình sửa phiếu: " + ex.Message, "Thông báo lỗi", MessageBoxIcon.Error);
            }
        }
        private void BoSungChiTietThau()
        {
            try
            {
                CreateThongTinThau();
                TThauChitietCollection lstChitietThau = CreateChiTietThau();
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (_objThau != null)
                        {
                            foreach (TThauChitiet item in lstChitietThau)
                            {
                                if (Utility.Int32Dbnull(item.IdThauCt, -1) <= 0)
                                {
                                    item.IdThau = _objThau.IdThau;
                                    item.TrangThai = 1;//Đã xác nhận
                                    item.IsNew = true;
                                    item.Save();
                                    SPs.ThuocThauThemchitiet(Utility.Int32Dbnull(item.IdThau, -1), Utility.Int32Dbnull(item.IdThauCt, -1)).Execute();
                                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Bổ sung chi tiết thầu: ID thầu: {0}; Số QĐ: {1}; Số HĐ: {2}; ID chi tiết: {3}; ID thuốc: {4}; Số lượng: {5}", item.IdThau, _objThau.SoQuyetdinh, _objThau.SoThau, item.IdThauCt, item.IdThuoc, item.SoLuong), newaction.Update, "UI");
                                }
                            }
                        }
                    }
                    scope.Complete();
                }
                m_enAct = action.Add;

                dtChiTietThau = SPs.ThuocThauLaythongtinchitiet(Utility.Int32Dbnull(txtIDThau.Text)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChiTietThau, dtChiTietThau, false, true, "1=1", "");
                Utility.ShowMsg("Bạn bổ sung chi tiết thành công", "Thông báo");
                m_blnCancel = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình bổ sung chi tiết thầu: " + ex.Message, "Thông báo lỗi", MessageBoxIcon.Error);
            }
        }
        private bool isValidData()
        {
            TThau _oThau = new Select().From(TThau.Schema).Where(TThau.Columns.IdThau).IsEqualTo(Utility.Int32Dbnull(txtIDThau.Text)).ExecuteSingle<TThau>();
            if (Utility.Int32Dbnull(txtIDThau.Text) > 0 && _oThau == null)
            {
                Utility.ShowMsg("Không tồn tại thông tin thầu, hoặc thông tin thầu đã bị xóa. Đề nghị kiểm tra lại.", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            if (m_enAct != action.Add && _oThau != null && Utility.Int32Dbnull(_oThau.TrangThai, -1) > 0)
            {
                Utility.ShowMsg("Thông tin thầu đã được xác nhận không được phép chỉnh sửa", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            
            if (string.IsNullOrEmpty(txtLoaiThau.myCode))
            {
                Utility.ShowMsg("Bạn phải chọn loại thầu trong danh mục", "Thông báo", MessageBoxIcon.Warning);
                txtLoaiThau.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtGoiThau.myCode))
            {
                Utility.ShowMsg("Bạn phải chọn gói thầu trong danh mục", "Thông báo", MessageBoxIcon.Warning);
                txtGoiThau.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSoThau.Text))
            {
                Utility.ShowMsg("Chưa nhập Số thầu", "Thông báo", MessageBoxIcon.Warning);
                txtSoThau.Focus();
                return false;
            }
            if (txtNhacungcap.myCode=="-1")
            {
                Utility.ShowMsg("Chưa nhập Nhà thầu", "Thông báo", MessageBoxIcon.Warning);
                txtNhacungcap.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(dtpNgayQDinh.Text))
            {
                Utility.ShowMsg("Chưa nhập Ngày quyết định", "Thông báo", MessageBoxIcon.Warning);
                dtpNgayQDinh.Focus();
                return false;
            }
            else
            {
                try
                {
                    DateTime dt = DateTime.Parse(dtpNgayQDinh.Text);
                    DateTime systemDate = DateTime.Now;
                    if ((systemDate - dt).TotalDays < 0)
                    {
                        Utility.ShowMsg(string.Format("Ngày quyết định không được lớn hơn ngày hệ thống ({0}).", systemDate), "Thông báo", MessageBoxIcon.Warning);
                        dtpNgayQDinh.Focus();
                        return false;
                    }
                    if (dt.Year < 2010)
                    {
                        Utility.ShowMsg(string.Format("Năm quyết định từ phải >=2010."), "Thông báo", MessageBoxIcon.Warning);
                        dtpNgayQDinh.Focus();
                        return false;
                    }
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Ngày quyết định không hợp lệ", "Thông báo", MessageBoxIcon.Warning);
                    dtpNgayQDinh.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(dtpngayqd_den.Text))
            {
                Utility.ShowMsg("Chưa nhập ngày kết thúc quyết định", "Thông báo", MessageBoxIcon.Warning);
                dtpngayqd_den.Focus();
                return false;
            }
            else
            {
                try
                {
                    DateTime dt = DateTime.Parse(dtpngayqd_den.Text);
                    DateTime _dtNgayThau = DateTime.Parse(dtpNgayQDinh.Text);
                    if ((_dtNgayThau - dt).TotalDays > 0)
                    {
                        Utility.ShowMsg(string.Format("Ngày kết thúc quyết định không được nhỏ hơn ngày quyết định ({0}).", _dtNgayThau), "Thông báo", MessageBoxIcon.Warning);
                        dtpngayqd_den.Focus();
                        return false;
                    }
                    if (dt.Year < 2010)
                    {
                        Utility.ShowMsg(string.Format("Năm kết thúc quyết định phải >=2010."), "Thông báo", MessageBoxIcon.Warning);
                        dtpngayqd_den.Focus();
                        return false;
                    }
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Ngày kết thúc quyết định không hợp lệ", "Thông báo", MessageBoxIcon.Warning);
                    dtpngayqd_den.Focus();
                    return false;
                }
            }
            
            if (string.IsNullOrEmpty(dtpNgayThau.Text))
            {
                Utility.ShowMsg("Chưa nhập Ngày thầu", "Thông báo", MessageBoxIcon.Warning);
                dtpNgayThau.Focus();
                return false;
            }
            else
            {
                try
                {
                    DateTime dt = DateTime.Parse(dtpNgayThau.Text);
                    DateTime systemDate = DateTime.Now;
                    if ((systemDate - dt).TotalDays < 0)
                    {
                        Utility.ShowMsg(string.Format("Ngày thầu không được lớn hơn ngày hệ thống ({0}).", systemDate), "Thông báo", MessageBoxIcon.Warning);
                        dtpNgayThau.Focus();
                        return false;
                    }
                    if (dt.Year < 2010)
                    {
                        Utility.ShowMsg(string.Format("Năm thầu từ phải >=2010."), "Thông báo", MessageBoxIcon.Warning);
                        dtpNgayThau.Focus();
                        return false;
                    }
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Ngày thầu không hợp lệ", "Thông báo", MessageBoxIcon.Warning);
                    dtpNgayThau.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(dtpngaythau_den.Text))
            {
                Utility.ShowMsg("Chưa nhập ngày kết thúc thầu", "Thông báo", MessageBoxIcon.Warning);
                dtpngaythau_den.Focus();
                return false;
            }
            else
            {
                try
                {
                    DateTime dt = DateTime.Parse(dtpngaythau_den.Text);
                    DateTime _dtNgayThau = DateTime.Parse(dtpNgayThau.Text);
                    if ((_dtNgayThau - dt).TotalDays > 0)
                    {
                        Utility.ShowMsg(string.Format("Ngày kết thúc thầu không được nhỏ hơn ngày thầu ({0}).", _dtNgayThau), "Thông báo", MessageBoxIcon.Warning);
                        dtpngaythau_den.Focus();
                        return false;
                    }
                    if (dt.Year < 2010)
                    {
                        Utility.ShowMsg(string.Format("Năm thầu đến phải >=2010."), "Thông báo", MessageBoxIcon.Warning);
                        dtpngaythau_den.Focus();
                        return false;
                    }
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Ngày kết thúc thầu không hợp lệ", "Thông báo", MessageBoxIcon.Warning);
                    dtpngaythau_den.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtSoQDinh.Text))
            {
                Utility.ShowMsg("Chưa nhập Số quyết định", "Thông báo", MessageBoxIcon.Warning);
                txtSoQDinh.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(txtSoQDinh.Text) && Utility.Int32Dbnull(txtIDThau.Text, -1) > 0)
            {
                TThauCollection _obj = new Select().From(TThau.Schema)
                                                            .Where(TThau.Columns.SoQuyetdinh).IsEqualTo(Utility.sDbnull(txtSoQDinh.Text, string.Empty))
                                                            .And(TThau.Columns.MaNhacungcap).IsEqualTo(Utility.Int16Dbnull(txtNhacungcap.myCode, -1))
                                                            .And(TThau.Columns.GoiThau).IsEqualTo(Utility.sDbnull(txtGoiThau.Text, string.Empty))
                                                            .And(TThau.Columns.TrangThai).IsNotEqualTo(1)
                                                            .And(TThau.Columns.IdThau).IsNotEqualTo(Utility.Int32Dbnull(txtIDThau.Text, -1))
                                                            .And(TThau.Columns.KieuThuocvt).IsEqualTo(Utility.sDbnull(kieu_thuocvt, "THUOC"))
                                                            .ExecuteAsCollection<TThauCollection>();
                if (_obj != null && _obj.Count > 0)
                {
                    Utility.ShowMsg(string.Format("Đã tồn tại Thầu với số QĐ {0} - Gói: {1} - Nhà cung cấp {2} mà chưa được duyệt thầu. Mời bạn kiểm tra lại", txtSoQDinh.Text, txtGoiThau.Text, txtNhacungcap.Text), "Thông báo", MessageBoxIcon.Warning);
                    txtSoThau.Focus();
                    return false;
                }
            }
            if (Utility.Int32Dbnull(cbohinhthucthau.SelectedIndex, -1) <= 0)
            {
                Utility.ShowMsg("Chưa chọn hình thức thầu", "Thông báo", MessageBoxIcon.Warning);
                cbohinhthucthau.Focus();
                return false;
            }

            if (grdChiTietThau.DataSource == null || (grdChiTietThau.DataSource != null && grdChiTietThau.GetDataRows().Count() <= 0))
            {
                Utility.ShowMsg("Chưa có chi tiết thầu.", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                foreach (GridEXRow gridExRow in grdChiTietThau.GetDataRows())
                {
                    decimal _nSoLuong = Utility.Int32Dbnull(gridExRow.Cells[TThauChitiet.Columns.SoLuong].Value, -1);
                    string _sDrugName = Utility.sDbnull(gridExRow.Cells[DmucThuoc.Columns.TenThuoc].Value, string.Empty);
                    if (_nSoLuong <= 0)
                    {
                        Utility.ShowMsg(string.Format("Mặt hàng {0} chưa nhập số lượng thầu.", _sDrugName), "Thông báo", MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            if (chkDieuTiet.Checked)
            {
                if (Utility.Int32Dbnull(txtNoidieutiet.MyID, -1) <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh viện điều tiết", "Thông báo", MessageBoxIcon.Warning);
                    txtNoidieutiet.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtSoHd_dieutiet.Text))
                {
                    Utility.ShowMsg("Bạn cần nhập Số CV/HĐ điều tiết", "Thông báo", MessageBoxIcon.Warning);
                    txtSoHd_dieutiet.Focus();
                    return false;
                }
                if (dtpNgayHD_DieuTiet.Value.Date>dtpNgayKT_DieuTiet.Value.Date)
                {
                    Utility.ShowMsg("Ngày bắt đầu HĐ điều tiết phải <= ngày kết thúc HĐ điều tiết", "Thông báo", MessageBoxIcon.Warning);
                    dtpNgayHD_DieuTiet.Focus();
                    return false;
                }
                try
                {
                    if (dtpNgayHD_DieuTiet.Value.Date >DateTime.Now)
                    {
                        Utility.ShowMsg(string.Format("Ngày điều tiết không được lớn hơn ngày hiện tại"), "Thông báo", MessageBoxIcon.Warning);
                        dtpNgayHD_DieuTiet.Focus();
                        return false;
                    }
                    if (dtpNgayHD_DieuTiet.Value.Year < 2010)
                    {
                        Utility.ShowMsg(string.Format("Ngày điều tiết không hợp lệ."), "Thông báo", MessageBoxIcon.Warning);
                        dtpNgayHD_DieuTiet.Focus();
                        return false;
                    }
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Ngày điều tiết không hợp lệ", "Thông báo", MessageBoxIcon.Warning);
                    dtpNgayHD_DieuTiet.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(dtpNgayKT_DieuTiet.Text))
                {
                    Utility.ShowMsg("Chưa nhập ngày kết thúc điều tiết", "Thông báo", MessageBoxIcon.Warning);
                    dtpNgayKT_DieuTiet.Focus();
                    return false;
                }
                else
                {
                    try
                    {
                        if (dtpNgayKT_DieuTiet.Value.Date > DateTime.Now)
                        {
                            Utility.ShowMsg(string.Format("Ngày kết thúc điều tiết không được nhỏ hơn ngày điều tiết "), "Thông báo", MessageBoxIcon.Warning);
                            dtpNgayKT_DieuTiet.Focus();
                            return false;
                        }
                        if (dtpNgayKT_DieuTiet.Value.Year < 2010)
                        {
                            Utility.ShowMsg(string.Format("Ngày kết thúc điều tiết không hợp lệ."), "Thông báo", MessageBoxIcon.Warning);
                            dtpNgayKT_DieuTiet.Focus();
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        Utility.ShowMsg("Ngày kết thúc điều tiết không hợp lệ", "Thông báo", MessageBoxIcon.Warning);
                        dtpNgayKT_DieuTiet.Focus();
                        return false;
                    }
                }

            }

            return true;
        }
        #endregion

        private void txtSoThau_Validated(object sender, EventArgs e)
        {
           
        }

        private void chkDieuTiet_CheckedChanged(object sender, EventArgs e)
        {
            pnlDieuTiet.Enabled = chkDieuTiet.Checked;
        }

    

        #region AddDetail
        private void AddDetail()
        {
            int id_thuoc = Utility.Int32Dbnull(txtDrug_ID.Text);
            var dataRows = from thuoc in dtChiTietThau.AsEnumerable()
                           where Utility.Int32Dbnull(thuoc[TThauChitiet.Columns.IdThuoc]) == id_thuoc
                                 && Utility.sDbnull(thuoc[TThauChitiet.Columns.DangSoche]) == Utility.sDbnull(txtDangSoChe.Text)
                                 && Utility.sDbnull(thuoc[TThauChitiet.Columns.NhomThau]) == Utility.sDbnull(txtNhomThau.Text)
                                 && Utility.sDbnull(thuoc[TThauChitiet.Columns.Tccl]) == Utility.sDbnull(txtTCCL.Text)
                                 && Utility.sDbnull(thuoc[TThauChitiet.Columns.NhomTckt]) == Utility.sDbnull(txtNhomTCKT.Text)
                                 && Utility.sDbnull(thuoc[TThauChitiet.Columns.SoDangky]) == Utility.sDbnull(txtSoDK.Text)
                                 && Utility.DecimaltoDbnull(thuoc[TThauChitiet.Columns.DonGia]) == Utility.DecimaltoDbnull(txtDonGia.Text)
                           select thuoc;
            if (!dataRows.Any())
            {
                DataRow drv = dtChiTietThau.NewRow();

                drv[TThauChitiet.Columns.IdThau] = Utility.Int32Dbnull(txtIDThau.Text, -1);
                drv[TThauChitiet.Columns.IdThuoc] = Utility.Int32Dbnull(id_thuoc, -1);
                drv[TThauChitiet.Columns.SoLuong] = Utility.DecimaltoDbnull(txtSoLuong.Text);
                drv[TThauChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(txtDonGia.Text);

                drv[TThauChitiet.Columns.DangSoche] = Utility.sDbnull(txtDangSoChe.Text);
                drv[TThauChitiet.Columns.Tccl] = Utility.sDbnull(txtTCCL.Text);
                drv[TThauChitiet.Columns.NhomTckt] = Utility.sDbnull(txtNhomTCKT.Text);
                drv[TThauChitiet.Columns.NguonGoc] = Utility.sDbnull(txtNguonGoc.Text);
                drv[TThauChitiet.Columns.NhomThau] = Utility.sDbnull(txtNhomThau.MyCode);
                drv[TThauChitiet.Columns.SoDangky] = Utility.sDbnull(txtSoDK.Text);
                drv["thanh_tien"] = Utility.DecimaltoDbnull(txtThanhTien.Text);

                var query = txtDrugName.dtData.Select(string.Format("{0} = {1}", DmucThuoc.Columns.IdThuoc, id_thuoc));
                if (query.GetLength(0) > 0)
                {
                    DataRow _thuoc = query.FirstOrDefault();

                    drv[DmucThuoc.Columns.MaThuoc] = Utility.sDbnull(_thuoc[DmucThuoc.Columns.MaThuoc]);
                    drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(_thuoc[DmucThuoc.Columns.TenThuoc]);
                    drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(_thuoc[DmucThuoc.Columns.HamLuong]);
                    drv[VDmucThuoc.Columns.TenHoatchat] = Utility.sDbnull(_thuoc[VDmucThuoc.Columns.TenHoatchat]);
                    drv[VDmucThuoc.Columns.TenHangsanxuat] = Utility.sDbnull(_thuoc[VDmucThuoc.Columns.TenHangsanxuat]);
                    drv[VDmucThuoc.Columns.TenNuocsanxuat] = Utility.sDbnull(_thuoc[VDmucThuoc.Columns.TenNuocsanxuat]);
                    drv[VDmucThuoc.Columns.TenNhomthuoc] = Utility.sDbnull(_thuoc[VDmucThuoc.Columns.TenNhomthuoc]);
                    drv[VDmucThuoc.Columns.TenLoaithuoc] = Utility.sDbnull(_thuoc[VDmucThuoc.Columns.TenLoaithuoc]);
                    drv[VDmucThuoc.Columns.TenDonvitinh] = Utility.sDbnull(_thuoc[VDmucThuoc.Columns.TenDonvitinh]);
                }
                else
                {
                    VDmucThuoc objDmucThuoc = new Select().From(VDmucThuoc.Schema).Where(VDmucThuoc.Columns.IdThuoc).IsEqualTo(id_thuoc).ExecuteSingle<VDmucThuoc>();
                    drv[DmucThuoc.Columns.MaThuoc] = Utility.sDbnull(objDmucThuoc.MaThuoc);
                    drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objDmucThuoc.TenThuoc);
                    drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(objDmucThuoc.HamLuong);
                    drv[VDmucThuoc.Columns.TenHangsanxuat] = Utility.sDbnull(objDmucThuoc.TenHangsanxuat);
                    drv[VDmucThuoc.Columns.TenNuocsanxuat] = Utility.sDbnull(objDmucThuoc.TenNuocsanxuat);
                    drv[VDmucThuoc.Columns.TenNhomthuoc] = Utility.sDbnull(objDmucThuoc.TenNhomthuoc);
                    drv[VDmucThuoc.Columns.TenLoaithuoc] = Utility.sDbnull(objDmucThuoc.TenLoaithuoc);
                    drv[VDmucThuoc.Columns.TenDonvitinh] = Utility.sDbnull(objDmucThuoc.TenDonvitinh);
                    drv[TThauChitiet.Columns.MaDvt] = Utility.sDbnull(objDmucThuoc.MaDonvitinh);
                }
                dtChiTietThau.Rows.Add(drv);
                Utility.GonewRowJanus(grdChiTietThau, "id_thuoc", Utility.sDbnull(id_thuoc));
            }
            else
            {
                DataRow firstvalue = dataRows.FirstOrDefault();
                if (firstvalue != null)
                {
                    if (m_enAct == action.Add)
                    {
                        if (Utility.Int32Dbnull(firstvalue[TThauChitiet.Columns.IdThauCt], -1) > 0)
                        {
                            Utility.ShowMsg("Thuốc/vật tư đã tồn tại trong thông tin thầu và đã được xác nhận. Không thể bổ sung/chỉnh sửa");
                            Utility.GonewRowJanus(grdChiTietThau, "id_thuoc", Utility.sDbnull(id_thuoc));
                            return;
                        }
                    }
                    firstvalue[TThauChitiet.Columns.SoLuong] = Utility.DoubletoDbnull(firstvalue[TThauChitiet.Columns.SoLuong]) +
                                                                  Utility.DoubletoDbnull(txtSoLuong.Text);
                    firstvalue["thanh_tien"] = Utility.DoubletoDbnull(firstvalue[TThauChitiet.Columns.SoLuong]) * Utility.DoubletoDbnull(firstvalue[TThauChitiet.Columns.DonGia]);
                    firstvalue.AcceptChanges();
                }
            }

            dtChiTietThau.AcceptChanges();
            grdChiTietThau.UpdateData();
            grdChiTietThau.Refresh();
            ModifyCommands();
        }

        private bool isValidChitiet()
        {
            _objThau = TThau.FetchByID(Utility.Int32Dbnull(txtIDThau.Text));
            if (Utility.Int32Dbnull(txtIDThau.Text) > 0 && _objThau == null)
            {
                Utility.ShowMsg("Không tồn tại thông tin thầu, hoặc thông tin thầu đã bị xóa. Đề nghị kiểm tra lại.", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            if (_objThau != null )
            {
                if (m_enAct == action.Add)//Bổ sung
                {
                }
                else//Thêm, sửa
                {
                    if (Utility.Int32Dbnull(_objThau.TrangThai, -1) > 0)
                    {
                        Utility.ShowMsg("Thông tin thầu đã được xác nhận không được phép chỉnh sửa", "Thông báo", MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            //SqlQuery sqlQuery = new Select().From(DmucThuoc.Schema).Where(DmucThuoc.Columns.IdThuoc).IsEqualTo(Utility.Int32Dbnull(txtDrug_ID.Text));
            //if (sqlQuery.GetRecordCount() <= 0)
            //{
            //    Utility.ShowMsg("Không tồn tại thuốc", "Thông báo", MessageBoxIcon.Warning);
            //    txtDrugName.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtNhomThau.MyCode))
            {
                Utility.ShowMsg("Bạn phải chọn nhóm thầu trong danh mục", "Thông báo", MessageBoxIcon.Warning);
                txtNhomThau.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtDangSoChe.Text))
            //{
            //    Utility.ShowMsg("Bạn chưa nhập dạng sơ chế", "Thông báo", MessageBoxIcon.Warning);
            //    txtDangSoChe.Focus();
            //    return false;
            //}
            if (kieu_thuocvt == "THUOC" && string.IsNullOrEmpty(txtTCCL.Text))
            {
                Utility.ShowMsg("Bạn chưa nhập TCCL", "Thông báo", MessageBoxIcon.Warning);
                txtTCCL.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSoLuong.Text,0) <= 0)
            {
                Utility.ShowMsg("Số lượng phải > 0", "Thông báo", MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtDonGia.Text, 0) <= 0)
            {
                Utility.ShowMsg("Đơn giá bạn đang để bằng không(=0). Nhấn OK để tiếp tục. Muốn sửa đơn giá, bạn có thể nhấn đúp chuột vào dòng chi tiết vừa thêm để chỉnh sửa lại", "Thông báo", MessageBoxIcon.Warning);
            }
            return true;
        }

        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            if (!isValidChitiet()) return;
            if (isUpdate)
            {
                isUpdate = false;
                Update();
            }
            else
                AddDetail();
            ClearControl();
            ModifyCommands();
            txtDrugName.Focus();
        }
        private void Update()
        {
            grdChiTietThau.CurrentRow.BeginEdit();
            grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.SoLuong].Value = Utility.DecimaltoDbnull(txtSoLuong.Text);
            grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.DangSoche].Value = Utility.sDbnull(txtDangSoChe.Text);
            grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.Tccl].Value = Utility.sDbnull(txtTCCL.Text);
            grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.NhomTckt].Value = Utility.sDbnull(txtNhomTCKT.Text);
            grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.NguonGoc].Value = Utility.sDbnull(txtNguonGoc.Text);
            grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.NhomThau].Value = Utility.sDbnull(txtNhomThau.MyCode);
            grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.SoDangky].Value = Utility.sDbnull(txtSoDK.Text);
            grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.DonGia].Value = Utility.DecimaltoDbnull(txtDonGia.Text);
            grdChiTietThau.CurrentRow.Cells["thanh_tien"].Value = Utility.DecimaltoDbnull(txtThanhTien.Text);
            grdChiTietThau.CurrentRow.EndEdit();
            grdChiTietThau.UpdateData();
            grdChiTietThau.Refresh();
            ModifyCommands();
        }
        #endregion

        private void ClearControl()
        {

            txtMathuoc.Clear();
            txtDrug_ID.Clear();
            txtDrugName.Clear();
            txtDVT.SetId(-1);
            txtnhasx.Clear();
            txtDangSoChe.Clear();
            txtTCCL.Clear();
            txtNhomTCKT.Clear();
            txtNguonGoc.Clear();
            txtSoLuong.Clear();
            txtDonGia.Clear();
            txtThanhTien.Clear();
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            txtThanhTien.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtDonGia.Text) * Utility.DecimaltoDbnull(txtSoLuong.Text));
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            txtThanhTien.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtDonGia.Text) * Utility.DecimaltoDbnull(txtSoLuong.Text));
            lblGiaNhap.ForeColor = Utility.DecimaltoDbnull(txtDonGia.Text) > 0 ? lblThanhtien.ForeColor : lblSL.ForeColor;
        }

        #region grdChiTietThau
        private void grdChiTietThau_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (grdChiTietThau.CurrentRow != null && grdChiTietThau.CurrentRow.RowType == RowType.Record)
            {
                GridEXRow gridExRow = grdChiTietThau.CurrentRow;
                long id_thau_ct = Utility.Int64Dbnull(gridExRow.Cells[TThauChitiet.Columns.IdThauCt].Value);
                long id_thau = Utility.Int64Dbnull(gridExRow.Cells[TThauChitiet.Columns.IdThau].Value);
                string _maThuoc = Utility.sDbnull(gridExRow.Cells["ma_thuoc"].Value);
                string _tenThuoc = Utility.sDbnull(gridExRow.Cells["ten_thuoc"].Value);
                SqlQuery sql =
                      new Select().From(TThauDieutietCt.Schema).Where(TThauDieutietCt.Columns.IdThau).IsEqualTo(id_thau)
                      .And(TThauDieutietCt.Columns.IdThauCt).IsEqualTo(id_thau_ct);
                if (sql.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Mặt hàng bạn chọn xóa khỏi thầu đã được áp thầu hoặc điều tiết thầu nên không thể xóa. Vui lòng kiểm tra lại");
                    return;
                }
                if (m_enAct != action.Add)//Thêm mới hoặc sửa thầu
                {
                    SqlQuery sqlQuery = new Select().From(TThau.Schema)
                                .Where(TThau.Columns.IdThau).IsEqualTo(Utility.Int32Dbnull(txtIDThau.Text))
                                .And(TThau.Columns.TrangThai).IsEqualTo(1);
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Thông tin thầu đã được xác nhận không được phép xóa", "Thông báo", MessageBoxIcon.Error);
                        return;
                    }
                }
                else//Bổ sung chi tiết thầu
                {
                    if (Utility.Int32Dbnull(id_thau_ct, -1) > 0)
                    {
                        Utility.ShowMsg("Mặt hàng đang chọn không được phép xóa. Chỉ được phép xóa các mặt hàng mới bổ sung.");
                        Utility.GonewRowJanus(grdChiTietThau, "id_thuoc", Utility.sDbnull(id_thau_ct));
                        return;
                    }
                }

                int num = TThauChitiet.Delete(id_thau_ct);
                if (num > 0)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa chi tiết thông tin thầu. (ID Thầu: {0} - Số thầu: {1} - Số QĐ: {2}, ID chi tiết: {3} - Thuốc/vật tư: {4}-{5})", txtIDThau.Text, txtSoThau.Text, txtSoQDinh.Text, id_thau_ct, _maThuoc, _tenThuoc), newaction.Delete, "UI");
                }
                gridExRow.Delete();
                grdChiTietThau.UpdateData();
                dtChiTietThau.AcceptChanges();
                sql = new Select().From(TThauChitiet.Schema)
                              .Where(TThauChitiet.Columns.IdThau).IsEqualTo(Utility.Int32Dbnull(txtIDThau.Text));
                if (sql.GetRecordCount() <= 0)
                {
                    TThau.Delete(Utility.Int32Dbnull(txtIDThau.Text));
                    txtIDThau.Text = "-1";
                    //Đưa về chế độ thêm mới thầu
                    dtChiTietThau.Clear();
                    m_enAct = action.Insert;
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa thông tin thầu do đã xóa hết chi tiết. (ID Thầu: {0} - Số thầu: {1} - Số QĐ: {2})", txtIDThau.Text, txtSoThau.Text, txtSoQDinh.Text), newaction.Delete, "UI");
                }
            }
            ModifyCommands();
        }

        private void grdChiTietThau_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "SO_LUONG")
                {
                    long id_thau_ct = Utility.Int64Dbnull(grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.IdThauCt].Value);
                    if (id_thau_ct <= 0)//Mới bổ sung
                    {
                    }
                    else//Dòng cũ bị chỉnh sửa thì cần kiểm tra trạng thái xác nhận
                    {
                        TThau _oThau = new Select().From(TThau.Schema).Where(TThau.Columns.IdThau).IsEqualTo(Utility.Int32Dbnull(txtIDThau.Text)).ExecuteSingle<TThau>();
                        if (_oThau.TrangThai > 0)
                        {
                            Utility.ShowMsg("Thầu đã được xác nhận nên không thể sửa trực tiếp trên lưới. Muốn chỉnh sửa số lượng bạn nên dùng tính năng Áp thầu hoặc Điều tiêt thầu");
                            e.Value = e.InitialValue;
                            return;
                        }
                    }
                    int sl = Utility.Int32Dbnull(e.Value);
                    if (sl < 0)
                    {
                        Utility.ShowMsg("Số lượng phải >= 0", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = e.InitialValue;
                        return;
                    }
                    grdChiTietThau.CurrentRow.BeginEdit();
                    grdChiTietThau.CurrentRow.Cells["thanh_tien"].Value = Utility.DecimaltoDbnull(sl) * Utility.DecimaltoDbnull(grdChiTietThau.CurrentRow.Cells[TThauChitiet.Columns.DonGia].Value);
                    grdChiTietThau.CurrentRow.EndEdit();

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        #endregion

        private void txtDonGia_TextChanged_1(object sender, EventArgs e)
        {

        }

        

    }
}