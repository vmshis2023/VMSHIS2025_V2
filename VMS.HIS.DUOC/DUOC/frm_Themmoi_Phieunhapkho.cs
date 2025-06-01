using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel.Log;
using Janus.Windows.GridEX;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
using VNS.HIS.UI.THUOC;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.DANHMUC;
using System.Threading;
using System.Transactions;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_Themmoi_Phieunhapkho : Form
    {
       
        #region"khai báo biến"
        string _phuongphapTinhgiaban = "2";//0= Tính theo thặng dư;1= Tính theo VAT+Thặng dư;2= Tính theo % 
        decimal _phantramSovoigianhap = 0;
        bool _chophepNhapgiaban = true;
        public DataTable p_mDataPhieuNhapKho = new DataTable();
        private DataTable m_dtDataPhieuChiTiet=new DataTable();
        private DataTable m_dtDataKhoNhap=new DataTable();
        private DataTable m_dtDataNhaCungCap=new DataTable();
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
       // private DataTable m_dtDataNhaCungCap=new DataTable();
        public action m_enAction = action.Insert;
        public bool b_Cancel = true;
        public Janus.Windows.GridEX.GridEX grdList;
        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        public string KIEU_THUOC_VT = "THUOC";
        int songaycanhbao = 10;//Số ngày giữa ngày hết hạn và ngày hiện tại cần bật cảnh báo để người dùng nhập ngày hết hạn cho đúng
        #endregion
        BackgroundWorker _worker = null;
        #region "khai báo khởi tạo Form"
        public frm_Themmoi_Phieunhapkho()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            if (PropertyLib._NhapkhoProperties.autosaveAfter > 0)
            {
                _worker = new BackgroundWorker();
                _worker.DoWork += _worker_DoWork;
                if (!_worker.IsBusy)
                    _worker.RunWorkerAsync();

            }
            txtDrugName._OnGridSelectionChanged += txtdrug__OnGridSelectionChanged;
            txtDrugName._OnChangedView += txtdrug__OnChangedView;
            //dtNgayHetHan.Value = globalVariables.SysDate.AddYears(1);
            dtNgayhoadon.Value =dtpNgayLapphieu.Value= globalVariables.SysDate;
            InitEvents();
            CauHinh();
        }
        bool _Autosave = false;
        bool isActive = true;
        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isActive)
            {
                Thread.Sleep(PropertyLib._NhapkhoProperties.autosaveAfter*1000);
                _Autosave = true;
                if (IsValidNhapKho_Auto())
                    AutoSave();
            }
        }
        void InitEvents()
        {
            txtChietkhau.TextChanged += new EventHandler(txtChietkhau_TextChanged);
            nmrThangDu.ValueChanged += new EventHandler(nmrThangDu_ValueChanged);
            txtTongTien.KeyDown += new KeyEventHandler(txtTongTien_KeyDown);
            this.FormClosing += frm_Themmoi_Phieunhapkho_FormClosing;
            
            txtSoluong.TextChanged += new EventHandler(txtSoluong_TextChanged);
            this.txtTongTien.TextChanged += new System.EventHandler(this.txtTongTien_TextChanged);
            this.txtTongTien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTongTien_KeyPress);
            this.txtVAT.TextChanged += new System.EventHandler(this.txtVAT_TextChanged);
            this.txtSoHoaDon.TextChanged += new System.EventHandler(this.txtSoHoaDon_TextChanged);
            this.cmdXoaThongTin.Click += new System.EventHandler(this.cmdXoaThongTin_Click);
            this.cmdInPhieuNhap.Click += new System.EventHandler(this.cmdInPhieuNhap_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.txtDrug_ID.TextChanged += new System.EventHandler(this.txtDrug_ID_TextChanged);
            this.grdChitiet.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdChitiet_CellUpdated);
            this.grdChitiet.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(this.grdChitiet_UpdatingCell);
            
            this.cmdAddDetail.Click += new System.EventHandler(this.cmdAddDetail_Click);
            this.cmdHuyThongTin.Click += new System.EventHandler(this.cmdHuyThongTin_Click);
            this.txtThanhTien.TextChanged += new System.EventHandler(this.txtThanhTien_TextChanged);
            this.txtThanhTien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtThanhTien_KeyPress);
            this.nmrThangDu.Click += new System.EventHandler(this.nmrThangDu_Click);
            this.Load += new System.EventHandler(this.frm_Themmoi_Phieunhapkho_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Themmoi_Phieunhapkho_KeyDown);
            this.cmdCauHinh.Click += new System.EventHandler(this.cmdCauHinh_Click);
            this.cmdThemPhieuNhap.Click += new System.EventHandler(this.cmdThemPhieuNhap_Click);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            //chkGiaBHYT.CheckedChanged += new EventHandler(chkGiaBHYT_CheckedChanged);
            chkCloseAfterSaving.CheckedChanged += new EventHandler(chkCloseAfterSaving_CheckedChanged);
            txtDrugName._OnEnterMe += new UCs.AutoCompleteTextbox_Thuoc.OnEnterMe(txtDrugName__OnEnterMe);
            grdChitiet.EditingCell += new EditingCellEventHandler(grdChitiet_EditingCell);
            grdChitiet.CurrentCellChanged += new EventHandler(grdChitiet_CurrentCellChanged);
            txtLyDoNhap._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtLyDoNhap__OnShowData);
            //txtNhacungcap._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNhacungcap__OnShowData);
            txtNguoiGiao._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNguoiGiao__OnShowData);
            txtNguoinhan._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNguoinhan__OnShowData);

            txtDongia._OnTextChanged += txtDongia__OnTextChanged;
            txtGiaDV._OnTextChanged += txtGiaban__OnTextChanged;
           // txtKhonhap._OnEnterMe += txtKhonhap__OnEnterMe;
            txtsoDK._OnShowData += txtsoDK__OnShowData;
            txtsoQDthau_Dmuc._OnShowData += txtsoQDthau__OnShowData;
            cmdNewDrug.Click += cmdNewDrug_Click;
            cmdNewStock.Click += cmdNewStock_Click;
            cmdQuickCreate.Click += cmdQuickCreate_Click;
           // txtNhacungcap._OnEnterMe += txtNhacungcap__OnEnterMe;
            cboSoQDthau.SelectedIndexChanged += cboSoQDthau_SelectedIndexChanged;
            cboKhonhap.KeyDown += cboKhoThuoc_KeyDown;
            grdChitiet.RowDoubleClick += GrdChitiet_RowDoubleClick;
        }
        bool isUpdate = false;
        private void GrdChitiet_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdChitiet) || m_enAction==action.View) return;
                //Load dữ liệu lên để sửa
                if (Utility.ByteDbnull(grdChitiet.GetValue("trang_thai")) == 1)
                {
                    Utility.ShowMsg("Chi tiết Thuốc-VTTH trong phiếu đã được xác nhận nên bạn không thể chỉnh sửa");
                    return;
                }
                txtDrugName.SetId(grdChitiet.GetValue("id_thuoc"));
                txtDrug_ID.Text = Utility.sDbnull(grdChitiet.GetValue("id_thuoc"));
                txtmathuoc.Text = Utility.sDbnull(grdChitiet.GetValue("ma_thuoc"));

                txtTCCL.Text = Utility.sDbnull(grdChitiet.GetValue("tccl"));
                txtsoDK.Text = Utility.sDbnull(grdChitiet.GetValue("so_dky"));
                dtNgayHetHan.Value = Convert.ToDateTime(grdChitiet.GetValue("ngay_hethan"));
                txtSoLo.Text = Utility.sDbnull(grdChitiet.GetValue("so_lo"));
                txtSoluong.Text = Utility.sDbnull(grdChitiet.GetValue("so_luong"));

                txtDongia.Text = Utility.sDbnull(grdChitiet.GetValue("gia_nhap"));
                txtGiaDV.Text = Utility.sDbnull(grdChitiet.GetValue("gia_ban"));
                txtGiaBHYT.Text = Utility.sDbnull(grdChitiet.GetValue("gia_bhyt"));
                txtPhuthuDT.Text = Utility.sDbnull(grdChitiet.GetValue("gia_phuthu_dungtuyen"));
                txtPhuthuTT.Text = Utility.sDbnull(grdChitiet.GetValue("gia_phuthu_traituyen"));
                txtChietkhau.Text = Utility.sDbnull(grdChitiet.GetValue("chiet_khau"));
                isUpdate = true;
            }
            catch (Exception ex)
            {

            }
        }

        void cboKhoThuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        void cboSoQDthau_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTrongthau.Checked)
                {
                    DataTable dtThuoc = SPs.ThuocThauLaydanhmucthuoc(Utility.sDbnull(cboSoQDthau.SelectedValue), Utility.sDbnull(cboNhaCungcap.SelectedValue), this.KIEU_THUOC_VT).GetDataSet().Tables[0];
                    if (chkTrongthau.Checked)
                    {
                        txtDrugName.dtData = dtThuoc;
                        txtDrugName.ChangeDataSource();
                        txtDrugName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void txtNhacungcap__OnEnterMe()
        {
            try
            {
                DataTable dtDataSoQD = Utility.ExecuteSql(string.Format("select distinct maso_qdinh from t_thau_qdinh where ma_nha_cc='{0}'", cboNhaCungcap.SelectedValue.ToString()),CommandType.Text).Tables[0];
                DataBinding.BindDataCombobox(cboSoQDthau, dtDataSoQD, "maso_qdinh", "maso_qdinh");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        bool blnCancel = false;
        void cmdQuickCreate_Click(object sender, EventArgs e)
        {
            try
            {
                cmdQuickCreate.Enabled = false;
                cmdCancel.BringToFront();
                prgBar1.Value = 0;
                prgBar1.Visible = true;
                prgBar1.Minimum = 0;
                prgBar1.Maximum = txtDrugName.dtData.Rows.Count;
                DataTable dtThuockho = new Select().From(TThuockho.Schema).Where(TThuockho.Columns.IdKho).IsEqualTo(Utility.Int32Dbnull( cboKhonhap.SelectedValue,-1)).ExecuteDataSet().Tables[0];
                string sl = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_SL_MACDINH", "0", false);
                foreach (DataRow dr in txtDrugName.dtData.Rows)
                {
                    if (blnCancel) break;
                    txtDrug_ID.Text = Utility.sDbnull(dr["id_thuoc"]);
                    if (chkChinhapthuocmoi.Checked && dtThuockho.Select("id_thuoc=" + txtDrug_ID.Text).Length <= 0)
                    {
                        dtNgayHetHan.Text = "01/01/2050";
                        //if (objThuoc != null)//Tự động điền số tồn đầu vào mục số lượng. Chỉ có tác dụng khi muốn khởi tạo danh mục thuốc và tồn đầu
                        //    txtSoluong.Text = Utility.sDbnull(objThuoc.TonDau, 10000);
                        //else
                        txtSoluong.Text = sl;
                        if (txtsoQDthau_Dmuc.Text.TrimStart().TrimEnd().Length <= 0) txtsoQDthau_Dmuc.Text = "QD01";
                        if (txtsoDK.Text.TrimStart().TrimEnd().Length <= 0) txtsoDK.Text = "DK1";
                        if (txtSoLo.Text.TrimStart().TrimEnd().Length <= 0) txtSoLo.Text = "LO1";
                        cmdAddDetail.PerformClick();
                    }
                    prgBar1.Value += 1;
                    
                }
                blnCancel = false;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                cmdQuickCreate.Enabled = true;
                prgBar1.Visible = false;
            }
        }

        void cmdNewStock_Click(object sender, EventArgs e)
        {
            frm_DanhmucKhothuoc dmuc_kho = new frm_DanhmucKhothuoc(KIEU_THUOC_VT);
            dmuc_kho.AutoNew = true;
            dmuc_kho.ShowDialog();
            if (!dmuc_kho.m_blnCancel)
            {
                InitStocks();
            }
        }

        void cmdNewDrug_Click(object sender, EventArgs e)
        {
            frm_qhe_doituong_thuoc_coban dmuc_thuoc = new frm_qhe_doituong_thuoc_coban(KIEU_THUOC_VT);
            dmuc_thuoc.AutoNew = true;
            dmuc_thuoc.ShowDialog();
            if (!dmuc_thuoc.m_blnCancel)
            {
                LoadAuCompleteThuoc();
            }
        }

        void txtsoQDthau__OnShowData()
        {
            VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtsoQDthau_Dmuc);
        }

        void txtsoDK__OnShowData()
        {
            VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtsoDK);
        }

        void txtKhonhap__OnEnterMe()
        {
            
        }

        void txtGiaban__OnTextChanged(string text)
        {
            //if (_bhytGiabhytBangGiaban)
            //    txtGiaBHYT.Text = txtGiaDV.Text;
            //else
            //    txtGiaBHYT.Text = txtDongia.Text;
        }

        void txtDongia__OnTextChanged(string text)
        {
            if (_phuongphapTinhgiaban == "0")
                nmrThangDu.Value = TinhThangDutheoQuyetDinhBYT(Utility.DecimaltoDbnull(txtDongia.Text, 0));
            else
                nmrThangDu.Value = 0;
            TinhGiaBan();
            ThanhTien();
        }

        void frm_Themmoi_Phieunhapkho_FormClosing(object sender, FormClosingEventArgs e)
        {
            isActive = false;
        }

        void txtNguoinhan__OnShowData()
        {
            VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtNguoinhan);
            
        }
       
        void txtNguoiGiao__OnShowData()
        {
            VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtNguoiGiao);
        }

        void txtNhacungcap__OnShowData()
        {
            //VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtNhacungcap);
        }

        void txtLyDoNhap__OnShowData()
        {

            VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtLyDoNhap);
        }

        void grdChitiet_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (grdChitiet.CurrentColumn != null) grdChitiet.CurrentColumn.InputMask = "";
        }
        string oldInpustMask = "";
        void grdChitiet_CurrentCellChanged(object sender, EventArgs e)
        {

         
            
        }

      

        void txtDrugName__OnEnterMe()
        {
            int _idthuoc = Utility.Int32Dbnull(txtDrugName.MyID, -1);
            txtDrug_ID.Text = _idthuoc.ToString();
            
        }

        void chkCloseAfterSaving_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._NhapkhoProperties.Themmoilientuc = !chkCloseAfterSaving.Checked;
            PropertyLib.SaveProperty(PropertyLib._NhapkhoProperties);
        }

       
        
        private void CauHinh()
        {

            nmrThangDu.Enabled = _phuongphapTinhgiaban=="0";
            chkCloseAfterSaving.Checked = !PropertyLib._NhapkhoProperties.Themmoilientuc;
            if (_phuongphapTinhgiaban!="0") nmrThangDu.Value = 0;
            txtGiaDV.Enabled = _chophepNhapgiaban;
            txtGiaDV.BackColor = _chophepNhapgiaban ? txtSoluong.BackColor : txtThanhTien.BackColor;
           
            //chkGiaBHYT.Enabled = BHYT_CHOPHEPNHAPGIA;
            //if (!BHYT_CHOPHEPNHAPGIA) chkGiaBHYT.Checked = false;
            //txtGiaBHYT.Enabled = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA;
            //txtGiaBHYT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;

            //txtPhuthuDT.Enabled = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA;
            //txtPhuthuDT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;

            //txtPhuthuTT.Enabled = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA;
            //txtPhuthuTT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;
        }
        void txtTongTien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDrugName.Focus();
        }

        void nmrThangDu_ValueChanged(object sender, EventArgs e)
        {

            TinhGiaBan();
        }

        void txtChietkhau_TextChanged(object sender, EventArgs e)
        {
            ThanhTien();
        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc load thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Themmoi_Phieunhapkho_Load(object sender, EventArgs e)
        {
            txtKieuthuocVT.Init();
            LoadData();  
        }
        void InitStocks()
        {
            m_dtDataKhoNhap = KIEU_THUOC_VT == "THUOC" ? CommonLoadDuoc.LAYDANHMUCKHO(-1, "ALL", "THUOC,THUOCVT", "CHANLE,CHAN", 0, 100, 1) : CommonLoadDuoc.LAYDANHMUCKHO(-1, "ALL", "VT,THUOCVT", "CHANLE,CHAN", 0, 100, 1);//CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN() : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
            txtKieuthuocVT.SetCode(KIEU_THUOC_VT);
            if (m_dtDataKhoNhap.Rows.Count > 1)
            {
                DataBinding.BindDataCombobox(cboKhonhap, m_dtDataKhoNhap, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn kho---", false);
            }
            else
            {
                DataBinding.BindDataCombobox(cboKhonhap, m_dtDataKhoNhap, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn---", true);

            }
          
        }

        void LoadData()
        {
            try
            {
                bool BatnhapQDthau = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_BATNHAPTHONGTIN_QDTHAU", "0", false) == "1";
                lblQDthau.ForeColor = lblSTTThau.ForeColor = BatnhapQDthau ? Color.Red : Color.Black;
                txtsoQDthau_Dmuc.Enabled = txtsoDK.Enabled = BatnhapQDthau;
                _phuongphapTinhgiaban = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_PHUONGPHAP_TINHGIABAN", "1", false);
                _phantramSovoigianhap = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_PHANTRAM_SOVOIGIANHAP", "0", false), 0);
                _chophepNhapgiaban = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_CHOPHEP_NHAPGIABAN", "1", false) == "1";
                lblChietkhau.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_HIENTHI_CHIETKHAUCHITIET", "0", false) == "1";
                txtChietkhau.Visible = lblChietkhau.Visible;
                txtPhuthuDT.TabStop = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_TABSTOP_PHUTHU", "0", false) == "1";
                cmdQuickCreate.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_HIENTHITAOPHIEUNHANH", "0", false) == "1";
                txtPhuthuTT.TabStop = txtPhuthuDT.TabStop;
                //lblTongtien.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_KIEMTRATONGTIEN", "0", false) == "1";
                //txtTongtienHD.Visible = lblTongtien.Visible;
                DataBinding.BindDataCombobox(cboNhaCungcap, THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHACUNGCAP", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten, "---Chọn NCC---", false);
                txtNguoiGiao.Init();
                txtNguoinhan.Init();
                txtsoDK.Init();
                txtsoQDthau_Dmuc.Init();
                txtLyDoNhap.Init();
                cmdQuickCreate.Visible = chkChinhapthuocmoi.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_HIENTHICHUCNANGNHAPNHANH", "0", false) == "1";
                songaycanhbao = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_KHOANGTHOIGIAN_CANHBAONGAYHETHAN", "10", false), 10);
                lblSTTThau.Enabled = lblQDthau.Enabled = txtsoDK.Enabled = txtsoQDthau_Dmuc.Enabled = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_BATNHAPTHONGTIN_QDTHAU", "0", false) == "1";
                if (!txtsoDK.Enabled)
                    lblSTTThau.ForeColor = lblQDthau.ForeColor = lblThangdu.ForeColor;

                InitStocks();

                txtNhanvien.Init(CommonLoadDuoc.LAYTHONGTIN_NHANVIEN(),
                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtNhanvien.SetId(-1);
                }
                else
                {
                    txtNhanvien.SetId(globalVariables.gv_intIDNhanvien);
                }
                bool gridView = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KEDONTHUOC_SUDUNGLUOI", "0", true), 0) == 1;
                if (!gridView)
                {
                    gridView = PropertyLib._AppProperties.GridView;
                }
                txtDrugName.GridView = gridView;
                LoadAuCompleteThuoc();
                getData();
                SetStatusControl();
                ModifyCommand();
                CauHinh();
                ConfigBHYT();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                if (m_enAction == action.View)
                {
                    cmdAddDetail.Enabled = cmdSave.Enabled = cmdXoaThongTin.Enabled = cmdQuickCreate.Enabled = cmdHuyThongTin.Enabled = cmdThemPhieuNhap.Enabled = false;
                }
            }
        }
        bool _bhytChophepnhapgia = false;
        bool _bhytHienthigia = false;
        bool _bhytChophepnhapgiaphuthu = false;
        bool _bhytLuachonApdung = false;
        bool _bhytGiabhytBangGiaban = false;
        bool _bhytCanhbaoGiamoikhacgiacu = true;
        bool _nhapkhothuocChophepNhapchietkhau = true;
        void ConfigBHYT()
        {
            _bhytCanhbaoGiamoikhacgiacu = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_CANHBAO_GIAMOIKHACGIACU", "0", true), 0) == 1;
            _bhytGiabhytBangGiaban = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_GIABHYT_BANG_GIABAN", "0", true), 0) == 1;
            _bhytChophepnhapgia = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_CHOPHEPNHAPGIA", "0", true), 0) == 1;
             _bhytLuachonApdung = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_LUACHON_APDUNG", "0", true), 0) == 1;
             _bhytChophepnhapgiaphuthu = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_CHOPHEPNHAPGIAPHUTHU", "0", true), 0) == 1;
             _bhytHienthigia = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_HIENTHIGIA", "0", true), 0) == 1;
             _nhapkhothuocChophepNhapchietkhau = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPKHOTHUOC_CHOPHEP_NHAPCHIETKHAU", "0", true), 0) == 1;

             //chkGiaBHYT.Visible = BHYT_LUACHON_APDUNG;
             //chkGiaBHYT.Enabled = BHYT_LUACHON_APDUNG;
            //Tạm REM toàn bộ các mục dưới
             //txtGiaBHYT.Visible = _bhytLuachonApdung;
             //txtGiaBHYT_cu.Visible = _bhytLuachonApdung;
             //txtPhuthuDT.Visible = _bhytLuachonApdung;
             //txtPhuthuTT.Visible = _bhytLuachonApdung;

             //lblPhuthuDt.Visible = _bhytLuachonApdung;
             //lblPhuthuTT.Visible = _bhytLuachonApdung;
             //lblBHYTcu.Visible = _bhytLuachonApdung;


             //if (!_bhytLuachonApdung) return;//Nếu cho lựa chọn áp dụng BHYT thì kiểm tra tiếp
             //txtGiaBHYT.Visible = _bhytHienthigia;
             //txtGiaBHYT_cu.Visible = _bhytHienthigia ;
             //txtPhuthuDT.Visible = _bhytHienthigia ;
             //txtPhuthuTT.Visible = _bhytHienthigia ;

             //lblPhuthuDt.Visible = _bhytHienthigia;
             //lblPhuthuTT.Visible = _bhytHienthigia;
             //lblBHYTcu.Visible = _bhytHienthigia;

             //txtGiaBHYT.Enabled = _bhytChophepnhapgia;// chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA;Từ các dòng sau bỏ chkGiaBHYT.Checked &&
             //txtGiaBHYT.BackColor = _bhytChophepnhapgia ? txtSoluong.BackColor : txtThanhTien.BackColor;

             //txtPhuthuDT.Enabled = _bhytChophepnhapgiaphuthu;
             //txtPhuthuDT.BackColor =  _bhytChophepnhapgiaphuthu ? txtSoluong.BackColor : txtThanhTien.BackColor;

             //txtPhuthuTT.Enabled = _bhytChophepnhapgiaphuthu;
             //txtPhuthuTT.BackColor =  _bhytChophepnhapgiaphuthu ? txtSoluong.BackColor : txtThanhTien.BackColor;


        }
        private void ClearControlPhieu()
        {
            foreach (Control control in grpControl.Controls)
            {
                if(control is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    ((Janus.Windows.GridEX.EditControls.EditBox)(control)).Clear();
                }
            }
            txtNhanvien.SetCode("-1");
            cboNhaCungcap.SelectedValue = -1;
            txtTongtienHD.Clear();
            dtNgayhoadon.Value =globalVariables.SysDate;
            txtSoHoaDon.Focus();
        }
        TPhieuNhapxuatthuoc objPhieuNhap = null;
        private void getData()
        {
            if (m_enAction == action.Update || m_enAction == action.View)
            {
                 objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                if (objPhieuNhap != null)
                {
                    txtSoHoaDon.Text = Utility.sDbnull(objPhieuNhap.SoHoadon);
                    dtNgayhoadon.Value = objPhieuNhap.NgayHoadon;
                    if (objPhieuNhap.NgayLap.HasValue)
                        dtpNgayLapphieu.Value = objPhieuNhap.NgayLap.Value;
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    txtLyDoNhap._Text = Utility.sDbnull(objPhieuNhap.MotaThem);
                    txtNguoiGiao._Text = Utility.sDbnull(objPhieuNhap.NguoiGiao);
                    txtNguoinhan._Text = Utility.sDbnull(objPhieuNhap.NguoiNhan);
                    dtNgayhoadon.Value = Convert.ToDateTime(objPhieuNhap.NgayHoadon);
                    txtTongtienHD.Text = Utility.sDbnull(objPhieuNhap.TongTien);
                    chkTrongthau.Checked = Utility.Byte2Bool(objPhieuNhap.TrongThau);
                    chkTrongthau.Enabled = !chkTrongthau.Checked;
                    cboKhonhap.SelectedIndex = Utility.GetSelectedIndex(cboKhonhap,Utility.sDbnull( objPhieuNhap.IdKhonhap));
                    txtNo.Text = objPhieuNhap.TkNo;
                    txtCo.Text = objPhieuNhap.TkCo;
                    txtVAT.Text = Utility.sDbnull(objPhieuNhap.Vat, 0);
                    txtSoCTkemtheo.Text = objPhieuNhap.SoChungtuKemtheo;
                    chkPhieuvay.Checked = Utility.Byte2Bool(objPhieuNhap.PhieuVay);
                    cboNhaCungcap.SelectedIndex = Utility.GetSelectedIndex(cboNhaCungcap, Utility.sDbnull(objPhieuNhap.MaNhacungcap));
                    txtNhanvien.SetId(objPhieuNhap.IdNhanvien);
                    m_dtDataPhieuChiTiet =
                         new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    Utility.SetDataSourceForDataGridEx(grdChitiet, m_dtDataPhieuChiTiet, false, true, "1=1", "");
                }
            }
            if(m_enAction==action.Insert)
            {
                m_dtDataPhieuChiTiet =
                       new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(-100);
                Utility.SetDataSourceForDataGridEx(grdChitiet, m_dtDataPhieuChiTiet, false, true, "1=1", "");
            }
          
        }
        DataTable dtDmuc_thuoc = new DataTable();
        private void LoadAuCompleteThuoc()
        {
            dtDmuc_thuoc = CommonLoadDuoc.LayThongTinThuoc(KIEU_THUOC_VT);
            txtDrugName.dtData = dtDmuc_thuoc;
            txtDrugName.ChangeDataSource();
        }
        private int id_thuockho = -1;
        private bool _allowDrugChanged;
        private void txtdrug__OnGridSelectionChanged(string ID, int id_thuockho, string _name, string Dongia,
         string phuthu, int tutuc)
        {
            //this.id_thuockho = id_thuockho;
            //_allowDrugChanged = false;
            //txtDrug_ID.Text = ID;
            //txtGiaban.Text = Dongia;
            //txtPhuthuDT.Text = phuthu;
        }
        private void txtdrug__OnChangedView(bool gridview)
        {
            PropertyLib._AppProperties.GridView = gridview;
            PropertyLib.SaveProperty(PropertyLib._AppProperties);
        }
        private void frm_Themmoi_Phieunhapkho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.Enter)
            {
                Control ctr = Utility.getActiveControl(this);
                if (ctr.GetType().Equals(cboKhonhap.GetType())) return;
                else
                    SendKeys.Send("{TAB}");
            }
            if ((e.Control && e.KeyCode==Keys.P) || e.KeyCode == Keys.F4 ) cmdInPhieuNhap.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtDrugName.Focus();
                txtDrugName.SelectAll();
            }
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.A && e.Control)cmdAddDetail.PerformClick();
            if(e.KeyCode==Keys.S && e.Control)cmdSave.PerformClick();
            if (e.KeyCode == Keys.F1 && e.Control) ConfigBHYT();
        }
        DmucThuoc objThuoc = null;
        long id_thau = -1;
        long id_thau_ct = -1;
        private void txtDrug_ID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Utility.Int32Dbnull(txtDrug_ID.Text,-1)>0)
                {
                   objThuoc= DmucThuoc.FetchByID(Utility.Int32Dbnull(txtDrug_ID.Text));
                   if (objThuoc != null)
                   {
                        dtNgayHetHan.ResetText();
                       chkApthau.Checked = false;
                       txtIdQD.Clear();
                       id_thau = -1;
                       id_thau_ct = -1;
                       DataRow[] arrThuoc = txtDrugName.dtData.Select(string.Format("id_thuoc={0}", objThuoc.IdThuoc));
                       if (arrThuoc.Length > 0 && chkTrongthau.Checked)//100%
                       {
                           chkApthau.Checked = Utility.Byte2Bool(arrThuoc[0]["la_apthau"]);
                           txtIdQD.Text = Utility.sDbnull(arrThuoc[0]["id_qdinh"], "0");
                           id_thau = Utility.Int64Dbnull(arrThuoc[0]["id_thau"], "-1");
                           id_thau_ct = Utility.Int64Dbnull(arrThuoc[0]["id_thau_ct"], "-1");
                           lblKhachuyen.Text = Utility.sDbnull(arrThuoc[0]["so_luong"], "0");
                           txtsoDK.Text = Utility.sDbnull(arrThuoc[0]["so_dangky"], "0");
                           txtsoQDthau_Dmuc.Text = Utility.sDbnull(arrThuoc[0]["maso_qdinh"], "0");
                           txtTCCL.Text = Utility.sDbnull(arrThuoc[0]["tccl"], "0");
                           txtGiaDV.Text = Utility.sDbnull(arrThuoc[0]["gia_bhyt_thau"], "0");
                           txtDongia.Text = Utility.sDbnull(arrThuoc[0]["gia_nhap_thau"], "0");
                           txtGiaBHYT.Text = Utility.sDbnull(arrThuoc[0]["gia_bhyt_thau"], "0");
                           txtSLthau.Text = Utility.sDbnull(arrThuoc[0]["sl_thau"], "0");
                           txtSlnhapkho.Text = Utility.sDbnull(arrThuoc[0]["sl_nhap"], "0");
                           txtsldieutiet.Text = Utility.sDbnull(arrThuoc[0]["sl_dieutiet"], "0");
                       }
                       else
                       {
                           txtGiaDV.Text = Utility.sDbnull(objThuoc.GiaDv, "0");
                           txtDongia.Text = Utility.sDbnull(objThuoc.DonGia, "0");
                           txtsoDK.Text = objThuoc.SoDangky;
                            txtsoQDthau_Dmuc.Text = Utility.sDbnull(objThuoc.QD31, "");// string.Format("{0};{1};{2}", Utility.sDbnull(objThuoc.QD31, ""), Utility.sDbnull(objThuoc.LoaiThau), Utility.sDbnull(objThuoc.NhomThau));
                           QheDoituongThuoc _objQhe = new Select().From(QheDoituongThuoc.Schema).Where(QheDoituongThuoc.Columns.IdThuoc).IsEqualTo(objThuoc.IdThuoc)
                                                                            .And(QheDoituongThuoc.Columns.IdLoaidoituongKcb).IsEqualTo(0).ExecuteSingle<QheDoituongThuoc>();
                           if (_objQhe != null)
                           {
                               // chkGiaBHYT.Checked = true;
                               if (!_bhytGiabhytBangGiaban) txtGiaBHYT.Text = _objQhe.DonGia.ToString();
                               txtGiaBHYT_cu.Text = _objQhe.DonGia.ToString();
                               txtPhuthuDT.Text = Utility.DecimaltoDbnull(_objQhe.PhuthuDungtuyen, 0).ToString();
                               txtPhuthuTT.Text = Utility.DecimaltoDbnull(_objQhe.PhuthuTraituyen, 0).ToString();
                           }
                           else
                           {
                               DataRow[] arrDr = m_dtDataPhieuChiTiet.Select(TPhieuNhapxuatthuocChitiet.Columns.IdThuoc + "=" + txtDrug_ID.Text);
                               if (arrDr.Length > 0)
                               {
                                   //chkGiaBHYT.Checked = Utility.Byte2Bool(Utility.ByteDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.CoBhyt]));
                                   txtGiaBHYT.Text = Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt], 0).ToString();
                                   txtGiaBHYT_cu.Text = Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.GiaBhytCu], 0).ToString();

                                   txtPhuthuDT.Text = Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen], 0).ToString();
                                   txtPhuthuTT.Text = Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen], 0).ToString();

                               }
                               else
                               {
                                   txtGiaBHYT_cu.Text = objThuoc.GiaBhyt.ToString();
                                   txtPhuthuDT.Text = objThuoc.PhuthuDungtuyen.ToString();
                                   txtPhuthuTT.Text = objThuoc.PhuthuTraituyen.ToString();
                               }
                           }
                       }
                       txtmathuoc.Text = objThuoc.MaThuoc;
                       DmucChung objMeasureUnit = THU_VIEN_CHUNG.LaydoituongDmucChung("DONVITINH", Utility.sDbnull(objThuoc.MaDonvitinh));
                       if (objMeasureUnit != null)
                       {
                           txtDonViTinh.Text = Utility.sDbnull(objMeasureUnit.Ten);
                           txtMaDonvitinh.Text = Utility.sDbnull(objMeasureUnit.Ma);

                       }
                      
                       
                     

                       //Bỏ chkGiaBHYT.Checked &&
                       //txtGiaBHYT.BackColor = _bhytChophepnhapgia ? txtSoluong.BackColor : txtThanhTien.BackColor;

                       //txtPhuthuDT.BackColor = _bhytChophepnhapgiaphuthu ? txtSoluong.BackColor : txtThanhTien.BackColor;

                       //txtPhuthuTT.BackColor = _bhytChophepnhapgiaphuthu ? txtSoluong.BackColor : txtThanhTien.BackColor;

                       //txtGiaBHYT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;
                       //txtPhuthuDT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;
                       //txtPhuthuTT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;
                       //txtGiaBHYT.Enabled = chkGiaBHYT.Checked;
                   }
                   else
                   {
                       txtDrug_ID.Clear();
                       txtmathuoc.Clear();
                       txtTCCL.Clear();
                       txtDongia.Clear();
                       txtGiaBHYT.Clear();
                       txtPhuthuDT.Clear();
                       txtPhuthuTT.Clear();
                       txtsoDK.Clear();
                       txtsoQDthau_Dmuc.Clear();
                   }

                }
                else
                {
                    txtDrug_ID.Clear();
                    txtmathuoc.Clear();
                    txtTCCL.Clear();
                    txtsoDK.Clear();
                    txtsoQDthau_Dmuc.Clear();
                    txtDongia.Clear();
                    txtGiaBHYT.Clear();
                    txtPhuthuDT.Clear();
                    txtPhuthuTT.Clear();
                    txtDonViTinh.Clear();
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới phiếu thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemPhieuNhap_Click(object sender, EventArgs e)
        {
            m_enAction = action.Insert;
            ClearControlPhieu();
            ClearControl();
            txtSoHoaDon.Focus();
            m_dtDataPhieuChiTiet.Clear();
            m_dtDataPhieuChiTiet.AcceptChanges();
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            if (!IsValidChiTiet()) return;
            if (isUpdate)
            {
                isUpdate = false;
                Update();
            }
            else
                ThemchitietNhapkho();
            ClearControl();
            TinhSumThanhTien();
            ModifyCommand();
        }
        private bool IsValidChiTiet()
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
            if (m_enAction == action.Update)
            {
                if (objPhieuNhap == null)
                {
                    Utility.ShowMsg("Phiếu đã được người khác xóa trong lúc bạn đang mở xem nên bạn không được phép thao tác. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                    return false;
                }
                else
                {

                    DataTable dtPaymentData = SPs.TPhieunhapxuatKiemtratrangthai(objPhieuNhap.IdPhieu).GetDataSet().Tables[0];
                    if (dtPaymentData.Rows.Count > 0)
                    {
                        Utility.ShowMsg("Phiếu đã được người khác xác nhận trong lúc bạn đang mở xem nên bạn không được phép sửa/xóa phiếu. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                        return false;
                    }
                }
            }

            SqlQuery sqlQuery = new Select().From(DmucThuoc.Schema)
                .Where(DmucThuoc.Columns.IdThuoc).IsEqualTo(Utility.Int32Dbnull(txtDrug_ID.Text));
            if (sqlQuery.GetRecordCount() <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn cần chọn thuốc để nhập", true);
                txtDrugName.Focus();
                return false;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_BATNHAPTHONGTIN_QDTHAU", "0", false) == "1")
            {
                if (Utility.DoTrim(txtsoQDthau_Dmuc.Text) == "")
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập thông tin số QĐ thầu\nCó thể chỉnh giá trị tham số hệ thống THUOC_NHAPKHO_BATNHAPTHONGTIN_QDTHAU", true);
                    txtsoQDthau_Dmuc.Focus();
                    return false;
                }
                if (Utility.DoTrim(txtsoDK.Text) == "")
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập thông tin số thứ tự thầu", true);
                    txtsoDK.Focus();
                    return false;
                }
            }
            if(dtNgayHetHan.Text=="")
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập ngày hết hạn của thuốc", true);
                dtNgayHetHan.Focus();
                return false;
            }    
            if (Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Day, globalVariables.SysDate, dtNgayHetHan.Value) <= songaycanhbao)
            {
                if (Utility.AcceptQuestion("Ngày hết hạn của thuốc so với ngày hiện tại rất gần. Bạn có muốn sửa lại ngày hết hạn.\n Có thể điều chỉnh tham số THUOC_NHAPKHO_KHOANGTHOIGIAN_CANHBAONGAYHETHAN", "Cảnh báo ngày hết hạn", true))
                {
                    dtNgayHetHan.Focus();
                    return false;
                }
            }
            //if (Utility.DoTrim(txtSoLo.Text) == "")
            //{
            //    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập thông tin số lô", true);
            //    txtSoLo.Focus();
            //    return false;
            //}
            if (Utility.DecimaltoDbnull(txtSoluong.Text) <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Số lượng >0", true);
                txtSoluong.Focus();
                return false;
            }
            //Kiểm tra liên quan đến thuốc thầu
            if (chkTrongthau.Enabled && id_thau > 0 && id_thau_ct > 0)
            {
                DataTable dtData = SPs.ThuocThauLaythongtinSoluong(id_thau_ct).GetDataSet().Tables[0];
                int slkhachuyen = Utility.Int32Dbnull(dtData.Rows[0]["sl_khachuyen"]);
                //Lấy số lượng chờ trong chính phiếu đang có
                int slcho_trongphieu = Utility.Int32Dbnull(m_dtDataPhieuChiTiet.Compute("sum(so_luong)", TThauDieutietCt.Columns.IdThauCt + "=" + id_thau_ct.ToString()), 0);
                slkhachuyen -= slcho_trongphieu;
                if (slkhachuyen < Utility.DecimaltoDbnull(txtSoluong.Text, 0))
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], string.Format("Số lượng nhập kho {0} phải <= Số lượng khả nhập {1}", txtSoluong.Text, slkhachuyen.ToString()), true);
                    txtSoluong.SelectAll();
                    txtSoluong.Focus();
                    return false;
                }
            }
            if (txtDongia.Text.Length == 0) txtDongia.Text = "0";
            if (txtGiaDV.Text.Length == 0) txtGiaDV.Text = "0";
            if (Utility.DecimaltoDbnull(txtGiaDV.Text, 0) <= 0)
            {
                if (!Utility.AcceptQuestion("Cảnh báo giá bán đang <=0. Nhấn Yes để tiếp tục thực hiện, nhấn No để quay trở lại nhập giá bán", "Cảnh báo giá đơn thuốc<=0", true))
                {
                    txtGiaDV.Focus();
                    return false;
                }
            }
            if (Utility.DecimaltoDbnull(txtDongia.Text, 0) <= 0)
            {
                if (!Utility.AcceptQuestion("Đơn giá bạn đang để <=0. Bạn có muốn hủy thao tác để nhập lại Đơn giá hay không(nhấn YES), hoặc tiếp tục chấp nhận Đơn giá này(Nhấn NO)?", "Xác nhận", true))
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Đơn giá phải >=0", true);
                    txtDongia.Focus();
                    return false;
                }
            }
            if (Utility.DecimaltoDbnull(txtGiaDV.Text,0) <=0)
            {
                if (!Utility.AcceptQuestion("Giá bán bạn đang để <=0. Bạn có muốn hủy thao tác để nhập lại giá bán hay không(nhấn YES), hoặc tiếp tục chấp nhận giá bán này(Nhấn NO)?", "Xác nhận", true))
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Giá bán phải >=0", true);
                    txtGiaDV.Focus();
                    return false;
                }
            }
            if (_bhytLuachonApdung && _bhytHienthigia && THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_CANHBAO_KHACGIA", "1", false) == "1")
                if (Utility.DecimaltoDbnull(txtGiaBHYT_cu.Text,-1) <=0 && Utility.DecimaltoDbnull(txtGiaBHYT.Text) != Utility.DecimaltoDbnull(txtGiaBHYT_cu.Text))
                {
                    if (!Utility.AcceptQuestion("Giá BHYT cũ khác giá BHYT mới. Bạn có chắc chắn điều chỉnh giá BHYT thành giá mới hay không?\nCó thể chỉnh tham số hệ thống THUOC_NHAPKHO_CANHBAO_KHACGIA", "Cảnh báo", true))
                    {
                        txtGiaBHYT.Focus();
                        return false;
                    }
                }
            //if ( _bhytChophepnhapgia)
                if (Utility.DecimaltoDbnull(txtPhuthuDT.Text) > Utility.DecimaltoDbnull(txtGiaBHYT.Text))
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Giá phụ thu đúng tuyến phải < giá BHYT", true);
                    txtPhuthuDT.Focus();
                    return false;
                }
            //if ( _bhytChophepnhapgia)
                if (Utility.DecimaltoDbnull(txtPhuthuTT.Text) > Utility.DecimaltoDbnull(txtGiaBHYT.Text))
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Giá phụ thu trái tuyến phải < giá BHYT", true);
                    txtPhuthuTT.Focus();
                    return false;
                }
            string THUOC_CANHBAO_NHAPVUOTTRAN_BHYT=THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_CANHBAO_NHAPVUOTTRAN_BHYT","0",false);
            if (THUOC_CANHBAO_NHAPVUOTTRAN_BHYT != "0" && objThuoc!=null)
            {
                int sluongvuottran = Utility.Int32Dbnull(objThuoc.SluongVuottran, 0);
                if (sluongvuottran > 0)
                {
                    int tongnhap = THUOC_NHAPKHO.ThuocTongnhapngoaiTrongNam(dtNgayhoadon.Value.Year, Utility.Int32Dbnull(txtDrug_ID.Text, 0));
                    if (tongnhap + Utility.DecimaltoDbnull(txtSoluong.Text, 0) > sluongvuottran)
                    {
                        string msg = string.Format("Thuốc {0} được cấu hình nhập mỗi năm không quá {1} {2}. Tổng số lượng đã nhập: {3} + Số lượng nhập lần này: {4} đang vượt quá số lượng vượt trần. Đề nghị bạn kiểm tra lại\nCó thể chỉnh tham số hệ thống THUOC_CANHBAO_NHAPVUOTTRAN_BHYT", txtDrugName.Text, sluongvuottran.ToString(), txtDonViTinh.Text, tongnhap.ToString(), txtSoluong.Text);
                        Utility.ShowMsg(msg);
                        if (THUOC_CANHBAO_NHAPVUOTTRAN_BHYT == "2")
                        {
                            txtSoluong.SelectAll();
                            txtSoluong.Focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private void Update()
        {
            grdChitiet.CurrentRow.BeginEdit();
            grdChitiet.CurrentRow.Cells["tccl"].Value = Utility.sDbnull(txtTCCL.Text);
            grdChitiet.CurrentRow.Cells["so_dky"].Value = Utility.sDbnull(txtsoDK.Text);
            grdChitiet.CurrentRow.Cells["ngay_hethan"].Value = dtNgayHetHan.Value;
            grdChitiet.CurrentRow.Cells["so_lo"].Value = Utility.sDbnull(txtSoLo.Text);
            grdChitiet.CurrentRow.Cells["so_luong"].Value = Utility.DecimaltoDbnull(txtSoluong.Text);
            grdChitiet.CurrentRow.Cells["gia_nhap"].Value = Utility.DecimaltoDbnull(txtDongia.Text);
            grdChitiet.CurrentRow.Cells["gia_ban"].Value = Utility.DecimaltoDbnull(txtGiaDV.Text);
            grdChitiet.CurrentRow.Cells["gia_bhyt"].Value = Utility.DecimaltoDbnull(txtGiaBHYT.Text);
            grdChitiet.CurrentRow.Cells["gia_phuthu_dungtuyen"].Value = Utility.DecimaltoDbnull(txtPhuthuDT.Text);
            grdChitiet.CurrentRow.Cells["gia_phuthu_traituyen"].Value = Utility.DecimaltoDbnull(txtPhuthuTT.Text);
            grdChitiet.CurrentRow.Cells["chiet_khau"].Value = Utility.DecimaltoDbnull(txtChietkhau.Text);
            grdChitiet.CurrentRow.Cells["thanh_tien"].Value = Utility.DecimaltoDbnull(txtThanhTien.Text);
            grdChitiet.CurrentRow.EndEdit();
            grdChitiet.UpdateData();
            grdChitiet.Refresh();
            ModifyCommand();

        }
        private void ThemchitietNhapkho()
        {
            try
            {
                //Kiểm tra không cho phép nhập trong thầu và ngoài thầu trong cùng 1 phiếu
                var q = from p in m_dtDataPhieuChiTiet.AsEnumerable()
                        where Utility.Int64Dbnull(p["id_qdinh"], 0) <=0
                        select p;
                if (Utility.Int64Dbnull(txtIdQD.Text, 0) > 0 && q.Any())
                {
                    Utility.ShowMsg("Không cho phép 1 phiếu vừa nhập thuốc trong thầu lẫn ngoài thầu. Vui lòng kiểm tra lại");
                    return;
                }

                DataRow[] arrDr = m_dtDataPhieuChiTiet.Select("0=1");

                var _data = from p in m_dtDataPhieuChiTiet.AsEnumerable()
                            where 1 == 2
                            select p;
                 if (chkTrongthau.Checked)
                     _data = from p in m_dtDataPhieuChiTiet.AsEnumerable()
                             where Utility.Int64Dbnull(p[TPhieuNhapxuatthuocChitiet.Columns.IdQdinh]) == Utility.Int64Dbnull(txtIdQD.Text, 0)
                             select p;
                 else
                     _data = from p in m_dtDataPhieuChiTiet.AsEnumerable()
                             where Utility.Int32Dbnull(p[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc], -1) == Utility.Int32Dbnull(txtDrug_ID.Text, -1)
                             && Utility.DecimaltoDbnull(p[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap]) == Utility.DecimaltoDbnull(txtDongia.Text, 0)
                             && Utility.sDbnull(p[TPhieuNhapxuatthuocChitiet.Columns.SoLo]) == Utility.DoTrim(txtSoLo.Text)
                             && Utility.Int32Dbnull(p[TPhieuNhapxuatthuocChitiet.Columns.Vat]) == Utility.Int32Dbnull(txtVAT.Text, 0)
                             && Utility.sDbnull(p[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan]) == dtNgayHetHan.Text
                             && Utility.Int32Dbnull(p[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt]) == Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0)
                             && Utility.Int32Dbnull(p[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen]) == Utility.DecimaltoDbnull(txtPhuthuDT.Text, 0)
                             && Utility.Int32Dbnull(p[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen]) == Utility.DecimaltoDbnull(txtPhuthuTT.Text, 0)
                             && Utility.sDbnull(p[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau]) == Utility.sDbnull(txtsoQDthau_Dmuc.Text, "")
                              && Utility.sDbnull(p[TPhieuNhapxuatthuocChitiet.Columns.SoDky]) == Utility.sDbnull(txtsoDK.Text, "")
                               && Utility.Int32Dbnull(p[TPhieuNhapxuatthuocChitiet.Columns.GiaBan]) == Utility.DecimaltoDbnull(txtGiaDV.Text, 0)
                             select p;
                 if (_data.Any())
                 {
                     arrDr = _data.ToArray<DataRow>();
                 }
                //DataRow[] arrDr = m_dtDataPhieuChiTiet
                //    .Select
                //    (
                //    TPhieuNhapxuatthuocChitiet.Columns.IdThuoc + "=" + txtDrug_ID.Text + " AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.GiaNhap + "=" + Utility.DecimaltoDbnull(txtDongia.Text,0) + " AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.SoLo + "='" + Utility.DoTrim(txtSoLo.Text) + "' AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.Vat + "=" + Utility.Int32Dbnull(txtVAT.Text, 0) + " AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.NgayHethan + "='" + dtNgayHetHan.Text + "' AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt + "='" + Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0) + "' AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen + "=" +Utility.DecimaltoDbnull(txtPhuthuDT.Text, 0) + " AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen + "=" +Utility.DecimaltoDbnull(txtPhuthuTT.Text, 0) + " AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau + "='" + Utility.sDbnull(txtsoQDthau.Text, "") + "' AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.SoDky + "='" + Utility.sDbnull(txtsoDK.Text, "") + "' AND "
                //    + TPhieuNhapxuatthuocChitiet.Columns.GiaBan + "=" + Utility.DecimaltoDbnull(txtGiaban.Text, 0)
                //    );
                if (arrDr.Length > 0)
                {
                    int newquantity =(int)( Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.SoLuong], 0) + Utility.DecimaltoDbnull(txtSoluong.Text));
                    arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = newquantity;
                    m_dtDataPhieuChiTiet.AcceptChanges();
                }
                else
                {
                    DataRow drv = m_dtDataPhieuChiTiet.NewRow();
                    drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc] = txtDrug_ID.Text;
                    drv["ten_donvitinh"] = Utility.sDbnull(txtDonViTinh.Text);
                    DmucThuoc objLDrug = DmucThuoc.FetchByID(Utility.Int32Dbnull(txtDrug_ID.Text));
                    if (objLDrug != null)
                    {
                        drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objLDrug.TenThuoc);

                        drv[DmucThuoc.Columns.MaThuoc] = Utility.sDbnull(objLDrug.MaThuoc);
                        drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(objLDrug.HamLuong);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPhuthuDT.Text);
                        drv[DmucThuoc.Columns.KieuThuocvattu] = objLDrug.KieuThuocvattu;

                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt] = Utility.DecimaltoDbnull(txtGiaBHYT.Text);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.CoBhyt] = _bhytLuachonApdung;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdQdinh] = Utility.Int64Dbnull(txtIdQD.Text,"0");
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThau] = id_thau;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThauCt] = id_thau_ct;

                        drv[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap] = Utility.sDbnull( cboNhaCungcap.SelectedValue);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLo] = Utility.DoTrim(txtSoLo.Text);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau] = Utility.DoTrim(txtsoQDthau_Dmuc.Text);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoDky] = Utility.DoTrim(txtsoDK.Text);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = Utility.DecimaltoDbnull(txtSoluong.Text);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = Utility.DecimaltoDbnull(txtThanhTien.Text);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau] = Utility.DecimaltoDbnull(Utility.DecimaltoDbnull(txtChietkhau.Text));
                        drv[TPhieuNhapxuatthuocChitiet.Columns.ThangDu] = Utility.Int32Dbnull(nmrThangDu.Value, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = Utility.DecimaltoDbnull(txtVAT.Text, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBan] = Utility.DecimaltoDbnull(txtGiaDV.Text, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = dtNgayHetHan.Text;
                        drv["NGAY_HET_HAN"] = dtNgayHetHan.Value.Date;
                        m_dtDataPhieuChiTiet.Rows.Add(drv);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ex.Message);
            }
        }
        private void SetStatusControl()
        {
             
             
        }
        /// <summary>
        /// hàm thực hiện việc Invalinhap khoa thuốc
        /// </summary>
        /// <returns></returns>
        private bool IsValidNhapKho()
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
            if (m_enAction == action.Update )
            {
                if (objPhieuNhap == null)
                {
                    Utility.ShowMsg("Phiếu đã được người khác xóa trong lúc bạn đang mở xem nên bạn không được phép thao tác. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                    return false;
                }
                else
                {

                    DataTable dtPaymentData = SPs.TPhieunhapxuatKiemtratrangthai(objPhieuNhap.IdPhieu).GetDataSet().Tables[0];
                    if (dtPaymentData.Rows.Count > 0)
                    {
                        Utility.ShowMsg("Phiếu đã được người khác xác nhận trong lúc bạn đang mở xem nên bạn không được phép sửa/xóa phiếu. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                        return false;
                    }
                }
            }
            //if (string.IsNullOrEmpty(txtSoHoaDon.Text))
            //{
            //    if (!_Autosave)
            //        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập số hóa đơn", true);
            //    txtSoHoaDon.Focus();
            //    return false;

            //}
            //if(dtNgayhoadon.Value.Date<dtpNgayLapphieu.Value.Date)
            if (Utility.sDbnull(txtSoHoaDon.Text) == "")
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập số hóa đơn", true);
                txtSoHoaDon.Focus();
                return false;
            }
            if (Utility.sDbnull( cboNhaCungcap.SelectedValue) == "-1")
            {
                if (!_Autosave)
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải chọn nhà cung cấp", true);
                cboNhaCungcap.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cboKhonhap.SelectedValue,-1)<=0)
            {
                if (!_Autosave)
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải chọn kho để nhập thuốc", true);
                cboKhonhap.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtTongtienHD.Text) != Utility.DecimaltoDbnull(txtTongTien.Text) && THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_KIEMTRATONGTIEN", "0", false) == "1")
            {
                if (!Utility.AcceptQuestion(string.Format("Tổng tiền hóa đơn {0} khác với tổng tiền nhập {1}. Bạn có muốn tiếp tục lưu", txtTongtienHD.Text, txtTongTien.Text),"Xác nhận",true))
                {
                    txtTongTien.Focus();
                    txtTongTien.SelectAll();
                    if (!_Autosave)
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Tổng tiền hóa đơn phải bằng tổng tiền nhập", true);
                    return false;
                }
            }
            if (objPhieuNhap != null && Utility.Byte2Bool(objPhieuNhap.TrongThau) && Utility.sDbnull(cboNhaCungcap.SelectedValue) != objPhieuNhap.MaNhacungcap)
            {
                Utility.ShowMsg("Không cho phép đổi nhà cung cấp sau khi đã nhập chi tiết thuốc thầu");
                cboNhaCungcap.SelectedValue = objPhieuNhap.MaNhacungcap;
                return false;
            }
            //Kiểm tra các dòng thuốc thầu
            foreach (DataRow dr in m_dtDataPhieuChiTiet.Rows)
            {
                if (Utility.Int64Dbnull(dr["id_qdinh"], 0) > 0)
                {
                    id_thau_ct = Utility.Int64Dbnull(dr["id_thau_ct"], 0);
                    DataTable dtData = SPs.ThuocThauLaythongtinSoluong(id_thau_ct).GetDataSet().Tables[0];
                    int slkhachuyen = Utility.Int32Dbnull(dtData.Rows[0]["sl_khachuyen"]);
                    //Lấy số lượng chờ trong chính phiếu đang có
                    int slcho_trongphieu = Utility.Int32Dbnull(m_dtDataPhieuChiTiet.Compute("sum(so_luong)", TThauDieutietCt.Columns.IdThauCt + "=" + id_thau_ct.ToString()), 0);
                    slkhachuyen += slcho_trongphieu;
                    if (slkhachuyen < Utility.DecimaltoDbnull(txtSoluong.Text, 0))
                    {
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], string.Format("Số lượng nhập kho của thuốc {0} phải <= Số lượng khả nhập {1}",Utility.sDbnull( dr["ten_thuoc"]), slkhachuyen.ToString()), true);
                        txtSoluong.SelectAll();
                        txtSoluong.Focus();
                        return false;
                    }
                }
            }
            return true;
        }
        private bool IsValidNhapKho_Auto()
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            if (string.IsNullOrEmpty(txtSoHoaDon.Text))
            {
               
                return false;

            }
            if (Utility.sDbnull(cboNhaCungcap.SelectedValue) == "-1")
            {
              
                return false;
            }
            if (Utility.Int32Dbnull(cboKhonhap.SelectedValue, -1) <= 0)
            {
              
                return false;
            }
            if (Utility.DecimaltoDbnull(txtTongtienHD.Text) != Utility.DecimaltoDbnull(txtTongTien.Text) && THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_KIEMTRATONGTIEN", "0", false) == "1")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện việc nhạp số hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSoHoaDon_TextChanged(object sender, EventArgs e)
        {
            SetStatusControl();
        }

        private void cboNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStatusControl();
        }

        private void cboKhoNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStatusControl();
        }

        private void cmdHuyThongTin_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        /// <summary>
        /// hàm thực hiện việc làm sạch thông tin 
        /// </summary>
        private void ClearControl()
        {
            txtDrugName.Clear();
            txtDrugName.Focus();
            //txtSoLo.Clear();
            txtDrug_ID.Clear();
            txtmathuoc.Clear();
            txtTCCL.Clear();
            txtSoluong.Clear();
            txtDongia.Clear();
            txtGiaDV.Clear();
            txtThanhTien.Clear();
            nmrThangDu.Value = 0;
            txtChietkhau.Clear();
            
        }
        /// <summary>
        /// hàm thực hiện việc cho phép 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaThongTin_Click(object sender, EventArgs e)
        {
            TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
            if (m_enAction == action.Update)
            {
                if (objPhieuNhap == null)
                {
                    Utility.ShowMsg("Phiếu đã được người khác xóa trong lúc bạn đang mở xem nên bạn không được phép thao tác. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                    return ;
                }
                else
                {

                    DataTable dtPaymentData = SPs.TPhieunhapxuatKiemtratrangthai(objPhieuNhap.IdPhieu).GetDataSet().Tables[0];
                    if (dtPaymentData.Rows.Count > 0)
                    {
                        Utility.ShowMsg("Phiếu đã được người khác xác nhận trong lúc bạn đang mở xem nên bạn không được phép sửa/xóa phiếu. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                        return ;
                    }
                }
            }
            bool hasErr = false;
            using (var scope = new TransactionScope())
            {
                using (var dbscope = new SharedDbConnectionScope())
                {
                    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdChitiet.GetCheckedRows())
                    {
                        int ID = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet].Value);
                        TPhieuNhapxuatthuocChitiet objcheck = TPhieuNhapxuatthuocChitiet.FetchByID(ID);
                        if (objcheck != null && objcheck.IdThuockho > 0)
                        {
                            hasErr = true;
                           
                            break;
                        }
                        TPhieuNhapxuatthuocChitiet.Delete(ID);
                        gridExRow.Delete();
                        grdChitiet.UpdateData();
                        m_dtDataPhieuChiTiet.AcceptChanges();
                    }
                }
                scope.Complete();
            }
            if (hasErr)
                Utility.ShowMsg("Phiếu đã được người khác xác nhận trong lúc bạn đang mở xem nên bạn không được phép sửa/xóa phiếu. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
            ModifyCommand();
            TinhSumThanhTien();
        }
        private void ModifyCommand()
        {
            cmdSave.Enabled = grdChitiet.RowCount > 0;
            cmdInPhieuNhap.Enabled = grdChitiet.RowCount > 0;
            cmdXoaThongTin.Enabled = grdChitiet.RowCount > 0;
            TinhSumThanhTien();
            
            //if(grdPhieuNhapChiTiet.RowCount>0)
            //{
            //    txtVAT.Enabled = false;
            //}
        }
        private void TinhSumThanhTien()
        {
            var query = from thuoc in grdChitiet.GetDataRows()
                        let y = Utility.DecimaltoDbnull(thuoc.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value)
                        select y;
            txtTongTien.Text = Utility.sDbnull(query.Sum());
        }
        private void ThanhTien()
        {
            decimal thanhtien = Utility.DecimaltoDbnull(txtDongia.Text,0)*Utility.DecimaltoDbnull(txtSoluong.Text);
            thanhtien = thanhtien + thanhtien * Utility.DecimaltoDbnull(txtVAT.Text) / 100;
               decimal ChietKhau= Utility.DecimaltoDbnull(txtChietkhau.Text);
            if (thanhtien < ChietKhau)
            {
                txtChietkhau.Text = thanhtien.ToString();
            }
            else
                thanhtien=thanhtien-ChietKhau;
            txtThanhTien.Text = Utility.sDbnull(thanhtien);
            TinhSumThanhTien();
        }
        private void txtDongia_Click(object sender, EventArgs e)
        {
            ThanhTien();
        }

        

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            ThanhTien();
            
        }
        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin của phiếu nhập kho thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            
            if(!IsValidNhapKho())return;
            cmdSave.Enabled = false;
            PerformAction();
            cmdSave.Enabled = true;
        }
        private void PerformAction()
        {
            switch (m_enAction)
            {
                case action.Insert:
                    ThemPhieuNhapKho();
                    break;
                case action.Update:
                    UpdatePhieuNhapKho();
                    break;
            }
        }
        private void AutoSave()
        {
            Utility.WaitNow(this);
            switch (m_enAction)
            {
                case action.Insert:
                    AutoInsert();
                    break;
                case action.Update:
                    AutoUpdate();
                    break;
            }
            Utility.DefaultNow(this);
        }
        #region "khai báo các đối tượng để thực hiện việc "
        private TPhieuNhapxuatthuoc TaophieuNhapkho()
        {
            TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc=new TPhieuNhapxuatthuoc();
            if(m_enAction==action.Update)
            {
                objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text,-1));
                
            }
            if (objTPhieuNhapxuatthuoc != null)
            {
                if (objTPhieuNhapxuatthuoc.IdPhieu > 0)
                {
                    objTPhieuNhapxuatthuoc.MarkOld();
                    objTPhieuNhapxuatthuoc.IsNew = false;
                }
                objTPhieuNhapxuatthuoc.SoHoadon = Utility.sDbnull(txtSoHoaDon.Text);
                objTPhieuNhapxuatthuoc.IdKhonhap = Utility.Int16Dbnull(cboKhonhap.SelectedValue, -1);
                objTPhieuNhapxuatthuoc.MaNhacungcap =Utility.sDbnull( cboNhaCungcap.SelectedValue);
                objTPhieuNhapxuatthuoc.MotaThem = Utility.DoTrim(txtLyDoNhap.Text);
                objTPhieuNhapxuatthuoc.TrangThai = 0;
                objTPhieuNhapxuatthuoc.PhieuVay = Utility.Bool2byte(chkPhieuvay.Checked);
                objTPhieuNhapxuatthuoc.IdNhanvien = Utility.Int16Dbnull(txtNhanvien.MyID, -1);
                    objTPhieuNhapxuatthuoc.IdNhanvien = Utility.Int16Dbnull(txtNhanvien.MyID,globalVariables.gv_intIDNhanvien);
                objTPhieuNhapxuatthuoc.NgayHoadon = dtNgayhoadon.Value;
                objTPhieuNhapxuatthuoc.NgayLap = dtpNgayLapphieu.Value;
                objTPhieuNhapxuatthuoc.NgayTao = globalVariables.SysDate;
                objTPhieuNhapxuatthuoc.NguoiTao = globalVariables.UserName;
                objTPhieuNhapxuatthuoc.NguoiGiao = Utility.DoTrim(txtNguoiGiao.Text);
                objTPhieuNhapxuatthuoc.NguoiNhan = Utility.DoTrim(txtNguoinhan.Text);
                objTPhieuNhapxuatthuoc.TkNo = Utility.DoTrim(txtNo.Text);
                objTPhieuNhapxuatthuoc.TkCo = Utility.DoTrim(txtCo.Text);
                objTPhieuNhapxuatthuoc.SoChungtuKemtheo = Utility.DoTrim(txtSoCTkemtheo.Text);
                objTPhieuNhapxuatthuoc.Vat = Utility.DecimaltoDbnull(txtVAT.Text);
                objTPhieuNhapxuatthuoc.LoaiPhieu = (byte)LoaiPhieu.PhieuNhapKho;
                objTPhieuNhapxuatthuoc.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapKho);
                objTPhieuNhapxuatthuoc.KieuThuocvattu = KIEU_THUOC_VT;
                objTPhieuNhapxuatthuoc.TrongThau =Utility.Bool2byte( chkTrongthau.Checked);
                objTPhieuNhapxuatthuoc.TongTien = Utility.DecimaltoDbnull(txtTongtienHD.Text, 0);
            }
            return objTPhieuNhapxuatthuoc;
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin chi tiết
        /// </summary>
        /// <returns></returns>
        private List< TPhieuNhapxuatthuocChitiet> TaoChitietNhapkho()
        {
            List<TPhieuNhapxuatthuocChitiet> lstDetails = new List<TPhieuNhapxuatthuocChitiet>();
            try
            {


                foreach (GridEXRow gridExRow in grdChitiet.GetDataRows())
                {
                    if (gridExRow.RowType == RowType.Record)
                    {
                        TPhieuNhapxuatthuocChitiet newItem = new TPhieuNhapxuatthuocChitiet();
                        newItem.MaNhacungcap = Utility.sDbnull(cboNhaCungcap.SelectedValue);
                        newItem.IdThuoc =
                            Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc].Value);
                        newItem.NgayHethan =
                            Convert.ToDateTime(gridExRow.Cells["NGAY_HET_HAN"].Value).Date;
                        newItem.GiaNhap = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap].Value);
                        newItem.SoLo = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLo].Value);
                        newItem.SoDky = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoDky].Value);
                        newItem.SoQdinhthau = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau].Value);
                        newItem.SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLuong].Value);
                        newItem.ThanhTien = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value);
                        newItem.ChietKhau = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau].Value);
                        newItem.Vat = Utility.DecimaltoDbnull(txtVAT.Text);
                        newItem.GiaBan = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBan].Value, 0);
                        newItem.DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.DonGia].Value, 0);
                        newItem.SluongChia = 1;
                        newItem.GiaBhyt = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt].Value);
                        newItem.GiaBhytCu = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBhytCu].Value);
                        newItem.CoBhyt = Utility.ByteDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.CoBhyt].Value);
                        newItem.GiaPhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen].Value);
                        newItem.GiaPhuthuTraituyen = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen].Value);
                        newItem.ThangDu = Utility.Int16Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThangDu].Value, 0);
                        newItem.KieuThuocvattu = Utility.sDbnull(gridExRow.Cells[DmucThuoc.Columns.KieuThuocvattu].Value);
                        newItem.IdQdinh = Utility.Int64Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdQdinh].Value, 0);
                        newItem.IdThau = Utility.Int64Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThau].Value, 0);
                        newItem.IdThauCt = Utility.Int64Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThauCt].Value, 0);
                        lstDetails.Add(newItem);
                    }
                }
                return lstDetails;
            }
            catch(Exception ex)
            {
                Utility.CatchException("Lỗi khi tạo dữ liệu phiếu chi tiết. Đề nghị kiểm tra lại\n",ex);
                return lstDetails;
            }
        }
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        private void ThemPhieuNhapKho()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = TaophieuNhapkho();
            List< TPhieuNhapxuatthuocChitiet> lstDetail=TaoChitietNhapkho();
            if (lstDetail.Count <= 0)
            {
               
                return;
            }
            ActionResult actionResult = new THUOC_NHAPKHO().ThemPhieuNhapKho(objPhieuNhap, lstDetail);
            switch (actionResult)
            {
                case ActionResult.Success:
                    
                    txtIDPhieuNhapKho.Text = Utility.sDbnull(objPhieuNhap.IdPhieu);
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow newDr = p_mDataPhieuNhapKho.NewRow();
                    Utility.FromObjectToDatarow(objTPhieuNhapxuatthuoc,ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhonhap.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    p_mDataPhieuNhapKho.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdList,TPhieuNhapxuatthuoc.Columns.IdPhieu,Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"] ,"Thêm mới phiếu thành công",false);
                    m_enAction = action.Update;
                    ClearControl();
                    if (!_Autosave)
                    {
                        ClearControlPhieu();
                        txtSoHoaDon.Focus();
                    }
                    b_Cancel = false;
                    if (PropertyLib._NhapkhoProperties.Themmoilientuc && !_Autosave) cmdThemPhieuNhap.PerformClick();
                    else
                        if (!_Autosave)
                            this.Close();
                    _Autosave = false;
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm phiếu", "Thông báo lỗi",MessageBoxIcon.Error);
                    break;
            }
        }
        private void UpdatePhieuNhapKho()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = TaophieuNhapkho();
            List<TPhieuNhapxuatthuocChitiet> lstDetail = TaoChitietNhapkho();
            if (lstDetail.Count <= 0)
            {

                return;
            }
            ActionResult actionResult = new THUOC_NHAPKHO().UpdatePhieuNhapKho(objPhieuNhap, lstDetail);
            switch (actionResult)
            {
                case ActionResult.Success:
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow[] arrDr =
                        p_mDataPhieuNhapKho.Select(string.Format("{0}={1}", TPhieuNhapxuatthuoc.Columns.IdPhieu,
                                                                 Utility.Int32Dbnull(txtIDPhieuNhapKho.Text)));
                    if(arrDr.GetLength(0)>0)
                    {
                        arrDr[0].Delete();
                    }
                    DataRow newDr = p_mDataPhieuNhapKho.NewRow();

                    Utility.FromObjectToDatarow(objTPhieuNhapxuatthuoc, ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhonhap.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    p_mDataPhieuNhapKho.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdList, TPhieuNhapxuatthuoc.Columns.IdPhieu, Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"] ,"Bạn sửa  phiếu thành công", false);
                    m_enAction = action.Insert;
                    ClearControl();
                    if (!_Autosave)
                    {
                        ClearControlPhieu();
                    }
                    b_Cancel = false;
                    if (!_Autosave)
                        this.Close();
                    _Autosave = false;
                    Utility.Log(this.Name, globalVariables.UserName,
                                        string.Format(
                                            "Sửa phiếu nhập kho với số phiếu là :{0} - Tại kho {1} ",
                                            objPhieuNhap.IdPhieu, objPhieuNhap.IdKhonhap), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }

        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        private void AutoInsert()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = TaophieuNhapkho();
            List<TPhieuNhapxuatthuocChitiet> lstDetail = TaoChitietNhapkho();
            if (lstDetail.Count <= 0 || objPhieuNhap == null)
            {

                return;
            }
            ActionResult actionResult = new THUOC_NHAPKHO().ThemPhieuNhapKho(objPhieuNhap, lstDetail);
            switch (actionResult)
            {
                case ActionResult.Success:

                    txtIDPhieuNhapKho.Text = Utility.sDbnull(objPhieuNhap.IdPhieu);
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow newDr = p_mDataPhieuNhapKho.NewRow();
                    Utility.FromObjectToDatarow(objTPhieuNhapxuatthuoc, ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhonhap.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    p_mDataPhieuNhapKho.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdList, TPhieuNhapxuatthuoc.Columns.IdPhieu, Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Tự động thêm mới phiếu nhập kho thành công", false);
                    m_enAction = action.Update;
                    _Autosave = false;
                    b_Cancel = false;

                    break;
                default:
                    break;
            }
        }
        private void AutoUpdate()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = TaophieuNhapkho();
            List<TPhieuNhapxuatthuocChitiet> lstDetail = TaoChitietNhapkho();
            if (lstDetail.Count <= 0 || objPhieuNhap == null )
            {

                return;
            }
            ActionResult actionResult = new THUOC_NHAPKHO().UpdatePhieuNhapKho(objPhieuNhap, lstDetail);
            switch (actionResult)
            {
                case ActionResult.Success:
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Tự động cập nhật phiếu nhập kho thành công", false);
                    _Autosave = false;
                    break;
                default:
                    break;
            }
        }
        #endregion
        /// <summary>
        /// HÀM THỰC HIỆN VIỆC IN PHIẾU ĐƠN THUỐC NHẬP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuNhap_Click(object sender, EventArgs e)
        {
            int IdPhieunhap = Utility.Int32Dbnull(txtIDPhieuNhapKho.Text, -1);
            VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuNhapkho(IdPhieunhap, "PHIẾU NHẬP KHO", globalVariables.SysDate);
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_INBIENBAN_GIAONHANHANG", "0", false) == "1")
            {
                VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InBienBanGiaoHang(IdPhieunhap, "BIÊN BẢN GIAO NHẬN HÀNG HÓA", globalVariables.SysDate);
            }
        }
        /// <summary>
        /// hàm thực hiện việc cho phép nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTongHoaDon_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtTongHoaDon_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
          
        }

      
        
       
        /// <summary>
        /// hàm thực hiện việc nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdChitiet_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {

            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.SoLuong.ToUpper())
            {
                decimal soluong = Utility.DecimaltoDbnull(e.Value);
                long id_thau_ct = Utility.Int64Dbnull(grdChitiet.GetValue("id_thau_ct"));
                DataTable dtData = SPs.ThuocThauLaythongtinSoluong(id_thau_ct).GetDataSet().Tables[0];
                int slkhachuyen = Utility.Int32Dbnull(dtData.Rows[0]["sl_khachuyen"]);
                //Lấy số lượng chờ trong chính phiếu đang có
                int slcho_trongphieu = Utility.Int32Dbnull(m_dtDataPhieuChiTiet.Compute("sum(so_luong)", TThauDieutietCt.Columns.IdThauCt + "=" + id_thau_ct.ToString()), 0);
                slkhachuyen += slcho_trongphieu;
                if (slkhachuyen < Utility.DecimaltoDbnull(txtSoluong.Text, 0))
                {
                    Utility.ShowMsg(string.Format("Số lượng nhập kho {0} phải <= Số lượng khả nhập {1}", e.Value, slkhachuyen.ToString()));
                    e.Value = e.InitialValue;
                    return;
                }
                decimal giaNhap = Utility.DecimaltoDbnull(grdChitiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.GiaNhap));
                decimal chietkhau = Utility.DecimaltoDbnull(grdChitiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.ChietKhau));
               
                grdChitiet.CurrentRow.BeginEdit();
                decimal thanhtien = ThanhTienTrenLuoi(giaNhap, soluong, chietkhau);
                grdChitiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value = thanhtien;
                
                grdChitiet.CurrentRow.EndEdit();
                grdChitiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaNhap.ToUpper())
            {
                decimal soluong = Utility.DecimaltoDbnull(grdChitiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.SoLuong));
                decimal giaNhap = Utility.DecimaltoDbnull(e.Value);
                decimal chietkhau = Utility.DecimaltoDbnull(grdChitiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.ChietKhau));
                int thangDu = TinhThangDutheoQuyetDinhBYT(giaNhap);
                if (_phuongphapTinhgiaban!="0") thangDu = 0;
                //REM lại để tính giá bán theo giá bán trong danh mục
                //decimal giaBan = TinhGiaBan(giaNhap, Utility.Int32Dbnull(txtVAT.Text, 0), thangDu);
                grdChitiet.CurrentRow.BeginEdit();
                decimal thanhtien = ThanhTienTrenLuoi(giaNhap, soluong, chietkhau);
                grdChitiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value = thanhtien;
                //grdPhieuNhapChiTiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBan].Value = giaBan;
                grdChitiet.CurrentRow.EndEdit();
                grdChitiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.ChietKhau.ToUpper())
            {
                decimal soluong = Utility.DecimaltoDbnull(grdChitiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.SoLuong));
                decimal chietkhau = Utility.DecimaltoDbnull(e.Value);
                decimal  giaNhap= Utility.DecimaltoDbnull(grdChitiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.GiaNhap));
                grdChitiet.CurrentRow.BeginEdit();
                decimal thanhtien = ThanhTienTrenLuoi(giaNhap, soluong, chietkhau);
                grdChitiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value = thanhtien;
                grdChitiet.CurrentRow.EndEdit();
                grdChitiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.ThangDu.ToUpper())
            {
                return;
                int  thangDu = Utility.Int32Dbnull(e.Value,0);
                if (_phuongphapTinhgiaban!="0") thangDu = 0;
                decimal giaNhap = Utility.Int32Dbnull(grdChitiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.GiaNhap), 0);
                
                decimal giaBan = TinhGiaBan(giaNhap, Utility.Int32Dbnull(txtVAT.Text, 0), thangDu);
                grdChitiet.CurrentRow.BeginEdit();
                grdChitiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBan].Value = giaBan;
                grdChitiet.CurrentRow.EndEdit();
                grdChitiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.NgayHethan.ToUpper())
            {
                if(string.IsNullOrEmpty(Utility.sDbnull(e.Value)))
                {
                    Utility.ShowMsg("Ngày hết hạn không thể bỏ trống \n Mời bạn xem lại","Thông báo",MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    DateTime ngayHethancu = Convert.ToDateTime(e.InitialValue);
                    DateTime ngayHethanmoi = Convert.ToDateTime(e.Value);
                    if(!SubSonic.Sugar.Dates.IsDate(ngayHethanmoi))
                    {
                        Utility.ShowMsg("Ngày hết hạn không đúng định dạng \n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
               

            }
            TinhSumThanhTien();
        }
        private decimal ThanhTienTrenLuoi(decimal  GiaNhap, decimal soluong,decimal  chietkhau)
        {
            decimal thanhtien = GiaNhap * soluong;
            thanhtien = thanhtien + thanhtien * Utility.DecimaltoDbnull(txtVAT.Text) / 100 - chietkhau;
            return thanhtien;
        }
        /// <summary>
        /// hàm thực hiện việc thay đổi thông tin của vat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtVAT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateWhenChanged();
                ThanhTien();
                TinhGiaBan();
            }
            catch
            {
            }
        }
        private void UpdateWhenChanged()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdChitiet.GetDataRows())
                {
                    Int32 soluong = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLuong].Value);
                    int THANG_DU = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThangDu].Value, 0);
                    decimal GiaNhap = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap].Value);
                    decimal ChietKhau = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau].Value);
                    decimal thanhtien = soluong * GiaNhap + (soluong * GiaNhap) * Utility.Int32Dbnull(txtVAT.Text, 0) / 100 - ChietKhau;
                    //REM lại để tính giá bán theo giá bán trong danh mục                   
                   // decimal Gia_ban = TinhGiaBan(GiaNhap, Utility.Int32Dbnull(txtVAT.Text, 0), THANG_DU);
                    gridExRow.BeginEdit();
                    gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value = thanhtien;
                    //gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBan].Value = Gia_ban;
                    gridExRow.EndEdit();
                }
                grdChitiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
                TinhSumThanhTien();
            }
            catch
            {
            }
        }

       
        void TinhGiaBan()
        {
            try
            {
                if (_phuongphapTinhgiaban == "3") return;
                if (!Utility.IsNumeric(txtDongia.Text))
                {
                    txtGiaDV.Text = "0";
                    return;
                }
                decimal GIA_Nhap = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                decimal GIA_BAn = 0;
                decimal GiaThangDu = 0;
                decimal GiaVAT = 0;
                if (_phuongphapTinhgiaban == "0")
                {
                    GiaVAT = GIA_Nhap;
                    GiaThangDu = (decimal)(GiaVAT * nmrThangDu.Value / 100);
                    GIA_BAn = GiaThangDu + GiaVAT;
                }
                else if (_phuongphapTinhgiaban == "1")
                {
                    //Giá VAT
                    GiaVAT = GIA_Nhap + (decimal)(GIA_Nhap * Utility.DecimaltoDbnull(txtVAT.Text) / 100);
                    //Thặng dư so với giá VAT
                    GiaThangDu = (decimal)(GiaVAT * nmrThangDu.Value / 100);
                    GIA_BAn = GiaThangDu + GiaVAT;
                }
                else
                {
                    GIA_BAn = GIA_Nhap + (decimal)(GIA_Nhap * Utility.DecimaltoDbnull(txtVAT.Text) / 100);
                }
                txtGiaDV.Text = GIA_BAn.ToString();
            }
            catch
            {
            }
        }
        decimal TinhGiaBan(decimal GiaNhap,int VAT,int ThangDu)
        {
            try
            {

                decimal GIA_BAn = GiaNhap;
                decimal giaThangDu = 0;
                decimal giaVat = 0;
                if (_phuongphapTinhgiaban == "0")
                {
                    giaVat = GiaNhap;
                    giaThangDu = (decimal)(giaVat * ThangDu / 100);
                    GIA_BAn = giaThangDu + giaVat;
                }
                else if (_phuongphapTinhgiaban == "1")
                {
                    //Giá VAT
                    giaVat = GiaNhap + (decimal)(GiaNhap * VAT / 100);
                    //Thặng dư so với giá VAT
                    giaThangDu = (decimal)(giaVat * ThangDu / 100);
                    GIA_BAn = giaThangDu + giaVat;
                }
                else
                {
                    GIA_BAn = GiaNhap +(decimal)( GiaNhap * _phantramSovoigianhap / 100);
                }
                return GIA_BAn;
            }
            catch
            {
                return GiaNhap;
            }
        }
       
        int TinhThangDutheoQuyetDinhBYT(decimal GiaMua)
        {
            //if (KIEU_THUOC_VT == "VT") return 0;
            if (GiaMua <= 1000) return 15;
            if (GiaMua > 1000 && GiaMua <= 5000) return 10;
            if (GiaMua > 5000 && GiaMua <= 100000) return 7;
            if (GiaMua > 100000 && GiaMua <= 1000000) return 5;
            if (GiaMua > 1000000) return 2;
            return 0;
        }
        private void txtDongia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtThanhTien_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtThanhTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void grdChitiet_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.SoLuong.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaNhap.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaBan.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.ChietKhau.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.ThangDu.ToUpper()

                )
            {
                e.Column.InputMask = "{0:#,#.##}";
            }
          //  UpdateWhenChanged();
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frm_Properties( PropertyLib._NhapkhoProperties);
                
                frm.ShowDialog();
                CauHinh();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }

        private void nmrThangDu_Click(object sender, EventArgs e)
        {

        }

        private void cmdQuickCreate_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            cmdCancel.Enabled = false;
            blnCancel = true;
            cmdCancel.SendToBack();
            cmdCancel.Enabled = true;
        }

        private void chkTrongthau_CheckedChanged(object sender, EventArgs e)
        {
            cboSoQDthau.Enabled = chkTrongthau.Checked;
            if (!chkTrongthau.Checked)
            {
                id_thau = -1;
                id_thau_ct = -1;
                txtDrugName.dtData = dtDmuc_thuoc;
                txtDrugName.ChangeDataSource();
                txtDrugName.Focus();
            }
            else//Load thuốc dựa theo số QĐ thầu
            {
                cboSoQDthau_SelectedIndexChanged(cboSoQDthau, e);
            }
        }

        private void cboNhaCungcap_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDataSoQD = Utility.ExecuteSql(string.Format("select distinct maso_qdinh from t_thau_qdinh where ma_nha_cc='{0}'", cboNhaCungcap.SelectedValue.ToString()), CommandType.Text).Tables[0];
                DataBinding.BindDataCombobox(cboSoQDthau, dtDataSoQD, "maso_qdinh", "maso_qdinh");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        
       

      
    }
}
