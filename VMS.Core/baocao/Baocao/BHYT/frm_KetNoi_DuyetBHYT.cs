using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;
using VNS.Libs.AppUI;
using VNS.Properties;
using SortOrder = Janus.Windows.GridEX.SortOrder;

namespace VNS.HIS.UI.Forms.Baocao
{
    public partial class frm_KetNoi_DuyetBHYT : Form
    {
        private bool _bHasloaded;
        private DataSet _dtXml = new DataSet();
        private DataTable _mDtTimKiem = new DataTable();
        private string _sFileName;
        private string _sLocalFilePath;
        private string _sLocalPath = Application.StartupPath + "XML";
        private XmlWriter _xmlWriter;

        public frm_KetNoi_DuyetBHYT()
        {
            InitializeComponent();

            dtToDate.Value = dtFromDate.Value = globalVariables.SysDate;
            Utility.VisiableGridEx(grdList, KcbPhieuDct.Columns.IdPhieuDct, globalVariables.IsAdmin);
            PropertyLib._xmlproperties = PropertyLib.GetXMLProperties();
        }

        private string MalanKam { get; set; }

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }

        private void frm_Danhsach_benhnhan_inphoi_BHYT_Load(object sender, EventArgs e)
        {

            _bw.DoWork += BwPerformSearch;
            _bw.RunWorkerCompleted += BwRunWorkerCompleted;
        }
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            if (!_bw.IsBusy)
            {
                _bw.RunWorkerAsync();
            }
            else
            {
                txtMessageDisplay.Text = string.Format("Tìm thấy {0} bệnh nhân !", _mDtTimKiem.Rows.Count);
            }

            ModifyCommand();
        }

        private void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtMessageDisplay.Text = string.Format("Tìm thấy {0} bệnh nhân !", _mDtTimKiem.Rows.Count);
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Visible = false;
        }

        private void BwPerformSearch(object sender, DoWorkEventArgs doWorkEventArgs)
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
                if (!chkByDate.Checked) dtFromDate.Value = Convert.ToDateTime("1900-01-01");

                _mDtTimKiem = SPs.SpXmlThongTuBHYT917(dtFromDate.Value, dtToDate.Value, 1, trangthai, tinhtrang).GetDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx(grdList, _mDtTimKiem, true, true, "1=1", "");
                Utility.SetGridEXSortKey(grdList, KcbPhieuDct.Columns.IdPhieuDct, SortOrder.Ascending);
                _bHasloaded = true;
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình lấy thông tin:" + ex);
            }
        }
        private readonly BackgroundWorker _bw = new BackgroundWorker();
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
        }

        /// <summary>
        ///     hàm thực hiện việc thay đổi thông tin của phần mã lần khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
                //CauHinhKCB();
            }
        }

        private void cmdExportXML_Click(object sender, EventArgs e)
        {
            try
            {
                // var xmlWriterSettings = new XmlWriterSettings()
                // {
                //     Indent = true,
                //     IndentChars = "\t"
                //     //NewLineOnAttributes = true
                // };
                // xmlWriter = XmlWriter.Create("C:\\abc.xml", xmlWriterSettings);
                //// xmlWriter.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0} encoding={0}UTF-8{0} standalone={0}yes{0}", ((char)34)));
                // xmlWriter.WriteStartElement("CHECKOUT");
                // xmlWriter.WriteStartElement("CHECKOUT1");
                // xmlWriter.WriteStartElement("CHECKOUT2");
                // xmlWriter.WriteElementString("a", "aaa");
                // xmlWriter.WriteElementString("b", "bbb");
                // xmlWriter.WriteElementString("c", "ccc");
                // xmlWriter.WriteElementString("d", "ddd");
                // xmlWriter.WriteEndElement();
                // xmlWriter.Flush();
                // this.UseWaitCursor = true;
                int i = 0;
                Utility.ResetProgressBar(progressBar, grdList.RowCount, true);
                foreach (GridEXRow row in grdList.GetCheckedRows())
                {
                    string maluotKham = Utility.sDbnull(row.Cells["ma_luotkham"].Value);
                    int idBenhnhan = Utility.Int32Dbnull(row.Cells["id_benhnhan"].Value);
                    bool kt = ProcessCreateXml(maluotKham, idBenhnhan);
                    if (kt)
                    {
                        new Update(KcbPhieuDct.Schema).Set(KcbPhieuDct.Columns.TrangthaiXml).EqualTo(1).Where(
                            KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).And(KcbPhieuDct.Columns.MaLuotkham).
                            IsEqualTo(maluotKham).Execute();
                        i = i + 1;
                    }

                    UIAction.SetValue4Prg(progressBar, 1);
                    // row.Cells["trangthai_xml"].Value = 1;
                }
                Utility.SetMsg(lblmsg, string.Format("Tổng số File XML là {0} file", i), false);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                if (_xmlWriter != null)
                {
                    //xmlWriter.WriteEndElement();
                    //xmlWriter.WriteEndDocument();
                    _xmlWriter.Close();
                }
                //  this.UseWaitCursor = false;
            }
        }

        private bool ProcessCreateXml(string maluotKham, int idBenhnhan)
        {
            try
            {
                _dtXml = SPs.ViettelLaythongtinDuyetbaohiem(Utility.sDbnull(maluotKham, ""),
                    Utility.Int32Dbnull(idBenhnhan, -1)).GetDataSet();
                if (_dtXml.Tables[0].Rows.Count <= 0) return false;

                _sLocalPath = Utility.sDbnull(PropertyLib._xmlproperties.Chonduongdan);
                if (!Directory.Exists(_sLocalPath)) Directory.CreateDirectory(_sLocalPath);
                var xmlWriterSettings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "\t",
                    OmitXmlDeclaration = true
                };
                _sFileName = Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_VAO"]) + "_" +
                             Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_THE"]) + "_CheckOut.xml";
                string sDirectory = _sLocalPath;
                if (!Directory.Exists(sDirectory)) Directory.CreateDirectory(sDirectory);
                _sLocalFilePath = _sLocalPath + "\\" + _sFileName;

                _xmlWriter = XmlWriter.Create(_sLocalFilePath, xmlWriterSettings);

                bool kt = ProcessXmlWrite();
                if (kt)
                    grdList.UnCheckAllRecords();

                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                return false;
            }
            finally
            {
                //xmlWriter.WriteEndElement();
                //xmlWriter.WriteEndDocument();
                if (_xmlWriter != null)
                    _xmlWriter.Close();
            }
        }
        /// <summary>
        /// Code theo thông tư 917
        /// </summary>
        /// <returns></returns>
        private bool ProcessXmlWrite()
        {
            try
            {
                // Xuất File XML1 
                _xmlWriter.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char)34)));
                _xmlWriter.WriteStartElement("TONG_HOP");
                if (_dtXml.Tables[0].Rows.Count > 0)
                {
                    _xmlWriter.WriteStartElement("THONGTINBENHNHAN");
                    _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LK"]));
                    _xmlWriter.WriteElementString("NGAYGIOVAO", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAYGIOVAO"]));
                    _xmlWriter.WriteElementString("NGAYGIORA", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAYGIORA"]));
                    _xmlWriter.WriteElementString("MABENHVIEN", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MABENHVIEN"]));
                    _xmlWriter.WriteElementString("CHANDOAN", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["CHANDOAN"]));
                    _xmlWriter.WriteElementString("TRANGTHAI", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TRANGTHAI"]));
                    _xmlWriter.WriteElementString("KETQUA", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["KETQUA"]));
                    _xmlWriter.WriteElementString("SODIENTHOAI_LH",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["SODIENTHOAI_LH"]));
                    _xmlWriter.WriteElementString("NGUOILIENHE",
                        Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGUOILIENHE"]));
                    _xmlWriter.WriteEndElement();
                }
                if (_dtXml.Tables[1].Rows.Count > 0)
                {
                    _xmlWriter.WriteStartElement("CHUYENTUYEN");
                    _xmlWriter.WriteElementString("SOHOSO", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["SOHOSO"]));
                    _xmlWriter.WriteElementString("SOCHUYENTUYEN",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["SOCHUYENTUYEN"]));
                    _xmlWriter.WriteElementString("MA_BV_CHUYENDEN",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_BV_CHUYENDEN"]));
                    _xmlWriter.WriteElementString("MA_BV_KHAMBENH",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_BV_KHAMBENH"]));
                    _xmlWriter.WriteElementString("TEN_CS_KHAMBENH",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["TEN_CS_KHAMBENH"]));
                    _xmlWriter.WriteElementString("NGHENGHIEP", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["NGHENGHIEP"]));
                    _xmlWriter.WriteElementString("NOILAMVIEC", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["NOILAMVIEC"]));
                    _xmlWriter.WriteElementString("LAMSANG", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["LAMSANG"]));
                    _xmlWriter.WriteElementString("KETQUAXETNGHIEM",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["KETQUAXETNGHIEM"]));
                    _xmlWriter.WriteElementString("CHANDOAN", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["CHANDOAN"]));
                    _xmlWriter.WriteElementString("PHUONGPHAPDIEUTRI",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["PHUONGPHAPDIEUTRI"]));
                    _xmlWriter.WriteElementString("LYDO_CHUYENTUYEN",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["LYDO_CHUYENTUYEN"]));
                    _xmlWriter.WriteElementString("HUONGDIEUTRI",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["HUONGDIEUTRI"]));
                    _xmlWriter.WriteElementString("THOIGIAN_CHUYEN",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["THOIGIAN_CHUYEN"]));
                    _xmlWriter.WriteElementString("PHUONGTIEN", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["PHUONGTIEN"]));
                    _xmlWriter.WriteElementString("NGUOI_HOTONG",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["NGUOI_HOTONG"]));
                    _xmlWriter.WriteElementString("MA_QUOCTICH",
                        Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_QUOCTICH"]));
                    _xmlWriter.WriteElementString("MA_DANTOC", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_DANTOC"]));
                    _xmlWriter.WriteStartElement("DSCHUYENVIEN");
                    _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_LK"]));
                    _xmlWriter.WriteElementString("MABV", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MABV"]));
                    _xmlWriter.WriteElementString("TUYEN", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["TUYEN"]));
                    _xmlWriter.WriteElementString("TUNGAY", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["TUNGAY"]));
                    _xmlWriter.WriteElementString("DENNGAY", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["DENNGAY"]));
                    _xmlWriter.WriteEndElement();
                    _xmlWriter.WriteEndElement();
                }

                if (_dtXml.Tables[2].Rows.Count > 0)
                {
                    _xmlWriter.WriteStartElement("THONGTINCHITIET");

                    _xmlWriter.WriteStartElement("TONGHOP");
                    _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_LK"]));
                    _xmlWriter.WriteElementString("STT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["STT"]));
                    _xmlWriter.WriteElementString("MA_BN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_BN"]));
                    _xmlWriter.WriteElementString("HO_TEN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["HO_TEN"]));
                    _xmlWriter.WriteElementString("NGAY_SINH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_SINH"]));
                    _xmlWriter.WriteElementString("GIOI_TINH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["GIOI_TINH"]));
                    _xmlWriter.WriteElementString("DIA_CHI", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["DIA_CHI"]));
                    _xmlWriter.WriteElementString("MA_THE", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_THE"]));
                    _xmlWriter.WriteElementString("MA_DKBD", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_DKBD"]));
                    _xmlWriter.WriteElementString("GT_THE_TU", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["GT_THE_TU"]));
                    _xmlWriter.WriteElementString("GT_THE_DEN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["GT_THE_DEN"]));
                    _xmlWriter.WriteElementString("MA_BENH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_BENH"]));
                    _xmlWriter.WriteElementString("MA_BENHKHAC",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_BENHKHAC"]));
                    _xmlWriter.WriteElementString("TEN_BENH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["TEN_BENH"]));
                    _xmlWriter.WriteElementString("MA_LYDO_VVIEN",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_LYDO_VVIEN"]));
                    _xmlWriter.WriteElementString("MA_NOI_CHUYEN",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_NOI_CHUYEN"]));
                    _xmlWriter.WriteElementString("MA_TAI_NAN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_TAI_NAN"]));
                    _xmlWriter.WriteElementString("NGAY_VAO", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_VAO"]));
                    _xmlWriter.WriteElementString("NGAY_RA", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_RA"]));
                    _xmlWriter.WriteElementString("SO_NGAY_DTRI",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["SO_NGAY_DTRI"]));
                    _xmlWriter.WriteElementString("KET_QUA_DTRI",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["KET_QUA_DTRI"]));
                    _xmlWriter.WriteElementString("TINH_TRANG_RV",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["TINH_TRANG_RV"]));
                    _xmlWriter.WriteElementString("NGAY_TTOAN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_TTOAN"]));
                    _xmlWriter.WriteElementString("MUC_HUONG", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MUC_HUONG"]));
                    _xmlWriter.WriteElementString("T_THUOC", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_THUOC"]));
                    _xmlWriter.WriteElementString("T_VTYT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_VTYT"]));
                    _xmlWriter.WriteElementString("T_TONGCHI", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_TONGCHI"]));
                    _xmlWriter.WriteElementString("T_BNTT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_BNTT"]));
                    _xmlWriter.WriteElementString("T_BHTT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_BHTT"]));
                    _xmlWriter.WriteElementString("T_NGUONKHAC",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_NGUONKHAC"]));
                    _xmlWriter.WriteElementString("T_NGOAIDS", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_NGOAIDS"]));
                    _xmlWriter.WriteElementString("NAM_QT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NAM_QT"]));
                    _xmlWriter.WriteElementString("THANG_QT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["THANG_QT"]));
                    _xmlWriter.WriteElementString("MA_LOAIKCB", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_LOAIKCB"]));
                    _xmlWriter.WriteElementString("MA_CSKCB", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_CSKCB"]));
                    _xmlWriter.WriteElementString("MA_KHUVUC",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_KHUVUC"], "_"));
                    _xmlWriter.WriteElementString("MA_PTTT_QT",
                        Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_PTTT_QT"], "_"));
                    _xmlWriter.WriteElementString("SO_PHIEU", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["SO_PHIEU"]));
                    _xmlWriter.WriteEndElement();

                    if (_dtXml.Tables[3].Rows.Count > 0)
                    {
                        _xmlWriter.WriteStartElement("BANG_CTTHUOC");
                        foreach (DataRow row in _dtXml.Tables[3].Rows)
                        {
                            _xmlWriter.WriteStartElement("CTTHUOC");
                            _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            _xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            _xmlWriter.WriteElementString("MA_THUOC", Utility.sDbnull(row["MA_THUOC"]));
                            _xmlWriter.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                            _xmlWriter.WriteElementString("TEN_THUOC", Utility.sDbnull(row["TEN_THUOC"]));
                            _xmlWriter.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                            _xmlWriter.WriteElementString("HAM_LUONG", Utility.sDbnull(row["HAM_LUONG"]));
                            _xmlWriter.WriteElementString("DUONG_DUNG", Utility.sDbnull(row["DUONG_DUNG"]));
                            _xmlWriter.WriteElementString("SO_DANG_KY", Utility.sDbnull(row["SO_DANG_KY"]));
                            _xmlWriter.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                            _xmlWriter.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                            _xmlWriter.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                            _xmlWriter.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                            _xmlWriter.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                            _xmlWriter.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                            _xmlWriter.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                            _xmlWriter.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            _xmlWriter.WriteElementString("TEN_KHOABV", Utility.sDbnull(row["TEN_KHOABV"]));

                            _xmlWriter.WriteEndElement();
                        }
                        _xmlWriter.WriteEndElement();
                    }
                    if (_dtXml.Tables[4].Rows.Count > 0)
                    {
                        _xmlWriter.WriteStartElement("BANG_CTDV");
                        foreach (DataRow row in _dtXml.Tables[4].Rows)
                        {
                            _xmlWriter.WriteStartElement("CTDV");
                            _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            _xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            _xmlWriter.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
                            _xmlWriter.WriteElementString("MA_VAT_TU", Utility.sDbnull(row["MA_VAT_TU"]));
                            _xmlWriter.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                            _xmlWriter.WriteElementString("TEN_DICH_VU", Utility.sDbnull(row["TEN_DICH_VU"]));
                            _xmlWriter.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                            _xmlWriter.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                            _xmlWriter.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                            _xmlWriter.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                            _xmlWriter.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                            _xmlWriter.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                            _xmlWriter.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                            _xmlWriter.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                            _xmlWriter.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            _xmlWriter.WriteElementString("NGAY_KQ", Utility.sDbnull(row["NGAY_KQ"]));
                            _xmlWriter.WriteElementString("TEN_KHOABV", Utility.sDbnull(row["TEN_KHOABV"]));
                            _xmlWriter.WriteEndElement();
                        }
                        _xmlWriter.WriteEndElement();
                    }
                    if (_dtXml.Tables[5].Rows.Count > 0)
                    {
                        _xmlWriter.WriteStartElement("BANG_CT_CLS");
                        foreach (DataRow row in _dtXml.Tables[5].Rows)
                        {
                            _xmlWriter.WriteStartElement("CLS");
                            _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            _xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            _xmlWriter.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
                            _xmlWriter.WriteElementString("MA_CHI_SO", Utility.sDbnull(row["MA_CHI_SO"]));
                            _xmlWriter.WriteElementString("TEN_CHI_SO", Utility.sDbnull(row["TEN_CHI_SO"]));
                            _xmlWriter.WriteElementString("GIA_TRI", Utility.sDbnull(row["GIA_TRI"]));
                            _xmlWriter.WriteElementString("MA_MAY", Utility.sDbnull(row["MA_MAY"]));
                            _xmlWriter.WriteElementString("MO_TA", Utility.sDbnull(row["MO_TA"]));
                            _xmlWriter.WriteElementString("KET_LUAN", Utility.sDbnull(row["KET_LUAN"]));
                            _xmlWriter.WriteEndElement();
                        }
                        _xmlWriter.WriteEndElement();
                    }
                    if (_dtXml.Tables[6].Rows.Count > 0)
                    {
                        _xmlWriter.WriteStartElement("BANG_DIENBIENBENH");
                        foreach (DataRow row in _dtXml.Tables[6].Rows)
                        {
                            _xmlWriter.WriteStartElement("DIENBIENBENH");
                            _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            _xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            _xmlWriter.WriteElementString("DIENBIEN", Utility.sDbnull(row["DIENBIEN"]));
                            _xmlWriter.WriteElementString("HOI_CHAN", Utility.sDbnull(row["HOI_CHAN"]));
                            _xmlWriter.WriteElementString("PHAU_THUAT", Utility.sDbnull(row["PHAU_THUAT"]));
                            _xmlWriter.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            _xmlWriter.WriteEndElement();
                        }
                        _xmlWriter.WriteEndElement();
                    }
                    _xmlWriter.WriteFullEndElement();
                }
                _xmlWriter.Flush();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //private bool ProcessXmlWrite()
        //{
        //    try
        //    {
        //        _xmlWriter.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char) 34)));
        //        _xmlWriter.WriteStartElement("CHECKOUT");
        //        if (_dtXml.Tables[0].Rows.Count > 0)
        //        {
        //            _xmlWriter.WriteStartElement("THONGTINBENHNHAN");
        //            _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MA_LK"]));
        //            _xmlWriter.WriteElementString("NGAYGIOVAO", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAYGIOVAO"]));
        //            _xmlWriter.WriteElementString("NGAYGIORA", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGAYGIORA"]));
        //            _xmlWriter.WriteElementString("MABENHVIEN", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["MABENHVIEN"]));
        //            _xmlWriter.WriteElementString("CHANDOAN", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["CHANDOAN"]));
        //            _xmlWriter.WriteElementString("TRANGTHAI", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["TRANGTHAI"]));
        //            _xmlWriter.WriteElementString("KETQUA", Utility.sDbnull(_dtXml.Tables[0].Rows[0]["KETQUA"]));
        //            _xmlWriter.WriteElementString("SODIENTHOAI_LH",
        //                Utility.sDbnull(_dtXml.Tables[0].Rows[0]["SODIENTHOAI_LH"]));
        //            _xmlWriter.WriteElementString("NGUOILIENHE",
        //                Utility.sDbnull(_dtXml.Tables[0].Rows[0]["NGUOILIENHE"]));
        //            _xmlWriter.WriteEndElement();
        //        }
        //        if (_dtXml.Tables[1].Rows.Count > 0)
        //        {
        //            _xmlWriter.WriteStartElement("CHUYENTUYEN");
        //            _xmlWriter.WriteElementString("SOHOSO", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["SOHOSO"]));
        //            _xmlWriter.WriteElementString("SOCHUYENTUYEN",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["SOCHUYENTUYEN"]));
        //            _xmlWriter.WriteElementString("MA_BV_CHUYENDEN",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_BV_CHUYENDEN"]));
        //            _xmlWriter.WriteElementString("MA_BV_KHAMBENH",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_BV_KHAMBENH"]));
        //            _xmlWriter.WriteElementString("TEN_CS_KHAMBENH",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["TEN_CS_KHAMBENH"]));
        //            _xmlWriter.WriteElementString("NGHENGHIEP", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["NGHENGHIEP"]));
        //            _xmlWriter.WriteElementString("NOILAMVIEC", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["NOILAMVIEC"]));
        //            _xmlWriter.WriteElementString("LAMSANG", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["LAMSANG"]));
        //            _xmlWriter.WriteElementString("KETQUAXETNGHIEM",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["KETQUAXETNGHIEM"]));
        //            _xmlWriter.WriteElementString("CHANDOAN", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["CHANDOAN"]));
        //            _xmlWriter.WriteElementString("PHUONGPHAPDIEUTRI",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["PHUONGPHAPDIEUTRI"]));
        //            _xmlWriter.WriteElementString("LYDO_CHUYENTUYEN",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["LYDO_CHUYENTUYEN"]));
        //            _xmlWriter.WriteElementString("HUONGDIEUTRI",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["HUONGDIEUTRI"]));
        //            _xmlWriter.WriteElementString("THOIGIAN_CHUYEN",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["THOIGIAN_CHUYEN"]));
        //            _xmlWriter.WriteElementString("PHUONGTIEN", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["PHUONGTIEN"]));
        //            _xmlWriter.WriteElementString("NGUOI_HOTONG",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["NGUOI_HOTONG"]));
        //            _xmlWriter.WriteElementString("MA_QUOCTICH",
        //                Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_QUOCTICH"]));
        //            _xmlWriter.WriteElementString("MA_DANTOC", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_DANTOC"]));
        //            _xmlWriter.WriteStartElement("DSCHUYENVIEN");
        //            _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MA_LK"]));
        //            _xmlWriter.WriteElementString("MABV", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["MABV"]));
        //            _xmlWriter.WriteElementString("TUYEN", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["TUYEN"]));
        //            _xmlWriter.WriteElementString("TUNGAY", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["TUNGAY"]));
        //            _xmlWriter.WriteElementString("DENNGAY", Utility.sDbnull(_dtXml.Tables[1].Rows[0]["DENNGAY"]));
        //            _xmlWriter.WriteEndElement();
        //            _xmlWriter.WriteEndElement();
        //        }

        //        if (_dtXml.Tables[2].Rows.Count > 0)
        //        {
        //            _xmlWriter.WriteStartElement("THONGTINCHITIET");

        //            _xmlWriter.WriteStartElement("TONGHOP");
        //            _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_LK"]));
        //            _xmlWriter.WriteElementString("STT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["STT"]));
        //            _xmlWriter.WriteElementString("MA_BN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_BN"]));
        //            _xmlWriter.WriteElementString("HO_TEN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["HO_TEN"]));
        //            _xmlWriter.WriteElementString("NGAY_SINH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_SINH"]));
        //            _xmlWriter.WriteElementString("GIOI_TINH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["GIOI_TINH"]));
        //            _xmlWriter.WriteElementString("DIA_CHI", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["DIA_CHI"]));
        //            _xmlWriter.WriteElementString("MA_THE", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_THE"]));
        //            _xmlWriter.WriteElementString("MA_DKBD", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_DKBD"]));
        //            _xmlWriter.WriteElementString("GT_THE_TU", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["GT_THE_TU"]));
        //            _xmlWriter.WriteElementString("GT_THE_DEN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["GT_THE_DEN"]));
        //            _xmlWriter.WriteElementString("MA_BENH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_BENH"]));
        //            _xmlWriter.WriteElementString("MA_BENHKHAC",
        //                Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_BENHKHAC"]));
        //            _xmlWriter.WriteElementString("TEN_BENH", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["TEN_BENH"]));
        //            _xmlWriter.WriteElementString("MA_LYDO_VVIEN",
        //                Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_LYDO_VVIEN"]));
        //            _xmlWriter.WriteElementString("MA_NOI_CHUYEN",
        //                Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_NOI_CHUYEN"]));
        //            _xmlWriter.WriteElementString("MA_TAI_NAN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_TAI_NAN"]));
        //            _xmlWriter.WriteElementString("NGAY_VAO", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_VAO"]));
        //            _xmlWriter.WriteElementString("NGAY_RA", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_RA"]));
        //            _xmlWriter.WriteElementString("SO_NGAY_DTRI",
        //                Utility.sDbnull(_dtXml.Tables[2].Rows[0]["SO_NGAY_DTRI"]));
        //            _xmlWriter.WriteElementString("KET_QUA_DTRI",
        //                Utility.sDbnull(_dtXml.Tables[2].Rows[0]["KET_QUA_DTRI"]));
        //            _xmlWriter.WriteElementString("TINH_TRANG_RV",
        //                Utility.sDbnull(_dtXml.Tables[2].Rows[0]["TINH_TRANG_RV"]));
        //            _xmlWriter.WriteElementString("NGAY_TTOAN", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NGAY_TTOAN"]));
        //            _xmlWriter.WriteElementString("MUC_HUONG", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MUC_HUONG"]));
        //            _xmlWriter.WriteElementString("T_THUOC", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_THUOC"]));
        //            _xmlWriter.WriteElementString("T_VTYT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_VTYT"]));
        //            _xmlWriter.WriteElementString("T_TONGCHI", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_TONGCHI"]));
        //            _xmlWriter.WriteElementString("T_BNTT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_BNTT"]));
        //            _xmlWriter.WriteElementString("T_BHTT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_BHTT"]));
        //            _xmlWriter.WriteElementString("T_NGUONKHAC",
        //                Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_NGUONKHAC"]));
        //            _xmlWriter.WriteElementString("T_NGOAIDS", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["T_NGOAIDS"]));
        //            _xmlWriter.WriteElementString("NAM_QT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["NAM_QT"]));
        //            _xmlWriter.WriteElementString("THANG_QT", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["THANG_QT"]));
        //            _xmlWriter.WriteElementString("MA_LOAIKCB", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_LOAIKCB"]));
        //            _xmlWriter.WriteElementString("MA_CSKCB", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_CSKCB"]));
        //            _xmlWriter.WriteElementString("MA_KHUVUC",
        //                Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_KHUVUC"], "_"));
        //            _xmlWriter.WriteElementString("MA_PTTT_QT",
        //                Utility.sDbnull(_dtXml.Tables[2].Rows[0]["MA_PTTT_QT"], "_"));
        //            _xmlWriter.WriteElementString("SO_PHIEU", Utility.sDbnull(_dtXml.Tables[2].Rows[0]["SO_PHIEU"]));
        //            _xmlWriter.WriteEndElement();

        //            if (_dtXml.Tables[3].Rows.Count > 0)
        //            {
        //                _xmlWriter.WriteStartElement("BANG_CTTHUOC");
        //                foreach (DataRow row in _dtXml.Tables[3].Rows)
        //                {
        //                    _xmlWriter.WriteStartElement("CTTHUOC");
        //                    _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
        //                    _xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
        //                    _xmlWriter.WriteElementString("MA_THUOC", Utility.sDbnull(row["MA_THUOC"]));
        //                    _xmlWriter.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
        //                    _xmlWriter.WriteElementString("TEN_THUOC", Utility.sDbnull(row["TEN_THUOC"]));
        //                    _xmlWriter.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
        //                    _xmlWriter.WriteElementString("HAM_LUONG", Utility.sDbnull(row["HAM_LUONG"]));
        //                    _xmlWriter.WriteElementString("DUONG_DUNG", Utility.sDbnull(row["DUONG_DUNG"]));
        //                    _xmlWriter.WriteElementString("SO_DANG_KY", Utility.sDbnull(row["SO_DANG_KY"]));
        //                    _xmlWriter.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
        //                    _xmlWriter.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
        //                    _xmlWriter.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
        //                    _xmlWriter.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
        //                    _xmlWriter.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
        //                    _xmlWriter.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
        //                    _xmlWriter.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
        //                    _xmlWriter.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
        //                    _xmlWriter.WriteElementString("TEN_KHOABV", Utility.sDbnull(row["TEN_KHOABV"]));

        //                    _xmlWriter.WriteEndElement();
        //                }
        //                _xmlWriter.WriteEndElement();
        //            }
        //            if (_dtXml.Tables[4].Rows.Count > 0)
        //            {
        //                _xmlWriter.WriteStartElement("BANG_CTDV");
        //                foreach (DataRow row in _dtXml.Tables[4].Rows)
        //                {
        //                    _xmlWriter.WriteStartElement("CTDV");
        //                    _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
        //                    _xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
        //                    _xmlWriter.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
        //                    _xmlWriter.WriteElementString("MA_VAT_TU", Utility.sDbnull(row["MA_VAT_TU"]));
        //                    _xmlWriter.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
        //                    _xmlWriter.WriteElementString("TEN_DICH_VU", Utility.sDbnull(row["TEN_DICH_VU"]));
        //                    _xmlWriter.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
        //                    _xmlWriter.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
        //                    _xmlWriter.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
        //                    _xmlWriter.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
        //                    _xmlWriter.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
        //                    _xmlWriter.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
        //                    _xmlWriter.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
        //                    _xmlWriter.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
        //                    _xmlWriter.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
        //                    _xmlWriter.WriteElementString("NGAY_KQ", Utility.sDbnull(row["NGAY_KQ"]));
        //                    _xmlWriter.WriteElementString("TEN_KHOABV", Utility.sDbnull(row["TEN_KHOABV"]));
        //                    _xmlWriter.WriteEndElement();
        //                }
        //                _xmlWriter.WriteEndElement();
        //            }
        //            if (_dtXml.Tables[5].Rows.Count > 0)
        //            {
        //                _xmlWriter.WriteStartElement("BANG_CT_CLS");
        //                foreach (DataRow row in _dtXml.Tables[5].Rows)
        //                {
        //                    _xmlWriter.WriteStartElement("CLS");
        //                    _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
        //                    _xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
        //                    _xmlWriter.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
        //                    _xmlWriter.WriteElementString("MA_CHI_SO", Utility.sDbnull(row["MA_CHI_SO"]));
        //                    _xmlWriter.WriteElementString("TEN_CHI_SO", Utility.sDbnull(row["TEN_CHI_SO"]));
        //                    _xmlWriter.WriteElementString("GIA_TRI", Utility.sDbnull(row["GIA_TRI"]));
        //                    _xmlWriter.WriteElementString("MA_MAY", Utility.sDbnull(row["MA_MAY"]));
        //                    _xmlWriter.WriteElementString("MO_TA", Utility.sDbnull(row["MO_TA"]));
        //                    _xmlWriter.WriteElementString("KET_LUAN", Utility.sDbnull(row["KET_LUAN"]));
        //                    _xmlWriter.WriteEndElement();
        //                }
        //                _xmlWriter.WriteEndElement();
        //            }
        //            if (_dtXml.Tables[6].Rows.Count > 0)
        //            {
        //                _xmlWriter.WriteStartElement("BANG_DIENBIENBENH");
        //                foreach (DataRow row in _dtXml.Tables[6].Rows)
        //                {
        //                    _xmlWriter.WriteStartElement("DIENBIENBENH");
        //                    _xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
        //                    _xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
        //                    _xmlWriter.WriteElementString("DIENBIEN", Utility.sDbnull(row["DIENBIEN"]));
        //                    _xmlWriter.WriteElementString("HOI_CHAN", Utility.sDbnull(row["HOI_CHAN"]));
        //                    _xmlWriter.WriteElementString("PHAU_THUAT", Utility.sDbnull(row["PHAU_THUAT"]));
        //                    _xmlWriter.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
        //                    _xmlWriter.WriteEndElement();
        //                }
        //                _xmlWriter.WriteEndElement();
        //            }
        //            _xmlWriter.WriteFullEndElement();
        //        }
        //        _xmlWriter.Flush();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}