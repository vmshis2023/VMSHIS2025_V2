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
//using SubSonic.Utilities;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_TomtatBA : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;

        public KcbTomtatBA ttba = new KcbTomtatBA();
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
                ttba = new Select().From(KcbTomtatBA.Schema).Where(KcbTomtatBA.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(KcbTomtatBA.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbTomtatBA>();
                if (ttba != null) m_enAct = action.Update;
                FillData4Update();
                cmdIn.Enabled = cmdXoa.Enabled = ttba != null && ttba.Id > 0;
                //cmdSave.Enabled = !objLuotkham.NgayRavien.HasValue;
            }
        }

        void FillData4Update()
        {
            ClearControl();
            dtTuNgay.Value = objLuotkham.NgayNhapvien.Value;
            dtDenNgay.Text = "";
            txtTinhtrangRavien._Text = "";
            txtCDRavien.Text = "";
            txtPPdieutri._Text = "";
            txtHuongdieutri.Text = "";
            chkNoikhoa.Checked = false;
            chkPTTT.Checked = false;
            txtNoikhoamota.Text = "";
            txtPTTTmota.Text = "";
            if (objRavien != null)
            {
                dtDenNgay.Value = objLuotkham.NgayRavien.Value;
                txtCDRavien.Text = objRavien.ChanDoan;
                txtPPdieutri._Text = objRavien.PhuongphapDieutri;
                txtTinhtrangRavien.SetCode(objRavien.MaTinhtrangravien);
            }
            txtCDnhapvien.Text = objNhapvien.ChandoanVaovien;
            
            autoKhoa.SetId(objLuotkham.IdKhoanoitru);
            txtTiensubenh.Text = objChandoanKetluan.TiensuBenh;
          
            if (ttba != null)
            {
                txtquatrinhbenhly.Text = ttba.QuatrinhbenhlyDienbienlamsang;
                txtTiensubenh.Text = ttba.TiensuBenh;
                txtTomtatCLS.Text = ttba.TomtatKqcls;
                txtDauhieulamsang.Text = ttba.DauhieuLamsang;
                txtHuongdieutri.Text = ttba.HuongDieutri;
                txtNoikhoamota.Text = ttba.NoikhoaMota;
                txtPTTTmota.Text = ttba.PtttMota;
                chkNoikhoa.Checked = Utility.Byte2Bool(ttba.Noikhoa);
                chkPTTT.Checked = Utility.Byte2Bool(ttba.Pttt);
                if (ttba.NgayTtba.HasValue)
                    dtNgayTTBA.Value = ttba.NgayTtba.Value;
            }
        }

        public void ClearControl()
        {
            txtquatrinhbenhly.Clear();
            txtTiensubenh.Clear();
            txtTomtatCLS.Clear();
            txtDauhieulamsang.Clear();
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
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { autoLydovv.LOAI_DANHMUC, txtPPdieutri.LOAI_DANHMUC,txtTinhtrangRavien.LOAI_DANHMUC }, true);
            autoLydovv.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, autoLydovv.LOAI_DANHMUC));
            txtPPdieutri.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtPPdieutri.LOAI_DANHMUC));
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

            if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin Tóm tắt bệnh án không ?", "Thông báo", true))
            {
                int banghi = new Delete().From<KcbTomtatBA>()
                     .Where(KcbTomtatBA.Columns.Id)
                     .IsEqualTo(Utility.Int32Dbnull(ttba.Id))
                     .Execute();
                if (banghi > 0)
                {
                    ttba = new KcbTomtatBA();
                    Utility.ShowMsg("Bạn xóa thông tin Tóm tắt bệnh án thành công", "Thông báo");
                   
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
            //var objForm = new frmPrintPreview("Tóm tắt BỆNH ÁN", crpt, true, true);
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
            //crpt.SetParameterValue("sTitleReport", "Tóm tắt BỆNH ÁN");
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
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lưu Tóm tắt bệnh án?", "Thông báo", true)) return;
                if (ttba == null) ttba = new KcbTomtatBA();
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
                ttba.QuatrinhbenhlyDienbienlamsang =Utility.DoTrim( txtquatrinhbenhly.Text);
                ttba.TiensuBenh = Utility.DoTrim(txtTiensubenh.Text);
                ttba.TomtatKqcls = Utility.DoTrim(txtTomtatCLS.Text);
                ttba.NgayTtba = dtNgayTTBA.Value;
                ttba.DauhieuLamsang = Utility.DoTrim(txtDauhieulamsang.Text);
                ttba.Noikhoa = Utility.Bool2byte(chkNoikhoa.Checked);
                ttba.NoikhoaMota = Utility.sDbnull(chkPTTT.Text);
                ttba.Pttt = Utility.Bool2byte(chkNoikhoa.Checked);
                ttba.PtttMota = Utility.sDbnull(txtPTTTmota.Text);
                ttba.HuongDieutri = Utility.sDbnull(txtHuongdieutri.Text);
                ttba.Save();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới Tóm tắt Bệnh án bệnh nhân: {0}-{1} thành công", ttba.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text), ttba.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới Tóm tắt Bệnh án thành công. Nhấn Ok để kết thúc");
                    cmdIn.Enabled = cmdXoa.Enabled = true;
                    if (_OnCreated != null) _OnCreated(ttba.Id, action.Insert);
                    m_enAct = action.Update;
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Tóm tắt Bệnh án bệnh nhân: {0}-{1} thành công", ttba.MaLuotkham, ucThongtinnguoibenh_v21.txtTenBN.Text), ttba.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null) _OnCreated(ttba.Id, action.Update);
                    MessageBox.Show("Đã cập nhật Tóm tắt Bệnh án thành công. Nhấn Ok để kết thúc");
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

                
                if (ttba == null || ttba.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo tờ tóm tắt Bệnh án trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.KcbTomtatBAIn(ttba.Id, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                dtData.TableName = "noitru_tomtatBA";
                List<string> lstAddedFields = new List<string>() {"gioitinh_nam","gioitinh_nu","noikhoa_khong", "noikhoa_co", "pttt_khong", "pttt_co",
                "tinhtrangravien_khoi", "tinhtrangravien_do", "tinhtrangravien_khongthaydoi",
                "tinhtrangravien_nanghon", "tinhtrangravien_tuvong", "tinhtrangravien_xinve","tinhtrangravien_khongxacdinh"};
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));

                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_tomtatBA.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                dtData.TableName = "noitru_tomtatBA";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["dia_diem"] = globalVariables.gv_strDiadiem;
                drData["ngay_thang_nam"] = Utility.FormatDateTime(ttba.NgayTtba.Value);
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("gioitinh_nam",objBenhnhan.IdGioitinh==0? "1":"0");
                dicMF.Add("gioitinh_nu", objBenhnhan.IdGioitinh == 0 ? "0" : "1");
                dicMF.Add("noikhoa_co", Utility.Byte2Bool(ttba.Noikhoa) ? "1" : "0");
                dicMF.Add("noikhoa_khong", Utility.Byte2Bool(ttba.Noikhoa) ? "0" : "1");
                dicMF.Add("pttt_co", Utility.Byte2Bool(ttba.Pttt) ? "1" : "0");
                dicMF.Add("pttt_khong", Utility.Byte2Bool(ttba.Pttt) ? "0" : "1");
                dicMF.Add("tinhtrangravien_khoi", Utility.sDbnull(objRavien.MaKquaDieutri)=="1" ? "1" : "0");
                dicMF.Add("tinhtrangravien_do", Utility.sDbnull(objRavien.MaKquaDieutri) == "2" ? "1" : "0");
                dicMF.Add("tinhtrangravien_khongthaydoi", Utility.sDbnull(objRavien.MaKquaDieutri) == "3" ? "1" : "0");
                dicMF.Add("tinhtrangravien_nanghon", Utility.sDbnull(objRavien.MaKquaDieutri) == "4" ? "1" : "0");
                dicMF.Add("tinhtrangravien_tuvong", Utility.sDbnull(objRavien.MaKquaDieutri) == "5" ? "1" : "0");
                dicMF.Add("tinhtrangravien_xinve", Utility.sDbnull(objRavien.MaKquaDieutri) == "6" ? "1" : "0");
                dicMF.Add("tinhtrangravien_khongxacdinh", Utility.sDbnull(objRavien.MaKquaDieutri) == "7" ? "1" : "0");
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\TomtatBA_V1.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("noitru_tomtatBA", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu tóm tắt Bệnh án tại thư mục sau :" + PathDoc);
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
                               Path.GetFileNameWithoutExtension(PathDoc), "TOMTAT_BA", objLuotkham.MaLuotkham, Utility.sDbnull(ttba.Id), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                    foreach (string field in dicMF.Keys)
                    {
                        if (builder.MoveToMergeField(field))
                        {
                            if (dicMF[field] == "1")
                            {
                                builder.InsertCheckBox(field, true, 10);
                            }
                            else
                            {
                                builder.InsertCheckBox(field, false, 10);
                            }
                        }
                    }
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
