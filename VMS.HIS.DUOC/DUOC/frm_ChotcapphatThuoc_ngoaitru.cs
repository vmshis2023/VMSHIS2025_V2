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
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
using SortOrder = Janus.Windows.GridEX.SortOrder;
using Microsoft.VisualBasic;
using System.IO;
using TriState = Janus.Windows.GridEX.TriState;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_ChotcapphatThuoc_ngoaitru : Form
    {
        Int16 id_kho = -1;
        private int Distance = 488;
        private bool b_Hasloaded = false;
        private string FileName = string.Format("{0}/{1}", Application.StartupPath, string.Format("SplitterDistancefrm_PhieuXuatBN.txt"));
        private DataTable m_dtDataDonThuoc = new DataTable();
        private DataTable m_dtDataPresDetail = new DataTable();
        string kieuthuoc_vt = "THUOC";
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        public frm_ChotcapphatThuoc_ngoaitru(string kieuthuoc_vt)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.kieuthuoc_vt = kieuthuoc_vt;
            
            dtFromdate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;
           
            dtNgayPhatThuoc.Value = globalVariables.SysDate;
            cmdConfig.Visible = globalVariables.IsAdmin;
            InitEvents();
            
        }
        void InitEvents()
        {
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            this.chkDisplayPresdetail.CheckedChanged += new System.EventHandler(this.chkDisplayPresdetail_CheckedChanged);
            this.cmdPhatThuoc.Click += new System.EventHandler(this.cmdPhatThuoc_Click);
            this.Load += new System.EventHandler(this.frm_ChotcapphatThuoc_ngoaitru_Load);
            grdPres.SelectionChanged += new EventHandler(grdPres_SelectionChanged);
            mnuChotCurrent.Click += new EventHandler(mnuChotCurrent_Click);
            mnuChotAll.Click += new EventHandler(mnuChotAll_Click);
            mnuHuychotCurrent.Click += new EventHandler(mnuHuychotCurrent_Click);
            mnuHuychotAll.Click += new EventHandler(mnuHuychotAll_Click);
            optTatca.CheckedChanged += new EventHandler(optTatca_CheckedChanged);
            optChuachot.CheckedChanged += new EventHandler(optTatca_CheckedChanged);
            optDachot.CheckedChanged += new EventHandler(optTatca_CheckedChanged);
        }
        void optTatca_CheckedChanged(object sender, EventArgs e)
        {
            //if (PropertyLib._HisDuocProperties.TimkiemkhiChontrangthai)
                cmdSearch_Click(cmdSearch, e);
        }
        void mnuHuychotAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPres)) return;
                if (!Utility.Coquyen("thuoc_huychotthuoc_ngoaitru"))
                {
                    Utility.thongbaokhongcoquyen("thuoc_huychotthuoc_ngoaitru", "hủy chốt đơn thuốc");
                    return;
                }
                
                List<int> lstID_Donthuoc = (from p in grdPres.GetDataRows().AsEnumerable()
                                            where Utility.Int32Dbnull(((DataRowView)p.DataRow)["trangthai_chot"], -1) == 1
                                            select Utility.Int32Dbnull(((DataRowView)p.DataRow)["id_donthuoc"], -1)).ToList<int>();
                if (lstID_Donthuoc.Count <= 0) return;
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn hủy chốt số liệu cấp phát thuốc cho {0} đơn thuốc đã được chốt hay không?", lstID_Donthuoc.Count.ToString()), "Thông báo", true))
                {
                    frm_NhaplydoHuy _NhaplydoHuy = new frm_NhaplydoHuy();
                    _NhaplydoHuy.ShowDialog();
                    if (!_NhaplydoHuy.m_blnCancel)
                    {
                        if (new ChotThuoc().HUYCHOT_CAPPHAT(lstID_Donthuoc, id_kho, _NhaplydoHuy.ngay_thuchien, _NhaplydoHuy.ten) == ActionResult.Success)
                        {
                            foreach (GridEXRow _row in grdPres.GetDataRows())
                            {
                                if (lstID_Donthuoc.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                                {
                                    _row.BeginEdit();
                                    _row.Cells["trangthai_chot"].Value = 0;
                                    _row.Cells["ngay_chot"].Value = DBNull.Value;
                                    _row.EndEdit();
                                }
                            }
                            grdPres.Refetch();
                            Utility.SetMsg(uiStatusBar2.Panels["1"], "Hủy chốt số liệu thành công!", false);

                        }
                        else
                            Utility.SetMsg(uiStatusBar2.Panels["1"], "Hủy chốt số liệu không thành công!Liên hệ VMS để được trợ giúp", true);
                        ModifyMenuItems();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void ModifyMenuItems()
        {
            mnuHuychotAll.Enabled = m_dtDataDonThuoc.Select("trangthai_chot=1").Length > 0 ;//&& Utility.Coquyen("thuoc_huychotthuoc_ngoaitru");// PropertyLib._HisDuocProperties.ChoPhepHuyChotThuoc;
            mnuChotAll.Enabled = m_dtDataDonThuoc.Select("trangthai_chot=0").Length > 0;
            mnuHuychotCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("trangthai_chot"), "-1") == "1";// && Utility.Coquyen("thuoc_huychotthuoc_ngoaitru");//PropertyLib._HisDuocProperties.ChoPhepHuyChotThuoc;
            mnuChotCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("trangthai_chot"), "-1") == "0";
            cmdPhatThuoc.Enabled = Utility.isValidGrid(grdPres);
        }
        void mnuHuychotCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPres)) return;
                if (!Utility.Coquyen("thuoc_huychotthuoc_ngoaitru"))
                {
                    Utility.thongbaokhongcoquyen("thuoc_huychotthuoc_ngoaitru", "hủy chốt đơn thuốc");
                    return;
                }
                List<int> lstID_Donthuoc = new List<int>() { Utility.Int32Dbnull(grdPres.GetValue("id_donthuoc"), -1) };
                if (Utility.AcceptQuestion("Bạn có chắc chắn hủy chốt số liệu cấp phát thuốc cho đơn thuốc đang chọn hay không?", "Thông báo", true))
                {
                     frm_NhaplydoHuy _NhaplydoHuy = new frm_NhaplydoHuy();
                     _NhaplydoHuy.ShowDialog();
                     if (!_NhaplydoHuy.m_blnCancel)
                     {
                         if (new ChotThuoc().HUYCHOT_CAPPHAT(lstID_Donthuoc, id_kho, _NhaplydoHuy.ngay_thuchien, _NhaplydoHuy.ten) == ActionResult.Success)
                         {
                             grdPres.CurrentRow.BeginEdit();
                             grdPres.CurrentRow.Cells["trangthai_chot"].Value = 0;
                             grdPres.CurrentRow.Cells["ngay_chot"].Value = DBNull.Value;
                             grdPres.CurrentRow.EndEdit();
                             grdPres.Refetch();
                             Utility.SetMsg(uiStatusBar2.Panels["1"], "Hủy chốt số liệu thành công!", false);
                         }
                         else
                             Utility.SetMsg(uiStatusBar2.Panels["1"], "Hủy chốt số liệu không thành công!Liên hệ VMS để được trợ giúp", true);



                         ModifyMenuItems();
                     }
                }
            }
            catch (Exception ex)
            {
                
            }
           

        }

        void mnuChotAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPres)) return;
                if (!Utility.Coquyen("thuoc_chotthuoc_ngoaitru"))
                {
                    Utility.thongbaokhongcoquyen("thuoc_chotthuoc_ngoaitru",  "chốt đơn thuốc");
                    return;
                }
                List<int> lstID_Donthuoc = (from p in grdPres.GetDataRows().AsEnumerable()
                                            where Utility.Int32Dbnull(((DataRowView)p.DataRow)["trangthai_chot"], -1) == 0
                                            select Utility.Int32Dbnull(((DataRowView)p.DataRow)["id_donthuoc"], -1)).ToList<int>();
                if (lstID_Donthuoc.Count <= 0) return;
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn chốt số liệu cấp phát thuốc cho {0} đơn thuốc chưa được chốt hay không?", lstID_Donthuoc.Count.ToString()), "Thông báo", true))
                {
                    if (new ChotThuoc().CHOT_CAPPHAT(lstID_Donthuoc, id_kho,  dtNgayPhatThuoc.Value.Date) == ActionResult.Success)
                    {
                        foreach (GridEXRow _row in grdPres.GetDataRows())
                        {
                            if (lstID_Donthuoc.Contains(Utility.Int32Dbnull(_row.Cells["id_donthuoc"].Value, -1)))
                            {
                                _row.BeginEdit();
                                _row.Cells["trangthai_chot"].Value = 1;
                                _row.Cells["ngay_chot"].Value = dtNgayPhatThuoc.Value.Date;
                                _row.EndEdit();
                            }
                        }
                        grdPres.Refetch();
                        Utility.SetMsg(uiStatusBar2.Panels["1"], "Chốt số liệu thành công!", false);

                    }
                    else
                        Utility.SetMsg(uiStatusBar2.Panels["1"], "Chốt số liệu không thành công!Liên hệ VMS để được trợ giúp", true);
                    ModifyMenuItems();
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void mnuChotCurrent_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPres)) return;
            if (!Utility.Coquyen("thuoc_chotthuoc_ngoaitru"))
            {
                Utility.thongbaokhongcoquyen("thuoc_chotthuoc_ngoaitru", "chốt đơn thuốc");
                return;
            }
            List<int> lstID_Donthuoc = new List<int>() { Utility.Int32Dbnull(grdPres.GetValue("id_donthuoc"), -1) };
            if (Utility.AcceptQuestion("Bạn có chắc chắn chốt số liệu cấp phát thuốc cho đơn thuốc đang chọn hay không?", "Thông báo", true))
            {
                if (new ChotThuoc().CHOT_CAPPHAT(lstID_Donthuoc,id_kho, dtNgayPhatThuoc.Value.Date) == ActionResult.Success)
                {

                    grdPres.CurrentRow.BeginEdit();
                    grdPres.CurrentRow.Cells["trangthai_chot"].Value = 1;
                    grdPres.CurrentRow.Cells["ngay_chot"].Value = dtNgayPhatThuoc.Value.Date;
                    grdPres.CurrentRow.EndEdit();
                    grdPres.Refetch();
                    Utility.SetMsg(uiStatusBar2.Panels["1"], "Chốt số liệu thành công!", false);

                }
                else
                    Utility.SetMsg(uiStatusBar2.Panels["1"], "Chốt số liệu không thành công!Liên hệ VMS để được trợ giúp", true);
                ModifyMenuItems();
            }
        }
        private void CauHinh()
        {
            chkDisplayPresdetail.Checked = PropertyLib._HisDuocProperties.Hienthichitietkhichotthuoc;
            splitContainer1.Panel2Collapsed = !chkDisplayPresdetail.Checked;
            dtNgayPhatThuoc.Enabled = PropertyLib._HisDuocProperties.Chophepnhapngaychot;
            mnuHuychotAll.Enabled = m_dtDataDonThuoc.Select("trangthai_chot=1").Length > 0 ;//&& Utility.Coquyen("thuoc_huychotthuoc_ngoaitru");//PropertyLib._HisDuocProperties.ChoPhepHuyChotThuoc;
            mnuHuychotCurrent.Enabled = Utility.isValidGrid(grdPres) && Utility.sDbnull(grdPres.GetValue("trangthai_chot"), "-1") == "1" ;//&& Utility.Coquyen("thuoc_huychotthuoc_ngoaitru");//PropertyLib._HisDuocProperties.ChoPhepHuyChotThuoc;
        }

       
       
        private DataTable m_dtKhothuoc=new DataTable();
       
        /// <summary>
        /// hàm thực hiện việc trạng thái thông tin của đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TimKiemThongTinDonThuoc();
        }

        private void TimKiemThongTinDonThuoc()
        {
            try
            {
                int Status = -1;
                if (optDachot.Checked) Status = 1;
                if (optChuachot.Checked) Status = 0;
                int NoiTru = 0;
                string MaKho = "-1";
                cmdPhatThuoc.Enabled = !optTatca.Checked;
                cmdPhatThuoc.Text = optDachot.Checked && Utility.Coquyen("thuoc_huychotthuoc_ngoaitru") ? "Hủy chốt" : "Chốt thuốc";
                cmdPhatThuoc.Tag = optDachot.Checked && Utility.Coquyen("thuoc_huychotthuoc_ngoaitru") ? "HUYCHOT" : "CHOT";
                var querykho = (from kho in m_dtKhothuoc.AsEnumerable().Cast<DataRow>()
                                let y = Utility.sDbnull(kho[TDmucKho.Columns.IdKho])
                                select y).ToArray();
                if (Utility.sDbnull(cboKho.SelectedValue, "-1") != "-1")
                {
                    MaKho = Utility.sDbnull(cboKho.SelectedValue, "-1");
                }
                id_kho = Utility.Int16Dbnull(cboKho.SelectedValue, "-1");
                m_dtDataDonThuoc =
                    SPs.ThuocTimkiemdonthuocChotcapphat(chkByDate.Checked? dtFromdate.Value.ToString("dd/MM/yyyy"): "01/01/1900",
                                                           chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900", NoiTru, cboObjectType.SelectedValue.ToString(), MaKho, Status,kieuthuoc_vt).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_BasicNofilter(grdPres, m_dtDataDonThuoc, true, true,Status==-1?"1=1":(Status==1 ? "NGAY_CHOT is not null" : "NGAY_CHOT is  null"), "ten_benhnhan asc");

                mnuHuychotAll.Enabled = m_dtDataDonThuoc.Select("trangthai_chot=1").Length > 0 ;//&& Utility.Coquyen("thuoc_huychotthuoc_ngoaitru");//PropertyLib._HisDuocProperties.ChoPhepHuyChotThuoc;
                mnuChotAll.Enabled = m_dtDataDonThuoc.Select("trangthai_chot=0").Length > 0 ; 

                if (grdPres.RowCount > 0)
                {
                    grdPres.MoveFirst();
                    grdPres_SelectionChanged(grdPres, new EventArgs());
                }
                if (grdPres.RowCount <= 0) m_dtDataPresDetail.Rows.Clear();
                ModifyMenuItems();
            }
            catch
            {
            }
            finally
            {
                //cmdPhatThuoc.Visible = optChuachot.Checked || Utility.Coquyen("thuoc_huychotthuoc_ngoaitru");//PropertyLib._HisDuocProperties.ChoPhepHuyChotThuoc; 
            }
        }
        
        /// <summary>
        /// hàm thực hiện việc dichuyeen thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!splitContainer1.Panel2Collapsed)
                {
                    if (grdPres.CurrentRow != null && grdPres.CurrentRow.RowType == RowType.Record)
                    {
                        Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(TPhieuXuatthuocBenhnhan.Columns.IdDonthuoc));
                        GetDataPresDetail();
                    }
                    else
                    {
                        grdPresDetail.DataSource = null;
                    }
                }
                ModifyMenuItems();
            }
            catch
            { }
            finally
            {
                ModifyMenuItems();
            }
        }
        
        private int Pres_ID=-1;
        private void GetDataPresDetail()
        {
            int id_phieu = Utility.Int32Dbnull(grdPres.GetValue(TPhieuXuatthuocBenhnhan.Columns.IdPhieu));
            m_dtDataPresDetail = SPs.ThuocLaychitietcapphatNgoaitru(id_phieu).GetDataSet().Tables[0];
            
            m_dtDataPresDetail.AcceptChanges();
            Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtDataPresDetail, true , true, "1=1",
                                                 "ten_thuoc");
        }
      
        /// <summary>
        /// hàm thực hiện việc cho phím tắt thực hiện tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChotcapphatThuoc_ngoaitru_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)this.Close();
            if(e.KeyCode==Keys.F3)cmdSearch.PerformClick();
            if(e.KeyCode==Keys.F5)
            {
                grdPres_SelectionChanged(grdPres,new EventArgs());
            }
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin theo đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPhatThuoc_Click(object sender, EventArgs e)
        {
            
            string thaotac = optDachot.Checked ? " hủy chốt" : " chốt thuốc";
            if (grdPres.GetCheckedRows().Length<=0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một đơn thuốc để thực hiện thao tác " + thaotac + "!");
                return;
            }
            List<int> lstID_Donthuoc = (from p in grdPres.GetCheckedRows().AsEnumerable()
                                        select Utility.Int32Dbnull(((DataRowView)p.DataRow)["id_donthuoc"], -1)).ToList<int>();
            try
            {
                Utility.SetMsg(uiStatusBar2.Panels["1"], "", false);
                if (Utility.AcceptQuestion("Bạn có chắc chắn " + thaotac + " số liệu cấp phát thuốc cho các đơn thuốc được chọn hay không?", "Thông báo", true))
                {
                    if (cmdPhatThuoc.Tag.ToString() == "CHOT")
                    {
                        if (new ChotThuoc().CHOT_CAPPHAT(lstID_Donthuoc, id_kho, dtNgayPhatThuoc.Value.Date) == ActionResult.Success)
                        {
                            foreach (GridEXRow dr in grdPres.GetCheckedRows())
                            {
                                dr.BeginEdit();
                                dr.Cells["trangthai_chot"].Value = 1;
                                dr.Cells["ngay_chot"].Value = dtNgayPhatThuoc.Value.Date;
                                dr.EndEdit();
                            }
                            grdPres.Refetch();
                            Utility.SetMsg(uiStatusBar2.Panels["1"], "Chốt số liệu thành công!", false);
                        }
                        else
                            Utility.SetMsg(uiStatusBar2.Panels["1"], "Chốt số liệu không thành công!Liên hệ VMS để được trợ giúp", true);
                    }
                    else
                    {
                        frm_NhaplydoHuy _NhaplydoHuy = new frm_NhaplydoHuy();
                        _NhaplydoHuy.ShowDialog();
                        if (!_NhaplydoHuy.m_blnCancel)
                        {
                            if (new ChotThuoc().HUYCHOT_CAPPHAT(lstID_Donthuoc, id_kho, _NhaplydoHuy.ngay_thuchien, _NhaplydoHuy.ten) == ActionResult.Success)
                            {
                                foreach (GridEXRow dr in grdPres.GetCheckedRows())
                                {
                                    dr.BeginEdit();
                                    dr.Cells["trangthai_chot"].Value = 0;
                                    dr.Cells["ngay_chot"].Value = DBNull.Value;
                                    dr.EndEdit();
                                }
                                m_dtDataDonThuoc.AcceptChanges();
                                Utility.SetMsg(uiStatusBar2.Panels["1"], "Hủy chốt số liệu thành công!", false);
                            }
                            else
                                Utility.SetMsg(uiStatusBar2.Panels["1"], "Hủy chốt số liệu không thành công!Liên hệ VMS để được trợ giúp", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.SetMsg(uiStatusBar2.Panels["1"], "Lỗi khi chốt số liệu:\n" + ex.Message, true);
            }
            finally
            {
                ModifyMenuItems();
            }
        }

        private void frm_ChotcapphatThuoc_ngoaitru_Load(object sender, EventArgs e)
        {

            DataBinding.BindDataCombobox(cboObjectType, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                       DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "---Chọn---", true);
            if(kieuthuoc_vt=="THUOC")
                m_dtKhothuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1,"TATCA,NGOAITRU", "THUOC,THUOCVT", "CHANLE,LE", 0, 100, 1); // CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NGOAITRU();
            else
                m_dtKhothuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1,"TATCA,NGOAITRU", "VT,THUOCVT", "CHANLE,LE", 0, 100, 1); // CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "NGOAITRU" });
            DataBinding.BindData(cboKho, m_dtKhothuoc,
                                     TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
            TimKiemThongTinDonThuoc();
            ModifyMenuItems();
            CauHinh();

        }

        private void chkDisplayPresdetail_CheckedChanged(object sender, EventArgs e)
        {
           
            splitContainer1.Panel2Collapsed = !chkDisplayPresdetail.Checked;
        }

        
      

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._HisDuocProperties);
                
                frm.ShowDialog();
                CauHinh();

                //grdPresDetail.SaveLayoutFile();
            }
            catch (Exception exception)
            {

            }
        }
       
       

    }
}
