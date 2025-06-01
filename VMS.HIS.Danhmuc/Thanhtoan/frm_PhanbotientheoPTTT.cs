using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_PhanbotientheoPTTT : Form
    {
        public long id_thanhtoan = -1;
        public KcbThanhtoan objTT = null;
        public long id_phieuthu = -1;
        public long id_tamung = -1;
        public bool m_blnCancel = true;
        public int Id_HD_LOG = -1;
        public int TotalPayment = 0;
        bool m_blnLoaded = false;
        bool isSaved = false;
        public KcbLuotkham objLuotkham = null;
        private DataTable m_dtData = new DataTable();
        List<string> lstPTTTBatchonNganhang = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
        List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
        public string ma_pttt = "";
        public string ma_nganhang = "";
        public byte loai_phanbo = 0;
        public decimal TongtienPhanBo = -1;
        public delegate void OnChangePTTT(long id_thanhtoan, string ma_pttt,string ten_pttt,string ma_nganhang,string ten_nganhang);
        public event OnChangePTTT _OnChangePTTT;
        public frm_PhanbotientheoPTTT(long id_thanhtoan, long id_phieuthu,long id_tamung,string ma_pttt,string ma_nganhang)
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            FormClosing += frm_PhanbotientheoPTTT_FormClosing;
            this.ma_pttt = ma_pttt;
            this.ma_nganhang = ma_nganhang;
            Utility.SetVisualStyle(this);
            DataTable dtNganhang = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("NGANHANG").And(DmucChung.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
            GridEXColumn _colIDNganHang = grdList.RootTable.Columns["ma_nganhang"];
            _colIDNganHang.HasValueList = true;
            _colIDNganHang.LimitToList = true;

            GridEXValueListItemCollection _colIDNganHang_Collection = grdList.RootTable.Columns["ma_nganhang"].ValueList;
            foreach (DataRow item in dtNganhang.Rows)
            {
                _colIDNganHang_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
            }
            this.id_thanhtoan = id_thanhtoan;
            objTT = KcbThanhtoan.FetchByID(id_thanhtoan);
            this.id_phieuthu = id_phieuthu;
            this.id_tamung = id_tamung;
            InitEvents();
            setProperties();
            
        }
        void ModifyQRButtons()
        {
            try
            {

                long IdQrcode = -1;
                List<string> lstAllowQR = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_QR", true).Split(',').ToList<string>();
                bool isActiveQRPay = THU_VIEN_CHUNG.Laygiatrithamsohethong("QR_ACTIVE", "0", true) == "1";
                if(isActiveQRPay)
                {
                    QrPhieuThanhtoan _qrphieu = new Select().From(QrPhieuThanhtoan.Schema).Where(QrPhieuThanhtoan.Columns.IdThanhtoan).IsEqualTo(id_thanhtoan).ExecuteSingle<QrPhieuThanhtoan>();
                    if (_qrphieu != null)
                        IdQrcode = _qrphieu.IdQrCode;
                }    
              
                bool hasCK = (from p in grdList.GetDataRows() where lstAllowQR.Contains(Utility.sDbnull(p.Cells[KcbThanhtoanPhanbotheoPTTT.Columns.MaPttt].Value)) && Utility.DecimaltoDbnull(p.Cells["so_tien"].Value)>0 select p).Any();
                cmdTaoQR.Enabled = isActiveQRPay && hasCK && IdQrcode <= 0 && objTT!=null;
                cmdHuyQR.Enabled = isActiveQRPay && hasCK && IdQrcode > 0 && objTT != null;
            }
            catch (Exception ex)
            {

            }
          
        }
        void frm_PhanbotientheoPTTT_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //if (lstPhanbo.Contains(Utility.sDbnull(cboPttt_chung.SelectedValue)) && !isSaved)
                //{
                //    Utility.ShowMsg("Bạn cần lưu thông tin phân bổ trước khi thoát khỏi chức năng");
                //    cmdAccept.Focus();
                //    e.Cancel = true;
                //}
                //if (lstPhanbo.Contains(Utility.sDbnull(cboPttt_chung.SelectedValue)))
                //{
                //    if (Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
                //    {
                //        Utility.ShowMsg(string.Format("Tổng tiền phân bổ theo các Hình thức thanh toán cần = {0}. Mời bạn kiểm tra lại", txtTongtien.Text));
                //        e.Cancel = true;

                //    }
                //}
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void InitEvents()
        {
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.Load += new System.EventHandler(this.frm_PhanbotientheoPTTT_Load);
            this.KeyDown += new KeyEventHandler(frm_PhanbotientheoPTTT_KeyDown);
            cmdAccept.Click += cmdAccept_Click;
            grdList.KeyDown += grdList_KeyDown;
            grdList.SelectionChanged += grdList_SelectionChanged;
            grdList.UpdatingCell += grdList_UpdatingCell;
            grdList.CellValueChanged += GrdList_CellValueChanged;
            txtSotien.KeyDown += txtSotien_KeyDown;
          
        }

        private void GrdList_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifyQRButtons();
        }

        void cboPttt_rieng__OnEnterMe()
        {
           
        }

        void cboPttt_chung__OnEnterMe()
        {
           

        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                GridEXColumn _colIDNganHang = grdList.RootTable.Columns["ma_nganhang"];
                bool blnChonNganhang = lstPTTTBatchonNganhang.Contains(Utility.sDbnull(grdList.GetValue("ma_pttt"), "ERR"));
                if (blnChonNganhang )
                {
                    _colIDNganHang.Selectable = true;
                }
                else
                {
                    _colIDNganHang.Selectable = false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        void txtSotien_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string ma_nganhang="-1";
                if (Utility.sDbnull( cboPttt_rieng.SelectedValue) != "-1" && cboNganhang.Enabled && Utility.sDbnull( cboNganhang.SelectedValue) != "-1")
                {
                    ma_nganhang =Utility.sDbnull( cboNganhang.SelectedValue);
                    errorProvider1.SetError(cboNganhang, string.Format("Bạn phải chọn ngân hàng ứng với phương thức thanh toán {0}", cboPttt_rieng.Text));
                }
                if (e.KeyCode == Keys.Enter || (e.Control && e.KeyCode == Keys.A) || (e.Control && e.KeyCode == Keys.C))
                {
                    if (Utility.sDbnull( cboPttt_rieng.SelectedValue) == "-1")
                    {
                        cboPttt_rieng.Focus();
                       
                        return;
                    }
                    else if (Utility.sDbnull(cboPttt_rieng.SelectedValue) != "-1" && cboNganhang.Enabled && Utility.sDbnull(cboNganhang.SelectedValue) == "-1")
                    {
                        cboNganhang.Focus();
                       
                        return;
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            string ma_pttt = Utility.sDbnull( cboPttt_rieng.SelectedValue);
                            decimal tong_tien = Utility.DecimaltoDbnull(txtSotien.Text,0);
                            if (tong_tien > 0)
                            {
                                DataRow[] arrDr = m_dtData.Select("ma_pttt='" + ma_pttt + "'");
                                if (arrDr.Length > 0)
                                {
                                    arrDr[0]["so_tien"] = tong_tien;
                                    arrDr[0]["ma_nganhang"]=ma_nganhang;
                                }
                                m_dtData.AcceptChanges();
                                txtSotien.Text = "";
                                cboPttt_rieng.SelectedIndex = -1;
                                cboPttt_rieng.Focus();
                            }
                            return;
                        }
                        else if (e.Control && e.KeyCode == Keys.C)
                        {
                            string ma_pttt = Utility.sDbnull( cboPttt_rieng.SelectedValue);
                            decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                            decimal tongtienkhac = 0;
                            decimal conlai = 0;
                            DataRow[] arrDr = m_dtData.Select("ma_pttt<>'" + ma_pttt + "'");
                            if (arrDr.Length > 0)
                            {
                                tongtienkhac = Utility.DecimaltoDbnull(arrDr.CopyToDataTable().Compute("SUM(so_tien)", "1=1"), 0);
                            }
                            conlai = Utility.DecimaltoDbnull(tong_tien, 0) - tongtienkhac;
                            arrDr = m_dtData.Select("ma_pttt='" + ma_pttt + "'");
                            if (arrDr.Length > 0)
                            {
                                arrDr[0]["so_tien"] = conlai;
                                arrDr[0]["ma_nganhang"] = ma_nganhang;
                            }
                            m_dtData.AcceptChanges();
                            cboPttt_rieng.SelectedIndex = -1;
                            return;
                        }
                        else if (e.Control && e.KeyCode == Keys.A)
                        {
                            string ma_pttt = Utility.sDbnull( cboPttt_rieng.SelectedValue);
                            decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                            foreach (DataRow dr in m_dtData.Rows)
                            {
                                if (Utility.sDbnull(dr["ma_pttt"], "") != ma_pttt)
                                {
                                    dr["so_tien"] = 0;
                                    dr["ma_nganhang"] = "";
                                }
                                else
                                {
                                    dr["so_tien"] = tong_tien;
                                    dr["ma_nganhang"] = ma_nganhang;
                                }
                            }
                            m_dtData.AcceptChanges();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                CheckSum();
            }
            
        }

        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                if (e.Control && e.KeyCode == Keys.C)
                {
                    string ma_pttt = Utility.GetValueFromGridColumn(grdList, "ma_pttt");
                    decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                    decimal tongtienkhac = 0;
                    decimal conlai = 0;
                    DataRow[] arrDr = m_dtData.Select("ma_pttt<>'" + ma_pttt + "'");
                    if (arrDr.Length > 0)
                    {
                        tongtienkhac = Utility.DecimaltoDbnull(arrDr.CopyToDataTable().Compute("SUM(so_tien)", "1=1"), 0);
                    }
                    conlai = Utility.DecimaltoDbnull(tong_tien, 0) - tongtienkhac;
                    arrDr = m_dtData.Select("ma_pttt='" + ma_pttt + "'");
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["so_tien"] = conlai;
                    }
                    m_dtData.AcceptChanges();
                    return;
                }
                if (e.Control && e.KeyCode == Keys.A)
                {
                    string ma_pttt = Utility.GetValueFromGridColumn(grdList, "ma_pttt");
                    decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                    decimal tongtienkhac = 0;
                    foreach (DataRow dr in m_dtData.Rows)
                    {
                        if (Utility.sDbnull(dr["ma_pttt"], "") != ma_pttt)
                        {
                            dr["so_tien"] = 0;
                        }
                        else
                        {
                            dr["so_tien"] = tong_tien;
                        }
                    }
                    m_dtData.AcceptChanges();
                    return;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                CheckSum();
            }

        }

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                if (e.Column.Key == "so_tien")
                {
                    errorProvider1.SetError(txtTongtien, "");
                    string ma_pttt=Utility.GetValueFromGridColumn(grdList,"ma_pttt");
                    decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                    decimal tongtienkhac = 0;
                    DataRow[] arrDr= m_dtData.Select("ma_pttt<>'" + ma_pttt + "'");
                    if (arrDr.Length > 0)
                    {
                        tongtienkhac = Utility.DecimaltoDbnull(arrDr.CopyToDataTable().Compute("SUM(so_tien)", "1=1"), 0);
                    }
                    if (tongtienkhac + Utility.DecimaltoDbnull(e.Value) > Utility.DecimaltoDbnull(tong_tien, 0))
                    {
                       // e.Cancel = true;
                        errorProvider1.SetError(txtTongtien, "Tổng tiền phân bổ theo các phương thức thanh toán cần phải bằng tổng tiền thanh toán. Mời bạn kiểm tra lại");
                    }
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
            }
            
        }
        void CheckSum()
        {
            errorProvider1.SetError(txtTongtien, "");
            if (Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
            {
                errorProvider1.SetError(txtTongtien, "Tổng tiền phân bổ theo các phương thức thanh toán cần phải bằng tổng tiền thanh toán. Mời bạn kiểm tra lại");
            }
        }
        void cmdAccept_Click(object sender, EventArgs e)
        {
            if (cboNganhangChung.Enabled && (Utility.sDbnull(cboNganhangChung.SelectedValue,"-1") == "-1" || Utility.sDbnull(cboNganhangChung.SelectedValue,"-1") == ""))
            {
                if (objTT != null && objTT.KieuThanhtoan == 0)
                {
                    Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt_chung.Text));
                    cboNganhangChung.Focus();
                    return;
                }
            }

            if (cboNganhang.Enabled && (Utility.sDbnull(cboNganhang.SelectedValue, "-1") == "-1" || Utility.sDbnull(cboNganhang.SelectedValue, "-1") == ""))
            {
                if (objTT != null && objTT.KieuThanhtoan == 0)
                {
                    Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt_rieng.Text));
                    cboNganhang.Focus();
                    return;
                }
            }
            //if (m_dtData.Select("so_tien<0").Length>0 || Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
              


                DataRow[] arrDr = null;
                arrDr = m_dtData.Select("so_tien>0");
                if (lstPhanbo.Contains(Utility.sDbnull(cboPttt_chung.SelectedValue)))
                {
                    if (m_dtData.Select("so_tien<0").Length > 0 || Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
                    {
                        errorProvider1.SetError(txtTongtien, string.Format("Tổng tiền phân bổ theo các Hình thức thanh toán cần = {0}. Mời bạn kiểm tra lại", txtTongtien.Text));
                        Utility.ShowMsg(string.Format("Tổng tiền phân bổ theo các Hình thức thanh toán cần = {0}. Mời bạn kiểm tra lại", txtTongtien.Text));
                        return;
                    }
                    //Kiểm tra các pttt bắt chọn ngân hàng
                    if (arrDr.Length > 0)
                    {
                        foreach (DataRow dr in arrDr)
                        {
                            string ma_pttt = Utility.sDbnull(dr["ma_pttt"]);
                            string ma_nganhang = Utility.sDbnull(dr["ma_nganhang"]);
                            string ten_pttt = Utility.sDbnull(dr["ten_pttt"]);
                            if (lstPTTTBatchonNganhang.Contains(ma_pttt) && (ma_nganhang == "" || ma_nganhang == "-1") )
                            {
                                if (objTT != null && objTT.KieuThanhtoan == 0)
                                {
                                    Utility.ShowMsg(string.Format("Bạn chưa chọn ngân hàng cho hình thức thanh toán {0}. Đề nghị đưa chuột vào cột ngân hàng ở dòng phương thức thanh toán {1} để chọn", ten_pttt, ten_pttt));
                                    return;
                                }
                            }
                        }

                    }
                }
                else
                {

                    //Kiểm tra nếu không phải chọn hình thức thanh toán là phân bổ mà số dòng phân bổ >1 thì cảnh báo
                    arrDr = m_dtData.Select("so_tien>0");
                    if (arrDr.Length > 1)
                    {
                        if (Utility.AcceptQuestion(string.Format("Dữ liệu trên lưới phân bổ không phù hợp với phương thức thanh toán {0}. Bạn có muốn hệ thống tự động phân bổ toàn bộ số tiền cho phương thức thanh toán này hay không?\nNhấn Yes để đồng ý. Nhấn No để tự phân bổ bằng tay trên lưới", cboPttt_chung.Text), "", true))
                        {
                            foreach (DataRow dr in m_dtData.Rows)
                            {
                                if (Utility.sDbnull(dr["ma_pttt"], "") != Utility.sDbnull(cboPttt_chung.SelectedValue))
                                    dr["so_tien"] = 0;
                                else
                                    dr["so_tien"] = Utility.DecimaltoDbnull(txtTongtien.Text);
                            }
                        }
                        else
                            return;
                    }
                    else if (arrDr.Length == 1)//Kiểm tra tiền phân bổ so với tổng tiền
                    {
                        if (m_dtData.Select("so_tien<0").Length > 0 || Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
                        {
                            errorProvider1.SetError(txtTongtien, string.Format("Tổng tiền phân bổ theo các Hình thức thanh toán cần = {0}. Mời bạn kiểm tra lại", txtTongtien.Text));
                            Utility.ShowMsg(string.Format("Tổng tiền phân bổ theo các Hình thức thanh toán cần = {0}. Mời bạn kiểm tra lại", txtTongtien.Text));
                            return;
                        }
                    }

                }
            arrDr = m_dtData.Select("so_tien>0");
            if (arrDr.Length <= 1 && lstPhanbo.Contains(Utility.sDbnull(cboPttt_chung.SelectedValue)))
            {
                Utility.ShowMsg(string.Format( "Bạn đang chọn phân bổ ra nhiều hình thức thanh toán khác nhau trong khi dữ liệu phân bổ đang chỉ có {0}.\nNếu không muốn phân bổ thì chọn lại hình thức thanh toán chung",arrDr[0]["ten_pttt"]));
                cboPttt_chung.Focus();
                return;
            }
            //if (m_dtData.Select("so_tien<0").Length > 0 || Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
            //{
            //    errorProvider1.SetError(txtTongtien, string.Format("Tổng tiền phân bổ theo các Hình thức thanh toán cần = {0}. Mời bạn kiểm tra lại", txtTongtien.Text));
            //    Utility.ShowMsg(string.Format("Tổng tiền phân bổ theo các Hình thức thanh toán cần = {0}. Mời bạn kiểm tra lại", txtTongtien.Text));
            //    return;
            //}
            string errMsg = "";
            if (_THANHTOAN.UpdateTienphanbotheoPttt(arrDr, objLuotkham, id_thanhtoan, id_phieuthu, id_tamung, Utility.sDbnull(cboPttt_chung.SelectedValue), cboNganhangChung.Enabled ? Utility.sDbnull(cboNganhangChung.SelectedValue) : "-1", Utility.DecimaltoDbnull(txtTongtien.Text),loai_phanbo, ref errMsg) != ActionResult.Success)
            {
                Utility.ShowMsg(errMsg);
            }
            else
            {
                isSaved = true;
                Utility.ShowMsg("Đã phân bổ HTTT thành công. Nhấn OK để kết thúc");
                if (_OnChangePTTT != null) _OnChangePTTT(id_thanhtoan, Utility.sDbnull(cboPttt_chung.SelectedValue), cboPttt_chung.Text, cboNganhangChung.Enabled ? Utility.sDbnull(cboNganhangChung.SelectedValue) : "", cboNganhangChung.Enabled ? cboNganhangChung.Text : "");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        /// <summary>
        /// /hàm thực hiện load thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhanbotientheoPTTT_Load(object sender, EventArgs e)
        {
            GetData();
            LoadPtttNganhang();
            cboPttt_chung_SelectedIndexChanged(cboPttt_chung, e);
            //Utility.focusCell(grdList, "so_tien");
            cboPttt_chung.Focus();
            m_blnLoaded = true;
            ModifyQRButtons();
        }
        DataTable dtPttt = new DataTable();
        DataTable dtPttt_rieng = new DataTable();
        DataTable dtNganhang = new DataTable();
        void LoadPtttNganhang()
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "PHUONGTHUCTHANHTOAN", "NGANHANG" }, true);
            dtPttt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "PHUONGTHUCTHANHTOAN");
            dtNganhang = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "NGANHANG");
            cboPttt_chung.DataSource = dtPttt;
            cboPttt_chung.ValueMember = DmucChung.Columns.Ma;
            cboPttt_chung.DisplayMember = DmucChung.Columns.Ten;
            cboNganhang.DataSource = dtNganhang;
            cboNganhang.ValueMember = DmucChung.Columns.Ma;
            cboNganhang.DisplayMember = DmucChung.Columns.Ten;

            cboNganhangChung.DataSource = dtNganhang;
            cboNganhangChung.ValueMember = DmucChung.Columns.Ma;
            cboNganhangChung.DisplayMember = DmucChung.Columns.Ten;


            dtPttt_rieng = getPTTT();
            cboPttt_rieng.DataSource = dtPttt_rieng;
            cboPttt_rieng.ValueMember = DmucChung.Columns.Ma;
            cboPttt_rieng.DisplayMember = DmucChung.Columns.Ten;

            cboPttt_chung.SelectedValue = ma_pttt;
            cboNganhangChung.SelectedValue = ma_nganhang;
            //cboPttt_chung.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtPttt);
            //cboNganhang.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtNganhang);
        }

        DataTable getPTTT()
        {
            DataTable dt = dtPttt.Clone();
            foreach (DataRow dr in dtPttt.Rows)
            {
                if (!lstPhanbo.Contains(Utility.sDbnull(dr["MA"], "")))
                    dt.ImportRow(dr);
            }
            return dt;
        }
        void BoDulieuPTTTPhanBo()
        {
            DataTable dt = m_dtData.Clone();
            foreach (DataRow dr in m_dtData.Rows)
            {
                if (!lstPhanbo.Contains(Utility.sDbnull(dr["ma_pttt"], "")))
                    dt.ImportRow(dr);
            }
            m_dtData = dt.Copy();
        }
        /// <summary>
        /// lấy thông tin dữ liệu
        /// </summary>
        private void GetData()
        {
            try
            {
                DataSet dsData = _THANHTOAN.KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(id_tamung > 0 ? -1 : id_thanhtoan, id_phieuthu, id_tamung);
                m_dtData = dsData.Tables[0];
                decimal tongtienHU = 0;
                if (dsData.Tables[1].Rows.Count > 0)
                    tongtienHU = Utility.DecimaltoDbnull(dsData.Tables[1].Rows[0]["so_tien"]);
                
                if (m_dtData != null && m_dtData.Rows.Count > 0)
                {

                    //if (m_dtData.Select("Tong_tien>0").Length > 0)
                    //{
                    //    decimal tongtien = Utility.DecimaltoDbnull(m_dtData.Select("Tong_tien>0")[0]["Tong_tien"], 0);
                    //    if (TongtienPhanBo > 0) tongtien = TongtienPhanBo;
                    //    txtTongtien.Text = Utility.sDbnull(tongtien - tongtienHU, "0");
                    //}
                    //else
                    //{
                        if (id_thanhtoan > 0 && id_tamung<=0 )
                        {
                            KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                            if (objThanhtoan != null)
                            {
                                if (objThanhtoan.TongtienChietkhau < objThanhtoan.TongtienChietkhauChitiet) objThanhtoan.TongtienChietkhau =Utility.DecimaltoDbnull( objThanhtoan.TongtienChietkhauChitiet,0);
                                txtTongtien.Text = Utility.sDbnull(objThanhtoan.BnhanChitra - objThanhtoan.TongtienChietkhau - tongtienHU, "0");
                            }

                        }
                        else if (id_tamung > 0)
                        {
                            NoitruTamung objTU = NoitruTamung.FetchByID(id_tamung);
                            if (objTU != null)
                                txtTongtien.Text = Utility.sDbnull(objTU.SoTien, "0");
                        }
                        else if (id_phieuthu > 0)
                        {
                            KcbPhieuthu objPT = KcbPhieuthu.FetchByID(id_phieuthu);
                            if (objPT != null)
                                txtTongtien.Text = Utility.sDbnull(objPT.SoTien, "0");
                        }
                        else
                        {
                            if (m_dtData.Select("Tong_tien>0").Length > 0)
                            {
                                decimal tongtien = Utility.DecimaltoDbnull(m_dtData.Select("Tong_tien>0")[0]["Tong_tien"], 0);
                                if (TongtienPhanBo > 0) tongtien = TongtienPhanBo;
                                txtTongtien.Text = Utility.sDbnull(tongtien - tongtienHU, "0");
                            }
                        }
                    //}
                    BoDulieuPTTTPhanBo();

                    m_dtData.AcceptChanges();
                    grdList.DataSource = m_dtData;
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
                foreach (Control control in pnlInfor.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();
                        txtFormantTongTien = ((EditBox)(control));
                        txtFormantTongTien.Clear();
                        txtFormantTongTien.ReadOnly = true;
                        txtFormantTongTien.TextAlignment = TextAlignment.Far;
                        txtFormantTongTien.TextChanged += txtEventTongTien_TextChanged;
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txt = ((EditBox)(sender));
            Utility.FormatCurrencyHIS(txt);
        }
        
        
        /// <summary>
        /// hàm thực hiện thoát khỏi form hienj tại
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thự hiện việc hủy thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhanbotientheoPTTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdAccept_Click(cmdAccept, new EventArgs());
        }

        private void cboPttt_chung_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ma_pttt = Utility.sDbnull(cboPttt_chung.SelectedValue);
            cboNganhangChung.Enabled = lstPTTTBatchonNganhang.Contains(this.ma_pttt);
            if (lstPhanbo.Contains(Utility.sDbnull(cboPttt_chung.SelectedValue)))
            {
                cboPttt_rieng.Enabled = true;
                txtSotien.Enabled = true;
                cboPttt_rieng.Focus();

                cboPttt_rieng_SelectedIndexChanged(cboPttt_rieng, new EventArgs());
                //Clear All//Bỏ 230824
                //foreach (DataRow dr in m_dtData.Rows)
                //{
                //    dr["so_tien"] = 0;
                //    dr["ma_nganhang"] = "";
                //}
                m_dtData.AcceptChanges();
            }
            else
            {
                cboPttt_rieng.Enabled = false;
                txtSotien.Enabled = true;
                txtSotien.Focus();
                //Kiểm tra nếu không phải chọn hình thức thanh toán là phân bổ mà số dòng phân bổ >1 thì cảnh báo
                DataRow[] arrDr = m_dtData.Select("so_tien>0");
                if (arrDr.Length > 1)
                    if (Utility.AcceptQuestion(string.Format("Dữ liệu trên lưới phân bổ không phù hợp với phương thức thanh toán {0}. Bạn có muốn hệ thống tự động phân bổ toàn bộ số tiền cho phương thức thanh toán này hay không?\nNhấn Yes để đồng ý. Nhấn No để tự phân bổ bằng tay trên lưới", cboPttt_chung.Text), "", true))
                    {
                        foreach (DataRow dr in m_dtData.Rows)
                        {
                            if (Utility.sDbnull(dr["ma_pttt"], "") != Utility.sDbnull(cboPttt_chung.SelectedValue))
                                dr["so_tien"] = 0;
                            else
                                dr["so_tien"] = Utility.DecimaltoDbnull(txtTongtien.Text);
                        }
                    }
            }
        }

        private void cboPttt_rieng_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboNganhang.Enabled = lstPTTTBatchonNganhang.Contains(Utility.sDbnull(cboPttt_rieng.SelectedValue));
            if (cboNganhang.Enabled)
                cboNganhang.Focus();
            else
                txtSotien.Focus();
        }
        
       
       
    }
}
