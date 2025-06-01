using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Noitru
{
    public partial class FrmTrichBienBanHoiChan : Form
    {
        public FrmTrichBienBanHoiChan()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitData();
        }

        public string malankham = "";
        public int idbenhnhan = -1;
        public action MEnAction = action.Insert;
        private void InitData()
        {
            txtChanDoan._OnSaveAs += txtChanDoan__OnSaveAs;
            txtChanDoan._OnShowData += txtChanDoan__OnShowData;

            txtchutoa._OnSaveAs += txtchutoa__OnSaveAs;
            txtchutoa._OnShowData += txtchutoa__OnShowData;

            txtThuKy._OnSaveAs += txtThuKy__OnSaveAs;
            txtThuKy._OnShowData += txtThuKy__OnShowData;

            txtThanhvien._OnSaveAs += txtThanhvien__OnSaveAs;
            txtThanhvien._OnShowData += txtThanhvien__OnShowData;

            txtTomtatquatrinh._OnSaveAs += txtTomtatquatrinh__OnSaveAs;
            txtTomtatquatrinh._OnShowData += txtTomtatquatrinh__OnShowData;

            txtketluan._OnSaveAs += txtketluan__OnSaveAs;
            txtketluan._OnShowData += txtketluan__OnShowData;

            txthuongdieutri._OnSaveAs += txthuongdieutri__OnSaveAs;
            txthuongdieutri._OnShowData += txthuongdieutri__OnShowData;

        }
        void txtChanDoan__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChanDoan.myCode;
                txtChanDoan.Init();
                txtChanDoan.SetCode(oldCode);
                txtChanDoan.Focus();
            }
        }
        void txtchutoa__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtchutoa.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtchutoa.myCode;
                txtchutoa.Init();
                txtchutoa.SetCode(oldCode);
                txtchutoa.Focus();
            }
        }
        void txtThuKy__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtThuKy.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtThuKy.myCode;
                txtThuKy.Init();
                txtThuKy.SetCode(oldCode);
                txtThuKy.Focus();
            }
        }
        void txtThanhvien__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtThanhvien.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtThanhvien.myCode;
                txtThanhvien.Init();
                txtThanhvien.SetCode(oldCode);
                txtThanhvien.Focus();
            }
        }
        void txtTomtatquatrinh__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTomtatquatrinh.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTomtatquatrinh.myCode;
                txtTomtatquatrinh.Init();
                txtTomtatquatrinh.SetCode(oldCode);
                txtTomtatquatrinh.Focus();
            }
        }
        void txtketluan__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtketluan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtketluan.myCode;
                txtketluan.Init();
                txtketluan.SetCode(oldCode);
                txtketluan.Focus();
            }
        }
        void txthuongdieutri__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txthuongdieutri.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txthuongdieutri.myCode;
                txthuongdieutri.Init();
                txthuongdieutri.SetCode(oldCode);
                txthuongdieutri.Focus();
            }
        }
        void txtChanDoan__OnSaveAs()
        {
            if (Utility.DoTrim(txtChanDoan.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtChanDoan.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChanDoan.myCode;
                txtChanDoan.Init();
                txtChanDoan.SetCode(oldCode);
                txtChanDoan.Focus();
            }
        }
        void txtchutoa__OnSaveAs()
        {
            if (Utility.DoTrim(txtchutoa.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtchutoa.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtchutoa.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtchutoa.myCode;
                txtchutoa.Init();
                txtchutoa.SetCode(oldCode);
                txtchutoa.Focus();
            }
        }
        void txtThuKy__OnSaveAs()
        {
            if (Utility.DoTrim(txtThuKy.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtThuKy.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtThuKy.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtThuKy.myCode;
                txtThuKy.Init();
                txtThuKy.SetCode(oldCode);
                txtThuKy.Focus();
            }
        }
        void txtThanhvien__OnSaveAs()
        {
            if (Utility.DoTrim(txtThanhvien.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtThanhvien.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtThanhvien.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtThanhvien.myCode;
                txtThanhvien.Init();
                txtThanhvien.SetCode(oldCode);
                txtThanhvien.Focus();
            }
        }
        void txtTomtatquatrinh__OnSaveAs()
        {
            if (Utility.DoTrim(txtTomtatquatrinh.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTomtatquatrinh.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtTomtatquatrinh.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTomtatquatrinh.myCode;
                txtTomtatquatrinh.Init();
                txtTomtatquatrinh.SetCode(oldCode);
                txtTomtatquatrinh.Focus();
            }
        }
        void txtketluan__OnSaveAs()
        {
            if (Utility.DoTrim(txtketluan.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtketluan.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtketluan.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtketluan.myCode;
                txtketluan.Init();
                txtketluan.SetCode(oldCode);
                txtketluan.Focus();
            }
        }
        void txthuongdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txthuongdieutri.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txthuongdieutri.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txthuongdieutri.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txthuongdieutri.myCode;
                txthuongdieutri.Init();
                txthuongdieutri.SetCode(oldCode);
                txthuongdieutri.Focus();
            }
        }
        private void FrmTrichBienBanHoiChan_Load(object sender, EventArgs e)
        {
            DataTable dt = SPs.NoitruLaythongtinbenhnhan(malankham, idbenhnhan).GetDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtPatient_ID.Text = idbenhnhan.ToString(CultureInfo.InvariantCulture);
                txtPatient_Code.Text = malankham.ToString(CultureInfo.InvariantCulture);
                txtPatient_Name.Text = Utility.sDbnull(dt.Rows[0]["ten_benhnhan"].ToString());
                txtTuoi.Text = Utility.sDbnull(dt.Rows[0]["tuoi"].ToString());
                txtGioitinh.Text = Utility.sDbnull(dt.Rows[0]["gioi_tinh"].ToString());
                txtSoBHYT.Text = Utility.sDbnull(dt.Rows[0]["mathe_bhyt"].ToString());
                txtBHTT.Text = Utility.sDbnull(dt.Rows[0]["ptram_bhyt"].ToString());
                txtDiaChi.Text = Utility.sDbnull(dt.Rows[0]["dia_chi"].ToString());
                txtChanDoan.Text = Utility.sDbnull(dt.Rows[0]["chan_doan"].ToString());
                dtpNgayhethanBHYT.Value = Convert.ToDateTime(dt.Rows[0]["ngayketthuc_bhyt"].ToString());
                txtBuong_lapphieu.Text = Utility.sDbnull(dt.Rows[0]["ten_buong"].ToString());
                txtGiuong_lapphieu.Text = Utility.sDbnull(dt.Rows[0]["ten_giuong"].ToString());
                txtidbuong.Text = Utility.sDbnull(dt.Rows[0]["id_buong"].ToString());
                txtidgiuong.Text = Utility.sDbnull(dt.Rows[0]["id_giuong"].ToString());
                var objbienbanhoichan =
                    new Select().From(NoitruBienbanHoichan.Schema)
                        .Where(NoitruBienbanHoichan.Columns.MaLuotkham)
                        .IsEqualTo(txtPatient_Code.Text)
                        .ExecuteSingle<NoitruBienbanHoichan>();
                if (objbienbanhoichan != null)
                {
                    txtThanhvien.Text = Utility.sDbnull(objbienbanhoichan.ThanhVien);
                    txtchutoa.Text = Utility.sDbnull(objbienbanhoichan.ChuToa);
                    txtThuKy.Text = Utility.sDbnull(objbienbanhoichan.ThuKy);
                    dtInput_Date.Value = Convert.ToDateTime(objbienbanhoichan.NgayLap);
                    txtTomtatquatrinh.Text = Utility.sDbnull(objbienbanhoichan.TomtatQtrinh);
                    txtketluan.Text = Utility.sDbnull(objbienbanhoichan.Ketluan);
                    txthuongdieutri.Text = Utility.sDbnull(objbienbanhoichan.HuongDtri);
                    txtid.Text = Utility.sDbnull(objbienbanhoichan.Id);
                    MEnAction = action.Update;
                }
                else
                {
                    MEnAction = action.Insert;
                    ClearControl();
                }
            }
        }

        private void ClearControl()
        {
            txtThanhvien.Text = "";
            txtchutoa.Text = "";
            txtThuKy.Text = "";
            dtInput_Date.Value = globalVariables.SysDate;
            txtTomtatquatrinh.Text = "";
            txtketluan.Text = ""; // Utility.sDbnull(objbienbanhoichan.Ketluan);
            txthuongdieutri.Text = ""; // Utility.sDbnull(objbienbanhoichan.HuongDtri);
        }

        private NoitruBienbanHoichan CreateBienBanHoiChan()
        {
           
            if (MEnAction == action.Insert)
            {
                _objBienbanHoichan = new NoitruBienbanHoichan();
                _objBienbanHoichan.NgayTao = globalVariables.SysDate;
                _objBienbanHoichan.NguoiTao = globalVariables.UserName;
                _objBienbanHoichan.IpMaytao = globalVariables.gv_strIPAddress;
                _objBienbanHoichan.IsNew = true;
            }
            else
            {
                _objBienbanHoichan = NoitruBienbanHoichan.FetchByID(Utility.Int64Dbnull(txtid.Text));
                _objBienbanHoichan.NgaySua = globalVariables.SysDate;
                _objBienbanHoichan.NguoiSua = globalVariables.UserName;
                _objBienbanHoichan.IpMaysua = globalVariables.gv_strIPAddress;
                _objBienbanHoichan.IsNew = false;
            }
            _objBienbanHoichan.IdBenhnhan = Utility.Int64Dbnull(txtPatient_ID.Text);
            _objBienbanHoichan.MaLuotkham = Utility.sDbnull(txtPatient_Code.Text);
            _objBienbanHoichan.IdBuong = Utility.Int32Dbnull(txtidbuong.Text);
            _objBienbanHoichan.IdGiuong = Utility.Int32Dbnull(txtidgiuong.Text);
            _objBienbanHoichan.ChuToa = Utility.sDbnull(txtchutoa.Text);
            _objBienbanHoichan.ThanhVien = Utility.sDbnull(txtThanhvien.Text);
            _objBienbanHoichan.ThuKy = Utility.sDbnull(txtThuKy.Text);
            _objBienbanHoichan.TomtatQtrinh = Utility.sDbnull(txtTomtatquatrinh.Text);
            _objBienbanHoichan.Ketluan = Utility.sDbnull(txtketluan.Text); 
            _objBienbanHoichan.HuongDtri = Utility.sDbnull(txthuongdieutri.Text);
            _objBienbanHoichan.NgayLap = dtInput_Date.Value;
            _objBienbanHoichan.ChanDoan = Utility.sDbnull(txtChanDoan.Text);
            return _objBienbanHoichan;
        }

        private NoitruBienbanHoichan _objBienbanHoichan = null;
        private void cmdLuuHoichan_Click(object sender, EventArgs e)
        {
             _objBienbanHoichan = CreateBienBanHoiChan();
            if (MEnAction == action.Insert)
            {
                _objBienbanHoichan.IsNew = true;
                _objBienbanHoichan.Save();
                txtid.Text = Utility.sDbnull(_objBienbanHoichan.Id);
                MEnAction = action.Update;
                lblMsg.Text = @"Thêm mới thành công biên bản hội chẩn";
            }
            else
            {
                _objBienbanHoichan.IsNew = false;
                _objBienbanHoichan.MarkOld();
                _objBienbanHoichan.Save();
                txtid.Text = Utility.sDbnull(_objBienbanHoichan.Id);
                MEnAction = action.Update;
                lblMsg.Text = @"Cập nhật thành công biên bản hội chẩn";
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void cmdHuyBienban_Click(object sender, EventArgs e)
        {
            try
            {
                new Delete().From(NoitruBienbanHoichan.Schema)
                    .Where(NoitruBienbanHoichan.Columns.Id)
                    .IsEqualTo(txtid.Text).Execute();
                lblMsg.Text = @"Xóa thành công biên bản hội chẩn";
                ClearControl();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdInPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                string malankham = txtPatient_Code.Text;
               
                DataTable dt = SPs.NoitruInBienbanHoichan(malankham, -1).GetDataSet().Tables[0];
                noitru_baocao.InTricbienbanhoichan(dt, "TRÍCH BIÊN BẢN HỘI CHẨN", dtInput_Date.Value);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
    }
}
