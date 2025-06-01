using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using System.IO;
using VNS.HIS.UI.Forms.Cauhinh;
using Janus.Windows.GridEX;
using VNS.HIS.UI.THANHTOAN;
using System.Transactions;
namespace VNS.HIS.UCs.Noitru
{
    public partial class ucTamung : UserControl
    {
        public delegate void OnChangedData();

        public event OnChangedData _OnChangedData;

        action m_enAct = action.FirstOrFinished;
        bool AllowedChanged = false;
        bool AllowedChanged_maskedEdit = false;
        NoitruTamung objTamung = null;
        private DataTable m_dtTimKiembenhNhan = new DataTable();
        public TrangthaiNoitru _TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        public bool callfromMenu = true;
        public KcbLuotkham objLuotkham = null;
        byte v_bytNoitru = 0;
        bool isLichsu = false;
        public ucTamung()
        {
            InitializeComponent();
            InitEvents();
            

        }
        bool hasLoadedDmuc = false;
        DataTable dtPttt = new DataTable();
        DataTable dtPttt_rieng = new DataTable();
        DataTable dtNganhang = new DataTable();
        void LoadPtttNganhang()
        {
            if (hasLoadedDmuc) return;
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
                GridEXColumn _colmaPttt = grdTamung.RootTable.Columns["ma_pttt"];
                _colmaPttt.HasValueList = true;
                _colmaPttt.LimitToList = true;

                GridEXValueListItemCollection _colmaPttt_Collection = grdTamung.RootTable.Columns["ma_pttt"].ValueList;
                foreach (DataRow item in dtPTTT.Rows)
                {
                    _colmaPttt_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }

                DataTable dtNganhang = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("NGANHANG").And(DmucChung.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
                GridEXColumn _colIDNganHang = grdTamung.RootTable.Columns["ma_nganhang"];
                _colIDNganHang.HasValueList = true;
                _colIDNganHang.LimitToList = true;

                GridEXValueListItemCollection _colIDNganHang_Collection = grdTamung.RootTable.Columns["ma_nganhang"].ValueList;
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
        void modifyLichsu()
        {
            if (isLichsu)
            {
                pnlInfor.Height = 130;
                pnlAction.Height = 0;
                pnlAction.Visible = false;
            }
        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if ((keyData == Keys.Right) || (keyData == Keys.Left) ||
        //        (keyData == Keys.Up) || (keyData == Keys.Down))
        //    {
        //        //Do custom stuff
        //        //true if key was processed by control, false otherwise
        //        return true;
        //    }
        //    else
        //    {
        //        return base.ProcessCmdKey(ref msg, keyData);
        //    }
        //}
        public void ChangePatients(KcbLuotkham objLuotkham, string maloailydothu, byte v_bytNoitru)
        {
            this.v_bytNoitru = v_bytNoitru;
            AllowedChanged = false;
            this.objLuotkham = objLuotkham;
            if (maloailydothu != string.Empty)
                txtLydo.LOAI_DANHMUC = maloailydothu;
            Init();
        }

        void InitEvents()
        {

            grdTamung.SelectionChanged += new EventHandler(grdTamung_SelectionChanged);
            grdTamung.KeyDown += new KeyEventHandler(grdTamung_KeyDown);
            grdTamung.ColumnButtonClick += grdTamung_ColumnButtonClick;
            grdTamung.CellValueChanged += grdTamung_CellValueChanged;
            txtSotien._OnTextChanged += new MaskedTextBox.MaskedTextBox.OnTextChanged(txtSotien__OnTextChanged);
            grdHoanung.ColumnButtonClick += grdHoanung_ColumnButtonClick;
            txtLydo._OnShowData += txtLydo__OnShowData;
            txtLydo._OnSaveAs += txtLydo__OnSaveAs;
            grdHuyTU.ColumnButtonClick += grdHuyTU_ColumnButtonClick;
            cmdthemmoi.Click += new EventHandler(cmdthemmoi_Click);
            cmdSua.Click += new EventHandler(cmdSua_Click);
            cmdxoa.Click += new EventHandler(cmdxoa_Click);
            cmdIn.Click += new EventHandler(cmdIn_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);
            cmdGhi.Click += new EventHandler(cmdGhi_Click);
            cmdConfig.Click += cmdConfig_Click;
            chkSaveAndPrint.CheckedChanged += chkSaveAndPrint_CheckedChanged;
            chkPrintPreview.CheckedChanged += chkPrintPreview_CheckedChanged;
            //foreach (Control ctrl in panel2.Controls)
            //    ctrl.KeyDown += ctrl_KeyDown;
            //txtPttt._OnEnterMe += txtPttt__OnEnterMe;
            autoNguonkiqui._OnEnterMe += autoNguonkiqui__OnEnterMe;
            //txtPttt._OnShowData += txtPttt__OnShowData;
            autoNguonkiqui._OnShowData += autoNguonkiqui__OnShowData;
            //autoNganhang._OnShowData += autoNganhang__OnShowData;
            uiTab1.SelectedTabChanged+=uiTab1_SelectedTabChanged;
            cmdInphieuhoanung.Click+=cmdInphieuhoanung_Click;
            cmdHuyhoanung.Click+=cmdHuyhoanung_Click;
            cmdHoanung.Click+=cmdHoanung_Click;
            grdHoanung.SelectionChanged += GrdHoanung_SelectionChanged;
            grdHuyTU.SelectionChanged += GrdHuyTU_SelectionChanged;
        }

        private void GrdHuyTU_SelectionChanged(object sender, EventArgs e)
        {
            NapdichvuTamung(Utility.isValidGrid(grdHuyTU) ? Utility.Int64Dbnull(grdHuyTU.GetValue("id")) : -1);
        }

        private void GrdHoanung_SelectionChanged(object sender, EventArgs e)
        {
            NapdichvuTamung(Utility.isValidGrid(grdHoanung) ? Utility.Int64Dbnull(grdHoanung.GetValue("id")) : -1);
        }

        void NapdichvuTamung(long id_tamung)
        {
            DataTable dtData = SPs.SpTamungLaydichvuKemtheo(id_tamung).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx_Basic(grdDvuKemtheo, dtData, true, true, "1=1", "ten_dichvu");
        }
        void grdHuyTU_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "RHTU")
                {
                    if (!Utility.Coquyen("TAMUNG_QUYEN_KHOIPHUC_HUYTAMUNG"))
                    {
                        Utility.ShowMsg("Bạn không có quyền phân bổ hình thức thanh toán(TAMUNG_QUYEN_KHOIPHUC_HUYTAMUNG). Vui lòng liên hệ quản trị để được cấp quyền");
                        return;
                    }
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn khôi phục tạm ứng đã hủy này hay không?", "Xác nhận", true))
                    {
                        NoitruTamung objtemp = NoitruTamung.FetchByID(Utility.Int64Dbnull(grdHuyTU.GetValue(NoitruTamung.Columns.Id)));
                        if (objtemp != null)
                        {
                            
                                if (noitru_TamungHoanung.RestoreTienTamung(objtemp))
                                {
                                    Utility.SetMsg(lblMsg, string.Format("Khôi phục bản ghi tạm ứng {0} thành công", txtSotien.Text), false);
                                    DataRow drDelete = Utility.getCurrentDataRow(grdHuyTU);
                                    if (drDelete != null)
                                    {
                                        //Thêm vào dòng hủy
                                        drDelete["tthai_huy"] = 0;
                                        drDelete["ngay_huy"] = DBNull.Value ;
                                        drDelete["nguoi_huy"] = "";
                                        drDelete["lydo_huy"] ="";
                                        drDelete["ma_httt_huy"] = "";
                                        drDelete["ma_nganhang_huy"] = "";
                                        m_dtTamung.ImportRow(drDelete);
                                        m_dtTamung.AcceptChanges();
                                        m_dtHuyTU.Rows.Remove(drDelete);
                                        m_dtHuyTU.AcceptChanges();
                                       
                                    }
                                }
                            
                            SetControlStatus();
                        }
                        else
                        {
                            Utility.SetMsg(lblMsg, string.Format("Bạn cần chọn dòng tạm ứng cần xóa"), true);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        void grdHoanung_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "PBHU")
            {
                if (!Utility.Coquyen("THANHTOAN_QUYEN_PHANBOPTTT"))
                {
                    Utility.ShowMsg("Bạn không có quyền phân bổ hình thức thanh toán(THANHTOAN_QUYEN_PHANBOPTTT). Vui lòng liên hệ quản trị để được cấp quyền");
                    return;
                }
                PhanboHU();
            } 
        }
        void PhanboHU()
        {
            if (!Utility.isValidGrid(grdHoanung)) return;
            long id_tamung = Utility.Int64Dbnull(grdHoanung.CurrentRow.Cells[NoitruTamung.Columns.Id].Value, -1);
            long IdThanhtoan =  Utility.Int64Dbnull(grdHoanung.CurrentRow.Cells[NoitruTamung.Columns.IdThanhtoan].Value, -1);
            string ma_pttt = Utility.sDbnull(grdHoanung.CurrentRow.Cells[NoitruTamung.Columns.MaPttt].Value, "TM");
            string ma_nganhang = Utility.sDbnull(grdHoanung.CurrentRow.Cells[KcbThanhtoan.Columns.MaNganhang].Value, "TM");
            frm_PhanbotientheoPTTT _PhanbotientheoPTTT = new frm_PhanbotientheoPTTT(IdThanhtoan, -1, id_tamung, ma_pttt, ma_nganhang);
            _PhanbotientheoPTTT.loai_phanbo = Convert.ToByte(1);
            _PhanbotientheoPTTT.TongtienPhanBo = Utility.DecimaltoDbnull(grdHoanung.CurrentRow.Cells[NoitruTamung.Columns.SoTien].Value, -1);
            _PhanbotientheoPTTT._OnChangePTTT += _PhanbotientheoPTTT__OnChangePTTTHU;
            _PhanbotientheoPTTT.objLuotkham = this.objLuotkham;
            _PhanbotientheoPTTT.ShowDialog();
        }
        void _PhanbotientheoPTTT__OnChangePTTTHU(long id_thanhtoan, string ma_pttt, string ten_pttt, string ma_nganhang, string ten_nganhang)
        {
            try
            {
                DataRow dr = ((DataRowView)grdHoanung.CurrentRow.DataRow).Row;
                dr["ma_pttt"] = ma_pttt;
                dr["ten_pttt"] = ten_pttt;
                dr["ma_nganhang"] = ma_nganhang;
                dr["ten_nganhang"] = ten_nganhang;
                m_dtHoanung.AcceptChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        void grdTamung_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "ma_pttt")
            {
                if (!Utility.Coquyen("THANHTOAN_SUA_PTTT"))
                {
                    Utility.ShowMsg("Bạn không có quyền sửa đổi phương thức thanh toán (THANHTOAN_SUA_PTTT). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                    return;
                }
                //Cập nhật lại PTTT
                if (!Utility.isValidGrid(grdTamung)) return;

                CapnhatPTTT_Grid();
            }
            else if (e.Column.Key == "ma_nganhang")
            {
                if (!Utility.Coquyen("THANHTOAN_SUA_PTTT"))
                {
                    Utility.ShowMsg("Bạn không có quyền sửa đổi phương thức thanh toán (THANHTOAN_SUA_PTTT). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                    return;
                }

                string ma_pttt = grdTamung.GetValue("ma_pttt").ToString();
                string ma_nganhangcu = grdTamung.CurrentRow.Cells["ma_nganhang"].Value.ToString();
                string ma_nganhang = Utility.sDbnull(grdTamung.GetValue("ma_nganhang").ToString(), "-1");

                List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                if (lstPTTT.Contains(ma_pttt) && (ma_nganhang == "-1" || ma_nganhang.Length == 0))//Đợi chọn ngân hàng
                {
                    return;
                }
                CapnhatPTTT_Grid();
            }
        }
        void CapnhatPTTT_Grid()
        {
            try
            {
                List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                Int32 idTU = Utility.Int32Dbnull(grdTamung.CurrentRow.Cells[NoitruTamung.Columns.Id].Value, -1);
                NoitruTamung objTU = NoitruTamung.FetchByID(idTU);
                string ma_pttt = grdTamung.GetValue("ma_pttt").ToString();
                string ma_pttt_cu = objTU.MaPttt;
                string ten_pttt_cu = grdTamung.CurrentRow.Cells["ten_pttt"].Value.ToString();
                string ma_nganhang = Utility.sDbnull(grdTamung.GetValue("ma_nganhang").ToString(), "-1");
                if (!lstPTTT.Contains(ma_pttt))
                    if (ma_pttt == ma_pttt_cu) return;
                
                DataRow[] arrDr = m_dtTamung.Select("id=" + idTU.ToString());
                if (arrDr.Length <= 0) return;
                

                if (!lstPTTT.Contains(ma_pttt))
                    ma_nganhang = "";
                if (lstPTTT.Contains(ma_pttt) && (ma_nganhang == "-1" || ma_nganhang.Length == 0))//Đợi chọn ngân hàng
                {
                    Utility.ShowMsg("Bạn cần chọn ngân hàng");
                    Utility.focusCell(grdTamung, "ma_nganhang");
                    return;
                }
                if (!isValidPttt_Nganhang_Grid())
                    return;
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thay đổi phương thức thanh toán từ {0} sang {1}", arrDr[0]["ten_pttt"], ma_pttt), "Xác nhận cập nhật PTTT", true))
                {
                    Utility.ShowMsg("Bạn vừa chọn hủy cập nhật phương thức thanh toán. Nhấn OK để kết thúc");
                    return;
                }
                List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                DataTable dtPhanbo = new KCB_THANHTOAN().KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(-1l, -1l, objTU.Id).Tables[0];
                //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                if (lstPhanbo.Contains(ma_pttt))
                {
                    if (dtPhanbo.Select("so_tien>0").Length <= 1)
                        Phanbo();
                }
                else
                {
                    if (dtPhanbo.Select("so_tien>0").Length > 1)//Không phải chọn phân bổ mà số lượng dòng phân bổ >1 thì thông báo người dùng lựa chọn
                    {
                        if (Utility.AcceptQuestion(string.Format("Lần phân bổ gần nhất bạn đang phân bổ tiền cho nhiều phương thức thanh toán. Trong khi hình thức thanh toán bạn đang chọn ({0}) không phải là phân bổ. Bạn có muốn hệ thống tự động chuyển tất cả số tiền sang hình thức vừa chọn {1} hay không?\n.Nhấn Yes để đồng ý. Nhấn No để xem lại thông tin phân bổ", ma_pttt, ma_pttt), "Cảnh báo và gợi ý", true))
                        {
                            Capnhatphanbo11(dtPhanbo, objTU, ma_pttt, ma_nganhang);
                        }
                        else
                            Phanbo();
                    }
                    else if (dtPhanbo.Select("so_tien>0").Length == 1)//Thực hiện cập nhật luôn
                    {
                        Capnhatphanbo11(dtPhanbo, objTU, ma_pttt, ma_nganhang);
                    }


                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void Capnhatphanbo11(DataTable dtPhanbo, NoitruTamung objTU, string ma_pttt, string ma_nganhang)
        {
            decimal so_tien = Utility.DecimaltoDbnull(dtPhanbo.Compute("sum(so_tien)", "1=1"), 0);

            using (var scope = new TransactionScope())
            {
                using (var sh = new SharedDbConnectionScope())
                {

                    new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema).Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdTamung).IsEqualTo(objTU.Id).Execute();

                    if (objTU != null)
                    {
                        objTU.MaPttt = ma_pttt;
                        objTU.MaNganhang = ma_nganhang;
                        objTU.MarkOld();
                        objTU.IsNew = false;
                        objTU.Save();
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(-1l, -1l, objTU.Id, objTU.MaPttt, objTU.MaNganhang,
          objTU.IdBenhnhan, objTU.MaLuotkham, objTU.Noitru, so_tien, so_tien, globalVariables.UserName, DateTime.Now, "", DateTime.Now, -1, 0, (byte)1).Execute();
                    }
                }
                scope.Complete();
            }
            foreach (DataRow dr in m_dtTamung.Rows)
                if (dr["Id"].ToString() == objTU.Id.ToString())
                {
                    dr["ma_pttt"] = ma_pttt;
                    dr["ten_pttt"] = cboPttt.Text;
                    dr["ma_nganhang"] = ma_nganhang;
                    dr["ten_nganhang"] = ma_nganhang;
                }
            m_dtTamung.AcceptChanges();
            Utility.ShowMsg("Cập nhật thông tin hình thức thanh toán thành công");
        }
        bool isValidPttt_Nganhang_Grid()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            string ma_pttt = grdTamung.GetValue("ma_pttt").ToString();
            string ma_nganhang = Utility.sDbnull(grdTamung.GetValue("ma_nganhang").ToString(), "-1");
            if (lstPTTT.Contains(ma_pttt) && (ma_nganhang.Length <= 0 || ma_nganhang == "-1"))
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", ma_pttt));
                cboNganhang.Focus();
                return false;
            }
            return true;
        }
        void grdTamung_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "cmdPB")
            {
                if (!Utility.Coquyen("THANHTOAN_QUYEN_PHANBOPTTT"))
                {
                    Utility.ShowMsg("Bạn không có quyền phân bổ hình thức thanh toán(THANHTOAN_QUYEN_PHANBOPTTT). Vui lòng liên hệ quản trị để được cấp quyền");
                    return;
                }
                Phanbo();
            }
        }
        
        void autoNganhang__OnShowData()
        {
            //DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(autoNganhang.LOAI_DANHMUC);
            //_DMUC_DCHUNG.ShowDialog();
            //if (!_DMUC_DCHUNG.m_blnCancel)
            //{
            //    string oldCode = autoNganhang.myCode;
            //    autoNganhang.Init();
            //    autoNganhang.SetCode(oldCode);
            //    autoNganhang.Focus();
            //}
        }

        void autoNguonkiqui__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(autoNguonkiqui.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = autoNguonkiqui.myCode;
                autoNguonkiqui.Init();
                autoNguonkiqui.SetCode(oldCode);
                autoNguonkiqui.Focus();
            }
        }

        void txtPttt__OnShowData()
        {
            //DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtPttt.LOAI_DANHMUC);
            //_DMUC_DCHUNG.ShowDialog();
            //if (!_DMUC_DCHUNG.m_blnCancel)
            //{
            //    string oldCode = txtPttt.myCode;
            //    txtPttt.Init();
            //    txtPttt.SetCode(oldCode);
            //    txtPttt.Focus();
            //}
        }

        void autoNguonkiqui__OnEnterMe()
        {
            try
            {
                List<string> lstqhe = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_TAMUNG_CAP_NGUON_PTTT", true).Split('-').ToList<string>();
                if (lstqhe.Count == 2)
                {
                    List<string> lstNguon = lstqhe[0].Split(',').ToList<string>();
                    List<string> lstPttt = lstqhe[1].Split(',').ToList<string>();
                    int idx = lstNguon.IndexOf(autoNguonkiqui.MyCode);
                    if (idx < 0) return;
                    string ma_pttt = lstPttt[idx];
                    cboPttt.SelectedValue = ma_pttt;
                    cboPttt_SelectedIndexChanged(cboPttt, new EventArgs());
                    //txtPttt.SetCode(ma_pttt);
                    //txtPttt.RaiseEnterEvents();
                    //if (!autoNganhang.Enabled) autoNganhang.SetCode("-1");
                    if (!cboNganhang.Enabled) cboNganhang.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException("THANHTOAN_TAMUNG_CAP_NGUON_PTTT", ex);
            }

        }
        void txtPttt__OnEnterMe()
        {
            //List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            //autoNganhang.Enabled = lstPTTT.Contains(txtPttt.MyCode);
        }
        public void EnterNextControl(Control currentCtr)
        {
            SelectNextControl(currentCtr, true, true, true, true);
        }
        void ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            Control _ctrl = sender as Control;
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SelectNextControl(_ctrl, true, true, true, true);

            }
        }



        void chkPrintPreview_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewPhieuTamung = chkPrintPreview.Checked;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._NoitruProperties.InsaukhiLuu = chkSaveAndPrint.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
        }

        void txtLydo__OnSaveAs()
        {
            if (Utility.DoTrim(txtLydo.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtLydo.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }

        void txtLydo__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }

        void grdTamung_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.isValidGrid(grdTamung) && e.KeyCode == Keys.Delete) cmdxoa.PerformClick();
        }

        void txtSotien__OnTextChanged(string text)
        {
            if (AllowedChanged_maskedEdit)
                Utility.SetMsg(lblMsg, text, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            try
            {
                if (m_enAct == action.Insert)
                {
                    objTamung = new NoitruTamung();
                    objTamung.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objTamung.MaLuotkham = objLuotkham.MaLuotkham;
                    objTamung.MaCoso = objLuotkham.MaCoso;
                    objTamung.IdKhoanoitru = v_bytNoitru == 0 ? globalVariables.idKhoatheoMay : objLuotkham.IdKhoanoitru;
                    objTamung.IdBuonggiuong = objLuotkham.IdRavien;
                    objTamung.IdBuong = objLuotkham.IdBuong;
                    objTamung.IdGiuong = objLuotkham.IdGiuong;
                    objTamung.Noitru = (byte)(objLuotkham.TrangthaiNoitru <= 0 ? 0 : 1);
                    objTamung.KieuTamung = 0;
                    objTamung.TuTrului = objTamung.Noitru == 0 ? true : chkTUtrului.Checked;
                    objTamung.GhiChu = Utility.DoTrim(txtGhichu.Text);
                    objTamung.MotaThem = txtLydo.Text;
                    objTamung.IdTnv = Utility.Int32Dbnull(txtNguoithu.MyID, -1);
                    objTamung.SoTien = Utility.DecimaltoDbnull(txtSotien.Text);
                    objTamung.NgayTamung = dtpNgaythu.Value;
                    objTamung.MaPttt = Utility.sDbnull(cboPttt.SelectedValue, "-1");// txtPttt.myCode;
                    objTamung.MaNganhang = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "-1") : "-1"; // autoNganhang.Enabled ? autoNganhang.myCode : "-1";
                    objTamung.MaNguonTamung = autoNguonkiqui.myCode;
                    objTamung.NguoiTao = globalVariables.UserName;
                    objTamung.NgayTao = DateTime.Now;
                    objTamung.TrangThai = 0;
                    objTamung.IdGoi = -1;
                    objTamung.Code = THU_VIEN_CHUNG.SinhmaVienphi("KQ");
                    objTamung.IsNew = true;
                    if (noitru_TamungHoanung.NoptienTamung(objTamung))
                    {
                        DataRow newDr = m_dtTamung.NewRow();
                        Utility.FromObjectToDatarow(objTamung, ref newDr);
                        newDr["sngay_tamung"] = dtpNgaythu.Value.ToString("dd/MM/yyyy");
                        newDr["ten_khoanoitru"] = "";
                        newDr["ten_nhanvien"] = txtNguoithu.Text;

                        newDr["nguoi_tao"] = globalVariables.UserName;
                        newDr["ngay_tao"] = DateTime.Now;

                        newDr[NoitruTamung.Columns.MaNganhang] = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "-1") : "-1";//autoNganhang.Enabled ? autoNganhang.myCode : "-1";
                        newDr[NoitruTamung.Columns.MaNguonTamung] = autoNguonkiqui.myCode;
                        newDr[NoitruTamung.Columns.MaPttt] = Utility.sDbnull(cboPttt.SelectedValue, "-1");// txtPttt.myCode;
                        newDr["ten_nganhang"] = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.Text, "-1") : "-1"; //autoNganhang.Enabled ? autoNganhang.Text : "";
                        newDr["ten_nguon_tamung"] = autoNguonkiqui.Text;
                        newDr["ten_pttt"] = cboPttt.Text;

                        m_dtTamung.Rows.Add(newDr);
                        m_dtTamung.AcceptChanges();
                        Utility.GotoNewRowJanus(grdTamung, NoitruTamung.Columns.Id, objTamung.Id.ToString());
                        if (chkSaveAndPrint.Checked)
                            cmdIn_Click(cmdIn, e);
                        m_enAct = action.FirstOrFinished;
                    }
                }
                else
                {
                    objTamung.SoTien = Utility.DecimaltoDbnull(txtSotien.Text);
                    objTamung.NgayTamung = dtpNgaythu.Value;
                    objTamung.TuTrului = objTamung.Noitru == 0 ? true : chkTUtrului.Checked;
                    objTamung.MotaThem = txtLydo.Text;
                    objTamung.GhiChu = Utility.DoTrim(txtGhichu.Text);
                    //objTamung.MotaThem = Utility.DoTrim(txtMotathem.Text);
                    objTamung.IdTnv = Utility.Int32Dbnull(txtNguoithu.MyID, -1);
                    objTamung.MaPttt = Utility.sDbnull(cboPttt.SelectedValue, "-1");// txtPttt.myCode;
                    objTamung.MaNganhang = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "-1") : "-1"; //autoNganhang.Enabled ? autoNganhang.myCode : "-1";
                    objTamung.MaNguonTamung = autoNguonkiqui.myCode;
                    objTamung.IsNew = false;
                    objTamung.MarkOld();
                    if (noitru_TamungHoanung.NoptienTamung(objTamung))
                    {

                        DataRow _myDr = ((DataRowView)grdTamung.CurrentRow.DataRow).Row;
                        _myDr[NoitruTamung.Columns.SoTien] = Utility.DecimaltoDbnull(txtSotien.Text);
                        _myDr[NoitruTamung.Columns.NgayTamung] = dtpNgaythu.Value;
                        _myDr[NoitruTamung.Columns.MotaThem] = txtLydo.Text;
                        _myDr[NoitruTamung.Columns.GhiChu] = txtGhichu.Text;
                        _myDr[NoitruTamung.Columns.IdTnv] = Utility.Int32Dbnull(txtNguoithu.MyID, -1);

                        _myDr["sngay_tamung"] = dtpNgaythu.Value.ToString("dd/MM/yyyy");
                        _myDr["ten_khoanoitru"] = "";
                        _myDr["ten_nhanvien"] = txtNguoithu.Text;

                        _myDr["nguoi_sua"] = globalVariables.UserName;
                        _myDr["ngay_sua"] = DateTime.Now;

                        _myDr[NoitruTamung.Columns.MaNganhang] = cboNganhang.Enabled?Utility.sDbnull(cboPttt.SelectedValue, "-1"):"-1";// autoNganhang.myCode;
                        _myDr[NoitruTamung.Columns.MaNguonTamung] = autoNguonkiqui.myCode;
                        _myDr[NoitruTamung.Columns.MaPttt] = Utility.sDbnull(cboPttt.SelectedValue, "-1");// txtPttt.myCode;
                        _myDr["ten_nganhang"] = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.Text, "-1") : "-1"; //autoNganhang.Text;
                        _myDr["ten_nguon_tamung"] = autoNguonkiqui.Text;
                        _myDr["ten_pttt"] = cboPttt.Text;

                        m_dtTamung.AcceptChanges();
                        m_enAct = action.FirstOrFinished;
                    }
                }
                setTongtienStatus();
                SetControlStatus();
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }


        }
        bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần tạm ứng", true);
                return false;
            }
            //Kiểm tra tạm ứng ngoại trú
            //if (objLuotkham.TrangthaiNoitru<=0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU",true)=="1")
            //{
            //    return true;
            //}
            if (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã hoàn ứng và ra viện nên bạn không thể nộp thêm tiền tạm ứng");
                return false;
            }
            //if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !new noitru_TamungHoanung().DathanhtoanhetNgoaitru(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham))
            //{
            //    Utility.SetMsg(lblMsg, "Bệnh nhân Dịch vụ chưa thanh toán hết tiền ngoại trú", true);
            //    return false;
            //}
            if (Utility.DecimaltoDbnull(txtSotien.Text) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập số tiền tạm ứng >0 ", true);
                txtSotien.SelectAll();
                txtSotien.Focus();
                return false;
            }
            if (Utility.DoTrim(txtLydo.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập lý do thu tiền tạm ứng ", true);
                txtLydo.SelectAll();
                txtLydo.Focus();
                return false;
            }
            if (txtNguoithu.MyID.ToString() == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập tên người thu tiền tạm ứng(Có thể xóa trắng và nhập phím cách để ra tất cả các nhân viên trong hệ thống)", true);
                txtNguoithu.SelectAll();
                txtNguoithu.Focus();
                return false;
            }
            if (Utility.sDbnull(cboPttt.SelectedValue, "-1") == "-1")
            {
                Utility.SetMsg(lblMsg, string.Format("Bạn phải chọn hình thức thanh toán"), true);
                cboPttt.Focus();
                return false;
            }
            if (cboNganhang.Enabled && Utility.sDbnull( cboNganhang.SelectedValue,"-1") == "-1")
            {
                Utility.SetMsg(lblMsg, string.Format("Phương thức thanh toán {0} bắt buộc phải nhập thông tin ngân hàng. Mời bạn chọn ngân hàng", cboPttt.Text), true);
                cboNganhang.Focus();
                return false;
            }
            //Kiêm tra nếu là tạm ứng trừ lùi với tình huống update
            if (m_enAct == action.Update)
            {
                if (objTamung != null)
                {
                    if (Utility.Bool2Bool(objTamung.TuTrului) && !chkTUtrului.Checked)
                    {
                        DataTable dtThanhtoanTUTL = new Select().From(KcbThanhtoanTamungTrului.Schema).Where(KcbThanhtoanTamungTrului.Columns.IdTamung).IsEqualTo(objTamung.IdTamung).ExecuteDataSet().Tables[0];
                        if (dtThanhtoanTUTL.Rows.Count > 0)
                        {
                            string sID = string.Join(",", (from p in dtThanhtoanTUTL.AsEnumerable() select Utility.sDbnull(p["id_thanhtoan"], "-1")).ToArray<string>());
                            Utility.ShowMsg(string.Format("Bản ghi tạm ứng bạn đang sửa là tạm ứng trừ lùi và đã được thanh toán cho các id thanh toán {0} nên không thể sửa tính chất trừ lùi được. Nhấn OK để hủy thao tác sửa này", sID));
                            chkTUtrului.Focus();
                            return false;
                        }
                    }
                }

            }
            return true;
        }
        void cmdHuy_Click(object sender, EventArgs e)
        {
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }

        void cmdIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân in phiếu tạm ứng");
                    return;
                }
                if (grdTamung.GetDataRows().Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu tạm ứng muốn in");
                    return;
                }
                if (!Utility.isValidGrid(grdTamung))
                {

                    grdTamung.MoveFirst();

                }
                long id_tamung = Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdTamung, NoitruTamung.Columns.Id), -1);
                NoitruTamung objTU = NoitruTamung.FetchByID(id_tamung);
                DataTable m_dtReport = noitru_TamungHoanung.NoitruInphieutamung(id_tamung, objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham);
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "noitru_phieutamung.xml");
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string reportcode = "noitru_phieutamung";
                bool hasCongkham = false;
                var q1 = from p in m_dtReport.AsEnumerable() where Utility.DecimaltoDbnull(p["id_phieuchitiet"]) >0 select p;
                var q = from p in m_dtReport.AsEnumerable() where Utility.Int32Dbnull(p["loai_dvu"]) == 1 select p;
                if(!q1.Any() )//tạm ứng ko theo dịch vụ
                {
                    hasCongkham = false;
                }
                else//Tạm ứng chi tiết dịch vụ
                {
                    reportcode = "noitru_phieutamung_dvu";
                    if (q.Any())
                    {
                        hasCongkham = true;
                    }    
                    
                }    
                string tieude = "", reportname = "";
                var crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
                if (crpt == null) return;

                MoneyByLetter _moneyByLetter = new MoneyByLetter();
                var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                Utility.UpdateLogotoDatatable(ref m_dtReport);

                crpt.SetDataSource(m_dtReport);

                objForm.NGAY = objTU != null ? objTU.NgayTamung.Value : DateTime.Now;
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportcode;
                objForm.nguoi_thuchien = m_dtReport.Rows[0]["tnv"].ToString();
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Tamung_Congkham", Utility.Bool2byte(hasCongkham).ToString());
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "TelePhone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(reportcode == "noitru_phieutamung_dvu"?Utility.Int32Dbnull(m_dtReport.Compute("SUM(thanh_tien)", "1=1"), 0).ToString(): Utility.Int32Dbnull(m_dtReport.Compute("SUM(so_tien)", "1=1"), 0).ToString()));
                Utility.SetParameterValue(crpt, "DIADIEM", globalVariables.gv_strDiadiem);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(objTU.NgayTamung.Value));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;

                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewPhieuTamung))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(objForm.getPrintNumber, false, 0, 0);
                }


            }
            catch (Exception)
            {
            }
        }

        void cmdxoa_Click(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TAMUNG_CHOPHEP_HUYTU", "0", true) == "1")
            {
                Utility.ShowMsg("Hệ thống không cho phép hủy tạm ứng. Yêu cầu thực hiện hoàn ứng bằng tay thay cho chức năng hủy tạm ứng");
                return;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TAMUNG_CHOPHEP_XOA", "0", true) == "1")
            {
                Xoatamung_V1();
            }
            else
                Xoatamung();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }
        void Xoatamung()
        {
            if (!isValidXoatamung()) return;
            try
            {
                string sQuestion = string.Format("Hủy tạm ứng được xử dụng trong tình huống nhập nhầm tạm ứng và muốn xóa để không hiển thị lên các báo cáo doanh thu.\nBạn có chắc chắn muốn hủy dòng tạm ứng đang chọn?\nNhấn Yes để thực hiện hủy tạm ứng. Nhấn No để hủy thao tác");
                if (!Utility.AcceptQuestion(sQuestion, "Xác nhận", true))
                {
                    return;
                }
                //if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy dòng tạm ứng đang chọn này hay không?", "Xác nhận", true))
                //{
                    if (objTamung != null)
                    {
                        frm_ChonLydohuyTUHU _ChonLydohuyTUHU = new frm_ChonLydohuyTUHU("LYDOHUYTAMUNG", "Hủy tiền tạm ứng", "Chọn lý do hủy tiền tạm ứng...", "Lý do hủy","Ngày tạm ứng");
                        _ChonLydohuyTUHU.dtNgayTUHU.Value = objTamung.NgayTamung.Value;
                        _ChonLydohuyTUHU.ShowDialog();
                        if (!_ChonLydohuyTUHU.m_blnCancel)
                        {
                            if (noitru_TamungHoanung.XoaTienTamung(objTamung, grdTamung.GetDataRows().Length - 1 > 0, _ChonLydohuyTUHU.txtLydoHuy.myCode, _ChonLydohuyTUHU.dtNgayHuy.Value, _ChonLydohuyTUHU.ma_pttt, Utility.sDbnull(_ChonLydohuyTUHU.cboNganhang.SelectedValue, "")))
                            {
                                Utility.SetMsg(lblMsg, string.Format("Hủy tạm ứng {0} thành công", txtSotien.Text), false);
                                DataRow drDelete = Utility.getCurrentDataRow(grdTamung);
                                if (drDelete != null)
                                {
                                    m_dtTamung.Rows.Remove(drDelete);
                                    m_dtTamung.AcceptChanges();
                                    //Thêm vào dòng hủy
                                    drDelete["tthai_huy"] = 1;
                                    drDelete["ngay_huy"] = _ChonLydohuyTUHU.dtNgayHuy.Value;
                                    drDelete["nguoi_huy"] = globalVariables.UserName;
                                    drDelete["lydo_huy"] = _ChonLydohuyTUHU.txtLydoHuy.myCode;
                                    drDelete["ma_httt_huy"] = _ChonLydohuyTUHU.ma_pttt;
                                    drDelete["ma_nganhang_huy"] = Utility.sDbnull(_ChonLydohuyTUHU.cboNganhang.SelectedValue, "");
                                    m_dtHuyTU.ImportRow(drDelete);
                                }
                            }
                        }
                        SetControlStatus();
                    }
                    else
                    {
                        Utility.SetMsg(lblMsg, string.Format("Bạn cần chọn dòng tạm ứng cần xóa"), true);
                    }
                //}
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                setTongtienStatus();
            }
        }
        void Xoatamung_V1()
        {
            if (!isValidXoatamung()) return;
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy dòng tạm ứng đang chọn này hay không?", "Xác nhận", true))
                {
                    if (objTamung != null)
                    {
                        frm_ChonLydohuyTUHU _ChonLydohuyTUHU = new frm_ChonLydohuyTUHU("LYDOHUYTAMUNG", "Hủy tiền tạm ứng", "Chọn lý do hủy tiền tạm ứng...", "Lý do hủy", "Ngày tạm ứng");
                        _ChonLydohuyTUHU.dtNgayTUHU.Value = objTamung.NgayTamung.Value;
                        _ChonLydohuyTUHU.ShowDialog();
                        if (!_ChonLydohuyTUHU.m_blnCancel)
                        {
                            if (noitru_TamungHoanung.XoaTienTamung_V1(objTamung, grdTamung.GetDataRows().Length - 1 > 0, _ChonLydohuyTUHU.txtLydoHuy.Text))
                            {
                                Utility.SetMsg(lblMsg, string.Format("Xóa tạm ứng {0} thành công", txtSotien.Text), false);
                                DataRow drDelete = Utility.getCurrentDataRow(grdTamung);
                                if (drDelete != null)
                                {
                                    m_dtTamung.Rows.Remove(drDelete);
                                    m_dtTamung.AcceptChanges();
                                }
                            }
                        }
                        SetControlStatus();
                    }
                    else
                    {
                        Utility.SetMsg(lblMsg, string.Format("Bạn cần chọn dòng tạm ứng cần xóa"), true);
                    }
                }
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                setTongtienStatus();
            }
        }
        bool isValidXoatamung()
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần tạm ứng", true);
                return false;
            }
            if (objTamung.TrangThai == 1)
            {
                Utility.SetMsg(lblMsg, "Đã hoàn ứng cho phần tạm ứng đang chọn nên bạn không thể hủy tạm ứng", true);
                return false;
            }
            if (objLuotkham.TrangthaiNoitru <= 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU", true) == "1")
            {
                return true;
            }
            if (objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã xuất viện nên bạn không thể hủy tiền tạm ứng");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được tài chính duyệt nên bạn không thể hủy tiền tạm ứng");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được tổng hợp xuất viện nên bạn không thể hủy tiền tạm ứng");
                return false;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThanhtoannoitru))
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán bạn không thể hủy tiền tạm ứng được");
                return false;
            }
            return true;
        }
        void cmdSua_Click(object sender, EventArgs e)
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã xuất viện nên bạn không thể sửa tiền tạm ứng");
                return;
            }
            if (objTamung != null && objTamung.TrangThai == 1)
            {
                Utility.ShowMsg("Đã hoàn ứng cho phần tạm ứng đang chọn nên bạn không thể sửa tạm ứng");
                return;
            }
            m_enAct = action.Update;
            AllowedChanged_maskedEdit = true;
            SetControlStatus();
        }
        public void Themmoi()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân cần nộp tiền tạm ứng");
                return;
            }

            if (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã hoàn ứng và thanh toán viện nên bạn không thể nộp thêm tiền tạm ứng");
                return;
            }
            m_enAct = action.Insert;
            AllowedChanged_maskedEdit = true;
            SetControlStatus();
        }
        void cmdthemmoi_Click(object sender, EventArgs e)
        {
            Themmoi();
        }
        private void SetControlStatus()
        {
            try
            {
                grdTamung.Enabled = false;
                AllowedChanged = false;
                switch (m_enAct)
                {
                    case action.Insert:
                        txtNguoithu.SetId(globalVariables.gv_intIDNhanvien);
                        //Cho phép nhập liệu mã loại đối tượng,vị trí, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);
                        dtpNgaythu.Enabled = true;
                        txtSotien.Enabled = true;
                        txtLydo.Enabled = true;
                        txtGhichu.Enabled = true;
                        txtNguoithu.Enabled = true;
                        
                        objTamung = null;
                        dtpNgaythu.Value = globalVariables.SysDate;
                        txtSotien.Text = "0";
                        txtLydo.SetCode("-1");
                        txtGhichu.Clear();
                        autoNguonkiqui.Enabled = true;
                        cboPttt.Enabled = true;
                        cboPttt_SelectedIndexChanged(cboPttt, new EventArgs());

                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Không cho phép nhấn Insert, Update,Delete
                        cmdthemmoi.Enabled = false;
                        cmdSua.Enabled = false;
                        cmdxoa.Enabled = false;
                        cmdGhi.Enabled = true;
                        cmdHuy.Enabled = true;
                        cmdGhi.BringToFront();
                        cmdHuy.BringToFront();

                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = false;
                        //Tự động Focus đến mục ID để người dùng nhập liệu
                        txtID.Text = "Tự sinh";
                        cmdHuy.Text = "Hủy";
                        txtSotien.Focus();
                        break;
                    case action.Update:
                        //Không cho phép cập nhật lại mã loại đối tượng
                        Utility.DisabledTextBox(txtID);
                        //Cho phép cập nhật lại vị trí, tên loại đối tượng và mô tả thêm
                        dtpNgaythu.Enabled = true;
                        txtLydo.Enabled = true;
                        txtGhichu.Enabled = true;
                        txtNguoithu.Enabled = true;
                        txtSotien.Enabled = true;
                        autoNguonkiqui.Enabled = true;
                        cboPttt.Enabled = true;
                        cboPttt_SelectedIndexChanged(cboPttt, new EventArgs());
                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Không cho phép nhấn Insert, Update,Delete
                        cmdthemmoi.Enabled = false;
                        cmdSua.Enabled = false;
                        cmdxoa.Enabled = false;
                        cmdGhi.Enabled = true;
                        cmdHuy.Enabled = true;
                        cmdGhi.BringToFront();
                        cmdHuy.BringToFront();
                        cmdHuy.Text = "Hủy";
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = false;
                        //Tự động Focus đến mục Position để người dùng nhập liệu
                        txtSotien.Focus();
                        break;
                    case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                        AllowedChanged_maskedEdit = false;
                        grdTamung.Enabled = true;
                        AllowedChanged = true;
                        //Không cho phép nhập liệu mã loại đối tượng, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);
                        dtpNgaythu.Enabled = false;
                        txtLydo.Enabled = false;
                        txtGhichu.Enabled = false;
                        txtNguoithu.Enabled = false;
                        txtSotien.Enabled = false;
                        autoNguonkiqui.Enabled = false;
                        cboPttt.Enabled = false;
                        cboNganhang.Enabled = false;

                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Sau khi nhấn Ghi thành công hoặc Hủy thao tác thì quay về trạng thái ban đầu
                        //Cho phép thêm mới
                        cmdGhi.Enabled = false;
                        cmdHuy.Enabled = false;
                        cmdGhi.SendToBack();
                        cmdHuy.SendToBack();
                        //Nút Hủy biến thành nút thoát
                        cmdHuy.Text = "Thoát";
                        //--------------------------------------------------------------
                        //Cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = true;
                        //Tự động chọn dòng hiện tại trên lưới để hiển thị lại trên Control
                        grdTamung_SelectionChanged(grdTamung, new EventArgs());
                        //Tự động Focus đến nút thêm mới? 
                        cmdthemmoi.Focus();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                grdTamung.Enabled = true;
                setTongtienStatus();
            }

        }
        void grdTamung_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowedChanged) return;
            FillData();
            NapdichvuTamung(Utility.isValidGrid(grdTamung) ? Utility.Int64Dbnull(grdTamung.GetValue("id")) : -1);
        }
        void FillData()
        {
            try
            {
                if (!Utility.isValidGrid(grdTamung))
                {

                    objTamung = null;
                    dtpNgaythu.Value = globalVariables.SysDate;
                    txtSotien.Text = "0";
                    chkTUtrului.Checked = false;
                    txtLydo.SetCode("-1");
                    txtGhichu.Clear();
                    txtNguoithu.SetCode("-1");
                    cboNganhang.SelectedIndex = -1;
                    cboPttt.SelectedIndex = -1;
                    //autoNganhang.SetCode("-1");
                    //txtPttt.SetCode("-1");
                    autoNguonkiqui.SetCode("-1");
                }
                else
                {

                    objTamung = NoitruTamung.FetchByID(Utility.Int32Dbnull(grdTamung.GetValue(NoitruTamung.Columns.Id)));

                    if (objTamung == null)
                    {
                        dtpNgaythu.Value = globalVariables.SysDate;
                        txtSotien.Text = "0";
                        chkTUtrului.Checked = false;
                        txtLydo.SetCode("-1");
                        txtGhichu.Clear();
                        txtNguoithu.SetCode("-1");
                        cboNganhang.SelectedIndex = -1;
                        cboPttt.SelectedIndex = -1;
                        autoNguonkiqui.SetCode("-1");
                    }
                    else
                    {
                        objTamung.IsNew = false;
                        objTamung.MarkOld();
                        dtpNgaythu.Value = objTamung.NgayTamung.Value;
                        txtSotien.Text = objTamung.SoTien.ToString();
                        txtLydo._Text = objTamung.MotaThem;
                        txtGhichu.Text = objTamung.GhiChu;
                        txtNguoithu.SetId(objTamung.IdTnv);
                        chkTUtrului.Checked = Utility.Bool2Bool(objTamung.TuTrului);
                        cboPttt.SelectedValue = objTamung.MaPttt;
                            cboNganhang.SelectedValue = objTamung.MaNganhang;
                        //autoNganhang.SetCode(objTamung.MaNganhang);
                        //txtPttt.SetCode(objTamung.MaHttt);
                        autoNguonkiqui.SetCode(objTamung.MaNguonTamung);

                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ModifyCommand();
                //SetControlStatus();
            }
        }


        public void Init()
        {
            LoadConfig();
            LoadPtttNganhang();
            InitPTTTColumns();
            uiTabPageHuyTU.TabVisible = Utility.Coquyen("tamung_xemlichsu_huytamung");
            LayLichsuTamung();
            AutoCompleteTextBox();
            cmdHuyhoanung.Enabled = Utility.isValidGrid(grdHoanung);
            cmdHoanung.Enabled = grdHoanung.RowCount <= 0;
            //cmdHuyhoanung.Visible = cmdHoanung.Visible = v_bytNoitru == 1 ? THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU", "0", false) == "0" : THU_VIEN_CHUNG.Laygiatrithamsohethong("NGOAITRU_TUDONGHOANUNG_KHITHANHTOANNGOAITRU", "0", false) == "0";
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
            ModifyCommand();
            hasLoadedDmuc = true;
        }

        private DataTable m_dtKhoaNoiTru = new DataTable();

        void AutoCompleteTextBox()
        {
            if (hasLoadedDmuc) return;
            txtLydo.Init();
            AutoCompleteTextBox_nguoithu();
            txtNguoithu.SetId(globalVariables.gv_intIDNhanvien);
           // autoNganhang.Init();
            autoNguonkiqui.Init();
            //txtPttt.Init();

        }
        void AutoCompleteTextBox_nguoithu()
        {
            DataTable m_dtNhanvien = SPs.DmucLaydanhsachTnv().GetDataSet().Tables[0];
            txtNguoithu.Init(m_dtNhanvien, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoithu.SetId(globalVariables.gv_intIDNhanvien);
        }

        private void ModifyCommand()
        {
            bool isValid = objLuotkham != null;
            bool isValid2 = Utility.isValidGrid(grdTamung);
            cmdSua.Enabled = isValid && isValid2;
            cmdxoa.Enabled = isValid && isValid2;
            cmdIn.Enabled = isValid && isValid2;
            cmdthemmoi.Enabled = isValid;
            cmdGhi.Enabled = m_enAct != action.FirstOrFinished;
            cmdHuy.Enabled = cmdGhi.Enabled;
            cmdHoanung.Enabled = m_dtTamung != null && m_dtTamung.Select("Kieu_tamung=0 and trang_thai=0 and (id_thanhtoan=-1 or id_thanhtoan is null)").Length > 0;
            cmdHuyhoanung.Enabled = Utility.isValidGrid(grdHoanung);
            cmdInphieuhoanung.Enabled = m_dtHoanung != null && m_dtHoanung.Rows.Count > 0;
        }

        public DataTable m_dtTamung = null;
        public DataTable m_dtHoanung = null;
        DataTable m_dtHuyTU = null;
        void setTongtienStatus()
        {
            try
            {
                if (m_dtTamung == null) return;
                lblTongtien.Text = "Tổng tiền: " + new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(m_dtTamung.Compute("SUM(so_tien)", "1=1"), "0"));
            }
            catch (Exception ex)
            {

            }

        }
        void LayLichsuTamung()
        {
            try
            {
                if (objLuotkham == null)
                {
                    grdTamung.DataSource = null;
                    return;
                }
                DataTable dt_AllData = new KCB_THAMKHAM().NoitruTimkiemlichsuNoptientamung(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan, 0), -1, -1,Utility.ByteDbnull( isLichsu?2:v_bytNoitru,2));//: (byte)(objLuotkham.TrangthaiNoitru > 0 ? 1 : 0));
                DataRow[] arrDr = dt_AllData.Select("Kieu_tamung=0 and tthai_huy_1=0");
                m_dtTamung = dt_AllData.Clone();
                m_dtHoanung = dt_AllData.Clone();
                if (arrDr.Length > 0) m_dtTamung = arrDr.CopyToDataTable();
                arrDr = dt_AllData.Select("Kieu_tamung=1 and tthai_huy_1=0");
                if (arrDr.Length > 0) m_dtHoanung = arrDr.CopyToDataTable();
                m_dtHuyTU = dt_AllData.Clone();
                arrDr = dt_AllData.Select("Kieu_tamung=0 and tthai_huy_1=1");
                if (arrDr.Length > 0) m_dtHuyTU = arrDr.CopyToDataTable();

                Utility.SetDataSourceForDataGridEx_Basic(grdTamung, m_dtTamung, false, true, "1=1", NoitruTamung.Columns.NgayTamung + " desc");
                Utility.SetDataSourceForDataGridEx_Basic(grdHoanung, m_dtHoanung, false, true, "1=1", NoitruTamung.Columns.NgayTamung + " desc");
                Utility.SetDataSourceForDataGridEx_Basic(grdHuyTU, m_dtHuyTU, false, true, "1=1", NoitruTamung.Columns.NgayTamung + " desc");
                grdTamung.MoveFirst();
                if (grdTamung.GetDataRows().Length <= 0)
                {
                    objTamung = null;
                    dtpNgaythu.Value = globalVariables.SysDate;
                    txtSotien.Text = "0";
                    txtLydo.SetCode("-1");
                    txtNguoithu.SetId(globalVariables.gv_intIDNhanvien);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                AllowedChanged = true;
                setTongtienStatus();
                ShowLSuTamung();
            }
        }
        void ShowLSuTamung()
        {
            return;//
            if (objLuotkham != null)
            {
                grdTamung.Width = 0;
            }
            else
            {
                grdTamung.Width = 425;
            }
        }


        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_Quanlytamung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }

            if (e.KeyCode == Keys.N && e.Control)
            {
                cmdthemmoi.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                cmdGhi.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.U && e.Control)
            {
                cmdSua.PerformClick();
                return;
            }
        }

        void LoadConfig()
        {
            if (hasLoadedDmuc) return;
            chkPrintPreview.Checked = PropertyLib._MayInProperties.PreviewPhieuTamung;
            chkSaveAndPrint.Checked = PropertyLib._NoitruProperties.InsaukhiLuu;
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._NoitruProperties);
            _Properties.ShowDialog();
            LoadConfig();
        }

        private void cmdIn_Click_1(object sender, EventArgs e)
        {

        }

        private void ctxMore_Opening(object sender, CancelEventArgs e)
        {
            if (!Utility.Coquyen("THANHTOAN_QUYEN_PHANBOPTTT"))
            {
                Utility.ShowMsg("Bạn không có quyền phân bổ hình thức thanh toán(THANHTOAN_QUYEN_PHANBOPTTT). Vui lòng liên hệ quản trị để được cấp quyền");
                return;
            }
            Phanbo();
        }
        void Phanbo()
        {
            if (!Utility.isValidGrid(grdTamung)) return;
            long id_tamung = Utility.Int64Dbnull(grdTamung.CurrentRow.Cells[NoitruTamung.Columns.Id].Value, -1);
            string ma_pttt = Utility.sDbnull(grdTamung.CurrentRow.Cells[NoitruTamung.Columns.MaPttt].Value, "TM");
            string ma_nganhang = Utility.sDbnull(grdTamung.CurrentRow.Cells[KcbThanhtoan.Columns.MaNganhang].Value, "TM");
            frm_PhanbotientheoPTTT _PhanbotientheoPTTT = new frm_PhanbotientheoPTTT(-1, -1, id_tamung, ma_pttt, ma_nganhang);
            _PhanbotientheoPTTT._OnChangePTTT += _PhanbotientheoPTTT__OnChangePTTT;
            _PhanbotientheoPTTT.objLuotkham = this.objLuotkham;
            _PhanbotientheoPTTT.ShowDialog();
        }

        void _PhanbotientheoPTTT__OnChangePTTT(long id_thanhtoan, string ma_pttt, string ten_pttt, string ma_nganhang, string ten_nganhang)
        {
            try
            {
                DataRow dr = ((DataRowView)grdTamung.CurrentRow.DataRow).Row;
                dr["ma_pttt"] = ma_pttt;
                dr["ten_pttt"] = ten_pttt;
                dr["ma_nganhang"] = ma_nganhang;
                dr["ten_nganhang"] = ten_nganhang;
                m_dtTamung.AcceptChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void uiTab1_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            try
            {
                if (uiTab1.SelectedIndex == 0)//Tạm ứng
                {
                    pnlActTamung.BringToFront();
                    ModifyCommand();//Tùy chỉnh theo dữ liệu tạm ứng
                }
                else//Hoàn ứng.Bật nút in để in phiếu hoàn ứng
                {
                    cmdHuy.PerformClick();//Hủy tác vụ bên tạm ứng
                    pnlActTamung.SendToBack();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdHoanung_Click(object sender, EventArgs e)
        {
            Hoanung();
        }
        void Hoanung()
        {
            try
            {
                if (!Utility.Coquyen("thanhtoan_hoanung"))
                {
                    Utility.ShowMsg("Bạn không có quyền hoàn ứng bằng tay(thanhtoan_hoanung). Vui lòng liên hệ bộ phận IT để được cấp quyền");
                    return;
                }
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (v_bytNoitru == 0)//Đang được gọi từ form thanh toán ngoại trú
                {
                    if (objLuotkham.TrangthaiNoitru > 0)
                    {
                        Utility.ShowMsg("Bệnh nhân đã vào viện nội trú nên bạn không được phép hoàn ứng các tạm ứng ngoại trú");
                        return;
                    }
                }
                else//Kiểm tra phần nội trú
                {
                    if (!globalVariables.isSuperAdmin)
                    {
                        if (objLuotkham.TrangthaiNoitru < 4)
                        {
                            Utility.ShowMsg("Chi phí KCB của người bệnh chưa được chuyển tài chính kế toán nên bạn không thể thực hiện hoàn ứng bằng tay");
                            return;
                        }
                    }
                }
                string maphieu = THU_VIEN_CHUNG.SinhmaVienphi("HKQ");
                SPs.NoitruHoanung(-1l, maphieu, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, DateTime.Now,
                    globalVariables.gv_intIDNhanvien, globalVariables.UserName,
                    Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, -1), Utility.Int64Dbnull(objLuotkham.IdRavien, -1),
                    Utility.Int32Dbnull(objLuotkham.IdBuong, -1), Utility.Int32Dbnull(objLuotkham.IdGiuong, -1),
                    (byte)(objLuotkham.TrangthaiNoitru>0?1:0),"TM","").Execute();

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }


        }
        void HuyHoanung()
        {
            try
            {
                if (!Utility.Coquyen("thanhtoan_huyhoanung"))
                {
                    Utility.ShowMsg("Bạn không có quyền hủy hoàn ứng (thanhtoan_huyhoanung). Vui lòng liên hệ bộ phận IT để được cấp quyền");
                    return;
                }
                NoitruTamung objHU = NoitruTamung.FetchByID(Utility.Int64Dbnull(grdHoanung.GetValue("id"), -1));
                //Kiểm tra ngày hủy
                int kcbThanhtoanSongayHuyHu =
                    Utility.Int32Dbnull(
                        THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYHU", "0", true), 0);
                var chenhlech =
                    (int)Math.Ceiling((globalVariables.SysDate.Date - objHU.NgayTamung.Value.Date).TotalDays);
                if (chenhlech > kcbThanhtoanSongayHuyHu)
                {
                    Utility.ShowMsg(
                       string.Format("Ngày Hoàn ứng: {0}. Hệ thống không cho phép bạn hủy hoàn ứng khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objHU.NgayTamung.Value.Date.ToString("dd/MM/yyyy"), kcbThanhtoanSongayHuyHu.ToString()));
                    return;
                }

                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                ////Bỏ các dòng code dưới để cho phép hủy. Nếu cần có thể chọn thanh toán và hoàn ứng lại được
                //if (v_bytNoitru == 0)//Đang được gọi từ form thanh toán ngoại trú
                //{
                //    GridEXRow currentRow = Utility.findthelastChild(grdHoanung.CurrentRow);
                //    //Không cho phép hủy hoàn ứng với các bản ghi gắn với thanh toán
                //    if (Utility.Int64Dbnull(currentRow.Cells["id_thanhtoan"].Value, -1) > 0)
                //    {
                //        Utility.ShowMsg("Bản tin bạn chọn hoàn ứng gắn với thanh toán nên không cho phép hủy tay. Cần hủy thanh toán để tự động hủy hoàn ứng");
                //        return;
                //    }
                //}
                //else// Nếu gọi từ nội trú thì kiểm tra đã thanh toán nội trú không cho phép hủy hoàn ứng
                //{
                //    if (objLuotkham.TrangthaiNoitru == 6)
                //    {
                //        Utility.ShowMsg("Bệnh nhân đã thanh toán nội trú nên bạn không được phép hủy hoàn ứng");
                //        return;
                //    }
                SPs.NoitruHuyhoanung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objHU.IdThanhtoan, v_bytNoitru).Execute();
                Utility.ShowMsg("Đã hủy hoàn ứng thành công");
                LayLichsuTamung();
                //}

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }

        private void cmdHuyhoanung_Click(object sender, EventArgs e)
        {
            HuyHoanung();
        }

        private void cmdInphieuhoanung_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân in phiếu hoàn ứng");
                    return;
                }
                if (grdHoanung.GetDataRows().Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu hoàn ứng muốn in");
                    return;
                }
                GridEXRow currentRow = Utility.findthelastChild(grdHoanung.CurrentRow);
                DataTable m_dtReport = SPs.KcbInphieuhoanung(Utility.sDbnull(currentRow.Cells[NoitruTamung.Columns.Code].Value, -1)).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thanhtoan_phieuhoanung.xml");
                if (m_dtReport == null || m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                List<string> lstCode = (from p in m_dtReport.AsEnumerable()
                                        select Utility.sDbnull(p["ma_tamung"], "")).Distinct().ToList<string>();
                string sophieutamung = "";
                if (lstCode.Count > 0)
                    sophieutamung = string.Join(",", lstCode.ToArray<string>());
                Int64 tongtienhoanung = Utility.Int64Dbnull(m_dtReport.Compute("SUM(so_tien)", "1=1"), 0);
                foreach (DataRow dr in m_dtReport.Rows)
                {
                    dr["so_tien"] = tongtienhoanung;
                }
                string tieude = "", reportname = "";
                var crpt = Utility.GetReport("thanhtoan_phieuhoanung", ref tieude, ref reportname);
                if (crpt == null) return;

                MoneyByLetter _moneyByLetter = new MoneyByLetter();
                var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                Utility.UpdateLogotoDatatable(ref m_dtReport);

                crpt.SetDataSource(m_dtReport);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thanhtoan_phieuhoanung";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "sophieutamung", sophieutamung);
                Utility.SetParameterValue(crpt, "TelePhone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(tongtienhoanung.ToString()).ToString());
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;

                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewPhieuTamung))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(objForm.getPrintNumber, false, 0, 0);
                }


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cboPttt_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            cboNganhang.Enabled = lstPTTT.Contains(Utility.sDbnull( cboPttt.SelectedValue,"-1")) && m_enAct!=action.FirstOrFinished;
            if (!cboNganhang.Enabled) cboNganhang.SelectedIndex = -1;
        }
    }
}
