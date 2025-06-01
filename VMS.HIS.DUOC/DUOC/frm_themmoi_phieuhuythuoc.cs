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
    public partial class frm_themmoi_phieuhuythuoc : Form
    {
        private DataTable  m_dtKhohuy = new DataTable();
        private int statusHethan = 1;
        public DataTable p_mDataPhieuNhapKho = new DataTable();
        private DataTable m_dtDataPhieuChiTiet = new DataTable();
        public action m_enAction = action.Insert;
        public bool b_Cancel = false;
        public string PerForm;
        public Janus.Windows.GridEX.GridEX grdList;
        public string KIEU_THUOC_VT = "THUOC";
        private DataTable m_PhieuDuTru = new DataTable();
        public bool AllowedChanged = true;
        string SplitterPath = "";
        public frm_themmoi_phieuhuythuoc()
        {
            InitializeComponent();
            this.Shown += frm_themmoi_phieuhuythuoc_Shown;
            this.FormClosing += frm_themmoi_phieuhuythuoc_FormClosing;
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            InitEvents();
            txtthuoc._OnGridSelectionChanged += txtthuoc__OnGridSelectionChanged;
            dtNgayNhap.Value = globalVariables.SysDate;
            
        }

        void frm_themmoi_phieuhuythuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString() });
        }

        void frm_themmoi_phieuhuythuoc_Shown(object sender, EventArgs e)
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
        void txtthuoc__OnGridSelectionChanged(string ID, int id_thuockho, string _name, string Dongia, string phuthu, int tutuc)
        {
            txtthuoc.MyID = ID;
        }
        void InitEvents()
        {
            txtthuoc._OnEnterMe += new UCs.AutoCompleteTextbox_Thuoc.OnEnterMe(txtthuoc__OnEnterMe);
            txtthuoc._OnSelectionChanged += new UCs.AutoCompleteTextbox_Thuoc.OnSelectionChanged(txtthuoc__OnSelectionChanged);
            txtMotathem.KeyDown += new KeyEventHandler(txtMotathem_KeyDown);
            this.grdKhoXuat.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(this.grdKhoXuat_UpdatingCell);
            this.cmdPrevius.Click += new System.EventHandler(this.cmdPrevius_Click);
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            this.cmdPrint.Click += new System.EventHandler(this.cmdInPhieuNhap_Click);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            this.Load += new System.EventHandler(this.frm_themmoi_phieuhuythuoc_Load);
            cmdExit.Click += new EventHandler(cmdExit_Click);
            grdKhoXuat.KeyDown += new KeyEventHandler(grdKhoXuat_KeyDown);
            grdKhoXuat.SelectionChanged += new EventHandler(grdKhoXuat_SelectionChanged);
            this.KeyDown += new KeyEventHandler(frm_themmoi_phieuhuythuoc_KeyDown);
            cboKhohuy.SelectedIndexChanged+=new EventHandler(cboKhohuy_SelectedIndexChanged);
            txtLydohuy._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtLydohuy__OnShowData);
            cmdAddDetail.Click += new EventHandler(cmdAddDetail_Click);
            txtLydohuy.KeyDown += new KeyEventHandler(txtLydohuy_KeyDown);
        }

        void grdKhoXuat_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowedChanged) return;
            if (!Utility.isValidGrid(grdList)) return;
        }

        void txtLydohuy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSoluongdutru.Clear();
                txtLydohuy.ClearText();
                txtthuoc.ClearText();
                txtthuoc.Focus();
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
                    AddDetailNext(grdKhoXuat.CurrentRow);
                    grdKhoXuat.CurrentRow.EndEdit();
                    UpdateWhenChanged();
                    //-------------------
                    txtSoluongdutru.Clear();
                    txtLydohuy.ClearText();
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
            errorProvider1.Clear();
            if (txtthuoc.MyID==txtthuoc.DefaultID)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn thuốc hủy",true);
                errorProvider1.SetError(txtthuoc, lblMsg.Text);
                txtthuoc.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSoluongdutru.Text,-1)<=0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập số lượng hủy",true);
                errorProvider1.SetError(txtSoluongdutru, lblMsg.Text);
                txtSoluongdutru.Focus();
                return false;
            }
            if (Utility.DoTrim(txtMotathem.Text) == "" && Utility.DoTrim(txtLydohuy.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập lý do hủy)", true);
                errorProvider1.SetError(txtLydohuy, lblMsg.Text);
                txtLydohuy.Focus();
                return false;
            }
            return true;
        }
        void txtLydohuy__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydohuy.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydohuy.myCode;
                txtLydohuy.Init();
                txtLydohuy.SetCode(oldCode);
                txtLydohuy.Focus();
            }
        }
        void txtMotathem_KeyDown(object sender, KeyEventArgs e)
        {
           
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
        }

        void txtSoluongdutru_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && Utility.Int32Dbnull(txtthuoc.MyID, -1) > 0)
                {
                 
                }
            }
            catch
            {
            }
            finally
            {
                txtthuoc.ResetText();
                txtthuoc.Focus();
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

        void cmdLydohuy_Click(object sender, EventArgs e)
        {
           
        }

       
       
        void frm_themmoi_phieuhuythuoc_KeyDown(object sender, KeyEventArgs e)
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
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name == txtHoidong.Name)
                    return;
                else
                    SendKeys.Send("{TAB}");
            }
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
                 txtthuoc.Focus();
                 grdKhoXuat.Focus();
                 cmdNext_Click(cmdNext, new EventArgs());
                 txtthuoc.ResetText();
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
                cboKhohuy.Enabled = grdPhieuXuatChiTiet.RowCount <= 0;
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
                VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuHuythuoc(IDPhieuNhap, "BIÊN BẢN HỦY THUỐC", globalVariables.SysDate);
            }
           
        }
        private void InitalData()
        {
            DataBinding.BindDataCombobox(cboNhanVien, CommonLoadDuoc.LAYTHONGTIN_NHANVIEN(),
                                      DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "---Nhân viên---",false);
            cboNhanVien.Enabled = false;
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhohuy = CommonLoadDuoc.LAYDANHMUCKHO(-1, "ALL", "THUOC,THUOCVT", "ALL", 0, 100, 1); // CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA();
            }
            else
            {
                m_dtKhohuy = CommonLoadDuoc.LAYDANHMUCKHO(-1, "ALL", "VT,THUOCVT", "ALL", 0, 100, 1); // CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_TATCA();
            }
            cboNhanVien.SelectedValue = globalVariables.gv_intIDNhanvien;
            DataBinding.BindDataCombobox(cboKhohuy, m_dtKhohuy,
                                       TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Kho nhập---", false);
        }

        private void getData()
        {
            if (m_enAction == action.Update || m_enAction == action.View)
            {
                TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                if (objPhieuNhap != null)
                {
                    txtIDPhieuNhapKho.Text = objPhieuNhap.IdPhieu.ToString();
                    dtNgayNhap.Value = objPhieuNhap.NgayHoadon;
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    if (Utility.Int32Dbnull(objPhieuNhap.IdKhoxuat) > 0)
                        cboKhohuy.SelectedValue = Utility.sDbnull(objPhieuNhap.IdKhoxuat);
                    if (Utility.Int32Dbnull(objPhieuNhap.IdNhanvien) > 0)
                        cboNhanVien.SelectedValue = Utility.sDbnull(objPhieuNhap.IdNhanvien);
                    txtMotathem.Text = objPhieuNhap.MotaThem;
                    txtHoidong.Rtf = objPhieuNhap.HoidongThanhly;
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
       
        private void frm_themmoi_phieuhuythuoc_Load(object sender, EventArgs e)
        {
            txtLydohuy.Init();
            InitalData();
            getData();
            AutocompleteThuoc();
            ModifyCommand();
        }
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = SPs.ThuocLaythuoctrongkhoxuatAutocomplete(Utility.Int32Dbnull(cboKhohuy.SelectedValue, -1), KIEU_THUOC_VT).GetDataSet().Tables[0];// new Select().From(DmucThuoc.Schema).Where(DmucThuoc.KieuThuocvattuColumn).IsEqualTo(KIEU_THUOC_VT).And(DmucThuoc.TrangThaiColumn).IsEqualTo(1).ExecuteDataSet().Tables[0];
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
        private void cboKhohuy_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                    SPs.ThuocLaythuoctrongkhoxuat(Utility.Int32Dbnull(cboKhohuy.SelectedValue),
                                                    -1, KIEU_THUOC_VT).GetDataSet().Tables[0];
                Utility.AddColumToDataTable(ref  m_dtDataThuocKho,"ShortName",typeof(string));
                foreach (DataRow drv in m_dtDataThuocKho.Rows)
                {
                    drv["ShortName"] = Utility.UnSignedCharacter(Utility.sDbnull(drv["Ten_Thuoc"]));
                }
                m_dtDataThuocKho.AcceptChanges();
                Utility.SetDataSourceForDataGridEx_Basic(grdKhoXuat, m_dtDataThuocKho, true, true, "So_luong>0", "");
                
            }
            catch (Exception)
            {
                
            }
        }


        /// <summary>
        /// HÀM HỰC HIỆN VIỆC LƯU LẠI THÔNG TIN 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
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
            objTPhieuNhapxuatthuoc.IdKhoxuat = Utility.Int16Dbnull(cboKhohuy.SelectedValue, -1);
            objTPhieuNhapxuatthuoc.MaNhacungcap = "";
            objTPhieuNhapxuatthuoc.MotaThem =Utility.DoTrim( txtMotathem.Text);
            objTPhieuNhapxuatthuoc.TrangThai = 0;
            objTPhieuNhapxuatthuoc.KieuThuocvattu = KIEU_THUOC_VT;
            objTPhieuNhapxuatthuoc.IdNhanvien = Utility.Int16Dbnull(cboNhanVien.SelectedValue, -1);
            if (Utility.Int32Dbnull(objTPhieuNhapxuatthuoc.IdNhanvien, -1) <= 0)
                objTPhieuNhapxuatthuoc.IdNhanvien = globalVariables.gv_intIDNhanvien;
            objTPhieuNhapxuatthuoc.NgayHoadon = dtNgayNhap.Value;
            objTPhieuNhapxuatthuoc.NgayTao = globalVariables.SysDate;
            objTPhieuNhapxuatthuoc.NguoiTao = globalVariables.UserName;
            objTPhieuNhapxuatthuoc.NguoiGiao = globalVariables.UserName;
            objTPhieuNhapxuatthuoc.HoidongThanhly = txtHoidong.Rtf;
            objTPhieuNhapxuatthuoc.LoaiPhieu =(byte) LoaiPhieu.PhieuHuy;
            objTPhieuNhapxuatthuoc.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuHuy);
            objTPhieuNhapxuatthuoc.DuTru = Utility.Bool2byte(false);
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
                    newItem.ThanhTien =
                        Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaBan]) *
                        Utility.Int32Dbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.SoLuong]);

                    newItem.Vat = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.Vat], 0);
                    newItem.MotaThem = Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.MotaThem]) == "" ? Utility.DoTrim(txtMotathem.Text) : Utility.sDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.MotaThem]);
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
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhohuy.SelectedValue, -1));
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
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhohuy.SelectedValue, -1));
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
        /// hàm thực hiện việc Invalinhap khoa thuốc
        /// </summary>
        /// <returns></returns>
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            errorProvider1.Clear();
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
            if (cboKhohuy.SelectedValue.ToString()=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn kho hủy", true);
                errorProvider1.SetError(cboKhohuy, lblMsg.Text);
                cboKhohuy.Focus();
                return false;
            }
            if (Utility.DoTrim( txtHoidong.Text)=="")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập nội dung biên bản",true);
                errorProvider1.SetError(txtHoidong, lblMsg.Text);
                txtHoidong.Focus();
                return false;
            }
            if (Utility.DoTrim(txtMotathem.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập ghi chú(lý do hủy chung)", true);
                errorProvider1.SetError(txtMotathem, lblMsg.Text);
                txtMotathem.Focus();
                return false;
            }
            return true;
        
        }

       

        private void cmdGetData_Click(object sender, EventArgs e)
        {
            getThuocTrongKho();
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
                    Utility.ShowMsg("Số lượng thuốc cần hủy phải >=0","Thông báo",MessageBoxIcon.Warning);
                    e.Cancel = true;
                }else
                {
                    if(soluongchuyen>soluongthat)
                    {
                        Utility.ShowMsg("Số lượng thuốc cần hủy phải <= số lượng thuốc có trong kho", "Thông báo", MessageBoxIcon.Warning);
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
            AddDetailNext();
        }

        private void AddDetailNext()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetDataRows())
                {
                    AddDetailNext(gridExRow);
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
        private void AddDetailNext(GridEXRow gridExRow)
        {
            try
            {
                string manhacungcap = "";
                string NgayHethan = "";
                string solo = "";
                int id_thuoc = -1;
                decimal dongia = 0m;
                decimal Giaban = 0m;
                Int32 soluongchuyen = 0;
                decimal vat = 0m;
                int isHetHan = 0;
                long IdThuockho = 0;
                int soluongthat = 0;
                int tongsoluongchuyen = 0;
                tongsoluongchuyen = 0;
                DateTime NgayNhap = DateTime.Now;
                decimal GiaBhyt = 0m;
               
                soluongthat = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_THAT"].Value);
                soluongchuyen = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_CHUYEN"].Value, 0);
                if (soluongchuyen > 0)
                {
                    GiaBhyt = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBhyt].Value, 0);
                    NgayNhap = Convert.ToDateTime(gridExRow.Cells[TThuockho.Columns.NgayNhap].Value).Date;
                    NgayHethan = Utility.sDbnull(gridExRow.Cells["NGAY_HET_HAN"].Value);
                    solo = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLo].Value);
                    id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuoc].Value, -1);
                   
                    dongia = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaNhap].Value, 0);
                    Giaban = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBan].Value, 0);

                    vat = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.Vat].Value, 0);
                    isHetHan = Utility.Int32Dbnull(gridExRow.Cells["IsHetHan"].Value, 0);
                    manhacungcap = Utility.sDbnull(gridExRow.Cells[TThuockho.Columns.MaNhacungcap].Value, 0);
                    IdThuockho = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuockho].Value, -1);
                    DataRow[] arrDr = m_dtDataPhieuChiTiet.Select(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho + "=" + IdThuockho.ToString());
                    if (arrDr.Length <= 0)
                    {
                        DataRow drv = m_dtDataPhieuChiTiet.NewRow();
                        drv[TPhieuNhapxuatthuocChitiet.Columns.MotaThem] = String.Empty;

                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc] = id_thuoc;
                        if (m_dtDataPhieuChiTiet.Columns.Contains(DmucThuoc.Columns.MaThuoc) && gridExRow.GridEX.RootTable.Columns.Contains(DmucThuoc.Columns.MaThuoc)) drv[DmucThuoc.Columns.MaThuoc] = Utility.sDbnull(gridExRow.Cells[DmucThuoc.Columns.MaThuoc].Value);
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
                        drv[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap] = NgayNhap;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt] = GiaBhyt;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen] = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.PhuthuDungtuyen].Value, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen] = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.PhuthuTraituyen].Value, 0);
                        drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = vat;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.MotaThem] = txtLydohuy.Text;
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
                        drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = NgayHethan;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu] = -1;
                        tongsoluongchuyen = soluongchuyen;
                        m_dtDataPhieuChiTiet.Rows.Add(drv);
                    }
                    else
                    {
                       
                        arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + soluongchuyen;
                        arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = dongia * Utility.Int32Dbnull(arrDr[0]["SO_LUONG"], 0);
                        tongsoluongchuyen = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]);
                        m_dtDataPhieuChiTiet.AcceptChanges();
                       
                    }
                    //Update lại dữ liệu từ kho xuất
                    gridExRow.BeginEdit();
                    //gridExRow.Cells["SO_LUONG"].Value = soluongthat - tongsoluongchuyen;
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
                Utility.ShowMsg("Lỗi khi chuyển thuốc:\n" + ex.Message);
            }
        }
        private void RemoveDetails()
        {
            try
            {
               
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuXuatChiTiet.GetCheckedRows())
                {
                    RemoveDetails(gridExRow);
                }
                ModifyCommand();
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy chuyển thuốc:\n"+ex.Message);
            }
        }
        private void RemoveDetails(GridEXRow gridExRow)
        {
            string manhacungcap = "";
            string NgayHethan = "";
            string solo = "";
            int id_thuoc = -1;
            decimal dongia = 0m;
            decimal Giaban = 0m;
            Int32 soluong = 0;
            decimal vat = 0m;
            int isHetHan = 0;
            long IdThuockho = 0;
            string ten_thuoc = "";
            try
            {
                DateTime NgayNhap = Convert.ToDateTime(gridExRow.Cells[TThuockho.Columns.NgayNhap].Value).Date;
                decimal GiaBhyt = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBhyt].Value, 0);
                ten_thuoc = Utility.sDbnull(gridExRow.Cells["TEN_THUOC"].Value);
                NgayHethan = Utility.sDbnull(gridExRow.Cells["NGAY_HET_HAN"].Value);
                solo = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLo].Value);
                id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuoc].Value, -1);
                IdThuockho = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuockho].Value, -1);
                dongia = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaNhap].Value, 0);
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
                    drv[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap] = NgayNhap;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt] = GiaBhyt;

                    drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = vat;
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
                    drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = NgayHethan;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu] = -1;
                    m_dtDataThuocKho.Rows.Add(drv);

                }
                else
                {
                    arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + soluong;
                    arrDr[0]["SLuongAo"] = Utility.DecimaltoDbnull(arrDr[0]["SLuongAo"]) - soluong;
                    arrDr[0]["SO_LUONG_THAT"] = arrDr[0]["SO_LUONG"];
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
                Utility.CatchException(string.Format("Lỗi khi hủy hủy thuốc {0}:\n", ten_thuoc), ex);
            }
        }

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
        private  void UpdateWhenChanged()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetDataRows())
            {
                if(gridExRow.RowType==RowType.Record)
                {
                    var query = from thuoc in grdPhieuXuatChiTiet.GetDataRows()
                                let sl = Utility.Int32Dbnull(thuoc.Cells["SO_LUONG"].Value)
                                let IdThuockho = Utility.Int32Dbnull(thuoc.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho].Value)
                                where IdThuockho == Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho].Value)
                                select sl;
                    if(query.Any())
                    {
                        int soluong = Utility.Int32Dbnull(query.FirstOrDefault());
                        int soluongthat = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_THAT"].Value);
                        gridExRow.BeginEdit();
                        gridExRow.Cells["SO_LUONG"].Value = soluongthat - soluong;
                        gridExRow.EndEdit();
                        grdKhoXuat.UpdateData();
                        m_dtDataThuocKho.AcceptChanges();
                    }
                }
            }
        }

        private void cmdPrevius_Click(object sender, EventArgs e)
        {
            RemoveDetails();
        }

        private void cmdXoaThongTin_Click_1(object sender, EventArgs e)
        {

        }

      

        private void cboKhoNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }
    }
}
