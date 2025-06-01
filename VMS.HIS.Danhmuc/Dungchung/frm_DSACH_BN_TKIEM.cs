using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
using VNS.HIS.BusRule.Classes;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_DSACH_BN_TKIEM : Form
    {
        public string MaLuotkham = "";
        public long IdBenhnhan = -1;
        public bool has_Cancel = true;
        public int DepartmentId = -1;
        public bool AutoSearch = false;
        string _args = "ALL";
        public byte noitrungoaitru = 100;
        public byte trangthai_noitru = 100;
        KcbLuotkham objLuotkham = null;
        public frm_DSACH_BN_TKIEM(string args,byte noitrungoaitru)
        {
            this._args = args;
            InitializeComponent();
            Utility.SetVisualStyle(this);
            NapTrangthaiDieutri();
            dtmFrom.Value = dtmTo.Value = DateTime.Now;
            this.noitrungoaitru = noitrungoaitru;
            this.KeyDown += new KeyEventHandler(frm_DSACH_BN_TKIEM_KeyDown);
            grdPatient.DoubleClick += new EventHandler(grdPatient_DoubleClick);
            grdPatient.KeyDown+=new KeyEventHandler(grdPatient_KeyDown);
            grdPatient.SelectionChanged += grdPatient_SelectionChanged;
            chkByDate.CheckedChanged+=new EventHandler(chkByDate_CheckedChanged);
            cmdTimKiem.Click += new EventHandler(cmdTimKiem_Click);
            txtPatientCode.KeyDown += new KeyEventHandler(txtPatientCode_KeyDown);
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        void grdPatient_SelectionChanged(object sender, EventArgs e)
        {
            Utility.SetMsg(lblTrangthainoitru, "", false);
            if (!Utility.isValidGrid(grdPatient))
            {
                objLuotkham = null;
            }
            else
            {
                objLuotkham = Utility.getKcbLuotkham(grdPatient.CurrentRow);
                Utility.SetMsg(lblTrangthainoitru, Utility.Laythongtintrangthainguoibenh(objLuotkham), false);
            }
        }
        public void FillAndSearchData(bool theongay,string IdBenhnhan,string MaLuotkham,string TenBenhnhan,string CMT,DateTime ngay_sinh,byte id_gioitinh, string Dienthoai,string IdDoituongKCB)
        {
            chkByDate.Checked = theongay;
            txtPatient_ID.Text = IdBenhnhan;
            txtPatientCode.Text = MaLuotkham;
            txtPatientName.Text = TenBenhnhan;
            txtCMT.Text = CMT;
            if (ngay_sinh.Year == 1900)
            {
                chkNgaysinh.Checked = false;
            }
            else
            {
                chkNgaysinh.Checked = true;
                dtpNgaysinh.Value = ngay_sinh;
            }
            cboPatientSex.SelectedValue = id_gioitinh;
            txtDienthoai.Text = Dienthoai;
            cboObjectType.SelectedIndex = Utility.GetSelectedIndex(cboObjectType, IdDoituongKCB);
            TimKiemThongTin(theongay);
        }
        public void FillAndSearchData(bool theongay, string IdBenhnhan, string MaLuotkham, string TenBenhnhan, string CMT, string Dienthoai, string IdDoituongKCB)
        {
            chkByDate.Checked = theongay;
            txtPatient_ID.Text = IdBenhnhan;
            txtPatientCode.Text = MaLuotkham;
            txtPatientName.Text = TenBenhnhan;
            txtCMT.Text = CMT;
            
           
            txtDienthoai.Text = Dienthoai;
            cboObjectType.SelectedIndex = Utility.GetSelectedIndex(cboObjectType, IdDoituongKCB);
            TimKiemThongTin(theongay);
        }
        void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatientCode.Text.Trim()) != "")
                {
                    string _ID = txtPatient_ID.Text.Trim();
                    string _Name = txtPatientName.Text.Trim();
                    int _Idx = cboObjectType.SelectedIndex;
                    string patientId = Utility.GetYY(DateTime.Now) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPatientCode.Text, 0), "000000");
                    txtPatient_ID.Clear();
                    txtPatientName.Clear();
                    cboObjectType.SelectedIndex = -1;
                    txtPatientCode.Text = patientId;
                    optTatCa.Checked = true;
                    TimKiemThongTin(false);
                    cboObjectType.SelectedIndex = _Idx;
                    txtPatientName.Text = _Name;
                    txtPatient_ID.Text = _ID;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                //throw;
            }
        }

        void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin(true);
        }
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtmTo.Enabled = dtmFrom.Enabled = chkByDate.Checked;
        }
        void TimKiemThongTin(bool theongay)
        {
            try
            {
                int Hos_status = -1;
                if (optNgoaiTru.Checked) Hos_status = 0;
                if (optNoiTru.Checked) Hos_status = 1;
                byte trangthaidieutri = Utility.ByteDbnull(cboTrangthainoitru.SelectedValue, 100);
                DataTable mDtPatient = new KCB_DANGKY().KcbTimkiemDanhsachBenhnhan(theongay ? (chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                    theongay ? (chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                                                     Utility.Int32Dbnull(cboObjectType.SelectedValue, -1), Hos_status,
                                                     Utility.sDbnull(txtPatientName.Text),
                                                     Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                     Utility.sDbnull(txtPatientCode.Text),
                                                     Utility.sDbnull(txtCMT.Text),chkNgaysinh.Checked?dtpNgaysinh.Value:new DateTime(1900,1,1),Utility.ByteDbnull(cboPatientSex.SelectedValue,100),
                                                     Utility.sDbnull(txtDienthoai.Text), globalVariables.MA_KHOA_THIEN, 0,
                                                     trangthai_noitru,trangthaidieutri,
                                                     Utility.sDbnull(this._args.Split('-')[0],"ALL"));
              Utility.SetDataSourceForDataGridEx(grdPatient, mDtPatient, true, true, "1=1", KcbDanhsachBenhnhan.Columns.IdBenhnhan + " desc");
              grdPatient.MoveFirst();
              Utility.focusCell(grdPatient, KcbDanhsachBenhnhan.Columns.TenBenhnhan);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
               // ModifyCommand();
            }
        }
        void grdPatient_DoubleClick(object sender, EventArgs e)
        {
            grdPatient_KeyDown(grdPatient, new KeyEventArgs(Keys.Enter));
        }

        void frm_DSACH_BN_TKIEM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                has_Cancel = true;
                this.Close();
            }
        }
        public DataTable dtPatient;
        private void frm_DSACH_BN_TKIEM_Load(object sender, EventArgs e)
        {
            try
            {
                if (noitrungoaitru == 1)
                    dtmFrom.Value = DateTime.Now.AddMonths(-1);
                dtmTo.Value = DateTime.Now;
                cboTrangthainoitru.SelectedIndex = 0;
                if (noitrungoaitru == 100) optTatCa.Checked = true;
                else if (noitrungoaitru == 0) optNgoaiTru.Checked = true;
                else if (noitrungoaitru == 1) optNoiTru.Checked = true;
                if (!AutoSearch)
                {
                    Utility.SetDataSourceForDataGridEx(grdPatient, dtPatient, true, true, "1=1", KcbDanhsachBenhnhan.Columns.IdBenhnhan + " desc");
                    grdPatient.MoveFirst();
                    Utility.focusCell(grdPatient, KcbDanhsachBenhnhan.Columns.TenBenhnhan);
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình load thông tin bệnh nhân");
            }
        }
        void NapTrangthaiDieutri()
        {
            DataTable dtTthai = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("TRANGTHAI_DIEUTRI").And(DmucChung.Columns.TrangThai).IsEqualTo(1).OrderAsc(DmucChung.Columns.SttHthi).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboTrangthainoitru, dtTthai, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            cboTrangthainoitru.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtTthai); //cboTrangthai.Items.Count > 0 ? 0 : -1;
        }
        private void grdPatient_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPatient)) return;
                if (e.KeyCode == Keys.Enter)
                {
                    MaLuotkham = Utility.sDbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, "");
                    IdBenhnhan = Utility.Int64Dbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value, -1);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    has_Cancel = false;
                    Close();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    Close();
                }

            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình chọn bệnh nhân");

            }
        }

        private void chkNgaysinh_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgaysinh.Enabled = chkNgaysinh.Checked;
        }
    }
}
