using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.Libs;
using NLog;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_thuchien_donthuoc : Form
    {
        public KcbLuotkham objLuotkham;
        public frm_thuchien_donthuoc(long id_donthuoc)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.id_donthuoc = id_donthuoc;
         
            grdPresDetail.CellUpdated += GrdPresDetail_CellUpdated;
            grdPresDetail.KeyDown += GrdPresDetail_KeyDown;
        }
        /// <summary>
        /// ten_nguoithuchien_toi
        /// noidungthuchien_sang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdPresDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var grid = (Janus.Windows.GridEX.GridEX)sender;

                if (grid.CurrentColumn != null && grid.CurrentColumn.Key == "ten_nguoithuchien_toi")
                {
                    var currentRow = grid.CurrentRow;
                    if (currentRow != null && currentRow.RowType == Janus.Windows.GridEX.RowType.Record)
                    {
                        int currentRowIndex = grid.Row;
                        int nextRowIndex = currentRowIndex + 1;

                        if (nextRowIndex < grid.RowCount)
                        {
                            e.Handled = true;

                            // Di chuyển đến dòng tiếp theo
                            grid.MoveTo(nextRowIndex);

                            // Focus vào cột A
                            var cotA = grid.RootTable.Columns["noidungthuchien_sang"];
                            grid.Col = cotA.Position;

                            // Gửi phím F2 để vào chế độ edit ô (trick chuẩn GridEX)
                            SendKeys.Send("{F2}");
                        }
                    }
                }
            }
        }

        private void GrdPresDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                string colName = e.Column.Key;
               int num= new Update(KcbDonthuocChitiet.Schema)
                    .Set(colName).EqualTo(Utility.sDbnull(grdPresDetail.GetValue(colName)))
                    .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Utility.Int64Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc)))
                    .And(KcbDonthuocChitiet.Columns.IdThuoc).IsEqualTo(Utility.Int64Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdThuoc)))
                    .Execute();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public long id_donthuoc=-1;
        private void frm_thuchien_donthuoc_Load(object sender, EventArgs e)
        {
            DataTable dtNhanvien = new Select("*").From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.TrangThai).IsEqualTo(1)
               .OrderAsc(DmucChung.Columns.SttHthi)
               .ExecuteDataSet().Tables[0];
            if (grdPresDetail.DropDowns.Contains("cboNguoithuchien"))
            {
                grdPresDetail.DropDowns["cboNguoithuchien"].DataSource = dtNhanvien;
            }

            DataTable dtData = SPs.KcbThuchiendonthuocLaychitietthuocTheodon(id_donthuoc).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdPresDetail, dtData, false, true, "", "stt_in,ten_thuoc");
            var cotA = grdPresDetail.RootTable.Columns["noidungthuchien_sang"];
            grdPresDetail.Col = cotA.Position;

            // Gửi phím F2 để vào chế độ edit ô (trick chuẩn GridEX)
            SendKeys.Send("{F2}");
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSavePres_Click(object sender, EventArgs e)
        {
            int presId = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
            PrintPres(presId, "");
        }
        private void PrintPres(int presID, string forcedTitle)
        {
            DataTable v_dtDataOrg = new KCB_KEDONTHUOC().LaythongtinDonthuoc_In(presID);

            DataRow[] arrDR = v_dtDataOrg.Select("tuvan_them=0");
            if (arrDR.Length <= 0)
            {
                PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
                return;
            }
            DataTable v_dtData = arrDR.CopyToDataTable();


            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            bool chandoangiunguyen = THU_VIEN_CHUNG.Laygiatrithamsohethong("DONTHUOC_INCHANDOANTHEOBACSI_KE", "1", true) == "1";
            if (!chandoangiunguyen)
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                                Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                if (!chandoangiunguyen)
                    drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            List<string> lstmatinhchat = (from p in v_dtData.AsEnumerable()
                                          select Utility.sDbnull(p["ma_tinhchat"], "")).Distinct().ToList<string>();
            foreach (string ma_tinhchat in lstmatinhchat)
            {
                DataRow[] arrTemp = v_dtData.Select(string.Format("(ma_tinhchat='{0}' or ma_tinhchat is null) and printed=0", ma_tinhchat));
                DataTable v_PrintData = v_dtData.Clone();
                if (arrTemp.Length > 0) v_PrintData = arrTemp.CopyToDataTable();//Chắc chắn có dữ liệu nên hàm copy ko bị lỗi
                if (v_PrintData.Rows.Count <= 0) continue;
                //Lấy danh sách các reportcode của từng tính chất thuốc
                string report_code = Utility.sDbnull(v_PrintData.Rows[0]["report_code"], "DONTHUOC_THUONG");
                //Lấy lại dữ liệu của tất cả các thuốc có cùng report nhưng khác tính chất để in đảm bảo ko bị tách đơn
                v_PrintData = v_dtData.Select(string.Format("report_code='{0}' and printed=0", report_code)).CopyToDataTable();
                //Đánh dấu trạng thái đã in để tránh in lại ở vòng for tính chất
                (from p in v_dtData.AsEnumerable() where Utility.sDbnull(p["report_code"], "") == report_code select p).ToList().ForEach(x => x["printed"] = 1);

                List<string> lstReportCode = v_PrintData.Rows[0]["report_code"].ToString().Split('@')[0].Split(';').ToList<string>();
                if (lstReportCode.Count <= 0) lstReportCode.Add("thamkham_InDonthuocA4");
                foreach (string _rcode in lstReportCode)
                {
                    string KhoGiay = "A100";// "A5";//Truyền giá trị này để giữ nguyên report
                    if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
                    var reportDocument = new ReportDocument();
                    string tieude = "", reportname = "", reportCode = "";
                    reportCode = string.Format("{0}_quay", _rcode);
                    reportDocument = Utility.GetReport(reportCode, KhoGiay, ref tieude, ref reportname);
                    if (reportDocument == null)
                    {
                        //Lấy mặc định do chưa được khai báo trong danh mục tính chất thuốc
                        switch (KhoGiay)
                        {
                            case "A5":
                                reportCode = "thamkham_InDonthuocA5";
                                reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                                break;
                            case "A4":
                                reportCode = "thamkham_InDonthuocA4";
                                reportDocument = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref reportname);
                                break;
                            default:
                                reportCode = "thamkham_InDonthuocA5";
                                reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                                break;
                        }
                    }
                    if (reportDocument == null) return;
                    if (Utility.DoTrim(forcedTitle).Length > 0)
                        tieude = forcedTitle;
                    Utility.WaitNow(this);
                    ReportDocument crpt = reportDocument;
                    frmPrintPreview objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
                    objForm.nguoi_thuchien = Utility.sDbnull(v_dtData.Rows[0]["ten_bacsikedon"], "");
                    try
                    {
                        objForm.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm.mv_sReportCode = reportCode;
                        crpt.SetDataSource(v_PrintData);
                        Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                        Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                        Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                        Utility.SetParameterValue(crpt, "ReportTitle", "ĐƠN THUỐC");
                        Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                        Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                        objForm.crptViewer.ReportSource = crpt;
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                                   PropertyLib._MayInProperties.PreviewInDonthuoc))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                            objForm.ShowDialog();
                            //cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                        }
                        else
                        {
                            objForm.addTrinhKy_OnFormLoad();
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            crpt.PrintToPrinter(1, false, 0, 0);
                        }

                        Utility.DefaultNow(this);
                    }
                    catch (Exception ex)
                    {
                        Utility.DefaultNow(this);
                    }
                    finally
                    {

                    }
                }//Kết thúc vòng for qua các liên trong tính chất
            }//Kết thúc vòng for tính chất
            //In đơn tư vấn thêm(nếu có)
            PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
        }
        string m_strMaLuotkham = "";
        private void PrintTuvanthem(int presID, string forcedTitle, DataTable p_dtData)
        {

            DataRow[] arrDR = p_dtData.Select("tuvan_them=1");
            if (arrDR.Length <= 0) return;
            DataTable v_dtData = arrDR.CopyToDataTable();
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            if (v_dtData != null && v_dtData.Rows.Count > 0)
                GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                            Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "", reportCode = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            frmPrintPreview objForm = new frmPrintPreview("IN ĐƠN TƯ VẤN", crpt, true, true);
            objForm.nguoi_thuchien = Utility.sDbnull(v_dtData.Rows[0]["ten_bacsikedon"], "");
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                           PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                    // cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        private void GetChanDoan(string icdChinh, string idcPhu, ref string icdName, ref string icdCode)
        {
            try
            {
                List<string> lstIcd = icdChinh.Split(',').ToList();
                DmucBenhCollection list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstIcd));
                foreach (DmucBenh item in list)
                {
                    icdName += item.TenBenh + "; ";
                    icdCode += item.MaBenh + "; ";
                }
                lstIcd = idcPhu.Split(',').ToList();
                list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstIcd));
                foreach (DmucBenh _item in list)
                {
                    icdName += _item.TenBenh + "; ";
                    icdCode += _item.MaBenh + "; ";
                }
                if (icdName.Trim() != "") icdName = icdName.Substring(0, icdName.Length - 1);
                if (icdCode.Trim() != "") icdCode = icdCode.Substring(0, icdCode.Length - 1);
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin) Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

    }
}
