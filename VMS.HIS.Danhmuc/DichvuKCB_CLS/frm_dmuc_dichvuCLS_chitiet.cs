using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.NGHIEPVU;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.UI.HinhAnh;
using VNS.HIS.BusRule.Classes;
using System.IO;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_dichvuCLS_chitiet : Form
    {
        #region "KHAI BAO THUOC TINH"
        private DataTable dsTable = new DataTable();
        private DataTable dsTableDetail = new DataTable();
        private string rowFilter = "1=1";
        private DataTable m_dtDichvuCLS = new DataTable();
        NLog.Logger mylog = NLog.LogManager.GetLogger("frm_dmuc_dichvuCLS_chitiet");

        string Args = "ALL";
        #endregion

        #region "HAM KHOI TAO "
        /// <summary>
        /// HAM KHOI TAO THONG TIN CHI TIET TIM KIEM
        /// </summary>
        public frm_dmuc_dichvuCLS_chitiet(string Args)
        {
            InitializeComponent();
            this.Args = Args;
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            this.Shown += frm_dmuc_dichvuCLS_chitiet_Shown;
            printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            grdServiceDetail.ApplyingFilter += new CancelEventHandler(grdServiceDetail_ApplyingFilter);
            grdServiceDetail.CellEdited += new ColumnActionEventHandler(grdServiceDetail_CellEdited);
            grdServiceDetail.CellValueChanged += grdServiceDetail_CellValueChanged;
            grdServiceDetail.UpdatingCell += grdServiceDetail_UpdatingCell;
            grdServiceDetail.SelectionChanged += new EventHandler(grdServiceDetail_SelectionChanged);
            grdChitiet.SelectionChanged += new EventHandler(grdChitiet_SelectionChanged);
            grdServiceDetail.FilterApplied += new EventHandler(grdServiceDetail_FilterApplied);
            bool quyensuagiaclstrenluoi = Utility.Coquyen("danhmuc_dichvucls_suagiatrenluoi");
            if (!quyensuagiaclstrenluoi)
            {
                grdServiceDetail.RootTable.Columns["gia_goc"].EditType = EditType.NoEdit;
                grdServiceDetail.RootTable.Columns["don_gia"].EditType = EditType.NoEdit;
                grdServiceDetail.RootTable.Columns["tinh_chkhau"].EditType = EditType.NoEdit;
            }
            grdChitiet.GotFocus += grdChitiet_GotFocus;
            grdChitiet.UpdatingCell += grdChitiet_UpdatingCell;
            grdServiceDetail.GotFocus += grdServiceDetail_GotFocus;
            grdVungKs.MouseDoubleClick+=grdVungKs_MouseDoubleClick;
            grdServiceDetail.DoubleClick += new EventHandler(grdServiceDetail_DoubleClick);
            grdChitiet.DoubleClick += new EventHandler(grdChitiet_DoubleClick);
            cmdConfig.Click += cmdConfig_Click;
            txtLoaiDichvu._OnEnterMe += txtLoaiDichvu__OnEnterMe;
            txtLoaiDichvu._OnSelectionChanged += txtLoaiDichvu__OnSelectionChanged;
            grdVungKs.RowCheckStateChanged += grdVungKs_RowCheckStateChanged;
            txtDichvu._OnEnterMe += txtDichvu__OnEnterMe;
            cmdAccept.Click += cmdAccept_Click;

        }

        void grdServiceDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "tnv_chidinh")
                {
                    if (Utility.Coquyen("cls_tnvchidinh"))
                    {
                        bool tnv_chidinh = Utility.Byte2Bool(grdServiceDetail.GetValue("tnv_chidinh"));
                        int id_chitietdichvu = Utility.Int32Dbnull(grdServiceDetail.GetValue("id_chitietdichvu"), 0);
                        int num = new Update(DmucDichvuclsChitiet.Schema).Set(DmucDichvuclsChitiet.TnvChidinhColumn).EqualTo(tnv_chidinh).Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(id_chitietdichvu).Execute();
                        if (num > 0)
                        {
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật trạng thái TNV chỉ định cho dịch vụ CLS ID={0} về trạng thái {1} thành công ", id_chitietdichvu, tnv_chidinh), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        void frm_dmuc_dichvuCLS_chitiet_Shown(object sender, EventArgs e)
        {
            if (Args == "DANHMUC")
            {
                uitabpageVKS.TabVisible = uiTabPageQheCamchidinh.TabVisible = false;
                panel3.Width = 0;
            }
            else if (Args == "VKS")
            {
                cmdNew.Visible = cmdEdit.Visible = cmdDelete.Visible = cmdDeleteALL.Visible = cmdConfig.Visible = false;
                uiTabPageQheCamchidinh.TabVisible = false;
            }
            else if (Args == "TUONGTACCLS")
            {
                uitabpageVKS.TabVisible = false;
            }
            else if (Args == "VIEW")
            {
                uitabpageVKS.TabVisible = uiTabPageQheCamchidinh.TabVisible = false;
                panel3.Width = 0;
                cmdThemMoi.Visible = cmdSua.Visible =cmdEdit.Visible= cmdXoa.Visible =cmdDeleteALL.Visible=cmdNew.Visible=cmdDelete.Visible=mnuHuyTvnChidinh.Visible=mnuTnvChidinh.Visible= false;
            }
        }

        void cmdAccept_Click(object sender, EventArgs e)
        {
           dmucDichvuCLS_busrule.UpdateQhe(v_ServiceDetail_Id, GetQheCamchidinhChungphieuCollection());
           Utility.ShowMsg("Đã cập nhật tương tác cận lâm sàng thành công");
        }

        void grdVungKs_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            try
            {
                string idvungks = "";
                if (grdServiceDetail.GetCheckedRows().Length > 0)
                {
                    foreach (GridEXRow _row in grdServiceDetail.GetCheckedRows())
                    {
                        DmucDichvuclsChitiet objDvu = DmucDichvuclsChitiet.FetchByID(Utility.sDbnull(_row.Cells["id_chitietdichvu"].Value, ""));
                        if (objDvu != null)
                        {
                            if (grdVungKs.GetCheckedRows().Count() > 0)
                            {

                                var query = (from chk in grdVungKs.GetCheckedRows()
                                             let x = Utility.sDbnull(chk.Cells[DmucVungkhaosat.Columns.Id].Value)
                                             select x).ToArray();
                                if (query != null && query.Count() > 0)
                                {
                                    idvungks = string.Join(",", query);
                                }
                            }
                            else
                            {

                                idvungks = Utility.GetValueFromGridColumn(grdVungKs, DmucVungkhaosat.Columns.Id);
                            }
                            objDvu.DsachVungkhaosat = idvungks;
                            objDvu.Save();
                            foreach (DataRow dr in dsTable.Rows)
                                if (Utility.Int32Dbnull(dr["id_chitietdichvu"], -1) == objDvu.IdChitietdichvu)
                                    dr["dsach_vungkhaosat"] = idvungks;
                            //dsTable.AcceptChanges();
                        }
                    }
                }
                else
                {
                    DmucDichvuclsChitiet objDvu = DmucDichvuclsChitiet.FetchByID(Utility.GetValueFromGridColumn(grdServiceDetail, "id_chitietdichvu"));
                    if (objDvu != null)
                    {
                        if (grdVungKs.GetCheckedRows().Count() > 0)
                        {

                            var query = (from chk in grdVungKs.GetCheckedRows()
                                         let x = Utility.sDbnull(chk.Cells[DmucVungkhaosat.Columns.Id].Value)
                                         select x).ToArray();
                            if (query != null && query.Count() > 0)
                            {
                                idvungks = string.Join(",", query);
                            }
                        }
                        else
                        {

                            idvungks = Utility.GetValueFromGridColumn(grdVungKs, DmucVungkhaosat.Columns.Id);
                        }
                        objDvu.DsachVungkhaosat = idvungks;
                        objDvu.Save();
                        foreach (DataRow dr in dsTable.Rows)
                            if (Utility.Int32Dbnull(dr["id_chitietdichvu"], -1) == objDvu.IdChitietdichvu)
                                dr["dsach_vungkhaosat"] = idvungks;
                        dsTable.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void grdChitiet_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "stt_hthi_chitiet")
            {
                DmucDichvuclsChitiet objdvclsct = DmucDichvuclsChitiet.FetchByID(Utility.Int64Dbnull(grdChitiet.CurrentRow.Cells["id_chitietdichvu"].Value, 0));
                if (objdvclsct != null)
                {
                    new Update(DmucDichvuclsChitiet.Schema).Set(DmucDichvuclsChitiet.Columns.SttHthi).EqualTo(Utility.Int32Dbnull(e.Value, 0)).Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(objdvclsct.IdChitietdichvu).Execute();
                    //objdvclsct.MarkOld();
                    //objdvclsct.IsNew = false;
                    //objdvclsct.SttHthi = Utility.Int32Dbnull(e.Value, 0);
                    //objdvclsct.Save();
                }
            }
        }

        void grdServiceDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            
            if (e.Column.Key == "don_gia")
            {
                DmucDichvuclsChitiet objdvclsct = DmucDichvuclsChitiet.FetchByID(Utility.Int64Dbnull(grdServiceDetail.CurrentRow.Cells["id_chitietdichvu"].Value, 0));
                if (objdvclsct != null)
                {
                    objdvclsct.MarkOld();
                    objdvclsct.IsNew = false;
                    objdvclsct.DonGia = Utility.DecimaltoDbnull(e.Value, 0);
                    objdvclsct.Save();
                }
            }
            else if (e.Column.Key == "gia_goc")
            {
                DmucDichvuclsChitiet objdvclsct = DmucDichvuclsChitiet.FetchByID(Utility.Int64Dbnull(grdServiceDetail.CurrentRow.Cells["id_chitietdichvu"].Value, 0));
                if (objdvclsct != null)
                {
                    objdvclsct.MarkOld();
                    objdvclsct.IsNew = false;
                    objdvclsct.GiaGoc = Utility.DecimaltoDbnull(e.Value, 0);
                    objdvclsct.Save();
                }
            }
            else if (e.Column.Key == "tinh_chkhau")
            {
                DmucDichvuclsChitiet objdvclsct = DmucDichvuclsChitiet.FetchByID(Utility.Int64Dbnull(grdServiceDetail.CurrentRow.Cells["id_chitietdichvu"].Value, 0));
                if (objdvclsct != null)
                {
                    objdvclsct.MarkOld();
                    objdvclsct.IsNew = false;
                    objdvclsct.TinhChkhau = Utility.ByteDbnull(e.Value);
                    objdvclsct.Save();
                }
            }
            else if (e.Column.Key == "stt_hthi")
            {
                DmucDichvuclsChitiet objdvclsct = DmucDichvuclsChitiet.FetchByID(Utility.Int64Dbnull(grdServiceDetail.CurrentRow.Cells["id_chitietdichvu"].Value, 0));
                if (objdvclsct != null)
                {
                    new Update(DmucDichvuclsChitiet.Schema).Set(DmucDichvuclsChitiet.Columns.SttHthi).EqualTo(Utility.Int32Dbnull(e.Value, 0)).Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(objdvclsct.IdChitietdichvu).Execute();
                    //objdvclsct.MarkOld();
                    //objdvclsct.IsNew = false;
                    //objdvclsct.SttHthi = Utility.Int32Dbnull(e.Value, 0);
                    //objdvclsct.Save();
                }
            }
        }

        void txtLoaiDichvu__OnSelectionChanged()
        {
            try
            {
                mylog.Trace("SPs.DmucLaydanhmucDichvuclsChitiet....");
                dsTable = SPs.DmucLaydanhmucDichvuclsChitiet(1, Utility.Int32Dbnull(txtLoaiDichvu.MyID, 0)).GetDataSet().Tables[0];
                mylog.Trace("SetDataSourceForDataGridEx....");
                Utility.SetDataSourceForDataGridEx(grdServiceDetail, dsTable, true, true, "id_cha<=0", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");

            }
            catch
            {
            }
        }

        void txtLoaiDichvu__OnEnterMe()
        {
            try
            {
                mylog.Trace("SPs.DmucLaydanhmucDichvuclsChitiet....");
                dsTable = SPs.DmucLaydanhmucDichvuclsChitiet(1, Utility.Int32Dbnull(txtLoaiDichvu.MyID, 0)).GetDataSet().Tables[0];
                mylog.Trace("SetDataSourceForDataGridEx....");
                Utility.SetDataSourceForDataGridEx(grdServiceDetail, dsTable, true, true, "id_cha<=0", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");
            }
            catch
            {
            }
        }

        void cmdConfig_Click(object sender, EventArgs e)
        {
           
        }
        void grdChitiet_DoubleClick(object sender, EventArgs e)
        {
            cmdEdit.PerformClick();
        }

        void grdServiceDetail_DoubleClick(object sender, EventArgs e)
        {
            cmdEdit.PerformClick();
        }

        void grdServiceDetail_GotFocus(object sender, EventArgs e)
        {
            _currentGRd = grdServiceDetail;
        }

        void grdChitiet_GotFocus(object sender, EventArgs e)
        {
            _currentGRd = grdChitiet;
        }

        void grdChitiet_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdChitiet)) return;
                v_ServiceDetail_Id = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
        void LoadDinhmucVTTH()
        {
            DataTable dtDinhmucVtth = new KCB_KEDONTHUOC().DmucLaychitietDinhmucVtth(Utility.Int32Dbnull(grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1));
           Utility.SetDataSourceForDataGridEx(grdDinhmucVTTH, dtDinhmucVtth, false, true, "1=1", "" + DmucThuoc.Columns.TenThuoc);
            grdDinhmucVTTH.MoveFirst();
        }
        void cboService_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        bool hanchequyendanhmuc = false;
        #region "HAM DUNG CHUNG"
        /// <summary>
        /// HAM THUC HIEN LOAD THONG TIN CUA DICH VU
        /// </summary>
        void InitData()
        {
            try
            {
                mylog.Trace("Load DmucDichvucl....");
                m_dtDichvuCLS = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                DataTable mDtDichvuClsNew = m_dtDichvuCLS.Clone();
                if (globalVariables.gv_dtQuyenNhanvien_Dmuc.Select(QheNhanvienDanhmuc.Columns.Loai + "= 0").Length <= 0)
                    mDtDichvuClsNew = m_dtDichvuCLS.Copy();
                else
                {
                    foreach (DataRow dr in m_dtDichvuCLS.Rows)
                    {
                        if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucDichvucl.Columns.IdLoaidichvu]), "0"))
                        {
                            hanchequyendanhmuc = true;
                            mDtDichvuClsNew.ImportRow(dr);
                        }
                    }
                }

                txtDichvu.Init(globalVariables.gv_dtDmucDichvuClsChitiet,
                new List<string>
                {
                    DmucDichvuclsChitiet.Columns.IdDichvu,
                    DmucDichvuclsChitiet.Columns.MaChitietdichvu,
                    DmucDichvuclsChitiet.Columns.TenChitietdichvu
                });
                Utility.SetDataSourceForDataGridEx_Basic(grdDanhsachCamChidinhChungphieu, globalVariables.gv_dtDmucDichvuClsChitiet, true, true, "1=1",
                "CHON DESC," + VDmucDichvuclsChitiet.Columns.SttHthiLoaidvu + "," +
                VDmucDichvuclsChitiet.Columns.SttHthiDichvu + "," + VDmucDichvuclsChitiet.Columns.SttHthi + "," +
                VDmucDichvuclsChitiet.Columns.TenChitietdichvu);
                txtLoaiDichvu.Init(mDtDichvuClsNew, new List<string>() { DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.MaDichvu, DmucDichvucl.Columns.TenDichvu });
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        /// <summary>
        /// HAM THUC HIEN TIM KIEM BAT SU KIEN TIM KIEM
        /// </summary>
        void Search()
        {
            try
            {

                mylog.Trace("SPs.DmucLaydanhmucDichvuclsChitiet....");
                dsTable = SPs.DmucLaydanhmucDichvuclsChitiet(1, hanchequyendanhmuc ? Utility.Int32Dbnull(txtLoaiDichvu.MyID, 0) : -1).GetDataSet().Tables[0];
                if (optTatca.Checked)
                {

                }
                else
                {
                    dsTable = optHieuluc.Checked ? dsTable.Select("trang_thai = 1").CopyToDataTable() : dsTable.Select("trang_thai = 0").CopyToDataTable();
                }
                mylog.Trace("SetDataSourceForDataGridEx....");
               // Utility.SetDataSourceForDataGridEx(grdServiceDetail, dsTable, true, true, "id_cha<=0", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");
                Utility.SetDataSourceForDataGridEx(grdServiceDetail, dsTable, true, true, "1=1", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");
                _currentGRd = grdServiceDetail;
                ModifyCommand();
            }
            catch (Exception)
            {

                ModifyCommand();
            }

        }
        void SearchDetail(long id_cha)
        {
            try
            {
                DataRow[] arrDr = dsTable.Select("id_cha=" + id_cha.ToString());
                if (arrDr.Length > 0) dsTableDetail = arrDr.CopyToDataTable();
                else dsTableDetail = null;
                Utility.SetDataSourceForDataGridEx(grdChitiet, dsTableDetail, true, true, "1=1", "stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
                //grdServiceDetail.DropDowns[0].DataSource = globalVariables.g_dtMeasureUnit;
                ModifyCommand();
            }
            catch (Exception)
            {

                ModifyCommand();
            }

        }
        /// <summary>
        /// HAM THUC HIEN ENABLE HOAC DISABLE CUA NUT
        /// </summary>
        void ModifyCommand()
        {
            try
            {
                cmdEdit.Enabled = Utility.isValidGrid(grdServiceDetail) || Utility.isValidGrid(grdChitiet);
                cmdDeleteALL.Enabled = Utility.isValidGrid(grdServiceDetail) || Utility.isValidGrid(grdChitiet);
                cmdDelete.Enabled = Utility.isValidGrid(grdServiceDetail) || Utility.isValidGrid(grdChitiet);
            }
            catch (Exception)
            {


            }

            //cmdSearchGrid.Enabled = grdServiceDetail.RowCount > 0;
        }
        #endregion

        #region "HAM KHAI BAO DUNG SU KIEN CUA FORM"
        /// <summary>
        /// HAM BAT SU KIEN TIM KIEM KHI NHAN NUT TIM KIEM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
                ModifyCommand();
            }
            catch (Exception)
            {


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
                if (this.ActiveControl != null && this.ActiveControl.Name == grdServiceDetail.Name)
                {
                    if (!Utility.isValidGrid(grdServiceDetail))
                    {
                        Utility.ShowMsg("Bạn cần chọn một dịch vụ trên lưới trước khi xóa");
                        return;
                    }
                    v_ServiceDetail_Id = Utility.Int32Dbnull(grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    KcbChidinhclsChitiet item = new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdChitietdichvu).IsEqualTo(v_ServiceDetail_Id).ExecuteSingle<KcbChidinhclsChitiet>();
                    if (item != null)
                    {
                        Utility.ShowMsg("Dịch vụ bạn chọn xóa đã từng được Bác sĩ dùng để chỉ định cho Bệnh nhân nên bạn không thể xóa");
                        return;
                    }
                    if (Utility.AcceptQuestion("Bạn có muốn xoá dịch vụ đang chọn không", "Thông báo", true))
                    {
                        SPs.DmucXoadanhmucDichvuclsChitiet(v_ServiceDetail_Id).Execute();
                        dsTable.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_Id)[0].Delete();
                        dsTable.AcceptChanges();

                    }

                }
                else if (this.ActiveControl != null && this.ActiveControl.Name == grdChitiet.Name)
                {
                    if (!Utility.isValidGrid(grdChitiet))
                    {
                        Utility.ShowMsg("Bạn cần chọn một dịch vụ trên lưới trước khi xóa");
                        return;
                    }
                    v_ServiceDetail_Id = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    KcbKetquaCl item = new Select().From(KcbKetquaCl.Schema).Where(KcbKetquaCl.Columns.IdDichvuchitiet).IsEqualTo(v_ServiceDetail_Id).ExecuteSingle<KcbKetquaCl>();
                    if (item != null)
                    {
                        Utility.ShowMsg("Dịch vụ bạn chọn xóa đã từng được Bác sĩ dùng để chỉ định cho Bệnh nhân nên bạn không thể xóa");
                        return;
                    }

                    if (Utility.AcceptQuestion("Bạn có muốn xoá dịch vụ đang chọn không", "Thông báo", true))
                    {
                        SPs.DmucXoadanhmucDichvuclsChitiet(v_ServiceDetail_Id).Execute();
                        dsTable.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_Id)[0].Delete();
                        dsTable.AcceptChanges();

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
                        string lstvalues = "";
                        foreach (GridEXRow gridExRow in _currentGRd.GetCheckedRows())
                        {
                            int _IdChitietdichvu = Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                            KcbChidinhclsChitiet item = new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdChitietdichvu).IsEqualTo(v_ServiceDetail_Id).ExecuteSingle<KcbChidinhclsChitiet>();
                            if (item != null)
                            {
                                lsterr = lsterr + Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "") + "\n";
                            }
                            else
                            {
                                SPs.DmucXoadanhmucDichvuclsChitiet(_IdChitietdichvu).Execute();
                                lstvalues += _IdChitietdichvu.ToString() + ",";
                            }
                        }
                        DataRow[] rows;
                        if (lstvalues.Length > 0)
                        {
                            lstvalues = lstvalues.Substring(0, lstvalues.Length - 1);
                            rows = dsTable.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + " IN (" + lstvalues + ")");
                            // UserName is Column Name
                            foreach (DataRow r in rows)
                                r.Delete();
                            dsTable.AcceptChanges();
                        }
                        if (Utility.DoTrim(lsterr) != "")
                        {
                            Utility.ShowMsg("Một số dịch vụ chi tiết sau đã được chỉ định nên bạn không thể xóa\n" + lsterr);
                        }
                    }
                    dsTable.AcceptChanges();
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

        /// <summary>
        /// HAM THUC HIEN THOAT FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// HAM THUC HIEN DUNG PHIM TAT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_dichvuCLS_chitiet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) toolStripButton1.PerformClick();
            if (e.KeyCode == Keys.F5) Search();
            if (e.Control && e.KeyCode == Keys.N) cmdNew.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
        }
        /// <summary>
        /// HAM THUC HIEN TAO MOI CHI TIET DICH VU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            frm_themmoi_dichvucls_chitiet frm = new frm_themmoi_dichvucls_chitiet();
            frm.txtID.Text = "-1";
            frm.m_enAction = action.Insert;
            frm.grdlistChitiet = grdChitiet;
            frm.grdlist = grdServiceDetail;
            frm.dtDataServiceDetail = dsTable;
            frm.Service_ID = Utility.Int32Dbnull(txtLoaiDichvu.MyID, -1);
            frm.ShowDialog();
            grdServiceDetail_SelectionChanged(grdServiceDetail, e);
            ModifyCommand();
        }
        /// <summary>
        /// HAM THUC HIEN SUA THONG TIN CHI TIET
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (_currentGRd != null && _currentGRd.Name == grdServiceDetail.Name)
            {
                if (Utility.isValidGrid(grdServiceDetail))
                {
                    frm_themmoi_dichvucls_chitiet frm = new frm_themmoi_dichvucls_chitiet();
                    frm.txtID.Text = grdServiceDetail.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu).ToString();
                    frm.m_enAction = action.Update;
                    frm.grdlist = grdServiceDetail;
                    frm.grdlistChitiet = grdChitiet;
                    frm.dtDataServiceDetail = dsTable;
                    if (grdServiceDetail.CurrentRow != null)
                        frm.drServiceDetail = Utility.FetchOnebyCondition(dsTable, DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_Id);
                    frm.ShowDialog();
                    grdServiceDetail_SelectionChanged(grdServiceDetail, e);
                }
            }
            else
            {
                if (Utility.isValidGrid(grdChitiet))
                {
                    frm_themmoi_dichvucls_chitiet frm = new frm_themmoi_dichvucls_chitiet();
                    frm.txtID.Text = grdChitiet.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu).ToString();
                    frm.m_enAction = action.Update;
                    frm.grdlist = grdChitiet;
                    frm.grdlistChitiet = grdChitiet;
                    frm.dtDataServiceDetail = dsTable;
                    if (grdChitiet.CurrentRow != null)
                        frm.drServiceDetail = Utility.FetchOnebyCondition(dsTable, DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_Id);
                    frm.ShowDialog();
                    grdServiceDetail_SelectionChanged(grdServiceDetail, e);
                }
            }
            ModifyCommand();
        }
        /// <summary>
        /// HAM THUC HIEN LOAD THONG TIN KHI LOAD FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_dichvuCLS_chitiet_Load(object sender, EventArgs e)
        {
            mylog.Trace("Load danh muc dung chung");
            GetVungKS();
            DataTable dt = new Select().From(DmucChung.Schema).ExecuteDataSet().Tables[0];
            dt = new Select(DmucChung.Columns.Ten).From(DmucChung.Schema).ExecuteDataSet().Tables[0];
            InitData();
            Search();
            ModifyCommand();
        }



        #endregion

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có muốn lưu toàn bộ thông tin này không", "Thông báo", true))
                {
                    foreach (DataRowView drv in dsTable.DefaultView)
                    {
                        new Update(DmucDichvuclsChitiet.Schema)
                            .Set(DmucDichvuclsChitiet.Columns.SttHthi).EqualTo(
                                Utility.Int32Dbnull(drv[DmucDichvuclsChitiet.Columns.SttHthi], 1))
                            .Set(DmucDichvuclsChitiet.Columns.MaChitietdichvu).EqualTo(
                                Utility.sDbnull(drv[DmucDichvuclsChitiet.Columns.MaChitietdichvu], ""))
                            .Set(DmucDichvuclsChitiet.Columns.TenChitietdichvu).EqualTo(
                                Utility.sDbnull(drv[DmucDichvuclsChitiet.Columns.TenChitietdichvu], ""))
                            .Set(DmucDichvuclsChitiet.Columns.DonGia).EqualTo(
                                Utility.DecimaltoDbnull(drv[DmucDichvuclsChitiet.Columns.DonGia], 0))
                            .Set(DmucDichvuclsChitiet.Columns.MaDonvitinh).EqualTo(
                                Utility.DecimaltoDbnull(drv[DmucDichvuclsChitiet.Columns.MaDonvitinh], 0))
                            .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(
                                Utility.Int32Dbnull(drv[DmucDichvuclsChitiet.Columns.IdChitietdichvu], -1)).Execute();

                    }
                    Utility.ShowMsg("Bạn thực hiện thành công", "Thông báo");
                }
            }
            catch (Exception)
            {

                ModifyCommand();
            }


        }


        private void grdServiceDetail_FilterApplied(object sender, EventArgs e)
        {
            ModifyCommand();
        }
        string dsach_vks = "";
        /// <summary>
        /// hàm thực hiện di chuyển trn lươới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdServiceDetail_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Utility.isValidGrid(grdServiceDetail))
                {
                    dsach_vks = Utility.sDbnull(grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.DsachVungkhaosat].Value, "");
                    v_ServiceDetail_Id = Utility.Int32Dbnull(grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    bool co_chitiet = Utility.sDbnull(grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.CoChitiet].Value, "0") == "1";
                    grdChitiet.Height = co_chitiet ? 200 : 0;
                    if (co_chitiet)
                        SearchDetail(v_ServiceDetail_Id);
                    AutocheckVKS();
                    LoadDinhmucVTTH();
                    ModifyCommand();
                }
                else v_ServiceDetail_Id = -1;
            }
            catch (Exception)
            {


            }
            finally
            {
                LoadQheCamchidinhchung(v_ServiceDetail_Id);
            }

        }
        void AutocheckVKS()
        {
            grdVungKs.UnCheckAllRecords();
            foreach (GridEXRow item in grdVungKs.GetDataRows())
            {
                if (("," + dsach_vks + ",").Contains("," + item.Cells[DmucVungkhaosat.Columns.Id].Value.ToString() + ","))
                {

                    item.BeginEdit();
                    item.IsChecked = true;
                    item.EndEdit();
                }
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void grdServiceDetail_CellEdited(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                string Code = "";
                Code = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.MaChitietdichvu].Value.ToString();
                string Id = "-1";
                Id = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value.ToString();
                new Update(DmucDichvuclsChitiet.Schema)
                    .Set(DmucDichvuclsChitiet.Columns.MaChitietdichvu).EqualTo(Code)
                    .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(
                        Utility.Int32Dbnull(Id)).Execute();
                ModifyCommand();
            }
            catch (Exception exception)
            {
                ModifyCommand();
            }

        }

        private void grdServiceDetail_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiên viecj cập nhập thôn tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdServiceDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            int ServiceDetail_ID = Utility.Int32Dbnull(grdServiceDetail.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu));

            if (e.Column.Key == DmucDichvuclsChitiet.Columns.SttHthi)
            {
                new Update(DmucDichvuclsChitiet.Schema)
                    .Set(DmucDichvuclsChitiet.Columns.SttHthi).EqualTo(
                        Utility.Int32Dbnull(grdServiceDetail.GetValue(DmucDichvuclsChitiet.Columns.SttHthi)))
                    .Set(DmucDichvuclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(DmucDichvuclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(
                        Utility.Int32Dbnull(grdServiceDetail.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu))).Execute();

            }
            Utility.GotoNewRowJanus(grdServiceDetail, DmucDichvuclsChitiet.Columns.IdChitietdichvu, Utility.sDbnull(ServiceDetail_ID));
        }

        private void optTatca_CheckedChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void optHieuluc_CheckedChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void optHethieuluc_CheckedChanged(object sender, EventArgs e)
        {
            Search();
        }



        private DataTable _dtVungKS;
        private void GetVungKS()
        {
            try
            {
                //if (this.m_strMaDichvu != "ALL")
                //{
                //    System.Collections.Generic.List<Int16> lstIdDvu = new Select(DmucDichvucl.Columns.IdDichvu).From(DmucDichvucl.Schema).Where(DmucDichvucl.Columns.IdLoaidichvu).In(m_strMaDichvu.Split(',').ToList<string>()).ExecuteTypedList<Int16>();
                //    _dtVungKS = new Select().From(DmucVungkhaosat.Schema).Where(DmucVungkhaosat.Columns.IdLoaidvu).In(lstIdDvu).ExecuteDataSet().Tables[0];
                //}
                //else
                    _dtVungKS = new Select().From(DmucVungkhaosat.Schema).ExecuteDataSet().Tables[0];
                grdVungKs.DataSource = _dtVungKS;
            }
            catch (Exception ex)
            {
            }
        }
        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            var obj = new DmucVungkhaosat { NgayTao = DateTime.Now };
            var f = new frm_themmoi_vungkhaosat("ALL") { m_enAct = action.Insert, Table = _dtVungKS, Obj = obj };
            f.ShowDialog();
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVungKs)) return;
            var obj = new DmucVungkhaosat(grdVungKs.GetValue(DmucVungkhaosat.Columns.Id));
            var f = new frm_themmoi_vungkhaosat("ALL") { m_enAct = action.Update, Obj = obj, Table = _dtVungKS };
            f.ShowDialog();
        }
        bool isValidbeforeDelete(string id_vungks, string tenvungks)
        {
            try
            {
                DataTable dtused = SPs.DmucVungkhaosatKiemtratruockhixoa(id_vungks).GetDataSet().Tables[0];
                if (dtused.Rows.Count > 0)
                {
                    Utility.ShowMsg(string.Format("Vùng khảo sát {0} đã được gắn với dịch vụ cận lâm sàng hoặc đã được sử dụng nhập trả kết quả nên bạn không thể xóa.", tenvungks));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
                return false;
            }
        }
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các vùng khảo sát đang được chọn", "Xác nhận xóa vùng khảo sát", true))
                {
                    if (grdVungKs.GetCheckedRows().Count() > 0)
                    {
                        foreach (GridEXRow row in grdVungKs.GetCheckedRows())
                        {
                            var keyId = (int)row.Cells[DmucVungkhaosat.Columns.Id].Value;
                             string tenvungks = row.Cells[DmucVungkhaosat.Columns.TenVungkhaosat].Value.ToString();
                             if (!isValidbeforeDelete(keyId.ToString(), tenvungks)) continue;
                             else
                             {
                                 DataRow dr = (from datarow in _dtVungKS.AsEnumerable()
                                               where datarow.Field<int>(DmucVungkhaosat.Columns.Id) == keyId
                                               select datarow).First();

                                 DmucVungkhaosat.Delete(keyId);
                                 if (dr != null) _dtVungKS.Rows.Remove(dr);
                             }
                        }
                    }
                    else
                    {
                        if (grdVungKs.CurrentRow == null) return;
                        if (!grdVungKs.CurrentRow.RowType.Equals(RowType.Record)) return;
                        var keyId = (int)grdVungKs.GetValue(DmucVungkhaosat.Columns.Id);
                        string tenvungks = grdVungKs.GetValue(DmucVungkhaosat.Columns.TenVungkhaosat).ToString();
                         if (!isValidbeforeDelete(keyId.ToString(), tenvungks)) return;
                         else
                         {
                             DataRow row = (from datarow in _dtVungKS.AsEnumerable()
                                            where datarow.Field<int>(DmucVungkhaosat.Columns.Id) == keyId
                                            select datarow).First();
                             DmucVungkhaosat.Delete(keyId);
                             if (row != null) _dtVungKS.Rows.Remove(row);
                         }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnLayDuLieu_Click(object sender, EventArgs e)
        {
            GetVungKS();
        }

        void grdVungKs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Utility.isValidGrid(grdVungKs))
                cmdSua.PerformClick();
        }

        private void cmdGanVungKS_Click(object sender, EventArgs e)
        {
            try
            {
                string idvungks = "";
                if (grdChitiet.GetCheckedRows().Length > 0)
                {
                    foreach (GridEXRow _row in grdChitiet.GetCheckedRows())
                    {
                        DmucDichvuclsChitiet objDvu = DmucDichvuclsChitiet.FetchByID(Utility.sDbnull(_row.Cells["id_chitietdichvu"].Value, ""));
                        if (objDvu != null)
                        {
                            if (grdVungKs.GetCheckedRows().Count() > 0)
                            {

                                var query = (from chk in grdVungKs.GetCheckedRows()
                                             let x = Utility.sDbnull(chk.Cells[DmucVungkhaosat.Columns.Id].Value)
                                             select x).ToArray();
                                if (query != null && query.Count() > 0)
                                {
                                    idvungks = string.Join(",", query);
                                }
                            }
                            else
                            {

                                idvungks = Utility.GetValueFromGridColumn(grdVungKs, DmucVungkhaosat.Columns.Id);
                            }
                            objDvu.DsachVungkhaosat = idvungks;
                            objDvu.Save();
                            foreach (DataRow dr in dsTable.Rows)
                                if (Utility.Int32Dbnull(dr["id_chitietdichvu"], -1) == objDvu.IdChitietdichvu)
                                    dr["dsach_vungkhaosat"] = idvungks;
                            dsTable.AcceptChanges();
                        }
                    }
                }
                else
                {
                    DmucDichvuclsChitiet objDvu = DmucDichvuclsChitiet.FetchByID(Utility.GetValueFromGridColumn(grdServiceDetail, "id_chitietdichvu"));
                    if (objDvu != null)
                    {
                        if (grdVungKs.GetCheckedRows().Count() > 0)
                        {

                            var query = (from chk in grdVungKs.GetCheckedRows()
                                         let x = Utility.sDbnull(chk.Cells[DmucVungkhaosat.Columns.Id].Value)
                                         select x).ToArray();
                            if (query != null && query.Count() > 0)
                            {
                                idvungks = string.Join(",", query);
                            }
                        }
                        else
                        {

                            idvungks = Utility.GetValueFromGridColumn(grdVungKs, DmucVungkhaosat.Columns.Id);
                        }
                        objDvu.DsachVungkhaosat = idvungks;
                        objDvu.Save();
                        foreach (DataRow dr in dsTable.Rows)
                            if (Utility.Int32Dbnull(dr["id_chitietdichvu"], -1) == objDvu.IdChitietdichvu)
                                dr["dsach_vungkhaosat"] = idvungks;
                        dsTable.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdDynamic_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdServiceDetail))
                {
                    Utility.ShowMsg("Bạn cần chọn một dịch vụ liên quan đến hình ảnh(XQuang,Siêu Âm, Nội soi...) để thực hiện cấu hình");
                    return;
                }
                int idvungks = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdVungKs, DmucVungkhaosat.Columns.Id), -1);

                DmucVungkhaosat objvks = DmucVungkhaosat.FetchByID(idvungks);
                try
                {
                    if (objvks == null)
                    {
                        Utility.ShowMsg("Bạn cần chọn chỉ định chi tiết cần cập nhật kết quả");
                        return;
                    }
                    frm_DynamicSetup _DynamicSetup = new frm_DynamicSetup();
                    _DynamicSetup.objvks = objvks;
                    _DynamicSetup.ImageID = -1;
                    _DynamicSetup.Id_chidinhchitiet = -1;
                    if (_DynamicSetup.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
            }
        }

        private void cmdConfig_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdDuplicate_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVungKs)) return;
            var obj = new DmucVungkhaosat(grdVungKs.GetValue(DmucVungkhaosat.Columns.Id));
            var f = new frm_themmoi_vungkhaosat("ALL") { m_enAct = action.Duplicate, Obj = obj, Table = _dtVungKS };
            f.ShowDialog();
        }

        private void cmdDynamicFields_Click(object sender, EventArgs e)
        {
            try
            {
                DmucVungkhaosat vks = DmucVungkhaosat.FetchByID(Utility.Int32Dbnull(grdVungKs.GetValue(DmucVungkhaosat.Columns.Id)));
                if (vks == null)
                {
                    Utility.ShowMsg("Bạn cần chọn chỉ định chi tiết cần cập nhật kết quả");
                    return;
                }
                frm_DynamicSetup _DynamicSetup = new frm_DynamicSetup();
                _DynamicSetup.objvks = vks;
                _DynamicSetup.ImageID = -1;
                _DynamicSetup.Id_chidinhchitiet = -1;
                if (_DynamicSetup.ShowDialog() == DialogResult.OK)
                {
                   
                }
            }
            catch (Exception)
            {

            }
        }
        #region "Tương tác cận lâm sàng-cận lâm sàng"
        private void txtDichvu__OnEnterMe()
        {
            foreach (GridEXRow gridExRow in grdDanhsachCamChidinhChungphieu.GetDataRows())
            {
                gridExRow.BeginEdit();
                if (Utility.Int32Dbnull(gridExRow.Cells[QheCamchidinhChungphieu.Columns.IdDichvu].Value) ==
                    Utility.Int32Dbnull(txtDichvu.MyID))
                {
                    gridExRow.Cells["CHON"].Value = 1;
                    gridExRow.IsChecked = true;
                    break;
                }
                gridExRow.BeginEdit();
            }
        }
        private void LoadQheCamchidinhchung(int id_chitiet)
        {
            QheCamchidinhChungphieuCollection lstqhe=
                new Select()
                .From(QheCamchidinhChungphieu.Schema)
                .WhereExpression(QheCamchidinhChungphieu.Columns.IdDichvu).IsEqualTo( id_chitiet).OrExpression(QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung).IsEqualTo(id_chitiet).CloseExpression().ExecuteAsCollection<QheCamchidinhChungphieuCollection>();
            foreach (GridEXRow gridExRow in grdDanhsachCamChidinhChungphieu.GetDataRows())
            {
                gridExRow.BeginEdit();
                if (Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value) !=
                    id_chitiet)
                {
                    IEnumerable<QheCamchidinhChungphieu> query = from kho in lstqhe.AsEnumerable()
                                                                 where
                                                                     Utility.Int32Dbnull(kho.IdDichvu, 0)
                                                                     == Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value)
                                                                     || Utility.Int32Dbnull(kho.IdDichvuCamchidinhchung, 0)
                                                                     == Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value)
                                                                 select kho;
                    if (query.Count() > 0)
                    {
                        //gridExRow.Cells["CHON"].Value = 1;
                        gridExRow.IsChecked = true;
                    }
                    else
                    {
                        //gridExRow.Cells["CHON"].Value = 0;
                        gridExRow.IsChecked = false;
                    }
                }
                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();
            }
        }
        private QheCamchidinhChungphieuCollection GetQheCamchidinhChungphieuCollection()
        {
            var lst = new QheCamchidinhChungphieuCollection();
            foreach (GridEXRow gridExRow in grdDanhsachCamChidinhChungphieu.GetCheckedRows())
            {
                var objQheNhanvienDanhmuc = new QheCamchidinhChungphieu();
                objQheNhanvienDanhmuc.IdDichvu = -1;
                objQheNhanvienDanhmuc.Loai = 0;
                objQheNhanvienDanhmuc.IdDichvuCamchidinhchung =
                    Utility.Int32Dbnull(gridExRow.Cells[VDmucDichvuclsChitiet.Columns.IdChitietdichvu].Value);
                objQheNhanvienDanhmuc.IsNew = true;
                lst.Add(objQheNhanvienDanhmuc);
            }
            return lst;
        }

        #endregion

        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            VNS.HIS.UI.NGOAITRU.frm_themmoi_dinhmuc_vtth _themmoi_dinhmuc_vtth = new NGOAITRU.frm_themmoi_dinhmuc_vtth("VT");
            _themmoi_dinhmuc_vtth.objDvuChitiet = DmucDichvuclsChitiet.FetchByID(v_ServiceDetail_Id);
            _themmoi_dinhmuc_vtth.ShowDialog();
            grdServiceDetail_SelectionChanged(grdServiceDetail, e);
        }

        private void cmdXoaDinhmucVTTh_Click(object sender, EventArgs e)
        {
            foreach (GridEXRow gridExRow in grdDinhmucVTTH.GetCheckedRows())
            {
                long IdChitiet = Utility.Int64Dbnull(gridExRow.Cells[TDinhmucVtth.Columns.Id].Value, -1);
                new Delete().From(TDinhmucVtth.Schema).Where(TDinhmucVtth.Columns.Id).IsEqualTo(IdChitiet).Execute();
                gridExRow.Delete();
                grdDinhmucVTTH.UpdateData();
                grdDinhmucVTTH.Refresh();
                
            }
        }

        private void mnuTnvChidinh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("cls_tnvchidinh"))
                {
                    Utility.ShowMsg("Bạn không có quyền cập nhật dịch vụ thu ngân viên chỉ định(cls_tnvchidinh). Liên hệ IT để được trợ giúp");
                    return;
                }
                List<int> lstId = (from p in grdServiceDetail.GetCheckedRows().AsEnumerable() select Utility.Int32Dbnull(p.Cells["id_chitietdichvu"].Value, -1)).Distinct().ToList<int>();
                if (lstId.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 dịch vụ trước khi cập nhật thu ngân viên chỉ định");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn cấp quyền thu ngân viên chỉ định cho các dịch vụ đang chọn trên lưới?", "Xác nhận", true))
                {
                    int num=new Update(DmucDichvuclsChitiet.Schema).Set(DmucDichvuclsChitiet.TnvChidinhColumn).EqualTo(1).Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).In(lstId).Execute();
                    if (num > 0)
                    {
                        if (num > 0)
                        {
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật trạng thái TNV chỉ định cho dịch vụ CLS ID={0} về trạng thái 1 thành công ", string.Join(",",lstId)), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        }
                        foreach (GridEXRow _row in grdServiceDetail.GetCheckedRows())
                        {
                            _row.BeginEdit();
                            _row.Cells["tnv_chidinh"].Value = 1;
                            _row.EndEdit();
                        }
                        Utility.ShowMsg("Đã cấp quyền thu ngân viên chỉ định với các dịch vụ đang chọn. Nhấn OK để kết thúc");
                    }
                }
                
            }
            catch (Exception ex)
            {
                
               
            }
        }

        private void mnuHuyTvnChidinh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("cls_tnvchidinh"))
                {
                    Utility.ShowMsg("Bạn không có quyền hủy dịch vụ thu ngân viên chỉ định(cls_tnvchidinh). Liên hệ IT để được trợ giúp");
                    return;
                }
                List<int> lstId = (from p in grdServiceDetail.GetCheckedRows().AsEnumerable() select Utility.Int32Dbnull(p.Cells["id_chitietdichvu"].Value, -1)).Distinct().ToList<int>();
                if (lstId.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 dịch vụ trước khi cập nhật thu ngân viên chỉ định");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn HỦY cấp quyền thu ngân viên chỉ định cho các dịch vụ đang chọn trên lưới?", "Xác nhận hủy", true))
                {
                    int num = new Update(DmucDichvuclsChitiet.Schema).Set(DmucDichvuclsChitiet.TnvChidinhColumn).EqualTo(0).Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).In(lstId).Execute();
                    if (num > 0)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy trạng thái TNV chỉ định cho dịch vụ CLS ID={0} về trạng thái 0 thành công ", string.Join(",",lstId)), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        foreach (GridEXRow _row in grdServiceDetail.GetCheckedRows())
                        {
                            _row.BeginEdit();
                            _row.Cells["tnv_chidinh"].Value = 0;
                            _row.EndEdit();
                        }
                        Utility.ShowMsg("Đã HỦY cấp quyền thu ngân viên chỉ định với các dịch vụ đang chọn. Nhấn OK để kết thúc");
                    }
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                saveFileDialog1.FileName = "dmuc_dichvu_canlamsang.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        gridEXExporter1.GridEX = grdServiceDetail;
                        gridEXExporter1.Export(s);

                    }
                    Utility.ShowMsg("Xuất Excel thành công. Nhấn OK để mở file");
                    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }



    }
}
