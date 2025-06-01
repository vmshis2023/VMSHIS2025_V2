using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using System.Collections.Generic;
using VNS.HIS.UI.Classess;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_InphieuCLS : Form
    {
        private DataTable m_dtNhomInPhieu = new DataTable();
        public bool Hos_Status = false;
        public KcbLuotkham objLuotkham;
        public bool bCancel = false;
        DataTable dt_Data = new DataTable();
        bool autoSelectAll = false;
        List<string> nhomcls = new List<string>();
        bool AllowChanged = false;
        public frm_InphieuCLS(DataTable dt_Data,bool autoSelectAll)
        {
            InitializeComponent();
            this.autoSelectAll = autoSelectAll;
            this.dt_Data = dt_Data;
            Utility.SetVisualStyle(this);
            dtNgayInPhieu.Value = THU_VIEN_CHUNG.GetSysDateTime();
            CauHinh();
            ResetNhominCLS();
            optA5.CheckedChanged += optA5_CheckedChanged;
            optA4.CheckedChanged += optA5_CheckedChanged;
            chkPrintPreview.CheckedChanged += chkPrintPreview_CheckedChanged;
            this.Shown += frm_InphieuCLS_Shown;
            AllowChanged = true;
        }

        void frm_InphieuCLS_Shown(object sender, EventArgs e)
        {
            cmdInChungPhieu.Enabled = cmdIntach.Enabled = optA4.Enabled = optA5.Enabled = chkPrintPreview.Enabled = dtNgayInPhieu.Enabled = grdList.RowCount > 0;
            if (autoSelectAll) grdList.CheckAllRecords();
        }

        void chkPrintPreview_CheckedChanged(object sender, EventArgs e)
        {
            if (!AllowChanged) return;
            if (PropertyLib._MayInProperties != null)
            {
                PropertyLib._MayInProperties.PreviewInCLS = chkPrintPreview.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
        }

        void optA5_CheckedChanged(object sender, EventArgs e)
        {
            if (!AllowChanged) return;
            if (PropertyLib._MayInProperties != null)
            {
                PropertyLib._MayInProperties.CoGiayInCLS = optA4.Checked ? Papersize.A4 : Papersize.A5;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
        }
        private string _tieude = "";
        private void CauHinh()
        {
            if (PropertyLib._MayInProperties != null)
            {
                optA4.Checked = PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4;
                optA5.Checked = !optA4.Checked;
                chkPrintPreview.Checked = PropertyLib._MayInProperties.PreviewInCLS;
            }
        }
        public string ma_chidinh { get; set; }
        public long id_phieu { get; set; }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_InphieuCLS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F5) Intachphieu();
            if (e.KeyCode == Keys.F4) InChungphieu();
        }


        void ResetNhominCLS()
        {
            try
            {
                if (dt_Data.Rows.Count <= 0) return;
                id_phieu = Utility.Int64Dbnull(dt_Data.Rows[0][KcbChidinhclsChitiet.Columns.IdChidinh],
                                                    -1);
                 ma_chidinh = Utility.sDbnull(dt_Data.Rows[0][KcbChidinhcl.Columns.MaChidinh], "");



                foreach (DataRow dr in dt_Data.Rows)
                {
                    if (Utility.Int64Dbnull(dr[KcbChidinhclsChitiet.Columns.IdChidinh]) == id_phieu)
                        if (!nhomcls.Contains(Utility.sDbnull(dr["nhom_in_cls"])))
                        {
                            nhomcls.Add(Utility.sDbnull(dr["nhom_in_cls"]));
                        }
                }
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung(globalVariables.DC_NHOMIN_CLS, true);
                if (!dtNhomin.Columns.Contains("ma_phieu"))
                    dtNhomin.Columns.AddRange(new DataColumn[] { new DataColumn(KcbChidinhcl.Columns.IdChidinh, typeof(Int64)), new DataColumn(KcbChidinhcl.Columns.MaChidinh, typeof(string)) });
                DataTable dttempt = dtNhomin.Clone();
                foreach (DataRow dr in dtNhomin.Rows)
                {
                    dr[KcbChidinhcl.Columns.IdChidinh] = id_phieu;
                    dr[KcbChidinhcl.Columns.MaChidinh] = ma_chidinh;
                    if (nhomcls.Contains(Utility.sDbnull(dr[DmucChung.Columns.Ma], "")))
                        dttempt.ImportRow(dr);
                }
                Utility.SetDataSourceForDataGridEx(grdList, dttempt, false, true, "1=1", string.Format("{0} asc", DmucChung.Columns.SttHthi));
               
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        /// <summary>
        /// hàm thực hiện việc load thôn gtin lên form
        /// khi load thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_InphieuCLS_Load(object sender, EventArgs e)
        {
            try
            {
                cmdIntach.Focus();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        
      
       public void Intachphieu()
        {
            try
            {
                var actionResult = ActionResult.Error;
                string mayin = "";


                List<long> lstSelectedPrint = (from p in dt_Data.AsEnumerable()
                                               select Utility.Int64Dbnull(p[KcbChidinhclsChitiet.Columns.IdChitietchidinh], 0)).ToList();
                List<string> lstNhominCLS = (from p in grdList.GetCheckedRows().AsEnumerable()
                                             select Utility.sDbnull(p.Cells["MA"].Value, "")).Distinct().ToList<string>();
                if (lstNhominCLS.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một nhóm phiếu cần in tách");
                    return;
                }
                actionResult = KcbInphieu.InTachToanBoPhieuCls(lstSelectedPrint, (int)objLuotkham.IdBenhnhan,
                                                                 objLuotkham.MaLuotkham, id_phieu,
                                                                 ma_chidinh, lstNhominCLS, "",
                                                                 -1, true,
                                                                 ref mayin);


                if (actionResult == ActionResult.Success)
                {

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                GC.Collect();
            }

        }
        private void cmdInPhieuCLS_Click(object sender, EventArgs e)
       {
           Intachphieu();
            
        }
        public void InChungphieu()
        {
            try
            {
                var actionResult = ActionResult.Error;
                string mayin = "";


                List<long> lstSelectedPrint = (from p in dt_Data.AsEnumerable()
                                               select Utility.Int64Dbnull(p[KcbChidinhclsChitiet.Columns.IdChitietchidinh], 0)).ToList();
                string nhomincls = "ALL";

                actionResult = KcbInphieu.InphieuChidinhCls(lstSelectedPrint, (int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                                              id_phieu,
                                                              ma_chidinh, nhomincls, -1,
                                                              false,
                                                              ref mayin);

                if (actionResult == ActionResult.Success)
                {

                }
                // if (mayin != "") cboLaserPrinters.Text = mayin;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                GC.Collect();
            }
        }
        /// <summary>
        /// hàm thực hiện việc in chung phiếu cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInChungPhieu_Click(object sender, EventArgs e)
        {
            InChungphieu();

        }
        /// <summary>
        /// hàm thực hiện việc in thông tin của cận lâm sàng dịch vụ
        /// </summary>
        private void InphieuCLS_ChoBenhNhanDV()
        {
            //TAssignInfo objAssignInfo = TAssignInfo.FetchByID(id_phieu);
            //if(objAssignInfo!=null)
            //{

            //    DataTable mDtReport = SPs.ClsLaokhoaInphieuChidinhCls(objAssignInfo.AssignCode, objAssignInfo.PatientCode,
            //                                   Utility.Int32Dbnull(objAssignInfo.PatientId)).GetDataSet().Tables[0];
            //    if (mDtReport.Rows.Count <= 0)
            //    {
            //        Utility.ShowMsg("Không tìm thấy bản ghi nào, bạn xem lại, phải chọn một bản ghi để thực hiện việc in phiếu chỉ định", "Thông báo", MessageBoxIcon.Warning);
            //        return;
            //    }
            //    mDtReport = (mDtReport.AsEnumerable().Cast<DataRow>().Select(lox => lox)
            //        ).CopyToDataTable();
            //    Utility.UpdateLogotoDatatable(ref mDtReport);

            //    mDtReport.AcceptChanges();
            //    string khoGiay = "A5";
            //    if (radKhoA4.Checked) khoGiay = "A4";
            //    BusinessHelper.CreateXml(mDtReport, "IN_CLS_CHUNG.XML");
            //    VietBaIT.HISLink.Business.Reports.Implementation.InPhieuCLS.InphieuCLSBHYT(mDtReport, _tieude,
            //                                                                                dtNgayInPhieu.Value,
            //                                                                                khoGiay,chkInToanBoRamayIn.Checked);

            //}

        }

        private void frm_InphieuCLS_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (_hisclsProperties != null)
            //{
            //    _hisclsProperties.IsInToanBoCls = chkInToanBoRamayIn.Checked;
            //    Utility.SaveHisCLSConfig(_hisclsProperties);
            //}
        }

        private void chkInToanBoRamayIn_CheckedChanged(object sender, EventArgs e)
        {
            //Utility.VisiableGridEx(grdList,"CHON",chkInToanBoRamayIn.Checked);
        }

        private void cmdInXacNhanHIV_Click(object sender, EventArgs e)
        {
            //if (objLuotkham != null)
            //{
            //    DataTable mDtReport =
            //        SPs.BcInPhieuHiv(id_phieu, objLuotkham.PatientCode, Utility.Int32Dbnull(objLuotkham.PatientId), 0)
            //            .GetDataSet()
            //            .Tables[0];
            //    if (mDtReport.Rows.Count <= 0)
            //    {
            //        Utility.ShowMsg("Không tìm thấy bản ghi","Thông báo",MessageBoxIcon.Warning);
            //        return;
            //    }

            //    VietBaIT.HISLink.Business.Reports.Implementation.InPhieuCLS.BC_InphieuHIV(mDtReport,dtNgayInPhieu.Value);
            //}

        }
    }
}