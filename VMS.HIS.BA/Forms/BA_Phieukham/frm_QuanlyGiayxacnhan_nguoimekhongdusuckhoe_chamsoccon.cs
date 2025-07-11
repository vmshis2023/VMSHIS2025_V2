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
    public partial class frm_QuanlyGiayxacnhan_nguoimekhongdusuckhoe_chamsoccon : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
       
        public frm_QuanlyGiayxacnhan_nguoimekhongdusuckhoe_chamsoccon()
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
            Load += frm_QuanlyGiayxacnhan_nguoimekhongdusuckhoe_chamsoccon_Load;
            KeyDown += frm_QuanlyGiayxacnhan_nguoimekhongdusuckhoe_chamsoccon_KeyDown;
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

        private void frm_QuanlyGiayxacnhan_nguoimekhongdusuckhoe_chamsoccon_Load(object sender, EventArgs e)
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
            m_dtData = SPs.Tt25GiayxacnhanNguoimekhongdusuckhoeChamsocconLaydanhsach(-1,tungay, denngay, "", ma_luotkham,ten_benhnhan).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngayxacnhan,ten_benhnhan");
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
        private void frm_QuanlyGiayxacnhan_nguoimekhongdusuckhoe_chamsoccon_KeyDown(object sender, KeyEventArgs e)
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
            frm_giayxacnhan_nguoimekhongdusuckhoe_chamsoccon giayxacnhan = new frm_giayxacnhan_nguoimekhongdusuckhoe_chamsoccon();
            giayxacnhan.mv_blnCallFromMenu = false;
            giayxacnhan._OnCreated += _OnCreated;
            giayxacnhan.m_enAct = action.Insert;
            giayxacnhan.ShowDialog();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            frm_giayxacnhan_nguoimekhongdusuckhoe_chamsoccon giayxacnhan = new frm_giayxacnhan_nguoimekhongdusuckhoe_chamsoccon();
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
                DataTable dt_temp = SPs.Tt25GiayxacnhanNguoimekhongdusuckhoeChamsocconLaydanhsach(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1),"","","").GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id, grdList.GetValue(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id)));
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
                        arrDr[0]["ngayxacnhan"] = dt_temp.Rows[0]["ngayxacnhan"];
                        arrDr[0]["so_hoso"] = dt_temp.Rows[0]["so_hoso"];
                        arrDr[0]["ngay_vaovien"] = dt_temp.Rows[0]["ngay_vaovien"];
                        //arrDr[0]["ngay_ravien"] = dt_temp.Rows[0]["ngay_ravien"];
                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, "id", id.ToString());
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
                        long IdPhieu = Utility.Int32Dbnull(grdList.GetValue(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id), -1);
                        new Delete().From(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Schema).Where(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id).IsEqualTo(IdPhieu).Execute();
                        emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON, Loaiphieu_HIS.TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON);
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
                Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon objGiayXacnhan = Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.FetchByID(Utility.Int32Dbnull(grdList.GetValue(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id), -1));
                if (objGiayXacnhan == null)
                {
                    Utility.ShowMsg(string.Format("Giấy xác nhận người mẹ không đủ sức khỏe chăm sóc con của người bệnh {0} có thể đã bị người khác xóa ở chức năng khác. Vui lòng kiểm tra lại bằng cách nhấn nút tìm kiếm", grdList.GetValue("ten_benhnhan").ToString()));
                    return;
                }
                
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa Giấy xác nhận người mẹ không đủ sức khỏe chăm sóc con {0} của người bệnh {1} hay không?", grdList.GetValue(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.SoHoso).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa", true))
                {
                    if (DeleteMe())
                    {
                        Utility.ShowMsg(string.Format("Xóa Giấy xác nhận người mẹ không đủ sức khỏe chăm sóc con cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id, grdList.GetValue(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id)));
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
                    Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon giayxacnhan = new Select().From(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Schema)
                           .Where(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id).IsEqualTo(Utility.Int64Dbnull( grdList.GetValue(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id)))
                           .ExecuteSingle<Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon>();
                    if (giayxacnhan==null)
                    {
                        Utility.ShowMsg("Giấy xác nhận người mẹ không đủ sức khỏe chăm sóc con không tồn tại.\nCó thể đã bị người khác xóa trong khi bạn đang thao tác.\nVui lòng kiểm tra lại");
                       
                        return;
                    }
                    DataTable dtData = SPs.Tt25GiayxacnhanNguoimekhongdusuckhoeChamsocconLaythongtinIn(giayxacnhan.Id).GetDataSet().Tables[0];
                    dtData.TableName = "TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON";
                    dtData.Rows[0]["sngaygio_nhapvien"] = giayxacnhan != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(giayxacnhan.Ngayvaovien, "") : ".......... giờ ....... ngày ........./........./.............";

                    dtData.Rows[0]["sngayxacnhan"] = Utility.FormatDateTime(Utility.sDbnull(dtData.Rows[0]["sngayxacnhan"], ""), "ngày......tháng......năm.........");
                    WordPrinter.InPhieu(dtData, "TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON.doc", "");


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
