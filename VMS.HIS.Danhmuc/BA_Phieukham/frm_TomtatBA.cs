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
using VMS.HIS.Bus;
using System.Transactions;
//using SubSonic.Utilities;

namespace VNS.HIS.UI.BA
{
    public partial class frm_TomtatBA : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;

        public EmrTongketBenhan ttba = new EmrTongketBenhan();
        KcbLuotkham objLuotkham = null;
        VKcbLuotkham objBenhnhan = null;
        NoitruPhieunhapvien objNhapvien;
        NoitruPhieuravien objRavien;
        KcbChandoanKetluan objChandoanKetluan;
        public action m_enAct = action.FirstOrFinished;
        public bool CallfromParent = false;
        public frm_TomtatBA()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtDenNgay.Value =dtTuNgay.Value=dtNgayTTBA.Value= DateTime.Now;
            ucThongtinnguoibenh_v21.noitrungoaitru = 1;
            ucThongtinnguoibenh_v21._OnEnterMe += ucThongtinnguoibenh_v21__OnEnterMe;
            this.KeyDown += frm_TomtatBA_KeyDown;
            ucThongtinnguoibenh_v21.SetReadonly();
        }

        void frm_TomtatBA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((ActiveControl != null && (ActiveControl.Name == txtquatrinhbenhly.Name || ActiveControl.Name == txtTiensubenh.Name || ActiveControl.Name == txtTomtatCLS.Name || ActiveControl.Name == txtDauhieulamsang.Name)))
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

        void ucThongtinnguoibenh_v21__OnEnterMe()
        {
            if (ucThongtinnguoibenh_v21.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh_v21.objLuotkham;
                objBenhnhan = ucThongtinnguoibenh_v21.objBenhnhan;
               
                objNhapvien = new Select().From(NoitruPhieunhapvien.Schema).Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                objRavien = new Select().From(NoitruPhieuravien.Schema).Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
                objChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0).And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbChandoanKetluan>();
                ttba = new Select().From(EmrTongketBenhan.Schema).Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrTongketBenhan>();
                if (ttba != null) m_enAct = action.Update;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_KHOITAOBA_TRUOCKHILAM_TTBA", "0", true) == "1")
                {
                    SqlQuery sqlQuery = new Select().From<EmrHosoluutru>()
                          .Where(EmrHosoluutru.Columns.MaLuotkham)
                          .IsEqualTo(objLuotkham.MaLuotkham)
                          .And(EmrHosoluutru.Columns.IdBenhnhan)
                          .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
                    EmrHosoluutru objhosoBA = sqlQuery.ExecuteSingle<EmrHosoluutru>();
                    if (objhosoBA == null)
                    {
                        Utility.ShowMsg("Người bệnh chưa được khởi tạo Bệnh án nên không thể làm tóm tắt BA(BA_KHOITAOBA_TRUOCKHILAM_TTBA=1)");
                        cmdSave.Enabled = false;
                        return;
                    }
                }
                cmdSave.Enabled = true;
                FillData4Update();
                cmdIn.Enabled = cmdXoa.Enabled = ttba != null && ttba.Id > 0;
                //cmdSave.Enabled = !objLuotkham.NgayRavien.HasValue;
            }
        }

        void FillData4Update()
        {
            ClearControl();
            
            dtDenNgay.Text = "";
            txtTinhtrangRavien._Text = "";
            txtCDRavien.Text = "";
            txtPPdieutri.Text = "";
            txtHuongdieutri.Text = "";
            chkNoikhoa.Checked = false;
            chkPTTT.Checked = false;
            txtNoikhoamota.Text = "";
            txtPTTTmota.Text = "";
            if (objRavien != null)
            {
                dtDenNgay.Value = objLuotkham.NgayRavien.Value;
                txtCDRavien.Text = objRavien.ChanDoan;
                txtPPdieutri.Text = objRavien.PhuongphapDieutri;
                txtTinhtrangRavien.SetCode(objRavien.MaTinhtrangravien);
            }
            txtCDnhapvien.Text = objNhapvien.ChandoanVaovien;
            dtTuNgay.Value = objLuotkham.NgayNhapvien.Value;
            autoKhoa.SetId(objLuotkham.IdKhoanoitru);
            txtTiensubenh.Text = objChandoanKetluan.TiensuBenh;
            txtBSDieuTri.SetId(Utility.sDbnull(objLuotkham.IdBsDieutrinoitruChinh));
            if (ttba != null)
            {
                txtPPdieutri.Text = ttba.PhuongphapDieutri;
                txttinhtrangravienMota.Text = ttba.TinhtrangRavienMota;
                txtquatrinhbenhly.Text = ttba.QuatrinhbenhlyDienbienlamsang;
                txtTiensubenh.Text = ttba.TiensuBenh;
                txtTomtatCLS.Text = ttba.TomtatKqcls;
                txtDauhieulamsang.Text = ttba.DauhieuLamsang;
                txtHuongdieutri.Text = ttba.HuongDieutri;
                txtNoikhoamota.Text = ttba.NoikhoaMota;
                txtPTTTmota.Text = ttba.PtttMota;

                txtNguoiGiaoHoSo.Text = Utility.sDbnull(ttba.NguoigiaoHoso);
                txtNguoiNhanHoSo.Text = Utility.sDbnull(ttba.NguoiNhanhoso);
                
                txtB_CTScanner.Text = Utility.sDbnull(ttba.SotoCt);
                txtB_Xquang.Text = Utility.sDbnull(ttba.SotoXquang);
                txtB_SieuAm.Text = Utility.sDbnull(ttba.SotoSieuam);
                txtB_XetNghiem.Text = Utility.sDbnull(ttba.SotoXetnghiem);
                txtB_Khac.Text = Utility.sDbnull(ttba.SotoKhac);

                chkNoikhoa.Checked = Utility.Byte2Bool(ttba.Noikhoa);
                chkPTTT.Checked = Utility.Byte2Bool(ttba.Pttt);
                if (ttba.NgayTtba.HasValue)
                    dtNgayTTBA.Value = ttba.NgayTtba.Value;
            }
            else
            {

            }    
        }

        public void ClearControl()
        {
            txtquatrinhbenhly.Clear();
            txtTiensubenh.Clear();
            txtTomtatCLS.Clear();
            txtDauhieulamsang.Clear();
            txtNguoiGiaoHoSo.Clear();
            txtNguoiNhanHoSo.Clear();
            txtBSDieuTri.Clear();
            txtB_CTScanner.Clear();
            txtB_Xquang.Clear();
            txtB_SieuAm.Clear();
            txtB_XetNghiem.Clear();
            txtB_Khac.Clear();
        }
        private Boolean isValidData()
        {
            
            if (string.IsNullOrEmpty(txtquatrinhbenhly.Text))
            {
                Utility.ShowMsg("Thông tin quá trình bệnh lý không được bỏ trống", "Cảnh báo", MessageBoxIcon.Warning);
                txtquatrinhbenhly.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTomtatCLS.Text))
            {
                Utility.ShowMsg("Thông tin tóm tắt lâm sàng không được bỏ trống", "Cảnh báo", MessageBoxIcon.Warning);
                txtTomtatCLS.Focus();
                return false;
            }
           
            return true;
        }

        private void frm_TomtatBA_Load(object sender, EventArgs e)
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { autoLydovv.LOAI_DANHMUC, txtTinhtrangRavien.LOAI_DANHMUC }, true);
            autoLydovv.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLydovv.LOAI_DANHMUC));
           
            txtTinhtrangRavien.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtTinhtrangRavien.LOAI_DANHMUC));
            DataTable mDtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            autoKhoa.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            if (ttba != null && m_enAct == action.Update)
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

            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin Tổng kết Bệnh án không ?", "Thông báo", true))
            {
                int banghi = new Delete().From<EmrTongketBenhan>()
                     .Where(EmrTongketBenhan.Columns.Id)
                     .IsEqualTo(Utility.Int32Dbnull(ttba.Id))
                     .Execute();
                if (banghi > 0)
                {
                    ttba = new EmrTongketBenhan();
                    Utility.ShowMsg("Bạn xóa thông tin Tổng kết Bệnh án thành công", "Thông báo");
                   
                    ucThongtinnguoibenh_v21.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_v21.txtMaluotkham.SelectAll();
                    ucThongtinnguoibenh_v21__OnEnterMe();
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
            //var objForm = new frmPrintPreview("Tổng kết Bệnh án", crpt, true, true);
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
            //crpt.SetParameterValue("sTitleReport", "Tổng kết Bệnh án");
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
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        //if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lưu Tổng kết Bệnh án?", "Thông báo", true)) return;
                        if (ttba == null) ttba = new EmrTongketBenhan();
                        if (ttba.Id > 0)
                        {
                            ttba.IsNew = false;
                            ttba.MarkOld();
                            ttba.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                            ttba.NguoiSua = globalVariables.UserName;
                        }
                        else
                        {
                            ttba.IsNew = true;
                            ttba.NguoiTao = globalVariables.UserName;
                            ttba.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        ttba.MaLuotkham = objLuotkham.MaLuotkham;
                        ttba.IdBenhnhan = (int)objLuotkham.IdBenhnhan;
                        ttba.IdKhoadieutri = Utility.Int32Dbnull(autoKhoa.MyID, -1);
                        ttba.QuatrinhbenhlyDienbienlamsang = Utility.DoTrim(txtquatrinhbenhly.Text);
                        ttba.TiensuBenh = Utility.DoTrim(txtTiensubenh.Text);
                        ttba.TomtatKqcls = Utility.DoTrim(txtTomtatCLS.Text);
                        ttba.NgayTtba = dtNgayTTBA.Value;
                        ttba.DauhieuLamsang = Utility.DoTrim(txtDauhieulamsang.Text);
                        ttba.Noikhoa = Utility.Bool2byte(chkNoikhoa.Checked);
                        ttba.NoikhoaMota = Utility.sDbnull(txtNoikhoamota.Text);
                        ttba.Pttt = Utility.Bool2byte(chkPTTT.Checked);
                        ttba.PtttMota = Utility.sDbnull(txtPTTTmota.Text);
                        ttba.TinhtrangRavienMota = Utility.sDbnull(txttinhtrangravienMota.Text);
                        ttba.PhuongphapDieutri = Utility.sDbnull(txtPPdieutri.Text);
                        ttba.HuongDieutri = Utility.sDbnull(txtHuongdieutri.Text);
                        ttba.NguoigiaoHoso = txtNguoiGiaoHoSo.Text;
                        ttba.NguoiNhanhoso = txtNguoiNhanHoSo.Text;
                        ttba.SotoCt = Utility.Int16Dbnull(txtB_CTScanner.Text);
                        ttba.SotoXquang = Utility.Int16Dbnull(txtB_Xquang.Text);
                        ttba.SotoSieuam = Utility.Int16Dbnull(txtB_SieuAm.Text);
                        ttba.SotoXetnghiem = Utility.Int16Dbnull(txtB_XetNghiem.Text);
                        ttba.SotoKhac = Utility.Int16Dbnull(txtB_Khac.Text);

                        ttba.Save();
                        new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.IdBsDieutrinoitruChinh).EqualTo(txtBSDieuTri.MyID)
                            .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            .Execute();
                        EmrBa objEmrBa = new Select().From(EmrBa.Schema).Where(EmrBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrBa>();
                        if (objEmrBa == null) objEmrBa = new EmrBa();
                        if (objEmrBa.IdBa > 0)
                        {
                            objEmrBa.IsNew = false;
                            objEmrBa.MarkOld();
                            objEmrBa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                            objEmrBa.NguoiSua = globalVariables.UserName;
                            //objEmrBa.TkbaQtbl = ttba.QuatrinhbenhlyDienbienlamsang;
                            //objEmrBa.TkbaTtkqxn = ttba.TomtatKqcls;
                            //objEmrBa.TkbaTtrv = ttba.TinhtrangRavienMota;
                            //objEmrBa.TkbaPpdt = ttba.PhuongphapDieutri;
                            //objEmrBa.TkbaHdt = ttba.HuongDieutri;
                            objEmrBa.Save();
                        }
                        else
                        {
                            //Phải khởi tạo BA xong mới được làm tổng kết BA
                        }
                    }
                    scope.Complete();

                }
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới Tổng kết Bệnh án bệnh nhân: {0}-{1} thành công", ttba.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text), ttba.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới Tổng kết Bệnh án thành công. Nhấn Ok để kết thúc");
                    cmdIn.Enabled = cmdXoa.Enabled = true;
                    if (_OnCreated != null) _OnCreated(ttba.Id, action.Insert);
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Tổng kết Bệnh án bệnh nhân: {0}-{1} thành công", ttba.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text), ttba.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null) _OnCreated(ttba.Id, action.Update);
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
                if (!Utility.AcceptQuestion("Bạn đang ở trạng thái thêm mới Tóm tắt BA và có thể đã nhập một số thông tin. Nếu nhấn thêm mới các thông tin mới nhập có thể bị xóa.\nBạn có chắc chắn muốn làm lại từ đầu không?", "Xác nhận", true))
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

                clsInBA.InTomTatBA(ttba);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void chkNoikhoa_CheckedChanged(object sender, EventArgs e)
        {
            txtNoikhoamota.Enabled = chkNoikhoa.Checked;
        }

        private void chkPTTT_CheckedChanged(object sender, EventArgs e)
        {
            txtPTTTmota.Enabled = chkPTTT.Checked;
        }
    }
}
