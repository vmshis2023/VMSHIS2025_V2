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
using System.Transactions;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_themmoi_dieutiet : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;
    
        public DataTable p_mDataPhieuNhapKho = new DataTable();
        private DataTable m_dtChitietdieutiet = new DataTable();
        public action m_enAction = action.Insert;
        public bool b_Cancel = false;
        public string PerForm;
        public Janus.Windows.GridEX.GridEX grdList;
        string KIEU_THUOC_VT = "THUOC";
        private DataTable m_PhieuDuTru = new DataTable();
        string SplitterPath = "";
        public frm_themmoi_dieutiet(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            this.Shown += frm_themmoi_dieutiet_Shown;
            this.FormClosing += frm_themmoi_dieutiet_FormClosing;
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            dtNgayNhap.Value = globalVariables.SysDate;
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            this.KeyDown += new KeyEventHandler(frm_themmoi_dieutiet_KeyDown);
            txtLyDoNhap._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtLyDoNhap__OnShowData);
            txtthuoc._OnEnterMe+=txtthuoc__OnEnterMe;
            txtthuoc._OnSelectionChanged +=txtthuoc__OnSelectionChanged;
            cmdAddDetail.Click += new EventHandler(cmdAddDetail_Click);
            grdChitietdieutiet.ColumnButtonClick += grdChitietdieutiet_ColumnButtonClick;
           
            grdChitietdieutiet.EditingCell += grdChitietdieutiet_EditingCell;
            grdChitietdieutiet.UpdatingCell += grdChitietdieutiet_UpdatingCell;
            grdThuocTrongthau.UpdatingCell += grdThuocTrongthau_UpdatingCell;
        }

        void grdThuocTrongthau_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "sl_chuyen")
                {
                    long id_thau_ct = Utility.Int64Dbnull(grdThuocTrongthau.GetValue("id_thau_ct"));
                    DataTable dtData = SPs.ThuocThauLaythongtinSoluong(id_thau_ct).GetDataSet().Tables[0];
                    int slkhachuyen = Utility.Int32Dbnull(dtData.Rows[0]["sl_khachuyen"]);
                    //Lấy số lượng chờ trong chính phiếu đang có
                    int slcho_trongphieu = 0;
                    slkhachuyen += slcho_trongphieu;
                    if (slkhachuyen < Utility.DecimaltoDbnull(e.Value, 0))
                    {
                        Utility.SetMsg(lblMsg, string.Format("Số lượng điều tiết {0} phải <= Số lượng khả chuyển {1}", e.Value, slkhachuyen.ToString()), true);
                        e.Value = e.InitialValue;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }  
        }

        void grdChitietdieutiet_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "So_Luong")
                {
                    long id_thau_ct = Utility.Int64Dbnull(grdThuocTrongthau.GetValue("id_thau_ct"));
                    DataTable dtData = SPs.ThuocThauLaythongtinSoluong(id_thau_ct).GetDataSet().Tables[0];
                    int slkhachuyen = Utility.Int32Dbnull(dtData.Rows[0]["sl_khachuyen"]);
                    //Lấy số lượng chờ trong chính phiếu đang có
                    int slcho_trongphieu = Utility.Int32Dbnull(e.InitialValue, 0);
                    slkhachuyen += slcho_trongphieu;
                    if (slkhachuyen < Utility.DecimaltoDbnull(e.Value, 0))
                    {
                        Utility.SetMsg(lblMsg, string.Format("Số lượng điều tiết {0} phải <= Số lượng khả chuyển {1}", txtSoluong.Text, slkhachuyen.ToString()), true);
                        e.Value = e.InitialValue;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void grdChitietdieutiet_EditingCell(object sender, EditingCellEventArgs e)
        {
           
        }

        void grdChitietdieutiet_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "XOA")
            {
                if (m_enAction != action.View)
                    Xoa();
            }
        }
        void Xoa()
        {
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các mặt hàng đang chọn?", "Xác nhận xóa", true))
            {
                objPhieudieutiet = TThauDieutiet.FetchByID(Utility.Int32Dbnull(txtId.Text));
                if (objPhieudieutiet != null && objPhieudieutiet.TrangThai == 1)
                {
                    Utility.ShowMsg("Phiếu điều tiết đã được xác nhận bởi người khác trong lúc bạn đang thao tác nên bạn không thể chỉnh sửa. Vui lòng kiểm tra lại");
                    return;
                }
                if (grdChitietdieutiet.GetCheckedRows().Count() <= 0)
                    grdChitietdieutiet.CurrentRow.BeginEdit();
                grdChitietdieutiet.CurrentRow.IsChecked = true;
                grdChitietdieutiet.CurrentRow.EndEdit();
                GridEXRow[] arrDr = grdChitietdieutiet.GetCheckedRows();
                foreach (GridEXRow _row in arrDr)
                {
                    long IdDieutietCt = Utility.Int64Dbnull(_row.Cells[TThauDieutietCt.Columns.IdDieutietCt].Value, 0);
                    int num = new Delete().From(TThauDieutietCt.Schema).Where(TThauDieutietCt.Columns.IdDieutietCt).IsEqualTo(IdDieutietCt).Execute();
                    if (IdDieutietCt <= 0) num = 1;//Xóa các dòng mới thêm chưa lưu
                    if (num > 0)
                    {
                        _row.Delete();
                        grdChitietdieutiet.UpdateData();
                        m_dtChitietdieutiet.AcceptChanges();
                    }
                }
            }
        }
        void frm_themmoi_dieutiet_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString() });
        }

        void frm_themmoi_dieutiet_Shown(object sender, EventArgs e)
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
        void cmdAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidDetailData()) return;
                if (Utility.isValidGrid(grdThuocTrongthau))
                {
                    long id_thau_ct = Utility.Int64Dbnull(grdThuocTrongthau.GetValue("id_thau_ct"));
                    DataTable dtData = SPs.ThuocThauLaythongtinSoluong(id_thau_ct).GetDataSet().Tables[0];
                    int slkhachuyen = Utility.Int32Dbnull(dtData.Rows[0]["sl_khachuyen"]);
                    //Lấy số lượng chờ trong chính phiếu đang có
                    int slcho_trongphieu = Utility.Int32Dbnull(m_dtChitietdieutiet.Compute("sum(so_luong)", TThauDieutietCt.Columns.IdThauCt + "=" + id_thau_ct.ToString()), 0);
                    slkhachuyen += slcho_trongphieu;
                    if (slkhachuyen<Utility.DecimaltoDbnull(txtSoluong.Text,0))
                    {
                        Utility.SetMsg(lblMsg, string.Format("Số lượng điều tiết {0} phải <= Số lượng khả chuyển {1}", txtSoluong.Text, slkhachuyen.ToString()), true);
                        txtSoluong.SelectAll();
                        txtSoluong.Focus();
                        return;
                    }
                    AddDetailNext(grdThuocTrongthau.CurrentRow);
                    //-------------------
                    txtSoluong.Clear();
                    txtthuoc.SetId(-1);
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
                Utility.SetMsg(lblMsg, "Bạn phải chọn thuốc điều tiết", true);
                txtthuoc.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSoluong.Text, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập số lượng điều tiết", true);
                txtSoluong.Focus();
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
                    var q = from p in grdThuocTrongthau.GetDataRows()
                            where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                            select p;
                    if (q.Count() > 0)
                    {
                        cmdAddDetail.Enabled = true;
                        grdThuocTrongthau.MoveTo(q.First());

                    }
                    else
                    {
                        cmdAddDetail.Enabled = false;
                    }
                    var q1 = from p in grdChitietdieutiet.GetDataRows()
                             where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                             select p;
                    if (q1.Count() > 0)
                    {
                        grdChitietdieutiet.MoveTo(q1.First());
                    }

                }
                else
                {
                    cmdAddDetail.Enabled = false;
                    grdThuocTrongthau.MoveFirst();
                }
            }
            catch
            {
            }
        }
        void txtthuoc__OnEnterMe()
        {
            try
            {
                int _idthuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
                if (_idthuoc > 0)
                {
                    var q = from p in grdThuocTrongthau.GetDataRows()
                            where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                            select p;
                    if (q.Count() > 0)
                    {
                        cmdAddDetail.Enabled = true;
                        grdThuocTrongthau.MoveTo(q.First());

                    }
                    else
                    {
                        cmdAddDetail.Enabled = false;
                    }
                    var q1 = from p in grdChitietdieutiet.GetDataRows()
                             where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                             select p;
                    if (q1.Count() > 0)
                    {
                        grdChitietdieutiet.MoveTo(q1.First());
                    }

                }
                else
                {
                    cmdAddDetail.Enabled = false;
                    grdThuocTrongthau.MoveFirst();
                }
            }
            catch
            {
            }
        }

        void txtLyDoNhap__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLyDoNhap.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLyDoNhap.myCode;
                txtLyDoNhap.Init();
                txtLyDoNhap.SetCode(oldCode);
                txtLyDoNhap.Focus();
            }
        }

       

        void frm_themmoi_dieutiet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
            if (e.Control && e.KeyCode == Keys.P) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                grdThuocTrongthau.Focus();
                grdThuocTrongthau.MoveFirst();
            }
        }
       
        void grdThuocTrongthau_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaThongTin_Click(object sender, EventArgs e)
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdChitietdieutiet.GetCheckedRows())
            {
                int ID = Utility.Int32Dbnull(gridExRow.Cells[TThauDieutietCt.Columns.IdDieutietCt].Value);
                TThauDieutietCt.Delete(ID);
                gridExRow.Delete();
                grdChitietdieutiet.UpdateData();
                m_dtChitietdieutiet.AcceptChanges();
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
                cmdSave.Enabled = grdChitietdieutiet.RowCount > 0 && m_enAction != action.View;
                cmdPrint.Enabled = grdChitietdieutiet.RowCount > 0;
                cmdXoa.Enabled = Utility.isValidGrid(grdChitietdieutiet) && (objPhieudieutiet == null || (objPhieudieutiet != null && objPhieudieutiet.TrangThai == 0)) && m_enAction!=action.View;
                cmdAddDetail.Visible = m_enAction != action.View;
            }
            catch (Exception exception)
            {

            }
            finally
            {
                if (m_enAction == action.View)
                {
                    cmdSave.Enabled = cmdAddDetail.Enabled = false;
                }
            }
            //TinhSumThanhTien();
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void getData()
        {
            if (m_enAction == action.Update || m_enAction == action.View)
            {
                objPhieudieutiet = TThauDieutiet.FetchByID(Utility.Int32Dbnull(txtId.Text));
                if (objPhieudieutiet != null)
                {

                    dtNgayNhap.Value = objPhieudieutiet.NgayDieutiet.Value;
                    txtId.Text = Utility.sDbnull(objPhieudieutiet.IdDieutiet);
                    txtLyDoNhap._Text = objPhieudieutiet.GhiChu;
                    txtNoidieutiet.SetId(objPhieudieutiet.IdBenhvienDieutiet);
                    txtSoCV_HD_dieutiet.Text = objPhieudieutiet.SoHdongDieutiet;
                    m_dtChitietdieutiet = SPs.ThuocThauDieutietLaythongtinchitiet(objPhieudieutiet.IdDieutiet).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grdChitietdieutiet, m_dtChitietdieutiet, false, true, "1=1", "");
                    grdChitietdieutiet.RootTable.Columns["XOA"].Visible = m_enAction != action.View;
                    grdChitietdieutiet.RootTable.Columns["so_luong"].EditType = m_enAction == action.View || objPhieudieutiet.TrangThai == 1 ? EditType.NoEdit : EditType.TextBox;
                }
            }
            if (m_enAction == action.Insert)
            {
                objPhieudieutiet = new TThauDieutiet();
                m_dtChitietdieutiet = SPs.ThuocThauDieutietLaythongtinchitiet(-1).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChitietdieutiet, m_dtChitietdieutiet, false, true, "1=1", "");
            }
        }
        DataTable m_dtThuoctrongthau = new DataTable();
        public long id_thau = -1;
        public long id_dieutiet = -1;
        private void InitData()
        {
            objThau = TThau.FetchByID(id_thau);
            m_dtThuoctrongthau = SPs.ThuocThauLaythongtinchitiet(id_thau).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdThuocTrongthau, m_dtThuoctrongthau, true, true, "1=1", "");
            txtthuoc.Init(m_dtThuoctrongthau, new List<string>() { "id_thuoc", "ma_thuoc", "ten_thuoc" });
            txtNoidieutiet.Init(globalVariables.gv_dtDmucBenhVien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });
        }
        TThau objThau = null;
        private void frm_themmoi_dieutiet_Load(object sender, EventArgs e)
        {
            txtId.Text = id_dieutiet.ToString();
            InitData();
            getData();
            ModifyCommand();
        }
        
  
        /// <summary>
        /// HÀM HỰC HIỆN VIỆC LƯU LẠI THÔNG TIN 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (m_enAction != action.Insert)
            {
                objPhieudieutiet = TThauDieutiet.FetchByID(Utility.Int32Dbnull(txtId.Text));
                if (objPhieudieutiet == null)
                {
                    Utility.SetMsg(lblMsg, "Phiếu điều tiết thầu không tồn tại(Có thể đã bị xóa trong lúc bạn đang thao tác). Vui lòng kiểm tra lại bằng cách thoát chức năng và nhấn nút Refresh để nạp lại thông tin các phiếu điều tiết theo thầu", true);
                    return;
                }
                if (objPhieudieutiet.TrangThai == 1)
                {
                    Utility.SetMsg(lblMsg, "Phiếu điều tiết đã được duyệt trong lúc bạn thao tác nên không thể chỉnh sửa. Vui lòng kiểm tra lại", true);
                    return;
                }
            }
            if (txtNoidieutiet.MyID == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn bệnh viện điều tiết đến", true);
                txtNoidieutiet.Focus();
                txtNoidieutiet.SelectAll();
                return ;
            }
            if (Utility.sDbnull(txtSoCV_HD_dieutiet.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập số Công văn(hoặc số Hợp đồng) điều tiết", true);
                txtSoCV_HD_dieutiet.Focus();
                return ;
            }
            if (objThau!=null &&  dtNgayNhap.Value < objThau.NgayXacnhan)
            {
                Utility.SetMsg(lblMsg, string.Format("Ngày điều tiết phải >= ngày xác nhận thầu {0}", objThau.NgayXacnhan.Value.ToString("dd/MM/yyyy")), true);
                txtSoCV_HD_dieutiet.Focus();
                return;
            }
            SqlQuery sqlQuery = new Select().From(TThauDieutiet.Schema).Where(TThauDieutiet.Columns.IdBenhvienDieutiet).IsEqualTo(txtNoidieutiet.MyID).And(TThauDieutiet.Columns.SoHdongDieutiet).IsEqualTo(Utility.sDbnull(txtSoCV_HD_dieutiet.Text));
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg(string.Format("Đã tồn tại phiếu điều tiết cho Bệnh viện {0} với số HĐ/CV điều tiết {1}. Vui lòng nhập các thông tin khác", txtNoidieutiet.Text, txtSoCV_HD_dieutiet.Text), "Thông báo", MessageBoxIcon.Warning);
                return ;
            }
            if (grdChitietdieutiet.GetDataRows().Length <= 0 || m_dtChitietdieutiet.Rows.Count <= 0)
            {
                if (m_enAction == action.Insert)
                {
                    Utility.ShowMsg("Chưa có thông tin điều tiết để lưu");
                    return;
                }
                else if (m_enAction == action.Update && objPhieudieutiet != null && objPhieudieutiet.IdDieutiet>0)
                {
                    if (Utility.AcceptQuestion("Chi tiết điều tiết đã được xóa hết. Bạn có chắc chắn muốn xóa phiếu điều tiết đang sửa?", "Xác nhận", true))
                    {
                        int num = new Delete().From(TThauDieutiet.Schema).Where(TThauDieutiet.Columns.IdDieutiet).IsEqualTo(objPhieudieutiet.IdDieutiet).Execute();
                        this.Close();
                    }
                    return;
                }
            }
            Insert_Update();
        }
       
        #region "khai báo các đối tượng để thực hiện việc "
        TThauDieutiet objPhieudieutiet = new TThauDieutiet();
        private TThauDieutiet TaoPhieudieutiet()
        {
            if (objPhieudieutiet == null) objPhieudieutiet = new TThauDieutiet();
            if (objPhieudieutiet.IdDieutiet>0)
            {
                objPhieudieutiet.MarkOld();
                objPhieudieutiet.NgaySua = DateTime.Now;
                objPhieudieutiet.NguoiSua = globalVariables.UserName;
                objPhieudieutiet.IsNew = false;
            }
            else
            {
                objPhieudieutiet.IsNew = true;
                objPhieudieutiet.TrangThai = 0;
                objPhieudieutiet.NgayTao = DateTime.Now;
                objPhieudieutiet.NguoiTao = globalVariables.UserName;
            }
            objPhieudieutiet.IdThau = id_thau;
            objPhieudieutiet.IdBenhvienDieutiet = Utility.Int32Dbnull(txtNoidieutiet.MyID);
            objPhieudieutiet.SoHdongDieutiet = Utility.sDbnull(txtSoCV_HD_dieutiet.Text);
            objPhieudieutiet.GhiChu = txtLyDoNhap.Text;
            objPhieudieutiet.NgayDieutiet = dtNgayNhap.Value;
            return objPhieudieutiet;
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin chi tiết
        /// </summary>
        /// <returns></returns>
        private List<TThauDieutietCt> getChitietDieutiet()
        {
           
            List<TThauDieutietCt> lstItems = new List<TThauDieutietCt>();
           
            foreach (DataRow dr in m_dtChitietdieutiet.Rows)
            {
              
                  TThauDieutietCt  newItem = new TThauDieutietCt();
                    newItem.IdThuoc =
                        Utility.Int32Dbnull(dr[TThauDieutietCt.Columns.IdThuoc]);
                    newItem.IdDieutietCt = Utility.Int64Dbnull(dr[TThauDieutietCt.Columns.IdDieutietCt],0);
                    newItem.IdDieutiet = Utility.Int64Dbnull(dr[TThauDieutietCt.Columns.IdDieutiet], 0);
                    newItem.IdThau = Utility.Int64Dbnull(dr[TThauDieutietCt.Columns.IdThau]);
                    newItem.IdThauCt = Utility.Int64Dbnull(dr[TThauDieutietCt.Columns.IdThauCt]);
                    newItem.SoLuong = Utility.Int32Dbnull(dr[TThauDieutietCt.Columns.SoLuong]);
                    //newItem.IdBenhvienDieutiet = Utility.Int32Dbnull(dr[TThauDieutietCt.Columns.IdBenhvienDieutiet], "");
                    //newItem.SoHdongDieutiet = Utility.sDbnull(dr[TThauDieutietCt.Columns.SoHdongDieutiet], "");
                    
                    lstItems.Add(newItem);
                   
            }
            return lstItems;
        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        private void Insert_Update()
        {
            TThauDieutiet objPhieudieutiet = TaoPhieudieutiet();

             List<TThauDieutietCt> lstItems=getChitietDieutiet();
            using (var scope = new TransactionScope())
            {
                using (var sh = new SharedDbConnectionScope())
                {
                    objPhieudieutiet.Save();
                    foreach (TThauDieutietCt item in lstItems)
                    {
                        if (item.IdDieutietCt <= 0)
                        {
                            item.IdDieutiet = objPhieudieutiet.IdDieutiet;
                            item.IsNew = true;
                            item.Save();
                        }
                        else
                        {
                            new Update(TThauDieutietCt.Schema)
                                .Set(TThauDieutietCt.Columns.SoLuong).EqualTo(item.SoLuong)
                                //.Set(TThauDieutietCt.Columns.IdBenhvienDieutiet).EqualTo(item.IdBenhvienDieutiet)
                                //.Set(TThauDieutietCt.Columns.SoHdongDieutiet).EqualTo(item.SoHdongDieutiet)
                                .Where(TThauDieutietCt.Columns.IdDieutietCt).IsEqualTo(item.IdDieutietCt)
                                .Execute();
                        }
                    }
                }
                scope.Complete();
            }
            //SPs.SpPostTTThau2SoQDinh(Utility.Int32Dbnull(objThau.IdThau, -1)).Execute();
            if (m_enAction == action.Insert)
            {
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới Thông tin điều tiết  thầu thành công: {0} thành công", objPhieudieutiet.IdDieutiet), objPhieudieutiet.IsNew ? newaction.Insert : newaction.Update, "UI");
                MessageBox.Show("Đã thêm mới điều tiết thầu thành công. Nhấn Ok để kết thúc");
                if (_OnCreated != null) _OnCreated(objPhieudieutiet.IdDieutiet, action.Insert);
                m_enAction = action.Update;
                this.Close();
            }
            else if (m_enAction == action.Update)
            {
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Thông tin thầu có ID: {0} thành công", objPhieudieutiet.IdDieutiet), objPhieudieutiet.IsNew ? newaction.Insert : newaction.Update, "UI");
                if (_OnCreated != null) _OnCreated(objPhieudieutiet.IdDieutiet, action.Update);
                MessageBox.Show("Đã cập nhật điều tiết thầu thành công. Nhấn Ok để kết thúc");
                m_enAction = action.Update;
                this.Close();
            }
            txtId.Text = Utility.sDbnull(objPhieudieutiet.IdDieutiet);
        }
       
        private void AddDetailNext(Janus.Windows.GridEX.GridEXRow gridExRow)
        {
            try
            {
                long id_thau_ct = Utility.Int64Dbnull(gridExRow.Cells["id_thau_ct"].Value);
                decimal so_luong = Utility.DecimaltoDbnull(txtSoluong.Text, 0);
                if (so_luong > 0)
                {

                    DataRow[] arrDr = m_dtChitietdieutiet.Select(TThauDieutietCt.Columns.IdThauCt + "=" + id_thau_ct.ToString());
                    if (arrDr.Length <= 0)
                    {
                        DataRow _source = ((DataRowView)gridExRow.DataRow).Row;
                        DataRow dest = m_dtChitietdieutiet.NewRow();
                        Utility.CopyData(_source, ref dest);
                        dest["SO_LUONG"] = so_luong;
                        //dest["id_bvien"] = Utility.Int32Dbnull(txtNoidieutiet.MyID);
                        //dest["ten_benhvien_dieutiet"] = Utility.sDbnull(txtNoidieutiet.Text);
                        //dest["so_hdong_dieutiet"] = Utility.sDbnull(txtSoCV_HD_dieutiet.Text);
                        dest["thanh_tien"] = Utility.DecimaltoDbnull(dest["don_gia"], 0) * Utility.DecimaltoDbnull(dest["SO_LUONG"], 0);
                        m_dtChitietdieutiet.Rows.Add(dest);
                    }
                    else
                    {
                        arrDr[0]["SO_LUONG"] = so_luong;
                        //arrDr[0]["id_bvien"] = Utility.Int32Dbnull(txtNoidieutiet.MyID);
                        //arrDr[0]["ten_benhvien_dieutiet"] = Utility.sDbnull(txtNoidieutiet.Text);
                        //arrDr[0]["so_hdong_dieutiet"] = Utility.sDbnull(txtSoCV_HD_dieutiet.Text);
                        arrDr[0]["thanh_tien"] = Utility.DecimaltoDbnull(arrDr[0]["don_gia"], 0) * Utility.DecimaltoDbnull(arrDr[0]["SO_LUONG"], 0);
                        m_dtChitietdieutiet.AcceptChanges();

                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển thuốc:\n" + ex.Message);
            }
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            Xoa();  
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            m_dtThuoctrongthau = SPs.ThuocThauLaythongtinchitiet(id_thau).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdThuocTrongthau, m_dtThuoctrongthau, true, true, "1=1", "");
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridEXRow _row in grdThuocTrongthau.GetDataRows())
                {
                    txtthuoc.SetId(_row.Cells["id_thuoc"].Value);
                    txtSoluong.Text = Utility.sDbnull(_row.Cells["sl_chuyen"].Value, "0");
                    txtthuoc.RaiseEnterEvents();
                    cmdAddDetail.PerformClick();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdPrevius_Click(object sender, EventArgs e)
        {

        }

        private void cmdSendAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chuyển toàn bộ thuốc trong thầu sang phiếu điều tiết"), "Cảnh báo", true)) return;
                foreach (DataRow dr in m_dtThuoctrongthau.Rows)
                {
                    dr["sl_chuyen"] = dr["sl_khachuyen"];
                }
                m_dtThuoctrongthau.AcceptChanges();
                cmdNext.PerformClick();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
