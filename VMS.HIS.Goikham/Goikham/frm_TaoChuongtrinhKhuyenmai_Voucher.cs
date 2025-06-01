using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.BusRule.Goikham;


namespace VNS.HIS.UI.GOIKHAM
{
    public partial class frm_TaoChuongtrinhKhuyenmai_Voucher : Form
    {
        public action CS_Action = action.Insert;
        public DataTable m_dtChitietgoi;
        public DataTable DtGoiKham;
        public int IdGoiDVu;
        private clsGoikham _goiKhamService;
        private DataSet _dsDanhMucDichVu;
        private string _rowFilter = "1=1";
        private GridEX _currentGrid;
        private bool _isSaved;
        public bool BCancel;
        bool Add_nhanh = false;//true= khi gõ enter tại các text nhóm thuốc, vtth, cls thì thêm ngay vào chi tiết. false = cần dùng nút mũi tên
        public frm_TaoChuongtrinhKhuyenmai_Voucher()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtpHieuLucTuNgay.Value =  DateTime.Now;
            dtpHieuLucDenNgay.Value = DateTime.Now.AddYears(1);
            _goiKhamService = new clsGoikham();
            m_dtChitietgoi = new DataTable();
            m_dtChitietgoi.Columns.Add("id_chitiet");
            m_dtChitietgoi.Columns.Add("id_chitietdichvu");
            m_dtChitietgoi.Columns.Add("ten_dichvu");
            m_dtChitietgoi.Columns.Add("ten_chitietdichvu");
            m_dtChitietgoi.Columns.Add("loai_dvu");
            m_dtChitietgoi.Columns.Add("Ten_Loai_DVU");
            m_dtChitietgoi.Columns.Add("GIA_TRONG_GOI");
            m_dtChitietgoi.Columns.Add("SO_LUONG");
            m_dtChitietgoi.Columns.Add("id_dichvu");
            m_dtChitietgoi.Columns.Add("don_gia");
            m_dtChitietgoi.Columns.Add("thanh_tien");
             txtnhomchidinh._OnEnterMe += txtnhomchidinh__OnEnterMe;
            cmdDelete.Click+=btnXoaDichVu_Click;
            txtCongkham._OnEnterMe += txtCongkham__OnEnterMe;
            txtDichvuCLS._OnEnterMe += txtCongkham__OnEnterMe;
            txtThuoc._OnEnterMe += txtCongkham__OnEnterMe;
            txtGiuong._OnEnterMe += txtCongkham__OnEnterMe;
            txtVT._OnEnterMe += txtCongkham__OnEnterMe;
            txtVT._OnSelectionChanged += txtVT__OnSelectionChanged;
            txtGiuong._OnSelectionChanged += txtVT__OnSelectionChanged;
            txtThuoc._OnSelectionChanged += txtVT__OnSelectionChanged;
            txtDichvuCLS._OnSelectionChanged += txtVT__OnSelectionChanged;
            txtCongkham._OnSelectionChanged += txtVT__OnSelectionChanged;

            autoThuoc._OnEnterMe += autoThuoc__OnEnterMe;
            autoVTTH._OnEnterMe += autoVTTH__OnEnterMe;
            autoNhomCLS._OnEnterMe += autoNhomCLS__OnEnterMe;
            cmdAccept.Click += cmdAccept_Click;
        }

        void autoNhomCLS__OnEnterMe()
        {
            if (!Add_nhanh) cmdAddCLS.Focus();
            else
                AddGroupCLS();
        }

        void autoVTTH__OnEnterMe()
        {
            if (!Add_nhanh)
            {
                cmdAddVTTH.Focus();
            }
            else
                AddGroupVTTH();
        }

        void autoThuoc__OnEnterMe()
        {
            if (!Add_nhanh) cmdAddThuoc.Focus();
            else
                AddGroupThuoc();
        }
        void AddGroupThuoc()
        {
            if (Utility.Int32Dbnull(autoThuoc.MyID, -1) > 0)
            {
              DataTable  m_dtChitiet = SPs.DmucLaychitietDonthuocmau(Utility.Int32Dbnull(autoThuoc.MyID, -1)).GetDataSet().Tables[0];
                //grdThuoc.UnCheckAllRecords();
                foreach (GridEXRow _row in grdThuoc.GetDataRows())
                {
                    if (m_dtChitiet.Select("id_chitietdichvu=" + _row.Cells["id_thuoc"].Value.ToString()).Length > 0)
                    {
                        ThemDongVaoLuoi(_row); 
                        //_row.BeginEdit();
                        //_row.IsChecked = true;
                        //_row.EndEdit();
                    }
                }
            }
        }
        void AddGroupVTTH()
        {
            if (Utility.Int32Dbnull(autoVTTH.MyID, -1) > 0)
            {
                DataTable m_dtChitiet = SPs.DmucLaychitietDonthuocmau(Utility.Int32Dbnull(autoVTTH.MyID, -1)).GetDataSet().Tables[0];
                //grdVTu.UnCheckAllRecords();
                foreach (GridEXRow _row in grdVTu.GetDataRows())
                {
                    if (m_dtChitiet.Select("id_chitietdichvu=" + _row.Cells["id_thuoc"].Value.ToString()).Length > 0)
                    {
                        ThemDongVaoLuoi(_row); 
                        //_row.BeginEdit();
                        //_row.IsChecked = true;
                        //_row.EndEdit();
                    }
                }
            }

        }
        void AddGroupCLS()
        {
            if (Utility.Int32Dbnull(autoNhomCLS.MyID, -1) > 0)
            {
                DataTable m_dtChitiet = new VNS.HIS.BusRule.Classes.KCB_CHIDINH_CANLAMSANG().DmucLaychitietNhomchidinhCls(Utility.Int32Dbnull(autoNhomCLS.MyID, -1));
                //grdCLS.UnCheckAllRecords();
                foreach (GridEXRow _row in grdCLS.GetDataRows())
                {
                    if (m_dtChitiet.Select("id_chitietdichvu=" + _row.Cells["id_chitietdichvu"].Value.ToString()).Length > 0)
                    {
                        ThemDongVaoLuoi(_row); 
                        //_row.BeginEdit();
                        //_row.IsChecked = true;
                        //_row.EndEdit();
                    }
                }
            }
        }
        void cmdAccept_Click(object sender, EventArgs e)
        {
                ThemDichVuVaoGoi(_currentGrid);
                _currentAuto.Focus();
                _currentAuto.SelectAll();
        }

        void txtVT__OnSelectionChanged()
        {
            myID = _currentAuto.MyID.ToString();
            //Utility.GotoNewRowJanus(_currentGrid, "id_chitietdichvu", _currentAuto.MyID.ToString());
            //_currentAuto.SelectAll();
        }
        VNS.HIS.UCs.AutoCompleteTextbox _currentAuto = null;
        void txtCongkham__OnEnterMe()
        {
            myID = _currentAuto.MyID.ToString();
            Utility.GotoNewRowJanus(_currentGrid, "id_chitietdichvu", _currentAuto.MyID.ToString());
            if (myID != "-1")
            {
                if (chkChiDinhNhanh.Checked)
                {
                    ThemDichVuVaoGoi(_currentGrid);
                    _currentAuto.SelectAll();
                }
                else
                    cmdAccept.Focus();
            }
        }

        private void frm_TaoChuongtrinhKhuyenmai_Voucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            else if (e.KeyCode == Keys.F2)
            {
                _currentAuto.Focus();
                _currentAuto.SelectAll();
            }
            else if (e.KeyCode == Keys.Escape) cmdThoat.PerformClick();
            else if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            else if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
            else if (e.Control && e.KeyCode == Keys.A) cmdAccept.PerformClick();
        }

        private void frm_TaoChuongtrinhKhuyenmai_Voucher_Load(object sender, EventArgs e)
        {
            try
            {
                cboLoaiChuongtrinh.SelectedIndex = 0;
                cboKieuCK.SelectedIndex = 0;
                //SetCurrencyFormat(new List<Control>() {txtThanhTien,txtThanhTienThamChieu,txtTienGoi,txtMienGiam,txtGiamtruBHYT });
                Add_nhanh = THU_VIEN_CHUNG.Laygiatrithamsohethong("GOI_THEMNHANHDVU_TUMAU","0",true)=="1";
                this.Text = CS_Action == action.Insert ? "Thêm Mới Gói Khám" : "Chỉnh sửa Gói Khám";
                txtTienGoi.LostFocus += txtTienGoi_LostFocus;
                txtMienGiam.LostFocus += txtMienGiam_LostFocus;
                txtGiamtruBHYT.LostFocus += txtGiamtruBHYT_LostFocus;

                _dsDanhMucDichVu = _goiKhamService.LayDanhMucDichVu();
                grdKham.DataSource = _dsDanhMucDichVu.Tables[0];
                grdCLS.DataSource = _dsDanhMucDichVu.Tables[1];
                grdThuoc.DataSource = _dsDanhMucDichVu.Tables[2];
                grdGiuong.DataSource = _dsDanhMucDichVu.Tables[3];
                grdVTu.DataSource = _dsDanhMucDichVu.Tables[4];
                autoNhomCLS.Init(SPs.DmucNhomcanlamsangGetdata(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin)).GetDataSet().Tables[0],
              new List<string>()
                    {
                        DmucNhomcanlamsang.Columns.Id,
                        DmucNhomcanlamsang.Columns.MaNhom,
                        DmucNhomcanlamsang.Columns.TenNhom
                    });
                DataTable dtDonThuocMau = new Select().From(DmucDonthuocmau.Schema).Where(DmucDonthuocmau.Columns.LoaiDon).IsEqualTo("THUOC").ExecuteDataSet().Tables[0];
                autoThuoc.Init(dtDonThuocMau,
                         new List<string>()
                    {
                        DmucDonthuocmau.Columns.Id,
                        DmucDonthuocmau.Columns.MaDonthuoc,
                        DmucDonthuocmau.Columns.TenDonthuoc
                    });
                dtDonThuocMau = new Select().From(DmucDonthuocmau.Schema).Where(DmucDonthuocmau.Columns.LoaiDon).IsEqualTo("VT").ExecuteDataSet().Tables[0];
                autoVTTH.Init(dtDonThuocMau,
                         new List<string>()
                    {
                        DmucDonthuocmau.Columns.Id,
                        DmucDonthuocmau.Columns.MaDonthuoc,
                        DmucDonthuocmau.Columns.TenDonthuoc
                    });
                txtCongkham.Init(_dsDanhMucDichVu.Tables[0], new List<string>() { "id_chitietdichvu", "id_chitietdichvu", "ten_chitietdichvu" });
                txtDichvuCLS.Init(_dsDanhMucDichVu.Tables[1], new List<string>() { "id_chitietdichvu", "id_chitietdichvu", "ten_chitietdichvu" });
                txtThuoc.Init(_dsDanhMucDichVu.Tables[2], new List<string>() { "id_chitietdichvu", "id_chitietdichvu", "ten_chitietdichvu" });
                txtGiuong.Init(_dsDanhMucDichVu.Tables[3], new List<string>() { "id_chitietdichvu", "id_chitietdichvu", "ten_chitietdichvu" });
                txtVT.Init(_dsDanhMucDichVu.Tables[4], new List<string>() { "id_chitietdichvu", "id_chitietdichvu", "ten_chitietdichvu" });
                AddUnsignedColumn(_dsDanhMucDichVu);
                if (!m_dtChitietgoi.Columns.Contains("IsNew"))
                {
                    m_dtChitietgoi.Columns.Add("IsNew");
                }
                grdChitiet.DataSource = m_dtChitietgoi;

                txtID.Text = "-1";
                if (CS_Action == action.Update)
                {
                    Getdata();
                    TinhThanhTien();
                }
                LoadNhomChiDinh();
                CauHinh();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                tabControl.SelectedIndex = 0;
                txtMaGoi.Focus();
            }
        }

        void txtGiamtruBHYT_LostFocus(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        void txtMienGiam_LostFocus(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        void txtTienGoi_LostFocus(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void CauHinh()
        {
            //_hisKhamProperties = Utility.GetKhamPropertiesConfig();
            //HisclsProperties = Utility.GetHisCLSPropertiesConfig();
            //  rad_TuTuc.Visible = HisclsProperties.HienthiTuTuc;
            //_hisPrintProperties = Utility.GetPrintPropertiesConfig();
            //if (_hisPrintProperties != null)
            //{
                chkChiDinhNhanh.Checked = true;// _hisPrintProperties.IsChiDinhNhanh;

           // }
        }

        private void AddUnsignedColumn(DataSet ds)
        {
            try
            {
                foreach (DataTable table in ds.Tables)
                {
                    if(!table.Columns.Contains("UnsignedServiceDetail_Name"))
                    {
                        table.Columns.Add("UnsignedServiceDetail_Name");
                    }
                    foreach (DataRow row in table.Rows)
                    {
                        row["UnsignedServiceDetail_Name"] =
                            Utility.UnSignedCharacter(Utility.sDbnull(row["ten_chitietdichvu"]));
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void Getdata()
        {
            try
            {
                txtID.Text = Utility.sDbnull(IdGoiDVu);
                var dtGoi = _goiKhamService.LayThongTinGoiKham(IdGoiDVu);
                if (dtGoi != null)
                {
                    txtMaGoi.Text = Utility.sDbnull(dtGoi.Rows[0]["ma_goi"]);
                    txtTenGoi.Text = Utility.sDbnull(dtGoi.Rows[0]["ten_goi"]);
                    txtTienGoi.Text = Utility.sDbnull(dtGoi.Rows[0]["so_tien"]);
                    txtMienGiam.Text = Utility.sDbnull(dtGoi.Rows[0]["MIEN_GIAM"]);
                    txtGiamtruBHYT.Text = Utility.sDbnull(dtGoi.Rows[0]["giam_bhyt"]);
                    cboLoaiChuongtrinh.SelectedIndex = Utility.Int32Dbnull(dtGoi.Rows[0]["loai_goi"], 1) - 1;
                    cboKieuCK.SelectedIndex = Utility.sDbnull(dtGoi.Rows[0]["kieu_chietkhau"]) == "T" ? 0 : 1;
                    chkKieuKhuyenmai.Checked = Utility.Byte2Bool(Utility.sDbnull(dtGoi.Rows[0][GoiDanhsach.Columns.KhuyenmaiTatcadvu]));
                    dtpHieuLucTuNgay.Value = Convert.ToDateTime(dtGoi.Rows[0]["HieuLuc_TuNgay"]);
                    dtpHieuLucDenNgay.Value = Convert.ToDateTime(dtGoi.Rows[0]["HieuLuc_DenNgay"]);
                    chkHieuLuc.Checked = Utility.Int32Dbnull(dtGoi.Rows[0]["trang_thai"]) == 1;
                    chkMGTong.Checked = Utility.ByteDbnull(dtGoi.Rows[0]["khuyenmai_tong"])==1 ;
                }
                else
                {
                    Utility.ShowMsg("Bạn chưa chọn thông tin cần chỉnh sủa", "Thông báo", MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void btnXoaDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                if(Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa {0} dịch vụ khỏi gói không?",grdChitiet.GetCheckedRows().Count()),"Thông báo",false))
                {
                    foreach (GridEXRow row in grdChitiet.GetCheckedRows())
                    {
                        var idChiTiet = Utility.Int32Dbnull(row.Cells["id_chitiet"].Value, -1);
                        var str = _goiKhamService.XoaDichVuTrongGoi(idChiTiet);
                        if (str == "OK")
                        {
                            row.Delete();
                            grdChitiet.UpdateData();
                            grdChitiet.Refresh();
                            m_dtChitietgoi.AcceptChanges();
                            TinhThanhTien();
                        }
                        else
                        {
                            Utility.ShowMsg(str);
                            return;
                        }
                    }
                    ModifyButtonCommand();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            BCancel = true;
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValid()) return;
                _isSaved = true;
                PerformAction();
            }
            catch (Exception ex)
            { 
                Utility.ShowMsg(ex.Message);
            }
         
        }

        private void PerformAction()
        {
            switch (CS_Action)
            {
                case action.Insert:
                    InsertDataGoiKham();
                    var newRow = DtGoiKham.NewRow();
                    newRow["id_goi"] = IdGoiDVu;
                    UpdateRowDataTable(newRow);
                    DtGoiKham.Rows.Add(newRow);
                    break;
                    
                case action.Update:
                    InsertDataGoiKham();
                    var currentRow = (from row in DtGoiKham.AsEnumerable()
                                      where Utility.Int32Dbnull(IdGoiDVu) == Utility.Int32Dbnull(row["id_goi"])
                                      select row).FirstOrDefault();
                    if(currentRow != null)
                    {
                        UpdateRowDataTable(currentRow);
                    }
                    break;
            }
            BCancel = false;
            Close();
        }

        private void UpdateRowDataTable(DataRow dr)
        {
            try
            {
                dr["ma_goi"] = txtMaGoi.Text;
                dr["ten_goi"] = txtTenGoi.Text;
                dr["so_tien"] = Utility.DoubletoDbnull(txtTienGoi.Text);
                dr["MIEN_GIAM"] = Utility.DoubletoDbnull(txtMienGiam.Text);
                dr["giam_bhyt"] = Utility.DoubletoDbnull(txtGiamtruBHYT.Text);
                dr["HieuLuc_TuNgay"] = dtpHieuLucTuNgay.Value;
                dr["HieuLuc_DenNgay"] = dtpHieuLucDenNgay.Value;
                dr["trang_thai"] = Utility.ByteDbnull(chkHieuLuc.Checked);
                dr["THANH_TIEN"] = Utility.DecimaltoDbnull(txtThanhTien.Text);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);   
            }
        }

        //private void UpdateDataGoiKham()
        //{
        //    try
        //    {
        //        var option = new TransactionOptions();
        //        option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
        //        option.Timeout = TimeSpan.FromMinutes(55);
        //        using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
        //        {
        //            IdGoiDVu = _goiKhamService.ThemSuaGoiKham(Utility.Int32Dbnull(txtID.Text), txtMaGoi.Text,
        //                                                      txtTenGoi.Text, chkHieuLuc.Checked);
        //            InsertDichVuVaoGoi(IdGoiDVu);
        //            scope.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.ShowMsg(ex.Message);
        //    }
        //}

        private void InsertDataGoiKham()
        {
            try
            {
                //var option = new TransactionOptions();
                //option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
                //option.Timeout = TimeSpan.FromMinutes(55);
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        DateTime? hieuLucTuNgay;
                        DateTime? hieuLucDenNgay;

                        hieuLucTuNgay = dtpHieuLucTuNgay.Value;
                        hieuLucDenNgay = dtpHieuLucDenNgay.Value;
                        IdGoiDVu = _goiKhamService.ThemSuaGoiKham(Utility.Int32Dbnull(txtID.Text),0, txtMaGoi.Text,
                                                                  txtTenGoi.Text, Utility.DecimaltoDbnull(txtTienGoi.Text),
                                                                  Utility.DecimaltoDbnull(txtMienGiam.Text),
                                                                  Utility.DecimaltoDbnull(txtGiamtruBHYT.Text),
                                                                  hieuLucTuNgay,
                                                                  hieuLucDenNgay,
                                                                 Utility.Bool2byte( chkHieuLuc.Checked), Utility.ByteDbnull(cboLoaiChuongtrinh.SelectedIndex + 1), Utility.Bool2byte(chkKieuKhuyenmai.Checked),Utility.sDbnull( cboKieuCK.SelectedValue,"%"),chkMGTong.Checked);
                        InsertDichVuVaoGoi(IdGoiDVu);
                    }
                    scope.Complete();
                }
                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void InsertDichVuVaoGoi(int idGoiDVu)
        {
            try
            {
                foreach (GridEXRow row in grdChitiet.GetDataRows())
                {
                    var isNew = Utility.Int32Dbnull(row.Cells["IsNew"].Value);
                    if (isNew != 1) continue;
                    var serviceId = Utility.Int32Dbnull(row.Cells["id_dichvu"].Value);
                    var serviceDetailId = Utility.Int32Dbnull(row.Cells["id_chitietdichvu"].Value);
                    var serviceDetailName = Utility.sDbnull(row.Cells["ten_chitietdichvu"].Value);
                    var loaiDv = Utility.Int16Dbnull(row.Cells["Loai_DVU"].Value);
                    var soLanThucHien = Utility.Int16Dbnull(row.Cells["SO_LUONG"].Value);
                    var idChiTiet = Utility.Int32Dbnull(row.Cells["ID_CHITIET"].Value);
                    decimal  donGia = Utility.DecimaltoDbnull(row.Cells["don_gia"].Value);
                    if(idChiTiet <= 0)
                    {
                        idChiTiet = _goiKhamService.ThemChiTietGoiKham(idGoiDVu, serviceId, serviceDetailId, serviceDetailName, soLanThucHien, loaiDv, donGia,false,0);
                    }
                    else
                    {
                        _goiKhamService.SuaChiTietGoiKham(idChiTiet, soLanThucHien);
                    }

                    row.BeginEdit();
                    row.Cells["ID_CHITIET"].Value = idChiTiet;
                    row.EndEdit();
                }
                grdChitiet.UpdateData();
                grdChitiet.Refresh();
                m_dtChitietgoi.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void SetCurrencyFormat(List<Control> lstControls )
        {
            try
            {
                foreach (Control control in lstControls)
                {
                    if (control is Janus.Windows.GridEX.EditControls.EditBox)
                    {
                        var txtFormantTongTien = new Janus.Windows.GridEX.EditControls.EditBox();
                        txtFormantTongTien = ((Janus.Windows.GridEX.EditControls.EditBox)(control));
                        txtFormantTongTien.Clear();
                        txtFormantTongTien.ReadOnly = true;
                        //if (txtFormantTongTien.Font.Size < 9)
                        //    txtFormantTongTien.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold,
                        //        GraphicsUnit.Point, 0);
                        txtFormantTongTien.TextAlignment = TextAlignment.Far;
                        txtFormantTongTien.KeyPress += txtEventTongTien_KeyPress;
                        txtFormantTongTien.TextChanged += txtEventTongTien_TextChanged;
                    }
                }
                //foreach (Control control in pnlThongtinBN.Controls)
                //{
                //    if (control is EditBox)
                //    {
                //        var txtControl = new EditBox();
                //        if (txtControl.Tag != "NO")//Đánh dấu một số Control cho phép chỉnh sửa. Ví dụ Hạn thẻ BHYT 
                //        //để người dùng có thể sửa nếu phía Tiếp đón gõ sai
                //        {
                //            txtControl = ((EditBox)(control));
                //            txtControl.ReadOnly = true;
                //            txtControl.BackColor = Color.White;
                //        }
                //        txtControl.ForeColor = Color.Black;
                //    }

                //    if (control is UICheckBox)
                //    {
                //        var chkControl = new UICheckBox();
                //        if (chkControl.Tag != "NO")
                //        {
                //            chkControl = (UICheckBox)control;
                //            chkControl.Enabled = false;
                //        }
                //    }
                //}
            }
            catch (Exception exception)
            {
            }
        }
        private void txtEventTongTien_KeyPress(Object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txtTongTien = ((Janus.Windows.GridEX.EditControls.EditBox)(sender));
            Utility.FormatCurrencyHIS(txtTongTien);
        }
        private bool IsValid()
        {
            errorProvider1.Clear();
            if(string.IsNullOrEmpty(txtMaGoi.Text))
            {
                Utility.ShowMsg("Bạn phải nhập mã gói");
                errorProvider1.SetError(txtMaGoi, "Bạn phải nhập mã gói");
                txtMaGoi.Focus();
                return false;
            }
            if(string.IsNullOrEmpty(txtTenGoi.Text))
            {
                Utility.ShowMsg("Bạn phải nhập tên gói");
                errorProvider1.SetError(txtTenGoi, "Bạn phải nhập tên gói");
                txtTenGoi.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtTienGoi.Text) <= 0)
            {
                Utility.ShowMsg("Tiền gói phải >0");
                errorProvider1.SetError(txtTienGoi, "Tiền gói phải >0");
                txtTienGoi.Focus();
                return false;
            }
            return true;
        }

        private void grdKham_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            PerformCellValueChanged(grdKham);
            TinhThanhTien();
        }

        private void grdCLS_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            PerformCellValueChanged(grdCLS);
            TinhThanhTien();
        }

        private void grdThuoc_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            PerformCellValueChanged(grdThuoc);
        }

        private void grdGiuong_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            PerformCellValueChanged(grdGiuong);
        }

        private void grdVTu_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            PerformCellValueChanged(grdVTu);
        }

        private void PerformCellValueChanged(GridEX grd)
        {
            if(chkChiDinhNhanh.Checked)
            {
                if (grd.CurrentRow.IsChecked)
                {
                    ThemDichVuVaoGoi(grd);
                }
            }
            
        }

        private void ModifyButtonCommand()
        {
            cmdDelete.Enabled = grdChitiet.GetCheckedRows().Any();
            cmdSave.Enabled = grdChitiet.RowCount > 0;
        }
        void resetNewItem()
        {
            if (m_dtChitietgoi == null || !m_dtChitietgoi.Columns.Contains("isnew")) return;
            foreach (DataRow dr in m_dtChitietgoi.Rows)
                dr["isnew"] = 0;
            m_dtChitietgoi.AcceptChanges();
        }
        
        private void ThemDichVuVaoGoi(GridEX grd)
        {
            try
            {
                if(!chkChiDinhNhanh.Checked)
                {
                    foreach (GridEXRow row in grd.GetCheckedRows())
                    {
                        ThemDongVaoLuoi(row);
                    }
                }
                else
                {
                    ThemDongVaoLuoi(grd.CurrentRow);
                }

                grd.UnCheckAllRecords();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void ThemDongVaoLuoi(GridEXRow currentRow)
        {
            try
            {

                var serviceDetailId = Utility.sDbnull(currentRow.Cells["id_chitietdichvu"].Value);
                var loaiDv = tabControl.SelectedTab.Key;
                DataRow[] arrDr = m_dtChitietgoi.Select(String.Format("{0}={1} and {2}={3}", "id_chitietdichvu",serviceDetailId ,"Loai_DVU",loaiDv));
                if (arrDr.Length <= 0)
                {
                    var newRow = m_dtChitietgoi.NewRow();
                    newRow["id_chitietdichvu"] = Utility.sDbnull(currentRow.Cells["id_chitietdichvu"].Value);
                    newRow["id_dichvu"] = Utility.Int32Dbnull(currentRow.Cells["id_dichvu"].Value);
                    newRow["ten_chitietdichvu"] = Utility.sDbnull(currentRow.Cells["ten_chitietdichvu"].Value);
                    newRow["Loai_DVU"] = tabControl.SelectedTab.Key;
                    newRow["SO_LUONG"] = 1;
                    newRow["Thanh_tien"] = Utility.DecimaltoDbnull(newRow["SO_LUONG"], 1) * Utility.DecimaltoDbnull(newRow["don_gia"], 1);
                    newRow["Ten_Loai_DVU"] = tabControl.SelectedTab.Text;
                    newRow["don_gia"] = Utility.DecimaltoDbnull(currentRow.Cells["gia_dv"].Value,0);
                    newRow["IsNew"] = 1;
                    m_dtChitietgoi.Rows.Add(newRow);
                }
                else
                {
                    arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + 1;
                    arrDr[0]["Thanh_tien"] = Utility.DecimaltoDbnull(arrDr[0]["SO_LUONG"], 1) * Utility.DecimaltoDbnull(arrDr[0]["don_gia"], 1);
                }
                m_dtChitietgoi.AcceptChanges();
                Utility.GotoNewRowJanus(grdChitiet,"id_chitietdichvu",serviceDetailId);
                TinhThanhTien();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            
        }

        private void TinhThanhTien()
        {
            try
            {
                txtThanhTien.Text = string.Format("{0:#,##0}",
                                                  Utility.DecimaltoDbnull(txtTienGoi.Text) -
                                                  Utility.DecimaltoDbnull(txtMienGiam.Text) -
                                                  Utility.DecimaltoDbnull(txtGiamtruBHYT.Text));
                txtThanhTienThamChieu.Text = string.Format("{0:#,##0}",(from row in grdChitiet.GetDataRows()
                                              select
                                                  Utility.DecimaltoDbnull(row.Cells["SO_LUONG"].Value)*
                                                  Utility.DecimaltoDbnull(row.Cells["don_gia"].Value)).Sum());

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void grdDichVuDaChon_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "GIA_TRONG_GOI" || e.Column.Key == "SO_LUONG" || e.Column.Key == "Gia_DV")
            {
                TinhThanhTien();
                grdChitiet.SetValue("IsNew",1);
            }
            
        }

        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            Filter((DataTable)_currentGrid.DataSource);
        }

        private void Filter(DataTable dt)
        {
            try
            {
                _rowFilter = "(ten_chitietdichvu like '%" + txtFilterName.Text.ToUpper().Trim() +
                             "%' or UnsignedServiceDetail_Name like '%" + txtFilterName.Text.ToUpper().Trim() + "%' )";
                dt.DefaultView.RowFilter = "1=1";
                dt.DefaultView.RowFilter = _rowFilter;
                dt.AcceptChanges();
                _currentGrid.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        string myID = "-1";
        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            
        }

        private void tabControl_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            switch (e.Page.Key)
            {
                case "1":
                    Bring2Front(txtCongkham);
                    _currentAuto = txtCongkham;
                    _currentGrid = grdKham;
                    break;
                case "2":
                    Bring2Front(txtDichvuCLS);
                    _currentAuto = txtDichvuCLS;
                    _currentGrid = grdCLS;
                    break;
                case "3":
                    Bring2Front(txtThuoc);
                    _currentAuto = txtThuoc;
                    _currentGrid = grdThuoc;
                    break;
                case "4":
                    Bring2Front(txtGiuong);
                    _currentAuto = txtGiuong;
                    _currentGrid = grdGiuong;
                    break;
                case "5":
                    Bring2Front(txtVT);
                    _currentAuto = txtVT;
                    _currentGrid = grdVTu;
                    break;
            }
        }
        void Bring2Front(VNS.HIS.UCs.AutoCompleteTextbox frontme)
        {
            foreach (Control ctr in pnlFilter.Controls)
            {
                if (ctr.GetType().Equals(txtCongkham.GetType()))
                {

                    ctr.Visible = false;
                }
            }
            frontme.Visible = true;
            frontme.Focus();
            frontme.SelectAll();
        }
        private void chkChiDinhNhanh_CheckedChanged(object sender, EventArgs e)
        {
            _currentGrid.UnCheckAllRecords();
        }

        private void grdDichVuDaChon_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if(grdChitiet.CurrentRow == null || grdChitiet.CurrentRow.RowType != RowType.Record) return;
                var loaiDv = Utility.Int32Dbnull(grdChitiet.GetValue("Loai_DVU"));
                //tabControl.SelectedTab = tabControl.TabPages[loaiDv - 1];
                Utility.GotoNewRowJanus(_currentGrid, "id_chitietdichvu", Utility.sDbnull(grdChitiet.GetValue("id_chitietdichvu")));
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void grdDichVuDaChon_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void OnlyNumber(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtTienGoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(sender, e);
        }

        private void txtMienGiam_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(sender, e);
        }

        private void txtGiamtruBHYT_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(sender, e);
        }

      
        private void txtTienGoi_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtTienGoi);
        }

        private void txtMienGiam_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtMienGiam);
        }

        private void txtGiamtruBHYT_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtGiamtruBHYT);
        }
        private void LoadNhomChiDinh()
        {
            //DataTable mDtNhomChiDinh = SPs.DanhmucNhomcls(globalVariables.UserName, "ALL", string.Empty, string.Empty, globalVariables.MA_KHOA_THIEN).GetDataSet().Tables[0];
            //txtnhomchidinh.Init(mDtNhomChiDinh, new List<string> { LNhomDvuCl.Columns.IdNhomcls, LNhomDvuCl.Columns.MaNhomcls, LNhomDvuCl.Columns.TenNhomcls });
            //txtnhomchidinh.SetId(-1);
        }
        private void cmdLuuNhomChiDinh_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtMaNhomCls.Text))
            //{
            //    Utility.ShowMsg("Mã nhóm không bỏ trống", "Thông báo", MessageBoxIcon.Warning);
            //    txtMaNhomCls.Focus();
            //    return;
            //}


            //if (string.IsNullOrEmpty(txtTenNhomCls.Text))
            //{
            //    Utility.ShowMsg("Tên nhóm không bỏ trống", "Thông báo", MessageBoxIcon.Warning);
            //    txtTenNhomCls.Focus();
            //    return;
            //}

            //if (Utility.AcceptQuestion("Bạn có muốn lưu thông tin của nhóm chỉ định vào sổ chỉ định của mình không", "Thông báo", true))
            //{
            //    ActionResult actionResult = new NghiepVuCLS().InsertNhomChiDinh(CreateLNhomDvuCl(), CreateArrLNhomDvuCl());
            //    switch (actionResult)
            //    {
            //        case ActionResult.Success:
            //            Utility.ShowMsg("Bạn tạo nhóm thành công", "Thông báo");
            //            LoadNhomChiDinh();
            //            txtMaNhomCls.Clear();
            //            txtTenNhomCls.Clear();
            //            break;
            //        case ActionResult.Error:
            //            Utility.ShowMsg("Có lỗi trong quá trình lưu thông tin nhóm chỉ định", "Thông báo", MessageBoxIcon.Error);

            //            break;
            //    }
            //}
           
        }
        void txtnhomchidinh__OnEnterMe()
        {
            txtFilterName.Clear();
            //if (chkChiDinhNhanh.Checked)
            AddDetailbySelectedGroup();
        }
        void AddDetailbySelectedGroup()
        {
            //if (Utility.Int32Dbnull(txtnhomchidinh.MyID, -1) > 0)
            //{
            //    DataTable dtChitietnhom =
            //        SPs.DanhmucNhomclsChitiet(Utility.Int32Dbnull(txtnhomchidinh.MyID, -1)).GetDataSet().Tables[0];
            //    UncheckItems();
            //    foreach (GridEXRow row in grdCLS.GetDataRows())
            //    {
            //        if (
            //            dtChitietnhom.Select(LNhomDvuCl.Columns.IdDvu + "=" +
            //                                 Utility.sDbnull(row.Cells[LServiceDetail.Columns.ServiceDetailId].Value,
            //                                     "-1")).Length > 0)
            //        {

            //            var serviceDetailId = Utility.sDbnull(row.Cells["ServiceDetail_ID"].Value);
            //            var loaiDv = "2";
            //            var query = from r in m_dtChitietgoi.AsEnumerable()
            //                        where Utility.sDbnull(r["ServiceDetail_ID"]) == serviceDetailId & Utility.sDbnull(r["Loai_DV"]) == loaiDv
            //                        select r;
            //            if (!query.Any())
            //            {
            //                var newRow = m_dtChitietgoi.NewRow();
            //                newRow["ServiceDetail_ID"] = Utility.sDbnull(row.Cells["ServiceDetail_ID"].Value);
            //                newRow["Service_ID"] = Utility.Int32Dbnull(row.Cells["Service_ID"].Value);
            //                newRow["ServiceDetail_Name"] = Utility.sDbnull(row.Cells["ServiceDetail_Name"].Value);
            //                newRow["Loai_DV"] = 2;
            //                newRow["SO_LUONG"] = 1;
            //                newRow["Ten_Loai_DV"] = "Cận lâm sàng";
            //                newRow["Gia_DV"] = Utility.sDbnull(row.Cells["Gia_DV"].Value);
            //                newRow["IsNew"] = 1;
            //                m_dtChitietgoi.Rows.Add(newRow);
            //            }
            //            //ThemDongVaoLuoi(row);
            //        }
            //    }
            //}
        }
        private void UncheckItems()
        {
            if (grdCLS.RowCount <= 0) return;
            grdCLS.UnCheckAllRecords();
            //Tạm bỏ 231025
            //foreach (GridEXRow _item in grdCLS.GetCheckedRows())
            //{
            //  _item.IsChecked = false;
            //}
        }

        private void cmdAddVTTH_Click(object sender, EventArgs e)
        {
            if (autoVTTH.MyID == "-1")
            {
                Utility.ShowMsg("Bạn cần chọn mẫu đơn VTTH trước khi thực hiện tính năng này");
                return;
            }
            if (Utility.AcceptQuestion(string.Format( "Bạn có chắc chắn muốn thêm các VTTH trong đơn mẫu {0} vào trong gói?",autoVTTH.Text), "Xác nhận", true))
                AddGroupVTTH();
        }

        private void cmdAddCLS_Click(object sender, EventArgs e)
        {
            if (autoNhomCLS.MyID == "-1")
            {
                Utility.ShowMsg("Bạn cần chọn nhóm dịch vụ CLS trước khi thực hiện tính năng này");
                return;
            }
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thêm các dịch vụ CLS trong nhóm {0} vào trong gói?", autoNhomCLS.Text), "Xác nhận", true))
                AddGroupCLS();
        }

        private void cmdAddThuoc_Click(object sender, EventArgs e)
        {
            if (autoThuoc.MyID == "-1")
            {
                Utility.ShowMsg("Bạn cần chọn mẫu đơn thuốc trước khi thực hiện tính năng này");
                return;
            }
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thêm các thuốc trong đơn mẫu {0} vào trong gói?", autoThuoc.Text), "Xác nhận", true))
                AddGroupThuoc();
        }

        private void cboKieuCK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKieuCK.SelectedValue.ToString() == "T")
                lblTileCK.Text = "Tiền miễn giảm";
            else
                lblTileCK.Text = "% miễn giảm";
        }
        //private Query _query = LNhomDvuCl.CreateQuery();
        //private LNhomDvuCl CreateLNhomDvuCl()
        //{
        //    LNhomDvuCl objLNhomDvuCl = new LNhomDvuCl();
        //    objLNhomDvuCl.NguoiTao = globalVariables.UserName;
        //    objLNhomDvuCl.NgayTao = BusinessHelper.GetSysDateTime();
        //    objLNhomDvuCl.TenNhomcls = txtTenNhomCls.Text;
        //    objLNhomDvuCl.MaNhomcls = txtMaNhomCls.Text;
        //    objLNhomDvuCl.MaKhoaThien = globalVariables.MA_KHOA_THIEN;
        //    objLNhomDvuCl.SoThuTu =
        //        Utility.Int32Dbnull(
        //            _query.WHERE(LNhomDvuCl.Columns.NguoiTao, Comparison.Equals, globalVariables.UserName).AND(LNhomDvuCl.Columns.IdNhomCha, Comparison.LessOrEquals, 0)
        //                .GetMax(LNhomDvuCl.Columns.SoThuTu)) + 1;
        //    return objLNhomDvuCl;
        //}
        //private LNhomDvuCl[] CreateArrLNhomDvuCl()
        //{
        //    int length = Utility.Int32Dbnull(grdDichVuDaChon.GetDataRows().Length);
        //    var arrNhomChiDinh = new LNhomDvuCl[length];
        //    int idx = 0;
        //    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDichVuDaChon.GetDataRows())
        //    {
        //        if (Utility.sDbnull(gridExRow.Cells["Loai_DV"].Value, "") == "2")
        //        {
        //            arrNhomChiDinh[idx] = new LNhomDvuCl();
        //            arrNhomChiDinh[idx].NguoiTao = globalVariables.UserName;
        //            arrNhomChiDinh[idx].NgayTao = BusinessHelper.GetSysDateTime();
        //            arrNhomChiDinh[idx].MaKhoaThien = globalVariables.MA_KHOA_THIEN;
        //            arrNhomChiDinh[idx].IdLoaiDvu = Utility.Int32Dbnull(gridExRow.Cells[LServiceDetail.Columns.ServiceId].Value);
        //            arrNhomChiDinh[idx].IdDvu = Utility.Int32Dbnull(gridExRow.Cells[LServiceDetail.Columns.ServiceDetailId].Value);
        //            arrNhomChiDinh[idx].SoLuong = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG"].Value);
        //            arrNhomChiDinh[idx].SoThuTu = Utility.Int32Dbnull(idx);
        //        }
        //        idx++;
        //    }

        //    return arrNhomChiDinh;
        //}


        

        
    }
}
