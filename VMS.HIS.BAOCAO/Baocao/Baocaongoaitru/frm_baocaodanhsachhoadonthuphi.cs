using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using Janus.Windows.GridEX;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaodanhsachhoadonthuphi : Form
    {
        public DataTable _reportTable = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        public frm_baocaodanhsachhoadonthuphi()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }
        void Initevents()
        {
            this.cmdExportToExcel.Click += new System.EventHandler(this.cmdExportToExcel_Click);
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.Load += new System.EventHandler(this.frm_baocaodanhsachhoadonthuphi_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_baocaodanhsachhoadonthuphi_KeyDown);
            cboHoadon.SelectedIndexChanged += new EventHandler(cboHoadon_SelectedIndexChanged);
            chkMaQuyen.CheckedChanged += new EventHandler(chkMaQuyen_CheckedChanged);
        }

        void chkMaQuyen_CheckedChanged(object sender, EventArgs e)
        {
            pnlQuyen.Enabled = chkMaQuyen.Checked;
            if (chkMaQuyen.Checked) cboQuyen.Focus();
        }

        void cboHoadon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(cboHoadon.SelectedValue, -1) == 0)
            {
                grdList.RootTable.Groups.Clear();
                chkMaQuyen.Enabled = false;
                pnlQuyen.Enabled = false;
            }
            else
            {
                chkMaQuyen.Enabled = true;
                chkMaQuyen_CheckedChanged(chkMaQuyen, e);
                grdList.RootTable.Groups.Clear();

                GridEXColumn gridExColumn = grdList.RootTable.Columns["mau_hoadon"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    gridExGroup.GroupPrefix = "Mẫu hóa đơn: ";
                    grdList.RootTable.Groups.Add(gridExGroup);

                     gridExColumn = grdList.RootTable.Columns["ma_quyen"];
                     gridExGroup = new GridEXGroup(gridExColumn);
                    gridExGroup.GroupPrefix = "Mã quyển: ";
                    grdList.RootTable.Groups.Add(gridExGroup);
                
            }
        }

        
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataTable m_dtKhoathucHien = new DataTable();

        private void frm_baocaodanhsachhoadonthuphi_Load(object sender, EventArgs e)
        {
            try
            {
                DataBinding.BindDataCombobox(cboQuyen, SPs.HoadonLaydanhsachquyenhoadon().GetDataSet().Tables[0],
                                          HoadonMau.Columns.MaQuyen, "ten_quyen", "Tất cả", true);

                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "Tất cả", true);
                DataBinding.BindDataCombobox(cbonhanvien, THU_VIEN_CHUNG.LaydanhsachThunganvien(),
                                      DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "Tất cả", true);
                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cbokhoa, m_dtKhoathucHien,
                                     DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Tất cả", true);
                var query = from khoa in m_dtKhoathucHien.AsEnumerable()
                            where Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) == globalVariables.MA_KHOA_THIEN
                            select khoa;
                if (query.Count() > 0)
                {
                    cbokhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
                m_blnhasLoaded = true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!",ex);
            }

        }
        bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (chkByDate.Checked &&  !Utility.isValidDate(dtFromDate.Value, dtToDate.Value))
            {
                Utility.SetMsg(lblMsg, "Từ ngày phải <= đến ngày", true);
                dtToDate.Focus();
                return false;
            }
            if (chkMaQuyen.Checked)
            {
                if (cboQuyen.SelectedValue.ToString() == "-1")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập quyển hóa đơn", true);
                    cboQuyen.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtfromSerie.Text,-1))<0)
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập số serie từ", true);
                    txtfromSerie.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtToserie.Text, -1)) < 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập số serie đến", true);
                    txtToserie.Focus();
                    return false;
                }

                if (!Utility.isValidNumber(Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtfromSerie.Text)), Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtToserie.Text))))
                {
                    Utility.SetMsg(lblMsg, "Số serie từ phải <= Số serie đến", true);
                    txtToserie.Focus();
                    return false;
                }
            }
            return true;
        }
        bool isValidOneOf()
        {
            return cboQuyen.SelectedValue.ToString() != "-1" || Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtfromSerie.Text, -1)) > 0 || Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtToserie.Text, -1)) > 0;
        }
        MoneyByLetter _moneyByLetter = new MoneyByLetter();
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
                if (!isValidData()) return;
                _reportTable =
                  BAOCAO_NGOAITRU.BaocaoDanhsachhoadonThuphi(chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                  chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate, chkLoaitimkiem.Checked ? 1 : 0,
                  Utility.sDbnull(cboDoituongKCB.SelectedValue, -1),
                                                   Utility.sDbnull(cbonhanvien.SelectedValue, -1),
                                                   Utility.Int32Dbnull(cboNTNT.SelectedValue, -1), 
                                                   Utility.Int32Dbnull(cboHoadon.SelectedValue, -1),chkMaQuyen.Checked? Utility.sDbnull(cboQuyen.SelectedValue,"-1"):"-1",
                                                   Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtfromSerie.Text)),Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtToserie.Text)),
                                                   Utility.sDbnull(cbokhoa.SelectedValue, -1));

                Utility.SetDataSourceForDataGridEx(grdList, _reportTable, false, true, "1=1", "");
                Janus.Windows.GridEX.GridEXColumn gridExColumnTientong = grdList.RootTable.Columns["tong_Tien"];
                tong_tien = Utility.DecimaltoDbnull(grdList.GetTotal(gridExColumnTientong, Janus.Windows.GridEX.AggregateFunction.Sum));


                if (_reportTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
                string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} -TNV :{3} - Loại điều trị :{4} ", dtFromDate.Text, dtToDate.Text,
                                        cboDoituongKCB.SelectedIndex > 0
                                            ? Utility.sDbnull(cboDoituongKCB.Text)
                                            : "Tất cả",
                                        cbonhanvien.SelectedIndex > 0
                                            ? Utility.sDbnull(cbonhanvien.Text)
                                            : "Tất cả", cboNTNT.SelectedIndex > 0 ? Utility.sDbnull(cboNTNT.Text) : "Tất cả");
                Utility.UpdateLogotoDatatable(ref _reportTable);

                var crpt = Utility.GetReport("baocao_danhsachhoadonthuphi", ref tieude, ref reportname);
                if (crpt == null) return;


                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, _reportTable.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(_reportTable);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "baocao_danhsachhoadonthuphi";
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", Condition);
                Utility.SetParameterValue(crpt, "Hienthinhom", Utility.Int32Dbnull(cboHoadon.SelectedValue, -1) == 0 ? 0 : 1);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());

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
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
                    return;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
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

       

        private void frm_baocaodanhsachhoadonthuphi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

        private void grdList_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

      
    }
}
