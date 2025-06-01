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
    public partial class ucThongtinnguoibenh_v3 : UserControl
    {
        public delegate void OnEnterMe();
        public event OnEnterMe _OnEnterMe;
        private bool AllowTextChanged;
        public bool AutoLoad = false;
        public KcbLuotkham objLuotkham = null;
        public VKcbLuotkham objBenhnhan = null;
        public DmucKhoaphong _khoaphong = null;
        public byte noitrungoaitru = 100;
        public byte trangthai_noitru = 100;
        public bool isReadonly = false;
        public string huongdieutri = "ALL"; //ALL,DTRI_NOITRU,DTRI_NGOAITRU
        string lastCode = "";
        public ucThongtinnguoibenh_v3()
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
                objBenhnhan = null;
                AllowTextChanged = false;
                if (!AutoLoad)
                {
                    var dtPatient = new DataTable();

                    objLuotkham = null;
                    string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                    ClearControls();
                    txtMaluotkham.Text = _patient_Code;
                    dtPatient = new KCB_THAMKHAM().TimkiemBenhnhan(txtMaluotkham.Text, -1, 1, 0);

                    DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
                    if (arrPatients.GetLength(0) <= 0)
                    {
                        if (dtPatient.Rows.Count > 1)
                        {
                            var frm = new frm_DSACH_BN_TKIEM("ALL", noitrungoaitru);
                            frm.trangthai_noitru = trangthai_noitru;
                            frm.MaLuotkham = txtMaluotkham.Text;
                            frm.dtPatient = dtPatient;
                            frm.ShowDialog();
                            if (!frm.has_Cancel)
                            {
                                txtMaluotkham.Text = frm.MaLuotkham;
                                lastCode = txtMaluotkham.Text;
                            }
                        }
                    }
                    else
                    {
                        txtMaluotkham.Text = _patient_Code;
                        lastCode = txtMaluotkham.Text;
                    }
                }
                DataTable dt_Patient = Utility.ExecuteSql(string.Format("select * from v_kcb_luotkham where ma_luotkham='{0}' order by ngay_tiepdon desc", txtMaluotkham.Text), CommandType.Text).Tables[0];
                if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                {
                    lastCode = txtMaluotkham.Text;
                    var q = new Select().From(VKcbLuotkham.Schema)
                          .Where(VKcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan]))
                          .And(VKcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MaLuotkham]))
                          .ExecuteAsCollection<VKcbLuotkhamCollection>();
                    if (q.Any())
                        objBenhnhan = q.FirstOrDefault();
                    txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                    objLuotkham =
                        new Select().From(KcbLuotkham.Schema)
                            .Where(KcbLuotkham.Columns.IdBenhnhan)
                            .IsEqualTo(txtIdBn.Text)
                            .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                            .ExecuteSingle<KcbLuotkham>();
                    _khoaphong = DmucKhoaphong.FetchByID(objLuotkham.IdKhoanoitru);
                    txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                    txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["nam_sinh"], "");
                    txtgioitinh.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.GioiTinh], "");
                    txtDiachi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                    txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MatheBhyt], "");
                    txtKhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_khoanoitru"], "");
                    txtBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucBuong.Columns.TenBuong], "");
                    txtGiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucGiuongbenh.Columns.TenGiuong], "");
                    txtDantoc.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_dantoc"], "");
                    txtTongiao.Text = Utility.sDbnull(dt_Patient.Rows[0]["ton_giao"], "");
                    txtNghenghiep.Text = Utility.sDbnull(dt_Patient.Rows[0]["nghe_nghiep"], "");
                    txtcoquan.Text = Utility.sDbnull(dt_Patient.Rows[0]["co_quan"], "");
                    txtCMT.Text = Utility.sDbnull(dt_Patient.Rows[0]["CMT"], "");
                    txtsovaovien.Text = Utility.sDbnull(dt_Patient.Rows[0]["so_vaovien"], "");
                    txtngaynhapvien.Text = Utility.sDbnull(dt_Patient.Rows[0]["sNgay_nhapvien"], "");
                    txtIdkhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdKhoanoitru], "-1");
                    txtIdravien.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdRavien], "-1");
                    txtidBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdBuong], "-1");
                    txtidgiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdGiuong], "-1");
                    txtNguoiLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["nguoi_lienhe"], "-1");
                    txtSDTLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["dienthoai_lienhe"], "-1");
                    txtDiachiLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["diachi_lienhe"], "-1");
                    txtSDT.Text = Utility.sDbnull(dt_Patient.Rows[0]["dien_thoai"], "-1");

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
                objBenhnhan = null;
                    AllowTextChanged = false;
                    if (!AutoLoad)
                    {
                        var dtPatient = new DataTable();

                        objLuotkham = null;
                        string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                        ClearControls();
                        txtMaluotkham.Text = _patient_Code;
                        dtPatient = new KCB_THAMKHAM().TimkiemBenhnhan(txtMaluotkham.Text, -1, 1, 0);

                        DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
                        if (arrPatients.GetLength(0) <= 0)
                        {
                            if (dtPatient.Rows.Count > 1)
                            {
                                var frm = new frm_DSACH_BN_TKIEM("ALL", noitrungoaitru);
                                frm.trangthai_noitru = trangthai_noitru;
                                frm.MaLuotkham = txtMaluotkham.Text;
                                frm.dtPatient = dtPatient;
                                frm.ShowDialog();
                                if (!frm.has_Cancel)
                                {
                                    txtMaluotkham.Text = frm.MaLuotkham;
                                    lastCode = txtMaluotkham.Text;
                                }
                            }
                        }
                        else
                        {
                            txtMaluotkham.Text = _patient_Code;
                            lastCode = txtMaluotkham.Text;
                        }
                    }
                    DataTable dt_Patient = Utility.ExecuteSql(string.Format("select * from v_kcb_luotkham where ma_luotkham='{0}' order by ngay_tiepdon desc", txtMaluotkham.Text), CommandType.Text).Tables[0];
                globalVariables.AppLog.Trace("ExecuteSql done");
                if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                    {
                        lastCode = txtMaluotkham.Text;
                    var q = new Select().From(VKcbLuotkham.Schema)
                          .Where(VKcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan]))
                          .And(VKcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MaLuotkham]))
                          .ExecuteAsCollection<VKcbLuotkhamCollection>();
                    if (q.Any())
                        objBenhnhan = q.FirstOrDefault();
                    globalVariables.AppLog.Trace("get  VKcbLuotkham done");
                    txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                        objLuotkham =
                            new Select().From(KcbLuotkham.Schema)
                                .Where(KcbLuotkham.Columns.IdBenhnhan)
                                .IsEqualTo(txtIdBn.Text)
                                .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                                .ExecuteSingle<KcbLuotkham>();
                    globalVariables.AppLog.Trace("get  KcbLuotkham done");
                    _khoaphong = DmucKhoaphong.FetchByID(objLuotkham.IdKhoanoitru);
                        txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                        txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["nam_sinh"], "");
                        txtgioitinh.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.GioiTinh], "");
                        txtDiachi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                        txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MatheBhyt], "");
                        txtKhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_khoanoitru"], "");
                        txtBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucBuong.Columns.TenBuong], "");
                        txtGiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucGiuongbenh.Columns.TenGiuong], "");
                        txtDantoc.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_dantoc"], "");
                        txtTongiao.Text = Utility.sDbnull(dt_Patient.Rows[0]["ton_giao"], "");
                        txtNghenghiep.Text = Utility.sDbnull(dt_Patient.Rows[0]["nghe_nghiep"], "");
                        txtcoquan.Text = Utility.sDbnull(dt_Patient.Rows[0]["co_quan"], "");
                        txtCMT.Text = Utility.sDbnull(dt_Patient.Rows[0]["CMT"], "");
                        txtsovaovien.Text = Utility.sDbnull(dt_Patient.Rows[0]["so_vaovien"], "");
                        txtngaynhapvien.Text = Utility.sDbnull(dt_Patient.Rows[0]["sNgay_nhapvien"], "");
                        txtIdkhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdKhoanoitru], "-1");
                        txtIdravien.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdRavien], "-1");
                        txtidBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdBuong], "-1");
                        txtidgiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdGiuong], "-1");
                        txtNguoiLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["nguoi_lienhe"], "-1");
                        txtSDTLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["dienthoai_lienhe"], "-1");
                        txtDiachiLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["diachi_lienhe"], "-1");
                        txtSDT.Text = Utility.sDbnull(dt_Patient.Rows[0]["dien_thoai"], "-1");

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
                objBenhnhan = null;
                AllowTextChanged = false;
                if (!AutoLoad)
                {
                    var dtPatient = new DataTable();

                    objLuotkham = null;
                    string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                    ClearControls();
                    txtMaluotkham.Text = _patient_Code;
                    dtPatient = new KCB_THAMKHAM().TimkiemBenhnhan(txtMaluotkham.Text, -1, 1, 0);

                    DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
                    if (arrPatients.GetLength(0) <= 0)
                    {
                        if (dtPatient.Rows.Count > 1)
                        {
                            var frm = new frm_DSACH_BN_TKIEM("ALL", noitrungoaitru);
                            frm.trangthai_noitru = trangthai_noitru;
                            frm.MaLuotkham = txtMaluotkham.Text;
                            frm.dtPatient = dtPatient;
                            frm.ShowDialog();
                            if (!frm.has_Cancel)
                            {
                                txtMaluotkham.Text = frm.MaLuotkham;
                                lastCode = txtMaluotkham.Text;
                            }
                        }
                    }
                    else
                    {
                        txtMaluotkham.Text = _patient_Code;
                        lastCode = txtMaluotkham.Text;
                    }
                }
                DataTable dt_Patient = Utility.ExecuteSql(string.Format("select * from v_kcb_luotkham where ma_luotkham='{0}' order by ngay_tiepdon desc", txtMaluotkham.Text), CommandType.Text).Tables[0];
                if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                {
                    lastCode = txtMaluotkham.Text;
                    var q = new Select().From(VKcbLuotkham.Schema)
                         .Where(VKcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan]))
                         .And(VKcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MaLuotkham]))
                         .ExecuteAsCollection<VKcbLuotkhamCollection>();
                    if (q.Any())
                        objBenhnhan = q.FirstOrDefault();
                    txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                    objLuotkham =
                        new Select().From(KcbLuotkham.Schema)
                            .Where(KcbLuotkham.Columns.IdBenhnhan)
                            .IsEqualTo(txtIdBn.Text)
                            .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                            .ExecuteSingle<KcbLuotkham>();
                    _khoaphong = DmucKhoaphong.FetchByID(objLuotkham.IdKhoanoitru);
                    txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                    txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["nam_sinh"], "");
                    txtgioitinh.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.GioiTinh], "");
                    txtDiachi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                    txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MatheBhyt], "");
                    txtKhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_khoanoitru"], "");
                    txtBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucBuong.Columns.TenBuong], "");
                    txtGiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucGiuongbenh.Columns.TenGiuong], "");
                    txtDantoc.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_dantoc"], "");
                    txtTongiao.Text = Utility.sDbnull(dt_Patient.Rows[0]["ton_giao"], "");
                    txtNghenghiep.Text = Utility.sDbnull(dt_Patient.Rows[0]["nghe_nghiep"], "");
                    txtcoquan.Text = Utility.sDbnull(dt_Patient.Rows[0]["co_quan"], "");
                    txtCMT.Text = Utility.sDbnull(dt_Patient.Rows[0]["CMT"], "");
                    txtsovaovien.Text = Utility.sDbnull(dt_Patient.Rows[0]["so_vaovien"], "");
                    txtngaynhapvien.Text = Utility.sDbnull(dt_Patient.Rows[0]["sNgay_nhapvien"], "");
                    txtIdkhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdKhoanoitru], "-1");
                    txtIdravien.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdRavien], "-1");
                    txtidBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdBuong], "-1");
                    txtidgiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdGiuong], "-1");
                    txtNguoiLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["nguoi_lienhe"], "-1");
                    txtSDTLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["dienthoai_lienhe"], "-1");
                    txtDiachiLienhe.Text = Utility.sDbnull(dt_Patient.Rows[0]["diachi_lienhe"], "-1");
                    txtSDT.Text = Utility.sDbnull(dt_Patient.Rows[0]["dien_thoai"], "-1");

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
