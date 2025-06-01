using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using System.IO;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Quanlytamung : Form
    {
        public delegate void OnChangedData();
        
        public event OnChangedData _OnChangedData;

        action m_enAct = action.FirstOrFinished;
        bool AllowedChanged = false;
        bool AllowedChanged_maskedEdit = false;
        NoitruTamung objTamung = null;
        private DataTable m_dtTimKiembenhNhan=new DataTable();
        public TrangthaiNoitru _TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        public bool callfromMenu = true;
        public KcbLuotkham objLuotkham = null;
       
        public frm_Quanlytamung()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
          
            InitEvents();
            dtToDate.Value = dtFromDate.Value = globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
        }
        void InitEvents()
        {
            grdList.SelectionChanged+=new EventHandler(grdList_SelectionChanged);
            cmdTimKiem.Click+=cmdTimKiem_Click;
             
        }

      
       
      
       
        void grdTamung_SelectionChanged(object sender, EventArgs e)
        {
           
            
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

        private void frm_Quanlytamung_Load(object sender, EventArgs e)
        {
           
            InitData();
            fillSearchData();
            TimKiemThongTin();
          
         
            
        }
        void fillSearchData()
        {
            try
            {
                if (objLuotkham != null)
                {
                    txtPatientCode.Text = objLuotkham.MaLuotkham;
                    txtPatientName.Clear();
                    chkByDate.Checked = false;
                    cboKhoaChuyenDen.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }
        private DataTable m_dtKhoaNoiTru=new DataTable();
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
           
            
            m_dtKhoaNoiTru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0);
            DataBinding.BindDataCombobox(cboKhoaChuyenDen, m_dtKhoaNoiTru,
                                       DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,"",true);
            string _rowFilter = "1=1";
            
            m_dtKhoaNoiTru.DefaultView.RowFilter = _rowFilter;
            m_dtKhoaNoiTru.AcceptChanges();

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
       

        private string _rowFilter = "1=1";
        private void TimKiemThongTin()
        {
            m_dtTimKiembenhNhan =SPs.NoitruTimkiembenhnhanTheobenhan(Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue),-1,
                                                txtPatientCode.Text, 1,
                                                chkByDate.Checked ? dtFromDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                string.Empty, Utility.Int16Dbnull(-1), -1, 0, globalVariables.gv_intIDNhanvien).
                    GetDataSet().Tables[0];
            _rowFilter = "1=1";
                //if (PropertyLib._NoitruProperties.HienthiKhoatheonguoidung)
                //{
                //    _rowFilter = string.Format("{0}={1}", NoitruPhanbuonggiuong.Columns.IdKhoanoitru,
                //        Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue));
                //}
            Utility.SetDataSourceForDataGridEx(grdList, m_dtTimKiembenhNhan, true, true, _rowFilter, "");
            AllowedChanged = true;
            grdList.MoveFirst();
            objLuotkham = Utility.getKcbLuotkhamFromGrid(grdList);
            grdList_SelectionChanged(grdList, new EventArgs());
            //ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNG_NOITRU", 1);
           
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
        
       
       
      
      
        
        
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                objLuotkham = Utility.getKcbLuotkhamFromGrid(grdList);
                if (!AllowedChanged) return;
                ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNG_NOITRU", 1);
                ucThongtinnguoibenh_v41.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                ucThongtinnguoibenh_v41.Refresh_V1(false);
                //LayLichsuTamung();
                //m_enAct = action.FirstOrFinished;
                //SetControlStatus();
                //ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        
        
        void ShowLSuTamung()
        {
            if (!Utility.isValidGrid(grdList) || ucTamung1.grdTamung.GetDataRows().Length <= 1)
            {
                pnlTamUng.Width = 0;
            }
            else
            {
                pnlTamUng.Width = 600;
            }
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
            MaLuotkham = Utility.sDbnull(txtPatientCode.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtPatientCode.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtPatientCode.Text);
                txtPatientCode.Text = MaLuotkham;
                txtPatientCode.Select(txtPatientCode.Text.Length, txtPatientCode.Text.Length);
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_Quanlytamung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                if ((ucTamung1.ActiveControl != null && ucTamung1.ActiveControl.Name == ucTamung1.cboPttt.Name && !ucTamung1.cboNganhang.Enabled))
                {
                    ucTamung1.cmdGhi.Focus();
                }
                else
                    SendKeys.Send("\t");
                //ProcessTabKey(true);
                return;
            }
            if (e.KeyCode == Keys.F3)
            {
                cmdTimKiem.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
               
                    this.Close();
              
            }
            if (e.KeyCode == Keys.F2)
            {
                txtPatientCode.Focus();
                txtPatientCode.SelectAll();
                return;
            }

           
        }

      
     
    }
}
