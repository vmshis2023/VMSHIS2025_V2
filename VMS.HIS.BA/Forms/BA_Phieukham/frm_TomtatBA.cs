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
using VMS.HIS.Bus.Emr;
using VMS.HIS.Danhmuc.Dungchung;
//using SubSonic.Utilities;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_TomtatBA : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;
        EmrDocuments emrdoc = new EmrDocuments();
        public EmrTomtatBa ttba = new EmrTomtatBa();

        KcbLuotkham objLuotkham = null;
        VKcbLuotkham objBenhnhan = null;
        NoitruPhieunhapvien objNhapvien;
        NoitruPhieuravien objRavien;
        KcbChandoanKetluan objChandoanKetluan;
        KcbDangkyKcb objCongkham;
        public action m_enAct = action.FirstOrFinished;
        public bool CallfromParent = false;
        public frm_TomtatBA()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtpNgayRavien.Value =dtpNgayNhapvien.Value=dtNgayTTBA.Value= DateTime.Now;
            ucThongtinnguoibenh_emr_basic1.noitrungoaitru = 1;
            ucThongtinnguoibenh_emr_basic1._OnEnterMe += ucThongtinnguoibenh_emr_basic1__OnEnterMe;
            this.KeyDown += frm_TomtatBA_KeyDown;
            ucThongtinnguoibenh_emr_basic1.SetReadonly();
            txtB_CTScanner.TextChanged += soluongto_TextChanged;
            txtB_Khac.TextChanged += soluongto_TextChanged;
            txtB_SieuAm.TextChanged += soluongto_TextChanged;
            txtB_XetNghiem.TextChanged += soluongto_TextChanged;
            txtB_Xquang.TextChanged += soluongto_TextChanged;
        }
        void soluongto_TextChanged(object sender, EventArgs e)
        {
            txtB_Tongso.Text = (Utility.Int32Dbnull(txtB_CTScanner.Text, 0) + Utility.Int32Dbnull(txtB_Khac.Text, 0) + Utility.Int32Dbnull(txtB_SieuAm.Text, 0) + Utility.Int32Dbnull(txtB_XetNghiem.Text, 0) + Utility.Int32Dbnull(txtB_Xquang.Text, 0)).ToString();
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

        void ucThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
                objCongkham = new Select().From(KcbDangkyKcb.Schema)
                    .Where(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objLuotkham.IdCongkhamNhapvien)
                    .ExecuteSingle<KcbDangkyKcb>();
                objNhapvien = new Select().From(NoitruPhieunhapvien.Schema).Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                objRavien = new Select().From(NoitruPhieuravien.Schema).Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
                objChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                    .Where(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                    .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objLuotkham.IdCongkhamNhapvien)//Công khám kết thúc hoặc công khám nhập viện
                    .ExecuteSingle<KcbChandoanKetluan>();
                ttba = new Select().From(EmrTomtatBa.Schema).Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrTomtatBa>();
                if (ttba != null) m_enAct = action.Update;
                //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_KHOITAOBA_TRUOCKHILAM_TKBA", "0", true) == "1")
                //{
                //    SqlQuery sqlQuery = new Select().From<EmrHosoluutru>()
                //          .Where(EmrHosoluutru.Columns.MaLuotkham)
                //          .IsEqualTo(objLuotkham.MaLuotkham)
                //          .And(EmrHosoluutru.Columns.IdBenhnhan)
                //          .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
                //    EmrHosoluutru objhosoBA = sqlQuery.ExecuteSingle<EmrHosoluutru>();
                //    if (objhosoBA == null)
                //    {
                //        Utility.ShowMsg("Người bệnh chưa được khởi tạo Bệnh án nên không thể làm tóm tắt BA(BA_KHOITAOBA_TRUOCKHILAM_TTBA=1)");
                //        cmdSave.Enabled = false;
                //        return;
                //    }
                //}
                cmdSave.Enabled = true;
                FillData4Update();
                cmdIn.Enabled = cmdXoa.Enabled = ttba != null && ttba.Id > 0;
                //cmdSave.Enabled = !objLuotkham.NgayRavien.HasValue;
            }
        }
        string getBenhPhu_V1(string ma,string ten)
        {
            // Tách thành mảng
            var arrMa = ma.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var arrTen = string.IsNullOrWhiteSpace(ten)
                ? Enumerable.Empty<string>().ToArray()
                : ten.Split(new[] { ',' }, StringSplitOptions.None);

            // Ghép
            var result = string.Join(",", arrMa.Select((m, i) =>
            {
                string t = i < arrTen.Length ? arrTen[i] : "";
                return $"{t}({m})";
            }));
            return result;
        }
        string getBenhPhu_V2(string ma, string ten)
        {
            var arrMa = ma.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(s => s.Trim())
                       .ToArray();

            var arrTen = string.IsNullOrWhiteSpace(ten)
                ? new string[0]               // hoặc Array.Empty<string>() nếu .NET >=4.6
                : ten.Split(new[] { ',' }, StringSplitOptions.None)
                     .Select(s => s.Trim())
                     .ToArray();

            var result = string.Join(",", arrMa.Select((m, i) =>
            {
                // nếu tên thiếu hoặc rỗng thì để trống
                string t = i < arrTen.Length ? arrTen[i] : "";
                return $"{m}:{t}";
            }));
            return result;
        }
        void FillData4Update()
        {
            ClearControl();
            txtId.Clear();
            dtpNgayRavien.Text = "";
            txtTinhtrangRavien._Text = "";
            txt_chandoan_ravien.Text = "";
            txtPPdieutri.Text = "";
            txtHuongdieutri.Text = "";
            chkNoikhoa.Checked = false;
            chkPTTT.Checked = false;
            txtNoikhoamota.Text = "";
            txtPTTTmota.Text = "";
            EmrPhieukhambenh objPKB = new Select().From(EmrPhieukhambenh.Schema)
               .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
               .And(EmrPhieukhambenh.Columns.Noitru).IsEqualTo(1)
               .ExecuteSingle<EmrPhieukhambenh>();
            if(objPKB==null)
                objPKB = new Select().From(EmrPhieukhambenh.Schema)
               .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
               .And(EmrPhieukhambenh.Columns.Noitru).IsEqualTo(0)
               .ExecuteSingle<EmrPhieukhambenh>();
            if (objRavien != null)
            {
                string cdrv = string.Format("{0}:{1},{2}", objRavien.MotaBenhchinh, objRavien.MabenhChinh, getBenhPhu_V1(Utility.sDbnull( objRavien.MabenhPhu), Utility.sDbnull(objRavien.TenBenhphu)));
                if (Utility.sDbnull(objRavien.ChanDoan).Length > 0)
                    cdrv += "," + Utility.sDbnull(objRavien.ChanDoan);
                dtpNgayRavien.Value = objLuotkham.NgayRavien.Value;
                txt_chandoan_ravien.Text = cdrv;
                txtPPdieutri.Text = objRavien.PhuongphapDieutri;
                txtTinhtrangRavien.SetCode(objRavien.MaTinhtrangravien);
                txtTruongkhoa.SetId(objRavien.IdBacsiChuyenvien); 
                autoKhoa.SetId(objRavien.IdKhoaravien);
                foreach (CheckBox cb in pnlKetquadieutriravien.Controls)
                    if (Utility.sDbnull(cb.Tag, "-1") == objRavien.MaKquaDieutri)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
            }
            else if (objLuotkham.TrangthaiNoitru <=0)//TKBA ngoại trú
            {
                // txtTinhtrangRavien._Text= objChandoanKetluan.
                if (objCongkham != null && objCongkham.ThoigianKetthuc.HasValue)
                    dtpNgayRavien.Value = objCongkham.ThoigianKetthuc.Value;
                else
                    dtpNgayRavien.ResetText();
                autoKhoa.SetId(objCongkham.IdKhoakcb);
                DmucKhoaphong objKhoaphong = DmucKhoaphong.FetchByID(objCongkham.IdKhoakcb);
                if (objKhoaphong != null)
                    txtTruongkhoa.SetId(objKhoaphong.IdTruongkhoa);
              
            }
            if (objLuotkham.TrangthaiNoitru > 0)
            {
                txtBSDieuTri.SetId(Utility.sDbnull(objLuotkham.IdBsDieutrinoitruChinh));
                txt_chandoanvaovien.Text = objNhapvien.ChandoanVaovien;
                autoLydovv._Text = objNhapvien.LydoNhapvien;
                dtpNgayNhapvien.Value = objLuotkham.NgayNhapvien.Value;
                txtTiensubenh.Text = objNhapvien != null ? objNhapvien.TsuBanthan : "";
                txtquatrinhbenhly.Text = objNhapvien != null ? objNhapvien.QuatrinhBenhly : "";
                txtDauhieulamsang.Text = objPKB!=null? objPKB.BoPhan:"";
            }
            else//TKBA Ngoại trú
            {
                txtBSDieuTri.SetId(Utility.sDbnull(objCongkham.IdBacsikham));
                txt_chandoanvaovien.Text = objChandoanKetluan != null ? objChandoanKetluan.Chandoan : "";
                autoLydovv._Text = objChandoanKetluan!=null? objChandoanKetluan.TrieuchungBandau:"";
                dtpNgayNhapvien.Value = objLuotkham.NgayTiepdon;
                txtTiensubenh.Text = objChandoanKetluan != null ? objChandoanKetluan.TiensuBenh : "";
                txtquatrinhbenhly.Text = objChandoanKetluan != null ? objChandoanKetluan.QuatrinhBenhly : "";
                txtTomtatCLS.Text = objChandoanKetluan != null ? objChandoanKetluan.TomtatCls : "";
                txtHuongdieutri.Text = objChandoanKetluan != null ? objChandoanKetluan.XuTri : "";
                DmucChung objTemp = THU_VIEN_CHUNG.LayDulieuDanhmucChung("KQK", objChandoanKetluan.Ketluan);
                if (objTemp != null)
                    txttinhtrangravienMota.Text = objTemp.Ten;
                objTemp = THU_VIEN_CHUNG.LayDulieuDanhmucChung("HDT", objChandoanKetluan.HuongDieutri);
                if (objTemp != null)
                    txtPPdieutri.Text = objTemp.Ten;
            }
           

            if (ttba != null && ttba.Id>0)
            {
                txtId.Text = ttba.Id.ToString();
                txt_chandoanvaovien.Text = ttba.ChandoanVaovien;
                txt_chandoan_ravien.Text = ttba.ChandoanRavien;
                autoLydovv._Text = ttba.LydoVaovien;
                txtPPdieutri.Text = ttba.PhuongphapDieutri;
                txttinhtrangravienMota.Text = ttba.TinhtrangRavienMota;
                txtquatrinhbenhly.Text = ttba.QuatrinhbenhlyDienbienlamsang;
                txtTiensubenh.Text = ttba.TiensuBenh;
                txtTomtatCLS.Text = ttba.TomtatKqcls;
                txtDauhieulamsang.Text = ttba.DauhieuLamsang;
                txtHuongdieutri.Text = ttba.HuongDieutri;
                txtNoikhoamota.Text = ttba.NoikhoaMota;
                txtPTTTmota.Text = ttba.PtttMota;

                txtNguoiGiaoHoSo.SetId(ttba.IdNguoigiaoHoso);
                txtNguoiNhanHoSo.SetId(ttba.IdNguoinhanHoso);
                txtBSDieuTri.SetId(ttba.IdBacsiDieutri);
                txtGDBV.SetId(ttba.IdGiamdoc);
                txtTruongkhoa.SetId(ttba.IdTruongkhoadieutri);

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
            txtSoHoso.Text = ttba == null || string.IsNullOrEmpty(Utility.sDbnull(ttba.SoHoso, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(9) : Utility.sDbnull(ttba.SoHoso, "");
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
            txtSoHoso.Clear();
        }
        private Boolean isValidData()
        {
            string Msg = "";
            if (Utility.sDbnull(txtSoHoso.Text) == "")
            {
                Msg = "Bạn phải nhập số hồ sơ";
                Utility.ShowMsg(Msg);
                txtSoHoso.Focus();
                return false;
            }
            DataTable dtData = new Select().From(EmrTomtatBa.Schema)
                .Where(EmrTomtatBa.Columns.SoHoso).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
                .And(EmrTomtatBa.Columns.Id).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
                .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Số hồ sơ đã được sử dụng. Vui lòng nhập số hồ sơ khác";
                Utility.ShowMsg(Msg);
               txtSoHoso.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txt_chandoanvaovien.Text))
            {
                Utility.ShowMsg("Thông tin  Chẩn đoán ra viện không được bỏ trống", "Cảnh báo", MessageBoxIcon.Warning);
                txt_chandoanvaovien.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_chandoan_ravien.Text))
            {
                Utility.ShowMsg("Thông tin Chẩn đoán ra viện không được bỏ trống", "Cảnh báo", MessageBoxIcon.Warning);
                txt_chandoan_ravien.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(autoLydovv.Text))
            {
                Utility.ShowMsg("Thông tin Lý do vào viện không được bỏ trống", "Cảnh báo", MessageBoxIcon.Warning);
                autoLydovv.Focus();
                return false;
            }
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
            //if (txtNguoiGiaoHoSo.MyID == "-1")
            //{
            //    Utility.ShowMsg("Bạn cần chọn người giao hồ sơ");
            //    txtNguoiGiaoHoSo.Focus();
            //    return false;
            //}
            //if (txtNguoiNhanHoSo.MyID == "-1")
            //{
            //    Utility.ShowMsg("Bạn cần chọn người nhận hồ sơ");
            //    txtNguoiNhanHoSo.Focus();
            //    return false;
            //}
            //if (txtBSDieuTri.MyID == "-1")
            //{
            //    Utility.ShowMsg("Bạn cần chọn Bác sĩ điều trị");
            //    txtBSDieuTri.Focus();
            //    return false;
            //}
            //if (txtTruongkhoa.MyID == "-1")
            //{
            //    Utility.ShowMsg("Bạn cần chọn Trưởng khoa điều trị");
            //    txtTruongkhoa.Focus();
            //    return false;
            //}
            if (txtGDBV.MyID == "-1")
            {
                Utility.ShowMsg("Bạn cần chọn Người đại diện đơn vị");
                txtGDBV.Focus();
                return false;
            }
            return true;
        }

        private void frm_TomtatBA_Load(object sender, EventArgs e)
        {
            DataTable dtBacsi = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, -1);
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { autoLydovv.LOAI_DANHMUC, txtTinhtrangRavien.LOAI_DANHMUC }, true);
            autoLydovv.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLydovv.LOAI_DANHMUC));
            txtTruongkhoa.Init(dtBacsi, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoiGiaoHoSo.Init(txtTruongkhoa.AutoCompleteSource, txtTruongkhoa.defaultItem);
            txtNguoiNhanHoSo.Init(txtTruongkhoa.AutoCompleteSource, txtTruongkhoa.defaultItem);
            txtGDBV.Init(txtTruongkhoa.AutoCompleteSource, txtTruongkhoa.defaultItem);
            txtBSDieuTri.Init(txtTruongkhoa.AutoCompleteSource, txtTruongkhoa.defaultItem);

            txtTinhtrangRavien.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtTinhtrangRavien.LOAI_DANHMUC));
            DataTable mDtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
            autoKhoa.Init(mDtKhoaNoitru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            if (ttba != null && m_enAct == action.Update)
            {
                FillData4Update();
            }
            else
            {
                ucThongtinnguoibenh_emr_basic1.Refresh();
            }
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {

          
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
                        if (ttba == null) ttba = new EmrTomtatBa();
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
                        ttba.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
                        ttba.SoHoso = Utility.DoTrim(txtSoHoso.Text);
                        ttba.MaLuotkham = objLuotkham.MaLuotkham;
                        ttba.IdBenhnhan = (int)objLuotkham.IdBenhnhan;
                        ttba.IdKhoadieutri = Utility.Int32Dbnull(autoKhoa.MyID, -1);
                        ttba.ChandoanVaovien = Utility.sDbnull(txt_chandoanvaovien.Text);
                        ttba.LydoVaovien = Utility.sDbnull(autoLydovv.Text);
                        ttba.ChandoanRavien = Utility.sDbnull(txt_chandoan_ravien.Text);
                        ttba.QuatrinhbenhlyDienbienlamsang = Utility.sDbnull(txtquatrinhbenhly.Text);
                        ttba.TiensuBenh = Utility.sDbnull(txtTiensubenh.Text);
                        ttba.TomtatKqcls = Utility.sDbnull(txtTomtatCLS.Text);
                        ttba.NgayTtba = dtNgayTTBA.Value;
                        ttba.DauhieuLamsang = Utility.DoTrim(txtDauhieulamsang.Text);
                        ttba.Noikhoa = Utility.Bool2byte(chkNoikhoa.Checked);
                        ttba.NoikhoaMota = chkNoikhoa.Checked? Utility.sDbnull(txtNoikhoamota.Text):"";
                        ttba.Pttt = Utility.Bool2byte(chkPTTT.Checked);
                        ttba.PtttMota = chkPTTT.Checked? Utility.sDbnull(txtPTTTmota.Text):"";
                        ttba.TinhtrangRavienMota = Utility.sDbnull(txttinhtrangravienMota.Text);
                        ttba.PhuongphapDieutri = Utility.sDbnull(txtPPdieutri.Text);
                        ttba.HuongDieutri = Utility.sDbnull(txtHuongdieutri.Text);

                        ttba.IdNguoigiaoHoso = Utility.Int16Dbnull(txtNguoiGiaoHoSo.MyID);
                        ttba.MaNguoigiaoHoso = txtNguoiGiaoHoSo.MyCode;
                        ttba.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
                        ttba.MaNguoinhanhoso = txtNguoiNhanHoSo.MyCode;
                        
                        ttba.IdTruongkhoadieutri = Utility.Int16Dbnull(txtTruongkhoa.MyID);
                        ttba.MaTruongkhoadieutri = txtTruongkhoa.MyCode;


                        ttba.IdGiamdoc = Utility.Int16Dbnull(txtGDBV.MyID);
                        ttba.MaGiamdoc = txtGDBV.MyCode;
                        ttba.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
                        ttba.MaBacsiDieutri = txtBSDieuTri.MyCode;

                        ttba.SotoCt = Utility.Int16Dbnull(txtB_CTScanner.Text);
                        ttba.SotoXquang = Utility.Int16Dbnull(txtB_Xquang.Text);
                        ttba.SotoSieuam = Utility.Int16Dbnull(txtB_SieuAm.Text);
                        ttba.SotoXetnghiem = Utility.Int16Dbnull(txtB_XetNghiem.Text);
                        ttba.SotoKhac = Utility.Int16Dbnull(txtB_Khac.Text);

                        ttba.Save();
                      
                        emrdoc.InitDocument(ttba.IdBenhnhan, ttba.MaLuotkham, Utility.Int64Dbnull(ttba.Id), ttba.NgayTtba.Value, Loaiphieu_HIS.PHIEU_TTBA, "BA_TTBA", ttba.NguoiTao, -1, -1, Utility.Byte2Bool(1), "", true,false,"",Loaiphieu_HIS.PHIEU_TTBA);
                        emrdoc.Force2Saved = true;
                        emrdoc.Save();
                        new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.IdBsDieutrinoitruChinh).EqualTo(txtBSDieuTri.MyID)
                            .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            .Execute();
                        //EmrBa objEmrBa = new Select().From(EmrBa.Schema).Where(EmrBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrBa>();
                        //if (objEmrBa == null) objEmrBa = new EmrBa();
                        //if (objEmrBa.IdBa > 0)
                        //{
                        //    objEmrBa.IsNew = false;
                        //    objEmrBa.MarkOld();
                        //    objEmrBa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                        //    objEmrBa.NguoiSua = globalVariables.UserName;
                        //    //objEmrBa.TkbaQtbl = ttba.QuatrinhbenhlyDienbienlamsang;
                        //    //objEmrBa.TkbaTtkqxn = ttba.TomtatKqcls;
                        //    //objEmrBa.TkbaTtrv = ttba.TinhtrangRavienMota;
                        //    //objEmrBa.TkbaPpdt = ttba.PhuongphapDieutri;
                        //    //objEmrBa.TkbaHdt = ttba.HuongDieutri;
                        //    objEmrBa.Save();
                        //}
                        //else
                        //{
                        //    //Phải khởi tạo BA xong mới được làm tổng kết BA
                        //}
                    }
                    scope.Complete();

                }
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới Tổng kết Bệnh án bệnh nhân: {0}-{1} thành công", ttba.MaLuotkham, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text), ttba.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới Tổng kết Bệnh án thành công");
                    cmdIn.Enabled = cmdXoa.Enabled = true;
                    if (_OnCreated != null) _OnCreated(ttba.Id, action.Insert);
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Tổng kết Bệnh án bệnh nhân: {0}-{1} thành công", ttba.MaLuotkham, ucThongtinnguoibenh_emr_basic1.txtTenBN.Text), ttba.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null) _OnCreated(ttba.Id, action.Update);
                    MessageBox.Show("Đã cập nhật Tổng kết Bệnh án thành công");
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
            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            try
            {
                globalVariables.dtSignInfor = SPs.EmrLaythongtinChukyTrenphieu(ttba.Id.ToString(), "", 1).GetDataSet().Tables[0];
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

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            //Cần thêm kiểm tra BA đã đóng thì không cho xóa TKBA nữa
            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin Tổng kết Bệnh án không ?", "Thông báo", true))
            {
                int banghi = new Delete().From<EmrTomtatBa>()
                     .Where(EmrTomtatBa.Columns.Id)
                     .IsEqualTo(Utility.Int32Dbnull(ttba.Id))
                     .Execute();
                //Xóa phiếu EMR, dùng cách cập nhật xem sao
                
                    emrdoc.DeleteDocument(ttba.Id, Loaiphieu_HIS.PHIEU_TKBA, "BA_TKBA");
                if (banghi > 0)
                {
                    ttba = new EmrTomtatBa();
                    Utility.ShowMsg("Bạn xóa thông tin Tổng kết Bệnh án thành công", "Thông báo");

                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
                    ucThongtinnguoibenh_emr_basic1__OnEnterMe();
                }

            }
        }

        private void lnk_kq_cls_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_XemKQCLS_V1 _XemKQCLS_V1 = new frm_XemKQCLS_V1(this.objLuotkham,100);
            if(_XemKQCLS_V1.ShowDialog()==DialogResult.OK)
            {
                string result = _XemKQCLS_V1.result;
                if (Utility.sDbnull(txtTomtatCLS.Text) != "")
                {
                    if (Utility.AcceptQuestion("Thông tin tóm tắt KQ cận lâm sàng sẽ được cập nhật theo các kết quả vừa chọn. Bạn có chắc chắn?", "Xác nhận", true))
                    {
                        txtTomtatCLS.Text = result;
                    }
                }
                else
                    txtTomtatCLS.Text = result;
            }    
        }

        private void lnk_chandoanvaovien_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (objNhapvien != null)
            {
                string cdrv = Utility.sDbnull(objNhapvien.ChandoanVaovien);// string.Format("{0}:{1},{2}", objNhapvien.MotaBenhchinh, objNhapvien.MabenhChinh, getBenhPhu_V1(Utility.sDbnull(objRavien.MabenhPhu), Utility.sDbnull(objRavien.TenBenhphu)));
                //if (Utility.sDbnull(objNhapvien.ChandoanVaovien).Length > 0)
                //    cdrv += "," + Utility.sDbnull(objNhapvien.ChandoanVaovien);
                txt_chandoanvaovien.Text = cdrv;
            }
        }

        private void lbk_chandoan_ravien_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (objRavien != null)
            {
                string cdrv = string.Format("{0}:{1},{2}", objRavien.MotaBenhchinh, objRavien.MabenhChinh, getBenhPhu_V1(Utility.sDbnull(objRavien.MabenhPhu), Utility.sDbnull(objRavien.TenBenhphu)));
                if (Utility.sDbnull(objRavien.ChanDoan).Length > 0)
                    cdrv += "," + Utility.sDbnull(objRavien.ChanDoan);
                txt_chandoan_ravien.Text = cdrv;
            }
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSoHoso.Text = THU_VIEN_CHUNG.TT25LaySohoso(9);
        }
    }
}
