using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.UI.NGOAITRU;
using SubSonic;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.Forms.Cauhinh;
using System.Globalization;
using VMS.HIS.BHYT;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_chuyendoituongkcb : Form
    {
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        private bool AllowTextChanged;
        private bool m_blnhasLoaded = false;
        private DataTable m_DC;
        private bool isAutoFinding;
        private bool hasjustpressBACKKey;
        byte _idLoaidoituongKcb = 1;
        Int16 _idDoituongKcb = 1;
        string _maDoituongKcb = "DV";
        string _tenDoituongKcb = "Dịch vụ";

        decimal _ptramBhytCu = 0m;
        decimal _ptramBhytGocCu = 0m;
        private bool _hasjustpressBackKey;
        public KcbLuotkham objLuotkham = null;
        public KcbLuotkham objLuotkhamMoi = null;
        private string SoBHYT = "";
        public bool m_blnSuccess = false;
        public frm_chuyendoituongkcb()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
        }
        void InitEvents()
        {
            this.FormClosing += new FormClosingEventHandler(frm_chuyendoituongkcb_FormClosing);
            this.Load += new EventHandler(frm_chuyendoituongkcb_Load);
            this.KeyDown += new KeyEventHandler(frm_chuyendoituongkcb_KeyDown);
            txtMaQuyenloi_BHYT.KeyDown += txtMaQuyenloi_BHYT_KeyDown;
            txtMaQuyenloi_BHYT.TextChanged += new EventHandler(txtMaQuyenloi_BHYT_TextChanged);

            txtNoiphattheBHYT.TextChanged += new EventHandler(txtNoiphattheBHYT_TextChanged);
            txtNoiphattheBHYT.KeyDown += txtNoiphattheBHYT_KeyDown;
            txtOthu4.KeyDown += txtOthu4_KeyDown;
            txtOthu4.TextChanged += new EventHandler(txtOthu4_TextChanged);
            txtOthu5.KeyDown += txtOthu5_KeyDown;
            txtOthu5.TextChanged += new EventHandler(txtOthu5_TextChanged);
            txtOthu6.TextChanged += new EventHandler(txtOthu6_TextChanged);
            txtOthu6.KeyDown += txtOthu6_KeyDown;
            txtOthu6.LostFocus += _LostFocus;
            txtMaKcbbd.KeyDown += txtNoiDKKCBBD_KeyDown;
            txtMaKcbbd.TextChanged += new EventHandler(txtNoiDKKCBBD_TextChanged);

            txtMaNoiCaptheBHYT.TextChanged += new EventHandler(txtNoiDongtrusoKCBBD_TextChanged);
            txtMaNoiCaptheBHYT.KeyDown += txtNoiDongtrusoKCBBD_KeyDown;

            chkTraiTuyen.CheckedChanged += chkTraiTuyen_CheckedChanged;
            cmdSave.Click += new EventHandler(cmdSave_Click);
            lnkThem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkThem_LinkClicked);
            cmdClose.Click += new EventHandler(cmdClose_Click);
            ucThongtinnguoibenh_v21._OnEnterMe += ucThongtinnguoibenh_v21__OnEnterMe;
            cboDoituongKCB.SelectedIndexChanged += new EventHandler(cboDoituongKCB_SelectedIndexChanged);
            txtmathebhyt.TextChanged += txtmathebhyt_TextChanged;
            grdDanhsachthe.ColumnButtonClick += grdDanhsachthe_ColumnButtonClick;
            cboMaKhuvuc.SelectedIndexChanged += CboMaKhuvuc_SelectedIndexChanged;
            cboMadoituongKCB.SelectedIndexChanged += CboMadoituongKCB_SelectedIndexChanged;
            cmdDoiDoituongKCB.Click += CmdDoiDoituongKCB_Click;
        }

        private void CmdDoiDoituongKCB_Click(object sender, EventArgs e)
        {
         
        }

        string lastCode = "";
        private void CboMadoituongKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            lastCode = Utility.sDbnull(cboMadoituongKCB.SelectedValue);
        }

        private void CboMaKhuvuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utility.sDbnull(cboMaKhuvuc.SelectedValue) != "")
            {
                if (chkTraiTuyen.Checked)
                {
                    chkTraiTuyen.Checked = false;
                    lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                }
            }
            TinhPtramBhyt();
        }

        KcbLichsuDoituongKcb objCurrent = null;
        void grdDanhsachthe_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                objCurrent = KcbLichsuDoituongKcb.FetchByID(Utility.Int64Dbnull(grdDanhsachthe.GetValue(KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb)));
                if (objCurrent != null && THU_VIEN_CHUNG.IsBaoHiem(objCurrent.IdLoaidoituongKcb))
                {
                    cmdHuy.BringToFront();
                    cmdSave.Enabled = true;
                    m_blnhasLoaded = false;
                    txtmathebhyt.Text = objCurrent.MatheBhyt;
                    if (!string.IsNullOrEmpty(Utility.sDbnull(objCurrent.NgaybatdauBhyt)))
                        dtpBHYT_Hieuluctu.Value = Convert.ToDateTime(objCurrent.NgaybatdauBhyt);
                    if (!string.IsNullOrEmpty(Utility.sDbnull(objCurrent.NgayketthucBhyt)))
                        dtpBHYT_Hieulucden.Value = Convert.ToDateTime(objCurrent.NgayketthucBhyt);
                    txtPtramBHYT.Text = Utility.sDbnull(objCurrent.PtramBhyt, "0");
                    txtptramDauthe.Text = Utility.sDbnull(objCurrent.PtramBhytGoc, "0");
                    txtMaQuyenloi_BHYT.Text = Utility.sDbnull(objCurrent.MaQuyenloi);
                    txtMaNoiCaptheBHYT.Text = Utility.sDbnull(objCurrent.NoiDongtrusoKcbbd);
                    txtDiachi_bhyt._Text = Utility.sDbnull(objCurrent.DiachiBhyt);
                    cboMaKhuvuc.SelectedValue = objCurrent.MadtuongSinhsong;
                    chkGiayBHYT.Checked = Utility.Byte2Bool(objCurrent.GiayBhyt);
                    if (objCurrent.NgayDu5nam.HasValue)
                        dtpNgaydu5nam.Value = objCurrent.NgayDu5nam.Value;
                    if (objCurrent.TuyentruocDtDenngay.HasValue)
                        dt_ngaydt_tuyentruoc_den.Value = objCurrent.TuyentruocDtDenngay.Value;
                    if (objCurrent.TuyentruocDtTungay.HasValue)
                        dt_ngaydt_tuyentruoc_tu.Value = objCurrent.TuyentruocDtTungay.Value;
                    txtchandoantuyenduoi.SetCode(objCurrent.ChandoanChuyenden);
                    cboMadoituongKCB.SelectedValue = objCurrent.MaDoituongKcbBhyt;
                    txtMaKcbbd.Text = Utility.sDbnull(objCurrent.MaKcbbd);
                    dtpNgaydu5nam.Enabled = chkGiayBHYT.Checked;
                    cmdGetBV.Enabled = txtchandoantuyenduoi.Enabled = dt_ngaydt_tuyentruoc_tu.Enabled = dt_ngaydt_tuyentruoc_den.Enabled = txtNoichuyenden.Enabled = chkChuyenVien.Checked;
                    m_blnhasLoaded = true;
                    GetNoiDangKy();
                    LoadNoiDKKCBBD();
                }
                else
                {
                    Utility.ShowMsg("Việc hiệu chỉnh chỉ áp dụng cho dòng đối tượng BHYT. Vui lòng chọn lại");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                m_blnhasLoaded = true;
            }
        }

        void txtmathebhyt_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (!THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb)) return;
            if (txtmathebhyt.Text.Length < 15) return;
            PhantichmatheBHYT(Utility.sDbnull(txtmathebhyt.Text));
            ModifyMaDauTheEpdungtuyen();
            //if (!IsValidTheBhyt()) return;//Bỏ do có thể nhập 10-12 kí tự
            TinhPtramBhyt();
            txtMaNoiCaptheBHYT.Focus();
            txtMaNoiCaptheBHYT.SelectAll();
        }
        private void PhantichmatheBHYT(string matheBHYT)
        {
            try
            {
                txtMadauthe.Text = matheBHYT.Substring(0, 2);
                txtMaQuyenloi_BHYT.Text = matheBHYT.Substring(2, 1);
                txtNoiphattheBHYT.Text = matheBHYT.Substring(3, 2);
                txtOthu4.Text = matheBHYT.Substring(5, 2);
                txtOthu5.Text = matheBHYT.Substring(7, 3);
                txtOthu6.Text = matheBHYT.Substring(10, 5);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void ModifyMaDauTheEpdungtuyen()
        {
            bool isEpDungTuyen = THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_MADAUTHE_EPDUNGTUYEN", "*", true).Split(',').ToList<string>().Contains(txtmathebhyt.Text.Substring(0, 2));
            chkTraiTuyen.Enabled = !isEpDungTuyen;
            if (chkTraiTuyen.Checked && isEpDungTuyen) chkTraiTuyen.Checked = false;
        }


        void ucThongtinnguoibenh_v21__OnEnterMe()
        {
            if (ucThongtinnguoibenh_v21.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh_v21.objLuotkham;
                Getdata();
            }
        }
        private bool bKhongSua = true;
        private void ModifyCommnadKiemTraDaThanhToan()
        {
            try
            {
                if (_objDoituongKcb != null && THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb))
                {
                    //nếu có lần chi phí thanh toán bảo hiểm y tế không cho chuyển đối tượng
                    SqlQuery sqlQuery = new Select(KcbThanhtoanChitiet.Columns.IdThanhtoan).From(KcbThanhtoan.Schema).InnerJoin(KcbThanhtoanChitiet.IdThanhtoanColumn, KcbThanhtoan.IdThanhtoanColumn)
                  .Where(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(ucThongtinnguoibenh_v21.objLuotkham.MaLuotkham)
                  .And(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(ucThongtinnguoibenh_v21.objLuotkham.IdBenhnhan)
                   .And(KcbThanhtoan.Columns.TrangthaiIn).IsEqualTo(0)
                  .And(KcbThanhtoanChitiet.Columns.MaDoituongKcb).IsEqualTo("BHYT")
                  .And(KcbThanhtoan.Columns.KieuThanhtoan).In(0, 1)
                  .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0);

                    //bool 
                    bKhongSua = sqlQuery.GetRecordCount() <= 0;
                    cboDoituongKCB.Enabled = bKhongSua;

                    // txtInsNumber.Enabled = bKhongSua && BusinessHelper.IsBaoHiem(Utility.Int32Dbnull(cboDoiTuong.SelectedValue));
                    chkTraiTuyen.Enabled = bKhongSua && THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb);
                    chkChuyenVien.Enabled = bKhongSua && THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb);
                    chkThongtuyen.Enabled = bKhongSua && THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb) && globalVariables.gv_blnThongTuyen;
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);

            }
        }
        void cboDoituongKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!AllowTextChanged) return;
            _maDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
            _objDoituongKcb = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_maDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
            ModifyCommnadKiemTraDaThanhToan();
            ChangeObjectRegion();
        }
        void ChangeObjectRegion()
        {
            if (_objDoituongKcb == null) return;
            _idDoituongKcb = _objDoituongKcb.IdDoituongKcb;
            _idLoaidoituongKcb = _objDoituongKcb.IdLoaidoituongKcb;
            _tenDoituongKcb = _objDoituongKcb.TenDoituongKcb;
            _ptramBhytCu = _objDoituongKcb.PhantramTraituyen.Value;
            _ptramBhytGocCu = _ptramBhytCu;
            txtPtramBHYT.Text = _objDoituongKcb.PhantramTraituyen.ToString();
            txtptramDauthe.Text = _objDoituongKcb.PhantramTraituyen.ToString();
            if (THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb))//ĐỐi tượng BHYT
            {
                pnlBHYT.Enabled = true;
                lblPtram.Text = @"Mức hưởng:";
                TinhPtramBhyt();
                lblTuyenBHYT.Visible = true;
                //NapThongtinDichvuKcb();
                txtmathebhyt.SelectAll();
                txtmathebhyt.Focus();
                //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_CHOPHEPSUDUNG_QRCODE", "0", false) == "1")
                //{
                txtQRCode.Visible = true;
                txtQRCode.Focus();
                //}
            }
            else//Đối tượng khác BHYT
            {
                lblTuyenBHYT.Visible = false;
                pnlBHYT.Enabled = false;
                lblPtram.Text = @"Mức hưởng:";
                XoathongtinBHYT(PropertyLib._KCBProperties.XoaBHYT);
                //NapThongtinDichvuKcb();
                txtTEN_BN.Focus();
                //  txtQRCode.Visible = false;
            }
        }
        void cmdClose_Click(object sender, EventArgs e)
        {
            m_blnSuccess = false;
            this.Close();
        }
        void frm_chuyendoituongkcb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {

                _maDoituongKcb = "DV";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _maDoituongKcb);
                return;
            }
            if (e.Control && e.KeyCode == Keys.B)
            {
                _maDoituongKcb = "BHYT";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _maDoituongKcb);
                return;
            }
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void frm_chuyendoituongkcb_Load(object sender, EventArgs e)
        {
            try
            {
                cboMaKhuvuc.DropDownStyle = ComboBoxStyle.DropDownList;
                m_blnhasLoaded = false;
                AllowTextChanged = false;
                //   Utility.SetColor(lblDiachiBHYT, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BHYT", "1", false) == "1");
                chkTraiTuyen.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEPTIEPDON_TRAITUYEN", "1", false) == "1";
                DataTable dtMaKhuvuc = THU_VIEN_CHUNG.LayDulieuDanhmucChung("MADOITUONGSINHSONG", true);
                DataBinding.BindDataCombobox(cboMaKhuvuc, dtMaKhuvuc, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "", true);

                m_dtDoiTuong_KCB = SPs.DmucLaydulieudanhmuMaDoiTuongKCBBhyt("MA_DOITUONG_KCB", "-1", "-1").GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboMadoituongKCB, m_dtDoiTuong_KCB, DmucChung.Columns.Ma, DmucChung.Columns.Ten);

                cboMaKhuvuc.SelectedValue = "";

                AddAutoCompleteDiaChi();
                DataBinding.BindDataCombobox(cboDoituongKCB, globalVariables.gv_dtDoituong, DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "", false);
                ucThongtinnguoibenh_v21.Refresh(true);


            }
            catch
            {
            }
            finally
            {
                m_blnhasLoaded = true;
                AllowTextChanged = true;
            }
        }
        private void AddAutoCompleteDiaChi()
        {
            txtDiachi_bhyt.LengthOfQuickType = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_DIACHIGOTAT_6KITU", "6", false));
            txtDiachi_bhyt.dtData = globalVariables.dtAutocompleteAddress;
            // txtDiachi.dtData = globalVariables.dtAutocompleteAddress.Copy();
            this.txtDiachi_bhyt.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            this.txtDiachi_bhyt.CaseSensitive = false;
            this.txtDiachi_bhyt.MinTypedCharacters = 1;

            //  this.txtDiachi.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            //  this.txtDiachi.CaseSensitive = false;
            //  this.txtDiachi.MinTypedCharacters = 1;
            //m_dtDataThanhPho = THU_VIEN_CHUNG.LayDmucDiachinh();
            //AddShortCut_DC();
        }
        private void CreateTable()
        {
            if (m_DC == null || m_DC.Columns.Count <= 0)
            {
                m_DC = new DataTable();
                m_DC.Columns.AddRange(new[]
                                          {
                                              new DataColumn("ShortCutXP", typeof (string)),
                                              new DataColumn("ShortCutQH", typeof (string)),
                                              new DataColumn("ShortCutTP", typeof (string)),
                                              new DataColumn("Value", typeof (string)),
                                              new DataColumn("ComparedValue", typeof (string))
                                          });
            }
        }


        void frm_chuyendoituongkcb_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        DataTable dtLichsu = new DataTable();
        private void Getdata()
        {
            if (objLuotkham == null) return;
            AllowTextChanged = false;
            _ptramBhytCu = 0m;
            if (objLuotkham != null)
            {
                _idDoituongKcb = objLuotkham.IdDoituongKcb;
                chkTraiTuyen.Checked = Utility.Int32Dbnull(objLuotkham.DungTuyen, 0) == 0;

                _idDoituongKcb = objLuotkham.IdDoituongKcb;
                _tenDoituongKcb = _idDoituongKcb == 1 ? "Dịch vụ" : "Bảo hiểm y tế";
                _maDoituongKcb = _idDoituongKcb == 1 ? "DV" : "BHYT";
                chkChuyenVien.Checked = Utility.Int32Dbnull(objLuotkham.TthaiChuyenden, 0) == 1;
                txtOldName.Text = _tenDoituongKcb;
                if (objLuotkham != null)
                {
                    _objDoituongKcb = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(objLuotkham.MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                    cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, objLuotkham.MaDoituongKcb);
                }
                if (THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb) && !string.IsNullOrEmpty(objLuotkham.MatheBhyt))//Thông tin BHYT
                {
                    txtmathebhyt.Text = objLuotkham.MatheBhyt;
                    if (!string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.NgaybatdauBhyt)))
                        dtpBHYT_Hieuluctu.Value = Convert.ToDateTime(objLuotkham.NgaybatdauBhyt);
                    if (!string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.NgayketthucBhyt)))
                        dtpBHYT_Hieulucden.Value = Convert.ToDateTime(objLuotkham.NgayketthucBhyt);
                    txtPtramBHYT.Text = Utility.sDbnull(objLuotkham.PtramBhyt, "0");
                    txtptramDauthe.Text = Utility.sDbnull(objLuotkham.PtramBhytGoc, "0");
                    txtMaQuyenloi_BHYT.Text = Utility.sDbnull(objLuotkham.MaQuyenloi);
                    txtMaNoiCaptheBHYT.Text = Utility.sDbnull(objLuotkham.NoiDongtrusoKcbbd);
                    txtDiachi_bhyt._Text = Utility.sDbnull(objLuotkham.DiachiBhyt);
                    cboMaKhuvuc.SelectedValue = objLuotkham.MadtuongSinhsong;
                    chkGiayBHYT.Checked = Utility.Byte2Bool(objLuotkham.GiayBhyt);
                    if (objLuotkham.NgayDu5nam.HasValue)
                        dtpNgaydu5nam.Value = objLuotkham.NgayDu5nam.Value;
                    if (objLuotkham.TuyentruocDtDenngay.HasValue)
                        dt_ngaydt_tuyentruoc_den.Value = objLuotkham.TuyentruocDtDenngay.Value;
                    if (objLuotkham.TuyentruocDtTungay.HasValue)
                        dt_ngaydt_tuyentruoc_tu.Value = objLuotkham.TuyentruocDtTungay.Value;
                    txtchandoantuyenduoi.SetCode(objLuotkham.ChandoanChuyenden);
                    cboMadoituongKCB.SelectedValue = objLuotkham.MaDoituongKcbBhyt;
                    txtMaKcbbd.Text = Utility.sDbnull(objLuotkham.MaKcbbd);
                    dtpNgaydu5nam.Enabled = chkGiayBHYT.Checked;
                    cmdGetBV.Enabled = txtchandoantuyenduoi.Enabled = dt_ngaydt_tuyentruoc_tu.Enabled = dt_ngaydt_tuyentruoc_den.Enabled = txtNoichuyenden.Enabled = chkChuyenVien.Checked;
                    pnlBHYT.Enabled = true;
                }
                else
                {
                    pnlBHYT.Enabled = true;
                    XoathongtinBHYT(true);
                }
                dtLichsu = new Select().From(KcbLichsuDoituongKcb.Schema)
                    .Where(KcbLichsuDoituongKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbLichsuDoituongKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbLichsuDoituongKcb.Columns.MaDoituongKcb).IsEqualTo("BHYT")//Chỉ load các dòng liên quan đến BHYT
                    .OrderDesc(KcbLichsuDoituongKcb.Columns.NgayApdung)
                    .ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDanhsachthe, dtLichsu, false, true, "1=1", "", true);
            }
            else
            {
            }
        }
        private void GetNoiDangKy()
        {
            //SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
            //    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtNoiDongtrusoKCBBD.Text);
            var objdiachinh = (from p in globalVariables.gv_dtDmucDiachinh.AsEnumerable()
                               where p[DmucDiachinh.Columns.MaDiachinh].Equals(txtMaNoiCaptheBHYT.Text)
                               select p).FirstOrDefault();
            //var objDiachinh = sqlQuery.ExecuteSingle<DmucDiachinh>();
            if (objdiachinh != null)
            {
                txtMaNoiCaptheBHYT.Text = Utility.sDbnull(objdiachinh["ten_diachinh"]);
                lblNoiCapThe.Visible = true;
                Utility.SetMsg(lblNoiCapThe, Utility.sDbnull(objdiachinh["ten_diachinh"]), false);
                //LoadClinicCode();
            }
            else
            {
                lblNoiCapThe.Visible = false;
                txtMaNoiCaptheBHYT.Text = "";
            }
        }
        void XoathongtinBHYT(bool forcetodel)
        {
            if (forcetodel)
            {
                m_blnhasLoaded = false;
                txtQRCode.Clear();
                txtmathebhyt.Clear();
                dtpBHYT_Hieuluctu.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
                dtpBHYT_Hieulucden.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
                txtPtramBHYT.Text = "";
                txtptramDauthe.Text = "";
                lblNoiCapThe.Text = "";
                txtMaNoiCaptheBHYT.Text = "";
                txtTenKcbbd.Text = "";
                lblClinicName.Text = "";
                txtMadauthe.Clear();
                cboMaKhuvuc.SelectedIndex = -1;
                chkGiayBHYT.Checked = false;
                txtMaQuyenloi_BHYT.Clear();
                txtMaNoiCaptheBHYT.Clear();
                txtOthu4.Clear();
                txtOthu5.Clear();
                txtOthu6.Clear();
                chkTraiTuyen.Checked = false;
                lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                txtNoiphattheBHYT.Clear();
                txtDiachi_bhyt.Clear();
                txtMaKcbbd.Clear();
                m_blnhasLoaded = true;
            }
        }
        private string GetSoBHYT
        {
            get { return SoBHYT; }
            set { SoBHYT = value; }
        }

        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                if (THU_VIEN_CHUNG.IsBaoHiem(_idLoaidoituongKcb))
                {
                    if (!isValidData()) return;
                    if (!IsValidTheBhyt()) return;
                }
                if (THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb))
                {
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_BATNHAP_DIACHI_BHYT", "0", false) == "1")
                    {
                        if (Utility.DoTrim(txtDiachi_bhyt.Text) == "")
                        {
                            errorProvider1.SetError(txtDiachi_bhyt, "Bạn phải nhập địa chỉ thẻ BHYT");
                            txtDiachi_bhyt.Focus();
                            return;
                        }
                    }

                    //Kiểm tra ngày của các thẻ BHYT
                    foreach (DataRow dr in dtLichsu.Rows)
                    {
                        KcbLichsuDoituongKcb objTest = KcbLichsuDoituongKcb.FetchByID(Utility.Int32Dbnull(dr[KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb]));
                        if (objTest.MaDoituongKcb == "BHYT")
                        {
                            if ((dtpBHYT_Hieuluctu.Value.Date >= objTest.NgaybatdauBhyt && dtpBHYT_Hieuluctu.Value.Date <= objTest.NgayketthucBhyt) || (dtpBHYT_Hieulucden.Value.Date >= objTest.NgaybatdauBhyt && dtpBHYT_Hieulucden.Value.Date <= objTest.NgayketthucBhyt))
                            {
                                Utility.GonewRowJanus(grdDanhsachthe, KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb, objTest.IdLichsuDoituongKcb.ToString());
                                Utility.ShowMsg(string.Format("Ngày hiệu lực của thẻ BHYT mới: {0}-{1} không được phép nằm trong khoảng ngày hiệu lực của một trong các thẻ cũ: {2}-{3}", dtpBHYT_Hieuluctu.Text, dtpBHYT_Hieulucden.Text, objTest.NgaybatdauBhyt.Value.ToString("dd/MM/yyyy"), objTest.NgayketthucBhyt.Value.ToString("dd/MM/yyyy")));
                                return;
                            }
                            if (dtpNgayApdungBHYT.Value.Date >= objTest.NgaybatdauBhyt && dtpNgayApdungBHYT.Value.Date <= objTest.NgayketthucBhyt)
                            {
                                Utility.GonewRowJanus(grdDanhsachthe, KcbLichsuDoituongKcb.Columns.IdLichsuDoituongKcb, objTest.IdLichsuDoituongKcb.ToString());
                                Utility.ShowMsg(string.Format("Ngày áp dụng BHYT của thẻ BHYT mới: {0} không được phép nằm trong khoảng ngày hiệu lực của một trong các thẻ cũ: {1}-{2}", dtpNgayApdungBHYT.Text, objTest.NgaybatdauBhyt.Value.ToString("dd/MM/yyyy"), objTest.NgayketthucBhyt.Value.ToString("dd/MM/yyyy")));
                                return;
                            }
                        }
                    }
                }
                //Kiểm tra nếu đối tượng đang khác BHYT mà thêm thẻ-->đổi từ DV->BHYT sẽ cảnh báo các chi phí đã kê trước ngày áp dụng thẻ thành tự túc-->Có thay đổi lại giá từ DV sang BHYT hay không?
                objLuotkhamMoi = GetNewInfor();
                string msg = "Bạn có chắc chắn muốn lưu thông tin vừa bổ sung(điều chỉnh)?";
                if (Utility.AcceptQuestion(msg, "Xác nhận", true))
                {
                    ActionResult actionResult = ChuyenDoituongKCB.Noithe(objLuotkhamMoi, objLuotkham, objCurrent);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            //Utility.ShowMsg("Chuyển đổi đối tượng thành công. Nhấn nút OK để kết thúc", "Thành công");
                            cmdNew.BringToFront();
                            ucThongtinnguoibenh_v21.Refresh(true);
                            break;
                        case ActionResult.Cancel:
                            //Utility.ShowMsg("Cập nhật thông tin không thành công","Thông báo");
                            break;
                        case ActionResult.Exception:
                            //Utility.ShowMsg("Có lỗi trong quá trình thực hiện.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.EnableButton(cmdSave, true);
                Utility.CatchException(ex);
            }
            finally
            {
                cmdNew.BringToFront();
                Utility.EnableButton(cmdSave, true);
            }
        }
        private KcbLuotkham GetNewInfor()
        {

            KcbLuotkham _objLuotkham = new KcbLuotkham();
            _objLuotkham.IdBenhnhan = objLuotkham.IdBenhnhan;
            _objLuotkham.MaLuotkham = objLuotkham.MaLuotkham;
            _objLuotkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            _objLuotkham.IdDoituongKcb = _idDoituongKcb;
            _objLuotkham.NgayApdungBhyt = dtpNgayApdungBHYT.Value;
            _objLuotkham.IdBenhvienDen = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
            _objLuotkham.TthaiChuyenden = (byte)(chkChuyenVien.Checked ? 1 : 0);
            _objLuotkham.ChandoanChuyenden = Utility.ReplaceStr(txtchandoantuyenduoi.Text);
            _objLuotkham.MaLydovaovien = LayMaLydoVv();
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_idDoituongKcb);
            if (objectType != null)
            {
                _objLuotkham.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                _objLuotkham.IdLoaidoituongKcb = objectType.IdLoaidoituongKcb;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(_idLoaidoituongKcb))
            {
                Laymathe_BHYT();
                _objLuotkham.MaKcbbd = Utility.sDbnull(txtMaKcbbd.Text, "");
                _objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(txtTenKcbbd.Text, "");
                _objLuotkham.MaNoicapBhyt = Utility.sDbnull(txtMaNoiCaptheBHYT.Text);
                _objLuotkham.NoicapBhyt = Utility.sDbnull(txtTenNoiCapTheBHYT.Text);
                _objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
                _objLuotkham.MatheBhyt = Laymathe_BHYT();
                _objLuotkham.MaDoituongBhyt = Utility.sDbnull(txtMadauthe.Text);
                _objLuotkham.MaDoituongKcbBhyt = Utility.sDbnull(cboMadoituongKCB.SelectedValue);
                _objLuotkham.MaQuyenloi = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, null);
                _objLuotkham.DungTuyen = !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1)));
                _objLuotkham.MadtuongSinhsong = Utility.sDbnull(cboMaKhuvuc.SelectedValue);
                _objLuotkham.GiayBhyt = Utility.Bool2byte(chkGiayBHYT.Checked);
                _objLuotkham.MaLydovaovien = LayMaLydoVv();
                if (chkGiayBHYT.Checked)
                {
                    _objLuotkham.NgayDu5nam = dtpNgaydu5nam.Value.Date;
                    _objLuotkham.NgayMienCctTu = dtpNgayMienCCT_Tu.Value.Date;
                    _objLuotkham.NgayMienCctDen = dtpNgayMienCCT_den.Value.Date;
                    _objLuotkham.GiayBhyt = 1;
                }
                else
                {
                    _objLuotkham.NgayDu5nam = null;
                    _objLuotkham.NgayMienCctTu = null;
                    _objLuotkham.NgayMienCctDen = null;
                    _objLuotkham.SogiayChuyentuyen = "";
                    _objLuotkham.GiayBhyt = 0;
                }
                if (chkChuyenVien.Checked)
                {
                    _objLuotkham.IdBenhvienDen = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                    _objLuotkham.TthaiChuyenden = (byte)(chkChuyenVien.Checked ? 1 : 0);
                    _objLuotkham.ChandoanChuyenden = Utility.ReplaceStr(txtchandoantuyenduoi.Text);
                    _objLuotkham.SogiayChuyentuyen = Utility.sDbnull(txtSochuyenvien.Text);
                    _objLuotkham.TuyentruocDtTungay = dt_ngaydt_tuyentruoc_tu.Value;
                    _objLuotkham.TuyentruocDtDenngay = dt_ngaydt_tuyentruoc_den.Value;
                }
                else
                {
                    _objLuotkham.IdBenhvienDen = 0;
                    _objLuotkham.TthaiChuyenden = 0;
                    _objLuotkham.ChandoanChuyenden = "";
                    _objLuotkham.SogiayChuyentuyen = "";
                    _objLuotkham.TuyentruocDtTungay = null;
                    _objLuotkham.TuyentruocDtDenngay = null;
                }
                _objLuotkham.NgayketthucBhyt = dtpBHYT_Hieulucden.Value.Date;
                _objLuotkham.NgaybatdauBhyt = dtpBHYT_Hieuluctu.Value.Date;
                _objLuotkham.NoicapBhyt = Utility.GetValue(lblNoiCapThe.Text, false);
                _objLuotkham.DiachiBhyt = Utility.sDbnull(txtDiachi_bhyt.Text);
                _objLuotkham.NoThe = chknothe.Checked;
                _objLuotkham.MaDoituongNothe = chknothe.Checked ? txtdoituongnothe.MyCode : "";

                _objLuotkham.PtramBhytGoc = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                _objLuotkham.PtramBhyt = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);

            }
            else
            {
                _objLuotkham.MatheBhyt = "";
                _objLuotkham.MaDoituongBhyt = "";
                _objLuotkham.MaDoituongKcbBhyt = "";
                _objLuotkham.MaQuyenloi = -1;
                _objLuotkham.DungTuyen = 0;
                _objLuotkham.NgayketthucBhyt = null;
                _objLuotkham.NgaybatdauBhyt = null;
                _objLuotkham.DiachiBhyt = "";
                _objLuotkham.MaKcbbd = "";
                _objLuotkham.NoiDongtrusoKcbbd = "";
                _objLuotkham.MaNoicapBhyt = "";
                _objLuotkham.NoicapBhyt = "";
                _objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;

                _objLuotkham.MadtuongSinhsong = "";

                _objLuotkham.GiayBhyt = 0;
                _objLuotkham.NgayDu5nam = null;
                _objLuotkham.NgayMienCctTu = null;
                _objLuotkham.NgayMienCctDen = null;


                _objLuotkham.IdBenhvienDen = 0;
                _objLuotkham.TthaiChuyenden = 0;
                _objLuotkham.ChandoanChuyenden = "";
                _objLuotkham.SogiayChuyentuyen = "";
                _objLuotkham.TuyentruocDtTungay = null;
                _objLuotkham.TuyentruocDtDenngay = null;
                _objLuotkham.MaLydovaovien = 0;

                _objLuotkham.NoThe = false;
                _objLuotkham.MaDoituongNothe = "";
                _objLuotkham.PtramBhytGoc = 0;
                _objLuotkham.PtramBhyt = 0;
            }
            _objLuotkham.TrangthaiNoitru = objLuotkham.TrangthaiNoitru;
            _objLuotkham.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
            return _objLuotkham;
        }
        private bool isValidData()
        {
            //if (string.IsNullOrEmpty(txtMadauthe.Text))
            //{
            //    Utility.ShowMsg("Bạn phải nhập đối tượng đầu thẻ cho bảo hiểm không bỏ trống", "Thông báo",
            //                    MessageBoxIcon.Information);
            //    txtMadauthe.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtMaQuyenloi_BHYT.Text))
            //{
            //    Utility.ShowMsg("Bạn phải nhập mã quyền lợi cho bảo hiểm không bỏ trống", "Thông báo");
            //    txtMaQuyenloi_BHYT.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtMaNoiCaptheBHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập Mã tỉnh, thành phố trực thuộc Trung ương, nơi phát hành thẻ BHYT", "Thông báo");
                txtMaNoiCaptheBHYT.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtOthu4.Text))
            //{
            //    Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 4  cho bảo hiểm không bỏ trống", "Thông báo");
            //    txtOthu4.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtOthu5.Text))
            //{
            //    Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 5  cho bảo hiểm không bỏ trống", "Thông báo");
            //    txtOthu5.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtOthu6.Text))
            //{
            //    Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 6  cho bảo hiểm không bỏ trống", "Thông báo");
            //    txtOthu6.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtNoiphattheBHYT.Text))
            //{
            //    Utility.ShowMsg("Bạn phải nhập nơi cấp thẻ BHYT", "Thông báo",
            //                    MessageBoxIcon.Information);
            //    txtNoiphattheBHYT.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtMaKcbbd.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký KCB ban đầu",
                                "Thông báo");
                txtMaKcbbd.Focus();
                return false;
            }
            if (Utility.Int16Dbnull(txtptramDauthe.Text, 0) <= 0)
            {
                Utility.ShowMsg("Đối tượng khám BHYT cần mức hưởng thẻ gốc >0. Vui lòng kiểm tra lại");
                return false;
            }
            if (string.IsNullOrEmpty(txtTenKcbbd.Text))
            {
                Utility.ShowMsg("Nơi đăng ký khám chữa bệnh ban đầu chưa tồn tại trong hệ thống", "Thông báo");
                var newItem = new frm_ThemnoiKCBBD();
                newItem.m_dtDataThanhPho = globalVariables.gv_dtDmucDiachinh;
                newItem.SetInfor(txtMaKcbbd.Text, txtNoiphattheBHYT.Text);
                if (newItem.ShowDialog() == DialogResult.OK)
                {
                    txtMaKcbbd.Text = "";
                    txtNoiphattheBHYT.Text = "";
                    txtMaKcbbd.Text = newItem.txtMa.Text.Trim();
                    txtNoiphattheBHYT.Text = newItem.txtMaThanhPho.Text.Trim();
                    dtpBHYT_Hieuluctu.Focus();
                }
                return false;
            }
            if (dtpBHYT_Hieulucden.Value < dtpBHYT_Hieuluctu.Value)
            {
                Utility.ShowMsg("Ngày hết hạn thẻ BHYT phải lớn hơn hoặc bằng ngày đăng ký thẻ BHYT", "Thông báo");
                dtpBHYT_Hieulucden.Focus();
                return false;
            }
            if (dtpBHYT_Hieuluctu.Value > globalVariables.SysDate)
            {
                Utility.ShowMsg("Ngày bắt đầu thẻ BHYT không thể lớn hơn ngày hiện tại", "Thông báo");
                dtpBHYT_Hieuluctu.Focus();
                return false;
            }
            if (dtpBHYT_Hieulucden.Value.Date < objLuotkham.NgayTiepdon.Date)
            {
                Utility.ShowMsg("Ngày kết thúc bảo hiểm đã hết hạn \n Do ngày hết hạn đang nhỏ hơn tiếp đón", "Thông báo", MessageBoxIcon.Warning);
                dtpBHYT_Hieulucden.Focus();
                return false;
            }
            ////Cảnh báo còn thuốc lần khám trước
            //TimeSpan songaychothuoc = Convert.ToDateTime(dtInsToDate.Value).Subtract(globalVariables.SysDate);
            //int songay = Utility.Int32Dbnull(songaychothuoc.TotalDays);
            //if (Utility.Int32Dbnull(songay) <= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_SONGAYBATHANTHE", "30", true))
            //    && Utility.Int16Dbnull(cboDoituongKCB.SelectedValue) == 2)
            //{
            //    Utility.ShowMsg(string.Format("Hạn thẻ BHYT còn {0} ngày", songay), "Cảnh Báo");
            //}
            return true;
        }
        DataTable m_dtDoiTuong_KCB = new DataTable();
        private void chkTraiTuyen_CheckedChanged(object sender, EventArgs e)
        {
            if (_objDoituongKcb != null && THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb))
            {
                if (chkTraiTuyen.Checked)
                {
                    if (Utility.sDbnull(cboMaKhuvuc.SelectedValue) == "")
                    {
                        Utility.ShowMsg("Thẻ BHYT có mã khu vực K1, K2, K3; được quỹ BHYT thanh toán chi phí khám chữa bệnh đối với bệnh viện tuyến huyện," +
                                        " điều trị nội trú đối với bệnh viện tuyến tỉnh, tuyến trung ương(không cần giấy chuyển tuyến khám chữa bệnh)");
                    }
                    chkThongtuyen.Checked = false;
                    chkCapCuu.Checked = false;
                    chkDungtuyen.Checked = false;
                    m_dtDoiTuong_KCB = SPs.DmucLaydulieudanhmuMaDoiTuongKCBBhyt("MA_DOITUONG_KCB", "3", "-1").GetDataSet().Tables[0];
                    DataBinding.BindDataCombobox(cboMadoituongKCB, m_dtDoiTuong_KCB, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                    cboMadoituongKCB.SelectedValue = lastCode;
                }
                TinhPtramBhyt();
                LayLydoVaovien();
            }
        }
        private byte LayMaLydoVv()
        {
            byte _value = 1;
            if (chkDungtuyen.Checked)
                _value = 1;
            else if (chkCapCuu.Checked)
                _value = 2;
            else if (chkTraiTuyen.Checked)
                _value = 3;
            else if (chkThongtuyen.Checked)
                _value = 4;
            return _value;
        }
        private void LayLydoVaovien()
        {
            string tenlydo = "";
            if (chkDungtuyen.Checked)
                tenlydo = "ĐÚNG TUYÊN";
            else if (chkCapCuu.Checked)
                tenlydo = "CẤP CỨU";
            else if (chkTraiTuyen.Checked)
                tenlydo = "TRÁI TUYÊN";
            else if (chkThongtuyen.Checked)
                tenlydo = "THÔNG TUYÊN";
            else tenlydo = "ĐÚNG TUYÊN";
            lblTuyenBHYT.Text = tenlydo;
            //return tenlydo;
        }
        private void _LostFocus(object sender, EventArgs e)
        {
            if (isAutoFinding) return;
            string MA_BHYT = txtMadauthe.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                             txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
            string noiDangky = txtMaNoiCaptheBHYT.Text.Trim() + txtMaKcbbd.Text.Trim();
            if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBHYT(MA_BHYT, noiDangky);
        }
        private void txtNoiDKKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                //string MA_BHYT = txtMadauthe.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiDongtrusoKCBBD.Text.Trim() + txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                //if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtMaKcbbd.Text.Length <= 0)
                {
                    txtMaNoiCaptheBHYT.Focus();
                    txtMaNoiCaptheBHYT.Select(txtMaNoiCaptheBHYT.Text.Length, 0);
                }
            }
        }
        private void txtNoiDongtrusoKCBBD_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_maDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtMaNoiCaptheBHYT.Text.Length <= 0)
            {
                txtOthu6.Focus();
                if (txtOthu6.Text.Length > 0) txtOthu6.Select(txtOthu6.Text.Length, 0);
                return;
            }
            if (txtMaNoiCaptheBHYT.Text.Length < 2) return;
            if (!IsValidTheBhyt()) return;
            LoadClinicCode();
            txtMaKcbbd.Focus();
            txtMaKcbbd.SelectAll();
        }

        private void txtOthu4_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_maDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu4.Text.Length <= 0)
            {
                txtNoiphattheBHYT.Focus();
                if (txtNoiphattheBHYT.Text.Length > 0) txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
                return;
            }
            if (txtOthu4.Text.Length < 2) return;
            if (!IsValidTheBhyt()) return;
            txtOthu5.Focus();
            txtOthu5.SelectAll();
        }

        private void txtOthu5_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_maDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu5.Text.Length <= 0)
            {
                txtOthu4.Focus();
                if (txtOthu4.Text.Length > 0) txtOthu4.Select(txtOthu4.Text.Length, 0);
                return;
            }
            if (txtOthu5.Text.Length < 3) return;
            if (!IsValidTheBhyt()) return;
            txtOthu6.Focus();
            txtOthu6.SelectAll();
        }

        private void txtOthu6_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_maDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu6.Text.Length <= 0)
            {
                txtOthu5.Focus();
                if (txtOthu5.Text.Length > 0) txtOthu5.Select(txtOthu5.Text.Length, 0);
                return;
            }
            if (txtOthu6.Text.Length < 5) return;
            if (!IsValidTheBhyt()) return;
            txtMaNoiCaptheBHYT.Focus();
            txtMaNoiCaptheBHYT.SelectAll();
        }




        private void txtNoiDKKCBBD_TextChanged(object sender, EventArgs e)
        {
            if (_maDoituongKcb == "DV") return;
            if (txtMaKcbbd.Text.Length < 3)
            {
                Utility.SetMsg(lblClinicName, "", false);
                return;
            }
            LoadClinicCode();
            if (lnkThem.Visible) lnkThem.Focus();
            else
                dtpBHYT_Hieuluctu.Focus();
        }

        private void LaySoTheBhyt()
        {
            string SoBHYT = string.Format("{0}{1}{2}", Utility.sDbnull(txtmathebhyt.Text), txtMaNoiCaptheBHYT.Text, txtMaKcbbd.Text);
            GetSoBHYT = SoBHYT;
        }
        private string mathe_bhyt_full()
        {
            return string.Format("{0}{1}{2}", Utility.sDbnull(txtmathebhyt.Text), txtMaNoiCaptheBHYT.Text, txtMaKcbbd.Text);

        }
        public bool kiemtra15kytu = false;
        private string Laymathe_BHYT()
        {
            string SoBHYT = Utility.sDbnull(txtmathebhyt.Text); //string.Format("{0}{1}{2}{3}{4}{5}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text, txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text);
            return SoBHYT;
        }
        DmucDoituongkcb _objDoituongKcb = null;
        private bool IsValidTheBhyt()
        {

            string mathe = Utility.sDbnull(Laymathe_BHYT());
            string dauthe = mathe.Substring(0, 2);
            string ma_quyenloi = mathe.Substring(2, 1);
            if (mathe.Length == 15)
            {
                if (!string.IsNullOrEmpty(mathe))
                //  if (!string.IsNullOrEmpty(txtMaDtuong_BHYT.Text))
                {
                    SqlQuery sqlQuery = new Select().From(DmucDoituongbhyt.Schema)
                        .Where(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(dauthe);
                    if (sqlQuery.GetRecordCount() <= 0)
                    {
                        Utility.ShowMsg(
                            "Mã đối tượng BHYT không tồn tại trong hệ thống. Mời bạn kiểm tra lại",
                            "Thông báo", MessageBoxIcon.Information);
                        txtmathebhyt.Focus();
                        txtmathebhyt.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtmathebhyt.Text))
                //if (Utility.DoTrim(txtMaDtuong_BHYT.Text) != "" && Utility.DoTrim(txtMaQuyenloi_BHYT.Text) != "")
                {
                    QheDautheQloiBhyt objQheDautheQloiBhyt = new Select().From(QheDautheQloiBhyt.Schema).Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(Utility.DoTrim(dauthe))
                        .And(QheDautheQloiBhyt.Columns.MaQloi).IsEqualTo(Utility.Int32Dbnull(ma_quyenloi, 0)).ExecuteSingle<QheDautheQloiBhyt>();
                    if (objQheDautheQloiBhyt == null)
                    {
                        Utility.ShowMsg(string.Format("Đầu thẻ BHYT: {0} chưa được cấu hình gắn với mã quyền lợi: {1}. Mời bạn kiểm tra lại", dauthe, ma_quyenloi));
                        txtmathebhyt.Focus();
                        txtmathebhyt.SelectAll();
                        return false;
                    }
                }
                //Check lại đoạn này xem có lặp lại đoạn trên không?
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_KIEMTRAMATHE", "1", true) == "1")
                {
                    if (!string.IsNullOrEmpty(ma_quyenloi))
                    {
                        if (Utility.Int32Dbnull(ma_quyenloi, 0) < 1 || Utility.Int32Dbnull(ma_quyenloi, 0) > 9)
                        {
                            Utility.ShowMsg("Số thứ tự 2 của mã bảo hiểm nằm trong khoảng từ 1->9", "Thông báo",
                                            MessageBoxIcon.Information);
                            txtmathebhyt.Focus();
                            txtmathebhyt.SelectAll();
                            return false;
                        }
                        var lstqhe =
                            new Select().From(QheDautheQloiBhyt.Schema)
                                .Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt)
                                .IsEqualTo(dauthe)
                                .ExecuteAsCollection<QheDautheQloiBhytCollection>();
                        if (lstqhe.Count > 0)
                        {
                            var q = from p in lstqhe
                                    where p.MaQloi == Utility.ByteDbnull(ma_quyenloi, -1)
                                    select _objDoituongKcb;

                            if (!q.Any())
                            {

                                Utility.ShowMsg(
                                    string.Format(
                                        "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                        dauthe, ma_quyenloi));
                                txtmathebhyt.Focus();
                                txtmathebhyt.SelectAll();
                                return false;
                            }
                        }
                        else
                        {
                            Utility.ShowMsg(
                                string.Format(
                                    "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                    dauthe, ma_quyenloi));
                            txtmathebhyt.Focus();
                            txtmathebhyt.SelectAll();
                            return false;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtMaNoiCaptheBHYT.Text))
            {
                if (txtMaNoiCaptheBHYT.Text.Length <= 1)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD phải nhập từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtMaNoiCaptheBHYT.Focus();
                    txtMaNoiCaptheBHYT.SelectAll();
                    return false;
                }

                if (Utility.Int32Dbnull(txtMaNoiCaptheBHYT.Text, 0) <= 0)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD không được phép có chữ cái và phải nằm trong khoảng từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtMaNoiCaptheBHYT.Focus();
                    txtMaNoiCaptheBHYT.SelectAll();
                    return false;
                }

                SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
                    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtMaNoiCaptheBHYT.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã thành phố nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtMaNoiCaptheBHYT.Focus();
                    txtMaNoiCaptheBHYT.SelectAll();
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(txtMaKcbbd.Text))
            {
                string maDiachinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_DANGKY_CACHXACDINH_NOIDKKCBBD", "1", true) == "0" ? txtNoiphattheBHYT.Text : txtMaNoiCaptheBHYT.Text;
                int i = (from p in globalVariables.gv_dtDmucNoiKCBBD.AsEnumerable()
                         where p[DmucNoiKCBBD.Columns.MaDiachinh].Equals(maDiachinh)
                         select p).Count();
                //SqlQuery sqlQuery = new Select().From(DmucNoiKCBBD.Schema)
                //    .Where(DmucNoiKCBBD.Columns.MaKcbbd).IsEqualTo(txtNoiDKKCBBD.Text)
                //    .And(DmucNoiKCBBD.Columns.MaDiachinh).IsEqualTo(maDiachinh);
                if (i <= 0)
                {
                    Utility.ShowMsg(
                        "Mã  nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtMaKcbbd.Focus();
                    txtMaKcbbd.SelectAll();
                    return false;
                }
            }
            return true;
        }
        private void FindPatientIDbyBHYT(string insuranceNum, string noiDangky)
        {
            try
            {

                DataTable temdt = SPs.KcbTimkiembenhnhantheomathebhyt(insuranceNum + noiDangky).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    //AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), Insurance_Num);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr =
                      temdt.Select(KcbLuotkham.Columns.MatheBhyt + "='" + insuranceNum + "' AND " +
                                   KcbLuotkham.Columns.NoiDongtrusoKcbbd + "= '" + noiDangky.Substring(0, 2) +
                                   "' AND " + KcbLuotkham.Columns.MaKcbbd + "= '" + noiDangky.Substring(2, 3) + "'");
                    if (arrDr.Length == 1)
                    {
                        //AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), Insurance_Num);
                    }
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            //AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, Insurance_Num);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }
        /// <summary>
        /// hàm thực hiện việc tính phàn trăm bảo hiểm
        /// </summary>
        private void TinhPtramBhyt()
        {
            try
            {
                LaySoTheBhyt();
                if (!string.IsNullOrEmpty(GetSoBHYT) && GetSoBHYT.Length >= 15)
                {
                    if ((!string.IsNullOrEmpty(GetSoBHYT)) && (!string.IsNullOrEmpty(txtMaKcbbd.Text)))
                    {
                        var objLuotkham = new KcbLuotkham();
                        objLuotkham.MaNoicapBhyt = Utility.sDbnull(txtMaNoiCaptheBHYT.Text);
                        objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(txtTenKcbbd.Text);
                        objLuotkham.MatheBhyt = GetSoBHYT;
                        objLuotkham.MaDoituongBhyt = txtMadauthe.Text;
                        objLuotkham.DungTuyen = !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1)));
                        objLuotkham.MadtuongSinhsong = Utility.sDbnull(cboMaKhuvuc.SelectedValue);
                        objLuotkham.GiayBhyt = Utility.Bool2byte(chkGiayBHYT.Checked);
                        objLuotkham.MaKcbbd = Utility.sDbnull(txtMaKcbbd.Text);
                        objLuotkham.IdDoituongKcb = _idDoituongKcb;
                        objLuotkham.MaLydovaovien = LayMaLydoVv();//đúng tuyến=1; cấp cứu =2; trái tuyến =3; thông tuyến=4
                        objLuotkham.MaQuyenloi = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text);
                        THU_VIEN_CHUNG.TinhPtramBhyt(objLuotkham);
                        txtPtramBHYT.Text = objLuotkham.PtramBhyt.ToString();
                        txtptramDauthe.Text = objLuotkham.PtramBhytGoc.ToString();
                    }
                    else
                    {
                        txtPtramBHYT.Text = "0";
                        txtptramDauthe.Text = "0";
                    }
                }
                else
                {
                    txtPtramBHYT.Text = "0";
                    txtptramDauthe.Text = "0";
                }
            }
            catch (Exception)
            {
                txtPtramBHYT.Text = "0";
                txtptramDauthe.Text = "0";
            }
        }
        void LoadNoiDKKCBBD()
        {
            string maDiachinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_DANGKY_CACHXACDINH_NOIDKKCBBD", true) == "0" ? txtNoiphattheBHYT.Text : txtMaNoiCaptheBHYT.Text;
            string vCliniCode = maDiachinh + txtMaKcbbd.Text.Trim();
            DataTable dataTable = new KCB_DANGKY().GetClinicCode(vCliniCode);
            if (dataTable.Rows.Count > 0)
            {
                string strClinicName = dataTable.Rows[0][DmucNoiKCBBD.Columns.TenKcbbd].ToString();
                txtTenKcbbd.Text = strClinicName.Trim();
            }
        }
        private string goiy_thongtuyen = "-1";
        /// <summary>
        /// hàm thực hiện việc load thông tin của nơi khám chữa bệnh ban đầu
        /// </summary>
        private void LoadClinicCode()
        {
            try
            {
                string maDiachinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_DANGKY_CACHXACDINH_NOIDKKCBBD", true) == "0" ? txtNoiphattheBHYT.Text : txtMaNoiCaptheBHYT.Text;
                //Lấy mã Cơ sở KCBBD
                string vCliniCode = maDiachinh + txtMaKcbbd.Text.Trim();
                string strClinicName = "";
                string noidkkcbbd = txtMaKcbbd.Text.Trim();
                bool isthongtuyen = false;
                int ma_hang = -1;//Mã hạng bệnh viện
                globalVariables.gv_strTuyenBHYT = THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TUYEN", "3", true);
                string thongtuyen = THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_THONGTUYEN", "", false);
                DataTable dataTable = _KCB_DANGKY.GetClinicCode(vCliniCode);
                if (dataTable.Rows.Count > 0)
                {
                    strClinicName = dataTable.Rows[0][DmucNoiKCBBD.Columns.TenKcbbd].ToString();
                    txtTenKcbbd.Text = strClinicName.Trim();
                    string tuyenclinic = dataTable.Rows[0][DmucNoiKCBBD.Columns.MaTuyen].ToString();
                    ma_hang = Utility.Int32Dbnull(dataTable.Rows[0][DmucNoiKCBBD.Columns.MaHang], -1);
                    //  Utility.SetMsg(lblClinicName, strClinicName, string.IsNullOrEmpty(txtNoiDKKCBBD.Text));
                    if (!string.IsNullOrEmpty(thongtuyen))
                    {
                        string[] tuyen = thongtuyen.Split(',');
                        foreach (string s in tuyen)
                        {
                            if (s == Utility.sDbnull(tuyenclinic, -1)) isthongtuyen = true;
                        }
                    }
                }
                else
                {
                    // Utility.SetMsg(lblClinicName, strClinicName, false);
                    txtTenKcbbd.Text = "";
                }
                // lblClinicName.Visible = dataTable.Rows.Count > 0;
                lnkThem.Visible = dataTable.Rows.Count <= 0;
                //txtNamePresent.Text = strClinicName;
                //Check đúng tuyến cần lấy mã nơi cấp BHYT+mã kcbbd thay vì mã cơ sở kcbbd
                if (!chkCapCuu.Checked) //Nếu không phải trường hợp cấp cứu
                {

                    if (globalVariables.gv_blnThongTuyen)
                    {
                        if (globalVariables.ACCOUNTCLINIC.Substring(0, 2) == txtMaNoiCaptheBHYT.Text.Trim() &&
                            globalVariables.ACCOUNTCLINIC.Substring(2, 3) == noidkkcbbd)
                        {
                            chkDungtuyen.Checked = true;
                            chkTraiTuyen.Checked = false;
                            chkThongtuyen.Checked = false;
                            goiy_thongtuyen = chkDungtuyen.Text;
                        }
                        else if (globalVariables.ACCOUNTCLINIC.Substring(0, 2) == txtMaNoiCaptheBHYT.Text.Trim() &&
                                 globalVariables.ACCOUNTCLINIC.Substring(2, 3) != noidkkcbbd && isthongtuyen)
                        {
                            chkDungtuyen.Checked = false;
                            chkTraiTuyen.Checked = false;
                            if (Utility.Int32Dbnull(globalVariables.gv_strTuyenBHYT) < 3) chkTraiTuyen.Checked = true; // bệnh viện tuyến tỉnh trở lên không có thông tuyến
                            else chkThongtuyen.Checked = true;
                            goiy_thongtuyen = chkThongtuyen.Text;

                        }
                        else if (globalVariables.ACCOUNTCLINIC.Substring(0, 2) == txtMaNoiCaptheBHYT.Text.Trim() &&
                          globalVariables.ACCOUNTCLINIC.Substring(2, 3) != noidkkcbbd && !isthongtuyen)
                        {
                            chkDungtuyen.Checked = false;
                            chkTraiTuyen.Checked = true;
                            chkThongtuyen.Checked = false;
                            goiy_thongtuyen = chkTraiTuyen.Text;
                        }
                        else if (globalVariables.ACCOUNTCLINIC.Substring(0, 2) != txtMaNoiCaptheBHYT.Text.Trim())
                        {
                            chkDungtuyen.Checked = false;
                            chkTraiTuyen.Checked = true;
                            if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                            //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                            {
                                chkTraiTuyen.Checked = !(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtMaNoiCaptheBHYT.Text.Trim() + txtMaKcbbd.Text.Trim()) ||
                                                       (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtMaNoiCaptheBHYT.Text.Trim() + txtMaKcbbd.Text.Trim()) && chkChuyenVien.Checked));
                            }
                            chkThongtuyen.Checked = false;
                            goiy_thongtuyen = chkTraiTuyen.Checked ? chkTraiTuyen.Text : "-1";
                        }
                        else
                        {
                            chkDungtuyen.Checked = false;
                            chkTraiTuyen.Checked = false;
                            chkThongtuyen.Checked = false;
                        }
                    }
                    else
                    {
                        if (globalVariables.ACCOUNTCLINIC.Substring(0, 2) == txtMaNoiCaptheBHYT.Text.Trim() &&
                            globalVariables.ACCOUNTCLINIC.Substring(2, 3) == noidkkcbbd)
                        {
                            chkDungtuyen.Checked = true;
                            chkTraiTuyen.Checked = false;
                            chkThongtuyen.Checked = false;
                            goiy_thongtuyen = chkDungtuyen.Text;
                        }
                        else
                        {
                            //log.Trace("tu dong check trai tuyen: " + globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN);
                            if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                            //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                            {
                                chkTraiTuyen.Checked = !(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtMaNoiCaptheBHYT.Text.Trim() + txtMaKcbbd.Text.Trim()) ||
                                                       (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtMaNoiCaptheBHYT.Text.Trim() + txtMaKcbbd.Text.Trim()) && chkChuyenVien.Checked));
                                goiy_thongtuyen = chkTraiTuyen.Checked ? chkTraiTuyen.Text : "-1";

                            }
                            //if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                            //    //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                            //    chkTraiTuyen.Checked =
                            //        !(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiCaptheBHYTBHYT.Text.Trim() +
                            //                                                txtNoiDKKCBBD.Text.Trim()) ||
                            //          (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiCaptheBHYTBHYT.Text.Trim() +
                            //                                                  txtNoiDKKCBBD.Text.Trim()) &&
                            //           chkChuyenVien.Checked));
                        }

                    }
                }
                else //Nếu là BN cấp cứu
                {
                    if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                        //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                        chkTraiTuyen.Checked =
                            (!(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtMaNoiCaptheBHYT.Text.Trim() + txtMaKcbbd.Text.Trim()) ||
                               (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtMaNoiCaptheBHYT.Text.Trim() + txtMaKcbbd.Text.Trim()) &&
                                chkChuyenVien.Checked))) && (!chkCapCuu.Checked);
                }

                if (Utility.sDbnull(cboMaKhuvuc.SelectedValue) != "")
                {
                    if (chkTraiTuyen.Checked)
                        chkTraiTuyen.Checked = false;
                }
                TinhPtramBhyt();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
            finally
            {
                lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            }
        }
        private void txtNoiphattheBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtNoiphattheBHYT.Text.Length <= 0)
                {
                    txtMaQuyenloi_BHYT.Focus();
                    txtMaQuyenloi_BHYT.Select(txtMaQuyenloi_BHYT.Text.Length, 0);
                }
            }
        }
        private void txtNoiphattheBHYT_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_maDoituongKcb == "DV") return;
            if (txtNoiphattheBHYT.Text.Length < 2)
            {
                Utility.SetMsg(lblNoiCapThe, "", false);
                return;
            }
            else
                GetNoiDangKy();
            if (!IsValidTheBhyt()) return;
            txtOthu4.Focus();
            txtOthu4.SelectAll();

        }
        private void txtMaQuyenloi_BHYT_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_maDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtMaQuyenloi_BHYT.Text.Length <= 0)
            {
                txtMadauthe.Focus();
                if (txtMadauthe.Text.Length > 0) txtMadauthe.Select(txtMadauthe.Text.Length, 0);
            }
            if (txtMaQuyenloi_BHYT.Text.Length < 1) return;
            if (!IsValidTheBhyt()) return;
            TinhPtramBhyt();
            txtNoiphattheBHYT.Focus();
            txtNoiphattheBHYT.SelectAll();
        }
        private void txtMadauthe_TextChanged(object sender, EventArgs e)
        {
            //if (_maDoituongKcb == "DV") return;
            //if (txtMadauthe.Text.Length < 2) return;
            //if (!IsValidTheBhyt()) return;
            //TinhPtramBhyt();
            //txtMaQuyenloi_BHYT.Focus();
            //txtMaQuyenloi_BHYT.SelectAll();
        }
        private void txtMadauthe_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    string MA_BHYT = Laymathe_BHYT();
            //    string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
            //    if (MA_BHYT.Length == 15 && noiDangky.Length == 5)
            //        FindPatientIDbyBhyt(MA_BHYT, noiDangky);
            //}
        }

        private void txtMaQuyenloi_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = Laymathe_BHYT();
                string noiDangky = txtMaNoiCaptheBHYT.Text.Trim() + txtMaKcbbd.Text.Trim();
                //if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBhyt(MA_BHYT, noiDangky);
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtMaQuyenloi_BHYT.Text.Length <= 0)
                {
                    txtMadauthe.Focus();
                    txtMadauthe.Select(txtMadauthe.Text.Length, 0);
                }
                return;
            }
            if (txtMaQuyenloi_BHYT.Text.Length == 1 && (Char.IsDigit((char)e.KeyCode) || Char.IsLetter((char)e.KeyCode)))
            {
                if (txtNoiphattheBHYT.Text.Length > 0)
                {
                    // txtNoiDongtrusoKCBBD.Text = ((char)e.KeyCode).ToString() + txtNoiDongtrusoKCBBD.Text.Substring(1);
                    txtNoiphattheBHYT.Focus();
                    txtNoiphattheBHYT.SelectAll();
                }
                return;
            }

        }

        private void txtNoiDongtrusoKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                //Không cần tìm
                //string MA_BHYT =  Laymathe_BHYT();
                //if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                //return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtMaNoiCaptheBHYT.Text.Length <= 0)
                {
                    txtOthu6.Focus();
                    txtOthu6.Select(txtOthu6.Text.Length, 0);
                }
            }
        }

        private void txtOthu4_KeyDown(object sender, KeyEventArgs e)
        {
            //_hasjustpressBackKey = false;
            //if (e.KeyCode == Keys.Enter)
            //{
            //    string MA_BHYT =  Laymathe_BHYT();
            //    string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
            //      if (MA_BHYT.Length == 15 && noiDangky.Length == 5)  FindPatientIDbyBhyt(MA_BHYT, noiDangky);
            //}
            //else if (e.KeyCode == Keys.Back)
            //{
            //    _hasjustpressBackKey = true;
            //    if (txtOthu4.Text.Length <= 0)
            //    {
            //        txtNoiphattheBHYT.Focus();
            //        txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
            //    }
            //}
        }

        private void txtOthu5_KeyDown(object sender, KeyEventArgs e)
        {
            //_hasjustpressBackKey = false;
            //if (e.KeyCode == Keys.Enter)
            //{
            //    string MA_BHYT =  Laymathe_BHYT();
            //    string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
            //    if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBhyt(MA_BHYT, noiDangky);
            //}
            //else if (e.KeyCode == Keys.Back)
            //{
            //    _hasjustpressBackKey = true;
            //    if (txtOthu5.Text.Length <= 0)
            //    {
            //        txtOthu4.Focus();
            //        txtOthu4.Select(txtOthu4.Text.Length, 0);
            //    }
            //}
        }

        private void txtOthu6_KeyDown(object sender, KeyEventArgs e)
        {
            _hasjustpressBackKey = false;

            if (e.KeyCode == Keys.Enter)
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_KIEMTRA20KYTU_BHYT", "0", false) == "1")
                {
                    return;
                }
                string MA_BHYT = Laymathe_BHYT();
                string noiDangky = txtMaNoiCaptheBHYT.Text.Trim() + txtMaKcbbd.Text.Trim();
                // if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBhyt(MA_BHYT, noiDangky);
            }
            else if (e.KeyCode == Keys.Back)
            {
                _hasjustpressBackKey = true;
                if (txtOthu6.Text.Length <= 0)
                {
                    txtOthu5.Focus();
                    txtOthu5.Select(txtOthu5.Text.Length, 0);
                }
            }
        }
        private bool isAutobinding = true;
        private void FindPatientIDbyBhyt(string insuranceNum, string noidangky)
        {
            try
            {
                DataTable temdt = SPs.KcbTimkiembenhnhantheomathebhyt(insuranceNum + noidangky).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    if (!KT_20_Ky_Tu_BHYT(temdt.Rows[0]["id_benhnhan"].ToString(), insuranceNum, noidangky)) return;
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), insuranceNum, noidangky);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr =
                        temdt.Select(KcbLuotkham.Columns.MatheBhyt + "='" + insuranceNum + "' AND " +
                                     KcbLuotkham.Columns.NoiDongtrusoKcbbd + "= '" + noidangky.Substring(0, 2) +
                                     "' AND " + KcbLuotkham.Columns.MaKcbbd + "= '" + noidangky.Substring(2, 3) + "'");
                    if (arrDr.Length == 1)
                    {
                        if (!KT_20_Ky_Tu_BHYT(temdt.Rows[0]["id_benhnhan"].ToString(), insuranceNum, noidangky)) return;
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), insuranceNum, noidangky);
                    }
                    else
                    {
                        isAutobinding = false;
                        var chonBn = new frm_CHON_BENHNHAN();
                        chonBn.temdt = temdt;
                        chonBn.ShowDialog();
                        if (!chonBn.mv_bCancel)
                        {
                            if (!KT_20_Ky_Tu_BHYT(temdt.Rows[0]["id_benhnhan"].ToString(), insuranceNum, noidangky)) return;
                            AutoFindLastExamandFetchIntoControls(chonBn.Patient_ID, insuranceNum, noidangky);
                            //return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }
        private bool KT_20_Ky_Tu_BHYT(string patientId, string sobhyt, string noiDangky)
        {

            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_KIEMTRA20KYTU_BHYT", "0", false) == "1")
            {
                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(patientId);
                if (!string.IsNullOrEmpty(sobhyt))
                {
                    sqlQuery.And(KcbLuotkham.Columns.MatheBhyt).IsEqualTo(sobhyt).And(KcbLuotkham.Columns.NoiDongtrusoKcbbd).IsEqualTo(noiDangky.Substring(0, 2))
                        .And(KcbLuotkham.Columns.MaKcbbd).IsEqualTo(noiDangky.Substring(2, 3));
                }
                sqlQuery.OrderDesc(KcbLuotkham.Columns.NgayTiepdon);
                var objPatientExam = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (objPatientExam != null)
                {
                    //txtIdBenhnhan.Text = patientId;
                    //txtMaLankham.Text = Utility.sDbnull(objPatientExam.MaLuotkham);
                    if (txtMaNoiCaptheBHYT.Text.ToUpper() !=
                        Utility.sDbnull(objPatientExam.NoiDongtrusoKcbbd).ToUpper() ||
                        txtMaKcbbd.Text.ToUpper() != Utility.sDbnull(objPatientExam.MaKcbbd).ToUpper())
                    { return false; }

                }
                return true;
            }
            else
                return true;

        }
        private bool bConLanKhamChuaThanhToan = false;
        private bool bConLanKhamChuaThanhToan_mess = false;
        string ma_luotkhamgannhatchuathanhtoan = "";
        private void AutoFindLastExamandFetchIntoControls(string patientId, string sobhyt, string noiDangky)
        {
            //try
            //{
            //    if (!Utility.CheckLockObject(m_strMaluotkham, "Tiếp đón", "TD"))
            //        return;
            //    //Trả lại mã lượt khám nếu chưa được dùng đến
            //    ResetLuotkham();
            //    SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
            //        .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(patientId);
            //    if (!string.IsNullOrEmpty(sobhyt))
            //    {
            //        sqlQuery.And(KcbLuotkham.Columns.MatheBhyt)
            //            .IsEqualTo(sobhyt)
            //            .And(KcbLuotkham.Columns.NoiDongtrusoKcbbd)
            //            .IsEqualTo(noiDangky.Substring(0, 2))
            //            .And(KcbLuotkham.Columns.MaKcbbd)
            //            .IsEqualTo(noiDangky.Substring(2, 3));
            //    }
            //    sqlQuery.OrderDesc(KcbLuotkham.Columns.NgayTiepdon);

            //    var objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
            //    if (objLuotkham != null)
            //    {
            //        txtIdBenhnhan.Text = patientId;
            //        txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
            //        m_strMaluotkham = objLuotkham.MaLuotkham;
            //        m_enAct = action.Update;
            //        AllowTextChanged = false;
            //        LoadThongtinBenhnhan();
            //        CanhbaoInphoi();
            //        LaydanhsachdangkyKcb();
            //        string ngayKham = globalVariables.SysDate.ToString("dd/MM/yyyy");
            //        string tenkhoa = "";
            //        ma_luotkhamgannhatchuathanhtoan = "";
            //        if (!NotPayment(txtIdBenhnhan.Text.Trim(), ref ma_luotkhamgannhatchuathanhtoan, ref ngayKham, ref tenkhoa))//Đã thanh toán-->Có thể thê lần khám. Nhưng kiểm tra ngày hẹn xem có được phép khám tiếp
            //        {

            //            KcbChandoanKetluan _Info = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.MaLuotkhamColumn).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbChandoanKetluan>();
            //            if (_Info != null && _Info.SongayDieutri != null)
            //            {
            //                int soNgayDieuTri = 0;
            //                if (_Info.SongayDieutri.Value.ToString() == "")
            //                {
            //                    soNgayDieuTri = 0;
            //                }
            //                else
            //                {
            //                    soNgayDieuTri = _Info.SongayDieutri.Value;
            //                }
            //                DateTime ngaykhamcu = _Info.NgayTao; ;
            //                DateTime ngaykhammoi = globalVariables.SysDate;
            //                TimeSpan songay = ngaykhammoi - ngaykhamcu;

            //                int kt = songay.Days;
            //                int kt1 = soNgayDieuTri - kt;
            //                kt1 = Utility.Int32Dbnull(kt1);
            //                // nếu khoảng cách từ lần khám trước đến ngày hiện tại lớn hơn ngày điều trị.
            //                if (kt >= soNgayDieuTri)
            //                {
            //                    m_enAct = action.Add;
            //                    SinhMaLanKham();
            //                    if (m_enAct == action.Insert || m_enAct == action.Add)
            //                    {
            //                        dtCreateDate.Value = globalVariables.SysDate;
            //                    }
            //                    else
            //                    {
            //                        dtCreateDate.Value = _objLuotkham.NgayTiepdon;
            //                    }
            //                    //txtTongChiPhiKham.Text = "0";
            //                    m_dtDangkyPhongkham.Rows.Clear();
            //                    txtKieuKham.Select();
            //                }
            //                else if (kt < soNgayDieuTri)
            //                {
            //                    DialogResult dialogResult =
            //                        MessageBox.Show(
            //                            @"Bác Sỹ hẹn :  " + soNgayDieuTri + @"ngày" + @"\nNgày khám gần nhất:  " +
            //                            ngaykhamcu + @"\nCòn: " + kt1 + @" ngày nữa mới được tái khám" +
            //                            @"\nBạn có muốn tiếp tục thêm lần khám. Nhấn Yes để thêm lần khám mới. Nhấn No để về trạng thái cập nhật", @"Thông Báo", MessageBoxButtons.YesNo);

            //                    if (dialogResult == DialogResult.Yes)
            //                    {
            //                        m_enAct = action.Add;
            //                        SinhMaLanKham();
            //                        if (m_enAct == action.Insert || m_enAct == action.Add)
            //                        {
            //                            dtCreateDate.Value = globalVariables.SysDate;
            //                        }
            //                        else
            //                        {
            //                            dtCreateDate.Value = _objLuotkham.NgayTiepdon;
            //                        }
            //                        //Reset dịch vụ KCB
            //                        //txtTongChiPhiKham.Text = "0";
            //                        m_dtDangkyPhongkham.Rows.Clear();
            //                        txtKieuKham.Select();
            //                    }
            //                    else if (dialogResult == DialogResult.No)
            //                    {
            //                        ClearControl();
            //                        SinhMaLanKham();
            //                        return;
            //                    }
            //                }
            //            }
            //            else//Chưa thăm khám-->Để nguyên trạng thái cập nhật
            //            {
            //            }
            //        }
            //        else//Còn lần khám chưa thanh toán-->Kiểm tra
            //        {
            //            //nếu là ngày hiện tại thì đặt về trạng thái sửa
            //            if (ngayKham == "NOREG" || ngayKham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
            //            {
            //                //LoadThongtinBenhnhan();
            //                if (ngayKham == "NOREG")//Bn chưa đăng ký phòng khám nào cả. 
            //                {
            //                    //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
            //                    if (globalVariables.SysDate.ToString("dd/MM/yyyy") == dtpInputDate.Value.ToString("dd/MM/yyyy"))
            //                    {
            //                        m_enAct = action.Update;

            //                        Utility.ShowMsg(
            //                           "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
            //                        //LaydanhsachdangkyKCB();
            //                        txtTEN_BN.Select();
            //                    }
            //                    else//Thêm lần khám cho ngày mới
            //                    {
            //                        m_enAct = action.Add;
            //                        SinhMaLanKham();
            //                        //Reset dịch vụ KCB
            //                        //txtTongChiPhiKham.Text = "0";
            //                        m_dtDangkyPhongkham.Rows.Clear();
            //                        txtKieuKham.Select();
            //                    }
            //                }
            //                else//Quay về trạng thái sửa
            //                {
            //                    m_enAct = action.Update;

            //                    Utility.ShowMsg(
            //                       "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
            //                    //LaydanhsachdangkyKCB();
            //                    txtTEN_BN.Select();
            //                }
            //            }
            //            else //Không cho phép thêm lần khám khác nếu chưa thanh toán lần khám của ngày hôm trước
            //            {
            //                if (m_enAct != action.Update)
            //                {
            //                    if (Utility.sDbnull(objLuotkham.HuongDieutri) == "2")//1: ngoại trú, 2: Điều trị ngoại trú, 3: Điều trị nội trú, 4: Điều trị nội trú ban ngày
            //                    {
            //                        bConLanKhamChuaThanhToan = true;
            //                        if (bConLanKhamChuaThanhToan_mess == false && !Utility.AcceptQuestion(string.Format("Bệnh nhân thuộc diện ĐIỀU TRỊ NGOẠI TRÚ (lần khám ngày: {0}; Mã lần khám: {1}; Khoa: {2}), CHỈ ĐƯỢC TIẾP ĐÓN CẤP CỨU. \nBạn có muốn tiếp tục?", ngayKham, ma_luotkhamgannhatchuathanhtoan, tenkhoa), this.Text, true))
            //                        {
            //                            bConLanKhamChuaThanhToan = false;
            //                            cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
            //                        }
            //                        else
            //                            chkCapCuu.Checked = true;
            //                        bConLanKhamChuaThanhToan_mess = true;
            //                    }
            //                    else
            //                    {
            //                        Utility.ShowMsg(string.Format("Bệnh nhân đang có lần khám ngày: {0} Mã lần khám: {1}; Khoa: {2} chưa thanh toán. \nCần thanh toán hết các lần đến khám bệnh của Bệnh nhân trước khi thêm lần khám mới", ngayKham, ma_luotkhamgannhatchuathanhtoan, tenkhoa));
            //                        cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
            //                    }
            //                }

            //                Utility.ShowMsg(
            //                    "Bệnh nhân đang có lần khám chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Bệnh nhân trước khi thêm lần khám mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Bệnh nhân");
            //                cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
            //            }
            //        }
            //        StatusControl();

            //        ModifyCommand();
            //        txtKieuKham.SetCode("-1");
            //        txtPhongkham.SetCode("-1");
            //        if (PropertyLib._KCBProperties.GoMaDvu)
            //        {
            //            txtExamtypeCode.SelectAll();
            //            txtExamtypeCode.Focus();
            //        }
            //        else
            //        {
            //            txtKieuKham.SelectAll();
            //            txtKieuKham.Focus();
            //        }
            //    }
            //    else
            //    {
            //        Utility.ShowMsg(
            //            "Bệnh nhân này chưa có lần khám nào-->Có thể bị lỗi dữ liệu. Đề nghị liên hệ với VNS để được giải đáp");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utility.ShowMsg("AutoFindLastExam().Exception-->" + ex.Message);
            //}
            //finally
            //{
            //    SetActionStatus();
            //    AllowTextChanged = true;
            //}
        }
        private void lnkThem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newItem = new frm_ThemnoiKCBBD();
            newItem.m_dtDataThanhPho = globalVariables.gv_dtDmucDiachinh;
            newItem.SetInfor(txtMaKcbbd.Text, txtNoiphattheBHYT.Text);
            if (newItem.ShowDialog() == DialogResult.OK)
            {
                txtMaKcbbd.Text = "";
                txtNoiphattheBHYT.Text = "";
                txtMaKcbbd.Text = newItem.txtMa.Text.Trim();
                txtNoiphattheBHYT.Text = newItem.txtMaThanhPho.Text.Trim();
                dtpBHYT_Hieuluctu.Focus();
            }
        }
        CultureInfo cultures = new CultureInfo("vi-VN");
        void txtQRCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AllowTextChanged = false;
                    string data = Utility.DoTrim(txtQRCode.Text);
                    if (data.Length > 0)
                    {
                        if (data.EndsWith("$"))
                        {
                            //Kích hoạt vùng BHYT
                            _maDoituongKcb = "BHYT";
                            cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _maDoituongKcb);
                            AllowTextChanged = true;//Change object region
                            cboDoituongKCB_SelectedIndexChanged(cboDoituongKCB, new EventArgs());
                            AllowTextChanged = false;//Tránh việc thông báo sai mã đầu thẻ khi thông tin là mã BHXH
                            //Ma the cu 
                            // DN4010114138505|5068e1baa16d205468e1bb8b204769616e67|20/10/1990|2|43747920435020434e5454205669e1bb8774204261|01 - 065|09/01/2018|-|10/01/2018|‎01090114138505|-|4| 01/08/2019|7ea4e8f446328cc-7102|$
                            //DN4010114138505
                            //Ma the moi 
                            //0204287018|486fc3a06e67205875c3a26e2048e1baa56e|30/07/1981|1|43747920435020434e5454205669e1bb8774204261|79 - 034|01/02/2021|-|20/02/2021|79020204287018|-|4| 01/01/2015|15e89ac07ee8517f-7102|4|5175e1baad6e2031322c205468c3a06e68207068e1bb912048e1bb93204368c3ad204d696e68|$
                            string mavach = data;
                            char[] delimiterChars = { '|' };
                            string[] arrayBhyt = mavach.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                            string tenbn = string.Empty;
                            string dia_chi = string.Empty;
                            string thebhyt = string.Empty;
                            if ((arrayBhyt[0] != null) && (arrayBhyt[5] != null) & (arrayBhyt[6] != null) &&
                                (arrayBhyt[7] != null))
                            {
                                thebhyt = arrayBhyt[0];
                                string ngaysinh = arrayBhyt[2];
                                string madkkcb = arrayBhyt[5];
                                DateTime tungay = Convert.ToDateTime(arrayBhyt[6], cultures);
                                if (arrayBhyt[7] != "-")
                                {
                                    DateTime denngay = Convert.ToDateTime(arrayBhyt[7], cultures);
                                    dtpBHYT_Hieulucden.Value = denngay;
                                }
                                if (arrayBhyt[12] != "-")
                                {
                                    DateTime denngay = Convert.ToDateTime(arrayBhyt[12], cultures);
                                    dtpNgaydu5nam.Value = denngay;
                                }
                                madkkcb = madkkcb.Replace("–", "-");
                                txtmathebhyt.Text = thebhyt;
                                #region bỏ vùng này do thẻ BHYT quét sẽ ra mã BHXH và từ mã này sẽ phải check thông tuyến
                                //txtMaDtuong_BHYT.Text = thebhyt.Substring(0, 2);
                                //txtMaQuyenloi_BHYT.Text = thebhyt.Substring(2, 1);
                                //txtNoiphattheBHYT.Text = thebhyt.Substring(3, 2);
                                //txtOthu4.Text = thebhyt.Substring(5, 2);
                                //txtOthu5.Text = thebhyt.Substring(7, 3);
                                //txtOthu6.Text = thebhyt.Substring(10, 5);
                                #endregion
                                string[] arraymadkkcb = madkkcb.Split(new char[] { '-' });
                                string madk = string.Empty;
                                string macso = string.Empty;
                                if (arraymadkkcb[0] != null) madk = Utility.sDbnull(arraymadkkcb[0]).Trim();
                                if (arraymadkkcb[1] != null) macso = Utility.sDbnull(arraymadkkcb[1]).Trim();
                                AllowTextChanged = true;//tự động load các giá trị nơi ĐKKCBBD, Nơi Đóng trụ sở
                                txtMaNoiCaptheBHYT.Text = madk.Trim();
                                txtMaKcbbd.Text = macso.Trim();
                                dtpBHYT_Hieuluctu.Value = tungay;
                                string[] arrayngaysinh = ngaysinh.Split(new char[] { '/' });
                                if (ngaysinh.Length > 8)
                                {
                                    dtpBOD.CustomFormat = @"dd/MM/yyyy HH:mm";
                                    dtpBOD.Value = new DateTime(Utility.Int16Dbnull(arrayngaysinh[2]),
                                        Utility.Int16Dbnull(arrayngaysinh[1]), Utility.Int16Dbnull(arrayngaysinh[0]));
                                }
                                else
                                {
                                    dtpBOD.CustomFormat = @"yyyy";
                                    dtpBOD.Value = new DateTime(
                                        Utility.Int16Dbnull(ngaysinh.Substring(ngaysinh.Length - 4, 4)), 01, 01);
                                }
                            }
                            if (arrayBhyt[1] != null)
                            {
                                tenbn = Utility.ConvertHexStrToUnicode(Utility.sDbnull(arrayBhyt[1]));
                                txtTEN_BN.Text = tenbn;
                            }
                            if (arrayBhyt[3] != null)
                            {
                                int gioitinh = Utility.Int16Dbnull(arrayBhyt[3]);
                                if (gioitinh == 1) gioitinh = 0;
                                if (gioitinh == 2) gioitinh = 1;
                                cboPatientSex.SelectedValue = gioitinh;
                            }
                            if (!string.IsNullOrEmpty(arrayBhyt[4]) && arrayBhyt[4] != "-")
                            {
                                dia_chi = Utility.ConvertHexStrToUnicode(arrayBhyt[4]);
                                txtDiachi_bhyt.Text = dia_chi;

                            }
                            if (!string.IsNullOrEmpty(arrayBhyt[10]) && arrayBhyt[10] != "-")
                            {
                                txtNguoiLienhe.Text = Utility.sDbnull(arrayBhyt[10], "");
                            }
                            if (arrayBhyt[11] != null)
                            {

                                string masinhsong = Utility.sDbnull(arrayBhyt[11]);
                                switch (masinhsong)
                                {
                                    case "4":
                                        cboMaKhuvuc.SelectedValue = "";
                                        break;
                                    case "5":
                                        cboMaKhuvuc.SelectedValue = "K1";
                                        break;
                                    case "6":
                                        cboMaKhuvuc.SelectedValue = "K2";
                                        break;
                                    case "7":
                                        cboMaKhuvuc.SelectedValue = "K3";
                                        break;
                                }
                            }


                        }
                        //else if (AutofillInforbyQRCode(data))//Bỏ phần check CMT
                        //{
                        //    txtQRCode.Clear();
                        //    txtTEN_BN.Focus();
                        //    string KCB_QRCODE_KIEMTRATRUNGTHONGTIN_SAUQUET = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_QRCODE_KIEMTRATRUNGTHONGTIN_SAUQUET", "0", true);
                        //    if (KCB_QRCODE_KIEMTRATRUNGTHONGTIN_SAUQUET == "1")
                        //        Checktrungthongtin();
                        //    else if (KCB_QRCODE_KIEMTRATRUNGTHONGTIN_SAUQUET == "2")//Kiểm tra theo CMT vừa quét để lấy bệnh nhân về
                        //    {
                        //        FindPatientIDbyCMT(txtCMT.Text.Trim());
                        //    }
                        //}
                        else
                        {
                            Utility.ShowMsg("Dữ liệu QRCode không đúng các định dạng mà hệ thống cho phép. Đề nghị kiểm tra lại");
                            txtQRCode.SelectAll();
                            txtQRCode.Focus();
                            return;
                        }
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_CHECKTHE_AUTO", "0", false) == "1")
                        {
                            if (!THU_VIEN_CHUNG.IsBaoHiem(_idLoaidoituongKcb)) return;
                            if (Utility.sDbnull(txtmathebhyt.Text) == "")
                            {
                                Utility.ShowMsg("Cần nhập mã BHXH trước khi thực hiện kiểm tra thông tuyến");
                                txtmathebhyt.Focus();
                                return;
                            }
                            var objAPIBH = new TheBHYT();
                            objAPIBH.hoTen = txtTEN_BN.Text;
                            objAPIBH.maThe = Laymathe_BHYT();
                            // trên cổng: 1: nam, 2, nữ
                            objAPIBH.ngaySinh = dtpBOD.CustomFormat != @"yyyy" ? dtpBOD.Value.ToString("dd/MM/yyyy") : dtpBOD.Value.Year.ToString();
                            BHYT_CheckCard_366(objAPIBH, sthongbao.Trim(), ref _maketqua, true);
                            Utility.Log(this.Name, globalVariables.UserName,
                                string.Format(
                                    "Thông báo : {0} . Họ tên: {1} Mã thẻ: {2} giới tính : {3} Ngày sinh: {4} Mã CSKBĐ: {5} Hạn thẻ từ: {6} đến :{7}! ",
                                    _maketqua, objAPIBH.hoTen, objAPIBH.maThe, Convert.ToSByte(Utility.Int16Dbnull(cboPatientSex.SelectedValue) == 0 ? 1 : 2), objAPIBH.ngaySinh,
                                    string.Format("{0}{1}", txtMaNoiCaptheBHYT.Text, txtMaKcbbd.Text),
                                    dtpBHYT_Hieuluctu.Value.ToString("dd/MM/yyyy"), dtpBHYT_Hieulucden.Value.ToString("dd/MM/yyyy")));
                        }
                    }
                    txtQRCode.SelectAll();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                AllowTextChanged = true;
            }

        }
        bool Checktrungthongtin()
        {
            //if (Utility.sDbnull(txtTEN_BN.Text).Length > 0 && Utility.sDbnull(txtCMT.Text).Length > 0)
            //{
            //    //Check trùng 3 thông tin Họ tên+ CCCD+Ngày tháng năm sinh
            //    DataTable dttrungthongtin = SPs.KcbTiepdonKiemtratrungthongtin(Utility.sDbnull(txtTEN_BN.Text), Utility.sDbnull(txtCMT.Text), Utility.ByteDbnull(cboPatientSex.SelectedValue), dtpBOD.Value.ToString("dd/MM/yyyy"), Utility.sDbnull(txtSDT.Text)).GetDataSet().Tables[0];
            //    if (dttrungthongtin.Rows.Count > 0)
            //    {
            //        if (Utility.AcceptQuestion("Hệ thống phát hiện đã tồn tại người bệnh trong hệ thống trùng thông tin Họ tên+ CCCD+ giới tính như bạn đang nhập.\nNhấn yes để hiển thị danh sách người bệnh trùng thông tin.\nNhấn No để tiếp tục lưu", "Thông báo trùng thông tin", true))
            //        {
            //            frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM(Args, 0);
            //            Timkiem_Benhnhan.AutoSearch = true;
            //            Timkiem_Benhnhan.FillAndSearchData(false, "", "", Utility.sDbnull(txtTEN_BN.Text), Utility.sDbnull(txtCMT.Text), dtpBOD.Value, Utility.ByteDbnull(cboPatientSex.SelectedValue, 100), "", "-1");
            //            if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //            {
            //                txtNoiDKKCBBD.Clear();
            //                txtNoiphattheBHYT.Clear();
            //                isAutoFinding = true;
            //                FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
            //                isAutoFinding = false;
            //                return true;
            //            }
            //        }
            //        else
            //            return false;
            //    }
            //    else
            //        return false;
            //}
            return false;
        }
        private void FindPatient(string patient_ID)
        {
            try
            {
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql =
                    "Select id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan where id_benhnhan like '%" +
                    patient_ID + "%'";

                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty, string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select("id_benhnhan=" + patient_ID);
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty, string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }
        private void FindPatientIDbyCMT(string CMT)
        {
            try
            {
                DataTable temdt = SPs.KcbTimkiembenhnhantheosocmt(CMT).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty, string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select(KcbDanhsachBenhnhan.Columns.Cmt + "='" + CMT + "'");
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty, string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }
        bool AutofillInforbyQRCode(string QRCode)
        {
            try
            {
                //Cấu trúc: 0 số CMT mới|1 Số CMT cũ| 2 Họ tên| 3 Năm sinh ddMMyyyy| 4 Giới tính(Nam, Nữ)| 5.1 Địa chỉ thường trú, 5.2 Quê quán|Ngày cấp căn cước ddMMyyyy
                string mavach = txtQRCode.Text;
                char[] delimiterChars = { '|' };
                string[] arrayBhyt = mavach.Split(delimiterChars);
                if (arrayBhyt.Length > 5)
                {
                    string ten = string.Empty;
                    string namsinh = string.Empty;
                    string socancuoc = string.Empty;
                    int gioiTinh = 0;
                    string diaChi = string.Empty;

                    ten = arrayBhyt[2];
                    socancuoc = arrayBhyt[0];
                    namsinh = arrayBhyt[3];
                    gioiTinh = arrayBhyt[4].ToUpper() == "NAM" ? 0 : 1;
                    if (gioiTinh == 2) gioiTinh = 1;

                    diaChi = arrayBhyt[5];
                    txtmathebhyt.Text = socancuoc;
                    txtCMT.Text = socancuoc;
                    txtTEN_BN.Text = ten;
                    txtDiachi_bhyt._Text = diaChi;
                    cboPatientSex.SelectedValue = gioiTinh;
                    if (namsinh.Length > 8)
                    {
                        dtpBOD.CustomFormat = @"dd/MM/yyyy";
                        dtpBOD.Value = new DateTime(Utility.Int16Dbnull(namsinh.Substring(4, 4)),
                            Utility.Int16Dbnull(namsinh.Substring(2, 2)), Utility.Int16Dbnull(namsinh.Substring(0, 2)));
                    }
                    else
                    {
                        dtpBOD.CustomFormat = @"yyyy";
                        dtpBOD.Value = new DateTime(
                            Utility.Int16Dbnull(namsinh.Substring(namsinh.Length - 4, 4)), 01, 01);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }

        }
        #region ChecktheBHYT
        string sthongbao = "";
        public KQLichSuKCB objLichSuKcb2018;
        private string _maketqua = "";
        DataTable _dtLichsuKcb = new DataTable();
        private bool BHYT_CheckCard_366(TheBHYT objApiTheBhyt, string sthongbao, ref string maketqua, bool sSave)
        {
            try
            {


                Utility.SetMsg(lblThongtuyen, "Đang kiểm tra thông tuyến BHYT. Vui lòng chờ trong giây lát...", false);
                objLichSuKcb2018 = new CheckTheBH().KiemTraTheBh366(objApiTheBhyt);
                if (objLichSuKcb2018 == null) return false;
                maketqua = Utility.sDbnull(objLichSuKcb2018.maKetQua, "");
                if (objLichSuKcb2018.maKetQua != "000" && objLichSuKcb2018.maKetQua != "001" && objLichSuKcb2018.maKetQua != "002" &&
                    objLichSuKcb2018.maKetQua != "003" && objLichSuKcb2018.maKetQua != "004")
                {
                    var frm = new FrmThongTinBHYT();
                    frm.Kcbnhanlichsu = objLichSuKcb2018;
                    frm.ShowDialog();
                    return false;
                }
                if (objLichSuKcb2018.maKetQua == "000")
                {
                    try
                    {
                        if (Utility.sDbnull(objLichSuKcb2018.hoTen) != "")
                            txtTEN_BN.Text = Utility.sDbnull(objLichSuKcb2018.hoTen);
                        dtpBHYT_Hieuluctu.Value = Convert.ToDateTime(objLichSuKcb2018.gtTheTu, cultures);
                        dtpBHYT_Hieulucden.Value = Convert.ToDateTime(objLichSuKcb2018.gtTheDen, cultures);
                        string makhuvuc = Utility.sDbnull(objLichSuKcb2018.maKV);
                        if (!string.IsNullOrEmpty(makhuvuc))
                        {
                            cboMaKhuvuc.SelectedValue = Utility.sDbnull(makhuvuc);
                        }
                        else
                        {
                            cboMaKhuvuc.SelectedValue = "";
                        }
                        if (objLichSuKcb2018.ngayDu5Nam.Length > 1)
                        {
                            dtpNgaydu5nam.Value = Convert.ToDateTime(objLichSuKcb2018.ngayDu5Nam, cultures);
                        }
                        string noidkkcbtrenthe = string.Format("{0}{1}", txtMaNoiCaptheBHYT.Text.Trim(), txtMaKcbbd.Text);
                        if (Utility.sDbnull(objLichSuKcb2018.maThe, "") != "")
                        {
                            if (!string.IsNullOrEmpty(noidkkcbtrenthe) && noidkkcbtrenthe != objLichSuKcb2018.maDKBD)
                            {
                                Utility.ShowMsg(string.Format("Thông tin khám chữa bệnh ban đầu của người bệnh nhập là {0} vào khác so với dữ liệu trên cổng BHYT là {1}",
                                        noidkkcbtrenthe, objLichSuKcb2018.maDKBD), "Thông báo", MessageBoxIcon.Warning);
                            }
                            if (objLichSuKcb2018.gioiTinh != null)
                            {
                                if (cboPatientSex.Text.ToUpper().Trim() != objLichSuKcb2018.gioiTinh.Trim().ToUpper())
                                {
                                    Utility.ShowMsg(string.Format("Thông tin giới tính của người bệnh nhập là ({0}) vào khác so với dữ liệu trên cổng BHYT là ({1}) ",
                                     cboPatientSex.Text.ToUpper().Trim(), objLichSuKcb2018.gioiTinh.Trim().ToUpper()), "Thông báo", MessageBoxIcon.Warning);
                                }
                                int gioitinh = 0;
                                if (objLichSuKcb2018.gioiTinh.Trim().ToUpper() == "NAM") gioitinh = 0;
                                if (objLichSuKcb2018.gioiTinh.Trim().ToUpper() == "NỮ") gioitinh = 1;
                                cboPatientSex.SelectedValue = gioitinh;
                            }
                            txtmathebhyt.Text = Utility.sDbnull(objLichSuKcb2018.maThe);
                            txtMaNoiCaptheBHYT.Text = Utility.sDbnull(objLichSuKcb2018.maDKBD).Substring(0, 2);
                            txtMaKcbbd.Text = Utility.sDbnull(objLichSuKcb2018.maDKBD).Substring(2, 3);
                            txtsosobhxh.Text = objLichSuKcb2018.maSoBHXH;

                        }
                        if (Utility.sDbnull(objLichSuKcb2018.diaChi, "") != "")
                        {
                            txtDiachi_bhyt.Text = objLichSuKcb2018.diaChi;
                        }
                        if (objLichSuKcb2018.dsLichSuKCB2018 != null)
                        {
                            _dtLichsuKcb = ConvertTableToList.dtLichSuKCB(objLichSuKcb2018.dsLichSuKCB2018).Rows.Cast<DataRow>().Take(5).CopyToDataTable();
                        }
                        if (sSave)
                        {
                            var frm = new FrmThongTinBHYT();
                            frm.Kcbnhanlichsu = objLichSuKcb2018;
                            frm.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg(ex.ToString());
                    }
                }
                if (objLichSuKcb2018.maKetQua == "001" || objLichSuKcb2018.maKetQua == "002")
                {
                    Utility.ShowMsg(objLichSuKcb2018.ghiChu, "Thông báo");
                    return true;
                }
                if (objLichSuKcb2018.maKetQua == "004")
                {
                    if (sthongbao == "1")
                    {
                        Utility.ShowMsg(objLichSuKcb2018.ghiChu, "Thông báo");
                    }
                    else
                    {
                        var frm = new FrmThongTinBHYT();
                        frm.Kcbnhanlichsu = objLichSuKcb2018;
                        frm.ShowDialog();
                        if (frm.Chapnhan)
                        {
                            if (Utility.sDbnull(objLichSuKcb2018.hoTen) != "")
                                txtTEN_BN.Text = Utility.sDbnull(objLichSuKcb2018.hoTen);
                            if (Utility.sDbnull(objLichSuKcb2018.maThe, "") != "")
                            {
                                txtmathebhyt.Text = objLichSuKcb2018.maThe; //BT
                                txtMaNoiCaptheBHYT.Text = Utility.sDbnull(objLichSuKcb2018.maDKBD).Substring(0, 2);
                                txtMaKcbbd.Text = Utility.sDbnull(objLichSuKcb2018.maDKBD).Substring(2, 3);
                                string noidkkcbtrenthe = string.Format("{0}{1}", txtMaNoiCaptheBHYT.Text.Trim(),
                                    txtMaKcbbd.Text);
                                txtsosobhxh.Text = objLichSuKcb2018.maSoBHXH;
                                if (!string.IsNullOrEmpty(noidkkcbtrenthe) && noidkkcbtrenthe != Utility.sDbnull(objLichSuKcb2018.maDKBDMoi, objLichSuKcb2018.maDKBD))
                                {
                                    Utility.ShowMsg(
                                        string.Format(
                                            "Thông tin khám chữa bệnh ban đầu của người bệnh nhập là {0} vào khác so với dữ liệu trên cổng BHYT là {1}",
                                            noidkkcbtrenthe, Utility.sDbnull(objLichSuKcb2018.maDKBDMoi, objLichSuKcb2018.maDKBD)), "Thông báo", MessageBoxIcon.Warning);
                                }

                                if (objLichSuKcb2018.gioiTinh != null)
                                {
                                    if (cboPatientSex.Text.ToUpper().Trim() != objLichSuKcb2018.gioiTinh.Trim().ToUpper())
                                    {
                                        Utility.ShowMsg(
                                            string.Format(
                                                "Thông tin giới tính của người bệnh nhập là {0} vào khác so với dữ liệu trên cổng BHYT là {1}",
                                                cboPatientSex.Text.ToUpper().Trim(),
                                                objLichSuKcb2018.gioiTinh.Trim().ToUpper()), "Thông báo");
                                    }
                                    int gioitinh = 0;
                                    if (objLichSuKcb2018.gioiTinh.Trim().ToUpper() == "NAM") gioitinh = 0;
                                    if (objLichSuKcb2018.gioiTinh.Trim().ToUpper() == "NỮ") gioitinh = 1;
                                    cboPatientSex.SelectedValue = gioitinh;
                                }
                            }
                            if (Utility.sDbnull(objLichSuKcb2018.gtTheTu, "") != "")
                            {
                                dtpBHYT_Hieuluctu.Value = Convert.ToDateTime(objLichSuKcb2018.gtTheTu, cultures);
                            }
                            if (Utility.sDbnull(objLichSuKcb2018.gtTheDen, "") != "")
                            {
                                dtpBHYT_Hieulucden.Value = Convert.ToDateTime(objLichSuKcb2018.gtTheDen, cultures);
                            }
                            if (Utility.sDbnull(txtDiachi_bhyt.Text, "") == "" &&
                                Utility.sDbnull(objLichSuKcb2018.diaChi, "") != "")
                            {
                                txtDiachi_bhyt.Text = objLichSuKcb2018.diaChi;
                            }
                            _dtLichsuKcb = frm.dtLichSuKCB;
                        }
                    }
                }
                if (objLichSuKcb2018.maKetQua == "003")
                {

                    if (sthongbao == "1")
                    {
                        Utility.ShowMsg(objLichSuKcb2018.ghiChu, "Thông báo");
                    }
                    else
                    {
                        var frm = new FrmThongTinBHYT();
                        frm.Kcbnhanlichsu = objLichSuKcb2018;
                        frm.ShowDialog();
                        if (frm.Chapnhan)
                        {

                            if (Utility.sDbnull(objLichSuKcb2018.hoTen) != "")
                                txtTEN_BN.Text = Utility.sDbnull(objLichSuKcb2018.hoTen);
                            if (Utility.sDbnull(objLichSuKcb2018.maTheMoi, "") != "")
                            {

                                txtmathebhyt.Text = objLichSuKcb2018.maTheMoi; //BT 
                                if (!string.IsNullOrEmpty(objLichSuKcb2018.maDKBDMoi) && objLichSuKcb2018.maDKBDMoi.Trim().Length == 5)
                                {
                                    txtMaNoiCaptheBHYT.Text = Utility.sDbnull(objLichSuKcb2018.maDKBDMoi).Substring(0, 2);
                                    txtMaKcbbd.Text = Utility.sDbnull(objLichSuKcb2018.maDKBDMoi).Substring(2, 3);
                                }
                                else
                                {
                                    txtMaNoiCaptheBHYT.Text = Utility.sDbnull(objLichSuKcb2018.maDKBD).Substring(0, 2);
                                    txtMaKcbbd.Text = Utility.sDbnull(objLichSuKcb2018.maDKBD).Substring(2, 3);
                                }

                                string noidkkcbtrenthe = string.Format("{0}{1}", txtMaNoiCaptheBHYT.Text.Trim(), txtMaKcbbd.Text);
                                if (!string.IsNullOrEmpty(noidkkcbtrenthe) && noidkkcbtrenthe != objLichSuKcb2018.maDKBD)
                                {
                                    Utility.ShowMsg(
                                        string.Format(
                                            "Thông tin khám chữa bệnh ban đầu của người bệnh nhập là {0} vào khác so với dữ liệu trên cổng BHYT là {1}",
                                            noidkkcbtrenthe, objLichSuKcb2018.maDKBD), "Thông báo", MessageBoxIcon.Warning);
                                }

                                if (objLichSuKcb2018.gioiTinh != null)
                                {
                                    if (cboPatientSex.Text.ToUpper().Trim() != objLichSuKcb2018.gioiTinh.Trim().ToUpper())
                                    {
                                        Utility.ShowMsg(
                                            string.Format(
                                                "Thông tin giới tính của người bệnh nhập là {0} vào khác so với dữ liệu trên cổng BHYT là {1}",
                                                cboPatientSex.Text.ToUpper().Trim(),
                                                objLichSuKcb2018.gioiTinh.Trim().ToUpper()), "Thông báo");
                                    }
                                    int gioitinh = 0;
                                    if (objLichSuKcb2018.gioiTinh.Trim().ToUpper() == "NAM") gioitinh = 0;
                                    if (objLichSuKcb2018.gioiTinh.Trim().ToUpper() == "NỮ") gioitinh = 1;
                                    cboPatientSex.SelectedValue = gioitinh;
                                }
                            }
                            if (Utility.sDbnull(objLichSuKcb2018.gtTheTuMoi, "") != "")
                            {
                                dtpBHYT_Hieuluctu.Value = Convert.ToDateTime(objLichSuKcb2018.gtTheTuMoi, cultures);
                            }
                            if (Utility.sDbnull(objLichSuKcb2018.gtTheDenMoi, "") != "")
                            {
                                dtpBHYT_Hieulucden.Value = Convert.ToDateTime(objLichSuKcb2018.gtTheDenMoi, cultures);
                            }
                            if (Utility.sDbnull(objLichSuKcb2018.diaChi, "") != "")
                            {
                                txtDiachi_bhyt.Text = objLichSuKcb2018.diaChi;
                            }
                            _dtLichsuKcb = frm.dtLichSuKCB;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.SetMsg(lblThongtuyen, ex.Message, true);
                return false;
            }
            finally
            {
                Utility.SetMsg(lblThongtuyen, "", false);
            }
        }

        #endregion

        private void chkGiayBHYT_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgaydu5nam.Enabled = false;
            TinhPtramBhyt();
            if (chkGiayBHYT.Checked)
            {
                chkTraiTuyen.Checked = false;
                dtpNgaydu5nam.Enabled = true;
                dtpNgaydu5nam.Value = globalVariables.SysDate;
            }
            else
            {
                dtpNgaydu5nam.Enabled = false;
            }
        }
        void Kiemtrathongtuyen()
        {
            try
            {
                if (!THU_VIEN_CHUNG.IsBaoHiem(_idLoaidoituongKcb))
                {

                    return;
                }
                if (Utility.sDbnull(txtmathebhyt.Text) == "")
                {
                    Utility.ShowMsg("Cần nhập mã BHXH trước khi thực hiện kiểm tra thông tuyến");
                    txtmathebhyt.Focus();
                    return;
                }
                var objAPIBH = new TheBHYT();
                objAPIBH.hoTen = txtTEN_BN.Text;
                objAPIBH.maThe = Laymathe_BHYT();
                // trên cổng: 1: nam, 2, nữ
                objAPIBH.ngaySinh = dtpBOD.CustomFormat != @"yyyy" ? dtpBOD.Value.ToString("dd/MM/yyyy") : dtpBOD.Value.Year.ToString();
                BHYT_CheckCard_366(objAPIBH, sthongbao.Trim(), ref _maketqua, true);
                Utility.Log(this.Name, globalVariables.UserName,
                    string.Format(
                        "Thông báo : {0} . Họ tên: {1} Mã thẻ: {2} giới tính : {3} Ngày sinh: {4} Mã CSKBĐ: {5} Hạn thẻ từ: {6} đến :{7}! ",
                        _maketqua, objAPIBH.hoTen, objAPIBH.maThe, Convert.ToSByte(Utility.Int16Dbnull(cboPatientSex.SelectedValue) == 0 ? 1 : 2), objAPIBH.ngaySinh,
                        string.Format("{0}{1}", txtMaNoiCaptheBHYT.Text, txtMaKcbbd.Text),
                        dtpBHYT_Hieuluctu.Value.ToString("dd/MM/yyyy"), dtpBHYT_Hieulucden.Value.ToString("dd/MM/yyyy")));
            }

            catch (Exception ex)
            {

                Utility.ShowMsg("Check thẻ gặp lỗi: vui lòng kiểm tra kết nối cổng BHYT, tài khoản, mật khẩu: " + ex.Message);
            }
        }
        private void cmdThongtuyen_Click(object sender, EventArgs e)
        {
            Kiemtrathongtuyen();
        }

        private void lnkCheckThongtuyen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kiemtrathongtuyen();
        }

        private void chkCapCuu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCapCuu.Checked)
            {
                chkTraiTuyen.Checked = false;
                chkDungtuyen.Checked = false;
                chkThongtuyen.Checked = false;

                m_dtDoiTuong_KCB = SPs.DmucLaydulieudanhmuMaDoiTuongKCBBhyt("MA_DOITUONG_KCB", "2", "2").GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboMadoituongKCB, m_dtDoiTuong_KCB, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                cboMadoituongKCB.SelectedValue = lastCode;
            }
            LayLydoVaovien();
        }

        private void chkThongtuyen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkThongtuyen.Checked)
            {
                chkTraiTuyen.Checked = false;
                chkCapCuu.Checked = false;
                chkDungtuyen.Checked = false;

            }
            else
            {
                //txtidbenhvienchuyenden.Visible = false;
            }
            LayLydoVaovien();
        }

        private void chkDungtuyen_CheckedChanged(object sender, EventArgs e)
        {
            if (_objDoituongKcb != null && THU_VIEN_CHUNG.IsBaoHiem(_objDoituongKcb.IdLoaidoituongKcb))
            {
                if (chkDungtuyen.Checked)
                {
                    chkThongtuyen.Checked = false;
                    chkCapCuu.Checked = false;
                    chkTraiTuyen.Checked = false;
                    //nếu nơi đăng ký khám ban đầu của thẻ bảo hiểm trùng với mã cơ sở của viện đang khám thì chọn là 1.1
                    if (globalVariables.ACCOUNTCLINIC.Substring(0, 2) == txtMaNoiCaptheBHYT.Text.Trim() &&
                               globalVariables.ACCOUNTCLINIC.Substring(2, 3) == txtMaKcbbd.Text.Trim())
                    {
                        m_dtDoiTuong_KCB = SPs.DmucLaydulieudanhmuMaDoiTuongKCBBhyt("MA_DOITUONG_KCB", "1", "1.1").GetDataSet().Tables[0];
                        DataBinding.BindDataCombobox(cboMadoituongKCB, m_dtDoiTuong_KCB, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                        cboMadoituongKCB.SelectedValue = lastCode;

                    }
                    else
                    {
                        m_dtDoiTuong_KCB = SPs.DmucLaydulieudanhmuMaDoiTuongKCBBhyt("MA_DOITUONG_KCB", "1", "-1").GetDataSet().Tables[0];
                        DataBinding.BindDataCombobox(cboMadoituongKCB, m_dtDoiTuong_KCB, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                        cboMadoituongKCB.SelectedValue = lastCode;
                    }

                }
                TinhPtramBhyt();
                LayLydoVaovien();
            }
        }

        private void chkChuyenVien_CheckedChanged(object sender, EventArgs e)
        {
            cmdGetBV.Enabled = txtchandoantuyenduoi.Enabled = dt_ngaydt_tuyentruoc_tu.Enabled = dt_ngaydt_tuyentruoc_den.Enabled = txtNoichuyenden.Enabled = chkChuyenVien.Checked;
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            cmdHuy.BringToFront();
            cmdSave.Enabled = true;
            objCurrent = null;
            XoathongtinBHYT(true);
            AllowTextChanged = false;
            cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, "BHYT");
            AllowTextChanged = true;
            cboDoituongKCB_SelectedIndexChanged(cboDoituongKCB, e);
            txtQRCode.Focus();
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdHuy_Click(object sender, EventArgs e)
        {
            cmdNew.BringToFront();
            cmdSave.Enabled = false;
            XoathongtinBHYT(true);
        }
    }
}
