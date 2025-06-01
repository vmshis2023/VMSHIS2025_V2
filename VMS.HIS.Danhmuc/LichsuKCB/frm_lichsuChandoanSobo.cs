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
    public partial class frm_lichsuChandoanSobo : Form
    {
        private readonly KCB_THAMKHAM _kcbThamkham = new KCB_THAMKHAM();
        private readonly DataTable _dtIcdPhu = new DataTable();
        public delegate void OnAccept(Int64 id_congkham);
        public event OnAccept _OnAccept;
        private readonly List<string> _lstResultColumns = new List<string>
        {
            "ten_chitietdichvu",
            "ketqua_cls",
            "binhthuong_nam",
            "binhthuong_nu"
        };
        private bool _buttonClick;
        private List<string> _lstVisibleColumns = new List<string>();
        public KcbDanhsachBenhnhan objBenhnhan = null;
        public KcbLuotkham objLuotkham = null;
        public bool AutoLoad = false;
        public bool Anluoidanhsachbenhnhan = false;
        private KcbDangkyKcb objKcbdangky;
        long id_congkham = -1;
        string SplitterPath = "";
        public frm_lichsuChandoanSobo()
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            if (PropertyLib._HinhAnhProperties == null) PropertyLib._HinhAnhProperties = PropertyLib.GetHinhAnhProperties();
            KeyPreview = true;

            dtpNgaydangky.Value = dtpNgaykham.Value = globalVariables.SysDate;
            InitEvents();

        }
        bool AllowSelectionChanged = false;
        private void InitEvents()
        {

            Load += frm_lichsuChandoanSobo_Load;
            FormClosing+=frm_lichsuChandoanSobo_FormClosing;
            Shown += frm_lichsuChandoanSobo_Shown;
            KeyDown += frm_lichsuChandoanSobo_KeyDown;
            grdCongkham.SelectionChanged += grdCongkham_SelectionChanged;
            optNoitru.CheckedChanged += optAll_CheckedChanged;
            optNgoaitru.CheckedChanged += optAll_CheckedChanged;
            txtBMI.TextChanged += txtibm_TextChanged;
            txtSPO2.TextChanged += txtSPO2_TextChanged;
            txtChieucao.Leave += txtChieucao_Leave;
            txtCannang.Leave += txtCannang_Leave;
        }


        void CalculateIBM()
        {
            if (Utility.DecimaltoDbnull(txtChieucao.Text, 0) > 0 && Utility.DecimaltoDbnull(txtChieucao.Text, 0) > 0)
            {
                Decimal IBM = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCannang.Text), 0) / ((Utility.DecimaltoDbnull(txtChieucao.Text, 0) / 100) *
                                   (Utility.DecimaltoDbnull(txtChieucao.Text, 0) / 100));
                txtBMI.Text = String.Format("{0:0.##}", IBM);
            }
        }

        private void txtChieucao_Leave(object sender, EventArgs e)
        {
            CalculateIBM();
        }

        private void txtCannang_Leave(object sender, EventArgs e)
        {
            CalculateIBM();
        }
        void txtSPO2_TextChanged(object sender, EventArgs e)
        {
            Utility.CanhbaoSPO2(dtSPO2, txtSPO2.Text, lblSPO2);
        }

        void txtibm_TextChanged(object sender, EventArgs e)
        {
            Utility.CanhbaoBMI(dtBMI, txtBMI.Text, lblMotaBMI);
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
            }
            catch (Exception ex)
            {


            }
        }
       


        void grdCongkham_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblkhamsobo, "", false);
                ClearControl();
                if (!AllowSelectionChanged) return;
                if (!Utility.isValidGrid(grdCongkham))
                {
                    return;
                }
                id_congkham = Utility.Int64Dbnull(grdCongkham.GetValue(KcbDangkyKcb.Columns.IdKham), -1);
                objKcbdangky = KcbDangkyKcb.FetchByID(id_congkham);
                HienthithongtinBn();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        string ma_luotkham_cu = "";

        void LoadCongkham()
        {
            try
            {
                if (objBenhnhan == null || objLuotkham == null)
                {
                    ClearControl();
                }
                DataTable dtLsuCongkham = _kcbThamkham.KcbLichsuKcbTimkiemphongkham(objLuotkham.IdBenhnhan, "", 1);
                Utility.SetDataSourceForDataGridEx_Basic(grdCongkham, dtLsuCongkham, false, true, "1=1", "ngay_dangky desc,ngay_tiepdon desc");
                grbLsuCongkham.Text = string.Format("Lịch sử khám ({0})", dtLsuCongkham.Rows.Count);
                grdCongkham.AutoSizeColumns();
                grdCongkham.MoveFirst();
                AllowSelectionChanged = true;
                grdCongkham_SelectionChanged(grdCongkham, new EventArgs());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
        void frm_lichsuChandoanSobo_Shown(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            Try2Splitter();
            Utility.DefaultNow(this);
        }
        DataTable dtBMI = new DataTable();
        DataTable dtSPO2 = new DataTable();
        private void frm_lichsuChandoanSobo_Load(object sender, EventArgs e)
        {
            DataTable dt_tempt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "BMI", "SPO2" }, true);
            dtBMI = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dt_tempt, "BMI");
            dtSPO2 = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dt_tempt, "SPO2");
            if (_dtIcdPhu != null) _dtIcdPhu.Clear();

            if (_dtIcdPhu != null && !_dtIcdPhu.Columns.Contains(DmucBenh.Columns.MaBenh))
            {
                _dtIcdPhu.Columns.Add(DmucBenh.Columns.MaBenh, typeof(string));
            }
            if (_dtIcdPhu != null && !_dtIcdPhu.Columns.Contains(DmucBenh.Columns.TenBenh))
            {
                _dtIcdPhu.Columns.Add(DmucBenh.Columns.TenBenh, typeof(string));
            }
            grd_ICD.DataSource = _dtIcdPhu;
            Get_DanhmucChung();
            LoadCongkham();

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
                }
            }
            catch (Exception)
            {

            }
        }


        void frm_lichsuChandoanSobo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString(),splitContainer2.SplitterDistance.ToString()});
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

      

        private void Get_DanhmucChung()
        {
            InitICDPhu();
            txtTrieuChungBD.Init();
            txtChanDoan.Init();
            autoHuongdieutri.Init();
            autoKet_Luan.Init();
            txtNhommau.Init();
            txtNhanxet.Init();
            txtChanDoanKemTheo.Init();
            txtCheDoAn.Init();
            AutoMabenhchinh.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
            autoMabenhphu.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });


        }


        DataTable dt_ICD_PHU = new DataTable();
        private void ClearControl()
        {
            try
            {
                //  objBenhnhan = null;
                id_congkham = -1;
                objKcbdangky = null;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
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
        KcbChandoanKetluan _KcbChandoanKetluan = null;
       void NapthongtinHoibenhvaChandoan()
        {
            if (objKcbdangky == null) return;
            _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
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
                        newDr[KcbDangkyKcb.Columns.IdKham] = _KcbChandoanKetluan != null ? _KcbChandoanKetluan.IdKham : -1;
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
                txtBMI.Text = Utility.sDbnull(_KcbChandoanKetluan.ChisoIbm);
                txtthiluc_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.ThilucMp);
                txtthiluc_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.ThilucMt);
                txtnhanap_mp.Text = Utility.sDbnull(_KcbChandoanKetluan.NhanapMp);
                txtnhanap_mt.Text = Utility.sDbnull(_KcbChandoanKetluan.NhanapMt);
                txtNhanxet._Text = Utility.sDbnull(_KcbChandoanKetluan.NhanXet);
                txtSPO2.Text = Utility.sDbnull(_KcbChandoanKetluan.SPO2);
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
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                NapthongtinCongkham();
                Napthongtinnguoibenh();
                NapthongtinHoibenhvaChandoan();
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
        private void frm_lichsuChandoanSobo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                Utility.Showhelps(this.GetType().Assembly.ManifestModule.Name, this.Name);
            else if (e.KeyCode == Keys.Enter)
            {

                SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.S)
                cmdLuuChandoan_sobo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            else if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl)
                if (ActiveControl != null) Utility.ShowMsg(ActiveControl.Name);


        }

        private void frm_lichsuChandoanSobo_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            if (_OnAccept != null)
                _OnAccept(id_congkham);
            this.Close();
        }

        private void mnuUpdateDiagnostic_Click(object sender, EventArgs e)
        {
            UpdateThongtinChanDoan();
        }
        void UpdateThongtinChanDoan()
        {
            try
            {
                if (grdCongkham.GetCheckedRows().Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 ca khám cần cập nhật thông tin chẩn đoán");
                    return;
                }
                string _des = string.Join("\n", (from p in grdCongkham.GetCheckedRows()
                                                 select "Ngày khám: " + Utility.sDbnull(p.Cells["sNgay_dangky"].Value)).ToArray<string>());
                string ask = string.Format("Bạn có chắc chắn muốn lấy thông tin chẩn đoán từ ca khám ngày {0} để thay thế cho chẩn đoán của các ca khám dưới đây hay không?\n{1}", Utility.sDbnull(grdCongkham.GetValue("sNgay_dangky")), _des);
                List<long> lstId = (from p in grdCongkham.GetCheckedRows()
                                    select Utility.Int64Dbnull(p.Cells["id_kham"].Value)).ToList<long>();
                string id_dich= string.Join(",", (from p in grdCongkham.GetCheckedRows()
                                               select  Utility.sDbnull(p.Cells["id_kham"].Value)).ToArray<string>());
                long id_nguon = Utility.Int64Dbnull(grdCongkham.GetValue("id_kham"));
                if (lstId.Contains(id_nguon))
                {
                    Utility.ShowMsg("Dữ liệu nguồn không được phép chọn. Vui lòng bỏ chọn trước khi thực hiện");
                    return;
                }    
                if (Utility.AcceptQuestion(ask, "Xác nhận cập nhật thông tin chẩn đoán", true))
                {
                  int num=  SPs.KcbThamkhamCapnhatthongtinchandoan(objLuotkham.IdBenhnhan, id_nguon, id_dich).Execute();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin chẩn đoán cho bệnh nhân có mã lần khám {0}, ID bệnh nhân: {1}, tên= {2}. id_kham nguồn ={3}, id_khám đích ={4}, số lượng bản ghi bị cập nhật ={5}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, id_nguon,id_dich, num), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg("Cập nhật thông tin chẩn đoán cho các ca khám đang chọn thành công. Nhấn OK để kết thúc");
                    grdCongkham.UnCheckAllRecords();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdUpdateDiagnostic_Click(object sender, EventArgs e)
        {
            UpdateThongtinChanDoan();
        }

        private void cmdLuuChandoan_sobo_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblkhamsobo, "", false);
                _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                        .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objKcbdangky.IdKham)
                                        .ExecuteSingle<KcbChandoanKetluan>();
                if (_KcbChandoanKetluan == null)
                    _KcbChandoanKetluan = new KcbChandoanKetluan();
                if (_KcbChandoanKetluan.IdChandoan <= 0)
                {
                    _KcbChandoanKetluan.IsNew = true;
                    _KcbChandoanKetluan.IdChandoan = -1;
                    _KcbChandoanKetluan.NgayTao = globalVariables.SysDate;
                    _KcbChandoanKetluan.NguoiTao = globalVariables.UserName;
                }
                else
                {
                    _KcbChandoanKetluan.IsNew = false;
                    _KcbChandoanKetluan.MarkOld();
                    _KcbChandoanKetluan.NguoiSua = globalVariables.UserName;
                    _KcbChandoanKetluan.NgaySua = globalVariables.SysDate;
                    _KcbChandoanKetluan.IpMaysua = globalVariables.gv_strIPAddress;
                    //Kiểm tra
                    //if (Utility.Coquyen("quyen_suathongtinhoibenhvachandoan") || objchuandoan.NguoiTao ==globalVariables.UserName)
                    //{
                    //}
                    //else
                    //{
                    //    return null;
                    //}
                }

                _KcbChandoanKetluan.IdKham = objKcbdangky.IdKham;
                _KcbChandoanKetluan.MaLuotkham = objLuotkham.MaLuotkham;
                _KcbChandoanKetluan.IdBenhnhan = objLuotkham.IdBenhnhan;
                _KcbChandoanKetluan.NhanXet = Utility.sDbnull(txtNhanxet.Text, "");
                _KcbChandoanKetluan.Para = Utility.sDbnull(txtPara.Text);
                _KcbChandoanKetluan.QuaiBi = Utility.Bool2byte(chkQuaibi.Checked);
                _KcbChandoanKetluan.Nhommau = txtNhommau.Text;
                _KcbChandoanKetluan.SPO2 = txtSPO2.Text;
                _KcbChandoanKetluan.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
                _KcbChandoanKetluan.TrieuchungBandau = Utility.sDbnull(txtTrieuChungBD.Text);
                _KcbChandoanKetluan.QuatrinhBenhly = Utility.sDbnull(txtQuatrinhbenhly.Text);
                _KcbChandoanKetluan.TiensuBenh = Utility.sDbnull(txtTiensubenh.Text);
                _KcbChandoanKetluan.TomtatCls = Utility.sDbnull(txtCLS.Text);
                _KcbChandoanKetluan.Huyetap = txtHa.Text;
                _KcbChandoanKetluan.Mach = txtMach.Text;
                _KcbChandoanKetluan.KieuChandoan = 0;
                _KcbChandoanKetluan.Nhiptim = Utility.sDbnull(txtNhipTim.Text);
                _KcbChandoanKetluan.Nhiptho = Utility.sDbnull(txtNhipTho.Text);
                _KcbChandoanKetluan.Chieucao = Utility.sDbnull(txtChieucao.Text);
                _KcbChandoanKetluan.Cannang = Utility.sDbnull(txtCannang.Text);
                _KcbChandoanKetluan.ChisoIbm = Utility.DecimaltoDbnull(txtBMI.Text, 0);
                _KcbChandoanKetluan.ThilucMp = Utility.sDbnull(txtthiluc_mp.Text, "");
                _KcbChandoanKetluan.ThilucMt = Utility.sDbnull(txtthiluc_mt.Text, "");
                _KcbChandoanKetluan.NhanapMp = Utility.sDbnull(txtnhanap_mp.Text, "");
                _KcbChandoanKetluan.NhanapMt = Utility.sDbnull(txtnhanap_mt.Text, "");
                _KcbChandoanKetluan.ChedoDinhduong = Utility.sDbnull(txtCheDoAn.Text, "");
                _KcbChandoanKetluan.XuTri = autoXutri.Text;
                _KcbChandoanKetluan.MaChandoan = Utility.sDbnull(txtChanDoan.myCode);
                _KcbChandoanKetluan.Chandoan = Utility.sDbnull(txtChanDoan.Text);
                _KcbChandoanKetluan.LoiDan = autoLoidan.Text;
                _KcbChandoanKetluan.Save();
               //int num= new Update(KcbChandoanKetluan.Schema)
               //     .Set(KcbChandoanKetluan.Columns.NhanXet).EqualTo(_KcbChandoanKetluan.NhanXet)
               //     .Set(KcbChandoanKetluan.Columns.Para).EqualTo(_KcbChandoanKetluan.Para)
               //     .Set(KcbChandoanKetluan.Columns.QuaiBi).EqualTo(_KcbChandoanKetluan.QuaiBi)
               //     .Set(KcbChandoanKetluan.Columns.Nhommau).EqualTo(_KcbChandoanKetluan.Nhommau)
               //     .Set(KcbChandoanKetluan.Columns.SPO2).EqualTo(_KcbChandoanKetluan.SPO2)
               //     .Set(KcbChandoanKetluan.Columns.Nhietdo).EqualTo(_KcbChandoanKetluan.Nhietdo)
               //     .Set(KcbChandoanKetluan.Columns.QuatrinhBenhly).EqualTo(_KcbChandoanKetluan.QuatrinhBenhly)
               //     .Set(KcbChandoanKetluan.Columns.TiensuBenh).EqualTo(_KcbChandoanKetluan.TiensuBenh)
               //     .Set(KcbChandoanKetluan.Columns.TomtatCls).EqualTo(_KcbChandoanKetluan.TomtatCls)
               //     .Set(KcbChandoanKetluan.Columns.Huyetap).EqualTo(_KcbChandoanKetluan.Huyetap)
               //     .Set(KcbChandoanKetluan.Columns.Mach).EqualTo(_KcbChandoanKetluan.Mach)
               //     .Set(KcbChandoanKetluan.Columns.Nhiptim).EqualTo(_KcbChandoanKetluan.Nhiptim)
               //     .Set(KcbChandoanKetluan.Columns.Nhiptho).EqualTo(_KcbChandoanKetluan.Nhiptho)
               //     .Set(KcbChandoanKetluan.Columns.Chieucao).EqualTo(_KcbChandoanKetluan.Chieucao)
               //     .Set(KcbChandoanKetluan.Columns.Cannang).EqualTo(_KcbChandoanKetluan.Cannang)
               //     .Set(KcbChandoanKetluan.Columns.ChisoIbm).EqualTo(_KcbChandoanKetluan.ChisoIbm)
               //     .Set(KcbChandoanKetluan.Columns.ChedoDinhduong).EqualTo(_KcbChandoanKetluan.ChedoDinhduong)
               //     .Where(KcbChandoanKetluan.Columns.IdChandoan).IsEqualTo(_KcbChandoanKetluan.IdChandoan)
               //     .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(_KcbChandoanKetluan.IdBenhnhan)
               //     .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(_KcbChandoanKetluan.MaLuotkham)
               //     .Execute();
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin chẩn đoán cho bệnh nhân có mã lần khám {0}, ID bệnh nhân: {1}, tên= {2}. id_kham ={3}", objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objBenhnhan.TenBenhnhan, _KcbChandoanKetluan.IdKham), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                Utility.SetMsg(lblkhamsobo, "Bạn đã lưu thông tin khám thành công", false);

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
          
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}