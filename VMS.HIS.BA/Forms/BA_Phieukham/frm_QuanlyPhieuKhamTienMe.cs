using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.Forms.Noitru;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.Forms.NGOAITRU;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using VMS.HIS.Bus.Emr;
using VMS.HIS.UI.EMR;
using VMS.HIS.Bus;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_QuanlyPhieuKhamTienMe : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
       
        public frm_QuanlyPhieuKhamTienMe()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
           
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtMaluotkham.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_QuanlyPhieuKhamTienMe_Load;
            KeyDown += frm_QuanlyPhieuKhamTienMe_KeyDown;
            grdList.MouseDoubleClick += GrdList_MouseDoubleClick;
        }

        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdUpdate.PerformClick();
        }

        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_QuanlyPhieuKhamTienMe_Load(object sender, EventArgs e)
        {
            
            InitData();
            TimKiemThongTin();
            ModifyCommand();
            
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
            dtpNgayin.Value = dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
        }
       
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdList);
            cmdUpdate.Enabled = cmdDelete.Enabled =cmdPrint.Enabled=cmd_KySo.Enabled= isValid;
        }

        private void TimKiemThongTin()
        {
            DateTime tungay=chkByDate.Checked ? dtFromDate.Value.Date : new DateTime(1900,1,1);
            DateTime denngay =chkByDate.Checked ? dtToDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59) : new DateTime(1900, 1, 1);
            string ma_luotkham=(Utility.DoTrim(txtMaluotkham.Text));
            string ten_benhnhan=(Utility.DoTrim(txtTennguoibenh.Text));
          
            if (ma_luotkham.Length > 0)
            {
                tungay= new DateTime(1900, 1, 1);
                denngay = new DateTime(1900, 1, 1);
                ten_benhnhan = "";
            }
            m_dtData = SPs.EmrPt03PhieukhamTienmeLaydanhsach(-1,tungay, denngay, "", ma_luotkham,ten_benhnhan).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "sngay_kham,ten_benhnhan");
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện trạng thái của tmf kiếm từ ngày đến ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
      
        /// <summary>
        /// hàm thưc hiện việc tìm kiếm htoong tin nhanh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadMaLanKham();
                chkByDate.Checked = false;
                cmdTimKiem.PerformClick();
            }
        }
        private void LoadMaLanKham()
        {
            MaLuotkham = Utility.sDbnull(txtMaluotkham.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtMaluotkham.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                txtMaluotkham.Text = MaLuotkham;
                txtMaluotkham.Select(txtMaluotkham.Text.Length, txtMaluotkham.Text.Length);
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_QuanlyPhieuKhamTienMe_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtMaluotkham.Focus();
                txtMaluotkham.SelectAll();
            }
            if(e.KeyCode==Keys.N&&e.Control)cmdInsert.PerformClick();
            if(e.KeyCode==Keys.U&&e.Control)cmdUpdate.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.KeyCode == Keys.P && e.Control) cmdPrint.PerformClick();
        }
     
        KcbLuotkham objKcbLuotkham = null;
       

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frm_PT03_PhieuKhamTienMe _bienban = new frm_PT03_PhieuKhamTienMe();
            _bienban.mv_blnCallFromMenu = false;
            _bienban._OnCreated += _OnCreated;
            _bienban.m_enAct = action.Insert;
            _bienban.ShowDialog();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            frm_PT03_PhieuKhamTienMe _bienban = new frm_PT03_PhieuKhamTienMe();
            _bienban.mv_blnCallFromMenu = false;
            _bienban.m_enAct = action.Update;
            _bienban._OnCreated += _OnCreated;
            _bienban.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString();
            _bienban.ucThongtinnguoibenh_emr_basic1.Refresh();
            _bienban.ShowDialog();

        }
        void _OnCreated(long id, action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.EmrPt03PhieukhamTienmeLaydanhsach(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1),"","","").GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrPt03PhieukhamTienme.Columns.Id, grdList.GetValue(EmrPt03PhieukhamTienme.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
                if (m_enAct == action.Insert && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtData.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (m_enAct == action.Update && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtData.Select("Id=" + id);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["sngay_kham"] = dt_temp.Rows[0]["sngay_kham"];
                       
                        arrDr[0]["so_phieu"] = dt_temp.Rows[0]["so_phieu"];
                     
                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, EmrPt03PhieukhamTienme.Columns.Id, id.ToString());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }
        EmrDocuments emrdoc = new EmrDocuments();
        bool DeleteMe()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        long IdPhieu = Utility.Int32Dbnull(grdList.GetValue(EmrPt03PhieukhamTienme.Columns.Id), -1);
                        new Delete().From(EmrPt03PhieukhamTienme.Schema).Where(EmrPt03PhieukhamTienme.Columns.Id).IsEqualTo(IdPhieu).Execute();
                        emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.PHIEUKHAM_TIENME, Loaiphieu_HIS.PHIEUKHAM_TIENME);
                    }
                    scope.Complete();


                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                EmrPt03PhieukhamTienme obj_bienban = EmrPt03PhieukhamTienme.FetchByID(Utility.Int32Dbnull(grdList.GetValue(EmrPt03PhieukhamTienme.Columns.Id), -1));
                if (obj_bienban == null)
                {
                    Utility.ShowMsg(string.Format("Phiếu khám tiền mê của người bệnh {0} có thể đã bị người khác xóa ở chức năng khác. Vui lòng kiểm tra lại bằng cách nhấn nút tìm kiếm", grdList.GetValue("ten_benhnhan").ToString()));
                    return;
                }
                
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu {0} của người bệnh {1} hay không?", grdList.GetValue(EmrPt03PhieukhamTienme.Columns.SoPhieu).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa", true))
                {
                    if (DeleteMe())
                    {
                        Utility.ShowMsg(string.Format("Xóa Phiếu khám tiền mê cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrPt03PhieukhamTienme.Columns.Id, grdList.GetValue(EmrPt03PhieukhamTienme.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                try
                {
                    long Id = Utility.Int64Dbnull(grdList.GetValue(EmrPt03PhieukhamTienme.Columns.Id));

                    EmrPt03PhieukhamTienme _phieu = new Select().From(EmrPt03PhieukhamTienme.Schema)
                       .Where(EmrPt03PhieukhamTienme.Columns.Id).IsEqualTo(Id)
                       .ExecuteSingle<EmrPt03PhieukhamTienme>();
                    if (_phieu.Id <= 0)
                    {
                        Utility.ShowMsg("Bạn cần lưu thông tin Phiếu khám tiền mê trước khi thực hiện in phiếu");
                        return;
                    }
                    DataTable dtData = SPs.EmrPt03PhieukhamTienmeLaythongtinIn(_phieu.Id).GetDataSet().Tables[0];
                    dtData.TableName = "PHIEU_KHAM_TIEN_ME";
                    dtData.Rows[0]["sngay_kham"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayKham, "") : "Ngày..........tháng........năm..........";
                 
                    WordPrinter.InPhieu(dtData, "PHIEU_KHAM_TIEN_ME.doc", "", false, @"\MergeFields\PHIEU_KHAM_TIEN_ME_CHECKED_FIELDS.txt");


                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
              
            }
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtpNgayin.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtSohoso.Clear();
            txtTennguoibenh.Clear();
            txtMaluotkham.Focus();

        }
     
    }
}
