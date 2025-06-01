using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using System.IO;
using Microsoft.VisualBasic;
using VNS.HIS.UI.Classess;
using Aspose.Words;
using System.Diagnostics;
using Janus.Windows.GridEX;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UCs;
using System.Transactions;
//using SubSonic.Utilities;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_phieutuvanPTTT : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;

        public KcbPhieutuvanPttt tuvanPttt = new KcbPhieutuvanPttt();
        KcbLuotkham objLuotkham = null;
        NoitruPhieunhapvien objNhapvien;
        NoitruPhieuravien objRavien;
        public action m_enAct = action.FirstOrFinished;
        public bool CallfromParent = false;
        private bool AllowSeletionChanged = false;
        public frm_phieutuvanPTTT()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtbskhac = globalVariables.gv_dtDmucNhanvien.Clone();
            dtNgaytuvan.Value = DateTime.Now;
            ucThongtinnguoibenh_doc_v11.noitrungoaitru = 1;
            ucThongtinnguoibenh_doc_v11._OnEnterMe += ucThongtinnguoibenh_doc_v11__OnEnterMe;
            this.KeyDown += frm_phieutuvanPTTT_KeyDown;
            ucThongtinnguoibenh_doc_v11.SetReadonly();
            grd_bskhac.ColumnButtonClick += grd_bskhac_ColumnButtonClick;
            txtBsKhac._OnEnterMe += txtBsKhac__OnEnterMe;
            txtPhuongPhapVoCam._OnShowDataV1 += _OnShowDataV1;
            txtPPgiamdau._OnShowDataV1 += _OnShowDataV1;
            grdList.SelectionChanged += grdList_SelectionChanged;
            cmdCancel.Click += cmdCancel_Click;
        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            AllowSeletionChanged = true;
            isDoing = false;
            cmdExit.BringToFront();
            grdList_SelectionChanged(grdList, e);
            ModifyCommands();
        }
        void ModifyCommands()
        {
            cmdThemmoi.Enabled = objLuotkham != null && !isDoing;
            cmdIn.Enabled = Utility.isValidGrid(grdList) && objLuotkham != null && tuvanPttt != null;
            cmdXoa.Enabled = Utility.isValidGrid(grdList) && objLuotkham != null && tuvanPttt != null && !isDoing;
            //cmdSave.Enabled = isDoing;
            //if (grdList.RowCount <= 0) ClearControl();
        }
        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSeletionChanged || isDoing) return;
                tuvanPttt = new Select().From(KcbPhieutuvanPttt.Schema).Where(KcbPhieutuvanPttt.Columns.IdPhieu).IsEqualTo(grdList.GetValue("id_phieu")).ExecuteSingle<KcbPhieutuvanPttt>();
                if (tuvanPttt != null) m_enAct = action.Update;
                FillData4Update();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                ModifyCommands();
            }
        }

        void _OnShowDataV1(UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            }
        }

        void txtBsKhac__OnEnterMe()
        {
            if (txtBsKhac.MyID != "-1")
            {
                AddBacsi(dtbskhac, grd_bskhac, txtBsKhac);
                txtBsKhac.Focus();
                txtBsKhac.SelectAll();
            }
        }
        private void AddBacsi(DataTable dtData, GridEX grdList, AutoCompleteTextbox auto)
        {
            try
            {
                EnumerableRowCollection<DataRow> query = from benh in dtData.AsEnumerable()
                                                         where Utility.sDbnull(benh[DmucNhanvien.Columns.MaNhanvien]) == auto.MyCode
                                                         && Utility.sDbnull(benh[DmucNhanvien.Columns.IdNhanvien]) == auto.MyID
                                                         select benh;
                if (!query.Any())
                {
                    DataRow drv = dtData.NewRow();
                    drv[DmucNhanvien.Columns.IdNhanvien] = auto.MyID;
                    drv[DmucNhanvien.Columns.MaNhanvien] = auto.MyCode;
                    drv[DmucNhanvien.Columns.TenNhanvien] = auto.Text;
                    dtData.Rows.Add(drv);
                    dtData.AcceptChanges();
                    grdList.AutoSizeColumns();
                }
                else
                {
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
        void grd_bskhac_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {

                if (e.Column.Key == "XOA")
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa bác sĩ tư vấn khác: {0} không?", grd_bskhac.GetValue("ten_nhanvien").ToString()), "Cảnh báo xóa", true))
                    {
                        grd_bskhac.CurrentRow.Delete();
                        dtbskhac.AcceptChanges();
                        grd_bskhac.Refetch();
                        grd_bskhac.AutoSizeColumns();

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

        void frm_phieutuvanPTTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((ActiveControl != null && (ActiveControl.Name == txtBsKhac.Name || ActiveControl.Name == txtChandoan.Name || ActiveControl.Name == txtRuiro.Name || ActiveControl.Name == txtGhichu.Name)))
                    return;
                else
                    SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.Escape)
            {
                cmdExit.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                cmdSave.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.T)
            {
                cmdThemmoi.PerformClick();
            }
        }
        DataTable m_dtData = new DataTable();
        void ucThongtinnguoibenh_doc_v11__OnEnterMe()
        {
            if (ucThongtinnguoibenh_doc_v11.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh_doc_v11.objLuotkham;
                objNhapvien = new Select().From(NoitruPhieunhapvien.Schema).Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                objRavien = new Select().From(NoitruPhieuravien.Schema).Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
               
                m_dtData = SPs.KcbPhieutuvanPtttTimkiem(-1, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1), objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, "").GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngay_tuvan,ten_benhnhan");
               
                //cmdSave.Enabled = !objLuotkham.NgayRavien.HasValue;
            }
        }
        public DataTable dtbskhac = new DataTable();
        void FillData4Update()
        {
            ClearControl();
            if (tuvanPttt != null)
            {
                dtNgaytuvan.Value = tuvanPttt.NgayTuvan;
                autoKhoa.SetId(tuvanPttt.IdKhoadieutri);
                txtBsChinh.SetId(tuvanPttt.IdBacsiChinh);
                FillBacsiPttt(tuvanPttt.IdBacsikhac, dtbskhac, grd_bskhac);
                List<string> lstValues = tuvanPttt.ThuocVt.Split(',').ToList<string>();
                if (lstValues.Count == 4)
                {
                    chkThuoc1.Checked = lstValues[0] == "1";
                    chkThuoc2.Checked = lstValues[1] == "1";
                    chkThuoc3.Checked = lstValues[2] == "1";
                    chkThuoc4.Checked = lstValues[3] == "1";
                }
                lstValues = tuvanPttt.ThuthuatGiamdau.Split(',').ToList<string>();
                if (lstValues.Count == 2)
                {
                    chkGiamdau1.Checked = lstValues[0] == "1";
                    chkGiamdau2.Checked = lstValues[1] == "1";
                }
                txtPhuongPhapVoCam._Text = tuvanPttt.PhuongphapVocam;
                txtPPgiamdau._Text = tuvanPttt.PhuongphapGiamdau;
                
                txtChandoan.Text = tuvanPttt.ChanDoan;
                txtRuiro.Text = tuvanPttt.RuiroGhinhan;
                txtGhichu.Text = tuvanPttt.GhichuThem;
                
            }
        }
        string getBacsithamgia(DataTable dtData)
        {
            var q = from p in dtData.AsEnumerable()
                    select Utility.sDbnull(p["id_nhanvien"], "");
            return string.Join(",", q.ToArray<string>());
        }
        string getFieldValue(DataTable dtData, string fieldName)
        {
            var q = from p in dtData.AsEnumerable()
                    select Utility.sDbnull(p[fieldName], "");
            return string.Join(",", q.ToArray<string>());
        }

        void FillBacsiPttt(string dataString, DataTable dtData, GridEX grdlist)
        {
            dtData.Clear();
            if (!string.IsNullOrEmpty(dataString) && dtData.Columns.Count > 0)
            {
                string[] rows = dataString.Split(',');
                foreach (string row in rows)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        DataRow newDr = dtData.NewRow();
                        newDr[DmucNhanvien.Columns.IdNhanvien] = Utility.Int16Dbnull(row, -1);
                        newDr[DmucNhanvien.Columns.TenNhanvien] = LaytenNvien(Utility.sDbnull(row, -1));
                        dtData.Rows.Add(newDr);
                        dtData.AcceptChanges();
                    }
                }
                grdlist.DataSource = dtData;
            }
        }
        private string LaytenNvien(string id_nhanvien)
        {
            try
            {
                string TenNhanvien = "";
                DataRow[] arrMaBenh =
                    globalVariables.gv_dtDmucNhanvien.Select(string.Format(DmucNhanvien.Columns.IdNhanvien + "={0}", id_nhanvien));
                if (arrMaBenh.GetLength(0) > 0) TenNhanvien = Utility.sDbnull(arrMaBenh[0][DmucNhanvien.Columns.TenNhanvien], "");
                return TenNhanvien;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public void ClearControl()
        {
            txtBsChinh.SetId(-1);
            txtBsKhac.SetId(-1);
            txtChandoan.Clear();
            txtPhuongPhapVoCam.SetCode("-1");
            txtPPgiamdau.SetCode("-1");
            chkThuoc1.Checked = chkThuoc2.Checked = chkThuoc3.Checked = chkThuoc4.Checked = chkGiamdau1.Checked = chkGiamdau2.Checked = false;
            txtRuiro.Clear();
            txtGhichu.Clear();
            dtbskhac.Rows.Clear();
            dtbskhac.AcceptChanges();
        }
        private Boolean isValidData()
        {

            //if (string.IsNullOrEmpty(txtChandoan.Text))
            //{
            //    Utility.ShowMsg("Thông tin quá trình bệnh lý không được bỏ trống", "Cảnh báo", MessageBoxIcon.Warning);
            //    txtChandoan.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtRuiro.Text))
            //{
            //    Utility.ShowMsg("Thông tin tóm tắt lâm sàng không được bỏ trống", "Cảnh báo", MessageBoxIcon.Warning);
            //    txtRuiro.Focus();
            //    return false;
            //}
           
            return true;
        }

        private void frm_phieutuvanPTTT_Load(object sender, EventArgs e)
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() {  txtPPgiamdau.LOAI_DANHMUC,txtPhuongPhapVoCam.LOAI_DANHMUC }, true);
            txtPPgiamdau.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPPgiamdau.LOAI_DANHMUC));
            txtPhuongPhapVoCam.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPhuongPhapVoCam.LOAI_DANHMUC));
            DataTable mDtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            autoKhoa.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            txtBsChinh.Init(globalVariables.gv_dtDmucNhanvien,
                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            txtBsKhac.Init(txtBsChinh.AutoCompleteSource, txtBsChinh.defaultItem);
            Utility.SetDataSourceForDataGridEx(grd_bskhac, dtbskhac, false, true, "", "");
            if (tuvanPttt != null && m_enAct == action.Update)
            {
                FillData4Update();
            }
            else
            {
                ucThongtinnguoibenh_doc_v11.Refresh();
            }
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {

            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin Phiếu tư vấn PTTT không ?", "Thông báo", true))
            {
                int banghi = new Delete().From<KcbPhieutuvanPttt>()
                     .Where(KcbPhieutuvanPttt.Columns.IdPhieu)
                     .IsEqualTo(Utility.Int32Dbnull(tuvanPttt.IdPhieu))
                     .Execute();
                if (banghi > 0)
                {
                    tuvanPttt = new KcbPhieutuvanPttt();
                    Utility.ShowMsg("Bạn xóa thông tin Phiếu tư vấn PTTT thành công", "Thông báo");
                   
                    ucThongtinnguoibenh_doc_v11.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_doc_v11.txtMaluotkham.SelectAll();
                    ucThongtinnguoibenh_doc_v11__OnEnterMe();
                }

            }
        }
        

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            //ReportDocument crpt = new ReportDocument();
            //string path = Utility.sDbnull(SystemReports.GetPathReport("TONGKETBENHAN"));
            //if (File.Exists(path))
            //{
            //    crpt.Load(path);
            //}
            //else
            //{
            //    Utility.ShowMsg("Không tìm thấy file\n Mời bạn liên hệ với quản trị để update thêm file report", "Thông báo", MessageBoxIcon.Error);
            //}
            //DataSet dt = SPs.SpTongketbenhan(Utility.Int32Dbnull(txtId.Text)).GetDataSet();
            //DataTable db = dt.Tables[0];
            //Utility.UpdateLogotoDatatable(ref db);
            //if (dt != null && dt.Tables.Count > 0)
            //{
            //    dt.Tables[0].TableName = "TONGKETBENHAN";
            //}
            ////dt.WriteXmlSchema("D:\\dsBienBanKiemThaoTuVong.xsd");
            //THU_VIEN_CHUNG.CreateXml(dt, "TONGKETBENHAN.xml");
            //var objForm = new frmPrintPreview("Phiếu tư vấn PTTT", crpt, true, true);
            //crpt.SetDataSource(dt);
            //objForm.crptViewer.ReportSource = crpt;
            //objForm.crptTrinhKyName = Path.GetFileName(path);
            //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
            //                                                     "                                                                  "
            //                                                         .Replace("#$X$#",
            //                                                             Strings.Chr(34) + "&Chr(13)&" +
            //                                                             Strings.Chr(34)) + Strings.Chr(34);
            //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name.ToUpper());
            //crpt.SetParameterValue("BranchName", globalVariables.Branch_Name.ToUpper());
            //crpt.SetParameterValue("Address", globalVariables.Branch_Address);
            //crpt.SetParameterValue("sTitleReport", "Phiếu tư vấn PTTT");
            //crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(DateTime.Now));
            //crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
            //objForm.ShowDialog();
            //crpt.Close();
            //crpt.Dispose();
            //objForm.Dispose();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            ClearControl();
        }


        private void cmdExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!isValidData()) return;
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lưu Phiếu tư vấn PTTT?", "Thông báo", true)) return;
                if (tuvanPttt == null || m_enAct==action.Insert) tuvanPttt = new KcbPhieutuvanPttt();
                if (tuvanPttt.IdPhieu > 0)
                {
                    tuvanPttt.IsNew = false;
                    tuvanPttt.MarkOld();
                    tuvanPttt.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    tuvanPttt.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    tuvanPttt.IsNew = true;
                    tuvanPttt.NguoiTao = globalVariables.UserName;
                    tuvanPttt.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                tuvanPttt.MaLuotkham = objLuotkham.MaLuotkham;
                tuvanPttt.IdBenhnhan = (int)objLuotkham.IdBenhnhan;
                tuvanPttt.IdKhoadieutri = Utility.Int16Dbnull(autoKhoa.MyID, -1);
                tuvanPttt.IdBacsiChinh = Utility.Int16Dbnull(txtBsChinh.MyID);
                tuvanPttt.IdBacsikhac = getBacsithamgia(dtbskhac);
                tuvanPttt.PhuongphapGiamdau = Utility.DoTrim(txtPPgiamdau.Text);
                tuvanPttt.PhuongphapVocam = Utility.DoTrim(txtPhuongPhapVoCam.Text);
                tuvanPttt.ThuocVt = string.Format("{0},{1},{2},{3}", Utility.Bool2byte(chkThuoc1), Utility.Bool2byte(chkThuoc2.Checked), Utility.Bool2byte(chkThuoc3.Checked), Utility.Bool2byte(chkThuoc4.Checked));
                tuvanPttt.ThuthuatGiamdau = string.Format("{0},{1}", Utility.Bool2byte(chkGiamdau1), Utility.Bool2byte(chkGiamdau2.Checked));
                tuvanPttt.ChanDoan =Utility.DoTrim( txtChandoan.Text);
                tuvanPttt.RuiroGhinhan = Utility.DoTrim(txtRuiro.Text);
                tuvanPttt.NgayTuvan = dtNgaytuvan.Value;
                tuvanPttt.GhichuThem = Utility.DoTrim(txtGhichu.Text);
                tuvanPttt.Save();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới Phiếu tư vấn PTTT bệnh nhân: {0}-{1} thành công", tuvanPttt.MaLuotkham, ucThongtinnguoibenh_doc_v11.txtTenBN.Text), tuvanPttt.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới Phiếu tư vấn PTTT thành công. Nhấn Ok để kết thúc");
                    cmdIn.Enabled = cmdXoa.Enabled = true;
                    if (_OnCreated != null) _OnCreated(tuvanPttt.IdPhieu, action.Insert);
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Phiếu tư vấn PTTT bệnh nhân: {0}-{1} thành công", tuvanPttt.MaLuotkham, ucThongtinnguoibenh_doc_v11.txtTenBN.Text), tuvanPttt.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null) _OnCreated(tuvanPttt.IdPhieu, action.Update);
                    MessageBox.Show("Đã cập nhật Phiếu tư vấn PTTT thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                RefreshData(tuvanPttt.IdPhieu);
                cmdExit.BringToFront();
                cmdCancel.PerformClick();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void RefreshData(long id)
        {
            try
            {
                DataTable dt_temp = SPs.KcbPhieutuvanPtttTimkiem(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1), -1, "", "").GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe(id))
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", KcbBienbanhoichan.Columns.Id, grdList.GetValue(KcbBienbanhoichan.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
                if (m_enAct == action.Insert && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtData.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (m_enAct == action.Update && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtData.Select("id_phieu=" + id);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["chan_doan"] = dt_temp.Rows[0]["chan_doan"];
                        arrDr[0]["phuongphap_vocam"] = dt_temp.Rows[0]["phuongphap_vocam"];
                        arrDr[0]["phuongphap_giamdau"] = dt_temp.Rows[0]["phuongphap_giamdau"];
                        arrDr[0]["ruiro_ghinhan"] = dt_temp.Rows[0]["ruiro_ghinhan"];
                        arrDr[0]["ghichu_them"] = dt_temp.Rows[0]["ghichu_them"];
                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, "id_phieu", id.ToString());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }
        bool DeleteMe(long id_phieu)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Delete().From(KcbBienbanhoichan.Schema).Where(KcbBienbanhoichan.Columns.Id).IsEqualTo(id_phieu).Execute();
                    }
                    scope.Complete();


                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        bool isDoing = false;
        private void cmdThemmoi_Click(object sender, EventArgs e)
        {

            cmdThemmoi.Enabled = false;
            isDoing = true;
            AllowSeletionChanged = false;
            m_enAct = action.Insert;
            cmdCancel.BringToFront();
            ClearControl();
            ucThongtinnguoibenh_doc_v11.txtMaluotkham.Focus();
            ucThongtinnguoibenh_doc_v11.txtMaluotkham.SelectAll();
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            try
            {

                
                if (tuvanPttt == null || tuvanPttt.IdPhieu <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo tờ Phiếu tư vấn PTTT trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.KcbPhieutuvanPtttIn(tuvanPttt.IdPhieu, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                dtData.TableName = "Phieu_TuvanPTTT";
                List<string> lstAddedFields = new List<string>() { "thuoc1", "thuoc2", "thuoc3", "thuoc4", "giamdau1", "giamdau2" };
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));  

                THU_VIEN_CHUNG.CreateXML(dtData, "Phieu_TuvanPTTT.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                dtData.TableName = "Phieu_TuvanPTTT";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["dia_diem"] = globalVariables.gv_strDiadiem;
                drData["ngay_thang_nam"] = Utility.FormatDateTime_gio_ngay_thang_nam(tuvanPttt.NgayTuvan,"Lúc");
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("thuoc1", tuvanPttt.ThuocVt.Split(',')[0]);
                dicMF.Add("thuoc2", tuvanPttt.ThuocVt.Split(',')[1]);
                dicMF.Add("thuoc3", tuvanPttt.ThuocVt.Split(',')[2]);
                dicMF.Add("thuoc4", tuvanPttt.ThuocVt.Split(',')[3]);
                dicMF.Add("giamdau1", tuvanPttt.ThuthuatGiamdau.Split(',')[0]);
                dicMF.Add("giamdau2", tuvanPttt.ThuthuatGiamdau.Split(',')[1]);
               
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\Phieu_TuvanPTTT.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("Phieu_TuvanPTTT", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Phiếu tư vấn PTTT tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "Phieu_TuvanPTTT", objLuotkham.MaLuotkham, Utility.sDbnull(tuvanPttt.IdPhieu), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if(tuvanPttt!=null &&   tuvanPttt.IdPhieu>0)
                {

                    if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin phiếu tư vấn PTTT không ?", "Thông báo", true))
                    {
                        int banghi = new Delete().From<KcbPhieutuvanPttt>()
                             .Where(KcbPhieutuvanPttt.Columns.IdPhieu)
                             .IsEqualTo(Utility.Int32Dbnull(tuvanPttt.IdPhieu))
                             .Execute();
                        if (banghi > 0)
                        {
                            tuvanPttt = new KcbPhieutuvanPttt();
                            if (_OnCreated != null) _OnCreated(tuvanPttt.IdPhieu, action.Delete);
                            Utility.ShowMsg("Bạn xóa thông tin phiếu tư vấn PTTT thành công", "Thông báo");
                            DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                            m_dtData.Rows.Remove(dr);
                            m_dtData.AcceptChanges();
                            grdList_SelectionChanged(grdList, e);
                            //ucThongtinnguoibenh_doc_v11.txtMaluotkham.Focus();
                            //ucThongtinnguoibenh_doc_v11.txtMaluotkham.SelectAll();
                            //ucThongtinnguoibenh_doc_v11__OnEnterMe();
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
                ModifyCommands();
            }
            
        }
    }
}
