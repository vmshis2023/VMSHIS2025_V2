using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using System.IO;
using Aspose.Words;
using System.Diagnostics;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_InPhieudieutri : Form
    {
        private string _rowFilter = "1=1";
        public DataTable m_dtPhieuDieuTriChonIn = new DataTable();
        public KcbLuotkham objLuotkham;
        public DataTable m_dtPhieudieutri = new DataTable();
        bool m_blnLoaded = false;
        public frm_InPhieudieutri()
        {
            InitializeComponent();
            InitEvents();
            dtNgayInPhieu.Value = globalVariables.SysDate;
            cauhinh();
        }
        void InitEvents()
        {
            cboKhoanoitru.SelectedIndexChanged += cboKhoanoitru_SelectedIndexChanged;
        }

        void cboKhoanoitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            string department_id = Utility.sDbnull(cboKhoanoitru.SelectedValue, globalVariables.idKhoatheoMay.ToString());
            bool IsAdmin = globalVariables.IsAdmin || (globalVariablesPrivate.objNhanvien != null && Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac"));
            m_dtPhieudieutri = new KCB_THAMKHAM().NoitruTimkiemphieudieutriTheoluotkham(Utility.Bool2byte(IsAdmin), "01/01/1900", objLuotkham.MaLuotkham,
                    (int)objLuotkham.IdBenhnhan, department_id, 0);
            _rowFilter = "1=1";
            if (!chkHienthiCaDaIn.Checked)
            {
                _rowFilter = string.Format("{0}={1}", NoitruPhieudieutri.Columns.TthaiIn, 0);
            }
            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtPhieudieutri, false, true, _rowFilter, NoitruPhieudieutri.Columns.NgayDieutri + " desc");
        }
        private void cauhinh()
        {
            try
            {
                
            }
            catch (Exception)
            {
               
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện việc load thông tin của Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_InPhieudieutri_Load(object sender, EventArgs e)
        {
            DataBinding.BindDataCombox(cboKhoanoitru,
                                                THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1),
                                                DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                "---Chọn khoa nội trú---", false);
            cboKhoanoitru.SelectedIndex=Utility.GetSelectedIndex(cboKhoanoitru,globalVariables.idKhoatheoMay.ToString());
            m_blnLoaded = true;

            string department_id = Utility.sDbnull(cboKhoanoitru.SelectedValue, globalVariables.idKhoatheoMay.ToString());
            bool IsAdmin =Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac");
            m_dtPhieudieutri = new KCB_THAMKHAM().NoitruTimkiemphieudieutriTheoluotkham(Utility.Bool2byte(IsAdmin), "01/01/1900", objLuotkham.MaLuotkham,
                     (int)objLuotkham.IdBenhnhan, department_id, 0);

            _rowFilter = "1=1";
            if (!chkHienthiCaDaIn.Checked)
            {
                _rowFilter = string.Format("{0}={1}", NoitruPhieudieutri.Columns.TthaiIn, 0);
            }
            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtPhieudieutri, false, true, _rowFilter, NoitruPhieudieutri.Columns.NgayDieutri + " desc");
            grdList.MoveFirst();

            grdList.CheckAllRecords();
        }
       
        /// <summary>
        /// hàm thực hiện việc hiển thị cần in 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkHienthiCaDaIn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _rowFilter = "1=1";
                if (!chkHienthiCaDaIn.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", NoitruPhieudieutri.Columns.TthaiIn, 0);
                }
                m_dtPhieudieutri.DefaultView.RowFilter = "1=1";
                m_dtPhieudieutri.DefaultView.RowFilter = _rowFilter;
                m_dtPhieudieutri.AcceptChanges();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        /// <summary>
        /// hàm thực hiện viễ xử lý thông tin in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
        }

        private void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
           
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu điều trị y lệnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInDieuTri_Click(object sender, EventArgs e)
        {
            //INPHIEU_DIEUTRI();
            InphieuDieutri_Word();
        }
        void InphieuDieutri_Word( )
        {
            try
            {
                string pdfFileName = "";
                Utility.AutoCheckGrid(grdList);
                string lstIdPhieu =string.Join(",", (from p in grdList.GetCheckedRows() select Utility.sDbnull(p.Cells["id_phieudieutri"].Value)).ToArray<string>());
                //Kiểm tra nếu phiếu điều trị chung có id_phieu=-1 thì cần insert lại các chữ ký của bác sỹ chưa ký
                SPs.EmrTaochukyTuPhieudieutrichung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, -1, "PHIEUDIEUTRI", lstIdPhieu).Execute();
                globalVariables.dtSignInfor = SPs.EmrLaythongtinChukyPhieudieutrichung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, lstIdPhieu, "PHIEUDIEUTRI", "PHIEUDIEUTRI").GetDataSet().Tables[0];

                string chan_doan = Utility.GetChandoanInPhieuDieuTri(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, lstIdPhieu, true);
                string nguoi_tao = "";
                List<string> lstSign = new List<string>();
                DataSet dsPrint = new noitru_phieudieutri().NoitruLaythongtinphieudieutriIn(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, lstIdPhieu);
                DataTable m_dtPhieuDieutri;
                m_dtPhieuDieutri = dsPrint.Tables[0];
                m_dtPhieuDieutri.TableName = "Phieudieutri";
                foreach (DataRow row in m_dtPhieuDieutri.Rows)
                {
                    row["chan_doan"] = chan_doan;
                }
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv" };
                Utility.AddColums2DataTable(ref m_dtPhieuDieutri, lstMoreColumns, typeof(string));
                Document doc;
                DataRow drData = m_dtPhieuDieutri.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                List<string> fieldNames = new List<string>();

                string PathDoc = string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, "PHIEUDIEUTRI.doc");
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(m_dtPhieuDieutri);

                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                    return;
                }


                List<string> lstWordFiles = new List<string>();
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                List<string> lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                if ((drData != null) && File.Exists(PathDoc))
                {
                   
                    List<string> lstDays = m_dtPhieuDieutri.AsEnumerable().Select(c => Utility.sDbnull(c["ngay_dieutri"])).Distinct().ToList<string>();
                    foreach (string ngay in lstDays)
                    {
                        string File2View = string.Format(@"{0}\{1}_{2}_{3}.Doc", Application.StartupPath, objLuotkham.MaLuotkham, "PHIEUDIEUTRI", Guid.NewGuid().ToString());
                        DataTable dtPhieuDieuTri = m_dtPhieuDieutri.Select("ngay_dieutri='"+ ngay + "'").CopyToDataTable();
                        doc = new Document(PathDoc);
                        DocumentBuilder builder = new DocumentBuilder(doc);
                        if (doc == null)
                        {
                            Utility.ShowMsg("Không nạp được file word.", "Thông báo");
                        }
                        Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                        //Tạo thông tin y lệnh trong tờ điều trị
                        foreach (DataRow row in dtPhieuDieuTri.Rows)
                        {
                            nguoi_tao = Utility.sDbnull(row["nguoi_tao"]);
                            var YLENH = new StringBuilder("");

                            //Tạo thông tin thuốc. 
                            List<DataRow> query = (dsPrint.Tables[1].AsEnumerable().Where(
                               chidinh => Utility.Int32Dbnull(chidinh["id_phieudieutri"]) == Utility.Int32Dbnull(row["id_phieudieutri"])
                                          &&
                                          Utility.Int32Dbnull(chidinh["id_loaithanhtoan"]) ==
                                          Utility.Int32Dbnull(KieuLoaiThanhToan.Thuoc))).ToList();
                            if (query.Any())
                            {
                                foreach (DataRow dr in query)
                                {
                                    YLENH.Append("<p>");
                                    YLENH.Append(string.Format("<b>{0} ( {1} )</b>", Utility.sDbnull(dr["TEN"]), Utility.sDbnull(dr["ten_hoatchat"])));
                                    YLENH.Append("<span > x </span> <b>");
                                    YLENH.Append(Utility.sDbnull(dr["SOLUONG"]));
                                    YLENH.Append(" ");
                                    YLENH.Append(Utility.sDbnull(dr["DONVI"]));
                                    YLENH.Append("</b>");
                                    if (Utility.sDbnull(dr["sDesc"]).Length > 0)
                                        YLENH.Append(string.Format("</br><i>{0}</i>", Utility.sDbnull(dr["sDesc"])));
                                    YLENH.Append("</p>");
                                }
                            }
                            //Tạo thông tin chỉ định
                            query = (from chidinh in dsPrint.Tables[1].AsEnumerable()
                                     where
                                         Utility.Int32Dbnull(chidinh["id_phieudieutri"]) ==
                                         Utility.Int32Dbnull(row["id_phieudieutri"])
                                         &&
                                         Utility.Int32Dbnull(chidinh["id_loaithanhtoan"]) ==
                                         Utility.Int32Dbnull(KieuLoaiThanhToan.CLS)
                                     select chidinh).ToList();
                            if (query.Any())
                            {
                                var q = (from p in query
                                         select Utility.sDbnull(p["TEN"]));
                                string dichvu = string.Join(",", q.ToArray<string>());
                                //foreach (DataRow dr in query)
                                //{
                                YLENH.Append("<p>");
                                YLENH.Append(string.Format("{0}", dichvu));
                                YLENH.Append("</p>");
                                //}
                            }
                            row["YLENH"] = YLENH.ToString();
                            //Đã tạo xong y lệnh-->Ghi luôn vào các rows
                            Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[1];

                            tab.LeftPadding = 5;
                            tab.RightPadding = 5;
                            tab.TopPadding = 3;
                            tab.BottomPadding = 3;
                            int idx = 1;
                            Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);
                            //newRow.RowFormat.Borders.Shadow = false;
                            //newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                            //newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                            //newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;


                            newRow.Cells[0].RemoveAllChildren();

                            newRow.Cells[1].RemoveAllChildren();

                            newRow.Cells[2].RemoveAllChildren();
                            newRow.Cells[0].EnsureMinimum();
                            newRow.Cells[1].EnsureMinimum();
                            newRow.Cells[2].EnsureMinimum();

                            Run r = new Run(doc);
                            r.Font.Name = "Times New Roman";
                            r.Font.Size = 12;
                            r.Font.Bold = false;
                            //r.Font.Color = Color.FromArgb(102, 0, 102);
                            r.Text = Utility.sDbnull(row["NGAY_LAPPHIEU"], "");
                            newRow.Cells[0].FirstParagraph.AppendChild(r);
                            newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Center;
                            newRow.Cells[0].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                            int i = 0;
                            while (i < newRow.Cells[0].Paragraphs.Count)
                            {
                                var para = newRow.Cells[0].Paragraphs[i];
                                if (string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                    para.Remove();
                                else
                                    i++;
                            }
                            i = 0;
                            r = new Run(doc);
                            r.Font.Name = "Times New Roman";
                            r.Font.Bold = false;
                            r.Font.Size = 12;
                            //r.Font.Color = Color.FromArgb(102, 0, 102);
                            string dienBien = Utility.sDbnull(row["DIENBIEN"], "");
                            dienBien = dienBien.Replace("\r\n", ControlChar.LineBreak).Replace("\n", ControlChar.LineBreak);
                            r.Text = Utility.sDbnull(dienBien, "");
                            newRow.Cells[1].FirstParagraph.AppendChild(r);
                            newRow.Cells[1].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                            newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                            while (i < newRow.Cells[1].Paragraphs.Count)
                            {
                                var para = newRow.Cells[1].Paragraphs[i];
                                if (string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                    para.Remove();
                                else
                                    i++;
                            }
                            i = 0;
                            r = new Run(doc);
                            r.Font.Name = "Times New Roman";
                            r.Font.Bold = false;
                            r.Font.Size = 12;
                            //r.Font.Color = Color.FromArgb(102, 0, 102);
                            //r.Text = Utility.sDbnull(row["YLENH"], "");
                            newRow.Cells[2].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                            newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                            builder.MoveTo(newRow.Cells[2].FirstParagraph);  // Di chuyển con trỏ vào đoạn đầu của cell

                            builder.InsertHtml(Utility.sDbnull(row["YLENH"], ""));
                            builder.Writeln();  // hoặc dùng builder.InsertParagraph();

                            //#region Old // Bước 3: Chèn merge field cho chữ ký bác sĩ
                            string nguoi_ky = LayThongTinNguoiKyToDieuTri(Utility.Int64Dbnull(row["id_phieudieutri"]));
                            if (nguoi_ky != "")
                            {
                                //Aspose.Words.Paragraph currentPara = builder.CurrentParagraph;
                                //builder.MoveTo(currentPara);
                                //builder.Writeln("Chữ ký:");
                                //builder.Writeln(); // xuống dòng
                                builder.InsertField(string.Format("MERGEFIELD {0} \\* MERGEFORMAT", string.Format("{0}_{1}", Utility.sDbnull(row["id_phieudieutri"]), nguoi_ky)), "");
                            }


                            while (i < newRow.Cells[2].Paragraphs.Count)
                            {
                                var para = newRow.Cells[2].Paragraphs[i];
                                if (string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                    para.Remove();
                                i++;
                            }

                            tab.AppendChild(newRow);
                            idx += 1;
                        }
                        //doc.UpdateFields();
                        doc.MailMerge.PreserveUnusedTags = true;
                        //Merge các field thông tin chung của người bệnh
                        doc.MailMerge.Execute(drData);
                        SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                        Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "", true);


                        if (File.Exists(File2View))
                        {
                            File.Delete(File2View);
                        }
                        doc.Save(File2View, SaveFormat.Doc);
                        lstWordFiles.Add(File2View);
                        //if (File.Exists(File2View))
                        //{
                        //    Process process = new Process();
                        //    try
                        //    {
                        //        process.StartInfo.FileName = File2View;
                        //        process.Start();
                        //        process.WaitForInputIdle();
                        //    }
                        //    catch
                        //    {
                        //    }
                        //}
                    }//end for days
                    //Nối các file word thành 1 file và mở ra cho người dùng xem
                    string output = string.Format(@"{0}\{1}_{2}_{3}.Doc", Application.StartupPath, objLuotkham.MaLuotkham, "PHIEUDIEUTRI", Guid.NewGuid().ToString());

                    VMS.HIS.Bus.Classess.WordMerger.MergeWordFiles(lstWordFiles, output);
                    Utility.OpenFile(output);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        string LayThongTinNguoiKyToDieuTri(long id_phieu)
        {
            var q = globalVariables.dtSignInfor.AsEnumerable().Where(c => Utility.Int64Dbnull(c["id_phieu"]) == id_phieu).FirstOrDefault();
            if (q != null)
                return Utility.sDbnull(q["nguoi_ky"]);
            return "";
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu điều trị cần thiết
        /// </summary>
        private void INPHIEU_DIEUTRI()
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một phiếu điều trị để in ", "Thông báo", MessageBoxIcon.Information);
                    return;
                }

                if (grdList.GetCheckedRows().Length <= 0)
                {
                    grdList.CurrentRow.IsChecked = true;
                }
                var TreatmentId = new StringBuilder("-1");
                foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    TreatmentId.Append(",");
                    TreatmentId.Append(gridExRow.Cells[NoitruPhieudieutri.Columns.IdPhieudieutri].Value.ToString());
                    gridExRow.BeginEdit();
                    gridExRow.Cells[NoitruPhieudieutri.Columns.TthaiIn].Value = 1;
                    gridExRow.EndEdit();
                }
                grdList.UpdateData();
                m_dtPhieudieutri.AcceptChanges();
                DataSet dsPrint;
                dsPrint = new noitru_phieudieutri().NoitruLaythongtinphieudieutriIn(objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham, TreatmentId.ToString());
                DataTable mdtDataPhieuDieuTri;
                mdtDataPhieuDieuTri = dsPrint.Tables[0];
                THU_VIEN_CHUNG.CreateXML(mdtDataPhieuDieuTri, "noitru_phieudieutri");
                foreach (DataRow row in mdtDataPhieuDieuTri.Rows)
                {
                    var YLENH = new StringBuilder("");
                    if (chkInYLenhThuocCLS.Checked)
                    {
                        //Tạo thông tin thuốc. 
                        List<DataRow> query = (dsPrint.Tables[1].AsEnumerable().Where(
                           chidinh => Utility.Int32Dbnull(chidinh["id_phieudieutri"]) == Utility.Int32Dbnull(row["id_phieudieutri"])
                                      &&
                                      Utility.Int32Dbnull(chidinh["id_loaithanhtoan"]) ==
                                      Utility.Int32Dbnull(KieuLoaiThanhToan.Thuoc))).ToList();
                        if (query.Any())
                        {
                            YLENH.Append("</br>");
                            foreach (DataRow dr in query)
                            {
                                YLENH.Append("<p>");
                                YLENH.Append(string.Format("<b>{0} ( {1} )</b>",  Utility.sDbnull(dr["TEN"]), Utility.sDbnull(dr["ten_hoatchat"])));
                                YLENH.Append("<span > x </span> <b>");
                                YLENH.Append(Utility.sDbnull(dr["SOLUONG"]));
                                YLENH.Append(" ");
                                YLENH.Append(Utility.sDbnull(dr["DONVI"]));
                                YLENH.Append("</b></br>");
                                YLENH.Append(string.Format("<i>{0}</i>", dr["sDesc"]));
                                YLENH.Append("</p><br>");
                            }
                        }
                        //Tạo thông tin chỉ định
                       query = (from chidinh in dsPrint.Tables[1].AsEnumerable()
                                               where
                                                   Utility.Int32Dbnull(chidinh["id_phieudieutri"]) ==
                                                   Utility.Int32Dbnull(row["id_phieudieutri"])
                                                   &&
                                                   Utility.Int32Dbnull(chidinh["id_loaithanhtoan"]) ==
                                                   Utility.Int32Dbnull(KieuLoaiThanhToan.CLS)
                                               select chidinh).ToList();
                        if (query.Any())
                        {
                            var q = (from p in query
                                     select Utility.sDbnull(p["TEN"]));
                            string dichvu = string.Join(",", q.ToArray<string>());
                            //foreach (DataRow dr in query)
                            //{
                                YLENH.Append("<p>");
                                YLENH.Append(string.Format("{0}", dichvu));
                                YLENH.Append("</p>");
                            //}
                        }

                        YLENH.Append("</br> ");
                        YLENH.Append("</br> ");
                        row["YLENH"] = YLENH.ToString();
                    }
                    else
                    {
                        //var YLENH = new StringBuilder("");
                        YLENH.Append("</br> ");
                        YLENH.Append("</br> ");
                        //YLENH.Append(string.Format("Người lập y lệnh {0}", Utility.sDbnull(row["ten_bacsidieutri"])));
                        //YLENH.Append("</br> ");
                        row["YLENH"] = YLENH.ToString();
                    }
                }


                Utility.UpdateLogotoDatatable(ref mdtDataPhieuDieuTri);
                mdtDataPhieuDieuTri.AcceptChanges();

                InphieuDieuTri(mdtDataPhieuDieuTri, dtNgayInPhieu.Value);

                foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells[NoitruPhieudieutri.Columns.TthaiIn].Value = 1;
                    gridExRow.EndEdit();
                }
                grdList.UpdateData();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
        public static void InphieuDieuTri(DataTable dtPrint, DateTime ngayin)
        {

            string tieude = "", reportname = "";
            var crpt = Utility.GetReport("noitru_phieudieutri", ref tieude, ref reportname);
            if (crpt == null) return;
            //var crpt = new crpt_PhieuDieuTri();
            var objForm = new frmPrintPreview("IN PHIẾU ĐIỀU TRỊ", crpt, true, true);
            crpt.SetDataSource(dtPrint);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = "noitru_phieudieutri";
            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
            Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(ngayin));
            Utility.SetParameterValue(crpt,"sTitleReport", tieude);
            Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());

            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            objForm.Dispose();

        }
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._NoitruProperties);
            frm.ShowDialog();
            cauhinh();
        }

        /// <summaắtry>
        /// hàm thực hiện việc phím t
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_InPhieudieutri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 || (e.Control && e.KeyCode==Keys.P) || e.KeyCode==Keys.P) cmdInDieuTri.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        }
    }
}