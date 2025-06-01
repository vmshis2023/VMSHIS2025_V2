using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using Microsoft.VisualBasic;
using VNS.Properties;

namespace VNS.HIS.UI.Baocao
{
    public partial class frm_INPHIEU_CLS : Form
    {
        public KcbChidinhcl objAssignInfo;
        private DataTable m_dtChiDinhCLS=new DataTable();
        private DataTable m_dtReport=new DataTable();
        public frm_INPHIEU_CLS()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            txtTieuDe.LostFocus+=new EventHandler(txtTieuDe_LostFocus);
            
            dtNgayInPhieu.Value = globalVariables.SysDate;
         
            radKhoA4.CheckedChanged+=new EventHandler(radKhoA4_CheckedChanged);
            radA5.CheckedChanged+=new EventHandler(radA5_CheckedChanged);
          ChonCauHinhIn();
        }

        private void radA5_CheckedChanged(object sender, EventArgs e)
        {
            WriteCauHinhIn();
        }
        private bool b_hasloadData = false;
        private string PathKhoGiayA5 = Application.StartupPath + @"\KhoGiay.txt";
        private void ChonCauHinhIn()
        {
            try
            {
               
               
                b_hasloadData = true;
            }catch(Exception exception)
            {
                
            }
           
        }

      
        private void WriteCauHinhIn()
        {
            try
            {
              
            }
            catch (Exception)
            {
                
                
            }
            
        }
        /// <summary>
        /// hàm thực hiện khổ A4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radKhoA4_CheckedChanged(object sender, EventArgs e)
        {
            WriteCauHinhIn();
        }
        private void txtTieuDe_LostFocus(object  sender,EventArgs eventArgs)
        {
           
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_INPHIEU_CLS_Load(object sender, EventArgs e)
        {
            getDataChiDinh();
        }

        private void getDataChiDinh()
        {
            if(objAssignInfo!=null)
            {
                lblAssignCode.Text = Utility.sDbnull(objAssignInfo.MaChidinh);
                DmucNhanvien objStaff = DmucNhanvien.FetchByID(objAssignInfo.IdBacsiChidinh);
                if(objStaff!=null)
                {
                    lblStaffName.Text = Utility.sDbnull(objStaff.TenNhanvien);
                }
                else
                {
                    lblStaffName.Text = Utility.sDbnull(objAssignInfo.NgayTao);
                }
                getData();
            }
            else
            {
                cmdInPhieuXN.Enabled = false;
            }
        }

        private void getData()
        {
            m_dtChiDinhCLS = null;
                //SPs.ClsLaokhoaInphieuChidinhCls(objAssignInfo.AssignCode, Utility.sDbnull(objAssignInfo.PatientCode),
                //                                Utility.Int32Dbnull(objAssignInfo.PatientId,-1)).GetDataSet().Tables[0];
            Utility.AddColumToDataTable(ref m_dtChiDinhCLS,"STT",typeof(Int32));
            Int32 idx = 1;
                
            foreach (DataRow drv in m_dtChiDinhCLS.Rows)
            {
                drv["STT"] = idx;
                idx++;
            }
            m_dtChiDinhCLS.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdAssignInfo, m_dtChiDinhCLS,false,true,"1=1","");

            try
            {
                grdAssignInfo.RootTable.SortKeys.Clear();
                Janus.Windows.GridEX.GridEXColumn gridExColumnGroup = grdAssignInfo.RootTable.Columns["IntOrderGroup"];
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdAssignInfo.RootTable.Columns["IntOrder"];
                Janus.Windows.GridEX.GridEXSortKey gridExSortKeyGroup = new GridEXSortKey(gridExColumnGroup, Janus.Windows.GridEX.SortOrder.Ascending);
                Janus.Windows.GridEX.GridEXSortKey gridExSortKey = new GridEXSortKey(gridExColumn, Janus.Windows.GridEX.SortOrder.Ascending);
                grdAssignInfo.RootTable.SortKeys.Add(gridExSortKeyGroup);
                grdAssignInfo.RootTable.SortKeys.Add(gridExSortKey);
                grdAssignInfo.CheckAllRecords();
            }
            catch (Exception exception)
            {

                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            ModifyCommand();
        }
        private void ModifyCommand()
        {
            cmdInPhieuXN.Enabled = grdAssignInfo.GetCheckedRows().Length > 0;
          
        }

        private void UpdateChoPhepIn()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignInfo.GetDataRows())
            {
                if(gridExRow.RowType==RowType.Record)
                {
                    int ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value);
                    //new Update(KcbChidinhclsChitiet.Schema)
                    //    .Set(KcbChidinhclsChitiet.Columns.ChoPhepIn).EqualTo(gridExRow.IsChecked ? 1 : 0)
                    //    .Where(KcbChidinhclsChitiet.Columns.IdChidinhChitiet).IsEqualTo(ID).Execute();
                }
            }
        }

        /// <summary>
        /// hàm thực hiện viêc in phiếu chỉ định cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            UpdateChoPhepIn();
            InphieuCLS();
        }

        private void InphieuCLS()
        {
            m_dtReport = null;
                //SPs.ClsLaokhoaInphieuChidinhCls(objAssignInfo.AssignCode, objAssignInfo.PatientCode,
                //                                Utility.Int32Dbnull(objAssignInfo.PatientId)).GetDataSet().Tables[0];
            if(m_dtReport.Rows.Count<=0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi nào, bạn xem lại, phải chọn một bản ghi để thực hiện việc in phiếu chỉ định","Thông báo",MessageBoxIcon.Warning);
                return;
            }
            m_dtReport = (from lox in m_dtReport.AsEnumerable().Cast<DataRow>()
                          where Utility.Int32Dbnull(lox["CHO_PHEP_IN"], 0) == 1
                          select lox
                         ).CopyToDataTable();
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            Utility.AddColumToDataTable(ref m_dtReport,"SO_PHIEU_BARCODE",typeof(byte[]));
            byte[] arrBarCode = null;
            Utility.CreateBarcodeData(ref m_dtReport, "", ref arrBarCode);

            foreach (DataRow drv in m_dtReport.Rows)
            {
                drv["SO_PHIEU_BARCODE"] = arrBarCode;
            }
            m_dtReport.AcceptChanges();
            string KhoGiay = "A5";
            if (radKhoA4.Checked) KhoGiay = "A4";
            try
            {
                ReportDocument reportDocument=new ReportDocument();
                 string tieude="", reportname = "";
                switch (KhoGiay)
                {
                    case "A5":
                        reportDocument = Utility.GetReport("thamkham_PHIEU_CHIDINH_CLS_A5" ,ref tieude,ref reportname);
                        break;
                    case "A4":
                        reportDocument = Utility.GetReport("thamkham_PHIEU_CHIDINH_CLS" ,ref tieude,ref reportname);
                        break;
                    default:
                        reportDocument = Utility.GetReport("thamkham_PHIEU_CHIDINH_CLS" ,ref tieude,ref reportname);
                        break;
                }
                if (reportDocument == null) return;
                var crpt = reportDocument;
                frmPrintPreview objForm = new frmPrintPreview(txtTieuDe.Text, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                //try
                //{
                crpt.SetDataSource(m_dtReport);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                // Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt,"sTitleReport", txtTieuDe.Text);
                Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {


            }
        }

        private void grdAssignInfo_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifyCommand();
        }

        private void grdAssignInfo_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifyCommand();
        }

        private void frm_INPHIEU_CLS_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F4)cmdInPhieuXN.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin của update vào cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignInfo_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            ModifyCommand();
        }

        private void grdAssignInfo_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            
            ModifyCommand();
        }
    }
   
}
