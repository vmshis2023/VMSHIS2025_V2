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
using VNS.Libs.AppUI;
using System.IO;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_themmoi_PhieutrathuocKholeVeKhoChan : Form
    {
        private DataTable m_dtKhoNhap, m_dtKhoXuat = new DataTable();
        private int statusHethan = 1;
        public DataTable p_mDataPhieuNhapKho = new DataTable();
        private DataTable m_dtDataPhieuChiTiet = new DataTable();
        public action m_enAction = action.Insert;
        public bool b_Cancel = false;
        public string PerForm;
        public Janus.Windows.GridEX.GridEX grdList;
        public string KIEU_THUOC_VT = "THUOC";
        private DataTable m_PhieuDuTru = new DataTable();
        DataTable m_dtDmucThuoc = new DataTable();
        string SplitterPath = "";
        public frm_themmoi_PhieutrathuocKholeVeKhoChan()
        {
            InitializeComponent();
            this.Shown += frm_themmoi_PhieutrathuocKholeVeKhoChan_Shown;
            this.FormClosing += frm_themmoi_PhieutrathuocKholeVeKhoChan_FormClosing;
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            dtNgayNhap.Value = globalVariables.SysDate;
            Initevents();
            
        }

        void frm_themmoi_PhieutrathuocKholeVeKhoChan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString() });
        }

        void frm_themmoi_PhieutrathuocKholeVeKhoChan_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }
        void Initevents()
        {
            cmdExit.Click += new EventHandler(cmdExit_Click);
            grdKhoXuat.KeyDown += new KeyEventHandler(grdKhoXuat_KeyDown);
            this.KeyDown += new KeyEventHandler(frm_themmoi_PhieutrathuocKholeVeKhoChan_KeyDown);
            txtLyDoXuat._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtLyDoXuat__OnShowData);
            txtNguoinhan._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNguoinhan__OnShowData);
            txtNguoigiao._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNguoigiao__OnShowData);
            txtthuoc._OnEnterMe += new UCs.AutoCompleteTextbox_Thuoc.OnEnterMe(txtthuoc__OnEnterMe);
            txtthuoc._OnSelectionChanged += new UCs.AutoCompleteTextbox_Thuoc.OnSelectionChanged(txtthuoc__OnSelectionChanged);
            cmdAddDetail.Click += new EventHandler(cmdAddDetail_Click);
            txtKhoXuat._OnEnterMe += txtKhoXuat__OnEnterMe;
            cmdSendAll.Click += cmdSendAll_Click;
            txtthuoc._OnGridSelectionChanged += txtthuoc__OnGridSelectionChanged;
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
        void txtthuoc__OnGridSelectionChanged(string ID, int id_thuockho, string _name, string Dongia, string phuthu, int tutuc)
        {
            txtthuoc.MyID = ID;
        }

        void cmdSendAll_Click(object sender, EventArgs e)
        {
            SendAllData();
        }

        void txtKhoXuat__OnEnterMe()
        {

            Laythuoctrongkhoxuat();
        }
        void Laythuoctrongkhoxuat()
        {
            string _rowFilter = "1=1";
            try
            {
                if (Utility.Int32Dbnull(txtKhoXuat.MyID, -1) > 0)
                {
                    _rowFilter = string.Format("{0}<>{1}", TDmucKho.Columns.IdKho,
                                               Utility.Int32Dbnull(txtKhoXuat.MyID));
                }
            }
            catch (Exception)
            {
                _rowFilter = "1=1";
            }
            m_dtKhoNhap.DefaultView.RowFilter = _rowFilter;
            m_dtKhoNhap.AcceptChanges();
            getThuocTrongKho();
            AutocompleteThuoc();
            ModifyCommand();
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
                        Utility.SetMsg(lblMsg, string.Format("Số lượng chuyển {0} phải <= Số lượng có {1}", txtSoluongdutru.Text, slkhachuyen.ToString()), true);
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
            Utility.SetMsg(lblMsg, "", true);
            if (txtthuoc.MyID == txtthuoc.DefaultID)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn "+(ten_kieuthuoc_vt)  +" chuyển", true);
                txtthuoc.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSoluongdutru.Text, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập số lượng chuyển", true);
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
                Utility.SetMsg(lblMsg, cmdAddDetail.Enabled ? "" : "Chú ý: "+(ten_kieuthuoc_vt) +" bạn chọn không có trong " + txtKhoXuat.Text + " Đề nghị chọn lại "+ten_kieuthuoc_vt +"!", false);
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
            finally
            {
                Utility.SetMsg(lblMsg, cmdAddDetail.Enabled ? "" : "Chú ý: " + (ten_kieuthuoc_vt) + " bạn chọn không có trong " + txtKhoXuat.Text + " Đề nghị chọn lại " + ten_kieuthuoc_vt + "!", false);
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
       
        void frm_themmoi_PhieutrathuocKholeVeKhoChan_KeyDown(object sender, KeyEventArgs e)
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
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                SendData();
                return;
            }
            if (e.Control && e.KeyCode == Keys.Delete)
            {
                RemoveDetails();
                return;
            }
            if (e.Control && e.KeyCode == Keys.A)
            {
                SendAllData();
                return;
            }
        }
        void SendAllData()
        {
            try
            {
                if (!globalVariables.IsAdmin || m_enAction != action.Insert || !Utility.isValidGrid(grdKhoXuat)) return;
                foreach (DataRow dr in m_dtDataThuocKho.Rows)
                {
                    dr["SO_LUONG_CHUYEN"] = Utility.Int32Dbnull(dr["SO_LUONG"], 0);
                }
                grdKhoXuat.CheckAllRecords();
                SendData();
            }
            catch (Exception ex)
            {
            }
        }
        void SendData()
        {
            try
            {
                grdKhoXuat.Focus();
                cmdNext_Click(cmdNext, new EventArgs());
            }
            catch (Exception ex)
            {
            }
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
            //Janus.Windows.GridEX.GridEXColumn gridExColumn = grdKhoXuat.RootTable.Columns["SO_LUONG_CHUYEN"];
            //if (e.Control && e.KeyCode == Keys.Enter )//&& Utility.Int32Dbnull( grdKhoXuat.GetValue( gridExColumn.Key),0)>0 &&  grdKhoXuat.CurrentColumn.Position == gridExColumn.Position)
            // {
            //     txtFilterName.Focus();
            //     grdKhoXuat.Focus();
            //     cmdNext_Click(cmdNext, new EventArgs());
            //     txtFilterName.Clear();
            //     txtFilterName.Focus();
            // }
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
                cmdNext.Enabled = m_dtDataThuocKho != null && m_dtDataThuocKho.Rows.Count > 0 && grdKhoXuat.GetDataRows().Length>0;
                cmdSendAll.Enabled = cmdNext.Enabled && m_enAction == action.Insert; 
                txtKhoXuat.Enabled = grdPhieuXuatChiTiet.RowCount <= 0;
                cmdPrevius.Enabled = txtKhoXuat.MyID != "-1" && txtKhonhan.MyID != "-1" && grdPhieuXuatChiTiet.GetDataRows().Length > 0;

            }
            catch (Exception exception)
            {

            }
            finally
            {
                if (m_enAction == action.View)
                {
                    cmdPrevius.Enabled = cmdNext.Enabled = cmdSave.Enabled = cmdAddDetail.Enabled = false;
                }
            }
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
                        VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuXuatkho_2lien(IDPhieuNhap, "PHIẾU XUẤT", globalVariables.SysDate);
                    else
                        VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuXuatkho(IDPhieuNhap, "PHIẾU XUẤT", globalVariables.SysDate);
                }
            }
           
        }
        private void InitData()
        {
            txtNhanvien.Init(CommonLoadDuoc.LAYTHONGTIN_NHANVIEN(),
                            new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            if (globalVariables.gv_intIDNhanvien <= 0)
            {
                txtNhanvien.SetId(-1);
            }
            else
            {
                txtNhanvien.SetId(globalVariables.gv_intIDNhanvien);
            }
            LoadKho();
        }

        private void LoadKho()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoNhap = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU,NGOAITRU", "THUOC,THUOCVT", "CHANLE,CHAN", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN();
                m_dtKhoXuat = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU,NGOAITRU", "THUOC,THUOCVT", "CHANLE,LE", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE();
            }
            else
            {
                m_dtKhoNhap = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU,NGOAITRU", "VT,THUOCVT", "CHANLE,CHAN", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
                m_dtKhoXuat = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NOITRU,NGOAITRU", "VT,THUOCVT", "CHANLE,LE", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU", "NOITRU" });
            }
            txtKhoXuat.Init(m_dtKhoXuat, new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });
            txtKhonhan.Init(m_dtKhoNhap, new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });
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
                    txtKhonhan.SetId(objPhieuNhap.IdKhonhap);
                    txtKhoXuat.SetId(objPhieuNhap.IdKhoxuat);
                    Laythuoctrongkhoxuat();
                    txtNhanvien.SetId(objPhieuNhap.IdNhanvien);
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
       
        string ten_kieuthuoc_vt = "Thuốc";
        private void frm_themmoi_PhieutrathuocKholeVeKhoChan_Load(object sender, EventArgs e)
        {
            ten_kieuthuoc_vt = KIEU_THUOC_VT == "VT" ? "Vật tư" : "Thuốc";
            txtLyDoXuat.Init();
            txtNguoinhan.Init();
            txtNguoigiao.Init();
            AutocompleteThuoc();
            InitData();
            getData();
            ModifyCommand();
        }
        private void AutocompleteThuoc()
        {

            try
            {
                m_dtDmucThuoc = SPs.ThuocLaythuoctrongkhoxuatAutocomplete(Utility.Int32Dbnull(txtKhoXuat.MyID, -1), KIEU_THUOC_VT).GetDataSet().Tables[0];// new Select().From(DmucThuoc.Schema).Where(DmucThuoc.KieuThuocvattuColumn).IsEqualTo(KIEU_THUOC_VT).And(DmucThuoc.TrangThaiColumn).IsEqualTo(1).ExecuteDataSet().Tables[0];
                if (m_dtDmucThuoc == null)
                {
                    txtthuoc.dtData = null;
                    return;
                }
                txtthuoc.dtData = m_dtDmucThuoc;
                txtthuoc.ChangeDataSource();
            }
            catch
            {
            }
        }
       
        private DataTable m_dtDataThuocKho=new DataTable();
        private void getThuocTrongKho()
        {
            try
            {
                if (Utility.Int32Dbnull(txtKhoXuat.MyID) <= 0)
                {
                    m_dtDataThuocKho.Clear();
                }
                else
                {
                    m_dtDataThuocKho =
                SPs.ThuocLaythuoctrongkhoxuat(Utility.Int32Dbnull(txtKhoXuat.MyID),
                                                statusHethan, KIEU_THUOC_VT).GetDataSet().Tables[0];

                    m_dtDataThuocKho.AcceptChanges();
                    Utility.SetDataSourceForDataGridEx_Basic(grdKhoXuat, m_dtDataThuocKho, true, true, "So_luong>0", "");
                }
                
            }
            catch (Exception)
            {

                ModifyCommand();
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
            objTPhieuNhapxuatthuoc.IdKhonhap = Utility.Int16Dbnull(txtKhonhan.MyID, -1);
            objTPhieuNhapxuatthuoc.IdKhoxuat = Utility.Int16Dbnull(txtKhoXuat.MyID, -1);
            objTPhieuNhapxuatthuoc.MaNhacungcap = "";
            objTPhieuNhapxuatthuoc.MotaThem = txtLyDoXuat.Text;
            objTPhieuNhapxuatthuoc.TrangThai = 0;
            objTPhieuNhapxuatthuoc.KieuThuocvattu = KIEU_THUOC_VT;
            objTPhieuNhapxuatthuoc.IdNhanvien = Utility.Int16Dbnull(txtNhanvien.MyID, -1);
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
            objTPhieuNhapxuatthuoc.LoaiPhieu = (byte)LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan;
            objTPhieuNhapxuatthuoc.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan);
            objTPhieuNhapxuatthuoc.DuTru = 0;
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
                    newItem.DonGia = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.DonGia]);
                    newItem.GiaBan = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaBan]);
                    newItem.GiaNhap = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap]);
                  
                    newItem.SoLo = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoLo], "");
                    newItem.SoDky = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoDky], "");
                    newItem.SoQdinhthau = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau], "");
                    newItem.SoLuong = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoLuong], 0);
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
                    newItem.SluongChia = 1;
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
        /// hàm thực hiện việc thêm phiếu nhập kho "+ten_kieuthuoc_vt +"
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
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(txtKhonhan.MyID, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(txtKhoXuat.MyID, -1));
                    if (objKho != null)
                        newDr["ten_khoxuat"] = Utility.sDbnull(objKho.TenKho);
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
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(txtKhonhan.MyID, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(txtKhoXuat.MyID, -1));
                    if (objKho != null)
                        newDr["ten_khoxuat"] = Utility.sDbnull(objKho.TenKho);
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
        /// hàm thực hiện việc Invalinhap khoa "+ten_kieuthuoc_vt +"
        /// </summary>
        /// <returns></returns>
        private bool InValiNhapKho()
        {
            errorProvider1.Clear();
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
            if (txtNguoigiao.myCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn người giao", true);
                errorProvider1.SetError(txtNguoigiao, lblMsg.Text);
                txtNguoigiao.Focus();
                return false;
            }
            if (txtNguoinhan.myCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn người nhận", true);
                errorProvider1.SetError(txtNguoinhan, lblMsg.Text);
                txtNguoinhan.Focus();
                return false;
            }
            if (Utility.DoTrim( txtLyDoXuat.Text)=="")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn lý do trả "+ten_kieuthuoc_vt, true);
                errorProvider1.SetError(txtLyDoXuat, lblMsg.Text);
                txtLyDoXuat.Focus();
                return false;
            }
            if (txtKhoXuat.MyID.ToString()=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn kho trả", true);
                errorProvider1.SetError(txtKhoXuat, lblMsg.Text);
                txtKhoXuat.Focus();
                return false;
            }

            if (txtKhonhan.MyID.ToString() == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn kho nhận "+(ten_kieuthuoc_vt) +"", true);
                errorProvider1.SetError(txtKhonhan, lblMsg.Text);
                txtKhonhan.Focus();
                return false;
            }
            if (txtKhonhan.MyID.ToString() == txtKhoXuat.MyID.ToString())
            {
                Utility.SetMsg(lblMsg, "Kho nhận và kho trả phải khác nhau", true);
                errorProvider1.SetError(txtKhoXuat, lblMsg.Text);
                txtKhoXuat.Focus();
                return false;
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
                    Utility.ShowMsg("Số lượng "+(ten_kieuthuoc_vt) +" cần chuyển phải >=0","Thông báo",MessageBoxIcon.Warning);
                    e.Cancel = true;
                }else
                {
                    if(soluongchuyen>soluongthat)
                    {
                        Utility.ShowMsg("Số lượng "+(ten_kieuthuoc_vt) +" cần chuyển phải <= số lượng "+(ten_kieuthuoc_vt) +" có trong kho", "Thông báo", MessageBoxIcon.Warning);
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
                UIAction._Visible(prg1, true);
                prg1.Value = 0;
                prg1.Maximum = grdKhoXuat.GetCheckedRows().Length;
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetCheckedRows())
                {
                    prg1.Value += 1 ;
                    AddDetail(gridExRow);
                }
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển " + ten_kieuthuoc_vt + ":\n" + ex.Message);
            }
            finally
            {
                UIAction._Visible(prg1, false);
                UpdateWhenChanged();
                ResetValueInGridEx();
                ModifyCommand();
                
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
                        DataRow[] _rowThuoc = m_dtDmucThuoc.Select(DmucThuoc.Columns.IdThuoc + "=" + id_thuoc);
                        if (_rowThuoc.Length>0)
                        {
                            drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.TenThuoc]);
                            drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.HamLuong]);
                            drv[DmucThuoc.Columns.MaHoatchat] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.MaHoatchat]);
                            drv[DmucThuoc.Columns.NuocSanxuat] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.NuocSanxuat]);
                            drv[DmucThuoc.Columns.HangSanxuat] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.HangSanxuat]);
                        }
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen] = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.PhuthuDungtuyen].Value, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen] = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.PhuthuTraituyen].Value, 0);

                        drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = vat;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt] = GiaBhyt;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap] = NgayNhap;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap] = dongia;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.DonGia] = dongia;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap] = manhacungcap;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLo] = solo;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoDky] = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoDky].Value);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau] = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoQdinhthau].Value);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdQdinh] = Utility.Int64Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdQdinh].Value);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho] = -1;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBan] = Giaban;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdChuyen] = IdThuockho;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = soluongchuyen;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = dongia * soluongchuyen;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau] = 0;
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
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển "+ten_kieuthuoc_vt +":\n" + ex.Message);
            }
        }
        private void RemoveDetails()
        {
            try
            {
                UIAction._Visible(prg1, true);
                prg1.Value = 0;
                prg1.Maximum = grdPhieuXuatChiTiet.GetCheckedRows().Length;
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuXuatChiTiet.GetCheckedRows())
                {
                    prg1.Value += 1;
                    RemoveDetail(gridExRow);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy chuyển "+ten_kieuthuoc_vt +":\n" + ex.Message);
            }
            finally
            {
                UIAction._Visible(prg1, false);
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
                    DataRow[] _rowThuoc = m_dtDmucThuoc.Select(DmucThuoc.Columns.IdThuoc + "=" + id_thuoc);
                    if (_rowThuoc.Length > 0)
                    {
                        drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.TenThuoc]);
                        drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.HamLuong]);
                        drv[DmucThuoc.Columns.MaHoatchat] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.MaHoatchat]);
                        drv[DmucThuoc.Columns.NuocSanxuat] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.NuocSanxuat]);
                        drv[DmucThuoc.Columns.HangSanxuat] = Utility.sDbnull(_rowThuoc[0][DmucThuoc.Columns.HangSanxuat]);
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
                Utility.ShowMsg("Lỗi khi hủy chuyển "+ten_kieuthuoc_vt +":\n" + ex.Message);
            }
        }
       
        #region unused
        private void ResetValueInGridEx()
        {
            grdKhoXuat.UnCheckAllRecords();
        }
        private void UpdateWhenChanged()
        {
            foreach (DataRow dr in m_dtDataThuocKho.Rows)
            {
                DataRow[] arrDr = m_dtDataPhieuChiTiet.Select("id_chuyen=" + Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho]));
                if (arrDr.Length>0)
                {
                    int soluong = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]);
                    int soluongthat = Utility.Int32Dbnull(dr["SO_LUONG_THAT"]);
                    int soluongao = Utility.Int32Dbnull(dr["SLuongAo"]);
                    dr["SO_LUONG"] = soluongthat  - soluongao;
                }
                dr["SO_LUONG_CHUYEN"] = 0;
            }
            m_dtDataThuocKho.AcceptChanges();
            //foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetDataRows())
            //{
            //    if (gridExRow.RowType == RowType.Record)
            //    {
            //        var query = from thuoc in grdPhieuXuatChiTiet.GetDataRows()
            //                    let sl = Utility.Int32Dbnull(thuoc.Cells["SO_LUONG"].Value)
            //                    let idchuyen = Utility.Int32Dbnull(thuoc.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdChuyen].Value)
            //                    where idchuyen == Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho].Value)
            //                    select sl;
            //        if (query.Any())
            //        {
            //            int soluong = Utility.Int32Dbnull(query.FirstOrDefault());
            //            int soluongthat = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_THAT"].Value);
            //            int soluongao = Utility.Int32Dbnull(gridExRow.Cells["SLuongAo"].Value);
            //            gridExRow.BeginEdit();
            //            gridExRow.Cells["SO_LUONG"].Value = soluongthat - soluong - soluongao;
            //            gridExRow.Cells["SO_LUONG_CHUYEN"].Value = 0;
            //            gridExRow.EndEdit();
            //            grdKhoXuat.UpdateData();
            //            m_dtDataThuocKho.AcceptChanges();
            //        }
            //    }
            //}
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
            if (Utility.Int32Dbnull( txtKhonhan.MyID,-1)<=0)
            {
                Utility.ShowMsg("Bạn cần chọn kho nhận trước khi thực hiện tính năng tạo phiếu dự trù(bổ sung) cho kho nhận");
                txtKhonhan.Focus();
                return;
            }
            if (m_dtDataPhieuChiTiet.Rows.Count > 0)
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn tạo nhanh phiếu dự trù cho kho " + txtKhonhan.Text + "\nChú ý: Nếu đồng ý tạo nhanh thì toàn bộ chi tiết đang chuyển sang kho " + txtKhonhan.Text + " sẽ được xóa đi.","Xác nhận", true))
                    return;
            m_dtDataPhieuChiTiet.Clear();
            m_PhieuDuTru = SPs.ThuocLapphieudutru(Utility.Int32Dbnull(txtKhonhan.MyID, -1), KIEU_THUOC_VT).GetDataSet().Tables[0];
            Dictionary<int, string> lstNotExists = new Dictionary<int, string>();
            if (m_PhieuDuTru.Rows.Count <= 0)
            {
                Utility.ShowMsg("Chưa có dữ liệu dự trù. Bạn chỉ có thể sử dụng tính năng này sau khi đã lập dự trù cho "+ten_kieuthuoc_vt +"-Vật tư tiêu hao");
                return;
            }
            foreach (DataRow drDuTru in m_PhieuDuTru.Rows)
            {
                int ID_THUOC=Utility.Int32Dbnull(drDuTru[DmucThuoc.Columns.IdThuoc]);
                string TEN_THUOC = Utility.sDbnull(drDuTru[DmucThuoc.Columns.TenThuoc], "Unknown");
                int soluongthuoc_canchuyen = Utility.Int32Dbnull(drDuTru["SO_LUONG_CHUYEN"]);
                if (soluongthuoc_canchuyen > 0)//Chỉ lấy các "+ten_kieuthuoc_vt +" có lượng cần chuyển >0
                {
                    int soluong_chuyen = 0;
                    //Lấy dữ liệu kho xuất "+ten_kieuthuoc_vt +" dự trù
                    DataRow[] arrDR = m_dtDataThuocKho.Select(DmucThuoc.Columns.IdThuoc+"=" + ID_THUOC,"NGAY_HETHAN ASC");
                    if (arrDR.Length > 0)//Nếu có "+ten_kieuthuoc_vt +" này từ kho xuất
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
                                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho] = -1;
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
                Utility.ShowMsg("Hệ thống không tự động chuyển một số "+ten_kieuthuoc_vt +"(Vật tư) vì lý do sau đây:\n" + msg);
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
