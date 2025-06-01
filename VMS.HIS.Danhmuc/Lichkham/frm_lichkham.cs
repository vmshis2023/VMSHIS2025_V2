using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using SubSonic;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.UI.Forms.Dungchung.UCs;
using VNS.Libs;
using VMS.HIS.DAL;
using Janus.Windows.GridEX;
using System.Globalization;
namespace VNS.HIS.UI.Forms.Noitru
{
    public partial class frm_lichkham : Form
    {
        public delegate void OnAccept(object ID);
        public event OnAccept _OnAccept;
        bool AllowChanged = false;
        public action m_enAct = action.Insert;
        public DataTable dtThoigian = new DataTable();
        public DataTable dtLichkham = new DataTable();
        string Args = "ALL";//ALL,CREATE,VIEW,CHAMCONG
        public frm_lichkham(string Args)
        {
            InitializeComponent();
            if (!dtThoigian.Columns.Contains("id"))
                dtThoigian.Columns.AddRange(new DataColumn[] { new DataColumn("id", typeof(string)), new DataColumn("thoigian_kham", typeof(string)) });
            Utility.SetDataSourceForDataGridEx_Basic(grdThoigian, dtThoigian, true, true, "", "");
            this.Args = Args;
            dtmFrom.Value = DateTime.Now.AddDays(-7);
            dtmTo.Value = DateTime.Now.AddDays(7);
            dtNgaykham.Value = DateTime.Now;
            pnlThemmoi.Width = (this.Args == "ALL" || this.Args == "CREATE") ? 459 : 0;
            grdList.SelectionChanged += grdList_SelectionChanged;
            txtCa._OnEnterMe += txtCa__OnEnterMe;
            this.Load += frm_lichkham_Load;
            grdThoigian.ColumnButtonClick += grdThoigian_ColumnButtonClick;
            grdList.UpdatingCell += grdList_UpdatingCell;
            txtBacsi._OnEnterMe += txtBacsi__OnEnterMe;
        }

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
               _lk = QlnsLichkham.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
                if (_lk != null)
                {
                    if (_lk.TthaiLamviec == 1)
                    {
                        if (!Utility.Coquyen("lichkham_suaghichusauchamcong"))
                        {
                            Utility.thongbaokhongcoquyen("lichkham_suaghichusauchamcong", "sửa ghi chú, ghi chú chấm công sau khi đã xác nhận chấm công");
                            e.Value = e.InitialValue;
                            return;
                        }
                    }
                    if (e.Column.Key == "ghi_chu")
                    {
                        _lk.GhiChu = e.Value.ToString();
                        _lk.MarkOld();
                        _lk.IsNew = false;
                        _lk.Save();
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa ghi chú từ lịch khám của bác sĩ {0} ngày khám {1} với thông tin ghi chú từ {2} thành {3}  ", txtBacsi1.Text, _lk.Ngay.ToString("dd/MM/yyyy"),Utility.sDbnull(e.InitialValue, ""), Utility.sDbnull(e.Value, "")), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    }
                    if (e.Column.Key == "ghichu_chamcong")
                    {
                        _lk.GhichuChamcong = e.Value.ToString();
                        _lk.MarkOld();
                        _lk.IsNew = false;
                        _lk.Save();
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa ghi chú chấm công từ lịch khám của bác sĩ {0} ngày khám {1} với thông tin ghi chú từ {2} thành {3}  ", txtBacsi1.Text, _lk.Ngay.ToString("dd/MM/yyyy"), Utility.sDbnull(e.InitialValue, ""), Utility.sDbnull(e.Value, "")), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void txtBacsi__OnEnterMe()
        {
            cmdSearch.PerformClick();
        }

       

        void grdThoigian_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa thời gian khám: {0} không?", grdThoigian.GetValue("thoigian_kham").ToString()), "Cảnh báo xóa", true))
                    {
                        grdThoigian.CurrentRow.Delete();
                        dtThoigian.AcceptChanges();
                        grdThoigian.Refetch();
                        grdThoigian.AutoSizeColumns();

                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {

            }
        }
        void ModifyButtons()
        {
            cmdXoa.Enabled = Utility.isValidGrid(grdList);
            cmdSave.Enabled = m_enAct == action.Insert || (m_enAct == action.Update && _currentRow != null);
        }
        void FillTime(string dataString)
        {
          
            dtThoigian.Clear();
            if (!string.IsNullOrEmpty(dataString))
            {
                string[] rows = dataString.Split(',');
                foreach (string row in rows)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        DataRow newDr = dtThoigian.NewRow();
                        newDr["id"] = Guid.NewGuid();
                        newDr["thoigian_kham"] = Utility.sDbnull(row, "");
                        dtThoigian.Rows.Add(newDr);
                        dtThoigian.AcceptChanges();
                    }
                }
                grdThoigian.DataSource = dtThoigian;
            }
        }
        private void AddTime(string thoigian)
        {
            try
            {
                DataRow newDr = dtThoigian.NewRow();
                newDr["id"] = Guid.NewGuid();
                newDr["thoigian_kham"] = Utility.sDbnull(thoigian, "");
                dtThoigian.Rows.Add(newDr);
                dtThoigian.AcceptChanges();
                grdThoigian.AutoSizeColumns();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
            }
        }
        void txtCa__OnEnterMe()
        {
            try
            {
                DmucChung obj = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo(txtCa.LOAI_DANHMUC).And(DmucChung.Columns.Ma).IsEqualTo(txtCa.myCode).ExecuteSingle<DmucChung>();
                if (obj != null)
                {
                    List<string> lstthoigian = Utility.sDbnull(obj.MotaThem, "").Split('-').ToList<string>();
                    if (lstthoigian.Count > 0)
                        Timefrom.Text = lstthoigian[0];
                    if (lstthoigian.Count ==2)
                        timeTo.Text = lstthoigian[1];
                }
            }
            catch (Exception ex)
            {
                
               
            }
        }
        DataRow _currentRow = null;
        QlnsLichkham _lk = null;
        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList) || !AllowChanged) return;
                _currentRow = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                _lk = QlnsLichkham.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
                mnuChamcong.Enabled = Utility.ByteDbnull(_lk.TthaiLamviec, 0) == 0;
                mnuHuychamcong.Enabled = Utility.ByteDbnull(_lk.TthaiLamviec, 0) == 1;
                if (Utility.isValidGrid(grdList))
                {
                    m_enAct = action.Update;
                    FillData4Update();
                }
                else
                    ResetData();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyButtons();
            }
           
           
        }
        void FillData4Update()
        {
            try
            {
                QlnsLichkham objLichkham = QlnsLichkham.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
                if (objLichkham != null)
                {
                    txtID.Text = objLichkham.Id.ToString();
                    dtNgaykham.Value = objLichkham.Ngay;
                    txtBacsi1.SetId(objLichkham.IdBacsi);
                    txtPK.SetId(objLichkham.IdKhoaphong);
                    txtGhichu.Text = objLichkham.GhiChu;
                    FillTime(objLichkham.ThoigianKham);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void ResetData()
        {
            dtNgaykham.Value = DateTime.Now;
            txtBacsi1.SetId(-1);
            txtPK.SetId(-1);
            txtCa.SetCode("-1");
            txtGhichu.Clear();
            dtThoigian.Rows.Clear();
        }
        void frm_lichkham_Load(object sender, EventArgs e)
        {
            txtPK.Init(THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", "ALL", 0), new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            txtBacsi.Init(THU_VIEN_CHUNG.LaydanhsachBacsi(-1,-1), new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtBacsi1.Init(txtBacsi.AutoCompleteSource, txtBacsi.defaultItem);
            txtCa.Init();
            cmdSearch.PerformClick();
            AllowChanged = true;
            ModifyButtons();
        }

        
        
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uiButton5_Click(object sender, EventArgs e)
        {
            txtCa.ShowMe();
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            txtPK.ShowMe();
        }

      
        private void uiButton1_Click(object sender, EventArgs e)
        {
            AddTime(string.Format("{0}: từ {1} đến {2}",txtCa.Text,Timefrom.Text,timeTo.Text));
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            AllowChanged = false;
            m_enAct = action.Insert;
            if (!chkThemmoilientuc.Checked)
                ResetData();
            txtID.Text="-1";
            dtNgaykham.Focus();
            cmdXoa.Enabled = false;
            cmdCancel.Visible = true;
            cmdNew.Visible = false;
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkXoachon.Checked && grdList.GetCheckedRows().Count()>0)
                {
                    GridEXRow[] lstRow = grdList.GetCheckedRows();
                    if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các lịch khám đang chọn hay không?", "Xác nhận xóa", true))
                    {
                        return;
                    }
                    List<long> lstDeletedId = new List<long>();
                    List<long> lstExceptionId = new List<long>();
                    foreach (GridEXRow row in lstRow)
                    {
                        QlnsLichkham objLichkham = QlnsLichkham.FetchByID(Utility.Int64Dbnull(row.Cells["id"].Value));
                        if (objLichkham != null)
                        {
                            if (objLichkham.TthaiLamviec == 1)
                            {
                                //Utility.ShowMsg("Lịch khám bạn chọn xóa đã được chấm công nên không được phép xóa");
                                lstExceptionId.Add(objLichkham.Id);
                                continue;
                            }
                            
                                new Delete().From(QlnsLichkham.Schema).Where(QlnsLichkham.Columns.Id).IsEqualTo(objLichkham.Id).Execute();
                                DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                dtLichkham.Rows.Remove(dr);
                                dtLichkham.AcceptChanges();
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa lịch khám của bác sĩ {0} với thông tin: ngày khám {1}, thời gian khám {2} thành công ", txtBacsi1.Text, objLichkham.Ngay.ToString("dd/MM/yyyy"), objLichkham.ThoigianKham), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                lstDeletedId.Add(objLichkham.Id);
                                AllowChanged = true;
                                grdList_SelectionChanged(grdList, e);
                            }
                        
                    }

                    if (lstDeletedId.Count > 0)
                    {
                        if (lstExceptionId.Count > 0)
                            Utility.ShowMsg("Xóa lịch khám thành công. Một số lịch khám đã chấm công sẽ không được phép xóa");
                        else
                            Utility.ShowMsg("Xóa lịch khám thành công");
                    }
                    else
                    {
                        if (lstExceptionId.Count > 0)
                            Utility.ShowMsg("Các lịch khám bạn chọn đã chấm công nên không được phép xóa");
                    }
                }
                else
                {
                    QlnsLichkham objLichkham = QlnsLichkham.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
                    if (objLichkham != null)
                    {
                        if (objLichkham.TthaiLamviec == 1)
                        {
                            Utility.ShowMsg("Lịch khám bạn chọn xóa đã được chấm công nên không được phép xóa");
                            return;
                        }
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa lịch khám đang chọn hay không?", "Xác nhận xóa", true))
                        {
                            new Delete().From(QlnsLichkham.Schema).Where(QlnsLichkham.Columns.Id).IsEqualTo(objLichkham.Id).Execute();
                            DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                            dtLichkham.Rows.Remove(dr);
                            dtLichkham.AcceptChanges();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa lịch khám của bác sĩ {0} với thông tin: ngày khám {1}, thời gian khám {2} thành công ", txtBacsi1.Text, objLichkham.Ngay.ToString("dd/MM/yyyy"), objLichkham.ThoigianKham), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            Utility.ShowMsg("Xóa lịch khám thành công");
                            AllowChanged = true;
                            grdList_SelectionChanged(grdList, e);
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
                ModifyButtons();
            }
        }

        private void cmdShowBS_Click(object sender, EventArgs e)
        {
            txtBacsi.ShowMe();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dtLichkham = SPs.LichkhamTimkiem(Utility.Int16Dbnull(txtBacsi.MyID, -1), -1, dtmFrom.Text, dtmTo.Text).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdList, dtLichkham, true, true, "", "ngay,ten_bacsi");
                
            }
            catch (Exception ex) 
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBacsi1.MyID == "-1")
                {
                    Utility.ShowMsg("Bạn cần chọn bác sĩ khám");
                    txtBacsi1.Focus();
                    return;
                }
                if (txtPK.MyID == "-1")
                {
                    Utility.ShowMsg("Bạn cần chọn phòng khám");
                    txtPK.Focus();
                    return;
                }
                if (dtThoigian.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Bạn cần thêm ít nhất 1 ca khám với thời gian khám từ... đến...");
                    Timefrom.Focus();
                    return;
                }
                QlnsLichkham newItem = QlnsLichkham.FetchByID(Utility.Int64Dbnull(txtID.Text, -1));
                if (newItem == null)
                {
                    newItem = new QlnsLichkham();
                    newItem.IsNew = true;
                    newItem.NgayTao = DateTime.Now;
                    newItem.NguoiTao = globalVariables.UserName;
                }
                else
                {
                    newItem.MarkOld();
                    newItem.IsNew = false;
                    newItem.NgaySua = DateTime.Now;
                    newItem.NguoiSua = globalVariables.UserName;
                }
                newItem.Ngay = dtNgaykham.Value;
                newItem.IdBacsi = Utility.Int16Dbnull(txtBacsi1.MyID, -1);
                newItem.IdKhoaphong = Utility.Int16Dbnull(txtPK.MyID, -1);
                var q = from p in dtThoigian.AsEnumerable()
                        select Utility.sDbnull(p["thoigian_kham"], "");
                string thoigian_kham = string.Join(",", q.ToArray<string>());
                newItem.ThoigianKham = thoigian_kham;
                newItem.TuanThang = Utility.GetWeekOfMonth(newItem.Ngay);
                CultureInfo curr = CultureInfo.CurrentCulture;
                int week = curr.Calendar.GetWeekOfYear(newItem.Ngay, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                newItem.TuanNam = (Int16)week;
                newItem.Thang = (Int16)newItem.Ngay.Month;
                newItem.Thu = Utility.ConvertDayVietnamese(newItem.Ngay.DayOfWeek.ToString());
                newItem.GhiChu = Utility.sDbnull(txtGhichu.Text);
                newItem.Save();
                txtID.Text = newItem.Id.ToString();
                if (m_enAct == action.Insert)
                {
                    DataRow newRow = dtLichkham.NewRow();
                    Utility.FromObjectToDatarow(newItem, ref newRow);
                    newRow["ten_khoaphong"] = txtPK.Text;
                    newRow["ten_bacsi"] = txtBacsi1.Text;
                    newRow["sNgay"] = newItem.Ngay.ToString("dd/MM/yyyy");
                    newRow["isNew"] = 1;
                    dtLichkham.Rows.Add(newRow);
                    dtLichkham.AcceptChanges();
                    MessageBox.Show("Đã thêm mới lịch khám thành công. Nhấn Ok để kết thúc");
                    AllowChanged = true;
                    Utility.GotoNewRowJanus(grdList, "id", newItem.Id.ToString());
                }
                else
                {
                    if (_currentRow == null || _currentRow["id"].ToString() != txtID.Text)
                    {
                        _currentRow = dtLichkham.Select("id=" + txtID.Text)[0];
                    }
                    if (_currentRow != null)
                    {
                        _currentRow["ngay"] = dtNgaykham.Value;
                        _currentRow["thu"] = newItem.Thu;
                        _currentRow["tuan_nam"] = newItem.TuanNam;
                        _currentRow["tuan_thang"] = newItem.TuanThang;
                        _currentRow["thoigian_kham"] = newItem.ThoigianKham;
                        _currentRow["ghi_chu"] = newItem.GhiChu;
                        _currentRow["thang"] = newItem.Thang;
                        _currentRow["id_bacsi"] = newItem.IdBacsi;
                        _currentRow["sNgay"] = dtNgaykham.Value.ToString("dd/MM/yyyy");
                        _currentRow["id_khoaphong"] = newItem.IdKhoaphong;
                        _currentRow["ten_khoaphong"] = txtPK.Text;
                        _currentRow["ten_bacsi"] = txtBacsi1.Text;
                        _currentRow["ghi_chu"] = newItem.GhiChu;
                        dtLichkham.AcceptChanges();
                    }
                    AllowChanged = true;
                    MessageBox.Show("Đã cập nhật lịch khám thành công. Nhấn Ok để kết thúc");
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                ModifyButtons();
            }
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            txtBacsi1.ShowMe();
        }

        private void cmdSaochep_Click(object sender, EventArgs e)
        {
            long lastID = -1;
            try
            {
                if (_currentRow == null || txtBacsi1.MyID == "-1" || txtPK.MyID == "-1")
                {
                    Utility.ShowMsg("Bạn cần chọn lịch khám hoặc bác sĩ+ phòng khám trước khi sao chép");
                    return;
                }
                if (dtSaoChepNgay.SelectedDates.Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 ngày cần sao chép");
                    return;
                }
                string dates = string.Join(",", (from q in dtSaoChepNgay.SelectedDates
                                                 select q.ToString("dd/MM/yyyy")).ToArray<string>());
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn sao chép lịch làm việc của bác sĩ {0}, ngày {1} cho các ngày: {2} không?\nChú ý: Các ngày được chọn mà đã có lịch sẽ không bị thay đổi bởi thao tác này", txtBacsi1.Text, dtNgaykham.Value.ToString("dd/MM/yyyy"), dates), "Xác nhận sao chép", true))
                {
                    AllowChanged = false;
                    DateTime[] ngaythangRange = dtSaoChepNgay.SelectedDates;
                    foreach (DateTime _dtm in ngaythangRange)
                    {
                        DataTable dtCheckDate = SPs.LichkhamCheckngay(_dtm.ToString("dd/MM/yyyy"), Utility.Int16Dbnull(txtBacsi1.MyID, -1), Utility.Int16Dbnull(txtPK.MyID, -1)).GetDataSet().Tables[0];
                        if (dtCheckDate.Rows.Count <= 0)
                        {
                            QlnsLichkham newItem = new QlnsLichkham();
                            newItem.IsNew = true;
                            newItem.NgayTao = DateTime.Now;
                            newItem.NguoiTao = globalVariables.UserName;

                            newItem.Ngay = _dtm;
                            newItem.IdBacsi = Utility.Int16Dbnull(txtBacsi1.MyID, -1);
                            newItem.IdKhoaphong = Utility.Int16Dbnull(txtPK.MyID, -1);
                            var q = from p in dtThoigian.AsEnumerable()
                                    select Utility.sDbnull(p["thoigian_kham"], "");
                            string thoigian_kham = string.Join(",", q.ToArray<string>());
                            newItem.ThoigianKham = thoigian_kham;
                            newItem.TuanThang = Utility.GetWeekOfMonth(newItem.Ngay);
                            CultureInfo curr = CultureInfo.CurrentCulture;
                            int week = curr.Calendar.GetWeekOfYear(newItem.Ngay, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                            newItem.TuanNam = (Int16)week;
                            newItem.Thang = (Int16)newItem.Ngay.Month;
                            newItem.Thu = Utility.ConvertDayVietnamese(newItem.Ngay.DayOfWeek.ToString());
                            newItem.GhiChu = Utility.sDbnull(txtGhichu.Text);
                            newItem.Save();
                            lastID = newItem.Id;
                            DataRow newRow = dtLichkham.NewRow();
                            Utility.FromObjectToDatarow(newItem, ref newRow);
                            newRow["ten_khoaphong"] = txtPK.Text;
                            newRow["ten_bacsi"] = txtBacsi1.Text;
                            newRow["sNgay"] = newItem.Ngay.ToString("dd/MM/yyyy");
                            newRow["isNew"] = 1;
                            dtLichkham.Rows.Add(newRow);
                            dtLichkham.AcceptChanges();
                        }
                    }
                    Utility.ShowMsg("Đã sao chép thành công lịch khám. Nhấn OK để kết thúc");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                AllowChanged = true;
                if (lastID > 0)
                    Utility.GotoNewRowJanus(grdList, "id", lastID.ToString());
            }
        }

        private void lnkToday_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtmFrom.Value = DateTime.Now;
            dtmTo.Value = DateTime.Now;
            cmdSearch.PerformClick();
        }

        private void lnkTomorrow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtmFrom.Value = DateTime.Now.AddDays(1);
            dtmTo.Value = DateTime.Now.AddDays(1);
            cmdSearch.PerformClick();
        }

        private void lnkthisWeek_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtmFrom.Value = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            dtmTo.Value = dtmFrom.Value.AddDays(6);
            cmdSearch.PerformClick();
        }

        private void lnkMonth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtmFrom.Value = DateTime.Now;
            dtmTo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            cmdSearch.PerformClick();
        }

        private void mnuChamcong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("lichkham_chamcong"))
                {
                    Utility.thongbaokhongcoquyen("lichkham_chamcong", "chấm công");
                    return;
                }
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chấm công ngày {0} của bác sĩ {1} thành công", grdList.GetValue("sNgay").ToString(), grdList.GetValue("ten_bacsi").ToString()), "Xác nhận chấm công", true))
                {
                    return;
                }
                if (_lk == null)
                    _lk = QlnsLichkham.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
                if (_lk == null)
                {
                    Utility.ShowMsg("Lịch khám bạn đang chọn có thể không tồn tại(do người khác xóa trong lúc bạn chưa kịp thực hiện). Vui lòng nhấn tìm kiếm lại hoặc trao đổi thông tin nội bộ để biết thêm chi tiết");
                    return;
                }
                _lk.TthaiLamviec = 1;
                if (_lk.NgayTao == null)
                {
                    _lk.NguoiChamcong= globalVariables.UserName;
                    _lk.NgayChamcong = DateTime.Now;
                }
                else
                {
                    _lk.NguoisuaChamcong = globalVariables.UserName;
                    _lk.NgaysuaChamcong = DateTime.Now;
                }
                _lk.Save();
                _currentRow["tthai_lamviec"] = 1;
                dtLichkham.AcceptChanges();
                Utility.ShowMsg(string.Format("Đã xác nhận ngày công {0} của bác sĩ {1} thành công", grdList.GetValue("sNgay").ToString(), grdList.GetValue("ten_bacsi").ToString()));
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           

        }

        private void mnuHuychamcong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("lichkham_huychamcong"))
                {
                    Utility.thongbaokhongcoquyen("lichkham_huychamcong", "hủy chấm công");
                    return;
                }
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy chấm công ngày {0} của bác sĩ {1} thành công", grdList.GetValue("sNgay").ToString(), grdList.GetValue("ten_bacsi").ToString()), "Xác nhận hủy chấm công", true))
                {
                    return;
                }
                if (_lk == null)
                    _lk = QlnsLichkham.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
                if (_lk == null)
                {
                    Utility.ShowMsg("Lịch khám bạn đang chọn có thể không tồn tại(do người khác xóa trong lúc bạn chưa kịp thực hiện). Vui lòng nhấn tìm kiếm lại hoặc trao đổi thông tin nội bộ để biết thêm chi tiết");
                    return;
                }
                _lk.TthaiLamviec = 0;

                _lk.NguoiChamcong = "";
                _lk.NgayChamcong = null;

                _lk.NguoisuaChamcong = "";
                _lk.NgaysuaChamcong = null;

                _lk.Save();
                _currentRow["tthai_lamviec"] = 1;
                _currentRow["ghichu_chamcong"] = "";
                dtLichkham.AcceptChanges();
                Utility.ShowMsg(string.Format("Đã hủy xác nhận ngày công {0} của bác sĩ {1} thành công", grdList.GetValue("sNgay").ToString(), grdList.GetValue("ten_bacsi").ToString()));
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdChamcong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("lichkham_chamcong"))
                {
                    Utility.thongbaokhongcoquyen("lichkham_chamcong", "chấm công");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn chấm công cho toàn bộ các ngày khám của các bác sĩ đang chọn", "Xác nhận chấm công", true))
                {
                    foreach (GridEXRow _row in grdList.GetCheckedRows())
                    {

                        QlnsLichkham _item = QlnsLichkham.FetchByID(Utility.Int64Dbnull(_row.Cells["id"].Value));
                        if (_item.TthaiLamviec == 0)
                        {
                            _item.TthaiLamviec = 1;
                            if (_item.NgayTao == null)
                            {
                                _item.NguoiChamcong = globalVariables.UserName;
                                _item.NgayChamcong = DateTime.Now;
                            }
                            else
                            {
                                _item.NguoisuaChamcong = globalVariables.UserName;
                                _item.NgaysuaChamcong = DateTime.Now;
                            }
                            _item.Save();
                            DataRow dr = ((DataRowView)_row.DataRow).Row;
                            dr["tthai_lamviec"] = 1;
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chấm công cho lịch khám id={0},ngày {1}, bác sĩ {2} thành công ", _item.Id, _item.Ngay, _item.IdBacsi), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                        }

                    }
                    dtLichkham.AcceptChanges();
                    Utility.ShowMsg(string.Format("Đã xác nhận ngày công cho các lịch khám được chọn thành công. Nhấn OK để kết thúc"));
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void lnk7ngaytruoc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtmFrom.Value = DateTime.Now.AddDays(-7);
            dtmTo.Value = DateTime.Now;  
            cmdSearch.PerformClick();
        }

        private void lnk7ngayketiep_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtmFrom.Value = DateTime.Now;
            dtmTo.Value = DateTime.Now.AddDays(7);
            cmdSearch.PerformClick();
        }

        private void lnkTuantruoc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtmFrom.Value = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(-7);
            dtmTo.Value = dtmFrom.Value.AddDays(6);
            cmdSearch.PerformClick();
        }

        private void cmdHuychamcong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("lichkham_huychamcong"))
                {
                    Utility.thongbaokhongcoquyen("lichkham_huychamcong", "hủy chấm công");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy chấm công cho các lịch khám đang chọn?", "Xác nhận chấm công", true))
                {
                    foreach (GridEXRow _row in grdList.GetCheckedRows())
                    {

                        QlnsLichkham _item = QlnsLichkham.FetchByID(Utility.Int64Dbnull(_row.Cells["id"].Value));
                        if (_item.TthaiLamviec == 1)
                        {
                            _item.TthaiLamviec = 0;
                            if (_item.NgayTao == null)
                            {
                                _item.NguoiChamcong = globalVariables.UserName;
                                _item.NgayChamcong = DateTime.Now;
                            }
                            else
                            {
                                _item.NguoisuaChamcong = globalVariables.UserName;
                                _item.NgaysuaChamcong = DateTime.Now;
                            }
                            _item.Save();
                            DataRow dr = ((DataRowView)_row.DataRow).Row;
                            dr["tthai_lamviec"] = 0;
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy chấm công cho lịch khám id={0},ngày {1}, bác sĩ {2} thành công ", _item.Id, _item.Ngay, _item.IdBacsi), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                        }

                    }
                    dtLichkham.AcceptChanges();
                    Utility.ShowMsg(string.Format("Đã hủy xác nhận ngày công cho các lịch khám được chọn thành công. Nhấn OK để kết thúc"));
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            AllowChanged = true;
            m_enAct = action.Normal;
            txtID.Text = "-1";
            dtNgaykham.Focus();
            cmdCancel.Visible = false;
            cmdNew.Visible = true;
            grdList_SelectionChanged(grdList, e);
        }

        
    }
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
