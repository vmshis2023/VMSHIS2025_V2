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
using VMS.HIS.Bus.Emr;
using System.Transactions;

namespace VNS.HIS.UI.DANHMUC.PHIEU
{
    public partial class frm_ThemChitietTruyendich : Form
    {
        public KcbLuotkham objLuotkham;
        public action em_Action = action.Insert;
        public DataTable m_dtPhieuchitiet=new DataTable();
        public DataTable dt_ThuocKetHop = new DataTable();        
        private ActionResult actionResult = ActionResult.Error;
        public long IdPhieu = -1;
        public long id_chitietdonthuoc = -1;
        public long id_donthuoc = -1;
        public int id_thuoc  = -1;
        public int Id_ThuocKho  = -1;
        public string TenThuoc  = "";
        public string solo  = "";
        public int Doctor_ID = -1;
        public decimal soluong_conlai = 0;
        public decimal soluong_ke = 0;
        public decimal soluongdatruyen = 0;
        public int id_khoadieutri = -1;
        public int id_BG = -1;
        public NoitruPhieudieutri objPDT;
        TThuockho thuockho = null;
        public Janus.Windows.GridEX.GridEX grdList;
        public bool b_Cancel = false;
        private readonly Logger _log;
        public NoitruPhieudichtruyenChitiet objPhieudichtruyen_chitiet = null;
        public NoitruPhieudichtruyen objphieu = null;
        double soGiotMoiMl = 20;
        bool AllowValueChanged = false;
        public frm_ThemChitietTruyendich()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            grdDonthuocchitiet.RowCheckStateChanged += grdDonthuocchitiet_RowCheckStateChanged;
          
            dtpNgaythuchien.Value = THU_VIEN_CHUNG.GetSysDateTime();
            dtp_thoigianbatdau.Value =  dtpNgaythuchien.Value.AddSeconds(5);
            dtp_thoigianketthuc.Value = dtp_thoigianbatdau.Value.AddSeconds(5);
            nmr_TocDo.ValueChanged += Nmr_TocDo_ValueChanged;
            dtp_thoigianbatdau.ValueChanged += Dtp_thoigianketthuc_ValueChanged;
            nmr_Volume.ValueChanged += Nmr_Volume_ValueChanged;
            grd_chitiet_dichtruyen.ColumnButtonClick += Grd_chitiet_dichtruyen_ColumnButtonClick;
            SetProperties(this);
        }

        private void Grd_chitiet_dichtruyen_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                NoitruPhieudichtruyenChitiet objPTD = NoitruPhieudichtruyenChitiet.FetchByID(Utility.Int64Dbnull(grd_chitiet_dichtruyen.GetValue("id")));
                if(objPTD!=null)
                {
                    dtpNgaythuchien.Value = objPTD.ThoigianKetthuc;
                    dtp_thoigianbatdau.Value = objPTD.ThoigianKetthuc.AddMinutes(5);
                }    
            }
            catch (Exception)
            {

            }
        }

        void SetProperties(Control parent)
        {
            foreach (Control ctr in parent.Controls)
                if (ctr.GetType().Equals(nmr_SoLuong.GetType()))
                {
                    NumericUpDown nmr = ctr as NumericUpDown;
                    nmr.Tag = 0;
                    nmr.MouseUp += nmr_MouseUp;
                    nmr.GotFocus += nmr_GotFocus;
                    nmr.Leave += nmr_Leave;

                }
                else
                    SetProperties(ctr);
        }

        private void nmr_GotFocus(object sender, EventArgs e)
        {

            NumericUpDown nmr = sender as NumericUpDown;
            // focus từ bàn phím (TAB), chưa có mouse event, chọn toàn bộ
            nmr.Select(0, nmr.Text.Length);
            nmr.Tag = 1;
        }

        private void nmr_MouseUp(object sender, MouseEventArgs e)
        {
            NumericUpDown nmr = sender as NumericUpDown;
            // chọn toàn bộ sau khi click, tránh lặp lại nếu đã chọn
            if (nmr.Tag.ToString() != "1")
            {
                nmr.BeginInvoke((MethodInvoker)(() =>
                {
                    nmr.Select(0, nmr.Text.Length);
                }));
                nmr.Tag = 1;
            }
        }

        private void nmr_Leave(object sender, EventArgs e)
        {
            NumericUpDown nmr = sender as NumericUpDown;
            nmr.Tag = 0; // reset khi rời khỏi control
        }
        private void Nmr_Volume_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowValueChanged) return;
            dtp_thoigianketthuc.Value = TinhThoiGianKetThuc(dtp_thoigianbatdau.Value, Utility.DoubletoDbnull(nmr_Volume.Value), Utility.DoubletoDbnull(nmr_TocDo.Value));
        }

        private void Dtp_thoigianketthuc_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowValueChanged) return;
            dtp_thoigianketthuc.Value = TinhThoiGianKetThuc(dtp_thoigianbatdau.Value, Utility.DoubletoDbnull(nmr_Volume.Value), Utility.DoubletoDbnull(nmr_TocDo.Value));
        }

        private void Nmr_TocDo_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowValueChanged) return;
            lblRoman.Text = Utility.FromNumber2ToRoman(Utility.Int32Dbnull(nmr_TocDo.Value));
            dtp_thoigianketthuc.Value = TinhThoiGianKetThuc(dtp_thoigianbatdau.Value, Utility.DoubletoDbnull(nmr_Volume.Value), Utility.DoubletoDbnull(nmr_TocDo.Value));
        }
         DateTime TinhThoiGianKetThuc(
    DateTime thoiGianBatDau,
    double theTichMl,
    double tocDoGiotPhut
    )
        {
            if (theTichMl <= 0 || tocDoGiotPhut <= 0 || soGiotMoiMl <= 0)
                throw new ArgumentException("Giá trị phải lớn hơn 0.");

            // Tổng thời gian truyền (phút)
            double thoiGianTruyenPhut = (theTichMl * soGiotMoiMl) / tocDoGiotPhut;
            lblThoigian.Text =string.Format("(Khoảng {0} phút)",Convert.ToInt32( thoiGianTruyenPhut).ToString());
            // Cộng thêm vào thời gian bắt đầu
            return thoiGianBatDau.AddMinutes(thoiGianTruyenPhut);
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
            if (!chk_freedom.Checked)
            {
                if (id_chitietdonthuoc < 0)
                {
                    Utility.SetMsgError(errorProvider1, txtidphieuthuoc, "Phải chọn thuốc truyền dịch");

                    return false;
                }
            }
            if(dtpNgaythuchien.Value<objPDT.NgayDieutri.Value)
            {
                Utility.SetMsgError(errorProvider1, dtpNgaythuchien,string.Format( "Ngày thực hiện dịch truyền phải sau ngày lập y lệnh {0}", objPDT.NgayDieutri.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                dtpNgaythuchien.Focus();
                return false;
            }    
            if (Utility.DecimaltoDbnull(nmr_SoLuong.Value,0)<=0)
            {
                Utility.SetMsgError(errorProvider1, nmr_SoLuong, "Số lượng thuốc truyền dịch phải lớn hơn 0(>0)");
                nmr_SoLuong.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(nmr_TocDo.Value, 0) <= 0)
            {
                Utility.SetMsgError(errorProvider1, nmr_TocDo, "Tốc độ truyền phải lớn hơn 0(>0)");
                nmr_TocDo.Focus();
                return false;
            }
            if (dtp_thoigianbatdau.Text == "")
            {
                Utility.SetMsgError(errorProvider1, dtp_thoigianbatdau, "Bạn phải nhập thời gian bắt đầu truyền dịch");
                dtp_thoigianbatdau.Focus();
                return false;
            }
            if (dtp_thoigianbatdau.Value < dtpNgaythuchien.Value.Date)
            {
                Utility.SetMsgError(errorProvider1, dtp_thoigianbatdau, "Thời gian bắt đầu phải >= ngày thực hiện");
                dtp_thoigianbatdau.Focus();
                return false;
            }
            if (dtp_thoigianketthuc.Text == "")
            {
                Utility.SetMsgError(errorProvider1, dtp_thoigianketthuc, "Bạn phải nhập thời gian kết thúc truyền dịch");
                dtp_thoigianketthuc.Focus();
                return false;
            }
            if (dtp_thoigianbatdau.Value >= dtp_thoigianketthuc.Value)
            {
                Utility.SetMsgError(errorProvider1, dtp_thoigianketthuc, "Thời gian kết thúc phải sau thời gian bắt đầu");
                dtp_thoigianketthuc.Focus();
                return false;
            }
            if (txtBacSyCD.MyID == "-1")
            {
                Utility.SetMsgError(errorProvider1, txtBacSyCD, "Bạn phải chọn Bác sĩ chỉ định dịch truyền");
                txtBacSyCD.Focus();
                return false;
            }
            if (Utility.Int32Dbnull( cbo_Yta.SelectedValue,0)<=0)
            {
                Utility.SetMsgError(errorProvider1, cbo_Yta, "Bạn phải chọn y tá thực hiện truyền dịch");
                cbo_Yta.Focus();
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
            nmr_SoLuong.ResetText();
            txtSoLo.Clear();
            nmr_TocDo.Clear();           
            GetData();
        }
        /// <summary>
        /// hàm thực hiện việc  thêm thông tin của bản ghi
        /// </summary>
        private void InsertData()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        CreatePhieuTruyenDich();
                        CreateNewPhieu();
                        objPhieudichtruyen_chitiet.Save();

                        actionResult = ActionResult.Success;
                        IdPhieu = objPhieudichtruyen_chitiet.IdPhieu;
                        SetStatusControl();
                    }
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
            }
        }
        NoitruPhieudichtruyen objPhieu = null;
        EmrDocuments emrdoc = new EmrDocuments();
        void CreatePhieuTruyenDich()
        {
          
            objPhieu = new Select().From(NoitruPhieudichtruyen.Schema)
                .Where(NoitruPhieudichtruyen.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(NoitruPhieudichtruyen.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .And(NoitruPhieudichtruyen.Columns.IdKhoadieutri).IsEqualTo(id_khoadieutri)
                .And(NoitruPhieudichtruyen.Columns.Noitru).IsEqualTo(true)
                .ExecuteSingle<NoitruPhieudichtruyen>();
            if (objPhieu==null || objPhieu.IdPhieu<=0)
            {
                objPhieu = new NoitruPhieudichtruyen();
                objPhieu.NgayTao = globalVariables.SysDate;
                objPhieu.NguoiTao = globalVariables.UserName;
            }
            else
            {
                IdPhieu = objPhieu.IdPhieu;
                objPhieu.NgaySua = globalVariables.SysDate;
                objPhieu.NguoiSua = globalVariables.UserName;
                return;
            }
            objPhieu.IdBenhnhan = objLuotkham.IdBenhnhan;
            objPhieu.MaLuotkham = objLuotkham.MaLuotkham;
            objPhieu.NgayPhieu = objPDT.NgayDieutri.Value;
            objPhieu.IdKhoadieutri = Utility.Int16Dbnull(objPDT.IdKhoanoitru, 0);
            objPhieu.IdKhoa = Utility.Int16Dbnull(objPDT.IdKhoanoitru,0);
            objPhieu.IdBuong =Utility.Int16Dbnull( objPDT.IdBuong, 0);
            objPhieu.IdGiuong = Utility.Int16Dbnull(objPDT.IdGiuong, 0);
            objPhieu.Noitru = true;
            objPhieu.ChanDoan = "";
            if (objPhieu.IdPhieu <= 0)
            {
                objPhieu.Save();
                emrdoc.InitDocument(objPhieu.IdBenhnhan, objPhieu.MaLuotkham, Utility.Int64Dbnull(objPhieu.IdPhieu), objPhieu.NgayPhieu.Value, Loaiphieu_HIS.PHIEUTHEODOI_TRUYENDICH, "PHIEUTHEODOI_TRUYENDICH", objPhieu.NguoiTao, Utility.Int16Dbnull(objPhieu.IdKhoadieutri), -1, Utility.Byte2Bool(0), "");
                emrdoc.Save();
            }
            IdPhieu = objPhieu.IdPhieu;
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin 
        /// </summary>
        private void UpdateData()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        CreatePhieuTruyenDich();
                        CreateNewPhieu();
                        objPhieudichtruyen_chitiet.Save();
                        //Lưu thông tin thực hiện đơn thuốc cho thuốc dịch truyền và thuốc kết hợp
                        actionResult = ActionResult.Success;
                        IdPhieu = objPhieudichtruyen_chitiet.IdPhieu;
                        SetStatusControl();
                    }
                    scope.Complete();
                }
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
                DataRow newDr = m_dtPhieuchitiet.NewRow();
                Utility.FromObjectToDatarow(objPhieudichtruyen_chitiet,ref newDr);
                //newDr[NoitruPhieudichtruyenChitiet.Columns.IdPhieu] = IdPhieu;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.NgayThuchien] = dtpNgaythuchien.Value;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.ThoigianBatdau] = dtp_thoigianbatdau.Value;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.ThoigianKetthuc] = dtp_thoigianketthuc.Value;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.IdThuoc] = Utility.Int32Dbnull(txtDrug_Id.Text, -1);
                //newDr[NoitruPhieudichtruyenChitiet.Columns.NguoiThuchien] = globalVariables.UserName;
                newDr["ten_dichtruyen"] = txtTenThuoc.Text;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.IdChitietdonthuoc] = id_chitietdonthuoc;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.NgayTao] = DateTime.Now;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.NguoiTao] = globalVariables.UserName;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.IdDonthuoc] = id_donthuoc;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.TrangthaiIn] = 0;
                //newDr[NoitruPhieudichtruyenChitiet.Columns.TocDo] = Utility.sDbnull(txtTocDo.Text, "");
                //newDr[NoitruPhieudichtruyenChitiet.Columns.SoLuong] = Utility.Int32Dbnull(txtQuantity.Text, 0);
                //newDr[NoitruPhieudichtruyenChitiet.Columns.IdBacsichidinh] = Utility.Int32Dbnull(txtBacSyCD.MyID, -1);
                //newDr[NoitruPhieudichtruyenChitiet.Columns.IdYtathuchien] = Utility.Int32Dbnull(txtYta.MyID, -1);
                // newDr["BSStaffName"] =cboBacSyCD.SelectedIndex>0? Utility.sDbnull(cboBacSyCD.Text,""):"";
                newDr["ten_bacsi_chidinh"] = txtBacSyCD.Text;
                newDr["ten_yta_thuchien"] = cbo_Yta.Text;
                newDr["so_lo"] = txtSoLo.Text;
                m_dtPhieuchitiet.Rows.Add(newDr);
                m_dtPhieuchitiet.AcceptChanges();
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

                DataRow[] newDr = m_dtPhieuchitiet.Select("id=" + Utility.Int64Dbnull(txtID.Text, -1));
                if (newDr.GetLength(0) > 0)
                {
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.NgayThuchien] = dtpNgaythuchien.Value;
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.ThoigianBatdau] = dtp_thoigianbatdau.Value;
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.ThoigianKetthuc] = dtp_thoigianketthuc.Value;
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.IdThuoc] = id_thuoc;
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.IdChitietdonthuoc] = id_chitietdonthuoc;
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.IdDonthuoc] = id_donthuoc;
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.TocDo] = Utility.sDbnull(nmr_TocDo.Text, "");
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.SoLuong] = Utility.DecimaltoDbnull(nmr_SoLuong.Value, 0);
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.IdBacsichidinh] = Utility.Int32Dbnull(txtBacSyCD.MyID, -1);
                    newDr[0][NoitruPhieudichtruyenChitiet.Columns.IdYtathuchien] = Utility.Int32Dbnull(cbo_Yta.SelectedValue, -1);
                    newDr[0]["ten_dichtruyen"] = txtTenThuoc.Text;
                    newDr[0]["ten_bacsi_chidinh"] = txtBacSyCD.Text;
                    newDr[0]["ten_yta_thuchien"] = cbo_Yta.Text;
                    newDr[0]["so_lo"] = txtSoLo.Text;


                }
                m_dtPhieuchitiet.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private NoitruPhieudichtruyenChitiet CreateNewPhieu()
        {

            if (objPhieudichtruyen_chitiet == null) objPhieudichtruyen_chitiet = new NoitruPhieudichtruyenChitiet();
            objPhieudichtruyen_chitiet.NguoiThuchien = globalVariables.UserName;
            objPhieudichtruyen_chitiet.IdPhieu = IdPhieu;
            objPhieudichtruyen_chitiet.NgayThuchien = Convert.ToDateTime(dtpNgaythuchien.Value);
            objPhieudichtruyen_chitiet.SoLuong = Utility.DecimaltoDbnull(nmr_SoLuong.Value);
            objPhieudichtruyen_chitiet.IdThuoc = Utility.Int32Dbnull(id_thuoc);
            objPhieudichtruyen_chitiet.SoLo = Utility.sDbnull(txtSoLo.Text);
            objPhieudichtruyen_chitiet.TenDichtruyen = Utility.sDbnull(txtTenThuoc.Text);
            objPhieudichtruyen_chitiet.ThoigianBatdau = Convert.ToDateTime(dtp_thoigianbatdau.Value);
            objPhieudichtruyen_chitiet.ThoigianKetthuc = Convert.ToDateTime(dtp_thoigianketthuc.Value);
            //objPhieudichtruyen_chitiet.BsCd = Utility.Int32Dbnull(cboBacSyCD.SelectedValue, -1);
            objPhieudichtruyen_chitiet.IdBacsichidinh = Utility.Int32Dbnull(txtBacSyCD.MyID, -1);
            objPhieudichtruyen_chitiet.TenBacsyChidinh = txtBacSyCD.Text;
            objPhieudichtruyen_chitiet.TenYtaThuchien = cbo_Yta.Text;
            objPhieudichtruyen_chitiet.IdYtathuchien = Utility.Int32Dbnull(cbo_Yta.SelectedValue, -1);
            objPhieudichtruyen_chitiet.TheTich = Utility.Int32Dbnull(nmr_Volume.Text, null);
            objPhieudichtruyen_chitiet.TocDo = Utility.Int32Dbnull(nmr_TocDo.Text, null);
            objPhieudichtruyen_chitiet.Roman = lblRoman.Text;
            objPhieudichtruyen_chitiet.IdChitietdonthuoc = id_chitietdonthuoc;
            objPhieudichtruyen_chitiet.IdDonthuoc = id_donthuoc;
            objPhieudichtruyen_chitiet.IdThuockho = Id_ThuocKho;
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
            objPhieudichtruyen_chitiet.IdThuocKethop = Utility.sDbnull(sthuockethop.ToString(), "");
            if (em_Action == action.Insert || objPhieudichtruyen_chitiet.IdPhieu <= 0)
            {
                objPhieudichtruyen_chitiet.IsNew = true;
                objPhieudichtruyen_chitiet.NguoiTao = globalVariables.UserName;
                objPhieudichtruyen_chitiet.NgayTao = DateTime.Now;
                objPhieudichtruyen_chitiet.TrangthaiIn = 0;
            }
            if (em_Action == action.Update)
            {
                objPhieudichtruyen_chitiet.MarkOld();
                objPhieudichtruyen_chitiet.Id = Utility.Int64Dbnull(txtID.Text, -1);
                objPhieudichtruyen_chitiet.NguoiSua = globalVariables.UserName;
                objPhieudichtruyen_chitiet.NgaySua = DateTime.Now;
            }
            return objPhieudichtruyen_chitiet;
        }
      
        /// <summary>
        /// hàm phím tắt thực hiện việc trong form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ThemChitietTruyendich_KeyDown(object sender, KeyEventArgs e)
        {
           
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.Control&&e.KeyCode==Keys.S)cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }

        private Query _query = NoitruPhieudichtruyenChitiet.CreateQuery();
        /// <summary>
        /// hàm thực hiện load form hiện tại lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ThemChitietTruyendich_Load(object sender, EventArgs e)
        {
            try
            {
                AllowValueChanged = false;
               //nmr_SoLuong.Maximum = soluong_ke;
                try
                {
                    List<string> lstHMS = new List<string>();
                    lstHMS = objPDT.GioDieutri.Split(':').ToList<string>();
                    objPDT.NgayDieutri = objPDT.NgayDieutri.Value.Date.AddHours(Convert.ToInt32(lstHMS[0])).AddMinutes(Convert.ToInt32(lstHMS[1])).AddSeconds(Convert.ToInt32(lstHMS[2]));
                }
                catch (Exception)
                {              
                }
               
                if (em_Action==action.Insert)
                {
                    //nmr_SoLuong.Maximum = soluong_conlai;
                    nmr_SoLuong.Value = soluong_conlai;
                    dtpNgaythuchien.Value = objPDT.NgayDieutri.Value.AddMinutes(1);
                }   
                else//Update
                {

                }    
                soGiotMoiMl = Utility.DoubletoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TRUYENDICH_SOGIOT_TREN_ML","20",false));
                lblLoaiDayTruyen.Text = string.Format("Đang dùng loại dây truyền {0} giọt/phút", soGiotMoiMl);
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
               // DataTable m_dtKhoaNoItru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0, -1);
                DataTable m_dtKhoaNoItru = Utility.ExecuteSql("select id_khoaphong, ma_khoaphong, ten_khoaphong from dmuc_khoaphong", CommandType.Text).Tables[0];
                txtKhoaphong.Init(m_dtKhoaNoItru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
                DataTable dtNhanVien = Utility.ExecuteSql("select id_nhanvien,ma_nhanvien,ten_nhanvien from dmuc_nhanvien order by ten_nhanvien", CommandType.Text).Tables[0];
                DataBinding.BindDataCombobox(cbo_Yta, dtNhanVien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
                txtidphieuthuoc.Text = Utility.sDbnull(id_chitietdonthuoc);
                txtDrug_Id.Text = Utility.sDbnull(id_thuoc);
               
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
                LoadPhieuTheoDoiTruyenDich();
                dtpNgaythuchien.Focus();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                txtTenThuoc.ReadOnly = txtSoLo.ReadOnly = txtBacSyCD.ReadOnly = !chk_freedom.Checked;
            }
        }
        private void LoadPhieuTheoDoiTruyenDich()
        {
            try
            {
                id_khoadieutri = Convert.ToInt16(objPDT != null ? objPDT.IdKhoanoitru : -1);
                m_dtPhieuchitiet = SPs.NoitruPhieutruyendichLaydanhsach(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, id_khoadieutri, -1).GetDataSet().Tables[0];
                txt_TheTichDaTruyen.Text = Utility.sDbnull(m_dtPhieuchitiet.AsEnumerable().Sum(c => Utility.Int32Dbnull(c["the_tich"])));
                Utility.SetDataSourceForDataGridEx(grd_chitiet_dichtruyen, m_dtPhieuchitiet, false, true, "1=1", "");
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình lấy thông tin phiếu chăm sóc :{0}", exception));
                }
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
                    TudongNapthongtinTheTich();
                    AllowValueChanged = true;
                    
                    break;
                case action.Update:
                    InitData4Update();
                    break;
            }
        }
        void TudongNapthongtinTheTich()
        {
            try
            {
                //NoitruPhieudichtruyenChitiet objChitiet = new Select().From(NoitruPhieudichtruyenChitiet.Schema)
                //                .Where(NoitruPhieudichtruyenChitiet.Columns.IdChitietdonthuoc).IsEqualTo(id_chitietdonthuoc)
                //                .ExecuteSingle<NoitruPhieudichtruyenChitiet>();
                //if (objChitiet != null)
                //{
                //    nmr_Volume.Value = Utility.Int32Dbnull(objChitiet.TheTich);
                //}
            }
            catch (Exception)
            {

             
            }
             
        }
        /// <summary>
        /// hàm thực hiện việc bind thông tin
        /// </summary>
        private void InitData4Update()
        {
            objPhieudichtruyen_chitiet = NoitruPhieudichtruyenChitiet.FetchByID(Utility.Int64Dbnull(txtID.Text, -1));
            if(objPhieudichtruyen_chitiet!=null)
            {
                txtID.Text = objPhieudichtruyen_chitiet.Id.ToString();
                dtpNgaythuchien.Value = objPhieudichtruyen_chitiet.NgayThuchien;
                nmr_SoLuong.Value = Utility.DecimaltoDbnull(objPhieudichtruyen_chitiet.SoLuong);
                dtp_thoigianbatdau.Value = objPhieudichtruyen_chitiet.ThoigianBatdau;
                dtp_thoigianketthuc.Value = objPhieudichtruyen_chitiet.ThoigianKetthuc;
                nmr_TocDo.Value = Utility.Int32Dbnull(objPhieudichtruyen_chitiet.TocDo);
                nmr_Volume.Value = Utility.Int32Dbnull(objPhieudichtruyen_chitiet.TheTich,1);
                txtDrug_Id.Text = Utility.sDbnull(objPhieudichtruyen_chitiet.IdThuoc);
                txtBacSyCD.SetId(objPhieudichtruyen_chitiet.IdBacsichidinh);
                cbo_Yta.SelectedValue=Utility.Int32Dbnull( objPhieudichtruyen_chitiet.IdYtathuchien);
                txtKhoaphong.SetId(objphieu.IdKhoadieutri);
                txtBuong.Text = objphieu.TenBuong;
                txtGiuong.Text = objphieu.TenGiuong;
                AllowValueChanged = true;
                lblRoman.Text = Utility.FromNumber2ToRoman(Utility.Int32Dbnull(nmr_TocDo.Value));
                string dataString = Utility.sDbnull(objPhieudichtruyen_chitiet.IdThuocKethop, "");
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

        private void chk_freedom_CheckedChanged(object sender, EventArgs e)
        {
            txtTenThuoc.ReadOnly = txtSoLo.ReadOnly = txtBacSyCD.ReadOnly = !chk_freedom.Checked;
        }

        private void txtTocDo_Click(object sender, EventArgs e)
        {

        }
    }
}
