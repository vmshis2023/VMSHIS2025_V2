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
using VNS.HIS.UI.DANHMUC;

namespace VNS.HISLink.UI.Duoc.Form_DanhMuc
{
    public partial class frm_Danhmuc_HoatChat : Form
    {
        private bool _bloadSuccess = false;
        public frm_Danhmuc_HoatChat()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this); 
            txtmucdotuongtac._OnShowData += txtmucdotuongtac__OnShowData;
            txtmucdo2._OnShowData += txtmucdotuongtac__OnShowData;
            txtmucdo3._OnShowData += txtmucdotuongtac__OnShowData;
            txtdotuoi._OnShowData += txtdotuoi__OnShowData;
            grdHoatChat.UpdatingCell += grdHoatChat_UpdatingCell;
            
        }

        void grdHoatChat_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == "Ma_HoatChat")
                {
                    DmucHoatchat objHC = DmucHoatchat.FetchByID(Utility.Int32Dbnull(grdHoatChat.GetValue("id_hoatchat")));
                    if (objHC != null)
                    {
                        objHC.MaHoatchat =Utility.sDbnull( e.Value);
                        objHC.MarkOld();
                        objHC.Save();
                        Utility.ShowMsg("Sửa mã hoạt chất thành công");
                    }
                }
            }
            catch (Exception)
            {
                
               
            }
            
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtdotuoi__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txtdotuoi.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtdotuoi.myCode;
                txtdotuoi.Init();
                txtdotuoi.SetCode(oldCode);
                txtdotuoi.Focus();
            }
        }
        private void txtmucdotuongtac__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txtmucdotuongtac.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtmucdotuongtac.myCode;
                txtmucdotuongtac.Init();
                txtmucdotuongtac.SetCode(oldCode);
                txtmucdotuongtac.Focus();
            }
        }
        private bool InVali()
        {
            if (grdHoatChat.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một thông tin để thực hiện xóa", "Thông báo", MessageBoxIcon.Warning);
                grdHoatChat.Focus();
                return false;
            }
            bool bExist = false;
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdHoatChat.GetCheckedRows())
            {
                string MaHoatchat = Utility.sDbnull(gridExRow.Cells[DmucHoatchat.Columns.MaHoatchat].Value);
                SqlQuery sqlQuery = new Select().From(DmucThuoc.Schema).Where(DmucThuoc.Columns.MaHoatchat).IsEqualTo(MaHoatchat);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    bExist = true;
                    break;
                }
            }
            if (bExist)
            {
                Utility.ShowMsg("Bản ghi tồn tại trong bảng L_drugs, bạn không thể xóa", "Thông báo", MessageBoxIcon.Warning);
                grdHoatChat.Focus();
                return false;
            }
            return true;
        }

        private DataTable m_dtHoatChat;
        void ModifyCommand()
        {
            try
            {
                cmdSua.Enabled = cmdXoa.Enabled = grdHoatChat.RowCount > 0 && grdHoatChat.CurrentRow.RowType == RowType.Record;
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!InVali()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin  không", "Thông báo", true))
                {
                    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdHoatChat.GetCheckedRows())
                    {
                        int record =
                            new Delete().From(DmucHoatchat.Schema)
                                .Where(DmucHoatchat.Columns.IdHoatchat)
                                .IsEqualTo(Utility.Int32Dbnull(gridExRow.Cells[DmucHoatchat.Columns.IdHoatchat].Value, -1))
                                .Execute();

                        if (record > 0)
                        {
                            gridExRow.Delete();
                        }
                    }

                    grdHoatChat.UpdateData();
                    grdHoatChat.Refresh();

                    m_dtHoatChat.AcceptChanges();
                    Utility.ShowMsg("Bạn xóa thông tin thành công", "thông báo");
                    ModifyCommand();

                }

            }
            catch (Exception exception)
            {

            }
        }

        private void frm_DanhMuc_DuongDung_Load(object sender, EventArgs e)
        {
            grptuongtac.Width = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_TAOQHE_TUONGTAC","0", true)=="1" && (globalVariables.IsAdmin || Utility.Coquyen("duoc_tuongtac")) ? 548 : 0;
            txtmucdotuongtac.Init();
            txtmucdo2.Init();
            txtmucdo3.Init();
            txtdotuoi.Init();
            getData();
            txtmucdotuongtac.Init();
            _bloadSuccess = true; 
        }

        private void getData()
        {
            m_dtHoatChat = new Select().From(DmucHoatchat.Schema).ExecuteDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdHoatChat, m_dtHoatChat, true, true, "1=1", "");
            txthoatchat2.Init(m_dtHoatChat, new List<string> { DmucHoatchat.Columns.IdHoatchat, DmucHoatchat.Columns.MaHoatchat, DmucHoatchat.Columns.TenHoatchat });
            ModifyCommand();
        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_hoatchat frm = new frm_themmoi_hoatchat();
                frm.em_action = action.Insert;
                frm.grdList = grdHoatChat;
                frm.p_dtHoatChat = m_dtHoatChat;
                frm.ShowDialog();
            }
            catch (Exception)
            {

                // throw;
            }
            finally
            {
                ModifyCommand();
            }
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_hoatchat frm = new frm_themmoi_hoatchat();
                frm.em_action = action.Update;
                frm.txtIdHoatChat.Text = Utility.sDbnull(grdHoatChat.GetValue(DmucHoatchat.Columns.IdHoatchat));
                frm.grdList = grdHoatChat;
                frm.p_dtHoatChat = m_dtHoatChat;
                frm.ShowDialog();
            }
            catch (Exception)
            {

                // throw;
            }
            finally
            {
                ModifyCommand();
            }
        }
        DataTable dtThuocThuoc;
        DataTable dtThuocBenh;
        DataTable dtThuocTuoi;
        private void gettuongtac(string maHoatChat)
        {
            try
            {
                if (grptuongtac.Width <=0) return;
                dtThuocThuoc = SPs.QheTuongtacLaydanhsach(0, maHoatChat).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdthuocthuoc, dtThuocThuoc, true, true, "", "");
                dtThuocBenh = SPs.QheTuongtacLaydanhsach(1, maHoatChat).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdthuocbenh, dtThuocBenh, true, true, "", "");
                dtThuocTuoi = SPs.QheTuongtacLaydanhsach(2, maHoatChat).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdthuoctuoi, dtThuocTuoi, true, true, "", "");
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
            

        }
        private void grdHoatChat_SelectionChanged(object sender, EventArgs e)
        {
            ModifyCommand();  
            if(_bloadSuccess)
            {
                if (!Utility.isValidGrid(grdHoatChat)) return;
                string mahoatchat = Utility.sDbnull(grdHoatChat.CurrentRow.Cells[DmucHoatchat.Columns.MaHoatchat].Value);
                gettuongtac(mahoatchat);
            }
        }

        private void grdHoatChat_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void frm_Danhmuc_HoatChat_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.N && e.Control) cmdThemMoi.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdSua.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoa.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdThoat.PerformClick();
        }


        private void ThemDulieuLenluoi()
        {
          
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            { 
                if(Utility.sDbnull(txtMaBoYte2.Text, "") == "")
                {
                    Utility.ShowMsg("Mã tương tác không được để trống", "Cảnh báo", MessageBoxIcon.Warning);
                    txtMaBoYte2.Focus();
                    return;
                }
                if (Utility.sDbnull(txthoatchat2.MyCode, "") == "")
                {
                    Utility.ShowMsg("Mã tương tác không được để trống", "Cảnh báo", MessageBoxIcon.Warning);
                    txthoatchat2.Focus();
                    return;
                }
                string mahoatchat = Utility.sDbnull(grdHoatChat.CurrentRow.Cells["Ma_HoatChat"].Value);
                SqlQuery sqlkt = new Select().From(QheTuongtacThuoc.Schema).Where(QheTuongtacThuoc.Columns.MaHoatchat).IsEqualTo(mahoatchat)
                        .And(QheTuongtacThuoc.Columns.MaTuongtac).IsEqualTo(Utility.sDbnull(txtMaBoYte2.Text))
                        .And(QheTuongtacThuoc.Columns.Loai).IsEqualTo(0);
                if (sqlkt.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Đã tồn tại bản ghi tương tác thuốc tương ứng", "Cảnh báo", MessageBoxIcon.Warning);
                    txtMaBoYte2.Focus();
                    return;
                }
                QheTuongtacThuoc objtuongtac = new QheTuongtacThuoc();
                objtuongtac.MaHoatchat = mahoatchat;
                objtuongtac.Loai = 0;
                objtuongtac.MaTuongtac = Utility.sDbnull(txthoatchat2.MyCode, "");
                objtuongtac.CoChe = Utility.sDbnull(txtcoche.Text, "");
                objtuongtac.NoiDung = Utility.sDbnull(txtnoidung.Text.Trim());
                objtuongtac.XuTri = Utility.sDbnull(txtxutri.Text.Trim());
                objtuongtac.HauQua = Utility.sDbnull(txthauqua.Text.Trim());
                objtuongtac.MucDo = Utility.sDbnull(txtmucdotuongtac.Text, "");
                objtuongtac.NguoiTao = globalVariables.UserName;
                objtuongtac.NgayTao = globalVariables.SysDate;
                objtuongtac.IsNew = true;
                objtuongtac.Save();
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới tương tác thuốc/thuốc {0}/ {1}", mahoatchat, Utility.sDbnull(txtMaBoYte2.Text)), newaction.Insert, "UI");
                DataRow dr = dtThuocThuoc.NewRow();
                dr["ma_hoatchat"] = mahoatchat;
                dr["ma_tuongtac"] = Utility.sDbnull(txthoatchat2.MyCode, "");
                dr["ten_tuongtac"] = Utility.sDbnull(txthoatchat2.Text, "");
                dr["co_che"] = Utility.sDbnull(txtcoche.Text, "");
                dr["hau_qua"] = Utility.sDbnull(txthauqua.Text.Trim());
                dr["xu_tri"] = Utility.sDbnull(txtxutri.Text.Trim());
                dr["muc_do"] = Utility.sDbnull(txtmucdotuongtac.Text, "");
                dr["noi_dung"] = Utility.sDbnull(txtnoidung.Text.Trim());
                dtThuocThuoc.Rows.Add(dr);
                dtThuocThuoc.AcceptChanges();
            }
            catch(Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            { 
                foreach(GridEXRow row in grdthuocthuoc.GetCheckedRows())
                {
                    string mahoatchat = Utility.sDbnull(row.Cells["Ma_HoatChat"].Value);
                    string matuongtac = Utility.sDbnull(row.Cells["ma_tuongtac"].Value);
                    new Delete().From(QheTuongtacThuoc.Schema).Where(QheTuongtacThuoc.Columns.MaHoatchat).IsEqualTo(mahoatchat)
                        .And(QheTuongtacThuoc.Columns.MaTuongtac).IsEqualTo(matuongtac)
                        .And(QheTuongtacThuoc.Columns.Loai).IsEqualTo(0) 
                        .Execute();
                    row.Delete();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa tương tác thuốc/thuốc {0}/ {1}", mahoatchat, matuongtac), newaction.Delete, "UI");
                }
               
            }
            catch(Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private bool _bLoadMaHoatchat = false;
        private void txtMaBoYte2_TextChanged(object sender, EventArgs e)
        {
            if (_bLoadMaHoatchat) return;
            DataTable dthoatchatnew = m_dtHoatChat.Copy();
            if (!string.IsNullOrEmpty(txtMaBoYte2.Text))
            {
                string rowfiler = string.Format("{0}='{1}'", DmucHoatchat.Columns.MaHoatchat, txtMaBoYte2.Text);
                dthoatchatnew.DefaultView.RowFilter = rowfiler;
            }
            dthoatchatnew.AcceptChanges();
            if (!string.IsNullOrEmpty(txtMaBoYte2.Text))
            {

                var query = from thuoc in dthoatchatnew.AsEnumerable()
                            let y = Utility.sDbnull(thuoc[DmucHoatchat.Columns.IdHoatchat])
                            where Utility.sDbnull(thuoc[DmucHoatchat.Columns.MaHoatchat]) == txtMaBoYte2.Text.Trim()
                            select y;
                if (query.Any())
                {
                    var firstrow = query.FirstOrDefault();
                    if (firstrow != null)
                    {
                        txthoatchat2.SetId(Utility.sDbnull(firstrow));
                    }
                }
            }
        }

        private void txthoatchat2__OnEnterMe()
        {
            _bLoadMaHoatchat = true;
            txtMaBoYte2.Text = Utility.sDbnull(txthoatchat2.MyCode);
            _bLoadMaHoatchat = false;
        }
        private bool hasMorethanOne = true;
        private bool isLike = true;
        private bool _searchmabenh = false;
        private string TEN_BENHPHU = "";
        private DataTable dt_ICD = new DataTable();
        private void txtAuxDieaseCode_TextChanged(object sender, EventArgs e)
        {
            //hasMorethanOne = true;
            //DataRow[] arrDr;
            //if (isLike && !_searchmabenh)
            //    arrDr =
            //        globalVariables.g_dtDiseaseList.Select("Disease_Code like '" + Utility.sDbnull(txtAuxDieaseCode.Text, "") +
            //                                               "%'");
            //else
            //    arrDr =
            //        globalVariables.g_dtDiseaseList.Select("Disease_Code = '" + Utility.sDbnull(txtAuxDieaseCode.Text, "") +   "'");
            //if (!string.IsNullOrEmpty(txtAuxDieaseCode.Text))
            //{
            //    if (arrDr.GetLength(0) == 1)
            //    {
            //        hasMorethanOne = false;
            //        txtAuxDieaseCode.Text = arrDr[0][LDisease.Columns.DiseaseCode].ToString();
            //        txtTenBenhPhu.Text = Utility.sDbnull(arrDr[0][LDisease.Columns.DiseaseName], "");
            //        TEN_BENHPHU = txtTenBenhPhu.Text;
            //    } 
            //    else if (arrDr.GetLength(0) >1)
            //    {
            //          arrDr = globalVariables.g_dtDiseaseList.Select("Disease_Code = '" + Utility.sDbnull(txtAuxDieaseCode.Text, "") +   "'");
            //        if (arrDr.GetLength(0) == 1)
            //        {
            //            hasMorethanOne = false;
            //            txtAuxDieaseCode.Text = arrDr[0][LDisease.Columns.DiseaseCode].ToString();
            //            txtTenBenhPhu.Text = Utility.sDbnull(arrDr[0][LDisease.Columns.DiseaseName], "");
            //            TEN_BENHPHU = txtTenBenhPhu.Text;
            //        }
            //    }
            //    else if (arrDr.GetLength(0) <=0) 
            //    {
            //        txtTenBenhPhu.Text = "";
            //        TEN_BENHPHU = "";
            //    }
            //}
            //else
            //{
            //    txtAuxDieaseCode.Text = "";
            //    TEN_BENHPHU = "";
            //}
        }

        private void cmdluu2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.sDbnull(txtAuxDieaseCode.Text, "") == "")
                {
                    Utility.ShowMsg("Mã tương tác không được để trống", "Cảnh báo", MessageBoxIcon.Warning);
                    txtAuxDieaseCode.Focus();
                    return;
                }
                if (Utility.sDbnull(txtTenBenhPhu.Text, "") == "")
                {
                    Utility.ShowMsg("Mã tương tác không được để trống", "Cảnh báo", MessageBoxIcon.Warning);
                    txtTenBenhPhu.Focus();
                    return;
                }
                string mahoatchat = Utility.sDbnull(grdHoatChat.CurrentRow.Cells["Ma_HoatChat"].Value);
                SqlQuery sqlkt = new Select().From(QheTuongtacThuoc.Schema).Where(QheTuongtacThuoc.Columns.MaHoatchat).IsEqualTo(mahoatchat)
                        .And(QheTuongtacThuoc.Columns.MaTuongtac).IsEqualTo(Utility.sDbnull(txtAuxDieaseCode.Text))
                        .And(QheTuongtacThuoc.Columns.Loai).IsEqualTo(1);
                if (sqlkt.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Đã tồn tại bản ghi tương tác thuốc tương ứng", "Cảnh báo", MessageBoxIcon.Warning);
                    txtAuxDieaseCode.Focus();
                    return;
                }
                QheTuongtacThuoc objtuongtac = new QheTuongtacThuoc();
                objtuongtac.MaHoatchat = mahoatchat;
                objtuongtac.Loai = 1;
                objtuongtac.MaTuongtac = Utility.sDbnull(txtAuxDieaseCode.Text, "");
                objtuongtac.CoChe = Utility.sDbnull(txtcoche2.Text, "");
                objtuongtac.NoiDung = Utility.sDbnull(txtnoidung2.Text.Trim());
                objtuongtac.XuTri = Utility.sDbnull(txtxutri2.Text.Trim());
                objtuongtac.HauQua = Utility.sDbnull(txthauqua2.Text.Trim());
                objtuongtac.MucDo = Utility.sDbnull(txtmucdo2.Text, "");
                objtuongtac.NguoiTao = globalVariables.UserName;
                objtuongtac.NgayTao = globalVariables.SysDate;
                objtuongtac.IsNew = true;
                objtuongtac.Save();
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới tương tác thuốc/bệnh {0}/ {1}", mahoatchat, Utility.sDbnull(txtAuxDieaseCode.Text)), newaction.Insert, "UI");
                DataRow dr = dtThuocBenh.NewRow();
                dr["ma_hoatchat"] = mahoatchat;
                dr["ma_tuongtac"] = Utility.sDbnull(txtAuxDieaseCode.Text, "");
                dr["ten_tuongtac"] = Utility.sDbnull(txtTenBenhPhu.Text, "");
                dr["co_che"] = Utility.sDbnull(txtcoche2.Text, "");
                dr["hau_qua"] = Utility.sDbnull(txthauqua2.Text.Trim());
                dr["xu_tri"] = Utility.sDbnull(txtxutri2.Text.Trim());
                dr["muc_do"] = Utility.sDbnull(txtmucdo2.Text, "");
                dr["noi_dung"] = Utility.sDbnull(txtnoidung2.Text.Trim());
                dtThuocBenh.Rows.Add(dr);
                dtThuocBenh.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdxoa2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridEXRow row in grdthuocbenh.GetCheckedRows())
                {
                    string mahoatchat = Utility.sDbnull(row.Cells["Ma_HoatChat"].Value);
                    string matuongtac = Utility.sDbnull(row.Cells["ma_tuongtac"].Value);
                    new Delete().From(QheTuongtacThuoc.Schema).Where(QheTuongtacThuoc.Columns.MaHoatchat).IsEqualTo(mahoatchat)
                        .And(QheTuongtacThuoc.Columns.MaTuongtac).IsEqualTo(matuongtac)
                        .And(QheTuongtacThuoc.Columns.Loai).IsEqualTo(1)
                        .Execute();
                    row.Delete();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa tương tác thuốc/bệnh {0}/{1}", mahoatchat, matuongtac), newaction.Delete, "UI");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdluu3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.sDbnull(txtdotuoi.myCode, "") == "")
                {
                    Utility.ShowMsg("Mã tương tác không được để trống", "Cảnh báo", MessageBoxIcon.Warning);
                    txtdotuoi.Focus();
                    return;
                }
                string mahoatchat = Utility.sDbnull(grdHoatChat.CurrentRow.Cells["Ma_HoatChat"].Value);
                SqlQuery sqlkt = new Select().From(QheTuongtacThuoc.Schema).Where(QheTuongtacThuoc.Columns.MaHoatchat).IsEqualTo(mahoatchat)
                        .And(QheTuongtacThuoc.Columns.MaTuongtac).IsEqualTo(Utility.sDbnull(txtdotuoi.myCode, ""))
                        .And(QheTuongtacThuoc.Columns.Loai).IsEqualTo(2);
                if (sqlkt.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Đã tồn tại bản ghi tương tác thuốc tương ứng", "Cảnh báo", MessageBoxIcon.Warning);
                    txtdotuoi.Focus();
                    return;
                }
                QheTuongtacThuoc objtuongtac = new QheTuongtacThuoc();
                objtuongtac.MaHoatchat = mahoatchat;
                objtuongtac.Loai = 2;
                objtuongtac.MaTuongtac = Utility.sDbnull(txtdotuoi.myCode, "");
                objtuongtac.CoChe = Utility.sDbnull(txtcoche3.Text, "");
                objtuongtac.NoiDung = Utility.sDbnull(txtnoidung3.Text.Trim());
                objtuongtac.XuTri = Utility.sDbnull(txtxutri3.Text.Trim());
                objtuongtac.HauQua = Utility.sDbnull(txthauqua3.Text.Trim());
                objtuongtac.MucDo = Utility.sDbnull(txtmucdo3.Text, "");
                objtuongtac.NguoiTao = globalVariables.UserName;
                objtuongtac.NgayTao = globalVariables.SysDate;
                objtuongtac.IsNew = true;
                objtuongtac.Save();
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới tương tác thuốc/tuổi {0}/ {1}", mahoatchat, Utility.sDbnull(txtdotuoi.Text)), newaction.Insert, "UI");
                DataRow dr = dtThuocTuoi.NewRow();
                dr["ma_hoatchat"] = mahoatchat;
                dr["ma_tuongtac"] = Utility.sDbnull(txtdotuoi.myCode, "");
                dr["ten_tuongtac"] = Utility.sDbnull(txtdotuoi.Text, "");
                dr["co_che"] = Utility.sDbnull(txtcoche3.Text, "");
                dr["hau_qua"] = Utility.sDbnull(txthauqua3.Text.Trim());
                dr["xu_tri"] = Utility.sDbnull(txtxutri3.Text.Trim());
                dr["muc_do"] = Utility.sDbnull(txtmucdo3.Text, "");
                dr["noi_dung"] = Utility.sDbnull(txtnoidung3.Text.Trim());
                dtThuocTuoi.Rows.Add(dr);
                dtThuocTuoi.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdxoa3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridEXRow row in grdthuoctuoi.GetCheckedRows())
                {
                    string mahoatchat = Utility.sDbnull(row.Cells["Ma_HoatChat"].Value);
                    string matuongtac = Utility.sDbnull(row.Cells["ma_tuongtac"].Value);
                    new Delete().From(QheTuongtacThuoc.Schema).Where(QheTuongtacThuoc.Columns.MaHoatchat).IsEqualTo(mahoatchat)
                        .And(QheTuongtacThuoc.Columns.MaTuongtac).IsEqualTo(matuongtac)
                        .And(QheTuongtacThuoc.Columns.Loai).IsEqualTo(2)
                        .Execute();
                    row.Delete();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa tương tác thuốc/tuổi {0}/{1}", mahoatchat, matuongtac), newaction.Delete, "UI");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
    }
}
