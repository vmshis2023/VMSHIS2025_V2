using System;
using System.Data;
using System.Windows.Forms;
using VMS.HIS.DAL;
using SubSonic;
using VNS.Libs;
using Janus.Windows.GridEX;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_dichvucls : Form
    {
        #region "THUOC TINH"
        public GridEX grdService;
        private DataTable m_dtLoaiDichvuCLS=new DataTable();
        public DataRow drServiceInfo;
        public  DataTable dsService=new DataTable();
        public action em_Action = action.Insert;
        bool m_blnLoaded = false;
        #endregion

        public frm_themmoi_dichvucls()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            THU_VIEN_CHUNG.EnableBHYT(this);
            this.KeyPreview = true;
            cmdThoat.Click+=new EventHandler(cmdThoat_Click);
            cmdGhi.Click+=new EventHandler(cmdGhi_Click);
            txtServiceName.LostFocus+=new EventHandler(txtServiceName_LostFocus);
            cboDepartment.SelectedIndexChanged += new EventHandler(cboDepartment_SelectedIndexChanged);
            txtDonvitinh._OnShowData += txtDonvitinh__OnShowData;
            txtQuychuan._OnShowData += txtQuychuan__OnShowData;
            txtNhominphoiBHYT._OnShowData += txtNhominphoiBHYT__OnShowData;
            chkKiemnghiem.CheckedChanged += chkKiemnghiem_CheckedChanged;
            txtServiceCode.LostFocus += txtServiceCode_LostFocus;
        }

        void txtNhominphoiBHYT__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhominphoiBHYT.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhominphoiBHYT.myCode;
                txtNhominphoiBHYT.Init();
                txtNhominphoiBHYT.SetCode(oldCode);
                txtNhominphoiBHYT.Focus();
            } 
        }

        void txtServiceCode_LostFocus(object sender, EventArgs e)
        {
            if (Utility.DoTrim(txtMaBHYT.Text) == "")
                txtMaBHYT.Text = txtServiceCode.Text;
        }

        void chkKiemnghiem_CheckedChanged(object sender, EventArgs e)
        {
            txtThetichtoithieu.Enabled = txtQuychuan.Enabled = txtSongaytraKQ.Enabled = txtDonvitinh.Enabled =chkCososanh.Enabled=chkTinhthetichtheochitieu.Enabled= chkKiemnghiem.Checked;
            if (chkKiemnghiem.Checked) txtThetichtoithieu.Focus();
            else
            {
                txtThetichtoithieu.Clear();
                txtQuychuan.SetCode("-1");
                txtSongaytraKQ.Clear();
                txtDonvitinh.SetCode("-1");
                chkCososanh.Checked = false;
                chkTinhthetichtheochitieu.Checked = false;
            }
        }

        void txtQuychuan__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtQuychuan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtQuychuan.myCode;
                txtQuychuan.Init();
                txtQuychuan.SetCode(oldCode);
                txtQuychuan.Focus();
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
                txtDonvitinh.Focus();
            } 
        }

        void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            DataTable dtPhong = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1), 1);
            DataBinding.BindDataCombobox(cboPhongthuchien, dtPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa phòng", true);
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtServicePrice_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(txtServicePrice);
        }

        private DataTable m_dtNhomDichVu,m_dtKhoaChucNang=new DataTable();
        void InitData()
        {


            DataTable dtnhominphoi = new Select().From(DmucChung.Schema)
                .Where(DmucChung.Columns.Loai).IsEqualTo(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_STT_INPHOI", "STT_INPHOIBHYT", true))
                .And(DmucChung.Columns.VietTat).IsEqualTo("2")
                .ExecuteDataSet().Tables[0];
            txtNhominphoiBHYT.Init(dtnhominphoi, new List<string>() { DmucChung.Columns.Ma, DmucChung.Columns.Ma, DmucChung.Columns.Ten });
            txtQuychuan.Init();
            txtDonvitinh.Init();
            m_dtLoaiDichvuCLS = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAIDICHVUCLS", true);
            DataTable m_dtLoaiDichvuCLS_new = m_dtLoaiDichvuCLS.Clone();
            if (globalVariables.gv_dtQuyenNhanvien_Dmuc.Select(QheNhanvienDanhmuc.Columns.Loai + "= 0").Length <= 0)
                m_dtLoaiDichvuCLS_new = m_dtLoaiDichvuCLS.Copy();
            else
            {
                foreach (DataRow dr in m_dtLoaiDichvuCLS.Rows)
                {
                    if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucChung.Columns.Ma]), "0"))
                    {
                        m_dtLoaiDichvuCLS_new.ImportRow(dr);
                    }
                }
            }
            DataBinding.BindDataCombox(cboServiceType, m_dtLoaiDichvuCLS_new, DmucChung.Columns.Ma, DmucChung.Columns.Ten,"Chọn",false);
            m_dtNhomDichVu = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBAOCAOCLS",true);
            DataBinding.BindDataCombox(cbonhombaocao, m_dtNhomDichVu, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
           DataTable  dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
           DataBinding.BindDataCombox(cboNhomin, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            m_dtKhoaChucNang = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL",1);
            DataBinding.BindDataCombobox(cboDepartment, m_dtKhoaChucNang, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Chọn---", true);
            
            
        }
        void ClearControl()
        {
            foreach (Control control in grpControl.Controls)
            {
                if (control is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    ((Janus.Windows.GridEX.EditControls.EditBox)(control)).Clear();
                }
                em_Action = action.Insert;
                txtServiceCode.Focus();
            }
        }
        private void frm_themmoi_dichvucls_Load(object sender, EventArgs e)
        {
            try
            {
                InitData();
                m_blnLoaded = true;
                if (em_Action == action.Update) GetData();
                if (em_Action == action.Insert)
                {
                    txtServiceOrder.Value = Utility.DecimaltoDbnull(query.GetMax(DmucDichvucl.Columns.SttHthi), 0);
                    txtID.Text = (Utility.DecimaltoDbnull(query.GetMax(DmucDichvucl.Columns.SttHthi), 0) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                m_blnLoaded = true;
            }
        }
        void BindServiceCode()
        {
            
        }
        private void frm_themmoi_dichvucls_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void frm_themmoi_dichvucls_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdThoat.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if(e.Control&&e.KeyCode==Keys.S)cmdGhi.PerformClick();
            if (e.KeyCode == Keys.F5) ClearControl();
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {

            if (!IsValidData())return;
            switch (em_Action)
            {
                case action.Insert:
                    Insert();
                    break;
                case action.Update:
                    Update();
                    break;
                    

            }
        }
     
        private bool  IsValidData()
        {
            SqlQuery q;
            Utility.SetMsg(lblMsg, "", true);
            if (string.IsNullOrEmpty(txtServiceCode.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập Mã dịch vụ", true);
                txtServiceCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaBHYT.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập Mã dịch vụ theo QĐ 29", true);
                txtMaBHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtServiceName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên dịch vụ", true);
                txtServiceName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenBHYT.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên dịch vụ theo QĐ 29", true);
                txtTenBHYT.Focus();
                return false;
            }
            if (cboServiceType.SelectedIndex <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn loại dịch vụ",true);
                cboServiceType.Focus();
                return false;
            }
            if (cboNhomin.SelectedIndex <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn nhóm in phiếu chỉ định CLS", true);
                cboNhomin.Focus();
                return false;
            }
            if (cbonhombaocao.SelectedIndex <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn nhóm báo cáo doanh thu", true);
                cbonhombaocao.Focus();
                return false;
            }
            if (txtNhominphoiBHYT.myCode=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn nhóm in biên lai, bảng kê chi phí KCB", true);
                txtNhominphoiBHYT.Focus();
                return false;
            }
            if(em_Action==action.Insert)
            {
                q = new Select().From(DmucDichvucl.Schema)
                    .Where(DmucDichvucl.Columns.MaDichvu).IsEqualTo(Utility.DoTrim(txtServiceCode.Text));
                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có mã như vậy. Mời bạn kiểm tra lại", true);
                    txtServiceCode.Focus();
                    return false;
                }
                q = new Select().From(DmucDichvucl.Schema)
                    .Where(DmucDichvucl.Columns.MaBhyt).IsEqualTo(Utility.DoTrim(txtMaBHYT.Text));
                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Mã QĐ 29 như vậy. Mời bạn kiểm tra lại", true);
                    txtMaBHYT.Focus();
                    return false;
                }

                q = new Select().From(DmucDichvucl.Schema)
                    .Where(DmucDichvucl.Columns.TenDichvu).IsEqualTo(Utility.DoTrim(txtServiceName.Text));
                if(q.GetRecordCount()>0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có tên như vậy. Mời bạn kiểm tra lại", true);
                    txtServiceName.Focus();
                    return false;
                }
                q = new Select().From(DmucDichvucl.Schema)
                    .Where(DmucDichvucl.Columns.TenBhyt).IsEqualTo(Utility.DoTrim(txtTenBHYT.Text));
                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Tên QĐ 29 như vậy. Mời bạn kiểm tra lại", true);
                    txtTenBHYT.Focus();
                    return false;
                }
            }
            if (em_Action == action.Update)
            {
                q = new Select().From(DmucDichvucl.Schema)
                   .Where(DmucDichvucl.Columns.MaDichvu).IsEqualTo(Utility.DoTrim(txtServiceCode.Text)).And(DmucDichvucl.Columns.IdDichvu).
                   IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Mã như vậy. Mời bạn kiểm tra lại", true);
                    txtServiceCode.Focus();
                    return false;
                }

                q = new Select().From(DmucDichvucl.Schema)
                  .Where(DmucDichvucl.Columns.MaBhyt).IsEqualTo(Utility.DoTrim(txtMaBHYT.Text)).And(DmucDichvucl.Columns.IdDichvu).
                  IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Mã QĐ 29 như vậy. Mời bạn kiểm tra lại", true);
                    txtMaBHYT.Focus();
                    return false;
                }

                q = new Select().From(DmucDichvucl.Schema)
                    .Where(DmucDichvucl.Columns.TenDichvu).IsEqualTo(Utility.DoTrim(txtServiceName.Text)).And(DmucDichvucl.Columns.IdDichvu).
                    IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có tên như vậy. Mời bạn kiểm tra lại", true);
                    txtServiceName.Focus();
                    return false;
                }
                q = new Select().From(DmucDichvucl.Schema)
                   .Where(DmucDichvucl.Columns.TenBhyt).IsEqualTo(Utility.DoTrim(txtTenBHYT.Text)).And(DmucDichvucl.Columns.IdDichvu).
                   IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có Tên QĐ 29 như vậy. Mời bạn kiểm tra lại", true);
                    txtTenBHYT.Focus();
                    return false;
                }
            }
            if (Utility.sDbnull(cboNhomin.SelectedValue, "-1") == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn nhóm in phiếu", true);
                cboNhomin.Focus();
                return false;
            }
            if (Utility.sDbnull(cbonhombaocao.SelectedValue, "-1") == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn nhóm báo cáo", true);
                cbonhombaocao.Focus();
                return false;
            }
            return true;
        }
        void GetData()
        {
            DmucDichvucl objDichVu = DmucDichvucl.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if (objDichVu != null)
            {
                txtID.Text = objDichVu.IdDichvu.ToString();
                txtServiceCode.Text = Utility.sDbnull(objDichVu.MaDichvu, "");
                txtServiceName.Text = Utility.sDbnull(objDichVu.TenDichvu, "");
                txtMaBHYT.Text = Utility.sDbnull(objDichVu.MaBhyt, "");
                txtTenBHYT.Text = Utility.sDbnull(objDichVu.TenBhyt, "");
                nmrDongia.Value = Utility.DecimaltoDbnull(objDichVu.DonGia, 0);
                if (Utility.Int32Dbnull(objDichVu.IdKhoaThuchien, -1) > 0)
                {
                    cboDepartment.SelectedIndex = Utility.GetSelectedIndex(cboDepartment, objDichVu.IdKhoaThuchien.Value.ToString());
                }
                if (Utility.Int32Dbnull(objDichVu.IdPhongThuchien, -1) > 0)
                {
                    cboPhongthuchien.SelectedIndex = Utility.GetSelectedIndex(cboPhongthuchien, objDichVu.IdPhongThuchien.Value.ToString());
                }
                txtDonvitinh.SetCode(objDichVu.MaDonvichitieu);
                txtQuychuan.SetCode(objDichVu.MaQuychuanSosanh);
                txtNhominphoiBHYT.SetCode(objDichVu.NhomInphoiBHYT);
                txtSongaytraKQ.Text =Utility.sDbnull( objDichVu.SongayTraketqua,0);
                txtThetichtoithieu.Text = Utility.sDbnull(objDichVu.ThetichToithieu, 0);
                chkTinhthetichtheochitieu.Checked = Utility.Byte2Bool(objDichVu.TinhthetichTheochitieu);
                chkCososanh.Checked = Utility.Byte2Bool(objDichVu.CoSosanh);
                chkKiemnghiem.Checked = Utility.Byte2Bool(objDichVu.LaDvuKiemnghiem);
                //chkKiemnghiem.Checked = txtDonvitinh.myCode != "-1" || txtQuychuan.myCode != "-1" || Utility.sDbnull( txtSongaytraKQ.Text) != ""
                //    || Utility.sDbnull(txtThetichtoithieu.Text) != ""
                //    || chkTinhthetichtheochitieu.Checked || chkCososanh.Checked;
                txtDesc.Text = Utility.sDbnull(objDichVu.MotaThem, "");
                txttenphieutrakqCDHA.Text = Utility.sDbnull(objDichVu.CdhaTenphieu, "");
                txtchidan.Text = objDichVu.ChiDan;
                txtServiceOrder.Value = Utility.DecimaltoDbnull(objDichVu.SttHthi, "1");
                cbonhombaocao.SelectedIndex = Utility.GetSelectedIndex(cbonhombaocao, objDichVu.NhomBaocao);
                cboNhomin.SelectedIndex = Utility.GetSelectedIndex(cboNhomin, objDichVu.NhomInCls);
                chkTrangthai.Checked = Utility.Int16Dbnull(objDichVu.TrangThai, 0) == 1;
                chkHaveDetail.Checked = objDichVu.HienthiChitiet == 1 ? true : false;
                chkHighTech.Checked = objDichVu.DichvuKtc == 1 ? true : false;
                chkIntachphieu.Checked = Utility.Byte2Bool(objDichVu.InTachphieu);
                cboServiceType.SelectedIndex = Utility.GetSelectedIndex(cboServiceType, Utility.sDbnull(objDichVu.IdLoaidichvu, "-1"));
                chkKiemnghiem_CheckedChanged(chkKiemnghiem, new EventArgs());
            }
        }

        private Query query = DmucDichvucl.CreateQuery();
        void Insert()
        {
            try
            {
                DmucDichvucl objDichVu = new DmucDichvucl();
                objDichVu.MotaThem = txtDesc.Text;
                objDichVu.MaDonvichitieu = txtDonvitinh.myCode;
                objDichVu.NhomInphoiBHYT = txtNhominphoiBHYT.myCode;
                objDichVu.MaQuychuanSosanh = txtQuychuan.myCode;
                objDichVu.SongayTraketqua = (byte) Utility.DecimaltoDbnull(txtSongaytraKQ.Text, 0);
                objDichVu.ThetichToithieu = (int) Utility.DecimaltoDbnull(txtThetichtoithieu.Text, 0);
                objDichVu.TinhthetichTheochitieu = Utility.Bool2byte(chkTinhthetichtheochitieu.Checked);
                objDichVu.CoSosanh=Utility.Bool2byte(chkCososanh.Checked );
                objDichVu.LaDvuKiemnghiem = Utility.Bool2byte(chkKiemnghiem.Checked);
                objDichVu.IdKhoaThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVu.IdPhongThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVu.MaDichvu = Utility.sDbnull(txtServiceCode.Text, "");
                objDichVu.TenDichvu = Utility.sDbnull(txtServiceName.Text, "");
                objDichVu.MaBhyt = Utility.sDbnull(txtMaBHYT.Text, "");
                objDichVu.TenBhyt = Utility.sDbnull(txtTenBHYT.Text, "");
                objDichVu.IdLoaidichvu = Utility.sDbnull(cboServiceType.SelectedValue, "-1");
                objDichVu.SttHthi = Utility.Int16Dbnull(txtServiceOrder.Value, 0);
                objDichVu.DichvuKtc = chkHighTech.Checked ? Convert.ToInt16(1) : Convert.ToInt16(0);
                objDichVu.HienthiChitiet = chkHaveDetail.Checked ? Convert.ToByte(1) : Convert.ToByte(0);
                objDichVu.NgayTao = globalVariables.SysDate;
                objDichVu.NguoiTao = globalVariables.UserName;
                objDichVu.ChiDan = Utility.DoTrim(txtchidan.Text);
                objDichVu.TrangThai = (byte)(chkTrangthai.Checked ? 1 : 0);
                objDichVu.InTachphieu = Utility.Bool2byte(chkIntachphieu.Checked);
                objDichVu.NhomInCls = Utility.sDbnull(cboNhomin.SelectedValue, "ALL");// getNhominCLS(cboNhomin.SelectedIndex);
                objDichVu.NhomBaocao = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                objDichVu.CdhaTenphieu =  Utility.sDbnull(txttenphieutrakqCDHA.Text);
                objDichVu.DonGia = nmrDongia.Value;
                objDichVu.IsNew = true;
                objDichVu.Save();
                ProcessData(objDichVu.IdDichvu);
            }
            catch
            {
            }
        }

        private void ProcessData(int id)
        {

            DataRow dr = dsService.NewRow();
            dr[VDmucDichvucl.Columns.IdDichvu] = id;
            dr[VDmucDichvucl.Columns.TenLoaidichvu] = Utility.sDbnull(cboServiceType.Text, "");
            dr[DmucDichvucl.Columns.NguoiTao] = globalVariables.UserName;
            dr[DmucDichvucl.Columns.NgayTao] = globalVariables.SysDate;
            dr[DmucDichvucl.Columns.IdDichvu] = Utility.Int32Dbnull(query.GetMax(DmucDichvucl.Columns.IdDichvu), -1);
            dr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(txtServiceName.Text, "");
            dr[DmucDichvucl.Columns.MaDichvu] = Utility.sDbnull(txtServiceCode.Text, "");
            dr[DmucDichvucl.Columns.TenBhyt] = Utility.sDbnull(txtTenBHYT.Text, "");
            dr[DmucDichvucl.Columns.MaBhyt] = Utility.sDbnull(txtMaBHYT.Text, "");
            dr[DmucDichvucl.Columns.HienthiChitiet] = chkHaveDetail.Checked ? Convert.ToByte(1) : Convert.ToByte(0);
            dr[DmucDichvucl.Columns.DichvuKtc] = chkHighTech.Checked ? Convert.ToInt16(1) : Convert.ToInt16(0);
            dr[DmucDichvucl.Columns.IdLoaidichvu] = Utility.sDbnull(cboServiceType.SelectedValue, "-1");
            dr[DmucDichvucl.Columns.SttHthi] = Utility.Int32Dbnull(txtServiceOrder.Text, 1);
            dr[DmucDichvucl.Columns.IdKhoaThuchien] = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
            dr[DmucDichvucl.Columns.IdPhongThuchien] = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
            if (cboDepartment.SelectedIndex > 0)
                dr[VDmucDichvucl.Columns.TenKhoaThuchien] = Utility.sDbnull(cboDepartment.Text);
            else
                dr[VDmucDichvucl.Columns.TenKhoaThuchien] = "";
            if (cboPhongthuchien.SelectedIndex > 0)
                dr[VDmucDichvucl.Columns.TenPhongThuchien] = Utility.sDbnull(cboPhongthuchien.Text);
            else
                dr[VDmucDichvucl.Columns.TenPhongThuchien] = "";
            dr[DmucDichvucl.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
            dr[DmucDichvucl.Columns.InTachphieu] = chkIntachphieu.Checked ? 1 : 0;
            dr[DmucDichvucl.Columns.NhomBaocao] = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
            dr[VDmucDichvucl.Columns.TenNhombaocaoDichvu] = Utility.sDbnull(cbonhombaocao.Text, "");
            dr[VDmucDichvucl.Columns.TenNhominphieucls] = Utility.sDbnull(cbonhombaocao.Text, "");
            dr[DmucDichvucl.Columns.NhomInCls] = Utility.sDbnull(cboNhomin.SelectedValue, "ALL");// getNhominCLS(cboNhomin.SelectedIndex);
            dr[DmucDichvucl.Columns.DonGia] = nmrDongia.Value;
            dr[DmucDichvucl.Columns.NhomInphoiBHYT] = txtNhominphoiBHYT.myCode;
            dr[DmucDichvucl.Columns.CdhaTenphieu]  = Utility.sDbnull(txttenphieutrakqCDHA.Text);
            dr[DmucDichvucl.Columns.MaDonvichitieu] = txtDonvitinh.myCode;
            dr[DmucDichvucl.Columns.MaQuychuanSosanh] = txtQuychuan.myCode;
            dr[DmucDichvucl.Columns.SongayTraketqua] = (byte)Utility.DecimaltoDbnull(txtSongaytraKQ.Text, 0);
            dr[DmucDichvucl.Columns.ThetichToithieu] = (int)Utility.DecimaltoDbnull(txtThetichtoithieu.Text, 0);
            dr[DmucDichvucl.Columns.TinhthetichTheochitieu] = Utility.Bool2byte(chkTinhthetichtheochitieu.Checked);
            dr[DmucDichvucl.Columns.CoSosanh] = Utility.Bool2byte(chkCososanh.Checked);
            dr[DmucDichvucl.Columns.LaDvuKiemnghiem] = Utility.Bool2byte(chkKiemnghiem.Checked);
            dsService.Rows.Add(dr);
            dsService.AcceptChanges();
            Utility.GotoNewRowJanus(grdService, DmucDichvucl.Columns.IdDichvu, id.ToString());
            this.Close();
        }
        /// <summary>
        //Phiếu xét nghiệm
        //Phiếu X Quang
        //Phiếu siêu âm
        //Phiếu điện tim
        //Phiếu nội soi
        //Phiếu điện não đồ
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string getNhominCLS(int index)
        {

            switch(index)
            {
                case 0:
                    return "XN";
                case 1:
                    return "XQ";
                case 2:
                    return "SA";
                case 3:
                    return "DIEN_TIM";
                case 4:
                    return "NS";
                case 5:
                    return "DIEN_NAODO";
                default:
                    return "XN";
            }
            return "XN";
                
        }
        int getselectedIndex(string strNhomin)
        {
            //Phiếu xét nghiệm
            //Phiếu X Quang
            //Phiếu siêu âm
            //Phiếu điện tim
            //Phiếu nội soi
            //Phiếu điện não đồ
            switch (strNhomin)
            {
                case "XN":
                    return 0;
                case "XQ":
                    return 1;
                case "SA":
                    return 2;
                case "DIEN_TIM":
                    return 3;
                case "NS":
                    return 4;
                case "DIEN_NAODO":
                    return 5;
                default:
                    return 0;
            }
            return 0;

        }
        void Update()
        {
            new Update(DmucDichvucl.Schema)
                .Set(DmucDichvucl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                .Set(DmucDichvucl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                .Set(DmucDichvucl.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(nmrDongia.Value,0))
                .Set(DmucDichvucl.Columns.TenDichvu).EqualTo(Utility.sDbnull(txtServiceName.Text, ""))
                .Set(DmucDichvucl.Columns.MaDichvu).EqualTo(Utility.sDbnull(txtServiceCode.Text, ""))
                .Set(DmucDichvucl.Columns.TenBhyt).EqualTo(Utility.sDbnull(txtTenBHYT.Text, ""))
                .Set(DmucDichvucl.Columns.MaBhyt).EqualTo(Utility.sDbnull(txtMaBHYT.Text, ""))
                .Set(DmucDichvucl.Columns.MotaThem).EqualTo(Utility.sDbnull(txtDesc.Text, ""))
                .Set(DmucDichvucl.Columns.IdLoaidichvu).EqualTo(Utility.sDbnull(cboServiceType.SelectedValue, "-1"))
                .Set(DmucDichvucl.Columns.TrangThai).EqualTo(chkTrangthai.Checked?1:0)
                 .Set(DmucDichvucl.Columns.InTachphieu).EqualTo(chkIntachphieu.Checked?1:0)
                .Set(DmucDichvucl.Columns.HienthiChitiet).EqualTo(chkHaveDetail.Checked ? 1 : 0)
                .Set(DmucDichvucl.Columns.SttHthi).EqualTo(Utility.Int16Dbnull(txtServiceOrder.Text, 0))
                .Set(DmucDichvucl.Columns.IdKhoaThuchien).EqualTo(Utility.Int16Dbnull(cboDepartment.SelectedValue,-1))
                .Set(DmucDichvucl.Columns.IdPhongThuchien).EqualTo(Utility.Int16Dbnull(cboPhongthuchien.SelectedValue,-1))
                .Set(DmucDichvucl.Columns.DichvuKtc).EqualTo(chkHighTech.Checked ? 1 : 0)
                .Set(DmucDichvucl.Columns.ChiDan).EqualTo(Utility.DoTrim(txtchidan.Text))
                .Set(DmucDichvucl.Columns.NhomBaocao).EqualTo(Utility.sDbnull(cbonhombaocao.SelectedValue,"-1"))
                .Set(DmucDichvucl.Columns.MaDonvichitieu).EqualTo(txtDonvitinh.myCode)
                .Set(DmucDichvucl.Columns.MaQuychuanSosanh).EqualTo(txtQuychuan.myCode)
                .Set(DmucDichvucl.Columns.NhomInphoiBHYT).EqualTo(txtNhominphoiBHYT.myCode)
                .Set(DmucDichvucl.Columns.SongayTraketqua).EqualTo((byte)Utility.DecimaltoDbnull(txtSongaytraKQ.Text, 0))
                .Set(DmucDichvucl.Columns.ThetichToithieu).EqualTo((int)Utility.DecimaltoDbnull(txtThetichtoithieu.Text, 0))
                .Set(DmucDichvucl.Columns.TinhthetichTheochitieu).EqualTo(Utility.Bool2byte( chkTinhthetichtheochitieu.Checked))
                .Set(DmucDichvucl.Columns.CoSosanh).EqualTo(Utility.Bool2byte( chkCososanh.Checked))
                .Set(DmucDichvucl.Columns.LaDvuKiemnghiem).EqualTo(Utility.Bool2byte(chkKiemnghiem.Checked))
                //.Set(DmucDichvucl.Columns.NhomInCls).EqualTo(getNhominCLS(cboNhomin.SelectedIndex))
                .Set(DmucDichvucl.Columns.NhomInCls).EqualTo(Utility.sDbnull(cboNhomin.SelectedValue,"ALL"))
                .Set(DmucDichvucl.Columns.CdhaTenphieu).EqualTo(Utility.sDbnull(txttenphieutrakqCDHA.Text))
                .Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(Utility.Int32Dbnull(txtID.Text,-1))
                .Execute();
            
            ProcessData1();
        }

        private void ProcessData1()
        {
            DataRow[] arrDr=dsService.Select(DmucDichvucl.Columns.IdDichvu+"="+txtID.Text);
            if(arrDr.Length>0)
            {
                if (arrDr[0][DmucDichvucl.Columns.IdDichvu].ToString().Equals(txtID.Text))
                {
                    arrDr[0][DmucDichvucl.Columns.NguoiSua] = globalVariables.UserName;
                    arrDr[0][DmucDichvucl.Columns.NgaySua] = globalVariables.SysDate;
                    arrDr[0][DmucDichvucl.Columns.InTachphieu] = chkIntachphieu.Checked ? 1 : 0;
                    arrDr[0][DmucDichvucl.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
                    arrDr[0][DmucDichvucl.Columns.CdhaTenphieu] = Utility.sDbnull(txttenphieutrakqCDHA.Text);
                    arrDr[0][DmucDichvucl.Columns.NhomBaocao] = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                    arrDr[0][VDmucDichvucl.Columns.TenNhombaocaoDichvu] = Utility.sDbnull(cbonhombaocao.Text, "");
                    arrDr[0][VDmucDichvucl.Columns.TenNhominphieucls] = Utility.sDbnull(cboNhomin.Text, "");
                    arrDr[0][DmucDichvucl.Columns.NhomInCls] = Utility.sDbnull(cboNhomin.SelectedValue, "ALL");// getNhominCLS(cboNhomin.SelectedIndex);
                    arrDr[0][DmucDichvucl.Columns.DonGia] = Utility.DecimaltoDbnull(nmrDongia.Value, 0);
                    arrDr[0][DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(txtServiceName.Text, "");
                    arrDr[0][DmucDichvucl.Columns.TenBhyt] = Utility.sDbnull(txtTenBHYT.Text, "");
                    arrDr[0][DmucDichvucl.Columns.MaDichvu] = Utility.sDbnull(txtServiceCode.Text, "");
                    arrDr[0][DmucDichvucl.Columns.MaBhyt] = Utility.sDbnull(txtMaBHYT.Text, "");
                    arrDr[0][DmucDichvucl.Columns.MotaThem] = Utility.sDbnull(txtDesc.Text, "");
                    arrDr[0][DmucDichvucl.Columns.IdLoaidichvu] = Utility.sDbnull(cboServiceType.SelectedValue, "-1");
                    arrDr[0][DmucDichvucl.Columns.HienthiChitiet] = chkHaveDetail.Checked ? 1 : 0;
                    arrDr[0][DmucDichvucl.Columns.SttHthi] = Utility.Int16Dbnull(txtServiceOrder.Text, 0);
                    arrDr[0][DmucDichvucl.Columns.DichvuKtc] = chkHighTech.Checked ? 1 : 0;
                    arrDr[0][VDmucDichvucl.Columns.TenLoaidichvu] = Utility.sDbnull(cboServiceType.Text, "");
                    arrDr[0][DmucDichvucl.Columns.IdKhoaThuchien] = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                    arrDr[0][DmucDichvucl.Columns.IdPhongThuchien] = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
                    arrDr[0][DmucDichvucl.Columns.ChiDan] = Utility.DoTrim(txtchidan.Text);
                    if (cboDepartment.SelectedIndex > 0)
                        arrDr[0][VDmucDichvucl.Columns.TenKhoaThuchien] = Utility.sDbnull(cboDepartment.Text);
                    else
                        arrDr[0][VDmucDichvucl.Columns.TenKhoaThuchien] = "";
                    if (cboDepartment.SelectedIndex > 0)
                        arrDr[0][VDmucDichvucl.Columns.TenPhongThuchien] = Utility.sDbnull(cboPhongthuchien.Text);
                    else
                        arrDr[0][VDmucDichvucl.Columns.TenPhongThuchien] = "";
                    arrDr[0][DmucDichvucl.Columns.NhomInphoiBHYT] = txtNhominphoiBHYT.myCode;
                    arrDr[0][DmucDichvucl.Columns.MaDonvichitieu] = txtDonvitinh.myCode;
                    arrDr[0][DmucDichvucl.Columns.MaQuychuanSosanh] = txtQuychuan.myCode;
                    arrDr[0][DmucDichvucl.Columns.SongayTraketqua] = (byte)Utility.DecimaltoDbnull(txtSongaytraKQ.Text, 0);
                    arrDr[0][DmucDichvucl.Columns.ThetichToithieu] = (int)Utility.DecimaltoDbnull(txtThetichtoithieu.Text, 0);
                    arrDr[0][DmucDichvucl.Columns.TinhthetichTheochitieu] = Utility.Bool2byte(chkTinhthetichtheochitieu.Checked);
                    arrDr[0][DmucDichvucl.Columns.LaDvuKiemnghiem] = Utility.Bool2byte(chkKiemnghiem.Checked);
                    arrDr[0][DmucDichvucl.Columns.CoSosanh] = Utility.Bool2byte(chkCososanh.Checked);
                }
            }
            dsService.AcceptChanges();
            this.Close();
        }
        private void txtServiceName_LostFocus(object sender, System.EventArgs e)
        {
            if (Utility.DoTrim(txtTenBHYT.Text) == "")
                txtTenBHYT.Text = txtServiceName.Text;
        }
        private void txtServicePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
           // Utility.OnlyDigit(e);
        }

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearControl();
        }

        private void cmdGhi_Click_1(object sender, EventArgs e)
        {

        }

      
    }
}
