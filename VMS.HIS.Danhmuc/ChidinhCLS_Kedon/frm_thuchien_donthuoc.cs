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
            grdPresDetail.CellValueChanged += GrdPresDetail_CellValueChanged;
            grdPresDetail.KeyDown += GrdPresDetail_KeyDown;
            grdPresDetail.CellEdited += GrdPresDetail_CellEdited;
        }

        private void GrdPresDetail_CellEdited(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.EditType == EditType.Combo)
                grdPresDetail.UpdateData();
        }

        private void GrdPresDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
           
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
                object _value = grdPresDetail.GetValue(colName);
                int num = new Update(KcbDonthuocChitiet.Schema)
                    .Set(colName).EqualTo(_value)
                    .Where(KcbDonthuocChitiet.Columns.IdDonthuoc)
                    .IsEqualTo(Utility.Int64Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc)))
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
            DataTable v_dtData = SPs.DonthuocThuchiendonthuocLaythongtinIn(objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham, presID).GetDataSet().Tables[0];

            string chan_doan = Utility.NoitruLaythongtinchandoanTheoDonThuoc(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, presID, true);

            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "Donthuoc_Thuchiendonthuoc_laythongtin_in.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, objLuotkham.MaLuotkham, ref Barcode);

            foreach (DataRow dr in v_dtData.Rows)
            {
                dr["chan_doan"] = chan_doan;
            }
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            
                    var reportDocument = new ReportDocument();
                    string tieude = "", reportname = "", reportCode = "";
            reportCode = "Donthuoc_Inphieu_Thuchiendonthuoc";
                    reportDocument = Utility.GetReport(reportCode, "A4", ref tieude, ref reportname);
                    if (reportDocument == null) return;
                    if (Utility.DoTrim(forcedTitle).Length > 0)
                        tieude = forcedTitle;
                    Utility.WaitNow(this);
                    ReportDocument crpt = reportDocument;
                    frmPrintPreview objForm = new frmPrintPreview("In phiếu thực hiện đơn thuốc", crpt, true, true);
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
                        Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                        Utility.SetParameterValue(crpt, "ReportTitle", "ĐƠN THUỐC");
                        Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                        Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "ten_donvi_captren", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "ten_benhvien", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "dia_chi", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "dien_thoai", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "tieu_de", tieude);
                Utility.SetParameterValue(crpt, "ngay_in", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "dieu_kien", THU_VIEN_CHUNG.BottomCondition());
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
               
        }
        string m_strMaLuotkham = "";
      
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
