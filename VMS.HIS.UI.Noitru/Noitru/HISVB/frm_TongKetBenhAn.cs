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
//using SubSonic.Utilities;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_TongKetBenhAn : Form
    {
        public KcbTongketBA Tkba = new KcbTongketBA();
        KcbLuotkham objLuotkham = null;
        public action m_enAct = action.FirstOrFinished;
        public bool CallfromParent = false;
        public frm_TongKetBenhAn()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtDenNgay.Value = dtNgayRaVien.Value = THU_VIEN_CHUNG.GetSysDateTime();
            ucThongtinnguoibenh_v21._OnEnterMe += ucThongtinnguoibenh_v21__OnEnterMe;
            this.KeyDown += frm_TongKetBenhAn_KeyDown;
            ucThongtinnguoibenh_v21.SetReadonly();
        }

        void frm_TongKetBenhAn_KeyDown(object sender, KeyEventArgs e)
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

        void ucThongtinnguoibenh_v21__OnEnterMe()
        {
            if (ucThongtinnguoibenh_v21.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh_v21.objLuotkham;
                Tkba = new Select().From(KcbTongketBA.Schema).Where(KcbTongketBA.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(KcbTongketBA.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbTongketBA>();
                if (Tkba != null) m_enAct = action.Update;
                FillData4Update();
                cmdXoa.Enabled = cmdSave.Enabled = !objLuotkham.NgayRavien.HasValue;
            }
        }

        void FillData4Update()
        {
            if (Tkba != null)
            {
                dtTuNgay.Value = Convert.ToDateTime(Tkba.TuNgay);
                dtDenNgay.Value = Convert.ToDateTime(Tkba.DenNgay);
                txtChanDoan.Text = Tkba.ChanDoan;
                autoKhoa.SetId(Tkba.IdKhoadieutri);
                txtSoNgayDtri.Text = Convert.ToString(Tkba.SoNgayDtri);
                txtTinhTrangHienTai._Text = Tkba.TTrangHienTai;
                autoLydovv.SetCode(Tkba.Ldvv);
                txtquatrinhbenhly.Text = Tkba.LamSang;
                txtCanLamSang.Text = Tkba.CanLamSang;
                txtPPdieutri.SetCode(Tkba.HuongDtri);
                dtNgayRaVien.Value = Convert.ToDateTime(Tkba.NgayRv);
            }
        }

        public void ClearControl()
        {
            txtPPdieutri.SetCode("-1");
            txtChanDoan.Text = string.Empty;
            autoLydovv.SetCode("-1");
            txtCanLamSang.Text = string.Empty;
            txtquatrinhbenhly.Text = string.Empty;
            txtSoNgayDtri.Text = string.Empty;
            txtTinhTrangHienTai.Text = string.Empty;

        }
        private Boolean isValidData()
        {

            
            if(string.IsNullOrEmpty(txtCanLamSang.Text))
            {
                Utility.ShowMsg("Thông tin tóm tắt lâm sàng không được bỏ trống","Cảnh báo", MessageBoxIcon.Warning);
                txtCanLamSang.Focus();
                return false;
            }
           
            if (string.IsNullOrEmpty(txtquatrinhbenhly.Text))
            {
                Utility.ShowMsg("Thông tin quá trình bệnh lý không được bỏ trống", "Cảnh báo", MessageBoxIcon.Warning);
                txtquatrinhbenhly.Focus();
                return false;
            }
            
            return true;
        }

        private void frm_TongKetBenhAn_Load(object sender, EventArgs e)
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { autoLydovv.LOAI_DANHMUC, txtPPdieutri.LOAI_DANHMUC }, true);
            autoLydovv.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLydovv.LOAI_DANHMUC));
            txtPPdieutri.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPPdieutri.LOAI_DANHMUC));

            DataTable mDtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            autoKhoa.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            
            if (Tkba != null && m_enAct == action.Update)
            {
                FillData4Update();
            }
            else
            {
                ucThongtinnguoibenh_v21.Refresh();
            }
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {

            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin tổng kết bệnh án không ?", "Thông báo", true))
            {
                int banghi = new Delete().From<KcbTongketBA>()
                     .Where(KcbTongketBA.Columns.Id)
                     .IsEqualTo(Utility.Int32Dbnull(Tkba.Id))
                     .Execute();
                if (banghi > 0)
                {
                    Tkba = new KcbTongketBA();
                    Utility.ShowMsg("Bạn xóa thông tin tổng kết bệnh án thành công", "Thông báo");
                    ClearControl();
                    ucThongtinnguoibenh_v21.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_v21.txtMaluotkham.SelectAll();
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
            //var objForm = new frmPrintPreview("TỔNG KẾT BỆNH ÁN", crpt, true, true);
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
            //crpt.SetParameterValue("sTitleReport", "TỔNG KẾT BỆNH ÁN");
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
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lưu Tổng kết bệnh án?", "Thông báo", true)) return;
                if (Tkba == null) Tkba = new KcbTongketBA();
                if (Tkba.Id > 0)
                {
                    Tkba.IsNew = false;
                    Tkba.MarkOld();
                    Tkba.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    Tkba.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    Tkba.IsNew = true;
                    Tkba.NguoiTao = globalVariables.UserName;
                    Tkba.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                Tkba.MaLuotkham = objLuotkham.MaLuotkham;
                Tkba.IdBenhnhan = (int)objLuotkham.IdBenhnhan;
                Tkba.Ldvv = Utility.sDbnull(autoLydovv.MyCode);
                Tkba.ChanDoan = Utility.sDbnull(txtChanDoan.Text);
                Tkba.SoNgayDtri = Utility.Int32Dbnull(txtSoNgayDtri.Text);
                Tkba.HuongDtri = Utility.sDbnull(txtPPdieutri.myCode);
                Tkba.TuNgay = Convert.ToDateTime(dtTuNgay.Value);
                Tkba.TTrangHienTai = Utility.sDbnull(txtTinhTrangHienTai.Text);
                Tkba.DenNgay = Convert.ToDateTime(dtDenNgay.Value);
                Tkba.NgayRv = Convert.ToDateTime(dtNgayRaVien.Value);
                Tkba.IdKhoadieutri = Utility.Int16Dbnull(autoKhoa.MyID, -1);
                Tkba.LamSang = Utility.sDbnull(txtquatrinhbenhly.Text);
                Tkba.CanLamSang = Utility.sDbnull(txtCanLamSang.Text);
                Tkba.Save();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới Tổng kết Bệnh án bệnh nhân: {0}-{1} thành công", Tkba.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text), Tkba.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã thêm mới Tổng kết Bệnh án thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Tổng kết Bệnh án bệnh nhân: {0}-{1} thành công", Tkba.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text), Tkba.IsNew ? newaction.Insert : newaction.Update, "UI");

                    MessageBox.Show("Đã cập nhật Tổng kết Bệnh án thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdThemmoi_Click(object sender, EventArgs e)
        {
            if (m_enAct == action.Insert)
            {
                if (!Utility.AcceptQuestion("Bạn đang ở trạng thái thêm mới Tổng kết BA và có thể đã nhập một số thông tin. Nếu nhấn thêm mới các thông tin mới nhập có thể bị xóa.\nBạn có chắc chắn muốn làm lại từ đầu không?", "Xác nhận", true))
                {
                    return;
                }
            }
            m_enAct = action.Insert;
            ClearControl();
            ucThongtinnguoibenh_v21.txtMaluotkham.Focus();
            ucThongtinnguoibenh_v21.txtMaluotkham.SelectAll();
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtData = SPs.KcbTongketBAIn(Tkba.Id).GetDataSet().Tables[0];
                dtData.TableName = "noitru_tongketBA";
                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_tongketBA.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                noitru_inphieu.Inphieusoket15ngay(dtData, DateTime.Now, chkPreview.Checked, "noitru_tongketBA");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
