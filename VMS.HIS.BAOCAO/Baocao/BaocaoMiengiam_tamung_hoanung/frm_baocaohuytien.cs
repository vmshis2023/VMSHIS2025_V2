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
using Microsoft.VisualBasic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaohuytien : Form
    {
        public DataTable _dtData = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        string args = "ALL";
        public frm_baocaohuytien(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.args = args;
            Initevents();
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }
        void Initevents()
        {
            this.KeyDown += new KeyEventHandler(frm_baocaohuytien_KeyDown);
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            chkByDate.CheckedChanged += new EventHandler(chkByDate_CheckedChanged);
            this.cmdExportToExcel.Click += new System.EventHandler(this.cmdExportToExcel_Click);
            this.cmdInPhieuXN.Click += new System.EventHandler(this.cmdInPhieuXN_Click);
            this.Load += new System.EventHandler(this.frm_baocaohuytien_Load);
            optAll.CheckedChanged += _CheckedChanged;
            optHuythanhtoan.CheckedChanged += _CheckedChanged;
            optHuyphieuchi.CheckedChanged += _CheckedChanged;
            optHuytamung.CheckedChanged += _CheckedChanged;
        }

        void _CheckedChanged(object sender, EventArgs e)
        {
            //if (optAll.Checked)
            //    baocaO_TIEUDE1.Init("baocao_tamung");
            //else
            //    baocaO_TIEUDE1.Init("baocao_hoanung");
        }
       
        private void frm_baocaohuytien_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                txtNhanvien.Init(this.args);
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!", ex);
            }

        }
        
        /// <summary>
        /// trạng thái của tìm kiếm từ ngày tới ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin của form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocaohuytien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
            //  if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
        /// <summary>
        /// hàm thực hiên việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        MoneyByLetter _moneyByLetter = new MoneyByLetter();
        /// <summary>
        /// hàm thực hiện việc export excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
               
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls",tieude);
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    FileStream fs = new FileStream(sPath, FileMode.Create);
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
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            byte loaiphieuhuy=3;
            if(optHuythanhtoan.Checked) loaiphieuhuy=0;
            if(optHuyphieuchi.Checked) loaiphieuhuy=1;
            if(optHuytamung.Checked) loaiphieuhuy=2;
                _dtData =BAOCAO_NGOAITRU.BaocaoHuytien(Utility.Int16Dbnull(txtNhanvien.MyID,-1),chkByDate.Checked?dtFromDate.Value.ToString("dd/MM/yyyy"):"01/01/1900",
                    chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900", loaiphieuhuy
                    );

                Utility.SetDataSourceForDataGridEx(grdList, _dtData, false, true, "1=1", "");

           
            Utility.UpdateLogotoDatatable(ref _dtData);
            THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_huytien.xml");
            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo", MessageBoxIcon.Information);
                return;
            }
            string Condition = string.Format("Từ ngày {0} đến {1} - Người hủy :{2} ", dtFromDate.Text, dtToDate.Text,txtNhanvien.Text);
            var crpt = Utility.GetReport( "baocao_huytien" , ref tieude, ref reportname);
            if (crpt == null) return;
            tieude = tieude + (loaiphieuhuy == 3 ? "" : (loaiphieuhuy == 0 ? "Thanh toán" : (loaiphieuhuy == 1 ? "Phiếu chi" : "Tạm ứng")));
            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count <= 0 ? false : true);
                //try
                //{
                crpt.SetDataSource(_dtData);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "baocao_huytien" ;
                Utility.SetParameterValue(crpt,"StaffName", StaffName);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", Condition);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "Tongtien_chu", new MoneyByLetter().sMoneyToLetter(_dtData.Compute("SUM(sotien_huy)", "1=1").ToString()));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
        

        private void cboKhoa_ThucHien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
