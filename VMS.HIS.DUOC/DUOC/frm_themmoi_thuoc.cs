using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
using VNS.Properties;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.NGHIEPVU;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_themmoi_thuoc : Form
    {
        #region "Public Variables(Class Level)
        public bool m_blnCancel = true;
        /// <summary>
        /// Biến xác định xem form được gọi từ đâu
        /// true: Gọi từ Menu
        /// false: Gọi từ một Form khác và thường trả về đối tượng khi chọn trên lưới hoặc nhấn nút chọn
        /// </summary>
        public bool m_blnCallFromMenu = true;
        /// <summary>
        /// Biến trả về khi thực hiện DoubleClick trên lưới với điều kiện blnCallFromMenu=false
        /// </summary>
        public DmucThuoc objThuoc;
        public Janus.Windows.GridEX.GridEX grdList;
        public DataTable m_dtDrugDataSource;
        public DataTable m_dtSameCodeDataSource = new DataTable();
        public DataTable m_dtObjectDataSource = new DataTable();
        public int Drug_ID = -1;
        /// <summary>
        /// Thao tác đang thực hiện là gì: Insert, Delete, Update hay Select...
        /// </summary>
        public action m_enAction;
        #endregion

        #region "Private Variables(Class Level)"

        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";

        private int m_intOldMaxDrugID = 0;

        /// <summary>
        /// Biến để tránh trường hợp khi gán Datasource cho GridView thì xảy ra sự kiện CurrentCellChanged
        /// Điều này là do 2 Thread thực hiện song song nhau. Do vậy ta cần xử lý nếu chưa Loaded xong thì
        /// chưa cho phép binding dữ liệu từ Gridview vào Control trên Form trong sự kiện CurrentCellChanged
        /// </summary>
        private bool m_blnLoaded = false;
        private SubSonic.Query m_Query;
        private SubSonic.QueryCommand m_QueryCmd = null;
        #endregion

        //Các phương thức khởi tạo của Class

        #region "Constructors"
        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của thuốc
        /// </summary>
        /// <param name="p_strDrugOnly"></param>
        public frm_themmoi_thuoc(string p_strDrugOnly)
        {
            InitializeComponent();
            try
            {
                Utility.SetVisualStyle(this);
                InitializeEvents();
                m_Query = DmucThuoc.CreateQuery();
                m_QueryCmd = m_Query.BuildSelectCommand();
                //Khởi tạo các giá trị mặc định. Có thể đặt ở Form load và có thể sử dụng Thread để Load trong 
                //một số trường hợp cần cải thiện Performance
                if (p_strDrugOnly.ToUpper() == "DRUGONLY")
                {

                }
                txtCode.Enabled = false;
               
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
                THU_VIEN_CHUNG.EnableBHYT(this);
            }
        }



        #endregion

        //Vùng này chứa các thuộc tính để thao tác với các đối tượng khác 
        //Hiện tại ko dùng

        #region "Public Properties"
        #endregion

        #region "Private Methods"

        #region "Private Methods including Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        private void InitializeEvents()
        {
             txtDongia.KeyPress += new KeyPressEventHandler( txtDongia_KeyPress);
            this.KeyDown += new KeyEventHandler(frm_themmoi_thuoc_KeyDown);
            txtName.LostFocus += new EventHandler(txtName_LostFocus);
            
            txtDonvitinh._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtDonvitinh__OnShowData);
            txtDonvichia._OnShowData += txtDonvichia__OnShowData;
            chkChiathuoc.CheckedChanged += chkChiathuoc_CheckedChanged;
            txtThuoc._OnEnterMe += txtThuoc__OnEnterMe;
            txtCachsudung._OnShowData += txtCachsudung__OnShowData;
            txtNuocSX._OnShowData += txtNuocSX__OnShowData;
            txtHangSX._OnShowData += txtHangSX__OnShowData;
            txtDangBaoChe._OnShowData += txtDangBaoChe__OnShowData;
            txtKieuthuocVT._OnShowData += txtKieuthuocVT__OnShowData;
            txtKieuthuocVT._OnEnterMe += txtKieuthuocVT__OnEnterMe;
            txtKieuthuocVT._OnSelectionChanged += txtKieuthuocVT__OnSelectionChanged;
            txtnhathau._OnShowData += txtnhathau__OnShowData;
            txtloaithau._OnShowData += txtloaithau__OnShowData;
            txtnhomthau._OnShowData += txtnhomthau__OnShowData;
            autoPhanloaithuoc._OnShowData += autoPhanloaithuoc__OnShowData;
            cmdNew.Click += cmdNew_Click;
        }

        void txtKieuthuocVT__OnSelectionChanged()
        {
            if (txtKieuthuocVT.myCode == "VT")
            {
                lblNhomduocly.ForeColor =  lblMathuoc.ForeColor;
                lblHoatchat.ForeColor = lblTinhchat.ForeColor = lblDuongdung.ForeColor = lblBietduoc.ForeColor;
            }
            else
            {
                lblNhomduocly.ForeColor =lblHoatchat.ForeColor=lblTinhchat.ForeColor=lblDuongdung.ForeColor= lblMathuoc.ForeColor;
            }
        }

        void autoPhanloaithuoc__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(autoPhanloaithuoc.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = autoPhanloaithuoc.myCode;
                autoPhanloaithuoc.Init();
                autoPhanloaithuoc.SetCode(oldCode);
                autoPhanloaithuoc.Focus();
            }    
        }

        void txtKieuthuocVT__OnEnterMe()
        {
            AutocompleteLoaithuoc();
        }

        void txtKieuthuocVT__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKieuthuocVT.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKieuthuocVT.myCode;
                txtKieuthuocVT.Init();
                txtKieuthuocVT.SetCode(oldCode);
                txtKieuthuocVT.Focus();
            }    
        }
        void cmdNew_Click(object sender, EventArgs e)
        {
            m_enAction = action.Insert;
            SetControlStatus();
        }

        void txtDangBaoChe__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDangBaoChe.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDangBaoChe.myCode;
                txtDangBaoChe.Init();
                txtDangBaoChe.SetCode(oldCode);
                txtDangBaoChe.Focus();
            }   
        }
        void txtnhathau__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtnhathau.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtnhathau.myCode;
                txtnhathau.Init();
                txtnhathau.SetCode(oldCode);
                txtnhathau.Focus();
            }
        }
        void txtloaithau__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtloaithau.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtloaithau.myCode;
                txtloaithau.Init();
                txtloaithau.SetCode(oldCode);
                txtloaithau.Focus();
            }
        }
        void txtnhomthau__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtnhomthau.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtnhomthau.myCode;
                txtnhomthau.Init();
                txtnhomthau.SetCode(oldCode);
                txtnhomthau.Focus();
            }
        }
        void txtHangSX__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtHangSX.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtHangSX.myCode;
                txtHangSX.Init();
                txtHangSX.SetCode(oldCode);
                txtHangSX.Focus();
            }    
        }

        void txtNuocSX__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNuocSX.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNuocSX.myCode;
                txtNuocSX.Init();
                txtNuocSX.SetCode(oldCode);
                txtNuocSX.Focus();
            }     
        }

        void txtCachsudung__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtCachsudung.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtCachsudung.myCode;
                txtCachsudung.Init();
                txtCachsudung.SetCode(oldCode);
                txtCachsudung.Focus();
            }   
        }

     
        void txtThuoc__OnEnterMe()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDmucthuoc.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    if (Utility.Int32Dbnull(gridExRow.Cells[DmucThuoc.Columns.IdThuoc].Value) == Utility.Int32Dbnull(txtThuoc.MyID))
                    {
                        gridExRow.Cells["CHON"].Value = 1;
                        gridExRow.IsChecked = true;
                        break;
                    }
                    gridExRow.BeginEdit();
                }
            }
            catch (Exception)
            {
                
               
            }
           
        }

        void chkChiathuoc_CheckedChanged(object sender, EventArgs e)
        {
            txtDonvichia.Enabled = txtDongiachia.Enabled = txtSoluongchia.Enabled = chkChiathuoc.Checked;
        }

        void txtDonvichia__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDonvitinh.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);

                oldCode = txtDonvichia.myCode;
                txtDonvichia.Init();
                txtDonvichia.SetCode(oldCode);

                txtDonvichia.Focus();
            }   
        }

       

        void txtDonvitinh__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDonvitinh.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);

                oldCode = txtDonvichia.myCode;
                txtDonvichia.Init();
                txtDonvichia.SetCode(oldCode);

                txtDonvitinh.Focus();
            } 
        }

        void cmdThemdonvitinh_Click(object sender, EventArgs e)
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG("DONVITINH");
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);
                txtDonvitinh.Focus();
            }
        }

        void InitLoaithuoc()
        {
            int id = Utility.Int32Dbnull(autoNhomDuocly.MyID, -1);
            AutocompleteLoaithuoc();
            autoNhomDuocly.SetId(id);
        }
        void txtLoaithuoc__OnEnterMe()
        {
            
        }
        void frm_themmoi_thuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) this.Dispose();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
        }


        /// <summary>
        /// Load các danh mục chủng loại thuốc và đơn vị tính vào ComboBox
        /// </summary>
        private void BindingDataSource()
        {


        }
        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi đóng gói dữ liệu vào Entity
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            errorProvider1.Clear();
            //if (String.IsNullOrEmpty(txtMaQD40.Text))
            //{
            //    Utility.SetMsg(lblMsg, "Bạn cần nhập mã thuốc.",true);
            //   txtMaQD40.Focus();
            //    return false;
            //}
            if (txtKieuthuocVT.myCode=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn kiểu thuốc vật tư.", true);
                errorProvider1.SetError(txtKieuthuocVT, lblMsg.Text);
                txtKieuthuocVT.Focus();
                return false;
            }
            //if (!globalVariables.IsAdmin)
            //{
                if (String.IsNullOrEmpty(txtCode.Text))
                {
                    Utility.SetMsg(lblMsg, "Mã thuốc không được để trống và phải là duy nhất trong hệ thống",true);
                    errorProvider1.SetError(txtCode, lblMsg.Text);
                    txtCode.Focus();
                    return false;
                }
            //}

            if (autoPhanloaithuoc.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập thông tin phân loại thuốc",true);
                errorProvider1.SetError(autoPhanloaithuoc, lblMsg.Text);
                autoPhanloaithuoc.Focus();
                autoPhanloaithuoc.SelectAll();
                return false;
            }
            if (Utility.DoTrim(txtName.Text)=="")
            {
                Utility.SetMsg(lblMsg, "Tên thuốc không được để trống.",true);
                errorProvider1.SetError(txtName, lblMsg.Text);
                txtName.Focus();
                return false;
            }
            if (txtKieuthuocVT.myCode == "THUOC")
            {
                if (autoNhomDuocly.MyCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn nhóm dược lý", true);
                    errorProvider1.SetError(autoNhomDuocly, lblMsg.Text);
                    autoNhomDuocly.Focus();
                    autoNhomDuocly.SelectAll();
                    return false;
                }
                if (autoHoatchat.MyCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn hoạt chất", true);
                    errorProvider1.SetError(autoHoatchat, lblMsg.Text);
                    autoHoatchat.Focus();
                    autoHoatchat.SelectAll();
                    return false;
                }
                if (autoTinhchat.MyCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn tính chất thuốc", true);
                    errorProvider1.SetError(autoTinhchat, lblMsg.Text);
                    autoTinhchat.Focus();
                    autoTinhchat.SelectAll();
                    return false;
                }
                if (txtCachsudung.myCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn đường dùng", true);
                    errorProvider1.SetError(txtCachsudung, lblMsg.Text);
                    txtCachsudung.Focus();
                    txtCachsudung.SelectAll();
                    return false;
                }
            }
            else
            {
                if (autoNhomDuocly.MyCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn nhóm dược lý", true);
                    errorProvider1.SetError(autoNhomDuocly, lblMsg.Text);
                    autoNhomDuocly.Focus();
                    autoNhomDuocly.SelectAll();
                    return false;
                }
            }
            if (txtDonvitinh.myCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập đơn vị tính", true);
                errorProvider1.SetError(txtDonvitinh, lblMsg.Text);
                txtDonvitinh.Focus();
                txtDonvitinh.SelectAll();
                return false;
            }
            if (Utility.DoTrim( txtDongia.Text)=="")
            {
                Utility.SetMsg(lblMsg, "Đơn giá không được để trống.",true);
                errorProvider1.SetError(txtDongia, lblMsg.Text);
                 txtDongia.Focus();
                return false;
            }
            if (chkChiathuoc.Checked)
            {
                if (Utility.DecimaltoDbnull(txtSoluongchia.Text,0)<=0)
                {
                    Utility.SetMsg(lblMsg, "Số lượng chia phải >0.", true);
                    errorProvider1.SetError(txtSoluongchia, lblMsg.Text);
                    txtSoluongchia.Focus();
                    return false;
                }
                if (Utility.DecimaltoDbnull(txtDongiachia.Text, 0) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Đơn giá chia phải >0.", true);
                    errorProvider1.SetError(txtDongiachia, lblMsg.Text);
                    txtDongiachia.Focus();
                    return false;
                }
                if (txtDonvichia.myCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập đơn vị chia thuốc", true);
                    errorProvider1.SetError(txtDonvichia, lblMsg.Text);
                    txtDonvichia.Focus();
                    return false;
                }
            }
            

            //if (BusinessHelper.GetAccountName() == "DETMAY")
            //{


            //    if (string.IsNullOrEmpty(txtNumber_Register.Text))
            //    {
            //        Utility.ShowMsg("Bạn phải nhập số đăng ký của thuốc");
            //        txtNumber_Register.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(txtNuocSX.Text))
            //    {
            //        Utility.ShowMsg("Bạn phải nhập số nhà sản xuất của thuốc");
            //        txtNuocSX.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(txtHangSX.Text))
            //    {
            //        Utility.ShowMsg("Bạn phải nhập số nước sản xuất của thuốc");
            //        txtHangSX.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(txtListBHYT.Text))
            //    {
            //        Utility.ShowMsg("Bạn phải nhập số  danh mục BHYT của thuốc");
            //        txtListBHYT.Focus();
            //        return false;
            //    }
            //}
            return true;
        }
        private void CreateDefaultTableStructure()
        {

        }
        /// <summary>
        /// Lấy về danh mục thuốc tương đương 
        /// </summary>
        private void GetSameCodeDrugList()
        {

        }
        /// <summary>
        /// Lấy về danh mục đối tượng thuốc
        /// </summary>
        private void GetObjectList()
        {

        }
        /// <summary>
        /// Điền dữ liệu của đối tượng cần cập nhật vào các Controls trên Form
        /// </summary>
        private void FillDataIntoControlWhenUpdate()
        {
            try
            {
                objThuoc = DmucThuoc.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
                if (objThuoc == null)
                {
                    Utility.ShowMsg("Lỗi không xác định được đối tượng cần cập nhật. Liên hệ với VMS để được giải đáp.");
                    return;
                }

                if (objThuoc != null)
                {
                    Drug_ID = objThuoc.IdThuoc;
                    txtID.Text = Utility.sDbnull(objThuoc.IdThuoc).Trim();
                    autoHoatchat.SetCode( Utility.sDbnull(objThuoc.MaHoatchat).Trim());
                    txtCode.Text = Utility.sDbnull(objThuoc.MaThuoc);
                    txtMaQD40.Text = Utility.sDbnull(objThuoc.MaQD40);
                    txtMaQDTinh.Text = Utility.sDbnull(objThuoc.MaQDTinh);
                    txtSlVuottran.Text = Utility.DecimaltoDbnull(objThuoc.SluongVuottran, 0).ToString();
                    txtDongia.Text = Utility.sDbnull(objThuoc.DonGia);
                    txtGiaDV.Text = Utility.sDbnull(objThuoc.GiaDv);
                    txtGiaBHYT.Text = Utility.sDbnull(objThuoc.GiaBhyt);
                    txtPTDT.Text = Utility.sDbnull(objThuoc.PhuthuDungtuyen);
                    txtPTTT.Text = Utility.sDbnull(objThuoc.PhuthuTraituyen);
                    txtName.Text = Utility.sDbnull(objThuoc.TenThuoc);
                    txtDesc.Text = Utility.sDbnull(objThuoc.MotaThem);
                    txtKieuthuocVT.SetCode(objThuoc.KieuThuocvattu);
                    AutocompleteLoaithuoc();
                    autoNhomDuocly.SetId(Utility.sDbnull(objThuoc.IdLoaithuoc));
                    txtDonvitinh.SetCode(objThuoc.MaDonvitinh);
                    autoPhanloaithuoc.SetCode(objThuoc.MaPhanloaithuoc);
                    autoTinhchat.SetCode(objThuoc.MaTinhchat);
                    txtNumber_Register.Text = Utility.sDbnull(objThuoc.SoDangky);
                    txtNuocSX._Text = Utility.sDbnull(objThuoc.NuocSanxuat);
                    txtHangSX._Text = Utility.sDbnull(objThuoc.HangSanxuat);
                    txtContent.Text = Utility.sDbnull(objThuoc.HamLuong);
                    txtQD31.Text = objThuoc.QD31;
                    txtloaithau.SetCode(objThuoc.LoaiThau);
                    txtnhomthau.SetCode(objThuoc.NhomThau);
                    txtnhathau.SetCode(objThuoc.IdNhathau);
                    chkTutuc.Checked = Utility.Byte2Bool(objThuoc.TuTuc);
                    chkSingle.Checked = Utility.Byte2Bool(objThuoc.SingleService);
                    txtDangBaoChe._Text= Utility.sDbnull(objThuoc.DangBaoche, "");
                    txtTEN_BHYT.Text = Utility.sDbnull(objThuoc.TenBhyt);
                    chkHieuLuc.Checked = Utility.Int32Dbnull(objThuoc.TrangThai, 0) == 1 ? true : false;
                    optAll.Checked = objThuoc.NoitruNgoaitru=="ALL";
                    optNgoai.Checked = objThuoc.NoitruNgoaitru == "NGOAI";
                    optNoitru.Checked = objThuoc.NoitruNgoaitru == "NOI";
                    txtGioihankedon.Text = Utility.sDbnull(objThuoc.GioihanKedon,"0");
                    txtBut.Text = Utility.sDbnull(objThuoc.DonviBut, "0");
                    txtCachsudung.SetCode(objThuoc.MaDuongdung);
                    txtSoluongchia.Text = Utility.DecimaltoDbnull(objThuoc.SluongChia, 0).ToString();
                    txtDongiachia.Text = Utility.DecimaltoDbnull(objThuoc.DongiaChia, 0).ToString();
                    txtDonvichia.SetCode(objThuoc.MaDvichia);
                    chkChiathuoc.Checked = Utility.Byte2Bool(objThuoc.CoChiathuoc);
                    chkThucphamchucnang.Checked = Utility.Bool2Bool(objThuoc.TuvanThem);
                    txtTenbietduoc.Text = objThuoc.TenBietduoc;
                    chkHuySuDung.Checked =  Utility.Bool2Bool(objThuoc.HuySudung);
                    chkThuoc_TheoDoi.Checked = Utility.Bool2Bool(objThuoc.ThuocTheodoi);
                    chkIsNhap_SLKhong.Checked = Utility.Bool2Bool(objThuoc.NhapSluong0);
                    chkGuiAX.Checked = Utility.Bool2Bool(objThuoc.AxCongbhyt);
                    chkCam_GuiXML.Checked = Utility.Bool2Bool(objThuoc.CamguiXml);
                    chkKeKemDichVu.Checked = Utility.Bool2Bool(objThuoc.KekemDvu);
                    chkLaGoi.Checked = Utility.Bool2Bool(objThuoc.LagoiVattu);
                    autoQuicach._Text = objThuoc.QuiCach;
                    lblDVT.Text = txtDonvitinh.Text;
                    txttylett.Text = Utility.sDbnull(objThuoc.TyleTt);
                    LoadQheCamchidinhchung(objThuoc.IdThuoc);
                }
                GetSameCodeDrugList();
                GetObjectList();
                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        private QheCamchidinhChungphieuCollection GetQheCamchidinhChungphieuCollection()
        {
            QheCamchidinhChungphieuCollection lst = new QheCamchidinhChungphieuCollection();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDmucthuoc.GetCheckedRows())
            {
                QheCamchidinhChungphieu objQheNhanvienDanhmuc = new QheCamchidinhChungphieu();
                objQheNhanvienDanhmuc.IdDichvu = -1;
                objQheNhanvienDanhmuc.Loai = 1;
                objQheNhanvienDanhmuc.IdDichvuCamchidinhchung = Utility.Int32Dbnull(gridExRow.Cells[QheCamchidinhChungphieu.Columns.IdDichvu].Value);
                objQheNhanvienDanhmuc.IsNew = true;
                lst.Add(objQheNhanvienDanhmuc);
            }
            return lst;
        }
        private void LoadQheCamchidinhchung(int id_thuoc)
        {
            DataRow[] arrDr = m_dtqheCamKeChungDonthuoc.Select(QheCamchidinhChungphieu.Columns.Loai + " IN ('THUOC', 'VT') AND (" + QheCamchidinhChungphieu.Columns.IdDichvu + "=" + id_thuoc + " OR " + QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung + "=" + id_thuoc+ ")");
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDmucthuoc.GetDataRows())
            {
                gridExRow.BeginEdit();
                if (Utility.Int32Dbnull(gridExRow.Cells["id_thuoc"].Value) != id_thuoc)
                {
                    var query = from kho in arrDr.AsEnumerable()
                                where
                                Utility.Int32Dbnull(kho[QheCamchidinhChungphieu.Columns.IdDichvu], 0)
                                == Utility.Int32Dbnull(gridExRow.Cells["id_thuoc"].Value)
                                || Utility.Int32Dbnull(kho[QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung], 0)
                                == Utility.Int32Dbnull(gridExRow.Cells["id_thuoc"].Value)
                                select kho;
                    if (query.Any())
                    {
                        gridExRow.Cells["CHON"].Value = 1;
                        gridExRow.IsChecked = true;
                    }
                    else
                    {
                        gridExRow.Cells["CHON"].Value = 0;
                        gridExRow.IsChecked = false;
                    }
                }
                gridExRow.EndEdit();

            }
        }
        private DataTable m_dtQuanHe = new DataTable();
        /// <summary>
        /// Thiết lập trạng thái các Control trên Form theo thao tác nghiệp vụ cần thực hiện
        /// Insert, Update hoặc Delete...
        /// </summary>
        private void SetControlStatus()
        {
            switch (m_enAction)
            {
                case action.Insert:
                    CreateDefaultTableStructure();
                    //Cho phép nhập liệu mã kho,vị trí, tên kho và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    //Utility.EnabledTextBox(txtMaQD40);
                    //Utility.EnabledTextBox(txtMaQDTinh);
                    Utility.EnabledTextBox(txtCode);
                    autoPhanloaithuoc.Enabled = true;
                    txtDonvitinh.Enabled = true;
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledControl(autoTinhchat);
                    //  Utility.EnabledTextBox( txtDongia);
                    txtDongia.Enabled = true;
                    Utility.EnabledTextBox(txtDesc);
                    txtCode.Clear();
                    txtMaQD40.Clear();
                    txtMaQDTinh.Clear();
                    txtContent.Clear();
                    txtNumber_Register.Clear();
                    txtHangSX.SetDefaultItem();
                    txtNuocSX.SetDefaultItem();
                    chkHieuLuc.Checked = true;
                    txtName.Clear();
                    txtTEN_BHYT.Clear();
                    autoPhanloaithuoc.SetDefaultItem();
                    autoHoatchat.Clear();
                    txtDangBaoChe.SetDefaultItem();
                    txtDonvitinh.SetDefaultItem();
                    txtDongia.Clear();
                    txtGiaBHYT.Clear();
                    txtCachsudung.SetDefaultItem();
                    txtPTDT.Clear();
                    txtSlVuottran.Text = "0";
                    txtPTTT.Clear();
                    optAll.Checked = true;
                    chkTutuc.Checked = false;
                    chkSingle.Checked = false;
                    chkThucphamchucnang.Checked = false;
                    txtDesc.Clear();
                    //--------------------------------------------------------------
                    //Cho phép nhấn nút Ghi
                    cmdSave.Enabled = true;
                    //Tự động Focus đến mục Code để người dùng nhập liệu
                    txtID.Text = "Tự sinh";
                    txtMaQD40.Text = "";
                    txtMaQD40.Focus();
                    break;
                case action.Update:
                    //Cho phép nhập liệu mã kho,vị trí, tên kho và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.EnabledTextBox(txtCode);
                    Utility.EnabledTextBox(txtMaQD40);
                    Utility.EnabledTextBox(txtMaQDTinh);
                    autoPhanloaithuoc.Enabled = true;
                    txtDonvitinh.Enabled = true;
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledControl(autoTinhchat);
                    //Utility.EnabledTextBox( txtDongia);
                     txtDongia.Enabled = true;
                    Utility.EnabledTextBox(txtDesc);
                    FillDataIntoControlWhenUpdate();
                    //--------------------------------------------------------------
                    //Cho phép nhấn nút Ghi
                    cmdSave.Enabled = true;
                    //Tự động Focus đến mục Code để người dùng nhập liệu
                   txtMaQD40.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    Utility.DisabledTextBox(txtID);
                    Utility.EnabledTextBox(txtMaQD40);
                    Utility.EnabledTextBox(txtMaQDTinh);
                    Utility.EnabledTextBox(txtCode);
                    autoPhanloaithuoc.Enabled = true;
                    txtDonvitinh.Enabled = true;
                    Utility.EnabledControl(autoTinhchat);
                    Utility.EnabledTextBox(txtName);
                    // Utility.EnabledTextBox( txtDongia);
                     txtDongia.Enabled = true;
                    Utility.EnabledTextBox(txtDesc);
                    autoPhanloaithuoc.SetDefaultItem();
                    txtDonvitinh.SetCode("-1");
                    txtID.Text = "Tự sinh";
                    txtCode.Clear();
                    txtMaQD40.Clear();
                    txtMaQDTinh.Clear();
                    txtName.Clear();
                    txtDongia.Clear();
                    txtGiaBHYT.Clear();
                    txtPTDT.Clear();
                    txtPTTT.Clear();
                    txtCachsudung.SetDefaultItem();
                    chkTutuc.Checked = false;
                    chkSingle.Checked = false;
                    txtDesc.Clear();
                    //Cho phép nhập mới liên tiếp
                    m_enAction = action.Insert;
                    m_dtObjectDataSource.Clear();
                    m_dtSameCodeDataSource.Clear();
                   txtMaQD40.Focus();
                    //txtCode.Text = BusinessHelper.MaThuoc();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region "Insert, Delete, Update,Select,..."
        /// <summary>
        /// Thực hiện nghiệp vụ Insert dữ liệu
        /// </summary>
        private void PerformInsertAction()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                SqlQuery sqlQuery = new Select().From(DmucThuoc.Schema)
                    .Where(DmucThuoc.Columns.MaThuoc).IsEqualTo(txtCode.Text);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại thuốc có mã(Drug Code)=" + txtCode.Text.Trim() + ".Đề nghị bạn nhập mã khác",true);
                    m_Query = DmucThuoc.CreateQuery();
                    return;
                }
                //Bước 0: Thêm mới thuốc
                DmucThuoc objThuoc = new DmucThuoc();
                objThuoc.TenThuoc = Utility.sDbnull(txtName.Text);
                objThuoc.TenBhyt = Utility.sDbnull(txtTEN_BHYT.Text);
                objThuoc.MaThuoc = Utility.sDbnull(txtCode.Text);
                objThuoc.MaQD40 = Utility.sDbnull(txtMaQD40.Text);
                objThuoc.MaQDTinh = Utility.sDbnull(txtMaQDTinh.Text);
                objThuoc.SluongVuottran = (int)Utility.DecimaltoDbnull(txtSlVuottran.Text, 0);

                objThuoc.IdLoaithuoc = Utility.Int16Dbnull(autoNhomDuocly.MyID);
                objThuoc.DonGia = Utility.DecimaltoDbnull( txtDongia.Text, 0);
                objThuoc.GiaDv = Utility.DecimaltoDbnull(txtGiaDV.Text, 0);
                objThuoc.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                objThuoc.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                objThuoc.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                objThuoc.MotaThem = Utility.sDbnull(txtDesc.Text);
                objThuoc.DangBaoche = Utility.DoTrim(txtDangBaoChe.Text);
                objThuoc.HamLuong = Utility.sDbnull(txtContent.Text);
                objThuoc.HangSanxuat = Utility.sDbnull(txtHangSX.Text);
                objThuoc.TrangThai = chkHieuLuc.Checked ? (byte)1 : (byte)0;
                objThuoc.TuTuc =Utility.Bool2byte( chkTutuc.Checked );
                objThuoc.NgayTao = DateTime.Now;
                objThuoc.QD31 = Utility.DoTrim(txtQD31.Text);
                objThuoc.NguoiTao = globalVariables.UserName;
                objThuoc.NuocSanxuat = Utility.sDbnull(txtNuocSX.Text);
                objThuoc.IdNhathau = Utility.sDbnull(txtnhathau.myCode);
                objThuoc.LoaiThau = Utility.sDbnull(txtloaithau.myCode);
                objThuoc.NhomThau = Utility.sDbnull(txtnhomthau.Text);
                objThuoc.SoDangky = txtNumber_Register.Text;
                objThuoc.GioihanKedon = (Int16)Utility.DecimaltoDbnull(txtGioihankedon.Text,-1);
                objThuoc.DonviBut = (int)Utility.DecimaltoDbnull(txtBut.Text, -1);
                objThuoc.MaDonvitinh = txtDonvitinh.myCode;
                objThuoc.MaDuongdung = txtCachsudung.myCode;
                objThuoc.SingleService = Utility.Bool2byte(chkSingle.Checked);
                objThuoc.CoChiathuoc = Utility.Bool2byte(chkChiathuoc.Checked);
                objThuoc.TuvanThem = chkThucphamchucnang.Checked;
                objThuoc.MaDvichia = txtDonvichia.myCode;
                objThuoc.SluongChia =(int) Utility.DecimaltoDbnull(txtSoluongchia.Text, 0);
                objThuoc.DongiaChia = Utility.DecimaltoDbnull(txtDongiachia.Text, 0);
                objThuoc.TyleTt = Utility.DecimaltoDbnull(txttylett.Text, 100);
                objThuoc.MaTinhchat = autoTinhchat.MyCode;
                objThuoc.MaHoatchat = Utility.sDbnull(autoHoatchat.MyCode);
                objThuoc.KieuThuocvattu = txtKieuthuocVT.myCode;
                objThuoc.NoitruNgoaitru = optAll.Checked ? "ALL" : (optNoitru.Checked ? "NOI" : "NGOAI");
                objThuoc.TenBietduoc = Utility.sDbnull(txtTenbietduoc.Text);
                objThuoc.HuySudung = chkHuySuDung.Checked;
                objThuoc.ThuocTheodoi = chkThuoc_TheoDoi.Checked;
                objThuoc.NhapSluong0 = chkIsNhap_SLKhong.Checked;
                objThuoc.AxCongbhyt = chkGuiAX.Checked;
                objThuoc.CamguiXml = chkCam_GuiXML.Checked;
                objThuoc.KekemDvu = chkKeKemDichVu.Checked;
                objThuoc.LagoiVattu = chkLaGoi.Checked;
                objThuoc.QuiCach =  Utility.DoTrim(autoQuicach.Text);
                objThuoc.MaPhanloaithuoc = autoPhanloaithuoc.MyCode;
                objThuoc.HoatChat = autoHoatchat.Text;
                objThuoc.GioihanThoigian = Utility.DecimaltoDbnull(txtgioihan_thoigian.Text, 0);
                objThuoc.LieudungToida = Utility.DecimaltoDbnull(txtlieudung_toida.Text, 0);
                objThuoc.IsNew = true;
                dmucThuoc_busrule.Insert(objThuoc, GetQheCamchidinhChungphieuCollection());
                int v_intNewDrugID = objThuoc.IdThuoc;
                txtID.Text = Utility.sDbnull(objThuoc.IdThuoc);
                DataRow dr = m_dtDrugDataSource.NewRow();
                Utility.FromObjectToDatarow(objThuoc, ref dr);
                dr[DmucTinhchatthuoc.Columns.TenTinhchat] = autoTinhchat.Text;
                dr[DmucLoaithuoc.Columns.TenLoaithuoc] = autoNhomDuocly.Text;
                dr[DmucLoaithuoc.Columns.MaLoaithuoc] = autoNhomDuocly.MyCode;
                dr[DmucThuoc.Columns.HoatChat] = autoHoatchat.Text;
                dr["ten_donvitinh"] = txtDonvitinh.Text;
                dr["ten_donvichia"] = txtDonvichia.Text;
                dr["ten_cachsudung"] = txtCachsudung.Text;
                dr[DmucThuoc.Columns.NguoiTao] = globalVariables.UserName;
                dr[DmucThuoc.Columns.NgayTao] = DateTime.Now;
                m_dtDrugDataSource.Rows.Add(dr);
                //txtName.AddNewItems(dr);
                m_dtDrugDataSource.AcceptChanges();
                m_blnCancel = false;
                m_enAction = action.Insert;
                //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                try
                {
                    Utility.GotoNewRowJanus(grdList, "id_thuoc", v_intNewDrugID.ToString().Trim());
                    if (chkThemlientuc.Checked)
                        SetControlStatus();
                    else
                        this.Close();
                }
                catch (Exception exception)
                {
                }
                Utility.SetMsg(lblMsg, "Thêm mới dữ liệu thành công!",false);
                SetControlStatus();
               txtMaQD40.Focus();
            }
            catch
            {
            }
        }

        private void SaveSameCodeDrug(int p_intNewDrugID)
        {
            foreach (DataRow dr in m_dtSameCodeDataSource.Rows)
            {
                QheThuoctuongduong.Insert(p_intNewDrugID, Convert.ToInt16(dr["id_thuoctuongduong"]));
            }
        }
        private void SaveDrug_ObjectType(int p_intNewDrugID)
        {
            foreach (DataRow dr in m_dtObjectDataSource.Rows)
            {

            }
        }
        /// <summary>
        /// Thực hiện nghiệp vụ Update dữ liệu
        /// </summary>
        private void PerformUpdateAction()
        {
            Utility.SetMsg(lblMsg, "", false);
            //Gọi Business cập nhật dữ liệu
            Int16 v_intUpdateDrugID = Convert.ToInt16(txtID.Text);
            SqlQuery sqlQuery = new Select().From(DmucThuoc.Schema)
                  .Where(DmucThuoc.Columns.MaThuoc).IsEqualTo(txtCode.Text)
                  .And(DmucThuoc.Columns.IdThuoc).IsNotEqualTo(v_intUpdateDrugID);

            //Kiểm tra nếu trùng Mã Drug Code thì bắt nhập mã khác
            //DmucThuocCollection v_arrSameCodeObject = new DmucThuocController().FetchByQuery(m_Query.AddWhere("Drug_Code", txtCode.Text.Trim().ToUpper()).AND("id_thuoc", Comparison.NotEquals, v_intUpdateDrugID));
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg, "Đã tồn tại thuốc có mã =" + txtCode.Text.Trim() +
                                ".Đề nghị bạn nhập mã khác",true);
                m_Query = DmucThuoc.CreateQuery();
                return;
            }
            //Create Again to ignore Where Clause
            m_Query = DmucThuoc.CreateQuery();
            //Tạo giá trị mới cho đối tượng đang cần Update
            DmucThuoc objThuoc = DmucThuoc.FetchByID(txtID.Text);
            
            objThuoc.MaThuoc = Utility.sDbnull(txtCode.Text);
            objThuoc.MaQD40 = Utility.sDbnull(txtMaQD40.Text);
            objThuoc.MaQDTinh = Utility.sDbnull(txtMaQDTinh.Text);
            objThuoc.SluongVuottran = (int)Utility.DecimaltoDbnull(txtSlVuottran.Text, 0);

            objThuoc.TenThuoc = Utility.GetValue(txtName.Text, false);
            objThuoc.TenBhyt = Utility.GetValue(txtTEN_BHYT.Text, false);
            objThuoc.DonGia = Utility.DecimaltoDbnull( txtDongia.Text,0);
            objThuoc.GiaDv = Utility.DecimaltoDbnull(txtGiaDV.Text, 0);
            objThuoc.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
            objThuoc.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
            objThuoc.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
            objThuoc.TyleTt = Utility.DecimaltoDbnull(txttylett.Text, 100);
            objThuoc.MaThuoc = Utility.GetValue(txtCode.Text, false);
            objThuoc.IdLoaithuoc = Convert.ToInt16(autoNhomDuocly.MyID);
            objThuoc.MaTinhchat = autoTinhchat.MyCode;
            objThuoc.MaDonvitinh = txtDonvitinh.myCode;
            objThuoc.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
            objThuoc.MotaThem = Utility.GetValue(txtDesc.Text, false);
            objThuoc.MaHoatchat = autoHoatchat.MyCode;
            objThuoc.NuocSanxuat = txtNuocSX.Text;
            objThuoc.HangSanxuat = txtHangSX.Text;
            objThuoc.HamLuong = txtContent.Text;
            objThuoc.QD31 = Utility.DoTrim(txtQD31.Text);
            objThuoc.IdNhathau = Utility.sDbnull(txtnhathau.myCode);
            objThuoc.LoaiThau = Utility.sDbnull(txtloaithau.myCode);
            objThuoc.SoDangky = txtNumber_Register.Text;
            objThuoc.NhomThau = Utility.sDbnull(txtnhomthau.Text);
            objThuoc.TrangThai = chkHieuLuc.Checked ? (byte)1 : (byte)0;
            objThuoc.DangBaoche = Utility.DoTrim(txtDangBaoChe.Text);
            objThuoc.NguoiSua = globalVariables.UserName;
            objThuoc.NgaySua = DateTime.Now;
            objThuoc.GioihanKedon = (Int16)Utility.DecimaltoDbnull(txtGioihankedon.Text, -1);
            objThuoc.DonviBut = (int)Utility.DecimaltoDbnull(txtBut.Text, -1);
            objThuoc.MaDuongdung = txtCachsudung.myCode;
            objThuoc.SingleService = Utility.Bool2byte(chkSingle.Checked);
            objThuoc.CoChiathuoc = Utility.Bool2byte(chkChiathuoc.Checked);
            objThuoc.MaDvichia = txtDonvichia.myCode;
            objThuoc.SluongChia = (int)Utility.DecimaltoDbnull(txtSoluongchia.Text, 0);
            objThuoc.DongiaChia = Utility.DecimaltoDbnull(txtDongiachia.Text, 0);
            objThuoc.TuvanThem = chkThucphamchucnang.Checked;
            objThuoc.NoitruNgoaitru=optAll.Checked?"ALL":(optNoitru.Checked?"NOI":"NGOAI");
            objThuoc.KieuThuocvattu = txtKieuthuocVT.myCode;
            objThuoc.TenBietduoc = Utility.sDbnull(txtTenbietduoc.Text);
            objThuoc.HuySudung = chkHuySuDung.Checked;
            objThuoc.ThuocTheodoi = chkThuoc_TheoDoi.Checked;
            objThuoc.NhapSluong0 = chkIsNhap_SLKhong.Checked;
            objThuoc.AxCongbhyt = chkGuiAX.Checked;
             objThuoc.CamguiXml = chkCam_GuiXML.Checked;
            objThuoc.KekemDvu = chkKeKemDichVu.Checked;
            objThuoc.LagoiVattu = chkLaGoi.Checked;
            objThuoc.QuiCach = Utility.DoTrim(autoQuicach.Text);
            objThuoc.MaPhanloaithuoc = autoPhanloaithuoc.MyCode;
            objThuoc.HoatChat = autoHoatchat.Text;
            objThuoc.GioihanThoigian = Utility.DecimaltoDbnull(txtgioihan_thoigian.Text, 0);
            objThuoc.LieudungToida = Utility.DecimaltoDbnull(txtlieudung_toida.Text, 0);
            objThuoc.IsNew = false;
            objThuoc.MarkOld();
            dmucThuoc_busrule.Insert(objThuoc, GetQheCamchidinhChungphieuCollection());
            //Update to Datasource to reflect on DataGridView
            new Update(KcbThanhtoanChitiet.Schema)
             .Set(KcbThanhtoanChitiet.Columns.DonviTinh).EqualTo(Utility.sDbnull(txtDonvitinh.Text, "Lần"))
             .Set(KcbThanhtoanChitiet.Columns.TenChitietdichvu).EqualTo(Utility.sDbnull(txtName.Text, ""))
             .Set(KcbThanhtoanChitiet.Columns.TenBhyt).EqualTo(Utility.sDbnull(txtTEN_BHYT.Text, ""))
             .Where(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan).IsEqualTo(3)
             .And(KcbThanhtoanChitiet.Columns.IdChitietdichvu).IsEqualTo(objThuoc.IdThuoc)
             .Execute();

            DataRow dr = Utility.FetchOnebyCondition(m_dtDrugDataSource, "id_thuoc=" + v_intUpdateDrugID.ToString().Trim());
            if (dr != null)
            {
                Utility.FromObjectToDatarow(objThuoc, ref dr);
                dr[DmucThuoc.Columns.MaTinhchat] = autoTinhchat.MyCode;
                dr[DmucTinhchatthuoc.Columns.TenTinhchat] = autoTinhchat.Text;
                dr[DmucLoaithuoc.Columns.TenLoaithuoc] = autoNhomDuocly.Text;
                dr[DmucLoaithuoc.Columns.MaLoaithuoc] = autoNhomDuocly.MyCode;
                dr[DmucThuoc.Columns.HoatChat] = autoHoatchat.Text;
                dr["ten_donvitinh"] = txtDonvitinh.Text;
                dr["ten_donvichia"] = txtDonvichia.Text;
                dr["ten_cachsudung"] = txtCachsudung.Text;


                dr[DmucThuoc.Columns.HamLuong] = txtContent.Text;
                dr[DmucThuoc.Columns.SoDangky] = txtNumber_Register.Text;
                dr[DmucThuoc.Columns.NuocSanxuat] = txtNuocSX.Text;
                dr[DmucThuoc.Columns.HangSanxuat] = txtHangSX.Text;
                //txtName.UpdateItems(dr);
                m_dtDrugDataSource.AcceptChanges();
            }
            //Return to the InitialStatus
            m_enAction = action.FirstOrFinished;

            //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
            if (grdList != null) Utility.GotoNewRowJanus(grdList, "id_thuoc", txtID.Text.Trim());
            SetControlStatus();
            Utility.SetMsg(lblMsg, "Cập nhật dữ liệu thành công.", false);
            this.Close();
        }

        public CallAction CallFrom = CallAction.FromMenu;
        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            try
            {
                //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                if (!IsValidData())
                {
                    return;
                }
                if (string.IsNullOrEmpty(txtTEN_BHYT.Text)) txtTEN_BHYT.Text = Utility.sDbnull(txtName.Text);
                switch (m_enAction)
                {
                    case action.Insert:
                        PerformInsertAction();
                        break;
                    case action.Update:
                        PerformUpdateAction();
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        #endregion
        #endregion
        DataTable m_dtqheCamKeChungDonthuoc = new DataTable();
        #region "Event Handlers: Form Events,GridView Events, Button Events"
        /// <summary>
        /// Sự kiện Load của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDrug_Load(object sender, EventArgs e)
        {
            txtKieuthuocVT.Init();
            InitLoaithuoc();
            Autocompletehoatchat();
            Autocompletetinhchat();
            txtDonvitinh.Init();
            txtDonvichia.Init();
            txtNuocSX.Init();
            txtHangSX.Init();
            txtDangBaoChe.Init();
            txtCachsudung.Init();
            txtnhathau.Init();
            txtloaithau.Init();
            txtnhomthau.Init();
            autoQuicach.Init();
            autoPhanloaithuoc.Init();
            pnlGia.Enabled = Utility.Coquyen("quyen_suagia_trendanhmucthuoc");
            //txtName.Init(m_dtDrugDataSource.Copy(), new List<string>() { DmucThuoc.Columns.IdThuoc, DmucThuoc.Columns.MaThuoc, DmucThuoc.Columns.TenThuoc });
            m_dtqheCamKeChungDonthuoc = new Select().From(QheCamchidinhChungphieu.Schema).Where(QheCamchidinhChungphieu.Columns.Loai).IsEqualTo(1).ExecuteDataSet().Tables[0];
            DataTable dtChitiet = new Select().From(DmucThuoc.Schema).ExecuteDataSet().Tables[0];
            Utility.AddColumToDataTable(ref dtChitiet, "CHON", typeof(int));
            txtThuoc.Init(dtChitiet, new List<string>() { DmucThuoc.Columns.IdThuoc, DmucThuoc.Columns.MaThuoc, DmucThuoc.Columns.TenThuoc });
            Utility.SetDataSourceForDataGridEx_Basic(grdDmucthuoc, dtChitiet, true, true, "1=1", "CHON DESC," + DmucThuoc.Columns.TenThuoc);
            SetControlStatus();
        }
        private void AutocompleteLoaithuoc()
        {

            DataTable dtLoaithuoc = null;
            dtLoaithuoc = new Select().From(DmucLoaithuoc.Schema)
 .Where(DmucLoaithuoc.KieuThuocvattuColumn).IsEqualTo(txtKieuthuocVT.myCode)
 .ExecuteDataSet().Tables[0];
            if (dtLoaithuoc == null) return;
            if (!dtLoaithuoc.Columns.Contains("ShortCut"))
                dtLoaithuoc.Columns.Add(new DataColumn("ShortCut", typeof(string)));
            DataTable m_dtLoaithuoc_new = dtLoaithuoc.Clone();
            if (globalVariables.gv_dtQuyenNhanvien_Dmuc.Select(QheNhanvienDanhmuc.Columns.Loai + "= 1").Length <= 0)
                m_dtLoaithuoc_new = dtLoaithuoc.Copy();
            else
            {
                
                foreach (DataRow dr in dtLoaithuoc.Rows)
                {
                    if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucLoaithuoc.Columns.IdLoaithuoc]), "1"))
                    {
                        m_dtLoaithuoc_new.ImportRow(dr);
                    }
                }
            }
            autoNhomDuocly.Init(m_dtLoaithuoc_new, new List<string>() { DmucLoaithuoc.Columns.IdLoaithuoc, DmucLoaithuoc.Columns.MaLoaithuoc, DmucLoaithuoc.Columns.TenLoaithuoc });
            if (objThuoc != null) autoNhomDuocly.SetId(objThuoc.IdLoaithuoc);

        }
        private void Autocompletehoatchat()
        {

            DataTable dtHoatchat = null;
            dtHoatchat = new Select().From(DmucHoatchat.Schema)
 .Where(DmucHoatchat.Columns.TrangThai).IsEqualTo(1)
 .ExecuteDataSet().Tables[0];
            if (dtHoatchat == null) return;
            if (!dtHoatchat.Columns.Contains("ShortCut"))
                dtHoatchat.Columns.Add(new DataColumn("ShortCut", typeof(string)));

            autoHoatchat.Init(dtHoatchat, new List<string>() { DmucHoatchat.Columns.IdHoatchat, DmucHoatchat.Columns.MaHoatchat, DmucHoatchat.Columns.TenHoatchat });
            if (objThuoc != null) autoHoatchat.SetCode(objThuoc.MaHoatchat);

        }
        private void Autocompletetinhchat()
        {

            DataTable dtTinhchat = null;
            dtTinhchat = new Select().From(DmucTinhchatthuoc.Schema)
 //.Where(DmucTinhchatthuoc.Columns.TrangThai).IsEqualTo(1)
 .ExecuteDataSet().Tables[0];
            if (dtTinhchat == null) return;
            if (!dtTinhchat.Columns.Contains("ShortCut"))
                dtTinhchat.Columns.Add(new DataColumn("ShortCut", typeof(string)));

            autoTinhchat.Init(dtTinhchat, new List<string>() { DmucTinhchatthuoc.Columns.IdTinhchat, DmucTinhchatthuoc.Columns.MaTinhchat, DmucTinhchatthuoc.Columns.TenTinhchat });
            if (objThuoc != null) autoTinhchat.SetCode(objThuoc.MaTinhchat);

        }
       
        private void cmdRemoveObject_Click(object sender, EventArgs e)
        {
            try
            {
                //Besure that you have selected a row on GridView to remove
                int ItemChecked = Utility.GetItemsChecked(m_dtObjectDataSource, "CHON");
                int TotalItemChecked = ItemChecked;//to Compare to ignore infinite loop
                if (ItemChecked <= 0) return;
            _Continue:
                foreach (DataRow dr in m_dtObjectDataSource.Rows)
                {
                    if (Convert.ToInt16(dr["CHON"]) == 1 || Convert.ToBoolean(dr["CHON"]) == true)
                    {
                        m_dtObjectDataSource.Rows.Remove(dr);
                        m_dtObjectDataSource.AcceptChanges();
                        ItemChecked -= 1;

                    }
                    if (ItemChecked > 0 && ItemChecked != TotalItemChecked) { TotalItemChecked = ItemChecked; goto _Continue; }
                }
            }
            catch
            {
            }
        }

        private void cmdRemoveSamecode_Click(object sender, EventArgs e)
        {
            try
            {
                //Besure that you have selected a row on GridView to remove
                int ItemChecked = Utility.GetItemsChecked(m_dtSameCodeDataSource, "CHON");
                int TotalItemChecked = ItemChecked;//to Compare to ignore infinite loop
                if (ItemChecked <= 0) return;
            _Continue:
                foreach (DataRow dr in m_dtSameCodeDataSource.Rows)
                {
                    if (Convert.ToInt16(dr["CHON"]) == 1 || Convert.ToBoolean(dr["CHON"]) == true)
                    {
                        m_dtSameCodeDataSource.Rows.Remove(dr);
                        m_dtSameCodeDataSource.AcceptChanges();
                        ItemChecked -= 1;

                    }
                    if (ItemChecked > 0 && ItemChecked != TotalItemChecked) { TotalItemChecked = ItemChecked; goto _Continue; }
                }
            }
            catch
            {
            }
        }

        private void cmdRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                //Remove All object whether they are checked for removing or not
                if (Utility.AcceptQuestion("Bạn đã chắc chắn muốn xóa bỏ tất cả các đối tượng ứng với loại thuốc này không?", "Xác nhận", true))
                    m_dtObjectDataSource.Rows.Clear();
            }
            catch
            {
            }
        }
        private void cmdRemoveAllSameCode_Click(object sender, EventArgs e)
        {
            try
            {
                //Remove All object whether they are checked for removing or not
                if (Utility.AcceptQuestion("Bạn đã chắc chắn muốn xóa bỏ tất cả các thuốc tương đương ứng với loại thuốc này không?", "Xác nhận", true))
                    m_dtSameCodeDataSource.Rows.Clear();
            }
            catch
            {
            }
        }

        private void chkCheckAllorNone_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtObjectDataSource.Rows)
            {
                // dr["CHON"] = chkCheckAllorNone.Checked ? 1 : 0;
            }
            m_dtObjectDataSource.AcceptChanges();
        }

        private void chkReverse_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtObjectDataSource.Rows)
            {
                dr["CHON"] = Convert.ToInt16(dr["CHON"]) == 1 ? 0 : 1;
            }
            m_dtObjectDataSource.AcceptChanges();
        }

        private void chkcheckallSameCode_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtSameCodeDataSource.Rows)
            {
                // dr["CHON"] = chkcheckallSameCode.Checked ? 1 : 0;
            }
            m_dtSameCodeDataSource.AcceptChanges();
        }

        private void chkReverseSameCode_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtSameCodeDataSource.Rows)
            {
                dr["CHON"] = Convert.ToInt16(dr["CHON"]) == 1 ? 0 : 1;
            }
            m_dtSameCodeDataSource.AcceptChanges();
        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Sự kiện nhấn nút Thoát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sự kiện nhấn nút Ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }
        private void cmdAddObject_Click(object sender, EventArgs e)
        {
            //frmSimpleObjectTypeList frm = new frmSimpleObjectTypeList();
            //frm.m_dtObjectDataSource = m_dtObjectDataSource;
            //frm.Drug_ID = Drug_ID;
            //frm.ShowDialog();
            //cmdRemoveAllObject.Enabled = m_dtObjectDataSource.Rows.Count > 0;
            //cmdRemoveObject.Enabled = cmdRemoveAllObject.Enabled;
            //if(m_enAction==action.Insert)
            //    Utility.SetDataSourceForDataGridView(grdObjectRelationList, m_dtObjectDataSource, false, true, "", "ObjectType_Name");
        }
        private void cmdAddSamecode_Click(object sender, EventArgs e)
        {
            //frmSimpleSameCodeDrugList frm = new frmSimpleSameCodeDrugList();
            //frm.m_dtSameCodeDrugDataSource = m_dtSameCodeDataSource;
            //frm.Drug_ID = Drug_ID;
            //frm.ShowDialog();
            //cmdRemoveAllSameCode.Enabled = m_dtSameCodeDataSource.Rows.Count > 0;
            //cmdRemoveSamecode.Enabled = cmdRemoveAllSameCode.Enabled;
            //if (m_enAction == action.Insert)
            //    Utility.SetDataSourceForDataGridView(grdSameCodeDrugList, m_dtSameCodeDataSource, false, true, "", "Drug_Name");
        }
        void  txtDongia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = Utility.NumbersOnly(e.KeyChar,  txtDongia);
        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc định dạng giá của thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void  txtDongia_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(  txtDongia.Enabled = true);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện việc địn dạng tên thuốc theo định dạng chuẩn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_LostFocus(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTEN_BHYT.Text))
                txtTEN_BHYT.Text = Utility.sDbnull(txtName.Text);
        }
        /// <summary>
        /// hàm thực hiện việc định dạng hàm lượng thuốc theo định dạng chuẩn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoHoatchat_LostFocus(object sender, System.EventArgs e)
        {
            //autoHoatchat.Text = Utility.chuanhoachuoi(autoHoatchat.Text);
        }
        private void cmdCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtTEN_BHYT.Text = Utility.sDbnull(txtName.Text);
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
