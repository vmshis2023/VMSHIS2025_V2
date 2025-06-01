using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using System.IO;
using VNS.HIS.UI.Forms.HinhAnh;
using CrystalDecisions.CrystalReports.Engine;
using VNS.Properties;
using System.Threading;
namespace VNS.HIS.UI.HinhAnh
{
    public partial class frm_chonXN_Print : Form
    {
        string ma_luotkham = "";
        Int64 id_benhnhan = -1;
        string MaBenhpham = "";
        string MaChidinh = "";
        int IdChitietdichvu = -1;
        int currRowIdx = -1;
        int id_chidinh = -1;
        int id_dichvu = -1;
        int co_chitiet = -1;
        public frm_chonXN_Print(string ma_luotkham, string MaBenhpham)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.ma_luotkham = ma_luotkham;
            this.MaBenhpham = MaBenhpham;
            this.Load += frm_chonXN_Print_Load;
            this.FormClosing += frm_chonXN_Print_FormClosing;
            this.KeyDown += frm_chonXN_Print_KeyDown;
        }

        void frm_chonXN_Print_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            else if (e.KeyCode == Keys.Escape) this.Close();
        }

        void frm_chonXN_Print_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
      
     
        void frm_chonXN_Print_Load(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            try
            {
                DataTable dt = SPs.ClsTimkiemClsthuocXetnghiem(ma_luotkham, "ALL", -1).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdChidinh, dt, true, true, "1=1", VKcbChidinhcl.Columns.SttHthiDichvu + " asc");
                grdChidinh.ExpandGroups();
            }
            catch (Exception)
            {
                ma_luotkham = "";

            }
        }
        void CheckKQ(ref DataTable v_dtData)
        {
            foreach (DataRow dr in v_dtData.Rows)
            {
                try
                {
                    SysSystemParameter _p = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("DUONGTINH").ExecuteSingle<SysSystemParameter>();
                    string orgkq=Utility.DoTrim(Utility.sDbnull(dr["ket_qua"], ""));
                   
                    string thamchieu="";
                    if (Utility.Int32Dbnull(dr["id_gioitinh"], -1) == 0)
                        thamchieu = Utility.sDbnull(dr["binhthuong_nu"], "");
                    else
                        thamchieu = Utility.sDbnull(dr["binhthuong_nam"], "");
                    if (!Utility.IsNumeric(orgkq))//Âm tính, dương tính,+,-, AT,DT
                    {
                        if (_p != null && _p.SValue != null && _p.SValue.TrimEnd().TrimStart().Length > 0)
                        {
                            List<string> lstDt = _p.SValue.ToUpper().Split(',').ToList<string>();
                            if (lstDt.Contains(orgkq.ToUpper()))
                                dr["caothap"] = 1;
                        }
                    }
                    else
                    {
                        Decimal kq = Utility.DecimaltoDbnull(chuanhoa(orgkq), 1010101);
                        if (thamchieu != "" && Utility.IsNumeric(kq))
                        {
                            if (thamchieu.Split('-').Count() == 2)
                            {
                                Decimal min = Utility.DecimaltoDbnull(chuanhoa(thamchieu.Split('-')[0]), 1010101);
                                Decimal max = Utility.DecimaltoDbnull(chuanhoa(thamchieu.Split('-')[1]), 1010101);
                                if (min != 1010101 && max != 1010101 && kq != 1010101 && max > min)
                                {
                                    if (min > kq)
                                        dr["caothap"] = -1;
                                    if (max < kq)
                                        dr["caothap"] = 1;
                                }
                            }
                            else if (thamchieu.Contains("<="))
                            {
                                decimal com = Utility.DecimaltoDbnull(chuanhoa(thamchieu.Replace("<=", "")), 1010101);
                                if (com != 1010101 && kq != 1010101)
                                {
                                    if (kq > com)
                                        dr["caothap"] = 1;
                                }
                            }
                            else if (thamchieu.Contains(">="))
                            {
                                decimal com = Utility.DecimaltoDbnull(chuanhoa(thamchieu.Replace(">=", "")), 1010101);
                                if (com != 1010101 && kq != 1010101)
                                {
                                    if (kq < com)
                                        dr["caothap"] = -1;
                                }
                            }
                            else if (thamchieu.Contains("<"))
                            {
                                decimal com = Utility.DecimaltoDbnull(chuanhoa(thamchieu.Replace("<", "")), 1010101);
                                if (com != 1010101 && kq != 1010101)
                                {
                                    if (kq >= com)
                                        dr["caothap"] = 1;
                                }
                            }
                            else if (thamchieu.Contains(">"))
                            {
                                decimal com = Utility.DecimaltoDbnull(chuanhoa(thamchieu.Replace(">", "")), 1010101);
                                if (com != 1010101 && kq != 1010101)
                                {
                                    if (kq <= com)
                                        dr["caothap"] = -1;
                                }
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {


                }
            }

        }
         string chuanhoa( string  s)
        {
            try
            {
                if(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator==".")
                   return s.Replace(",",".");
                else
                    return s.Replace(".",",");
            }
            catch (Exception)
            {
                return "1010101";
               
            }
         }
        private void InKQXN()
        {
            string dsdv = "-1";
            if(grdChidinh.GetCheckedRows().Count()>0)
            {
                var ds = (from p in grdChidinh.GetCheckedRows()
                         select p.Cells["id_dichvu"].Value.ToString()).ToArray<string>();
                dsdv = string.Join(",", ds);
            }
            DataTable v_dtData = SPs.ClsTimkiemKQXNIn(Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdChidinh, "id_benhnhan")), Utility.sDbnull(Utility.GetValueFromGridColumn(grdChidinh, "ma_luotkham")), Utility.sDbnull(Utility.GetValueFromGridColumn(grdChidinh, "ma_chidinh")), dsdv).GetDataSet().Tables[0];
            CheckKQ(ref v_dtData);
            THU_VIEN_CHUNG.CreateXML(v_dtData, "chidinh_inKQXN.xml");
            
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "", reportCode = "";
            reportCode = "chidinh_inKQXN";
            reportDocument = Utility.GetReport("chidinh_inKQXN", ref tieude, ref reportname);
            
            if (reportDocument == null) return;
            
            ReportDocument crpt = reportDocument;
            frmPrintPreview objForm = new frmPrintPreview("IN KQXN", crpt, true, true);
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", "KẾT QUẢ XÉT NGHIỆM");
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }
        private void cmdInKQXN_Click(object sender, EventArgs e)
        {
            InKQXN();
        }
    }
    
}
