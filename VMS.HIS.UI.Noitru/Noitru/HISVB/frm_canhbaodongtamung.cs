using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using System.Collections.Generic;
using VNS.HIS.UI.Classess;
using System.Reflection;
using VNS.Properties;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_canhbaodongtamung : Form
    {
        KcbLuotkham objLuotkham = null;
        private NLog.Logger log;
        bool mv_blnHasloaded = false;
        string SplitterPath = "";
        public frm_canhbaodongtamung()
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            log = LogManager.GetCurrentClassLogger();
            dtToDate.Value = dtFromDate.Value = THU_VIEN_CHUNG.GetSysDateTime();
            cboCondition.SelectedIndex = 0;
            txtSotientu.TextChanged += txtSotientu_TextChanged;
            txtSotienden.TextChanged += txtSotienden_TextChanged;
            grdList.SelectionChanged += GrdList_SelectionChanged;
            optAll_0.CheckedChanged += optAll_CheckedChanged;
            optNoitru_0.CheckedChanged += optAll_CheckedChanged;
            optNgoaitru_0.CheckedChanged += optAll_CheckedChanged;
            this.FormClosing += Frm_canhbaodongtamung_FormClosing;
            this.Shown += Frm_canhbaodongtamung_Shown;
           Utility.AutoBindCheckedComboBox(cboNgaydieutri);
        }
       
        void Try2Splitter()
        {
            try
            {


                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null)
                {
                    if (lstSplitterSize.Count > 0) splitContainer1.SplitterDistance = lstSplitterSize[0];
                }
            }
            catch (Exception)
            {

            }
        }
        private void Frm_canhbaodongtamung_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }

        private void Frm_canhbaodongtamung_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString()});
        }

        void optAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!mv_blnHasloaded) return;
                string RowFilter = "1=1";
                PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Tatca;
                if (optNoitru_0.Checked)
                {
                    PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Noitru;
                    RowFilter = "noi_tru=1";
                }
                if (optNgoaitru_0.Checked)
                {
                    PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Ngoaitru;
                    RowFilter = "noi_tru=0";
                }
                m_dtChiPhiThanhtoan.DefaultView.RowFilter = RowFilter;
                m_dtChiPhiThanhtoan.AcceptChanges();
                PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
            }
            catch (Exception ex)
            {


            }
        }
        private void GrdList_SelectionChanged(object sender, EventArgs e)
        {
            ShowHideMienGiam();
            objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
            LoadData();
            mv_blnHasloaded = true;
        }
        void ShowHideMienGiam()
        {
            bool MIENGIAM_CHOPHEPTHUCHIEN_TONGHOPCHIPHI_KCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("MIENGIAM_CHOPHEPTHUCHIEN_TONGHOPCHIPHI_KCB", "1", true) == "1";
            if (MIENGIAM_CHOPHEPTHUCHIEN_TONGHOPCHIPHI_KCB)
            {
               
                grdThongTinChuaThanhToan.RootTable.Columns["tile_chietkhau"].Visible = false;
                grdThongTinChuaThanhToan.RootTable.Columns["tien_chietkhau"].Visible = false;
            }

        }
        DataTable m_dtChiPhiThanhtoan = new DataTable();
        string idkhoanoitru = "-1";
        bool Khoanoitrutonghop = false;
        void LoadData()
        {
            try
            {
               if(objLuotkham==null)
                {
                    grdThongTinChuaThanhToan.DataSource = null;
                    return;
                }
               
                m_dtChiPhiThanhtoan =
                   SPs.NoitruTonghopChiphiRavien(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, Utility.Bool2byte(!Khoanoitrutonghop), idkhoanoitru, getidphieudieutri(),0).GetDataSet().Tables[0];
                // Utility.SetDataSourceForDataGridEx_Basic(grdThongTinChuaThanhToan, m_dtChiPhiThanhtoan,true, true, "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : ""), "");
                Utility.SetDataSourceForDataGridEx_Basic(grdThongTinChuaThanhToan, m_dtChiPhiThanhtoan, true, true, "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : ""), "");
                m_dtChiPhiThanhtoan.TableName = "tonghopchiphi_kcb";
                THU_VIEN_CHUNG.CreateXML(m_dtChiPhiThanhtoan, string.Format("tonghopchiphi_kcb.xml" ));
                var q = (from p in m_dtChiPhiThanhtoan.AsEnumerable()
                         select new { id_khoadieutri = Utility.sDbnull(p["id_khoadieutri"]), ten_khoadieutri = Utility.sDbnull(p["ten_khoadieutri"]) }).Distinct();
                DataTable dtKhoadieutri = LINQResultToDataTable(q);
                DataBinding.BindDataCombobox(cboKhoadieutri, dtKhoadieutri, "id_khoadieutri", "ten_khoadieutri", "Chọn khoa điều trị", true);
                if (cboKhoadieutri.Items.Count == 1) cboKhoadieutri.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        string getidphieudieutri()
        {
            string lstIdphieudieutri = "-1";
            if (cboNgaydieutri.CheckedValues != null)
            {
                var query = (from chk in cboNgaydieutri.CheckedValues.AsEnumerable()
                             let x = Utility.sDbnull(chk)
                             select x).ToArray();
                if (query != null && query.Count() > 0)
                {
                    lstIdphieudieutri = string.Join(",", query);
                }
                else
                    lstIdphieudieutri = "-1";
            }
            return lstIdphieudieutri;
        }
        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }
        void txtSotienden_TextChanged(object sender, EventArgs e)
        {
            getDieukienchenhlech();
        }

        void txtSotientu_TextChanged(object sender, EventArgs e)
        {
            getDieukienchenhlech();
        }

        //public frm_canhbaodongtamung(string sArg)
        //{
        //    InitializeComponent();
        //    this.thamso = sArg;
        //    log = LogManager.GetCurrentClassLogger();
        //    dtToDate.Value = dtFromDate.Value = THU_VIEN_CHUNG.GetSysDateTime();
        //    cboCondition.SelectedIndex = 0;
        //}
        
        private void frm_canhbaodongtamung_Load(object sender, EventArgs e)
        {
            InitData();
            SearchthongTin();
            ModifyCommand();
        }
        private void InitData()
        {
            try
            {
                DataTable mDtKhoaNoitru = THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName,Utility.Bool2byte( globalVariables.IsAdmin),(byte)1);
                txtKhoanoitru.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
                if (Utility.Coquyen("tamung_canhbao_chonkhoa"))
                    txtKhoanoitru.Enabled = true;
                else
                {
                   
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh khoi tao khoa noi tru =" + exception);
            }
        }
        /// <summary>
        /// hàm thực hiện việc tìm kiesm thông tin của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if ((Utility.sDbnull( cboCondition.SelectedValue,"") == "0" || Utility.sDbnull( cboCondition.SelectedValue,"") == "1") && Utility.sDbnull(txtSotientu.Text,"").Length<=0)
            {
                Utility.ShowMsg("Bạn cần nhập thông tin số tiền chênh lệnh giữa Tổng tạm ứng - Tổng chi phí");
                txtSotienden.Focus();
                return;
            }
            if (Utility.sDbnull(cboCondition.SelectedValue, "") == "2" && Utility.sDbnull(txtSotientu.Text, "").Length <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập thông tin số tiền chênh lệnh từ (Tổng tạm ứng - Tổng chi phí)");
                txtSotienden.Focus();
                return;
            }
            if (Utility.sDbnull(cboCondition.SelectedValue, "") == "2" && Utility.sDbnull(txtSotienden.Text, "").Length <= 0)
            {
                Utility.ShowMsg("Bạn cần nhập thông tin số tiền chênh lệnh đến (Tổng tạm ứng - Tổng chi phí)");
                txtSotienden.Focus();
                return;
            }
            SearchthongTin();
        }

        private DataTable m_dtDataSearch = new DataTable();
        private void ModifyCommand()
        {
            cmdIn.Enabled = grdList.RowCount > 0;
        }
        private DataTable m_dtCanhBaoGoiDv=new DataTable();
        private void SearchthongTin()
        {
            int Status = -1;
            byte noitru=100;
            if(optNoitru.Checked) noitru=1;
            if(optNgoaitru.Checked) noitru=0;
            m_dtDataSearch =
                SPs.NoitruCanhbaoTamung(chkCreateDate.Checked ? dtFromDate.Value
                                                           : Convert.ToDateTime("01/01/1900"),
                                                           chkCreateDate.Checked ? dtToDate.Value : Convert.ToDateTime("01/01/1900"), txtPatientCode.Text, -1, txtTenBN.Text,
                                                           Utility.Bool2byte(chkTinhngoaitru.Checked),Utility.Int32Dbnull( txtKhoanoitru.MyID,-1),Utility.sDbnull( cboCondition.SelectedValue,"-1")
                                                           , Utility.DecimaltoDbnull(txtSotientu.Text), Utility.DecimaltoDbnull(txtSotienden.Text), noitru).GetDataSet().Tables[0];
         
            Utility.SetDataSourceForDataGridEx(grdList,m_dtDataSearch,true,true,"1=1","");
            ModifyCommand();
        }
        string getDieukienchenhlech()
        {
            if (Utility.sDbnull(cboCondition.SelectedValue, "") == "-1") return "Tất cả";
            else if (Utility.sDbnull(cboCondition.SelectedValue, "") == "0") return string.Format("Tổng tiền tạm ứng -Tổng chi phí >={0}", txtSotientu.Text);
            else if (Utility.sDbnull(cboCondition.SelectedValue, "") == "1") return string.Format("Tổng tiền tạm ứng -Tổng chi phí <={0}", txtSotientu.Text);
            else return string.Format("Tổng tiền tạm ứng -Tổng chi phí trong khoảng:{0}-{1}",txtSotientu.Text,txtSotienden.Text);

        }
        private void cmdINDANHSACH_Click(object sender, EventArgs e)
        {
            try
            {


                THU_VIEN_CHUNG.CreateXML(m_dtDataSearch, "noitru_canhbaotientamung.xml");
                if (m_dtDataSearch == null || m_dtDataSearch.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu để in", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string Condition = string.Format("{0} - Khoa điều trị : {1} - Chênh lệch :{2}", chkCreateDate.Checked ? string.Format("") : "Từ ngày đến ngày: Tất cả", txtKhoanoitru.MyID == "-1" ? "Tất cả" : txtKhoanoitru.Text, getDieukienchenhlech());
                                       
                noitru_inphieu.InPhieu(m_dtDataSearch, DateTime.Now, Condition,true, "noitru_canhbaotientamung");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_canhbaodongtamung_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.F3)cmdSearch.PerformClick();
            if(e.KeyCode==Keys.F4)cmdIn.PerformClick();
        }

        private void radTatCa_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void uiTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void uiButton2_Click(object sender, EventArgs e)
        {
            string TNDN = string.Format("Từ ngày {0} đến ngày {1}", dtFromDate.Text, dtToDate.Text);
            if(m_dtCanhBaoGoiDv.Rows.Count<=0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi nào","Thông báo",MessageBoxIcon.Warning);
                return;
            }
          //  VietBaIT.HISLink.Reports_TAMPHUC.INPHIEU_CLASS.INPHIEU.TAMPHUC_INDANHSACH_CANHBAO_GOI_DVU(m_dtCanhBaoGoiDv, txtTIEUDE_GOIDV.Text, TNDN);
        }

        private void mnuInTach_Click(object sender, EventArgs e)
        {
            //string PatientCode = Utility.sDbnull(grdList.CurrentRow.Cells[TPatientExam.Columns.PatientCode].Value);
            ////string PatientName = Utility.sDbnull(grdListBinhThuong.CurrentRow.Cells[TPatientInfo.Columns.PatientName].Value);
            ////string PatientAddr = Utility.sDbnull(grdListBinhThuong.CurrentRow.Cells[TPatientInfo.Columns.PatientAddr].Value);
         
            ////string Year_Of_Birth = Utility.sDbnull(grdListBinhThuong.CurrentRow.Cells[TPatientInfo.Columns.YearOfBirth].Value);
            ////string PatientSex = Utility.sDbnull(grdListBinhThuong.CurrentRow.Cells[TPatientInfo.Columns.PatientSex].Value);


            //DataTable  m_dtInTachCanhBao =
            //  SPs.TamphucTimkiemDanhsachBnhanCanhbao(string.Empty,
            //                                         chkCreateDate.Checked
            //                                             ? dtFromDate.Value
            //                                             : Convert.ToDateTime("01/01/1900"),
            //                                         chkCreateDate.Checked
            //                                             ? dtToDate.Value
            //                                             : BusinessHelper.GetSysDateTime(),
            //                                             Utility.Int32Dbnull(cboKhoa.SelectedValue, -1), Utility.sDbnull(cboCondition.SelectedValue),
            //                                                              Utility.DecimaltoDbnull(txtSoTien.Text, 0), Utility.sDbnull(PatientCode,"-1")).GetDataSet().
            //      Tables[0];
            //VietBaIT.HISLink.Business.Reports.Implementation.BC_Inphieu_KyHoanQui.InTachCanhBaoKyQui(m_dtInTachCanhBao);

        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
                    return;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", "Cảnh báo tạm ứng chi phí điều trị nội trú");
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    var fs = new FileStream(sPath, FileMode.Create);
                    fs.CanWrite.CompareTo(true);
                    fs.CanRead.CompareTo(true);
                    gridEXExporter1.Export(fs);
                    fs.Dispose();
                }
                saveFileDialog1.Dispose();
                saveFileDialog1.Reset();
            }
            catch (Exception exception)
            {
            }
        }

        private void cboCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSotienden.Enabled = Utility.sDbnull(cboCondition.SelectedValue, "-1") == "2";
            Utility.SetMsg(lblDiengiaiChenhlech, getDieukienchenhlech(), false);
        }

        private void cboKhoadieutri_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoadieutri.SelectedValue.ToString() == "-1")
                    m_dtChiPhiThanhtoan.DefaultView.RowFilter = "1=1";
                else
                    m_dtChiPhiThanhtoan.DefaultView.RowFilter = "id_khoadieutri=" + cboKhoadieutri.SelectedValue.ToString();
                m_dtChiPhiThanhtoan.AcceptChanges();
                LoadNgayDieutri();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void LoadNgayDieutri()
        {
            try
            {
                DataTable m_dtPhieudieutri = SPs.NoitruTimkiemphieudieutriTheoKhoadieutri(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, Utility.sDbnull(cboKhoadieutri.SelectedValue, "-1")).GetDataSet().Tables[0];
                cboNgaydieutri.DropDownDataSource = m_dtPhieudieutri;
                cboNgaydieutri.DropDownDisplayMember = "sNgay_dieutri";
                cboNgaydieutri.DropDownDataMember = NoitruPhieudieutri.Columns.IdPhieudieutri;
                cboNgaydieutri.DropDownValueMember = NoitruPhieudieutri.Columns.IdPhieudieutri;
            }
            catch (Exception)
            {


            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                DataTable dtData = SPs.NoitruTonghopChiphiRavien(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, Utility.Bool2byte(!Khoanoitrutonghop), Utility.sDbnull(cboKhoadieutri.SelectedValue, "-1"), getidphieudieutri(),0).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                    return;
                }
                DataTable dtDataPrint = dtData.Clone();
                DataRow[] arrDr = null;
                if (optAll1.Checked)
                {
                    dtDataPrint = dtData.Copy();
                }
                else if (optNoitru1.Checked)
                {
                    arrDr = dtData.Select("Noi_tru=1");
                    if (arrDr.Length > 0) dtDataPrint = arrDr.CopyToDataTable();
                }
                else if (optNgoaitru1.Checked)
                {
                    arrDr = dtData.Select("Noi_tru=0");
                    if (arrDr.Length > 0) dtDataPrint = arrDr.CopyToDataTable();
                }
                Utility.UpdateLogotoDatatable(ref dtDataPrint);


                ReportDocument reportDocument = new ReportDocument();
                string tieude = "", reportname = "", reportCode = "";
                reportCode = "thanhtoan_bangkechiphiKCB_Noitru_Tonghop";

                reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);

                if (reportDocument == null) return;
                var crpt = reportDocument;

                decimal tt_bnhan_chitra = Utility.DecimaltoDbnull(dtDataPrint.Rows[0]["tong_tien"]) - Utility.DecimaltoDbnull(dtDataPrint.Rows[0]["tong_tien_tamung"]) - Utility.DecimaltoDbnull(dtDataPrint.Rows[0]["tong_tien_mg"]);
                var objForm = new frmPrintPreview("", crpt, true, true);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                //try
                //{
                crpt.SetDataSource(dtDataPrint.DefaultView);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithLocation(DateTime.Now, globalVariables.gv_strDiadiem));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                       new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tt_bnhan_chitra)));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();

                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
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

        private void uiButton1_Click(object sender, EventArgs e)
        {
            InChitiet();
        }
        void InChitiet()
        {
            try
            {
                Utility.WaitNow(this);
                DataTable dtData = SPs.NoitruTonghopChiphiRavien(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, Utility.Bool2byte(!Khoanoitrutonghop), Utility.sDbnull(cboKhoadieutri.SelectedValue, "-1"), getidphieudieutri(),0).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.Sapxepthutuin(ref dtData, false);
                dtData.DefaultView.Sort = "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

                THU_VIEN_CHUNG.CreateXML(dtData, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV_chuathanhtoan.XML");
                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                    return;
                }
                DataTable dtDataPrint = dtData.Clone();
                DataRow[] arrDr = null;
                if (optAll1.Checked)
                {
                    dtDataPrint = dtData.Copy();
                }
                else if (optNoitru1.Checked)
                {
                    arrDr = dtData.Select("Noi_tru=1");
                    if (arrDr.Length > 0) dtDataPrint = arrDr.CopyToDataTable();
                }
                else if (optNgoaitru1.Checked)
                {
                    arrDr = dtData.Select("Noi_tru=0");
                    if (arrDr.Length > 0) dtDataPrint = arrDr.CopyToDataTable();
                }

                Utility.UpdateLogotoDatatable(ref dtDataPrint);
                dtDataPrint.DefaultView.Sort = "stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
                dtDataPrint.AcceptChanges();
                var p = (from q in dtDataPrint.AsEnumerable()
                         group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                         select new
                         {
                             _key = r.Key,
                             tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                             tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                             tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                         }).ToList();

                decimal tong = Utility.getSUM(dtDataPrint, "TT_BN");
                decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
                decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
                tong = tong - tong_ck;
                ReportDocument reportDocument = new ReportDocument();
                string tieude = "", reportname = "", reportCode = "";
                reportCode = "thanhtoan_bangkechiphiKCB_Noitru_Theongay";
                //if (PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet)
                //{
                //    reportCode = "thanhtoan_Bienlai_Dichvu_A4_Innhiet";
                reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                //}
                //else
                //{
                //    switch (khogiay)
                //    {
                //        case "A4":
                //            reportCode = tong_ck <= 0 ? "thanhtoan_Bienlai_Dichvu_A4" : "thanhtoan_Bienlai_Dichvu_Comiengiam_A4";
                //            reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                //            break;
                //        case "A5":
                //            reportCode = tong_ck <= 0 ? "thanhtoan_Bienlai_Dichvu_A5" : "thanhtoan_Bienlai_Dichvu_Comiengiam_A5";
                //            reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                //            break;

                //    }
                //}
                if (reportDocument == null) return;
                var crpt = reportDocument;


                var objForm = new frmPrintPreview("", crpt, true, true);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                //try
                //{
                crpt.SetDataSource(dtDataPrint.DefaultView);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
                Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
                Utility.SetParameterValue(crpt, "tongtien_bn", tong);
                Utility.SetParameterValue(crpt, "DIADIEM", globalVariables.gv_strDiadiem);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                //Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithLocation(DateTime.Now, globalVariables.gv_strDiadiem));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                       new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();

                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
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

        private void grdThongTinChuaThanhToan_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

        private void cboNgaydieutri_CheckedValuesChanged(object sender, EventArgs e)
        {

        }
    }
}
