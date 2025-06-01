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
    public partial class frm_PhanbotientheoPTTT_V1 : Form
    {
        public long id_thanhtoan = -1;
        public long id_phieuthu = -1;
        public long id_tamung = -1;
        public bool m_blnCancel = true;
        public int Id_HD_LOG = -1;
        public int TotalPayment = 0;
        bool m_blnLoaded = false;
        public KcbLuotkham objLuotkham = null;
        private DataTable m_dtData = new DataTable();
        List<string> lstPTTTBatchonNganhang = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
        List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
        public string ma_pttt = "";
        public string ma_nganhang = "";
        public delegate void OnChangePTTT(long id_thanhtoan, string ma_pttt,string ten_pttt,string ma_nganhang,string ten_nganhang);
        public event OnChangePTTT _OnChangePTTT;
        public frm_PhanbotientheoPTTT_V1(long id_thanhtoan, long id_phieuthu,long id_tamung,string ma_pttt,string ma_nganhang)
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            this.id_phieuthu = id_phieuthu;
            this.id_tamung = id_tamung;
            InitEvents();
            setProperties();
            
        }
        void InitEvents()
        {
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.Load += new System.EventHandler(this.frm_PhanbotientheoPTTT_V1_Load);
            this.KeyDown += new KeyEventHandler(frm_PhanbotientheoPTTT_V1_KeyDown);
            cmdAccept.Click += cmdAccept_Click;
            grdList.KeyDown += grdList_KeyDown;
            grdList.SelectionChanged += grdList_SelectionChanged;
            grdList.UpdatingCell += grdList_UpdatingCell;
            txtSotien.KeyDown += txtSotien_KeyDown;
            txtPttt_chung._OnEnterMe += txtPttt_chung__OnEnterMe;
            autoPttt._OnEnterMe += autoPttt__OnEnterMe;
        }

        void autoPttt__OnEnterMe()
        {
            autoNganhang.Enabled = lstPTTTBatchonNganhang.Contains(autoPttt.MyCode);
            if (autoNganhang.Enabled)
                autoNganhang.Focus();  
        }

        void txtPttt_chung__OnEnterMe()
        {
            this.ma_pttt = txtPttt_chung.myCode;
            autoNganhangchung.Enabled = lstPTTTBatchonNganhang.Contains(this.ma_pttt);
            if (lstPhanbo.Contains(txtPttt_chung.myCode))
            {
                autoPttt.Enabled = true;
                txtSotien.Enabled = true;
                autoPttt.Focus();
                autoPttt.SelectAll();
                autoPttt.RaiseEnterEvents();
                //Clear All
                foreach (DataRow dr in m_dtData.Rows)
                {
                    dr["so_tien"] = 0;

                }
                m_dtData.AcceptChanges();
            }
            else
            {
                autoPttt.Enabled = false;
                txtSotien.Enabled = true;
                txtSotien.Focus();
                //Kiểm tra nếu không phải chọn hình thức thanh toán là phân bổ mà số dòng phân bổ >1 thì cảnh báo
                DataRow[] arrDr = m_dtData.Select("so_tien>0");
                if (arrDr.Length > 1)
                    if (Utility.AcceptQuestion(string.Format("Dữ liệu trên lưới phân bổ không phù hợp với phương thức thanh toán {0}. Bạn có muốn hệ thống tự động phân bổ toàn bộ số tiền cho phương thức thanh toán này hay không?\nNhấn Yes để đồng ý. Nhấn No để tự phân bổ bằng tay trên lưới", txtPttt_chung.Text), "", true))
                    {
                        foreach (DataRow dr in m_dtData.Rows)
                        {
                            if (Utility.sDbnull(dr["ma_pttt"], "") != txtPttt_chung.MyCode)
                                dr["so_tien"] = 0;
                            else
                                dr["so_tien"] = Utility.DecimaltoDbnull(txtTongtien.Text);
                        }
                    }
            }

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
                if (autoPttt.myCode != "-1" && autoNganhang.Enabled && autoNganhang.myCode != "-1")
                {
                    ma_nganhang = autoNganhang.myCode;
                    errorProvider1.SetError(autoNganhang, string.Format("Bạn phải chọn ngân hàng ứng với phương thức thanh toán {0}", autoPttt.Text));
                }
                if (e.KeyCode == Keys.Enter || (e.Control && e.KeyCode == Keys.A) || (e.Control && e.KeyCode == Keys.C))
                {
                    if (autoPttt.myCode == "-1")
                    {
                        autoPttt.Focus();
                        autoPttt.SelectAll();
                        return;
                    }
                    else if (autoPttt.myCode != "-1" && autoNganhang.Enabled && autoNganhang.myCode == "-1")
                    {
                        autoNganhang.Focus();
                        autoNganhang.SelectAll();
                        return;
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            string ma_pttt = autoPttt.myCode;
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
                                autoPttt.SetCode("-1");
                                autoPttt.Focus();
                                autoPttt.SelectAll();
                            }
                            return;
                        }
                        else if (e.Control && e.KeyCode == Keys.C)
                        {
                            string ma_pttt = autoPttt.myCode;
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
                            autoPttt.SetCode("-1");
                            return;
                        }
                        else if (e.Control && e.KeyCode == Keys.A)
                        {
                            string ma_pttt = autoPttt.myCode;
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
            if (autoNganhangchung.Enabled && autoNganhangchung.myCode == "-1")
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", txtPttt_chung.Text));
                autoNganhangchung.Focus();
                return ;
            }
            //if (m_dtData.Select("so_tien<0").Length>0 || Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
                if (m_dtData.Select("so_tien<0").Length > 0 || Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
            {
                errorProvider1.SetError(txtTongtien, "Tổng tiền phân bổ theo các phương thức thanh toán cần phải bằng tổng tiền thanh toán và tiền PTTT không được phép chứa giá trị nhỏ hơn không. Mời bạn kiểm tra lại");
                return;
            }

            DataRow[] arrDr = m_dtData.Select("so_tien>0");
            if (arrDr.Length <= 1 && lstPhanbo.Contains(txtPttt_chung.MyCode))
            {
                Utility.ShowMsg(string.Format( "Bạn đang chọn phân bổ ra nhiều hình thức thanh toán khác nhau trong khi dữ liệu phân bổ đang chỉ có {0}.\nNếu không muốn phân bổ thì chọn lại hình thức thanh toán chung",arrDr[0]["ten_pttt"]));
                txtPttt_chung.Focus();
                txtPttt_chung.SelectAll();
                return;
            }
            //Kiểm tra các pttt bắt chọn ngân hàng
            if (arrDr.Length > 0)
            {
                foreach (DataRow dr in arrDr)
                {
                    string ma_pttt=Utility.sDbnull(dr["ma_pttt"]);
                    string ma_nganhang=Utility.sDbnull(dr["ma_nganhang"]);
                    string ten_pttt=Utility.sDbnull(dr["ten_pttt"]);
                    if (lstPTTTBatchonNganhang.Contains(ma_pttt) && (ma_nganhang == "" || ma_nganhang == "-1"))
                    {
                        Utility.ShowMsg(string.Format("Bạn chưa chọn ngân hàng cho hình thức thanh toán {0}. Đề nghị đưa chuột vào cột ngân hàng ở dòng phương thức thanh toán {1} để chọn", ten_pttt, ten_pttt));
                        return;
                    }
                }

            }
            string errMsg = "";
            if (_THANHTOAN.UpdateTienphanbotheoPttt(arrDr, objLuotkham, id_thanhtoan, id_phieuthu, id_tamung, txtPttt_chung.MyCode, autoNganhangchung.Enabled ? autoNganhangchung.myCode : "-1", Utility.DecimaltoDbnull(txtTongtien.Text),0, ref errMsg) != ActionResult.Success)
            {
                Utility.ShowMsg(errMsg);
            }
            else
            {
                Utility.ShowMsg("Đã phân bổ HTTT thành công. Nhấn OK để kết thúc");
                if (_OnChangePTTT != null) _OnChangePTTT(id_thanhtoan, txtPttt_chung.MyCode, txtPttt_chung.Text, autoNganhangchung.Enabled ? autoNganhangchung.myCode : "", autoNganhangchung.Enabled ? autoNganhangchung.Text : "");
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
        private void frm_PhanbotientheoPTTT_V1_Load(object sender, EventArgs e)
        {
            GetData();
            txtPttt_chung.Init();
            autoPttt.Init(getPTTT());
            autoNganhang.Init();
            autoNganhangchung.Init();
            txtPttt_chung.SetCode(ma_pttt);
            autoNganhangchung.SetCode(ma_nganhang);
            txtPttt_chung.RaiseEnterEvents();
           
            //Utility.focusCell(grdList, "so_tien");
            txtPttt_chung.Focus();
            txtPttt_chung.SelectAll();
            m_blnLoaded = true;
        }
        DataTable getPTTT()
        {
            DataTable dt = txtPttt_chung.dtData.Clone();
            foreach (DataRow dr in txtPttt_chung.dtData.Rows)
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
                m_dtData = _THANHTOAN.KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(id_thanhtoan,id_phieuthu,id_tamung).Tables[0];
                BoDulieuPTTTPhanBo();
                m_dtData.AcceptChanges();
                grdList.DataSource = m_dtData;
                if (m_dtData != null && m_dtData.Rows.Count > 0)
                {
                    if (m_dtData.Select("Tong_tien>0").Length > 0)
                        txtTongtien.Text = Utility.sDbnull(m_dtData.Select("Tong_tien>0")[0]["Tong_tien"], "0");
                    else
                    {
                        KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                        if (objThanhtoan != null)
                            txtTongtien.Text = Utility.sDbnull(objThanhtoan.BnhanChitra - objThanhtoan.TongtienChietkhau, "0");
                    }
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
        private void frm_PhanbotientheoPTTT_V1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdAccept_Click(cmdAccept, new EventArgs());
        }
        
       
       
    }
}
