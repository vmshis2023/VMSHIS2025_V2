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
    public partial class frm_Ghino : Form
    {
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        public KcbLuotkham objLuotkham;
        DataTable m_dtChiPhiThanhtoan = new DataTable();
        bool blnLoaded = false;
        public bool isCancel = true;
        public bool ghi_no = true;
        private NLog.Logger log;
        byte v_bytNoitru = 0;//0= ngoại trú;1= nội trú
        string lst_IDLoaithanhtoan = "";
        DataTable m_dt_dichvu_ghino=new DataTable();
        public frm_Ghino(DataTable m_dtChiPhiThanhtoan, DataTable m_dt_dichvu_ghino, NLog.Logger log, byte v_bytNoitru, string lst_IDLoaithanhtoan)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.m_dtChiPhiThanhtoan = m_dtChiPhiThanhtoan;
            this.m_dt_dichvu_ghino = m_dt_dichvu_ghino;
            this.log = log;
            this.v_bytNoitru = v_bytNoitru;
            dtPaymentDate.Value = DateTime.Now;
            this.lst_IDLoaithanhtoan = lst_IDLoaithanhtoan;
            this.KeyDown += Frm_Ghino_KeyDown;
            ucThongtinnguoibenh_v21.SetReadonly();
            grdThongTinChuaThanhToan.CellUpdated += grdThongTinChuaThanhToan_CellUpdated;
            grdThongTinChuaThanhToan.ColumnHeaderClick += grdThongTinChuaThanhToan_ColumnHeaderClick;
            grdThongTinChuaThanhToan.EditingCell += grdThongTinChuaThanhToan_EditingCell;
            grdThongTinChuaThanhToan.RowCheckStateChanged += grdThongTinChuaThanhToan_RowCheckStateChanged;
            grd_danhsach_ghino.SelectionChanged +=  grd_danhsach_ghino_SelectionChanged;
        }

        private void  grd_danhsach_ghino_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grd_danhsach_ghino) || !blnLoaded)
                {
                    grd_chitietghino.DataSource = null;
                    return;
                }
                long id = Utility.Int64Dbnull(grd_danhsach_ghino.GetValue("id"));
                DataTable dt_chitietghino = m_dtChiPhiThanhtoan.Clone();
                var p = m_dtChiPhiThanhtoan.AsEnumerable().Where(c => Utility.Int64Dbnull(c["id_tamthu"]) == id).ToList();
                if (p.Any())
                    dt_chitietghino = p.CopyToDataTable();
                grd_chitietghino.DataSource = dt_chitietghino;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }

        private void Frm_Ghino_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode==Keys.F5)
                LayLichsuGhino();
        }

        private void frm_Ghino_Load(object sender, EventArgs e)
        {
            try
            {
                uiTabPageDichvu.TabVisible = ghi_no;
                dtPaymentDate.Enabled = Utility.Coquyen("GHINO_SUA_NGAYGHINO");
                dtPaymentDate.Value = globalVariables.SysDate;
                ucThongtinnguoibenh_v21.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                ucThongtinnguoibenh_v21.Refresh(false);
                LayLichsuGhino();
                setProperties();
                Utility.SetDataSourceForDataGridEx(grdThongTinChuaThanhToan, m_dt_dichvu_ghino, true, true, "trangthai_huy=0 and tthai_tamthu=0 and trangthai_thanhtoan=0", "");
                blnLoaded = true;
                grd_danhsach_ghino_SelectionChanged(grd_danhsach_ghino, e);
                UncheckAll();
                SetSumTotalProperties();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
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

                foreach (DataRowView drv in m_dt_dichvu_ghino.DefaultView)
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
               // ThongtinTamung();
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
                txtThuathieu.Text = txtSoTienCanNop.Text;
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
                txtLydo.Text = string.Format("ghi nợ cho các dịch vụ: {0}", string.Join(",", lstItemChecked.ToArray<string>()));
                
                //Thay hàm TinhToanSoTienPhaithu= hàm SetSumTotalProperties để tính lại tiền BHYT chi trả
                SetSumTotalProperties();
               
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
                cmdInhoadon.Enabled = Utility.isValidGrid(grd_danhsach_ghino) && objLuotkham != null;
                cmdInBienlai.Visible = Utility.isValidGrid(grd_danhsach_ghino) && objLuotkham != null;
                //cmdInBienlaiTonghop.Visible = Utility.isValidGrid(grdList) && Utility.isValidGrid( grd_danhsach_ghino) &&  grd_danhsach_ghino.GetDataRows().Length > 1 && objLuotkham != null;
                int TotalPayment = grd_danhsach_ghino.GetDataRows().Length;
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
                    Utility.ShowMsg("Bạn chưa chọn dịch vụ nào để thực hiện ghi nợ. Vui lòng chọn lại");
                    return;
                }
                if (Utility.sDbnull(txtLydo.Text).Length<=0)
                {
                    Utility.ShowMsg("Bạn phải nhập lý do ghi nợ");
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
              if(chk_thoatsaukhiluu.Checked)  this.Close();
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
        ///PA2:  Tạo bản ghi tạm ứng ghi nợ. Sau đó sẽ hoàn ứng khi thanh toán
        /// </summary>
       
        public decimal TongtienCk = 0m;
        public decimal TileChietkhau = 0m;
        public decimal TongtienCkHoadon = 0m;
        public decimal TongtienCkChitiet = 0m;
        public string MaLdoCk = "";
        public string Lydo_chietkhau = "";
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
                        if (!Utility.AcceptQuestion(string.Format("Bạn có muốn ghi nợ các dịch vụ đang chọn với số tiền {0} vnđ hay không?", txtSoTienCanNop.Text), "Thông báo thanh toán", true))
                        {
                            return;
                        }
                    string ErrMsg = "";

                    TongtienCk = 0;
                    TileChietkhau = 0;
                    TongtienCkHoadon = 0;
                    TongtienCkChitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                    MaLdoCk = "";
                    Lydo_chietkhau = "";

                    ErrMsg = "";
                    KcbThanhtoan v_objPayment = TaophieuThanhtoan();
                    DateTime q = (from p in grdThongTinChuaThanhToan.GetCheckedRows()
                                  select Convert.ToDateTime(p.Cells["CreatedDate"].Value)).Max();
                    v_objPayment.MaxNgayTao = q;
                    List<KcbChietkhau> lstChietkhau = new List<KcbChietkhau>();
                    List<string> lstKey = grdThongTinChuaThanhToan.GetCheckedRows().Select(c=>Utility.sDbnull(c.Cells[""].Value)).Distinct().ToList<string>();
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
                    
                        KcbThanhtoanGhino ghino = new KcbThanhtoanGhino();
                        ghino.IdBenhnhan = objLuotkham.IdBenhnhan;
                        ghino.MaLuotkham = objLuotkham.MaLuotkham;
                        ghino.IdNguoiGhino = globalVariables.gv_intIDNhanvien;
                    ghino.LydoGhino = Utility.DoTrim(txtLydo.Text);
                        ghino.NgayTao = globalVariables.SysDate;
                        ghino.NgayGhino = ghino.NgayTao;
                        ghino.NguoiTao = globalVariables.UserName;
                        ghino.SoTien = v_objPayment.TongTien;
                        ghino.TrangThai = 0;
                        actionResult = _THANHTOAN.Ghino(ghino, objLuotkham,
                            lstItems,  ref v_Payment_ID, ref ErrMsg);
                   
                    bool IN_HOADON = true;
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Ghi nợ tiền cho bệnh nhân ID={0}, PID={1}, Tên={2}, sô tiền={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text, v_objPayment.TongTien.ToString()), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                            LayLichsuGhino();
                            m_dt_dichvu_ghino.AsEnumerable()
    .Where(r => lstKey.Contains(Utility.sDbnull(r["privatekey"])))
    .ToList()
    .ForEach(r => { 
        r["id_tamthu"] = v_Payment_ID;
        r["tthai_tamthu"] = 1;
    });
                            Utility.GotoNewRowJanus(grd_danhsach_ghino, "id", v_Payment_ID.ToString());
                            if (v_Payment_ID <= 0)
                            {
                                grd_danhsach_ghino.MoveFirst();
                            }
                            
                            //Tạm rem phần hóa đơn đỏ lại
                            if (chkTudonginhoadonsauthanhtoan.Checked)
                            {
                                
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
       
       
        private DataTable m_dtTamthu, m_dtPhieuChi = new DataTable();
        private void LayLichsuGhino()
        {
            try
            {
                DataTable m_dtTamthu = SPs.KcbThanhtoanGhinoLaydanhsach(new DateTime(1900, 1, 1), new DateTime(1900, 1, 1), objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
              
                Utility.SetDataSourceForDataGridEx(grd_danhsach_ghino, m_dtTamthu, false, true, "1=1", "");
                uiTabPageLichsu.Text = string.Format("Lịch sử ghi nợ ({0})", m_dtTamthu.Rows.Count);
                if (grd_danhsach_ghino.GetRows().Count() > 0)
                    grd_danhsach_ghino.MoveFirst();
            }
            catch (Exception exception)
            {
                Utility.CatchException("Lỗi khi lấy thông tin lịch sử ghi nợ của bệnh nhân", exception);
                // throw;
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
            objPayment.KieuThanhtoan = 5;//0=Thanh toán thường;1= trả lại tiền;2= thanh toán bỏ viện;5=phiếu ghi nợ
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.NoiTru = v_bytNoitru;
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.TtoanThuoc = false;//0= thanh toán các loại dịch vụ;1= thanh toán đơn thuốc tại quầy
            objPayment.NguoiIn = string.Empty;
            objPayment.MaPttt = "";
            objPayment.MaNganhang = "";
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.TongTien = Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0);
            objPayment.Ghichu = "";
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
     
        private bool IsValidata()
        {
            bool bCheckPayment = false;
             
            if (grdThongTinChuaThanhToan.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một dịch vụ chưa thanh toán để thực hiện ghi nợ", "Thông báo", MessageBoxIcon.Warning);
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
                Utility.ShowMsg("Bạn phải chọn các bản ghi chưa thực hiện thanh toán mới ghi nợ được", "Thông báo", MessageBoxIcon.Warning);
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
                Utility.ShowMsg("Bạn phải bỏ chọn bản ghi bị hủy trước khi ghi nợ.Vui lòng kiểm tra lại", "Thông báo",
                    MessageBoxIcon.Warning);
                grdThongTinChuaThanhToan.Focus();
                return false;
            }
           
            objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin bệnh nhân cần Ghi nợ. Đề nghị liên hệ IT để được giải quyết");
                return false;
            }
            
            if (objLuotkham.NgayTiepdon > dtPaymentDate.Value)
            {
                Utility.ShowMsg(string.Format("Ngày ghi nợ không được phép nhỏ hơn ngày tiếp đón người bệnh {0}", objLuotkham.NgayTiepdon.ToString("dd/MM/yyyy HH:mm:ss")));
                return false;
            }

            return true;
        }
       


        private void chkChinhtienTamthu_CheckedChanged(object sender, EventArgs e)
        {
            txtThuathieu.ReadOnly = !chkChinhtienTamthu.Checked;
        }
        int num = 0;
        private void cmd_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("GHINO_XOAPHIEU"))
                {
                    Utility.thongbaokhongcoquyen("GHINO_XOAPHIEU", "xóa phiếu ghi nợ. Vui lòng liên hệ bộ phận IT để được trợ giúp");
                    return;
                }
                if (grd_danhsach_ghino.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grd_danhsach_ghino))
                {
                    grd_danhsach_ghino.CurrentRow.BeginEdit();
                    grd_danhsach_ghino.CurrentRow.IsChecked = true;
                    grd_danhsach_ghino.CurrentRow.EndEdit();
                }
                GridEXRow[] lstRows = grd_danhsach_ghino.GetCheckedRows();
                foreach (GridEXRow row in lstRows)
                {
                    long id = Utility.Int64Dbnull(row.Cells["Id"].Value);
                    KcbThanhtoanGhino objGhino = KcbThanhtoanGhino.FetchByID(id);
                    if (objGhino != null)
                    {
                        if (objGhino.NguoiTao != globalVariables.UserName)
                        {
                            if (!Utility.Coquyen("GHINO_XOAPHIEU_CUANGUOIKHAC"))
                            {
                                Utility.thongbaokhongcoquyen("GHINO_XOAPHIEU_CUANGUOIKHAC", string.Format("xóa phiếu ghi nợ id={0} do {1} tạo. Vui lòng liên hệ bộ phận IT để được trợ giúp", objGhino.Id, objGhino.NguoiTao));
                                return;
                            }
                        }
                        using (var scope = new TransactionScope())
                        {
                            using (var dbscope = new SharedDbConnectionScope())
                            {
                                num = new Delete().From(KcbThanhtoanGhino.Schema).Where(KcbThanhtoanGhino.Columns.Id).IsEqualTo(id).Execute();
                                StoredProcedure spupdate = SPs.SpUpdateTrangthaiGhino(0,
                          objGhino.Id, objGhino.NgayGhino, objLuotkham.Noitru,
                          -1, -1,
                          DateTime.Now, globalVariables.UserName, 1);
                                num += spupdate.Execute();
                            }
                            scope.Complete();
                        }
                        if (num > 0)
                        {
                            isCancel = false;
                            row.Delete();
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Phiếu ghi nợ id={0} tạo bởi {1} không tồn tại (Có thể vừa bị xóa trong khi bạn chưa thao tác). Vui lòng nhấn F5 để làm mới lại danh sách", objGhino.Id, objGhino.NguoiTao));
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmd_exit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmd_gachno_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("GHINO_GACHNO"))
                {
                    Utility.thongbaokhongcoquyen("GHINO_GACHNO", "gạch nợ. Vui lòng liên hệ bộ phận IT để được trợ giúp");
                    return;
                }
                if (grd_danhsach_ghino.GetCheckedRows().Count() <= 0 && Utility.isValidGrid(grd_danhsach_ghino))
                {
                    grd_danhsach_ghino.CurrentRow.BeginEdit();
                    grd_danhsach_ghino.CurrentRow.IsChecked = true;
                    grd_danhsach_ghino.CurrentRow.EndEdit();
                }
                foreach (GridEXRow row in grd_danhsach_ghino.GetCheckedRows())
                {
                    long id = Utility.Int64Dbnull(row.Cells["Id"].Value);
                    KcbThanhtoanGhino objGhino = KcbThanhtoanGhino.FetchByID(id);
                    if (objGhino != null)
                    {

                        num = new Update(KcbThanhtoanGhino.Schema)
                            .Set(KcbThanhtoanGhino.Columns.TrangThai).EqualTo(1)
                            .Set(KcbThanhtoanGhino.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbThanhtoanGhino.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(KcbThanhtoanGhino.Columns.IdNguoiGachno).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(KcbThanhtoanGhino.Columns.NgayGachno).EqualTo(globalVariables.SysDate)
                            .Where(KcbThanhtoanGhino.Columns.Id).IsEqualTo(id).Execute();

                        if (num > 0)
                        {
                            row.BeginEdit();
                            row.Cells[KcbThanhtoanGhino.Columns.TrangThai].Value = 1;
                            row.Cells[KcbThanhtoanGhino.Columns.IdNguoiGachno].Value = globalVariables.gv_intIDNhanvien;
                            row.Cells["ten_nguoi_gachno"].Value = globalVariables.UserName;
                            row.Cells[KcbThanhtoanGhino.Columns.NgayGachno].Value = globalVariables.SysDate; ;
                            row.EndEdit();
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Phiếu ghi nợ id={0} tạo bởi {1} không tồn tại (Có thể vừa bị xóa trong khi bạn chưa thao tác). Vui lòng nhấn F5 để làm mới lại danh sách", objGhino.Id, objGhino.NguoiTao));
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

       
    }
}
