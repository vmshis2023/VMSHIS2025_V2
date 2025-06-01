using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU;
using VNS.Libs;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_dichvucls_chitiet : Form
    {
        #region "THUOC TINH"

        private readonly Query query = DmucDichvuclsChitiet.CreateQuery();
        public DataRow drServiceDetail;
        public DataTable dtDataServiceDetail = new DataTable();
        private DataTable dtMauQK = new DataTable();
        public GridEX grdlist;
        public GridEX grdlistChitiet;
        private bool m_blnLoaded;
        private DataTable m_dtDichvuCLS = new DataTable();
        private DataTable m_dtqheCamchidinhCLSChungphieu = new DataTable();
        private DataTable m_dtqhemauKQ = new DataTable();
        public action m_enAction = action.Insert;
        private DmucDichvuclsChitiet objDichVuChitiet;
        // private Query query = DmucDichvuclsChitiet.CreateQuery();

        #endregion

        #region "KHOI TAO "
        
        public frm_themmoi_dichvucls_chitiet()
        {
            InitializeComponent();
            THU_VIEN_CHUNG.EnableBHYT(this);
            Utility.SetVisualStyle(this);
            KeyPreview = true;
            cmdExit.Click += cmdExit_Click;
            cmdSave.Click += btnNew_Click;
            txtServiceDetailName.LostFocus += txtServiceDetailName_LostFocus;
            cboDepartment.SelectedIndexChanged += cboDepartment_SelectedIndexChanged;
            chkHienThi.Checked = true;
            txtDonvitinh._OnShowDataV1 += _OnShowDataV1;
            txtPhuongphapthu._OnShowDataV1 += _OnShowDataV1;
            
            chkKiemnghiem.CheckedChanged += chkKiemnghiem_CheckedChanged;
            txtServiceDetailCode.LostFocus += txtServiceDetailCode_LostFocus;
            
           
            txtNhominphoiBHYT._OnShowData += txtNhominphoiBHYT__OnShowData;
            autoPpvocam._OnShowDataV1 += _OnShowDataV1;
            autoLoaipttt._OnShowDataV1 += _OnShowDataV1;
            txtChuyenkhoa._OnShowDataV1 += _OnShowDataV1;
            txtVitrithuchien._OnShowDataV1 += _OnShowDataV1;
            txtLoaiDichvu._OnEnterMe += txtLoaiDichvu__OnEnterMe;
            autoCachthucPTTT._OnEnterMe += autoCachthucPTTT__OnEnterMe;
        }

        void _OnShowDataV1(UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }
        }

        void autoCachthucPTTT__OnEnterMe()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(autoCachthucPTTT.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = autoCachthucPTTT.myCode;
                autoCachthucPTTT.Init();
                autoCachthucPTTT.SetCode(oldCode);
                autoCachthucPTTT.Focus();
            }
        }

        void txtLoaiDichvu__OnEnterMe()
        {
            List<string> lstdanhmuclienquanPttt = THU_VIEN_CHUNG.Laygiatrithamsohethong("DANHMUC_MADVU_HIENTHI_NHAPTHONGTIN_PTTT", "xxx", true).Split(',').ToList<string>();
            autoLoaipttt.Enabled=autoCachthucPTTT.Enabled= lstdanhmuclienquanPttt.Contains(txtLoaiDichvu.MyCode) || txtLoaiDichvu.MyCode=="PTTT";
        }

        private void txtNhominphoiBHYT__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhominphoiBHYT.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhominphoiBHYT.myCode;
                txtNhominphoiBHYT.Init();
                txtNhominphoiBHYT.SetCode(oldCode);
                txtNhominphoiBHYT.Focus();
            }
        }

        private void txtloaipttt__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(autoLoaipttt.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = autoLoaipttt.myCode;
                autoLoaipttt.Init();
                autoLoaipttt.SetCode(oldCode);
                autoLoaipttt.Focus();
            }
        }

        private void txtppvocam__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(autoPpvocam.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = autoPpvocam.myCode;
                autoPpvocam.Init();
                autoPpvocam.SetCode(oldCode);
                autoPpvocam.Focus();
            }
        }

       

      

       

        private void txtServiceDetailCode_LostFocus(object sender, EventArgs e)
        {
            if (Utility.DoTrim(txtMaBhyt.Text) == "")
                txtMaBhyt.Text = txtServiceDetailCode.Text;
        }

        private void chkKiemnghiem_CheckedChanged(object sender, EventArgs e)
        {
            txtsoluongchitieu.Enabled = txtPhuongphapthu.Enabled = txtKihieuDat.Enabled = chkKiemnghiem.Checked;
            if (chkKiemnghiem.Checked) txtsoluongchitieu.Focus();
            else
            {
                txtsoluongchitieu.Clear();
                txtPhuongphapthu.SetCode("-1");
                txtKihieuDat.Clear();
            }
        }

       

        private void txtPhuongphapthu__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtPhuongphapthu.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtPhuongphapthu.myCode;
                txtPhuongphapthu.Init();
                txtPhuongphapthu.SetCode(oldCode);
                txtPhuongphapthu.Focus();
            }
        }

        private void txtDonvitinh__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtDonvitinh.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);
                txtDonvitinh.Focus();
            }
        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            DataTable dtPhong =
                THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1), 1);
            DataBinding.BindDataCombobox(cboPhongthuchien, dtPhong, DmucKhoaphong.Columns.IdKhoaphong,
                DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa phòng", true);
        }

        #endregion

        #region "SU KIEN CUA FORM"

        private void frmServiceDetail_Load(object sender, EventArgs e)
        {
            BindService();
            DataTable dtnhominphoi = new Select().From(DmucChung.Schema)
                .Where(DmucChung.Columns.Loai)
                .IsEqualTo(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_STT_INPHOI", "STT_INPHOIBHYT", true))
                .And(DmucChung.Columns.VietTat).IsEqualTo("2")
                .ExecuteDataSet().Tables[0];
            txtNhominphoiBHYT.Init(dtnhominphoi,
                new List<string> {DmucChung.Columns.Ma, DmucChung.Columns.Ma, DmucChung.Columns.Ten});
            autoCachthucPTTT.Init();
            txtDonvitinh.Init();
            autoCachthucPTTT.Init();
            autoLoaipttt.Init();
            autoPpvocam.Init();
            txtPhuongphapthu.Init();
            m_dtqheCamchidinhCLSChungphieu =
                new Select().From(QheCamchidinhChungphieu.Schema)
                    .Where(QheCamchidinhChungphieu.Columns.Loai)
                    .IsEqualTo(0)
                    .ExecuteDataSet()
                    .Tables[0];
            m_dtqhemauKQ = new Select().From(QheDichvuMauketqua.Schema).ExecuteDataSet().Tables[0];
            m_blnLoaded = true;
            SetControlStatus();
        }

        private void ClearControl()
        {
            foreach (Control control in grpControl.Controls)
            {
                if (control is TextBox && control.Name != txtLoaiDichvu.Name && control.Name != txtDonvitinh.Name && control.Name != txtDichvuCha.Name)
                {
                    ((TextBox)(control)).Clear();
                }
                if (control is EditBox && control.Name != txtLoaiDichvu.Name && control.Name != txtDonvitinh.Name && control.Name != txtDichvuCha.Name)
                {
                    ((EditBox)(control)).Clear();
                }
                m_enAction = action.Insert;
                txtServiceDetailCode.Focus();
            }
        }

        /// <summary>
        ///     HÀM THỰC HIỆN THOÁT FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     THỰC HIỆN GHI THÔNG TIN CỦA DỊCH VỤ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
            switch (m_enAction)
            {
                case action.Insert:
                    Insert();
                    break;
                case action.Update:
                    Update();
                    break;
            }
        }


        /// <summary>
        ///     HÀM THỰC HIỆN PHÍM TẮT CỦA FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmServiceDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) ClearControl();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
        }


        /// <summary>
        ///     HÀM THỰC HIỆN CHỈ CHO NHẬP SỐ VÀO Ô TIỀN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        #endregion

        #region "HAM THUC HIEN HAM CHUNG"

        public int Service_ID = -1;
        private DataTable m_dtKhoaChucNang = new DataTable();

        private void SetControlStatus()
        {
            if (m_enAction == action.Update)
            {
                getData();
            }
            if (m_enAction == action.Insert)
            {
                try
                {
                    txtIntOrder.Value = Utility.DecimaltoDbnull(query.GetMax(DmucDichvuclsChitiet.Columns.SttHthi), 0) +
                                        1;
                    txtID.Text =
                        Utility.sDbnull(
                            Utility.DecimaltoDbnull(query.GetMax(DmucDichvuclsChitiet.Columns.IdDichvu), 0) + 1);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void BindService()
        {
            dtpNgayCongBo.Value = dtpNgayHieuLuc.Value = globalVariables.SysDate;
            dtpNgayKetthuc.Value = dtpNgayCongBo.Value.AddYears(1);
            m_dtDichvuCLS = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
            DataTable m_dtDichvuCLS_new = m_dtDichvuCLS.Clone();
            if (globalVariables.gv_dtQuyenNhanvien_Dmuc.Select(QheNhanvienDanhmuc.Columns.Loai + "= 0").Length <= 0)
                m_dtDichvuCLS_new = m_dtDichvuCLS.Copy();
            else
            {
                foreach (DataRow dr in m_dtDichvuCLS.Rows)
                {
                    if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucDichvucl.Columns.IdLoaidichvu]), "0"))
                    {
                        m_dtDichvuCLS_new.ImportRow(dr);
                    }
                }
            }
            txtLoaiDichvu.Init(m_dtDichvuCLS_new,
                new List<string>
                {
                    DmucDichvucl.Columns.IdDichvu,
                    DmucDichvucl.Columns.MaDichvu,
                    DmucDichvucl.Columns.TenDichvu
                });
            DataTable dtnhombaocao = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBAOCAOCLS", true);
            DataBinding.BindDataCombox(cbonhombaocao, dtnhombaocao, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            m_dtKhoaChucNang = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 1);
            DataBinding.BindDataCombobox(cboDepartment, m_dtKhoaChucNang, DmucKhoaphong.Columns.IdKhoaphong,
                DmucKhoaphong.Columns.TenKhoaphong, "---Chọn---", true);
            DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
            DataBinding.BindDataCombox(cboNhomin, dtNhomin, DmucChung.Columns.Ma, "ma_ten");
            LoadServicesDetails();
            if (File.Exists(Application.StartupPath + "\\CAUHINH\\chkthemmoidichvulientuc.txt"))
            {
                chkThemmoilientuc.Checked =
                    Convert.ToInt16(File.ReadAllText(Application.StartupPath + "\\CAUHINH\\chkthemmoidichvulientuc.txt")) ==
                    1
                        ? true
                        : false;
            }
        }

        private void LoadServicesDetails()
        {
            DataTable dtdata =
                new Select().From(DmucDichvuclsChitiet.Schema)
                    .Where(DmucDichvuclsChitiet.Columns.CoChitiet)
                    .IsEqualTo(1)
                    .ExecuteDataSet()
                    .Tables[0];
            txtDichvuCha.Init(dtdata,
                new List<string>
                {
                    DmucDichvuclsChitiet.Columns.IdChitietdichvu,
                    DmucDichvuclsChitiet.Columns.MaChitietdichvu,
                    DmucDichvuclsChitiet.Columns.TenChitietdichvu
                });
        }

        /// <summary>
        ///     ham thuc hien viec laythong tin cua du lieu
        /// </summary>
        private void getData()
        {
            objDichVuChitiet = DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if (objDichVuChitiet != null)
            {
                txtServiceDetailName.Text = Utility.sDbnull(objDichVuChitiet.TenChitietdichvu, "");
                txtTenBHYT.Text = Utility.sDbnull(objDichVuChitiet.TenChitietdichvuBhyt, "");
                txtServiceDetailCode.Text = Utility.sDbnull(objDichVuChitiet.MaChitietdichvu, "");
                txtMaBhyt.Text = Utility.sDbnull(objDichVuChitiet.MaChitietdichvuBhyt, "");
                txtmaqd.Text = Utility.sDbnull(objDichVuChitiet.MaQd, "");
                txtsttbhyt.Text = Utility.sDbnull(objDichVuChitiet.SttDmbhyt, "");
                txtDongia.Text = Utility.sDbnull(objDichVuChitiet.DonGia, "0");
                txtGiagoc.Text = Utility.sDbnull(objDichVuChitiet.GiaGoc, "0");
                txtGiaBHYT.Text = Utility.sDbnull(objDichVuChitiet.GiaBhyt, "0");
                txtPTDT.Text = Utility.sDbnull(objDichVuChitiet.PhuthuDungtuyen, "0");
                txtPTTT.Text = Utility.sDbnull(objDichVuChitiet.PhuthuTraituyen, "0");
                txtchidan.Text = objDichVuChitiet.ChiDan;
                txtMotathem.Text = Utility.sDbnull(objDichVuChitiet.MotaThem, "");
                txtBTNam.Text = Utility.sDbnull(drServiceDetail[DmucDichvuclsChitiet.Columns.BinhthuongNam], "");
                txtBTNu.Text = Utility.sDbnull(drServiceDetail[DmucDichvuclsChitiet.Columns.BinhthuongNu], "");
                txtIntOrder.Value = Utility.DecimaltoDbnull(objDichVuChitiet.SttHthi, 1);
                chkTutuc.Checked = Utility.Byte2Bool(objDichVuChitiet.TuTuc);
                chkLachiphithem.Checked = Utility.Byte2Bool(objDichVuChitiet.LaChiphithem);
                txtDonvitinh.SetCode(Utility.sDbnull(objDichVuChitiet.MaDonvitinh));
                txtLoaiDichvu.MyID = Utility.sDbnull(objDichVuChitiet.IdDichvu, "-1");
                chkHienThi.Checked = Utility.sDbnull(objDichVuChitiet.HienThi, "0") == "1";
                chkCochitiet.Checked = Utility.Byte2Bool(objDichVuChitiet.CoChitiet);
                chkSingle.Checked = Utility.Byte2Bool(objDichVuChitiet.SingleService);
                chkKiemnghiem.Checked = Utility.Byte2Bool(objDichVuChitiet.LaDvuKiemnghiem);
                chkTrangthai.Checked = Utility.sDbnull(objDichVuChitiet.TrangThai, "0") == "1";
                chkTinhCK.Checked = Utility.Byte2Bool(objDichVuChitiet.TinhChkhau);
                chkTnvchidinh.Checked = Utility.Byte2Bool(objDichVuChitiet.TnvChidinh);
                chkDvuTieuhao.Checked = Utility.Byte2Bool(objDichVuChitiet.LaDvuTieuhao);
                txtLoaiDichvu.SetId(Utility.sDbnull(objDichVuChitiet.IdDichvu, "-1"));
                txtDichvuCha.SetId(Utility.sDbnull(objDichVuChitiet.IdCha, "-1"));
                txtsoluongchitieu.Text = Utility.sDbnull(objDichVuChitiet.SoluongChitieu, "1");
                txtPhuongphapthu.SetCode(objDichVuChitiet.MaPhuongphapthu);
                txtNhominphoiBHYT.SetCode(objDichVuChitiet.NhomInphoiBHYT);
                autoLoaipttt.SetCode(objDichVuChitiet.LoaiPttt);
                autoCachthucPTTT.SetCode(objDichVuChitiet.CachthucPttt);
                autoPpvocam.SetCode(objDichVuChitiet.PpVocam);
                 txttenphieutrakqCDHA.Text = Utility.sDbnull(objDichVuChitiet.MauCanhan);
                txtmadichvu.Text = Utility.sDbnull(objDichVuChitiet.XmlMadichvu);
                txtmachiso.Text = Utility.sDbnull(objDichVuChitiet.XmlMachiso);
                txttenchiso.Text = Utility.sDbnull(objDichVuChitiet.XmlTenchiso);
                txtmamay.Text = Utility.sDbnull(objDichVuChitiet.XmlMamay);
                nmrThoigian.Value = Utility.Int16Dbnull(objDichVuChitiet.SongayChophepChidinhtiep);
                cboNhomin.SelectedIndex = Utility.GetSelectedIndex(cboNhomin, objDichVuChitiet.NhomInClsChitiet);
                
                txtMaCongbo.Text = Utility.sDbnull(objDichVuChitiet.MaCongbo);
                cboNhomChiphiXML.SelectedIndex = Utility.GetSelectedIndex(cboNhomChiphiXML, objDichVuChitiet.NhomChiphi);
                cboKhoaXML.SelectedIndex = Utility.GetSelectedIndex(cboKhoaXML, objDichVuChitiet.MaKhoathuchien);
                txtQuitrinh.Text = Utility.sDbnull(objDichVuChitiet.QuyTrinh);
                txtCanhbao.Text = Utility.sDbnull(objDichVuChitiet.CanhBao);
                txtCSKCBCGKT.Text = Utility.sDbnull(objDichVuChitiet.CskcbCgkt);
                txtCSKCB_CLS.Text = Utility.sDbnull(objDichVuChitiet.CskcbCls);
                txtVitrithuchien.SetCode( Utility.sDbnull(objDichVuChitiet.ViTriThDvkt));
                txtChuyenkhoa.SetCode(Utility.sDbnull(objDichVuChitiet.Chuyenkhoa));
                txtTylenguonkhac.Text = Utility.sDbnull(objDichVuChitiet.TyleTtNguonkhac, "0");
                txtTyleHotro.Text = Utility.sDbnull(objDichVuChitiet.TyleHotro, "0");
                chkThanhtoannguonkhac.Checked = Utility.Byte2Bool(objDichVuChitiet.BhytNguonKhac);
                chkHIV.Checked = Utility.Byte2Bool(objDichVuChitiet.Hiv);
                if (objDichVuChitiet.NgayKetthuc == null || objDichVuChitiet.NgayCongbo==null)
                {
                    dtpNgayCongBo.Value = DateTime.Now;
                }
                else
                {
                    dtpNgayCongBo.Value = Convert.ToDateTime(objDichVuChitiet.NgayCongbo);
                }
                if (objDichVuChitiet.NgayBatdau == null || objDichVuChitiet.NgayBatdau == null)
                {
                    dtpNgayHieuLuc.IsNullDate = true;
                }
                else
                {
                    dtpNgayHieuLuc.Value = Convert.ToDateTime(objDichVuChitiet.NgayBatdau);
                }

                if (objDichVuChitiet.NgayKetthuc == null || objDichVuChitiet.NgayKetthuc == null)
                {
                    dtpNgayKetthuc.IsNullDate = true;
                }
                else
                {
                    dtpNgayKetthuc.Value = Convert.ToDateTime(objDichVuChitiet.NgayKetthuc);
                }
                txtKihieuDat.Text = objDichVuChitiet.KihieuDinhtinhDat;
                if (Utility.Int32Dbnull(objDichVuChitiet.IdKhoaThuchien, -1) > 0)
                {
                    cboDepartment.SelectedIndex = Utility.GetSelectedIndex(cboDepartment,
                        objDichVuChitiet.IdKhoaThuchien.Value.ToString());
                }
                if (Utility.Int32Dbnull(objDichVuChitiet.IdPhongThuchien, -1) > 0)
                {
                    cboPhongthuchien.SelectedIndex = Utility.GetSelectedIndex(cboPhongthuchien,
                        objDichVuChitiet.IdPhongThuchien.Value.ToString());
                }
                cbonhombaocao.SelectedIndex = Utility.GetSelectedIndex(cbonhombaocao, objDichVuChitiet.NhomBaocao);
                
                
            }
        }

      

       

        /// <summary>
        ///     HAM THUC HIEN KIEM TRA XEM CO DU TIEU CHUAN DE EP THEM CSDL
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            SqlQuery q;
            Utility.SetMsg(lblMsg, "", true);
            if (string.IsNullOrEmpty(txtServiceDetailCode.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập mã dịch vụ", true);
                txtServiceDetailCode.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtLoaiDichvu.MyID, "-1") <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn Dịch vụ", true);
                txtLoaiDichvu.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtMaBhyt.Text))
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập mã theo QĐ 29", true);
            //    txtMaBhyt.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtServiceDetailName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên dịch vụ", true);
                txtServiceDetailName.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtTenBHYT.Text))
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập tên dịch vụ theo QĐ 29 ", true);
            //    txtTenBHYT.Focus();
            //    return false;
            //}
            if (m_enAction == action.Insert)
            {
                q = new Select().From(DmucDichvuclsChitiet.Schema)
                    .Where(DmucDichvuclsChitiet.Columns.MaChitietdichvu).IsEqualTo(Utility.DoTrim(txtServiceDetailCode.Text));
                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có mã như vậy. Mời bạn kiểm tra lại", true);
                    txtServiceDetailCode.Focus();
                    return false;
                }
                //q = new Select().From(DmucDichvuclsChitiet.Schema)
                //    .Where(DmucDichvuclsChitiet.Columns.MaChitietdichvuBhyt).IsEqualTo(Utility.DoTrim(txtMaBhyt.Text));
                //if (q.GetRecordCount() > 0)
                //{
                //    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Mã QĐ 29 như vậy. Mời bạn kiểm tra lại", true);
                //    txtMaBhyt.Focus();
                //    return false;
                //}

                q = new Select().From(DmucDichvuclsChitiet.Schema)
                    .Where(DmucDichvuclsChitiet.Columns.TenChitietdichvu).IsEqualTo(Utility.DoTrim(txtServiceDetailName.Text));
                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có tên như vậy. Mời bạn kiểm tra lại", true);
                    txtServiceDetailName.Focus();
                    return false;
                }
                //q = new Select().From(DmucDichvuclsChitiet.Schema)
                //    .Where(DmucDichvuclsChitiet.Columns.TenChitietdichvuBhyt).IsEqualTo(Utility.DoTrim(txtTenBHYT.Text));
                //if (q.GetRecordCount() > 0)
                //{
                //    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Tên QĐ 29 như vậy. Mời bạn kiểm tra lại", true);
                //    txtTenBHYT.Focus();
                //    return false;
                //}
            }
            if (m_enAction == action.Update)
            {
                q = new Select().From(DmucDichvuclsChitiet.Schema)
                   .Where(DmucDichvuclsChitiet.Columns.MaChitietdichvu).IsEqualTo(Utility.DoTrim(txtServiceDetailCode.Text)).And(DmucDichvuclsChitiet.Columns.IdChitietdichvu).
                   IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Mã như vậy. Mời bạn kiểm tra lại", true);
                    txtServiceDetailCode.Focus();
                    return false;
                }

                //q = new Select().From(DmucDichvuclsChitiet.Schema)
                //  .Where(DmucDichvuclsChitiet.Columns.MaChitietdichvuBhyt).IsEqualTo(Utility.DoTrim(txtMaBhyt.Text)).And(DmucDichvuclsChitiet.Columns.IdChitietdichvu).
                //  IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

                //if (q.GetRecordCount() > 0)
                //{
                //    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Mã QĐ 29 như vậy. Mời bạn kiểm tra lại", true);
                //    txtMaBhyt.Focus();
                //    return false;
                //}

                q = new Select().From(DmucDichvuclsChitiet.Schema)
                    .Where(DmucDichvuclsChitiet.Columns.TenChitietdichvu).IsEqualTo(Utility.DoTrim(txtServiceDetailName.Text)).And(DmucDichvuclsChitiet.Columns.IdChitietdichvu).
                    IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có tên như vậy. Mời bạn kiểm tra lại", true);
                    txtServiceDetailName.Focus();
                    return false;
                }
                //q = new Select().From(DmucDichvuclsChitiet.Schema)
                //   .Where(DmucDichvuclsChitiet.Columns.TenChitietdichvuBhyt).IsEqualTo(Utility.DoTrim(txtTenBHYT.Text)).And(DmucDichvuclsChitiet.Columns.IdChitietdichvu).
                //   IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

                //if (q.GetRecordCount() > 0)
                //{
                //    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Tên QĐ 29 như vậy. Mời bạn kiểm tra lại", true);
                //    txtTenBHYT.Focus();
                //    return false;
                //}
            }

           

            return true;
        }

        private void Insert()
        {
            int actionResult = LuuThongtin();
            if (actionResult > -1)
            {
                Utility.SetMsg(lblMsg, "Thêm thông tin thành công", false);
                try
                {
                    ProcessData(actionResult);
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi trong quá trình thêm mới thông tin");
                }
            }
            else
            {
                Utility.SetMsg(lblMsg, "Bạn thực hiện không thành công", true);
            }
        }

        private int LuuThongtin()
        {
            try
            {
                objDichVuChitiet = new DmucDichvuclsChitiet();
                objDichVuChitiet.IsNew = true;
                objDichVuChitiet.DonGia = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                objDichVuChitiet.GiaGoc = Utility.DecimaltoDbnull(txtGiagoc.Text, 0);
                objDichVuChitiet.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                objDichVuChitiet.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                objDichVuChitiet.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                objDichVuChitiet.IdChitietdichvu = Utility.Int32Dbnull(txtID.Text, -1);
                objDichVuChitiet.IdDichvu = Utility.Int16Dbnull(txtLoaiDichvu.MyID, -1);
                objDichVuChitiet.TenChitietdichvu = Utility.DoTrim(txtServiceDetailName.Text);
                objDichVuChitiet.TenChitietdichvuBhyt = Utility.DoTrim(txtTenBHYT.Text);
                objDichVuChitiet.MaChitietdichvu = Utility.sDbnull(txtServiceDetailCode.Text, "");
                objDichVuChitiet.MaChitietdichvuBhyt = Utility.sDbnull(txtMaBhyt.Text, "");
                objDichVuChitiet.SttHthi = Utility.Int32Dbnull(txtIntOrder.Value);
                objDichVuChitiet.MaDonvitinh = txtDonvitinh.myCode;
                objDichVuChitiet.LaDvuTieuhao = Utility.Bool2byte(chkDvuTieuhao.Checked);
                objDichVuChitiet.TinhChkhau =Utility.Bool2byte( chkTinhCK.Checked);
                objDichVuChitiet.NgayTao = globalVariables.SysDate;
                objDichVuChitiet.NguoiTao = globalVariables.UserName;
                objDichVuChitiet.MotaThem = Utility.DoTrim(txtMotathem.Text);
                objDichVuChitiet.BinhthuongNam = Utility.DoTrim(txtBTNam.Text);
                objDichVuChitiet.BinhthuongNu = Utility.DoTrim(txtBTNu.Text);
                objDichVuChitiet.ChiDan = Utility.DoTrim(txtchidan.Text);
                objDichVuChitiet.MauCanhan = Utility.DoTrim(txttenphieutrakqCDHA.Text);
                objDichVuChitiet.TrangThai = (byte) (chkTrangthai.Checked ? 1 : 0);
                objDichVuChitiet.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
                objDichVuChitiet.TnvChidinh = Utility.Bool2byte(chkTnvchidinh.Checked);
                objDichVuChitiet.LaDvuKiemnghiem = Utility.Bool2byte(chkKiemnghiem.Checked);
                objDichVuChitiet.LaChiphithem = Utility.Bool2byte(chkLachiphithem.Checked);
                objDichVuChitiet.IdCha = Utility.Int32Dbnull(txtDichvuCha.MyID, -1);
                objDichVuChitiet.CoChitiet = Utility.Bool2byte(chkCochitiet.Checked);
                objDichVuChitiet.SingleService = Utility.Bool2byte(chkSingle.Checked);
                objDichVuChitiet.NhomBaocao = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                objDichVuChitiet.IdKhoaThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVuChitiet.IdPhongThuchien = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
                objDichVuChitiet.KihieuDinhtinhDat = Utility.sDbnull(txtKihieuDat.Text);
                objDichVuChitiet.MaPhuongphapthu = txtPhuongphapthu.myCode;
                objDichVuChitiet.NhomInphoiBHYT = txtNhominphoiBHYT.myCode;
                objDichVuChitiet.NgayCongbo = dtpNgayCongBo.Value;
                objDichVuChitiet.NgayBatdau = dtpNgayHieuLuc.Value;
                objDichVuChitiet.NgayKetthuc = dtpNgayKetthuc.Value;
                objDichVuChitiet.SoluongChitieu = (int) Utility.DecimaltoDbnull(txtsoluongchitieu.Text, 1);
                objDichVuChitiet.XmlMadichvu = Utility.sDbnull(txtmadichvu.Text);
                objDichVuChitiet.XmlMachiso = Utility.sDbnull(txtmachiso.Text);
                objDichVuChitiet.XmlTenchiso = Utility.sDbnull(txttenchiso.Text);
                objDichVuChitiet.XmlMamay = Utility.sDbnull(txtmamay.Text);
                objDichVuChitiet.SongayChophepChidinhtiep =Utility.ByteDbnull( nmrThoigian.Value,0);
                objDichVuChitiet.NhomInClsChitiet = Utility.sDbnull(cboNhomin.SelectedValue, "-1");

                objDichVuChitiet.MaCongbo=  Utility.sDbnull(txtMaCongbo.Text);
                objDichVuChitiet.NhomChiphi=Utility.sDbnull(cboNhomChiphiXML.SelectedValue, "-1");
                objDichVuChitiet.MaKhoathuchien = Utility.sDbnull(cboKhoaXML.SelectedValue, "-1");
                objDichVuChitiet.QuyTrinh = Utility.sDbnull(txtQuitrinh.Text);
                objDichVuChitiet.CanhBao = Utility.sDbnull(txtCanhbao.Text);
                objDichVuChitiet.CskcbCgkt = Utility.sDbnull(txtCSKCBCGKT.Text);
                objDichVuChitiet.CskcbCls = Utility.sDbnull(txtCSKCB_CLS.Text);
                objDichVuChitiet.ViTriThDvkt = Utility.Int32Dbnull(txtVitrithuchien.myCode,-1);
                objDichVuChitiet.Chuyenkhoa = Utility.sDbnull(txtChuyenkhoa.myCode);
                objDichVuChitiet.TyleTtNguonkhac = Utility.DecimaltoDbnull(txtTylenguonkhac.Text, 0);
                objDichVuChitiet.TyleHotro = Utility.DecimaltoDbnull(txtTyleHotro.Text, 0);
                objDichVuChitiet.BhytNguonKhac = Utility.Bool2byte(chkThanhtoannguonkhac.Checked);
                chkHIV.Checked = Utility.Byte2Bool(objDichVuChitiet.Hiv);

                objDichVuChitiet.IsNew = true;
                dmucDichvuCLS_busrule.Insert(objDichVuChitiet,null,
                   null);
                return objDichVuChitiet.IdChitietdichvu;
            }
            catch
            {
                return -1;
            }
        }

       

       
        private void ProcessData(int serviceDetailId)
        {
            DataRow dr = dtDataServiceDetail.NewRow();
            dr[DmucDichvuclsChitiet.Columns.TenChitietdichvuBhyt] = Utility.DoTrim(txtTenBHYT.Text);
            dr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(txtServiceDetailName.Text, "");
            dr[DmucDichvuclsChitiet.Columns.MaChitietdichvu] = Utility.sDbnull(txtServiceDetailCode.Text, "");
            dr[DmucDichvuclsChitiet.Columns.MaChitietdichvuBhyt] = Utility.sDbnull(txtMaBhyt.Text, "");
            dr[DmucDichvuclsChitiet.Columns.MaQd] = Utility.sDbnull(txtmaqd.Text, "");
            dr[DmucDichvuclsChitiet.Columns.SttDmbhyt] = Utility.sDbnull(txtsttbhyt.Text, "");
            dr[DmucDichvuclsChitiet.Columns.NgayTao] = globalVariables.SysDate;
            dr[DmucDichvuclsChitiet.Columns.NguoiTao] = globalVariables.UserName;

            dr[DmucDichvuclsChitiet.Columns.GiaGoc] = Utility.DecimaltoDbnull(txtGiagoc.Text, 0);
            dr[DmucDichvuclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
            dr[DmucDichvuclsChitiet.Columns.GiaBhyt] = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
            dr[DmucDichvuclsChitiet.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
            dr[DmucDichvuclsChitiet.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(txtPTTT.Text, 0);

            dr[DmucDichvuclsChitiet.Columns.SttHthi] = txtIntOrder.Value;
            dr[DmucDichvuclsChitiet.Columns.IdDichvu] = Utility.Int16Dbnull(txtLoaiDichvu.MyID, -1);
            dr[DmucDichvuclsChitiet.Columns.NgayTao] = globalVariables.SysDate;
            dr[DmucDichvuclsChitiet.Columns.NguoiTao] = globalVariables.UserName;
            dr[DmucDichvuclsChitiet.Columns.IdChitietdichvu] = serviceDetailId;
            dr[DmucDichvuclsChitiet.Columns.TuTuc] = Utility.Bool2byte(chkTutuc.Checked);
            dr[DmucDichvuclsChitiet.Columns.LaChiphithem] = Utility.Bool2byte(chkLachiphithem.Checked);
            dr[DmucDichvuclsChitiet.Columns.CoChitiet] = Utility.Bool2byte(chkCochitiet.Checked);
            dr[DmucDichvuclsChitiet.Columns.SingleService] = Utility.Bool2byte(chkSingle.Checked);
            dr[DmucDichvuclsChitiet.Columns.IdCha] = Utility.Int32Dbnull(txtDichvuCha.MyID, -1);
            dr[DmucDichvuclsChitiet.Columns.HienThi] = chkHienThi.Checked ? 1 : 0;
            dr[DmucDichvuclsChitiet.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
            dr[DmucDichvuclsChitiet.Columns.TinhChkhau] = chkTinhCK.Checked;
            dr[DmucDichvucl.Columns.TenDichvu] = txtLoaiDichvu.Text;
            dr[DmucDichvuclsChitiet.Columns.ChiDan] = Utility.DoTrim(txtchidan.Text);
            dr[DmucDichvuclsChitiet.Columns.MaDonvitinh] = txtDonvitinh.myCode;
            dr["ten_donvitinh"] = txtDonvitinh.Text;
            dr[DmucDichvuclsChitiet.Columns.BinhthuongNam] = Utility.DoTrim(txtBTNam.Text);
            dr[DmucDichvuclsChitiet.Columns.BinhthuongNu] = Utility.DoTrim(txtBTNu.Text);
            dr[DmucDichvucl.Columns.IdKhoaThuchien] = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
            dr[DmucDichvucl.Columns.IdPhongThuchien] = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
            dr[DmucDichvuclsChitiet.Columns.NhomInphoiBHYT] = txtNhominphoiBHYT.myCode;
            dr[DmucDichvuclsChitiet.Columns.LoaiPttt] = autoLoaipttt.myCode;
            dr[DmucDichvuclsChitiet.Columns.PpVocam] = autoPpvocam.myCode;
            dr[DmucDichvuclsChitiet.Columns.NgayCongbo] = dtpNgayCongBo.Value;
            dr[DmucDichvuclsChitiet.Columns.NgayBatdau] = dtpNgayHieuLuc.Value;
            dr[DmucDichvuclsChitiet.Columns.NgayKetthuc] = dtpNgayKetthuc.Value;
            dr[DmucDichvuclsChitiet.Columns.KihieuDinhtinhDat] = Utility.sDbnull(txtKihieuDat.Text);
            dr[DmucDichvuclsChitiet.Columns.MaPhuongphapthu] = txtPhuongphapthu.myCode;
            dr[DmucDichvuclsChitiet.Columns.SoluongChitieu] = (int) Utility.DecimaltoDbnull(txtsoluongchitieu.Text, 1);
            dr[DmucDichvuclsChitiet.Columns.LaDvuKiemnghiem] = Utility.Bool2byte(chkKiemnghiem.Checked);
            dr[DmucDichvuclsChitiet.Columns.XmlMadichvu] = Utility.sDbnull(txtmadichvu.Text);
            dr[DmucDichvuclsChitiet.Columns.XmlMachiso] = Utility.sDbnull(txtmachiso.Text);
            dr[DmucDichvuclsChitiet.Columns.XmlTenchiso] = Utility.sDbnull(txttenchiso.Text);
            dr[DmucDichvuclsChitiet.Columns.XmlMamay] = Utility.sDbnull(txtmamay.Text);
            dr[DmucDichvuclsChitiet.Columns.SongayChophepChidinhtiep] = Utility.ByteDbnull(nmrThoigian.Value, 0);
            if (cboDepartment.SelectedIndex > 0)
                dr[VDmucDichvucl.Columns.TenKhoaThuchien] = Utility.sDbnull(cboDepartment.Text);
            else
                dr[VDmucDichvucl.Columns.TenKhoaThuchien] = "";
            if (cboPhongthuchien.SelectedIndex > 0)
                dr[VDmucDichvucl.Columns.TenPhongThuchien] = Utility.sDbnull(cboPhongthuchien.Text);
            else
                dr[VDmucDichvucl.Columns.TenPhongThuchien] = "";
            dr[VDmucDichvucl.Columns.MaDichvu] = txtLoaiDichvu.MyCode;
            dr[DmucDichvuclsChitiet.Columns.NhomBaocao] = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
            dr[DmucDichvuclsChitiet.Columns.NhomInClsChitiet] = Utility.sDbnull(cboNhomin.SelectedValue, "-1");
            dr["ten_nhombaocao_chitiet"] = Utility.sDbnull(cbonhombaocao.Text, "");
            dr["ten_nhom_in_cls_chitiet"] = Utility.sDbnull(cboNhomin.Text, "");
            dtDataServiceDetail.Rows.Add(dr);
            dtDataServiceDetail.AcceptChanges();
            if (Utility.Int32Dbnull(txtDichvuCha.MyID, -1) <= 0)
                Utility.GotoNewRowJanus(grdlist, DmucDichvuclsChitiet.Columns.IdChitietdichvu,
                    serviceDetailId.ToString());
            else
                Utility.GotoNewRowJanus(grdlistChitiet, DmucDichvuclsChitiet.Columns.IdChitietdichvu,
                    serviceDetailId.ToString());
            if (!chkThemmoilientuc.Checked) Close();
            else
            {
                m_enAction = action.Insert;
                if (chkCochitiet.Checked) LoadServicesDetails();
                chkCochitiet.Checked = false;
                chkTutuc.Checked = false;
                chkHienThi.Checked = true;
                chkTrangthai.Checked = true;
                ClearControl();
                SetControlStatus();
            }
        }

        private void Update()
        {
            try
            {
                objDichVuChitiet = DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtID.Text, 0));
                objDichVuChitiet.MarkOld();
                objDichVuChitiet.GiaGoc = Utility.DecimaltoDbnull(txtGiagoc.Text, 0);
                objDichVuChitiet.DonGia = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                objDichVuChitiet.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                objDichVuChitiet.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                objDichVuChitiet.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                objDichVuChitiet.LaDvuKiemnghiem = Utility.Bool2byte(chkKiemnghiem.Checked);
                objDichVuChitiet.IdChitietdichvu = Utility.Int32Dbnull(txtID.Text, -1);
                objDichVuChitiet.IdDichvu = Utility.Int16Dbnull(txtLoaiDichvu.MyID, -1);
                objDichVuChitiet.TenChitietdichvu = Utility.DoTrim(txtServiceDetailName.Text);
                objDichVuChitiet.TenChitietdichvuBhyt = Utility.DoTrim(txtTenBHYT.Text);
                objDichVuChitiet.MaChitietdichvu = Utility.sDbnull(txtServiceDetailCode.Text, "");
                objDichVuChitiet.MaChitietdichvuBhyt = Utility.sDbnull(txtMaBhyt.Text, "");
                objDichVuChitiet.MaQd = Utility.sDbnull(txtmaqd.Text, "");
                objDichVuChitiet.SttDmbhyt = Utility.sDbnull(txtsttbhyt.Text, "");
                objDichVuChitiet.TinhChkhau =Utility.Bool2byte( chkTinhCK.Checked);
                objDichVuChitiet.SttHthi = Utility.Int32Dbnull(txtIntOrder.Value);
                objDichVuChitiet.MaDonvitinh = txtDonvitinh.myCode;
                //objDichVuChitiet.NgayTao = globalVariables.SysDate;
                //objDichVuChitiet.NguoiTao = globalVariables.UserName;
                objDichVuChitiet.NgaySua = globalVariables.SysDate;
                objDichVuChitiet.NguoiSua = globalVariables.UserName;
                objDichVuChitiet.MotaThem = Utility.DoTrim(txtMotathem.Text);
                objDichVuChitiet.BinhthuongNam = Utility.DoTrim(txtBTNam.Text);
                objDichVuChitiet.BinhthuongNu = Utility.DoTrim(txtBTNu.Text);
                objDichVuChitiet.ChiDan = Utility.DoTrim(txtchidan.Text);
                objDichVuChitiet.TrangThai = (byte) (chkTrangthai.Checked ? 1 : 0);
                objDichVuChitiet.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
                objDichVuChitiet.TnvChidinh = Utility.Bool2byte(chkTnvchidinh.Checked);
                objDichVuChitiet.LaChiphithem = Utility.Bool2byte(chkLachiphithem.Checked);
                objDichVuChitiet.IdCha = Utility.Int32Dbnull(txtDichvuCha.MyID, -1);
                objDichVuChitiet.CoChitiet = Utility.Bool2byte(chkCochitiet.Checked);
                objDichVuChitiet.SingleService = Utility.Bool2byte(chkSingle.Checked);
                objDichVuChitiet.NhomBaocao = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                objDichVuChitiet.IdKhoaThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVuChitiet.IdPhongThuchien = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
                objDichVuChitiet.KihieuDinhtinhDat = Utility.sDbnull(txtKihieuDat.Text);
                objDichVuChitiet.MaPhuongphapthu = txtPhuongphapthu.myCode;
                objDichVuChitiet.NhomInphoiBHYT = txtNhominphoiBHYT.myCode;
                objDichVuChitiet.LoaiPttt = autoLoaipttt.myCode;
                objDichVuChitiet.CachthucPttt = autoCachthucPTTT.myCode;
                objDichVuChitiet.PpVocam = autoPpvocam.myCode;
                objDichVuChitiet.NgayCongbo = dtpNgayCongBo.Value;
                objDichVuChitiet.NgayBatdau = dtpNgayHieuLuc.Value;
                objDichVuChitiet.NgayKetthuc = dtpNgayKetthuc.Value;
                objDichVuChitiet.SoluongChitieu = (int) Utility.DecimaltoDbnull(txtsoluongchitieu.Text, 1);
                objDichVuChitiet.XmlMadichvu = Utility.sDbnull(txtmadichvu.Text);
                objDichVuChitiet.XmlMachiso = Utility.sDbnull(txtmachiso.Text);
                objDichVuChitiet.XmlTenchiso = Utility.sDbnull(txttenchiso.Text);
                objDichVuChitiet.XmlMamay = Utility.sDbnull(txtmamay.Text);
                objDichVuChitiet.SongayChophepChidinhtiep = Utility.ByteDbnull(nmrThoigian.Value, 0);
                objDichVuChitiet.NhomInClsChitiet = Utility.sDbnull(cboNhomin.SelectedValue, "-1");
                objDichVuChitiet.MauCanhan = Utility.DoTrim(txttenphieutrakqCDHA.Text);
                objDichVuChitiet.MaCongbo = Utility.sDbnull(txtMaCongbo.Text);
                objDichVuChitiet.NhomChiphi = Utility.sDbnull(cboNhomChiphiXML.SelectedValue, "-1");
                objDichVuChitiet.MaKhoathuchien = Utility.sDbnull(cboKhoaXML.SelectedValue, "-1");
                objDichVuChitiet.QuyTrinh = Utility.sDbnull(txtQuitrinh.Text);
                objDichVuChitiet.CanhBao = Utility.sDbnull(txtCanhbao.Text);
                objDichVuChitiet.CskcbCgkt = Utility.sDbnull(txtCSKCBCGKT.Text);
                objDichVuChitiet.CskcbCls = Utility.sDbnull(txtCSKCB_CLS.Text);
                objDichVuChitiet.ViTriThDvkt = Utility.Int32Dbnull(txtVitrithuchien.myCode, -1);
                objDichVuChitiet.Chuyenkhoa = Utility.sDbnull(txtChuyenkhoa.myCode);
                objDichVuChitiet.TyleTtNguonkhac = Utility.DecimaltoDbnull(txtTylenguonkhac.Text, 0);
                objDichVuChitiet.TyleHotro = Utility.DecimaltoDbnull(txtTyleHotro.Text, 0);
                objDichVuChitiet.BhytNguonKhac = Utility.Bool2byte(chkThanhtoannguonkhac.Checked);
                chkHIV.Checked = Utility.Byte2Bool(objDichVuChitiet.Hiv);

                objDichVuChitiet.IsNew = false;
                objDichVuChitiet.MarkOld();
                dmucDichvuCLS_busrule.Insert(objDichVuChitiet, null,
                    null);

                //Tạm bỏ 29/08/2015
                //new Update(KcbThanhtoanChitiet.Schema)
                //    .Set(KcbThanhtoanChitiet.Columns.IdDichvu).EqualTo(Utility.Int16Dbnull(txtLoaiDichvu.MyID, -1))
                //    .Set(KcbThanhtoanChitiet.Columns.DonviTinh).EqualTo(txtDonvitinh.Text)
                //    .Set(KcbThanhtoanChitiet.Columns.TenChitietdichvu).EqualTo(Utility.sDbnull(txtServiceDetailName.Text, ""))
                //    .Where(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan).IsEqualTo(2)
                //    .And(KcbThanhtoanChitiet.Columns.IdDichvu).IsEqualTo(Utility.Int32Dbnull(txtID.Text, -1))
                //    .Execute();


                ProcessData1();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void ProcessData1()
        {
            try
            {
                foreach (DataRow dr in dtDataServiceDetail.Rows)
                {
                    if (Utility.Int32Dbnull(dr[DmucDichvuclsChitiet.Columns.IdChitietdichvu], -1) ==
                        Utility.Int32Dbnull(txtID.Text, 0))
                    {
                        dr[DmucDichvuclsChitiet.Columns.TenChitietdichvuBhyt] = Utility.DoTrim(txtTenBHYT.Text);
                        dr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(txtServiceDetailName.Text,
                            "");
                        dr[DmucDichvuclsChitiet.Columns.MaChitietdichvu] = Utility.sDbnull(txtServiceDetailCode.Text, "");
                        dr[DmucDichvuclsChitiet.Columns.MaChitietdichvuBhyt] = Utility.sDbnull(txtMaBhyt.Text, "");
                        dr[DmucDichvuclsChitiet.Columns.MaQd] = Utility.sDbnull(txtmaqd.Text, "");
                        dr[DmucDichvuclsChitiet.Columns.SttDmbhyt] = Utility.sDbnull(txtsttbhyt.Text, "");
                        dr[DmucDichvuclsChitiet.Columns.NgaySua] = globalVariables.SysDate;
                        dr[DmucDichvuclsChitiet.Columns.NguoiSua] = globalVariables.UserName;
                        dr[DmucDichvuclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                        dr[DmucDichvuclsChitiet.Columns.GiaGoc] = Utility.DecimaltoDbnull(txtGiagoc.Text, 0);
                        dr[DmucDichvuclsChitiet.Columns.GiaBhyt] = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                        dr[DmucDichvuclsChitiet.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                        dr[DmucDichvuclsChitiet.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                        dr[DmucDichvuclsChitiet.Columns.TuTuc] = Utility.Bool2byte(chkTutuc.Checked);
                        dr[DmucDichvuclsChitiet.Columns.LaChiphithem] = Utility.Bool2byte(chkLachiphithem.Checked);
                        dr[DmucDichvuclsChitiet.Columns.CoChitiet] = Utility.Bool2byte(chkCochitiet.Checked);
                        dr[DmucDichvuclsChitiet.Columns.SingleService] = Utility.Bool2byte(chkSingle.Checked);
                        dr[DmucDichvuclsChitiet.Columns.IdCha] = Utility.Int32Dbnull(txtDichvuCha.MyID, -1);
                        dr[DmucDichvuclsChitiet.Columns.SttHthi] = txtIntOrder.Value;
                        dr[DmucDichvuclsChitiet.Columns.MotaThem] = Utility.DoTrim(txtMotathem.Text);
                        dr[DmucDichvuclsChitiet.Columns.IdDichvu] = Utility.Int16Dbnull(txtLoaiDichvu.MyID, -1);
                        dr[DmucDichvuclsChitiet.Columns.BinhthuongNam] = Utility.DoTrim(txtBTNam.Text);
                        dr[DmucDichvuclsChitiet.Columns.BinhthuongNu] = Utility.DoTrim(txtBTNu.Text);
                        dr[DmucDichvuclsChitiet.Columns.ChiDan] = Utility.DoTrim(txtchidan.Text);
                        dr[DmucDichvuclsChitiet.Columns.IdKhoaThuchien] =
                            Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                        dr[DmucDichvuclsChitiet.Columns.IdPhongThuchien] =
                            Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
                        dr[DmucDichvuclsChitiet.Columns.KihieuDinhtinhDat] = Utility.sDbnull(txtKihieuDat.Text);
                        dr[DmucDichvuclsChitiet.Columns.MaPhuongphapthu] = txtPhuongphapthu.myCode;
                        dr[DmucDichvuclsChitiet.Columns.NhomInphoiBHYT] = txtNhominphoiBHYT.myCode;
                        dr[DmucDichvuclsChitiet.Columns.LoaiPttt] = autoLoaipttt.myCode;
                        dr[DmucDichvuclsChitiet.Columns.PpVocam] = autoPpvocam.myCode;
                        dr[DmucDichvuclsChitiet.Columns.SoluongChitieu] = (int) Utility.DecimaltoDbnull(txtsoluongchitieu.Text, 1);
                        dr[DmucDichvuclsChitiet.Columns.LaDvuKiemnghiem] = Utility.Bool2byte(chkKiemnghiem.Checked);
                        dr[VDmucDichvucl.Columns.MaDichvu] = txtLoaiDichvu.MyCode;
                        if (cboDepartment.SelectedIndex > 0)
                            dr[VDmucDichvuclsChitiet.Columns.TenKhoaThuchien] = Utility.sDbnull(cboDepartment.Text);
                        else
                            dr[VDmucDichvuclsChitiet.Columns.TenKhoaThuchien] = "";
                        if (cboPhongthuchien.SelectedIndex > 0)
                            dr[VDmucDichvuclsChitiet.Columns.TenPhongThuchien] = Utility.sDbnull(cboPhongthuchien.Text);
                        else
                            dr[VDmucDichvucl.Columns.TenPhongThuchien] = "";
                        dr[DmucDichvuclsChitiet.Columns.HienThi] = chkHienThi.Checked ? 1 : 0;
                        dr[DmucDichvuclsChitiet.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
                        dr[DmucDichvuclsChitiet.Columns.TinhChkhau] = chkTinhCK.Checked;
                        dr[DmucDichvucl.Columns.TenDichvu] = txtLoaiDichvu.Text;
                        dr[DmucDichvuclsChitiet.Columns.NhomBaocao] = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                        dr["ten_nhombaocao_chitiet"] = Utility.sDbnull(cbonhombaocao.Text, "");
                        dr[DmucDichvuclsChitiet.Columns.MaDonvitinh] = txtDonvitinh.myCode;
                        dr[DmucDichvuclsChitiet.Columns.SongayChophepChidinhtiep] =Utility.ByteDbnull( nmrThoigian.Value,0);
                        dr["ten_donvitinh"] = txtDonvitinh.Text;
                        dr[DmucDichvuclsChitiet.Columns.NhomInClsChitiet] = Utility.sDbnull(cboNhomin.SelectedValue, "-1");
                        dr["ten_nhom_in_cls_chitiet"] = Utility.sDbnull(cboNhomin.Text, "");
                        break;
                    }
                }
                dtDataServiceDetail.AcceptChanges();
                if (Utility.Int32Dbnull(txtDichvuCha.MyID, -1) <= 0)
                    Utility.GotoNewRowJanus(grdlist, DmucDichvuclsChitiet.Columns.IdChitietdichvu, txtID.Text);
                else
                    Utility.GotoNewRowJanus(grdlistChitiet, DmucDichvuclsChitiet.Columns.IdChitietdichvu, txtID.Text);
            }
            catch (Exception)
            {
                // throw;
            }

            Close();
        }

        #endregion

        private void txtServiceDetailName_LostFocus(object sender, EventArgs e)
        {
            if (Utility.DoTrim(txtTenBHYT.Text) == "")
                txtTenBHYT.Text = txtServiceDetailName.Text;
        }


        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearControl();
        }

        private void chkThemmoilientuc_CheckedChanged(object sender, EventArgs e)
        {
            File.WriteAllText(Application.StartupPath + "\\CAUHINH\\chkthemmoidichvulientuc.txt",
                chkThemmoilientuc.Checked ? "1" : "0");
        }

        private void txtMaBhyt_TextChanged(object sender, EventArgs e)
        {
            txtmadichvu.Text = txtMaBhyt.Text;
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {

        }
        List<string> lstNames = new List<string>();
        List<string> lstPrices = new List<string>();
        private void cmdBrowFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog _file = new OpenFileDialog();
            if (_file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //List<string> lstVal = File.ReadAllText(_file.FileName).Split('@').ToList<string>();
                //lstNames = lstVal[0].Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList<string>();
                //lstPrices = lstVal[1].Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList<string>();
                //if (lstNames.Count == lstPrices.Count)
                //{
                //    for (int i = 0; i <= lstNames.Count - 1; i++)
                //    {
                //        txtServiceDetailName.Text = lstNames[i];
                //        txtTenBHYT.Text = txtServiceDetailName.Text;
                //        txtDongia.Text = lstPrices[i];
                //        txtGiagoc.Text = txtDongia.Text;
                //        txtServiceDetailCode.Text = txtLoaiDichvu.MyCode + i.ToString();
                //        txtMaBhyt.Text = txtServiceDetailCode.Text;
                //        btnNew.PerformClick();

                //    }
                //}
                //else
                //{
                //    Utility.ShowMsg("Số lượng phần tử khác nhau. Kiểm tra lại");
                //}
                Int32 maxSTT = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("DONVITINH").ExecuteDataSet().Tables[0].AsEnumerable().Select(t => t.Field<int>("STT_HTHI")).Max();
                List<string> lstLine = File.ReadAllLines(_file.FileName).ToList<string>();
                foreach (string line in lstLine)
                {
                    List<string> lstValues = line.Split('@').ToList<string>();

                    txtServiceDetailName.Text = lstValues[1];
                    txtTenBHYT.Text = txtServiceDetailName.Text;
                    txtDongia.Text = "0";
                    txtGiagoc.Text = "0";
                    txtServiceDetailCode.Text = txtLoaiDichvu.MyCode + lstValues[0];
                    txtMaBhyt.Text = txtServiceDetailCode.Text;
                    txtIntOrder.Value = Utility.Int32Dbnull(lstValues[2], 1);
                    string _DVT = Utility.DoTrim(lstValues[3]);
                    maxSTT += 1;
                    checkDVT(_DVT, maxSTT);
                    txtDonvitinh.SetCode(_DVT);
                    txtBTNam.Text = lstValues[4];
                    txtBTNu.Text = lstValues[5];
                    cmdSave.PerformClick();
                }
            }
        }
        bool checkDVT(string _DVT,int STT)
        {
            DmucChung _item = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("DONVITINH").And(DmucChung.Columns.Ma).IsEqualTo(_DVT).ExecuteSingle<DmucChung>();
            if (_item != null)
                return true;
            else
            {
                 _item = new DmucChung();
                _item.Ma = _DVT;
                _item.Ten = _DVT;
                _item.Loai = "DONVITINH";
                _item.SttHthi = STT;
                _item.TrangThai = 1;
                _item.TrangthaiMacdinh = 0;
                _item.NgayTao = globalVariables.SysDate;
                _item.NguoiTao = globalVariables.UserName;
                _item.IsNew = true;
                _item.Save();
                txtDonvitinh.Init();
                return true;
            }
        }
    }
}