using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.Libs;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
//using reports.Baocao;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaochidinhCLS : Form
    {
        private readonly string args = "";
        private MoneyByLetter _moneyByLetter = new MoneyByLetter();
        public DataTable _reportTable = new DataTable();
        private bool m_blnhasLoaded;
        private DataTable m_dtKhoathucHien = new DataTable();
        private string reportname = "";
        private string tieude = "";
        private int _iddichvuchitiet = new int();
        private string _mabschidinh = "-1";
        private decimal tong_tien;

        public frm_baocaochidinhCLS(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.args = args;
            Initevents();
            cmdExit.Click += cmdExit_Click;
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }

        private void Initevents()
        {
            cmdExportToExcel.Click += cmdExportToExcel_Click;
            cmdPrint.Click += cmdPrint_Click;
            cmdExit.Click += cmdExit_Click;
            Load += frm_baocaochidinhCLS_Load;
            KeyDown += frm_baocaochidinhCLS_KeyDown;
            chkChitiet.CheckedChanged += chkChitiet_CheckedChanged;
            cbokhoa.SelectedIndexChanged += cbokhoa_SelectedIndexChanged;
            ShowGrid();
        }

        private void cbokhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // DataBinding.BindDataCombobox(cboPhong, THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.sDbnull( cbokhoa.SelectedValue,"-1"),-1), DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "----Chọn phòng thực hiện ----",true);
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
                baocaO_TIEUDE1.Init("baocao_chidinhcls_chitiet");
            }
            else
            {
                grdList.BringToFront();
                baocaO_TIEUDE1.Init("baocao_chidinhcls_tonghop");
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_baocaochidinhCLS_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNhomdichvu = new Select().From(VDmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                cboNhomdichvuCLS.DropDownDataSource = dtNhomdichvu;
                cboNhomdichvuCLS.DropDownDataMember = VDmucDichvucl.Columns.IdDichvu;
                cboNhomdichvuCLS.DropDownDisplayMember = VDmucDichvucl.Columns.TenDichvu;
                cboNhomdichvuCLS.DropDownValueMember = VDmucDichvucl.Columns.IdDichvu;

                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                             DmucDoituongkcb.Columns.MaDoituongKcb,
                                             DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);

                txtNhanvien.Init(CommonLoadDuoc.LAYTHONGTIN_NHANVIEN());
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
                DataTable dtdmuc = new Select().From(DmucDichvuclsChitiet.Schema).Where(DmucDichvuclsChitiet.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
                txtchitietdichvu.Init(dtdmuc
              ,
              new List<string>
                        {
                            DmucDichvuclsChitiet.Columns.IdChitietdichvu,
                            DmucDichvuclsChitiet.Columns.MaChitietdichvu,
                            DmucDichvuclsChitiet.Columns.TenChitietdichvu
                        });
                DataTable dtBacsi = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, -1);
                cboBacSyChiDinh.DropDownDataSource = dtBacsi;
                cboBacSyChiDinh.DropDownDataMember = DmucNhanvien.Columns.IdNhanvien;
                cboBacSyChiDinh.DropDownDisplayMember = DmucNhanvien.Columns.TenNhanvien;
                cboBacSyChiDinh.DropDownValueMember = DmucNhanvien.Columns.IdNhanvien;
                m_blnhasLoaded = true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!", ex);
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
            try
            {
                _mabschidinh = "-1";
                _iddichvuchitiet = -1;
                // Lấy Id Bác sỹ
                if (!string.IsNullOrEmpty(cboBacSyChiDinh.Text))
                {
                    var query = (from chk in cboBacSyChiDinh.CheckedValues.AsEnumerable()
                                 let x = Utility.sDbnull(chk)
                                 select x).ToArray();
                    if (query.Count() > 0)
                    {
                        _mabschidinh = string.Join(",", query);
                    }
                }
                string nhomdichvu = "-1";
                if (!string.IsNullOrEmpty(cboNhomdichvuCLS.Text) && cboNhomdichvuCLS.CheckedValues != null)
                {
                    string[] query = (from chk in cboNhomdichvuCLS.CheckedValues.AsEnumerable()
                                      let x = Utility.sDbnull(chk)
                                      select x).ToArray();
                    if (query.Count() > 0)
                    {
                        nhomdichvu = string.Join(",", query);
                    }
                }
                if (chkChitiet.Checked)
                {
                    _reportTable =
                        BAOCAO_NGOAITRU.BaocaoChidinhclsChitiet(
                            chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                            chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                            Utility.sDbnull(cboDoituongKCB.SelectedValue, -1), nhomdichvu,
                            txtNhanvien.myCode, Utility.sDbnull(cbokhoa.SelectedValue, -1),
                            Utility.Int32Dbnull(chkKieuchidinh.SelectedValue, -1), args,_mabschidinh,Utility.Int32Dbnull(txtchitietdichvu.MyID,-1));
                    THU_VIEN_CHUNG.CreateXML(_reportTable, "baocao_chidinhcls_chitiet");
                    Utility.SetDataSourceForDataGridEx(grdChitiet, _reportTable, false, true, "1=1", "");
                    GridEXColumn gridExColumnTientong = grdChitiet.RootTable.Columns["Thanh_Tien"];
                    tong_tien = Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumnTientong, AggregateFunction.Sum));
                }
                else
                {
                    _reportTable =
                        BAOCAO_NGOAITRU.BaocaoChidinhclsTonghop(
                            chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                            chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                            Utility.sDbnull(cboDoituongKCB.SelectedValue, -1), nhomdichvu,
                            txtNhanvien.myCode, Utility.sDbnull(cbokhoa.SelectedValue, -1),
                            Utility.Int32Dbnull(chkKieuchidinh.SelectedValue, -1), args);
                    THU_VIEN_CHUNG.CreateXML(_reportTable, "baocao_chidinhcls_tonghop");
                    Utility.SetDataSourceForDataGridEx(grdList, _reportTable, false, true, "1=1", "");
                    GridEXColumn gridExColumnTientong = grdList.RootTable.Columns["Thanh_Tien"];
                    tong_tien = Utility.DecimaltoDbnull(grdList.GetTotal(gridExColumnTientong, AggregateFunction.Sum));
                }

                if (_reportTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
                string Condition =
                    string.Format(
                        "Từ ngày {0} đến {1} - Đối tượng : {2} - Nhân viên :{3} - Khoa chỉ định :{4}  - Loại dịch vụ: {5}",
                        dtFromDate.Text, dtToDate.Text,
                        cboDoituongKCB.SelectedIndex > 0
                            ? Utility.sDbnull(cboDoituongKCB.Text)
                            : "Tất cả",
                        txtNhanvien.myCode != "-1"
                            ? txtNhanvien.Text
                            : "Tất cả", cbokhoa.SelectedIndex > 0 ? Utility.sDbnull(cbokhoa.Text) : "Tất cả",
                        cboNhomdichvuCLS.CheckedValues != null ? Utility.sDbnull(cboNhomdichvuCLS.Text) : "Tất cả");
                Utility.UpdateLogotoDatatable(ref _reportTable);
                string reportCode = chkChitiet.Checked ? "baocao_chidinhcls_chitiet" : "baocao_chidinhcls_tonghop";
                ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                if (crpt == null) return;


                var objForm = new frmPrintPreview(tieude, crpt, true, _reportTable.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(_reportTable);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", Condition);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
              
                Utility.SetParameterValue(crpt, "Tongtien_chu", new MoneyByLetter().sMoneyToLetter(tong_tien.ToString()));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                            Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                               globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi in báo cáo", ex);
            }
        }


        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                if (grdChitiet.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdChitiet.Focus();
                    return;
                }
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
                Utility.ShowMsg("Lỗi:" +exception.Message);
            }
        }


        private void frm_baocaochidinhCLS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

        private void cmdPrint_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdExportToExcel_Click_1(object sender, EventArgs e)
        {

        }
    }
}