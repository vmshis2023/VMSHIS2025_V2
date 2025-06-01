using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.UI.THUOC;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.DANHMUC;
using System.IO;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_themmoi_phieuxuatthuoctutruc : Form
    {
        private DataTable m_dtKhoNhap, m_dtKhoXuat, m_KhoaLinh = new DataTable();
        private int statusHethan = 1;
        public DataTable p_mDataPhieuNhapKho = new DataTable();
        private DataTable m_dtDataPhieuChiTiet = new DataTable();
        public action m_enAction = action.Insert;
        public bool b_Cancel = false;
        public string PerForm;
        public Janus.Windows.GridEX.GridEX grdList;
        public string KIEU_THUOC_VT = "THUOC";
        private DataTable m_PhieuDuTru = new DataTable();
        bool b_Hasloaded=false;
        string SplitterPath = "";
        public frm_themmoi_phieuxuatthuoctutruc()
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            this.Shown += _Shown;
            this.FormClosing += _FormClosing;
            Utility.SetVisualStyle(this);
            dtNgayNhap.Value = globalVariables.SysDate;
            Initevents();
            
        }
        void _FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString() });
        }

        void _Shown(object sender, EventArgs e)
        {
            Try2Splitter();

        }



        void Try2Splitter()
        {
            try
            {
                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count == 1)
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];

                }
            }
            catch (Exception)
            {

            }
        }
        void Initevents()
        {
            cmdExit.Click += new EventHandler(cmdExit_Click);
            grdKhoXuat.KeyDown += new KeyEventHandler(grdKhoXuat_KeyDown);
            this.KeyDown += new KeyEventHandler(frm_themmoi_phieuxuatthuoctutruc_KeyDown);
            txtLyDoXuat._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtLyDoXuat__OnShowData);
            txtNguoinhan._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNguoinhan__OnShowData);
            txtNguoigiao._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNguoigiao__OnShowData);
            txtthuoc._OnEnterMe += new UCs.AutoCompleteTextbox_Thuoc.OnEnterMe(txtthuoc__OnEnterMe);
            txtthuoc._OnSelectionChanged += new UCs.AutoCompleteTextbox_Thuoc.OnSelectionChanged(txtthuoc__OnSelectionChanged);
            cmdAddDetail.Click += new EventHandler(cmdAddDetail_Click);
            cboKhoalinh.SelectedIndexChanged += new EventHandler(cboKhoalinh_SelectedIndexChanged);
            txtthuoc._OnGridSelectionChanged += txtthuoc__OnGridSelectionChanged;
        }
        void txtthuoc__OnGridSelectionChanged(string ID, int id_thuockho, string _name, string Dongia, string phuthu, int tutuc)
        {
            txtthuoc.MyID = ID;
        }
        void cboKhoalinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!b_Hasloaded) return;
                short IDKhoa = Utility.Int16Dbnull(cboKhoalinh.SelectedValue, -1);
                //DataRow[] arrdr = m_dtKhonhan.Select("ID_KHOAPHONG=" + IDKhoa + " or id_khoaphong_captren=" + IDKhoa);
                //DataTable _newTable = m_dtKhonhan.Clone();
                //if (arrdr.Length > 0) _newTable = arrdr.CopyToDataTable();
                DataTable _newTable = CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(IDKhoa, -1, "TATCA,NOITRU", KIEU_THUOC_VT, 1, 100); 
                DataBinding.BindDataCombobox(cboKhoNhap, _newTable,
                                         TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                         "---Chọn tủ thuốc---", true);
                if (_newTable.Rows.Count == 2)
                {
                    cboKhoNhap.SelectedIndex = 1;
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void txtNguoigiao__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNguoigiao.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNguoigiao.myCode;
                txtNguoigiao.Init();
                txtNguoigiao.SetCode(oldCode);
                txtNguoigiao.Focus();
            }
        }

        void cmdAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidDetailData()) return;
                if (Utility.isValidGrid(grdKhoXuat))
                {
                    decimal soluong = Utility.DecimaltoDbnull(grdKhoXuat.CurrentRow.Cells["SO_LUONG_THAT"].Value, 0);
                    decimal SLuongAo = Utility.DecimaltoDbnull(grdKhoXuat.CurrentRow.Cells["SLuongAo"].Value, 0);
                    decimal slkhachuyen = (soluong - SLuongAo);
                    if (Utility.DecimaltoDbnull(txtSoluongdutru.Text, -1) > slkhachuyen)
                    {
                        Utility.SetMsg(lblMsg, string.Format("Số lượng hủy {0} phải <= Số lượng có {1}", txtSoluongdutru.Text, slkhachuyen.ToString()), true);
                        txtSoluongdutru.SelectAll();
                        txtSoluongdutru.Focus();
                        return;
                    }
                    grdKhoXuat.CurrentRow.BeginEdit();
                    grdKhoXuat.CurrentRow.Cells["SO_LUONG_CHUYEN"].Value = Utility.DecimaltoDbnull(txtSoluongdutru.Text, -1);
                    AddDetail(grdKhoXuat.CurrentRow);
                    grdKhoXuat.CurrentRow.EndEdit();
                    UpdateWhenChanged();
                    //-------------------
                    txtSoluongdutru.Clear();
                    txtthuoc.ClearText();
                    txtthuoc.Focus();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }
        bool isValidDetailData()
        {
            errorProvider1.Clear();
            Utility.SetMsg(lblMsg, "", true);
            if (txtthuoc.MyID == txtthuoc.DefaultID)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn thuốc chuyển", true);
                errorProvider1.SetError(txtthuoc, lblMsg.Text);
                txtthuoc.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSoluongdutru.Text, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập số lượng chuyển", true);
                errorProvider1.SetError(txtSoluongdutru, lblMsg.Text);
                txtSoluongdutru.Focus();
                return false;
            }

            return true;
        }

        void txtthuoc__OnSelectionChanged()
        {
            try
            {
                int _idthuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
                if (_idthuoc > 0)
                {
                    var q = from p in grdKhoXuat.GetDataRows()
                            where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                            select p;
                    if (q.Count() > 0)
                    {
                        cmdAddDetail.Enabled = true;
                        grdKhoXuat.MoveTo(q.First());

                    }
                    else
                    {
                        cmdAddDetail.Enabled = false;
                    }
                    var q1 = from p in grdPhieuXuatChiTiet.GetDataRows()
                             where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                             select p;
                    if (q1.Count() > 0)
                    {
                        grdPhieuXuatChiTiet.MoveTo(q1.First());
                    }

                }
                else
                {
                    cmdAddDetail.Enabled = false;
                    grdKhoXuat.MoveFirst();
                }
            }
            catch
            {
            }
            finally
            {
                Utility.SetMsg(lblMsg, cmdAddDetail.Enabled ? "" : "Chú ý: Thuốc bạn chọn không có trong " + cboKhoXuat.Text + " Đề nghị chọn lại thuốc!", false);
            }
        }

        void txtthuoc__OnEnterMe()
        {
            try
            {
                int _idthuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
                if (_idthuoc > 0)
                {
                    var q = from p in grdKhoXuat.GetDataRows()
                            where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                            select p;
                    if (q.Count() > 0)
                    {
                        cmdAddDetail.Enabled = true;
                        grdKhoXuat.MoveTo(q.First());

                    }
                    else
                    {
                        cmdAddDetail.Enabled = false;
                    }
                    var q1 = from p in grdPhieuXuatChiTiet.GetDataRows()
                             where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                             select p;
                    if (q1.Count() > 0)
                    {
                        grdPhieuXuatChiTiet.MoveTo(q1.First());
                    }

                }
                else
                {
                    cmdAddDetail.Enabled = false;
                    grdKhoXuat.MoveFirst();
                }
            }
            catch
            {
            }
        }

        void txtNguoinhan__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNguoinhan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNguoinhan.myCode;
                txtNguoinhan.Init();
                txtNguoinhan.SetCode(oldCode);
                txtNguoinhan.Focus();
            }
        }
        void txtLyDoXuat__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLyDoXuat.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNguoinhan.myCode;
                txtLyDoXuat.Init();
                txtLyDoXuat.SetCode(oldCode);
                txtLyDoXuat.Focus();
            }
        }
       
        void frm_themmoi_phieuxuatthuoctutruc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
            if (e.Control && e.KeyCode == Keys.P) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                grdKhoXuat.Focus();
                grdKhoXuat.MoveFirst();
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdKhoXuat.RootTable.Columns["SO_LUONG_CHUYEN"];
                grdKhoXuat.Col = gridExColumn.Position;
            }
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.Z) Test();
        }
        void Test()
        {
            if (!globalVariables.IsAdmin || m_enAction!=action.Insert || !Utility.isValidGrid(grdKhoXuat)) return;
            foreach (DataRow dr in m_dtDataThuocKho.Rows)
            {
                dr["SO_LUONG_CHUYEN"] = (int)(Utility.Int32Dbnull(dr["SO_LUONG"], 0) / 2);
            }
        }
        void grdKhoXuat_KeyDown(object sender, KeyEventArgs e)
        {
            Janus.Windows.GridEX.GridEXColumn gridExColumn = grdKhoXuat.RootTable.Columns["SO_LUONG_CHUYEN"];
            if (e.Control && e.KeyCode == Keys.Enter )//&& Utility.Int32Dbnull( grdKhoXuat.GetValue( gridExColumn.Key),0)>0 &&  grdKhoXuat.CurrentColumn.Position == gridExColumn.Position)
             {
                 txtFilterName.Focus();
                 grdKhoXuat.Focus();
                 cmdNext_Click(cmdNext, new EventArgs());
                 txtFilterName.Clear();
                 txtFilterName.Focus();
             }
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaThongTin_Click(object sender, EventArgs e)
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuXuatChiTiet.GetCheckedRows())
            {
                int ID = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet].Value);
                TPhieuNhapxuatthuocChitiet.Delete(ID);
                gridExRow.Delete();
                grdPhieuXuatChiTiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc trạng thái thông tin 
        /// của nút
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdSave.Enabled = grdPhieuXuatChiTiet.RowCount > 0;
                cmdPrint.Enabled = grdPhieuXuatChiTiet.RowCount > 0;
                // cmdXoaThongTin.Enabled = grdPhieuXuatChiTiet.GetCheckedRows().Length > 0;
                cboKhoXuat.Enabled = grdPhieuXuatChiTiet.RowCount <= 0;

                // cboKhoXuat.Enabled = grdPhieuXuatChiTiet.RowCount <= 0;

                cmdTaoNhanh.Visible = true;

                if (Utility.Int32Dbnull(cboKhoNhap.SelectedValue) > 0 && m_enAction == action.Insert) cmdTaoNhanh.Enabled = true;
                else cmdTaoNhanh.Enabled = false;
            }
            catch (Exception exception)
            {

            }
            finally
            {
                if (m_enAction == action.View)
                {
                    cmdPrevius.Enabled = cmdNext.Enabled =  cmdTaoNhanh.Enabled = cmdSave.Enabled = cmdAddDetail.Enabled = false;
                }
            }

            //TinhSumThanhTien();
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuNhap_Click(object sender, EventArgs e)
        {
            int IDPhieuNhap = Utility.Int32Dbnull(txtIDPhieuNhapKho.Text, -1);

            TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(IDPhieuNhap);
            if (objPhieuNhap != null)
            {
                if (Utility.Byte2Bool(objPhieuNhap.DuTru.Value))
                    VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuDutru(IDPhieuNhap, "PHIẾU DỰ TRÙ", globalVariables.SysDate);
                else
                {
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_INPHIEUXUATKHO _2LIEN", "0", false) == "1")
                        VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuXuatkho_2lien(IDPhieuNhap, "PHIẾU XUẤT THUỐC TỦ TRỰC", globalVariables.SysDate);
                    else
                        VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuXuatkho(IDPhieuNhap, "PHIẾU XUẤT THUỐC TỦ TRỰC", globalVariables.SysDate);
                }
            }

            
        }
        private void InitData()
        {
            m_KhoaLinh = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            DataBinding.BindDataCombobox(cboNhanVien, CommonLoadDuoc.LAYTHONGTIN_NHANVIEN(),
                                      DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "---Nhân viên---",false);
            cboNhanVien.Enabled = false;
            LoadKho();
        }

        private void LoadKho()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoXuat = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU", "THUOC,THUOCVT", "CHANLE,LE", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NOITRU();
                m_dtKhoNhap = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU", "THUOC,THUOCVT", "CHANLE,LE", 1, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_TUTHUOC();
            }
            else
            {
                m_dtKhoXuat = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU", "VT,THUOCVT", "CHANLE,LE", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOVTTH_LE_NOITRU();
                m_dtKhoNhap = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU", "VT,THUOCVT", "CHANLE,LE", 1, 100, 1);//  CommonLoadDuoc.LAYTHONGTIN_TUVTTH();
            }
            DataBinding.BindDataCombobox(cboKhoNhap, m_dtKhoNhap,
                                          TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                          "---Chọn tủ thuốc---", true);
            cboNhanVien.SelectedValue = globalVariables.gv_intIDNhanvien;

            DataBinding.BindDataCombobox(cboKhoXuat, m_dtKhoXuat,
                                       TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                       "---Kho xuất---", true);

            DataBinding.BindDataCombobox(cboKhoalinh, m_KhoaLinh,
                                      DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "--Chọn khoa lĩnh--", true);

            
        }

        private void getData()
        {
            if (m_enAction == action.Update || m_enAction == action.View)
            {
                TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                if (objPhieuNhap != null)
                {

                    dtNgayNhap.Value = objPhieuNhap.NgayHoadon;
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    dtNgayNhap.Value = Convert.ToDateTime(objPhieuNhap.NgayHoadon);
                    cboKhoalinh.SelectedValue = Utility.sDbnull(objPhieuNhap.IdKhoalinh);
                        cboKhoXuat.SelectedValue = Utility.sDbnull(objPhieuNhap.IdKhoxuat);
                        cboKhoNhap.SelectedValue = Utility.sDbnull(objPhieuNhap.IdKhonhap);
                        cboNhanVien.SelectedValue = Utility.sDbnull(objPhieuNhap.IdNhanvien);
                    
                    txtNo.Text = objPhieuNhap.TkNo;
                    txtCo.Text = objPhieuNhap.TkCo;
                    txtSoCT.Text = objPhieuNhap.SoChungtuKemtheo;
                    txtNguoinhan._Text=objPhieuNhap.NguoiNhan;
                    txtNguoigiao._Text = objPhieuNhap.NguoiGiao;
                    txtLyDoXuat._Text = objPhieuNhap.MotaThem;
                    m_dtDataPhieuChiTiet =
                        new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    Utility.SetDataSourceForDataGridEx_Basic(grdPhieuXuatChiTiet, m_dtDataPhieuChiTiet, true, true, "1=1", "");
                }
            }
            if (m_enAction == action.Insert)
            {
                m_dtDataPhieuChiTiet =
                       new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(-100);
                Utility.SetDataSourceForDataGridEx_Basic(grdPhieuXuatChiTiet, m_dtDataPhieuChiTiet, true, true, "SO_LUONG>0", "");
            }
            UpdateWhenChanged();
        }
        private void SetStatusControl()
        {
        }
        private void frm_themmoi_phieuxuatthuoctutruc_Load(object sender, EventArgs e)
        {
            txtLyDoXuat.Init();
            txtNguoinhan.Init();
            txtNguoigiao.Init();
            AutocompleteThuoc();
            InitData();
            getData();
            b_Hasloaded = true;
            ModifyCommand();
        }
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = SPs.ThuocLaythuoctrongkhoxuatAutocomplete(Utility.Int32Dbnull(cboKhoXuat.SelectedValue, -1), KIEU_THUOC_VT).GetDataSet().Tables[0];// new Select().From(DmucThuoc.Schema).Where(DmucThuoc.KieuThuocvattuColumn).IsEqualTo(KIEU_THUOC_VT).And(DmucThuoc.TrangThaiColumn).IsEqualTo(1).ExecuteDataSet().Tables[0];
                if (_dataThuoc == null)
                {
                    txtthuoc.dtData = null;
                    return;
                }
                txtthuoc.dtData = _dataThuoc;
                txtthuoc.ChangeDataSource();
            }
            catch
            {
            }
        }
        //hàm thực hiện việc ẩn hiện thông tin 
        private void cboKhoXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _rowFilter = "1=1";
            try
            {
                if (cboKhoXuat.SelectedIndex > 0)
                {
                    _rowFilter = string.Format("{0}<>{1}", TDmucKho.Columns.IdKho,
                                               Utility.Int32Dbnull(cboKhoXuat.SelectedValue));
                }
            }
            catch (Exception)
            {
                _rowFilter = "1=1";
            }
            m_dtKhoNhap.DefaultView.RowFilter = _rowFilter;
            m_dtKhoNhap.AcceptChanges();
            SetStatusControl();
            getThuocTrongKho();
            AutocompleteThuoc();
            ModifyCommand();

        }
        private DataTable m_dtDataThuocKho=new DataTable();
        private void getThuocTrongKho()
        {
            try
            {
                m_dtDataThuocKho =
            SPs.ThuocLaythuoctrongkhoxuat(Utility.Int32Dbnull(cboKhoXuat.SelectedValue),
                                            statusHethan, KIEU_THUOC_VT).GetDataSet().Tables[0];

               //Xử lý số lượng của các thuốc chia theo số lượng chia
                foreach (DataRow drv in m_dtDataThuocKho.Rows)
                {
                    //
                    if (Utility.sDbnull(drv[DmucThuoc.Columns.CoChiathuoc], "0") == "1")
                    {
                        drv["SO_LUONG"] = drv["SO_LUONG_THEODONVI_CHIA"];
                        drv["SO_LUONG_THAT"] = drv["SO_LUONG_THAT_THEODONVI_CHIA"];
                        if (Utility.sDbnull(drv["ten_donvichia"], "") != "")
                            drv["ten_donvitinh"] = drv["ten_donvichia"];

                    }

                }
                
                m_dtDataThuocKho.AcceptChanges();
                Utility.SetDataSourceForDataGridEx_Basic(grdKhoXuat, m_dtDataThuocKho, true, true, "So_luong>0", "");

            }
            catch (Exception)
            {

                //throw;
            }
        }


        /// <summary>
        /// HÀM HỰC HIỆN VIỆC LƯU LẠI THÔNG TIN 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!InValiNhapKho()) return;
            PerformAction();
        }
        private void PerformAction()
        {
            try
            {
                switch (m_enAction)
                {
                    case action.Insert:
                        ThemPhieuXuatKho();
                        break;
                    case action.Update:
                        UpdatePhieuXuatKho();
                        break;
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        #region "khai báo các đối tượng để thực hiện việc "
        private TPhieuNhapxuatthuoc CreatePhieuNhapKho()
        {
            TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = new TPhieuNhapxuatthuoc();
            if (m_enAction == action.Update)
            {
                objTPhieuNhapxuatthuoc.MarkOld();
                objTPhieuNhapxuatthuoc.IsLoaded = true;
                objTPhieuNhapxuatthuoc.IdPhieu = Utility.Int32Dbnull(txtIDPhieuNhapKho.Text, -1);
            }
            objTPhieuNhapxuatthuoc.Vat = 0;
            objTPhieuNhapxuatthuoc.SoHoadon = string.Empty;
            objTPhieuNhapxuatthuoc.IdKhonhap = Utility.Int16Dbnull(cboKhoNhap.SelectedValue, -1);
            objTPhieuNhapxuatthuoc.IdKhoxuat = Utility.Int16Dbnull(cboKhoXuat.SelectedValue, -1);
            objTPhieuNhapxuatthuoc.MaNhacungcap = "";
            objTPhieuNhapxuatthuoc.MotaThem = txtLyDoXuat.Text;
            objTPhieuNhapxuatthuoc.TrangThai = 0;
            objTPhieuNhapxuatthuoc.KieuThuocvattu = KIEU_THUOC_VT;
            objTPhieuNhapxuatthuoc.IdKhoalinh = Utility.Int16Dbnull(cboKhoalinh.SelectedValue, 0);
            objTPhieuNhapxuatthuoc.MotaThem = "Xuất thuốc tủ trực khoa nội trú";
            objTPhieuNhapxuatthuoc.IdNhanvien = Utility.Int16Dbnull(cboNhanVien.SelectedValue, -1);
            if (Utility.Int32Dbnull(objTPhieuNhapxuatthuoc.IdNhanvien, -1) <= 0)
                objTPhieuNhapxuatthuoc.IdNhanvien = globalVariables.gv_intIDNhanvien;
            objTPhieuNhapxuatthuoc.TkNo = Utility.DoTrim(txtNo.Text);
            objTPhieuNhapxuatthuoc.TkCo = Utility.DoTrim(txtCo.Text);
            objTPhieuNhapxuatthuoc.SoChungtuKemtheo = Utility.DoTrim(txtSoCT.Text);
            objTPhieuNhapxuatthuoc.NgayHoadon = dtNgayNhap.Value;
            objTPhieuNhapxuatthuoc.NgayTao = globalVariables.SysDate;
            objTPhieuNhapxuatthuoc.NguoiTao = globalVariables.UserName;
            objTPhieuNhapxuatthuoc.NguoiGiao =Utility.DoTrim( txtNguoigiao.Text);
            objTPhieuNhapxuatthuoc.NguoiNhan =Utility.DoTrim( txtNguoinhan.Text);
            objTPhieuNhapxuatthuoc.LoaiPhieu = (byte)LoaiPhieu.PhieuXuatKhoa;
            objTPhieuNhapxuatthuoc.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuXuatKhoa);
            objTPhieuNhapxuatthuoc.DuTru = Utility.Bool2byte(chkPhieudutru.Checked);
            return objTPhieuNhapxuatthuoc;
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin chi tiết
        /// </summary>
        /// <returns></returns>
        private TPhieuNhapxuatthuocChitiet[] CreateArrPhieuChiTiet()
        {
           
            List<TPhieuNhapxuatthuocChitiet> lstItems = new List<TPhieuNhapxuatthuocChitiet>();
            foreach (DataRow dr in m_dtDataPhieuChiTiet.Rows)
            {
              
                  TPhieuNhapxuatthuocChitiet  newItem = new TPhieuNhapxuatthuocChitiet();
                    newItem.IdThuoc =
                        Utility.Int32Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc]);
                    newItem.NgayHethan =
                        Convert.ToDateTime(dr[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan]).Date;
                    newItem.GiaBan = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaBan]);
                    newItem.GiaNhap = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap]);
                    newItem.DonGia = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.DonGia]);
                    newItem.SoLo = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoLo], "");
                    newItem.SoDky = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoDky], "");
                    newItem.SoQdinhthau = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau], "");
                    newItem.SoLuong = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoLuong], 0);
                    newItem.SluongChia = Utility.Int32Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SluongChia], 0);
                    newItem.ThanhTien =
                        Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap]) *
                        Utility.Int32Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoLuong]);

                    newItem.Vat = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.Vat], 0);
                    newItem.MotaThem = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.MotaThem]);
                    newItem.IdPhieu = Utility.Int32Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu], -1);
                    newItem.NgayNhap = Convert.ToDateTime(dr[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap]).Date;
                    newItem.GiaBhyt = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt]);
                    newItem.GiaPhuthuDungtuyen = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen]);
                    newItem.GiaPhuthuTraituyen = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen]);
                    newItem.IdQdinh = Utility.Int64Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.IdQdinh], -1);
                    newItem.IdThuockho = Utility.Int64Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho], -1);
                    newItem.IdChuyen = Utility.Int64Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.IdChuyen], -1);
                    newItem.MaNhacungcap = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap]);
                    newItem.KieuThuocvattu = KIEU_THUOC_VT;
                    newItem.ChietKhau = Utility.Int32Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau], -1);
                    lstItems.Add(newItem);
                   
            }
            return lstItems.ToArray();
        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        private void ThemPhieuXuatKho()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = CreatePhieuNhapKho();

            ActionResult actionResult = new XuatThuoc().ThemPhieuXuatKho(objPhieuNhap, CreateArrPhieuChiTiet());
            switch (actionResult)
            {
                case ActionResult.Success:
                    txtIDPhieuNhapKho.Text = Utility.sDbnull(objPhieuNhap.IdPhieu);
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow newDr = p_mDataPhieuNhapKho.NewRow();
                    Utility.FromObjectToDatarow(objTPhieuNhapxuatthuoc, ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoNhap.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoXuat.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khoxuat"] = Utility.sDbnull(objKho.TenKho);
                    newDr["ten_khoa"] = Utility.sDbnull(cboKhoalinh.Text);
                    p_mDataPhieuNhapKho.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdList, TPhieuNhapxuatthuoc.Columns.IdPhieu, Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    //Utility.ShowMsg("Bạn thêm mới phiếu chuyển kho thành công", "Thông báo");
                    m_enAction = action.Insert;
                  
                    b_Cancel = true;
                    this.Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm phiếu", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }
        private void UpdatePhieuXuatKho()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = CreatePhieuNhapKho();

            ActionResult actionResult = new XuatThuoc().UpdatePhieuXuatKho(objPhieuNhap, CreateArrPhieuChiTiet());
            switch (actionResult)
            {
                case ActionResult.Success:
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow[] arrDr =
                        p_mDataPhieuNhapKho.Select(string.Format("{0}={1}", TPhieuNhapxuatthuoc.Columns.IdPhieu,
                                                                 Utility.Int32Dbnull(txtIDPhieuNhapKho.Text)));
                    if (arrDr.GetLength(0) > 0)
                    {
                        arrDr[0].Delete();
                    }
                    DataRow newDr = p_mDataPhieuNhapKho.NewRow();
                    Utility.FromObjectToDatarow(objTPhieuNhapxuatthuoc, ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoNhap.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoXuat.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khoxuat"] = Utility.sDbnull(objKho.TenKho);
                    newDr["ten_khoa"] = Utility.sDbnull(cboKhoalinh.Text);
                    p_mDataPhieuNhapKho.Rows.Add(newDr);

                    Utility.GonewRowJanus(grdList, TPhieuNhapxuatthuoc.Columns.IdPhieu, Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    //Utility.ShowMsg("Bạn sửa  phiếu thành công", "Thông báo");
                    m_enAction = action.Insert;
                    this.Close();
                    b_Cancel = true;
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện việc Invalinhap khoa thuốc
        /// </summary>
        /// <returns></returns>
        private bool InValiNhapKho()
        {
            Utility.SetMsg(lblMsg, "", false);
            TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
            if (m_enAction == action.Update)
            {
                if (objPhieuNhap == null)
                {
                    Utility.ShowMsg("Phiếu đã được người khác xóa trong lúc bạn đang mở xem nên bạn không được phép thao tác. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                    return false;
                }
                else
                {

                    DataTable dtPaymentData = SPs.TPhieunhapxuatKiemtratrangthai(objPhieuNhap.IdPhieu).GetDataSet().Tables[0];
                    if (dtPaymentData.Rows.Count > 0)
                    {
                        Utility.ShowMsg("Phiếu đã được người khác xác nhận trong lúc bạn đang mở xem nên bạn không được phép sửa/xóa phiếu. Nhấn OK để kết thúc và liên hệ nội bộ khoa phòng để biết");
                        return false;
                    }
                }
            }
            //if (Utility.DoTrim( txtLyDoXuat.Text)=="")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải chọn lý do " + (chkPhieudutru.Checked ? " dự trù" : " xuất thuốc"), true);
            //    txtLyDoXuat.Focus();
            //    return false;
            //}
            if (cboKhoXuat.SelectedValue.ToString()=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn kho xuất", true);
                errorProvider1.SetError(cboKhoXuat, lblMsg.Text);
                cboKhoXuat.Focus();
                return false;
            }
            if (cboKhoalinh.SelectedValue.ToString() == "-1")
            {
                Utility.ShowMsg("Bạn phải chọn khoa lĩnh", "Thông báo", MessageBoxIcon.Warning);
                errorProvider1.SetError(cboKhoalinh, "Bạn phải chọn khoa lĩnh");
                cboKhoalinh.Focus();
                return false;
            }
            if (cboKhoNhap.SelectedValue.ToString() == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn tủ thuốc để nhập thuốc", true);
                errorProvider1.SetError(cboKhoNhap, lblMsg.Text);
                cboKhoNhap.Focus();
                return false;
            }

            if (m_enAction == action.Insert)
            {
                if (grdPhieuXuatChiTiet.GetDataRows().Length <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn phải chọn thuốc cần xuất từ kho lẻ nội trú sang tủ trực", true);
                    errorProvider1.SetError(cmdNext, lblMsg.Text);
                    cmdNext.Focus();
                    return false;
                }

            }
            return true;
        }

        private void chkIsHetHan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIsHetHan.Checked) statusHethan = 1;
                else statusHethan = -1;
                getThuocTrongKho();
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        private void cmdGetData_Click(object sender, EventArgs e)
        {
            getThuocTrongKho();
        }
        /// <summary>
        /// hàm thực hiện việc thay đổi thôn gtin của trên lưới 
        /// loc thông tin của thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterText(txtFilterName.Text.Trim());
            }
            catch (Exception)
            {
                
                //throw;
            }
           
        }
        private void FilterText(string prefixText)
        {
            try
            {
                m_dtDataThuocKho.DefaultView.RowFilter = "1=1";
                if (!string.IsNullOrEmpty(prefixText))
                {
                    m_dtDataThuocKho.DefaultView.RowFilter = "TEN_THUOC like '%" + prefixText + "%' OR ma_thuoc like '%" + prefixText + "%' OR shortname like '%" + prefixText + "%'";
                }
            }
            catch (Exception)
            {

                /// throw;
            }
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt của lọc thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter||e.KeyCode==Keys.PageDown)
            {
                grdKhoXuat.Focus();
                grdKhoXuat.MoveFirst();
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdKhoXuat.RootTable.Columns["SO_LUONG_CHUYEN"];
                grdKhoXuat.Col = gridExColumn.Position;
            }
        }

        private void grdKhoXuat_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if(e.Column.Key=="SO_LUONG_CHUYEN")
            {
                int soluongchuyen = Utility.Int32Dbnull(e.Value);
                int soluongchuyencu = Utility.Int32Dbnull(e.InitialValue);
                int soluongthat = Utility.Int32Dbnull(grdKhoXuat.GetValue("So_luong"));
                if(soluongchuyen<0)
                {
                    Utility.ShowMsg("Số lượng thuốc cần chuyển phải >=0","Thông báo",MessageBoxIcon.Warning);
                    e.Cancel = true;
                }else
                {
                    if(soluongchuyen>soluongthat)
                    {
                        Utility.ShowMsg("Số lượng thuốc cần chuyển phải <= số lượng thuốc có trong kho", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = soluongchuyencu;
                        e.Cancel = true;
                    }
                    else
                    {
                        grdKhoXuat.CurrentRow.IsChecked = soluongchuyen>0;
                    }
                }
            }
        }
        /// <summary>
        /// hàm thực hiện việc đấy thông tin của 
        /// dược từ kho này sang kho kia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNext_Click(object sender, EventArgs e)
        {
            AddDetails();
        }
        private void AddDetails()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetDataRows())
                {
                    AddDetail(gridExRow);
                }
                UpdateWhenChanged();
                ResetValueInGridEx();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển thuốc:\n" + ex.Message);
            }
        }
        private void AddDetail(Janus.Windows.GridEX.GridEXRow gridExRow)
        {
            try
            {
                string manhacungcap = "";
                string NgayHethan = "";
                DateTime dtmNgayHethan = DateTime.Now;
                DateTime NgayNhap = DateTime.Now;
                string solo = "";
                int id_thuoc = -1;
                decimal dongia = 0m;
                decimal Giaban = 0m;
                decimal GiaBhyt = 0m;
                Int32 soluongchuyen = 0;

                decimal vat = 0m;
                int isHetHan = 0;
                long IdThuockho = 0;
                int soluongthat = 0;
                int tongsoluongchuyen = 0;

                tongsoluongchuyen = 0;
                int soluongao = Utility.Int32Dbnull(gridExRow.Cells["sLuongAo"].Value,0);
                soluongthat = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_THAT"].Value);
                soluongchuyen = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_CHUYEN"].Value, 0);
                if (soluongchuyen > 0)
                {
                    NgayHethan = Utility.sDbnull(gridExRow.Cells["NGAY_HET_HAN"].Value);
                    dtmNgayHethan = Convert.ToDateTime(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan].Value).Date;
                    NgayNhap = Convert.ToDateTime(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap].Value).Date;
                    solo = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLo].Value);
                    id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuoc].Value, -1);

                    dongia = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaNhap].Value, 0);
                    Giaban = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBan].Value, 0);
                    GiaBhyt = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBhyt].Value, 0);
                    vat = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.Vat].Value, 0);
                    isHetHan = Utility.Int32Dbnull(gridExRow.Cells["IsHetHan"].Value, 0);
                    manhacungcap = Utility.sDbnull(gridExRow.Cells[TThuockho.Columns.MaNhacungcap].Value, 0);
                    IdThuockho = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuockho].Value, -1);
                    DataRow[] arrDr = m_dtDataPhieuChiTiet.Select(TPhieuNhapxuatthuocChitiet.Columns.IdChuyen + "=" + IdThuockho.ToString());
                    if (arrDr.Length <= 0)
                    {
                        DataRow drv = m_dtDataPhieuChiTiet.NewRow();
                        drv[TPhieuNhapxuatthuocChitiet.Columns.MotaThem] = String.Empty;
                        if (m_dtDataPhieuChiTiet.Columns.Contains(DmucThuoc.Columns.MaThuoc) && gridExRow.GridEX.RootTable.Columns.Contains(DmucThuoc.Columns.MaThuoc)) drv[DmucThuoc.Columns.MaThuoc] = Utility.sDbnull(gridExRow.Cells[DmucThuoc.Columns.MaThuoc].Value);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc] = id_thuoc;
                        drv["ten_donvitinh"] = Utility.sDbnull(gridExRow.Cells["ten_donvitinh"].Value);
                        drv["IsHetHan"] = isHetHan;
                        DmucThuoc objLDrug = DmucThuoc.FetchByID(id_thuoc);
                        if (objLDrug != null)
                        {
                            drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objLDrug.TenThuoc);
                            drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(objLDrug.HamLuong);
                            drv[DmucThuoc.Columns.MaHoatchat] = Utility.sDbnull(objLDrug.MaHoatchat);
                            drv[DmucThuoc.Columns.NuocSanxuat] = Utility.sDbnull(objLDrug.NuocSanxuat);
                            drv[DmucThuoc.Columns.HangSanxuat] = Utility.sDbnull(objLDrug.HangSanxuat);
                        }
                        drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = vat;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt] = GiaBhyt;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen] = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.PhuthuDungtuyen].Value, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen] = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.PhuthuTraituyen].Value, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap] = NgayNhap;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap] = dongia;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.DonGia] = dongia;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap] = manhacungcap;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLo] = solo;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoDky] = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoDky].Value);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau] = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau].Value);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdQdinh] = Utility.Int64Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdQdinh].Value);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho] = IdThuockho;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBan] = Giaban;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdChuyen] = IdThuockho;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = soluongchuyen;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = dongia * soluongchuyen;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau] = 0;

                        drv[TPhieuNhapxuatthuocChitiet.Columns.SluongChia] = Utility.Int32Dbnull(gridExRow.Cells[DmucThuoc.Columns.SluongChia].Value, 0);
                        

                        drv["NGAY_HET_HAN"] = NgayHethan;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = dtmNgayHethan;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu] = -1;
                        tongsoluongchuyen = soluongchuyen;
                        m_dtDataPhieuChiTiet.Rows.Add(drv);
                    }
                    else
                    {
                        arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + soluongchuyen;
                        tongsoluongchuyen = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]);
                        m_dtDataPhieuChiTiet.AcceptChanges();
                    }
                    //Update lại dữ liệu từ kho xuất
                    gridExRow.BeginEdit();
                    //gridExRow.Cells["SO_LUONG"].Value = soluongthat - tongsoluongchuyen - soluongao;
                    gridExRow.Cells["SO_LUONG_CHUYEN"].Value = 0;
                    gridExRow.Cells["SLuongAo"].Value = Utility.Int32Dbnull(gridExRow.Cells["SLuongAo"].Value) + soluongchuyen;
                    gridExRow.IsChecked = false;
                    gridExRow.EndEdit();
                }
                grdKhoXuat.UpdateData();
                m_dtDataThuocKho.AcceptChanges();
                //UpdateWhenChanged();
                //ResetValueInGridEx();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển thuốc:\n" + ex.Message);
            }
        }
        private void RemoveDetails()
        {
            try
            {

                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuXuatChiTiet.GetCheckedRows())
                {
                    RemoveDetail(gridExRow);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy chuyển thuốc:\n" + ex.Message);
            }
            finally
            {
                UpdateWhenChanged();
                ResetValueInGridEx();
                ModifyCommand();
            }
        }
        private void RemoveDetail(Janus.Windows.GridEX.GridEXRow gridExRow)
        {
            try
            {
                string manhacungcap = "";
                string NgayHethan = "";
                DateTime dtmNgayHethan = DateTime.Now;
                string solo = "";
                int id_thuoc = -1;
                decimal dongia = 0m;
                decimal Giaban = 0m;
                decimal GiaBhyt = 0m;
                Int32 soluong = 0;
                decimal vat = 0m;
                int isHetHan = 0;
                long IdThuockho = 0;
                DateTime NgayNhap = DateTime.Now;
                NgayNhap = Convert.ToDateTime(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap].Value).Date;
                dtmNgayHethan = Convert.ToDateTime(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan].Value).Date;
                NgayHethan = Utility.sDbnull(gridExRow.Cells["NGAY_HET_HAN"].Value);
                solo = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLo].Value);
                id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuoc].Value, -1);
                IdThuockho = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdChuyen].Value, -1);
                dongia = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaNhap].Value, 0);
                GiaBhyt = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBhyt].Value, 0);
                Giaban = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBan].Value, 0);
                soluong = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG"].Value, 0);
                vat = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.Vat].Value, 0);
                isHetHan = Utility.Int32Dbnull(gridExRow.Cells["IsHetHan"].Value, 0);
                manhacungcap = Utility.sDbnull(gridExRow.Cells[TThuockho.Columns.MaNhacungcap].Value, 0);



                DataRow[] arrDr = m_dtDataThuocKho.Select(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho + "=" + IdThuockho.ToString());
                if (arrDr.Length <= 0)
                {
                    DataRow drv = m_dtDataThuocKho.NewRow();


                    drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc] = id_thuoc;
                    drv["ten_donvitinh"] = Utility.sDbnull(gridExRow.Cells["ten_donvitinh"].Value);
                    drv["IsHetHan"] = isHetHan;
                    DmucThuoc objLDrug = DmucThuoc.FetchByID(id_thuoc);
                    if (objLDrug != null)
                    {
                        drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objLDrug.TenThuoc);
                        drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(objLDrug.HamLuong);
                        drv[DmucThuoc.Columns.MaHoatchat] = Utility.sDbnull(objLDrug.MaHoatchat);
                        drv[DmucThuoc.Columns.NuocSanxuat] = Utility.sDbnull(objLDrug.NuocSanxuat);
                        drv[DmucThuoc.Columns.HangSanxuat] = Utility.sDbnull(objLDrug.HangSanxuat);
                    }
                    drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = vat;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt] = GiaBhyt;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap] = NgayNhap;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap] = dongia;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.DonGia] = dongia;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap] = manhacungcap;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.SoLo] = solo;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.SoDky] = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoDky].Value);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau] = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau].Value);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho] = IdThuockho;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBan] = Giaban;
                    
                    drv[TPhieuNhapxuatthuocChitiet.Columns.SluongChia] = Utility.Int32Dbnull(gridExRow.Cells[DmucThuoc.Columns.SluongChia].Value, 0); ;

                    drv[TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = soluong;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = dongia * soluong;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau] = 0;
                    drv["NGAY_HET_HAN"] = NgayHethan;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = dtmNgayHethan;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu] = -1;
                    m_dtDataThuocKho.Rows.Add(drv);

                }
                else
                {
                    arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + soluong;
                    arrDr[0]["SLuongAo"] = Utility.DecimaltoDbnull(arrDr[0]["SLuongAo"]) - soluong;
                    //arrDr[0]["SO_LUONG_THAT"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"],0) + Utility.Int32Dbnull(arrDr[0]["sLuongAo"], 0);
                    m_dtDataThuocKho.AcceptChanges();
                }
                gridExRow.Delete();
                grdPhieuXuatChiTiet.UpdateData();
                grdPhieuXuatChiTiet.Refresh();
                m_dtDataPhieuChiTiet.AcceptChanges();
                m_dtDataThuocKho.AcceptChanges();

                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy chuyển thuốc:\n" + ex.Message);
            }
        }
       
        #region unused
        private void ResetValueInGridEx()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetCheckedRows())
            {
                gridExRow.BeginEdit();
                gridExRow.Cells["SO_LUONG_CHUYEN"].Value = 0;
                gridExRow.IsChecked = false;
                gridExRow.BeginEdit();
            }
            grdKhoXuat.UpdateData();
            m_dtDataThuocKho.AcceptChanges();
        }
        private void UpdateWhenChanged()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    var query = from thuoc in grdPhieuXuatChiTiet.GetDataRows()
                                let sl = Utility.Int32Dbnull(thuoc.Cells["SO_LUONG"].Value)
                                let idchuyen = Utility.Int32Dbnull(thuoc.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdChuyen].Value)
                                where idchuyen == Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho].Value)
                                select sl;
                    if (query.Any())
                    {
                        int soluong = Utility.Int32Dbnull(query.FirstOrDefault());
                        int soluongthat = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_THAT"].Value);
                        int soluongao = Utility.Int32Dbnull(gridExRow.Cells["SLuongAo"].Value);
                        gridExRow.BeginEdit();
                        gridExRow.Cells["SO_LUONG"].Value = soluongthat  - soluongao;
                        gridExRow.Cells["SO_LUONG_CHUYEN"].Value = 0;
                        gridExRow.EndEdit();
                        grdKhoXuat.UpdateData();
                        m_dtDataThuocKho.AcceptChanges();
                    }
                }
            }
        }
        #endregion
       

        private void cmdPrevius_Click(object sender, EventArgs e)
        {
            RemoveDetails();
        }

       

        private void cboKhoNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void cmdTaoNhanh_Click(object sender, EventArgs e)
        {
            //Kiểm tra xem đã chọn kho nhận hay chưa
            if (cboKhoNhap.Items.Count <= 0 || cboKhoNhap.SelectedIndex <= -1 || cboKhoNhap.SelectedValue.ToString() == "-1")
            {
                Utility.ShowMsg("Bạn cần chọn kho nhận trước khi thực hiện tính năng tạo phiếu dự trù(bổ sung) cho kho nhận");
                return;
            }
            if (m_dtDataPhieuChiTiet.Rows.Count > 0)
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn tạo nhanh phiếu dự trù cho kho " + cboKhoNhap.Text + "\nChú ý: Nếu đồng ý tạo nhanh thì toàn bộ chi tiết đang chuyển sang kho " + cboKhoNhap.Text + " sẽ được xóa đi.","Xác nhận", true))
                    return;
            m_dtDataPhieuChiTiet.Clear();
            m_PhieuDuTru = SPs.ThuocLapphieudutru(Utility.Int32Dbnull(cboKhoNhap.SelectedValue, -1), KIEU_THUOC_VT).GetDataSet().Tables[0];
            Dictionary<int, string> lstNotExists = new Dictionary<int, string>();
            if (m_PhieuDuTru.Rows.Count <= 0)
            {
                Utility.ShowMsg("Chưa có dữ liệu dự trù. Bạn chỉ có thể sử dụng tính năng này sau khi đã lập dự trù cho Thuốc-Vật tư tiêu hao");
                return;
            }
            foreach (DataRow drDuTru in m_PhieuDuTru.Rows)
            {
                int ID_THUOC=Utility.Int32Dbnull(drDuTru[DmucThuoc.Columns.IdThuoc]);
                string TEN_THUOC = Utility.sDbnull(drDuTru[DmucThuoc.Columns.TenThuoc], "Unknown");
                int soluongthuoc_canchuyen = Utility.Int32Dbnull(drDuTru["SO_LUONG_CHUYEN"]);
                if (soluongthuoc_canchuyen > 0)//Chỉ lấy các thuốc có lượng cần chuyển >0
                {
                    int soluong_chuyen = 0;
                    //Lấy dữ liệu kho xuất thuốc dự trù
                    DataRow[] arrDR = m_dtDataThuocKho.Select(DmucThuoc.Columns.IdThuoc+"=" + ID_THUOC,"NGAY_HETHAN ASC");
                    if (arrDR.Length > 0)//Nếu có thuốc này từ kho xuất
                    {
                        string manhacungcap = "";
                        string NgayHethan = "";
                        string solo = "";
                        int id_thuoc = -1;
                        decimal dongia = 0m;
                        decimal GiaBhyt = 0m;
                        decimal Giaban = 0m;
                        Int32 soluongchuyen = 0;
                        Int32 Tongsoluongchuyen = 0;
                        decimal vat = 0m;
                        int isHetHan = 0;
                        long IdThuockho = 0;
                        DateTime NgayNhap = DateTime.Now;
                        int TongSoluong =Utility.Int32Dbnull( arrDR.CopyToDataTable().Compute("SUM(SO_LUONG)", "1=1"),0);
                        if (TongSoluong >= soluongthuoc_canchuyen)
                        {
                            foreach (DataRow _dr in arrDR)
                            {
                                Tongsoluongchuyen = 0;
                                if (soluongthuoc_canchuyen > 0)
                                {
                                    Int32 soluong = Utility.Int32Dbnull(_dr["SO_LUONG"], 0);
                                    if (soluong > soluongthuoc_canchuyen)
                                    {
                                        soluong_chuyen = soluongthuoc_canchuyen;
                                        soluongthuoc_canchuyen = 0;
                                    }
                                    else
                                    {
                                        soluong_chuyen = soluong;
                                        soluongthuoc_canchuyen = soluongthuoc_canchuyen - soluong_chuyen;
                                    }

                                    NgayHethan = Utility.sDbnull(_dr["NGAY_HET_HAN"]);
                                    solo = Utility.sDbnull(_dr[TPhieuNhapxuatthuocChitiet.Columns.SoLo]);
                                    id_thuoc = Utility.Int32Dbnull(_dr[TThuockho.Columns.IdThuoc], -1);
                                    NgayNhap = Convert.ToDateTime(_dr[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap]).Date;
                                    dongia = Utility.DecimaltoDbnull(_dr[TThuockho.Columns.GiaNhap], 0);
                                    Giaban = Utility.DecimaltoDbnull(_dr[TThuockho.Columns.GiaBan], 0);
                                    GiaBhyt = Utility.DecimaltoDbnull(_dr[TThuockho.Columns.GiaBhyt], 0);
                                    vat = Utility.DecimaltoDbnull(_dr[TThuockho.Columns.Vat], 0);
                                    isHetHan = Utility.Int32Dbnull(_dr["IsHetHan"], 0);
                                    manhacungcap = Utility.sDbnull(_dr[TThuockho.Columns.MaNhacungcap], 0);
                                    IdThuockho = Utility.Int32Dbnull(_dr[TThuockho.Columns.IdThuockho], -1);
                                    DataRow[] arrDr = m_dtDataPhieuChiTiet.Select(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho + "=" + IdThuockho.ToString());
                                    if (arrDr.Length <= 0)
                                    {
                                        DataRow drv = m_dtDataPhieuChiTiet.NewRow();
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.MotaThem] = String.Empty;

                                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc] = id_thuoc;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap] = NgayNhap;
                                        drv["ten_donvitinh"] = Utility.sDbnull(_dr["ten_donvitinh"]);
                                        drv["IsHetHan"] = isHetHan;
                                        DmucThuoc objLDrug = DmucThuoc.FetchByID(id_thuoc);
                                        if (objLDrug != null)
                                        {
                                            drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objLDrug.TenThuoc);
                                            drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(objLDrug.HamLuong);
                                            drv[DmucThuoc.Columns.MaHoatchat] = Utility.sDbnull(objLDrug.MaHoatchat);
                                            drv[DmucThuoc.Columns.NuocSanxuat] = Utility.sDbnull(objLDrug.NuocSanxuat);
                                            drv[DmucThuoc.Columns.HangSanxuat] = Utility.sDbnull(objLDrug.HangSanxuat);
                                        }
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt] = GiaBhyt;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = vat;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap] = dongia;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.DonGia] = dongia;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap] = manhacungcap;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLo] = solo;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho] = IdThuockho;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBan] = Giaban;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdChuyen] = IdThuockho;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = soluong_chuyen;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = dongia * soluong_chuyen;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau] = 0;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = NgayHethan;
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu] = -1;
                                        Tongsoluongchuyen = soluong_chuyen;
                                        m_dtDataPhieuChiTiet.Rows.Add(drv);
                                    }
                                    else
                                    {
                                        arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + soluongchuyen;
                                        Tongsoluongchuyen = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]);
                                        m_dtDataPhieuChiTiet.AcceptChanges();
                                    }
                                }
                                //Update lại dữ liệu từ kho xuất
                                _dr["SO_LUONG"] = Utility.Int32Dbnull(_dr["SO_LUONG"], 0) - Tongsoluongchuyen;
                                _dr["SO_LUONG_CHUYEN"] = 0;
                            }

                        }
                        else
                        {
                            if (!lstNotExists.ContainsKey(ID_THUOC)) lstNotExists.Add(ID_THUOC, TEN_THUOC+string.Format( "-->không đủ(Số lượng có:{0}-số lượng cần chuyển:{1}",TongSoluong.ToString(),soluongthuoc_canchuyen.ToString()));
                        }
                       

                        //UpdateWhenChanged();
                        //ResetValueInGridEx();
                        ModifyCommand();
                    }
                    else
                    {
                        if (!lstNotExists.ContainsKey(ID_THUOC)) lstNotExists.Add(ID_THUOC, TEN_THUOC+"-->Không có trong kho xuất");
                    }

                  
                }
            }
            if (lstNotExists.Count > 0)//Cảnh báo
            {
                string msg = string.Join("\n", lstNotExists.Values.ToArray());
                Utility.ShowMsg("Hệ thống không tự động chuyển một số Thuốc(Vật tư) vì lý do sau đây:\n" + msg);
            }
            grdKhoXuat.UpdateData();
            grdPhieuXuatChiTiet.UpdateData();
            ModifyCommand();
        }

        private DataTable CreateBangThuoc(int idthuoc)
        {
            DataTable m_BangThuoc = new DataTable();
            m_BangThuoc = m_dtDataThuocKho.Clone();
            foreach (DataRow drthuoc in m_dtDataThuocKho.Rows)
            {
                if(Utility.Int32Dbnull(drthuoc["ID_THUOC"]) == idthuoc)
                {;
                    m_BangThuoc.ImportRow(drthuoc);
                    //m_BangThuoc.AcceptChanges();
                }
            }
            return m_BangThuoc;
        }
    }
}
