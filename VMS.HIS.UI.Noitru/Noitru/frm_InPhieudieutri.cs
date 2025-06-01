using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using System.IO;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_InPhieudieutri : Form
    {
        private string _rowFilter = "1=1";
        public DataTable m_dtPhieuDieuTriChonIn = new DataTable();
        public KcbLuotkham objLuotkham;
        public DataTable m_dtPhieudieutri = new DataTable();
        bool m_blnLoaded = false;
        public frm_InPhieudieutri()
        {
            InitializeComponent();
            InitEvents();
            dtNgayInPhieu.Value = globalVariables.SysDate;
            cauhinh();
        }
        void InitEvents()
        {
            cboKhoanoitru.SelectedIndexChanged += cboKhoanoitru_SelectedIndexChanged;
        }

        void cboKhoanoitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            string department_id = Utility.sDbnull(cboKhoanoitru.SelectedValue, globalVariables.idKhoatheoMay.ToString());
            bool IsAdmin = globalVariables.IsAdmin || (globalVariablesPrivate.objNhanvien != null && Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac"));
            m_dtPhieudieutri = new KCB_THAMKHAM().NoitruTimkiemphieudieutriTheoluotkham(Utility.Bool2byte(IsAdmin), "01/01/1900", objLuotkham.MaLuotkham,
                    (int)objLuotkham.IdBenhnhan, department_id, 0);
            _rowFilter = "1=1";
            if (!chkHienthiCaDaIn.Checked)
            {
                _rowFilter = string.Format("{0}={1}", NoitruPhieudieutri.Columns.TthaiIn, 0);
            }
            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtPhieudieutri, false, true, _rowFilter, NoitruPhieudieutri.Columns.NgayDieutri + " desc");
        }
        private void cauhinh()
        {
            try
            {
                
            }
            catch (Exception)
            {
               
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện việc load thông tin của Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_InPhieudieutri_Load(object sender, EventArgs e)
        {
            DataBinding.BindDataCombox(cboKhoanoitru,
                                                THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1),
                                                DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                "---Chọn khoa nội trú---", false);
            cboKhoanoitru.SelectedIndex=Utility.GetSelectedIndex(cboKhoanoitru,globalVariables.idKhoatheoMay.ToString());
            m_blnLoaded = true;

            string department_id = Utility.sDbnull(cboKhoanoitru.SelectedValue, globalVariables.idKhoatheoMay.ToString());
            bool IsAdmin =Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac");
            m_dtPhieudieutri = new KCB_THAMKHAM().NoitruTimkiemphieudieutriTheoluotkham(Utility.Bool2byte(IsAdmin), "01/01/1900", objLuotkham.MaLuotkham,
                     (int)objLuotkham.IdBenhnhan, department_id, 0);

            _rowFilter = "1=1";
            if (!chkHienthiCaDaIn.Checked)
            {
                _rowFilter = string.Format("{0}={1}", NoitruPhieudieutri.Columns.TthaiIn, 0);
            }
            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtPhieudieutri, false, true, _rowFilter, NoitruPhieudieutri.Columns.NgayDieutri + " desc");
            grdList.MoveFirst();

            grdList.CheckAllRecords();
        }
       
        /// <summary>
        /// hàm thực hiện việc hiển thị cần in 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkHienthiCaDaIn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _rowFilter = "1=1";
                if (!chkHienthiCaDaIn.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", NoitruPhieudieutri.Columns.TthaiIn, 0);
                }
                m_dtPhieudieutri.DefaultView.RowFilter = "1=1";
                m_dtPhieudieutri.DefaultView.RowFilter = _rowFilter;
                m_dtPhieudieutri.AcceptChanges();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        /// <summary>
        /// hàm thực hiện viễ xử lý thông tin in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
        }

        private void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
           
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu điều trị y lệnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInDieuTri_Click(object sender, EventArgs e)
        {

            INPHIEU_DIEUTRI();
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu điều trị cần thiết
        /// </summary>
        private void INPHIEU_DIEUTRI()
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một phiếu điều trị để in ", "Thông báo", MessageBoxIcon.Information);
                    return;
                }

                if (grdList.GetCheckedRows().Length <= 0)
                {
                    grdList.CurrentRow.IsChecked = true;
                }
                var TreatmentId = new StringBuilder("-1");
                foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    TreatmentId.Append(",");
                    TreatmentId.Append(gridExRow.Cells[NoitruPhieudieutri.Columns.IdPhieudieutri].Value.ToString());
                    gridExRow.BeginEdit();
                    gridExRow.Cells[NoitruPhieudieutri.Columns.TthaiIn].Value = 1;
                    gridExRow.EndEdit();
                }
                grdList.UpdateData();
                m_dtPhieudieutri.AcceptChanges();
                DataSet dsPrint;
                dsPrint = new noitru_phieudieutri().NoitruLaythongtinphieudieutriIn(TreatmentId.ToString());
                DataTable mdtDataPhieuDieuTri;
                mdtDataPhieuDieuTri = dsPrint.Tables[0];
                THU_VIEN_CHUNG.CreateXML(mdtDataPhieuDieuTri, "noitru_phieudieutri");
                foreach (DataRow row in mdtDataPhieuDieuTri.Rows)
                {
                    var YLENH = new StringBuilder("");
                    if (chkInYLenhThuocCLS.Checked)
                    {
                        //Tạo thông tin thuốc. 
                        List<DataRow> query = (dsPrint.Tables[1].AsEnumerable().Where(
                           chidinh => Utility.Int32Dbnull(chidinh["id_phieudieutri"]) == Utility.Int32Dbnull(row["id_phieudieutri"])
                                      &&
                                      Utility.Int32Dbnull(chidinh["id_loaithanhtoan"]) ==
                                      Utility.Int32Dbnull(KieuLoaiThanhToan.Thuoc))).ToList();
                        if (query.Any())
                        {
                            YLENH.Append("</br>");
                            foreach (DataRow dr in query)
                            {
                                YLENH.Append("<p>");
                                YLENH.Append(string.Format("<b>{0} ( {1} )</b>",  Utility.sDbnull(dr["TEN"]), Utility.sDbnull(dr["ten_hoatchat"])));
                                YLENH.Append("<span > x </span> <b>");
                                YLENH.Append(Utility.sDbnull(dr["SOLUONG"]));
                                YLENH.Append(" ");
                                YLENH.Append(Utility.sDbnull(dr["DONVI"]));
                                YLENH.Append("</b></br>");
                                YLENH.Append(string.Format("<i>{0}</i>", dr["sDesc"]));
                                YLENH.Append("</p><br>");
                            }
                        }
                        //Tạo thông tin chỉ định
                       query = (from chidinh in dsPrint.Tables[1].AsEnumerable()
                                               where
                                                   Utility.Int32Dbnull(chidinh["id_phieudieutri"]) ==
                                                   Utility.Int32Dbnull(row["id_phieudieutri"])
                                                   &&
                                                   Utility.Int32Dbnull(chidinh["id_loaithanhtoan"]) ==
                                                   Utility.Int32Dbnull(KieuLoaiThanhToan.CLS)
                                               select chidinh).ToList();
                        if (query.Any())
                        {
                            var q = (from p in query
                                     select Utility.sDbnull(p["TEN"]));
                            string dichvu = string.Join(",", q.ToArray<string>());
                            //foreach (DataRow dr in query)
                            //{
                                YLENH.Append("<p>");
                                YLENH.Append(string.Format("{0}", dichvu));
                                YLENH.Append("</p>");
                            //}
                        }

                        YLENH.Append("</br> ");
                        YLENH.Append("</br> ");
                        row["YLENH"] = YLENH.ToString();
                    }
                    else
                    {
                        //var YLENH = new StringBuilder("");
                        YLENH.Append("</br> ");
                        YLENH.Append("</br> ");
                        //YLENH.Append(string.Format("Người lập y lệnh {0}", Utility.sDbnull(row["ten_bacsidieutri"])));
                        //YLENH.Append("</br> ");
                        row["YLENH"] = YLENH.ToString();
                    }
                }


                Utility.UpdateLogotoDatatable(ref mdtDataPhieuDieuTri);
                mdtDataPhieuDieuTri.AcceptChanges();

                InphieuDieuTri(mdtDataPhieuDieuTri, dtNgayInPhieu.Value);

                foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells[NoitruPhieudieutri.Columns.TthaiIn].Value = 1;
                    gridExRow.EndEdit();
                }
                grdList.UpdateData();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
        public static void InphieuDieuTri(DataTable dtPrint, DateTime ngayin)
        {

            string tieude = "", reportname = "";
            var crpt = Utility.GetReport("noitru_phieudieutri", ref tieude, ref reportname);
            if (crpt == null) return;
            //var crpt = new crpt_PhieuDieuTri();
            var objForm = new frmPrintPreview("IN PHIẾU ĐIỀU TRỊ", crpt, true, true);
            crpt.SetDataSource(dtPrint);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = "noitru_phieudieutri";
            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
            Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(ngayin));
            Utility.SetParameterValue(crpt,"sTitleReport", tieude);
            Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());

            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            objForm.Dispose();

        }
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._NoitruProperties);
            frm.ShowDialog();
            cauhinh();
        }

        /// <summaắtry>
        /// hàm thực hiện việc phím t
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_InPhieudieutri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 || (e.Control && e.KeyCode==Keys.P) || e.KeyCode==Keys.P) cmdInDieuTri.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        }
    }
}