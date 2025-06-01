using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UCs;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.Libs;
using VNS.Properties;
using VNS.UCs;
using System.Threading;
using VNS.HIS.UI.HinhAnh;
using VNS.HIS.UI.Forms.HinhAnh;
using Aspose.Words;
using System.Diagnostics;
using VNS.HIS.Classes;
using System.Transactions;
using VNS.HIS.UI.NOITRU;
using VMS.HIS.Danhmuc;

namespace VNS.HIS.UI.NGOAITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_lichsukcb : Form
    {
        private readonly KCB_THAMKHAM _kcbThamkham = new KCB_THAMKHAM();
        private readonly DataTable _dtIcdPhu = new DataTable();

        private readonly List<string> _lstResultColumns = new List<string>
        {
            "ten_chitietdichvu",
            "ketqua_cls",
            "binhthuong_nam",
            "binhthuong_nu"
        };
        private bool _buttonClick;
        private DataTable _mDtDanhsachbenhnhanthamkham = new DataTable();
        private DataSet _ds = new DataSet();
        private List<string> _lstVisibleColumns = new List<string>();
        private DataTable _mDtDonthuocChitietView = new DataTable();
        private DataTable _mDtPresDetail = new DataTable();
        public KcbDanhsachBenhnhan objBenhnhan = null;
        public KcbLuotkham objLuotkham = null;
        public bool AutoLoad = false;
        public bool Anluoidanhsachbenhnhan = false;
        private KcbDangkyKcb objKcbdangky;
        string SplitterPath = "";
        public frm_lichsukcb()
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            if (PropertyLib._HinhAnhProperties == null) PropertyLib._HinhAnhProperties = PropertyLib.GetHinhAnhProperties();
            KeyPreview = true;

            dtpNgaydangky.Value = dtpNgaykham.Value = globalVariables.SysDate;
            dtFromDate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;
            webBrowser1.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor_simple.html");
            InitEvents();

        }
        bool AllowSelectionChanged = false;
        bool AllowSelectionChanged1 = false;
        private void InitEvents()
        {

            Load += frm_lichsukcb_Load;
            FormClosing+=frm_lichsukcb_FormClosing;
            Shown += frm_lichsukcb_Shown;
            KeyDown += frm_lichsukcb_KeyDown;
            cmdSearch.Click += cmdSearch_Click;
            grdList.KeyDown += grdList_KeyDown;
            grdList.DoubleClick += grdList_DoubleClick;
            grdList.MouseClick += grdList_MouseClick;
            grdList.SelectionChanged += grdList_SelectionChanged;
            grdLuotkham.SelectionChanged += grdLuotkham_SelectionChanged;
            grdCongkham.SelectionChanged += grdCongkham_SelectionChanged;
            grdAssignDetail.SelectionChanged+=grdAssignDetail_SelectionChanged;
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;
            txtID.KeyDown += txtID_KeyDown;
            txtID.KeyPress += txtID_KeyPress;
            optNoitru.CheckedChanged += optAll_CheckedChanged;
            optNgoaitru.CheckedChanged += optAll_CheckedChanged;
        }
        void optAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string RowFilter = "1=1";
                PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Tatca;
                if (optNoitru.Checked)
                {
                    RowFilter = "noitru=1";
                }
                if (optNgoaitru.Checked)
                {
                    RowFilter = "noitru=0";
                }
                if (m_dtChidinhCLS != null && m_dtChidinhCLS.Rows.Count > 0 && m_dtChidinhCLS.Columns.Count > 0)
                {
                    m_dtChidinhCLS.DefaultView.RowFilter = RowFilter;
                    m_dtChidinhCLS.AcceptChanges();
                }
                if (m_dtDonthuoc != null && m_dtDonthuoc.Rows.Count > 0 && m_dtDonthuoc.Columns.Count > 0)
                {
                    m_dtDonthuoc.DefaultView.RowFilter = RowFilter;
                    m_dtDonthuoc.AcceptChanges();
                }
                if (m_dtVTTH != null && m_dtVTTH.Rows.Count > 0 && m_dtVTTH.Columns.Count > 0)
                {
                    m_dtVTTH.DefaultView.RowFilter = RowFilter;
                    m_dtVTTH.AcceptChanges();
                }
            }
            catch (Exception ex)
            {


            }
        }
        void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.sDbnull(txtID.Text).Length > 0)
                cmdSearch.PerformClick();
        }
        public void SearchmeNow()
        {
            try
            {
                AllowSelectionChanged = false;
                txtMaluotkham.Text = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                if (dtLuotkham != null && dtLuotkham.Columns.Contains("ma_luotkham") && dtLuotkham.Select("ma_luotkham='" + txtMaluotkham.Text + "'").Length > 0)//Tìm trên lưới
                {
                    Utility.GotoNewRowJanus(grdLuotkham, "ma_luotkham", txtMaluotkham.Text);
                    string id_benhnhan = grdLuotkham.GetValue(KcbLuotkham.Columns.IdBenhnhan).ToString();
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.IdBenhnhan, id_benhnhan);
                }
                else//Tìm từ CSDL
                {
                    SearchPatient(new DateTime(1900, 1, 1), new DateTime(1900, 1, 1),-1, Utility.DoTrim(txtMaluotkham.Text), "");
                }
                AllowSelectionChanged = true;
                grdList_SelectionChanged(grdList, new EventArgs());
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }



        }

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.sDbnull(txtMaluotkham.Text).Length > 0)
            {
                SearchmeNow();
            }

        }

        void grdCongkham_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged1) return;
                CLSLoaded = false;
                ThuocVTTHLoaded = false;
                if (!Utility.isValidGrid(grdCongkham))
                {
                    ClearControl();
                    return;
                }
                objKcbdangky = KcbDangkyKcb.FetchByID(Utility.Int64Dbnull(grdCongkham.GetValue(KcbDangkyKcb.Columns.IdKham), -1));
                HienthithongtinBn();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        string ma_luotkham_cu = "";
        void grdLuotkham_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowSelectionChanged) return;
            AllowSelectionChanged1 = false;
            BuongGiuongLoaded = false;
            TamungLoaded = false;
            LoadCongkham();
            AllowSelectionChanged1 = true;
            grdCongkham_SelectionChanged(grdCongkham, e);
        }
        void LoadCongkham()
        {
            try
            {
                if (!Utility.isValidGrid(grdLuotkham))
                {
                    ClearControl();
                }
                objLuotkham = Utility.getKcbLuotkham(grdLuotkham.CurrentRow);
                if (ma_luotkham_cu != objLuotkham.MaLuotkham)
                {
                    ma_luotkham_cu = objLuotkham.MaLuotkham;
                    BuongGiuongLoaded = false;
                    TamungLoaded = false;
                }
                DataTable dtData = _kcbThamkham.KcbLichsuKcbTimkiemphongkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,0);
                Utility.SetDataSourceForDataGridEx_Basic(grdCongkham, dtData, false, true, "", "");
                //grbRegs.Height = grdCongkham.GetDataRows().Length <= 1 ? 0 : 120;
                grdCongkham.MoveFirst();

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        DataTable dtLuotkham = new DataTable();
        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged) return;
                if (!Utility.isValidGrid(grdList))
                {
                    ClearControl();
                }
                int idbenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(idbenhnhan);
                dtLuotkham = _kcbThamkham.KcbLichsuKcbLuotkham(idbenhnhan);
                Utility.SetDataSourceForDataGridEx_Basic(grdLuotkham, dtLuotkham, true, true, "", "");
                UpdateGroup();
                grdLuotkham.MoveFirst();


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void UpdateGroup()
        {
            if (!Utility.isValidGrid(grdLuotkham)) return;
            var counts = ((DataView)grdLuotkham.DataSource).Table.AsEnumerable()
                .GroupBy(x => x.Field<string>("ma_doituong_kcb"))
                .Select(g => new { g.Key, Count = g.Count() });
            if (counts.Count() >= 2)
            {
                if (grdLuotkham.RootTable.Groups.Count <= 0)
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn) { GroupPrefix = "Nhóm đối tượng KCB: " };
                    grdLuotkham.RootTable.Groups.Add(gridExGroup);
                }
            }
            else
            {
                GridEXColumn gridExColumn = grdLuotkham.RootTable.Columns["ma_doituong_kcb"];
                var gridExGroup = new GridEXGroup(gridExColumn);
                grdLuotkham.RootTable.Groups.Clear();
            }
            grdLuotkham.UpdateData();
            grdLuotkham.Refresh();
        }

        void frm_lichsukcb_Shown(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            Try2Splitter();
            if (Anluoidanhsachbenhnhan)
                grdList.Height = 0;
            Utility.DefaultNow(this);
        }
        void Try2Splitter()
        {
            try
            {


                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count == 3)
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];
                    splitContainer2.SplitterDistance = lstSplitterSize[1];
                    splitContainer3.SplitterDistance = lstSplitterSize[2];
                }
            }
            catch (Exception)
            {

            }
        }


        void frm_lichsukcb_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString(),splitContainer2.SplitterDistance.ToString(), splitContainer3.SplitterDistance.ToString() });
            timer1.Stop();
            //this.Dispose();
        }

        private void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                HienthithongtinBn();
        }

        private void grdList_MouseClick(object sender, MouseEventArgs e)
        {
            if (PropertyLib._ThamKhamProperties.ViewAfterClick && !_buttonClick)
                HienthithongtinBn();
            _buttonClick = false;
        }



        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            HienthithongtinBn();
        }

        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin của thăm khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            AllowSelectionChanged = false;
            SearchPatient(dtFromDate.Value, dtToDate.Value, Utility.Int64Dbnull(txtID.Text,-1), Utility.DoTrim(txtMaluotkham.Text), Utility.DoTrim(txtTenBN.Text));
            AllowSelectionChanged = true;
            grdList_SelectionChanged(grdList, new EventArgs());
        }

        private void SearchPatient(DateTime tungay, DateTime denngay,long id_benhnhan, string ma_luotkham, string ten_benhnhan)
        {
            try
            {
                ClearControl();

                objKcbdangky = null;
                objBenhnhan = null;
                objLuotkham = null;
                if (_dtIcdPhu != null) _dtIcdPhu.Clear();

                if (_mDtPresDetail != null) _mDtPresDetail.Clear();
                _mDtDanhsachbenhnhanthamkham =
                    _kcbThamkham.KcbLichsuKcbTimkiemBenhnhan(tungay, denngay, ma_luotkham, id_benhnhan, ten_benhnhan, "", -1, "ALL");
                Utility.SetDataSourceForDataGridEx_Basic(grdList, _mDtDanhsachbenhnhanthamkham, true, true, "",
                    KcbDanhsachBenhnhan.Columns.TenBenhnhan);
                if (_dtIcdPhu != null && !_dtIcdPhu.Columns.Contains(DmucBenh.Columns.MaBenh))
                {
                    _dtIcdPhu.Columns.Add(DmucBenh.Columns.MaBenh, typeof(string));
                }
                if (_dtIcdPhu != null && !_dtIcdPhu.Columns.Contains(DmucBenh.Columns.TenBenh))
                {
                    _dtIcdPhu.Columns.Add(DmucBenh.Columns.TenBenh, typeof(string));
                }
                grd_ICD.DataSource = _dtIcdPhu;

                grdList.MoveFirst();

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void LaydanhsachbacsiChidinh()
        {
            try
            {
                //   m_dtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, 0);
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                              new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }
                if (globalVariables.IsAdmin)
                {
                    txtBacsi.Enabled = true;
                }
                else
                {
                    txtBacsi.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();

        private void Laythongtinchidinhngoaitru()
        {
            try
            {


                DataSet ds = _KCB_THAMKHAM.LaythongtinCLSTheoCongkham(objKcbdangky.IdBenhnhan, objKcbdangky.MaLuotkham, objKcbdangky.IdKham);
                DataTable m_dtChidinhCLS = ds.Tables[0];
                DataTable m_dtDonthuoc = ds.Tables[1].Clone();
                DataTable m_dtVTTH = ds.Tables[1].Clone();
                //Tạm Rem lại ko gom thuốc,vtth thành 1 đơn nữa. 230524
                DataRow[] arrtempt = ds.Tables[1].Select("kieu_thuocvattu = 'THUOC'");
                if (arrtempt.Length > 0) m_dtDonthuoc = arrtempt.CopyToDataTable();
                arrtempt = ds.Tables[1].Select("kieu_thuocvattu = 'VT'");
                //Utility.ShowMsg("OK1");
                if (arrtempt.Length > 0) m_dtVTTH = arrtempt.CopyToDataTable();
                Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtChidinhCLS, false, true, "", "");
                grdAssignDetail.MoveFirst();

                Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuoc, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);
                Utility.SetDataSourceForDataGridEx(grdVTTH, m_dtVTTH, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);

            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }

        private void Get_DanhmucChung()
        {
            InitICDPhu();
            txtTrieuChungBD.Init();
            txtChanDoan.Init();
            autoTrangthai.Init();
            autoHuongdieutri.Init();
            autoKet_Luan.Init();
            txtNhommau.Init();
            txtNhanxet.Init();
            txtChanDoanKemTheo.Init();
            txtCheDoAn.Init();
            AutoMabenhchinh.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
            autoMabenhphu.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });


        }


        private void frm_lichsukcb_Load(object sender, EventArgs e)
        {
            Get_DanhmucChung();
            txtMaluotkham.Focus();
            if (AutoLoad)
                SearchmeNow();
            
        }
        DataTable dt_ICD_PHU = new DataTable();
        private void ClearControl()
        {
            try
            {
              //  objBenhnhan = null;
                objLuotkham = null;
                objKcbdangky = null;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                grdAssignDetail.DataSource = null;
                grdPresDetail.DataSource = null;
                grdVTTH.DataSource = null;
                txtTenBenhChinh.Clear();
                txtTenBenhPhu.Clear();
                txtReg_ID.Text = "";

                txtCongkham.Clear();
                txtPhongkham.Clear();
                foreach (Control control in pnlPatientInfor.Controls)
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


                foreach (Control control in pnlKetluan.Controls)
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

                foreach (Control control in pnlother.Controls)
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
                txtSongaydieutri.Text = "0";
                txtSoNgayHen.Text = "0";
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        void Napthongtinnguoibenh()
        {
            if (objLuotkham != null)
            {
                ucThongtinnguoibenh_v31.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                ucThongtinnguoibenh_v31.Refresh();
            }
            else
                ucThongtinnguoibenh_v31.ClearControls();
        }
        void NapthongtinCongkham()
        {
            if (objKcbdangky == null) return;
            dtpNgaydangky.Text = grdCongkham.GetValue("sNgay_dangky").ToString();
            dtpNgaykham.Text = grdCongkham.GetValue("sNgay_kham").ToString();
            txtBacsi._Text = grdCongkham.GetValue("ten_bacsikham").ToString();
            txtPhongkham.Text = grdCongkham.GetValue("ten_phongkham").ToString();
            txtCongkham.Text = grdCongkham.GetValue("ten_dichvukcb").ToString();
        }
        long id_kham = -1;
        bool CLSLoaded = false;
        DataTable m_dtChidinhCLS = new DataTable();
        void NapthongtinChidinhCLS()
        {
            try
            {
                if (objKcbdangky == null) return;
                if (objKcbdangky.IdKham == id_kham && CLSLoaded) return;
                id_kham = objKcbdangky.IdKham;
                DataSet ds = _KCB_THAMKHAM.LaythongtinCLSTheoCongkham(objKcbdangky.IdBenhnhan, objKcbdangky.MaLuotkham, objKcbdangky.IdKham);
                m_dtChidinhCLS = ds.Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdAssignDetail, m_dtChidinhCLS, false, true, "", "");
                CLSLoaded = true;
                grdAssignDetail.MoveFirst();
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        bool ThuocVTTHLoaded = false;
        DataTable m_dtDonthuoc = new DataTable();
        DataTable m_dtVTTH = new DataTable();
        void NapthongtinDonthuoc_Vtth()
        {
            try
            {
                if (objKcbdangky == null) return;
                if (objKcbdangky.IdKham == id_kham && ThuocVTTHLoaded) return;
                id_kham = objKcbdangky.IdKham;

                DataSet ds = _KCB_THAMKHAM.LaythongtinThuocTheoCongkham(objKcbdangky.IdBenhnhan, objKcbdangky.MaLuotkham, objKcbdangky.IdKham);
                 m_dtDonthuoc = ds.Tables[0].Clone();
                 m_dtVTTH = ds.Tables[0].Clone();
                //Tạm Rem lại ko gom thuốc,vtth thành 1 đơn nữa. 230524
                DataRow[] arrtempt = ds.Tables[0].Select("kieu_thuocvattu = 'THUOC'");
                if (arrtempt.Length > 0) m_dtDonthuoc = arrtempt.CopyToDataTable();
                arrtempt = ds.Tables[0].Select("kieu_thuocvattu = 'VT'");
                //Utility.ShowMsg("OK1");
                if (arrtempt.Length > 0) m_dtVTTH = arrtempt.CopyToDataTable();

                Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuoc, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);
                Utility.SetDataSourceForDataGridEx(grdVTTH, m_dtVTTH, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);
                ThuocVTTHLoaded = true;

            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        bool TamungLoaded = false;
        void NapthongtinTamung()
        {
            if (TamungLoaded ) return;
            
            ucTamung1.ChangePatients(objLuotkham, "", 2);
            TamungLoaded = true;
        }
        bool BuongGiuongLoaded = false;
        void NapthongtinBuonggiuong()
        {
            try
            {

                if (BuongGiuongLoaded ) return;
                
                //Lấy tất cả lịch sử buồng giường
                DataTable dt_buonggiuong =
                    new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(chkDisplayType.Checked ? "" : objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "-1", -1);
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, dt_buonggiuong, false, true, "1=1", NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
                grdBuongGiuong.MoveFirst();
                BuongGiuongLoaded = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Loi :" + ex.Message);
            }
            finally
            {
            }
        }
        void NapthongtinChandoan()
        {
        }
        void InitICDPhu()
        {

            if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.MaBenh))
            {
                dt_ICD_PHU.Columns.Add(DmucBenh.Columns.MaBenh, typeof(string));
            }
            if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.TenBenh))
            {
                dt_ICD_PHU.Columns.Add(DmucBenh.Columns.TenBenh, typeof(string));
            }
            if (dt_ICD_PHU != null && !dt_ICD_PHU.Columns.Contains(KcbDangkyKcb.Columns.IdKham))
            {
                dt_ICD_PHU.Columns.Add(KcbDangkyKcb.Columns.IdKham, typeof(Int64));
            }
            grd_ICD.DataSource = dt_ICD_PHU;
        } 
        void NapthongtinHoibenhvaChandoan()
        {
            if (objKcbdangky == null) return;
            KcbChandoanKetluan _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                   .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objKcbdangky.IdKham).
                                   ExecuteSingle
                                   <KcbChandoanKetluan>();
            //Điền thông tin chung
            if (!Utility.Byte2Bool(objBenhnhan.IdGioitinh))//Nam
            {
                lblPara.Visible = txtPara.Visible = false;
                chkQuaibi.Visible = true;
            }
            else//Nữ
            {
                lblPara.Visible = txtPara.Visible = true;
                chkQuaibi.Visible = false;
            }
            txtMaBenhChinh.Text = Utility.sDbnull(objLuotkham.MabenhChinh);
            AutoMabenhchinh.SetCode(Utility.sDbnull(objLuotkham.MabenhChinh));
           // AutoMabenhchinh.RaiseEnterEvents();
            //txtTenBenhChinh.Text = Utility.sDbnull(_KcbChandoanKetluan.MotaBenhchinh);
            string mabenhphu_theocongkham = _KcbChandoanKetluan != null ? Utility.sDbnull(_KcbChandoanKetluan.MabenhPhu, "") : "";
            List<string> lstmabenhphu_congkham = mabenhphu_theocongkham.Split(',').ToList<string>();
            dt_ICD_PHU.Clear();
            if (!string.IsNullOrEmpty(mabenhphu_theocongkham))
            {
                foreach (string row in lstmabenhphu_congkham)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        DataRow newDr = dt_ICD_PHU.NewRow();
                        newDr[DmucBenh.Columns.MaBenh] = row;
                        newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                        newDr[KcbDangkyKcb.Columns.IdKham] = _KcbChandoanKetluan.IdKham;
                        dt_ICD_PHU.Rows.Add(newDr);
                        dt_ICD_PHU.AcceptChanges();
                    }
                }

            }

            //Fill mã bệnh phụ của các phòng khám khác
            string ma_benhphu = Utility.sDbnull(objLuotkham.MabenhPhu, "");
            if (!string.IsNullOrEmpty(ma_benhphu))
            {
                string[] rows = ma_benhphu.Split(',');
                foreach (string row in rows)
                {
                    if (!string.IsNullOrEmpty(row) && !lstmabenhphu_congkham.Contains(row))
                    {
                        DataRow newDr = dt_ICD_PHU.NewRow();
                        newDr[DmucBenh.Columns.MaBenh] = row;
                        newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                        newDr[KcbDangkyKcb.Columns.IdKham] = -1;
                        dt_ICD_PHU.Rows.Add(newDr);
                        dt_ICD_PHU.AcceptChanges();
                    }
                }

            }
            grd_ICD.DataSource = dt_ICD_PHU;

            if (_KcbChandoanKetluan != null)
            {
                autoKet_Luan.SetCode(Utility.sDbnull(_KcbChandoanKetluan.Ketluan));
                txtidchandoan.Text = Utility.sDbnull(_KcbChandoanKetluan.IdChandoan, "-1");
                // txtHuongdieutri.SetCode(_KcbChandoanKetluan.HuongDieutri);
                autoHuongdieutri.SetCode(_KcbChandoanKetluan.HuongDieutri);
                autoLoidan._Text = _KcbChandoanKetluan.LoiDan;
                autoXutri._Text = _KcbChandoanKetluan.XuTri;
                txtPara.Text = Utility.sDbnull(_KcbChandoanKetluan.Para);
                chkQuaibi.Checked = Utility.Byte2Bool(_KcbChandoanKetluan.QuaiBi);
                txtSongaydieutri.Text = Utility.sDbnull(_KcbChandoanKetluan.SongayDieutri, "0");
                txtSoNgayHen.Text = Utility.sDbnull(_KcbChandoanKetluan.SoNgayhen, "0");
                dtpNgayHen.Value = dtpNgaydangky.Value.AddDays(Utility.Int32Dbnull(_KcbChandoanKetluan.SoNgayhen, 0));
                txtHa.Text = Utility.sDbnull(_KcbChandoanKetluan.Huyetap);
                txtTrieuChungBD._Text = Utility.sDbnull(_KcbChandoanKetluan.TrieuchungBandau);
                txtQuatrinhbenhly._Text = Utility.sDbnull(_KcbChandoanKetluan.QuatrinhBenhly);
                txtTiensubenh._Text = Utility.sDbnull(_KcbChandoanKetluan.TiensuBenh);
                txtCLS._Text = Utility.sDbnull(_KcbChandoanKetluan.TomtatCls);
                txtMach.Text = Utility.sDbnull(_KcbChandoanKetluan.Mach);
                txtNhipTim.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptim);
                txtNhipTho.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptho);
                txtNhietDo.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhietdo);
                txtCannang.Text = Utility.sDbnull(_KcbChandoanKetluan.Cannang);
                txtChieucao.Text = Utility.sDbnull(_KcbChandoanKetluan.Chieucao);
                txtibm.Text = Utility.sDbnull(_KcbChandoanKetluan.ChisoIbm);
                txtthiluc_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.ThilucMp);
                txtthiluc_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.ThilucMt);
                txtnhanap_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.NhanapMp);
                txtnhanap_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.NhanapMt);
                txtNhanxet._Text = Utility.sDbnull(_KcbChandoanKetluan.NhanXet);
                txtCheDoAn._Text = Utility.sDbnull(_KcbChandoanKetluan.ChedoDinhduong, "");
                if (!string.IsNullOrEmpty(Utility.sDbnull(_KcbChandoanKetluan.Nhommau)) &&
                    Utility.sDbnull(_KcbChandoanKetluan.Nhommau) != "-1")
                    txtNhommau._Text = Utility.sDbnull(_KcbChandoanKetluan.Nhommau);

                txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);
                txtChanDoanKemTheo._Text = Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo);


            }
        }
        private string GetTenBenh(string maBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh =
                globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", maBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return TenBenh;
        }
        
        private int i = 0;
        private void HienthithongtinBn()
        {
            try
            {
                pnlAttachedFiles.Controls.Clear();
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                NapthongtinCongkham();
                Napthongtinnguoibenh();
                NapthongtinHoibenhvaChandoan();
                if (tabDiagInfo.SelectedIndex == 1)
                    NapthongtinChidinhCLS();
                else if (tabDiagInfo.SelectedIndex == 2)
                    NapthongtinDonthuoc_Vtth();
                else if (tabDiagInfo.SelectedIndex == 4)
                    NapthongtinDonthuoc_Vtth();
                else if (tabDiagInfo.SelectedIndex == 4)
                     NapthongtinTamung(); 
                else if (tabDiagInfo.SelectedIndex == 5)
                    NapthongtinBuonggiuong();
                txtMach.Focus();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
            }
            finally
            {
                dtpNgayHen.MinDate = dtpNgaydangky.Value;

            }
        }



        /// <summary>
        /// hàm thực hiện việc dùng phím tắt in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_lichsukcb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                Utility.Showhelps(this.GetType().Assembly.ManifestModule.Name, this.Name);
            if (e.KeyCode == Keys.Enter)
            {
                if ((ActiveControl != null && ActiveControl.Name == grdList.Name) ||
                    (tabPageChanDoan.ActiveControl != null && tabPageChanDoan.ActiveControl.Name == autoMabenhphu.Name))
                    return;
                else if ((tabPageChanDoan.ActiveControl != null && tabPageChanDoan.ActiveControl.Name == AutoMabenhchinh.Name))
                {
                    autoMabenhphu.Focus();
                    autoMabenhphu.SelectAll();
                }
                else
                    SendKeys.Send("{TAB}");
            }


            if (e.Control & e.KeyCode == Keys.F) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();

            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl)
                if (ActiveControl != null) Utility.ShowMsg(ActiveControl.Name);
            
            if (e.Control && e.KeyCode == Keys.D1)//(e.Alt && e.KeyCode == Keys.F1)
            {
                tabDiagInfo.SelectedIndex = 0;
                txtMach.Focus();
                return;
            }

            if (e.Control && e.KeyCode == Keys.D3)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 2;
                return;
            }

            if (e.Control && e.KeyCode == Keys.D4)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 3;
                return;
            }
            if (e.Control && e.KeyCode == Keys.D5)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 4;
                return;
            }
            if (e.Control && e.KeyCode == Keys.D6)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 5;
                return;
            }
            if (e.Control && e.KeyCode == Keys.D7)//(e.Alt && e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedIndex = 6;
                return;
            }


        }

        bool CKEditorInput = true;//true=CKeditor;false=Dynamic
        GridEXRow RowCLS = null;
        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            RowCLS =Utility.findthelastChild(grdAssignDetail.CurrentRow);
            ShowResult();
            pnlAttachedFiles.Height = Utility.isValidGrid(grdAssignDetail) ? 60 : 0;
        }
        void LoadHTML()
        {
            string noidung = txtNoiDung.Text;
            webBrowser1.Document.InvokeScript("setValue", new[] { noidung });
        }
        DmucVungkhaosat vks = null;
        void ShowEditor(int id_VungKS)
        {
            pnlCKEditor.BringToFront();

            vks = DmucVungkhaosat.FetchByID(id_VungKS);
            txtNoiDung.Text = vks != null ? vks.MotaHtml : "";
            richtxtKetluan.Text = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "ket_luan"));
            txtDenghi.Text = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "de_nghi"));
            string html = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "mo_ta_HTML"), "");
            if (html != "")
                txtNoiDung.Text = html;
            timer1.Start();
            LoadHTML();
        }
        DataTable dtKQXN = null;
        private void ShowResult()
        {
            try
            {
                bool VisibleKQXN = Utility.Laygiatrithamsohethong("THAMKHAM_NHAPKQ_XN", "0", true) == "1";
                dtKQXN = null;
                //uiTabKqCls.Width = 0;
                mnuNhapKQCDHA.Visible = false;
                mnuNhapKQXN.Visible = false;
                LoadAttachedFiles();
                CKEditorInput = Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhclsChitiet.Columns.ResultType) == "1";
                Int16 trangthaiChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 coChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);

                int idChitietdichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int idDichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);


                int idChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                string ketluanHa =
                  Utility.sDbnull(
                      Utility.GetValueFromGridColumn(grdAssignDetail, "ketluan_ha"), "");
                string maloaiDichvuCls =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                string maBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham),
                                    "XN");
                if (Utility.Coquyen("quyen_nhap_kqxn"))
                    grdKetqua.RootTable.Columns["Ket_qua"].EditType = EditType.TextBox;
                else
                    grdKetqua.RootTable.Columns["Ket_qua"].EditType = EditType.NoEdit;

                if (trangthaiChitiet <= 2)
                //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    if (maloaiDichvuCls != "XN")
                    {
                        pnlXN.SendToBack();
                        splitContainer3.Panel2Collapsed = true;
                        mnuNhapKQCDHA.Visible = true;
                    }
                    else
                    {
                        pnlXN.BringToFront();
                        splitContainer3.Panel2Collapsed = true;
                        ShowKQXN();
                        //mnuNhapKQXN.Visible = true; 
                    }

                    Application.DoEvents();
                }
                else//Có kết quả CLS
                {
                    if (maloaiDichvuCls == "XN")
                        pnlXN.BringToFront();
                    else
                        pnlXN.SendToBack();
                    splitContainer3.Panel2Collapsed = false;
                    //Utility.ShowColumns(grdKetqua, coChitiet == 1 ? lstKQCochitietColumns : lstKQKhongchitietColumns);
                    //Lấy dữ liệu CLS
                    if (maloaiDichvuCls == "XN")
                    {
                        //mnuNhapKQXN.Visible = true;

                        ShowKQXN();
                        //dtKQXN =
                        //    SPs.ClsTimkiemketquaXNChitiet(objLuotkham.MaLuotkham, maChidinh, maBenhpham, idChidinh,
                        //                                  coChitiet, idDichvu, idChitietchidinh).GetDataSet().Tables[0];
                        //Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dtKQXN, true, true, "1=1",
                        //                                         "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

                        //Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
                    }
                    else //XQ,SA,DT,NS
                    {
                        mnuNhapKQCDHA.Visible = true;
                        if (CKEditorInput)
                        {
                            pnlCKEditor.BringToFront();
                            ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_VungKS"), 0));
                        }
                        else
                        {
                            pnlCKEditor.SendToBack();

                        }
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }


        string MaBenhpham = "";
        string MaChidinh = "";
        int IdChitietdichvu = -1;
        int currRowIdx = -1;
        int id_chidinh = -1;
        int id_dichvu = -1;
        int co_chitiet = -1;
        void ShowKQXN()
        {
            if (!Utility.isValidGrid(grdAssignDetail)) return;
            int tempRowIdx = grdAssignDetail.CurrentRow.RowIndex;
            if (currRowIdx == -1 || currRowIdx != tempRowIdx)
            {
                currRowIdx = tempRowIdx;
                id_dichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);
                IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                co_chitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);
                id_chidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                MaChidinh = Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh);
                MaBenhpham = Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham);
                HienthiNhapketqua(id_dichvu, co_chitiet);
            }
        }
        void HienthiNhapketqua(int id_dichvu, int co_chitiet)
        {
            try
            {
                // DataTable dt = SPs.ClsTimkiemthongsoXNNhapketqua(ma_luotkham, MaChidinh, MaBenhpham, id_chidinh, co_chitiet, id_dichvu, IdChitietdichvu).GetDataSet().Tables[0];
                DataTable dt = SPs.ClsLayKetquaXn(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, MaChidinh, id_chidinh, 0, objBenhnhan.IdGioitinh).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet,stt_in");

                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
            }
            catch (Exception ex)
            {


            }
        }



        private void frm_lichsukcb_FormClosed(object sender, FormClosedEventArgs e)
        {
        }


        void LoadAttachedFiles()
        {
            try
            {
                Util.ReleaseControlMemory(pnlAttachedFiles, "3");
                pnlAttachedFiles.Controls.Clear();
                int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                DataTable dtAttachedFiles = new Select().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.IdChidinh).IsEqualTo(idChidinh).ExecuteDataSet().Tables[0];
                foreach (DataRow dr in dtAttachedFiles.Rows)
                {
                    Label _file = new Label();
                    _file.ContextMenuStrip = ctxDelFile;
                    _file.AutoSize = true;
                    _file.Font = lblSample.Font;
                    _file.ForeColor = lblSample.ForeColor;
                    _file.Text = dr[TblFiledinhkem.Columns.Id].ToString() + "-" + dr[TblFiledinhkem.Columns.FileName].ToString();
                    _file.Tag = dr[TblFiledinhkem.Columns.FileData];
                    _file.Click += _file_Click;
                    pnlAttachedFiles.Controls.Add(_file);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());

            }

        }

        void _file_Click(object sender, EventArgs e)
        {
            Label obj = sender as Label;
            byte[] fileData = obj.Tag as byte[];
            //Save to temp folder
            string fileName = Application.StartupPath + @"\tempdpf\" + obj.Text;
            Utility.CreateFolder(fileName);
            File.WriteAllBytes(fileName, fileData);
            System.Diagnostics.Process.Start(fileName);
        }



        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();

        private void tabDiagInfo_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            try
            {
                if (tabDiagInfo.SelectedIndex == 1)
                    NapthongtinChidinhCLS();
                else if (tabDiagInfo.SelectedIndex == 2)
                    NapthongtinDonthuoc_Vtth();
                else if (tabDiagInfo.SelectedIndex == 3)
                    NapthongtinDonthuoc_Vtth();
                else if (tabDiagInfo.SelectedIndex == 4)
                    NapthongtinTamung();
                else if (tabDiagInfo.SelectedIndex == 5)
                    NapthongtinBuonggiuong();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void lnkExpand_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            grdList.Height = grdList.Height == 0 ? 300 : 0;
        }

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtFromDate.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtTenBN.Clear();
            txtMaluotkham.Focus();
        }

        private void cdViewPDF_Click(object sender, EventArgs e)
        {
            if (RowCLS == null || objLuotkham == null ) return;
            frm_PdfViewer _PdfViewer = new frm_PdfViewer(0);
            _PdfViewer.ma_luotkham = objLuotkham.MaLuotkham;
            _PdfViewer.ma_chidinh = Utility.sDbnull(RowCLS.Cells[KcbChidinhcl.Columns.MaChidinh].Value);
            _PdfViewer.ShowDialog();
        }

        private void chkDisplayType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Lấy tất cả lịch sử buồng giường
                DataTable dt_buonggiuong =
                    new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(chkDisplayType.Checked ? "" : objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "-1", -1);
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, dt_buonggiuong, false, true, "1=1", NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
                grdBuongGiuong.MoveFirst();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }


    }
}