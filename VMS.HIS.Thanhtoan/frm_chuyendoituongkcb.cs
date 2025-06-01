using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;
using VNS.HIS.UI.NGOAITRU;
using SubSonic;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.Forms.Cauhinh;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_chuyendoituongkcb : Form
    {
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        private bool AllowTextChanged;
        private bool m_blnhasLoaded=false;
        private DataTable m_DC;
        private bool isAutoFinding;
        private bool hasjustpressBACKKey;
        byte _IdLoaidoituongKcb = 1;
        Int16 _IdDoituongKcb = 1;
        string _MaDoituongKcb = "DV";
        string _TenDoituongKcb = "Dịch vụ";
        decimal PtramBhytCu = 0m;
        decimal PtramBhytGocCu = 0m;
        
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


            txtMaDtuong_BHYT.KeyDown += txtMaDtuong_BHYT_KeyDown;
            txtMaDtuong_BHYT.TextChanged += new EventHandler(txtMaDtuong_BHYT_TextChanged);

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
            txtNoiDKKCBBD.KeyDown += txtNoiDKKCBBD_KeyDown;
            txtNoiDKKCBBD.TextChanged += new EventHandler(txtNoiDKKCBBD_TextChanged);

            txtNoiDongtrusoKCBBD.TextChanged += new EventHandler(txtNoiDongtrusoKCBBD_TextChanged);
            txtNoiDongtrusoKCBBD.KeyDown += txtNoiDongtrusoKCBBD_KeyDown;

            chkTraiTuyen.CheckedChanged += chkTraiTuyen_CheckedChanged;
            cmdSave.Click += new EventHandler(cmdSave_Click);
            lnkThem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkThem_LinkClicked);
            cmdClose.Click += new EventHandler(cmdClose_Click);
           
            cboDoituongKCB.SelectedIndexChanged += new EventHandler(cboDoituongKCB_SelectedIndexChanged);
        }

        void cboDoituongKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!AllowTextChanged) return;
            _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
            objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
            ChangeObjectRegion();
        }
        void ChangeObjectRegion()
        {
            if (objDoituongKCB == null)
            {
                _IdDoituongKcb = -1;
                _MaDoituongKcb = "";
                _TenDoituongKcb = "";
                PtramBhytCu = 0m;
                txtPtramBHYT.Text = "0";
                errorProvider1.SetError(cboDoituongKCB, "Mời bạn chọn đối tượng khám chữa bệnh cần chuyển đổi");
                return;
            }
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            txtPtramBHYT.Text = objDoituongKCB.PhantramTraituyen.ToString();
            if (objDoituongKCB.IdLoaidoituongKcb == 0)//ĐỐi tượng BHYT
            {
                pnlBHYT.Enabled = true;
              //  lblPtram.Text = "Phần trăm BHYT";
                TinhPtramBHYT();
                txtMaDtuong_BHYT.SelectAll();
                txtMaDtuong_BHYT.Focus();
            }
            else//Đối tượng khác BHYT
            {
                pnlBHYT.Enabled = false;
             //   lblPtram.Text = "P.trăm giảm giá";
                XoathongtinBHYT(PropertyLib._KCBProperties.XoaBHYT);
               // txtTEN_BN.Focus();
            }
        }
        DmucDoituongkcb objDoituongKCB = null;
        void cmdClose_Click(object sender, EventArgs e)
        {
            m_blnSuccess = false;
            this.Close();
        }
        void frm_chuyendoituongkcb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {

                _MaDoituongKcb = "DV";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                return;
            }
            if (e.Control && e.KeyCode == Keys.B)
            {
                _MaDoituongKcb = "BHYT";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
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
                m_blnhasLoaded = false;
                AllowTextChanged = false;
             //   Utility.SetColor(lblDiachiBHYT, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BHYT", "1", false) == "1");
                chkTraiTuyen.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEPTIEPDON_TRAITUYEN", "1", false) == "1";
                txtMaDTsinhsong.Init();
                AddAutoCompleteDiaChi();
                Getdata();
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(), DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "", false);
               
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
        private void Getdata()
        {
            AllowTextChanged = false;
            PtramBhytCu = 0m;
            KcbDanhsachBenhnhan objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
            if (objBenhnhan != null)
            {
                txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan);
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                txtGioiTinh.Text = Utility.sDbnull(objBenhnhan.GioiTinh);
              //  txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai);
                txtDiachi_bhyt._Text = Utility.sDbnull(objBenhnhan.DiachiBhyt);
                //txtDiachi._Text = Utility.sDbnull(objBenhnhan.DiaChi);
                if (!string.IsNullOrEmpty(Utility.sDbnull(objBenhnhan.NgaySinh)))
                {

                }
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);
              //  txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh));
               // txtNgheNghiep._Text = Utility.sDbnull(objBenhnhan.NgheNghiep);
              //  cboPatientSex.SelectedValue = Utility.sDbnull(objBenhnhan.GioiTinh);
            //    if (Utility.Int32Dbnull(objBenhnhan.DanToc) > 0)
             //       txtDantoc._Text = objBenhnhan.DanToc;
              //  txtCMT.Text = Utility.sDbnull(objBenhnhan.Cmt);

            }

            if (objLuotkham != null)
            {
                _IdDoituongKcb = objLuotkham.IdDoituongKcb;
                chkTraiTuyen.Checked = Utility.Int32Dbnull(objLuotkham.DungTuyen, 0) == 0;

                _IdDoituongKcb = objLuotkham.IdDoituongKcb;
                _TenDoituongKcb = _IdDoituongKcb == 1 ? "Dịch vụ" : "Bảo hiểm y tế";
                _MaDoituongKcb = _IdDoituongKcb == 1 ? "DV" : "BHYT";
                chkChuyenVien.Checked = Utility.Int32Dbnull(objLuotkham.TthaiChuyenden, 0) == 1;
                txtOldName.Text = _TenDoituongKcb;
                if (!string.IsNullOrEmpty(objLuotkham.MatheBhyt))//Thông tin BHYT
                {
               //     txtTrieuChungBD._Text = Utility.sDbnull(objLuotkham.TrieuChung);
                    if (!string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.NgaybatdauBhyt)))
                        dtInsFromDate.Value = Convert.ToDateTime(objLuotkham.NgaybatdauBhyt);
                    if (!string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.NgayketthucBhyt)))
                        dtInsToDate.Value = Convert.ToDateTime(objLuotkham.NgayketthucBhyt);
                    txtPtramBHYT.Text = Utility.sDbnull(objLuotkham.PtramBhyt, "0");
                    txtptramDauthe.Text = Utility.sDbnull(objLuotkham.PtramBhytGoc, "0");
                    PtramBhytCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                    //HS7010340000005
                    txtMaDtuong_BHYT.Text = Utility.sDbnull(objLuotkham.MaDoituongBhyt);
                    txtMaQuyenloi_BHYT.Text = Utility.sDbnull(objLuotkham.MaQuyenloi);
                    txtNoiDongtrusoKCBBD.Text = Utility.sDbnull(objLuotkham.NoiDongtrusoKcbbd);
                    txtOthu4.Text = Utility.sDbnull(objLuotkham.MatheBhyt).Substring(5, 2);
                    txtOthu5.Text = Utility.sDbnull(objLuotkham.MatheBhyt).Substring(7, 3);
                    txtOthu6.Text = Utility.sDbnull(objLuotkham.MatheBhyt).Substring(10, 5);

                    txtNoiphattheBHYT.Text = Utility.sDbnull(objLuotkham.MaNoicapBhyt);
                    txtNoiDKKCBBD.Text = Utility.sDbnull(objLuotkham.MaKcbbd);
                    GetNoiDangKy();
                    pnlBHYT.Enabled = true;
                }
                else
                {
                    XoathongtinBHYT(true);
                }
            }
            else
            {
            }
        }
        void XoathongtinBHYT(bool forcetodel)
        {
            if (forcetodel)
            {
                _IdDoituongKcb = 1;
                _MaDoituongKcb = "DV";
                _TenDoituongKcb = "Dịch vụ";
                dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
                dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
                txtPtramBHYT.Text = "";
                lblNoiCapThe.Text = "";
                lblClinicName.Text = "";
                txtMaDtuong_BHYT.Clear();
                txtMaQuyenloi_BHYT.Clear();
                txtNoiDongtrusoKCBBD.Clear();
                txtOthu4.Clear();
                txtOthu5.Clear();
                txtOthu6.Clear();
                chkTraiTuyen.Checked = false;
                chkChuyenVien.Checked = false;
                txtNoiphattheBHYT.Clear();
                txtDiachi_bhyt.Clear();
                txtNoiDKKCBBD.Clear();
                pnlBHYT.Enabled = false;
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
                if (pnlBHYT.Enabled)
                {
                    if (!isValidData()) return;
                    if (!isValidMatheBHYT()) return;
                }
                 objLuotkhamMoi = GetNewInfor();
                string msg = "";
                if (objLuotkham.MaDoituongKcb != objLuotkhamMoi.MaDoituongKcb)
                {
                    DmucDoituongkcb objOld = new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                    DmucDoituongkcb objNew = new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(objLuotkhamMoi.MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                    msg = string.Format("Bạn có muốn chuyển đối tượng từ {0} sang {1} hay không?", objOld.TenDoituongKcb, objNew.TenDoituongKcb);
                }
                else
                {
                    msg = "Bạn có chắc chắn muốn cập nhật lại thông tin không?";
                }
                if (Utility.AcceptQuestion(msg, "Xác nhận", true))
                {
                    ActionResult actionResult = ChuyenDoituongKCB.Chuyendoituong(objLuotkhamMoi, objLuotkham,PtramBhytCu);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Chuyển đổi đối tượng thành công. Nhấn nút OK để kết thúc", "Thành công");
                            m_blnSuccess = true;
                            Utility.EnableButton(cmdSave, true);
                            Close();
                            break;
                        case ActionResult.Cancel:
                            Utility.ShowMsg("Bệnh nhân này đã được thanh toán một số dịch vụ.Bạn cần liên hệ bộ phận thanh toán hủy thanh toán trước khi thực hiện tính năng Chuyển đối tượng này","Thông báo");
                            break;
                        case ActionResult.Exception:
                            Utility.ShowMsg("Có lỗi trong quá trình chuyển thông tin đối tượng.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.EnableButton(cmdSave, true);
                Utility.ShowMsg("Lỗi khi nhấn nút Chuyển đối tượng:\n" + ex.Message);
                throw;
            }
            finally
            {
                Utility.EnableButton(cmdSave, true);
            }
        }
        private KcbLuotkham GetNewInfor()
        {

            KcbLuotkham _newitem = new KcbLuotkham();
            _newitem.IdBenhnhan = objLuotkham.IdBenhnhan;
            _newitem.MaLuotkham = objLuotkham.MaLuotkham;
            _newitem.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            _newitem.IdDoituongKcb = _IdDoituongKcb;
           
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_IdDoituongKcb);
            if (objectType != null)
            {
                _newitem.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                _newitem.IdLoaidoituongKcb = objectType.IdLoaidoituongKcb;
            }
            if (objectType.IdLoaidoituongKcb==0)
            {
                Laymathe_BHYT();
                _newitem.MaKcbbd = Utility.sDbnull(txtNoiDKKCBBD.Text, "");
                _newitem.NoiDongtrusoKcbbd = Utility.sDbnull(txtNoiDongtrusoKCBBD.Text, "");
                _newitem.MaNoicapBhyt = Utility.sDbnull(txtNoiphattheBHYT.Text);
                _newitem.LuongCoban = globalVariables.LUONGCOBAN;
                _newitem.MatheBhyt = Laymathe_BHYT();
                _newitem.MaDoituongBhyt = Utility.sDbnull(txtMaDtuong_BHYT.Text);
                _newitem.MaQuyenloi = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, null);
                _newitem.DungTuyen = !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1))); // Utility.ByteDbnull(chkTraiTuyen.Checked ? 0 : 1);
                _newitem.TthaiChuyenden = (byte)(chkChuyenVien.Checked ? 1 : 0);
                _newitem.NgayketthucBhyt = dtInsToDate.Value.Date;
                _newitem.NgaybatdauBhyt = dtInsFromDate.Value.Date;
                _newitem.NoicapBhyt = Utility.GetValue(lblNoiCapThe.Text, false);
                _newitem.DiachiBhyt = Utility.sDbnull(txtDiachi_bhyt.Text);

                _newitem.GiayBhyt = Utility.Bool2byte(chkGiayBHYT.Checked);
                _newitem.MadtuongSinhsong = Utility.sDbnull(txtMaDTsinhsong.myCode);
               
            }
            else
            {
                _newitem.GiayBhyt = 0;
                _newitem.MadtuongSinhsong = "";
                _newitem.MaKcbbd = "";
                _newitem.NoiDongtrusoKcbbd = "";
                _newitem.MaNoicapBhyt = "";
                _newitem.LuongCoban = globalVariables.LUONGCOBAN;
                _newitem.MatheBhyt = "";
                _newitem.MaDoituongBhyt = "";
                _newitem.MaQuyenloi = -1;
                _newitem.DungTuyen = 0;
                _newitem.TthaiChuyenden = 0;
                _newitem.NgayketthucBhyt = null;
                _newitem.NgaybatdauBhyt = null;
                _newitem.NoicapBhyt = "";
                _newitem.DiachiBhyt = "";
               
            }
            _newitem.PtramBhytGoc = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
            _newitem.PtramBhyt = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);
            _newitem.TrangthaiNoitru = objLuotkham.TrangthaiNoitru;
            _newitem.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
            return _newitem;
        }
        private bool isValidData()
        {
            if (string.IsNullOrEmpty(txtMaDtuong_BHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập đối tượng đầu thẻ cho bảo hiểm không bỏ trống", "Thông báo",
                                MessageBoxIcon.Information);
                txtMaDtuong_BHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaQuyenloi_BHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập mã quyền lợi cho bảo hiểm không bỏ trống", "Thông báo");
                txtMaQuyenloi_BHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiDongtrusoKCBBD.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 3  cho bảo hiểm không bỏ trống", "Thông báo");
                txtNoiDongtrusoKCBBD.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu4.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 4  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu4.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu5.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 5  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu5.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu6.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 6  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu6.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiphattheBHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi cấp thẻ  cho bảo hiểm không bỏ trống", "Thông báo",
                                MessageBoxIcon.Information);
                txtNoiphattheBHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiDKKCBBD.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký khám chữa bệnh ban đầu cho bảo hiểm không bỏ trống",
                                "Thông báo");
                txtNoiDKKCBBD.Focus();
                return false;
            }
            if (dtInsToDate.Value < dtInsFromDate.Value)
            {
                Utility.ShowMsg("Ngày hết hạn thẻ BHYT phải lớn hơn hoặc bằng ngày đăng ký thẻ BHYT", "Thông báo");
                dtInsToDate.Focus();
                return false;
            }
            if (dtInsToDate.Value < globalVariables.SysDate)
            {
                Utility.ShowMsg("Ngày hết hạn thẻ BHYT phải lớn hơn hoặc bằng ngày hiện tại", "Thông báo");
                dtInsToDate.Focus();
                return false;
            }
            return true;
        }
        private void chkTraiTuyen_CheckedChanged(object sender, EventArgs e)
        {
            TinhPtramBHYT();
        }
        private void _LostFocus(object sender, EventArgs e)
        {
            if (isAutoFinding) return;
            string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                             txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
            string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
            if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBHYT(MA_BHYT, noiDangky);
        }
        private void txtNoiDKKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                //string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiDongtrusoKCBBD.Text.Trim() + txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                //if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtNoiDKKCBBD.Text.Length <= 0)
                {
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.Select(txtNoiDongtrusoKCBBD.Text.Length, 0);
                }
            }
        }
        private void txtNoiDongtrusoKCBBD_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtNoiDongtrusoKCBBD.Text.Length <= 0)
            {
                txtOthu6.Focus();
                if (txtOthu6.Text.Length > 0) txtOthu6.Select(txtOthu6.Text.Length, 0);
                return;
            }
            if (txtNoiDongtrusoKCBBD.Text.Length < 2) return;
            if (!isValidMatheBHYT()) return;
            LoadClinicCode();
            txtNoiDKKCBBD.Focus();
            txtNoiDKKCBBD.SelectAll();
        }

        private void txtOthu4_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu4.Text.Length <= 0)
            {
                txtNoiphattheBHYT.Focus();
                if (txtNoiphattheBHYT.Text.Length > 0) txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
                return;
            }
            if (txtOthu4.Text.Length < 2) return;
            if (!isValidMatheBHYT()) return;
            txtOthu5.Focus();
            txtOthu5.SelectAll();
        }

        private void txtOthu5_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu5.Text.Length <= 0)
            {
                txtOthu4.Focus();
                if (txtOthu4.Text.Length > 0) txtOthu4.Select(txtOthu4.Text.Length, 0);
                return;
            }
            if (txtOthu5.Text.Length < 3) return;
            if (!isValidMatheBHYT()) return;
            txtOthu6.Focus();
            txtOthu6.SelectAll();
        }

        private void txtOthu6_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu6.Text.Length <= 0)
            {
                txtOthu5.Focus();
                if (txtOthu5.Text.Length > 0) txtOthu5.Select(txtOthu5.Text.Length, 0);
                return;
            }
            if (txtOthu6.Text.Length < 5) return;
            if (!isValidMatheBHYT()) return;
            txtNoiDongtrusoKCBBD.Focus();
            txtNoiDongtrusoKCBBD.SelectAll();
        }



        private void GetNoiDangKy()
        {
            SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
                .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtNoiphattheBHYT.Text);
            var objDiachinh = sqlQuery.ExecuteSingle<DmucDiachinh>();
            if (objDiachinh != null)
            {
                lblNoiCapThe.Visible = true;
                Utility.SetMsg(lblNoiCapThe, Utility.sDbnull(objDiachinh.TenDiachinh), true);
            }
            else
            {
                lblNoiCapThe.Visible = false;
            }
        }

        private void txtNoiDKKCBBD_TextChanged(object sender, EventArgs e)
        {
            if (_MaDoituongKcb == "DV") return;
            if (txtNoiDKKCBBD.Text.Length < 3)
            {
                Utility.SetMsg(lblClinicName, "", false);
                return;
            }
            LoadClinicCode();
            if (lnkThem.Visible) lnkThem.Focus();
            else
                dtInsFromDate.Focus();
        }

        private void LaySoTheBHYT()
        {
            string SoBHYT = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text,
                                          txtNoiDongtrusoKCBBD.Text, txtNoiDKKCBBD.Text);
            GetSoBHYT = SoBHYT;
        }
        private string mathe_bhyt_full()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text,
                                          txtNoiDongtrusoKCBBD.Text, txtNoiDKKCBBD.Text);

        }

        private string Laymathe_BHYT()
        {
            string SoBHYT = string.Format("{0}{1}{2}{3}{4}{5}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text);
            return SoBHYT;
        }
        private bool isValidMatheBHYT()
        {

            if (!string.IsNullOrEmpty(txtMaDtuong_BHYT.Text))
            {
                SqlQuery sqlQuery = new Select().From(DmucDoituongbhyt.Schema)
                    .Where(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(txtMaDtuong_BHYT.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã đối tượng BHYT không tồn tại trong hệ thống. Mời bạn kiểm tra lại",
                        "Thông báo", MessageBoxIcon.Information);
                    txtMaDtuong_BHYT.Focus();
                    txtMaDtuong_BHYT.SelectAll();
                    return false;
                }
            }
            if (Utility.DoTrim(txtMaDtuong_BHYT.Text) != "" && Utility.DoTrim(txtMaQuyenloi_BHYT.Text) != "")
            {
                QheDautheQloiBhyt objQheDautheQloiBhyt = new Select().From(QheDautheQloiBhyt.Schema).Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(Utility.DoTrim(txtMaDtuong_BHYT.Text))
                    .And(QheDautheQloiBhyt.Columns.MaQloi).IsEqualTo(Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0)).ExecuteSingle<QheDautheQloiBhyt>();
                if (objQheDautheQloiBhyt == null)
                {
                    Utility.ShowMsg(string.Format("Đầu thẻ BHYT: {0} chưa được cấu hình gắn với mã quyền lợi: {1}. Mời bạn kiểm tra lại", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                    txtMaQuyenloi_BHYT.Focus();
                    txtMaQuyenloi_BHYT.SelectAll();
                    return false;
                }
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_KIEMTRAMATHE", "1", true) == "1")
            {
                if (!string.IsNullOrEmpty(txtMaQuyenloi_BHYT.Text))
                {
                    if (Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0) < 0 || Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0) > 9)
                    {
                        Utility.ShowMsg("Số thứ tự 2 của mã bảo hiểm nằm trong khoảng từ 1->9", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtMaQuyenloi_BHYT.Focus();
                        txtMaQuyenloi_BHYT.SelectAll();
                        return false;
                    }

                    QheDautheQloiBhytCollection lstqhe = new Select().From(QheDautheQloiBhyt.Schema).Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(txtMaDtuong_BHYT.Text).ExecuteAsCollection<QheDautheQloiBhytCollection>();
                    if (lstqhe.Count > 0)
                    {
                        var q = from p in lstqhe
                                where p.MaQloi == Utility.ByteDbnull(txtMaQuyenloi_BHYT.Text, -1)
                                select objDoituongKCB;

                        if (q.Count() <= 0)
                        {

                            Utility.ShowMsg(
                                string.Format(
                                    "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                    txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                            txtMaQuyenloi_BHYT.Focus();
                            txtMaQuyenloi_BHYT.SelectAll();
                            return false;
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(
                            string.Format(
                                "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                        txtMaQuyenloi_BHYT.Focus();
                        txtMaQuyenloi_BHYT.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtNoiphattheBHYT.Text))
                {
                    //if (txtNoiphattheBHYT.Text.Length <= 0)
                    //{
                    //    Utility.ShowMsg("Mã nơi phát thẻ BHYT phải nằm trong khoảng từ 00->99", "Thông báo",
                    //                    MessageBoxIcon.Information);
                    //    txtNoiphattheBHYT.Focus();
                    //    txtNoiphattheBHYT.SelectAll();
                    //    return false;
                    //}
                    //if (Utility.Int32Dbnull(txtNoiphattheBHYT.Text, 0) <= 0)
                    //{
                    //    Utility.ShowMsg("Mã nơi phát thẻ BHYT không được phép có chữ cái và phải nằm trong khoảng từ 00->99", "Thông báo",
                    //                    MessageBoxIcon.Information);
                    //    txtNoiphattheBHYT.Focus();
                    //    txtNoiphattheBHYT.SelectAll();
                    //    return false;
                    //}
                }
                if (!string.IsNullOrEmpty(txtOthu4.Text))
                {
                    //if (txtOthu4.Text.Length <= 1)
                    //{
                    //    Utility.ShowMsg("Hai kí tự ô số 4 của mã bảo hiểm nằm trong khoảng từ 01->99", "Thông báo",
                    //                    MessageBoxIcon.Information);
                    //    txtOthu4.Focus();
                    //    txtOthu4.SelectAll();
                    //    return false;
                    //}

                    //if (Utility.Int32Dbnull(txtOthu4.Text, 0) <= 0)
                    //{
                    //    Utility.ShowMsg("Hai kí tự ô số 4 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 01->99", "Thông báo",
                    //                    MessageBoxIcon.Information);
                    //    txtOthu4.Focus();
                    //    txtOthu4.SelectAll();
                    //    return false;
                    //}
                }
                if (!string.IsNullOrEmpty(txtOthu5.Text))
                {
                    if (txtOthu5.Text.Length <= 2)
                    {
                        Utility.ShowMsg("3 kí tự ô số 5 của mã bảo hiểm nằm trong khoảng từ 001->999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu5.Focus();
                        txtOthu5.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu5.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("3 kí tự ô số 5 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 001->999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu5.Focus();
                        txtOthu5.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu6.Text))
                {
                    if (txtOthu6.Text.Length <= 4)
                    {
                        Utility.ShowMsg("5 kí tự ô số 6 của mã bảo hiểm nằm trong khoảng từ 00001->99999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu6.Focus();
                        txtOthu6.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu6.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("5 kí tự ô số 6 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 00001->99999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu6.Focus();
                        txtOthu6.SelectAll();
                        return false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtNoiDongtrusoKCBBD.Text))
            {
                if (txtNoiDongtrusoKCBBD.Text.Length <= 1)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD phải nhập từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }

                if (Utility.Int32Dbnull(txtNoiDongtrusoKCBBD.Text, 0) <= 0)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD không được phép có chữ cái và phải nằm trong khoảng từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }

                SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
                    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtNoiDongtrusoKCBBD.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã thành phố nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(txtNoiDKKCBBD.Text))
            {
                string ma_diachinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_DANGKY_CACHXACDINH_NOIDKKCBBD", true) == "0" ? txtNoiphattheBHYT.Text : txtNoiDongtrusoKCBBD.Text;
                SqlQuery sqlQuery = new Select().From(DmucNoiKCBBD.Schema)
                    .Where(DmucNoiKCBBD.Columns.MaKcbbd).IsEqualTo(txtNoiDKKCBBD.Text)
                    .And(DmucNoiKCBBD.Columns.MaDiachinh).IsEqualTo(ma_diachinh);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã  nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtNoiDKKCBBD.Focus();
                    txtNoiDKKCBBD.SelectAll();
                    return false;
                }
            }
            return true;
        }
        private void FindPatientIDbyBHYT(string insuranceNum, string noiDangky)
        {
            try
            {

                DataTable temdt = SPs.KcbTimkiembenhnhantheomathebhyt(insuranceNum+ noiDangky).GetDataSet().Tables[0];
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
        private void TinhPtramBHYT()
        {
            try
            {
                LaySoTheBHYT();
                if (!string.IsNullOrEmpty(Laymathe_BHYT()) && Laymathe_BHYT().Length >= 15)
                {
                    if ((!string.IsNullOrEmpty(GetSoBHYT)) && (!string.IsNullOrEmpty(txtNoiDKKCBBD.Text)))
                    {
                        var objLuotkham = new KcbLuotkham();
                        objLuotkham.MaNoicapBhyt = Utility.sDbnull(txtNoiphattheBHYT.Text);
                        objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(txtNoiDongtrusoKCBBD.Text);
                        objLuotkham.MatheBhyt = Laymathe_BHYT();
                        objLuotkham.MaDoituongBhyt = txtMaDtuong_BHYT.Text;
                        objLuotkham.DungTuyen = !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1))); // (byte?)(chkTraiTuyen.Checked ? 0 : 1);
                        objLuotkham.MaKcbbd = Utility.sDbnull(txtNoiDKKCBBD.Text);
                        objLuotkham.IdDoituongKcb = _IdDoituongKcb;
                        objLuotkham.MaQuyenloi = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text);
                        objLuotkham.MadtuongSinhsong = txtMaDTsinhsong.myCode;
                        objLuotkham.GiayBhyt = Utility.Bool2byte(chkGiayBHYT.Checked);
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

        /// <summary>
        /// hàm thực hiện việc load thông tin của nơi khám chữa bệnh ban đầu
        /// </summary>
        private void LoadClinicCode()
        {
            try
            {
                string ma_diachinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_DANGKY_CACHXACDINH_NOIDKKCBBD", true) == "0" ? txtNoiphattheBHYT.Text : txtNoiDongtrusoKCBBD.Text;
                //Lấy mã Cơ sở KCBBD
                string v_CliniCode = ma_diachinh + txtNoiDKKCBBD.Text.Trim();
                string strClinicName = "";
                DataTable dataTable = _KCB_DANGKY.GetClinicCode(v_CliniCode);
                if (dataTable.Rows.Count > 0)
                {
                    strClinicName = dataTable.Rows[0][DmucNoiKCBBD.Columns.TenKcbbd].ToString();
                    Utility.SetMsg(lblClinicName, strClinicName, !string.IsNullOrEmpty(txtNoiDKKCBBD.Text));
                }
                else
                {
                    Utility.SetMsg(lblClinicName, strClinicName, false);
                }
                lblClinicName.Visible = dataTable.Rows.Count > 0;
                lnkThem.Visible = dataTable.Rows.Count <= 0;
                //txtNamePresent.Text = strClinicName;
                //Check đúng tuyến cần lấy mã nơi cấp BHYT+mã kcbbd thay vì mã cơ sở kcbbd

                if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                    //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                    chkTraiTuyen.Checked =
                        !(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                txtNoiDKKCBBD.Text.Trim()) ||
                          (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                  txtNoiDKKCBBD.Text.Trim()) &&
                           chkChuyenVien.Checked));

                TinhPtramBHYT();
            }
            catch (Exception exception)
            {
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
            if (!m_blnhasLoaded) return ;
            if (_MaDoituongKcb == "DV") return;
            if (txtNoiphattheBHYT.Text.Length < 2)
            {
                Utility.SetMsg(lblNoiCapThe, "", false);
                return;
            }
            else
                GetNoiDangKy();
            if (!isValidMatheBHYT()) return;
            txtOthu4.Focus();
            txtOthu4.SelectAll();

        }
        private void txtMaQuyenloi_BHYT_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtMaQuyenloi_BHYT.Text.Length <= 0)
            {
                txtMaDtuong_BHYT.Focus();
                if (txtMaDtuong_BHYT.Text.Length > 0) txtMaDtuong_BHYT.Select(txtMaDtuong_BHYT.Text.Length, 0);
            }
            if (txtMaQuyenloi_BHYT.Text.Length < 1) return;
            if (!isValidMatheBHYT()) return;
            TinhPtramBHYT();
            txtNoiphattheBHYT.Focus();
            txtNoiphattheBHYT.SelectAll();
        }
        private void txtMaDtuong_BHYT_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnhasLoaded) return;
            if (_MaDoituongKcb == "DV") return;
            if (txtMaDtuong_BHYT.Text.Length < 2) return;
            if (!isValidMatheBHYT()) return;
            TinhPtramBHYT();
            txtMaQuyenloi_BHYT.Focus();
        }
        private void txtMaDtuong_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiDongtrusoKCBBD.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBHYT(MA_BHYT, noiDangky);
            }
        }

        private void txtMaQuyenloi_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBHYT(MA_BHYT, noiDangky);
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtMaQuyenloi_BHYT.Text.Length <= 0)
                {
                    txtMaDtuong_BHYT.Focus();
                    txtMaDtuong_BHYT.Select(txtMaDtuong_BHYT.Text.Length, 0);
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
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                //Không cần tìm
                //string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                //                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                //if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                //return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtNoiDongtrusoKCBBD.Text.Length <= 0)
                {
                    txtOthu6.Focus();
                    txtOthu6.Select(txtOthu6.Text.Length, 0);
                }
            }
        }

        private void txtOthu4_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBHYT(MA_BHYT, noiDangky);
                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtOthu4.Text.Length <= 0)
                {
                    txtNoiphattheBHYT.Focus();
                    txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
                }
            }
        }

        private void txtOthu5_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBHYT(MA_BHYT, noiDangky);
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtOthu5.Text.Length <= 0)
                {
                    txtOthu4.Focus();
                    txtOthu4.Select(txtOthu4.Text.Length, 0);
                }
            }
        }

        private void txtOthu6_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                string noiDangky = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                if (MA_BHYT.Length == 15 && noiDangky.Length == 5) FindPatientIDbyBHYT(MA_BHYT, noiDangky);
                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtOthu6.Text.Length <= 0)
                {
                    txtOthu5.Focus();
                    txtOthu5.Select(txtOthu5.Text.Length, 0);
                }
            }
        }

        private void lnkThem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newItem = new frm_ThemnoiKCBBD();
            newItem.m_dtDataThanhPho = globalVariables.gv_dtDmucDiachinh;
            newItem.SetInfor(txtNoiDKKCBBD.Text, txtNoiphattheBHYT.Text);
            if (newItem.ShowDialog() == DialogResult.OK)
            {
                txtNoiDKKCBBD.Text = "";
                txtNoiphattheBHYT.Text = "";
                txtNoiDKKCBBD.Text = newItem.txtMa.Text.Trim();
                txtNoiphattheBHYT.Text = newItem.txtMaThanhPho.Text.Trim();
                dtInsFromDate.Focus();
            }
        }
    }
}
