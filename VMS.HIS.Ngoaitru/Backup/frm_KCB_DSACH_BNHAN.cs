using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.Forms.Dungchung;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using System.IO;
using VNS.Libs;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using System.Drawing.Printing;
using VNS.HIS.Classes;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.HinhAnh;
using VNS.HIS.UI.HinhAnh;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.Forms.NGOAITRU;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_KCB_DSACH_BNHAN : BaseForm
    {
        KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private DataTable m_dtDanhsachDichvuKCB = new DataTable();
        private DataTable m_dtPatient = new DataTable();
        private DataTable m_PhongKham = new DataTable();
        private DataTable m_kieuKham;
        private DataTable m_dtChiDinhCLS = new DataTable();
        private bool m_blnHasloaded = false;
        string Args = "ALL";
        string SplitterPath = "";
        public frm_KCB_DSACH_BNHAN(string Args)
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            this.Args = Args;
            this.InitTrace();
            webBrowser1.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor_simple.html");
            timer1.Enabled = true;
            timer1.Interval = 900;
            this.KeyPreview = true;
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = globalVariables.SysDate;
            InitEvents();
            InitFtp();
            CauHinh();
        }
        void InitEvents()
        {
            Shown += frm_KCB_DSACH_BNHAN_Shown;
            this.txtPatientCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientCode_KeyDown);
            this.txtPatient_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_ID_KeyDown);
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);

            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            this.grdRegExam.ColumnButtonClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdRegExam_ColumnButtonClick);
            this.grdRegExam.SelectionChanged += new System.EventHandler(this.grdRegExam_SelectionChanged);


            this.cmdThemMoiBN.Click += new System.EventHandler(this.cmdThemMoiBN_Click);
            this.cmdSuaThongTinBN.Click += new System.EventHandler(this.cmdSuaThongTinBN_Click);
            this.cmdThemLanKham.Click += new System.EventHandler(this.cmdThemLanKham_Click);
            this.cmdXoaBenhNhan.Click += new System.EventHandler(this.cmdXoaBenhNhan_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);

            this.cboKieuKham.ValueChanged += new System.EventHandler(this.cboKieuKham_ValueChanged);
            this.cmdAddDvuKCB.Click += new System.EventHandler(this.cmdAddDvuKCB_Click);
            this.lblPhuThu.TextChanged += new System.EventHandler(this.lblPhuThu_TextChanged);
            this.lblDonGia.TextChanged += new System.EventHandler(this.lblDonGia_TextChanged);
            this.lblDonGia.Click += new System.EventHandler(this.lblDonGia_Click);
            this.cmdThanhToanKham.Click += new System.EventHandler(this.cmdThanhToanKham_Click);
            this.cmdInPhieuKham.Click += new System.EventHandler(this.cmdInPhieuKham_Click);
            this.cmdXoaKham.Click += new System.EventHandler(this.cmdXoaKham_Click);
            grdAssignDetail.MouseDoubleClick += grdAssignDetail_MouseDoubleClick;
            this.grdAssignDetail.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdAssignDetail_CellValueChanged);
            this.grdAssignDetail.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grdAssignDetail_FormattingRow);
            this.grdAssignDetail.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdAssignDetail_ColumnHeaderClick);
            this.grdAssignDetail.SelectionChanged += new System.EventHandler(this.grdAssignDetail_SelectionChanged);
            this.txtTongChiPhi.TextChanged += new System.EventHandler(this.txtTongChiPhi_TextChanged);
            this.cmdXoaChiDinh.Click += new System.EventHandler(this.cmdXoaChiDinh_Click);
            this.cmdSuaChiDinh.Click += new System.EventHandler(this.cmdSuaChiDinh_Click);
            this.cmdThemChiDinh.Click += new System.EventHandler(this.cmdThemChiDinh_Click);

            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);

            this.cmdCauhinh.Click += new System.EventHandler(this.cmdCauhinh_Click);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_KCB_DSACH_BNHAN_FormClosing);
            this.Load += new System.EventHandler(this.frm_KCB_DSACH_BNHAN_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_KCB_DSACH_BNHAN_KeyDown);
            txtPhongkham._OnSelectionChanged += new UCs.AutoCompleteTextbox.OnSelectionChanged(txtPhongkham__OnSelectionChanged);
            txtKieuKham._OnSelectionChanged += new UCs.AutoCompleteTextbox.OnSelectionChanged(txtKieuKham__OnSelectionChanged);
            txtPhongkham._OnEnterMe += txtPhongkham__OnEnterMe;
            txtKieuKham._OnEnterMe += txtKieuKham__OnEnterMe;
            txtExamtypeCode._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtExamtypeCode__OnSelectionChanged);
            txtExamtypeCode._OnEnterMe += txtExamtypeCode__OnEnterMe;
            mnuMaDVu.Click += new EventHandler(mnuMaDVu_Click);

            cboKieuin.SelectedIndexChanged += new EventHandler(cboKieuin_SelectedIndexChanged);
            chkInsaukhiluu.CheckedChanged += new EventHandler(chkInsaukhiluu_CheckedChanged);
            cboPrintPreview.SelectedIndexChanged += new EventHandler(cboPrintPreview_SelectedIndexChanged);
            cboLaserPrinters.SelectedIndexChanged += new EventHandler(cboLaserPrinters_SelectedIndexChanged);
            cboA4.SelectedIndexChanged += new EventHandler(cboA4_SelectedIndexChanged);

            cmdInBienlai.Click += new EventHandler(cmdInlaihoadon_Click);
            cmdInhoadon.Click += new EventHandler(cmdInhoadon_Click);

            cmdPrintAssign.Click += new EventHandler(cmdPrintAssign_Click);
            mnuDelFile.Click+=mnuDelFile_Click;
            lnkMore.Click += lnkMore_Click;
            //mnuNhapKQCDHA.Click += mnuNhapKQCDHA_Click;
            pnlXQ.Width = PropertyLib._KCBProperties.ResultInput ? 400 : 0;
        }

        void frm_KCB_DSACH_BNHAN_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }
        void Try2Splitter()
        {
            try
            {
                int value1 = Utility.Int32Dbnull(Utility.GetFirstValueFromFile(SplitterPath), -1);
                if (value1 > 0)
                    splitContainer1.SplitterDistance = value1;
            }
            catch (Exception)
            {

            }
        }
        void txtExamtypeCode__OnEnterMe()
        {
            cboKieuKham.Text = txtMyNameEdit.Text;
            //cboKieuKham.Value = txtExamtypeCode.MyID;
            txtKieuKham._Text = cboKieuKham.Text;
            txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
        }

        void txtPhongkham__OnEnterMe()
        {
            DataRow[] arrDr = m_dtDanhsachDichvuKCB.Select("id_dichvukcb=" + txtPhongkham.MyID);
            cboKieuKham.SelectedIndex = Utility.GetSelectedIndex(cboKieuKham, txtPhongkham.MyID.ToString());//Text = arrDr.Length <= 0 ? "---Chọn công khám----" : arrDr[0]["ten_dichvukcb"].ToString();
            cboKieuKham_ValueChanged(cboKieuKham, new EventArgs());
        }

        void txtKieuKham__OnEnterMe()
        {
            AutoloadPhongkham_Congkham();
        }
        void AutoloadPhongkham_Congkham()
        {
            m_PhongKham = m_dtDanhsachDichvuKCB.Clone();

            DataRow[] arrdr = m_dtDanhsachDichvuKCB.Select("id_kieukham=" + txtKieuKham.MyID);
            if (arrdr.Length > 0)
                m_PhongKham = arrdr.CopyToDataTable();
            if (!m_PhongKham.Columns.Contains("TEN"))
                m_PhongKham.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(string)), new DataColumn("Ma", typeof(string)), new DataColumn("TEN", typeof(string)) });
            foreach (DataRow dr in m_PhongKham.Rows)
            {
                dr["id"] = dr["id_dichvukcb"].ToString();// string.Format("{0}-{1}", dr["id_phongkham"].ToString(), dr["id_dichvukcb"].ToString());
                dr["ma"] = dr["ma_phongkham"].ToString();// string.Format("{0}-{1}", dr["ma_phongkham"].ToString(), dr["ma_dichvukcb"].ToString());
                dr["ten"] = string.Format("{0}-{1}-Đơn giá:{2}", dr["ten_phongkham"].ToString(), dr["ten_dichvukcb"].ToString(), dr["don_gia"].ToString());
            }
            txtPhongkham.Init(m_PhongKham, new List<string>() { "id", "ma", "ten" });
        }
        void mnuNhapKQCDHA_Click(object sender, EventArgs e)
        {
            BeginExam();
        }

        void lnkMore_Click(object sender, EventArgs e)
        {
            BeginExam();
        }
        void grdAssignDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BeginExam();
        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdSuaThongTinBN.PerformClick();
        }

        void cmdPrintAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string mayin = "";
                int vAssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string vAssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                var nhomcls = new List<string>();
                foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
                {
                    if (Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value) == vAssignId)
                        if (!nhomcls.Contains(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value)))
                        {
                            nhomcls.Add(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value));
                        }
                }
                List<long> lstSelectedPrint = (from p in grdAssignDetail.GetCheckedRows().AsEnumerable()
                                               select Utility.Int64Dbnull(p.Cells["id_chitietchidinh"].Value, 0)).ToList();
                string nhomincls = "ALL";
                if (cboServicePrint.SelectedIndex > 0)
                {
                    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");
                    if (nhomincls == "-1")
                    {
                        nhomincls = "DV";
                    }

                }
                var actionResult = ActionResult.Error;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTACHTOANBO_CLS", "0", false) == "1" &&
                   chkIntach.Checked && cboServicePrint.SelectedIndex <= 0)
                {
                    actionResult = KcbInphieu.InTachToanBoPhieuCls(lstSelectedPrint,(int)objLuotkham.IdBenhnhan,
                                                                     objLuotkham.MaLuotkham, vAssignId,
                                                                     vAssignCode, nhomcls, Utility.sDbnull(cboServicePrint.SelectedValue, "ALL"),
                                                                     cboServicePrint.SelectedIndex, chkIntach.Checked,
                                                                     ref mayin);
                }
                else
                {
                    actionResult = KcbInphieu.InphieuChidinhCls((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                                                  vAssignId,
                                                                  vAssignCode, nhomincls, cboServicePrint.SelectedIndex,
                                                                  chkIntach.Checked,
                                                                  ref mayin);
                }
                //KcbInphieu.InphieuChidinhCls((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, vAssignId, vAssignCode, nhomincls, cboServicePrint.SelectedIndex, chkIntach.Checked, ref mayin);
                if (mayin != "") cboLaserPrinters.Text = mayin;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void cmdInhoadon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return;
                int paymentId = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdThanhtoan].Value);
                InHoadon(paymentId);
            }
            catch
            { }
        }

        void cmdInlaihoadon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return;
                int Payment_Id = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdThanhtoan].Value);
                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, Payment_Id, objLuotkham, 0);
            }
            catch
            { }
        }




        void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {

            PropertyLib._MayInProperties.CoGiayInBienlai = cboA4.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }

        void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.TenMayInPhieuKCB = cboLaserPrinters.Text;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }

        void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.PreviewPhieuKCB = cboPrintPreview.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void chkInsaukhiluu_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.InPhieuKCBsaukhiluu = chkInsaukhiluu.Checked;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboKieuin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.KieuInPhieuKCB = cboKieuin.SelectedIndex == 0 ? KieuIn.Innhiet : KieuIn.InLaser;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void txtExamtypeCode__OnSelectionChanged()
        {
            cboKieuKham.Text = txtMyNameEdit.Text;
            //cboKieuKham.Value = txtExamtypeCode.MyID;
            txtKieuKham._Text = cboKieuKham.Text;
            txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
        }

        void mnuMaDVu_Click(object sender, EventArgs e)
        {
            PropertyLib._KCBProperties.GoMaDvu = mnuMaDVu.Checked;
            pnlKieuPhongkham.Visible = !PropertyLib._KCBProperties.GoMaDvu;
            pnlGoMaDvu.Visible = PropertyLib._KCBProperties.GoMaDvu;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }

        void txtPhongkham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }

        void txtKieuKham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }
        private void AutoLoadKieuKham()
        {
            try
            {
                if (Utility.Int32Dbnull(txtIDKieuKham, -1) == -1 || Utility.Int32Dbnull(txtIDPkham, -1) == -1)
                {
                    cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                }
                DataRow[] arrDr =
                     m_dtDanhsachDichvuKCB.Select(DmucDoituongkcb.Columns.MaDoituongKcb + "='ALL' OR " + DmucDoituongkcb.Columns.MaDoituongKcb + "='" + MA_DTUONG + "' AND " + DmucKieukham.Columns.IdKieukham + "=" +
                                                  txtIDKieuKham.Text.Trim() + " AND " + DmucDichvukcb.Columns.IdPhongkham + "=" + txtIDPkham.Text.Trim());
                if (arrDr.Length <= 0)
                {
                    cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                }
                else
                {
                    cboKieuKham.Text = arrDr[0][DmucDichvukcb.Columns.TenDichvukcb].ToString();
                }
            }
            catch
            {
            }
        }
        private void CauHinh()
        {

            if (PropertyLib._KCBProperties != null)
            {

                cmdThanhToanKham.Enabled = PropertyLib._KCBProperties.Chophepthanhtoan;
                cmdThanhToanKham.Visible = cmdThanhToanKham.Enabled;

                grdRegExam.RootTable.Columns["colThanhtoan"].Visible = PropertyLib._KCBProperties.Chophepthanhtoan && (PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi || PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Cahai);
                grdRegExam.RootTable.Columns["colDelete"].Visible = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi || PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Cahai;
                grdRegExam.RootTable.Columns["colIn"].Visible = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi || PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Cahai;
                pnlnutchucnang.Visible = PropertyLib._KCBProperties.Kieuhienthi != Kieuhienthi.Trenluoi;
                pnlnutchucnang.Height = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi ? 0 : 33;
                tabPageChiDinh.TabVisible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEP_CHIDINH_KHONGQUAPHONGKHAM", "0", false) == "1";
                tabChiDinh.Width = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEP_CHIDINH_KHONGQUAPHONGKHAM", "0", false) == "0" ? 0 : PropertyLib._KCBProperties.Chieurong;
                pnlKieuPhongkham.Visible = !PropertyLib._KCBProperties.GoMaDvu;
                pnlGoMaDvu.Visible = PropertyLib._KCBProperties.GoMaDvu;
                mnuMaDVu.Checked = PropertyLib._KCBProperties.GoMaDvu;
                cboA4.Text = PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5";
                cboPrintPreview.SelectedIndex = PropertyLib._MayInProperties.PreviewPhieuKCB ? 0 : 1;
                cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                cboKieuin.SelectedIndex = PropertyLib._MayInProperties.KieuInPhieuKCB == KieuIn.Innhiet ? 0 : 1;
                chkInsaukhiluu.Checked = PropertyLib._MayInProperties.InPhieuKCBsaukhiluu;

            }
            mnuUpdateMaBenhAn.Visible =
                mnuUpdateMaBenhNhan.Visible = mnuUpdateMalankham.Visible = mnuupdatengaykham.Visible = Utility.Coquyen("tiepdon_update_thongtinbenhnhan");
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
                //if (radNgoaiTru.Checked) Hos_status = 0;
                //if (radNoiTru.Checked) Hos_status = 1;
                Hos_status = Utility.Int16Dbnull(cboTrangThai.SelectedValue, -1);
                m_dtPatient = _KCB_DANGKY.KcbTiepdonTimkiemBenhnhan(theongay ? (chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                    theongay ? (chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                                                     Utility.Int32Dbnull(cboObjectType.SelectedValue, -1), Hos_status,
                                                     Utility.sDbnull(txtPatientName.Text),
                                                     Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                     Utility.sDbnull(txtPatientCode.Text), "", "", globalVariables.MA_KHOA_THIEN, 0, (byte)100, this.Args.Split('-')[0]);
                Utility.SetDataSourceForDataGridEx(grdList, m_dtPatient, true, true, "1=1", KcbDanhsachBenhnhan.Columns.IdBenhnhan + " desc");
                //Utility.SetMsg(lblTongSo, string.Format("&Tổng số bản ghi :{0}", m_dtPatient.Rows.Count), true);
                if (grdList.GetDataRows().Length <= 0)
                    m_dtDangkyPhongkham.Rows.Clear();
                UpdateGroup();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                ModifyCommand();
            }
        }
        void UpdateGroup()
        {
            try
            {
                var counts = m_dtPatient.AsEnumerable().GroupBy(x => x.Field<string>("ma_doituong_kcb"))
                    .Select(g => new { g.Key, Count = g.Count() });
                if (counts.Count() >= 2)
                {
                    if (grdList.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "Nhóm đối tượng KCB: ";
                        grdList.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdList.RootTable.Groups.Clear();
                }
                grdList.UpdateData();
                grdList.Refresh();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin(true);
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
        private bool AllowTextChanged;
        private bool AllowSelectionChanged=false;
        void NapThongtinDichvuKCB()
        {
            bool oldStatus = AllowTextChanged;
            try
            {
                cboKieuKham.DataSource = null;
                //Khởi tạo danh mục Loại khám
                string objecttype_code = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaDoituongKcb));
                m_dtDanhsachDichvuKCB = THU_VIEN_CHUNG.LayDsach_Dvu_KCB(objecttype_code, this.Args.Split('-')[1], -1);
                Get_KIEUKHAM(objecttype_code);
                Get_PHONGKHAM(objecttype_code);
                AutocompleteMaDvu();
               // AutocompletePhongKham();
                AutocompleteKieuKham();
                m_dtDanhsachDichvuKCB.AcceptChanges();
                cboKieuKham.DataSource = m_dtDanhsachDichvuKCB;
                cboKieuKham.DataMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.ValueMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.DisplayMember = DmucDichvukcb.Columns.TenDichvukcb;
                //   cboKieuKham.Visible = globalVariables.UserName == "ADMIN";
                if (m_dtDanhsachDichvuKCB == null || m_dtDanhsachDichvuKCB.Columns.Count <= 0) return;
                AllowTextChanged = true;
                if (m_dtDanhsachDichvuKCB.Rows.Count == 1)
                {
                    cboKieuKham.SelectedIndex = 0;

                }
                AllowTextChanged = oldStatus;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        //private void Get_PHONGKHAM(string MA_DTUONG)
        //{
        //    m_PhongKham = THU_VIEN_CHUNG.Get_PHONGKHAM(MA_DTUONG);
        //}

        //private void Get_KIEUKHAM(string MA_DTUONG)
        //{
        //    m_kieuKham = THU_VIEN_CHUNG.Get_KIEUKHAM(MA_DTUONG, -1);
        //}
        private void Get_PHONGKHAM(string MA_DTUONG)
        {
            List<int> lstIdCongkham = (from p in m_dtDanhsachDichvuKCB.AsEnumerable()
                                       select Utility.Int32Dbnull(p.Field<int>(DmucDichvukcb.Columns.IdDichvukcb))).Distinct().ToList<int>();
            if (lstIdCongkham.Count <= 0) lstIdCongkham.Add(-1);
            m_PhongKham = new Select().From(DmucKieukham.Schema).Where(DmucKieukham.Columns.IdKieukham).In(lstIdCongkham).ExecuteDataSet().Tables[0];
        }

        private void Get_KIEUKHAM(string MA_DTUONG)
        {
            List<Int16> lstIdKieuKham = (from p in m_dtDanhsachDichvuKCB.AsEnumerable()
                                         select Utility.Int16Dbnull(p.Field<Int16>(DmucDichvukcb.Columns.IdKieukham))).Distinct().ToList<Int16>();
            if (lstIdKieuKham.Count <= 0) lstIdKieuKham.Add(-1);
            m_kieuKham = new Select().From(DmucKieukham.Schema).Where(DmucKieukham.Columns.IdKieukham).In(lstIdKieuKham).ExecuteDataSet().Tables[0];

        }
        /// <summary>
        /// hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_DSACH_BNHAN_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
            DataBinding.BindDataCombox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Tất cả", true);
            if (cboServicePrint.Items.Count > 0) cboServicePrint.SelectedIndex = 0;
            AllowTextChanged = true;
            LayDsach_DoituongKCB();
            LayThongtinDVu_KCB();
            cboTrangThai.SelectedIndex = 0;
            TimKiemThongTin(true);
            AutoloadSaveAndPrintConfig();
            ModifyButtonCommandRegExam();
            LoadLaserPrinters();
            m_blnHasloaded = true;
        }
        private void LoadLaserPrinters()
        {
            if (string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInPhieuKCB))
            {
                PropertyLib._MayInProperties.TenMayInPhieuKCB = Utility.GetDefaultPrinter();
                m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInPhieuKCB);
            }
            if (PropertyLib._KCBProperties != null)
            {
                try
                {
                    //khoi tao may in
                    String pkInstalledPrinters;
                    cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                        cboLaserPrinters.Items.Add(pkInstalledPrinters);
                    }
                }
                catch (Exception exception)
                {
                    Utility.ShowMsg("Lỗi:" + exception.Message);
                }
                finally
                {
                    m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInPhieuKCB);
                    cboLaserPrinters.Text = m_strDefaultLazerPrinterName;
                }
            }
            if (File.Exists(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt"))
            {
                chkIntach.Checked =
                    Convert.ToInt16(File.ReadAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt")) ==
                    1
                        ? true
                        : false;
            }
        }
        private void LayThongtinDVu_KCB()
        {
            try
            {
                m_dtDanhsachDichvuKCB = THU_VIEN_CHUNG.LayDsach_Dvu_KCB("ALL", this.Args.Split('-')[1], -1);
                m_kieuKham = THU_VIEN_CHUNG.Get_KIEUKHAM("ALL", -1);
                m_PhongKham = THU_VIEN_CHUNG.Get_PHONGKHAM("ALL");
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }
        private void AutocompleteMaDvu()
        {
            DataRow[] arrDr = null;
            try
            {
                if (m_dtDanhsachDichvuKCB == null) return;
                if (!m_dtDanhsachDichvuKCB.Columns.Contains("ShortCut"))
                    m_dtDanhsachDichvuKCB.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                arrDr = m_dtDanhsachDichvuKCB.Select(DmucDoituongkcb.Columns.MaDoituongKcb + "='ALL' OR " + DmucDoituongkcb.Columns.MaDoituongKcb + "='" + MA_DTUONG + "'");
                if (arrDr.Length <= 0)
                {
                    this.txtExamtypeCode.AutoCompleteList = new List<string>();
                    return;
                }
                txtExamtypeCode.Init(m_dtDanhsachDichvuKCB, new List<string>() { DmucDichvukcb.Columns.IdDichvukcb, DmucDichvukcb.Columns.MaDichvukcb, DmucDichvukcb.Columns.TenDichvukcb });
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }


        private void AutocompletePhongKham()
        {
            try
            {
                if (m_PhongKham == null) return;
                if (!m_PhongKham.Columns.Contains("ShortCut"))
                    m_PhongKham.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                txtPhongkham.Init(m_PhongKham, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            }
            catch
            {
            }
            finally
            {

            }
        }

        private void AutocompleteKieuKham()
        {
            try
            {
                if (m_kieuKham == null) return;
                if (!m_kieuKham.Columns.Contains("ShortCut"))
                    m_kieuKham.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                txtKieuKham.Init(m_kieuKham, new List<string>() { DmucKieukham.Columns.IdKieukham, DmucKieukham.Columns.MaKieukham, DmucKieukham.Columns.TenKieukham });
                txtKieuKham.RaiseEnterEvents();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void LayDsach_DoituongKCB()
        {
            DataBinding.BindDataCombobox(cboObjectType, globalVariables.gv_dtDoituong,
                                       DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "---Chọn đối tượng---", true);
        }

        private void LoadChiDinh()
        {
            AllowSelectionChanged = false;
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            m_dtChiDinhCLS = _KCB_DANGKY.LayChiDinhCLS_KhongKham(MaLuotkham, Patient_ID, 200);
            Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtChiDinhCLS, false, true, "1=1", "");
            UpdateWhanChanged();
            ModifycommandAssignDetail();
            AllowSelectionChanged = true;
        }
        private DataTable m_dtDangkyPhongkham = new DataTable();
        private void BindRegExam()
        {
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            m_dtDangkyPhongkham = _KCB_DANGKY.LayDsachCongkhamDadangki(MaLuotkham, Patient_ID,0);
            Utility.SetDataSourceForDataGridEx(grdRegExam, m_dtDangkyPhongkham, false, true, "", KcbDangkyKcb.Columns.IdKham + " desc");

        }
        private void UpdateWhanChanged()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells["TT"].Value =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) *
                        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value);

                }
            }
            grdList.UpdateData();
            m_dtChiDinhCLS.AcceptChanges();
            UpdateSumOfChiDinh();
        }

        private void UpdateSumOfChiDinh()
        {
            Janus.Windows.GridEX.GridEXColumn gridExColumn = grdAssignDetail.RootTable.Columns["TT"];
            Janus.Windows.GridEX.GridEXColumn gridExColumnPhuThu = grdAssignDetail.RootTable.Columns[KcbChidinhclsChitiet.Columns.PhuThu];
            decimal Thanhtien = Utility.DecimaltoDbnull(grdAssignDetail.GetTotal(gridExColumn, AggregateFunction.Sum));
            decimal phuthu = Utility.DecimaltoDbnull(grdAssignDetail.GetTotal(gridExColumnPhuThu, AggregateFunction.Sum));

            txtTongChiPhi.Text = Utility.sDbnull(Thanhtien);// + phuthu);
        }
        string MA_DTUONG = "DV";
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    if (m_dtDangkyPhongkham != null) m_dtDangkyPhongkham.Clear();
                    objLuotkham = null;
                    objBenhnhan = null;
                    return;
                }
                if (grdList.CurrentRow != null)
                {
                    objLuotkham = CreatePatientExam();
                    if (objLuotkham != null)
                        objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
                    MA_DTUONG = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaDoituongKcb));
                    txtKieuKham._Text = "";
                    txtPhongkham._Text = "";
                    txtIDKieuKham.Text = "-1";
                    txtIDPkham.Text = "-1";
                    cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    NapThongtinDichvuKCB();
                    cboKieuKham_SelectedIndexChanged(cboKieuKham, new EventArgs());
                    BindRegExam();
                    LoadChiDinh();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyButtonCommandRegExam();
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc cáu hình trạng thái của nút
        /// </summary>
        private void ModifyCommand()
        {
            cmdSuaThongTinBN.Enabled =
           cmdXoaBenhNhan.Enabled =
           cmdThemLanKham.Enabled =mnuFingerRegister.Enabled=cmdRegisterFinger.Enabled= Utility.isValidGrid(grdList);
            plnAddDvuKCB.Enabled = Utility.isValidGrid(grdList);
        }
        /// <summary>
        /// hàm thực hiện viecj nhận formating trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {

            if (e.Row.RowType == RowType.TotalRow)
            {
                e.Row.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value = "Tổng cộng :";
            }
        }
        private void frm_KCB_DSACH_BNHAN_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2File(SplitterPath, splitContainer1.SplitterDistance.ToString());
        }

        private void txtTongChiPhi_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtTongChiPhi);
        }

        private void cboKieuKham_SelectedIndexChanged(object sender, EventArgs e)
        {

            //LG
        }
        private KcbDangkyKcb CreateNewRegExam()
        {
            bool b_HasKham = false;
            var query = from phong in m_dtDangkyPhongkham.AsEnumerable().Cast<DataRow>()
                        where
                            Utility.Int32Dbnull(phong[KcbDangkyKcb.Columns.IdDichvuKcb], -100) ==
                            Utility.Int32Dbnull(cboKieuKham.Value, -1)
                        select phong;
            if (query.Count() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã đăng ký dịch vụ khám này. Đề nghị bạn xem lại");
                b_HasKham = true;
            }
            else
            {
                b_HasKham = false;
            }

            if (!b_HasKham)
            {
                KcbDangkyKcb objRegExam = new KcbDangkyKcb();
                DmucDichvukcb objDichvuKCB = DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                DmucKhoaphong objdepartment = new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).IsEqualTo(objDichvuKCB.IdKhoaphong).ExecuteSingle<DmucKhoaphong>();
                DmucDoituongkcb objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(MA_DTUONG).ExecuteSingle<DmucDoituongkcb>();
                if (objDichvuKCB != null)
                {
                    string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                    int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                    int dungtuyen = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.DungTuyen), 0);
                    objRegExam.IdDichvuKcb = Utility.Int16Dbnull(objDichvuKCB.IdDichvukcb, -1);
                    objRegExam.IdKieukham = objDichvuKCB.IdKieukham;
                    objRegExam.NhomBaocao = objDichvuKCB.NhomBaocao;
                    objRegExam.LaPhidichvukemtheo = 0;
                    objRegExam.SttKham = -1;
                    objRegExam.IdCha = -1;
                    objRegExam.TyleTt = 100;
                    objRegExam.IdKhoakcb = objDichvuKCB.IdKhoaphong;
                    if (objLuotkham != null)
                    {
                        objRegExam.MadoituongGia = objLuotkham.MadoituongGia;
                    }

                    objRegExam.DonGia = Utility.DecimaltoDbnull(objDichvuKCB.DonGia, 0);
                    objRegExam.NguoiTao = globalVariables.UserName;
                    if (objdepartment != null)
                    {
                       
                        objRegExam.MaPhongStt = objdepartment.MaPhongStt;

                    }
                    if (objDoituongKCB != null)
                    {
                        objRegExam.IdLoaidoituongkcb = objDoituongKCB.IdLoaidoituongKcb;
                        objRegExam.MaDoituongkcb = objDoituongKCB.MaDoituongKcb;
                        objRegExam.IdDoituongkcb = objDoituongKCB.IdDoituongKcb;
                    }
                    if (Utility.Int16Dbnull(objDichvuKCB.IdPhongkham, -1) > -1)
                        objRegExam.IdPhongkham = Utility.Int16Dbnull(objDichvuKCB.IdPhongkham, -1);
                    else
                        objRegExam.IdPhongkham = Utility.Int16Dbnull(txtIDPkham.Text, -1);

                    if (Utility.Int32Dbnull(objDichvuKCB.IdBacsy) > 0)
                        objRegExam.IdBacsikham = Utility.Int16Dbnull(objDichvuKCB.IdBacsy);
                    //else
                    //{
                    //    objRegExam.IdBacsikham = globalVariables.gv_intIDNhanvien;
                    //}
                    objRegExam.PhuThu = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? (dungtuyen == 1 ? Utility.DecimaltoDbnull(objDichvuKCB.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objDichvuKCB.PhuthuTraituyen)) : 0m;
                    objRegExam.NgayDangky = globalVariables.SysDate;
                    objRegExam.IdBenhnhan = Patient_ID;
                    objRegExam.TrangthaiThanhtoan = 0;
                    objRegExam.TrangthaiHuy = 0;
                    objRegExam.Noitru = 0;
                    objRegExam.TrangthaiIn = 0;
                    objRegExam.TuTuc = Utility.ByteDbnull(objDichvuKCB.TuTuc, 0);
                    objRegExam.MaKhoaThuchien = objdepartment.MaKhoaphong;// globalVariables.MA_KHOA_THIEN;
                    objRegExam.TenDichvuKcb = cboKieuKham.Text;
                    objRegExam.NgayTiepdon = globalVariables.SysDate;
                    objRegExam.IpMaytao = globalVariables.gv_strIPAddress;
                    objRegExam.TenMaytao = globalVariables.gv_strComputerName;
                    objRegExam.MaLuotkham = MaLuotkham;
                    if (THU_VIEN_CHUNG.IsNgoaiGio())
                    {
                        objRegExam.KhamNgoaigio = 1;
                        objRegExam.DonGia = Utility.DecimaltoDbnull(objDichvuKCB.DongiaNgoaigio, 0);
                        objRegExam.PhuThu = Utility.Byte2Bool(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(objDichvuKCB.PhuthuNgoaigio, 0) : Utility.DecimaltoDbnull(objDichvuKCB.PhuthuDungtuyen);
                    }
                    else
                    {
                        objRegExam.KhamNgoaigio = 0;
                    }
                }
                else
                {
                    objRegExam = null;
                }
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_TINHGIAKHAM_THONGTU37", "0", false) == "1")
                {
                    THU_VIEN_CHUNG.TinhlaiGiaChiphiKcb(m_dtDangkyPhongkham, ref objRegExam);
                }
                return objRegExam;
            }
            return null;
        }
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        void ProcessData()
        {
            long v_RegId = -1;
            if (objLuotkham == null) objLuotkham = CreatePatientExam();
            if (objLuotkham != null)
            {
                KcbDangkyKcb objRegExam = CreateNewRegExam();
                if (objRegExam != null)
                {
                    objRegExam.MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                    objRegExam.IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));

                    ActionResult actionResult = _KCB_DANGKY.InsertRegExam(objRegExam, objLuotkham, ref v_RegId, Utility.Int32Dbnull(cboKieuKham.Value));
                    if (actionResult == ActionResult.Success)
                    {
                        if (m_dtDangkyPhongkham != null)
                        {
                            BindRegExam();
                            Utility.GonewRowJanus(grdRegExam, KcbDangkyKcb.Columns.IdKham, v_RegId.ToString());
                            cmdInPhieuKham.Focus();
                        }
                        //Reset cac thong tin chi dinh phong kham
                        cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                        cboKieuKham.SelectedIndex = -1;
                        txtKieuKham.Clear();
                        txtPhongkham.Clear();
                        txtIDPkham.Text = "-1";
                        txtIDKieuKham.Text = "-1";
                        txtExamtypeCode.Text = "-1";
                        m_dtDangkyPhongkham.AcceptChanges();
                    }
                    if (actionResult == ActionResult.Error)
                    {
                        Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
        private void cmdAddDvuKCB_Click(object sender, EventArgs e)
        {

            if (Utility.Int32Dbnull(cboKieuKham.Value) <= -1)
            {
                Utility.ShowMsg("Bạn phải chọn dịch vụ khám cần thêm cho bệnh nhân", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            DmucDichvukcb objDichvuKCB =
                   DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
            if (objDichvuKCB != null)
            {
                Utility.SetMsg(lblDonGia, Utility.sDbnull(objDichvuKCB.DonGia), true);
                Utility.SetMsg(lblPhuThu, Utility.sDbnull(Utility.Int32Dbnull(objLuotkham.DungTuyen.Value, 0) == 1 ? objDichvuKCB.PhuthuDungtuyen : objDichvuKCB.PhuthuTraituyen), true);
                if ((m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdPhongkham + "=" + objDichvuKCB.IdPhongkham + "").GetLength(0) <= 0))
                {
                    ProcessData();
                }
                else
                {
                    if (Utility.AcceptQuestion("Bệnh nhân đã được đăng ký khám chữa bệnh tại phòng khám này. Bạn có muốn tiếp tục đăng ký dịch vụ KCB mới vừa chọn hay không?", "Thông báo", true))
                    {
                        ProcessData();
                    }
                }
            }

        }
        private KcbLuotkham CreatePatientExam()
        {
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham objLuotkham = new KcbLuotkham();

            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Patient_ID);
            objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
            return objLuotkham;


        }
        private KcbDanhsachBenhnhan CreatePatientInfo()
        {
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.IdBenhnhan));
            KcbDanhsachBenhnhan objBenhnhan = new KcbDanhsachBenhnhan();

            SqlQuery sqlQuery = new Select().From(KcbDanhsachBenhnhan.Schema)
                .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(Patient_ID);

            objBenhnhan = sqlQuery.ExecuteSingle<KcbDanhsachBenhnhan>();
            return objBenhnhan;
        }
        private void ModifycommandAssignDetail()
        {
            try
            {
                cmdSuaChiDinh.Enabled = Utility.isValidGrid(grdAssignDetail);
                cmdXoaChiDinh.Enabled = Utility.isValidGrid(grdAssignDetail);
                cmdPrintAssign.Enabled = Utility.isValidGrid(grdAssignDetail);
                chkIntach.Enabled = cmdPrintAssign.Enabled;
                cboServicePrint.Enabled = cmdPrintAssign.Enabled;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            //cmdSend.Enabled = grdAssignInfo.RowCount > 0;
        }

        private void cmdThemChiDinh_Click(object sender, EventArgs e)
        {
            KcbLuotkham objLuotkham = CreatePatientExam();
            KcbDanhsachBenhnhan objbenhnhan = CreatePatientInfo();
            if (objLuotkham != null)
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.Exam_ID = -1;
                frm.txtAssign_ID.Text = "-100";
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objbenhnhan;
                frm.txtidkham.Text = Utility.sDbnull(grdRegExam.GetValue("id_kham"), -1);
                //frm.ObjRegExam = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(grdRegExam.GetValue("id_kham"), -1));
                frm.m_eAction = action.Insert;
                frm.noitru = 0;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LoadChiDinh();
                    UpdateSumOfChiDinh();
                }
                ModifycommandAssignDetail();
            }
            ModifyCommand();
        }
        private bool InValiUpdateChiDinh()
        {
            int Assign_ID = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(Assign_ID)
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu chỉ định này đã thanh toán, Bạn không được phép sửa(Có thể liên hệ với Quầy thanh toán để hủy thanh toán trước khi sửa lại)", "Thông báo");
                cmdThemChiDinh.Focus();
                return false;
            }
            return true;
        }
        private void cmdSuaChiDinh_Click(object sender, EventArgs e)
        {
            try
            {
                KcbLuotkham objLuotkham = CreatePatientExam();
                KcbDanhsachBenhnhan objbenhnhan = CreatePatientInfo();
                if (objLuotkham != null)
                {
                    if (!InValiUpdateChiDinh()) return;
                    frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN", 0);
                    frm.noitru = 0;

                    frm.Exam_ID = Utility.Int32Dbnull(-1, -1);
                    frm.objLuotkham = CreatePatientExam();
                    frm.objBenhnhan = objbenhnhan;
                    frm.m_eAction = action.Update;
                    frm.txtAssign_ID.Text = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
                    frm.ShowDialog();
                    if (!frm.m_blnCancel)
                    {
                        //  LoadChiDinhCLS();
                        LoadChiDinh();
                        UpdateSumOfChiDinh();
                    }
                    ModifycommandAssignDetail();
                }
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }

        }
        private bool isValidChidinh()
        {
            bool b_Cancel = false;
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                    return false;
                    break;
                }
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                return false;

            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        private void PerforActionDeleteAssign()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                int Assign_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                                    -1);

                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail_ID);
                gridExRow.Delete();
                m_dtChiDinhCLS.AcceptChanges();
            }
            UpdateSumOfChiDinh();
        }

        private void cmdXoaChiDinh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidChidinh()) return;
                var query = (from chidinh in grdAssignDetail.GetCheckedRows().AsEnumerable()
                             let x = Utility.sDbnull(chidinh.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value)
                             select x).ToArray();
                string ServiceDetail_Name = string.Join("; ", query);
                string Question = string.Format("Bạn có muốn xóa thông tin chỉ định {0} \n đang chọn không", ServiceDetail_Name);
                if (Utility.AcceptQuestion(Question, "Thông báo", true))
                {
                    PerforActionDeleteAssign();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa chỉ định của bệnh nhân ID={0}, PID={1}, Tên={2}, chỉ định bị xóa={3} thành công", Utility.getValueOfGridCell(grdList, "id_benhnhan"), Utility.getValueOfGridCell(grdList, "ma_luotkham"), Utility.getValueOfGridCell(grdList, "ten_benhnhan"), ServiceDetail_Name), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                    //ModifyCommmand();
                    ModifycommandAssignDetail();
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }


        }

        private void grdAssignDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void grdAssignDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void txtTongChiPhiKham_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(txtTongChiPhiKham);
        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            try
            {
                frm_KCB_DANGKY frm = new frm_KCB_DANGKY(this.Args);
                frm.m_enAct = action.Insert;
                frm.m_dtPatient = m_dtPatient;
                this.myTrace.FunctionID = globalVariables.FunctionID;
                this.myTrace.FunctionName = globalVariables.FunctionName;
                frm.myTrace = this.myTrace;
                frm._OnActionSuccess += frm__OnActionSuccess;
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.MBlnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
                ModifycommandAssignDetail();
                ModifyButtonCommandRegExam();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }

        }

        void frm__OnActionSuccess()
        {
            UpdateGroup();
        }
        /// <summary>
        /// hàm thục hiện việc thêm lần khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemLanKham_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn bệnh nhân để thêm lượt khám mới");
                    return;
                }
                DataTable _temp = _KCB_DANGKY.KcbLaythongtinBenhnhan(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)));
                if (_temp != null && Utility.ByteDbnull(_temp.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) > 0 && Utility.ByteDbnull(_temp.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) < 4)
                {
                    Utility.ShowMsg("Bệnh nhân đang ở trạng thái nội trú và chưa ra viện nên không thể thêm lần khám mới. Đề nghị bạn xem lại");
                    return;
                }
                frm_KCB_DANGKY frm = new frm_KCB_DANGKY(this.Args);
                frm.txtIdBenhnhan.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm.txtIdBenhnhan.Enabled = false;
                frm._mabenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.Maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                this.myTrace.FunctionID = globalVariables.FunctionID;
                this.myTrace.FunctionName = globalVariables.FunctionName;
                frm.myTrace = this.myTrace;
                frm.m_enAct = action.Add;
                frm._OnActionSuccess += frm__OnActionSuccess;
                frm.m_dtPatient = m_dtPatient;
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.MBlnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                    
                }
                ModifyCommand();
                ModifycommandAssignDetail();
                ModifyButtonCommandRegExam();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        /// <summary>
        /// hàm thực hiện sửa thông tin của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaThongTinBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để sửa thông tin");
                    return;
                }
                SqlQuery sqlkiemtra = new Select().From(KcbThanhtoan.Schema).Where(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)));
                bool updateOnly=sqlkiemtra.GetRecordCount() > 0;
                //if ()
                //{
                //    frm_KCB_DANGKY_CAPNHAT frm = new frm_KCB_DANGKY_CAPNHAT(this.Args);
                //    frm.txtMaBN.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                //    frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                //    frm._mabenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                //    frm.Maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                //    frm.txtMaBN.Enabled = false;
                //    frm.txtMaLankham.Enabled = false;
                //    this.myTrace.FunctionID = globalVariables.FunctionID;
                //    this.myTrace.FunctionName = globalVariables.FunctionName;
                //    frm.myTrace = this.myTrace;
                //    frm._OnActionSuccess += frm__OnActionSuccess;
                //    frm.MEnAction = action.Update;
                //    frm.m_dtPatient = m_dtPatient;
                //    frm.grdList = grdList;
                //    frm.ShowDialog();
                //    if (!frm.MBlnCancel)
                //    {
                //        UpdateGroup();
                //        grdList_SelectionChanged(grdList, new EventArgs());

                //    }
                //}
                //else
                //{
                    frm_KCB_DANGKY frm = new frm_KCB_DANGKY(this.Args);
                    frm.updateonly = updateOnly;
                    frm.txtIdBenhnhan.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                    frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                    frm._mabenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                    frm.Maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                    frm.txtIdBenhnhan.Enabled = false;
                    frm.txtMaLankham.Enabled = false;
                    this.myTrace.FunctionID = globalVariables.FunctionID;
                    this.myTrace.FunctionName = globalVariables.FunctionName;
                    frm.myTrace = this.myTrace;
                    frm._OnActionSuccess += frm__OnActionSuccess;
                    frm.m_enAct = action.Update;
                    frm.m_dtPatient = m_dtPatient;
                    frm.grdList = grdList;
                    frm.ShowDialog();
                    if (!frm.MBlnCancel)
                    {
                        UpdateGroup();
                        grdList_SelectionChanged(grdList, new EventArgs());

                    }
                //}
                ModifyCommand();
                ModifycommandAssignDetail();
                ModifyButtonCommandRegExam();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }

        }
        #region "Thông tin khám chữa bệnh"
        private void cboKieuKham_TextChanged(object sender, EventArgs e)
        {
            string _rowFilter = "1=1";
            if (!string.IsNullOrEmpty(cboKieuKham.Text) && m_blnHasloaded)
            {


                cboKieuKham.DroppedDown = true;

            }
            else
            {

                cboKieuKham.DroppedDown = false;
            }


        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin khám 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaKham_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList))
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để xóa khám");
                return;
            }
            if (!IsValidData()) return;
            if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa công khám đang chọn không ?", "Thông báo", true))
            {
                HuyThamKham();
            }
            ModifyButtonCommandRegExam();
        }
        private void HuyThamKham()
        {

            if (grdRegExam.CurrentRow != null)
            {

                int v_RegId = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);

                //if (Utility.AcceptQuestion("Bạn muốn hủy dịch vụ khám đang chọn ", "Thông báo", true))
                //{

                    ActionResult actionResult = _KCB_DANGKY.PerformActionDeleteRegExam(v_RegId);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa công khám của bệnh nhân ID={0}, PID={1}, Tên={2}, công khám={3} thành công", Utility.getValueOfGridCell(grdList, "id_benhnhan"), Utility.getValueOfGridCell(grdList, "ma_luotkham"), Utility.getValueOfGridCell(grdList, "ten_benhnhan"), Utility.getValueOfGridCell(grdRegExam, "ten_dichvukcb")), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            DataRow[] arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdKham + "=" + v_RegId + " OR  " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + v_RegId);
                            if (arrDr.GetLength(0) > 0)
                            {
                                int _count = arrDr.Length;
                                List<string> lstregid = (from p in arrDr.AsEnumerable()
                                                         select p.Field<long>(KcbDangkyKcb.IdKhamColumn.ColumnName).ToString()
                                                      ).ToList<string>();
                                for (int i = 1; i <= _count; i++)
                                {
                                    DataRow[] tempt = m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdKham + "=" + lstregid[i - 1]);
                                    if (tempt.Length > 0)
                                        tempt[0].Delete();
                                    m_dtDangkyPhongkham.AcceptChanges();
                                }
                            }
                            m_dtDangkyPhongkham.AcceptChanges();

                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Bạn thực hiện xóa công khám không thành công. Liên hệ đơn vị cung cấp phần mềm để được trợ giúp", "Thông báo");
                            break;
                    }
                //}
            }
            ModifyButtonCommandRegExam();

        }
        private void ModifyButtonCommandRegExam()
        {
            if (Utility.isValidGrid(grdRegExam))
            {
                cmdXoaKham.Enabled = Utility.Int32Dbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.TrangthaiThanhtoan), 0) == 0;
                cmdInBienlai.Enabled = !cmdXoaKham.Enabled;
                cmdInhoadon.Enabled = cmdInBienlai.Enabled;
                cmdThanhToanKham.Text = cmdXoaKham.Enabled ? "T.Toán" : "Hủy TT";
                cmdThanhToanKham.Tag = cmdXoaKham.Enabled ? "TT" : "HTT";
                cmdInPhieuKham.Enabled = grdRegExam.RowCount > 0 && grdRegExam.CurrentRow.RowType == RowType.Record;

                grdRegExam.RootTable.Columns["colThanhtoan"].ButtonText = cmdThanhToanKham.Text;
                pnlPrint.Visible = !cmdXoaKham.Enabled;
                cmdInBienlai.Visible = !cmdXoaKham.Enabled;
                cmdInhoadon.Visible = cmdInBienlai.Visible;
                cmdThanhToanKham.Enabled = PropertyLib._KCBProperties.Chophepthanhtoan;
                cmdThanhToanKham.Visible = cmdThanhToanKham.Enabled;
            }
            else
            {
                cmdXoaKham.Enabled = cmdInBienlai.Enabled = cmdInPhieuKham.Enabled = cmdThanhToanKham.Enabled = false;
                pnlPrint.Visible = false;
                cmdInBienlai.Visible = false;
                cmdInhoadon.Visible = false;
                cmdThanhToanKham.Visible = false;
            }
        }
        private void cmdInPhieuKham_Click(object sender, EventArgs e)
        {
            InPhieu();
            ModifyButtonCommandRegExam();
        }
        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";
        private string m_strDefaultLazerPrinterName = "";
        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strSaveandprintPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strSaveandprintPath));
            }
            catch
            {
            }
        }
        private readonly string strSaveandprintPath1 = Application.StartupPath +
                                                       @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";
        private void AutoloadSaveAndPrintConfig()
        {
            try
            {
                Try2CreateFolder();
                if (File.Exists(strSaveandprintPath1))
                {
                    using (var _reader = new StreamReader(strSaveandprintPath1))
                    {
                        if (_reader.ReadLine().Trim() != "")
                        {
                            m_strDefaultLazerPrinterName = _reader.ReadLine().Trim();

                            _reader.BaseStream.Flush();
                            _reader.Close();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        KCB_QMS _KCB_QMS = new KCB_QMS();
        void InPhieu()
        {
            try
            {
                if (grdRegExam.GetDataRows().Count() <= 0 || grdRegExam.CurrentRow.RowType != RowType.Record)
                    return;
                
                if (PropertyLib._MayInProperties.KieuInPhieuKCB == KieuIn.Innhiet)
                    InPhieuKCB();
                else
                    InphieuKham();
                //Thêm thông tin QMS cho phòng khám
                if (objBenhnhan != null && objLuotkham != null && objRegExam != null)
                    _KCB_QMS.QmsPhongkhamInsert((int)objRegExam.SttKham.Value, objRegExam.MaPhongStt, DateTime.Now, objRegExam.NgayTiepdon.Value, objRegExam.MaLuotkham, objBenhnhan.IdBenhnhan, objBenhnhan.TenBenhnhan, (int)objBenhnhan.NamSinh, Utility.Int32Dbnull(grdList.GetValue("tuoi"), 0), objBenhnhan.GioiTinh, objRegExam.MaKhoaThuchien, (int)objRegExam.IdPhongkham, objRegExam.IdKham, (int)objRegExam.IdDichvuKcb, objRegExam.TenDichvuKcb);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg("Lỗi khi in phiếu khám\n" + ex.Message);
            }
        }
        /// <summary>
        /// Lấy về regID của phòng khám thay vì lấy  phải phí dịch vụ kèm theo
        /// </summary>
        /// <returns></returns>
        int GetrealRegID()
        {
            int IdKham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
            int idphongchidinh = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.IdChaColumn.ColumnName].Value, -1);
            int LaphiDVkemtheo = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.LaPhidichvukemtheoColumn.ColumnName].Value, -1);
            if (LaphiDVkemtheo == 1)
            {
                foreach (GridEXRow _row in grdRegExam.GetDataRows())
                {
                    if (Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1) == idphongchidinh)
                        return Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
                }
            }
            else
                return IdKham;
            return IdKham;
        }
        private void InPhieuKCB()
        {

            int IdKham = -1;
            string tieude = "", reportname = "";
            //VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET crpt = new VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET();
            ReportDocument crpt = Utility.GetReport("tiepdon_PHIEUKHAM_NHIET", ref tieude, ref reportname);
            if (crpt == null) return;
            var objPrint = new frmPrintPreview("IN PHIẾU KHÁM", crpt, true, true);
            IdKham = GetrealRegID();
           
            try
            {
                objRegExam = KcbDangkyKcb.FetchByID(IdKham);
                DmucKhoaphong lDepartment = DmucKhoaphong.FetchByID(objRegExam.IdPhongkham);
                Utility.SetParameterValue(crpt, "PHONGKHAM", Utility.sDbnull(lDepartment.MaKhoaphong));
                Utility.SetParameterValue(crpt, "STT", Utility.sDbnull(objRegExam.SttKham, ""));
                Utility.SetParameterValue(crpt, "BENHAN", Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, ""));
                Utility.SetParameterValue(crpt, "TENBN", Utility.sDbnull(grdList.CurrentRow.Cells[KcbDanhsachBenhnhan.Columns.TenBenhnhan].Value, ""));
                Utility.SetParameterValue(crpt, "GT_TUOI", Utility.sDbnull(grdList.CurrentRow.Cells[KcbDanhsachBenhnhan.Columns.GioiTinh].Value, "") + " - " + Utility.sDbnull(grdList.CurrentRow.Cells["Tuoi"].Value, "") + " tuổi");
                string SOTHE = "Không có thẻ";
                string HANTHE = "Không có hạn";
                if (Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaDoituongKcb].Value) == "BHYT")
                {
                    SOTHE = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MatheBhyt].Value, "Không có thẻ");
                    HANTHE = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.NgayketthucBhyt].Value);
                }
                Utility.SetParameterValue(crpt, "SOTHE", SOTHE);
                Utility.SetParameterValue(crpt, "HANTHE", HANTHE);
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                    objPrint.ShowDialog();
                else
                {
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Có lỗi trong quá trình in phiếu khám-->\n" + ex.Message);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }
        KcbDangkyKcb objRegExam = null;
        private void InphieuKham()
        {
            int IdKham = GetrealRegID();
            objRegExam = KcbDangkyKcb.FetchByID(IdKham);
            if (objRegExam != null)
            {
                new Update(KcbDangkyKcb.Schema)
                    .Set(KcbDangkyKcb.Columns.TrangthaiIn).EqualTo(1)
                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objRegExam.IdKham).Execute();
                IEnumerable<GridEXRow> query = from kham in grdRegExam.GetDataRows()
                                               where
                                                   kham.RowType == RowType.Record &&
                                                   Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1) ==
                                                   Utility.Int32Dbnull(objRegExam.IdKham)
                                               select kham;
                if (query.Count() > 0)
                {
                    GridEXRow gridExRow = query.FirstOrDefault();
                    gridExRow.BeginEdit();
                    gridExRow.Cells[KcbDangkyKcb.Columns.TrangthaiIn].Value = 1;
                    gridExRow.EndEdit();
                    grdRegExam.UpdateData();
                }
                DataTable v_dtData = _KCB_DANGKY.LayThongtinInphieuKCB(IdKham);
                THU_VIEN_CHUNG.CreateXML(v_dtData, Application.StartupPath + @"\Xml4Reports\PhieuKCB.XML");
                Utility.CreateBarcodeData(ref v_dtData, Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.MaLuotkham)));
                if (v_dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo");
                    return;
                }
                KcbLuotkham objLuotkham = CreatePatientExam();
                if (objLuotkham != null)
                    KcbInphieu.INPHIEU_KHAM(Utility.sDbnull(objLuotkham.MaDoituongKcb), v_dtData,
                                                  "PHIẾU KHÁM BỆNH", cboA4.Text == @"A5" ? "A5" : "A4");
            }
        }
        /// <summary>
        /// hàm thực hiện lần thanh toán 
        /// </summary>
        /// <returns></returns>
        private KcbThanhtoan CreatePayment()
        {
            KcbLuotkham objLuotkham = CreatePatientExam();
            KcbThanhtoan objPayment = new KcbThanhtoan();
            if (objLuotkham != null)
            {
                objPayment.IdThanhtoan = -1;
                objPayment.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPayment.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
                objPayment.NgayThanhtoan = globalVariables.SysDate;
                objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                objPayment.KieuThanhtoan = 0;//0=Ngoại trú;1=nội trú
                objPayment.TrangthaiIn = 0;
                objPayment.NgayIn = null;
                objPayment.NguoiIn = string.Empty;
                objPayment.NgayTonghop = null;
                objPayment.NguoiTonghop = string.Empty;
                objPayment.NgayChot = null;
                objPayment.TrangthaiChot = 0;
                objPayment.TongTien = 0;
                //2 mục này được tính lại ở Business
                objPayment.BnhanChitra = -1;
                objPayment.NoiTru = 0;
                objPayment.BhytChitra = -1;
                objPayment.TileChietkhau = 0;
                objPayment.KieuChietkhau = "%";
                objPayment.TongtienChietkhau = 0;
                objPayment.TongtienChietkhauChitiet = 0;
                objPayment.TongtienChietkhauHoadon = 0;
                objPayment.MaLydoChietkhau = "";
                objPayment.NgayTao = globalVariables.SysDate;
                objPayment.NguoiTao = globalVariables.UserName;
                objPayment.IpMaytao = globalVariables.gv_strIPAddress;
                objPayment.TenMaytao = globalVariables.gv_strComputerName;
                objPayment.MaPttt = "TM";
            }
            return objPayment;
        }
        KcbLuotkham objLuotkham = null;
        KcbDanhsachBenhnhan objBenhnhan = null;
        private List<int> GetIDKham()
        {
            List<int> lstRegID = new List<int>();
            int IdKham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            DataRow[] arrDr = null;
            if (PropertyLib._KCBProperties.Thanhtoancaphidichvukemtheo)
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString() + " OR " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + IdKham.ToString());
            else
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString());
            foreach (DataRow dr in arrDr)
            {
                if (Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdThanhtoan], -1) > 0)
                {
                    IdKham = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                    if (!lstRegID.Contains(IdKham)) lstRegID.Add(IdKham);
                }
            }
            return lstRegID;
        }
        /// <summary>
        /// hàm thực hiện thanh toán chi tiết 
        /// </summary>
        /// <returns></returns>
        private KcbThanhtoanChitiet[] Taodulieuthanhtoanchitiet(ref List<int> lstRegID)
        {
            int IdKham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            DataRow[] arrDr = null;
            if (PropertyLib._KCBProperties.Thanhtoancaphidichvukemtheo)
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString() + " OR " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + IdKham.ToString());
            else
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString());
            List<KcbThanhtoanChitiet> lstPaymentDetail = new List<KcbThanhtoanChitiet>();

            foreach (DataRow dr in arrDr)
            {
                if (Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdThanhtoan], -1) <= 0)
                {
                    KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                    newItem.IdThanhtoan = -1;
                    newItem.IdChitiet = -1;
                    newItem.TinhChiphi = 1;
                    if (!lstRegID.Contains(IdKham)) lstRegID.Add(IdKham);
                    if (objLuotkham.PtramBhyt != null) newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                    if (objLuotkham.PtramBhytGoc != null) newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                    newItem.SoLuong = 1;
                    //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                    newItem.BnhanChitra = -1;
                    newItem.BhytChitra = -1;
                    newItem.GiaGoc = 0;
                    newItem.DonGia = Utility.DecimaltoDbnull(dr[KcbDangkyKcb.Columns.DonGia], 0);
                    newItem.TyleTt = Utility.DecimaltoDbnull(dr[KcbThanhtoanChitiet.Columns.TyleTt], 0);
                    newItem.PhuThu = Utility.DecimaltoDbnull(dr[KcbDangkyKcb.Columns.PhuThu], 0);
                    newItem.TuTuc = Utility.ByteDbnull(dr[KcbDangkyKcb.Columns.TuTuc], 0);
                    newItem.IdPhieu = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                    newItem.IdKham = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                    newItem.IdPhieuChitiet = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                    newItem.IdDichvu = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdDichvuKcb], -1);
                    newItem.IdChitietdichvu = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKieukham], -1);
                    newItem.TenChitietdichvu = Utility.sDbnull(dr[KcbDangkyKcb.Columns.TenDichvuKcb], "Không xác định").Trim();
                    newItem.TenBhyt = Utility.sDbnull(dr[KcbDangkyKcb.Columns.TenDichvuKcb], "Không xác định").Trim();
                    newItem.SttIn = 0;
                    newItem.IdKhoakcb = Utility.Int16Dbnull(dr["id_khoakcb"], -1);
                    newItem.IdPhongkham = Utility.Int16Dbnull(dr[KcbDangkyKcb.Columns.IdPhongkham], -1);
                    newItem.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                    newItem.IdLoaithanhtoan = (byte)(Utility.Int32Dbnull(dr[KcbDangkyKcb.LaPhidichvukemtheoColumn.ColumnName], 0) == 1 ? 0 : 1);
                    newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(newItem.IdLoaithanhtoan);
                    newItem.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                    newItem.DonviTinh = "Lượt";
                    newItem.KieuChietkhau = "%";
                    newItem.TileChietkhau = 0;
                    newItem.TienChietkhau = 0m;
                    newItem.IdThanhtoanhuy = -1;
                    newItem.TrangthaiHuy = 0;
                    newItem.TrangthaiBhyt = 0;
                    newItem.TrangthaiChuyen = 0;
                    newItem.NoiTru = 0;
                    newItem.NguonGoc = (byte)0;
                    newItem.NgayTao = globalVariables.SysDate;
                    newItem.NguoiTao = globalVariables.UserName;

                    lstPaymentDetail.Add(newItem);
                }
            }
            return lstPaymentDetail.ToArray(); ;
        }
        private void HuyThanhtoan()
        {
            try
            {
                string ma_lydohuy = "";
                string ten_lydohuy = "";
                if (!Utility.isValidGrid(grdRegExam)) return;
                if (!Utility.Coquyen("quyen_huythanhtoan_tatca"))
                {
                    Utility.ShowMsg("Bạn không được cấp quyền hủy thanh toán");
                    return;
                }

                if (objLuotkham == null)
                {
                    objLuotkham = CreatePatientExam();
                }
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin bệnh nhân dựa vào dữ liệu trên lưới danh sách bệnh nhân. Liên hệ bộ phận IT để được trợ giúp");
                    return;
                }
                if (Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, 0) >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy thanh toán ngoại trú nữa");
                    return;
                }
                KcbDangkyKcb objDangky = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1));
                if (objDangky == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin Đăng ký dịch vụ KCB. Liên hệ bộ phận IT để được trợ giúp");
                    return;
                }
                if (Utility.Byte2Bool(objDangky.DachidinhCls))
                {
                    Utility.ShowMsg("Dịch vụ Khám đang chọn đã được bác sĩ chỉ định dịch vụ cận lâm sàng nên bạn không thể hủy thanh toán");
                    return;
                }
                if (Utility.Byte2Bool(objDangky.DakeDonthuoc))
                {
                    Utility.ShowMsg("Dịch vụ Khám đang chọn đã được bác sĩ kê đơn thuốc nên bạn không thể hủy thanh toán");
                    return;
                }
                //if (Utility.Byte2Bool(objDangky.TrangThai))
                //{
                //    Utility.ShowMsg("Dịch vụ Khám đang chọn đã được khám xong nên bạn không thể hủy thanh toán");
                //    return;
                //}


                int v_intIdThanhtoan = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_intIdThanhtoan);
                if (objPayment != null)
                {
                    //Kiểm tra ngày hủy
                    int kcbThanhtoanSongayHuythanhtoan =
                        Utility.Int32Dbnull(
                            THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
                    var chenhlech =
                        (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                    if (chenhlech > kcbThanhtoanSongayHuythanhtoan)
                    {
                        Utility.ShowMsg(
                            "Hệ thống không cho phép bạn hủy thanh toán đã quá ngày. Cần liên hệ quản trị hệ thống để được trợ giúp");
                        return;
                    }
                    if (Utility.Byte2Bool(objPayment.TrangthaiChot))
                    {
                        Utility.ShowMsg(
                            "Thanh toán đang chọn đã được chốt nên bạn không thể hủy thanh toán. Mời bạn xem lại!");
                        return;
                    }

                    List<int> lstRegID = GetIDKham();
                    if (PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan)
                    {
                        frm_HuyThanhtoan frm = new frm_HuyThanhtoan("1");
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = v_intIdThanhtoan;
                        frm.Chuathanhtoan = 0;
                        frm.ShowCancel = true;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            foreach (DataRow _row in m_dtDangkyPhongkham.Rows)
                            {
                                if (lstRegID.Contains(Utility.Int32Dbnull(_row[KcbDangkyKcb.Columns.IdKham], -1)))
                                {
                                    _row["ten_trangthai_thanhtoan"] = "Chưa thanh toán";
                                    _row[KcbDangkyKcb.Columns.IdThanhtoan] = -1;
                                    _row[KcbDangkyKcb.Columns.TrangthaiThanhtoan] = (byte)0;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                            if (!Utility.AcceptQuestion(string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}", v_intIdThanhtoan.ToString()), "Thông báo", true))
                            {
                                return;
                            }
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1", false) == "1")
                        {
                            frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN", "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện...", "Lý do hủy thanh toán");
                            _Nhaplydohuythanhtoan.ShowDialog();
                            if (_Nhaplydohuythanhtoan.m_blnCancel) return;
                            ma_lydohuy = _Nhaplydohuythanhtoan.ma;
                            ten_lydohuy = _Nhaplydohuythanhtoan.ten;
                        }
                        bool HUYTHANHTOAN_HUYBIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
                        ActionResult actionResult = new KCB_THANHTOAN().HuyThanhtoan(KcbThanhtoan.FetchByID(v_intIdThanhtoan), objLuotkham, ma_lydohuy, Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbThanhtoan.Columns.IdHdonLog], -1), HUYTHANHTOAN_HUYBIENLAI);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy thanh toán công khám cho người bệnh tại form tiếp đón Id={0}, PID={1}, Tên ={2}, Công khám={3}, Lý do hủy={4} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan)), Utility.sDbnull(grdRegExam.GetValue("ten_dichvukcb")),string.Format("Mã: {0}- Tên: {1}",ma_lydohuy, ten_lydohuy)), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                foreach (DataRow _row in m_dtDangkyPhongkham.Rows)
                                {
                                    if (lstRegID.Contains(Utility.Int32Dbnull(_row[KcbDangkyKcb.Columns.IdKham], -1)))
                                    {
                                        _row["ten_trangthai_thanhtoan"] = "Chưa thanh toán";
                                        _row[KcbDangkyKcb.Columns.IdThanhtoan] = -1;
                                        _row[KcbDangkyKcb.Columns.TrangthaiThanhtoan] = 0;
                                    }
                                }
                                break;
                            case ActionResult.ExistedRecord:
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.UNKNOW:
                                Utility.ShowMsg("Lỗi không xác định", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Cancel:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy thanh toán", "Thông báo", MessageBoxIcon.Error);
            }
            finally
            {
                ModifyButtonCommandRegExam();
            }

        }
        void Thanhtoan()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (grdRegExam.RowCount <= 0)
                {
                    Utility.ShowMsg("Chọn phòng khám để thanh toán,Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                int IdKham = Utility.Int32Dbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));
                SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(IdKham)
                    .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bản ghi này đã thanh toán,Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                if (PropertyLib._KCBProperties.Hoitruockhithanhtoan)

                    if (!Utility.AcceptQuestion("Bạn có muốn thực hiện việc thanh toán khám bệnh cho bệnh nhân không ?",
                                               "Thông báo", true))
                        return;
                long Payment_Id = -1;
                objLuotkham = CreatePatientExam();
                if (Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, 0) >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép thanh toán ngoại trú nữa");
                    return;
                }
                KcbThanhtoan objPayment = CreatePayment();
                DateTime maxDate = Convert.ToDateTime(grdRegExam.GetValue("ngay_dangky"));
                objPayment.MaxNgayTao = maxDate;
                List<int> lstRegID = new List<int>();
                decimal TTBN_Chitrathucsu = 0;
                string ErrMsg = "";
                ActionResult actionResult = new KCB_THANHTOAN().ThanhtoanChiphiDvuKcb(objPayment, objLuotkham, Taodulieuthanhtoanchitiet(ref lstRegID).ToList<KcbThanhtoanChitiet>(),null, ref Payment_Id, -1, false,false,"", ref TTBN_Chitrathucsu, ref ErrMsg);

                switch (actionResult)
                {
                    case ActionResult.Success:
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thanh toán công khám cho người bệnh tại form tiếp đón Id={0}, PID={1}, Tên ={2}, Công khám={3} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan)), Utility.sDbnull(grdRegExam.GetValue("ten_dichvukcb"))), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        foreach (DataRow _row in m_dtDangkyPhongkham.Rows)
                        {
                            if (lstRegID.Contains(Utility.Int32Dbnull(_row[KcbDangkyKcb.Columns.IdKham], -1)))
                            {
                                _row["ten_trangthai_thanhtoan"] = "Đã thanh toán";
                                _row[KcbDangkyKcb.Columns.IdThanhtoan] = Payment_Id;
                                _row[KcbDangkyKcb.Columns.TrangthaiThanhtoan] = 1;
                            }
                        }
                        m_dtDangkyPhongkham.AcceptChanges();
                        Payment_Id = Utility.Int64Dbnull(objPayment.IdThanhtoan);
                        if (PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan && TTBN_Chitrathucsu > 0)
                        {
                            int KCB_THANHTOAN_KIEUINHOADON = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KIEUINHOADON", "1", false));
                            if (KCB_THANHTOAN_KIEUINHOADON == 1 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                InHoadon(Payment_Id);
                            if (KCB_THANHTOAN_KIEUINHOADON == 2 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, Payment_Id, objLuotkham, 0);
                        }
                        Utility.SetMsg(lblMsg, "Thanh toán thành công", false);
                        break;
                    case ActionResult.Error:
                        Utility.SetMsg(lblMsg, "Lỗi khi thanh toán", true);
                        break;
                    case ActionResult.Cancel:
                        Utility.ShowMsg(ErrMsg);
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                ModifyButtonCommandRegExam();
            }


        }
        private KcbPhieuthu CreatePhieuThu(KcbThanhtoan objPayment, decimal TONG_TIEN)
        {
            var objPhieuThu = new KcbPhieuthu();
            objPhieuThu.IdThanhtoan = objPayment.IdThanhtoan;
            objPhieuThu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 0);
            objPhieuThu.SoluongChungtugoc = 1;
            objPhieuThu.LoaiPhieuthu = Convert.ToByte(0);
            objPhieuThu.NgayThuchien = globalVariables.SysDate;
            objPhieuThu.SoTien = TONG_TIEN;
            objPhieuThu.NguoiNop = globalVariables.UserName;
            objPhieuThu.IdNhanvien = globalVariables.gv_intIDNhanvien;
            objPhieuThu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
            objPhieuThu.IdBenhnhan = objPayment.IdBenhnhan;
            objPhieuThu.MaLuotkham = objPayment.MaLuotkham;
            objPhieuThu.TaikhoanCo = "";
            objPhieuThu.TaikhoanNo = "";
            objPhieuThu.LydoNop = "Thu phí KCB bệnh nhân";
            objPhieuThu.NguoiTao = globalVariables.UserName;
            objPhieuThu.NgayTao = globalVariables.SysDate;
            return objPhieuThu;
        }
        void InHoadon(long _Payment_ID)
        {
            try
            {
                KcbThanhtoan objPayment = new Select().From(KcbThanhtoan.Schema).Where(KcbThanhtoan.IdThanhtoanColumn).IsEqualTo(_Payment_ID).ExecuteSingle<KcbThanhtoan>();
                if (objPayment == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin hóa đơn thanh toán", "Thông báo lỗi", MessageBoxIcon.Warning);
                    return;
                }
                decimal TONG_TIEN = Utility.Int32Dbnull(objPayment.TongTien, -1);
                ActionResult actionResult = new KCB_THANHTOAN().UpdateDataPhieuThu(CreatePhieuThu(objPayment, TONG_TIEN));
                switch (actionResult)
                {
                    case ActionResult.Success:
                        new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(_Payment_ID);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in hóa đơn", "Thông báo lỗi", MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
            }
        }





        private void cmdThanhToanKham_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdRegExam)) return;
            if (Utility.sDbnull(cmdThanhToanKham.Tag, "ABCD") == "TT")
                Thanhtoan();
            else
                HuyThanhtoan();
        }
        /// <summary>
        /// hàm thực hiện việc hủy phiếu khám bệnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRegExam_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "colDelete")
            {
                if (!IsValidData())
                    return;
                HuyThamKham();
            }
            if (e.Column.Key == "colIn")
            {
                InPhieu();
            }
            if (e.Column.Key == "colThanhtoan")
            {
                if (Utility.sDbnull(cmdThanhToanKham.Tag, "ABCD") == "TT")
                    Thanhtoan();
                else
                    HuyThanhtoan();
            }
        }

        #endregion
        private bool IsValidData()
        {
            if (!Utility.isValidGrid(grdRegExam)) return false;
            if (grdRegExam.CurrentRow == null) return false;
            if (Utility.Coquyen("KCB_XOACONGKHAM") || globalVariables.UserName == Utility.sDbnull(grdRegExam.GetValue("nguoi_tao")))
            {
            }
            else
            {
                Utility.ShowMsg("Bạn không được cấp quyền xóa công khám. Liên hệ quản trị hệ thống để được cấp thêm quyền", "Thông báo");
                return false;
            }
            int v_RegId = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            KcbDangkyKcb objRegExam = KcbDangkyKcb.FetchByID(v_RegId);
            if (objRegExam != null)
            {
                if (objRegExam.TrangthaiThanhtoan>=1)
                {
                    Utility.ShowMsg("Công khám đang chọn đã thanh toán, Bạn không thể xóa", "Thông báo");
                    grdRegExam.Focus();
                    return false;
                }
                else if (objRegExam.TrangThai >= 1)
                {
                    Utility.ShowMsg("Công khám bạn chọn đã được bác sĩ kết thúc khám nên bạn không thể xóa", "Thông báo");
                    grdRegExam.Focus();
                    return false;
                }

                else if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_XOATHONGTINLANKHAM_FULL", "0", false) == "1")
                {
                    SqlQuery q =
                        new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdKham).IsEqualTo(
                            objRegExam.IdKham).And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS và đã được thanh toán. Yêu cầu Hủy thanh toán các chỉ định CLS trước khi hủy phòng khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(
                            objRegExam.IdKham).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc và đã được thanh toán. Yêu cầu hủy thanh toán đơn thuốc trước khi hủy phòng khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                }
                else//Nếu có chỉ định CLS hoặc thuốc thì không cho phép xóa
                {
                    if (objRegExam.DachidinhCls >= 1)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS. Yêu cầu xóa chỉ định CLS trước khi xóa công khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    else if (objRegExam.DakeDonthuoc >= 1)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc. Yêu cầu xóa đơn thuốc trước khi xóa công khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    SqlQuery q =
                        new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(
                            objRegExam.IdKham);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS. Yêu cầu xóa chỉ định CLS trước khi xóa công khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objRegExam.IdKham);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc. Yêu cầu xóa đơn thuốc trước khi xóa công khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                }

            }
            return true;
        }
        private void frm_KCB_DSACH_BNHAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F1) tabChiDinh.SelectedTab = Tabpagedangky;
            if (e.KeyCode == Keys.F2) tabChiDinh.SelectedTab = tabPageChiDinh;
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdSuaThongTinBN.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoaBenhNhan.PerformClick();
            if (e.KeyCode == Keys.K && e.Control) cmdThemLanKham.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaBenhNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để xóa");
                    return;
                }
                string ErrMgs = "";
                string v_MaLuotkham =
                   Utility.sDbnull(
                     grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                       "");
                int v_Patient_ID =
                     Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);

                if (!IsValidDeleteData()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin lần khám này không", "Thông báo", true))
                {
                    myTrace.FunctionID = globalVariables.FunctionID;
                    myTrace.FunctionName = globalVariables.FunctionName;
                    ActionResult actionResult = _KCB_DANGKY.PerformActionDeletePatientExam(myTrace, v_MaLuotkham,
                                                                                                       v_Patient_ID, ref ErrMgs);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa bệnh nhân ID={0}, PID={1}, Tên={2} ", v_Patient_ID.ToString(), v_MaLuotkham,  Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan))), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            grdList.CurrentRow.Delete();
                            grdList.UpdateData();
                            grdList_SelectionChanged(grdList, e);
                            m_dtPatient.AcceptChanges();
                            UpdateGroup();
                           
                            //Utility.ShowMsg("Xóa lần khám thành công", "Thành công");
                            break;
                        case ActionResult.Exception:
                            if (ErrMgs != "")
                                Utility.ShowMsg(ErrMgs);
                            else
                                Utility.ShowMsg("Bệnh nhân đã có thông tin chỉ định dịch vụ hoặc đơn thuốc... /n bạn không thể xóa lần khám này", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin", "Thông báo");
                            break;
                    }
                }
                ModifyButtonCommandRegExam();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }
        void ResetNhominCLS()
        {
            try
            {
                if (grdAssignDetail.GetDataRows().Length <= 0) return;
                var nhomcls = new List<string>();
                foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
                {
                    if (!nhomcls.Contains(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value)))
                    {
                        nhomcls.Add(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value));
                    }
                }
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung(globalVariables.DC_NHOMIN_CLS, true);
                DataTable dttempt = dtNhomin.Clone();
                foreach (DataRow dr in dtNhomin.Rows)
                    if (nhomcls.Contains(Utility.sDbnull(dr[DmucChung.Columns.Ma], "")))
                        dttempt.ImportRow(dr);
                DataBinding.BindDataCombobox(cboServicePrint, dttempt, DmucChung.Columns.Ma, DmucChung.Columns.Ten,
                                                          "Tất cả", true);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        private bool IsValidDeleteData()
        {

            string v_MaLuotkham =
             Utility.sDbnull(
               grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                 "");
            int v_Patient_ID =
                 Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
            if (objLuotkham != null)
            {
                if (objLuotkham.TrangthaiNgoaitru > 0)
                {
                    Utility.ShowMsg("Bệnh nhân đang chọn đã thực hiện khám ngoại trú nên bạn không được phép xóa");
                    return false;
                }
                if (objLuotkham.TrangthaiNoitru > 0)
                {
                    Utility.ShowMsg("Bệnh nhân đang chọn đã nhập viện nội trú nên bạn không được phép xóa");
                    return false;
                }
            }
            SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
                .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bản ghi trong đăng ký khám đã thanh toán, Bạn không xóa được");
                return false;
            }
            sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(
                    new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema).Where(
                        KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham).And(KcbChidinhcl.Columns.IdBenhnhan).
                        IsEqualTo(v_Patient_ID))
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bạn không thể xóa bệnh nhân trên vì bệnh nhân đã thanh toán một số dịch vụ cận lâm sàng", "Thông báo");
                return false;
            }
            sqlQuery = new Select().From(KcbDonthuoc.Schema)
                .Where(KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham).And(KcbDonthuoc.Columns.IdBenhnhan).
                        IsEqualTo(v_Patient_ID)
                .And(KcbDonthuoc.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bạn không thể xóa bệnh nhân trên vì bệnh nhân đã có đơn thuốc được thanh toán", "Thông báo");
                return false;
            }
            sqlQuery = new Select().From(KcbThanhtoan.Schema)
                .Where(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                .And(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã có hóa đơn. Mời bạn qua hủy hóa đơn trước khi thực hiện xóa bệnh nhân", "Thông báo");
                return false;
            }
            return true;
        }
        /// <summary>
        /// ham thực hiện viecj in phiếu cỉnh định
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuChiDinh_Click(object sender, EventArgs e)
        {
            if (grdAssignDetail.CurrentRow != null)
            {
                int v_AssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                if (objAssignInfo != null)
                {
                    frm_INPHIEU_CLS frm = new frm_INPHIEU_CLS();
                    frm.objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                    frm.ShowDialog();
                }

            }

        }
        /// <summary>
        /// hàm thực hiện việc chọn kiểu khám bệnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboKieuKham_ValueChanged(object sender, EventArgs e)
        {
            if (cboKieuKham.SelectedIndex >= 0)
            {
                DmucDichvukcb objDichvuKCB =
                    DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                if (objDichvuKCB != null)
                {
                    Utility.SetMsg(lblDonGia, Utility.sDbnull(objDichvuKCB.DonGia), true);
                    Utility.SetMsg(lblPhuThu, Utility.sDbnull(objDichvuKCB.PhuthuDungtuyen), true);
                }
                else
                {
                    Utility.SetMsg(lblDonGia, Utility.sDbnull(0), true);
                    Utility.SetMsg(lblPhuThu, Utility.sDbnull(0), true);
                }
            }
        }
        /// <summary>
        /// hàm thực hiện viec lọc thông in trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void grdList_AddingRecord(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void lblDonGia_Click(object sender, EventArgs e)
        {

        }

        private void lblDonGia_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(lblDonGia);
        }

        private void lblPhuThu_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(lblPhuThu);
        }

        private void grdRegExam_SelectionChanged(object sender, EventArgs e)
        {
            ModifyButtonCommandRegExam();
        }

        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowSelectionChanged) return;
            ModifycommandAssignDetail();
            ResetNhominCLS();
            ShowResult();
            pnlAttachedFiles.Height = Utility.isValidGrid(grdAssignDetail) ? 60 : 0;
        }

        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatientCode.Text.Trim()) != "")
                {
                    string _ID = txtPatient_ID.Text.Trim();
                    string patient_ID = Utility.GetYY(DateTime.Now) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPatientCode.Text, 0), "000000");
                    txtPatient_ID.Clear();
                    txtPatientCode.Text = patient_ID;
                    TimKiemThongTin(false);
                    if (grdList.RowCount == 1) grdList_SelectionChanged(grdList, new EventArgs());
                    txtPatient_ID.Text = _ID;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                //throw;
            }
        }

        private void txtPatient_ID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatient_ID.Text.Trim()) != "")
                {
                    string _code = txtPatientCode.Text.Trim();
                    txtPatientCode.Clear();
                    TimKiemThongTin(false);
                    if (grdList.RowCount == 1) grdList_SelectionChanged(grdList, new EventArgs());
                    txtPatientCode.Text = _code;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                //throw;
            }
        }



        private void cmdCauhinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            CauHinh();
        }

        private void mnuUpdateMalankham_Click(object sender, EventArgs e)
        {
            VNS.HIS.UI.Forms.Dungchung.frmUpdateMaLanKham frm = new frmUpdateMaLanKham();
            frm.txtmabenhnhancu.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
            frm.txtidbenhnhancu.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
            frm.ShowDialog();
        }

        private void mnuUpdateMaBenhAn_Click(object sender, EventArgs e)
        {
            var frm = new frmUpdateMaBenhAn();
            frm.txtmalankham.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
            frm.txtidbenhnhancu.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
            frm.ShowDialog();
        }

        private void mnuUpdateMaBenhNhan_Click(object sender, EventArgs e)
        {
            var frm = new frmUpdateMaBenhNhan();
            frm.txtmabenhnhancu.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
            frm.ShowDialog();
        }

        private void cmdNhapVien_Click(object sender, EventArgs e)
        {
            if (objLuotkham.TrangthaiNoitru > 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú nên bạn chỉ có thể xem và không được phép sửa các thông tin thăm khám");
                return;
            }
            //Kiểm tra xem có đơn thuốc ngoại trú hay không
            string errMsg = "";
            StoredProcedure v_sp = SPs.KcbKiemtradieukiennhapvien(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, errMsg);
            v_sp.Execute();
            errMsg = Utility.sDbnull(v_sp.OutputValues[0]);
            if (Utility.DoTrim(errMsg).Length > 0)
            {
                Utility.ShowMsg(errMsg);
                return;
            }
            var frm = new frm_Nhapvien();
            frm.CallfromParent = true;
            frm.id_kham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells["id_kham"].Value);
            frm.objLuotkham = objLuotkham;
            frm.ShowDialog();
            if (frm.b_Cancel)
            {
                TimKiemThongTin(true);
            }
        }

        private void mnuupdatengaykham_Click(object sender, EventArgs e)
        {
            var frm = new FrmUpdateNgaykham(Utility.sDbnull(grdList.CurrentRow.Cells["ma_luotkham"].Value));
            frm.ShowDialog();
        }

        private void cmdInphieuravien_Click(object sender, EventArgs e)
        {
            try
            {
                string maLankham = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
                Utility.WaitNow(this);
                DataTable dtData =
                    SPs.NoitruInphieuravien(Utility.DoTrim(maLankham)).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_phieuravien.XML");
                Utility.UpdateLogotoDatatable(ref dtData);
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport("noitru_phieuravien", ref tieude, ref reportname);
                if (crpt == null) return;
                var objForm = new frmPrintPreview(tieude, crpt, true,
                    dtData.Rows.Count > 0);
                crpt.SetDataSource(dtData);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_phieuravien";
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate",
                    Utility.FormatDateTimeWithThanhPho(Convert.ToDateTime(dtData.Rows[0]["ngay_ravien"].ToString())));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                          Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
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

        private void chkIntach_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntach.Checked)
            {
                File.WriteAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt", "1");
            }
            else
            {
                File.WriteAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt", "0");
            }
        }

        void LoadHTML()
        {
            string noidung = txtNoiDung.Text;
            webBrowser1.Document.InvokeScript("setValue", new[] { noidung });
        }
        DmucVungkhaosat vks = null;
        void ShowEditor(int id_VungKS)
        {
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
        private void ShowResult()
        {
            try
            {
                return;
                pnlXQ.Width = 0;
                //mnuNhapKQCDHA.Visible = false;
                LoadAttachedFiles();
                if (!PropertyLib._KCBProperties.ResultInput) return;
                //mnuNhapKQCDHA.Visible = false;
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

                int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                int idChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                string ketluanHa =
                  Utility.sDbnull(
                      Utility.GetValueFromGridColumn(grdAssignDetail, "ketluan_ha"), "");
                string maloaiDichvuCls =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                string maBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham),
                                    "XN");
                if (trangthaiChitiet <= 2)
                //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    if (maloaiDichvuCls != "XN")
                    {
                        pnlXQ.Width = 0;
                        //mnuNhapKQCDHA.Visible = true;
                    }
                    else
                    {
                    }

                    Application.DoEvents();
                }
                else
                {
                    
                    if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
                    {
                        if (coChitiet == 1 || maloaiDichvuCls != "XN")
                            pnlXQ.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                        else
                            pnlXQ.Width = 0;
                    }
                    else
                    {
                        pnlXQ.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                    }
                    //Utility.ShowColumns(grdKetqua, coChitiet == 1 ? lstKQCochitietColumns : lstKQKhongchitietColumns);
                    //Lấy dữ liệu CLS
                    if (maloaiDichvuCls == "XN")
                    {
                        
                        
                    }
                    else //XQ,SA,DT,NS
                    {
                        //mnuNhapKQCDHA.Visible = true;
                        ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_VungKS"), 0));
                    }
                    Application.DoEvents();
                }
               
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        void LoadAttachedFiles()
        {
            try
            {
                flowAttachedFiles.Controls.Clear();
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
                    flowAttachedFiles.Controls.Add(_file);
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
        private void cmdFileAttach_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdAssignDetail))
            {
                Utility.ShowMsg("Cần có phiếu chỉ định mới có thể thực hiện thêm các File kết quả đính kèm", "Thông báo");
                return;
            }
            int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
            string MaChidinh = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), "xxx");
            OpenFileDialog _openfile = new OpenFileDialog();
            _openfile.Multiselect = true;
            if (_openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (_openfile.FileNames.Count() >= 5)
                {
                    if (!Utility.AcceptQuestion(string.Format("Bạn đang chọn nhiều ({0}) file kết quả cho phiếu chỉ định này. Bạn có chắc chắn?", _openfile.FileNames.Count().ToString()), "Cảnh báo", true))
                    {
                        return;
                    }
                }
                foreach (string sfile in _openfile.FileNames)
                {
                    byte[] file;
                    using (var stream = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            file = reader.ReadBytes((int)stream.Length);
                        }
                    }

                    //TblFiledinhkem _file = new Select().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.IdChidinh).IsEqualTo(idChidinh).ExecuteSingle<TblFiledinhkem>();
                    //if (_file == null)
                    //{
                    TblFiledinhkem _file = new TblFiledinhkem();
                    _file.NguoiTao = globalVariables.UserName;
                    _file.NgayTao = globalVariables.SysDate;
                    _file.IsNew = true;
                    //}
                    //else
                    //{
                    //    _file.IsNew = false;
                    //    _file.MarkOld();
                    //}
                    _file.FileName = Path.GetFileName(sfile);
                    _file.IdChidinh = idChidinh;
                    _file.FileData = file;
                    _file.Save();

                }
                LoadAttachedFiles();
            }

        }

        private void mnuDelFile_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a MenuItem
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    Label sourceControl = owner.SourceControl as Label;
                    int id = Utility.Int32Dbnull(sourceControl.Text.Split('-')[0], 0);
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa file đính kèm vừa chọn?", "Xác nhận xóa", true))
                    {
                        new Delete().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.Id).IsEqualTo(id).Execute();
                        LoadAttachedFiles();
                    }
                }

                // Get the control that is displaying this context menu

            }
        }
        public FTPclient FtpClient;
        public string _FtpClientCurrentDirectory;
        private string _baseDirectory = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Radio\\");
        private void InitFtp()
        {
            try
            {

                FtpClient = new FTPclient(PropertyLib._HinhAnhProperties.FTPServer, PropertyLib._HinhAnhProperties.UID, PropertyLib._HinhAnhProperties.PWD);
                FtpClient.UsePassive = true;
                _FtpClientCurrentDirectory = FtpClient.CurrentDirectory;
                _baseDirectory = Utility.DoTrim(PropertyLib._HinhAnhProperties.ImageFolder);
                if (_baseDirectory.EndsWith(@"\")) _baseDirectory = _baseDirectory.Substring(0, _baseDirectory.Length - 1);
                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }
            }
            catch
            {
            }
        }
        private void BeginExam()
        {
            if (!PropertyLib._KCBProperties.ResultInput) return;
            int v_id_chitietchidinh = -1;
            try
            {
                if (PropertyLib._HinhAnhProperties == null) PropertyLib._HinhAnhProperties = PropertyLib.GetHinhAnhProperties();
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    if (grdAssignDetail.CurrentRow != null && grdAssignDetail.CurrentRow.RowType == RowType.Record)
                    {
                        v_id_chitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietchidinh"), -1);
                        if (Utility.DoTrim(Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value, "")) == "")
                        {
                            Utility.ShowMsg("Dịch vụ này chưa gán vùng khảo sát nên không thể nhập kết quả.\nNhấn OK để thực hiện gán vùng khảo sát cho dịch vụ đang chọn");
                            frm_chonvungksat _chonvungks = new frm_chonvungksat(new List<string>());
                            _chonvungks.Hthi_Chon = true;
                            _chonvungks.ten_dvu = Utility.GetValueFromGridColumn(grdAssignDetail, "ten_chitietdichvu");
                            if (_chonvungks.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                if (Utility.DoTrim(_chonvungks.vungks).Length > 0)
                                {
                                    grdAssignDetail.CurrentRow.BeginEdit();
                                    grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value = _chonvungks.vungks;
                                    grdAssignDetail.CurrentRow.EndEdit();
                                    grdAssignDetail.UpdateData();
                                    grdAssignDetail.Refetch();
                                    DmucDichvuclsChitiet objDvu = DmucDichvuclsChitiet.FetchByID(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietdichvu"));
                                    if (objDvu != null)
                                    {
                                        objDvu.DsachVungkhaosat = _chonvungks.vungks;
                                        objDvu.Save();
                                        m_dtChiDinhCLS.AsEnumerable().Where(c => c.Field<Int32>("id_chitietdichvu") == Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietdichvu"))).ToList().ForEach(x1 => { x1["dsach_vungkhaosat"] = _chonvungks.vungks; });
                                    }
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("Bạn vừa hủy chọn vùng khảo sát nên không thể nhập kết quả");
                                return;
                            }

                        }
                        DataRowView dr = grdAssignDetail.CurrentRow.DataRow as DataRowView;
                        Utility.SetMsg(lblMsg, "Đang nạp thông tin bệnh nhân...", false);
                        if (FtpClient != null) FtpClient.CurrentDirectory = _FtpClientCurrentDirectory;
                        int Status = Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells["trang_thai"].Value, -1);
                        if (Status <= 2)
                        {
                            new KCB_HinhAnh().UpdateXacNhanDaThucHien(v_id_chitietchidinh, 2);
                        }
                        int id_VungKS = Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells["id_VungKS"].Value, -1);
                        if (id_VungKS > 0)
                        {
                            goto _EnterResult;
                        }
                        List<string> lstID = new List<string>();
                        lstID = Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value, "-1").Split(',').ToList<string>();
                        if (lstID.Count >= 2)
                        {
                            frm_chonvungksat _chonvungksat = new frm_chonvungksat(lstID);
                            if (_chonvungksat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                id_VungKS = _chonvungksat.Id;
                            else
                                return;
                        }
                        else
                            if (lstID.Count > 0)
                                id_VungKS = Utility.Int32Dbnull(lstID[0], -1);
                    _EnterResult:
                        frm_NhaptraKQ frm = new frm_NhaptraKQ();
                        DataTable dtWorkList = SPs.HinhanhTimkiembnhanTheoIDchidinhchitiet(v_id_chitietchidinh).GetDataSet().Tables[0];
                        frm.drWorklistDetail = (((DataRowView)grdAssignDetail.CurrentRow.DataRow).Row);
                        frm.drWorklist = dtWorkList.Rows[0];
                        if (frm.FtpClient == null) frm.FtpClient = this.FtpClient;
                        frm.ID_Study_Detail = v_id_chitietchidinh;
                        frm.lstID = Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value, "-1").Split(',').ToList<string>();
                        frm.id_VungKS = id_VungKS;
                        frm.StrServiceCode = Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["ma_loaidichvu"].Value);
                        frm.ShowDialog();
                        if (!frm.mv_blnCancel)
                        {
                            grdAssignDetail_SelectionChanged(grdAssignDetail, new EventArgs());
                        }
                        frm.Dispose();
                        frm = null;
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                Utility.SetMsg(lblMsg, "Mời bạn tiếp tục làm việc...", false);
                this.Cursor = Cursors.Default;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNoiDung.Text)) ClickData();
        }
        private void ClickData()
        {
            webBrowser1.Document.InvokeScript("setValue", new[] { txtNoiDung.Text });
            timer1.Stop();
        }

        private void cmdIntachPhieu_Click(object sender, EventArgs e)
        {
            ChoninphieuCLS(false);
        }
        void ChoninphieuCLS(bool inchung)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn người bệnh trước khi thực hiện các chức năng in phiếu chỉ định");
                return;
            }
            frm_InphieuCLS _InphieuCLS = new frm_InphieuCLS(m_dtChiDinhCLS, Utility.getUserConfigValue("CHIDINHCLS_TUDONGCHONCACPHIEUKHI_INTACH", (byte)1) == 1);
            _InphieuCLS.objLuotkham = this.objLuotkham;
            if (!inchung)
                _InphieuCLS.ShowDialog();
            else
            {
                _InphieuCLS.InChungphieu();
            }
        }

        private void cmdInchung_Click(object sender, EventArgs e)
        {
            ChoninphieuCLS(true);
        }
        internal static IntPtr hWnd;
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern void SendMessageW(IntPtr hWnd, uint msg, uint wParam, uint lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowW(string className, string windowName);
        internal static Process process;
        private void mnuFingerRegister_Click(object sender, EventArgs e)
        {
            RegisterFinger();  
        }

        private void cmdFingerPrint_Click(object sender, EventArgs e)
        {
            RegisterFinger();
        }
        void RegisterFinger()
        {
            try
            {
                string patientID = Utility.sDbnull(grdList.CurrentRow.Cells["id_benhnhan"].Value, "");
                if (Utility.Int32Dbnull(patientID, -1) > 0)
                {
                    List<string> _list = new List<string>();
                    _list.Add(patientID.ToString());
                    _list.Add(1.ToString());
                    string sPatientInforFile = Application.StartupPath + @"\IVF_FR\PatientInfor.txt";
                    string appName = Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe";
                    if (File.Exists(sPatientInforFile))
                    {
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    else
                    {
                        File.CreateText(sPatientInforFile);
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    Utility.KillProcess(appName);
                    Thread.Sleep(100);
                    process = Process.Start(Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe");
                    if (process != null) process.WaitForExit();
                    WaitForSingleObject(process.Handle, 0xffffffff);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

    }
}
