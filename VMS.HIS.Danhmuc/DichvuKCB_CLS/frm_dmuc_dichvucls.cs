using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_dichvucls : Form
    {
        private  DataTable m_dtLoaiDichvuCLS=new DataTable();
        private  DataTable dtDataCLS=new DataTable();
        private string RowFilter = "1=1";
        bool m_blnLoaded = false;
        public frm_dmuc_dichvucls()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //grdList.ApplyingFilter+=new CancelEventHandler(grdList_ApplyingFilter);
            grdList.SelectionChanged +=new EventHandler(grdList_SelectionChanged);
           grdList.FilterApplied+=new EventHandler(grdList_FilterApplied);
           grdList.CellUpdated += new ColumnActionEventHandler(grdList_CellUpdated);
           grdList.EditingCell += grdList_EditingCell;
           grdList.UpdatingCell += grdList_UpdatingCell;
           cboDepartment.SelectedIndexChanged += new EventHandler(cboDepartment_SelectedIndexChanged);
        }

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                int ServiceDetail_ID = Utility.Int32Dbnull(grdList.GetValue(DmucDichvucl.Columns.IdDichvu));

                if (e.Column.Key == DmucDichvucl.Columns.SttHthi)
                {
                    new Update(DmucDichvucl.Schema)
                        .Set(DmucDichvucl.Columns.SttHthi).EqualTo(
                            Utility.Int32Dbnull(grdList.GetValue(DmucDichvucl.Columns.SttHthi)))
                        .Set(DmucDichvucl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                        .Set(DmucDichvucl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        .Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(
                            Utility.Int32Dbnull(grdList.GetValue(DmucDichvucl.Columns.IdDichvu))).Execute();

                }
                if (e.Column.Key == DmucDichvucl.Columns.CdhaTenphieu)
                {
                    new Update(DmucDichvucl.Schema)
                        .Set(DmucDichvucl.Columns.CdhaTenphieu).EqualTo(Utility.sDbnull(e.Value))
                        .Set(DmucDichvucl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                        .Set(DmucDichvucl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        .Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(DmucDichvucl.Columns.IdDichvu))).Execute();

                }
                Utility.GotoNewRowJanus(grdList, DmucDichvucl.Columns.IdDichvu, Utility.sDbnull(ServiceDetail_ID));
            }
            catch
            { }
        }

        void grdList_EditingCell(object sender, EditingCellEventArgs e)
        {
            
        }

        void grdList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if(!Utility.Coquyen("DMUC_CANLAMSANG_SUANHOM_TRENLUOI"))
            {
                Utility.thongbaokhongcoquyen("DMUC_CANLAMSANG_SUANHOM_TRENLUOI","quyền sửa các nhóm in, nhóm chi phí, nhóm BHYT, nhóm phiếu EMR trên lưới. Vui lòng liên hệ IT bệnh viện");
                return;
            }    
            string colKey = e.Column.Key;
            string colValue = Utility.sDbnull(grdList.GetValue(colKey));
            int v_intIdDichvu = Utility.Int32Dbnull(grdList.GetValue(DmucDichvucl.Columns.IdDichvu));
            int num = 0;
            if (colKey == DmucDichvucl.Columns.MaPhieuEmr)
            {
                num = new Update(DmucDichvucl.Schema).Set(DmucDichvucl.Columns.MaPhieuEmr).EqualTo(colValue).Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(v_intIdDichvu).Execute();
            }
            else if (colKey == DmucDichvucl.Columns.NhomBaocao)
            {
                num = new Update(DmucDichvucl.Schema).Set(DmucDichvucl.Columns.NhomBaocao).EqualTo(colValue).Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(v_intIdDichvu).Execute();
            }
            else if (colKey == DmucDichvucl.Columns.NhomInCls)
            {
                num = new Update(DmucDichvucl.Schema).Set(DmucDichvucl.Columns.NhomInCls).EqualTo(colValue).Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(v_intIdDichvu).Execute();
            }
            else if (colKey == DmucDichvucl.Columns.NhomInphoiBHYT)
            {
                num = new Update(DmucDichvucl.Schema).Set(DmucDichvucl.Columns.NhomInphoiBHYT).EqualTo(colValue).Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(v_intIdDichvu).Execute();
            }
        }

        void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            DataTable dtPhong = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1), 1);
            DataBinding.BindDataCombobox(cboPhongthuchien, dtPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa phòng", true);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search();
            ModifyCommand();
        }
        bool hanchequyendanhmuc = false;
        List<string> lstLoaiCLS = new List<string>();
        void InitData()
        {
            try
            {
                DataTable dtPhieuEMR = new Select("*").From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("EMR_PHIEU")
               .OrderAsc(DmucChung.Columns.SttHthi)
               .ExecuteDataSet().Tables[0];
                if (grdList.DropDowns.Contains("cboPhieuEmr"))
                {
                    grdList.DropDowns["cboPhieuEmr"].DataSource = dtPhieuEMR;
                }
                DataTable dtNhomchiphi = new Select("*").From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("NHOMBAOCAOCLS")
              .OrderAsc(DmucChung.Columns.SttHthi)
              .ExecuteDataSet().Tables[0];
                if (grdList.DropDowns.Contains("cboNhomChiphi"))
                {
                    grdList.DropDowns["cboNhomChiphi"].DataSource = dtNhomchiphi;
                }
                DataTable dtNhominphieuCLS = new Select("*").From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("NHOM_INPHIEU_CLS")
              .OrderAsc(DmucChung.Columns.SttHthi)
              .ExecuteDataSet().Tables[0];
                if (grdList.DropDowns.Contains("cboNhominphieu"))
                {
                    grdList.DropDowns["cboNhominphieu"].DataSource = dtNhominphieuCLS;
                }
                DataTable dtnhominphoi = new Select().From(DmucChung.Schema)
                 .Where(DmucChung.Columns.Loai).IsEqualTo(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_STT_INPHOI", "STT_INPHOIBHYT", true))
                 .And(DmucChung.Columns.VietTat).IsEqualTo("2")
                 .ExecuteDataSet().Tables[0];
                if (grdList.DropDowns.Contains("cbo_nhom_inphoiBHYT"))
                {
                    grdList.DropDowns["cbo_nhom_inphoiBHYT"].DataSource = dtnhominphoi;
                }


                m_dtLoaiDichvuCLS = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAIDICHVUCLS", true);
                DataTable m_dtLoaiDichvuCLS_new = m_dtLoaiDichvuCLS.Clone();
                if (globalVariables.gv_dtQuyenNhanvien_Dmuc.Select(QheNhanvienDanhmuc.Columns.Loai + "= 0").Length <= 0)
                    m_dtLoaiDichvuCLS_new = m_dtLoaiDichvuCLS.Copy();
                else
                {
                    foreach (DataRow dr in m_dtLoaiDichvuCLS.Rows)
                    {
                        if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucChung.Columns.Ma]), "0"))
                        {
                            hanchequyendanhmuc = true;
                            if (!lstLoaiCLS.Contains(Utility.sDbnull(dr[DmucChung.Columns.Ma], "0")))
                                lstLoaiCLS.Add(Utility.sDbnull(dr[DmucChung.Columns.Ma], "0"));
                            m_dtLoaiDichvuCLS_new.ImportRow(dr);
                        }
                    }
                }
                DataBinding.BindDataCombox(cboServiceType, m_dtLoaiDichvuCLS_new, DmucChung.Columns.Ma, DmucChung.Columns.Ten,"---Chọn---", false);
                DataBinding.BindDataCombox(cbonhombaocao, dtNhomchiphi, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataTable m_dtKhoaChucNang = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL",1);
                DataBinding.BindDataCombobox(cboDepartment, m_dtKhoaChucNang, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Chọn---", true);
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("InitData()-->\n" + ex.Message);
            }
        }
        void Search()
        {
            SqlQuery _sqlquery = new Select().From(VDmucDichvucl.Schema);
            if (Utility.sDbnull(cboServiceType.SelectedValue, "-1") != "-1" )
                if (_sqlquery.HasWhere)
                    _sqlquery.Where(VDmucDichvucl.Columns.IdLoaidichvu).IsEqualTo(Utility.sDbnull(cboServiceType.SelectedValue, "-1"));
                else
                    _sqlquery.And(VDmucDichvucl.Columns.IdLoaidichvu).IsEqualTo(Utility.sDbnull(cboServiceType.SelectedValue, "-1"));
            if (hanchequyendanhmuc)
                if (_sqlquery.HasWhere)
                    _sqlquery.Where(VDmucDichvucl.Columns.IdLoaidichvu).In(lstLoaiCLS);
                else
                    _sqlquery.And(VDmucDichvucl.Columns.IdLoaidichvu).In(lstLoaiCLS);
            if (Utility.sDbnull(cbonhombaocao.SelectedValue, "-1") != "-1")
                if (_sqlquery.HasWhere)
                    _sqlquery.Where(VDmucDichvucl.Columns.NhomBaocao).IsEqualTo(Utility.sDbnull(cbonhombaocao.SelectedValue, "-1"));
                else
                    _sqlquery.And(VDmucDichvucl.Columns.NhomBaocao).IsEqualTo(Utility.sDbnull(cbonhombaocao.SelectedValue, "-1"));

            if (Utility.Int32Dbnull(cboDepartment.SelectedValue, -1) != -1)
                if (_sqlquery.HasWhere)
                    _sqlquery.Where(VDmucDichvucl.Columns.IdKhoaThuchien).IsEqualTo(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1));
                else
                    _sqlquery.And(VDmucDichvucl.Columns.IdKhoaThuchien).IsEqualTo(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1));

            if (Utility.Int32Dbnull(cboPhongthuchien.SelectedValue, -1) != -1)
                if (_sqlquery.HasWhere)
                    _sqlquery.Where(VDmucDichvucl.Columns.IdPhongThuchien).IsEqualTo(Utility.Int32Dbnull(cboPhongthuchien.SelectedValue, -1));
                else
                    _sqlquery.And(VDmucDichvucl.Columns.IdPhongThuchien).IsEqualTo(Utility.Int32Dbnull(cboPhongthuchien.SelectedValue, -1));

            dtDataCLS = _sqlquery.ExecuteDataSet().Tables[0];
            dtDataCLS.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdList, dtDataCLS, true, true, "1=1", "stt_hthi_loaidvu,ten_loaidichvu,stt_hthi,ten_dichvu");
        }

        private void frm_dmuc_dichvucls_Load(object sender, EventArgs e)
        {
            InitData();
            m_blnLoaded = true;
            Search();
            ModifyCommand();


        }
        void ModifyCommand()
        {

            if (!Utility.isValidGrid(grdList))
            {
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
                cmdDeleteALL.Enabled = grdList.RowCount > 0;
                cmdPrint.Enabled = grdList.RowCount > 0;
                cmdSaveAll.Enabled = grdList.RowCount > 0;
            }
            else
            {
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
                cmdDeleteALL.Enabled = grdList.RowCount > 0;
                cmdPrint.Enabled = grdList.RowCount > 0;
                cmdSaveAll.Enabled = grdList.RowCount > 0;
            }
           
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
           
        }

      
     
        private int v_Service_ID = -1;
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtDataCLS.DefaultView.Count <= 0)
                {
                    Utility.ShowMsg("Hiện thời không có bản ghi nào để thao tác", "Thông báo");
                    grdList.Focus();
                    return;

                }
                DmucDichvuclsChitiet item = new Select().From(DmucDichvuclsChitiet.Schema).Where(DmucDichvuclsChitiet.Columns.IdDichvu).IsEqualTo(v_Service_ID).ExecuteSingle<DmucDichvuclsChitiet>();
                if (item != null)
                {
                    Utility.ShowMsg("Dịch vụ bạn chọn xóa đã có chi tiết nên bạn không thể xóa");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có muốn xoá dịch vụ đang chọn không ", "Thông báo", true))
                {
                    if (grdList.CurrentRow != null)
                    {
                        new Delete()
                            .From(DmucDichvucl.Schema)
                            .Where(DmucDichvucl.Columns.IdDichvu)
                            .IsEqualTo(v_Service_ID).Execute();
                        DataRow[] arrDr = dtDataCLS.Select(DmucDichvucl.Columns.IdDichvu + "=" + v_Service_ID);
                        if (arrDr.GetLength(0) > 0)
                        {
                            arrDr[0].Delete();
                        }
                        dtDataCLS.AcceptChanges();

                    }
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

        private void cmdDeleteALL_Click(object sender, EventArgs e)
        {

            try
            {
                Janus.Windows.GridEX.GridEXRow[] checkedRows;
                checkedRows = grdList.GetCheckedRows();
                if (checkedRows.Length == 0)
                {

                    Utility.ShowMsg("Bạn phải chọn một bản ghi thao tác", "Thông báo");
                    grdList.Focus();
                    return;
                }
                string lstError = "";
                if (grdList.CurrentRow != null)
                {
                    string message = string.Format("Bạn có muốn xoá {0} bản ghi đang chọn không", checkedRows.Length);
                    if (Utility.AcceptQuestion(message, "Thông báo", true))
                    {
                        string lstvalues = "";
                        foreach (Janus.Windows.GridEX.GridEXRow row in checkedRows)
                        {
                            int iddichvu = Utility.Int32Dbnull(row.Cells[DmucDichvucl.Columns.IdDichvu].Value, 0);
                            DmucDichvuclsChitiet item = new Select().From(DmucDichvuclsChitiet.Schema).Where(DmucDichvuclsChitiet.Columns.IdDichvu).IsEqualTo(iddichvu).ExecuteSingle<DmucDichvuclsChitiet>();
                            if (item != null)
                            {
                                lstError =lstError+ Utility.sDbnull(row.Cells[DmucDichvucl.Columns.TenDichvu].Value, "")+"\n";
                            }
                            else
                            {
                            new Delete()
                                .From(DmucDichvucl.Schema)
                                .Where(DmucDichvucl.Columns.IdDichvu)
                                .IsEqualTo(iddichvu)
                                .Execute();
                            lstvalues += iddichvu.ToString() + ",";
                            }
                        }
                        DataRow[] rows;
                        if (lstvalues.Length > 0)
                        {
                            lstvalues = lstvalues.Substring(0, lstvalues.Length - 1);
                            rows = dtDataCLS.Select(DmucDichvucl.Columns.IdDichvu + " IN (" + lstvalues + ")");
                            // UserName is Column Name
                            foreach (DataRow r in rows)
                                r.Delete();
                            dtDataCLS.AcceptChanges();
                        }
                        if (Utility.DoTrim(lstError) != "")
                        {
                            Utility.ShowMsg("Một số dịch vụ sau đã có chi tiết nên bạn không thể xóa\n" + lstError);
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommand();
            }
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_dichvucls frm = new frm_themmoi_dichvucls();
                frm.em_Action = action.Insert;
                frm.grdService = grdList;
                frm.dsService = dtDataCLS;
                frm.ShowDialog();
                ModifyCommand();
            }catch(Exception exception)
            {
                ModifyCommand();
            }
           
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_dichvucls frm = new frm_themmoi_dichvucls();
                frm.em_Action = action.Update;
                if (grdList.CurrentRow != null)
                {
                    frm.drServiceInfo = Utility.FetchOnebyCondition(dtDataCLS,DmucDichvucl.Columns.IdDichvu+ "=" + v_Service_ID);
                    frm.txtID.Text = v_Service_ID.ToString();
                    frm.dsService = dtDataCLS;
                    frm.grdService = grdList;
                    frm.ShowDialog();
                    ModifyCommand();
                }
            }
            catch (Exception)
            {

                ModifyCommand();
            }
           
        }

       
        private void frm_dmuc_dichvucls_KeyDown(object sender, KeyEventArgs e)
        {
            //Add event handeler for Ctrl + E and Ctrl + D
            if(e.KeyCode==Keys.F4)cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.Control && (e.KeyCode == Keys.N)) cmdNew.PerformClick();//Call edit command
            if (e.Control && (e.KeyCode == Keys.E)) cmdEdit.PerformClick();//Call edit command
            if (e.Control && (e.KeyCode == Keys.D)) cmdDelete.PerformClick();//Call delete command
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }
        
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (grdList.CurrentRow != null&&grdList.CurrentRow.RowType==RowType.Record)
            {
                v_Service_ID = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvucl.Columns.IdDichvu].Value, -1);
            }
            ModifyCommand();
        }

        private void grdList_FilterApplied(object sender, EventArgs e)
        {
           
        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridEXRow gridExRow in grdList.GetRows())
                {
                    if (gridExRow.RowType == RowType.Record)
                    {
                        new Update(DmucDichvucl.Schema)
                            .Set(DmucDichvucl.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(gridExRow.Cells[DmucDichvucl.Columns.DonGia].Value, 0))
                            .Set(DmucDichvucl.Columns.SttHthi).EqualTo(Utility.Int32Dbnull(gridExRow.Cells[DmucDichvucl.Columns.SttHthi].Value,
                                                                                        -1))
                            .Set(DmucDichvucl.Columns.TenDichvu).EqualTo(Utility.sDbnull(
                                gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, ""))
                                  .Set(DmucDichvucl.Columns.MotaThem).EqualTo(Utility.sDbnull(
                                gridExRow.Cells[DmucDichvucl.Columns.MotaThem].Value, ""))
                            .Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[DmucDichvucl.Columns.IdDichvu].Value, -1)).Execute();

                    }
                }
                grdList.UpdateData();
                Utility.ShowMsg("Cập nhập thông tin thành công","Thông báo");
            }catch(Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ","Thông báo",MessageBoxIcon.Error);
                return;
            }
            

        }
        int num = 0;
        private void mnu_capnhat_nhomchiphi_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Cần chọn một Dịch vụ trên lưới để lấy nhóm chi phí của dịch vụ đó làm nguồn dữ liệu cập nhật cho các Dịch vụ đang chọn khác");
                    return;
                }
                List<int> lstIdDichvu = grdList.GetCheckedRows().Select(c => Utility.Int32Dbnull(c.Cells["id_dichvu"].Value)).ToList<int>();
               
                    string nhom_baocao = Utility.sDbnull(grdList.CurrentRow.Cells["nhom_baocao"].Value);
                num= new Update(DmucDichvucl.Schema)
                .Set(DmucDichvucl.Columns.NhomBaocao).EqualTo(nhom_baocao)
                .Where(DmucDichvucl.Columns.IdDichvu).In(lstIdDichvu)
                .Execute();
                if(num>0)
                {
                    // Cập nhật cột nhom_baocao
                    foreach (var row in grdList.GetCheckedRows())
                    {
                        // Nếu dữ liệu gốc từ DataRow:
                        var drv = row.DataRow as DataRowView;
                        if (drv != null)
                        {
                            drv["nhom_baocao"] = nhom_baocao;
                        }
                        else
                        {
                            // Trong trường hợp grid bind trực tiếp với object model:
                            row.BeginEdit();
                            row.Cells["nhom_baocao"].Value = nhom_baocao;
                            row.EndEdit();
                        }
                    }
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật nhóm chi phí cho các dịch vụ {0} về giá trị {1} thành công ", lstIdDichvu.ToString(), nhom_baocao), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }    
               
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }

        private void mnu_capnhat_nhominphieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Cần chọn một Dịch vụ trên lưới để lấy Nhóm in phiếu CĐ của dịch vụ đó làm nguồn dữ liệu cập nhật cho các Dịch vụ đang chọn khác");
                    return;
                }
                List<int> lstIdDichvu = grdList.GetCheckedRows().Select(c => Utility.Int32Dbnull(c.Cells["id_dichvu"].Value)).ToList<int>();

                string nhom_in_cls = Utility.sDbnull(grdList.CurrentRow.Cells["nhom_in_cls"].Value);
                num = new Update(DmucDichvucl.Schema)
                .Set(DmucDichvucl.Columns.NhomInCls).EqualTo(nhom_in_cls)
                .Where(DmucDichvucl.Columns.IdDichvu).In(lstIdDichvu)
                .Execute();
                if (num > 0)
                {
                    // Cập nhật cột nhom_baocao
                    foreach (GridEXRow row in grdList.GetCheckedRows())
                    {
                        // Nếu dữ liệu gốc từ DataRow:
                        var drv = row.DataRow as DataRowView;
                        if (drv != null)
                        {
                            drv["nhom_in_cls"] = nhom_in_cls;
                        }
                        else
                        {
                            // Trong trường hợp grid bind trực tiếp với object model:
                            row.BeginEdit();
                            row.Cells["nhom_in_cls"].Value = nhom_in_cls;
                            row.EndEdit();
                        }
                    }
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Nhóm in phiếu CĐ cho các dịch vụ {0} về giá trị {1} thành công ", lstIdDichvu.ToString(), nhom_in_cls), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnu_capnhat_nhominphoiBHYT_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Cần chọn một Dịch vụ trên lưới để lấy Nhóm in phôi BHYT của dịch vụ đó làm nguồn dữ liệu cập nhật cho các Dịch vụ đang chọn khác");
                    return;
                }
                List<int> lstIdDichvu = grdList.GetCheckedRows().Select(c => Utility.Int32Dbnull(c.Cells["id_dichvu"].Value)).ToList<int>();

                string nhom_inphoiBHYT = Utility.sDbnull(grdList.CurrentRow.Cells["nhom_inphoiBHYT"].Value);
                num = new Update(DmucDichvucl.Schema)
                .Set(DmucDichvucl.Columns.NhomInphoiBHYT).EqualTo(nhom_inphoiBHYT)
                .Where(DmucDichvucl.Columns.IdDichvu).In(lstIdDichvu)
                .Execute();
                if (num > 0)
                {
                    // Cập nhật cột nhom_baocao
                    foreach (var row in grdList.GetCheckedRows())
                    {
                        // Nếu dữ liệu gốc từ DataRow:
                        var drv = row.DataRow as DataRowView;
                        if (drv != null)
                        {
                            drv["nhom_inphoiBHYT"] = nhom_inphoiBHYT;
                        }
                        else
                        {
                            // Trong trường hợp grid bind trực tiếp với object model:
                            row.BeginEdit();
                            row.Cells["nhom_inphoiBHYT"].Value = nhom_inphoiBHYT;
                            row.EndEdit();
                        }
                    }
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Nhóm in phôi BHYT, Biên lai, Bảng kê chi phí KCB cho các dịch vụ {0} về giá trị {1} thành công ", lstIdDichvu.ToString(), nhom_inphoiBHYT), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnu_capnhat_phieu_emr_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Cần chọn một Dịch vụ trên lưới để lấy Mã phiếu EMR của dịch vụ đó làm nguồn dữ liệu cập nhật cho các Dịch vụ đang chọn khác");
                    return;
                }
                List<int> lstIdDichvu = grdList.GetCheckedRows().Select(c => Utility.Int32Dbnull(c.Cells["id_dichvu"].Value)).ToList<int>();

                string ma_phieu_emr = Utility.sDbnull(grdList.CurrentRow.Cells["ma_phieu_emr"].Value);
                num = new Update(DmucDichvucl.Schema)
                .Set(DmucDichvucl.Columns.MaPhieuEmr).EqualTo(ma_phieu_emr)
                .Where(DmucDichvucl.Columns.IdDichvu).In(lstIdDichvu)
                .Execute();
                if (num > 0)
                {
                    // Cập nhật cột nhom_baocao
                    foreach (var row in grdList.GetCheckedRows())
                    {
                        // Nếu dữ liệu gốc từ DataRow:
                        var drv = row.DataRow as DataRowView;
                        if (drv != null)
                        {
                            drv["ma_phieu_emr"] = ma_phieu_emr;
                        }
                        else
                        {
                            // Trong trường hợp grid bind trực tiếp với object model:
                            row.BeginEdit();
                            row.Cells["ma_phieu_emr"].Value = ma_phieu_emr;
                            row.EndEdit();
                        }
                    }
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Mã phiếu EMR cho các dịch vụ {0} về giá trị {1} thành công ", lstIdDichvu.ToString(), ma_phieu_emr), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
