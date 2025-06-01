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
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.Libs;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_PhieuDuTru : Form
    {
        private DataTable m_Thuoc = new DataTable();
        string KIEU_THUOC_VT = "THUOC";
        bool hasLoaded = false;
        public frm_PhieuDuTru(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            this.FormClosing += frm_PhieuDuTru_FormClosing;
            txtthuoc._OnEnterMe +=txtthuoc__OnEnterMe;
            txtSoluongdutru.KeyDown += new KeyEventHandler(txtSoluongdutru_KeyDown);
            txtthuoc._OnSelectionChanged +=txtthuoc__OnSelectionChanged;
            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
            this.KeyDown += new KeyEventHandler(frm_PhieuDuTru_KeyDown);
            mnuHuy.Click += new EventHandler(mnuHuy_Click);
            mnuHuyAll.Click += new EventHandler(mnuHuyAll_Click);
            chkUpdate.CheckedChanged += new EventHandler(chkUpdate_CheckedChanged);
            chkHienthithuoccoDutru.CheckedChanged += new EventHandler(chkHienthithuoccoDutru_CheckedChanged);
           
            grdList.UpdatingCell += grdList_UpdatingCell;
            this.KeyUp += _KeyUp;
        }

        void _KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record && e.Column.Key == "SO_LUONG")
                {
                    int IDTHUOC = Utility.Int32Dbnull(grdList.GetValue(DmucThuoc.Columns.IdThuoc), 0);
                    Int16 IDKHO = Utility.Int16Dbnull(cboKhonhan.SelectedValue);
                    int SOLUONG = Utility.Int32Dbnull(e.Value);
                    Update(IDKHO, IDTHUOC, SOLUONG);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:", ex.Message);
            }
        }

       

        void frm_PhieuDuTru_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void chkHienthithuoccoDutru_CheckedChanged(object sender, EventArgs e)
        {
            m_Thuoc.DefaultView.Sort = chkHienthithuoccoDutru.Checked ? "ten_thuoc,ten_donvitinh" : "ten_thuoc,ten_donvitinh"; //"COQUANHE desc,ten_thuoc,ten_donvitinh" : "ten_thuoc,ten_donvitinh"; 
        }

        void chkUpdate_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList))
            {
                mnuHuy.Enabled = false;

            }
            else
            {
                int SOLUONG = Utility.Int32Dbnull(grdList.GetValue("SO_LUONG"), 0);
                mnuHuy.Enabled = SOLUONG > 0;
            }
        }

        void mnuHuyAll_Click(object sender, EventArgs e)
        {
            Huydutru();

        }

        void mnuHuy_Click(object sender, EventArgs e)
        {
            Huydutru();
        }

        void frm_PhieuDuTru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (ActiveControl != null && ActiveControl.Name == grdList.Name)
                    SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) cboKho_SelectedIndexChanged(cboKhoxuat, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave_Click(cmdSave, new EventArgs());
            if (e.KeyCode == Keys.P && e.Control) cmdPrint.PerformClick();
        }

        void txtthuoc__OnSelectionChanged()
        {
            try
            {
                //int _idthuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
                //if (_idthuoc > 0)
                //{
                //    var q = from p in grdList.GetDataRows()
                //            where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                //            select p;
                //    if (q.Count() > 0)
                //    {
                //        grdList.MoveTo(q.First());
                //    }
                //}
                //else
                //{
                //    grdList.MoveFirst();
                //}
            }
            catch
            {
            }
        }
        void Update(short IDKHO, int IDTHUOC, int SOLUONG)
        {

           
            var q = from p in grdList.GetDataRows()
                    where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == IDTHUOC
                    select p;

            int SLUONG_TRONGKHO = q.Count() > 0 ? Utility.Int32Dbnull(q.FirstOrDefault().Cells["SLUONG_TRONGKHO"].Value, 0) : 0;

            int SLUONG_CANCHUYEN = SOLUONG - SLUONG_TRONGKHO;
            SLUONG_CANCHUYEN = SLUONG_CANCHUYEN <= 0 ? 0 : SLUONG_CANCHUYEN;

            DataRow[] dr = m_Thuoc.Select(DmucThuoc.Columns.IdThuoc + "=" + IDTHUOC.ToString());
            if (dr.Length > 0)
            {
                dr[0]["SO_LUONG"] = SOLUONG <= 0 ? 0 : SOLUONG;
                dr[0]["COQUANHE"] = SOLUONG <= 0 ? 0 : 1;
                grdList.SetValue("SLUONG_CANCHUYEN", SLUONG_CANCHUYEN);
                //m_Thuoc.AcceptChanges();
            }

            TDutruThuocCollection lst =
                new Select().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                    .And(TDutruThuoc.Columns.IdKhonhan).IsEqualTo(IDKHO)
                    .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT)
                    .ExecuteAsCollection<TDutruThuocCollection>();
            if (lst.Count > 0)
            {
                if (SOLUONG <= 0)
                {
                    //new Delete().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                    //.And(TDutruThuoc.Columns.IdKhonhan).IsEqualTo(IDKHO)
                    //.And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                }
                else
                {
                  int _num=  new Update(TDutruThuoc.Schema)
                        .Set("soluong_dutru").EqualTo(SOLUONG)
                        .Set(TDutruThuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        .Set(TDutruThuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                        .Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC).And(TDutruThuoc.Columns.IdKhonhan).
                        IsEqualTo(IDKHO)
                        .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                  if (_num > 0) Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật số lượng dự trù cho kho {0}, thuốc {1}, số lượng= {2} thành công ", IDKHO.ToString(), IDTHUOC, SOLUONG.ToString()), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }


            }
            else
            {
                TDutruThuoc objThongTin = new TDutruThuoc();
                objThongTin.IdThuoc = IDTHUOC;
                objThongTin.KieuThuocVt = KIEU_THUOC_VT;
                objThongTin.IdKhonhan = IDKHO;
                objThongTin.SoluongDutru = SOLUONG;
                objThongTin.NgayTao = DateTime.Now;
                objThongTin.NguoiTao = globalVariables.UserName;
                objThongTin.IsNew = true;
                objThongTin.Save();
            }
           

        }
        void txtSoluongdutru_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && Utility.Int32Dbnull(txtthuoc.MyID, -1) > 0)
                {
                    TDutruThuoc _new = new Select().From(TDutruThuoc.Schema)
                   .Where(TDutruThuoc.Columns.IdKhonhan).IsEqualTo(Utility.Int16Dbnull(cboKhonhan.SelectedValue, -1))
                   .And(TDutruThuoc.Columns.IdThuoc).IsEqualTo(Utility.Int32Dbnull(txtthuoc.MyID, -1))
                   .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).ExecuteSingle<TDutruThuoc>();
                    if (_new == null)
                    {
                        _new = new TDutruThuoc();
                        _new.IdKhonhan = Utility.Int16Dbnull(cboKhonhan.SelectedValue, -1);
                        _new.IdThuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
                        _new.SoluongDutru = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSoluongdutru.Text, 0));
                        _new.KieuThuocVt = KIEU_THUOC_VT;
                        _new.NgayTao = DateTime.Now;
                        _new.NguoiTao = globalVariables.UserName;
                        _new.IsNew = true;
                        _new.Save();
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới dự trù cho kho {0}, thuốc {1}, số lượng= {2} thành công ", _new.IdKhonhan.ToString(), _new.IdThuoc, _new.SoluongDutru.ToString()), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                        //
                        DataTable dtNew = SPs.ThuocLaythongtinDutruthuoc(Utility.Int16Dbnull(cboKhoxuat.SelectedValue), KIEU_THUOC_VT, Utility.Int16Dbnull(cboKhonhan.SelectedValue), _new.IdThuoc).GetDataSet().Tables[0];
                        if (dtNew.Rows.Count == 1)
                            if (m_Thuoc.Select("id_thuoc=" + _new.IdThuoc.ToString()).Length <= 0)
                            {
                                m_Thuoc.ImportRow(dtNew.Rows[0]);
                                //focus new row
                                Utility.GotoNewRowJanus(grdList, "id_thuoc", _new.IdThuoc.ToString());
                            }

                    }
                    else
                    {
                        int IDTHUOC = Utility.Int32Dbnull(txtthuoc.MyID, -1);
                        Int16 IDKHO = Utility.Int16Dbnull(cboKhonhan.SelectedValue);
                        int SOLUONG = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSoluongdutru.Text, 0));
                        Update(IDKHO, IDTHUOC, SOLUONG);
                        Utility.GotoNewRowJanus(grdList, "id_thuoc", IDTHUOC.ToString());
                    }
                    txtthuoc.ResetText();
                    txtthuoc.Focus();
                }
            }
            catch
            {
            }
            finally
            {
               
            }
        }

        void txtthuoc__OnEnterMe()
        {
            int _idthuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
            if (_idthuoc > 0)
            {
                if (m_Thuoc != null && m_Thuoc.Columns.Count > 0)
                {
                    DataRow[] dr = m_Thuoc.Select(DmucThuoc.Columns.IdThuoc + "=" + _idthuoc.ToString());
                    if (dr.Length > 0)
                    {
                        txtSoluongdutru.Text = Utility.sDbnull(dr[0]["SO_LUONG"], "0");
                    }
                }

                txtSoluongdutru.Focus();
                txtSoluongdutru.SelectAll();
            }
            else
            {
                txtSoluongdutru.Clear();
            }
        }
        private void LoadKho()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoXuat = CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", "THUOC,THUOCVT", "ALL", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NOITRU();
                m_dtKhoLinh = CommonLoadDuoc.LAYDANHMUCKHO(-1, "ALL", "THUOC,THUOCVT", "ALL", 100, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_TUTHUOC();
            }
            else
            {
                m_dtKhoXuat = CommonLoadDuoc.LAYDANHMUCKHO(-1, "ALL", "VT,THUOCVT", "ALL", 0, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
                m_dtKhoLinh = CommonLoadDuoc.LAYDANHMUCKHO(-1, "ALL", "VT,THUOCVT", "ALL", 100, 100, 1);//  CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NOITRU" });
            }
            DataBinding.BindDataCombobox(cboKhonhan, m_dtKhoLinh,
                                          TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                          "---Kho nhận---", true);
            DataBinding.BindDataCombobox(cboKhoxuat, m_dtKhoXuat,
                                       TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                       "---Kho xuất---", true);
            //DataBinding.BindDataCombobox(cboKhoalinh,
            //                                       THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)2,"ALL"),
            //                                       DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
            //                                       "---Khoa lĩnh---", false, false);

            DataBinding.BindDataCombobox(cboKhoalinh, THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)2, "ALL"),
                                  DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                  "---Khoa lập dự trù---", true);
        }
        private void Modifyconmand()
        {
            if (Utility.Int32Dbnull(grdList.GetValue("SO_LUONG"), 0) > 0)
            {
                cboKhoxuat.Enabled = false;
                cboKhoalinh.Enabled = false;
                cboKhonhan.Enabled = false;
            }
        }
        void LoadUserConfigs()
        {
            try
            {

                chkUpdate.Checked = Utility.getUserConfigValue(chkUpdate.Tag.ToString(), Utility.Bool2byte(chkUpdate.Checked)) == 1;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {

                Utility.SaveUserConfig(chkUpdate.Tag.ToString(), Utility.Bool2byte(chkUpdate.Checked));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void frm_PhieuDuTru_Load(object sender, EventArgs e)
        {
            LoadUserConfigs();
            AutocompleteThuoc();
            LoadKho();
            //DataBinding.BindDataCombobox(cboKho, this.KIEU_THUOC_VT.TrimStart().TrimEnd() == "THUOC" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOCLE_TUTRUC(this.KIEU_THUOC_VT.TrimStart().TrimEnd()) : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU", "NOITRU" }), TDmucKho.Columns.IdKho,
            //                     TDmucKho.Columns.TenKho, "Chọn kho", true);
            hasLoaded = true;
            //LoadThongTinThuoc();
        }
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = SPs.ThuocLaydanhmucthuoc(KIEU_THUOC_VT, -1).GetDataSet().Tables[0];// new Select().From(DmucThuoc.Schema).Where(DmucThuoc.KieuThuocvattuColumn).IsEqualTo(KIEU_THUOC_VT).And(DmucThuoc.TrangThaiColumn).IsEqualTo(1).ExecuteDataSet().Tables[0];
                txtthuoc.Init(_dataThuoc.Select("trang_thai=1").CopyToDataTable(), new List<string>() { "id_thuoc", "ma_thuoc", "ten_thuoc" });
            }
            catch
            {
            }
        }
        private void LoadThongTinThuoc()
        {
            if (!hasLoaded || Utility.Int16Dbnull(cboKhonhan.SelectedValue) <= 0)
            {
                m_Thuoc.Rows.Clear();
                return;
            }
            else
                m_Thuoc = SPs.ThuocLaythongtinDutruthuoc(Utility.Int16Dbnull(cboKhoxuat.SelectedValue), KIEU_THUOC_VT,Utility.Int16Dbnull(cboKhonhan.SelectedValue),-1).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_Thuoc, true, true, "1=1", "ten_thuoc,ten_donvitinh");//"COQUANHE desc,ten_thuoc,ten_donvitinh");
           // txtthuoc.Init(m_Thuoc, new List<string>() { "id_thuoc", "ma_thuoc", "ten_thuoc" });
        }

        private void cmdGetData_Click(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(cboKhoxuat.SelectedValue) > 0)
            {
                LoadThongTinThuoc();
            }
        }



        private void grdList_CellUpdated(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
           
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lưu lại toàn bộ thông tin dự trù thuốc/vật tư trong kho?", "Thông báo", true)) 
                    return;
                foreach (GridEXRow _row in grdList.GetDataRows())
                {
                    int IDTHUOC = Utility.Int32Dbnull(grdList.GetValue(DmucThuoc.Columns.IdThuoc), 0);
                    Int16 IDKHO = Utility.Int16Dbnull(cboKhoxuat.SelectedValue);
                    int SOLUONG = Utility.Int32Dbnull(grdList.GetValue("SO_LUONG"), 0);

                    if (SOLUONG <= 0)
                        new Delete().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                        .And(TDutruThuoc.Columns.IdKho).IsEqualTo(IDKHO)
                        .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                    else
                        new Update(TDutruThuoc.Schema)
                            .Set(TDutruThuoc.Columns.SoluongDutru).EqualTo(SOLUONG)
                            .Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC).And(TDutruThuoc.Columns.IdKho).
                            IsEqualTo(IDKHO)
                            .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:", ex.Message);
            }
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadThongTinThuoc();
        }

        private void cmdHuydutru_all_Click(object sender, EventArgs e)
        {
            Huydutru();
        }
        void Huydutru()
        {
            try
            {
                int count_checked = grdList.GetCheckedRows().Count();
                 Int16 IDKHO= Utility.Int16Dbnull(cboKhonhan.SelectedValue);
                 if (count_checked<=0  )
                {
                    if (Utility.isValidGrid(grdList))
                    {
                        GridEXRow _currentRow = grdList.CurrentRow;
                        if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy dự trù thuốc/vật tư {0} trong kho {1} hay không?", Utility.sDbnull(_currentRow.Cells[DmucThuoc.Columns.TenThuoc].Value, "không xác định"), cboKhoxuat.Text), "Cảnh báo", true))
                        {
                           
                            int IDTHUOC = 0;


                            IDTHUOC = Utility.Int32Dbnull(_currentRow.Cells[DmucThuoc.Columns.IdThuoc].Value, 0);
                            new Delete().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                          .And(TDutruThuoc.Columns.IdKhonhan).IsEqualTo(IDKHO)
                          .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                            _currentRow.BeginEdit();
                            _currentRow.Cells["COQUANHE"].Value = 0;
                            _currentRow.Cells["SO_LUONG"].Value = 0;
                            _currentRow.Cells["SLUONG_CANCHUYEN"].Value = 0;
                            _currentRow.EndEdit();

                            grdList.UpdateData();
                            m_Thuoc.AcceptChanges();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy dự trù cho các thuốc {0} trong kho {1} thành công ", IDTHUOC.ToString(), IDKHO.ToString()), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            Utility.ShowMsg(string.Format("Đã hủy dự trù thuốc/vật tư {0} trong kho {1} thành công!", Utility.sDbnull(_currentRow.Cells[DmucThuoc.Columns.TenThuoc].Value, "không xác định"), cboKhoxuat.Text));
                        }
                    }
                    else
                    {
                        Utility.ShowMsg("Bạn cần chọn ít nhất một thuốc có dự trù để có thể thực hiện thao tác hủy dự trù thuốc!");
                    }
                }
                else//Hủy toàn bộ
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy dự trù thuốc/vật tư đang được chọn hay không?"), "Cảnh báo", true))
                    {
                          int IDTHUOC = 0;
                          List<string> lstIdthuoc = new List<string>();
                        foreach(GridEXRow _row in grdList.GetCheckedRows())
                        {
                            IDTHUOC = Utility.Int32Dbnull(_row.Cells[DmucThuoc.Columns.IdThuoc].Value, 0);
                            lstIdthuoc.Add(IDTHUOC.ToString());
                              new Delete().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                            .And(TDutruThuoc.Columns.IdKhonhan).IsEqualTo(IDKHO)
                            .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                            _row.BeginEdit();
                            _row.Cells["COQUANHE"].Value= 0;
                            _row.Cells["SO_LUONG"].Value= 0;
                            _row.Cells["SLUONG_CANCHUYEN"].Value=0;
                            _row.EndEdit();
                        }
                        grdList.UpdateData();
                        m_Thuoc.AcceptChanges();
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy dự trù cho các thuốc {0} trong kho {1} thành công ",string.Join(",", lstIdthuoc.ToArray<string>()), IDKHO.ToString()), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        Utility.ShowMsg(string.Format("Đã hủy dự trù thuốc/vật tư đang được chọn thành công!"));
                    }
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:", ex.Message);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
               
                //Truyền dữ liệu vào datatable
                DataTable m_dtReport = BAOCAO_THUOC.ThuocLaythongtinInphieuDutruthuoc(Utility.Int16Dbnull(cboKhonhan.SelectedValue, -1), Utility.Int16Dbnull(cboKhoxuat.SelectedValue, -1), KIEU_THUOC_VT);
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_PhieuDutru.xml");
                
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                
                //Add logo vào datatable
                Utility.UpdateLogotoDatatable(ref m_dtReport);
                string tieude = "", reportname = "";
                string mabaocao =  "thuoc_PhieuDutru";
                var crpt = Utility.GetReport(mabaocao, ref tieude, ref reportname);
                if (crpt == null) return;

                //baocaO_TIEUDE1.TIEUDE
                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = System.IO.Path.GetFileName(reportname);
                objForm.mv_sReportCode = mabaocao;
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", "");
                Utility.SetParameterValue(crpt, "tenkho", cboKhoxuat.Text);


                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }
        private DataTable m_dtKhoXuat, m_dtKhoLinh, m_KhoaLinh = new DataTable();
        private void cboKhoalinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!hasLoaded) return;
            //    string IDKhoa = cboKhoalinh.SelectedValue.ToString();
            //    DataRow[] arrdr = m_dtKhoLinh.Select("ID_KHOAPHONG=" + IDKhoa + " or id_khoaphong_captren=" + IDKhoa);
            //    DataTable _newTable = m_dtKhoLinh.Clone();
            //    if (arrdr.Length > 0) _newTable = arrdr.CopyToDataTable();
            //    DataBinding.BindDataCombobox(cboKhonhan, _newTable,
            //                               TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "--Chọn tủ thuốc--", false);
            //    if (_newTable.Rows.Count ==1)
            //    {
            //        cboKhonhan.SelectedIndex = 0;
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Utility.ShowMsg("Lỗi:" + exception.Message);
            //}
            try
            {
                if (!hasLoaded) return;
                short IDKhoa = Utility.Int16Dbnull(cboKhoalinh.SelectedValue, -1);
                //DataRow[] arrdr = m_dtKhonhan.Select("ID_KHOAPHONG=" + IDKhoa + " or id_khoaphong_captren=" + IDKhoa);
                //DataTable _newTable = m_dtKhonhan.Clone();
                //if (arrdr.Length > 0) _newTable = arrdr.CopyToDataTable();
                DataTable _newTable = CommonLoadDuoc.LAYDANHMUCKHO(IDKhoa, "ALL", KIEU_THUOC_VT, "ALL", 100, 100, 1);// CommonLoadDuoc.LAYDANHMUCKHO_THEOKHOA(IDKhoa, IDKhoa, "ALL", KIEU_THUOC_VT, 100, 100);
                DataBinding.BindDataCombobox(cboKhonhan, _newTable,
                                         TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                         "---Chọn kho dự trù---", true);
                if (_newTable.Rows.Count == 2)
                {
                    cboKhonhan.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }

        private void cboKhonhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(Utility.Int16Dbnull(cboKhonhan.SelectedIndex)>=0)
            //{
            //    LoadThongTinThuoc();
            //    Modifyconmand();
            //}
            
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            //if (Utility.Int32Dbnull(cboKhoxuat.SelectedValue) <= 0)
            //{
            //    Utility.ShowMsg("Bạn cần chọn kho xuất");
            //    cboKhoxuat.Focus();
            //    return;
            //}
            if (Utility.Int32Dbnull(cboKhoalinh.SelectedValue) <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn khoa lập dự trù");
                cboKhoalinh.Focus();
                return;
            }
            if (Utility.Int32Dbnull(cboKhonhan.SelectedValue) <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn tủ thuốc hoặc kho lập dự trù");
                cboKhonhan.Focus();
                return;
            }
            //if (Utility.Int32Dbnull(cboKhoxuat.SelectedValue)==Utility.Int32Dbnull(cboKhonhan.SelectedValue))
            //{
            //    Utility.ShowMsg("Kho lập dự trù phải khác kho xuất thuốc");
            //    cboKhonhan.Focus();
            //    return;
            //}
            if (Utility.Int32Dbnull(cboKhonhan.SelectedValue) > 0)
            {
                LoadThongTinThuoc();
            }
            if (cmdSearch.Tag.ToString() == "0")//Bắt đầu thực hiện
            {
                cboKhoxuat.Enabled = false;
                cboKhoalinh.Enabled = false;
                cboKhonhan.Enabled = false;
                cmdSearch.Tag = "1";
                cmdSearch.Text = "Chọn lại";
            }
            else//Hủy chọn kho khác
            {
                cmdSearch.Text = "Lấy thông tin dự trù";
                cmdSearch.Tag = "0";
                cboKhoxuat.Enabled = true;
                cboKhoalinh.Enabled = true;
                cboKhonhan.Enabled = true;
            }

            
        }

        private void grdList_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }

        private void cmdPhieulinhbututruc_Click(object sender, EventArgs e)
        {
            LoadThongTinThuoc();
            if (m_Thuoc.Select("CHOXACNHAN>0").Count() > 0)
            {
                Utility.ShowMsg("Còn tồn tại các thuốc/vtth đã kê nhưng chưa được tổng hợp cấp phát hoặc được duyệt. Bạn cần tổng hợp hoặc duyệt hết các phiếu trước khi in phiếu lĩnh bù tủ trực");
                return;
            }
            try
            {

                //Truyền dữ liệu vào datatable
                DataTable m_dtReport = m_Thuoc.Clone();
                DataRow[] arrDr = m_Thuoc.Select("SLUONG_CANCHUYEN<0");
                if (arrDr.Length > 0) m_dtReport = arrDr.CopyToDataTable();
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_phieulinhbututruc.xml");

                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                //Add logo vào datatable
                Utility.UpdateLogotoDatatable(ref m_dtReport);
                string tieude = "", reportname = "";
                string mabaocao = "thuoc_phieulinhbututruc";
                var crpt = Utility.GetReport(mabaocao, ref tieude, ref reportname);
                if (crpt == null) return;

                //baocaO_TIEUDE1.TIEUDE
                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = System.IO.Path.GetFileName(reportname);
                objForm.mv_sReportCode = mabaocao;
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", "");
                Utility.SetParameterValue(crpt, "tenkho", cboKhoxuat.Text);


                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                
              
            }
        }
    }
}
