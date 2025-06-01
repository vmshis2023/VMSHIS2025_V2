using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_AdTPhieuCapphatNoitruCT : Form
    {
        public DataTable p_phieuCapPhatThuoc;
        public string madoituong { get; set; }
        public int id_khoXuat { get; set; }
        public bool IsPhieuBoSung { get; set; }
        public int idKhoaLinh { get; set; }
        public string loaiphieu { get; set; }

        public frm_AdTPhieuCapphatNoitruCT()
        {
            InitializeComponent();
            CauHinh();
        }

        private void CauHinh()
        {
            //globalVariables._HisDuocCapphatnoitruProperties = Utility.GetHISDuocCapPhatNoiTru();
            //if (globalVariables._HisDuocCapphatnoitruProperties != null)
            //{
            //    chkLuuVaIn.Checked = PropertyLib._HisDuocCapphatnoitruProperties.;
            //}
        }

        /// <summary>
        ///     hàm thực hiện việc thoát Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     hàm thực hiện việc in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieu_Click(object sender, EventArgs e)
        {
            int idcapphat = Utility.Int32Dbnull(txtID_CAPPHAT.Text);
            DataTable dataTable = SPs.InphieuDenghiCapphatthuocNoitru(idcapphat).GetDataSet().Tables[0];
            TPhieuCapphatNoitru objTPhieuCapphatNoitru = TPhieuCapphatNoitru.FetchByID(idcapphat);
            if (objTPhieuCapphatNoitru != null)
            {
                DUOC_Noitru.InPhieuTongHopLinhThuocVT(objTPhieuCapphatNoitru, dataTable);
            }
        }

        /// <summary>
        ///     hàm thực hiện việc lưu lại thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            TPhieuCapphatNoitru objPhieuCapphat = CreatePhieuCapPhat();
            ActionResult actionResult =
                new CapphatThuocKhoa().UpdatePhieuCapPhat(objPhieuCapphat,
                    CreatePhieuCapPhatCT(), Utility.Int32Dbnull(txtId_KhoXuat.Text));
            switch (actionResult)
            {
                case ActionResult.Success:
                    if (me_Action == action.Insert) me_Action = action.Update;
                    txtID_CAPPHAT.Text = Utility.sDbnull(objPhieuCapphat.IdCapphat);
                    AdTPhieuCapphatNoitru();
                    Utility.set(lblMessage, "Bạn lưu thành công", true);
                    if (chkLuuVaIn.Checked) cmdInPhieu.PerformClick();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình lưu phiếu cấp phát", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
        }

        private void AdTPhieuCapphatNoitru()
        {
            try
            {
                if (p_phieuCapPhatThuoc != null)
                {
                    EnumerableRowCollection<DataRow> query = from phieu in p_phieuCapPhatThuoc.AsEnumerable()
                        where
                            Utility.Int32Dbnull(phieu[TPhieuCapphatNoitru.Columns.IdCapphat]) ==
                            Utility.Int32Dbnull(txtID_CAPPHAT.Text)
                        select phieu;
                    if (!query.Any())
                    {
                        DataRow drv = p_phieuCapPhatThuoc.NewRow();
                        TPhieuCapphatNoitru objPhieuCapphat = TPhieuCapphatNoitru.FetchByID(Utility.Int32Dbnull(txtID_CAPPHAT.Text));
                        Utility.FromObjectToDatarow(objPhieuCapphat, ref drv);
                        if (p_phieuCapPhatThuoc.Columns.Contains("ten_KHOA_LINH"))
                            drv["ten_KHOA_LINH"] = Utility.sDbnull(txtTen_KHOA_LINH.Text);
                        if (p_phieuCapPhatThuoc.Columns.Contains("ten_kho_xuat"))
                            drv["ten_kho_xuat"] = Utility.sDbnull(txtTenKho.Text);
                        if (p_phieuCapPhatThuoc.Columns.Contains("ten_nvien"))
                            drv["ten_nvien"] = Utility.sDbnull(globalVariables.gv_sStaffName);
                        p_phieuCapPhatThuoc.Rows.Add(drv);
                    }
                    else
                    {
                        DataRow drv = query.FirstOrDefault();
                        if (drv != null)
                        {
                            drv["ID_CAPPHAT"] = Utility.Int32Dbnull(txtID_CAPPHAT.Text);

                            drv["ID_KHO_XUAT"] = Utility.Int32Dbnull(txtId_KhoXuat.Text);
                            if (p_phieuCapPhatThuoc.Columns.Contains("ten_kho_xuat"))
                                drv["ten_kho_xuat"] = Utility.sDbnull(txtTenKho.Text);
                            drv["ID_KHOA_LINH"] = Utility.Int32Dbnull(txtID_KHOA_LINH.Text);
                            if (p_phieuCapPhatThuoc.Columns.Contains("ten_KHOA_LINH"))
                                drv["ten_KHOA_LINH"] = Utility.sDbnull(txtTen_KHOA_LINH.Text);
                            drv["ID_NVIEN"] = Utility.Int32Dbnull(txtID_NVIEN.Text);
                            if (p_phieuCapPhatThuoc.Columns.Contains("ten_nvien"))
                                drv["ten_nvien"] = Utility.sDbnull(txtTen_NVIEN.Text);
                            drv["NGAY_NHAP"] = dtNgayCapPhat.Text;
                            drv["MOTA_THEM"] = Utility.sDbnull(txtMOTA_THEM.Text);
                            drv["Loai_Phieu"] = radThuoc.Checked ? "THUOC" : "VT";
                            drv["Da_CapPhat"] = chkDa_CapPhat.Checked;
                        }
                        drv.AcceptChanges();
                        p_phieuCapPhatThuoc.AcceptChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        public action me_Action = action.Update;

        private TPhieuCapphatChitiet[] CreatePhieuCapPhatCT()
        {
            try
            {
                var arrPhieuCapPhatCT = new TPhieuCapphatChitiet[grdDonThuoc.RowCount];
                int idx = 0;
                foreach (GridEXRow gridExRow in grdDonThuoc.GetDataRows())
                {
                    arrPhieuCapPhatCT[idx] = new TPhieuCapphatChitiet();
                    arrPhieuCapPhatCT[idx].IdCapphat = Utility.Int32Dbnull(txtID_CAPPHAT.Text);
                    arrPhieuCapPhatCT[idx].DaLinh = chkDa_CapPhat.Checked;
                    arrPhieuCapPhatCT[idx].IdDtri =
                        Utility.Int32Dbnull(gridExRow.Cells[TPhieuCapphatChitiet.Columns.IdDtri].Value, -1);
                    //arrPhieuCapPhatCT[idx].SoLuong = Utility.Int32Dbnull(row[TPhieuCapphatChitiet.Columns.SoLuong], -1);
                    arrPhieuCapPhatCT[idx].IdDonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuoc.Columns.PresId].Value, -1);
                    // arrPhieuCapPhatCT[idx].IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[TPhieuCapphatChitiet.Columns.IdDonthuoc].Value, -1);
                    idx++;
                }
                return arrPhieuCapPhatCT;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private TPhieuCapphatNoitru CreatePhieuCapPhat()
        {
            try
            {
                var objPhieuCapPhat = new TPhieuCapphatNoitru();

                if (me_Action == action.Update)
                {
                    objPhieuCapPhat.MarkOld();
                    objPhieuCapPhat.IsLoaded = false;
                    objPhieuCapPhat.IsNew = false;
                    objPhieuCapPhat.IdCapphat = Utility.Int32Dbnull(txtID_CAPPHAT.Text);
                    objPhieuCapPhat.NgaySua = globalVariables.SysDate;
                    objPhieuCapPhat.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    objPhieuCapPhat.NgayTao = globalVariables.SysDate;
                    objPhieuCapPhat.NguoiTao = globalVariables.UserName;
                    objPhieuCapPhat.DaCapPhat = false;
                }
                txtMOTA_THEM.Text = Utility.sDbnull(txtMOTA_THEM.Text);
                objPhieuCapPhat.MaDoiTuong = Utility.sDbnull(txtMaDoiTuong.Text);
                objPhieuCapPhat.IdKhoaLinh = Utility.Int16Dbnull(txtID_KHOA_LINH.Text, -1);
                objPhieuCapPhat.IdNvien = Utility.Int16Dbnull(txtID_NVIEN.Text, -1);
                objPhieuCapPhat.IdKhoXuat = Utility.Int16Dbnull(txtId_KhoXuat.Text);
                objPhieuCapPhat.LinhBSung = radLinhBoSung.Checked ? true : false;
                objPhieuCapPhat.NgayNhap = dtNgayCapPhat.Value.Date;
                objPhieuCapPhat.LoaiPhieu = Utility.sDbnull(radThuoc.Checked ? radThuoc.Tag : radLinhVTYT.Tag);
                objPhieuCapPhat.MotaThem = Utility.sDbnull(txtMOTA_THEM.Text);
                object maxvalue =
                    grdDonThuoc.GetDataRows().AsEnumerable().Max(c => c.Cells[KcbDonthuoc.Columns.PresDate].Value);
                objPhieuCapPhat.DenNgay = Convert.ToDateTime(maxvalue);
                object minvalue =
                    grdDonThuoc.GetDataRows().AsEnumerable().Min(c => c.Cells[KcbDonthuoc.Columns.PresDate].Value);
                objPhieuCapPhat.TuNgay = Convert.ToDateTime(minvalue);
                return objPhieuCapPhat;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void frm_AdTPhieuCapphatNoitruCT_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            TPhieuCapphatNoitru objPhieuCapphat = TPhieuCapphatNoitru.FetchByID(Utility.Int32Dbnull(txtID_CAPPHAT.Text));
            if (objPhieuCapphat != null)
            {
                txtID_CAPPHAT.Text = Utility.sDbnull(objPhieuCapphat.IdCapphat);
                if (!string.IsNullOrEmpty(Utility.sDbnull(objPhieuCapphat.NgayNhap)))
                    dtNgayCapPhat.Value = Convert.ToDateTime(objPhieuCapphat.NgayNhap);
                else
                {
                    dtNgayCapPhat.Value = DateTime.Now;
                }
                txtID_KHOA_LINH.Text = Utility.sDbnull(objPhieuCapphat.IdKhoaLinh);
                idKhoaLinh = Utility.Int32Dbnull(objPhieuCapphat.IdKhoaLinh);
                DmucKhoaphong objLDepartment = DmucKhoaphong.FetchByID(objPhieuCapphat.IdKhoaLinh);
                if (objLDepartment != null)
                {
                    txtTen_KHOA_LINH.Text = Utility.sDbnull(objLDepartment.DepartmentName);
                }
                //chkIsBoSung.Checked = Convert.ToBoolean(objPhieuCapphat.LinhBSung);

                IsPhieuBoSung = Convert.ToBoolean(objPhieuCapphat.loaiPhieu);
                radLinhBoSung.Checked = IsPhieuBoSung;
                loaiphieu = Utility.sDbnull(objPhieuCapphat.LoaiPhieu);
                radThuoc.Checked = loaiphieu == "THUOC";
                radLinhVTYT.Checked = loaiphieu == "VT";
                txtID_NVIEN.Text = Utility.sDbnull(objPhieuCapphat.idn);
                DmucNhanvien objStaff = DmucNhanvien.FetchByID(objPhieuCapphat.IdNvien);
                if (objStaff != null)
                {
                    txtTen_NVIEN.Text = Utility.sDbnull(objStaff.StaffName);
                }
                txtId_KhoXuat.Text = Utility.sDbnull(objPhieuCapphat.IdKhoXuat);
                id_khoXuat = Utility.Int32Dbnull(objPhieuCapphat.IdKhoXuat);
                DKho objKho = DKho.FetchByID(objPhieuCapphat.IdKhoXuat);
                if (objKho != null)
                {
                    txtTenKho.Text = Utility.sDbnull(objKho.TenKho);
                }
                madoituong = Utility.sDbnull(objPhieuCapphat.MaDoiTuong);
                txtMaDoiTuong.Text = Utility.sDbnull(madoituong);
                if (globalVariables.gv_TongHopDonThuocMaDoiTuong)
                {
                    SqlQuery sqlQuery =
                        new Select().From<LObjectType>().Where(LObjectType.Columns.ObjectTypeCode).IsEqualTo(madoituong);
                    var objectType = sqlQuery.ExecuteSingle<LObjectType>();
                    if (objectType != null) txtObjectType_Name.Text = Utility.sDbnull(objectType.ObjectTypeName);
                }
                else
                {
                    txtObjectType_Name.Text = "Tất cả";
                }
                txtMOTA_THEM.Text = Utility.sDbnull(objPhieuCapphat.MotaThem);
                me_Action = action.Update;
            }
            else
            {
                dtNgayCapPhat.Value = globalVariables.SysDate;
                txtID_KHOA_LINH.Text = Utility.sDbnull(idKhoaLinh);
                LDepartment objLDepartment = LDepartment.FetchByID(idKhoaLinh);
                if (objLDepartment != null)
                {
                    txtTen_KHOA_LINH.Text = Utility.sDbnull(objLDepartment.DepartmentName);
                }
                txtID_NVIEN.Text = Utility.sDbnull(globalVariables.gv_StaffID);
                LStaff objStaff = LStaff.FetchByID(globalVariables.gv_StaffID);
                if (objStaff != null)
                {
                    txtTen_NVIEN.Text = Utility.sDbnull(objStaff.StaffName);
                }

                txtId_KhoXuat.Text = Utility.sDbnull(id_khoXuat);
                DKho objKho = DKho.FetchByID(id_khoXuat);
                if (objKho != null)
                {
                    txtTenKho.Text = Utility.sDbnull(objKho.TenKho);
                }
                //loaiphieu = Utility.sDbnull(objPhieuCapphat.LoaiPhieu);
                radThuoc.Checked = loaiphieu == "THUOC";
                radLinhVTYT.Checked = loaiphieu == "VT";
                radLinhBoSung.Checked = IsPhieuBoSung;
                // IsPhieuBoSung = Convert.ToBoolean(objPhieuCapphat.LinhBSung);
                txtMaDoiTuong.Text = Utility.sDbnull(madoituong);
                if (globalVariables.gv_TongHopDonThuocMaDoiTuong)
                {
                    SqlQuery sqlQuery =
                        new Select().From<LObjectType>().Where(LObjectType.Columns.ObjectTypeCode).IsEqualTo(madoituong);
                    var objectType = sqlQuery.ExecuteSingle<LObjectType>();
                    if (objectType != null) txtObjectType_Name.Text = Utility.sDbnull(objectType.ObjectTypeName);
                }
                else
                {
                    txtObjectType_Name.Text = "Tất cả";
                }
            }
            LoadDonThuoc();
        }

        private DataTable m_dtPhieuLinh = new DataTable();

        /// <summary>
        ///     hàm thực hiện việc load thông tin đơn thuốc
        /// </summary>
        private void LoadDonThuoc()
        {
            LoadPhieuDonThuoc();
            LoadPhieuLinh();
            Modifycommand();
        }

        private void LoadPhieuDonThuoc()
        {
            if (!globalVariables.gv_TongHopDonThuocMaDoiTuong)
            {
                madoituong = "TATCA";
            }
            m_dtDonThuoc =
                SPs.DuocLoadThongTinDonThuocCanCapPhat(madoituong, id_khoXuat, IsPhieuBoSung, loaiphieu, idKhoaLinh,
                    me_Action == action.Insert ? 0 : 1)
                    .GetDataSet()
                    .Tables[0];

            Utility.SetDataSourceForDataGridEx(grdPres, m_dtDonThuoc, true, true, "IsChon=0", "");
            Modifycommand();
        }

        private void Modifycommand()
        {
            cmdChuyenSang.Enabled = grdPres.RowCount > 0;
            cmdHuyXacNhan.Enabled = grdDonThuoc.RowCount > 0;
            cmdSave.Enabled = grdDonThuoc.RowCount > 0;
        }

        private DataSet ds = new DataSet();

        private void LoadPhieuLinh()
        {
            int idcapphat = Utility.Int32Dbnull(txtID_CAPPHAT.Text);
            ds = SPs.DuocPhieuCapPhatByID(idcapphat).GetDataSet();
            m_dtPhieuLinh = ds.Tables[0];
            Utility.SetDataSourceForDataGridEx(grdDonThuoc, m_dtPhieuLinh, true, true, "IsChon=1", "");
            Modifycommand();
        }

        private DataTable m_dtDonThuoc = new DataTable();
        private string RowFillerPresDetail;
        private DataTable m_dtDataPresDetail = new DataTable();
        private int Pres_ID = -1;

        /// <summary>
        ///     hàm thực hiện việc thay đổi thông tin của đơn thuốc chi tiết
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdPres.CurrentRow != null && grdPres.CurrentRow.RowType == RowType.Record)
                {
                    //lblmsg.Visible = false;
                    Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.PresId));
                    // id_kho = Utility.Int32Dbnull(txtId_KhoXuat.Text);


                    GetDataPresDetail();
                }
                else
                {
                    grdPresDetail.DataSource = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void GetDataPresDetail()
        {
            int stock_id = -1;
            stock_id = Utility.Int32Dbnull(id_khoXuat);
            m_dtDataPresDetail = SPs.DuocLaythongtinDonthuocLinhthuoc(Pres_ID, stock_id).GetDataSet().Tables[0];
            if (!m_dtDataPresDetail.Columns.Contains("CHON")) m_dtDataPresDetail.Columns.Add("CHON", typeof (int));
            foreach (DataRow dr in m_dtDataPresDetail.Rows)
            {
                dr["CHON"] = 0;
            }
            m_dtDataPresDetail.AcceptChanges();
            string _rowFilter = string.Format("Pres_ID={0}", Pres_ID);
            Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDataPresDetail, false, true, "1=1",
                KcbDonthuocChitiet.Columns.ThuTuIn);
            GridEXColumn gridExColumn =
                grdPresDetail.RootTable.Columns[KcbDonthuocChitiet.Columns.ThuTuIn];
            var gridExSortKey = new GridEXSortKey(gridExColumn, SortOrder.Ascending);
            grdPresDetail.RootTable.SortKeys.Add(gridExSortKey);
            // grdPresDetail.DataSource = p_dtPresDetail.DefaultView;

            ///thực hiện việc thay đổi khi load thông tin của thành tiền
            UpdateDataWhenChanged();
            //ModifyCommnad();
        }

        private void UpdateDataWhenChanged()
        {
            try
            {
                foreach (GridEXRow gridExRow in grdPresDetail.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells["TT"].Value = Utility.DecimaltoDbnull(gridExRow.Cells["Price"].Value, 0)*
                                                  Utility.Int32Dbnull(gridExRow.Cells["Quantity"].Value, 0);
                    gridExRow.Cells["TotalSurcharge"].Value =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SurchargePrice].Value, 0)*
                        Utility.Int32Dbnull(gridExRow.Cells["Quantity"].Value, 0);
                    gridExRow.EndEdit();
                }
                grdPresDetail.UpdateData();
                m_dtDataPresDetail.AcceptChanges();


                //  sMoneyLetter.Text = _moneyByLetter.sMoneyToLetter(toolTong.Text);
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                // throw;
            }
        }

        private void frm_AdTPhieuCapphatNoitruCT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (globalVariables._HisDuocCapphatnoitruProperties != null)
            {
                globalVariables._HisDuocCapphatnoitruProperties.IsLuuVaIn = chkLuuVaIn.Checked;
                Utility.SaveHisDuocNoitruConfig(globalVariables._HisDuocCapphatnoitruProperties);
            }
        }

        /// <summary>
        ///     hàm thực hiện việc chuyên thông tin sang bên phải
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChuyenSang_Click(object sender, EventArgs e)
        {
            if (grdPres.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn chọn bản ghi cần chuyển sang", "Thông báo", MessageBoxIcon.Error);
                return;
            }
            foreach (GridEXRow gridExRow in grdPres.GetCheckedRows())
            {
                int pres_id = Utility.Int32Dbnull(gridExRow.Cells["Pres_ID"].Value, -1);
                EnumerableRowCollection<DataRow> query = from thuoc in m_dtDonThuoc.AsEnumerable()
                    where
                        Utility.Int32Dbnull(thuoc[KcbDonthuoc.Columns.PresId]) == pres_id
                    select thuoc;
                if (query.Any())
                {
                    DataRow firstrow = query.FirstOrDefault();
                    if (firstrow != null)
                    {
                        firstrow["IsChon"] = 1;
                    }
                    firstrow.AcceptChanges();
                }

                EnumerableRowCollection<DataRow> query1 = from thuoc in m_dtPhieuLinh.AsEnumerable()
                    where Utility.Int32Dbnull(thuoc[KcbDonthuoc.Columns.PresId]) == pres_id
                    select thuoc;
                if (!query1.Any())
                {
                    DataRow firstrow1 = query.FirstOrDefault();
                    if (firstrow1 != null)
                    {
                        DataRow dr = m_dtPhieuLinh.NewRow();
                        dr["IsChon"] = 1;
                        dr["Pres_ID"] = Utility.Int32Dbnull(firstrow1["Pres_ID"]);
                        if (m_dtPhieuLinh.Columns.Contains("ID_DTRI"))
                            dr["ID_DTRI"] = Utility.Int32Dbnull(firstrow1["Treat_ID"]);
                        if (m_dtPhieuLinh.Columns.Contains("Treat_ID"))
                            dr["Treat_ID"] = Utility.Int32Dbnull(firstrow1["Treat_ID"]);
                        dr["Patient_Name"] = Utility.sDbnull(firstrow1["Patient_Name"]);
                        dr["Year_Of_Birth"] = Utility.Int32Dbnull(firstrow1["Year_Of_Birth"]);
                        dr["Patient_Code"] = Utility.sDbnull(firstrow1["Patient_Code"]);
                        dr["Patient_Addr"] = Utility.sDbnull(firstrow1["Patient_Addr"]);
                        dr["Patient_ID"] = Utility.Int32Dbnull(firstrow1["Patient_ID"]);
                        if (m_dtPhieuLinh.Columns.Contains("Pres_Date"))
                            dr["Pres_Date"] = Convert.ToDateTime(firstrow1["Pres_Date"]);
                        if (m_dtPhieuLinh.Columns.Contains("Pres_Name"))
                            dr["Pres_Name"] = Utility.sDbnull(firstrow1["Pres_Name"]);
                        m_dtPhieuLinh.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow firstrow = query1.FirstOrDefault();
                    if (firstrow != null)
                    {
                        firstrow["IsChon"] = 1;
                    }
                    firstrow.AcceptChanges();
                }
            }

            Modifycommand();
        }

        /// <summary>
        ///     hàm thực hiện việc hủy xác nhận thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHuyXacNhan_Click(object sender, EventArgs e)
        {
            if (grdDonThuoc.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn chọn bản ghi cần chuyển sang", "Thông báo", MessageBoxIcon.Error);
                return;
            }
            foreach (GridEXRow gridExRow in grdDonThuoc.GetCheckedRows())
            {
                EnumerableRowCollection<DataRow> query = from thuoc in m_dtDonThuoc.AsEnumerable()
                    where
                        Utility.Int32Dbnull(thuoc[KcbDonthuoc.Columns.PresId]) ==
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuoc.Columns.PresId].Value)
                    select thuoc;
                if (query.Any())
                {
                    DataRow firstrow = query.FirstOrDefault();
                    if (firstrow != null)
                    {
                        firstrow["IsChon"] = 0;
                    }
                    firstrow.AcceptChanges();
                }
                query = from thuoc in m_dtPhieuLinh.AsEnumerable()
                    where
                        Utility.Int32Dbnull(thuoc[KcbDonthuoc.Columns.]) ==
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuoc.Columns.PresId].Value)
                    select thuoc;
                if (query.Any())
                {
                    DataRow firstrow = query.FirstOrDefault();
                    if (firstrow != null)
                    {
                        firstrow["IsChon"] = 0;
                    }
                    firstrow.AcceptChanges();
                }
            }

            Modifycommand();
        }

        private void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == KcbDonthuocChitiet.Columns.Quantity)
            {
                if (e.Column.Key == KcbDonthuocChitiet.Columns.Quantity)
                {
                    int id = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.));
                    int soluong = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.Quantity));
                    int soluongTraLaiCu = Utility.Int32Dbnull(e.InitialValue);
                    int soluongmoi = Utility.Int32Dbnull(e.Value);
                    if (soluongmoi <= 0)
                    {
                        Utility.ShowMsg("Số lượng phải >=0\n mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
                        e.Cancel = true;
                        e.Value = soluongTraLaiCu;
                    }
                    //  SqlQuery sqlQuery = new Select().From<TPhieuCapphatNoitru>().Where(Dph);
                    CapphatThuocKhoa.UpdateSoLuongTrenDonThuoc(id, soluongmoi);
                }
            }
        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            me_Action = action.Insert;
            txtID_CAPPHAT.Clear();
            getData();
        }
    }
}
