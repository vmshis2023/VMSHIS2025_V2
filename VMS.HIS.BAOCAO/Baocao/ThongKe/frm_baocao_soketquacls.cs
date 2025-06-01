using System;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Baocao.ThongKe
{
    public partial class frm_baocao_soketquacls : Form
    {
        private DataTable dtResult;

        public frm_baocao_soketquacls()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }

        private void frm_baocao_soketquacls_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;
            cboLoaiSo.SelectedValue = -1;
            DataBinding.BindDataCombobox(cboDoiTuong, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.IdDoituongKcb,
                                           DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng", true);
        }

        private void SetPropertiesCol(string colName, string colCaption)
        {
            if (!dtResult.Columns.Contains(colName))
                return;
            grdResult.RootTable.Columns[colName].Caption = colCaption;
            grdResult.RootTable.Columns[colName].AutoSizeMode = ColumnAutoSizeMode.AllCellsAndHeader;
            grdResult.RootTable.Columns[colName].FilterRowComparison = ConditionOperator.Contains;
            grdResult.RootTable.Columns[colName].AggregateFunction = AggregateFunction.None;
        }

        private void grdResult_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.TotalRow)
            {
                e.Row.Cells["dia_chi"].Value = "Tổng Số Xét Nghiệm:";
                e.Row.Cells["ten_benhnhan"].Value = "Tổng Sô BN:  " + dtResult.Rows.Count.ToString();
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                grdResult.RootTable.Columns.Clear();
                dtResult =
                    SPs.BaocaoThongkeSoketqua(dtpFromDate.Value.Date, dtpToDate.Value,
                                              Utility.sDbnull(cboLoaiSo.SelectedValue, -1),Utility.Int16Dbnull(cboDoiTuong.SelectedValue),
                                              Utility.Int16Dbnull(chkTinhTrang.Checked)).
                        GetDataSet().Tables[0];
                if (dtResult.Rows.Count <= 0 | dtResult.Columns.Count <= 1)
                {
                    Utility.ShowMsg("Không tìm thấy kết quả !");
                    cmdExportToExcel.Enabled = false;
                    return;
                }
                cmdExportToExcel.Enabled = true;
                for (int i = 0; i < dtResult.Columns.Count; i++)
                {
                    GridEXColumn grdCol = new GridEXColumn(dtResult.Columns[i].ColumnName);
                    grdResult.RootTable.Columns.Add(grdCol);
                    grdResult.RootTable.Columns[grdCol.Key].AggregateFunction = AggregateFunction.ValueCount;
                }
                grdResult.RootTable.Columns["id_benhnhan"].Visible = false;
                grdResult.RootTable.Columns["ma_luotkham"].Visible = false;
                grdResult.RootTable.Columns["ten_doituong_kcb"].Visible = false;
                grdResult.RootTable.Columns["ngay_ketqua"].Visible = false;
                SetPropertiesCol("ten_benhnhan", "Tên BN");
                SetPropertiesCol("nam_sinh", "Năm sinh");
                SetPropertiesCol("gioi_tinh", "Giới tính");
                SetPropertiesCol("dia_chi", "Địa chỉ");
                SetPropertiesCol("mathe_bhyt", "Mã BHYT");
                SetPropertiesCol("ma_kcbbd", "Nơi ĐKKCB");
                SetPropertiesCol("so_benh_an", "Số bệnh án");
                SetPropertiesCol("ten_doituong_kcb", "Đối tượng");
                SetPropertiesCol("chan_doan", "Chẩn đoán");
                SetPropertiesCol("ngay_ketqua", "Ngày kết quả");
                //SetPropertiesCol("ten_doituong_kcb", "Đối tượng");
                SetPropertiesCol("ten_khoaphong", "Phòng chỉ định");
                SetPropertiesCol("ten_nhanvien", "Bác sỹ chỉ định");
                grdResult.DataSource = dtResult;
                grdResult.RootTable.Groups.Add("ngay_ketqua");
                grdResult.RootTable.Groups.Add("ten_doituong_kcb");
                grdResult.AutoSizeColumns();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            ExcelUtlity.ExportGridEx(grdResult);
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if(dtpFromDate.Value>dtpToDate.Value)
            {
                dtpToDate.Value = dtpFromDate.Value;
            }
        }

        private void frm_baocao_soketquacls_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if(e.KeyCode==Keys.F4 && cmdExportToExcel.Enabled) cmdExportToExcel.PerformClick();
            if(e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        }

        private void cmdNhapketqua_Click(object sender, EventArgs e)
        {
            //VNS.HIS.UI.Forms.NGOAITRU.frm_NhapKetQua frm = new frm_NhapKetQua();
            //frm.ShowDialog();
        }
    }
}