using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VMS.HIS.DAL;
using Janus.Windows.GridEX;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Microsoft.VisualBasic;
using VNS.HIS.UI.Classess;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_PhieuSoKetDieuTri : Form
    {

        DataTable dtData = new DataTable();
        KcbLuotkham objLuotkham = null;
        public action m_enAct = action.FirstOrFinished;
        public bool CallfromParent = false;
        KcbPhieusoket15ngay skdt = new KcbPhieusoket15ngay();
        bool AllowSelectionChanged = false;
        public frm_PhieuSoKetDieuTri()
        {
            InitializeComponent();
            ucThongtinnguoibenh_v21._OnEnterMe += ucThongtinnguoibenh_v21__OnEnterMe;
            grdLichSu.SelectionChanged += grdLichSu_SelectionChanged;
            this.KeyDown += frm_PhieuSoKetDieuTri_KeyDown;
            FormClosing += frm_PhieuSoKetDieuTri_FormClosing;
        }

        void frm_PhieuSoKetDieuTri_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void frm_PhieuSoKetDieuTri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
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
        void grdLichSu_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged) return;
                if (Utility.isValidGrid(grdLichSu))
                {
                    skdt = new Select().From(KcbPhieusoket15ngay.Schema).Where(KcbPhieusoket15ngay.Columns.Id).IsEqualTo(grdLichSu.GetValue("id").ToString()).ExecuteSingle<KcbPhieusoket15ngay>();
                    m_enAct = action.Update;
                    FillData4Update();
                }
                else
                {
                    skdt = null;
                    ClearControl();
                    m_enAct = action.Insert;
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

        void ucThongtinnguoibenh_v21__OnEnterMe()
        {
            if (ucThongtinnguoibenh_v21.objLuotkham != null)
            {
                AllowSelectionChanged = false;
                objLuotkham = ucThongtinnguoibenh_v21.objLuotkham;
                dtData = SPs.KcbTimkiemdanhsachphieusoket15ngay(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdLichSu, dtData, true, true, "1=1", "ngay_lap desc");
                AllowSelectionChanged = true;
                grdLichSu_SelectionChanged(grdLichSu, new EventArgs());
            }
        }
        DataTable dt;
        private void ClearControl()
        {
            txtId.Text = string.Empty;
            dtNgayHoiChan.Value = THU_VIEN_CHUNG.GetSysDateTime();
            dtNgayHoiChan.ReadOnly = false;
            txtBacsidieutri.SetId("-1");
            autoKhoa.SetId("-1");
            txtDienBienCLS.Text = string.Empty;
            txtDanhGiaKQ.Text = string.Empty;
            txtXNghiemCLS.Text = string.Empty;
            txtQuaTrinhDieuTri.Text = string.Empty;
            txtHuongDTTiepVaTienLuong.Text = string.Empty;
        }

       
        void ModifyCommand()
        {
            cmdXoa.Enabled = cmdIn.Enabled = grdLichSu.RowCount > 0 && objLuotkham != null;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //ReportDocument crpt = new ReportDocument();
            //string path = Utility.sDbnull(SystemReports.GetPathReport("SOKET_DIEUTRI"));
            //if (File.Exists(path))
            //{
            //    crpt.Load(path);
            //}
            //else
            //{
            //    Utility.ShowMsg("Không tìm thấy file\n Mời bạn liên hệ với quản trị để update thêm file report", "Thông báo", MessageBoxIcon.Error);
            //}
            //DataTable dt = SPs.SpSoKet15NgayDieuTri(txtstt_rec.Text).GetDataSet().Tables[0];

            //Utility.UpdateLogotoDatatable(ref dt);
            //BusinessHelper.CreateXml(dt, "SOKET_15NGAY_DIEUTRI.xml");
            //var objForm = new frmPrintPreview("PHIẾU SƠ KẾT 15 NGÀY ĐIỀU TRỊ", crpt, true, true);
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
            //crpt.SetParameterValue("ReportTitle", "PHIẾU SƠ KẾT 15 NGÀY ĐIÊU TRỊ");
            //crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(DateTime.Now));
            //objForm.ShowDialog();
            //crpt.Close();
            //crpt.Dispose();
            //objForm.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void FillData4Update()
        {
           

            if (skdt != null)
            {
                txtId.Text = Utility.sDbnull(skdt.Id, "");

                autoKhoa.SetId(skdt.IdKhoadieutri);
                dtNgayHoiChan.Value = skdt.NgayLap;
                txtDienBienCLS.Text = skdt.DienBienLSTrongDotDieuTri;
                txtXNghiemCLS.Text = skdt.XetNghiemCLS;
                txtQuaTrinhDieuTri.Text = skdt.QuaTrinhDieuTri;
                txtDanhGiaKQ.Text = skdt.DanhGiaKetQua;
                txtHuongDTTiepVaTienLuong.Text = skdt.HuongDieuTriTiepVaTienLuong;
                txtBacsidieutri.SetId(skdt.IdBacsidieutri);
            }
            
        }

        private Boolean isValiData()
        {
          
            if (Utility.DoTrim(autoKhoa.Text) == "")
            {
                MessageBox.Show("Bạn cần nhập khoa điều trị");
                autoKhoa.Focus();
                return false;
            }
            
            if (Utility.DoTrim(txtBacsidieutri.Text) == "")
            {
                MessageBox.Show("Bạn cần nhập bác sỹ điều trị");
                txtBacsidieutri.Focus();
                return false;
            }

            return true;
        }

        void LoadUserConfigs()
        {
            try
            {
                chkPreview.Checked = Utility.getUserConfigValue(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked)) == 1;

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
                Utility.SaveUserConfig(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked));

            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void frm_PhieuSoKetDieuTri_Load(object sender, EventArgs e)
        {
            LoadUserConfigs();
            DataTable mDtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            autoKhoa.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            txtBacsidieutri.Init(globalVariables.gv_dtDmucNhanvien,
                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            
            if (skdt != null && m_enAct == action.Update)
            {
                FillData4Update();
            }
            else
            {
                ucThongtinnguoibenh_v21.Refresh();
            }
        }

        private string Laysophieu()
        {
            string ma_phieu = "";
            StoredProcedure sp = SPs.SpGetMaphieusoket15ngay(DateTime.Now.Year, ma_phieu);
            sp.Execute();
            return Utility.sDbnull(sp.OutputValues[0], "-1");
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValiData() == false) return;
                if (MessageBox.Show("Bạn chắc chắn muốn lưu phiếu sơ kết điều trị?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                if (skdt == null) skdt = new KcbPhieusoket15ngay();
                if (skdt.Id <= 0)
                {
                    skdt = new KcbPhieusoket15ngay();
                    skdt.IsNew = true;
                    skdt.NgayTao = DateTime.Now;
                    skdt.NguoiTao = globalVariables.UserName;
                    skdt.MaPhieu = Laysophieu();

                }
                else
                {
                    skdt.MarkOld();
                    skdt.IsNew = false;
                    skdt.NgaySua = DateTime.Now;
                    skdt.NguoiSua = globalVariables.UserName;
                }
                skdt.IdBenhnhan = objLuotkham.IdBenhnhan;
                skdt.MaLuotkham = objLuotkham.MaLuotkham;
                skdt.IdKhoadieutri = Utility.Int32Dbnull(autoKhoa.MyID);
                skdt.Buong = ucThongtinnguoibenh_v21.txtidBuong.Text;
                skdt.Giuong = ucThongtinnguoibenh_v21.txtGiuong.Text;
                skdt.NgayLap = dtNgayHoiChan.Value;
                skdt.DienBienLSTrongDotDieuTri = txtDienBienCLS.Text;
                skdt.XetNghiemCLS = txtXNghiemCLS.Text;
                skdt.QuaTrinhDieuTri = txtQuaTrinhDieuTri.Text;
                skdt.DanhGiaKetQua = txtDanhGiaKQ.Text;
                skdt.HuongDieuTriTiepVaTienLuong = txtHuongDTTiepVaTienLuong.Text;
                skdt.IdBacsidieutri = Utility.Int32Dbnull(txtBacsidieutri.MyID);
                skdt.Save();
                txtId.Text = skdt.Id.ToString();
               
                if (m_enAct == action.Insert)
                {
                    DataRow newRow = dtData.NewRow();
                    Utility.FromObjectToDatarow(skdt, ref newRow);
                    newRow["ten_nhanvien"] = txtBacsidieutri.Text;
                    newRow["ten_khoaphong"] = autoKhoa.Text;
                    dtData.Rows.Add(newRow);
                    dtData.AcceptChanges();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới phiếu sơ kết điều trị 15 ngày cho bệnh nhân: {0}-{1} thành công", skdt.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text), skdt.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã thêm mới phiếu sơ kết điều trị 15 ngày thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật phiếu sơ kết điều trị 15 ngày cho bệnh nhân: {0}-{1} thành công", skdt.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text), skdt.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã cập nhật phiếu sơ kết điều trị 15 ngày thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin phiếu sơ kết điều trị 15 ngày đang chọn không ?", "Thông báo", true))
            {
                int banghi = new Delete().From<KcbPhieusoket15ngay>()
                     .Where(KcbPhieusoket15ngay.Columns.Id)
                     .IsEqualTo(Utility.Int32Dbnull(skdt.Id))
                     .Execute();
                if (banghi > 0)
                {
                    Utility.ShowMsg("Bạn xóa thông tin tổng kết bệnh án thành công", "Thông báo");
                    DataRow dr = ((DataRowView)grdLichSu.CurrentRow.DataRow).Row;
                    dtData.Rows.Remove(dr);
                    dtData.AcceptChanges();
                    grdLichSu_SelectionChanged(grdLichSu, e);
                    
                }

            }
        }

        private void cmdThemmoi_Click(object sender, EventArgs e)
        {
            if (m_enAct == action.Insert)
            {
                if (!Utility.AcceptQuestion("Bạn đang ở trạng thái thêm mới phiếu sơ kết điều trị 15 ngày và có thể đã nhập một số thông tin. Nếu nhấn thêm mới các thông tin mới nhập có thể bị xóa.\nBạn có chắc chắn muốn làm lại từ đầu không?", "Xác nhận", true))
                {
                    return;
                }
            }
            m_enAct = action.Insert;
            ClearControl();
            autoKhoa.Focus();
            skdt = null;
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtData = SPs.KcbPhieusoket15ngayIn(skdt.Id).GetDataSet().Tables[0];
                dtData.TableName = "noitru_phieusoket15ngay";
                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_phieusoket15ngay.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                noitru_inphieu.Inphieusoket15ngay(dtData, DateTime.Now, chkPreview.Checked, "noitru_phieusoket15ngay");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
