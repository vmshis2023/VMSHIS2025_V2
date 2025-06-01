using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using System.Collections.Generic;

namespace VNS.HIS.UI.HinhAnh
{
    public partial class frm_DanhMucVungKhaoSat : Form
    {
        #region Fiels

        private DataTable _dataTable;

        string args = "All";
        #endregion

        #region Contructor

        public frm_DanhMucVungKhaoSat(string args)
        {
            InitializeComponent();
            grdMauKQ.UpdatingCell += grdMauKQ_UpdatingCell;
            grdList.UpdatingCell += grdList_UpdatingCell;
            grdList.SelectionChanged += grdList_SelectionChanged;
            this.args = args;
        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                List<String> lstFile = Utility.sDbnull(grdList.GetValue("tenfile_KQ")).Split(',').ToList<string>();
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdMauKQ.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    if (lstFile.Contains( Utility.sDbnull(gridExRow.Cells["MA"].Value)))
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

        void grdMauKQ_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "MOTA_THEM")
                {
                    string ma = grdMauKQ.CurrentRow.Cells["MA"].Value.ToString();
                    new Update(DmucChung.Schema).Set(DmucChung.Columns.MotaThem).EqualTo(Utility.sDbnull(e.Value.ToString(), ""))
                        .Where(DmucChung.Columns.Ma).IsEqualTo(ma)
                        .And(DmucChung.Columns.Loai).IsEqualTo("MAUTRAKQ")
                        .Execute();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "tenfile_KQ" )
                {
                    DmucVungkhaosat objVKS = DmucVungkhaosat.FetchByID(Utility.Int32Dbnull(grdList.GetValue("Id"), -1));
                    if (objVKS != null)
                    {
                        objVKS.MarkOld();
                        objVKS.IsNew = false;
                        objVKS.TenfileKq = Utility.sDbnull(e.Value, "");
                        objVKS.Save();
                    }
                }
                else if (e.Column.Key == "Kichthuocanh")
                {
                    DmucVungkhaosat objVKS = DmucVungkhaosat.FetchByID(Utility.Int32Dbnull(grdList.GetValue("Id"), -1));
                    if (objVKS != null)
                    {
                        objVKS.MarkOld();
                        objVKS.IsNew = false;
                        objVKS.Kichthuocanh = Utility.sDbnull(e.Value, "");
                        objVKS.Save();
                    }
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

      

        #endregion

        #region Private method

        private DataTable GetAll()
        {
            try
            {
                _dataTable = SPs.ClsCdhaGetvungks(globalVariables.IsAdmin ? "ALL" : this.args).GetDataSet().Tables[0];// new Select().From(DmucVungkhaosat.Schema).ExecuteDataSet().Tables[0];
                return _dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Form Events

        /// <summary>
        /// Xử lý sự kiện formload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_DanhMucDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                grdList.DataSource = GetAll();
                grdMauKQ.DataSource = new Select().From(DmucChung.Schema).Where(DmucChung.LoaiColumn).IsEqualTo("MAUTRAKQ").ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                
            }
           

        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            if (grdList.GetDataRows().Length<=0 || grdList.CurrentRow == null || !grdList.CurrentRow.RowType.Equals(RowType.Record)) return;
            var obj = new DmucVungkhaosat(grdList.GetValue(DmucVungkhaosat.Columns.Id));
            var f = new frm_themmoi_vungkhaosat(this.args) {m_enAct  = action.Update, Obj = obj, Table = _dataTable};
            f.ShowDialog();
        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            var obj = new DmucVungkhaosat {NgayTao = DateTime.Now};
            var f = new frm_themmoi_vungkhaosat(this.args) { m_enAct = action.Insert, Table = _dataTable, Obj = obj };
            f.ShowDialog();
        }
        bool isValidbeforeDelete(string id_vungks,string tenvungks)
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
                    if (grdList.GetCheckedRows().Count() > 0)
                    {

                        foreach (GridEXRow row in grdList.GetCheckedRows())
                        {
                            var keyId = (int)row.Cells[DmucVungkhaosat.Columns.Id].Value;
                            string tenvungks = row.Cells[DmucVungkhaosat.Columns.TenVungkhaosat].Value.ToString();
                            if (!isValidbeforeDelete(keyId.ToString(), tenvungks)) continue;
                            else
                            {
                                DataRow dr = (from datarow in _dataTable.AsEnumerable()
                                              where datarow.Field<int>(DmucVungkhaosat.Columns.Id) == keyId
                                              select datarow).First();
                                DmucVungkhaosat.Delete(keyId);
                                if (dr != null) _dataTable.Rows.Remove(dr);
                            }
                        }
                    }
                    else
                    {
                        if (grdList.CurrentRow == null) return;
                        if (!grdList.CurrentRow.RowType.Equals(RowType.Record)) return;
                        var keyId = (int)grdList.GetValue(DmucVungkhaosat.Columns.Id);
                        string tenvungks = grdList.GetValue(DmucVungkhaosat.Columns.TenVungkhaosat).ToString();
                        if (!isValidbeforeDelete(keyId.ToString(), tenvungks)) return;
                        else
                        {
                            DataRow row = (from datarow in _dataTable.AsEnumerable()
                                           where datarow.Field<int>(DmucVungkhaosat.Columns.Id) == keyId
                                           select datarow).First();
                            DmucVungkhaosat.Delete(keyId);
                            if (row != null) _dataTable.Rows.Remove(row);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            cmdSua.PerformClick();

        }

        #endregion

        private void btnLayDuLieu_Click(object sender, EventArgs e)
        {
            grdList.DataSource = GetAll();
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dataTable != null)
                    _dataTable.DataSet.WriteXml(string.Format(@"{0}\{1}.xml", Application.StartupPath, "DMUC_VUNGKS"), XmlWriteMode.WriteSchema);
                Utility.ShowMsg("Xuất dữ liệu danh mục vùng khảo sát thành công");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog _openFd = new OpenFileDialog();
                if (_openFd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(_openFd.FileName);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DmucVungkhaosat _newItem = new DmucVungkhaosat();
                        _newItem.IsNew = true;
                        _newItem.MaLoaidvu = dr["ma_loaidvu"].ToString(); ;
                        _newItem.MaKhaosat = dr["ma_khaosat"].ToString();
                        _newItem.TenVungkhaosat = dr["ten_vungkhaosat"].ToString();
                        _newItem.NguoiTao = globalVariables.UserName;
                        _newItem.NgayTao = DateTime.Now;
                        _newItem.TenfileKq = "mauchung.doc";
                        _newItem.Mota = dr["mota"].ToString();
                        _newItem.MotaHtml = dr["mota_Html"].ToString();
                        _newItem.KetLuan = dr["ket_luan"].ToString();
                        _newItem.DeNghi = dr["de_nghi"].ToString();
                        _newItem.TrangThai = true;
                        _newItem.Kichthuocanh = "100x100";
                        _newItem.Save();

                    }
                    Utility.ShowMsg("Cập nhật danh mục thành công");
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdCapnhatMauKS_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn gắn các mẫu phiếu kết quả đang chọn cho các vùng khảo sát đang chọn hay không?", "Xác nhận gắn mẫu phiếu cho các vùng khảo sát", true))
                {
                    foreach (GridEXRow _rowVKS in grdList.GetCheckedRows())
                    {
                        int _id = Utility.Int32Dbnull(_rowVKS.Cells["id"].Value, -1);
                        string mautraKQ = string.Join(",", (from p in grdMauKQ.GetCheckedRows() select p.Cells["MA"].Value.ToString()).ToArray<string>());
                        new Update(DmucVungkhaosat.Schema).Set(DmucVungkhaosat.Columns.TenfileKq).EqualTo(mautraKQ).Where(DmucVungkhaosat.Columns.Id).IsEqualTo(_id).Execute();
                    }
                    Utility.ShowMsg("Cập nhật mẫu trả kết quả cho Vùng khảo sát thành công. Nhấn OK để kết thúc");
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

       
        
    }
}