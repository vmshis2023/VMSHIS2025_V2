using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.Libs;
using VMS.HIS.DAL;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.UCs;
using VNS.HIS.BusRule.Classes;
using SubSonic;

namespace VNS.HIS.UI.Forms.Dungchung.UCs
{
    public partial class ucThongtinnguoibenh_emr_basic : UserControl
    {
        public delegate void OnEnterMe();
        public event OnEnterMe _OnEnterMe;
        private bool AllowTextChanged;
        public bool AutoLoad = false;
        public KcbLuotkham objLuotkham = null;
        public byte noitrungoaitru = 100;
        public byte trangthai_noitru = 100;
        public bool isReadonly = false;
        public string huongdieutri = "ALL"; //ALL,DTRI_NOITRU,DTRI_NGOAITRU
        string lastCode = "";
        public ucThongtinnguoibenh_emr_basic()
        {
            InitializeComponent();
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;
            txtMaluotkham.Enabled = cmdSearch.Enabled = !isReadonly;
        }
        public void SetReadonly()
        {
            txtMaluotkham.ReadOnly = cmdSearch.Enabled = false;
        }
        public void Refresh(string patient_code)
        {
            try
            {
                txtMaluotkham.Text = patient_code;
                objLuotkham = null;
                AllowTextChanged = false;
                
                string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                DataTable dt_Patient = SPs.EmrLaythongtinnguoibenhMaluotkhamIdbenhnhan(-1, _patient_Code).GetDataSet().Tables[0];
                if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                {
                    lastCode = txtMaluotkham.Text;

                    txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                    objLuotkham =
                        new Select().From(KcbLuotkham.Schema)
                            .Where(KcbLuotkham.Columns.IdBenhnhan)
                            .IsEqualTo(txtIdBn.Text)
                            .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                            .ExecuteSingle<KcbLuotkham>();
                    txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                    txtBOD.Text = Utility.sDbnull(dt_Patient.Rows[0]["nam_sinh"], "");
                    txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["Tuoi"], "");
                    chkNam.Checked = Utility.sDbnull(dt_Patient.Rows[0]["id_gioitinh"], "0") == "0";
                    chkNu.Checked = Utility.sDbnull(dt_Patient.Rows[0]["id_gioitinh"], "0") == "1";
                    txtNghenghiep.Text= Utility.sDbnull(dt_Patient.Rows[0]["ten_nghenghiep"], "");
                    txtMaNgheNghiep.Text = Utility.sDbnull(dt_Patient.Rows[0]["nghe_nghiep"], "");
                    txtDantoc.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_dantoc"], "");
                    txtMaDantoc.Text = Utility.sDbnull(dt_Patient.Rows[0]["dan_toc"], "");
                    txtTenNgoaikieu.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_quocgia"], "");
                    txtMaNgoaikieu.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_quocgia"], "");
                    txtDiachi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                    txtXaphuong.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_xaphuong"], "");
                    txtMaXaPhuong.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_xaphuong"], "");
                    txtQuanhuyen.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_quanhuyen"], "");
                    txtMaQuanHuyen.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_quanhuyen"], "");
                    txtTinhTp.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_tinhtp"], "");
                    txtMaTinhTp.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_tinhtp"], "");
                    txtNoilamviec.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_coquan"], "");
                    txtBHYTTuNgay.Text= Utility.sDbnull(dt_Patient.Rows[0]["ngaybatdau_bhyt"], "");
                    txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0]["mathe_bhyt"], "");
                    lblNguoilienhe.Text = string.Format("11. Họ tên, địa chỉ người nhà khi cần báo tin: {0},{1}", Utility.sDbnull(dt_Patient.Rows[0]["nguoi_lienhe"], ""), Utility.sDbnull(dt_Patient.Rows[0]["diachi_lienhe"], "")); ;
                    txtDTLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["dienthoai_lienhe"], "-1");

                }
                if (_OnEnterMe != null) _OnEnterMe();

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
        public void Refresh()
        {
            try
            {
                globalVariables.AppLog.Trace("Start Refresh");
                objLuotkham = null;
                AllowTextChanged = false;
                string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                DataTable dt_Patient = SPs.EmrLaythongtinnguoibenhMaluotkhamIdbenhnhan(-1, _patient_Code).GetDataSet().Tables[0];
                if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                {
                    lastCode = txtMaluotkham.Text;

                    txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                    objLuotkham =
                        new Select().From(KcbLuotkham.Schema)
                            .Where(KcbLuotkham.Columns.IdBenhnhan)
                            .IsEqualTo(txtIdBn.Text)
                            .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                            .ExecuteSingle<KcbLuotkham>();
                    txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                    txtBOD.Text = Utility.sDbnull(dt_Patient.Rows[0]["nam_sinh"], "");
                    txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["Tuoi"], "");
                    chkNam.Checked = Utility.sDbnull(dt_Patient.Rows[0]["id_gioitinh"], "0") == "0";
                    chkNu.Checked = Utility.sDbnull(dt_Patient.Rows[0]["id_gioitinh"], "0") == "1";
                    txtNghenghiep.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_nghenghiep"], "");
                    txtMaNgheNghiep.Text = Utility.sDbnull(dt_Patient.Rows[0]["nghe_nghiep"], "");
                    txtDantoc.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_dantoc"], "");
                    txtMaDantoc.Text = Utility.sDbnull(dt_Patient.Rows[0]["dan_toc"], "");
                    txtTenNgoaikieu.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_quocgia"], "");
                    txtMaNgoaikieu.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_quocgia"], "");
                    txtDiachi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                    txtXaphuong.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_xaphuong"], "");
                    txtMaXaPhuong.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_xaphuong"], "");
                    txtQuanhuyen.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_quanhuyen"], "");
                    txtMaQuanHuyen.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_quanhuyen"], "");
                    txtTinhTp.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_tinhtp"], "");
                    txtMaTinhTp.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_tinhtp"], "");
                    txtNoilamviec.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_coquan"], "");
                    txtBHYTTuNgay.Text = Utility.sDbnull(dt_Patient.Rows[0]["ngaybatdau_bhyt"], "");
                    txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0]["mathe_bhyt"], "");
                    lblNguoilienhe.Text = string.Format("11. Họ tên, địa chỉ người nhà khi cần báo tin: {0},{1}", Utility.sDbnull(dt_Patient.Rows[0]["nguoi_lienhe"], ""), Utility.sDbnull(dt_Patient.Rows[0]["diachi_lienhe"], "")); ;
                    txtDTLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["dienthoai_lienhe"], "-1");

                }
                if (_OnEnterMe != null) _OnEnterMe();

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                globalVariables.AppLog.Trace("End Refresh");
                AllowTextChanged = true;
            }
        }
        public void Refresh(bool RaiseEvent)
        {
            try
            {
                objLuotkham = null;
                AllowTextChanged = false;
                string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                DataTable dt_Patient = SPs.EmrLaythongtinnguoibenhMaluotkhamIdbenhnhan(-1, _patient_Code).GetDataSet().Tables[0];
                if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                {
                    lastCode = txtMaluotkham.Text;

                    txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                    objLuotkham =
                        new Select().From(KcbLuotkham.Schema)
                            .Where(KcbLuotkham.Columns.IdBenhnhan)
                            .IsEqualTo(txtIdBn.Text)
                            .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                            .ExecuteSingle<KcbLuotkham>();
                    txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                    txtBOD.Text = Utility.sDbnull(dt_Patient.Rows[0]["nam_sinh"], "");
                    txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["Tuoi"], "");
                    chkNam.Checked = Utility.sDbnull(dt_Patient.Rows[0]["id_gioitinh"], "0") == "0";
                    chkNu.Checked = Utility.sDbnull(dt_Patient.Rows[0]["id_gioitinh"], "0") == "1";
                    txtNghenghiep.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_nghenghiep"], "");
                    txtMaNgheNghiep.Text = Utility.sDbnull(dt_Patient.Rows[0]["nghe_nghiep"], "");
                    txtDantoc.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_dantoc"], "");
                    txtMaDantoc.Text = Utility.sDbnull(dt_Patient.Rows[0]["dan_toc"], "");
                    txtTenNgoaikieu.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_quocgia"], "");
                    txtMaNgoaikieu.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_quocgia"], "");
                    txtDiachi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                    txtXaphuong.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_xaphuong"], "");
                    txtMaXaPhuong.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_xaphuong"], "");
                    txtQuanhuyen.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_quanhuyen"], "");
                    txtMaQuanHuyen.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_quanhuyen"], "");
                    txtTinhTp.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_tinhtp"], "");
                    txtMaTinhTp.Text = Utility.sDbnull(dt_Patient.Rows[0]["ma_tinhtp"], "");
                    txtNoilamviec.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_coquan"], "");
                    txtBHYTTuNgay.Text = Utility.sDbnull(dt_Patient.Rows[0]["ngaybatdau_bhyt"], "");
                    txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0]["mathe_bhyt"], "");
                    lblNguoilienhe.Text = string.Format("11. Họ tên, địa chỉ người nhà khi cần báo tin: {0},{1}", Utility.sDbnull(dt_Patient.Rows[0]["nguoi_lienhe"], ""), Utility.sDbnull(dt_Patient.Rows[0]["diachi_lienhe"], "")); ;
                    txtDTLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["dienthoai_lienhe"], "-1");
                }
                if (RaiseEvent && _OnEnterMe != null) _OnEnterMe();

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
        public void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Utility.DoTrim(txtMaluotkham.Text) != "")
                    Refresh();
                else
                    txtMaluotkham.Text = lastCode;
            }
        }
        public void ClearControls()
        {
            try
            {

                foreach (Control control in pnlTop.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }

            }
            catch (Exception)
            {
            }
        }
        private void cmdgetPatient_Click(object sender, EventArgs e)
        {

        }

        void cmdSearch_Click(object sender, EventArgs e)
        {
            if (noitrungoaitru == 1)
            {
                var frm = new frm_TimkiemBenhnhanNoitru("ALL", 1);
                frm.MaLuotkham = txtMaluotkham.Text;
                frm.huongdieutri = huongdieutri;
                frm.ShowDialog();
                if (!frm.has_Cancel)
                {
                    txtMaluotkham.Text = frm.MaLuotkham;
                    txtMaluotkham_KeyDown(txtMaluotkham, new KeyEventArgs(Keys.Enter));
                }
            }
            else
            {
                frm_DSACH_BN_TKIEM frm = new frm_DSACH_BN_TKIEM("ALL", noitrungoaitru);
                frm.trangthai_noitru = this.trangthai_noitru;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtMaluotkham.Text = frm.MaLuotkham;
                    txtMaluotkham_KeyDown(txtMaluotkham, new KeyEventArgs(Keys.Enter));
                }
            }
        }
    }
}
