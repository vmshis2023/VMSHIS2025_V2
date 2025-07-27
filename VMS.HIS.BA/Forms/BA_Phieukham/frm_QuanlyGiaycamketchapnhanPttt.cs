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
    public partial class frm_QuanlyGiaycamketchapnhanPttt : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
       
        public frm_QuanlyGiaycamketchapnhanPttt()
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
            Load += frm_QuanlyGiaycamketchapnhanPttt_Load;
            KeyDown += frm_QuanlyGiaycamketchapnhanPttt_KeyDown;
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

        private void frm_QuanlyGiaycamketchapnhanPttt_Load(object sender, EventArgs e)
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
            string ten_benhnhan=(Utility.DoTrim(txtTennguoibenh.Text));
          
            if (ma_luotkham.Length > 0)
            {
                tungay= new DateTime(1900, 1, 1);
                denngay = new DateTime(1900, 1, 1);
                ten_benhnhan = "";
            }
            m_dtData = SPs.EmrPhieucamketchapnhanPtttLaydanhsach(-1,tungay, denngay, "", ma_luotkham,ten_benhnhan).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngay_camket,ten_benhnhan");
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
        private void frm_QuanlyGiaycamketchapnhanPttt_KeyDown(object sender, KeyEventArgs e)
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
            frm_phieucamketchapnhan_pttt giayxacnhan = new frm_phieucamketchapnhan_pttt();
            giayxacnhan.mv_blnCallFromMenu = false;
            giayxacnhan._OnCreated += _OnCreated;
            giayxacnhan.m_enAct = action.Insert;
            giayxacnhan.ShowDialog();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            frm_phieucamketchapnhan_pttt giayxacnhan = new frm_phieucamketchapnhan_pttt();
            giayxacnhan.mv_blnCallFromMenu = false;
            giayxacnhan.m_enAct = action.Update;
            giayxacnhan._OnCreated += _OnCreated;
            giayxacnhan.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString();
            giayxacnhan.ucThongtinnguoibenh_emr_basic1.Refresh();
            giayxacnhan.ShowDialog();

        }
        void _OnCreated(long id, action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.EmrPhieucamketchapnhanPtttLaydanhsach(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1),"","","").GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrPhieucamketchapnhanPttt.Columns.IdPhieu, grdList.GetValue(EmrPhieucamketchapnhanPttt.Columns.IdPhieu)));
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
                        arrDr[0]["ngay_camket"] = dt_temp.Rows[0]["ngay_camket"];
                        arrDr[0]["ma_phieu"] = dt_temp.Rows[0]["ma_phieu"];
                     
                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, EmrPhieucamketchapnhanPttt.Columns.IdPhieu, id.ToString());
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
                        long IdPhieu = Utility.Int32Dbnull(grdList.GetValue(EmrPhieucamketchapnhanPttt.Columns.IdPhieu), -1);
                        new Delete().From(EmrPhieucamketchapnhanPttt.Schema).Where(EmrPhieucamketchapnhanPttt.Columns.IdPhieu).IsEqualTo(IdPhieu).Execute();
                        emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.PHIEU_CAMKET_PTTT, Loaiphieu_HIS.PHIEU_CAMKET_PTTT);
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
                EmrPhieucamketchapnhanPttt objGiayXacnhan = EmrPhieucamketchapnhanPttt.FetchByID(Utility.Int32Dbnull(grdList.GetValue(EmrPhieucamketchapnhanPttt.Columns.IdPhieu), -1));
                if (objGiayXacnhan == null)
                {
                    Utility.ShowMsg(string.Format("Phiếu chấp nhận cam kết PTTT và Gây mê hồi sức của người bệnh {0} có thể đã bị người khác xóa ở chức năng khác. Vui lòng kiểm tra lại bằng cách nhấn nút tìm kiếm", grdList.GetValue("ten_benhnhan").ToString()));
                    return;
                }
                
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu {0} của người bệnh {1} hay không?", grdList.GetValue(EmrPhieucamketchapnhanPttt.Columns.MaPhieu).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa", true))
                {
                    if (DeleteMe())
                    {
                        Utility.ShowMsg(string.Format("Xóa phiếu giấy xác nhận tai nạn thương tích cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrPhieucamketchapnhanPttt.Columns.IdPhieu, grdList.GetValue(EmrPhieucamketchapnhanPttt.Columns.IdPhieu)));
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
                    EmrPhieucamketchapnhanPttt phieucamket    = new Select().From(EmrPhieucamketchapnhanPttt.Schema)
                           .Where(EmrPhieucamketchapnhanPttt.Columns.IdPhieu).IsEqualTo(Utility.Int64Dbnull(grdList.GetValue(EmrPhieucamketchapnhanPttt.Columns.IdPhieu)))
                           .ExecuteSingle<EmrPhieucamketchapnhanPttt>();
                    if (phieucamket.IdPhieu <= 0)
                    {
                        Utility.ShowMsg("Bạn cần lưu thông tin phiếu chấp thuận PTTT và Gây mê hồi sức trước khi thực hiện in phiếu");
                        return;
                    }
                    DataTable dtData = SPs.EmrPhieucamketchapnhanPtttLaythongtinIn(phieucamket.IdPhieu).GetDataSet().Tables[0];
                    dtData.TableName = "phieucamketchapnhan_pttt";
                    dtData.Rows[0]["sngay_camket"] = phieucamket != null ? Utility.FormatDateTime_gio_ngay_thang_nam(phieucamket.NgayCamket, "") : "Ngày ......./......./..........";
                    WordPrinter.InPhieu(dtData, "phieucamketchapnhan_pttt.doc", "");


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
