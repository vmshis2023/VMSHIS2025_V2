using System;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using System.Collections.Generic;


namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_TimKiem_BN : Form
    {
        public bool m_blnCancel = true;
        public DataTable p_mdtDataTimKiem = new DataTable();
        public string ten_benhnhan { get; set; }
        public string MaLuotkham { get; set; }
        public int IdBenhnhan { get; set; }
        public bool SearchByDate { get{return chkByDate.Checked;} set{chkByDate.Checked=value;} }
        public bool mv_blnCallFromMenu=true;
        public int SearchType = 0;
        public frm_TimKiem_BN()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            foreach (Janus.Windows.GridEX.GridEXColumn gridExColumn in grdList.RootTable.Columns)
            {
                gridExColumn.EditType = EditType.NoEdit;
            }
            this.KeyDown += frm_TimKiem_BN_KeyDown;
            dtDenNgay.Value = dtTuNgay.Value = globalVariables.SysDate;
            grdList.DoubleClick+=grdList_DoubleClick;
            grdList.SelectionChanged += grdList_SelectionChanged;
            grdList.KeyDown += grdList_KeyDown;
            cmdTimKiem.Click+=cmdTimKiem_Click;
            txtID.KeyDown += txtID_KeyDown;
            txtPatientCode.KeyDown += txtPatientCode_KeyDown;
            CauHinh();
        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;

            LayLichsuBuongGiuong();
        }
        void LayLichsuBuongGiuong()
        {
            try
            {
               // objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)));
                //Lấy tất cả lịch sử buồng giường
              DataTable  m_dtBG =
                    new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(
                        Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)),
                        Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), "-1", -1);
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, m_dtBG, false, true, "1=1",
                    NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
                grdBuongGiuong.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Loi :" + ex.Message);
            }
            finally
            {
                ShowLSuBuongGiuong();
            }
        }
        void ShowLSuBuongGiuong()
        {
            if (!Utility.isValidGrid(grdList) || grdBuongGiuong.GetDataRows().Length <= 1)
            {
                grdBuongGiuong.Width = 0;
            }
            else
            {
                grdBuongGiuong.Width = 425;
            }
        }
        void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && Utility.sDbnull(txtPatientCode.Text, "").Length > 0)
            {
                if (chkQuickSearchOn.Checked)
                {
                    txtPatientCode.Text = Utility.AutoFullPatientCode(txtPatientCode.Text);
                    TimKiem(2);
                }
            }
        }

        void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.sDbnull(txtID.Text, "").Length > 0)
            {
                if (chkQuickSearchOn.Checked)
                {
                    TimKiem(1);
                }
            }
        }

        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.isValidGrid(grdList) && e.KeyCode == Keys.Enter)
            {
                AcceptData();
            }
        }

        void frm_TimKiem_BN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F3)
            {
            }
        }

        private void CauHinh()
        {
           
        }
        private  DataTable m_dtKhoaNoiTru=new DataTable();
        /// <summary>
        /// hàm thực hiện việc load thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_TimKiem_BN_Load(object sender, EventArgs e)
        {
            txtPatientCode.Text = MaLuotkham;
            txtPatientName.Text = ten_benhnhan;
            if (globalVariables.IsAdmin)
            {
                m_dtKhoaNoiTru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                txtKhoanoitru.Init(m_dtKhoaNoiTru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            }
            else
            {
                m_dtKhoaNoiTru = THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1);
                txtKhoanoitru.Init(m_dtKhoaNoiTru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            }

            TimKiem(SearchType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchType">0= All; 1= ID;2=Mã khám;3= Tên người bệnh</param>
        private void TimKiem(int searchType)
        {
            string tungay = chkByDate.Checked ? dtTuNgay.Value.ToString("dd/MM/yyyy") : "01/01/1900";
            string denngay =  chkByDate.Checked ? dtDenNgay.Value.ToString("dd/MM/yyyy") : "01/01/1900";
            long id_benhnhan = Utility.Int64Dbnull(txtID.Text, -1);
            string ma_luotkham = Utility.sDbnull(txtPatientCode.Text);
            string ten_benhnhan = Utility.sDbnull(txtPatientName.Text);
            if (searchType == 1)
            {
                tungay = denngay="01/01/1900";
                ma_luotkham = "";
                ten_benhnhan = "";
            }
            else if (searchType == 2)
            {
                tungay = denngay = "01/01/1900";
                id_benhnhan = -1;
                ten_benhnhan = "";
            }
            else if (searchType == 3)
            {
                tungay = denngay = "01/01/1900";
                id_benhnhan = -1;
                ma_luotkham = "";
            }
            p_mdtDataTimKiem =
                SPs.NoitruTimkiembenhnhanTheobenhan(Utility.Int32Dbnull(txtKhoanoitru.MyID),id_benhnhan, ma_luotkham, -1, tungay, denngay, ten_benhnhan, 1, -1, 0, globalVariables.gv_intIDNhanvien).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, p_mdtDataTimKiem, true, true, "1=1", "");
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            AcceptData();
        }
        /// <summary>
        /// hàm thực hiện việc chấp nhận thông t ncuar bệnh nhân
        /// </summary>
        private void AcceptData()
        {
            if (mv_blnCallFromMenu) return;
            if (Utility.isValidGrid(grdList))
            {
                ten_benhnhan = Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan));
                MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                m_blnCancel = false;
                Close();
            }
        }

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtDenNgay.Enabled = dtTuNgay.Enabled = chkByDate.Checked;
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem(0);
        }

        private void frm_TimKiem_BN_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void lnkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtDenNgay.Value = dtTuNgay.Value = DateTime.Now;
            txtID.Clear();
            txtPatientCode.Clear();
            txtPatientName.Clear();
            txtKhoanoitru.SetId(-1);
            txtID.Focus();
        }
    }
}