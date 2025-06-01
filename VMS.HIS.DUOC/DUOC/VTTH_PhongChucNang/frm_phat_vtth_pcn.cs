using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using VNS.HIS.BusRule.Classes;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
using System.IO;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.NGOAITRU;
using System.Drawing;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.Classes;
using Janus.Windows.GridEX.EditControls;
using VNS.UCs;
using System.Transactions;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_phat_vtth_pcn : Form
    {
        private int Distance = 488;
        private NLog.Logger log;
        private string _fileName = string.Format("{0}/{1}", Application.StartupPath, string.Format("SplitterDistancefrm_PhieuXuatBN.txt"));
        private DataTable m_dtDonthuoc = new DataTable();
        private DataTable m_dtChitietdonthuoc = new DataTable();
        string kieuthuoc_vt = "THUOC";
        string kieukho = "ALL";//ALL,TATCA,NOITRU,NGOAITRU
        byte noitru_ngoaitru = 100;
        byte khoaphong_theonguoidung = 0;
        public bool AllowSelectionChanged = false;
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        bool b_Hasloaded = false;
        string SplitterPath = "";
        private DataTable m_dtChiPhiDaThanhToan = new DataTable();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">kieuthuoc_vt-noitrungoaitru</param>
        public frm_phat_vtth_pcn(string args)
        {
            InitializeComponent();
            try
            {
                SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
                Utility.SetVisualStyle(this);
                this.kieuthuoc_vt = args.Split('-')[0];
                kieukho = args.Split('-')[1];
                noitru_ngoaitru =Utility.ByteDbnull( args.Split('-')[2]);
                khoaphong_theonguoidung=Utility.ByteDbnull(args.Split('-')[3]);
                InitEvents();

                dtFromdate.Value = globalVariables.SysDate;
                dtToDate.Value = globalVariables.SysDate;

                log = NLog.LogManager.GetLogger(this.Name);
                dtNgayPhatThuoc.Value = globalVariables.SysDate;
                CauHinh();
                cmdConfig.Visible = globalVariables.IsAdmin;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        void InitEvents()
        {
            
            this.Load += new System.EventHandler(this.frm_phat_vtth_pcn_Load);
            Shown += frm_phat_vtth_pcn_Shown;
            FormClosing += frm_phat_vtth_pcn_FormClosing;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_phat_vtth_pcn_KeyDown);
            this.txtPres_ID.Click += new System.EventHandler(this.txtPres_ID_Click);
            this.txtPres_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPres_ID_KeyDown);
            this.txtPID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPID_KeyDown_1);
            
            //this.radTatCa.CheckedChanged += new System.EventHandler(this.radTatCa_CheckedChanged);
            //this.radChuaXacNhan.CheckedChanged += new System.EventHandler(this.radChuaXacNhan_CheckedChanged);
            //this.radDaXacNhan.CheckedChanged += new System.EventHandler(this.radDaXacNhan_CheckedChanged);
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            cmdKiemTraSoLuong.Click+=new EventHandler(cmdKiemTraSoLuong_Click);
            grdPres.ApplyingFilter+=new CancelEventHandler(grdPres_ApplyingFilter);
            grdPresDetail.UpdatingCell += grdPresDetail_UpdatingCell;
            grdPresDetail.CellUpdated+=grdPresDetail_CellUpdated;
            grdPresDetail.RowCheckStateChanged+=grdPresDetail_RowCheckStateChanged;
            grdPresDetail.ColumnHeaderClick+=grdPresDetail_ColumnHeaderClick;
            grdPresDetail.EditingCell += grdPresDetail_EditingCell;
            grdPresDetail.CellValueChanged += grdPresDetail_CellValueChanged;

        
           chkThanhtoan.CheckedChanged += _CheckedChanged;
           chkHuythanhtoan.CheckedChanged += _CheckedChanged;
           chkCapphat.CheckedChanged += _CheckedChanged;
           chkHuyCapphat.CheckedChanged += _CheckedChanged;
           chkHienthidvuhuyTT.CheckedChanged += _CheckedChanged;
          
           cboKhoanoitru.SelectedIndexChanged += cboKhoanoitru_SelectedIndexChanged;
           cboTrangthai.SelectedIndexChanged += cboTrangthai_SelectedIndexChanged;
          
        }

        void cboTrangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!AllowSelectionChanged) return;
            FilterMe();
        }
        void FilterMe()
        {
            try
            {
                string Rowfilter = "";
                if (Utility.Int32Dbnull( cboTrangthai.SelectedValue,0)==-1)
                {
                    Rowfilter = "1=1";
                }
                else if (Utility.Int32Dbnull(cboTrangthai.SelectedValue, 0) == 6)
                {
                    Rowfilter = "trang_thai=0";
                }
                else 
                {
                    Rowfilter = "trang_thai=1";
                }
                if (Rowfilter.Length <= 0)
                    Rowfilter = "1=1";

                m_dtDonthuoc.DefaultView.RowFilter = Rowfilter;

                m_dtDonthuoc.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void cboKhoanoitru_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            try
            {
                if (!b_Hasloaded) return;
                short IDKhoa = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1);
              
                DataTable _newTable = CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(IDKhoa, -1, "TATCA,NOITRU", kieuthuoc_vt, 100, 100);
                DataBinding.BindDataCombobox(cboKho, _newTable,
                                           TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "--Chọn tủ thuốc--", true);
                if (_newTable.Rows.Count == 2)
                {
                    cboKho.SelectedIndex = 1;
                }
            }
            catch
            {
            }

        }

      
        void Try2Splitter()
        {
            try
            {


                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count >=1)
                {
                    splitContainer4.SplitterDistance = lstSplitterSize[0];
                }
            }
            catch (Exception)
            {

            }
        }
        void SaveUserConfigs()
        {
            Utility.SaveUserConfig(chkThanhtoan.Tag.ToString(), Utility.Bool2byte(chkThanhtoan.Checked));
            Utility.SaveUserConfig(chkHuythanhtoan.Tag.ToString(), Utility.Bool2byte(chkHuythanhtoan.Checked));
            Utility.SaveUserConfig(chkHienthidvuhuyTT.Tag.ToString(), Utility.Bool2byte(chkHienthidvuhuyTT.Checked));
            Utility.SaveUserConfig(chkRestoreDefaultPTTT.Tag.ToString(), Utility.Bool2byte(chkRestoreDefaultPTTT.Checked));
            Utility.SaveUserConfig(chkCapphat.Tag.ToString(), Utility.Bool2byte(chkCapphat.Checked));
            Utility.SaveUserConfig(chkHuyCapphat.Tag.ToString(), Utility.Bool2byte(chkHuyCapphat.Checked));

        }
        void frm_phat_vtth_pcn_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer4.SplitterDistance.ToString(), splitContainer4.SplitterDistance.ToString() });
        }

        void frm_phat_vtth_pcn_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }

       

        void grdPresDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            
        }

        void grdPresDetail_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (grdPresDetail.CurrentColumn != null) grdPresDetail.CurrentColumn.InputMask = "";
        }
       
        /// <summary>
        ///     HÀM THỰC HIỆN VIỆC THAY ĐỔI THÔNG TIN PHIẾU CẬN LÂM SÀNG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdThongTinDaThanhToan_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == KcbThanhtoanChitiet.Columns.TrangthaiHuy)
            {
            }
        }

        private void grdThongTinDaThanhToan_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifyCommand();
        }

        private void grdThongTinDaThanhToan_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifyCommand();
        }
       
       
        void _CheckedChanged(object sender, EventArgs e)
        {
            //Utility.SaveUserConfig(((Janus.Windows.EditControls.UICheckBox)sender).Tag.ToString(), Utility.Bool2byte(((Janus.Windows.EditControls.UICheckBox)sender).Checked));
        }

        private void grdPresDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
         
        }
      
        void grdPresDetail_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            if (e.Row != null)
            {
                e.Row.BeginEdit();
                if (Utility.sDbnull(e.Row.Cells["trangthai_thanhtoan"].Value, "0") == "0")
                    e.Row.Cells["CHON"].Value = Utility.ByteDbnull(e.CheckState == RowCheckState.Checked ? 1 : 0);
                else
                    e.Row.IsChecked = false;
                e.Row.EndEdit();

            }
           
            ModifyCommand();
        }
        private void grdPresDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
           
        }
        void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
           
        }

        void ChangeQuantity(KcbDonthuocChitiet objChitiet, decimal sl_cu, decimal sl_moi, DataRow currentDR)
        {

        }
        
        void updateQtyinDataTable(long IdChitietdonthuoc, decimal newQty, decimal TT)
        {
            try
            {
                DataRow[] arrDr = m_dtChitietdonthuoc.Select(string.Format("{0}={1}", KcbDonthuocChitiet.Columns.IdChitietdonthuoc, IdChitietdonthuoc));
                if(arrDr.Length>0)
                {
                    arrDr[0][KcbDonthuocChitiet.Columns.SluongSua] = newQty;
                    arrDr[0]["TT"] = TT;
                }
            }
            catch (Exception ex)
            {

              
            }
        }
        private void CauHinh()
        {
            cmdHuycapphat.Visible = PropertyLib._HisDuocProperties.HuyXacNhan;
            dtNgayPhatThuoc.Enabled = PropertyLib._HisDuocProperties.ChoPhepChinhNgayDuyet;
            string HIENTHIPHANTICHGIA_TRENLUOI = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_HIENTHIPHANTICHGIA_TRENLUOI", "0", false);
          
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtEventTongTien_KeyPress(Object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
          
        }
      
        void LoadUserConfigs()
        {
            chkThanhtoan.Checked = Utility.getUserConfigValue(chkThanhtoan.Tag.ToString(), Utility.Bool2byte(chkThanhtoan.Checked)) == 1;
            chkHuythanhtoan.Checked = Utility.getUserConfigValue(chkHuythanhtoan.Tag.ToString(), Utility.Bool2byte(chkHuythanhtoan.Checked)) == 1;
            chkHienthidvuhuyTT.Checked = Utility.getUserConfigValue(chkHienthidvuhuyTT.Tag.ToString(), Utility.Bool2byte(chkHienthidvuhuyTT.Checked)) == 1;
            chkRestoreDefaultPTTT.Checked = Utility.getUserConfigValue(chkRestoreDefaultPTTT.Tag.ToString(), Utility.Bool2byte(chkRestoreDefaultPTTT.Checked)) == 1;
            chkCapphat.Checked = Utility.getUserConfigValue(chkCapphat.Tag.ToString(), Utility.Bool2byte(chkCapphat.Checked)) == 1;
            chkHuyCapphat.Checked = Utility.getUserConfigValue(chkHuyCapphat.Tag.ToString(), Utility.Bool2byte(chkHuyCapphat.Checked)) == 1;

           
        }
        private DataTable m_dtKhothuoc=new DataTable();
        private void frm_phat_vtth_pcn_Load(object sender, EventArgs e)
        {
            LoadUserConfigs();
            if (kieuthuoc_vt == "ALL")
                m_dtKhothuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1, kieukho, "ALL", "CHANLE,LE", 100, 100,1);// CommonLoadDuoc.LAYTHONGTIN_KHOVTTH_LE_NGOAITRU();
            else if (kieuthuoc_vt == "THUOC")
                m_dtKhothuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1, kieukho, "THUOC,THUOCVT", "CHANLE,LE", 100, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NGOAITRU();
            else
                m_dtKhothuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1, kieukho, "VT,THUOCVT", "CHANLE,LE", 100, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOLE_NGOAITRU();
            DataBinding.BindData(cboKho, m_dtKhothuoc,
                                     TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
            DataBinding.BindDataCombobox(cboKhoanoitru,
                khoaphong_theonguoidung == 0 ? THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", "ALL", 1) : THU_VIEN_CHUNG.LaydanhsachKhoaKhidangnhap(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin)),
                                                DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                "---Chọn khoa nội trú---", false, false);
            b_Hasloaded = true;
            cboKhoanoitru.SelectedIndex = Utility.GetSelectedIndex(cboKhoanoitru, globalVariablesPrivate.objKhoaphong.IdKhoaphong.ToString());
            if (cboKhoanoitru.SelectedIndex < 0 && cboKhoanoitru.Items.Count > 0) cboKhoanoitru.SelectedIndex = 0;
           
            dtNgayPhatThuoc.Value = globalVariables.SysDate;
            DataTable dtTthai = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("TRANGTHAI_DONTHUOC_TUTRUC").And(DmucChung.Columns.TrangThai).IsEqualTo(1).OrderAsc(DmucChung.Columns.SttHthi).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboTrangthai, dtTthai, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            cboKhoanoitru_SelectedIndexChanged(cboKhoanoitru, e);
            TimKiemThongTinDonThuoc();
            txtPID.Focus();
            txtPID.SelectAll();

        }
       
        /// <summary>
        /// hàm thực hiện việc trạng thái thông tin của đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TimKiemThongTinDonThuoc();
        }
        private void TimKiemThongTinDonThuoc()
        {
            try
            {
                AllowSelectionChanged = false;
                int Status  =Utility.Int32Dbnull(cboTrangthai.SelectedValue);
                int NoiTru = 0;
                m_dtDonthuoc =
                    SPs.ThuocTimkiemdonthuocTutruc(Utility.Int32Dbnull(txtPres_ID.Text, -1), txtPID.Text,
                                                           Utility.sDbnull(txtTenBN.Text),"ALL",
                                                           chkByDate.Checked ? dtFromdate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                           chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900", Status,
                                                            Utility.Int32Dbnull(cboKho.SelectedValue),100,4,kieuthuoc_vt).GetDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx_Basic(grdPres, m_dtDonthuoc, true, true, "1=1", "ten_benhnhan");
                grdPres.AutoSizeColumns();
                RowFilterView();
                if (grdPres.GetDataRows().Length <= 0)
                {
                    m_dtChitietdonthuoc.Rows.Clear();
                    m_dtChiPhiDaThanhToan = null;
                    ResetItems();
                }
                else
                    grdPres.MoveFirst();
              //  ModifyCommand();
            }
            catch (Exception exception)
            {
                if(globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
               
            }
            finally
            {
                AllowSelectionChanged = true;
                grdPres_SelectionChanged(grdPres, new EventArgs());
                ModifyCommand();
            }
           
        }
        
        /// <summary>
        /// hàm thực hiện việc lọc filter của dược
        /// </summary>
        private void RowFilterView()
        {
            if (PropertyLib._HisDuocProperties.LocDonThuocKhiDuyet)
            {
                //string rowFilter = "1=1";

                //if (radChuaXacNhan.Checked) rowFilter = string.Format("{0}={1}", KcbDonthuoc.Columns.TrangThai, 0);
                //if (radDaXacNhan.Checked) rowFilter = string.Format("{0}={1}", KcbDonthuoc.Columns.TrangThai, 1);
                //    m_dtDonthuoc.DefaultView.RowFilter = rowFilter;
                //    m_dtDonthuoc.AcceptChanges();
            }
            
        }
        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPres_ID_Click(object sender, EventArgs e)
        {

        }

        private void txtPres_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if(Utility.Int32Dbnull(txtPres_ID.Text)>0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmdSearch.PerformClick();
                }
            }
           
        }
       
        /// <summary>
        /// hàm thực hiện việc dichuyeen thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged) return;
                if (Utility.isValidGrid(grdPres))
                {
                  
                    Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                    objBenhnhan = KcbDanhsachBenhnhan.FetchByID(Utility.Int64Dbnull(grdPres.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1));
                    objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdPres.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1), Utility.sDbnull(grdPres.GetValue(KcbLuotkham.Columns.MaLuotkham), "-1"));
                    GetDataPresDetail();
                    Laydanhsachphieuxuatthuoc();
                }
                else
                {
                    grdPresDetail.DataSource = null;
                }
              
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ex.Message);
                
            }
            ModifyCommand();
        }
        void Laydanhsachphieuxuatthuoc()
        {
            try
            {
                DataTable dtData = SPs.ThuocLaydanhsachphieuxuatthuocbenhnhan(Pres_ID).GetDataSet().Tables[0];
                flowpnlPhieuxuatthuoc.Controls.Clear();
                foreach (DataRow dr in dtData.Rows)
                {
                    ucPhieuxuatthuocBN item = new ucPhieuxuatthuocBN(Utility.Int64Dbnull( dr[TPhieuXuatthuocBenhnhan.Columns.IdPhieu].ToString(),-1), dr[TPhieuXuatthuocBenhnhan.Columns.MaPhieu].ToString(), dr["ten_phieu"].ToString());
                    item._OnClick += item__OnClick;
                    flowpnlPhieuxuatthuoc.Controls.Add(item);
                    toolTip1.SetToolTip(item._ScheduleObj, string.Format("Phiếu xuất thuốc cho bệnh nhân: {0}", dr["ten_phieu"].ToString()));
                    //item.Margin = m_objPadding;
                    item.Size = new Size(PropertyLib._PhieuxuatBNProperty.UCWidth, PropertyLib._PhieuxuatBNProperty.UCHeight);
                    item.ResetColor();
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
              
            }
        }
        void ResetItems()
        {
            ucPhieuxuatthuocBN _item = null;
            foreach (Control ctr in flowpnlPhieuxuatthuoc.Controls)
            {
                _item = ctr as ucPhieuxuatthuocBN;
                _item.Size = new Size(PropertyLib._PhieuxuatBNProperty.UCWidth, PropertyLib._PhieuxuatBNProperty.UCHeight);
                _item.ResetColor();
            }
            Application.DoEvents();
        }
        bool Forced2Select = false;
        bool MyClick = false;
        void item__OnClick(ucPhieuxuatthuocBN obj)
        {
            MyClick = true;
            ClickMe(obj);
            ModifyCommand();
            MyClick = false; 
        }
       
        void ClickMe(ucPhieuxuatthuocBN obj)
        {
            //if (!System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
            //    ResetPreviousSelectedObject(flowpnlPhieuxuatthuoc, obj);
            if (Forced2Select)
                obj.SelectMe();
            else
                if (!obj.isPressed) obj.SelectMe();
                else
                    obj.UnSelectMe();
        }
        public void ResetPreviousSelectedObject(FlowLayoutPanel pnlItems, ucPhieuxuatthuocBN _Selected)
        {
            ucPhieuxuatthuocBN _item = null;
            try
            {
                foreach (Control ctr in pnlItems.Controls)
                {
                    _item = ctr as ucPhieuxuatthuocBN;
                    if (_item.isPressed && _item._ID != _Selected._ID)
                        _item.Reset();
                }
            }
            catch
            {
            }
            finally
            {
            }
            Application.DoEvents();
        }
        ucPhieuxuatthuocBN getSelectedObject()
        {
            if (flowpnlPhieuxuatthuoc.Controls.Count == 1) return flowpnlPhieuxuatthuoc.Controls[0] as ucPhieuxuatthuocBN;
            foreach (Control item in flowpnlPhieuxuatthuoc.Controls)
            {
                ucPhieuxuatthuocBN pxt = item as ucPhieuxuatthuocBN;
                if (pxt.isPressed)
                    return pxt;
            }
            return null;
        }
        private void ModifyCommand()
        {
            try
            {
                if (!AllowSelectionChanged) return;
                bool isValidParent = Utility.isValidGrid(grdPres);

                bool _dacapphathet = m_dtChitietdonthuoc != null && !m_dtChitietdonthuoc.AsEnumerable().Any(c => c.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) == 0);// Utility.sDbnull(Utility.getValueOfGridCell(grdPres, KcbDonthuoc.Columns.TrangThai), "0") == "1";
                bool tthai_capphat = m_dtChitietdonthuoc != null && m_dtChitietdonthuoc.AsEnumerable().Any(c => c.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) == 1);// Utility.ByteDbnull(Utility.getValueOfGridCell(grdPres, KcbDonthuoc.Columns.TrangThai), "0");
                bool isValidDetail = Utility.isValidGrid(grdPresDetail);
                cmdIndonthuoc.Enabled = isValidParent;
               
                cmdPhatThuoc.Enabled = cmdDuyetcapphat.Enabled = isValidParent && isValidDetail && !_dacapphathet;// _dathanhtoan && tthai_capphat < 2;
                cmdHuycapphat.Enabled = cmdHuyduyetCapphat.Enabled = isValidParent && isValidDetail && tthai_capphat && (flowpnlPhieuxuatthuoc.Controls.Count == 1 || (flowpnlPhieuxuatthuoc.Controls.Count > 1 && getSelectedObject() != null));
                cmdKiemTraSoLuong.Enabled = isValidParent && isValidDetail;
              

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private long Pres_ID=-1;
        private void GetDataPresDetail()
        {

            int stock_id = -1;
            string ma_luotkham = Utility.sDbnull(Utility.getValueOfGridCell(grdPres,KcbLuotkham.Columns.MaLuotkham));
            long  v_IdDonthuoc= Utility.Int64Dbnull(Utility.getValueOfGridCell(grdPres, KcbDonthuoc.Columns.IdDonthuoc));
            long v_IdBenhnhan = Utility.Int64Dbnull(Utility.getValueOfGridCell(grdPres, KcbLuotkham.Columns.IdBenhnhan));

            m_dtChitietdonthuoc = SPs.ThuocTutrucChitietthuocCapphat(ma_luotkham, v_IdBenhnhan, v_IdDonthuoc).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtChitietdonthuoc, false, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
            Utility.AddColumToDataTable(ref m_dtChitietdonthuoc, "colCHON", typeof(byte));
            //UpdateTuCheckKhiChuaThanhToan();
            //SetSumTotalProperties();
            m_dtChitietdonthuoc.AcceptChanges();
           
        }
      
      
        /// <summary>
        /// hàm thực hiện việc cho phím tắt thực hiện tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_phat_vtth_pcn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            else if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            else if (e.KeyCode == Keys.F2)
            {
                txtPID.Clear();
                txtPID.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                grdPres_SelectionChanged(grdPres, new EventArgs());
            }
            else if (e.Control && e.KeyCode == Keys.S) cmdPhatThuoc_Click(cmdPhatThuoc, new EventArgs());
            else if (e.Control && e.KeyCode == Keys.P) cmdIndonthuoc.PerformClick();
        }
        void DuyetCapphatthuoc()
        {
            try
            {
                Utility.SetMsg(uiStatusBar2.Panels[1], "", false);
                //Lấy từ CSDL cho chắc ăn thay vì lấy trên lưới danh sách
                DataTable dtChitietcapphat = SPs.ThuocTutrucLaydanhsachchitietchuacapphat(Pres_ID).GetDataSet().Tables[0];
                if (!KiemtradonthuocOK(dtChitietcapphat)) return;
                Utility.EnableButton(cmdPhatThuoc, false);
                cmdPhatThuoc.Cursor = Cursors.WaitCursor;
                long presId = Utility.Int64Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1);
                string ErrMsg = "";
                Int16 stockId = Utility.Int16Dbnull(m_dtChitietdonthuoc.Rows[0][KcbDonthuocChitiet.Columns.IdKho]);
                if (presId > 0 && stockId > 0)
                {
                    try
                    {
                        KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(presId);
                        if (objDonthuoc.NgayKedon.ToString("dd/MM/yyyy") != dtNgayPhatThuoc.Value.ToString("dd/MM/yyyy"))
                        {
                            frm_ChonngayXacnhan _ChonngayXacnhan = new frm_ChonngayXacnhan();
                            _ChonngayXacnhan.pdt_InputDate = objDonthuoc.NgayKedon;
                            _ChonngayXacnhan.ShowDialog();
                            if (_ChonngayXacnhan.b_Cancel)
                                return;
                            else
                                dtNgayPhatThuoc.Value = _ChonngayXacnhan.pdt_InputDate;
                        }

                        //cần cấp phát toàn bộ thì mở cột này
                    List<long> lstIdchitietcancaphat    =dtChitietcapphat.AsEnumerable().Select(c=>c.Field<long>(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)).Distinct().ToList<long>();
                    ActionResult actionResult = new XuatThuoc().LinhThuocBenhNhan(presId, stockId, lstIdchitietcancaphat, dtNgayPhatThuoc.Value, ref ErrMsg);

                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                UpdateHasConfirm(lstIdchitietcancaphat);
                                Laydanhsachphieuxuatthuoc();
                                Utility.SetMsg(uiStatusBar2.Panels[1], "Bạn thực hiện việc phát thuốc thành công", false);
                                break;
                            case ActionResult.NotEnoughDrugInStock:
                                Utility.ShowMsg(string.Format("Thuốc không đủ cấp phát. Mời bạn kiểm tra lại\n{0}", ErrMsg));
                                break;
                            case ActionResult.Error:
                                Utility.SetMsg(uiStatusBar2.Panels[1], "Lỗi trong quá trình phát thuốc cho bệnh nhân", true);
                                break;
                            case ActionResult.Cancel:
                                Utility.SetMsg(uiStatusBar2.Panels[1], "Toàn bộ các thuốc bạn vừa chọn để cấp phát đã được người khác cấp phát xong. Vui lòng liên hệ nội bộ để biết", true);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi trong quá trình phát thuốc" + ex.Message);
                    }
                }
                cmdPhatThuoc.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                cmdPhatThuoc.Enabled = true;
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin theo đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPhatThuoc_Click(object sender, EventArgs e)
        {
            DuyetCapphatthuoc();
        }

        private void UpdateHasConfirm(List<long> lstIdchitietcancaphat)
        {
                foreach (GridEXRow gridExRow in grdPresDetail.GetDataRows())
                {
                    if (lstIdchitietcancaphat.Contains(Utility.Int64Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value)))
                    {
                        gridExRow.BeginEdit();
                        gridExRow.Cells[KcbDonthuocChitiet.Columns.TrangThai].Value = 1;
                        gridExRow.EndEdit();
                    }
                }
                grdPresDetail.UpdateData();
                m_dtChitietdonthuoc.AcceptChanges();
                var query = from donthuoc in grdPresDetail.GetDataRows().AsEnumerable()
                            where
                                Utility.Int64Dbnull(donthuoc.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value) == Pres_ID
                            select donthuoc;
                if (query.Any())
                {
                    Pres_ID = Utility.Int64Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                    SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID)
                        .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                    int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                    if (PropertyLib._HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDonthuoc.AcceptChanges();
                    }
                    else
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDonthuoc.AcceptChanges();
                    }
                }
        }
        /// <summary>
        /// hàm thực hiện việc update thông tin hủy
        /// </summary>
        private void UpdateHuyHasConfirm()
        {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells[KcbDonthuocChitiet.Columns.TrangThai].Value = 0;
                    gridExRow.EndEdit();
                }
                grdPresDetail.UpdateData();
                m_dtChitietdonthuoc.AcceptChanges();
                var query = from donthuoc in grdPresDetail.GetDataRows().AsEnumerable()
                            where
                                Utility.Int64Dbnull(donthuoc.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value) == Pres_ID
                            select donthuoc;
                if (query.Any())
                {
                    Pres_ID = Utility.Int64Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                    SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID)
                        .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                    int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                    if (PropertyLib._HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDonthuoc.AcceptChanges();
                    }
                    else
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDonthuoc.AcceptChanges();
                    }
                }

        }
        private bool InValiDonthuoc()
        {

           Pres_ID = Utility.Int64Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1);
            SqlQuery sqlQuery = new Select().From(KcbDonthuoc.Schema)
                .Where(KcbDonthuoc.Columns.TrangThai).IsEqualTo(1)
                .And(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(Pres_ID);
            if(sqlQuery.GetRecordCount()>0)
            {
                Utility.ShowMsg("Đơn thuốc đã phát thuốc, Mời bạn xem lại thông tin ","Thông báo",MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool InValiHuyDonthuoc()
        {
            if (grdPresDetail.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Không tìm thấy chi tiết đơn thuốc. Bạn cần chọn ít nhất 1 đơn thuốc có chi tiết để thao tác", "Thông báo", MessageBoxIcon.Error);

                return false;
            }
            int tthai_chot = Utility.Int32Dbnull(grdPres.GetValue("tthai_chot"), -1);
            if (tthai_chot == 1)
            {
                Utility.ShowMsg("Đơn thuốc đã được chốt nên không thể hủy. Đề nghị bạn kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            if (m_dtChitietdonthuoc.Select(string.Format("{0}={1}", KcbDonthuocChitiet.Columns.TrangThai ,1)).Length<=0)
            {
                Utility.ShowMsg("Đơn thuốc chưa có chi tiết được cấp phát thuốc nên không thể hủy. Đề nghị bạn kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// /hàm thực hiện việc khởi tạo thông tin của phiếu xuất cho bệnh nhân
        /// </summary>
        /// <param name="objPrescription"></param>
        /// <returns></returns>
       
        private TPhieuXuatthuocBenhnhanChitiet []CreatePhieuXuaChiTiet()
        {
            int length = 0;
            int idx = 0;
            var arrPhieuXuatCT = new TPhieuXuatthuocBenhnhanChitiet[length];
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
            {
                if(gridExRow.RowType==RowType.Record)
                {
                    arrPhieuXuatCT[idx]=new TPhieuXuatthuocBenhnhanChitiet();
                    arrPhieuXuatCT[idx].ChiDan =Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.MotaThem].Value);
                    arrPhieuXuatCT[idx].SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoLuong].Value);
                    arrPhieuXuatCT[idx].IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,-1);
                    arrPhieuXuatCT[idx].DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value);
                 
                    
                    idx++;
                }
            }
            return arrPhieuXuatCT;
        }
        /// <summary>
        /// hàm thưc hiện việc kiểm tra thông tin của kho có đủ thuốc không 
        /// Nếu không đủ không cho phát thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdKiemTraSoLuong_Click(object sender, EventArgs e)
        {
            //if(!KiemtradonthuocOK())return;
            //else
            //{
            //    Utility.ShowMsg("Bạn có thể xác nhận phiếu lĩnh thuốc của bệnh nhân\n Mời bạn phát thuốc","Thông báo",MessageBoxIcon.Information);
            //}
        }

        private bool KiemtradonthuocOK(DataTable dtChitietcapphat)
        {
            try
            {
               // if (!radChuaXacNhan.Checked && !grdPres.GetDataRows().Any()) return false;
                string idLoaidoituongKcb = Utility.GetValueFromGridColumn(grdPres, "id_loaidoituong_kcb");
                string inphieuDct = Utility.sDbnull(grdPres.GetValue("id_phieu_dct"), "0");
                string dathanhtoan = Utility.GetValueFromGridColumn(grdPres, "dathanhtoan");
                long  presId = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                string ma_luotkham = Utility.sDbnull(grdPres.GetValue(KcbDonthuoc.Columns.MaLuotkham));
                string  tenBenhnhan = Utility.sDbnull(grdPres.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan));

                //DataSet dskiemtra = SPs.KcbNgoaitruKiemtraCapphatthuoc(presId, ma_luotkham).GetDataSet();
                //SqlQuery sqlkt = new Select().From(TPhieuXuatthuocBenhnhan.Schema).Where(TPhieuXuatthuocBenhnhan.Columns.IdDonthuoc).IsEqualTo(presId);
                //SqlQuery sqlktdonthuoc = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(presId);

                //Tạm bỏ đi sẽ dùng hàm khác kiểm tra sau 230530
                //if (dskiemtra.Tables[0].Rows.Count <= 0)
                //{
                //    Utility.ShowMsg(string.Format("Đơn thuốc của bệnh nhân {0} không tồn tại nữa! Bạn cần tìm kiếm lại thông tin đơn thuốc", tenBenhnhan));
                //    log.Trace(string.Format("Đơn thuốc của bệnh nhân {0} không tồn tại nữa! Bạn cần tìm kiếm lại thông tin đơn thuốc", tenBenhnhan));
                //    return false;
                //}
                //if (dskiemtra.Tables[1].Rows.Count > 0)
                //{
                //    Utility.ShowMsg(string.Format("Đơn thuốc của bệnh nhân {0} đã được cấp phát!", tenBenhnhan));
                //    log.Trace(string.Format("Đơn thuốc của bệnh nhân {0} đã được cấp phát!", tenBenhnhan));
                //    return false;
                //}
                //    if (idLoaidoituongKcb == "1")
                //    {
                //        if (dskiemtra.Tables[2].Rows.Count > 0)
                //        {
                //            Utility.ShowMsg(
                //          string.Format(
                //              "Đối tượng bệnh nhân Dịch vụ đang chọn chưa thanh toán đơn thuốc nên bạn không thể thực hiện cấp phát." +
                //              "\nĐề nghị bệnh nhân đi nộp tiền thanh toán trước khi quay lại lĩnh thuốc"));
                //            return false;
                //        }
                //    }
                //    else
                //    {
                //        if (dskiemtra.Tables[2].Rows.Count <= 0)
                //        {
                //            Utility.ShowMsg(string.Format("Đối tượng bệnh nhân BHYT đang chọn chưa in phôi BHYT nên bạn không thể thực hiện cấp phát thuốc." +
                //              "\nĐề nghị bệnh nhân đến quầy thanh toán in phôi BHYT trước khi quay lại lĩnh thuốc"));
                //            return false;
                //        }

                //    }
                    
                    foreach (DataRow dr in dtChitietcapphat.Rows)//m_dtChitietdonthuoc.Rows)
                {
                    
                    long idDonthuoc = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdDonthuoc]);
                    int idThuoc = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc]);
                    string drugName = Utility.sDbnull(dr["ten_chitietdichvu"]);
                    int idKho = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdKho]);
                    int idThuockho = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdThuockho]);
                    decimal soLuong = Utility.DecimaltoDbnull(dr[KcbDonthuocChitiet.Columns.SoLuong]);
                    decimal soLuongTon = CommonLoadDuoc.SoLuongTonTrongKho(idDonthuoc, idKho, idThuoc, idThuockho, 0, (byte)0);//Ko cần kiểm tra chờ xác nhận
                    if (soLuongTon < soLuong)
                    {
                        Utility.ShowMsg(string.Format("Bạn không thể xác nhận đơn thuốc, Vì thuốc :{0} số lượng tồn hiện tại trong kho không đủ\n Mời bạn xem lại số lượng", drugName));
                        Utility.GonewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc, idThuoc.ToString());
                        return false;
                    }
                }
                if (chkCapphat.Checked)
                {
                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn cấp phát đơn thuốc đang chọn của người bệnh {0}. Thuốc sẽ chính thức trừ khỏi kho sau khi cấp phát", tenBenhnhan), "Thông báo", true))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// hàm thực hiện việc di chuyển thôn gtin trên đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện viecj cấu hình 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._HisDuocProperties);
                frm.ShowDialog();
                CauHinh();
            }
            catch(Exception exception)
            {
                Utility.ShowMsg("Lỗi"+ exception.Message);
            }
          
        }

        private void LoadLayout()
        {

            string layoutDir = GetLayoutDirectory() + @"\GridEXLayout.gxl";

            if (File.Exists(layoutDir))
            {

                FileStream layoutStream;

                layoutStream = new FileStream(layoutDir, FileMode.Open);

                grdPresDetail.LoadLayoutFile(layoutStream);

                layoutStream.Close();

            }

        }
        private string GetLayoutDirectory()
        {
            DirectoryInfo dInfo;
            dInfo = new DirectoryInfo(Application.ExecutablePath).Parent;

            dInfo = new DirectoryInfo(dInfo.FullName + @"\LayoutData");
            if (!dInfo.Exists) dInfo.Create();
            return dInfo.FullName;
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPres_SelectionChanged(grdPres, e);
        }
        void HuyCapphatthuoc()
        {
            try
            {
                ucPhieuxuatthuocBN selectedObject = getSelectedObject();
                if (selectedObject == null)
                {
                    Utility.ShowMsg("Bạn cần chọn một phiếu xuất thuốc bên dưới để thực hiện hủy cấp phát", "thông báo", MessageBoxIcon.Information);
                    return;
                }
                if (chkHuyCapphat.Checked)
                {
                    if (!Utility.AcceptQuestion("Bạn có muốn thực hiện hủy phát thuốc cho bệnh nhân \n Dữ liệu hủy sẽ được trả lại kho phát thuốc", "Thông báo", true))
                    {
                        return;
                    }
                }
                if (!InValiHuyDonthuoc()) return;
                frm_NhaplydoHuy _NhaplydoHuy = new frm_NhaplydoHuy("LYDOHUYXACNHAN", "HỦY XÁC NHẬN ĐƠN THUỐC", "Chọn lý do hủy xác nhận trước khi thực hiện...", "Lý do hủy", "Ngày hủy");
                _NhaplydoHuy.ShowDialog();
                if (!_NhaplydoHuy.m_blnCancel)
                {
                    Int16 stockID = Utility.Int16Dbnull(m_dtChitietdonthuoc.Rows[0][KcbDonthuocChitiet.Columns.IdKho]);
                    dtNgayPhatThuoc.Value = globalVariables.SysDate;
                    try
                    {
                        ActionResult actionResult =
                           new XuatThuoc().HuyXacNhanDonThuocBN(Pres_ID,selectedObject._ID, stockID, _NhaplydoHuy.ngay_thuchien, _NhaplydoHuy.ten);
                        switch (actionResult)
                        {
                            case ActionResult.DataUsed:
                                Utility.ShowMsg("Một trong các thuốc bạn chọn đã được sử dụng nên bạn không thể thực hiện hủy xác nhận", "thông báo", MessageBoxIcon.Information);
                                break;
                            case ActionResult.Success:
                                UpdateHuyHasConfirm();
                                Laydanhsachphieuxuatthuoc();
                                Utility.Log(this.Name, globalVariables.UserName,
                                                   string.Format(
                                                       "Hủy phát thuốc của bệnh nhân có mã lần khám {0} và mã bệnh nhân là: {1}. Đơn thuốc {2} bởi {3}",
                                                       Utility.sDbnull(grdPres.CurrentRow.Cells["ma_luotkham"].Value),
                                                       Utility.sDbnull(grdPres.CurrentRow.Cells["id_benhnhan"].Value),
                                                       Utility.sDbnull(grdPres.CurrentRow.Cells["id_donthuoc"].Value),
                                                       globalVariables.UserName), newaction.Delete ,this.GetType().Assembly.ManifestModule.Name);
                                Utility.ShowMsg("Bạn thực hiện việc hủy phát thuốc thành công", "thông báo", MessageBoxIcon.Information);
                                break;
                            case ActionResult.Cancel:
                                Utility.ShowMsg("Tồn tại chi tiết thuốc đã được trả lại tiền nên bạn không thể hủy xác nhận. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình hủy phát thuốc cho bệnh nhân", "Thông báo", MessageBoxIcon.Error);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi trong quá trình hủy đơn thuốc" + ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        /// <summary>
        /// hàm thực hiện việc hủy thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHuyDonThuoc_Click(object sender, EventArgs e)
        {
            HuyCapphatthuoc();
        }
        private KcbDonthuocChitiet []CreatePresDetail()
        {
            int idx = 0;
            int length = 0;
            var query = from chitiet in grdPresDetail.GetDataRows()
                        let y = chitiet.RowType == RowType.Record
                        select y;
            length = query.Count();
            var arrDetail = new KcbDonthuocChitiet[length];
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
            {
                arrDetail[idx]=new KcbDonthuocChitiet();
              
                arrDetail[idx].IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value);
                arrDetail[idx].IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value);
                arrDetail[idx].IdChitietdonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value);
                arrDetail[idx].IdKho = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdKho].Value);
                arrDetail[idx].DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value);
                arrDetail[idx].PhuThu = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.PhuThu].Value);
                arrDetail[idx].SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoLuong].Value);
                arrDetail[idx].MotaThem = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.MotaThem].Value);
                arrDetail[idx].ChidanThem = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.ChidanThem].Value);
                arrDetail[idx].CachDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.CachDung].Value);
                arrDetail[idx].DonviTinh = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonviTinh].Value);
                arrDetail[idx].SoluongDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoluongDung].Value);
                arrDetail[idx].SolanDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SolanDung].Value);
                arrDetail[idx].IdThanhtoan = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThanhtoan].Value);
                arrDetail[idx].TuTuc = Utility.ByteDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.TuTuc].Value);
                idx++;
            }
            return arrDetail;
        }
        private bool InValiXoaThongTin()
        {
            if (grdPresDetail.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi để thực hiện việc xóa thông tin đơn thuốc", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bản ghi đã thanh toán, bạn không thể xóa thông tin ", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bạn phải chọn những bản ghi chưa xác nhận", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
       
       

        private void radTatCa_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }

        private void radChuaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }

        private void radDaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }
        private void txtPID_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (txtPID.Text.Trim() != "" && Utility.Int32Dbnull(txtPID.Text) > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        string patient_ID = Utility.GetYY(globalVariables.SysDate) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPID.Text, 0), "000000");
                        txtPID.Text = patient_ID;
                        int Status = -1;
                        int NoiTru = 0;
                        m_dtDonthuoc =
                            SPs.ThuocTimkiemdonthuocCapphatngoaitru(-1, txtPID.Text,"","ALL",
                                                                   "01/01/1900","01/01/1900", Status,
                                                                    Utility.Int32Dbnull(cboKho.SelectedValue),100, kieuthuoc_vt,100,1).
                                GetDataSet().Tables[0];

                        Utility.SetDataSourceForDataGridEx(grdPres, m_dtDonthuoc, true, true, "1=1", "ten_benhnhan asc");
                        RowFilterView();
                     //   ModifyCommand();
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
                        ModifyCommand();
                    }
                }
            }
        }
        private void GetChanDoan(string icdChinh, string idcPhu, ref string icdName, ref string icdCode)
        {
            try
            {
                List<string> lstIcd = icdChinh.Split(',').ToList();
                DmucBenhCollection list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstIcd));
                foreach (DmucBenh item in list)
                {
                    icdName += item.TenBenh + "; ";
                    icdCode += item.MaBenh + "; ";
                }
                lstIcd = idcPhu.Split(',').ToList();
                list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstIcd));
                foreach (DmucBenh _item in list)
                {
                    icdName += _item.TenBenh + "; ";
                    icdCode += _item.MaBenh + "; ";
                }
                if (icdName.Trim() != "") icdName = icdName.Substring(0, icdName.Length - 1);
                if (icdCode.Trim() != "") icdCode = icdCode.Substring(0, icdCode.Length - 1);
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin) Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private readonly KCB_KEDONTHUOC _kcbKedonthuoc = new KCB_KEDONTHUOC();
        private void PrintPres(int presID, string forcedTitle)
        {
            DataTable v_dtDataOrg = _kcbKedonthuoc.LaythongtinDonthuocTaiQuay_In(presID);

            DataRow[] arrDR = v_dtDataOrg.Select("tuvan_them=0");
            if (arrDR.Length <= 0)
            {
                PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
                return;
            }
            DataTable v_dtData = arrDR.CopyToDataTable();
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            if (v_dtData != null && v_dtData.Rows.Count > 0)
                GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                            Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "", reportCode = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonthuocA4";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            frmPrintPreview objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
            objForm.nguoi_thuchien = Utility.sDbnull(v_dtData.Rows[0]["ten_bacsikedon"], "");
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", "ĐƠN THUỐC");
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                           PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                    //cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
            finally
            {
                PrintTuvanthem(presID, forcedTitle, v_dtDataOrg);
            }
        }
        private void PrintTuvanthem(int presID, string forcedTitle, DataTable p_dtData)
        {

            DataRow[] arrDR = p_dtData.Select("tuvan_them=1");
            if (arrDR.Length <= 0) return;
            DataTable v_dtData = arrDR.CopyToDataTable();
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA5.xml");
            byte[] Barcode = null;
            Utility.CreateBarcodeData(ref v_dtData, m_strMaLuotkham, ref Barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            if (v_dtData != null && v_dtData.Rows.Count > 0)
                GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                            Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            //  THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "", reportCode = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonTuvanA4";
                    reportDocument = Utility.GetReport("thamkham_InDonTuvanA4", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            frmPrintPreview objForm = new frmPrintPreview("IN ĐƠN TƯ VẤN", crpt, true, true);
            objForm.nguoi_thuchien = Utility.sDbnull(v_dtData.Rows[0]["ten_bacsikedon"], "");
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                           PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                   // cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        string m_strMaLuotkham = "";
        private void cmdPrintPres_Click(object sender, EventArgs e)
        {
            m_strMaLuotkham=Utility.sDbnull(grdPres.GetValue(KcbDonthuoc.Columns.MaLuotkham));
            int presId = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
            PrintPres(presId, "");
            

        }

        private void cmdcauhinh_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._PhieuxuatBNProperty);
            frm.ShowDialog();
            ResetItems();
            //try
            //{
            //    frm_Properties frm = new frm_Properties(PropertyLib._MayInProperties);
            //    frm.ShowDialog();
            //    CauHinh();
            //}
            //catch (Exception exception)
            //{
            //    Utility.ShowMsg("Lỗi" + exception.Message);
            //}
        }
        
       
        KcbDanhsachBenhnhan objBenhnhan;
        KcbLuotkham objLuotkham;
       
        KCB_DANGKY _kcbDangky = new KCB_DANGKY();
       
        private bool Kiemtratrangthai_donthuoc()
        {
            var _item =
                new Select().From(KcbDonthuocChitiet.Schema)
                .Where(KcbDonthuocChitiet.IdDonthuocColumn).IsEqualTo(Pres_ID)
                .AndExpression(KcbDonthuoc.Columns.TrangThai).IsEqualTo(1).Or(KcbDonthuoc.Columns.TrangthaiThanhtoan).IsEqualTo(1).CloseExpression().ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }
      

        private void cmdDuyetcapphat_Click(object sender, EventArgs e)
        {
            DuyetCapphatthuoc();
        }

        private void cmdCheck_Click(object sender, EventArgs e)
        {
            //if (!KiemtradonthuocOK()) return;
            //else
            //{
            //    Utility.ShowMsg("Bạn có thể xác nhận phiếu lĩnh thuốc của bệnh nhân\n Mời bạn phát thuốc", "Thông báo", MessageBoxIcon.Information);
            //}
        }

      
        private void cmdIndonthuoc_Click(object sender, EventArgs e)
        {
            m_strMaLuotkham = Utility.sDbnull(grdPres.GetValue(KcbDonthuoc.Columns.MaLuotkham));
            int presId = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
            PrintPres(presId, "");
        }

     

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
       

        private void cmdHuyduyetCapphat_Click(object sender, EventArgs e)
        {
            HuyCapphatthuoc();
        }

        private void chkThanhtoan_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mnuRollback_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPresDetail)) return;
            }
            catch (Exception ex)
            {
                
            }
            

        }

        private void cmdCauhinhPhieuxuatthuoc_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._PhieuxuatBNProperty);
            frm.ShowDialog();
            ResetItems();
        }

        private void cmdTraLaiTien_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuUpdateIDthuockho_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPresDetail))
                {
                    Utility.ShowMsg("Bạn cần chọn thuốc/vật tư trong đơn trước khi thực hiện chức năng này");
                    return;
                }
                frm_capnhat_idthuockho _capnhat_idthuockho = new frm_capnhat_idthuockho();
                _capnhat_idthuockho.id_phieu = Utility.Int64Dbnull(grdPresDetail.GetValue("id_donthuoc"), -1);
                _capnhat_idthuockho.id_phieu_ctiet = Utility.Int64Dbnull(grdPresDetail.GetValue("id_chitietdonthuoc"), -1);
                _capnhat_idthuockho.so_luong = Utility.Int32Dbnull(grdPresDetail.GetValue("so_luong"), -1);
                _capnhat_idthuockho.id_thuoc = Utility.Int32Dbnull(grdPresDetail.GetValue("id_thuoc"), -1);
                _capnhat_idthuockho.id_kho = Utility.Int32Dbnull(grdPresDetail.GetValue("id_kho"), -1);
                _capnhat_idthuockho.loai = 1;
                _capnhat_idthuockho.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
