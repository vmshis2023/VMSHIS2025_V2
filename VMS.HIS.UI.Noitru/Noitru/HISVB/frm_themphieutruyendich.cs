using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using NLog;
using Janus.Windows.GridEX;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_themphieutruyendich : Form
    {
        public KcbLuotkham objLuotkham;
        NoitruPhieudichtruyen objPhieudichtruyen = null;
        public action em_Action = action.Insert;
        public DataTable p_DataPhieuDich=new DataTable();
        public DataTable dt_ThuocKetHop = new DataTable();        
        private ActionResult actionResult = ActionResult.Error;
        private long IdPhieu = -1;
        public long id_chitietdonthuoc = -1;
        public long id_donthuoc = -1;
        public int id_thuoc  = -1;
        public int Id_ThuocKho  = -1;
        public string TenThuoc  = "";
        public string solo  = "";
        public int Doctor_ID = -1;
        public int SoLuong  = 0;
        public int id_khoadieutri = -1;
        public int id_BG = -1;
        TThuockho thuockho = null;
        public Janus.Windows.GridEX.GridEX grdList;
        public bool b_Cancel = false;
        private readonly Logger _log;
        public NoitruPhieudichtruyen objPhieuDichTruyen = null;
        public frm_themphieutruyendich()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            grdDonthuocchitiet.RowCheckStateChanged += grdDonthuocchitiet_RowCheckStateChanged;
          
            dtCreateDate.Value = THU_VIEN_CHUNG.GetSysDateTime();
            dtBatDau.Value =  dtCreateDate.Value.AddSeconds(5);
            dtKetthuc.Value = dtBatDau.Value.AddSeconds(5);
        }

        void grdDonthuocchitiet_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {

            AddOneRow_id_thuoc(e.CheckState == RowCheckState.Checked);

        }
        /// <summary>
        /// hàm thực hiện thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidData()) return;
                PerformAction();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi :{0}", exception));
                }
                _log.Trace(exception);
            }
        }
        /// <summary>
        /// hàm thwucj hiện việc hoạt động trạng thái của phần insert hay update thông tin
        /// </summary>
        private void PerformAction()
        {
            switch(em_Action)
            {
                case action.Insert:
                    InsertData();
                    break;
                case action.Update:
                    UpdateData();
                    break;
            }
        }
        /// <summary>
        /// /hàm thực hện việc kiểm tra thông tin của phần dịch truyền
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            errorProvider1.Clear();
            if(id_chitietdonthuoc <0)
            {
                Utility.SetMsgError(errorProvider1, txtidphieuthuoc, "Phải chọn dịch truyền cần theo dõi");
               
                return false;
            }
            if (Utility.Int32Dbnull(txtQuantity.Value,0)<=0)
            {
                Utility.SetMsgError(errorProvider1, txtQuantity, "Số lượng lớn hơn 0(>0)");
                txtQuantity.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtTocDo.Value, 0) <= 0)
            {
                Utility.SetMsgError(errorProvider1, txtTocDo, "Tốc độ truyền phải lớn hơn 0(>0)");
                txtTocDo.Focus();
                return false;
            }
            if (dtBatDau.Value < dtCreateDate.Value)
            {
                Utility.SetMsgError(errorProvider1, dtBatDau, "Thời gian bắt đầu phải >= ngày thực hiện");
                dtBatDau.Focus();
                return false;
            }
            if (dtBatDau.Value > dtKetthuc.Value)
            {
                Utility.SetMsgError(errorProvider1, dtKetthuc, "Thời gian kết thúc phải >= thời gian bắt đầu");
                dtKetthuc.Focus();
                return false;
            }
            if (txtYta.MyID =="-1")
            {
                Utility.SetMsgError(errorProvider1, dtKetthuc, "Bạn phải chọn y tá thực hiện truyền dịch");
                txtYta.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện trạng thái của control khi nhập
        /// </summary>
       private void SetStatusControl()
       {
           switch (actionResult)
           {
               case ActionResult.Success:
                   Utility.ShowMsg("Bạn lưu thông tin thành công","Thông báo");
                   switch (em_Action)
                   {
                       case action.Insert:
                           txtID.Text = IdPhieu.ToString();
                           ProcessDataWhenInsert();
                           break;
                       case action.Update:
                           ProcessDataWhenUpdate();
                           break;
                   }
                   break;
               case ActionResult.Error:
                   Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin","Thông báo",MessageBoxIcon.Error);
                   break;
           }
           if (actionResult==ActionResult.Success)
           {
               if (chkContine.Checked)
               {
                   ClearControl();
               }
               else
               {
                   this.Close();
               }
           }
          
       }
        /// <summary>
        /// hàm thực hiện việc thêm mới thoogn tin
        /// </summary>
        private void ClearControl()
        {
            txtQuantity.Clear();
            txtSoLo.Clear();
            txtTocDo.Clear();           
            GetData();
        }
        /// <summary>
        /// hàm thực hiện việc  thêm thông tin của bản ghi
        /// </summary>
        private void InsertData()
        {
            CreateNewPhieu();
            objPhieudichtruyen.Save();
            actionResult = ActionResult.Success;
            IdPhieu = objPhieudichtruyen.IdPhieu;
            SetStatusControl();
        }
       /// <summary>
       /// hàm thực hiện việc cập nhập thông tin 
       /// </summary>
        private void UpdateData()
        {
            try
            {
                CreateNewPhieu();
                objPhieudichtruyen.Save();
                actionResult = ActionResult.Success;
                IdPhieu = objPhieudichtruyen.IdPhieu;
                SetStatusControl();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi :{0}", exception));
                }
                _log.Trace(exception);
            }

        }
        private void ProcessDataWhenInsert()
        {
            try
            {
                DataRow newDr = p_DataPhieuDich.NewRow();
                newDr[NoitruPhieudichtruyen.Columns.IdPhieu] = IdPhieu;
                newDr[NoitruPhieudichtruyen.Columns.NgayThuchien] = dtCreateDate.Value;
                newDr[NoitruPhieudichtruyen.Columns.ThoigianBatdau] = dtBatDau.Value;
                newDr[NoitruPhieudichtruyen.Columns.ThoigianKetthuc] = dtKetthuc.Value;
                newDr[NoitruPhieudichtruyen.Columns.MaLuotkham] = objLuotkham.MaLuotkham;
                newDr[NoitruPhieudichtruyen.Columns.IdBenhnhan] = objLuotkham.IdBenhnhan;
                newDr[NoitruPhieudichtruyen.Columns.IdThuoc] = Utility.Int32Dbnull(txtDrug_Id.Text, -1);
                newDr[NoitruPhieudichtruyen.Columns.NguoiThuchien] = globalVariables.UserName;
                newDr["ten_thuoc"] = txtTenThuoc.Text;
                newDr[NoitruPhieudichtruyen.Columns.IdChitietdonthuoc] = id_chitietdonthuoc;
                newDr[NoitruPhieudichtruyen.Columns.NgayTao] = DateTime.Now;
                newDr[NoitruPhieudichtruyen.Columns.NguoiTao] = globalVariables.UserName;
                newDr[NoitruPhieudichtruyen.Columns.IdDonthuoc] = id_donthuoc;
                newDr[NoitruPhieudichtruyen.Columns.TrangthaiIn] = 0;
                newDr[NoitruPhieudichtruyen.Columns.IdKhoadieutri] = id_khoadieutri;
                newDr[NoitruPhieudichtruyen.Columns.IdBuonggiuong] = id_BG;
                newDr[NoitruPhieudichtruyen.Columns.TocDo] = Utility.sDbnull(txtTocDo.Text, "");
                newDr[NoitruPhieudichtruyen.Columns.SoLuong] = Utility.Int32Dbnull(txtQuantity.Text, 0);
                newDr[NoitruPhieudichtruyen.Columns.IdBacsichidinh] = Utility.Int32Dbnull(txtBacSyCD.MyID, -1);
                newDr[NoitruPhieudichtruyen.Columns.IdYtathuchien] = Utility.Int32Dbnull(txtYta.MyID, -1);
                // newDr["BSStaffName"] =cboBacSyCD.SelectedIndex>0? Utility.sDbnull(cboBacSyCD.Text,""):"";
                newDr["ten_bacsi_chidinh"] = txtBacSyCD.Text;
                newDr["ten_yta_thuchien"] = txtYta.Text;
                newDr["ten_khoaphong"] = txtKhoaphong.Text;
                newDr["SO_LO"] = txtSoLo.Text;
                p_DataPhieuDich.Rows.Add(newDr);
                p_DataPhieuDich.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
          
        }
        /// <summary>
        /// hàm thực hiện xử lý thông tin cập nhập thông tin
        /// </summary>
        private void ProcessDataWhenUpdate()
        {
            try
            {

                DataRow[] newDr = p_DataPhieuDich.Select("id_phieu=" + Utility.Int64Dbnull(txtID.Text, -1));
                if (newDr.GetLength(0) > 0)
                {
                    newDr[0][NoitruPhieudichtruyen.Columns.NgayThuchien] = dtCreateDate.Value;
                    newDr[0][NoitruPhieudichtruyen.Columns.ThoigianBatdau] = dtBatDau.Value;
                    newDr[0][NoitruPhieudichtruyen.Columns.ThoigianKetthuc] = dtKetthuc.Value;
                    newDr[0][NoitruPhieudichtruyen.Columns.MaLuotkham] = objLuotkham.MaLuotkham;
                    newDr[0][NoitruPhieudichtruyen.Columns.IdBenhnhan] = objLuotkham.IdBenhnhan;
                    newDr[0][NoitruPhieudichtruyen.Columns.IdThuoc] = id_thuoc;
                    newDr[0][NoitruPhieudichtruyen.Columns.IdChitietdonthuoc] = id_chitietdonthuoc;
                    newDr[0][NoitruPhieudichtruyen.Columns.IdDonthuoc] = id_donthuoc;
                    newDr[0][NoitruPhieudichtruyen.Columns.IdBuonggiuong] = id_BG;
                    newDr[0][NoitruPhieudichtruyen.Columns.IdKhoadieutri] = id_khoadieutri;
                    newDr[0][NoitruPhieudichtruyen.Columns.TocDo] = Utility.sDbnull(txtTocDo.Text, "");
                    newDr[0][NoitruPhieudichtruyen.Columns.SoLuong] = Utility.Int32Dbnull(txtQuantity.Text, 0);
                    newDr[0][NoitruPhieudichtruyen.Columns.IdBacsichidinh] = Utility.Int32Dbnull(txtBacSyCD.MyID, -1);
                    newDr[0][NoitruPhieudichtruyen.Columns.IdYtathuchien] = Utility.Int32Dbnull(txtYta.MyID, -1);
                    //  newDr[0]["BSStaffName"] = cboBacSyCD.SelectedIndex > 0 ? Utility.sDbnull(cboBacSyCD.Text, "") : "";
                    newDr[0]["ten_bacsi_chidinh"] = txtBacSyCD.Text;
                    newDr[0]["ten_yta_thuchien"] = txtYta.Text;
                    newDr[0]["ten_khoaphong"] = txtKhoaphong.Text;
                    newDr[0]["SO_LO"] = txtSoLo.Text;


                }
                p_DataPhieuDich.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private NoitruPhieudichtruyen CreateNewPhieu()
        {

            if (objPhieudichtruyen == null) objPhieudichtruyen = new NoitruPhieudichtruyen();
            objPhieudichtruyen.MaLuotkham = objLuotkham.MaLuotkham;
            objPhieudichtruyen.IdBenhnhan = objLuotkham.IdBenhnhan;
            objPhieudichtruyen.NguoiThuchien = globalVariables.UserName;
            objPhieudichtruyen.NgayThuchien = Convert.ToDateTime(dtCreateDate.Value);
            objPhieudichtruyen.SoLuong = Utility.Int32Dbnull(txtQuantity.Value);
            objPhieudichtruyen.IdThuoc = Utility.Int32Dbnull(id_thuoc);
            objPhieudichtruyen.ThoigianBatdau = Convert.ToDateTime(dtBatDau.Value);
            objPhieudichtruyen.ThoigianKetthuc = Convert.ToDateTime(dtKetthuc.Value);
            //objPhieudichtruyen.BsCd = Utility.Int32Dbnull(cboBacSyCD.SelectedValue, -1);
            objPhieudichtruyen.IdBacsichidinh = Utility.Int32Dbnull(txtBacSyCD.MyID, -1);
            objPhieudichtruyen.IdYtathuchien = Utility.Int32Dbnull(txtYta.MyID, -1);
            objPhieudichtruyen.IdBuonggiuong = id_BG;
            objPhieudichtruyen.IdKhoadieutri = id_khoadieutri;
            objPhieudichtruyen.TocDo = Utility.Int32Dbnull(txtTocDo.Text, null);
            objPhieudichtruyen.IdChitietdonthuoc = id_chitietdonthuoc;
            objPhieudichtruyen.IdDonthuoc = id_donthuoc;
            int recordRow = 0;
            var sthuockethop = new StringBuilder("");
            if (dt_ThuocKetHop.Rows.Count > 0)
            {
                foreach (DataRow row in dt_ThuocKetHop.Rows)
                {
                    if (recordRow > 0) sthuockethop.Append(",");
                    sthuockethop.Append(Utility.sDbnull(row["id_thuoc"], ""));
                    recordRow++;
                }
            }
            objPhieudichtruyen.IdThuocKethop = Utility.sDbnull(sthuockethop.ToString(), "");
            if (em_Action == action.Insert || objPhieudichtruyen.IdPhieu <= 0)
            {
                objPhieudichtruyen.IsNew = true;
                objPhieudichtruyen.NguoiTao = globalVariables.UserName;
                objPhieudichtruyen.NgayTao = DateTime.Now;
                objPhieudichtruyen.TrangthaiIn = 0;
            }
            if (em_Action == action.Update)
            {
                objPhieudichtruyen.MarkOld();
                objPhieudichtruyen.IdPhieu = Utility.Int64Dbnull(txtID.Text, -1);
                objPhieudichtruyen.NguoiSua = globalVariables.UserName;
                objPhieudichtruyen.NgaySua = DateTime.Now;
            }
            return objPhieudichtruyen;
        }
      
        /// <summary>
        /// hàm phím tắt thực hiện việc trong form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themphieutruyendich_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F4)cmdPrint.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.Control&&e.KeyCode==Keys.S)cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }

        private Query _query = NoitruPhieudichtruyen.CreateQuery();
        /// <summary>
        /// hàm thực hiện load form hiện tại lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themphieutruyendich_Load(object sender, EventArgs e)
        {
            try
            {
                if (!dt_ThuocKetHop.Columns.Contains("id_thuoc"))
                {
                    dt_ThuocKetHop.Columns.Add("id_thuoc", typeof(string));
                }
                if (!dt_ThuocKetHop.Columns.Contains("ten_thuoc"))
                {
                    dt_ThuocKetHop.Columns.Add("ten_thuoc", typeof(string));
                }
                thuockho = TThuockho.FetchByID(Id_ThuocKho);
                txtBacSyCD.Init(globalVariables.gv_dtDmucNhanvien, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                DataTable m_dtKhoaNoItru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0, -1);
                txtKhoaphong.Init(m_dtKhoaNoItru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
                txtYta.Init(txtBacSyCD.AutoCompleteSource, txtBacSyCD.defaultItem);
               
                txtidphieuthuoc.Text = Utility.sDbnull(id_chitietdonthuoc);
                txtDrug_Id.Text = Utility.sDbnull(id_thuoc);
                txtQuantity.Value = SoLuong;
                txtTenThuoc.Text = TenThuoc;
                if (thuockho != null)
                {
                    txtSoLo.Text = thuockho.SoLo;
                }
                else
                    txtSoLo.Text = solo;

                txtBacSyCD.SetId(Doctor_ID.ToString());
                txtKhoaphong.SetId(id_khoadieutri);

                GetData();
                LoadThuocTheoDoiTruyenDich_ThuocKetHop();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        DataTable m_dtPhieuTheoDoiTruyenDich_ThuocKetHop = new DataTable();
        private void LoadThuocTheoDoiTruyenDich_ThuocKetHop()
        {
            try
            {
                
                m_dtPhieuTheoDoiTruyenDich_ThuocKetHop = SPs.NoitruPhieutruyendichLaythongtinthuoctruyendichKethop(objLuotkham.IdBenhnhan, Utility.sDbnull(objLuotkham.MaLuotkham),Utility.Int32Dbnull( txtKhoaphong.MyID,-1),id_donthuoc,id_chitietdonthuoc, id_thuoc).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdDonthuocchitiet, m_dtPhieuTheoDoiTruyenDich_ThuocKetHop, false, true, "1=1", "");
                Autocheck();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình lấy thông tin phiếu dịch truyền :{0}", exception));
                }
            }
        }
        void Autocheck()
        {
            try
            {
                foreach (GridEXRow row in grdDonthuocchitiet.GetDataRows())
                {
                    if (dt_ThuocKetHop.Select("id_thuoc=" + row.Cells["id_thuoc"].Value.ToString()).Length > 0)
                    {
                        row.BeginEdit();
                        row.IsChecked = true;
                        row.EndEdit();
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }
        private void GetData()
        {
            switch (em_Action)
            {
                case action.Insert:
                    txtID.Text = "-1";
                    break;
                case action.Update:
                    BindData();
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện việc bind thông tin
        /// </summary>
        private void BindData()
        {
            objPhieudichtruyen = NoitruPhieudichtruyen.FetchByID(Utility.Int64Dbnull(txtID.Text, -1));
            if(objPhieudichtruyen!=null)
            {

                dtCreateDate.Value = objPhieudichtruyen.NgayThuchien;
                txtQuantity.Value = Utility.Int32Dbnull(objPhieudichtruyen.SoLuong);
                dtBatDau.Value = objPhieudichtruyen.ThoigianBatdau;
                dtKetthuc.Value = objPhieudichtruyen.ThoigianKetthuc;
                txtTocDo.Value = Utility.Int32Dbnull(objPhieudichtruyen.TocDo);
                txtDrug_Id.Text = Utility.sDbnull(objPhieudichtruyen.IdThuoc);
                txtBacSyCD.SetId(objPhieudichtruyen.IdBacsichidinh);
                txtYta.SetId(objPhieudichtruyen.IdYtathuchien);
                txtKhoaphong.SetId(objPhieudichtruyen.IdKhoadieutri);
                string dataString = Utility.sDbnull(objPhieudichtruyen.IdThuocKethop, "");
                dt_ThuocKetHop.Clear();
                if (!string.IsNullOrEmpty(dataString))
                {
                    string[] lstid_thuoc = dataString.Split(',');
                    foreach (string id_thuoc in lstid_thuoc)
                    {
                        if (!string.IsNullOrEmpty(id_thuoc))
                        {
                            DataRow newDr = dt_ThuocKetHop.NewRow();
                            newDr["id_thuoc"] = id_thuoc;
                            newDr["ten_thuoc"] = gettenthuoc(id_thuoc);
                            dt_ThuocKetHop.Rows.Add(newDr);
                            dt_ThuocKetHop.AcceptChanges();
                        }
                    }
                    grdThuockethop.DataSource = dt_ThuocKetHop;
                   
                }

            }
        }
        string gettenthuoc(string id_thuoc)
        {
            var q = from p in globalVariables.gv_dtDanhMucThuoc.AsEnumerable()
                    where p["id_thuoc"].ToString() == id_thuoc
                    select p;
            if (q.Any())
                return q.First()["ten_thuoc"].ToString();
            return "";
        }
        ModifyRegistry ModifyRegistry=new ModifyRegistry();
        
        /// <summary>
        /// hàm thực hiện việc in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, EventArgs e)
        {
           
            //VietBaIT.HISLink.Reports.Class.InPhieuNoiTru.INPHIEU_THEODOI_TRUYENDICH_CHOOSE(objLuotkham, "PHIẾU THEO DÕI TRUYỀN DỊCH", IdPhieu);
        }

        private void grd_ICD_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

      
        private void AddOneRow_id_thuoc(bool CHON)
        {

            try
            {
                GridEXRow gridExRow = grdDonthuocchitiet.CurrentRow;
                 int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells["id_thuoc"].Value, -1);
                    string Drug_Name = Utility.sDbnull(gridExRow.Cells["ten_thuoc"].Value, -1);
                if (CHON)
                {
                    
                   
                    EnumerableRowCollection<DataRow> query = from thuoc in dt_ThuocKetHop.AsEnumerable()
                                                             where Utility.Int32Dbnull(thuoc["id_thuoc"]) == Drug_ID
                                                             select thuoc;


                    if (!query.Any())
                    {

                        DataRow drv = dt_ThuocKetHop.NewRow();
                        drv["id_thuoc"] = Drug_ID;
                        EnumerableRowCollection<string> query1 = from thuoc in m_dtPhieuTheoDoiTruyenDich_ThuocKetHop.AsEnumerable()
                                                                 where Utility.Int32Dbnull(thuoc["id_thuoc"]) == Drug_ID
                                                                 select Utility.sDbnull(thuoc["ten_thuoc"]);
                        if (query1.Any())
                        {
                            drv["ten_thuoc"] = Utility.sDbnull(query1.FirstOrDefault());
                        }

                        dt_ThuocKetHop.Rows.Add(drv);
                        dt_ThuocKetHop.AcceptChanges();
                        grdThuockethop.DataSource = dt_ThuocKetHop;

                    }
                    else
                    {

                    }
                }
                else
                {
                    foreach (DataRow row in dt_ThuocKetHop.Rows)
                    {
                        if (row["id_thuoc"].ToString() == Drug_ID.ToString())
                        {
                            dt_ThuocKetHop.Rows.Remove(row);
                            break;
                        }
                    }
                    dt_ThuocKetHop.AcceptChanges();
                }
            }
            catch (Exception EX )
            {
                Utility.ShowMsg("Có lỗi trong quá trình thêm thông tin vào lưới");
            }

        }

        private void grdThuockethop_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                    grdThuockethop.CurrentRow.Delete();
                    dt_ThuocKetHop.AcceptChanges();
                    grdThuockethop.Refetch();
                    grdThuockethop.AutoSizeColumns();
                    LoadThuocTheoDoiTruyenDich_ThuocKetHop();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin Mã ICD");
                throw;
            }
        }

        private void cmdGetdata_Click(object sender, EventArgs e)
        {

        }
    }
}
