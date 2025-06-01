using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Baocao
{
    public partial class frm_tonghopkhamchuabenh : Form
    {
        public DataTable _reportTable = new DataTable();
        private DataTable m_dtKhoathucHien = new DataTable();
        private string reportname = "";
        private string tieude = "";
        private string sThamSo = "";
        public frm_tonghopkhamchuabenh(string Arg)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            sThamSo = Arg;
            cmdExit.Click += cmdExit_Click;
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }

        private void Initevents()
        {
            cmdExportToExcel.Click += cmdExportToExcel_Click;
            cmdPrint.Click += cmdPrint_Click;
            cmdExit.Click += cmdExit_Click;
            Load += frm_tonghopkhamchuabenh_Load;
            KeyDown += frm_tonghopkhamchuabenh_KeyDown;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Autocompletenhanvien()
        {
            DataTable _nhanvien = SPs.DmucLaydanhsachNhanvienTiepdon().GetDataSet().Tables[0];
            try
            {

                if (_nhanvien == null) return;
                if (!_nhanvien.Columns.Contains("ShortCut"))
                    _nhanvien.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in _nhanvien.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim());
                    shortcut = dr[DmucNhanvien.Columns.UserName].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in _nhanvien.AsEnumerable()
                            select p[DmucNhanvien.Columns.IdNhanvien].ToString() + "#" + Utility.sDbnull(p[DmucNhanvien.Columns.UserName], "") + "@" + p.Field<string>(DmucNhanvien.Columns.TenNhanvien).ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtNhanvien.AutoCompleteList = source;
                this.txtNhanvien.TextAlign = HorizontalAlignment.Center;
                this.txtNhanvien.CaseSensitive = false;
                this.txtNhanvien.MinTypedCharacters = 1;

            }
        }
        private void frm_tonghopkhamchuabenh_Load(object sender, EventArgs e)
        {
            try
            {
                Autocompletenhanvien();
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                             DmucDoituongkcb.Columns.IdDoituongKcb,
                                             DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);
                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cbokhoa, m_dtKhoathucHien,
                                             DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                             "Chọn khoa KCB", true);
                EnumerableRowCollection<DataRow> query = from khoa in m_dtKhoathucHien.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) ==
                                                             globalVariables.MA_KHOA_THIEN
                                                         select khoa;
                if (query.Count() > 0)
                {
                    cbokhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi load chức năng!"+ ex.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu xét nghiêm
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            
                _reportTable = BAOCAO_NGOAITRU.BaoCaoThongkeKhamChuaBenh(dtFromDate.Value, dtToDate.Value,
                                                                     Utility.Int32Dbnull(cboDoituongKCB.SelectedValue,-1),
                                                                     Utility.Int32Dbnull(chkLoaitimkiem.Checked,0),
                                                                     Utility.Int32Dbnull(cboGT.SelectedValue, -1),
                                                                    Utility.Int32Dbnull(txtNhanvien.MyID, -100), sThamSo);
                THU_VIEN_CHUNG.CreateXML(_reportTable,"baocao_thongke_thutienchitiet.XML");
                if (_reportTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    grdChitiet.DataSource = null;
                    return;
                }
                Utility.SetDataSourceForDataGridEx(grdChitiet,_reportTable,true,true,"","");
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
                //string Condition = "";
                string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} -Thu ngân viên :{3} - Giới tính :{4} ",
                                                 dtFromDate.Text, dtToDate.Text,
                                                 cboDoituongKCB.SelectedIndex > 0
                                                     ? Utility.sDbnull(cboDoituongKCB.Text)
                                                     : "Tất cả",
                                                 Utility.Int32Dbnull(txtNhanvien.MyID,-100) > 0
                                                     ? Utility.sDbnull(txtNhanvien.Text)
                                                     : "Tất cả",
                                                 cboGT.SelectedIndex > 0 ? Utility.sDbnull(cboGT.Text) : "Tất cả");
                Utility.UpdateLogotoDatatable(ref _reportTable);
                string reportCode = "baocao_thongke_thutienchitiet";
               
                ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                if (crpt == null) return;
                try
                {

                var objForm = new frmPrintPreview(tieude, crpt, true,
                                                  _reportTable.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(_reportTable);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", Condition);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
                {
                    Utility.FreeMemory(crpt);
                }
            
        }


        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                ExcelUtlity.ExportGridEx(grdChitiet);
                //saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                //saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
                ////saveFileDialog1.ShowDialog();
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    string sPath = saveFileDialog1.FileName;
                //    var fs = new FileStream(sPath, FileMode.Create);
                //    fs.CanWrite.CompareTo(true);
                //    fs.CanRead.CompareTo(true);
                //    gridEXExporter1.Export(fs);
                //    fs.Dispose();
                //}
                //saveFileDialog1.Dispose();
                //saveFileDialog1.Reset();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }


        private void frm_tonghopkhamchuabenh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

        private void grdChitiet_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

        private void cmdPrint_Click_1(object sender, EventArgs e)
        {

        }
    }
}