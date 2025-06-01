using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;

using VietBaIT.HISLink.DataAccessLayer;
using VietBaIT.HISLink.LoadDataCommon;
using VietBaIT.HISLink.UI.ControlUtility;
//using VietBaIT.HISLink.UI.Duoc.FormConfig;
//using VietBaIT.HISLink.UI.Duoc.Form_TAMPHUC_Duoc;
using SortOrder = Janus.Windows.GridEX.SortOrder;

namespace VietBaIT.HISLink.UI.Duoc.Form_NghiepVu
{
    public partial class frm_DieuTiet_Thau : Form
    {
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath,
                                                         string.Format("SplitterDistancefrm_DieuTiet_Thau.txt"));

        private string kieuvattuthuoc = "TATCA";
        private  AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();

        private int Distance = 488;
        private HisDuocProperties HisDuocProperties;
        private bool b_Hasloaded = false;
        private int id_PhieuNhap;
        private DataTable m_dtDieuTietThau = new DataTable();
        private DataTable m_dtDieuTietThauChiTiet = new DataTable();

        public frm_DieuTiet_Thau(string skieuvattuthuoc)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            toolStrip1.BackColor = globalVariables.SystemColor;
            Utility.loadIconToForm(this);
            dtFromdate.Value = DateTime.Now.AddMonths(-1);
            dtToDate.Value = DateTime.Now;
            kieuvattuthuoc = skieuvattuthuoc;
            KeyDown += frm_DieuTiet_Thau_KeyDown;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            grdDieuTietThau.ApplyingFilter += grdList_ApplyingFilter;
            grdDieuTietThau.SelectionChanged += grdList_SelectionChanged;
            ReadSliper();
        }
        public frm_DieuTiet_Thau()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            toolStrip1.BackColor = globalVariables.SystemColor;
            Utility.loadIconToForm(this);
            dtFromdate.Value = DateTime.Now.AddMonths(-1);
            dtToDate.Value = DateTime.Now;
           // kieuvattuthuoc = skieuvattuthuoc;
            KeyDown += frm_DieuTiet_Thau_KeyDown;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            grdDieuTietThau.ApplyingFilter += grdList_ApplyingFilter;
            grdDieuTietThau.SelectionChanged += grdList_SelectionChanged;
            ReadSliper();
        }
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_DieuTiet_Thau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && e.Control) cmdThem.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdUpdate.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoa.PerformClick();
            if (e.KeyCode == Keys.F4) cmdIn.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.F12 && e.Control&&e.Shift)
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        }

        /// <summary>
        /// hàm thực hiện việc lưu kéo ra kheo vào của cửa sổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!b_Hasloaded) return;
            SplitterDistance = splitContainer1.SplitterDistance;
            if (File.Exists(FileName))
            {
                WriteSlipterContaiter();
            }
        }

        /// <summary>
        /// hàm thực hiên viecj viết thông tin của sliper khi di chuyển vào file text
        /// </summary>
        private void WriteSlipterContaiter()
        {
            SplitterDistance = splitContainer1.SplitterDistance;
            File.WriteAllText(FileName, SplitterDistance.ToString());
        }

        private void ReadSliper()
        {
            if (File.Exists(FileName))
            {
                SplitterDistance = Utility.Int32Dbnull(File.ReadAllLines(FileName)[0]);
            }
            else
            {
                WriteSlipterContaiter();
            }
            splitContainer1.SplitterDistance = SplitterDistance;
        }
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TIMKIEM_THONGTIN();
        }
        private void TIMKIEM_THONGTIN()
        {
            grdDieuTietThau.DataSource = null;
            grdChiTiet.DataSource = null;
            int trangThai = -1;
            if (radDaNhap.Checked) trangThai = 1;
            if (radChuaNhapKho.Checked) trangThai = 0;
            if (radTatCa.Checked) trangThai = -1;


            m_dtDieuTietThau = SPs.SpLayDanhSachDieuTietThau(
                                                                chkByDate.Checked ? dtFromdate.Value : Convert.ToDateTime("01/01/1900"),
                                                                chkByDate.Checked ? dtToDate.Value : DateTime.Now,
                                                                Utility.sDbnull(txtSoThau.Text), Utility.sDbnull(txtSoQDinh.Text), trangThai,
                                                                Utility.sDbnull(txtTenThuoc1.Text))
                                    .GetDataSet().Tables[0];

            Utility.SetDataSourceForDataGridEx(grdDieuTietThau, m_dtDieuTietThau, true, true, "1=1", "");
            Utility.SetGridEXSortKey(grdDieuTietThau, DDieuTietThau.Columns.IdDieutiet, SortOrder.Ascending);
            ModifyCommnad();
        }
        private void ModifyCommnad()
        {
            bool isValidGid = grdDieuTietThau.RowCount > 0 && grdDieuTietThau.CurrentRow != null &&
                              grdDieuTietThau.CurrentRow.RowType == RowType.Record;
            bool trangthai = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.TrangThai), 0) == 0;
            bool chuaxacnhan = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.DaXacNhan), 0) == 0;
            cmdUpdate.Enabled = cmdXoa.Enabled = chuaxacnhan && trangthai && isValidGid;
            cmdXacNhan.Enabled = chuaxacnhan && isValidGid;
            cmdHuyXacNhan.Enabled = !cmdXacNhan.Enabled;
            cmdIn.Enabled = isValidGid;
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }

        private bool InValiUpdateXoa()
        {
            int IdDieuTiet = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.IdDieutiet), -1);
            SqlQuery sqlQuery = new Select().From(DDieuTietThau.Schema).Where(DDieuTietThau.Columns.TrangThai).IsEqualTo(1).And(DDieuTietThau.Columns.IdDieutiet).IsEqualTo(IdDieuTiet);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bản ghi đã Xác nhận. Bạn không thể sửa hoặc xóa thông tin", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            int IdThau = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.IdDieutiet), -1);
            if (!InValiUpdateXoa()) return;
            //if(!InValiXacNhan())return;
            if (Utility.AcceptQuestion(string.Format("Bạn có muốn xóa điều tiết thầu {0}.", IdThau), "Thông báo", true))
            {
                new Update(DDieuTietThau.Schema).Set(DDieuTietThau.Columns.TrangThai).EqualTo(1).Where(DDieuTietThau.Columns.IdDieutiet).IsEqualTo(IdThau).Execute();
                grdDieuTietThau.CurrentRow.Delete();
                grdDieuTietThau.UpdateData();
                m_dtDieuTietThau.AcceptChanges();
                HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Xóa điều tiết thầu: {0}", IdThau), action.Delete, "UI");
                Utility.ShowMsg("Bạn xóa điều tiết thầu thành công", "Thông báo", MessageBoxIcon.Information);
            }
            ModifyCommnad();
        }

        private void AddAutoCompleteThuoc()
        {
            DataTable m_dtthuoc = CommonLoadDuoc.LayThongTinThuoc();
            txtTenThuoc1.DataSource = m_dtthuoc;
            txtTenThuoc1.DataMember = LDrug.Columns.DrugName;
            txtTenThuoc1.ValueMember = LDrug.Columns.DrugName;
            txtTenThuoc1.DisplayMember = LDrug.Columns.DrugName;
        }

        private void frm_DieuTiet_Thau_Load(object sender, EventArgs e)
        {
            AddAutoCompleteThuoc();
            TIMKIEM_THONGTIN();
            ModifyCommnad();
        }
        private void cmdInPhieuNhapKho_Click(object sender, EventArgs e)
        {
            try
            {
                id_PhieuNhap = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.IdDieutiet), -1);
            }
            catch (Exception)
            {
            }
        }
        private bool InValiXacNhan()
        {
            int IdThau = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.IdDieutiet), -1);
            SqlQuery sqlQuery = new Select().From<DDieuTietThau>().Where(DDieuTietThau.Columns.IdDieutiet).IsEqualTo(IdThau).And(DDieuTietThau.Columns.TrangThai).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bản ghi đã được xác nhận.\nMời bạn lấy lại dữ liệu", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void cmdXacNhan_Click(object sender, EventArgs e)
        {
            cmdXacNhan.Enabled = false;
            if (!InValiXacNhan()) return;
            int IdDieuTiet = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.IdDieutiet), -1);
            try
            {
                DDieuTietThau objDDieuTietThau = DDieuTietThau.FetchByID(IdDieuTiet);
                if (objDDieuTietThau != null)
                {
                    objDDieuTietThau.DaXacNhan = true;
                    objDDieuTietThau.NgayXacNhan = DateTime.Now;
                    objDDieuTietThau.NguoiXacNhan = globalVariables.UserName;
                    objDDieuTietThau.IsNew = false;
                    objDDieuTietThau.MarkOld();
                    objDDieuTietThau.Save();

                    foreach (GridEXRow item in grdChiTiet.GetDataRows())
                    {
                        int idThauct = Utility.Int32Dbnull(item.Cells[DDieuTietThauCt.Columns.IdThauCt].Value, -1);
                        int slDieuTiet = Utility.Int32Dbnull(item.Cells[DDieuTietThauCt.Columns.SoLuong].Value, 0);

                        DThongTinThauChiTiet chiTietThau = DThongTinThauChiTiet.FetchByID(idThauct);
                        if (chiTietThau != null)
                        {
                            chiTietThau.SlDaNhap = Utility.Int32Dbnull(chiTietThau.SlDaNhap, 0) + slDieuTiet;
                            chiTietThau.SlDieuTiet = Utility.Int32Dbnull(chiTietThau.SlDieuTiet, 0) + slDieuTiet;
                            chiTietThau.MarkOld();
                            chiTietThau.IsLoaded = true;
                            chiTietThau.Save();
                        }
                    }

                    grdDieuTietThau.CurrentRow.Cells["Da_Xac_Nhan"].Value = 1;
                    grdDieuTietThau.UpdateData();
                    m_dtDieuTietThau.AcceptChanges();
                    Utility.ShowMsg("Xác nhận thành công.", "Thông báo", MessageBoxIcon.Information);
                    HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Xác nhận điều tiết thầu: {0}", IdDieuTiet), action.Update, "UI");
                    cmdHuyXacNhan.Enabled = true;
                }
                else
                {
                    Utility.ShowMsg("Không tìm thấy điều tiết thầu trong CSDL.\nMời bạn lấy lại dữ liệu", "Thông báo", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(string.Format("Lỗi: {0}", ex.Message), "Thông báo", MessageBoxIcon.Information);
                cmdXacNhan.Enabled = true;
            }
        }
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommnad();
        }
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (grdDieuTietThau.CurrentRow != null && grdDieuTietThau.CurrentRow.RowType == RowType.Record)
            {
                int IdThau = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.IdDieutiet), -1);
                m_dtDieuTietThauChiTiet = SPs.SpChiTietDieuTietThau(IdThau).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChiTiet, m_dtDieuTietThauChiTiet, false, true, "1=1", "");
            }
            else
            {
                grdChiTiet.DataSource = null;
            }
            ModifyCommnad();
        }
        private void cmdThem_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frm_Add_DieuTiet_Thau(kieuvattuthuoc);
                frm.EmAction = action.Insert;
                frm.dtThongTin_DieuTiet = m_dtDieuTietThau;
                frm.GrdList = grdDieuTietThau;
                frm.txtID_DieuTiet.Text = "-1";
                frm.ShowDialog();
                if (frm.BCancel)
                {
                    grdList_SelectionChanged(grdDieuTietThau, new EventArgs());
                }
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
                ModifyCommnad();
            }
        }
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!InValiUpdateXoa()) return;
                if (!InValiXacNhan()) return;
                int IdThau = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.IdDieutiet), -1);
                var frm = new frm_Add_DieuTiet_Thau(kieuvattuthuoc);
                frm.EmAction = action.Update;
                frm.GrdList = grdDieuTietThau;
                frm.dtThongTin_DieuTiet = m_dtDieuTietThau;
                frm.txtID_DieuTiet.Text = Utility.sDbnull(IdThau);

                frm.ShowDialog();
                if (frm.BCancel)
                {
                    HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Sửa điều tiết thầu: {0}", IdThau), action.Update, "UI");
                    grdList_SelectionChanged(grdDieuTietThau, new EventArgs());
                }
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
                ModifyCommnad();
            }
        }
        private void txtSoThau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSearch.PerformClick();
            }
        }
        private bool InValiHuyXacNhan()
        {
            int IdThau = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.IdDieutiet), -1);
            SqlQuery sqlQuery = new Select().From<DDieuTietThau>().Where(DDieuTietThau.Columns.IdDieutiet).IsEqualTo(IdThau).And(DDieuTietThau.Columns.DaXacNhan).IsEqualTo(0);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bản ghi chưa xác nhận để thực hiện hủy.\nMời bạn lấy lại dữ liệu", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void cmdHuyXacNhan_Click(object sender, EventArgs e)
        {
            if (!InValiHuyXacNhan()) return;
            if (Utility.AcceptQuestion("Bạn có chắc chắn hủy xác nhận phiếu điều tiết này không?", "Thông báo", true))
            {
                cmdHuyXacNhan.Enabled = false;
                int IdThau = Utility.Int32Dbnull(grdDieuTietThau.GetValue(DDieuTietThau.Columns.IdDieutiet), -1);
                try
                {
                    DDieuTietThau objDDieuTietThau = DDieuTietThau.FetchByID(IdThau);
                    if (objDDieuTietThau != null)
                    {
                        objDDieuTietThau.DaXacNhan = false;
                        objDDieuTietThau.NgayXacNhan = null;
                        objDDieuTietThau.NguoiXacNhan = string.Empty;
                        objDDieuTietThau.IsNew = false;
                        objDDieuTietThau.MarkOld();
                        objDDieuTietThau.Save();

                        foreach (GridEXRow item in grdChiTiet.GetDataRows())
                        {
                            int idThauct = Utility.Int32Dbnull(item.Cells[DDieuTietThauCt.Columns.IdThauCt].Value, -1);
                            int slDieuTiet = Utility.Int32Dbnull(item.Cells[DDieuTietThauCt.Columns.SoLuong].Value, 0);

                            DThongTinThauChiTiet chiTietThau = DThongTinThauChiTiet.FetchByID(idThauct);
                            if (chiTietThau != null)
                            {
                                chiTietThau.SlDaNhap = Utility.Int32Dbnull(chiTietThau.SlDaNhap, 0) - slDieuTiet;
                                chiTietThau.SlDieuTiet = Utility.Int32Dbnull(chiTietThau.SlDieuTiet, 0) - slDieuTiet;
                                chiTietThau.MarkOld();
                                chiTietThau.IsLoaded = true;
                                chiTietThau.Save();
                            }
                        }

                        grdDieuTietThau.CurrentRow.Cells["Da_Xac_Nhan"].Value = 0;
                        grdDieuTietThau.UpdateData();
                        m_dtDieuTietThau.AcceptChanges();
                        Utility.ShowMsg("Hủy Xác nhận thành công.", "Thông báo", MessageBoxIcon.Information);
                        HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Hủy Xác nhận điều tiết thầu: {0}", IdThau), action.Update, "UI");
                        cmdXacNhan.Enabled = true;
                    }
                    else
                    {
                        Utility.ShowMsg("Không tìm thấy điều tiết thầu trong CSDL.\nMời bạn lấy lại dữ liệu", "Thông báo", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg(string.Format("Lỗi: {0}", ex.Message), "Thông báo", MessageBoxIcon.Information);
                    cmdXacNhan.Enabled = true;
                }
            }
        }

        private void tsmDieuTiet_Click(object sender, EventArgs e)
        {

        }
    }
}