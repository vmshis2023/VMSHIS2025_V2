using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Baocao
{
    public partial class frm_thongketheoma_ICD10 : Form
    {
        public DataTable _dtData = new DataTable();
        private string _list;
        private MoneyByLetter _moneyByLetter = new MoneyByLetter();
        private DataTable m_dtKhoathucHien = new DataTable();
        private string reportname = "";
        private string tieude = "";

        public frm_thongketheoma_ICD10()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
            
        }

        private void Initevents()
        {
            cmdExit.Click += cmdExit_Click;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load;
            chkChitiet.CheckedChanged += chkChitiet_CheckedChanged;
            ShowGrid();
        }

        private void chkChitiet_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void ShowGrid()
        {
            if (chkChitiet.Checked)
            {
                grdChitiet.BringToFront();
                baocaO_TIEUDE1.Init("baocao_thongketheomabenh_icd10_chitiet");
            }
            else
            {
                grdList.BringToFront();
                baocaO_TIEUDE1.Init("baocao_thongketheomabenh_icd10_tonghop");
            }
        }

        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                AutocompleICD10();

                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                             DmucDoituongkcb.Columns.IdDoituongKcb,
                                             DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);

                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cboKhoa, m_dtKhoathucHien,
                                             DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                             "Chọn khoa KCB", true);
                EnumerableRowCollection<DataRow> query = from khoa in m_dtKhoathucHien.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) ==
                                                             globalVariables.MA_KHOA_THIEN
                                                         select khoa;
                if (query.Count() > 0)
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!", ex);
            }
        }

        private void AutocompleICD10()
        {
            DataTable _dtMabenhICD10 =
                new Select(DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh).From(
                    DmucBenh.Schema).ExecuteDataSet().Tables[0];

            txtMaBenhICD10.Init(_dtMabenhICD10,
                                new List<string>
                                    {DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh});
            try
            {
                if (_dtMabenhICD10 == null) return;
                if (!_dtMabenhICD10.Columns.Contains("ShortCut"))
                    _dtMabenhICD10.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                foreach (DataRow dr in _dtMabenhICD10.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucBenh.Columns.TenBenh].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucBenh.Columns.TenBenh].ToString().Trim());
                    shortcut = dr[DmucBenh.Columns.MaBenh].ToString().Trim();
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
            catch (Exception ex)
            {
                if(globalVariables.IsAdmin)
                Utility.ShowMsg("Lỗi:" +ex.Message);
            }
            finally
            {
                var source = new List<string>();
                EnumerableRowCollection<string> query = from p in _dtMabenhICD10.AsEnumerable()
                                                        select
                                                            p[DmucBenh.Columns.IdBenh] + "#" +
                                                            Utility.sDbnull(p[DmucBenh.Columns.MaBenh], "") + "@" +
                                                            p.Field<string>(DmucBenh.Columns.TenBenh) + "@" +
                                                            p.Field<string>("shortcut");
                source = query.ToList();
                txtMaBenhICD10.AutoCompleteList = source;
                txtMaBenhICD10.TextAlign = HorizontalAlignment.Center;
                txtMaBenhICD10.CaseSensitive = false;
                txtMaBenhICD10.MinTypedCharacters = 1;
            }
        }

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
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

        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            _dtData = new DataTable();
            if (chkChitiet.Checked)
            {
                _dtData =
                    BAOCAO_NGOAITRU.BaoCaoThongkeTheoMaBenhICD10ChiTiet(
                        chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                        chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                        Utility.sDbnull(cboDoituongKCB.SelectedValue, ""), Utility.sDbnull(cboKhoa.SelectedValue, ""),
                        Utility.sDbnull(txtListICD.Text, ""));
                THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_thongketheomabenh_icd10_chitiet.xml");
                Utility.SetDataSourceForDataGridEx(grdChitiet, _dtData, false, true, "1=1", "");
            }
            else
            {
                _dtData =
                    BAOCAO_NGOAITRU.BaoCaoThongkeTheoMaBenhICD10TongHop(
                        chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                        chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                        Utility.sDbnull(cboDoituongKCB.SelectedValue, ""), Utility.sDbnull(cboKhoa.SelectedValue, ""),
                        Utility.sDbnull(txtListICD.Text, ""));
                THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_thongketheomabenh_icd10_tonghop.xml");
                Utility.SetDataSourceForDataGridEx(grdList, _dtData, false, true, "1=1", "");
            }

            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo",
                                MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);


            string Condition =
                string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa KCB :{3} - Người tiếp đón: {4}",
                              dtFromDate.Text, dtToDate.Text,
                              cboDoituongKCB.SelectedIndex >= 0
                                  ? Utility.sDbnull(cboDoituongKCB.Text)
                                  : "Tất cả",
                              cboKhoa.SelectedIndex > 0
                                  ? Utility.sDbnull(cboKhoa.Text)
                                  : "Tất cả", txtMaBenhICD10.MyCode == "-1" ? "Tất cả" : txtMaBenhICD10.Text);
            ReportDocument crpt =
                Utility.GetReport(
                    chkChitiet.Checked
                        ? "baocao_thongketheomabenh_icd10_chitiet"
                        : "baocao_thongketheomabenh_icd10_tonghop", ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                var objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(_dtData);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = chkChitiet.Checked
                                             ? "baocao_thongketheomabenh_icd10_chitiet"
                                             : "baocao_thongketheomabenh_icd10_tonghop";
                crpt.SetParameterValue("StaffName", StaffName);
                crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                crpt.SetParameterValue("Address", globalVariables.Branch_Address);
                crpt.SetParameterValue("Phone", globalVariables.Branch_Phone);
                crpt.SetParameterValue("FromDateToDate", Condition);
                crpt.SetParameterValue("sTitleReport", tieude);
                crpt.SetParameterValue("sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        private void txtMaBenhICD10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdAdd.PerformClick();
            }
        }
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (_list == null)
            {
                _list = _list + txtMaBenhICD10.MyCode;
                txtListICD.Text = Utility.sDbnull(_list);
            }
            else
            {
                _list = _list + "," + txtMaBenhICD10.MyCode;
                txtListICD.Text = Utility.sDbnull(_list);
            }
            txtMaBenhICD10.Clear();
            txtMaBenhICD10.Focus();
        }

        private void frm_thongketheoma_ICD10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F4) cmdExportToExcel.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.A) cmdAdd.PerformClick();
        }

    }
}