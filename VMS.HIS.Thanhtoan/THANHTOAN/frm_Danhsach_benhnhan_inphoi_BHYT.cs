using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using System.Xml;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Libs.AppUI;
using VNS.Properties;
using SortOrder = Janus.Windows.GridEX.SortOrder;

namespace VNS.HIS.UI.Forms.THANHTOAN
{
    public partial class FrmDanhsachBenhnhanInphoiBhyt : Form
    {
        public delegate void AddLog(string logText, Color sActionColor);

        private const string _sNewline = "\r\n";
        private bool _bHasloaded;
        private DataSet _dtXml = new DataSet();
        private DataTable _mDtTimKiem = new DataTable();
        private string _macskcb = "27025";
        private string _sFileName;
        private string _sLocalFilePath;
        private string _sLocalPath = Application.StartupPath + "XML";
        private XmlWriter _xmlWriter917;
        private XmlWriterSettings _xmlWriterSettings;
        private Logger log;

        public FrmDanhsachBenhnhanInphoiBhyt()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value = globalVariables.SysDate;
            txtMaLanKham.LostFocus += txtMaLanKham_LostFocus;
            txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
            Utility.VisiableGridEx(grdList, KcbPhieuDct.Columns.IdPhieuDct, globalVariables.IsAdmin);
            PropertyLib._xmlproperties = PropertyLib.GetXMLProperties();
        }

        private string MalanKam { get; set; }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MalanKam = Utility.sDbnull(txtMaLanKham.Text.Trim());
                if (!string.IsNullOrEmpty(MalanKam) && txtMaLanKham.Text.Length < 8)
                {
                    MalanKam = Utility.GetYY(globalVariables.SysDate) +
                               Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLanKham.Text, 0), "000000");
                    txtMaLanKham.Text = MalanKam;
                    txtMaLanKham.Select(txtMaLanKham.Text.Length, txtMaLanKham.Text.Length);
                }
                if (!string.IsNullOrEmpty(txtMaLanKham.Text))
                {
                    cmdTimKiem.PerformClick();
                }
            }
        }

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }

        private void frm_Danhsach_benhnhan_inphoi_BHYT_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            txtMaLanKham.Focus();
            txtMaLanKham.SelectAll();
            _macskcb = globalVariables.gv_strNoicapBHYT + globalVariables.gv_strNoiDKKCBBD;
            // SqlQuery sqlkt =THU_VIEN_CHUNG.LoadThamSoHeThong("",)
        }

        private void txtMaLanKham_LostFocus(object sender, EventArgs eventArgs)
        {
            MalanKam = Utility.sDbnull(txtMaLanKham.Text.Trim());
            if (!string.IsNullOrEmpty(MalanKam) && txtMaLanKham.Text.Length < 8)
            {
                MalanKam = Utility.GetYY(globalVariables.SysDate) +
                           Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLanKham.Text, 0), "000000");
                txtMaLanKham.Text = MalanKam;
            }
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            if (chkDuLieuDaTao.Checked)
            {
                _mDtTimKiem = SPs.SpXmlThongTuBHYT917Search().GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, _mDtTimKiem, true, true, "1=1", "");
                Utility.SetGridEXSortKey(grdList, KcbPhieuDct.Columns.IdPhieuDct, SortOrder.Ascending);
                _bHasloaded = true;
                ModifyCommand();
            }
            else
            {
                TimKiemThongTin();
            }
        }

        private void TimKiemThongTin()
        {
            try
            {
                int tinhtrang, trangthai, chuaketthuc;
                if (chkAllTinhTrang.Checked) tinhtrang = -1;
                else
                {
                    tinhtrang = radNgoaiTru.Checked ? 0 : 1;
                }
                if (chkAllTrangThai.Checked) trangthai = -1;
                else
                {
                    trangthai = radDaduyet.Checked ? 1 : 0;
                }
                if (chkChuaKetThuc.Checked) chuaketthuc = 1;
                else
                {
                    chuaketthuc = 0;
                }
                if (!chkByDate.Checked) dtFromDate.Value = Convert.ToDateTime("1900-01-01");
                _mDtTimKiem =
                    SPs.ThanhtoanDanhsachInphoiBhyt(dtFromDate.Value, dtToDate.Value,
                        Utility.sDbnull(txtMaLanKham.Text, ""), Utility.Int16Dbnull(trangthai),
                        Utility.Int32Dbnull(tinhtrang)
                        , Utility.sDbnull(chuaketthuc, 0)).GetDataSet().
                        Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, _mDtTimKiem, true, true, "1=1", "");
                Utility.SetGridEXSortKey(grdList, KcbPhieuDct.Columns.IdPhieuDct, SortOrder.Ascending);
                _bHasloaded = true;
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void ModifyCommand()
        {
            if (_bHasloaded)
            {
                EnumerableRowCollection<DataRow> query = from daxuatxml in _mDtTimKiem.AsEnumerable()
                    where Utility.Int32Dbnull(daxuatxml["trangthai_xml"]) == 1
                    select daxuatxml;
                EnumerableRowCollection<DataRow> query1 = from chuaxuatxml in _mDtTimKiem.AsEnumerable()
                    where Utility.Int32Dbnull(chuaxuatxml["trangthai_xml"]) == 0
                    select chuaxuatxml;
                Utility.SetMsg(lblDaKetThuc, string.Format("Đã xuất xml: {0} ", query.Count()), true);
                Utility.SetMsg(lblChuaKetThuc, string.Format("Chưa xuất xml: {0}", query1.Count()), true);
            }

            cmdXuatExcel.Enabled = grdList.RowCount > 0;
        }

        private void cmdXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
                    return;
                }
                ExcelUtlity.ExportGridEx(grdList);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        private void frm_Danhsach_benhnhan_inphoi_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.F2)
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
            }
        }

        /// <summary>
        ///     hàm thực hiện việc thay đổi thông tin của phần mã lần khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaLanKham_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaLanKham.Text)) chkByDate.Checked = false;
        }

        private void radNgoaiTru_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void radNoiTru_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void radChuaduyet_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void radDaduyet_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            if (PropertyLib._xmlproperties != null)
            {
                var frm = new frm_Properties(PropertyLib._xmlproperties);
                frm.ShowDialog();
            }
        }

        private void cmdExportXML_Click(object sender, EventArgs e)
        {
            rtxtLogs.Clear();
            if (radChitiet.Checked)
            {
                try
                {
                    int i = 0, j = 0;
                    Utility.ResetProgressBar(prgBar, grdList.GetCheckedRows().Count(), true);
                    foreach (GridEXRow row in grdList.GetCheckedRows())
                    {
                       
                        string maluotKham = Utility.sDbnull(row.Cells["ma_luotkham"].Value);
                        int idBenhnhan = Utility.Int32Dbnull(row.Cells["id_benhnhan"].Value);
                        string istao = Utility.sDbnull(row.Cells["IsTao"].Value);
                        if (istao != "N")
                        {
                            bool kt = ProcessCreateXml(maluotKham);
                            if (kt)
                            {
                                new Update(KcbPhieuDct.Schema).Set(KcbPhieuDct.Columns.TrangthaiXml)
                                    .EqualTo(2).Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(maluotKham).Execute();
                                i = i + 1;
                                LogText(string.Format("{0}. Xuất dữ liệu thành công bệnh nhân ", maluotKham),
                                    Color.DarkBlue);
                            }
                            else
                            {
                                j = j + 1;
                                LogText(
                                    string.Format("{0}. Xuất dữ liệu không thành công bệnh nhân ",maluotKham),
                                    Color.Red);
                            }

                        }
                        else
                        {
                            j = j + 1;
                            LogText(
                                  string.Format("{0}. Bệnh nhân chưa được tạo dữ liệu", maluotKham),
                                  Color.Black);
                        }
                        
                        UIAction.SetValue4Prg(prgBar, 1);
                        Application.DoEvents();
                        row.IsChecked = false;
                    }
                    LogText( string.Format("Tạo thành công {0} file XML, {1} không thành công!  ", i, j),
                                 Color.LightSeaGreen);
                    Utility.SetMsg(lblmsg, string.Format("Tổng số File XML là {0} file", i), false);
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                }
                finally
                {
                    if (_xmlWriter917 != null)
                    {
                        _xmlWriter917.Close();
                    }
                }
            }
            else
            {
                Utility.ResetProgressBar(prgBar, grdList.GetCheckedRows().Count(), true);
                _sLocalPath = Utility.sDbnull(PropertyLib._xmlproperties.ChonduongdanFileTonghop);
                _xmlWriterSettings = new XmlWriterSettings
                {
                    Encoding = new UnicodeEncoding(false, false),
                    Indent = true,
                    IndentChars = "\t",
                    OmitXmlDeclaration = true
                };
                if (!grdList.GetCheckedRows().Any()) return;

                _sFileName = Utility.sDbnull(_macskcb) + "_" +
                             Utility.sDbnull(DateTime.Now.ToString("yyyyMMdd")) + "_CheckOut.xml";
                string sDirectoryxml1 = _sLocalPath;
                if (!Directory.Exists(sDirectoryxml1)) Directory.CreateDirectory(sDirectoryxml1);
                _sLocalFilePath = _sLocalPath + "\\" + _sFileName;

                using (XmlWriter xmlWriter917Tonghop = XmlWriter.Create(_sLocalFilePath, _xmlWriterSettings))
                {
                    try
                    {
                        _sLocalFilePath = _sLocalPath + "\\" + _sFileName;
                        xmlWriter917Tonghop.WriteProcessingInstruction("xml",
                            string.Format("version={0}1.0{0}", ((char) 34)));
                        xmlWriter917Tonghop.WriteStartElement("GIAMDINHHS");
                        xmlWriter917Tonghop.WriteStartElement("THONGTINDONVI");
                        xmlWriter917Tonghop.WriteElementString("MACSKCB", Utility.sDbnull(_macskcb));
                        xmlWriter917Tonghop.WriteEndElement();
                        xmlWriter917Tonghop.WriteStartElement("THONGTINHOSO");
                        xmlWriter917Tonghop.WriteElementString("NGAYLAP",
                            Utility.sDbnull(DateTime.Now.ToString("yyyyMMdd")));
                        xmlWriter917Tonghop.WriteElementString("SOLUONGHOSO",
                            Utility.sDbnull(grdList.GetCheckedRows().Count()));
                        xmlWriter917Tonghop.WriteStartElement("DANHSACHHOSO");
                        Utility.ResetProgressBar(prgBar, grdList.GetCheckedRows().Count(), true);
                        int i = 0;
                        foreach (GridEXRow row in grdList.GetCheckedRows())
                        {
                            string maluotKham = Utility.sDbnull(row.Cells["ma_luotkham"].Value);
                            int idBenhnhan = Utility.Int32Dbnull(row.Cells["id_benhnhan"].Value);
                            _dtXml = SPs.SpXmlThongTuBHYT917GetData(Utility.sDbnull(maluotKham)).GetDataSet();
                            //var sbXmLhoso = new StringBuilder();
                            //using (XmlWriter xmlWriter917Hoso = XmlWriter.Create(sbXmLhoso, _xmlWriterSettings))
                            //{
                                xmlWriter917Tonghop.WriteStartElement("HOSO");

                                xmlWriter917Tonghop.WriteStartElement("FILEHOSO");
                                xmlWriter917Tonghop.WriteElementString("LOAIHOSO", Utility.sDbnull("XML1"));

                                #region Ghi XML1

                                var sbXml1 = new StringBuilder();
                                using (XmlWriter xmlWriter917Xml1 = XmlWriter.Create(sbXml1, _xmlWriterSettings))
                                {
                                    //Nội dung File XML số 1 
                                    xmlWriter917Xml1.WriteProcessingInstruction("xml",
                                        string.Format("version={0}1.0{0}", ((char) 34)));
                                    xmlWriter917Xml1.WriteStartElement("TONG_HOP");
                                    xmlWriter917Xml1.WriteElementString("MA_LK",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LK"]));
                                    xmlWriter917Xml1.WriteElementString("STT",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["STT"]));
                                    xmlWriter917Xml1.WriteElementString("MA_BN", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_BN"]));
                                    xmlWriter917Xml1.WriteStartElement("HO_TEN");
                                    xmlWriter917Xml1.WriteCData(Utility.sDbnull(_dtXml.Tables[0].Rows[0]["HO_TEN"]));
                                    xmlWriter917Xml1.WriteEndElement();
                                    //xmlWriter917Xml1.WriteElementString("HO_TEN",
                                    //    string.Format("<![CDATA[{0}]]>",
                                    //        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["HO_TEN"])));
                                       
                                    xmlWriter917Xml1.WriteElementString("NGAY_SINH",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_SINH"]));
                                    xmlWriter917Xml1.WriteElementString("GIOI_TINH",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["GIOI_TINH"]));
                                    xmlWriter917Xml1.WriteStartElement("DIA_CHI");
                                    xmlWriter917Xml1.WriteCData(Utility.sDbnull(_dtXml.Tables[0].Rows[0]["DIA_CHI"]));
                                    xmlWriter917Xml1.WriteEndElement();
                                    //xmlWriter917Xml1.WriteElementString("DIA_CHI",
                                    //    string.Format("<![CDATA[{0}]]>",
                                    //        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["DIA_CHI"])));
                                    xmlWriter917Xml1.WriteElementString("MA_THE",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_THE"]));
                                    xmlWriter917Xml1.WriteElementString("MA_DKBD",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_DKBD"]));
                                    xmlWriter917Xml1.WriteElementString("GT_THE_TU",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["GT_THE_TU"]));
                                    xmlWriter917Xml1.WriteElementString("GT_THE_DEN",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["GT_THE_DEN"]));
                                    xmlWriter917Xml1.WriteElementString("MA_BENH",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_BENH"]));
                                    xmlWriter917Xml1.WriteElementString("MA_BENHKHAC",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_BENHKHAC"]));
                                    xmlWriter917Xml1.WriteStartElement("TEN_BENH");
                                    xmlWriter917Xml1.WriteCData(Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TEN_BENH"]));
                                    xmlWriter917Xml1.WriteEndElement();
                                    //xmlWriter917Xml1.WriteElementString("TEN_BENH",
                                    //    string.Format("<![CDATA[{0}]]>",
                                    //        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TEN_BENH"])));
                                    xmlWriter917Xml1.WriteElementString("MA_LYDO_VVIEN",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LYDO_VVIEN"]));
                                    xmlWriter917Xml1.WriteElementString("MA_NOI_CHUYEN",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_NOI_CHUYEN"]));
                                    xmlWriter917Xml1.WriteElementString("MA_TAI_NAN",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_TAI_NAN"]));
                                    xmlWriter917Xml1.WriteElementString("NGAY_VAO",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_VAO"]));
                                    xmlWriter917Xml1.WriteElementString("NGAY_RA",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_RA"]));
                                    xmlWriter917Xml1.WriteElementString("SO_NGAY_DTRI",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["SO_NGAY_DTRI"]));
                                    xmlWriter917Xml1.WriteElementString("KET_QUA_DTRI",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["KET_QUA_DTRI"]));
                                    xmlWriter917Xml1.WriteElementString("TINH_TRANG_RV",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TINH_TRANG_RV"]));
                                    xmlWriter917Xml1.WriteElementString("NGAY_TTOAN",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_TTOAN"]));
                                    xmlWriter917Xml1.WriteElementString("MUC_HUONG",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MUC_HUONG"]));
                                    xmlWriter917Xml1.WriteElementString("T_THUOC",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_THUOC"]));
                                    xmlWriter917Xml1.WriteElementString("T_VTYT",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_VTYT"]));
                                    xmlWriter917Xml1.WriteElementString("T_TONGCHI",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_TONGCHI"]));
                                    xmlWriter917Xml1.WriteElementString("T_BNTT",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_BNTT"]));
                                    xmlWriter917Xml1.WriteElementString("T_BHTT",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_BHTT"]));
                                    xmlWriter917Xml1.WriteElementString("T_NGUONKHAC",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_NGUONKHAC"]));
                                    xmlWriter917Xml1.WriteElementString("T_NGOAIDS",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_NGOAIDS"]));
                                    xmlWriter917Xml1.WriteElementString("NAM_QT",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NAM_QT"]));
                                    xmlWriter917Xml1.WriteElementString("THANG_QT",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["THANG_QT"]));
                                    xmlWriter917Xml1.WriteElementString("MA_LOAI_KCB",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LOAI_KCB"]));
                                    xmlWriter917Xml1.WriteElementString("MA_KHOA",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_KHOA"]));
                                    xmlWriter917Xml1.WriteElementString("MA_CSKCB",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_CSKCB"]));
                                    xmlWriter917Xml1.WriteElementString("MA_KHUVUC",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_KHUVUC"], "_"));
                                    xmlWriter917Xml1.WriteElementString("MA_PTTT_QT",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_PTTT_QT"], "_"));
                                    xmlWriter917Xml1.WriteElementString("CAN_NANG",
                                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["CAN_NANG"]));
                                    xmlWriter917Xml1.WriteEndElement();
                                    xmlWriter917Xml1.Flush();
                                }

                                #endregion

                                var encodingXml1 = new UnicodeEncoding();
                                string sXml1 = Convert.ToBase64String(encodingXml1.GetBytes(sbXml1.ToString()));
                                xmlWriter917Tonghop.WriteElementString("NOIDUNGFILE", sXml1);
                                xmlWriter917Tonghop.WriteEndElement(); // Đóng file hồ sơ

                                #region Ghi XML2

                            if (_dtXml.Tables[1].Rows.Count > 0)
                            {
                                xmlWriter917Tonghop.WriteStartElement("FILEHOSO");
                                xmlWriter917Tonghop.WriteElementString("LOAIHOSO", Utility.sDbnull("XML2"));
                                var sbXml2 = new StringBuilder();
                                using (XmlWriter xmlWriter917Xml2 = XmlWriter.Create(sbXml2, _xmlWriterSettings))
                                {
                                    if (_dtXml.Tables[1].Rows.Count > 0)
                                        xmlWriter917Xml2.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char)34)));
                                    xmlWriter917Xml2.WriteStartElement("DSACH_CHI_TIET_THUOC");
                                    foreach (DataRow rowDichvu in _dtXml.Tables[1].Rows)
                                    {
                                        xmlWriter917Xml2.WriteStartElement("CHI_TIET_THUOC");
                                        xmlWriter917Xml2.WriteElementString("MA_LK", Utility.sDbnull(rowDichvu["MA_LK"]));
                                        xmlWriter917Xml2.WriteElementString("STT", Utility.sDbnull(rowDichvu["STT"]));
                                        xmlWriter917Xml2.WriteElementString("MA_THUOC", Utility.sDbnull(rowDichvu["MA_THUOC"]));
                                        xmlWriter917Xml2.WriteElementString("MA_NHOM", Utility.sDbnull(rowDichvu["MA_NHOM"]));
                                        xmlWriter917Xml2.WriteStartElement("TEN_THUOC");
                                        xmlWriter917Xml2.WriteCData(Utility.sDbnull(rowDichvu["TEN_THUOC"]));
                                        xmlWriter917Xml2.WriteEndElement();
                                        //xmlWriter917Xml2.WriteElementString("TEN_THUOC", string.Format("<![CDATA[{0}]]>", Utility.sDbnull(rowDichvu["TEN_THUOC"])));
                                        xmlWriter917Xml2.WriteElementString("DON_VI_TINH", Utility.sDbnull(rowDichvu["DON_VI_TINH"]));
                                        xmlWriter917Xml2.WriteStartElement("HAM_LUONG");
                                        xmlWriter917Xml2.WriteCData(Utility.sDbnull(rowDichvu["HAM_LUONG"]));
                                        xmlWriter917Xml2.WriteEndElement();
                                     //   xmlWriter917Xml2.WriteElementString("HAM_LUONG", string.Format("<![CDATA[{0}]]>", Utility.sDbnull(rowDichvu["HAM_LUONG"])));
                                        xmlWriter917Xml2.WriteElementString("DUONG_DUNG", Utility.sDbnull(rowDichvu["DUONG_DUNG"]));
                                        xmlWriter917Xml2.WriteStartElement("LIEU_DUNG");
                                        xmlWriter917Xml2.WriteCData(Utility.sDbnull(rowDichvu["LIEU_DUNG"]));
                                        xmlWriter917Xml2.WriteEndElement();
                                      //  xmlWriter917Xml2.WriteElementString("LIEU_DUNG", string.Format("<![CDATA[{0}]]>", Utility.sDbnull(rowDichvu["LIEU_DUNG"])));
                                        xmlWriter917Xml2.WriteElementString("SO_DANG_KY", Utility.sDbnull(rowDichvu["SO_DANG_KY"]));
                                        xmlWriter917Xml2.WriteElementString("SO_LUONG", Utility.sDbnull(rowDichvu["SO_LUONG"]));
                                        xmlWriter917Xml2.WriteElementString("DON_GIA", Utility.sDbnull(rowDichvu["DON_GIA"]));
                                        xmlWriter917Xml2.WriteElementString("TYLE_TT", Utility.sDbnull(rowDichvu["TYLE_TT"]));
                                        xmlWriter917Xml2.WriteElementString("THANH_TIEN", Utility.sDbnull(rowDichvu["THANH_TIEN"]));
                                        xmlWriter917Xml2.WriteElementString("MA_KHOA", Utility.sDbnull(rowDichvu["MA_KHOA"]));
                                        xmlWriter917Xml2.WriteElementString("MA_BAC_SI", Utility.sDbnull(rowDichvu["MA_BAC_SI"]));
                                        xmlWriter917Xml2.WriteElementString("MA_BENH", Utility.sDbnull(rowDichvu["MA_BENH"]));
                                        xmlWriter917Xml2.WriteElementString("NGAY_YL", Utility.sDbnull(rowDichvu["NGAY_YL"]));
                                        xmlWriter917Xml2.WriteElementString("MA_PTTT", Utility.sDbnull(rowDichvu["MA_PTTT"]));
                                        xmlWriter917Xml2.WriteEndElement();
                                    }
                                    xmlWriter917Xml2.WriteEndElement();
                                    xmlWriter917Xml2.Flush();
                                }
                                var encodingXml2 = new UnicodeEncoding();
                                string sXml2 = Convert.ToBase64String(encodingXml2.GetBytes(sbXml2.ToString()));
                                xmlWriter917Tonghop.WriteElementString("NOIDUNGFILE", sXml2);
                                xmlWriter917Tonghop.WriteEndElement();

                                #endregion

                            }
                               
                                xmlWriter917Tonghop.WriteStartElement("FILEHOSO");
                                xmlWriter917Tonghop.WriteElementString("LOAIHOSO", Utility.sDbnull("XML3"));

                                #  region  Ghi XML3 

                                var sbXml3 = new StringBuilder();
                                using (XmlWriter xmlWriter917Xml3 = XmlWriter.Create(sbXml3, _xmlWriterSettings))
                                {
                                    xmlWriter917Xml3.WriteProcessingInstruction("xml",
                                        string.Format("version={0}1.0{0}", ((char) 34)));
                                    xmlWriter917Xml3.WriteStartElement("DSACH_CHI_TIET_DVKT");
                                    foreach (DataRow rowThuoc in _dtXml.Tables[2].Rows)
                                    {
                                        xmlWriter917Xml3.WriteStartElement("CHI_TIET_DVKT");
                                        xmlWriter917Xml3.WriteElementString("MA_LK",
                                            Utility.sDbnull(rowThuoc["MA_LK"]));
                                        xmlWriter917Xml3.WriteElementString("STT", Utility.sDbnull(rowThuoc["STT"]));
                                        xmlWriter917Xml3.WriteElementString("MA_DICH_VU", Utility.sDbnull(rowThuoc["MA_DICH_VU"]));
                                        xmlWriter917Xml3.WriteElementString("MA_VAT_TU",Utility.sDbnull(rowThuoc["MA_VAT_TU"]));
                                        xmlWriter917Xml3.WriteElementString("MA_NHOM",
                                            Utility.sDbnull(rowThuoc["MA_NHOM"]));
                                        xmlWriter917Xml3.WriteStartElement("TEN_DICH_VU");
                                        xmlWriter917Xml3.WriteCData(Utility.sDbnull(rowThuoc["TEN_DICH_VU"]));
                                        xmlWriter917Xml3.WriteEndElement();
                                        //xmlWriter917Xml3.WriteElementString("TEN_DICH_VU",
                                        //          string.Format("<![CDATA[{0}]]>",
                                        //    Utility.sDbnull(rowThuoc["TEN_DICH_VU"])));
                                        xmlWriter917Xml3.WriteElementString("DON_VI_TINH",
                                            Utility.sDbnull(rowThuoc["DON_VI_TINH"]));
                                        xmlWriter917Xml3.WriteElementString("SO_LUONG",
                                            Utility.sDbnull(rowThuoc["SO_LUONG"]));
                                        xmlWriter917Xml3.WriteElementString("DON_GIA",
                                            Utility.sDbnull(rowThuoc["DON_GIA"]));
                                        xmlWriter917Xml3.WriteElementString("TYLE_TT",
                                            Utility.sDbnull(rowThuoc["TYLE_TT"]));
                                        xmlWriter917Xml3.WriteElementString("THANH_TIEN",
                                            Utility.sDbnull(rowThuoc["THANH_TIEN"]));
                                        xmlWriter917Xml3.WriteElementString("MA_KHOA",
                                            Utility.sDbnull(rowThuoc["MA_KHOA"]));
                                        xmlWriter917Xml3.WriteElementString("MA_BAC_SI",
                                            Utility.sDbnull(rowThuoc["MA_BAC_SI"]));
                                        xmlWriter917Xml3.WriteElementString("MA_BENH",
                                            Utility.sDbnull(rowThuoc["MA_BENH"]));
                                        xmlWriter917Xml3.WriteElementString("NGAY_YL",
                                            Utility.sDbnull(rowThuoc["NGAY_YL"]));
                                        xmlWriter917Xml3.WriteElementString("NGAY_KQ",
                                            Utility.sDbnull(rowThuoc["NGAY_KQ"]));
                                        xmlWriter917Xml3.WriteElementString("MA_PTTT",
                                            Utility.sDbnull(rowThuoc["MA_PTTT"]));
                                        xmlWriter917Xml3.WriteEndElement();
                                    }
                                    xmlWriter917Xml3.WriteEndElement();
                                    xmlWriter917Xml3.Flush();
                                }

                                #endregion

                                var encodingXml3 = new UnicodeEncoding();
                                string sXml3 = Convert.ToBase64String(encodingXml3.GetBytes(sbXml3.ToString()));
                                xmlWriter917Tonghop.WriteElementString("NOIDUNGFILE", sXml3);
                                xmlWriter917Tonghop.WriteEndElement();
                                xmlWriter917Tonghop.WriteEndElement(); // Đóng hồ sơ


                                new Update(KcbPhieuDct.Schema).Set(KcbPhieuDct.Columns.TrangthaiXml).EqualTo(1).Where(
                                    KcbPhieuDct.Columns.IdBenhnhan)
                                    .IsEqualTo(idBenhnhan)
                                    .And(KcbPhieuDct.Columns.MaLuotkham)
                                    .
                                    IsEqualTo(maluotKham).Execute();
                                UIAction.SetValue4Prg(prgBar, 1);
                            i = i + 1;
                            LogText(string.Format("{0}. Ghi dữ liệu XML thành công bệnh nhân {1} vào File tổng hợp",i, maluotKham), Color.DarkBlue);
                           // }
                        }
                        xmlWriter917Tonghop.WriteEndElement(); // Đóng Danh sách hồ sơ
                        xmlWriter917Tonghop.WriteEndElement(); // Đóng thông tin hồ sơ
                        xmlWriter917Tonghop.WriteEndDocument();
                        xmlWriter917Tonghop.Flush();
                    }
                    catch (Exception ex)
                    {
                        log.Trace("Lỗi " + ex.Message);
                        LogText(string.Format(" Ghi dữ liệu XML không thành công bệnh nhân vào File tổng hợp"), Color.Red);
                    }
                    finally
                    {
                        if (xmlWriter917Tonghop != null)
                        {
                            xmlWriter917Tonghop.Close();
                        }
                    }
                }
            }
        }

        private bool ProcessCreateXml(string maluotKham)
        {
            try
            {
                _dtXml = SPs.SpXmlThongTuBHYT917GetData(Utility.sDbnull(maluotKham)).GetDataSet();
                if (rad917.Checked)
                {
                    ProcessXmlWrite917();
                }
                else
                {
                    ProcessXmlWrite9324();
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                return false;
            }
            finally
            {
                if (_xmlWriter917 != null)
                    _xmlWriter917.Close();
            }
        }

        /// <summary>
        ///     Code theo thông tư 917
        /// </summary>
        /// <returns></returns>
        private bool ProcessXmlWrite917()
        {
            try
            {
                _sLocalPath = Utility.sDbnull(PropertyLib._xmlproperties.ChonduongdanFileChiTiet917);
                if (!Directory.Exists(_sLocalPath)) Directory.CreateDirectory(_sLocalPath);
                _xmlWriterSettings = new XmlWriterSettings
                {
                    Encoding = new UnicodeEncoding(false, false),
                    Indent = true,
                    IndentChars = "\t",
                    OmitXmlDeclaration = true
                };

                #region Ghi File XML1 

                if (_dtXml.Tables[0].Rows.Count <= 0) return true;
                // Ghi file XML1 
                _sFileName = Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_CSKCB"]) + "_" +
                             Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_VAO"]) + "_" +
                             Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_THE"]) + "_CheckOut.xml";
                string sDirectoryxml1 = _sLocalPath;
                if (!Directory.Exists(sDirectoryxml1)) Directory.CreateDirectory(sDirectoryxml1);
                _sLocalFilePath = _sLocalPath + "\\" + _sFileName;

                var sbXml1 = new StringBuilder();
                using (XmlWriter xmlWriter917Xml1 = XmlWriter.Create(sbXml1, _xmlWriterSettings))
                {
                    xmlWriter917Xml1.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char) 34)));
                    //Nội dung File XML số 1 
                    xmlWriter917Xml1.WriteStartElement("TONG_HOP");
                    xmlWriter917Xml1.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LK"]));
                    xmlWriter917Xml1.WriteElementString("STT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["STT"]));
                    xmlWriter917Xml1.WriteElementString("MA_BN", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_BN"]));
                    xmlWriter917Xml1.WriteStartElement("HO_TEN");
                    xmlWriter917Xml1.WriteCData(Utility.sDbnull(_dtXml.Tables[0].Rows[0]["HO_TEN"]));
                    xmlWriter917Xml1.WriteEndElement();
                    //xmlWriter917Xml1.WriteElementString("HO_TEN", string.Format("<![CDATA[{0}]]>",
                    //                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["HO_TEN"])));
                    xmlWriter917Xml1.WriteElementString("NGAY_SINH",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_SINH"]));
                    xmlWriter917Xml1.WriteElementString("GIOI_TINH",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["GIOI_TINH"]));
                    xmlWriter917Xml1.WriteStartElement("DIA_CHI");
                    xmlWriter917Xml1.WriteCData(Utility.sDbnull(_dtXml.Tables[0].Rows[0]["DIA_CHI"]));
                    xmlWriter917Xml1.WriteEndElement();
                    //xmlWriter917Xml1.WriteElementString("DIA_CHI",
                    //  string.Format("<![CDATA[{0}]]>",
                    //                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["DIA_CHI"])));
                    xmlWriter917Xml1.WriteElementString("MA_THE", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_THE"]));
                    xmlWriter917Xml1.WriteElementString("MA_DKBD",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_DKBD"]));
                    xmlWriter917Xml1.WriteElementString("GT_THE_TU",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["GT_THE_TU"]));
                    xmlWriter917Xml1.WriteElementString("GT_THE_DEN",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["GT_THE_DEN"]));
                    xmlWriter917Xml1.WriteElementString("MA_BENH",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_BENH"]));
                    xmlWriter917Xml1.WriteElementString("MA_BENHKHAC",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_BENHKHAC"]));
                    xmlWriter917Xml1.WriteStartElement("TEN_BENH");
                    xmlWriter917Xml1.WriteCData(Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TEN_BENH"]));
                    xmlWriter917Xml1.WriteEndElement();
                    //xmlWriter917Xml1.WriteElementString("TEN_BENH",
                    //    string.Format("<![CDATA[{0}]]>",
                    //                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TEN_BENH"])));
                    xmlWriter917Xml1.WriteElementString("MA_LYDO_VVIEN",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LYDO_VVIEN"]));
                    xmlWriter917Xml1.WriteElementString("MA_NOI_CHUYEN",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_NOI_CHUYEN"]));
                    xmlWriter917Xml1.WriteElementString("MA_TAI_NAN",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_TAI_NAN"]));
                    xmlWriter917Xml1.WriteElementString("NGAY_VAO",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_VAO"]));
                    xmlWriter917Xml1.WriteElementString("NGAY_RA",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_RA"]));
                    xmlWriter917Xml1.WriteElementString("SO_NGAY_DTRI",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["SO_NGAY_DTRI"]));
                    xmlWriter917Xml1.WriteElementString("KET_QUA_DTRI",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["KET_QUA_DTRI"]));
                    xmlWriter917Xml1.WriteElementString("TINH_TRANG_RV",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TINH_TRANG_RV"]));
                    xmlWriter917Xml1.WriteElementString("NGAY_TTOAN",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_TTOAN"]));
                    xmlWriter917Xml1.WriteElementString("MUC_HUONG",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MUC_HUONG"]));
                    xmlWriter917Xml1.WriteElementString("T_THUOC",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_THUOC"]));
                    xmlWriter917Xml1.WriteElementString("T_VTYT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_VTYT"]));
                    xmlWriter917Xml1.WriteElementString("T_TONGCHI",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_TONGCHI"]));
                    xmlWriter917Xml1.WriteElementString("T_BNTT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_BNTT"]));
                    xmlWriter917Xml1.WriteElementString("T_BHTT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_BHTT"]));
                    xmlWriter917Xml1.WriteElementString("T_NGUONKHAC",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_NGUONKHAC"]));
                    xmlWriter917Xml1.WriteElementString("T_NGOAIDS",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_NGOAIDS"]));
                    xmlWriter917Xml1.WriteElementString("NAM_QT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NAM_QT"]));
                    xmlWriter917Xml1.WriteElementString("THANG_QT",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["THANG_QT"]));
                    xmlWriter917Xml1.WriteElementString("MA_LOAI_KCB",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LOAI_KCB"]));
                    xmlWriter917Xml1.WriteElementString("MA_KHOA",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_KHOA"]));
                    xmlWriter917Xml1.WriteElementString("MA_CSKCB",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_CSKCB"]));
                    xmlWriter917Xml1.WriteElementString("MA_KHUVUC",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_KHUVUC"], "_"));
                    xmlWriter917Xml1.WriteElementString("MA_PTTT_QT",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_PTTT_QT"], "_"));
                    xmlWriter917Xml1.WriteElementString("CAN_NANG",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["CAN_NANG"]));
                    xmlWriter917Xml1.WriteEndElement();
                    xmlWriter917Xml1.Flush();
                }
                var encodingXML1 = new UnicodeEncoding();
                string sXML1 = Convert.ToBase64String(encodingXML1.GetBytes(sbXml1.ToString()));
                // File.WriteAllText(_sLocalFilePath, s);

                #endregion

                // Ghi File XML2 

                #region Ghi File XML2 

                var encodingXml2 = new UnicodeEncoding();
                string sXml2 = "";
                if (_dtXml.Tables[1].Rows.Count > 0)
                {
                    //_sFileName = Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_CSKCB"]) + "_" +
                    //     Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_VAO"]) + "_" +
                    //      Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_THE"]) + "_XML2.xml";
                    //string sDirectoryxml2 = _sLocalPath;
                    //if (!Directory.Exists(sDirectoryxml2)) Directory.CreateDirectory(sDirectoryxml2);
                    //_sLocalFilePath = _sLocalPath + "\\" + _sFileName;

                    //_xmlWriter917 = XmlWriter.Create(_sLocalFilePath, xmlWriterSettings);
                    //_xmlWriter917.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char)34)));
                    var sbXml2 = new StringBuilder();
                    using (XmlWriter xmlWriter917Xml2 = XmlWriter.Create(sbXml2, _xmlWriterSettings))
                    {
                        xmlWriter917Xml2.WriteStartElement("DSACH_CHI_TIET_THUOC");
                        foreach (DataRow row in _dtXml.Tables[1].Rows)
                        {
                            xmlWriter917Xml2.WriteStartElement("CHI_TIET_THUOC");
                            xmlWriter917Xml2.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            xmlWriter917Xml2.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            xmlWriter917Xml2.WriteElementString("MA_THUOC", Utility.sDbnull(row["MA_THUOC"]));
                            xmlWriter917Xml2.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                            xmlWriter917Xml2.WriteStartElement("TEN_THUOC");
                            xmlWriter917Xml2.WriteCData(Utility.sDbnull(row["TEN_THUOC"]));
                            xmlWriter917Xml2.WriteEndElement();
                            //xmlWriter917Xml2.WriteElementString("TEN_THUOC", string.Format("<![CDATA[{0}]]>",
                            //                Utility.sDbnull(row["TEN_THUOC"])));
                            xmlWriter917Xml2.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                            xmlWriter917Xml2.WriteStartElement("HAM_LUONG");
                            xmlWriter917Xml2.WriteCData(Utility.sDbnull(row["HAM_LUONG"]));
                            xmlWriter917Xml2.WriteEndElement();
                            //xmlWriter917Xml2.WriteElementString("HAM_LUONG", string.Format("<![CDATA[{0}]]>",
                            //                Utility.sDbnull(row["HAM_LUONG"])));
                            xmlWriter917Xml2.WriteElementString("DUONG_DUNG", Utility.sDbnull(row["DUONG_DUNG"]));
                            xmlWriter917Xml2.WriteStartElement("LIEU_DUNG");
                            xmlWriter917Xml2.WriteCData(Utility.sDbnull(row["LIEU_DUNG"]));
                            xmlWriter917Xml2.WriteEndElement();
                            //xmlWriter917Xml2.WriteElementString("LIEU_DUNG", string.Format("<![CDATA[{0}]]>",
                            //                Utility.sDbnull(row["LIEU_DUNG"])));
                            xmlWriter917Xml2.WriteElementString("SO_DANG_KY", Utility.sDbnull(row["SO_DANG_KY"]));
                            xmlWriter917Xml2.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                            xmlWriter917Xml2.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                            xmlWriter917Xml2.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                            xmlWriter917Xml2.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                            xmlWriter917Xml2.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                            xmlWriter917Xml2.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                            xmlWriter917Xml2.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                            xmlWriter917Xml2.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            xmlWriter917Xml2.WriteElementString("MA_PTTT", Utility.sDbnull(row["MA_PTTT"]));

                            xmlWriter917Xml2.WriteEndElement();
                        }
                        xmlWriter917Xml2.WriteEndElement();
                        xmlWriter917Xml2.Flush();
                    }
                    sXml2 = Convert.ToBase64String(encodingXml2.GetBytes(sbXml2.ToString()));
                }

                #endregion

                // Ghi File XML 3 

                #region Ghi File XML 3

                var encodingXml3 = new UnicodeEncoding();
                string sXml3 = "";
                if (_dtXml.Tables[2].Rows.Count > 0)
                {
                    //_sFileName = Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_CSKCB"]) + "_" +
                    //     Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_VAO"]) + "_" +
                    //      Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_THE"]) + "_XML3.xml";
                    //string sDirectoryxml3 = _sLocalPath;
                    //if (!Directory.Exists(sDirectoryxml3)) Directory.CreateDirectory(sDirectoryxml3);
                    //_sLocalFilePath = _sLocalPath + "\\" + _sFileName;

                    //_xmlWriter917 = XmlWriter.Create(_sLocalFilePath, xmlWriterSettings);
                    //_xmlWriter917.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char)34)));
                    var sbXml3 = new StringBuilder();
                    using (XmlWriter xmlWriter917Xml3 = XmlWriter.Create(sbXml3, _xmlWriterSettings))
                    {
                        xmlWriter917Xml3.WriteStartElement("DSACH_CHI_TIET_DVKT");
                        foreach (DataRow row in _dtXml.Tables[2].Rows)
                        {
                            xmlWriter917Xml3.WriteStartElement("CHI_TIET_DVKT");
                            xmlWriter917Xml3.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            xmlWriter917Xml3.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            xmlWriter917Xml3.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
                            xmlWriter917Xml3.WriteElementString("MA_VAT_TU", Utility.sDbnull(row["MA_VAT_TU"]));
                            xmlWriter917Xml3.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                            xmlWriter917Xml3.WriteStartElement("TEN_DICH_VU");
                            xmlWriter917Xml3.WriteCData(Utility.sDbnull(row["TEN_DICH_VU"]));
                            xmlWriter917Xml3.WriteEndElement();
                            //xmlWriter917Xml3.WriteElementString("TEN_DICH_VU", string.Format("<![CDATA[{0}]]>",
                            //                Utility.sDbnull(row["TEN_DICH_VU"])));
                            xmlWriter917Xml3.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                            xmlWriter917Xml3.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                            xmlWriter917Xml3.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                            xmlWriter917Xml3.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                            xmlWriter917Xml3.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                            xmlWriter917Xml3.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                            xmlWriter917Xml3.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                            xmlWriter917Xml3.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                            xmlWriter917Xml3.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            xmlWriter917Xml3.WriteElementString("NGAY_KQ", Utility.sDbnull(row["NGAY_KQ"]));
                            xmlWriter917Xml3.WriteElementString("MA_PTTT", Utility.sDbnull(row["MA_PTTT"]));
                            xmlWriter917Xml3.WriteEndElement();
                        }
                        xmlWriter917Xml3.WriteEndElement();
                        xmlWriter917Xml3.Flush();
                    }

                    sXml3 = Convert.ToBase64String(encodingXml3.GetBytes(sbXml3.ToString()));
                    // File.WriteAllText(_sLocalFilePath, s);
                }

                #endregion

                using (XmlWriter _xmlWriter917 = XmlWriter.Create(_sLocalFilePath, _xmlWriterSettings))
                {
                    _xmlWriter917.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char) 34)));
                    _xmlWriter917.WriteStartElement("GIAMDINHHS");
                    _xmlWriter917.WriteStartElement("THONGTINDONVI");
                    _xmlWriter917.WriteElementString("MACSKCB", Utility.sDbnull(_macskcb));
                    _xmlWriter917.WriteEndElement();

                    _xmlWriter917.WriteStartElement("THONGTINHOSO");
                    _xmlWriter917.WriteElementString("NGAYLAP", Utility.sDbnull(DateTime.Now.ToString("yyyyMMdd")));
                    _xmlWriter917.WriteElementString("SOLUONGHOSO", Utility.sDbnull(1));


                    _xmlWriter917.WriteStartElement("DANHSACHHOSO");
                    _xmlWriter917.WriteStartElement("HOSO");

                    if (sXML1 != "")
                    {
                        _xmlWriter917.WriteStartElement("FILEHOSO");
                        _xmlWriter917.WriteElementString("LOAIHOSO", Utility.sDbnull("XML1"));
                        _xmlWriter917.WriteElementString("NOIDUNGFILE", sXML1);
                        _xmlWriter917.WriteEndElement();
                    }

                    if (sXml2 != "")
                    {
                        _xmlWriter917.WriteStartElement("FILEHOSO");
                        _xmlWriter917.WriteElementString("LOAIHOSO", Utility.sDbnull("XML2"));
                        _xmlWriter917.WriteElementString("NOIDUNGFILE", sXml2);
                        _xmlWriter917.WriteEndElement();
                    }
                    if (sXml3 != "")
                    {
                        _xmlWriter917.WriteStartElement("FILEHOSO");
                        _xmlWriter917.WriteElementString("LOAIHOSO", Utility.sDbnull("XML3"));
                        _xmlWriter917.WriteElementString("NOIDUNGFILE", sXml3);
                        _xmlWriter917.WriteEndElement();
                    }
                    _xmlWriter917.WriteEndElement(); // Đóng hồ sơ
                    _xmlWriter917.WriteEndElement(); // Đóng Danh sách hồ sơ

                    _xmlWriter917.WriteEndElement(); // Đóng thông tin hồ sơ
                    _xmlWriter917.Flush();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Code theo thông tư 9324
        /// </summary>
        /// <param name="ma_lankham"></param>
        /// <param name="id_benhnhan"></param>
        /// <returns></returns>
        private bool ProcessXmlWrite9324()
        {
            try
            {
                _sLocalPath = Utility.sDbnull(PropertyLib._xmlproperties.ChonduongdanFileChiTiet9324);
                if (!Directory.Exists(_sLocalPath)) Directory.CreateDirectory(_sLocalPath);
                _xmlWriterSettings = new XmlWriterSettings
                {
                    Encoding = new UnicodeEncoding(false, false),
                    Indent = true,
                    IndentChars = "\t",
                    OmitXmlDeclaration = true
                };

                #region Ghi File XML1

                if (_dtXml.Tables[0].Rows.Count <= 0) return true;
                // Ghi file XML1 
                _sFileName = Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_CSKCB"]) + "_" +
                             Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_VAO"]) + "_" +
                             Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_THE"]) + "_XML1.xml";
                string sDirectoryxml1 = _sLocalPath;
                if (!Directory.Exists(sDirectoryxml1)) Directory.CreateDirectory(sDirectoryxml1);
                _sLocalFilePath = _sLocalPath + "\\" + _sFileName;

                using (XmlWriter xmlWriter917Xml1 = XmlWriter.Create(_sLocalFilePath, _xmlWriterSettings))
                {
                    try
                    {
                        xmlWriter917Xml1.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char)34)));
                        //Nội dung File XML số 1 
                        xmlWriter917Xml1.WriteStartElement("TONG_HOP");
                        xmlWriter917Xml1.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LK"]));
                        xmlWriter917Xml1.WriteElementString("STT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["STT"]));
                        xmlWriter917Xml1.WriteElementString("MA_BN", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_BN"]));
                        xmlWriter917Xml1.WriteStartElement("HO_TEN");
                        xmlWriter917Xml1.WriteCData(Utility.sDbnull(_dtXml.Tables[0].Rows[0]["HO_TEN"]));
                        xmlWriter917Xml1.WriteEndElement();
                     //   xmlWriter917Xml1.WriteElementString("HO_TEN",    string.Format(@"<![CDATA[{0}]]>",Utility.sDbnull(_dtXml.Tables[0].Rows[0]["HO_TEN"])));
                        xmlWriter917Xml1.WriteElementString("NGAY_SINH", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_SINH"]));
                        xmlWriter917Xml1.WriteElementString("GIOI_TINH", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["GIOI_TINH"]));
                        xmlWriter917Xml1.WriteStartElement("DIA_CHI");
                        xmlWriter917Xml1.WriteCData(Utility.sDbnull(_dtXml.Tables[0].Rows[0]["DIA_CHI"]));
                        xmlWriter917Xml1.WriteEndElement();
                     //   xmlWriter917Xml1.WriteElementString("DIA_CHI",string.Format("<![CDATA[{0}]]>",Utility.sDbnull(_dtXml.Tables[0].Rows[0]["DIA_CHI"])));
                        xmlWriter917Xml1.WriteElementString("MA_THE", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_THE"]));
                        xmlWriter917Xml1.WriteElementString("MA_DKBD", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_DKBD"]));
                        xmlWriter917Xml1.WriteElementString("GT_THE_TU",  Utility.sDbnull(_dtXml.Tables[0].Rows[0]["GT_THE_TU"]));
                        xmlWriter917Xml1.WriteElementString("GT_THE_DEN", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["GT_THE_DEN"]));
                        xmlWriter917Xml1.WriteElementString("MA_BENH",Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_BENH"]));
                        xmlWriter917Xml1.WriteElementString("MA_BENHKHAC",  Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_BENHKHAC"]));
                        xmlWriter917Xml1.WriteStartElement("TEN_BENH");
                        xmlWriter917Xml1.WriteCData(Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TEN_BENH"]));
                        xmlWriter917Xml1.WriteEndElement();
                      //  xmlWriter917Xml1.WriteElementString("TEN_BENH",string.Format("<![CDATA[{0}]]>",Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TEN_BENH"])));
                        xmlWriter917Xml1.WriteElementString("MA_LYDO_VVIEN",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LYDO_VVIEN"]));
                        xmlWriter917Xml1.WriteElementString("MA_NOI_CHUYEN",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_NOI_CHUYEN"]));
                        xmlWriter917Xml1.WriteElementString("MA_TAI_NAN",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_TAI_NAN"]));
                        xmlWriter917Xml1.WriteElementString("NGAY_VAO",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_VAO"]));
                        xmlWriter917Xml1.WriteElementString("NGAY_RA",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_RA"]));
                        xmlWriter917Xml1.WriteElementString("SO_NGAY_DTRI",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["SO_NGAY_DTRI"]));
                        xmlWriter917Xml1.WriteElementString("KET_QUA_DTRI",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["KET_QUA_DTRI"]));
                        xmlWriter917Xml1.WriteElementString("TINH_TRANG_RV",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TINH_TRANG_RV"]));
                        xmlWriter917Xml1.WriteElementString("NGAY_TTOAN",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_TTOAN"]));
                        xmlWriter917Xml1.WriteElementString("MUC_HUONG",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MUC_HUONG"]));
                        xmlWriter917Xml1.WriteElementString("T_THUOC",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_THUOC"]));
                        xmlWriter917Xml1.WriteElementString("T_VTYT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_VTYT"]));
                        xmlWriter917Xml1.WriteElementString("T_TONGCHI",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_TONGCHI"]));
                        xmlWriter917Xml1.WriteElementString("T_BNTT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_BNTT"]));
                        xmlWriter917Xml1.WriteElementString("T_BHTT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_BHTT"]));
                        xmlWriter917Xml1.WriteElementString("T_NGUONKHAC",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_NGUONKHAC"]));
                        xmlWriter917Xml1.WriteElementString("T_NGOAIDS",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["T_NGOAIDS"]));
                        xmlWriter917Xml1.WriteElementString("NAM_QT", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NAM_QT"]));
                        xmlWriter917Xml1.WriteElementString("THANG_QT",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["THANG_QT"]));
                        xmlWriter917Xml1.WriteElementString("MA_LOAI_KCB",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LOAI_KCB"]));
                        xmlWriter917Xml1.WriteElementString("MA_KHOA",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_KHOA"]));
                        xmlWriter917Xml1.WriteElementString("MA_CSKCB",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_CSKCB"]));
                        xmlWriter917Xml1.WriteElementString("MA_KHUVUC",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_KHUVUC"], "_"));
                        xmlWriter917Xml1.WriteElementString("MA_PTTT_QT",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_PTTT_QT"], "_"));
                        xmlWriter917Xml1.WriteElementString("CAN_NANG",
                            Utility.sDbnull(_dtXml.Tables[0].Rows[0]["CAN_NANG"]));
                        xmlWriter917Xml1.WriteEndElement();
                        xmlWriter917Xml1.Flush();
                    }
                    catch (Exception)
                    {
                        LogText(
                            string.Format("Lỗi file XML1 của bệnh nhân {0}",
                                Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LK"])), Color.Red);
                    }
                    finally
                    {
                        if (xmlWriter917Xml1 != null)
                        {
                            xmlWriter917Xml1.Close();
                        }
                    }
                   
                }
                #endregion

                // Ghi File XML2 

                #region Ghi File XML2

                if (_dtXml.Tables[1].Rows.Count > 0)
                {
                    _sFileName = Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_CSKCB"]) + "_" +
                         Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_VAO"]) + "_" +
                          Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_THE"]) + "_XML2.xml";
                    string sDirectoryxml2 = _sLocalPath;
                    if (!Directory.Exists(sDirectoryxml2)) Directory.CreateDirectory(sDirectoryxml2);
                    _sLocalFilePath = _sLocalPath + "\\" + _sFileName;

                    using (XmlWriter xmlWriter917Xml2 = XmlWriter.Create(_sLocalFilePath, _xmlWriterSettings))
                    {
                        try
                        {
                            xmlWriter917Xml2.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char)34)));
                            xmlWriter917Xml2.WriteStartElement("DSACH_CHI_TIET_THUOC");
                            foreach (DataRow row in _dtXml.Tables[1].Rows)
                            {
                                xmlWriter917Xml2.WriteStartElement("CHI_TIET_THUOC");
                                xmlWriter917Xml2.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                                xmlWriter917Xml2.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                                xmlWriter917Xml2.WriteElementString("MA_THUOC", Utility.sDbnull(row["MA_THUOC"]));
                                xmlWriter917Xml2.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                                xmlWriter917Xml2.WriteStartElement("TEN_THUOC");
                                xmlWriter917Xml2.WriteCData(Utility.sDbnull(row["TEN_THUOC"]));
                                xmlWriter917Xml2.WriteEndElement();
                                //xmlWriter917Xml2.WriteElementString("TEN_THUOC", string.Format("<![CDATA[{0}]]>",
                                //                Utility.sDbnull(row["TEN_THUOC"])));
                                xmlWriter917Xml2.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                                   xmlWriter917Xml2.WriteStartElement("HAM_LUONG");
                                   xmlWriter917Xml2.WriteCData(Utility.sDbnull(row["HAM_LUONG"]));
                                xmlWriter917Xml2.WriteEndElement();
                               
                                //xmlWriter917Xml2.WriteElementString("HAM_LUONG", string.Format("<![CDATA[{0}]]>",
                                //                Utility.sDbnull(row["HAM_LUONG"])));
                                xmlWriter917Xml2.WriteElementString("DUONG_DUNG", Utility.sDbnull(row["DUONG_DUNG"]));
                                xmlWriter917Xml2.WriteStartElement("LIEU_DUNG");
                                xmlWriter917Xml2.WriteCData(Utility.sDbnull(row["LIEU_DUNG"]));
                                xmlWriter917Xml2.WriteEndElement();
                                //xmlWriter917Xml2.WriteElementString("LIEU_DUNG", string.Format("<![CDATA[{0}]]>",
                                //                Utility.sDbnull(row["LIEU_DUNG"])));
                                xmlWriter917Xml2.WriteElementString("SO_DANG_KY", Utility.sDbnull(row["SO_DANG_KY"]));
                                xmlWriter917Xml2.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                                xmlWriter917Xml2.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                                xmlWriter917Xml2.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                                xmlWriter917Xml2.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                                xmlWriter917Xml2.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                                xmlWriter917Xml2.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                                xmlWriter917Xml2.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                                xmlWriter917Xml2.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                                xmlWriter917Xml2.WriteElementString("MA_PTTT", Utility.sDbnull(row["MA_PTTT"]));

                                xmlWriter917Xml2.WriteEndElement();
                            }
                            xmlWriter917Xml2.WriteEndElement();
                            xmlWriter917Xml2.Flush();
                        }
                        catch (Exception ex)
                        {

                            LogText(
                                string.Format("Lỗi file XML2 của bệnh nhân {0}",
                                    Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LK"])), Color.Red);
                        }
                        finally
                        {
                            if (xmlWriter917Xml2 != null)
                            {
                                xmlWriter917Xml2.Close();
                            }
                        }
                        
                    }
                 //   sXML2 = Convert.ToBase64String(encodingXML2.GetBytes(sbXML2.ToString()));
                }

                #endregion

                // Ghi File XML 3 

                #region Ghi File XML 3

                if (_dtXml.Tables[2].Rows.Count > 0)
                {
                    _sFileName = Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_CSKCB"]) + "_" +
                         Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAY_VAO"]) + "_" +
                          Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_THE"]) + "_XML3.xml";
                    string sDirectoryxml3 = _sLocalPath;
                    if (!Directory.Exists(sDirectoryxml3)) Directory.CreateDirectory(sDirectoryxml3);
                    _sLocalFilePath = _sLocalPath + "\\" + _sFileName;
                    using (XmlWriter xmlWriter917Xml3 = XmlWriter.Create(_sLocalFilePath, _xmlWriterSettings))
                    {
                        try
                        {
                            xmlWriter917Xml3.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char)34)));
                            xmlWriter917Xml3.WriteStartElement("DSACH_CHI_TIET_DVKT");
                            foreach (DataRow row in _dtXml.Tables[2].Rows)
                            {
                                xmlWriter917Xml3.WriteStartElement("CHI_TIET_DVKT");
                                xmlWriter917Xml3.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                                xmlWriter917Xml3.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                                xmlWriter917Xml3.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
                                xmlWriter917Xml3.WriteElementString("MA_VAT_TU", Utility.sDbnull(row["MA_VAT_TU"]));
                                xmlWriter917Xml3.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                                xmlWriter917Xml3.WriteStartElement("TEN_DICH_VU");
                                xmlWriter917Xml3.WriteCData(Utility.sDbnull(row["TEN_DICH_VU"]));
                                xmlWriter917Xml3.WriteEndElement();
                                //xmlWriter917Xml3.WriteElementString("TEN_DICH_VU", string.Format("<![CDATA[{0}]]>",
                                //                Utility.sDbnull(row["TEN_DICH_VU"])));
                                xmlWriter917Xml3.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                                xmlWriter917Xml3.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                                xmlWriter917Xml3.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                                xmlWriter917Xml3.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                                xmlWriter917Xml3.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                                xmlWriter917Xml3.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                                xmlWriter917Xml3.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                                xmlWriter917Xml3.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                                xmlWriter917Xml3.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                                xmlWriter917Xml3.WriteElementString("NGAY_KQ", Utility.sDbnull(row["NGAY_KQ"]));
                                xmlWriter917Xml3.WriteElementString("MA_PTTT", Utility.sDbnull(row["MA_PTTT"]));
                                xmlWriter917Xml3.WriteEndElement();
                            }
                            xmlWriter917Xml3.WriteEndElement();
                            xmlWriter917Xml3.Flush();
                        }
                        catch (Exception ex)
                        {
                            LogText(string.Format("Lỗi file XML3 của bệnh nhân {0}", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LK"])),Color.Red);
                        }
                        finally
                        {
                            if (xmlWriter917Xml3 !=null)
                            {
                                xmlWriter917Xml3.Close();
                            }
                        }
                       
                    }
                }

                #endregion
              
               
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool CreateTempTable(string ma_lankham, long id_benhnhan)
        {
            try
            {
                using (var sp = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        SPs.SpXmlThongTuBHYT917(ma_lankham, id_benhnhan).Execute();
                        sh.Dispose();
                    }
                    sp.Complete();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void LogText(string sLogText, Color sActionColor)
        {
                if (InvokeRequired)
                {
                    Invoke(new AddLog(LogText), new object[] {sLogText, sActionColor});
                }
                else
                {
                    AddAction(sLogText, sActionColor);
                    //rtxtLogs.AppendText(sLogText);
                    rtxtLogs.AppendText(_sNewline);
                    //TextBoxTraceListener.SendMessage(_richTextBoxLog.Handle, TextBoxTraceListener.WM_VSCROLL, TextBoxTraceListener.SB_BOTTOM, 0);
                }
        }

        private void AddAction(string sLogText, Color color)
        {
            if (sLogText.Length > 0)
            {
                Color oldColor = rtxtLogs.SelectionColor;
                rtxtLogs.SelectionLength = 0;
                rtxtLogs.SelectionStart = rtxtLogs.Text.Length;
                rtxtLogs.SelectionColor = color;
                rtxtLogs.SelectionFont = new Font(rtxtLogs.SelectionFont, FontStyle.Bold);
                rtxtLogs.AppendText(sLogText);
                rtxtLogs.SelectionColor = oldColor;
            }
        }

        private void cmdTaoDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                rtxtLogs.Clear();
                // Reset dữ liệu bảng tạm
               
                // thực hiện insert vào bảng XML_1_917,   XML_2_917, XML_3_917, XML_4_917, XML_5_917
                int i = 0, j = 0;
                Utility.ResetProgressBar(prgBar, grdList.GetCheckedRows().Count(), true);
                foreach (GridEXRow row in grdList.GetCheckedRows())
                {
                    string maluotKham = Utility.sDbnull(row.Cells["ma_luotkham"].Value);
                    long idBenhnhan = Utility.Int32Dbnull(row.Cells["id_benhnhan"].Value);
                    string maphieudongttra = Utility.sDbnull(row.Cells["ma_phieu_dct"].Value);
                    if (!string.IsNullOrEmpty(maphieudongttra))
                    {
                        SPs.SpXmlThongTuBHYT917Reset(maluotKham).Execute();
                        bool kt = CreateTempTable(maluotKham, idBenhnhan);
                        
                        if (kt)
                        {
                            i = i + 1;
                            LogText(
                                string.Format("{0}. Insert thành công bệnh nhân vào bảng tạm ",
                                    maluotKham), Color.DarkBlue);
                            new Update(KcbPhieuDct.Schema).Set(KcbPhieuDct.Columns.TrangthaiXml)
                                .EqualTo(1)
                                .Where(KcbPhieuDct.Columns.MaLuotkham)
                                .IsEqualTo(maluotKham)
                                .Execute();
                        }
                        else
                        {
                            j = j + 1;
                            LogText(
                                string.Format("{0}. Insert không thành công bệnh nhân vào bảng tạm ",
                                    maluotKham), Color.Red);
                        }
                    }
                    else
                    {
                        j = j + 1;
                        LogText(
                                string.Format("{0}. Bệnh nhân chưa được in phôi BHYT ",
                                    maluotKham), Color.Red);
                    }
                    
                    UIAction.SetValue4Prg(prgBar, 1);
                    Application.DoEvents();
                    row.IsChecked = false;
                }
                LogText( string.Format("Tạo dữ liệu thành công {0} hồ sơ, thất bại {1} hồ sơ", i, j
                                    ), Color.Red);
            }
            catch (Exception ex )
            {
                LogText(string.Format("Lỗi khi tạo dữ liệu" + ex.Message), Color.Red);
            }
        }

        #region ghi XML cũ 

        /// <summary>
        /// ghi dữ liệu cũ
        /// </summary>
        /// <returns></returns>
        private bool ProcessXmlWrite()
        {
            try
            {
                _xmlWriter917.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char) 34)));
                _xmlWriter917.WriteStartElement("CHECKOUT");
                if (_dtXml.Tables[0].Rows.Count > 0)
                {
                    _xmlWriter917.WriteStartElement("THONGTINBENHNHAN");
                    _xmlWriter917.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LK"]));
                    _xmlWriter917.WriteElementString("NGAYGIOVAO",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAYGIOVAO"]));
                    _xmlWriter917.WriteElementString("NGAYGIORA", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAYGIORA"]));
                    _xmlWriter917.WriteElementString("MABENHVIEN",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MABENHVIEN"]));
                    _xmlWriter917.WriteElementString("CHANDOAN", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["CHANDOAN"]));
                    _xmlWriter917.WriteElementString("TRANGTHAI", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TRANGTHAI"]));
                    _xmlWriter917.WriteElementString("KETQUA", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["KETQUA"]));
                    _xmlWriter917.WriteElementString("SODIENTHOAI_LH",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["SODIENTHOAI_LH"]));
                    _xmlWriter917.WriteElementString("NGUOILIENHE",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGUOILIENHE"]));
                    _xmlWriter917.WriteEndElement();
                }
                if (_dtXml.Tables[1].Rows.Count > 0)
                {
                    _xmlWriter917.WriteStartElement("CHUYENTUYEN");
                    _xmlWriter917.WriteElementString("SOHOSO", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["SOHOSO"]));
                    _xmlWriter917.WriteElementString("SOCHUYENTUYEN",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["SOCHUYENTUYEN"]));
                    _xmlWriter917.WriteElementString("MA_BV_CHUYENDEN",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_BV_CHUYENDEN"]));
                    _xmlWriter917.WriteElementString("MA_BV_KHAMBENH",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_BV_KHAMBENH"]));
                    _xmlWriter917.WriteElementString("TEN_CS_KHAMBENH",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["TEN_CS_KHAMBENH"]));
                    _xmlWriter917.WriteElementString("NGHENGHIEP",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["NGHENGHIEP"]));
                    _xmlWriter917.WriteElementString("NOILAMVIEC",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["NOILAMVIEC"]));
                    _xmlWriter917.WriteElementString("LAMSANG", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["LAMSANG"]));
                    _xmlWriter917.WriteElementString("KETQUAXETNGHIEM",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["KETQUAXETNGHIEM"]));
                    _xmlWriter917.WriteElementString("CHANDOAN", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["CHANDOAN"]));
                    _xmlWriter917.WriteElementString("PHUONGPHAPDIEUTRI",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["PHUONGPHAPDIEUTRI"]));
                    _xmlWriter917.WriteElementString("LYDO_CHUYENTUYEN",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["LYDO_CHUYENTUYEN"]));
                    _xmlWriter917.WriteElementString("HUONGDIEUTRI",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["HUONGDIEUTRI"]));
                    _xmlWriter917.WriteElementString("THOIGIAN_CHUYEN",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["THOIGIAN_CHUYEN"]));
                    _xmlWriter917.WriteElementString("PHUONGTIEN",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["PHUONGTIEN"]));
                    _xmlWriter917.WriteElementString("NGUOI_HOTONG",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["NGUOI_HOTONG"]));
                    _xmlWriter917.WriteElementString("MA_QUOCTICH",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_QUOCTICH"]));
                    _xmlWriter917.WriteElementString("MA_DANTOC", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_DANTOC"]));
                    _xmlWriter917.WriteStartElement("DSCHUYENVIEN");
                    _xmlWriter917.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_LK"]));
                    _xmlWriter917.WriteElementString("MABV", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MABV"]));
                    _xmlWriter917.WriteElementString("TUYEN", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["TUYEN"]));
                    _xmlWriter917.WriteElementString("TUNGAY", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["TUNGAY"]));
                    _xmlWriter917.WriteElementString("DENNGAY", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["DENNGAY"]));
                    _xmlWriter917.WriteEndElement();
                    _xmlWriter917.WriteEndElement();
                }

                if (_dtXml.Tables[2].Rows.Count > 0)
                {
                    _xmlWriter917.WriteStartElement("THONGTINCHITIET");

                    _xmlWriter917.WriteStartElement("TONGHOP");
                    _xmlWriter917.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_LK"]));
                    _xmlWriter917.WriteElementString("STT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["STT"]));
                    _xmlWriter917.WriteElementString("MA_BN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_BN"]));
                    _xmlWriter917.WriteElementString("HO_TEN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["HO_TEN"]));
                    _xmlWriter917.WriteElementString("NGAY_SINH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_SINH"]));
                    _xmlWriter917.WriteElementString("GIOI_TINH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["GIOI_TINH"]));
                    _xmlWriter917.WriteElementString("DIA_CHI", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["DIA_CHI"]));
                    _xmlWriter917.WriteElementString("MA_THE", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_THE"]));
                    _xmlWriter917.WriteElementString("MA_DKBD", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_DKBD"]));
                    _xmlWriter917.WriteElementString("GT_THE_TU", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["GT_THE_TU"]));
                    _xmlWriter917.WriteElementString("GT_THE_DEN",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["GT_THE_DEN"]));
                    _xmlWriter917.WriteElementString("MA_BENH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_BENH"]));
                    _xmlWriter917.WriteElementString("MA_BENHKHAC",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_BENHKHAC"]));
                    _xmlWriter917.WriteElementString("TEN_BENH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["TEN_BENH"]));
                    _xmlWriter917.WriteElementString("MA_LYDO_VVIEN",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_LYDO_VVIEN"]));
                    _xmlWriter917.WriteElementString("MA_NOI_CHUYEN",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_NOI_CHUYEN"]));
                    _xmlWriter917.WriteElementString("MA_TAI_NAN",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_TAI_NAN"]));
                    _xmlWriter917.WriteElementString("NGAY_VAO", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_VAO"]));
                    _xmlWriter917.WriteElementString("NGAY_RA", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_RA"]));
                    _xmlWriter917.WriteElementString("SO_NGAY_DTRI",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["SO_NGAY_DTRI"]));
                    _xmlWriter917.WriteElementString("KET_QUA_DTRI",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["KET_QUA_DTRI"]));
                    _xmlWriter917.WriteElementString("TINH_TRANG_RV",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["TINH_TRANG_RV"]));
                    _xmlWriter917.WriteElementString("NGAY_TTOAN",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_TTOAN"]));
                    _xmlWriter917.WriteElementString("MUC_HUONG", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MUC_HUONG"]));
                    _xmlWriter917.WriteElementString("T_THUOC", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_THUOC"]));
                    _xmlWriter917.WriteElementString("T_VTYT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_VTYT"]));
                    _xmlWriter917.WriteElementString("T_TONGCHI", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_TONGCHI"]));
                    _xmlWriter917.WriteElementString("T_BNTT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_BNTT"]));
                    _xmlWriter917.WriteElementString("T_BHTT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_BHTT"]));
                    _xmlWriter917.WriteElementString("T_NGUONKHAC",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_NGUONKHAC"]));
                    _xmlWriter917.WriteElementString("T_NGOAIDS", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_NGOAIDS"]));
                    _xmlWriter917.WriteElementString("NAM_QT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NAM_QT"]));
                    _xmlWriter917.WriteElementString("THANG_QT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["THANG_QT"]));
                    _xmlWriter917.WriteElementString("MA_LOAIKCB",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_LOAIKCB"]));
                    _xmlWriter917.WriteElementString("MA_CSKCB", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_CSKCB"]));
                    _xmlWriter917.WriteElementString("MA_KHUVUC",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_KHUVUC"], "_"));
                    _xmlWriter917.WriteElementString("MA_PTTT_QT",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_PTTT_QT"], "_"));
                    _xmlWriter917.WriteElementString("SO_PHIEU", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["SO_PHIEU"]));
                    _xmlWriter917.WriteEndElement();

                    if (_dtXml.Tables[3].Rows.Count > 0)
                    {
                        _xmlWriter917.WriteStartElement("BANG_CTTHUOC");
                        foreach (DataRow row in _dtXml.Tables[3].Rows)
                        {
                            _xmlWriter917.WriteStartElement("CTTHUOC");
                            _xmlWriter917.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            _xmlWriter917.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            _xmlWriter917.WriteElementString("MA_THUOC", Utility.sDbnull(row["MA_THUOC"]));
                            _xmlWriter917.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                            _xmlWriter917.WriteElementString("TEN_THUOC", Utility.sDbnull(row["TEN_THUOC"]));
                            _xmlWriter917.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                            _xmlWriter917.WriteElementString("HAM_LUONG", Utility.sDbnull(row["HAM_LUONG"]));
                            _xmlWriter917.WriteElementString("DUONG_DUNG", Utility.sDbnull(row["DUONG_DUNG"]));
                            _xmlWriter917.WriteElementString("SO_DANG_KY", Utility.sDbnull(row["SO_DANG_KY"]));
                            _xmlWriter917.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                            _xmlWriter917.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                            _xmlWriter917.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                            _xmlWriter917.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                            _xmlWriter917.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                            _xmlWriter917.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                            _xmlWriter917.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                            _xmlWriter917.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            _xmlWriter917.WriteElementString("TEN_KHOABV", Utility.sDbnull(row["TEN_KHOABV"]));

                            _xmlWriter917.WriteEndElement();
                        }
                        _xmlWriter917.WriteEndElement();
                    }
                    if (_dtXml.Tables[4].Rows.Count > 0)
                    {
                        _xmlWriter917.WriteStartElement("BANG_CTDV");
                        foreach (DataRow row in _dtXml.Tables[4].Rows)
                        {
                            _xmlWriter917.WriteStartElement("CTDV");
                            _xmlWriter917.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            _xmlWriter917.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            _xmlWriter917.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
                            _xmlWriter917.WriteElementString("MA_VAT_TU", Utility.sDbnull(row["MA_VAT_TU"]));
                            _xmlWriter917.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                            _xmlWriter917.WriteElementString("TEN_DICH_VU", Utility.sDbnull(row["TEN_DICH_VU"]));
                            _xmlWriter917.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                            _xmlWriter917.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                            _xmlWriter917.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                            _xmlWriter917.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                            _xmlWriter917.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                            _xmlWriter917.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                            _xmlWriter917.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                            _xmlWriter917.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                            _xmlWriter917.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            _xmlWriter917.WriteElementString("NGAY_KQ", Utility.sDbnull(row["NGAY_KQ"]));
                            _xmlWriter917.WriteElementString("TEN_KHOABV", Utility.sDbnull(row["TEN_KHOABV"]));
                            _xmlWriter917.WriteEndElement();
                        }
                        _xmlWriter917.WriteEndElement();
                    }
                    if (_dtXml.Tables[5].Rows.Count > 0)
                    {
                        _xmlWriter917.WriteStartElement("BANG_CT_CLS");
                        foreach (DataRow row in _dtXml.Tables[5].Rows)
                        {
                            _xmlWriter917.WriteStartElement("CLS");
                            _xmlWriter917.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            _xmlWriter917.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            _xmlWriter917.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
                            _xmlWriter917.WriteElementString("MA_CHI_SO", Utility.sDbnull(row["MA_CHI_SO"]));
                            _xmlWriter917.WriteElementString("TEN_CHI_SO", Utility.sDbnull(row["TEN_CHI_SO"]));
                            _xmlWriter917.WriteElementString("GIA_TRI", Utility.sDbnull(row["GIA_TRI"]));
                            _xmlWriter917.WriteElementString("MA_MAY", Utility.sDbnull(row["MA_MAY"]));
                            _xmlWriter917.WriteElementString("MO_TA", Utility.sDbnull(row["MO_TA"]));
                            _xmlWriter917.WriteElementString("KET_LUAN", Utility.sDbnull(row["KET_LUAN"]));
                            _xmlWriter917.WriteEndElement();
                        }
                        _xmlWriter917.WriteEndElement();
                    }
                    if (_dtXml.Tables[6].Rows.Count > 0)
                    {
                        _xmlWriter917.WriteStartElement("BANG_DIENBIENBENH");
                        foreach (DataRow row in _dtXml.Tables[6].Rows)
                        {
                            _xmlWriter917.WriteStartElement("DIENBIENBENH");
                            _xmlWriter917.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            _xmlWriter917.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            _xmlWriter917.WriteElementString("DIENBIEN", Utility.sDbnull(row["DIENBIEN"]));
                            _xmlWriter917.WriteElementString("HOI_CHAN", Utility.sDbnull(row["HOI_CHAN"]));
                            _xmlWriter917.WriteElementString("PHAU_THUAT", Utility.sDbnull(row["PHAU_THUAT"]));
                            _xmlWriter917.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            _xmlWriter917.WriteEndElement();
                        }
                        _xmlWriter917.WriteEndElement();
                    }
                    _xmlWriter917.WriteFullEndElement();
                }
                _xmlWriter917.Flush();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion 

        private void mnuDelLog_Click(object sender, EventArgs e)
        {
            rtxtLogs.Clear();
        }

        private void radTongHop_CheckedChanged(object sender, EventArgs e)
        {
            if (radTongHop.Checked)
            {
                grpKieu.Visible = false;
            }
            else
            {
                grpKieu.Visible = true;
            }
        }

        private void mnuTaoDuLieu_Click(object sender, EventArgs e)
        {
            cmdTaoDuLieu.PerformClick();
        }

        private void mnuXuatDuLieu_Click(object sender, EventArgs e)
        {
            cmdExportXML.PerformClick();
        }

        private void mnuXuatExcel_Click(object sender, EventArgs e)
        {
            cmdXuatExcel.PerformClick();
        }

        private void mnuKiemtradulieu_Click(object sender, EventArgs e)
        {
            try
            {
                _dtCheckError = new DataTable("dtError");
                if (!_dtCheckError.Columns.Contains("ma_lk")) _dtCheckError.Columns.Add("ma_lk", typeof (string));
                if (!_dtCheckError.Columns.Contains("ho_ten")) _dtCheckError.Columns.Add("ho_ten", typeof (string));
                if (!_dtCheckError.Columns.Contains("ma_the")) _dtCheckError.Columns.Add("ma_the", typeof (string));
                if (!_dtCheckError.Columns.Contains("ma_dkkcb")) _dtCheckError.Columns.Add("ma_dkkcb", typeof (string));
                if (!_dtCheckError.Columns.Contains("gioi_tinh")) _dtCheckError.Columns.Add("gioi_tinh", typeof (string));
                if (!_dtCheckError.Columns.Contains("nam_sinh")) _dtCheckError.Columns.Add("nam_sinh", typeof (string));
                if (!_dtCheckError.Columns.Contains("dien_giai")) _dtCheckError.Columns.Add("dien_giai", typeof (string));
                if (!_dtCheckError.Columns.Contains("ma_loi")) _dtCheckError.Columns.Add("ma_loi", typeof (string));

                bool kt = true;
                Utility.ResetProgressBar(prgBar, grdList.GetDataRows().Count(), true);
                foreach (var row in grdList.GetDataRows())
                {
                      string maLk = Utility.sDbnull(row.Cells["ma_luotkham"].Value);
                      string hoTen = Utility.sDbnull(row.Cells["ten_benhnhan"].Value);
                      string namSinh = Utility.sDbnull(row.Cells["nam_sinh"].Value);
                      short gioiTinh = (short) (Utility.sDbnull(row.Cells["ten_benhnhan"].Value) == "Nam"? 2:1);
                      string matheBHYT = Utility.sDbnull(row.Cells["mathe_bhyt"].Value);
                      string maKcbbd = Utility.sDbnull(row.Cells["ma_kcbbd"].Value);
                      if (!string.IsNullOrEmpty(maLk) && THU_VIEN_CHUNG.Laygiatrithamsohethong("XML_CHECK_STRUCTURE", "1",false) == "1")
                      {
                        CheckDataError_structure(maLk, hoTen, matheBHYT, maKcbbd, gioiTinh, namSinh, "", "");
                          if (THU_VIEN_CHUNG.Laygiatrithamsohethong("XML_CHECK_DATA", "0", false) == "1")
                          {
                              CheckDataError_Data(maLk);
                          }
                      
                      }
                    row.IsChecked = false;
                    UIAction.SetValue4Prg(prgBar, 1);
                }
                FrmListError frm = new FrmListError();
                frm.dtError = _dtCheckError;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
       
        DataTable _dtCheckError = new DataTable();

        private bool CheckDataError_structure(string maLk, string hoTen, string maThe, string maDkbd, short gioiTinh, string ngaySinh, string messge, string maloi)
        {
            DataSet ds = SPs.XmlCheckData(maLk).GetDataSet();
            // Check tong tien xml1 = xml2+ xml3  ?
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = "Tổng tiền trong XML1 không bằng tổng tiền trong XML2 và XML3";
                row["ma_loi"] = "1.01";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, "Tổng tiền trong XML1 không bằng tổng tiền trong XML2 và XML3"), Color.Red);
                return false;
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = "Tổng tiền thuốc XML2 không bằng tiền thuốc trong XML1";
                row["ma_loi"] = "1.02";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, "Tổng tiền thuốc XML2 không bằng tiền thuốc trong XML1"), Color.Red);
                return false;
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = "Tổng tiền VTYT nhóm (10,11) trong XML3 không bằng tiền VTYT trong XML1";
                row["ma_loi"] = "1.03";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, "Tổng tiền VTYT nhóm (10,11) trong XML3 không bằng tiền VTYT trong XML1"), Color.Red);
                return false;
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = string.Format("Không tồn tại mã cơ sở khám chữa bệnh ({0}) trong hệ thống", ds.Tables[3].Rows[0]["MA_DKBD"].ToString());
                row["ma_loi"] = "1.04";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, string.Format("Không tồn tại mã cơ sở khám chữa bệnh ({0}) trong hệ thống", ds.Tables[3].Rows[0]["MA_DKBD"].ToString())), Color.Red);
                return false;
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = string.Format("Mã nơi chuyển ({0}) không có trong hệ thống", ds.Tables[4].Rows[0]["MA_NOI_CHUYEN"].ToString());
                row["ma_loi"] = "1.05";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, string.Format("Mã nơi chuyển ({0}) không có trong hệ thống", ds.Tables[4].Rows[0]["MA_NOI_CHUYEN"].ToString())), Color.Red);
                return false;
            }
            if (ds.Tables[5].Rows.Count > 0)
            {
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = "Tỷ lệ chi trả BHYT không đúng với mức hưởng";
                row["ma_loi"] = "1.06";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, string.Format("Tỷ lệ chi trả BHYT không đúng với mức hưởng")), Color.Red);
                return false;
            }
            if (ds.Tables[6].Rows.Count > 0)
            {
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = "Ngày giờ vào viện không thể lớn hơn ngày ra viện";
                row["ma_loi"] = "1.07";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, string.Format("Ngày giờ vào viện không thể lớn hơn ngày ra viện")), Color.Red);
                return false;
            }
            if (ds.Tables[7].Rows.Count > 0)
            {
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = "Ngày thanh toán không thể nhỏ hơn ngày ra hoặc ngày vào viện";
                row["ma_loi"] = "1.08";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, string.Format("Ngày thanh toán không thể nhỏ hơn ngày ra hoặc ngày vào viện")), Color.Red);
                return false;
            }
            if (ds.Tables[8].Rows.Count > 0)
            {
                string tenDichvu = "";
                tenDichvu = ds.Tables[8].Rows.Cast<DataRow>().Aggregate("", (current, r) => current + r["TEN_DICH_VU"].ToString() + "; ");

                //foreach (DataRow r in ds.Tables[8].Rows)
                //{
                //    tenDichvu = tenDichvu + r["TEN_DICH_VU"].ToString() + "; "; 
                //}
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = string.Format("Dịch vụ kỹ thuật {0} chưa được phê duyệt", tenDichvu);
                row["ma_loi"] = "1.09";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, string.Format("Mã dịch vụ kỹ thuật {0} chưa được phê duyệt", tenDichvu)), Color.Red);
                return false;
            }
            if (ds.Tables[9].Rows.Count > 0)
            {
                string tenVtyt = "";
                tenVtyt = ds.Tables[9].Rows.Cast<DataRow>().Aggregate("", (current, r) => current + r["TEN_DICH_VU"].ToString() + "; ");
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = string.Format("VTYT {0} chưa được phê duyệt", tenVtyt);
                row["ma_loi"] = "1.10";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, string.Format("VTYT {0} chưa được phê duyệt", tenVtyt)), Color.Red);
                return false;
            }
            if (ds.Tables[10].Rows.Count > 0)
            {
                string tenThuoc = "";
                tenThuoc = ds.Tables[10].Rows.Cast<DataRow>().Aggregate("", (current, r) => current + r["TEN_DICH_VU"].ToString() + "; ");
                DataRow row = _dtCheckError.NewRow();
                row["ma_lk"] = maLk;
                row["ho_ten"] = hoTen;
                row["ma_the"] = maThe;
                row["ma_dkkcb"] = maDkbd;
                row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                row["nam_sinh"] = ngaySinh;
                row["dien_giai"] = string.Format("Thuốc {0} chưa được phê duyệt", tenThuoc);
                row["ma_loi"] = "1.05";
                _dtCheckError.Rows.Add(row);
                LogText(string.Format("{0}. {1}", maLk, string.Format("Thuốc {0} chưa được phê duyệt", tenThuoc)), Color.Red);
                return false;
            }
            return true;
        }
        private bool CheckDataError_Data(string ma_lk)
        {
            DataTable dt = new Select().From(Xml1917.Schema).Where(Xml1917.Columns.MaLk).IsEqualTo(ma_lk).ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                string maThe = dt.Rows[0]["ma_the"].ToString();
                string hoTen = dt.Rows[0]["ho_ten"].ToString();
                string ngaySinh = dt.Rows[0]["ngay_sinh"].ToString().Substring(0, 4);
                short gioiTinh = Convert.ToSByte(dt.Rows[0]["gioi_tinh"]);
                string maDkbd = dt.Rows[0]["ma_dkbd"].ToString();
                string gtTheTu = dt.Rows[0]["gt_the_tu"].ToString();
                string gtTheDen = dt.Rows[0]["gt_the_den"].ToString();
                 string username = THU_VIEN_CHUNG.Laygiatrithamsohethong_off("BHYT_USERNAME", "27025_BV", false);
                 string passWord = THU_VIEN_CHUNG.Laygiatrithamsohethong_off("BHYT_PASSWORD", "khtc2014", false);
                string messge = "";
                string maloi = "";
                var thongtin = new CheckHoSoThongTuyen.KQChiTietThe();
                var check = new CheckHoSoThongTuyen.CheckCard();
                check.CheckTheThongTuyen(maThe, hoTen, ngaySinh, gioiTinh, maDkbd, gtTheTu, gtTheDen, username, passWord,
                    ref messge, ref  maloi, ref thongtin);
                if (maloi != "000")
                {
                    DataRow row = _dtCheckError.NewRow();
                    row["ma_lk"] = ma_lk;
                    row["ho_ten"] = hoTen;
                    row["ma_the"] = maThe;
                    row["ma_dkkcb"] = maDkbd;
                    row["gioi_tinh"] = gioiTinh == 2 ? "Nữ" : "Nam";
                    row["nam_sinh"] = ngaySinh;
                    row["dien_giai"] = messge;
                    row["ma_loi"] = maloi;
                    _dtCheckError.Rows.Add(row);
                    LogText(string.Format("{0}. {1}", ma_lk, messge), Color.Red);
                    return false;
                }
            }
            return true;
        }
    }
}