using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.C1Excel;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;
using System.Collections.Generic;
using System.Linq;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_qhe_doituong_dichvuCls : Form
    {
        private readonly DataTable m_dtObjectRelationService = new DataTable();
        private readonly DataTable m_dtReportObjectType = new DataTable();
        private bool CLS_GIATHEO_KHOAKCB;
        private int ObjectType_ID = -1;
        private int ServiceDetail_ID = -1;
        private int ServiceObject_Type_ID = -1;
        private int Service_ID = -1;
        private string _rowFilter = "1=1";
        private ActionResult actionResult = ActionResult.Error;
        private DataSet ds = new DataSet();
        private DataTable dt_KhoaThucHien;
        private Query m_Query = QheDoituongDichvucl.CreateQuery();
        private bool m_blnLoaded;
        private DataTable m_dtDataDetailService = new DataTable();
        private DataTable m_dtObjectType = new DataTable();
        private DataTable m_dtGiaDichvu = new DataTable();
        private DataTable m_dtServiceList = new DataTable();
        private DataTable m_dtServiceTypeList = new DataTable();
        private string rowFilters = "1=1";
        private DataTable v_DataPrint = new DataTable();
        private int v_ObjectType_ID = -1;
        private int v_ObjectType_Id = -1;

        /// <summary>
        ///     HAM THUC HIEN DICH VU, QUAN HE DOI TUONG -DICH VU
        /// </summary>
        private int v_ObjectType_Service_ID = -1;

        private int v_ServiceDetail_ID = -1;
        string SplitterPath = "";
        public frm_qhe_doituong_dichvuCls()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            if (PropertyLib._QheGiaCLSProperties == null) PropertyLib._QheGiaCLSProperties = PropertyLib.GetQheGiaCLSProperties();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            InitEvents();
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            KeyPreview = true;
        }

        private void InitEvents()
        {
            cboService.SelectedIndexChanged += cboService_SelectedIndexChanged;
            cmdAdd.Click += cmdAdd_Click;
            cmdDelete.Click += cmdDelete_Click;
            cmdSaveObjectAll.Click += cmdSaveObjectAll_Click;
            cmdDetailDeleteAll.Click += cmdDetailDeleteAll_Click;
            cmdThemMoi.Click += cmdThemMoi_Click;
            cmdCapNhap.Click += cmdCapNhap_Click;
            cmdPrint.Click += cmdPrint_Click;
            cmdPrintRelationObject.Click += cmdPrintRelationObject_Click;
            cmdExportExcel.Click += cmdExportExcel_Click;
            cmdClose.Click += cmdClose_Click;
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            cmdUpdate.Click += cmdUpdate_Click;
            cboKhoaTH.SelectedIndexChanged += cboKhoaTH_SelectedIndexChanged;
            Load += frm_qhe_doituong_dichvuCls_Load;
            KeyDown += frm_qhe_doituong_dichvuCls_KeyDown;
            grdList.ApplyingFilter += grdList_ApplyingFilter;
            grdList.SelectionChanged += grdList_SelectionChanged;
            cmdCauhinhgia.Click += cmdCauhinhgia_Click;
            optQhe_tatca.CheckedChanged += optQhe_tatca_CheckedChanged;
            optCoQhe.CheckedChanged += optQhe_tatca_CheckedChanged;
            optKhongQhe.CheckedChanged += optQhe_tatca_CheckedChanged;
            optTatca.CheckedChanged += optQhe_tatca_CheckedChanged;
            optHieuluc.CheckedChanged += optQhe_tatca_CheckedChanged;
            optHethieuluc.CheckedChanged += optQhe_tatca_CheckedChanged;
            cmdDel.Click+=cmdDelete_Click;
            cmdDeleteALL.Click += cmdDeleteALL_Click;
            mnuTaoQheGiaDvuCLS_DoituongKCB.Click += mnuTaoQheGiaDvuCLS_DoituongKCB_Click;
            cboBogia.SelectedIndexChanged += cboBogia_SelectedIndexChanged;
            grdQhe.UpdatingCell += grdQhe_UpdatingCell;
            this.Shown += frm_qhe_doituong_dichvuCls_Shown;
            this.FormClosing += frm_qhe_doituong_dichvuCls_FormClosing;
            grdQhe.CellValueChanged += grdQhe_CellValueChanged;
            grdQhe.CellUpdated += grdQhe_CellUpdated;
        }

        void grdQhe_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                grdQhe.UpdateData();
                DataRow BHYTRow = GetGiaBHYT();
                if (BHYTRow != null)
                {
                    grdList.CurrentRow.BeginEdit();
                    grdList.CurrentRow.Cells["gia_bhyt"].Value = Utility.DecimaltoDbnull(BHYTRow["don_gia"]);
                    grdList.CurrentRow.Cells["gia_dichvu"].Value = getGiaDV();
                    grdList.CurrentRow.Cells["phuthu_dungtuyen"].Value = Utility.DecimaltoDbnull(BHYTRow["phuthu_dungtuyen"]);
                    grdList.CurrentRow.Cells["phuthu_traituyen"].Value = Utility.DecimaltoDbnull(BHYTRow["phuthu_traituyen"]);
                    grdList.CurrentRow.Cells["Tyle_tt"].Value = Utility.DecimaltoDbnull(BHYTRow["Tyle_tt"]);
                    grdList.CurrentRow.EndEdit();
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void grdQhe_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
           
        }

        void frm_qhe_doituong_dichvuCls_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() {(splitContainer1.Width- splitContainer1.Panel2.Width).ToString() });
        }
        void Try2Splitter()
        {
            try
            {
                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath) select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null )
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void frm_qhe_doituong_dichvuCls_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }
        decimal getGiaDV()
        {
            try
            {
                DataRow[] arrDr = m_dtGiaDichvu.Select("ma_doituong_kcb='DV'");
                if (arrDr.Length > 0)
                    return Utility.DecimaltoDbnull(arrDr[0]["don_gia"]);
                return -1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        DataRow GetGiaBHYT()
        {
            try
            {
                DataRow[] arrDr = m_dtGiaDichvu.Select("ma_doituong_kcb='BHYT'");
                if (arrDr.Length > 0)
                    return arrDr[0];
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
       
        void grdQhe_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {

                string colName = e.Column.Key;
                string ma_doituong_kcb = Utility.sDbnull(grdQhe.CurrentRow.Cells["ma_doituong_kcb"].Value, 0);
                decimal don_gia = Utility.DecimaltoDbnull(e.Value, 0);
                decimal GiaBHYT = don_gia;
                decimal GiaDV = getGiaDV();
                decimal GiaPhuThu = Utility.DecimaltoDbnull(grdQhe.CurrentRow.Cells["Phuthu_Traituyen"].Value, 0);
                if (e.Column.Key == "don_gia")
                {
                    colName = "don_gia";
                    GiaBHYT = don_gia;
                    if (ma_doituong_kcb == "BHYT" && GiaDV > 0 && PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaPTTT)
                        GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                }
                else if (e.Column.Key == "phuthu_traituyen")
                {
                    colName = "phuthu_traituyen";
                    GiaPhuThu = don_gia;
                }
                else if (e.Column.Key == "phuthu_dungtuyen")
                {
                    colName = "phuthu_dungtuyen";
                }
                else if (e.Column.Key == "tyle_tt")
                {
                    colName = "tyle_tt";
                }
                int num = -1;
                long id_quanhe = Utility.Int64Dbnull(grdQhe.GetValue("id_quanhe"), -1);
                if (id_quanhe > 0)//Update
                {
                    if (colName == "don_gia")
                    {
                        if (don_gia > 0)
                            num = new Update(QheDoituongDichvucl.Schema)
                            .Set(colName).EqualTo(don_gia)
                            .Set(QheDoituongDichvucl.Columns.PhuthuTraituyen).EqualTo(GiaPhuThu)
                            .Where(QheDoituongDichvucl.Columns.IdQuanhe).IsEqualTo(id_quanhe).Execute();
                        else
                        {
                            //Không cần xử lý việc xóa quan hệ khi nhập giá =0
                        }


                    }
                    else
                        num = new Update(QheDoituongDichvucl.Schema)
                      .Set(colName).EqualTo(don_gia)
                      .Where(QheDoituongDichvucl.Columns.IdQuanhe).IsEqualTo(id_quanhe).Execute();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật giá {0}={1} của dịch vụ {2},IdChitietdichvu={3} ", e.Column.Key, e.Value, grdList.CurrentRow.Cells["ten_chitietdichvu"].Value, grdList.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }
                else
                {
                    //Không nhảy vào nhánh này
                }

                grdQhe.CurrentRow.BeginEdit();
                grdQhe.CurrentRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value = GiaPhuThu;
                grdQhe.CurrentRow.EndEdit();
              
               

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            } 
        }

        void cboBogia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            DmucBogiadichvu objBogia = DmucBogiadichvu.FetchByID(Utility.Int32Dbnull(cboBogia.SelectedValue, -1));
            lblFrom.Visible = lblTo.Visible = dtpFrom.Visible = dtpTo.Visible = objBogia != null;
            if (objBogia != null)
            {
                dtpFrom.Value = objBogia.NgayBatdau;
                dtpTo.Value = objBogia.NgayKetthuc;
            }
            grdList_SelectionChanged(grdList, e);
        }

        void mnuTaoQheGiaDvuCLS_DoituongKCB_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.Log(this.Name, globalVariables.UserName, "Tạo giá quan hệ đối tượng cận lâm sàng dựa theo đối tượng tham chiếu", newaction.Reset, this.GetType().Assembly.ManifestModule.Name);
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                if (Utility.Int32Dbnull(cboBogia.SelectedValue, -1) <= 0)
                {
                    if (Utility.AcceptQuestion("Nếu bạn muốn tạo giá cho đối tượng BHYT thì cần chọn bộ giá BHYT trước khi dùng chức năng này. Nhấn Yes để quay lại chọn bộ giá BHYT, nhấn No để tiếp tục", "Xác nhận", true))
                        return;
                }
                Utility.WaitNow(this);
                //if (grdList.GetCheckedRows().Length <= 0)
                //{
                //    Utility.ShowMsg("Bạn cần chọn ít nhất 1 dịch vụ cận lâm sàng để thực hiện chức năng này");
                //    return;
                //}
                frm_ChonDoituongKCB_All frm = new frm_ChonDoituongKCB_All();
                frm._enObjectType = enObjectType.DichvuCLS;
                frm.m_dtQheDoituong_CLS = m_dtGiaDichvu.Clone();
                frm.Original_Price =
                    Utility.DecimaltoDbnull(grdList.CurrentRow.Cells["gia_dichvu"].Value, 0);
                frm.MaKhoaTHIEN = Utility.sDbnull(cboKhoaTH.SelectedValue, "");
                frm.DetailService =
                    Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, 0);
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                    AutoCreate(frm.m_dtQheDoituong_CLS, frm.id_doituongkcb_thamchieu,false);
               
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
                ModifyCommand1();
                ModifyCommand();
            }
           
        }
        void AutoCreate(DataTable dtDoituongKcb, Int16 id_doituongKcb_thamchieu,bool isreset)
        {
            try
            {
                foreach (DataRow dr in dtDoituongKcb.Rows)
                {
                    byte id_loaidoituongkcb = Utility.ByteDbnull(dr[QheDoituongDichvucl.Columns.IdLoaidoituongKcb], -1);
                    Int16 id_doituongkcb = Utility.Int16Dbnull(dr[QheDoituongDichvucl.Columns.IdDoituongKcb], -1);
                    int id_bogia = -1;
                    DateTime? ngay_batdau=null;
                    DateTime? ngay_ketthuc = null;
                    if (THU_VIEN_CHUNG.IsBaoHiem(id_loaidoituongkcb))
                    {
                        DmucBogiadichvu objBogia = DmucBogiadichvu.FetchByID(Utility.Int32Dbnull(cboBogia.SelectedValue, -1));
                        if (objBogia != null)
                        {
                            id_bogia = objBogia.IdBogia;
                            ngay_batdau = objBogia.NgayBatdau;
                            ngay_ketthuc = objBogia.NgayKetthuc;
                        }
                        else
                        {
                            Utility.ShowMsg("Đối tượng BHYT cần chọn bộ giá trước khi sao chép giá dịch vụ từ đối tượng tham chiếu");
                            continue;
                        }
                    }
                    SPs.QheDichvuclsDoituongkcbAutoCreate(id_loaidoituongkcb, id_doituongkcb, id_doituongKcb_thamchieu, globalVariables.UserName, DateTime.Now, Utility.Bool2byte(isreset), id_bogia,ngay_batdau,ngay_ketthuc).Execute();
                }
                Utility.ShowMsg("Đã tạo dữ liệu thành công. Nhấn OK để kết thúc");
                grdList_SelectionChanged(grdList, new EventArgs());

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        /// <summary>
        /// HAM THUC HIEN XOA THONG TIN DANG CHON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private int v_ServiceDetail_Id = -1;
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl != null && this.ActiveControl.Name == grdList.Name)
                {
                    if (!Utility.isValidGrid(grdList))
                    {
                        Utility.ShowMsg("Bạn cần chọn một dịch vụ trên lưới trước khi xóa");
                        return;
                    }
                    v_ServiceDetail_Id = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    KcbChidinhclsChitiet item = new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdChitietdichvu).IsEqualTo(v_ServiceDetail_Id).ExecuteSingle<KcbChidinhclsChitiet>();
                    if (item != null)
                    {
                        Utility.ShowMsg("Dịch vụ bạn chọn xóa đã từng được Bác sĩ dùng để chỉ định cho Bệnh nhân nên bạn không thể xóa");
                        return;
                    }
                    if (Utility.AcceptQuestion("Bạn có muốn xoá dịch vụ đang chọn không", "Thông báo", true))
                    {
                        SPs.DmucXoadanhmucDichvuclsChitiet(v_ServiceDetail_Id).Execute();
                        m_dtDataDetailService.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_Id)[0].Delete();
                        m_dtDataDetailService.AcceptChanges();

                    }

                }
                
            }
            catch (Exception)
            {

                ModifyCommand();
            }

        }
        GridEX _currentGRd = null;
        /// <summary>
        /// HAM THUC HIEN XOA NHIEU LUA CHON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeleteALL_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentGRd == null) return;
                SqlQuery q;
                bool b_FlagService = false;
                if (!Utility.isValidCheckedGrid(_currentGRd))
                {
                    Utility.ShowMsg("Bạn cần check chọn ít nhất một dịch vụ trên lưới trước khi xóa");
                    return;
                }
                if (_currentGRd.GetCheckedRows().Length <= 0)
                {
                    Utility.ShowMsg("Bạn phải chọn một dịch vụ thực hiện xoá", "Thông báo");
                    _currentGRd.Focus();
                    return;
                }
                string lsterr = "";
                if (_currentGRd.CurrentRow != null)
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xoá các dịch vụ đang chọn không", "Thông báo", true))
                    {
                        foreach (Janus.Windows.GridEX.GridEXRow gridExRow in _currentGRd.GetCheckedRows())
                        {
                            int _IdChitietdichvu = Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                            v_ServiceDetail_Id = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                            KcbChidinhclsChitiet item = new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdChitietdichvu).IsEqualTo(v_ServiceDetail_Id).ExecuteSingle<KcbChidinhclsChitiet>();
                            if (item != null)
                            {
                                lsterr = lsterr + Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "") + ";";
                            }
                            else
                            {
                                SPs.DmucXoadanhmucDichvuclsChitiet(_IdChitietdichvu).Execute();
                                gridExRow.Delete();
                                _currentGRd.UpdateData();
                                _currentGRd.Refetch();
                                m_dtDataDetailService.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + _IdChitietdichvu)[0].Delete();
                                m_dtDataDetailService.AcceptChanges();
                            }
                        }
                        if (Utility.DoTrim(lsterr) != "")
                        {
                            Utility.ShowMsg("Một số dịch vụ chi tiết sau đã có chi tiết nên bạn không thể xóa\n" + lsterr);
                        }
                    }
                    m_dtDataDetailService.AcceptChanges();
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                ModifyCommand();
            }

        }
       


        private void optQhe_tatca_CheckedChanged(object sender, EventArgs e)
        {
            cboService_SelectedIndexChanged(cboService, e);
        }

        private void cmdCauhinhgia_Click(object sender, EventArgs e)
        {
            var _Properties = new frm_Properties(PropertyLib._QheGiaCLSProperties);
            _Properties.ShowDialog();
        }

        private void cboService_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                byte trang_thai=Utility.ByteDbnull( optTatca.Checked?100:(optHieuluc.Checked?1:0));
                byte coqhe = Utility.ByteDbnull(optQhe_tatca.Checked ? 100 : (optCoQhe.Checked ? 1 : 0));
                m_dtDataDetailService = SPs.DichvuClsGetData(Utility.Int32Dbnull(cboService.SelectedValue, -1), trang_thai, coqhe).GetDataSet().Tables[0];
                
                //SqlQuery _SqlQuery = new Select().From(VDmucDichvuclsChitiet.Schema);
                //if (Utility.Int32Dbnull(cboService.SelectedValue, -1) > -1)
                //    _SqlQuery.Where(VDmucDichvuclsChitiet.Columns.IdDichvu)
                //        .IsEqualTo(Utility.Int32Dbnull(cboService.SelectedValue, -1));
                //if (_SqlQuery.HasWhere)
                //{
                //    if (!optTatca.Checked)
                //        _SqlQuery.And(VDmucDichvuclsChitiet.Columns.TrangThai).IsEqualTo(optHieuluc.Checked ? 1 : 0);
                //    if (!optQhe_tatca.Checked)
                //        _SqlQuery.And(VDmucDichvuclsChitiet.Columns.CoQhe).IsEqualTo(optCoQhe.Checked ? 1 : 0);
                //}
                //else
                //{
                //    if (!optTatca.Checked)
                //        _SqlQuery.Where(VDmucDichvuclsChitiet.Columns.TrangThai).IsEqualTo(optHieuluc.Checked ? 1 : 0);
                //    if (!optQhe_tatca.Checked)
                //        _SqlQuery.And(VDmucDichvuclsChitiet.Columns.CoQhe).IsEqualTo(optCoQhe.Checked ? 1 : 0);
                //}
                //m_dtDataDetailService =
                //    _SqlQuery.OrderAsc(VDmucDichvuclsChitiet.Columns.SttHthiLoaidvu,
                //        VDmucDichvuclsChitiet.Columns.SttHthiDichvu, VDmucDichvuclsChitiet.Columns.SttHthi)
                //        .ExecuteDataSet()
                //        .Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, m_dtDataDetailService, true, true, "1=1",
                    "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ex.Message);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ModifyCommand1()
        {
            cmdDetailDeleteAll.Enabled = grdQhe.RowCount > 0;
            cmdDelete.Enabled = grdQhe.RowCount > 0;
            cmdSaveObjectAll.Enabled = grdQhe.RowCount > 0;
        }


        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
                {
                    Service_ID = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                    ServiceDetail_ID = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    //_rowFilter = DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + ServiceDetail_ID;
                    //m_dtGiaDichvu = new Select().From(VQheDoituongDichvucl.Schema)
                    //    .Where(VQheDoituongDichvucl.Columns.IdChitietdichvu).IsEqualTo(ServiceDetail_ID)
                    //    .And(VQheDoituongDichvucl.Columns.IdBogia).IsEqualTo(Utility.Int32Dbnull(cboBogia.SelectedValue, -1))
                    //    .OrderAsc(VQheDoituongDichvucl.Columns.SttHthi).ExecuteDataSet().Tables[0];
                    m_dtGiaDichvu = SPs.QheGiadoituongCls(ServiceDetail_ID, Utility.Int32Dbnull(cboBogia.SelectedValue, -1)).GetDataSet().Tables[0];
                    if (!m_dtGiaDichvu.Columns.Contains("IsNew"))
                        m_dtGiaDichvu.Columns.Add("IsNew", typeof (int));
                    if (!m_dtGiaDichvu.Columns.Contains("CHON"))
                        m_dtGiaDichvu.Columns.Add("CHON", typeof (int));

                    rowFilters = QheDoituongDichvucl.Columns.MaKhoaThuchien + " ='" +
                                 Utility.sDbnull(cboKhoaTH.SelectedValue, "") + "'";
                    Utility.SetDataSourceForDataGridEx(grdQhe, m_dtGiaDichvu, true, true, "1=1", "");
                }
                ModifyCommand();
                ModifyCommand1();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        /// <summary>
        ///     hàm thực hiện xóa nhiều bản ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDetailDeleteAll_Click(object sender, EventArgs e)
        {
            GridEXRow[] ArrCheckList = grdQhe.GetCheckedRows();
            if (ArrCheckList.Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa", "Thông báo");
                grdQhe.Focus();
                return;
            }
            string strLength = string.Format("Bạn có muốn xoá {0} bản ghi không", ArrCheckList.Length);
            if (Utility.AcceptQuestion(strLength, "Thông báo", true))
            {
                foreach (GridEXRow drv in ArrCheckList)
                {
                    new Delete().From(QheDoituongDichvucl.Schema)
                        .Where(QheDoituongDichvucl.Columns.MaDoituongKcb)
                        .IsEqualTo(Utility.sDbnull(drv.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value, "-1"))
                        .And(QheDoituongDichvucl.Columns.IdChitietdichvu).IsEqualTo(ServiceDetail_ID)
                        .And(QheDoituongDichvucl.Columns.MaKhoaThuchien)
                        .IsEqualTo(Utility.sDbnull(cboKhoaTH.SelectedValue, "ALL"))
                        .Execute();
                    drv.Delete();
                    grdQhe.UpdateData();
                    grdQhe.Refresh();
                }
                m_dtGiaDichvu.AcceptChanges();
                ModifyCommand1();
            }
            ModifyCommand();
        }

        

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (grdList.RowCount <= 0) return;
            var frm = new frm_ChonDoituongKCB();
            frm._enObjectType = enObjectType.DichvuCLS;
            frm.m_dtObjectDataSource = m_dtGiaDichvu;
            frm.Original_Price =
                Utility.DecimaltoDbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.DonGia].Value, 0);
            frm.MaKhoaTHIEN = Utility.sDbnull(cboKhoaTH.SelectedValue, "");
            frm.DetailService =
                Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, 0);
            frm.ShowDialog();
            if (!frm.m_blnCancel)
                cmdSaveObjectAll_Click(cmdSaveObjectAll, e);
            ModifyCommand1();
            ModifyCommand();
        }

        /// <summary>
        ///     HÀM THUWCJHIEENJ KHỞI TẠO CHI TIẾT ĐỐI TƯỢNG CHI TIÊT DỊCH VỤ
        /// </summary>
        /// <returns></returns>
        private QheDoituongDichvucl CreateDmucDoituongkcbDetailService()
        {
            var objectTypeService = new QheDoituongDichvucl();

            return objectTypeService;
        }

        private void ModifyCommand()
        {
            try
            {
                cmdDelete.Enabled = grdQhe.RowCount > 0;
                cmdDetailDeleteAll.Enabled = grdQhe.RowCount > 0;
                cmdUpdate.Enabled = cmdCapNhap.Enabled = grdList.RowCount > 0 && grdList.CurrentRow !=null && grdList.CurrentRow.RowType == RowType.Record;
                cmdThemMoi.Enabled = grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record;
                cmdSaveObjectAll.Enabled = grdQhe.RowCount > 0 && grdList.CurrentRow != null && grdQhe.CurrentRow.RowType == RowType.Record;
            }
            catch (Exception)
            {
            }
        }

        private void SetStatusMessage()
        {
            switch (actionResult)
            {
                case ActionResult.Success:

                    Utility.ShowMsg("Bạn thực hiện thành công", "Thông báo");
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập", "Thông báo");
                    break;
            }
        }

        private void InitData()
        {
            try
            {
                DataTable mDtDichvuCls = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                DataTable mDtDichvuClsNew = mDtDichvuCls.Clone();
                foreach (DataRow dr in mDtDichvuCls.Rows)
                {
                    if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucDichvucl.Columns.IdLoaidichvu]), "0"))
                    {
                        mDtDichvuClsNew.ImportRow(dr);
                    }
                }
                DataBinding.BindDataCombobox(cboService, mDtDichvuClsNew, DmucDichvucl.Columns.IdDichvu,
                    DmucDichvucl.Columns.TenDichvu, "---Chọn---", true);
                //  m_dtDataDetailService = SPs.DmucLaydanhmucDichvuclsChitietTheoID(Utility.Int16Dbnull(cboService.SelectedValue, -1)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, m_dtDataDetailService, true, true, "1=1",
                    "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
                dt_KhoaThucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                cboKhoaTH.DataSource = dt_KhoaThucHien;
                cboKhoaTH.ValueMember = DmucKhoaphong.Columns.MaKhoaphong;
                cboKhoaTH.DisplayMember = DmucKhoaphong.Columns.TenKhoaphong;
                cboKhoaTH.SelectedIndex = 0;
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin khoa");
            }
        }

        private void frm_qhe_doituong_dichvuCls_Load(object sender, EventArgs e)
        {
           
            cmdThemMoi.Visible =  Utility.Coquyen("danhmuc_quyen_themmoidichvu");
            cmdCapNhap.Visible = Utility.Coquyen("danhmuc_quyen_themmoidichvu");
            cmdDel.Visible = cmdDeleteALL.Visible = cmdCapNhap.Visible;
            LoadBogia();
            LoadData();
            cboBogia_SelectedIndexChanged(cboBogia, e);
        }

        private void LoadData()
        {
            try
            {
                CLS_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_GIATHEO_KHOAKCB", "0", true) == "1";
                cboKhoaTH.Enabled = CLS_GIATHEO_KHOAKCB;
                if (!CLS_GIATHEO_KHOAKCB) cboKhoaTH.Text = "Tất cả";
                InitData();
                ModifyCommand1();
                ModifyCommand();
                m_blnLoaded = true;
                cboService_SelectedIndexChanged(cboService, new EventArgs());
                if (grdList.GetDataRows().Length > 0) grdList.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        private void cmdSearchOnGrid_Click(object sender, EventArgs e)
        {
            grdList.FilterMode = FilterMode.Automatic;
        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            SaveAll();
        }


        private void SaveAll()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                decimal GiaDV = LayGiaDV();
                int ServiceDetailId = -1;
                decimal GiaPhuThu = 0;
                decimal GiaBHYT = LayGiaBHYT();
                string KTH = "ALL";
                decimal tyle_tt = 100;
                foreach (GridEXRow gridExRow in grdQhe.GetRows())
                {
                    ServiceDetailId =
                        Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1);
                    SqlQuery q =
                        new Select().From(QheDoituongDichvucl.Schema)
                            .Where(QheDoituongDichvucl.Columns.IdChitietdichvu)
                            .
                            IsEqualTo(
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1)).And(
                                        QheDoituongDichvucl.Columns.MaDoituongKcb)
                            .IsEqualTo(Utility.sDbnull(
                                gridExRow.Cells[QheDoituongDichvucl.Columns.MaDoituongKcb].Value, "-1"))
                            .And(QheDoituongDichvucl.Columns.MaKhoaThuchien)
                            .IsEqualTo(Utility.sDbnull(cboKhoaTH.SelectedValue, ""));
                    //.Or(QheDoituongDichvucl.Columns.MaDoituongKcb).IsEqualTo("BHYT");
                    GiaPhuThu =  Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value, 0);
                    tyle_tt = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.TyleTt].Value, 0);
                    int ObjectTypeType =
                        Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdLoaidoituongKcb].Value, 0);

                    //if (ObjectTypeType == 0) KTH = "ALL"; else 
                    KTH = Utility.sDbnull(cboKhoaTH.SelectedValue, "ALL");
                    //Nếu có lưu đối tượng BHYT và tồn tại giá DV thì tự động tính phụ thu trái tuyến cho đối tượng BHYT đó
                    if (gridExRow.Cells[QheDoituongDichvucl.Columns.IdLoaidoituongKcb].Value.ToString() == "0" && GiaDV > 0)
                    {
                        GiaBHYT = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
                        if (PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaPTTT)
                            GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                    }
                    //Nếu đối tượng BHYT có tồn tại thì update lại thông tin trong đó có giá phụ thu trái tuyến
                    if (q.GetRecordCount() > 0)
                    {
                        new Update(QheDoituongDichvucl.Schema)
                            .Set(QheDoituongDichvucl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(QheDoituongDichvucl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(QheDoituongDichvucl.Columns.IdDichvu).EqualTo(GetService_ID( Utility.Int32Dbnull( gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1)))
                            .Set(QheDoituongDichvucl.Columns.DonGia).EqualTo( Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.TyleTt).EqualTo(tyle_tt)
                            .Set(QheDoituongDichvucl.Columns.PhuthuDungtuyen).EqualTo( Utility.DecimaltoDbnull( gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.PhuthuTraituyen).EqualTo(GiaPhuThu)
                            .Set(QheDoituongDichvucl.Columns.MotaThem).EqualTo(Utility.sDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.MotaThem].Value, ""))
                            .Where(QheDoituongDichvucl.Columns.IdChitietdichvu).IsEqualTo(ServiceDetail_ID)
                            .And(QheDoituongDichvucl.Columns.IdLoaidoituongKcb).IsEqualTo(Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdLoaidoituongKcb].Value, -1))
                            .And(QheDoituongDichvucl.Columns.MaKhoaThuchien).IsEqualTo(KTH)
                            .Execute();
                    }
                    else
                    {
                        DmucDoituongkcbCollection objectTypeCollection =
                            new DmucDoituongkcbController().FetchByQuery(
                                DmucDoituongkcb.CreateQuery().AddWhere(DmucDoituongkcb.Columns.MaDoituongKcb,
                                    Comparison.Equals,
                                    Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value, "-1")));

                        foreach (DmucDoituongkcb lObjectType in objectTypeCollection)
                        {
                            var _newItems = new QheDoituongDichvucl();
                            _newItems.IdDoituongKcb = lObjectType.IdDoituongKcb;
                            _newItems.IdDichvu =
                                Utility.Int16Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdDichvu].Value, -1);
                            _newItems.IdChitietdichvu =
                                Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value,
                                    -1);
                            _newItems.TyleGiam = 0;
                            _newItems.KieuGiamgia = 0;
                            _newItems.DonGia =
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
                            _newItems.PhuthuDungtuyen =
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0);
                            _newItems.PhuthuTraituyen = GiaPhuThu;
                            _newItems.IdLoaidoituongKcb =
                                Utility.Int32Dbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.IdLoaidoituongKcb].Value, -1);
                            _newItems.MaDoituongKcb = lObjectType.MaDoituongKcb;

                            _newItems.NguoiTao = globalVariables.UserName;
                            _newItems.NgayTao = globalVariables.SysDate;
                            _newItems.MaKhoaThuchien = KTH;
                            _newItems.IsNew = true;
                            _newItems.Save();
                            gridExRow.BeginEdit();
                            gridExRow.Cells[QheDoituongDichvucl.Columns.IdQuanhe].Value = _newItems.IdQuanhe;
                            gridExRow.EndEdit();
                        }
                    }
                    gridExRow.BeginEdit();
                    gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value = GiaPhuThu;
                    gridExRow.EndEdit();
                    grdQhe.UpdateData();
                    //Tự động update giá dịch vụ cho tất cả các khoa là giống nhau và giống giá sửa cuối cùng
                    if (PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaDichVu)
                    {
                        SqlQuery sqlQuery = new Select().From(DmucDoituongkcb.Schema)
                            .Where(DmucDoituongkcb.Columns.IdLoaidoituongKcb).IsEqualTo(1)
                            .And(DmucDoituongkcb.Columns.MaDoituongKcb)
                            .IsEqualTo(Utility.sDbnull(
                                gridExRow.Cells[QheDoituongDichvucl.Columns.MaDoituongKcb].Value, "-1"));
                        var objectType = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                        if (objectType != null)
                        {
                            new Update(DmucDichvuclsChitiet.Schema)
                                .Set(DmucDichvuclsChitiet.Columns.DonGia)
                                .EqualTo(
                                    Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0))
                                .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu)
                                .IsEqualTo(
                                    Utility.Int32Dbnull(
                                        gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1))
                                .Execute();
                        }
                    }
                }
                //new Update(DmucDichvuclsChitiet.Schema).Set(DmucDichvuclsChitiet.Columns.DonGia).EqualTo(GiaDV)
                //   .Set(DmucDichvuclsChitiet.Columns.GiaBhyt).EqualTo(GiaBHYT)
                //   .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(Utility.Int32Dbnull(grdList.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1))
                //   .Execute();
                //Cập nhật giá BHYT cho các khoa thực hiện
                if (PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaBHYT)
                {
                    if (GiaBHYT >= 0)
                    {
                        var lstItems =
                            new Select().From(QheDoituongDichvucl.Schema).
                                Where(QheDoituongDichvucl.Columns.IdChitietdichvu).
                                IsEqualTo(ServiceDetailId)
                                .And(QheDoituongDichvucl.MaKhoaThuchienColumn)
                                .IsNotEqualTo(KTH)
                                .ExecuteAsCollection<QheDoituongDichvuclCollection>();
                        foreach (QheDoituongDichvucl item in lstItems)
                        {
                            int ObjectTypeType = item.IdLoaidoituongKcb.Value;
                            if (ObjectTypeType == 1)
                                GiaDV = item.DonGia.Value;
                        }
                        GiaPhuThu = 0;
                        foreach (QheDoituongDichvucl item in lstItems)
                        {
                            int ObjectTypeType = item.IdLoaidoituongKcb.Value;
                            if (ObjectTypeType.ToString() == "0" && GiaDV > 0)
                            {
                                GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                                Update _update =
                                    new Update(QheDoituongDichvucl.Schema).Set(QheDoituongDichvucl.DonGiaColumn)
                                        .EqualTo(GiaBHYT);
                                if (PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaPTTT)
                                    _update.Set(QheDoituongDichvucl.PhuthuTraituyenColumn).EqualTo(GiaPhuThu);
                                _update.Where(QheDoituongDichvucl.IdLoaidoituongKcbColumn)
                                    .IsEqualTo(0)
                                    .And(QheDoituongDichvucl.IdChitietdichvuColumn)
                                    .IsEqualTo(ServiceDetailId)
                                    .And(QheDoituongDichvucl.MaKhoaThuchienColumn).IsNotEqualTo(KTH)
                                    .Execute();
                            }
                        }
                    }
                }
                Utility.SetMsg(lblMsg, "Bạn thực hiện cập nhập giá thành công", false);
            }
            catch (Exception exception)
            {
                Utility.SetMsg(lblMsg, "Lỗi trong quá trình cập nhập thông tin", false);
            }
        }

        private int GetService_ID(int v_ServiceDetail_ID)
        {
            int v_Service_ID = -1;
            DataRow[] arrDr =
                globalVariables.gv_dtDmucDichvuClsChitiet.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                                 v_ServiceDetail_ID);
            if (arrDr.GetLength(0) > 0)
            {
                v_Service_ID = Utility.Int32Dbnull(arrDr[0][DmucDichvucl.Columns.IdDichvu], -1);
            }
            return v_Service_ID;
        }

        private short GetService_ID2(int v_ServiceDetail_ID)
        {
            short v_Service_ID = -1;
            DataRow[] arrDr =
                globalVariables.gv_dtDmucDichvuClsChitiet.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                                 v_ServiceDetail_ID);
            if (arrDr.GetLength(0) > 0)
            {
                v_Service_ID = Utility.Int16Dbnull(arrDr[0][DmucDichvucl.Columns.IdDichvu], -1);
            }
            return v_Service_ID;
        }

        private decimal GetLastPrice(decimal Price, int v_DiscountType, decimal v_DiscountRate)
        {
            decimal v_LastPrice = 0;
            if (v_DiscountType == 1)
            {
                v_LastPrice = Price - v_DiscountRate;
            }
            if (v_DiscountType == 0)
            {
                v_LastPrice = Price*(100 - v_DiscountRate)/100;
            }
            return v_LastPrice;
        }


        private void frm_qhe_doituong_dichvuCls_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdClose.PerformClick();
            if (e.KeyCode == Keys.F5) LoadData();
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoi.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdUpdate.PerformClick();
            if (e.KeyCode == Keys.S && e.Control) cmdSaveObjectAll_Click(cmdSaveObjectAll, new EventArgs());
        }

        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand1();
            ModifyCommand();
        }


        private void txtDetailLastPrice_LostFocus(object sender, EventArgs e)
        {
        }


        private void cmdPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void cmdExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
               SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                saveFileDialog1.FileName ="dmuc_cls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        gridEXExporter.GridEX = grdList;
                        gridEXExporter.Export(s);    

                    }
                    Utility.ShowMsg("Xuất Excel thành công. Nhấn OK để mở file");
                    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                }
                
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           

            //v_DataPrint =
            //    SPs.DmucLaydulieuQhedichvuclsIn(Utility.Int32Dbnull(cboKieuIn.SelectedValue, 0)).GetDataSet().Tables[0];
            //if (v_DataPrint.Rows.Count <= 0) return;
            //string reportcode = "qhe_PhieuinGiaCLStheodoituong";
            //string duongdan = Utility.GetPathExcel(reportcode);
            //var book = new C1XLBook();
            //book.Load(duongdan);
            //book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
            //XLSheet sheet = book.Sheets[0];

            //DataTable dt = v_DataPrint;
            //int idxRow = 6;
            //int idxColSh = 0;
            //int sttloaidichvu = 1;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        sheet[idxRow, idxColSh].SetValue(
            //            string.Format("{0}.{1}", sttloaidichvu, Convert.ToString(dt.Rows[i]["Ten_dichvu"])),
            //            HamDungChung.styleStringLeft_Bold(book));
            //        sttloaidichvu = sttloaidichvu + 1;
            //        idxRow = idxRow + 1;
            //    }
            //    else
            //    {
            //        if (dt.Rows[i]["Ten_dichvu"].ToString() !=
            //            dt.Rows[i - 1]["Ten_dichvu"].ToString())
            //        {
            //            sheet[idxRow, idxColSh].SetValue(
            //                string.Format("{0}.{1}", sttloaidichvu,
            //                    Convert.ToString(dt.Rows[i]["Ten_dichvu"])),
            //                HamDungChung.styleStringLeft_Bold(book));
            //            sttloaidichvu = sttloaidichvu + 1;
            //            idxRow = idxRow + 1;
            //        }
            //    }
            //    sheet[idxRow, idxColSh].SetValue(Convert.ToString(i.ToString()), HamDungChung.styleStringCenter(book));
            //    sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ma_chitietdichvu"]), HamDungChung.styleStringCenter(book));
            //    sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["ma_chitietdichvu_bhyt"]), HamDungChung.styleStringLeft(book));
            //    sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
            //    sheet[idxRow, idxColSh + 4].SetValue(Convert.ToDecimal(dt.Rows[i]["gia_bhyt"]), HamDungChung.styleNumber(book));
            //    sheet[idxRow, idxColSh + 5].SetValue(Convert.ToDecimal(dt.Rows[i]["gia_dv"]), HamDungChung.styleNumber(book));
            //    idxRow = idxRow + 1;
            //}
            //string getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
            //string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel\\ExportExcel\\";
            //if (!Directory.Exists(pathDirectory))
            //{
            //    Directory.CreateDirectory(pathDirectory);
            //}

            //book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" + reportcode +
            //          getTime + ".xls");
            //Process.Start(
            //    new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
            //                         reportcode + getTime + ".xls"));
            ////string sPath = "drug.xls";
            ////FileStream fs = new FileStream(sPath, FileMode.Create);
            ////gridEXExporter.Export(fs);
        }



        private QheDoituongDichvucl CreateObjectTypeService(GridEXRow gridExRow)
        {
            var objectTypeService = new QheDoituongDichvucl();
            objectTypeService.DonGia = Utility.DecimaltoDbnull(
                gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
            objectTypeService.PhuthuDungtuyen = Utility.DecimaltoDbnull(
                gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0);
            objectTypeService.IdChitietdichvu =
                Utility.Int16Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value);
            objectTypeService.MotaThem = gridExRow.Cells[QheDoituongDichvucl.Columns.MotaThem].Value.ToString();
            objectTypeService.IdDoituongKcb =
                Utility.Int16Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdDoituongKcb].Value, -1);
            objectTypeService.TyleGiam =
                Utility.Int16Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.TyleGiam].Value, -1);
            objectTypeService.KieuGiamgia =
                Utility.ByteDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.KieuGiamgia].Value);
            return objectTypeService;
        }

        private decimal GetLastPrice(GridEXRow gridExRow)
        {
            if (gridExRow.Cells[QheDoituongDichvucl.Columns.KieuGiamgia].Value.ToString() == "0")
            {
                return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0)*
                       (100 - Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.TyleGiam].Value))/100;
            }
            return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0) -
                   Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.TyleGiam].Value, 0);
        }

        private void cmDeteleServiceDetail_Click_1(object sender, EventArgs e)
        {
        }


        private void txtDetailLastPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }


        private void cmdSaveObjectAll_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private decimal LayGiaDV()
        {
            foreach (GridEXRow gridExRow in grdQhe.GetRows())
            {
                if (gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value.ToString() == "DV")
                    return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
            }
            return -1;
        }

        private decimal LayGiaBHYT()
        {
            foreach (GridEXRow gridExRow in grdQhe.GetRows())
            {
                if (gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value.ToString() == "BHYT")
                    return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
            }
            return -1;
        }

        private void SaveQheDoituongDichvuCSL()
        {
            try
            {
                foreach (GridEXRow gridExRow in grdList.GetRows())
                {
                    SqlQuery q =
                        new Select().From(QheDoituongDichvucl.Schema)
                            .Where(QheDoituongDichvucl.Columns.IdChitietdichvu)
                            .
                            IsEqualTo(
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1)).And(
                                        QheDoituongDichvucl.Columns.IdLoaidoituongKcb).IsEqualTo(
                                            Utility.Int32Dbnull(
                                                gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1));
                    if (q.GetRecordCount() > 0)
                    {
                        new Update(QheDoituongDichvucl.Schema)
                            .Set(QheDoituongDichvucl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(QheDoituongDichvucl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(QheDoituongDichvucl.Columns.IdDichvu).EqualTo(
                                GetService_ID(
                                    Utility.Int32Dbnull(
                                        gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1)))
                            .Set(QheDoituongDichvucl.Columns.DonGia).EqualTo(
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.PhuthuDungtuyen).EqualTo(
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.PhuthuTraituyen).EqualTo(
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.MotaThem).EqualTo(
                                Utility.sDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.MotaThem].Value, ""))
                            .Set(QheDoituongDichvucl.Columns.MaKhoaThuchien)
                            .EqualTo(Utility.sDbnull(cboKhoaTH.SelectedValue, ""))
                              .Set(QheDoituongDichvucl.Columns.TyleTt).EqualTo(Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.TyleTt].Value, 0))
                            .Where(QheDoituongDichvucl.Columns.IdChitietdichvu).IsEqualTo(ServiceDetail_ID)
                            .And(QheDoituongDichvucl.Columns.IdLoaidoituongKcb).IsEqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1))
                            .Execute();
                    }
                    else
                    {
                        QheDoituongDichvucl _newItem = new QheDoituongDichvucl();
                        _newItem.IdDoituongKcb = -1;
                        _newItem.IdDichvu = GetService_ID2(Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1));
                        _newItem.IdChitietdichvu = Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1);
                        _newItem.TyleGiam = 0;
                        _newItem.KieuGiamgia = 1;
                        _newItem.TyleTt = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.TyleTt].Value, 0);
                        _newItem.MotaThem = Utility.sDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.MotaThem].Value, "");
                        _newItem.DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
                        _newItem.PhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0);
                        _newItem.IdLoaidoituongKcb = Utility.Int32Dbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1);
                        _newItem.PhuthuTraituyen = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value, 0);
                        _newItem.MaDoituongKcb = "";
                        _newItem.NgayTao = globalVariables.SysDate;
                        _newItem.NguoiTao = globalVariables.UserName;
                        _newItem.MaKhoaThuchien = Utility.sDbnull(cboKhoaTH.SelectedValue, "");
                        _newItem.IsNew = true;
                        _newItem.Save();

                    }
                    SqlQuery sqlQuery = new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.Columns.IdLoaidoituongKcb).IsEqualTo(1);
                    var objectType = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                    if (objectType != null && objectType.MaDoituongKcb == "DV")
                    {
                        new Update(DmucDichvuclsChitiet.Schema)
                            .Set(DmucDichvuclsChitiet.Columns.DonGia)
                            .EqualTo(Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value,
                                0))
                            .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu)
                            .IsEqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value,
                                    -1)).Execute();
                    }
                }
                Utility.ShowMsg("Bạn thực hiện cập nhập giá thành công", "Thông báo");
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin", "Thông báo lỗi", MessageBoxIcon.Error);
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            ActionUpdate();
        }

        private void ActionUpdate()
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
            {
                if (grdList.CurrentRow == null) return;
                v_ServiceDetail_ID =
                    Utility.Int32Dbnull(grdList.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                frm_themmoi_dichvucls_chitiet frm = new frm_themmoi_dichvucls_chitiet();
                frm.txtID.Text = Utility.sDbnull(v_ServiceDetail_ID);
                frm.m_enAction = action.Update;
                frm.dtDataServiceDetail = m_dtDataDetailService;
                if (grdList.CurrentRow != null)
                    frm.drServiceDetail = Utility.FetchOnebyCondition(m_dtDataDetailService,
                        DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_ID);
                frm.ShowDialog();
            }
            ModifyCommand();
            ModifyCommand1();
        }

       

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            cmdUpdate.Enabled = false;
            if (grdList.RowCount > 0)
            {
                cmdUpdate.Enabled = grdList.RowCount > 0 && grdList.CurrentRow.RowType == RowType.Record;
            }
        }


        private void groupBox5_Enter(object sender, EventArgs e)
        {
        }

        private void cmdPrintRelationObject_Click(object sender, EventArgs e)
        {
            v_DataPrint =
                SPs.DmucLaydulieuQhedichvuclsIn(Utility.Int32Dbnull(cboKieuIn.SelectedValue, 0)).GetDataSet().Tables[0];
            THU_VIEN_CHUNG.CreateXML(v_DataPrint, "qhe_PhieuinGiaCLStheodoituong.XML");
            PrintReport(PropertyLib._QheGiaCLSProperties.TieudeBaocaoGiaCls);
        }

        private void ProcessDataReport(ref DataTable dataTable)
        {
            if (!dataTable.Columns.Contains("Price_DV")) dataTable.Columns.Add("Price_DV", typeof (decimal));
            if (!dataTable.Columns.Contains("Price_BHYT")) dataTable.Columns.Add("Price_BHYT", typeof (decimal));
            if (!dataTable.Columns.Contains("Price_KYC")) dataTable.Columns.Add("Price_KYC", typeof (decimal));
            foreach (DataRow drv in dataTable.Rows)
            {
                DataRow[] arrDr =
                    m_dtReportObjectType.Select(DmucDoituongkcb.Columns.IdLoaidoituongKcb + " =1 and " +
                                                DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                Utility.Int32Dbnull(drv[QheDoituongDichvucl.Columns.IdChitietdichvu], -1));
                if (arrDr.GetLength(0) > 0)
                {
                    drv["Price_DV"] = Utility.DecimaltoDbnull(arrDr[0][QheDoituongDichvucl.Columns.DonGia], 0);
                }
                DataRow[] arrDrBH =
                    m_dtReportObjectType.Select(DmucDoituongkcb.Columns.IdLoaidoituongKcb + " =0 and " +
                                                DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                Utility.Int32Dbnull(drv[QheDoituongDichvucl.Columns.IdChitietdichvu], -1));
                if (arrDrBH.GetLength(0) > 0)
                {
                    drv["Price_BHYT"] = Utility.DecimaltoDbnull(arrDrBH[0][QheDoituongDichvucl.Columns.DonGia], 0);
                }
                DataRow[] arrDrQNCS =
                    m_dtReportObjectType.Select(DmucDoituongkcb.Columns.IdLoaidoituongKcb + "= 2  and and " +
                                                DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                Utility.Int32Dbnull(drv[QheDoituongDichvucl.Columns.IdChitietdichvu], -1));
                if (arrDrQNCS.GetLength(0) > 0)
                {
                    drv["Price_KYC"] = Utility.DecimaltoDbnull(arrDrQNCS[0][QheDoituongDichvucl.Columns.DonGia], 0);
                }
            }
            dataTable.AcceptChanges();
        }

        /// <summary>
        ///     hàm thực hiện in báo cáo
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void PrintReport(string sTitleReport)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("qhe_PhieuinGiaCLStheodoituong", ref tieude, ref reportname);
            if (crpt == null) return;
            var objFromPre = new frmPrintPreview(tieude, crpt, true, v_DataPrint.Rows.Count > 0);
            //var objFromPre =
            //    new frmPrintPreview(sTitleReport, crpt,false, true);
            Utility.WaitNow(this);
            crpt.SetDataSource(v_DataPrint);
            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
            Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
            Utility.SetParameterValue(crpt, "sDateTime", Utility.FormatDateTime(globalVariables.SysDate));
            Utility.SetParameterValue(crpt, "txtTrinhky",
                Utility.getTrinhky(objFromPre.mv_sReportFileName, globalVariables.SysDate));
            objFromPre.crptViewer.ReportSource = crpt;
            objFromPre.ShowDialog();
            Utility.DefaultNow(this);
        }


        private DataTable GetDataCheck()
        {
            DataTable dataTable = m_dtDataDetailService.Copy();
            foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
            {
                DataRow[] arrDr =
                    dataTable.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                     Utility.Int32Dbnull(
                                         gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1));
                if (arrDr.GetLength(0) <= 0)
                {
                    arrDr[0].Delete();
                }
            }

            dataTable.AcceptChanges();
            return dataTable;
        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
            {
                if (grdList.CurrentRow == null) return;
                var frm = new frm_themmoi_dichvucls_chitiet();
                frm.grdlist = grdList;
                frm.txtID.Text = "-1";
                frm.m_enAction = action.Insert;
                frm.dtDataServiceDetail = m_dtDataDetailService;
                if (grdList.CurrentRow != null)
                    frm.drServiceDetail = Utility.FetchOnebyCondition(m_dtDataDetailService,
                        DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_ID);
                frm.ShowDialog();
            }
            ModifyCommand();
            ModifyCommand1();
        }

        private void cmdCapNhap_Click(object sender, EventArgs e)
        {
            ActionUpdate();
        }

        private void cboKhoaTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded || !CLS_GIATHEO_KHOAKCB) return;
            grdList_SelectionChanged(grdList, e);
        }

        private void cmdCauhinh_Click(object sender, EventArgs e)
        {

        }

        private void cmdExportExcel_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuCreateGiaDV_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.Log(this.Name, globalVariables.UserName, "Reset lại quan hệ đối tượng cận lâm sàng dựa theo đối tượng DV", newaction.Reset, this.GetType().Assembly.ManifestModule.Name);
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                DmucDoituongkcb objDoituong = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo("DV").ExecuteSingle<DmucDoituongkcb>();
                if (objDoituong != null)
                {
                    SPs.QheDichvuclsDoituongkcbAutoCreateDv(objDoituong.IdLoaidoituongKcb, objDoituong.IdDoituongKcb, objDoituong.IdDoituongKcb, globalVariables.UserName, DateTime.Now).Execute();
                }
                Utility.ShowMsg("Đã tạo dữ liệu thành công. Nhấn OK để kết thúc");
                grdList_SelectionChanged(grdList, e);

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cboThembogia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                frm_ThemBogia _ThemBogia = new frm_ThemBogia();
                _ThemBogia.m_enAct = newaction.Insert;
                _ThemBogia.ShowDialog();
                LoadBogia();
                cboBogia.SelectedIndex = Utility.GetSelectedIndex(cboBogia, _ThemBogia._objBogia.IdBogia.ToString());
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
           
        }
        void LoadBogia()
        {
            try
            {
               
                DataTable dtBogia = SPs.QheGiadoituongClsTimdanhsachbogia(-1, "-1", "-1",DateTime.Now.Date).GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboBogia, dtBogia, DmucBogiadichvu.Columns.IdBogia, DmucBogiadichvu.Columns.TenBogia, "", true);
                cboBogia_SelectedIndexChanged(cboBogia, new EventArgs());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void cboSuabogia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                int id_bogia = Utility.Int32Dbnull(cboBogia.SelectedValue, -1);
                if (id_bogia <= 0)
                {
                    return;
                }
                frm_ThemBogia _ThemBogia = new frm_ThemBogia();
                _ThemBogia._objBogia = DmucBogiadichvu.FetchByID(id_bogia);
                _ThemBogia.m_enAct = newaction.Update;
                _ThemBogia.ShowDialog();
                LoadBogia();
                cboBogia.SelectedIndex = Utility.GetSelectedIndex(cboBogia, id_bogia.ToString());
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }

        private void CmdList_Click(object sender, EventArgs e)
        {
            frm_quanly_bogia_bhyt _quanly_bogia_bhyt = new frm_quanly_bogia_bhyt();
            _quanly_bogia_bhyt.KTH = Utility.sDbnull(cboKhoaTH.SelectedValue, "ALL"); 
            _quanly_bogia_bhyt.ShowDialog();
            LoadBogia();
        }

        private void mnuResetAndAutoCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.Log(this.Name, globalVariables.UserName, "Tạo giá quan hệ đối tượng cận lâm sàng dựa theo đối tượng tham chiếu", newaction.Reset, this.GetType().Assembly.ManifestModule.Name);
                if (!globalVariables.isSuperAdmin)
                {
                    Utility.ShowMsg("Chỉ có super Admin mới được phép sử dụng tính năng này");
                    return;
                }
                if (Utility.Int32Dbnull(cboBogia.SelectedValue, -1) <= 0)
                {
                    if (Utility.AcceptQuestion("Nếu bạn muốn tạo giá cho đối tượng BHYT thì cần chọn bộ giá BHYT trước khi dùng chức năng này. Nhấn Yes để quay lại chọn bộ giá BHYT, nhấn No để tiếp tục", "Xác nhận", true))
                        return;
                }
                Utility.WaitNow(this);
                //if (grdList.GetCheckedRows().Length <= 0)
                //{
                //    Utility.ShowMsg("Bạn cần chọn ít nhất 1 dịch vụ cận lâm sàng để thực hiện chức năng này");
                //    return;
                //}
                frm_ChonDoituongKCB_All frm = new frm_ChonDoituongKCB_All();
                frm._enObjectType = enObjectType.DichvuCLS;
                frm.m_dtQheDoituong_CLS = m_dtGiaDichvu.Clone();
                frm.Original_Price =
                    Utility.DecimaltoDbnull(grdList.CurrentRow.Cells["gia_dichvu"].Value, 0);
                frm.MaKhoaTHIEN = Utility.sDbnull(cboKhoaTH.SelectedValue, "");
                frm.DetailService =
                    Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, 0);
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                    AutoCreate(frm.m_dtQheDoituong_CLS, frm.id_doituongkcb_thamchieu, true);

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
                ModifyCommand1();
                ModifyCommand();
            }
        }

        private void mnuTaoQheGiaDvuCLS_DoituongKCB_Click_1(object sender, EventArgs e)
        {

        }
    }
}