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
using VNS.HIS.UI.NGOAITRU;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_QlyLichhenKCB : Form
    {
        public string MaLuotkham = "";
        public long IdBenhnhan = -1;
        public bool has_Cancel = true;
        public int DepartmentId = -1;
        public bool AutoSearch = false;
        string _args = "ALL";
        DataTable m_dtLsuGoiKCB = new DataTable();
        public frm_QlyLichhenKCB(string args)
        {
            this._args = args;
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(frm_QlyLichhenKCB_KeyDown);
            grdPatient.DoubleClick += new EventHandler(grdPatient_DoubleClick);
            grdPatient.KeyDown+=new KeyEventHandler(grdPatient_KeyDown);
            
            grdPatient.CurrentCellChanged += grdPatient_CurrentCellChanged;
            grdLsugoi.EditingCell += grdLsugoi_EditingCell;
            chkByDate.CheckedChanged+=new EventHandler(chkByDate_CheckedChanged);
            cmdTimKiem.Click += new EventHandler(cmdTimKiem_Click);
            txtPatientCode.KeyDown += new KeyEventHandler(txtPatientCode_KeyDown);
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            grdLsugoi.UpdatingCell += grdLsugoi_UpdatingCell;
            optAllHen.CheckedChanged += optAllHen_CheckedChanged;
            optChuatoihen.CheckedChanged += optAllHen_CheckedChanged;
            optDaquahen.CheckedChanged += optAllHen_CheckedChanged;
            grdLsugoi.KeyDown += grdLsugoi_KeyDown;
        }

        void grdLsugoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Utility.isValidGrid(grdLsugoi)) return;
            if (e.KeyCode == Keys.Delete)
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa lời gọi: " + Utility.getValueOfGridCell(grdLsugoi, "noi_dung"), "Xác nhận xóa", true))
                {
                    new Delete().From(KcbLichsugoihenKCB.Schema).Where(KcbLichsugoihenKCB.Columns.Id).IsEqualTo(Utility.getValueOfGridCell(grdLsugoi, "Id")).Execute();
                }
            }
        }

        void optAllHen_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
        }
        void SetFilter()
        {
            string filter = "";
            if (optChuatoihen.Checked)
                if (filter.Length <= 0)
                    filter = " _type=1";
                else
                    filter += " AND _type=1";
            else if (optDaquahen.Checked)
                if (filter.Length <= 0)
                    filter = " _type=0";
                else
                    filter += " AND _type=0";
            if (filter.Length <= 0) filter = "1=1";
            Utility.SetDataSourceForDataGridEx(grdPatient, m_dtLSuHen, true, true, filter, "_type desc, songay_conlai asc,ten_benhnhan");
        }
        void grdLsugoi_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                KcbLichsugoihenKCB objhen = KcbLichsugoihenKCB.FetchByID(Utility.Int64Dbnull(Utility.getValueOfGridCell(grdLsugoi, "id"), -1));
                if(objhen!=null)
                {
                    objhen.NoiDung =Utility.sDbnull( e.Value,"");
                    objhen.MarkOld();
                    objhen.IsNew = false;
                    objhen.Save();
                }
            }
            catch (Exception exx)
            {
                
            }
        }

        void grdLsugoi_EditingCell(object sender, EditingCellEventArgs e)
        {
            //if (!Utility.isValidGrid(grdLsugoi)) return;
            //if (e.Column.Key.ToUpper() == KcbLichsugoihenKCB.Columns.NoiDung.ToUpper())
            //{
            //    string _newval = e.Value.ToString();
            //    KcbLichsugoihenKCB obj = KcbLichsugoihenKCB.FetchByID(grdPatient.CurrentRow.Cells["id"].Value.ToString());
            //    obj.NoiDung = _newval;
            //    obj.IsNew = false;
            //    obj.MarkOld();
            //    obj.Save();
            //}
        }

        void grdPatient_CurrentCellChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPatient)) return;
            m_dtLsuGoiKCB=new KCB_DANGKY().KcbLaydsachGoihenKCB(Utility.sDbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, ""));
            Utility.SetDataSourceForDataGridEx(grdLsugoi, m_dtLsuGoiKCB, true, true, "1=1", "ngay_goi");
        }
        public void FillAndSearchData(bool theongay,string IdBenhnhan,string MaLuotkham,string TenBenhnhan,string CMT,string Dienthoai,string IdDoituongKCB)
        {
            chkByDate.Checked = theongay;
            txtPatient_ID.Text = IdBenhnhan;
            txtPatientCode.Text = MaLuotkham;
            txtPatientName.Text = TenBenhnhan;
            txtCMT.Text = CMT;
            txtDienthoai.Text = Dienthoai;
            cboObjectType.SelectedIndex = Utility.GetSelectedIndex(cboObjectType, IdDoituongKCB);
            TimKiemThongTin(true);
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
                    radTatCa.Checked = true;
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
        DataTable m_dtLSuHen = new DataTable();
        void TimKiemThongTin(bool theongay)
        {
            try
            {
                int Hos_status = -1;
                if (radNgoaiTru.Checked) Hos_status = 0;
                if (radNoiTru.Checked) Hos_status = 1;
                m_dtLSuHen = new KCB_DANGKY().KcbQuanlylichhenKCB(theongay ? (chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                    theongay ? (chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                                                     Utility.Int32Dbnull(cboObjectType.SelectedValue, -1), Hos_status,
                                                     Utility.sDbnull(txtPatientName.Text),
                                                     Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                     Utility.sDbnull(txtPatientCode.Text),
                                                     Utility.sDbnull(txtCMT.Text),
                                                     Utility.sDbnull(txtDienthoai.Text), globalVariables.MA_KHOA_THIEN, 0,
                                                     (byte) 100,
                                                     Utility.sDbnull(this._args.Split('-')[0], "ALL"), Utility.Int16Dbnull(nmrGoilaitruoc.Value));
                SetFilter();
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

        void frm_QlyLichhenKCB_KeyDown(object sender, KeyEventArgs e)
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
        private void frm_QlyLichhenKCB_Load(object sender, EventArgs e)
        {
            try
            {
                dtmFrom.Value = DateTime.Now;
                dtmTo.Value = DateTime.Now;
                cboTrangthainoitru.SelectedIndex = 0;
                //if (!AutoSearch)
                //{
                //    Utility.SetDataSourceForDataGridEx(grdPatient, m_dtLSuHen, true, true, filter, "_type desc, songay_conlai asc,ten_benhnhan");
                //    grdPatient.MoveFirst();
                //    Utility.focusCell(grdPatient, KcbDanhsachBenhnhan.Columns.TenBenhnhan);
                //}
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình load thông tin bệnh nhân");
            }
        }

        private void grdPatient_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utility.isValidGrid(grdPatient))
                {
                    frm_lichsukcb _lichsukcb = new frm_lichsukcb();
                    _lichsukcb.txtMaluotkham.Text = grdPatient.GetValue("ma_luotkham").ToString();
                    _lichsukcb.AutoLoad = true;
                    _lichsukcb.Anluoidanhsachbenhnhan = true;
                    _lichsukcb.ShowDialog();
                }
                else
                {
                    Utility.ShowMsg("Cần chọn người bệnh trên lưới danh sách trước khi nhấn xem lịch sử KCB. Vui lòng kiểm tra lại");
                }
            }
            catch (Exception)
            {

            }

            //try
            //{
            //    if (!Utility.isValidGrid(grdPatient)) return;
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        MaLuotkham = Utility.sDbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, "");
            //        IdBenhnhan = Utility.Int64Dbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value, -1);
            //        frm_KCB_LSKCB _KCB_LSKCB = new frm_KCB_LSKCB("KT-P;KSK-P");
            //        _KCB_LSKCB.id_benhnhan = IdBenhnhan;
            //        _KCB_LSKCB.ShowDialog();
                    
            //    }
            //    else if (e.KeyCode == Keys.Escape)
            //    {
            //        DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //        Close();
            //    }

            //}
            //catch (Exception)
            //{
            //    Utility.ShowMsg("Có lỗi trong quá trình chọn bệnh nhân");

            //}
        }

        private void mnuMark_Click(object sender, EventArgs e)
        {
            try
            {
                KcbLichsugoihenKCB obj = new KcbLichsugoihenKCB();
                obj.NoiDung = "Cuộc gọi hẹn lần thứ: " + (m_dtLsuGoiKCB.Rows.Count + 1).ToString();
                obj.MaLuotkham = Utility.sDbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, "");
                obj.IdBenhnhan = Utility.Int64Dbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value, -1);
                obj.NgayGoi = DateTime.Now;
                obj.IsNew = true;
                obj.Save();
                grdPatient_CurrentCellChanged(grdPatient, e);
            }
            catch (Exception)
            {

              
            }
        }

        private void mnuUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
