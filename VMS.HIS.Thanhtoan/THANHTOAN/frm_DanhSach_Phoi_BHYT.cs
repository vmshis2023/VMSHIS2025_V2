using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
namespace  VNS.HIS.UI.THANHTOAN
{
    public partial class frm_DanhSach_Phoi_BHYT : Form
    {
        private DataTable m_dtTimKiem=new DataTable();
        private bool b_Hasloaded = false;
        public frm_DanhSach_Phoi_BHYT()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value = globalVariables.SysDate;
            txtMaLanKham.LostFocus+=new EventHandler(txtMaLanKham_LostFocus);
            txtMaLanKham.KeyDown+=new KeyEventHandler(txtMaLanKham_KeyDown);
            Utility.VisiableGridEx(grdList,KcbPhieuDct.Columns.IdPhieuDct,globalVariables.IsAdmin);

        }
        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MalanKam = Utility.sDbnull(txtMaLanKham.Text.Trim());
                if (!string.IsNullOrEmpty(MalanKam) && txtMaLanKham.Text.Length < 8)
                {
                    MalanKam = Utility.GetYY(globalVariables.SysDate) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLanKham.Text, 0), "000000");
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

        private void frm_DanhSach_Phoi_BHYT_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            txtMaLanKham.Focus();
            txtMaLanKham.SelectAll();
        }
        private string MalanKam { get; set; }
        private void txtMaLanKham_LostFocus(object sender, EventArgs eventArgs)
        {
            try
            {
                MalanKam = Utility.sDbnull(txtMaLanKham.Text.Trim());
                if (!string.IsNullOrEmpty(MalanKam) && txtMaLanKham.Text.Length < 8)
                {
                    MalanKam = Utility.GetYY(globalVariables.SysDate) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLanKham.Text, 0), "000000");
                    txtMaLanKham.Text = MalanKam;
                }

            }
            catch (Exception)
            {

                // throw;
            }
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        /// <summary>
        /// Creates an object wrapper for the BHYT_DANHSACH_INPHOI_BAOHIEM Procedure
        /// </summary>
        public static StoredProcedure BhytDanhsachInphoiBaohiem(DateTime? FromDate, DateTime? ToDate, int? IsKetThuc, string MaLanKham, int? KieuThanhToan)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("BHYT_DANHSACH_INPHOI_BAOHIEM", DataService.GetInstance("ORM"), "dbo");

            sp.Command.AddParameter("@FromDate", FromDate, DbType.DateTime, null, null);

            sp.Command.AddParameter("@ToDate", ToDate, DbType.DateTime, null, null);

            sp.Command.AddParameter("@IsKetThuc", IsKetThuc, DbType.Int32, 0, 10);

            sp.Command.AddParameter("@MaLanKham", MaLanKham, DbType.String, null, null);

            sp.Command.AddParameter("@KieuThanhToan", KieuThanhToan, DbType.Int32, 0, 10);

            return sp;
        }
        
        private void TimKiemThongTin()
        {
            m_dtTimKiem =
               BhytDanhsachInphoiBaohiem(chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                                              chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                                              chkChuaKetThuc.Checked ? 0 : -1, txtMaLanKham.Text,
                                              radNgoaiTru.Checked ? 0 : 1).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList,m_dtTimKiem,true,true,"1=1","");
            Utility.SetGridEXSortKey(grdList,KcbPhieuDct.Columns.IdPhieuDct,Janus.Windows.GridEX.SortOrder.Ascending);
            b_Hasloaded = true;
            ModifyCommand();
        }

        private void ModifyCommand()
        {
            if(b_Hasloaded)
            {
                var query = from daketthuc in m_dtTimKiem.AsEnumerable()
                            where Utility.Int32Dbnull(daketthuc["Is_ketThuc"]) == 1
                            select daketthuc;
                var query1 = from chuaketthuc in m_dtTimKiem.AsEnumerable()
                             where Utility.Int32Dbnull(chuaketthuc["Is_ketThuc"]) == 0
                             select chuaketthuc;
                Utility.SetMsg(lblDaKetThuc, string.Format("Đã kết thúc: {0} ", query.Count()), true);
                Utility.SetMsg(lblChuaKetThuc, string.Format("Chưa kết thúc: {0}", query1.Count()), true);
            }
           
            cmdXuatExcel.Enabled = grdList.RowCount > 0;
        }

        private void cmdXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
                    return;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", "DanhSachInPhoiBHYT");
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
        /// <summary>
        /// hàm thực hiện việc chưa kết thúc thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkChuaKetThuc_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }

        private void frm_DanhSach_Phoi_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)this.Close();
            if(e.KeyCode==Keys.F2)
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
            }

        }
        /// <summary>
        /// hàm thực hiện việc thay đổi thông tin của phần mã lần khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaLanKham_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaLanKham.Text)) chkByDate.Checked = false;
        }
    }
}
