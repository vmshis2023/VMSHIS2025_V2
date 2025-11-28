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
    public partial class frm_QuanlyHosoTheoDoiSoSinh : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
       
        public frm_QuanlyHosoTheoDoiSoSinh()
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
            Load += frm_QuanlyHosoTheoDoiSoSinh_Load;
            KeyDown += frm_QuanlyHosoTheoDoiSoSinh_KeyDown;
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

        private void frm_QuanlyHosoTheoDoiSoSinh_Load(object sender, EventArgs e)
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
            cmdUpdate.Enabled = cmdDelete.Enabled =cmdPrint.Enabled= isValid;
        }

        private void TimKiemThongTin()
        {
            DateTime tungay=chkByDate.Checked ? dtFromDate.Value.Date : new DateTime(1900,1,1);
            DateTime denngay =chkByDate.Checked ? dtToDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59) : new DateTime(1900, 1, 1);
            string ma_luotkham=(Utility.DoTrim(txtMaluotkham.Text));
            string ten_benhnhan=(Utility.DoTrim(txt_hoten_me.Text));
          
            if (ma_luotkham.Length > 0)
            {
                tungay= new DateTime(1900, 1, 1);
                denngay = new DateTime(1900, 1, 1);
                ten_benhnhan = "";
            }
            m_dtData = SPs.EmrHosoTheodoiSosinhLaydanhsach(-1,tungay, denngay, "",-1, ma_luotkham,ten_benhnhan,"","",100).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngay_phieu,ngaysinh_be,hoten_be");
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
        private void frm_QuanlyHosoTheoDoiSoSinh_KeyDown(object sender, KeyEventArgs e)
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
            frm_hoso_theodoi_sosinh _phieu = new frm_hoso_theodoi_sosinh();
            _phieu.mv_blnCallFromMenu = false;
            _phieu._OnCreated += _OnCreated;
            _phieu.m_enAct = action.Insert;
            _phieu.InitData(null, null);
            _phieu.ShowDialog();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            frm_hoso_theodoi_sosinh _phieu = new frm_hoso_theodoi_sosinh();
            _phieu.mv_blnCallFromMenu = false;
            _phieu.m_enAct = action.Update;
            _phieu._OnCreated += _OnCreated;
            objKcbLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
            _phieu.InitData(objKcbLuotkham, EmrHosoTheodoiSosinh.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id"))));
            _phieu.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString();
            _phieu.ucThongtinnguoibenh_emr_basic1.Refresh();
            _phieu.ShowDialog();

        }
        void _OnCreated(long id, action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.EmrHosoTheodoiSosinhLaydanhsach(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1),"",-1,"","","","",100).GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrHosoTheodoiSosinh.Columns.Id, grdList.GetValue(EmrHosoTheodoiSosinh.Columns.Id)));
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
                    DataRow[] arrDr = m_dtData.Select("id=" + id);
                    if (arrDr.Length > 0)
                    {
                        foreach (DataColumn col in m_dtData.Columns)
                        {
                            arrDr[0][col.ColumnName] = dt_temp.Rows[0][col.ColumnName];
                        }
                     
                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, EmrHosoTheodoiSosinh.Columns.Id, id.ToString());
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
                        long IdPhieu = Utility.Int32Dbnull(grdList.GetValue(EmrHosoTheodoiSosinh.Columns.Id), -1);
                        new Delete().From(EmrHosoTheodoiSosinh.Schema).Where(EmrHosoTheodoiSosinh.Columns.Id).IsEqualTo(IdPhieu).Execute();
                        emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.HOSOTHEODOI_SOSINH, Loaiphieu_HIS.HOSOTHEODOI_SOSINH);
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
                EmrHosoTheodoiSosinh objGiayXacnhan = EmrHosoTheodoiSosinh.FetchByID(Utility.Int32Dbnull(grdList.GetValue(EmrHosoTheodoiSosinh.Columns.Id), -1));
                if (objGiayXacnhan == null)
                {
                    Utility.ShowMsg(string.Format("Hồ sơ theo dõi sơ sinh của bé {0} con của sản phụ {1} có thể đã bị người khác xóa ở chức năng khác. Vui lòng kiểm tra lại bằng cách nhấn nút tìm kiếm", grdList.GetValue("hoten_be").ToString(), grdList.GetValue("ten_benhnhan").ToString()));
                    return;
                }
                
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa Hồ sơ theo dõi sơ sinh có mã: {0} của bé {1} con của sản phụ {2} hay không?", grdList.GetValue(EmrHosoTheodoiSosinh.Columns.MaPhieu).ToString(), grdList.GetValue("hoten_be").ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa", true))
                {
                    if (DeleteMe())
                    {
                        Utility.ShowMsg(string.Format("Xóa Hồ sơ theo dõi sơ sinh cho bé {0} thành công", grdList.GetValue("hoten_be").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrHosoTheodoiSosinh.Columns.Id, grdList.GetValue(EmrHosoTheodoiSosinh.Columns.Id)));
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
                    EmrHosoTheodoiSosinh _phieu = new Select().From(EmrHosoTheodoiSosinh.Schema)
                           .Where(EmrHosoTheodoiSosinh.Columns.Id).IsEqualTo(Utility.Int64Dbnull(grdList.GetValue(EmrHosoTheodoiSosinh.Columns.Id)))
                           .ExecuteSingle<EmrHosoTheodoiSosinh>();
                    if (_phieu.Id <= 0)
                    {
                        Utility.ShowMsg("Bạn cần lưu thông tin Hồ sơ theo dõi sơ sinh trước khi thực hiện in phiếu");
                        return;
                    }
                    WordPrinter.InHosoTheodoiSosinh(_phieu.Id, false);


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
            dtFromDate.Value = dtToDate.Value=dtpNgayin.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txt_maphieu.Clear();
            txt_hoten_bo.Clear();
            txt_hoten_con.Clear();
            txt_hoten_me.Clear();
            txtMaluotkham.Focus();
            cbo_gioitinh_con.SelectedIndex = -1;

        }

        private void cmd_phieutheodoi_sosinh_Click(object sender, EventArgs e)
        {
          
            //frm_hoso_theodoi_sosinh _hoso_theodoi_sosinh = new frm_hoso_theodoi_sosinh();
            //_hoso_theodoi_sosinh.InitData(grdList.CurrentRow);
            //_hoso_theodoi_sosinh.ShowDialog();
        }
    }
}
