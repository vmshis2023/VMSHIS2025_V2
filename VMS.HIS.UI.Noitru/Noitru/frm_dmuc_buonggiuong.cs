using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using System.Linq;
using System.IO;
using System.Transactions;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_dmuc_buonggiuong : Form
    {
        private bool bHasloaded;
        private DataTable m_dtDataGiuong = new DataTable();
        private DataTable m_dtKhoaNoiTru = new DataTable();
        public frm_dmuc_buonggiuong()
        {
            InitializeComponent();
            InitEvents();
            Utility.VisiableGridEx(grdBed,NoitruDmucGiuongbenh.Columns.IdGiuong,globalVariables.IsAdmin);
            Utility.VisiableGridEx(grdRoom,NoitruDmucBuong.Columns.IdBuong,globalVariables.IsAdmin);
        }
        void InitEvents()
        {
            grdRoom.UpdatingCell += new UpdatingCellEventHandler(grdRoom_UpdatingCell);
            grdBed.UpdatingCell += new UpdatingCellEventHandler(grdBed_UpdatingCell);
            this.grdBed.SelectionChanged += this.grdBed_SelectionChanged;
            this.grdBed.ApplyingFilter += this.grdBed_ApplyingFilter;
            cmdAccept.Click += cmdAccept_Click;
        }

        void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Utility.isValidGrid(grdBed))
                //{
                //    Utility.ShowMsg("Bạn cần chọn vào dòng dữ liêu giường để thực hiện câp nhật giá thay vì chọn vào Phòng(buồng)");
                //    return;
                //}
                bool autocheck = false;
                NoitruQheGiuongGiumCollection lstQhe = new NoitruQheGiuongGiumCollection();
                if (grdBed.GetCheckedRows().Count() <= 0)
                {
                    grdBed.CurrentRow.BeginEdit();
                    grdBed.CurrentRow.IsChecked = true;
                    grdBed.CurrentRow.EndEdit();
                    autocheck = true;
                }
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        foreach (GridEXRow _row in grdBed.GetCheckedRows())
                        {
                            Int16 IdGiuong = Utility.Int16Dbnull(_row.Cells[NoitruQheGiuongGium.Columns.IdGiuong].Value, 0);
                            foreach (GridEXRow _gia in grdGiaGiuong.GetCheckedRows())
                            {
                                NoitruQheGiuongGium _objnew = new NoitruQheGiuongGium();
                                _objnew.IdGia = Utility.Int16Dbnull(_gia.Cells[NoitruQheGiuongGium.Columns.IdGia].Value, 0);
                                _objnew.IdGiuong = IdGiuong;
                                lstQhe.Add(_objnew);
                            }
                            new Delete().From(NoitruQheGiuongGium.Schema).Where(NoitruQheGiuongGium.Columns.IdGiuong).IsEqualTo(IdGiuong).Execute();
                        }
                        lstQhe.SaveAll();
                        scope.Complete();
                    }//end of SharedDbConnectionScope
                }//end of TransactionScope
                if (autocheck)
                {
                    grdBed.CurrentRow.BeginEdit();
                    grdBed.CurrentRow.IsChecked = false;
                    grdBed.CurrentRow.EndEdit();
                }
                Utility.ShowMsg("Đã cập nhật quan hệ Giường-Loại giường thành công");
            }
            catch (Exception ex)
            {

            }
        }

        void grdBed_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (!bHasloaded) return;
                if (!Utility.isValidGrid(grdBed)) return;
                if (e.Column.Key == "ten_giuong" && Utility.sDbnull(e.Value.ToString()).Length > 0)
                {
                    NoitruDmucGiuongbenh _obj = NoitruDmucGiuongbenh.FetchByID(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.IdGiuong)));
                    _obj.TenGiuong = e.Value.ToString();
                    _obj.IsNew = false;
                    _obj.MarkOld();
                    _obj.Save();
                }

                //if (e.InitialValue.ToString() != e.Value.ToString())
                //{
                //    NoitruDmucGiuongbenh _obj = NoitruDmucGiuongbenh.FetchByID(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.IdGiuong)));
                //    _obj.MaGiuong = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.MaGiuong.ToUpper() ? e.Value.ToString() : Utility.sDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.MaGiuong), "");
                //    _obj.TenGiuong = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.TenGiuong.ToUpper() ? e.Value.ToString() : Utility.sDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.TenGiuong), "");
                //    _obj.GiaDichvu = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.GiaDichvu.ToUpper() ? (decimal)e.Value : Utility.DecimaltoDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.GiaDichvu), 0);
                //    _obj.GiaBhyt = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.GiaBhyt.ToUpper() ? (decimal)e.Value : Utility.DecimaltoDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.GiaBhyt), 0);
                //    _obj.GiaKhac = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.GiaKhac.ToUpper() ? (decimal)e.Value : Utility.DecimaltoDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.GiaKhac), 0);
                //    _obj.PhuthuDungtuyen = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.PhuthuDungtuyen.ToUpper() ? (decimal)e.Value : Utility.DecimaltoDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.PhuthuDungtuyen), 0);
                //    _obj.PhuthuTraituyen = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.PhuthuTraituyen.ToUpper() ? (decimal)e.Value : Utility.DecimaltoDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.PhuthuTraituyen), 0);

                //    _obj.SonguoiToida = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.SonguoiToida.ToUpper() ? (byte)e.Value : Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.SonguoiToida), 1);
                //    _obj.MotaThem = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.MotaThem.ToUpper() ? e.Value.ToString() : Utility.sDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.MotaThem), "");
                //    _obj.TrangThai = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.TrangThai.ToUpper() ? (byte)e.Value : Utility.ByteDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.TrangThai), 1);
                //    _obj.TthaiTunguyen = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.TthaiTunguyen.ToUpper() ? (byte)e.Value : Utility.ByteDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.TthaiTunguyen), 0);
                //    _obj.DangSudung = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.DangSudung.ToUpper() ? (byte)e.Value : Utility.ByteDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.DangSudung), 1);
                //    _obj.SttHthi = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.SttHthi.ToUpper() ? (Int16)e.Value : Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.SttHthi), 1);
                //    _obj.TenBhyt = e.Column.DataMember.ToUpper() == NoitruDmucGiuongbenh.Columns.TenBhyt.ToUpper() ? e.Value.ToString() : Utility.sDbnull(Utility.GetValueFromGridColumn(grdBed, NoitruDmucGiuongbenh.Columns.TenBhyt), "");
                //    _obj.IsNew = false;
                //    _obj.MarkOld();
                //    SqlQuery sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
                //        .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(_obj.IdBuong)
                //        .And(NoitruDmucGiuongbenh.Columns.MaGiuong).IsEqualTo(_obj.MaGiuong)
                //        .And(NoitruDmucGiuongbenh.Columns.IdGiuong).IsNotEqualTo(_obj.IdGiuong);
                //    if (sqlQuery.GetRecordCount() > 0)
                //    {
                //        Utility.ShowMsg("Mã giường đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                //        return;
                //    }
                //    _obj.Save();
                //}
            }
            catch (Exception ex)
            {
                
                
            }
        }

        void grdRoom_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (!bHasloaded) return;
                if (!Utility.isValidGrid(grdRoom)) return;
                //if (e.InitialValue.ToString() != e.Value.ToString())
                //{
                //    NoitruDmucBuong _obj = NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdRoom, NoitruDmucBuong.Columns.IdBuong)));
                //    _obj.MaBuong = e.Column.DataMember.ToUpper() == NoitruDmucBuong.Columns.MaBuong.ToUpper() ? e.Value.ToString() : Utility.sDbnull(Utility.GetValueFromGridColumn(grdRoom, NoitruDmucBuong.Columns.MaBuong), "");
                //    _obj.TenBuong = e.Column.DataMember.ToUpper() == NoitruDmucBuong.Columns.TenBuong.ToUpper() ? e.Value.ToString() : Utility.sDbnull(Utility.GetValueFromGridColumn(grdRoom, NoitruDmucBuong.Columns.TenBuong), "");
                //    _obj.DonGia = e.Column.DataMember.ToUpper() == NoitruDmucBuong.Columns.DonGia.ToUpper() ? (int)e.Value : Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdRoom, NoitruDmucBuong.Columns.DonGia), 0);
                //    _obj.MotaThem = e.Column.DataMember.ToUpper() == NoitruDmucBuong.Columns.MotaThem.ToUpper() ? e.Value.ToString() : Utility.sDbnull(Utility.GetValueFromGridColumn(grdRoom, NoitruDmucBuong.Columns.MotaThem), "");
                //    _obj.TrangThai = e.Column.DataMember.ToUpper() == NoitruDmucBuong.Columns.TrangThai.ToUpper() ? (byte)e.Value : Utility.ByteDbnull(Utility.GetValueFromGridColumn(grdRoom, NoitruDmucBuong.Columns.TrangThai), 1);
                //    _obj.SttHthi = e.Column.DataMember.ToUpper() == NoitruDmucBuong.Columns.SttHthi.ToUpper() ? (Int16)e.Value : Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdRoom, NoitruDmucBuong.Columns.SttHthi), 1);
                //    _obj.IsNew = false;
                //    _obj.MarkOld();
                //    _obj.Save();
                //}
                if (e.Column.Key=="ten_buong" && Utility.sDbnull( e.Value.ToString()).Length>0)
                {
                    NoitruDmucBuong _obj = NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdRoom, NoitruDmucBuong.Columns.IdBuong)));
                    _obj.TenBuong = e.Value.ToString();
                    _obj.IsNew = false;
                    _obj.MarkOld();
                    _obj.Save();
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }

       
        private void ModifyCommand()
        {
            if (grdRoom.CurrentRow != null && grdRoom.CurrentRow.RowType == RowType.Record)
            {
                cmdSua.Enabled = cmdXoa.Enabled = grdRoom.RowCount > 0 && grdRoom.CurrentRow.RowType == RowType.Record;
            }
            else
            {
                cmdSua.Enabled = cmdXoa.Enabled = false;
            }
            if (grdBed.CurrentRow != null && grdBed.CurrentRow.RowType == RowType.Record)
            {
                cmdSuaGiuong.Enabled =
                    cmdXoaGiuong.Enabled = grdBed.RowCount > 0 && grdBed.CurrentRow.RowType == RowType.Record;
            }
            else
            {
                cmdSuaGiuong.Enabled = cmdXoaGiuong.Enabled = false;
            }

            //  cmdSua.Enabled = cmdXoa.Enabled = grdRoom.RowCount > 0 && grdRoom.CurrentRow.RowType == RowType.Record;
        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            var objRoom = new NoitruDmucBuong();
            var frm = new frm_Add_PhongNoiTru();
            frm.objRoom = objRoom;
            frm.m_enAct = action.Insert;
            frm.p_dtDataPhong = globalVariables.gv_PhongNoitru;
            frm.MyGetData=new frm_Add_PhongNoiTru.timkiem(LoadData);
            frm.grdList = grdRoom;
            frm.ShowDialog();
            ModifyCommand();
        }

        private void cmdSuaGiuong_Click(object sender, EventArgs e)
        {
            int idphong = Utility.Int32Dbnull(grdBed.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong));
            NoitruDmucGiuongbenh objBed = NoitruDmucGiuongbenh.FetchByID(idphong);
            if (objBed != null)
            {
                var frm = new frm_Add_GiuongNoiTru();
                frm.objBed = objBed;
                frm.m_enAct = action.Update;
                frm.p_dtDataGiuong = m_dtDataGiuong;
                frm.grdList = grdBed;
                frm.MyGetData = new frm_Add_GiuongNoiTru.timkiem(LoadData);
                frm.ShowDialog();
            }
            ModifyCommand();
        }

        private void cmdThemGiuong_Click(object sender, EventArgs e)
        {
            var objBed = new NoitruDmucGiuongbenh();
            var frm = new frm_Add_GiuongNoiTru();
            frm.objBed = objBed;
            frm.m_enAct = action.Insert;
            frm.p_dtDataGiuong = m_dtDataGiuong;
            frm.grdList = grdBed;
            frm.MyGetData = new frm_Add_GiuongNoiTru.timkiem(LoadData);
            frm.ShowDialog();
            ModifyCommand();
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            int idphong = Utility.Int32Dbnull(grdRoom.GetValue(NoitruDmucBuong.Columns.IdBuong));
            NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(idphong);
            if (objRoom != null)
            {
                var frm = new frm_Add_PhongNoiTru();
                frm.objRoom = objRoom;
                frm.m_enAct = action.Update;
                frm.p_dtDataPhong = globalVariables.gv_PhongNoitru;
                frm.grdList = grdRoom;
                frm.MyGetData = new frm_Add_PhongNoiTru.timkiem(LoadData);
                frm.ShowDialog();
            }
            ModifyCommand();
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            if (grdRoom.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxIcon.Error);
                return;
            }
            foreach (GridEXRow gridExRow in grdRoom.GetCheckedRows())
            {
                int idphong = Utility.Int32Dbnull(gridExRow.Cells[NoitruDmucBuong.Columns.IdBuong].Value);
                string tenbuong = Utility.sDbnull(gridExRow.Cells[NoitruDmucBuong.Columns.TenBuong].Value);
                SqlQuery sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
                    .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(idphong);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Phòng nội trú " + tenbuong + " tồn tại giường, Bạn không thể xóa\n Mời bạn xem lại", "Thông báo");
                    tabBuonGiuong.SelectedTab = tabPageGiuong;
                    return;
                }
                sqlQuery = new Select().From(NoitruPhanbuonggiuong.Schema)
                  .Where(NoitruPhanbuonggiuong.Columns.IdBuong).IsEqualTo(idphong);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Phòng nội trú " + tenbuong + " đã được sử dụng trong quá trình phân buồng giường, Bạn không thể xóa\n Mời bạn xem lại", "Thông báo");
                    return;
                }

            }
            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin bản ghi đang chọn không ?", "Thông báo", true))
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        foreach (GridEXRow gridExRow in grdRoom.GetCheckedRows())
                        {
                            int idphong = Utility.Int32Dbnull(gridExRow.Cells[NoitruDmucBuong.Columns.IdBuong].Value);
                            new Delete().From(NoitruDmucBuong.Schema).Where(NoitruDmucBuong.Columns.IdBuong).IsEqualTo(idphong).Execute();
                            gridExRow.Delete();
                        }
                        scope.Complete();
                    }//end of SharedDbConnectionScope
                }//end of TransactionScope
                grdRoom.UpdateData();
                globalVariables.gv_PhongNoitru.AcceptChanges();
            }
            ModifyCommand();
        }

        private void frm_dmuc_buonggiuong_Load(object sender, EventArgs e)
        {
            LoadData();
            ModifyCommand();
            
        }

        private void LoadData()
        {
            bHasloaded = false;
            LoadPhongNoiTru();
            SearchGiuong();
            DataTable dtLoaiGiuong = SPs.DmucLaydanhmucGiabuonggiuong("-1").GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdGiaGiuong, dtLoaiGiuong, true, true, "1=1", NoitruGiabuonggiuong.Columns.SttHthi + "," + NoitruGiabuonggiuong.Columns.TenGia);
            bHasloaded = true;
        }

        private void LoadPhongNoiTru()
        {
            globalVariables.gv_PhongNoitru = SPs.NoitruTimkiembuongTheokhoa(-1).GetDataSet().Tables[0];

            Utility.SetDataSourceForDataGridEx(grdRoom, globalVariables.gv_PhongNoitru, true, true, "1=1", NoitruDmucBuong.Columns.SttHthi);
            globalVariables.gv_KhoaNoitru =THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0);
            m_dtKhoaNoiTru = globalVariables.gv_KhoaNoitru.Copy();
            DataBinding.BindDataCombobox(cboKhoaNoiTru, m_dtKhoaNoiTru, DmucKhoaphong.Columns.IdKhoaphong,
                                       DmucKhoaphong.Columns.TenKhoaphong, "---Khoa nội trú---",true);
        }

        private void SearchGiuong()
        {
            m_dtDataGiuong = SPs.NoitruTimkiemGiuong(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue, -1),
                                                     Utility.Int32Dbnull(cboPhongNoiTru.SelectedValue, -1),
                                                     txtBedCode.Text, txtBed_Name.Text).GetDataSet().Tables[0];
            
            Utility.SetDataSourceForDataGridEx(grdBed, m_dtDataGiuong, true, true, "1=1", NoitruDmucGiuongbenh.Columns.SttHthi);
            ModifyCommand();
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSearchGiuong_Click(object sender, EventArgs e)
        {
            SearchGiuong();
        }

        private void cmdXoaGiuong_Click(object sender, EventArgs e)
        {
            if (grdBed.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxIcon.Error);
                return;
            }
            foreach (GridEXRow gridExRow in grdBed.GetCheckedRows())
            {
                int idgiuong = Utility.Int32Dbnull(gridExRow.Cells[NoitruDmucGiuongbenh.Columns.IdGiuong].Value);
                string TenGiuong = Utility.sDbnull(gridExRow.Cells[NoitruDmucGiuongbenh.Columns.TenGiuong].Value);

                SqlQuery sqlQuery = new Select().From(NoitruPhanbuonggiuong.Schema)
                  .Where(NoitruPhanbuonggiuong.Columns.IdGiuong).IsEqualTo(idgiuong);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Giường nội trú " + TenGiuong + " đã được sử dụng trong quá trình phân buồng giường, Bạn không thể xóa\n Mời bạn bỏ chọn giường đó khỏi danh sách cần xóa", "Thông báo");
                    return;
                }

            }
            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin bản ghi đang chọn không ?", "Thông báo", true))
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        foreach (GridEXRow gridExRow in grdBed.GetCheckedRows())
                        {
                            short idphong = Utility.Int16Dbnull(gridExRow.Cells[NoitruDmucGiuongbenh.Columns.IdGiuong].Value);
                            NoitruDmucGiuongbenh.Delete(idphong);
                            new Delete().From(NoitruQheGiuongGium.Schema).Where(NoitruQheGiuongGium.Columns.IdGiuong).IsEqualTo(idphong).Execute();
                            gridExRow.Delete();
                        }
                        scope.Complete();
                    }//end of SharedDbConnectionScope
                }//end of TransactionScope
                grdBed.UpdateData();
                m_dtDataGiuong.AcceptChanges();
            }
            ModifyCommand();
        }

        private void grdRoom_SelectionChanged(object sender, EventArgs e)
        {
            if (!bHasloaded) return;
            ModifyCommand();
        }

        private void grdRoom_ApplyingFilter(object sender, CancelEventArgs e)
        {
            if (!bHasloaded) return;
            ModifyCommand();
        }

        private void grdBed_ApplyingFilter(object sender, CancelEventArgs e)
        {
            if (!bHasloaded) return;
            ModifyCommand();
        }

        private void grdBed_SelectionChanged(object sender, EventArgs e)
        {
            if (!bHasloaded) return;
            ModifyCommand();
            LoadQheGiuong_LoaiGiuong();
        }
        private void LoadQheGiuong_LoaiGiuong()
        {
            try
            {
                if (!Utility.isValidGrid(grdBed))
                {
                    grdGiaGiuong.UnCheckAllRecords();
                    return;
                }
                Int16 bedId = Utility.Int16Dbnull(Utility.getValueOfGridCell(grdBed, NoitruQheGiuongGium.Columns.IdGiuong), -1);
                NoitruQheGiuongGiumCollection lstQhe = new Select().From(NoitruQheGiuongGium.Schema)
                .Where(NoitruQheGiuongGium.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(bedId)).
                ExecuteAsCollection<NoitruQheGiuongGiumCollection>();
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdGiaGiuong.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    var query = from kho in lstQhe.AsEnumerable()
                                where kho.IdGia == Utility.Int32Dbnull(gridExRow.Cells[NoitruGiabuonggiuong.Columns.IdGia].Value)
                                select kho;
                    if (query.Count() > 0)
                    {
                        gridExRow.IsChecked = true;
                    }

                    else
                    {
                        gridExRow.IsChecked = false;
                    }
                    gridExRow.EndEdit();

                }
            }
            catch (Exception ex)
            {


            }

        }
        private void cboKhoaNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bHasloaded)
            {
                SqlQuery sqlQuery = new Select().From(NoitruDmucBuong.Schema)
                    .Where(NoitruDmucBuong.Columns.IdKhoanoitru).IsEqualTo(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue));
                DataBinding.BindDataCombobox(cboPhongNoiTru, sqlQuery.ExecuteDataSet().Tables[0], NoitruDmucBuong.Columns.IdBuong,
                                           NoitruDmucBuong.Columns.TenBuong, "Chọn phòng nội trú",true);
                // IntialDataControl();
            }
        }

        private void cmdThoatGiuong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_dmuc_buonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.N)
            {
                if (tabBuonGiuong.SelectedTab == tabPagePhong)
                {
                    cmdThemMoi.PerformClick();
                }
                if (tabBuonGiuong.SelectedTab == tabPageGiuong)
                {
                    cmdThemGiuong.PerformClick();
                }
            }
            if (e.Control && e.KeyCode == Keys.E)
            {
                if (tabBuonGiuong.SelectedTab == tabPagePhong)
                {
                    cmdSua.PerformClick();
                }
                if (tabBuonGiuong.SelectedTab == tabPageGiuong)
                {
                    cmdSuaGiuong.PerformClick();
                }
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                if (tabBuonGiuong.SelectedTab == tabPagePhong)
                {
                    cmdXoa.PerformClick();
                }
                if (tabBuonGiuong.SelectedTab == tabPageGiuong)
                {
                    cmdXoaGiuong.PerformClick();
                }
            }
            if (e.KeyCode == Keys.F3) cmdSearchGiuong.PerformClick();
            if (e.KeyCode == Keys.F5)
            {
                if (tabBuonGiuong.SelectedTab == tabPagePhong)
                {
                    LoadPhongNoiTru();
                }
                if (tabBuonGiuong.SelectedTab == tabPageGiuong)
                {
                    SearchGiuong();
                }
            }
        }

        private void cmdAccept_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            try
            {
                 SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                saveFileDialog1.FileName = "dmucgiuong.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        gridEXExporter1.GridEX = grdBed;
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