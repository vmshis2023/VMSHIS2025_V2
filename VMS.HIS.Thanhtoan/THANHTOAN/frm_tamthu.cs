using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_tamthu : Form
    {
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        public KcbLuotkham objLuotkham;
        DataTable m_dtChiPhiThanhtoan = new DataTable();
        bool blnLoaded = false;
        public bool isCancel = true;
        private NLog.Logger log;
        byte v_bytNoitru = 0;//0= ngoại trú;1= nội trú
        string lst_IDLoaithanhtoan = "";
        public frm_tamthu(DataTable m_dtChiPhiThanhtoan, NLog.Logger log, byte v_bytNoitru, string lst_IDLoaithanhtoan)
        {
            InitializeComponent();
            this.m_dtChiPhiThanhtoan = m_dtChiPhiThanhtoan;
            this.log = log;
            this.v_bytNoitru = v_bytNoitru;
            dtPaymentDate.Value = DateTime.Now;
            this.lst_IDLoaithanhtoan = lst_IDLoaithanhtoan;
            Utility.SetVisualStyle(this);
            ucThongtinnguoibenh_v21.SetReadonly();
            grdThongTinChuaThanhToan.CellUpdated += grdThongTinChuaThanhToan_CellUpdated;
            grdThongTinChuaThanhToan.ColumnHeaderClick += grdThongTinChuaThanhToan_ColumnHeaderClick;
            grdThongTinChuaThanhToan.EditingCell += grdThongTinChuaThanhToan_EditingCell;
            grdThongTinChuaThanhToan.RowCheckStateChanged += grdThongTinChuaThanhToan_RowCheckStateChanged;
        }

        private void frm_tamthu_Load(object sender, EventArgs e)
        {
            try
            {
                dtPaymentDate.Enabled = Utility.Coquyen("thanhtoan_suangaythanhtoan");
                ucThongtinnguoibenh_v21.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                ucThongtinnguoibenh_v21.Refresh(false);
                LoadPtttNganhang();
                InitPTTTColumns();
                setProperties();
                Utility.SetDataSourceForDataGridEx(grdThongTinChuaThanhToan, m_dtChiPhiThanhtoan, true, true, "trangthai_huy=0 and tthai_tamthu=0 and trangthai_thanhtoan=0", "");
                blnLoaded = true;
                UncheckAll();
                SetSumTotalProperties();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        DataTable dtPttt = new DataTable();
        DataTable dtNganhang = new DataTable();
        void LoadPtttNganhang()
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "PHUONGTHUCTHANHTOAN", "NGANHANG" }, true);
            dtPttt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "PHUONGTHUCTHANHTOAN");
            dtNganhang = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "NGANHANG");
            cboPttt.DataSource = dtPttt;
            cboPttt.ValueMember = DmucChung.Columns.Ma;
            cboPttt.DisplayMember = DmucChung.Columns.Ten;
            cboNganhang.DataSource = dtNganhang;
            cboNganhang.ValueMember = DmucChung.Columns.Ma;
            cboNganhang.DisplayMember = DmucChung.Columns.Ten;

            cboPttt.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtPttt);
            cboNganhang.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtNganhang);
        }
        void InitPTTTColumns()
        {
            try
            {
                DataTable dtPTTT = dtPttt;
                GridEXColumn _colmaPttt = grdTamthu.RootTable.Columns["ma_pttt"];
                _colmaPttt.HasValueList = true;
                _colmaPttt.LimitToList = true;

                GridEXValueListItemCollection _colmaPttt_Collection = grdTamthu.RootTable.Columns["ma_pttt"].ValueList;
                foreach (DataRow item in dtPTTT.Rows)
                {
                    _colmaPttt_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }


                DataTable dtNganhang = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("NGANHANG").And(DmucChung.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
                GridEXColumn _colIDNganHang = grdTamthu.RootTable.Columns["ma_nganhang"];
                _colIDNganHang.HasValueList = true;
                _colIDNganHang.LimitToList = true;

                GridEXValueListItemCollection _colIDNganHang_Collection = grdTamthu.RootTable.Columns["ma_nganhang"].ValueList;
                foreach (DataRow item in dtNganhang.Rows)
                {
                    _colIDNganHang_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }



            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }


        }
        private void setProperties()
        {
            try
            {
                foreach (Control control in pnlThongtintien.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();

                        txtFormantTongTien = ((EditBox)(control));
                        if (txtFormantTongTien.Name != txtGhichu.Name)
                        {
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
                }

            }
            catch (Exception exception)
            {
            }
        }
        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txtTongTien = ((EditBox)(sender));
            Utility.FormatCurrencyHIS(txtTongTien);
        }
        private void txtEventTongTien_KeyPress(Object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }
        decimal _chuathanhtoan = 0m;
        private void SetSumTotalProperties()
        {
            try
            {

                string errMsg = "";
                decimal newBhyt = Utility.DecimaltoDbnull(txtPtramBHChiTra.Text, 0);
                //_THANHTOAN.TinhlaitienBhyTtruocThanhtoan(m_dtChiPhiThanhtoan, TaophieuThanhtoan(), objLuotkham, Taodulieuthanhtoanchitiet(ref errMsg), ref newBhyt);
                txtPtramBHChiTra.Text = newBhyt.ToString();

                decimal tt = 0m;
                decimal tt_bhyt = 0m;
                decimal tt_bhyt_cct = 0m;
                decimal tt_bn_cct = 0m;
                decimal tt_bn_ttt = 0m;
                decimal TT_BN = 0m;
                decimal tt_phuthu = 0m;
                decimal tt_tutuc = 0m;
                _chuathanhtoan = 0m;

                foreach (DataRowView drv in m_dtChiPhiThanhtoan.DefaultView)
                {
                    if (Utility.Int32Dbnull(drv["tinh_chiphi"], 0) == 1 && Utility.Int32Dbnull(drv["trangthai_huy"], 0) == 0 && Utility.Int32Dbnull(drv["tthai_tamthu"], 0) == 0)
                    {
                        tt += Utility.DecimaltoDbnull(drv["TT"], 0);
                        tt_bhyt += Utility.DecimaltoDbnull(drv["TT_BHYT"], 0);
                        tt_bhyt_cct += Utility.DecimaltoDbnull(drv["tt_bhyt_cct"], 0);
                        tt_bn_cct += Utility.DecimaltoDbnull(drv["tt_bn_cct"], 0);
                        tt_bn_ttt += Utility.DecimaltoDbnull(drv["tt_bn_ttt"], 0);
                        TT_BN += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        if (Utility.Int32Dbnull(drv["trangthai_thanhtoan"], 0) == 0) _chuathanhtoan += Utility.DecimaltoDbnull(drv["TT_BN"], 0);
                        tt_phuthu += Utility.DecimaltoDbnull(drv["TT_PHUTHU"], 0);
                        if (Utility.Int32Dbnull(drv["tu_tuc"], 0) == 1) tt_tutuc += Utility.DecimaltoDbnull(drv["TT_TUTUC"], 0);

                    }
                }
                txtTongChiPhi.Text = Utility.sDbnull(tt);
                txtTongtienDCT.Text = !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? "0" : Utility.sDbnull(tt_bhyt);
                txtPhuThu.Text = Utility.sDbnull(tt_phuthu);
                if (Utility.DecimaltoDbnull(tt_tutuc) > 0)
                {
                    txtTuTuc.BackColor = Color.Yellow;
                }
                else
                {
                    txtTuTuc.BackColor = Color.Honeydew;
                }
                txtTuTuc.Text = Utility.sDbnull(tt_tutuc);
                txtBHCT.Text = Utility.sDbnull(tt_bhyt_cct, "0");
                txtBNCT.Text = Utility.sDbnull(tt_bn_cct, "0");
                //txtBN_TTT.Text = Utility.sDbnull(tt_bn_ttt, "0");
                txtBNPhaiTra.Text = Utility.sDbnull(TT_BN);
                TinhToanSoTienPhaithu();
                ThongtinTamung();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private GridEXColumn getGridExColumn(GridEX gridEx, string colName)
        {
            return gridEx.RootTable.Columns[colName];
        }
        void ThongtinTamung()
        {
            SysSystemParameter _objLabel = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("THANHTOAN_THUATHIEU").ExecuteSingle<SysSystemParameter>();
            decimal tongTamung = 0;
            txtTongTU.Clear();
            bool isTU = false;
            if (isTU)
            {

                tongTamung = 0;
                txtTongTU.Text = tongTamung.ToString();
                if (Math.Abs(tongTamung) != 0)
                {
                    decimal chenhlech = _chuathanhtoan - tongTamung;
                    if (chenhlech > 0)
                    {
                        lblThuathieu.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
                        lblThuathieu.ForeColor = Color.DarkBlue;
                        txtThuathieu.Text = chenhlech.ToString();
                    }
                    else
                    {
                        lblThuathieu.ForeColor = Color.DarkRed;
                        lblThuathieu.Text = _objLabel == null ? @"Trả lại BN" : _objLabel.SValue.Split(';')[1];
                        txtThuathieu.Text = Math.Abs(chenhlech).ToString();
                    }
                }

            }
            else
            {
                lblTiennop.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
            }
            if (tongTamung == 0)
            {
                lblThuathieu.Text = _objLabel == null ? @"BN Nộp tiền" : _objLabel.SValue.Split(';')[0];
                txtThuathieu.Text = txtSoTienCanNop.Text;
            }
        }
        private void TinhToanSoTienPhaithu()
        {
            try
            {
                List<GridEXRow> query = (from thanhtoan in grdThongTinChuaThanhToan.GetCheckedRows()
                                         where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                                               && Utility.Int32Dbnull(thanhtoan.Cells["trangthai_thanhtoan"].Value) == 0
                                               && Utility.Int32Dbnull(thanhtoan.Cells["tthai_tamthu"].Value) == 0
                                         //&& Utility.Int32Dbnull(thanhtoan.Cells["trang_thai"].Value) == 0
                                         select thanhtoan).ToList();


                decimal thanhtien = query.Sum(c => Utility.DecimaltoDbnull(c.Cells["TT"].Value));//Lấy tổng tiền =(đơn giá gốc+ phụ thu)*số lượng
                decimal Chietkhauchitiet = 0;
                txtSoTienCanNop.Text = Utility.sDbnull(thanhtien - Chietkhauchitiet);
                _chuathanhtoan = thanhtien - Chietkhauchitiet;
                txtTienChietkhau.Text = Utility.sDbnull(Chietkhauchitiet);
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void grdThongTinChuaThanhToan_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (!blnLoaded) return;
                grdThongTinChuaThanhToan.RowCheckStateChanged -= grdThongTinChuaThanhToan_RowCheckStateChanged;
                SetSumTotalProperties();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                grdThongTinChuaThanhToan.RowCheckStateChanged += grdThongTinChuaThanhToan_RowCheckStateChanged;
            }
        }
        private void grdThongTinChuaThanhToan_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            SetSumTotalProperties();
            e.Column.InputMask = "{0:#,#.##}";
        }
        void grdThongTinChuaThanhToan_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (grdThongTinChuaThanhToan.CurrentColumn != null) grdThongTinChuaThanhToan.CurrentColumn.InputMask = "";
        }
        void grdThongTinChuaThanhToan_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            try
            {
                if (!blnLoaded) return;
                bool isCheck = e.CheckState == RowCheckState.Checked;
                foreach (GridEXRow r in grdThongTinChuaThanhToan.GetCheckedRows())
                {
                    r.BeginEdit();
                    if (Utility.sDbnull(r.Cells["trangthai_thanhtoan"].Value, "0") == "1" || Utility.sDbnull(r.Cells["tthai_tamthu"].Value, "0") == "1")
                    {
                        r.IsChecked = false;
                    }
                    r.EndEdit();
                    ((DataRowView)r.DataRow).Row["colChon"] = r.IsChecked ? 1 : 0;
                    ((DataRowView)r.DataRow).Row["CHON"] = r.IsChecked ? 1 : 0;

                }
                foreach (GridEXRow r in grdThongTinChuaThanhToan.GetDataRows())
                {
                    if (!r.IsChecked)
                    {
                        ((DataRowView)r.DataRow).Row["colChon"] = r.IsChecked ? 1 : 0;
                        ((DataRowView)r.DataRow).Row["CHON"] = r.IsChecked ? 1 : 0;
                    }

                }
                List<string> lstItemChecked = (from p in grdThongTinChuaThanhToan.GetCheckedRows() select Utility.sDbnull(p.Cells["ten_chitietdichvu"].Value)).ToList<string>();
                txtLydo.Text = string.Format("Tạm thu cho các dịch vụ: {0}", string.Join(",", lstItemChecked.ToArray<string>()));
                //if (e.Row != null && e.Row.RowType == RowType.Record)//Nếu check vào từng dòng
                //{
                //    e.Row.BeginEdit();
                //    //if (Utility.sDbnull(e.Row.Cells["trangthai_thanhtoan"].Value, "0") == "0")
                //    //    e.Row.Cells["CHON"].Value = Utility.ByteDbnull(e.CheckState == RowCheckState.Checked ? 1 : 0);
                //    //else
                //    //    e.Row.IsChecked = false;
                //    if (Utility.sDbnull(e.Row.Cells["trangthai_thanhtoan"].Value, "0") == "1")
                //        e.Row.IsChecked = false;
                //    e.Row.EndEdit();
                //    ((DataRowView)e.Row.DataRow).Row["colChon"] = e.Row.IsChecked ? 1 : 0;
                //    ((DataRowView)e.Row.DataRow).Row["CHON"] = e.Row.IsChecked ? 1 : 0;
                //}
                //else//Nếu check theo nhóm hoặc toàn bộ
                //{

                //    foreach (GridEXRow r in grdThongTinChuaThanhToan.GetCheckedRows())
                //    {
                //        if (Utility.sDbnull(r.Cells["trangthai_thanhtoan"].Value, "0") == "1")//Nếu thanh toán rồi thì bỏ chọn
                //        {
                //            r.BeginEdit();
                //            r.IsChecked = false;
                //            r.EndEdit();
                //        }
                //        ((DataRowView)r.DataRow).Row["colChon"] = r.IsChecked ? 1 : 0;
                //        ((DataRowView)r.DataRow).Row["CHON"] = r.IsChecked ? 1 : 0;
                //    }
                //}

                //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
                SetSumTotalProperties();
                //TinhToanSoTienPhaithu();
                ModifyCommand();
            }
            catch (Exception)
            {
            }
            finally
            {
                Application.DoEvents();
            }

        }
        private void UncheckAll()
        {
            try
            {
                foreach (GridEXRow gridExRow in grdThongTinChuaThanhToan.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    if (gridExRow.RowType == RowType.Record)
                    {
                        gridExRow.IsChecked = false;
                        gridExRow.Cells["colChon"].Value = gridExRow.IsChecked ? 1 : 0;
                    }
                    gridExRow.EndEdit();
                }
                grdThongTinChuaThanhToan.UpdateData();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void ModifyCommand()
        {
            try
            {
                bool chotratienngoaitrukhidangnoitru = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHOTRALAITIENNGOAITRU_KHIDANHAPVIEN", "0", false) == "1";
                cmdCancel.Enabled = objLuotkham != null && objLuotkham.TrangthaiNoitru <= 0;
                cmdAccept.Enabled = grdThongTinChuaThanhToan.GetCheckedRows().Length > 0 && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 0 && Utility.ByteDbnull(objLuotkham.Noitru) <= 0;
                cmdInhoadon.Enabled = Utility.isValidGrid(grdTamthu) && objLuotkham != null;
                cmdInBienlai.Visible = Utility.isValidGrid(grdTamthu) && objLuotkham != null;
                //cmdInBienlaiTonghop.Visible = Utility.isValidGrid(grdList) && Utility.isValidGrid(grdTamthu) && grdTamthu.GetDataRows().Length > 1 && objLuotkham != null;
                int TotalPayment = grdTamthu.GetDataRows().Length;
                if (TotalPayment > 1 && objLuotkham != null)
                {
                    string _value = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUHIEUTHI_INBIENLAITONGHOP", "0", false);
                    if (_value == "0")
                    {
                        chkIntonghop.Visible = false;
                    }
                    else
                    {
                        chkIntonghop.Visible = true;
                    }

                }

                else
                {
                    chkIntonghop.Visible = false;
                    chkIntonghop.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public List<int> lstPrivateKey = new List<int>();
        private bool blnJustPayment;
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdThongTinChuaThanhToan.GetCheckedRows().Length <= 0)
                {
                    Utility.ShowMsg("Bạn chưa chọn dịch vụ nào để thực hiện tạm thu. Vui lòng chọn lại");
                    return;
                }
                if (Utility.sDbnull(txtLydo.Text).Length<=0)
                {
                    Utility.ShowMsg("Bạn phải nhập lý do tạm thu");
                    txtLydo.Focus();
                    return;
                }
                lstPrivateKey = (from p in grdThongTinChuaThanhToan.GetCheckedRows()
                                 select Utility.Int32Dbnull(p.Cells["privatekey"].Value, 0)).ToList<int>();
                Utility.EnableButton(cmdAccept, false);
                if (blnJustPayment) return;
                blnJustPayment = true;
                if (!IsValidata()) return;
                //if (!PayCheckDate(dtInput_Date.Value)) return;

                PerformAction();
                blnJustPayment = false;

                isCancel = false;
                this.Close();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {

            }
        }
        /// <summary>
        ///PA2:  Tạo bản ghi tạm ứng tạm thu. Sau đó sẽ hoàn ứng khi thanh toán
        /// </summary>
        ActionResult TamungTamthu(List<KcbThanhtoanChitiet> lstItems)
        {
            try
            {
                NoitruTamung objTamung = new NoitruTamung();
                objTamung.IdBenhnhan = objLuotkham.IdBenhnhan;
                objTamung.MaLuotkham = objLuotkham.MaLuotkham;
                objTamung.MaCoso = objLuotkham.MaCoso;
                objTamung.IdKhoanoitru = v_bytNoitru == 0 ? globalVariables.idKhoatheoMay : objLuotkham.IdKhoanoitru;
                objTamung.IdBuonggiuong = objLuotkham.IdRavien;
                objTamung.IdBuong = objLuotkham.IdBuong;
                objTamung.IdGiuong = objLuotkham.IdGiuong;
                objTamung.Noitru = (byte)(objLuotkham.TrangthaiNoitru <= 0 ? 0 : 1);
                objTamung.KieuTamung = 0;
                objTamung.TuTrului = false;
                objTamung.GhiChu = Utility.DoTrim(txtGhichu.Text);
                objTamung.MotaThem = txtLydo.Text;
                objTamung.IdTnv = Utility.Int32Dbnull(globalVariables.gv_intIDNhanvien, -1);
                objTamung.SoTien = Utility.DecimaltoDbnull(txtThuathieu.Text);
                objTamung.NgayTamung = dtPaymentDate.Value;
                objTamung.MaPttt = Utility.sDbnull(cboPttt.SelectedValue, "-1");// txtPttt.myCode;
                objTamung.MaNganhang = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "-1") : "-1"; // autoNganhang.Enabled ? autoNganhang.myCode : "-1";
                objTamung.MaNguonTamung = "-1";
                objTamung.NguoiTao = globalVariables.UserName;
                objTamung.NgayTao = DateTime.Now;
                objTamung.TrangThai = 0;
                objTamung.IdGoi = -1;
                objTamung.LaTamthu = true;
                objTamung.Code = THU_VIEN_CHUNG.SinhmaVienphi("KQ");
                objTamung.IsNew = true;

                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        objTamung.Save();
                        foreach (KcbThanhtoanChitiet _chitiet in lstItems)
                        {
                            KcbTamungDichvu objTUDV = new KcbTamungDichvu();
                            objTUDV.IdTamung = objTamung.Id;
                            objTUDV.IdPhieu = _chitiet.IdPhieu;
                            objTUDV.IdPhieuchitiet = _chitiet.IdPhieuChitiet;
                            objTUDV.TenChitietdichvu = _chitiet.TenChitietdichvu;
                            objTUDV.IdChitietdichvu = _chitiet.IdChitietdichvu;
                            objTUDV.IdDichvu = _chitiet.IdDichvu;
                            objTUDV.LoaiDvu = _chitiet.IdLoaithanhtoan;
                            objTUDV.DonGia = _chitiet.DonGia;
                            objTUDV.SoLuong = Utility.Int16Dbnull(_chitiet.SoLuong, 0);
                            objTUDV.Save();
                            //Cập nhật trạng thái tạm thu
                            StoredProcedure spupdate = SPs.SpUpdateTrangthaitamthu(_chitiet.IdLoaithanhtoan,
                 objTamung.IdThanhtoan, objTamung.NgayTamung, objTamung.Noitru,
                 _chitiet.KieuChietkhau, _chitiet.TileChietkhau,
                 _chitiet.TienChietkhau, _chitiet.IdPhieu, _chitiet.IdPhieuChitiet,
                 objTamung.NgayTao, objTamung.NguoiTao,0);
                            int reval = spupdate.Execute();
                            if (reval <= 0)
                            {
                                string ErrMsg = string.Format("Cập nhật thông tin tạm thu không thành công dịch vụ loại {0} với id_phieu={1},id_phieuchitiet={2}", _chitiet.IdLoaithanhtoan, _chitiet.IdPhieu, _chitiet.IdPhieuChitiet);
                                Utility.Log("Tạm thu", globalVariables.UserName, ErrMsg, newaction.Error, "frm_THANHTOAN_NGOAITRU");
                                return ActionResult.Cancel;
                            }
                        }
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(-1l, -1l, objTamung.Id, objTamung.MaPttt, objTamung.MaNganhang,
                                    objTamung.IdBenhnhan, objTamung.MaLuotkham,
                                    objTamung.Noitru, objTamung.SoTien, objTamung.SoTien,
                                    objTamung.NguoiTao, objTamung.NgayTao, "", objTamung.NgayTao, -1, 0, 1).Execute();
                    }
                    scope.Complete();
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                return ActionResult.Exception;
            }
        }
        public decimal TongtienCk = 0m;
        public decimal TileChietkhau = 0m;
        public decimal TongtienCkHoadon = 0m;
        public decimal TongtienCkChitiet = 0m;
        public string MaLdoCk = "";
        public string Lydo_chietkhau = "";
        bool ttoan_dthuoc = false;
        public long v_Payment_ID = -1;
        private void PerformAction()
        {
            try
            {
                globalVariables.MaphieuHoanung = "";
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (objLuotkham != null)
                {

                    if (PropertyLib._ThanhtoanProperties.Hoitruockhithanhtoan)
                        if (!Utility.AcceptQuestion(string.Format("Bạn có muốn tạm thu các dịch vụ đang chọn với số tiền {0} vnđ hay không?", txtSoTienCanNop.Text), "Thông báo thanh toán", true))
                        {
                            return;
                        }
                    string ErrMsg = "";

                    long IdHdonLog = -1;
                    TongtienCk = 0;
                    TileChietkhau = 0;
                    TongtienCkHoadon = 0;
                    TongtienCkChitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                    bool bo_ckchitiet = true;
                    string ma_uudai = "";
                    MaLdoCk = "";
                    Lydo_chietkhau = "";

                    decimal ttbnChitrathucsu = 0;
                    ErrMsg = "";
                    KcbThanhtoan v_objPayment = TaophieuThanhtoan();
                    DateTime q = (from p in grdThongTinChuaThanhToan.GetCheckedRows()
                                  select Convert.ToDateTime(p.Cells["CreatedDate"].Value)).Max();
                    v_objPayment.MaxNgayTao = q;
                    List<KcbChietkhau> lstChietkhau = new List<KcbChietkhau>();
                    List<KcbThanhtoanChitiet> lstItems = Taodulieuthanhtoanchitiet(ref ErrMsg);
                    if (Utility.DoTrim(ErrMsg).Length > 0)
                    {
                        Utility.ShowMsg(ErrMsg);
                        return;
                    }
                    if (lstItems == null)
                    {
                        Utility.ShowMsg("Lỗi khi tạo dữ liệu thanh toán chi tiết. Liên hệ đơn vị cung cấp phần mềm để được hỗ trợ\n" + ErrMsg);
                        return;
                    }
                    ActionResult actionResult = ActionResult.UNKNOW;
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_TAMUNG_TAMTHU_DVU", "0", true) == "1")
                    {
                        actionResult = TamungTamthu(lstItems);
                    }
                    else//Dùng phương pháp tạo bản ghi thanh toán và thực hiện kết chuyển
                    {
                        actionResult= _THANHTOAN.TamthuChiphiDvuKcb(v_objPayment, objLuotkham,
                            lstItems, lstChietkhau, ref v_Payment_ID, IdHdonLog,
                            chkLayHoadon.Checked &&
                            THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1", bo_ckchitiet, ma_uudai,
                            ref ttbnChitrathucsu, ref ErrMsg);
                    }
                    bool IN_HOADON = true;
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Tạm thu tiền cho bệnh nhân ID={0}, PID={1}, Tên={2}, sô tiền={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text, v_objPayment.TongTien.ToString()), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                            LayLichsuTamthu();
                            Utility.GotoNewRowJanus(grdTamthu, KcbThanhtoan.Columns.IdThanhtoan, v_Payment_ID.ToString());
                            if (v_Payment_ID <= 0)
                            {
                                grdTamthu.MoveFirst();
                            }
                            //Kiểm tra nếu hình thức thanh toán phân bổ thì hiển thị chức năng phân bổ
                            List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                            //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                            if (lstPhanbo.Contains(cboPttt.SelectedValue.ToString()))
                            {
                                Phanbo();
                            }
                            //Tạm rem phần hóa đơn đỏ lại
                            if (IN_HOADON && chkTudonginhoadonsauthanhtoan.Checked)
                            {
                                int kcbThanhtoanKieuinhoadon = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUINHOADONTUDONG_SAUKHITHANHTOAN", "1", false));
                                if (kcbThanhtoanKieuinhoadon == 1 || kcbThanhtoanKieuinhoadon == 3)
                                    InHoadon();
                                if (kcbThanhtoanKieuinhoadon == 2 || kcbThanhtoanKieuinhoadon == 3)
                                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, v_Payment_ID,-1, objLuotkham, v_bytNoitru);
                            }


                            if (chkHienthiDichvusaukhinhannutthanhtoan.Checked)
                            {
                                ShowPaymentDetail(v_Payment_ID);
                            }


                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                        case ActionResult.Cancel:
                            Utility.ShowMsg(ErrMsg);
                            break;
                    }
                    IN_HOADON = false;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                TongtienCk = 0m;
                TongtienCkChitiet = 0m;
                TongtienCkHoadon = 0m;
                MaLdoCk = "";
                ModifyCommand();
                GC.Collect();
            }
        }
        void ShowPaymentDetail(long v_Payment_ID)
        {
            if (objLuotkham == null)
            {
                return;
            }
            if (objLuotkham != null)
            {
                frm_HuyThanhtoan frm = new frm_HuyThanhtoan(lst_IDLoaithanhtoan);
                frm.objLuotkham = objLuotkham;
                frm.v_Payment_Id = v_Payment_ID;
                frm.Chuathanhtoan = _chuathanhtoan;
                frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                frm.TotalPayment = grdTamthu.GetDataRows().Length;
                frm.ShowCancel = false;
                frm.ShowDialog();
            }
        }
        void InHoadon()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdTamthu.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                decimal TONG_TIEN = Utility.Int32Dbnull(grdTamthu.CurrentRow.Cells["BN_CT"].Value, -1);
                ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(_Payment_ID);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in hóa đơn", "Thông báo lỗi", MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
                log.Trace(ex.Message);
            }
        }
        private DataTable m_dtTamthu, m_dtPhieuChi = new DataTable();
        private void LayLichsuTamthu()
        {
            try
            {
                m_dtTamthu = null;
                DataTable dtData =
                       _THANHTOAN.LaythongtinCacLanthanhtoan(objLuotkham.MaLuotkham,
                          objLuotkham.IdBenhnhan, 5, v_bytNoitru, 0,
                           globalVariables.MA_KHOA_THIEN, lst_IDLoaithanhtoan, -1);

                DataRow[] arrDR = dtData.Select("Kieu_ThanhToan = 5");
                if (arrDR.Length > 0) m_dtTamthu = arrDR.CopyToDataTable();
                else
                    m_dtTamthu = dtData.Clone();
                grdTamthu.Visible = m_dtTamthu.Rows.Count > 0;
                Utility.SetDataSourceForDataGridEx(grdTamthu, dtData, false, true, "1=1", "");
                uiTabPageLichsu.Text = string.Format("Lịch sử tạm thu ({0})", dtData.Rows.Count);
                if (m_dtTamthu.Rows.Count <= 0)
                {
                    txtsotiendathu.Text = "0";
                    txtDachietkhau.Text = "0";
                }
                else
                {
                }
                //RefreshMienGiam();
            }
            catch (Exception exception)
            {
                Utility.CatchException("Lỗi khi lấy thông tin lịch sử tạm thu của bệnh nhân", exception);
                // throw;
            }
        }
        bool Phanbo()
        {
            if (!Utility.isValidGrid(grdTamthu)) return false;
            v_Payment_ID = Utility.Int64Dbnull(grdTamthu.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
            string ma_pttt = Utility.sDbnull(grdTamthu.CurrentRow.Cells[KcbThanhtoan.Columns.MaPttt].Value, "TM");
            string ma_nganhang = Utility.sDbnull(grdTamthu.CurrentRow.Cells[KcbThanhtoan.Columns.MaNganhang].Value, "TM");
            frm_PhanbotientheoPTTT _PhanbotientheoPTTT = new frm_PhanbotientheoPTTT(v_Payment_ID, -1, -1, ma_pttt, ma_nganhang);
            _PhanbotientheoPTTT.objLuotkham = this.objLuotkham;
            _PhanbotientheoPTTT._OnChangePTTT += _PhanbotientheoPTTT__OnChangePTTT;
            return _PhanbotientheoPTTT.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
        void _PhanbotientheoPTTT__OnChangePTTT(long id_thanhtoan, string ma_pttt, string ten_pttt, string ma_nganhang, string ten_nganhang)
        {
            try
            {
                DataRow dr = ((DataRowView)grdTamthu.CurrentRow.DataRow).Row;
                dr["ma_pttt"] = ma_pttt;
                dr["ten_pttt"] = ten_pttt;
                dr["ma_nganhang"] = ma_nganhang;
                dr["ten_nganhang"] = ten_nganhang;
                m_dtTamthu.AcceptChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private KcbThanhtoan TaophieuThanhtoan()
        {
            KcbThanhtoan objPayment = new KcbThanhtoan();
            //Lấy maxdate của các dịch vụ để đưa vào bảng thanh toán phục vụ mục đích chặn sửa ngày thanh toán < ngày tạo dịch vụ

            objPayment.IdThanhtoan = -1;
            objPayment.MaLuotkham = objLuotkham.MaLuotkham;
            objPayment.IdBenhnhan = objLuotkham.IdBenhnhan;
            objPayment.NgayThanhtoan = dtPaymentDate.Value;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.KieuThanhtoan = 5;//0=Thanh toán thường;1= trả lại tiền;2= thanh toán bỏ viện;5=phiếu tạm thu
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.NoiTru = v_bytNoitru;
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.TtoanThuoc = false;//0= thanh toán các loại dịch vụ;1= thanh toán đơn thuốc tại quầy
            objPayment.NguoiIn = string.Empty;
            objPayment.MaPttt = cboPttt.SelectedValue.ToString();
            objPayment.MaNganhang = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "") : "-1";
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.TongTien = Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0);
            objPayment.Ghichu = Utility.DoTrim(txtGhichu.Text);
            objPayment.BnhanChitra = objPayment.TongTien;
            objPayment.BhytChitra = 0;
            objPayment.TileChietkhau = 0;
            objPayment.KieuChietkhau = "T";
            objPayment.TongtienChietkhau = 0;
            objPayment.TongtienChietkhauChitiet = 0;
            objPayment.TongtienChietkhauHoadon = 0;
            if (chkLayHoadon.Checked && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1")
            {
                objPayment.MauHoadon = "";
                objPayment.KiHieu = "";
                objPayment.IdCapphat = -1;
                objPayment.MaQuyen = "";
                objPayment.Serie = "";
            }

            objPayment.MaLydoChietkhau = MaLdoCk;
            objPayment.LydoChietkhau = Lydo_chietkhau;
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            objPayment.IpMaytao = globalVariables.gv_strIPAddress;
            objPayment.TenMaytao = globalVariables.gv_strComputerName;
            return objPayment;
        }
        /// <summary>
        ///     hàm thực hiện mảng của chi tiết thanh toán chi tiết
        /// </summary>
        /// <returns></returns>
        private List<KcbThanhtoanChitiet> Taodulieuthanhtoanchitiet(ref string errMsg)
        {
            try
            {
                DataTable dtDataCheck = new DataTable();
                byte ErrType = 0;//0= xóa dịch vụ sau khi tnv chọn người bệnh-->có trong bảng tt chi tiết, ko có trong các bảng dịch vụ khám,thuốc/vtth,cls;1= đã bị người khác thanh toán;
                List<KcbThanhtoanChitiet> lstItems = new List<KcbThanhtoanChitiet>();
                foreach (GridEXRow row in grdThongTinChuaThanhToan.GetCheckedRows())
                {
                    KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                    newItem.IdThanhtoan = -1;
                    newItem.IdChitiet = -1;
                    newItem.TinhChiphi = 1;
                    if (objLuotkham.PtramBhyt != null) newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                    if (objLuotkham.PtramBhytGoc != null) newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                    //newItem.SoLuong = Utility.DecimaltoDbnull(row.Cells["sluong_sua"].Value, 0);
                    //if (newItem.SoLuong <= 0) newItem.SoLuong = Utility.DecimaltoDbnull(row.Cells["so_luong"].Value, 0);
                    newItem.SoLuong = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.SoLuong].Value, 0);
                    //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                    newItem.BnhanChitra = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.BnhanChitra].Value, 0);
                    newItem.BhytChitra = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.BhytChitra].Value, 0);
                    newItem.DonGia = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.DonGia].Value, 0);
                    newItem.GiaGoc = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.GiaGoc].Value, 0);
                    newItem.TyleTt = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TyleTt].Value, 0);
                    newItem.PhuThu = Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.PhuThu].Value, 0);
                    newItem.TinhChkhau = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TinhChkhau].Value, 0);
                    newItem.CkNguongt = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.CkNguongt].Value, 0);
                    newItem.TuTuc = Utility.ByteDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TuTuc].Value, 0);
                    newItem.IdPhieu = Utility.Int32Dbnull(row.Cells["id_phieu"].Value);
                    newItem.IdKham = Utility.Int32Dbnull(row.Cells["Id_Kham"].Value);
                    newItem.IdPhieuChitiet = Utility.Int32Dbnull(row.Cells["Id_Phieu_Chitiet"].Value, -1);
                    newItem.IdDichvu = Utility.Int16Dbnull(row.Cells["Id_dichvu"].Value, -1);
                    newItem.IdChitietdichvu = Utility.Int16Dbnull(row.Cells["Id_Chitietdichvu"].Value, -1);
                    newItem.TenChitietdichvu = Utility.sDbnull(row.Cells["Ten_Chitietdichvu"].Value, "Không xác định").Trim();
                    newItem.TenBhyt = Utility.sDbnull(row.Cells["ten_bhyt"].Value, "Không xác định").Trim();
                    newItem.DonviTinh = Utility.chuanhoachuoi(Utility.sDbnull(row.Cells["Ten_donvitinh"].Value, "Lượt"));
                    newItem.SttIn = Utility.Int16Dbnull(row.Cells["stt_in"].Value, 0);
                    newItem.IdKhoakcb = Utility.Int16Dbnull(row.Cells["id_khoakcb"].Value, -1);
                    newItem.IdPhongkham = Utility.Int16Dbnull(row.Cells["id_phongkham"].Value, -1);
                    newItem.IdBacsiChidinh = Utility.Int16Dbnull(row.Cells["id_bacsi"].Value, -1);
                    newItem.IdLoaithanhtoan = Utility.ByteDbnull(row.Cells["Id_Loaithanhtoan"].Value, -1);
                    newItem.IdLichsuDoituongKcb = Utility.Int64Dbnull(row.Cells[KcbThanhtoanChitiet.Columns.IdLichsuDoituongKcb].Value, -1);
                    newItem.MatheBhyt = Utility.sDbnull(row.Cells[KcbThanhtoanChitiet.Columns.MatheBhyt].Value, -1);
                    newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(Utility.Int32Dbnull(row.Cells["Id_Loaithanhtoan"].Value, -1));
                    newItem.TienChietkhau = Math.Round(Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TienChietkhau].Value, 0m), 3);
                    newItem.TileChietkhau = Math.Round(Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.TileChietkhau].Value, 0m), 3);
                    newItem.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                    newItem.UserTao = Utility.sDbnull(row.Cells["User_tao"].Value, "UKN").Trim();
                    newItem.KieuChietkhau = "%";
                    newItem.IdThanhtoanhuy = -1;
                    newItem.TrangthaiHuy = 0;
                    newItem.TrangthaiBhyt = 0;
                    newItem.TrangthaiChuyen = 0;
                    newItem.NoiTru = v_bytNoitru;
                    newItem.NguonGoc = (byte)0;
                    newItem.NgayTao = globalVariables.SysDate;
                    newItem.NguoiTao = globalVariables.UserName;
                    lstItems.Add(newItem);
                    dtDataCheck = SPs.ThanhtoanKiemtratontaitruockhithanhtoan(newItem.IdPhieu, newItem.IdPhieuChitiet, newItem.IdLoaithanhtoan).GetDataSet().Tables[0];
                    if (dtDataCheck.Rows.Count <= 0)
                    {
                        ErrType = 0;
                        errMsg += newItem.TenChitietdichvu + "\n";
                        break;
                    }
                    else//Kiểm tra trạng thái thanh toán tránh việc thanh toán 2 lần(2 user cùng chọn và sau đó từng người bấm nút thanh toán)
                        if (dtDataCheck.Rows[0]["trangthai_thanhtoan"].ToString() == "1")
                    {
                        ErrType = 1;
                        errMsg += newItem.TenChitietdichvu + "\n";
                        break;
                    }
                }
                if (errMsg.Length > 0)
                    if (ErrType == 0)
                        errMsg = "Một số dịch vụ đang chọn thanh toán đã bị xóa/hủy bởi người khác. Vui lòng chọn lại người bệnh để lấy lại dữ liệu mới nhất. Kiểm tra các dịch vụ không tồn tại dưới đây:\n" + errMsg;
                    else if (ErrType == 1)
                        errMsg = "Một số dịch vụ đang chọn thanh toán đã được thanh toán bởi TNV khác(trong lúc bạn chọn và chưa bấm thanh toán). Vui lòng chọn lại người bệnh để lấy lại dữ liệu mới nhất. Kiểm tra các dịch vụ đã được thanh toán dưới đây:\n" + errMsg;
                return lstItems;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }
        private bool PayCheckDate(DateTime InputDate)
        {
            //if (globalVariables.SysDate.Date != InputDate.Date && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOPHEPCHONGAYTHANHTOAN", "1", false) == "1")
            //{
            //    frm_ChonngayThanhtoan frm = new frm_ChonngayThanhtoan();
            //    frm.pdt_InputDate = dtInput_Date.Value;
            //    frm.ShowDialog();
            //    if (!frm.mv_blnCancel)
            //    {
            //        dtPaymentDate.Value = frm.pdt_InputDate;
            //        return true;
            //    }
            //    else
            //        return false;
            //}
            return true;
        }
        private bool IsValidata()
        {
            bool bCheckPayment = false;
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHOPHEP_0_DONG", true) == "0")
            {
                if (Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0) == 0)
                {
                    Utility.ShowMsg("Hệ thống chỉ cho phép thanh toán khi số tiền thu phải > 0. Mời bạn kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (grdThongTinChuaThanhToan.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một dịch vụ chưa thanh toán để thực hiện thanh toán", "Thông báo", MessageBoxIcon.Warning);
                grdThongTinChuaThanhToan.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdThongTinChuaThanhToan.GetCheckedRows())
            {
                if (gridExRow.Cells["trangthai_thanhtoan"].Value.ToString() == "1")
                {
                    bCheckPayment = true;
                    break;
                }
            }
            if (bCheckPayment)
            {
                Utility.ShowMsg("Bạn phải chọn các bản ghi chưa thực hiện thanh toán mới thanh toán được", "Thông báo", MessageBoxIcon.Warning);
                grdThongTinChuaThanhToan.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdThongTinChuaThanhToan.GetCheckedRows())
            {
                if (gridExRow.Cells["trangthai_huy"].Value.ToString() == "1")
                {
                    bCheckPayment = true;
                    break;
                }
            }
            if (bCheckPayment)
            {
                Utility.ShowMsg("Bạn phải bỏ chọn bản ghi bị hủy trước khi thanh toán.Vui lòng kiểm tra lại", "Thông báo",
                    MessageBoxIcon.Warning);
                grdThongTinChuaThanhToan.Focus();
                return false;
            }
            if (cboPttt.SelectedValue.ToString() == "-1")
            {
                Utility.ShowMsg("Bạn phải chọn phương thức thanh toán trước khi thực hiện thanh toán");
                cboPttt.Focus();
                return false;
            }
            if (!isValidPttt_Nganhang())
                return false;
            objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin bệnh nhân cần thanh toán. Đề nghị liên hệ IT để được giải quyết");
                return false;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && Utility.DoTrim(objLuotkham.MatheBhyt) == "")
            {
                Utility.ShowMsg("Bệnh nhân BHYT cần nhập mã thẻ BHYT trước khi thanh toán");
                return false;
            }

            if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
            {
                Utility.ShowMsg("Bệnh nhân này đã phát sinh dịch vụ nội trú(Nộp tiền tạm ứng, Lập phiếu điều trị...) nên hệ thống không cho phép thanh toán ngoại trú nữa");
                return false;
            }

            if (objLuotkham.NgayTiepdon > dtPaymentDate.Value)
            {
                Utility.ShowMsg(string.Format("Ngày thanh toán không được phép nhỏ hơn ngày tiếp đón người bệnh {0}", objLuotkham.NgayTiepdon.ToString("dd/MM/yyyy HH:mm:ss")));
                return false;
            }

            return true;
        }
        bool isValidPttt_Nganhang()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            if (cboNganhang.Enabled && Utility.sDbnull(cboNganhang.SelectedValue, "") == "")
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt.Text));
                cboNganhang.Focus();
                return false;
            }
            return true;
        }

        private void cboPttt_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            cboNganhang.Enabled = lstPTTT.Contains(Utility.sDbnull(cboPttt.SelectedValue, "-1"));
            if (!cboNganhang.Enabled) cboNganhang.SelectedIndex = -1;
        }

        private void cmdInBienlai_Click(object sender, EventArgs e)
        {
            int _Payment_ID = Utility.Int32Dbnull(grdTamthu.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
            byte kieuthanhtoan = Utility.ByteDbnull(grdTamthu.CurrentRow.Cells[KcbThanhtoan.Columns.KieuThanhtoan].Value, 0);
            byte ttoan_thuoc = Utility.ByteDbnull(grdTamthu.CurrentRow.Cells[KcbThanhtoan.Columns.TtoanThuoc].Value, 0);
            if (kieuthanhtoan == 0 || kieuthanhtoan == 5)
            {
                if (chkIntonghop.Visible && chkIntonghop.Checked)
                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(true, _Payment_ID,-1, objLuotkham, 0);
                else
                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, _Payment_ID,-1, objLuotkham, 0);
            }
            else//Phiếu chi
            {
                KcbThanhtoan objKcbThanhtoan = KcbThanhtoan.FetchByID(_Payment_ID);
                if (ttoan_thuoc == 0)
                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiPhieuChi(chkIntonghop.Visible && chkIntonghop.Checked, _Payment_ID, objLuotkham, objKcbThanhtoan.NoiTru);
                else
                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiPhieuChiTralaiThuoc(false, _Payment_ID,-1, objLuotkham, objKcbThanhtoan.NoiTru);
            }
        }

        private void cmdInhoadon_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdTamthu))
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một phiếu thanh toán để in hóa đơn trong lưới bên dưới", "thông báo");
                return;
            }
            byte kieuthanhtoan = Utility.ByteDbnull(grdTamthu.CurrentRow.Cells[KcbThanhtoan.Columns.KieuThanhtoan].Value, 0);
            if (kieuthanhtoan == 0 || kieuthanhtoan == 5)
                InHoadon();
            else
                new INPHIEU_THANHTOAN_NGOAITRU(this.log).InPhieuchi(Utility.Int32Dbnull(grdTamthu.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1));
            string seria = Utility.sDbnull(grdTamthu.GetValue(KcbThanhtoan.Columns.Serie), "");
            if (seria != "")
            {
                InHoaDon_BanHang();
            }

        }

        private void chkChinhtienTamthu_CheckedChanged(object sender, EventArgs e)
        {
            txtThuathieu.ReadOnly = !chkChinhtienTamthu.Checked;
        }

        void InHoaDon_BanHang()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdTamthu.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                new INPHIEU_THANHTOAN_NGOAITRU().InHoaDon_BanHang(_Payment_ID, -1,v_bytNoitru);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
                log.Trace(ex.Message);
            }
        }
    }
}
