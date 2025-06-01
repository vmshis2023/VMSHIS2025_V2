using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using Janus.Windows.GridEX;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_quanly_baocao : Form
    {
        DataTable dtReport;
      
        public frm_quanly_baocao()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            cmdThoat.Click += new EventHandler(btnExit_Click);
            cmdSua.Click+=new EventHandler(cmdSua_Click);
            cmdXoa.Click+=new EventHandler(cmdXoa_Click);
            cmdThemMoi.Click+=new EventHandler(cmdThemMoi_Click);
            grdList.ApplyingFilter+=new CancelEventHandler(grdList_ApplyingFilter);
            grdList.DoubleClick +=new EventHandler(grdList_DoubleClick);
            grdList.SelectionChanged+=new EventHandler(grdList_SelectionChanged);
            grdList.CellUpdated += GrdList_CellUpdated;
        }

        private void GrdList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                string colKey = e.Column.Key;
                string colValue = Utility.sDbnull(grdList.GetValue(colKey));
                string mabc = Utility.sDbnull(grdList.GetValue(SysReport.Columns.MaBaocao));
                int num = 0;
                if (colKey == SysReport.Columns.MaPhieuEmr)
                {
                     num = new Update(SysReport.Schema).Set(SysReport.Columns.MaPhieuEmr).EqualTo(colValue).Where(SysReport.Columns.MaBaocao).IsEqualTo(mabc).Execute();
                }
                else
                     num = new Update(SysReport.Schema).Set(colKey).EqualTo(colValue).Where(SysReport.Columns.MaBaocao).IsEqualTo(mabc).Execute();
                grdList.UpdateData();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        private void ModifyButtons()
        {
            cmdSua.Enabled = Utility.isValidGrid(grdList);
            cmdXoa.Enabled = Utility.isValidGrid(grdList);
        }

        private void LoadDataReport()
        {
            try
            {
                dtReport = SPs.DanhmucLaydanhsachbaocaohethong().GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList,dtReport,true,true,"1=1","stt_nhombaocao,ma_baocao");
                DataTable dtPhieuEMR = new Select("*").From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("EMR_PHIEU")
                .OrderAsc(DmucChung.Columns.SttHthi)
                .ExecuteDataSet().Tables[0];
                if (grdList.DropDowns.Contains("cboPhieuEmr"))
                {
                    grdList.DropDowns["cboPhieuEmr"].DataSource = dtPhieuEMR;
                }
            }
            catch (Exception ex) { }
        }

        private void frm_quanly_baocao_Load(object sender, EventArgs e)
        {
          
            LoadDataReport();
            ModifyButtons();            
        }
       
        private void AddNew()
        {
            try
            {
                frm_themmoi_baocao frm = new frm_themmoi_baocao();
                frm.objReport = new SysReport();
                frm.m_enAct = action.Insert;
                frm.dt_data = dtReport;
                frm.grdList = grdList;
                frm.ShowDialog();
            }
            catch (Exception ex) { }                        
        }

        private void Edit()
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                string mabaocao = grdList.CurrentRow.Cells[SysReport.Columns.MaBaocao].Value.ToString();
                SysReport obj = new SysReport(mabaocao);
                frm_themmoi_baocao frm = new frm_themmoi_baocao();
                frm.grdList = grdList;
                frm.objReport = obj;
                frm.dt_data = dtReport;
                frm.m_enAct = action.Update;
                frm.ShowDialog();
            }
            catch (Exception ex) { }
        }

       
        private void Delete()
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                string mabaocao = "";
                string lstID = "";
                foreach (var item in grdList.GetCheckedRows())
                {
                    mabaocao = item.Cells[SysReport.Columns.MaBaocao].Value.ToString();
                    lstID += "," + mabaocao;
                }
                if (lstID.Trim() == "")
                    lstID = grdList.CurrentRow.Cells[SysReport.Columns.MaBaocao].Value.ToString();
                if (lstID.Length<=0) return;
                
                if (SPs.DanhmucXoadanhsachbaocaohethong(lstID).Execute()>0)
                {
                    Utility.ShowMsg("Xóa dữ liệu thành công", "Thông báo", MessageBoxIcon.Information);
                    lstID = "," + lstID + ",";
                    DataTable dttemp = (from p in dtReport.AsEnumerable()
                                        where !lstID.Contains("," + Utility.sDbnull(p[SysReport.Columns.MaBaocao]) + ",")
                                        select p).CopyToDataTable();

                    dtReport = dttemp.Copy();
                    Utility.SetDataSourceForDataGridEx(grdList, dtReport, true, true, "1=1", "stt_nhombaocao,ma_baocao");
                }
                else
                {
                    Utility.ShowMsg("Xóa dữ liệu không thành công", "Thông báo", MessageBoxIcon.Information);
                }
                ModifyButtons();
            }
            catch (Exception ex) { }
        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            AddNew();
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin bản ghi đang chọn không ?", "Thông báo", true)) 
                Delete();
        }

        private void frm_quanly_baocao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdThoat.PerformClick();
            if (e.KeyCode == Keys.F5) LoadDataReport();
            if (e.Control && e.KeyCode == Keys.N) cmdThemMoi.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdSua.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdXoa.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyButtons();
        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            ModifyButtons();
        }

        private void cmdUpdateXml_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog filediag = new OpenFileDialog();
                filediag.Filter="XML|*.xml";
                string ma_baocao="";
                if (filediag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(filediag.FileName);
                    foreach(DataRow dr in ds.Tables[0].Rows)
                        if (dtReport.Select("ma_baocao='" + dr["ma_baocao"].ToString() + "'").Length <= 0)
                        {
                            DataRow newDr = dtReport.NewRow();
                            Utility.CopyData(dr, ref newDr);
                            dtReport.Rows.Add(newDr);
                            ma_baocao = dr["ma_baocao"].ToString();
                            SysReport newreport = new SysReport();
                            newreport.MaBaocao = dr[SysReport.Columns.MaBaocao].ToString();
                            newreport.MaNhom = dr[SysReport.Columns.MaNhom].ToString();
                            newreport.TieuDe = dr[SysReport.Columns.TieuDe].ToString();
                            newreport.FileRieng = dr[SysReport.Columns.FileRieng].ToString();
                            newreport.FileChuan = dr[SysReport.Columns.FileChuan].ToString();
                            newreport.MoTa = dr[SysReport.Columns.MoTa].ToString();
                            newreport.PrintNumber = Utility.Int16Dbnull(dr[SysReport.Columns.PrintNumber], 0);
                            newreport.NguoiTao = globalVariables.UserName;
                            newreport.NgayTao = DateTime.Now;
                            newreport.IsNew = true;
                            newreport.Save();
                        }
                    Utility.ShowMsg("Đã import dữ liệu báo cáo thành công. Nhấn OK để refresh lại dữ liệu");
                    dtReport = SPs.DanhmucLaydanhsachbaocaohethong().GetDataSet().Tables[0];
                    dtReport.AcceptChanges();
                    Utility.GonewRowJanus(grdList,"ma_baocao",ma_baocao);
                }
            }
            catch (Exception ex)
            {
                
               Utility.CatchException(ex);
            }
        }
        private string PathFolder = string.Format("{0}/{1}", Application.StartupPath, "System_Report");
        private void cmdXML_Click(object sender, EventArgs e)
        {
            List<string> lstMaBC = (from p in grdList.GetCheckedRows().AsEnumerable()
                                    select Utility.sDbnull(p.Cells["ma_baocao"].Value, "")).Distinct().ToList<string>();
            try
            {
                SysReportCollection objDulieuDkCollection = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).In(lstMaBC)
             .ExecuteAsCollection<SysReportCollection>();
                DataTable xmlDulieuDK = objDulieuDkCollection.ToDataTable();
                if (!System.IO.Directory.Exists(PathFolder))
                {
                    System.IO.Directory.CreateDirectory(PathFolder);
                }

                string path = string.Format("{0}/{1}.xml", PathFolder, Utility.sDbnull("SysReportXML"));
                xmlDulieuDK.WriteXml(path);
                Utility.ShowMsg("Xuất dữ liệu thành công", "Thông báo");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
           
        }

        private void cmdDuplicate_Click(object sender, EventArgs e)
        {
            try
            {
                int i=1;
                foreach (GridEXRow row in grdList.GetCheckedRows())
                {
                    SysReport _newR=SysReport.FetchByID( row.Cells["ma_baocao"].Value.ToString());
                    if (_newR != null)
                    {
                        _newR.MaBaocao = _newR.MaBaocao + "_copy" + i.ToString();
                        _newR.NgayTao = DateTime.Now;
                        _newR.NguoiTao = globalVariables.UserName;
                        _newR.IsNew = true;
                        _newR.Save();
                        i += 1;
                    }
                }
                Utility.ShowMsg("Sao chép báo cáo thành công. Nhấn OK để nạp lại danh mục");
                LoadDataReport();
                ModifyButtons();   
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
