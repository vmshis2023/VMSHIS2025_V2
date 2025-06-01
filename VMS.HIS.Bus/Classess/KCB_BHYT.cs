using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using System.Xml;
using NLog;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Classess
{
    public class KCB_BHYT
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
        
        public  bool ProcessCreateTemp(string maluotkham, long idBenhnhan)
        {
            try
            {

                using (var sp = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        SPs.SpXmlThongTuBHYT917(maluotkham, idBenhnhan).Execute();
                        sh.Dispose();
                    }
                    sp.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Trace("Lỗi:" + ex.Message);
                return false;
            }
        }

        public bool ProcessCreateXml(string maluotKham, string _path)
        {
            try
            {
                _sLocalPath = _path;
                if (!Directory.Exists(_sLocalPath)) Directory.CreateDirectory(_sLocalPath);
                _xmlWriterSettings = new XmlWriterSettings
                {
                    Encoding = new UnicodeEncoding(false, false),
                    Indent = true,
                    IndentChars = "\t",
                    OmitXmlDeclaration = true
                };
                _dtXml = SPs.SpXmlThongTuBHYT917GetData(Utility.sDbnull(maluotKham)).GetDataSet();
                if (_dtXml.Tables[0].Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu để chuyển!");
                    return false;
                }
                    
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
                    xmlWriter917Xml1.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char)34)));
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
                    _macskcb = globalVariables.gv_strNoicapBHYT + globalVariables.gv_strNoiDKKCBBD;
                    _xmlWriter917.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0}", ((char)34)));
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
            catch (Exception ex)
            {
                log.Trace("Lỗi:" + ex.Message);
                return false;
            }
        }
    }
}